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
		private Label lblFactorName;

		public ObservationEvent(Engine engine, object obj) 
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
				else if(c is Label) 
				{
					lblFactorName = (Label)c;
				}
			}

			SetFactorname();
	
			LoadVariates();

			LoadGrid();
		}

		private void SetFactorname() 
		{
			DataAccess da = new DataAccess();
			object obj = da.QueryScalar(String.Format("SELECT factor_name FROM factor WHERE study_id={0}", engine.GetStudyId()));
			lblFactorName.Text = obj.ToString();
		}

		private void LoadVariates() 
		{
			try 
			{
				DataAccess da = new DataAccess();
				DataSet ds = da.QueryAsDataset(String.Format("SELECT scale_id, variate_name FROM variate WHERE study_id={0}", engine.GetStudyId()));
				cbVariates.ValueMember = "variate_id";
				cbVariates.DisplayMember = "variate_name";
				cbVariates.DataSource = ds.Tables[0];
				cbVariates.Refresh();
			} 
			catch(ArgumentException e)  { }
		}

		private void LoadGrid() 
		{
			try 
			{
				DataAccess da = new DataAccess();
			} 
			catch(SqlCeException e) 
			{
			}
		}
	}
}
