using System;
using System.Windows.Forms;
using System.Data;

using IcisMobile.Framework.DataAccessLayer;
using IcisMobile.Framework.Helper;

namespace IcisMobile.Framework.EventHandler
{
	/// <summary>
	/// Summary description for DataEntry.
	/// </summary>
	public class DataEntryEvent
	{
		private ComboBox cbVariates;
		private Engine engine;
		private TabPage page;
		private TextBox tbBarcode;
		private TextBox tbValue;
		private Label lblPname;
		private Label lblStatus;
		private Button btnSave;
		private frmProgress frmLoader = new frmProgress();
		private String levelno;
		private Button btnClear;

		public DataEntryEvent(Engine engine, object obj)
		{
			this.engine = engine;
			page = (TabPage)obj;
			levelno = "";
			
			foreach(Control c1 in page.Controls) 
			{
				if(c1 is ComboBox) 
				{
					cbVariates = (ComboBox)c1;
				} 
				else if(c1 is TextBox) 
				{
					TextBox temp = (TextBox)c1;
					if(temp.Text == "Barcode") 
					{
						tbBarcode = temp;
						tbBarcode.Text = "";
					} 
					else 
					{
						tbValue = temp;
					}
				} 
				else if(c1 is Label) 
				{
					if(((Label)c1).Text == "Status:") 
					{
						lblStatus = (Label)c1;
					} else if(((Label)c1).Text.Length == 0) 
					{
						lblPname = (Label)c1;
					}
				} 
				else if(c1 is Button) 
				{
					if(((Button)c1).Text == "Save") 
					{
						btnSave = (Button)c1;
					} 
					else 
					{
						btnClear = (Button)c1;
					}
				}
			}

			Init();
			
			tbBarcode.LostFocus += new System.EventHandler(tbBarcode_LostFocus);
			btnSave.Click += new System.EventHandler(btnSave_Click);
			btnClear.Click += new System.EventHandler(btnClear_Click);
			tbBarcode.Focus();
		}

		public void Init() 
		{
			try 
			{				
				object x = DataAccess.Instance().QueryScalar(String.Format("SELECT variate_id FROM variate WHERE study_id={0} ORDER BY variate_name", engine.GetStudyId()));				
				
				DataRow row = DataAccess.Instance().QueryRow(String.Format("SELECT variate_name, variate_property, variate_scale, variate_method, variate_datatype FROM variate WHERE study_id={0} AND variate_id={1} ORDER BY variate_name", engine.GetStudyId(), x));
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

		private void btnSave_Click(object sender, EventArgs e)
		{
			if(tbBarcode.Text.Length > 0 && tbValue.Text.Length > 0) 
			{
				frmLoader.progressbar1.Maximum = 3;
				frmLoader.Show();
				frmLoader.Update(1, "Saving...");

				btnSave.Enabled = false;
				String sql = String.Format("UPDATE data_varchar SET data_value={0} WHERE variate_id={1} AND level_no={2}", tbValue.Text, cbVariates.SelectedValue, levelno);
				DataAccess.Instance().ExecuteSql(sql);

				frmLoader.Update(2);

				lblStatus.Text = "Status: Saved";
				btnSave.Enabled = true;
				
				frmLoader.Update(3);
				frmLoader.Hide();
			}
		}

		private void tbBarcode_LostFocus(object sender, EventArgs e)
		{
			if(tbBarcode.Text.Length > 0) 
			{
				String sql = String.Format("SELECT level_no FROM level_varchar WHERE study_id={0} AND level_value={1}", engine.GetStudyId(), tbBarcode.Text);
				object oLevelNo = DataAccess.Instance().QueryScalar(sql);

				if(oLevelNo != null && oLevelNo.ToString().Length > 0) 
				{
					levelno = oLevelNo.ToString();
					sql = String.Format("SELECT data_value FROM data_varchar WHERE variate_id={0} AND level_no={1}", cbVariates.SelectedValue, levelno);
					object obj = DataAccess.Instance().QueryScalar(sql);
					if(obj != null && obj.ToString() != "") 
					{
						tbValue.Text = obj.ToString();			
					} 
					else 
					{
						tbValue.Text = "";
					}

					sql = String.Format("SELECT level_desc FROM level_varchar WHERE level_value={0} AND study_id={1}", tbBarcode.Text, engine.GetStudyId());
					obj = DataAccess.Instance().QueryScalar(sql);
					if(obj != null && obj.ToString() != "")
					{
						lblPname.Text = obj.ToString();
					}
				} 
				else 
				{
					ResourceHelper.ShowInfo("Plant does not exists");
					tbBarcode.Focus();
					tbBarcode.SelectAll();
				}
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			tbBarcode.Text = "";
			tbValue.Text = "";
			lblStatus.Text = "Status:";
			tbBarcode.Focus();
		}
	}
}
