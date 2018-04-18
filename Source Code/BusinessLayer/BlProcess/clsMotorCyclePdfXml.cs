using System;
using System.Xml;
using System.Data;
using Cms.DataLayer;
using System.Collections;

namespace Cms.BusinessLayer.BlProcess
{
	/// <summary>
	/// Summary description for clsMotorCyclePdfXml.
	/// </summary>
	public class clsMotorCyclePdfXml : ClsCommonPdfXML
	{
		#region Declarations
		private XmlElement RootElementDecPage;
		private XmlElement AcordMotorRootElement;
		private Hashtable htpremium=new Hashtable(); 
		private Hashtable htSumTotal=new Hashtable(); 
		private Hashtable htpremium_dis=new Hashtable(); 
		private Hashtable htpremium_sur=new Hashtable(); 
		private string recvd_prem = "";
        string UMPDreject, UNDSPreject, PUNCSreject, PUMSPreject, UNCSLreject;//strVehNo,
		bool chkFlag17=false,chkFlag=false,chkFlag16=false,chkFlag14=false;
		private string stCode="";
		private DataWrapper gobjWrapper;
        DataSet DSTempVehicle, DSTemp_DataSet;//DSTempOperator,DSTempDataSet,
		double dblBISPL=0,dblPD=0,dblPUMSP_limit1=0,dblPUMSP_limit2=0, dblRLCSL=0, dblPUNCS_limit1=0,dblPUNCS_limit2=0;//,dblSumTotal=0;
		double dblUNCSL_limit1=0,dblUNCSL_limit2=0,dblUNDSP_limit1=0,dblUNDSP_limit2=0,dblUMPD_limit1=0,dblUMPD_ded=0;
		private string []prnOrd_covCode;
		private string []prnOrd_attFile;
		private int []prnOrd;
		private string strInsScore="",strInsType="";
		private string strOldPolicyVer="";
		private double dblOldSum=0;
		private string expiry_date = "";
		string newInsuScr="";
		string oldInsuScr="";
		private string strInsuScore="";
		private string eff_date = "";
		string NamedInsuredWithSuffix="";
		//private string goldVewrsionId="0";
		int intPrivacyPage = 0;
		int inttotalpage=0;
		#endregion

		#region Constructor
		public clsMotorCyclePdfXml(string lstrClientId,string lstrPolicyId,string lstrVersionId,string lStrCalledFrom,string lStrCalledFor,string stateCode,string strProcessID,string Agn_Ins,string temp,DataWrapper lObjWrapper)
		{
			gStrClientID=lstrClientId;
			gStrPolicyId=lstrPolicyId;
			gStrPolicyVersion=lstrVersionId;
			gStrCalledFrom=lStrCalledFrom.ToUpper().Trim();
			gStrPdfFor=lStrCalledFor.ToUpper().Trim();
			stCode=stateCode;
			gStrProcessID = strProcessID;
			gStrCopyTo = Agn_Ins;
			this.gobjWrapper = lObjWrapper;
			if(Agn_Ins != null && Agn_Ins != "")
			{
				string []copyTo = Agn_Ins.Split('-');
				if(copyTo[0]=="AGENCY")
					CopyTo= "AGENT'S"+ " "+ "COPY" ;
				else
					CopyTo= copyTo[0]+"'S"+ " "+ "COPY" ;
			}
			gStrtemp=temp;
			DSTempDataSet = new DataSet();
			gobjWrapper.ClearParameteres();
			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFPolicyDetails");
			gobjWrapper.ClearParameteres();
	//		DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFPolicyDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			if(DSTempDataSet.Tables[0].Rows.Count>0)
			{
				SetPDFVersionLobNode("MOT",DateTime.Parse(DSTempDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
				SetPDFInsScoresLobNode("MOT",DateTime.Parse(DSTempDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
			}
		}
		#endregion

		public string getMotorCycleAcordPDFXml()
		{
			try
			{
			AcordPDFXML = new XmlDocument();
			AcordPDFXML.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <" + RootElement + "/>");
			if(gStrCopyTo != "AGENCY")
			{
				creatmaillingpage();
			}
			createRootElementForAllRootPDFs();
			FillMonth();
			LoadRateXML("CYCL",gobjWrapper);
			//creating Xml From Here
			CreatePolicyAgencyXML();
			CreateCoApplicantXml();
			//createOperatorXML();
			createMOTOXML();
			createMOTOUnderwritingGeneralXML();
			createAcordMotorViolationHistory();
			createOptionRejectorModifyXML();
				if (gStrCopyTo == "CUSTOMER-NOWORD"  &&					
					(gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS.ToString() ||gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS.ToString() 
					|| gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RENEWAL_PROCESS.ToString() || gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RENEWAL_PROCESS.ToString())) 
				{
					RemoveEnorsementWordings();
					createEndorsementWordings();
				}
			if(gStrCopyTo != "AGENCY" && gStrCopyTo != "CUSTOMER-NOWORD")
				createEndorsementWordings();
			if(gStrCopyTo == "CUSTOMER" || gStrCopyTo == "CUSTOMER-NOWORD")
				createAddWordingsXML();

			string customerFullxml="";
			customerFullxml=AcordPDFXML.OuterXml;
			InsertCustomerFullWordingXml(gStrClientID,gStrPolicyId,gStrPolicyVersion,gStrCopyTo,customerFullxml);
			return AcordPDFXML.OuterXml;
			}
			catch(Exception ex)
			{
				throw(new Exception("Error while creating Motorcycle XML.",ex));
			}
		}

		#region To Create Root Element For All Root PDFs
		private void createRootElementForAllRootPDFs()
		{
			if (gStrPdfFor == PDFForDecPage)
			{
				RootElementDecPage = AcordPDFXML.CreateElement(RootElementForAllPDF);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(RootElementDecPage);
				if(stCode.Equals("IN")) 
					RootElementDecPage.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEIN"));
				else if(stCode.Equals("MI")) 
					RootElementDecPage.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEMI"));
			}
			else if (gStrPdfFor == PDFForAcord)
			{

				AcordMotorRootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(AcordMotorRootElement);
				
				if(stCode.Equals("IN")) 
					AcordMotorRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORDMOTORIN"));
				else if(stCode.Equals("MI")) 
					AcordMotorRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORDMOTORMI"));
				
				
			}
		}
		#endregion
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
						DecMailElement.InnerXml = DecMailElement.InnerXml +  "<MESSAGE " + fieldType + "=\"" + fieldTypeText + "\">" + DstempDocument.Tables[0].Rows[0]["DOCUMENT_MESSAGE"].ToString()+ "</MESSAGE>";
				
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
		private  void InsertCustomerFullWordingXml(string strCustomerId,string strAppId, string strAppVersionId, string strCopyTo, string strcutomerxml)
		{
			try
			{
				//DataWrapper objDataWrapper;
				//objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);	
					
				gobjWrapper.AddParameter("@CUSTOMER_ID",strCustomerId);
				gobjWrapper.AddParameter("@POLICY_ID",strAppId);
				gobjWrapper.AddParameter("@POLICY_VERSION_ID",strAppVersionId);
				gobjWrapper.AddParameter("@CALLED_FOR",strCopyTo);
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
		private string NamedInsuredWithSuffixs(DataSet dsIns)
		{
			string names = "";
			if(dsIns.Tables.Count > 0)
			{
				foreach(DataRow drIns in dsIns.Tables[0].Rows)
				{
					if(names != "")
						names += " & " + drIns["APPNAME"].ToString()+ drIns["SUFFIX"].ToString();
					else
						names += drIns["APPNAME"].ToString() + drIns["SUFFIX"].ToString();
				}
			}
			return names;

		}
		#region Creating Policy And Agency Xml 
		private void CreatePolicyAgencyXML()
		{
			try
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
				strOldPolicyVer = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
				expiry_date = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString());
				eff_date = DSTempDataSet.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString();
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
				//			CopyTo			= RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["COPY_TO"].ToString());
	
				if(Reason.Trim() != "" && DSTempDataSet.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString() != "")
					Reason += " / Effective Date: " + DSTempDataSet.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString();

				AgencyName = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString());
				AgencyAddress = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_ADD"].ToString());
				AgencyCitySTZip = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_CITYSTZIP"].ToString());
				AgencyPhoneNumber = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString());
				AgencyCode = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_CODE"].ToString());
				AgencySubCode = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["SUB_CODE"].ToString());
				AgencyBilling = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PAYMENTBILLING"].ToString())=="AB"?"Agency Bill":"Direct Bill" ;
				recvd_prem = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["RECEIVED_PRMIUM"].ToString());
				currTerm = int.Parse(DSTempDataSet.Tables[0].Rows[0]["CURRENT_TERM"].ToString());
				if(currTerm > 1)
					applyInsScore = int.Parse(DSTempDataSet.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString());

				if(AgencyPhoneNumber !="")
				{
					if(AgencyPhoneNumber.IndexOf(")")>0)
					{
						string[] AgencyPhoneNumbers = new string[0];
						AgencyPhoneNumbers = AgencyPhoneNumber.Split(')');
						AgencyPhoneNumber = AgencyPhoneNumbers[0]+") "+AgencyPhoneNumbers[1];
					}
				}
				#endregion


				if (gStrPdfFor == PDFForAcord)
				{
					XmlElement AcordPolicyElement;
					AcordPolicyElement = AcordPDFXML.CreateElement("POLICY");
					AcordMotorRootElement.AppendChild(AcordPolicyElement);
					AcordPolicyElement.SetAttribute(fieldType,fieldTypeSingle);
				
					string stStatus="";

					gobjWrapper.AddParameter("@Customer_Id",gStrClientID);
					gobjWrapper.AddParameter("@AppPol_Id",gStrPolicyId);
					gobjWrapper.AddParameter("@AppPolVersion_Id",gStrPolicyVersion);
					gobjWrapper.AddParameter("@CalledFrom",gStrCalledFrom);
					DataSet DSTempDataSet1 = gobjWrapper.ExecuteDataSet("Proc_GetPDFStateCode");
					gobjWrapper.ClearParameteres();

					//DataSet DSTempDataSet1 = new DataWrapper(ConnStr,CommandType.Text).ExecuteDataSet("Proc_GetPDFStateCode " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'" );
					if (DSTempDataSet1.Tables[0].Rows.Count>0)
					{
						if (DSTempDataSet1.Tables[0].Rows[0]["POLICY_STATUS"].ToString().Trim() !="")
						{
							stStatus= DSTempDataSet1.Tables[0].Rows[0]["POLICY_STATUS"].ToString();
						}
					}

					if(stStatus == "" || stStatus.Trim() == "Suspended" || stStatus.Trim() == "UISSUE")
						stStatus = "N";
					else
						stStatus = "Y";

					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<BOUND " + fieldType + "=\"" + fieldTypeText + "\">" + stStatus + "</BOUND>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<TERM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_TERMS"].ToString()) + "</TERM>";
					if(DSTempDataSet.Tables[0].Rows[0]["APP_TERMS"].ToString() != "12" && DSTempDataSet.Tables[0].Rows[0]["APP_TERMS"].ToString() !="6")
						AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<APP_TERM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("0") + "</APP_TERM>";
					else
						AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<APP_TERM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_TERMS"].ToString()) + "</APP_TERM>";

					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</POLICYNUMBER>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<EFFECTIVEDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</EFFECTIVEDATE>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<EXPIRATIONDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</EXPIRATIONDATE>";
					//Agency
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</AGENCY>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYCOD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_CODE"].ToString()) + "</AGENCYCOD>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYSUBCOD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["SUB_CODE"].ToString()) + "</AGENCYSUBCOD>";
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<APP_PRE_ADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["YEARS_AT_PREV_ADD"].ToString()) + "</APP_PRE_ADDRESS>";
				
				
				
				}
				else
				{
					//Code have to move in creatmotoxml() function
					/*XmlElement DecPagePolicyAgentElement;
					DecPagePolicyAgentElement = AcordPDFXML.CreateElement("POLICY");
					RootElementDecPage.AppendChild(DecPagePolicyAgentElement);
					DecPagePolicyAgentElement.SetAttribute(fieldType,fieldTypeSingle);

					if(gStrCalledFrom == "POLICY")
					{
						DecPagePolicyAgentElement.InnerXml = DecPagePolicyAgentElement.InnerXml +  "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</POLICYNUMBER>";
						DecPagePolicyAgentElement.InnerXml = DecPagePolicyAgentElement.InnerXml +  "<POLICYEFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</POLICYEFFDATE>";
						DecPagePolicyAgentElement.InnerXml = DecPagePolicyAgentElement.InnerXml +  "<POLICYEXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</POLICYEXPDATE>";
						DecPagePolicyAgentElement.InnerXml = DecPagePolicyAgentElement.InnerXml +  "<REASON " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["REASON"].ToString()) + "</REASON>";
					}
					DecPagePolicyAgentElement.InnerXml += "<copyTo " + fieldType + "=\"" + fieldTypeText + "\">" + CopyTo + "</copyTo>";
				*/
				}
			}
			catch(Exception ex)
			{
				throw(new Exception("Error while creating Agency XML.",ex));
			}
		}
		#endregion

		#region Code for Co-Applicant or Named Insured Info Xml Generation.
		private void CreateCoApplicantXml()
		{
			try
			{
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
				gobjWrapper.ClearParameteres();

				//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			
				gobjWrapper.AddParameter("@Customer_Id",gStrClientID);
				gobjWrapper.AddParameter("@AppPol_Id",gStrPolicyId);
				gobjWrapper.AddParameter("@AppPolVersion_Id",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CalledFrom",gStrCalledFrom);
				DataSet DSAgDataSet  = gobjWrapper.ExecuteDataSet("Proc_GetPDFAgencyCode");
				gobjWrapper.ClearParameteres();

				//DataSet DSAgDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAgencyCode " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");

				DataSet DSPolDataSet;
				string assoc_pol = "";

				if(DSAgDataSet.Tables[0].Rows.Count > 0)
				{
								
					gobjWrapper.AddParameter("@CUSTOMER_ID",gStrClientID);
					gobjWrapper.AddParameter("@AGENCY_ID",DSAgDataSet.Tables[0].Rows[0]["AGENCY_ID"].ToString());
					gobjWrapper.AddParameter("@POLICY_NUMBER","");
					DSPolDataSet  = gobjWrapper.ExecuteDataSet("Proc_GetAllPolicies");
					gobjWrapper.ClearParameteres();

					//DSPolDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetAllPolicies " + gStrClientID + "," + DSAgDataSet.Tables[0].Rows[0]["AGENCY_ID"].ToString() + ",''");
					
					if(DSPolDataSet.Tables[0].Rows.Count > 0)
					{
						foreach(DataRow Poldr in DSPolDataSet.Tables[0].Rows)
						{
							if((Poldr["POLICY_NUMBER"].ToString().StartsWith("A")) || Poldr["POLICY_NUMBER"].ToString().StartsWith("H"))
							{
								assoc_pol = Poldr["POLICY_NUMBER"].ToString();
								break;
							}
						}
					}
				}

				if (DSTempDataSet.Tables[0].Rows.Count > 0 )
				{
					#region Global Variable Assignment
					ApplicantName = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPNAME"].ToString());
					NamedInsuredWithSuffix = RemoveJunkXmlCharacters(NamedInsuredWithSuffixs(DSTempDataSet));
					ApplicantAddress = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPADDRESS"].ToString());
					ApplicantCityStZip = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPCITYSTZIP"].ToString());
				
					reason_code1 = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_REASON_CODE"].ToString());
					reason_code2 = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_REASON_CODE2"].ToString());
					reason_code3 = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_REASON_CODE3"].ToString());
					reason_code4 = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_REASON_CODE4"].ToString());

					CustomerAddress = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTADDRESS"].ToString());
					CustomerCityStZip = RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTCITYSTZIP"].ToString());
					strInsScore=RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString());
					strInsuScore = strInsScore;
					strInsType=RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTOMER_INSURANCE_SCORE_TYPE"].ToString());;
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
						#region Acord 82 Page
						XmlElement AcordMotorNamedInsuredElement;
						AcordMotorNamedInsuredElement = AcordPDFXML.CreateElement("NAMEDINSURED");
						AcordMotorRootElement.AppendChild(AcordMotorNamedInsuredElement);
						AcordMotorNamedInsuredElement.SetAttribute(fieldType,fieldTypeSingle);

						AcordMotorNamedInsuredElement.InnerXml = AcordMotorNamedInsuredElement.InnerXml +  "<APP_NAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPNAME"].ToString()) + "</APP_NAME>";
						AcordMotorNamedInsuredElement.InnerXml = AcordMotorNamedInsuredElement.InnerXml +  "<APP_ADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTADDRESS"].ToString()) + "</APP_ADDRESS>";
						AcordMotorNamedInsuredElement.InnerXml = AcordMotorNamedInsuredElement.InnerXml +  "<APP_CITY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CITY"].ToString()) + "</APP_CITY>";
						AcordMotorNamedInsuredElement.InnerXml = AcordMotorNamedInsuredElement.InnerXml +  "<AP_COUNTY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTCITYSTZIP"].ToString()) + "</AP_COUNTY>";
						AcordMotorNamedInsuredElement.InnerXml = AcordMotorNamedInsuredElement.InnerXml +  "<APP_STATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["STATE_CODE"].ToString()) + "</APP_STATE>";
						AcordMotorNamedInsuredElement.InnerXml = AcordMotorNamedInsuredElement.InnerXml +  "<APP_ZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ZIP_CODE"].ToString()) + "</APP_ZIP>";
						AcordMotorNamedInsuredElement.InnerXml = AcordMotorNamedInsuredElement.InnerXml +  "<AUTOHO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(assoc_pol) + "</AUTOHO>";
						AcordMotorNamedInsuredElement.InnerXml = AcordMotorNamedInsuredElement.InnerXml +  "<APPL_PHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PHONE"].ToString()) + "</APPL_PHONE>";
					
						AcordMotorNamedInsuredElement.InnerXml = AcordMotorNamedInsuredElement.InnerXml +  "<APPCITYCOUNTRYZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUSTCITYSTZIP"].ToString()) + "</APPCITYCOUNTRYZIP>";
					
					
						AcordMotorNamedInsuredElement.InnerXml = AcordMotorNamedInsuredElement.InnerXml +  "<APPLICANTCOPLAN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Wolverine Mutual") + "</APPLICANTCOPLAN>";


						//					DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAuto_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
						#endregion
					}
					if (gStrPdfFor == PDFForDecPage)
					{
						// Move this code to MotoCreateXML()
						#region Dec Page
					/*	XmlElement DecMotorNamedInsuredElement;
						DecMotorNamedInsuredElement = AcordPDFXML.CreateElement("NAMEDINSURED");
						RootElementDecPage.AppendChild(DecMotorNamedInsuredElement);
						DecMotorNamedInsuredElement.SetAttribute(fieldType,fieldTypeSingle);

						DecMotorNamedInsuredElement.InnerXml = DecMotorNamedInsuredElement.InnerXml +  "<INSUREDNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPNAME"].ToString()) + "</INSUREDNAME>";
						DecMotorNamedInsuredElement.InnerXml = DecMotorNamedInsuredElement.InnerXml +  "<INSUREDADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPADDRESS"].ToString()) + "</INSUREDADDRESS>";
						DecMotorNamedInsuredElement.InnerXml = DecMotorNamedInsuredElement.InnerXml +  "<INSUREDCITYSTZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["APPCITYSTZIP"].ToString()) + "</INSUREDCITYSTZIP>";
						DecMotorNamedInsuredElement.InnerXml = DecMotorNamedInsuredElement.InnerXml +  "<AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyName + "</AGENCYNAME>";
						DecMotorNamedInsuredElement.InnerXml = DecMotorNamedInsuredElement.InnerXml +  "<AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyAddress + "</AGENCYADDRESS>";
						DecMotorNamedInsuredElement.InnerXml = DecMotorNamedInsuredElement.InnerXml +  "<AGENCYCITYSTZIP " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCitySTZip + "</AGENCYCITYSTZIP>";
						DecMotorNamedInsuredElement.InnerXml = DecMotorNamedInsuredElement.InnerXml +  "<PHONE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyPhoneNumber + "</PHONE_NO>";


						//Reason Code
						//					DecMotorNamedInsuredElement.InnerXml +="<reason_code1 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code1 + "</reason_code1>";
						//					DecMotorNamedInsuredElement.InnerXml +="<reason_code2 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code2 + "</reason_code2>";
						//					DecMotorNamedInsuredElement.InnerXml +="<reason_code3 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code3 + "</reason_code3>";
						//					DecMotorNamedInsuredElement.InnerXml +="<reason_code4 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code4 + "</reason_code4>";

						DecMotorNamedInsuredElement.InnerXml = DecMotorNamedInsuredElement.InnerXml +  "<AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCode + "</AGENCYCODE>";
						DecMotorNamedInsuredElement.InnerXml = DecMotorNamedInsuredElement.InnerXml +  "<AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" +AgencySubCode + "</AGENCYSUBCODE>";
						DecMotorNamedInsuredElement.InnerXml = DecMotorNamedInsuredElement.InnerXml +  "<AGENCYBILLING " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyBilling+ "</AGENCYBILLING>";
						DecMotorNamedInsuredElement.InnerXml = DecMotorNamedInsuredElement.InnerXml +  "<AGENCYBILLING " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyBilling+ "</AGENCYBILLING>";
						DecMotorNamedInsuredElement.InnerXml = DecMotorNamedInsuredElement.InnerXml +  "<DEC_DATE " + fieldType + "=\"" + fieldTypeText + "\">" + DateTime.Now.ToString("MM/dd/yyyy") + "</DEC_DATE>";
						*/				
						#endregion

					}
				}  
			}
			catch(Exception ex)
			{
				throw(new Exception("Error while creating Co-Applicant XML.",ex));
			}
		}
		#endregion
		// policy adjusted premium 
		private double GetEffectivePremium( double OldGrossPremium,double NewGrossPremium)
		{
			double EffectivePremium = 0;
			int TotalPolicyTime = 365;
			int DaysDiff=0;
			
			gobjWrapper.AddParameter("@CUSTOMER_ID",gStrClientID);
			gobjWrapper.AddParameter("@APP_ID",0);
			gobjWrapper.AddParameter("@APP_VERSION_ID",0);
			gobjWrapper.AddParameter("@POLICY_ID",gStrPolicyId);
			gobjWrapper.AddParameter("@POLICY_VERSION_ID",gStrPolicyVersion);
			DataSet dsPolicy = gobjWrapper.ExecuteDataSet("Proc_GetPolicyDetails");
			gobjWrapper.ClearParameteres();

			//DataSet dsPolicy = gobjSqlHelper.ExecuteDataSet("Proc_GetPolicyDetails " + gStrClientID + ",0,0," + gStrPolicyId + "," + gStrPolicyVersion);

			if (dsPolicy.Tables[0].Rows.Count>0)
			{
				TotalPolicyTime= Convert.ToInt32(dsPolicy.Tables[0].Rows[0]["POLICY_DAYS"].ToString());
			}
			Cms.BusinessLayer.BlProcess.ClsEndorsmentProcess objProcess = new	Cms.BusinessLayer.BlProcess.ClsEndorsmentProcess ();

			Cms.Model.Policy.Process.ClsProcessInfo objInfo = objProcess.GetRunningProcess(int.Parse(gStrClientID), int.Parse(gStrPolicyId));
			if (objInfo!=null)
			{
				if ( objInfo.PROCESS_ID == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS)
				{
					DaysDiff = TimeSpan.FromTicks(objInfo.EXPIRY_DATE.Ticks - objInfo.EFFECTIVE_DATETIME.Ticks).Days;

				}
			}
			else
			{
				if (expiry_date != "" && eff_date !="")
					DaysDiff = TimeSpan.FromTicks(Convert.ToDateTime(expiry_date).Ticks - Convert.ToDateTime(eff_date).Ticks).Days;
				else
					DaysDiff =0;
			}
			EffectivePremium = ((NewGrossPremium - OldGrossPremium) / TotalPolicyTime) * DaysDiff;

			return EffectivePremium;
		}
		private double CalculateEffcPremium(string oldPolicyVersion)
		{
			string newVersion = gStrPolicyVersion;
			double sumTtlC=0;
			// Adjusted premium after endorsement
			try
			{
				if(oldPolicyVersion != "" && oldPolicyVersion != "0")
				{
					
					// if policy proccess is not committed
					if(gStrtemp!="final")
					{
						DataSet DSTempRateDataSet = new DataSet();

						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						gobjWrapper.AddParameter("@LOB","CYCL");
						DSTempRateDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFQuoteRateXML");
						gobjWrapper.ClearParameteres();

						//DSTempRateDataSet = new DataWrapper(ConnStr,CommandType.Text).ExecuteDataSet("Proc_GetPDFQuoteRateXML " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "','" + "CYCL" + "'");
						gStrPolicyVersion = oldPolicyVersion;
						// Load quote premium xml
						if(DSTempRateDataSet.Tables[0].Rows.Count>0)
						{
							LoadRateXML("CYCL");
						}
						foreach (XmlNode SumTotalNode in GetSumTotalPremium())
						{
							if(getAttributeValue(SumTotalNode,"STEPPREMIUM")!=null && getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()!="" )
								sumTtlC += double.Parse(getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()) ;						
						}
					}
						//If policy is committed
					else
					{
						DataSet DSTempDatasetBoatDetail = new DataSet();
						DataSet DstrailerPre = new DataSet();
						DataSet DsSchprem = new DataSet();
						gStrPolicyVersion = newVersion;

						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",newVersion);
						DSTempDatasetBoatDetail = gobjWrapper.ExecuteDataSet("PROC_GetpdfAdjusted_Premium");
						gobjWrapper.ClearParameteres();

						//DSTempDatasetBoatDetail = gobjSqlHelper.ExecuteDataSet("PROC_GetpdfAdjusted_Premium " + gStrClientID + "," + gStrPolicyId + "," + newVersion +"");			
						if(DSTempDatasetBoatDetail.Tables[0].Rows[0]["WRITTEN_PREMIUM"].ToString() != null && DSTempDatasetBoatDetail.Tables[0].Rows[0]["WRITTEN_PREMIUM"].ToString() != "")
						{
							dblOldSum=Convert.ToDouble(DSTempDatasetBoatDetail.Tables[0].Rows[0]["WRITTEN_PREMIUM"].ToString());
						}
					}
					if(gStrtemp!="final")
					{
						dblOldSum = sumTtlC ;
					}
					gStrPolicyVersion = newVersion;
					LoadRateXML("CYCL");
				}
			}
			
			catch(Exception ex)
			{
				throw(ex);
			}
			return dblOldSum;
		}
		#region code for MOTOR info Xml
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
		private void createMOTOXML()
		{
			try
			{
				int MotoCtr = 0, Operator_Ctr = 0,cycleCounter=0,intpageno=1;
				double dbEstTotal=0;
				//int operCounttemp=0;
				//string strLimit = "";
				string inspercent = "",strMccaFee="";
				double sumTtl=0;
				//string adjPrem="";
				//double adjpremium=0;
				string strPrem = "";
				DataSet dstemcovcnt = new DataSet();
				XmlElement DecPageRootElement;
				DecPageRootElement = AcordPDFXML.CreateElement("DECPAGEMOTORCYCLEINFO");
			
				XmlElement AcordMotorElement;
				AcordMotorElement = AcordPDFXML.CreateElement("AccordMotorCycleInfo");
				// if Process type is blank
				if(gStrProcessID=="")
				{
					gStrProcessID = GetPolicyProcess( gStrClientID ,gStrPolicyId ,gStrPolicyVersion , gStrCalledFrom,gobjWrapper);
				}
				
				if (gStrPdfFor == PDFForDecPage)
				{
//					if(gStrCopyTo != "AGENCY" && strNeedPage2 == "Y")
//						createPage2XML(ref RootElementDecPage);
				
					prnOrd_covCode = new string[100];
					prnOrd_attFile = new string[100];
					prnOrd = new int[100];
					RootElementDecPage.AppendChild(DecPageRootElement);
					DecPageRootElement.SetAttribute(fieldType,fieldTypeMultiple);
					DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECCYCLE"));
					DecPageRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECCYCLE"));
					DecPageRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECCYCLEEXTN"));
					DecPageRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECCYCLEEXTN"));							
				
				}
				else if(gStrPdfFor == PDFForAcord)
				{
					AcordMotorRootElement.AppendChild(AcordMotorElement);
					AcordMotorElement.SetAttribute(fieldType,fieldTypeMultiple);
					AcordMotorElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORDCYCLE"));
					AcordMotorElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORDCYCLE"));
					AcordMotorElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORDCYCLEEXTN"));
					AcordMotorElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORDCYCLEEXTN"));	

				}
				DataSet dsTempPolicy = new DataSet();

				
					
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFAuto_Details");
				gobjWrapper.ClearParameteres();

				//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAuto_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			

				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				dsTempPolicy = gobjWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
				gobjWrapper.ClearParameteres();

				//dsTempPolicy = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			
				if(DSTempDataSet.Tables[0].Rows.Count>0)
				{	
					// total Policy pages
					inttotalpage +=  DSTempDataSet.Tables[0].Rows.Count;
					
					////////////////
					// Total Policy Pages (Start)
					////////////////
					foreach(DataRow MotoDetail in DSTempDataSet.Tables[0].Rows)
					{
						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						gobjWrapper.AddParameter("@VEHICLEID",MotoDetail["VEHICLE_ID"]);
						gobjWrapper.AddParameter("@RISKTYPE",MotoDetail["MOTORCYCLETYPE"]);
						gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						dstemcovcnt = gobjWrapper.ExecuteDataSet("Proc_GetPDFAuto_Coverage_Details");
						gobjWrapper.ClearParameteres();

						//dstemcovcnt = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAuto_Coverage_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +MotoDetail["VEHICLE_ID"] +  ",'"+ MotoDetail["MOTORCYCLETYPE"] +"','" + gStrCalledFrom + "'");
						//policy and endorsement coverage more than 20
						// get total coverages policy level and vehicle level individually  if any of them more than 10 then increase page value 
						int intplC=0, intrlC=0;
						foreach( DataRow MotoCoverageDetail in 	dstemcovcnt.Tables[0].Rows)
						{
						
							if(MotoCoverageDetail["COVERAGE_TYPE"].ToString() == "PL")
							{
								intplC++;
							}
							else if(MotoCoverageDetail["COVERAGE_TYPE"].ToString() == "RL")
							{
								intrlC++;
							}
						}
						if(intplC > 10 || intrlC> 10)
						{
							inttotalpage++;
						}
						// if additinal interest more than 2

						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						gobjWrapper.AddParameter("@VEHICLEID",MotoDetail["Vehicle_ID"]);
						gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						dstemcovcnt = gobjWrapper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS");
						gobjWrapper.ClearParameteres();

						//dstemcovcnt = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + "," + MotoDetail["Vehicle_ID"] + ",'" + gStrCalledFrom + "'");
						if(dstemcovcnt.Tables[0].Rows.Count > 2)
						{
							inttotalpage++;
						}
						//if assingned operator more than 10

						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						gobjWrapper.AddParameter("@VEHICLEID",MotoDetail["Vehicle_ID"]);
						gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						dstemcovcnt = gobjWrapper.ExecuteDataSet("Proc_GetPDFAuto_AssgnDriv");
						gobjWrapper.ClearParameteres();

						//dstemcovcnt = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAuto_AssgnDriv " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",'" + gStrCalledFrom + "',"  + MotoDetail["Vehicle_ID"]);
						if(dstemcovcnt.Tables[1].Rows.Count > 4)
						{
							inttotalpage++;
						}
					}
					///////////////////////////
					////total Policy Pages (End)
					//////////////////////////

					///////////////////////////////////////
					//  POLICY  Sum Total(start)
					//////////////////////////////////////
					foreach(DataRow MotoDetail in DSTempDataSet.Tables[0].Rows)
					{

						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						gobjWrapper.AddParameter("@VEHICLEID",MotoDetail["VEHICLE_ID"]);
						gobjWrapper.AddParameter("@RISKTYPE",MotoDetail["MOTORCYCLETYPE"]);
						gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						DataSet DSTempVehicle1 = gobjWrapper.ExecuteDataSet("Proc_GetPDFAuto_Coverage_Details");
						gobjWrapper.ClearParameteres();

						//DataSet DSTempVehicle1 = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAuto_Coverage_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +MotoDetail["VEHICLE_ID"] +  ",'"+ MotoDetail["MOTORCYCLETYPE"] +"','" + gStrCalledFrom + "'");
						//double sumTtl=0;
						
						if(gStrtemp != "final")
						{
							foreach (XmlNode SumTotalNode in GetSumTotalPremium(MotoDetail["VEHICLE_ID"].ToString()))
							{
								if(getAttributeValue(SumTotalNode,"STEPPREMIUM")!=null && getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()!="" )
									sumTtl = double.Parse(getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()) ;						
							}
						}
						else 
						{
							string sumtotal = GetPremiumAll(DSTempVehicle1, "SUMTOTAL", MotoDetail["VEHICLE_ID"].ToString());
							if (sumtotal != "")
								sumTtl = double.Parse(sumtotal);
						}
						dbEstTotal +=sumTtl;

						
					}
					///////////////////////////////////////
					// POLICY   Sum Total(end)
					//////////////////////////////////////
					// Policy adjusted premium
					string adjPrm="";
					if(gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS.ToString() || gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS.ToString() 
						|| gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REINSTATEMENT_PROCESS.ToString() || gStrProcessID == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REINSTATEMENT_PROCESS.ToString())
					{
						if(gStrtemp !="final")
						{
							adjPrm = DollarFormat(GetEffectivePremium(CalculateEffcPremium(strOldPolicyVer),dbEstTotal));
						}
						else
						{
							adjPrm = DollarFormat(CalculateEffcPremium(strOldPolicyVer));
						}
						if(adjPrm !="")
							adjPrm = "$" + adjPrm;
					}
				//old space of privacy policy
					foreach(DataRow MotoDetail in DSTempDataSet.Tables[0].Rows)
					{
						#region DecPage Info
						if (gStrPdfFor == PDFForDecPage)
						{
							// Policy Genral Information
							XmlElement MotorDecPageElement;	
							MotorDecPageElement = AcordPDFXML.CreateElement("MotorCycleInfo");
							DecPageRootElement.AppendChild(MotorDecPageElement);
							MotorDecPageElement.SetAttribute(fieldType,fieldTypeNormal);
							MotorDecPageElement.SetAttribute(id,cycleCounter.ToString());
						
//							XmlElement DecPagePolicyAgentElement;
//							DecPagePolicyAgentElement = AcordPDFXML.CreateElement("POLICY");
//							RootElementDecPage.AppendChild(DecPagePolicyAgentElement);
//							DecPagePolicyAgentElement.SetAttribute(fieldType,fieldTypeSingle);
//							DecPagePolicyAgentElement.SetAttribute(id,MotoCtr.ToString());


							//Privacy Policy Page
							if(cycleCounter==0)
							{
								#region Policy Notice Page
								if(gStrPdfFor == PDFForDecPage)
								{
									////////////////////////////////////////////////////////////
									// Policy Notice and Adverse Section page (start) 
									///////////////////////////////////////////////////////////
									// adverse letter
									string strAdverseRep="";
									DataSet dsTempApplcant = new DataSet();

									gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
									gobjWrapper.AddParameter("@POLID",gStrPolicyId);
									gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
									gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
									dsTempApplcant = gobjWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
									gobjWrapper.ClearParameteres();

									//dsTempApplcant = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
									strAdverseRep = dsTempApplcant.Tables[1].Rows[0]["ADVERSE_LETTER_REQD"].ToString();
					
									//check for diffferent process
									if(gStrCopyTo != "AGENCY")
									{
										if(gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS.ToString() && gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS.ToString() 
											&& gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REWRITE_PROCESS.ToString() && gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REWRITE_PROCESS.ToString()
											&& gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REINSTATEMENT_PROCESS.ToString() && gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REINSTATEMENT_PROCESS.ToString()
											&& gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RENEWAL_PROCESS.ToString() && gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RENEWAL_PROCESS.ToString()
											&& gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_NEW_BUSINESS_PROCESS.ToString() && gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_NEW_BUSINESS_PROCESS.ToString())
										{
											if(gStrCopyTo != "AGENCY" && strNeedPage2 == "Y")
												createPage2XML(ref MotorDecPageElement);
										}
										else if(gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS.ToString() || gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS.ToString() )
										{
											//store preassigned value of strNeedPage2
											string elginsusc=strNeedPage2;
											if(gStrCopyTo != "AGENCY" && strAdverseRep=="Y" && strNeedPage2 == "Y" && strInsScore !="-2")
											{
												ChkPreInsuScr();
												if(newInsuScr == oldInsuScr)
												{

												}
												else
												{
													if(oldInsuScr !="-2")
													{
														// refernce itrack 3222 if fallen in lower tier then print
														if(strNeedPage2 =="Y")
															createPage2AdverseXML(ref MotorDecPageElement);
													}
													else
													{
														createPage2AdverseXML(ref MotorDecPageElement);
													}
												}
							
											}
											else if(gStrCopyTo != "AGENCY"  && strAdverseRep=="Y" && strNeedPage2 == "Y" && strInsScore =="-2")
											{
												//call ChkPreInsuScr function to assign values to newInsuScr,oldInsuScr
												ChkPreInsuScr();
												if(newInsuScr == oldInsuScr)
												{

												}
												else
												{
													if(elginsusc=="Y")
														createPage2NHNSAdverseXML(ref MotorDecPageElement);
												}
											}

										}
										else if(gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RENEWAL_PROCESS.ToString() || gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RENEWAL_PROCESS.ToString())
										{
											//store preassigned value of strNeedPage2
											string elginsusc=strNeedPage2;
											// if insurance score is not no hit no score
											if(gStrCopyTo != "AGENCY" && strNeedPage2 == "Y" && strInsScore !="-2")
											{

												ChkPreInsuScr();
												if(newInsuScr == oldInsuScr)
												{

												}
												else
												{
													if(oldInsuScr !="-2")
													{
														// refernce itrack 3222 if fallen in lower tier then print
														if(strNeedPage2 =="Y")
															createPage2AdverseXML(ref MotorDecPageElement);
													}
													else
													{
														createPage2AdverseXML(ref MotorDecPageElement);
													}
												}
											}
												//if insuracne score is no hit no score
											else if(gStrCopyTo != "AGENCY" && strNeedPage2 == "Y" && strInsScore =="-2")
											{
												//call ChkPreInsuScr function to assign values to newInsuScr,oldInsuScr
												ChkPreInsuScr();
												if(newInsuScr == oldInsuScr)
												{

												}
												else
												{
													if(elginsusc=="Y")
														createPage2NHNSAdverseXML(ref MotorDecPageElement);
												}
											}
										}
										else if(gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_NEW_BUSINESS_PROCESS.ToString() || gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_NEW_BUSINESS_PROCESS.ToString())
										{
											if(gStrCopyTo != "AGENCY" && strNeedPage2 == "Y")
											{
												createPage2XML(ref MotorDecPageElement);
											}
											else if( strNeedPage2 == "N")
											{
												createPage2PrivacyPageXML(ref MotorDecPageElement);
											}
										}
										else if(gStrProcessID == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REWRITE_PROCESS.ToString() || gStrProcessID == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REWRITE_PROCESS.ToString())
										{
											if( strNeedPage2 == "N")
											{
												createPage2PrivacyPageXML(ref MotorDecPageElement);
											}
											else
											{
												createPage2XML(ref MotorDecPageElement);
											}
										}
										else
										{
											if(gStrCopyTo != "AGENCY" && strNeedPage2 == "Y")
											{
												createPage2AdverseXML(ref MotorDecPageElement);
											}
										}
									}
									////////////////////////////////////////////////////////////////////
									///// Policy Notice and Adverse Section page (End) 
									///////////////////////////////////////////////////////////
			
								}
								#endregion 
							}
							if(gStrCalledFrom.Equals(CalledFromPolicy))
							{
								MotorDecPageElement.InnerXml +=   "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(PolicyNumber) + "</POLICYNUMBER>";
								MotorDecPageElement.InnerXml +=   "<POLICYVERSION " + fieldType + "=\"" + fieldTypeText + "\">" + gStrPolicyVersion + "</POLICYVERSION>";
								MotorDecPageElement.InnerXml +=   "<POLICYEFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(PolicyEffDate) + "</POLICYEFFDATE>";
								MotorDecPageElement.InnerXml +=   "<POLICYEXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(PolicyExpDate) + "</POLICYEXPDATE>";
								MotorDecPageElement.InnerXml +=   "<REASON " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Reason) + "</REASON>";
							}
							// Moto page No
							MotorDecPageElement.InnerXml +="<PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " + "of" +" " +  inttotalpage + "</PAGE_NO>";
							
							//Moto
							MotorDecPageElement.InnerXml +=   "<copyTo " + fieldType + "=\"" + fieldTypeText + "\">" + CopyTo + "</copyTo>";
							MotorDecPageElement.InnerXml +=   "<INSUREDNAME " + fieldType + "=\"" + fieldTypeText + "\">" + NamedInsuredWithSuffix + "</INSUREDNAME>";
							MotorDecPageElement.InnerXml +=   "<INSUREDADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsTempPolicy.Tables[0].Rows[0]["APPADDRESS"].ToString()) + "</INSUREDADDRESS>";
							MotorDecPageElement.InnerXml +=   "<INSUREDCITYSTZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(dsTempPolicy.Tables[0].Rows[0]["APPCITYSTZIP"].ToString()) + "</INSUREDCITYSTZIP>";
							MotorDecPageElement.InnerXml +=   "<AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyName + "</AGENCYNAME>";
							MotorDecPageElement.InnerXml +=   "<AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyAddress + "</AGENCYADDRESS>";
							MotorDecPageElement.InnerXml +=   "<AGENCYCITYSTZIP " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCitySTZip + "</AGENCYCITYSTZIP>";
							MotorDecPageElement.InnerXml +=   "<AGENCYPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyPhoneNumber + "</AGENCYPHONE>";
							MotorDecPageElement.InnerXml +=   "<AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCode + "</AGENCYCODE>";
							MotorDecPageElement.InnerXml +=   "<AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" +AgencySubCode + "</AGENCYSUBCODE>";
							MotorDecPageElement.InnerXml +=   "<AGENCYBILLING " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyBilling+ "</AGENCYBILLING>";
							
							gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
							gobjWrapper.AddParameter("@POLID",gStrPolicyId);
							gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
							gobjWrapper.AddParameter("@VEHICLEID",MotoDetail["VEHICLE_ID"]);
							gobjWrapper.AddParameter("@RISKTYPE",MotoDetail["MOTORCYCLETYPE"]);
							gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
							DSTempVehicle = gobjWrapper.ExecuteDataSet("Proc_GetPDFAuto_Coverage_Details");
							gobjWrapper.ClearParameteres();
					
							//DSTempVehicle = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAuto_Coverage_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +MotoDetail["VEHICLE_ID"] +  ",'"+ MotoDetail["MOTORCYCLETYPE"] +"','" + gStrCalledFrom + "'");
						
							#region Cycle Info

							sumTtl=0;
							htpremium.Clear(); 
							if(gStrtemp != "final")
							{
								foreach (XmlNode PremiumNode in GetAutoPremium(MotoDetail["VEHICLE_ID"].ToString()))
								{
									if(!htpremium.Contains(getAttributeValue(PremiumNode,"COMPONENT_CODE")))
										htpremium.Add(getAttributeValue(PremiumNode,"COMPONENT_CODE"),getAttributeValue(PremiumNode,"STEPPREMIUM"));
								}
							}
							///////////////////////////////////////
							//    Sum Total(start)
							//////////////////////////////////////
							if(gStrtemp == "temp")
							{
								foreach (XmlNode SumTotalNode in GetSumTotalPremium(MotoDetail["VEHICLE_ID"].ToString()))
								{
									if(getAttributeValue(SumTotalNode,"STEPPREMIUM")!=null && getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()!="" )
										sumTtl += double.Parse(getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()) ;						
								}
							}
							else
							{
								string sumtotal = GetPremiumAll(DSTempVehicle, "SUMTOTAL", MotoDetail["VEHICLE_ID"].ToString());
								if (sumtotal != "")
									sumTtl += double.Parse(sumtotal);
							}
							///////////////////////////////////////
							//    Sum Total(end)
							//////////////////////////////////////
							////////////////////////////////////////////
							// Michigan Statutory Assessment add it on BI (Start)
							////////////////////////////////////////////
							// if Process not committed
							if(gStrtemp !="final")
							{
								strMccaFee="0";
								foreach(XmlNode MotoNode in GetMCCAFee(MotoDetail["VEHICLE_ID"].ToString()))
								{
									if(getAttributeValue(MotoNode,"STEPPREMIUM")!=null && getAttributeValue(MotoNode,"STEPPREMIUM").ToString()!="" )
									{
										strMccaFee = getAttributeValue(MotoNode,"STEPPREMIUM").ToString();						
													
									}
								}
							}
								//if process commited
							else
							{
								strMccaFee="0";
								foreach(DataRow CoverageDetails in DSTempVehicle.Tables[1].Rows)
								{
									if(CoverageDetails["COMPONENT_CODE"].ToString() == "MCCAFEE")
										strMccaFee = CoverageDetails["COVERAGE_PREMIUM"].ToString();
								}
							}
							// MCCA FEE FOR AGENT'S COPY (ACCOUNTING PURPOSE)
							if(gStrCopyTo == "AGENCY" && strMccaFee!="")
							{
								MotorDecPageElement.InnerXml= MotorDecPageElement.InnerXml +  "<AGENCYACCOUNTINFORMATION  " + fieldType +"=\""+ fieldTypeText +"\">"+"**Account Information - "+"$"+RemoveJunkXmlCharacters(DollarFormat(double.Parse(strMccaFee.ToString()))) + "**"+"</AGENCYACCOUNTINFORMATION>";
							}
							////////////////////////////////////////////
							// Michigan Statutory Assessment add it on BI (End)
							////////////////////////////////////////////
							///
							// Insurance Score
							XmlNodeList ins_scoreNodeList = GetCreditForMotInsScore();
							XmlNode ins_scoreNode;
							if(ins_scoreNodeList.Count > 0)
							{
								ins_scoreNode = ins_scoreNodeList.Item(0);
								String [] discRows = getAttributeValue(ins_scoreNode,"STEPDESC").Split('-');

								if(discRows.Length >= 1)
									inspercent = discRows[discRows.Length -1];
							}
							//No hit No Score
							if(strInsScore == "-2")
							{
								strInsScore = "No Hit/No Score";
							}
							//-1
							if(strInsScore == "-1")
							{
								strInsScore = "";
							}
						
							//Get Policy Motorcycle Information
																																						
							MotorDecPageElement.InnerXml +=   "<MOTORDESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(MotoDetail["VEHICLE_ID"].ToString()) + "  "  + RemoveJunkXmlCharacters(MotoDetail["VEHICLE_YEAR"].ToString())+"/"+RemoveJunkXmlCharacters(MotoDetail["MAKE"].ToString())+"/"+RemoveJunkXmlCharacters(MotoDetail["MODEL"].ToString())+ "/" +RemoveJunkXmlCharacters(MotoDetail["VIN"].ToString())+"     "+ RemoveJunkXmlCharacters(MotoDetail["VEHICLE_CC"].ToString() +" CC's") + "</MOTORDESCRIPTION>";
							MotorDecPageElement.InnerXml +=  "<TERRITORY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(MotoDetail["TERRITORY"].ToString()) + "</TERRITORY>";
							MotorDecPageElement.InnerXml +=   "<INSURANCESCORE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strInsScore) + "</INSURANCESCORE>";
							MotorDecPageElement.InnerXml +=   "<INSURANCEDISCOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(inspercent) + "</INSURANCEDISCOUNT>";
							MotorDecPageElement.InnerXml +=   "<INSURANCETYPE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strInsType) + "</INSURANCETYPE>";
							MotorDecPageElement.InnerXml +=   "<VEHICLECLASS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(MotoDetail["MOTORCYCLE_CLASS"].ToString()) + "</VEHICLECLASS>";
							MotorDecPageElement.InnerXml +=   "<VEHICLEAGE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(MotoDetail["VEHICLE_AGE"].ToString()) + "</VEHICLEAGE>";
							MotorDecPageElement.InnerXml +=   "<MOTORCYCLETYPE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(MotoDetail["LOOKUP_VALUE_DESC"].ToString()) + "</MOTORCYCLETYPE>";
							MotorDecPageElement.InnerXml +=   "<VEHICLESYMBOL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(MotoDetail["SYMBOL"].ToString()) + "</VEHICLESYMBOL>";
							MotorDecPageElement.InnerXml +=   "<GARAGEADD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(MotoDetail["GRG_ADD"].ToString()) + "</GARAGEADD>";
							MotorDecPageElement.InnerXml +=   "<GARAGECITY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(MotoDetail["GRG_CITYSTZIP"].ToString()) + "</GARAGECITY>";
							MotorDecPageElement.InnerXml +=   "<DATE " + fieldType + "=\"" + fieldTypeText + "\">" + DateTime.Now.ToString("MM/dd/yyyy") + "</DATE>";
							#region COVERAGES
							//To calculate Estimated Total

							#region setting Root liability coverages Attribute
							XmlElement LCRootElement;
							LCRootElement = AcordPDFXML.CreateElement("LIABCOVERAGES");
							MotorDecPageElement.AppendChild(LCRootElement);
							LCRootElement.SetAttribute(fieldType,fieldTypeMultiple);
							LCRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECLBLCOVERAGE"));
							LCRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECLBLCOVERAGE"));
							LCRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECLBLCOVERAGEEXTN"));
							LCRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECLBLCOVERAGEEXTN"));
							#endregion

							chkFlag17=false;chkFlag=false;chkFlag16=false;chkFlag14=false;
                            string CovLimit = "";//SecCoveLimit = "",
							string strDec = "";
							//string strDectxt = "";

                            int lcCtr = 0;//intliacov=0,
							string flgEdit="";
							foreach(DataRow dtRow in DSTempVehicle.Tables[0].Rows)
							{
								if(dtRow["COV_CODE"].ToString()=="RRUMM")
									flgEdit="Y";
							}
							foreach(DataRow CoverageDetails in DSTempVehicle.Tables[0].Rows)
							{
						
								#region Policy Level Coverages for Dec Page
								if (gStrPdfFor == PDFForDecPage)
								{
									if((CoverageDetails["COVERAGE_TYPE"].ToString().ToUpper() == "PL" || CoverageDetails["COV_CODE"].ToString().ToUpper() == "MEDPM1" || CoverageDetails["COV_CODE"].ToString().ToUpper() == "MEDPM" || CoverageDetails["COV_CODE"].ToString().ToUpper() == "MEDPM2") && CoverageDetails["COV_CODE"].ToString().ToUpper() != "MPEMC" && CoverageDetails["COV_CODE"].ToString().ToUpper() != "M10" && (CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString()!="Reject" && CoverageDetails["LIMIT2_AMOUNT_TEXT"].ToString()!="Reject"))
									{
										XmlElement LCElement;
										LCElement = AcordPDFXML.CreateElement("LIABCOVERAGESINFO");
										LCRootElement.AppendChild(LCElement);
										LCElement.SetAttribute(fieldType,fieldTypeNormal);
										LCElement.SetAttribute(id,lcCtr.ToString());

										string covDes = "",strLimitText="";
										
										string CovCode = CoverageDetails["COV_CODE"].ToString();
										
										if(CovCode == "MEDPM1")
										{
											covDes=CoverageDetails["COV_DES"].ToString();
											if(covDes!="")
											{
												//1st Party Medical-Excess
												//if(covDes.IndexOf("-")>=0)
												//covDes=covDes.Substring(0,covDes.IndexOf("-")-1);
												if(CoverageDetails["limit1_amount_text"].ToString()!="")
													strLimitText=CoverageDetails["limit1_amount_text"].ToString();
												if(strLimitText.IndexOf("-")>=0)
													strLimitText=strLimitText.Substring(strLimitText.IndexOf("-"),strLimitText.Length-strLimitText.IndexOf("-"));
												if(strLimitText.IndexOf("-")>=0)
													strLimitText=strLimitText.Replace("-","");
												if(strLimitText!="$300" && strLimitText!="")
													covDes=covDes+" ("+strLimitText+")";
											}
										}
										//PAGE INFO
										if(lcCtr >= 11 && lcCtr < 12 )
										{
											intpageno++;
										}
						
										LCElement.InnerXml +="<AT_LC_PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " + "of" +" " +  inttotalpage + "</AT_LC_PAGE_NO>";
										if(flgEdit=="Y")
											LCElement.InnerXml +="<AH_LC_EDITIONDATE " + fieldType + "=\"" + fieldTypeText + "\">" + "Edition Date" +  "</AH_LC_EDITIONDATE>";
										// Coverage Limit
										CovLimit = CoverageDetails["LIMIT_1"].ToString();
										if(CoverageDetails["LIMIT_2"].ToString() != "" && CovLimit != "")
										{
											CovLimit=GetIntFormat(CovLimit +  "000");
											CovLimit += "/" + GetIntFormat(CoverageDetails["LIMIT_2"].ToString());										
										}
										else
										{
											CovLimit += CoverageDetails["LIMIT_2"].ToString();
											CovLimit=GetIntFormat(CovLimit);
										}
										//Coverage Deductible
										strDec = CoverageDetails["DEDUCTIBLE_1"].ToString();
										if(strDec=="0" || strDec=="$0.00" || strDec=="$0" || strDec=="0.00" || strDec=="")
											strDec="";
										else									
											strDec = GetIntFormat(strDec);
										// formating for Premium
										if(gStrtemp == "temp")
										{
											strPrem=GetPremiumBeforeCommit(DSTempVehicle,CovCode,htpremium);
								
											if(CovCode.Trim() == "BISPL" || CovCode.Trim() == "RLCSL")
											{
												if(strPrem !="")
												{
													strPrem = System.Convert.ToString(int.Parse(strPrem.Replace(".00","")) + int.Parse(strMccaFee.Replace(".00","")));
													strPrem = strPrem + ".00";
												}
											}
											if(strPrem !="")
											{
												strPrem = "$" + strPrem;
											}
										}
										else
										{
											strPrem=GetPremium(DSTempVehicle,CovCode);
											if(CovCode.Trim() == "BISPL" || CovCode.Trim() == "RLCSL")
											{
												if(strPrem !="")
												{
													strPrem = System.Convert.ToString(int.Parse(strPrem.Replace(".00","")) + int.Parse(strMccaFee.Replace(".00","")));
													strPrem = strPrem + ".00";
												}
											}
											if(strPrem !="")
											{
												strPrem = "$" + strPrem;
											}
										}
										LCElement.InnerXml += "<MOTORDESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(MotoDetail["VEHICLE_ID"].ToString()) + "  "  + RemoveJunkXmlCharacters(MotoDetail["VEHICLE_YEAR"].ToString())+"/"+RemoveJunkXmlCharacters(MotoDetail["MAKE"].ToString())+"/"+RemoveJunkXmlCharacters(MotoDetail["MODEL"].ToString())+ "/" +RemoveJunkXmlCharacters(MotoDetail["VIN"].ToString())+"     "+ RemoveJunkXmlCharacters(MotoDetail["VEHICLE_CC"].ToString() +" CC's") + "</MOTORDESCRIPTION>";
										LCElement.InnerXml +="<MOTO_PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " +  "of" + " " + inttotalpage + "</MOTO_PAGE_NO>";
							
										// Individual Mapping of policy level coverages
										//if (gStrPdfFor == PDFForDecPage && (covCode.Equals("RLCSL") || covCode.Equals("PUNCS") ||covCode.Equals("UNCSL") || covCode.Equals("BISPL") ||covCode.Equals("PD") || covCode.Equals("PUMSP") ||covCode.Equals("MEDPM")|| covCode.Equals("MEDPM1") || covCode.Equals("MEDPM2") || covCode.Equals("E17")))						
										//{
										#region SWITCH CASE
										switch(CoverageDetails["COV_CODE"].ToString())
										{
												//Single Limits Liability (CSL)
											case "RLCSL":
											{
												LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_DESCRIPTION>";
												LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LIMIT>";							
												LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";	
												LCElement.InnerXml +="<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";							
												lcCtr++;
												break;
											}
												//Uninsured Motorists (CSL)
											case "PUNCS":
											{
												LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_DESCRIPTION>";
												LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LIMIT>";							
												LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";							
												LCElement.InnerXml +="<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";							
												lcCtr++;
												break;
											}
												//Underinsured Motorists (CSL) (M-16) 
											case "UNCSL":
											{
												LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_DESCRIPTION>";
												LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LIMIT>";							
												LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";							
												LCElement.InnerXml +="<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";							
												lcCtr++;
												
												#region Dec Page Element
												/*if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" && chkFlag16 != true)	
												{												
													XmlElement DecPageMotorCycleEndoUNCSL;
													DecPageMotorCycleEndoUNCSL = AcordPDFXML.CreateElement("MOTORCYCLEENDORSEMENTUNCSL");
													MotorDecPageElement.AppendChild(DecPageMotorCycleEndoUNCSL);
													DecPageMotorCycleEndoUNCSL.SetAttribute(fieldType,fieldTypeMultiple);
													if(CoverageDetails["ATTACH_FILE"] != null && CoverageDetails["ATTACH_FILE"].ToString() != "")
													{
														DecPageMotorCycleEndoUNCSL.SetAttribute(PrimPDF,CoverageDetails["ATTACH_FILE"].ToString());
														DecPageMotorCycleEndoUNCSL.SetAttribute(PrimPDFBlocks,"1");
													}
													else
													{
														DecPageMotorCycleEndoUNCSL.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEUNCSL"));
														DecPageMotorCycleEndoUNCSL.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEUNCSL"));
													}
													DecPageMotorCycleEndoUNCSL.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEUNCSLEXTN"));
													DecPageMotorCycleEndoUNCSL.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEUNCSLEXTN"));

													XmlElement EndoElementUNCSL;
													EndoElementUNCSL = AcordPDFXML.CreateElement("ENDOELEMENTUNCSLINFO");
													DecPageMotorCycleEndoUNCSL.AppendChild(EndoElementUNCSL);
													EndoElementUNCSL.SetAttribute(fieldType,fieldTypeNormal);
													EndoElementUNCSL.SetAttribute(id,"0");
													chkFlag16 = true;
												}*/	
												#endregion 
												break;
											}
												//Bodily Injury Liability (Split Limit) 
											case "BISPL":
											{
												LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_DESCRIPTION>";
												LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LIMIT>";							
												LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";
												LCElement.InnerXml +="<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";							
												lcCtr++;
												break;
											}
												//Property Damage Liability 
											case "PD":
											{
												LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_DESCRIPTION>";
												LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LIMIT>";							
												LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";		
												LCElement.InnerXml +="<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";							
												lcCtr++;
												break;
											}
												//Uninsured Motorists (BI Split Limit)
											case "PUMSP":
											{
												LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_DESCRIPTION>";
												LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LIMIT>";							
												LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";							
												LCElement.InnerXml +="<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";							
												lcCtr++;
												break;
											}
												//Medical Payments -1st Party
											case "MEDPM1":
											{
												LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covDes)+"</AT_DESCRIPTION>";
												LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LIMIT>";	
												if(strDec!="")
													LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_DEDUCTIBLE>";							
												else
													LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";							
												LCElement.InnerXml +="<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";							
												lcCtr++;
												break;
											}
												//Medical Payments 
											case "MEDPM":
											{
												if(stCode=="IN")
												{
													if(CoverageDetails["limit1_amount_text"].ToString()!="" )
														LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString() + " ("+ CoverageDetails["limit1_amount_text"].ToString()+")")+"</AT_DESCRIPTION>";							
													else
														LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_DESCRIPTION>";
													LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LIMIT>";							
													LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";							
													LCElement.InnerXml +="<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";							
													lcCtr++;
												}	
												break;
											}
												//Uninsured Motorist PD (M-17)
											case "E17":
											{
												LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_DESCRIPTION>";
												LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LIMIT>";							
												LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_DEDUCTIBLE>";
												LCElement.InnerXml +="<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";							
												lcCtr++;
												#region Dec Page Element
												/*if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" && chkFlag17 != true)	
												{												
													XmlElement DecPageMotorCycleEndoE17;
													DecPageMotorCycleEndoE17 = AcordPDFXML.CreateElement("MOTORCYCLEENDORSEMENTE17");
													MotorDecPageElement.AppendChild(DecPageMotorCycleEndoE17);
													DecPageMotorCycleEndoE17.SetAttribute(fieldType,fieldTypeMultiple);
													if(CoverageDetails["ATTACH_FILE"] != null && CoverageDetails["ATTACH_FILE"].ToString() != "")
													{
														DecPageMotorCycleEndoE17.SetAttribute(PrimPDF,CoverageDetails["ATTACH_FILE"].ToString());
														DecPageMotorCycleEndoE17.SetAttribute(PrimPDFBlocks,"1");
													}
													else
													{
														DecPageMotorCycleEndoE17.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEE17"));
														DecPageMotorCycleEndoE17.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEE17"));
													}
													DecPageMotorCycleEndoE17.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEE17EXTN"));
													DecPageMotorCycleEndoE17.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEE17EXTN"));

													XmlElement EndoElementE17;
													EndoElementE17 = AcordPDFXML.CreateElement("ENDOELEMENTE17INFO");
													DecPageMotorCycleEndoE17.AppendChild(EndoElementE17);
													EndoElementE17.SetAttribute(fieldType,fieldTypeNormal);
													EndoElementE17.SetAttribute(id,"0");
													chkFlag17 = true;
												}*/
												#endregion Dec Page Element
												break;
											}
												// Medical Payment - Option 2
											case "MEDPM2":
											{
												LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_DESCRIPTION>";
												LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LIMIT>";							
												LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_DEDUCTIBLE>";							
												LCElement.InnerXml +="<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";							
												lcCtr++;
												break;
											}
												//Underinsured Motorists (BI Split Limit) (M-16) 
											case "UNDSP":
											{
												LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_DESCRIPTION>";
												LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LIMIT>";
												LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";							
												LCElement.InnerXml +="<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";							
												lcCtr++;
												break;	
											}
												//Notice of Option to Reject or Modify A-9 
											case "RRUMM":
												LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString()+"                                            "+CoverageDetails["EDITION_DATE"].ToString())+"</AT_DESCRIPTION>";
												LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LIMIT>";
												LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";							
												LCElement.InnerXml +="<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_PREMIUM>";							
												lcCtr++;
												break;	
												//Uninsured Motorists (PD) 
											case "UMPD":
												LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_DESCRIPTION>";
												LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LIMIT>";
												if(strDec =="0" || strDec =="" || strDec =="0.00" || strDec =="$0" || strDec =="$0.00")
													LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";							
												else
													LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_DEDUCTIBLE>";							
												LCElement.InnerXml +="<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";							
												lcCtr++;
												break;
											default :
												covDes = CoverageDetails["COV_DES"].ToString();
												if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString() != "")
													covDes += " - " + CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString();
												LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covDes)+"</AT_DESCRIPTION>";
												if(CovLimit =="0" || CovLimit =="" || CovLimit =="0.00" || CovLimit =="$0" || CovLimit =="$0.00")
													LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LIMIT>";
												else
													LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LIMIT>";
												if(strDec =="0" || strDec =="" || strDec =="0.00" || strDec =="$0" || strDec =="$0.00")
													LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";
												else
													LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_DEDUCTIBLE>";
												if(strPrem =="0" || strPrem =="" || strPrem =="0.00" || strPrem =="$0" || strPrem =="$0.00")
													LCElement.InnerXml +="<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_PREMIUM>";
												else
													LCElement.InnerXml +="<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";							
												lcCtr++;
												break;

										}
										#endregion 
									}
								}
								#endregion Policy Level Coverages for Dec Page
							}
							chkFlag17=false;chkFlag=false;chkFlag16=false;chkFlag14=false;
							//Modified by Mohit Agarwal 5-Nov-07 for Endorsements Print Order
							if (gStrPdfFor == PDFForDecPage)
							{
								foreach(DataRow CoverageDetails in DSTempVehicle.Tables[0].Rows)
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
							}
							// Additinal Form And Coverages
							//							foreach(DataRow CoverageDetails in DSTempVehicle.Tables[0].Rows)
							//							{
							#region DEC PAGE Additional Forms and Endorsements
							XmlElement DecPageMOTOEndmts;
							DecPageMOTOEndmts= AcordPDFXML.CreateElement("ENDORSEMENTS");

							MotorDecPageElement.AppendChild(DecPageMOTOEndmts);
							DecPageMOTOEndmts.SetAttribute(fieldType,fieldTypeMultiple);
							DecPageMOTOEndmts.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEMOTOEND"));
							DecPageMOTOEndmts.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEMOTOEND"));
							DecPageMOTOEndmts.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEMOTOENDEXTN"));
							DecPageMOTOEndmts.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEMOTOENDEXTN"));
						


							//#region SWITCH CASE FOR DEC PAGE
							//	DSTempVehicle = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAuto_Coverage_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +MotoDetail["VEHICLE_ID"] +  ",'"+ MotoDetail["RISKTYPE"] +"','" + gStrCalledFrom + "'");
					
							int decEndCtr=0;
							//string decstrCtr="";
							foreach(DataRow CoverageDetails in DSTempVehicle.Tables[0].Rows)
							{
								string CovCode=CoverageDetails["COV_CODE"].ToString();
								string covDes="";
								//string strMccaFee="";
								if(CovCode!="RRUMM"  && CovCode!="UNDSP" && CovCode!="MEDPM2" && CovCode!="E17" && CovCode!="MEDPM" && CovCode!="MEDPM1" && CovCode!="PUMSP" && CovCode!="PD" && CovCode!="BISPL" && CovCode!="UNCSL" && CovCode!="PUNCS" && CovCode!="RLCSL")
								{
									// pageINFO
									if(lcCtr <= 10)
									{
										if(decEndCtr >=11 && decEndCtr < 12)
										{
											intpageno++;
										}
									}
									// Coverage Limit	
									CovLimit = CoverageDetails["LIMIT_1"].ToString();
									if(CoverageDetails["LIMIT_2"].ToString() != "" && CovLimit != "")
									{
										CovLimit=GetIntFormat(CovLimit +  "000");
										CovLimit += "/" + GetIntFormat(CoverageDetails["LIMIT_2"].ToString());										
									}
									else
									{
										CovLimit += CoverageDetails["LIMIT_2"].ToString();
										CovLimit=GetIntFormat(CovLimit);
									}
									//Coverage Deductible
									strDec = CoverageDetails["DEDUCTIBLE_1"].ToString();
									if(strDec=="0" || strDec=="$0.00" || strDec=="$0" || strDec=="0.00" || strDec=="")
										strDec="";
									else									
										strDec = GetIntFormat(strDec);
									// formating for Premium
									if(gStrtemp == "temp")
									{
										strPrem=GetPremiumBeforeCommit(DSTempVehicle,CovCode,htpremium);
								
										if(CovCode.Trim() == "BI")
										{
											if(strPrem !="")
											{
												strPrem = System.Convert.ToString(int.Parse(strPrem.Replace(".00","")) + int.Parse(strMccaFee.Replace(".00","")));
												strPrem = strPrem + ".00";
											}
										}
										if(strPrem !="")
										{
											strPrem = "$" + strPrem;
										}
									}
									else
									{
										strPrem=GetPremium(DSTempVehicle,CovCode);
										if(CovCode.Trim() == "BI")
										{
											if(strPrem !="")
											{
												strPrem = System.Convert.ToString(int.Parse(strPrem.Replace(".00","")) + int.Parse(strMccaFee.Replace(".00","")));
												strPrem = strPrem + ".00";
											}
										}
										if(strPrem !="" && strPrem.ToUpper() !="INCLUDED")
										{
											strPrem = "$" + strPrem;
										}
									}
						
									
									#region Endorsement Elements
							
							
									XmlElement DecpageAutoEndmtElement;
									DecpageAutoEndmtElement = AcordPDFXML.CreateElement("ENDORSEMENTSINFO");
									DecPageMOTOEndmts.AppendChild(DecpageAutoEndmtElement);
									DecpageAutoEndmtElement.SetAttribute(fieldType,fieldTypeNormal);
									DecpageAutoEndmtElement.SetAttribute(id,decEndCtr.ToString());
										
									if(lcCtr >= 9)
									{
										LCRootElement.AppendChild(DecpageAutoEndmtElement);
									}
									else
									{
										DecPageMOTOEndmts.AppendChild(DecpageAutoEndmtElement);
									}

									#endregion
									DecpageAutoEndmtElement.InnerXml += "<MOTORDESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(MotoDetail["VEHICLE_ID"].ToString()) + "  "  + RemoveJunkXmlCharacters(MotoDetail["VEHICLE_YEAR"].ToString())+"/"+RemoveJunkXmlCharacters(MotoDetail["MAKE"].ToString())+"/"+RemoveJunkXmlCharacters(MotoDetail["MODEL"].ToString())+ "/" +RemoveJunkXmlCharacters(MotoDetail["VIN"].ToString())+"     "+ RemoveJunkXmlCharacters(MotoDetail["VEHICLE_CC"].ToString() +" CC's") + "</MOTORDESCRIPTION>";
//									DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
//									if(CoverageDetails["COV_CODE"].ToString()=="M10")
//									{
//									}
//									else
//										DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
									
//									DecpageAutoEndmtElement.InnerXml +="<AT_LC_PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " + "of" +" " +  inttotalpage + "</AT_LC_PAGE_NO>";	
									#region ADDITIONAL FORMS AND COVERAGES 
									switch(CoverageDetails["COV_CODE"].ToString())
									{
											//Other Than Collision (Comprehensive)
										case "OTC":
										{
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
											DecpageAutoEndmtElement.InnerXml +="<MOTO_PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " + "of" +" " +  inttotalpage + "</MOTO_PAGE_NO>";	
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_LC_DEDUCTIBLE>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
											decEndCtr++;
										}
											break;
											//Collision
										case "COLL":
										{
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
											DecpageAutoEndmtElement.InnerXml +="<MOTO_PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " + "of" +" " +  inttotalpage + "</MOTO_PAGE_NO>";	
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_LC_DEDUCTIBLE>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
											decEndCtr++;
										}
											break;
											//
										case "ROAD":
										{
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
											DecpageAutoEndmtElement.InnerXml +="<MOTO_PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " + "of" +" " +  inttotalpage + "</MOTO_PAGE_NO>";	
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LC_LIMIT>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
											decEndCtr++;
										}
											break;
											//Helmet & Riding Apparel Coverage (M-15)
										case "EBM15":
										{
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
											DecpageAutoEndmtElement.InnerXml +="<MOTO_PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " + "of" +" " +  inttotalpage + "</MOTO_PAGE_NO>";	
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LC_LIMIT>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
											decEndCtr++;
										}
											break;
											//Motorcycle Trailer - M-49 Other than Collision 
										case "EBM49":
										{
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
											DecpageAutoEndmtElement.InnerXml +="<MOTO_PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " + "of" +" " +  inttotalpage + "</MOTO_PAGE_NO>";	
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LC_LIMIT>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_LC_DEDUCTIBLE>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
											decEndCtr++;
										}
											break;
											//Motorcycle Trailer - M-49 Collision  
										case "CEBM49":
										{
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
											DecpageAutoEndmtElement.InnerXml +="<MOTO_PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " + "of" +" " +  inttotalpage + "</MOTO_PAGE_NO>";	
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_LC_DEDUCTIBLE>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
											decEndCtr++;
										}
											break;
											//Additional Physical Damage Coverage (M-14)
										case "PDC14":
										{
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
											DecpageAutoEndmtElement.InnerXml +="<MOTO_PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " + "of" +" " +  inttotalpage + "</MOTO_PAGE_NO>";	
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LC_LIMIT>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
											decEndCtr++;
										}
											break;
											//Motorcycle Policy (MC-1)
										case "MPEMC":
										{
												DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
												DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
												DecpageAutoEndmtElement.InnerXml +="<MOTO_PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " + "of" +" " +  inttotalpage + "</MOTO_PAGE_NO>";	
												DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
												DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
												DecpageAutoEndmtElement.InnerXml +="<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
												decEndCtr++;
										}
											break;
											//M-10 Amendment of Provisions - Michigan
										case "M10":
										{
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
											DecpageAutoEndmtElement.InnerXml +="<MOTO_PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " + "of" +" " +  inttotalpage + "</MOTO_PAGE_NO>";	
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
											decEndCtr++;
										}
											break;
										default:
											if((CoverageDetails["COVERAGE_TYPE"].ToString().ToUpper()=="RL") && CoverageDetails["COV_CODE"].ToString()!="MEDPM2" && CoverageDetails["COV_CODE"].ToString()!="MEDPM" && CoverageDetails["COV_CODE"].ToString()!="MEDPM1")
											{
												covDes = CoverageDetails["COV_DES"].ToString();
												if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString() != "")
													covDes += " - " + CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString();
												DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covDes)+"</AT_LC_DESCRIPTION>";
												DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
												DecpageAutoEndmtElement.InnerXml +="<MOTO_PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " + "of" +" " +  inttotalpage + "</MOTO_PAGE_NO>";	
												if(CovLimit =="0" || CovLimit =="" || CovLimit =="0.00" || CovLimit =="$0" || CovLimit =="$0.00")
												{
													DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
												}
												else
												{
													DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LC_LIMIT>";
												}
												if(strDec =="0" || strDec =="" || strDec =="0.00" || strDec =="$0" || strDec =="$0.00")
												{
													DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
												}
												else
												{
													DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_LC_DEDUCTIBLE>";
												}
												if(strPrem =="0" || strPrem =="" || strPrem =="0.00" || strPrem =="$0" || strPrem =="$0.00")
												{
													DecpageAutoEndmtElement.InnerXml +="<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
												}
												else
												{
													DecpageAutoEndmtElement.InnerXml +="<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
												}
												decEndCtr++;
											}
											break;
									}
								}
							}
							#endregion ADDITIONAL FORMS AND COVERAGES 
							#endregion DEC PAGE Additional Forms and Endorsements
							
							if(sumTtl!=0)
								MotorDecPageElement.InnerXml +="<TOTAL_MOTORCYCLE_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">$"+ RemoveJunkXmlCharacters(DollarFormat(sumTtl))+"</TOTAL_MOTORCYCLE_PREMIUM>";
							if(dbEstTotal!=0)
								MotorDecPageElement.InnerXml +="<TOTAL_POLICY_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">$"+ RemoveJunkXmlCharacters(DollarFormat(dbEstTotal))+"</TOTAL_POLICY_PREMIUM>";
							
								MotorDecPageElement.InnerXml +="<PREMIUM_ADJUST " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(adjPrm)+"</PREMIUM_ADJUST>";
						
								
							#endregion COVERAGES
						
					
							#region Creating Credit And Surcharge Xml
							int CreditSurchRowCounter = 0;
							if (isRateGenerated)
							{
								
								// when proccess not commited
								if(gStrtemp == "temp")
								{
								
									#region Credits
									XmlElement DecPageMotoCredit;
									DecPageMotoCredit = AcordPDFXML.CreateElement("MOTOCREDIT");

									if (gStrPdfFor == PDFForDecPage)
									{
										#region Dec Page Element
										MotorDecPageElement.AppendChild(DecPageMotoCredit);
										DecPageMotoCredit.SetAttribute(fieldType,fieldTypeMultiple);
										DecPageMotoCredit.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECREDIT"));
										DecPageMotoCredit.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGECREDIT"));
										DecPageMotoCredit.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGECREDITEXTN"));
										DecPageMotoCredit.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGECREDITEXTN"));
										#endregion
									}
							
									htpremium_dis.Clear(); 
									foreach (XmlNode CreditNode in GetCredits(MotoDetail["VEHICLE_ID"].ToString()))
									{
										if(!htpremium_dis.Contains(getAttributeValue(CreditNode,"COMPONENT_CODE")))
											htpremium_dis.Add(getAttributeValue(CreditNode,"COMPONENT_CODE"),getAttributeValue(CreditNode,"STEPPREMIUM"));
										else
											continue;
										string strCreditDisc="";
										if (gStrPdfFor == PDFForDecPage)
										{// Remove Discount Word from Credit discription
											strCreditDisc = getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n","");
											if(strCreditDisc.IndexOf("Insurance")>0)
											{
											}
											else
											{
												if(strCreditDisc.IndexOf(":")>0)
												{
													strCreditDisc=strCreditDisc.Substring(strCreditDisc.IndexOf(":",strCreditDisc.LastIndexOf(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))));
													strCreditDisc=strCreditDisc.Replace(":","");
												}
								
												#region Dec Page
												XmlElement DecPageMotoCreditElement;
												DecPageMotoCreditElement = AcordPDFXML.CreateElement("AUTOCREDITINFO");
												DecPageMotoCredit.AppendChild(DecPageMotoCreditElement);
												DecPageMotoCreditElement.SetAttribute(fieldType,fieldTypeNormal);
												DecPageMotoCreditElement.SetAttribute(id,CreditSurchRowCounter.ToString());
												DecPageMotoCreditElement.InnerXml += "<MOTORDESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(MotoDetail["VEHICLE_ID"].ToString()) + "  "  + RemoveJunkXmlCharacters(MotoDetail["VEHICLE_YEAR"].ToString())+"/"+RemoveJunkXmlCharacters(MotoDetail["MAKE"].ToString())+"/"+RemoveJunkXmlCharacters(MotoDetail["MODEL"].ToString())+ "/" +RemoveJunkXmlCharacters(MotoDetail["VIN"].ToString())+"     "+ RemoveJunkXmlCharacters(MotoDetail["VEHICLE_CC"].ToString() +" CC's") + "</MOTORDESCRIPTION>";
												DecPageMotoCreditElement.InnerXml += "<CREDITDISC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strCreditDisc) +"</CREDITDISC>"; 
												#endregion
												CreditSurchRowCounter++;
											}
										}
									
									}
									#endregion

															

									#region Surcharges
									XmlElement DecPageMotoSurch;
									DecPageMotoSurch = AcordPDFXML.CreateElement("AUTOSURCHARGE");

									if (gStrPdfFor == PDFForDecPage)
									{
										#region Dec Page Element
										MotorDecPageElement.AppendChild(DecPageMotoSurch);
										DecPageMotoSurch.SetAttribute(fieldType,fieldTypeMultiple);
										DecPageMotoSurch.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESRCHG"));
										DecPageMotoSurch.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHG"));
										DecPageMotoSurch.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESRCHGEXTN"));
										DecPageMotoSurch.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHGEXTN"));
										#endregion
									}
							
									htpremium_sur.Clear(); 
									foreach (XmlNode CreditNode in GetSurcharges(MotoDetail["VEHICLE_ID"].ToString()))
									{
										if(!htpremium_sur.Contains(getAttributeValue(CreditNode,"COMPONENT_CODE")))
											htpremium_sur.Add(getAttributeValue(CreditNode,"COMPONENT_CODE"),getAttributeValue(CreditNode,"STEPPREMIUM"));
										else
											continue;

										if (gStrPdfFor == PDFForDecPage)
										{
											// Remove Surcharge Word from Credit discription
											string strCreditSurch = getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n","");
											if(strCreditSurch.IndexOf(":")>0)
											{
												strCreditSurch=strCreditSurch.Substring(strCreditSurch.IndexOf(":",strCreditSurch.LastIndexOf(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))));
												strCreditSurch=strCreditSurch.Replace(":","");
											}
											#region Dec Page
											XmlElement DecPageMotoSurchElement;
											DecPageMotoSurchElement = AcordPDFXML.CreateElement("AUTOCREDITINFO");
											DecPageMotoCredit.AppendChild(DecPageMotoSurchElement);
											DecPageMotoSurchElement.SetAttribute(fieldType,fieldTypeNormal);
											DecPageMotoSurchElement.SetAttribute(id,CreditSurchRowCounter.ToString());
											DecPageMotoSurchElement.InnerXml += "<MOTORDESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(MotoDetail["VEHICLE_ID"].ToString()) + "  "  + RemoveJunkXmlCharacters(MotoDetail["VEHICLE_YEAR"].ToString())+"/"+RemoveJunkXmlCharacters(MotoDetail["MAKE"].ToString())+"/"+RemoveJunkXmlCharacters(MotoDetail["MODEL"].ToString())+ "/" +RemoveJunkXmlCharacters(MotoDetail["VIN"].ToString())+"     "+ RemoveJunkXmlCharacters(MotoDetail["VEHICLE_CC"].ToString() +" CC's") + "</MOTORDESCRIPTION>";
											DecPageMotoSurchElement.InnerXml += "<CREDITDISC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strCreditSurch)+"</CREDITDISC>"; 
											//DecPageMotoSurchElement.InnerXml += "<SURCHARGE_AMT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</SURCHARGE_AMT>"; 
											#endregion
										}								
										CreditSurchRowCounter++;
									}
									#endregion
								}
							}
									// when Proccess Commited
								if(gStrtemp != "temp")
								{
									#region Credits
									XmlElement DecPageMotoCredit;
									DecPageMotoCredit = AcordPDFXML.CreateElement("MOTOCREDIT");

									if (gStrPdfFor == PDFForDecPage)
									{
										#region Dec Page Element
										MotorDecPageElement.AppendChild(DecPageMotoCredit);
										DecPageMotoCredit.SetAttribute(fieldType,fieldTypeMultiple);
										DecPageMotoCredit.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECREDIT"));
										DecPageMotoCredit.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGECREDIT"));
										DecPageMotoCredit.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGECREDITEXTN"));
										DecPageMotoCredit.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGECREDITEXTN"));
										#endregion
									}
							
									htpremium_dis.Clear(); 
									foreach (DataRow CreditNode in DSTempVehicle.Tables[1].Rows )
									{
										if(CreditNode["COMPONENT_TYPE"].ToString()=="D" && (CreditNode["COVERAGE_PREMIUM"].ToString()!="0" && CreditNode["COVERAGE_PREMIUM"].ToString()!="0.00" && CreditNode["COVERAGE_PREMIUM"].ToString()!=""))
										{
											string strCreditDisc="",strCreditPerc="";
											strCreditDisc = CreditNode["COMP_REMARKS"].ToString();
											strCreditPerc = CreditNode["COM_EXT_AD"].ToString();
											
											if(strCreditDisc.IndexOf(":")>0)
											{
												strCreditDisc=strCreditDisc.Substring(strCreditDisc.IndexOf(":",strCreditDisc.LastIndexOf(strCreditDisc)));
												strCreditDisc=strCreditDisc.Replace(":","");
												if(strCreditPerc!="")
													strCreditDisc=strCreditDisc+" -"+strCreditPerc;
												else
													strCreditDisc=strCreditDisc;
											}
											if (gStrPdfFor == PDFForDecPage)
											{
												if(strCreditDisc.IndexOf("Insurance")>0)
												{
												}
												else
												{
													#region Dec Page
													XmlElement DecPageMotoCreditElement;
													DecPageMotoCreditElement = AcordPDFXML.CreateElement("AUTOCREDITINFO");
													DecPageMotoCredit.AppendChild(DecPageMotoCreditElement);
													DecPageMotoCreditElement.SetAttribute(fieldType,fieldTypeNormal);
													DecPageMotoCreditElement.SetAttribute(id,CreditSurchRowCounter.ToString());
													DecPageMotoCreditElement.InnerXml +="<MOTORDESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(MotoDetail["VEHICLE_ID"].ToString()) + "  "  + RemoveJunkXmlCharacters(MotoDetail["VEHICLE_YEAR"].ToString())+"/"+RemoveJunkXmlCharacters(MotoDetail["MAKE"].ToString())+"/"+RemoveJunkXmlCharacters(MotoDetail["MODEL"].ToString())+ "/" +RemoveJunkXmlCharacters(MotoDetail["VIN"].ToString())+"     "+ RemoveJunkXmlCharacters(MotoDetail["VEHICLE_CC"].ToString() +" CC's") + "</MOTORDESCRIPTION>";
													DecPageMotoCreditElement.InnerXml += "<CREDITDISC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strCreditDisc) +"</CREDITDISC>"; 
													#endregion
													CreditSurchRowCounter++;
												}
											}											
										}
									}
									#endregion

															

									#region Surcharges
									XmlElement DecPageMotoSurch;
									DecPageMotoSurch = AcordPDFXML.CreateElement("AUTOSURCHARGE");

									if (gStrPdfFor == PDFForDecPage)
									{
										#region Dec Page Element
										MotorDecPageElement.AppendChild(DecPageMotoSurch);
										DecPageMotoSurch.SetAttribute(fieldType,fieldTypeMultiple);
										DecPageMotoSurch.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESRCHG"));
										DecPageMotoSurch.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHG"));
										DecPageMotoSurch.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESRCHGEXTN"));
										DecPageMotoSurch.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHGEXTN"));
										#endregion
									}
							
									htpremium_sur.Clear(); 
									foreach (DataRow CreditNode in DSTempVehicle.Tables[1].Rows)
									{
										if(CreditNode["COMPONENT_TYPE"].ToString()=="S" && (CreditNode["COVERAGE_PREMIUM"].ToString()!="0" && CreditNode["COVERAGE_PREMIUM"].ToString()!="0.00" && CreditNode["COVERAGE_PREMIUM"].ToString()!=""))
										{	
											string strCreditSurch = CreditNode["COMP_REMARKS"].ToString();
											string strCreditSurcPerc = CreditNode["COM_EXT_AD"].ToString();
											if(strCreditSurch.IndexOf(":")>0)
											{
												strCreditSurch=strCreditSurch.Substring(strCreditSurch.IndexOf(":",strCreditSurch.LastIndexOf(strCreditSurch)));
												strCreditSurch=strCreditSurch.Replace(":","");
												if(strCreditSurcPerc!="")
													strCreditSurch=strCreditSurch+" -"+strCreditSurcPerc;
												else
													strCreditSurch=strCreditSurch;
											}
											if (gStrPdfFor == PDFForDecPage)
											{
												#region Dec Page
													XmlElement DecPageMotoSurchElement;
													DecPageMotoSurchElement = AcordPDFXML.CreateElement("AUTOCREDITINFO");
													DecPageMotoCredit.AppendChild(DecPageMotoSurchElement);
													DecPageMotoSurchElement.SetAttribute(fieldType,fieldTypeNormal);
													DecPageMotoSurchElement.SetAttribute(id,CreditSurchRowCounter.ToString());
													DecPageMotoSurchElement.InnerXml += "<MOTORDESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(MotoDetail["VEHICLE_ID"].ToString()) + "  "  + RemoveJunkXmlCharacters(MotoDetail["VEHICLE_YEAR"].ToString())+"/"+RemoveJunkXmlCharacters(MotoDetail["MAKE"].ToString())+"/"+RemoveJunkXmlCharacters(MotoDetail["MODEL"].ToString())+ "/" +RemoveJunkXmlCharacters(MotoDetail["VIN"].ToString())+"     "+ RemoveJunkXmlCharacters(MotoDetail["VEHICLE_CC"].ToString() +" CC's") + "</MOTORDESCRIPTION>";
													DecPageMotoSurchElement.InnerXml += "<CREDITDISC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strCreditSurch)+"</CREDITDISC>"; 
												#endregion
											}								
											CreditSurchRowCounter++;
										}
									}
									#endregion
								}

							
							#endregion 

							#region Additional Interest
					
							gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
							gobjWrapper.AddParameter("@POLID",gStrPolicyId);
							gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
							gobjWrapper.AddParameter("@VEHICLEID",MotoDetail["Vehicle_ID"]);
							gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
							DSTemp_DataSet = gobjWrapper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS");
							gobjWrapper.ClearParameteres();

							//DSTemp_DataSet = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion +","+ MotoDetail["VEHICLE_ID"]+"," + "'" + gStrCalledFrom + "'");
										
							XmlElement DecPageADDINT;
							DecPageADDINT= AcordPDFXML.CreateElement("ADDITIONALINTEREST");
							if(gStrPdfFor == PDFForDecPage && DSTemp_DataSet.Tables[0].Rows.Count > 0)
							{

								MotorDecPageElement.AppendChild(DecPageADDINT);
								DecPageADDINT.SetAttribute(fieldType,fieldTypeMultiple);
								DecPageADDINT.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEADDINT"));
								DecPageADDINT.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEADDINT"));
								DecPageADDINT.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEADDINTEXTN"));
								DecPageADDINT.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEADDINTEXTN"));
					
								int addintCtr=0;
								foreach(DataRow AddInt in DSTemp_DataSet.Tables[0].Rows)
								{
								
									XmlElement DecpageMOTORADDINT;
									DecpageMOTORADDINT = AcordPDFXML.CreateElement("ADITIONALINTERSTINFO");
									DecPageADDINT.AppendChild(DecpageMOTORADDINT);
									DecpageMOTORADDINT.SetAttribute(fieldType,fieldTypeNormal);
									DecpageMOTORADDINT.SetAttribute(id,addintCtr.ToString());
						
									if(addintCtr > 1 && addintCtr <3)
									{
										intpageno++;
									}
									DecpageMOTORADDINT.InnerXml += "<MOTORDESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(MotoDetail["VEHICLE_ID"].ToString()) + "  "  + RemoveJunkXmlCharacters(MotoDetail["VEHICLE_YEAR"].ToString())+"/"+RemoveJunkXmlCharacters(MotoDetail["MAKE"].ToString())+"/"+RemoveJunkXmlCharacters(MotoDetail["MODEL"].ToString())+ "/" +RemoveJunkXmlCharacters(MotoDetail["VIN"].ToString())+"     "+ RemoveJunkXmlCharacters(MotoDetail["VEHICLE_CC"].ToString() +" CC's") + "</MOTORDESCRIPTION>";
									DecpageMOTORADDINT.InnerXml += "<MOTO_SERIALNO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddInt["RANK"].ToString()) + "</MOTO_SERIALNO>";
									DecpageMOTORADDINT.InnerXml +="<MOTO_PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " +  "of" + " " + inttotalpage + "</MOTO_PAGE_NO>";
									DecpageMOTORADDINT.InnerXml +=  "<MOTO_nameAddress " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddInt["HOLDER_NAME"].ToString()+", "+ AddInt["ADDRESS"].ToString()+" "+AddInt["CITYSTATEZIP"]) + "</MOTO_nameAddress>";
									DecpageMOTORADDINT.InnerXml += "<MOTO_natureofInterest " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddInt["NATUREOFINTEREST"].ToString()) + "</MOTO_natureofInterest>";
									DecpageMOTORADDINT.InnerXml +=  "<MOTO_loanNo " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddInt["LOAN_REF_NUMBER"].ToString()) + "</MOTO_loanNo>";
									addintCtr++;	
								}
							}


					

							#endregion Additional Interest

							#region Operator Details
							Operator_Ctr=0;

							//DSTempOperator = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAutoOperatorDtls " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'," + MotoDetail["Vehicle_ID"] );
							//DSTempOperator = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAutoOperatorDtls " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "',0");
					
							XmlElement DecPageOperatorElement;
							DecPageOperatorElement = AcordPDFXML.CreateElement("DECPAGEOPERATORINFO");
					
							if(gStrPdfFor == PDFForDecPage)
							{
								MotorDecPageElement.AppendChild(DecPageOperatorElement);
								DecPageOperatorElement.SetAttribute(fieldType,fieldTypeMultiple);
								DecPageOperatorElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOPERATOR"));
								DecPageOperatorElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOPERATOR"));
								DecPageOperatorElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEOPERATOREXTN"));
								DecPageOperatorElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOPERATOREXTN"));		
								XmlElement DecOperatorElement;		
								//foreach(DataRow OperatorDetail in DSTempOperator.Tables[0].Rows)

								gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
								gobjWrapper.AddParameter("@POLID",gStrPolicyId);
								gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
								gobjWrapper.AddParameter("@VEHICLEID",MotoDetail["Vehicle_ID"]);
								gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
								DataSet DSTempAutoAssgnOpr = gobjWrapper.ExecuteDataSet("Proc_GetPDFAuto_AssgnDriv");
								gobjWrapper.ClearParameteres();

								//DataSet DSTempAutoAssgnOpr = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAuto_AssgnDriv " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",'" + gStrCalledFrom + "',"  + MotoDetail["Vehicle_ID"]);
								if(DSTempAutoAssgnOpr.Tables.Count > 1 && DSTempAutoAssgnOpr.Tables[1].Rows.Count > 0)
								{
									foreach(DataRow OprDetails in DSTempAutoAssgnOpr.Tables[1].Rows)
									{
										XmlElement DecPageAutoOprElement;
										DecPageAutoOprElement = AcordPDFXML.CreateElement("OPERATORINFO");

										#region Operator Dec Page
										DecOperatorElement = AcordPDFXML.CreateElement("OPERATORINFO");
										DecPageOperatorElement.AppendChild(DecOperatorElement);
										DecOperatorElement.SetAttribute(fieldType,fieldTypeNormal);
										DecOperatorElement.SetAttribute(id,Operator_Ctr.ToString());
										
										//PAGE INFO
										if(Operator_Ctr >=4 && Operator_Ctr <5)
										{
											intpageno++;
										}
										DecOperatorElement.InnerXml +="<MOTORDESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(MotoDetail["VEHICLE_ID"].ToString()) + "  "  + RemoveJunkXmlCharacters(MotoDetail["VEHICLE_YEAR"].ToString())+"/"+RemoveJunkXmlCharacters(MotoDetail["MAKE"].ToString())+"/"+RemoveJunkXmlCharacters(MotoDetail["MODEL"].ToString())+ "/" +RemoveJunkXmlCharacters(MotoDetail["VIN"].ToString())+"     "+ RemoveJunkXmlCharacters(MotoDetail["VEHICLE_CC"].ToString() +" CC's") + "</MOTORDESCRIPTION>";
										DecOperatorElement.InnerXml +="<MOTO_OPERATORNAME " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(OprDetails["DRIVER_NAME"].ToString()) +"</MOTO_OPERATORNAME>"; 
										DecOperatorElement.InnerXml +="<MOTO_PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " +  "of" + " " +  inttotalpage +  "</MOTO_PAGE_NO>";
										DecOperatorElement.InnerXml +="<MOTO_OPERATOR_LICENSE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OprDetails["DRIVER_DRIV_LIC"].ToString()) +"</MOTO_OPERATOR_LICENSE>"; 
										DecOperatorElement.InnerXml +="<MOTO_OPERATOR_DOB " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OprDetails["DRIVER_DOB"].ToString())+"</MOTO_OPERATOR_DOB>"; 
										DecOperatorElement.InnerXml +="<MOTO_OPERATOR_RATED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OprDetails["ASSIGNED_ASS"].ToString())+"</MOTO_OPERATOR_RATED>"; 
										#endregion
										Operator_Ctr++;
									}
								}

							}
					
							#endregion Operator Details
							#endregion DecPage Info
						}

						if (gStrPdfFor == PDFForAcord)
						{	
							#region Root element decl. for Motor
						
							//DSTempOperator.Clear();

							
							gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
							gobjWrapper.AddParameter("@POLID",gStrPolicyId);
							gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
							gobjWrapper.AddParameter("@VEHICLEID","0");
							gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
							DSTempOperator = gobjWrapper.ExecuteDataSet("Proc_GetPDFAutoOperatorDtls");
							gobjWrapper.ClearParameteres();

							//DSTempOperator = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAutoOperatorDtls " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'," + "0" );
						
							XmlElement AcordMotorDriverElement;
							AcordMotorDriverElement = AcordPDFXML.CreateElement("MotorDriverInfo");
							AcordMotorRootElement.AppendChild(AcordMotorDriverElement);
							AcordMotorDriverElement.SetAttribute(fieldType,fieldTypeMultiple);
							if(stCode.Equals("IN")) 
							{
							
								AcordMotorDriverElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORDMOTORDRIVERIN"));
								AcordMotorDriverElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORDMOTORDRIVERIN"));
								AcordMotorDriverElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORDINMOTORDRIVEREXTN"));
								AcordMotorDriverElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORDINMOTORDRIVEREXTN"));
							}

							else if(stCode.Equals("MI"))
							{
								AcordMotorDriverElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORDMOTORDRIVERMI"));
								AcordMotorDriverElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORDMOTORDRIVERMI"));
								AcordMotorDriverElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORDMIMOTORDRIVEREXTN"));
								AcordMotorDriverElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORDMIMOTORDRIVEREXTN"));
							}
								
							// Maping for self relationship
							foreach(DataRow OperatorDetail in DSTempOperator.Tables[0].Rows)
							{
								if(DSTempOperator.Tables[0].Rows.Count <= Operator_Ctr)
									break;
								if(OperatorDetail["RELATION"].ToString()=="Self")
								{
									XmlElement OperatorElement;
									OperatorElement = AcordPDFXML.CreateElement("OPERATORINFO");
									AcordMotorDriverElement.AppendChild(OperatorElement);
									OperatorElement.SetAttribute(fieldType,fieldTypeNormal);
									OperatorElement.SetAttribute(id,Operator_Ctr.ToString());

									OperatorElement.InnerXml = OperatorElement.InnerXml +  "<OPERATORNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(OperatorDetail["DRIVER_NAME"].ToString()) + "</OPERATORNAME>";
									OperatorElement.InnerXml = OperatorElement.InnerXml +  "<OPERATORSEX " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(OperatorDetail["DRIVER_SEX"].ToString()) + "</OPERATORSEX>";
									OperatorElement.InnerXml = OperatorElement.InnerXml +  "<OPERATORRELATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(OperatorDetail["RELATION"].ToString()) + "</OPERATORRELATION>";
									OperatorElement.InnerXml = OperatorElement.InnerXml +  "<OPERATORDOB " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(OperatorDetail["DRIVER_DOB"].ToString()) + "</OPERATORDOB>";
							
									#region PRIN OCC ID
									gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
									gobjWrapper.AddParameter("@POLID",gStrPolicyId);
									gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
									gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
									DataSet DSTempDataSet1 = gobjWrapper.ExecuteDataSet("Proc_GetPDFAuto_Details");
									gobjWrapper.ClearParameteres();


									//DataSet DSTempDataSet1 = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAuto_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
									string strOprSSn="";
									strOprSSn =  OperatorDetail["DRIVER_SSN"].ToString();
									if(strOprSSn !="" && strOprSSn !="0")
									{
										strOprSSn = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(OperatorDetail["DRIVER_SSN"].ToString());
									
										if(strOprSSn.Trim()!="")//If-Else condition added by Charles on 21/7/2009 for Itrack 6129
										{
											string strvaln = "xxx-xx-";
											strvaln += strOprSSn.Substring(strvaln.Length, strOprSSn.Length - strvaln.Length);
											strOprSSn = strvaln;
										}
										else
											strOprSSn="";

									}
									if(DSTempDataSet1.Tables[0].Rows.Count>0)
									{			
						
										foreach(DataRow MotoDetail1 in DSTempDataSet1.Tables[0].Rows)
										{
											string prin_occ = "";

											gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
											gobjWrapper.AddParameter("@POLID",gStrPolicyId);
											gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
											gobjWrapper.AddParameter("@VEHICLEID",MotoDetail1["Vehicle_ID"]);
											gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
											DataSet DSTempAutoAssgnOpr1  = gobjWrapper.ExecuteDataSet("Proc_GetPDFAuto_AssgnDriv");
											gobjWrapper.ClearParameteres();

											//DataSet DSTempAutoAssgnOpr1 = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAuto_AssgnDriv " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",'" + gStrCalledFrom + "',"  + MotoDetail1["Vehicle_ID"]);
											//while(operCounttemp < DSTempAutoOpr.Tables[0].Rows.Count)
											if(DSTempAutoAssgnOpr1.Tables.Count > 1 && DSTempAutoAssgnOpr1.Tables[1].Rows.Count > 0)
											{
												foreach(DataRow OprDetails in DSTempAutoAssgnOpr1.Tables[1].Rows)
												{
													if(OperatorDetail["OPERATORNO"].ToString() == OprDetails["DRIVER_ID"].ToString())
													{
														prin_occ = OprDetails["OPERATOROCC"].ToString();
														break;
													}
												}
											}
											switch (MotoDetail1["VEHICLE_ID"].ToString())
											{
												case "1": 
													OperatorElement.InnerXml = OperatorElement.InnerXml +  "<OPERATOROCCUPATION1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(prin_occ) + "</OPERATOROCCUPATION1>";
													break;
												case "2":
													OperatorElement.InnerXml = OperatorElement.InnerXml +  "<OPERATOROCCUPATION2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(prin_occ) + "</OPERATOROCCUPATION2>";
													break;
												case "3":
													OperatorElement.InnerXml = OperatorElement.InnerXml +  "<OPERATOROCCUPATION3 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(prin_occ) + "</OPERATOROCCUPATION3>";
													break;
											}
										}
									}
									#endregion PRIN OCC ID
									OperatorElement.InnerXml = OperatorElement.InnerXml +  "<OPERATORLICNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(OperatorDetail["DRIVER_DRIV_LIC"].ToString()) + "</OPERATORLICNUM>";
									OperatorElement.InnerXml = OperatorElement.InnerXml +  "<STATELIC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(OperatorDetail["DRIVER_LIC_STATE"].ToString()) + "</STATELIC>";
									if(OperatorDetail["NO_CYCLE_ENDMT"].ToString().Trim() == "1")
										OperatorElement.InnerXml = OperatorElement.InnerXml +  "<CYCLENDO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Yes") + "</CYCLENDO>";
									else if(OperatorDetail["NO_CYCLE_ENDMT"].ToString().Trim() == "0")
										OperatorElement.InnerXml = OperatorElement.InnerXml +  "<CYCLENDO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("No") + "</CYCLENDO>";
									OperatorElement.InnerXml = OperatorElement.InnerXml +  "<SOCIALSECNO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strOprSSn) + "</SOCIALSECNO>";

									Operator_Ctr++;
								}
							}

							//Mapping for other than self relationship
							foreach(DataRow OperatorDetail in DSTempOperator.Tables[0].Rows)
							{
								if(DSTempOperator.Tables[0].Rows.Count <= Operator_Ctr)
									break;
								if(OperatorDetail["RELATION"].ToString()!="Self")
								{
									XmlElement OperatorElement;
									OperatorElement = AcordPDFXML.CreateElement("OPERATORINFO");
									AcordMotorDriverElement.AppendChild(OperatorElement);
									OperatorElement.SetAttribute(fieldType,fieldTypeNormal);
									OperatorElement.SetAttribute(id,Operator_Ctr.ToString());

									OperatorElement.InnerXml = OperatorElement.InnerXml +  "<OPERATORNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(OperatorDetail["DRIVER_NAME"].ToString()) + "</OPERATORNAME>";
									OperatorElement.InnerXml = OperatorElement.InnerXml +  "<OPERATORSEX " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(OperatorDetail["DRIVER_SEX"].ToString()) + "</OPERATORSEX>";
									OperatorElement.InnerXml = OperatorElement.InnerXml +  "<OPERATORRELATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(OperatorDetail["RELATION"].ToString()) + "</OPERATORRELATION>";
									OperatorElement.InnerXml = OperatorElement.InnerXml +  "<OPERATORDOB " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(OperatorDetail["DRIVER_DOB"].ToString()) + "</OPERATORDOB>";
							
									#region PRIN OCC ID
									gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
									gobjWrapper.AddParameter("@POLID",gStrPolicyId);
									gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
									gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
									DataSet DSTempDataSet1 = gobjWrapper.ExecuteDataSet("Proc_GetPDFAuto_Details");
									gobjWrapper.ClearParameteres();


									//DataSet DSTempDataSet1 = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAuto_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
									string strOprSSn="";
									strOprSSn =  OperatorDetail["DRIVER_SSN"].ToString();
									if(strOprSSn !="" && strOprSSn !="0")
									{
										strOprSSn = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(OperatorDetail["DRIVER_SSN"].ToString());
									
										if(strOprSSn.Trim()!="")//If-Else condition added by Charles on 21/7/2009 for Itrack 6129
										{
											string strvaln = "xxx-xx-";
											strvaln += strOprSSn.Substring(strvaln.Length, strOprSSn.Length - strvaln.Length);
											strOprSSn = strvaln;
										}
										else
											strOprSSn="";

									}
									if(DSTempDataSet1.Tables[0].Rows.Count>0)
									{			
						
										foreach(DataRow MotoDetail1 in DSTempDataSet1.Tables[0].Rows)
										{
											string prin_occ = "";

											gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
											gobjWrapper.AddParameter("@POLID",gStrPolicyId);
											gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
											gobjWrapper.AddParameter("@VEHICLEID",MotoDetail1["Vehicle_ID"]);
											gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
											DataSet DSTempAutoAssgnOpr1  = gobjWrapper.ExecuteDataSet("Proc_GetPDFAuto_AssgnDriv");
											gobjWrapper.ClearParameteres();

											//DataSet DSTempAutoAssgnOpr1 = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAuto_AssgnDriv " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",'" + gStrCalledFrom + "',"  + MotoDetail1["Vehicle_ID"]);
											//while(operCounttemp < DSTempAutoOpr.Tables[0].Rows.Count)
											if(DSTempAutoAssgnOpr1.Tables.Count > 1 && DSTempAutoAssgnOpr1.Tables[1].Rows.Count > 0)
											{
												foreach(DataRow OprDetails in DSTempAutoAssgnOpr1.Tables[1].Rows)
												{
													if(OperatorDetail["OPERATORNO"].ToString() == OprDetails["DRIVER_ID"].ToString())
													{
														prin_occ = OprDetails["OPERATOROCC"].ToString();
														break;
													}
												}
											}
											switch (MotoDetail1["VEHICLE_ID"].ToString())
											{
												case "1": 
													OperatorElement.InnerXml = OperatorElement.InnerXml +  "<OPERATOROCCUPATION1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(prin_occ) + "</OPERATOROCCUPATION1>";
													break;
												case "2":
													OperatorElement.InnerXml = OperatorElement.InnerXml +  "<OPERATOROCCUPATION2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(prin_occ) + "</OPERATOROCCUPATION2>";
													break;
												case "3":
													OperatorElement.InnerXml = OperatorElement.InnerXml +  "<OPERATOROCCUPATION3 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(prin_occ) + "</OPERATOROCCUPATION3>";
													break;
											}
										}
									}
									#endregion PRIN OCC ID
									OperatorElement.InnerXml = OperatorElement.InnerXml +  "<OPERATORLICNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(OperatorDetail["DRIVER_DRIV_LIC"].ToString()) + "</OPERATORLICNUM>";
									OperatorElement.InnerXml = OperatorElement.InnerXml +  "<STATELIC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(OperatorDetail["DRIVER_LIC_STATE"].ToString()) + "</STATELIC>";
									if(OperatorDetail["NO_CYCLE_ENDMT"].ToString().Trim() == "1")
										OperatorElement.InnerXml = OperatorElement.InnerXml +  "<CYCLENDO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Yes") + "</CYCLENDO>";
									else if(OperatorDetail["NO_CYCLE_ENDMT"].ToString().Trim() == "0")
										OperatorElement.InnerXml = OperatorElement.InnerXml +  "<CYCLENDO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("No") + "</CYCLENDO>";
									OperatorElement.InnerXml = OperatorElement.InnerXml +  "<SOCIALSECNO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strOprSSn) + "</SOCIALSECNO>";

									Operator_Ctr++;
								}
							}

							//string driv_dob = "";
							string mcage = "X";

							gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
							gobjWrapper.AddParameter("@POLID",gStrPolicyId);
							gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
							gobjWrapper.AddParameter("@VEHICLEID",MotoDetail["Vehicle_ID"]);
							gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
							DataSet DSTempAutoAssgnOpr  = gobjWrapper.ExecuteDataSet("Proc_GetPDFAuto_AssgnDriv");
							gobjWrapper.ClearParameteres();

							//DataSet DSTempAutoAssgnOpr = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAuto_AssgnDriv " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",'" + gStrCalledFrom + "',"  + MotoDetail["Vehicle_ID"]);
							if(DSTempAutoAssgnOpr.Tables.Count > 1 && DSTempAutoAssgnOpr.Tables[1].Rows.Count > 0)
							{
								foreach(DataRow OprDetails in DSTempAutoAssgnOpr.Tables[1].Rows)
								{
										if(OprDetails["MATUREOPERATOR"].ToString() != "")
											mcage = "";
								}
							}
						

							XmlElement MotorElement;
							MotorElement = AcordPDFXML.CreateElement("CYCLEINFO");

							AcordMotorElement.AppendChild(MotorElement);
							MotorElement.SetAttribute(fieldType,fieldTypeNormal);
							MotorElement.SetAttribute(id,MotoCtr.ToString());
							#endregion

							MotorElement.InnerXml = MotorElement.InnerXml +  "<MCYEAR " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(MotoDetail["VEHICLE_YEAR"].ToString()) + "</MCYEAR>";
							MotorElement.InnerXml = MotorElement.InnerXml +  "<MCMAKE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(MotoDetail["MAKE"].ToString()) + "</MCMAKE>";
							MotorElement.InnerXml = MotorElement.InnerXml +  "<MCMODEL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(MotoDetail["MODEL"].ToString()) + "</MCMODEL>";
							MotorElement.InnerXml = MotorElement.InnerXml +  "<MCSERIAL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(MotoDetail["VIN"].ToString()) + "</MCSERIAL>";
							MotorElement.InnerXml = MotorElement.InnerXml +  "<MCCC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(MotoDetail["VEHICLE_CC"].ToString()) + "</MCCC>";
							MotorElement.InnerXml = MotorElement.InnerXml +  "<MCAGE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(mcage) + "</MCAGE>";
							if(dbEstTotal!=0)
								MotorElement.InnerXml +="<TOTALPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+ (dbEstTotal + ".00")+"</TOTALPREMIUM>";
						
							MotorElement.InnerXml +="<RECEIVEDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+ (recvd_prem)+"</RECEIVEDPREMIUM>";
							if(MotoDetail["MOTORCYCLE_TYPE"].ToString()=="11425")
								MotorElement.InnerXml = MotorElement.InnerXml +  "<MCTOURBIKE " + fieldType + "=\"" + fieldTypeText + "\">" +"X" + "</MCTOURBIKE>";
							if(MotoDetail["MOTORCYCLE_TYPE"].ToString()=="11424")
								MotorElement.InnerXml = MotorElement.InnerXml +  "<MCSPORTHP " + fieldType + "=\"" + fieldTypeText + "\">" + "X" + "</MCSPORTHP>";
							if(MotoDetail["MOTORCYCLE_TYPE"].ToString()=="11422")
								MotorElement.InnerXml = MotorElement.InnerXml +  "<MCEXTRAHZ " + fieldType + "=\"" + fieldTypeText + "\">" + "X" + "</MCEXTRAHZ>";
							if(MotoDetail["MOTORCYCLE_TYPE"].ToString()=="11423")
								MotorElement.InnerXml = MotorElement.InnerXml +  "<MCREGULAR " + fieldType + "=\"" + fieldTypeText + "\">" + "X" + "</MCREGULAR>";
							if(gStrtemp == "temp")
							{
								if(GetTransfer_Renewal(MotoDetail["VEHICLE_ID"].ToString()).Count >0)
									MotorElement.InnerXml = MotorElement.InnerXml +  "<MCTRANSFERCREDITS " + fieldType + "=\"" + fieldTypeText + "\">" +"X" + "</MCTRANSFERCREDITS>";
							}
							
							gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
							gobjWrapper.AddParameter("@POLID",gStrPolicyId);
							gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
							gobjWrapper.AddParameter("@VEHICLEID",MotoDetail["VEHICLE_ID"]);
							gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
							DataSet DSTempAutoAddInt = gobjWrapper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS");
							gobjWrapper.ClearParameteres();

							//DataSet DSTempAutoAddInt = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + "," + MotoDetail["VEHICLE_ID"] + ",'" + gStrCalledFrom + "'");

							if(DSTempAutoAddInt.Tables.Count > 0 && DSTempAutoAddInt.Tables[0].Rows.Count > 0)
							{
								string losspayee = "";
								losspayee = DSTempAutoAddInt.Tables[0].Rows[0]["HOLDER_NAME"].ToString() + ", " + DSTempAutoAddInt.Tables[0].Rows[0]["ADDRESS"].ToString() + DSTempAutoAddInt.Tables[0].Rows[0]["CITYSTATEZIP"].ToString();
								MotorElement.InnerXml = MotorElement.InnerXml +  "<LOSSPAYABLETO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(losspayee) + "</LOSSPAYABLETO>";
							}

							#region COVERAGES

							gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
							gobjWrapper.AddParameter("@POLID",gStrPolicyId);
							gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
							gobjWrapper.AddParameter("@VEHICLEID",MotoDetail["VEHICLE_ID"]);
							gobjWrapper.AddParameter("@RISKTYPE",MotoDetail["MOTORCYCLETYPE"]);
							gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
							DSTempVehicle = gobjWrapper.ExecuteDataSet("Proc_GetPDFAuto_Coverage_Details");
							gobjWrapper.ClearParameteres();

							//DSTempVehicle = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAuto_Coverage_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +MotoDetail["VEHICLE_ID"] +  ",'"+ MotoDetail["MOTORCYCLETYPE"] +"','" + gStrCalledFrom + "'");
							if(gStrtemp != "temp")
							{
								foreach (DataRow CreditNode in DSTempVehicle.Tables[1].Rows )
								{
									if(CreditNode["COMPONENT_CODE"].ToString()=="D_XFR")
									{
										MotorElement.InnerXml = MotorElement.InnerXml +  "<MCTRANSFERCREDITS " + fieldType + "=\"" + fieldTypeText + "\">" +"X" + "</MCTRANSFERCREDITS>";
									}
								}
							}
				

							//int lcCtr=0,dblSumTotal=0;	
							string uninsmotb = "-1";
							string undinsmotb = "-1";
							string uninsmotp = "-1";
							string un_undinsmotb = "-1";
							string undinsmot = "-1";
							int rowindex = 0;
					
							// Acord coverage premium when policy not commited
							htpremium.Clear(); 
							if(gStrtemp != "final")
							{
								foreach (XmlNode PremiumNode in GetAutoPremium(MotoDetail["VEHICLE_ID"].ToString()))
								{
									if(!htpremium.Contains(getAttributeValue(PremiumNode,"COMPONENT_CODE")))
										htpremium.Add(getAttributeValue(PremiumNode,"COMPONENT_CODE"),getAttributeValue(PremiumNode,"STEPPREMIUM"));
								}

							}
							string strVehNo="0";
							
							XmlElement MotorCOVRootElement;
							MotorCOVRootElement = AcordPDFXML.CreateElement("LIABCOVERAGESINFO");
							MotorElement.AppendChild(MotorCOVRootElement);
							MotorCOVRootElement.SetAttribute(fieldType,fieldTypeMultiple);
							MotorCOVRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACCORDLBLCOVERAGE"));
							MotorCOVRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACCORDLBLCOVERAGE"));
							MotorCOVRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACCORDLBLCOVERAGEEXTN"));
							MotorCOVRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACCORDLBLCOVERAGEEXTN"));
									
							foreach(DataRow CoverageDetails in DSTempVehicle.Tables[0].Rows)
							{
								//GetPremium(DSTempVehicle,covCode)
								string covCode = CoverageDetails["COV_CODE"].ToString();
								#region Accord Coverages 
								if (gStrPdfFor == PDFForAcord)
								{
									XmlElement MotorCOVElement;
									MotorCOVElement = AcordPDFXML.CreateElement("COVERAGEINFO");

									MotorCOVRootElement.AppendChild(MotorCOVElement);
									MotorCOVElement.SetAttribute(fieldType,fieldTypeNormal);
									MotorCOVElement.SetAttribute(id,MotoCtr.ToString());
					
									// Coverage premium 
									// formating for Premium
									if(gStrtemp == "temp")
									{
										strPrem=GetPremiumBeforeCommit(DSTempVehicle,covCode,htpremium);
									}
									else
									{
										strPrem=GetPremium(DSTempVehicle,covCode);										
									}
									#region SWITCH CASE
									switch(CoverageDetails["COV_CODE"].ToString())
									{

										case "RLCSL":
											if(CoverageDetails["LIMIT_1"].ToString()!="")
												MotorCOVElement.InnerXml +="<SINGLELIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</SINGLELIMIT>";							
											else 
												MotorCOVElement.InnerXml +="<SINGLELIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</SINGLELIMIT>";							
												
											MotorCOVElement.InnerXml +="<SINGLELIMITPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</SINGLELIMITPREMIUM>";
											
											if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"].ToString() != "")
												dblRLCSL = Convert.ToDouble(CoverageDetails["LIMIT_1"]);
										break;

										case "BISPL":
											if(CoverageDetails["LIMIT_1"].ToString()!="")
												MotorCOVElement.InnerXml +="<BODILYINJURYLIMITBEFORE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</BODILYINJURYLIMITBEFORE>";
											else 
												MotorCOVElement.InnerXml +="<BODILYINJURYLIMITBEFORE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</BODILYINJURYLIMITBEFORE>";

											MotorCOVElement.InnerXml +="<BODILYINJURYLIMITAFTER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_2"].ToString())+"</BODILYINJURYLIMITAFTER>";
											MotorElement.InnerXml +="<BI_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</BI_PREMIUM>";
											
											if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"].ToString() != "")
												dblBISPL = Convert.ToDouble(CoverageDetails["LIMIT_1"]);
											break;
										case "PD":
											if(CoverageDetails["LIMIT_1"].ToString()!="")
												MotorCOVElement.InnerXml +="<PROPERTYDAMAGELIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</PROPERTYDAMAGELIMIT>";
											else 
												MotorCOVElement.InnerXml +="<PROPERTYDAMAGELIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</PROPERTYDAMAGELIMIT>";
	
											MotorElement.InnerXml +="<PD_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</PD_PREMIUM>";
											
											if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"].ToString() != "")
												dblPD = Convert.ToDouble(CoverageDetails["LIMIT_1"]);
											break;	
					
										case "MEDPM":
											if(CoverageDetails["LIMIT_1"].ToString()!="")
												MotorCOVElement.InnerXml +="<MEDICALPAYMENTLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</MEDICALPAYMENTLIMIT>";
											else 
												MotorCOVElement.InnerXml +="<MEDICALPAYMENTLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</MEDICALPAYMENTLIMIT>";

											MotorCOVElement.InnerXml +="<MEDICALPAYMENTFULL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString().ToUpper())+"</MEDICALPAYMENTFULL>";
											MotorCOVElement.InnerXml +="<MEDICALPAYMENTEXCESS " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString().ToUpper())+"</MEDICALPAYMENTEXCESS>";
									
											MotorElement.InnerXml +="<MP_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</MP_PREMIUM>";
											break;	

										case "MEDPM1":
											if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString().ToUpper() != "REJECT")
											{
												MotorElement.InnerXml +="<MEDICALPAYMENT " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters("O1")+"</MEDICALPAYMENT>";
												MotorElement.InnerXml +="<license " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters("O1")+"</license>";
											}
											else
												MotorElement.InnerXml +="<license " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters("O2")+"</license>";
											
											if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString().IndexOf("Excess")>0)
											{
												MotorElement.InnerXml +="<MEDICALPAYMENT1 " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters("O11")+"</MEDICALPAYMENT1>";
												MotorElement.InnerXml +="<license1 " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters("O11")+"</license1>";
											}
											else if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString().IndexOf("$300")>0)
											{
												MotorElement.InnerXml +="<MEDICALPAYMENT1 " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters("O12")+"</MEDICALPAYMENT1>";
												MotorElement.InnerXml +="<license1 " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters("O12")+"</license1>";
											}
											else if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString().IndexOf("Full")>0)
											{
												MotorElement.InnerXml +="<MEDICALPAYMENT1 " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters("O13")+"</MEDICALPAYMENT1>";
												MotorElement.InnerXml +="<license1 " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters("O13")+"</license1>";
											}

											MotorElement.InnerXml +="<MEDPM1LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</MEDPM1LIMIT>";
											MotorElement.InnerXml +="<MP_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</MP_PREMIUM>";
											break;
										case "MEDPM2":
											MotorElement.InnerXml +="<MEDICALPAYMENT " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters("O2")+"</MEDICALPAYMENT>";
											MotorElement.InnerXml +="<MP_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</MP_PREMIUM>";
											break;
										case "OTC":
											MotorElement.InnerXml +="<COMPREHENSIVE_DED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</COMPREHENSIVE_DED>";
											MotorElement.InnerXml +="<CMP_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</CMP_PREMIUM>";
											break;

										case "PUMSP":
											if(CoverageDetails["LIMIT_1"].ToString()!="" )
											{
												if(dblBISPL> double.Parse(CoverageDetails["LIMIT_1"].ToString()))
												{
													MotorElement.InnerXml +="<UNINSMOTB " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("1")+"</UNINSMOTB>";
													MotorElement.InnerXml +="<UNINSBLIMIT1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</UNINSBLIMIT1>";
													MotorElement.InnerXml +="<UNINSBLIMIT2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_2"].ToString())+"</UNINSBLIMIT2>";
												}
											}
											if(CoverageDetails["LIMIT_1"].ToString()!="" )
											{
												MotorElement.InnerXml +="<UNINSUREDLIMITBEFORE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</UNINSUREDLIMITBEFORE>";
											}
											else
												MotorElement.InnerXml +="<UNINSUREDLIMITBEFORE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</UNINSUREDLIMITBEFORE>";
											
											if(CoverageDetails["LIMIT_2"].ToString()!="" )
											{
												MotorElement.InnerXml +="<UNINSUREDLIMITAFTER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_2"].ToString())+"</UNINSUREDLIMITAFTER>";
											}
											MotorElement.InnerXml +="<UIMOTO_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</UIMOTO_PREMIUM>";
											if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"].ToString() != "")
												dblPUMSP_limit1 = Convert.ToDouble(CoverageDetails["LIMIT_1"]);
											if(CoverageDetails["LIMIT_2"]!=null && CoverageDetails["LIMIT_2"].ToString() != "")
												dblPUMSP_limit2 = Convert.ToDouble(CoverageDetails["LIMIT_2"]);
											if(CoverageDetails["LIMIT1_AMOUNT_TEXT"]!=null)
												PUMSPreject = CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString();

											if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString() == "Reject")
												uninsmotb = "0";
											break;
								
										case "PUNCS":
											if(CoverageDetails["LIMIT_1"].ToString()!="" )
											{
												MotorCOVElement.InnerXml +="<UNINSUREDLIMITBEFORE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</UNINSUREDLIMITBEFORE>";
												if(dblRLCSL> double.Parse(CoverageDetails["LIMIT_1"].ToString()))
												{
													MotorElement.InnerXml +="<UNINSMOTP " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("1")+"</UNINSMOTP>";
													MotorElement.InnerXml +="<UNINSPLIMIT1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</UNINSPLIMIT1>";
													MotorElement.InnerXml +="<UNINSPDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</UNINSPDED>";
												}
											}
											else 
											{
												MotorCOVElement.InnerXml +="<UNINSUREDLIMITBEFORE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</UNINSUREDLIMITBEFORE>";
											}
											
											MotorElement.InnerXml +="<UIMOTO_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</UIMOTO_PREMIUM>";
											
											if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"].ToString() != "")
												dblPUNCS_limit1 = Convert.ToDouble(CoverageDetails["LIMIT_1"]);
											if(CoverageDetails["LIMIT_2"]!=null && CoverageDetails["LIMIT_2"].ToString() != "")
												dblPUNCS_limit2 = Convert.ToDouble(CoverageDetails["LIMIT_2"]);
											if(CoverageDetails["LIMIT1_AMOUNT_TEXT"]!=null)
												PUNCSreject = CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString();

											if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString() == "Reject")
												un_undinsmotb = "0";
											break;

										case "UNCSL":
											if(CoverageDetails["LIMIT_1"].ToString()!="")
												MotorCOVElement.InnerXml +="<CSLUNDERINSUREDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</CSLUNDERINSUREDLIMIT>";
											else 
												MotorCOVElement.InnerXml +="<CSLUNDERINSUREDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</CSLUNDERINSUREDLIMIT>";
											
											MotorElement.InnerXml +="<UNIMOTO_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</UNIMOTO_PREMIUM>";
										
											if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"]!=DBNull.Value && CoverageDetails["LIMIT_1"].ToString()!="")
												dblUNCSL_limit1 = Convert.ToDouble(CoverageDetails["LIMIT_1"]);
											if(CoverageDetails["LIMIT1_AMOUNT_TEXT"]!=null)
												UNCSLreject = CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString();

											if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString() == "Reject")
												undinsmot = "0";					
											break;
										
										case "UNDSP":
											if(CoverageDetails["LIMIT_1"].ToString()!="")
												MotorCOVElement.InnerXml +="<UNDERINSUREDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</UNDERINSUREDLIMIT>";
											else
												MotorCOVElement.InnerXml +="<UNDERINSUREDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</UNDERINSUREDLIMIT>";
											MotorCOVElement.InnerXml +="<UNDERINSUREDLIMITAFTER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_2"].ToString())+"</UNDERINSUREDLIMITAFTER>";
											MotorElement.InnerXml +="<UNIMOTO_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</UNIMOTO_PREMIUM>";
											
											if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"].ToString() != "")
												dblUNDSP_limit1 = Convert.ToDouble(CoverageDetails["LIMIT_1"]);
											if(CoverageDetails["LIMIT_2"]!=null && CoverageDetails["LIMIT_2"].ToString() != "")
												dblUNDSP_limit2 = Convert.ToDouble(CoverageDetails["LIMIT_2"]);
											if(CoverageDetails["LIMIT1_AMOUNT_TEXT"]!=null)
												UNDSPreject = CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString();

											if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString() == "Reject")
												undinsmotb = "0";
											break;	
									
	
										case "UMPD":
											if(CoverageDetails["LIMIT_1"].ToString()!="")
											{
												MotorCOVElement.InnerXml +="<UMPROPDAMAGELIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</UMPROPDAMAGELIMIT>";
												if(dblPD > double.Parse(CoverageDetails["LIMIT_1"].ToString()))
												{
													MotorElement.InnerXml +="<UNINSMOTP " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("1")+"</UNINSMOTP>";
													MotorElement.InnerXml +="<UNINSPLIMIT1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</UNINSPLIMIT1>";
													if(CoverageDetails["DEDUCTIBLE_1"].ToString() =="0" || CoverageDetails["DEDUCTIBLE_1"].ToString() =="" || CoverageDetails["DEDUCTIBLE_1"].ToString() =="0.00" || CoverageDetails["DEDUCTIBLE_1"].ToString() =="$0" || CoverageDetails["DEDUCTIBLE_1"].ToString() =="$0.00")
														MotorElement.InnerXml +="<UNINSPDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</UNINSPDED>";
													else
														MotorElement.InnerXml +="<UNINSPDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</UNINSPDED>";												}
											}
											else 
											{
												MotorCOVElement.InnerXml +="<UMPROPDAMAGELIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</UMPROPDAMAGELIMIT>";
											}
											if(CoverageDetails["DEDUCTIBLE_1"].ToString().CompareTo("0")==0)
												MotorCOVElement.InnerXml +="<UMPDFULL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+"F"+"</UMPDFULL>";
											else if(CoverageDetails["DEDUCTIBLE_1"].ToString().CompareTo("300")==0)
												MotorCOVElement.InnerXml +="<UMPD300 " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+"H"+"</UMPD300>";
	
											MotorElement.InnerXml +="<UIMOTO_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</UIMOTO_PREMIUM>";
											
											if(CoverageDetails["LIMIT_1"]!=DBNull.Value && CoverageDetails["LIMIT_1"].ToString() != "")
												dblUMPD_limit1 = Convert.ToDouble(CoverageDetails["LIMIT_1"].ToString());
											if(CoverageDetails["DEDUCTIBLE_1"]!=null && CoverageDetails["DEDUCTIBLE_1"].ToString() != "")
												dblUMPD_ded	   = Convert.ToDouble(CoverageDetails["DEDUCTIBLE_1"]);	
											if(CoverageDetails["LIMIT1_AMOUNT_TEXT"]!=null)
												UMPDreject=CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString();
									
											if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString() == "Reject")
												uninsmotp = "0";
											break;	

										case "COLL":
											MotorElement.InnerXml +="<COLLISION_DED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</COLLISION_DED>";
											if(CoverageDetails["DEDUCTIBLE_1"]==null || CoverageDetails["DEDUCTIBLE_1"].ToString()== "")
												MotorElement.InnerXml +="<COLLISIONCHK " + fieldType +"=\""+ fieldTypeText +"\">"+"N"+"</COLLISIONCHK>";
											else
												MotorElement.InnerXml +="<COLLISIONCHK " + fieldType +"=\""+ fieldTypeText +"\">"+"Y"+"</COLLISIONCHK>";
											
											MotorElement.InnerXml += "<COLL_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</COLL_PREMIUM>";
											break;
										case "ROAD":
											if(CoverageDetails["LIMIT_1"]!=null)
												MotorCOVElement.InnerXml +="<ROADSERVICE " + fieldType +"=\""+ fieldTypeText +"\">"+ "Y" +"</ROADSERVICE>";
											else
												MotorCOVElement.InnerXml +="<ROADSERVICE " + fieldType +"=\""+ fieldTypeText +"\">"+ "N" +"</ROADSERVICE>";
					
											MotorElement.InnerXml +="<ROADSERVICE_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</ROADSERVICE_PREMIUM>";
											break;

										case "PDC14":
											if(CoverageDetails["LIMIT_1"].ToString() !="")
												MotorElement.InnerXml +="<M14LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</M14LIMIT>";
											else 
												MotorElement.InnerXml +="<M14LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</M14LIMIT>";								
											
											MotorElement.InnerXml +="<M14_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</M14_PREMIUM>";
											break;

										case "EBM49":
											MotorElement.InnerXml +="<M49LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</M49LIMIT>";
											MotorElement.InnerXml +="<M49_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</M49_PREMIUM>";
											break;
								
										case "RREIM":
											MotorCOVElement.InnerXml +="<TRANS_NO " + fieldType +"=\""+ fieldTypeText +"\">"+ strVehNo +"</TRANS_NO>";
											MotorCOVElement.InnerXml +="<TE_RR_LIMITBEFORE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</TE_RR_LIMITBEFORE>";
											MotorCOVElement.InnerXml +="<TE_RR_LIMITAFTER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_2"].ToString())+"</TE_RR_LIMITAFTER>";
											MotorElement.InnerXml +="<TERR_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</TERR_PREMIUM>";
											break;
										case "LPD":
											MotorElement.InnerXml +="<LPD_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</LPD_PREMIUM>";
											break;	
									}
									rowindex++;
									if(rowindex == DSTempVehicle.Tables[0].Rows.Count)
									{
										if(uninsmotb == "0" && uninsmotp == "0")
											un_undinsmotb = "0";

										if(undinsmotb == "0")
											undinsmot = "0";

										if(uninsmotb != "-1")
											MotorCOVElement.InnerXml +="<UNINSMOTB " + fieldType +"=\""+ fieldTypeText +"\">"+uninsmotb+"</UNINSMOTB>";
										if(undinsmotb != "-1")
											MotorCOVElement.InnerXml +="<UNDINSMOTB " + fieldType +"=\""+ fieldTypeText +"\">"+undinsmotb+"</UNDINSMOTB>";
										if(uninsmotp != "-1")
											MotorCOVElement.InnerXml +="<UNINSMOTP " + fieldType +"=\""+ fieldTypeText +"\">"+uninsmotp+"</UNINSMOTP>";
										if(un_undinsmotb != "-1")
											MotorCOVElement.InnerXml +="<UN_UNDINSMOTB " + fieldType +"=\""+ fieldTypeText +"\">"+un_undinsmotb+"</UN_UNDINSMOTB>";
										if(undinsmot != "-1")
											MotorCOVElement.InnerXml +="<UNDINSMOT " + fieldType +"=\""+ fieldTypeText +"\">"+undinsmot+"</UNDINSMOT>";
									}
									#endregion
									if(gStrtemp == "temp")
									{
										sumTtl=0;
										foreach (XmlNode SumTotalNode in GetSumTotalPremium(MotoDetail["VEHICLE_ID"].ToString()))
										{
											if(getAttributeValue(SumTotalNode,"STEPPREMIUM")!=null && getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()!="" )
												sumTtl = double.Parse(getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()) ;						
										}
									}
									else
									{
										if(GetTotalRiskPremium(DSTempVehicle,"SUMTOTAL")!="")
										sumTtl = double.Parse(GetTotalRiskPremium(DSTempVehicle,"SUMTOTAL"));
									}
									if(sumTtl!=0)
										MotorElement.InnerXml +="<VEH_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+ (sumTtl + ".00")+"</VEH_PREMIUM>";
									}
								#endregion
							}
					
							#endregion COVERAGES
							MotoCtr++;
						}
						intpageno++;
						cycleCounter++;
					}
					MotoCtr=0;
					
					#endregion
				}
			}
			catch(Exception ex)
			{
				throw(new Exception("Error while creating Motorcycle XML.",ex));
			}
		}
	
	#endregion


		#region Code for Underwriting And General Info Xml Generation
		private void createMOTOUnderwritingGeneralXML()
		{
			try
			{
				if (gStrPdfFor == PDFForAcord)
				{
					#region setting Root General Information
				
					#endregion

					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFAutoUnderwritingDetails");
					gobjWrapper.ClearParameteres();

					//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAutoUnderwritingDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
					if(DSTempDataSet.Tables[0].Rows.Count >0)
					{
						#region AcordMOTO Page General Info
						XmlElement MotoGenInfoElement;
						MotoGenInfoElement = AcordPDFXML.CreateElement("GENERALINFO");
						AcordMotorRootElement.AppendChild(MotoGenInfoElement);
						MotoGenInfoElement.SetAttribute(fieldType,fieldTypeSingle);
				
						//--------------------
						MotoGenInfoElement.InnerXml +="<LICENSE " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["DRIVER_SUS_REVOKED"].ToString()) + "</LICENSE>";
						if(DSTempDataSet.Tables[0].Rows[0]["DRIVER_SUS_REVOKED"].ToString()=="1")
							MotoGenInfoElement.InnerXml +="<LICENSEREMARKS " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["DRIVER_SUS_REVOKED_MC_DESC"].ToString()) + "</LICENSEREMARKS>";
					
						MotoGenInfoElement.InnerXml +="<DRIVING " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_CONVICTED_CARELESS_DRIVE"].ToString()) + "</DRIVING>";
						if(DSTempDataSet.Tables[0].Rows[0]["IS_CONVICTED_CARELESS_DRIVE"].ToString()=="1")
							MotoGenInfoElement.InnerXml +="<DRIVINGREMARKS " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_CONVICTED_CARELESS_DRIVE_DESC"].ToString()) + "</DRIVINGREMARKS>";
					
						MotoGenInfoElement.InnerXml +="<ACCIDENTDRUGUSE " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_CONVICTED_ACCIDENT"].ToString()) + "</ACCIDENTDRUGUSE>";
						if(DSTempDataSet.Tables[0].Rows[0]["IS_CONVICTED_ACCIDENT"].ToString()=="1")
							MotoGenInfoElement.InnerXml +="<ACCIDENTDRUGUSEREMARKS " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_CONVICTED_ACCIDENT_DESC"].ToString()) + "</ACCIDENTDRUGUSEREMARKS>";
					
						MotoGenInfoElement.InnerXml +="<COMBUSSPARADE " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_COMMERCIAL_USE"].ToString()) + "</COMBUSSPARADE>";
						if(DSTempDataSet.Tables[0].Rows[0]["IS_COMMERCIAL_USE"].ToString()=="1")
							MotoGenInfoElement.InnerXml +="<COMBUSSPARADEREMARKS " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_COMMERCIAL_USE_DESC"].ToString()) + "</COMBUSSPARADEREMARKS>";
					
						MotoGenInfoElement.InnerXml +="<CYCLEUSEDFOR " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_USEDFOR_RACING"].ToString()) + "</CYCLEUSEDFOR>";
						if(DSTempDataSet.Tables[0].Rows[0]["IS_USEDFOR_RACING"].ToString()=="1")
							MotoGenInfoElement.InnerXml +="<CYCLEUSEDFORREMARKS " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_USEDFOR_RACING_DESC"].ToString()) + "</CYCLEUSEDFORREMARKS>";
					
						MotoGenInfoElement.InnerXml +="<CYCLECOST " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_COST_OVER_DEFINED_LIMIT"].ToString()) + "</CYCLECOST>";
						if(DSTempDataSet.Tables[0].Rows[0]["IS_COST_OVER_DEFINED_LIMIT"].ToString()=="1")
							MotoGenInfoElement.InnerXml +="<CYCLECOSTREMARKS " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_COST_OVER_DEFINED_LIMIT_DESC"].ToString()) + "</CYCLECOSTREMARKS>";
					
						MotoGenInfoElement.InnerXml +="<CYCLEWHEELS " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_MORE_WHEELS"].ToString()) + "</CYCLEWHEELS>";
						if(DSTempDataSet.Tables[0].Rows[0]["IS_MORE_WHEELS"].ToString()=="1")
							MotoGenInfoElement.InnerXml +="<CYCLEWHEELSREMARKS " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_MORE_WHEELS_DESC"].ToString()) + "</CYCLEWHEELSREMARKS>";
					
						MotoGenInfoElement.InnerXml +="<CYCLEWITHEXTENDEDFORK " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_EXTENDED_FORKS"].ToString()) + "</CYCLEWITHEXTENDEDFORK>";
						if(DSTempDataSet.Tables[0].Rows[0]["IS_EXTENDED_FORKS"].ToString()=="1")
							MotoGenInfoElement.InnerXml +="<CYCLEWITHEXTENDEDFORKREMARKS " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_EXTENDED_FORKS_DESC"].ToString()) + "</CYCLEWITHEXTENDEDFORKREMARKS>";
					
						MotoGenInfoElement.InnerXml +="<MODTOINCSPEED " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_MODIFIED_INCREASE_SPEED"].ToString()) + "</MODTOINCSPEED>";
						if(DSTempDataSet.Tables[0].Rows[0]["IS_MODIFIED_INCREASE_SPEED"].ToString()=="1")
							MotoGenInfoElement.InnerXml +="<MODTOINCSPEEDREMARKS " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_MODIFIED_INCREASE_SPEED_DESC"].ToString()) + "</MODTOINCSPEEDREMARKS>";
					
						MotoGenInfoElement.InnerXml +="<MODIFIEDORASSEMBLED " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_MODIFIED_KIT"].ToString()) + "</MODIFIEDORASSEMBLED>";
						if(DSTempDataSet.Tables[0].Rows[0]["IS_MODIFIED_KIT"].ToString()=="1")
							MotoGenInfoElement.InnerXml +="<MODIFIEDORASSEMBLEDREMARKS " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_MODIFIED_KIT_DESC"].ToString()) + "</MODIFIEDORASSEMBLEDREMARKS>";
					
						MotoGenInfoElement.InnerXml +="<OTHERTHANINSUREDORSALVAGE " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["SALVAGE_TITLE"].ToString()) + "</OTHERTHANINSUREDORSALVAGE>";
						if(DSTempDataSet.Tables[0].Rows[0]["SALVAGE_TITLE"].ToString()=="1")
							MotoGenInfoElement.InnerXml +="<OTHERTHANINSUREDORSALVAGEREMARKS " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["SALVAGE_TITLE_MC_DESC"].ToString()) + "</OTHERTHANINSUREDORSALVAGEREMARKS>";
					

						MotoGenInfoElement.InnerXml +="<LICENSEFORROADUSE " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_LICENSED_FOR_ROAD"].ToString()) + "</LICENSEFORROADUSE>";
						if(DSTempDataSet.Tables[0].Rows[0]["IS_LICENSED_FOR_ROAD"].ToString()=="1")
							MotoGenInfoElement.InnerXml +="<LICENSEFORROADUSEREMARKS " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_LICENSED_FOR_ROAD_DESC"].ToString()) + "</LICENSEFORROADUSEREMARKS>";
					
						MotoGenInfoElement.InnerXml +="<DECLINECANCELRENEW " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["COVERAGE_DECLINED"].ToString()) + "</DECLINECANCELRENEW>";
						if(DSTempDataSet.Tables[0].Rows[0]["IS_LICENSED_FOR_ROAD"].ToString()== "1")
							MotoGenInfoElement.InnerXml +="<DECLINECANCELRENEWREMARKS " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["COVERAGE_DECLINED_MC_DESC"].ToString()) + "</DECLINECANCELRENEWREMARKS>";
					
						MotoGenInfoElement.InnerXml +="<DISCOUNT " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["MULTI_POLICY_DISC_APPLIED"].ToString()) + "</DISCOUNT>";



						#endregion

					
			
					}
				}
			}
			catch(Exception ex)
			{
				throw(new Exception("Error while creating Underwriting XML.",ex));
			}
		}
		#endregion
 
		#region Code for Driver Information Accident and violation History
		
		private void createAcordMotorViolationHistory()
		{
			try
			{
				#region Driver Information
				if (gStrPdfFor == PDFForAcord)
				{
					DSTempDataSet.Clear(); 

					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFAutoOperatorDtls");
					gobjWrapper.ClearParameteres();

					//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAutoOperatorDtls " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			
					#region Acord Page
					if (DSTempDataSet.Tables[0].Rows.Count >0)
					{
						XmlElement AcordMOTODriver;
						AcordMOTODriver = AcordPDFXML.CreateElement("MOTODRIVERINFORMATION");
						AcordMotorRootElement.AppendChild(AcordMOTODriver);
						AcordMOTODriver.SetAttribute(fieldType,fieldTypeMultiple);
					
						if(stCode.Equals("IN")) 
						{
							
							AcordMOTODriver.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACCORDINDRIVER"));
							AcordMOTODriver.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACCORDINDRIVER"));
							AcordMOTODriver.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACCORDINDRIVEREXTN"));
							AcordMOTODriver.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACCORDINDRIVEREXTN"));
						}

						else if(stCode.Equals("MI"))
						{
							AcordMOTODriver.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACCORDMIDRIVER"));
							AcordMOTODriver.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACCORDMIDRIVER"));
							AcordMOTODriver.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACCORDMIDRIVEREXTN"));
							AcordMOTODriver.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACCORDMIDRIVEREXTN"));
						}



						int RowCounter = 0;
						foreach(DataRow Row in DSTempDataSet.Tables[0].Rows)
						{
							XmlElement AcordMOTODRIVERelement;
							AcordMOTODRIVERelement = AcordPDFXML.CreateElement("EMPLOYERINFO");
							AcordMOTODriver.AppendChild(AcordMOTODRIVERelement);
							AcordMOTODRIVERelement.SetAttribute(fieldType,fieldTypeNormal);
							AcordMOTODRIVERelement.SetAttribute(id,RowCounter.ToString());
						
							AcordMOTODRIVERelement.InnerXml +="<DRIVERNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DRIVER_NAME"].ToString()) + "</DRIVERNAME>";
							AcordMOTODRIVERelement.InnerXml +="<OCCUPATION " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(Row["GEN_OCCUPATION"].ToString()) + "</OCCUPATION>";
							if(Row["GEN_OCCUPATION"].ToString().CompareTo("EMPLOYED")==0)
								AcordMOTODRIVERelement.InnerXml +="<OCCUPATION_DESC " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(Row["OCCUPATION"].ToString()) + "</OCCUPATION_DESC>";
							AcordMOTODRIVERelement.InnerXml +="<VIOLATIONS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Refer to MVR Report") + "</VIOLATIONS>";
							AcordMOTODRIVERelement.InnerXml +="<LOSSESINCLCOMP " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters("Refer to A+ Report") + "</LOSSESINCLCOMP>";
						
							RowCounter++;
						}
					}
					#endregion
				}
				#endregion 
			}
			catch(Exception ex)
			{
				throw(new Exception("Error while creating Violation History XML.",ex));
			}
		}
		
		#endregion
		
		#region Code for option to REJECT or MODIFY
		private void createOptionRejectorModifyXML()
		{
			try
			{
				if (gStrPdfFor == PDFForAcord)
				{
					//DSTempDataSet.Clear(); 
					//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAuto_Coverage_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
					//				if(DSTempDataSet.Tables[0].Rows.Count >0)
					//				{
					#region AcordMOTO Option Info
					XmlElement MotoOptionInfoElement;
					MotoOptionInfoElement = AcordPDFXML.CreateElement("GENERALINFO");
					AcordMotorRootElement.AppendChild(MotoOptionInfoElement);
					MotoOptionInfoElement.SetAttribute(fieldType,fieldTypeSingle);
					#endregion
					if(dblPUNCS_limit1 < dblRLCSL) 
					{
						MotoOptionInfoElement.InnerXml +="<BI_REDUCEDLIMIT " + fieldType + "=\"" + fieldTypeCheckBox + "\">" +"1" + "</BI_REDUCEDLIMIT>";
						MotoOptionInfoElement.InnerXml +="<BI_LIMITBEFORE " + fieldType + "=\"" + fieldTypeText + "\">" + dblPUNCS_limit1 + "</BI_LIMITBEFORE>";
						MotoOptionInfoElement.InnerXml +="<BI_LIMITAFTER " + fieldType + "=\"" + fieldTypeText + "\">" +  dblPUNCS_limit2 + "</BI_LIMITAFTER>";

					}
					if(dblPUMSP_limit1 < dblBISPL)
					{
						MotoOptionInfoElement.InnerXml +="<BI_REDUCEDLIMIT " + fieldType + "=\"" + fieldTypeCheckBox + "\">" +"1" + "</BI_REDUCEDLIMIT>";
						MotoOptionInfoElement.InnerXml +="<BI_LIMITBEFORE " + fieldType + "=\"" + fieldTypeText + "\">" + dblPUMSP_limit1 + "</BI_LIMITBEFORE>";
						MotoOptionInfoElement.InnerXml +="<BI_LIMITAFTER " + fieldType + "=\"" + fieldTypeText + "\">" +  dblPUMSP_limit2 + "</BI_LIMITAFTER>";

					}


					if(dblUNCSL_limit1 < dblRLCSL) 
					{
						MotoOptionInfoElement.InnerXml +="<UNI_REDUCEDLIMIT " + fieldType + "=\"" + fieldTypeCheckBox + "\">" +"1" + "</UNI_REDUCEDLIMIT>";
						MotoOptionInfoElement.InnerXml +="<UNI_LIMITBEFORE " + fieldType + "=\"" + fieldTypeText + "\">" + dblUNCSL_limit1 + "</UNI_LIMITBEFORE>";
						MotoOptionInfoElement.InnerXml +="<UNI_LIMITAFTER " + fieldType + "=\"" + fieldTypeText + "\">" +  dblUNCSL_limit2 + "</UNI_LIMITAFTER>";

					}
					if(dblUNDSP_limit1 < dblBISPL)
					{
						MotoOptionInfoElement.InnerXml +="<UNI_REDUCEDLIMIT " + fieldType + "=\"" + fieldTypeCheckBox + "\">" +"1" + "</UNI_REDUCEDLIMIT>";
						MotoOptionInfoElement.InnerXml +="<UNI_LIMITBEFORE " + fieldType + "=\"" + fieldTypeText + "\">" + dblUNDSP_limit1 + "</UNI_LIMITBEFORE>";
						MotoOptionInfoElement.InnerXml +="<UNI_LIMITAFTER " + fieldType + "=\"" + fieldTypeText + "\">" +  dblUNDSP_limit2 + "</UNI_LIMITAFTER>";

					}

					if(dblPUNCS_limit1 < dblRLCSL)
					{
						MotoOptionInfoElement.InnerXml +="<UI_REDUCEDLIMIT " + fieldType + "=\"" + fieldTypeCheckBox + "\">" +"1" + "</UI_REDUCEDLIMIT>";
						MotoOptionInfoElement.InnerXml +="<UI_LIMITBEFORE " + fieldType + "=\"" + fieldTypeText + "\">" + dblPUNCS_limit1 + "</UI_LIMITBEFORE>";
						MotoOptionInfoElement.InnerXml +="<UI_LIMITAFTER " + fieldType + "=\"" + fieldTypeText + "\">" +  dblPUNCS_limit2 + "</UI_LIMITAFTER>";
					}

					if(dblUMPD_limit1 < dblBISPL)
					{
						MotoOptionInfoElement.InnerXml +="<UI_REDUCEDLIMIT " + fieldType + "=\"" + fieldTypeCheckBox + "\">" +"1" + "</UI_REDUCEDLIMIT>";
						MotoOptionInfoElement.InnerXml +="<UI_LIMITBEFORE " + fieldType + "=\"" + fieldTypeText + "\">" + dblUMPD_ded + "</UI_LIMITBEFORE>";

					}
					if(UMPDreject!=null)
						if(UMPDreject.ToUpper().CompareTo("REJECT")==0)
						{
							MotoOptionInfoElement.InnerXml +="<UMPDReject " + fieldType + "=\"" + fieldTypeCheckBox + "\">" +"1" + "</UMPDReject>";
					
						}
					if(PUMSPreject!=null)
						if(PUMSPreject.ToUpper().CompareTo("REJECT")==0)
						{
							MotoOptionInfoElement.InnerXml +="<UMBIPDReject " + fieldType + "=\"" + fieldTypeCheckBox + "\">" +"1" + "</UMBIPDReject>";
					
						}

					if(PUNCSreject!=null)
						if(PUNCSreject.ToUpper().CompareTo("REJECT")==0)
						{
							MotoOptionInfoElement.InnerXml +="<UMBIPDReject " + fieldType + "=\"" + fieldTypeCheckBox + "\">" +"1" + "</UMBIPDReject>";
					
						}
					if(UNDSPreject!=null)
						if(UNDSPreject.ToUpper().CompareTo("REJECT")==0)
						{
							MotoOptionInfoElement.InnerXml +="<UNIReject " + fieldType + "=\"" + fieldTypeCheckBox + "\">" +"1" + "</UNIReject>";
					
						}

					if(UNCSLreject!=null)
						if(UNCSLreject.ToUpper().CompareTo("REJECT")==0)
						{
							MotoOptionInfoElement.InnerXml +="<UNIReject " + fieldType + "=\"" + fieldTypeCheckBox + "\">" +"1" + "</UNIReject>";
					
						}
					XmlElement MotoFooterInfoElement;
					MotoFooterInfoElement = AcordPDFXML.CreateElement("GENERALINFO");
					AcordMotorRootElement.AppendChild(MotoFooterInfoElement);
					MotoFooterInfoElement.SetAttribute(fieldType,fieldTypeSingle);
				
					MotoFooterInfoElement.InnerXml +="<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNUMBER>";
					MotoFooterInfoElement.InnerXml +="<EFFECTIVE_DATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</EFFECTIVE_DATE>";
					MotoFooterInfoElement.InnerXml +="<EXPIRY_DATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</EXPIRY_DATE>";


				

			
										
				}
			}
			catch(Exception ex)
			{
				throw(new Exception("Error while creating Rejection XML.",ex));
			}
		}
		#endregion 
		

		#region Endorsement Wordings
		private void RemoveEnorsementWordings()
		{
			try
			{
				if (prnOrd_covCode==null) return;
				DataSet dsAutos = new DataSet();

				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				dsAutos = gobjWrapper.ExecuteDataSet("Proc_GetPDFAuto_Details");
				gobjWrapper.ClearParameteres();

				//dsAutos = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAuto_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
			
				foreach(DataRow MotoDetail in dsAutos.Tables[0].Rows)
				{
					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@VEHICLEID",MotoDetail["VEHICLE_ID"]);
					gobjWrapper.AddParameter("@RISKTYPE",MotoDetail["MOTORCYCLETYPE"]);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DataSet DSNewCoverageEndorsemet = gobjWrapper.ExecuteDataSet("Proc_GetPDFAuto_Coverage_Details");
					gobjWrapper.ClearParameteres();

					//DataSet DSNewCoverageEndorsemet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAuto_Coverage_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +MotoDetail["VEHICLE_ID"] +  ",'"+ MotoDetail["MOTORCYCLETYPE"] +"','" + gStrCalledFrom + "'");
					
					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@VEHICLEID",MotoDetail["VEHICLE_ID"]);
					gobjWrapper.AddParameter("@RISKTYPE",MotoDetail["MOTORCYCLETYPE"]);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DataSet DSOldCoverageEndorsemet = gobjWrapper.ExecuteDataSet("Proc_GetPDFAuto_Coverage_Details");
					gobjWrapper.ClearParameteres();
					
					//DataSet DSOldCoverageEndorsemet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAuto_Coverage_Details "  + gStrClientID + "," + gStrPolicyId + "," + strOldPolicyVer + "," +MotoDetail["VEHICLE_ID"] +  ",'"+ MotoDetail["MOTORCYCLETYPE"] +"','" + gStrCalledFrom + "'");
				
					foreach ( DataRow NewEndDetails in DSNewCoverageEndorsemet.Tables[0].Rows)
					{
						string CovCode=NewEndDetails["COV_CODE"].ToString();
						int counter = 0;
						DataRow[] drOld= DSOldCoverageEndorsemet.Tables[0].Select("COV_CODE='" + CovCode + "' AND ENDORS_ASSOC_COVERAGE='Y'");
						if( drOld.Length>0)
						{
							if(drOld[0]["EDITION_DATE"].ToString()==NewEndDetails["EDITION_DATE"].ToString())	
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
			try
			{
				if(gStrPdfFor == PDFForDecPage)
				{
					int endorsCount = 0;
					int counter = 0,Cntrl=0;
					int intCnt =0;
					int BlankPagePrinted=0;
					inttotalpage += intPrivacyPage;
					//check for even and odd number of pages
					inttotalpage = inttotalpage%2;
//					while(prnOrd_covCode[endorsCount] != null)
//						endorsCount++;
					endorsCount=prnOrd_covCode.Length;
					while(counter < endorsCount)
					{
						int lowestIndex = GetLowestPrnIndex(ref prnOrd, endorsCount);
						string prncovCode = prnOrd_covCode[lowestIndex];
						string prnAttFile = prnOrd_attFile[lowestIndex];
						if(prnAttFile != null && prnAttFile != "" && prncovCode !=null && prncovCode!="")
						{
							//					if(CoverageDetails["ENDORS_PRINT"].ToString() =="Y" )
							#region Commented Code
							/*	switch(prncovCode)
								{
										//MotorCycle Policy
									case "MPEMC":
									{
										region Dec Page Element
										XmlElement DecPageMotorCycleEndoMPEMC;
										DecPageMotorCycleEndoMPEMC = AcordPDFXML.CreateElement("MOTORCYCLEENDORSEMENTEMPEMC");
										RootElementDecPage.AppendChild(DecPageMotorCycleEndoMPEMC);
										DecPageMotorCycleEndoMPEMC.SetAttribute(fieldType,fieldTypeMultiple);
										if(prnAttFile != null && prnAttFile.ToString() != "")
										{
											DecPageMotorCycleEndoMPEMC.SetAttribute(PrimPDF,prnAttFile.ToString());
											DecPageMotorCycleEndoMPEMC.SetAttribute(PrimPDFBlocks,"1");
										}
										else
										{
											DecPageMotorCycleEndoMPEMC.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEMPEMC"));
											DecPageMotorCycleEndoMPEMC.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEMPEMC"));
										}
										DecPageMotorCycleEndoMPEMC.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEMPEMCEXTN"));
										DecPageMotorCycleEndoMPEMC.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEMPEMCEXTN"));

										XmlElement EndoElementMPEMC;
										EndoElementMPEMC = AcordPDFXML.CreateElement("ENDOELEMENTMPEMCINFO");
										DecPageMotorCycleEndoMPEMC.AppendChild(EndoElementMPEMC);
										EndoElementMPEMC.SetAttribute(fieldType,fieldTypeNormal);
										EndoElementMPEMC.SetAttribute(id,"0");
										endregion 
										break;
									}
										//Other than collision (Comprehensive)
									case "OTC":
									{
										region Dec Page Element
										XmlElement DecPageMotorCycleEndoOTC;
										DecPageMotorCycleEndoOTC = AcordPDFXML.CreateElement("MOTORCYCLEENDORSEMENTEOTC");
										RootElementDecPage.AppendChild(DecPageMotorCycleEndoOTC);
										DecPageMotorCycleEndoOTC.SetAttribute(fieldType,fieldTypeMultiple);
										if(prnAttFile != null && prnAttFile.ToString() != "")
										{
											DecPageMotorCycleEndoOTC.SetAttribute(PrimPDF,prnAttFile.ToString());
											DecPageMotorCycleEndoOTC.SetAttribute(PrimPDFBlocks,"1");
										}
										else
										{
											DecPageMotorCycleEndoOTC.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOTC"));
											DecPageMotorCycleEndoOTC.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOTC"));
										}
										DecPageMotorCycleEndoOTC.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEOTCEXTN"));
										DecPageMotorCycleEndoOTC.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOTCEXTN"));

										XmlElement EndoElementOTC;
										EndoElementOTC = AcordPDFXML.CreateElement("ENDOELEMENTOTCINFO");
										DecPageMotorCycleEndoOTC.AppendChild(EndoElementOTC);
										EndoElementOTC.SetAttribute(fieldType,fieldTypeNormal);
										EndoElementOTC.SetAttribute(id,"0");
										endregion 
										break;
									}
										//Collision
									case "COLL":
									{
										region Dec Page Element
										XmlElement DecPageMotorCycleEndoCOLL;
										DecPageMotorCycleEndoCOLL = AcordPDFXML.CreateElement("MOTORCYCLEENDORSEMENTECOLL");
										RootElementDecPage.AppendChild(DecPageMotorCycleEndoCOLL);
										DecPageMotorCycleEndoCOLL.SetAttribute(fieldType,fieldTypeMultiple);
										if(prnAttFile != null && prnAttFile.ToString() != "")
										{
											DecPageMotorCycleEndoCOLL.SetAttribute(PrimPDF,prnAttFile.ToString());
											DecPageMotorCycleEndoCOLL.SetAttribute(PrimPDFBlocks,"1");
										}
										else
										{
											DecPageMotorCycleEndoCOLL.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECOLL"));
											DecPageMotorCycleEndoCOLL.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGECOLL"));
										}
										DecPageMotorCycleEndoCOLL.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGECOLLEXTN"));
										DecPageMotorCycleEndoCOLL.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGECOLLEXTN"));

										XmlElement EndoElementCOLL;
										EndoElementCOLL = AcordPDFXML.CreateElement("ENDOELEMENTCOLLINFO");
										DecPageMotorCycleEndoCOLL.AppendChild(EndoElementCOLL);
										EndoElementCOLL.SetAttribute(fieldType,fieldTypeNormal);
										EndoElementCOLL.SetAttribute(id,"0");
										endregion 
										break;
									}
										//Additional Physical Damage coverage
									case "EBM14":
									{
										region Dec Page Element
										XmlElement DecPageMotorCycleEndoEBM14;
										DecPageMotorCycleEndoEBM14 = AcordPDFXML.CreateElement("MOTORCYCLEENDORSEMENTEEBM14");
										RootElementDecPage.AppendChild(DecPageMotorCycleEndoEBM14);
										DecPageMotorCycleEndoEBM14.SetAttribute(fieldType,fieldTypeMultiple);
										if(prnAttFile != null && prnAttFile.ToString() != "")
										{
											DecPageMotorCycleEndoEBM14.SetAttribute(PrimPDF,prnAttFile.ToString());
											DecPageMotorCycleEndoEBM14.SetAttribute(PrimPDFBlocks,"1");
										}
										else
										{
											DecPageMotorCycleEndoEBM14.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBM14"));
											DecPageMotorCycleEndoEBM14.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBM14"));
										}
										DecPageMotorCycleEndoEBM14.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBM14EXTN"));
										DecPageMotorCycleEndoEBM14.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBM14EXTN"));

										XmlElement EndoElementEBM14;
										EndoElementEBM14 = AcordPDFXML.CreateElement("ENDOELEMENTEBM14INFO");
										DecPageMotorCycleEndoEBM14.AppendChild(EndoElementEBM14);
										EndoElementEBM14.SetAttribute(fieldType,fieldTypeNormal);
										EndoElementEBM14.SetAttribute(id,"0");
										chkFlag14 = true;
										endregion 
										break;
									}
										//Additional Physical Damage coverage
									case "PDC14":
									{
										region Dec Page Element
										XmlElement DecPageMotorCycleEndoPDC14;
										DecPageMotorCycleEndoPDC14 = AcordPDFXML.CreateElement("MOTORCYCLEENDORSEMENTPDC14");
										RootElementDecPage.AppendChild(DecPageMotorCycleEndoPDC14);
										DecPageMotorCycleEndoPDC14.SetAttribute(fieldType,fieldTypeMultiple);
										if(prnAttFile != null && prnAttFile.ToString() != "")
										{
											DecPageMotorCycleEndoPDC14.SetAttribute(PrimPDF,prnAttFile.ToString());
											DecPageMotorCycleEndoPDC14.SetAttribute(PrimPDFBlocks,"1");
										}
										else
										{
											DecPageMotorCycleEndoPDC14.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEPDC14"));
											DecPageMotorCycleEndoPDC14.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEPDC14"));
										}
										DecPageMotorCycleEndoPDC14.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEPDC14EXTN"));
										DecPageMotorCycleEndoPDC14.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEPDC14EXTN"));
										// nsbisht
										XmlElement EndoElementPDC14;
										EndoElementPDC14 = AcordPDFXML.CreateElement("ENDOELEMENTPDC14INFO");
										DecPageMotorCycleEndoPDC14.AppendChild(EndoElementPDC14);
										EndoElementPDC14.SetAttribute(fieldType,fieldTypeNormal);
										EndoElementPDC14.SetAttribute(id,"0");
										chkFlag14 = true;
										endregion 
										break;
									}

										//Helmut and riding Apparel
									case "EBM15":
									{
										region Dec Page Element
										XmlElement DecPageMotorCycleEndoEBM15;
										DecPageMotorCycleEndoEBM15 = AcordPDFXML.CreateElement("MOTORCYCLEENDORSEMENTEEBM15");
										RootElementDecPage.AppendChild(DecPageMotorCycleEndoEBM15);
										DecPageMotorCycleEndoEBM15.SetAttribute(fieldType,fieldTypeMultiple);
										if(prnAttFile != null && prnAttFile.ToString() != "")
										{
											DecPageMotorCycleEndoEBM15.SetAttribute(PrimPDF,prnAttFile.ToString());
											DecPageMotorCycleEndoEBM15.SetAttribute(PrimPDFBlocks,"1");
										}
										else
										{
											DecPageMotorCycleEndoEBM15.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBM15"));
											DecPageMotorCycleEndoEBM15.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBM15"));
										}
										DecPageMotorCycleEndoEBM15.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBM15EXTN"));
										DecPageMotorCycleEndoEBM15.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBM15EXTN"));

										XmlElement EndoElementEBM15;
										EndoElementEBM15 = AcordPDFXML.CreateElement("ENDOELEMENTEBM15INFO");
										DecPageMotorCycleEndoEBM15.AppendChild(EndoElementEBM15);
										EndoElementEBM15.SetAttribute(fieldType,fieldTypeNormal);
										EndoElementEBM15.SetAttribute(id,"0");
										endregion 
										break;
									}
										//Helmut and riding Apparel
									case "HRAC15":
									{
										region Dec Page Element
										XmlElement DecPageMotorCycleEndoHRAC15;
										DecPageMotorCycleEndoHRAC15 = AcordPDFXML.CreateElement("MOTORCYCLEENDORSEMENTEHRAC15");
										RootElementDecPage.AppendChild(DecPageMotorCycleEndoHRAC15);
										DecPageMotorCycleEndoHRAC15.SetAttribute(fieldType,fieldTypeMultiple);
										if(prnAttFile != null && prnAttFile.ToString() != "")
										{
											DecPageMotorCycleEndoHRAC15.SetAttribute(PrimPDF,prnAttFile.ToString());
											DecPageMotorCycleEndoHRAC15.SetAttribute(PrimPDFBlocks,"1");
										}
										else
										{
											DecPageMotorCycleEndoHRAC15.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHRAC15"));
											DecPageMotorCycleEndoHRAC15.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHRAC15"));
										}
										DecPageMotorCycleEndoHRAC15.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHRAC15EXTN"));
										DecPageMotorCycleEndoHRAC15.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHRAC15EXTN"));

										XmlElement EndoElementHRAC15;
										EndoElementHRAC15 = AcordPDFXML.CreateElement("ENDOELEMENTHRAC15INFO");
										DecPageMotorCycleEndoHRAC15.AppendChild(EndoElementHRAC15);
										EndoElementHRAC15.SetAttribute(fieldType,fieldTypeNormal);
										EndoElementHRAC15.SetAttribute(id,"0");
										endregion 
										break;
									}

									//MotorCycle Trailer Endorsement(Other than Collision)
					
									case "EBM49":
									{
										region Dec Page Element
																
										XmlElement DecPageMotorCycleEndoEBM49;
										DecPageMotorCycleEndoEBM49 = AcordPDFXML.CreateElement("MOTORCYCLEENDORSEMENTEEBM49");
										RootElementDecPage.AppendChild(DecPageMotorCycleEndoEBM49);
										DecPageMotorCycleEndoEBM49.SetAttribute(fieldType,fieldTypeMultiple);
										if(prnAttFile != null && prnAttFile.ToString() != "")
										{
											DecPageMotorCycleEndoEBM49.SetAttribute(PrimPDF,prnAttFile.ToString());
											DecPageMotorCycleEndoEBM49.SetAttribute(PrimPDFBlocks,"1");
										}
										else
										{
											DecPageMotorCycleEndoEBM49.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBM49"));
											DecPageMotorCycleEndoEBM49.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBM49"));
										}
										DecPageMotorCycleEndoEBM49.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBM49EXTN"));
										DecPageMotorCycleEndoEBM49.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBM49EXTN"));

										// nsbisht
										XmlElement EndoElementEBM49;
										EndoElementEBM49 = AcordPDFXML.CreateElement("ENDOELEMENTEBM49INFO");
										DecPageMotorCycleEndoEBM49.AppendChild(EndoElementEBM49);
										EndoElementEBM49.SetAttribute(fieldType,fieldTypeNormal);
										EndoElementEBM49.SetAttribute(id,"0"); 
										chkFlag = true;
										endregion 
										break;
									}
					
										//MotorCycle Trailer Endorsement (Collision)
									case "CEBM49":
									{
										region Dec Page Element
																			
										XmlElement DecPageMotorCycleEndoEBM49;
										DecPageMotorCycleEndoEBM49 = AcordPDFXML.CreateElement("MOTORCYCLEENDORSEMENTCEBM49");
										RootElementDecPage.AppendChild(DecPageMotorCycleEndoEBM49);
										DecPageMotorCycleEndoEBM49.SetAttribute(fieldType,fieldTypeMultiple);
										if(prnAttFile != null && prnAttFile.ToString() != "")
										{
											DecPageMotorCycleEndoEBM49.SetAttribute(PrimPDF,prnAttFile.ToString());
											DecPageMotorCycleEndoEBM49.SetAttribute(PrimPDFBlocks,"1");
										}
										else
										{
											DecPageMotorCycleEndoEBM49.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECEBM49"));
											DecPageMotorCycleEndoEBM49.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGECEBM49"));
										}
										DecPageMotorCycleEndoEBM49.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBM49EXTN"));
										DecPageMotorCycleEndoEBM49.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBM49EXTN"));

										XmlElement EndoElementEBM49;
										EndoElementEBM49 = AcordPDFXML.CreateElement("ENDOELEMENTEBM49INFO");
										DecPageMotorCycleEndoEBM49.AppendChild(EndoElementEBM49);
										EndoElementEBM49.SetAttribute(fieldType,fieldTypeNormal);
										EndoElementEBM49.SetAttribute(id,"0");
										chkFlag = true;
										endregion 
										break;
									}

										//Road
									case "ROAD":
									{
										region Dec Page Element
										XmlElement DecPageMotorCycleEndoROAD;
										DecPageMotorCycleEndoROAD = AcordPDFXML.CreateElement("MOTORCYCLEENDORSEMENTROAD");
										RootElementDecPage.AppendChild(DecPageMotorCycleEndoROAD);
										DecPageMotorCycleEndoROAD.SetAttribute(fieldType,fieldTypeMultiple);
										if(prnAttFile != null && prnAttFile.ToString() != "")
										{
											DecPageMotorCycleEndoROAD.SetAttribute(PrimPDF,prnAttFile.ToString());
											DecPageMotorCycleEndoROAD.SetAttribute(PrimPDFBlocks,"1");
										}
										else
										{
											DecPageMotorCycleEndoROAD.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEROAD"));
											DecPageMotorCycleEndoROAD.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEROAD"));
										}
										DecPageMotorCycleEndoROAD.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEROADEXTN"));
										DecPageMotorCycleEndoROAD.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEROADEXTN"));
										// nsbisht
										XmlElement EndoElementROAD;
										EndoElementROAD = AcordPDFXML.CreateElement("ENDOELEMENTROADINFO");
										DecPageMotorCycleEndoROAD.AppendChild(EndoElementROAD);
										EndoElementROAD.SetAttribute(fieldType,fieldTypeNormal);
										EndoElementROAD.SetAttribute(id,"0");
											ndregion 
										break;
									}

									case "UNDSP":
									{
										region Dec Page Element
										XmlElement DecPageMotorCycleEndoUNDSP;
										DecPageMotorCycleEndoUNDSP = AcordPDFXML.CreateElement("MOTORCYCLEENDORSEMENTUNDSP");
										RootElementDecPage.AppendChild(DecPageMotorCycleEndoUNDSP);
										DecPageMotorCycleEndoUNDSP.SetAttribute(fieldType,fieldTypeMultiple);
										if(prnAttFile != null && prnAttFile.ToString() != "")
										{
											DecPageMotorCycleEndoUNDSP.SetAttribute(PrimPDF,prnAttFile.ToString());
											DecPageMotorCycleEndoUNDSP.SetAttribute(PrimPDFBlocks,"1");
										}
										else
										{
											DecPageMotorCycleEndoUNDSP.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEUNDSP"));
											DecPageMotorCycleEndoUNDSP.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEUNDSP"));
										}
										DecPageMotorCycleEndoUNDSP.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEUNDSPEXTN"));
										DecPageMotorCycleEndoUNDSP.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEUNDSPEXTN"));
										// nsbisht
										XmlElement EndoElementUNDSP;
										EndoElementUNDSP = AcordPDFXML.CreateElement("ENDOELEMENTUNDSPINFO");
										DecPageMotorCycleEndoUNDSP.AppendChild(EndoElementUNDSP);
										EndoElementUNDSP.SetAttribute(fieldType,fieldTypeNormal);
										EndoElementUNDSP.SetAttribute(id,"0");
								
										chkFlag16 = true;
										endregion 
										break;
									}
								
									case "E17":
									{
										region Dec Page Element
										XmlElement DecPageMotorCycleEndoE17;
										DecPageMotorCycleEndoE17 = AcordPDFXML.CreateElement("MOTORCYCLEENDORSEMENTE17");
										RootElementDecPage.AppendChild(DecPageMotorCycleEndoE17);
										DecPageMotorCycleEndoE17.SetAttribute(fieldType,fieldTypeMultiple);
										if(prnAttFile != null && prnAttFile.ToString() != "")
										{
											DecPageMotorCycleEndoE17.SetAttribute(PrimPDF,prnAttFile.ToString());
											DecPageMotorCycleEndoE17.SetAttribute(PrimPDFBlocks,"1");
										}
										else
										{
											DecPageMotorCycleEndoE17.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEE17"));
											DecPageMotorCycleEndoE17.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEE17"));
										}
										DecPageMotorCycleEndoE17.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEE17EXTN"));
										DecPageMotorCycleEndoE17.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEE17EXTN"));
										// nsbisht
										XmlElement EndoElementE17;
										EndoElementE17 = AcordPDFXML.CreateElement("ENDOELEMENTE17INFO");
										DecPageMotorCycleEndoE17.AppendChild(EndoElementE17);
										EndoElementE17.SetAttribute(fieldType,fieldTypeNormal);
										EndoElementE17.SetAttribute(id,"0");
										endregion 
										break;
									}
									case "RRUMM":
									{
										region Dec Page Element
										XmlElement DecPageMotorCycleEndoRRUMM;
										DecPageMotorCycleEndoRRUMM = AcordPDFXML.CreateElement("MOTORCYCLEENDORSEMENTRRUMM");
										RootElementDecPage.AppendChild(DecPageMotorCycleEndoRRUMM);
										DecPageMotorCycleEndoRRUMM.SetAttribute(fieldType,fieldTypeMultiple);
										if(prnAttFile != null && prnAttFile.ToString() != "")
										{
											DecPageMotorCycleEndoRRUMM.SetAttribute(PrimPDF,prnAttFile.ToString());
											DecPageMotorCycleEndoRRUMM.SetAttribute(PrimPDFBlocks,"1");
										}
										else
										{
											DecPageMotorCycleEndoRRUMM.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGERRUMM"));
											DecPageMotorCycleEndoRRUMM.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGERRUMM"));
										}
										DecPageMotorCycleEndoRRUMM.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGERRUMMEXTN"));
										DecPageMotorCycleEndoRRUMM.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGERRUMMEXTN"));
										// nsbisht
										XmlElement EndoElementRRUMM;
										EndoElementRRUMM = AcordPDFXML.CreateElement("ENDOELEMENTRRUMMINFO");
										DecPageMotorCycleEndoRRUMM.AppendChild(EndoElementRRUMM);
										EndoElementRRUMM.SetAttribute(fieldType,fieldTypeNormal);
										EndoElementRRUMM.SetAttribute(id,"0");
										endregion 
										break;
									}
									case "M10":
									{
										region Dec Page Element
										XmlElement DecPageMotorCycleEndoM10;
										DecPageMotorCycleEndoM10 = AcordPDFXML.CreateElement("MOTORCYCLEENDORSEMENTM10");
										RootElementDecPage.AppendChild(DecPageMotorCycleEndoM10);
										DecPageMotorCycleEndoM10.SetAttribute(fieldType,fieldTypeMultiple);
										if(prnAttFile != null && prnAttFile.ToString() != "")
										{
											DecPageMotorCycleEndoM10.SetAttribute(PrimPDF,prnAttFile.ToString());
											DecPageMotorCycleEndoM10.SetAttribute(PrimPDFBlocks,"1");
										}
										else
										{
											DecPageMotorCycleEndoM10.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEM10"));
											DecPageMotorCycleEndoM10.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEM10"));
										}
										DecPageMotorCycleEndoM10.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEM10EXTN"));
										DecPageMotorCycleEndoM10.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEM10EXTN"));
										// nsbisht
										XmlElement EndoElementM10;
										EndoElementM10 = AcordPDFXML.CreateElement("ENDOELEMENTM10INFO");
										DecPageMotorCycleEndoM10.AppendChild(EndoElementM10);
										EndoElementM10.SetAttribute(fieldType,fieldTypeNormal);
										EndoElementM10.SetAttribute(id,"0");
										endregion 
										break;
									}
									case "UNCSL":
									{
										region Dec Page Element
										XmlElement DecPageMotorCycleEndoUNCSL;
										DecPageMotorCycleEndoUNCSL = AcordPDFXML.CreateElement("MOTORCYCLEENDORSEMENTUNCSL");
										RootElementDecPage.AppendChild(DecPageMotorCycleEndoUNCSL);
										DecPageMotorCycleEndoUNCSL.SetAttribute(fieldType,fieldTypeMultiple);
										if(prnAttFile != null && prnAttFile.ToString() != "")
										{
											DecPageMotorCycleEndoUNCSL.SetAttribute(PrimPDF,prnAttFile.ToString());
											DecPageMotorCycleEndoUNCSL.SetAttribute(PrimPDFBlocks,"1");
										}
										else
										{
											DecPageMotorCycleEndoUNCSL.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEUNCSL"));
											DecPageMotorCycleEndoUNCSL.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEUNCSL"));
										}
										DecPageMotorCycleEndoUNCSL.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEUNCSLEXTN"));
										DecPageMotorCycleEndoUNCSL.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEUNCSLEXTN"));
										// nsbisht
										XmlElement EndoElementUNCSL;
										EndoElementUNCSL = AcordPDFXML.CreateElement("ENDOELEMENTUNCSLINFO");
										DecPageMotorCycleEndoUNCSL.AppendChild(EndoElementUNCSL);
										EndoElementUNCSL.SetAttribute(fieldType,fieldTypeNormal);
										EndoElementUNCSL.SetAttribute(id,"0");
										endregion 
										break;
									}
									default :
									{*/
							#endregion
							XmlElement DecPageMotoEndoCyl;
							XmlElement EndoElementCYCL;
							if(BlankPagePrinted==0 && inttotalpage!=0)
							{
								DecPageMotoEndoCyl = AcordPDFXML.CreateElement("MOTOENDORSEMENTCYCL" + "_" + 0);
								RootElementDecPage.AppendChild(DecPageMotoEndoCyl);
								DecPageMotoEndoCyl.SetAttribute(fieldType,fieldTypeMultiple);

								DecPageMotoEndoCyl.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEBLANKDOCUMENT"));
								DecPageMotoEndoCyl.SetAttribute(PrimPDFBlocks,"1");

								DecPageMotoEndoCyl.SetAttribute(SecondPDF,getAcordPDFBlockFromXML("DECPAGEBLANKDOCUMENT"));
								DecPageMotoEndoCyl.SetAttribute(SecondPDFBlocks,"1");

								
								EndoElementCYCL = AcordPDFXML.CreateElement("ENDOELEMENTCYCLINFO" + "_" + 0);
								DecPageMotoEndoCyl.AppendChild(EndoElementCYCL);
								EndoElementCYCL.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementCYCL.SetAttribute(id,"0");
								BlankPagePrinted++;
								//intCnt++;
							}
							
							if( inttotalpage!=0)
							{
								Cntrl=intCnt+1;
								//XmlElement DecPageMotoEndoCyl;
								DecPageMotoEndoCyl = AcordPDFXML.CreateElement("MOTOENDORSEMENTCYCL" + "_" + Cntrl);
								RootElementDecPage.AppendChild(DecPageMotoEndoCyl);
								DecPageMotoEndoCyl.SetAttribute(fieldType,fieldTypeMultiple);

								DecPageMotoEndoCyl.SetAttribute(PrimPDF,prnAttFile);
								DecPageMotoEndoCyl.SetAttribute(PrimPDFBlocks,"1");

								DecPageMotoEndoCyl.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEDPWORDEXTN"));
								DecPageMotoEndoCyl.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDPWORDEXTN"));

								//XmlElement EndoElementCYCL;
								EndoElementCYCL = AcordPDFXML.CreateElement("ENDOELEMENTCYCLINFO" + "_" + Cntrl);
								DecPageMotoEndoCyl.AppendChild(EndoElementCYCL);
								EndoElementCYCL.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementCYCL.SetAttribute(id,"0");
								intCnt++;
							}
							else
							{
								//XmlElement DecPageMotoEndoCyl;
								DecPageMotoEndoCyl = AcordPDFXML.CreateElement("MOTOENDORSEMENTCYCL" + "_" + intCnt);
								RootElementDecPage.AppendChild(DecPageMotoEndoCyl);
								DecPageMotoEndoCyl.SetAttribute(fieldType,fieldTypeMultiple);

								DecPageMotoEndoCyl.SetAttribute(PrimPDF,prnAttFile);
								DecPageMotoEndoCyl.SetAttribute(PrimPDFBlocks,"1");

								DecPageMotoEndoCyl.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEDPWORDEXTN"));
								DecPageMotoEndoCyl.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDPWORDEXTN"));

								//XmlElement EndoElementCYCL;
								EndoElementCYCL = AcordPDFXML.CreateElement("ENDOELEMENTCYCLINFO" + "_" + intCnt);
								DecPageMotoEndoCyl.AppendChild(EndoElementCYCL);
								EndoElementCYCL.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementCYCL.SetAttribute(id,"0");
								intCnt++;
							}
							//															}
							//																break;
							//														}
						}
								counter++;
					}
				}
			}
			catch(Exception ex)
			{
				throw(new Exception("Error while creating Endorsement wording XML.",ex));
			}
		}

		#endregion Endorsement Wordings

		#region Addition Wordings
		private void createAddWordingsXML()
		{
			string lob_id="3";
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

						RootElementDecPage.AppendChild(DecAddWordingsElement);
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
		
		// Added by Mohit Agarwal 13-Dec, ITrack 3211
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
			//string strAdverseRep="";
			intPrivacyPage=1;
			XmlElement DecPageInfoElement;
			DecPageInfoElement = AcordPDFXML.CreateElement("PAGE2INFO");
			DecAddPageElement.AppendChild(DecPageInfoElement);
			DecPageInfoElement.SetAttribute(fieldType,fieldTypeNormal);
			DecPageInfoElement.SetAttribute(id,"0");
			if(strInsuScore =="-2")
			{
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_hd1 " + fieldType + "=\"" + fieldTypeText + "\">" +RemoveJunkXmlCharacters("FCRA ADVERSE ACTION DISCLOSURE 15 USC 1681m & 16 CFR 601,")+ "</fcra_hd1>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_hd2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("MIB Nos.  2003-1 & 2003-2 & IC 27-2-21 Sec. 19") + "</fcra_hd2>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Based upon information contained in a consumer report supplied by Trans Union, P. O. Box 1000, Chester, PA 19022, toll free telephone number 800-645-1938, Wolverine Mutual Insurance Company has calculated your insurance score and determined your premium discounts which you will find on your declarations page. Trans Union did not make the decision to take this action and cannot give you the specific reason for it. However, you have a right to dispute directly to Trans Union the accuracy or completeness of any information furnished by it, and you have a right to obtain a free copy of the consumer report from Trans Union if you request it not later than 60 days after receipt of this notice.") + "</fcra_txt>";
				
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_reas " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</fcra_reas>";
				DecPageInfoElement.InnerXml +="<reason_code1 " + fieldType + "=\"" + fieldTypeText + "\">" + "" + "</reason_code1>";
				DecPageInfoElement.InnerXml +="<reason_code2 " + fieldType + "=\"" + fieldTypeText + "\">" + "" + "</reason_code2>";
				DecPageInfoElement.InnerXml +="<reason_code3 " + fieldType + "=\"" + fieldTypeText + "\">" + "" + "</reason_code3>";
				DecPageInfoElement.InnerXml +="<reason_code4 " + fieldType + "=\"" + fieldTypeText + "\">" + "" + "</reason_code4>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bullet1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</bullet1>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bullet2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</bullet2>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bullet3 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</bullet3>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bullet4 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</bullet4>";
			}
			else
			{
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_hd1 " + fieldType + "=\"" + fieldTypeText + "\">" +RemoveJunkXmlCharacters("FCRA ADVERSE ACTION DISCLOSURE 15 USC 1681m & 16 CFR 601,")+ "</fcra_hd1>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_hd2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("MIB Nos.  2003-1 & 2003-2 & IC 27-2-21 Sec. 19") + "</fcra_hd2>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Based upon information contained in a consumer report supplied by Trans Union, P. O. Box 1000, Chester, PA 19022, toll free telephone number 800-645-1938, Wolverine Mutual Insurance Company has calculated your insurance score and determined your premium discounts which you will find on your declarations page. Trans Union did not make the decision to take this action and cannot give you the specific reason for it. However, you have a right to dispute directly to Trans Union the accuracy or completeness of any information furnished by it, and you have a right to obtain a free copy of the consumer report from Trans Union if you request it not later than 60 days after receipt of this notice.") + "</fcra_txt>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_reas " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("The following factors in your credit history identified by Trans Union were the primary influences in determining your insurance score and associated premium discount:") + "</fcra_reas>";
			
				DecPageInfoElement.InnerXml +="<reason_code1 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code1 + "</reason_code1>";
				DecPageInfoElement.InnerXml +="<reason_code2 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code2 + "</reason_code2>";
				DecPageInfoElement.InnerXml +="<reason_code3 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code3 + "</reason_code3>";
				DecPageInfoElement.InnerXml +="<reason_code4 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code4 + "</reason_code4>";
			}
			
		}
		private void createPage2PrivacyPageXML(ref XmlElement DecPage1Element)
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
			//string strAdverseRep="";
			intPrivacyPage=1;
			XmlElement DecPageInfoElement;
			DecPageInfoElement = AcordPDFXML.CreateElement("PAGE2INFO");
			DecAddPageElement.AppendChild(DecPageInfoElement);
			DecPageInfoElement.SetAttribute(fieldType,fieldTypeNormal);
			DecPageInfoElement.SetAttribute(id,"0");
						
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_hd1 " + fieldType + "=\"" + fieldTypeText + "\">" +RemoveJunkXmlCharacters("")+ "</fcra_hd1>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_hd2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</fcra_hd2>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</fcra_txt>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_reas " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</fcra_reas>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bullet1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</bullet1>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bullet2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</bullet2>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bullet3 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</bullet3>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bullet4 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</bullet4>";
								
						
		}
		private void createPage2AdverseXML(ref XmlElement DecPage1Element)
		{
			XmlElement DecAddPageElement;
			DecAddPageElement = AcordPDFXML.CreateElement("ADDPAGE2");

			DecPage1Element.AppendChild(DecAddPageElement);
			DecAddPageElement.SetAttribute(fieldType,fieldTypeMultiple);
			DecAddPageElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEPAGE2"));
			DecAddPageElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEPAGE2"));
			DecAddPageElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEPAGE2"));
			DecAddPageElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEPAGE2"));

			// if Proccess is endorsement or Renewal then we wil not print adverse section

					
			//int RowCounter = 0;
			string strAdverseRep="";
			intPrivacyPage=1;
			XmlElement DecPageInfoElement;
			DecPageInfoElement = AcordPDFXML.CreateElement("PAGE2INFO");
			DecAddPageElement.AppendChild(DecPageInfoElement);
			DecPageInfoElement.SetAttribute(fieldType,fieldTypeNormal);
			DecPageInfoElement.SetAttribute(id,"0");
			DataSet dsTempPolicy = new DataSet();

			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			dsTempPolicy = gobjWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
			gobjWrapper.ClearParameteres();

			//dsTempPolicy = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			strAdverseRep = dsTempPolicy.Tables[1].Rows[0]["ADVERSE_LETTER_REQD"].ToString();
			
			
			///////////////////////
			///
			//"<not_pol " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_pol>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<priv_pol " + fieldType + "=\"" + fieldTypeText + "\">"  + RemoveJunkXmlCharacters("") + "</priv_pol>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<priv_text " + fieldType + "=\"" + fieldTypeText + "\">" +RemoveJunkXmlCharacters("")+ "</priv_text>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_coll " + fieldType + "=\"" + fieldTypeText + "\">"+ RemoveJunkXmlCharacters("") + "</inf_coll>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<infr_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</infr_txt>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bull1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") +"</bull1>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_bull1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") +"</inf_bull1>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_bull2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</inf_bull2>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_bull3 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</inf_bull3>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_disc " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</inf_disc>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</inf_txt>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<wolv_mut " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</wolv_mut>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<wolv_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</wolv_txt>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<sec_proc " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</sec_proc>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<sec_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</sec_txt>";
					
			
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_hd1 " + fieldType + "=\"" + fieldTypeText + "\">" +RemoveJunkXmlCharacters("FCRA ADVERSE ACTION DISCLOSURE 15 USC 1681m & 16 CFR 601,")+ "</fcra_hd1>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_hd2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("MIB Nos.  2003-1 & 2003-2 & IC 27-2-21 Sec. 19") + "</fcra_hd2>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Based upon information contained in a consumer report supplied by Trans Union, P. O. Box 1000, Chester, PA 19022, toll free telephone number 800-645-1938, Wolverine Mutual Insurance Company has calculated your insurance score and determined your premium discounts which you will find on your declarations page. Trans Union did not make the decision to take this action and cannot give you the specific reason for it. However, you have a right to dispute directly to Trans Union the accuracy or completeness of any information furnished by it, and you have a right to obtain a free copy of the consumer report from Trans Union if you request it not later than 60 days after receipt of this notice.") + "</fcra_txt>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_reas " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("The following factors in your credit history identified by Trans Union were the primary influences in determining your insurance score and associated premium discount:") + "</fcra_reas>";

			DecPageInfoElement.InnerXml +="<reason_code1 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code1 + "</reason_code1>";
			DecPageInfoElement.InnerXml +="<reason_code2 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code2 + "</reason_code2>";
			DecPageInfoElement.InnerXml +="<reason_code3 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code3 + "</reason_code3>";
			DecPageInfoElement.InnerXml +="<reason_code4 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code4 + "</reason_code4>";
				
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<not_pol " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_pol>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<not_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_txt>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<not_txt1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_txt1>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<not_txt2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_txt2>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<ins_add " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</ins_add>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<hotline " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</hotline>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<website " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</website>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<foot_left " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</foot_left>";
				

			
		}
		private void createPage2NHNSAdverseXML(ref XmlElement DecPage1Element)
		{
			XmlElement DecAddPageElement;
			DecAddPageElement = AcordPDFXML.CreateElement("ADDPAGE2");

			DecPage1Element.AppendChild(DecAddPageElement);
			DecAddPageElement.SetAttribute(fieldType,fieldTypeMultiple);
			DecAddPageElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEPAGE2"));
			DecAddPageElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEPAGE2"));
			DecAddPageElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEPAGE2"));
			DecAddPageElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEPAGE2"));

			// if Proccess is endorsement or Renewal then we wil not print adverse section

					
			//int RowCounter = 0;
			string strAdverseRep="";
			intPrivacyPage=1;
			XmlElement DecPageInfoElement;
			DecPageInfoElement = AcordPDFXML.CreateElement("PAGE2INFO");
			DecAddPageElement.AppendChild(DecPageInfoElement);
			DecPageInfoElement.SetAttribute(fieldType,fieldTypeNormal);
			DecPageInfoElement.SetAttribute(id,"0");
			DataSet dsTempPolicy = new DataSet();

			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			dsTempPolicy = gobjWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
			gobjWrapper.ClearParameteres();

			//dsTempPolicy = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			strAdverseRep = dsTempPolicy.Tables[1].Rows[0]["ADVERSE_LETTER_REQD"].ToString();
			
			///////////////////////
			///
			//"<not_pol " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_pol>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<priv_pol " + fieldType + "=\"" + fieldTypeText + "\">"  + RemoveJunkXmlCharacters("") + "</priv_pol>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<priv_text " + fieldType + "=\"" + fieldTypeText + "\">" +RemoveJunkXmlCharacters("")+ "</priv_text>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_coll " + fieldType + "=\"" + fieldTypeText + "\">"+ RemoveJunkXmlCharacters("") + "</inf_coll>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<infr_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</infr_txt>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bull1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") +"</bull1>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_bull1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") +"</inf_bull1>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_bull2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</inf_bull2>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_bull3 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</inf_bull3>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_disc " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</inf_disc>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</inf_txt>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<wolv_mut " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</wolv_mut>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<wolv_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</wolv_txt>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<sec_proc " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</sec_proc>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<sec_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</sec_txt>";
			
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_hd1 " + fieldType + "=\"" + fieldTypeText + "\">" +RemoveJunkXmlCharacters("FCRA ADVERSE ACTION DISCLOSURE 15 USC 1681m & 16 CFR 601,")+ "</fcra_hd1>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_hd2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("MIB Nos.  2003-1 & 2003-2 & IC 27-2-21 Sec. 19") + "</fcra_hd2>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Based upon information contained in a consumer report supplied by Trans Union, P. O. Box 1000, Chester, PA 19022, toll free telephone number 800-645-1938, Wolverine Mutual Insurance Company has calculated your insurance score and determined your premium discounts which you will find on your declarations page. Trans Union did not make the decision to take this action and cannot give you the specific reason for it. However, you have a right to dispute directly to Trans Union the accuracy or completeness of any information furnished by it, and you have a right to obtain a free copy of the consumer report from Trans Union if you request it not later than 60 days after receipt of this notice.") + "</fcra_txt>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_reas " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</fcra_reas>";

			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bullet1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</bullet1>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bullet2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</bullet2>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bullet3 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</bullet3>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bullet4 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</bullet4>";
			
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<not_pol " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_pol>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<not_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_txt>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<not_txt1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_txt1>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<not_txt2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</not_txt2>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<ins_add " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</ins_add>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<hotline " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</hotline>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<website " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</website>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<foot_left " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</foot_left>";
				

			
		}
		private string ChkPreInsuScr()
		{

			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",strOldPolicyVer);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DataSet dsoldInsuScr = gobjWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
			gobjWrapper.ClearParameteres();

			//DataSet dsoldInsuScr = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + strOldPolicyVer + ",'" + gStrCalledFrom + "'");
			
			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DataSet dsnewInsuScr = gobjWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
			gobjWrapper.ClearParameteres();

			//DataSet dsnewInsuScr = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			
			newInsuScr = dsnewInsuScr.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString();
			oldInsuScr = dsoldInsuScr.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString();
			if(Convert.ToDouble(dsnewInsuScr.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString()) < Convert.ToDouble(dsoldInsuScr.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString()))
			{
				strNeedPage2 ="Y";
				return strNeedPage2;
			}
			else if(Convert.ToDouble(dsnewInsuScr.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString()) >= Convert.ToDouble(dsoldInsuScr.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString()))
			{
				strNeedPage2 ="N";
				return strNeedPage2;
			}
			else
			{
				return strNeedPage2;
			}
		}
		#endregion
//		// Added by Mohit Agarwal 13-Dec, ITrack 3211
//		#region Page 2
//		private void createPage2XML(ref XmlElement DecPage1Element)
//		{
//			XmlElement DecAddPageElement;
//			DecAddPageElement = AcordPDFXML.CreateElement("ADDPAGE2");
//
//			DecPage1Element.AppendChild(DecAddPageElement);
//			DecAddPageElement.SetAttribute(fieldType,fieldTypeMultiple);
//			DecAddPageElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEPAGE2"));
//			DecAddPageElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEPAGE2"));
//			DecAddPageElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEPAGE2"));
//			DecAddPageElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEPAGE2"));
//
//			int RowCounter = 0;
//			XmlElement DecPageInfoElement;
//			DecPageInfoElement = AcordPDFXML.CreateElement("PAGE2INFO");
//			DecAddPageElement.AppendChild(DecPageInfoElement);
//			DecPageInfoElement.SetAttribute(fieldType,fieldTypeNormal);
//			DecPageInfoElement.SetAttribute(id,"0");
//
//			DecPageInfoElement.InnerXml +="<reason_code1 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code1 + "</reason_code1>";
//			DecPageInfoElement.InnerXml +="<reason_code2 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code2 + "</reason_code2>";
//			DecPageInfoElement.InnerXml +="<reason_code3 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code3 + "</reason_code3>";
//			DecPageInfoElement.InnerXml +="<reason_code4 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code4 + "</reason_code4>";
//		}
//		#endregion

	}
}





