/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * */

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
		private ArrayList constraints;

		public Database()
		{
			tables = new ArrayList();
			constraints = new ArrayList();
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

		public void AddConstraint(String s) 
		{
			constraints.Add(s);
		}

		public ArrayList GetContraints() 
		{
			return constraints;
		}

		public String GetConstraint(int x) 
		{
			return (String)constraints[x];
		}
	}
}
