/**
 * @author edwardpantojalegaspi
 * @since 2009.09.28
 * */

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
		#region Members
		private frmProgress frmLoader = new frmProgress();
		private Engine engine;
		private TabPage page;
		private DataGrid grid;
		/// <summary>
		/// Variates or Plants list.
		/// </summary>
		private ComboBox cbVariates;
		/// <summary>
		/// List of discrete values.
		/// </summary>
		private ComboBox cbValues;
		private TextBox tbValue;
		private TrackBar trackValue;
		private Label lblFactorName;
		private DataAccess da;
		private object variate_factor_id;
		private object scale_pid;
		private object variate_id;
		private Button btnSave;
		private RadioButton rbtnP;
		private RadioButton rbtnV;
		private bool isPlant;
		private bool isContinuous;
		private Timer timerBtn;
		private Button btnList;
		private ComboBox cbPaging;
		private String selectedVariate;
		private System.EventHandler eventSelectedIndexChanged;
		#endregion

		#region Constructors
		public ObservationEvent(Engine engine, object obj) 
		{
			this.engine = engine;
			page = (TabPage)obj;
			da = new DataAccess();
			
			//find our controls
			foreach(Control c in page.Controls) 
			{
				if(c is ComboBox) 
				{
					ComboBox cb = (ComboBox)c;
					if(cb.ValueMember.Equals("scale_id")) 
					{
						cbValues = cb;
					} 
					else if(cb.ValueMember.Equals("variate_id")) 
					{
						cbVariates = cb;
					} 
					else 
					{
						cbPaging = cb;
					}
				} 
				else if(c is DataGrid) 
				{
					grid = (DataGrid)c;
					grid.SelectionBackColor = System.Drawing.Color.MistyRose;
					grid.SelectionForeColor = System.Drawing.Color.Black;
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
					Button b = (Button)c;
					if(b.Text.Equals("Save")) 
					{
						btnSave = b;
					} 
					else 
					{
						btnList = b;
					}
				} 
				else if(c is RadioButton) 
				{
					RadioButton r = (RadioButton)c;
					if(r.Text.Equals("P")) 
					{
						rbtnP = r;
					} 
					else 
					{
						rbtnV = r;
					}
				}
			}
			
			InitPaging();

			Init();

			eventSelectedIndexChanged = new System.EventHandler(cbVariates_SelectedIndexChanged);
			cbVariates.SelectedIndexChanged += eventSelectedIndexChanged;
			
			btnSave.Click += new System.EventHandler(btnSave_Click);
			trackValue.ValueChanged += new System.EventHandler(trackValue_ValueChanged);			
			cbValues.SelectedIndexChanged += new System.EventHandler(cbValues_SelectedIndexChanged);
			grid.CurrentCellChanged += new System.EventHandler(grid_CurrentCellChanged);
			grid.KeyPress += new KeyPressEventHandler(grid_KeyPress);
			rbtnP.Click += new System.EventHandler(rbtnP_Click);
			rbtnV.Click += new System.EventHandler(rbtnV_Click);
			tbValue.KeyPress += new KeyPressEventHandler(tbValue_KeyPress);
			btnList.Click += new System.EventHandler(btnList_Click);
			cbPaging.SelectedIndexChanged += new System.EventHandler(cbPaging_SelectedIndexChanged);
			
			timerBtn = new Timer();
			timerBtn.Interval = 300;
			timerBtn.Enabled = false;
			timerBtn.Tick += new System.EventHandler(timerBtn_Tick);
		}

		private void InitPaging() 
		{
			Settings.MAX_RECORD_PER_PAGE = Convert.ToInt16(Framework.Helper.LanguageHelper.GetConfig("max_plant_list"));
			String sql = "SELECT level_no, level_value FROM level_varchar WHERE study_id=" + engine.GetStudyId();
			DataAccess da = new Framework.DataAccessLayer.DataAccess();
			
			DataTable dt = da.QueryAsDataTable(sql);

			if(Settings.MAX_RECORD_PER_PAGE < dt.Rows.Count) 
			{
				Settings.RECORD_COUNT_PLANT = (int)Math.Floor(dt.Rows.Count / Settings.MAX_RECORD_PER_PAGE);
			} 
			else 
			{
				Settings.RECORD_COUNT_PLANT = 0;
			}

			cbPaging.Items.Clear();
			for(int i = 0; i <= Settings.RECORD_COUNT_PLANT; i++) 
			{				
				cbPaging.Items.Add(i);
			}
		}

		#endregion
		#region Init
		public void Init() 
		{
			SetFactorname();
			if(isPlant) 
			{
				variate_factor_id = da.QueryScalar(String.Format("SELECT l.level_no FROM factor f INNER JOIN level_varchar l ON f.factor_id=l.factor_id WHERE f.study_id={0} ORDER BY l.level_value", engine.GetStudyId()));
				variate_id = da.QueryScalar(String.Format("SELECT v.variate_id, v.variate_name, d.data_value FROM variate v INNER JOIN data_varchar d ON v.variate_id=d.variate_id WHERE v.study_id={0} AND d.level_no={1}", engine.GetStudyId(), variate_factor_id));
			} 
			else 
			{
				variate_factor_id = da.QueryScalar(String.Format("SELECT variate_id FROM variate WHERE study_id={0} ORDER BY variate_name", engine.GetStudyId()));
			}
			//RadioButton b = new RadioButton();
			//LoadVariates();
			//LoadGrid();
			//ShowInputValues();
			frmLoader.Update(2, Helper.LanguageHelper.GetMessage("m_obs_load_variates"));
			LoadVariates();
			frmLoader.Update(3, Helper.LanguageHelper.GetMessage("m_obs_load_datagrid"));
			LoadGrid(true);
			frmLoader.Update(4, Helper.LanguageHelper.GetMessage("m_obs_update_values"));
			ShowInputValues();
			frmLoader.Update(5, Helper.LanguageHelper.GetMessage("m_obs_finalize_screen"));
			frmLoader.Hide();
			
			tbValue.Text = "";
			grid.Select(0);

			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		private void SetFactorname() 
		{
			object obj = da.QueryScalar(String.Format("SELECT factor_name FROM factor WHERE study_id={0}", engine.GetStudyId()));
			lblFactorName.Text = obj.ToString();
		}
		#endregion
		#region Show Input and Control for Values
		private void ShowInputValues() 
		{
			DataRow row = null;
			if(isPlant) 
			{
				row = da.QueryRow(String.Format("SELECT b.scale_pid, b.scale_type FROM variate a LEFT JOIN scale b ON a.scale_id=b.scale_id WHERE a.study_id={0} AND a.variate_id={1}", engine.GetStudyId(), variate_id));
			} 
			else 
			{
				row = da.QueryRow(String.Format("SELECT b.scale_pid, b.scale_type FROM variate a LEFT JOIN scale b ON a.scale_id=b.scale_id WHERE a.study_id={0} AND a.variate_id={1}", engine.GetStudyId(), variate_factor_id));			
			}
			scale_pid = row[0];
			if(row == null || row[1].ToString().Equals("D")) 
			{ //discontinuous
				isContinuous = false;
				tbValue.ReadOnly = true;
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
				isContinuous = true;
				tbValue.ReadOnly = false;
				try 
				{
					ShowContinuous();
				} 
				catch(NullReferenceException e) 
				{
					isContinuous = false;
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
			DataTable dt = da.QueryAsDataTable(String.Format("SELECT scaledis_value, scaledis_desc FROM scaledis WHERE scale_pid={0}", scale_pid));
			cbValues.DataSource = dt;
			cbValues.ValueMember = "scaledis_value";
			cbValues.DisplayMember = "scaledis_desc";
			cbValues.Refresh();
		}

		private void ShowContinuous() 
		{
			DataRow row = da.QueryRow(String.Format("SELECT scalecon_start, scalecon_end FROM scalecon WHERE scale_pid={0}", scale_pid));
			cbValues.Visible = false;
			trackValue.Visible = true;
			trackValue.Minimum = Convert.ToInt16(row[0].ToString());
			trackValue.Maximum = Convert.ToInt16(row[1].ToString());
		}

		#endregion
		#region Load Variates and Grid
		private void LoadVariates() 
		{
			try 
			{
				string sql = "";
				if(isPlant) 
				{
					//sql = String.Format("SELECT l.level_no, l.level_value FROM factor f INNER JOIN level_varchar l ON f.factor_id=l.factor_id WHERE f.study_id={0} ORDER BY l.level_value", engine.GetStudyId());
					sql = "SELECT level_no, level_value FROM level_varchar WHERE study_id=" + engine.GetStudyId();

					int x = 0;					
					x = Settings.CURRENT_PAGE_NO * Settings.MAX_RECORD_PER_PAGE;

					DataSet ds = da.QueryAsDataset(sql, x, Settings.MAX_RECORD_PER_PAGE);

					cbVariates.SelectedIndexChanged -= eventSelectedIndexChanged;
					cbVariates.DataSource = ds.Tables[0];
					cbVariates.ValueMember = "level_no";
					cbVariates.DisplayMember = "level_value";
					cbVariates.SelectedIndexChanged += eventSelectedIndexChanged;
				} 
				else 
				{
					sql = String.Format("SELECT variate_id, variate_name FROM variate WHERE study_id={0} ORDER BY variate_name", engine.GetStudyId());
					DataTable dt = da.QueryAsDataTable(sql);
					
					selectedVariate = dt.Rows[0].ItemArray[1].ToString();
					
					cbVariates.SelectedIndexChanged -= eventSelectedIndexChanged;
					cbVariates.DataSource = dt;
					cbVariates.ValueMember = "variate_id";
					cbVariates.DisplayMember = "variate_name";
					cbVariates.SelectedIndexChanged += eventSelectedIndexChanged;
				}
				cbVariates.Refresh();
			} 
			catch(ArgumentException e)  { }
		}

		private void LoadGrid(bool updateStyle)
		{
			try 
			{
				string sql = "";
				if(isPlant) 
				{
					sql = String.Format("SELECT v.variate_id AS ID, v.variate_name AS col1, d.data_value AS col2 FROM variate v INNER JOIN data_varchar d ON v.variate_id=d.variate_id WHERE d.study_id={0} AND v.study_id={0} AND d.level_no={1}", engine.GetStudyId(), variate_factor_id);
					if(updateStyle) 
					{
						UpdateGridStyle("col1", "col2", "Trait", "Value");
					}
					grid.DataSource = da.QueryAsDataTable(sql);					
				} 
				else 
				{
                    sql = String.Format("SELECT b.level_no AS ID, a.level_value AS col1, b.data_value AS col2 FROM level_varchar a INNER JOIN data_varchar b ON a.level_no=b.level_no WHERE a.study_id={0} AND b.study_id={0} AND b.variate_id={1}", engine.GetStudyId(), variate_factor_id);
					if(updateStyle) 
					{
						UpdateGridStyle("col1", "col2", "Bar Code", selectedVariate);
					}
					int x = 0;			
					x = Settings.CURRENT_PAGE_NO * Settings.MAX_RECORD_PER_PAGE;
					grid.DataSource = da.QueryAsDataset(sql, x, Settings.MAX_RECORD_PER_PAGE).Tables[0];
				}
				grid.Refresh();
			} 
			catch(SqlCeException e) { }
		}

		#endregion
		#region Grid Style, Update Tab Window
		private void UpdateGridStyle(string col1, string col2, string head1, string head2) 
		{
			grid.TableStyles.Clear();
			DataGridTableStyle myGridTableStyle = new DataGridTableStyle();
			DataGridTextBoxColumn myColumn = new DataGridTextBoxColumn();
			myGridTableStyle.MappingName = "czetsuya";
			
			myColumn.Width = 0;
			myColumn.MappingName = "ID";
			myColumn.HeaderText = "ID";
			myGridTableStyle.GridColumnStyles.Add(myColumn);

			DataGridTextBoxColumn myColumn1 = new DataGridTextBoxColumn();
			myColumn1.Width = 100;
			myColumn1.MappingName = col1;
			myColumn1.HeaderText = head1;
			myGridTableStyle.GridColumnStyles.Add(myColumn1);

			DataGridTextBoxColumn myColumn2 = new DataGridTextBoxColumn();
			myColumn2.Width = 100;
			myColumn2.MappingName = col2;
			myColumn2.HeaderText = head2;
			myGridTableStyle.GridColumnStyles.Add(myColumn2);
              
			grid.TableStyles.Add(myGridTableStyle);
		}

		private void UpdateScreen() 
		{
			frmLoader.progressbar1.Maximum = 5;
			frmLoader.Show();
			frmLoader.Update(1, Helper.LanguageHelper.GetMessage("m_obs_init_data"));
			Init();
		}

		#endregion
		#region Control Events
		private void cbVariates_SelectedIndexChanged(object sender, EventArgs e)
		{
			DataRowView row = (DataRowView)cbVariates.SelectedItem;
			object x = row.Row.ItemArray[0];
			if(!isPlant) 
			{
				selectedVariate = row.Row.ItemArray[1].ToString();
			}

			if(variate_factor_id != x) 
			{
				variate_factor_id = x;
			
				ShowInputValues();
				LoadGrid(true);

				tbValue.Text = "";
			}
			grid.Select(0);
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			Save();
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

		private void grid_CurrentCellChanged(object sender, EventArgs e)
		{
			if(isPlant) 
			{
				tbValue.Text = "";
				DataTable dt = (DataTable)grid.DataSource;
				variate_id = dt.Rows[grid.CurrentRowIndex].ItemArray[0];
				ShowInputValues();
			}
			grid.Select(grid.CurrentRowIndex);
		}

		private void rbtnP_Click(object sender, EventArgs e)
		{
			if(rbtnP.Checked) 
			{
				btnList.Enabled = true;
				cbPaging.Enabled = false;
				isPlant = true;
			} 
			else 
			{	
				btnList.Enabled = false;
				cbPaging.Enabled = true;
				isPlant = false;
			}
			UpdateScreen();
		}

		private void rbtnV_Click(object sender, EventArgs e)
		{
			if(rbtnV.Checked) 
			{
				btnList.Enabled = false;
				cbPaging.Enabled = true;
				isPlant = false;
			} 
			else 
			{	
				btnList.Enabled = true;
				cbPaging.Enabled = false;
				isPlant = true;
			}
			UpdateScreen();
		}

		private void timerBtn_Tick(object sender, EventArgs e)
		{
			btnSave.Enabled = true;
			btnSave.Text = "Save";
			timerBtn.Enabled = false;
		}

		private void grid_KeyPress(object sender, KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)Keys.Enter) 
			{
				tbValue.Focus();
			}
		}

		private void tbValue_KeyPress(object sender, KeyPressEventArgs e)
		{
			if(e.KeyChar == (char)Keys.Enter) 
			{
				Save();
			}
		}

		private void btnList_Click(object sender, EventArgs e)
		{
			frmPlantList f = new frmPlantList(this, engine.GetStudyId());
			f.Show();
		}

		private void cbPaging_SelectedIndexChanged(object sender, EventArgs e)
		{
			Settings.CURRENT_PAGE_NO = cbPaging.SelectedIndex;
			frmLoader.Show();
			frmLoader.progressbar1.Maximum = 2;
			frmLoader.Update(1, "Loading datagrid");
			LoadGrid(false);
			frmLoader.Update(2);
			frmLoader.Hide();
		}
		#endregion
		#region Save Data
		private void Save() 
		{
			timerBtn.Enabled = true;
			btnSave.Text = "Saving";
			btnSave.Enabled = false;

			bool flag = true;
			if(isContinuous) 
			{
				double d = 0;
				try 
				{
					d = Double.Parse(tbValue.Text);
				} 
				catch(ArgumentException e1) 
				{
					Helper.LogHelper.WriteLog(e1.Message);
				}
				catch(FormatException e1) 
				{
					Helper.LogHelper.WriteLog(e1.Message);
				}
				catch(OverflowException e1) 
				{ 
					Helper.LogHelper.WriteLog(e1.Message);
				}
				if(!(d >= trackValue.Minimum && d <= trackValue.Maximum))
				{
					flag = false;
					Helper.ResourceHelper.ShowInfo(Helper.LanguageHelper.GetMessage("m_obs_value_out_range"));
					tbValue.Text = "";
					tbValue.Focus();
				}
			} 

			if(flag) 
			{
				DataTable dt = (DataTable)grid.DataSource;
				int x = grid.CurrentRowIndex;
				if(!tbValue.Text.Equals(dt.Rows[x].ItemArray[2])) 
				{ //index 2 is the data
					string sql = "";
					string grid_current_index = dt.Rows[x].ItemArray[0].ToString();

					if(isPlant) 
					{
						sql = String.Format("UPDATE data_varchar SET data_value='{0}' WHERE study_id={1} AND variate_id={2} AND level_no={3}", tbValue.Text, engine.GetStudyId(), grid_current_index, variate_factor_id);
					} 
					else 
					{
						sql = String.Format("UPDATE data_varchar SET data_value='{0}' WHERE study_id={1} AND variate_id={2} AND level_no={3}", tbValue.Text, engine.GetStudyId(), variate_factor_id, grid_current_index);
					}
					//MessageBox.Show(sql, "");
					da.Update(sql);
					//LoadGrid();
					grid[grid.CurrentRowIndex, 2] = tbValue.Text;

					grid.Focus();

					if((x + 1) < dt.Rows.Count) 
					{
						grid.CurrentRowIndex = x + 1;
					}
					else 
					{
						grid.Select(0);
						grid.CurrentRowIndex = 0;
					}
				}
			}
		}
		#endregion

		#region Paging
		public void RefreshPaging(int selectedIndex) 
		{
			//sql = String.Format("SELECT l.level_no, l.level_value FROM factor f INNER JOIN level_varchar l ON f.factor_id=l.factor_id WHERE f.study_id={0} ORDER BY l.level_value", engine.GetStudyId());
			String sql = "SELECT level_no, level_value FROM level_varchar WHERE study_id=" + engine.GetStudyId();

			int x = 0;
			if(Settings.CURRENT_PAGE_NO != 0)
			{
				x = Settings.CURRENT_PAGE_NO * Settings.MAX_RECORD_PER_PAGE;
			} 
			else 
			{
				x = Settings.CURRENT_PAGE_NO * Settings.MAX_RECORD_PER_PAGE;
			}

			DataSet ds = da.QueryAsDataset(sql, x, Settings.MAX_RECORD_PER_PAGE);

			cbVariates.SelectedIndexChanged -= eventSelectedIndexChanged;
			cbVariates.DataSource = ds.Tables[0];
			cbVariates.ValueMember = "level_no";
			cbVariates.DisplayMember = "level_value";
			cbVariates.SelectedIndex = selectedIndex;
			cbVariates.SelectedIndexChanged += eventSelectedIndexChanged;
		}
		#endregion	
	}
}
