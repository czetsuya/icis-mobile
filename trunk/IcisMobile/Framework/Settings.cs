/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * */

using System;

namespace IcisMobile.Framework
{
	/// <summary>
	/// Summary description for Settings.
	/// </summary>
	public class Settings
	{
		//public static String TEMP_DIR = @"\Program Files\IcisMobile\";
		public static String TEMP_DIR = "";
		public static String SCHEMA_FILE = "schema.xml";
		public static String LOG_FILE = "log.txt";
		public static String DATABASE_FILE = "icis_mobile.sdf";
		public static String DATABASE_PASSWORD = "q2dm1";
		public static String DATABASE_SOURCE = "";
		public static String STUDY_FILE = "study";
		public static int MAX_RECORD_PER_PAGE = 10;
		public static int CURRENT_PAGE_NO = 0;
		public static int RECORD_COUNT_PLANT = 0;
	}
}
