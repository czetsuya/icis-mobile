/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * */

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

namespace IcisMobile.Framework.Helper
{
	/// <summary>
	/// Summary description for MessageHelper.
	/// </summary>
	public class ResourceHelper
	{
		public static void ShowInfo(String s) 
		{
			MessageBox.Show(s, "Notice", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
		}

		public static bool ShowQuestion(String s, String caption) 
		{
			if(DialogResult.Yes == MessageBox.Show(s, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
				return true;
			else
				return false;
		}

		public static Image GetImage(String file) 
		{	
			try 
			{
				return new Bitmap(Settings.TEMP_DIR + file);
			} 
			catch(NullReferenceException e) 
			{
				LogHelper.WriteLog(e.Message);
			}
			return null; //default
		}
	}
}
