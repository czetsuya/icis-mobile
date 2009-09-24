/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * */

using System;

namespace IcisMobile.Framework.DataCollection.Study
{
	/// <summary>
	/// Factor specific data.
	/// </summary>
	public class Factor : AbstractType
	{
		private string row;

		public Factor()
		{
			
		}

		public string ROW 
		{
			set { row = value; }
			get { return row; }
		}
	}
}
