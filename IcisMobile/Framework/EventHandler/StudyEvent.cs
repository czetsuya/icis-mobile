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
using IcisMobile.Framework.Helper;

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
		private ContextMenu cm;
		private Label lblStudy;
		private TreeView treeView;

		public StudyEvent(Engine engine, object obj)
		{
			this.engine = engine;

			//get the page and tree view
			page = (TabPage)obj;

			foreach(Control temp in page.Controls) 
			{
				if(temp is TreeView) 
				{
					treeView = (TreeView)temp;
				} 
				else if(temp is Label) 
				{
					lblStudy = ((Label)temp);
				}
			}

			cm = new ContextMenu();
			MenuItem item = new MenuItem();
			item.Text = "Delete";
			cm.MenuItems.Add(item);
			lblStudy.ContextMenu = cm;
			lblStudy.Click += new System.EventHandler(lblStudy_Click);
			item.Click += new System.EventHandler(item_Click);
            
			treeView.AfterSelect += new TreeViewEventHandler(view_AfterSelect);

			Init();
		}

		public void Init() 
		{
			//get the root node
			TreeNode root = treeView.Nodes[0];
			root.Nodes.Clear();
			
			treeList = new TreeList();

			//select all studies			
			DataTable dt = DataAccess.Instance().QueryAsDataTable("SELECT study_id, study_name, study_title FROM study ORDER BY study_name");
			DataTable table = dt;

			frmLoader.Show();
			frmLoader.progressbar1.Maximum = table.Rows.Count;
			int cnt = 1;
			//begin updating the tree
			treeView.BeginUpdate();
			foreach(DataRow row in table.Rows) 
			{
				frmLoader.Update(cnt++, "Loading " + row["study_name"].ToString());
				string title = row["study_name"] + "/" + row["study_title"];
				TreeNode tempNode = new TreeNode();
				tempNode.Text = title;
				treeList.AddNode(Convert.ToInt16(row["study_id"].ToString()), root.Nodes.Add(tempNode), title);				
			}
			treeView.EndUpdate();

			frmLoader.Hide();
			treeView.CollapseAll();
		}

		private void view_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeView view = (TreeView)sender;

			try 
			{
				Framework.DataCollection.Tree.TreeList.Node node = treeList.GetNodeByTreeId(view.SelectedNode.Index);
				if(!node.name.ToLower().Equals("studies")) 
				{
					lblStudy.Text = ">" + node.name;
					engine.SetStudyId(node.databaseid);
				}
			} 
			catch(NullReferenceException e1) { }
		}

		private void lblStudy_Click(object sender, EventArgs e)
		{
			cm.Show(lblStudy, new System.Drawing.Point(20, 200));
		}

		private void item_Click(object sender, EventArgs e)
		{

			if(ResourceHelper.ShowQuestion(LanguageHelper.GetMessage("m_confirm_delete"), "Confirm")) 
			{
				lblStudy.Text = "";
				Framework.DataCollection.Tree.TreeList.Node node = treeList.GetNodeByTreeId(treeView.SelectedNode.Index);
				engine.DeleteStudy(node.databaseid);
				
				//remove from tree's arraylist
				treeList.getNodes().Remove(node);
				
				//remove from view
				TreeNode root = treeView.Nodes[0];
				root.Nodes.RemoveAt(node.treeid);
			}
		}
	}
}
