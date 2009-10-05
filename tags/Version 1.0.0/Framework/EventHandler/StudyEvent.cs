/**
 * @author edwardpantojalegaspi
 * @since 2009.09.28
 * */

using System;
using System.Collections;
using System.Windows.Forms;
using System.Data;

using IcisMobile.Framework.DataAccessLayer;
using IcisMobile.Framework.DataCollection.Tree;

namespace IcisMobile.Framework.EventHandler
{
	/// <summary>
	/// Summary description for StudyEvent.
	/// </summary>
	public class StudyEvent
	{
		private TabPage page;
		private TreeList treeList;
		private Engine engine;
		private frmProgress frmLoader = new frmProgress();

		public StudyEvent(Engine engine, object obj)
		{
			this.engine = engine;

			//get the page and tree view
			page = (TabPage)obj;
			TreeView view = null;
			
			foreach(Control temp in page.Controls) 
			{
				if(temp is TreeView)
					view = (TreeView)temp;
			}
			
			view.AfterSelect += new TreeViewEventHandler(view_AfterSelect);

			//get the root node
			TreeNode root = view.Nodes[0];
			root.Nodes.Clear();
			
			treeList = new TreeList();

			//select all studies
			DataAccess da = new DataAccess();
			DataSet ds = da.QueryAsDataset("SELECT study_id, study_name, study_title FROM study ORDER BY study_name");
			DataTable table = ds.Tables[0];

			frmLoader.Show();
			frmLoader.progressbar1.Maximum = table.Rows.Count;
			int cnt = 1;
			//begin updating the tree
			view.BeginUpdate();
			foreach(DataRow row in table.Rows) 
			{
				frmLoader.Update(cnt++, "Loading " + row["study_name"].ToString());
				string title = row["study_name"] + "/" + row["study_title"];
				TreeNode tempNode = new TreeNode();
				tempNode.Text = title;
				treeList.AddNode(Convert.ToInt16(row["study_id"].ToString()), root.Nodes.Add(tempNode), title);
			}
			view.EndUpdate();
			frmLoader.Hide();
			view.CollapseAll();
		}

		private void view_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeView view = (TreeView)sender;

			foreach(Control temp in page.Controls) 
			{
				try 
				{
					if(temp is Label) 
					{
						Framework.DataCollection.Tree.TreeList.Node node = treeList.GetNodeByTreeId(view.SelectedNode.Index);
						((Label)temp).Text = ">" + node.name;
						engine.SetStudyId(node.databaseid);
						break;
					}
				} 
				catch(NullReferenceException e1) { }
			}
		}
	}
}
