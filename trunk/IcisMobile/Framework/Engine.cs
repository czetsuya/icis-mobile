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
		private static int STUDY_ID = -1;
	
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
			if(!FileHelper.isExists(Settings.TEMP_DIR + Settings.DATABASE_FILE))
			{
				ResourceHelper.ShowInfo(LanguageHelper.GetMessage("m_create_database"));
			} 
			else 
			{
				if(!FileHelper.isExists(Settings.TEMP_DIR + LanguageHelper.GetConfig("study_data_file")))
				{
					ResourceHelper.ShowInfo(LanguageHelper.GetMessage("m_no_study_data"));
				} 
				else {
					//get the default study file that contains study data
					frmProgressLoader.Show();			
					frmProgressLoader.progressbar1.Maximum = 6;
			
					frmProgressLoader.Update(1, LanguageHelper.GetMessage("m_load_study_1"));

					Study study = XMLHelper.Instance().parseStudy(Settings.TEMP_DIR + LanguageHelper.GetConfig("study_data_file"));
				

					//checks if a study is already existing in the database
					if(SQLBuilder.CheckStudy(study.NAME)) 
					{
						ResourceHelper.ShowInfo(LanguageHelper.GetMessage("m_study_exist"));
						frmProgressLoader.Hide();
					} 
					else 
					{
						String study_id = DataAccess.Instance().Insert(SQLBuilder.PrepareStudyScript(study));
						study.ID = study_id;
			
						frmProgressLoader.Update(2, LanguageHelper.GetMessage("m_load_study_2"));
						//load scales - FIRST FOREIGN KEY OF table factors
						SQLBuilder.InsertScales(study.GetScales(), study.ID);
			
						frmProgressLoader.Update(3, LanguageHelper.GetMessage("m_load_study_3"));
						///load factors
						SQLBuilder.InsertFactors(ref study);
			
						frmProgressLoader.Update(4, LanguageHelper.GetMessage("m_load_study_4"));
						//load variates
						SQLBuilder.InsertVariates(ref study);			
			
						frmProgressLoader.Update(5, LanguageHelper.GetMessage("m_load_study_5"));
						//get the default file for study observation data, load the data
						
						GC.Collect(); //reclaim memory

						ArrayList arrLevelNo = new ArrayList();
						//DataAccess.Instance().Insert(SQLBuilder.PrepareStudyDataScript(ref arrLevelNo, study, FileHelper.ReadFile(Settings.TEMP_DIR + LanguageHelper.GetConfig("study_observation_data_file"))));
						//DataAccess.Instance().InsertLevel(frmProgressLoader, "INSERT INTO level_varchar (study_id, factor_id, level_value, level_no) VALUES ({0}, {1}, '{2}', '{3}')", study.ID, SQLBuilder.PrepareStudyDataScript(ref arrLevelNo, study, FileHelper.ReadFile(Settings.TEMP_DIR + LanguageHelper.GetConfig("study_observation_data_file"))));
						Framework.Util.OptimizedDataHandler.InsertLevel(Settings.TEMP_DIR + LanguageHelper.GetConfig("study_observation_data_file"), frmProgressLoader, study);

						GC.Collect(); //reclaim memory

						//prepopulate observation data
						//DataAccess.Instance().Insert(SQLBuilder.InsertObservationData(study, arrLevelNo));
						//DataAccess.Instance().InsertData(frmProgressLoader, "INSERT INTO data_varchar (study_id, variate_id, level_no) VALUES ({0}, {1}, {2})", study.ID, SQLBuilder.InsertObservationData(study, arrLevelNo));

						frmProgressLoader.Hide();

						ResourceHelper.ShowInfo(LanguageHelper.GetMessage("study_load_ok"));
					}
					if(studyEvent != null) 
					{
						studyEvent.Init();
					}
				}
			}
		}
		#endregion
		
		#region Tab Control Handler
		private StudyEvent studyEvent;
		private ScaleEvent scaleEvent;
		private VariateEvent variateEvent;
		private ObservationEvent observationEvent;
		private DataEntryEvent deEvent;
		/// <summary>
		/// This event is performed when the study tab page is clicked.
		/// </summary>
		/// <param name="obj"></param>
		public void StudyClicked(object obj) 
		{
			try 
			{
				if(studyEvent == null) 
				{
					studyEvent = new StudyEvent(this, obj);
				}
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
			if(Engine.STUDY_ID == -1) 
			{
				ResourceHelper.ShowInfo(LanguageHelper.GetMessage("e_select_study"));
				((IcisMobile)((Control)obj).Parent.Parent).ShowTab(1);
			} 
			else 
			{
				if(scaleEvent == null) 
				{
					frmProgressLoader.Show();
					frmProgressLoader.progressbar1.Maximum = 2;

					frmProgressLoader.Update(1, LanguageHelper.GetMessage("m_loading"));

					scaleEvent = new ScaleEvent(this, obj);

					frmProgressLoader.progressbar1.Value = 2;
					frmProgressLoader.Hide();
				}
			}
		}
		
		/// <summary>
		/// This event is performed when the variate tab page is clicked.
		/// </summary>
		/// <param name="obj"></param>
		public void VariateClicked(object obj) 
		{
			if(Engine.STUDY_ID == -1) 
			{
				ResourceHelper.ShowInfo(LanguageHelper.GetMessage("e_select_study"));
				((IcisMobile)((Control)obj).Parent.Parent).ShowTab(1);
			} 
			else 
			{	
				if(variateEvent == null) 
				{
					frmProgressLoader.Show();
					frmProgressLoader.progressbar1.Maximum = 2;

					frmProgressLoader.Update(1, LanguageHelper.GetMessage("m_loading"));

					variateEvent = new VariateEvent(this, obj);

					frmProgressLoader.progressbar1.Value = 2;
					frmProgressLoader.Hide();
				}
			}
		}

		/// <summary>
		/// This event is performed when the data entry tab page is clicked.
		/// </summary>
		/// <param name="obj"></param>
		public void DataEntryClicked(object obj) 
		{
			if(Engine.STUDY_ID == -1) 
			{
				ResourceHelper.ShowInfo(LanguageHelper.GetMessage("e_select_study"));
				((IcisMobile)((Control)obj).Parent.Parent).ShowTab(1);
			} 
			else 
			{
				if(deEvent == null) 
				{
					frmProgressLoader.Show();
					frmProgressLoader.progressbar1.Maximum = 2;

					frmProgressLoader.Update(1, LanguageHelper.GetMessage("m_loading"));

					deEvent = new DataEntryEvent(this, obj);

					frmProgressLoader.progressbar1.Value = 2;
					frmProgressLoader.Hide();
				}
			}
		}

		/// <summary>
		/// This event is performed when the observation tab page is clicked.
		/// </summary>
		/// <param name="obj"></param>
		public void ObservationClick(object obj) 
		{
			if(Engine.STUDY_ID == -1) 
			{
				ResourceHelper.ShowInfo(LanguageHelper.GetMessage("e_select_study"));
				((IcisMobile)((Control)obj).Parent.Parent).ShowTab(1);
			} 
			else 
			{				
				if(observationEvent == null) 
				{
					try 
					{
						frmProgressLoader.Show();
						frmProgressLoader.progressbar1.Maximum = 3;

						frmProgressLoader.Update(1, LanguageHelper.GetMessage("m_loading"));
						frmProgressLoader.progressbar1.Value = 2;
						observationEvent = new ObservationEvent(this, obj);
					} 
					catch(Exception e)
					{
						LogHelper.WriteLog(e.Message);
					}
					finally 
					{
						frmProgressLoader.progressbar1.Value = 3;
						frmProgressLoader.Hide();
					}
				} 
				else 
				{
					observationEvent.LoadGrid(false);
				}
			}
		}
		#endregion

		#region Downloader
		/// <summary>
		/// Download the data from the device to desktop xml.
		/// </summary>
		public void DownloadData(TabControl obj) 
		{
			if(Engine.STUDY_ID == -1) 
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
			if(x != Engine.STUDY_ID) 
			{
				Engine.STUDY_ID = x;

				if(scaleEvent != null) 
				{
					frmProgressLoader.Show();
					frmProgressLoader.progressbar1.Maximum = 2;
					frmProgressLoader.Update(1, "Updating scale's tab...");
					scaleEvent.Init();
					frmProgressLoader.progressbar1.Value = 2;
					frmProgressLoader.Hide();
				}
				if(variateEvent != null) 
				{
					frmProgressLoader.Show();
					frmProgressLoader.progressbar1.Maximum = 2;
					frmProgressLoader.Update(1, "Updating variate's tab...");
					variateEvent.Init();
					frmProgressLoader.progressbar1.Value = 2;
					frmProgressLoader.Hide();
				}
				if(observationEvent != null) 
				{
					frmProgressLoader.Show();
					frmProgressLoader.progressbar1.Maximum = 2;
					frmProgressLoader.Update(1, "Updating observation's tab...");
					observationEvent.Init();
					frmProgressLoader.progressbar1.Value = 2;
					frmProgressLoader.Hide();
				}
			}
		}

		/// <summary>
		/// Gets the study id currently selected.
		/// </summary>
		/// <returns></returns>
		public int GetStudyId() 
		{
			return Engine.STUDY_ID;
		}
		#endregion

		#region Delete Study
		public void DeleteStudy(int x) 
		{
			frmProgressLoader.Show();
			frmProgressLoader.progressbar1.Maximum = 4;
			frmProgressLoader.Update(1, "Deleting scales...");
			
			DataAccess da = new DataAccess();
			string sql = string.Format("DELETE FROM scale WHERE study_id={0} scale_id IN (SELECT scale_id FROM variate WHERE study_id={0})", x);
			da.ExecuteSql(sql);

			frmProgressLoader.Update(2, "Deleting study...");
			sql = String.Format("DELETE FROM study WHERE study_id={0}", x);
			da.ExecuteSql(sql);

			frmProgressLoader.Update(3, "Finalizing database...");
			frmProgressLoader.progressbar1.Value = 4;
			frmProgressLoader.Hide();

			Engine.STUDY_ID = -1;
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
