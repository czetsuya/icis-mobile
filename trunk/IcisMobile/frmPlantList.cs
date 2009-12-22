using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using System.Data;

using IcisMobile.Framework;

namespace IcisMobile
{
	/// <summary>
	/// Summary description for frmPlantList.
	/// </summary>
	public class frmPlantList : System.Windows.Forms.Form
	{
		#region System
		private System.Windows.Forms.ListBox lbPlant;
		private System.Windows.Forms.Button btnSelPlant;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.ComboBox cbPage;
		private System.Windows.Forms.Label lblPlanList;
	
		public frmPlantList()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lbPlant = new System.Windows.Forms.ListBox();
			this.lblPlanList = new System.Windows.Forms.Label();
			this.btnSelPlant = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.cbPage = new System.Windows.Forms.ComboBox();
			// 
			// lbPlant
			// 
			this.lbPlant.Location = new System.Drawing.Point(8, 40);
			this.lbPlant.Size = new System.Drawing.Size(224, 182);
			// 
			// lblPlanList
			// 
			this.lblPlanList.Location = new System.Drawing.Point(0, 8);
			this.lblPlanList.Size = new System.Drawing.Size(240, 24);
			this.lblPlanList.Text = "List of Plants";
			this.lblPlanList.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// btnSelPlant
			// 
			this.btnSelPlant.Location = new System.Drawing.Point(176, 234);
			this.btnSelPlant.Size = new System.Drawing.Size(56, 22);
			this.btnSelPlant.Text = "Select";
			this.btnSelPlant.Click += new System.EventHandler(this.btnSelPlant_Click);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(120, 234);
			this.btnClose.Size = new System.Drawing.Size(55, 22);
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// cbPage
			// 
			this.cbPage.Location = new System.Drawing.Point(8, 232);
			this.cbPage.SelectedIndexChanged += new System.EventHandler(this.cbPage_SelectedIndexChanged);
			// 
			// frmPlantList
			// 
			this.Controls.Add(this.cbPage);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnSelPlant);
			this.Controls.Add(this.lblPlanList);
			this.Controls.Add(this.lbPlant);
			this.Text = "frmPlantList";

		}
		#endregion
		#endregion
		
		private int studyId;
		private Framework.EventHandler.ObservationEvent obsEvent;

		public frmPlantList(Framework.EventHandler.ObservationEvent obsEvent, int studyId) 
		{
			InitializeComponent();
			
			this.obsEvent = obsEvent;
			this.studyId = studyId;

			InitializeICIS();
		}

		private void InitializeICIS() 
		{
			cbPage.Items.Clear();
			for(int i = 0; i <= Settings.RECORD_COUNT_PLANT; i++) 
			{				
				cbPage.Items.Add(i);
			}

			refreshGrid();
		}

		private void refreshGrid() 
		{
			String sql = "SELECT level_no, level_value FROM level_varchar WHERE study_id=" + studyId;
			Framework.DataAccessLayer.DataAccess da = new Framework.DataAccessLayer.DataAccess();
						
			int x = 0;			
			x = Settings.CURRENT_PAGE_NO * Settings.MAX_RECORD_PER_PAGE;

			DataSet ds = da.QueryAsDataset(sql, x, Settings.MAX_RECORD_PER_PAGE);
			lbPlant.DataSource = ds.Tables[0];
			lbPlant.DisplayMember = "level_value";
			lbPlant.ValueMember = "level_no";
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void cbPage_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Settings.CURRENT_PAGE_NO = cbPage.SelectedIndex;
			refreshGrid();
		}

		private void btnSelPlant_Click(object sender, System.EventArgs e)
		{
			obsEvent.RefreshPaging(lbPlant.SelectedIndex);
			Close();
		}
	}
}
