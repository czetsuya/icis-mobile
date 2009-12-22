/**
 * @author edwardpantojalegaspi
 * @since 2009.09.23
 * */

using System;
using System.Collections;

namespace IcisMobile.Framework.DataCollection.Tree
{
	/// <summary>
	/// Summary description for TreeList.
	/// </summary>
	public class TreeList
	{
		private ArrayList arrNode = new ArrayList();

		public struct Node
		{
			public int databaseid;
			public int treeid;
			public string name;
		}

		public void AddNode(int databaseid, int treeid, string name) 
		{
			Node temp = new Node();
			temp.databaseid = databaseid;
			temp.treeid = treeid;
			temp.name = name;
			arrNode.Add(temp);
		}

		public Node GetNode(int x) 
		{
			return (Node)arrNode[x];
		}

		public string GetNameByTreeId(int x) 
		{
			String s = "";
			foreach(Node temp in arrNode) 
			{
				if(temp.treeid == x) 
				{
					s = temp.name;
				}
			}
			return s;
		}

		public Node GetNodeByTreeId(int x) 
		{
			Node node = new Node();
			foreach(Node temp in arrNode) 
			{
				if(temp.treeid == x) 
				{
					node = temp;
				}
			}
			return node;
		}

		public int COUNT 
		{
			get { return arrNode.Count; }
		}

		public ArrayList getNodes() 
		{
			return arrNode;
		}
	}
}
