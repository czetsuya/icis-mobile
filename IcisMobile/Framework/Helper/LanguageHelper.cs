using System;
using System.Resources;

namespace IcisMobile.Framework.Helper
{
	/// <summary>
	/// Summary description for Language.
	/// </summary>
	public class LanguageHelper
	{
		public static String GetMessage(String name) 
		{
			String s = "";
			try 
			{
				ResourceManager rm = new ResourceManager("IcisMobile.Resources.Message", System.Reflection.Assembly.GetExecutingAssembly());
				s = rm.GetString(name);
			} 
			catch(MissingManifestResourceException e) 
			{
				LogHelper.WriteLog(Resource.ErrorCode.RESOURCE_LOADING, e.Message);
			}
			return s;
		}

		public static String GetLabel(String name) 
		{
			String s = "";
			try 
			{
				ResourceManager rm = new ResourceManager("IcisMobile.Resources.Label", System.Reflection.Assembly.GetExecutingAssembly());
				s = rm.GetString(name);
			} 
			catch(MissingManifestResourceException e) 
			{
				LogHelper.WriteLog(Resource.ErrorCode.RESOURCE_LOADING, e.Message);
			}
			return s;
		}
	}
}
