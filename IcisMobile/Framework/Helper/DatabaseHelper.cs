using System;
using System.Text;
using System.Data;
using System.IO;
using System.Data.SqlServerCe;
using IcisMobile.Framework.DataCollection;

namespace IcisMobile.Framework.Helper
{
	/// <summary>
	/// Summary description for DatabaseHelper.
	/// </summary>
	public class DatabaseHelper
	{
		private static string newLine = " ";
		public static void CreateDBFromSchema(Database database) 
		{
			try 
			{
				if(File.Exists(Resource.Settings.TEMP_DIR + Resource.Settings.DATABASE_FILE)) 
				{
					try 
					{
						File.Delete(Resource.Settings.TEMP_DIR + Resource.Settings.DATABASE_FILE);
					} 
					catch(IOException e)
					{
						MessageHelper.ShowInfo("");
						LogHelper.WriteLog(LanguageHelper.GetMessage("file_delete_failed") + ": " + Resource.Settings.TEMP_DIR + Resource.Settings.DATABASE_FILE);
					}
				}
				
				SqlCeEngine engine = new SqlCeEngine(GetConnection());
				engine.CreateDatabase();

				StringBuilder sb = new StringBuilder();

				for(int i = 0; i < database.TABLECOUNT; i++) 
				{
					Table t = database.GetTableByIndex(i);
					
					sb.Append(newLine);
					sb.Append("CREATE TABLE " + t.NAME + " (");
					sb.Append(newLine);

					sb.Append(PrepareColumnCreateScript(t.GetColumnByIndex(0)));
					for(int j = 1; j < t.COLUMNCOUNT; j++) 
					{
						sb.Append("," + newLine + PrepareColumnCreateScript(t.GetColumnByIndex(j)));
					}
					sb.Append(newLine);
					sb.Append(");");
				}

				ExecuteScript(sb.ToString());
			} 
			catch(Exception e) 
			{
				LogHelper.WriteLog(Resource.ErrorCode.DATABASE_OPEN_DB, e.Message);
			}
		}

		public static void ExecuteScript(String sql) 
		{
			SqlCeConnection conn = null;
			try 
			{
				conn = new SqlCeConnection(GetConnection());
				conn.Open();

				SqlCeCommand cmd = conn.CreateCommand();
				cmd.CommandText = sql;
				cmd.ExecuteNonQuery();
			} 
			catch(Exception e) 
			{
				LogHelper.WriteLog(Resource.ErrorCode.DATABASE_EXECUTE_SQL, e.Message);
			}
			finally {
				if(conn != null) 
				{
					if(conn.State == ConnectionState.Open) 
					{
						conn.Close();
						conn.Dispose();
					}
				}
			}
		}

		private static String PrepareColumnCreateScript(Column c) 
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(c.NAME);
			sb.Append(" ");
			sb.Append(c.TYPE);			
			
			if(c.TYPE.ToLower().Equals("varchar")) 
			{
				sb.Append("(" + c.LENGTH + ")");
			}

			if(Convert.ToBoolean(c.PRIMARY)) 
			{
				sb.Append(" IDENTITY(1,1) PRIMARY KEY");
			}
			return sb.ToString();
		}

		public static String GetConnection() 
		{	
			return "Data Source=" + Resource.Settings.TEMP_DIR + Resource.Settings.DATABASE_FILE;
		}
	}
}
