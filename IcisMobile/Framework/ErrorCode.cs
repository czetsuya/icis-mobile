/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * */

using System;

namespace IcisMobile.Framework
{
	/// <summary>
	/// Summary description for ErrorCode.
	/// </summary>
	public class ErrorCode
	{
		public static int XML_INVALID_NODE = 101;
		public static int XML_STUDY_LOAD_FAILED = 102;
		public static int DATABASE_OPEN_DB = 200;
		public static int DATABASE_EXECUTE_SQL = 201;
		public static int DATABASE_EXECUTE_SQL_SCALAR = 202;
		public static int DATABASE_EXECUTE_SQL_ROW = 203;
		public static int DATABASE_EXECUTE_SQL_ARRAY = 204;
		public static int DATABASE_EXECUTE_SQL_DATASET = 205;
		public static int DATABASE_EXECUTE_SQL_DATATABLE = 206;
		public static int DATABASE_EXECUTE_SQL_INSERT = 207;
		public static int RESOURCE_LOADING = 301;
	}
}
