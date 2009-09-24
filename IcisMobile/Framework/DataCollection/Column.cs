/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * */

using System;
using Microsoft.VisualBasic;

namespace IcisMobile.Framework.DataCollection
{
	/// <summary>
	/// Summary description for Column.
	/// </summary>
	public class Column
	{
		private String name;
		private String type;
		private String length;
		private String isPrimary;
		private String isNull;

		public Column()
		{
			name = "";
			type = "varchar";
			length = "10";
			isPrimary = "false";
			isNull = "";
		}

		public String NAME 
		{
			get { return name; }
			set { name = value; }
		}

		public String TYPE 
		{
			get { return type; }
			set { type = value; }
		}

		public String LENGTH 
		{
			get { return length; }
			set { length = value; }
		}

		public String PRIMARY 
		{
			get { return isPrimary; }
			set { isPrimary = value; }
		}

		public String ISNULL
		{
			get { return isNull; }
			set { isNull = value; }
		}
	}
}
