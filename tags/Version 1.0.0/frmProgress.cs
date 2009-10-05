using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace IcisMobile
{
	/// <summary>
	/// Summary description for frmProgress.
	/// </summary>
	public class frmProgress : System.Windows.Forms.Form
	{
		public System.Windows.Forms.ProgressBar progressbar1;
		public  System.Windows.Forms.Label lblMsg;
	
		public frmProgress()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public void Update(int i, string label) 
		{
			progressbar1.Value = i;
			lblMsg.Text = label;
			this.Refresh();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmProgress));
			this.progressbar1 = new System.Windows.Forms.ProgressBar();
			this.lblMsg = new System.Windows.Forms.Label();
			// 
			// progressbar1
			// 
			this.progressbar1.Location = new System.Drawing.Point(40, 104);
			// 
			// lblMsg
			// 
			this.lblMsg.Location = new System.Drawing.Point(8, 136);
			this.lblMsg.Size = new System.Drawing.Size(224, 20);
			this.lblMsg.Text = "Loading...";
			this.lblMsg.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// frmProgress
			// 
			this.Controls.Add(this.lblMsg);
			this.Controls.Add(this.progressbar1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Text = "Loading...";

		}
		#endregion
	}
}
