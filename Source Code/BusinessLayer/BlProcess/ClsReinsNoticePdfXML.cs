using System;
using System.Xml;
using System.Data;
using Cms.DataLayer;
using System.Collections;

namespace Cms.BusinessLayer.BlProcess
{
	/// <summary>
	/// <CreatedBy>Mohit Agarwal</CreatedBy>
	/// <Dated>12-Feb-2007</Dated>
	/// <Purpose>To Create XML for NON-RENEWAL NOTICE for ALL LOBs</Purpose>
	/// </summary>
	public class ClsReinsNoticePdfXML : ClsCommonPdfXML
	{
		#region Declarations
		private XmlElement DecPageRootElement;
		private Hashtable htpremium=new Hashtable(); 
		private DataWrapper gobjSqlHelper;
		private string stCode="";
		private string gStrLobCode="";
		private string gInsAgn="Insured";
		private string gAILobCode="";
		private string strstateCode="";
		#endregion

		#region Constructor
		public ClsReinsNoticePdfXML(string lstrClientId,string lstrPolicyId,string lstrVersionId,string lStrCalledFrom,string lStrCalledFor,string lStrLobCode,string stateCode, string Agn_Ins)
		{
			gStrClientID=lstrClientId;
			gStrPolicyId=lstrPolicyId;
			gStrPolicyVersion=lstrVersionId;
			gStrCalledFrom=lStrCalledFrom.ToUpper().Trim();
			gStrPdfFor=lStrCalledFor.ToUpper().Trim();
			gStrLobCode="HOME";
			stCode="IN";
			gInsAgn=Agn_Ins;
			gAILobCode=lStrLobCode.ToUpper().Trim();
			strstateCode=stateCode;
		}
		#endregion

		public string getReinsNotPDFXml()
		{
			DSTempDataSet = new DataSet();
			gobjSqlHelper = new DataWrapper(ConnStr,CommandType.Text);
			SetPDFVersionLobNode(gStrLobCode,System.DateTime.Now);
		
			AcordPDFXML = new XmlDocument();
			AcordPDFXML.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <" + RootElement + "/>");

			createRootElementForAllRootPDFs();
			//LoadRateXML();
			FillMonth();

			//creating Xml From Here
			CreateCustAgencyXML();
			creatmaillingpage();
			return AcordPDFXML.OuterXml;
		}
		private void creatmaillingpage()
		{
			string strsendmessg="";
			string strInsname="";
			string strInsAdd="";
			string strcityzip="";
			XmlElement MaillingRootElementDecPage;
			MaillingRootElementDecPage    = AcordPDFXML.CreateElement("MAILLINGPAGE");
			
			DataSet DstempAppDocument = new DataSet();
			DstempAppDocument = gobjSqlHelper.ExecuteDataSet("Proc_GetCancellationNoticeData " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion);
			strInsname=DstempAppDocument.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString();
			strInsAdd = DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString();
			if(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString().Trim() != "")
				strInsAdd += ", " + DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString();
			
			//strInsAdd=RemoveJunkXmlCharacters(DstempAppDocument.Tables[0].Rows[0]["APPADDRESS"].ToString());
			strcityzip=DstempAppDocument.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString()+ " "+DstempAppDocument.Tables[0].Rows[0]["STATE_CODE"].ToString()+" "+DstempAppDocument.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString();
			
			DataSet DstempDocument = new DataSet();
			ClsPolicyProcess objPolicyProcess = new ClsPolicyProcess();			
			objPolicyProcess.BeginTransaction();
			string DocumentCode=objPolicyProcess.GetCancellationCode("ALL","ALL",gInsAgn,"REINS");//("ALL","ALL",gInsAgn,gCancType);
			objPolicyProcess.CommitTransaction();
			if(DocumentCode!="" && DocumentCode!=null)
			{
				DstempDocument = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFDocumentMessage " + DocumentCode + "");
				strsendmessg=DstempDocument.Tables[0].Rows[0]["SEND_MESSAGE"].ToString();
			}
			//AcordPDFXML.SelectSingleNode(RootElement).AppendChild(MaillingRootElementDecPage);
			if(strsendmessg	=="Y")
			{
				//DecPageRootElement.AppendChild(MaillingRootElementDecPage);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(MaillingRootElementDecPage);
				MaillingRootElementDecPage.SetAttribute(fieldType,fieldTypeMultiple);
				MaillingRootElementDecPage.SetAttribute(PrimPDF,getAcordPDFNameFromXML("MAILLINGPAGE"));
				MaillingRootElementDecPage.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("MAILLINGPAGE"));
				MaillingRootElementDecPage.SetAttribute(SecondPDF,getAcordPDFNameFromXML("MAILLINGPAGEEXTN"));
				MaillingRootElementDecPage.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("MAILLINGPAGEEXTN"));
				
				XmlElement DecMailElement;
				DecMailElement =  AcordPDFXML.CreateElement("MAILADDRESS");
				MaillingRootElementDecPage.AppendChild(DecMailElement);
				DecMailElement.SetAttribute(fieldType,fieldTypeSingle);
				DecMailElement.SetAttribute(id,"0");

				DecMailElement.InnerXml = DecMailElement.InnerXml +  "<MAILLINGNAME " + fieldType + "=\"" + fieldTypeText + "\">" +  RemoveJunkXmlCharacters(strInsname) + "</MAILLINGNAME>";
				DecMailElement.InnerXml = DecMailElement.InnerXml +  "<MAILLINGADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" +  RemoveJunkXmlCharacters(strInsAdd) + "</MAILLINGADDRESS>";
				DecMailElement.InnerXml = DecMailElement.InnerXml +  "<CITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" +  RemoveJunkXmlCharacters(strcityzip) + "</CITYSTATEZIP>";
				if(DstempDocument.Tables[0].Rows[0]["DOCUMENT_MESSAGE"].ToString()!="0")
					DecMailElement.InnerXml = DecMailElement.InnerXml +  "<MESSAGE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DstempDocument.Tables[0].Rows[0]["DOCUMENT_MESSAGE"].ToString()) + "</MESSAGE>";
			}
		
		}
		#region create mailling page xml for additinal intrest
		private void creatmaillingpageAddintrst(int addlintindex)
		{
			string strsendmessg="";
			string strInsname="";
			string strInsAdd="";
			string strcityzip="";
			DataSet DSTempAint=null;
			if(gAILobCode == "HOME" || gAILobCode == "RENT")
				DSTempAint = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_Additional_Interest " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",0,'" + gStrCalledFrom + "'");
			else if(gAILobCode == "MOT" || gAILobCode == "PPA")
				DSTempAint = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",0,'" + gStrCalledFrom + "'");
			else if(gAILobCode == "WAT")
				DSTempAint = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",0,'" + gStrCalledFrom + "'");
			strInsname=DSTempAint.Tables[0].Rows[addlintindex]["HOLDER_NAME"].ToString();	
			strInsAdd = DSTempAint.Tables[0].Rows[addlintindex]["ADDRESS"].ToString();
			if(strInsAdd.Trim().EndsWith(","))
				strInsAdd = strInsAdd.Substring(0, strInsAdd.LastIndexOf(","));
			if(gAILobCode == "HOME" || gAILobCode == "RENT")
			{
				strcityzip=DSTempAint.Tables[0].Rows[addlintindex]["HOLDERCITYSTATEZIP"].ToString();
			}
			else
			{
				strcityzip=DSTempAint.Tables[0].Rows[addlintindex]["CITYSTATEZIP"].ToString();
			}
			
			
			XmlElement MaillingRootElementDecPage;
			MaillingRootElementDecPage    = AcordPDFXML.CreateElement("MAILLINGPAGE");
			DataSet DstempAppDocument = new DataSet();
			DataSet DstempDocument = new DataSet();
			ClsPolicyProcess objPolicyProcess = new ClsPolicyProcess();			
			objPolicyProcess.BeginTransaction();
			string DocumentCode=objPolicyProcess.GetCancellationCode("ALL","ALL",gInsAgn,"REINS");
			objPolicyProcess.CommitTransaction();
			if(DocumentCode!="" && DocumentCode!=null)
			{
				DstempDocument = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFDocumentMessage " + DocumentCode + "");
				strsendmessg=DstempDocument.Tables[0].Rows[0]["SEND_MESSAGE"].ToString();
			}
			//AcordPDFXML.SelectSingleNode(RootElement).AppendChild(MaillingRootElementDecPage);
			if(strsendmessg	=="Y")
			{
				//DecPageRootElement.AppendChild(MaillingRootElementDecPage);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(MaillingRootElementDecPage);
				MaillingRootElementDecPage.SetAttribute(fieldType,fieldTypeMultiple);
				MaillingRootElementDecPage.SetAttribute(PrimPDF,getAcordPDFNameFromXML("MAILLINGPAGE"));
				MaillingRootElementDecPage.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("MAILLINGPAGE"));
				MaillingRootElementDecPage.SetAttribute(SecondPDF,getAcordPDFNameFromXML("MAILLINGPAGEEXTN"));
				MaillingRootElementDecPage.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("MAILLINGPAGEEXTN"));
				
				XmlElement DecMailElement;
				DecMailElement =  AcordPDFXML.CreateElement("MAILADDRESS");
				MaillingRootElementDecPage.AppendChild(DecMailElement);
				DecMailElement.SetAttribute(fieldType,fieldTypeSingle);
				DecMailElement.SetAttribute(id,"0");

				DecMailElement.InnerXml = DecMailElement.InnerXml +  "<MAILLINGNAME " + fieldType + "=\"" + fieldTypeText + "\">" +  RemoveJunkXmlCharacters(strInsname) + "</MAILLINGNAME>";
				DecMailElement.InnerXml = DecMailElement.InnerXml +  "<MAILLINGADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" +  RemoveJunkXmlCharacters(strInsAdd) + "</MAILLINGADDRESS>";
				DecMailElement.InnerXml = DecMailElement.InnerXml +  "<CITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" +  RemoveJunkXmlCharacters(strcityzip) + "</CITYSTATEZIP>";
				if(DstempDocument.Tables[0].Rows[0]["DOCUMENT_MESSAGE"].ToString()!="0")
					DecMailElement.InnerXml = DecMailElement.InnerXml +  "<MESSAGE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DstempDocument.Tables[0].Rows[0]["DOCUMENT_MESSAGE"].ToString()) + "</MESSAGE>";
			}
		
		}
		#endregion

		#region To Create Root Element For All Root PDFs
		private void createRootElementForAllRootPDFs()
		{
			DecPageRootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
			AcordPDFXML.SelectSingleNode(RootElement).AppendChild(DecPageRootElement);

			if(gInsAgn=="Insured")
				DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEREINSNOTINS"));
			else
				DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEREINSNOTAGN"));
		}
		#endregion

		#region Creating Customer And Agency Xml 
		private string NamedInsured(DataSet dsIns)
		{
			string names = "";
			if(dsIns.Tables.Count > 1)
			{
				foreach(DataRow drIns in dsIns.Tables[1].Rows)
				{
					if(names != "")
						names += ", " + drIns["APPNAME"].ToString();
					else
						names += drIns["APPNAME"].ToString();
				}
			}
			return names;

		}

		private void CreateCustAgencyXML()
		{
			string due_date = DateTime.Now.Month.ToString() + "/" + (DateTime.Now.Day + 5).ToString() + "/" + DateTime.Now.Year.ToString();
			DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetCancellationNoticeData " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion);
			
			#region Policy Agency Part
			XmlElement DecPagePolicyElement;
			DecPagePolicyElement = AcordPDFXML.CreateElement("POLICY");
			DecPageRootElement.AppendChild(DecPagePolicyElement);
			DecPagePolicyElement.SetAttribute(fieldType,fieldTypeSingle);
		
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_nam1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString()) + "</canc_nam1>";
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_nam2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString()) + "</canc_nam2>";
			string cust_addr = DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString();
			if(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString().Trim() != "")
				cust_addr += ", " +  DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString();
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_addr1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(cust_addr) + "</canc_addr1>";
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_addr2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString() + " " + DSTempDataSet.Tables[0].Rows[0]["STATE_CODE"].ToString() + " " + DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString()) + "</canc_addr2>";
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<AGENC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</AGENC>";
			string agn_addr = DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD1"].ToString();
			if(DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD2"].ToString().Trim() != "")
				agn_addr += ", " + DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD2"].ToString();
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<AGENC_ADDR1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(agn_addr) + "</AGENC_ADDR1>";
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<AGENC_ADDR2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_CITY"].ToString() + " " + DSTempDataSet.Tables[0].Rows[0]["AGENCY_STATE"].ToString() + " " + DSTempDataSet.Tables[0].Rows[0]["AGENCY_ZIP"].ToString()) + "</AGENC_ADDR2>";
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<AGENC_PHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString()) + "</AGENC_PHONE>";
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<lob " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["LOB_DESC"].ToString()) + "</lob>";
			if(DSTempDataSet.Tables[0].Rows[0]["PROCESS_DATE"] != System.DBNull.Value)
			{
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<dat_cancel " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PROCESS_DATE"].ToString()) + "</dat_cancel>";
				base_due_date= DSTempDataSet.Tables[0].Rows[0]["PROCESS_DATE"].ToString();
			}
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<dat_mailing " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DateTime.Now.ToString("MM/dd/yyyy")) + "</dat_mailing>";

			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<polNum " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</polNum>";
			if((DSTempDataSet.Tables[0].Rows[0]["APP_INCEPTION_DATE"] != System.DBNull.Value)&&(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value))
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<polTerm " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Convert.ToDateTime(DSTempDataSet.Tables[0].Rows[0]["APP_INCEPTION_DATE"]).ToString("MM/dd/yyyy")+"-"+Convert.ToDateTime(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"]).ToString("MM/dd/yyyy")) + "</polTerm>";
			//DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<reason_canc " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["REASON"].ToString()) + "</reason_canc>";
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<reason_canc " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</reason_canc>";
			#endregion
		}
		#endregion

		public string getReinsNotAIPDFXml(int addlintindex)
		{
			DSTempDataSet = new DataSet();
			gobjSqlHelper = new DataWrapper(ConnStr,CommandType.Text);
			SetPDFVersionLobNode("HOME",System.DateTime.Now);
		
			AcordPDFXML = new XmlDocument();
			AcordPDFXML.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <" + RootElement + "/>");

			createRootElementAIForAllRootPDFs();
			//LoadRateXML();
			FillMonth();

			//creating Xml From Here
			CreateCustAgencyAIXML(addlintindex);
			creatmaillingpageAddintrst(addlintindex);
			return AcordPDFXML.OuterXml;
		}

		#region To Create Root Element For All Root PDFs
		private void createRootElementAIForAllRootPDFs()
		{
			DecPageRootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
			AcordPDFXML.SelectSingleNode(RootElement).AppendChild(DecPageRootElement);

			DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAIREINSNOT"));

		}
		#endregion

		#region Creating Customer And Agency Xml 
		private void CreateCustAgencyAIXML(int addlintindex)
		{
			string due_date = DateTime.Now.Month.ToString() + "/" + (DateTime.Now.Day + 5).ToString() + "/" + DateTime.Now.Year.ToString();
			DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetCancellationNoticeData " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion);
			
			#region Policy Agency Part
			string cust_name = DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString();
			string cust_add = DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString();
			if(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString().Trim()!="")
				 cust_add +=  ", " + DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString();
			string cust_city = DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString() + " " + DSTempDataSet.Tables[0].Rows[0]["STATE_CODE"].ToString() + " " + DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString();
			string agency_name = DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString();
			string agency_add = DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD1"].ToString();
			if(DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD2"].ToString().Trim()!="")
				agency_add += ", " + DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD2"].ToString();
			string agency_city = DSTempDataSet.Tables[0].Rows[0]["AGENCY_CITY"].ToString() + " " + DSTempDataSet.Tables[0].Rows[0]["AGENCY_STATE"].ToString() + " " + DSTempDataSet.Tables[0].Rows[0]["AGENCY_ZIP"].ToString();
			string agency_phone = DSTempDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString();
			string lob = DSTempDataSet.Tables[0].Rows[0]["LOB_DESC"].ToString();
			string pol_num = DSTempDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString();
			string app_term = "";
			if((DSTempDataSet.Tables[0].Rows[0]["APP_INCEPTION_DATE"] != System.DBNull.Value)&&(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value))
				app_term = Convert.ToDateTime(DSTempDataSet.Tables[0].Rows[0]["APP_INCEPTION_DATE"]).ToString("MM/dd/yyyy")+"-"+Convert.ToDateTime(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"]).ToString("MM/dd/yyyy");
			string reason = DSTempDataSet.Tables[0].Rows[0]["REASON"].ToString();
			string process_date = "";
			if(DSTempDataSet.Tables[0].Rows[0]["PROCESS_DATE"] != System.DBNull.Value)
			{
				process_date = DSTempDataSet.Tables[0].Rows[0]["PROCESS_DATE"].ToString();
				base_due_date= DSTempDataSet.Tables[0].Rows[0]["PROCESS_DATE"].ToString();
			}
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<premDue " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(due_date) + "</premDue>";
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_tot " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["TOTAL_DUE"].ToString()) + "</prem_tot>";
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_min " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["MIN_DUE"].ToString()) + "</prem_min>";


			XmlElement DecPagePolicyElement;
			DecPagePolicyElement = AcordPDFXML.CreateElement("POLICY");
			DecPageRootElement.AppendChild(DecPagePolicyElement);
			DecPagePolicyElement.SetAttribute(fieldType,fieldTypeMultiple);
			DecPagePolicyElement.SetAttribute(PrimPDF,"");
			DecPagePolicyElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAIREINSNOT"));
			DecPagePolicyElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEAIREINSNOT"));
			DecPagePolicyElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAIREINSNOT"));
			#endregion

			DataSet DSTempDwellinAdd=null;
			if(gAILobCode == "HOME" || gAILobCode == "RENT")
				DSTempDwellinAdd = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_Additional_Interest " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",0,'" + gStrCalledFrom + "'");
			else if(gAILobCode == "MOT" || gAILobCode == "PPA")
				DSTempDwellinAdd = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",0,'" + gStrCalledFrom + "'");
			else if(gAILobCode == "WAT")
				DSTempDwellinAdd = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",0,'" + gStrCalledFrom + "'");
			#region Addl Int Info
			int RowCounter = 0;
			//foreach (DataRow Row in DSTempDwellinAdd.Tables[0].Rows)
			if(DSTempDwellinAdd.Tables[0].Rows.Count > addlintindex)
			{
				XmlElement DecPageAddlInts;
				DecPageAddlInts = AcordPDFXML.CreateElement("ADDITIONALINTERESTS");
				DecPagePolicyElement.AppendChild(DecPageAddlInts);
				DecPageAddlInts.SetAttribute(fieldType,fieldTypeNormal);
				DecPageAddlInts.SetAttribute(id,RowCounter.ToString());

				DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<canc_nam1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</canc_nam1>";
				DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<canc_nam2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(cust_name) + "</canc_nam2>";
				DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<canc_addr1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(cust_add) + "</canc_addr1>";
				DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<canc_addr2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(cust_city) + "</canc_addr2>";
				DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<AGENC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(agency_name) + "</AGENC>";
				DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<AGENC_ADDR1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(agency_add) + "</AGENC_ADDR1>";
				DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<AGENC_ADDR2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(agency_city) + "</AGENC_ADDR2>";
				DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<AGENC_PHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(agency_phone) + "</AGENC_PHONE>";
				DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<lob " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(lob) + "</lob>";
				DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<dat_cancel " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(process_date) + "</dat_cancel>";
				DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<dat_mailing " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DateTime.Now.ToString("MM/dd/yyyy")) + "</dat_mailing>";
				//			DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<premDue " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(due_date) + "</premDue>";
				//			DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<prem_tot " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["TOTAL_DUE"].ToString()) + "</prem_tot>";
				//			DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<prem_min " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["MIN_DUE"].ToString()) + "</prem_min>";
				DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<polNum " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(pol_num) + "</polNum>";
				if((DSTempDataSet.Tables[0].Rows[0]["APP_INCEPTION_DATE"] != System.DBNull.Value)&&(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value))
					DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<polTerm " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(app_term) + "</polTerm>";
				//					DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<reason_canc " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(reason) + "</reason_canc>";
				//			DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<prem_tot " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</prem_tot>";
				//			DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<prem_amt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString()) + "</prem_amt>";
				if(gAILobCode == "HOME" || gAILobCode == "RENT")
				{
					//						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDITIONALINTERESTNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["RANK"].ToString())+"</ADDITIONALINTERESTNO>"; 
					DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["HOLDER_NAME"].ToString())+"</ADDL>"; 
					string addl_addr = DSTempDwellinAdd.Tables[0].Rows[addlintindex]["ADDRESS"].ToString();
					if(addl_addr.Trim().EndsWith(","))
						addl_addr = addl_addr.Substring(0,addl_addr.LastIndexOf(","));
					DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL_ADDR1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(addl_addr)+"</ADDL_ADDR1>"; 
					//DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL_ADDR1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["ADDRESS"].ToString())+"</ADDL_ADDR1>"; 
					DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL_ADDR2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["HOLDERCITYSTATEZIP"].ToString())+"</ADDL_ADDR2>"; 
				}
				else if(gAILobCode == "MOT" || gAILobCode == "PPA")
				{
					//						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDITIONALINTERESTNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["RANK"].ToString())+"</ADDITIONALINTERESTNO>"; 
					DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["HOLDER_NAME"].ToString())+"</ADDL>";
					string addl_addr = DSTempDwellinAdd.Tables[0].Rows[addlintindex]["ADDRESS"].ToString();
					if(addl_addr.Trim().EndsWith(","))
						addl_addr = addl_addr.Substring(0,addl_addr.LastIndexOf(","));
					DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL_ADDR1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(addl_addr)+"</ADDL_ADDR1>"; 
					//DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL_ADDR1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["ADDRESS"].ToString())+"</ADDL_ADDR1>"; 
					DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL_ADDR2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["CITYSTATEZIP"].ToString())+"</ADDL_ADDR2>"; 
				}
				else if(gAILobCode == "WAT")
				{
					DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["HOLDER_NAME"].ToString())+"</ADDL>"; 
					string addl_addr = DSTempDwellinAdd.Tables[0].Rows[addlintindex]["ADDRESS"].ToString();
					if(addl_addr.Trim().EndsWith(","))
						addl_addr = addl_addr.Substring(0,addl_addr.LastIndexOf(","));
					DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL_ADDR1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(addl_addr)+"</ADDL_ADDR1>"; 
					//DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL_ADDR1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["ADDRESS"].ToString())+"</ADDL_ADDR1>"; 
					DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL_ADDR2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["CITYSTATEZIP"].ToString())+"</ADDL_ADDR2>"; 
				}
				RowCounter++;
			}
			#endregion
		}
		#endregion


	}
}