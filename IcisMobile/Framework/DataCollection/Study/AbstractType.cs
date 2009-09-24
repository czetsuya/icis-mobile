/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * @description Abstract data type for factor and variate.
 * */
using System;

namespace IcisMobile.Framework.DataCollection.Study
{
	/// <summary>
	/// Abstract type data type for storing similar data from factors and variates.
	/// </summary>
	public abstract class AbstractType
	{
		private String id;
		private String name;
		private String property;
		private String scale;
		private String method;
		private String datatype;
		private String scaleid;

		public AbstractType()
		{
			name = "";
			property = "";
			scale = "";
			method = "";
			datatype = "";
		}

		public String ID
		{
			set { id = value; }
			get { return id; }
		}

		public String NAME 
		{
			set { name = value; }
			get { return name; }
		}

		public String PROPERTY 
		{
			set { property = value; }
			get { return property; }
		}

		public String SCALE 
		{
			set { scale = value; }
			get { return scale; }
		}

		public String METHOD 
		{
			set { method = value; }
			get { return method; }
		}

		public String DATATYPE 
		{
			set { datatype = value.ToUpper(); }
			get { return datatype; }
		}

		public String SCALEID 
		{
			set { scaleid = value; }
			get { return scaleid; }
		}
	}
}
