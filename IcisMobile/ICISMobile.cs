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
		private System.Windows.Forms.TabPage tabData;
		private System.Windows.Forms.TabPage tabSystem;
		private System.Windows.Forms.PictureBox imgIcis;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label lblCopyright;
		private System.Windows.Forms.PictureBox imgICISMain;
		private System.Windows.Forms.TreeView treeView1;
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
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode();
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode();
			this.tabMenu = new System.Windows.Forms.TabControl();
			this.tabSystem = new System.Windows.Forms.TabPage();
			this.tabAbout = new System.Windows.Forms.TabPage();
			this.imgICISMain = new System.Windows.Forms.PictureBox();
			this.lblCopyright = new System.Windows.Forms.Label();
			this.imgIcis = new System.Windows.Forms.PictureBox();
			this.lblVersion = new System.Windows.Forms.Label();
			this.tabScale = new System.Windows.Forms.TabPage();
			this.tabStudy = new System.Windows.Forms.TabPage();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.tabVariate = new System.Windows.Forms.TabPage();
			this.tabData = new System.Windows.Forms.TabPage();
			// 
			// tabMenu
			// 
			this.tabMenu.Controls.Add(this.tabSystem);
			this.tabMenu.Controls.Add(this.tabData);
			this.tabMenu.Controls.Add(this.tabAbout);
			this.tabMenu.Controls.Add(this.tabScale);
			this.tabMenu.Controls.Add(this.tabStudy);
			this.tabMenu.Controls.Add(this.tabVariate);
			this.tabMenu.SelectedIndex = 0;
			this.tabMenu.Size = new System.Drawing.Size(240, 272);
			// 
			// tabSystem
			// 
			this.tabSystem.Location = new System.Drawing.Point(4, 4);
			this.tabSystem.Size = new System.Drawing.Size(232, 243);
			this.tabSystem.Text = "System";
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
			// tabScale
			// 
			this.tabScale.Location = new System.Drawing.Point(4, 4);
			this.tabScale.Size = new System.Drawing.Size(232, 243);
			this.tabScale.Text = "Scale";
			// 
			// tabStudy
			// 
			this.tabStudy.Controls.Add(this.treeView1);
			this.tabStudy.Location = new System.Drawing.Point(4, 4);
			this.tabStudy.Size = new System.Drawing.Size(232, 243);
			this.tabStudy.Text = "Study";
			// 
			// treeView1
			// 
			this.treeView1.ImageIndex = -1;
			this.treeView1.Location = new System.Drawing.Point(16, 16);
			treeNode3.Text = "Node2";
			treeNode2.Nodes.Add(treeNode3);
			treeNode2.Text = "Node1";
			treeNode1.Nodes.Add(treeNode2);
			treeNode1.Text = "Node0";
			this.treeView1.Nodes.Add(treeNode1);
			this.treeView1.SelectedImageIndex = -1;
			// 
			// tabVariate
			// 
			this.tabVariate.Location = new System.Drawing.Point(4, 4);
			this.tabVariate.Size = new System.Drawing.Size(232, 243);
			this.tabVariate.Text = "Variate";
			// 
			// tabData
			// 
			this.tabData.Location = new System.Drawing.Point(4, 4);
			this.tabData.Size = new System.Drawing.Size(232, 243);
			this.tabData.Text = "Data";
			// 
			// IcisMobile
			// 
			this.Controls.Add(this.tabMenu);
			this.Text = "ICIS-Mobile";

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
			tabAbout.Text = LanguageHelper.GetConfig("tab_about");
			tabVariate.Text = LanguageHelper.GetConfig("tab_variate");
			tabScale.Text = LanguageHelper.GetConfig("tab_scale");
			tabStudy.Text = LanguageHelper.GetConfig("tab_study");
			tabData.Text = LanguageHelper.GetConfig("tab_data");
			tabSystem.Text = LanguageHelper.GetConfig("tab_system");
			
			engine = new Engine();

			initSystem();
		}

		#region System
		private ImageButton ibtnInitDb;
		private ImageButton ibtnLoadStudy;
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
		#endregion
	}
}
