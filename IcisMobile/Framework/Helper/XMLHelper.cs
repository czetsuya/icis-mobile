/**
 * @author edwardpantojalegaspi
 * @since 2009.09.15
 * */

using System;
using System.Xml;
using System.Collections;

using IcisMobile.Framework.DataCollection;
using IcisMobile.Framework.DataCollection.Study;

namespace IcisMobile.Framework.Helper
{
	/// <summary>
	/// Summary description for XMLHelper.
	/// </summary>
	public class XMLHelper
	{
		private static XMLHelper instance = null;

		public static XMLHelper Instance() 
		{
			if(instance == null) 
			{
				instance = new XMLHelper();
			}
			return instance;
		}

		#region Read Schema
		public Database readSchema(String file) 
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
						if(reader.Name.ToLower().Equals("constraint")) 
						{
							database.AddConstraint(reader.ReadInnerXml());
						} 
						else if(reader.Name.ToLower().Equals("table")) 
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
								else if(reader.Name.ToLower().Equals("null")) 
								{
									if(reader.Value.Length != 0) 
									{
										try 
										{
											Convert.ToBoolean(reader.Value); //check if valid bool
											col.ISNULL = reader.Value;
										} 
										catch(FormatException e) 
										{
											col.ISNULL = "false";
											LogHelper.WriteLog(ErrorCode.XML_INVALID_NODE, "Table: " + table.NAME + " Column: " + col.NAME + " Attribute: primary " + e.Message);
										}
									}
								}
								else if(reader.Name.ToLower().Equals("length")) 
								{
									if(reader.Value.Length != 0) 
									{
										try 
										{
											Convert.ToInt16(reader.Value); //check if valid int
											col.LENGTH = reader.Value;
										} 
										catch(FormatException e) 
										{
											col.LENGTH = "10";
											LogHelper.WriteLog(ErrorCode.XML_INVALID_NODE, "Table: " + table.NAME + " Column: " + col.NAME + " Attribute: length "  + e.Message);
										}
									}
								}
								else if(reader.Name.ToLower().Equals("primary")) 
								{
									if(reader.Value.Length != 0) 
									{
										try 
										{
											Convert.ToBoolean(reader.Value); //check if valid bool
											col.PRIMARY = reader.Value;
										} 
										catch(FormatException e) 
										{
											col.PRIMARY = "false";
											LogHelper.WriteLog(ErrorCode.XML_INVALID_NODE, "Table: " + table.NAME + " Column: " + col.NAME + " Attribute: primary " + e.Message);
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
				LogHelper.WriteLog(ErrorCode.XML_INVALID_NODE, e.Message);
			} 
			finally 
			{
				 reader.Close();
			}
			return database;
		}
	
		#endregion

		public Study parseStudy(String file) 
		{
			Study study = new Study();
			XmlTextReader reader = null;
			try 
			{
				reader = new XmlTextReader(file);

				while(reader.Read()) 
				{
					if(reader.NodeType == XmlNodeType.Element) 
					{
						String key = reader.Name.ToLower();

						if(key.Equals("study-name")) 
						{
							study.NAME = reader.ReadInnerXml();	
						}
						else if(key.Equals("study-title")) 
						{
							study.TITLE = reader.ReadInnerXml();
						}
						else if(key.Equals("study-start-date")) 
						{
							study.STARTDATE = reader.ReadInnerXml();
						}
						else if(key.Equals("study-end-date"))  
						{
							study.ENDDATE = reader.ReadInnerXml();
						}
						else if(key.Equals("factors"))
						{
							study.SetFactors(GetFactors(reader));
						}
						else if(key.Equals("variates"))
						{
							study.SetVariates(GetVariates(reader));
						}
						else if(key.Equals("scales"))
						{
							study.SetScales(GetScales(reader));
						}
					}
				}
			} 
			catch(Exception e) 
			{
				LogHelper.WriteLog(ErrorCode.XML_STUDY_LOAD_FAILED, e.Message);
			}
			finally 
			{
				if(reader != null) 
				{
					reader.Close();
				}
			}
			return study;
		}

		
		private ArrayList GetFactors(XmlTextReader reader) 
		{
			ArrayList factors = new ArrayList();

			Factor factor = new Factor();
			while(reader.Read()) 
			{ //loop to all factors
				if(reader.NodeType == XmlNodeType.Element) 
				{
					String key = reader.Name.ToLower();

					if(key.Equals("factor")) 
					{
                        factor = new Factor();
					} 
					else if(key.Equals("name"))
					{
                        factor.NAME = reader.ReadInnerXml();
					}
					else if(key.Equals("property")) 
					{
						factor.PROPERTY = reader.ReadInnerXml();
					}
					else if(key.Equals("scale")) 
					{
						factor.SCALE = reader.ReadInnerXml();
					}
					else if(key.Equals("method")) 
					{
						factor.METHOD = reader.ReadInnerXml();
					}
					else if(key.Equals("data-type")) 
					{
						factor.DATATYPE = reader.ReadInnerXml();
					}
					else if(key.Equals("scaleid")) 
					{
						factor.SCALEID = reader.ReadInnerXml();
					}
				}
				else if(reader.NodeType == XmlNodeType.EndElement) 
				{	
					if(reader.Name.ToLower().Equals("factor")) 
					{
						factors.Add(factor);
					} 
					else if(reader.Name.ToLower().Equals("factors")) 
					{
						break;
					}
				}
			}
			return factors;
		}

		
		private ArrayList GetVariates(XmlTextReader reader) 
		{
			ArrayList variates = new ArrayList();

			Variate variate = new Variate();
			while(reader.Read()) 
			{ //loop to all variates
				if(reader.NodeType == XmlNodeType.Element) 
				{
					String key = reader.Name.ToLower();

					if(key.Equals("variate")) 
					{
						variate = new Variate();
					} 
					else if(key.Equals("name"))
					{
						variate.NAME = reader.ReadInnerXml();
					}
					else if(key.Equals("property")) 
					{
						variate.PROPERTY = reader.ReadInnerXml();
					}
					else if(key.Equals("scale")) 
					{
						variate.SCALE = reader.ReadInnerXml();
					}
					else if(key.Equals("method")) 
					{
						variate.METHOD = reader.ReadInnerXml();
					}
					else if(key.Equals("data-type")) 
					{
						variate.DATATYPE = reader.ReadInnerXml();
					}
					else if(key.Equals("scaleid")) 
					{
						variate.SCALEID = reader.ReadInnerXml();
					}
				}
				else if(reader.NodeType == XmlNodeType.EndElement) 
				{
					if(reader.Name.ToLower().Equals("variate")) 
					{
						variates.Add(variate);
					} 
					else if(reader.Name.ToLower().Equals("variates")) 
					{
						 break;
					}
				}
			}
			return variates;
		}

		
		private ArrayList GetScales(XmlTextReader reader) 
		{
			ArrayList arrTemp = new ArrayList();
			
			Scale scale = new Scale();
			while(reader.Read()) 
			{
				if(reader.NodeType == XmlNodeType.Element) 
				{
					String key = reader.Name.ToLower();

					if(key.Equals("scale")) 
					{
						scale = new Scale();
					}
					else if(key.Equals("id"))
					{
						scale.ID = reader.ReadInnerXml();
					}
					else if(key.Equals("name"))
					{
						scale.NAME = reader.ReadInnerXml();
					}
					else if(key.Equals("type")) 
					{
						scale.TYPE = reader.ReadInnerXml();
					}
					else if(key.Equals("value1")) 
					{
						scale.VALUE1 = reader.ReadInnerXml();
					}
					else if(key.Equals("value2")) 
					{
						scale.VALUE2 = reader.ReadInnerXml();
					}
					else if(key.Equals("value3")) 
					{
						scale.VALUE3 = reader.ReadInnerXml();
					}
				} 
				else if(reader.NodeType == XmlNodeType.EndElement) 
				{
					if(reader.Name.ToLower().Equals("scale")) 
					{
						arrTemp.Add(scale);
					} 
					else if(reader.Name.ToLower().Equals("scales")) 
					{
						break;
					}
				}
			}
			return arrTemp;
		}

		public void parseStudyData(String file) 
		{

		}
	}
}
