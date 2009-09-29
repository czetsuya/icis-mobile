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
				frmProgressLoader.progressbar1.Maximum = 2;

				frmProgressLoader.Update(1, LanguageHelper.GetMessage("m_loading"));
				
				new ScaleEvent(this, obj);
				
				frmProgressLoader.progressbar1.Value = 2;
				frmProgressLoader.Hide();
			}
		}
		
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
				frmProgressLoader.progressbar1.Maximum = 2;

				frmProgressLoader.Update(1, LanguageHelper.GetMessage("m_loading"));
				
				new VariateEvent(this, obj);
				
				frmProgressLoader.progressbar1.Value = 2;
				frmProgressLoader.Hide();
			}
		}

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
				frmProgressLoader.progressbar1.Maximum = 2;

				frmProgressLoader.Update(1, LanguageHelper.GetMessage("m_loading"));
				
				new ObservationEvent(this, obj);
				
				frmProgressLoader.progressbar1.Value = 2;
				frmProgressLoader.Hide();
			}
		}
		#endregion

		#region Accessors
		public void SetStudyId(int x) 
		{
			study_id = x;
		}

		public int GetStudyId() 
		{
			return study_id;
		}
		#endregion

		public void Destroy() 
		{
		}
	}
}
