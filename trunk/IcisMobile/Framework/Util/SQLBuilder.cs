using System;
using System.IO;
using System.Text;
using System.Collections;

using IcisMobile.Framework.DataCollection;
using IcisMobile.Framework.DataAccessLayer;
using IcisMobile.Framework.DataCollection.Study;
using IcisMobile.Framework.Util;

namespace IcisMobile.Framework.Helper
{
	/// <summary>
	/// Summary description for SQLHelper.
	/// </summary>
	public class SQLBuilder
	{
		private static frmProgress frmProgressLoader = new frmProgress();
		private static string newLine = " ";

		#region Schema Builder
		public static bool CreateDBFromSchema(Database database) 
		{
			System.Collections.ArrayList queries = new System.Collections.ArrayList();
			try 
			{
				if(File.Exists(Settings.TEMP_DIR + Settings.DATABASE_FILE)) 
				{
					try 
					{
						File.Delete(Settings.TEMP_DIR + Settings.DATABASE_FILE);
					} 
					catch(IOException e)
					{	
						LogHelper.WriteLog(LanguageHelper.GetMessage("file_delete_failed") + ": " + Settings.TEMP_DIR + Settings.DATABASE_FILE + "\r\n" + e.Message);
						return false;
					}
				}
				
				DataAccess.Instance().CreateDatabase();

				StringBuilder sb = new StringBuilder();
				
				frmProgressLoader.Show();
				frmProgressLoader.progressbar1.Maximum = database.TABLECOUNT + database.GetContraints().Count;
				int cnt = 0;
				
				for(int i = 0; i < database.TABLECOUNT; i++) 
				{	
					sb = new StringBuilder();

					Table t = database.GetTableByIndex(i);
					
					//frmProgressLoader.lblMsg.Text = "Creating table: " + t.NAME;
					frmProgressLoader.Update(cnt++, "Creating table: " + t.NAME);

					sb.Append("CREATE TABLE " + t.NAME + " (");
					sb.Append(newLine);

					sb.Append(PrepareColumnCreateScript(t.GetColumnByIndex(0)));
					for(int j = 1; j < t.COLUMNCOUNT; j++) 
					{
						sb.Append("," + newLine + PrepareColumnCreateScript(t.GetColumnByIndex(j)));
					}
					sb.Append(newLine);
					sb.Append(")");
					
					queries.Add(sb.ToString());
				}

				frmProgressLoader.lblMsg.Text = "Updating constraints...";
				foreach(String s in database.GetContraints()) 
				{
					frmProgressLoader.progressbar1.Value = cnt++;
					frmProgressLoader.Refresh();
					queries.Add(s);
				}

				frmProgressLoader.lblMsg.Text = "Inserting data...";
				DataAccess.Instance().Insert(queries);
				frmProgressLoader.progressbar1.Value = cnt++;
				frmProgressLoader.Refresh();
				frmProgressLoader.Hide();
			} 
			catch(Exception e) 
			{
				LogHelper.WriteLog(ErrorCode.DATABASE_OPEN_DB, e.Message);
			}
			return true;
		}	

		private static String PrepareColumnCreateScript(Column c) 
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(c.NAME);
			sb.Append(" ");
			sb.Append(c.TYPE);

			if(	c.TYPE.ToLower().Equals("nvarchar") ||
				c.TYPE.ToLower().Equals("varchar") ||
				c.TYPE.ToLower().Equals("ntext") ) 
			{ //append length
				if(c.LENGTH.Length > 0)
					sb.Append("(" + c.LENGTH + ")");
			}

			if(Convert.ToBoolean(c.PRIMARY)) 
			{
				sb.Append(" IDENTITY(1,1) NOT NULL PRIMARY KEY");
			} 
			else 
			{
				if(c.ISNULL != "") 
				{ //append not null
					if(!Convert.ToBoolean(c.ISNULL)) 
					{
						sb.Append(" NOT NULL");
					}
				}
			}
			return sb.ToString();
		}
		#endregion

		#region Prepare Study, Scales, Factors and Variates
		public static String PrepareStudyScript(Study study) 
		{
			String sql = String.Format("INSERT INTO study (study_name, study_title, study_sdate, study_edate) VALUES ('{0}', '{1}', '{2}', '{3}')", study.NAME, study.TITLE, study.STARTDATE, study.ENDDATE);
			return sql;
		}

		public static ArrayList PrepareScaleScript(ArrayList source) 
		{
			ArrayList queries = new ArrayList();
			foreach(Scale obj in source) 
			{
				String sql = "";
				if(obj.TYPE.ToUpper().Equals("C")) //continuous
				{
					sql = String.Format("INSERT INTO scale (scale_id, scale_name, scale_type) VALUES ({0}, '{1}', '{2}')", obj.ID, obj.NAME, obj.TYPE);
					queries.Add(sql);
					if(obj.VALUE1.Length > 0 && obj.VALUE2.Length > 0)
					{
						sql = String.Format("INSERT INTO scalecon (scale_id, scalecon_start, scalecon_end) VALUES ({0}, {1}, {2})", obj.ID, obj.VALUE1, obj.VALUE2);
						queries.Add(sql);
					}
				} 
				else //discontinuous
				{
					sql = String.Format("INSERT INTO scale (scale_id, scale_name, scale_type) VALUES ({0}, '{1}', '{2}')", obj.ID, obj.NAME, obj.TYPE);
					queries.Add(sql);
					
					//add values
					StringTokenizer st = new StringTokenizer(obj.VALUE3, "\r\n");
					while(st.HasMoreTokens()) 
					{
						string[] words = st.NextToken().Split('|');
						if((words != null) && (words[0] != "" && words[1] != "")) 
						{
							sql = String.Format("INSERT INTO scaledis (scale_id, scaledis_value, scaledis_desc) VALUES ({0}, '{1}', '{2}')", obj.ID, words[0], words[1]);
							queries.Add(sql);
						}
					}
				}
			}
			return queries;
		}

		public static void InsertFactors(ref Study study) 
		{
			for(int i = 0; i < study.GetFactors().Count; i++)
			{
				Factor obj = study.GetFactor(i);
				//String sql = String.Format("INSERT INTO factor (study_id, scale_id, factor_name, factor_property, factor_scale, factor_method, factor_datatype) VALUES ({0}, {1}, '{2}', '{3}', '{4}', '{5}', '{6}')", study.ID, obj.SCALEID, obj.NAME, obj.PROPERTY, obj.SCALEID, obj.METHOD, obj.DATATYPE);
				String sql = String.Format("INSERT INTO factor (study_id, factor_name) VALUES ({0}, '{1}')", study.ID, obj.NAME);
				String id = DataAccess.Instance().Insert(sql);
				study.GetFactor(i).ID = id;
			}
		}

		public static void InsertVariates(ref Study study) 
		{
			for(int i = 0; i < study.GetVariates().Count; i++)
			{
				Variate obj = study.GetVariate(i);
				String sql = String.Format("INSERT INTO variate (study_id, scale_id, variate_name, variate_property, variate_scale, variate_method, variate_datatype) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", study.ID, obj.SCALEID, obj.NAME, obj.PROPERTY, obj.SCALE, obj.METHOD, obj.DATATYPE);
				String id = DataAccess.Instance().Insert(sql);
				study.GetVariate(i).ID = id;
			}
		}
		#endregion

		#region Prepare Study Data
		public static ArrayList _PrepareStudyDataScript(Study study, ArrayList arrTemp) 
		{
			ArrayList arrRet = new ArrayList();
			String sql = "";
			foreach(string s in arrTemp) 
			{
				//length of words is the # of factors in observation sheet
				String[] words = s.Split(',');
				for(int i = 0; i < study.GetFactors().Count; i++) 
				{
					Factor factor = study.GetFactor(i);
					if(factor.DATATYPE.Equals("C")) //string
					{
						sql = String.Format("INSERT INTO level_varchar (study_id, factor_id, level_value) VALUES ({0}, {1}, '{2}')", study.ID, factor.ID, words[i]);
					} 
					else //numeric
					{
						sql = String.Format("INSERT INTO level_int (study_id, factor_id, level_value) VALUES ({0}, {1}, '{2}')", study.ID, factor.ID, words[i]);
					}
					arrRet.Add(sql);
				}
			}

			return arrRet;
		}

		public static ArrayList PrepareStudyDataScript(Study study, ArrayList arrTemp) 
		{
			ArrayList arrRet = new ArrayList();
			String sql = "";
			foreach(string s in arrTemp) 
			{
				//length of words is the # of factors in observation sheet
				String[] words = s.Split('|');
				for(int i = 0; i < study.GetFactors().Count; i++) 
				{
					Factor factor = study.GetFactor(i);
					sql = String.Format("INSERT INTO level_varchar (study_id, factor_id, level_value, level_no) VALUES ({0}, {1}, '{2}', '{3}')", study.ID, factor.ID, words[1], words[0]);
					arrRet.Add(sql);
				}
			}

			return arrRet;
		}
		#endregion
	}
}
