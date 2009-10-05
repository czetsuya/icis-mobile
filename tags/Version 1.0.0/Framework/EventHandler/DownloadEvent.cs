using System;
using System.Data;
using System.Collections;

using IcisMobile.Framework.DataCollection;

namespace IcisMobile.Framework.EventHandler
{
	/// <summary>
	/// Summary description for DownloadEvent.
	/// </summary>
	public class DownloadEvent
	{
		private int study_id = -1;
		private const string newLine = "\r\n";

		public DownloadEvent(int x)
		{
            study_id = x;
						
			WriteToFile(PrepareData());
		}

		private void WriteToFile(ArrayList arrValues) 
		{
			DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess();
			object study_name = da.QueryScalar(String.Format("SELECT study_name FROM study WHERE study_id={0}", study_id));

			try 
			{
				System.Text.StringBuilder sb = new System.Text.StringBuilder();
				sb.Append("[czetsuya=" + study_name + "]");
				sb.Append(newLine);
				foreach(Data data in arrValues) 
				{
					sb.Append(String.Format("[{0}]", data.NAME));
					sb.Append(newLine);
					foreach(Data.STRUCTVALUE temp in data.GetData()) 
					{
						if(temp.val.ToString() != "") 
						{
							sb.Append(temp.level +"="+ temp.val);
							sb.Append(newLine);
						}
					}
				}
				Helper.FileHelper.WriteToFile(Helper.LanguageHelper.GetConfig("file_data_name"), sb.ToString());
			} 
			catch(Exception e) 
			{
				Helper.LogHelper.WriteLog(e.Message);
			}
		}

		private ArrayList PrepareData() 
		{
			ArrayList arrValues = new ArrayList();
			if(study_id != -1) 
			{
				Data data;
				DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess();
				
				string sql = String.Format("SELECT variate_id, variate_name FROM variate WHERE study_id={0}", study_id);
                DataTable dt = da.QueryAsDataTable(sql);
				foreach(DataRow row in dt.Rows) 
				{
					data = new Data();
					data.ID = row.ItemArray[0].ToString();
					data.NAME = row.ItemArray[1].ToString();

					sql = String.Format("SELECT level_no, data_value FROM data_varchar WHERE study_id={0} AND variate_id={1} AND data_value IS NOT NULL ORDER BY level_no", study_id, data.ID);
					dt = da.QueryAsDataTable(sql);
					if(dt != null) 
					{
						foreach(DataRow row1 in dt.Rows) 
						{
							data.Add(row1.ItemArray[0], row1.ItemArray[1]);
						}
						arrValues.Add(data);
					}
				}
			}
			return arrValues;
		}
	}
}
