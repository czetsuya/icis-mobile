/**
 * @author edwardpantojalegaspi
 * @since 2009.09.28
 * */

using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlServerCe;
using System.ComponentModel;

using IcisMobile.Framework.DataAccessLayer;

namespace IcisMobile.Framework.EventHandler
{
	/// <summary>
	/// Summary description for VariateEvent.
	/// </summary>
	public class VariateEvent
	{
		private frmProgress frmLoader = new frmProgress();
		private Engine engine;
		private TabPage page;
		private ComboBox cbVariates;
		private Label lblName;
		private Label lblProperty;
		private Label lblScale;
		private Label lblMethod;
		private Label lblDatatype;		

		public VariateEvent(Engine engine, object obj) 
		{
			this.engine = engine;
			page = (TabPage)obj;
			
			foreach(Control c1 in page.Controls) 
			{
				if(c1 is ComboBox) 
				{
					cbVariates = (ComboBox)c1;
				}
				else if(c1 is Panel) 
				{
					foreach(Control c in ((Panel)c1).Controls) 
					{ //search for labels
						Label lblTemp = (Label)c;
						if(lblTemp.Text.Equals("Name:")) 
						{
							lblName = lblTemp;
						} 
						else if(lblTemp.Text.Equals("Property:")) 
						{
							lblProperty = lblTemp;
						}
						else if(lblTemp.Text.Equals("Scale:")) 
						{
							lblScale = lblTemp;
						}
						else if(lblTemp.Text.Equals("Method:")) 
						{
							lblMethod = lblTemp;
						}
						else if(lblTemp.Text.Equals("Data Type:")) 
						{
							lblDatatype = lblTemp;
						}
					}
				}
			}
			
			Init();
			cbVariates.SelectedIndexChanged += new System.EventHandler(cbVariates_SelectedIndexChanged);
		}

		public void Init() 
		{
			try 
			{				
				object x = DataAccess.Instance().QueryScalar(String.Format("SELECT variate_id FROM variate WHERE study_id={0} ORDER BY variate_name", engine.GetStudyId()));				
				
				DataRow row = DataAccess.Instance().QueryRow(String.Format("SELECT variate_name, variate_property, variate_scale, variate_method, variate_datatype FROM variate WHERE study_id={0} AND variate_id={1} ORDER BY variate_name", engine.GetStudyId(), x));
				UpdateLabel(row);
			} 
			catch(NullReferenceException e) 
			{ 
				Framework.Helper.LogHelper.WriteLog(e.Message);
			}

			LoadVariates();
		}

		private void LoadVariates() 
		{
			try 
			{
				DataTable dt = DataAccess.Instance().QueryAsDataTable(String.Format("SELECT variate_id, variate_name FROM variate WHERE study_id={0} ORDER BY variate_name", engine.GetStudyId()));
				cbVariates.ValueMember = "variate_id";
				cbVariates.DisplayMember = "variate_name";
				cbVariates.DataSource = dt;
				cbVariates.Update();
			} 
			catch(Exception e)  { } 
		}

		private void cbVariates_SelectedIndexChanged(object sender, EventArgs e)
		{
            Reload();
		}

		private void Reload() 
		{
			try 
			{
				DataRowView rv = (DataRowView)cbVariates.SelectedItem;
				int x = (int)rv.Row.ItemArray[0];
				
				DataRow row = DataAccess.Instance().QueryRow(String.Format("SELECT variate_name, variate_property, variate_scale, variate_method, variate_datatype FROM variate WHERE study_id={0} AND variate_id={1} ORDER BY variate_name", engine.GetStudyId(), x));
				UpdateLabel(row);
				
			} 
			catch(NullReferenceException e) 
			{ 
				Framework.Helper.LogHelper.WriteLog("Variate: " + e.Message);
			}
		}

		private void UpdateLabel(DataRow row) 
		{
			lblName.Text = "Name: " + " " + row["variate_name"];
			lblProperty.Text = "Property: " + " " + row["variate_property"];
			lblScale.Text = "Scale: " + " " + row["variate_scale"];
			lblMethod.Text = "Method: " + " " + row["variate_method"];
			lblDatatype.Text = "Data Type: " + " " + row["variate_datatype"];
		}
	}
}
