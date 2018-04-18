using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.Configuration;
using Cms.DataLayer;

namespace Cms.BusinessLayer.BlCommon
{
	/// <Author>Deepak Gupta</Author>
	/// <Dated>Aug-29-2006</Date>
	/// <Purpose>To Build Query Xml for the Template and to save in database</Purpose>
	public class ClsBuildQueryXml: Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		private XmlDocument QueryXmlDoc=new XmlDocument();
		const string NodeType = "NodeType";
		const string dbName = "dbName";
		const string TableId = "ID";
		const string FromClause = "Table";
		const string SPClause = "SP";
		const string WhereClause = "Where";
		const string relationShip = "Relation";
		const string ParamType = "type";
		const string GenericField = "1";
		const string DatabaseField = "2";
		const string SearchTillRootNodeName = "BRICS";
		const string TableNodeType = "T";
		const string HiddenParamFieldType = "H";
		const string DateField = "#";
		const string DateTimeField = "@";
		const string CurrencyField = "$";
		const string TimeField = "~";
		const string NumericField = "|";

		private string Template_Id;
		private string FieldXMLPath;
		//private string MarginXML;
		private string TemplatePath="";

		//public ClsBuildQueryXml(string strTemplateId,string strFieldXMLPath,string strMarginXML,string strTemplatePath)
		public ClsBuildQueryXml(string strTemplateId,string strFieldXMLPath,string strTemplatePath)
		{
			Template_Id = strTemplateId;
			FieldXMLPath = strFieldXMLPath;
			//MarginXML = strMarginXML;
			TemplatePath = strTemplatePath;
		}

		//This function is used traverse the field’s array which are dropped on template.
		public void CreateQueryXML()
		{
			XmlDocument objFieldXml = new XmlDocument();
			XmlNode FieldNode;
			XmlNode Node;
			XmlElement GenericElement;

			XmlDocument objUserDefFieldXml = new XmlDocument();
			XmlNode UserDefFieldNode;

			string FieldName;
			string AliasName;
			string fieldType;
			//geting array of fields
			Array lstrFieldArray = GetFieldsArray();
			
			objFieldXml.Load(FieldXMLPath + "\\" + "W$18.xml");
			FieldNode = objFieldXml.SelectSingleNode("//brics/brics_client");
			UserDefFieldNode = null;
				
			//objUserDefFieldXml.Load(FieldXMLPath + "\\" + "W$18T.xml");
			//UserDefFieldNode = objUserDefFieldXml.SelectSingleNode("//brics/brics_client//brics/brics_client");

			QueryXmlDoc.LoadXml("<sql TemplateId=\"" + Template_Id + "\"><generic NodeType=\"N\"></generic><sqlquery></sqlquery></sql>");
			
			//traversing fields array
			foreach (string lstrField in lstrFieldArray)
			{	
				switch(GoAhead(lstrField).ToString().Trim())
				{
					case GenericField: 
						//node type generic
						Node = QueryXmlDoc.SelectSingleNode("//sql/generic");
						GenericElement = QueryXmlDoc.CreateElement(lstrField);
						GenericElement.SetAttribute("NodeType", "D");
						Node.AppendChild(GenericElement);
						Node=null;
						GenericElement=null;
						break;
					case DatabaseField:
						//node type database
						if (lstrField.Trim().ToUpper().IndexOf("USERDEF_") >= 0)
							Node = UserDefFieldNode.SelectSingleNode("//" + lstrField);
						else
							Node = FieldNode.SelectSingleNode("//" + lstrField);								

						if (Node != null)
						{
							FieldName = GetAttributeValue(Node, dbName);
							AliasName = Node.Name;
							fieldType = GetAttributeValue(Node, NodeType);
						
							AddQuery2QueryXml(FieldName, AliasName, fieldType, Node.ParentNode);
						}
						break;
				}
			}
			//string SqlQuery = "UPDATE DOC_TEMPLATE_LIST SET MARGINS = '" + MarginXML + "',LAST_MODIFIED_DATE='" + System.DateTime.Now.ToString() + "',  QUERYXML = '" + "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + QueryXmlDoc.OuterXml.Replace("'","''") + "' WHERE TEMPLATE_ID=" + Template_Id;
			string SqlQuery = "UPDATE DOC_TEMPLATE_LIST SET LAST_MODIFIED_DATE='" + System.DateTime.Now.ToString() + "',  QUERYXML = '" + "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + QueryXmlDoc.OuterXml.Replace("'","''") + "' WHERE TEMPLATE_ID=" + Template_Id;
			DataLayer.DataWrapper.ExecuteNonQuery(ClsCommon.ConnStr,System.Data.CommandType.Text,SqlQuery);

			//Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + QueryXmlDoc.OuterXml);
			QueryXmlDoc.RemoveAll();
			QueryXmlDoc=null;
			objUserDefFieldXml.RemoveAll();
			objUserDefFieldXml=null;
		}
		//used to check the generic and group or class fields 
		private string GoAhead(string strFieldName)
		{
			if (strFieldName.Substring(strFieldName.Length-5,5)== "Start" || strFieldName.Substring(strFieldName.Length-3,3)== "End" || strFieldName.IndexOf("TemplateClass")>0)
				return "0";
			else if ("current_date,current_time,server_timezone".IndexOf(strFieldName) >= 0)
				return GenericField;
			else
				return DatabaseField;
		}
		//This function is used to read & fetch the field names from the template and will put into a string array. 
		private Array GetFieldsArray()
		{
			string rtfContent="";
			string TempRtfContent="";
			
			int lIntSelectedLength;
			int lIntPos;
			int posStart;
			int posEnd;

			ArrayList lstrFieldArray = new ArrayList();
	
			FileStream FSO = new FileStream(TemplatePath,FileMode.Open,FileAccess.Read);
			StreamReader SR = new StreamReader(FSO);
			rtfContent = SR.ReadToEnd();
			TempRtfContent = rtfContent;

			while (rtfContent.Trim().Length > 0)
			{
				lIntPos = rtfContent.IndexOf("<<");
				posEnd = rtfContent.IndexOf(">>");
				posStart = lIntPos;
				posStart = posStart + "<<".Length;
				lIntSelectedLength = posEnd - posStart;
				if (lIntSelectedLength > 0)
				{   
					if (! lstrFieldArray.Contains(rtfContent.Substring(posStart, lIntSelectedLength)))
					{	
						lstrFieldArray.Add(rtfContent.Substring(posStart, lIntSelectedLength));
					}
					rtfContent = rtfContent.Substring(posEnd+2);
				}
				else
				{
					rtfContent = "";
				}
				SR.Close();
				FSO.Close();
			}
			rtfContent = TempRtfContent;
			while (rtfContent.Trim().Length > 0)
			{
				lIntPos = rtfContent.IndexOf("<" + DateField);
				posEnd = rtfContent.IndexOf(DateField + ">");
				posStart = lIntPos;
				posStart = posStart + ("<" + DateField).Length;
				lIntSelectedLength = posEnd - posStart;
				if (lIntSelectedLength > 0)
				{   
					if (! lstrFieldArray.Contains(rtfContent.Substring(posStart, lIntSelectedLength)))
					{	
						lstrFieldArray.Add(rtfContent.Substring(posStart, lIntSelectedLength));
					}
					rtfContent = rtfContent.Substring(posEnd+2);
				}
				else
				{
					rtfContent = "";
				}
				SR.Close();
				FSO.Close();
			}
			rtfContent = TempRtfContent;
			while (rtfContent.Trim().Length > 0)
			{
				lIntPos = rtfContent.IndexOf("<" + DateTimeField);
				posEnd = rtfContent.IndexOf(DateTimeField + ">");
				posStart = lIntPos;
				posStart = posStart + ("<" + DateTimeField).Length;
				lIntSelectedLength = posEnd - posStart;
				if (lIntSelectedLength > 0)
				{   
					if (! lstrFieldArray.Contains(rtfContent.Substring(posStart, lIntSelectedLength)))
					{	
						lstrFieldArray.Add(rtfContent.Substring(posStart, lIntSelectedLength));
					}
					rtfContent = rtfContent.Substring(posEnd+2);
				}
				else
				{
					rtfContent = "";
				}
				SR.Close();
				FSO.Close();
			}
			rtfContent = TempRtfContent;
			while (rtfContent.Trim().Length > 0)
			{
				lIntPos = rtfContent.IndexOf("<" + CurrencyField);
				posEnd = rtfContent.IndexOf(CurrencyField + ">");
				posStart = lIntPos;
				posStart = posStart + ("<" + CurrencyField).Length;
				lIntSelectedLength = posEnd - posStart;
				if (lIntSelectedLength > 0)
				{   
					if (! lstrFieldArray.Contains(rtfContent.Substring(posStart, lIntSelectedLength)))
					{	
						lstrFieldArray.Add(rtfContent.Substring(posStart, lIntSelectedLength));
					}
					rtfContent = rtfContent.Substring(posEnd+2);
				}
				else
				{
					rtfContent = "";
				}
				SR.Close();
				FSO.Close();
			}
			rtfContent = TempRtfContent;
			while (rtfContent.Trim().Length > 0)
			{
				lIntPos = rtfContent.IndexOf("<" + TimeField);
				posEnd = rtfContent.IndexOf(TimeField + ">");
				posStart = lIntPos;
				posStart = posStart + ("<" + TimeField).Length;
				lIntSelectedLength = posEnd - posStart;
				if (lIntSelectedLength > 0)
				{   
					if (! lstrFieldArray.Contains(rtfContent.Substring(posStart, lIntSelectedLength)))
					{	
						lstrFieldArray.Add(rtfContent.Substring(posStart, lIntSelectedLength));
					}
					rtfContent = rtfContent.Substring(posEnd+2);
				}
				else
				{
					rtfContent = "";
				}
				SR.Close();
				FSO.Close();
			}
			rtfContent = TempRtfContent;
			while (rtfContent.Trim().Length > 0)
			{
				lIntPos = rtfContent.IndexOf("<" + NumericField);
				posEnd = rtfContent.IndexOf(NumericField + ">");
				posStart = lIntPos;
				posStart = posStart + ("<" + NumericField).Length;
				lIntSelectedLength = posEnd - posStart;
				if (lIntSelectedLength > 0)
				{   
					if (! lstrFieldArray.Contains(rtfContent.Substring(posStart, lIntSelectedLength)))
					{	
						lstrFieldArray.Add(rtfContent.Substring(posStart, lIntSelectedLength));
					}
					rtfContent = rtfContent.Substring(posEnd+2);
				}
				else
				{
					rtfContent = "";
				}
				SR.Close();
				FSO.Close();
			}
			return lstrFieldArray.ToArray(typeof(System.String));
		}
		//fetching the attribute value of a node 
		private string GetAttributeValue(XmlNode Node, string AttributeName)
		{
			for (int AttributeCounter = 0; AttributeCounter < Node.Attributes.Count;AttributeCounter++)
			{
				XmlAttribute XmlAttrib= Node.Attributes[AttributeCounter];
				if (XmlAttrib.Name.ToUpper() == AttributeName.ToUpper())
				{	
					return XmlAttrib.Value;
				}
			}
			return "";
		}
		//This function is used to check the query existence in the query xml. 
		//If it will find the query in query xml then it will append the field name, 
		//alias name and field type into the query xml. Else it will call the 
		//BuildQueryGroup function to built the complete hierarchy of queries into the query xml.
		private void AddQuery2QueryXml(string FieldName,string AliasName,string fieldType,XmlNode MotherNode)
		{
			XmlNode QueryNode;
			XmlNode fields;
			XmlNode onTemplatefields;
			XmlNode Types;
			
			while (MotherNode.Name.ToUpper() != SearchTillRootNodeName)
			{
				if (MotherNode !=null)
				{
					//checking node type "Table"
					if (GetAttributeValue(MotherNode, NodeType).Substring(0,1).ToUpper() == TableNodeType)
					{	//checking availability on query in generated query xml.
						QueryNode = QueryXmlDoc.SelectSingleNode("//query[@id='" + GetAttributeValue(MotherNode, TableId) + "']");
						if (QueryNode!=null)
						{	//found -- 
							//now it is fetching fields,fieldtype,ontemplatefieldname 
							//to add the new field to the existing query
							fields = QueryXmlDoc.SelectSingleNode("//query[@id='" + GetAttributeValue(MotherNode, TableId) + "']/select/fields");
							Types = QueryXmlDoc.SelectSingleNode("//query[@id='" + GetAttributeValue(MotherNode, TableId) + "']/select/fieldType");
							onTemplatefields = QueryXmlDoc.SelectSingleNode("//query[@id='" + GetAttributeValue(MotherNode, TableId) + "']/select/ontemplateFieldname");
                    
							if (fields.InnerText.Trim() == "")
							{
								fields.InnerText = FieldName;
								onTemplatefields.InnerText = AliasName;
								Types.InnerText = fieldType;
							}
							else
							{
								fields.InnerText = fields.InnerText + "," + FieldName;
								onTemplatefields.InnerText = onTemplatefields.InnerText + "," + AliasName;
								Types.InnerText = Types.InnerText + "," + fieldType;
							}
							QueryNode = null;
							MotherNode=null;
							fields=null;
							onTemplatefields=null;
							Types=null;
							break;
						}
						else
						{	//query not found in query xml so it is calling this function to build the query hierarchy.
							BuildQueryGroup(FieldName, AliasName, fieldType, MotherNode);
							MotherNode=null;
							break;
						}
					}
					else
					{
						MotherNode = MotherNode.ParentNode;
					}
				}
				else
				{
					MotherNode=null;
					break;
				}
			}
		}
		//This function will be used to build the hierarchy of the query in to the query xml.
		private void BuildQueryGroup(string FieldName,string AliasName,string fieldType, XmlNode MotherNode)
		{
			XmlNode ParentXMLNode;
			XmlNode	ChildXMLNode;
			XmlNode	QueryNode;
			XmlNode Sql;
			XmlDocument TempXMLDoc = new XmlDocument();
			XmlNode TempSQL;
			
			TempXMLDoc.LoadXml("<sql></sql>");
			TempSQL = TempXMLDoc.SelectSingleNode("sql");
			//building query envelop of a single query with query id 1
			ChildXMLNode = BuildQueryEnvelop(FieldName, AliasName, fieldType, MotherNode);
			
			MotherNode = MotherNode.ParentNode;
			while (MotherNode.Name.ToUpper() != SearchTillRootNodeName)
			{
				if (GetAttributeValue(MotherNode, NodeType).Substring(0,1).ToUpper() == TableNodeType)
				{
					//checking the availability of the paretn query in the prepared query
					QueryNode = QueryXmlDoc.SelectSingleNode("//query[@id='" + GetAttributeValue(MotherNode, TableId) + "']");
					if (QueryNode != null)
					{
						//found -- it is now appending the child and calling 
						//AddParam2Parent to add the params of the child into itself
						QueryNode.AppendChild(ChildXMLNode);
						AddParam2Parent(QueryNode);
						return;
					}
					else
					{	
						//Not found now it is again calling program to build query xml of the parent
						ParentXMLNode = BuildQueryEnvelop("", "", "", MotherNode);
						//appending child to the parent
						ParentXMLNode.AppendChild(ChildXMLNode);
						
						//I first added this xml element to this Temporary Xml Doc
						//Because you cant serach a perticular node in the element or node
						//untill element or node is not conected to the XML Document
						//So i first added this node to the document and then i called
						//function to add params or child to parent .
						//Then i removed this element from the temporary document.
						ParentXMLNode =TempXMLDoc.ImportNode(ParentXMLNode,true);
						TempSQL.AppendChild(ParentXMLNode);
						//adding params of child to the parent
						AddParam2Parent(ParentXMLNode);
						TempSQL.RemoveChild(ParentXMLNode);
						ParentXMLNode=QueryXmlDoc.ImportNode(ParentXMLNode,true);

						ChildXMLNode = ParentXMLNode;
						MotherNode = MotherNode.ParentNode;
					}
				}
				else
				{
					MotherNode = MotherNode.ParentNode;
				}
			}
			Sql = QueryXmlDoc.SelectSingleNode("//sql/sqlquery");
			Sql.AppendChild(ChildXMLNode);
			ParentXMLNode=null;
			ChildXMLNode=null;
			TempSQL=null;
			TempXMLDoc=null;
		}
		//This function is used to build the query node or envelop for a specific table.
		private XmlNode BuildQueryEnvelop(string FieldName, string AliasName, string fieldType, XmlNode MotherNode)
		{
			XmlDocument TempXMLDoc = new XmlDocument();
			XmlNode Node;
			string QueryEnvelop;
			XmlNode Sql;

			QueryEnvelop = "<sql>" + 
				"<query id=\"" +  GetAttributeValue(MotherNode, TableId) + "\">" +
				"<ontemplate></ontemplate>" +
				"<relation>1</relation>" +
				"<select>" +
				"<ontemplateGroupname></ontemplateGroupname>" +
				"<ontemplateFieldname></ontemplateFieldname>" +
				"<fieldType></fieldType>" +
				"<fields></fields>" +
				"<from></from>" +
				"<sp></sp>" +
				"<where></where>" +
				"<params></params>" +
				"</select>" +
				"</query>" +
				"</sql>";
			TempXMLDoc.LoadXml(QueryEnvelop);
			
			Node = TempXMLDoc.SelectSingleNode("//sql/query/ontemplate");
			Node.InnerText = MotherNode.Name;
			Node = TempXMLDoc.SelectSingleNode("//sql/query/relation");
			Node.InnerText = GetAttributeValue(MotherNode, relationShip);
			
			Node = TempXMLDoc.SelectSingleNode("//sql/query/select/ontemplateGroupname");
			if (GetAttributeValue(MotherNode, relationShip).ToUpper() == "M")
				Node.InnerText = MotherNode.SelectNodes("//" + MotherNode.Name + "/fields/*").Item(0).Name;
			else
				Node.InnerText = "";
						
			Node = TempXMLDoc.SelectSingleNode("//sql/query/select/ontemplateFieldname");
			Node.InnerText = AliasName;
			Node = TempXMLDoc.SelectSingleNode("//sql/query/select/fieldType");
			Node.InnerText = fieldType;
			Node = TempXMLDoc.SelectSingleNode("//sql/query/select/fields");
			Node.InnerText = FieldName;
			Node = TempXMLDoc.SelectSingleNode("//sql/query/select/from");
			Node.InnerText = GetAttributeValue(MotherNode, FromClause);
			Node = TempXMLDoc.SelectSingleNode("//sql/query/select/sp");
			Node.InnerText = GetAttributeValue(MotherNode, SPClause);
			Node = TempXMLDoc.SelectSingleNode("//sql/query/select/where");
			Node.InnerText = GetAttributeValue(MotherNode, WhereClause);
			
			Node = TempXMLDoc.SelectSingleNode("//sql/query/select/params");
			Node.InnerXml=MotherNode.SelectSingleNode("//" + MotherNode.Name + "/params").InnerXml;
			
			Node = TempXMLDoc.SelectSingleNode("//sql/query");
			Sql = TempXMLDoc.SelectSingleNode("//sql");
			Sql.RemoveChild(Node);
			
			Node = QueryXmlDoc.ImportNode(Node, true);
			TempXMLDoc=null;
			return Node;
		}
		//This function used to add praram type "h" (id field like Vehicle_id) to the parent's field's node.
		private void AddParam2Parent(XmlNode ParentXMLNode)
		{
			XmlNodeList Nodes;
			XmlNode FieldNode;
			XmlNode fieldType;
			XmlNode ontemplateFieldname;
			//getting fields,fieldType and onTemplateFieldName nodes of parent
			FieldNode = ParentXMLNode.SelectSingleNode("//query[@id='" + GetAttributeValue(ParentXMLNode, TableId) + "']/select/fields");
			fieldType = ParentXMLNode.SelectSingleNode("//query[@id='" + GetAttributeValue(ParentXMLNode, TableId) + "']/select/fieldType");
			ontemplateFieldname = ParentXMLNode.SelectSingleNode("//query[@id='" + GetAttributeValue(ParentXMLNode, TableId) + "']/select/ontemplateFieldname");
			//fetching params of child
			Nodes = ParentXMLNode.SelectNodes("//query[@id='" + GetAttributeValue(ParentXMLNode, TableId) + "']/query/select/params/*");
			foreach (XmlNode Node in Nodes)
			{	//checing param type of childs params
				if (GetAttributeValue(Node, ParamType).ToUpper() == HiddenParamFieldType)
				{
					if (FieldNode.InnerText.IndexOf(GetAttributeValue(Node, dbName) + " " + Node.Name) < 0)
					{
						if (FieldNode.InnerText.Trim() == "")
						{
							FieldNode.InnerText = GetAttributeValue(Node, dbName) + " " + Node.Name;
							fieldType.InnerText = HiddenParamFieldType;
							ontemplateFieldname.InnerText= Node.Name;
						}
						else
						{
							FieldNode.InnerText = FieldNode.InnerText + "," + GetAttributeValue(Node, dbName) + " " + Node.Name;
							fieldType.InnerText = fieldType.InnerText + "," + HiddenParamFieldType;
							ontemplateFieldname.InnerText = ontemplateFieldname.InnerText + "," + Node.Name;
						}
					}
				}
			}
			Nodes=null;
			FieldNode =null;
			fieldType =null;
			ontemplateFieldname =null;
		}
	}
}
