/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * */

using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Data.SqlTypes;
using System.Collections;

using IcisMobile.Framework.Helper;

namespace IcisMobile.Framework.DataAccessLayer
{
	/// <summary>
	/// Summary description for DatabaseHelper.
	/// </summary>
	public class DataAccess
	{
		private static DataAccess instance = null;
		private SqlCeConnection conn = null;

		public DataAccess() 
		{
			try 
			{
				conn = new SqlCeConnection(GetConnection());
			}
			catch(Exception e) 
			{
				LogHelper.WriteLog(e.Message);
			}
		}
		
		public static DataAccess Instance() 
		{
			if(instance == null) 
			{
				instance = new DataAccess();
			}
			return instance;
		}

		public void CreateDatabase() 
		{
			SqlCeEngine engine = new SqlCeEngine(GetConnection());
			engine.CreateDatabase();
		}

		#region Query
		public object QueryScalar(string sql) 
		{
			object obj = null;
			DataSet ds = new DataSet();
			try 
			{
				conn.Open();
				SqlCeCommand cmd = conn.CreateCommand();
				cmd.CommandText = sql;
				SqlCeDataReader reader = cmd.ExecuteReader();

				while(reader.Read()) 
				{
					obj = reader[0];
					break;
				}
			} 
			catch(SqlCeException e) 
			{
				LogHelper.WriteLog(ErrorCode.DATABASE_EXECUTE_SQL_SCALAR, e.Message);
			} 
			finally 
			{
				conn.Close();
			}
			return obj;
		}

		public DataRow QueryRow(string sql) 
		{
			DataRow row = null;
			DataSet ds = new DataSet();
			try 
			{
				conn.Open();
				SqlCeDataAdapter da = new SqlCeDataAdapter(sql, conn);
				da.Fill(ds);
				if(ds.Tables[0].Rows.Count > 0)
					row = ds.Tables[0].Rows[0];
								
			} 
			catch(IndexOutOfRangeException e1) 
			{
				LogHelper.WriteLog(ErrorCode.DATABASE_EXECUTE_SQL_ROW, e1.Message);
			}
			catch(SqlCeException e) 
			{
				LogHelper.WriteLog(ErrorCode.DATABASE_EXECUTE_SQL_ROW, e.Message);
			} 
			finally 
			{
				conn.Close();
			}
			return row;
		}

		public ArrayList QueryAsArray(string sql) 
		{
			ArrayList arrTemp = new ArrayList();
			DataSet ds = new DataSet();
			try 
			{
				conn.Open();
				SqlCeCommand cmd = conn.CreateCommand();
				cmd.CommandText = sql;
				SqlCeDataReader reader = cmd.ExecuteReader();

				while(reader.Read()) 
				{
					arrTemp.Add(reader[0].ToString());
				}
			} 
			catch(SqlCeException e) 
			{
				LogHelper.WriteLog(ErrorCode.DATABASE_EXECUTE_SQL_ARRAY, e.Message);
			} 
			finally 
			{
				conn.Close();
			}
			return arrTemp;
		}

		public DataSet QueryAsDataset(string sql) 
		{
			DataSet ds = new DataSet();
			try 
			{
				conn.Open();
				SqlCeDataAdapter da = new SqlCeDataAdapter(sql, conn);
				da.Fill(ds);
			} 
			catch(SqlCeException e) 
			{
				LogHelper.WriteLog(ErrorCode.DATABASE_EXECUTE_SQL_DATASET, e.Message);
			} 
			finally 
			{
				conn.Close();
			}
			return ds;
		}

		public DataTable QueryAsDataTable(string sql) 
		{
			DataTable dt = new DataTable();
			try 
			{
				conn.Open();
				SqlCeDataAdapter da = new SqlCeDataAdapter(sql, conn);
				da.Fill(dt);
			} 
			catch(SqlCeException e) 
			{
				LogHelper.WriteLog(ErrorCode.DATABASE_EXECUTE_SQL_DATATABLE, e.Message);
			} 
			finally 
			{
				conn.Close();
			}
			dt.TableName = "czetsuya";
			return dt;
		}

		public void ExecuteSql(string sql) 
		{
			try 
			{
				conn.Open();
				SqlCeCommand cmd = conn.CreateCommand();
				cmd.CommandText = sql;
				cmd.ExecuteNonQuery();
			} 
			catch(SqlCeException e) 
			{
				LogHelper.WriteLog(ErrorCode.DATABASE_EXECUTE_SQL_DATASET, e.Message);
			} 
			finally 
			{
				conn.Close();
			}
		}
		#endregion
	
		#region Insert Overloads
		public void Insert(System.Collections.ArrayList queries) 
		{
			SqlCeTransaction transaction = null;
			try 
			{	
				conn.Open();
				SqlCeCommand cmd = conn.CreateCommand();
				transaction = conn.BeginTransaction();

				cmd.Connection = conn;
				cmd.Transaction = transaction;

				foreach(String sql in queries) 
				{
					cmd.CommandText = sql;
					cmd.ExecuteNonQuery();
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

		public String Insert(String sql) 
		{
			String id = "-1";
			try 
			{
				conn.Open();

				SqlCeCommand cmd = conn.CreateCommand();
				cmd.CommandText = sql;
				cmd.ExecuteNonQuery();

				cmd.CommandText = "SELECT @@IDENTITY";
				SqlDecimal x = (SqlDecimal)cmd.ExecuteScalar();
				id = x.ToString();
			} 
			catch(SqlCeException e) 
			{
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
			return id;
		}
		#endregion

		public void Update(String sql) 
		{
			try 
			{
				conn.Open();
				SqlCeCommand cmd = conn.CreateCommand();
				cmd.CommandText = sql;
				cmd.ExecuteNonQuery();
			} 
			catch(SqlCeException e) 
			{
				LogHelper.WriteLog(ErrorCode.DATABASE_EXECUTE_SQL, e.Message);
			} 
			finally 
			{
				conn.Close();
			}
		}
		
		public void Dispose() 
		{
			if(conn != null) 
			{
				if(conn.State == ConnectionState.Open) 
				{
					conn.Close();
					conn.Dispose();
				}
			}
		}

		#region Connection String
		/// <summary>
		/// Gets the connection string from configuration file.
		/// </summary>
		/// <returns>sqlce connection string</returns>
		public String GetConnection() 
		{	
			return "Data Source=" + Settings.TEMP_DIR + Settings.DATABASE_FILE;
		}
		#endregion
	}
}
