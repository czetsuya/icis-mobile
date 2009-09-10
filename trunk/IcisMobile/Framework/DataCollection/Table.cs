using System;
using System.Collections;

namespace IcisMobile.Framework.DataCollection
{
	/// <summary>
	/// Summary description for Table.
	/// </summary>
	public class Table
	{
		private ArrayList columns;
		private String name;

		public Table()
		{
			columns = new ArrayList();
		}

		public void AddColumn(Column c) 
		{
			columns.Add(c);
		}

		public Column GetColumnByIndex(int i) 
		{
			if(i < columns.Count)
				return (Column)columns[i];
			else
				return null;
		}

		public int COLUMNCOUNT
		{
			get { return columns.Count; }
		}

		public String NAME 
		{ 
			get { return name; }
			set { name = value; }
		}
	}
}
