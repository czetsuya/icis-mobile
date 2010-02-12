using System;
using System.Collections;
using System.IO;
using System.Data.SqlServerCe;
using System.Data.SqlTypes;
using System.Data;

using IcisMobile.Framework.DataCollection.Study;
using IcisMobile.Framework.Helper;

namespace IcisMobile.Framework.Util
{
	/// <summary>
	/// Summary description for OptimizedDataHandler.
	/// </summary>
	public class OptimizedDataHandler
	{
		public static void InsertLevel(String path, frmProgress frmProgressLoader, Study study) 
		{
			String levelQueryTemplate = "INSERT INTO level_varchar (study_id, factor_id, level_value, level_no, level_desc) VALUES ({0}, {1}, '{2}', '{3}', '{4}')";
			String dataQueryTemplate = "INSERT INTO data_varchar (study_id, variate_id, level_no) VALUES ({0}, {1}, {2})";
	
			using(TextReader reader = new StreamReader(path)) 
			{
				SqlCeConnection conn = null;
				SqlCeTransaction transaction = null;			

				try 
				{
					conn = new SqlCeConnection(DataAccessLayer.DataAccess.GetConnection());
					conn.Open();
					SqlCeCommand cmd = conn.CreateCommand();
					transaction = conn.BeginTransaction();

					cmd.Connection = conn;
					cmd.Transaction = transaction;

					while(reader.Peek() != -1) 
					{
						String s = reader.ReadLine();
						//length of words is the # of factors in observation sheet
						String[] words = s.Split('|');
						String[] temp = words[1].Split(new char[] {'-','>'});
						String barCode = temp[0];
						String plantDesc = temp[2];
						//for(int i = 0; i < study.GetFactors().Count; i++) 

						frmProgressLoader.Update("Inserting record: " + words[0]);

						foreach(Factor factor in study.GetFactors())
						{
							//Factor factor = study.GetFactor(i);
							cmd.CommandText = String.Format(levelQueryTemplate, study.ID, factor.ID, barCode, words[0], plantDesc);
							cmd.ExecuteNonQuery();
						}

						foreach(Variate variate in study.GetVariates()) 
						{
							cmd.CommandText = String.Format(dataQueryTemplate, study.ID, variate.ID, words[0]);
							cmd.ExecuteNonQuery();
						}
					}

					transaction.Commit();
				} 
				catch(SqlCeException e) 
				{
					if(transaction != null) 
						transaction.Rollback();
					LogHelper.WriteLog(ErrorCode.DATABASE_EXECUTE_SQL_INSERT, e.Message);
				}
				finally 
				{
					if(conn != null) 
					{
						if(conn.State == ConnectionState.Open) 
						{
							conn.Close();
						}
					}
				}
			}
		}
	}
}
