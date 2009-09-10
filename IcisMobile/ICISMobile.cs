using System;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.Resources;
using System.Reflection;

using IcisMobile.Framework.Helper;
using IcisMobile.Framework.Resource;

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
		private System.Windows.Forms.Button btnInitSchema;
		private System.Windows.Forms.PictureBox imgIcis;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label lblCopyright;
		private System.Windows.Forms.PictureBox imgICISMain;
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
			this.tabMenu = new System.Windows.Forms.TabControl();
			this.tabAbout = new System.Windows.Forms.TabPage();
			this.imgIcis = new System.Windows.Forms.PictureBox();
			this.tabSystem = new System.Windows.Forms.TabPage();
			this.btnInitSchema = new System.Windows.Forms.Button();
			this.tabStudy = new System.Windows.Forms.TabPage();
			this.tabScale = new System.Windows.Forms.TabPage();
			this.tabVariate = new System.Windows.Forms.TabPage();
			this.tabData = new System.Windows.Forms.TabPage();
			this.lblVersion = new System.Windows.Forms.Label();
			this.lblCopyright = new System.Windows.Forms.Label();
			this.imgICISMain = new System.Windows.Forms.PictureBox();
			// 
			// tabMenu
			// 
			this.tabMenu.Controls.Add(this.tabAbout);
			this.tabMenu.Controls.Add(this.tabVariate);
			this.tabMenu.Controls.Add(this.tabStudy);
			this.tabMenu.Controls.Add(this.tabScale);
			this.tabMenu.Controls.Add(this.tabData);
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
			// imgIcis
			// 
			this.imgIcis.Image = ((System.Drawing.Image)(resources.GetObject("imgIcis.Image")));
			this.imgIcis.Size = new System.Drawing.Size(240, 56);
			this.imgIcis.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			// 
			// tabSystem
			// 
			this.tabSystem.Controls.Add(this.btnInitSchema);
			this.tabSystem.Location = new System.Drawing.Point(4, 4);
			this.tabSystem.Size = new System.Drawing.Size(232, 243);
			this.tabSystem.Text = "System";
			// 
			// btnInitSchema
			// 
			this.btnInitSchema.Location = new System.Drawing.Point(8, 16);
			this.btnInitSchema.Size = new System.Drawing.Size(176, 24);
			this.btnInitSchema.Text = "Initialize Database";
			this.btnInitSchema.Click += new System.EventHandler(this.btnInitSchema_Click);
			// 
			// tabStudy
			// 
			this.tabStudy.Location = new System.Drawing.Point(4, 4);
			this.tabStudy.Size = new System.Drawing.Size(232, 243);
			this.tabStudy.Text = "Study";
			// 
			// tabScale
			// 
			this.tabScale.Location = new System.Drawing.Point(4, 4);
			this.tabScale.Size = new System.Drawing.Size(232, 243);
			this.tabScale.Text = "Scale";
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
			// lblVersion
			// 
			this.lblVersion.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
			this.lblVersion.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblVersion.Location = new System.Drawing.Point(8, 210);
			this.lblVersion.Size = new System.Drawing.Size(208, 24);
			this.lblVersion.Text = "ICIS Mobile Ver. 1.0.0";
			// 
			// lblCopyright
			// 
			this.lblCopyright.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
			this.lblCopyright.Location = new System.Drawing.Point(8, 230);
			this.lblCopyright.Size = new System.Drawing.Size(216, 24);
			this.lblCopyright.Text = "Copyright 2009 CRIL, IRRI";
			// 
			// imgICISMain
			// 
			this.imgICISMain.Image = ((System.Drawing.Image)(resources.GetObject("imgICISMain.Image")));
			this.imgICISMain.Location = new System.Drawing.Point(26, 56);
			this.imgICISMain.Size = new System.Drawing.Size(184, 144);
			this.imgICISMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
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

		private void InitializeICISComponents() 
		{
			tabAbout.Text = LanguageHelper.GetLabel("tab_about");
			tabVariate.Text = LanguageHelper.GetLabel("tab_variate");
			tabScale.Text = LanguageHelper.GetLabel("tab_scale");
			tabStudy.Text = LanguageHelper.GetLabel("tab_study");
			tabData.Text = LanguageHelper.GetLabel("tab_data");
			tabSystem.Text = LanguageHelper.GetLabel("tab_system");
		}

		#region System
		private void btnInitSchema_Click(object sender, System.EventArgs e)
		{	
			if(System.IO.File.Exists(Settings.TEMP_DIR + Settings.SCHEMA_FILE)) 
			{
				DatabaseHelper.CreateDBFromSchema(XMLHelper.parseSchema(Settings.TEMP_DIR + Settings.SCHEMA_FILE));
			} 
			else 
			{

			}
		}
		#endregion
	}
}
