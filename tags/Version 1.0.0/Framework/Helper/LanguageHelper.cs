/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * */

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
				ResourceManager rm = new ResourceManager("IcisMobile.Resources.message", System.Reflection.Assembly.GetExecutingAssembly());
				s = rm.GetString(name);
			} 
			catch(MissingManifestResourceException e) 
			{
				LogHelper.WriteLog(ErrorCode.RESOURCE_LOADING, e.Message);
			}
			return s;
		}

		public static String GetConfig(String name) 
		{
			String s = "";
			try 
			{
				ResourceManager rm = new ResourceManager("IcisMobile.Resources.config", System.Reflection.Assembly.GetExecutingAssembly());
				s = rm.GetString(name);
			} 
			catch(MissingManifestResourceException e) 
			{
				LogHelper.WriteLog(ErrorCode.RESOURCE_LOADING, e.Message);
			}
			return s;
		}
	}
}
