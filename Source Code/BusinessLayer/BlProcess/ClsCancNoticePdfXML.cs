using System;
using System.Xml;
using System.Data;
using Cms.DataLayer;
using System.Collections;
using Cms.ExceptionPublisher;
namespace Cms.BusinessLayer.BlProcess
{
	/// <summary>
	/// <CreatedBy>Mohit Agarwal</CreatedBy>
	/// <Dated>05-Feb-2007</Dated>
	/// <Purpose>To Create XML for CANCELLATION NOTICE for ALL LOBs</Purpose>
	/// </summary>
	public class ClsCancNoticePdfXML : ClsCommonPdfXML
	{
		#region Declarations
		private XmlElement DecPageRootElement;
		private Hashtable htpremium=new Hashtable(); 
		private DataWrapper gobjSqlHelper;
		private string stCode="";
		private string gStrLobCode="";
		private string gAILobCode="";
		private string gInsAgn="Insured";
		private string gCancType = "Company";
		double late_fee = 0;
		#endregion


		#region Constructor
		public ClsCancNoticePdfXML()
		{
		}

		public ClsCancNoticePdfXML(DataWrapper objDatawrapper)
		{
			objWrapper = objDatawrapper ; 
		}

		public ClsCancNoticePdfXML(string lstrClientId,string lstrPolicyId,string lstrVersionId,string lStrCalledFrom,string lStrCalledFor,string lStrLobCode,string stateCode, string Agn_Ins, string canc_type )
		{
			gStrClientID=lstrClientId;
			gStrPolicyId=lstrPolicyId;
			gStrPolicyVersion=lstrVersionId;
			gStrCalledFrom=lStrCalledFrom.ToUpper().Trim();
			gStrPdfFor=lStrCalledFor.ToUpper().Trim();
			switch(lStrLobCode.ToUpper())
			{
				case "WAT":
					gStrLobCode = "BOAT";
					break;
				case "HOME":
					gStrLobCode = "HOME";
					break;
				case "RENT":
					gStrLobCode = "REDW";
					break;
				case "PPA":
					gStrLobCode = "AUTO";
					break;
				case "MOT":
					gStrLobCode = "MOT";
					break;
				case "UMB":
					gStrLobCode = "UMB";
					break;
			}
			gAILobCode=lStrLobCode;
			stCode=stateCode;
			gInsAgn=Agn_Ins;
			gCancType = canc_type;
			
			if((gCancType == "INSREQ") || (gCancType == "NSF") || (gCancType == "NONPAY") || (gCancType == "NONPAYDBMEMO") || (lStrCalledFor == "ADDLINT"))
				gStrLobCode = "HOME";

			}
		#endregion

		#region Cancellation Notice
		private string NamedInsured(DataSet dsIns)
		{
			string names = "";
			if(dsIns.Tables.Count > 1)
			{
				foreach(DataRow drIns in dsIns.Tables[1].Rows)
				{
					if(names != "")
						names += " & " + drIns["APPNAME"].ToString();
					else
						names += drIns["APPNAME"].ToString();
				}
			}
			return names;
		}
		private string NamedInsuredWithSuffixs(DataSet dsIns)
		{
			string names = "";
			if(dsIns.Tables.Count > 1)
			{
				foreach(DataRow drIns in dsIns.Tables[1].Rows)
				{
					if(names != "")
						names += " & " + drIns["APPNAME"].ToString()+ drIns["APP_SUFFIX"].ToString();
					else
						names += drIns["APPNAME"].ToString() + drIns["APP_SUFFIX"].ToString();
				}
			}
			return names;
		}
		public string getCancNotPDFXml()
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
			DataWrapper gobjSqlHelper;
			gobjSqlHelper = new DataWrapper(ConnStr,CommandType.Text);
			
						DataSet DstempAppDocument = new DataSet();
						DstempAppDocument = gobjSqlHelper.ExecuteDataSet("Proc_GetCancellationNoticeData " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion);
			strInsname=DstempAppDocument.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString()+" "+DstempAppDocument.Tables[0].Rows[0]["CUSTOMER_SUFFIX"].ToString();
			strInsAdd = DstempAppDocument.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString();
			if(DstempAppDocument.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString().Trim() != "")
				strInsAdd += ", " + DstempAppDocument.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString();
			
			//strInsAdd=RemoveJunkXmlCharacters(DstempAppDocument.Tables[0].Rows[0]["APPADDRESS"].ToString());
			strcityzip=DstempAppDocument.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString()+ " "+DstempAppDocument.Tables[0].Rows[0]["STATE_CODE"].ToString()+" "+DstempAppDocument.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString();
			
			DataSet DstempDocument = new DataSet();
			ClsPolicyProcess objPolicyProcess = new ClsPolicyProcess();			
			objPolicyProcess.BeginTransaction();
			string DocumentCode=objPolicyProcess.GetCancellationCode("ALL","ALL",gInsAgn,gCancType);
			objPolicyProcess.CommitTransaction();
			
			if(DocumentCode!="" && DocumentCode!=null)
			{
				DstempDocument = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFDocumentMessage " + DocumentCode + "");
				strsendmessg=DstempDocument.Tables[0].Rows[0]["SEND_MESSAGE"].ToString();
			}
			gobjSqlHelper.Dispose();
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
// overload for mailling page 
		private void creatmaillingpage(DataSet dsTempSet)
		{
			string strsendmessg="";
			string strInsname="";
			string strInsAdd="";
			string strcityzip="";
			XmlElement MaillingRootElementDecPage;
			MaillingRootElementDecPage    = AcordPDFXML.CreateElement("MAILLINGPAGE");
			
//			DataSet DstempAppDocument = new DataSet();
//			DstempAppDocument = gobjSqlHelper.ExecuteDataSet("Proc_GetCancellationNoticeData " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion);
			strInsname=dsTempSet.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString()+ " "+dsTempSet.Tables[0].Rows[0]["CUSTOMER_SUFFIX"].ToString();
			strInsAdd = dsTempSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString();
			if(dsTempSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString().Trim() != "")
				strInsAdd += ", " + dsTempSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString();
			
			//strInsAdd=RemoveJunkXmlCharacters(DstempAppDocument.Tables[0].Rows[0]["APPADDRESS"].ToString());
			strcityzip=dsTempSet.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString()+ " "+dsTempSet.Tables[0].Rows[0]["STATE_CODE"].ToString()+" "+dsTempSet.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString();
			
			DataSet DstempDocument = new DataSet();
			ClsPolicyProcess objPolicyProcess = new ClsPolicyProcess();			
			objPolicyProcess.BeginTransaction();
			string DocumentCode=objPolicyProcess.GetCancellationCode("ALL","ALL",gInsAgn,"NONPAYDB");
			objPolicyProcess.CommitTransaction();
			DataWrapper gobjSqlHelper;
			gobjSqlHelper = new DataWrapper(ConnStr,CommandType.Text);
			if(DocumentCode!="" && DocumentCode!=null)
			{
				DstempDocument = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFDocumentMessage " + DocumentCode + "");
				strsendmessg=DstempDocument.Tables[0].Rows[0]["SEND_MESSAGE"].ToString();
			}
			gobjSqlHelper.Dispose();
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
			DataWrapper gobjSqlHelper;
			gobjSqlHelper = new DataWrapper(ConnStr,CommandType.Text);
			
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
			if(gStrLobCode == "REDW"  || gStrLobCode == "RENT" ||  gAILobCode == "HOME" || gAILobCode == "REDW"  || gAILobCode == "RENT" )
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
			string DocumentCode=objPolicyProcess.GetCancellationCode("ALL","ALL",gInsAgn,gCancType);
			objPolicyProcess.CommitTransaction();
			
			if(DocumentCode!="" && DocumentCode!=null)
			{
				DstempDocument = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFDocumentMessage " + DocumentCode + "");
				strsendmessg=DstempDocument.Tables[0].Rows[0]["SEND_MESSAGE"].ToString();
			}
			gobjSqlHelper.Dispose();
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

		// overload of mailling page for additional intrest
		private void creatmaillingpageAddintrst(string HolderName , string Address1 ,string Address2 )
		{
			string strsendmessg="";
			string strInsname="";
			string strInsAdd="";
			string strcityzip="";
			strInsname=HolderName;//dsTempMail.Tables[0].Rows[0]["HOLDER_NAME"].ToString();	
			strInsAdd =Address1; //dsTempMail.Tables[0].Rows[0]["ADDRESS"].ToString();
			strcityzip = Address2;
					
			XmlElement MaillingRootElementDecPage;
			MaillingRootElementDecPage    = AcordPDFXML.CreateElement("MAILLINGPAGE");
			DataSet DstempAppDocument = new DataSet();
			DataSet DstempDocument = new DataSet();
			ClsPolicyProcess objPolicyProcess = new ClsPolicyProcess();			
			objPolicyProcess.BeginTransaction();
			string DocumentCode=objPolicyProcess.GetCancellationCode("ALL","ALL",gInsAgn,"NONPAYDB");
			objPolicyProcess.CommitTransaction();
			
			DataWrapper gobjSqlHelper;
			gobjSqlHelper = new DataWrapper(ConnStr,CommandType.Text);
			
			if(DocumentCode!="" && DocumentCode!=null)
			{
				DstempDocument = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFDocumentMessage " + DocumentCode + "");
				strsendmessg=DstempDocument.Tables[0].Rows[0]["SEND_MESSAGE"].ToString();
			}
			gobjSqlHelper.Dispose();
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

			if(gCancType == "NONPAYDBMEMO")
			{
				if(gInsAgn=="Insured")
					DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECANCMEMONONPAYDBINS"));
				else
					DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECANCMEMONONPAYDBAGN"));
			}
			else if(gCancType == "NSF")
			{
				if(gInsAgn=="Insured")
					DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECANCNOTNSFINS"));
				else
					DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECANCNOTNSFAGN"));
			}
			else if(gCancType == "NONPAY")
			{
				if(gInsAgn=="Insured")
					DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECANCNOTNONPAYINS"));
				else
					DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECANCNOTNONPAYAGN"));
			}
			else if(gCancType != "INSREQ")
			{
				if(stCode == "IN")
				{
					if(gInsAgn=="Insured")
						DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECANCNOTINDINS"));
					else
						DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECANCNOTINDAGN"));
				}
				if(stCode == "MI")
				{
					if(gInsAgn=="Insured")
						DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECANCNOTMICINS"));
					else
						DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECANCNOTMICAGN"));
				}
				if(stCode == "WI")
				{
					if(gInsAgn=="Insured")
						DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECANCNOTWISINS"));
					else
						DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECANCNOTWISAGN"));
				}
			}
			else
				DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECANCNOTINSREQAGN"));

		}
		#endregion

		#region Creating Customer And Agency Xml 
		private void CreateCustAgencyXML()
		{
			string due_date = DateTime.Now.Month.ToString() + "/" + (DateTime.Now.Day + 5).ToString() + "/" + DateTime.Now.Year.ToString();
			DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetCancellationNoticeData " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion);
			
			#region Policy Agency Part
			XmlElement DecPagePolicyElement;
			DecPagePolicyElement = AcordPDFXML.CreateElement("POLICY");
			DecPageRootElement.AppendChild(DecPagePolicyElement);
			DecPagePolicyElement.SetAttribute(fieldType,fieldTypeSingle);
		
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_nam1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(NamedInsuredWithSuffixs(DSTempDataSet)) + "</canc_nam1>";
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_nam2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString()) +" "+ RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_SUFFIX"].ToString()) + "</canc_nam2>";
			string custaddr = DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString();
			if(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString().Trim() != "")
				custaddr += ", " + DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString();
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_addr1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(custaddr) + "</canc_addr1>";
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_addr2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString() + " " + DSTempDataSet.Tables[0].Rows[0]["STATE_CODE"].ToString() + " " + DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString()) + "</canc_addr2>";
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<AGENC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</AGENC>";

			string agency_add = DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD1"].ToString();
			if(DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD2"].ToString().Trim() != "")
				custaddr += ", " + DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD2"].ToString();

			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<AGENC_ADDR1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(agency_add) + "</AGENC_ADDR1>";
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<AGENC_ADDR2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_CITY"].ToString() + " " + DSTempDataSet.Tables[0].Rows[0]["AGENCY_STATE"].ToString() + " " + DSTempDataSet.Tables[0].Rows[0]["AGENCY_ZIP"].ToString()) + "</AGENC_ADDR2>";
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<AGENC_PHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString()) + "</AGENC_PHONE>";
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<lob " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["LOB_DESC"].ToString()) + "</lob>";
			if(DSTempDataSet.Tables[0].Rows[0]["PROCESS_DATE"] != System.DBNull.Value)
			{
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<dat_cancel " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PROCESS_DATE"].ToString()) + "</dat_cancel>";
				base_due_date= DSTempDataSet.Tables[0].Rows[0]["PROCESS_DATE"].ToString();
			}
			//Ravindra(10-22-2008) iTrack 4831
			//DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<dat_mailing " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DateTime.Now.ToString("MM/dd/yyyy")) + "</dat_mailing>";
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<dat_mailing " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DateTime.Now.AddDays(1).ToString("MM/dd/yyyy")) + "</dat_mailing>";

			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<premDue " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(due_date) + "</premDue>";
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_tot " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["TOTAL_DUE"].ToString()) + "</prem_tot>";
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_min " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["MIN_DUE"].ToString()) + "</prem_min>";
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<polNum " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</polNum>";
			if((DSTempDataSet.Tables[0].Rows[0]["APP_INCEPTION_DATE"] != System.DBNull.Value)&&(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value))
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<polTerm " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Convert.ToDateTime(DSTempDataSet.Tables[0].Rows[0]["APP_INCEPTION_DATE"]).ToString("MM/dd/yyyy")+"-"+Convert.ToDateTime(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"]).ToString("MM/dd/yyyy")) + "</polTerm>";
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<reason_canc " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["REASON"].ToString()) + "</reason_canc>";
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_tot " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</prem_tot>";
			//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_amt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString()) + "</prem_amt>";
			//			if(gCancType == "NSF")
			//			{
			//				if(gInsAgn=="Insured")
			//					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<code " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("CNNSFINNR 11/06") + "</code>";
			//				else
			//					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<code " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("CNNSFAGNR 11/06") + "</code>";
			//			}
			//			if(gCancType.IndexOf("NONPAY") >= 0)
			//			{
			//				if(gInsAgn=="Insured")
			//					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<code " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("CNNPINAR 11/06") + "</code>";
			//				else
			//					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<code " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("CNNPAGAR 11/06") + "</code>";
			//			}
			if(gCancType == "INSREQ")
			{
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<cancel_type " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CANCELLATION_TYPE"].ToString()) + "</cancel_type>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<ret_premium " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["RETURN_PREMIUM"].ToString()) + "</ret_premium>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_option " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CANCELLATION_OPTION"].ToString()) + "</canc_option>";
			}
			if(gCancType == "NSF" || gCancType == "NONPAY" || gCancType == "NONPAYDBMEMO" || gCancType =="")
			{
				if(DSTempDataSet.Tables[0].Rows[0]["RETURN_PREMIUM"].ToString()!=null && DSTempDataSet.Tables[0].Rows[0]["RETURN_PREMIUM"].ToString()!="")
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<premium " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["RETURN_PREMIUM"].ToString()) + "</premium>";
			}
			if(DSTempDataSet.Tables[0].Rows[0]["STATS_FEES"].ToString()!=null && DSTempDataSet.Tables[0].Rows[0]["STATS_FEES"].ToString()!="" && DSTempDataSet.Tables[0].Rows[0]["STATS_FEES"].ToString()!="0" && DSTempDataSet.Tables[0].Rows[0]["STATS_FEES"].ToString()!="0.00")
				if(DSTempDataSet.Tables[0].Rows[0]["STATS_FEES"].ToString()!=null && DSTempDataSet.Tables[0].Rows[0]["STATS_FEES"].ToString()!="" && DSTempDataSet.Tables[0].Rows[0]["STATS_FEES"].ToString()!="0" && DSTempDataSet.Tables[0].Rows[0]["STATS_FEES"].ToString()!="0.00")
				{
					if(DSTempDataSet.Tables[0].Rows[0]["STATS_FEES"].ToString().IndexOf("-")>=0)
					{
						string strStateFee = "";
						strStateFee = DSTempDataSet.Tables[0].Rows[0]["STATS_FEES"].ToString().Replace("-","");
						DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<statistical " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(strStateFee) + "</statistical>";
					}
					else
					{
						DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<statistical " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["STATS_FEES"].ToString()) + "</statistical>";
					}
				}
			try{ notice_amount = double.Parse(DSTempDataSet.Tables[0].Rows[0]["RETURN_PREMIUM"].ToString()); } 
			catch//(Exception ex)
            {}
			#endregion
		}
		#endregion
		#endregion Cancellation Notice

		#region Cancellation Notice Addl Int
		public string getCancNotAIPDFXml(int addlintindex)
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

			if(gCancType == "INSREQ")
				DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAICANCNOTINSREQAGN"));
			else if(gCancType == "NONPAYDBMEMO")
				DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECANCMEMONONPAYDBAI"));
			else
				DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAICANCNOT"));
		}
		#endregion

		#region Creating Customer And Agency Xml 
		private void CreateCustAgencyAIXML(int addlintindex)
		{
			try
			{
				string due_date = DateTime.Now.Month.ToString() + "/" + (DateTime.Now.Day + 5).ToString() + "/" + DateTime.Now.Year.ToString();
				DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetCancellationNoticeData " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion);
			
				#region Policy Agency Part
				string cust_name = DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString() +" "+ DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_SUFFIX"].ToString();

				string cust_add = DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString();
				if(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString().Trim() != "")
					cust_add += ", " + DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString();

				string cust_city = DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString() + " " + DSTempDataSet.Tables[0].Rows[0]["STATE_CODE"].ToString() + " " + DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString();
				string agency_name = DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString();

				string agency_add = DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD1"].ToString();
				if(DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD2"].ToString().Trim() != "")
					cust_add += ", " + DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD2"].ToString();

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
				if(gCancType == "INSREQ")
				{
					DecPagePolicyElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAICANCNOTINSREQAGN"));
					DecPagePolicyElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEAICANCNOTINSREQAGN"));
					DecPagePolicyElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAICANCNOTINSREQAGN"));
				}
				else
				{
					DecPagePolicyElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAICANCNOT"));
					DecPagePolicyElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEAICANCNOT"));
					DecPagePolicyElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAICANCNOT"));
				}
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
					//DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<dat_mailing " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DateTime.Now.ToString("MM/dd/yyyy")) + "</dat_mailing>";
					//Ravindra(10-22-2008) iTrack 4831
					DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<dat_mailing " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DateTime.Now.AddDays(1).ToString("MM/dd/yyyy")) + "</dat_mailing>";
					//			DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<premDue " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(due_date) + "</premDue>";
					//			DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<prem_tot " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["TOTAL_DUE"].ToString()) + "</prem_tot>";
					//			DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<prem_min " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["MIN_DUE"].ToString()) + "</prem_min>";
					DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<polNum " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(pol_num) + "</polNum>";
					if((DSTempDataSet.Tables[0].Rows[0]["APP_INCEPTION_DATE"] != System.DBNull.Value)&&(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value))
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<polTerm " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(app_term) + "</polTerm>";
					DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<reason_canc " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(reason) + "</reason_canc>";
					//			DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<prem_tot " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</prem_tot>";
					//			DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<prem_amt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString()) + "</prem_amt>";
					if(gCancType == "INSREQ")
					{
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<cancel_type " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Insured Request") + "</cancel_type>";
					}
					if(gAILobCode == "HOME" || gAILobCode == "RENT")
					{
						string addAddr = DSTempDwellinAdd.Tables[0].Rows[addlintindex]["ADDRESS"].ToString();
						if(addAddr.Trim().EndsWith(","))
							addAddr = addAddr.Substring(0, addAddr.LastIndexOf(","));
						//						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDITIONALINTERESTNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["RANK"].ToString())+"</ADDITIONALINTERESTNO>"; 
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["HOLDER_NAME"].ToString())+"</ADDL>"; 
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL_ADDR1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(addAddr)+"</ADDL_ADDR1>"; 
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL_ADDR2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["HOLDERCITYSTATEZIP"].ToString())+"</ADDL_ADDR2>"; 
					}
					else if(gAILobCode == "MOT" || gAILobCode == "PPA")
					{
						string addAddr = DSTempDwellinAdd.Tables[0].Rows[addlintindex]["ADDRESS"].ToString();
						if(addAddr.Trim().EndsWith(","))
							addAddr = addAddr.Substring(0, addAddr.LastIndexOf(","));
						//						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDITIONALINTERESTNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["RANK"].ToString())+"</ADDITIONALINTERESTNO>"; 
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["HOLDER_NAME"].ToString())+"</ADDL>"; 
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL_ADDR1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(addAddr)+"</ADDL_ADDR1>"; 
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL_ADDR2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["CITYSTATEZIP"].ToString())+"</ADDL_ADDR2>"; 
					}
					else if(gAILobCode == "WAT")
					{
						string addAddr = DSTempDwellinAdd.Tables[0].Rows[addlintindex]["ADDRESS"].ToString();
						if(addAddr.Trim().EndsWith(","))
							addAddr = addAddr.Substring(0, addAddr.LastIndexOf(","));
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["HOLDER_NAME"].ToString())+"</ADDL>"; 
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL_ADDR1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(addAddr)+"</ADDL_ADDR1>"; 
						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL_ADDR2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["CITYSTATEZIP"].ToString())+"</ADDL_ADDR2>"; 
					}
					DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL_LOANNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["LOAN_REF_NUMBER"].ToString())+"</ADDL_LOANNO>"; 

					RowCounter++;
				}
				#endregion
			}
			catch(Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				throw objExp;
			}
		}
		#endregion
		#endregion Cancellation Notice Addl Int

		#region Cancellation Notice Non Pay DB 
		public string getCancNotNonPayDBPDFXml()
		{
			DSTempDataSet = new DataSet();
			
			gobjSqlHelper = new DataWrapper(ConnStr,CommandType.Text);
			
			SetPDFVersionLobNode("HOME",System.DateTime.Now);
		
			AcordPDFXML = new XmlDocument();
			AcordPDFXML.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <" + RootElement + "/>");

			createRootElementNonPayDBForAllRootPDFs();
			//LoadRateXML();
			FillMonth();

			//creating Xml From Here
			CreateNonPayDBXML();
			createAccHistoryXML();
			createInstallmentHistoryXML();
			creatmaillingpage();

			return AcordPDFXML.OuterXml;
		}

		#region To Create Root Element For All Root PDFs
		private void createRootElementNonPayDBForAllRootPDFs()
		{
			DecPageRootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
			AcordPDFXML.SelectSingleNode(RootElement).AppendChild(DecPageRootElement);

			if(gInsAgn=="Insured")
				DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECANCNOTNONPAYDBINS"));
			else
				DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECANCNOTNONPAYDBAGN"));
		}
		#endregion

		#region Creating Customer And Agency Xml 
		private void CreateNonPayDBXML()
		{
			
			string due_date = DateTime.Now.Month.ToString() + "/" + (DateTime.Now.Day + 5).ToString() + "/" + DateTime.Now.Year.ToString();
			try
			{

				DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetCancellationNoticeNonPayDBData " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion);

				string app_term = "";
				string messg = "";
				if((DSTempDataSet.Tables[0].Rows[0]["APP_INCEPTION_DATE"] != System.DBNull.Value)&&(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value))
					app_term = Convert.ToDateTime(DSTempDataSet.Tables[0].Rows[0]["APP_INCEPTION_DATE"]).ToString("MM/dd/yyyy")+"-"+Convert.ToDateTime(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"]).ToString("MM/dd/yyyy");

				#region Policy Agency Part
				XmlElement DecPagePolicyElement;
				DecPagePolicyElement = AcordPDFXML.CreateElement("POLICY");
				DecPageRootElement.AppendChild(DecPagePolicyElement);
				DecPagePolicyElement.SetAttribute(fieldType,fieldTypeSingle);
		
				if(DSTempDataSet.Tables[0].Rows.Count > 0)
				{
					base_due_date= DSTempDataSet.Tables[0].Rows[0]["DUE_DATE"].ToString();
					try
					{ 
						MinimumDue = double.Parse(DSTempDataSet.Tables[0].Rows[0]["MINIMUM_DUE"].ToString().Replace("$","").Replace(",","")); 
						notice_amount = double.Parse(DSTempDataSet.Tables[0].Rows[0]["TOTAL_DUE"].ToString().Replace("$","").Replace(",","")); 
					} 
					catch//(Exception ex) 
                    {}

					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_nam1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString()) + " "+ RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_SUFFIX"].ToString()) + "</canc_nam1>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_nam2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString()) + " "+ RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_SUFFIX"].ToString()) + "</canc_nam2>";
					string custAddr = DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString();
					if(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString().Trim() != "")
						custAddr += ", " + DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString();
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_addr1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(custAddr) + "</canc_addr1>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_addr2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString() + " " + DSTempDataSet.Tables[0].Rows[0]["STATE_CODE"].ToString() + " " + DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString()) + "</canc_addr2>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_due " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["DUE_DATE"].ToString()) + "</prem_due>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_tot " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["TOTAL_DUE"].ToString()) + "</prem_tot>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_min " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["MINIMUM_DUE"].ToString()) + "</prem_min>";
					//DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_amt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AMOUNT_PAID"].ToString()) + "</prem_amt>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<polNum " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POL_NUMBER"].ToString()) + "</polNum>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<agency " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</agency>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<Agency_Code " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_CODE"].ToString().ToUpper().Trim()) + "</Agency_Code>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<Agency_phone " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString()) + "</Agency_phone>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<nsf_fee " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["NSF_FEE"].ToString()) + "</nsf_fee>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_fee " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["LATE_FEE"].ToString()) + "</canc_fee>";
					try { late_fee = double.Parse(DSTempDataSet.Tables[0].Rows[0]["LATE_FEE"].ToString().Replace("$","").Replace(" ","").Replace(",","").Replace("A","")); } 
					catch(Exception){}
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<ocra " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["OCRA"].ToString()) + "</ocra>";
					//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_amt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString()) + "</prem_amt>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_tot_due " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["TOTAL_DUE"].ToString()) + "</foot_tot_due>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_min_due " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["MINIMUM_DUE"].ToString()) + "</foot_min_due>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_polnumber " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POL_NUMBER"].ToString()) + "</foot_polnumber>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_billdate " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DateTime.Now.ToString("MM/dd/yyyy")) + "</foot_billdate>";

					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_due_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["DUE_DATE"].ToString()) + "</foot_due_date>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_billplan " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["BILL_PLAN"].ToString()) + "</foot_billplan>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_eff_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["EFFECTIVE_DATE"].ToString()) + "</foot_eff_date>";

				
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_poltyp " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_TYPE"].ToString()) + "</foot_poltyp>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_pol_term " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(app_term) + "</foot_pol_term>";
					if((gAILobCode == "HOME") || (gAILobCode == "RENT"))
					{
						DataSet DSLocDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_Dwelling_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'NOTICE'");
						if(DSLocDataSet != null && DSLocDataSet.Tables[0].Rows.Count > 0)
						{
							DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<property " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSLocDataSet.Tables[0].Rows[0]["LOC_ADDRESS"].ToString() + ", " + DSLocDataSet.Tables[0].Rows[0]["LOC_CITYSTATEZIP"].ToString()) + "</property>";
							DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<property_loc " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Property Location:") + "</property_loc>";
						}
					}
				}
				if(DSTempDataSet.Tables.Count >= 5)
				{
					messg = DSTempDataSet.Tables[3].Rows[0]["MESSAGE"].ToString();
					if((DSTempDataSet.Tables[0].Rows[0]["POLICY_STATE_CODE"].ToString() == "MI") && ((gStrLobCode == "AUTO") || (gStrLobCode == "MOT")))
						messg += DSTempDataSet.Tables[4].Rows[0]["MESSAGE_MI"].ToString();
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<messg " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(messg) + "</messg>";
				}
				#endregion
			}
			catch(Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				throw objExp;
			}
	}

		#endregion

		#region code for account history
		private void createAccHistoryXML()
		{
			
			try
			{
				XmlElement DecPageAccHistElement;
				DecPageAccHistElement    = AcordPDFXML.CreateElement("ACCHIST");
				//			int DwellingCtr = 0,AddInt=0;			
			
				#region Account History for DecPage
				DecPageRootElement.AppendChild(DecPageAccHistElement);
				DecPageAccHistElement.SetAttribute(fieldType,fieldTypeMultiple);
				DecPageAccHistElement.SetAttribute(PrimPDF,"");
				DecPageAccHistElement.SetAttribute(PrimPDFBlocks,"12");
				DecPageAccHistElement.SetAttribute(SecondPDF,"");
				DecPageAccHistElement.SetAttribute(SecondPDFBlocks,"12");
				#endregion

				int RowCounter=0;
				if(DSTempDataSet.Tables.Count > 1)
				{
					foreach(DataRow AccountHistory in DSTempDataSet.Tables[1].Rows)
					{
						XmlElement DecPageAccElement;
						DecPageAccElement = AcordPDFXML.CreateElement("ACCROW");
						DecPageAccHistElement.AppendChild(DecPageAccElement);
						DecPageAccElement.SetAttribute(fieldType,fieldTypeNormal);
						DecPageAccElement.SetAttribute(id,RowCounter.ToString());
						if(AccountHistory["BILL_DATE"] != System.DBNull.Value)
							DecPageAccElement.InnerXml= DecPageAccElement.InnerXml + "<acc_date " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Convert.ToDateTime(AccountHistory["BILL_DATE"]).ToString("MM/dd/yyyy"))+"</acc_date>";
						DecPageAccElement.InnerXml= DecPageAccElement.InnerXml + "<acc_activity " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AccountHistory["ACTIVITY_DESC"].ToString())+"</acc_activity>";
						if(AccountHistory["EFF_DATE"] != System.DBNull.Value)
							DecPageAccElement.InnerXml= DecPageAccElement.InnerXml + "<acc_eff " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Convert.ToDateTime(AccountHistory["EFF_DATE"]).ToString("MM/dd/yyyy"))+"</acc_eff>";
						DecPageAccElement.InnerXml= DecPageAccElement.InnerXml + "<acc_amt " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AccountHistory["AMOUNT"].ToString())+"</acc_amt>";
						RowCounter++;
						if(RowCounter > 11)
							break;
					}
				}
			}
			catch(Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				throw objExp;
			}
		}
		#endregion

		#region code for Installment Info
		private void createInstallmentHistoryXML()
		{
			try
			{
				XmlElement DecPageInsHistElement;
				DecPageInsHistElement    = AcordPDFXML.CreateElement("INSHIST");
				//			int DwellingCtr = 0,AddInt=0;			
			
				#region Account History for DecPage
				DecPageRootElement.AppendChild(DecPageInsHistElement);
				DecPageInsHistElement.SetAttribute(fieldType,fieldTypeMultiple);
				DecPageInsHistElement.SetAttribute(PrimPDF,"");
				DecPageInsHistElement.SetAttribute(PrimPDFBlocks,"12");
				DecPageInsHistElement.SetAttribute(SecondPDF,"");
				DecPageInsHistElement.SetAttribute(SecondPDFBlocks,"12");
				#endregion

				int RowCounter=0;
				if(DSTempDataSet.Tables.Count > 2)
				{
					foreach(DataRow InstallmentHistory in DSTempDataSet.Tables[2].Rows)
					{
						XmlElement DecPageInsElement;
						DecPageInsElement = AcordPDFXML.CreateElement("INSROW");
						DecPageInsHistElement.AppendChild(DecPageInsElement);
						DecPageInsElement.SetAttribute(fieldType,fieldTypeNormal);
						DecPageInsElement.SetAttribute(id,RowCounter.ToString());
						try
						{
//							if(RowCounter == 0)
//							{
//								double ins_fee = double.Parse(InstallmentHistory["AMOUNT"].ToString().Replace("$","").Replace(" ","").Replace(",",""));
//								ins_fee += late_fee;
//
//								string insfee = String.Format("{0:0,0.00}", ins_fee);
//
//								DecPageInsElement.InnerXml= DecPageInsElement.InnerXml + "<ins_amt " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$ " + insfee)+"</ins_amt>";
//							}
//							else
								DecPageInsElement.InnerXml= DecPageInsElement.InnerXml + "<ins_amt " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(InstallmentHistory["AMOUNT"].ToString())+"</ins_amt>";
						}
						catch(Exception)
						{}

						DecPageInsElement.InnerXml= DecPageInsElement.InnerXml + "<ins_dat " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(InstallmentHistory["DUE_DATE"].ToString())+"</ins_dat>";
						RowCounter++;
						if(RowCounter > 11)
							break;
					}
				}
			}
			catch(Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				throw objExp;
			}
		}
		#endregion

		#endregion Cancellation Notice Non Pay DB

		#region Cancellation Notice Non Pay DB Addl Int ####Additional####
		public string getCancNotAINonPayDbPDFXml(int addlintindex )
		{
			DSTempDataSet = new DataSet();
			gobjSqlHelper = new DataWrapper(ConnStr,CommandType.Text);
			
			SetPDFVersionLobNode("HOME",System.DateTime.Now);
		
			AcordPDFXML = new XmlDocument();
			AcordPDFXML.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <" + RootElement + "/>");

			createRootElementNonPayDBAIForAllRootPDFs();
			//LoadRateXML();
			FillMonth();
	
			//creating Xml From Here
			CreateNonPayDBAIXML(addlintindex);
			createAccHistoryXML();
			createInstallmentHistoryXML();
			creatmaillingpageAddintrst(addlintindex);
			return AcordPDFXML.OuterXml;
		}

		#region To Create Root Element For All Root PDFs
		private void createRootElementNonPayDBAIForAllRootPDFs()
		{
			DecPageRootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
			AcordPDFXML.SelectSingleNode(RootElement).AppendChild(DecPageRootElement);

			DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECANCNOTNONPAYDBAI"));
		}
		#endregion

		#region Creating Customer And Agency Xml 
		private void CreateNonPayDBAIXML(int addlintindex)
		{
			string due_date = DateTime.Now.Month.ToString() + "/" + (DateTime.Now.Day + 5).ToString() + "/" + DateTime.Now.Year.ToString();
			string canc_nam2 = "";
			string canc_addr1 = "";
			string canc_addr2 = "";
			string nature_of_int = "";
			
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
				if(gAILobCode == "HOME" || gAILobCode == "RENT")
				{
					//						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDITIONALINTERESTNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["RANK"].ToString())+"</ADDITIONALINTERESTNO>"; 
					canc_nam2 = DSTempDwellinAdd.Tables[0].Rows[addlintindex]["HOLDER_NAME"].ToString();
					canc_addr1 = DSTempDwellinAdd.Tables[0].Rows[addlintindex]["ADDRESS"].ToString();
					canc_addr2 = DSTempDwellinAdd.Tables[0].Rows[addlintindex]["HOLDERCITYSTATEZIP"].ToString();
					nature_of_int = DSTempDwellinAdd.Tables[0].Rows[addlintindex]["LOOKUP_VALUE_DESC"].ToString();
				}
				else if(gAILobCode == "MOT" || gAILobCode == "PPA")
				{
					//						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDITIONALINTERESTNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["RANK"].ToString())+"</ADDITIONALINTERESTNO>"; 
					canc_nam2 = DSTempDwellinAdd.Tables[0].Rows[addlintindex]["HOLDER_NAME"].ToString();
					canc_addr1 = DSTempDwellinAdd.Tables[0].Rows[addlintindex]["ADDRESS"].ToString(); 
					canc_addr2 = DSTempDwellinAdd.Tables[0].Rows[addlintindex]["CITYSTATEZIP"].ToString();
				}
				else if(gAILobCode == "WAT")
				{
					canc_nam2 = DSTempDwellinAdd.Tables[0].Rows[addlintindex]["HOLDER_NAME"].ToString(); 
					canc_addr1 = DSTempDwellinAdd.Tables[0].Rows[addlintindex]["ADDRESS"].ToString(); 
					canc_addr2 = DSTempDwellinAdd.Tables[0].Rows[addlintindex]["CITYSTATEZIP"].ToString(); 
				}
			}

			if(canc_addr1.Trim().EndsWith(","))
				canc_addr1 = canc_addr1.Substring(0, canc_addr1.LastIndexOf(","));

			#endregion Addl Int Info

			DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetCancellationNoticeNonPayDBData " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion );
			
			string app_term = "";
			string messg = "";
			if((DSTempDataSet.Tables[0].Rows[0]["APP_INCEPTION_DATE"] != System.DBNull.Value)&&(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value))
				app_term = Convert.ToDateTime(DSTempDataSet.Tables[0].Rows[0]["APP_INCEPTION_DATE"]).ToString("MM/dd/yyyy")+"-"+Convert.ToDateTime(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"]).ToString("MM/dd/yyyy");

			#region Add Int Policy Agency Part
			XmlElement DecPagePolicyElement;
			DecPagePolicyElement = AcordPDFXML.CreateElement("POLICY");
			DecPageRootElement.AppendChild(DecPagePolicyElement);
			DecPagePolicyElement.SetAttribute(fieldType,fieldTypeSingle);
		
			if(DSTempDataSet.Tables[0].Rows.Count > 0)
			{
				base_due_date= DSTempDataSet.Tables[0].Rows[0]["DUE_DATE"].ToString();
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_nam1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</canc_nam1>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_nam2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(canc_nam2) + "</canc_nam2>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_addr1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(canc_addr1) + "</canc_addr1>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_addr2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(canc_addr2) + "</canc_addr2>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_due " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["DUE_DATE"].ToString()) + "</prem_due>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_tot " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["TOTAL_DUE"].ToString()) + "</prem_tot>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_min " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["MINIMUM_DUE"].ToString()) + "</prem_min>";
				//DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_amt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AMOUNT_PAID"].ToString()) + "</prem_amt>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<polNum " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POL_NUMBER"].ToString()) + "</polNum>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<nsf_fee " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["NSF_FEE"].ToString()) + "</nsf_fee>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_fee " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["LATE_FEE"].ToString()) + "</canc_fee>";
				try{late_fee = double.Parse(DSTempDataSet.Tables[0].Rows[0]["LATE_FEE"].ToString().Replace("$","").Replace(" ","").Replace(",","").Replace("A",""));}
				catch(Exception){}
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<agency " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</agency>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<Agency_Code " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_CODE"].ToString().ToUpper().Trim()) + "</Agency_Code>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<Agency_phone " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString()) + "</Agency_phone>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<ocra " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["OCRA"].ToString()) + "</ocra>";
				//			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_amt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString()) + "</prem_amt>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_tot_due " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["TOTAL_DUE"].ToString()) + "</foot_tot_due>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_min_due " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["MINIMUM_DUE"].ToString()) + "</foot_min_due>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_polnumber " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POL_NUMBER"].ToString()) + "</foot_polnumber>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_billdate " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DateTime.Now.ToString("MM/dd/yyyy")) + "</foot_billdate>";

				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_due_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["DUE_DATE"].ToString()) + "</foot_due_date>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_billplan " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["BILL_PLAN"].ToString()) + "</foot_billplan>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_eff_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["EFFECTIVE_DATE"].ToString()) + "</foot_eff_date>";

				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_poltyp " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_TYPE"].ToString()) + "</foot_poltyp>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_pol_term " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(app_term) + "</foot_pol_term>";
				if((gAILobCode == "HOME") || (gAILobCode == "RENT"))
				{
					DataSet DSLocDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_Dwelling_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'NOTICE'");
					if(DSLocDataSet != null && DSLocDataSet.Tables[0].Rows.Count > 0)
					{
						DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<property " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSLocDataSet.Tables[0].Rows[0]["LOC_ADDRESS"].ToString() + ", " + DSLocDataSet.Tables[0].Rows[0]["LOC_CITYSTATEZIP"].ToString()) + "</property>";
						DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<property_loc " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Property Location:") + "</property_loc>";
					}
				}
			}
			if(DSTempDataSet.Tables.Count >= 5)
			{
				messg = DSTempDataSet.Tables[3].Rows[0]["MESSAGE"].ToString();
				if((DSTempDataSet.Tables[0].Rows[0]["POLICY_STATE_CODE"].ToString() == "MI") && ((gStrLobCode == "AUTO") || (gStrLobCode == "MOT")))
					messg += DSTempDataSet.Tables[4].Rows[0]["MESSAGE_MI"].ToString();
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<messg " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(messg) + "</messg>";
			}

			if(((gAILobCode == "HOME") || (gAILobCode == "RENT")) && (nature_of_int.IndexOf("Lien Holder") >= 0))
			{
				XmlElement DecPageLeinBackElement;
				DecPageLeinBackElement    = AcordPDFXML.CreateElement("LEINHOLDERBACK");
				//			int DwellingCtr = 0,AddInt=0;			
			
				#region Leinholder additional wording for Canc Notice
				DecPageRootElement.AppendChild(DecPageLeinBackElement);
				DecPageLeinBackElement.SetAttribute(fieldType,fieldTypeMultiple);
				DecPageLeinBackElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGENONPAYDBBK"));
				DecPageLeinBackElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGENONPAYDBBK"));
				DecPageLeinBackElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGENONPAYDBBKEXTN"));
				DecPageLeinBackElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGENONPAYDBBKEXTN"));
				#endregion Leinholder additional wording for Canc Notice

				XmlElement DecPageLeinElement;
				DecPageLeinElement = AcordPDFXML.CreateElement("LEINHOLDER");
				DecPageLeinBackElement.AppendChild(DecPageLeinElement);
				DecPageLeinElement.SetAttribute(fieldType,fieldTypeNormal);
				DecPageLeinElement.SetAttribute(id,RowCounter.ToString());
			}
			#endregion
		}
		#endregion

		#endregion Cancellation Notice Non Pay DB Addl Int

		#region Cert of Mail Addl Int
		public string getCertMailAIPDFXml(int addlintindex)
		{
			DSTempDataSet = new DataSet();
			gobjSqlHelper = new DataWrapper(ConnStr,CommandType.Text);
			SetPDFVersionLobNode("HOME",System.DateTime.Now);
		
			AcordPDFXML = new XmlDocument();
			AcordPDFXML.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <" + RootElement + "/>");

			createRootElementAICertMailForAllRootPDFs();
			//LoadRateXML();
			FillMonth();

			//creating Xml From Here
			CreateAICertMailXML(addlintindex);
			creatmaillingpageAddintrst(addlintindex);
			return AcordPDFXML.OuterXml;
		}

		#region To Create Root Element For All Root PDFs
		private void createRootElementAICertMailForAllRootPDFs()
		{
			DecPageRootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
			AcordPDFXML.SelectSingleNode(RootElement).AppendChild(DecPageRootElement);

			DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAICERTMAIL"));
		}
		#endregion

		#region Creating Addl Int Xml 
		private void CreateAICertMailXML(int addlintindex)
		{
			string due_date = DateTime.Now.Month.ToString() + "/" + (DateTime.Now.Day + 5).ToString() + "/" + DateTime.Now.Year.ToString();
			
			#region Policy Agency Part
			XmlElement DecPagePolicyElement;
			DecPagePolicyElement = AcordPDFXML.CreateElement("CERTMAIL");
			DecPageRootElement.AppendChild(DecPagePolicyElement);
			DecPagePolicyElement.SetAttribute(fieldType,fieldTypeMultiple);
			DecPagePolicyElement.SetAttribute(PrimPDF,"");
			DecPagePolicyElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAICERTMAIL"));
			DecPagePolicyElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEAICERTMAIL"));
			DecPagePolicyElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAICERTMAIL"));
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

				if(gAILobCode == "HOME" || gAILobCode == "RENT")
				{
					//						DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDITIONALINTERESTNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["RANK"].ToString())+"</ADDITIONALINTERESTNO>"; 
					DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["HOLDER_NAME"].ToString())+"</ADDL>"; 
					string addl_addr = DSTempDwellinAdd.Tables[0].Rows[addlintindex]["ADDRESS"].ToString();
					if(addl_addr.Trim().EndsWith(","))
						addl_addr = addl_addr.Substring(0,addl_addr.LastIndexOf(","));
					DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL_ADDR1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(addl_addr)+"</ADDL_ADDR1>"; 
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
					DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL_ADDR2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["CITYSTATEZIP"].ToString())+"</ADDL_ADDR2>"; 
				}
				else if(gAILobCode == "WAT")
				{
					DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["HOLDER_NAME"].ToString())+"</ADDL>"; 
					string addl_addr = DSTempDwellinAdd.Tables[0].Rows[addlintindex]["ADDRESS"].ToString();
					if(addl_addr.Trim().EndsWith(","))
						addl_addr = addl_addr.Substring(0,addl_addr.LastIndexOf(","));
					DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL_ADDR1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(addl_addr)+"</ADDL_ADDR1>"; 
					DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +"<ADDL_ADDR2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellinAdd.Tables[0].Rows[addlintindex]["CITYSTATEZIP"].ToString())+"</ADDL_ADDR2>"; 
				}
				DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetCancellationNoticeData " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion);
				if(DSTempDataSet.Tables[0].Rows[0]["PROCESS_DATE"] != System.DBNull.Value)
					DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<file_policy " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("File: " + DSTempDataSet.Tables[0].Rows[0]["POL_NUMBER"].ToString() + " / " + DSTempDataSet.Tables[0].Rows[0]["PROCESS_DATE"].ToString() + " / " +  DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</file_policy>";
				else
					DecPageAddlInts.InnerXml = DecPageAddlInts.InnerXml +  "<file_policy " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("File: " + DSTempDataSet.Tables[0].Rows[0]["POL_NUMBER"].ToString() + " / " + DateTime.Now.ToString("MM/dd/yyyy") + " / " +  DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</file_policy>";
				
				RowCounter++;
			}
			#endregion
		}
		#endregion
		#endregion Cert of Mail Addl Int

		#region Certificate of Mail
		public string getCertMailPDFXml()
		{
			DSTempDataSet = new DataSet();
			gobjSqlHelper = new DataWrapper(ConnStr,CommandType.Text);
			SetPDFVersionLobNode("HOME",System.DateTime.Now);
		
			AcordPDFXML = new XmlDocument();
			AcordPDFXML.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <" + RootElement + "/>");

			createRootElementForAllCertMailRootPDFs();
			//LoadRateXML();
			FillMonth();

			//creating Xml From Here
			CreateCertMailXML();
			//creatmaillingpageAddintrst(addlintindex);
			return AcordPDFXML.OuterXml;
		}

		#region To Create Root Element For All Root PDFs
		private void createRootElementForAllCertMailRootPDFs()
		{
			DecPageRootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
			AcordPDFXML.SelectSingleNode(RootElement).AppendChild(DecPageRootElement);

			if(gInsAgn == "Insured")
				DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECERTMAILINS"));
			else
				DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECERTMAIL"));
		}
		#endregion

		#region Creating Customer And Agency Xml 
		private void CreateCertMailXML()
		{
			string due_date = DateTime.Now.Month.ToString() + "/" + (DateTime.Now.Day + 5).ToString() + "/" + DateTime.Now.Year.ToString();
			DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetCancellationNoticeData " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion);
			
			#region Policy Agency Part
			XmlElement DecPagePolicyElement;
			DecPagePolicyElement = AcordPDFXML.CreateElement("CERTMAIL");
			DecPageRootElement.AppendChild(DecPagePolicyElement);
			DecPagePolicyElement.SetAttribute(fieldType,fieldTypeSingle);
		
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_nam1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</canc_nam1>";
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_nam2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString())+" "+ RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_SUFFIX"].ToString()) + "</canc_nam2>";
			if(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString() !="")
			{
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_addr1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString() + ", " + DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString()) + "</canc_addr1>";	
			}
			else
			{
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_addr1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString()) + "</canc_addr1>";
			}				
			DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_addr2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString() + " " + DSTempDataSet.Tables[0].Rows[0]["STATE_CODE"].ToString() + " " + DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString()) + "</canc_addr2>";
			if(DSTempDataSet.Tables[0].Rows[0]["PROCESS_DATE"] != System.DBNull.Value)
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<file_policy " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("File: " + DSTempDataSet.Tables[0].Rows[0]["POL_NUMBER"].ToString() + " / " + DSTempDataSet.Tables[0].Rows[0]["PROCESS_DATE"].ToString() + " / " +  DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</file_policy>";
			else
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<file_policy " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("File: " + DSTempDataSet.Tables[0].Rows[0]["POL_NUMBER"].ToString() + " / " + DateTime.Now.ToString("MM/dd/yyyy") + " / " +  DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</file_policy>";

			DataSet DSFeesDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetSystemParams");
			
			if(DSFeesDataSet != null && DSFeesDataSet.Tables[0].Rows.Count > 0)
			{
				int fees_0 = Convert.ToInt32(DSFeesDataSet.Tables[0].Rows[0]["POSTAGE_FEE"].ToString());
				int fees_1 = Convert.ToInt32(DSFeesDataSet.Tables[0].Rows[0]["RESTR_DELIV_FEE"].ToString());
				int fees_2 = Convert.ToInt32(DSFeesDataSet.Tables[0].Rows[0]["CERTIFIED_FEE"].ToString());
				int fees_3 = Convert.ToInt32(DSFeesDataSet.Tables[0].Rows[0]["RET_RECEIPT_FEE"].ToString());
				int fees_4 = fees_0 + fees_1 + fees_2 + fees_3;

				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<fees0 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(convertDoubleCurrency(fees_0)) + "</fees0>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<fees1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(convertDoubleCurrency(fees_1)) + "</fees1>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<fees2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(convertDoubleCurrency(fees_2)) + "</fees2>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<fees3 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(convertDoubleCurrency(fees_3)) + "</fees3>";
				DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<fees4 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(convertDoubleCurrency(fees_4)) + "</fees4>";
			}
			#endregion
		}

		private string convertDoubleCurrency(int fees)
		{
			int fees_adecimal = fees % 100;
			int fees_bdecimal = fees /100;

			string strfees = "$";
			strfees += fees_bdecimal.ToString() + ".";
			if(fees_adecimal.ToString().Length == 1)
				strfees += "0";
			strfees += fees_adecimal.ToString();

			return strfees;
		}

		#endregion
		#endregion Certificate of Mail




		/********* Functions Added by Ravindra(03-13-2008) For Past Due Notices DB  *****************/

		public string GetNonPayDBXMLForInsured(DataSet dsNonPayDB, string PropertyAddress)
		{
			bool ShowInstallments = false;
			if(	dsNonPayDB.Tables[0].Rows[0]["SHOW_INS_SCHEDULE"].ToString().Trim() == "Y")
			{
				ShowInstallments = true;
			}


			string Name = dsNonPayDB.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString()+" "+ dsNonPayDB.Tables[0].Rows[0]["CUSTOMER_SUFFIX"].ToString()  ;

			string Addr1 = dsNonPayDB.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString();
			if(dsNonPayDB.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString().Trim() != "")
				Addr1 += ", " + dsNonPayDB.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString();
			
			Addr1 = Addr1;
		
			string Addr2 = 	dsNonPayDB.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString() + " " + dsNonPayDB.Tables[0].Rows[0]["STATE_CODE"].ToString() + " " + dsNonPayDB.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString() ;
	
			CreateCommonElementsForNonPayDB("INSURED");

			CreateNonPayMainXML(dsNonPayDB  , Name , Addr1 , Addr2 , PropertyAddress,ShowInstallments,"I");

            AddAccountHistory(dsNonPayDB);
			
			if(ShowInstallments)
			{
				AddInstallmentSchedule(dsNonPayDB);
			}
			creatmaillingpage(dsNonPayDB);
			return AcordPDFXML.OuterXml;

		}

		
		public string GetNonPayDBXMLForAgent(DataSet dsNonPayDB, string PropertyAddress)
		{
			
			bool ShowInstallments = false;
			if(	dsNonPayDB.Tables[0].Rows[0]["SHOW_INS_SCHEDULE"].ToString().Trim() == "Y")
			{
				ShowInstallments = true;
			}


			string Name = dsNonPayDB.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString()+" "+dsNonPayDB.Tables[0].Rows[0]["CUSTOMER_SUFFIX"].ToString() ;

			string Addr1 = dsNonPayDB.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString();
			if(dsNonPayDB.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString().Trim() != "")
				Addr1 += ", " + dsNonPayDB.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString();
			
			Addr1 = Addr1;
		
			string Addr2 = 	dsNonPayDB.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString() + " " + dsNonPayDB.Tables[0].Rows[0]["STATE_CODE"].ToString() + " " + dsNonPayDB.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString() ;
	
			CreateCommonElementsForNonPayDB("AGENCY");

			CreateNonPayMainXML(dsNonPayDB  , Name , Addr1 , Addr2 , PropertyAddress,ShowInstallments,"A");

			AddAccountHistory(dsNonPayDB);

			if(ShowInstallments)
			{
				AddInstallmentSchedule(dsNonPayDB);
			}
			creatmaillingpage(dsNonPayDB);
			return AcordPDFXML.OuterXml;
		}

		
		public string GetNonPayDBXMLForAddIntrest(DataSet dsNonPayDB , string HolderName , string Address1 , string Address2 ,string PropertyAddress, bool AddWordings)
		{
			bool ShowInstallments = false;
			if(	dsNonPayDB.Tables[0].Rows[0]["SHOW_INS_SCHEDULE"].ToString().Trim() == "Y")
			{
				ShowInstallments = true;
			}

			CreateCommonElementsForNonPayDB("ADDINT");

			CreateNonPayMainXML(dsNonPayDB  , HolderName , Address1 , Address2 , PropertyAddress,ShowInstallments,"L");

			if(AddWordings)
			{
				XmlElement DecPageLeinBackElement;
				DecPageLeinBackElement    = AcordPDFXML.CreateElement("LEINHOLDERBACK");
				DecPageRootElement.AppendChild(DecPageLeinBackElement);
				DecPageLeinBackElement.SetAttribute(fieldType,fieldTypeMultiple);
				DecPageLeinBackElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGENONPAYDBBK"));
				DecPageLeinBackElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGENONPAYDBBK"));
				DecPageLeinBackElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGENONPAYDBBKEXTN"));
				DecPageLeinBackElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGENONPAYDBBKEXTN"));
		
				XmlElement DecPageLeinElement;
				DecPageLeinElement = AcordPDFXML.CreateElement("LEINHOLDER");
				DecPageLeinBackElement.AppendChild(DecPageLeinElement);
				DecPageLeinElement.SetAttribute(fieldType,fieldTypeNormal);
				DecPageLeinElement.SetAttribute(id,"0");
			}

			AddAccountHistory(dsNonPayDB);
			
			if(ShowInstallments)
			{
				AddInstallmentSchedule(dsNonPayDB);
			}
			creatmaillingpageAddintrst(HolderName , Address1 , Address2);
			return AcordPDFXML.OuterXml;
		}

		
		#region Private Utility Functions For Non Pay DB

		private void CreateNonPayMainXML(DataSet dsNonPayDB , string Name , string Address1 , string Address2 ,string PropertyAddress,
				bool ShowInstallments, string CalledFor )
		{
			try
			{
				string app_term = "" , PolNum;
				string messg = "";
				if((dsNonPayDB.Tables[0].Rows[0]["APP_INCEPTION_DATE"] != System.DBNull.Value)&&(dsNonPayDB.Tables[0].Rows[0]["APP_EXPIRATION_DATE"] != System.DBNull.Value))
					app_term = Convert.ToDateTime(dsNonPayDB.Tables[0].Rows[0]["APP_INCEPTION_DATE"]).ToString("MM/dd/yyyy")+"-"+Convert.ToDateTime(dsNonPayDB.Tables[0].Rows[0]["APP_EXPIRATION_DATE"]).ToString("MM/dd/yyyy");

				XmlElement DecPagePolicyElement;
				DecPagePolicyElement = AcordPDFXML.CreateElement("POLICY");
				DecPageRootElement.AppendChild(DecPagePolicyElement);
				DecPagePolicyElement.SetAttribute(fieldType,fieldTypeSingle);
		
				if(dsNonPayDB.Tables[0].Rows.Count > 0)
				{
					base_due_date= dsNonPayDB.Tables[0].Rows[0]["DUE_DATE"].ToString();
					
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_nam1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Name) + "</canc_nam1>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_nam2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Name) + "</canc_nam2>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_addr1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Address1) + "</canc_addr1>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_addr2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Address2)  + "</canc_addr2>";
				
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_due " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNonPayDB.Tables[0].Rows[0]["DUE_DATE"].ToString()) + "</prem_due>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_tot " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNonPayDB.Tables[0].Rows[0]["TOTAL_DUE"].ToString()) + "</prem_tot>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<prem_min " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNonPayDB.Tables[0].Rows[0]["MINIMUM_DUE"].ToString()) + "</prem_min>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<polNum " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNonPayDB.Tables[0].Rows[0]["POL_NUMBER"].ToString()) + "</polNum>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<agency " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNonPayDB.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</agency>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<Agency_Code " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNonPayDB.Tables[0].Rows[0]["AGENCY_CODE"].ToString().ToUpper().Trim()) + "</Agency_Code>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<Agency_phone " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNonPayDB.Tables[0].Rows[0]["AGENCY_PHONE"].ToString()) + "</Agency_phone>";

					//Ravindra(12-3-2008) iTrack 5009
//					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<nsf_fee " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNonPayDB.Tables[0].Rows[0]["NSF_FEE"].ToString()) + "</nsf_fee>";
//					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_fee " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNonPayDB.Tables[0].Rows[0]["LATE_FEE"].ToString()) + "</canc_fee>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<nsf_fee " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNonPayDB.Tables[0].Rows[0]["NSF_MESSAGE"].ToString()) + "</nsf_fee>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<canc_fee " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNonPayDB.Tables[0].Rows[0]["LF_MESSAGE"].ToString()) + "</canc_fee>";

					try 
					{ 
						late_fee = double.Parse(dsNonPayDB.Tables[0].Rows[0]["LATE_FEE"].ToString().Replace("$","").Replace(" ","").Replace(",","").Replace("A","")); 
					} 
					catch(Exception)
					{}
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<ocra " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNonPayDB.Tables[0].Rows[0]["OCRA"].ToString()) + "</ocra>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_tot_due " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNonPayDB.Tables[0].Rows[0]["TOTAL_DUE"].ToString()) + "</foot_tot_due>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_min_due " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNonPayDB.Tables[0].Rows[0]["MINIMUM_DUE"].ToString()) + "</foot_min_due>";
					
					PolNum = dsNonPayDB.Tables[0].Rows[0]["POL_NUMBER"].ToString();

					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_polnumber " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNonPayDB.Tables[0].Rows[0]["POL_NUMBER"].ToString()) + "</foot_polnumber>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_billdate " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DateTime.Now.ToString("MM/dd/yyyy")) + "</foot_billdate>";

					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_due_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNonPayDB.Tables[0].Rows[0]["DUE_DATE"].ToString()) + "</foot_due_date>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_billplan " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNonPayDB.Tables[0].Rows[0]["BILL_PLAN"].ToString()) + "</foot_billplan>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_eff_date " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNonPayDB.Tables[0].Rows[0]["EFFECTIVE_DATE"].ToString()) + "</foot_eff_date>";

				
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_poltyp " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNonPayDB.Tables[0].Rows[0]["POLICY_TYPE"].ToString()) + "</foot_poltyp>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<foot_pol_term " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(app_term) + "</foot_pol_term>";
					
					

					if(PropertyAddress != "")
					{
						DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<property " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(PropertyAddress) + "</property>";
						DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<property_loc " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Property Location:") + "</property_loc>";
					}

				}
				if(dsNonPayDB.Tables.Count >= 5)
				{
					messg = dsNonPayDB.Tables[3].Rows[0]["MESSAGE"].ToString();
					//if((stCode == "MI") && ((gStrLobCode == "AUTO") || (gStrLobCode == "MOT")))
					if(
							(dsNonPayDB.Tables[0].Rows[0]["POLICY_STATE_CODE"].ToString().Trim() == "MI" )
							&& ( (dsNonPayDB.Tables[0].Rows[0]["LOB_CODE"].ToString().Trim() == "AUTOP" ) 
									|| (dsNonPayDB.Tables[0].Rows[0]["LOB_CODE"].ToString().Trim() == "CYCL" ))
						)
						messg += dsNonPayDB.Tables[4].Rows[0]["MESSAGE_MI"].ToString();
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<messg " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(messg) + "</messg>";

					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<message_ins " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsNonPayDB.Tables[3].Rows[0]["INS_MESSAGE"].ToString()) + "</message_ins>";

				}
				if(ShowInstallments)
				{
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<ins_header " + fieldType + "=\"" + fieldTypeText + "\">Installment Schedule</ins_header>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<ins_date_h " + fieldType + "=\"" + fieldTypeText + "\">Due Date</ins_date_h>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<ins_amt_h " + fieldType + "=\"" + fieldTypeText + "\">Amount</ins_amt_h>";
				}
				else
				{
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<ins_header " + fieldType + "=\"" + fieldTypeText + "\"></ins_header>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<ins_date_h " + fieldType + "=\"" + fieldTypeText + "\"></ins_date_h>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<ins_amt_h " + fieldType + "=\"" + fieldTypeText + "\"></ins_amt_h>";
				}

				int BillMortgagee = Convert.ToInt32(dsNonPayDB.Tables[0].Rows[0]["BILL_MORTAGAGEE"]);
				//Ravindra(10-20-2008): Condition was incorrect
				if((CalledFor == "I" || CalledFor == "A" )  && BillMortgagee > 0)  //== 1 )
				{
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<message_lienholder " + fieldType + "=\"" + fieldTypeText + "\"> Your Mortgage Company has been billed for the Premium Due.</message_lienholder>";
				}
				else
				{
						DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<message_lienholder " + fieldType + "=\"" + fieldTypeText + "\"></message_lienholder>";
				}

				//On lien holders copy display insured details
				//Ravindra(07-21-2009): iTrack 5692
				if(CalledFor == "L" ) //&& BillMortgagee > 0) // == 1)
				{
					string InsuredName = RemoveJunkXmlCharacters(dsNonPayDB.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString()) +" "+RemoveJunkXmlCharacters(dsNonPayDB.Tables[0].Rows[0]["CUSTOMER_SUFFIX"].ToString()) ;

					string InsuredAddr1 = dsNonPayDB.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString();
					if(dsNonPayDB.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString().Trim() != "")
						InsuredAddr1 += ", " + dsNonPayDB.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString();
			
					InsuredAddr1 = RemoveJunkXmlCharacters(InsuredAddr1);
		
					string InsuredAddr2 = 	RemoveJunkXmlCharacters(dsNonPayDB.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString() + " " + dsNonPayDB.Tables[0].Rows[0]["STATE_CODE"].ToString() + " " + dsNonPayDB.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString()) ;
					//BillMortgagee Condition  Added For Itrack Issue #5692.
					if(BillMortgagee > 0)
					{
						DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<insured_msg " + fieldType + "=\"" + fieldTypeText + "\">Mortgagee Pays Premium For:</insured_msg>";
					}
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<insured_name " + fieldType + "=\"" + fieldTypeText + "\">" + InsuredName + "</insured_name>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<insured_addr1 " + fieldType + "=\"" + fieldTypeText + "\">"+ InsuredAddr1  +"</insured_addr1>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<insured_addr2 " + fieldType + "=\"" + fieldTypeText + "\">" + InsuredAddr2  + "</insured_addr2>";
	
				}
			}
			catch(Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				throw objExp;
			}
		}

		
		private void AddInstallmentSchedule(DataSet dsNonPayDB)
		{
			try
			{
				XmlElement DecPageInsHistElement;
				DecPageInsHistElement    = AcordPDFXML.CreateElement("INSHIST");
				DecPageRootElement.AppendChild(DecPageInsHistElement);
				DecPageInsHistElement.SetAttribute(fieldType,fieldTypeMultiple);
				DecPageInsHistElement.SetAttribute(PrimPDF,"");
				DecPageInsHistElement.SetAttribute(PrimPDFBlocks,"12");
				DecPageInsHistElement.SetAttribute(SecondPDF,"");
				DecPageInsHistElement.SetAttribute(SecondPDFBlocks,"12");
	
				int RowCounter=0;
				if(dsNonPayDB.Tables.Count > 2)
				{
					foreach(DataRow InstallmentHistory in dsNonPayDB.Tables[2].Rows)
					{
						XmlElement DecPageInsElement;
						DecPageInsElement = AcordPDFXML.CreateElement("INSROW");
						DecPageInsHistElement.AppendChild(DecPageInsElement);
						DecPageInsElement.SetAttribute(fieldType,fieldTypeNormal);
						DecPageInsElement.SetAttribute(id,RowCounter.ToString());
						try
						{
							DecPageInsElement.InnerXml= DecPageInsElement.InnerXml + "<ins_amt " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(InstallmentHistory["AMOUNT"].ToString())+"</ins_amt>";
						}
						catch(Exception)
						{}

						DecPageInsElement.InnerXml= DecPageInsElement.InnerXml + "<ins_dat " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(InstallmentHistory["DUE_DATE"].ToString())+"</ins_dat>";
						RowCounter++;
						if(RowCounter > 11)
							break;
					}
				}
			}
			catch(Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				throw objExp;
			}
		}

		
		private void AddAccountHistory(DataSet dsNonPayDB)
		{
			
			try
			{
				XmlElement DecPageAccHistElement;
				DecPageAccHistElement    = AcordPDFXML.CreateElement("ACCHIST");
				DecPageRootElement.AppendChild(DecPageAccHistElement);
				DecPageAccHistElement.SetAttribute(fieldType,fieldTypeMultiple);
				DecPageAccHistElement.SetAttribute(PrimPDF,"");
				DecPageAccHistElement.SetAttribute(PrimPDFBlocks,"12");
				DecPageAccHistElement.SetAttribute(SecondPDF,"");
				DecPageAccHistElement.SetAttribute(SecondPDFBlocks,"12");
			
				int RowCounter=0;
				if(dsNonPayDB.Tables.Count > 1)
				{
					foreach(DataRow AccountHistory in dsNonPayDB.Tables[1].Rows)
					{
						XmlElement DecPageAccElement;
						DecPageAccElement = AcordPDFXML.CreateElement("ACCROW");
						DecPageAccHistElement.AppendChild(DecPageAccElement);
						DecPageAccElement.SetAttribute(fieldType,fieldTypeNormal);
						DecPageAccElement.SetAttribute(id,RowCounter.ToString());
						if(AccountHistory["BILL_DATE"] != System.DBNull.Value)
							DecPageAccElement.InnerXml= DecPageAccElement.InnerXml + "<acc_date " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Convert.ToDateTime(AccountHistory["BILL_DATE"]).ToString("MM/dd/yyyy"))+"</acc_date>";
						DecPageAccElement.InnerXml= DecPageAccElement.InnerXml + "<acc_activity " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AccountHistory["ACTIVITY_DESC"].ToString())+"</acc_activity>";
						if(AccountHistory["EFF_DATE"] != System.DBNull.Value)
							DecPageAccElement.InnerXml= DecPageAccElement.InnerXml + "<acc_eff " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Convert.ToDateTime(AccountHistory["EFF_DATE"]).ToString("MM/dd/yyyy"))+"</acc_eff>";
						DecPageAccElement.InnerXml= DecPageAccElement.InnerXml + "<acc_amt " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AccountHistory["AMOUNT"].ToString())+"</acc_amt>";
						RowCounter++;
						if(RowCounter > 11)
							break;
					}
				}
			}
			catch(Exception objExp)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				throw objExp;
			}
		}

		
		private void CreateCommonElementsForNonPayDB(string CalledFrom)
		{
			SetPDFVersionLobNode("HOME",System.DateTime.Now);
		
			AcordPDFXML = new XmlDocument();
			AcordPDFXML.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <" + RootElement + "/>");
			
			DecPageRootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
			AcordPDFXML.SelectSingleNode(RootElement).AppendChild(DecPageRootElement);

			if(CalledFrom == "INSURED")
			{
				DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECANCNOTNONPAYDBINS"));
				FillMonth();
			}
			else if(CalledFrom == "AGENCY")
			{
				DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECANCNOTNONPAYDBAGN"));
			}
			else
			{
				DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECANCNOTNONPAYDBAI"));
			}

		}
		#endregion

		/************ Added by ravindra ends here ***************************/
	}
}