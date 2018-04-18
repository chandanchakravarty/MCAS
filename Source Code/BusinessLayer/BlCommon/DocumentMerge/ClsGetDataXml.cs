using System;
using System.Data;
//using System.Collections;
//using System.Configuration;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using Cms.DataLayer;

namespace Cms.BusinessLayer.BlCommon
{
	/// <Author>Deepak Gupta</Author>
	/// <Dated>Aug-29-2006</Date>
	/// <Purpose>To Build Data Xml for the Client/Application/Policy against QueryXML stored in database.</Purpose>
	public class ClsGetDataXml: Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		#region Declarations
		//Basic parameters
		private string Merge_Id;
		private string Merge_Ids;
		//Local variables
		private string PrintLblEnvp;
		private string EnvelopeReturnAddress;
		private string PrintAddress;
		private string LblEnvpId;
		private string Template_Id;
		private string Template_Name;
		private string Template_Type;
		private string Client_Id;
		private string Policy_Id;
		private string Policy_Version_Id;
		private string App_Id;
		private string App_Version_Id;
		private string User_Id;
		private string Check_Id;  //Modified 1 June 2007 :Praveen Kasana :Added Check ID]
		private string Applicant_Id;
		private string Claim_Id;
		private string Party_Id;
		private int intRecordCount=0;
		private XmlDocument DataXmlDoc=new XmlDocument();
		//Constants
		const string MergeId = "MergeId";
		const string TotalPrintOuts="total_printouts";
		const string RecordId="id";
		const string TemplateID = "TemplateID";
		const string DisplayName = "DisplayName";
		const string RecordsDataXpath="//mergedata/records";
		const string GenericQueryXpath="//sql/generic";
		const string SqlClientQueryXpath="//sql/sqlquery/*";
		const string InnerQueryXpath="/query";
		const string OnTemplateXpath="/ontemplate";
		const string RelationsXpath="/relation";
		const string SelectXpath="/select";
		const string SPXpath = "/sp";
		const string FieldsXpath = "/fields";
		const string FromXpath = "/from";
		const string WhereXpath = "/where";
		const string onTemplateGroupXpath = "/ontemplateGroupname";
		const string onTemplateFieldXpath = "/ontemplateFieldname";
		const string FieldTypeXpath = "/fieldType";
		const string ParamsXpath = "/params/*";
		const string GroupNodeType="M";
		const string NormalNodeType="N";
		const string FieldNodeType="F";
		const string ClassNodeType="C";
		const string NodeType="NodeType";
		const string TagName="TagName";
		const string HideClass="HideClass";
		//const string ClientTemplateClass="clientTemplateClass";
		//const string BrokerTemplateClass="brokerTemplateClass";
		//const string CarrierTemplateClass="carrierTemplateClass";
		const string ASPPageParam="U";
		const string DataBaseParamType="H";
		const string DataType="DataType";
		const string NumericDataType="N";
		const string ParamTypeAttributeName="TYPE";
		const string Envelopes = "envelopes";
		const string Labels = "labels";
		const string CLAIM_LETTER_TYPE = "11200";
		#endregion

		public ClsGetDataXml(string strMergeId)
		{
			Merge_Ids = strMergeId;
		}
		
		public string CreateDataXML()
		{
			string strSQL;
			DataSet dsSndLtr = new DataSet();

			string lstrDataXml="<mergedata>" +
									"<records total_printouts=\"0\" documents=\"0\" envelopes=\"0\" labels=\"0\">" +
										"<labels></labels>" +
										"<envelopes></envelopes>" +
									"</records>" + 
								"</mergedata>";
			DataXmlDoc.LoadXml(lstrDataXml);
			
			if (Merge_Ids.Trim() != "")
			{
				strSQL="Proc_DOC_GetTemplateMergeInfo '" + Merge_Ids.Trim().ToUpper() + "'";
				dsSndLtr=DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,CommandType.Text,strSQL);
				
				foreach (DataRow Row in dsSndLtr.Tables[0].Rows)
				{
					PrintLblEnvp=Row["PRINT_LBL_ENVP"].ToString().Trim();
					LblEnvpId=Row["LBL_ENVP_ID"].ToString().Trim();
					PrintAddress=Row["PRINT_ADDRESS"].ToString().Trim();
					EnvelopeReturnAddress=Row["RETURN_ADDRESS"].ToString().Trim();
					
					Template_Id =Row["TEMPLATE_ID"].ToString().Trim();
					Merge_Id =Row["MERGE_ID"].ToString().Trim();
					Template_Name=Row["DISPLAYNAME"].ToString().Trim();
					Template_Type = "";	
					if(Row["LETTERTYPE"] != DBNull.Value)
						Template_Type = Row["LETTERTYPE"].ToString().Trim(); //TO ELIMINATE COLUMN AMBIGUITY FOR CLAIM MERGE TYPE

					
					if (Row["CLIENT_ID"] != DBNull.Value)
						Client_Id=Row["CLIENT_ID"].ToString().Trim();	
					else
						Client_Id="0";
					
					if (Row["POLICY_ID"] != DBNull.Value && Row["POLICY_ID"].ToString().Trim() != "0")
						Policy_Id=Row["POLICY_ID"].ToString().Trim();	
					else
					{
						if(Template_Type!=CLAIM_LETTER_TYPE)
                            Policy_Id="0 OR POLICY_ID IS NOT NULL";
						else
							Policy_Id="0";
					}

					if (Row["POLICY_VERSION_ID"] != DBNull.Value && Row["POLICY_VERSION_ID"].ToString().Trim() != "0")
						Policy_Version_Id=Row["POLICY_VERSION_ID"].ToString().Trim();	
					else
					{
						if(Template_Type!=CLAIM_LETTER_TYPE)
                            Policy_Version_Id="0 OR POLICY_VERSION_ID IS NOT NULL";
						else
							Policy_Version_Id="0";
					}

					if (Row["APP_ID"] != DBNull.Value && Row["APP_ID"].ToString().Trim() != "0")
						App_Id=Row["APP_ID"].ToString().Trim();	
					else
						App_Id="0 OR APP_ID IS NOT NULL";

					if (Row["APP_VERSION_ID"] != DBNull.Value && Row["APP_VERSION_ID"].ToString().Trim() != "0")
						App_Version_Id=Row["APP_VERSION_ID"].ToString().Trim();	
					else
						App_Version_Id="0 OR APP_VERSION_ID IS NOT NULL";

					if (Row["USER_ID"] != DBNull.Value)
						User_Id=Row["USER_ID"].ToString().Trim();	
					
					 //Modified 1 June 2007 :Praveen Kasana :Added Check ID
					if (Row["CHECK_ID"] !=DBNull.Value)
						Check_Id = Row["CHECK_ID"].ToString().Trim();
					else
						Check_Id = "0";
					 //Modified 25 sep 2008 :Praveen Kasana:
					if (Row["APPLICANT_ID"] !=DBNull.Value)
						Applicant_Id = Row["APPLICANT_ID"].ToString().Trim();
					else
						Applicant_Id = "0";

					//Claim parties
					if (Row["CLAIM_ID"] !=DBNull.Value && Row["CLAIM_ID"].ToString().Trim() != "0")
						Claim_Id = Row["CLAIM_ID"].ToString().Trim();
					else
						Claim_Id = "0";

					if (Row["PARTY_ID"] !=DBNull.Value && Row["PARTY_ID"].ToString().Trim() != "0")
						Party_Id = Row["PARTY_ID"].ToString().Trim();
					else
						Party_Id = "0";

					
					GetQueryXmlAndCreateDataXml();
				}
			}

			strSQL="Proc_DOC_UpdateMergeStatus '" + Merge_Ids.Trim().ToUpper() + "'"; 
			DataLayer.DataWrapper.ExecuteNonQuery(ClsCommon.ConnStr,CommandType.Text,strSQL); 
			
			return("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + DataXmlDoc.OuterXml.Replace("�","-"));
			DataXmlDoc.RemoveAll();
			DataXmlDoc=null;
		}
		
		//used to get queryxml for specific templateid and to start processing for creation of dataxml
		//also it checks for the package
		private void GetQueryXmlAndCreateDataXml()
		{
			XmlNode Node;
			XmlDocument QueryXmlDoc=new XmlDocument();
			string strSQL;
			DataSet DS;

			intRecordCount++;

			strSQL = "Proc_DOC_GetTemplateInformation " + Template_Id;
			DS = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,CommandType.Text,strSQL);
			
			if (DS.Tables[0].Rows.Count > 0)
				QueryXmlDoc.LoadXml(DS.Tables[0].Rows[0]["QUERYXML"].ToString());

			Node = QueryXmlDoc.SelectSingleNode("//sql");
			CreateRecordInDataXml(Node,intRecordCount.ToString(),GetAttributeValue(Node,TemplateID));

			DS=null;
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

		//Setting attriute value
		private void SetAttributeValue(XmlNode Node, string AttributeName,string AttributeValue)
		{
			for (int AttributeCounter = 0; AttributeCounter < Node.Attributes.Count;AttributeCounter++)
			{
				XmlAttribute XmlAttrib= Node.Attributes[AttributeCounter];
				if (XmlAttrib.Name.ToUpper() == AttributeName.ToUpper())
				{	
					XmlAttrib.Value=AttributeValue;
					return;
				}
			}
		}

		private void CreateRecordInDataXml(XmlNode Node,string RecordCount,string lstrTemplateID)
		{
			XmlElement Record;
			XmlElement Data;
			XmlNode Records;
			XmlNode Generic;
			XmlElement ClientNode;
			//XmlElement Classes;
			//string ClassXml;
			//setting records count
			Records=DataXmlDoc.SelectSingleNode(RecordsDataXpath);
			SetAttributeValue(Records,TotalPrintOuts,RecordCount);
			
			//creating record element and setting record id
			Record=DataXmlDoc.CreateElement("record");
			Record.SetAttribute(RecordId,RecordCount);
			Record.SetAttribute(TemplateID,lstrTemplateID);
			Record.SetAttribute(MergeId,Merge_Id);
			Record.SetAttribute(MergeId,Merge_Id);
			Record.SetAttribute(DisplayName,Template_Name);
			
			//Getting The Label Envelop Data
			Record.InnerXml = GetLabelEnvelopInformation(RecordCount.ToString());
			int LblEnvpCount;
			if (PrintLblEnvp=="2")
			{
				LblEnvpCount=int.Parse(GetAttributeValue(Records,Labels));
				LblEnvpCount++;
				SetAttributeValue(Records,Labels,LblEnvpCount.ToString());
			}
			else if(PrintLblEnvp=="1" )
			{
				LblEnvpCount=int.Parse(GetAttributeValue(Records,Envelopes));
				LblEnvpCount++;
				SetAttributeValue(Records,Envelopes,LblEnvpCount.ToString());
			}

			Records.AppendChild(Record);
//			//Creating Class Node
//			Classes=DataXmlDoc.CreateElement("classes");
//			if (TemplateClass.Trim()=="1")
//				ClassXml="<" + ClientTemplateClass + " " + HideClass + "=\"false\" " + TagName + "=\"" + ClientTemplateClass + "\"></" + ClientTemplateClass + ">";
//			else
//				ClassXml="<" + ClientTemplateClass + " " + HideClass + "=\"true\" " + TagName + "=\"" + ClientTemplateClass + "\"></" + ClientTemplateClass + ">";
//			
//			if (TemplateClass.Trim()=="2")
//				ClassXml=ClassXml + "<" + BrokerTemplateClass + " " + HideClass + "=\"false\" " + TagName + "=\"" + BrokerTemplateClass + "\"></" + BrokerTemplateClass + ">";
//			else
//				ClassXml=ClassXml + "<" + BrokerTemplateClass + " " + HideClass + "=\"true\" " + TagName + "=\"" + BrokerTemplateClass + "\"></" + BrokerTemplateClass + ">";
//			
//			if (TemplateClass.Trim()=="3")
//				ClassXml=ClassXml + "<" + CarrierTemplateClass + " " + HideClass + "=\"false\" " + TagName + "=\"" + CarrierTemplateClass + "\"></" + CarrierTemplateClass + ">";
//			else
//				ClassXml=ClassXml + "<" + CarrierTemplateClass + " " + HideClass + "=\"true\" " + TagName + "=\"" + CarrierTemplateClass + "\"></" + CarrierTemplateClass + ">";
//
//			Classes.InnerXml=ClassXml;
//			Record.AppendChild(Classes);

			//creating data node.
			Data=DataXmlDoc.CreateElement("data");
			Record.AppendChild(Data);
			//adding generic field
			Generic = Node.SelectSingleNode(GenericQueryXpath);
			Generic=DataXmlDoc.ImportNode(Generic.Clone(),true);
			Data.AppendChild(Generic);

			//creating element for client
			ClientNode =DataXmlDoc.CreateElement("client");
			
			ClientNode.SetAttribute(NodeType,NormalNodeType);
			Data.AppendChild(ClientNode);
			Records = null;
			Record = null;
			Generic = null;
			Data=null;
			
			//calling funtion to build the data xml of specific client
			CreateDataXml(Node.SelectNodes(SqlClientQueryXpath),ClientNode, null);
			
			ClientNode=null;
		}

		//used to Creat Data Xml for a single record
		private void CreateDataXml(XmlNodeList Nodes,XmlNode DataXmlNode, DataRow ParentDataRow)
		{
			foreach(XmlNode Node in Nodes)
			{	//cloning node because if will not clone then program will search in whole document
				XmlNode SqlQueryNode=Node.CloneNode(true);
				
				XmlElement OntemplateTableName;
				string QueryRelation;
				
				//Getting OnTemplate Display Name and Relation from the Query Xml.
				OntemplateTableName=DataXmlDoc.CreateElement(SqlQueryNode.SelectSingleNode(OnTemplateXpath).InnerText);
				QueryRelation=SqlQueryNode.SelectSingleNode(RelationsXpath).InnerText;
				//Checking Multiple Occurance Relation
				if (QueryRelation.Trim().ToUpper() == GroupNodeType.ToUpper())
				{
					//True Multiple Occurance 
					//Setting NodeType "M" also Setting Tag Name
					OntemplateTableName.SetAttribute(NodeType,GroupNodeType);
					OntemplateTableName.SetAttribute(TagName,OntemplateTableName.Name);
				}
				else
				{
					//Setting NodeType "N" Normal
					OntemplateTableName.SetAttribute(NodeType,NormalNodeType);
				}
				//here we are building the Query For a specific query node
				string strSQL=BuildQuery(SqlQueryNode.SelectSingleNode(SelectXpath),ParentDataRow);
				//Executing Query
				if (strSQL.Trim()!="")
				{	
					DataSet DS = new DataSet();
					DS = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,CommandType.Text,strSQL);
					
					if (DS.Tables[0].Rows.Count>0)
					{	
						foreach (DataRow Row in DS.Tables[0].Rows)
						{
							//here we are calling BuildXml function to build the data xml of a specific data node
							OntemplateTableName.InnerXml = BuildXml(SqlQueryNode.SelectSingleNode(SelectXpath),Row) + OntemplateTableName.InnerXml;
							//here we are getting the query node which exists under the current query node.
							XmlNode InnerQueryNode = SqlQueryNode.SelectSingleNode(InnerQueryXpath);
							if (InnerQueryNode!=null)
							{
								if (OntemplateTableName.FirstChild !=null)
								{	//chicking this condition because we have 2 group all records a table into specific node
									if (GetAttributeValue(OntemplateTableName.FirstChild,NodeType).ToString().Trim().ToUpper()==NormalNodeType)
									{
										//Means Records are multiple
										CreateDataXml(SqlQueryNode.SelectNodes(InnerQueryXpath),OntemplateTableName.FirstChild,Row);
									}
									else
									{	//single record
										CreateDataXml(SqlQueryNode.SelectNodes(InnerQueryXpath),OntemplateTableName,Row);
									}
								}
								else
									CreateDataXml(SqlQueryNode.SelectNodes(InnerQueryXpath),OntemplateTableName,Row);
							}
							InnerQueryNode=null;
						}
					}
					else
					{	//here we are calling BuildBlankXml to build the xml with blank data
						OntemplateTableName.InnerXml = BuildBlankXml(SqlQueryNode.SelectSingleNode(SelectXpath)) + OntemplateTableName.InnerXml;
						
						XmlNode InnerQueryNode = SqlQueryNode.SelectSingleNode(InnerQueryXpath);
						if (InnerQueryNode!=null)
						{
							if (OntemplateTableName.FirstChild !=null)
							{	//chicking this condition because we have 2 group all records a table into specific node
								if (GetAttributeValue(OntemplateTableName.FirstChild,NodeType).ToString().Trim().ToUpper()==NormalNodeType)
									CreateDataXml(SqlQueryNode.SelectNodes(InnerQueryXpath),OntemplateTableName.FirstChild,null);
								else
									CreateDataXml(SqlQueryNode.SelectNodes(InnerQueryXpath),OntemplateTableName,null);
							}
							else
								CreateDataXml(SqlQueryNode.SelectNodes(InnerQueryXpath),OntemplateTableName,null);
						}
						InnerQueryNode=null;
					}
				}
				else
				{	//here we are calling BuildBlankXml to build the xml with blank data
					OntemplateTableName.InnerXml = BuildBlankXml(SqlQueryNode.SelectSingleNode(SelectXpath)) + OntemplateTableName.InnerXml;
					XmlNode InnerQueryNode = SqlQueryNode.SelectSingleNode(InnerQueryXpath);
					if (InnerQueryNode!=null)
					{
						if (OntemplateTableName.FirstChild !=null)
						{	//chicking this condition because we have 2 group all records a table into specific node
							if (GetAttributeValue(OntemplateTableName.FirstChild,NodeType).ToString().Trim().ToUpper()==NormalNodeType)
								CreateDataXml(SqlQueryNode.SelectNodes(InnerQueryXpath),OntemplateTableName.FirstChild,null);
							else
								CreateDataXml(SqlQueryNode.SelectNodes(InnerQueryXpath),OntemplateTableName,null);
						}
						else
							CreateDataXml(SqlQueryNode.SelectNodes(InnerQueryXpath),OntemplateTableName,null);
					}
					InnerQueryNode=null;
				}
				DataXmlNode.AppendChild(OntemplateTableName);
				OntemplateTableName=null;
				SqlQueryNode=null;
			}
		}

		//used to assemble the query from the query node
		private string BuildQuery(XmlNode Select,DataRow Row)
		{
			XmlNode SP;
			XmlNode Fields;
			XmlNode From;
			XmlNode Where;
			string strSQL;
			string strWhere="";

			Select=Select.CloneNode(true);
			SP=Select.SelectSingleNode(SPXpath);
			Fields=Select.SelectSingleNode(FieldsXpath);
			From=Select.SelectSingleNode(FromXpath);
			Where=Select.SelectSingleNode(WhereXpath);
			
			//will need to make changes here when multiple policies will come in the scene
			//also will need to make chanegs here for contact case.
			if (SP.InnerText.Trim()=="")
			{
				strSQL="Select " + Fields.InnerText + " From " + From.InnerText ;
				if (Fields.InnerText.ToString().Trim()=="")
					return "";
				if (Where.InnerText.Trim()!="")
					strWhere = " Where " + Where.InnerText;
			}
			else
			{
				strSQL=SP.InnerText;
				if (Where.InnerText.Trim()!="")
					strWhere = " " + Where.InnerText;
			}
			SP=null;
			Fields=null;
			From=null;
			Where=null;
			XmlNodeList Params;
			//getting params from the query node
			Params=Select.SelectNodes(ParamsXpath);
			foreach(XmlNode Param in Params)
			{	//code 2 replace the params in the query string
				if (GetAttributeValue(Param,ParamTypeAttributeName).ToUpper()==ASPPageParam)
				{	//these are all params which are coming from the asp page
					switch(Param.Name.Trim().ToUpper())					
					{
						case "CUTOMERID":
							strWhere=strWhere.Replace(Param.InnerText,Client_Id);
							break;
						case "APPID":
							strWhere=strWhere.Replace(Param.InnerText,App_Id);
							break;
						case "APPVERSIONID":
							strWhere=strWhere.Replace(Param.InnerText,App_Version_Id);
							break;
						case "POLICYID":
							strWhere=strWhere.Replace(Param.InnerText,Policy_Id);
							break;
						case "POLICYVERSIONID":
							strWhere=strWhere.Replace(Param.InnerText,Policy_Version_Id);
							break;
						case "USERID":
							strWhere=strWhere.Replace(Param.InnerText,User_Id);
							break;
						case "CHECKID": //Modified 1 June 2007 :Praveen Kasana :Added Check ID
							strWhere=strWhere.Replace(Param.InnerText,Check_Id);
							break;
						case "APPLICANT_ID": //Modified 22 Sep 2008 :Praveen Kasana :
							strWhere=strWhere.Replace(Param.InnerText,Applicant_Id);
							break;
						case "CLAIMID": //Modified 10 Dec 2008 :Praveen Kasana :
							strWhere=strWhere.Replace(Param.InnerText,Claim_Id);
							break;
						case "PARTYID": //Modified 10 Dec 2008 :Praveen Kasana :
							strWhere=strWhere.Replace(Param.InnerText,Party_Id);
							break;
						case "DATE_TIME":
							strWhere=strWhere.Replace(Param.InnerText,System.DateTime.Now.Date.ToString());
							break;
					}
				}
				else
				{	//param coming from parent table
					if (Row==null)
						return "";
					else
						if (GetAttributeValue(Param,DataType).Trim().ToUpper()==NumericDataType)
						{
						if (Row[Param.Name].ToString().Trim()=="")
							strWhere=strWhere.Replace(Param.InnerText,"null");
						else
						{
							strWhere=strWhere.Replace(Param.InnerText,Row[Param.Name].ToString().Trim());
							//Added For Co-Applicant
							//If No Co-Applicant is selected then Modify the Query
							//Else Merge the selected Co-Applicant
							if(Applicant_Id == "0")
							{
								if(strWhere.IndexOf("AND PAL.APPLICANT_ID") > 0)
								{
									strWhere = strWhere.Replace("AND PAL.APPLICANT_ID = 0","");
								}
							}
						}
						}
						else
							strWhere=strWhere.Replace(Param.InnerText,Row[Param.Name].ToString().Trim());
				}
			}
			//For Application Applicant
			if(Applicant_Id == "0")
			{
				if(strWhere.IndexOf("AND AAL.APPLICANT_ID = 0") > 0)
				{
					strWhere = strWhere.Replace("AND AAL.APPLICANT_ID = 0","");
				}
			}

			return strSQL + strWhere;
		}

		//used to build the DataXml Of a query Node
		private string BuildXml(XmlNode Select,DataRow Row)
		{	//cloning 
			Select = Select.CloneNode(true);
			string GroupName=Select.SelectSingleNode(onTemplateGroupXpath).InnerText.Trim();
			string[] OnTemplate=Select.SelectSingleNode(onTemplateFieldXpath).InnerText.Trim().Split(',');
			string[] FieldTypes=Select.SelectSingleNode(FieldTypeXpath).InnerText.Trim().Split(',');
			string[] Fields=Select.SelectSingleNode(FieldsXpath).InnerText.Trim().Split(',');
			
			int ArrayConunter=0;
			string strDataXml="";
			//building data nodes in the xml
			while (ArrayConunter < OnTemplate.Length)
			{
				if (FieldTypes[ArrayConunter].ToString().ToUpper().Trim()!=DataBaseParamType)
				{
					//Also will need to make a change here for class templates common fields
					if (Select.SelectSingleNode(SPXpath).InnerText.ToString().Trim()=="")
						strDataXml=strDataXml+ "<" + OnTemplate[ArrayConunter].ToString().Trim() + " " + NodeType + "=\"" + FieldTypes[ArrayConunter].ToString().ToUpper().Trim() + "\">" + RemoveJunkXmlCharacters(Row[ArrayConunter].ToString().Trim()) + "</" + OnTemplate[ArrayConunter].ToString().Trim() + ">";
					else
						strDataXml=strDataXml+ "<" + OnTemplate[ArrayConunter].ToString().Trim() + " " + NodeType + "=\"" + FieldTypes[ArrayConunter].ToString().ToUpper().Trim() + "\">" + RemoveJunkXmlCharacters(Row[Fields[ArrayConunter].ToString().Trim()].ToString().Trim()) + "</" + OnTemplate[ArrayConunter].ToString().Trim() + ">";
				}
				ArrayConunter++;
			}
			if (GroupName.Trim() =="")
			{
				return strDataXml;	
			}
			else
			{
				//returning all data nodes under the group node
				return "<" + GroupName.ToString().Trim() + " " + NodeType + "=\"" + NormalNodeType + "\">" + strDataXml + "</" + GroupName.Trim() + ">";
			}
		}

		//used to build the Empty DataXml Of a query Node
		private string BuildBlankXml(XmlNode Select)
		{
			Select = Select.CloneNode(true);
			string GroupName=Select.SelectSingleNode(onTemplateGroupXpath).InnerText.Trim();
			if (Select.SelectSingleNode(onTemplateFieldXpath).InnerText.Trim() == "")
			{
				if (GroupName.Trim() =="")
					return "";	
				else
					return "<" + GroupName.Trim() + " " + NodeType + "=\"" + NormalNodeType + "\"></" + GroupName.Trim() + ">";
			}

			string[] OnTemplate=Select.SelectSingleNode(onTemplateFieldXpath).InnerText.Trim().Split(',');
			string[] FieldTypes=Select.SelectSingleNode(FieldTypeXpath).InnerText.Trim().Split(',');

			int ArrayConunter=0;
			string strDataXml="";
			//building data nodes in the xml
			while (ArrayConunter < OnTemplate.Length)
			{
				if (FieldTypes[ArrayConunter].ToString().ToUpper().Trim()!=DataBaseParamType)
				{
					//Also will need to make a change here for class templates common fields
					strDataXml=strDataXml+ "<" + OnTemplate[ArrayConunter].ToString().Trim() + " " + NodeType + "=\"" + FieldTypes[ArrayConunter].ToString().ToUpper().Trim() + "\"></" + OnTemplate[ArrayConunter].ToString().Trim() + ">";
				}
				ArrayConunter++;
			}
			if (GroupName.Trim() =="")
			{
				return strDataXml;	
			}
			else
			{
				//returning all data nodes under the group node
				return "<" + GroupName.Trim() + " " + NodeType + "=\"" + NormalNodeType + "\">" + strDataXml + "</" + GroupName.Trim() + ">";
			}
		}

		//used to remove junk xml characters
		private string RemoveJunkXmlCharacters(string strNodeContent)
		{
			strNodeContent = strNodeContent.Replace("<","&lt;");
			strNodeContent = strNodeContent.Replace(">","&gt;");
			strNodeContent = strNodeContent.Replace("&","&amp;");
			strNodeContent = strNodeContent.Replace("'","&apos;");
			strNodeContent = strNodeContent.Replace("\"","&quot;");
			return strNodeContent;
		}

		//To Get The Complete Details For Label/Envelope
		private string GetLabelEnvelopInformation(string RecordCount)
		{
			if (PrintLblEnvp != "0")
			{
				return("");
//				string strLabelEnvelope="";
//				string returnAddress="";
//				strLabelEnvelope = "<LblEnvp Id=\"" + LblEnvpType + "\">";
//				
//				string strSQL;
//				if(TemplateClass=="1" || TemplateClass=="0")
//				{
//					strSQL="";
//					strSQL =  "if '0'='" + Named_Insured_Id + "' " +
//								"begin " +
//									"if '0'='" + Contact_Id + "' " +
//									"begin " +
//										"if '0'='" + Aka_Id + "' " +
//										"begin " +
//											"Select Case Client_Type When 'P' Then Client_Fname+' '+Client_Mname+' '+Client_Lname Else Client_Fname End as LblName,'' as LblDispName,Client_Add1 as LblAdd1,Client_Add2 as LblAdd2,Client_City as LblCity,Client_State as LblState,Client_Zip as LblZip From Client_List Where Client_Id="+ Client_Id + " " +
//										"end " +
//										"else " +
//										"begin " +
//											"Select DATA_2 LblName,Case Client_Type When 'C' Then Client_Fname Else '' End LblDispName,DATA_3 LblAdd1,DATA_4 LblAdd2,DATA_5 LblCity,DATA_6 LblState,DATA_7 LblZip FROM COMMON_ITEMS_DATA Inner Join Client_List On Client_Id = IREFERENCE_1 And ITEM_TYPE=103 Where IREFERENCE_1=" + Client_Id + " and ITEM_ID=" + Aka_Id + " " +
//										"end " +
//									"end " +
//									"else " +
//									"begin " +
//										"Select Contact_Fname+' '+Contact_Mname+' '+Contact_Lname As LblName,Case Client_Type When 'C' Then Client_Fname Else '' End LblDispName,Contact_Add1 as LblAdd1,Contact_Add2 as LblAdd2,Contact_City as LblCity,Contact_State as LblState,Contact_Zip as LblZip From Contact_List Inner Join Client_List On Client_Id = Individual_Contact_Id And Contact_Type=1 Where Individual_Contact_Id=" + Client_Id + " and Contact_Id=" + Contact_Id + " " +
//									"end " +
//								"end " +
//								"else " +
//								"begin " +
//									"SELECT NAME LblName,Case Client_Type When 'C' Then Client_Fname Else '' End LblDispName,ADDRESS1 LblAdd1,ADDRESS2 LblAdd2,CITY LblCity,STATE LblState,ZIP LblZip FROM POLICY_NAMED_INSURED PNI Inner Join Client_List CL On CL.Client_Id = PNI.Client_Id Where PNI.CLIENT_ID=" + Client_Id + " and PNI.POLICY_ID=" + Policy_Id + " AND PNI.POLICY_VER_TRACKING_ID=" + Policy_Ver_Tracking_Id + " AND NAME_ID=" + Named_Insured_Id + " " +
//								"end ";
//				}
//				else if(TemplateClass =="2")
//				{
//					strSQL =  "if '0'='" + Brok_Contact_id + "' " +
//						"begin " +
//						"Select Broker_Name as LblName,'' as LblDispName,Broker_Add1 as LblAdd1,Broker_Add2 as LblAdd2,Broker_City as LblCity,Broker_State as LblState,Broker_Zip as LblZip From Broker_List Where Broker_Id=" + Broker_Id + " " +
//						"end " +
//						"else " +
//						"begin " +
//						"Select Contact_Fname+' '+Contact_Mname+' '+Contact_Lname As LblName,Broker_Name as LblDispName,Contact_Add1 as LblAdd1,Contact_Add2 as LblAdd2,Contact_City as LblCity,Contact_State as LblState,Contact_Zip as LblZip From Contact_List Inner Join Broker_List On Broker_Id=Individual_Contact_Id And Contact_Type=4 Where Individual_Contact_Id=" + Broker_Id + " and Contact_Id=" + Brok_Contact_id + " " +
//						"end";
//				}
//				else 
//				{
//					strSQL =  "if '0'='" + Carr_Contact_id + "' " +
//						"begin " +
//						//"if exists(Select * From Contact_List Where Contact_Type=2 and Individual_Contact_Id=" + Carrier_Id + ") " +
//						//	"Select Top 1 Contact_Fname+' '+Contact_Mname+' '+Contact_Lname As LblName,Comp_Name as LblDispName,Contact_Add1 as LblAdd1,Contact_Add2 as LblAdd2,Contact_City as LblCity,Contact_State as LblState,Contact_Zip as LblZip From Contact_List Inner Join Comp_List On Comp_Id=Individual_Contact_Id And Contact_Type=2 Where Individual_Contact_Id=" + Carrier_Id + " " +
//						//"else " +
//						"Select Comp_Name as LblName,'' as LblDispName,Comp_Add1 as LblAdd1,Comp_Add2 as LblAdd2,Comp_City as LblCity,Comp_State as LblState,Comp_Zip as LblZip From Comp_List Where Comp_Id=" + Carrier_Id + " " +
//						"end " +
//						"else " +
//						"begin " +
//						"Select Contact_Fname+' '+Contact_Mname+' '+Contact_Lname As LblName,Comp_Name as LblDispName,Contact_Add1 as LblAdd1,Contact_Add2 as LblAdd2,Contact_City as LblCity,Contact_State as LblState,Contact_Zip as LblZip From Contact_List Inner Join Comp_List On Comp_Id=Individual_Contact_Id And Contact_Type=2 Where Individual_Contact_Id=" + Carrier_Id + " and Contact_Id=" + Carr_Contact_id + " " +
//						"end";
//				}
//				
//				DataSet dsSndLtr = new DataSet();
//				dsSndLtr = DataLayer.DataWrapper.ExecuteDataset(ClsCommon.ConnStr,CommandType.Text,strSQL);
//				foreach (DataRow Row in dsSndLtr.Tables[0].Rows)
//				{
//					strLabelEnvelope+="<LblName>" + RemoveJunkXmlCharacters(Row["LblName"].ToString()) + "</LblName>";
//					strLabelEnvelope+="<LblDispName>" + RemoveJunkXmlCharacters(Row["LblDispName"].ToString()) + "</LblDispName>";
//					strLabelEnvelope+="<LblAdd1>" + RemoveJunkXmlCharacters(Row["LblAdd1"].ToString()) + "</LblAdd1>";
//					strLabelEnvelope+="<LblAdd2>" + RemoveJunkXmlCharacters(Row["LblAdd2"].ToString()) + "</LblAdd2>";
//					strLabelEnvelope+="<LblCity>" + RemoveJunkXmlCharacters(Row["LblCity"].ToString()) + "</LblCity>";
//					strLabelEnvelope+="<LblState>" + RemoveJunkXmlCharacters(Row["LblState"].ToString()) + "</LblState>";
//					strLabelEnvelope+="<LblZip>" + RemoveJunkXmlCharacters(Row["LblZip"].ToString()) + "</LblZip>";
//				}
//				
//				returnAddress="";
//				if (PrintLblEnvp=="1" && EnvelopeReturnAddress !="") 
//				{
//					dtSndLtr = SQLHelper.ExecuteInlineQuery("Select Agency_Id,Reg_Id,Div_Id From Template_List Where Template_Id=" + Template_Id);
//					string AgencyId=dtSndLtr.Rows[0]["Agency_Id"].ToString().Trim();
//					string RegId=dtSndLtr.Rows[0]["Reg_Id"].ToString().Trim();
//					string DivId=dtSndLtr.Rows[0]["Div_Id"].ToString().Trim();
//					
//					if (TemplateClass == "1" || TemplateClass == "0")
//						strSQL = "Select CLIENT_AGENCY Agency_Id,CLIENT_REGN Reg_Id,CLIENT_DIV Div_Id from client_list Where Client_Id = " + Client_Id;
//					else if(TemplateClass == "2")
//						strSQL = "SELECT ISNULL(BROKER_AGENCY,'') BROKER_AGENCY Agency_Id,ISNULL(BROKER_REGN,'') BROKER_REGN Reg_Id,ISNULL(BROKER_DIV,'') BROKER_DIV Div_Id FROM BROKER_LIST WHERE BROKER_ID=" + Broker_Id;
//					else
//						strSQL="";
//
//					if (EnvelopeReturnAddress == "A" && (AgencyId!="" || strSQL!=""))
//					{
//						if (AgencyId=="")
//						{
//							dtSndLtr = SQLHelper.ExecuteInlineQuery(strSQL);
//							AgencyId=dtSndLtr.Rows[0]["Agency_Id"].ToString().Trim();
//							strSQL="";
//						}
//						if (AgencyId != "" && AgencyId != "0")
//						{	
//							strSQL="Select AGENCY_NAME Name,AGENCY_ADD1 Add1,AGENCY_ADD2 Add2,AGENCY_CITY City,AGENCY_STATE State,AGENCY_ZIP Zip From Agency_List Where Agency_Id=" + AgencyId;
//						}
//					}
//					else if (EnvelopeReturnAddress == "AA" && (AgencyId!="" || strSQL!=""))
//					{
//						if (AgencyId=="")
//						{
//							dtSndLtr = SQLHelper.ExecuteInlineQuery(strSQL);
//							AgencyId=dtSndLtr.Rows[0]["Agency_Id"].ToString().Trim();
//							strSQL="";
//						}
//						if (AgencyId != "" && AgencyId != "0")
//						{
//							strSQL="Select ALTERNATE_AGENCY_NAME Name,ALTERNATE_AGENCY_ADD1 Add1,ALTERNATE_AGENCY_ADD2 Add2,ALTERNATE_AGENCY_CITY City,ALTERNATE_AGENCY_STATE State,ALTERNATE_AGENCY_ZIP Zip From Agency_List Where Agency_Id=" + AgencyId;
//						}
//					}
//					else if (EnvelopeReturnAddress == "R" && (RegId!="" || strSQL!=""))
//					{
//						if (RegId=="")
//						{
//							dtSndLtr = SQLHelper.ExecuteInlineQuery(strSQL);
//							RegId=dtSndLtr.Rows[0]["Reg_Id"].ToString().Trim();
//							strSQL="";
//						}
//						if (RegId != "" && RegId != "0")
//						{
//							strSQL="Select REG_NAME Name,REG_ADD1 Add1,REG_ADD2 Add2,REG_CITY City,REG_STATE State,REG_ZIP Zip From Reg_List Where Reg_Id=" + RegId;		
//						}
//					}
//					else if (EnvelopeReturnAddress == "AR" && (RegId!="" || strSQL!=""))
//					{
//						if (RegId=="")
//						{
//							dtSndLtr = SQLHelper.ExecuteInlineQuery(strSQL);
//							RegId=dtSndLtr.Rows[0]["Reg_Id"].ToString().Trim();
//							strSQL="";
//						}
//						if (RegId != "" && RegId != "0")
//						{
//							strSQL="Select ALTERNATE_REGION_NAME Name,ALTERNATE_REGION_ADD1 Add1,ALTERNATE_REGION_ADD2 Add2,ALTERNATE_REGION_CITY City,ALTERNATE_REGION_STATE State,ALTERNATE_REGION_ZIP Zip From Reg_List Where Reg_Id=" + RegId;
//						}
//					}
//					else if (EnvelopeReturnAddress == "D" && (DivId !="" || strSQL!=""))
//					{
//						if (DivId=="")
//						{
//							dtSndLtr = SQLHelper.ExecuteInlineQuery(strSQL);
//							DivId=dtSndLtr.Rows[0]["Div_Id"].ToString().Trim();
//							strSQL="";
//						}
//						if (DivId != "" && DivId != "0")
//						{
//							strSQL="Select DIV_NAME Name,DIV_ADD1 Add1,DIV_ADD2 Add2,DIV_CITY City,DIV_STATE State,DIV_ZIP Zip From Div_List Where Div_Id=" + DivId;
//						}
//					}
//					else if (EnvelopeReturnAddress == "AD" && (DivId !="" || strSQL!=""))
//					{
//						if (DivId=="")
//						{
//							dtSndLtr = SQLHelper.ExecuteInlineQuery(strSQL);
//							DivId=dtSndLtr.Rows[0]["Div_Id"].ToString().Trim();
//							strSQL="";
//						}
//						if (DivId != "" && DivId != "0")
//						{
//							strSQL="Select ALTERNATE_DIVISION_NAME Name,ALTERNATE_DIVISION_ADD1 Add1,ALTERNATE_DIVISION_ADD2 Add2,ALTERNATE_DIVISION_CITY City,ALTERNATE_DIVISION_STATE State,ALTERNATE_DIVISION_ZIP Zip From Div_List Where Div_Id=" + DivId;
//						}
//					}
//					if (strSQL !="")
//					{
//						dtSndLtr = SQLHelper.ExecuteInlineQuery(strSQL);
//						returnAddress+="<ReturnAddress>";
//						returnAddress+="<LblName>" + RemoveJunkXmlCharacters(dtSndLtr.Rows[0]["Name"].ToString().Trim()) + "</LblName>";
//						returnAddress+="<LblAdd1>" + RemoveJunkXmlCharacters(dtSndLtr.Rows[0]["Add1"].ToString().Trim()) + "</LblAdd1>";
//						returnAddress+="<LblAdd2>" + RemoveJunkXmlCharacters(dtSndLtr.Rows[0]["Add2"].ToString().Trim()) + "</LblAdd2>";
//						returnAddress+="<LblCity>" + RemoveJunkXmlCharacters(dtSndLtr.Rows[0]["City"].ToString().Trim()) + "</LblCity>";
//						returnAddress+="<LblState>" + RemoveJunkXmlCharacters(dtSndLtr.Rows[0]["State"].ToString().Trim()) + "</LblState>";
//						returnAddress+="<LblZip>" + RemoveJunkXmlCharacters(dtSndLtr.Rows[0]["Zip"].ToString().Trim()) + "</LblZip>";
//						returnAddress+="</ReturnAddress>";
//					}
//				}	
//				strLabelEnvelope += returnAddress + "</LblEnvp>";
//				
//				XmlNode Node;
//				string Xpath="";
//				if (PrintLblEnvp=="1")
//					Xpath="//prn_envelope[@id='" + LblEnvpType + "']";
//				else if (PrintLblEnvp=="2")
//					Xpath="//prn_label[@id='" + LblEnvpType + "']";
//					
//				Node=DataXmlDoc.SelectSingleNode(Xpath);
//				if (Node==null)
//				{
//					dtSndLtr = SQLHelper.ExecuteInlineQuery("SELECT Description FROM  WORD_MERGE_LABEL_ENVELOPE Where ItemId=" + LblEnvpType);
//					
//					if (PrintLblEnvp=="1")
//						Xpath="//envelopes";
//					else if (PrintLblEnvp=="2")
//						Xpath ="//labels";
//					
//					Node=DataXmlDoc.SelectSingleNode(Xpath);
//					
//					Node.InnerXml = Node.InnerXml + dtSndLtr.Rows[0][0].ToString().Trim();
//				}
//
//				return(strLabelEnvelope);
			}
			else
				return("");
		}
		//To Update the Path in Template table
		public void UpdateTemplatePath(string templatePath,	string MergeID)
		{
			string strStoredProc = "Proc_DOC_UpdateTemplatePath";
			
			int		returnResult;
			if(templatePath!="")
			{
				DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@MERGE_ID",int.Parse(MergeID));
				objWrapper.AddParameter("@TEMPLATE_PATH",templatePath);
				returnResult = objWrapper.ExecuteNonQuery(strStoredProc);
				objWrapper.ClearParameteres();

			}

		}
	}
}
