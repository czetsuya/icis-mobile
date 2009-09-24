using System;
using System.Collections;

using IcisMobile.Framework.Helper;
using IcisMobile.Framework.DataAccessLayer;
using IcisMobile.Framework;
using IcisMobile.Framework.DataCollection.Study;

namespace IcisMobile.Framework
{
	/// <summary>
	/// Summary description for Engine.
	/// </summary>
	public class Engine
	{
        frmProgress frmProgressLoader = new frmProgress();		
	
		public void InitSchema() 
		{
			if(ResourceHelper.ShowQuestion(LanguageHelper.GetMessage("engine_create_schema"), "")) 
			{
				if(System.IO.File.Exists(Settings.TEMP_DIR + Settings.SCHEMA_FILE)) 
				{
					if(SQLHelper.CreateDBFromSchema(XMLHelper.Instance().readSchema(Settings.TEMP_DIR + Settings.SCHEMA_FILE))) 
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

		public void LoadStudyFromFile() 
		{
			//get the default study file that contains study data
			frmProgressLoader.Show();			
			frmProgressLoader.progressbar1.Maximum = 6;
			
			frmProgressLoader.Update(1, LanguageHelper.GetMessage("m_load_study_1"));

            Study study = XMLHelper.Instance().parseStudy(Settings.TEMP_DIR + LanguageHelper.GetConfig("study_data_file"));
			String study_id = DataAccess.Instance().Insert(SQLHelper.PrepareStudyScript(study));
			study.ID = study_id;
			
			frmProgressLoader.Update(2, LanguageHelper.GetMessage("m_load_study_2"));
			//load scales - FIRST FOREIGN KEY OF table factors
			DataAccess.Instance().Insert(SQLHelper.PrepareScaleScript(study.GetScales()));
			
			frmProgressLoader.Update(3, LanguageHelper.GetMessage("m_load_study_3"));
			///load factors
			SQLHelper.InsertFactors(ref study);
			
			frmProgressLoader.Update(4, LanguageHelper.GetMessage("m_load_study_4"));
			//load variates
			SQLHelper.InsertVariates(ref study);
			
			frmProgressLoader.Update(5, LanguageHelper.GetMessage("m_load_study_5"));
			//get the default file for study observation data, load the data			
			DataAccess.Instance().Insert(SQLHelper.PrepareStudyDataScript(study, FileHelper.ReadFile(Settings.TEMP_DIR + LanguageHelper.GetConfig("study_observation_data_file"))));

			frmProgressLoader.Hide();

			ResourceHelper.ShowInfo(LanguageHelper.GetMessage("study_load_ok"));
		}

		
	}
}
