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
			
			Init();
			cbVariates.SelectedIndexChanged += new System.EventHandler(cbVariates_SelectedIndexChanged);
		}

		public void Init() 
		{
			LoadVariates();
			LoadPanel();
		}

		private void LoadVariates() 
		{
			try 
			{	
				DataTable dt = DataAccess.Instance().QueryAsDataTable(String.Format("SELECT scale_id, variate_name FROM variate WHERE study_id={0} ORDER BY variate_name", engine.GetStudyId()));
				cbVariates.ValueMember = "scale_pid";
				cbVariates.DisplayMember = "variate_name";
				cbVariates.DataSource = dt;
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
						
			DataRow dataRow = DataAccess.Instance().QueryRow(String.Format("SELECT scale_type, scale_pid FROM scale WHERE scale_id={0} AND study_id={1}", x, engine.GetStudyId()));
			
			object scale_pid = dataRow.ItemArray[1];
			
			if(dataRow.ItemArray[0].ToString().Equals("C"))
			{ //continuous
				DataTable dt = DataAccess.Instance().QueryAsDataTable(String.Format("SELECT scalecon_start AS Minimum, scalecon_end AS Maximum FROM scalecon WHERE scale_pid={0} ORDER BY scalecon_start", scale_pid));
				grid.DataSource = dt;

				grid.Refresh();
			}
			else 
			{ //discontinuous
				DataTable dt = DataAccess.Instance().QueryAsDataTable(String.Format("SELECT scaledis_value AS Scale, scaledis_desc AS Meaning FROM scaledis WHERE scale_pid={0} ORDER BY scaledis_value", scale_pid));
				grid.DataSource = dt;
				
				grid.Refresh();
			}
		}
	}
}
