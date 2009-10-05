/**
 * @author edwardpantojalegaspi
 * @since 2009.09.21
 * */

using System;
using System.Windows.Forms;
using System.Collections;

using IcisMobile.Framework.Helper;
using IcisMobile.Framework.DataAccessLayer;
using IcisMobile.Framework;
using IcisMobile.Framework.DataCollection.Study;
using IcisMobile.Framework.EventHandler;

namespace IcisMobile.Framework
{
	/// <summary>
	/// Summary description for Engine.
	/// </summary>
	public class Engine
	{
		private frmProgress frmProgressLoader = new frmProgress();
		private int study_id = -1;
	
		#region Create Database
		/// <summary>
		/// Creates the database from xml schema. Xml file path is configurable in the config resource.
		/// </summary>
		public void InitSchema() 
		{
			if(ResourceHelper.ShowQuestion(LanguageHelper.GetMessage("engine_create_schema"), ""))
			{
				if(System.IO.File.Exists(Settings.TEMP_DIR + Settings.SCHEMA_FILE)) 
				{
					if(SQLBuilder.CreateDBFromSchema(XMLHelper.Instance().readSchema(Settings.TEMP_DIR + Settings.SCHEMA_FILE))) 
					{
						ResourceHelper.ShowInfo(LanguageHelper.GetMessage("db_init_completed"));
					} 
					else 
					{
						ResourceHelper.ShowInfo(LanguageHelper.GetMessage("engine_database_used"));
					}
				}
			}
		}
		#endregion
		
		#region Load Study Data
		/// <summary>
		/// Loads the data from an xml and text file. The file path are configurable in the config resource.
		/// The file is created and uploaded to the mobile from the desktop pc.
		/// </summary>
		public void LoadStudyFromFile() 
		{
			//get the default study file that contains study data
			frmProgressLoader.Show();			
			frmProgressLoader.progressbar1.Maximum = 6;
			
			frmProgressLoader.Update(1, LanguageHelper.GetMessage("m_load_study_1"));

			Study study = XMLHelper.Instance().parseStudy(Settings.TEMP_DIR + LanguageHelper.GetConfig("study_data_file"));
			String study_id = DataAccess.Instance().Insert(SQLBuilder.PrepareStudyScript(study));
			study.ID = study_id;
			
			frmProgressLoader.Update(2, LanguageHelper.GetMessage("m_load_study_2"));
			//load scales - FIRST FOREIGN KEY OF table factors
			DataAccess.Instance().Insert(SQLBuilder.PrepareScaleScript(study.GetScales()));
			
			frmProgressLoader.Update(3, LanguageHelper.GetMessage("m_load_study_3"));
			///load factors
			SQLBuilder.InsertFactors(ref study);
			
			frmProgressLoader.Update(4, LanguageHelper.GetMessage("m_load_study_4"));
			//load variates
			SQLBuilder.InsertVariates(ref study);			
			
			frmProgressLoader.Update(5, LanguageHelper.GetMessage("m_load_study_5"));
			//get the default file for study observation data, load the data
			ArrayList arrLevelNo = new ArrayList();
			DataAccess.Instance().Insert(SQLBuilder.PrepareStudyDataScript(ref arrLevelNo, study, FileHelper.ReadFile(Settings.TEMP_DIR + LanguageHelper.GetConfig("study_observation_data_file"))));

			//prepopulate observation data
			DataAccess.Instance().Insert(SQLBuilder.InsertObservationData(study, arrLevelNo));

			frmProgressLoader.Hide();

			ResourceHelper.ShowInfo(LanguageHelper.GetMessage("study_load_ok"));
		}
		#endregion
		
		#region Tab Control Handler
		/// <summary>
		/// This event is performed when the study tab page is clicked.
		/// </summary>
		/// <param name="obj"></param>
		public void StudyClicked(object obj) 
		{
			try 
			{
				new StudyEvent(this, obj);
			} 
			catch(Exception e) 
			{
				ResourceHelper.ShowInfo(LanguageHelper.GetMessage("m_load_schema"));
				((IcisMobile)((Control)obj).Parent.Parent).ShowTab(5);
			}
		}

		/// <summary>
		/// This event is performed when the scale tab page is clicked.
		/// </summary>
		/// <param name="obj"></param>
		public void ScaleClicked(object obj) 
		{
			if(study_id == -1) 
			{
				ResourceHelper.ShowInfo(LanguageHelper.GetMessage("e_select_study"));
				((IcisMobile)((Control)obj).Parent.Parent).ShowTab(1);
			} 
			else 
			{
				frmProgressLoader.Show();
				frmProgressLoader.progressbar1.Maximum = 3;

				frmProgressLoader.Update(1, LanguageHelper.GetMessage("m_loading"));
				
				new ScaleEvent(this, obj);

				frmProgressLoader.progressbar1.Value = 2;
				
				frmProgressLoader.progressbar1.Value = 3;
				frmProgressLoader.Hide();
			}
		}
		
		/// <summary>
		/// This event is performed when the variate tab page is clicked.
		/// </summary>
		/// <param name="obj"></param>
		public void VariateClicked(object obj) 
		{
			if(study_id == -1) 
			{
				ResourceHelper.ShowInfo(LanguageHelper.GetMessage("e_select_study"));
				((IcisMobile)((Control)obj).Parent.Parent).ShowTab(1);
			} 
			else 
			{
				frmProgressLoader.Show();
				frmProgressLoader.progressbar1.Maximum = 3;

				frmProgressLoader.Update(1, LanguageHelper.GetMessage("m_loading"));
				
				new VariateEvent(this, obj);

				frmProgressLoader.progressbar1.Value = 2;
				
				frmProgressLoader.progressbar1.Value = 3;
				frmProgressLoader.Hide();
			}
		}

		/// <summary>
		/// This event is performed when the observation tab page is clicked.
		/// </summary>
		/// <param name="obj"></param>
		public void ObservationClick(object obj) 
		{
			if(study_id == -1) 
			{
				ResourceHelper.ShowInfo(LanguageHelper.GetMessage("e_select_study"));
				((IcisMobile)((Control)obj).Parent.Parent).ShowTab(1);
			} 
			else 
			{
				frmProgressLoader.Show();
				frmProgressLoader.progressbar1.Maximum = 3;

				frmProgressLoader.Update(1, LanguageHelper.GetMessage("m_loading"));
				
				new ObservationEvent(this, obj);

				frmProgressLoader.progressbar1.Value = 2;
				
				frmProgressLoader.progressbar1.Value = 3;
				frmProgressLoader.Hide();
			}
		}
		#endregion

		#region Downloader
		/// <summary>
		/// Download the data from the device to desktop xml.
		/// </summary>
		public void DownloadData(TabControl obj) 
		{
			if(study_id == -1) 
			{
				ResourceHelper.ShowInfo(LanguageHelper.GetMessage("e_select_study"));
				obj.SelectedIndex = 1;
			} 
			else 
			{
				frmProgressLoader.Show();
				frmProgressLoader.progressbar1.Maximum = 2;

				frmProgressLoader.Update(1, LanguageHelper.GetMessage("m_saving"));
				new DownloadEvent(GetStudyId());
				
				frmProgressLoader.progressbar1.Value = 2;
				frmProgressLoader.Hide();
			}
		}
		#endregion

		#region Accessors
		/// <summary>
		/// Sets the study id that will be modified.
		/// </summary>
		/// <param name="x"></param>
		public void SetStudyId(int x) 
		{
			study_id = x;
		}

		/// <summary>
		/// Gets the study id currently selected.
		/// </summary>
		/// <returns></returns>
		public int GetStudyId() 
		{
			return study_id;
		}
		#endregion

		/// <summary>
		/// Destroy all the objects, resources opened and created.
		/// </summary>
		public void Destroy() 
		{
		}
	}
}
