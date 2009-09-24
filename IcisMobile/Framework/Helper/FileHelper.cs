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
	}
}
