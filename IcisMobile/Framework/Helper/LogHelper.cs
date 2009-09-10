using System;
using IcisMobile.Framework.Resource;
using System.IO;

namespace IcisMobile.Framework.Helper
{
	/// <summary>
	/// Summary description for LogHelper.
	/// </summary>
	public class LogHelper
	{
		public static void WriteLog(string log)
		{
			StreamWriter writer = null;

			try 
			{
				if(!File.Exists(Settings.TEMP_DIR + Settings.LOG_FILE)) 
				{
					using(writer = File.CreateText(Settings.TEMP_DIR + Settings.LOG_FILE))
					{
						writer.WriteLine("ICIS-Mobile Log");
						writer.WriteLine(">" + DateTime.Now.ToString());
						writer.WriteLine(log);
						writer.Flush();
					}
				} 
				else 
				{
					using(writer = File.AppendText(Settings.TEMP_DIR + Settings.LOG_FILE))
					{
						writer.WriteLine(">" + DateTime.Now.ToString());
						writer.WriteLine(log);
						writer.Flush();
					}
				}
			} 
			catch(Exception e) 
			{
			}
			finally
			{
				if(writer != null) 
				{
					writer.Close();
				}
			}
		}

		public static void WriteLog(int errorcode, string log)
		{
			StreamWriter writer = null;

			try 
			{
				if(!File.Exists(Settings.TEMP_DIR + Settings.LOG_FILE)) 
				{
					using(writer = File.CreateText(Settings.TEMP_DIR + Settings.LOG_FILE))
					{
						writer.WriteLine("ICIS-Mobile Log");
						writer.WriteLine(">" + DateTime.Now.ToString());
						writer.WriteLine(errorcode + " - " + log);
						writer.Flush();
					}
				} 
				else 
				{
					using(writer = File.AppendText(Settings.TEMP_DIR + Settings.LOG_FILE))
					{
						writer.WriteLine(">" + DateTime.Now.ToString());
						writer.WriteLine(errorcode + " - " + log);
						writer.Flush();
					}
				}
			} 
			catch(Exception e) 
			{
			}
			finally
			{
				if(writer != null) 
				{
					writer.Close();
				}
			}
		}
	}
}
