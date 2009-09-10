using System;
using System.Windows.Forms;

namespace IcisMobile.Framework.Helper
{
	/// <summary>
	/// Summary description for MessageHelper.
	/// </summary>
	public class MessageHelper
	{
		public static void ShowInfo(String s) 
		{
			MessageBox.Show(s, "Notice", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
		}
	}
}
