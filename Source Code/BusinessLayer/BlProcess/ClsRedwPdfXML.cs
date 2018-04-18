using System;
using System.Xml;
using System.Data;
using Cms.DataLayer;
using System.Collections;  

namespace Cms.BusinessLayer.BlProcess
{
	/// <summary>
	/// <CreatedBy>Deepak Gupta</CreatedBy>
	/// <Dated>03-July-2006</Dated>
	/// <Purpose>To Create XML for Acord84 PDF for REDW LOB</Purpose>
	/// </summary>
	public class ClsRedwPdfXML : ClsCommonPdfXML
	{
		#region Declarations
		private XmlElement DecPageRootElement;
		private XmlElement Acord84RootElement;
		private XmlElement SupplementalRootElement;
		private Hashtable htpremium=new Hashtable(); 
		private Hashtable htpremium_dis=new Hashtable(); 
		private Hashtable htpremium_sur=new Hashtable(); 
		//double sumtotal=0;
		string gstrGetPremium="0";
		string goldVewrsionId="0";
		int gintGetindex=0;
		private DataWrapper gobjWrapper;
		public DataSet DSTempDataSet1;
		private string ApplicantName1 = "";
		private string locAdd1,locAdd2,locCity,locState,locZip;
		private string stCode="";
		private string strPolicyNum=""; 
		string appAddress="", appCityState="";
		private string []prnOrd_covCode;
		private string []prnOrd_attFile;
		private int []prnOrd;
		int intPrivacyPage = 0;
		int totdwellpage=0,totcovPage = 0;

		#endregion

		#region Constructor
		public ClsRedwPdfXML(string lstrClientId,string lstrPolicyId,string lstrVersionId,string lStrCalledFrom,string lStrCalledFor,string stateCode,string strProcessID,string Agn_Ins,string temp,DataWrapper LobjWrapper)
		{
			gStrClientID=lstrClientId;
			gStrPolicyId=lstrPolicyId;
			gStrPolicyVersion=lstrVersionId;
			gStrCalledFrom=lStrCalledFrom.ToUpper().Trim();
			gStrPdfFor=lStrCalledFor.ToUpper().Trim();
			stCode=stateCode;
			gStrProcessID = strProcessID;
			gStrCopyTo = Agn_Ins;
			gStrtemp = temp;
			if(Agn_Ins != null && Agn_Ins != "")
			{
				string []copyTo = Agn_Ins.Split('-');
				if(copyTo[0] == "CUSTOMER")
					CopyTo = "CUSTOMER'S COPY";
				else if(copyTo[0] == "AGENCY")
					CopyTo = "AGENT'S COPY";
			}
			this.gobjWrapper = LobjWrapper;
			DSTempDataSet = new DataSet();
			DSTempDataSet1 = new DataSet();
			//gobjSqlHelper = new DataWrapper(ConnStr,CommandType.Text);
			
			gobjWrapper.ClearParameteres();
			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFPolicyDetails");
			gobjWrapper.ClearParameteres();

			//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFPolicyDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			if(DSTempDataSet.Tables[0].Rows.Count>0)
			{
				SetPDFVersionLobNode("REDW",DateTime.Parse(DSTempDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
				SetPDFInsScoresLobNode("REDW",DateTime.Parse(DSTempDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
				goldVewrsionId=DSTempDataSet.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString();
			}
		}
		#endregion

		public string getRentalAcordPDFXml()
		{
			try
			{
				AcordPDFXML = new XmlDocument();
				AcordPDFXML.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <" + RootElement + "/>");
				
				createRootElementForAllRootPDFs();
				
				LoadRateXML("REDW",gobjWrapper);
				//creating Xml From Here
				CreatePolicyAgencyXML();
			
				CreateNamedInsuredCoAppXml();
				CreatePriorPolicyCoverage();
			
				createDwellingUnderwritingGeneralXML();
			
				createRentalDwellingXML();
				CreatePriorLossXml();
				createAcord84HomeAddlIntXml();
			
				if (gStrCopyTo == "CUSTOMER-NOWORD"  &&					
					(gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS.ToString() ||gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS.ToString() 
					|| gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RENEWAL_PROCESS.ToString() || gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RENEWAL_PROCESS.ToString()
					)
					) 
				{
					RemoveEnorsementWordings();
					createEndorsementWordings();
				}
				if(gStrCopyTo != "AGENCY" && gStrCopyTo != "CUSTOMER-NOWORD")
					createEndorsementWordings();
				
				if(gStrCopyTo == "CUSTOMER" || gStrCopyTo == "CUSTOMER-NOWORD")
					createAddWordingsXML();
				if(gStrCopyTo != "AGENCY")
				{
					creatmaillingpage();
				}
				
					string customerFullxml="";
					customerFullxml=AcordPDFXML.OuterXml;
					InsertCustomerFullWordingXml(gStrClientID,gStrPolicyId,gStrPolicyVersion,gStrCopyTo,customerFullxml);
				
				return AcordPDFXML.OuterXml;
			}
			catch(Exception ex)
			{
				System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
				addInfo.Add("Err Descriptor ","Error while generating PDF.");
				addInfo.Add("CustomerID" ,gStrClientID);
				addInfo.Add("PolicyID",gStrPolicyId);
				addInfo.Add("PolicyVersionID",gStrPolicyVersion);
				ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex,addInfo );
				throw(new Exception("Error while generating dwelling PDF.",ex));
			}
		}

		#region code for mailling page xml Generation
		private void creatmaillingpage()
		{
			string strsendmessg="";
			string strInsname="";
			string strInsAdd="";
			string strcityzip="";
			XmlElement MaillingRootElementDecPage;
			MaillingRootElementDecPage    = AcordPDFXML.CreateElement("MAILLINGPAGE");

			DataSet DstempAppDocument = new DataSet();

			
			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DstempAppDocument = gobjWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
			gobjWrapper.ClearParameteres();
			//DstempAppDocument = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			strInsname=RemoveJunkXmlCharacters(DstempAppDocument.Tables[0].Rows[0]["APPNAME"].ToString())+ " " + RemoveJunkXmlCharacters(DstempAppDocument.Tables[0].Rows[0]["SUFFIX"].ToString());
			strInsAdd=RemoveJunkXmlCharacters(DstempAppDocument.Tables[0].Rows[0]["APPADDRESS"].ToString());
			strcityzip=RemoveJunkXmlCharacters(DstempAppDocument.Tables[0].Rows[0]["APPCITYSTZIP"].ToString());
			DataSet DstempDocument = new DataSet();
			
			if (gStrPdfFor == PDFForDecPage)
			{
				gobjWrapper.AddParameter("@DOCUMENT_CODE","DEC_PAGE");
				DstempDocument = gobjWrapper.ExecuteDataSet("Proc_GetPDFDocumentMessage");
				gobjWrapper.ClearParameteres();
				//DstempDocument = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFDocumentMessage " + "DEC_PAGE" + "");
				strsendmessg=DstempDocument.Tables[0].Rows[0]["SEND_MESSAGE"].ToString();
				
				if(strsendmessg	=="Y")
				{
					AcordPDFXML.SelectSingleNode(RootElement).AppendChild(MaillingRootElementDecPage);
					//DecPageRootElement.AppendChild(MaillingRootElementDecPage);
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

					DecMailElement.InnerXml = DecMailElement.InnerXml +  "<MAILLINGNAME " + fieldType + "=\"" + fieldTypeText + "\">" + strInsname + "</MAILLINGNAME>";
					DecMailElement.InnerXml = DecMailElement.InnerXml +  "<MAILLINGADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + strInsAdd + "</MAILLINGADDRESS>";
					DecMailElement.InnerXml = DecMailElement.InnerXml +  "<CITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + strcityzip + "</CITYSTATEZIP>";
					if(DstempDocument.Tables[0].Rows[0]["DOCUMENT_MESSAGE"].ToString()!="0")
						DecMailElement.InnerXml = DecMailElement.InnerXml +  "<MESSAGE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DstempDocument.Tables[0].Rows[0]["DOCUMENT_MESSAGE"].ToString()) + "</MESSAGE>";
				}
			}
			else if (gStrPdfFor == PDFForAcord)
			{
				gobjWrapper.AddParameter("@DOCUMENT_CODE","ACORD");
				DstempDocument = gobjWrapper.ExecuteDataSet("Proc_GetPDFDocumentMessage");
				gobjWrapper.ClearParameteres();

				//DstempDocument = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFDocumentMessage " + "ACORD" + "");
				strsendmessg=DstempDocument.Tables[0].Rows[0]["SEND_MESSAGE"].ToString();
				
				if(strsendmessg	=="Y")
				{
					AcordPDFXML.SelectSingleNode(RootElement).AppendChild(MaillingRootElementDecPage);
					//Acord84RootElement.AppendChild(MaillingRootElementDecPage);
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

					DecMailElement.InnerXml= DecMailElement.InnerXml +  "<MAILLINGNAME " + fieldType +"=\""+ fieldTypeText +"\">"+strInsname+"</MAILLINGNAME>"; 
					DecMailElement.InnerXml= DecMailElement.InnerXml +  "<MAILLINGADDRESS " + fieldType +"=\""+ fieldTypeText +"\">"+strInsAdd+"</MAILLINGADDRESS>"; 
					DecMailElement.InnerXml= DecMailElement.InnerXml +  "<CITYSTATEZIP " + fieldType +"=\""+ fieldTypeText +"\">"+strcityzip+"</CITYSTATEZIP>"; 
					DecMailElement.InnerXml= DecMailElement.InnerXml +  "<MESSAGE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DstempDocument.Tables[0].Rows[0]["DOCUMENT_MESSAGE"].ToString())+"</MESSAGE>"; 
				}
			}

		}
		#endregion
		

		#region insret customer full wording xml
		private void InsertCustomerFullWordingXml(string strCustomerId,string strAppId, string strAppVersionId, string StrCopyTo, string strcutomerxml)
		{
			try
			{
				//DataWrapper objDataWrapper;
				//objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);	
					
				gobjWrapper.AddParameter("@CUSTOMER_ID",strCustomerId);
				gobjWrapper.AddParameter("@POLICY_ID",strAppId);
				gobjWrapper.AddParameter("@POLICY_VERSION_ID",strAppVersionId);
				gobjWrapper.AddParameter("@CALLED_FOR",StrCopyTo);
				gobjWrapper.AddParameter("@CUSTOMER_XML",strcutomerxml);
                gobjWrapper.ExecuteNonQuery("PROC_GETFULLCUSTOMERXML_INFO");
				gobjWrapper.ClearParameteres();
			}
			
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{}

		}
		#endregion
		#region To Create Root Element For All Root PDFs
		private void createRootElementForAllRootPDFs()
		{
			if (gStrPdfFor == PDFForDecPage)
			{
				DecPageRootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(DecPageRootElement);
				DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGE"));
			}
			else if (gStrPdfFor == PDFForAcord)
			{

				Acord84RootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(Acord84RootElement);
				Acord84RootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD84"));

				SupplementalRootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(SupplementalRootElement);
				SupplementalRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENT"));
			}
		}
		#endregion

		#region Creating Policy, Agency And Attachments Xml 
		private void CreatePolicyAgencyXML()
		{
			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFPolicyDetails");
			gobjWrapper.ClearParameteres();
			
			//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFPolicyDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			
			#region Global Variable Assignment
			
			PolicyNumber = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString());
			PolicyEffDate = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString());
			PolicyExpDate = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString());
			//Added for policy type
			PolicyType = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_TYPE"].ToString());
			//
			if(gStrProcessID != null && gStrProcessID != "")
			{
				DataSet DSAddWordSet = new DataSet();
				gobjWrapper.AddParameter("@STATE_ID","0");
				gobjWrapper.AddParameter("@LOB_ID","0");
				gobjWrapper.AddParameter("@PROCESS_ID",gStrProcessID);
				DSAddWordSet = gobjWrapper.ExecuteDataSet("Proc_GetAddWordingsData");
				gobjWrapper.ClearParameteres();

				//DSAddWordSet = gobjSqlHelper.ExecuteDataSet("Proc_GetAddWordingsData " +  "0," +  "0," + gStrProcessID);
				
				if (DSAddWordSet.Tables.Count > 1 && DSAddWordSet.Tables[1].Rows.Count > 0)
					Reason		=	RemoveJunkXmlCharacters(DSAddWordSet.Tables[1].Rows[0]["PROCESS_DESC"].ToString());
			}
			else
				Reason		=	RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["REASON"].ToString());
			//	CopyTo			= RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["COPY_TO"].ToString());
			//Reason = Reason.Replace("Reinstate", "Reinstatement");

			if(Reason.Trim() != "" && DSTempDataSet.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString() != "")
				Reason += " / Effective Date: " + DSTempDataSet.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString();

			AgencyName = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString());
			AgencyAddress = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD"].ToString()).Trim() ;
			//			if(!AgencyAddress.Equals(""))
			//			{			
			//				if(AgencyAddress.LastIndexOf(",")!=-1)
			//					AgencyAddress=AgencyAddress.Substring(0,(AgencyAddress.Length));  
			//			}        
			AgencyCitySTZip = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_CITYSTZIP"].ToString()).Trim() ;
			if(!AgencyCitySTZip.Equals(""))
			{
				if(AgencyCitySTZip.EndsWith(","))
					AgencyCitySTZip=AgencyCitySTZip.Substring(0,(AgencyCitySTZip.Length-1));  
			}
			AgencyPhoneNumber = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString());
			AgencyCode = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["NUM_AGENCY_CODE"].ToString());
			AgencySubCode = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["SUB_CODE"].ToString()).Trim();

			if(!AgencySubCode.Equals(""))
			{
				if(AgencySubCode.Trim().EndsWith("/"))
				{
					AgencySubCode=AgencySubCode.Substring(0,AgencySubCode.LastIndexOf("/"));

				}
			}
			AgencyBilling = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PAYMENTBILLING"].ToString());//=="AB"?"Agency Bill":"Direct Bill" ;
			if(AgencyBilling =="AB")
			{
				AgencyBilling="Agency Bill";
			}
			else if(AgencyBilling == "MB")
			{
				AgencyBilling="Mortgage Bill";
			}
			else
			{
				AgencyBilling="Direct Bill";
			}
			
			#endregion

			if (gStrPdfFor==PDFForAcord)
			{
				#region Acord84 Page
				XmlElement AcordPolicyElement;
				AcordPolicyElement = AcordPDFXML.CreateElement("POLICY");
				Acord84RootElement.AppendChild(AcordPolicyElement);
				AcordPolicyElement.SetAttribute(fieldType,fieldTypeSingle);
			
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(System.DateTime.Today.ToString("MM/dd/yyyy") ) + "</DATE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</POLICYNUMBER>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<EFFECTIVEDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</EFFECTIVEDATE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<EXPIRATIONDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</EXPIRATIONDATE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<BINDEREFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["BINDEREFFDATE"].ToString()) + "</BINDEREFFDATE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<BINDEREXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["BINDEREXPDATE"].ToString()) + "</BINDEREXPDATE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<BINDERCVGNOTBOUND " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["BINDERCVGNOTBOUND"].ToString()) + "</BINDERCVGNOTBOUND>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<PAYMENTBILLING " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PAYMENTBILLING"].ToString()) + "</PAYMENTBILLING>";

				if(DSTempDataSet.Tables[0].Rows[0]["PAYMENTBILLING"].ToString() == "DB" || DSTempDataSet.Tables[0].Rows[0]["PAYMENTBILLING"].ToString() == "MB")
				{
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<PAYMENTDIRECTBILL " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PAYMENTDIRECTBILL"].ToString()) + "</PAYMENTDIRECTBILL>";
					/* Commented by Asfa (30-Jan-2008) -iTrack issue #3495 Part 3
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<PAYMENTAPPBILL " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PAYMENTAPPBILL"].ToString()) + "</PAYMENTAPPBILL>";
					*/
					string strbillPlan=DSTempDataSet.Tables[0].Rows[0]["PAYMENTAPPBILL"].ToString();
					if(DSTempDataSet.Tables[0].Rows[0]["PLAN_CODE"].ToString() == "FULLPAY")
					{
						strbillPlan="FP";
						AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<PAYMENTAPPBILL " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters("FP") + "</PAYMENTAPPBILL>";
					}
					else if(DSTempDataSet.Tables[0].Rows[0]["PAYMENTAPPBILL"].ToString() != "FP" && DSTempDataSet.Tables[0].Rows[0]["PLAN_CODE"].ToString() != "FULLPAY")
					{
							AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<PAYMENTDIRECTOTHER " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PLAN_DESCRIPTION"].ToString()) + "</PAYMENTDIRECTOTHER>";
					}
				}
				// Added by Asfa (30-Jan-2008) -iTrack issue #3495 Part 3
				if(DSTempDataSet.Tables[0].Rows[0]["PLAN_CODE"].ToString() != "FULLPAY")
				{
					//					
					//					if(DSTempDataSet.Tables[0].Rows[0]["PAYMENTAPPBILL"].ToString() == "FP")
					
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<PAYMENTAPPBILL " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PAYMENTAPPBILL"].ToString()) + "</PAYMENTAPPBILL>";
				}
				
				//				string strPlanDesc="";
				//				if(DSTempDataSet.Tables[0].Rows[0]["PAYMENTAPPBILL"]!=null && DSTempDataSet.Tables[0].Rows[0]["PAYMENTAPPBILL"].ToString()!="")
				//				{
				//					if(DSTempDataSet.Tables[0].Rows[0]["PAYMENTAPPBILL"].ToString().Equals("OP"))
				//						if(DSTempDataSet.Tables[0].Rows[0]["PLAN_DESCRIPTION"]!=null && DSTempDataSet.Tables[0].Rows[0]["PLAN_DESCRIPTION"].ToString()!="")
				//							strPlanDesc=DSTempDataSet.Tables[0].Rows[0]["PLAN_DESCRIPTION"].ToString();
				//				}
				//				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<PAYMENTAPPBILL " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PAYMENTAPPBILL"].ToString()) + "</PAYMENTAPPBILL>";
				//				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<PAYMENTDIRECTOTHER " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(strPlanDesc) + "</PAYMENTDIRECTOTHER>";
				
				if(DSTempDataSet.Tables[0].Rows[0]["YEAR_AT_CURR_RESI"]!=null && DSTempDataSet.Tables[0].Rows[0]["YEAR_AT_CURR_RESI"].ToString()!="" && DSTempDataSet.Tables[0].Rows[0]["YEAR_AT_CURR_RESI"].ToString()!="0" )
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<DATECURRRESIDENCE " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["YEAR_AT_CURR_RESI"].ToString()) + "</DATECURRRESIDENCE>";
				if(DSTempDataSet.Tables[0].Rows[0]["POLICY_TYPE"].ToString()!="")
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<APPLICANTCOPLAN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Wolverine Mutual/" + DSTempDataSet.Tables[0].Rows[0]["POLICY_TYPE"].ToString()) + "</APPLICANTCOPLAN>";                
			

				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<APPLICANTPREVIOUSADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["YEARS_AT_PREV_ADD"].ToString()) + "</APPLICANTPREVIOUSADDRESS>";
				//Agency
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</AGENCYNAME>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AgencyAddress) + "</AGENCYADDRESS>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_CITYSTZIP"].ToString()) + "</AGENCYCITYSTATEZIP>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString()) + "</AGENCYPHONE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYFAX " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_FAX"].ToString()) + "</AGENCYFAX>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AgencyCode) + "</AGENCYCODE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AgencySubCode) + "</AGENCYSUBCODE>";
				//ATTACHMENT INFO
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<ATTCHREPLCOSTEST " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PROPRTY_INSP_CREDIT"].ToString()) + "</ATTCHREPLCOSTEST>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<ATTCHPHOTOGRAPH " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PIC_OF_LOC"].ToString()) + "</ATTCHPHOTOGRAPH>";
				#endregion
			}
				//added to hide/show notice to policy holder
			else if(gStrPdfFor == PDFForDecPage)
			{
				string VarStateCode = SetStateCode(gStrCalledFrom,System.Convert.ToInt32(gStrPolicyId),System.Convert.ToInt32(gStrPolicyVersion),System.Convert.ToInt32(gStrClientID),gobjWrapper);
				
				if((VarStateCode == "IN") && (!(Reason.StartsWith("New Business")) || (Reason.StartsWith("Rewrite"))))
				{
					XmlElement DecPagePolicyElement;
					DecPagePolicyElement = AcordPDFXML.CreateElement("WORDINGS");
					DecPageRootElement.AppendChild(DecPagePolicyElement);
					DecPagePolicyElement.SetAttribute(fieldType,fieldTypeSingle);
			
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<not_pol " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_pol>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<not_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_txt>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<not_txt1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_txt1>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<not_txt2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_txt2>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<ins_add " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</ins_add>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<hotline " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</hotline>";
					DecPagePolicyElement.InnerXml = DecPagePolicyElement.InnerXml +  "<website " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</website>";
				}
					
				XmlElement DecPageFcra_Adverse_Section;
				DecPageFcra_Adverse_Section = AcordPDFXML.CreateElement("FCRA_ADVERSE_SECTION");
				DecPageRootElement.AppendChild(DecPageFcra_Adverse_Section);
				DecPageRootElement.SetAttribute(fieldType,fieldTypeSingle);
				DecPageRootElement.InnerXml = DecPageRootElement.InnerXml +  "<FCRA_ADVERSE_SECTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</FCRA_ADVERSE_SECTION>";
			}
			//
		}
		#endregion

		#region Creating Named Insured And CoApplicant Info

		private string NamedInsured(DataSet dsIns)
		{
			string names = "";
			if(dsIns.Tables.Count > 0)
			{
				if(DSTempDataSet.Tables[0].Rows.Count > 1)
					names = DSTempDataSet.Tables[0].Rows[0]["APPNAME"].ToString() +DSTempDataSet.Tables[0].Rows[0]["SUFFIX"].ToString() + " & " + DSTempDataSet.Tables[0].Rows[1]["APPNAME"].ToString()+ DSTempDataSet.Tables[0].Rows[1]["SUFFIX"].ToString();
				else
					names = DSTempDataSet.Tables[0].Rows[0]["APPNAME"].ToString()+DSTempDataSet.Tables[0].Rows[0]["SUFFIX"].ToString();
			}
			return names;

		}

		private void CreateNamedInsuredCoAppXml()
		{
			string appAdd1,appAdd2,appCity,appState,appZip;
						
			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
			gobjWrapper.ClearParameteres();

			//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			if (DSTempDataSet.Tables[0].Rows.Count > 0 )
			{
				#region Global Variable Assignment
				ApplicantName1 = RemoveJunkXmlCharacters(NamedInsured(DSTempDataSet));
				ApplicantName = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPNAME"].ToString());
				ApplicantAddress = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTADDRESS"].ToString());
				ApplicantCityStZip = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTCITYSTZIP"].ToString()).Trim() ;
				if(!ApplicantCityStZip.Equals(""))
				{
					if(ApplicantCityStZip.EndsWith(","))
						ApplicantCityStZip=ApplicantCityStZip.Substring(0,(ApplicantCityStZip.Length-1));  


				}

				appAdd1=RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ADDRESS1"].ToString().Trim() );
				appAdd2=RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ADDRESS2"].ToString().Trim());
				appCity=RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CITY"].ToString().Trim());
				appState=RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["STATE_CODE"].ToString().Trim());
				appZip=RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ZIP_CODE"].ToString().Trim());

				
				appAddress=RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPADDRESS"].ToString());
				appCityState= RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPCITYSTZIP"].ToString());

				reason_code1 = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_REASON_CODE"].ToString());
				reason_code2 = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_REASON_CODE2"].ToString());
				reason_code3 = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_REASON_CODE3"].ToString());
				reason_code4 = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_REASON_CODE4"].ToString());
				if(currTerm <= 1)
				{
					if(IsInsScorePage2(currTerm, stCode, PolicyEffDate, DSTempDataSet.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString()))
						strNeedPage2 = "Y";
					else
						strNeedPage2 = "N";
				}
				else
				{
					if(IsInsScorePage2(currTerm, stCode, PolicyEffDate, applyInsScore.ToString()))
						strNeedPage2 = "Y";
					else
						strNeedPage2 = "N";
				}

				#endregion

				if (gStrPdfFor==PDFForAcord)
				{
					#region Acord 84 Page
					string strAdd="";
					string strCoappSSn="";
					XmlElement Acord84NamedInsuredElement;
					Acord84NamedInsuredElement = AcordPDFXML.CreateElement("NAMEDINSURED");
					Acord84RootElement.AppendChild(Acord84NamedInsuredElement);
					Acord84NamedInsuredElement.SetAttribute(fieldType,fieldTypeSingle);

					string ApplName = "";
					if(DSTempDataSet.Tables[0].Rows.Count > 1)
						ApplName = DSTempDataSet.Tables[0].Rows[0]["APPNAME"].ToString() +DSTempDataSet.Tables[0].Rows[0]["SUFFIX"].ToString()  + " & " + DSTempDataSet.Tables[0].Rows[1]["APPNAME"].ToString();
					else
						ApplName = DSTempDataSet.Tables[0].Rows[0]["APPNAME"].ToString() +DSTempDataSet.Tables[0].Rows[0]["SUFFIX"].ToString();

					Acord84NamedInsuredElement.InnerXml = Acord84NamedInsuredElement.InnerXml +  "<APPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ApplName) + "</APPLICANTNAME>";
					Acord84NamedInsuredElement.InnerXml = Acord84NamedInsuredElement.InnerXml +  "<APPLICANTADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + appAddress + "</APPLICANTADDRESS>";
					Acord84NamedInsuredElement.InnerXml = Acord84NamedInsuredElement.InnerXml +  "<APPLICANTCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + appCityState + "</APPLICANTCITYSTATEZIP>";
					Acord84NamedInsuredElement.InnerXml = Acord84NamedInsuredElement.InnerXml +  "<APPLICANTHOMEPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PHONE"].ToString()) + "</APPLICANTHOMEPHONE>";
					Acord84NamedInsuredElement.InnerXml = Acord84NamedInsuredElement.InnerXml +  "<APPLICANTBUSSPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_BUSINESS_PHONE"].ToString()) + "</APPLICANTBUSSPHONE>";

					//					if(locAdd1!=appAdd1 || locAdd2!=appAdd2 || locCity!=appCity || locState!=appState || locZip!=appZip )
					//					{
					//						if((locAdd1!=null && locAdd1!="") || (locAdd2!=null && locAdd2!=""))
					//							Acord84NamedInsuredElement.InnerXml += "<PROPERTYLOCATIONADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + (locAdd1 + ", " + locAdd2) + ", </PROPERTYLOCATIONADDRESS>";
					//						if((locCity!=null && locCity!="") || (locState!=null && locState!="") || (locZip!=null && locZip!=""))
					//							Acord84NamedInsuredElement.InnerXml += "<PROPERTYLOCATIONCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + (locCity + ", " + locState + ", " + locZip) + "</PROPERTYLOCATIONCITYSTATEZIP>";						
					//						
					//					}
					//Added by Mohit / Manoj Rathore 5-May-2007


					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DataSet DSDwellDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_Dwelling_Details");
					gobjWrapper.ClearParameteres();

					//DataSet DSDwellDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_Dwelling_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
					foreach(DataRow Dwellrow in DSDwellDataSet.Tables[0].Rows)
					{
						if(ApplicantAddress.Trim() != Dwellrow["LOC_ADDRESS"].ToString().Trim() || ApplicantCityStZip.Trim() != Dwellrow["LOC_CITYSTATEZIP"].ToString().Trim())
						{
							Acord84NamedInsuredElement.InnerXml = Acord84NamedInsuredElement.InnerXml +  "<PROPERTYLOCATIONADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Dwellrow["LOC_ADDRESS"].ToString()) + "</PROPERTYLOCATIONADDRESS>";
							Acord84NamedInsuredElement.InnerXml = Acord84NamedInsuredElement.InnerXml +  "<PROPERTYLOCATIONCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Dwellrow["LOC_CITYSTATEZIP"].ToString()) + "</PROPERTYLOCATIONCITYSTATEZIP>";
							break;
						}
					}
					if(DSTempDataSet.Tables[0].Rows[0]["SSN"].ToString() !="" && DSTempDataSet.Tables[0].Rows[0]["SSN"].ToString() !="0")
					{
						strCoappSSn = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(DSTempDataSet.Tables[0].Rows[0]["SSN"].ToString());
						
						if(strCoappSSn.Trim()!="")//If-Else condition added by Charles on 21/7/2009 for Itrack 6129
						{
							string strvaln = "xxx-xx-";
							strvaln += strCoappSSn.Substring(strvaln.Length, strCoappSSn.Length - strvaln.Length);
							strCoappSSn = strvaln;
						}
						else
							strCoappSSn="";

					}
					Acord84NamedInsuredElement.InnerXml +=   "<PCOAPPOCCUPATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["OCCUPATION"].ToString()) + "</PCOAPPOCCUPATION>";					
					Acord84NamedInsuredElement.InnerXml +=  "<PCOAPPYEAREMPL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["YEARSEMPL"].ToString()) + "</PCOAPPYEAREMPL>";					
					Acord84NamedInsuredElement.InnerXml +=   "<PCOAPPDATEOFBIRTH " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["DOB"].ToString()) + "</PCOAPPDATEOFBIRTH>";
					Acord84NamedInsuredElement.InnerXml +=   "<PCOAPPSSN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strCoappSSn) + "</PCOAPPSSN>";
					Acord84NamedInsuredElement.InnerXml +=   "<PCOAPPEMPLOYERNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CO_APPLI_EMPL_NAME"].ToString()) + "</PCOAPPEMPLOYERNAME>";

					if(DSTempDataSet.Tables[0].Rows[0]["CO_APPLI_EMPL_ADD"]!=null && DSTempDataSet.Tables[0].Rows[0]["CO_APPLI_EMPL_ADD"].ToString()!="")
					{
						strAdd=DSTempDataSet.Tables[0].Rows[0]["CO_APPLI_EMPL_ADD"].ToString().Trim() ;
						if(strAdd.EndsWith(",")) 
							strAdd=strAdd.Substring(0,strAdd.LastIndexOf(","));  

					}

					string marStatCd = DSTempDataSet.Tables[0].Rows[0]["MARTSTATUSCODE"].ToString();
					if(marStatCd == "P")
						marStatCd = "S";
					Acord84NamedInsuredElement.InnerXml +=   "<PCOAPPEMPLOYERADD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strAdd) + "</PCOAPPEMPLOYERADD>";
					Acord84NamedInsuredElement.InnerXml +=   "<PCOAPPYEAROCCU " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["YEARSOCCU"].ToString()) + "</PCOAPPYEAROCCU>";
					Acord84NamedInsuredElement.InnerXml +=   "<PCOAPPMARTSTATUSCD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(marStatCd) + "</PCOAPPMARTSTATUSCD>";

					XmlElement AddlCoApplicantElement;
					AddlCoApplicantElement = AcordPDFXML.CreateElement("ADDITIONALCOAPPLICANTS");
					Acord84RootElement.AppendChild(AddlCoApplicantElement);
					AddlCoApplicantElement.SetAttribute(fieldType,fieldTypeMultiple);
					AddlCoApplicantElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD84COAPP"));
					AddlCoApplicantElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD84COAPP"));
					AddlCoApplicantElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD84COAPPEXTN"));
					AddlCoApplicantElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD84COAPPEXTN"));

					for(int RowCounter=1;RowCounter<DSTempDataSet.Tables[0].Rows.Count;RowCounter++)
					{
						XmlElement CoAppElement;
						CoAppElement =  AcordPDFXML.CreateElement("COAPPLICANT");
						AddlCoApplicantElement.AppendChild(CoAppElement);
						CoAppElement.SetAttribute(fieldType,fieldTypeNormal);
						CoAppElement.SetAttribute(id,(RowCounter-1).ToString());
						strAdd="";
						if(DSTempDataSet.Tables[0].Rows[RowCounter]["SSN"].ToString() !="" && DSTempDataSet.Tables[0].Rows[RowCounter]["SSN"].ToString() !="0")
						{
							strCoappSSn = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(DSTempDataSet.Tables[0].Rows[RowCounter]["SSN"].ToString());
							
							if(strCoappSSn.Trim()!="")//If-Else condition added by Charles on 21/7/2009 for Itrack 6129
							{
								string strvaln = "xxx-xx-";
								strvaln += strCoappSSn.Substring(strvaln.Length, strCoappSSn.Length - strvaln.Length);
								strCoappSSn = strvaln;
							}
							else
								strCoappSSn="";
						}

						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<ADDLPOLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</ADDLPOLICYNUMBER>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<ADDLAPPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName1 + "</ADDLAPPLICANTNAME>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<NAMEINSUREDNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</NAMEINSUREDNAME>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<NAMEINSUREDADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + appAddress  + "</NAMEINSUREDADDRESS>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<NAMEINSUREDCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + appCityState  + "</NAMEINSUREDCITYSTATEZIP>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPEMPLOYEENAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[RowCounter]["APPNAME"].ToString()) + "</COAPPEMPLOYEENAME>";
						if(RowCounter == 1)
						{
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPOCCUPATION1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[RowCounter]["OCCUPATION"].ToString()) + "</COAPPOCCUPATION1>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPYEAREMPL1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[RowCounter]["YEARSEMPL"].ToString()) + "</COAPPYEAREMPL1>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPDATEOFBIRTH1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[RowCounter]["DOB"].ToString()) + "</COAPPDATEOFBIRTH1>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPSSN1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strCoappSSn) + "</COAPPSSN1>";
						}
						else
						{
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPOCCUPATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[RowCounter]["OCCUPATION"].ToString()) + "</COAPPOCCUPATION>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPYEAREMPL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[RowCounter]["YEARSEMPL"].ToString()) + "</COAPPYEAREMPL>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPDATEOFBIRTH " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[RowCounter]["DOB"].ToString()) + "</COAPPDATEOFBIRTH>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPSSN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strCoappSSn) + "</COAPPSSN>";
						}

						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPMARTSTATUS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[RowCounter]["MARTSTATUS"].ToString()) + "</COAPPMARTSTATUS>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPEMPLOYERNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[RowCounter]["CO_APPLI_EMPL_NAME"].ToString()) + "</COAPPEMPLOYERNAME>";
						if(DSTempDataSet.Tables[0].Rows[RowCounter]["CO_APPLI_EMPL_ADD"]!=null && DSTempDataSet.Tables[0].Rows[RowCounter]["CO_APPLI_EMPL_ADD"].ToString()!="")
						{
							strAdd=DSTempDataSet.Tables[0].Rows[RowCounter]["CO_APPLI_EMPL_ADD"].ToString().Trim() ;
							if(strAdd.EndsWith(",")) 
								strAdd=strAdd.Substring(0,strAdd.LastIndexOf(","));  

						}
						marStatCd = DSTempDataSet.Tables[0].Rows[RowCounter]["MARTSTATUSCODE"].ToString();
						if(marStatCd == "P")
							marStatCd = "S";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPEMPLOYERADD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strAdd) + "</COAPPEMPLOYERADD>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPYEAROCCU " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[RowCounter]["YEARSOCCU"].ToString()) + "</COAPPYEAROCCU>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPMARTSTATUSCD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(marStatCd) + "</COAPPMARTSTATUSCD>";
					}
					#endregion
				}
			}
		}
		#endregion

		#region Creating Prior Policy/Coverage XML
		private void CreatePriorPolicyCoverage()
		{

			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@LOBCODE","REDW");
			gobjWrapper.AddParameter("@DATAFOR","POLICY");
			DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFPriorPolicyAndLossDetails");
			gobjWrapper.ClearParameteres();

			//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFPriorPolicyAndLossDetails " + gStrClientID + ",'REDW','POLICY'");
			
			if (DSTempDataSet.Tables[0].Rows.Count>0)
			{
				if (gStrPdfFor==PDFForAcord)
				{
					#region Acord 84 Page
			
					XmlElement Acord84PriorPolicyElement;
					Acord84PriorPolicyElement = AcordPDFXML.CreateElement("PRIORPOLICYCOVERAGE");
					Acord84RootElement.AppendChild(Acord84PriorPolicyElement);
					Acord84PriorPolicyElement.SetAttribute(fieldType,fieldTypeSingle);

					Acord84PriorPolicyElement.InnerXml = Acord84PriorPolicyElement.InnerXml +  "<PRIORCARRIER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CARRIER"].ToString()) + "</PRIORCARRIER>";
					Acord84PriorPolicyElement.InnerXml = Acord84PriorPolicyElement.InnerXml +  "<PRIORPOLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["OLD_POLICY_NUMBER"].ToString()) + "</PRIORPOLICYNUMBER>";
					if(DSTempDataSet.Tables[0].Rows[0]["EFF_DATE"]!=null && DSTempDataSet.Tables[0].Rows[0]["EFF_DATE"].ToString()!="")
						if(!DSTempDataSet.Tables[0].Rows[0]["EFF_DATE"].ToString().Trim().Equals(DateTime.MinValue.ToString()) && !DSTempDataSet.Tables[0].Rows[0]["EFF_DATE"].ToString().Trim().Equals("01/01/1900"))
							Acord84PriorPolicyElement.InnerXml = Acord84PriorPolicyElement.InnerXml +  "<PRIOREXPIRATIONDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["EFF_DATE"].ToString()) + "</PRIOREXPIRATIONDATE>";
					#endregion
				}
			}
		}
		#endregion

		#region Creating Prior Loss XML
		private void CreatePriorLossXml()
		{

			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@LOBCODE","REDW");
			gobjWrapper.AddParameter("@DATAFOR","LOSS");
			DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFPriorPolicyAndLossDetails");
			gobjWrapper.ClearParameteres();

			//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFPriorPolicyAndLossDetails " + gStrClientID + ",'REDW','LOSS'");
			int RowCounter=0;
			if(DSTempDataSet.Tables[0].Rows.Count == 0)
			{
				if (gStrPdfFor==PDFForAcord)
				{
					XmlElement Acord84PriorLoss;
					Acord84PriorLoss = AcordPDFXML.CreateElement("PRIORLOSSCOVERAGE");
					Acord84RootElement.AppendChild(Acord84PriorLoss);
					Acord84PriorLoss.SetAttribute(fieldType,fieldTypeMultiple);
					Acord84PriorLoss.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD84LOSSHIST"));
					Acord84PriorLoss.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD84LOSSHIST"));
					Acord84PriorLoss.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD84LOSSHISTEXTN"));
					Acord84PriorLoss.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD84LOSSHISTEXTN"));

					XmlElement Acord84PriorLossElement;
					Acord84PriorLossElement =  AcordPDFXML.CreateElement("COAPPLICANT");
					Acord84PriorLoss.AppendChild(Acord84PriorLossElement);
					Acord84PriorLossElement.SetAttribute(fieldType,fieldTypeNormal);
					Acord84PriorLossElement.SetAttribute(id,RowCounter.ToString());

					Acord84PriorLossElement.InnerXml = Acord84PriorLossElement.InnerXml +  "<PRIORLOSSENTERED " + fieldType + "=\"" + fieldTypeText + "\">N</PRIORLOSSENTERED>";
				}
			}
			else if (DSTempDataSet.Tables[0].Rows.Count>0)
			{
				if (gStrPdfFor==PDFForAcord)
				{
					XmlElement Acord84PriorLoss;
					Acord84PriorLoss = AcordPDFXML.CreateElement("PRIORLOSSCOVERAGE");
					Acord84RootElement.AppendChild(Acord84PriorLoss);
					Acord84PriorLoss.SetAttribute(fieldType,fieldTypeMultiple);
					Acord84PriorLoss.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD84LOSSHIST"));
					Acord84PriorLoss.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD84LOSSHIST"));
					Acord84PriorLoss.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD84LOSSHISTEXTN"));
					Acord84PriorLoss.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD84LOSSHISTEXTN"));

					foreach (DataRow Row in DSTempDataSet.Tables[0].Rows)
					{
						XmlElement Acord84PriorLossElement;
						Acord84PriorLossElement =  AcordPDFXML.CreateElement("COAPPLICANT");
						Acord84PriorLoss.AppendChild(Acord84PriorLossElement);
						Acord84PriorLossElement.SetAttribute(fieldType,fieldTypeNormal);
						Acord84PriorLossElement.SetAttribute(id,RowCounter.ToString());

						Acord84PriorLossElement.InnerXml = Acord84PriorLossElement.InnerXml +  "<PRIORLOSSENTERED " + fieldType + "=\"" + fieldTypeText + "\">Y</PRIORLOSSENTERED>";
						Acord84PriorLossElement.InnerXml = Acord84PriorLossElement.InnerXml +  "<APPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName1 + "</APPLICANTNAME>";
						Acord84PriorLossElement.InnerXml = Acord84PriorLossElement.InnerXml +  "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNUMBER>";
						Acord84PriorLossElement.InnerXml = Acord84PriorLossElement.InnerXml +  "<LOSSHISTDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["OCCURENCE_DATE"].ToString()) + "</LOSSHISTDATE>";
						Acord84PriorLossElement.InnerXml = Acord84PriorLossElement.InnerXml +  "<LOSSHISTTYPE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["LOSS_TYPE"].ToString()) + "</LOSSHISTTYPE>";
						Acord84PriorLossElement.InnerXml = Acord84PriorLossElement.InnerXml +  "<LOSSHISTDESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["LOSS_DESC"].ToString()) + "</LOSSHISTDESCRIPTION>";
						string strLossAmt="";
						if(Row["AMOUNT"]!=null && Row["AMOUNT"].ToString()!="")
						{
							strLossAmt=Row["AMOUNT"].ToString();
							if(!strLossAmt.Equals("0"))
								Acord84PriorLossElement.InnerXml = Acord84PriorLossElement.InnerXml +  "<LOSSHISTAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">"+"$" + RemoveJunkXmlCharacters(strLossAmt) + "</LOSSHISTAMOUNT>";


						}

						
						RowCounter++;
					}
				}
			}
		}
		#endregion

		#region code for Rental Dwelling Xml
		private int GetLowestPrnIndex(ref int []prnOrder, int prnCount)
		{
			int retIndex = 0;
			for(int prnIndex = 0; prnIndex < prnCount; prnIndex++)
			{
				if(prnOrder[prnIndex] < prnOrder[retIndex])
					retIndex = prnIndex;
			}
			prnOrder[retIndex] = 999999;
			return retIndex;
		}
		private void createRentalDwellingXML()
		{
			int DwellingCtr = 0,AddInt=0;	
			string strCompDate="";
			int extraCovPage = 0, pagecounter=0;
			int other_struct_cov = 0;
			string ProPExpFee ="0";
			XmlElement DecPageDwellMultipleElement;
			DecPageDwellMultipleElement = AcordPDFXML.CreateElement("REDW");

			XmlElement Acord84DwellMultipleElement;
			Acord84DwellMultipleElement = AcordPDFXML.CreateElement("REDW");

			XmlElement SupplementDwellMultipleElement;
			SupplementDwellMultipleElement    = AcordPDFXML.CreateElement("REDW");

			XmlElement DecPageDwellMultipleElement0;
			DecPageDwellMultipleElement0 = AcordPDFXML.CreateElement("REDW0");

			XmlElement DecPageREDWElement0;
			DecPageREDWElement0	= AcordPDFXML.CreateElement("REDWINFO0");

					
			if (gStrPdfFor == PDFForAcord)
			{
				Acord84RootElement.AppendChild(Acord84DwellMultipleElement);
				SupplementalRootElement.AppendChild(SupplementDwellMultipleElement);
			}
			
			#region setting Dwelling Root Attribute
			if (gStrPdfFor==PDFForDecPage )
			{
				prnOrd_covCode = new string[100];
				prnOrd_attFile = new string[100];
				prnOrd = new int[100];
				#region Dwelling Root Element for DecPage		
				DecPageRootElement.AppendChild(DecPageDwellMultipleElement0);
				DecPageDwellMultipleElement0.SetAttribute(fieldType,fieldTypeMultiple);
				DecPageDwellMultipleElement0.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEREDW1"));
				DecPageDwellMultipleElement0.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEREDW1"));
				DecPageDwellMultipleElement0.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEREDWEXTN"));
				DecPageDwellMultipleElement0.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEREDWEXTN"));
				#endregion

				// Added by Mohit Agarwal for Customer Details irrespective of added boats
				// 14-Mar-2007
				#region setting Customer Agency Details
				DecPageDwellMultipleElement0.AppendChild(DecPageREDWElement0);
				DecPageREDWElement0.SetAttribute(fieldType,fieldTypeNormal);
				DecPageREDWElement0.SetAttribute(id,DwellingCtr.ToString());
				if(gStrCalledFrom.Equals(CalledFromPolicy))
				{
					DecPageREDWElement0.InnerXml = DecPageREDWElement0.InnerXml +  "<POLICYNO " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNO>";
					DecPageREDWElement0.InnerXml = DecPageREDWElement0.InnerXml +  "<POLICYPERIODFROM " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</POLICYPERIODFROM>";
					DecPageREDWElement0.InnerXml = DecPageREDWElement0.InnerXml +  "<POLICYPERIODTO " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</POLICYPERIODTO>";
					DecPageREDWElement0.InnerXml = DecPageREDWElement0.InnerXml +  "<REASON " + fieldType + "=\"" + fieldTypeText + "\">" + Reason  + "</REASON>";
					//Added by uday on 13-Feb-2008 -iTrack # 3619
					DecPageREDWElement0.InnerXml = DecPageREDWElement0.InnerXml +  "<ALL_POLICY_TYPE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyType + "</ALL_POLICY_TYPE>";
					DecPageREDWElement0.InnerXml += "<copyTo " + fieldType + "=\"" + fieldTypeText + "\">" + CopyTo + "</copyTo>";
				}

				DecPageREDWElement0.InnerXml = DecPageREDWElement0.InnerXml +  "<PRIMARYCONTACTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName1 + "</PRIMARYCONTACTNAME>";
				DecPageREDWElement0.InnerXml = DecPageREDWElement0.InnerXml +  "<PRIMARYCONTACTADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + appAddress + "</PRIMARYCONTACTADDRESS>";
				DecPageREDWElement0.InnerXml = DecPageREDWElement0.InnerXml +  "<PRIMARYCONTACTCITY " + fieldType + "=\"" + fieldTypeText + "\">" + appCityState + "</PRIMARYCONTACTCITY>";
				DecPageREDWElement0.InnerXml = DecPageREDWElement0.InnerXml +  "<AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyName + "</AGENCYNAME>";
				DecPageREDWElement0.InnerXml = DecPageREDWElement0.InnerXml +  "<AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyAddress + "</AGENCYADDRESS>";
				DecPageREDWElement0.InnerXml = DecPageREDWElement0.InnerXml +  "<AGENCYCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCitySTZip + "</AGENCYCITYSTATEZIP>";
				DecPageREDWElement0.InnerXml = DecPageREDWElement0.InnerXml +  "<AGENCYPHONENO " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyPhoneNumber.Replace(")",") ") + "</AGENCYPHONENO>";
				DecPageREDWElement0.InnerXml = DecPageREDWElement0.InnerXml +  "<AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCode + "</AGENCYCODE>";
				DecPageREDWElement0.InnerXml = DecPageREDWElement0.InnerXml +  "<AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencySubCode + "</AGENCYSUBCODE>";
				DecPageREDWElement0.InnerXml = DecPageREDWElement0.InnerXml +  "<AGENCYBILLING " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyBilling + "</AGENCYBILLING>";

				if(gStrCopyTo != "AGENCY")
				{
					if(gStrCalledFrom =="POLICY")
					{
						string strProccessId="";
						strProccessId=GetPolicyProcess(gStrClientID ,gStrPolicyId,gStrPolicyVersion,gStrCalledFrom,gobjWrapper);
						if(strProccessId ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_NEW_BUSINESS_PROCESS.ToString() || strProccessId ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_NEW_BUSINESS_PROCESS.ToString()
							|| strProccessId ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REWRITE_PROCESS.ToString() || strProccessId ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REWRITE_PROCESS.ToString())
						{
							createPage2XML(ref DecPageREDWElement0);
						}
//						else
//						{					
//							createPage2PolicyPrivacyXML(ref DecPageREDWElement0);
//						}
					}
					else
					{
						createPage2XML(ref DecPageREDWElement0);
					}
				}
				//Reason Code
				//				DecPageREDWElement0.InnerXml +="<reason_code1 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code1 + "</reason_code1>";
				//				DecPageREDWElement0.InnerXml +="<reason_code2 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code2 + "</reason_code2>";
				//				DecPageREDWElement0.InnerXml +="<reason_code3 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code3 + "</reason_code3>";
				//				DecPageREDWElement0.InnerXml +="<reason_code4 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code4 + "</reason_code4>";
				#endregion

				DecPageREDWElement0.AppendChild(DecPageDwellMultipleElement);
				DecPageDwellMultipleElement.SetAttribute(fieldType,fieldTypeMultiple);
				DecPageDwellMultipleElement.SetAttribute(PrimPDF,"");
				DecPageDwellMultipleElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEREDW1"));
				DecPageDwellMultipleElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEREDWEXTN"));
				DecPageDwellMultipleElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEREDWEXTN"));

			}
			else 	if (gStrPdfFor==PDFForAcord )
			{

				#region Dwelling Root Element for Accord 80
				Acord84DwellMultipleElement.SetAttribute(fieldType,fieldTypeMultiple);
				Acord84DwellMultipleElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD84REDW"));
				Acord84DwellMultipleElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD84REDW"));
				Acord84DwellMultipleElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD84REDWEXTN"));
				Acord84DwellMultipleElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD84REDWEXTN"));
			
				#endregion

				#region Dwelling Root Element for Accord 84 Supplement
				SupplementDwellMultipleElement.SetAttribute(fieldType,fieldTypeMultiple);
				SupplementDwellMultipleElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTREDW"));
				SupplementDwellMultipleElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTREDW"));
				SupplementDwellMultipleElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTREDWEXTN"));
				SupplementDwellMultipleElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTREDWEXTN"));
			
				#endregion			
			}
			#endregion

			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_Dwelling_Details");
			gobjWrapper.ClearParameteres();

			//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_Dwelling_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
			double Recived_prem=0;
			string strRecived_prem ="";
            //Changed By Chetna
            //if(DSTempDataSet.Tables[0].Rows.ToString()!=null)
            if (DSTempDataSet.Tables[0].Rows.Count != 0)
			{
                strRecived_prem = DSTempDataSet.Tables[0].Rows[0]["RECEIVED_PRMIUM"].ToString();				
				Recived_prem = System.Convert.ToDouble(DSTempDataSet.Tables[0].Rows[0]["RECEIVED_PRMIUM"].ToString());
				
			}	
			 
			if(strRecived_prem.IndexOf(".")>0)
			{
			//	strRecived_prem = strRecived_prem;
			}
			else
			{
				strRecived_prem = strRecived_prem + ".00";
			}
			string MinimumDue="0";
			//			if(DSTempDataSet.Tables[2].Rows.ToString()!=null)
			//			{
			//				MinimumDue = DSTempDataSet.Tables[2].Rows[0]["TOTAL_DUE"].ToString();
			//			}
			//strRecived_prem = strRecived_prem.Replace(",","");
			totdwellpage = DSTempDataSet.Tables[0].Rows.Count;
			foreach(DataRow DwellingDetail in DSTempDataSet.Tables[0].Rows)
			{
				other_struct_cov = 0;

				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@DWELLINGID",DwellingDetail["DWELLING_ID"]);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DataSet DSTempOutbuildings_1 = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_otherstructures");
				gobjWrapper.ClearParameteres();


				//DataSet DSTempOutbuildings_1 = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_otherstructures " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + DwellingDetail["DWELLING_ID"] +  ",'" + gStrCalledFrom + "'");
				foreach(DataRow DROutbuil in DSTempOutbuildings_1.Tables[0].Rows)
				{
					if(DROutbuil["PREMISES_DESCRIPTION"].ToString() !="")
					{
						other_struct_cov++;
					}
				}

				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@DWELLINGID",DwellingDetail["DWELLING_ID"]);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DataSet DSTempDwelling = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeOwner_Coverage_Details");
				gobjWrapper.ClearParameteres();

				//DataSet DSTempDwelling = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeOwner_Coverage_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + DwellingDetail["DWELLING_ID"] +  ",'" + gStrCalledFrom + "'");
				//if(DSTempDwelling.Tables[0].Rows.Count + other_struct_cov- 7 >= 12)
				if(DSTempDwelling.Tables[0].Rows.Count + other_struct_cov> 18)
                    totcovPage++;
			}				
			foreach(DataRow DwellingDetail in DSTempDataSet.Tables[0].Rows)
			{
				#region Rental Dwelling Details 
				XmlElement DecPageREDWElement;
				DecPageREDWElement	= AcordPDFXML.CreateElement("REDWINFO");

				XmlElement Acord84REDWElement;
				Acord84REDWElement	= AcordPDFXML.CreateElement("HOMEINFO");

				XmlElement SupplementREDWElement;
				SupplementREDWElement	= AcordPDFXML.CreateElement("HOMEINFO");
				
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@DWELLINGID",DwellingDetail["DWELLING_ID"]);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DataSet DSTempDwelling = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeOwner_Coverage_Details");
				gobjWrapper.ClearParameteres();

				//DataSet DSTempDwelling = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeOwner_Coverage_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + DwellingDetail["DWELLING_ID"] +  ",'" + gStrCalledFrom + "'");
			
				htpremium.Clear(); 
				foreach (XmlNode PremiumNode in GetPremium(DwellingDetail["DWELLING_ID"].ToString()))
				{
					if(!htpremium.Contains(getAttributeValue(PremiumNode,"COMPONENT_CODE")))
						htpremium.Add(getAttributeValue(PremiumNode,"COMPONENT_CODE"),getAttributeValue(PremiumNode,"STEPPREMIUM"));
				}
				// nbisht
				string sumTtl="0";
				string strTerritory="";
				double sumTtlPol = 0;
				if(gStrtemp != "final")
				{
					foreach (XmlNode SumTotalNode in GetSumTotalPremium(DwellingDetail["DWELLING_ID"].ToString()))
					{
						sumTtl= getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString() ;
						//					strTerritory=getAttributeValue(SumTotalNode,"PRODUCTDESC").ToString() ;

						//					if(strTerritory.Length>0 )
						//					{
						//						int colonPos=strTerritory.IndexOf(":");
						//						int lastBracketPos=strTerritory.IndexOf(")");
						//						if(lastBracketPos!=-1 && colonPos!=-1)
						//							strTerritory=strTerritory.Substring((colonPos+1),(lastBracketPos-colonPos)); 
						//						
						//					}
					}
				
					foreach (XmlNode SumTotalNode in GetSumTotalPremium())
					{
						if(getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString() != "")
							sumTtlPol+= double.Parse(getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString());
					}
				}
				else
				{
					string strPrimium = GetPremiumAll(DSTempDwelling, "SUMTOTAL", DwellingDetail["DWELLING_ID"].ToString());
					if(strPrimium !="")
					{
						double sumtotal = double.Parse(GetPremiumAll(DSTempDwelling, "SUMTOTAL", DwellingDetail["DWELLING_ID"].ToString()));
						sumTtl = Convert.ToString(sumtotal); 
						sumTtlPol+= Convert.ToDouble(sumTtl);
					}
				}
				
				foreach (XmlNode TerritoryNode in GetRentalTerritory())
				{
					strTerritory=getAttributeValue(TerritoryNode,"PRODUCTDESC").ToString() ;
					if(strTerritory.Length>0 )
					{
						int colonPos=strTerritory.IndexOf(":");
						int lastBracketPos=strTerritory.IndexOf(")");
						if(lastBracketPos!=-1 && colonPos!=-1)
							strTerritory=strTerritory.Substring((colonPos+1),(lastBracketPos-colonPos)); 

						strTerritory = strTerritory.Substring(1,strTerritory.Length-2);
						break;
					}
				}
				// Property Expense Fee
				if(gStrtemp != "final")
				{
					foreach (XmlNode PropExpenseFee in GetPropertyExpenseFee(DwellingDetail["DWELLING_ID"].ToString()))
					{
						if(getAttributeValue(PropExpenseFee,"STEPPREMIUM").ToString() != "")
							ProPExpFee= getAttributeValue(PropExpenseFee,"STEPPREMIUM").ToString();
					}
				}
				else
				{
					foreach(DataRow PremiumDetails in DSTempDwelling.Tables[1].Rows)
					{
						if(PremiumDetails["COMPONENT_CODE"].ToString() == "PRP_EXPNS_FEE")
						{
							if(PremiumDetails["COVERAGE_PREMIUM"].ToString() !=null && PremiumDetails["COVERAGE_PREMIUM"].ToString()!="")
							ProPExpFee= PremiumDetails["COVERAGE_PREMIUM"].ToString();
						}
					}
				}
				if (gStrPdfFor==PDFForDecPage )
				{
					#region Rental Dwelling Element for Dec Page					
					DecPageDwellMultipleElement.AppendChild(DecPageREDWElement);
					DecPageREDWElement.SetAttribute(fieldType,fieldTypeNormal);
					DecPageREDWElement.SetAttribute(id,DwellingCtr.ToString());

					string premAdd=DwellingDetail["LOC_ADDRESS"].ToString().Trim() ;
					if(!premAdd.Equals(""))
					{
						if(premAdd.EndsWith(","))
							premAdd=premAdd.Substring(0,(premAdd.Length-1));  
					}

					string premCity=DwellingDetail["LOC_CITYSTATEZIP"].ToString() ;
					if(!premCity.Equals(""))
					{
						if(premCity.EndsWith(","))
							premCity=premCity.Substring(0,(premCity.Length-1));  
					}


					if(gStrCalledFrom.Equals(CalledFromPolicy))
					{
						DecPageREDWElement.InnerXml = DecPageREDWElement.InnerXml +  "<POLICYNO " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNO>";
						DecPageREDWElement.InnerXml = DecPageREDWElement.InnerXml +  "<POLICYPERIODFROM " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</POLICYPERIODFROM>";
						DecPageREDWElement.InnerXml = DecPageREDWElement.InnerXml +  "<POLICYPERIODTO " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</POLICYPERIODTO>";
						//For iTrack # 3619
						DecPageREDWElement.InnerXml = DecPageREDWElement.InnerXml +  "<ALL_POLICY_TYPE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyType + "</ALL_POLICY_TYPE>";
						//
						DecPageREDWElement.InnerXml = DecPageREDWElement.InnerXml +  "<REASON " + fieldType + "=\"" + fieldTypeText + "\">" + Reason  + "</REASON>";
					}

					DecPageREDWElement.InnerXml = DecPageREDWElement.InnerXml +  "<PRIMARYCONTACTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName1 + "</PRIMARYCONTACTNAME>";
					if(DwellingCtr > 0)
						DecPageREDWElement.InnerXml = DecPageREDWElement.InnerXml +  "<PAGE " + fieldType + "=\"" + fieldTypeText + "\">" + (extraCovPage+pagecounter+1).ToString() + " of " + (totcovPage + totdwellpage).ToString() + "</PAGE>";


					DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml +  "<PREMISESDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(premAdd) + ", " + RemoveJunkXmlCharacters(premCity) +"</PREMISESDESCRIPTION>"; 
					DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml +  "<OCCUPANCY " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["OCCUPANCY"].ToString())+"</OCCUPANCY>"; 
					DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml +  "<CONSTRUCTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["CONSTRUCTION_TYPE"].ToString())+"</CONSTRUCTION>";
					DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml +  "<YEARCONSTRUCTION  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["YEAR_BUILT"].ToString())+"</YEARCONSTRUCTION>";
					string mark_val = "";
					mark_val = DwellingDetail["MARKET_VALUE"].ToString();
					if(mark_val.IndexOf(".00") >0)
						mark_val = "$" + mark_val.Replace(".00","");
					DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml +  "<MARKETVALUE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(mark_val)+"</MARKETVALUE>";
					DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml +  "<INFLATION  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["INFLATION_PRECENT"].ToString().Replace(", Inflation %: ",""))+"</INFLATION>";
					DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml +  "<COUNTY  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["COUNTY"].ToString()) +"</COUNTY>";
					if(DwellingDetail["HYDRANTDEC"]!=null && DwellingDetail["HYDRANTDEC"].ToString()!="")
						DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml +  "<FIREHYDRANT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["HYDRANTDEC"].ToString() + " FT" )+"</FIREHYDRANT>";
					
					if(DwellingDetail["FIRE_STATION_DIST"]!=null && DwellingDetail["FIRE_STATION_DIST"].ToString()!="")
						DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml +  "<FIRESTATION  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["FIRE_STATION_DIST"].ToString() + " Miles" )+"</FIRESTATION>";
					DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml +  "<PROTECTIONCLASS  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["PROT_CLASS"].ToString())+"</PROTECTIONCLASS>";
					DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml +  "<TERRITORY  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strTerritory)+"</TERRITORY>";
					DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml +  "<SIGNATUREDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(System.DateTime.Today.ToString("MM/dd/yyyy")  )+"</SIGNATUREDATE>";
					string strfloatX = "280";
					string strfloatY = "25";
					string strfloatW = "153";
					string strfloatH = "22";
					string strpageNo = "2";
					string strImageFile="G B Laing.jpg";
					DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml +  "<SIGN_ISIMAGE " + fieldType + "=\""+ fieldTypeImage +"\" ISIMAGE=\"" + imageTypeYes + "\" FLOATX=\"" + strfloatX + "\" FLOATY=\"" + strfloatY + "\" FLOATW=\"" + strfloatW  + "\" FLOATH=\"" + strfloatH  + "\" PAGENO=\"" + strpageNo  + "\" IMAGEPATH=\"" + strImageFile + "\"></SIGN_ISIMAGE>";
					//DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml +  "<SIGN_ISIMAGE " + fieldType + "=\""+ fieldTypeImage +"\" " + imageType + "=\"" + imageTypeYes + "\" " + floatX + "=\"" + strfloatX + "\" " + floatY + "=\"" + strfloatY + "\" " + floatW + "=\"" + strfloatW  + "\" " + floatH + "=\"" + strfloatH  + "\" " + pageNo + "=\"" + strpageNo  + "\" IMAGEPATH=\"" + strImageFile + "\"></SIGN_ISIMAGE>";
					//"<SIGNATURE " + fieldType +"=\""+ fieldTypeImage +"\" " + imageType + "=\"" + imageTypeYes + "\" " + floatX + "=\"" + strfloatX + "\" " + floatY + "=\"" + strfloatY + "\" " + floatW + "=\"" + strfloatW  + "\" " + floatH + "=\"" + strfloatH  + "\" " + pageNo + "=\"" + strpageNo  + "\"></SIGNATURE>";

					#endregion
				}
				else 	if (gStrPdfFor==PDFForAcord )
				{
					#region Dwelling Element for Accord 84
					
					Acord84DwellMultipleElement.AppendChild(Acord84REDWElement);
					Acord84REDWElement.SetAttribute(fieldType,fieldTypeNormal);
					Acord84REDWElement.SetAttribute(id,DwellingCtr.ToString()); 

					string dwellingID=	DwellingDetail["DWELLING_ID"].ToString();

					locAdd1=RemoveJunkXmlCharacters(DwellingDetail["LOC_ADD1"].ToString().Trim());
					locAdd2=RemoveJunkXmlCharacters(DwellingDetail["LOC_ADD2"].ToString().Trim());
					locCity=RemoveJunkXmlCharacters(DwellingDetail["LOC_CITY"].ToString().Trim());
					locState=RemoveJunkXmlCharacters(DwellingDetail["STATE_CODE"].ToString().Trim());
					locZip=RemoveJunkXmlCharacters(DwellingDetail["LOC_ZIP"].ToString().Trim());
				
					
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<HOFORM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["HO_FORM"].ToString()) +"</HOFORM>"; 
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<ENDORSEMENTDESC " + fieldType +"=\""+ fieldTypeText +"\">See Rental Dwelling Supplemental Application</ENDORSEMENTDESC>"; 
					if(strRecived_prem!="0" && strRecived_prem!="" && strRecived_prem!=".00")
					{
						if(strRecived_prem!="" && strRecived_prem!="0" && strRecived_prem!="0.00")
						{
							Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<DEPOSITAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strRecived_prem) +"</DEPOSITAMOUNT>"; 
						}
						else
						{
							Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<DEPOSITAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("") +"</DEPOSITAMOUNT>"; 
						}
					}
					if(sumTtl!="0")
					{
						sumTtl=(Convert.ToDouble(sumTtl)).ToString("###,###");
						Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<ESTIMATEDTOTALPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+ (sumTtl +".00" )+"</ESTIMATEDTOTALPREMIUM>"; 
					}
					if (sumTtl!="0")
					{
//						if(Recived_prem=)
//						{
//							Recived_prem=0;
//						}
						if(Convert.ToDouble(sumTtl) > Recived_prem)
						{
							MinimumDue=Convert.ToString(Convert.ToDouble(sumTtl) - Recived_prem);
						}
						else
						{
							MinimumDue=Convert.ToString(Recived_prem - Convert.ToDouble(sumTtl));
						}
//						if(MinimumDue!="0")
//						{						
//							MinimumDue=DoubleDollarFormat(Convert.ToDouble(MinimumDue));
//						}
						if(MinimumDue.IndexOf(".")>0)
						{
							
						}
						else
						{
							MinimumDue = MinimumDue + ".00";
						}
						Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<BALANCEAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(MinimumDue) +"</BALANCEAMOUNT>"; 
					}
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<CONSTRUCTIONCODE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["EXTERIOR_CONSTRUCTION"].ToString()) +"</CONSTRUCTIONCODE>"; 
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<EXTERIOR_CONSTRUCTION_DESC " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["EXTERIOR_CONSTRUCTION_DESC"].ToString()) +"</EXTERIOR_CONSTRUCTION_DESC>"; 
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<YRBUILT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["YEAR_BUILT"].ToString()) +"</YRBUILT>"; 
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<MARKETVALUE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["MARKET_VALUE"].ToString()) +"</MARKETVALUE>"; 
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<NOAPTS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["RATE_NO_APTS"].ToString()) +"</NOAPTS>";
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<REPLACEMENTCOST " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["REPLACEMENT_COST"].ToString()) +"</REPLACEMENTCOST>";	
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<CONSTRUCTIONTYPE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["STRUCTURE_TYPE"].ToString()) +"</CONSTRUCTIONTYPE>";	
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<USAGETYPE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["USAGE_TYPE"].ToString()) +"</USAGETYPE>";	
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<BUILDERSRISK " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["COC"].ToString()) +"</BUILDERSRISK>";	
					strCompDate= DwellingDetail["COMP_DATE"].ToString().Trim();
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<NOFAMILIES " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["NO_OF_FAMILIES"].ToString()) +"</NOFAMILIES>";

					string strPurPrice="";
					if(DwellingDetail["PUR_YR_PRICE"]!=null && DwellingDetail["PUR_YR_PRICE"].ToString()!="")
					{
						strPurPrice=DwellingDetail["PUR_YR_PRICE"].ToString().Trim();
						if(strPurPrice.StartsWith("/"))
						{
							strPurPrice=strPurPrice.Substring(1);  
						}

						if(strPurPrice.EndsWith("/.00"))
						{
							strPurPrice=strPurPrice.Substring(0,strPurPrice.LastIndexOf("/"));  
						}
					}
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<PURCHASEDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPurPrice.Replace(".00","")) +"</PURCHASEDATE>";
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<PROTECTIONCLASS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["PROT_CLASS"].ToString()) +"</PROTECTIONCLASS>";
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<DISTANCEHYDRANT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["HYDRANTDEC"].ToString()) +"</DISTANCEHYDRANT>";
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<DISTANCEFIRESTATION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["FIRE_STATION_DIST"].ToString()) +"</DISTANCEFIRESTATION>";
					if(DwellingDetail["CENT_ST_BURG_FIRE"].ToString()=="Y" || DwellingDetail["CENT_ST_FIRE"].ToString()=="Y")
					{
						Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<PROTECTIONTYPESMOKECENTRAL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 1 +"</PROTECTIONTYPESMOKECENTRAL>";
					}	
					else
					{
						Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<PROTECTIONTYPESMOKECENTRAL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 0 +"</PROTECTIONTYPESMOKECENTRAL>";
					}	

					if(DwellingDetail["CENT_ST_BURG_FIRE"].ToString()=="Y" || DwellingDetail["CENT_ST_BURG"].ToString()=="Y")
					{
						Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<PROTECTIONTYPEBURGLARCENTRAL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 1 +"</PROTECTIONTYPEBURGLARCENTRAL>";
					}	
					else
					{
						Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<PROTECTIONTYPEBURGLARCENTRAL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 0 +"</PROTECTIONTYPEBURGLARCENTRAL>";
					}	

					if(DwellingDetail["DIR_FIRE_AND_POLICE"].ToString()=="Y" || DwellingDetail["DIR_FIRE"].ToString()=="Y")
					{
						Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<PROTECTIONTYPESMOKEDIRECT " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 1 +"</PROTECTIONTYPESMOKEDIRECT>";
					}	
					else
					{
						Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<PROTECTIONTYPESMOKEDIRECT " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 0 +"</PROTECTIONTYPESMOKEDIRECT>";
					}	
					if(DwellingDetail["DIR_FIRE_AND_POLICE"].ToString()=="Y" || DwellingDetail["DIR_POLICE"].ToString()=="Y")
					{
						Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<PROTECTIONTYPEBURGLARDIRECT " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 1 +"</PROTECTIONTYPEBURGLARDIRECT>";
					}	
					else
					{
						Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<PROTECTIONTYPEBURGLARDIRECT " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 0 +"</PROTECTIONTYPEBURGLARDIRECT>";
					}	

					if(DwellingDetail["LOC_FIRE_GAS"].ToString()=="Y" || DwellingDetail["TWO_MORE_FIRE"].ToString()=="Y")
					{
						Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<PROTECTIONTYPESMOKELOCAL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 1 +"</PROTECTIONTYPESMOKELOCAL>";
					}	
					else
					{
						Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<PROTECTIONTYPESMOKELOCAL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 0 +"</PROTECTIONTYPESMOKELOCAL>";
					}	
					if(DwellingDetail["PRIMARY_HEAT_OTHER_DESC"].ToString().Length <= 0)
						Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<PRIMARYHEATTYPE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["PHEAT_TYPE"].ToString()) +"</PRIMARYHEATTYPE>";	
					else
						Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<PRIMARYHEATTYPE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["PRIMARY_HEAT_OTHER_DESC"].ToString()) +"</PRIMARYHEATTYPE>";	

					if(DwellingDetail["SECONDARY_HEAT_OTHER_DESC"].ToString().Length <= 0)
						Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<SECONDARYHEATTYPE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["SHEAT_TYPE"].ToString()) +"</SECONDARYHEATTYPE>";
					else
						Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<SECONDARYHEATTYPE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["SECONDARY_HEAT_OTHER_DESC"].ToString()) +"</SECONDARYHEATTYPE>";


					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<RENOVATIONTYPEPARTWIRING " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["WIRING"].ToString()) +"</RENOVATIONTYPEPARTWIRING>";
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<RENOVATIONTYPEWIRINGYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["WIRING_UPDATE_YEAR"].ToString()) +"</RENOVATIONTYPEWIRINGYEAR>";

					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<RENOVATIONTYPEPARTPLUMBING " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["PLUMBING"].ToString()) +"</RENOVATIONTYPEPARTPLUMBING>";
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<RENOVATIONTYPEPLUMBINGYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["PLUMBING_UPDATE_YEAR"].ToString()) +"</RENOVATIONTYPEPLUMBINGYEAR>";

					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<RENOVATIONTYPEPARTHEATING " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["HEATING"].ToString()) +"</RENOVATIONTYPEPARTHEATING>";
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<RENOVATIONTYPEHEATINGYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["HEATING_UPDATE_YEAR"].ToString()) +"</RENOVATIONTYPEHEATINGYEAR>";

					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<RENOVATIONTYPEPARTROOFING " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["ROOFING"].ToString()) +"</RENOVATIONTYPEPARTROOFING>";
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<RENOVATIONTYPEROOFINGYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["ROOFING_UPDATE_YEAR"].ToString()) +"</RENOVATIONTYPEROOFINGYEAR>";

					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<NOAMPS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["NO_OF_AMPS"].ToString()) +"</NOAMPS>";
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<CIRCUITBREAKERS " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["CIRCUIT_BREAKERS"].ToString()) +"</CIRCUITBREAKERS>";
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<FOUNDATIONOPEN " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["FOUNDATION"].ToString()) +"</FOUNDATIONOPEN>";
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<OCCUPANCY_CODE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["OCCUPANCY_CODE"].ToString()) +"</OCCUPANCY_CODE>";
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<NOWEEKSRENTED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["NO_WEEKS_RENTED"].ToString()) +"</NOWEEKSRENTED>";
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<VISIBLETONEIGHBOURS " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["NEIGHBOURS_VISIBLE"].ToString()) +"</VISIBLETONEIGHBOURS>";
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<ISSWIMMINGPOOL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["SWIMMING_POOL"].ToString()) +"</ISSWIMMINGPOOL>";
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<SWIMMINGPOOLPOSITION " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["SWIMMING_POOL_TYPE"].ToString()) +"</SWIMMINGPOOLPOSITION>";
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<SWIMMINGPOOLAPPROVEDFENCE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["APPROVED_FENCE"].ToString()) +"</SWIMMINGPOOLAPPROVEDFENCE>";
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<SWIMMINGPOOLDIVINGBOARD " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["DIVING_BOARD"].ToString()) +"</SWIMMINGPOOLDIVINGBOARD>";
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<SWIMMINGPOOLSLICE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["SLIDE"].ToString()) +"</SWIMMINGPOOLSLICE>";
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<INSPECTED " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["INSPECTED"].ToString()) +"</INSPECTED>";
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<OCCUPIEDDAILY " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["OCCUPIED_DAILY"].ToString()) +"</OCCUPIEDDAILY>";
					if(DwellingDetail["LOOKUP_VALUE_DESC"].ToString().Trim() !="")
					{
						if(DwellingDetail["LOOKUP_VALUE_DESC"].ToString().Trim().ToUpper().Equals("OTHER"))
							Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<ROOFMATERIAL " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(DwellingDetail["ROOF_OTHER_DESC"].ToString())  +"</ROOFMATERIAL>";
						else
							Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<ROOFMATERIAL " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(DwellingDetail["LOOKUP_VALUE_DESC"].ToString())  +"</ROOFMATERIAL>";
					}

					
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<SPRINKLER " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ RemoveJunkXmlCharacters(DwellingDetail["SPRINKER"].ToString())  +"</SPRINKLER>";

					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<BROAD " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ RemoveJunkXmlCharacters(DwellingDetail["BROAD"].ToString())  +"</BROAD>";
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<SPECIAL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ RemoveJunkXmlCharacters(DwellingDetail["SPECIAL"].ToString())  +"</SPECIAL>";
					if(!strTerritory.Equals(""))
					{
						int intfirstPos=0,intLastPos=0;
						intfirstPos=strTerritory.IndexOf("[");
						intLastPos=strTerritory.IndexOf("]");
 
						if((intfirstPos!=-1) && (intLastPos!=-1))	
						{
							strTerritory = strTerritory.Substring(intfirstPos+1,(intLastPos-intfirstPos)-1);   
						}

						Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<TERRCODE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ RemoveJunkXmlCharacters(strTerritory)  +"</TERRCODE>";
					}
					
					string premgrp = "";
					foreach (XmlNode PremiumNode in GetRentalPremGroup())
					{
						premgrp=getAttributeValue(PremiumNode,"GROUPDESC").ToString() ;
						if(premgrp.Length>0 )
						{
							premgrp = premgrp.Replace("Premium Group:","");
							premgrp = premgrp.Replace("Rated Class:","          ");
							if(premgrp.IndexOf(",") > 1)
								premgrp = premgrp.Substring(1, premgrp.IndexOf(",")-1);							
							else
								premgrp = premgrp.Substring(1, premgrp.Length - 1);
							break;
						}
					}
					Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<PREMGROUP " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ RemoveJunkXmlCharacters(premgrp)  +"</PREMGROUP>";
					
					#endregion

					#region Rental Dwelling Element for Supplement	
				
					SupplementDwellMultipleElement.AppendChild(SupplementREDWElement);
					SupplementREDWElement.SetAttribute(fieldType,fieldTypeNormal);
					SupplementREDWElement.SetAttribute(id,DwellingCtr.ToString());

					SupplementREDWElement.InnerXml= SupplementREDWElement.InnerXml + "<APPLICANTNAME " + fieldType +"=\""+ fieldTypeText +"\">"+ ApplicantName1 +"</APPLICANTNAME>";
					SupplementREDWElement.InnerXml= SupplementREDWElement.InnerXml + "<APPLICANTNO " + fieldType +"=\""+ fieldTypeText +"\">"+ PolicyNumber +"</APPLICANTNO>";
					//SupplementREDWElement.InnerXml= SupplementREDWElement.InnerXml + "<INSURANCESCORE " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(DwellingDetail["CUSTOMER_INSURANCE_SCORE"].ToString()) +"</INSURANCESCORE>";
					SupplementREDWElement.InnerXml= SupplementREDWElement.InnerXml + "<ENCLOSED_SWIMMING_POOLYN " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(DwellingDetail["IS_SWIMPOLL_HOTTUB"].ToString()) +"</ENCLOSED_SWIMMING_POOLYN>";
					//SupplementREDWElement.InnerXml= SupplementREDWElement.InnerXml +  "<PERCENTAGETYPE " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(DwellingDetail["CUSTOMER_INSURANCE_SCORE_TYPE"].ToString()) +"</PERCENTAGETYPE>";

					SupplementREDWElement.InnerXml = SupplementREDWElement.InnerXml +  "<DWELLING_RENTEDYN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_RENTED_IN_PART"].ToString()) + "</DWELLING_RENTEDYN>";
					SupplementREDWElement.InnerXml = SupplementREDWElement.InnerXml +  "<DWELLING_OWNEDYN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_DWELLING_OWNED_BY_OTHER"].ToString()) + "</DWELLING_OWNEDYN>";

					if(DSTempDataSet.Tables[0].Rows[0]["DESC_RENTED_IN_PART"].ToString() != "")
						SupplementREDWElement.InnerXml = SupplementREDWElement.InnerXml +  "<DWELLING_RENTED_DESC " + fieldType + "=\"" + fieldTypeText + "\">#1 : " + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["DESC_RENTED_IN_PART"].ToString()) + "</DWELLING_RENTED_DESC>";
					if(DSTempDataSet.Tables[0].Rows[0]["DESC_DWELLING_OWNED_BY_OTHER"].ToString() != "")
						SupplementREDWElement.InnerXml = SupplementREDWElement.InnerXml +  "<DWELLING_OWNED_DESC " + fieldType + "=\"" + fieldTypeText + "\">#2 : " + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["DESC_DWELLING_OWNED_BY_OTHER"].ToString()) + "</DWELLING_OWNED_DESC>";
					if(DSTempDataSet.Tables[0].Rows[0]["DESC_IS_SWIMPOLL_HOTTUB"].ToString() != "")
						SupplementREDWElement.InnerXml = SupplementREDWElement.InnerXml +  "<ENCLOSED_SWIMMING_POOL_DESC " + fieldType + "=\"" + fieldTypeText + "\">#3 : " + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["DESC_IS_SWIMPOLL_HOTTUB"].ToString()) + "</ENCLOSED_SWIMMING_POOL_DESC>";
					
					#endregion
				}
				#endregion

				#region COVERAGES
				//DataSet DSTempDwelling = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeOwner_Coverage_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + DwellingDetail["DWELLING_ID"] +  ",'" + gStrCalledFrom + "'");
				double dblSumTotal=0;
				int RowCounter=0;
				double APDIDed=0;
				string strLimit="";
				string InccovClimit="";
				string InccovDlimit="";
				// Premium access for increased coverage  c and d
				foreach(DataRow PremiumDetails in DSTempDwelling.Tables[1].Rows)
				{
					string componentCode = PremiumDetails["COMPONENT_CODE"].ToString();

					if(componentCode=="INCR_COV_C")
					{
						InccovClimit = PremiumDetails["COVERAGE_PREMIUM"].ToString();
						
					}

					if(componentCode=="INCR_COV_D")
					{
						InccovDlimit = PremiumDetails["COVERAGE_PREMIUM"].ToString();
						
					}
				}
				
				foreach(DataRow CoverageDetails in DSTempDwelling.Tables[0].Rows)
				{
					string covCode = CoverageDetails["COV_CODE"].ToString();
					

					#region Acord84 Coverage
					
					RowCounter++;
					#endregion

					#region Dec Page Coverages
					if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"].ToString()!="")
					{
						strLimit=CoverageDetails["LIMIT_1"].ToString().Trim() ;
						//if(!strLimit.Equals("0.00"))
						if(!strLimit.Equals("0"))
						{
							if(strLimit.EndsWith("/"))
							{
								strLimit=strLimit.Substring(0,strLimit.Length-1 ); 
							}
						}
						else
							strLimit="";

					}
					string strLimit1 = strLimit;
					//					if(strLimit1.IndexOf(".00") > 0)
					//					{
					//						strLimit1 = "$" + strLimit1.Replace(".00","");
					//					}
					if(strLimit1 != "0" && strLimit1 != "")
					{
						strLimit1 = "$" + strLimit1.Replace(".00","");
						
					}

					string strLimit2 = strLimit.Replace(".00","");
					
					if(strLimit2 != "0" && strLimit2 != "")
					{
						strLimit2 = "$" + strLimit2.Replace(".00","");
						
					}

					string strDed1 = CoverageDetails["DEDUCTIBLE"].ToString();
					if(strDed1.IndexOf(".00") > 0)
					{
						strDed1 = strDed1.Replace(".00","");
						if(strDed1!="" && strDed1!="0" && strDed1!="N/A")
						{
							string dbldeduc=(Convert.ToDouble(strDed1)).ToString("###,###");
							strDed1= "$" + dbldeduc;
						}
					}

					//string strPrem1 = GetPremium(DSTempDwelling, CoverageDetails["COV_CODE"].ToString());
					//Added by asfa (20-Feb-2008) - iTrack issue #3331
					string strPrem1="";
					if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
						strPrem1= GetPremiumBeforeCommit(DSTempDwelling, CoverageDetails["COV_CODE"].ToString(),htpremium );
					else // when gStrtemp != "temp"
						strPrem1 = GetPremium(DSTempDwelling, CoverageDetails["COV_CODE"].ToString());
					

					if(strPrem1.IndexOf(".00") > 0)
					{
						//strPrem1 = "$" + strPrem1;
						if(strPrem1!="" && strPrem1!="0" && strPrem1!="Included")
						{ 
							if(CoverageDetails["COV_CODE"].ToString()=="DWELL")
							{
								if(strPrem1.IndexOf(".00")>0)
								{
									strPrem1 = strPrem1.Replace(".00","");
								}
								if(ProPExpFee.IndexOf(".00")>0)
								{
									ProPExpFee = ProPExpFee.Replace(".00","");
								}
								strPrem1 = System.Convert.ToString(int.Parse(strPrem1) + int.Parse(ProPExpFee));
							}

							string dbldeduc=(Convert.ToDouble(strPrem1)).ToString("###,###");
							strPrem1= "$" + dbldeduc +".00";
							if(strPrem1=="$.00")
							{
								strPrem1="Included";
							}
						}
					}

					switch(CoverageDetails["COV_CODE"].ToString())
					{
							// Coverage A XML
						case "DWELL":
							if (gStrPdfFor==PDFForDecPage )
							{
								DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGEADWELLING " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</PC_COVERAGEADWELLING>";
								DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGEADWELLINGLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strLimit1)+"</PC_COVERAGEADWELLINGLIMIT>";

								DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGEADWELLINGPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem1)+"</PC_COVERAGEADWELLINGPREMIUM>";
								// getting premium from hash table to add on sumtotal component
								if(htpremium.Contains("COV_A"))
								{
									gstrGetPremium	=	htpremium["COV_A"].ToString();
									gintGetindex	=	gstrGetPremium.IndexOf(".");
									//									if(gintGetindex==	-1)
									//										DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGEADWELLINGPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["COV_A"].ToString() + ".00")+"</PC_COVERAGEADWELLINGPREMIUM>";
									//									else
									//										DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGEADWELLINGPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["COV_A"].ToString())+"</PC_COVERAGEADWELLINGPREMIUM>";
									//									DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGEADWELLINGPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempDwelling, covCode))+"</PC_COVERAGEADWELLINGPREMIUM>";
									dblSumTotal+=int.Parse(htpremium["COV_A"].ToString());
								}

							}
							else if (gStrPdfFor==PDFForAcord )
							{
								// Replace $ sign for accord
								strLimit2 = strLimit2.Replace("$","");
								Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<DWELLINGAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strLimit2) +"</DWELLINGAMOUNT>"; 
							}
							break;
							// coverage B XML
						case "OSTR":

							gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
							gobjWrapper.AddParameter("@POLID",gStrPolicyId);
							gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
							gobjWrapper.AddParameter("@DWELLINGID",DwellingDetail["DWELLING_ID"]);
							gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
							DSTempDataSet1 = gobjWrapper.ExecuteDataSet("Proc_GetPDF_OtherStructures_Amount");
							gobjWrapper.ClearParameteres();

							//DSTempDataSet1 = gobjSqlHelper.ExecuteDataSet("Proc_GetPDF_OtherStructures_Amount " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + DwellingDetail["DWELLING_ID"] +  ",'"  + gStrCalledFrom + "'");			
							double dblOtherStructAmt=0;
							if(DSTempDataSet1!=null )
							{
								if(DSTempDataSet1.Tables[0].Rows.Count>0)
								{
									if(DSTempDataSet1.Tables[0].Rows[0]["ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED"]!= null  && DSTempDataSet1.Tables[0].Rows[0]["ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED"].ToString()!="" )
										dblOtherStructAmt=double.Parse(DSTempDataSet1.Tables[0].Rows[0]["ADDITIONAL_AMOUNT_OF_INSURANCE_DESIRED"].ToString());
								}
							}
							if (gStrPdfFor==PDFForDecPage )
							{
								DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGEBOTHERSTRUCTURES " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</PC_COVERAGEBOTHERSTRUCTURES>";
								if(CoverageDetails["LIMIT_2"]!=null && CoverageDetails["LIMIT_2"].ToString()!="" )
								{
									string ostr_limit = (double.Parse(CoverageDetails["LIMIT_2"].ToString()) + (dblOtherStructAmt)).ToString();
									if(ostr_limit.IndexOf(".") == -1)
										DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGEBOTHERSTRUCTURESLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(strLimit1) +"</PC_COVERAGEBOTHERSTRUCTURESLIMIT>";
									else
										DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGEBOTHERSTRUCTURESLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(strLimit1)+"</PC_COVERAGEBOTHERSTRUCTURESLIMIT>";
								}

								DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGEBOTHERSTRUCTURESPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</PC_COVERAGEBOTHERSTRUCTURESPREMIUM>";
								// getting premium from hash table to add on sum total component
								if(htpremium.Contains("COV_B"))
								{
									string strINCRBPremium="";
									double totalBPremium=0;

									if(htpremium.Contains("INCR_COV_B"))
										strINCRBPremium=htpremium["INCR_COV_B"].ToString();

									gstrGetPremium	=	htpremium["COV_B"].ToString();
									gintGetindex	=	gstrGetPremium.IndexOf(".");
									
									if(strINCRBPremium!="" && gstrGetPremium!="")
										totalBPremium=double.Parse(gstrGetPremium.Trim()) + double.Parse(strINCRBPremium.Trim()); 
									else 
										totalBPremium=double.Parse(gstrGetPremium.Trim()) ;

									
									//									if(gintGetindex==	-1)
									//										DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGEBOTHERSTRUCTURESPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(totalBPremium.ToString()  + ".00")+"</PC_COVERAGEBOTHERSTRUCTURESPREMIUM>";
									//									else
									//										DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGEBOTHERSTRUCTURESPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(totalBPremium.ToString() )+"</PC_COVERAGEBOTHERSTRUCTURESPREMIUM>";
									//									DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGEBOTHERSTRUCTURESPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempDwelling, covCode))+"</PC_COVERAGEBOTHERSTRUCTURESPREMIUM>";

									dblSumTotal+=int.Parse(totalBPremium.ToString() );
								}
							}
							else if (gStrPdfFor==PDFForAcord )
							{
								// replace $ sign 
								strLimit2 = strLimit2.Replace("$","");
								Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<OTHERSTRUCTURESAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strLimit2) +"</OTHERSTRUCTURESAMOUNT>"; 
							}
							break;
							// coverage C XML
						case "LPP":
							if (gStrPdfFor==PDFForDecPage )
							{
								DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGECUNSCHEDULEDPERSONALPROPERTY " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</PC_COVERAGECUNSCHEDULEDPERSONALPROPERTY>";
								DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strLimit1)+"</PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYLIMIT>";
								if(InccovClimit !="")
								{
									strPrem1="0";
									if(strPrem1.IndexOf("$")>0)
									{
										strPrem1 = strPrem1.Replace("$","");
									}
									if(strPrem1 =="Included")
									{
										strPrem1="0";
									}
									strPrem1=Convert.ToString(Convert.ToDouble(strPrem1) + Convert.ToDouble(InccovClimit));
									strPrem1 = "$" + strPrem1 + ".00";
								}
								
								// Hash table premium to add on sumtotal component 
								if(gStrtemp!="final")
								{
										string strINCRCPremium="0";
										double totalCPremium=0;

										if(htpremium.Contains("INCR_COV_C"))
											strINCRCPremium=htpremium["INCR_COV_C"].ToString();

//										gstrGetPremium	=	htpremium["COV_C"].ToString();
//										gintGetindex	=	gstrGetPremium.IndexOf(".");

										if(strINCRCPremium!="")
											totalCPremium += double.Parse(strINCRCPremium.Trim()); 
//										else
//											totalCPremium=double.Parse(gstrGetPremium.Trim());
										dblSumTotal+=int.Parse(totalCPremium.ToString());
										if(Convert.ToDouble(strINCRCPremium)> 0)
										{
											strPrem1="0";
											strPrem1=Convert.ToString(Convert.ToDouble(strPrem1) + Convert.ToDouble(strINCRCPremium));
											strPrem1="$" + strPrem1 +".00";
										}
									//}
								}
								// 
								if(strPrem1=="0" || strPrem1 =="0.00")
								{
									DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYPREMIUM>";
								}
								else
									DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem1)+"</PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYPREMIUM>";
								
								
									//									if(gintGetindex==	-1)
									//										DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(totalCPremium.ToString() + ".00")+"</PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYPREMIUM>";
									//									else
									//										DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(totalCPremium.ToString())+"</PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYPREMIUM>";
									//									DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempDwelling, covCode))+"</PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYPREMIUM>";
								
							}
							else if (gStrPdfFor==PDFForAcord )
							{
								// replace $ sign 
								strLimit2 = strLimit2.Replace("$","");
								Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<PERSONALPROPERTYAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strLimit2) +"</PERSONALPROPERTYAMOUNT>"; 
							}
							break;

							// coverage D XML
						case "RV":
							if (gStrPdfFor==PDFForDecPage )
							{
								DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGEDLOSSOFUSE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</PC_COVERAGEDLOSSOFUSE>";
								DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGEDLOSSOFUSELIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strLimit1)+"</PC_COVERAGEDLOSSOFUSELIMIT>";
								if(InccovDlimit !="")
								{
									strPrem1="0";
									if(strPrem1.IndexOf("$")>0)
									{
										strPrem1 = strPrem1.Replace("$","");
									}
									if(strPrem1=="Included")
									{
										strPrem1="0";
									}
									strPrem1=Convert.ToString(Convert.ToDouble(strPrem1) + Convert.ToDouble(InccovDlimit));
									strPrem1 = "$" + strPrem1 + ".00";
								}
								// hash table premium to add on sumtotal component
								if(gStrtemp!="final")
								{
									
										string strINCRDPremium="0";
										double totalDPremium=0;
										if(htpremium.Contains("INCR_COV_D"))
											strINCRDPremium=htpremium["INCR_COV_D"].ToString();

									
//										gstrGetPremium	=	htpremium["COV_C"].ToString();
//										gintGetindex	=	gstrGetPremium.IndexOf(".");

										if(strINCRDPremium!="")
											totalDPremium += double.Parse(strINCRDPremium.Trim()); 
//										else
//											totalDPremium=double.Parse(gstrGetPremium.Trim());
										dblSumTotal+=int.Parse(totalDPremium.ToString());
										if(Convert.ToDouble(strINCRDPremium) >0)
										{
											strPrem1="0";
											strPrem1=Convert.ToString(Convert.ToDouble(strPrem1) + Convert.ToDouble(strINCRDPremium));
											strPrem1="$" + strPrem1 +".00";
										}
									

								}
								
								if(strPrem1=="0" || strPrem1 =="0.00")
								{
									DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGEDLOSSOFUSEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</PC_COVERAGEDLOSSOFUSEPREMIUM>";
								
								}
								else
								{
									DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGEDLOSSOFUSEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem1)+"</PC_COVERAGEDLOSSOFUSEPREMIUM>";
								}	//									if(gintGetindex==	-1)
									//										DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGEDLOSSOFUSEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(totalDPremium.ToString() + ".00")+"</PC_COVERAGEDLOSSOFUSEPREMIUM>";
									//									else
									//										DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGEDLOSSOFUSEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(totalDPremium.ToString())+"</PC_COVERAGEDLOSSOFUSEPREMIUM>";
									//									DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PC_COVERAGEDLOSSOFUSEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempDwelling, covCode))+"</PC_COVERAGEDLOSSOFUSEPREMIUM>";

									
							}
							else if (gStrPdfFor==PDFForAcord )
							{
								// replace $ sign 
								strLimit2 = strLimit2.Replace("$","");
								Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<RENTAL_VALUE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strLimit2) +"</RENTAL_VALUE>"; 
							}
							break;

							//coverage E XML
						case "CSL":
							if (gStrPdfFor==PDFForDecPage )
							{
								if(strLimit1=="" || strLimit1=="0" || strLimit1=="0.00" || strLimit1=="$0" || strLimit1=="$0 No Coverage")
								{
									DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PERSONALLIABILITYLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</PERSONALLIABILITYLIMIT>";							
								}
								else
								{
									DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PERSONALLIABILITYLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strLimit1)+"</PERSONALLIABILITYLIMIT>";							
								}
								// itrack 4208 if coverage have no coverage then we should show noting on premium and limit
								if (strLimit1=="" || strLimit1=="0" || strLimit1=="0.00" || strLimit1=="$0" || strLimit1=="$0 No Coverage")
								{
									DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PERSONALLIABILITYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+""+"</PERSONALLIABILITYPREMIUM>";
								}	
								else if(strPrem1=="0" || strPrem1 =="0.00" || strPrem1 =="")
								{
									DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PERSONALLIABILITYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+"Included"+"</PERSONALLIABILITYPREMIUM>";
								}
								else
								{
									DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PERSONALLIABILITYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem1)+"</PERSONALLIABILITYPREMIUM>";
								}
								// Hash table premium to add on sumtotal component
								if(htpremium.Contains("COV_E"))
								{
									gstrGetPremium	=	htpremium["COV_E"].ToString();
									gintGetindex	=	gstrGetPremium.IndexOf(".");
									//									if(gintGetindex==	-1)
									//										DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PERSONALLIABILITYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["COV_E"].ToString() + ".00")+"</PERSONALLIABILITYPREMIUM>";
									//									else
									//										DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PERSONALLIABILITYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["COV_E"].ToString())+"</PERSONALLIABILITYPREMIUM>";
									//									DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<PERSONALLIABILITYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempDwelling, covCode))+"</PERSONALLIABILITYPREMIUM>";

									dblSumTotal+=int.Parse(htpremium["COV_E"].ToString());
								}
							}
							else if (gStrPdfFor==PDFForAcord )
							{
								// replace $ sign 
								strLimit2 = strLimit2.Replace("$","");
								if(strLimit1=="" || strLimit1=="0" || strLimit1=="0.00" || strLimit1=="$0" || strLimit1=="$0 No Coverage")
								{
									Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<PERSONALLIABILITYAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("") +"</PERSONALLIABILITYAMOUNT>"; 
								}
								else
								{
									Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<PERSONALLIABILITYAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strLimit2) +"</PERSONALLIABILITYAMOUNT>"; 
								}
							}
							break;

							// coverage F XML
						case "MEDPM":
							if (gStrPdfFor==PDFForDecPage )
							{
								if(strLimit1 == "" || strLimit1 =="$0" || strLimit1=="$0 No Coverage")
								{
									DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<MEDICALPAYMENTSLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</MEDICALPAYMENTSLIMIT>";							
								}
								else
								{
									DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<MEDICALPAYMENTSLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strLimit1+" Per Person/$25,000 Each Occurrence")+"</MEDICALPAYMENTSLIMIT>";							
								}
								// itrack 4208 if coverage have no coverage then we should show noting on premium and limit
								if(strLimit1 == "" || strLimit1 =="$0" || strLimit1=="$0 No Coverage")
								{
									DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<MEDICALPAYMENTSPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+""+"</MEDICALPAYMENTSPREMIUM>";
								}	
								else if(strPrem1=="0" || strPrem1 =="0.00" || strPrem1 =="")
								{
									DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<MEDICALPAYMENTSPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+"Included"+"</MEDICALPAYMENTSPREMIUM>";
								}
								else
								{
									DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<MEDICALPAYMENTSPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem1)+"</MEDICALPAYMENTSPREMIUM>";
								}
								// Hash table premium to add on sumtotal component
								if(htpremium.Contains("COV_F"))
								{
									gstrGetPremium	=	htpremium["COV_F"].ToString();
									gintGetindex	=	gstrGetPremium.IndexOf(".");
									//									if(gintGetindex==	-1)
									//										DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<MEDICALPAYMENTSPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["COV_F"].ToString() + ".00")+"</MEDICALPAYMENTSPREMIUM>";
									//									else
									//										DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<MEDICALPAYMENTSPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["COV_F"].ToString())+"</MEDICALPAYMENTSPREMIUM>";
									//									DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<MEDICALPAYMENTSPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempDwelling, covCode))+"</MEDICALPAYMENTSPREMIUM>";

									dblSumTotal+=int.Parse(htpremium["COV_F"].ToString());
								}
							}
							else if (gStrPdfFor==PDFForAcord )
							{
								// replace $ sign 
								strLimit2 = strLimit2.Replace("$","");
								if(strLimit1=="" || strLimit1=="0" || strLimit1=="0.00" || strLimit1=="$0" || strLimit1=="$0 No Coverage")
								{
									Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<MEDICALPAYMENTSAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("") +"</MEDICALPAYMENTSAMOUNT>"; 
								}
								else
								{
									Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<MEDICALPAYMENTSAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strLimit2) +"</MEDICALPAYMENTSAMOUNT>"; 
								}
							}
							break;	
					
							// All Peril deductible XML
						case "APDI":
							// for dec page
							if (gStrPdfFor==PDFForDecPage )
							{
								if(CoverageDetails["DEDUCTIBLE"]!=null && CoverageDetails["DEDUCTIBLE"].ToString()!="" )
									if(double.Parse(CoverageDetails["DEDUCTIBLE"].ToString()) > 0 )
									{
										DecPageREDWElement.InnerXml= DecPageREDWElement.InnerXml + "<ALL_PERIL_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDed1) +"</ALL_PERIL_DEDUCTIBLE>";							
										APDIDed=double.Parse(CoverageDetails["DEDUCTIBLE"].ToString());
									}
							}
								//for Accord xml
							else if (gStrPdfFor==PDFForAcord )
							{
								if(CoverageDetails["DEDUCTIBLE"]!=null && CoverageDetails["DEDUCTIBLE"].ToString()!="" )
								{
									if(double.Parse(CoverageDetails["DEDUCTIBLE"].ToString()) > 0 )
									{
										Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<ALL_PERIL_CHECK " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+1+"</ALL_PERIL_CHECK>"; 
										Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<ALL_PERIL_AMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE"].ToString().Replace(".00","")) +"</ALL_PERIL_AMOUNT>"; 
										APDIDed=double.Parse(CoverageDetails["DEDUCTIBLE"].ToString());
									}
									else
									{
										Acord84REDWElement.InnerXml= Acord84REDWElement.InnerXml +  "<ALL_PERIL_CHECK " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+0+"</ALL_PERIL_CHECK>"; 
									}
										
								}
							}
							break;
					}								
					#endregion
				}
				#endregion

				#region Endorsements
				XmlElement DecPageREDWEndmts;
				DecPageREDWEndmts= AcordPDFXML.CreateElement("ENDORSEMENTS");		

				XmlElement SupplementREDWEndmts;
				SupplementREDWEndmts= AcordPDFXML.CreateElement("ENDORSEMENTS");

				#region Declaration Page Rental Dwelling Endorsements
				if (gStrPdfFor==PDFForDecPage )
				{
					DecPageREDWElement.AppendChild(DecPageREDWEndmts);
					DecPageREDWEndmts.SetAttribute(fieldType,fieldTypeMultiple);
					DecPageREDWEndmts.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEREDWCOV"));
					DecPageREDWEndmts.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEREDWCOV"));
					DecPageREDWEndmts.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEREDWCOVEXTN"));
					DecPageREDWEndmts.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEREDWCOVEXTN"));
				}
				#endregion

				#region Supplemental Page Rental Dwelling Endorsements
				if (gStrPdfFor==PDFForAcord )
				{
					SupplementREDWElement.AppendChild(SupplementREDWEndmts);
					SupplementREDWEndmts.SetAttribute(fieldType,fieldTypeMultiple);
					SupplementREDWEndmts.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTREDWEND"));
					SupplementREDWEndmts.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTREDWEND"));
					SupplementREDWEndmts.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTREDWENDEXTN"));
					SupplementREDWEndmts.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTREDWENDEXTN"));
				}
				#endregion
					
				int endCtr=0;
				int DecPageEndCtr=0;
				
				other_struct_cov = 0;

				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@DWELLINGID",DwellingDetail["DWELLING_ID"]);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DataSet DSTempOutbuildings_2 = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_otherstructures");
				gobjWrapper.ClearParameteres();

				//DataSet DSTempOutbuildings_2 = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_otherstructures " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + DwellingDetail["DWELLING_ID"] +  ",'" + gStrCalledFrom + "'");
				foreach(DataRow DROutbuil in DSTempOutbuildings_2.Tables[0].Rows)
				{
					if(DROutbuil["PREMISES_DESCRIPTION"].ToString() !="")
					{
						other_struct_cov++;
					}
				}

				//Modified by Mohit Agarwal 5-Nov-07 for Endorsements Print Order
				if (gStrPdfFor == PDFForDecPage)
				{
					foreach(DataRow CoverageDetails in DSTempDwelling.Tables[0].Rows)
					{	
						int prnCtr = 0;
						string CovCode=CoverageDetails["COV_CODE"].ToString();
						if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y" && CoverageDetails["ENDORS_PRINT"].ToString() !="N")
						{
							while(prnOrd_covCode[prnCtr] != CoverageDetails["COV_CODE"].ToString() && prnOrd_covCode[prnCtr] != null)
							{
								prnCtr++;
							}
							//Endorsement already in list
							if(prnOrd_covCode[prnCtr] == CoverageDetails["COV_CODE"].ToString())
								continue;

							prnOrd_covCode[prnCtr] = CoverageDetails["COV_CODE"].ToString();
							if(CoverageDetails["ATTACH_FILE"] != System.DBNull.Value)
								prnOrd_attFile[prnCtr] = CoverageDetails["ATTACH_FILE"].ToString();
							if(CoverageDetails["PRINT_ORDER"] != System.DBNull.Value)
								prnOrd[prnCtr] = int.Parse(CoverageDetails["PRINT_ORDER"].ToString());
							else
								prnOrd[prnCtr] = 0;
						}
						//					else
						//						prnOrd[prnCtr] = 999999;
						//					prnCtr++;
					}
					if(DSTempDwelling.Tables[0].Rows.Count + other_struct_cov - 7 >= 12)
						extraCovPage++;
				}

				foreach(DataRow CoverageDetails in DSTempDwelling.Tables[0].Rows)
				{		
					strLimit="";
					string strDed="";
					string CovCode = CoverageDetails["COV_CODE"].ToString();
					string comCode ="";
					if(CoverageDetails["COMPONENT_CODE"]!=null && CoverageDetails["COMPONENT_CODE"].ToString()!="" )
						comCode = CoverageDetails["COMPONENT_CODE"].ToString().ToString().ToUpper()  ;

					#region DecPage
					if (gStrPdfFor==PDFForDecPage)
					{
						if (CovCode!="DWELL" && CovCode!="OSTR" && CovCode!="LPP" && CovCode!="RV" && CovCode!="CSL" && CovCode!="MEDPM" && CovCode!="APDI")
						{
							if(CoverageDetails["LIMIT_1"]!=null)
							{
								strLimit=CoverageDetails["LIMIT_1"].ToString().Trim() ;
								//if(!strLimit.Equals("0.00"))
								if(!(strLimit.Equals("0") || strLimit == "" || strLimit.Equals("0.00")))
								{
									if(strLimit.EndsWith("/"))
									{
										strLimit=strLimit.Substring(0,strLimit.Length-1 ); 
									}
								}
								else
									strLimit="Included";
							}

							if(CoverageDetails["DEDUCTIBLE"]!=null)
							{
								strDed=CoverageDetails["DEDUCTIBLE"].ToString().Trim() ;
								if(strDed.Equals("0.00"))
								{
									strDed="";
								}
							}

							XmlElement DecPageDwellEndmtElement;
							DecPageDwellEndmtElement = AcordPDFXML.CreateElement("ENDORSEMENTSINFO");
							DecPageREDWEndmts.AppendChild(DecPageDwellEndmtElement);
							DecPageDwellEndmtElement.SetAttribute(fieldType,fieldTypeNormal);
							DecPageDwellEndmtElement.SetAttribute(id,(DecPageEndCtr).ToString() );

							if(gStrCalledFrom.Equals(CalledFromPolicy))
							{
								DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<POLICYNO " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNO>";
								DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<POLICYPERIODFROM " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</POLICYPERIODFROM>";
								DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<POLICYPERIODTO " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</POLICYPERIODTO>";
								//Added for Itrack # 3619
								DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<ALL_POLICY_TYPE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyType + "</ALL_POLICY_TYPE>";
								//
								DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<REASON " + fieldType + "=\"" + fieldTypeText + "\">" + Reason  + "</REASON>";
							}
							DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<PRIMARYCONTACTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName1 + "</PRIMARYCONTACTNAME>";
							if(DecPageEndCtr >= 12)
								DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<PAGE " + fieldType + "=\"" + fieldTypeText + "\">" + (extraCovPage+pagecounter+1).ToString() + " of " + (totcovPage + totdwellpage).ToString() + "</PAGE>";
							DecPageDwellEndmtElement.InnerXml += "<HM_FORMNO " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_FORMNO>";							
							DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";                            
							DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							/*// ADDED FOR ITRACK # 3495
							if(CovCode.Trim().ToUpper() =="BOSTR")
							{	
								DataSet DSTempOutbuildings1 = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_otherstructures " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + DwellingDetail["DWELLING_ID"] +  ",'" + gStrCalledFrom + "'");
								string strIncLmtCov= "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())
								foreach(DataRow DROutbuil in DSTempOutbuildings1.Tables[0].Rows)
								{
									if(DROutbuil["SATELLITE_EQUIPMENT"].ToString() != "10963")
									{
										if(DROutbuil["PREMISES_DESCRIPTION"].ToString() != "")
										{
											strIncLmtCov +=";\n" + DROutbuil["PREMISES_DESCRIPTION"].ToString();
										}
									}
								}
								strIncLmtCov += "</HM_ENDDESCRIPTION>";
								DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + strIncLmtCov;
							}
							else
								DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							*/
							//if(strLimit.Trim().Equals("0.00"))
							if(strLimit.Trim().Equals("0") || strLimit == "")
								strLimit="Included";
							string strLimit1 = strLimit;
							//if(strLimit1.IndexOf(".00") > 0)
							if(strLimit1 != "0" && strLimit1 != "Included")
							{
								strLimit1 = "$" + strLimit1.Replace(".00","");
							}
							if(strLimit1 == "0")
							{
								strLimit1 ="";
							}
	
							switch (CovCode.Trim().ToUpper())  
							{
								case "EDP469":
									if(strDed.Trim().Equals("0.00"))
										strDed="";
									else
									{
										if(strDed.IndexOf(".")!=-1 )
										{
											strDed=strDed.Substring(0,strDed.IndexOf("."));   
										}
										strDed+="%";
									}
									break;
								case "MSC480":
									strDed="2%";
									break;
								case "BR1143":
								case "ACS":
									strDed=APDIDed.ToString()+".00" ; 
									break;
								case "BIAA":
									strDed=APDIDed.ToString()+".00"; 
									break;
								case "CS":
									strDed=APDIDed.ToString()+".00"; 
									break;
								case "RTE":
									strDed=APDIDed.ToString()+".00"; 
									break;
								case "TSPL":
									strDed=APDIDed.ToString()+".00"; 
									break;
								case "DP321":
								case "DP02":
								case "DP03":
								case "DP320":
									//Added on 12 Feb
								case "DP392":
									strDed="N/A";
									break;
									//itrack issue 3467 
								case "PIOSS":
									strDed="N/A";
									break;
								case "IOO":
									strDed="N/A";
									break;
									//itrack issue 3467 
								case "DP382":
									strDed="N/A";
									break;
									//itrack issue 3467 
								case "DP289":
									strDed=APDIDed.ToString()+".00" ;
									break;
								case "DP422":
									strDed=APDIDed.ToString()+".00";
									break;
								case "SD":
								case "BOSTR":
									//strDed=APDIDed.ToString() + ".00"; 
									strLimit="0";
									break;
								case "DP100":
								case "DP113":
								case "DP121":
								case "DP417":
								case "DP216":
								case "LP417":
									strDed="N/A"; 
									break;
								case "LP124":
									strDed="N/A"; 
									break;
																	
							}

							strLimit1 = strLimit;
							//if(strLimit1.IndexOf(".00") > 0)
							if(strLimit1 != "0" && strLimit1 != "Included")
							{
								strLimit1 = "$" + strLimit1.Replace(".00","");
							}
							if(strLimit1 == "0")
							{
								strLimit1 ="";
							}
							string strDed1 = strDed;
							if(strDed1.IndexOf(".00") > 0)
							{
								strDed1 = strDed1.Replace(".00","");
							
								if(strDed1!="" && strDed1!="0" && strDed1!="N/A")
								{
									string dbldeduc=(Convert.ToDouble(strDed1)).ToString("###,###");
									strDed1= "$" + dbldeduc;
								}
							}
							//string strPrem1 = GetPremium(DSTempDwelling, CovCode);
							//Added by asfa (20-Feb-2008) - iTrack issue #3331
							string strPrem1="";
							if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
								strPrem1= GetPremiumBeforeCommit(DSTempDwelling, CovCode, htpremium );
							else // when gStrtemp != "temp"
								strPrem1 = GetPremium(DSTempDwelling, CovCode);
							if(strPrem1 =="")
								strPrem1 = "Included";
							if(strPrem1.IndexOf(".00") > 0)
							{
								if(strPrem1!="" && strPrem1!="0" && strPrem1!="Included" )
								{
									string dbldeduc=(Convert.ToDouble(strPrem1)).ToString("###,###");
									strPrem1= "$" + dbldeduc +".00";
								}
							}
							switch (CovCode.Trim().ToUpper())  
							{
								case "DP03":
								case "DP02":
								case "DP100":
								case "DP113":
								case "DP121":
								case "DP321":
								case "DP417":
								case "DP422":
									strLimit1="Included";
									break;
								case "DP216":								
									//case "DP289":
									//strDed=APDIDed.ToString() + ".00";
									//	//DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDed)+"</HM_ENDDEDUCTIBLE>";
									//	break;
								case "BR1143":
									// LP-124
								case "CSL":
									
								case "MEDPM":
								case "IOO":
								case "PIOSS":
									// LP-124
									strPrem1="Included"; 
									break;
								case "DP900":
									strLimit1="N/A";
									strDed1="N/A";
									strPrem1="N/A";
									break;
							}
							if(CovCode.Trim().ToUpper() !="BOSTR" && CovCode.Trim().ToUpper() !="SD" && CovCode.Trim().ToUpper() !="DP900")
							{
								DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strLimit1)+"</HM_ENDLIMIT>";
								DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDed1)+"</HM_ENDDEDUCTIBLE>";
							}
							//							if(htpremium.Contains(comCode)) 
							//								DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+(RemoveJunkXmlCharacters(htpremium[comCode].ToString()) + ".00") +"</HM_ENDPREMIUM>";
							
							DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem1) +"</HM_ENDPREMIUM>";
							if(CovCode.Trim().ToUpper() =="BOSTR" || CovCode.Trim().ToUpper() =="SD" || CovCode.Trim().ToUpper() =="DP900")
							{	

								gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
								gobjWrapper.AddParameter("@POLID",gStrPolicyId);
								gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
								gobjWrapper.AddParameter("@DWELLINGID",DwellingDetail["DWELLING_ID"]);
								gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
								DataSet DSTempOutbuildings1 = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_otherstructures");
								gobjWrapper.ClearParameteres();

								//DataSet DSTempOutbuildings1 = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_otherstructures " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + DwellingDetail["DWELLING_ID"] +  ",'" + gStrCalledFrom + "'");
								// if promisses discription is not provided then 
								//it will be shown on satelite dish and other structure respectively
								strDed=APDIDed.ToString(); 
								if(strDed.Equals("0.00"))
								{
									strDed="";
								}
								if(strDed.IndexOf(".00")>0)
								{
									strDed=strDed.Replace(".00","");
								}
								else if(strDed.IndexOf(".0")>0)
								{
									strDed=strDed.Replace(".0","");
								}
								else
								{
									strDed=strDed;
								}
								if(strDed!="" && strDed!="0" && strDed!="N/A")
								{
									string dbldeduc=(Convert.ToDouble(strDed)).ToString("###,###");
									strDed= "$" + dbldeduc;
								}
								double dbllimit=0;
								if(CovCode.Trim().ToUpper() =="BOSTR" || CovCode.Trim().ToUpper() =="SD")
								{
									foreach(DataRow DROutbuil in DSTempOutbuildings1.Tables[0].Rows)
									{
										if(CovCode.Trim().ToUpper() =="BOSTR" && DROutbuil["SATELLITE_EQUIPMENT"].ToString() != "10963")
										{
											if(DROutbuil["PREMISES_DESCRIPTION"].ToString() ==null || DROutbuil["PREMISES_DESCRIPTION"].ToString() =="")
											{
												if( DROutbuil["INSURING_VALUE"].ToString()!="" && DROutbuil["INSURING_VALUE"].ToString()!=null)
												dbllimit += System.Convert.ToDouble(DROutbuil["INSURING_VALUE"].ToString());
												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDed)+"</HM_ENDDEDUCTIBLE>";

											}
										}
										if(CovCode.Trim().ToUpper() =="SD" && DROutbuil["SATELLITE_EQUIPMENT"].ToString() == "10963")
										{
											if(DROutbuil["PREMISES_DESCRIPTION"].ToString() ==null || DROutbuil["PREMISES_DESCRIPTION"].ToString() =="")
											{
												if(DROutbuil["INSURING_VALUE"].ToString()!="" && DROutbuil["INSURING_VALUE"].ToString()!=null)
												dbllimit += System.Convert.ToDouble(DROutbuil["INSURING_VALUE"].ToString());
												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDed)+"</HM_ENDDEDUCTIBLE>";

											}
										}
									}
									if(dbllimit !=0 )
									DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetIntFormat(dbllimit.ToString()))+"</HM_ENDLIMIT>";
								}
								//DSTempOutbuildings1.Tables[0].Rows.Count;
								//XmlElement DecPageDwellEndmtElement;
								//string strIncLmtCov = DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString());
								
								foreach(DataRow DROutbuil in DSTempOutbuildings1.Tables[0].Rows)
								{
									string otherstructuredesc="";
									string insurevalue=DROutbuil["INSURING_VALUE"].ToString();
									if(insurevalue.IndexOf(".00") > 0)
										insurevalue =  insurevalue.Replace(".00","");
									// Other Structure Description Details those have Satellite equipment
									if(CovCode.Trim().ToUpper() =="SD")
									{
										if(DROutbuil["SATELLITE_EQUIPMENT"].ToString() == "10963" && CovCode.Trim().ToUpper() =="SD")								
										{
											otherstructuredesc= DROutbuil["PREMISES_DESCRIPTION"].ToString();
											if(otherstructuredesc !="")
											{
											DecPageEndCtr++;
											DecPageDwellEndmtElement = AcordPDFXML.CreateElement("ENDORSEMENTSINFO");
											DecPageREDWEndmts.AppendChild(DecPageDwellEndmtElement);
											DecPageDwellEndmtElement.SetAttribute(fieldType,fieldTypeMultiple);
											DecPageDwellEndmtElement.SetAttribute(id,(DecPageEndCtr).ToString() );
											
												if(gStrCalledFrom.Equals(CalledFromPolicy))
												{
													DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<POLICYNO " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNO>";
													DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<POLICYPERIODFROM " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</POLICYPERIODFROM>";
													DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<POLICYPERIODTO " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</POLICYPERIODTO>";
													//ADDED FOR ITRACK # 3619
													DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<ALL_POLICY_TYPE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyType + "</ALL_POLICY_TYPE>";
													//
													DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<REASON " + fieldType + "=\"" + fieldTypeText + "\">" + Reason  + "</REASON>";
												}
												DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<PRIMARYCONTACTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName1 + "</PRIMARYCONTACTNAME>";
												if(DecPageEndCtr >= 12)
													DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<PAGE " + fieldType + "=\"" + fieldTypeText + "\">" + (extraCovPage+pagecounter+1).ToString() + " of " + (totcovPage + totdwellpage).ToString() + "</PAGE>";
										
												//											if(otherstructuredesc !="")
												//											{
												DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<HM_ENDDESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">" + "   - "+ RemoveJunkXmlCharacters(otherstructuredesc) + "</HM_ENDDESCRIPTION>";
												if(DROutbuil["INSURING_VALUE"].ToString()!="")
												{
													DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\"> $"+insurevalue+"</HM_ENDLIMIT>";
												
												}
												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDed)+"</HM_ENDDEDUCTIBLE>";
											}
											//										if(nsatctr<1)
											//										{
											//											if(DROutbuil["INSURING_VALUE"].ToString() != "")
											//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + DollarFormat(Convert.ToDouble(DROutbuil["INSURING_VALUE"].ToString())).Replace(".00",""))+"</HM_ENDLIMIT>";
											//											nsatctr++;	
											//										}
										}
									}
										// ADDED FOR ITRACK # 3495
										// Other Structure Description Details those do not have satelite equipment.
										//DROutbuil["SATELLITE_EQUIPMENT"].ToString() != "10963" && &&  CovCode.Trim().ToUpper() !="DP900"
									else if(CovCode.Trim().ToUpper() =="BOSTR" && DROutbuil["COVERAGE_BASIS"].ToString() == "NONE" && DROutbuil["SATELLITE_EQUIPMENT"].ToString() != "10963")
									{
										otherstructuredesc= DROutbuil["PREMISES_DESCRIPTION"].ToString();
										if(otherstructuredesc !="")
										{
										DecPageEndCtr++;
										DecPageDwellEndmtElement = AcordPDFXML.CreateElement("ENDORSEMENTSINFO");
										DecPageREDWEndmts.AppendChild(DecPageDwellEndmtElement);
										DecPageDwellEndmtElement.SetAttribute(fieldType,fieldTypeMultiple);
										DecPageDwellEndmtElement.SetAttribute(id,(DecPageEndCtr).ToString() );
										
											if(gStrCalledFrom.Equals(CalledFromPolicy))
											{
												DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<POLICYNO " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNO>";
												DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<POLICYPERIODFROM " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</POLICYPERIODFROM>";
												DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<POLICYPERIODTO " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</POLICYPERIODTO>";
												//ADDED FOR ITRACK # 3619
												DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<ALL_POLICY_TYPE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyType + "</ALL_POLICY_TYPE>";
												//
												DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<REASON " + fieldType + "=\"" + fieldTypeText + "\">" + Reason  + "</REASON>";
											}
											DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<PRIMARYCONTACTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName1 + "</PRIMARYCONTACTNAME>";
											if(DecPageEndCtr >= 12)
												DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<PAGE " + fieldType + "=\"" + fieldTypeText + "\">" + (extraCovPage+pagecounter+1).ToString() + " of " + (totcovPage + totdwellpage).ToString() + "</PAGE>";
											//										if(otherstructuredesc !="")
											//										{
											DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<HM_ENDDESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">" + "   - "+ RemoveJunkXmlCharacters(otherstructuredesc) + "</HM_ENDDESCRIPTION>";
											if(DROutbuil["INSURING_VALUE"].ToString()!="")
											{
												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\"> $"+insurevalue+"</HM_ENDLIMIT>";
											
											}
											DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDed)+"</HM_ENDDEDUCTIBLE>";
										}
										
									}
									else if(DROutbuil["SATELLITE_EQUIPMENT"].ToString() != "10963" && CovCode.Trim().ToUpper() =="DP900" && DROutbuil["COVERAGE_BASIS"].ToString() == "Excluded")
									{
										otherstructuredesc= DROutbuil["PREMISES_DESCRIPTION"].ToString();
										DecPageEndCtr++;
										DecPageDwellEndmtElement = AcordPDFXML.CreateElement("ENDORSEMENTSINFO");
										DecPageREDWEndmts.AppendChild(DecPageDwellEndmtElement);
										DecPageDwellEndmtElement.SetAttribute(fieldType,fieldTypeMultiple);
										DecPageDwellEndmtElement.SetAttribute(id,(DecPageEndCtr).ToString() );

										if(gStrCalledFrom.Equals(CalledFromPolicy))
										{
											DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<POLICYNO " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNO>";
											DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<POLICYPERIODFROM " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</POLICYPERIODFROM>";
											DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<POLICYPERIODTO " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</POLICYPERIODTO>";
											//ADDED FOR ITRACK # 3619
											DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<ALL_POLICY_TYPE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyType + "</ALL_POLICY_TYPE>";
											//
											DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<REASON " + fieldType + "=\"" + fieldTypeText + "\">" + Reason  + "</REASON>";
										}
										DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<PRIMARYCONTACTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName1 + "</PRIMARYCONTACTNAME>";
										if(DecPageEndCtr >= 12)
											DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<PAGE " + fieldType + "=\"" + fieldTypeText + "\">" + (extraCovPage+pagecounter+1).ToString() + " of " + (totcovPage + totdwellpage).ToString() + "</PAGE>";
										if(otherstructuredesc !="")
										{
											DecPageDwellEndmtElement.InnerXml = DecPageDwellEndmtElement.InnerXml +  "<HM_ENDDESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">"  + "   - "+ RemoveJunkXmlCharacters(otherstructuredesc) + "</HM_ENDDESCRIPTION>";
											DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</HM_ENDLIMIT>";
											DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</HM_ENDDEDUCTIBLE>";
										}
										
									}
									/*{
										DecPageEndCtr++;
										DecPageDwellEndmtElement = AcordPDFXML.CreateElement("ENDORSEMENTSINFO");
										DecPageREDWEndmts.AppendChild(DecPageDwellEndmtElement);
										DecPageDwellEndmtElement.SetAttribute(fieldType,fieldTypeNormal);
										DecPageDwellEndmtElement.SetAttribute(id,(DecPageEndCtr).ToString() );
										//	DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("             " + DROutbuil["PREM_LOC"].ToString())+"</HM_ENDDESCRIPTION>";
										//									if(gStrCalledFrom.Equals(CalledFromPolicy))
										//									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString()+ " " + DROutbuil["PREMISES_DESCRIPTION"].ToString())+"</HM_ENDDESCRIPTION>";
										//   DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_PREMISE_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DROutbuil["PREMISES_DESCRIPTION"].ToString())+"</HM_PREMISE_DESCRIPTION>";
										//									}
									}
									*/
									//								 	
									//strIncLmtCov += "</HM_ENDDESCRIPTION>";
									//DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + strIncLmtCov;
								}
								
							}

							#region SWITCH CASE FOR DEC PAGE
							//							switch(CoverageDetails["COV_CODE"].ToString())
							//							{
							//								case "BOSTR":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";
							//							
							//										if(htpremium.Contains("BOSTR"))
							//										{
							//											gstrGetPremium	=	htpremium["BOSTR"].ToString();
							//											gintGetindex	=	gstrGetPremium.IndexOf(".");
							//											if(gintGetindex==	-1)
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOSTR"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//											else
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOSTR"].ToString())+"</HM_ENDPREMIUM>";
							//
							//										}
							//									}
							//									break;
							//								case "BIAA":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+APDIDed+"</HM_ENDDEDUCTIBLE>";
							//							
							//										if(htpremium.Contains("BLDG_ALT"))
							//										{
							//											gstrGetPremium	=	htpremium["BLDG_ALT"].ToString();
							//											gintGetindex	=	gstrGetPremium.IndexOf(".");
							//											if(gintGetindex==	-1)
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BLDG_ALT"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//											else
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BLDG_ALT"].ToString())+"</HM_ENDPREMIUM>";
							//										}
							//									}
							//									break;
							//								case "TSPL":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+APDIDed+"</HM_ENDDEDUCTIBLE>";
							//							
							//										if(htpremium.Contains("TREES"))
							//										{
							//											gstrGetPremium	=	htpremium["TREES"].ToString();
							//											gintGetindex	=	gstrGetPremium.IndexOf(".");
							//											if(gintGetindex==	-1)
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["TREES"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//											else
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["TREES"].ToString())+"</HM_ENDPREMIUM>";
							//										
							//										}
							//									}
							//									break;
							//								case "RTE":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";
							//					
							//										if(htpremium.Contains("RADIO"))
							//										{
							//											gstrGetPremium	=	htpremium["RADIO"].ToString();
							//											gintGetindex	=	gstrGetPremium.IndexOf(".");
							//											if(gintGetindex==	-1)
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RADIO"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//											else
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RADIO"].ToString())+"</HM_ENDPREMIUM>";
							//										}
							//									}
							//									break;
							//								case "SD":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"].ToString()!="" )
							//										{											
							//											DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+ (double.Parse(RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())) + (500.00))+"</HM_ENDLIMIT>";
							//										}
							//										else
							//											DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">500.00</HM_ENDLIMIT>";
							//
							//										
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";
							//					
							//										if(htpremium.Contains("SAT_DISH"))
							//										{
							//											gstrGetPremium	=	htpremium["SAT_DISH"].ToString();
							//											gintGetindex	=	gstrGetPremium.IndexOf(".");
							//											if(gintGetindex==	-1)
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["SAT_DISH"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//											else
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["SAT_DISH"].ToString())+"</HM_ENDPREMIUM>";
							//										}
							//									}
							//									break;
							//								case "ACS":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+APDIDed+"</HM_ENDDEDUCTIBLE>";
							//					
							//										if(htpremium.Contains("AWNG"))
							//										{
							//											gstrGetPremium	=	htpremium["AWNG"].ToString();
							//											gintGetindex	=	gstrGetPremium.IndexOf(".");
							//											if(gintGetindex==	-1)
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["AWNG"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//											else
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["AWNG"].ToString())+"</HM_ENDPREMIUM>";
							//										}
							//									}
							//									break;
							//								case "EDP469":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">5%</HM_ENDDEDUCTIBLE>";
							//					
							//										if(htpremium.Contains("ERTHQKE"))
							//										{
							//											gstrGetPremium	=	htpremium["ERTHQKE"].ToString();
							//											gintGetindex	=	gstrGetPremium.IndexOf(".");
							//											if(gintGetindex==	-1)
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ERTHQKE"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//											else
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ERTHQKE"].ToString())+"</HM_ENDPREMIUM>";
							//										}
							//									}
							//
							//									#region Dec Page Element
							//									if(CoverageDetails["ENDORS_PRINT"].ToString() =="Y" )
							//									{
							//										XmlElement DecPageRedwtEndoDP469;
							//										DecPageRedwtEndoDP469 = AcordPDFXML.CreateElement("REDWENDORSEMENTDP469");
							//										DecPageDwellEndmtElement.AppendChild(DecPageRedwtEndoDP469);
							//										DecPageRedwtEndoDP469.SetAttribute(fieldType,fieldTypeMultiple);
							//										if(CoverageDetails["ATTACH_FILE"] != null && CoverageDetails["ATTACH_FILE"].ToString() != "")
							//										{
							//											DecPageRedwtEndoDP469.SetAttribute(PrimPDF,CoverageDetails["ATTACH_FILE"].ToString());
							//											DecPageRedwtEndoDP469.SetAttribute(PrimPDFBlocks,"1");
							//										}
							//										else
							//										{
							//											DecPageRedwtEndoDP469.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEDP469"));
							//											DecPageRedwtEndoDP469.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP469"));
							//										}
							//										DecPageRedwtEndoDP469.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEDP469EXTN"));
							//										DecPageRedwtEndoDP469.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP469EXTN"));
							//	
							//										XmlElement EndoElementDP469;
							//										EndoElementDP469 = AcordPDFXML.CreateElement("ENDOELEMENTDP1143INFO");
							//										DecPageRedwtEndoDP469.AppendChild(EndoElementDP469);
							//										EndoElementDP469.SetAttribute(fieldType,fieldTypeNormal);
							//										EndoElementDP469.SetAttribute(id,DecPageEndCtr.ToString());
							//									}
							//									#endregion
							//									break;
							//								case "BR1143":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+APDIDed+"</HM_ENDDEDUCTIBLE>";
							//									
							//					
							//										if(htpremium.Contains("BR1143"))
							//										{
							//											gstrGetPremium	=	htpremium["BR1143"].ToString();
							//											gintGetindex	=	gstrGetPremium.IndexOf(".");
							//											if(gintGetindex==	-1)
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BR1143"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//											else
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BR1143"].ToString())+"</HM_ENDPREMIUM>";
							//										}
							//									}
							//
							//									#region Dec Page Element
							//									if(CoverageDetails["ENDORS_PRINT"].ToString() =="Y" )
							//									{
							//										XmlElement DecPageRedwtEndoDP1143;
							//										DecPageRedwtEndoDP1143 = AcordPDFXML.CreateElement("REDWENDORSEMENTDP1143");
							//										DecPageDwellEndmtElement.AppendChild(DecPageRedwtEndoDP1143);
							//										DecPageRedwtEndoDP1143.SetAttribute(fieldType,fieldTypeMultiple);
							//										if(CoverageDetails["ATTACH_FILE"] != null && CoverageDetails["ATTACH_FILE"].ToString() != "")
							//										{
							//											DecPageRedwtEndoDP1143.SetAttribute(PrimPDF,CoverageDetails["ATTACH_FILE"].ToString());
							//											DecPageRedwtEndoDP1143.SetAttribute(PrimPDFBlocks,"1");
							//										}
							//										else
							//										{
							//											DecPageRedwtEndoDP1143.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEDP1143"));
							//											DecPageRedwtEndoDP1143.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP1143"));
							//										}
							//										DecPageRedwtEndoDP1143.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEDP1143EXTN"));
							//										DecPageRedwtEndoDP1143.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP1143EXTN"));
							//	
							//										XmlElement EndoElementDP1143;
							//										EndoElementDP1143 = AcordPDFXML.CreateElement("ENDOELEMENTDP1143INFO");
							//										DecPageRedwtEndoDP1143.AppendChild(EndoElementDP1143);
							//										EndoElementDP1143.SetAttribute(fieldType,fieldTypeNormal);
							//										EndoElementDP1143.SetAttribute(id,DecPageEndCtr.ToString());
							//									}
							//									#endregion
							//									break;
							//								case "IF184":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">500.00</HM_ENDDEDUCTIBLE>";
							//					
							//										if(htpremium.Contains("INST_FLTR_BLDG"))
							//										{
							//											gstrGetPremium	=	htpremium["INST_FLTR_BLDG"].ToString();
							//											gintGetindex	=	gstrGetPremium.IndexOf(".");
							//											if(gintGetindex==	-1)
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INST_FLTR_BLDG"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//											else
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INST_FLTR_BLDG"].ToString())+"</HM_ENDPREMIUM>";
							//										}
							//									}
							//									break;
							//								case "CS":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+APDIDed+"</HM_ENDDEDUCTIBLE>";
							//					
							//										if(htpremium.Contains("CNTNTS_STRG"))
							//										{
							//											gstrGetPremium	=	htpremium["CNTNTS_STRG"].ToString();
							//											gintGetindex	=	gstrGetPremium.IndexOf(".");
							//											if(gintGetindex==	-1)
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CNTNTS_STRG"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//											else
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CNTNTS_STRG"].ToString())+"</HM_ENDPREMIUM>";
							//										}
							//									}
							//									break;
							//								case "IFNSE":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">500.00</HM_ENDDEDUCTIBLE>";
							//					
							//										if(htpremium.Contains("INST_FLTR_NON_STR"))
							//										{
							//											gstrGetPremium	=	htpremium["INST_FLTR_NON_STR"].ToString();
							//											gintGetindex	=	gstrGetPremium.IndexOf(".");
							//											if(gintGetindex==	-1)
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INST_FLTR_NON_STR"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//											else
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INST_FLTR_NON_STR"].ToString())+"</HM_ENDPREMIUM>";
							//										}
							//									}
							//									break;
							//								case "MSC480":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">2%</HM_ENDDEDUCTIBLE>";
							//					
							//										if(htpremium.Contains("MNE_SBS"))
							//										{
							//											gstrGetPremium	=	htpremium["MNE_SBS"].ToString();
							//											gintGetindex	=	gstrGetPremium.IndexOf(".");
							//											if(gintGetindex==	-1)
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["MNE_SBS"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//											else
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["MNE_SBS"].ToString())+"</HM_ENDPREMIUM>";
							//
							//										}
							//									}
							//
							//									#region Dec Page Element
							//									if(CoverageDetails["ENDORS_PRINT"].ToString() =="Y" )
							//									{
							//										XmlElement DecPageRedwtEndoDP480;
							//										DecPageRedwtEndoDP480 = AcordPDFXML.CreateElement("REDWENDORSEMENTDP480");
							//										DecPageDwellEndmtElement.AppendChild(DecPageRedwtEndoDP480);
							//										DecPageRedwtEndoDP480.SetAttribute(fieldType,fieldTypeMultiple);
							//										if(CoverageDetails["ATTACH_FILE"] != null && CoverageDetails["ATTACH_FILE"].ToString() != "")
							//										{
							//											DecPageRedwtEndoDP480.SetAttribute(PrimPDF,CoverageDetails["ATTACH_FILE"].ToString());
							//											DecPageRedwtEndoDP480.SetAttribute(PrimPDFBlocks,"1");
							//										}
							//										else
							//										{
							//											DecPageRedwtEndoDP480.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEDP480"));
							//											DecPageRedwtEndoDP480.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP480"));
							//										}
							//										DecPageRedwtEndoDP480.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEDP480EXTN"));
							//										DecPageRedwtEndoDP480.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP480EXTN"));
							//	
							//										XmlElement EndoElementDP480;
							//										EndoElementDP480 = AcordPDFXML.CreateElement("ENDOELEMENTDP480INFO");
							//										DecPageRedwtEndoDP480.AppendChild(EndoElementDP480);
							//										EndoElementDP480.SetAttribute(fieldType,fieldTypeNormal);
							//										EndoElementDP480.SetAttribute(id,DecPageEndCtr.ToString());
							//									}
							//									#endregion
							//									break;
							//								case "IOO":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";
							//					
							//										if(htpremium.Contains("INCI_OFCE"))
							//										{
							//											gstrGetPremium	=	htpremium["INCI_OFCE"].ToString();
							//											gintGetindex	=	gstrGetPremium.IndexOf(".");
							//											if(gintGetindex==	-1)
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCI_OFCE"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//											else
							//												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCI_OFCE"].ToString())+"</HM_ENDPREMIUM>";
							//										}
							//									}
							//									break;
							//								
							//								case "DP100":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//									}
							//									
							//									#region Dec Page Element
							////									XmlElement DecPageRedwtEndoDP100;
							////									DecPageRedwtEndoDP100 = AcordPDFXML.CreateElement("REDWENDORSEMENTDP100");
							////									DecPageDwellEndmtElement.AppendChild(DecPageRedwtEndoDP100);
							////									DecPageRedwtEndoDP100.SetAttribute(fieldType,fieldTypeMultiple);
							////									DecPageRedwtEndoDP100.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEDP100"));
							////									DecPageRedwtEndoDP100.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP100"));
							////									DecPageRedwtEndoDP100.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEDP100EXTN"));
							////									DecPageRedwtEndoDP100.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP100EXTN"));
							////
							////									XmlElement EndoElementDP100;
							////									EndoElementDP100 = AcordPDFXML.CreateElement("EndoElementDP100INFO");
							////									DecPageRedwtEndoDP100.AppendChild(EndoElementDP100);
							////									EndoElementDP100.SetAttribute(fieldType,fieldTypeNormal);
							////									EndoElementDP100.SetAttribute(id,DecPageEndCtr.ToString());
							//									#endregion
							//
							//									break;
							//								case "DP113":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//									}
							//
							//									#region Dec Page Element
							//									if(CoverageDetails["ENDORS_PRINT"].ToString() =="Y" )
							//									{
							//										XmlElement DecPageRedwtEndoDP113;
							//										DecPageRedwtEndoDP113 = AcordPDFXML.CreateElement("REDWENDORSEMENTDP113");
							//										DecPageDwellEndmtElement.AppendChild(DecPageRedwtEndoDP113);
							//										DecPageRedwtEndoDP113.SetAttribute(fieldType,fieldTypeMultiple);
							//										if(CoverageDetails["ATTACH_FILE"] != null && CoverageDetails["ATTACH_FILE"].ToString() != "")
							//										{
							//											DecPageRedwtEndoDP113.SetAttribute(PrimPDF,CoverageDetails["ATTACH_FILE"].ToString());
							//											DecPageRedwtEndoDP113.SetAttribute(PrimPDFBlocks,"1");
							//										}
							//										else
							//										{
							//											DecPageRedwtEndoDP113.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEDP113"));
							//											DecPageRedwtEndoDP113.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP113"));
							//										}
							//										DecPageRedwtEndoDP113.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEDP113EXTN"));
							//										DecPageRedwtEndoDP113.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP113EXTN"));
							//	
							//										XmlElement EndoElementDP113;
							//										EndoElementDP113 = AcordPDFXML.CreateElement("ENDOELEMENTDP113INFO");
							//										DecPageRedwtEndoDP113.AppendChild(EndoElementDP113);
							//										EndoElementDP113.SetAttribute(fieldType,fieldTypeNormal);
							//										EndoElementDP113.SetAttribute(id,DecPageEndCtr.ToString());
							//									}
							//									#endregion
							//
							//
							//									break;
							//								case "DP417":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//									}
							//										
							//									#region Dec Page Element
							//									if(CoverageDetails["ENDORS_PRINT"].ToString() =="Y" )
							//									{
							//										XmlElement DecPageRedwEndoDP417;
							//										DecPageRedwEndoDP417 = AcordPDFXML.CreateElement("REDWENDORSEMENTDP417");
							//										DecPageDwellEndmtElement.AppendChild(DecPageRedwEndoDP417);
							//										DecPageRedwEndoDP417.SetAttribute(fieldType,fieldTypeMultiple);
							//										DecPageRedwEndoDP417.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEDP417"));
							//										DecPageRedwEndoDP417.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP417"));
							//										DecPageRedwEndoDP417.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEDP417EXTN"));
							//										DecPageRedwEndoDP417.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP417EXTN"));
							//		
							//										XmlElement EndoElementDP417;
							//										EndoElementDP417 = AcordPDFXML.CreateElement("ENDOELEMENTDP417INFO");
							//										DecPageRedwEndoDP417.AppendChild(EndoElementDP417);
							//										EndoElementDP417.SetAttribute(fieldType,fieldTypeNormal);
							//										EndoElementDP417.SetAttribute(id,DecPageEndCtr.ToString() );
							//									}
							//									#endregion
							//
							//									break;
							//								case "DP422":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//									}
							//									#region Dec Page Element
							//									if(CoverageDetails["ENDORS_PRINT"].ToString() =="Y" )
							//									{
							//										XmlElement DecPageRedwtEndoDP422;
							//										DecPageRedwtEndoDP422 = AcordPDFXML.CreateElement("REDWENDORSEMENTDP422");
							//										DecPageDwellEndmtElement.AppendChild(DecPageRedwtEndoDP422);
							//										DecPageRedwtEndoDP422.SetAttribute(fieldType,fieldTypeMultiple);
							//										if(CoverageDetails["ATTACH_FILE"] != null && CoverageDetails["ATTACH_FILE"].ToString() != "")
							//										{
							//											DecPageRedwtEndoDP422.SetAttribute(PrimPDF,CoverageDetails["ATTACH_FILE"].ToString());
							//											DecPageRedwtEndoDP422.SetAttribute(PrimPDFBlocks,"1");
							//										}
							//										else
							//										{
							//											DecPageRedwtEndoDP422.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEDP422"));
							//											DecPageRedwtEndoDP422.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP422"));
							//										}
							//										DecPageRedwtEndoDP422.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEDP422EXTN"));
							//										DecPageRedwtEndoDP422.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP422EXTN"));
							//	
							//										XmlElement EndoElementDP422;
							//										EndoElementDP422 = AcordPDFXML.CreateElement("ENDOELEMENTDP422INFO");
							//										DecPageRedwtEndoDP422.AppendChild(EndoElementDP422);
							//										EndoElementDP422.SetAttribute(fieldType,fieldTypeNormal);
							//										EndoElementDP422.SetAttribute(id,DecPageEndCtr.ToString());
							//									}
							//									#endregion
							//
							//									break;
							//								case "DP289":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//									}
							//
							//									#region Dec Page Element
							//									if(CoverageDetails["ENDORS_PRINT"].ToString() =="Y"  )
							//									{
							//										XmlElement DecPageRedwtEndoDP289;
							//										DecPageRedwtEndoDP289 = AcordPDFXML.CreateElement("REDWENDORSEMENTDP289");
							//										DecPageDwellEndmtElement.AppendChild(DecPageRedwtEndoDP289);
							//										DecPageRedwtEndoDP289.SetAttribute(fieldType,fieldTypeMultiple);
							//										if(CoverageDetails["ATTACH_FILE"] != null && CoverageDetails["ATTACH_FILE"].ToString() != "")
							//										{
							//											DecPageRedwtEndoDP289.SetAttribute(PrimPDF,CoverageDetails["ATTACH_FILE"].ToString());
							//											DecPageRedwtEndoDP289.SetAttribute(PrimPDFBlocks,"1");
							//										}
							//										else
							//										{
							//											DecPageRedwtEndoDP289.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEDP289"));
							//											DecPageRedwtEndoDP289.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP289"));
							//										}
							//										DecPageRedwtEndoDP289.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEDP289EXTN"));
							//										DecPageRedwtEndoDP289.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP289EXTN"));
							//	
							//										XmlElement EndoElementDP289;
							//										EndoElementDP289 = AcordPDFXML.CreateElement("ENDOELEMENTDP289INFO");
							//										DecPageRedwtEndoDP289.AppendChild(EndoElementDP289);
							//										EndoElementDP289.SetAttribute(fieldType,fieldTypeNormal);
							//										EndoElementDP289.SetAttribute(id,DecPageEndCtr.ToString());
							//									}
							//									#endregion
							//									break;
							//								case "DP382":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//									}
							//									#region Dec Page Element
							//									if(CoverageDetails["ENDORS_PRINT"].ToString() =="Y"  )
							//									{
							//										XmlElement DecPageRedwtEndoDP382;
							//										DecPageRedwtEndoDP382 = AcordPDFXML.CreateElement("REDWENDORSEMENTDP422");
							//										DecPageDwellEndmtElement.AppendChild(DecPageRedwtEndoDP382);
							//										DecPageRedwtEndoDP382.SetAttribute(fieldType,fieldTypeMultiple);
							//										if(CoverageDetails["ATTACH_FILE"] != null && CoverageDetails["ATTACH_FILE"].ToString() != "")
							//										{
							//											DecPageRedwtEndoDP382.SetAttribute(PrimPDF,CoverageDetails["ATTACH_FILE"].ToString());
							//											DecPageRedwtEndoDP382.SetAttribute(PrimPDFBlocks,"1");
							//										}
							//										else
							//										{
							//											DecPageRedwtEndoDP382.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEDP382"));
							//											DecPageRedwtEndoDP382.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP382"));
							//										}
							//										DecPageRedwtEndoDP382.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEDP382EXTN"));
							//										DecPageRedwtEndoDP382.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP382EXTN"));
							//	
							//										XmlElement EndoElementDP382;
							//										EndoElementDP382 = AcordPDFXML.CreateElement("ENDOELEMENTDP382INFO");
							//										DecPageRedwtEndoDP382.AppendChild(EndoElementDP382);
							//										EndoElementDP382.SetAttribute(fieldType,fieldTypeNormal);
							//										EndoElementDP382.SetAttribute(id,DecPageEndCtr.ToString());
							//									}
							//									#endregion
							//
							//
							//									break;
							//								case "LP417":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//									}
							//									break;
							//								case "PIOSS":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//									}
							//									break;
							//								case "DP02":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//									}
							//									break;
							//								case "DP03":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//									}
							//									break;
							//								case "DP216":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//									}
							//									break;
							//								case "DP320":
							//									if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//									{
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//										DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">500.00</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//									}
							//
							//									#region Dec Page Element
							//									if(CoverageDetails["ENDORS_PRINT"].ToString() =="Y" )
							//									{
							//										XmlElement DecPageRedwtEndoDP320;
							//										DecPageRedwtEndoDP320 = AcordPDFXML.CreateElement("REDWENDORSEMENTDP320");
							//										DecPageDwellEndmtElement.AppendChild(DecPageRedwtEndoDP320);
							//										DecPageRedwtEndoDP320.SetAttribute(fieldType,fieldTypeMultiple);
							//										if(CoverageDetails["ATTACH_FILE"] != null && CoverageDetails["ATTACH_FILE"].ToString() != "")
							//										{
							//											DecPageRedwtEndoDP320.SetAttribute(PrimPDF,CoverageDetails["ATTACH_FILE"].ToString());
							//											DecPageRedwtEndoDP320.SetAttribute(PrimPDFBlocks,"1");
							//										}
							//										else
							//										{
							//											DecPageRedwtEndoDP320.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEDP320"));
							//											DecPageRedwtEndoDP320.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP320"));
							//										}
							//										DecPageRedwtEndoDP320.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEDP320EXTN"));
							//										DecPageRedwtEndoDP320.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP320EXTN"));
							//	
							//										XmlElement EndoElementDP320;
							//										EndoElementDP320 = AcordPDFXML.CreateElement("ENDOELEMENTDP320INFO");
							//										DecPageRedwtEndoDP320.AppendChild(EndoElementDP320);
							//										EndoElementDP320.SetAttribute(fieldType,fieldTypeNormal);
							//										EndoElementDP320.SetAttribute(id,DecPageEndCtr.ToString());
							//									}
							//									#endregion
							//
							//
							//									break;
							//								default:
							//									DecPageDwellEndmtElement.InnerXml += "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//									DecPageDwellEndmtElement.InnerXml += "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";									
							//									DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//									DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
							//									DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//									
							//									
							//									if (string.CompareOrdinal(CoverageDetails["COV_CODE"].ToString(),"DP121")==0)
							//									{
							//										#region Dec Page Element
							//										if(CoverageDetails["ENDORS_PRINT"].ToString() =="Y" )
							//										{
							//											XmlElement DecPageRedwtEndoDP121;
							//											DecPageRedwtEndoDP121 = AcordPDFXML.CreateElement("REDWENDORSEMENTDP121");
							//											DecPageDwellEndmtElement.AppendChild(DecPageRedwtEndoDP121);
							//											DecPageRedwtEndoDP121.SetAttribute(fieldType,fieldTypeMultiple);
							//											if(CoverageDetails["ATTACH_FILE"] != null && CoverageDetails["ATTACH_FILE"].ToString() != "")
							//											{
							//												DecPageRedwtEndoDP121.SetAttribute(PrimPDF,CoverageDetails["ATTACH_FILE"].ToString());
							//												DecPageRedwtEndoDP121.SetAttribute(PrimPDFBlocks,"1");
							//											}
							//											else
							//											{
							//												DecPageRedwtEndoDP121.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEDP121"));
							//												DecPageRedwtEndoDP121.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP121"));
							//											}
							//											DecPageRedwtEndoDP121.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEDP121EXTN"));
							//											DecPageRedwtEndoDP121.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP121EXTN"));
							//	
							//											XmlElement EndoElementDP121;
							//											EndoElementDP121 = AcordPDFXML.CreateElement("ENDOELEMENTDP121INFO");
							//											DecPageRedwtEndoDP121.AppendChild(EndoElementDP121);
							//											EndoElementDP121.SetAttribute(fieldType,fieldTypeNormal);
							//											EndoElementDP121.SetAttribute(id,DecPageEndCtr.ToString());
							//										}
							//										#endregion
							//
							//									}
							//
							//									if (string.CompareOrdinal(CoverageDetails["COV_CODE"].ToString(),"DP321")==0)
							//									{
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";										
							//
							//										#region Dec Page Element
							//										if(CoverageDetails["ENDORS_PRINT"].ToString() =="Y" )
							//										{
							//											XmlElement DecPageRedwtEndoDP321;
							//											DecPageRedwtEndoDP321 = AcordPDFXML.CreateElement("REDWENDORSEMENTDP321");
							//											DecPageDwellEndmtElement.AppendChild(DecPageRedwtEndoDP321);
							//											DecPageRedwtEndoDP321.SetAttribute(fieldType,fieldTypeMultiple);
							//											if(CoverageDetails["ATTACH_FILE"] != null && CoverageDetails["ATTACH_FILE"].ToString() != "")
							//											{
							//												DecPageRedwtEndoDP321.SetAttribute(PrimPDF,CoverageDetails["ATTACH_FILE"].ToString());
							//												DecPageRedwtEndoDP321.SetAttribute(PrimPDFBlocks,"1");
							//											}
							//											else
							//											{
							//												DecPageRedwtEndoDP321.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEDP321"));
							//												DecPageRedwtEndoDP321.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP321"));
							//											}
							//											DecPageRedwtEndoDP321.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEDP321EXTN"));
							//											DecPageRedwtEndoDP321.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP321EXTN"));
							//		
							//											XmlElement EndoElementDP321;
							//											EndoElementDP321 = AcordPDFXML.CreateElement("ENDOELEMENTDP321INFO");
							//											DecPageRedwtEndoDP321.AppendChild(EndoElementDP321);
							//											EndoElementDP321.SetAttribute(fieldType,fieldTypeNormal);
							//											EndoElementDP321.SetAttribute(id,DecPageEndCtr.ToString());
							//										}
							//										#endregion
							//
							//									}
							//
							//									if (string.CompareOrdinal(CoverageDetails["COV_CODE"].ToString(),"DP392")==0)
							//									{
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">10000.00</HM_ENDLIMIT>";
							//										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//
							//										#region Dec Page Element
							//										if(CoverageDetails["ENDORS_PRINT"].ToString() =="Y" )
							//										{
							//											XmlElement DecPageRedwtEndoDP392;
							//											DecPageRedwtEndoDP392 = AcordPDFXML.CreateElement("REDWENDORSEMENTDP392");
							//											DecPageDwellEndmtElement.AppendChild(DecPageRedwtEndoDP392);
							//											DecPageRedwtEndoDP392.SetAttribute(fieldType,fieldTypeMultiple);
							//											if(CoverageDetails["ATTACH_FILE"] != null && CoverageDetails["ATTACH_FILE"].ToString() != "")
							//											{
							//												DecPageRedwtEndoDP392.SetAttribute(PrimPDF,CoverageDetails["ATTACH_FILE"].ToString());
							//												DecPageRedwtEndoDP392.SetAttribute(PrimPDFBlocks,"1");
							//											}
							//											else
							//											{
							//												DecPageRedwtEndoDP392.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEDP392"));
							//												DecPageRedwtEndoDP392.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP392"));
							//											}
							//											DecPageRedwtEndoDP392.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEDP392EXTN"));
							//											DecPageRedwtEndoDP392.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP392EXTN"));
							//		
							//											XmlElement EndoElementDP392;
							//											EndoElementDP392 = AcordPDFXML.CreateElement("ENDOELEMENTDP392INFO");
							//											DecPageRedwtEndoDP392.AppendChild(EndoElementDP392);
							//											EndoElementDP392.SetAttribute(fieldType,fieldTypeNormal);
							//											EndoElementDP392.SetAttribute(id,DecPageEndCtr.ToString());
							//										}
							//										#endregion
							//
							//									}
							//
							//									if (string.CompareOrdinal(CoverageDetails["COV_CODE"].ToString(),"DP900")==0)
							//									{
							//										#region Dec Page Element
							//										if(CoverageDetails["ENDORS_PRINT"].ToString() =="Y"  )
							//										{
							//											XmlElement DecPageRedwtEndoDP900;
							//											DecPageRedwtEndoDP900 = AcordPDFXML.CreateElement("REDWENDORSEMENTDP900");
							//											DecPageDwellEndmtElement.AppendChild(DecPageRedwtEndoDP900);
							//											DecPageRedwtEndoDP900.SetAttribute(fieldType,fieldTypeMultiple);
							//											if(CoverageDetails["ATTACH_FILE"] != null && CoverageDetails["ATTACH_FILE"].ToString() != "")
							//											{
							//												DecPageRedwtEndoDP900.SetAttribute(PrimPDF,CoverageDetails["ATTACH_FILE"].ToString());
							//												DecPageRedwtEndoDP900.SetAttribute(PrimPDFBlocks,"1");
							//											}
							//											else
							//											{
							//												DecPageRedwtEndoDP900.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEDP900"));
							//												DecPageRedwtEndoDP900.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP900"));
							//											}
							//											DecPageRedwtEndoDP900.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEDP900EXTN"));
							//											DecPageRedwtEndoDP900.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDP900EXTN"));
							//		
							//											XmlElement EndoElementDP900;
							//											EndoElementDP900 = AcordPDFXML.CreateElement("ENDOELEMENTDP900INFO");
							//											DecPageRedwtEndoDP900.AppendChild(EndoElementDP900);
							//											EndoElementDP900.SetAttribute(fieldType,fieldTypeNormal);
							//											EndoElementDP900.SetAttribute(id,DecPageEndCtr.ToString());
							//										}
							//										#endregion
							//
							//									}
							//
							//									break;
							//
							//							}
							#endregion

							//if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" " || CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//{
							DecPageEndCtr++;
							//}

							#region TOTAL PREMIUM

							if (gStrPdfFor==PDFForDecPage)
							{
								string strAddPrm="";
								string sumTotalPol="";
								if(sumTtl !="0" && sumTtl !="")
									sumTtl=(Convert.ToDouble(sumTtl)).ToString("###,###");
								if(sumTtlPol !=0) 
								{
									sumTotalPol=sumTtlPol.ToString("###,###");
									sumTotalPol = "$"+sumTotalPol  + ".00" ;
								}
								if(gStrCopyTo == "AGENCY")
								{
									if(ProPExpFee.ToString()!="" && ProPExpFee.ToString()!=null && ProPExpFee.ToString()!="0" && ProPExpFee.ToString()!="0.00")
									{
										DecPageDwellEndmtElement.InnerXml += DecPageDwellEndmtElement.InnerXml + "<AGENCYACCOUNTINFORMATION " + fieldType +"=\""+ fieldTypeText +"\">"+ "**Account Information - "+"$"+RemoveJunkXmlCharacters(DollarFormat(double.Parse(ProPExpFee.ToString()))) + "**" +"</AGENCYACCOUNTINFORMATION>";					
									}
								}
								DecPageDwellEndmtElement.InnerXml += "<TOTAL_LOCATION_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+ ("$"+sumTtl  + ".00" )+"</TOTAL_LOCATION_PREMIUM>";
								DecPageDwellEndmtElement.InnerXml += "<TOTAL_ANNUAL_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+ (sumTotalPol)+"</TOTAL_ANNUAL_PREMIUM>";
							
								// if proccess not committed
								if(gStrtemp != "final")
								{

									gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
									gobjWrapper.AddParameter("@POLID",gStrPolicyId);
									gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
									gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
									DataSet DSAddPremium = gobjWrapper.ExecuteDataSet("Proc_GetAdditionalPremiumPDF");
									gobjWrapper.ClearParameteres();

									//DataSet DSAddPremium = gobjSqlHelper.ExecuteDataSet("Proc_GetAdditionalPremiumPDF " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'"  + gStrCalledFrom + "'");
									
									XmlDocument RateXmlDocument = new XmlDocument();
									if(DSAddPremium != null)
									{
										if(DSAddPremium.Tables.Count>0 )
											if(DSAddPremium.Tables[0].Rows.Count>0)
											{
												RateXmlDocument.LoadXml(DSAddPremium.Tables[0].Rows[0][0].ToString());	
											}
										if(RateXmlDocument!=null )
										{
											XmlNode xNode=RateXmlDocument.SelectSingleNode("//PREMIUM/NETPREMIUM"); 
											if(xNode!=null)
											{
												strAddPrm=xNode.InnerXml;
												if(strAddPrm == ".00")
												{
													strAddPrm="0.00";
												}
											}
										}
									}//END OF NULL CHECK FOR DATASET
								}
									// if proccess committed
								else
								{

									gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
									gobjWrapper.AddParameter("@POLID",gStrPolicyId);
									gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
									DataSet DSAddPremium1 = gobjWrapper.ExecuteDataSet("PROC_GetpdfAdjusted_Premium");
									gobjWrapper.ClearParameteres();

									//DataSet DSAddPremium1 = gobjSqlHelper.ExecuteDataSet("PROC_GetpdfAdjusted_Premium " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "");

									strAddPrm = DSAddPremium1.Tables[0].Rows[0]["WRITTEN_PREMIUM"].ToString();
								}
								if(gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS.ToString() || gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS.ToString() 
									|| gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REINSTATEMENT_PROCESS.ToString() || gStrProcessID == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REINSTATEMENT_PROCESS.ToString())
								{
									if(strAddPrm == "0")
									{
										strAddPrm = strAddPrm + ".00";
									}
										DecPageDwellEndmtElement.InnerXml += "<ADDITIONAL_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+ ("$"+strAddPrm) +"</ADDITIONAL_PREMIUM>";
								}
								else
								{
										DecPageDwellEndmtElement.InnerXml += "<ADDITIONAL_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+ "" +"</ADDITIONAL_PREMIUM>";
								}		
							}//END OF CHECK FOR CALLING PAGE 
							#endregion
						}//END OF COVERAGE CODE CHECK
						
					}//END OF CALLING PAGE CHECK
					#endregion
					
					#region Supplement Page
					if (gStrPdfFor==PDFForAcord )
					{
						if (CovCode!="DWELL" && CovCode!="OSTR" && CovCode!="LPP" && CovCode!="RV" && CovCode!="CSL" && CovCode!="MEDPM" && CovCode!="APDI")
						{

							if(CoverageDetails["LIMIT_1"]!=null)
							{
								strLimit=CoverageDetails["LIMIT_1"].ToString().Trim() ;
								if(!(strLimit.Equals("0") || strLimit == "" || strLimit.Equals("0.00")))
								{
									if(strLimit.EndsWith("/"))
									{
										strLimit=strLimit.Substring(0,strLimit.Length-1 ); 
									}
								}
								else
									strLimit="Included";
							}
							if(CovCode == "SD")
								strLimit=CoverageDetails["LIMIT_2"].ToString().Trim() ;

							if(CoverageDetails["DEDUCTIBLE"]!=null)
							{
								strDed=CoverageDetails["DEDUCTIBLE"].ToString().Trim() ;
								if(strDed.Equals("0.00"))
								{
									strDed="";
								}
							}

							XmlElement SupplementDwellEndmtElement;
							SupplementDwellEndmtElement = AcordPDFXML.CreateElement("ENDORSEMENTSINFO");
							SupplementREDWEndmts.AppendChild(SupplementDwellEndmtElement);
							SupplementDwellEndmtElement.SetAttribute(fieldType,fieldTypeNormal);
							SupplementDwellEndmtElement.SetAttribute(id,endCtr.ToString());

							/* Commented by Asfa (30-Jan-2008) -iTrack issue #3495 Part 5
							SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							*/
							SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";

							
							if(strLimit.Trim().Equals("0") || strLimit == "" || strLimit.Equals("0.00"))
								strLimit="Included";
							string strPrem1="";
							switch (CovCode.Trim().ToUpper())  
							{
								case "EDP469":
									if(strDed.Trim().Equals("0.00"))
										strDed="";
									else
									{
										if(strDed.IndexOf(".")!=-1 )
										{
											strDed=strDed.Substring(0,strDed.IndexOf("."));   
										}
										strDed+="%";
									}
									break;
								case "MSC480":
									strDed="2%";
									break;
								case "BR1143":
									strDed=APDIDed.ToString()+".00"; 
									break;
								case "ACS":
									strDed=APDIDed.ToString()+".00"; 
									break;
								case "BIAA":
									strDed=APDIDed.ToString()+".00"; 
									break;
								case "CS":
									strDed=APDIDed.ToString()+".00"; 
									break;
								case "RTE":
									strDed=APDIDed.ToString()+".00"; 
									break;
								case "TSPL":
									strDed=APDIDed.ToString()+".00"; 
									break;
								case "DP320":
									strDed="N/A";
									break;
								case "DP321":
								case "DP02":
									strDed="N/A";
									break;
								case "DP03":
									strDed="N/A";
									break;
								case "DP422":
									strDed=APDIDed.ToString()+".00"; 
									break;
								case "SD":
								case "BOSTR":
									strDed=APDIDed.ToString()+".00"; 
									break;
								case "DP100":
								case "DP113":
								case "DP121":
								case "DP417":
								case "DP216":
								case "LP417":
									strDed="N/A";
									strLimit="Included";
									strPrem1="Included";
									break;
								
									//								case "SD":
									//									if (strLimit.ToUpper().Equals("INCLUDED"))
									//										strLimit="500.00";
									//									strDed=APDIDed.ToString() + ".00"; 
									//									break;
							}

							string strLimit1 = strLimit;
							if(strLimit1.IndexOf(".00") > 0)
								strLimit1 = "$" + strLimit1.Replace(".00","");

							string strDed1 = strDed;
							if(strDed1.IndexOf(".00") > 0)
							{
								strDed1 = strDed1.Replace(".00","");
								if(strDed1!="" && strDed1!="0" && strDed1!="N/A")
								{
									string dbldeduc=(Convert.ToDouble(strDed1)).ToString("###,###");
									strDed1= "$" + dbldeduc;
								}
							}
							//string strPrem1 = GetPremium(DSTempDwelling, CovCode);
							//Added by asfa (20-Feb-2008) - iTrack issue #3331
							
							if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
								strPrem1= GetPremiumBeforeCommit(DSTempDwelling, CovCode, htpremium );
							else // when gStrtemp != "temp"
								strPrem1 = GetPremium(DSTempDwelling, CovCode);
							
							if(strPrem1.IndexOf(".00") > 0)
								strPrem1 = "$" + strPrem1;
					
							switch (CovCode.Trim().ToUpper())  
							{
								case "DP03":
								case "DP02":
									strPrem1="Included";
									break;
								case "DP100":
									strPrem1="Included";
									break;
								case "DP113":
									strPrem1="Included";
									break;
								case "DP121":
								case "DP320":
									strPrem1="Included";
									break;
								case "DP321":
								case "DP417":
									strPrem1="Included";
									break;
								case "DP422":
									strLimit1="Included";
									strPrem1="Included";
									break;
								case "DP216":
								case "DP289":
								case "BR1143":
								
								case "CSL":
								case "MEDPM":
								case "IOO":
									strPrem1="Included";
									strDed1="N/A";
									break;
								case "PIOSS":
								
									strPrem1="Included";
									strDed1="N/A";
									break;
								case "DP382":
									strDed1="N/A";
									strLimit1="Included";
									strPrem1="Included";
									break;
								case "DP392":
									strDed1="N/A";
									break;
								case "DP900":
									strDed1="N/A";
									strLimit1="N/A";
									strPrem1="N/A";
									break;
								case "LP124":
									strDed1="N/A"; 
									strPrem1="Included";
									break;
								case "LP417":
									strPrem1="Included";
									break;
							}

							if(strLimit1=="0")
							{
								strLimit1="Included";
							}
							SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<APPLICANTNAME " + fieldType +"=\""+ fieldTypeText +"\">"+ApplicantName1+"</APPLICANTNAME>";
							SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<APPLICANTNO " + fieldType +"=\""+ fieldTypeText +"\">"+PolicyNumber+"</APPLICANTNO>";
							SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strLimit1)+"</HM_ENDLIMIT>";
							SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDed1)+"</HM_ENDDEDUCTIBLE>";

							//							if(htpremium.Contains(comCode)) 
							SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem1)+"</HM_ENDPREMIUM>";
							
							#region switch case
							//if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//{
							//								XmlElement SupplementDwellEndmtElement;
							//								SupplementDwellEndmtElement = AcordPDFXML.CreateElement("ENDORSEMENTSINFO");
							//								SupplementREDWEndmts.AppendChild(SupplementDwellEndmtElement);
							//								SupplementDwellEndmtElement.SetAttribute(fieldType,fieldTypeNormal);
							//								SupplementDwellEndmtElement.SetAttribute(id,endCtr.ToString());
							//		
							//								#region SWITCH CASE FOR SUPPLEMENT PAGE
							//								switch(CoverageDetails["COV_CODE"].ToString())
							//								{
							//									case "BOSTR":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";
							//					
							//											if(htpremium.Contains("BOSTR"))
							//											{
							//												gstrGetPremium	=	htpremium["BOSTR"].ToString();
							//												gintGetindex	=	gstrGetPremium.IndexOf(".");
							//												if(gintGetindex==	-1)
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOSTR"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//												else
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOSTR"].ToString())+"</HM_ENDPREMIUM>";
							//											}
							//										}
							//
							//										break;
							//									case "BIAA":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+APDIDed+"</HM_ENDDEDUCTIBLE>";
							//					
							//											if(htpremium.Contains("BLDG_ALT"))
							//											{
							//												gstrGetPremium	=	htpremium["BLDG_ALT"].ToString();
							//												gintGetindex	=	gstrGetPremium.IndexOf(".");
							//												if(gintGetindex==	-1)
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BLDG_ALT"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//												else
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BLDG_ALT"].ToString())+"</HM_ENDPREMIUM>";
							//											}
							//										}
							//										break;
							//									case "TSPL":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+APDIDed+"</HM_ENDDEDUCTIBLE>";
							//					
							//											if(htpremium.Contains("TREES"))
							//											{
							//												gstrGetPremium	=	htpremium["TREES"].ToString();
							//												gintGetindex	=	gstrGetPremium.IndexOf(".");
							//												if(gintGetindex==	-1)
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["TREES"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//												else
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["TREES"].ToString())+"</HM_ENDPREMIUM>";
							//											}
							//										}
							//										break;
							//									case "RTE":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";
							//			
							//											if(htpremium.Contains("RADIO"))
							//											{
							//												gstrGetPremium	=	htpremium["RADIO"].ToString();
							//												gintGetindex	=	gstrGetPremium.IndexOf(".");
							//												if(gintGetindex==	-1)
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RADIO"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//												else
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RADIO"].ToString())+"</HM_ENDPREMIUM>";
							//											}
							//										}
							//										break;
							//									case "SD":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"].ToString()!="" )
							//											{
							//												SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+ (double.Parse(RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())) + (500.00)) +"</HM_ENDLIMIT>";	
							//											}
							//											else
							//												SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">500.00</HM_ENDLIMIT>";	
							//									
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";
							//			
							//											if(htpremium.Contains("SAT_DISH"))
							//											{
							//												gstrGetPremium	=	htpremium["SAT_DISH"].ToString();
							//												gintGetindex	=	gstrGetPremium.IndexOf(".");
							//												if(gintGetindex==	-1)
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["SAT_DISH"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//												else
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["SAT_DISH"].ToString())+"</HM_ENDPREMIUM>";
							//											}
							//										}
							//										break;
							//									case "ACS":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+APDIDed+"</HM_ENDDEDUCTIBLE>";
							//			
							//											if(htpremium.Contains("AWNG"))
							//											{
							//												gstrGetPremium	=	htpremium["AWNG"].ToString();
							//												gintGetindex	=	gstrGetPremium.IndexOf(".");
							//												if(gintGetindex==	-1)
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["AWNG"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//												else
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["AWNG"].ToString())+"</HM_ENDPREMIUM>";
							//											}
							//										}
							//										break;
							//									case "EDP469":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">5%</HM_ENDDEDUCTIBLE>";
							//			
							//											if(htpremium.Contains("ERTHQKE"))
							//											{
							//												gstrGetPremium	=	htpremium["ERTHQKE"].ToString();
							//												gintGetindex	=	gstrGetPremium.IndexOf(".");
							//												if(gintGetindex==	-1)
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ERTHQKE"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//												else
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ERTHQKE"].ToString())+"</HM_ENDPREMIUM>";
							//											}
							//										}
							//										break;
							//									case "BR1143":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";
							//			
							//											if(htpremium.Contains("BR1143"))
							//											{
							//												gstrGetPremium	=	htpremium["BR1143"].ToString();
							//												gintGetindex	=	gstrGetPremium.IndexOf(".");
							//												if(gintGetindex==	-1)
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BR1143"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//												else
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BR1143"].ToString())+"</HM_ENDPREMIUM>";
							//											}
							//										}
							//										break;
							//									case "IF184":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";
							//			
							//											if(htpremium.Contains("INST_FLTR_BLDG"))
							//											{
							//												gstrGetPremium	=	htpremium["INST_FLTR_BLDG"].ToString();
							//												gintGetindex	=	gstrGetPremium.IndexOf(".");
							//												if(gintGetindex==	-1)
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INST_FLTR_BLDG"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//												else
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INST_FLTR_BLDG"].ToString())+"</HM_ENDPREMIUM>";
							//											}
							//										}
							//										break;
							//									case "CS":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+APDIDed+"</HM_ENDDEDUCTIBLE>";
							//			
							//											if(htpremium.Contains("CNTNTS_STRG"))
							//											{
							//												gstrGetPremium	=	htpremium["CNTNTS_STRG"].ToString();
							//												gintGetindex	=	gstrGetPremium.IndexOf(".");
							//												if(gintGetindex==	-1)
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CNTNTS_STRG"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//												else
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CNTNTS_STRG"].ToString())+"</HM_ENDPREMIUM>";
							//
							//											}
							//										}
							//										break;
							//									case "IFNSE":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">500.00</HM_ENDDEDUCTIBLE>";
							//			
							//											if(htpremium.Contains("INST_FLTR_NON_STR"))
							//											{
							//												gstrGetPremium	=	htpremium["INST_FLTR_NON_STR"].ToString();
							//												gintGetindex	=	gstrGetPremium.IndexOf(".");
							//												if(gintGetindex==	-1)
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INST_FLTR_NON_STR"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//												else
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INST_FLTR_NON_STR"].ToString())+"</HM_ENDPREMIUM>";
							//											}
							//										}
							//										break;
							//									case "MSC480":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">2%</HM_ENDDEDUCTIBLE>";
							//			
							//											if(htpremium.Contains("MNE_SBS"))
							//											{
							//												gstrGetPremium	=	htpremium["MNE_SBS"].ToString();
							//												gintGetindex	=	gstrGetPremium.IndexOf(".");
							//												if(gintGetindex==	-1)
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["MNE_SBS"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//												else
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["MNE_SBS"].ToString())+"</HM_ENDPREMIUM>";
							//											}
							//										}
							//										break;
							//									case "IOO":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";								
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";
							//			
							//											if(htpremium.Contains("INCI_OFCE"))
							//											{
							//												gstrGetPremium	=	htpremium["INCI_OFCE"].ToString();
							//												gintGetindex	=	gstrGetPremium.IndexOf(".");
							//												if(gintGetindex==	-1)
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCI_OFCE"].ToString() + ".00")+"</HM_ENDPREMIUM>";
							//												else
							//													SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCI_OFCE"].ToString())+"</HM_ENDPREMIUM>";
							//											}
							//										}
							//										break;
							//						
							//									case "DP100":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";								
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//										}
							//										break;
							//									case "DP113":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";								
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//										}
							//										break;
							//									case "DP121":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";								
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//										}
							//										break;
							//									case "DP417":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";								
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//										}
							//										break;
							//									case "DP422":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";								
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//										}
							//										break;
							//									case "DP289":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";								
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//										}
							//										break;
							//									case "DP382":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";								
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//										}
							//										break;
							//									case "LP417":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";								
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//										}
							//										break;
							//									case "PIOSS":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";								
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//										}
							//										break;
							//									case "DP02":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";								
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";										
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//										}
							//										break;
							//									case "DP03":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";								
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//										}
							//										break;
							//									case "DP216":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";							
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//										}
							//										break;
							//									case "DP320":
							//										if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()!="Y")
							//										{
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())  +"</HM_ENDFORM>";								
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">500.00</HM_ENDLIMIT>";
							//											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//										}
							//										break;
							//									default:
							//										SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString()) +"</HM_ENDFORM>";								
							//										SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
							//
							//										SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
							//										SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
							//										SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";												
							//
							//										break;
							//
							//								}												
							//								#endregion
							//	
							//							
							#endregion

						
		
							//}//END OF COVERAGE CODE CHECK
							endCtr++;
						}//END OF CALLING PAGE CHECK FOR ACCORD
					}//
					#endregion
				}//END OF FOR LOOP CHECK
				///////////////////////////////////////////////////////////////////
				// Property Expense fee will be shown at last in endorsement section
//				if(gStrCopyTo == "AGENCY")
//				{
//					if (gStrPdfFor==PDFForDecPage)
//					{
//						if(ProPExpFee.ToString()!="" && ProPExpFee.ToString()!=null && ProPExpFee.ToString()!="0" && ProPExpFee.ToString()!="0.00"  )
//						{
//							XmlElement DecPageDwellPropFeeElement;
//							DecPageDwellPropFeeElement = AcordPDFXML.CreateElement("ENDORSEMENTSINFO");
//							DecPageREDWEndmts.AppendChild(DecPageDwellPropFeeElement);
//							DecPageDwellPropFeeElement.SetAttribute(fieldType,fieldTypeNormal);
//							DecPageDwellPropFeeElement.SetAttribute(id,"0" );
//							DecPageDwellPropFeeElement.InnerXml= DecPageDwellPropFeeElement.InnerXml + "<AGENCYACCOUNTINFORMATION " + fieldType +"=\""+ fieldTypeText +"\">"+ "**Account Information -  "+"$"+RemoveJunkXmlCharacters(DollarFormat(double.Parse(ProPExpFee.ToString()))) + "**" +"</AGENCYACCOUNTINFORMATION>";				
//						}
//					}
//				}
					if (gStrPdfFor==PDFForAcord )
					{
						if(ProPExpFee.ToString()!="" && ProPExpFee.ToString()!=null && ProPExpFee.ToString()!="0" && ProPExpFee.ToString()!="0.00"  )
						{
							XmlElement SupplementDwellPropFeeElement;
							SupplementDwellPropFeeElement = AcordPDFXML.CreateElement("ENDORSEMENTSINFO");
							SupplementREDWEndmts.AppendChild(SupplementDwellPropFeeElement);
							SupplementDwellPropFeeElement.SetAttribute(fieldType,fieldTypeNormal);
							SupplementDwellPropFeeElement.SetAttribute(id,endCtr.ToString());

							SupplementDwellPropFeeElement.InnerXml= SupplementDwellPropFeeElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Expense Fee")+"</HM_ENDDESCRIPTION>";
							SupplementDwellPropFeeElement.InnerXml= SupplementDwellPropFeeElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</HM_ENDLIMIT>";
							SupplementDwellPropFeeElement.InnerXml= SupplementDwellPropFeeElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</HM_ENDDEDUCTIBLE>";
							SupplementDwellPropFeeElement.InnerXml= SupplementDwellPropFeeElement.InnerXml + "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">$"+RemoveJunkXmlCharacters(DollarFormat(double.Parse(ProPExpFee.ToString())))+"</HM_ENDPREMIUM>";
						}
					}
				///////////////////////////////////////////////////////////////////
				#endregion			

				#region Supplemental Page Outbuildings
				
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@DWELLINGID",DwellingDetail["DWELLING_ID"]);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DataSet DSTempOutbuildings = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_otherstructures");
				gobjWrapper.ClearParameteres();

				//DataSet DSTempOutbuildings = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_otherstructures " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + DwellingDetail["DWELLING_ID"] +  ",'" + gStrCalledFrom + "'");
				if (gStrPdfFor==PDFForAcord  )
				{
					XmlElement SupplementRootOutbuildings;
					SupplementRootOutbuildings= AcordPDFXML.CreateElement("OUTBUILDINGS");					
					SupplementREDWElement.AppendChild(SupplementRootOutbuildings);
					SupplementRootOutbuildings.SetAttribute(fieldType,fieldTypeMultiple);
					SupplementRootOutbuildings.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTOUTBUILDING"));
					SupplementRootOutbuildings.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTOUTBUILDING"));
					SupplementRootOutbuildings.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTOUTBUILDINGEXTN"));
					SupplementRootOutbuildings.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTOUTBUILDINGEXTN"));

					int outCtr=0;
					if(DSTempOutbuildings.Tables[0].Rows.Count>0   )
					{
						foreach(DataRow CoverageDetails in DSTempOutbuildings.Tables[0].Rows)
						{
							XmlElement SupplementOutbuildings;
							SupplementOutbuildings= AcordPDFXML.CreateElement("OUTBUILDINGSINFO");
							SupplementRootOutbuildings.AppendChild(SupplementOutbuildings);
							SupplementOutbuildings.SetAttribute(fieldType,fieldTypeNormal);
							SupplementOutbuildings.SetAttribute(id,outCtr.ToString() );

							SupplementOutbuildings.InnerXml= SupplementOutbuildings.InnerXml + "<DESCRIPTIONOUTBUILDINGS " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["PREMISES_DESCRIPTION"].ToString()) +"</DESCRIPTIONOUTBUILDINGS>";
							SupplementOutbuildings.InnerXml= SupplementOutbuildings.InnerXml + "<USEOUTBUILDINGS " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["PREMISES_USE"].ToString()) +"</USEOUTBUILDINGS>";
							SupplementOutbuildings.InnerXml= SupplementOutbuildings.InnerXml + "<CONDITIONOUTBUILDINGS " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["LOOKUP_VALUE_DESC"].ToString()) +"</CONDITIONOUTBUILDINGS>";
							SupplementOutbuildings.InnerXml= SupplementOutbuildings.InnerXml + "<PHOTOUTBUILDINGS " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["PICTURE_ATTACHED"].ToString()) +"</PHOTOUTBUILDINGS>";
							if(CoverageDetails["INSURING_VALUE"].ToString()!="")
							{
								SupplementOutbuildings.InnerXml= SupplementOutbuildings.InnerXml + "<VALUEOUTBUILDINGS " + fieldType +"=\""+ fieldTypeText +"\">$"+ RemoveJunkXmlCharacters(CoverageDetails["INSURING_VALUE"].ToString()) +"</VALUEOUTBUILDINGS>";
							}
							outCtr++;
						}
					}
					else
					{
						XmlElement SupplementOutbuildings;
						SupplementOutbuildings= AcordPDFXML.CreateElement("OUTBUILDINGSINFO");
						SupplementRootOutbuildings.AppendChild(SupplementOutbuildings);
						SupplementOutbuildings.SetAttribute(fieldType,fieldTypeNormal);
						SupplementOutbuildings.SetAttribute(id,outCtr.ToString() );

						SupplementOutbuildings.InnerXml= SupplementOutbuildings.InnerXml + "<DESCRIPTIONOUTBUILDINGS " + fieldType +"=\""+ fieldTypeText +"\">"+ "None" +"</DESCRIPTIONOUTBUILDINGS>";
					}

				}
				
				#endregion

				#region Addl Interests
				XmlElement DecPageDwellAddlInt;
				DecPageDwellAddlInt = AcordPDFXML.CreateElement("ADDITIONALINT");			

				#region Additional Root Element int Dec Page
				if (gStrPdfFor==PDFForDecPage)
				{
						
					DecPageREDWElement.AppendChild(DecPageDwellAddlInt);
					DecPageDwellAddlInt.SetAttribute(fieldType,fieldTypeMultiple);
					DecPageDwellAddlInt.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEREDWADDLINT"));
					DecPageDwellAddlInt.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEREDWADDLINT"));
					DecPageDwellAddlInt.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEWDWADDLINTEXTN"));
					DecPageDwellAddlInt.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEWDWADDLINTEXTN"));
				}
				#endregion
				

				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@DWELLINGID",DwellingDetail["DWELLING_ID"]);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DataSet DSTempDwellinAddInt = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_Additional_Interest");
				gobjWrapper.ClearParameteres();

				//DataSet DSTempDwellinAddInt = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_Additional_Interest " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + "," + DwellingDetail["DWELLING_ID"] + ",'" + gStrCalledFrom + "'");
				foreach(DataRow AddlIntDetails in DSTempDwellinAddInt.Tables[0].Rows)
				{
					XmlElement DecPageDwelAddlIntElement;
					DecPageDwelAddlIntElement = AcordPDFXML.CreateElement("ADDITIONALINTINFO");

					#region AddlInt Dec Page
					if (gStrPdfFor==PDFForDecPage )
					{
						DecPageDwellAddlInt.AppendChild(DecPageDwelAddlIntElement);
						DecPageDwelAddlIntElement.SetAttribute(fieldType,fieldTypeNormal);
						DecPageDwelAddlIntElement.SetAttribute(id,AddInt.ToString());
					
						DecPageDwelAddlIntElement.InnerXml = DecPageDwelAddlIntElement.InnerXml +"<HOMEOWNER_SERIALNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["RANK"].ToString())+"</HOMEOWNER_SERIALNO>"; 
						DecPageDwelAddlIntElement.InnerXml = DecPageDwelAddlIntElement.InnerXml +"<HOMEOWNER_NAMEADDRESS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["HOLDER_CITYSTATEZIP"].ToString())+"</HOMEOWNER_NAMEADDRESS>"; 
						DecPageDwelAddlIntElement.InnerXml = DecPageDwelAddlIntElement.InnerXml +"<HOMEOWNER_NATUREOFINTEREST " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["LOOKUP_VALUE_DESC"].ToString())+"</HOMEOWNER_NATUREOFINTEREST>"; 
						DecPageDwelAddlIntElement.InnerXml = DecPageDwelAddlIntElement.InnerXml +"<HOMEOWNER_LOANNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["LOAN_REF_NUMBER"].ToString())+"</HOMEOWNER_LOANNO>"; 
					}
					#endregion

					
					AddInt++;
				}
				AddInt=0;
				#endregion

				#region Creating Credit And Surcharge Xml
				
					int CreditSurchRowCounter = 0;
					string AdditionalExtnTxt="";

					#region Credits
					XmlElement DecPageREDWCredit;
					DecPageREDWCredit = AcordPDFXML.CreateElement("REDWCREDIT");

					XmlElement SupplementREDWCredit;
					SupplementREDWCredit = AcordPDFXML.CreateElement("REDWCREDIT");

					if (gStrPdfFor == PDFForDecPage)
					{
						#region Dec Page Element
						DecPageREDWElement.AppendChild(DecPageREDWCredit);
						DecPageREDWCredit.SetAttribute(fieldType,fieldTypeMultiple);
						DecPageREDWCredit.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECREDIT"));
						DecPageREDWCredit.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGECREDIT"));
						DecPageREDWCredit.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGECREDITEXTN"));
						DecPageREDWCredit.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGECREDITEXTN"));

						#endregion
					}
					else if (gStrPdfFor == PDFForAcord)
					{
						#region Supplement Page Element
						SupplementREDWElement.AppendChild(SupplementREDWCredit);
						SupplementREDWCredit.SetAttribute(fieldType,fieldTypeMultiple);
						SupplementREDWCredit.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTCREDIT"));
						SupplementREDWCredit.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTCREDIT"));
						SupplementREDWCredit.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTCREDITEXTN"));
						SupplementREDWCredit.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTCREDITEXTN"));
					
						#endregion
					}

					htpremium_dis.Clear(); 
					//When policy is not commited
					string strCompCode="",strDesc="",strPremium="",strpercen="";
					if(gStrtemp == "temp")
					{
						if (isRateGenerated)
						{
							foreach (XmlNode CreditNode in GetCredits(DwellingDetail["DWELLING_ID"].ToString()))
							{
								if(!htpremium_dis.Contains(getAttributeValue(CreditNode,"COMPONENT_CODE")))
									htpremium_dis.Add(getAttributeValue(CreditNode,"COMPONENT_CODE"),getAttributeValue(CreditNode,"STEPPREMIUM"));
								else
									continue;

						
							
						
								strCompCode=getAttributeValue(CreditNode,"COMPONENT_CODE").Trim() ;
								strDesc=getAttributeValue(CreditNode,"STEPDESC");
								if(strDesc.IndexOf("-    ")>=0)
									strDesc=strDesc.Replace("-    ","");
								if(strCompCode.Equals("D_DUC"))
								{
									if(strCompDate=="")
									{
										//strDesc=getAttributeValue(CreditNode,"STEPDESC") ;
									}
									else
									{
										strDesc=strDesc + "-" + strCompDate  ;
									}
								}

							
								else if(strCompCode.Equals("D_MULTIPOL"))
									strDesc=strDesc + "-" + strPolicyNum  ;			
				
								strPremium=getAttributeValue(CreditNode,"STEPPREMIUM");

								if (gStrPdfFor == PDFForDecPage)
								{
									XmlElement DecPageREDWCreditElement;
									DecPageREDWCreditElement = AcordPDFXML.CreateElement("REDWCREDITINFO");
									DecPageREDWCredit.AppendChild(DecPageREDWCreditElement);
									DecPageREDWCreditElement.SetAttribute(fieldType,fieldTypeNormal);
									DecPageREDWCreditElement.SetAttribute(id,CreditSurchRowCounter.ToString());
											
									DecPageREDWCreditElement.InnerXml += "<HM_DISCOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDesc.Replace("Discount - ","").Replace("\r\n",""))+"</HM_DISCOUNT>"; 
									//DecPageREDWCreditElement.InnerXml += "<HM_DISCPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPremium)+"</HM_DISCPREMIUM>"; 
								}					
								else if (gStrPdfFor == PDFForAcord)
								{
									#region Supplement Page
									XmlElement SupplementREDWCreditElement;
									SupplementREDWCreditElement = AcordPDFXML.CreateElement("REDWCREDITINFO");
									SupplementREDWCredit.AppendChild(SupplementREDWCreditElement);
									SupplementREDWCreditElement.SetAttribute(fieldType,fieldTypeNormal);
									SupplementREDWCreditElement.SetAttribute(id,CreditSurchRowCounter.ToString());

									SupplementREDWCreditElement.InnerXml += "<HM_DISCOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDesc.Replace("Discount - ",""))+"</HM_DISCOUNT>"; 
									SupplementREDWCreditElement.InnerXml += "<HM_DISCPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPremium)+"</HM_DISCPREMIUM>"; 

									#endregion
								}	

								CreditSurchRowCounter++;
							}
									
							htpremium_sur.Clear(); 
							foreach (XmlNode CreditNode in GetSurcharges(DwellingDetail["DWELLING_ID"].ToString()))
							{
								if(!htpremium_sur.Contains(getAttributeValue(CreditNode,"COMPONENT_CODE")))
									htpremium_sur.Add(getAttributeValue(CreditNode,"COMPONENT_CODE"),getAttributeValue(CreditNode,"STEPPREMIUM"));
								else
									continue;

								if (gStrPdfFor == PDFForDecPage)
								{
									//XmlElement DecPageREDWSurchElement;
									XmlElement DecPageREDWCreditElement;
									DecPageREDWCreditElement = AcordPDFXML.CreateElement("REDWCREDITINFO");
									DecPageREDWCredit.AppendChild(DecPageREDWCreditElement);
									DecPageREDWCreditElement.SetAttribute(fieldType,fieldTypeNormal);
									DecPageREDWCreditElement.SetAttribute(id,CreditSurchRowCounter.ToString());
											
									DecPageREDWCreditElement.InnerXml += "<HM_DISCOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("Charge - ","").Replace("\r\n",""))+"</HM_DISCOUNT>"; 

									CreditSurchRowCounter++;
								}
							}
						}
				}
					// when policy commited
				else
				{

						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						gobjWrapper.AddParameter("@DWELLINGID",DwellingDetail["DWELLING_ID"]);
						gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						DSTempDwelling = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeOwner_Coverage_Details");
						gobjWrapper.ClearParameteres();

					//DSTempDwelling = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeOwner_Coverage_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + DwellingDetail["DWELLING_ID"] +  ",'" + gStrCalledFrom + "'");
					//discount
					foreach (DataRow COVPREMDETAIL in DSTempDwelling.Tables[1].Rows)
					{
						if(COVPREMDETAIL["COMPONENT_TYPE"].ToString().ToUpper()=="D" && (COVPREMDETAIL["COVERAGE_PREMIUM"].ToString()!="0" && COVPREMDETAIL["COVERAGE_PREMIUM"].ToString()!="0.00"))
						{
							strCompCode=COVPREMDETAIL["COMPONENT_CODE"].ToString().Trim();
							strDesc=COVPREMDETAIL["COMP_REMARKS"].ToString().Trim();
							strpercen=COVPREMDETAIL["COM_EXT_AD"].ToString().Trim();
							if(strpercen!="")
								strDesc=strDesc+" "+strpercen;
							else
								strDesc=strDesc;
							if(strCompCode.Equals("D_DUC"))
							{
								if(strCompDate=="")
								{
										
								}
								else
								{
									strDesc=strDesc + "-" + strCompDate;
								}
							}

							
							else if(strCompCode.Equals("D_MULTIPOL"))
								strDesc=strDesc + "-" + strPolicyNum;			
				
							strPremium=COVPREMDETAIL["COVERAGE_PREMIUM"].ToString().Trim();

							if (gStrPdfFor == PDFForDecPage)
							{
								XmlElement DecPageREDWCreditElement;
								DecPageREDWCreditElement = AcordPDFXML.CreateElement("REDWCREDITINFO");
								DecPageREDWCredit.AppendChild(DecPageREDWCreditElement);
								DecPageREDWCreditElement.SetAttribute(fieldType,fieldTypeNormal);
								DecPageREDWCreditElement.SetAttribute(id,CreditSurchRowCounter.ToString());
											
								DecPageREDWCreditElement.InnerXml += "<HM_DISCOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDesc.Replace("Discount - ","").Replace("\r\n",""))+"</HM_DISCOUNT>"; 
							}					
							else if (gStrPdfFor == PDFForAcord)
							{
								#region Supplement Page
								XmlElement SupplementREDWCreditElement;
								SupplementREDWCreditElement = AcordPDFXML.CreateElement("REDWCREDITINFO");
								SupplementREDWCredit.AppendChild(SupplementREDWCreditElement);
								SupplementREDWCreditElement.SetAttribute(fieldType,fieldTypeNormal);
								SupplementREDWCreditElement.SetAttribute(id,CreditSurchRowCounter.ToString());

								SupplementREDWCreditElement.InnerXml += "<HM_DISCOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDesc.Replace("Discount - ",""))+"</HM_DISCOUNT>"; 
								SupplementREDWCreditElement.InnerXml += "<HM_DISCPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPremium)+"</HM_DISCPREMIUM>"; 

								#endregion
							}	

							CreditSurchRowCounter++;
						}
					}
					// surcharge
					foreach (DataRow COVPREMDETAIL in DSTempDwelling.Tables[1].Rows)
					{
						if(COVPREMDETAIL["COMPONENT_TYPE"].ToString().ToUpper()=="S" && (COVPREMDETAIL["COVERAGE_PREMIUM"].ToString()!="0" && COVPREMDETAIL["COVERAGE_PREMIUM"].ToString()!="0.00"))
						{
							strDesc=COVPREMDETAIL["COMP_REMARKS"].ToString().Trim();
							strpercen=COVPREMDETAIL["COM_EXT_AD"].ToString().Trim();
							if(strpercen!="")
								strDesc=strDesc+" "+strpercen;
							else
								strDesc=strDesc;

							if (gStrPdfFor == PDFForDecPage)
							{
								//XmlElement DecPageREDWSurchElement;
								XmlElement DecPageREDWCreditElement;
								DecPageREDWCreditElement = AcordPDFXML.CreateElement("REDWCREDITINFO");
								DecPageREDWCredit.AppendChild(DecPageREDWCreditElement);
								DecPageREDWCreditElement.SetAttribute(fieldType,fieldTypeNormal);
								DecPageREDWCreditElement.SetAttribute(id,CreditSurchRowCounter.ToString());
											
								DecPageREDWCreditElement.InnerXml += "<HM_DISCOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDesc)+"</HM_DISCOUNT>"; 

								CreditSurchRowCounter++;
							}
						}
					}
				}
					#endregion

					CreditSurchRowCounter = 0;
					AdditionalExtnTxt="";

					#region Surcharges
					XmlElement DecPageREDWSurch;
					DecPageREDWSurch = AcordPDFXML.CreateElement("RECDWSURCHARGE");

					XmlElement SupplementREDWSurch;
					SupplementREDWSurch = AcordPDFXML.CreateElement("REDWSURCHARGE");

				

					XmlElement SupplementREDWSurchElement;
					SupplementREDWSurchElement = AcordPDFXML.CreateElement("BOATSURCHARGEINFO");
								

					if (gStrPdfFor == PDFForDecPage)
					{
						//						#region Dec Page Element
						//						DecPageREDWElement.AppendChild(DecPageREDWSurch);
						//						DecPageREDWSurch.SetAttribute(fieldType,fieldTypeMultiple);
						//						DecPageREDWSurch.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESRCHG"));
						//						DecPageREDWSurch.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHG"));
						//						DecPageREDWSurch.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESRCHGEXTN"));
						//						DecPageREDWSurch.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHGEXTN"));
						//
						//					
						//						#endregion
					}
					else if (gStrPdfFor == PDFForAcord)
					{
						#region Supplement Page Element
						DecPageREDWElement.AppendChild(SupplementREDWSurch);
						SupplementREDWSurch.SetAttribute(fieldType,fieldTypeMultiple);
						SupplementREDWSurch.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTSRCHG"));
						SupplementREDWSurch.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTSRCHG"));
						SupplementREDWSurch.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTSRCHGEXTN"));
						SupplementREDWSurch.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTSRCHGEXTN"));

						SupplementREDWSurch.AppendChild(SupplementREDWSurchElement);
						SupplementREDWSurchElement.SetAttribute(fieldType,fieldTypeNormal);
						SupplementREDWSurchElement.SetAttribute(id,CreditSurchRowCounter.ToString());						
						#endregion
					}

					htpremium_sur.Clear(); 
					foreach (XmlNode CreditNode in GetSurcharges(DwellingDetail["DWELLING_ID"].ToString()))
					{
						if(!htpremium_sur.Contains(getAttributeValue(CreditNode,"COMPONENT_CODE")))
							htpremium_sur.Add(getAttributeValue(CreditNode,"COMPONENT_CODE"),getAttributeValue(CreditNode,"STEPPREMIUM"));
						else
							continue;

						if (gStrPdfFor == PDFForDecPage)
						{
							//							XmlElement DecPageREDWSurchElement;
							//							DecPageREDWSurchElement = AcordPDFXML.CreateElement("REDWSURCHARGEINFO");
							//							DecPageREDWSurch.AppendChild(DecPageREDWSurchElement);
							//							DecPageREDWSurchElement.SetAttribute(fieldType,fieldTypeNormal);
							//							DecPageREDWSurchElement.SetAttribute(id,CreditSurchRowCounter.ToString());
							//
							//							DecPageREDWSurchElement.InnerXml += "<HM_SURCHARGE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("Charge - ",""))+"</HM_SURCHARGE>"; 

						}
						else if (gStrPdfFor == PDFForAcord)
						{
							#region Supplement Page

							if (DwellingCtr%2 == 0)
								AdditionalExtnTxt="";
							else
								AdditionalExtnTxt="1";

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_VALUED_CST")	
							{							
								SupplementREDWSurchElement.InnerXml += "<HM_FAMILYUNITS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("Charge - ",""))+"</HM_FAMILYUNITS>"; 
								//DecPageBoatSurchElement.InnerXml += "<SURCHARGEPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</SURCHARGEPREM>";
							}
							#endregion
						}
						CreditSurchRowCounter++;
					}
					#endregion
				//}
				#endregion

				DwellingCtr++;
			}
		}
		#endregion

		#region Code for Dwelling Addl Interests
		private void createAcord84HomeAddlIntXml()
		{

			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@DWELLINGID",0);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DataSet DSTempDwellinAdd = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_Additional_Interest");
			gobjWrapper.ClearParameteres();

			//DataSet DSTempDwellinAdd = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_Additional_Interest " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",0,'" + gStrCalledFrom + "'");
			
			#region Acord84 Page
			if (DSTempDwellinAdd.Tables[0].Rows.Count >0)
			{
				if (gStrPdfFor==PDFForAcord  )
				{
					XmlElement Acord84DwellAddlInt;
					Acord84DwellAddlInt = AcordPDFXML.CreateElement("ADDITIONALINTERESTS");
					Acord84RootElement.AppendChild(Acord84DwellAddlInt);
					Acord84DwellAddlInt.SetAttribute(fieldType,fieldTypeMultiple);
					Acord84DwellAddlInt.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD84REDWADDLINT"));
					Acord84DwellAddlInt.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD84REDWADDLINT"));
					Acord84DwellAddlInt.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD84REDWADDLINTEXTN"));
					Acord84DwellAddlInt.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD84REDWADDLINTEXTN"));

					int RowCounter = 0;
					foreach(DataRow Row in DSTempDwellinAdd.Tables[0].Rows)
					{
						XmlElement Acord84DwellAddlIntElement;
						Acord84DwellAddlIntElement = AcordPDFXML.CreateElement("ADDLINTINFO");
						Acord84DwellAddlInt.AppendChild(Acord84DwellAddlIntElement);
						Acord84DwellAddlIntElement.SetAttribute(fieldType,fieldTypeNormal);
						Acord84DwellAddlIntElement.SetAttribute(id,RowCounter.ToString());

						Acord84DwellAddlIntElement.InnerXml = Acord84DwellAddlIntElement.InnerXml +"<APPLICANTNAME " + fieldType +"=\""+ fieldTypeText +"\">"+ApplicantName1+"</APPLICANTNAME>"; 
						Acord84DwellAddlIntElement.InnerXml = Acord84DwellAddlIntElement.InnerXml +"<APPLICANTIONNUMBER " + fieldType +"=\""+ fieldTypeText +"\">"+PolicyNumber+"</APPLICANTIONNUMBER>"; 
						Acord84DwellAddlIntElement.InnerXml = Acord84DwellAddlIntElement.InnerXml +"<ADDITIONALINTERESTNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Row["RANK"].ToString())+"</ADDITIONALINTERESTNO>"; 
						Acord84DwellAddlIntElement.InnerXml = Acord84DwellAddlIntElement.InnerXml +"<ADDITIONALINTERESTADDRESS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Row["NAME_ADDRESS"].ToString())+"</ADDITIONALINTERESTADDRESS>"; 
						Acord84DwellAddlIntElement.InnerXml = Acord84DwellAddlIntElement.InnerXml +"<ADDITIONALINTERESTCITYSTATEZIP " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Row["HOLDERCITYSTATEZIP"].ToString())+"</ADDITIONALINTERESTCITYSTATEZIP>"; 
						Acord84DwellAddlIntElement.InnerXml = Acord84DwellAddlIntElement.InnerXml +"<NATUREOFINTEREST " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(Row["NATURE_OF_INTEREST"].ToString())+"</NATUREOFINTEREST>"; 

						Acord84DwellAddlIntElement.InnerXml = Acord84DwellAddlIntElement.InnerXml +"<ADDITIONALINTERESTLOANNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Row["LOAN_REF_NUMBER"].ToString())+"</ADDITIONALINTERESTLOANNO>"; 

						string strNatInt="";
						if(Row["NATURE_OF_INTEREST"]!=null && Row["NATURE_OF_INTEREST"].ToString()!="")
						{
							strNatInt=Row["NATURE_OF_INTEREST"].ToString().Trim();
							if(strNatInt.Equals("3"))
							{
								if(Row["MEMO"]!=null && Row["MEMO"].ToString()!="")
									Acord84DwellAddlIntElement.InnerXml = Acord84DwellAddlIntElement.InnerXml +"<NATUREOFINTDESC " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(Row["MEMO"].ToString())+"</NATUREOFINTDESC>"; 		
								else if(Row["NATUREOFINT_DESC"]!=null && Row["NATUREOFINT_DESC"].ToString()!="")
								{
									Acord84DwellAddlIntElement.InnerXml = Acord84DwellAddlIntElement.InnerXml +"<NATUREOFINTDESC " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(Row["NATUREOFINT_DESC"].ToString())+"</NATUREOFINTDESC>"; 		
								}
							} 
						}
						
					
						RowCounter++;
					}
				}
			}
			#endregion
		}
		#endregion

		#region Code for Underwriting And General Info Xml Generation
		private void createDwellingUnderwritingGeneralXML()
		{

			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeownerUnderwritingDetails");
			gobjWrapper.ClearParameteres();

			//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeownerUnderwritingDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			if(DSTempDataSet.Tables[0].Rows.Count >0)
			{
				if(DSTempDataSet.Tables[0].Rows[0]["DESC_MULTI_POLICY_DISC_APPLIED"]!=null && DSTempDataSet.Tables[0].Rows[0]["DESC_MULTI_POLICY_DISC_APPLIED"].ToString()!="" )
					strPolicyNum=DSTempDataSet.Tables[0].Rows[0]["DESC_MULTI_POLICY_DISC_APPLIED"].ToString().Trim();  

				#region Acord84 Page General Info
				if (gStrPdfFor==PDFForAcord  )
				{
					XmlElement Acord84GenInfoElement;
					Acord84GenInfoElement = AcordPDFXML.CreateElement("GENERALINFO");
					Acord84RootElement.AppendChild(Acord84GenInfoElement);
					Acord84GenInfoElement.SetAttribute(fieldType,fieldTypeSingle);
				
					if(DSTempDataSet.Tables[0].Rows[0]["ANY_FARMING_BUSINESS_COND"].ToString().Trim() != "0")
						Acord84GenInfoElement.InnerXml = Acord84GenInfoElement.InnerXml +  "<FARMING " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_FARMING_BUSINESS_COND"].ToString()) + "</FARMING>";
					else
			
						Acord84GenInfoElement.InnerXml = Acord84GenInfoElement.InnerXml +  "<FARMING " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PROPERTY_USED_WHOLE_PART"].ToString()) + "</FARMING>";
					Acord84GenInfoElement.InnerXml = Acord84GenInfoElement.InnerXml +  "<RESIDENCEEMPLOYEES " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_RESIDENCE_EMPLOYEE"].ToString()) + "</RESIDENCEEMPLOYEES>";
					Acord84GenInfoElement.InnerXml = Acord84GenInfoElement.InnerXml +  "<OTHERRESIDENCE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_OTHER_RESI_OWNED"].ToString()) + "</OTHERRESIDENCE>";
					Acord84GenInfoElement.InnerXml = Acord84GenInfoElement.InnerXml +  "<OTHERINSURANCE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_OTH_INSU_COMP"].ToString()) + "</OTHERINSURANCE>";
					Acord84GenInfoElement.InnerXml = Acord84GenInfoElement.InnerXml +  "<INSURANCETRANSFERRED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["HAS_INSU_TRANSFERED_AGENCY"].ToString()) + "</INSURANCETRANSFERRED>";
					Acord84GenInfoElement.InnerXml = Acord84GenInfoElement.InnerXml +  "<COVERAGEDECLINED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_COV_DECLINED_CANCELED"].ToString()) + "</COVERAGEDECLINED>";
					Acord84GenInfoElement.InnerXml = Acord84GenInfoElement.InnerXml +  "<ANIMALSPETS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANIMALS_EXO_PETS_HISTORY"].ToString()) + "</ANIMALSPETS>";
					Acord84GenInfoElement.InnerXml = Acord84GenInfoElement.InnerXml +  "<FIVEACRES " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PROPERTY_ON_MORE_THAN"].ToString()) + "</FIVEACRES>";
					Acord84GenInfoElement.InnerXml = Acord84GenInfoElement.InnerXml +  "<LANDUSE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PROPERTY_ON_MORE_THAN_DESC"].ToString()) + "</LANDUSE>";

					Acord84GenInfoElement.InnerXml = Acord84GenInfoElement.InnerXml +  "<APPLICANTCONVICTED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CONVICTION_DEGREE_IN_PAST"].ToString()) + "</APPLICANTCONVICTED>";
					Acord84GenInfoElement.InnerXml = Acord84GenInfoElement.InnerXml +  "<BUILDINGUNDERRENOVATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_RENOVATION"].ToString()) + "</BUILDINGUNDERRENOVATION>";
					Acord84GenInfoElement.InnerXml = Acord84GenInfoElement.InnerXml +  "<PROPERTYCOMMERCIAL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_PROP_NEXT_COMMERICAL"].ToString()) + "</PROPERTYCOMMERCIAL>";
					Acord84GenInfoElement.InnerXml = Acord84GenInfoElement.InnerXml +  "<TRAMPOLINE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["TRAMPOLINE"].ToString()) + "</TRAMPOLINE>";
					Acord84GenInfoElement.InnerXml = Acord84GenInfoElement.InnerXml +  "<BUILDINGUNDERCONSTRUCTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["BUILD_UNDER_CON_GEN_CONT"].ToString()) + "</BUILDINGUNDERCONSTRUCTION>";

					Acord84GenInfoElement.InnerXml =  Acord84GenInfoElement.InnerXml +  "<DATELASTINSPECTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["LAST_INSPECTED_DATE"].ToString()) + "</DATELASTINSPECTION>";
					//All Remarks Go here.
					if(DSTempDataSet.Tables[0].Rows[0]["FARMING_DESC"].ToString()!= null)
					{
						Acord84GenInfoElement.InnerXml = Acord84GenInfoElement.InnerXml +  "<ANY_FARMING_DESC " + fieldType + "=\"" + fieldTypeText + "\">Question #1: " + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["FARMING_DESC"].ToString()) + "</ANY_FARMING_DESC>";
					}
				}
				#endregion

				
				#region Acord 84 Additinal Remarks Sheet 
				if(DSTempDataSet.Tables[0].Rows[0]["DESC_RESIDENCE_EMPLOYEE"].ToString()!="" || DSTempDataSet.Tables[0].Rows[0]["DESC_OTHER_RESIDENCE"].ToString()!="" || DSTempDataSet.Tables[0].Rows[0]["DESC_OTHER_INSURANCE"].ToString()!="" || DSTempDataSet.Tables[0].Rows[0]["DESC_INSU_TRANSFERED_AGENCY"].ToString()!="" || DSTempDataSet.Tables[0].Rows[0]["DESC_COV_DECLINED_CANCELED"].ToString()!="" || DSTempDataSet.Tables[0].Rows[0]["BREED_OTHER_DESCRIPTION"].ToString()!="" || DSTempDataSet.Tables[0].Rows[0]["DESC_CONVICTION_DEGREE_IN_PAST"].ToString()!="" || DSTempDataSet.Tables[0].Rows[0]["DESC_RENOVATION"].ToString()!="" || DSTempDataSet.Tables[0].Rows[0]["DESC_PROPERTY"].ToString()!="" || DSTempDataSet.Tables[0].Rows[0]["DESC_TRAMPOLINE"].ToString()!="" || DSTempDataSet.Tables[0].Rows[0]["REMARKS"].ToString()!="")
					if (gStrPdfFor==PDFForAcord  )
					{
						XmlElement ACORD84ADDLREMARKS;
						ACORD84ADDLREMARKS = AcordPDFXML.CreateElement("ACORD84ADDLREMARKS");
						Acord84RootElement.AppendChild(ACORD84ADDLREMARKS);
						ACORD84ADDLREMARKS.SetAttribute(fieldType,fieldTypeMultiple);
						ACORD84ADDLREMARKS.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD84REDWREMARKS"));
						ACORD84ADDLREMARKS.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD84REDWREMARKS"));
						ACORD84ADDLREMARKS.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD84REDWREMARKSEXTN"));
						ACORD84ADDLREMARKS.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD84REDWREMARKSEXTN"));
				
						
						ACORD84ADDLREMARKS.InnerXml = ACORD84ADDLREMARKS.InnerXml +  "<APPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName1 + "</APPLICANTNAME>";
						ACORD84ADDLREMARKS.InnerXml = ACORD84ADDLREMARKS.InnerXml +  "<APPLICANTIONNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</APPLICANTIONNUMBER>";

						ACORD84ADDLREMARKS.InnerXml = ACORD84ADDLREMARKS.InnerXml +  "<ALL_REMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["RENTAL_REMARKS_PDF"].ToString()) + "</ALL_REMARKS>";
						/*ACORD84ADDLREMARKS.InnerXml = ACORD84ADDLREMARKS.InnerXml +  "<NO_OF_RESI_EMPLOYEES " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #2: " + DSTempDataSet.Tables[0].Rows[0]["DESC_RESIDENCE_EMPLOYEE"].ToString()) + "</NO_OF_RESI_EMPLOYEES>";
						ACORD84ADDLREMARKS.InnerXml = ACORD84ADDLREMARKS.InnerXml +  "<OTHER_RESIDENCE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #4: " + DSTempDataSet.Tables[0].Rows[0]["DESC_OTHER_RESIDENCE"].ToString()) + "</OTHER_RESIDENCE>";
						ACORD84ADDLREMARKS.InnerXml = ACORD84ADDLREMARKS.InnerXml +  "<OTHER_INSURANCE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #5: " + DSTempDataSet.Tables[0].Rows[0]["DESC_OTHER_INSURANCE"].ToString()) + "</OTHER_INSURANCE>";
						ACORD84ADDLREMARKS.InnerXml = ACORD84ADDLREMARKS.InnerXml +  "<INSURANCE_TRANSFERRED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #6: " + DSTempDataSet.Tables[0].Rows[0]["DESC_INSU_TRANSFERED_AGENCY"].ToString())  + "</INSURANCE_TRANSFERRED>";
						ACORD84ADDLREMARKS.InnerXml = ACORD84ADDLREMARKS.InnerXml +  "<COVERAGE_DECLINED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #7: " + DSTempDataSet.Tables[0].Rows[0]["DESC_COV_DECLINED_CANCELED"].ToString()) + "</COVERAGE_DECLINED>";
						ACORD84ADDLREMARKS.InnerXml = ACORD84ADDLREMARKS.InnerXml +  "<BREED_OF_DOG " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #9: " +DSTempDataSet.Tables[0].Rows[0]["NO_OF_PETS"].ToString() + ":" +  DSTempDataSet.Tables[0].Rows[0]["BREED_OTHER_DESCRIPTION"].ToString()) + "</BREED_OF_DOG>";
						ACORD84ADDLREMARKS.InnerXml = ACORD84ADDLREMARKS.InnerXml +  "<ANY_CONVICTED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #14: " + DSTempDataSet.Tables[0].Rows[0]["DESC_CONVICTION_DEGREE_IN_PAST"].ToString()) + "</ANY_CONVICTED>";
						ACORD84ADDLREMARKS.InnerXml = ACORD84ADDLREMARKS.InnerXml +  "<CONSTRUCTION_DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #19: " + DSTempDataSet.Tables[0].Rows[0]["DESC_RENOVATION"].ToString()) + "</CONSTRUCTION_DATE>";
						ACORD84ADDLREMARKS.InnerXml = ACORD84ADDLREMARKS.InnerXml +  "<PROPERTY_DESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #21: " + DSTempDataSet.Tables[0].Rows[0]["DESC_PROPERTY"].ToString()) + "</PROPERTY_DESCRIPTION>";
						ACORD84ADDLREMARKS.InnerXml = ACORD84ADDLREMARKS.InnerXml +  "<TRAMPOLINE_DESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #22: " + DSTempDataSet.Tables[0].Rows[0]["DESC_TRAMPOLINE"].ToString()) + "</TRAMPOLINE_DESC>";
						ACORD84ADDLREMARKS.InnerXml = ACORD84ADDLREMARKS.InnerXml +  "<ANY_REMARK " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Any Other Remarks: " + DSTempDataSet.Tables[0].Rows[0]["REMARKS"].ToString()) + "</ANY_REMARK>";
*/
						ACORD84ADDLREMARKS.InnerXml = "<ACORD84REMARKSINFO " + fieldType + "=\"" + fieldTypeNormal + "\" " + id + "=\"0\">" + ACORD84ADDLREMARKS.InnerXml + "</ACORD84REMARKSINFO>";

					
					}
				#endregion
			}
		}
		#endregion

		#region Endorsement Wordings
		//by prAVESH ON 21 MARCH 2008 FOR REMOVING THOSE ENDORSEMENT WHICH HAS NOT BEEN CHANGED AT ENDORSEMENT /RENEWAL PROCESS
		private void RemoveEnorsementWordings()
		{
			try
			{
				if (prnOrd_covCode==null) return;
				DataSet dsDwelling = new DataSet();

				
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				dsDwelling = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_Dwelling_Details");
				gobjWrapper.ClearParameteres();

				//dsDwelling = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_Dwelling_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
				foreach(DataRow DwellingDetail in dsDwelling.Tables[0].Rows)
				{

					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@DWELLINGID",DwellingDetail["DWELLING_ID"]);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DataSet DSNewCoverageEndorsemet = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeOwner_Coverage_Details");
					gobjWrapper.ClearParameteres();

					//DataSet DSNewCoverageEndorsemet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeOwner_Coverage_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + DwellingDetail["DWELLING_ID"] +  ",'" + gStrCalledFrom + "'");

					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",goldVewrsionId);
					gobjWrapper.AddParameter("@DWELLINGID",DwellingDetail["DWELLING_ID"]);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DataSet DSOldCoverageEndorsemet = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeOwner_Coverage_Details");
					gobjWrapper.ClearParameteres();

					//DataSet DSOldCoverageEndorsemet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeOwner_Coverage_Details " + gStrClientID + "," + gStrPolicyId + "," + goldVewrsionId +    "," + DwellingDetail["DWELLING_ID"] +  ",'" + gStrCalledFrom + "'");
					foreach ( DataRow NewEndDetails in DSNewCoverageEndorsemet.Tables[0].Rows)
					{
						string CovCode=NewEndDetails["COV_CODE"].ToString();
						int counter = 0;
						DataRow[] drOld= DSOldCoverageEndorsemet.Tables[0].Select("COV_CODE='" + CovCode + "' AND ENDORS_ASSOC_COVERAGE='Y'");
						if( drOld.Length>0)
						{
//							if (drOld[0]["LIMIT_1"].ToString()==NewEndDetails["LIMIT_1"].ToString() && drOld[0]["EDITION_DATE"].ToString()==NewEndDetails["EDITION_DATE"].ToString() && drOld[0]["DEDUCTIBLE_1"].ToString()==NewEndDetails["DEDUCTIBLE_1"].ToString() 
//								&& drOld[0]["DEDUCTIBLE"].ToString()==NewEndDetails["DEDUCTIBLE"].ToString() 
//								&& drOld[0]["LIMIT_2"].ToString()==NewEndDetails["LIMIT_2"].ToString()  
//								)
							if ( drOld[0]["EDITION_DATE"].ToString()==NewEndDetails["EDITION_DATE"].ToString() )
							{
								for (counter=0;counter< prnOrd_covCode.Length;counter++)
								{
									if (prnOrd_covCode[counter]==null) continue;
									if(prnOrd_covCode[counter].ToString()==CovCode)
									{
										prnOrd_covCode[counter]=null;
									}
								}
							}
						}
				
					}

				}	
			}
			catch(Exception ex)
			{
				throw(ex);
			}

		}

		private void createEndorsementWordings()
		{
			if(gStrPdfFor == PDFForDecPage)
			{
				int endorsCount = 0;
				int counter = 0,Cntrl=0;
				int BlankPagePrinted=0;
				totdwellpage +=totcovPage;
				totdwellpage += intPrivacyPage;
				//check for even and odd number of pages
				totdwellpage = totdwellpage%2;
				//while(prnOrd_covCode[endorsCount] != null)
				//	endorsCount++;
				endorsCount=prnOrd_covCode.Length;

				while(counter < endorsCount)
				{
					int lowestIndex = GetLowestPrnIndex(ref prnOrd, endorsCount);
					string prncovCode="";
					if (prnOrd_covCode[lowestIndex]!=null)
						prncovCode = prnOrd_covCode[lowestIndex];
					string prnAttFile = prnOrd_attFile[lowestIndex];
					//					if(CoverageDetails["ENDORS_PRINT"].ToString() =="Y" )
				{
							
					if(prnAttFile != null && prnAttFile != ""  && prncovCode!="")
					{

						XmlElement DecPageRedwtEndoDP;
						XmlElement EndoElementDP;
						if(BlankPagePrinted==0 && totdwellpage!=0)
						{
							DecPageRedwtEndoDP = AcordPDFXML.CreateElement("REDWENDORSEMENTDP" + "_" + 0);
							DecPageRootElement.AppendChild(DecPageRedwtEndoDP);
							DecPageRedwtEndoDP.SetAttribute(fieldType,fieldTypeMultiple);

							//DecPageRedwtEndoDP.SetAttribute(PrimPDF,CoverageDetails["ATTACH_FILE"].ToString());
							DecPageRedwtEndoDP.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEBLANKDOCUMENT"));
							DecPageRedwtEndoDP.SetAttribute(PrimPDFBlocks,"1");

							DecPageRedwtEndoDP.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEBLANKDOCUMENT"));
							DecPageRedwtEndoDP.SetAttribute(SecondPDFBlocks,"1");

						
							EndoElementDP = AcordPDFXML.CreateElement("ENDOELEMENTDPINFO" + "_" + 0);
							DecPageRedwtEndoDP.AppendChild(EndoElementDP);
							EndoElementDP.SetAttribute(fieldType,fieldTypeNormal);
							EndoElementDP.SetAttribute(id,"0");
							BlankPagePrinted++;
							//counter++;
						}
						
						if(totdwellpage!=0)
						{
							Cntrl=counter+1;
							DecPageRedwtEndoDP = AcordPDFXML.CreateElement("REDWENDORSEMENTDP" + "_" + Cntrl);
							DecPageRootElement.AppendChild(DecPageRedwtEndoDP);
							DecPageRedwtEndoDP.SetAttribute(fieldType,fieldTypeMultiple);

							//DecPageRedwtEndoDP.SetAttribute(PrimPDF,CoverageDetails["ATTACH_FILE"].ToString());
							DecPageRedwtEndoDP.SetAttribute(PrimPDF,prnAttFile);
							DecPageRedwtEndoDP.SetAttribute(PrimPDFBlocks,"1");

							DecPageRedwtEndoDP.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEDPWORDEXTN"));
							DecPageRedwtEndoDP.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDPWORDEXTN"));

						
							EndoElementDP = AcordPDFXML.CreateElement("ENDOELEMENTDPINFO" + "_" + Cntrl);
							DecPageRedwtEndoDP.AppendChild(EndoElementDP);
							EndoElementDP.SetAttribute(fieldType,fieldTypeNormal);
							EndoElementDP.SetAttribute(id,"0");
						}
						else
						{
							DecPageRedwtEndoDP = AcordPDFXML.CreateElement("REDWENDORSEMENTDP" + "_" + counter);
							DecPageRootElement.AppendChild(DecPageRedwtEndoDP);
							DecPageRedwtEndoDP.SetAttribute(fieldType,fieldTypeMultiple);

							//DecPageRedwtEndoDP.SetAttribute(PrimPDF,CoverageDetails["ATTACH_FILE"].ToString());
							DecPageRedwtEndoDP.SetAttribute(PrimPDF,prnAttFile);
							DecPageRedwtEndoDP.SetAttribute(PrimPDFBlocks,"1");

							DecPageRedwtEndoDP.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEDPWORDEXTN"));
							DecPageRedwtEndoDP.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDPWORDEXTN"));

						
							EndoElementDP = AcordPDFXML.CreateElement("ENDOELEMENTDPINFO" + "_" + counter);
							DecPageRedwtEndoDP.AppendChild(EndoElementDP);
							EndoElementDP.SetAttribute(fieldType,fieldTypeNormal);
							EndoElementDP.SetAttribute(id,"0");
						}
					}
				}
					counter++;
				}
			}

		}

		#endregion Endorsement Wordings

		#region Addition Wordings
		private void createAddWordingsXML()
		{
			string lob_id="6";
			string state_id="";

			if(stCode == "IN")
			{
				state_id = "14";
			}
			else if(stCode == "MI")
			{
				state_id = "22";
			}
			if (gStrPdfFor == PDFForDecPage)
			{
				if(gStrProcessID != null && gStrProcessID != "")
				{
					DataSet DSAddWordSet = new DataSet();

					gobjWrapper.AddParameter("@STATE_ID",state_id);
					gobjWrapper.AddParameter("@LOB_ID",lob_id);
					gobjWrapper.AddParameter("@PROCESS_ID",gStrProcessID);
					DSAddWordSet = gobjWrapper.ExecuteDataSet("Proc_GetAddWordingsData");
					gobjWrapper.ClearParameteres();

					//DSAddWordSet = gobjSqlHelper.ExecuteDataSet("Proc_GetAddWordingsData " + state_id + "," + lob_id + "," + gStrProcessID);
			
					if (DSAddWordSet.Tables[0].Rows.Count > 0)
					{
						XmlElement DecAddWordingsElement;
						DecAddWordingsElement = AcordPDFXML.CreateElement("ADDWORDINGS");

						DecPageRootElement.AppendChild(DecAddWordingsElement);
						DecAddWordingsElement.SetAttribute(fieldType,fieldTypeMultiple);
						DecAddWordingsElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEADDWORDINGS"));
						DecAddWordingsElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEADDWORDINGS"));
						DecAddWordingsElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEADDWORDINGS"));
						DecAddWordingsElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEADDWORDINGS"));

						int RowCounter = 0;
						XmlElement DecWordingsInfoElement;
						DecWordingsInfoElement = AcordPDFXML.CreateElement("WORDINGSINFO");
						DecAddWordingsElement.AppendChild(DecWordingsInfoElement);
						DecWordingsInfoElement.SetAttribute(fieldType,fieldTypeNormal);
						DecWordingsInfoElement.SetAttribute(id,RowCounter.ToString());

						DecWordingsInfoElement.InnerXml = DecAddWordingsElement.InnerXml +  "<Wordings_text " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSAddWordSet.Tables[0].Rows[0]["PDF_WORDINGS"].ToString()) + "</Wordings_text>";
					}
				}
			}
		}
		#endregion

		#region Page 2
		private void createPage2XML(ref XmlElement DecPage1Element)
		{
			XmlElement DecAddPageElement;
			DecAddPageElement = AcordPDFXML.CreateElement("ADDPAGE2");

			DecPage1Element.AppendChild(DecAddPageElement);
			DecAddPageElement.SetAttribute(fieldType,fieldTypeMultiple);
			DecAddPageElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEPAGE2"));
			DecAddPageElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEPAGE2"));
			DecAddPageElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEPAGE2"));
			DecAddPageElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEPAGE2"));

			//int RowCounter = 0;
			intPrivacyPage=1;
			XmlElement DecPageInfoElement;
			DecPageInfoElement = AcordPDFXML.CreateElement("PAGE2INFO");
			DecAddPageElement.AppendChild(DecPageInfoElement);
			DecPageInfoElement.SetAttribute(fieldType,fieldTypeNormal);
			DecPageInfoElement.SetAttribute(id,"0");
/*
			DecPageInfoElement.InnerXml +="<reason_code1 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code1 + "</reason_code1>";
			DecPageInfoElement.InnerXml +="<reason_code2 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code2 + "</reason_code2>";
			DecPageInfoElement.InnerXml +="<reason_code3 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code3 + "</reason_code3>";
			DecPageInfoElement.InnerXml +="<reason_code4 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code4 + "</reason_code4>";
*/		
}

		private void createPage2PolicyPrivacyXML(ref XmlElement DecPage1Element)
		{
			XmlElement DecAddPageElement;
			DecAddPageElement = AcordPDFXML.CreateElement("ADDPAGE2");

			DecPage1Element.AppendChild(DecAddPageElement);
			DecAddPageElement.SetAttribute(fieldType,fieldTypeMultiple);
			DecAddPageElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEPAGE2"));
			DecAddPageElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEPAGE2"));
			DecAddPageElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEPAGE2"));
			DecAddPageElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEPAGE2"));

			//int RowCounter = 0;
			intPrivacyPage=1;
			XmlElement DecPageInfoElement;
			DecPageInfoElement = AcordPDFXML.CreateElement("PAGE2INFO");
			DecAddPageElement.AppendChild(DecPageInfoElement);
			DecPageInfoElement.SetAttribute(fieldType,fieldTypeNormal);
			DecPageInfoElement.SetAttribute(id,"0");

			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<not_pol " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_pol>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<not_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_txt>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<not_txt1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_txt1>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<not_txt2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_txt2>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<ins_add " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</ins_add>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<hotline " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</hotline>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<website " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</website>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<foot_left " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</foot_left>";
				

		}
		#endregion
	}
}
