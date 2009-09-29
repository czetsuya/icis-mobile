using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlServerCe;

using IcisMobile.Framework.DataAccessLayer;

namespace IcisMobile.Framework.EventHandler
{
	/// <summary>
	/// Summary description for VariateEvent.
	/// </summary>
	public class ObservationEvent
	{
		private frmProgress frmLoader = new frmProgress();
		private Engine engine;
		private TabPage page;
		private DataGrid grid;
		private ComboBox cbVariates;
		private ComboBox cbValues;
		private TextBox tbValue;
		private TrackBar trackValue;
		private Label lblFactorName;
		private DataAccess da;
		private object variate_id;
		private object scale_id;
		private Button btnSave;

		public ObservationEvent(Engine engine, object obj) 
		{
			this.engine = engine;
			page = (TabPage)obj;
			da = new DataAccess();
			
			foreach(Control c in page.Controls) 
			{
				if(c is ComboBox) 
				{
					ComboBox cb = (ComboBox)c;
					if(cb.ValueMember.Equals("scale_id")) 
					{
						cbValues = cb;
					} 
					else 
					{
						cbVariates = cb;
					}
				} 
				else if(c is DataGrid) 
				{
					grid = (DataGrid)c;
				} 
				else if(c is Label) 
				{
					lblFactorName = (Label)c;
				} 
				else if(c is TextBox) 
				{
					tbValue = (TextBox)c;
				} 
				else if(c is TrackBar) 
				{
					trackValue = (TrackBar)c;
				} 
				else if(c is Button) 
				{
					btnSave = (Button)c;
				}
			}

			SetFactorname();

			Init();
	
			LoadVariates();

			LoadGrid();

			cbVariates.SelectedIndexChanged += new System.EventHandler(cbVariates_SelectedIndexChanged);
			btnSave.Click += new System.EventHandler(btnSave_Click);
			trackValue.ValueChanged += new System.EventHandler(trackValue_ValueChanged);
			cbValues.SelectedIndexChanged += new System.EventHandler(cbValues_SelectedIndexChanged);
		}

		private void Init() 
		{
			DataRow row = da.QueryRow(String.Format("SELECT variate_id FROM variate WHERE study_id={0} ORDER BY variate_name", engine.GetStudyId()));
			variate_id = row[0];
		}

		private void ShowInputValues() 
		{
			DataRow row = da.QueryRow(String.Format("SELECT a.scale_id, b.scale_type FROM variate a LEFT JOIN scale b ON a.scale_id=b.scale_id WHERE a.study_id={0} AND a.variate_id={1}", engine.GetStudyId(), variate_id));

			scale_id = row[0];
			if(row == null || row[1].ToString().Equals("D")) 
			{ //discontinuous
				try 
				{
					ShowDiscontinuous();
				} 
				catch(NullReferenceException e) 
				{
					ShowTextboxOnly();
				}
			} 
			else 
			{ //continuous
				try 
				{
					ShowContinuous();
				} 
				catch(NullReferenceException e) 
				{
					ShowTextboxOnly();
				}
			}
		}

		private void ShowTextboxOnly() 
		{
			cbValues.Visible = false;
			trackValue.Visible = false;
		}
		
		private void ShowDiscontinuous() 
		{
			cbValues.Visible = true;
			trackValue.Visible = false;
			DataTable dt = da.QueryAsDataTable(String.Format("SELECT scaledis_value, scaledis_desc FROM scaledis WHERE scale_id={0}", scale_id));
			cbValues.DataSource = dt;
			cbValues.ValueMember = "scaledis_value";
			cbValues.DisplayMember = "scaledis_desc";
			cbValues.Refresh();
		}

		private void ShowContinuous() 
		{
			DataRow row = da.QueryRow(String.Format("SELECT scalecon_start, scalecon_end FROM scalecon WHERE scale_id={0}", scale_id));
			cbValues.Visible = false;
			trackValue.Visible = true;
			trackValue.Minimum = Convert.ToInt16(row[0].ToString());
			trackValue.Maximum = Convert.ToInt16(row[1].ToString());
		}

		private void SetFactorname() 
		{
			object obj = da.QueryScalar(String.Format("SELECT factor_name FROM factor WHERE study_id={0}", engine.GetStudyId()));
			lblFactorName.Text = obj.ToString();
		}

		private void LoadVariates() 
		{
			try 
			{
				DataTable dt = da.QueryAsDataTable(String.Format("SELECT variate_id, variate_name FROM variate WHERE study_id={0} ORDER BY variate_name", engine.GetStudyId()));
				cbVariates.ValueMember = "variate_id";
				cbVariates.DisplayMember = "variate_name";
				cbVariates.DataSource = dt;
				cbVariates.Refresh();
			} 
			catch(ArgumentException e)  { }
		}

		private void LoadGrid()
		{
			try 
			{
				string sql = String.Format("SELECT b.level_no AS ID, a.level_value AS Factor, b.data_value AS Data FROM level_varchar a INNER JOIN data_varchar b ON a.level_no=b.level_no WHERE b.study_id={0} AND b.variate_id={1}", engine.GetStudyId(), variate_id);
				DataTable dt = da.QueryAsDataTable(sql);
				grid.DataSource = dt;
				grid.Refresh();
			} 
			catch(SqlCeException e) 
			{
			}
		}

		private void cbVariates_SelectedIndexChanged(object sender, EventArgs e)
		{
			DataRowView row = (DataRowView)cbVariates.SelectedItem;
			variate_id = (int)row.Row.ItemArray[0];
			ShowInputValues();
			LoadGrid();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			DataTable dt = (DataTable)grid.DataSource;
			string level_no = dt.Rows[grid.CurrentRowIndex].ItemArray[0].ToString();
			String sql = String.Format("UPDATE data_varchar SET data_value='{0}' WHERE study_id={1} AND variate_id={2} AND level_no={3}", tbValue.Text, engine.GetStudyId(), variate_id, level_no);
			da.Update(sql);
			LoadGrid();
		}

		private void trackValue_ValueChanged(object sender, EventArgs e)
		{
			tbValue.Text = trackValue.Value.ToString();
		}

		private void cbValues_SelectedIndexChanged(object sender, EventArgs e)
		{
			DataRowView row = (DataRowView)cbValues.SelectedItem;
			tbValue.Text = row.Row.ItemArray[0].ToString();
			tbValue.Update();
		}
	}
}
