/**
 * @author edwardpantojalegaspi
 * @since 2009.09.30
 * */

using System;
using System.Collections;

namespace IcisMobile.Framework.DataCollection
{
	/// <summary>
	/// Summary description for Data.
	/// </summary>
	public class Data
	{
		public struct STRUCTVALUE 
		{
			public object level;
			public object val;
		}

		private ArrayList arrData;
		private string variate_name;
		public string variate_id;

		public Data() 
		{
			arrData = new ArrayList();
		}

		public string ID 
		{
			set { variate_id = value; }
			get { return variate_id; }
		}

		public string NAME 
		{
			set { variate_name = value; }
			get { return variate_name; }
		}

		public void Add(object level, object val) 
		{
			STRUCTVALUE temp = new STRUCTVALUE();
			temp.level = level;
			temp.val = val;
			arrData.Add(temp);
		}

		public STRUCTVALUE Get(int x) 
		{
			return (STRUCTVALUE)arrData[x];
		}

		public ArrayList GetData() 
		{
			return arrData;
		}
	}
}
