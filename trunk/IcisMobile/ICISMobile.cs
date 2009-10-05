using System;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.Resources;
using System.Reflection;
using System.Drawing;

using IcisMobile.Framework.Helper;
using IcisMobile.Framework;
using IcisMobile.Framework.Util;

namespace IcisMobile
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class IcisMobile : System.Windows.Forms.Form
	{
		#region Native
		private System.Windows.Forms.TabPage tabAbout;
		private System.Windows.Forms.TabPage tabVariate;
		private System.Windows.Forms.TabPage tabScale;
		private System.Windows.Forms.TabPage tabStudy;
		private System.Windows.Forms.TabPage tabSystem;
		private System.Windows.Forms.PictureBox imgIcis;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label lblCopyright;
		private System.Windows.Forms.PictureBox imgICISMain;
		private System.Windows.Forms.TreeView studyTree;
		private System.Windows.Forms.Label txtStudy;
		private System.Windows.Forms.Label lblVariate;
		private System.Windows.Forms.DataGrid dgScale;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
		private System.Windows.Forms.TabPage tabObservation;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblName1;
		private System.Windows.Forms.Label lblProperty1;
		private System.Windows.Forms.Label lblScale1;
		private System.Windows.Forms.Label lblMethod1;
		private System.Windows.Forms.Label lblDatatype1;
		private System.Windows.Forms.Label lblFactor1;
		private System.Windows.Forms.Label lblObsFactor;
		private System.Windows.Forms.ComboBox cbObsVariates;
		private System.Windows.Forms.ComboBox cbObsValues;
		private System.Windows.Forms.ComboBox cbScaleVariates;
		private System.Windows.Forms.ComboBox cbVariateVariates;
		private System.Windows.Forms.TrackBar trackObsValue;
		private System.Windows.Forms.TextBox tbObsValue;
		private System.Windows.Forms.Button btnObsSave;
		private System.Windows.Forms.Label lblVariates;
		private System.Windows.Forms.ComboBox vbVarVariates;
		private System.Windows.Forms.ComboBox cbScaleVariate;
		private System.Windows.Forms.DataGrid dgData;
		private System.Windows.Forms.RadioButton rbtnV;
		private System.Windows.Forms.RadioButton rbtnP;
		private System.Windows.Forms.TabControl tabMenu;

		public IcisMobile()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			InitializeICISComponents();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(IcisMobile));
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode();
			this.tabMenu = new System.Windows.Forms.TabControl();
			this.tabAbout = new System.Windows.Forms.TabPage();
			this.imgICISMain = new System.Windows.Forms.PictureBox();
			this.lblCopyright = new System.Windows.Forms.Label();
			this.imgIcis = new System.Windows.Forms.PictureBox();
			this.lblVersion = new System.Windows.Forms.Label();
			this.tabObservation = new System.Windows.Forms.TabPage();
			this.rbtnV = new System.Windows.Forms.RadioButton();
			this.rbtnP = new System.Windows.Forms.RadioButton();
			this.btnObsSave = new System.Windows.Forms.Button();
			this.lblObsFactor = new System.Windows.Forms.Label();
			this.dgData = new System.Windows.Forms.DataGrid();
			this.cbObsVariates = new System.Windows.Forms.ComboBox();
			this.cbObsValues = new System.Windows.Forms.ComboBox();
			this.trackObsValue = new System.Windows.Forms.TrackBar();
			this.tbObsValue = new System.Windows.Forms.TextBox();
			this.tabVariate = new System.Windows.Forms.TabPage();
			this.vbVarVariates = new System.Windows.Forms.ComboBox();
			this.lblVariates = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblDatatype1 = new System.Windows.Forms.Label();
			this.lblMethod1 = new System.Windows.Forms.Label();
			this.lblScale1 = new System.Windows.Forms.Label();
			this.lblProperty1 = new System.Windows.Forms.Label();
			this.lblName1 = new System.Windows.Forms.Label();
			this.tabScale = new System.Windows.Forms.TabPage();
			this.cbScaleVariate = new System.Windows.Forms.ComboBox();
			this.dgScale = new System.Windows.Forms.DataGrid();
			this.lblVariate = new System.Windows.Forms.Label();
			this.tabStudy = new System.Windows.Forms.TabPage();
			this.txtStudy = new System.Windows.Forms.Label();
			this.studyTree = new System.Windows.Forms.TreeView();
			this.tabSystem = new System.Windows.Forms.TabPage();
			this.cbScaleVariates = new System.Windows.Forms.ComboBox();
			this.cbVariateVariates = new System.Windows.Forms.ComboBox();
			this.lblFactor1 = new System.Windows.Forms.Label();
			this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
			// 
			// tabMenu
			// 
			this.tabMenu.Controls.Add(this.tabAbout);
			this.tabMenu.Controls.Add(this.tabObservation);
			this.tabMenu.Controls.Add(this.tabVariate);
			this.tabMenu.Controls.Add(this.tabScale);
			this.tabMenu.Controls.Add(this.tabStudy);
			this.tabMenu.Controls.Add(this.tabSystem);
			this.tabMenu.SelectedIndex = 0;
			this.tabMenu.Size = new System.Drawing.Size(240, 272);
			// 
			// tabAbout
			// 
			this.tabAbout.Controls.Add(this.imgICISMain);
			this.tabAbout.Controls.Add(this.lblCopyright);
			this.tabAbout.Controls.Add(this.imgIcis);
			this.tabAbout.Controls.Add(this.lblVersion);
			this.tabAbout.Location = new System.Drawing.Point(4, 4);
			this.tabAbout.Size = new System.Drawing.Size(232, 243);
			this.tabAbout.Text = "About";
			// 
			// imgICISMain
			// 
			this.imgICISMain.Image = ((System.Drawing.Image)(resources.GetObject("imgICISMain.Image")));
			this.imgICISMain.Location = new System.Drawing.Point(26, 56);
			this.imgICISMain.Size = new System.Drawing.Size(184, 144);
			this.imgICISMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			// 
			// lblCopyright
			// 
			this.lblCopyright.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
			this.lblCopyright.Location = new System.Drawing.Point(8, 230);
			this.lblCopyright.Size = new System.Drawing.Size(216, 24);
			this.lblCopyright.Text = "Copyright 2009 CRIL, IRRI";
			// 
			// imgIcis
			// 
			this.imgIcis.Image = ((System.Drawing.Image)(resources.GetObject("imgIcis.Image")));
			this.imgIcis.Size = new System.Drawing.Size(240, 56);
			this.imgIcis.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			// 
			// lblVersion
			// 
			this.lblVersion.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
			this.lblVersion.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblVersion.Location = new System.Drawing.Point(8, 210);
			this.lblVersion.Size = new System.Drawing.Size(208, 24);
			this.lblVersion.Text = "ICIS Mobile Ver. 1.0.0";
			// 
			// tabObservation
			// 
			this.tabObservation.Controls.Add(this.rbtnV);
			this.tabObservation.Controls.Add(this.rbtnP);
			this.tabObservation.Controls.Add(this.btnObsSave);
			this.tabObservation.Controls.Add(this.lblObsFactor);
			this.tabObservation.Controls.Add(this.dgData);
			this.tabObservation.Controls.Add(this.cbObsVariates);
			this.tabObservation.Controls.Add(this.cbObsValues);
			this.tabObservation.Controls.Add(this.trackObsValue);
			this.tabObservation.Controls.Add(this.tbObsValue);
			this.tabObservation.Location = new System.Drawing.Point(4, 4);
			this.tabObservation.Size = new System.Drawing.Size(232, 243);
			this.tabObservation.Text = "Observation";
			// 
			// rbtnV
			// 
			this.rbtnV.Checked = true;
			this.rbtnV.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
			this.rbtnV.Location = new System.Drawing.Point(40, 0);
			this.rbtnV.Size = new System.Drawing.Size(32, 20);
			this.rbtnV.Text = "V";
			// 
			// rbtnP
			// 
			this.rbtnP.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
			this.rbtnP.Location = new System.Drawing.Point(8, 0);
			this.rbtnP.Size = new System.Drawing.Size(32, 20);
			this.rbtnP.Text = "P";
			// 
			// btnObsSave
			// 
			this.btnObsSave.Location = new System.Drawing.Point(176, 24);
			this.btnObsSave.Size = new System.Drawing.Size(48, 20);
			this.btnObsSave.Text = "Save";
			// 
			// lblObsFactor
			// 
			this.lblObsFactor.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
			this.lblObsFactor.Location = new System.Drawing.Point(72, 2);
			this.lblObsFactor.Size = new System.Drawing.Size(160, 20);
			this.lblObsFactor.Text = "Factor";
			// 
			// dgData
			// 
			this.dgData.Location = new System.Drawing.Point(0, 76);
			this.dgData.Size = new System.Drawing.Size(240, 162);
			this.dgData.Text = "dataGrid1";
			// 
			// cbObsVariates
			// 
			this.cbObsVariates.DisplayMember = "variate_id";
			this.cbObsVariates.Location = new System.Drawing.Point(8, 24);
			this.cbObsVariates.Size = new System.Drawing.Size(164, 26);
			this.cbObsVariates.ValueMember = "variate_id";
			// 
			// cbObsValues
			// 
			this.cbObsValues.DisplayMember = "scale_id";
			this.cbObsValues.Location = new System.Drawing.Point(112, 48);
			this.cbObsValues.Size = new System.Drawing.Size(112, 26);
			this.cbObsValues.ValueMember = "scale_id";
			// 
			// trackObsValue
			// 
			this.trackObsValue.Location = new System.Drawing.Point(112, 48);
			this.trackObsValue.Size = new System.Drawing.Size(120, 56);
			this.trackObsValue.Visible = false;
			// 
			// tbObsValue
			// 
			this.tbObsValue.Location = new System.Drawing.Point(8, 48);
			this.tbObsValue.Size = new System.Drawing.Size(98, 26);
			this.tbObsValue.Text = "";
			// 
			// tabVariate
			// 
			this.tabVariate.Controls.Add(this.vbVarVariates);
			this.tabVariate.Controls.Add(this.lblVariates);
			this.tabVariate.Controls.Add(this.panel1);
			this.tabVariate.Location = new System.Drawing.Point(4, 4);
			this.tabVariate.Size = new System.Drawing.Size(232, 243);
			this.tabVariate.Text = "Variate";
			// 
			// vbVarVariates
			// 
			this.vbVarVariates.Location = new System.Drawing.Point(112, 7);
			this.vbVarVariates.Size = new System.Drawing.Size(100, 22);
			// 
			// lblVariates
			// 
			this.lblVariates.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
			this.lblVariates.Location = new System.Drawing.Point(24, 10);
			this.lblVariates.Size = new System.Drawing.Size(72, 20);
			this.lblVariates.Text = "Variates:";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lblDatatype1);
			this.panel1.Controls.Add(this.lblMethod1);
			this.panel1.Controls.Add(this.lblScale1);
			this.panel1.Controls.Add(this.lblProperty1);
			this.panel1.Controls.Add(this.lblName1);
			this.panel1.Location = new System.Drawing.Point(0, 40);
			this.panel1.Size = new System.Drawing.Size(240, 200);
			// 
			// lblDatatype1
			// 
			this.lblDatatype1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
			this.lblDatatype1.Location = new System.Drawing.Point(8, 160);
			this.lblDatatype1.Size = new System.Drawing.Size(224, 20);
			this.lblDatatype1.Text = "Data Type:";
			// 
			// lblMethod1
			// 
			this.lblMethod1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
			this.lblMethod1.Location = new System.Drawing.Point(8, 128);
			this.lblMethod1.Size = new System.Drawing.Size(224, 20);
			this.lblMethod1.Text = "Method:";
			// 
			// lblScale1
			// 
			this.lblScale1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
			this.lblScale1.Location = new System.Drawing.Point(8, 88);
			this.lblScale1.Size = new System.Drawing.Size(240, 20);
			this.lblScale1.Text = "Scale:";
			// 
			// lblProperty1
			// 
			this.lblProperty1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
			this.lblProperty1.Location = new System.Drawing.Point(8, 56);
			this.lblProperty1.Size = new System.Drawing.Size(224, 20);
			this.lblProperty1.Text = "Property:";
			// 
			// lblName1
			// 
			this.lblName1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
			this.lblName1.Location = new System.Drawing.Point(8, 16);
			this.lblName1.Size = new System.Drawing.Size(232, 20);
			this.lblName1.Text = "Name:";
			// 
			// tabScale
			// 
			this.tabScale.Controls.Add(this.cbScaleVariate);
			this.tabScale.Controls.Add(this.dgScale);
			this.tabScale.Controls.Add(this.lblVariate);
			this.tabScale.Location = new System.Drawing.Point(4, 4);
			this.tabScale.Size = new System.Drawing.Size(232, 243);
			this.tabScale.Text = "Scale";
			// 
			// cbScaleVariate
			// 
			this.cbScaleVariate.Location = new System.Drawing.Point(104, 6);
			this.cbScaleVariate.Size = new System.Drawing.Size(100, 22);
			// 
			// dgScale
			// 
			this.dgScale.Location = new System.Drawing.Point(0, 40);
			this.dgScale.Size = new System.Drawing.Size(240, 200);
			this.dgScale.Text = "Scales";
			// 
			// lblVariate
			// 
			this.lblVariate.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
			this.lblVariate.Location = new System.Drawing.Point(24, 10);
			this.lblVariate.Size = new System.Drawing.Size(72, 20);
			this.lblVariate.Text = "Variate:";
			// 
			// tabStudy
			// 
			this.tabStudy.Controls.Add(this.txtStudy);
			this.tabStudy.Controls.Add(this.studyTree);
			this.tabStudy.Location = new System.Drawing.Point(4, 4);
			this.tabStudy.Size = new System.Drawing.Size(232, 243);
			this.tabStudy.Text = "Study";
			// 
			// txtStudy
			// 
			this.txtStudy.Location = new System.Drawing.Point(8, 200);
			this.txtStudy.Size = new System.Drawing.Size(224, 40);
			// 
			// studyTree
			// 
			this.studyTree.ImageIndex = -1;
			treeNode1.Text = "Studies";
			this.studyTree.Nodes.Add(treeNode1);
			this.studyTree.SelectedImageIndex = -1;
			this.studyTree.Size = new System.Drawing.Size(232, 192);
			// 
			// tabSystem
			// 
			this.tabSystem.Location = new System.Drawing.Point(4, 4);
			this.tabSystem.Size = new System.Drawing.Size(232, 243);
			this.tabSystem.Text = "System";
			// 
			// cbScaleVariates
			// 
			this.cbScaleVariates.Size = new System.Drawing.Size(100, 22);
			// 
			// cbVariateVariates
			// 
			this.cbVariateVariates.Size = new System.Drawing.Size(100, 22);
			// 
			// dataGridTextBoxColumn1
			// 
			this.dataGridTextBoxColumn1.HeaderText = "Min";
			this.dataGridTextBoxColumn1.MappingName = "scalecon_start";
			this.dataGridTextBoxColumn1.NullText = "(null)";
			// 
			// dataGridTextBoxColumn2
			// 
			this.dataGridTextBoxColumn2.HeaderText = "Max";
			this.dataGridTextBoxColumn2.MappingName = "scalecon_end";
			this.dataGridTextBoxColumn2.NullText = "(null)";
			// 
			// IcisMobile
			// 
			this.Controls.Add(this.tabMenu);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Text = "ICIS Mobile";

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>

		static void Main() 
		{
			Application.Run(new IcisMobile());
		}

		#endregion

		#region System
		private Engine engine;
		private void InitializeICISComponents() 
		{
			tabMenu.TabPages.Clear();
			tabMenu.TabPages.Add(tabAbout);
			tabMenu.TabPages.Add(tabStudy);
			tabMenu.TabPages.Add(tabVariate);
			tabMenu.TabPages.Add(tabScale);
			tabMenu.TabPages.Add(tabObservation);
			tabMenu.TabPages.Add(tabSystem);

			tabAbout.Text = LanguageHelper.GetConfig("tab_about");
			tabStudy.Text = LanguageHelper.GetConfig("tab_study");
			tabVariate.Text = LanguageHelper.GetConfig("tab_variate");
			tabScale.Text = LanguageHelper.GetConfig("tab_scale");
			tabObservation.Text = LanguageHelper.GetConfig("tab_observation");
			tabSystem.Text = LanguageHelper.GetConfig("tab_system");

			tabMenu.Refresh();
			
			tabMenu.SelectedIndexChanged += new EventHandler(tabMenu_SelectedIndexChanged);
			
			engine = new Engine();

			initSystem();
		}

		#region System
		private ImageButton ibtnInitDb;
		private ImageButton ibtnLoadStudy;
		private ImageButton ibtnExit;
		private ImageButton ibtnDownloadData;
		private void initSystem() 
		{
			int hmargin = 45;
			ibtnInitDb = new ImageButton();
			ibtnInitDb.Image = ResourceHelper.GetImage("b_init_db.jpg");
			ibtnInitDb.Location = new Point(30, 30);
			ibtnInitDb.Size = new Size(67, 66);
			ibtnInitDb.Click += new EventHandler(ibtnInitDb_Click);
			tabSystem.Controls.Add(ibtnInitDb);

			ibtnLoadStudy = new ImageButton();
			ibtnLoadStudy.Image = ResourceHelper.GetImage("b_load_study.jpg");
			ibtnLoadStudy.Location = new Point(30 + 67 + hmargin, 30);
			ibtnLoadStudy.Size = new Size(67, 66);
			ibtnLoadStudy.Click += new EventHandler(ibtnLoadStudy_Click);
			tabSystem.Controls.Add(ibtnLoadStudy);

			ibtnDownloadData = new ImageButton();
			ibtnDownloadData.Image = ResourceHelper.GetImage("b_download_data.jpg");
			ibtnDownloadData.Location = new Point(30, 30 + hmargin +  66);
			ibtnDownloadData.Size = new Size(67, 66);
			ibtnDownloadData.Click += new EventHandler(ibtnDownloadData_Click);
			tabSystem.Controls.Add(ibtnDownloadData);

			ibtnExit = new ImageButton();
			ibtnExit.Image = ResourceHelper.GetImage("b_exit.jpg");
			ibtnExit.Location = new Point(30 + 67 + hmargin, 30 + hmargin +  66);
			ibtnExit.Size = new Size(67, 66);
			ibtnExit.Click += new EventHandler(ibtnExit_Click);
			tabSystem.Controls.Add(ibtnExit);
		}
		#endregion
		
		private void ibtnInitDb_Click(object sender, EventArgs e)
		{
			engine.InitSchema();
		}

		private void ibtnLoadStudy_Click(object sender, EventArgs e)
		{
			engine.LoadStudyFromFile();
		}

		private void ibtnExit_Click(object sender, EventArgs e)
		{
			Destroy();
		}

		private void ibtnDownloadData_Click(object sender, EventArgs e)
		{
			engine.DownloadData(tabMenu);
		}
		#endregion

		private void tabMenu_SelectedIndexChanged(object sender, EventArgs e)
		{	
			TabControl obj = (TabControl)sender;
			obj.TabPages[obj.SelectedIndex].Refresh();
			switch(obj.SelectedIndex) 
			{ 
				case 0 : //about
					break;
				case 1 : //study
					engine.StudyClicked(obj.TabPages[obj.SelectedIndex]);
					break;
				case 2 : //variate
					engine.VariateClicked(obj.TabPages[obj.SelectedIndex]);
					break;
				case 3 : //scale
                    engine.ScaleClicked(obj.TabPages[obj.SelectedIndex]);
					break;
				case 4 : //data
					engine.ObservationClick(obj.TabPages[obj.SelectedIndex]);
					break;
				case 5 : //system
					break;
				default :
					break;
			}
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		public void ShowTab(int x) 
		{
			tabMenu.SelectedIndex = x;
		}

		private void Destroy() 
		{
			frmProgress frmProgressLoader = new frmProgress();
			frmProgressLoader.Show();
			frmProgressLoader.progressbar1.Maximum = 2;

			frmProgressLoader.Update(1, LanguageHelper.GetMessage("m_unloading"));
				
			frmProgressLoader.progressbar1.Value = 2;
			frmProgressLoader.Hide();
			
			engine.Destroy();
			Application.Exit();
		}
	}
}
