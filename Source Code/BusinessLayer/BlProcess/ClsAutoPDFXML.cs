using System;
using System.Xml;
using System.Data;
using Cms.DataLayer;
using System.Collections;


namespace Cms.BusinessLayer.BlProcess
{
	/// <summary>
	/// Summary description for ClsAutoPDFXML.
	/// </summary>
	public class ClsAutoPDFXML : ClsCommonPdf
	{

		#region Declarations
		private XmlElement DecPageRootElement;
		private XmlElement Acord90RootElement;
		private XmlElement SupplementalRootElement;
		private Hashtable htpremium=new Hashtable(); 
		private Hashtable htSumTotal=new Hashtable(); 
		private Hashtable htpremium_dis=new Hashtable(); 
		private Hashtable htpremium_sur=new Hashtable(); 
		
		private   DataSet DSTempPolicyDataSet= new DataSet() ;		
		private   DataSet DSTempApplicantDataSet = new DataSet();
		private   DataSet DstempDocument = new DataSet();
		private   DataSet DSAddWordSet = new DataSet();
		private   DataSet DSTempAutoDetailDataSet = new DataSet();
		private   DataSet DSTempRateDataSet = new DataSet();
		private   DataSet DSTempUnderWritDataSet = new DataSet();
		private   DataSet DSTempPriorLossDataSet = new DataSet();
		private   DataSet DSTempAddIntrst = new DataSet();
		private   DataSet DSTempEquipment = new DataSet();
		private   DataSet DSTempOperators = new DataSet();

		private DataWrapper objDataWrapper;
		private string stCode="";
		private string strSubLine="",strPolicyType="";
		private string strInsScore="",strInsType="",strUwTier="";
		private string strInsuScore="";
		private string strTransport="",strSalvage="",strVolunteer="",strUSCitizen="",strLicensed="";
		private string strHealthCare="";
		private string strrecdPremium="";
		private string in_military = "0";
		private string driv_no_military = "";						
		private string inspercent = "";
		private double recv_prem = 0.0;
		private string []prnOrd_covCode;
		private string []prnOrd_attFile;
		private int []prnOrd;
		private string NumAgencyCode="";
		private string InsScrDis="";
		string newInsuScr="";
		string oldInsuScr="";
		private string strOldPolicyVer="";
		private double dblOldSum=0;
		private string expiry_date = "";
		private string eff_date = "";
		private string goldVewrsionId="0";
		string NamedInsuredWithSuffix="";
		int intPrivacyPage = 0;
		int inttotalpage=0;
		int flgError=0;
		string strModXml="";
		private  string outXml;
		#endregion

		public ClsAutoPDFXML(string lstrClientId,string lstrPolicyId,string lstrVersionId,string lStrCalledFrom,string lStrCalledFor,string stateCode,string strProcessID,string Agn_Ins,string temp, string loutXml, DataWrapper objDatWrapper,DataSet lDSTempPolicyDataSet,DataSet lDSTempApplicantDataSet,DataSet lDstempDocument,DataSet lDSAddWordSet,DataSet lDSTempAutoDetailDataSet,DataSet lDSTempRateDataSet,DataSet lDSTempPriorLossDataSet,DataSet lDSTempUnderWritDataSet, DataSet lDSTempAddIntrst, DataSet lDSTempEquipment, DataSet lDSTempOperators)
		{
			gStrClientID=lstrClientId;
			gStrPolicyId=lstrPolicyId;
			gStrPolicyVersion=lstrVersionId;
			gStrCalledFrom=lStrCalledFrom.ToUpper().Trim();
			gStrPdfFor=lStrCalledFor.ToUpper().Trim();
			stCode=stateCode;
			gStrProcessID = strProcessID;
			gStrCopyTo = Agn_Ins;
			if(Agn_Ins != null && Agn_Ins != "")
			{
				string []copyTo = Agn_Ins.Split('-');
				if(copyTo[0]=="AGENCY")
				CopyTo= "AGENT'S"+ " "+ "COPY" ;
				else
				CopyTo= copyTo[0]+"'S"+ " "+ "COPY" ;
			}
			gStrtemp = temp;
			DSTempDataSet = new DataSet();
			this.objDataWrapper=objDatWrapper;
			DSTempPolicyDataSet = lDSTempPolicyDataSet.Copy();
			DSTempApplicantDataSet = lDSTempApplicantDataSet.Copy();
			DstempDocument = lDstempDocument.Copy();
			DSAddWordSet = lDSAddWordSet.Copy();
			DSTempAutoDetailDataSet = lDSTempAutoDetailDataSet.Copy();
			DSTempRateDataSet = lDSTempRateDataSet.Copy();
			DSTempUnderWritDataSet = lDSTempUnderWritDataSet.Copy();
			DSTempPriorLossDataSet = lDSTempPriorLossDataSet.Copy();
			DSTempAddIntrst = lDSTempAddIntrst.Copy();
			DSTempEquipment = lDSTempEquipment.Copy();
			DSTempOperators = lDSTempOperators.Copy();
			outXml = loutXml;
			//if(objDataWrapper==null)
			//	objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
			//if(DSTempPolicyDataSet==null && DSTempPolicyDataSet.Tables.Count<=0)
			objDataWrapper.ClearParameteres();
//			objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//			objDataWrapper.AddParameter("@POLID",gStrPolicyId);
//			objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//			objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//			DSTempDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFPolicyDetails");
//			objDataWrapper.ClearParameteres();
               // DSTempDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFPolicyDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			if(DSTempPolicyDataSet.Tables[0].Rows.Count>0)
			{
				SetPDFVersionLobNode("AUTO",DateTime.Parse(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
				SetPDFInsScoresLobNode("AUTO",DateTime.Parse(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
			}
		}

		public string getAutoAcordPDFXml()
		{
				AcordPDFXML = new XmlDocument();
				if(gStrPdfFor == PDFForAcord)
					return getAcordPDFXml();
				if(outXml==null || outXml=="")
				{
					gCopyTo = gStrCopyTo;
					gStrCopyTo="CUSTOMER";
					outXml = getAcordPDFXml();
					gStrCopyTo=gCopyTo;
					return outXml; 
				}	
				strModXml = ModifiedXml(outXml);
				if(gStrCopyTo=="AGENCY" && strModXml!="")
				{
					InsertCustomerFullWordingXml(gStrClientID,gStrPolicyId,gStrPolicyVersion,gStrCopyTo,strModXml);
					return strModXml;
				}
				else if(gStrCopyTo=="CUSTOMER-NOWORD" && strModXml!="")
				{
					InsertCustomerFullWordingXml(gStrClientID,gStrPolicyId,gStrPolicyVersion,gStrCopyTo,strModXml);
					return strModXml;
				}
				else if(gStrCopyTo=="CUSTOMER")
				{
					InsertCustomerFullWordingXml(gStrClientID,gStrPolicyId,gStrPolicyVersion,gStrCopyTo,strModXml);
					return strModXml; 
				}
				else
				{
					InsertCustomerFullWordingXml(gStrClientID,gStrPolicyId,gStrPolicyVersion,gStrCopyTo,outXml);
					return outXml; 
				}
			}
		private string getAcordPDFXml()
		{
			AcordPDFXML.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <" + RootElement + "/>");
			if(gStrCopyTo != "AGENCY")
			{
				creatmaillingpage();
			}
			createRootElementForAllRootPDFs();
			FillMonth();
			if(gStrtemp!="final")
			{
				LoadRateXML("AUTOP",DSTempRateDataSet);
			}
			//Creating Xml For Policy
			CreatePolicyAgencyXml();
			CreateCoApplicantXml();
			CreatePriorPolicyCoverage();
			
			createAutoUnderwritingGeneralXML();
			createAUTOXML();
			createAcord90AutoAddlIntXml();
			createAcord90EmplXml();
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
			{
				createEndorsementWordings();
			}

			if(gStrCopyTo == "CUSTOMER" || gStrCopyTo == "CUSTOMER-NOWORD")
			{
				createAddWordingsXML();
			}
            
			if(gStrPdfFor == PDFForAcord)
				InsertCustomerFullWordingXml(gStrClientID,gStrPolicyId,gStrPolicyVersion,gStrCopyTo,AcordPDFXML.OuterXml);
			return AcordPDFXML.OuterXml;
		}
		#region Manipulated Xml
		private string ModifiedXml(string strPdfXml)
		{
			try
			{
				strPdfXml= strPdfXml.Replace("\"","'");
				strPdfXml= strPdfXml.Replace("<?xml version='1.0' encoding='utf-8'?>","");
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(strPdfXml);
				XmlNode nodParrent, nodchild,  nodEndsPrent;//nodEndos,
				nodParrent = doc.SelectSingleNode("ACORDXML");
				nodchild = doc.SelectSingleNode("ACORDXML/MAILLINGPAGE");
				string enodnod="";//, strEndnd="";
				if(gStrCopyTo=="AGENCY")
				{
					// Remove Mailing page from agent copy
					if(nodParrent!=null)
						nodchild = nodParrent.SelectSingleNode("MAILLINGPAGE");
					if(nodchild!=null)
						nodParrent.RemoveChild(nodchild);
					// Remove Endorsement Wording from agent copy
					nodParrent =  doc.SelectSingleNode("ACORDXML/ACORD");
					for(int intCntr=0; intCntr<=100; intCntr++)
					{
						enodnod = "AUTOENDORSEMENTPP" + "_" +intCntr;
						nodchild = nodParrent.SelectSingleNode(enodnod);
						if(nodchild!=null)
							nodParrent.RemoveChild(nodchild);
					}
					// Update Copy' to node 
					nodchild = nodParrent.SelectSingleNode("AUTO0/AUTOINFO0/copyTo");
					if(nodchild!=null)
						nodchild.InnerText="AGENT'S"+ " "+ "COPY";
					
					// Remove Policy Notice page from Agenct copy
					nodParrent = doc.SelectSingleNode("ACORDXML/ACORD/AUTO0/AUTOINFO0");
					if(nodParrent!=null)
						nodchild = nodParrent.SelectSingleNode("ADDPAGE2");
					if(nodchild!=null)
						nodParrent.RemoveChild(nodchild);

				}
				else if(gStrCopyTo=="CUSTOMER-NOWORD")
				{

					AcordPDFXML.LoadXml("<" + RootElement + "/>");
					// Remove Endorsement Wording from  No word copy
					nodParrent =  doc.SelectSingleNode("ACORDXML/ACORD");
					for(int intCntr=0; intCntr<=100; intCntr++)
					{
						enodnod = "AUTOENDORSEMENTPP" + "_" +intCntr;
						nodchild = nodParrent.SelectSingleNode(enodnod);
						if(nodchild!=null)
						{
							if(nodchild.Attributes["PRIMPDF"].Value.ToString().Trim().ToUpper()!="BLANK DOCUMENT.PDF")
								nodParrent.RemoveChild(nodchild);
						}
					}
					if (gStrCopyTo == "CUSTOMER-NOWORD"  &&					
						(gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS.ToString() ||gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS.ToString() 
						|| gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RENEWAL_PROCESS.ToString() || gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RENEWAL_PROCESS.ToString())
						)
					{
						PrintEndorsement();
						RemoveEnorsementWordings();
						createEndorsementWordings();
					}
					string strTmp = AcordPDFXML.OuterXml;
					strTmp = strTmp.Replace("\"","'");
					strTmp = strTmp.Replace("\r","");
					strTmp = strTmp.Replace("\n","");
					strTmp = strTmp.Replace("\t","");
					XmlDocument tmpDoc = new XmlDocument();
					tmpDoc.LoadXml(strTmp);
					nodEndsPrent = tmpDoc.SelectSingleNode("ACORDXML/ACORD");
					//nodParrent = nodParrent.SelectSingleNode("ACORD");
					if(nodEndsPrent!=null)
					nodParrent.InnerXml=nodParrent.InnerXml + nodEndsPrent.InnerXml; 
					
//					for(int intCntr=0; intCntr<=100; intCntr++)
//					{
//						enodnod = "ACORD/"+"AUTOENDORSEMENTPP" + "_" +intCntr;
//						nodchild = nodParrent.SelectSingleNode(enodnod);
//						for(int intEndCtr=0; intEndCtr<=100;intEndCtr++)
//						{
//							strEndnd = "ACORD/"+"AUTOENDORSEMENTPP" + "_" +intEndCtr;
//							nodEndos = nodEndsPrent.SelectSingleNode(strEndnd);
//							if(nodchild!=null && nodEndos!=null)
//							{
//								if(nodEndos!=nodchild)
//									nodParrent.RemoveChild(nodchild);
//							}
//						}						
//					}
					// remove Accounting information node
					nodParrent = doc.SelectSingleNode("ACORDXML/ACORD/AUTO0/AUTOINFO0/AUTO");
					foreach(XmlNode nod in nodParrent)
					{
						nodchild = nod.SelectSingleNode("AGENCYACCOUNTINFORMATION");
						if(nodchild!=null)
							nod.RemoveChild(nodchild);
					}
				}
				else
				{
					// remove Accounting information node
					nodParrent = doc.SelectSingleNode("ACORDXML/ACORD/AUTO0/AUTOINFO0/AUTO");
					foreach (XmlNode nod in nodParrent)
					{
						nodchild = nod.SelectSingleNode("AGENCYACCOUNTINFORMATION");
						if(nodchild!=null)
							nod.RemoveChild(nodchild);	
					}
				}
				return doc.OuterXml;
			}
			catch (Exception Ex)
			{
				flgError=1;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(Ex);
				return getAcordPDFXml();
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
//			DataSet DstempAppDocument = new DataSet();
//			objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//			objDataWrapper.AddParameter("@POLID",gStrPolicyId);
//			objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//			objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//			DstempAppDocument = objDataWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
//			objDataWrapper.ClearParameteres();
			//DstempAppDocument = objDataWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			strInsname=RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["APPNAME"].ToString())+ " " + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["SUFFIX"].ToString());
			strInsAdd=RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["APPADDRESS"].ToString());
			strcityzip=RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["APPCITYSTZIP"].ToString());
			//DataSet DstempDocument = new DataSet();
			
			if (gStrPdfFor == PDFForDecPage)
			{
				//objDataWrapper.AddParameter("@DOCUMENT_CODE","DEC_PAGE");
			//	DstempDocument = objDataWrapper.ExecuteDataSet("Proc_GetPDFDocumentMessage");
				//DstempDocument = objDataWrapper.ExecuteDataSet("Proc_GetPDFDocumentMessage " + "DEC_PAGE" + "");
				objDataWrapper.ClearParameteres();
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
				//objDataWrapper.AddParameter("@DOCUMENT_CODE","ACORD");
				//DstempDocument = objDataWrapper.ExecuteDataSet("Proc_GetPDFDocumentMessage");
				//DstempDocument = objDataWrapper.ExecuteDataSet("Proc_GetPDFDocumentMessage " + "ACORD" + "");
//				objDataWrapper.ClearParameteres();
//				strsendmessg=DstempDocument.Tables[0].Rows[0]["SEND_MESSAGE"].ToString();
//				
//				if(strsendmessg	=="Y")
//				{
//					AcordPDFXML.SelectSingleNode(RootElement).AppendChild(MaillingRootElementDecPage);
//					MaillingRootElementDecPage.SetAttribute(fieldType,fieldTypeMultiple);
//					MaillingRootElementDecPage.SetAttribute(PrimPDF,getAcordPDFNameFromXML("MAILLINGPAGE"));
//					MaillingRootElementDecPage.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("MAILLINGPAGE"));
//					MaillingRootElementDecPage.SetAttribute(SecondPDF,getAcordPDFNameFromXML("MAILLINGPAGEEXTN"));
//					MaillingRootElementDecPage.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("MAILLINGPAGEEXTN"));
//				
//					XmlElement DecMailElement;
//					DecMailElement =  AcordPDFXML.CreateElement("MAILADDRESS");
//					MaillingRootElementDecPage.AppendChild(DecMailElement);
//					DecMailElement.SetAttribute(fieldType,fieldTypeSingle);
//					DecMailElement.SetAttribute(id,"0");
//
//					DecMailElement.InnerXml= DecMailElement.InnerXml +  "<MAILLINGNAME " + fieldType +"=\""+ fieldTypeText +"\">"+strInsname+"</MAILLINGNAME>"; 
//					DecMailElement.InnerXml= DecMailElement.InnerXml +  "<MAILLINGADDRESS " + fieldType +"=\""+ fieldTypeText +"\">"+strInsAdd+"</MAILLINGADDRESS>"; 
//					DecMailElement.InnerXml= DecMailElement.InnerXml +  "<CITYSTATEZIP " + fieldType +"=\""+ fieldTypeText +"\">"+strcityzip+"</CITYSTATEZIP>"; 
//					DecMailElement.InnerXml= DecMailElement.InnerXml +  "<MESSAGE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DstempDocument.Tables[0].Rows[0]["DOCUMENT_MESSAGE"].ToString())+"</MESSAGE>"; 
//				}
			}
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
				Acord90RootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(Acord90RootElement);

				if(stCode.Equals("IN")) 
					Acord90RootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD90IN"));
				else if(stCode.Equals("MI")) 
					Acord90RootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD90MI"));

				SupplementalRootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(SupplementalRootElement);

				SupplementalRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENT"));
			}
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
		#region Code for Policy And Agency Info Xml Generation
		private void CreatePolicyAgencyXml()
		{
//			objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//			objDataWrapper.AddParameter("@POLID",gStrPolicyId);
//			objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//			objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//			DSTempDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFPolicyDetails");
//			objDataWrapper.ClearParameteres();
			//DSTempDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFPolicyDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			
			#region Global Variable Assignment
			goldVewrsionId=DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString();
			PolicyNumber = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString());
			PolicyEffDate = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString());
			PolicyExpDate = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString());
			strOldPolicyVer = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
			expiry_date = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString());
			eff_date = DSTempPolicyDataSet.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString();
			if(gStrProcessID != null && gStrProcessID != "" && gStrProcessID != "0")
			{
//				DataSet DSAddWordSet = new DataSet();
//				objDataWrapper.AddParameter("@STATE_ID","0");
//				objDataWrapper.AddParameter("@LOB_ID","0");
//				objDataWrapper.AddParameter("@PROCESS_ID",gStrProcessID);
//				DSAddWordSet = objDataWrapper.ExecuteDataSet("Proc_GetAddWordingsData");
//				objDataWrapper.ClearParameteres();
				//DSAddWordSet = objDataWrapper.ExecuteDataSet("Proc_GetAddWordingsData " +  "0," +  "0," + gStrProcessID);
			
				if (DSAddWordSet.Tables.Count > 1 && DSAddWordSet.Tables[1].Rows.Count > 0)
					Reason		=	RemoveJunkXmlCharacters(DSAddWordSet.Tables[1].Rows[0]["PROCESS_DESC"].ToString());
			}
			else
				Reason		=	RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["REASON"].ToString());
			//			CopyTo			= RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["COPY_TO"].ToString());
	
			if(Reason.Trim() != "" && DSTempPolicyDataSet.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString() != "")
				Reason += " / Effective Date: " + DSTempPolicyDataSet.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString();

			AgencyName = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString());
			AgencyAddress = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_ADD"].ToString());
			AgencyCitySTZip = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_CITYSTZIP"].ToString().Trim());
			AgencyPhoneNumber = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString());
			AgencyCode = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_CODE"].ToString());
			NumAgencyCode = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["NUM_AGENCY_CODE"].ToString());
			AgencySubCode = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["SUB_CODE"].ToString());
			AgencyBilling = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["PAYMENTBILLING"].ToString())=="AB"?"Agency Bill":"Direct Bill" ;
			strSubLine=RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["Subline"].ToString());
			if(strSubLine.Equals("0"))
				strSubLine="";
			strPolicyType=RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_TYPE"].ToString());
			strrecdPremium=RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["RECEIVED_PRMIUM"].ToString());
			currTerm = int.Parse(DSTempPolicyDataSet.Tables[0].Rows[0]["CURRENT_TERM"].ToString());
			if(currTerm > 1)
				applyInsScore = int.Parse(DSTempPolicyDataSet.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString());
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

			if (gStrPdfFor==PDFForAcord)
			{
				#region Acord90 Page
				XmlElement AcordPolicyElement;
				AcordPolicyElement = AcordPDFXML.CreateElement("POLICY");
				Acord90RootElement.AppendChild(AcordPolicyElement);
				AcordPolicyElement.SetAttribute(fieldType,fieldTypeSingle);
			
				AcordPolicyElement.InnerXml +="<DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(System.DateTime.Today.ToString("MM/dd/yyyy") ) + "</DATE>";
				AcordPolicyElement.InnerXml +="<APPLICANTPOLNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</APPLICANTPOLNUM>";
				AcordPolicyElement.InnerXml +="<APPLICANTEFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</APPLICANTEFFDATE>";
				AcordPolicyElement.InnerXml +="<APPLICANTEXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</APPLICANTEXPDATE>";
				AcordPolicyElement.InnerXml +="<BINDEREFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</BINDEREFFDATE>";
				AcordPolicyElement.InnerXml +="<BINDEREXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</BINDEREXPDATE>";
				AcordPolicyElement.InnerXml +="<BINDERCVGNOTBOUND " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["BINDERCVGNOTBOUND"].ToString()) + "</BINDERCVGNOTBOUND>";
				AcordPolicyElement.InnerXml +="<PAYMENTBILLING " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["PAYMENTBILLING"].ToString()) + "</PAYMENTBILLING>";
				AcordPolicyElement.InnerXml +="<PAYMENTDIRECTBILL " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["PAYMENTDIRECTBILL"].ToString()) + "</PAYMENTDIRECTBILL>";
				AcordPolicyElement.InnerXml +="<PAYMENTAPPBILL " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["PAYMENTAPPBILL"].ToString()) + "</PAYMENTAPPBILL>";
				AcordPolicyElement.InnerXml +="<PAYMENT_PLAN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["PLAN_DESCRIPTION"].ToString()) + "</PAYMENT_PLAN>";
				//Agency
				AcordPolicyElement.InnerXml +="<AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</AGENCYNAME>";
				if(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_ADD"].ToString()!="")
				{
					AcordPolicyElement.InnerXml +="<AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_ADD"].ToString()) + "</AGENCYADDRESS>";
				}
				else
				{
					AcordPolicyElement.InnerXml +="<AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_ADD"].ToString()) + "</AGENCYADDRESS>";
				}
				object o1=new object();
				object o2=new object();
				 
				AgencyCitySTZip = AgencyCitySTZip.Trim();
				if(AgencyCitySTZip.EndsWith(","))
					AgencyCitySTZip = AgencyCitySTZip.Substring(0, AgencyCitySTZip.Length-1);
				AcordPolicyElement.InnerXml +="<AGENCYCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AgencyCitySTZip) + "</AGENCYCITYSTATEZIP>";
				AcordPolicyElement.InnerXml +="<AGENCYPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString()) + "</AGENCYPHONE>";
				AcordPolicyElement.InnerXml +="<AGENCYFAX " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_FAX"].ToString()) + "</AGENCYFAX>";
				AcordPolicyElement.InnerXml +="<AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_CODE"].ToString()) + "</AGENCYCODE>";
				AcordPolicyElement.InnerXml +="<AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["SUB_CODE"].ToString()) + "</AGENCYSUBCODE>";

				//AcordPolicyElement.InnerXml += "<CURRENTADDRESS " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(AutoDetail["CURRENTADDRESS"].ToString())+"</CURRENTADDRESS>";
				if(DSTempPolicyDataSet.Tables[0].Rows[0]["YEAR_AT_CURR_RESI"].ToString() != "0")
					AcordPolicyElement.InnerXml += "<YRSATPREADDRESS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["YEAR_AT_CURR_RESI"].ToString())+"</YRSATPREADDRESS>";
				AcordPolicyElement.InnerXml += "<APPLICANTPREVIOUSADDRESS " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["YEARS_AT_PREV_ADD"].ToString())+"</APPLICANTPREVIOUSADDRESS>";
				AcordPolicyElement.InnerXml += "<RECEIVED_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["RECEIVED_PRMIUM"].ToString())+"</RECEIVED_PREMIUM>";
				try{ recv_prem = double.Parse(DSTempPolicyDataSet.Tables[0].Rows[0]["RECEIVED_PRMIUM"].ToString()); } 
                catch//(Exception ex) 
                {}

				string strfloatX = "380";
				string strfloatY = "36";
				string strfloatW = "153";
				string strfloatH = "22";
				string strpageNo = "2";

				AcordPolicyElement.InnerXml += "<SIGNATURE " + fieldType +"=\""+ fieldTypeImage +"\" " + imageType + "=\"" + imageTypeYes + "\" " + floatX + "=\"" + strfloatX + "\" " + floatY + "=\"" + strfloatY + "\" " + floatW + "=\"" + strfloatW  + "\" " + floatH + "=\"" + strfloatH  + "\" " + pageNo + "=\"" + strpageNo  + "\"></SIGNATURE>";

				#endregion

				#region Supplemental Page
				XmlElement SupplementPolicyElement;
				SupplementPolicyElement = AcordPDFXML.CreateElement("POLICY");
				SupplementalRootElement.AppendChild(SupplementPolicyElement);
				SupplementPolicyElement.SetAttribute(fieldType,fieldTypeSingle);

				SupplementPolicyElement.InnerXml += "<APPLICANTIONNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</APPLICANTIONNUMBER>";			
				#endregion

			}

		}
		#endregion

		#region Code for Co-Applicant or Named Insured Info Xml Generation.
		private void CreateCoApplicantXml()
		{
//			objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//			objDataWrapper.AddParameter("@POLID",gStrPolicyId);
//			objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//			objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//			DSTempDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
//			objDataWrapper.ClearParameteres();
			//DSTempDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			
			if (DSTempApplicantDataSet.Tables[0].Rows.Count > 0 )
			{
				#region Global Variable Assignment
				ApplicantName = RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["APPNAME"].ToString());
				NamedInsuredWithSuffix = RemoveJunkXmlCharacters(NamedInsuredWithSuffixs(DSTempApplicantDataSet));
				ApplicantAddress = RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["APPADDRESS"].ToString());
				ApplicantCityStZip = RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["APPCITYSTZIP"].ToString());

				reason_code1 = RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["CUSTOMER_REASON_CODE"].ToString());
				reason_code2 = RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["CUSTOMER_REASON_CODE2"].ToString());
				reason_code3 = RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["CUSTOMER_REASON_CODE3"].ToString());
				reason_code4 = RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["CUSTOMER_REASON_CODE4"].ToString());

				CustomerAddress = RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["CUSTADDRESS"].ToString());
				CustomerCityStZip = RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["CUSTCITYSTZIP"].ToString());
				strInsScore=RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString());
				strInsuScore = strInsScore;
				strInsType=RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["CUSTOMER_INSURANCE_SCORE_TYPE"].ToString());
				strUwTier=RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["UNDERWRITING_TIER"].ToString());
				if(currTerm <= 1)
				{
					if(IsInsScorePage2(currTerm, stCode, PolicyEffDate, DSTempApplicantDataSet.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString()))
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
					#region Acord 90 Page
					XmlElement Acord90NamedInsuredElement;
					Acord90NamedInsuredElement = AcordPDFXML.CreateElement("NAMEDINSURED");
					Acord90RootElement.AppendChild(Acord90NamedInsuredElement);
					Acord90NamedInsuredElement.SetAttribute(fieldType,fieldTypeSingle);

					Acord90NamedInsuredElement.InnerXml +="<APPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["APPNAME"].ToString()) + "</APPLICANTNAME>";
					Acord90NamedInsuredElement.InnerXml +="<APPLICANTADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["CUSTADDRESS"].ToString()) + "</APPLICANTADDRESS>";
					Acord90NamedInsuredElement.InnerXml +="<APPLICANTCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["CUSTCITYSTZIP"].ToString()) + "</APPLICANTCITYSTATEZIP>";
					Acord90NamedInsuredElement.InnerXml +="<APPLICANTHOMEPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["PHONE"].ToString()) + "</APPLICANTHOMEPHONE>";

					if(stCode.Equals("MI"))
					{
						if(strSubLine != "")
							Acord90NamedInsuredElement.InnerXml +="<APPLICANTCOPLAN " + fieldType + "=\"" + fieldTypeText + "\">" + (RemoveJunkXmlCharacters("Wolverine Mutual") + "/" +  strSubLine) + "</APPLICANTCOPLAN>";
						else
							Acord90NamedInsuredElement.InnerXml +="<APPLICANTCOPLAN " + fieldType + "=\"" + fieldTypeText + "\">" + (RemoveJunkXmlCharacters("Wolverine Mutual") + strSubLine) + "</APPLICANTCOPLAN>";
					}
					else if(stCode.Equals("IN"))
					{
						if(strPolicyType != "" && strSubLine != "")
							Acord90NamedInsuredElement.InnerXml +="<APPLICANTCOPLAN " + fieldType + "=\"" + fieldTypeText + "\">" + (RemoveJunkXmlCharacters("Wolverine Mutual") + "/" +  strPolicyType  + "/" + strSubLine) + "</APPLICANTCOPLAN>";
						else if(strPolicyType == "" && strSubLine == "")
							Acord90NamedInsuredElement.InnerXml +="<APPLICANTCOPLAN " + fieldType + "=\"" + fieldTypeText + "\">" + (RemoveJunkXmlCharacters("Wolverine Mutual") +  strPolicyType  + strSubLine) + "</APPLICANTCOPLAN>";
						else
							Acord90NamedInsuredElement.InnerXml +="<APPLICANTCOPLAN " + fieldType + "=\"" + fieldTypeText + "\">" + (RemoveJunkXmlCharacters("Wolverine Mutual") + "/" +  strPolicyType + strSubLine) + "</APPLICANTCOPLAN>";
					}

					XmlElement AddlCoApplicantElement;
					AddlCoApplicantElement = AcordPDFXML.CreateElement("ADDITIONALCOAPPLICANTS");
					Acord90RootElement.AppendChild(AddlCoApplicantElement);
					AddlCoApplicantElement.SetAttribute(fieldType,fieldTypeMultiple);
					AddlCoApplicantElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ADDLAPPLICANT"));
					AddlCoApplicantElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ADDLAPPLICANT"));
					AddlCoApplicantElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ADDLAPPLICANTEXTN"));
					AddlCoApplicantElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ADDLAPPLICANTEXTN"));

					for(int RowCounter=1;RowCounter<DSTempApplicantDataSet.Tables[0].Rows.Count;RowCounter++)
					{
						XmlElement CoAppElement;
						CoAppElement =  AcordPDFXML.CreateElement("COAPPLICANT");
						AddlCoApplicantElement.AppendChild(CoAppElement);
						CoAppElement.SetAttribute(fieldType,fieldTypeNormal);
						CoAppElement.SetAttribute(id,(RowCounter-1).ToString());
						string strCoAppSSn="";
						if(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["SSN"].ToString() !="" && DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["SSN"].ToString() !="0")
						{
							strCoAppSSn = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["SSN"].ToString());
							if(strCoAppSSn.Trim() != "")//If-Else condition added by Charles on 21/7/2009 for Itrack 6129
							{
								string strvaln = "xxx-xx-";
								strvaln += strCoAppSSn.Substring(strvaln.Length, strCoAppSSn.Length - strvaln.Length);
								strCoAppSSn = strvaln;
							}
							else
								strCoAppSSn="";
						}

						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNUMBER>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<APPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</APPLICANTNAME>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<NAMEINSUREDNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</NAMEINSUREDNAME>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<NAMEINSUREDADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantAddress + "</NAMEINSUREDADDRESS>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<NAMEINSUREDCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantCityStZip + "</NAMEINSUREDCITYSTATEZIP>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPOCCUPATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["OCCUPATION"].ToString()) + "</COAPPOCCUPATION>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPEMPLOYEENAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["APPNAME"].ToString()) + "</COAPPEMPLOYEENAME>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPYEAREMPL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["YEARSEMPL"].ToString()) + "</COAPPYEAREMPL>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPMARTSTATUS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["MARTSTATUS"].ToString()) + "</COAPPMARTSTATUS>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPDATEOFBIRTH " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["DOB"].ToString()) + "</COAPPDATEOFBIRTH>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPSSN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strCoAppSSn) + "</COAPPSSN>";
					}
					#endregion

					#region Supplemental Page
					XmlElement SupplementalNamedInsuredElement;
					SupplementalNamedInsuredElement = AcordPDFXML.CreateElement("NAMEDINSURED");
					SupplementalRootElement.AppendChild(SupplementalNamedInsuredElement);
					SupplementalNamedInsuredElement.SetAttribute(fieldType,fieldTypeSingle);

					SupplementalNamedInsuredElement.InnerXml += "<APPLICATION_NUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</APPLICATION_NUMBER>";
					SupplementalNamedInsuredElement.InnerXml += "<APPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["APPNAME"].ToString()) + "</APPLICANTNAME>";
					SupplementalNamedInsuredElement.InnerXml += "<APPLICANT_EMAIL_ADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["EMAIL"].ToString()) + "</APPLICANT_EMAIL_ADDRESS>";
					if(DSTempApplicantDataSet.Tables[0].Rows.Count > 1)
						SupplementalNamedInsuredElement.InnerXml += "<COAPPLICANT_EMAIL_ADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[1]["EMAIL"].ToString()) + "</COAPPLICANT_EMAIL_ADDRESS>";
					#endregion
					#region Garage address
//					objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//					objDataWrapper.AddParameter("@POLID",gStrPolicyId);
//					objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//					objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//					DSTempDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_Details");
//					objDataWrapper.ClearParameteres();
					//DSTempDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			

					if(DSTempAutoDetailDataSet.Tables[0].Rows.Count>0)
					{

						Acord90NamedInsuredElement.InnerXml += "<TOTALVEHNO " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DSTempAutoDetailDataSet.Tables[0].Rows.Count.ToString())+"</TOTALVEHNO>";
						foreach(DataRow AutoDetail in DSTempAutoDetailDataSet.Tables[0].Rows)
						{
							if((CustomerAddress.Trim() != RemoveJunkXmlCharacters(AutoDetail["GRG_ADD"].ToString().Trim())) || (CustomerCityStZip.Trim() != RemoveJunkXmlCharacters(AutoDetail["GRG_CITYSTZIP"].ToString().Trim())))
							{
								Acord90NamedInsuredElement.InnerXml += "<VEHICLENO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["VEHICLE_ID"].ToString())+"</VEHICLENO>";
								Acord90NamedInsuredElement.InnerXml += "<GARAGELOCATION " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(AutoDetail["GRG_ADD"].ToString() + " " + AutoDetail["GRG_CITYSTZIP"].ToString())+"</GARAGELOCATION>";
								break;
							}
						}
					}
					#endregion

				}
			}
		}
		#endregion
		// policy adjusted premium 
		private double GetEffectivePremium( double OldGrossPremium,double NewGrossPremium)
		{
			double EffectivePremium = 0;
			int TotalPolicyTime = 365;
			int DaysDiff=0;
			
			objDataWrapper.AddParameter("@CUSTOMER_ID",gStrClientID);
			objDataWrapper.AddParameter("@APP_ID",0);
			objDataWrapper.AddParameter("@APP_VERSION_ID",0);
			objDataWrapper.AddParameter("@POLICY_ID",gStrPolicyId);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",gStrPolicyVersion);
			DataSet dsPolicy = objDataWrapper.ExecuteDataSet("Proc_GetPolicyDetails");
			objDataWrapper.ClearParameteres();
			//DataSet dsPolicy = objDataWrapper.ExecuteDataSet("Proc_GetPolicyDetails " + gStrClientID + ",0,0," + gStrPolicyId + "," + gStrPolicyVersion);

			if (dsPolicy.Tables[0].Rows.Count>0)
			{
				TotalPolicyTime= Convert.ToInt32(dsPolicy.Tables[0].Rows[0]["POLICY_DAYS"].ToString());
			}
			Cms.BusinessLayer.BlProcess.ClsEndorsmentProcess objProcess = new
				Cms.BusinessLayer.BlProcess.ClsEndorsmentProcess ();

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
						//DSTempRateDataSet = new DataWrapper(ConnStr,CommandType.Text).ExecuteDataSet("Proc_GetPDFQuoteRateXML " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "','" + "AUTOP" + "'");
						objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						objDataWrapper.AddParameter("@POLID",gStrPolicyId);
						objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						objDataWrapper.AddParameter("@LOB","AUTOP");
						DSTempRateDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFQuoteRateXML");
						objDataWrapper.ClearParameteres();
						//DSTempRateDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFQuoteRateXML " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "','" + "AUTOP" + "'");
						gStrPolicyVersion = oldPolicyVersion;
						// Load quote premium xml
						if(DSTempRateDataSet.Tables[0].Rows.Count>0)
						{
							LoadRateXML("AUTOP");
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
						objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						objDataWrapper.AddParameter("@POLID",gStrPolicyId);
						objDataWrapper.AddParameter("@VERSIONID",newVersion);
						DSTempDatasetBoatDetail = objDataWrapper.ExecuteDataSet("PROC_GetpdfAdjusted_Premium");
						objDataWrapper.ClearParameteres();
						//DSTempDatasetBoatDetail = objDataWrapper.ExecuteDataSet("PROC_GetpdfAdjusted_Premium " + gStrClientID + "," + gStrPolicyId + "," + newVersion +"");			
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
					//LoadRateXML("AUTOP");
				}
			}
			
			catch(Exception ex)
			{
				throw(ex);
			}
			return dblOldSum;
		}
		#region insret customer full wording xml
		private void InsertCustomerFullWordingXml(string strCustomerId,string strAppId, string strAppVersionId, string strCopyTo, string strcutomerxml)
		{
			try
			{
				//DataWrapper gobjDataWrapper;
				objDataWrapper.AddParameter("@CUSTOMER_ID",strCustomerId);
				objDataWrapper.AddParameter("@POLICY_ID",strAppId);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",strAppVersionId);
				objDataWrapper.AddParameter("@CALLED_FOR",strCopyTo);
				objDataWrapper.AddParameter("@CUSTOMER_XML",strcutomerxml);
				objDataWrapper.ExecuteNonQuery("PROC_INSERTXMLFORPDF");				
			}			
			catch//(Exception ex)
			{
				//throw new Exception(ex.Message);
			}
			finally
			{}
		}
		#endregion
		#region code for AUTO info Xml
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
		private void createAUTOXML()
		{
            DataSet DSTempVehicle, DSTempViolation;//,DSTempOperator,DSTempEquip
			int AutoCtr = 0, Operator_Ctr = 0,intOrdered=0,intWorkLoss=0, violations_ctr=0;
			int policyCovFlag=0;
			double dbEstTotal=0;
			double balance_due = 0;
			string strDriverName="";
			//int operCounttemp = 0;
			string strMccaFee="0";
			int coll_applicable = 614;
			int intpageno=1;
			string strregCollDed="";
			int cntEno=0;
			DataSet dstemcovcnt;
			//COUNTER TO SHOW EXTENDED NONOWNED COVERAGE
			int intEno=0;
			XmlElement AutoRootElementDecPage;
			AutoRootElementDecPage    = AcordPDFXML.CreateElement("AUTO");

			XmlElement AutoRootElementAcord90;
			AutoRootElementAcord90 = AcordPDFXML.CreateElement("AUTO");
			
			XmlElement AutoRootElementSupplement;
			AutoRootElementSupplement = AcordPDFXML.CreateElement("AUTO");

			XmlElement AutoRootElementDecPage0;
			AutoRootElementDecPage0    = AcordPDFXML.CreateElement("AUTO0");

			XmlElement AutoElementDecPage0;
			AutoElementDecPage0	= AcordPDFXML.CreateElement("AUTOINFO0");
			// if Process type is blank
			if(gStrProcessID=="")
				{
					gStrProcessID = GetPolicyProcess( gStrClientID ,gStrPolicyId ,gStrPolicyVersion , gStrCalledFrom,objDataWrapper);
				}
			#region operator Accord
			if (gStrPdfFor == PDFForAcord)
			{	
				#region setting Root Operate Attribute
				XmlElement OperaterRootElement;
				OperaterRootElement = AcordPDFXML.CreateElement("OPERATOR");
				Acord90RootElement.AppendChild(OperaterRootElement);
				OperaterRootElement.SetAttribute(fieldType,fieldTypeMultiple);
				OperaterRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD90OPERATOR"));
				OperaterRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD90OPERATOR"));
				OperaterRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD90OPERATOREXTN"));
				OperaterRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD90OPERATOREXTN"));
				#endregion
//				objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//				objDataWrapper.AddParameter("@POLID",gStrPolicyId);
//				objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//				objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//				objDataWrapper.AddParameter("@VEHICLEID","0");
//				DSTempOperator = objDataWrapper.ExecuteDataSet("Proc_GetPDFAutoOperatorDtls");
//				objDataWrapper.ClearParameteres();
				//DSTempOperator = objDataWrapper.ExecuteDataSet("Proc_GetPDFAutoOperatorDtls " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "',0");
						
				foreach(DataRow OperatorDetail in DSTempOperators.Tables[0].Rows)
				{
					XmlElement OperatorElement;
					OperatorElement = AcordPDFXML.CreateElement("OPERATORINFO");
					OperaterRootElement.AppendChild(OperatorElement);
					OperatorElement.SetAttribute(fieldType,fieldTypeNormal);
					OperatorElement.SetAttribute(id,Operator_Ctr.ToString());
					string strOprSSn="";
					strOprSSn =  OperatorDetail["DRIVER_SSN"].ToString();
					if(strOprSSn !="" && strOprSSn !="0")
					{
						strOprSSn = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(OperatorDetail["DRIVER_SSN"].ToString());
						
						if(strOprSSn.Trim() != "")//If-Else condition added by Charles on 21/7/2009 for Itrack 6129
						{
								string strvaln = "xxx-xx-";
							strvaln += strOprSSn.Substring(strvaln.Length, strOprSSn.Length - strvaln.Length);
							strOprSSn = strvaln;
						}
						else
							strOprSSn="";
					}
					//OperatorElement.InnerXml +="<OPERATORNUMBER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OperatorDetail["OPERATORNO"].ToString())+"</OPERATORNUMBER>"; 
					OperatorElement.InnerXml +="<OPERATORNUMBER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters((Operator_Ctr+1).ToString())+"</OPERATORNUMBER>"; 
					OperatorElement.InnerXml +="<OPERATORNAME " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OperatorDetail["DRIVER_NAME"].ToString())+"</OPERATORNAME>"; 
					OperatorElement.InnerXml +="<OPERATORSEX " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OperatorDetail["DRIVER_SEX"].ToString())+"</OPERATORSEX>"; 
					OperatorElement.InnerXml +="<OPERATOR_MARITALSTATUS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OperatorDetail["DRIVER_MART_STAT"].ToString())+"</OPERATOR_MARITALSTATUS>"; 
					OperatorElement.InnerXml +="<OPERATOR_RELATION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OperatorDetail["RELATION"].ToString())+"</OPERATOR_RELATION>"; 
					OperatorElement.InnerXml +="<OPERATORDOB " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OperatorDetail["DRIVER_DOB"].ToString())+"</OPERATORDOB>"; 
					OperatorElement.InnerXml +="<OPERATOR_OCCUPATION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OperatorDetail["OCCUPATION"].ToString())+"</OPERATOR_OCCUPATION>"; 
					OperatorElement.InnerXml +="<OPERATOR_DATELICENSE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OperatorDetail["DATE_LICENSED"].ToString())+"</OPERATOR_DATELICENSE>"; 
					OperatorElement.InnerXml +="<OPERATOR_GOODSTUDENT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OperatorDetail["DRIVER_GOOD_STUDENT"].ToString())+"</OPERATOR_GOODSTUDENT>";
					if(OperatorDetail["DRIVER_DRIV_LIC"].ToString() != "" && OperatorDetail["STATE_NAME"].ToString() != "")
						OperatorElement.InnerXml +="<OPERATORAUTODRVLIC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OperatorDetail["DRIVER_DRIV_LIC"].ToString()) + "/" +RemoveJunkXmlCharacters(OperatorDetail["STATE_NAME"].ToString()) +"</OPERATORAUTODRVLIC>"; 
					else
						OperatorElement.InnerXml +="<OPERATORAUTODRVLIC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OperatorDetail["DRIVER_DRIV_LIC"].ToString()) + RemoveJunkXmlCharacters(OperatorDetail["STATE_NAME"].ToString()) +"</OPERATORAUTODRVLIC>"; 
					OperatorElement.InnerXml +="<OPERATORSSN " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strOprSSn)+"</OPERATORSSN>"; 
					if(OperatorDetail["ORDERED"]!=null && OperatorDetail["ORDERED"].ToString()!="" && OperatorDetail["ORDERED"].ToString()!="0")
						intOrdered=int.Parse(OperatorDetail["ORDERED"].ToString());
					if(OperatorDetail["WAIVER_WORK_LOSS_BENEFITS"]!=null && OperatorDetail["WAIVER_WORK_LOSS_BENEFITS"].ToString()!="")
					{
						intWorkLoss=1;//int.Parse(OperatorDetail["WAIVER_WORK_LOSS_BENEFITS"].ToString());
						strDriverName=RemoveJunkXmlCharacters(OperatorDetail["DRIVER_NAME"].ToString()) + System.Environment.NewLine;

					}
					if(OperatorDetail["DRIVER_VOLUNTEER_POLICE_FIRE"]!=null && OperatorDetail["DRIVER_VOLUNTEER_POLICE_FIRE"].ToString()!="" )
						if(OperatorDetail["DRIVER_VOLUNTEER_POLICE_FIRE"].ToString()!="0" )
							strVolunteer=OperatorDetail["DRIVER_VOLUNTEER_POLICE_FIRE"].ToString();

					if(OperatorDetail["DRIVER_US_CITIZEN"]!=null && OperatorDetail["DRIVER_US_CITIZEN"].ToString()!="" )
						if(OperatorDetail["DRIVER_US_CITIZEN"].ToString()!="0" )
							strUSCitizen=OperatorDetail["DRIVER_US_CITIZEN"].ToString();

					Operator_Ctr++;
				}
						
			}
			#endregion

			#region setting Auto Root Elements
			if (gStrPdfFor == PDFForDecPage)
			{
				prnOrd_covCode = new string[50];
				prnOrd_attFile = new string[50];
				prnOrd = new int[50];

				#region Auto Root Element for DecPage
				DecPageRootElement.AppendChild(AutoRootElementDecPage0);
				AutoRootElementDecPage0.SetAttribute(fieldType,fieldTypeMultiple);
				AutoRootElementDecPage0.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAUTO"));
				AutoRootElementDecPage0.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAUTO"));
				AutoRootElementDecPage0.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEAUTOEXTN"));
				AutoRootElementDecPage0.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAUTOEXTN"));
				#endregion

				// Added by Mohit Agarwal for Customer Details irrespective of added Autos
				// 14-Mar-2007
				#region setting Customer Agency Details
				AutoRootElementDecPage0.AppendChild(AutoElementDecPage0);
				AutoElementDecPage0.SetAttribute(fieldType,fieldTypeNormal);
				AutoElementDecPage0.SetAttribute(id,AutoCtr.ToString());
				//Policy
				if(gStrCalledFrom.Equals(CalledFromPolicy))
				{					
					AutoElementDecPage0.InnerXml +="<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNUMBER>";
					AutoElementDecPage0.InnerXml +="<POLICYVERSION " + fieldType + "=\"" + fieldTypeText + "\">" + gStrPolicyVersion + "</POLICYVERSION>";
					AutoElementDecPage0.InnerXml +="<POLICYEFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</POLICYEFFDATE>";
					AutoElementDecPage0.InnerXml +="<POLICYEXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</POLICYEXPDATE>";
					AutoElementDecPage0.InnerXml +="<REASON " + fieldType + "=\"" + fieldTypeText + "\">" + Reason + "</REASON>";
					AutoElementDecPage0.InnerXml += "<copyTo " + fieldType + "=\"" + fieldTypeText + "\">" + CopyTo + "</copyTo>";
				}
						
				//Agency
				AutoElementDecPage0.InnerXml +="<AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyName + "</AGENCYNAME>";
				if(AgencyAddress!="")
				{
					AutoElementDecPage0.InnerXml +="<AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyAddress +"</AGENCYADDRESS>";
						
				}
				if(AgencyCitySTZip!="")
				{
					AutoElementDecPage0.InnerXml += "<AGENCYCITYSTZIP " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCitySTZip + "</AGENCYCITYSTZIP>";
						
				}
				if(AgencyPhoneNumber!="")
				{
					AutoElementDecPage0.InnerXml +="<AGENCYPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyPhoneNumber + "</AGENCYPHONE>";
				}
				if(AgencyCode!="")
				{
					AutoElementDecPage0.InnerXml +="<AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + NumAgencyCode + "</AGENCYCODE>";
				}
				AutoElementDecPage0.InnerXml +="<AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencySubCode + "</AGENCYSUBCODE>";
				AutoElementDecPage0.InnerXml +="<AGENCYBILLING " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyBilling + "</AGENCYBILLING>";
				//Named Insured
				AutoElementDecPage0.InnerXml +="<INSUREDNAME " + fieldType + "=\"" + fieldTypeText + "\">" + NamedInsuredWithSuffix + "</INSUREDNAME>";
				AutoElementDecPage0.InnerXml +="<INSUREDADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + CustomerAddress + "</INSUREDADDRESS>";
				AutoElementDecPage0.InnerXml +="<INSUREDCITYSTZIP " + fieldType + "=\"" + fieldTypeText + "\">" + CustomerCityStZip + "</INSUREDCITYSTZIP>";

//				if(gStrCopyTo != "AGENCY" && strNeedPage2 == "Y")
//					createPage2XML(ref AutoElementDecPage0);
				////////////////////////////////////////////////////////////
				// Policy Notice and Adverse Section page (start) 
				///////////////////////////////////////////////////////////
				// adverse letter
				string strAdverseRep="";
//				DataSet dsTempPolicy = new DataSet();
//				objDataWrapper.ClearParameteres();
//				objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//				objDataWrapper.AddParameter("@POLID",gStrPolicyId);
//				objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//				objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//				dsTempPolicy = objDataWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
//				objDataWrapper.ClearParameteres();
				//dsTempPolicy = objDataWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
				strAdverseRep = DSTempApplicantDataSet.Tables[1].Rows[0]["ADVERSE_LETTER_REQD"].ToString();
				// if process is blank
				if(gStrProcessID=="")
				{
					gStrProcessID = GetPolicyProcess( gStrClientID ,gStrPolicyId ,gStrPolicyVersion , gStrCalledFrom,objDataWrapper);
				}
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
							createPage2XML(ref AutoElementDecPage0);
					}
					else if(gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS.ToString() || gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS.ToString() )
					{
						//store preassigned value of strNeedPage2
						string elginsusc=strNeedPage2;
						if(gStrCopyTo != "AGENCY" && strAdverseRep=="Y" && strNeedPage2 == "Y" && strInsScore !="-2")
						{
							ChkPreInsuScr(objDataWrapper);
							if(newInsuScr == oldInsuScr)
							{

							}
							else
							{
								if(oldInsuScr !="-2")
								{
									// refernce itrack 3222 if fallen in lower tier then print
									if(strNeedPage2 =="Y")
										createPage2AdverseXML(ref AutoElementDecPage0);
								}
								else
								{
									createPage2AdverseXML(ref AutoElementDecPage0);
								}
							}
							
						}
						else if(gStrCopyTo != "AGENCY"  && strAdverseRep=="Y" && strNeedPage2 == "Y" && strInsScore =="-2")
						{
							//call ChkPreInsuScr function to assign values to newInsuScr,oldInsuScr
							ChkPreInsuScr(objDataWrapper);
							if(newInsuScr == oldInsuScr)
							{

							}
							else
							{
								if(elginsusc=="Y")
									createPage2NHNSAdverseXML(ref AutoElementDecPage0);
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

							ChkPreInsuScr(objDataWrapper);
							if(newInsuScr == oldInsuScr)
							{

							}
							else
							{
								if(oldInsuScr !="-2")
								{
									// refernce itrack 3222 if fallen in lower tier then print
									if(strNeedPage2 =="Y")
										createPage2AdverseXML(ref AutoElementDecPage0);
								}
								else
								{
									createPage2AdverseXML(ref AutoElementDecPage0);
								}
							}
						}
							//if insuracne score is no hit no score
						else if(gStrCopyTo != "AGENCY" && strNeedPage2 == "Y" && strInsScore =="-2")
						{
							//call ChkPreInsuScr function to assign values to newInsuScr,oldInsuScr
							ChkPreInsuScr(objDataWrapper);
							if(newInsuScr == oldInsuScr)
							{

							}
							else
							{
								if(elginsusc=="Y")
									createPage2NHNSAdverseXML(ref AutoElementDecPage0);
							}
						}
					}
					else if(gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_NEW_BUSINESS_PROCESS.ToString() || gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_NEW_BUSINESS_PROCESS.ToString())
					{
						if(gStrCopyTo != "AGENCY" && strNeedPage2 == "Y")
						{
							createPage2XML(ref AutoElementDecPage0);
						}
						else if( strNeedPage2 == "N")
						{
							createPage2PrivacyPageXML(ref AutoElementDecPage0);
						}
					}
					else if(gStrProcessID == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REWRITE_PROCESS.ToString() || gStrProcessID == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REWRITE_PROCESS.ToString())
					{
						if( strNeedPage2 == "N")
						{
							createPage2PrivacyPageXML(ref AutoElementDecPage0);
						}
						else
						{
							createPage2XML(ref AutoElementDecPage0);
						}
					}
					else
					{
						if(gStrCopyTo != "AGENCY" && strNeedPage2 == "Y")
						{
							createPage2AdverseXML(ref AutoElementDecPage0);
						}
					}
				}
				////////////////////////////////////////////////////////////////////
				///// Policy Notice and Adverse Section page (End) 
				///////////////////////////////////////////////////////////
			
				#endregion

				AutoElementDecPage0.AppendChild(AutoRootElementDecPage);
				AutoRootElementDecPage.SetAttribute(fieldType,fieldTypeMultiple);
				AutoRootElementDecPage.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAUTO"));
				AutoRootElementDecPage.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAUTO"));
				AutoRootElementDecPage.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEAUTOEXTN"));
				AutoRootElementDecPage.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAUTOEXTN"));

			}
			else if (gStrPdfFor == PDFForAcord)
			{
				#region Auto Root Element for Acord90
				Acord90RootElement.AppendChild(AutoRootElementAcord90);
				AutoRootElementAcord90.SetAttribute(fieldType,fieldTypeMultiple);
				if(stCode.Equals("IN")) 
				{
					AutoRootElementAcord90.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD90AUTOIN"));
					AutoRootElementAcord90.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD90AUTOIN"));
					AutoRootElementAcord90.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD90AUTOEXTNIN"));
					AutoRootElementAcord90.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD90AUTOEXTNIN"));
				}
				else if(stCode.Equals("MI"))
				{
					AutoRootElementAcord90.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD90AUTOMI"));
					AutoRootElementAcord90.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD90AUTOMI"));
					AutoRootElementAcord90.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD90AUTOEXTNMI"));
					AutoRootElementAcord90.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD90AUTOEXTNMI"));
				}
				#endregion
			
				#region Auto Root Element for Supplement
				SupplementalRootElement.AppendChild(AutoRootElementSupplement);

				AutoRootElementSupplement.SetAttribute(fieldType,fieldTypeMultiple);
				AutoRootElementSupplement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTAUTO"));
				AutoRootElementSupplement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTAUTO"));
				AutoRootElementSupplement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTAUTOEXTN"));
				AutoRootElementSupplement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTAUTOEXTN"));
				#endregion
			}
			#endregion

//			objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//			objDataWrapper.AddParameter("@POLID",gStrPolicyId);
//			objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//			objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//			DSTempDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_Details");
//			objDataWrapper.ClearParameteres();
			//DSTempDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
			XmlNodeList ins_scoreNodeList = GetCreditForMotInsScore();
			XmlNode ins_scoreNode;
			if(ins_scoreNodeList.Count > 0)
			{
				ins_scoreNode = ins_scoreNodeList.Item(0);
				String [] discRows = getAttributeValue(ins_scoreNode,"STEPDESC").Split('-');

				if(discRows.Length >= 1)
					inspercent = discRows[discRows.Length -1];
			}
			
			
			if(DSTempAutoDetailDataSet.Tables[0].Rows.Count>0)
			{

				// total Policy pages
				inttotalpage +=  DSTempAutoDetailDataSet.Tables[0].Rows.Count;

				foreach(DataRow AutoDetail in DSTempAutoDetailDataSet.Tables[0].Rows)
				{
					objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					objDataWrapper.AddParameter("@POLID",gStrPolicyId);
					objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					objDataWrapper.AddParameter("@VEHICLEID",AutoDetail["VEHICLE_ID"]);
					objDataWrapper.AddParameter("@RISKTYPE",AutoDetail["RISKTYPE"]);
					objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					dstemcovcnt = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_RiskWise_Coverage_Details");
					objDataWrapper.ClearParameteres();
					//dstemcovcnt = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_Coverage_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +AutoDetail["VEHICLE_ID"] +  ",'"+ AutoDetail["RISKTYPE"] +"','" + gStrCalledFrom + "'");
					//policy and endorsement coverage more than 14
					// get total coverages policy level and vehicle level individually  if any of them more than 7 then increase page value 
					int intplC=0, intrlC=0;
					foreach( DataRow AutoCoverageDetail in 	dstemcovcnt.Tables[0].Rows)
					{
						
						if(AutoCoverageDetail["COVERAGE_TYPE"].ToString() == "PL")
						{
							intplC++;
						}
						else if(AutoCoverageDetail["COVERAGE_TYPE"].ToString() == "RL")
						{
							intrlC++;
						}
					}

					objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					objDataWrapper.AddParameter("@POLID",gStrPolicyId);
					objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					objDataWrapper.AddParameter("@VEHICLEID",AutoDetail["VEHICLE_ID"]);
					objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					dstemcovcnt = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_Excluded_Driver_and_Message");
					objDataWrapper.ClearParameteres();
					// check number of extended nonowned driver and excluded driver and increase intplC counter by that
					string excdriver95 = "";
					int intexcD=0,intEnoD=0;
					if(dstemcovcnt.Tables[0].Rows !=null)
					{
						foreach(DataRow DriverDetails in dstemcovcnt.Tables[0].Rows)
						{
							if(DriverDetails["SIGNATURE_OBT"].ToString() == "10963" && DriverDetails["EXCEXTEND_DRIVER"].ToString() == "3477")
							{
								excdriver95="TRUE";
							}
						}
					}
					if(excdriver95=="TRUE")
					{
						foreach(DataRow DriverDetails in dstemcovcnt.Tables[0].Rows)
						{
							if(DriverDetails["EXCEXTEND_DRIVER"].ToString() == "3477")
							{
								intexcD++;
							}
						}
						if(dstemcovcnt.Tables[1].Rows !=null)
						{
							foreach(DataRow DriverDetails in dstemcovcnt.Tables[1].Rows)
							{
								intexcD++;
							}
						}
					}
					if(cntEno==0)
					{
						if(dstemcovcnt.Tables[0].Rows !=null)
						{
							foreach(DataRow DriverDetails in dstemcovcnt.Tables[0].Rows)
							{
								if(DriverDetails["EXCEXTEND_DRIVER"].ToString() == "10963")
								{
									intEnoD++;
									cntEno++;
								}
							}
						}
					}
					intplC = intplC+intexcD+intEnoD;
                    if(intplC > 8 || intrlC> 8)
					{
						inttotalpage++;
					}

					// if additinal interest more than 2
//					objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//					objDataWrapper.AddParameter("@POLID",gStrPolicyId);
//					objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//					objDataWrapper.AddParameter("@VEHICLEID",AutoDetail["Vehicle_ID"]);
//					objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//					dstemcovcnt = objDataWrapper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS");
//					objDataWrapper.ClearParameteres();
					//dstemcovcnt = objDataWrapper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + "," + AutoDetail["Vehicle_ID"] + ",'" + gStrCalledFrom + "'");
					int intInrstCntr=0;
					foreach(DataRow row in DSTempAddIntrst.Tables[0].Rows)
					{
						if(row["VEHICLE_ID"]==AutoDetail["Vehicle_ID"])
							intInrstCntr++;
					}
					if(intInrstCntr > 2)
					{
						inttotalpage++;
					}
					//if Schedule Mislanous Equipment more than 2
//					objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//					objDataWrapper.AddParameter("@POLID",gStrPolicyId);
//					objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//					objDataWrapper.AddParameter("@VEHICLEID",AutoDetail["Vehicle_ID"]);
//					objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//					dstemcovcnt	= objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_MiscEquipment");
//					objDataWrapper.ClearParameteres();
					//dstemcovcnt	= objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_MiscEquipment " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",'" + gStrCalledFrom + "',"  + AutoDetail["Vehicle_ID"]);
					int intEquip=0;
					foreach(DataRow row in DSTempEquipment.Tables[0].Rows)
					{
						if(row["VEHICLE_ID"].ToString()==AutoDetail["Vehicle_ID"].ToString())
							intEquip++;
					}
					if(intEquip > 2)
					{
						inttotalpage++;
						
					}
					//if assingned operator more than 10
					objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					objDataWrapper.AddParameter("@POLID",gStrPolicyId);
					objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					objDataWrapper.AddParameter("@VEHICLEID",AutoDetail["Vehicle_ID"]);
					objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					dstemcovcnt = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_AssgnDriv");
					objDataWrapper.ClearParameteres();
					//dstemcovcnt = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_AssgnDriv " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",'" + gStrCalledFrom + "',"  + AutoDetail["Vehicle_ID"]);
					if(dstemcovcnt.Tables[1].Rows.Count > 4)
					{
						inttotalpage++;
					}
				}

				
				foreach(DataRow AutoDetail in DSTempAutoDetailDataSet.Tables[0].Rows)
				{
					objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					objDataWrapper.AddParameter("@POLID",gStrPolicyId);
					objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					objDataWrapper.AddParameter("@VEHICLEID",AutoDetail["VEHICLE_ID"]);
					objDataWrapper.AddParameter("@RISKTYPE",AutoDetail["RISKTYPE"]);
					objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DataSet DSTempVehicle1 = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_RiskWise_SumTotal");
					objDataWrapper.ClearParameteres();
					//DataSet DSTempVehicle1 = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_Coverage_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +AutoDetail["VEHICLE_ID"] +  ",'"+ AutoDetail["RISKTYPE"] +"','" + gStrCalledFrom + "'");
					double sumTtl=0;			

					if(gStrtemp != "final")
					{
						foreach (XmlNode SumTotalNode in GetSumTotalPremium(AutoDetail["VEHICLE_ID"].ToString()))
						{
							if(getAttributeValue(SumTotalNode,"STEPPREMIUM")!=null && getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()!="" )
								sumTtl += double.Parse(getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()) ;						
						}
					
					}
					else 
					{
						string sumtotal = GetSumTotalPremium(DSTempVehicle1, "SUMTOTAL", AutoDetail["VEHICLE_ID"].ToString());
						if (sumtotal != "")
							sumTtl = double.Parse(sumtotal);
					}

					//					if(AutoDetail["AMOUNT"] != System.DBNull.Value)
					//						balance_due += Convert.ToInt32(AutoDetail["AMOUNT"]);
					dbEstTotal +=sumTtl ;
				}

				foreach(DataRow AutoDetail in DSTempAutoDetailDataSet.Tables[0].Rows)
				{
					XmlElement AutoElementDecPage;
					AutoElementDecPage	= AcordPDFXML.CreateElement("AUTOINFO");
				
					XmlElement AutoElementAcord90;
					AutoElementAcord90	= AcordPDFXML.CreateElement("AUTOINFO");

					XmlElement AutoElementSuppliment;
					AutoElementSuppliment	= AcordPDFXML.CreateElement("AUTOINFO");

					objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					objDataWrapper.AddParameter("@POLID",gStrPolicyId);
					objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					objDataWrapper.AddParameter("@VEHICLEID",AutoDetail["VEHICLE_ID"]);
					objDataWrapper.AddParameter("@RISKTYPE",AutoDetail["RISKTYPE"]);
					objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempVehicle = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_Coverage_Details");
					objDataWrapper.ClearParameteres();
					//DSTempVehicle = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_Coverage_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +AutoDetail["VEHICLE_ID"] +  ",'"+ AutoDetail["RISKTYPE"] +"','" + gStrCalledFrom + "'");

					double sumTtl=0;
					htpremium.Clear(); 
					///////////////////////////////////////
					//    Sum Total(start)
					//////////////////////////////////////
					foreach (XmlNode PremiumNode in GetAutoPremium(AutoDetail["VEHICLE_ID"].ToString()))
					{
						if(!htpremium.Contains(getAttributeValue(PremiumNode,"COMPONENT_CODE")))
							htpremium.Add(getAttributeValue(PremiumNode,"COMPONENT_CODE"),getAttributeValue(PremiumNode,"STEPPREMIUM"));
					}
					
					if(gStrtemp == "temp")
					{
						foreach (XmlNode SumTotalNode in GetSumTotalPremium(AutoDetail["VEHICLE_ID"].ToString()))
						{
							if(getAttributeValue(SumTotalNode,"STEPPREMIUM")!=null && getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()!="" )
								sumTtl += double.Parse(getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()) ;						
						}
					}
					else
					{
						string sumtotal = GetPremiumAll(DSTempVehicle, "SUMTOTAL", AutoDetail["VEHICLE_ID"].ToString());
						if (sumtotal != "")
							sumTtl += double.Parse(sumtotal);
					}
					///////////////////////////////////////
					//    Sum Total(end)
					//////////////////////////////////////
					/////////////////////////////////////
					// Insurance Score Discount(Start)
					////////////////////////////////////
					// if proccess not committed
					if(gStrtemp !="final")
					{
						InsScrDis="";
						foreach (XmlNode AutoNode in GetInsuranceDiscountPercent(AutoDetail["VEHICLE_ID"].ToString()))
						{
						
							if(getAttributeValue(AutoNode,"COM_EXT_AD")!=null && getAttributeValue(AutoNode,"COM_EXT_AD").ToString()!="" )
							{
								InsScrDis = getAttributeValue(AutoNode,"COM_EXT_AD").ToString() ;						
													
							}
						}
					}
						// if proccess committed
					else
					{
						InsScrDis="";
						foreach(DataRow CoverageDetails in DSTempVehicle.Tables[1].Rows)
						{
							if(CoverageDetails["COMPONENT_CODE"].ToString() == "D_INS_SCR")
								InsScrDis = CoverageDetails["COM_EXT_AD"].ToString();
						}
					}
					/////////////////////////////////////
					// Insurance Score Discount(end)
					////////////////////////////////////
					////////////////////////////////////////////
					// Michigan Statutory Assessment add it on PIP (Start)
					////////////////////////////////////////////
					// if Process not committed
					if(gStrtemp !="final")
					{
						strMccaFee="0";
						foreach(XmlNode AutoNode in GetMCCAFee(AutoDetail["VEHICLE_ID"].ToString()))
						{
							if(getAttributeValue(AutoNode,"STEPPREMIUM")!=null && getAttributeValue(AutoNode,"STEPPREMIUM").ToString()!="" )
							{
								strMccaFee = getAttributeValue(AutoNode,"STEPPREMIUM").ToString();						
													
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
					
						// MCCA FEE FOR AGENT'S COPY (ACCOUNTING PURPOSE)
						AutoElementDecPage.InnerXml= AutoElementDecPage.InnerXml +  "<AGENCYACCOUNTINFORMATION  " + fieldType +"=\""+ fieldTypeText +"\">"+"**Account Information - "+"$"+RemoveJunkXmlCharacters(DollarFormat(double.Parse(strMccaFee.ToString()))) + "**"+"</AGENCYACCOUNTINFORMATION>";
						if(gStrCopyTo == "AGENCY" && flgError==1)
						{
							AutoElementDecPage.InnerXml= AutoElementDecPage.InnerXml +  "<AGENCYACCOUNTINFORMATION  " + fieldType +"=\""+ fieldTypeText +"\">"+"**Account Information - "+"$"+RemoveJunkXmlCharacters(DollarFormat(double.Parse(strMccaFee.ToString()))) + "**"+"</AGENCYACCOUNTINFORMATION>";
						}
					}

					////////////////////////////////////////////
					// Michigan Statutory Assessment add it on PIP (End)
					////////////////////////////////////////////
					////////////////////////////////////////////
					// Misclanous Schedule Equipment Premium (Start)
					////////////////////////////////////////////
					// Calculate total Limit to display at a-15 and a-16 coverages
					double strtotLimit=0;
//					objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//					objDataWrapper.AddParameter("@POLID",gStrPolicyId);
//					objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//					objDataWrapper.AddParameter("@VEHICLEID",AutoDetail["Vehicle_ID"]);
//					objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//					dstemcovcnt	= objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_MiscEquipment");
//					objDataWrapper.ClearParameteres();
					//dstemcovcnt	= objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_MiscEquipment " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",'" + gStrCalledFrom + "',"  + AutoDetail["Vehicle_ID"]);
					foreach(DataRow EquipLimit in DSTempEquipment.Tables[0].Rows)
					{
						if(EquipLimit["VEHICLE_ID"].ToString()==AutoDetail["Vehicle_ID"].ToString())
							strtotLimit += double.Parse(EquipLimit["ITEM_VALUE"].ToString()); 
					}
					int comcollPrem=0; string strcompEditDate="",strcomcollPrem="";
					if(gStrtemp !="final")
					{
						foreach(XmlNode XtrComCollnode in  GetMiscScheduleEquipmentCompColl(AutoDetail["VEHICLE_ID"].ToString()))
						{
							if(getAttributeValue(XtrComCollnode,"STEPPREMIUM")!=null && getAttributeValue(XtrComCollnode,"STEPPREMIUM").ToString()!="" )
							{
								comcollPrem += int.Parse(getAttributeValue(XtrComCollnode,"STEPPREMIUM").ToString());
							}
						}
					}
					else
					{
						foreach(DataRow CoverageDetails in DSTempVehicle.Tables[1].Rows)
						{
							if(CoverageDetails["COMPONENT_CODE"].ToString() == "XTR_COMP" || CoverageDetails["COMPONENT_CODE"].ToString() == "XTR_COLL")
							{
								double covepremium = 0.0;
								covepremium = double.Parse(CoverageDetails["COVERAGE_PREMIUM"].ToString());
								comcollPrem +=int.Parse(covepremium.ToString());
							}
						}
						foreach(DataRow CovDesc in DSTempVehicle.Tables[0].Rows)
						{
							if(CovDesc["EDITION_DATE"] !=null && CovDesc["COMPONENT_CODE"].ToString() == "XTR_COMP")
							{
								strcompEditDate =CovDesc["EDITION_DATE"].ToString();
							}

						}
					}
					if(comcollPrem != 0 && comcollPrem != 0.00)
					{
						strcomcollPrem = "$" + DollarFormat(double.Parse(comcollPrem.ToString()));
					}
					
					////////////////////////////////////////////
					// Misclanous Schedule Equipment Premium (end)
					////////////////////////////////////////////
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
					//dbEstTotal +=sumTtl ;
					string strVehNo="0";
					string strSuspendedVehicle="";
					strSuspendedVehicle=AutoDetail["SUSPENDEDCOMP_ONLY"].ToString();
					if (gStrPdfFor == PDFForDecPage)
					{
						#region Auto Element for Dec Page
						AutoRootElementDecPage.AppendChild(AutoElementDecPage);
						AutoElementDecPage.SetAttribute(fieldType,fieldTypeNormal);
						AutoElementDecPage.SetAttribute(id,AutoCtr.ToString());
						//Policy
						AutoElementDecPage.InnerXml +="<INSUREDNAME " + fieldType + "=\"" + fieldTypeText + "\">" + NamedInsuredWithSuffix + "</INSUREDNAME>";
						if(gStrCalledFrom.Equals(CalledFromPolicy))
						{					
							AutoElementDecPage.InnerXml +="<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNUMBER>";
							AutoElementDecPage.InnerXml +="<POLICYVERSION " + fieldType + "=\"" + fieldTypeText + "\">" + gStrPolicyVersion + "</POLICYVERSION>";
							AutoElementDecPage.InnerXml +="<POLICYEFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</POLICYEFFDATE>";
							AutoElementDecPage.InnerXml +="<POLICYEXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</POLICYEXPDATE>";
							AutoElementDecPage.InnerXml +="<REASON " + fieldType + "=\"" + fieldTypeText + "\">" + Reason + "</REASON>";
							
						}
			
						// Auto page No
						AutoElementDecPage.InnerXml +="<PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " + "of" +" " +  inttotalpage + "</PAGE_NO>";
						//Auto
						AutoElementDecPage.InnerXml +="<AUTODESCRIPTION  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["INSURED_VEH_NUMBER"].ToString()) + "  "  + RemoveJunkXmlCharacters(AutoDetail["VEHICLE_YEAR"].ToString()) +"/"+ RemoveJunkXmlCharacters(AutoDetail["MAKE"].ToString()) +"/" + RemoveJunkXmlCharacters(AutoDetail["MODEL"].ToString()) + "/" + RemoveJunkXmlCharacters(AutoDetail["VIN"].ToString()) +"     "+ RemoveJunkXmlCharacters(AutoDetail["BODY_TYPE"].ToString()) +"</AUTODESCRIPTION>"; 
						AutoElementDecPage.InnerXml +="<TERRITORY  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["TERRITORY"].ToString())+"</TERRITORY>"; 
						
						AutoElementDecPage.InnerXml +="<VEHICLECLASS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["CLASS"].ToString())+"</VEHICLECLASS>";
						if(AutoDetail["USE_VEHICLE"].ToString().Trim() != "" && AutoDetail["MILES_TO_WORK"].ToString().Trim() != "" && AutoDetail["MILES_TO_WORK"].ToString().Trim() != "0")
							AutoElementDecPage.InnerXml +="<USEVEHICLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["USE_VEHICLE"].ToString()) + "/" + RemoveJunkXmlCharacters(AutoDetail["MILES_TO_WORK"].ToString())  +"</USEVEHICLE>";
						else
							AutoElementDecPage.InnerXml +="<USEVEHICLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["USE_VEHICLE"].ToString()) +"</USEVEHICLE>";
						AutoElementDecPage.InnerXml +="<INSURANCESCORE  " + fieldType +"=\""+ fieldTypeText +"\">"+strInsScore+"</INSURANCESCORE>";
						AutoElementDecPage.InnerXml +="<INSURANCEDISCOUNT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(InsScrDis)+"</INSURANCEDISCOUNT>";
						if(stCode.Equals("IN"))
							AutoElementDecPage.InnerXml +="<INSURANCETYPE  " + fieldType +"=\""+ fieldTypeText +"\">"+strUwTier+"</INSURANCETYPE>";
						else
							AutoElementDecPage.InnerXml +="<INSURANCETYPE  " + fieldType +"=\""+ fieldTypeText +"\">"+strInsType+"</INSURANCETYPE>";

						AutoElementDecPage.InnerXml +="<VEHICLEUSE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["VEHICLE_USE"].ToString())+"</VEHICLEUSE>";
						
						AutoElementDecPage.InnerXml +="<GARAGEADD  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["GRG_ADD"].ToString())+"</GARAGEADD>";
						AutoElementDecPage.InnerXml +="<GARAGECITY  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["GRG_CITYSTZIP"].ToString())+"</GARAGECITY>";
						
						AutoElementDecPage.InnerXml= AutoElementDecPage.InnerXml +  "<VEHICLETYPE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["VEHICLE_TYPE"].ToString())+"</VEHICLETYPE>";
						AutoElementDecPage.InnerXml= AutoElementDecPage.InnerXml +  "<VEHICLESYMBOL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["SYMBOL"].ToString())+"</VEHICLESYMBOL>";
						//
						// Total vehicle premium and policy Premium and Adjusted Premium
						AutoElementDecPage.InnerXml= AutoElementDecPage.InnerXml +  "<TOTAL_VEHICLE_PREMIUM  " + fieldType +"=\""+ fieldTypeText +"\">$"+RemoveJunkXmlCharacters(DollarFormat(sumTtl))+"</TOTAL_VEHICLE_PREMIUM>";
						AutoElementDecPage.InnerXml= AutoElementDecPage.InnerXml +  "<TOTAL_POLICY_PREMIUM  " + fieldType +"=\""+ fieldTypeText +"\">$"+RemoveJunkXmlCharacters(DollarFormat(dbEstTotal))+"</TOTAL_POLICY_PREMIUM>";
						AutoElementDecPage.InnerXml= AutoElementDecPage.InnerXml +  "<DATE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(System.DateTime.Today.ToString("MM/dd/yyyy") )+"</DATE>";
						AutoElementDecPage.InnerXml= AutoElementDecPage.InnerXml +  "<PREMIUM_ADJUST  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(adjPrm)+"</PREMIUM_ADJUST>";

						#endregion
					}
					else if (gStrPdfFor == PDFForAcord)
					{
						#region Auto Element for Acord90						
						AutoRootElementAcord90.AppendChild(AutoElementAcord90);
						AutoElementAcord90.SetAttribute(fieldType,fieldTypeNormal);
						AutoElementAcord90.SetAttribute(id,AutoCtr.ToString());
						
						string MakeModelBody = "";
						if(AutoDetail["MAKE"].ToString() != "" && AutoDetail["MODEL"].ToString() != "")
							MakeModelBody = AutoDetail["MAKE"].ToString() + "/" + AutoDetail["MODEL"].ToString();
						else
							MakeModelBody = AutoDetail["MAKE"].ToString() + AutoDetail["MODEL"].ToString();

						if(MakeModelBody != "" && AutoDetail["BODY_TYPE"].ToString() != "")
							MakeModelBody += "/" + AutoDetail["BODY_TYPE"].ToString();
						else
							MakeModelBody += AutoDetail["BODY_TYPE"].ToString();

						strVehNo=AutoDetail["INSURED_VEH_NUMBER"].ToString();
						AutoElementAcord90.InnerXml += "<VEHICLENUMBER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["INSURED_VEH_NUMBER"].ToString())+"</VEHICLENUMBER>";
						AutoElementAcord90.InnerXml += "<YEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["VEHICLE_YEAR"].ToString())+"</YEAR>";
						if(RemoveJunkXmlCharacters(AutoDetail["LEASED"].ToString()) == "")
							AutoElementAcord90.InnerXml += "<MAKEMODELBODYTYPE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(MakeModelBody) +"</MAKEMODELBODYTYPE>"; 
						else
							AutoElementAcord90.InnerXml += "<MAKEMODELBODYTYPE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(MakeModelBody) +" / " + RemoveJunkXmlCharacters(AutoDetail["LEASED"].ToString()) +"</MAKEMODELBODYTYPE>"; 
						
						if(RemoveJunkXmlCharacters(AutoDetail["STATE_NAME"].ToString()) == "")
							AutoElementAcord90.InnerXml += "<VINREGISTEREDSTATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["VIN"].ToString()) +"</VINREGISTEREDSTATE>"; 
						else
							AutoElementAcord90.InnerXml += "<VINREGISTEREDSTATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["VIN"].ToString()) + " / " + RemoveJunkXmlCharacters(AutoDetail["STATE_NAME"].ToString()) +"</VINREGISTEREDSTATE>"; 

						AutoElementAcord90.InnerXml +="<HPCC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["HPCC"].ToString()) +"</HPCC>"; 
						if(AutoDetail["LEASED"]!=null && AutoDetail["LEASED"].ToString()!="")
						{
							if(AutoDetail["LEASED"].ToString().Equals("LEASED")) 
								AutoElementAcord90.InnerXml +="<DATELEASED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["DATEPURCHASED"].ToString())+"</DATELEASED>"; 
							else
								AutoElementAcord90.InnerXml +="<DATEPURCHASED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["DATEPURCHASED"].ToString())+"</DATEPURCHASED>"; 

						}
							
						
						AutoElementAcord90.InnerXml +="<NEWUSED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["NEWUSED"].ToString())+"</NEWUSED>"; 
						AutoElementAcord90.InnerXml += "<VEHICLENUMBER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["INSURED_VEH_NUMBER"].ToString())+"</VEHICLENUMBER>";

						AutoElementAcord90.InnerXml +="<COSTNEW " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["AMOUNT"].ToString())+"</COSTNEW>"; 
						AutoElementAcord90.InnerXml +="<SYMBOLAGEGROUP  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["SYMBOL"].ToString()) +" / " + RemoveJunkXmlCharacters(AutoDetail["AGEGROUP"].ToString()) +"</SYMBOLAGEGROUP>"; 
						AutoElementAcord90.InnerXml +="<TERRITORY  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["TERRITORY"].ToString())+"</TERRITORY>"; 
						AutoElementAcord90.InnerXml +="<MILESATWORK   " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["MILES_TO_WORK"].ToString())+"</MILESATWORK>"; 
						AutoElementAcord90.InnerXml += "<USAGE   " + fieldType +"=\""+ fieldTypeText +"\">" + RemoveJunkXmlCharacters(AutoDetail["USAGE"].ToString()) + "</USAGE>"; 
						AutoElementAcord90.InnerXml += "<MULTICAR   " + fieldType +"=\""+ fieldTypeText +"\">" + RemoveJunkXmlCharacters(AutoDetail["MULTI_CAR"].ToString()) + "</MULTICAR>"; 


						AutoElementAcord90.InnerXml += "<ANNUALMILEAGE   " + fieldType +"=\""+ fieldTypeText +"\">" + RemoveJunkXmlCharacters(AutoDetail["ANNUAL_MILEAGE"].ToString()) + "</ANNUALMILEAGE>"; 
						AutoElementAcord90.InnerXml += "<CLASS   " + fieldType +"=\""+ fieldTypeText +"\">" + RemoveJunkXmlCharacters(AutoDetail["CLASS"].ToString()) + "</CLASS>"; 
						AutoElementAcord90.InnerXml += "<AIRBAG   " + fieldType +"=\""+ fieldTypeText +"\">" + RemoveJunkXmlCharacters(AutoDetail["AIRBAG"].ToString()) + "</AIRBAG>"; 
						AutoElementAcord90.InnerXml += "<ANTILOCK   " + fieldType +"=\""+ fieldTypeText +"\">" + RemoveJunkXmlCharacters(AutoDetail["ANTI_LOCK_BRAKES"].ToString()) + "</ANTILOCK>"; 
						AutoElementAcord90.InnerXml += "<CREDITANDSURCH   " + fieldType +"=\""+ fieldTypeText +"\">" + RemoveJunkXmlCharacters("See Supplemental App") + "</CREDITANDSURCH>"; 
						//if(stCode.Equals("IN"))  
						AutoElementAcord90.InnerXml += "<PASSIVE_SEATBELT   " + fieldType +"=\""+ fieldTypeText +"\">" + RemoveJunkXmlCharacters(AutoDetail["PASSIVE_SEAT_BELT"].ToString()) + "</PASSIVE_SEATBELT>"; 
					
						// Added by Mohit Agarwal 11 Apr 2007

						objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						objDataWrapper.AddParameter("@POLID",gStrPolicyId);
						objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						objDataWrapper.AddParameter("@VEHICLEID",AutoDetail["VEHICLE_ID"].ToString());
						DataSet DSAssgnDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_AssgnDriv");
						objDataWrapper.ClearParameteres();
						//DataSet DSAssgnDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_AssgnDriv " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'," + AutoDetail["VEHICLE_ID"].ToString());			
						if(DSAssgnDataSet.Tables[0].Rows.Count > 0)
						{
							AutoElementAcord90.InnerXml += "<GOVERNDRIV   " + fieldType +"=\""+ fieldTypeText +"\">" + RemoveJunkXmlCharacters(DSAssgnDataSet.Tables[0].Rows[0]["DRIVER_ID"].ToString()) + "</GOVERNDRIV>"; 
						}
						#endregion

						#region Auto Element for Supplement
						AutoRootElementSupplement.AppendChild(AutoElementSuppliment);
						AutoElementSuppliment.SetAttribute(fieldType,fieldTypeNormal);
						AutoElementSuppliment.SetAttribute(id,AutoCtr.ToString());

						AutoElementSuppliment.InnerXml += "<VEHICLE_NUMBER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["INSURED_VEH_NUMBER"].ToString())+"</VEHICLE_NUMBER>";
					
						AutoElementSuppliment.InnerXml += "<YEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AutoDetail["VEHICLE_YEAR"].ToString()) + "/" +RemoveJunkXmlCharacters(AutoDetail["MAKE"].ToString() + "/" +AutoDetail["MODEL"].ToString() + "/" +AutoDetail["VIN"].ToString()) +"</YEAR>";
						AutoElementSuppliment.InnerXml += "<INSURANCE_SCORE " + fieldType +"=\""+ fieldTypeText +"\">"+strInsScore+"</INSURANCE_SCORE>";
						AutoElementSuppliment.InnerXml += "<INSURANCE_PERCENTAGE " + fieldType +"=\""+ fieldTypeText +"\">"+inspercent+"</INSURANCE_PERCENTAGE>";
						AutoElementSuppliment.InnerXml += "<INSURANCE_TYPE " + fieldType +"=\""+ fieldTypeText +"\">"+strInsType+"</INSURANCE_TYPE>";

						#region MISCELLEANEOUS EQUIPMENT
						XmlElement SchEquipmentRootElement;
						SchEquipmentRootElement = AcordPDFXML.CreateElement("SCHEQUIPMENT");
						AutoElementSuppliment.AppendChild(SchEquipmentRootElement);
						SchEquipmentRootElement.SetAttribute(fieldType,fieldTypeMultiple);
						SchEquipmentRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTEQUIPMENT"));
						SchEquipmentRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTEQUIPMENT"));
						SchEquipmentRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTEQUIPMENTEXTN"));
						SchEquipmentRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTEQUIPMENTEXTN"));
						
//						objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//						objDataWrapper.AddParameter("@POLID",gStrPolicyId);
//						objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//						objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//						objDataWrapper.AddParameter("@VEHICLEID",AutoDetail["VEHICLE_ID"]);
//						DSTempEquip = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_MiscEquipment");
//						objDataWrapper.ClearParameteres();
						//DSTempEquip = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_MiscEquipment " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "',"  +AutoDetail["VEHICLE_ID"] );
						int intEquipCtr=0;
						foreach(DataRow EquipDetail in DSTempEquipment.Tables[0].Rows)
						{
							if(EquipDetail["VEHICLE_ID"].ToString()==AutoDetail["Vehicle_ID"].ToString())
							{
								XmlElement EquipmentElement;
								EquipmentElement = AcordPDFXML.CreateElement("OPERATORINFO");
								SchEquipmentRootElement.AppendChild(EquipmentElement);
								EquipmentElement.SetAttribute(fieldType,fieldTypeNormal);
								EquipmentElement.SetAttribute(id,intEquipCtr.ToString());

								EquipmentElement.InnerXml +=  "<AUTO_SCHEDULEDDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EquipDetail["ITEM_DESCRIPTION"].ToString())+"</AUTO_SCHEDULEDDESC>"; 
								if(EquipDetail["ITEM_VALUE"].ToString()!="")
								{
									EquipmentElement.InnerXml +=  "<AUTO_AMOUNT " + fieldType +"=\""+ fieldTypeText +"\">$"+RemoveJunkXmlCharacters(EquipDetail["ITEM_VALUE"].ToString())+"</AUTO_AMOUNT>"; 
								}
								//EquipmentElement.InnerXml +=  "<INSURANCE_SCORE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EquipDetail["ITEM_VALUE"].ToString())+"</INSURANCE_SCORE>"; 


								intEquipCtr++;
							}
						}
						#endregion

						#endregion
					}

					#region COVERAGES
						//To calculate Estimated Total

					#region setting Root liability coverages Attribute
					XmlElement LCRootElement;
					LCRootElement = AcordPDFXML.CreateElement("LIABCOVERAGES");
					AutoElementDecPage.AppendChild(LCRootElement);
					LCRootElement.SetAttribute(fieldType,fieldTypeMultiple);
					LCRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECLBLCOVERAGE"));
					LCRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECLBLCOVERAGE"));
					LCRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECLBLCOVERAGEEXTN"));
					LCRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECLBLCOVERAGEEXTN"));
					#endregion

					int lcCtr=0;//,dblSumTotal=0;
					//string SecCoveLimit = "";
					string strDec = "";
					string strDectxt = "";
					string strPrem = "";
					//int intliacov=0;
				
					
					foreach(DataRow CoverageDetails in DSTempVehicle.Tables[0].Rows)
					{
						#region Policy Level Coverages for Dec Page
						if (gStrPdfFor == PDFForDecPage)
						{
							//							if(lcCtr > 4)
							//								break;
							
							string CovCode = CoverageDetails["COV_CODE"].ToString();
							string CovDes = "";
							string CovLimit = "";
							string flgEdit="";
							
							if((CoverageDetails["COVERAGE_TYPE"].ToString() == "PL") && (CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString()!="Reject" && CoverageDetails["LIMIT2_AMOUNT_TEXT"].ToString()!="Reject")) //CovCode.Equals("SLL") || CovCode.Equals("BISPL") || CovCode.Equals("UNCSL") || CovCode.Equals("PUNCS") || CovCode.Equals("PD") || CovCode.Equals("UNDSP") || CovCode.Equals("PUMSP") || CovCode.Equals("PIP")  || CovCode.Equals("CAB91") || CovCode.Equals("PIP94") || CovCode.Equals("PPI")  || CovCode.Equals("UMPD") || CovCode.Equals("RRUM") || CovCode.Equals("MP") || CovCode.Equals("ENO")  )
							{
								XmlElement LCElement;
								LCElement = AcordPDFXML.CreateElement("LIABCOVERAGESINFO");
								// if ENO or Excluded (A-95 or A-96 or A-10 or A-91) exists then edition date should be
								foreach(DataRow dtRow in DSTempVehicle.Tables[0].Rows)
								{
									if(dtRow["COV_CODE"].ToString()=="ENO" || dtRow["COV_CODE"].ToString()=="CAB91" || dtRow["COV_CODE"].ToString()=="EP95" ||dtRow["COV_CODE"].ToString()=="A10" ||dtRow["COV_CODE"].ToString()=="RRUM" )
									{
										flgEdit="Y";
									}
								}
								
								// If extened nonowned coverage and risk is not first  one
								if(CovCode=="ENO" && (intEno>=1 || strSuspendedVehicle =="TRUE" || AutoDetail["RISKTYPE"].ToString()=="TR" || AutoDetail["RISKTYPE"].ToString()=="CTT" || AutoDetail["RISKTYPE"].ToString()=="SCO"))
								{
								}
								else
								{
										LCRootElement.AppendChild(LCElement);
										LCElement.SetAttribute(fieldType,fieldTypeNormal);
										LCElement.SetAttribute(id,lcCtr.ToString());

										//PAGE INFO
										if(lcCtr >= 8 && lcCtr < 9 )
										{
											intpageno++;
										}
										LCElement.InnerXml +="<AT_LC_AUTODESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(AutoDetail["INSURED_VEH_NUMBER"].ToString()) + "  "  + RemoveJunkXmlCharacters(AutoDetail["VEHICLE_YEAR"].ToString()) +"/"+ RemoveJunkXmlCharacters(AutoDetail["MAKE"].ToString()) +"/" + RemoveJunkXmlCharacters(AutoDetail["MODEL"].ToString()) + "/" + RemoveJunkXmlCharacters(AutoDetail["VIN"].ToString()) +"     "+ RemoveJunkXmlCharacters(AutoDetail["BODY_TYPE"].ToString()) +"</AT_LC_AUTODESCRIPTION>";
										LCElement.InnerXml +="<AT_LC_PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " +  "of" + " " +  inttotalpage +  "</AT_LC_PAGE_NO>";
									if(flgEdit=="Y")
										LCElement.InnerXml +="<AH_LC_EDITIONDATE " + fieldType + "=\"" + fieldTypeText + "\">" + "Edition Date" +  "</AH_LC_EDITIONDATE>";
									lcCtr++;
								}
								// fromating for Coverage limit
								if(CovCode.Trim() == "PIP")
								{
									
								}
								else
								{
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
																	
								}
								//formating for Deductible
								strDec = CoverageDetails["DEDUCTIBLE_1"].ToString();
								if(strDec=="0" || strDec=="$0.00" || strDec=="$0" || strDec=="0.00" || strDec=="")
									strDec="";
								else									
								strDec = GetIntFormat(strDec);

								// formating for Premium
								if(gStrtemp == "temp")
								{
									strPrem=GetPremiumBeforeCommit(DSTempVehicle,CovCode,htpremium);
									if(CovCode.Trim() == "PIP")
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
									if(CovCode.Trim() == "PIP")
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
								
								// Individual Mapping of policy level coverages
								#region SWITCH CASE
								switch(CoverageDetails["COV_CODE"].ToString())
								{
										// Bodily Injury Liability (Split Limit)
									case "BISPL":
									{
										LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_DESCRIPTION>";
										LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LIMIT>";
										LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";
										LCElement.InnerXml += "<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";
										//lcCtr++;
									}
										break;
									// Property Damage Liability	
									case "PD":
									{
										LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_DESCRIPTION>";
										LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LIMIT>";
										LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";
										LCElement.InnerXml += "<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";
										//lcCtr++;
									}
										break;
										// Single Limit Liability CSL	
									case "SLL":
									{
										LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_DESCRIPTION>";
										LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LIMIT>";
										LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";
										LCElement.InnerXml += "<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";
										//lcCtr++;
									}
										break;
										// Uninsured Motorist (BI Split Limits)
									case "PUMSP":
									{
										if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString()!="Reject" && CoverageDetails["LIMIT2_AMOUNT_TEXT"].ToString()!="Reject")
										{
											LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_DESCRIPTION>";
											LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LIMIT>";
											LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";
											// If uninsured premium is 0 then show it as included itrack 4266
											if(strPrem =="" || strPrem =="0" || strPrem == "0.00")
											{
												LCElement.InnerXml += "<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_PREMIUM>";
											}
											else
											{
												LCElement.InnerXml += "<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";
											}
											//lcCtr++;
										}
									}
										break;
										// Underinsured Motorists (BI Split Limit)
									case "UNDSP":
									{
										if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString()!="Reject" && CoverageDetails["LIMIT2_AMOUNT_TEXT"].ToString()!="Reject")
										{
											LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_DESCRIPTION>";
											LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LIMIT>";
											LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";
											LCElement.InnerXml += "<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";
											//lcCtr++;
										}
									}
										break;
										// Property Protection Insurance
									case "PPI":
									{
										LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_DESCRIPTION>";
										LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LIMIT>";
										LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";
										LCElement.InnerXml += "<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";
										//lcCtr++;
									}
										break;
										// Uninsured Motorists (CSL)
									case "PUNCS":
									{
										if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString()!="Reject" && CoverageDetails["LIMIT2_AMOUNT_TEXT"].ToString()!="Reject")
										{
											LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_DESCRIPTION>";
											LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LIMIT>";
											LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";
											LCElement.InnerXml += "<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";
											//lcCtr++;
										}
									}
										break;
										// Underinsured Motorists (CSL)
									case "UNCSL":
									{
										if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString()!="Reject" && CoverageDetails["LIMIT2_AMOUNT_TEXT"].ToString()!="Reject")
										{
											LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_DESCRIPTION>";
											LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LIMIT>";
											LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";
											LCElement.InnerXml += "<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";
											//lcCtr++;
										}
									}
										break;
										// A-91 Coordination of Benefits
									case "CAB91":
									{
										LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"                                                              "+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_DESCRIPTION>";
										LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LIMIT>";
										LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_DEDUCTIBLE>";
										LCElement.InnerXml += "<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_PREMIUM>";
										//lcCtr++;
									}
										break;
										// A-95 Excluded Person(s) and A-96 Excluded Person(s)
									case "EP95":
									{
										// CHECK SIGNATURE OBTAINED IF IT IS THEN THIS ENDORSEMENT AND CORRESPONDING FORM WILL BE OTHERWISE NO
										string strEP95="";
										if(DSTempVehicle.Tables[2].Rows !=null)
										{
											foreach(DataRow DriverDetails in DSTempVehicle.Tables[2].Rows)
											{
												if(DriverDetails["SIGNATURE_OBT"].ToString() == "10963")
												{
													strEP95="TRUE";
												}
											}
										}
										if(strEP95 == "TRUE")
										{
											LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString()) + "                                                                          "+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_DESCRIPTION>";
											LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LIMIT>";
											LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";
											LCElement.InnerXml += "<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_PREMIUM>";
											//lcCtr++;

											// name of the excluded drivers in the coverage A-95 and A-96
											if(DSTempVehicle.Tables[2].Rows !=null)
											{
												string flgExtenDrivr = "";
												foreach(DataRow DriverDetails in DSTempVehicle.Tables[2].Rows)
												{
													if(DriverDetails["EXCEXTEND_DRIVER"].ToString() == "3477")
													{
														//PAGE INFO
														if(lcCtr >= 8 && lcCtr < 9 )
														{
															intpageno++;
														}
														LCElement = AcordPDFXML.CreateElement("LIABCOVERAGESINFO");
														LCRootElement.AppendChild(LCElement);
														LCElement.SetAttribute(fieldType,fieldTypeNormal);
														LCElement.SetAttribute(id,lcCtr.ToString());
														LCElement.InnerXml +="<AT_LC_AUTODESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(AutoDetail["INSURED_VEH_NUMBER"].ToString()) + "  "  + RemoveJunkXmlCharacters(AutoDetail["VEHICLE_YEAR"].ToString()) +"/"+ RemoveJunkXmlCharacters(AutoDetail["MAKE"].ToString()) +"/" + RemoveJunkXmlCharacters(AutoDetail["MODEL"].ToString()) + "/" + RemoveJunkXmlCharacters(AutoDetail["VIN"].ToString()) +"     "+ RemoveJunkXmlCharacters(AutoDetail["BODY_TYPE"].ToString()) +"</AT_LC_AUTODESCRIPTION>";
														LCElement.InnerXml +="<AT_LC_PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " +  "of" + " " +  inttotalpage +  "</AT_LC_PAGE_NO>";
														LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+"   " +RemoveJunkXmlCharacters(DriverDetails["DRIVER_NAME"].ToString())+ " born "+ RemoveJunkXmlCharacters(DriverDetails["DRIVER_DOB"].ToString())+ " is an Excluded Driver" + "</AT_DESCRIPTION>";
														//if(lcCtr==9)
														LCElement.InnerXml +="<AH_LC_EDITIONDATE " + fieldType + "=\"" + fieldTypeText + "\">" + "Edition Date" +  "</AH_LC_EDITIONDATE>";
														lcCtr ++;
														flgExtenDrivr = "TRUE";
													}
												}
												if(flgExtenDrivr =="TRUE")
												{
													if(lcCtr == 7)
													{
														LCElement = AcordPDFXML.CreateElement("LIABCOVERAGESINFO");
														LCRootElement.AppendChild(LCElement);
														LCElement.SetAttribute(fieldType,fieldTypeNormal);
														LCElement.SetAttribute(id,lcCtr.ToString());
														lcCtr++;
													}
													foreach(DataRow DriverDetails in DSTempVehicle.Tables[3].Rows)
													{
														//PAGE INFO
														if(lcCtr >= 8 && lcCtr < 9 )
														{
															intpageno++;
														}
														LCElement = AcordPDFXML.CreateElement("LIABCOVERAGESINFO");
														LCRootElement.AppendChild(LCElement);
														LCElement.SetAttribute(fieldType,fieldTypeNormal);
														LCElement.SetAttribute(id,lcCtr.ToString());
														LCElement.InnerXml +="<AT_LC_AUTODESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(AutoDetail["INSURED_VEH_NUMBER"].ToString()) + "  "  + RemoveJunkXmlCharacters(AutoDetail["VEHICLE_YEAR"].ToString()) +"/"+ RemoveJunkXmlCharacters(AutoDetail["MAKE"].ToString()) +"/" + RemoveJunkXmlCharacters(AutoDetail["MODEL"].ToString()) + "/" + RemoveJunkXmlCharacters(AutoDetail["VIN"].ToString()) +"     "+ RemoveJunkXmlCharacters(AutoDetail["BODY_TYPE"].ToString()) +"</AT_LC_AUTODESCRIPTION>";
														LCElement.InnerXml +="<AT_LC_PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " +  "of" + " " +  inttotalpage +  "</AT_LC_PAGE_NO>";
														LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(DriverDetails["DESCRIPTION_MESSAGE"].ToString())+"</AT_DESCRIPTION>";
														LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DriverDetails["LIMIT_MESSAGE"].ToString())+"</AT_LIMIT>";
														LCElement.InnerXml += "<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DriverDetails["DEDUCTIBLE_MESSAGE"].ToString())+"</AT_DEDUCTIBLE>";
														//if(lcCtr==9)
															LCElement.InnerXml +="<AH_LC_EDITIONDATE " + fieldType + "=\"" + fieldTypeText + "\">" + "Edition Date" +  "</AH_LC_EDITIONDATE>";
														lcCtr++;
													}	
												}
											}
										}
									}
										break;
										// PIP Waiver/Rejection of Work Loss Benefit A-94
									case "PIP94":
									{
										LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_DESCRIPTION>";
										LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LIMIT>";
										LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";
										LCElement.InnerXml += "<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_PREMIUM>";
										//lcCtr++;
									}
										break;
										
										// Extended Non-Owned Coverage for Named Individual (A-34) and A-35 Extended Non-Owned Coverage for Named Individual
									case "ENO":
									{
										if(strSuspendedVehicle !="TRUE" && AutoDetail["RISKTYPE"].ToString()!="TR" && AutoDetail["RISKTYPE"].ToString()!="CTT" && AutoDetail["RISKTYPE"].ToString()!="SCO")
										{
											if(intEno == 0)
											{
												LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString()) + "          "+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_DESCRIPTION>";
												LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LIMIT>";
												LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";
												LCElement.InnerXml += "<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";
												//lcCtr++;
												// name of the excluded drivers in the coverage A-95 and A-96
												if(DSTempVehicle.Tables[2].Rows !=null)
												{
													foreach(DataRow DriverDetails in DSTempVehicle.Tables[2].Rows)
													{
														if(DriverDetails["EXCEXTEND_DRIVER"].ToString() == "10963")
														{
															//PAGE INFO
															if(lcCtr >= 8 && lcCtr < 9 )
															{
																intpageno++;
															}
															LCElement = AcordPDFXML.CreateElement("LIABCOVERAGESINFO");
															LCRootElement.AppendChild(LCElement);
															LCElement.SetAttribute(fieldType,fieldTypeNormal);
															LCElement.SetAttribute(id,lcCtr.ToString());
															LCElement.InnerXml +="<AT_LC_AUTODESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(AutoDetail["INSURED_VEH_NUMBER"].ToString()) + "  "  + RemoveJunkXmlCharacters(AutoDetail["VEHICLE_YEAR"].ToString()) +"/"+ RemoveJunkXmlCharacters(AutoDetail["MAKE"].ToString()) +"/" + RemoveJunkXmlCharacters(AutoDetail["MODEL"].ToString()) + "/" + RemoveJunkXmlCharacters(AutoDetail["VIN"].ToString()) +"     "+ RemoveJunkXmlCharacters(AutoDetail["BODY_TYPE"].ToString()) +"</AT_LC_AUTODESCRIPTION>";
															LCElement.InnerXml +="<AT_LC_PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " +  "of" + " " +  inttotalpage +  "</AT_LC_PAGE_NO>";
															LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+"    " +RemoveJunkXmlCharacters(DriverDetails["DRIVER_NAME"].ToString())+"</AT_DESCRIPTION>";
															//if(lcCtr==9)
																LCElement.InnerXml +="<AH_LC_EDITIONDATE " + fieldType + "=\"" + fieldTypeText + "\">" + "Edition Date" +  "</AH_LC_EDITIONDATE>";
															lcCtr ++;
														}
													}
												}
											}
											intEno++;
										}
										
									}
										break;

										// Medical Payments 
									case "MP":
									{
										LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_DESCRIPTION>";
										LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LIMIT>";
										LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";
										LCElement.InnerXml += "<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";
										//lcCtr++;
									}
										break;
										
										// Rejection / Reduction of Uninsured & Underinsured Motorist Coverage (A-9)
									case "RRUM":
									{
										LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+ "     "+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_DESCRIPTION>";
										LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LIMIT>";
										LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";
										LCElement.InnerXml += "<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_PREMIUM>";
										//lcCtr++;
									}
										break;
										
										// Uninsured Motorist PD
									case "UMPD":
									{
										if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString()!="Reject" && CoverageDetails["LIMIT2_AMOUNT_TEXT"].ToString()!="Reject")
										{
											LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_DESCRIPTION>";
											LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LIMIT>";
											if(strDec =="0" || strDec =="" || strDec =="0.00" || strDec =="$0" || strDec =="$0.00")
												LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";
											else
												LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_DEDUCTIBLE>";
											LCElement.InnerXml += "<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";
											//lcCtr++;
										}
									}
										break;
									case "PIP":
									{
										CovDes = CoverageDetails["COV_DES"].ToString();
										if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString() != "")
											CovDes += " - " + CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString();
										LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovDes)+"</AT_DESCRIPTION>";
										LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Unlimited")+"</AT_LIMIT>";
										LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_DEDUCTIBLE>";
										LCElement.InnerXml += "<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";
										//lcCtr++;
									}
										break;
									default: 
									{
										CovDes = CoverageDetails["COV_DES"].ToString();
										if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString() != "")
											CovDes += " - " + CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString();
										if(CoverageDetails["COV_CODE"].ToString()=="A10")
										{
											LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovDes)+ "                                                           "+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_DESCRIPTION>";
										}
										else
										{
											LCElement.InnerXml +="<AT_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovDes)+"</AT_DESCRIPTION>";
										}

										if(CovLimit =="0" || CovLimit =="" || CovLimit =="0.00" || CovLimit =="$0" || CovLimit =="$0.00")
											LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LIMIT>";
										else
											LCElement.InnerXml +="<AT_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LIMIT>";
										if(strDec =="0" || strDec =="" || strDec =="0.00" || strDec =="$0" || strDec =="$0.00")
											LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_DEDUCTIBLE>";
										else
											LCElement.InnerXml +="<AT_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_DEDUCTIBLE>";
										if(strPrem =="0" || strPrem =="" || strPrem =="0.00" || strPrem =="$0" || strPrem =="$0.00")
											LCElement.InnerXml += "<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_PREMIUM>";
										else
											LCElement.InnerXml += "<AT_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_PREMIUM>";
										//lcCtr++;
									}
										break;
								}
								#endregion 
							}
							
						}
								#endregion
							#region Policy Level Coverages for Accord Page
						else if (gStrPdfFor == PDFForAcord)
						{
							policyCovFlag=1;
							string CovCode = CoverageDetails["COV_CODE"].ToString();

							#region SWITCH CASE
							switch(CoverageDetails["COV_CODE"].ToString())
							{
								case "SLL":
									AutoElementAcord90.InnerXml +="<SINGLELIMITLIABILITY " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</SINGLELIMITLIABILITY>";							

									if(CoverageDetails["DEDUCTIBLE_1"]==null || CoverageDetails["DEDUCTIBLE_1"].ToString()=="")
										AutoElementAcord90.InnerXml +="<UNINSUREDMOTORISTSPDAFTER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</UNINSUREDMOTORISTSPDAFTER>";
									
									//if(htpremium.Contains("CSL"))
									{
										//AutoElementAcord90.InnerXml +="<CSL_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CSL"].ToString() + ".00")+"</CSL_PREMIUM>";
										AutoElementAcord90.InnerXml +="<CSL_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempVehicle,CovCode))+"</CSL_PREMIUM>";
										//dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
									}
									break;
								case "BISPL":
									AutoElementAcord90.InnerXml +="<BODILYINJURYLIABILITYBEFORE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</BODILYINJURYLIABILITYBEFORE>";
									AutoElementAcord90.InnerXml +="<BODILYINJURYLIABILITYAFTER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_2"].ToString())+"</BODILYINJURYLIABILITYAFTER>";
							
									//if(htpremium.Contains("BI"))
									{
										//AutoElementAcord90.InnerXml +="<BI_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BI"].ToString() + ".00")+"</BI_PREMIUM>";
										AutoElementAcord90.InnerXml +="<BI_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempVehicle,CovCode))+"</BI_PREMIUM>";
										//dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
									}
									break;
								case "PD":
									AutoElementAcord90.InnerXml +="<PROPERTYDAMAGELIABILITY " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</PROPERTYDAMAGELIABILITY>";
									//if(htpremium.Contains("PD"))
									{
										//AutoElementAcord90.InnerXml +="<PD_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PD"].ToString() + ".00")+"</PD_PREMIUM>";
										AutoElementAcord90.InnerXml +="<PD_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempVehicle,CovCode))+"</PD_PREMIUM>";
										//dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
									}

									break;						
								case "MP":
									AutoElementAcord90.InnerXml +="<MEDICALPAYMENTS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</MEDICALPAYMENTS>";
									//if(htpremium.Contains("MP"))
									{
										//AutoElementAcord90.InnerXml +="<MP_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["MP"].ToString() + ".00")+"</MP_PREMIUM>";
										AutoElementAcord90.InnerXml +="<MP_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempVehicle,CovCode))+"</MP_PREMIUM>";
										//dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
									}
									break;
								case "PUNCS":
									AutoElementAcord90.InnerXml +="<UNINSUREDMOTORISTSAFTER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</UNINSUREDMOTORISTSAFTER>";								
									AutoElementAcord90.InnerXml +="<UNINSUREDMOTORISTPDSAFTER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</UNINSUREDMOTORISTPDSAFTER>";
									//if(htpremium.Contains("UM"))
									{
										//AutoElementAcord90.InnerXml +="<UM_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["UM"].ToString() + ".00")+"</UM_PREMIUM>";
										AutoElementAcord90.InnerXml +="<UM_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempVehicle,CovCode))+"</UM_PREMIUM>";
										//dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
									}
									break;
								case "PUMSP":
									if(CoverageDetails["LIMIT_1"].ToString()!="" )
									{
										AutoElementAcord90.InnerXml +="<UNINSUREDMOTORISTSBEFORE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</UNINSUREDMOTORISTSBEFORE>";
									}
									
									if(CoverageDetails["LIMIT_2"].ToString()!="" )
									{
										AutoElementAcord90.InnerXml +="<UNINSUREDMOTORISTSAFTER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_2"].ToString())+"</UNINSUREDMOTORISTSAFTER>";
									}

									//if(htpremium.Contains("UM"))
									{
										//AutoElementAcord90.InnerXml +="<UM_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["UM"].ToString() + ".00")+"</UM_PREMIUM>";
										AutoElementAcord90.InnerXml +="<UM_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempVehicle,CovCode))+"</UM_PREMIUM>";
										//dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
									}

									break;
								case "UMPD":
									AutoElementAcord90.InnerXml +="<UNINSUREDMOTORISTSPDBEFORE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</UNINSUREDMOTORISTSPDBEFORE>";

									//if(CoverageDetails["DEDUCTIBLE_1"]!=null && CoverageDetails["DEDUCTIBLE_1"].ToString()!="")
									if(CoverageDetails["DEDUCTIBLE_1"].ToString() =="0" || CoverageDetails["DEDUCTIBLE_1"].ToString() =="" || CoverageDetails["DEDUCTIBLE_1"].ToString() =="0.00" || CoverageDetails["DEDUCTIBLE_1"].ToString() =="$0" || CoverageDetails["DEDUCTIBLE_1"].ToString() =="$0.00")
										AutoElementAcord90.InnerXml +="<UNINSUREDMOTORISTSPDAFTER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</UNINSUREDMOTORISTSPDAFTER>";
									else
										AutoElementAcord90.InnerXml +="<UNINSUREDMOTORISTSPDAFTER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</UNINSUREDMOTORISTSPDAFTER>";
						
									//if(htpremium.Contains("UIMPD"))
									{
										//AutoElementAcord90.InnerXml +="<UMPD_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["UIMPD"].ToString() + ".00")+"</UMPD_PREMIUM>";
										AutoElementAcord90.InnerXml +="<UMPD_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempVehicle,CovCode))+"</UMPD_PREMIUM>";
										//dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
									}

									break;
								case "UNCSL":
									AutoElementAcord90.InnerXml +="<UNDERINSUREDMOTORISTSAFTER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</UNDERINSUREDMOTORISTSAFTER>";
									//if(htpremium.Contains("UIM"))
									{
										//AutoElementAcord90.InnerXml +="<UIM_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["UIM"].ToString() + ".00")+"</UIM_PREMIUM>";
										AutoElementAcord90.InnerXml +="<UIM_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempVehicle,CovCode))+"</UIM_PREMIUM>";
										//dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
									}
									break;
								case "UNDSP":
									AutoElementAcord90.InnerXml +="<UNDERINSUREDMOTORISTSBEFORE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</UNDERINSUREDMOTORISTSBEFORE>";
									AutoElementAcord90.InnerXml +="<UNDERINSUREDMOTORISTSAFTER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_2"].ToString())+"</UNDERINSUREDMOTORISTSAFTER>";

									//if(htpremium.Contains("UIM"))
									{
										//AutoElementAcord90.InnerXml +="<UIM_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["UIM"].ToString() + ".00")+"</UIM_PREMIUM>";
										AutoElementAcord90.InnerXml +="<UIM_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempVehicle,CovCode))+"</UIM_PREMIUM>";
										//dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
									}
									break;											
								case "PIP":
									AutoElementAcord90.InnerXml +="<PIP_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</PIP_DEDUCTIBLE>";								
									if(CoverageDetails["LIMIT_ID"].ToString() == "686")
										AutoElementAcord90.InnerXml +="<LIMIT_ID1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_ID"].ToString())+"</LIMIT_ID1>";								
									else if(CoverageDetails["LIMIT_ID"].ToString() == "687")
										AutoElementAcord90.InnerXml +="<LIMIT_ID2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_ID"].ToString())+"</LIMIT_ID2>";								
									else if(CoverageDetails["LIMIT_ID"].ToString() == "688")
									{
										AutoElementAcord90.InnerXml +="<LIMIT_ID1 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("686")+"</LIMIT_ID1>";								
										AutoElementAcord90.InnerXml +="<LIMIT_ID2 " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("687")+"</LIMIT_ID2>";								
									}
									strHealthCare=RemoveJunkXmlCharacters(CoverageDetails["add_information"].ToString());
									//if(htpremium.Contains("PIP"))
									{
										//AutoElementAcord90.InnerXml +="<PIP_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PIP"].ToString() + ".00")+"</PIP_PREMIUM>";
										AutoElementAcord90.InnerXml +="<PIP_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempVehicle,CovCode))+"</PIP_PREMIUM>";
										//dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
									}
									break;							
								case "PPI":
									//if(htpremium.Contains("PPI"))
									{
										//AutoElementAcord90.InnerXml +="<PP_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PPI"].ToString() + ".00")+"</PP_PREMIUM>";
										AutoElementAcord90.InnerXml +="<PP_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempVehicle,CovCode))+"</PP_PREMIUM>";
										//dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
									}
									break;							
								case "OTC":								
									AutoElementAcord90.InnerXml +="<COMPREHENSIVE_NO " + fieldType +"=\""+ fieldTypeText +"\">"+ strVehNo +"</COMPREHENSIVE_NO>";
									AutoElementAcord90.InnerXml +="<COMPREHENSIVE_DED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</COMPREHENSIVE_DED>";
									//if(htpremium.Contains("COMP"))
									{
										//AutoElementAcord90.InnerXml +="<COMP_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["COMP"].ToString() + ".00")+"</COMP_PREMIUM>";
										AutoElementAcord90.InnerXml +="<COMP_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempVehicle,CovCode))+"</COMP_PREMIUM>";
										//dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
									}
									break;
								case "COMP":
									AutoElementAcord90.InnerXml +="<COMPREHENSIVE_NO " + fieldType +"=\""+ fieldTypeText +"\">"+ strVehNo +"</COMPREHENSIVE_NO>";
									AutoElementAcord90.InnerXml +="<COMPREHENSIVE_DED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</COMPREHENSIVE_DED>";
									//if(htpremium.Contains("COMP"))
									{
										//AutoElementAcord90.InnerXml +="<COMP_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["COMP"].ToString() + ".00")+"</COMP_PREMIUM>";
										AutoElementAcord90.InnerXml +="<COMP_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempVehicle,CovCode))+"</COMP_PREMIUM>";
										//dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
									}
 
									break;
								case "COLL":
									if(CoverageDetails["LIMIT_id"]!=null && CoverageDetails["LIMIT_id"].ToString()!="" ) 
									{
//										AutoElementAcord90.InnerXml +="<LC_APPLICABLE " + fieldType +"=\""+ fieldTypeText +"\">"+ CoverageDetails["LIMIT_id"].ToString() +"</LC_APPLICABLE>";										
									}
									
									if(stCode.Equals(STATE_MICHIGAN ))
									{
										if(CoverageDetails["DEDUCTIBLE_1"]!=null && CoverageDetails["DEDUCTIBLE_1"].ToString()!="" ) 
										{
											if(CoverageDetails["DEDUCTIBLE_1"].ToString().Trim() == "0.00")
												coll_applicable = 0;
									//		AutoElementAcord90.InnerXml +="<LC_APPLICABLE " + fieldType +"=\""+ fieldTypeText +"\">"+ coll_applicable.ToString() +"</LC_APPLICABLE>";
										}
										if(CoverageDetails["DEDUCTIBLE1_AMOUNT_TEXT"]!=null && CoverageDetails["DEDUCTIBLE1_AMOUNT_TEXT"].ToString()!="" ) 
										{
											if(CoverageDetails["DEDUCTIBLE1_AMOUNT_TEXT"].ToString().Equals("Regular")  )
											{
												AutoElementAcord90.InnerXml +="<COLLISION_NO " + fieldType +"=\""+ fieldTypeText +"\">"+ strVehNo +"</COLLISION_NO>";
												AutoElementAcord90.InnerXml +="<COLLISION_DED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</COLLISION_DED>";
												//if(htpremium.Contains("COLL"))
												{
													//AutoElementAcord90.InnerXml +="<COL_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["COLL"].ToString() + ".00")+"</COL_PREMIUM>";
													AutoElementAcord90.InnerXml +="<COL_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempVehicle,CovCode))+"</COL_PREMIUM>";
													//dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
												}
											}
											else if(CoverageDetails["DEDUCTIBLE1_AMOUNT_TEXT"].ToString().Equals("Broad")  )
											{
												AutoElementAcord90.InnerXml +="<COMPREHENSIVE_NO " + fieldType +"=\""+ fieldTypeText +"\">"+ strVehNo +"</COMPREHENSIVE_NO>";
												AutoElementAcord90.InnerXml +="<BRD_COLLISION_DED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</BRD_COLLISION_DED>";
												//if(htpremium.Contains("COLL"))
												{
													//AutoElementAcord90.InnerXml +="<BC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["COLL"].ToString() + ".00")+"</BC_PREMIUM>";
													AutoElementAcord90.InnerXml +="<BC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempVehicle,CovCode))+"</BC_PREMIUM>";
													//dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
												}
											}
											else if(CoverageDetails["LIMIT_id"]!=null && CoverageDetails["LIMIT_id"].ToString()!="" ) 
											{
												if(CoverageDetails["LIMIT_id"].ToString().Equals("1"))
												{
													//if(htpremium.Contains("COLL"))
													{
														//AutoElementAcord90.InnerXml +="<LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["COLL"].ToString() + ".00")+"</LC_PREMIUM>";
														AutoElementAcord90.InnerXml +="<LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempVehicle,CovCode))+"</LC_PREMIUM>";
														//dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
													}
												}
											}
										}
									}
									else if(stCode.Equals(STATE_INDIANA) )
									{
										if(CoverageDetails["DEDUCTIBLE_1"]!=null && CoverageDetails["DEDUCTIBLE_1"].ToString()!="" ) 
										{
											AutoElementAcord90.InnerXml +="<COLLISION_NO " + fieldType +"=\""+ fieldTypeText +"\">"+ strVehNo +"</COLLISION_NO>";
											AutoElementAcord90.InnerXml +="<COLLISION_DED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</COLLISION_DED>";
										}
										//if(htpremium.Contains("COLL"))
										{
											//AutoElementAcord90.InnerXml +="<COL_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["COLL"].ToString() + ".00")+"</COL_PREMIUM>";
											AutoElementAcord90.InnerXml +="<COL_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempVehicle,CovCode))+"</COL_PREMIUM>";
											//dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
										}
									}
									strregCollDed = GetIntFormat(CoverageDetails["DEDUCTIBLE_1"].ToString());
									break;
								case "ROAD":
									AutoElementAcord90.InnerXml +="<TOWING_NO " + fieldType +"=\""+ fieldTypeText +"\">"+ strVehNo +"</TOWING_NO>";
									AutoElementAcord90.InnerXml +="<TENGLABR_DED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</TENGLABR_DED>";
						
									//if(htpremium.Contains("RD_SRVC"))
									{
										//AutoElementAcord90.InnerXml +="<TL_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RD_SRVC"].ToString() + ".00")+"</TL_PREMIUM>";
										AutoElementAcord90.InnerXml +="<TL_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempVehicle,CovCode))+"</TL_PREMIUM>";
										//dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
									}
									break;
						
								case "RREIM":
									AutoElementAcord90.InnerXml +="<TRANS_NO " + fieldType +"=\""+ fieldTypeText +"\">"+ strVehNo +"</TRANS_NO>";
									AutoElementAcord90.InnerXml +="<TE_RR_LIMITBEFORE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</TE_RR_LIMITBEFORE>";
									AutoElementAcord90.InnerXml +="<TE_RR_LIMITAFTER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_2"].ToString())+"</TE_RR_LIMITAFTER>";
									//if(htpremium.Contains("RNT_RMBRS"))
									{
										//AutoElementAcord90.InnerXml +="<TERR_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RNT_RMBRS"].ToString() + ".00")+"</TERR_PREMIUM>";
										AutoElementAcord90.InnerXml +="<TERR_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempVehicle,CovCode))+"</TERR_PREMIUM>";
										//dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
									}
									break;
								case "LPD":
									//if(htpremium.Contains("M_TRT"))
									{
										//AutoElementAcord90.InnerXml +="<LPD_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["M_TRT"].ToString() + ".00")+"</LPD_PREMIUM>";
										AutoElementAcord90.InnerXml +="<LPD_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempVehicle,CovCode))+"</LPD_PREMIUM>";
										//dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
									}

									break;					
							
							}
							#endregion


							if(sumTtl!=0)
							{
								if(sumTtl.ToString().IndexOf(".")>0)
									AutoElementAcord90.InnerXml +="<TOTAL_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+ (sumTtl)+"</TOTAL_PREMIUM>";
								else
									AutoElementAcord90.InnerXml +="<TOTAL_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+ (sumTtl+".00")+"</TOTAL_PREMIUM>";
									AutoElementAcord90.InnerXml +="<VEHICLENUMBERCOV " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(AutoDetail["INSURED_VEH_NUMBER"].ToString())+"</VEHICLENUMBERCOV>";
							}							
							if((AutoCtr == 0) && dbEstTotal != 0)
							{
								if(dbEstTotal.ToString().IndexOf(".")>0)
								AutoElementAcord90.InnerXml += "<ESTIMATED_TOTAL " + fieldType +"=\""+ fieldTypeText +"\">"+ (dbEstTotal) +"</ESTIMATED_TOTAL>";		
								else
								AutoElementAcord90.InnerXml += "<ESTIMATED_TOTAL " + fieldType +"=\""+ fieldTypeText +"\">"+ (dbEstTotal.ToString()+".00") +"</ESTIMATED_TOTAL>";		
							}
							balance_due = dbEstTotal - recv_prem;
							if((AutoCtr == 0) && balance_due != 0)
							{
								if(balance_due.ToString().IndexOf(".")>0)
								AutoElementAcord90.InnerXml += "<BALANCE " + fieldType +"=\""+ fieldTypeText +"\">"+ (balance_due) +"</BALANCE>";		
								else
								AutoElementAcord90.InnerXml += "<BALANCE " + fieldType +"=\""+ fieldTypeText +"\">"+ (balance_due.ToString() +".00") +"</BALANCE>";		
							}
						}
						#endregion
					}
					#endregion
	
					#region Endorsements
					XmlElement DecPageAUTOEndmts;
					DecPageAUTOEndmts= AcordPDFXML.CreateElement("ENDORSEMENTS");		

					XmlElement SupplementAUTOEndmts;
					SupplementAUTOEndmts= AcordPDFXML.CreateElement("ENDORSEMENTS");

					#region Declaration Page Auto Endorsements
					if (gStrPdfFor==PDFForDecPage )
					{
						AutoElementDecPage.AppendChild(DecPageAUTOEndmts);
						DecPageAUTOEndmts.SetAttribute(fieldType,fieldTypeMultiple);
						DecPageAUTOEndmts.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAUTOEND"));
						DecPageAUTOEndmts.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAUTOEND"));
						DecPageAUTOEndmts.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEAUTOENDEXTN"));
						DecPageAUTOEndmts.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAUTOENDEXTN"));
					}
					#endregion

					#region Supplemental Page Auto Endorsements
					if (gStrPdfFor==PDFForAcord )
					{
						AutoElementSuppliment.AppendChild(SupplementAUTOEndmts);
						SupplementAUTOEndmts.SetAttribute(fieldType,fieldTypeMultiple);
						SupplementAUTOEndmts.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTAUTOEND"));
						SupplementAUTOEndmts.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTAUTOEND"));
						SupplementAUTOEndmts.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTAUTOENDEXTN"));
						SupplementAUTOEndmts.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTAUTOENDEXTN"));
					}
					#endregion
					
					int endCtr=0,decEndCtr=0;

					if(gStrPdfFor == PDFForDecPage)
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
					
					foreach(DataRow CoverageDetails in DSTempVehicle.Tables[0].Rows)
					{		
						string CovCode = CoverageDetails["COV_CODE"].ToString();
						string CovLimit = "";

						if((CoverageDetails["COVERAGE_TYPE"].ToString().ToUpper()=="RL") && (CovCode!="SLL" && CovCode!="UNCSL" && CovCode!="PUNCS" && CovCode!="BISPL" && CovCode!="PD" && CovCode!="UNDSP" && CovCode!="PUMSP" && CovCode!="PIP" && CovCode!="CAB91"  && CovCode!="PPI" && CovCode!="UMPD" && CovCode!="RRUM" && CovCode!="MP" && CovCode!="ENO" && CovCode!="EP95" && CovCode!="A10"))
						{
							// formating for Coverage Limit
							CovLimit = CoverageDetails["LIMIT_1"].ToString();
							if(CoverageDetails["LIMIT_2"].ToString() != "" && CovLimit != "")
							{
								CovLimit=GetIntFormat(CovLimit);
								CovLimit += "/" + GetIntFormat(CoverageDetails["LIMIT_2"].ToString());										
							}
							else
							{
								CovLimit += CoverageDetails["LIMIT_2"].ToString();
								CovLimit=GetIntFormat(CovLimit);
							}
							// Formating for Deductible
							strDec = CoverageDetails["DEDUCTIBLE_1"].ToString();
							strDec = GetIntFormat(strDec);
							if(CoverageDetails["deductible1_amount_text"].ToString() != "" && CoverageDetails["deductible1_amount_text"].ToString() != null)
							{
								strDectxt = CoverageDetails["deductible1_amount_text"].ToString();
								strDec = strDec + " " + strDectxt;
							}
									
							// formating for Premium
							if(gStrtemp == "temp")
							{
								strPrem=GetPremiumBeforeCommit(DSTempVehicle,CovCode,htpremium);
								if(strPrem !="")
								{
									strPrem = "$" + strPrem;
								}
							}
							else
							{
								strPrem=GetPremium(DSTempVehicle,CovCode);
								if(strPrem !="")
								{
									strPrem = "$" + strPrem;
								}
							}
								#region DecPage
							if (gStrPdfFor==PDFForDecPage )
							{
							//	if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//	{
									XmlElement DecpageAutoEndmtElement;
								// Append in Coverage level node so that endorsement additinal page will be same as coverage additinal page
									DecpageAutoEndmtElement = AcordPDFXML.CreateElement("ENDORSEMENTSINFO");
									if(lcCtr >= 9)
									{
										LCRootElement.AppendChild(DecpageAutoEndmtElement);
									}
									else
									{
										DecPageAUTOEndmts.AppendChild(DecpageAutoEndmtElement);
									}
									DecpageAutoEndmtElement.SetAttribute(fieldType,fieldTypeNormal);
									DecpageAutoEndmtElement.SetAttribute(id,decEndCtr.ToString());
								
								// pageINFO
								if(lcCtr <= 8 && decEndCtr >=8 && decEndCtr < 9)
								{
									intpageno++;
								}
								DecpageAutoEndmtElement.InnerXml +="<AT_LC_AUTODESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(AutoDetail["INSURED_VEH_NUMBER"].ToString()) + "  "  + RemoveJunkXmlCharacters(AutoDetail["VEHICLE_YEAR"].ToString()) +"/"+ RemoveJunkXmlCharacters(AutoDetail["MAKE"].ToString()) +"/" + RemoveJunkXmlCharacters(AutoDetail["MODEL"].ToString()) + "/" + RemoveJunkXmlCharacters(AutoDetail["VIN"].ToString()) +"     "+ RemoveJunkXmlCharacters(AutoDetail["BODY_TYPE"].ToString()) +"</AT_LC_AUTODESCRIPTION>";
								DecpageAutoEndmtElement.InnerXml +="<AT_LC_PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " +  "of" + " " +  inttotalpage + "</AT_LC_PAGE_NO>";
																								
								//									// mapping for endorsement coverages
								//									if(CovCode == "IFGHO" || CovCode == "LPD" || CovCode == "LLGC" || CovCode == "EBCE" || CovCode == "DD25" || CovCode == "AMC49" || CovCode == "CC49" || CovCode == "MHT22" || CovCode == "PAP6" || CovCode == "TE90" || CovCode == "TE92" || CovCode == "TAP7" || CovCode == "SPA8")
								//										DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">Included</AT_LC_LIMIT>";
								#region ADDITIONAL FORMS AND COVERAGES 
								switch(CoverageDetails["COV_CODE"].ToString())
								{
										//Limited Property Damage Liability (Mini-Tort)
									case "LPD":
									{
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LC_LIMIT>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
										DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
										decEndCtr++;
									}
										break;
										//Other Than Collision (Comprehensive) Michigan
									case "COMP":
									{
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_LC_DEDUCTIBLE>";
										DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
										decEndCtr++;
									}
										break;
										//Other Than Collision (Comprehensive) Indiana
									case "OTC":
									{
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_LC_DEDUCTIBLE>";
										DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
										decEndCtr++;
									}
										break;
										//Collision
									case "COLL":
									{
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_LC_DEDUCTIBLE>";
										DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
										// Collision deductible for regular collision a-68
										strregCollDed = strDec;
										decEndCtr++;
									}
										break;
										//Road Service 
									case "ROAD":
									{
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LC_LIMIT>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
										DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
										decEndCtr++;
									}
										break;
										//Rental Reimbursement (A-89) 
									case "RREIM":
									{
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LC_LIMIT>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
										DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
										decEndCtr++;
									}
										break;
										//Loan / Lease Gap Coverage (A-11)  
									case "LLGC":
									{
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
										DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
										decEndCtr++;
									}
										break;
										//Sound Reproducing - Tapes (A-29)  
									case "SORPE":
									{
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LC_LIMIT>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
										DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
										decEndCtr++;
									}
										break;
										//Customizing Equipment (A-14)  
									case "EBCE":
									{
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
										if(strPrem == "" || strPrem =="0" || strPrem =="0.00")
										{
											DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
										}
										else
										{
											DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
										}
										decEndCtr++;
									}
										break;
										//A-15 Miscellaneous Extra Equipment – Comprehensive  and A-16 Miscellaneous Extra Equipment – Comprehensive  
									case "EECOMP":
									{
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetIntFormat(strtotLimit.ToString()))+"</AT_LC_LIMIT>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_LC_DEDUCTIBLE>";
										DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
										decEndCtr++;
									}
										break;
										//A-15 Miscellaneous Extra Equipment – Collision   and A-16 Miscellaneous Extra Equipment – Collision
									case "EECOLL":
									{
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetIntFormat(strtotLimit.ToString()))+"</AT_LC_LIMIT>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_LC_DEDUCTIBLE>";
										DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
										decEndCtr++;
									}
										break;
										//A-68 Regular Collision Coverage  Deductible work left 
									case "RCC68":
									{
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strregCollDed)+"</AT_LC_DEDUCTIBLE>";
										DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
										decEndCtr++;
									}
										break;
										//A-31 Sound Receiving & Transmitting Equipment   
									case "SRTE":
									{
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LC_LIMIT>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
										DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
										decEndCtr++;
									}
										break;
										//A-90 Transportation Expense    
									case "TE90":
									{
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
										DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
										decEndCtr++;
									}
										break;
										//A-64 Snow-plowing   and A-8 
									case "SPA8":
									{
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_LC_DEDUCTIBLE>";
										DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
										decEndCtr++;
									}
										break;
										//A-45 Stated Amount       
									case "SA44":
									{
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
										DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
										decEndCtr++;
									}
										break;
										//A-46 Classic Car          
									case "CC49":
									{
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
										DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
										decEndCtr++;
									}
										break;
										//A-49 Antique Motor Car          
									case "AMC49":
									{
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
										DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
										decEndCtr++;
									}
										break;
										//A-25 Diminishing Deductible           
									case "DD25":
									{
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
										DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
										decEndCtr++;
									}
										break;
										//A-22 Motor Homes, Truck or Van Campers & Travel Trailers            
									case "MHT22":
									{
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
										DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
										decEndCtr++;
									}
										break;
										//TB-6 Personal Auto Policy            
									case "PAP6":
									{
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
										DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
										decEndCtr++;
									}
										break;
										//TB-7 Trailblazer Policy             
									case "TAP7":
									{
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
										DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
										DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
										decEndCtr++;
									}
										break;
									default: 
									{
										if(CoverageDetails["COVERAGE_TYPE"].ToString().ToUpper()=="RL")
										{
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
											DecpageAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";

											if(CovLimit =="0" || CovLimit =="" || CovLimit =="0.00" || CovLimit =="$0" || CovLimit =="$0.00")
												DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
											else
												DecpageAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LC_LIMIT>";
											if(strDec =="0" || strDec =="" || strDec =="0.00" || strDec =="$0" || strDec =="$0.00")
												DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
											else
												DecpageAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_LC_DEDUCTIBLE>";
											if(strPrem =="0" || strPrem =="" || strPrem =="0.00" || strPrem =="$0" || strPrem =="$0.00")
												DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
											else
												DecpageAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
											decEndCtr++;
										}
									}
										break;
								}
								#endregion
								//}									 
							}
							#endregion
						}
					
						#region Supplement Page Accord
						if (gStrPdfFor==PDFForAcord )
						{
							if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							{
								if(CovCode!="SLL" && CovCode!="UNCSL" && CovCode!="PUNCS" && CovCode!="BISPL" && CovCode!="PD" && CovCode!="UNDSP" && CovCode!="PUMSP" && CovCode!="PIP" && CovCode!="UMPD" && CovCode!="MP" && CovCode!="OTC" && CovCode!="COMP" && CovCode!="COLL" && CovCode!="ROAD" && CovCode!="RREIM" && CovCode!="LPD" )
								{
									XmlElement SupplementAutoEndmtElement;
									SupplementAutoEndmtElement = AcordPDFXML.CreateElement("ENDORSEMENTSINFO");
									SupplementAUTOEndmts.AppendChild(SupplementAutoEndmtElement);
									SupplementAutoEndmtElement.SetAttribute(fieldType,fieldTypeNormal);
									SupplementAutoEndmtElement.SetAttribute(id,endCtr.ToString());
									
									if(endCtr >= 4)
									{
										SupplementAutoEndmtElement.InnerXml +="<AT_LC_FORMNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</AT_LC_FORMNO>";
										SupplementAutoEndmtElement.InnerXml +="<AT_LC_EDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</AT_LC_EDITIONDATE>";
									}
									SupplementAutoEndmtElement.InnerXml +="<AT_LC_DESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</AT_LC_DESCRIPTION>";
									endCtr++;
									switch(CoverageDetails["COV_CODE"].ToString())
									{
											//Limited Property Damage Liability (Mini-Tort)
										case "TAP7":
										{
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
											SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
											//endCtr++;		
										}
											break;
										case "PAP6":
										{
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
											SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
											//endCtr++;		
										}
											break;
										case "MHT22":
										{
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
											SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
											//endCtr++;		
										}
											break;
											//A-25 Diminishing Deductible           
										case "DD25":
										{
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
											SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
											//endCtr++;		
										}
											break;
											//A-49 Antique Motor Car          
										case "AMC49":
										{
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
											SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
											//endCtr++;		
										}
											break;
											//A-46 Classic Car   
										case "CC49":
										{
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
											SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
											//endCtr++;		
										}
											break;
											//A-45 Stated Amount       
										case "SA44":
										{
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
											SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
											//endCtr++;		
										}
											break;
											//A-64 Snow-plowing   and A-8 
										case "SPA8":
										{
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_LC_DEDUCTIBLE>";
											SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
											//endCtr++;		
										}
											break;
											//A-90 Transportation Expense    
										case "TE90":
										{
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_LC_DEDUCTIBLE>";
											SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
											//endCtr++;		
										}
											break;
											//A-31 Sound Receiving & Transmitting Equipment   
										case "SRTE":
										{
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LC_LIMIT>";
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
											SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
										//endCtr++;		
										}
											break;
											//A-68 Regular Collision Coverage  Deductible work left 
										case "RCC68":
										{ 
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strregCollDed)+"</AT_LC_DEDUCTIBLE>";
											SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
											//endCtr++;		
										}
											break;
											//A-15 Miscellaneous Extra Equipment – Collision   and A-16 Miscellaneous Extra Equipment – Collision
										case "EECOLL":
										{
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_LC_DEDUCTIBLE>";
											SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
											//endCtr++;		
										}
											break;
											//A-15 Miscellaneous Extra Equipment – Comprehensive  and A-16 Miscellaneous Extra Equipment – Comprehensive  
										case "EECOMP":
										{
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_LC_DEDUCTIBLE>";
											SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
											//endCtr++;		
										}
											break;
											//Customizing Equipment (A-14)  
										case "EBCE":
										{
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
											if(strPrem!="0" && strPrem!="0.00")
											{
												SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
											}
											else
											{
												SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
											}
											//endCtr++;		
										}
											break;
											//Sound Reproducing - Tapes (A-29)  
										case "SORPE":
										{
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LC_LIMIT>";
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
											SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
											//endCtr++;		
										}
											break;
											//Loan / Lease Gap Coverage (A-11)  
										case "LLGC":
										{
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
											SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
											//endCtr++;		
										}
											break;
											//Road Service 
										case "ROAD":
										{
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LC_LIMIT>";
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
											SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
											//endCtr++;	
										}
											break;
											//Collision
										case "COLL":
										{
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
											SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
											// Collision deductible for regular collision a-68
											strregCollDed = strDec;
											//endCtr++;	
										}
											break;
											//Other Than Collision (Comprehensive) Indiana
										case "OTC":
										{
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_LC_DEDUCTIBLE>";
											SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
											//endCtr++;
										}
											break;
											//Other Than Collision (Comprehensive) Michigan
										case "COMP":
										{
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_LC_DEDUCTIBLE>";
											SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
											//endCtr++;
										}
											break;
											//Limited Property Damage Liability (Mini-Tort)
										case "LPD":
										{
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
											SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
											SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
											//endCtr++;
										}
											break;
										default: 
										{
											if(CoverageDetails["COVERAGE_TYPE"].ToString().ToUpper()=="RL")
											{
												if(CovLimit =="0" || CovLimit =="" || CovLimit =="0.00" || CovLimit =="$0" || CovLimit =="$0.00")
													SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_LIMIT>";
												else
													SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</AT_LC_LIMIT>";
												if(strDec =="0" || strDec =="" || strDec =="0.00" || strDec =="$0" || strDec =="$0.00")
													SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</AT_LC_DEDUCTIBLE>";
												else
													SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</AT_LC_DEDUCTIBLE>";
												if(strPrem =="0" || strPrem =="" || strPrem =="0.00" || strPrem =="$0" || strPrem =="$0.00")
													SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</AT_LC_PREMIUM>";
												else
													SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</AT_LC_PREMIUM>";
												//endCtr++;
											}
											break;
										}

									}
									
//									if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"].ToString()!="")
//									{
//										SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">$"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</AT_LC_LIMIT>";
//									}
//									else
//									{
//										if(CoverageDetails["LIMIT1_amount_text"]!=null && CoverageDetails["LIMIT1_amount_text"].ToString()!="" && CoverageDetails["LIMIT1_amount_text"].ToString()!="Limited"  && CoverageDetails["LIMIT1_amount_text"].ToString()!="Loan"  && CoverageDetails["LIMIT1_amount_text"].ToString()!="Lease")
//										{
//											SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">$" + RemoveJunkXmlCharacters(CoverageDetails["LIMIT1_amount_text"].ToString())+"</AT_LC_LIMIT>"; 
//										}
//										else
//										{
//											SupplementAutoEndmtElement.InnerXml +="<AT_LC_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">" + RemoveJunkXmlCharacters(CoverageDetails["LIMIT1_amount_text"].ToString())+"</AT_LC_LIMIT>"; 
//										}
//									}
//
//									if(CoverageDetails["DEDUCTIBLE_1"]!=null && CoverageDetails["DEDUCTIBLE_1"].ToString()!="")
//									{
//										SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">$"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</AT_LC_DEDUCTIBLE>";
//
//									}
//									else
//									{
//										if(CoverageDetails["DEDUCTIBLE1_amount_text"]!=null && CoverageDetails["DEDUCTIBLE1_amount_text"].ToString()!="")
//											SupplementAutoEndmtElement.InnerXml +="<AT_LC_DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">$"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE1_amount_text"].ToString())+"</AT_LC_DEDUCTIBLE>";
//									}
//									if(GetPremium(DSTempVehicle,CoverageDetails["COV_CODE"].ToString()) != "" && GetPremium(DSTempVehicle,CoverageDetails["COV_CODE"].ToString()) !="Included" && GetPremium(DSTempVehicle,CoverageDetails["COV_CODE"].ToString()) !="0" && GetPremium(DSTempVehicle,CoverageDetails["COV_CODE"].ToString()) !="0.00")
//										SupplementAutoEndmtElement.InnerXml += "<AT_LC_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">$"+RemoveJunkXmlCharacters(GetPremium(DSTempVehicle,CoverageDetails["COV_CODE"].ToString()))+"</AT_LC_PREMIUM>";
//									endCtr++;		
								 }
								}									 
							}
							#endregion

					
						}
					#region OPERATOR
					if(gStrPdfFor == PDFForDecPage)
					{
						XmlElement DecPageAutoOpr;
						DecPageAutoOpr = AcordPDFXML.CreateElement("OPERATOR");			

						int  intOpr=0;
						#region Additional Root Element int Dec Page
						AutoElementDecPage.AppendChild(DecPageAutoOpr);
						DecPageAutoOpr.SetAttribute(fieldType,fieldTypeMultiple);
						DecPageAutoOpr.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOPERATOR"));
						DecPageAutoOpr.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOPERATOR"));
						DecPageAutoOpr.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEOPERATOREXTN"));
						DecPageAutoOpr.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOPERATOREXTN"));
						#endregion

						objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						objDataWrapper.AddParameter("@POLID",gStrPolicyId);
						objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						objDataWrapper.AddParameter("@VEHICLEID",AutoDetail["Vehicle_ID"]);
						DataSet DSTempAutoAssgnOpr = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_AssgnDriv");
						objDataWrapper.ClearParameteres();
						//DataSet DSTempAutoAssgnOpr = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_AssgnDriv " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",'" + gStrCalledFrom + "',"  + AutoDetail["Vehicle_ID"]);
						//while(operCounttemp < DSTempAutoOpr.Tables[0].Rows.Count)
						if(DSTempAutoAssgnOpr.Tables.Count > 1 && DSTempAutoAssgnOpr.Tables[1].Rows.Count > 0)
						{
							foreach(DataRow OprDetails in DSTempAutoAssgnOpr.Tables[1].Rows)
							{
								XmlElement DecPageAutoOprElement;
								DecPageAutoOprElement = AcordPDFXML.CreateElement("OPERATORINFO");

								#region Operator Dec Page
								DecPageAutoOpr.AppendChild(DecPageAutoOprElement);
								DecPageAutoOprElement.SetAttribute(fieldType,fieldTypeNormal);
								DecPageAutoOprElement.SetAttribute(id,intOpr.ToString());
								
								//PAGE INFO
								if(intOpr >=4 && intOpr <5)
								{
									intpageno++;
								}
								DecPageAutoOprElement.InnerXml +="<AUTO_AUTODESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(AutoDetail["INSURED_VEH_NUMBER"].ToString()) + "  "  + RemoveJunkXmlCharacters(AutoDetail["VEHICLE_YEAR"].ToString()) +"/"+ RemoveJunkXmlCharacters(AutoDetail["MAKE"].ToString()) +"/" + RemoveJunkXmlCharacters(AutoDetail["MODEL"].ToString()) + "/" + RemoveJunkXmlCharacters(AutoDetail["VIN"].ToString()) +"     "+ RemoveJunkXmlCharacters(AutoDetail["BODY_TYPE"].ToString()) +"</AUTO_AUTODESCRIPTION>";
								DecPageAutoOprElement.InnerXml +="<AUTO_PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " +  "of" + " " +  inttotalpage +  "</AUTO_PAGE_NO>";
								DecPageAutoOprElement.InnerXml +="<AUTO_OPERATORNAME " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(OprDetails["DRIVER_NAME"].ToString()) +"</AUTO_OPERATORNAME>"; 
								DecPageAutoOprElement.InnerXml +="<AUTO_OPERATOR_LICENSE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OprDetails["DRIVER_DRIV_LIC"].ToString()) +"</AUTO_OPERATOR_LICENSE>"; 
								DecPageAutoOprElement.InnerXml +="<AUTO_OPERATOR_DOB " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OprDetails["DRIVER_DOB"].ToString())+"</AUTO_OPERATOR_DOB>"; 
								DecPageAutoOprElement.InnerXml +="<AUTO_OPERATOR_RATED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OprDetails["ASSIGNED_ASS"].ToString())+"</AUTO_OPERATOR_RATED>"; 
								#endregion

								//operCounttemp++;
								intOpr++;
							}
							//							if(intOpr >= 7)
							//								break;
						}
						intOpr=0;
					}
					#endregion
								
					#endregion			

					#region Addl Interests
					XmlElement DecPageAutoAddlInt;
					DecPageAutoAddlInt = AcordPDFXML.CreateElement("ADDITIONALINT");			
					int  AddInt=0;
					#region Additional Root Element int Dec Page
					if (gStrPdfFor==PDFForDecPage)
					{
						AutoElementDecPage.AppendChild(DecPageAutoAddlInt);
						DecPageAutoAddlInt.SetAttribute(fieldType,fieldTypeMultiple);
						DecPageAutoAddlInt.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAUTOADDLINT"));
						DecPageAutoAddlInt.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAUTOADDLINT"));
						DecPageAutoAddlInt.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEAUTOADDLINTEXTN"));
						DecPageAutoAddlInt.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAUTOADDLINTEXTN"));
					}
					#endregion
				
					
//					objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//					objDataWrapper.AddParameter("@POLID",gStrPolicyId);
//					objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//					objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//					objDataWrapper.AddParameter("@VEHICLEID",AutoDetail["Vehicle_ID"]);
//					DataSet DSTempAutoAddInt = objDataWrapper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS");
//					objDataWrapper.ClearParameteres();
					//DataSet DSTempAutoAddInt = objDataWrapper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + "," + AutoDetail["Vehicle_ID"] + ",'" + gStrCalledFrom + "'");
					foreach(DataRow AddlIntDetails in DSTempAddIntrst.Tables[0].Rows)
					{
						XmlElement DecPageAutoAddlIntElement;
						DecPageAutoAddlIntElement = AcordPDFXML.CreateElement("ADDITIONALINTINFO");
						if(AddlIntDetails["VEHICLE_ID"]==AutoDetail["Vehicle_ID"])
						{
							#region AddlInt Dec Page
							if (gStrPdfFor==PDFForDecPage )
							{
								DecPageAutoAddlInt.AppendChild(DecPageAutoAddlIntElement);
								DecPageAutoAddlIntElement.SetAttribute(fieldType,fieldTypeNormal);
								DecPageAutoAddlIntElement.SetAttribute(id,AddInt.ToString());
								if(AddInt > 1 && AddInt <3)
								{
									intpageno++;
								}
								DecPageAutoAddlIntElement.InnerXml +="<AUTO_AUTODESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(AutoDetail["INSURED_VEH_NUMBER"].ToString()) + "  "  + RemoveJunkXmlCharacters(AutoDetail["VEHICLE_YEAR"].ToString()) +"/"+ RemoveJunkXmlCharacters(AutoDetail["MAKE"].ToString()) +"/" + RemoveJunkXmlCharacters(AutoDetail["MODEL"].ToString()) + "/" + RemoveJunkXmlCharacters(AutoDetail["VIN"].ToString()) +"     "+ RemoveJunkXmlCharacters(AutoDetail["BODY_TYPE"].ToString()) +"</AUTO_AUTODESCRIPTION>";
								DecPageAutoAddlIntElement.InnerXml +="<AUTO_PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " +  "of" + " " + inttotalpage + "</AUTO_PAGE_NO>";
								DecPageAutoAddlIntElement.InnerXml +="<AUTO_SERIALNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["RANK"].ToString())+"</AUTO_SERIALNO>"; 
								DecPageAutoAddlIntElement.InnerXml +="<AUTO_NAMEADDRESS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["HOLDER_NAME"].ToString()) +","+ ' '+ RemoveJunkXmlCharacters(AddlIntDetails["ADDRESS"].ToString())+", " + RemoveJunkXmlCharacters(AddlIntDetails["CITYSTATEZIP"].ToString())+"</AUTO_NAMEADDRESS>"; 
								DecPageAutoAddlIntElement.InnerXml +="<AUTO_NATUREOFINTEREST " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["ADDLINTNAME"].ToString())+"</AUTO_NATUREOFINTEREST>"; 
								DecPageAutoAddlIntElement.InnerXml +="<AUTO_LOANNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["LOAN_REF_NUMBER"].ToString())+"</AUTO_LOANNO>"; 
							}
							#endregion					
							AddInt++;
						}
					}
					AddInt=0;
					#endregion

					#region Schedule Equipment
					XmlElement DecPageAutoEqp;
					DecPageAutoEqp = AcordPDFXML.CreateElement("SCHEQUIP");			
					int  intSchCtr=0;
					#region Additional Root Element int Dec Page
					if (gStrPdfFor==PDFForDecPage)
					{
						AutoElementDecPage.AppendChild(DecPageAutoEqp);
						DecPageAutoEqp.SetAttribute(fieldType,fieldTypeMultiple);
						DecPageAutoEqp.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAUTOEQUIPMENT"));
						DecPageAutoEqp.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAUTOEQUIPMENT"));
						DecPageAutoEqp.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEAUTOEQUIPMENTEXTN"));
						DecPageAutoEqp.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAUTOEQUIPMENTEXTN"));
					}
					#endregion
				
//					objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//					objDataWrapper.AddParameter("@POLID",gStrPolicyId);
//					objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//					objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//					objDataWrapper.AddParameter("@VEHICLEID",AutoDetail["Vehicle_ID"]);
//					DataSet DSTempAutoEquip = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_MiscEquipment");
//					objDataWrapper.ClearParameteres();
					//DataSet DSTempAutoEquip = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_MiscEquipment " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",'" + gStrCalledFrom + "',"  + AutoDetail["Vehicle_ID"]);
					// commented to show equipment limit in at coverage level
					//					double strtotLimit=0;
//					foreach(DataRow EquipLimit in DSTempAutoEquip.Tables[0].Rows)
//					{
//						strtotLimit += double.Parse(EquipLimit["ITEM_VALUE"].ToString()); 
//					}
					foreach(DataRow SchDetails in DSTempEquipment.Tables[0].Rows)
					{
						if(SchDetails["VEHICLE_ID"].ToString()==AutoDetail["Vehicle_ID"].ToString())
						{
							XmlElement DecPageAutoEquipElement;
							DecPageAutoEquipElement = AcordPDFXML.CreateElement("SCHEQUIPINFO");

							#region AddlInt Dec Page
							if (gStrPdfFor==PDFForDecPage )
							{
								DecPageAutoEqp.AppendChild(DecPageAutoEquipElement);
								DecPageAutoEquipElement.SetAttribute(fieldType,fieldTypeNormal);
								DecPageAutoEquipElement.SetAttribute(id,intSchCtr.ToString());
							
								//PAGE INFO
								if(intSchCtr >1 && intSchCtr <3)
								{
									intpageno++;
								}
								DecPageAutoEquipElement.InnerXml +=  "<AUTO_EDITION_DATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strcompEditDate)+"</AUTO_EDITION_DATE>"; 
								DecPageAutoEquipElement.InnerXml +=  "<AUTO_SCHEDULEMISCELL_EQUIPMENT_TOTAL_LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetIntFormat(strtotLimit.ToString()))+"</AUTO_SCHEDULEMISCELL_EQUIPMENT_TOTAL_LIMIT>"; 
								DecPageAutoEquipElement.InnerXml +=  "<AUTO_SCHEDULE_EQUIP_TOTAL_PRREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strcomcollPrem)+"</AUTO_SCHEDULE_EQUIP_TOTAL_PRREMIUM>"; 
								DecPageAutoEquipElement.InnerXml +="<AUTO_AUTODESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(AutoDetail["INSURED_VEH_NUMBER"].ToString()) + "  "  + RemoveJunkXmlCharacters(AutoDetail["VEHICLE_YEAR"].ToString()) +"/"+ RemoveJunkXmlCharacters(AutoDetail["MAKE"].ToString()) +"/" + RemoveJunkXmlCharacters(AutoDetail["MODEL"].ToString()) + "/" + RemoveJunkXmlCharacters(AutoDetail["VIN"].ToString()) +"     "+ RemoveJunkXmlCharacters(AutoDetail["BODY_TYPE"].ToString()) +"</AUTO_AUTODESCRIPTION>";
								DecPageAutoEquipElement.InnerXml +="<AUTO_PAGE_NO " + fieldType + "=\"" + fieldTypeText + "\">" + intpageno + " " +  "of" + " " +  inttotalpage + "</AUTO_PAGE_NO>";
								DecPageAutoEquipElement.InnerXml +="<AT_SCHEDULEDNO " + fieldType +"=\""+ fieldTypeText +"\">"+ (intSchCtr+1) +"</AT_SCHEDULEDNO>"; 
								DecPageAutoEquipElement.InnerXml +="<AUTO_SCHEDULEDDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(SchDetails["ITEM_DESCRIPTION"].ToString()) +"</AUTO_SCHEDULEDDESC>"; 
								DecPageAutoEquipElement.InnerXml +="<AUTO_AMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetIntFormat(SchDetails["ITEM_VALUE"].ToString()))+"</AUTO_AMOUNT>"; 
							}
							#endregion
							intSchCtr++;
						}
					}
					intSchCtr=0;
					#endregion

					#region Creating Credit And Surcharge Xml
					// When process not commited 
					if(gStrtemp == "temp")
					{
						if (isRateGenerated)
						{
							// When process not commited
							int CreditSurchRowCounter = 0;
						

							#region Credits
							XmlElement DecPageAutoCredit;
							DecPageAutoCredit = AcordPDFXML.CreateElement("AUTOCREDIT");

							XmlElement SupplementAutoCredit;
							SupplementAutoCredit = AcordPDFXML.CreateElement("AUTOCREDIT");

							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								AutoElementDecPage.AppendChild(DecPageAutoCredit);
								DecPageAutoCredit.SetAttribute(fieldType,fieldTypeMultiple);
								DecPageAutoCredit.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECREDIT"));
								DecPageAutoCredit.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGECREDIT"));
								DecPageAutoCredit.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGECREDITEXTN"));
								DecPageAutoCredit.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGECREDITEXTN"));
								#endregion
							}
							else if (gStrPdfFor == PDFForAcord)
							{
								#region Supplement Page Element
								AutoElementSuppliment.AppendChild(SupplementAutoCredit);
								SupplementAutoCredit.SetAttribute(fieldType,fieldTypeMultiple);
								SupplementAutoCredit.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTCREDIT"));
								SupplementAutoCredit.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTCREDIT"));
								SupplementAutoCredit.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTCREDITEXTN"));
								SupplementAutoCredit.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTCREDITEXTN"));
								#endregion
							}

							htpremium_dis.Clear(); 
							// for discount
						
							foreach (XmlNode CreditNode in GetCredits(AutoDetail["VEHICLE_ID"].ToString()))
							{
								if(!htpremium_dis.Contains(getAttributeValue(CreditNode,"COMPONENT_CODE")))
									htpremium_dis.Add(getAttributeValue(CreditNode,"COMPONENT_CODE"),getAttributeValue(CreditNode,"STEPPREMIUM"));
								else
									continue;
								string strCreditDisc="";
								if (gStrPdfFor == PDFForDecPage)
								{
									// Remove Discount Word from Credit discription
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
								
								
										//credit information
										#region Dec Page
										XmlElement DecPageAutoCreditElement;
										DecPageAutoCreditElement = AcordPDFXML.CreateElement("AUTOCREDITINFO");
										DecPageAutoCredit.AppendChild(DecPageAutoCreditElement);
										DecPageAutoCreditElement.SetAttribute(fieldType,fieldTypeNormal);
										DecPageAutoCreditElement.SetAttribute(id,CreditSurchRowCounter.ToString());
										DecPageAutoCreditElement.InnerXml += "<CREDITDISC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strCreditDisc) +"</CREDITDISC>"; 
										#endregion
									}
								}
								else if (gStrPdfFor == PDFForAcord)
								{
									#region Supplement Page
									XmlElement SupplementAutoCreditElement;
									SupplementAutoCreditElement = AcordPDFXML.CreateElement("AUTOCREDITINFO");
									SupplementAutoCredit.AppendChild(SupplementAutoCreditElement);
									SupplementAutoCreditElement.SetAttribute(fieldType,fieldTypeNormal);
									SupplementAutoCreditElement.SetAttribute(id,CreditSurchRowCounter.ToString());
								
									SupplementAutoCreditElement.InnerXml +="<AUTO_AUTODESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(AutoDetail["INSURED_VEH_NUMBER"].ToString()) + "  "  + RemoveJunkXmlCharacters(AutoDetail["VEHICLE_YEAR"].ToString()) +"/"+ RemoveJunkXmlCharacters(AutoDetail["MAKE"].ToString()) +"/" + RemoveJunkXmlCharacters(AutoDetail["MODEL"].ToString()) + "/" + RemoveJunkXmlCharacters(AutoDetail["VIN"].ToString()) +"     "+ RemoveJunkXmlCharacters(AutoDetail["BODY_TYPE"].ToString()) +"</AUTO_AUTODESCRIPTION>";
									SupplementAutoCreditElement.InnerXml += "<CREDITDISC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</CREDITDISC>"; 
									//SupplementAutoCreditElement.InnerXml += "<CREDIT_AMT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</CREDIT_AMT>"; 
									#endregion
								}
								if(strCreditDisc.IndexOf("Insurance")>0 && gStrPdfFor == PDFForDecPage)
								{
								}
								else
								{
									CreditSurchRowCounter++;
								}
							}

							#endregion

								

							#region Surcharges
							XmlElement DecPageAutoSurch;
							DecPageAutoSurch = AcordPDFXML.CreateElement("AUTOSURCHARGE");

							XmlElement SupplementAutoSurch;
							SupplementAutoSurch = AcordPDFXML.CreateElement("AUTOSURCHARGE");

							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								AutoElementDecPage.AppendChild(DecPageAutoSurch);
								DecPageAutoSurch.SetAttribute(fieldType,fieldTypeMultiple);
								DecPageAutoSurch.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESRCHG"));
								DecPageAutoSurch.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHG"));
								DecPageAutoSurch.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESRCHGEXTN"));
								DecPageAutoSurch.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHGEXTN"));
								#endregion
							}
							else if (gStrPdfFor == PDFForAcord)
							{
								//reset counter value for surcharge
								CreditSurchRowCounter = 0;
								#region Supplement Page Element
								AutoElementSuppliment.AppendChild(SupplementAutoSurch);
								SupplementAutoSurch.SetAttribute(fieldType,fieldTypeMultiple);
								SupplementAutoSurch.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTSRCHG"));
								SupplementAutoSurch.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTSRCHG"));
								SupplementAutoSurch.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTSRCHGEXTN"));
								SupplementAutoSurch.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTSRCHGEXTN"));
								#endregion
							}

							htpremium_sur.Clear(); 
							foreach (XmlNode CreditNode in GetSurcharges(AutoDetail["VEHICLE_ID"].ToString()))
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
									// surcharge information
									#region Dec Page
									XmlElement DecPageAutoCreditElement;
									DecPageAutoCreditElement = AcordPDFXML.CreateElement("AUTOCREDITINFO");
									DecPageAutoCredit.AppendChild(DecPageAutoCreditElement);
									DecPageAutoCreditElement.SetAttribute(fieldType,fieldTypeNormal);
									DecPageAutoCreditElement.SetAttribute(id,CreditSurchRowCounter.ToString());
									DecPageAutoCreditElement.InnerXml += "<CREDITDISC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strCreditSurch) +"</CREDITDISC>"; 
									#endregion
								}
								else if (gStrPdfFor == PDFForAcord)
								{
									#region Supplement Page
									XmlElement SupplementAutoSurchElement;
									SupplementAutoSurchElement = AcordPDFXML.CreateElement("AUTOSURCHARGEINFO");
									SupplementAutoSurch.AppendChild(SupplementAutoSurchElement);
									SupplementAutoSurchElement.SetAttribute(fieldType,fieldTypeNormal);
									SupplementAutoSurchElement.SetAttribute(id,CreditSurchRowCounter.ToString());
														

									SupplementAutoSurchElement.InnerXml += "<SURCHARGE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC"))+"</SURCHARGE>"; 
									//SupplementAutoSurchElement.InnerXml += "<SURCHARGE_AMT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</SURCHARGE_AMT>"; 
									#endregion
								}
								CreditSurchRowCounter++;
							}
							CreditSurchRowCounter = 0;
							
							#endregion
						}
					}
						// When process Commited
					else
					{
						int CreditSurchRowCounter = 0;
						

						#region Credits
						XmlElement DecPageAutoCredit;
						DecPageAutoCredit = AcordPDFXML.CreateElement("AUTOCREDIT");

						XmlElement SupplementAutoCredit;
						SupplementAutoCredit = AcordPDFXML.CreateElement("AUTOCREDIT");

						if (gStrPdfFor == PDFForDecPage)
						{
							#region Dec Page Element
							AutoElementDecPage.AppendChild(DecPageAutoCredit);
							DecPageAutoCredit.SetAttribute(fieldType,fieldTypeMultiple);
							DecPageAutoCredit.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECREDIT"));
							DecPageAutoCredit.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGECREDIT"));
							DecPageAutoCredit.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGECREDITEXTN"));
							DecPageAutoCredit.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGECREDITEXTN"));
							#endregion
						}
						else if (gStrPdfFor == PDFForAcord)
						{
							#region Supplement Page Element
							AutoElementSuppliment.AppendChild(SupplementAutoCredit);
							SupplementAutoCredit.SetAttribute(fieldType,fieldTypeMultiple);
							SupplementAutoCredit.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTCREDIT"));
							SupplementAutoCredit.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTCREDIT"));
							SupplementAutoCredit.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTCREDITEXTN"));
							SupplementAutoCredit.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTCREDITEXTN"));
							#endregion
						}

						htpremium_dis.Clear(); 
						// for discount
						
						foreach (DataRow CreditNode in DSTempVehicle.Tables[1].Rows )
						{
							if(CreditNode["COMPONENT_TYPE"].ToString()=="D" && (CreditNode["COVERAGE_PREMIUM"].ToString()!="0" && CreditNode["COVERAGE_PREMIUM"].ToString()!="0.00" && CreditNode["COVERAGE_PREMIUM"].ToString()!=""))
							{
//								if(!htpremium_dis.Contains(getAttributeValue(CreditNode,"COMPONENT_CODE")))
//									htpremium_dis.Add(getAttributeValue(CreditNode,"COMPONENT_CODE"),getAttributeValue(CreditNode,"STEPPREMIUM"));
//								else
//									continue;
								// Remove Discount Word from Credit discription 
								string strCreditDisc="",strCreditPerc="";
								strCreditDisc = CreditNode["COMP_REMARKS"].ToString();//getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n","");
								strCreditPerc = CreditNode["COM_EXT_AD"].ToString();
								if(strCreditDisc.IndexOf(":")>0)
								{
									strCreditDisc=strCreditDisc.Substring(strCreditDisc.IndexOf(":",strCreditDisc.LastIndexOf(strCreditDisc)));
									strCreditDisc=strCreditDisc.Replace(":","");
									if(strCreditPerc!="")
									strCreditDisc=strCreditDisc+" - "+strCreditPerc;
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
										//credit information
										#region Dec Page
										XmlElement DecPageAutoCreditElement;
										DecPageAutoCreditElement = AcordPDFXML.CreateElement("AUTOCREDITINFO");
										DecPageAutoCredit.AppendChild(DecPageAutoCreditElement);
										DecPageAutoCreditElement.SetAttribute(fieldType,fieldTypeNormal);
										DecPageAutoCreditElement.SetAttribute(id,CreditSurchRowCounter.ToString());
										DecPageAutoCreditElement.InnerXml += "<CREDITDISC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strCreditDisc) +"</CREDITDISC>"; 
										#endregion
									}
								}
								else if (gStrPdfFor == PDFForAcord)
								{
									#region Supplement Page
									XmlElement SupplementAutoCreditElement;
									SupplementAutoCreditElement = AcordPDFXML.CreateElement("AUTOCREDITINFO");
									SupplementAutoCredit.AppendChild(SupplementAutoCreditElement);
									SupplementAutoCreditElement.SetAttribute(fieldType,fieldTypeNormal);
									SupplementAutoCreditElement.SetAttribute(id,CreditSurchRowCounter.ToString());
								
									SupplementAutoCreditElement.InnerXml +="<AUTO_AUTODESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(AutoDetail["INSURED_VEH_NUMBER"].ToString()) + "  "  + RemoveJunkXmlCharacters(AutoDetail["VEHICLE_YEAR"].ToString()) +"/"+ RemoveJunkXmlCharacters(AutoDetail["MAKE"].ToString()) +"/" + RemoveJunkXmlCharacters(AutoDetail["MODEL"].ToString()) + "/" + RemoveJunkXmlCharacters(AutoDetail["VIN"].ToString()) +"     "+ RemoveJunkXmlCharacters(AutoDetail["BODY_TYPE"].ToString()) +"</AUTO_AUTODESCRIPTION>";
									SupplementAutoCreditElement.InnerXml += "<CREDITDISC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strCreditDisc)+"</CREDITDISC>"; 
									//SupplementAutoCreditElement.InnerXml += "<CREDIT_AMT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</CREDIT_AMT>"; 
									#endregion
								}
								if(strCreditDisc.IndexOf("Insurance")>0 && gStrPdfFor == PDFForDecPage)
								{
								}
								else
								{
									CreditSurchRowCounter++;
								}
							}
						}

						#endregion

								

						#region Surcharges
						XmlElement DecPageAutoSurch;
						DecPageAutoSurch = AcordPDFXML.CreateElement("AUTOSURCHARGE");

						XmlElement SupplementAutoSurch;
						SupplementAutoSurch = AcordPDFXML.CreateElement("AUTOSURCHARGE");

						if (gStrPdfFor == PDFForDecPage)
						{
							#region Dec Page Element
							AutoElementDecPage.AppendChild(DecPageAutoSurch);
							DecPageAutoSurch.SetAttribute(fieldType,fieldTypeMultiple);
							DecPageAutoSurch.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESRCHG"));
							DecPageAutoSurch.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHG"));
							DecPageAutoSurch.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESRCHGEXTN"));
							DecPageAutoSurch.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHGEXTN"));
							#endregion
						}
						else if (gStrPdfFor == PDFForAcord)
						{
							//reset counter value for surcharge
							CreditSurchRowCounter = 0;
							#region Supplement Page Element
							AutoElementSuppliment.AppendChild(SupplementAutoSurch);
							SupplementAutoSurch.SetAttribute(fieldType,fieldTypeMultiple);
							SupplementAutoSurch.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTSRCHG"));
							SupplementAutoSurch.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTSRCHG"));
							SupplementAutoSurch.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTSRCHGEXTN"));
							SupplementAutoSurch.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTSRCHGEXTN"));
							#endregion
						}

						htpremium_sur.Clear(); 
						foreach (DataRow CreditNode in DSTempVehicle.Tables[1].Rows)
						{

							if(CreditNode["COMPONENT_TYPE"].ToString()=="S" && (CreditNode["COVERAGE_PREMIUM"].ToString()!="0" && CreditNode["COVERAGE_PREMIUM"].ToString()!="0.00" && CreditNode["COVERAGE_PREMIUM"].ToString()!=""))
							{
								//							if(!htpremium_sur.Contains(getAttributeValue(CreditNode,"COMPONENT_CODE")))
								//								htpremium_sur.Add(getAttributeValue(CreditNode,"COMPONENT_CODE"),getAttributeValue(CreditNode,"STEPPREMIUM"));
								//							else
								//								continue;
								// Remove Surcharge Word from Credit discription
								string strCreditSurch = CreditNode["COMP_REMARKS"].ToString();//getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n","");
								string strCreditSurcPerc = CreditNode["COM_EXT_AD"].ToString();
								if(strCreditSurch.IndexOf(":")>0)
								{
									strCreditSurch=strCreditSurch.Substring(strCreditSurch.IndexOf(":",strCreditSurch.LastIndexOf(strCreditSurch)));
									strCreditSurch=strCreditSurch.Replace(":","");
									if(strCreditSurcPerc!="")
									strCreditSurch=strCreditSurch+" - "+strCreditSurcPerc;
									else
									strCreditSurch=strCreditSurch;
								}
								if (gStrPdfFor == PDFForDecPage)
								{
									
									// surcharge information
									#region Dec Page
									XmlElement DecPageAutoCreditElement;
									DecPageAutoCreditElement = AcordPDFXML.CreateElement("AUTOCREDITINFO");
									DecPageAutoCredit.AppendChild(DecPageAutoCreditElement);
									DecPageAutoCreditElement.SetAttribute(fieldType,fieldTypeNormal);
									DecPageAutoCreditElement.SetAttribute(id,CreditSurchRowCounter.ToString());
									DecPageAutoCreditElement.InnerXml += "<CREDITDISC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strCreditSurch) +"</CREDITDISC>"; 
									#endregion
								}
								else if (gStrPdfFor == PDFForAcord)
								{
									#region Supplement Page
									XmlElement SupplementAutoSurchElement;
									SupplementAutoSurchElement = AcordPDFXML.CreateElement("AUTOSURCHARGEINFO");
									SupplementAutoSurch.AppendChild(SupplementAutoSurchElement);
									SupplementAutoSurchElement.SetAttribute(fieldType,fieldTypeNormal);
									SupplementAutoSurchElement.SetAttribute(id,CreditSurchRowCounter.ToString());
														

									SupplementAutoSurchElement.InnerXml += "<SURCHARGE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strCreditSurch)+"</SURCHARGE>"; 
									//SupplementAutoSurchElement.InnerXml += "<SURCHARGE_AMT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</SURCHARGE_AMT>"; 
									#endregion
								}
								CreditSurchRowCounter++;
							}
						}
						CreditSurchRowCounter = 0;
							
						#endregion
						
					}
					#endregion

					if ((gStrPdfFor == PDFForAcord) && (AutoCtr == 0))
					{	
						AutoElementAcord90.InnerXml += "<MOTORVEHICLEREPORT " + fieldType +"=\""+ fieldTypeText +"\">"+ intOrdered +"</MOTORVEHICLEREPORT>";		
						if(intWorkLoss==1)
						{
							AutoElementAcord90.InnerXml += "<PRINTNAME " + fieldType +"=\""+ fieldTypeText +"\">"+ strDriverName +"</PRINTNAME>";		
						}
					}

					AutoCtr++;
					intpageno++;
				}
	
				//Added by Mohit Agarwal 4-Apr-2007
				#region Violations
				if (gStrPdfFor == PDFForAcord)
				{	
					#region setting Root Operate Attribute
					XmlElement ViolationRootElement;
					ViolationRootElement = AcordPDFXML.CreateElement("VIOLATIONS");
					Acord90RootElement.AppendChild(ViolationRootElement);
					ViolationRootElement.SetAttribute(fieldType,fieldTypeMultiple);
					ViolationRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD90VIOLATION"));
					ViolationRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD90VIOLATION"));
					ViolationRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD90VIOLATIONEXTN"));
					ViolationRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD90VIOLATIONEXTN"));
					#endregion
				
					objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					objDataWrapper.AddParameter("@POLID",gStrPolicyId);
					objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempViolation = objDataWrapper.ExecuteDataSet("Proc_GetPDFViolationsPPA");
					objDataWrapper.ClearParameteres();
					//DSTempViolation = objDataWrapper.ExecuteDataSet("Proc_GetPDFViolationsPPA " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
						
					foreach(DataRow ViolationDetail in DSTempViolation.Tables[0].Rows)
					{
//						if(violations_ctr >= 2)
//							break;

						XmlElement ViolationElement;
						ViolationElement = AcordPDFXML.CreateElement("VIOLATIONINFO");
						ViolationRootElement.AppendChild(ViolationElement);
						ViolationElement.SetAttribute(fieldType,fieldTypeNormal);
						ViolationElement.SetAttribute(id,violations_ctr.ToString());

						//ViolationElement.InnerXml +="<ViolationNUMBER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(ViolationDetail["ViolationNO"].ToString())+"</ViolationNUMBER>"; 
						ViolationElement.InnerXml +="<DRIVER_NUMBER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(ViolationDetail["DRIVER_ID"].ToString())+"</DRIVER_NUMBER>";
						if(ViolationDetail["MVR_DATE"] != System.DBNull.Value && ViolationDetail["MVR_DATE"].ToString() != "")
							ViolationElement.InnerXml +="<DRIVER_ACCIDENT_DATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Convert.ToDateTime(ViolationDetail["MVR_DATE"]).ToString("MM/dd/yyyy"))+"</DRIVER_ACCIDENT_DATE>"; 
						ViolationElement.InnerXml +="<DRIVER_ACCIDENT_DESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(ViolationDetail["VIOLATION_DESC"].ToString())+"</DRIVER_ACCIDENT_DESC>"; 
						if(ViolationDetail["MVR_DEATH"].ToString() == "Y")
							ViolationElement.InnerXml +="<BI_YES " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Yes")+"</BI_YES>"; 
						if(ViolationDetail["MVR_DEATH"].ToString() == "N")
							ViolationElement.InnerXml +="<BI_NO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("No")+"</BI_NO>"; 
						if(ViolationDetail["MVR_AMOUNT"].ToString()!="")
						ViolationElement.InnerXml +="<PROPERTYDAMAGE " + fieldType +"=\""+ fieldTypeText +"\">$"+RemoveJunkXmlCharacters(ViolationDetail["MVR_AMOUNT"].ToString())+"</PROPERTYDAMAGE>"; 
						if(violations_ctr == 0)
						{
							if(DSTempViolation.Tables.Count >= 2)
							{
								if(DSTempViolation.Tables[1].Rows[0]["VIOL_COUNT"].ToString() != "0")
									ViolationElement.InnerXml += "<PRIORLOSS_CHECK " + fieldType +"=\""+ fieldTypeText +"\">"+ "1" +"</PRIORLOSS_CHECK>";		
								else
									ViolationElement.InnerXml += "<PRIORLOSS_CHECK " + fieldType +"=\""+ fieldTypeText +"\">"+ "0" +"</PRIORLOSS_CHECK>";		
							}
						}
						violations_ctr++;

					}
					violations_ctr=0;					
				}
				#endregion

				#region Collision Applicable
				if (gStrPdfFor == PDFForAcord)
				{	
					#region setting Root Operate Attribute
					XmlElement ApplicableRootElement;
					ApplicableRootElement = AcordPDFXML.CreateElement("LCAPPLICABLE");
					Acord90RootElement.AppendChild(ApplicableRootElement);
					ApplicableRootElement.SetAttribute(fieldType,fieldTypeMultiple);
					ApplicableRootElement.SetAttribute(PrimPDF,"");
					ApplicableRootElement.SetAttribute(PrimPDFBlocks,"1");
					ApplicableRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD90OPERATOREXTN"));
					ApplicableRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD90OPERATOREXTN"));
					#endregion
				
					XmlElement ApplicableElement;
					ApplicableElement = AcordPDFXML.CreateElement("APPLICABLEINFO");
					ApplicableRootElement.AppendChild(ApplicableElement);
					ApplicableElement.SetAttribute(fieldType,fieldTypeNormal);
					ApplicableElement.SetAttribute(id,"0");

					ApplicableElement.InnerXml +="<LC_APPLICABLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(coll_applicable.ToString())+"</LC_APPLICABLE>"; 
				}

				#endregion
			
				#region Supplemental Page
				if (gStrPdfFor == PDFForAcord)
				{
					XmlElement SupplementalElement;
					SupplementalElement = AcordPDFXML.CreateElement("ADDINSURED");
					SupplementalRootElement.AppendChild(SupplementalElement);
					SupplementalElement.SetAttribute(fieldType,fieldTypeSingle);
					
					SupplementalElement.InnerXml += "<HEALTH_CARE_PROVIDER " + fieldType +"=\""+ fieldTypeText +"\">"+ strHealthCare +"</HEALTH_CARE_PROVIDER>";

					if(strTransport.Equals("1"))
						SupplementalElement.InnerXml += "<VEHICLE_USE " + fieldType +"=\""+ fieldTypeText +"\">Yes</VEHICLE_USE>";
					else
						SupplementalElement.InnerXml += "<VEHICLE_USE " + fieldType +"=\""+ fieldTypeText +"\">No</VEHICLE_USE>";

					if(strSalvage.Equals("1"))
						SupplementalElement.InnerXml += "<SALVAGE_TITLE " + fieldType +"=\""+ fieldTypeText +"\">Yes</SALVAGE_TITLE>";
					else
						SupplementalElement.InnerXml += "<SALVAGE_TITLE " + fieldType +"=\""+ fieldTypeText +"\">No</SALVAGE_TITLE>";

					if(strVolunteer.Equals("1"))
						SupplementalElement.InnerXml += "<VOLUNTEER_FIREMAN " + fieldType +"=\""+ fieldTypeText +"\">Yes</VOLUNTEER_FIREMAN>";
					else
						SupplementalElement.InnerXml += "<VOLUNTEER_FIREMAN " + fieldType +"=\""+ fieldTypeText +"\">No</VOLUNTEER_FIREMAN>";

					if(strUSCitizen.Equals("1"))
						SupplementalElement.InnerXml += "<US_CITIZEN " + fieldType +"=\""+ fieldTypeText +"\">Yes</US_CITIZEN>";
					else
						SupplementalElement.InnerXml += "<US_CITIZEN " + fieldType +"=\""+ fieldTypeText +"\">No</US_CITIZEN>";

					if(strLicensed.Equals("1"))
						SupplementalElement.InnerXml += "<LICENSED_DRIVER " + fieldType +"=\""+ fieldTypeText +"\">Yes</LICENSED_DRIVER>";
					else
						SupplementalElement.InnerXml += "<LICENSED_DRIVER " + fieldType +"=\""+ fieldTypeText +"\">No</LICENSED_DRIVER>";
				}
				#endregion

				#region POLICY ELEMENT
				//				XmlElement AcordPolicyElement;
				//				AcordPolicyElement = AcordPDFXML.CreateElement("POLICY");
				//				Acord90RootElement.AppendChild(AcordPolicyElement);
				//				AcordPolicyElement.SetAttribute(fieldType,fieldTypeSingle);
				//			
				//				AcordPolicyElement.InnerXml +="<ESTIMATED_TOTAL " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + (dbEstTotal +".00") + "</ESTIMATED_TOTAL>";
				//				AcordPolicyElement.InnerXml +="<RECEIVED_PREMIUM " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + strrecdPremium + "</RECEIVED_PREMIUM>";
				//				double dblrecPremium=0;
				//				if(strrecdPremium!="")
				//					dblrecPremium=double.Parse(strrecdPremium); 
				//				AcordPolicyElement.InnerXml +="<BALANCE " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + ((dbEstTotal - dblrecPremium) +".00") + "</BALANCE>";


				#endregion
			}

		}
		#endregion

		#region Creating Prior Policy/Coverage XML
		private void CreatePriorPolicyCoverage()
		{
			if (gStrPdfFor == PDFForAcord)
			{
//				DSTempDataSet.Clear(); 
//				objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//				objDataWrapper.AddParameter("@LOBCODE","AUTOP");
//				objDataWrapper.AddParameter("@DATAFOR","POLICY");
//				DSTempDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFPriorPolicyAndLossDetails");
//				objDataWrapper.ClearParameteres();
				//DSTempDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFPriorPolicyAndLossDetails " + gStrClientID + ",'AUTOP','POLICY'");
			
				if (DSTempPriorLossDataSet.Tables[0].Rows.Count>0)
				{
					#region Acord 90 Page			
					XmlElement Acord90PriorPolicyElement;
					Acord90PriorPolicyElement = AcordPDFXML.CreateElement("PRIORPOLICYCOVERAGE");
					Acord90RootElement.AppendChild(Acord90PriorPolicyElement);
					Acord90PriorPolicyElement.SetAttribute(fieldType,fieldTypeSingle);

					Acord90PriorPolicyElement.InnerXml +="<PRIORCARRIER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPriorLossDataSet.Tables[0].Rows[0]["CARRIER"].ToString()) + "</PRIORCARRIER>";
					Acord90PriorPolicyElement.InnerXml +="<PRIORYEARSWITHCO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPriorLossDataSet.Tables[0].Rows[0]["YEARS_PRIOR_COMP"].ToString()) + "</PRIORYEARSWITHCO>";
					if(DSTempPriorLossDataSet.Tables[0].Rows[0]["EFF_DATE"].ToString() != "")
						Acord90PriorPolicyElement.InnerXml +="<PRIOREXPIRATIONDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPriorLossDataSet.Tables[0].Rows[0]["OLD_POLICY_NUMBER"].ToString()) + "/" + RemoveJunkXmlCharacters(DSTempPriorLossDataSet.Tables[0].Rows[0]["EFF_DATE"].ToString()) + "</PRIOREXPIRATIONDATE>";
					else
						Acord90PriorPolicyElement.InnerXml +="<PRIOREXPIRATIONDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPriorLossDataSet.Tables[0].Rows[0]["OLD_POLICY_NUMBER"].ToString()) + "</PRIOREXPIRATIONDATE>";
					#endregion
				}
			}
		}
		#endregion 

		#region Code for Underwriting And General Info Xml Generation
		private void createAutoUnderwritingGeneralXML()
		{
			if (gStrPdfFor == PDFForAcord)
			{
				//Added by Mohit Agarwal 11-Apr-2007 
				int Operator_Ctr = 0;
//				objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//				objDataWrapper.AddParameter("@POLID",gStrPolicyId);
//				objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//				objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//				objDataWrapper.AddParameter("@VEHICLEID","0");
//				DataSet DSTempOperator = objDataWrapper.ExecuteDataSet("Proc_GetPDFAutoOperatorDtls");
//				objDataWrapper.ClearParameteres();
				//DataSet DSTempOperator = objDataWrapper.ExecuteDataSet("Proc_GetPDFAutoOperatorDtls " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "',0");
						
				foreach(DataRow OperatorDetail in DSTempOperators.Tables[0].Rows)
				{
					if(OperatorDetail["IN_MILITARY"].ToString() == "Y")
					{
						in_military = "1";
						if(driv_no_military != "")
							driv_no_military += ", ";
						driv_no_military += (Operator_Ctr+1).ToString();
					}

					Operator_Ctr++;
				}

				objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				objDataWrapper.AddParameter("@POLID",gStrPolicyId);
				objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFAutoUnderwritingDetails");
				objDataWrapper.ClearParameteres();
				//DSTempDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFAutoUnderwritingDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
				if(DSTempDataSet.Tables[0].Rows.Count >0)
				{
					#region Acord90 Page General Info
					XmlElement Acord90GenInfoElement;
					Acord90GenInfoElement = AcordPDFXML.CreateElement("GENERALINFO");
					Acord90RootElement.AppendChild(Acord90GenInfoElement);
					Acord90GenInfoElement.SetAttribute(fieldType,fieldTypeSingle);
				
				
					Acord90GenInfoElement.InnerXml +="<SOLELYOWNED " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_NON_OWNED_VEH"].ToString()) + "</SOLELYOWNED>";
					Acord90GenInfoElement.InnerXml +="<CARMODIFICATION " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CAR_MODIFIED"].ToString()) + "</CARMODIFICATION>";
					Acord90GenInfoElement.InnerXml +="<EXISTINGDAMAGE " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["EXISTING_DMG"].ToString()) + "</EXISTINGDAMAGE>";
					Acord90GenInfoElement.InnerXml +="<CARATSCHOOL " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_CAR_AT_SCH"].ToString()) + "</CARATSCHOOL>";
					Acord90GenInfoElement.InnerXml +="<OTHERINSURANCE " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_OTH_AUTO_INSU"].ToString()) + "</OTHERINSURANCE>";
					Acord90GenInfoElement.InnerXml +="<OTHERINSURANCEWITHCO " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["MULTI_POLICY_DISC_APPLIED"].ToString()) + "</OTHERINSURANCEWITHCO>";
					//Acord90GenInfoElement.InnerXml +="<MEMBERINMILITARY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANIMALS_EXO_PETS_HISTORY"].ToString()) + "</MEMBERINMILITARY>";
					Acord90GenInfoElement.InnerXml +="<MEMBERINMILITARY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(in_military) + "</MEMBERINMILITARY>";
					if(driv_no_military != "")
						Acord90GenInfoElement.InnerXml +="<DRIVERS_MILITARY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("(" + driv_no_military + ")") + "</DRIVERS_MILITARY>";
					Acord90GenInfoElement.InnerXml +="<LICENSEREVOKED " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["DRIVER_SUS_REVOKED"].ToString()) + "</LICENSEREVOKED>";

					Acord90GenInfoElement.InnerXml +="<MENTALIMPAIRMENT " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PHY_MENTL_CHALLENGED"].ToString()) + "</MENTALIMPAIRMENT>";
					Acord90GenInfoElement.InnerXml +="<FINANCIALRESPONSIBILITY " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_FINANCIAL_RESPONSIBILITY"].ToString()) + "</FINANCIALRESPONSIBILITY>";
					Acord90GenInfoElement.InnerXml +="<INSURANCETRANSFERRED " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["INS_AGENCY_TRANSFER"].ToString()) + "</INSURANCETRANSFERRED>";
					Acord90GenInfoElement.InnerXml +="<COVERAGEDECLINED " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["COVERAGE_DECLINED"].ToString()) + "</COVERAGEDECLINED>";
					Acord90GenInfoElement.InnerXml +="<VEHICLEINSPECTED " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_VEH_INSPECTED"].ToString()) + "</VEHICLEINSPECTED>";
					
					//RENARKS
					if(DSTempDataSet.Tables[0].Rows[0]["ANY_NON_OWNED_VEH_PP_DESC"] != null && DSTempDataSet.Tables[0].Rows[0]["ANY_NON_OWNED_VEH_PP_DESC"].ToString().Trim() != "")
						Acord90GenInfoElement.InnerXml +="<VEHICLE_DESC " + fieldType + "=\"" + fieldTypeText + "\">#1:" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_NON_OWNED_VEH_PP_DESC"].ToString()) + "</VEHICLE_DESC>";
					if(DSTempDataSet.Tables[0].Rows[0]["CAR_MODIFIED_DESC"] != null && DSTempDataSet.Tables[0].Rows[0]["CAR_MODIFIED_DESC"].ToString().Trim() != "")
					{
						string car_modified = DSTempDataSet.Tables[0].Rows[0]["CAR_MODIFIED_DESC"].ToString() + "/" + DSTempDataSet.Tables[0].Rows[0]["COST_EQUIPMENT_DESC"].ToString();
						if(car_modified.IndexOf("/") > 0)
							Acord90GenInfoElement.InnerXml +="<CAR_MODIFICATION_DESC " + fieldType + "=\"" + fieldTypeText + "\">#2:" + RemoveJunkXmlCharacters(car_modified)+ "</CAR_MODIFICATION_DESC>";   
						else
							Acord90GenInfoElement.InnerXml +="<CAR_MODIFICATION_DESC " + fieldType + "=\"" + fieldTypeText + "\">#2:" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CAR_MODIFIED_DESC"].ToString())+ "</CAR_MODIFICATION_DESC>";   
					}
					if(DSTempDataSet.Tables[0].Rows[0]["EXISTING_DMG_PP_DESC"] != null && DSTempDataSet.Tables[0].Rows[0]["EXISTING_DMG_PP_DESC"].ToString().Trim() != "")
						Acord90GenInfoElement.InnerXml +="<DAMAGED_DESC " + fieldType + "=\"" + fieldTypeText + "\">#3:" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["EXISTING_DMG_PP_DESC"].ToString()) + "</DAMAGED_DESC>";   
					if(stCode.Equals("IN"))
					{
						if(DSTempDataSet.Tables[0].Rows[0]["ANY_CAR_AT_SCH_DESC"] != null && DSTempDataSet.Tables[0].Rows[0]["ANY_CAR_AT_SCH_DESC"].ToString().Trim() != "")
							Acord90GenInfoElement.InnerXml +="<CAR_DESC " + fieldType + "=\"" + fieldTypeText + "\">#5:" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_CAR_AT_SCH_DESC"].ToString()) + "</CAR_DESC>";   
						if(DSTempDataSet.Tables[0].Rows[0]["ANY_OTH_AUTO_INSU_DESC"] != null && DSTempDataSet.Tables[0].Rows[0]["ANY_OTH_AUTO_INSU_DESC"].ToString().Trim() != "")
							Acord90GenInfoElement.InnerXml +="<AUTO_INSURANCE_DESC " + fieldType + "=\"" + fieldTypeText + "\">#7:" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_OTH_AUTO_INSU_DESC"].ToString()) + "</AUTO_INSURANCE_DESC>";   
					}

					if(DSTempDataSet.Tables[0].Rows[0]["USE_AS_TRANSPORT_FEE"]!=null && DSTempDataSet.Tables[0].Rows[0]["USE_AS_TRANSPORT_FEE"].ToString()!="" )
						if(DSTempDataSet.Tables[0].Rows[0]["USE_AS_TRANSPORT_FEE"].ToString()!="0" )
							strTransport=RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["USE_AS_TRANSPORT_FEE"].ToString()); 

					if(DSTempDataSet.Tables[0].Rows[0]["SALVAGE_TITLE"]!=null && DSTempDataSet.Tables[0].Rows[0]["SALVAGE_TITLE"].ToString()!="" )
						if(DSTempDataSet.Tables[0].Rows[0]["SALVAGE_TITLE"].ToString()!="0" )
							strSalvage=RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["SALVAGE_TITLE"].ToString()); 

					if(DSTempDataSet.Tables[0].Rows[0]["IS_OTHER_THAN_INSURED"]!=null && DSTempDataSet.Tables[0].Rows[0]["IS_OTHER_THAN_INSURED"].ToString()!="" )
						if(DSTempDataSet.Tables[0].Rows[0]["IS_OTHER_THAN_INSURED"].ToString()!="0" )
							strLicensed=RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_OTHER_THAN_INSURED"].ToString()); 
					#endregion

				    int add_underwritingpage = 0;
					if(stCode.Equals("MI"))
					{
						if(DSTempDataSet.Tables[0].Rows[0]["ANY_CAR_AT_SCH_DESC"].ToString() != "" || DSTempDataSet.Tables[0].Rows[0]["ANY_OTH_AUTO_INSU_DESC"].ToString() != "" || DSTempDataSet.Tables[0].Rows[0]["USE_AS_TRANSPORT_FEE_DESC"].ToString() != "" || DSTempDataSet.Tables[0].Rows[0]["COVERAGE_DECLINED_PP_DESC"].ToString() != "" || DSTempDataSet.Tables[0].Rows[0]["AGENCY_VEH_INSPECTED_PP_DESC"].ToString() != "" || DSTempDataSet.Tables[0].Rows[0]["ANY_FINANCIAL_RESPONSIBILITY_PP_DESC"].ToString() != "" ||
							DSTempDataSet.Tables[0].Rows[0]["MULTI_POLICY_DISC_APPLIED_PP_DESC"].ToString() != "" || DSTempDataSet.Tables[0].Rows[0]["DRIVER_SUS_REVOKED_PP_DESC"].ToString() != "" || DSTempDataSet.Tables[0].Rows[0]["PHY_MENTL_CHALLENGED_PP_DESC"].ToString() != "")
							add_underwritingpage = 1;
					}
					else
					{
						if(DSTempDataSet.Tables[0].Rows[0]["AGENCY_VEH_INSPECTED_PP_DESC"].ToString() != "" || DSTempDataSet.Tables[0].Rows[0]["MULTI_POLICY_DISC_APPLIED_PP_DESC"].ToString() != "" || DSTempDataSet.Tables[0].Rows[0]["DRIVER_SUS_REVOKED_PP_DESC"].ToString() != "" || DSTempDataSet.Tables[0].Rows[0]["PHY_MENTL_CHALLENGED_PP_DESC"].ToString() != "")
							add_underwritingpage = 1;
					}
					if(add_underwritingpage == 1)
					{
						#region Acord 90 Remarks Additional Sheet
						XmlElement ACORD90ADDLREMARKS;
						ACORD90ADDLREMARKS = AcordPDFXML.CreateElement("ACORD90ADDLREMARKS");
						Acord90RootElement.AppendChild(ACORD90ADDLREMARKS);
						ACORD90ADDLREMARKS.SetAttribute(fieldType,fieldTypeMultiple);
						ACORD90ADDLREMARKS.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD90REMARKS"));
						ACORD90ADDLREMARKS.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD90REMARKS"));
						ACORD90ADDLREMARKS.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD90REMARKSEXTN"));
						ACORD90ADDLREMARKS.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD90REMARKSEXTN"));
				
						if(stCode.Equals("MI"))
						{
							if(DSTempDataSet.Tables[0].Rows[0]["ANY_CAR_AT_SCH_DESC"] != null && DSTempDataSet.Tables[0].Rows[0]["ANY_CAR_AT_SCH_DESC"].ToString().Trim() != "")
								ACORD90ADDLREMARKS.InnerXml +="<CAR_DESC " + fieldType + "=\"" + fieldTypeText + "\">#5:" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_CAR_AT_SCH_DESC"].ToString()) + "</CAR_DESC>";   
							if(DSTempDataSet.Tables[0].Rows[0]["ANY_OTH_AUTO_INSU_DESC"] != null && DSTempDataSet.Tables[0].Rows[0]["ANY_OTH_AUTO_INSU_DESC"].ToString().Trim() != "")
								ACORD90ADDLREMARKS.InnerXml +="<AUTO_INSURANCE_DESC " + fieldType + "=\"" + fieldTypeText + "\">#7:" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_OTH_AUTO_INSU_DESC"].ToString()) + "</AUTO_INSURANCE_DESC>";   
							if(DSTempDataSet.Tables[0].Rows[0]["USE_AS_TRANSPORT_FEE_DESC"] != null && DSTempDataSet.Tables[0].Rows[0]["USE_AS_TRANSPORT_FEE_DESC"].ToString().Trim() != "")
								ACORD90ADDLREMARKS.InnerXml +="<ANY_TRANSPORT_DESC " + fieldType + "=\"" + fieldTypeText + "\">#12:" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["USE_AS_TRANSPORT_FEE_DESC"].ToString()) + "</ANY_TRANSPORT_DESC>"; 
							if(DSTempDataSet.Tables[0].Rows[0]["COVERAGE_DECLINED_PP_DESC"] != null && DSTempDataSet.Tables[0].Rows[0]["COVERAGE_DECLINED_PP_DESC"].ToString().Trim() != "")
								ACORD90ADDLREMARKS.InnerXml +="<COV_DECLINED_DESC " + fieldType + "=\"" + fieldTypeText + "\">#14:" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["COVERAGE_DECLINED_PP_DESC"].ToString()) + "</COV_DECLINED_DESC>"; 
							if(DSTempDataSet.Tables[0].Rows[0]["AGENCY_VEH_INSPECTED_PP_DESC"] != null && DSTempDataSet.Tables[0].Rows[0]["AGENCY_VEH_INSPECTED_PP_DESC"].ToString().Trim() != "")
								ACORD90ADDLREMARKS.InnerXml +="<AGENT_INSPECTION_DESC " + fieldType + "=\"" + fieldTypeText + "\">#16:" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_VEH_INSPECTED_PP_DESC"].ToString()) + "</AGENT_INSPECTION_DESC>"; 
							if(DSTempDataSet.Tables[0].Rows[0]["ANY_FINANCIAL_RESPONSIBILITY_PP_DESC"] != null && DSTempDataSet.Tables[0].Rows[0]["ANY_FINANCIAL_RESPONSIBILITY_PP_DESC"].ToString().Trim() != "")
								ACORD90ADDLREMARKS.InnerXml +="<ANY_FINANCIAL_DESC " + fieldType + "=\"" + fieldTypeText + "\">#11:" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_FINANCIAL_RESPONSIBILITY_PP_DESC"].ToString()) + "</ANY_FINANCIAL_DESC>"; 

						}
						else
						{
							if(DSTempDataSet.Tables[0].Rows[0]["AGENCY_VEH_INSPECTED_PP_DESC"] != null && DSTempDataSet.Tables[0].Rows[0]["AGENCY_VEH_INSPECTED_PP_DESC"].ToString().Trim() != "")
								ACORD90ADDLREMARKS.InnerXml +="<MEMBER_DESC " + fieldType + "=\"" + fieldTypeText + "\">#9:" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["AGENCY_VEH_INSPECTED_PP_DESC"].ToString()) + "</MEMBER_DESC>";   
						}

						if(DSTempDataSet.Tables[0].Rows[0]["MULTI_POLICY_DISC_APPLIED_PP_DESC"] != null && DSTempDataSet.Tables[0].Rows[0]["MULTI_POLICY_DISC_APPLIED_PP_DESC"].ToString().Trim() != "")
							ACORD90ADDLREMARKS.InnerXml +="<POLICY_DESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">#8:" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["MULTI_POLICY_DISC_APPLIED_PP_DESC"].ToString()) + "</POLICY_DESCRIPTION>";   
						if(DSTempDataSet.Tables[0].Rows[0]["DRIVER_SUS_REVOKED_PP_DESC"] != null && DSTempDataSet.Tables[0].Rows[0]["DRIVER_SUS_REVOKED_PP_DESC"].ToString().Trim() != "")
							ACORD90ADDLREMARKS.InnerXml +="<LICENSE_DESC " + fieldType + "=\"" + fieldTypeText + "\">#10:" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["DRIVER_SUS_REVOKED_PP_DESC"].ToString()) + "</LICENSE_DESC>";   
						if(DSTempDataSet.Tables[0].Rows[0]["PHY_MENTL_CHALLENGED_PP_DESC"] != null && DSTempDataSet.Tables[0].Rows[0]["PHY_MENTL_CHALLENGED_PP_DESC"].ToString().Trim() != "")
							ACORD90ADDLREMARKS.InnerXml +="<DRIVER_IMPAIRMENT_DESC " + fieldType + "=\"" + fieldTypeText + "\">#11:" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PHY_MENTL_CHALLENGED_PP_DESC"].ToString()) + "</DRIVER_IMPAIRMENT_DESC>"; 

				
						ACORD90ADDLREMARKS.InnerXml = "<ACORD90REMARKSINFO " + fieldType + "=\"" + fieldTypeNormal + "\" " + id + "=\"0\">" + ACORD90ADDLREMARKS.InnerXml + "</ACORD90REMARKSINFO>";
						#endregion
					}
					#region POLICY ELEMENT
					XmlElement AcordPolicyElement;
					AcordPolicyElement = AcordPDFXML.CreateElement("POLICY");
					Acord90RootElement.AppendChild(AcordPolicyElement);
					AcordPolicyElement.SetAttribute(fieldType,fieldTypeSingle);
			
					AcordPolicyElement.InnerXml +="<CURRENTADDRESS " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CUR_RES_TYPE"].ToString()) + "</CURRENTADDRESS>";
					#endregion
			
				}
			}
		}
		#endregion

		#region Code for Auto Addl Interests
		private void createAcord90AutoAddlIntXml()
		{
			#region Additional Int
			if (gStrPdfFor == PDFForAcord)
			{
//				objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//				objDataWrapper.AddParameter("@POLID",gStrPolicyId);
//				objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//				objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//				objDataWrapper.AddParameter("@VEHICLEID",0);
//				DSTempDataSet = objDataWrapper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS");
//				objDataWrapper.ClearParameteres();
				//DSTempDataSet = objDataWrapper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",0,'" + gStrCalledFrom + "'");
			
				#region Acord82 Page
				if (DSTempAddIntrst.Tables[0].Rows.Count >0)
				{
					XmlElement Acord90AddlInts;
					Acord90AddlInts = AcordPDFXML.CreateElement("ADDITIONALINTERESTS");
					Acord90RootElement.AppendChild(Acord90AddlInts);
					Acord90AddlInts.SetAttribute(fieldType,fieldTypeMultiple);
					Acord90AddlInts.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD90AUTOADDLINT"));
					Acord90AddlInts.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD90AUTOADDLINT"));
					Acord90AddlInts.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD90AUTOADDLINTEXTN"));
					Acord90AddlInts.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD90AUTOADDLINTEXTN"));

					int RowCounter = 0;
					foreach(DataRow Row in DSTempAddIntrst.Tables[0].Rows)
					{
						XmlElement Acord90ADDLINTElement;
						Acord90ADDLINTElement = AcordPDFXML.CreateElement("ADDLINTINFO");
						Acord90AddlInts.AppendChild(Acord90ADDLINTElement);
						Acord90ADDLINTElement.SetAttribute(fieldType,fieldTypeNormal);
						Acord90ADDLINTElement.SetAttribute(id,RowCounter.ToString());

						Acord90ADDLINTElement.InnerXml +="<ADDINT_VEHICLENO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["INSURED_VEH_NUMBER"].ToString()) + "</ADDINT_VEHICLENO>";
						Acord90ADDLINTElement.InnerXml +="<ADDLINTTYPEOFINT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["NATURE_OF_INTEREST"].ToString()) + "</ADDLINTTYPEOFINT>";
						Acord90ADDLINTElement.InnerXml +="<ADDLINTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["HOLDER_NAME"].ToString()) + ", " +  RemoveJunkXmlCharacters(Row["ADDRESS"].ToString()) + "</ADDLINTNAME>";
						Acord90ADDLINTElement.InnerXml +="<ADDLINTADD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["CITYSTATEZIP"].ToString()) + "</ADDLINTADD>";
						Acord90ADDLINTElement.InnerXml +="<ADDLINTLOANNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["LOAN_REF_NUMBER"].ToString()) + "</ADDLINTLOANNUM>";
					
						RowCounter++;
					}
				}
				#endregion
			}
			#endregion Additional Int
	
		}
		#endregion

		#region Code for Employer Information Interests
		private void createAcord90EmplXml()
		{
			#region Employer Information
			if (gStrPdfFor == PDFForAcord)
			{
				DSTempDataSet.Clear(); 

//				objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//				objDataWrapper.AddParameter("@POLID",gStrPolicyId);
//				objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//				objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//				DSTempDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
//				objDataWrapper.ClearParameteres();
				//DSTempDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			
				#region Acord90 Page
				if (DSTempApplicantDataSet.Tables[0].Rows.Count >0)
				{
					XmlElement Acord90Empl;
					Acord90Empl = AcordPDFXML.CreateElement("EMPLOYERINFORMATION");
					Acord90RootElement.AppendChild(Acord90Empl);
					Acord90Empl.SetAttribute(fieldType,fieldTypeMultiple);
					Acord90Empl.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD90COAPP"));
					Acord90Empl.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD90COAPP"));
					Acord90Empl.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD90COAPPEXTN"));
					Acord90Empl.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD90COAPPEXTN"));

					int RowCounter = 0;
					foreach(DataRow Row in DSTempApplicantDataSet.Tables[0].Rows)
					{
						XmlElement Acord90EMPLElement;
						Acord90EMPLElement = AcordPDFXML.CreateElement("EMPLOYERINFO");
						Acord90Empl.AppendChild(Acord90EMPLElement);
						Acord90EMPLElement.SetAttribute(fieldType,fieldTypeNormal);
						Acord90EMPLElement.SetAttribute(id,RowCounter.ToString());

						string coapp_add = Row["CO_APPLI_EMPL_ADD"].ToString().Trim();
						if(coapp_add.EndsWith(","))
							coapp_add = coapp_add.Substring(0,coapp_add.Length-1);

						Acord90EMPLElement.InnerXml +="<APPLICANT_EMPLOYER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["CO_APPLI_EMPL_NAME"].ToString()) + "</APPLICANT_EMPLOYER>";
						Acord90EMPLElement.InnerXml +="<APPLICANT_EMPLOYER_ADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(coapp_add) + "</APPLICANT_EMPLOYER_ADDRESS>";
						Acord90EMPLElement.InnerXml +="<APPLICANT_EMPLOYER_WPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["CO_APPLI_EMPL_PHONE"].ToString()) + "</APPLICANT_EMPLOYER_WPHONE>";
						Acord90EMPLElement.InnerXml +="<APPLICANT_YEARS_CURR_EMP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["YEARSEMPL"].ToString()) + "</APPLICANT_YEARS_CURR_EMP>";
					
						RowCounter++;
					}
				}
				#endregion
			}
			#endregion Additional Int
			
		}
		#endregion
		#region Endorsement Wordings
		private void RemoveEnorsementWordings()
		{
			try
			{
				if (prnOrd_covCode==null) return;
//				DataSet dsAutos = new DataSet();
//				objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//				objDataWrapper.AddParameter("@POLID",gStrPolicyId);
//				objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//				objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//				dsAutos = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_Details");
//				objDataWrapper.ClearParameteres();
				//dsAutos = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
			
				if(strOldPolicyVer=="")
					strOldPolicyVer = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
				foreach(DataRow AutoDetail in DSTempAutoDetailDataSet.Tables[0].Rows)
				{
					objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					objDataWrapper.AddParameter("@POLID",gStrPolicyId);
					objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					objDataWrapper.AddParameter("@VEHICLEID",AutoDetail["VEHICLE_ID"]);
					objDataWrapper.AddParameter("@RISKTYPE",AutoDetail["RISKTYPE"]);
					objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DataSet DSNewCoverageEndorsemet = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_RiskWise_Coverage_Details");
					objDataWrapper.ClearParameteres();
					//DataSet DSNewCoverageEndorsemet = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_Coverage_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +AutoDetail["VEHICLE_ID"] +  ",'"+ AutoDetail["RISKTYPE"] +"','" + gStrCalledFrom + "'");
					objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					objDataWrapper.AddParameter("@POLID",gStrPolicyId);
					objDataWrapper.AddParameter("@VERSIONID",strOldPolicyVer);
					objDataWrapper.AddParameter("@VEHICLEID",AutoDetail["VEHICLE_ID"]);
					objDataWrapper.AddParameter("@RISKTYPE",AutoDetail["RISKTYPE"]);
					objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DataSet DSOldCoverageEndorsemet = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_RiskWise_Coverage_Details");
					objDataWrapper.ClearParameteres();
					//DataSet DSOldCoverageEndorsemet = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_Coverage_Details " + gStrClientID + "," + gStrPolicyId + "," + strOldPolicyVer + "," +AutoDetail["VEHICLE_ID"] +  ",'"+ AutoDetail["RISKTYPE"] +"','" + gStrCalledFrom + "'");
				
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
			//int AutoCtr=0;
			if(gStrPdfFor == PDFForDecPage)
			{
				int endorsCount = 0;
				int BlankPagePrinted=0;
				int counter = 0;
				int intCnt = 0,Cntrl=0;
				inttotalpage += intPrivacyPage;
				//check for even and odd number of pages
				inttotalpage = inttotalpage%2;
//				while(prnOrd_covCode[endorsCount] != null)
//					endorsCount++;
				endorsCount=prnOrd_covCode.Length;
				while(counter < endorsCount)
				{
					int lowestIndex = GetLowestPrnIndex(ref prnOrd, endorsCount);
					string prncovCode = prnOrd_covCode[lowestIndex];
					string prnAttFile = prnOrd_attFile[lowestIndex];
					if(prnAttFile != null && prnAttFile != "" && prncovCode !=null && prncovCode!="")
					{
						#region SWITCH CASE FOR DEC PAGE
						#region Previous Commented Code
						//					switch(prncovCode)
						//					{
						//							
						//						/*case "IFGHO":
						//						{
						//						}
						//							break;
						//						case "RREIM":
						//						{
						//							XmlElement DecPageEndoA89;
						//							DecPageEndoA89 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA89");
						//							DecPageRootElement.AppendChild(DecPageEndoA89);
						//							DecPageEndoA89.SetAttribute(fieldType,fieldTypeMultiple);
						//							if(prnAttFile != null && prnAttFile.ToString() != "")
						//							{
						//								DecPageEndoA89.SetAttribute(PrimPDF,prnAttFile.ToString());
						//								DecPageEndoA89.SetAttribute(PrimPDFBlocks,"1");
						//							}
						//							else
						//							{
						//								DecPageEndoA89.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA89"));
						//								DecPageEndoA89.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA89"));
						//							}
						//							DecPageEndoA89.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA89EXTN"));
						//							DecPageEndoA89.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA89EXTN"));
						//
						//							XmlElement EndoElementA89;
						//							EndoElementA89 = AcordPDFXML.CreateElement("EndoElementA89INFO");
						//							DecPageEndoA89.AppendChild(EndoElementA89);
						//							EndoElementA89.SetAttribute(fieldType,fieldTypeNormal);
						//							EndoElementA89.SetAttribute(id,AutoCtr.ToString());
						//							
						//						}
						//							break;
						//						case "LPD":
						//						{
						//						}
						//							break;
						//						case "COLL":
						//						{
						//						}
						//							break;
						//						case "COMP":
						//						{
						//						}
						//							break;
						//						case "ROAD":
						//						{
						//						}
						//							break;	
						//						case "MEC":
						//						{
						//							#region Dec Page Element
						//							if((stCode == "IN"))	
						//							{
						//								XmlElement DecPageEndoA16;
						//								DecPageEndoA16 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA16");
						//								DecPageRootElement.AppendChild(DecPageEndoA16);
						//								DecPageEndoA16.SetAttribute(fieldType,fieldTypeMultiple);
						//								if(prnAttFile != null && prnAttFile.ToString() != "")
						//								{
						//									DecPageEndoA16.SetAttribute(PrimPDF,prnAttFile.ToString());
						//									DecPageEndoA16.SetAttribute(PrimPDFBlocks,"1");
						//								}
						//								else
						//								{
						//									DecPageEndoA16.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA16"));
						//									DecPageEndoA16.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA16"));
						//								}
						//								DecPageEndoA16.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA16EXTN"));
						//								DecPageEndoA16.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA16EXTN"));
						//
						//								XmlElement EndoElementA16;
						//								EndoElementA16 = AcordPDFXML.CreateElement("EndoElementA16INFO");
						//								DecPageEndoA16.AppendChild(EndoElementA16);
						//								EndoElementA16.SetAttribute(fieldType,fieldTypeNormal);
						//								EndoElementA16.SetAttribute(id,AutoCtr.ToString());
						//							}
						//							#endregion
						//						}
						//							break;
						//						case "ENCNI":
						//						{
						//							#region Dec Page Element
						//						{
						//							XmlElement DecPageEndoA34;
						//							DecPageEndoA34 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA34");
						//							DecPageRootElement.AppendChild(DecPageEndoA34);
						//							DecPageEndoA34.SetAttribute(fieldType,fieldTypeMultiple);
						//							if(prnAttFile != null && prnAttFile.ToString() != "")
						//							{
						//								DecPageEndoA34.SetAttribute(PrimPDF,prnAttFile.ToString());
						//								DecPageEndoA34.SetAttribute(PrimPDFBlocks,"1");
						//							}
						//							else
						//							{
						//								DecPageEndoA34.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA34"));
						//								DecPageEndoA34.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA34"));
						//							}
						//							DecPageEndoA34.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA34EXTN"));
						//							DecPageEndoA34.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA34EXTN"));
						//
						//							XmlElement EndoElementA34;
						//							EndoElementA34 = AcordPDFXML.CreateElement("EndoElementA34INFO");
						//							DecPageEndoA34.AppendChild(EndoElementA34);
						//							EndoElementA34.SetAttribute(fieldType,fieldTypeNormal);
						//							EndoElementA34.SetAttribute(id,AutoCtr.ToString());
						//						}
						//							#endregion
						//						}
						//							break;
						//						case "SPSLO":
						//						{
						//							#region Dec Page Element
						//							if((stCode == "MI"))
						//							{
						//								XmlElement DecPageEndoA64;
						//								DecPageEndoA64 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA64");
						//								DecPageRootElement.AppendChild(DecPageEndoA64);
						//								DecPageEndoA64.SetAttribute(fieldType,fieldTypeMultiple);
						//								if(prnAttFile != null && prnAttFile.ToString() != "")
						//								{
						//									DecPageEndoA64.SetAttribute(PrimPDF,prnAttFile.ToString());
						//									DecPageEndoA64.SetAttribute(PrimPDFBlocks,"1");
						//								}
						//								else
						//								{
						//									DecPageEndoA64.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA64"));
						//									DecPageEndoA64.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA64"));
						//								}
						//								DecPageEndoA64.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA64EXTN"));
						//								DecPageEndoA64.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA64EXTN"));
						//
						//								XmlElement EndoElementA64;
						//								EndoElementA64 = AcordPDFXML.CreateElement("EndoElementA64INFO");
						//								DecPageEndoA64.AppendChild(EndoElementA64);
						//								EndoElementA64.SetAttribute(fieldType,fieldTypeNormal);
						//								EndoElementA64.SetAttribute(id,AutoCtr.ToString());
						//							}
						//							#endregion
						//						}
						//							break;
						//						case "DE95":
						//						{
						//							#region Dec Page Element
						//							if((stCode == "MI"))	
						//							{
						//								XmlElement DecPageEndoA95;
						//								DecPageEndoA95 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA95");
						//								DecPageRootElement.AppendChild(DecPageEndoA95);
						//								DecPageEndoA95.SetAttribute(fieldType,fieldTypeMultiple);
						//								if(prnAttFile != null && prnAttFile.ToString() != "")
						//								{
						//									DecPageEndoA95.SetAttribute(PrimPDF,prnAttFile.ToString());
						//									DecPageEndoA95.SetAttribute(PrimPDFBlocks,"1");
						//								}
						//								else
						//								{
						//									DecPageEndoA95.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA95"));
						//									DecPageEndoA95.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA95"));
						//								}
						//								DecPageEndoA95.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA95EXTN"));
						//								DecPageEndoA95.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA95EXTN"));
						//
						//								XmlElement EndoElementA95;
						//								EndoElementA95 = AcordPDFXML.CreateElement("EndoElementA95INFO");
						//								DecPageEndoA95.AppendChild(EndoElementA95);
						//								EndoElementA95.SetAttribute(fieldType,fieldTypeNormal);
						//								EndoElementA95.SetAttribute(id,AutoCtr.ToString());
						//							}
						//							#endregion
						//						}
						//							break;
						//
						//						case "PIPWR":
						//						{
						//							#region Dec Page Element
						//							if((stCode == "MI"))
						//							{
						//								XmlElement DecPageEndoA94;
						//								DecPageEndoA94 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA94");
						//								DecPageRootElement.AppendChild(DecPageEndoA94);
						//								DecPageEndoA94.SetAttribute(fieldType,fieldTypeMultiple);
						//								if(prnAttFile != null && prnAttFile.ToString() != "")
						//								{
						//									DecPageEndoA94.SetAttribute(PrimPDF,prnAttFile.ToString());
						//									DecPageEndoA94.SetAttribute(PrimPDFBlocks,"1");
						//								}
						//								else
						//								{
						//									DecPageEndoA94.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA94"));
						//									DecPageEndoA94.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA94"));
						//								}
						//								DecPageEndoA94.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA94EXTN"));
						//								DecPageEndoA94.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA94EXTN"));
						//
						//								XmlElement EndoElementA94;
						//								EndoElementA94 = AcordPDFXML.CreateElement("EndoElementA94INFO");
						//								DecPageEndoA94.AppendChild(EndoElementA94);
						//								EndoElementA94.SetAttribute(fieldType,fieldTypeNormal);
						//								EndoElementA94.SetAttribute(id,AutoCtr.ToString());
						//							}
						//							#endregion
						//						}
						//							break;
						//
						//						case "CBD":
						//						{
						//							#region Dec Page Element
						//							if((stCode == "MI"))
						//							{
						//								XmlElement DecPageEndoA91;
						//								DecPageEndoA91 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA91");
						//								DecPageRootElement.AppendChild(DecPageEndoA91);
						//								DecPageEndoA91.SetAttribute(fieldType,fieldTypeMultiple);
						//								if(prnAttFile != null && prnAttFile.ToString() != "")
						//								{
						//									DecPageEndoA91.SetAttribute(PrimPDF,prnAttFile.ToString());
						//									DecPageEndoA91.SetAttribute(PrimPDFBlocks,"1");
						//								}
						//								else
						//								{
						//									DecPageEndoA91.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA91"));
						//									DecPageEndoA91.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA91"));
						//								}
						//								DecPageEndoA91.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA91EXTN"));
						//								DecPageEndoA91.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA91EXTN"));
						//
						//								XmlElement EndoElementA91;
						//								EndoElementA91 = AcordPDFXML.CreateElement("EndoElementA91INFO");
						//								DecPageEndoA91.AppendChild(EndoElementA91);
						//								EndoElementA91.SetAttribute(fieldType,fieldTypeNormal);
						//								EndoElementA91.SetAttribute(id,AutoCtr.ToString());
						//							}
						//							#endregion
						//						}
						//							break;
						//						case "PETAS":
						//						{
						//							#region Dec Page Element
						//							if((stCode == "MI"))
						//							{
						//								XmlElement DecPageEndoA20;
						//								DecPageEndoA20 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA20");
						//								DecPageRootElement.AppendChild(DecPageEndoA20);
						//								DecPageEndoA20.SetAttribute(fieldType,fieldTypeMultiple);
						//								if(prnAttFile != null && prnAttFile.ToString() != "")
						//								{
						//									DecPageEndoA20.SetAttribute(PrimPDF,prnAttFile.ToString());
						//									DecPageEndoA20.SetAttribute(PrimPDFBlocks,"1");
						//								}
						//								else
						//								{
						//									DecPageEndoA20.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA20"));
						//									DecPageEndoA20.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA20"));
						//								}
						//								DecPageEndoA20.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA20EXTN"));
						//								DecPageEndoA20.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA20EXTN"));
						//
						//								XmlElement EndoElementA20;
						//								EndoElementA20 = AcordPDFXML.CreateElement("EndoElementA20INFO");
						//								DecPageEndoA20.AppendChild(EndoElementA20);
						//								EndoElementA20.SetAttribute(fieldType,fieldTypeNormal);
						//								EndoElementA20.SetAttribute(id,AutoCtr.ToString());
						//							}
						//							#endregion
						//						}
						//							break;
						//
						//						case "RCC":
						//						{
						//							#region Dec Page Element
						//							if((stCode == "MI"))
						//							{
						//								XmlElement DecPageEndoA68;
						//								DecPageEndoA68 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA68");
						//								DecPageRootElement.AppendChild(DecPageEndoA68);
						//								DecPageEndoA68.SetAttribute(fieldType,fieldTypeMultiple);
						//								if(prnAttFile != null && prnAttFile.ToString() != "")
						//								{
						//									DecPageEndoA68.SetAttribute(PrimPDF,prnAttFile.ToString());
						//									DecPageEndoA68.SetAttribute(PrimPDFBlocks,"1");
						//								}
						//								else
						//								{
						//									DecPageEndoA68.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA68"));
						//									DecPageEndoA68.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA68"));
						//								}
						//								DecPageEndoA68.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA68EXTN"));
						//								DecPageEndoA68.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA68EXTN"));
						//
						//								XmlElement EndoElementA68;
						//								EndoElementA68 = AcordPDFXML.CreateElement("EndoElementA68INFO");
						//								DecPageEndoA68.AppendChild(EndoElementA68);
						//								EndoElementA68.SetAttribute(fieldType,fieldTypeNormal);
						//								EndoElementA68.SetAttribute(id,AutoCtr.ToString());
						//							}
						//							#endregion
						//						}
						//							break;
						//
						//						case "ANCNI":
						//						{
						//							#region Dec Page Element
						//							if((stCode == "IN"))
						//							{
						//								XmlElement DecPageEndoA35;
						//								DecPageEndoA35 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA35");
						//								DecPageRootElement.AppendChild(DecPageEndoA35);
						//								DecPageEndoA35.SetAttribute(fieldType,fieldTypeMultiple);
						//								if(prnAttFile != null && prnAttFile.ToString() != "")
						//								{
						//									DecPageEndoA35.SetAttribute(PrimPDF,prnAttFile.ToString());
						//									DecPageEndoA35.SetAttribute(PrimPDFBlocks,"1");
						//								}
						//								else
						//								{
						//									DecPageEndoA35.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA35"));
						//									DecPageEndoA35.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA35"));
						//								}
						//								DecPageEndoA35.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA35EXTN"));
						//								DecPageEndoA35.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA35EXTN"));
						//
						//								XmlElement EndoElementA35;
						//								EndoElementA35 = AcordPDFXML.CreateElement("EndoElementA35INFO");
						//								DecPageEndoA35.AppendChild(EndoElementA35);
						//								EndoElementA35.SetAttribute(fieldType,fieldTypeNormal);
						//								EndoElementA35.SetAttribute(id,AutoCtr.ToString());
						//							}
						//							#endregion
						//						}
						//							break;
						//						case "LLGC":
						//						{
						//							#region Dec Page Element
						//							XmlElement DecPageEndoA11;
						//							DecPageEndoA11 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA11");
						//							DecPageRootElement.AppendChild(DecPageEndoA11);
						//							DecPageEndoA11.SetAttribute(fieldType,fieldTypeMultiple);
						//							if(prnAttFile != null && prnAttFile.ToString() != "")
						//							{
						//								DecPageEndoA11.SetAttribute(PrimPDF,prnAttFile.ToString());
						//								DecPageEndoA11.SetAttribute(PrimPDFBlocks,"1");
						//							}
						//							else
						//							{
						//								DecPageEndoA11.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA11"));
						//								DecPageEndoA11.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA11"));
						//							}
						//							DecPageEndoA11.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA11EXTN"));
						//							DecPageEndoA11.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA11EXTN"));
						//
						//							XmlElement EndoElementA11;
						//							EndoElementA11 = AcordPDFXML.CreateElement("EndoElementA11INFO");
						//							DecPageEndoA11.AppendChild(EndoElementA11);
						//							EndoElementA11.SetAttribute(fieldType,fieldTypeNormal);
						//							EndoElementA11.SetAttribute(id,AutoCtr.ToString());
						//							
						//							#endregion
						//						}
						//							break;
						//						case "EBCE":
						//						{
						//							XmlElement DecPageEndoA14;
						//							DecPageEndoA14 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA14");
						//							DecPageRootElement.AppendChild(DecPageEndoA14);
						//							DecPageEndoA14.SetAttribute(fieldType,fieldTypeMultiple);
						//							if(prnAttFile != null && prnAttFile.ToString() != "")
						//							{
						//								DecPageEndoA14.SetAttribute(PrimPDF,prnAttFile.ToString());
						//								DecPageEndoA14.SetAttribute(PrimPDFBlocks,"1");
						//							}
						//							else
						//							{
						//								DecPageEndoA14.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA14"));
						//								DecPageEndoA14.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA14"));
						//							}
						//							DecPageEndoA14.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA14EXTN"));
						//							DecPageEndoA14.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA14EXTN"));
						//
						//							XmlElement EndoElementA14;
						//							EndoElementA14 = AcordPDFXML.CreateElement("EndoElementA14INFO");
						//							DecPageEndoA14.AppendChild(EndoElementA14);
						//							EndoElementA14.SetAttribute(fieldType,fieldTypeNormal);
						//							EndoElementA14.SetAttribute(id,AutoCtr.ToString());
						//						}
						//							break;
						//						case "SORPE":
						//						{
						//							XmlElement DecPageEndoA29;
						//							DecPageEndoA29 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA29");
						//							DecPageRootElement.AppendChild(DecPageEndoA29);
						//							DecPageEndoA29.SetAttribute(fieldType,fieldTypeMultiple);
						//							if(prnAttFile != null && prnAttFile.ToString() != "")
						//							{
						//								DecPageEndoA29.SetAttribute(PrimPDF,prnAttFile.ToString());
						//								DecPageEndoA29.SetAttribute(PrimPDFBlocks,"1");
						//							}
						//							else
						//							{
						//								DecPageEndoA29.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA29"));
						//								DecPageEndoA29.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA29"));
						//							}
						//							DecPageEndoA29.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA29EXTN"));
						//							DecPageEndoA29.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA29EXTN"));
						//
						//							XmlElement EndoElementA29;
						//							EndoElementA29 = AcordPDFXML.CreateElement("EndoElementA29INFO");
						//							DecPageEndoA29.AppendChild(EndoElementA29);
						//							EndoElementA29.SetAttribute(fieldType,fieldTypeNormal);
						//							EndoElementA29.SetAttribute(id,AutoCtr.ToString());
						//						}
						//							break;
						//						case "SRTE":
						//						{
						//							XmlElement DecPageEndoA31;
						//							DecPageEndoA31 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA31");
						//							DecPageRootElement.AppendChild(DecPageEndoA31);
						//							DecPageEndoA31.SetAttribute(fieldType,fieldTypeMultiple);
						//							if(prnAttFile != null && prnAttFile.ToString() != "")
						//							{
						//								DecPageEndoA31.SetAttribute(PrimPDF,prnAttFile.ToString());
						//								DecPageEndoA31.SetAttribute(PrimPDFBlocks,"1");
						//							}
						//							else
						//							{
						//								DecPageEndoA31.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA31"));
						//								DecPageEndoA31.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA31"));
						//							}
						//							DecPageEndoA31.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA31EXTN"));
						//							DecPageEndoA31.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA31EXTN"));
						//
						//							XmlElement EndoElementA31;
						//							EndoElementA31 = AcordPDFXML.CreateElement("EndoElementA31INFO");
						//							DecPageEndoA31.AppendChild(EndoElementA31);
						//							EndoElementA31.SetAttribute(fieldType,fieldTypeNormal);
						//							EndoElementA31.SetAttribute(id,AutoCtr.ToString());
						//						}
						//							break;
						//						case "EECOMP":
						//						{
						//							#region Dec Page Element
						//							if((stCode == "MI"))
						//							{
						//								XmlElement DecPageEndoA15;
						//								DecPageEndoA15 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA15");
						//								DecPageRootElement.AppendChild(DecPageEndoA15);
						//								DecPageEndoA15.SetAttribute(fieldType,fieldTypeMultiple);
						//								if(prnAttFile != null && prnAttFile.ToString() != "")
						//								{
						//									DecPageEndoA15.SetAttribute(PrimPDF,prnAttFile.ToString());
						//									DecPageEndoA15.SetAttribute(PrimPDFBlocks,"1");
						//								}
						//								else
						//								{
						//									DecPageEndoA15.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA15"));
						//									DecPageEndoA15.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA15"));
						//								}
						//								DecPageEndoA15.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA15EXTN"));
						//								DecPageEndoA15.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA15EXTN"));
						//
						//								XmlElement EndoElementA15;
						//								EndoElementA15 = AcordPDFXML.CreateElement("EndoElementA15INFO");
						//								DecPageEndoA15.AppendChild(EndoElementA15);
						//								EndoElementA15.SetAttribute(fieldType,fieldTypeNormal);
						//								EndoElementA15.SetAttribute(id,AutoCtr.ToString());
						//							}
						//							else if((stCode == "IN"))
						//							{
						//								XmlElement DecPageEndoA16;
						//								DecPageEndoA16 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA16");
						//								DecPageRootElement.AppendChild(DecPageEndoA16);
						//								DecPageEndoA16.SetAttribute(fieldType,fieldTypeMultiple);
						//								if(prnAttFile != null && prnAttFile.ToString() != "")
						//								{
						//									DecPageEndoA16.SetAttribute(PrimPDF,prnAttFile.ToString());
						//									DecPageEndoA16.SetAttribute(PrimPDFBlocks,"1");
						//								}
						//								else
						//								{
						//									DecPageEndoA16.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA16"));
						//									DecPageEndoA16.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA16"));
						//								}
						//								DecPageEndoA16.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA16EXTN"));
						//								DecPageEndoA16.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA16EXTN"));
						//
						//								XmlElement EndoElementA16;
						//								EndoElementA16 = AcordPDFXML.CreateElement("EndoElementA16INFO");
						//								DecPageEndoA16.AppendChild(EndoElementA16);
						//								EndoElementA16.SetAttribute(fieldType,fieldTypeNormal);
						//								EndoElementA16.SetAttribute(id,AutoCtr.ToString());
						//							}
						//							#endregion
						//						}
						//							break;
						//						case "EECOLL":
						//						{
						//							#region Dec Page Element
						//							if((stCode == "MI"))
						//							{
						//								XmlElement DecPageEndoA15;
						//								DecPageEndoA15 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA15");
						//								DecPageRootElement.AppendChild(DecPageEndoA15);
						//								DecPageEndoA15.SetAttribute(fieldType,fieldTypeMultiple);
						//								if(prnAttFile != null && prnAttFile.ToString() != "")
						//								{
						//									DecPageEndoA15.SetAttribute(PrimPDF,prnAttFile.ToString());
						//									DecPageEndoA15.SetAttribute(PrimPDFBlocks,"1");
						//								}
						//								else
						//								{
						//									DecPageEndoA15.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA15"));
						//									DecPageEndoA15.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA15"));
						//								}
						//								DecPageEndoA15.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA15EXTN"));
						//								DecPageEndoA15.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA15EXTN"));
						//
						//								XmlElement EndoElementA15;
						//								EndoElementA15 = AcordPDFXML.CreateElement("EndoElementA15INFO");
						//								DecPageEndoA15.AppendChild(EndoElementA15);
						//								EndoElementA15.SetAttribute(fieldType,fieldTypeNormal);
						//								EndoElementA15.SetAttribute(id,AutoCtr.ToString());
						//							}
						//							#endregion
						//						}
						//							break;	
						//						case "ENO":
						//						{
						//							XmlElement DecPageEndoA35;
						//							DecPageEndoA35 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA35");
						//							DecPageRootElement.AppendChild(DecPageEndoA35);
						//							DecPageEndoA35.SetAttribute(fieldType,fieldTypeMultiple);
						//							if(prnAttFile != null && prnAttFile.ToString() != "")
						//							{
						//								DecPageEndoA35.SetAttribute(PrimPDF,prnAttFile.ToString());
						//								DecPageEndoA35.SetAttribute(PrimPDFBlocks,"1");
						//							}
						//							else
						//							{
						//								DecPageEndoA35.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA35"));
						//								DecPageEndoA35.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA35"));
						//							}
						//							DecPageEndoA35.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA35EXTN"));
						//							DecPageEndoA35.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA35EXTN"));
						//
						//							XmlElement EndoElementA35;
						//							EndoElementA35 = AcordPDFXML.CreateElement("EndoElementA35INFO");
						//							DecPageEndoA35.AppendChild(EndoElementA35);
						//							EndoElementA35.SetAttribute(fieldType,fieldTypeNormal);
						//							EndoElementA35.SetAttribute(id,AutoCtr.ToString());
						//						}
						//							break;
						//						case "HA":
						//						{
						//							XmlElement DecPageEndoA85;
						//							DecPageEndoA85 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA85");
						//							DecPageRootElement.AppendChild(DecPageEndoA85);
						//							DecPageEndoA85.SetAttribute(fieldType,fieldTypeMultiple);
						//							if(prnAttFile != null && prnAttFile.ToString() != "")
						//							{
						//								DecPageEndoA85.SetAttribute(PrimPDF,prnAttFile.ToString());
						//								DecPageEndoA85.SetAttribute(PrimPDFBlocks,"1");
						//							}
						//							else
						//							{
						//								DecPageEndoA85.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA85"));
						//								DecPageEndoA85.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA85"));
						//							}
						//							DecPageEndoA85.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA85EXTN"));
						//							DecPageEndoA85.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA85EXTN"));
						//
						//							XmlElement EndoElementA85;
						//							EndoElementA85 = AcordPDFXML.CreateElement("EndoElementA85INFO");
						//							DecPageEndoA85.AppendChild(EndoElementA85);
						//							EndoElementA85.SetAttribute(fieldType,fieldTypeNormal);
						//							EndoElementA85.SetAttribute(id,AutoCtr.ToString());
						//						}
						//							break;
						//						case "EBENO":
						//						{
						//							XmlElement DecPageEndoA80;
						//							DecPageEndoA80 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA80");
						//							DecPageRootElement.AppendChild(DecPageEndoA80);
						//							DecPageEndoA80.SetAttribute(fieldType,fieldTypeMultiple);
						//							if(prnAttFile != null && prnAttFile.ToString() != "")
						//							{
						//								DecPageEndoA80.SetAttribute(PrimPDF,prnAttFile.ToString());
						//								DecPageEndoA80.SetAttribute(PrimPDFBlocks,"1");
						//							}
						//							else
						//							{
						//								DecPageEndoA80.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA80"));
						//								DecPageEndoA80.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA80"));
						//							}
						//							DecPageEndoA80.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA80EXTN"));
						//							DecPageEndoA80.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA80EXTN"));
						//
						//							XmlElement EndoElementA80;
						//							EndoElementA80 = AcordPDFXML.CreateElement("EndoElementA80INFO");
						//							DecPageEndoA80.AppendChild(EndoElementA80);
						//							EndoElementA80.SetAttribute(fieldType,fieldTypeNormal);
						//							EndoElementA80.SetAttribute(id,AutoCtr.ToString());
						//						}
						//							break;
						//						case "RRUM":
						//						{
						//							XmlElement DecPageEndoA9;
						//							DecPageEndoA9 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA9");
						//							DecPageRootElement.AppendChild(DecPageEndoA9);
						//							DecPageEndoA9.SetAttribute(fieldType,fieldTypeMultiple);
						//							if(prnAttFile != null && prnAttFile.ToString() != "")
						//							{
						//								DecPageEndoA9.SetAttribute(PrimPDF,prnAttFile.ToString());
						//								DecPageEndoA9.SetAttribute(PrimPDFBlocks,"1");
						//							}
						//							else
						//							{
						//								DecPageEndoA9.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA9"));
						//								DecPageEndoA9.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA9"));
						//							}
						//							DecPageEndoA9.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA9EXTN"));
						//							DecPageEndoA9.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA9EXTN"));
						//
						//							XmlElement EndoElementA9;
						//							EndoElementA9 = AcordPDFXML.CreateElement("EndoElementA9INFO");
						//							DecPageEndoA9.AppendChild(EndoElementA9);
						//							EndoElementA9.SetAttribute(fieldType,fieldTypeNormal);
						//							EndoElementA9.SetAttribute(id,AutoCtr.ToString());
						//						}
						//							break;
						//						case "EBHA":
						//						{
						//							XmlElement DecPageEndoA85;
						//							DecPageEndoA85 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA85");
						//							DecPageRootElement.AppendChild(DecPageEndoA85);
						//							DecPageEndoA85.SetAttribute(fieldType,fieldTypeMultiple);
						//							if(prnAttFile != null && prnAttFile.ToString() != "")
						//							{
						//								DecPageEndoA85.SetAttribute(PrimPDF,prnAttFile.ToString());
						//								DecPageEndoA85.SetAttribute(PrimPDFBlocks,"1");
						//							}
						//							else
						//							{
						//								DecPageEndoA85.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA85"));
						//								DecPageEndoA85.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA85"));
						//							}
						//							DecPageEndoA85.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA85EXTN"));
						//							DecPageEndoA85.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA85EXTN"));
						//
						//							XmlElement EndoElementA85;
						//							EndoElementA85 = AcordPDFXML.CreateElement("EndoElementA85INFO");
						//							DecPageEndoA85.AppendChild(EndoElementA85);
						//							EndoElementA85.SetAttribute(fieldType,fieldTypeNormal);
						//							EndoElementA85.SetAttribute(id,AutoCtr.ToString());
						//
						//						}
						//							break;
						//						case "CAB91":
						//						{
						//							XmlElement DecPageEndoA91;
						//							DecPageEndoA91 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA91");
						//							DecPageRootElement.AppendChild(DecPageEndoA91);
						//							DecPageEndoA91.SetAttribute(fieldType,fieldTypeMultiple);
						//							if(prnAttFile != null && prnAttFile.ToString() != "")
						//							{
						//								DecPageEndoA91.SetAttribute(PrimPDF,prnAttFile.ToString());
						//								DecPageEndoA91.SetAttribute(PrimPDFBlocks,"1");
						//							}
						//							else
						//							{
						//								DecPageEndoA91.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA91"));
						//								DecPageEndoA91.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA91"));
						//							}
						//							DecPageEndoA91.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA91EXTN"));
						//							DecPageEndoA91.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA91EXTN"));
						//
						//							XmlElement EndoElementA91;
						//							EndoElementA91 = AcordPDFXML.CreateElement("EndoElementA91INFO");
						//							DecPageEndoA91.AppendChild(EndoElementA91);
						//							EndoElementA91.SetAttribute(fieldType,fieldTypeNormal);
						//							EndoElementA91.SetAttribute(id,AutoCtr.ToString());
						//						}
						//							break;
						//						case "OTC":
						//						{												
						//						}
						//							break;
						//						case "RCC68":
						//						{
						//							#region Dec Page Element
						//							if((stCode == "MI"))
						//							{
						//								XmlElement DecPageEndoRCC68;
						//								DecPageEndoRCC68 = AcordPDFXML.CreateElement("AUTOENDORSEMENTRCC68");
						//								DecPageRootElement.AppendChild(DecPageEndoRCC68);
						//								DecPageEndoRCC68.SetAttribute(fieldType,fieldTypeMultiple);
						//								if(prnAttFile != null && prnAttFile.ToString() != "")
						//								{
						//									DecPageEndoRCC68.SetAttribute(PrimPDF,prnAttFile.ToString());
						//									DecPageEndoRCC68.SetAttribute(PrimPDFBlocks,"1");
						//								}
						//								else
						//								{
						//									DecPageEndoRCC68.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGERCC68"));
						//									DecPageEndoRCC68.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGERCC68"));
						//								}
						//								DecPageEndoRCC68.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGERCC68EXTN"));
						//								DecPageEndoRCC68.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGERCC68EXTN"));
						//
						//								XmlElement EndoElementRCC68;
						//								EndoElementRCC68 = AcordPDFXML.CreateElement("EndoElementRCC68INFO");
						//								DecPageEndoRCC68.AppendChild(EndoElementRCC68);
						//								EndoElementRCC68.SetAttribute(fieldType,fieldTypeNormal);
						//								EndoElementRCC68.SetAttribute(id,AutoCtr.ToString());
						//							}
						//							#endregion
						//						}
						//							break;
						//						case "DD25":
						//						{
						//							#region Dec Page Element
						//							XmlElement DecPageEndoA25;
						//							DecPageEndoA25 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA25");
						//							DecPageRootElement.AppendChild(DecPageEndoA25);
						//							DecPageEndoA25.SetAttribute(fieldType,fieldTypeMultiple);
						//							if(prnAttFile != null && prnAttFile.ToString() != "")
						//							{
						//								DecPageEndoA25.SetAttribute(PrimPDF,prnAttFile.ToString());
						//								DecPageEndoA25.SetAttribute(PrimPDFBlocks,"1");
						//							}
						//							else
						//							{
						//								DecPageEndoA25.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA25"));
						//								DecPageEndoA25.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA25"));
						//							}
						//							DecPageEndoA25.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA25EXTN"));
						//							DecPageEndoA25.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA25EXTN"));
						//
						//							XmlElement EndoElementA25;
						//							EndoElementA25 = AcordPDFXML.CreateElement("EndoElementA25INFO");
						//							DecPageEndoA25.AppendChild(EndoElementA25);
						//							EndoElementA25.SetAttribute(fieldType,fieldTypeNormal);
						//							EndoElementA25.SetAttribute(id,AutoCtr.ToString());
						//						}
						//							#endregion
						//					
						//												
						//						
						//							break;
						//						case "AMC49":
						//						{							
						//							XmlElement DecPageEndoA49;
						//							DecPageEndoA49 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA49");
						//							DecPageRootElement.AppendChild(DecPageEndoA49);
						//							DecPageEndoA49.SetAttribute(fieldType,fieldTypeMultiple);
						//							if(prnAttFile != null && prnAttFile.ToString() != "")
						//							{
						//								DecPageEndoA49.SetAttribute(PrimPDF,prnAttFile.ToString());
						//								DecPageEndoA49.SetAttribute(PrimPDFBlocks,"1");
						//							}
						//							else
						//							{
						//								DecPageEndoA49.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA49"));
						//								DecPageEndoA49.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA49"));
						//							}
						//							DecPageEndoA49.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA49EXTN"));
						//							DecPageEndoA49.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA49EXTN"));
						//
						//							XmlElement EndoElementA49;
						//							EndoElementA49 = AcordPDFXML.CreateElement("EndoElementA49INFO");
						//							DecPageEndoA49.AppendChild(EndoElementA49);
						//							EndoElementA49.SetAttribute(fieldType,fieldTypeNormal);
						//							EndoElementA49.SetAttribute(id,AutoCtr.ToString());
						//						}
						//							break;
						//						case "CC49":
						//						{
						//							XmlElement DecPageEndoA46;
						//							DecPageEndoA46 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA46");
						//							DecPageRootElement.AppendChild(DecPageEndoA46);
						//							DecPageEndoA46.SetAttribute(fieldType,fieldTypeMultiple);
						//							if(prnAttFile != null && prnAttFile.ToString() != "")
						//							{
						//								DecPageEndoA46.SetAttribute(PrimPDF,prnAttFile.ToString());
						//								DecPageEndoA46.SetAttribute(PrimPDFBlocks,"1");
						//							}
						//							else
						//							{
						//								DecPageEndoA46.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA46"));
						//								DecPageEndoA46.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA46"));
						//							}
						//							DecPageEndoA46.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA46EXTN"));
						//							DecPageEndoA46.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA46EXTN"));
						//
						//							XmlElement EndoElementA46;
						//							EndoElementA46 = AcordPDFXML.CreateElement("EndoElementA46INFO");
						//							DecPageEndoA46.AppendChild(EndoElementA46);
						//							EndoElementA46.SetAttribute(fieldType,fieldTypeNormal);
						//							EndoElementA46.SetAttribute(id,AutoCtr.ToString());
						//						}
						//							break;
						//						case "MHT22":
						//						{
						//							XmlElement DecPageEndoA22;
						//							DecPageEndoA22 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA22");
						//							DecPageRootElement.AppendChild(DecPageEndoA22);
						//							DecPageEndoA22.SetAttribute(fieldType,fieldTypeMultiple);
						//							if(prnAttFile != null && prnAttFile.ToString() != "")
						//							{
						//								DecPageEndoA22.SetAttribute(PrimPDF,prnAttFile.ToString());
						//								DecPageEndoA22.SetAttribute(PrimPDFBlocks,"1");
						//							}
						//							else
						//							{
						//								DecPageEndoA22.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA22"));
						//								DecPageEndoA22.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA22"));
						//							}
						//							DecPageEndoA22.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA22EXTN"));
						//							DecPageEndoA22.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA22EXTN"));
						//
						//							XmlElement EndoElementA22;
						//							EndoElementA22 = AcordPDFXML.CreateElement("EndoElementA22INFO");
						//							DecPageEndoA22.AppendChild(EndoElementA22);
						//							EndoElementA22.SetAttribute(fieldType,fieldTypeNormal);
						//							EndoElementA22.SetAttribute(id,AutoCtr.ToString());
						//						}
						//							break;
						//						case "PAP6":
						//						{							
						//							XmlElement DecPageEndoAB6;
						//							DecPageEndoAB6 = AcordPDFXML.CreateElement("AUTOENDORSEMENTAB6");
						//							DecPageRootElement.AppendChild(DecPageEndoAB6);
						//							DecPageEndoAB6.SetAttribute(fieldType,fieldTypeMultiple);
						//							if(prnAttFile != null && prnAttFile.ToString() != "")
						//							{
						//								DecPageEndoAB6.SetAttribute(PrimPDF,prnAttFile.ToString());
						//								DecPageEndoAB6.SetAttribute(PrimPDFBlocks,"1");
						//							}
						//							else
						//							{
						//								DecPageEndoAB6.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAB6"));
						//								DecPageEndoAB6.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAB6"));
						//							}
						//							DecPageEndoAB6.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEAB6EXTN"));
						//							DecPageEndoAB6.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAB6EXTN"));
						//
						//							XmlElement EndoElementAB6;
						//							EndoElementAB6 = AcordPDFXML.CreateElement("EndoElementAB6INFO");
						//							DecPageEndoAB6.AppendChild(EndoElementAB6);
						//							EndoElementAB6.SetAttribute(fieldType,fieldTypeNormal);
						//							EndoElementAB6.SetAttribute(id,AutoCtr.ToString());
						//						}
						//							break;
						//						case "TE90":
						//						{
						//						#region Dec Page Element
						//								XmlElement DecPageEndoA90;
						//								DecPageEndoA90 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA90");
						//								DecPageRootElement.AppendChild(DecPageEndoA90);
						//								DecPageEndoA90.SetAttribute(fieldType,fieldTypeMultiple);
						//								if(prnAttFile != null && prnAttFile.ToString() != "")
						//								{
						//									DecPageEndoA90.SetAttribute(PrimPDF,prnAttFile.ToString());
						//									DecPageEndoA90.SetAttribute(PrimPDFBlocks,"1");
						//								}
						//								else
						//								{
						//									DecPageEndoA90.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA90"));
						//									DecPageEndoA90.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA90"));
						//								}
						//								DecPageEndoA90.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA90EXTN"));
						//								DecPageEndoA90.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA90EXTN"));
						//
						//								XmlElement EndoElementA90;
						//								EndoElementA90 = AcordPDFXML.CreateElement("EndoElementA90INFO");
						//								DecPageEndoA90.AppendChild(EndoElementA90);
						//								EndoElementA90.SetAttribute(fieldType,fieldTypeNormal);
						//								EndoElementA90.SetAttribute(id,AutoCtr.ToString());
						//								#endregion
						//						}
						//							break;
						//						case "MEE":
						//						{
						//							#region Dec Page Element
						//							if((stCode == "MI"))
						//							{
						//								XmlElement DecPageEndoA15;
						//								DecPageEndoA15 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA15");
						//								DecPageRootElement.AppendChild(DecPageEndoA15);
						//								DecPageEndoA15.SetAttribute(fieldType,fieldTypeMultiple);
						//								if(prnAttFile != null && prnAttFile.ToString() != "")
						//								{
						//									DecPageEndoA15.SetAttribute(PrimPDF,prnAttFile.ToString());
						//									DecPageEndoA15.SetAttribute(PrimPDFBlocks,"1");
						//								}
						//								else
						//								{
						//									DecPageEndoA15.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA15"));
						//									DecPageEndoA15.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA15"));
						//								}
						//								DecPageEndoA15.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA15EXTN"));
						//								DecPageEndoA15.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA15EXTN"));
						//
						//								XmlElement EndoElementA15;
						//								EndoElementA15 = AcordPDFXML.CreateElement("EndoElementA15INFO");
						//								DecPageEndoA15.AppendChild(EndoElementA15);
						//								EndoElementA15.SetAttribute(fieldType,fieldTypeNormal);
						//								EndoElementA15.SetAttribute(id,AutoCtr.ToString());
						//							}
						//							#endregion
						//						}
						//							break;
						//						case "A10":
						//						{
						//							#region Dec Page Element
						//							if((stCode == "MI"))
						//							{
						//								XmlElement DecPageEndoA10;
						//								DecPageEndoA10 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA10");
						//								DecPageRootElement.AppendChild(DecPageEndoA10);
						//								DecPageEndoA10.SetAttribute(fieldType,fieldTypeMultiple);
						//								if(prnAttFile != null && prnAttFile.ToString() != "")
						//								{
						//									DecPageEndoA10.SetAttribute(PrimPDF,prnAttFile.ToString());
						//									DecPageEndoA10.SetAttribute(PrimPDFBlocks,"1");
						//								}
						//								else
						//								{
						//									DecPageEndoA10.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA10"));
						//									DecPageEndoA10.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA10"));
						//								}
						//								DecPageEndoA10.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA10EXTN"));
						//								DecPageEndoA10.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA10EXTN"));
						//
						//								XmlElement EndoElementA10;
						//								EndoElementA10 = AcordPDFXML.CreateElement("EndoElementA10INFO");
						//								DecPageEndoA10.AppendChild(EndoElementA10);
						//								EndoElementA10.SetAttribute(fieldType,fieldTypeNormal);
						//								EndoElementA10.SetAttribute(id,AutoCtr.ToString());
						//							}
						//							#endregion
						//						}
						//							break;
						//						case "EP95":
						//						{
						//							#region Dec Page Element
						//							if((stCode == "IN"))
						//							{
						//								XmlElement DecPageEndoA96;
						//								DecPageEndoA96 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA96");
						//								DecPageRootElement.AppendChild(DecPageEndoA96);
						//								DecPageEndoA96.SetAttribute(fieldType,fieldTypeMultiple);
						//								if(prnAttFile != null && prnAttFile.ToString() != "")
						//								{
						//									DecPageEndoA96.SetAttribute(PrimPDF,prnAttFile.ToString());
						//									DecPageEndoA96.SetAttribute(PrimPDFBlocks,"1");
						//								}
						//								else
						//								{
						//									DecPageEndoA96.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA96"));
						//									DecPageEndoA96.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA96"));
						//								}
						//								DecPageEndoA96.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA96EXTN"));
						//								DecPageEndoA96.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA96EXTN"));
						//
						//								XmlElement EndoElementA96;
						//								EndoElementA96 = AcordPDFXML.CreateElement("EndoElementA96INFO");
						//								DecPageEndoA96.AppendChild(EndoElementA96);
						//								EndoElementA96.SetAttribute(fieldType,fieldTypeNormal);
						//								EndoElementA96.SetAttribute(id,AutoCtr.ToString());
						//							}
						//							else if((stCode == "MI"))
						//							{
						//								XmlElement DecPageEndoA95;
						//								DecPageEndoA95 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA95");
						//								DecPageRootElement.AppendChild(DecPageEndoA95);
						//								DecPageEndoA95.SetAttribute(fieldType,fieldTypeMultiple);
						//								if(prnAttFile != null && prnAttFile.ToString() != "")
						//								{
						//									DecPageEndoA95.SetAttribute(PrimPDF,prnAttFile.ToString());
						//									DecPageEndoA95.SetAttribute(PrimPDFBlocks,"1");
						//								}
						//								else
						//								{
						//									DecPageEndoA95.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA95"));
						//									DecPageEndoA95.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA95"));
						//								}
						//								DecPageEndoA95.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA95EXTN"));
						//								DecPageEndoA95.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA95EXTN"));
						//
						//								XmlElement EndoElementA95;
						//								EndoElementA95 = AcordPDFXML.CreateElement("EndoElementA95INFO");
						//								DecPageEndoA95.AppendChild(EndoElementA95);
						//								EndoElementA95.SetAttribute(fieldType,fieldTypeNormal);
						//								EndoElementA95.SetAttribute(id,AutoCtr.ToString());
						//							}
						//							#endregion
						//						}
						//							break;									
						//						case "TE92":
						//						{
						//						}
						//							break;
						//						case "TAP7":
						//						{
						//							XmlElement DecPageEndoAB7;
						//							DecPageEndoAB7 = AcordPDFXML.CreateElement("AUTOENDORSEMENTAB7");
						//							DecPageRootElement.AppendChild(DecPageEndoAB7);
						//							DecPageEndoAB7.SetAttribute(fieldType,fieldTypeMultiple);
						//							if(prnAttFile != null && prnAttFile.ToString() != "")
						//							{
						//								DecPageEndoAB7.SetAttribute(PrimPDF,prnAttFile.ToString());
						//								DecPageEndoAB7.SetAttribute(PrimPDFBlocks,"1");
						//							}
						//							else
						//							{
						//								DecPageEndoAB7.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAB7"));
						//								DecPageEndoAB7.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAB7"));
						//							}
						//							DecPageEndoAB7.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEAB7EXTN"));
						//							DecPageEndoAB7.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAB7EXTN"));
						//
						//							XmlElement EndoElementAB7;
						//							EndoElementAB7 = AcordPDFXML.CreateElement("EndoElementAB7INFO");
						//							DecPageEndoAB7.AppendChild(EndoElementAB7);
						//							EndoElementAB7.SetAttribute(fieldType,fieldTypeNormal);
						//							EndoElementAB7.SetAttribute(id,AutoCtr.ToString());
						//						}
						//							break;
						//						case "PIP94":
						//						{
						//							XmlElement DecPageEndoA94;
						//							DecPageEndoA94 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA94");
						//							DecPageRootElement.AppendChild(DecPageEndoA94);
						//							DecPageEndoA94.SetAttribute(fieldType,fieldTypeMultiple);
						//							if(prnAttFile != null && prnAttFile.ToString() != "")
						//							{
						//								DecPageEndoA94.SetAttribute(PrimPDF,prnAttFile.ToString());
						//								DecPageEndoA94.SetAttribute(PrimPDFBlocks,"1");
						//							}
						//							else
						//							{
						//								DecPageEndoA94.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA94"));
						//								DecPageEndoA94.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA94"));
						//							}
						//							DecPageEndoA94.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA94EXTN"));
						//							DecPageEndoA94.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA94EXTN"));
						//
						//							XmlElement EndoElementA94;
						//							EndoElementA94 = AcordPDFXML.CreateElement("EndoElementA94INFO");
						//							DecPageEndoA94.AppendChild(EndoElementA94);
						//							EndoElementA94.SetAttribute(fieldType,fieldTypeNormal);
						//							EndoElementA94.SetAttribute(id,AutoCtr.ToString());
						//						}
						//							break;
						//						case "SPA8":
						//						{
						//							#region Dec Page Element
						//							if((stCode == "IN"))
						//							{
						//								XmlElement DecPageEndoA8;
						//								DecPageEndoA8 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA8");
						//								DecPageRootElement.AppendChild(DecPageEndoA8);
						//								DecPageEndoA8.SetAttribute(fieldType,fieldTypeMultiple);
						//								if(prnAttFile != null && prnAttFile.ToString() != "")
						//								{
						//									DecPageEndoA8.SetAttribute(PrimPDF,prnAttFile.ToString());
						//									DecPageEndoA8.SetAttribute(PrimPDFBlocks,"1");
						//								}
						//								else
						//								{
						//									DecPageEndoA8.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA8"));
						//									DecPageEndoA8.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA8"));
						//								}
						//								DecPageEndoA8.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA8EXTN"));
						//								DecPageEndoA8.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA8EXTN"));
						//
						//								XmlElement EndoElementA8;
						//								EndoElementA8 = AcordPDFXML.CreateElement("EndoElementA8INFO");
						//								DecPageEndoA8.AppendChild(EndoElementA8);
						//								EndoElementA8.SetAttribute(fieldType,fieldTypeNormal);
						//								EndoElementA8.SetAttribute(id,AutoCtr.ToString());
						//							}
						//							if((stCode == "MI"))
						//							{
						//								XmlElement DecPageEndoA64;
						//								DecPageEndoA64 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA64");
						//								DecPageRootElement.AppendChild(DecPageEndoA64);
						//								DecPageEndoA64.SetAttribute(fieldType,fieldTypeMultiple);
						//								if(prnAttFile != null && prnAttFile.ToString() != "")
						//								{
						//									DecPageEndoA64.SetAttribute(PrimPDF,prnAttFile.ToString());
						//									DecPageEndoA64.SetAttribute(PrimPDFBlocks,"1");
						//								}
						//								else
						//								{
						//									DecPageEndoA64.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA64"));
						//									DecPageEndoA64.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA64"));
						//								}
						//								DecPageEndoA64.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA64EXTN"));
						//								DecPageEndoA64.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA64EXTN"));
						//
						//								XmlElement EndoElementA64;
						//								EndoElementA64 = AcordPDFXML.CreateElement("EndoElementA64INFO");
						//								DecPageEndoA64.AppendChild(EndoElementA64);
						//								EndoElementA64.SetAttribute(fieldType,fieldTypeNormal);
						//								EndoElementA64.SetAttribute(id,AutoCtr.ToString());
						//							}
						//							#endregion												
						//						}
						//							break;
						//						case "SA44":
						//						{
						//							XmlElement DecPageEndoSA44;
						//							DecPageEndoSA44 = AcordPDFXML.CreateElement("AUTOENDORSEMENTSA44");
						//							DecPageRootElement.AppendChild(DecPageEndoSA44);
						//							DecPageEndoSA44.SetAttribute(fieldType,fieldTypeMultiple);
						//							if(prnAttFile != null && prnAttFile.ToString() != "")
						//							{
						//								DecPageEndoSA44.SetAttribute(PrimPDF,prnAttFile.ToString());
						//								DecPageEndoSA44.SetAttribute(PrimPDFBlocks,"1");
						//							}
						//							else
						//							{
						//								DecPageEndoSA44.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESA44"));
						//								DecPageEndoSA44.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESA44"));
						//							}
						//							DecPageEndoSA44.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESA44EXTN"));
						//							DecPageEndoSA44.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESA44EXTN"));
						//
						//							XmlElement EndoElementSA44;
						//							EndoElementSA44 = AcordPDFXML.CreateElement("EndoElementSA44INFO");
						//							DecPageEndoSA44.AppendChild(EndoElementSA44);
						//							EndoElementSA44.SetAttribute(fieldType,fieldTypeNormal);
						//							EndoElementSA44.SetAttribute(id,AutoCtr.ToString());
						//						}
						//							break;
						//						case "RU":
						//						{
						//							#region Dec Page Element
						//							if((stCode == "IN"))
						//							{
						//								XmlElement DecPageEndoA9;
						//								DecPageEndoA9 = AcordPDFXML.CreateElement("AUTOENDORSEMENTA9");
						//								DecPageRootElement.AppendChild(DecPageEndoA9);
						//								DecPageEndoA9.SetAttribute(fieldType,fieldTypeMultiple);
						//								if(prnAttFile != null && prnAttFile.ToString() != "")
						//								{
						//									DecPageEndoA9.SetAttribute(PrimPDF,prnAttFile.ToString());
						//									DecPageEndoA9.SetAttribute(PrimPDFBlocks,"1");
						//								}
						//								else
						//								{
						//									DecPageEndoA9.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEA9"));
						//									DecPageEndoA9.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA9"));
						//								}
						//								DecPageEndoA9.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEA9EXTN"));
						//								DecPageEndoA9.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEA9EXTN"));
						//
						//								XmlElement EndoElementA9;
						//								EndoElementA9 = AcordPDFXML.CreateElement("EndoElementA9INFO");
						//								DecPageEndoA9.AppendChild(EndoElementA9);
						//								EndoElementA9.SetAttribute(fieldType,fieldTypeNormal);
						//								EndoElementA9.SetAttribute(id,AutoCtr.ToString());
						//							}
						//							#endregion
						//												
						//						}
						//							break;*/
						//							#endregion
						//						case "MCT":
						//						{
						//						}
						//							break;										
						//						default:
						//						{
						#endregion	

						XmlElement DecPageAutoEndoPP;
						XmlElement EndoElementPPA;
						if(BlankPagePrinted==0 && inttotalpage!=0)
						{
							DecPageAutoEndoPP = AcordPDFXML.CreateElement("AUTOENDORSEMENTPP" + "_" + 0);
							DecPageRootElement.AppendChild(DecPageAutoEndoPP);
							DecPageAutoEndoPP.SetAttribute(fieldType,fieldTypeMultiple);

							DecPageAutoEndoPP.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEBLANKDOCUMENT"));
							DecPageAutoEndoPP.SetAttribute(PrimPDFBlocks,"1");

							DecPageAutoEndoPP.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEBLANKDOCUMENT"));
							DecPageAutoEndoPP.SetAttribute(SecondPDFBlocks,"1");

						
							EndoElementPPA = AcordPDFXML.CreateElement("ENDOELEMENTPPAINFO" + "_" + 0);
							DecPageAutoEndoPP.AppendChild(EndoElementPPA);
							EndoElementPPA.SetAttribute(fieldType,fieldTypeNormal);
							EndoElementPPA.SetAttribute(id,"0");
							//intCnt++;
							BlankPagePrinted++;
						}
						
						if(inttotalpage!=0)
						{
							Cntrl=intCnt+1;
							DecPageAutoEndoPP = AcordPDFXML.CreateElement("AUTOENDORSEMENTPP" + "_" + Cntrl);
							DecPageRootElement.AppendChild(DecPageAutoEndoPP);
							DecPageAutoEndoPP.SetAttribute(fieldType,fieldTypeMultiple);

							DecPageAutoEndoPP.SetAttribute(PrimPDF,prnAttFile);
							DecPageAutoEndoPP.SetAttribute(PrimPDFBlocks,"1");

							DecPageAutoEndoPP.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEDPWORDEXTN"));
							DecPageAutoEndoPP.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDPWORDEXTN"));

						
							EndoElementPPA = AcordPDFXML.CreateElement("ENDOELEMENTPPAINFO" + "_" + Cntrl);
							DecPageAutoEndoPP.AppendChild(EndoElementPPA);
							EndoElementPPA.SetAttribute(fieldType,fieldTypeNormal);
							EndoElementPPA.SetAttribute(id,"0");
							intCnt++;
						}
						else
						{
							DecPageAutoEndoPP = AcordPDFXML.CreateElement("AUTOENDORSEMENTPP" + "_" + intCnt);
							DecPageRootElement.AppendChild(DecPageAutoEndoPP);
							DecPageAutoEndoPP.SetAttribute(fieldType,fieldTypeMultiple);

							DecPageAutoEndoPP.SetAttribute(PrimPDF,prnAttFile);
							DecPageAutoEndoPP.SetAttribute(PrimPDFBlocks,"1");

							DecPageAutoEndoPP.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEDPWORDEXTN"));
							DecPageAutoEndoPP.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDPWORDEXTN"));

						
							EndoElementPPA = AcordPDFXML.CreateElement("ENDOELEMENTPPAINFO" + "_" + intCnt);
							DecPageAutoEndoPP.AppendChild(EndoElementPPA);
							EndoElementPPA.SetAttribute(fieldType,fieldTypeNormal);
							EndoElementPPA.SetAttribute(id,"0");
							intCnt++;
						}
						//						}
						//							break;
						//
						//					}	
					}					
					#endregion
					counter++;
				}
			}

		}

		#endregion Endorsement Wordings

		#region Addition Wordings
		private void createAddWordingsXML()
		{
			//string lob_id="2";
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
				if(gStrProcessID != null && gStrProcessID != ""  && gStrProcessID != "0")
				{
//					DataSet DSAddWordSet = new DataSet();
//					objDataWrapper.AddParameter("@STATE_ID",state_id);
//					objDataWrapper.AddParameter("@LOB_ID",lob_id);
//					objDataWrapper.AddParameter("@PROCESS_ID",gStrProcessID);
//					DSAddWordSet = objDataWrapper.ExecuteDataSet("Proc_GetAddWordingsData");
//					objDataWrapper.ClearParameteres();
					//DSAddWordSet = objDataWrapper.ExecuteDataSet("Proc_GetAddWordingsData " + state_id + "," + lob_id + "," + gStrProcessID);
			
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

						DecWordingsInfoElement.InnerXml = DecWordingsInfoElement.InnerXml +  "<Wordings_text " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSAddWordSet.Tables[0].Rows[0]["PDF_WORDINGS"].ToString()) + "</Wordings_text>";
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
			intPrivacyPage = 1;
			XmlElement DecPageInfoElement;
			DecPageInfoElement = AcordPDFXML.CreateElement("PAGE2INFO");
			DecAddPageElement.AppendChild(DecPageInfoElement);
			DecPageInfoElement.SetAttribute(fieldType,fieldTypeNormal);
			DecPageInfoElement.SetAttribute(id,"0");
			if(strInsuScore =="-2")
			{
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_reas " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</fcra_reas>";
				DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_reas_1_0 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</fcra_reas_1_0>";
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
			intPrivacyPage = 1;
			XmlElement DecPageInfoElement;
			DecPageInfoElement = AcordPDFXML.CreateElement("PAGE2INFO");
			DecAddPageElement.AppendChild(DecPageInfoElement);
			DecPageInfoElement.SetAttribute(fieldType,fieldTypeNormal);
			DecPageInfoElement.SetAttribute(id,"0");
						
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_hd1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</fcra_hd1>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_hd2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</fcra_hd2>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</fcra_txt>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_reas " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</fcra_reas>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_reas_1_0 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</fcra_reas_1_0>";
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
			intPrivacyPage = 1;
			XmlElement DecPageInfoElement;
			DecPageInfoElement = AcordPDFXML.CreateElement("PAGE2INFO");
			DecAddPageElement.AppendChild(DecPageInfoElement);
			DecPageInfoElement.SetAttribute(fieldType,fieldTypeNormal);
			DecPageInfoElement.SetAttribute(id,"0");
//			DataSet dsTempPolicy = new DataSet();
//			objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//			objDataWrapper.AddParameter("@POLID",gStrPolicyId);
//			objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//			objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//			dsTempPolicy = objDataWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
//			objDataWrapper.ClearParameteres();
			//dsTempPolicy = objDataWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			strAdverseRep = DSTempApplicantDataSet.Tables[1].Rows[0]["ADVERSE_LETTER_REQD"].ToString();
			
			
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
			intPrivacyPage = 1;
			XmlElement DecPageInfoElement;
			DecPageInfoElement = AcordPDFXML.CreateElement("PAGE2INFO");
			DecAddPageElement.AppendChild(DecPageInfoElement);
			DecPageInfoElement.SetAttribute(fieldType,fieldTypeNormal);
			DecPageInfoElement.SetAttribute(id,"0");
//			DataSet dsTempPolicy = new DataSet();
//			objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//			objDataWrapper.AddParameter("@POLID",gStrPolicyId);
//			objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//			objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//			dsTempPolicy = objDataWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
//			objDataWrapper.ClearParameteres();
			//dsTempPolicy = objDataWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			strAdverseRep = DSTempApplicantDataSet.Tables[1].Rows[0]["ADVERSE_LETTER_REQD"].ToString();
			
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
				
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_reas " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</fcra_reas>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_reas_1_0 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("") + "</fcra_reas_1_0>";
			
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
		private string ChkPreInsuScr(DataWrapper objDataWrapper)
		{
			objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			objDataWrapper.AddParameter("@POLID",gStrPolicyId);
			objDataWrapper.AddParameter("@VERSIONID",goldVewrsionId);
			objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DataSet dsoldInsuScr = objDataWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
			objDataWrapper.ClearParameteres();
			//DataSet dsoldInsuScr = objDataWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + goldVewrsionId + ",'" + gStrCalledFrom + "'");
//			objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//			objDataWrapper.AddParameter("@POLID",gStrPolicyId);
//			objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//			objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//			DataSet dsnewInsuScr = objDataWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
//			objDataWrapper.ClearParameteres();
			//DataSet dsnewInsuScr = objDataWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			newInsuScr = DSTempApplicantDataSet.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString();
			oldInsuScr = dsoldInsuScr.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString();
			if(Convert.ToDouble(DSTempApplicantDataSet.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString()) < Convert.ToDouble(dsoldInsuScr.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString()))
			{
				strNeedPage2 ="Y";
				return strNeedPage2;
			}
			else if(Convert.ToDouble(DSTempApplicantDataSet.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString()) >= Convert.ToDouble(dsoldInsuScr.Tables[0].Rows[0]["APPLY_INSURANCE_SCORE"].ToString()))
			{
				strNeedPage2 ="N";
				return strNeedPage2;
			}
			else
			{
				return strNeedPage2;
			}
		}
		private void PrintEndorsement()
		{
			DecPageRootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
			AcordPDFXML.SelectSingleNode(RootElement).AppendChild(DecPageRootElement);

			DecPageRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGE"));

			prnOrd_covCode = new string[50];
			prnOrd_attFile = new string[50];
			prnOrd = new int[50];
			DataSet DSTempVehicle = new DataSet();
			foreach(DataRow AutoDetail in DSTempAutoDetailDataSet.Tables[0].Rows)
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				objDataWrapper.AddParameter("@POLID",gStrPolicyId);
				objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				objDataWrapper.AddParameter("@VEHICLEID",AutoDetail["VEHICLE_ID"]);
				objDataWrapper.AddParameter("@RISKTYPE",AutoDetail["RISKTYPE"]);
				objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempVehicle = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_RiskWise_Coverage_Details");
				objDataWrapper.ClearParameteres();
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
		}
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
		#endregion


	}
}

