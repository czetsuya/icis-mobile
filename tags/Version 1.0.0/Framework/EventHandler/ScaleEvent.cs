/**
 * @author edwardpantojalegaspi
 * @since 2009.09.28
 * */

using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;

using IcisMobile.Framework.DataAccessLayer;

namespace IcisMobile.Framework.EventHandler
{
	/// <summary>
	/// Summary description for ScaleEvent.
	/// </summary>
	public class ScaleEvent
	{
		private TabPage page;
		private Engine engine;
		private ComboBox cbVariates;
		private DataGrid grid;

		public ScaleEvent(Engine engine, object obj)
		{
            this.engine = engine;
			page = (TabPage)obj;

			foreach(Control c in page.Controls) 
			{
				if(c is ComboBox) 
				{
					cbVariates = (ComboBox)c;
				} 
				else if(c is DataGrid) 
				{
					grid = (DataGrid)c;
				}
			}

			LoadVariates();

			LoadPanel();

			cbVariates.SelectedIndexChanged += new System.EventHandler(cbVariates_SelectedIndexChanged);
		}

		private void LoadVariates() 
		{
			try 
			{
				DataAccess da = new DataAccess();
				DataSet ds = da.QueryAsDataset(String.Format("SELECT scale_id, variate_name FROM variate WHERE study_id={0} ORDER BY variate_name", engine.GetStudyId()));
				cbVariates.ValueMember = "scale_id";
				cbVariates.DisplayMember = "variate_name";
				cbVariates.DataSource = ds.Tables[0];
				cbVariates.Refresh();
			} 
			catch(ArgumentException e)  { }
		}

		
		private void cbVariates_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadPanel();	
		}

		private void LoadPanel() 
		{
			DataRowView row = (DataRowView)cbVariates.SelectedItem;
			int x = (int)row.Row.ItemArray[0];
			
			DataAccess da = new DataAccess();
			object obj = da.QueryScalar(String.Format("SELECT scale_type FROM scale WHERE scale_id={0}", x));
			
			if(obj.ToString().Equals("C"))
			{ //continuous
				DataSet ds = da.QueryAsDataset(String.Format("SELECT scalecon_start AS Minimum, scalecon_end AS Maximum FROM scalecon WHERE scale_id={0} ORDER BY scalecon_start", x));
				grid.DataSource = ds.Tables[0];

				grid.Refresh();
			}
			else 
			{ //discontinuous
				DataSet ds = da.QueryAsDataset(String.Format("SELECT scaledis_value AS Scale, scaledis_desc AS Meaning FROM scaledis WHERE scale_id={0} ORDER BY scaledis_value", x));
				grid.DataSource = ds.Tables[0];
				
				grid.Refresh();
			}
		}
	}
}
