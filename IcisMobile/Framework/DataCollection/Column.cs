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

		public Column()
		{
			name = "";
			type = "varchar";
			length = "10";
			isPrimary = "false";
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
	}
}
