using System;
using System.Collections;

namespace IcisMobile.Framework.DataCollection
{
	/// <summary>
	/// Summary description for Database.
	/// </summary>
	public class Database
	{
		private ArrayList tables;

		public Database()
		{
			tables = new ArrayList();
		}

		public void AddTable(Table t) 
		{
			tables.Add(t);
		}

		public Table GetTableByIndex(int i) 
		{
			if(i < tables.Count) 
				return (Table)tables[i];
			else 
				return null;
		}

		public int TABLECOUNT
		{
			get { return tables.Count; }
		}
	}
}
