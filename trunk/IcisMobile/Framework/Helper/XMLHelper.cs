using System;
using System.Xml;
using IcisMobile.Framework.DataCollection;

namespace IcisMobile.Framework.Helper
{
	/// <summary>
	/// Summary description for XMLHelper.
	/// </summary>
	public class XMLHelper
	{
		public static Database parseSchema(String file) 
		{
			XmlTextReader reader = new XmlTextReader(file);
			
			Database database = new Database();
			Table table = new Table();
			
			try 
			{
				while(reader.Read()) 
				{
					if(reader.NodeType == XmlNodeType.Element) 
					{
						if(reader.Name.ToLower().Equals("table")) 
						{
							table = new Table();
							table.NAME = reader.GetAttribute("name");
						} 
						else if(reader.Name.ToLower().Equals("column")) 
						{
							Column col = new Column();
							for(int i = 0; i < reader.AttributeCount; i++) 
							{
								reader.MoveToAttribute(i);
								if(reader.Name.ToLower().Equals("name")) 
								{
									col.NAME = reader.Value;
								} 
								else if(reader.Name.ToLower().Equals("type")) 
								{
									col.TYPE = reader.Value;
								}
								else if(reader.Name.ToLower().Equals("length")) 
								{
									if(reader.Value.Length != 0) 
									{
										try 
										{
											Convert.ToInt16(reader.Value);
											col.LENGTH = reader.Value;
										} 
										catch(FormatException e) 
										{
											col.LENGTH = "10";
											//LogHelper.Instance().WriteLog(Resource.ErrorCode.XML_INVALID_NODE, "Table: " + table.NAME + " Column: " + col.NAME + " Attribute: length "  + e.Message);
											LogHelper.WriteLog(Resource.ErrorCode.XML_INVALID_NODE, "Table: " + table.NAME + " Column: " + col.NAME + " Attribute: length "  + e.Message);
										}
									}
								}
								else if(reader.Name.ToLower().Equals("primary")) 
								{
									if(reader.Value.Length != 0) 
									{
										try 
										{
											Convert.ToBoolean(reader.Value);
											col.PRIMARY = reader.Value;
										} 
										catch(FormatException e) 
										{
											col.PRIMARY = "false";
											LogHelper.WriteLog(Resource.ErrorCode.XML_INVALID_NODE, "Table: " + table.NAME + " Column: " + col.NAME + " Attribute: primary " + e.Message);
										}
									}
								}
							}
							table.AddColumn(col);
						} 
					} 
					else if(reader.NodeType == XmlNodeType.EndElement)
					{
						if(reader.Name.ToLower().Equals("table")) 
						{
							database.AddTable(table);
						}
					}
				}
			} 
			catch(XmlException e) 
			{
				LogHelper.WriteLog(Resource.ErrorCode.XML_INVALID_NODE, e.Message);
			} 
			finally 
			{
				 reader.Close();
			}
			return database;
		}
	}
}
