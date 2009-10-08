/**
 * @author edwardpantojalegaspi
 * @since 2009.09.23
 * */

using System;
using System.IO;
using System.Collections;

namespace IcisMobile.Framework.Helper
{
	/// <summary>
	/// Summary description for FileHelper.
	/// </summary>
	public class FileHelper
	{
		public static ArrayList ReadFile(String path) 
		{
			ArrayList arrTemp = new ArrayList();
			using(TextReader reader = new StreamReader(path)) 
			{
				while(reader.Peek() != -1)
					arrTemp.Add(reader.ReadLine());
			}
			return arrTemp;
		}

		public static void WriteToFile(string path, string data) 
		{
			using(TextWriter writer = new StreamWriter(path)) 
			{
                writer.Write(data);
			}
		}

		public static bool isExists(string file) 
		{
			return File.Exists(file);
		}
	}
}
