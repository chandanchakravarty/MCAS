using System;
using System.Xml;
using System.Data;
using Cms.DataLayer;
using System.Collections;
using System.Text;
namespace Cms.BusinessLayer.BlProcess
{
	/// <summary>
	/// <CreatedBy>Deepak Gupta</CreatedBy>
	/// <Dated>26-June-2006</Dated>
	/// <Purpose>To Create XML for Acord80 PDF for HOME LOB</Purpose>
	/// </summary>
	public class ClsHomePdfXML : ClsCommonPdf
	{
		#region Declarations
		private XmlElement DecPageRootElement;
		private XmlElement Acord80RootElement;
		private XmlElement Acord81RootElement;
		private XmlElement Acord73RootElement;
		private XmlElement Acord82RootElement;
		private XmlElement SupplementalRootElement;
		private Hashtable htpremium=new Hashtable(); 
		private Hashtable htpremium_dis=new Hashtable(); 
		private Hashtable htpremium_sur=new Hashtable(); 

		
		private   DataSet DSTempPolicyDataSet= new DataSet() ;		
		private   DataSet DSTempApplicantDataSet = new DataSet();
		private   DataSet DstempDocument = new DataSet();
		private   DataSet DSAddWordSet = new DataSet();
		private   DataSet DSTempDwellingDetailDataSet = new DataSet();
		private   DataSet DSTempPriorLossDataSet = new DataSet();
		private   DataSet DSTempRateDataSet = new DataSet();
		private   DataSet DSTempAddIntrst = new DataSet();
		private   DataSet DSTempOperators = new DataSet();

		string lstrGetPremium="0";
		//double dblSumTotal=0;
		double sumtotal=0;
		string gstrGetPremium="0";
		string goldVewrsionId="0";
		int gintGetindex=0;
		string getPhotoAttach;
		string getMarineSurvey;
		string gstrBoatTerritory;
		//bool gCtrlnChkHO65=false;
		private DataWrapper gobjWrapper;
		private string stCode="";
		private string strrecdPremium="";
		private string strOldPolicyVer="";
		private double dblOldSum=0;
		private string strInsScore="",strInsType="";
		private string strInsuScore="";
		private string expiry_date = "";
		private string eff_date = "";
		string NamedInsuredWithSuffix="";
		//private string strAppAddress1,strAppAddress2,strAppCity,strAppState,strAppZip;
		private string strRVLiabLim = "", strRVMedLim = "";
		private string inspercent = "";
		private string []prnOrd_covCode;
		private string []prnOrd_attFile;
		private int []prnOrd;
		int flgError=0;
		string strModXml="";
		private  string outXml;
		private const int UnattachedInclude=1500;
		string newInsuScr="";
		string oldInsuScr="";
		private int schEquip = 0, noBoats=0, intschEquip=0;
		int lintGetindex=0;
		//int intPrivacyPage = 0;
		private string ApplicantName1 = "";
		int intPrivacyPage = 0;
		#endregion
		 
		#region Constructor
		public ClsHomePdfXML(string lstrClientId,string lstrPolicyId,string lstrVersionId,string lStrCalledFrom,string lStrCalledFor,string stateCode,string strProcessID,string Agn_Ins,string temp,DataWrapper objWrapper,string loutXml,DataSet lDSTempPolicyDataSet,DataSet lDSTempApplicantDataSet,DataSet lDstempDocument, DataSet lDSAddWordSet,DataSet lDSTempAutoDetailDataSet,DataSet lDSTempRateDataSet,DataSet lDSTempPriorLossDataSet,DataSet lDSTempAddIntrst,DataSet lDSTempOperators)
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
			gStrtemp=temp;
			outXml = loutXml;
			DSTempDataSet = new DataSet();
			//gobjWrapper = new DataWrapper(ConnStr,CommandType.Text);
			this.gobjWrapper=objWrapper;
			DSTempPolicyDataSet=lDSTempPolicyDataSet.Copy();
			DSTempApplicantDataSet=lDSTempApplicantDataSet.Copy();
			DstempDocument=lDstempDocument.Copy();
			DSAddWordSet=lDSAddWordSet.Copy();
			DSTempDwellingDetailDataSet=lDSTempAutoDetailDataSet.Copy();
			DSTempPriorLossDataSet=lDSTempPriorLossDataSet.Copy();
			DSTempRateDataSet=lDSTempRateDataSet.Copy();
			DSTempAddIntrst=lDSTempAddIntrst.Copy();
			DSTempOperators=lDSTempOperators.Copy();
			//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFPolicyDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			if(DSTempPolicyDataSet.Tables[0].Rows.Count>0)
			{
				SetPDFVersionLobNode("HOME",DateTime.Parse(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
				SetPDFInsScoresLobNode("HOME",DateTime.Parse(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()));
			}
		}
		#endregion

		public string getHomeAcordPDFXml()
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
		public string getAcordPDFXml()
		{
			AcordPDFXML = new XmlDocument();
			AcordPDFXML.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <" + RootElement + "/>");

			if(gStrCopyTo != "AGENCY")
			{
				creatmaillingpage();
			}
			if(gStrtemp!="final")
			{
				LoadRateXML("HOME",DSTempRateDataSet);
			}
			createRootElementForAllRootPDFs();
			//LoadRateXML();
			FillMonth();

			//creating Xml From Here
			CreatePolicyAgencyXML();
			CreateNamedInsuredCoAppXml();
			CreatePriorPolicyCoverage();
			CreatePriorLossXml();
			createDwellingXML();
			createAcord80HomeAddlIntXml();
			createDwellingUnderwritingGeneralXML();
			CreateScheduleArticlesXML();
			CreateRVXML();
			CreateGeneralInfoXML();
			/*
				createWaterCraftXml();
				createBoatXML();
				createAcord82BoatAddlIntXml();
				createAcord82OperatorXML();
				createAcord82OperatorExpViolationXML();
				createBoatUnderwritingGeneralXML();
				*/
			if(gStrtemp!="final")
			{
				LoadRateXML("HOME-BOAT",gobjWrapper);
			}
			createBoatXML();
			createAcord82BoatAddlIntXml();
			createAcord82OperatorXML();
			createAcord82OperatorExpViolationXML();
			createBoatUnderwritingGeneralXML();		
			CreateSolidFuelXml();
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
			if(gStrCopyTo == "CUSTOMER")
				createAddWordingsXML();
            //Code to Insert XML By Chetna on March 12,10
            InsertCustomerFullWordingXml(gStrClientID,gStrPolicyId,gStrPolicyVersion,gStrCopyTo,AcordPDFXML.OuterXml);
			return AcordPDFXML.OuterXml;

		}
		private string ModifiedXml(string strPdfXml)
		{
			try
			{
				if(gStrCopyTo=="AGENCY")
				{
					return	getAcordPDFXml();
				}
				else if(gStrCopyTo=="CUSTOMER-NOWORD")
				{
					return	getAcordPDFXml();
				}
				else
				{
					return	getAcordPDFXml();
				}
				return "";
			}
			catch (Exception Ex)
			{
				flgError=1;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(Ex);
				return getAcordPDFXml();
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
			strInsname=RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["APPNAME"].ToString())+ " " + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["SUFFIX"].ToString());
			strInsAdd=RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["APPADDRESS"].ToString());
			strcityzip=RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["APPCITYSTZIP"].ToString());
			
			if (gStrPdfFor == PDFForDecPage)
			{
				gobjWrapper.ClearParameteres();
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
						gobjWrapper.ClearParameteres();
						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						gobjWrapper.AddParameter("@LOB","AUTOP");
						DSTempRateDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFQuoteRateXML");
						gobjWrapper.ClearParameteres();
						//DSTempRateDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFQuoteRateXML " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "','" + "AUTOP" + "'");
						gStrPolicyVersion = oldPolicyVersion;
						// Load quote premium xml
						if(DSTempRateDataSet.Tables[0].Rows.Count>0)
						{
							LoadRateXML("HOME",DSTempRateDataSet);
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
						gobjWrapper.ClearParameteres();
						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",newVersion);
						DSTempDatasetBoatDetail = gobjWrapper.ExecuteDataSet("PROC_GetpdfAdjusted_Premium");
						gobjWrapper.ClearParameteres();
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
				gobjWrapper.ClearParameteres();
				gobjWrapper.AddParameter("@CUSTOMER_ID",strCustomerId);
				gobjWrapper.AddParameter("@POLICY_ID",strAppId);
				gobjWrapper.AddParameter("@POLICY_VERSION_ID",strAppVersionId);
				gobjWrapper.AddParameter("@CALLED_FOR",strCopyTo);
				gobjWrapper.AddParameter("@CUSTOMER_XML",strcutomerxml);
				gobjWrapper.ExecuteNonQuery("PROC_INSERTXMLFORPDF");				
			}			
			catch//(Exception ex)
			{
				//throw new Exception(ex.Message);
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
				Acord80RootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(Acord80RootElement);

				Acord80RootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD80"));
			
				Acord81RootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(Acord81RootElement);

				Acord81RootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD81"));

				Acord73RootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(Acord73RootElement);

				Acord73RootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD73"));

				Acord82RootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(Acord82RootElement);
				
				gobjWrapper.ClearParameteres();
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempDataSet = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS");
				gobjWrapper.ClearParameteres();

				if(DSTempDataSet.Tables[0].Rows.Count>0)
					Acord82RootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82"));

				SupplementalRootElement = AcordPDFXML.CreateElement(RootElementForAllPDF);
				AcordPDFXML.SelectSingleNode(RootElement).AppendChild(SupplementalRootElement);
				if(DSTempDataSet.Tables[0].Rows.Count>0)
					SupplementalRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENT"));
			}
		}
		#endregion

		#region Creating Policy And Agency Xml 
		private void CreatePolicyAgencyXML()
		{

			//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFPolicyDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			
			#region Global Variable Assignment
			PolicyNumber	= RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString());
			PolicyEffDate	= RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString());
			PolicyExpDate	= RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString());
			strOldPolicyVer = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
			expiry_date = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString());
			eff_date = DSTempPolicyDataSet.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString();
			if(gStrProcessID != null && gStrProcessID != "" && gStrProcessID != "0")
			{
				//DataSet DSAddWordSet = new DataSet();
				//DSAddWordSet = gobjWrapper.ExecuteDataSet("Proc_GetAddWordingsData " +  "0," +  "0," + gStrProcessID);
			
				if (DSAddWordSet.Tables.Count > 1 && DSAddWordSet.Tables[1].Rows.Count > 0)
					Reason		=	RemoveJunkXmlCharacters(DSAddWordSet.Tables[1].Rows[0]["PROCESS_DESC"].ToString());
			}
			else
				Reason			= RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["Reason"].ToString());

			if(Reason.Trim() != "" && DSTempPolicyDataSet.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString() != "")
				Reason += " / Effective Date: " + DSTempPolicyDataSet.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString();

			goldVewrsionId=DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString();
	//		CopyTo			= RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["COPY_TO"].ToString());
		//	if(Reason.Trim() != "" && DSTempPolicyDataSet.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString() != "")
		//		Reason += " / Effective Date: " + DSTempPolicyDataSet.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString();
			
			AgencyName = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString());
			AgencyAddress = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_ADD"].ToString());
			AgencyCitySTZip = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_CITYSTZIP"].ToString());
			AgencyPhoneNumber = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString());
			AgencyCode = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_CODE"].ToString());
			AgencySubCode = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["SUB_CODE"].ToString());
			AgencyBilling = RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["PAYMENTBILLING"].ToString())=="AB"?"Agency Bill":"Direct Bill" ;
			currTerm = int.Parse(DSTempPolicyDataSet.Tables[0].Rows[0]["CURRENT_TERM"].ToString());
			strrecdPremium=RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["RECEIVED_PRMIUM"].ToString());
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

			if (gStrPdfFor == PDFForAcord)
			{
				#region Acord80 Page
				XmlElement AcordPolicyElement;
				AcordPolicyElement = AcordPDFXML.CreateElement("POLICY");
				Acord80RootElement.AppendChild(AcordPolicyElement);
				AcordPolicyElement.SetAttribute(fieldType,fieldTypeSingle);
			
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(System.DateTime.Today.ToString("MM/dd/yyyy")) + "</DATE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</POLICYNUMBER>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<EFFECTIVEDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</EFFECTIVEDATE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<EXPIRATIONDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</EXPIRATIONDATE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<BINDEREFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</BINDEREFFDATE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<BINDEREXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</BINDEREXPDATE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<BINDERCVGNOTBOUND " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["BINDERCVGNOTBOUND"].ToString()) + "</BINDERCVGNOTBOUND>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<PAYMENTBILLING " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["PAYMENTBILLING"].ToString()) + "</PAYMENTBILLING>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<PAYMENTDIRECTBILL " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["PAYMENTDIRECTBILL"].ToString()) + "</PAYMENTDIRECTBILL>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<PAYMENTAPPBILL " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["PAYMENTAPPBILL"].ToString()) + "</PAYMENTAPPBILL>";
				if(DSTempPolicyDataSet.Tables[0].Rows[0]["YEAR_AT_CURR_RESI"].ToString() != "0")
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<DATECURRRESIDENCE " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["YEAR_AT_CURR_RESI"].ToString()) + "</DATECURRRESIDENCE>";
				if(DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_TYPE"].ToString()!="")
					AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<APPLICANTCOPLAN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Wolverine Mutual/" + DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_TYPE"].ToString()) + "</APPLICANTCOPLAN>";

				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<APPLICANTPREVIOUSADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("" + DSTempPolicyDataSet.Tables[0].Rows[0]["YEARS_AT_PREV_ADD"].ToString()) + "</APPLICANTPREVIOUSADDRESS>";
				//Agency
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</AGENCYNAME>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_ADD"].ToString()) + "</AGENCYADDRESS>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_CITYSTZIP"].ToString()) + "</AGENCYCITYSTATEZIP>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString()) + "</AGENCYPHONE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYFAX " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_FAX"].ToString()) + "</AGENCYFAX>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_CODE"].ToString()) + "</AGENCYCODE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["SUB_CODE"].ToString()) + "</AGENCYSUBCODE>";
				//ATTACHMENT INFO
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<ATTCHINLANDMARINE " + fieldType + "=\"" + fieldTypeText + "\"></ATTCHINLANDMARINE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<ATTCHREPLCOSTEST " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["PROPRTY_INSP_CREDIT"].ToString()) + "</ATTCHREPLCOSTEST>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<ATTCHPHOTOGRAPH " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["PIC_OF_LOC"].ToString()) + "</ATTCHPHOTOGRAPH>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<ATTACHSOLIDFUEL " + fieldType + "=\"" + fieldTypeText + "\"></ATTACHSOLIDFUEL>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<ATTACHPROTDEVICE " + fieldType + "=\"" + fieldTypeText + "\"></ATTACHPROTDEVICE>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<ATTACHRV " + fieldType + "=\"" + fieldTypeText + "\"></ATTACHRV>";
				AcordPolicyElement.InnerXml = AcordPolicyElement.InnerXml +  "<ATTACHBOAT " + fieldType + "=\"" + fieldTypeText + "\"></ATTACHBOAT>";

				#endregion

				#region Acord81 Page
				XmlElement Acord81PolicyElement;
				Acord81PolicyElement = AcordPDFXML.CreateElement("POLICY");
				Acord81RootElement.AppendChild(Acord81PolicyElement);
				Acord81PolicyElement.SetAttribute(fieldType,fieldTypeSingle);

				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(System.DateTime.Today.ToString("MM/dd/yyyy")) + "</DATE>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</POLICYNUMBER>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<EFFECTIVEDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</EFFECTIVEDATE>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<EXPIRATIONDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</EXPIRATIONDATE>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<BINDEREFFECTIVEDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</BINDEREFFECTIVEDATE>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<BINDEREXPIRATIONDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</BINDEREXPIRATIONDATE>";
				//				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<BINDEREFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["BINDEREFFDATE"].ToString()) + "</BINDEREFFDATE>";
				//				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<BINDEREXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["BINDEREXPDATE"].ToString()) + "</BINDEREXPDATE>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<BINDERCVGNOTBOUND " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["BINDERCVGNOTBOUND"].ToString()) + "</BINDERCVGNOTBOUND>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<PAYMENTBILLING " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["PAYMENTBILLING"].ToString()) + "</PAYMENTBILLING>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<APPLICANTCOPLAN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Wolverine/" + DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_TYPE"].ToString()) + "</APPLICANTCOPLAN>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<PAYMENTPLAN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["PLAN_DESCRIPTION"].ToString()) + "</PAYMENTPLAN>";
				//Agency
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</AGENCYNAME>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_ADD"].ToString()) + "</AGENCYADDRESS>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<AGENCYCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_CITYSTZIP"].ToString()) + "</AGENCYCITYSTATEZIP>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<AGENCYPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString()) + "</AGENCYPHONE>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<AGENCYFAX " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_FAX"].ToString()) + "</AGENCYFAX>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_CODE"].ToString()) + "</AGENCYCODE>";
				Acord81PolicyElement.InnerXml = Acord81PolicyElement.InnerXml +  "<AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["SUB_CODE"].ToString()) + "</AGENCYSUBCODE>";
			
				#endregion

				#region Acord73 Page
				XmlElement Acord73PolicyElement;
				Acord73PolicyElement = AcordPDFXML.CreateElement("POLICY");
				Acord73RootElement.AppendChild(Acord73PolicyElement);
				Acord73PolicyElement.SetAttribute(fieldType,fieldTypeSingle);

				Acord73PolicyElement.InnerXml = Acord73PolicyElement.InnerXml +  "<DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(System.DateTime.Today.ToString("MM/dd/yyyy")) + "</DATE>";
				
				Acord73PolicyElement.InnerXml = Acord73PolicyElement.InnerXml +  "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</POLICYNUMBER>";
				Acord73PolicyElement.InnerXml = Acord73PolicyElement.InnerXml +  "<EFFECTIVEDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</EFFECTIVEDATE>";
				Acord73PolicyElement.InnerXml = Acord73PolicyElement.InnerXml +  "<EXPIRATIONDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</EXPIRATIONDATE>";
				//Agency
				Acord73PolicyElement.InnerXml = Acord73PolicyElement.InnerXml +  "<AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</AGENCYNAME>";
				Acord73PolicyElement.InnerXml = Acord73PolicyElement.InnerXml +  "<AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_ADD"].ToString()) + "</AGENCYADDRESS>";
				Acord73PolicyElement.InnerXml = Acord73PolicyElement.InnerXml +  "<AGENCYCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_CITYSTZIP"].ToString()) + "</AGENCYCITYSTATEZIP>";
				Acord73PolicyElement.InnerXml = Acord73PolicyElement.InnerXml +  "<AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_CODE"].ToString()) + "</AGENCYCODE>";
				Acord73PolicyElement.InnerXml = Acord73PolicyElement.InnerXml +  "<AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["SUB_CODE"].ToString()) + "</AGENCYSUBCODE>";
				#endregion

				#region Acord82 Page
				gobjWrapper.ClearParameteres();
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempDataSet = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS");
				gobjWrapper.ClearParameteres();

				if(DSTempDataSet.Tables[0].Rows.Count>0)
				{
					XmlElement Acord82PolicyElement;
					Acord82PolicyElement = AcordPDFXML.CreateElement("POLICY");
					Acord82RootElement.AppendChild(Acord82PolicyElement);
					Acord82PolicyElement.SetAttribute(fieldType,fieldTypeSingle);
			
					Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(System.DateTime.Today.ToString("MM/dd/yyyy")) + "</DATE>";
					Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<APPLICANTPOLNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_NUMBER"].ToString()) + "</APPLICANTPOLNUM>";
					Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<APPLICANTEFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</APPLICANTEFFDATE>";
					Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<APPLICANTEXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</APPLICANTEXPDATE>";
					Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<BINDEREFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()) + "</BINDEREFFDATE>";
					Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<BINDEREXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()) + "</BINDEREXPDATE>";
					if(DSTempPolicyDataSet.Tables[0].Rows[0]["BINDERCVGNOTBOUND"].ToString() == "1")
						Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<BINDERCVGNOTBOUND " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Y") + "</BINDERCVGNOTBOUND>";
					Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<PAYMENTBILLING " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["PAYMENTBILLING"].ToString()) + "</PAYMENTBILLING>";
					Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<PAYMENTDIRECTBILL " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["PAYMENTDIRECTBILL"].ToString()) + "</PAYMENTDIRECTBILL>";
					Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<PAYMENTAPPBILL " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["PAYMENTAPPBILL"].ToString()) + "</PAYMENTAPPBILL>";
					//Agency
					Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString()) + "</AGENCYNAME>";
					Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_ADD"].ToString()) + "</AGENCYADDRESS>";
					Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<AGENCYCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_CITYSTZIP"].ToString()) + "</AGENCYCITYSTATEZIP>";
					Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<AGENCYPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_PHONE"].ToString()) + "</AGENCYPHONE>";
					Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<AGENCYFAX " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_FAX"].ToString()) + "</AGENCYFAX>";
					Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_CODE"].ToString()) + "</AGENCYCODE>";
					Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["SUB_CODE"].ToString()) + "</AGENCYSUBCODE>";
					Acord82PolicyElement.InnerXml = Acord82PolicyElement.InnerXml +  "<APPLICANTCOPLAN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Wolverine/" + DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_TYPE"].ToString()) + "</APPLICANTCOPLAN>";
				}
				#endregion
			}
		}
		#endregion

		#region Creating Named Insured And CoApplicant Info
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
		private void CreateNamedInsuredCoAppXml()
		{
			//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
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
                
				if (gStrPdfFor == PDFForAcord)
				{
					#region Acord 82 Page
					XmlElement Acord82NamedInsuredElement;
					Acord82NamedInsuredElement = AcordPDFXML.CreateElement("NAMEDINSURED");
					Acord82RootElement.AppendChild(Acord82NamedInsuredElement);
					Acord82NamedInsuredElement.SetAttribute(fieldType,fieldTypeSingle);

					Acord82NamedInsuredElement.InnerXml = Acord82NamedInsuredElement.InnerXml +  "<APPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["APPNAME"].ToString()) + "</APPLICANTNAME>";
					Acord82NamedInsuredElement.InnerXml = Acord82NamedInsuredElement.InnerXml +  "<APPLICANTADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["CUSTADDRESS"].ToString()) + "</APPLICANTADDRESS>";
					Acord82NamedInsuredElement.InnerXml = Acord82NamedInsuredElement.InnerXml +  "<APPLICANTCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["CUSTCITYSTZIP"].ToString()) + "</APPLICANTCITYSTATEZIP>";
					Acord82NamedInsuredElement.InnerXml = Acord82NamedInsuredElement.InnerXml +  "<APPLICANTHOMEPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["PHONE"].ToString()) + "</APPLICANTHOMEPHONE>";

					#endregion

					#region Acord 73 Page
					XmlElement Acord73NamedInsuredElement;
					Acord73NamedInsuredElement = AcordPDFXML.CreateElement("NAMEDINSURED");
					Acord73RootElement.AppendChild(Acord73NamedInsuredElement);
					Acord73NamedInsuredElement.SetAttribute(fieldType,fieldTypeSingle);

					Acord73NamedInsuredElement.InnerXml = Acord73NamedInsuredElement.InnerXml +  "<APPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["APPNAME"].ToString()) + "</APPLICANTNAME>";
					Acord73NamedInsuredElement.InnerXml = Acord73NamedInsuredElement.InnerXml +  "<APPLICANTADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["CUSTADDRESS"].ToString()) + "</APPLICANTADDRESS>";
					Acord73NamedInsuredElement.InnerXml = Acord73NamedInsuredElement.InnerXml +  "<APPLICANTCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["CUSTCITYSTZIP"].ToString()) + "</APPLICANTCITYSTATEZIP>";
					#endregion
				
					#region Acord 81 Page
					XmlElement Acord81NamedInsuredElement;
					Acord81NamedInsuredElement = AcordPDFXML.CreateElement("NAMEDINSURED");
					Acord81RootElement.AppendChild(Acord81NamedInsuredElement);
					Acord81NamedInsuredElement.SetAttribute(fieldType,fieldTypeSingle);

					Acord81NamedInsuredElement.InnerXml = Acord81NamedInsuredElement.InnerXml +  "<APPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["APPNAME"].ToString()) + "</APPLICANTNAME>";
					Acord81NamedInsuredElement.InnerXml = Acord81NamedInsuredElement.InnerXml +  "<APPLICANTADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["CUSTADDRESS"].ToString()) + "</APPLICANTADDRESS>";
					Acord81NamedInsuredElement.InnerXml = Acord81NamedInsuredElement.InnerXml +  "<APPLICANTCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["CUSTCITYSTZIP"].ToString()) + "</APPLICANTCITYSTATEZIP>";
					Acord81NamedInsuredElement.InnerXml = Acord81NamedInsuredElement.InnerXml +  "<APPLICANTBUSSPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["CUSTOMER_BUSINESS_PHONE"].ToString()) + "</APPLICANTBUSSPHONE>";
					Acord81NamedInsuredElement.InnerXml = Acord81NamedInsuredElement.InnerXml +  "<APPLICANTDOB " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["DOB"].ToString()) + "</APPLICANTDOB>";
					Acord81NamedInsuredElement.InnerXml = Acord81NamedInsuredElement.InnerXml +  "<APPLICANTMARITALSTATUS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["MARTSTATUSCODE"].ToString()) + "</APPLICANTMARITALSTATUS>";
					Acord81NamedInsuredElement.InnerXml = Acord81NamedInsuredElement.InnerXml +  "<APPLICANTOCCUPATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["OCCUPATION"].ToString()) + "</APPLICANTOCCUPATION>";
					if (DSTempApplicantDataSet.Tables[0].Rows.Count > 1)
						Acord81NamedInsuredElement.InnerXml = Acord81NamedInsuredElement.InnerXml +  "<SPOUSEOCCUPATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[1]["OCCUPATION"].ToString()) + "</SPOUSEOCCUPATION>";
					else
						Acord81NamedInsuredElement.InnerXml = Acord81NamedInsuredElement.InnerXml +  "<SPOUSEOCCUPATION " + fieldType + "=\"" + fieldTypeText + "\"></SPOUSEOCCUPATION>";

					#endregion

					#region Acord 80 Page

					int RowCounter=0;
					string strCoappSSn="";
					XmlElement Acord80NamedInsuredElement;
					Acord80NamedInsuredElement = AcordPDFXML.CreateElement("NAMEDINSURED");
					Acord80RootElement.AppendChild(Acord80NamedInsuredElement);
					Acord80NamedInsuredElement.SetAttribute(fieldType,fieldTypeSingle);

					Acord80NamedInsuredElement.InnerXml = Acord80NamedInsuredElement.InnerXml +  "<APPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["APPNAME"].ToString()) + "</APPLICANTNAME>";
					Acord80NamedInsuredElement.InnerXml = Acord80NamedInsuredElement.InnerXml +  "<APPLICANTADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["CUSTADDRESS"].ToString()) + "</APPLICANTADDRESS>";
					Acord80NamedInsuredElement.InnerXml = Acord80NamedInsuredElement.InnerXml +  "<APPLICANTCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["CUSTCITYSTZIP"].ToString()) + "</APPLICANTCITYSTATEZIP>";
					Acord80NamedInsuredElement.InnerXml = Acord80NamedInsuredElement.InnerXml +  "<APPLICANTHOMEPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["PHONE"].ToString()) + "</APPLICANTHOMEPHONE>";
					Acord80NamedInsuredElement.InnerXml = Acord80NamedInsuredElement.InnerXml +  "<APPLICANTBUSSPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["CUSTOMER_BUSINESS_PHONE"].ToString()) + "</APPLICANTBUSSPHONE>";
			
					XmlElement AddlCoApplicantElement;
					AddlCoApplicantElement = AcordPDFXML.CreateElement("ADDITIONALCOAPPLICANTS");
					Acord80RootElement.AppendChild(AddlCoApplicantElement);
					AddlCoApplicantElement.SetAttribute(fieldType,fieldTypeMultiple);
					AddlCoApplicantElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD80COAPP"));
					AddlCoApplicantElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD80COAPP"));
					AddlCoApplicantElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD80COAPPEXTN"));
					AddlCoApplicantElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD80COAPPEXTN"));
 
					if(DSTempApplicantDataSet.Tables[0].Rows.Count == 1)
					{
						XmlElement AppElement;
						AppElement =  AcordPDFXML.CreateElement("APPLICANT");
						AddlCoApplicantElement.AppendChild(AppElement);
						AppElement.SetAttribute(fieldType,fieldTypeNormal);
						AppElement.SetAttribute(id,(RowCounter).ToString());

						AppElement.InnerXml = AppElement.InnerXml +  "<ADDLPOLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</ADDLPOLICYNUMBER>";
						AppElement.InnerXml = AppElement.InnerXml +  "<ADDLAPPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</ADDLAPPLICANTNAME>";
						AppElement.InnerXml = AppElement.InnerXml +  "<NAMEINSUREDNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</NAMEINSUREDNAME>";
						AppElement.InnerXml = AppElement.InnerXml +  "<NAMEINSUREDADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantAddress + "</NAMEINSUREDADDRESS>";
						AppElement.InnerXml = AppElement.InnerXml +  "<NAMEINSUREDCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantCityStZip + "</NAMEINSUREDCITYSTATEZIP>";
					
					
						//Added by Mohit Agarwal 23-Apr-2007
						//DataSet DSDwellDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_Dwelling_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
						foreach(DataRow Dwellrow in DSTempDwellingDetailDataSet.Tables[0].Rows)
						{
							if(ApplicantAddress.Trim() != Dwellrow["LOC_ADDRESS"].ToString().Trim() || ApplicantCityStZip.Trim() != Dwellrow["LOC_CITYSTATEZIP"].ToString().Trim())
							{
								AppElement.InnerXml = AppElement.InnerXml +  "<LOCATIONADD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Dwellrow["LOC_ADDRESS"].ToString()) + "</LOCATIONADD>";
								AppElement.InnerXml = AppElement.InnerXml +  "<LOCATIONADD2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Dwellrow["LOC_CITYSTATEZIP"].ToString()) + "</LOCATIONADD2>";
								break;
							}
						}
						//					AppElement.InnerXml = AppElement.InnerXml +  "<LOCATIONADD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["LOCADD"].ToString()) + "</LOCATIONADD>";
						//					AppElement.InnerXml = AppElement.InnerXml +  "<LOCATIONADD2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["LOCADD2"].ToString()) + "</LOCATIONADD2>";
					
						if(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["SSN"].ToString() !="" && DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["SSN"].ToString() !="0")
						{
							strCoappSSn = "";
							strCoappSSn = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["SSN"].ToString());
							
							if(strCoappSSn.Trim()!="")//If-Else condition added by Charles on 21/7/2009 for Itrack 6129
							{
								string strvaln = "xxx-xx-";
								strvaln += strCoappSSn.Substring(strvaln.Length, strCoappSSn.Length - strvaln.Length);
								strCoappSSn = strvaln;
							}
							else
								strCoappSSn="";

						}
						AppElement.InnerXml = AppElement.InnerXml +  "<APPOCCUPATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["OCCUPATION"].ToString()) + "</APPOCCUPATION>";
						AppElement.InnerXml = AppElement.InnerXml +  "<APPEMPLOYEENAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["APPNAME"].ToString()) + "</APPEMPLOYEENAME>";
						AppElement.InnerXml = AppElement.InnerXml +  "<APPYEAREMPL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["YEARSEMPL"].ToString()) + "</APPYEAREMPL>";
						AppElement.InnerXml = AppElement.InnerXml +  "<APPMARTSTATUS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["MARTSTATUS"].ToString()) + "</APPMARTSTATUS>";
						AppElement.InnerXml = AppElement.InnerXml +  "<APPDATEOFBIRTH " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["DOB"].ToString()) + "</APPDATEOFBIRTH>";
						AppElement.InnerXml = AppElement.InnerXml +  "<APPSSN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strCoappSSn) + "</APPSSN>";
						AppElement.InnerXml = AppElement.InnerXml +  "<APPEMPLOYERNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["CO_APPLI_EMPL_NAME"].ToString()) + "</APPEMPLOYERNAME>";
						AppElement.InnerXml = AppElement.InnerXml +  "<APPEMPLOYERADD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["CO_APPLI_EMPL_ADD"].ToString()) + "</APPEMPLOYERADD>";
						AppElement.InnerXml = AppElement.InnerXml +  "<APPYEAROCCU " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["YEARSOCCU"].ToString()) + "</APPYEAROCCU>";
						AppElement.InnerXml = AppElement.InnerXml +  "<APPMARTSTATUSCD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["MARTSTATUSCODE"].ToString()) + "</APPMARTSTATUSCD>";
					}

					for(RowCounter=1;RowCounter<DSTempApplicantDataSet.Tables[0].Rows.Count;RowCounter++)
					{
						XmlElement CoAppElement;
						CoAppElement =  AcordPDFXML.CreateElement("COAPPLICANT");
						AddlCoApplicantElement.AppendChild(CoAppElement);
						CoAppElement.SetAttribute(fieldType,fieldTypeNormal);
						CoAppElement.SetAttribute(id,(RowCounter-1).ToString());

						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<ADDLPOLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</ADDLPOLICYNUMBER>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<ADDLAPPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</ADDLAPPLICANTNAME>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<NAMEINSUREDNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</NAMEINSUREDNAME>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<NAMEINSUREDADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantAddress + "</NAMEINSUREDADDRESS>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<NAMEINSUREDCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantCityStZip + "</NAMEINSUREDCITYSTATEZIP>";

						if(RowCounter == 1)
						{
							//Added by Mohit Agarwal 23-Apr-2007
							//DataSet DSDwellDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_Dwelling_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
							foreach(DataRow Dwellrow in DSTempDwellingDetailDataSet.Tables[0].Rows)
							{
								if(ApplicantAddress.Trim() != Dwellrow["LOC_ADDRESS"].ToString().Trim() || ApplicantCityStZip.Trim() != Dwellrow["LOC_CITYSTATEZIP"].ToString().Trim())
								{
									CoAppElement.InnerXml = CoAppElement.InnerXml +  "<LOCATIONADD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Dwellrow["LOC_ADDRESS"].ToString()) + "</LOCATIONADD>";
									CoAppElement.InnerXml = CoAppElement.InnerXml +  "<LOCATIONADD2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Dwellrow["LOC_CITYSTATEZIP"].ToString()) + "</LOCATIONADD2>";
									break;
								}
							}
							//					CoAppElement.InnerXml = CoAppElement.InnerXml +  "<LOCATIONADD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["LOCADD"].ToString()) + "</LOCATIONADD>";
							//					CoAppElement.InnerXml = CoAppElement.InnerXml +  "<LOCATIONADD2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["LOCADD2"].ToString()) + "</LOCATIONADD2>";
					
							if(DSTempApplicantDataSet.Tables[0].Rows[0]["SSN"].ToString() !="" && DSTempApplicantDataSet.Tables[0].Rows[0]["SSN"].ToString() !="0")
								{
									strCoappSSn = "";
									strCoappSSn = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(DSTempApplicantDataSet.Tables[0].Rows[0]["SSN"].ToString());
									
									if(strCoappSSn.Trim()!="")//If-Else condition added by Charles on 21/7/2009 for Itrack 6129
									{
										string strvaln = "xxx-xx-";
										strvaln += strCoappSSn.Substring(strvaln.Length, strCoappSSn.Length - strvaln.Length);
										strCoappSSn = strvaln;
									}
									else
										strCoappSSn="";

								}
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<APPOCCUPATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["OCCUPATION"].ToString()) + "</APPOCCUPATION>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<APPEMPLOYEENAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["APPNAME"].ToString()) + "</APPEMPLOYEENAME>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<APPYEAREMPL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["YEARSEMPL"].ToString()) + "</APPYEAREMPL>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<APPMARTSTATUS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["MARTSTATUS"].ToString()) + "</APPMARTSTATUS>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<APPDATEOFBIRTH " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["DOB"].ToString()) + "</APPDATEOFBIRTH>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<APPSSN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strCoappSSn) + "</APPSSN>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<APPEMPLOYERNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["CO_APPLI_EMPL_NAME"].ToString()) + "</APPEMPLOYERNAME>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<APPEMPLOYERADD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["CO_APPLI_EMPL_ADD"].ToString()) + "</APPEMPLOYERADD>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<APPYEAROCCU " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["YEARSOCCU"].ToString()) + "</APPYEAROCCU>";
							CoAppElement.InnerXml = CoAppElement.InnerXml +  "<APPMARTSTATUSCD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[0]["MARTSTATUSCODE"].ToString()) + "</APPMARTSTATUSCD>";
						}
						if(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["SSN"].ToString() !="" && DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["SSN"].ToString() !="0")
						{
							strCoappSSn = "";
							strCoappSSn = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["SSN"].ToString());
							
							if(strCoappSSn.Trim()!="")//If-Else condition added by Charles on 21/7/2009 for Itrack 6129
							{
								string strvaln = "xxx-xx-";
								strvaln += strCoappSSn.Substring(strvaln.Length, strCoappSSn.Length - strvaln.Length);
								strCoappSSn = strvaln;
							}
							else
								strCoappSSn="";
						}
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPOCCUPATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["OCCUPATION"].ToString()) + "</COAPPOCCUPATION>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPEMPLOYEENAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["APPNAME"].ToString()) + "</COAPPEMPLOYEENAME>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPYEAREMPL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["YEARSEMPL"].ToString()) + "</COAPPYEAREMPL>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPMARTSTATUS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["MARTSTATUS"].ToString()) + "</COAPPMARTSTATUS>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPDATEOFBIRTH " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["DOB"].ToString()) + "</COAPPDATEOFBIRTH>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPSSN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strCoappSSn) + "</COAPPSSN>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPEMPLOYERNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["CO_APPLI_EMPL_NAME"].ToString()) + "</COAPPEMPLOYERNAME>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPEMPLOYERADD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["CO_APPLI_EMPL_ADD"].ToString()) + "</COAPPEMPLOYERADD>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPYEAROCCU " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["YEARSOCCU"].ToString()) + "</COAPPYEAROCCU>";
						CoAppElement.InnerXml = CoAppElement.InnerXml +  "<COAPPMARTSTATUSCD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempApplicantDataSet.Tables[0].Rows[RowCounter]["MARTSTATUSCODE"].ToString()) + "</COAPPMARTSTATUSCD>";
					}
					#endregion
				}
			}
		}
		#endregion

		#region Creating Prior Policy/Coverage XML
		private void CreatePriorPolicyCoverage()
		{
			if (gStrPdfFor == PDFForAcord)
			{
				//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFPriorPolicyAndLossDetails " + gStrClientID + ",'HOME','POLICY'");
			
				if (DSTempPriorLossDataSet.Tables[0].Rows.Count>0)
				{
					#region Acord 80 Page
			
					XmlElement Acord80PriorPolicyElement;
					Acord80PriorPolicyElement = AcordPDFXML.CreateElement("PRIORPOLICYCOVERAGE");
					Acord80RootElement.AppendChild(Acord80PriorPolicyElement);
					Acord80PriorPolicyElement.SetAttribute(fieldType,fieldTypeSingle);

					Acord80PriorPolicyElement.InnerXml = Acord80PriorPolicyElement.InnerXml +  "<PRIORCARRIER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPriorLossDataSet.Tables[0].Rows[0]["CARRIER"].ToString()) + "</PRIORCARRIER>";
					Acord80PriorPolicyElement.InnerXml = Acord80PriorPolicyElement.InnerXml +  "<PRIORPOLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPriorLossDataSet.Tables[0].Rows[0]["OLD_POLICY_NUMBER"].ToString()) + "</PRIORPOLICYNUMBER>";
					Acord80PriorPolicyElement.InnerXml = Acord80PriorPolicyElement.InnerXml +  "<PRIOREXPIRATIONDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPriorLossDataSet.Tables[0].Rows[0]["EFF_DATE"].ToString()) + "</PRIOREXPIRATIONDATE>";
					if(DSTempPriorLossDataSet.Tables[0].Rows[0]["EFF_DATE"].ToString() != "")
						Acord80PriorPolicyElement.InnerXml +="<PRIOREXPIRATIONDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPriorLossDataSet.Tables[0].Rows[0]["OLD_POLICY_NUMBER"].ToString()) + "/" + RemoveJunkXmlCharacters(DSTempPriorLossDataSet.Tables[0].Rows[0]["EFF_DATE"].ToString()) + "</PRIOREXPIRATIONDATE>";
					else
						Acord80PriorPolicyElement.InnerXml +="<PRIOREXPIRATIONDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempPriorLossDataSet.Tables[0].Rows[0]["OLD_POLICY_NUMBER"].ToString()) + "</PRIOREXPIRATIONDATE>";
			
					#endregion
				}
			}
		}
		#endregion

		#region Creating Prior Loss XML
		private void CreatePriorLossXml()
		{
			if (gStrPdfFor == PDFForAcord)
			{
				//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFPriorPolicyAndLossDetails " + gStrClientID + ",'HOME','LOSS'");
				if (DSTempPriorLossDataSet.Tables[0].Rows.Count>0)
				{
					XmlElement Acord80PriorLoss;
					Acord80PriorLoss = AcordPDFXML.CreateElement("PRIORLOSSCOVERAGE");
					Acord80RootElement.AppendChild(Acord80PriorLoss);
					Acord80PriorLoss.SetAttribute(fieldType,fieldTypeMultiple);
					Acord80PriorLoss.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD80LOSSHIST"));
					Acord80PriorLoss.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD80LOSSHIST"));
					Acord80PriorLoss.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD80LOSSHISTEXTN"));
					Acord80PriorLoss.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD80LOSSHISTEXTN"));

					int RowCounter=0;
					foreach (DataRow Row in DSTempDataSet.Tables[0].Rows)
					{
						XmlElement Acord80PriorLossElement;
						Acord80PriorLossElement =  AcordPDFXML.CreateElement("COAPPLICANT");
						Acord80PriorLoss.AppendChild(Acord80PriorLossElement);
						Acord80PriorLossElement.SetAttribute(fieldType,fieldTypeNormal);
						Acord80PriorLossElement.SetAttribute(id,RowCounter.ToString());

						Acord80PriorLossElement.InnerXml = Acord80PriorLossElement.InnerXml +  "<PRIORLOSSENTERED " + fieldType + "=\"" + fieldTypeText + "\">Y</PRIORLOSSENTERED>";
						Acord80PriorLossElement.InnerXml = Acord80PriorLossElement.InnerXml +  "<APPLICANTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</APPLICANTNAME>";
						Acord80PriorLossElement.InnerXml = Acord80PriorLossElement.InnerXml +  "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNUMBER>";
						Acord80PriorLossElement.InnerXml = Acord80PriorLossElement.InnerXml +  "<LOSSHISTDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["OCCURENCE_DATE"].ToString()) + "</LOSSHISTDATE>";
						Acord80PriorLossElement.InnerXml = Acord80PriorLossElement.InnerXml +  "<LOSSHISTTYPE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["LOSS_TYPE"].ToString()) + "</LOSSHISTTYPE>";
						Acord80PriorLossElement.InnerXml = Acord80PriorLossElement.InnerXml +  "<LOSSHISTDESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["LOSS_DESC"].ToString()) + "</LOSSHISTDESCRIPTION>";
						Acord80PriorLossElement.InnerXml = Acord80PriorLossElement.InnerXml +  "<LOSSHISTAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["AMOUNT"].ToString()) + "</LOSSHISTAMOUNT>";
						RowCounter++;
					}

				}
			}
		}
		#endregion

		#region Creating Solid Fuel Xml
		private void CreateSolidFuelXml()
		{
			if (gStrPdfFor == PDFForAcord)
			{
				gobjWrapper.ClearParameteres();
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFSolidFuelDetails");
				//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFSolidFuelDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
				if (DSTempDataSet.Tables[0].Rows.Count > 0) 
				{
					Acord80RootElement.SelectSingleNode("POLICY/ATTACHSOLIDFUEL").InnerText = "1";

					XmlElement Acord73SolidFuel;
					Acord73SolidFuel = AcordPDFXML.CreateElement("SOLIDFUEL");
					Acord73RootElement.AppendChild(Acord73SolidFuel);
					Acord73SolidFuel.SetAttribute(fieldType,fieldTypeMultiple);
					Acord73SolidFuel.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD73SOLIDFUEL"));
					Acord73SolidFuel.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD73SOLIDFUEL"));
					Acord73SolidFuel.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD73SOLIDFUELEXTN"));
					Acord73SolidFuel.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD73SOLIDFUELEXTN"));
					int RowCounter=0;
					foreach (DataRow Row in DSTempDataSet.Tables[0].Rows)
					{
						XmlElement Acord73SolidFuelElement;
						Acord73SolidFuelElement =  AcordPDFXML.CreateElement("SOLIDFUELINFO");
						Acord73SolidFuel.AppendChild(Acord73SolidFuelElement);
						Acord73SolidFuelElement.SetAttribute(fieldType,fieldTypeNormal);
						Acord73SolidFuelElement.SetAttribute(id,RowCounter.ToString());

						//Solid Fuel Details
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_MANUFACTURER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["MANUFACTURER"].ToString()) + "</SFD_MANUFACTURER>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_BRANDNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["BRAND_NAME"].ToString()) + "</SFD_BRANDNAME>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_MODELNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["MODEL_NUMBER"].ToString()) + "</SFD_MODELNUM>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_FUEL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["FUEL"].ToString()) + "</SFD_FUEL>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_STOVETYPE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["STOVE_TYPE"].ToString()) + "</SFD_STOVETYPE>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_LABTESTED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["HAVE_LABORATORY_LABEL"].ToString()) + "</SFD_LABTESTED>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_ISTHEUNIT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["IS_UNIT"].ToString()) + "</SFD_ISTHEUNIT>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_ISTHEUNITOTHDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["UNIT_OTHER_DESC"].ToString()) + "</SFD_ISTHEUNITOTHDESC>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_CONSTR " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["CONSTRUCTION"].ToString()) + "</SFD_CONSTR>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_LOCATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["LOCATION"].ToString()) + "</SFD_LOCATION>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_LOCATIONOTHDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["LOC_OTHER_DESC"].ToString()) + "</SFD_LOCATIONOTHDESC>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_YEARDEVINST " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["YEAR_DEVICE_INSTALLED"].ToString()) + "</SFD_YEARDEVINST>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_INSTALLDONE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["WAS_PROF_INSTALL_DONE"].ToString()) + "</SFD_INSTALLDONE>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_INSTINSPBY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["INSTALL_INSPECTED_BY"].ToString()) + "</SFD_INSTINSPBY>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_INSTINSPBYOTHDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["INSTALL_OTHER_DESC"].ToString()) + "</SFD_INSTINSPBYOTHDESC>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_HEATUSE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["HEATING_USE"].ToString()) + "</SFD_HEATUSE>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_HEATTYPEUSE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["HEATING_SOURCE"].ToString()) + "</SFD_HEATTYPEUSE>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SFD_HEATTYPEUSEOTHDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["OTHER_DESC"].ToString()) + "</SFD_HEATTYPEUSEOTHDESC>";
						//Chimney
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CHIM_STOVEVENTED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["IS_STOVE_VENTED"].ToString()) + "</CHIM_STOVEVENTED>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CHMOTHERATTCH " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["OTHER_DEVICES_ATTACHED"].ToString()) + "</CHMOTHERATTCH>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CHMCONSTR " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["CHIMNEY_CONSTRUCTION"].ToString()) + "</CHMCONSTR>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CHMCONSTROTHDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["CONSTRUCT_OTHER_DESC"].ToString()) + "</CHMCONSTROTHDESC>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CHIM_TILEFLUELINE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["IS_TILE_FLUE_LINING"].ToString()) + "</CHIM_TILEFLUELINE>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CHIM_FROMGNDUP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["IS_CHIMNEY_GROUND_UP"].ToString()) + "</CHIM_FROMGNDUP>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CHIM_AFTERHSEBLT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["CHIMNEY_INST_AFTER_HOUSE_BLT"].ToString()) + "</CHIM_AFTERHSEBLT>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CHIM_COVCOMWALL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["IS_CHIMNEY_COVERED"].ToString()) + "</CHIM_COVCOMWALL>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CHIM_COVCOMWALLDIST " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DIST_FROM_SMOKE_PIPE"].ToString()) + "</CHIM_COVCOMWALLDIST>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CHIM_THIMPROTEDGE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["THIMBLE_OR_MATERIAL"].ToString()) + "</CHIM_THIMPROTEDGE>";
						//Stove Pipe
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SPSTOVEPIPEIS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["STOVE_PIPE_IS"].ToString()) + "</SPSTOVEPIPEIS>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SPPIPEFITSNUG " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DOES_SMOKE_PIPE_FIT"].ToString()) + "</SPPIPEFITSNUG>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SPWASTEHEAT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["SMOKE_PIPE_WASTE_HEAT"].ToString()) + "</SPWASTEHEAT>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SPCONSECURE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["STOVE_CONN_SECURE"].ToString()) + "</SPCONSECURE>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SPPASSINETERIOR " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["SMOKE_PIPE_PASS"].ToString()) + "</SPPASSINETERIOR>";
						
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SPTHIMBLE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["SELECT_PASS"].ToString()) + "</SPTHIMBLE>";
						if (Row["SELECT_PASS"].ToString()=="8966")
						{
							Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SPPASSINCHES " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["PASS_INCHES"].ToString()) + "</SPPASSINCHES>";
						}
						else if(Row["SELECT_PASS"].ToString()=="8967")
						{
							Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<SPPASSINCHES1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["PASS_INCHES"].ToString()) + "</SPPASSINCHES1>";
						}
							//Unit Clearance
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<UCUSECONFORM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["STOVE_INSTALLATION_CONFORM_SPECIFICATIONS"].ToString()) + "</UCUSECONFORM>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<UCPROTMETDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["PROT_MAT_SPACED"].ToString()) + "</UCPROTMETDESC>";
						//FIRE PROTECTION
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<FPSMOKEDETECTOR " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["IS_SMOKE_DETECTOR"].ToString()) + "</FPSMOKEDETECTOR>";
						//CLEANING
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CLPIPECLEAN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["STOVE_SMOKE_PIPE_CLEANED"].ToString()) + "</CLPIPECLEAN>";
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CLPIPEINSPECTEDBY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["STOVE_CLEANER"].ToString()) + "</CLPIPEINSPECTEDBY>";
						//REMARKS
						Acord73SolidFuelElement.InnerXml = Acord73SolidFuelElement.InnerXml +  "<CLFPREMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["REMARKS"].ToString()) + "</CLFPREMARKS>";

						RowCounter++;
					}
				}
				else
					AcordPDFXML.SelectSingleNode(RootElement).RemoveChild(Acord73RootElement);
			}
		}
		#endregion

		#region Create RV XML

		// Added by Mohit Agarwal 30-Apr-2007
		private string getRVPremium(DataSet DSTempDataSet, string comp_code, string risk_id)
		{
			if(DSTempDataSet.Tables.Count > 1 && DSTempDataSet.Tables[1].Rows.Count > 0)
			{
				foreach (DataRow Row in DSTempDataSet.Tables[1].Rows)
				{
					if(Row["COMPONENT_CODE"].ToString() == comp_code && Row["RISK_ID"].ToString() == risk_id)
						return Row["COVERAGE_PREMIUM"].ToString();
				}
			}
			return "";
		}

		private void CreateRVXML()
		{
			gobjWrapper.ClearParameteres();
			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFRecreationalVehiclesDetails");
			//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFRecreationalVehiclesDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			if (DSTempDataSet.Tables[0].Rows.Count > 0) 
			{
				XmlElement DecPageRV;
				DecPageRV = AcordPDFXML.CreateElement("RECREATIONALVEHICLES");

				XmlElement Acord80RV;
				Acord80RV = AcordPDFXML.CreateElement("RECREATIONALVEHICLES");

				if (gStrPdfFor == PDFForDecPage)
				{
					#region Elements for DecPage
					DecPageRootElement.AppendChild(DecPageRV);
					DecPageRV.SetAttribute(fieldType,fieldTypeMultiple);
					DecPageRV.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGERV"));
					DecPageRV.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGERV"));
					DecPageRV.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGERVEXTN"));
					DecPageRV.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGERVEXTN"));
					#endregion
				}
				else if (gStrPdfFor == PDFForAcord)
				{
					Acord80RootElement.SelectSingleNode("POLICY/ATTACHRV").InnerText = "1";
					#region Elements for Acord 80 RV
					Acord80RootElement.AppendChild(Acord80RV);
					Acord80RV.SetAttribute(fieldType,fieldTypeMultiple);
					Acord80RV.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD80RV"));
					Acord80RV.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD80RV"));
					Acord80RV.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD80RVEXTN"));
					Acord80RV.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD80RVEXTN"));
					#endregion
				}

				int RVRowCounter=0;
				string strDed="",strCov="",strPrem="",strAmt="",strLibPrem="",strMedLIMT="",strMedPrem="";
				foreach (DataRow Row in DSTempDataSet.Tables[0].Rows)
				{
					XmlElement DecPageRVElement;
					DecPageRVElement =  AcordPDFXML.CreateElement("RVINFO");
				
					XmlElement Acord80RVElement;
					Acord80RVElement =  AcordPDFXML.CreateElement("RVINFO");
					strAmt=Row["INSURING_VALUE"].ToString();
					strCov=GetIntFormat(strRVLiabLim);
					strMedLIMT=GetIntFormat(strRVMedLim);
					strDed=GetIntFormat(Row["DEDUCTIBLE"].ToString());

					if(gStrtemp == "temp")
					{
						htpremium.Clear();
						foreach (XmlNode PremiumNode in GetRVPremium(Row["REC_VEH_ID"].ToString()))
						{
							if(!htpremium.Contains(getAttributeValue(PremiumNode,"COMPONENT_CODE")))
								htpremium.Add(getAttributeValue(PremiumNode,"COMPONENT_CODE"),getAttributeValue(PremiumNode,"STEPPREMIUM"));
						}
							strPrem=GetRVPremiumBeforeCommit("PD", htpremium);							
							strLibPrem=GetRVPremiumBeforeCommit("LIAB", htpremium);
							strMedPrem=GetRVPremiumBeforeCommit("MEDPAY", htpremium);
						
					}
					else
					{					
						strPrem=getRVPremium(DSTempDataSet, "PD", Row["REC_VEH_ID"].ToString());
						strLibPrem=getRVPremium(DSTempDataSet, "LIAB", Row["REC_VEH_ID"].ToString());
						strMedPrem=getRVPremium(DSTempDataSet, "MEDPAY", Row["REC_VEH_ID"].ToString());
					}
					if (gStrPdfFor == PDFForDecPage)
					{
						#region Dec Page
						DecPageRV.AppendChild(DecPageRVElement);
						DecPageRVElement.SetAttribute(fieldType,fieldTypeNormal);
						DecPageRVElement.SetAttribute(id,RVRowCounter.ToString());

						if(string.Compare(gStrCalledFrom,"Policy",true)==0)
						{
							DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVPOLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</RVPOLICYNUMBER>";
							DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVEFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</RVEFFDATE>";
							DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVEXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</RVEXPDATE>";
							DecPageRVElement.InnerXml += "<copyTo " + fieldType + "=\"" + fieldTypeText + "\">" + CopyTo + "</copyTo>";
						}
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVNAMEDINSUNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</RVNAMEDINSUNAME>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVNAMEDINSUADD " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantAddress + "</RVNAMEDINSUADD>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVNAMEDINSUCITYSTZIP " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantCityStZip + "</RVNAMEDINSUCITYSTZIP>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVAGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyName + "</RVAGENCYNAME>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVAGENCYADD " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyAddress + "</RVAGENCYADD>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVAGENCYCITYSTZIP " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCitySTZip + "</RVAGENCYCITYSTZIP>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVAGENCYPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyPhoneNumber + "</RVAGENCYPHONE>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVAGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCode + "</RVAGENCYCODE>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVAGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencySubCode + "</RVAGENCYSUBCODE>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVAGENCYBILLING " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyBilling + "</RVAGENCYBILLING>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["COMPANY_ID_NUMBER"].ToString()) + "</RVNUMBER>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVDESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["VEHICLE_DESC"].ToString()) + "</RVDESCRIPTION>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVYEARMAKEMODEL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["YEARMAKEMODEL"].ToString()) + "</RVYEARMAKEMODEL>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVHPCC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["HORSE_POWER"].ToString()) + "</RVHPCC>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVSERIALNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["SERIAL"].ToString()) + "</RVSERIALNUMBER>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVINSURINGVALUE " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(strAmt) + "</RVINSURINGVALUE>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVPERSONALLIABLIM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strCov) + "</RVPERSONALLIABLIM>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVMEDPMLIABLIM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strMedLIMT) + "</RVMEDPMLIABLIM>";
						DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVDEDUCTIBLE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strDed) + "</RVDEDUCTIBLE>";
							if(strPrem!="")
								DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(strPrem) + "</RVPREMIUM>";
							if(strLibPrem!="")
								DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVPERSONALLIABPREM " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(strLibPrem) + "</RVPERSONALLIABPREM>";
							if (strMedPrem!="")
								DecPageRVElement.InnerXml = DecPageRVElement.InnerXml +  "<RVMEDPMLIABPREM " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(strMedPrem) + "</RVMEDPMLIABPREM>";
						#endregion
					}
					else if (gStrPdfFor == PDFForAcord)
					{
						#region Acord 80
						Acord80RV.AppendChild(Acord80RVElement);
						Acord80RVElement.SetAttribute(fieldType,fieldTypeNormal);
						Acord80RVElement.SetAttribute(id,RVRowCounter.ToString());

						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVPOLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</RVPOLICYNUMBER>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVEFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</RVEFFDATE>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVEXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</RVEXPDATE>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVNAMEDINSUNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</RVNAMEDINSUNAME>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVNAMEDINSUADD " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantAddress + "</RVNAMEDINSUADD>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVNAMEDINSUCITYSTZIP " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantCityStZip + "</RVNAMEDINSUCITYSTZIP>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVAGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyName + "</RVAGENCYNAME>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVAGENCYADD " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyAddress + "</RVAGENCYADD>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVAGENCYCITYSTZIP " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCitySTZip + "</RVAGENCYCITYSTZIP>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVAGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCode + "</RVAGENCYCODE>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVREMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["REMARKS"].ToString()) + "</RVREMARKS>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["COMPANY_ID_NUMBER"].ToString()) + "</RVNUMBER>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVYEAR " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["YEAR"].ToString()) + "</RVYEAR>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVMAKE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["MAKE"].ToString()) + "</RVMAKE>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVMODEL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["MODEL"].ToString()) + "</RVMODEL>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVVEHICLETYPE " + fieldType + "=\"" + fieldTypeCheckBox + "\">" + RemoveJunkXmlCharacters(Row["VEHICLE_TYPE"].ToString()) + "</RVVEHICLETYPE>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVHPCC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["HORSE_POWER"].ToString()) + "</RVHPCC>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVSERIALNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["SERIAL"].ToString()) + "</RVSERIALNUMBER>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVINSURINGVALUE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["INSURING_VALUE"].ToString()) + "</RVINSURINGVALUE>";
						if(Row["DEDUCTIBLE"].ToString()!="")
						{
							Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVDEDUCTIBLE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DEDUCTIBLE"].ToString()) + "</RVDEDUCTIBLE>";
						}
						
							Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">" + getRVPremium(DSTempDataSet, "SUMTOTAL", Row["REC_VEH_ID"].ToString()) + "</RVPREMIUM>";
						
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVPARTRACE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["USED_IN_RACE_SPEED"].ToString()) + "</RVPARTRACE>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVPRIORLOSS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["PRIOR_LOSSES"].ToString()) + "</RVPRIORLOSS>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVSTATEREG " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["IS_UNIT_REG_IN_OTHER_STATE"].ToString()) + "</RVSTATEREG>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVRISKCANCEL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["RISK_DECL_BY_OTHER_COMP"].ToString()) + "</RVRISKCANCEL>";
						Acord80RVElement.InnerXml = Acord80RVElement.InnerXml +  "<RVGENINFOANYYESDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DESC_RISK_DECL_BY_OTHER_COMP"].ToString()) + "</RVGENINFOANYYESDESC>";
						#endregion
					}

					#region Additional Interests
					DataSet DSTmpRVAddlInt = new DataSet();
					gobjWrapper.ClearParameteres();
					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@RVID",Row["REC_VEH_ID"].ToString());
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTmpRVAddlInt = gobjWrapper.ExecuteDataSet("Proc_GetPDFRVAdditionalInterests");
					//DSTmpRVAddlInt = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFRVAdditionalInterests " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + Row["REC_VEH_ID"].ToString() + ",'" + gStrCalledFrom + "'");
					
					if (DSTmpRVAddlInt.Tables[0].Rows.Count>0)
					{
						XmlElement DecPageRVAddInt;
						DecPageRVAddInt = AcordPDFXML.CreateElement("RVADDLINT");

						XmlElement Acord80RVAddInt;
						Acord80RVAddInt = AcordPDFXML.CreateElement("RVADDLINT");						

						if (gStrPdfFor == PDFForDecPage)
						{
							#region Elements for Dec Page
							DecPageRVElement.AppendChild(DecPageRVAddInt);
							DecPageRVAddInt.SetAttribute(fieldType,fieldTypeMultiple);
							DecPageRVAddInt.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGERVADDLINT"));
							DecPageRVAddInt.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGERVADDLINT"));
							DecPageRVAddInt.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGERVADDLINTEXTN"));
							DecPageRVAddInt.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGERVADDLINTEXTN"));
							#endregion
						}
						else if (gStrPdfFor == PDFForAcord)
						{
							#region Elements For Acord 80 RV
							Acord80RVElement.AppendChild(Acord80RVAddInt);
							Acord80RVAddInt.SetAttribute(fieldType,fieldTypeMultiple);
							Acord80RVAddInt.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD80RVADDLINT"));
							Acord80RVAddInt.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD80RVADDLINT"));
							Acord80RVAddInt.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD80RVADDLINTEXTN"));
							Acord80RVAddInt.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD80RVADDLINTEXTN"));
							#endregion
						}

						int RVAddIntRowCounter=0;
						string strAddIntExtnTxt = "";
						foreach (DataRow AddlRow in DSTmpRVAddlInt.Tables[0].Rows)
						{
							if (RVRowCounter%2 == 0)
								strAddIntExtnTxt="";
							else
								strAddIntExtnTxt="1";
							
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page
								XmlElement DecPageRVAddIntElement;
								DecPageRVAddIntElement =  AcordPDFXML.CreateElement("RVADDLINTINFO");
								DecPageRVAddInt.AppendChild(DecPageRVAddIntElement);
								DecPageRVAddIntElement.SetAttribute(fieldType,fieldTypeNormal);
								DecPageRVAddIntElement.SetAttribute(id,RVAddIntRowCounter.ToString());

								DecPageRVAddIntElement.InnerXml = DecPageRVAddIntElement.InnerXml +  "<RVADLINTNUMBER" + strAddIntExtnTxt + " " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["ADD_INT_ID"].ToString()) + "</RVADLINTNUMBER" + strAddIntExtnTxt + ">";
								DecPageRVAddIntElement.InnerXml = DecPageRVAddIntElement.InnerXml +  "<RVADLINTNAMEADDRESS" + strAddIntExtnTxt + " " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["ADDLINT_NAME"].ToString()) + "</RVADLINTNAMEADDRESS" + strAddIntExtnTxt + ">";
								DecPageRVAddIntElement.InnerXml = DecPageRVAddIntElement.InnerXml +  "<RVADLINTNATUREINT" + strAddIntExtnTxt + " " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["NATUREOFINT"].ToString()) + "</RVADLINTNATUREINT" + strAddIntExtnTxt + ">";
								DecPageRVAddIntElement.InnerXml = DecPageRVAddIntElement.InnerXml +  "<RVADLINTLOANNUM" + strAddIntExtnTxt + " " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["LOAN_REF_NUMBER"].ToString()) + "</RVADLINTLOANNUM" + strAddIntExtnTxt + ">";
								#endregion
							}
							else if (gStrPdfFor == PDFForAcord)
							{
								#region Acord 80
								XmlElement Acord80RVAddIntElement;
								Acord80RVAddIntElement =  AcordPDFXML.CreateElement("RVADDLINTINFO");
								Acord80RVAddInt.AppendChild(Acord80RVAddIntElement);
								Acord80RVAddIntElement.SetAttribute(fieldType,fieldTypeNormal);
								Acord80RVAddIntElement.SetAttribute(id,RVAddIntRowCounter.ToString());

								Acord80RVAddIntElement.InnerXml = Acord80RVAddIntElement.InnerXml +  "<RVADLINTNUMBER" + strAddIntExtnTxt + " " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["ADD_INT_ID"].ToString()) + "</RVADLINTNUMBER" + strAddIntExtnTxt + ">";
								Acord80RVAddIntElement.InnerXml = Acord80RVAddIntElement.InnerXml +  "<RVADLINTNAMEADDRESS" + strAddIntExtnTxt + " " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["ADDLINT_NAME"].ToString()) + "</RVADLINTNAMEADDRESS" + strAddIntExtnTxt + ">";
								Acord80RVAddIntElement.InnerXml = Acord80RVAddIntElement.InnerXml +  "<RVADLINTNATUREINT" + strAddIntExtnTxt + " " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["NATUREOFINT"].ToString()) + "</RVADLINTNATUREINT" + strAddIntExtnTxt + ">";
								Acord80RVAddIntElement.InnerXml = Acord80RVAddIntElement.InnerXml +  "<RVADLINTLOANNUM" + strAddIntExtnTxt + " " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["LOAN_REF_NUMBER"].ToString()) + "</RVADLINTLOANNUM" + strAddIntExtnTxt + ">";
								#endregion
							}
							RVAddIntRowCounter++;
						}
					}
					#endregion

					#region Operators
					DataSet DSTmpRVOperators = new DataSet();
					//DSTmpRVOperators = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFOperatorDtls " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
					if (DSTempOperators.Tables[0].Rows.Count>0)
					{
						XmlElement DecPageRVOperator;
						DecPageRVOperator = AcordPDFXML.CreateElement("RVOPERATOR");

						XmlElement Acord80RVOperator;
						Acord80RVOperator = AcordPDFXML.CreateElement("RVOPERATOR");
						if (gStrPdfFor == PDFForDecPage)
						{
							#region Elements For Dec Page
							DecPageRVElement.AppendChild(DecPageRVOperator);
							DecPageRVOperator.SetAttribute(fieldType,fieldTypeMultiple);
							DecPageRVOperator.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGERVOPERATOR"));
							DecPageRVOperator.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGERVOPERATOR"));
							DecPageRVOperator.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGERVOPERATOREXTN"));
							DecPageRVOperator.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGERVOPERATOREXTN"));
							#endregion
						}
						else if (gStrPdfFor == PDFForAcord)
						{
							#region Elements For Acord 80 RV
							Acord80RVElement.AppendChild(Acord80RVOperator);
							Acord80RVOperator.SetAttribute(fieldType,fieldTypeMultiple);
							Acord80RVOperator.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD80RVOPERATOR"));
							Acord80RVOperator.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD80RVOPERATOR"));
							Acord80RVOperator.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD80RVOPERATOREXTN"));
							Acord80RVOperator.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD80RVOPERATOREXTN"));
							#endregion
						}

						int RVOprRowCounter=0;
						foreach (DataRow AddlRow in DSTempOperators.Tables[0].Rows)
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page
								XmlElement DecPageRVOperatorElement;
								DecPageRVOperatorElement =  AcordPDFXML.CreateElement("RVADDLINTINFO");
								DecPageRVOperator.AppendChild(DecPageRVOperatorElement);
								DecPageRVOperatorElement.SetAttribute(fieldType,fieldTypeNormal);
								DecPageRVOperatorElement.SetAttribute(id,RVOprRowCounter.ToString());

								DecPageRVOperatorElement.InnerXml = DecPageRVOperatorElement.InnerXml +  "<RVOPERATORNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["DRIVER_NAME"].ToString()) + "</RVOPERATORNAME>";
								DecPageRVOperatorElement.InnerXml = DecPageRVOperatorElement.InnerXml +  "<RVOPERATORLICNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["DRIVER_DRIV_LIC"].ToString()) + "</RVOPERATORLICNUM>";
								DecPageRVOperatorElement.InnerXml = DecPageRVOperatorElement.InnerXml +  "<RVOPERATORDOB " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["DRIVER_DOB"].ToString()) + "</RVOPERATORDOB>";
								#endregion
							}
							else if (gStrPdfFor == PDFForAcord)
							{
								#region Acord 80
								XmlElement Acord80RVOperatorElement;
								Acord80RVOperatorElement =  AcordPDFXML.CreateElement("RVADDLINTINFO");
								Acord80RVOperator.AppendChild(Acord80RVOperatorElement);
								Acord80RVOperatorElement.SetAttribute(fieldType,fieldTypeNormal);
								Acord80RVOperatorElement.SetAttribute(id,RVOprRowCounter.ToString());

								Acord80RVOperatorElement.InnerXml = Acord80RVOperatorElement.InnerXml +  "<RVOPERATORNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["DRIVER_NAME"].ToString()) + "</RVOPERATORNAME>";
								Acord80RVOperatorElement.InnerXml = Acord80RVOperatorElement.InnerXml +  "<RVOPERATORLICNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["DRIVER_DRIV_LIC"].ToString()) + "</RVOPERATORLICNUM>";
								Acord80RVOperatorElement.InnerXml = Acord80RVOperatorElement.InnerXml +  "<RVOPERATORDOB " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["DRIVER_DOB"].ToString()) + "</RVOPERATORDOB>";
								Acord80RVOperatorElement.InnerXml = Acord80RVOperatorElement.InnerXml +  "<RVOPERATORSTATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddlRow["STATE_NAME"].ToString()) + "</RVOPERATORSTATE>";
								#endregion
							}
							RVOprRowCounter++;
						}
					}
					#endregion

					RVRowCounter++;
				}
			}
		}
		#endregion
		#region
		private void createScheduleEquipXml(ref XmlElement BoatElementDecPage)
		{

			#region EQUIPMENT ROOT ELEMENT DECLARATION
			XmlElement EquipmentRootElement;
			DataSet BoattmpDataSet;
			DataSet DsSchprem;
			EquipmentRootElement = AcordPDFXML.CreateElement("EQUIPMENT");
			BoatElementDecPage.AppendChild(EquipmentRootElement);
			//DecPageRootElement.AppendChild(EquipmentRootElement);
			EquipmentRootElement.SetAttribute(fieldType,fieldTypeMultiple);
			EquipmentRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEQUIPMENT"));
			EquipmentRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEQUIPMENT"));
			EquipmentRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEQUIPMENTEXTN"));
			EquipmentRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEQUIPMENTEXTN"));
			#endregion

			int intTmpCtr1 = 0,Schprem=0,schRskPrm=0;
			gobjWrapper.ClearParameteres();
			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			BoattmpDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFSchEquipmentsDetails");
			gobjWrapper.ClearParameteres();

			gobjWrapper.AddParameter("@CUSTOMER_ID",gStrClientID);
			gobjWrapper.AddParameter("@POL_ID",gStrPolicyId);
			gobjWrapper.AddParameter("@POL_VERSION_ID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DsSchprem = gobjWrapper.ExecuteDataSet("PROC_GetScheduleEquip_Premium");
			gobjWrapper.ClearParameteres();
			
			foreach(DataRow row in DsSchprem.Tables[0].Rows)
			{
				if(row["COVERAGE_PREMIUM"].ToString()!="")
					Schprem += int.Parse(row["COVERAGE_PREMIUM"].ToString());
			}
			//BoattmpDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFSchEquipmentsDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			foreach(DataRow EquipmentDetails in BoattmpDataSet.Tables[0].Rows)
			{
				
				#region EQUIPMENT ELEMENT DECLARATION
				XmlElement EquipmentElement;
				EquipmentElement = AcordPDFXML.CreateElement("EQUIPMENTINFO");
				EquipmentRootElement.AppendChild(EquipmentElement);
				EquipmentElement.SetAttribute(fieldType,fieldTypeNormal);
				EquipmentElement.SetAttribute(id,intTmpCtr1.ToString());
				#endregion
				
				EquipmentElement.InnerXml += "<SCHEQUPNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(EquipmentDetails["EQUIP_NO"].ToString()) + "</SCHEQUPNUMBER>";
				EquipmentElement.InnerXml += "<SCHEQUPDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(EquipmentDetails["EQUIPDESC"].ToString()) + "</SCHEQUPDESC>";
				EquipmentElement.InnerXml += "<SCHEQUPSERIALNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(EquipmentDetails["SERIAL_NO"].ToString()) + "</SCHEQUPSERIALNUM>";
				EquipmentElement.InnerXml += "<SCHEQUPELECTRONIC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(EquipmentDetails["EQUIPMENT_TYPE"].ToString()) + "</SCHEQUPELECTRONIC>";
				EquipmentElement.InnerXml += "<SCHEQUPLIMIT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(EquipmentDetails["INSURED_VALUE"].ToString()) + "</SCHEQUPLIMIT>";
				EquipmentElement.InnerXml += "<SCHEQUPDED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(EquipmentDetails["EQUIP_AMOUNT"].ToString()) + "</SCHEQUPDED>";
				foreach(DataRow row in DsSchprem.Tables[0].Rows)
				{
					if(row["EQUIP_NO"].ToString()==EquipmentDetails["EQUIP_NO"].ToString()  && row["COVERAGE_PREMIUM"].ToString()!="")
						schRskPrm = int.Parse(row["COVERAGE_PREMIUM"].ToString());
				}				
				EquipmentElement.InnerXml +="<SCHEQUPPREM " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(DollarFormat(double.Parse(schRskPrm.ToString()))) + "</SCHEQUPPREM>";
				EquipmentElement.InnerXml += "<SCHEQUPPREMTOT " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(DollarFormat(double.Parse(Schprem.ToString()))) + "</SCHEQUPPREMTOT>";
				intTmpCtr1 ++;
			}
			intTmpCtr1 = 0;
		}
		#endregion
		#region Dec Page WaterCraft Information
		private void createWaterCraftXml()
		{
			//LoadRateXML("HOME-BOAT",DSTempRateDataSet);
			//int BoatCtr = 0;

			if (gStrPdfFor == PDFForDecPage)
			{
				int intBoatCtr=0,intTmpCtr=0;
				DataSet BoattmpDataSet;

				gobjWrapper.ClearParameteres();
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempDataSet = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS");
				//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
				
				
				if(DSTempDataSet.Tables[0].Rows.Count>0)
				{
					#region declaration Boat Root Element DecPage
					XmlElement BoatRootElementDecPage;
					BoatRootElementDecPage = AcordPDFXML.CreateElement("BOAT");
					DecPageRootElement.AppendChild(BoatRootElementDecPage);

					BoatRootElementDecPage.SetAttribute(fieldType,fieldTypeMultiple);
					BoatRootElementDecPage.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEBOAT"));
					BoatRootElementDecPage.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBOAT"));
					BoatRootElementDecPage.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEBOATEXTN"));
					BoatRootElementDecPage.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBOATEXTN"));
					#endregion

					string sumTtl="0";
					
						getPhotoAttach	=	DSTempDataSet.Tables[0].Rows[0]["PHOTO_ATTACHED"].ToString();
										
						getMarineSurvey	=	DSTempDataSet.Tables[0].Rows[0]["MARINE_SURVEY"].ToString(); 
					

					if(gStrtemp != "final")
					{
						foreach (XmlNode SumTotalNode in GetSumTotalPremium())
						{
							sumTtl= getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString() ;
						}
					
					}
					else 
					{
						double sumTtl1 = 0.0;
						foreach(DataRow BoatDetails in DSTempDataSet.Tables[0].Rows)
						{
							gobjWrapper.ClearParameteres();
							gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
							gobjWrapper.AddParameter("@POLID",gStrPolicyId);
							gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
							gobjWrapper.AddParameter("@BOATID",BoatDetails["BOAT_ID"].ToString());
							gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
							DataSet BoattmpDataSet1 = gobjWrapper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails"); 
							//DataSet BoattmpDataSet1 = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + BoatDetails["BOAT_ID"].ToString()  + ",'" + gStrCalledFrom + "'");
							
							sumTtl1=GetPremiumSch(BoattmpDataSet1,"MSE","COVERAGE_PREMIUM");
							sumTtl1 += GetPremiumSch(BoattmpDataSet1,"B","COVERAGE_PREMIUM");
							sumTtl1 += GetPremiumSch(BoattmpDataSet1,"T","COVERAGE_PREMIUM");

							string sumUn = GetPremiumAll(BoattmpDataSet1, "BOAT_UNATTACH_PREMIUM", BoatDetails["BOAT_ID"].ToString());
							if (sumUn != "")
								sumTtl1 += double.Parse(sumUn.Replace("$",""));


						}
						sumTtl = sumTtl1.ToString();
					}
				

					foreach(DataRow BoatDetails in DSTempDataSet.Tables[0].Rows)
					{
						htpremium.Clear(); 
						foreach (XmlNode PremiumNode in GetPremium(BoatDetails["BOAT_ID"].ToString()))
						{
							if(!htpremium.Contains(getAttributeValue(PremiumNode,"COMPONENT_CODE")))
							htpremium.Add(getAttributeValue(PremiumNode,"COMPONENT_CODE"),getAttributeValue(PremiumNode,"STEPPREMIUM"));
						}

						#region Boat Child Element Dec Page
						XmlElement BoatElementSuppliment;
						BoatElementSuppliment	= AcordPDFXML.CreateElement("BOATINFO");

						XmlElement BoatElementDecPage;
						BoatElementDecPage = AcordPDFXML.CreateElement("BOATINFO");
						BoatRootElementDecPage.AppendChild(BoatElementDecPage);

						BoatElementDecPage.SetAttribute(fieldType,fieldTypeNormal);
						BoatElementDecPage.SetAttribute(id,intBoatCtr.ToString());
						#endregion

						#region Boat Root Element Node XML
						if(string.Compare(gStrCalledFrom,"Policy",true)==0)
						{
							BoatElementDecPage.InnerXml += "<WATERCRAFT_POLICYNO " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</WATERCRAFT_POLICYNO>";
							BoatElementDecPage.InnerXml += "<WATERCRAFT_POLICYPERIODFROM " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</WATERCRAFT_POLICYPERIODFROM>";
							BoatElementDecPage.InnerXml += "<WATERCRAFT_POLICYPERIODTO " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</WATERCRAFT_POLICYPERIODTO>";
						}
						BoatElementDecPage.InnerXml += "<WATERCRAFT_REASON " + fieldType + "=\"" + fieldTypeText + "\"></WATERCRAFT_REASON>";
						BoatElementDecPage.InnerXml += "<copyTo " + fieldType + "=\"" + fieldTypeText + "\">" + CopyTo + "</copyTo>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_PRIMARYCONTACTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</WATERCRAFT_PRIMARYCONTACTNAME>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_PRIMARYCONTACTADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantAddress + "</WATERCRAFT_PRIMARYCONTACTADDRESS>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_PRIMARYCONTACTCITY " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantCityStZip + "</WATERCRAFT_PRIMARYCONTACTCITY>";
						if(BoatDetails["LOCADD"].ToString()!="")
						{
							BoatElementDecPage.InnerXml += "<WATERCRAFT_LOCATIONADDRESS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetails["LOCADD"].ToString())+"</WATERCRAFT_LOCATIONADDRESS>"; 
						}
						if(BoatDetails["LOCCITY"].ToString()!="")
						{
							BoatElementDecPage.InnerXml += "<WATERCRAFT_LOCATIONCITYSTZIP " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetails["LOCCITY"].ToString())+"</WATERCRAFT_LOCATIONCITYSTZIP>"; 
						}
						BoatElementDecPage.InnerXml += "<WATERCRAFT_AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyName + "</WATERCRAFT_AGENCYNAME>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyAddress + "</WATERCRAFT_AGENCYADDRESS>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_AGENCYCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCitySTZip + "</WATERCRAFT_AGENCYCITYSTATEZIP>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_AGENCYPHONENO " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyPhoneNumber + "</WATERCRAFT_AGENCYPHONENO>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCode + "</WATERCRAFT_AGENCYCODE>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencySubCode + "</WATERCRAFT_AGENCYSUBCODE>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_AGENCYBILLING " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyBilling + "</WATERCRAFT_AGENCYBILLING>";
						#endregion
				
						#region Boat Element Node XML
						BoatElementDecPage.InnerXml += "<WATERCRAFT_USE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["WATERS_NAVIGATED_USE"].ToString()) + "</WATERCRAFT_USE>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_TERRITORY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["BOAT_TERRITORY"].ToString()) + "</WATERCRAFT_TERRITORY>";
						
						BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATNO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["BOAT_NO"].ToString()) + "</WATERCRAFT_BOATNO>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATTYPE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["BOATTYPE"].ToString()) + "</WATERCRAFT_BOATTYPE>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATPURCHASEYEAR " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["BOAT_YEAR"].ToString())+ "</WATERCRAFT_BOATPURCHASEYEAR>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATMODEL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["BOAT_MAKE_MODEL"].ToString()) + "</WATERCRAFT_BOATMODEL>";
						if(BoatDetails["BOAT_INCHES"].ToString()=="")
						{
							BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATLENGTH " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["BOAT_LENGTH"].ToString() +"'") + "</WATERCRAFT_BOATLENGTH>";
						}
						else
						{
							BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATLENGTH " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["BOAT_LENGTH"].ToString() +"'" + BoatDetails["BOAT_INCHES"].ToString() +"''") + "</WATERCRAFT_BOATLENGTH>";
						
						}
						BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATCC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["HORSE_POWER"].ToString()) + "</WATERCRAFT_BOATCC>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATSERIALNO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["HULL_ID_NO"].ToString()) + "</WATERCRAFT_BOATSERIALNO>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATCONSTRUCTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["HULL_MATERIAL"].ToString()) + "</WATERCRAFT_BOATCONSTRUCTION>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATINSURINGVALUE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["PRESENT_VALUE"].ToString()) + "</WATERCRAFT_BOATINSURINGVALUE>";
						BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATDED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(BoatDetails["DEDUCTIBLE"].ToString()) + "</WATERCRAFT_BOATDED>";
						

						#region Coverages
						gobjWrapper.ClearParameteres();
						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						gobjWrapper.AddParameter("@BOATID",BoatDetails["BOAT_ID"].ToString());
						gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						BoattmpDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails"); 
						//BoattmpDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + BoatDetails["BOAT_ID"].ToString()  + ",'" + gStrCalledFrom + "'");
						//double dblSumUnattach=0;
						foreach(DataRow CoverageDetails in BoattmpDataSet.Tables[0].Rows)
						{
							switch(CoverageDetails["COV_CODE"].ToString())
							{
								case "EBPPDACV":
									BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(GetPremium(BoattmpDataSet, CoverageDetails["COV_CODE"].ToString()))+"</WATERCRAFT_BOATPREMIUM>";
//									if(htpremium.Contains("BOAT_PD"))
//									{
//										gstrGetPremium = htpremium["BOAT_PD"].ToString();
//										gintGetindex = gstrGetPremium.IndexOf(".");
//										if(gintGetindex!= -1)
//										{
//											BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["BOAT_PD"].ToString() + ".00")+"</WATERCRAFT_BOATPREMIUM>";
//										}
//										else
//											BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["BOAT_PD"].ToString())+"</WATERCRAFT_BOATPREMIUM>";
//
//									}									
									break;
								case "EBPPDAV":
									BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(GetPremium(BoattmpDataSet, CoverageDetails["COV_CODE"].ToString()))+"</WATERCRAFT_BOATPREMIUM>";
//									if(htpremium.Contains("BOAT_PD"))
//									{
//										gstrGetPremium = htpremium["BOAT_PD"].ToString();
//										gintGetindex = gstrGetPremium.IndexOf(".");
//										if(gintGetindex!= -1)
//										{
//											BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["BOAT_PD"].ToString() + ".00")+"</WATERCRAFT_BOATPREMIUM>";
//										}
//										else
//											BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["BOAT_PD"].ToString())+"</WATERCRAFT_BOATPREMIUM>";
//
//									}									
									break;
								case "EBPPDJ":
									BoatElementDecPage.InnerXml += "<WATERCRAFT_BOATPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(GetPremium(BoattmpDataSet, CoverageDetails["COV_CODE"].ToString()))+"</WATERCRAFT_BOATPREMIUM>";
									break;
								case "LCCSL":
									BoatElementDecPage.InnerXml += "<WATERCRAFT_PERSONALLIABILITY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString()) + "</WATERCRAFT_PERSONALLIABILITY>";
									BoatElementDecPage.InnerXml += "<WATERCRAFT_PERSONALLIABILITYLIMIT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString()+ "  Each Occurence" )+ "</WATERCRAFT_PERSONALLIABILITYLIMIT>";
									BoatElementDecPage.InnerXml += "<WATERCRAFT_PERSONALLIABILITYPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(GetPremium(BoattmpDataSet, CoverageDetails["COV_CODE"].ToString()))+"</WATERCRAFT_PERSONALLIABILITYPREMIUM>";										
//									if(htpremium.Contains("BOAT_ LIABILITY_PREMIUM"))
//									{
//										
//										gstrGetPremium = htpremium["BOAT_ LIABILITY_PREMIUM"].ToString();
//										gintGetindex = gstrGetPremium.IndexOf(".");
//										if(gintGetindex!= -1)
//										{
//											BoatElementDecPage.InnerXml += "<WATERCRAFT_PERSONALLIABILITYPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["BOAT_ LIABILITY_PREMIUM"].ToString() + ".00")+"</WATERCRAFT_PERSONALLIABILITYPREMIUM>";										
//										}
//										else
//											BoatElementDecPage.InnerXml += "<WATERCRAFT_PERSONALLIABILITYPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["BOAT_ LIABILITY_PREMIUM"].ToString())+"</WATERCRAFT_PERSONALLIABILITYPREMIUM>";
//
//									}									
									break;
								case "MCPAY":
									BoatElementDecPage.InnerXml += "<WATERCRAFT_MEDICALPAYMENTS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString()) + "</WATERCRAFT_MEDICALPAYMENTS>";
									BoatElementDecPage.InnerXml += "<WATERCRAFT_MEDICALPAYMENTSLIMIT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString()+ "  Per Person 25000.00 Each Occurence") + "</WATERCRAFT_MEDICALPAYMENTSLIMIT>";
									BoatElementDecPage.InnerXml += "<WATERCRAFT_MEDICALPAYMENTSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(GetPremium(BoattmpDataSet, CoverageDetails["COV_CODE"].ToString()))+"</WATERCRAFT_MEDICALPAYMENTSPREMIUM>";
//									if(htpremium.Contains("BOAT_MP_PREMIUM"))
//									{										
//										gstrGetPremium = htpremium["BOAT_MP_PREMIUM"].ToString();
//										gintGetindex = gstrGetPremium.IndexOf(".");
//										if(gintGetindex!= -1)
//										{
//											BoatElementDecPage.InnerXml += "<WATERCRAFT_MEDICALPAYMENTSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["BOAT_MP_PREMIUM"].ToString() + ".00")+"</WATERCRAFT_MEDICALPAYMENTSPREMIUM>";
//										}
//										else
//											BoatElementDecPage.InnerXml += "<WATERCRAFT_MEDICALPAYMENTSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["BOAT_MP_PREMIUM"].ToString())+"</WATERCRAFT_MEDICALPAYMENTSPREMIUM>";
//
//									}									
									break;
								case "UMBCS":
									BoatElementDecPage.InnerXml += "<WATERCRAFT_UNINSUREDBOATERS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString()) + "</WATERCRAFT_UNINSUREDBOATERS>";
									BoatElementDecPage.InnerXml += "<WATERCRAFT_UNINSUREDBOATERSLIMIT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString()+"  Each Occurence/Each Unit ") + "</WATERCRAFT_UNINSUREDBOATERSLIMIT>";
									BoatElementDecPage.InnerXml += "<WATERCRAFT_UNINSUREDBOATERSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(GetPremium(BoattmpDataSet, CoverageDetails["COV_CODE"].ToString()))+"</WATERCRAFT_UNINSUREDBOATERSPREMIUM>";
//									if(htpremium.Contains("BOAT_UB_PREMIUM"))
//									{
//										gstrGetPremium = htpremium["BOAT_UB_PREMIUM"].ToString();
//										gintGetindex = gstrGetPremium.IndexOf(".");
//										if(gintGetindex == -1)
//										{
//											BoatElementDecPage.InnerXml += "<WATERCRAFT_UNINSUREDBOATERSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UB_PREMIUM"].ToString() + ".00")+"</WATERCRAFT_UNINSUREDBOATERSPREMIUM>";
//										}
//										else
//											BoatElementDecPage.InnerXml += "<WATERCRAFT_UNINSUREDBOATERSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UB_PREMIUM"].ToString())+"</WATERCRAFT_UNINSUREDBOATERSPREMIUM>";
//
//									}
									
									break;

//								case "WP865":
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_WE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString()) + "</WATERCRAFT_WE>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_WE_NUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString()) + "</WATERCRAFT_WE_NUMBER>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_WE_DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString()) + "</WATERCRAFT_WE_DATE>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_WE_LIMIT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString()) + "</WATERCRAFT_WE_LIMIT>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_WE_DED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString()) + "</WATERCRAFT_WE_DED>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_WE_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(GetPremium(BoattmpDataSet, CoverageDetails["COV_CODE"].ToString()))+" Included"+"</WATERCRAFT_WE_PREMIUM>";
//									if(htpremium.Contains("BOAT_TOW"))
//									{	
//										gstrGetPremium = htpremium["BOAT_TOW"].ToString();
//										gintGetindex = gstrGetPremium.IndexOf(".");
//										if(gintGetindex == -1)
//										{
//											BoatElementDecPage.InnerXml += "<WATERCRAFT_WE_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["BOAT_TOW"].ToString() + ".00")+" Included"+"</WATERCRAFT_WE_PREMIUM>";
//										}
//										else
//											BoatElementDecPage.InnerXml += "<WATERCRAFT_WE_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["BOAT_TOW"].ToString())+" Included"+"</WATERCRAFT_WE_PREMIUM>";
//
//									}
									
//									break;
//
//								case "BTESC":
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_BT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString()) + "</WATERCRAFT_BT>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_BT_NUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + " " + "</WATERCRAFT_BT_NUMBER>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_BT_DATE " + fieldType + "=\"" + fieldTypeText + "\">" + " " + "</WATERCRAFT_BT_DATE>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_BT_LIMIT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString()) + "</WATERCRAFT_BT_LIMIT>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_BT_DED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString()) + "</WATERCRAFT_BT_DED>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_BT_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(GetPremium(BoattmpDataSet, CoverageDetails["COV_CODE"].ToString()))+"</WATERCRAFT_BT_PREMIUM>";
//									if(htpremium.Contains("BOAT_TOW"))
//									{	
//										gstrGetPremium = htpremium["BOAT_TOW"].ToString();
//										gintGetindex = gstrGetPremium.IndexOf(".");
//										if(gintGetindex == -1)
//										{
//											BoatElementDecPage.InnerXml += "<WATERCRAFT_BT_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["BOAT_TOW"].ToString() + ".00")+" Included"+"</WATERCRAFT_BT_PREMIUM>";
//										}
//										else
//											BoatElementDecPage.InnerXml += "<WATERCRAFT_BT_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["BOAT_TOW"].ToString())+" Included"+"</WATERCRAFT_BT_PREMIUM>";
//
//									}
									
//									break;
//								case "EBIUE":
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_UE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString()) + "</WATERCRAFT_UE>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_UE_NUMBER " + fieldType + "=\"" + fieldTypeText + "\">" +" "+ "</WATERCRAFT_UE_NUMBER>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_UE_DATE " + fieldType + "=\"" + fieldTypeText + "\">" + " " + "</WATERCRAFT_UE_DATE>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_UE_LIMIT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString()) + "</WATERCRAFT_UE_LIMIT>";
//									
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_UE_DED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString()) + "</WATERCRAFT_UE_DED>";
//									
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_UE_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(GetPremium(BoattmpDataSet, CoverageDetails["COV_CODE"].ToString()))+"</WATERCRAFT_UE_PREMIUM>";
//									if(htpremium.Contains("BOAT_UNATTACH_PREMIUM"))
//									{										
//										dblSumUnattach+=int.Parse(htpremium["BOAT_UNATTACH_PREMIUM"].ToString());
//										BoatElementDecPage.InnerXml += "<BOATCOVENDMTPREM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(dblSumUnattach+ ".00")+"</BOATCOVENDMTPREM>";
//									}
									
//									break;
//								case "EBSCEAV":
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_AV " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString()) + "</WATERCRAFT_AV>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_AV_NUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString()) + "</WATERCRAFT_AV_NUMBER>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_AV_DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString()) + "</WATERCRAFT_AV_DATE>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_AV_LIMIT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString()) + "</WATERCRAFT_AV_LIMIT>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_AV_DED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString()) + "</WATERCRAFT_AV_DED>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_AV_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(GetPremium(BoattmpDataSet, CoverageDetails["COV_CODE"].ToString()))+"</WATERCRAFT_AV_PREMIUM>";
//									if(htpremium.Contains("BOAT_AV100"))
//									{
//										gstrGetPremium = htpremium["BOAT_AV100"].ToString();
//										gintGetindex = gstrGetPremium.IndexOf(".");
//										if(gintGetindex ==  -1)
//										{
//											BoatElementDecPage.InnerXml += "<WATERCRAFT_AV_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["BOAT_AV100"].ToString() + ".00")+"</WATERCRAFT_AV_PREMIUM>";
//										}
//										else
//											BoatElementDecPage.InnerXml += "<WATERCRAFT_AV_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["BOAT_AV100"].ToString() )+"</WATERCRAFT_AV_PREMIUM>";
//
//									}
									
//									break;
//								case "EBSMECE":
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_CE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString()) + "</WATERCRAFT_CE>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_CE_NUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString()) + "</WATERCRAFT_CE_NUMBER>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_CE_DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString()) + "</WATERCRAFT_CE_DATE>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_CE_LIMIT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString()) + "</WATERCRAFT_CE_LIMIT>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_CE_DED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString()) + "</WATERCRAFT_CE_DED>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_CE_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(GetPremium(BoattmpDataSet, CoverageDetails["COV_CODE"].ToString()))+"</WATERCRAFT_CE_PREMIUM>";
//									if(htpremium.Contains("BOAT_OP720"))
//									{
//										dblSumUnattach+=int.Parse(htpremium["BOAT_OP720"].ToString());
//										BoatElementDecPage.InnerXml += "<BOATCOVENDMTPREM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(dblSumUnattach+ ".00")+"</BOATCOVENDMTPREM>";
//									}								
									
//									break;
//								case "OP400":
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_PW " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString()) + "</WATERCRAFT_PW>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_PW_NUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString()) + "</WATERCRAFT_PW_NUMBER>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_PW_DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString()) + "</WATERCRAFT_PW_DATE>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_PW_LIMIT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString()) + "</WATERCRAFT_PW_LIMIT>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_PW_DED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString()) + "</WATERCRAFT_PW_DED>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_PW_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(GetPremium(BoattmpDataSet, CoverageDetails["COV_CODE"].ToString()))+"</WATERCRAFT_PW_PREMIUM>";
//									if(htpremium.Contains("BOAT_OP400"))
//									{
//										gstrGetPremium = htpremium["BOAT_OP400"].ToString();
//										gintGetindex = gstrGetPremium.IndexOf(".");
//										if(gintGetindex == -1)
//										{
//											BoatElementDecPage.InnerXml += "<WATERCRAFT_PW_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["BOAT_OP400"].ToString() + ".00")+" Included"+"</WATERCRAFT_PW_PREMIUM>";
//										}
//										else
//											BoatElementDecPage.InnerXml += "<WATERCRAFT_PW_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["BOAT_OP400"].ToString())+" Included"+"</WATERCRAFT_PW_PREMIUM>";
//
//
//									}
									
//									break;
//								case "EBSMWL":
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_LP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString()) + "</WATERCRAFT_LP>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_LP_NUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString()) + "</WATERCRAFT_LP_NUMBER>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_LP_DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString()) + "</WATERCRAFT_LP_DATE>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_LP_LIMIT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString()) + "</WATERCRAFT_LP_LIMIT>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_LP_DED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString()) + "</WATERCRAFT_LP_DED>";
//									BoatElementDecPage.InnerXml += "<WATERCRAFT_LP_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(GetPremium(BoattmpDataSet, CoverageDetails["COV_CODE"].ToString()))+"</WATERCRAFT_LP_PREMIUM>";
//									if(htpremium.Contains("BOAT_OP900"))
//									{
//										dblSumUnattach+=int.Parse(htpremium["BOAT_OP900"].ToString());
//										BoatElementDecPage.InnerXml += "<BOATCOVENDMTPREM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(dblSumUnattach + ".00")+"</BOATCOVENDMTPREM>";
//									}
									
//									break;
							}

						}

						#region  Forms and Endorsements
						#region Declaration Page Boat Endorsements
						XmlElement DecPageHomeEndmts;
						DecPageHomeEndmts = AcordPDFXML.CreateElement("BOATENDORSEMENTS");
						BoatElementDecPage.AppendChild(DecPageHomeEndmts);
						DecPageHomeEndmts.SetAttribute(fieldType,fieldTypeMultiple);
						DecPageHomeEndmts.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHOMEEND"));
						DecPageHomeEndmts.SetAttribute(PrimPDFBlocks,"6");
						DecPageHomeEndmts.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHOMEENDEXTN"));
						DecPageHomeEndmts.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHOMEENDEXTN"));
						#endregion

						int DecPageEndCtr = 0;
						foreach(DataRow CoverageDetails in BoattmpDataSet.Tables[0].Rows)
						{
							string CovCode = CoverageDetails["COV_CODE"].ToString();
							if (CovCode!="LCCSL" && CovCode!="MCPAY" && CovCode!="UMBCS")
							{	
								if(CovCode=="EBIUE" && BoatDetails["BOAT_ID"].ToString() != "1")
									continue;
								XmlElement DecPageBoatEndmtElement;
								DecPageBoatEndmtElement = AcordPDFXML.CreateElement("BOATENDORSEMENTSINFO");
								DecPageHomeEndmts.AppendChild(DecPageBoatEndmtElement);
								DecPageBoatEndmtElement.SetAttribute(fieldType,fieldTypeNormal);
								DecPageBoatEndmtElement.SetAttribute(id,(DecPageEndCtr).ToString() );

								DecPageBoatEndmtElement.InnerXml= DecPageBoatEndmtElement.InnerXml + "<HM_DECPAGE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("HOMEOWNERS  DECLARATION")+"</HM_DECPAGE>";
								DecPageBoatEndmtElement.InnerXml= DecPageBoatEndmtElement.InnerXml + "<WAT_SECTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("WATERCRAFT SECTION")+"</WAT_SECTION>";
								DecPageBoatEndmtElement.InnerXml= DecPageBoatEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</HM_ENDFORM>";
								DecPageBoatEndmtElement.InnerXml= DecPageBoatEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
								DecPageBoatEndmtElement.InnerXml= DecPageBoatEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
								if(CoverageDetails["LIMIT_1"].ToString().Trim() != "0.00")
									DecPageBoatEndmtElement.InnerXml= DecPageBoatEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
								DecPageBoatEndmtElement.InnerXml= DecPageBoatEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</HM_ENDDEDUCTIBLE>";
								DecPageBoatEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(BoattmpDataSet, CovCode))+"</HM_ENDPREMIUM>";
								DecPageEndCtr++;
							}
						}
						#endregion
						#endregion

						#region Creating Credit And Surcharge Xml
						if (isRateGenerated)
						{
							int CreditSurchRowCounter = 0;
							string AdditionalExtnTxt="";

							#region Credits
							XmlElement DecPageBoatCredit;
							DecPageBoatCredit = AcordPDFXML.CreateElement("BOATCREDIT");

							
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								BoatElementDecPage.AppendChild(DecPageBoatCredit);
								DecPageBoatCredit.SetAttribute(fieldType,fieldTypeMultiple);
								DecPageBoatCredit.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECREDIT"));
								DecPageBoatCredit.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGECREDIT"));
								DecPageBoatCredit.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGECREDITEXTN"));
								DecPageBoatCredit.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGECREDITEXTN"));
								#endregion
							}
							

							foreach (XmlNode CreditNode in GetCredits(BoatDetails["BOAT_ID"].ToString()))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									string step_desc = getAttributeValue(CreditNode,"STEPDESC").Replace(" ","");
									string step_prem = getAttributeValue(CreditNode,"STEPPREMIUM").Trim();

									if(step_desc.IndexOf("+0%") >0 || step_desc.IndexOf("-0%") >0 || step_prem == "0")
										continue;

									#region Dec Page
									XmlElement DecPageBoatCreditElement;
									DecPageBoatCreditElement = AcordPDFXML.CreateElement("BOATCREDITINFO");
									DecPageBoatCredit.AppendChild(DecPageBoatCreditElement);
									DecPageBoatCreditElement.SetAttribute(fieldType,fieldTypeNormal);
									DecPageBoatCreditElement.SetAttribute(id,CreditSurchRowCounter.ToString());
//
//									if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_MULTIPOL")
//										DecPageBoatCreditElement.InnerXml += "<WATERCRAFT_CREDIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n","") + "-" + BoatDetails["DESC_MULTI_POLICY_DISC_APPLIED"].ToString() ) +"</WATERCRAFT_CREDIT>"; 
//									else if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_DUC") 
//										DecPageBoatCreditElement.InnerXml += "<WATERCRAFT_CREDIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n","") + "-" + BoatDetails["DWELLING_CONST_DATE"].ToString() ) +"</WATERCRAFT_CREDIT>"; 
//									else
										DecPageBoatCreditElement.InnerXml += "<WATERCRAFT_CREDIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n","")) +"</WATERCRAFT_CREDIT>"; 

									DecPageBoatCreditElement.InnerXml += "<CREDITDISCPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</CREDITDISCPREM>"; 
									#endregion
								}
								

								CreditSurchRowCounter++;
							}
							#endregion

							CreditSurchRowCounter = 0;
							AdditionalExtnTxt="";

							#region Surcharges
							XmlElement DecPageBoatSurch;
							DecPageBoatSurch = AcordPDFXML.CreateElement("BOATSURCHARGE");

							XmlElement SupplementBoatSurch;
							SupplementBoatSurch = AcordPDFXML.CreateElement("BOATSURCHARGE");

							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								BoatElementDecPage.AppendChild(DecPageBoatSurch);
								DecPageBoatSurch.SetAttribute(fieldType,fieldTypeMultiple);
								DecPageBoatSurch.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESRCHG"));
								DecPageBoatSurch.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHG"));
								DecPageBoatSurch.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESRCHGEXTN"));
								DecPageBoatSurch.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHGEXTN"));
								#endregion
							}
							

							foreach (XmlNode CreditNode in GetSurcharges(BoatDetails["BOAT_ID"].ToString()))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									string step_desc = getAttributeValue(CreditNode,"STEPDESC").Replace(" ","");
									string step_prem = getAttributeValue(CreditNode,"STEPPREMIUM").Trim();

									if(step_desc.IndexOf("+0%") >0 || step_desc.IndexOf("-0%") >0 || step_prem == "0")
										continue;
									#region Dec Page
									XmlElement DecPageBoatSurchElement;
									DecPageBoatSurchElement = AcordPDFXML.CreateElement("BOATSURCHARGEINFO");
									DecPageBoatSurch.AppendChild(DecPageBoatSurchElement);
									DecPageBoatSurchElement.SetAttribute(fieldType,fieldTypeNormal);
									DecPageBoatSurchElement.SetAttribute(id,CreditSurchRowCounter.ToString());

									DecPageBoatSurchElement.InnerXml += "<WATERCRAFT_SURCHARGE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</WATERCRAFT_SURCHARGE>"; 
									DecPageBoatSurchElement.InnerXml += "<SURCHARGEPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</SURCHARGEPREM>"; 
									#endregion
								}
								
								CreditSurchRowCounter++;
							}
							#endregion
						}
						#endregion
			
						#region Additional Int

						#region declaration Additional Int Root Element 
						XmlElement AdditionalIntRootElement;
						AdditionalIntRootElement = AcordPDFXML.CreateElement("ADDITIONALINT");
						BoatElementDecPage.AppendChild(AdditionalIntRootElement);
						AdditionalIntRootElement.SetAttribute(fieldType,fieldTypeMultiple);
						AdditionalIntRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEADDLINT"));
						AdditionalIntRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEADDLINT"));
						AdditionalIntRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEADDLINTEXTN"));
						AdditionalIntRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEADDLINTEXTN"));
						#endregion
						gobjWrapper.ClearParameteres();
						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						gobjWrapper.AddParameter("@BOATID",BoatDetails["BOAT_ID"].ToString());
						gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						BoattmpDataSet = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS");
						//BoattmpDataSet = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + BoatDetails["BOAT_ID"].ToString()  + ",'" + gStrCalledFrom + "'");
					
						foreach(DataRow AddIntDetails in BoattmpDataSet.Tables[0].Rows)
						{
							#region declaration Additional Int Element 
							XmlElement AdditionalIntElement;
							AdditionalIntElement = AcordPDFXML.CreateElement("ADDITIONALINTINFO");
							AdditionalIntRootElement.AppendChild(AdditionalIntElement);
							AdditionalIntElement.SetAttribute(fieldType,fieldTypeNormal);
							AdditionalIntElement.SetAttribute(id,intTmpCtr.ToString());
							#endregion
				
							AdditionalIntElement.InnerXml += "<WATERCRAFT_AISERIALNO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddIntDetails["BOAT_NO"].ToString()) + "</WATERCRAFT_AISERIALNO>";
							AdditionalIntElement.InnerXml += "<ADDLINTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddIntDetails["HOLDER_NAME"].ToString()) + "</ADDLINTNAME>";
							AdditionalIntElement.InnerXml += "<ADDLINTADD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddIntDetails["ADDRESS"].ToString()) + "</ADDLINTADD>";
							AdditionalIntElement.InnerXml += "<WATERCRAFT_NATUREOFINTEREST " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddIntDetails["ADDLINTNAME"].ToString()) + "</WATERCRAFT_NATUREOFINTEREST>";
							AdditionalIntElement.InnerXml += "<ADDLINTLOANNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddIntDetails["LOAN_REF_NUMBER"].ToString()) + "</ADDLINTLOANNUM>";
							AdditionalIntElement.InnerXml += "<ADDLINTTYPEOFINT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(AddIntDetails["NATURE_OF_INTEREST"].ToString()) + "</ADDLINTTYPEOFINT>";

							intTmpCtr++;
						}
						intTmpCtr = 0;	
						#endregion

						#region Schedule equipment
			
						#region EQUIPMENT ROOT ELEMENT DECLARATION
						XmlElement EquipmentRootElement;
						EquipmentRootElement = AcordPDFXML.CreateElement("EQUIPMENT");
						BoatElementDecPage.AppendChild(EquipmentRootElement);
						EquipmentRootElement.SetAttribute(fieldType,fieldTypeMultiple);
						EquipmentRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEQUIPMENT"));
						EquipmentRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEQUIPMENT"));
						EquipmentRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEQUIPMENTEXTN"));
						EquipmentRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEQUIPMENTEXTN"));
						#endregion

						int intTmpCtr1 = 0;
						gobjWrapper.ClearParameteres();
						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						BoattmpDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFSchEquipmentsDetails");
						//BoattmpDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFSchEquipmentsDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
						foreach(DataRow EquipmentDetails in BoattmpDataSet.Tables[0].Rows)
						{
				
							#region EQUIPMENT ELEMENT DECLARATION
							XmlElement EquipmentElement;
							EquipmentElement = AcordPDFXML.CreateElement("EQUIPMENTINFO");
							EquipmentRootElement.AppendChild(EquipmentElement);
							EquipmentElement.SetAttribute(fieldType,fieldTypeNormal);
							EquipmentElement.SetAttribute(id,intTmpCtr1.ToString());
							#endregion
				
							EquipmentElement.InnerXml += "<WATERCRAFT_SERIALNO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(EquipmentDetails["EQUIP_NO"].ToString()) + "</WATERCRAFT_SERIALNO>";
							EquipmentElement.InnerXml += "<WA_SCHEQUIPDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(EquipmentDetails["EQUIPDESC"].ToString()) + "</WA_SCHEQUIPDESC>";
							EquipmentElement.InnerXml += "<WA_SCHEQUIPSERIAL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(EquipmentDetails["SERIAL_NO"].ToString()) + "</WA_SCHEQUIPSERIAL>";
							EquipmentElement.InnerXml += "<WA_SCHEQUIP_ELECTRONIC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(EquipmentDetails["EQUIPMENT_TYPE"].ToString()) + "</WA_SCHEQUIP_ELECTRONIC>";
							EquipmentElement.InnerXml += "<WA_SCHEQUIP_LIMIT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(EquipmentDetails["INSURED_VALUE"].ToString()) + "</WA_SCHEQUIP_LIMIT>";
							EquipmentElement.InnerXml += "<WA_SCHEQUIP_DED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(EquipmentDetails["EQUIP_AMOUNT"].ToString()) + "</WA_SCHEQUIP_DED>";
							EquipmentElement.InnerXml += "<WA_SCHEQUIP_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\"></WA_SCHEQUIP_PREMIUM>";
				
							intTmpCtr1 ++;
						}
						intTmpCtr1 = 0;
						#endregion

						#region Opraters
			
						#region OPRATERS ROOT ELEMENT DECLARATION
						XmlElement OperatorRootElement;
						OperatorRootElement = AcordPDFXML.CreateElement("OPERATOR");
						BoatElementDecPage.AppendChild(OperatorRootElement);
						OperatorRootElement.SetAttribute(fieldType,fieldTypeMultiple);
						OperatorRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOPERATOR"));
						OperatorRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOPERATOR"));
						OperatorRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEOPERATOREXTN"));
						OperatorRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOPERATOREXTN"));
						#endregion

//						gobjWrapper.AddParameter(@CUSTOMERID,gStrClientID);
//						gobjWrapper.AddParameter(@POLID,gStrPolicyId);
//						gobjWrapper.AddParameter(@VERSIONID,gStrPolicyVersion);
//						gobjWrapper.AddParameter(@CALLEDFROM,gStrCalledFrom);
//						BoattmpDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFOperatorDtls");
						//BoattmpDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFOperatorDtls " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
						foreach(DataRow OperatorDetails in DSTempOperators.Tables[0].Rows)
						{
				
							#region OPERATOR ELEMENT DECLARATION
							XmlElement OperatorElement;
							OperatorElement = AcordPDFXML.CreateElement("OPERATORINFO");
							OperatorRootElement.AppendChild(OperatorElement);
							OperatorElement.SetAttribute(fieldType,fieldTypeNormal);
							OperatorElement.SetAttribute(id,intTmpCtr1.ToString());
							#endregion
							string strOprSSn="";
							strOprSSn =  OperatorDetails["DRIVER_SSN"].ToString();
							if(strOprSSn !="" && strOprSSn !="0")
							{
								strOprSSn = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(OperatorDetails["DRIVER_SSN"].ToString());
								
								if(strOprSSn.Trim()!="")//If-Else condition added by Charles on 21/7/2009 for Itrack 6129
								{
										string strvaln = "xxx-xx-";
									strvaln += strOprSSn.Substring(strvaln.Length, strOprSSn.Length - strvaln.Length);
									strOprSSn = strvaln;
								}
								else
									strOprSSn="";
							}
							OperatorElement.InnerXml += "<OPERATORNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(OperatorDetails["OPERATORNO"].ToString()) + "</OPERATORNUMBER>";
							OperatorElement.InnerXml += "<OPERATORNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(OperatorDetails["DRIVER_NAME"].ToString()) + "</OPERATORNAME>";
							OperatorElement.InnerXml += "<OPERATORSEX " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(OperatorDetails["DRIVER_SEX"].ToString()) + "</OPERATORSEX>";
							//OperatorElement.InnerXml += "<26_10_MAR_STATUS " + fieldType + "=\"" + fieldTypeText + "\"></26_10_MAR_STATUS>";
							OperatorElement.InnerXml += "<OPERATORDOB " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(OperatorDetails["DRIVER_DOB"].ToString()) + "</OPERATORDOB>";
							OperatorElement.InnerXml += "<OPERATORAUTODRVLIC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(OperatorDetails["DRIVER_DRIV_LIC"].ToString()) + "</OPERATORAUTODRVLIC>";
							OperatorElement.InnerXml += "<OPERATORLICSTATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(OperatorDetails["DRIVER_LIC_STATE"].ToString()) + "</OPERATORLICSTATE>";
							OperatorElement.InnerXml += "<OPERATORSSN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strOprSSn) + "</OPERATORSSN>";
								
							intTmpCtr1 ++;
						}
						intTmpCtr1 = 0;
						#endregion
               
						intBoatCtr++;
						#endregion

					}
				}
			}
		}
		#endregion

		#region Schedule Articles XML
		private string getSchPremium(DataSet DSTempDataSet, string comp_code)
		{
			if(DSTempDataSet.Tables.Count > 1 && DSTempDataSet.Tables[1].Rows.Count > 0)
			{
				foreach (DataRow Row in DSTempDataSet.Tables[1].Rows)
				{
					if(Row["COMPONENT_CODE"].ToString() == comp_code)// && Row["RISK_ID"].ToString() == risk_id)
						return Row["COVERAGE_PREMIUM"].ToString();
				}
			}
			return "";
		}

		private void CreateScheduleArticlesXML()
		{
			int tmpArtCtr=0,tmpctr=0, TempCounter=0;
			//string strOtherInfo="";
			DataSet tmpSchArticle;

			gobjWrapper.ClearParameteres();
			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DSTempDataSet = gobjWrapper.ExecuteDataSet("PROC_GETPDF_HOME_SCHEDULE_ARTICLES_INFO");
			//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_HOME_SCHEDULE_ARTICLES_INFO " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			if(DSTempDataSet.Tables[0].Rows.Count>0)
			{
				XmlElement SchArticleRootElement;
				SchArticleRootElement = AcordPDFXML.CreateElement("SCHEDULEARTICLES");

				XmlElement ArticleInfoInlandRootElement;
				ArticleInfoInlandRootElement = AcordPDFXML.CreateElement("ARTICLES");

				XmlElement XArticleInfoInlandRootElement;
				XArticleInfoInlandRootElement = AcordPDFXML.CreateElement("XARTICLES");
				
				if (gStrPdfFor == PDFForDecPage)
				{
					#region declaration Schedule Article Root Element DecPage
					DecPageRootElement.AppendChild(SchArticleRootElement);
					SchArticleRootElement.SetAttribute(fieldType,fieldTypeMultiple);
					SchArticleRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESCHARTICLES"));
					SchArticleRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESCHARTICLES"));
					SchArticleRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESCHARTICLESEXTN"));
					SchArticleRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESCHARTICLESEXTN"));
					#endregion
				}
				if (gStrPdfFor == PDFForAcord)
				{
					Acord80RootElement.SelectSingleNode("POLICY/ATTCHINLANDMARINE").InnerText = "1";
					#region Article Info Root Element for Inland Acord 81
					Acord81RootElement.AppendChild(ArticleInfoInlandRootElement);
					ArticleInfoInlandRootElement.SetAttribute(fieldType,fieldTypeMultiple);
					ArticleInfoInlandRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD81INLANDMARIN"));
					ArticleInfoInlandRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD81INLANDMARIN"));
					ArticleInfoInlandRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD81INLANDMARINEXTN"));
					ArticleInfoInlandRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD81INLANDMARINEXTN"));
					#endregion
					
					#region Extra Article Info Root Element for Inland Acord 81
					Acord81RootElement.AppendChild(XArticleInfoInlandRootElement);
					XArticleInfoInlandRootElement.SetAttribute(fieldType,fieldTypeMultiple);
					XArticleInfoInlandRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD81XINLANDMARIN"));
					XArticleInfoInlandRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD81XINLANDMARIN"));
					XArticleInfoInlandRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD81XINLANDMARINEXTN"));
					XArticleInfoInlandRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD81XINLANDMARINEXTN"));
					
					#endregion
					
				}

					XmlElement ArticleInfoInlandElement;
					ArticleInfoInlandElement = AcordPDFXML.CreateElement("ARTICLEINFO");

					//				XmlElement ADDITIONALRATINGINFORMATIONElement;
					//				ADDITIONALRATINGINFORMATIONElement = AcordPDFXML.CreateElement("ADDITIONALRATINGINFORMATION");

					ArticleInfoInlandRootElement.AppendChild(ArticleInfoInlandElement);
					ArticleInfoInlandElement.SetAttribute(fieldType,fieldTypeNormal);
					ArticleInfoInlandElement.SetAttribute(id,"0");

				
			
					foreach(DataRow SchArticleDetail in DSTempDataSet.Tables[0].Rows)
					{		
						//ArticleInfoInlandElement.AppendChild(ADDITIONALRATINGINFORMATIONElement);

						#region SWITCH CASE

						string com_code = SchArticleDetail["COMPONENT_CODE"].ToString();
						switch(SchArticleDetail["COV_CODE"].ToString())
						{
							case "JEWEL":
								ArticleInfoInlandElement.InnerXml += "<JEWELLERYAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</JEWELLERYAMOUNT>";
								ArticleInfoInlandElement.InnerXml += "<JEWELLERYRATE " + fieldType + "=\"" + fieldTypeText + "\"></JEWELLERYRATE>";
								ArticleInfoInlandElement.InnerXml += "<JEWELLERYPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, com_code))+"</JEWELLERYPREMIUM>";																			
						
								break;
							case "FURS":
								ArticleInfoInlandElement.InnerXml += "<FURSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</FURSAMOUNT>";
								ArticleInfoInlandElement.InnerXml += "<FURSRATE " + fieldType + "=\"" + fieldTypeText + "\"></FURSRATE>";
								ArticleInfoInlandElement.InnerXml += "<FURSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, com_code))+"</FURSPREMIUM>";																			
						
								break;
							case "FINEBR":
								ArticleInfoInlandElement.InnerXml += "<FINEARTSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</FINEARTSAMOUNT>";
								ArticleInfoInlandElement.InnerXml += "<FINEARTSRATE " + fieldType + "=\"" + fieldTypeText + "\"></FINEARTSRATE>";
								ArticleInfoInlandElement.InnerXml += "<FINEARTSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, com_code))+"</FINEARTSPREMIUM>";																			
								
								if (SchArticleDetail["AMOUNT"].ToString()!="" && Double.Parse(SchArticleDetail["AMOUNT"].ToString())>0)
								{
									ArticleInfoInlandElement.InnerXml += "<BREAKAGECOVERAGE " + fieldType + "=\"" + fieldTypeText + "\">1</BREAKAGECOVERAGE>";
								}
								else
								{
									ArticleInfoInlandElement.InnerXml += "<BREAKAGECOVERAGE " + fieldType + "=\"" + fieldTypeText + "\">0</BREAKAGECOVERAGE>";
								}
								break;
							case "FINEWBR":
									ArticleInfoInlandElement.InnerXml += "<FINEARTSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</FINEARTSAMOUNT>";
									ArticleInfoInlandElement.InnerXml += "<FINEARTSRATE " + fieldType + "=\"" + fieldTypeText + "\"></FINEARTSRATE>";
								if(htpremium.Contains("FNE_ART_WO_BRK"))
								{
									gstrGetPremium = htpremium["FNE_ART_WO_BRK"].ToString();
									gintGetindex = gstrGetPremium.IndexOf(".");
									if(gintGetindex == -1)
										ArticleInfoInlandElement.InnerXml += "<FINEARTSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["FNE_ART_WO_BRK"].ToString() + ".00")+"</FINEARTSPREMIUM>";
									else
										ArticleInfoInlandElement.InnerXml += "<FINEARTSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["FNE_ART_WO_BRK"].ToString())+"</FINEARTSPREMIUM>";
									if (SchArticleDetail["AMOUNT"].ToString()!="" && Double.Parse(SchArticleDetail["AMOUNT"].ToString())>0)
										ArticleInfoInlandElement.InnerXml += "<BREAKAGECOVERAGE " + fieldType + "=\"" + fieldTypeText + "\">1</BREAKAGECOVERAGE>";
									else
										ArticleInfoInlandElement.InnerXml += "<BREAKAGECOVERAGE " + fieldType + "=\"" + fieldTypeText + "\">0</BREAKAGECOVERAGE>";
								}
								break;
							case "CAMER":
								ArticleInfoInlandElement.InnerXml += "<CAMERASAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</CAMERASAMOUNT>";
								ArticleInfoInlandElement.InnerXml += "<CAMERASRATE " + fieldType + "=\"" + fieldTypeText + "\"></CAMERASRATE>";
								ArticleInfoInlandElement.InnerXml += "<CAMERASPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, com_code))+"</CAMERASPREMIUM>";																			
								break;
							case "MUSIC":
								ArticleInfoInlandElement.InnerXml += "<MUSICALINSTRUMENTSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</MUSICALINSTRUMENTSAMOUNT>";
								ArticleInfoInlandElement.InnerXml += "<MUSICALINSTRUMENTSRATE " + fieldType + "=\"" + fieldTypeText + "\"></MUSICALINSTRUMENTSRATE>";
								ArticleInfoInlandElement.InnerXml += "<MUSICALINSTRUMENTSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, com_code))+"</MUSICALINSTRUMENTSPREMIUM>";																			
								break;
							case "SILVE":
								ArticleInfoInlandElement.InnerXml += "<SILVERWAREAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</SILVERWAREAMOUNT>";
								ArticleInfoInlandElement.InnerXml += "<SILVERWARERATE " + fieldType + "=\"" + fieldTypeText + "\"></SILVERWARERATE>";
								ArticleInfoInlandElement.InnerXml += "<SILVERWAREPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, com_code))+"</SILVERWAREPREMIUM>";																			
								break;
							case "STAMP":
								ArticleInfoInlandElement.InnerXml += "<STAMPSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</STAMPSAMOUNT>";
								ArticleInfoInlandElement.InnerXml += "<STAMPSRATE " + fieldType + "=\"" + fieldTypeText + "\"></STAMPSRATE>";
								ArticleInfoInlandElement.InnerXml += "<STAMPSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, com_code))+"</STAMPSPREMIUM>";																			
								break;
							case "RARE":
								ArticleInfoInlandElement.InnerXml += "<COINSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</COINSAMOUNT>";
								ArticleInfoInlandElement.InnerXml += "<COINSRATE " + fieldType + "=\"" + fieldTypeText + "\"></COINSRATE>";
								ArticleInfoInlandElement.InnerXml += "<COINSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, com_code))+"</COINSPREMIUM>";																			
								break;
							case "GOLF":
								ArticleInfoInlandElement.InnerXml += "<GOLFERSEQUIPMENTAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</GOLFERSEQUIPMENTAMOUNT>";
								ArticleInfoInlandElement.InnerXml += "<GOLFERSEQUIPMENTRATE " + fieldType + "=\"" + fieldTypeText + "\"></GOLFERSEQUIPMENTRATE>";
								ArticleInfoInlandElement.InnerXml += "<GOLFERSEQUIPMENTPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, com_code))+"</GOLFERSEQUIPMENTPREMIUM>";																			
								break;
							case "PERSOD":
								ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</PERSONALCOMPUTERAMOUNT>";
								ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERRATE " + fieldType + "=\"" + fieldTypeText + "\"></PERSONALCOMPUTERRATE>";
								ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, com_code))+"</PERSONALCOMPUTERPREMIUM>";																			
								break;
							case "PERSOL":
								ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</PERSONALCOMPUTERAMOUNT>";
								ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERRATE " + fieldType + "=\"" + fieldTypeText + "\"></PERSONALCOMPUTERRATE>";
								ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, com_code))+"</PERSONALCOMPUTERPREMIUM>";																			
								break;
							default:
								XmlElement XArticleInfoInlandElement;
								XArticleInfoInlandElement =  AcordPDFXML.CreateElement("XArticleInfoInlandElementINFO");			
								XArticleInfoInlandRootElement.AppendChild(XArticleInfoInlandElement);
								XArticleInfoInlandElement.SetAttribute(fieldType,fieldTypeNormal);
								XArticleInfoInlandElement.SetAttribute(id,TempCounter.ToString());
							

								XArticleInfoInlandElement.InnerXml += "<OTHERPROP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["COV_DES"].ToString()) + "</OTHERPROP>";
								XArticleInfoInlandElement.InnerXml += "<OTHERPREM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, com_code)) + "</OTHERPREM>";
								XArticleInfoInlandElement.InnerXml += "<OTHERAMT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetail["AMOUNT"].ToString()) + "</OTHERAMT>";
								TempCounter++; 
								break;						
						}
						#endregion										
					}
	
					TempCounter = 0;
					string strDec="",CovLimit="",strPrem="";			
					#region Schedule Article Details
				if(gStrtemp == "temp")
				{
					htpremium.Clear(); 
					foreach (XmlNode PremiumNode in GetSchArtPremium())
					{
						if(!htpremium.Contains(getAttributeValue(PremiumNode,"COMPONENT_CODE")))
							htpremium.Add(getAttributeValue(PremiumNode,"COMPONENT_CODE"),getAttributeValue(PremiumNode,"STEPPREMIUM"));
					}
				}
					foreach(DataRow SchArticleDetails in DSTempDataSet.Tables[0].Rows)
					{
						XmlElement SchArticleElement;
						SchArticleElement = AcordPDFXML.CreateElement("SCHEDULEARTICLESINFO");
						
						XmlElement ArticleInfoRootElement;
						ArticleInfoRootElement = AcordPDFXML.CreateElement("ARTICLES");

						
						strDec = SchArticleDetails["LIMIT_DEDUC_AMOUNT"].ToString();
						if(strDec=="0" || strDec=="$0.00" || strDec=="$0" || strDec=="0.00" || strDec=="")
							strDec="";
						else									
							strDec = GetIntFormat(strDec);

						// formating for Limit
						CovLimit = SchArticleDetails["AMOUNT"].ToString().Trim();
						if(SchArticleDetails["AMOUNT"].ToString() != "" && CovLimit != ""  && CovLimit != "0" && SchArticleDetails["AMOUNT"].ToString() != "0" && SchArticleDetails["AMOUNT"].ToString() != "0.00")
						{
							CovLimit=GetIntFormat(CovLimit);
							//CovLimit += "/" + GetIntFormat(CoverageDetails["LIMIT_2"].ToString());										
						}
						else
						{
							//if(CoverageDetails["LIMIT_2"].ToString()!="" && CoverageDetails["LIMIT_2"].ToString() != "0" && CoverageDetails["LIMIT_2"].ToString() != "0.00")
							//	CovLimit = System.Convert.ToString(int.Parse(CovLimit) +  int.Parse(CoverageDetails["LIMIT_2"].ToString()));
							CovLimit=GetIntFormat(CovLimit);
						}
						if(CovLimit=="$0" || CovLimit=="$0/$0" || CovLimit=="0")
							CovLimit="";
						// formating for Premium
						string comp_code = SchArticleDetails["COMPONENT_CODE"].ToString().ToUpper();
						if(gStrtemp == "temp")
						{
							strPrem=GetRVPremiumBeforeCommit(comp_code, htpremium);
							if(strPrem !="")
							{
								strPrem = "$" + strPrem;
							}
						}
						else
						{
							strPrem=getSchPremium(DSTempDataSet, comp_code);
							if(strPrem !="")
							{
								strPrem = "$" + strPrem;
							}
						}
						if (gStrPdfFor == PDFForDecPage)
						{
							#region declaration Schedule Article Element DecPage
							SchArticleRootElement.AppendChild(SchArticleElement);
							SchArticleElement.SetAttribute(fieldType,fieldTypeNormal);
							SchArticleElement.SetAttribute(id, tmpArtCtr.ToString());
							if(string.Compare(gStrCalledFrom,"Policy",true)==0)
							{
								SchArticleElement.InnerXml += "<SCHART_POLICYNO " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</SCHART_POLICYNO>";
								SchArticleElement.InnerXml += "<SCHART_POLICYPERIODFROM " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</SCHART_POLICYPERIODFROM>";
								SchArticleElement.InnerXml += "<SCHART_POLICYPERIODTO " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</SCHART_POLICYPERIODTO>";
								SchArticleElement.InnerXml += "<SCHART_REASON " + fieldType + "=\"" + fieldTypeText + "\"></SCHART_REASON>";
								SchArticleElement.InnerXml += "<copyTo " + fieldType + "=\"" + fieldTypeText + "\">" + CopyTo + "</copyTo>";
							}
							SchArticleElement.InnerXml += "<SCHART_PRIMARYCONTACTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</SCHART_PRIMARYCONTACTNAME>";
							SchArticleElement.InnerXml += "<SCHART_PRIMARYCONTACTADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantAddress  + "</SCHART_PRIMARYCONTACTADDRESS>";
							SchArticleElement.InnerXml += "<SCHART_PRIMARYCONTACTCITY " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantCityStZip + "</SCHART_PRIMARYCONTACTCITY>";
							SchArticleElement.InnerXml += "<SCHART_AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyName  + "</SCHART_AGENCYNAME>";
							SchArticleElement.InnerXml += "<SCHART_AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyAddress + "</SCHART_AGENCYADDRESS>";
							SchArticleElement.InnerXml += "<SCHART_AGENCYCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCitySTZip  + "</SCHART_AGENCYCITYSTATEZIP>";
							SchArticleElement.InnerXml += "<SCHART_AGENCYPHONENO " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyPhoneNumber + "</SCHART_AGENCYPHONENO>";
							SchArticleElement.InnerXml += "<SCHART_AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCode + "</SCHART_AGENCYCODE>";
							SchArticleElement.InnerXml += "<SCHART_AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencySubCode  + "</SCHART_AGENCYSUBCODE>";
							SchArticleElement.InnerXml += "<SCHART_AGENCYBILLING " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyBilling + "</SCHART_AGENCYBILLING>";
				
							SchArticleElement.InnerXml += "<SCHART_CATEGORY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetails["COV_DES"].ToString()) + "</SCHART_CATEGORY>";
							SchArticleElement.InnerXml += "<SCHART_TOTALAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(CovLimit) + "</SCHART_TOTALAMOUNT>";
							SchArticleElement.InnerXml += "<SCHART_DEDUCTIBLE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strDec) + "</SCHART_DEDUCTIBLE>";
							SchArticleElement.InnerXml += "<SCHART_FORM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(SchArticleDetails["FORM_NO"].ToString()) + "</SCHART_FORM>";

							SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(strPrem)+"</SCHART_PREMIUM>";

							/*if (gStrPdfFor == PDFForDecPage)
							{
								switch (SchArticleDetails["COV_CODE"].ToString().ToUpper())
								{
									case	"BICYC"	:
											SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									break;
									case	"CAMER"	:
											SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									break;
									case	"CELLU"	:
											SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									break;
									case	"FINEBR"	:
											SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									break;
									case	"FINEWBR"	:
											SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									break;
									case	"FURS"	:
											SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									break;
									case	"GOLF"	:
											SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									break;
									case	"GUNS"	:
											SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									break;
									case	"JEWEL"	:
											SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									break;
									case	"MUSIC"	:
											SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									break;
									case	"RARE"	:
											SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									break;
									case	"SILVE"	:
											SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									break;
									case	"STAMP"	:
											SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									break;
									case	"PERSOL"	:
											SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									break;
									case	"PERSOD"	:
											SchArticleElement.InnerXml += "<SCHART_PREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(getSchPremium(DSTempDataSet, comp_code))+"</SCHART_PREMIUM>";
									break;
								}
							}
							*/
							#endregion
					
							#region Article Info Root Element for Dec page
							SchArticleElement.AppendChild(ArticleInfoRootElement);

							ArticleInfoRootElement.SetAttribute(fieldType,fieldTypeMultiple);
							ArticleInfoRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEARTICLES"));
							ArticleInfoRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEARTICLES"));
							ArticleInfoRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEARTICLESEXTN"));
							ArticleInfoRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEARTICLESEXTN"));
							#endregion
						}
				
						gobjWrapper.ClearParameteres();
						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						gobjWrapper.AddParameter("@ITEMID",SchArticleDetails["ITEM_ID"].ToString());
						gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						tmpSchArticle = gobjWrapper.ExecuteDataSet("PROC_GETPDF_HOME_SCHEDULE_ARTICLES_DETAILS");
						//tmpSchArticle = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_HOME_SCHEDULE_ARTICLES_DETAILS " + gStrClientID + ", " + gStrPolicyId + ", " + gStrPolicyVersion + "," + SchArticleDetails["ITEM_ID"].ToString() + ", '" + gStrCalledFrom + "'");
					
						//XmlElement ArticleInfoInlandElement;
						ArticleInfoInlandElement = AcordPDFXML.CreateElement("ARTICLEINFO");

						//XmlElement ADDITIONALRATINGINFORMATIONElement;
						//ADDITIONALRATINGINFORMATIONElement = AcordPDFXML.CreateElement("ADDITIONALRATINGINFORMATION");

						/*if (gStrPdfFor == PDFForAcord)
						{
							#region Article info Element for Inland Acord 81
							ArticleInfoInlandRootElement.AppendChild(ArticleInfoInlandElement);
							ArticleInfoInlandElement.SetAttribute(fieldType,fieldTypeNormal);
							ArticleInfoInlandElement.SetAttribute(id,TempCounter.ToString());
							TempCounter++;
						
							ArticleInfoInlandElement.AppendChild(ADDITIONALRATINGINFORMATIONElement);
							#endregion
						}*/
					
						#region Article Info Details
						string Sch_ItmVale="";
										
						foreach(DataRow ArticleInfo in tmpSchArticle.Tables[0].Rows)
						{
							Sch_ItmVale=ArticleInfo["ITEM_INSURING_VALUE"].ToString();
							if(Sch_ItmVale=="0")
								Sch_ItmVale="";		
							XmlElement ArticleInfoElement;
							ArticleInfoElement = AcordPDFXML.CreateElement("ARTICLEINFO");
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Article Info Element for Dec page
								ArticleInfoRootElement.AppendChild(ArticleInfoElement);
								ArticleInfoElement.SetAttribute(fieldType,fieldTypeNormal);
								ArticleInfoElement.SetAttribute(id,tmpctr.ToString());
								ArticleInfoElement.InnerXml += "<SCHEQUIP_CAT1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_NUMBER"].ToString()) + "</SCHEQUIP_CAT1>";
								ArticleInfoElement.InnerXml += "<SCHEQUIP_CAT1DESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_DESCRIPTION"].ToString()) + "</SCHEQUIP_CAT1DESC>";
								ArticleInfoElement.InnerXml += "<SCHEQUIP_CAT1SERIALNO " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_SERIAL_NUMBER"].ToString()) + "</SCHEQUIP_CAT1SERIALNO>";
								ArticleInfoElement.InnerXml += "<SCHEQUIP_CAT1INSUREDVALUE " + fieldType + "=\"" + fieldTypeText + "\">$" + RemoveJunkXmlCharacters(Sch_ItmVale) + "</SCHEQUIP_CAT1INSUREDVALUE>";
								#endregion
							}
							else if (gStrPdfFor == PDFForAcord)
							{
								/*#region Article Info for Indand Acord 81
								switch(SchArticleDetails["COV_CODE"].ToString())
								{
									case "JEWEL":
										ArticleInfoInlandElement.InnerXml += "<JEWELLERYAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</JEWELLERYAMOUNT>";
										ArticleInfoInlandElement.InnerXml += "<JEWELLERYRATE " + fieldType + "=\"" + fieldTypeText + "\"></JEWELLERYRATE>";

										if(htpremium.Contains("JWL"))
										{
											ArticleInfoInlandElement.InnerXml += "<JEWELLERYPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["JWL"].ToString() + ".00")+"</JEWELLERYPREMIUM>";																			
										}
									
										break;
									case "FURS":
										ArticleInfoInlandElement.InnerXml += "<FURSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</FURSAMOUNT>";
										ArticleInfoInlandElement.InnerXml += "<FURSRATE " + fieldType + "=\"" + fieldTypeText + "\"></FURSRATE>";
										if(htpremium.Contains("FURS"))
										{
											ArticleInfoInlandElement.InnerXml += "<FURSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["FURS"].ToString() + ".00")+"</FURSAMOUNT>";
										}									
										break;
									case "FINEBR":
										ArticleInfoInlandElement.InnerXml += "<FINEARTSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</FINEARTSAMOUNT>";
										ArticleInfoInlandElement.InnerXml += "<FINEARTSRATE " + fieldType + "=\"" + fieldTypeText + "\"></FINEARTSRATE>";
										if(htpremium.Contains("FNE_ART_WTH_BRK"))
										{
											ArticleInfoInlandElement.InnerXml += "<FINEARTSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["FNE_ART_WTH_BRK"].ToString() + ".00")+"</FINEARTSPREMIUM>";
										}									
									
										if (ArticleInfo["ITEM_INSURING_VALUE"].ToString()!="" && Double.Parse(ArticleInfo["ITEM_INSURING_VALUE"].ToString())>0)
										{
											ArticleInfoInlandElement.InnerXml += "<BREAKAGECOVERAGE " + fieldType + "=\"" + fieldTypeText + "\">1</BREAKAGECOVERAGE>";
										}
										else
										{
											ArticleInfoInlandElement.InnerXml += "<BREAKAGECOVERAGE " + fieldType + "=\"" + fieldTypeText + "\">0</BREAKAGECOVERAGE>";
										}
										break;
									case "FINEWBR":
										ArticleInfoInlandElement.InnerXml += "<FINEARTSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</FINEARTSAMOUNT>";
										ArticleInfoInlandElement.InnerXml += "<FINEARTSRATE " + fieldType + "=\"" + fieldTypeText + "\"></FINEARTSRATE>";
										if(htpremium.Contains("FNE_ART_WO_BRK"))
										{
											ArticleInfoInlandElement.InnerXml += "<FINEARTSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["FNE_ART_WO_BRK"].ToString() + ".00")+"</FINEARTSPREMIUM>";
										}									
									
										if (ArticleInfo["ITEM_INSURING_VALUE"].ToString()!="" && Double.Parse(ArticleInfo["ITEM_INSURING_VALUE"].ToString())>0)
										{
											ArticleInfoInlandElement.InnerXml += "<BREAKAGECOVERAGE " + fieldType + "=\"" + fieldTypeText + "\">1</BREAKAGECOVERAGE>";
										}
										else
										{
											ArticleInfoInlandElement.InnerXml += "<BREAKAGECOVERAGE " + fieldType + "=\"" + fieldTypeText + "\">0</BREAKAGECOVERAGE>";
										}
										break;
									case "CAMER":
										ArticleInfoInlandElement.InnerXml += "<CAMERASAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</CAMERASAMOUNT>";
										ArticleInfoInlandElement.InnerXml += "<CAMERASRATE " + fieldType + "=\"" + fieldTypeText + "\"></CAMERASRATE>";
										if(htpremium.Contains("CAMER"))
										{
											ArticleInfoInlandElement.InnerXml += "<CAMERASPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["CAMER"].ToString() + ".00")+"</CAMERASPREMIUM>";
										}									
										break;
									case "MUSIC":
										ArticleInfoInlandElement.InnerXml += "<MUSICALINSTRUMENTSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</MUSICALINSTRUMENTSAMOUNT>";
										ArticleInfoInlandElement.InnerXml += "<MUSICALINSTRUMENTSRATE " + fieldType + "=\"" + fieldTypeText + "\"></MUSICALINSTRUMENTSRATE>";
										if(htpremium.Contains("MUSIC"))
										{
											ArticleInfoInlandElement.InnerXml += "<MUSICALINSTRUMENTSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["MUSIC"].ToString() + ".00")+"</MUSICALINSTRUMENTSPREMIUM>";
										}																		
										break;
									case "SILVE":
										ArticleInfoInlandElement.InnerXml += "<SILVERWAREAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</SILVERWAREAMOUNT>";
										ArticleInfoInlandElement.InnerXml += "<SILVERWARERATE " + fieldType + "=\"" + fieldTypeText + "\"></SILVERWARERATE>";
										if(htpremium.Contains("SLVR"))
										{
											ArticleInfoInlandElement.InnerXml += "<SILVERWAREPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["SLVR"].ToString() + ".00")+"</SILVERWAREPREMIUM>";
										}																											
										break;
									case "STAMP":
										ArticleInfoInlandElement.InnerXml += "<STAMPSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</STAMPSAMOUNT>";
										ArticleInfoInlandElement.InnerXml += "<STAMPSRATE " + fieldType + "=\"" + fieldTypeText + "\"></STAMPSRATE>";
										if(htpremium.Contains("STMP"))
										{
											ArticleInfoInlandElement.InnerXml += "<STAMPSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["STMP"].ToString() + ".00")+"</STAMPSPREMIUM>";
										}									
										break;
									case "RARE":
										ArticleInfoInlandElement.InnerXml += "<COINSAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</COINSAMOUNT>";
										ArticleInfoInlandElement.InnerXml += "<COINSRATE " + fieldType + "=\"" + fieldTypeText + "\"></COINSRATE>";
										if(htpremium.Contains("RARE_COIN"))
										{
											ArticleInfoInlandElement.InnerXml += "<COINSPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["RARE_COIN"].ToString() + ".00")+"</COINSPREMIUM>";
										}										
										break;
									case "GOLF":
										ArticleInfoInlandElement.InnerXml += "<GOLFERSEQUIPMENTAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</GOLFERSEQUIPMENTAMOUNT>";
										ArticleInfoInlandElement.InnerXml += "<GOLFERSEQUIPMENTRATE " + fieldType + "=\"" + fieldTypeText + "\"></GOLFERSEQUIPMENTRATE>";
										if(htpremium.Contains("GOLF"))
										{
											ArticleInfoInlandElement.InnerXml += "<GOLFERSEQUIPMENTPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["GOLF"].ToString() + ".00")+"</GOLFERSEQUIPMENTPREMIUM>";
										}							
									
										break;
									case "PERSOD":
										ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</PERSONALCOMPUTERAMOUNT>";
										ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERRATE " + fieldType + "=\"" + fieldTypeText + "\"></PERSONALCOMPUTERRATE>";
										if(htpremium.Contains("PRS_CMP"))
										{
											ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["PRS_CMP"].ToString() + ".00")+"</PERSONALCOMPUTERPREMIUM>";										
										}									
										break;
									case "PERSOL":
										ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERAMOUNT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(ArticleInfo["ITEM_INSURING_VALUE"].ToString()) + "</PERSONALCOMPUTERAMOUNT>";
										ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERRATE " + fieldType + "=\"" + fieldTypeText + "\"></PERSONALCOMPUTERRATE>";
										if(htpremium.Contains("PRS_CMP"))
										{
											//ArticleInfoInlandElement.InnerXml += "<PERSONALCOMPUTERPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">"+RemoveJunkXmlCharacters(htpremium["PRS_CMP"].ToString() + ".00")+"</PERSONALCOMPUTERPREMIUM>";										
										}					
										break;
									default:
										strOtherInfo = SchArticleDetails["COV_DES"].ToString();
										strOtherInfo = strOtherInfo + " Insuring Value:-" + ArticleInfo["ITEM_INSURING_VALUE"].ToString() + System.Environment.NewLine;
										ADDITIONALRATINGINFORMATIONElement.InnerText += strOtherInfo;
										break;
						
								}
									#endregion */
							}
							tmpctr++;
						}
						#endregion
						tmpctr = 0;
						tmpArtCtr++;
					}
					#endregion			
				}

			else
			{
				if (gStrPdfFor == PDFForAcord)
					AcordPDFXML.SelectSingleNode(RootElement).RemoveChild(Acord81RootElement);
			}
			
		}
		#endregion

		#region GeneralInfo XML For Acord 81
		private void CreateGeneralInfoXML()
		{
			if (gStrPdfFor == PDFForAcord)
			{
				gobjWrapper.ClearParameteres();
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFAcord81GeneralInfo");
				gobjWrapper.ClearParameteres();
				//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFAcord81GeneralInfo " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
				if(DSTempDataSet.Tables[0].Rows.Count>0)
				{
					DataRow GeneralDetail = DSTempDataSet.Tables[0].Rows[0];
					XmlElement GeneralInfoRootElement;
					GeneralInfoRootElement = AcordPDFXML.CreateElement("GeneralInfo");
					Acord81RootElement.AppendChild(GeneralInfoRootElement);
					GeneralInfoRootElement.SetAttribute(fieldType,fieldTypeSingle);

					GeneralInfoRootElement.InnerXml += "<PROTECTIVEDEVICES " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(GeneralDetail["PROTDEVICE"].ToString())  + "</PROTECTIVEDEVICES>";
					GeneralInfoRootElement.InnerXml += "<PROPERTYEXHIBITED " + fieldType + "=\"" + fieldTypeText + "\"></PROPERTYEXHIBITED>";
					GeneralInfoRootElement.InnerXml += "<DEDUCTIBLEAPPLY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(GeneralDetail["ISDEDUCTIBLE"].ToString())  + "</DEDUCTIBLEAPPLY>";
					GeneralInfoRootElement.InnerXml += "<OTHERINSURANCE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(GeneralDetail["OTHERINSURANCEWITHWOLVERINE"].ToString())  + "</OTHERINSURANCE>";
					GeneralInfoRootElement.InnerXml += "<LOSSOCCUR " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(GeneralDetail["COVDECLINEDCANCELLED"].ToString())  + "</LOSSOCCUR>";
					GeneralInfoRootElement.InnerXml += "<COVERAGEDECLINED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(GeneralDetail["PRIORLOSS"].ToString())  + "</COVERAGEDECLINED>";
					GeneralInfoRootElement.InnerXml += "<PRIORINSURANCEPOLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(GeneralDetail["OLD_POLICY_NUMBER_AND_CARRIER"].ToString())  + "</PRIORINSURANCEPOLICYNUMBER>";
				}
			}
		}
		#endregion
		private int GetPortAccessPrem()
		{

			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DataSet DSTempDataSet1 = gobjWrapper.ExecuteDataSet("Proc_GetPDFSchEquipmentsDetails");
			gobjWrapper.ClearParameteres();

			//DataSet DSTempDataSet1 = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFSchEquipmentsDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
			int sumtotal = 0;

			if (DSTempDataSet1.Tables[0].Rows.Count > 0)
			{
				int RowCounter = 0;
				string[] arrDropdown; 
				int i=0;

				schEquip = 1;
				arrDropdown=new string[GetSchPremium().Count];
				foreach (XmlNode SchPremiumNode in GetSchPremium())
				{
					arrDropdown[i]=getAttributeValue(SchPremiumNode,"STEPPREMIUM").ToString();
					schEquip=i;
					i++;
				}
				foreach (DataRow Row in DSTempDataSet1.Tables[0].Rows)
				{
					if(arrDropdown.Length>0)
					{
						if(RowCounter<arrDropdown.Length)
						{
							sumtotal+=int.Parse(arrDropdown[RowCounter].ToString() ); 
						}
					}
					RowCounter++ ;
				}
			}
			return sumtotal;
		}

		#region code for Acord 82 boat info Xml
		private void createBoatXML()
		{
			double dblSumTotal=0;
			gobjWrapper.ClearParameteres();
			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DSTempDataSet = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS");
			gobjWrapper.ClearParameteres();
			if(DSTempDataSet.Tables[0].Rows.Count>0)
			{	
				DataSet DSTempEngineTrailer;
				int BoatCtr = 0, Eng_tra_Ctr = 0, Boatpage=1;
				int SchPrem = 0;
				double unattach_prem = 0;
				double sumTtlC=0;
				string boat_ded = "";
				double trailertotalprem=0;
			

				XmlElement BoatRootElementDecPage;
				BoatRootElementDecPage    = AcordPDFXML.CreateElement("BOAT");

				XmlElement BoatRootElementAcord82;
				BoatRootElementAcord82 = AcordPDFXML.CreateElement("BOAT");
			
				XmlElement BoatRootElementSupplement;
				BoatRootElementSupplement = AcordPDFXML.CreateElement("BOAT");

				XmlElement BoatRootElementDecPage0;
				BoatRootElementDecPage0    = AcordPDFXML.CreateElement("BOAT0");

				XmlElement BoatElementDecPage0;
				BoatElementDecPage0	= AcordPDFXML.CreateElement("BOATINFO0");
			
				#region setting Boat Root Elements
				if (gStrPdfFor == PDFForDecPage)
				{
					#region Boat Root Element for DecPage
					DecPageRootElement.AppendChild(BoatRootElementDecPage0);
					BoatRootElementDecPage0.SetAttribute(fieldType,fieldTypeMultiple);
					BoatRootElementDecPage0.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEBOAT"));
					BoatRootElementDecPage0.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBOAT"));
					BoatRootElementDecPage0.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEBOATNEWEXTN"));
					BoatRootElementDecPage0.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBOATNEWEXTN"));
					#endregion

					//prnOrd_covCode = new string[50];
					//prnOrd_attFile = new string[50];
					//prnOrd = new int[50];

					// Added by Mohit Agarwal for Customer Details irrespective of added boats
					// 14-Mar-2007
					#region setting Customer Agency Details
					BoatRootElementDecPage0.AppendChild(BoatElementDecPage0);
					BoatElementDecPage0.SetAttribute(fieldType,fieldTypeNormal);
					BoatElementDecPage0.SetAttribute(id,BoatCtr.ToString());
					//Policy
					BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<CURRDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(System.DateTime.Today.ToShortDateString()) + "</CURRDATE>";
					if(gStrCalledFrom.Equals(CalledFromPolicy))
					{					
						BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNUMBER>";
						BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<POLICYEFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</POLICYEFFDATE>";
						BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<POLICYEXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</POLICYEXPDATE>";
						BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<REASON " + fieldType + "=\"" + fieldTypeText + "\">" + Reason + "</REASON>";
						BoatElementDecPage0.InnerXml += "<copyTo " + fieldType + "=\"" + fieldTypeText + "\">" + CopyTo + "</copyTo>";
					}
					
					//Status Missing--Dont now How to maintain it.
					//POlicy Premium Adjustment.
					//Agency
					BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyName + "</AGENCYNAME>";
					if(AgencyAddress!="")
					{
						BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyAddress +" "+ "</AGENCYADDRESS>";
					
					}
					if(AgencyCitySTZip!="")
					{
						BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<AGENCYCITYSTZIP " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCitySTZip + " "+"</AGENCYCITYSTZIP>";
					
					}
					if(AgencyPhoneNumber!="")
					{
						BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<AGENCYPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyPhoneNumber +"</AGENCYPHONE>";
					}
					if(AgencyCode!="")
					{
						BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCode + "</AGENCYCODE>";
					}
					BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencySubCode + "</AGENCYSUBCODE>";
					BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<AGENCYBILLING " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyBilling + "</AGENCYBILLING>";
					//Named Insured
					BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<INSUREDNAME " + fieldType + "=\"" + fieldTypeText + "\">" + NamedInsuredWithSuffix + "</INSUREDNAME>";
					BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<INSUREDADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + CustomerAddress + "</INSUREDADDRESS>";
					BoatElementDecPage0.InnerXml = BoatElementDecPage0.InnerXml +  "<INSUREDCITYSTZIP " + fieldType + "=\"" + fieldTypeText + "\">" + CustomerCityStZip + "</INSUREDCITYSTZIP>";
					// adverse letter
					string strAdverseRep="";
					DataSet dsTempPolicy = new DataSet();
					gobjWrapper.ClearParameteres();
					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					dsTempPolicy = gobjWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
					gobjWrapper.ClearParameteres();

					//dsTempPolicy = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
					strAdverseRep = dsTempPolicy.Tables[1].Rows[0]["ADVERSE_LETTER_REQD"].ToString();
					// if process is blank
					if(gStrProcessID=="")
					{
						gStrProcessID = GetPolicyProcess( gStrClientID ,gStrPolicyId ,gStrPolicyVersion , gStrCalledFrom, gobjWrapper);
					}
					#endregion 

					BoatElementDecPage0.AppendChild(BoatRootElementDecPage);
					BoatRootElementDecPage.SetAttribute(fieldType,fieldTypeMultiple);
					BoatRootElementDecPage.SetAttribute(PrimPDF,"");
					BoatRootElementDecPage.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBOAT"));
					BoatRootElementDecPage.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEBOATNEWEXTN"));
					BoatRootElementDecPage.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBOATNEWEXTN"));

				}
				else if (gStrPdfFor == PDFForAcord)
				{
					#region Boat Root Element for Acord82
					Acord82RootElement.AppendChild(BoatRootElementAcord82);
					BoatRootElementAcord82.SetAttribute(fieldType,fieldTypeMultiple);
					BoatRootElementAcord82.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82BOAT"));
					BoatRootElementAcord82.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82BOAT"));
					BoatRootElementAcord82.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82BOATEXTN"));
					BoatRootElementAcord82.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82BOATEXTN"));
					#endregion
			
					#region Boat Root Element for Supplement
					SupplementalRootElement.AppendChild(BoatRootElementSupplement);

					BoatRootElementSupplement.SetAttribute(fieldType,fieldTypeMultiple);
					BoatRootElementSupplement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTBOAT"));
					BoatRootElementSupplement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTBOAT"));
					BoatRootElementSupplement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTBOATEXTN"));
					BoatRootElementSupplement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTBOATEXTN"));
					#endregion
				}
				#endregion
				//declare dataset for trailer premium			
				DataSet Dstrailpre = new DataSet();	
				DataSet DsSchprem = new DataSet();

			
			
				//			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				//			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				//			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				//			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				//			DSTempDataSet = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS");
				//			gobjWrapper.ClearParameteres();

				//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
				gobjWrapper.AddParameter("@CUSTOMER_ID",gStrClientID);
				gobjWrapper.AddParameter("@POL_ID",gStrPolicyId);
				gobjWrapper.AddParameter("@POL_VERSION_ID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DsSchprem = gobjWrapper.ExecuteDataSet("PROC_GetScheduleEquip_Premium");
				gobjWrapper.ClearParameteres();
			
				//DsSchprem =  gobjSqlHelper.ExecuteDataSet("PROC_GetScheduleEquip_Premium " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
				// fetch total policy premium
				foreach(DataRow BoatDetail in DSTempDataSet.Tables[0].Rows)
				{
			
					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DataSet DSTempEngineTrailer1 = gobjWrapper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails");
					gobjWrapper.ClearParameteres();

					//DataSet DSTempEngineTrailer1 = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
					gobjWrapper.AddParameter("@CUSTOMER_ID",gStrClientID);
					gobjWrapper.AddParameter("@POL_ID",gStrPolicyId);
					gobjWrapper.AddParameter("@POL_VERSION_ID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@BOAT_ID",BoatDetail["BOAT_ID"]);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					Dstrailpre = gobjWrapper.ExecuteDataSet("PROC_GetTrailer_Premium");
					gobjWrapper.ClearParameteres();

					//Dstrailpre = gobjSqlHelper.ExecuteDataSet("PROC_GetTrailer_Premium " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
					// If Proccess is not committed then fetch premiums from premium xml
					// if proccess is committed then fetch premiums from split table
					if(gStrtemp!="final")
					{
					}
					else
					{
						// premium from split table 
					
						SchPrem=Convert.ToInt32(GetTrailerPremium(DsSchprem,"COVERAGE_PREMIUM"));
						sumTtlC += GetPremiumSch(DSTempEngineTrailer1,"B","COVERAGE_PREMIUM");
						sumTtlC += GetTrailerPremium(Dstrailpre,"COVERAGE_PREMIUM");
						sumTtlC +=Convert.ToInt32(GetPremiumUnattached(DSTempEngineTrailer1,"UAE","COVERAGE_PREMIUM"));

						if(SchPrem !=0 || SchPrem !=0.00)
						{
							intschEquip = 1;
						}
					}
				}
				#region Boat Xml for each one
				foreach(DataRow BoatDetail in DSTempDataSet.Tables[0].Rows)
				{
				
					XmlElement BoatElementDecPage;
					BoatElementDecPage	= AcordPDFXML.CreateElement("BOATINFO");
				
					XmlElement BoatElementAcord82;
					BoatElementAcord82	= AcordPDFXML.CreateElement("BOATINFO");

					XmlElement BoatElementSuppliment;
					BoatElementSuppliment	= AcordPDFXML.CreateElement("BOATINFO");

					htpremium.Clear(); 

					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DataSet DSTempEngineTrailer1 = gobjWrapper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails");
					gobjWrapper.ClearParameteres();

					//DataSet DSTempEngineTrailer1 = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
				
					gobjWrapper.AddParameter("@CUSTOMER_ID",gStrClientID);
					gobjWrapper.AddParameter("@POL_ID",gStrPolicyId);
					gobjWrapper.AddParameter("@POL_VERSION_ID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@BOAT_ID",BoatDetail["BOAT_ID"]);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					Dstrailpre = gobjWrapper.ExecuteDataSet("PROC_GetTrailer_Premium");
					gobjWrapper.ClearParameteres();

					//Dstrailpre = gobjSqlHelper.ExecuteDataSet("PROC_GetTrailer_Premium " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
					//DataSet DSTempEngineTrailer1 = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
					// total premium of boat
					//LoadRateXML("HOME-BOAT",DSTempRateDataSet);
					foreach (XmlNode PremiumNode in GetBoatPremium(BoatDetail["BOAT_ID"].ToString()))
					{
						if(!htpremium.Contains(getAttributeValue(PremiumNode,"COMPONENT_CODE")))
							htpremium.Add(getAttributeValue(PremiumNode,"COMPONENT_CODE"),getAttributeValue(PremiumNode,"STEPPREMIUM"));
					}

					double sumTtlB=0;
					double Accordtrailertotalprem=0;
					// premium from quote premium xml
					if(gStrtemp!="final")
					{
						// fetch premiums from quote premium xml
						foreach (XmlNode SumTotalNode in GetSumTotalPremium())
						{
							if(getAttributeValue(SumTotalNode,"STEPPREMIUM")!=null && getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()!="" )
								sumTtlC += double.Parse(getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()) ;						
						}
						// premium for schedule equipment
						SchPrem = GetPortAccessPrem();
						//sumTtlC += SchPrem;
						double schPremAdj=0;
						//					foreach (XmlNode PremAdjNode in GetMinPremSch())
						//					{
						//						if(getAttributeValue(PremAdjNode,"STEPPREMIUM")!=null && getAttributeValue(PremAdjNode,"STEPPREMIUM").ToString()!="" )
						//						{
						//							schPremAdj = double.Parse(getAttributeValue(PremAdjNode,"STEPPREMIUM").ToString()) ;						
						//							break;
						//						}
						//					}
						SchPrem += Convert.ToInt32(schPremAdj);
						if(SchPrem !=0 || SchPrem !=0.00)
						{
							intschEquip = 1;
						}
						// fetch final premium for each boat and add it. 
						foreach (XmlNode SumTotalNode in GetSumTotalPremium(BoatDetail["BOAT_ID"].ToString()))
						{
							if(getAttributeValue(SumTotalNode,"STEPPREMIUM")!=null && getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()!="" )
							{
								sumTtlB += double.Parse(getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()) ;						
								break;
							}
						}
						// premium for unattached equipment
						foreach (XmlNode PremiumNode in GetUnattachedPremium("1"))
						{
							if(getAttributeValue(PremiumNode,"COMPONENT_CODE")=="BOAT_UNATTACH_PREMIUM" && getAttributeValue(PremiumNode,"STEPPREMIUM")!="" && getAttributeValue(PremiumNode,"STEPPREMIUM")!="Included")
							{
								unattach_prem=Convert.ToDouble(getAttributeValue(PremiumNode,"STEPPREMIUM"));
							}
						}
					
					}
					else
					{
						sumTtlB = GetPremiumBoat(DSTempEngineTrailer1,"B",BoatDetail["BOAT_ID"].ToString(),"COVERAGE_PREMIUM");
						sumTtlB += GetTrailerPremium(Dstrailpre,"COVERAGE_PREMIUM");
						trailertotalprem=GetTrailerPremium(Dstrailpre,"COVERAGE_PREMIUM");
						Accordtrailertotalprem=GetTrailerPremium(Dstrailpre,"COVERAGE_PREMIUM");
						unattach_prem = GetPremiumUnattached(DSTempEngineTrailer1,"UAE","COVERAGE_PREMIUM");
						//sumTtlB += unattach_prem;
						//					SchPrem=GetPremiumSch();
					}

					// fetch premium information of trailer attched with boat
					double finaltrailPrem=0;

					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DataSet DSTempEngineTrail = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_TRAILER_DETAILS");
					gobjWrapper.ClearParameteres();

					//DataSet DSTempEngineTrail = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_TRAILER_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
					foreach (DataRow trailRow in DSTempEngineTrail.Tables[0].Rows)
					{
						foreach (XmlNode TrailNode in GetSumTotalTrailerPremium(trailRow["TRAILER_NUMBER"].ToString()))
						{
							// if proccess not committed
							if(gStrtemp !="final")
							{
								if(getAttributeValue(TrailNode,"STEPPREMIUM")!=null && getAttributeValue(TrailNode,"STEPPREMIUM").ToString()!="" )
								{
									finaltrailPrem += double.Parse(getAttributeValue(TrailNode,"STEPPREMIUM").ToString()) ;						
									trailertotalprem=finaltrailPrem;
									Accordtrailertotalprem=finaltrailPrem;
									break;
								}
									// if proccess committed
								else
								{
							
								}
							}
						}
					}

					//					if(AutoDetail["AMOUNT"] != System.DBNull.Value)
					//						balance_due += Convert.ToInt32(AutoDetail["AMOUNT"]);
					// boat deductible
				
					double sumTtlE=0;
					foreach(DataRow CoverageDetails1 in DSTempEngineTrailer1.Tables[0].Rows)
					{

						string covCode1 = CoverageDetails1["COV_CODE"].ToString();
						if(covCode1 == "BDEDUC" && boat_ded == "")
						{
							boat_ded = CoverageDetails1["DEDUCTIBLE_1"].ToString();
							string grandpercent = CoverageDetails1["DEDUCTIBLE1_AMOUNT_TEXT"].ToString();
							if(grandpercent.IndexOf("%") > 0)
							{
								double granddeduc = GetPrecentDed(grandpercent, BoatDetail["PRESENT_VALUE"].ToString());
								if(granddeduc != 0)
									boat_ded = "$" + DollarFormat(granddeduc);
							}
							//break;
						}

						if(CoverageDetails1["ENDORS_ASSOC_COVERAGE"].ToString().Trim() == "Y")
						{
							//string endorsprem = GetPremium(DSTempEngineTrailer1, covCode1);
							//Added by asfa (20-Feb-2008) - iTrack issue #3331
							string endorsprem="";
							if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
								endorsprem= GetPremiumBeforeCommit(DSTempEngineTrailer1, covCode1,htpremium );
							else // when gStrtemp != "temp"
								endorsprem = GetPremium(DSTempEngineTrailer1, covCode1);
								
							try
							{
								if(endorsprem != "" && endorsprem != "Included")
									sumTtlE += double.Parse(endorsprem.Replace("$",""));
							}
							catch//(Exception ex)
							{}
						}
					}
					// Maping for desining of Dec page 
					if (gStrPdfFor == PDFForDecPage)
					{
						#region Boat Element for Dec Page
						BoatRootElementDecPage.AppendChild(BoatElementDecPage);
						BoatElementDecPage.SetAttribute(fieldType,fieldTypeNormal);
						BoatElementDecPage.SetAttribute(id,BoatCtr.ToString());
						//Policy
						/*						BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<CURRDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(System.DateTime.Today.ToShortDateString()) + "</CURRDATE>";
											if(gStrCalledFrom.Equals(CalledFromPolicy))
											{					
												BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNUMBER>";
												BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<POLICYEFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</POLICYEFFDATE>";
												BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<POLICYEXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</POLICYEXPDATE>";
												BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<REASON " + fieldType + "=\"" + fieldTypeText + "\">" + Reason + "</REASON>";
												BoatElementDecPage.InnerXml += "<copyTo " + fieldType + "=\"" + fieldTypeText + "\">" + CopyTo + "</copyTo>";
											}
					
											//Status Missing--Dont now How to maintain it.
											//POlicy Premium Adjustment.
											//Agency
											BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyName + "</AGENCYNAME>";
											if(AgencyAddress!="")
											{
												BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyAddress +" "+ "</AGENCYADDRESS>";
					
											}
											if(AgencyCitySTZip!="")
											{
												BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<AGENCYCITYSTZIP " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCitySTZip + " "+"</AGENCYCITYSTZIP>";
					
											}
											if(AgencyPhoneNumber!="")
											{
												BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<AGENCYPHONE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyPhoneNumber +"</AGENCYPHONE>";
											}
											if(AgencyCode!="")
											{
												BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCode + "</AGENCYCODE>";
											}
											BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencySubCode + "</AGENCYSUBCODE>";
											BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<AGENCYBILLING " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyBilling + "</AGENCYBILLING>";
											//Named Insured
											BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<INSUREDNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</INSUREDNAME>";
											BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<INSUREDADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + CustomerAddress + "</INSUREDADDRESS>";
											BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<INSUREDCITYSTZIP " + fieldType + "=\"" + fieldTypeText + "\">" + CustomerCityStZip + "</INSUREDCITYSTZIP>";

											//Reason Code
											BoatElementDecPage.InnerXml +="<reason_code1 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code1 + "</reason_code1>";
											BoatElementDecPage.InnerXml +="<reason_code2 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code2 + "</reason_code2>";
											BoatElementDecPage.InnerXml +="<reason_code3 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code3 + "</reason_code3>";
											BoatElementDecPage.InnerXml +="<reason_code4 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code4 + "</reason_code4>";
						*/					
						//Location
						// preparing inner xml
						BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<INSUREDNAME " + fieldType + "=\"" + fieldTypeText + "\">" + NamedInsuredWithSuffix + "</INSUREDNAME>";
						BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<CURRDATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(System.DateTime.Today.ToShortDateString()) + "</CURRDATE>";
						if(gStrCalledFrom.Equals(CalledFromPolicy))
						{
							BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNUMBER>";
							BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<POLICYEFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</POLICYEFFDATE>";
							BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<POLICYEXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</POLICYEXPDATE>";
							BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<REASON " + fieldType + "=\"" + fieldTypeText + "\">" + Reason + "</REASON>";
						}
						noBoats= DSTempDataSet.Tables[0].Rows.Count;
						string pageOf = Boatpage + " of " + (DSTempDataSet.Tables[0].Rows.Count+intschEquip).ToString();
						BoatElementDecPage.InnerXml = BoatElementDecPage.InnerXml +  "<PAGE " + fieldType + "=\"" + fieldTypeText + "\">" + pageOf + "</PAGE>";
						// Watercraft Location
						if(BoatDetail["LOCADD"].ToString()!="")
						{
							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<LOCATIONADDRESS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["LOCADD"].ToString())+"</LOCATIONADDRESS>"; 
						}
						if(BoatDetail["LOCCITY"].ToString()!="")
						{
							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<LOCATIONCITYSTZIP " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["LOCCITY"].ToString())+"</LOCATIONCITYSTZIP>"; 
						}

						// Added by Mohit Agarwal 25-Jan-2007
						// Insurance Score
						if(BoatDetail["APPLY_INSURANCE_SCORE"] != null && BoatDetail["APPLY_INSURANCE_SCORE"].ToString().IndexOf("-") < 0)
							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<INS_SCORE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["APPLY_INSURANCE_SCORE"].ToString())+"</INS_SCORE>"; 

						XmlNodeList ins_scoreNodeList = GetCreditForInsScore();
						XmlNode ins_scoreNode;
						if(ins_scoreNodeList.Count > 0)
						{
							ins_scoreNode = ins_scoreNodeList.Item(0);
							String [] discRows = getAttributeValue(ins_scoreNode,"STEPDESC").Split('-');

							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<DISCOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(discRows[discRows.Length -1])+"</DISCOUNT>"; 
						}
						//maping Boat details
						BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<WATERSNAVIGATED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["WATERS_NAVIGATED_USE"].ToString())+"</WATERSNAVIGATED>"; 
						string terr = BoatDetail["BOAT_TERRITORY"].ToString();
						string []territory = terr.Split('-');

						string makemodel = "";
						BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATTERRITORY  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(territory[0])+"</BOATTERRITORY>"; 
												
						BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATNUMBER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_NO"].ToString())+"</BOATNUMBER>"; 
						BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATTYPE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOATTYPE"].ToString())+"</BOATTYPE>";
						BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATYEAR  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_YEAR"].ToString())+"</BOATYEAR>";
						makemodel = BoatDetail["BOAT_MAKE"].ToString();
						if(BoatDetail["BOAT_MODEL"].ToString() != "")
							makemodel += "/" + BoatDetail["BOAT_MODEL"].ToString();
						BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATDESC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(makemodel)+"</BOATDESC>";
						//BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATMODEL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_MODEL"].ToString())+"</BOATMODEL>";
						if(BoatDetail["BOAT_INCHES"].ToString()=="")
						{
							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATLENGTH  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_LENGTH"].ToString() +"'")+"</BOATLENGTH>";
						}
						else
						{
							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATLENGTH  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_LENGTH"].ToString() +"'"+" "+ BoatDetail["BOAT_INCHES"].ToString() +"''")+"</BOATLENGTH>";
						}
						BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATHPCC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["HORSE_POWER"].ToString().Replace(".00",""))+"</BOATHPCC>";
						BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATSERIAL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["HULL_ID_NO"].ToString())+"</BOATSERIAL>";
						BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATCONSTRUCTION  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["HULL_MATERIAL"].ToString())+"</BOATCONSTRUCTION>";
						if(BoatDetail["PRESENT_VALUE"].ToString()!="" && BoatDetail["PRESENT_VALUE"].ToString()!="0")
						{
							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATLIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + BoatDetail["PRESENT_VALUE"].ToString().Replace(".00",""))+"</BOATLIMIT>";
						}
						else
						{
							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATLIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</BOATLIMIT>";
						}
						BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATDED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATDED>";
						// Mapping Boat Coverages
						string lstrGetPremium="";
						if(gStrtemp !="final")
						{
							if(htpremium.Contains("BOAT_PD"))
							{
								lstrGetPremium = htpremium["BOAT_PD"].ToString();
								int lintGetPremium = lstrGetPremium.IndexOf(".");
								if(lstrGetPremium != "0" && lstrGetPremium !="")
								{
									if(lintGetPremium == -1)
									{
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + htpremium["BOAT_PD"].ToString()+".00")+"</BOATPREMIUM>";
									}
									else
									{
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + htpremium["BOAT_PD"].ToString())+"</BOATPREMIUM>";
									}
								}
							}
						}
						else
						{
							int lintGetPremium=0;
							foreach(DataRow BoatPd in  DSTempEngineTrailer1.Tables[1].Rows)
								if(BoatPd["COMPONENT_CODE"].ToString() == "BOAT_PD")
								{
									lstrGetPremium = BoatPd["COVERAGE_PREMIUM"].ToString();
									lintGetPremium = lstrGetPremium.IndexOf(".");
								}
							if(lstrGetPremium != "0" && lstrGetPremium !="")
							{
								if(lintGetPremium == -1)
								{
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + lstrGetPremium +".00")+"</BOATPREMIUM>";
								}
								else
								{
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + lstrGetPremium)+"</BOATPREMIUM>";
								}
							}
						}
						//BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATPREMIUM  " + fieldType +"=\""+ fieldTypeText +"\"></BOATPREMIUM>";
						//int strcount=0,strtrialer=0 ;
						//Engine Information...
						//#region engine & trailer Element for Dec Page
						int engtrictr=0;
						double trailPremium=0;	
		
						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
						gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						DSTempEngineTrailer = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_ENGINE_DETAILS");
						gobjWrapper.ClearParameteres();

						//DSTempEngineTrailer = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_ENGINE_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
					
						int countengine = DSTempEngineTrailer.Tables[0].Rows.Count;
					
						if(DSTempEngineTrailer.Tables[0].Rows.Count>0)
						{
							foreach(DataRow EngineDetail in DSTempEngineTrailer.Tables[0].Rows)
							{
								if(engtrictr < 2)
								{
									if(engtrictr==0)
									{
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1NUMBER  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["ENGINE_NO"].ToString())+"</ENGINE1NUMBER>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1TYPE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["ENGINETYPE"].ToString())+"</ENGINE1TYPE>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1YEAR  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["YEAR"].ToString())+"</ENGINE1YEAR>";
										makemodel = EngineDetail["MAKE"].ToString();
										if(EngineDetail["MODEL"].ToString() != "")
											makemodel += "/" + EngineDetail["MODEL"].ToString();
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1DESC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(makemodel)+"</ENGINE1DESC>";
										//BoatElementDecPage.InnerXml= DecPageEngineElement.InnerXml +  "<ENGINEMODEL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempEngineTrailer.Tables[0].Rows[0]["MODEL"].ToString())+"</ENGINEMODEL>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1HPCC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["HORSEPOWER"].ToString())+"</ENGINE1HPCC>";								
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1SERIAL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["SERIAL_NO"].ToString())+"</ENGINE1SERIAL>";
										if(EngineDetail["BOAT_INSURING_VALUE"].ToString() =="0.00" || EngineDetail["BOAT_INSURING_VALUE"].ToString() =="0")
										{
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1LIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</ENGINE1LIMIT>";
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1PREMIUM  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</ENGINE1PREMIUM>";
										}
										else
										{
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1LIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</ENGINE1LIMIT>";
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1PREMIUM  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</ENGINE1PREMIUM>";
										}
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1DED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</ENGINE1DED>";
									
										engtrictr++;
									}
									else
									{
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2NUMBER  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["ENGINE_NO"].ToString())+"</ENGINE2NUMBER>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2TYPE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["ENGINETYPE"].ToString())+"</ENGINE2TYPE>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2YEAR  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["YEAR"].ToString())+"</ENGINE2YEAR>";
										makemodel = EngineDetail["MAKE"].ToString();
										if(EngineDetail["MODEL"].ToString() != "")
											makemodel += "/" + EngineDetail["MODEL"].ToString();
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2DESC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(makemodel)+"</ENGINE2DESC>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2HPCC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["HORSEPOWER"].ToString())+"</ENGINE2HPCC>";
										//BoatElementDecPage.InnerXml= DecPageEngineElement.InnerXml +  "<ENGINEMODEL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempEngineTrailer.Tables[0].Rows[0]["MODEL"].ToString())+"</ENGINEMODEL>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2SERIAL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["SERIAL_NO"].ToString())+"</ENGINE2SERIAL>";
										if(EngineDetail["BOAT_INSURING_VALUE"].ToString() =="0.00" || EngineDetail["BOAT_INSURING_VALUE"].ToString() =="0")
										{
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2LIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</ENGINE2LIMIT>";
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2PREMIUM  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</ENGINE2PREMIUM>";
										}
										else
										{
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2LIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</ENGINE2LIMIT>";
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2PREMIUM  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</ENGINE2PREMIUM>";
										}
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2DED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</ENGINE2DED>";
									
										engtrictr++;
									}
								}
							}
					
							//TRAILER INFORMATION...

							gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
							gobjWrapper.AddParameter("@POLID",gStrPolicyId);
							gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
							gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
							gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
							DSTempEngineTrailer = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_TRAILER_DETAILS");
							gobjWrapper.ClearParameteres();

							//DSTempEngineTrailer = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_TRAILER_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
							int counttrailer = DSTempEngineTrailer.Tables[0].Rows.Count;
							int secondtrailer=0,enginectr=0,trialerctr=0;
							if(DSTempEngineTrailer.Tables[0].Rows.Count>0)
							{
								foreach(DataRow trailerdetail in DSTempEngineTrailer.Tables[0].Rows)
								{
									// fetch premium for each trialer
									// if process committed
									if(gStrtemp =="final")
									{
										foreach(DataRow trailerRow in Dstrailpre.Tables[0].Rows)
										{
											if((trailerRow["TRAILER_NUMBER"].ToString()) == (trailerdetail["TRAILER_NUMBER"].ToString()))
												trailPremium=Convert.ToDouble(trailerRow["COVERAGE_PREMIUM"].ToString());
										}
									}
										// if proccess not committed
									else
									{
										foreach (XmlNode TrailNode in GetSumTotalTrailerPremium(trailerdetail["TRAILER_NUMBER"].ToString()))
										{
											if(getAttributeValue(TrailNode,"STEPPREMIUM")!=null && getAttributeValue(TrailNode,"STEPPREMIUM").ToString()!="" )
											{
												trailPremium = double.Parse(getAttributeValue(TrailNode,"STEPPREMIUM").ToString()) ;						
												break;
											}
										}
									
									}
							
									//end fetching trailerpremium
								
									if(countengine==1 && enginectr==0)
									{
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2NUMBER  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILER_NUMBER"].ToString())+"</ENGINE2NUMBER>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2TYPE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILERTYPE"].ToString())+"</ENGINE2TYPE>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2YEAR  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["YEAR"].ToString())+"</ENGINE2YEAR>";
										makemodel = trailerdetail["MANUFACTURER"].ToString();
										if(trailerdetail["MODEL"].ToString() != "")
											makemodel += "/" + trailerdetail["MODEL"].ToString();
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2DESC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(makemodel)+"</ENGINE2DESC>";
										//							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINEMODEL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempEngineTrailer.Tables[0].Rows[0]["MODEL"].ToString())+"</TRAILERMODEL>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2SERIAL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["SERIAL_NO"].ToString())+"</ENGINE2SERIAL>";
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2LIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailerdetail["TRAILER_LIMIT"].ToString().Replace(".00",""))+"</ENGINE2LIMIT>";

										string trailded ="$" + trailerdetail["TRAILER_DED"].ToString().Replace(".00","");
										string grandpercent = trailerdetail["TRAILER_DED_AMOUNT_TEXT"].ToString();
										if(grandpercent.IndexOf("%") > 0)
										{
											double granddeduc = GetPrecentDed(grandpercent, DSTempEngineTrailer.Tables[0].Rows[0]["TRAILER_LIMIT"].ToString());
											if(granddeduc != 0)
												trailded = "$" + DollarFormat(granddeduc).Replace(".00","");
										}
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2DED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailded)+"</ENGINE2DED>";

										if(trailPremium != 0)
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2PREMIUM  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailPremium.ToString() + ".00")+"</ENGINE2PREMIUM>";
										enginectr++;
										engtrictr++;
									}
										////////////--------
										///
									else
									{
										if(trialerctr < 2)
										{
											if(secondtrailer==0)
											{
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1NUMBER  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILER_NUMBER"].ToString())+"</TRAILER1NUMBER>";
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1TYPE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILERTYPE"].ToString())+"</TRAILER1TYPE>";
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1YEAR  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["YEAR"].ToString())+"</TRAILER1YEAR>";
												makemodel = trailerdetail["MANUFACTURER"].ToString();
												if(trailerdetail["MODEL"].ToString() != "")
													makemodel += "/" + trailerdetail["MODEL"].ToString();
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1DESC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(makemodel)+"</TRAILER1DESC>";
												//							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILERMODEL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempEngineTrailer.Tables[0].Rows[0]["MODEL"].ToString())+"</TRAILERMODEL>";
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1SERIAL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["SERIAL_NO"].ToString())+"</TRAILER1SERIAL>";
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1LIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailerdetail["TRAILER_LIMIT"].ToString().Replace(".00",""))+"</TRAILER1LIMIT>";

												string trailded ="$" + trailerdetail["TRAILER_DED"].ToString().Replace(".00","");
												string grandpercent = trailerdetail["TRAILER_DED_AMOUNT_TEXT"].ToString();
												if(grandpercent.IndexOf("%") > 0)
												{
													double granddeduc = GetPrecentDed(grandpercent, trailerdetail["TRAILER_LIMIT"].ToString());
													if(granddeduc != 0)
														trailded = "$" + DollarFormat(granddeduc).Replace(".00","");
												}

												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1DED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailded)+"</TRAILER1DED>";
												if(trailPremium != 0)
													BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1PREMIUM  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailPremium.ToString() + ".00")+"</TRAILER1PREMIUM>";
												secondtrailer++;
												engtrictr++;
												trialerctr++;

											}
											else
											{
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2NUMBER  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILER_NUMBER"].ToString())+"</TRAILER2NUMBER>";
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2TYPE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILERTYPE"].ToString())+"</TRAILER2TYPE>";
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2YEAR  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["YEAR"].ToString())+"</TRAILER2YEAR>";
												makemodel = trailerdetail["MANUFACTURER"].ToString();
												if(trailerdetail["MODEL"].ToString() != "")
													makemodel += "/" + trailerdetail["MODEL"].ToString();
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2DESC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(makemodel)+"</TRAILER2DESC>";
												//							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILERMODEL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempEngineTrailer.Tables[0].Rows[0]["MODEL"].ToString())+"</TRAILERMODEL>";
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2SERIAL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["SERIAL_NO"].ToString())+"</TRAILER2SERIAL>";
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2LIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailerdetail["TRAILER_LIMIT"].ToString().Replace(".00",""))+"</TRAILER2LIMIT>";

												string trailded ="$" + trailerdetail["TRAILER_DED"].ToString().Replace(".00","");
												string grandpercent = trailerdetail["TRAILER_DED_AMOUNT_TEXT"].ToString();
												if(grandpercent.IndexOf("%") > 0)
												{
													double granddeduc = GetPrecentDed(grandpercent, trailerdetail["TRAILER_LIMIT"].ToString());
													if(granddeduc != 0)
														trailded = "$" + DollarFormat(granddeduc).Replace(".00","");
												}

												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2DED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailded)+"</TRAILER2DED>";
												if(trailPremium != 0)
													BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2PREMIUM  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailPremium.ToString() + ".00")+"</TRAILER2PREMIUM>";
												engtrictr++;
												trialerctr++;
											}
										}
									}
								
								}
								//reinialize secondtrailer counter for next boat
								secondtrailer=0;
								trialerctr=0;
							}	
							// reinialize enginecounter for next boat
							countengine=0;
						}
						else // No Engine
						{
							// mapping Boat Attached trailer information

							gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
							gobjWrapper.AddParameter("@POLID",gStrPolicyId);
							gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
							gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
							gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
							DSTempEngineTrailer = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_TRAILER_DETAILS");
							gobjWrapper.ClearParameteres();

							//DSTempEngineTrailer = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_TRAILER_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
							int counttrailer=DSTempEngineTrailer.Tables[0].Rows.Count;
							int RowCount=0;
							if(DSTempEngineTrailer.Tables[0].Rows.Count>0)
							{
								foreach(DataRow trailerdetail in DSTempEngineTrailer.Tables[0].Rows)
								{
									// fetch premium for each trialer
									// if process committed
									if(gStrtemp =="final")
									{
										foreach(DataRow trailerRow in Dstrailpre.Tables[0].Rows)
										{
											if((trailerRow["TRAILER_NUMBER"].ToString()) == (trailerdetail["TRAILER_NUMBER"].ToString()))
												trailPremium=Convert.ToDouble(trailerRow["COVERAGE_PREMIUM"].ToString());
										}
									}
										// if proccess not committed
									else
									{
										foreach (XmlNode TrailNode in GetSumTotalTrailerPremium(trailerdetail["TRAILER_NUMBER"].ToString()))
										{
											if(getAttributeValue(TrailNode,"STEPPREMIUM")!=null && getAttributeValue(TrailNode,"STEPPREMIUM").ToString()!="" )
											{
												trailPremium = double.Parse(getAttributeValue(TrailNode,"STEPPREMIUM").ToString()) ;						
												break;
											}
										}
									}
							
									//end fetching trailerpremium
									// first trailer
									if(RowCount <4)
									{
										if(RowCount==0)
										{
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1NUMBER  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILER_NUMBER"].ToString())+"</ENGINE1NUMBER>";
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1TYPE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILERTYPE"].ToString())+"</ENGINE1TYPE>";
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1YEAR  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["YEAR"].ToString())+"</ENGINE1YEAR>";
											makemodel = trailerdetail["MANUFACTURER"].ToString();
											if(trailerdetail["MODEL"].ToString() != "")
												makemodel += "/" + trailerdetail["MODEL"].ToString();
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1DESC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(makemodel)+"</ENGINE1DESC>";
											//							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINEMODEL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempEngineTrailer.Tables[0].Rows[0]["MODEL"].ToString())+"</ENGINEMODEL>";
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1SERIAL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["SERIAL_NO"].ToString())+"</ENGINE1SERIAL>";
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1LIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailerdetail["TRAILER_LIMIT"].ToString().Replace(".00",""))+"</ENGINE1LIMIT>";

											string trailded ="$" + trailerdetail["TRAILER_DED"].ToString().Replace(".00","");
											string grandpercent = trailerdetail["TRAILER_DED_AMOUNT_TEXT"].ToString();
											if(grandpercent.IndexOf("%") > 0)
											{
												double granddeduc = GetPrecentDed(grandpercent, trailerdetail["TRAILER_LIMIT"].ToString());
												if(granddeduc != 0)
													trailded = "$" + DollarFormat(granddeduc).Replace(".00","");
											}

											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1DED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailded)+"</ENGINE1DED>";
									
									
											if(trailPremium != 0)
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE1PREMIUM  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailPremium.ToString() + ".00")+"</ENGINE1PREMIUM>";
								
											RowCount++;
											engtrictr++;
										}
											// second trialer
										else if(RowCount==1)
										{
									
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2NUMBER  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILER_NUMBER"].ToString())+"</ENGINE2NUMBER>";
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2TYPE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILERTYPE"].ToString())+"</ENGINE2TYPE>";
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2YEAR  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["YEAR"].ToString())+"</ENGINE2YEAR>";
											makemodel = trailerdetail["MANUFACTURER"].ToString();
											if(trailerdetail["MODEL"].ToString() != "")
												makemodel += "/" + trailerdetail["MODEL"].ToString();
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2DESC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(makemodel)+"</ENGINE2DESC>";
											//							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINEMODEL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempEngineTrailer.Tables[0].Rows[0]["MODEL"].ToString())+"</ENGINEMODEL>";
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2SERIAL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["SERIAL_NO"].ToString())+"</ENGINE2SERIAL>";
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2LIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailerdetail["TRAILER_LIMIT"].ToString().Replace(".00",""))+"</ENGINE2LIMIT>";

											string trailded ="$" + trailerdetail["TRAILER_DED"].ToString().Replace(".00","");
											string grandpercent = trailerdetail["TRAILER_DED_AMOUNT_TEXT"].ToString();
											if(grandpercent.IndexOf("%") > 0)
											{
												double granddeduc = GetPrecentDed(grandpercent, trailerdetail["TRAILER_LIMIT"].ToString());
												if(granddeduc != 0)
													trailded = "$" + DollarFormat(granddeduc).Replace(".00","");
											}

											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2DED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailded)+"</ENGINE2DED>";
											if(trailPremium != 0)
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINE2PREMIUM  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailPremium.ToString() + ".00")+"</ENGINE2PREMIUM>";
								
											RowCount++;
											engtrictr++;
										}
											//third trailer
										else if(RowCount==2)
										{
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1NUMBER  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILER_NUMBER"].ToString())+"</TRAILER1NUMBER>";
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1TYPE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILERTYPE"].ToString())+"</TRAILER1TYPE>";
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1YEAR  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["YEAR"].ToString())+"</TRAILER1YEAR>";
											makemodel = trailerdetail["MANUFACTURER"].ToString();
											if(trailerdetail["MODEL"].ToString() != "")
												makemodel += "/" + trailerdetail["MODEL"].ToString();
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1DESC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(makemodel)+"</TRAILER1DESC>";
											//							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINEMODEL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempEngineTrailer.Tables[0].Rows[0]["MODEL"].ToString())+"</ENGINEMODEL>";
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1SERIAL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["SERIAL_NO"].ToString())+"</TRAILER1SERIAL>";
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1LIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailerdetail["TRAILER_LIMIT"].ToString().Replace(".00",""))+"</TRAILER1LIMIT>";

											string trailded ="$" + trailerdetail["TRAILER_DED"].ToString().Replace(".00","");
											string grandpercent = trailerdetail["TRAILER_DED_AMOUNT_TEXT"].ToString();
											if(grandpercent.IndexOf("%") > 0)
											{
												double granddeduc = GetPrecentDed(grandpercent, trailerdetail["TRAILER_LIMIT"].ToString());
												if(granddeduc != 0)
													trailded = "$" + DollarFormat(granddeduc).Replace(".00","");
											}

											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1DED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailded)+"</TRAILER1DED>";
											if(trailPremium != 0)
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER1PREMIUM  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailPremium.ToString() + ".00")+"</TRAILER1PREMIUM>";
											RowCount++;
											engtrictr++;
										}
											//fourth trailer
										else 
										{
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2NUMBER  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILER_NUMBER"].ToString())+"</TRAILER2NUMBER>";
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2TYPE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["TRAILERTYPE"].ToString())+"</TRAILER2TYPE>";
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2YEAR  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["YEAR"].ToString())+"</TRAILER2YEAR>";
											makemodel = trailerdetail["MANUFACTURER"].ToString();
											if(trailerdetail["MODEL"].ToString() != "")
												makemodel += "/" + trailerdetail["MODEL"].ToString();
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2DESC  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(makemodel)+"</TRAILER2DESC>";
											//							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<ENGINEMODEL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempEngineTrailer.Tables[0].Rows[0]["MODEL"].ToString())+"</ENGINEMODEL>";
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2SERIAL  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailerdetail["SERIAL_NO"].ToString())+"</TRAILER2SERIAL>";
											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2LIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailerdetail["TRAILER_LIMIT"].ToString().Replace(".00",""))+"</TRAILER2LIMIT>";

											string trailded ="$" + trailerdetail["TRAILER_DED"].ToString().Replace(".00","");
											string grandpercent = trailerdetail["TRAILER_DED_AMOUNT_TEXT"].ToString();
											if(grandpercent.IndexOf("%") > 0)
											{
												double granddeduc = GetPrecentDed(grandpercent, trailerdetail["TRAILER_LIMIT"].ToString());
												if(granddeduc != 0)
													trailded = "$" + DollarFormat(granddeduc).Replace(".00","");
											}

											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2DED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(trailded)+"</TRAILER2DED>";
											if(trailPremium != 0)
												BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<TRAILER2PREMIUM  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + trailPremium.ToString() + ".00")+"</TRAILER2PREMIUM>";
											RowCount++;
											engtrictr++;
										}
									}
								}
								// reinialize counter for second boat
								RowCount=0;
							}
							
						}
						// re intialize counters for second boat
						engtrictr=0;
					}
						
						#endregion
				
						// Mapping for Accord pdf 
					else if (gStrPdfFor == PDFForAcord)
					{
						#region Boat Element for Acord82
						BoatRootElementAcord82.AppendChild(BoatElementAcord82);
						BoatElementAcord82.SetAttribute(fieldType,fieldTypeNormal);
						BoatElementAcord82.SetAttribute(id,BoatCtr.ToString());
						// mapping Boat Details
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<APPLICANTNAME " + fieldType +"=\""+ fieldTypeText +"\">"+ApplicantName1+"</APPLICANTNAME>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<APPLICANTIONNUMBER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(PolicyNumber)+"</APPLICANTIONNUMBER>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATNUMBER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_NO"].ToString())+"</BOATNUMBER>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATPOWER " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_POWER"].ToString())+"</BOATPOWER>";
						if(BoatDetail["BOAT_POWER"]!=null && BoatDetail["BOAT_POWER"].ToString()!="")
							if(BoatDetail["BOAT_POWER"].ToString().Equals("OTH"))
							{
								BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATPOWEROTHER " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOATTYPE"].ToString())+"</BOATPOWEROTHER>";
							}
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATHULLMATERIAL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(BoatDetail["HULLMATERIAL"].ToString())+"</BOATHULLMATERIAL>";
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<HULLOTHERMATERIAL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["HULLMATERIAL_DESCRIPTION"].ToString())+"</HULLOTHERMATERIAL>";
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_YEAR"].ToString())+"</BOATYEAR>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_MAKE_MODEL"].ToString())+"</BOATMAKEMODEL>"; 
						if(BoatDetail["BOAT_INCHES"].ToString()=="")
						{
							BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATLENGTH " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_LENGTH"].ToString() +"'") +"</BOATLENGTH>"; 
						}
						else
						{
							BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATLENGTH " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_LENGTH"].ToString() +"'" + BoatDetail["BOAT_INCHES"].ToString() +"''") +"</BOATLENGTH>"; 
						}
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATSPEED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_SPEED"].ToString())+"</BOATSPEED>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATDTPURCHASED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["DATE_PURCHASED"].ToString())+"</BOATDTPURCHASED>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATPVALUE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["PRESENT_VALUE"].ToString())+"</BOATPVALUE>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATHULLIDNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["HULL_ID_NO"].ToString())+"</BOATHULLIDNUM>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATWATERSNAV  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["WATERS_NAVIGATED_USE"].ToString())+"</BOATWATERSNAV>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATTERRITORY  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_TERRITORY"].ToString())+"</BOATTERRITORY>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATLOCATION   " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["LOCADD"].ToString()+", " + BoatDetail["LOCCITY"].ToString())+"</BOATLOCATION>"; 
						string strLayUPFrom="";
						string strLayUPTo="";
						if (BoatDetail["LAY_UP_PERIOD_FROM_MONTH"].ToString()!="0" && BoatDetail["LAY_UP_PERIOD_FROM_DAY"].ToString()!="0")
							strLayUPFrom="From "+ MonthName[BoatDetail["LAY_UP_PERIOD_FROM_MONTH"].ToString()].ToString() +" / " + BoatDetail["LAY_UP_PERIOD_FROM_DAY"].ToString() + " ";
						if (BoatDetail["LAY_UP_PERIOD_TO_MONTH"].ToString()!="0" && BoatDetail["LAY_UP_PERIOD_TO_DAY"].ToString()!="0")
							strLayUPTo=" To "+ MonthName[BoatDetail["LAY_UP_PERIOD_TO_MONTH"].ToString()].ToString() +" / " + BoatDetail["LAY_UP_PERIOD_TO_DAY"].ToString();
				
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATLAYUP   " + fieldType +"=\""+ fieldTypeText +"\">" + strLayUPFrom + strLayUPTo + "</BOATLAYUP>"; 
						#endregion

						#region Boat Element for suppliment Page
						BoatRootElementSupplement.AppendChild(BoatElementSuppliment);
						BoatElementSuppliment.SetAttribute(fieldType,fieldTypeNormal);
						BoatElementSuppliment.SetAttribute(id,BoatCtr.ToString());
						BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml +  "<APPLICANTNAME " + fieldType +"=\""+ fieldTypeText +"\">"+ApplicantName1+"</APPLICANTNAME>"; 
						BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml +  "<APPLICANTIONNUMBER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(PolicyNumber)+"</APPLICANTIONNUMBER>"; 
						BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml +  "<BOATNUMBER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_NO"].ToString()+ " TYPE: " + BoatDetail["BOATTYPE"].ToString())+"</BOATNUMBER>"; 
						BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml +  "<BOATYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_YEAR"].ToString())+"</BOATYEAR>"; 
						BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml +  "<BOATMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_MAKE_MODEL"].ToString())+"</BOATMAKEMODEL>"; 
						#endregion

						#region Acord82 Engine Details
											
						#region setting Root ENGINE Attribute
						XmlElement EngineRootElement;
						EngineRootElement	= AcordPDFXML.CreateElement("ENGINE");
						BoatElementAcord82.AppendChild(EngineRootElement);
						EngineRootElement.SetAttribute(fieldType,fieldTypeMultiple);
						EngineRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82ENGINE"));
						EngineRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82ENGINE"));
						EngineRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82ENGINEEXTN"));
						EngineRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82ENGINEEXTN"));
						#endregion
					
						// fetching boat engine information

						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
						gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						DSTempEngineTrailer = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_ENGINE_DETAILS");
						gobjWrapper.ClearParameteres();

						//DSTempEngineTrailer = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_ENGINE_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");

						foreach(DataRow EngineDetail in DSTempEngineTrailer.Tables[0].Rows)
						{
							XmlElement EngineElement;
							EngineElement = AcordPDFXML.CreateElement("ENGINEINFO");
							EngineRootElement.AppendChild(EngineElement);
							EngineElement.SetAttribute(fieldType,fieldTypeNormal);
							EngineElement.SetAttribute(id,Eng_tra_Ctr.ToString());

							EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINEYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["YEAR"].ToString())+"</ENGINEYEAR>"; 
							EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINEMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["MAKE_MODEL"].ToString())+"</ENGINEMAKEMODEL>"; 
							EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINESRNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["SERIAL_NO"].ToString())+"</ENGINESRNUM>"; 
							EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINEHP " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["HORSEPOWER"].ToString())+"</ENGINEHP>"; 
							EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINEFUELTYPE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(EngineDetail["FUEL_TYPE"].ToString())+"</ENGINEFUELTYPE>"; 
							//EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINEPRESENTVALUE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["PRESENT_VALUE"].ToString())+"</ENGINEPRESENTVALUE>"; 
							EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINEPRESENTVALUE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</ENGINEPRESENTVALUE>"; 
							if(BoatDetail["BOAT_POWER"]!=null && BoatDetail["BOAT_POWER"].ToString()!= "")
								EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINEOTHER " + fieldType +"=\""+ fieldTypeText +"\">" + BoatDetail["TWIN_SINGLE"].ToString() + " - " +  RemoveJunkXmlCharacters(EngineDetail["OTHER"].ToString()) + "</ENGINEOTHER>"; 

							Eng_tra_Ctr++;
						}
						Eng_tra_Ctr = 0;
						#endregion

						#region Acord82 trailer Details

						#region setting Root TRAILER Attribute
						XmlElement TrailerRootElement;
						TrailerRootElement = AcordPDFXML.CreateElement("TRAILER"); 
						BoatElementAcord82.AppendChild(TrailerRootElement);
						TrailerRootElement.SetAttribute(fieldType,fieldTypeMultiple);
						TrailerRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82TRAILER"));
						TrailerRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82TRAILER"));
						TrailerRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82TRAILEREXTN"));
						TrailerRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82TRAILEREXTN"));
						#endregion
					
						// maping Boat attached Trailer 

						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
						gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						DSTempEngineTrailer = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_TRAILER_DETAILS");
						gobjWrapper.ClearParameteres();

						//DSTempEngineTrailer = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_TRAILER_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");

						foreach(DataRow TrailerDetail in DSTempEngineTrailer.Tables[0].Rows)
						{
							XmlElement TrailerElement;
							TrailerElement = AcordPDFXML.CreateElement("TRAILERINFO");
							TrailerRootElement.AppendChild(TrailerElement);
							TrailerElement.SetAttribute(fieldType,fieldTypeNormal);
							TrailerElement.SetAttribute(id,Eng_tra_Ctr.ToString());
					
							TrailerElement.InnerXml= TrailerElement.InnerXml +  "<TRAILERYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(TrailerDetail["YEAR"].ToString())+"</TRAILERYEAR>"; 
							if(TrailerDetail["MODEL"].ToString() !="" && TrailerDetail["MANUFACTURER"].ToString() !="")
							{
								TrailerElement.InnerXml= TrailerElement.InnerXml +  "<TRAILERMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(TrailerDetail["MANUFACTURER"].ToString()+ "/" + TrailerDetail["MODEL"].ToString())+"</TRAILERMAKEMODEL>"; 
							}
							else if(TrailerDetail["MANUFACTURER"].ToString() !="")
							{
								TrailerElement.InnerXml= TrailerElement.InnerXml +  "<TRAILERMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(TrailerDetail["MANUFACTURER"].ToString())+"</TRAILERMAKEMODEL>"; 
							}
							else
							{
								TrailerElement.InnerXml= TrailerElement.InnerXml +  "<TRAILERMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(TrailerDetail["MODEL"].ToString())+"</TRAILERMAKEMODEL>"; 
							}
							TrailerElement.InnerXml= TrailerElement.InnerXml +  "<TRAILERSRNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(TrailerDetail["SERIAL_NO"].ToString())+"</TRAILERSRNUM>"; 
							if(TrailerDetail["INSURED_VALUE"].ToString()!="")
							{
								if(Eng_tra_Ctr == 0)
								{
									TrailerElement.InnerXml= TrailerElement.InnerXml +  "<TRAILERCOST " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(TrailerDetail["INSURED_VALUE"].ToString().Replace(".00",""))+"</TRAILERCOST>"; 
								}
								else
								{
									TrailerElement.InnerXml= TrailerElement.InnerXml +  "<TRAILERCOST " + fieldType +"=\""+ fieldTypeText +"\">$"+RemoveJunkXmlCharacters(TrailerDetail["INSURED_VALUE"].ToString().Replace(".00",""))+"</TRAILERCOST>"; 
								}
							}
							else
							{
								TrailerElement.InnerXml= TrailerElement.InnerXml +  "<TRAILERCOST " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</TRAILERCOST>"; 
							}
							Eng_tra_Ctr++;
						}
						Eng_tra_Ctr = 0;
						#endregion
					}

					#region COVERAGES

					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempEngineTrailer = gobjWrapper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails");
					gobjWrapper.ClearParameteres();

					//DSTempEngineTrailer = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
				
					// Three Boat coverages moved separately for other in separate xml portion
					// 16-Aug 2007 Mohit Agarwal
				

					foreach(DataRow CoverageDetails in DSTempEngineTrailer.Tables[0].Rows)
					{
						//Added by asfa (20-Feb-2008) - iTrack issue #3331
						string covPrem="";
						if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
							covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
						else // when gStrtemp != "temp"
							covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
						//					if(CoverageDetails["COV_CODE"].ToString() == "EBIUE")
						//					{
						//						unattach_prem = double.Parse(covPrem);
						//					}
					
						if(covPrem == "")
							covPrem = "Included";
						else if(covPrem != "Included" && covPrem != "Extended") 
						{
							covPrem ="$" + covPrem;
						}
						// premium for Unattached Equipment And Personal Effects
						//try
						//{
						//if(CoverageDetails["COV_CODE"].ToString() == "EBIUE" && covPrem.Trim() != "Included")
						//unattach_prem = double.Parse(covPrem.Replace("$",""));
						//covPrem = "$" + System.Convert.ToString(unattach_prem);
						//}
						//catch(Exception ex)
						//{}

						switch(CoverageDetails["COV_CODE"].ToString())
						{
								// premium for Watercraft Liability
							case "LCCSL":
								// mapping for dec page
								if (gStrPdfFor == PDFForDecPage)
								{
								
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALLIAB " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVPERSONALLIAB>";
									if(CoverageDetails["LIMIT_1"].ToString() != "")
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALLIABLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","")+ " Each Occurrence" )+"</BOATCOVPERSONALLIABLIM>";
									else
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALLIABLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString())+"</BOATCOVPERSONALLIABLIM>";
									if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString() =="Extended from HO")
									{
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALLIABPREM " + fieldType +"=\""+ fieldTypeText +"\">"+"Extended"+"</BOATCOVPERSONALLIABPREM>";
								
									}
									else
									{
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALLIABPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPERSONALLIABPREM>";
									}

									//								if(htpremium.Contains("BOAT_ LIABILITY_PREMIUM"))
									//								{
									//									lstrGetPremium = htpremium["BOAT_ LIABILITY_PREMIUM"].ToString();
									//									lintGetindex   = lstrGetPremium.IndexOf(".");
									//									if(lintGetindex ==-1)
									//									{
									//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALLIABPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_ LIABILITY_PREMIUM"].ToString() + ".00")+"</BOATCOVPERSONALLIABPREM>";
									//									}
									//									else
									//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALLIABPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_ LIABILITY_PREMIUM"].ToString())+"</BOATCOVPERSONALLIABPREM>";
									//								}
									//								
								}
								break;
								// premium for Section II - Medical
							case "MCPAY":
								// mapping for dec page
								if (gStrPdfFor == PDFForDecPage)
								{
									//BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVMEDPM>";
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Medical Payment")+"</BOATCOVMEDPM>";
									if(CoverageDetails["LIMIT_1"].ToString() != "")
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPMLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","") + " Per Person /$25,000 Each Occurrence" )+"</BOATCOVMEDPMLIM>";
									else
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPMLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString())+"</BOATCOVMEDPMLIM>";
									if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString() =="Extended from HO")
									{
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+"Extended"+"</BOATCOVMEDPMPREM>";
								
									}
									else
									{
										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVMEDPMPREM>";
									}
									//								if(htpremium.Contains("BOAT_MP_PREMIUM"))
									//								{
									//									lstrGetPremium = htpremium["BOAT_MP_PREMIUM"].ToString();
									//									lintGetindex = lstrGetPremium.IndexOf(".");
									//									if(lintGetindex ==-1)
									//									{
									//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_MP_PREMIUM"].ToString() + ".00")+"</BOATCOVMEDPMPREM>";
									//									}
									//									else
									//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_MP_PREMIUM"].ToString())+"</BOATCOVMEDPMPREM>";
									//
									//								}
									//								
								}
								break;
								// premium for Uninsured Boaters
							case "UMBCS":
								// mapping for dec page
								if (gStrPdfFor == PDFForDecPage)
								{
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUMBOATERS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVUMBOATERS>";
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUMBOATERSLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","") + " Each Occurrence/Each Unit")+"</BOATCOVUMBOATERSLIM>";
									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUMBOATERSPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVUMBOATERSPREM>";
									//								if(htpremium.Contains("BOAT_UB_PREMIUM"))
									//								{	
									//									lstrGetPremium = htpremium["BOAT_UB_PREMIUM"].ToString();
									//									lintGetindex = lstrGetPremium.IndexOf(".");
									//									if(lintGetindex ==-1)
									//									{
									//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUMBOATERSPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UB_PREMIUM"].ToString() + ".00")+"</BOATCOVUMBOATERSPREM>";
									//									}
									//									else
									//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUMBOATERSPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UB_PREMIUM"].ToString())+"</BOATCOVUMBOATERSPREM>";
									//
									//								}
									//								
								}
								break;
							default: break;
						}
					}
					string strAVDed="",strAVLim="";
					double sumTtl=0;
					double dblSumUnattach=0;
					// premium of trailer attched with boat
					foreach (XmlNode SumTotalNode in GetSumTotalTrailerPremium())
					{
						if(getAttributeValue(SumTotalNode,"STEPPREMIUM")!=null && getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()!="" )
							sumTtl += double.Parse(getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()) ;						

					}
	
					if(BoatCtr != 0)
						BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<TOTALBOATPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+("$" + DollarFormat(sumTtlB + finaltrailPrem))+"</TOTALBOATPREMIUM>";
					else
						BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<TOTALBOATPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+"$" + DollarFormat(sumTtlB+ finaltrailPrem + unattach_prem)+"</TOTALBOATPREMIUM>";
					if(gStrtemp!="final")
					{
						BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<TOTALPOLPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+("$" + DollarFormat(sumTtlC+(double)SchPrem+unattach_prem))+"</TOTALPOLPREMIUM>";							
					}
					else
					{
						BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<TOTALPOLPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+("$" + DollarFormat(sumTtlC+(double)SchPrem))+"</TOTALPOLPREMIUM>";							
					}
				
					if(gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS.ToString() || gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS.ToString() 
						|| gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REINSTATEMENT_PROCESS.ToString() || gStrProcessID == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REINSTATEMENT_PROCESS.ToString())
					{
				
						// premium adjusted after endorsement
						// if procces not committed
						if(gStrtemp!="final")
						{
							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<POLPREMIUMADJUSTMENT " + fieldType +"=\""+ fieldTypeText +"\">"+("$" + DollarFormat(GetEffectivePremium(dblOldSum,sumTtlC+unattach_prem+(double)SchPrem)))+"</POLPREMIUMADJUSTMENT>";							
						}
							// if proccess committed
						else
						{
							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<POLPREMIUMADJUSTMENT " + fieldType +"=\""+ fieldTypeText +"\">"+("$" + DollarFormat(dblOldSum))+"</POLPREMIUMADJUSTMENT>";							
						}
					}
					else
					{
						if(dblOldSum != 0 || dblOldSum != 0.00)
						{
							// premium adjusted after endorsement
							// if procces not committed
							if(gStrtemp!="final")
							{
								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<POLPREMIUMADJUSTMENT " + fieldType +"=\""+ fieldTypeText +"\">"+("$" + DollarFormat(GetEffectivePremium(dblOldSum,sumTtlC+unattach_prem+(double)SchPrem)))+"</POLPREMIUMADJUSTMENT>";							
							}
								// if proccess committed
							else
							{
								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<POLPREMIUMADJUSTMENT " + fieldType +"=\""+ fieldTypeText +"\">"+("$" + DollarFormat(dblOldSum))+"</POLPREMIUMADJUSTMENT>";							
							}
						}
					}
					#region DecPage CovRoot Element
					XmlElement DecPageCovRootElement;
					DecPageCovRootElement = AcordPDFXML.CreateElement("COVERAGES");
					BoatElementDecPage.AppendChild(DecPageCovRootElement);
					DecPageCovRootElement.SetAttribute(fieldType,fieldTypeMultiple);
					DecPageCovRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECOVERAGE"));
					DecPageCovRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGECOVERAGE"));
					DecPageCovRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGECOVERAGEEXTN"));
					DecPageCovRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGECOVERAGEEXTN"));
					#endregion

				
					int CovCtr=0;
					//Added by Mohit Agarwal 6-Aug-07 for Endorsements Print Order

					if(gStrPdfFor == PDFForDecPage)
					{
						foreach(DataRow CoverageDetails in DSTempEngineTrailer.Tables[0].Rows)
						{	
							int prnCtr = 0;
							string CovCode=CoverageDetails["COV_CODE"].ToString();
							if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y" && (CovCode=="EBSCEAV" || CovCode=="EBSMECE" || CovCode=="WAT400" || CovCode=="EBSMWL" || CovCode=="BRCC"|| CovCode=="OP300M") && CoverageDetails["ENDORS_PRINT"].ToString() !="N")
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
						// Print order for WP100 will be last because it contains signature
						foreach(DataRow CoverageDetails in DSTempEngineTrailer.Tables[0].Rows)
						{
							int prnCtr = 0;
							string CovCode=CoverageDetails["COV_CODE"].ToString();
							if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y" && (CovCode=="WP100" ) && CoverageDetails["ENDORS_PRINT"].ToString() !="N")
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
						}

					}
					string strDeduct="";
					foreach(DataRow CoverageDetails in DSTempEngineTrailer.Tables[0].Rows)
					{

					
						string covCode = CoverageDetails["COV_CODE"].ToString();
						//Added by asfa (20-Feb-2008) - iTrack issue #3331
						string covPrem="";
						if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
							covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
						else // when gStrtemp != "temp"
							covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
						if(covPrem == "")
							covPrem = "Included";
						else if(covPrem != "Included") 
						{
							covPrem ="$" + covPrem;
						}
						if(CoverageDetails["COV_CODE"].ToString()=="BDEDUC")
						{
							strDeduct= CoverageDetails["DEDUCTIBLE_1"].ToString();
							string grandpercents = CoverageDetails["DEDUCTIBLE1_AMOUNT_TEXT"].ToString();
							if(grandpercents.IndexOf("%") > 0)
							{
								double granddeduc = GetPrecentDed(grandpercents, BoatDetail["PRESENT_VALUE"].ToString());
								if(granddeduc != 0)
									strDeduct = "$" + DollarFormat(granddeduc);
							}
						}
						//string covCode1 = CoverageDetails1["COV_CODE"].ToString();
						boat_ded = CoverageDetails["DEDUCTIBLE_1"].ToString();
						string grandpercent = CoverageDetails["DEDUCTIBLE1_AMOUNT_TEXT"].ToString();
						if(grandpercent.IndexOf("%") > 0)
						{
							double granddeduc = GetPrecentDed(grandpercent, BoatDetail["PRESENT_VALUE"].ToString());
							if(granddeduc != 0)
								boat_ded = "$" + DollarFormat(granddeduc);
						}

						if((gStrPdfFor == PDFForDecPage) && ((covCode == "BDEDUC") ||(covCode == "EBPPDACV") || (covCode == "EBPPDAV") || (covCode == "EBPPDJ") || (covCode == "TRAILER") || (covCode == "OUTBOARD1") || (covCode == "OUTBOARD2") || (covCode == "PORTACCESS") || (covCode == "LCCSL") || (covCode == "MCPAY") || (covCode == "UMBCS")))
							continue;

						if(BoatDetail["BOAT_ID"].ToString() != "1" && gStrPdfFor == PDFForDecPage && covCode == "EBIUE")
							continue;

						#region DecPage  Coverage Element 
						XmlElement DecpageCovElement;
						DecpageCovElement = AcordPDFXML.CreateElement("COVERAGEINFO");
						DecPageCovRootElement.AppendChild(DecpageCovElement);
						DecpageCovElement.SetAttribute(fieldType,fieldTypeNormal);
						DecpageCovElement.SetAttribute(id,CovCtr.ToString());
					

						// Boat Deductible "BDEDUC" 
						string strDeductible="0";
						strDeductible=GetDeducWat(DSTempEngineTrailer,"BDEDUC");
						strDeductible=strDeductible.Replace(".00","");
						#endregion
						#region SWITCH CASE
						switch(CoverageDetails["COV_CODE"].ToString())
						{

							
								// mapping for Section 1 - Covered Property Damage - Actual Cash Value
							case "EBPPDACV":
								// mapping for dec page
							
								//break;
								if (gStrPdfFor == PDFForDecPage)
								{
									//								if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"].ToString()!="" )
									//									strAVLim=CoverageDetails["LIMIT_1"].ToString(); 
									//								if(CoverageDetails["DEDUCTIBLE_1"]!=null && CoverageDetails["DEDUCTIBLE_1"].ToString()!="" )
									//									strAVDed=CoverageDetails["DEDUCTIBLE_1"].ToString(); 
									DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
									DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
									if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
										DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
									DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
									DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATCOVDED>";
									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
									CovCtr++;
								}
									// mapping for accord page
								else if (gStrPdfFor == PDFForAcord)
								{
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$",""))+"</BOATCOVHULLLIM>";
									if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"].ToString()!="" )
										strAVLim=CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$",""); 

									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00","").Replace("$",""))+"</BOATCOVHULLDED>";
									if(CoverageDetails["DEDUCTIBLE_1"]!=null && CoverageDetails["DEDUCTIBLE_1"].ToString()!="" )
										strAVDed=CoverageDetails["DEDUCTIBLE_1"].ToString().Replace(".00","").Replace("$",""); 
								
									//BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVHULLPREM>";
									//Added by asfa (20-Feb-2008) - iTrack issue #3331
									covPrem="";
									if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
										covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
									else // when gStrtemp != "temp"
										covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVHULLPREM>";
					
									if(htpremium.Contains("BOAT_PD"))
									{
										lstrGetPremium = htpremium["BOAT_PD"].ToString();
										lintGetindex = lstrGetPremium.IndexOf(".");
										if(lintGetindex == -1)
										{
											//										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_PD"].ToString() + ".00")+"</BOATCOVHULLPREM>";
										}
										//									else
										//										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_PD"].ToString())+"</BOATCOVHULLPREM>";

										dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
									}
								}
								break;
								// mapping for Section 1 - Covered Property Damage - Agreed Value
							case "EBPPDAV":
								// mapping for  dec page
								if (gStrPdfFor == PDFForDecPage)
								{
									//								if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"].ToString()!="" )
									//									strAVLim=CoverageDetails["LIMIT_1"].ToString(); 
									//								if(CoverageDetails["DEDUCTIBLE_1"]!=null && CoverageDetails["DEDUCTIBLE_1"].ToString()!="" )
									//									strAVDed=CoverageDetails["DEDUCTIBLE_1"].ToString(); 
									DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
									DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
									if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
										DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
									DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
									DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATCOVDED>";
									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
									CovCtr++;
								}
									// mapping for accord page
								else if (gStrPdfFor == PDFForAcord)
								{
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$",""))+"</BOATCOVHULLLIM>";
									if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"].ToString()!="" )
										strAVLim=CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$",""); 
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00","").Replace("$",""))+"</BOATCOVHULLDED>";
									if(CoverageDetails["DEDUCTIBLE_1"]!=null && CoverageDetails["DEDUCTIBLE_1"].ToString()!="" )
										strAVDed=CoverageDetails["DEDUCTIBLE_1"].ToString().Replace(".00","").Replace("$",""); 
									//BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVHULLPREM>";
									//Added by asfa (20-Feb-2008) - iTrack issue #3331
									covPrem="";
									if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
										covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
									else // when gStrtemp != "temp"
										covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVHULLPREM>";

									if(htpremium.Contains("BOAT_PD"))
									{	
										lstrGetPremium = htpremium["BOAT_PD"].ToString();
										lintGetindex = lstrGetPremium.IndexOf(".");
										if(lintGetindex ==-1)
										{
											//										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_PD"].ToString() + ".00")+"</BOATCOVHULLPREM>";
										}
										//									else
										//										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_PD"].ToString())+"</BOATCOVHULLPREM>";

										dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
									}
								}
								break;
								//
							case "OUTBOARD1":
								// mapping for accord page
								if (gStrPdfFor == PDFForAcord)
								{
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVOM1LIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$",""))+"</BOATCOVOM1LIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVOM1DED " + fieldType +"=\""+ fieldTypeText +"\">"+boat_ded.Replace("$","")+"</BOATCOVOM1DED>";
								}
								// mapping for dec page
								if(gStrPdfFor == PDFForDecPage)
								{
									DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
									DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
									if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
										DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
									DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
									DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATCOVDED>";
									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
									CovCtr++;

								}
								break;
							case "OUTBOARD2":
								if (gStrPdfFor == PDFForAcord)
								{
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVOM2LIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$",""))+"</BOATCOVOM2LIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVOM2DED " + fieldType +"=\""+ fieldTypeText +"\">"+boat_ded.Replace(".00","").Replace("$","")+"</BOATCOVOM2DED>";
								}
								break;
							case "PORTACCESS":
								if (gStrPdfFor == PDFForAcord)
								{
									if(BoatCtr == 0)
									{
										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVPORTACCLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$",""))+"</BOATCOVPORTACCLIM>";
										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVPORTACCDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00","").Replace("$",""))+"</BOATCOVPORTACCDED>";
										if(SchPrem !=0)
										{
											BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVPORTACCPREM " + fieldType +"=\""+ fieldTypeText +"\">"+ (SchPrem.ToString() + ".00") +"</BOATCOVPORTACCPREM>";
										}
										else
										{
											BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVPORTACCPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</BOATCOVPORTACCPREM>";
										}
									}
								
								}
								//							if (gStrPdfFor == PDFForDecPage)
								//							{
								//								//								if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"].ToString()!="" )
								//								//									strAVLim=CoverageDetails["LIMIT_1"].ToString(); 
								//								//								if(CoverageDetails["DEDUCTIBLE_1"]!=null && CoverageDetails["DEDUCTIBLE_1"].ToString()!="" )
								//								//									strAVDed=CoverageDetails["DEDUCTIBLE_1"].ToString(); 
								//								DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
								//								DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
								//								if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
								//									DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
								//								DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</BOATCOVLIM>";
								//								DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString())+"</BOATCOVDED>";
								//								//								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVPREM>";
								//								CovCtr++;
								//							}
								break;
							case "TRAILER":
								if (gStrPdfFor == PDFForAcord)
								{
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVTRAILERLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$",""))+"</BOATCOVTRAILERLIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVTRAILERDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00","").Replace("$","")) +"</BOATCOVTRAILERDED>";
															
									if(trailertotalprem!=0 && CoverageDetails["LIMIT_1"].ToString() != "")
									{
										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVTRAILERPREM " + fieldType +"=\""+ fieldTypeText +"\">"+ (trailertotalprem.ToString() + ".00") +"</BOATCOVTRAILERPREM>";
									}
									dblSumTotal+=sumTtl;
								}
								if(gStrPdfFor == PDFForDecPage)
								{
									DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
									DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
									if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
										DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
									DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
									DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATCOVDED>";
									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
									CovCtr++;

								}
								break;
								// mapping for Boat Replacement Cost
							case "BRCC":
							{
								// mapping for dec page
								if(gStrPdfFor == PDFForDecPage)
								{
									DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
									DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
									if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
										DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
									DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
									DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATCOVDED>";
									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
									CovCtr++;

								}
									// mapping for accord page
								else if (gStrPdfFor == PDFForAcord)
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVBRCCDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVBRCCDESC>";
									if(CoverageDetails["LIMIT_1"].ToString() !="" && CoverageDetails["LIMIT_1"].ToString() !="0" && CoverageDetails["LIMIT_1"].ToString() !="0.00" && CoverageDetails["LIMIT_1"].ToString() !="Included")
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVBRCCLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVBRCCLIMIT>";
									}
									else
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVBRCCLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$",""))+"</BOATCOVBRCCLIMIT>";
									}
									if(boat_ded !="" && boat_ded != "0" && boat_ded != "0.00" && boat_ded !="Included")
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVBRCCDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATCOVBRCCDEDUCTIBLE>";
									}
									else
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVBRCCDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00","").Replace("$",""))+"</BOATCOVBRCCDEDUCTIBLE>";
									}
									//BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVBRCCPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVBRCCPREMIUM>";
									//Added by asfa (20-Feb-2008) - iTrack issue #3331
									covPrem="";
									if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
										covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
									else // when gStrtemp != "temp"
										covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
									if(covPrem !="" && covPrem !="0" && covPrem != "0.00" && covPrem !="Included")
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVBRCCPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">$"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVBRCCPREMIUM>";
									}
									else
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVBRCCPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVBRCCPREMIUM>";
									}
								}
								break;

							}
								// mapping for Watercraft Liability
							case "LCCSL":
								// mapping for dec page
								if (gStrPdfFor == PDFForAcord)
								{
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVCSLLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$",""))+"</BOATCOVCSLLIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVCSLDED " + fieldType +"=\""+ fieldTypeText +"\">N/A</BOATCOVCSLDED>";
									//BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVCSLPREM>";
									//Added by asfa (20-Feb-2008) - iTrack issue #3331
									covPrem="";
									if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
										covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
									else // when gStrtemp != "temp"
										covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
									if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString() =="Extended from HO")
									{
										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+"Extended"+"</BOATCOVCSLPREM>";
									}
									else
									{
										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVCSLPREM>";
									}
									if(htpremium.Contains("BOAT_ LIABILITY_PREMIUM"))
									{
										lstrGetPremium = htpremium["BOAT_ LIABILITY_PREMIUM"].ToString();
										lintGetindex = lstrGetPremium.IndexOf(".");
										if(lintGetindex ==-1)
										{
											//										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_ LIABILITY_PREMIUM"].ToString() + ".00")+"</BOATCOVCSLPREM>";
										}
										else
											//										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_ LIABILITY_PREMIUM"].ToString())+"</BOATCOVCSLPREM>";

											dblSumTotal+=int.Parse(htpremium["BOAT_ LIABILITY_PREMIUM"].ToString());
									}
								}
									// mapping for dec pge
								else if (gStrPdfFor == PDFForDecPage)
								{
								
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALLIAB " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVPERSONALLIAB>";
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALLIABLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString()+ " Each Occurence" )+"</BOATCOVPERSONALLIABLIM>";
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALLIABPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVPERSONALLIABPREM>";
									//								if(htpremium.Contains("BOAT_ LIABILITY_PREMIUM"))
									//								{
									//									lstrGetPremium = htpremium["BOAT_ LIABILITY_PREMIUM"].ToString();
									//									lintGetindex   = lstrGetPremium.IndexOf(".");
									//									if(lintGetindex ==-1)
									//									{
									//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALLIABPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_ LIABILITY_PREMIUM"].ToString() + ".00")+"</BOATCOVPERSONALLIABPREM>";
									//									}
									//									else
									//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALLIABPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_ LIABILITY_PREMIUM"].ToString())+"</BOATCOVPERSONALLIABPREM>";
									//								}
									//								
								}
								break;
								// mapping for Section II - Medical
							case "MCPAY":
								// mapping for accord page
								if (gStrPdfFor == PDFForAcord)
								{
									if(CoverageDetails["LIMIT_1"].ToString()!="")
									{
										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVMEDPMLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$","")+"/25,000")+"</BOATCOVMEDPMLIM>";
									}
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVMEDPMDED " + fieldType +"=\""+ fieldTypeText +"\">N/A</BOATCOVMEDPMDED>";
									//BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVMEDPMPREM>";
									//Added by asfa (20-Feb-2008) - iTrack issue #3331
									covPrem="";
									if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
										covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
									else // when gStrtemp != "temp"
										covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
									if(CoverageDetails["LIMIT1_AMOUNT_TEXT"].ToString() =="Extended from HO")
									{
										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+"Extended"+"</BOATCOVMEDPMPREM>";
								
									}
									else
									{
										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVMEDPMPREM>";
									}
									if(htpremium.Contains("BOAT_MP_PREMIUM"))
									{
										lstrGetPremium = htpremium["BOAT_MP_PREMIUM"].ToString();
										lintGetindex = lstrGetPremium.IndexOf(".");
										if(lintGetindex ==-1)
										{
											//										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_MP_PREMIUM"].ToString() + ".00")+"</BOATCOVMEDPMPREM>";
										}
										else
											//										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_MP_PREMIUM"].ToString())+"</BOATCOVMEDPMPREM>";

											dblSumTotal+=int.Parse(htpremium["BOAT_MP_PREMIUM"].ToString());
									}
								}
									// mapping for de cpage
								else if (gStrPdfFor == PDFForDecPage)
								{
									//BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVMEDPM>";
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Medical Payment")+"</BOATCOVMEDPM>";
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPMLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString() + " Per Person 25000.00 Each Occurence" )+"</BOATCOVMEDPMLIM>";
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVMEDPMPREM>";
									//								if(htpremium.Contains("BOAT_MP_PREMIUM"))
									//								{
									//									lstrGetPremium = htpremium["BOAT_MP_PREMIUM"].ToString();
									//									lintGetindex = lstrGetPremium.IndexOf(".");
									//									if(lintGetindex ==-1)
									//									{
									//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_MP_PREMIUM"].ToString() + ".00")+"</BOATCOVMEDPMPREM>";
									//									}
									//									else
									//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_MP_PREMIUM"].ToString())+"</BOATCOVMEDPMPREM>";
									//
									//								}
									//								
								}
								break;
								// mapping for Uninsured Boaters
							case "UMBCS":
								// mapping for accord pge
								if (gStrPdfFor == PDFForAcord)
								{
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVUBCSLLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$","") )+"</BOATCOVUBCSLLIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVUBCSLDED " + fieldType +"=\""+ fieldTypeText +"\">N/A</BOATCOVUBCSLDED>";
									//BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVUBCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVUBCSLPREM>";
									//Added by asfa (20-Feb-2008) - iTrack issue #3331
									covPrem="";
									if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
										covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
									else // when gStrtemp != "temp"
										covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVUBCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVUBCSLPREM>";
									if(htpremium.Contains("BOAT_UB_PREMIUM"))
									{
										lstrGetPremium = htpremium["BOAT_UB_PREMIUM"].ToString();
										lintGetindex = lstrGetPremium.IndexOf(".");
										if(lintGetindex ==-1)
										{
											//										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVUBCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UB_PREMIUM"].ToString() + ".00")+"</BOATCOVUBCSLPREM>";
										}
										else
											//										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVUBCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UB_PREMIUM"].ToString())+"</BOATCOVUBCSLPREM>";
											dblSumTotal+=int.Parse(htpremium["BOAT_UB_PREMIUM"].ToString());
									}
								}
									// mapping for dec page
								else if (gStrPdfFor == PDFForDecPage)
								{
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUMBOATERS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVUMBOATERS>";
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUMBOATERSLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString() + " Each Occurence/Each Unit")+"</BOATCOVUMBOATERSLIM>";
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUMBOATERSPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVUMBOATERSPREM>";
									//								if(htpremium.Contains("BOAT_UB_PREMIUM"))
									//								{	
									//									lstrGetPremium = htpremium["BOAT_UB_PREMIUM"].ToString();
									//									lintGetindex = lstrGetPremium.IndexOf(".");
									//									if(lintGetindex ==-1)
									//									{
									//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUMBOATERSPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UB_PREMIUM"].ToString() + ".00")+"</BOATCOVUMBOATERSPREM>";
									//									}
									//									else
									//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUMBOATERSPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UB_PREMIUM"].ToString())+"</BOATCOVUMBOATERSPREM>";
									//
									//								}
									//								
								}
								break;
								// mapping for Broad Form Watercraft Policy (WP-100)
							case "WP100":
								// mapping for dec page
								if (gStrPdfFor == PDFForDecPage)
								{
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVWCRAFTPOLICYDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVWCRAFTPOLICYDESC>";
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVWCRAFTPOLICYNUM " + fieldType +"=\""+ fieldTypeText +"\">WP-100</BOATCOVWCRAFTPOLICYNUM>";
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVWCRAFTPOLICYDATE " + fieldType +"=\""+ fieldTypeText +"\">03/01</BOATCOVWCRAFTPOLICYDATE>";
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVWCRAFTPOLICYLIM " + fieldType +"=\""+ fieldTypeText +"\"></BOATCOVWCRAFTPOLICYLIM>";
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVWCRAFTPOLICYDED " + fieldType +"=\""+ fieldTypeText +"\"></BOATCOVWCRAFTPOLICYDED>";
									//								if(htpremium.Count>0)
									//									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVWCRAFTPOLICYPREM " + fieldType +"=\""+ fieldTypeText +"\">Included</BOATCOVWCRAFTPOLICYPREM>";
									//								
									//								
									//								#region Dec Page Element
									//								if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
									//								{
									//
									//									XmlElement DecPageBoatEndoWP100;
									//									DecPageBoatEndoWP100 = AcordPDFXML.CreateElement("BOATENDORSEMENTWP100");
									//									BoatElementDecPage.AppendChild(DecPageBoatEndoWP100);
									//									DecPageBoatEndoWP100.SetAttribute(fieldType,fieldTypeMultiple);
									//									DecPageBoatEndoWP100.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEWP100"));
									//									DecPageBoatEndoWP100.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEWP100"));
									//									DecPageBoatEndoWP100.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEWP100EXTN"));
									//									DecPageBoatEndoWP100.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEWP100EXTN"));
									//
									//									XmlElement EndoElementWP100;
									//									EndoElementWP100 = AcordPDFXML.CreateElement("EndoElementWP100INFO");
									//									DecPageBoatEndoWP100.AppendChild(EndoElementWP100);
									//									EndoElementWP100.SetAttribute(fieldType,fieldTypeNormal);
									//									EndoElementWP100.SetAttribute(id,BoatCtr.ToString());
									//								}
									//								#endregion
									DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
									DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
									if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
										DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
									string limit="0";
									if(CoverageDetails["LIMIT_1"].ToString()=="" || CoverageDetails["LIMIT_1"].ToString()==null || CoverageDetails["LIMIT_1"].ToString()=="" || CoverageDetails["LIMIT_1"].ToString()=="0" || CoverageDetails["LIMIT_1"].ToString()=="0.00")
									{
										limit="Included";
									}
									else
									{
										limit=CoverageDetails["LIMIT_1"].ToString();
									}
									DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(limit.Replace(".00",""))+"</BOATCOVLIM>";
									DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDeduct.Replace(".00",""))+"</BOATCOVDED>";
									//								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVPREM>";
									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
									//								if(htpremium.Count>0)
									//									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">Included</BOATCOVPREM>";
								
								
									CovCtr++;
								}
									// mapping for accord page
								else if (gStrPdfFor == PDFForAcord)
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWPDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVWPDESC>";
									string limit="0";
									if(CoverageDetails["LIMIT_1"].ToString()=="" || CoverageDetails["LIMIT_1"].ToString()==null || CoverageDetails["LIMIT_1"].ToString()=="0" || CoverageDetails["LIMIT_1"].ToString()=="0.00")
									{
										limit="Included";
									}
									else
									{
										limit= "$" + CoverageDetails["LIMIT_1"].ToString();
									}
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWPLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(limit.Replace(".00",""))+"</BOATCOVWPLIMIT>";
									if(strDeduct !="" && strDeduct !="0" && strDeduct != "0.00" && strDeduct != "Included")
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWPDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDeduct.Replace(".00",""))+"</BOATCOVWPDEDUCTIBLE>";
									}
									else
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWPDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDeduct.Replace(".00","").Replace("$",""))+"</BOATCOVWPDEDUCTIBLE>";
									}
									//BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWPPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVWPPREMIUM>";
									//Added by asfa (20-Feb-2008) - iTrack issue #3331
									covPrem="";
									if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
										covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
									else // when gStrtemp != "temp"
										covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
									if(covPrem !="" && covPrem !="0" && covPrem != "0.00" && covPrem != "Included")
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWPPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">$"+RemoveJunkXmlCharacters("Included")+"</BOATCOVWPPREMIUM>";
									}
									else
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWPPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</BOATCOVWPPREMIUM>";
									}
								}
								break;
								// mapping for Boat Towing and Emergency Service
							case "BTESC":
								// mapping for dec page
								if (gStrPdfFor == PDFForDecPage)
								{
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVTOWINGDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVTOWINGDESC>";
									//								if(BoatDetail["PRESENT_VALUE"]!= null && BoatDetail["PRESENT_VALUE"].ToString()!="")
									//									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVTOWINGLIM " + fieldType +"=\""+ fieldTypeText +"\">"+( (.05) * double.Parse(BoatDetail["PRESENT_VALUE"].ToString()) + ".00")+"</BOATCOVTOWINGLIM>";
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVTOWINGDED " + fieldType +"=\""+ fieldTypeText +"\">N/A</BOATCOVTOWINGDED>";
									//								if(htpremium.Count>0 )
									//								{
									//									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVTOWINGPREM " + fieldType +"=\""+ fieldTypeText +"\">Included</BOATCOVTOWINGPREM>";
									//								}
									DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
									DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
									if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
										DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
									DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
									DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+"N/A"+"</BOATCOVDED>";
									//								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVPREM>";
									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
									//								if(htpremium.Count>0 )
									//								{
									//									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">Included</BOATCOVPREM>";
									//								}
									CovCtr++;
								}
								break;
								// mapping for Unattached Equipment And Personal Effects
							case "EBIUE":
								// mapping for dec page
								if (gStrPdfFor == PDFForDecPage)
								{
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUNATTACHEQUIPDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVUNATTACHEQUIPDESC>";
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUNATTACHEQUIPLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</BOATCOVUNATTACHEQUIPLIM>";
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUNATTACHEQUIPDED " + fieldType +"=\""+ fieldTypeText +"\">100.00</BOATCOVUNATTACHEQUIPDED>";
									//								if(htpremium.Contains("BOAT_UNATTACH_PREMIUM"))
									//								{
									//									lstrGetPremium = htpremium["BOAT_UNATTACH_PREMIUM"].ToString();
									//									lintGetindex = lstrGetPremium.IndexOf(".");
									//									if(lintGetindex == -1)
									//									{
									//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUNATTACHEQUIPPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UNATTACH_PREMIUM"].ToString()  + ".00")+"</BOATCOVUNATTACHEQUIPPREM>";
									//									}
									//									else
									//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVUNATTACHEQUIPPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UNATTACH_PREMIUM"].ToString())+"</BOATCOVUNATTACHEQUIPPREM>";
									//
									//								}
									DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
									DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
									if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
										DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
									DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
									DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString().Replace(".00",""))+"</BOATCOVDED>";
									//								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVPREM>";
									CovCtr++;
									string UnattachedPremium="";
									UnattachedPremium =unattach_prem.ToString();
									if(UnattachedPremium == "" || UnattachedPremium == "0" ||  UnattachedPremium == "0.00")
										UnattachedPremium = "Included";
									else if(UnattachedPremium != "Included" && UnattachedPremium.IndexOf(".00")<0) 
									{
										UnattachedPremium ="$" + UnattachedPremium+".00";
									}
									else
									{
										UnattachedPremium ="$" + UnattachedPremium;
									}
									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(UnattachedPremium)+"</BOATCOVPREM>";
									//								if(htpremium.Contains("BOAT_UNATTACH_PREMIUM"))
									//								{
									//									lstrGetPremium = htpremium["BOAT_UNATTACH_PREMIUM"].ToString();
									//									lintGetindex = lstrGetPremium.IndexOf(".");
									//									if(lintGetindex == -1)
									//									{
									//										DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UNATTACH_PREMIUM"].ToString()  + ".00")+"</BOATCOVPREM>";
									//									}
									//									else
									//										DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UNATTACH_PREMIUM"].ToString())+"</BOATCOVPREM>";
									//
									//								}								
								}
									// mapping for accord page
								else if (gStrPdfFor == PDFForAcord)
								{
									string Unattachedincl = CoverageDetails["LIMIT_1"].ToString();
									Unattachedincl = Unattachedincl.Replace("$","");
									Unattachedincl = Unattachedincl.Replace(",","");
									Unattachedincl = Unattachedincl.Replace(".00","");

									int UnattachedInc = System.Convert.ToInt32(Unattachedincl) - UnattachedInclude;
								
									if(UnattachedInc!=0)
									{
										Unattachedincl = UnattachedInc.ToString("###,###");
										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATUNATTACHED " + fieldType +"=\""+ fieldTypeText +"\">"+"Unattached Equip."+"</BOATUNATTACHED>";
										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATUNATTACHEDLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Unattachedincl.ToString().Replace(".00","").Replace("$",""))+"</BOATUNATTACHEDLIM>";
										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATUNATTACHEDDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString().Replace(".00","").Replace("$","")) +"</BOATUNATTACHEDDED>";
									
										string UnattachedPremium="";
										UnattachedPremium =unattach_prem.ToString();
										if(UnattachedPremium == "" || UnattachedPremium == "0" ||  UnattachedPremium == "0.00" || UnattachedPremium == "Included")
											UnattachedPremium = "";
										else if(UnattachedPremium != "Included" && UnattachedPremium.IndexOf(".00")<0) 
										{
											UnattachedPremium = UnattachedPremium+".00";
										}
										BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVUNATTACHEDPREM " + fieldType +"=\""+ fieldTypeText +"\">"+ (UnattachedPremium.ToString()) +"</BOATCOVUNATTACHEDPREM>";
									}
								}
								break;
								// mapping for Agreed Value (AV 100)

							case "EBSCEAV":
								// mapping for dec page
								if (gStrPdfFor == PDFForDecPage)
								{
									//									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVAVEDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVAVEDESC>";
									//									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVAVELIM " + fieldType +"=\""+ fieldTypeText +"\">"+strAVLim+"</BOATCOVAVELIM>";
									//									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVAVEDED " + fieldType +"=\""+ fieldTypeText +"\">"+strAVDed+"</BOATCOVAVEDED>";
									//									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVAVENUM " + fieldType +"=\""+ fieldTypeText +"\">AV-100</BOATCOVAVENUM>";
									//									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVAVEDATE " + fieldType +"=\""+ fieldTypeText +"\">03/01</BOATCOVAVEDATE>";
									//									if(htpremium.Contains("BOAT_AV100"))
									//									{
									//										lstrGetPremium = htpremium["BOAT_AV100"].ToString();
									//										lintGetindex = lstrGetPremium.IndexOf(".");
									//										if(lintGetindex == -1)
									//										{
									//											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVAVEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_AV100"].ToString() + ".00")+"</BOATCOVAVEPREMIUM>";
									//										}
									//										else
									//											BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVAVEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_AV100"].ToString())+"</BOATCOVAVEPREMIUM>";
									//										
									//									}
									//
									//									#region Dec Page Element
									//									if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
									//									{
									//										XmlElement DecPageBoatEndoAV100;
									//										DecPageBoatEndoAV100 = AcordPDFXML.CreateElement("BOATENDORSEMENTAV100");
									//										BoatElementDecPage.AppendChild(DecPageBoatEndoAV100);
									//										DecPageBoatEndoAV100.SetAttribute(fieldType,fieldTypeMultiple);
									//										DecPageBoatEndoAV100.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAV100"));
									//										DecPageBoatEndoAV100.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAV100"));
									//										DecPageBoatEndoAV100.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEAV100EXTN"));
									//										DecPageBoatEndoAV100.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAV100EXTN"));
									//
									//										XmlElement EndoElementAV100;
									//										EndoElementAV100 = AcordPDFXML.CreateElement("EndoElementAV100INFO");
									//										DecPageBoatEndoAV100.AppendChild(EndoElementAV100);
									//										EndoElementAV100.SetAttribute(fieldType,fieldTypeNormal);
									//										EndoElementAV100.SetAttribute(id,BoatCtr.ToString());
									//									}
									//									#endregion
							
									DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
									DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
									if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
										DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
									string limit="0";
									if(CoverageDetails["LIMIT_1"].ToString()=="" || CoverageDetails["LIMIT_1"].ToString()==null || CoverageDetails["LIMIT_1"].ToString()=="0" || CoverageDetails["LIMIT_1"].ToString()=="0.00")
									{
										limit="Included";
									}
									else
									{
										limit=CoverageDetails["LIMIT_1"].ToString();
									}
									DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(limit.Replace(".00",""))+"</BOATCOVLIM>";
									DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDeduct.Replace(".00",""))+"</BOATCOVDED>";
									//								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVPREM>";
									CovCtr++;
									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
									//								if(htpremium.Contains("BOAT_AV100"))
									//								{
									//									lstrGetPremium = htpremium["BOAT_AV100"].ToString();
									//									lintGetindex = lstrGetPremium.IndexOf(".");
									//									if(lintGetindex == -1)
									//									{
									//										DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_AV100"].ToString() + ".00")+"</BOATCOVPREM>";
									//									}
									//									else
									//										DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_AV100"].ToString())+"</BOATCOVPREM>";
									//									
									//								}

								}
									// mapping for accord page
								else if (gStrPdfFor == PDFForAcord)
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVAVEDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVAVEDESC>";
									string limit="0";
									if(CoverageDetails["LIMIT_1"].ToString()=="" || CoverageDetails["LIMIT_1"].ToString()==null || CoverageDetails["LIMIT_1"].ToString()=="0" || CoverageDetails["LIMIT_1"].ToString()=="0.00")
									{
										limit="Included";
									}
									else
									{
										limit=CoverageDetails["LIMIT_1"].ToString();
									}
									if(limit!="Included")
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVAVELIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(limit.Replace(".00",""))+"</BOATCOVAVELIMIT>";
									}
									else
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVAVELIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(limit.Replace(".00","").Replace("$",""))+"</BOATCOVAVELIMIT>";
									}
									if(boat_ded !="" && boat_ded !="0" && boat_ded !="0.00")
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVAVEDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATCOVAVEDEDUCTIBLE>";
									}
									else
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVAVEDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00","").Replace("$",""))+"</BOATCOVAVEDEDUCTIBLE>";
									}
									//BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVAVEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVAVEPREMIUM>";
									//Added by asfa (20-Feb-2008) - iTrack issue #3331
									covPrem="";
									if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
										covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
									else // when gStrtemp != "temp"
										covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
									if(covPrem != "" && covPrem != "0" && covPrem != "0.00" && covPrem != "Included")
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVAVEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">$"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVAVEPREMIUM>";
									}
									else
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVAVEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVAVEPREMIUM>";
									}
									if(htpremium.Contains("BOAT_AV100"))
									{
										lstrGetPremium = htpremium["BOAT_AV100"].ToString();
										lintGetindex = lstrGetPremium.IndexOf(".");
										if(lintGetindex == -1)
										{
											//											BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVAVEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_AV100"].ToString() + ".00")+"</BOATCOVAVEPREMIUM>";
										}
										//										else
										//											BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVAVEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_AV100"].ToString())+"</BOATCOVAVEPREMIUM>";

									}

								}
								
							
								break;

								// OP 300 coverage 
								/////////////////////
							
							case "OP300M":
								// mapping for dec page
								if (gStrPdfFor == PDFForDecPage)
								{
									DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
									DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
									if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
										DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
									string limit="0";
									if(CoverageDetails["LIMIT_1"].ToString()=="" || CoverageDetails["LIMIT_1"].ToString()=="" || CoverageDetails["LIMIT_1"].ToString()==null || CoverageDetails["LIMIT_1"].ToString()=="0" || CoverageDetails["LIMIT_1"].ToString()=="0.00")
									{
										limit="Included";
									}
									else
									{
										limit=CoverageDetails["LIMIT_1"].ToString();
									}
									DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(limit.Replace(".00",""))+"</BOATCOVLIM>";
									DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</BOATCOVDED>";
									//								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVPREM>";
									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
									//								if(htpremium.Count>0)
									//									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">Included</BOATCOVPREM>";
									CovCtr++;
								}
									// mapping for accord page
								else if (gStrPdfFor == PDFForAcord)
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVOP3DESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVOP3DESC>";
									string limit="0";
									if(CoverageDetails["LIMIT_1"].ToString()=="" || CoverageDetails["LIMIT_1"].ToString()==null || CoverageDetails["LIMIT_1"].ToString()=="0" || CoverageDetails["LIMIT_1"].ToString()=="0.00")
									{
										limit="Included";
									}
									else
									{
										limit=CoverageDetails["LIMIT_1"].ToString();
									}
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVOP3LIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(limit.Replace(".00","").Replace("$",""))+"</BOATCOVOP3LIMIT>";
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVOP3DEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</BOATCOVOP3DEDUCTIBLE>";
									//BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWPPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVWPPREMIUM>";
									//Added by asfa (20-Feb-2008) - iTrack issue #3331
									covPrem="";
									if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
										covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
									else // when gStrtemp != "temp"
										covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVOP3PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVOP3PREMIUM>";
								}
								break;
								////////////////////////////
						
								//						case "OP720":
								//							
								//							if (gStrPdfFor == PDFForDecPage)
								//							{
								//								
								//								#region Dec Page Element
								//								if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
								//								{
								//
								//									XmlElement DecPageBoatEndo;
								//									DecPageBoatEndo = AcordPDFXML.CreateElement("BOATENDORSEMENT");
								//									BoatElementDecPage.AppendChild(DecPageBoatEndo);
								//									DecPageBoatEndo.SetAttribute(fieldType,fieldTypeMultiple);
								//									DecPageBoatEndo.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOP720"));
								//									DecPageBoatEndo.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP720"));
								//									DecPageBoatEndo.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEOP720EXTN"));
								//									DecPageBoatEndo.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP720EXTN"));
								//
								//									XmlElement EndoElement;
								//									EndoElement = AcordPDFXML.CreateElement("EndoElement");
								//									DecPageBoatEndo.AppendChild(EndoElement);
								//									EndoElement.SetAttribute(fieldType,fieldTypeNormal);
								//									EndoElement.SetAttribute(id,BoatCtr.ToString());
								//								}
								//								#endregion
								//								DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
								//								DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
								//								if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
								//									DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
								//								DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
								//								DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString().Replace(".00",""))+"</BOATCOVDED>";
								//								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
								//								CovCtr++;
								//							}	
								//							break;	
								// mapping for Client Entertainment Liability (OP 720)
							case "EBSMECE":
								if (gStrPdfFor == PDFForDecPage)
								{
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVCLIENTENTERTAINDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVCLIENTENTERTAINDESC>";
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVCLIENTENTERTAINLIM " + fieldType +"=\""+ fieldTypeText +"\">Included</BOATCOVCLIENTENTERTAINLIM>";
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVCLIENTENTERTAINDED " + fieldType +"=\""+ fieldTypeText +"\">N/A</BOATCOVCLIENTENTERTAINDED>";
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVCLIENTENTERTAINNUM " + fieldType +"=\""+ fieldTypeText +"\">OP-720</BOATCOVCLIENTENTERTAINNUM>";
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVCLIENTENTERTAINDATE " + fieldType +"=\""+ fieldTypeText +"\">05/97</BOATCOVCLIENTENTERTAINDATE>";
									//
									//								if(htpremium.Contains("BOAT_OP720"))
									//								{
									//									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVCLIENTENTERTAINPREM " + fieldType +"=\""+ fieldTypeText +"\">15.00</BOATCOVCLIENTENTERTAINPREM>";
									//								}								
									DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
									DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
									if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
										DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
									string limit="0";
									if(CoverageDetails["LIMIT_1"].ToString()=="" || CoverageDetails["LIMIT_1"].ToString()==null || CoverageDetails["LIMIT_1"].ToString()=="0" || CoverageDetails["LIMIT_1"].ToString()=="0.00")
									{
										limit="Included";
									}
									else
									{
										limit=CoverageDetails["LIMIT_1"].ToString();
									}
									DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(limit.Replace(".00",""))+"</BOATCOVLIM>";
															
									DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+"Included"+"</BOATCOVDED>";
									//								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVPREM>";
									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
									//								if(htpremium.Contains("BOAT_OP720"))
									//								{
									//									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">15.00</BOATCOVPREM>";
									//								}									
									CovCtr++;
								}
								else if (gStrPdfFor == PDFForAcord)
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVCEDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVCEDESC>";
									string limit="0";
									if(CoverageDetails["LIMIT_1"].ToString()=="" || CoverageDetails["LIMIT_1"].ToString()==null || CoverageDetails["LIMIT_1"].ToString()=="0" || CoverageDetails["LIMIT_1"].ToString()=="0.00")
									{
										limit="Included";
									}
									else
									{
										limit="$" + CoverageDetails["LIMIT_1"].ToString();
									}
								
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVCELIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(limit.Replace(".00",""))+"</BOATCOVCELIMIT>";
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVCEDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</BOATCOVCEDEDUCTIBLE>";
									//BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVCEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVCEPREMIUM>";
									//Added by asfa (20-Feb-2008) - iTrack issue #3331
									covPrem="";
									if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
										covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
									else // when gStrtemp != "temp"
										covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
									if(covPrem !="" && covPrem !="0" && covPrem !="0.00" && covPrem != "Included")
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVCEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">$"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVCEPREMIUM>";
									}
									else
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVCEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVCEPREMIUM>";
									}
									if(htpremium.Contains("BOAT_OP720"))
									{
										lstrGetPremium = htpremium["BOAT_OP720"].ToString();
										lintGetindex = lstrGetPremium.IndexOf(".");
										if(lintGetindex == -1)
										{
											//										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVCEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_OP720"].ToString() + ".00")+"</BOATCOVCEPREMIUM>";
										}
										//									else
										//										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVCEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_OP720"].ToString())+"</BOATCOVCEPREMIUM>";

										if(htpremium["BOAT_OP720"].ToString()!=null)
										{
											dblSumUnattach+=int.Parse(htpremium["BOAT_OP720"].ToString());
										}										
									}
								}
								break;
							case "WAT400":
								if (gStrPdfFor == PDFForDecPage)
								{
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALWCRAFTDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVPERSONALWCRAFTDESC>";
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALWCRAFTLIM " + fieldType +"=\""+ fieldTypeText +"\">Included</BOATCOVPERSONALWCRAFTLIM>";
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALWCRAFTDED " + fieldType +"=\""+ fieldTypeText +"\">N/A</BOATCOVPERSONALWCRAFTDED>";
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALWCRAFTNUM " + fieldType +"=\""+ fieldTypeText +"\">OP-400</BOATCOVPERSONALWCRAFTNUM>";							
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALWCRAFTDATE " + fieldType +"=\""+ fieldTypeText +"\">05/97</BOATCOVPERSONALWCRAFTDATE>";
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVPERSONALWCRAFTPREM " + fieldType +"=\""+ fieldTypeText +"\">Included</BOATCOVPERSONALWCRAFTPREM>";
									//								
									//								
									//								#region Dec Page Element
									//								if(CoverageDetails["ENDORS_PRINT"].ToString()!="N")	
									//								{
									//									XmlElement DecPageBoatEndoOP400;
									//									DecPageBoatEndoOP400 = AcordPDFXML.CreateElement("BOATENDORSEMENTOP400");
									//									BoatElementDecPage.AppendChild(DecPageBoatEndoOP400);
									//									DecPageBoatEndoOP400.SetAttribute(fieldType,fieldTypeMultiple);
									//									DecPageBoatEndoOP400.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOP400"));
									//									DecPageBoatEndoOP400.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP400"));
									//									DecPageBoatEndoOP400.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEOP400EXTN"));
									//									DecPageBoatEndoOP400.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP400EXTN"));
									//
									//									XmlElement EndoElementOP400;
									//									EndoElementOP400 = AcordPDFXML.CreateElement("EndoElementOP400INFO");
									//									DecPageBoatEndoOP400.AppendChild(EndoElementOP400);
									//									EndoElementOP400.SetAttribute(fieldType,fieldTypeNormal);
									//									EndoElementOP400.SetAttribute(id,BoatCtr.ToString() );
									//								}
									//								#endregion

									DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
									DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
									if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
										DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
									DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</BOATCOVLIM>";
									DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</BOATCOVDED>";
									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
									CovCtr++;
								}
								else if (gStrPdfFor == PDFForAcord)
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVPWEDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVPWEDESC>";
									if(CoverageDetails["LIMIT_1"].ToString()!="" && CoverageDetails["LIMIT_1"].ToString() !="0" && CoverageDetails["LIMIT_1"].ToString() !="0.00" && CoverageDetails["LIMIT_1"].ToString() !="Included")
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVPWELIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVPWELIMIT>";
									}
									else
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVPWELIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</BOATCOVPWELIMIT>";
									}
									if(boat_ded !="" && boat_ded !="0" && boat_ded != "0.00")
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVPWEDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATCOVPWEDEDUCTIBLE>";
									}
									else
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVPWEDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</BOATCOVPWEDEDUCTIBLE>";
									}
									//BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVPWEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVPWEPREMIUM>";
									//Added by asfa (20-Feb-2008) - iTrack issue #3331
									covPrem="";
									if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
										covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
									else // when gStrtemp != "temp"
										covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
									if(covPrem !="0" && covPrem !="0.00" && covPrem !="Included" && covPrem !="")
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVPWEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">$"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPWEPREMIUM>";
									}
									else
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVPWEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</BOATCOVPWEPREMIUM>";
									}
									//To be Implemented as ITableMapping is only Endorsement
									if(htpremium.Contains("BOAT_OP400"))
									{
										//									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVPWEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_OP400"].ToString() + ".00")+"</BOATCOVPWEPREMIUM>";
									}
									//								else 
									//									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVPWEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">Included</BOATCOVPWEPREMIUM>";
								}
								break;
								//						case "OP900":
								//
								//							if (gStrPdfFor == PDFForDecPage)
								//							{
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVLIABPOLTNUM " + fieldType +"=\""+ fieldTypeText +"\">OP-900</BOATCOVLIABPOLTNUM>";							
								//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVLIABPOLTDATE " + fieldType +"=\""+ fieldTypeText +"\">03/06</BOATCOVLIABPOLTDATE>";
								//								#region Dec Page Element
								//								if(CoverageDetails["ENDORS_PRINT"].ToString()!="N")	
								//								{
								//
								//									XmlElement DecPageBoatEndoOP900;
								//									DecPageBoatEndoOP900 = AcordPDFXML.CreateElement("BOATENDORSEMENTOP900");
								//									BoatElementDecPage.AppendChild(DecPageBoatEndoOP900);
								//									DecPageBoatEndoOP900.SetAttribute(fieldType,fieldTypeMultiple);
								//									DecPageBoatEndoOP900.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOP900"));
								//									DecPageBoatEndoOP900.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP900"));
								//									DecPageBoatEndoOP900.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEOP900EXTN"));
								//									DecPageBoatEndoOP900.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP900EXTN"));
								//
								//									XmlElement EndoElementOP900;
								//									EndoElementOP900 = AcordPDFXML.CreateElement("BOATENDORSEMENTOP900INFO");
								//									DecPageBoatEndoOP900.AppendChild(EndoElementOP900);
								//									EndoElementOP900.SetAttribute(fieldType,fieldTypeNormal);
								//
								//									EndoElementOP900.SetAttribute(id,BoatCtr.ToString());
								//								}
								//								#endregion
								//								DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
								//								DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
								//								if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
								//									DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
								//								DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
								//								DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+"N/A"+"</BOATCOVDED>";
								//								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
								//								CovCtr++;
								//							}
								//							break;
							case "EBSMWL":
								if (gStrPdfFor == PDFForDecPage)
								{
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVLIABPOLTDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVLIABPOLTDESC>";
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVLIABPOLTLIM " + fieldType +"=\""+ fieldTypeText +"\">50000.00</BOATCOVLIABPOLTLIM>";
									//								BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVLIABPOLTDED " + fieldType +"=\""+ fieldTypeText +"\">N/A</BOATCOVLIABPOLTDED>";
									//								if(htpremium.Contains("BOAT_OP900"))
									//								{
									//									BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVLIABPOLTPREM " + fieldType +"=\""+ fieldTypeText +"\">10.00</BOATCOVLIABPOLTPREM>";
									//								}
									DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
									DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
									if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
										DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
									DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
									DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+"N/A"+"</BOATCOVDED>";
									//								DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVPREM>";
									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
									//								if(htpremium.Contains("BOAT_OP900"))
									//								{
									//									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">10.00</BOATCOVPREM>";
									//								}
									CovCtr++;
								}
								else if (gStrPdfFor == PDFForAcord)
								{
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWLPCDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVWLPCDESC>";
									if(CoverageDetails["LIMIT_1"].ToString() != "" || CoverageDetails["LIMIT_1"].ToString() != "0" || CoverageDetails["LIMIT_1"].ToString() != "0.00" || CoverageDetails["LIMIT_1"].ToString()!="Included")
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWLPCLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVWLPCLIMIT>";
									}
									else
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWLPCLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00","").Replace("$",""))+"</BOATCOVWLPCLIMIT>";
									}
									BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWLPCDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</BOATCOVWLPCDEDUCTIBLE>";
									//BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWLPCPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString()))+"</BOATCOVWLPCPREMIUM>";
									//Added by asfa (20-Feb-2008) - iTrack issue #3331
									covPrem="";
									if(gStrtemp!="final" && gStrCalledFrom.Equals(CalledFromPolicy))
										covPrem= GetPremiumBeforeCommit(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString(),htpremium );
									else // when gStrtemp != "temp"
										covPrem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
									if(covPrem != "" && covPrem !="Included" && covPrem !="0" && covPrem != "0.00")
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWLPCPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">$"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVWLPCPREMIUM>";
									}
									else
									{
										BoatElementSuppliment.InnerXml= BoatElementSuppliment.InnerXml + "<BOATCOVWLPCPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVWLPCPREMIUM>";
									}
									if(htpremium.Contains("BOAT_OP900"))
									{
										lstrGetPremium = htpremium["BOAT_OP900"].ToString();
										lintGetindex = lstrGetPremium.IndexOf(".");
										if(lintGetindex == -1)
										{
											//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVWLPCPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_OP900"].ToString() + ".00")+"</BOATCOVWLPCPREMIUM>";
										}
										//									else
										//										BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<BOATCOVWLPCPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_OP900"].ToString())+"</BOATCOVWLPCPREMIUM>";

										dblSumUnattach+=int.Parse(htpremium["BOAT_OP900"].ToString());
									}
								}
								break;
							case "BDEDUC":
								if (gStrPdfFor == PDFForDecPage)
								{
									//								if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"].ToString()!="" )
									//									strAVLim=CoverageDetails["LIMIT_1"].ToString(); 
									//								if(CoverageDetails["DEDUCTIBLE_1"]!=null && CoverageDetails["DEDUCTIBLE_1"].ToString()!="" )
									//									strAVDed=CoverageDetails["DEDUCTIBLE_1"].ToString(); 
									// Assign Value of deductible to variable
								
									DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
									DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
									if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
										DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
									DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
									DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATCOVDED>";
									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
									CovCtr++;
								}
								break;
							case "EBPPDJ":
								if (gStrPdfFor == PDFForDecPage)
								{
									//								if(CoverageDetails["LIMIT_1"]!=null && CoverageDetails["LIMIT_1"].ToString()!="" )
									//									strAVLim=CoverageDetails["LIMIT_1"].ToString(); 
									//								if(CoverageDetails["DEDUCTIBLE_1"]!=null && CoverageDetails["DEDUCTIBLE_1"].ToString()!="" )
									//									strAVDed=CoverageDetails["DEDUCTIBLE_1"].ToString(); 
									DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
									DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
									if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
										DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
									DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
									DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATCOVDED>";
									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
									CovCtr++;
								}
								break;
							default:
								if(gStrPdfFor == PDFForDecPage)
								{
									DecpageCovElement.InnerXml+= "<BOATCOVDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</BOATCOVDESC>";
									DecpageCovElement.InnerXml+= "<BOATCOVNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString())+"</BOATCOVNUM>";
									if(CoverageDetails["EDITION_DATE"] != System.DBNull.Value)
										DecpageCovElement.InnerXml+= "<BOATCOVDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</BOATCOVDATE>";
									DecpageCovElement.InnerXml+= "<BOATCOVLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace(".00",""))+"</BOATCOVLIM>";
									DecpageCovElement.InnerXml+= "<BOATCOVDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded.Replace(".00",""))+"</BOATCOVDED>";
									DecpageCovElement.InnerXml+= "<BOATCOVPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(covPrem)+"</BOATCOVPREM>";
									CovCtr++;

								}

								break;
						}
						//					if(gStrPdfFor == PDFForDecPage)
						//					{
						//						int lowestIndex = GetLowestPrnIndex(ref prnOrd, DSTempEngineTrailer.Tables[0].Rows.Count);
						//						string prncovCode = prnOrd_covCode[lowestIndex];
						//						string prnAttFile = prnOrd_attFile[lowestIndex];
						//						switch(prncovCode)
						//						{
						//							case "WP100": 								
						//								#region Dec Page Element
						//								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
						//								{
						//
						//									XmlElement DecPageBoatEndoWP100;
						//									DecPageBoatEndoWP100 = AcordPDFXML.CreateElement("BOATENDORSEMENTWP100");
						//									DecpageCovElement.AppendChild(DecPageBoatEndoWP100);
						//									DecPageBoatEndoWP100.SetAttribute(fieldType,fieldTypeMultiple);
						//									if(prnAttFile != null && prnAttFile.ToString() != "")
						//									{
						//										DecPageBoatEndoWP100.SetAttribute(PrimPDF,prnAttFile.ToString());
						//										DecPageBoatEndoWP100.SetAttribute(PrimPDFBlocks,"1");
						//									}
						//									else
						//									{
						//										DecPageBoatEndoWP100.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEWP100"));
						//										DecPageBoatEndoWP100.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEWP100"));
						//									}
						//									DecPageBoatEndoWP100.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEWP100EXTN"));
						//									DecPageBoatEndoWP100.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEWP100EXTN"));
						//
						//									XmlElement EndoElementWP100;
						//									EndoElementWP100 = AcordPDFXML.CreateElement("EndoElementWP100INFO");
						//									DecPageBoatEndoWP100.AppendChild(EndoElementWP100);
						//									EndoElementWP100.SetAttribute(fieldType,fieldTypeNormal);
						//									EndoElementWP100.SetAttribute(id,BoatCtr.ToString());
						//								}
						//								#endregion		
						//								break;
						//							case "EBSCEAV": 
						//								#region Dec Page Element
						//								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
						//								{
						//									XmlElement DecPageBoatEndoAV100;
						//									DecPageBoatEndoAV100 = AcordPDFXML.CreateElement("BOATENDORSEMENTAV100");
						//									DecpageCovElement.AppendChild(DecPageBoatEndoAV100);
						//									DecPageBoatEndoAV100.SetAttribute(fieldType,fieldTypeMultiple);
						//									if(prnAttFile != null && prnAttFile.ToString() != "")
						//									{
						//										DecPageBoatEndoAV100.SetAttribute(PrimPDF,prnAttFile.ToString());
						//										DecPageBoatEndoAV100.SetAttribute(PrimPDFBlocks,"1");
						//									}
						//									else
						//									{
						//										DecPageBoatEndoAV100.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAV100"));
						//										DecPageBoatEndoAV100.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAV100"));
						//									}
						//									DecPageBoatEndoAV100.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEAV100EXTN"));
						//									DecPageBoatEndoAV100.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAV100EXTN"));
						//
						//									XmlElement EndoElementAV100;
						//									EndoElementAV100 = AcordPDFXML.CreateElement("EndoElementAV100INFO");
						//									DecPageBoatEndoAV100.AppendChild(EndoElementAV100);
						//									EndoElementAV100.SetAttribute(fieldType,fieldTypeNormal);
						//									EndoElementAV100.SetAttribute(id,BoatCtr.ToString());
						//								}
						//								#endregion				
						//								break;
						//							case "OP720": 
						//								#region Dec Page Element
						//								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
						//								{
						//
						//									XmlElement DecPageBoatEndo;
						//									DecPageBoatEndo = AcordPDFXML.CreateElement("BOATENDORSEMENT");
						//									DecpageCovElement.AppendChild(DecPageBoatEndo);
						//									DecPageBoatEndo.SetAttribute(fieldType,fieldTypeMultiple);
						//									if(prnAttFile != null && prnAttFile.ToString() != "")
						//									{
						//										DecPageBoatEndo.SetAttribute(PrimPDF,prnAttFile.ToString());
						//										DecPageBoatEndo.SetAttribute(PrimPDFBlocks,"1");
						//									}
						//									else
						//									{
						//										DecPageBoatEndo.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOP720"));
						//										DecPageBoatEndo.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP720"));
						//									}
						//									DecPageBoatEndo.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEOP720EXTN"));
						//									DecPageBoatEndo.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP720EXTN"));
						//
						//									XmlElement EndoElement;
						//									EndoElement = AcordPDFXML.CreateElement("EndoElement");
						//									DecPageBoatEndo.AppendChild(EndoElement);
						//									EndoElement.SetAttribute(fieldType,fieldTypeNormal);
						//									EndoElement.SetAttribute(id,BoatCtr.ToString());
						//								}
						//								#endregion
						//								break;
						//							case "WAT400": 
						//								#region Dec Page Element
						//								//if(CoverageDetails["ENDORS_PRINT"].ToString()!="N")	
						//								{
						//									XmlElement DecPageBoatEndoOP400;
						//									DecPageBoatEndoOP400 = AcordPDFXML.CreateElement("BOATENDORSEMENTOP400");
						//									DecpageCovElement.AppendChild(DecPageBoatEndoOP400);
						//									DecPageBoatEndoOP400.SetAttribute(fieldType,fieldTypeMultiple);
						//									if(prnAttFile != null && prnAttFile.ToString() != "")
						//									{
						//										DecPageBoatEndoOP400.SetAttribute(PrimPDF,prnAttFile.ToString());
						//										DecPageBoatEndoOP400.SetAttribute(PrimPDFBlocks,"1");
						//									}
						//									else
						//									{
						//										DecPageBoatEndoOP400.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOP400"));
						//										DecPageBoatEndoOP400.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP400"));
						//									}
						//									DecPageBoatEndoOP400.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEOP400EXTN"));
						//									DecPageBoatEndoOP400.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP400EXTN"));
						//
						//									XmlElement EndoElementOP400;
						//									EndoElementOP400 = AcordPDFXML.CreateElement("EndoElementOP400INFO");
						//									DecPageBoatEndoOP400.AppendChild(EndoElementOP400);
						//									EndoElementOP400.SetAttribute(fieldType,fieldTypeNormal);
						//									EndoElementOP400.SetAttribute(id,BoatCtr.ToString() );
						//								}
						//								#endregion
						//								break;
						//							case "OP900":
						//								#region Dec Page Element
						//								//if(CoverageDetails["ENDORS_PRINT"].ToString()!="N")	
						//								{
						//
						//									XmlElement DecPageBoatEndoOP900;
						//									DecPageBoatEndoOP900 = AcordPDFXML.CreateElement("BOATENDORSEMENTOP900");
						//									DecpageCovElement.AppendChild(DecPageBoatEndoOP900);
						//									DecPageBoatEndoOP900.SetAttribute(fieldType,fieldTypeMultiple);
						//									if(prnAttFile != null && prnAttFile.ToString() != "")
						//									{
						//										DecPageBoatEndoOP900.SetAttribute(PrimPDF,prnAttFile.ToString());
						//										DecPageBoatEndoOP900.SetAttribute(PrimPDFBlocks,"1");
						//									}
						//									else
						//									{
						//										DecPageBoatEndoOP900.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOP900"));
						//										DecPageBoatEndoOP900.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP900"));
						//									}
						//									DecPageBoatEndoOP900.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEOP900EXTN"));
						//									DecPageBoatEndoOP900.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOP900EXTN"));
						//
						//									XmlElement EndoElementOP900;
						//									EndoElementOP900 = AcordPDFXML.CreateElement("BOATENDORSEMENTOP900INFO");
						//									DecPageBoatEndoOP900.AppendChild(EndoElementOP900);
						//									EndoElementOP900.SetAttribute(fieldType,fieldTypeNormal);
						//
						//									EndoElementOP900.SetAttribute(id,BoatCtr.ToString());
						//								}
						//								#endregion
						//								break;
						//							default: break;
						//						}
						//					}
						#endregion

					
					}

					if(DSTempEngineTrailer.Tables[0].Rows.Count>0) 
					{
						if (gStrPdfFor == PDFForAcord)
						{
							//						if(BoatCtr == 0)
							//							sumTtlE += SchPrem;
							if(sumTtlE !=0)
							{
								BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVENDMTPREM " + fieldType +"=\""+ fieldTypeText +"\">"+(sumTtlE + ".00")+"</BOATCOVENDMTPREM>";
							}
							else
							{
								BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVENDMTPREM " + fieldType +"=\""+ fieldTypeText +"\">"+""+"</BOATCOVENDMTPREM>";
							}
							dblSumTotal+=dblSumUnattach;
						}
					}


					if (gStrPdfFor == PDFForAcord)
					{
						if(BoatCtr == 0)
						{
							sumTtlB += SchPrem;
							sumTtlB += unattach_prem;
						}
						if(gStrtemp!="final")
						{
							sumTtlB += Accordtrailertotalprem;
						}
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVTTOTAL " + fieldType +"=\""+ fieldTypeText +"\">"+(sumTtlB.ToString()  + ".00" )+"</BOATCOVTTOTAL>";
						//					if(htpremium.Contains("SUMTOTAL"))
						//					{
						//						if (gStrPdfFor == PDFForDecPage)
						//						{
						//						
						////							BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml + "<TOTALPOLPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+(dblSumTotal.ToString()  + ".00" )+"</TOTALPOLPREMIUM>";							
						//						}
						//						else if (gStrPdfFor == PDFForAcord)
						//						{
						//							BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVTTOTAL " + fieldType +"=\""+ fieldTypeText +"\">"+(dblSumTotal.ToString()  + ".00" )+"</BOATCOVTTOTAL>";
						//						}
						//					}
					
					}
					dblSumTotal=0;
					dblSumUnattach=0;
					#endregion

					if (gStrPdfFor == PDFForDecPage)
					{
						#region operator Dec Page

						#region setting Root Operate Attribute
						XmlElement OperaterRootElement;
						OperaterRootElement = AcordPDFXML.CreateElement("OPERATOR");
						BoatElementDecPage.AppendChild(OperaterRootElement);
						OperaterRootElement.SetAttribute(fieldType,fieldTypeMultiple);
						OperaterRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOPERATOR"));
						OperaterRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOPERATOR"));
						OperaterRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEOPERATOREXTN"));
						OperaterRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOPERATOREXTN"));

					
						#endregion
					
						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						gobjWrapper.AddParameter("@VEHICLEID",BoatDetail["BOAT_ID"]);
						gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						DSTempEngineTrailer = gobjWrapper.ExecuteDataSet("Proc_GetPDFOperatorDtls");
						gobjWrapper.ClearParameteres();

						//DSTempEngineTrailer = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFOperatorDtls " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "',"  +BoatDetail["BOAT_ID"] );
						foreach(DataRow OperatorDetail in DSTempEngineTrailer.Tables[0].Rows)
						{
							XmlElement OperatorElement;
							OperatorElement = AcordPDFXML.CreateElement("OPERATORINFO");
							OperaterRootElement.AppendChild(OperatorElement);
							OperatorElement.SetAttribute(fieldType,fieldTypeNormal);
							OperatorElement.SetAttribute(id,Eng_tra_Ctr.ToString());

							OperatorElement.InnerXml= OperatorElement.InnerXml +  "<OPERATORNAME " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OperatorDetail["DRIVER_NAME"].ToString())+"</OPERATORNAME>"; 
							OperatorElement.InnerXml= OperatorElement.InnerXml +  "<OPERATORLICENSENUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OperatorDetail["DRIVER_DRIV_LIC"].ToString())+"</OPERATORLICENSENUM>"; 
							OperatorElement.InnerXml= OperatorElement.InnerXml +  "<OPERATORDATEOBIRTH " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OperatorDetail["DRIVER_DOB"].ToString())+"</OPERATORDATEOBIRTH>"; 
							OperatorElement.InnerXml= OperatorElement.InnerXml +  "<OPERATOR_RATED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(OperatorDetail["ASSIGNED_ASS"].ToString())+"</OPERATOR_RATED>"; 

							Eng_tra_Ctr++;
						}
						Eng_tra_Ctr=0;
						#endregion
					}

					#region Additional Int
					XmlElement AddlIntDecRootElement;
					AddlIntDecRootElement = AcordPDFXML.CreateElement("ADDITIONALINT");
					if (gStrPdfFor == PDFForDecPage)
					{
						#region setting AddlInt DecRootElement Attribute
						BoatElementDecPage.AppendChild(AddlIntDecRootElement);
						AddlIntDecRootElement.SetAttribute(fieldType,fieldTypeMultiple);
						AddlIntDecRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEADDLINT"));
						AddlIntDecRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEADDLINT"));
						AddlIntDecRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEADDLINTEXTN"));
						AddlIntDecRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEADDLINTEXTN"));
						#endregion
					}

					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempEngineTrailer = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS");
					gobjWrapper.ClearParameteres();

					//DSTempEngineTrailer = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + "," + BoatDetail["BOAT_ID"] + ",'" + gStrCalledFrom + "'");
					foreach(DataRow AddlIntDetails in DSTempEngineTrailer.Tables[0].Rows)
					{
						if (gStrPdfFor == PDFForDecPage)
						{
							#region AddlInt Dec Page
							XmlElement AddlIntDecElement;
							AddlIntDecElement = AcordPDFXML.CreateElement("ADDITIONALINTINFO");
							AddlIntDecRootElement.AppendChild(AddlIntDecElement);
							AddlIntDecElement.SetAttribute(fieldType,fieldTypeNormal);
							AddlIntDecElement.SetAttribute(id,Eng_tra_Ctr.ToString());

							//Added Mohit Agarwal 29-Aug ITrack 2440
							//						AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLSERIAL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["HULL_ID_NO"].ToString())+"</ADDLSERIAL>"; 
							//						AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLDESC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_MAKE_MODEL"].ToString())+"</ADDLDESC>"; 
							//						AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLLENGTH " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_LENGTH"].ToString())+"</ADDLLENGTH>"; 
							//						AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLHP " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["HORSE_POWER"].ToString())+"</ADDLHP>"; 
							//						AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLCONS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["HULL_MATERIAL"].ToString())+"</ADDLCONS>"; 
							//						AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLUSE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["WATERS_NAVIGATED_USE"].ToString())+"</ADDLUSE>"; 
							//						AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLTERR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_TERRITORY"].ToString())+"</ADDLTERR>"; 

							AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLINTNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["BOAT_NO"].ToString())+"</ADDLINTNUM>"; 
							AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLINTNAME " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["HOLDER_NAME"].ToString() + ", " + AddlIntDetails["ADDRESS"].ToString() +""+ AddlIntDetails["CITYSTATEZIP"].ToString())+"</ADDLINTNAME>"; 
							//AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLINTADDRESS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["ADDRESS"].ToString())+"</ADDLINTADDRESS>"; 
							AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLINTNATUREINTEREST " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["ADDLINTNAME"].ToString())+"</ADDLINTNATUREINTEREST>"; 
							if(AddlIntDetails["LOAN_REF_NUMBER"].ToString()!="")
							{
								AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLINTLOANNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_NO"].ToString() + "/" + AddlIntDetails["LOAN_REF_NUMBER"].ToString())+"</ADDLINTLOANNUM>"; 
							}
							else
							{
								AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLINTLOANNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["BOAT_NO"].ToString())+"</ADDLINTLOANNUM>"; 
							}
							if(AddlIntDetails["BOAT_TRAILER"].ToString() != "2")
								AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLINTTYPE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOATTYPE"].ToString())+"</ADDLINTTYPE>"; 
							else
								AddlIntDecElement.InnerXml = AddlIntDecElement.InnerXml +"<ADDLINTTYPE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Trailer")+"</ADDLINTTYPE>"; 
							
							Eng_tra_Ctr++;
							#endregion
						}
						else if (gStrPdfFor == PDFForAcord)
						{
							#region AddlInt Supplement Page
							if(AddlIntDetails["BOAT_TRAILER"].ToString() == "2")
							{
								BoatElementSuppliment.InnerXml = BoatElementSuppliment.InnerXml +"<ADDLINTNATUREINT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["NATURE_OF_INTEREST"].ToString())+"</ADDLINTNATUREINT>"; 
								BoatElementSuppliment.InnerXml = BoatElementSuppliment.InnerXml +"<ADDINTNAME " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["HOLDER_NAME"].ToString()) + ", " + RemoveJunkXmlCharacters(AddlIntDetails["ADDRESS"].ToString()) +"</ADDINTNAME>"; 
								BoatElementSuppliment.InnerXml = BoatElementSuppliment.InnerXml +"<ADDINTADDRESS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["CITYSTATEZIP"].ToString())+"</ADDINTADDRESS>"; 
								BoatElementSuppliment.InnerXml = BoatElementSuppliment.InnerXml +"<ADDINTLOANNUMBER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["LOAN_REF_NUMBER"].ToString())+"</ADDINTLOANNUMBER>"; 
								if(BoatDetail["BOAT_MODEL"].ToString()!="")
								{
									BoatElementSuppliment.InnerXml = BoatElementSuppliment.InnerXml +"<BOATYEARMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_YEAR"].ToString()+"/"+BoatDetail["BOAT_MAKE"].ToString()+"/"+BoatDetail["BOAT_MODEL"].ToString())+"</BOATYEARMAKEMODEL>"; 
								}
								else
								{
									BoatElementSuppliment.InnerXml = BoatElementSuppliment.InnerXml +"<BOATYEARMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_YEAR"].ToString()+"/"+BoatDetail["BOAT_MAKE"].ToString())+"</BOATYEARMAKEMODEL>"; 
								}
								break;
							}
							#endregion
						}
					}
					Eng_tra_Ctr=0;	

					#endregion Additional Int
					#region Schedule Equip
					createScheduleEquipXml(ref BoatElementDecPage);
					#endregion

					#region Creating Credit And Surcharge Xml
					if (isRateGenerated)
					{
						int CreditSurchRowCounter = 0;
						string AdditionalExtnTxt="";

						#region Credits
						XmlElement DecPageBoatCredit;
						DecPageBoatCredit = AcordPDFXML.CreateElement("BOATCREDIT");

						XmlElement SupplementBoatCredit;
						SupplementBoatCredit = AcordPDFXML.CreateElement("BOATCREDIT");

						if (gStrPdfFor == PDFForDecPage)
						{
							#region Dec Page Element
							BoatElementDecPage.AppendChild(DecPageBoatCredit);
							DecPageBoatCredit.SetAttribute(fieldType,fieldTypeMultiple);
							DecPageBoatCredit.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECREDIT"));
							DecPageBoatCredit.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGECREDIT"));
							DecPageBoatCredit.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGECREDITEXTN"));
							DecPageBoatCredit.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGECREDITEXTN"));
							#endregion
						}
						else if (gStrPdfFor == PDFForAcord)
						{
							#region Supplement Page Element
							BoatElementSuppliment.AppendChild(SupplementBoatCredit);
							SupplementBoatCredit.SetAttribute(fieldType,fieldTypeMultiple);
							SupplementBoatCredit.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTCREDIT"));
							SupplementBoatCredit.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTCREDIT"));
							SupplementBoatCredit.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTCREDITEXTN"));
							SupplementBoatCredit.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTCREDITEXTN"));
							#endregion
						}

						htpremium_dis.Clear(); 
						foreach (XmlNode CreditNode in GetCredits(BoatDetail["BOAT_ID"].ToString()))
						{
							if(!htpremium_dis.Contains(getAttributeValue(CreditNode,"COMPONENT_CODE")))
								htpremium_dis.Add(getAttributeValue(CreditNode,"COMPONENT_CODE"),getAttributeValue(CreditNode,"STEPPREMIUM"));
							else
								continue;

							string component_code = getAttributeValue(CreditNode,"COMPONENT_CODE");
							string creditdesc = getAttributeValue(CreditNode,"STEPDESC");
							if(creditdesc.Replace(" ","").IndexOf("+0%") > 0 || creditdesc.Replace(" ","").IndexOf("-0%") > 0)
								creditdesc = "";

							if(component_code != "D_MULTIPOL" && component_code != "D_DUC" && creditdesc == "")
								continue;

							if(getAttributeValue(CreditNode,"STEPPREMIUM").Trim() == "0")
								continue;

							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page
								XmlElement DecPageBoatCreditElement;
								DecPageBoatCreditElement = AcordPDFXML.CreateElement("BOATCREDITINFO");
								DecPageBoatCredit.AppendChild(DecPageBoatCreditElement);
								DecPageBoatCreditElement.SetAttribute(fieldType,fieldTypeNormal);
								DecPageBoatCreditElement.SetAttribute(id,CreditSurchRowCounter.ToString());

								//							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_MULTIPOL")
								//								DecPageBoatCreditElement.InnerXml += "<CREDITDISC " + fieldType +"=\""+ fieldTypeText +"\">" + BoatDetail["DESC_MULTI_POLICY_DISC_APPLIED"].ToString().Trim()  +"</CREDITDISC>"; 
								//							else if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_DUC") 
								//								DecPageBoatCreditElement.InnerXml += "<CREDITDISC " + fieldType +"=\""+ fieldTypeText +"\">"+ BoatDetail["DWELLING_CONST_DATE"].ToString().Trim()  +"</CREDITDISC>"; 
								//							else
								DecPageBoatCreditElement.InnerXml += "<CREDITDISC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("Discount - ","").Trim()) +"</CREDITDISC>"; 

								//							DecPageBoatCreditElement.InnerXml += "<CREDITDISCPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</CREDITDISCPREM>"; 
								#endregion
							}
							else if (gStrPdfFor == PDFForAcord)
							{
								#region Supplement Page
								XmlElement SupplementBoatCreditElement;
								SupplementBoatCreditElement = AcordPDFXML.CreateElement("BOATCREDITINFO");
								SupplementBoatCredit.AppendChild(SupplementBoatCreditElement);
								SupplementBoatCreditElement.SetAttribute(fieldType,fieldTypeNormal);
								SupplementBoatCreditElement.SetAttribute(id,CreditSurchRowCounter.ToString());
								if (BoatCtr%2 == 0)
									AdditionalExtnTxt="";
								else
									AdditionalExtnTxt="1";

								SupplementBoatCreditElement.InnerXml += "<CREDITDISC" + AdditionalExtnTxt + " " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("Discount - ","").Trim())+"</CREDITDISC" + AdditionalExtnTxt + ">"; 
								SupplementBoatCreditElement.InnerXml += "<CREDITDISCPREM" + AdditionalExtnTxt + " " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</CREDITDISCPREM" + AdditionalExtnTxt + ">"; 
								#endregion
							}

							CreditSurchRowCounter++;
						}

						#region Surcharges
						htpremium_sur.Clear(); 
						foreach (XmlNode CreditNode in GetSurcharges(BoatDetail["BOAT_ID"].ToString()))
						{
							if(!htpremium_sur.Contains(getAttributeValue(CreditNode,"COMPONENT_CODE")))
								htpremium_sur.Add(getAttributeValue(CreditNode,"COMPONENT_CODE"),getAttributeValue(CreditNode,"STEPPREMIUM"));
							else
								continue;

							string surcdesc = getAttributeValue(CreditNode,"STEPDESC");
							if(surcdesc.Replace(" ","").IndexOf("+0%") > 0 || surcdesc.Replace(" ","").IndexOf("-0%") > 0)
								continue;

							if(getAttributeValue(CreditNode,"STEPPREMIUM").Trim() == "0")
								continue;

							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page
								XmlElement DecPageBoatSurchElement;
								DecPageBoatSurchElement = AcordPDFXML.CreateElement("BOATCREDITINFO");
								DecPageBoatCredit.AppendChild(DecPageBoatSurchElement);
								DecPageBoatSurchElement.SetAttribute(fieldType,fieldTypeNormal);
								DecPageBoatSurchElement.SetAttribute(id,CreditSurchRowCounter.ToString());
	
								DecPageBoatSurchElement.InnerXml += "<CREDITDISC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("Charge - ","").Replace(" for Unrelated parties","").Trim())+"</CREDITDISC>";
								//								if(getAttributeValue(CreditNode,"STEPPREMIUM") == "0")
								//									DecPageBoatSurchElement.InnerXml += "<SURCHARGEPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</SURCHARGEPREM>"; 
								//								else
								//									DecPageBoatSurchElement.InnerXml += "<SURCHARGEPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</SURCHARGEPREM>"; 
								#endregion
							}
							CreditSurchRowCounter++;
						}

						#endregion Surcharges

						#endregion

						CreditSurchRowCounter = 0;
						AdditionalExtnTxt="";

						#region Surcharges
						//					XmlElement DecPageBoatSurch;
						//					DecPageBoatSurch = AcordPDFXML.CreateElement("BOATSURCHARGE");

						XmlElement SupplementBoatSurch;
						SupplementBoatSurch = AcordPDFXML.CreateElement("BOATSURCHARGE");

						if (gStrPdfFor == PDFForDecPage)
						{
							//						#region Dec Page Element
							//						BoatElementDecPage.AppendChild(DecPageBoatSurch);
							//						DecPageBoatSurch.SetAttribute(fieldType,fieldTypeMultiple);
							//						DecPageBoatSurch.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESRCHG"));
							//						DecPageBoatSurch.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHG"));
							//						DecPageBoatSurch.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESRCHGEXTN"));
							//						DecPageBoatSurch.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHGEXTN"));
							//						#endregion
						}
						else if (gStrPdfFor == PDFForAcord)
						{
							#region Supplement Page Element
							BoatElementSuppliment.AppendChild(SupplementBoatSurch);
							SupplementBoatSurch.SetAttribute(fieldType,fieldTypeMultiple);
							SupplementBoatSurch.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTSRCHG"));
							SupplementBoatSurch.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTSRCHG"));
							SupplementBoatSurch.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTSRCHGEXTN"));
							SupplementBoatSurch.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTSRCHGEXTN"));
							#endregion
						}

						htpremium_sur.Clear(); 
						foreach (XmlNode CreditNode in GetSurcharges(BoatDetail["BOAT_ID"].ToString()))
						{
							if(!htpremium_sur.Contains(getAttributeValue(CreditNode,"COMPONENT_CODE")))
								htpremium_sur.Add(getAttributeValue(CreditNode,"COMPONENT_CODE"),getAttributeValue(CreditNode,"STEPPREMIUM"));
							else
								continue;

							string surcdesc = getAttributeValue(CreditNode,"STEPDESC");
							if(surcdesc.Replace(" ","").IndexOf("+0%") > 0 || surcdesc.Replace(" ","").IndexOf("-0%") > 0)
								continue;

							if(getAttributeValue(CreditNode,"STEPPREMIUM").Trim() == "0")
								continue;

							if (gStrPdfFor == PDFForDecPage)
							{
								//							#region Dec Page
								//							XmlElement DecPageBoatSurchElement;
								//							DecPageBoatSurchElement = AcordPDFXML.CreateElement("BOATSURCHARGEINFO");
								//							DecPageBoatSurch.AppendChild(DecPageBoatSurchElement);
								//							DecPageBoatSurchElement.SetAttribute(fieldType,fieldTypeNormal);
								//							DecPageBoatSurchElement.SetAttribute(id,CreditSurchRowCounter.ToString());
								//
								//							DecPageBoatSurchElement.InnerXml += "<SURCHARGE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("Charge - ","").Replace(" for Unrelated parties","").Trim())+"</SURCHARGE>";
								//							if(getAttributeValue(CreditNode,"STEPPREMIUM") == "0")
								//								DecPageBoatSurchElement.InnerXml += "<SURCHARGEPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("Included")+"</SURCHARGEPREM>"; 
								//							else
								//								DecPageBoatSurchElement.InnerXml += "<SURCHARGEPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</SURCHARGEPREM>"; 
								//							#endregion
							}
							else if (gStrPdfFor == PDFForAcord)
							{
								#region Supplement Page
								XmlElement SupplementBoatSurchElement;
								SupplementBoatSurchElement = AcordPDFXML.CreateElement("BOATSURCHARGEINFO");
								SupplementBoatSurch.AppendChild(SupplementBoatSurchElement);
								SupplementBoatSurchElement.SetAttribute(fieldType,fieldTypeNormal);
								SupplementBoatSurchElement.SetAttribute(id,CreditSurchRowCounter.ToString());

								if (BoatCtr%2 == 0)
									AdditionalExtnTxt="";
								else
									AdditionalExtnTxt="1";

								SupplementBoatSurchElement.InnerXml += "<SURCHARGE" + AdditionalExtnTxt + " " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("Charge - ","").Replace(" for Unrelated parties","").Trim())+"</SURCHARGE" + AdditionalExtnTxt + ">"; 
								SupplementBoatSurchElement.InnerXml += "<SURCHARGEPREM" + AdditionalExtnTxt + " " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</SURCHARGEPREM" + AdditionalExtnTxt + ">"; 
								#endregion
							}
							CreditSurchRowCounter++;
						}
						#endregion
					}
					#endregion
				
					BoatCtr++;
					Boatpage++;
					//Reset Total Policy premium Variable			
					if(gStrtemp!="final")
					{
						sumTtlC=0;
					}
					#endregion
				}
			}
		}
		/*private void createBoatXML()
		{
			double dblSumTotal=0;
			gobjWrapper.ClearParameteres();
			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DSTempDataSet = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS");
			//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
							
			if (gStrPdfFor == PDFForAcord)
			{
				
				if(DSTempDataSet.Tables[0].Rows.Count>0)
				{
					Acord80RootElement.SelectSingleNode("POLICY/ATTACHBOAT").InnerText = "1";
					
					gstrBoatTerritory = DSTempDataSet.Tables[0].Rows[0]["BOAT_TERRITORY"].ToString();
			
					XmlElement BoatRootElementAcord82;
					DataSet DSTempEngineTrailer;
					int BoatCtr = 0, Eng_tra_Ctr = 0;
			
					BoatRootElementAcord82    = AcordPDFXML.CreateElement("BOAT");
					Acord82RootElement.AppendChild(BoatRootElementAcord82);

					#region setting Boat Root Attribute
					BoatRootElementAcord82.SetAttribute(fieldType,fieldTypeMultiple);
					BoatRootElementAcord82.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82BOAT"));
					BoatRootElementAcord82.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82BOAT"));
					BoatRootElementAcord82.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82BOATEXTN"));
					BoatRootElementAcord82.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82BOATEXTN"));
					#endregion
				
					foreach(DataRow BoatDetail in DSTempDataSet.Tables[0].Rows)
					{
						XmlElement BoatElementAcord82;
						BoatElementAcord82	= AcordPDFXML.CreateElement("BOATINFO");

						htpremium.Clear(); 
						foreach (XmlNode PremiumNode in GetPremium(BoatDetail["BOAT_ID"].ToString()))
						{
							if(!htpremium.Contains(getAttributeValue(PremiumNode,"COMPONENT_CODE")))
							htpremium.Add(getAttributeValue(PremiumNode,"COMPONENT_CODE"),getAttributeValue(PremiumNode,"STEPPREMIUM"));
						}
				
						#region Boat Element for Acord82
						BoatRootElementAcord82.AppendChild(BoatElementAcord82);
						BoatElementAcord82.SetAttribute(fieldType,fieldTypeNormal);
						BoatElementAcord82.SetAttribute(id,BoatCtr.ToString());
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATNUMBER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_NO"].ToString())+"</BOATNUMBER>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATPOWER " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_POWER"].ToString())+"</BOATPOWER>";
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATHULLMATERIAL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(BoatDetail["HULLMATERIAL"].ToString())+"</BOATHULLMATERIAL>";
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<HULLOTHERMATERIAL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["HULLMATERIAL_DESCRIPTION"].ToString())+"</HULLOTHERMATERIAL>";
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_YEAR"].ToString())+"</BOATYEAR>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_MAKE_MODEL"].ToString())+"</BOATMAKEMODEL>"; 
						
						//BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATLENGTH " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_LENGTH"].ToString() +"'" + BoatDetail["BOAT_INCHES"].ToString() +"''") +"</BOATLENGTH>"; 

						if(BoatDetail["BOAT_INCHES"].ToString()=="")
						{
							BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATLENGTH " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_LENGTH"].ToString() +"'") +"</BOATLENGTH>"; 
						}
						else
						{
							BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATLENGTH " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_LENGTH"].ToString() +"'" + BoatDetail["BOAT_INCHES"].ToString() +"''") +"</BOATLENGTH>"; 
						}


						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATSPEED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_SPEED"].ToString())+"</BOATSPEED>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATDTPURCHASED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["DATE_PURCHASED"].ToString())+"</BOATDTPURCHASED>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATPVALUE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["PRESENT_VALUE"].ToString())+"</BOATPVALUE>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATHULLIDNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["HULL_ID_NO"].ToString())+"</BOATHULLIDNUM>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATWATERSNAV  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["WATERS_NAVIGATED_USE"].ToString())+"</BOATWATERSNAV>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATTERRITORY  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_TERRITORY"].ToString())+"</BOATTERRITORY>"; 
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATLOCATION   " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["LOCADD"].ToString()+", " + BoatDetail["LOCCITY"].ToString())+"</BOATLOCATION>"; 
						string strLayUPFrom="";
						string strLayUPTo="";
						if (BoatDetail["LAY_UP_PERIOD_FROM_MONTH"].ToString()!="0" && BoatDetail["LAY_UP_PERIOD_FROM_DAY"].ToString()!="0")
							strLayUPFrom="From "+ MonthName[BoatDetail["LAY_UP_PERIOD_FROM_MONTH"].ToString()].ToString() +" / " + BoatDetail["LAY_UP_PERIOD_FROM_DAY"].ToString() + " ";
						if (BoatDetail["LAY_UP_PERIOD_TO_MONTH"].ToString()!="0" && BoatDetail["LAY_UP_PERIOD_TO_DAY"].ToString()!="0")
							strLayUPTo=" To "+ MonthName[BoatDetail["LAY_UP_PERIOD_TO_MONTH"].ToString()].ToString() +" / " + BoatDetail["LAY_UP_PERIOD_TO_DAY"].ToString();
				
						BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml +  "<BOATLAYUP   " + fieldType +"=\""+ fieldTypeText +"\">" + strLayUPFrom + strLayUPTo + "</BOATLAYUP>"; 
						#endregion
				
						#region Acord82 Engine Details
						gobjWrapper.ClearParameteres();
						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
						gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						DSTempEngineTrailer = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_ENGINE_DETAILS");
						//DSTempEngineTrailer = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_ENGINE_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
						if(DSTempEngineTrailer.Tables[0].Rows.Count>0)
						{
							XmlElement EngineRootElement;
							EngineRootElement	= AcordPDFXML.CreateElement("ENGINE");
							BoatElementAcord82.AppendChild(EngineRootElement);
											
							#region setting Root ENGINE Attribute
							EngineRootElement.SetAttribute(fieldType,fieldTypeMultiple);
							EngineRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82ENGINE"));
							EngineRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82ENGINE"));
							EngineRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82ENGINEEXTN"));
							EngineRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82ENGINEEXTN"));
							#endregion

							foreach(DataRow EngineDetail in DSTempEngineTrailer.Tables[0].Rows)
							{
								XmlElement EngineElement;
								EngineElement = AcordPDFXML.CreateElement("ENGINEINFO");
								EngineRootElement.AppendChild(EngineElement);
								EngineElement.SetAttribute(fieldType,fieldTypeNormal);
								EngineElement.SetAttribute(id,Eng_tra_Ctr.ToString());

								EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINEYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["YEAR"].ToString())+"</ENGINEYEAR>"; 
								EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINEMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["MAKE_MODEL"].ToString())+"</ENGINEMAKEMODEL>"; 
								EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINESRNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["SERIAL_NO"].ToString())+"</ENGINESRNUM>"; 
								EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINEHP " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["HORSEPOWER"].ToString())+"</ENGINEHP>"; 
								EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINEFUELTYPE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(EngineDetail["FUEL_TYPE"].ToString())+"</ENGINEFUELTYPE>"; 
								EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINEPRESENTVALUE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["PRESENT_VALUE"].ToString())+"</ENGINEPRESENTVALUE>"; 
								EngineElement.InnerXml= EngineElement.InnerXml +  "<ENGINEOTHER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(EngineDetail["OTHER"].ToString())+"</ENGINEOTHER>"; 

								Eng_tra_Ctr++;
							}
							Eng_tra_Ctr = 0;
						}
						#endregion
				
						#region Acord82 trailer Details
						gobjWrapper.ClearParameteres();
						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
						gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						DSTempEngineTrailer = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_TRAILER_DETAILS");
						//DSTempEngineTrailer = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_TRAILER_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
						if(DSTempEngineTrailer.Tables[0].Rows.Count>0)
						{
							XmlElement TrailerRootElement;
							TrailerRootElement = AcordPDFXML.CreateElement("TRAILER"); 
							BoatElementAcord82.AppendChild(TrailerRootElement);

							#region setting Root TRAILER Attribute
							TrailerRootElement.SetAttribute(fieldType,fieldTypeMultiple);
							TrailerRootElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82TRAILER"));
							TrailerRootElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82TRAILER"));
							TrailerRootElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82TRAILEREXTN"));
							TrailerRootElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82TRAILEREXTN"));
							#endregion

					
							foreach(DataRow TrailerDetail in DSTempEngineTrailer.Tables[0].Rows)
							{
								XmlElement TrailerElement;
								TrailerElement = AcordPDFXML.CreateElement("TRAILERINFO");
								TrailerRootElement.AppendChild(TrailerElement);
								TrailerElement.SetAttribute(fieldType,fieldTypeNormal);
								TrailerElement.SetAttribute(id,Eng_tra_Ctr.ToString());
					
								TrailerElement.InnerXml= TrailerElement.InnerXml +  "<TRAILERYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(TrailerDetail["YEAR"].ToString())+"</TRAILERYEAR>"; 
								TrailerElement.InnerXml= TrailerElement.InnerXml +  "<TRAILERMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(TrailerDetail["MANUFACTURER"].ToString())+"</TRAILERMAKEMODEL>"; 
								TrailerElement.InnerXml= TrailerElement.InnerXml +  "<TRAILERSRNUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(TrailerDetail["SERIAL_NO"].ToString())+"</TRAILERSRNUM>"; 
								TrailerElement.InnerXml= TrailerElement.InnerXml +  "<TRAILERCOST " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(TrailerDetail["INSURED_VALUE"].ToString())+"</TRAILERCOST>"; 
					
								Eng_tra_Ctr++;
							}
							Eng_tra_Ctr = 0;
						}
						#endregion

						#region COVERAGES
						dblSumTotal=0;
						gobjWrapper.ClearParameteres();
						gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						gobjWrapper.AddParameter("@POLID",gStrPolicyId);
						gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"].ToString());
						gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						DSTempEngineTrailer = gobjWrapper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails"); 
						//DSTempEngineTrailer = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
						foreach(DataRow CoverageDetails in DSTempEngineTrailer.Tables[0].Rows)
						{
							string prem = GetPremium(DSTempEngineTrailer, CoverageDetails["COV_CODE"].ToString());
							prem = prem.Replace("$","");
							if(prem != "" && prem != "Included")
								dblSumTotal+=int.Parse(prem.Replace(".00",""));
							switch(CoverageDetails["COV_CODE"].ToString())
							{
								case "EBPPDACV":
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVHULLLIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVHULLDED>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(prem)+"</BOATCOVHULLPREM>";
									if(htpremium.Contains("BOAT_PD"))
									{
										gstrGetPremium = htpremium["BOAT_PD"].ToString();
										gintGetindex = gstrGetPremium.IndexOf(".");
//										if(gintGetindex == -1)
//											BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_PD"].ToString() + ".00")+"</BOATCOVHULLPREM>";
//										else
//											BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_PD"].ToString())+"</BOATCOVHULLPREM>";
//										if(prem != "" && prem != "Included")
//											//dblSumTotal+=int.Parse(prem);
//										//dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
									}
									break;
								case "EBPPDAV":
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVHULLLIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVHULLDED>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(prem)+"</BOATCOVHULLPREM>";
									if(htpremium.Contains("BOAT_PD"))
									{
										//gstrGetPremium	=	htpremium["BOAT_PD"].ToString();
										gintGetindex	=	gstrGetPremium.IndexOf(".");
//										if(gintGetindex==	-1)
//											BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_PD"].ToString() + ".00")+"</BOATCOVHULLPREM>";
//										else
//											BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVHULLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_PD"].ToString())+"</BOATCOVHULLPREM>";
//										if(prem != "" && prem != "Included")
//											//dblSumTotal+=int.Parse(prem);
//										//dblSumTotal+=int.Parse(htpremium["BOAT_PD"].ToString());
									}
									break;
								case "OUTBOARD1":
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVOM1LIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVOM1LIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVOM1DED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVOM1DED>";
									break;
								case "OUTBOARD2":
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVOM2LIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVOM2LIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVOM2DED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVOM2DED>";
									break;
								case "PORTACCESS":
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVPORTACCLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVPORTACCLIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVPORTACCDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVPORTACCDED>";
									break;
								case "TRAILER":
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVTRAILERLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVTRAILERLIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVTRAILERDED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVTRAILERDED>";

									break;
								case "LCCSL":
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVCSLLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVCSLLIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVCSLDED " + fieldType +"=\""+ fieldTypeText +"\">N/A</BOATCOVCSLDED>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(prem)+"</BOATCOVCSLPREM>";
									if(htpremium.Contains("BOAT_ LIABILITY_PREMIUM"))
									{
										//gstrGetPremium	=	htpremium["BOAT_ LIABILITY_PREMIUM"].ToString();
										gintGetindex	=	gstrGetPremium.IndexOf(".");
//										if(gintGetindex==	-1)
//											BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_ LIABILITY_PREMIUM"].ToString() + ".00")+"</BOATCOVCSLPREM>";
//										else
//											BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_ LIABILITY_PREMIUM"].ToString())+"</BOATCOVCSLPREM>";
//										if(prem != "" && prem != "Included")
//											//dblSumTotal+=int.Parse(prem);
//										//dblSumTotal+=int.Parse(htpremium["BOAT_ LIABILITY_PREMIUM"].ToString());
									}
									break;
								case "MCPAY":
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVMEDPMLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVMEDPMLIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVMEDPMDED " + fieldType +"=\""+ fieldTypeText +"\">N/A</BOATCOVMEDPMDED>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(prem)+"</BOATCOVMEDPMPREM>";
									if(htpremium.Contains("BOAT_MP_PREMIUM"))
									{
										//gstrGetPremium	=	htpremium["BOAT_MP_PREMIUM"].ToString();
										gintGetindex	=	gstrGetPremium.IndexOf(".");
//										if(gintGetindex==	-1)
//											BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_MP_PREMIUM"].ToString() + ".00")+"</BOATCOVMEDPMPREM>";
//										else
//											BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVMEDPMPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_MP_PREMIUM"].ToString())+"</BOATCOVMEDPMPREM>";
//										if(prem != "" && prem != "Included")
//											//dblSumTotal+=int.Parse(prem);
//										//dblSumTotal+=int.Parse(htpremium["BOAT_MP_PREMIUM"].ToString());
									}
									break;
								case "UMBCS":
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVUBCSLLIM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString().Replace("$","").Replace(".00",""))+"</BOATCOVUBCSLLIM>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVUBCSLDED " + fieldType +"=\""+ fieldTypeText +"\">N/A</BOATCOVUBCSLDED>";
									BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVUBCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(prem)+"</BOATCOVUBCSLPREM>";
									if(htpremium.Contains("BOAT_UB_PREMIUM"))
									{
										//gstrGetPremium	=	htpremium["BOAT_UB_PREMIUM"].ToString();
										gintGetindex	=	gstrGetPremium.IndexOf(".");
//										if(gintGetindex==	-1)
//											BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVUBCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UB_PREMIUM"].ToString() + ".00")+"</BOATCOVUBCSLPREM>";
//										else
//											BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVUBCSLPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BOAT_UB_PREMIUM"].ToString())+"</BOATCOVUBCSLPREM>";
//										if(prem != "" && prem != "Included")
//											//dblSumTotal+=int.Parse(prem);
//										//dblSumTotal+=int.Parse(htpremium["BOAT_UB_PREMIUM"].ToString());
									}
									break;
							
							}
					
						}

						if (gStrPdfFor == PDFForAcord)
						{
							if(htpremium.Contains("SUMTOTAL"))
							{
								BoatElementAcord82.InnerXml= BoatElementAcord82.InnerXml + "<BOATCOVTTOTAL " + fieldType +"=\""+ fieldTypeText +"\">"+(dblSumTotal.ToString()  + ".00" )+"</BOATCOVTTOTAL>";
							}
					
						}
						#endregion
				
						BoatCtr++;
					}
				}
				else
					AcordPDFXML.SelectSingleNode(RootElement).RemoveChild(Acord82RootElement);
			}
		}*/

		#endregion

		#region Acord 82 Code for Boat Trailer Addl Interests
		private void createAcord82BoatAddlIntXml()
		{
			if (gStrPdfFor == PDFForAcord)
			{
				XmlElement Acord82AddlInts;
				Acord82AddlInts = AcordPDFXML.CreateElement("ADDITIONALINTERESTS");
				Acord82RootElement.AppendChild(Acord82AddlInts);
				Acord82AddlInts.SetAttribute(fieldType,fieldTypeMultiple);
				Acord82AddlInts.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82ADDLINT"));
				Acord82AddlInts.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82ADDLINT"));
				Acord82AddlInts.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82ADDLINTEXTN"));
				Acord82AddlInts.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82ADDLINTEXTN"));

				int RowCounter = 0;

				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DataSet DSTempDataSet1 = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS");
				gobjWrapper.ClearParameteres();

				//DataSet DSTempDataSet1 = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
				foreach(DataRow BoatDetail in DSTempDataSet1.Tables[0].Rows)
				{
					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempDataSet = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS");
					gobjWrapper.ClearParameteres();

					//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + BoatDetail["BOAT_ID"].ToString() + ",'" + gStrCalledFrom + "'");
			
					#region Acord82 Page
					//					if (DSTempDataSet.Tables[0].Rows.Count >0)
				{
					foreach(DataRow Row in DSTempDataSet.Tables[0].Rows)
					{
						XmlElement Acord82ADDLINTElement;
						Acord82ADDLINTElement = AcordPDFXML.CreateElement("ADDLINTINFO");
						Acord82AddlInts.AppendChild(Acord82ADDLINTElement);
						Acord82ADDLINTElement.SetAttribute(fieldType,fieldTypeNormal);
						Acord82ADDLINTElement.SetAttribute(id,RowCounter.ToString());
						
						string strAddress=Row["ACCORDADDRESS"].ToString();
						
						Acord82ADDLINTElement.InnerXml = Acord82ADDLINTElement.InnerXml +  "<ADDLINTTYPEOFINT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["NATURE_OF_INTEREST"].ToString()) + "</ADDLINTTYPEOFINT>";
						Acord82ADDLINTElement.InnerXml = Acord82ADDLINTElement.InnerXml +  "<ADDLINTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["HOLDER_NAME"].ToString()) + ", " +  RemoveJunkXmlCharacters(strAddress) + "</ADDLINTNAME>";
						Acord82ADDLINTElement.InnerXml = Acord82ADDLINTElement.InnerXml +  "<ADDLINTADD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["CITYSTATEZIP"].ToString()) + "</ADDLINTADD>";
						if(Row["LOAN_REF_NUMBER"].ToString()!="")
						{
							Acord82ADDLINTElement.InnerXml = Acord82ADDLINTElement.InnerXml +  "<ADDLINTLOANNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["BOAT_NO"].ToString() + "/" + Row["LOAN_REF_NUMBER"].ToString()) + "</ADDLINTLOANNUM>";
						}
						else
						{
							Acord82ADDLINTElement.InnerXml = Acord82ADDLINTElement.InnerXml +  "<ADDLINTLOANNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["BOAT_NO"].ToString() )+ "</ADDLINTLOANNUM>";
						}
						if(BoatDetail["BOAT_MODEL"].ToString()!="")
						{
							Acord82ADDLINTElement.InnerXml = Acord82ADDLINTElement.InnerXml +"<BOATYEARMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_YEAR"].ToString()+"/"+BoatDetail["BOAT_MAKE"].ToString()+"/"+BoatDetail["BOAT_MODEL"].ToString())+"</BOATYEARMAKEMODEL>"; 
						}
						else
						{
							Acord82ADDLINTElement.InnerXml = Acord82ADDLINTElement.InnerXml +"<BOATYEARMAKEMODEL " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(BoatDetail["BOAT_YEAR"].ToString()+"/"+BoatDetail["BOAT_MAKE"].ToString())+"</BOATYEARMAKEMODEL>"; 
						}
						RowCounter++;
					}
				}
				}
				#endregion
			}
		}
		/*private void createAcord82BoatAddlIntXml()
		{
			if (gStrPdfFor == PDFForAcord)
			{
				gobjWrapper.ClearParameteres();
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@BOATID",0);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempDataSet = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS");
				//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",0,'" + gStrCalledFrom + "'");
				#region Acord82 Page
				if (DSTempDataSet.Tables[0].Rows.Count >0)
				{
					XmlElement Acord82AddlInts;
					Acord82AddlInts = AcordPDFXML.CreateElement("ADDITIONALINTERESTS");
					Acord82RootElement.AppendChild(Acord82AddlInts);
					Acord82AddlInts.SetAttribute(fieldType,fieldTypeMultiple);
					Acord82AddlInts.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82ADDLINT"));
					Acord82AddlInts.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82ADDLINT"));
					Acord82AddlInts.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82ADDLINTEXTN"));
					Acord82AddlInts.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82ADDLINTEXTN"));

					int RowCounter = 0;
					foreach(DataRow Row in DSTempDataSet.Tables[0].Rows)
					{
						XmlElement Acord82ADDLINTElement;
						Acord82ADDLINTElement = AcordPDFXML.CreateElement("ADDLINTINFO");
						Acord82AddlInts.AppendChild(Acord82ADDLINTElement);
						Acord82ADDLINTElement.SetAttribute(fieldType,fieldTypeNormal);
						Acord82ADDLINTElement.SetAttribute(id,RowCounter.ToString());

						Acord82ADDLINTElement.InnerXml = Acord82ADDLINTElement.InnerXml +  "<ADDLINTTYPEOFINT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["NATURE_OF_INTEREST"].ToString()) + "</ADDLINTTYPEOFINT>";
						Acord82ADDLINTElement.InnerXml = Acord82ADDLINTElement.InnerXml +  "<ADDLINTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["HOLDER_NAME"].ToString()) + "</ADDLINTNAME>";
						Acord82ADDLINTElement.InnerXml = Acord82ADDLINTElement.InnerXml +  "<ADDLINTADD " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["ADDRESS"].ToString()) + "</ADDLINTADD>";
						Acord82ADDLINTElement.InnerXml = Acord82ADDLINTElement.InnerXml +  "<ADDLINTLOANNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["BOAT_NO"].ToString() + "/" + Row["LOAN_REF_NUMBER"].ToString()) + "</ADDLINTLOANNUM>";
					
						RowCounter++;
					}
				}
				#endregion
			}
		}*/
		#endregion

		#region Code for Acord82Operators
		private void createAcord82OperatorXML()
		{
			if(gStrPdfFor == PDFForAcord)
			{

				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetAccordOperatorDtls");
				gobjWrapper.ClearParameteres();

				//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetAccordOperatorDtls " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
				#region Acord82 Page
				if (DSTempDataSet.Tables[0].Rows.Count >0)
				{
					XmlElement Acord82Operators;
					Acord82Operators = AcordPDFXML.CreateElement("OPERATORS");
					Acord82RootElement.AppendChild(Acord82Operators);
					Acord82Operators.SetAttribute(fieldType,fieldTypeMultiple);
					Acord82Operators.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82OPERATOR"));
					Acord82Operators.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82OPERATOR"));
					Acord82Operators.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82OPERATOREXTN"));
					Acord82Operators.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82OPERATOREXTN"));

					int RowCounter = 0,operator_id=1;
					foreach(DataRow Row in DSTempDataSet.Tables[0].Rows)
					{
						XmlElement Acord82OperatorElement;
						Acord82OperatorElement = AcordPDFXML.CreateElement("OPERATORINFO");
						Acord82Operators.AppendChild(Acord82OperatorElement);
						Acord82OperatorElement.SetAttribute(fieldType,fieldTypeNormal);
						Acord82OperatorElement.SetAttribute(id,RowCounter.ToString());
						string strOprSSn="";
						strOprSSn =  Row["DRIVER_SSN"].ToString();
						if(strOprSSn !="" && strOprSSn !="0")
						{
							strOprSSn = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(Row["DRIVER_SSN"].ToString());
							
							if(strOprSSn.Trim() !="")//If-Else condition added by Charles on 21/7/2009 for Itrack 6129
							{
								string strvaln = "xxx-xx-";
								strvaln += strOprSSn.Substring(strvaln.Length, strOprSSn.Length - strvaln.Length);
								strOprSSn = strvaln;
							}
							else 
								strOprSSn="";
						}

						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(operator_id.ToString()) + "</OPERATORNUMBER>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DRIVER_NAME"].ToString()) + "</OPERATORNAME>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORSEX " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DRIVER_SEX"].ToString()) + "</OPERATORSEX>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORDOB " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DRIVER_DOB"].ToString()) + "</OPERATORDOB>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORAUTODRVLIC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DRIVER_DRIV_LIC"].ToString()) + "</OPERATORAUTODRVLIC>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORLICSTATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["STATE_NAME"].ToString()) + "</OPERATORLICSTATE>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORSSN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(strOprSSn) + "</OPERATORSSN>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORMARITALSTATUS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["MARITAL_STATUS"].ToString()) + "</OPERATORMARITALSTATUS>";
						operator_id++;
						RowCounter++;
					}
				}
				#endregion
			}
		}
		/*private void createAcord82OperatorXML()
		{
			if (gStrPdfFor == PDFForAcord)
			{
				gobjWrapper.ClearParameteres();
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFOperatorDtls");
				//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFOperatorDtls " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
				#region Acord82 Page
				if (DSTempDataSet.Tables[0].Rows.Count >0)
				{
					XmlElement Acord82Operators;
					Acord82Operators = AcordPDFXML.CreateElement("OPERATORS");
					Acord82RootElement.AppendChild(Acord82Operators);
					Acord82Operators.SetAttribute(fieldType,fieldTypeMultiple);
					Acord82Operators.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82OPERATOR"));
					Acord82Operators.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82OPERATOR"));
					Acord82Operators.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82OPERATOREXTN"));
					Acord82Operators.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82OPERATOREXTN"));

					int RowCounter = 0;
					foreach(DataRow Row in DSTempDataSet.Tables[0].Rows)
					{
						XmlElement Acord82OperatorElement;
						Acord82OperatorElement = AcordPDFXML.CreateElement("OPERATORINFO");
						Acord82Operators.AppendChild(Acord82OperatorElement);
						Acord82OperatorElement.SetAttribute(fieldType,fieldTypeNormal);
						Acord82OperatorElement.SetAttribute(id,RowCounter.ToString());
						string strOprSSn="";
						strOprSSn =  Row["DRIVER_SSN"].ToString();
						if(strOprSSn !="" && strOprSSn !="0")
						{
							strOprSSn = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(Row["DRIVER_SSN"].ToString());
							
							if(strOprSSn.Trim()!="")//If-Else condition added by Charles on 21/7/2009 for Itrack 6129
							{
								string strvaln = "xxx-xx-";
								strvaln += strOprSSn.Substring(strvaln.Length, strOprSSn.Length - strvaln.Length);
								strOprSSn = strvaln;
							}
							else
								strOprSSn="";

						}

						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["OPERATORNO"].ToString()) + "</OPERATORNUMBER>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORNAME " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DRIVER_NAME"].ToString()) + "</OPERATORNAME>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORSEX " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DRIVER_SEX"].ToString()) + "</OPERATORSEX>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORDOB " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DRIVER_DOB"].ToString()) + "</OPERATORDOB>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORAUTODRVLIC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DRIVER_DRIV_LIC"].ToString()) + "</OPERATORAUTODRVLIC>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORLICSTATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["STATE_NAME"].ToString()) + "</OPERATORLICSTATE>";
						Acord82OperatorElement.InnerXml = Acord82OperatorElement.InnerXml +  "<OPERATORSSN " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["DRIVER_SSN"].ToString()) + "</OPERATORSSN>";

						RowCounter++;
					}
				}
				#endregion
			}
		}*/
		#endregion

		#region Code for Acord82 Operator Experience and Violations
		private void createAcord82OperatorExpViolationXML()
		{
			if (gStrPdfFor == PDFForAcord)
			{
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DataSet DSTempDriver = gobjWrapper.ExecuteDataSet("Proc_GetAccordOperatorDtls");
				gobjWrapper.ClearParameteres();

				//DataSet DSTempDriver = gobjSqlHelper.ExecuteDataSet("Proc_GetAccordOperatorDtls " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
				string gStrdriverId="";
				int OperatorNo=1;
				int RowCounter = 0;
				foreach(DataRow Row in DSTempDriver.Tables[0].Rows)
				{				
					gStrdriverId=Row["OPERATORNO"].ToString();

					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@DRIVERID",gStrdriverId);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFOperatorExpDtls");
					gobjWrapper.ClearParameteres();

					//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFOperatorExpDtls " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "','"+ gStrdriverId +"'");
					#region Acord82 Page
					if (DSTempDataSet.Tables[0].Rows.Count >0 || DSTempDataSet.Tables[1].Rows.Count >0)
					{
						XmlElement Acord82OperatorViolations;
						Acord82OperatorViolations = AcordPDFXML.CreateElement("VIOLATIONS");
						Acord82RootElement.AppendChild(Acord82OperatorViolations);
						Acord82OperatorViolations.SetAttribute(fieldType,fieldTypeMultiple);
					
						Acord82OperatorViolations.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82OPREXP"));
						Acord82OperatorViolations.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82OPREXP"));
					
						Acord82OperatorViolations.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82OPREXPEXTN"));
						Acord82OperatorViolations.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82OPREXPEXTN"));

						
						string tempStr="0";
						//To fill the Operators and MVR details on main Page.
						//foreach(DataRow Row in DSTempDataSet.Tables[0].Rows)
						//{
						if(tempStr!=OperatorNo.ToString())//Row["DRIVER_ID"].ToString())
						{
							XmlElement Acord82ViolationElement;
							Acord82ViolationElement = AcordPDFXML.CreateElement("VIOLATIONINFO");
							Acord82OperatorViolations.AppendChild(Acord82ViolationElement);
							Acord82ViolationElement.SetAttribute(fieldType,fieldTypeNormal);
							Acord82ViolationElement.SetAttribute(id,RowCounter.ToString());
							
							if(DSTempDataSet.Tables[0].Rows.Count >0 &&  DSTempDataSet.Tables[1].Rows.Count > 0)
							{
								Acord82ViolationElement.InnerXml = Acord82ViolationElement.InnerXml +  "<OPERATOREXPNUM " + fieldType + "=\"" + fieldTypeText + "\">" + OperatorNo.ToString() + "</OPERATOREXPNUM>";
								Acord82ViolationElement.InnerXml = Acord82ViolationElement.InnerXml +  "<OPERATOREXPSUMMARY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["EXPDESCRIPTION"].ToString()) +  RemoveJunkXmlCharacters(DSTempDataSet.Tables[1].Rows[0]["VIODESCRIPTION"].ToString()) + "</OPERATOREXPSUMMARY>";
							}
							if(DSTempDataSet.Tables[0].Rows.Count >0 &&  DSTempDataSet.Tables[1].Rows.Count <= 0)
							{
								
								Acord82ViolationElement.InnerXml = Acord82ViolationElement.InnerXml +  "<OPERATOREXPNUM " + fieldType + "=\"" + fieldTypeText + "\">" + OperatorNo.ToString() + "</OPERATOREXPNUM>";
								Acord82ViolationElement.InnerXml = Acord82ViolationElement.InnerXml +  "<OPERATOREXPSUMMARY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["EXPDESCRIPTION"].ToString()) + "</OPERATOREXPSUMMARY>";
								
							}
							if(DSTempDataSet.Tables[0].Rows.Count <= 0 && DSTempDataSet.Tables[1].Rows.Count >0)
							{
								
								Acord82ViolationElement.InnerXml = Acord82ViolationElement.InnerXml +  "<OPERATOREXPNUM " + fieldType + "=\"" + fieldTypeText + "\">" + OperatorNo.ToString() + "</OPERATOREXPNUM>";
								Acord82ViolationElement.InnerXml = Acord82ViolationElement.InnerXml +  "<OPERATOREXPSUMMARY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[1].Rows[1]["VIODESCRIPTION"].ToString()) + "</OPERATOREXPSUMMARY>";
								
							}
							 
							RowCounter++;
						}
						tempStr=gStrdriverId;
							
						//}
					
						tempStr="0";
						//To fill the Operators and MVR details on Add on Operator Experience Page.
						/*foreach(DataRow Row in DSTempDataSet.Tables[0].Rows)
						{*/
						if(tempStr==OperatorNo.ToString())//Row["DRIVER_ID"].ToString())
						{
							XmlElement Acord82ViolationElement1;
							Acord82ViolationElement1 = AcordPDFXML.CreateElement("VIOLATIONINFO");
							Acord82OperatorViolations.AppendChild(Acord82ViolationElement1);
							Acord82ViolationElement1.SetAttribute(fieldType,fieldTypeNormal);
							Acord82ViolationElement1.SetAttribute(id,RowCounter.ToString());
							
							Acord82ViolationElement1.InnerXml = Acord82ViolationElement1.InnerXml +  "<OPERATOREXPNUM " + fieldType + "=\"" + fieldTypeText + "\">" + OperatorNo.ToString() + "</OPERATOREXPNUM>";
							Acord82ViolationElement1.InnerXml = Acord82ViolationElement1.InnerXml +  "<OPERATOREXPSUMMARY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["EXPDESCRIPTION"].ToString()) + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["VIODESCRIPTION"].ToString()) + "</OPERATOREXPSUMMARY>";
							RowCounter++;
						}
						tempStr=gStrdriverId;
							
						//}
					}
					OperatorNo++;
				}
				#endregion
			}
		}
		/*private void createAcord82OperatorExpViolationXML()
		{
			if (gStrPdfFor == PDFForAcord)
			{
				gobjWrapper.ClearParameteres();
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFOperatorExpDtls");
				//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFOperatorExpDtls " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
				#region Acord82 Page
				if (DSTempDataSet.Tables[0].Rows.Count >0)
				{
					XmlElement Acord82OperatorViolations;
					Acord82OperatorViolations = AcordPDFXML.CreateElement("VIOLATIONS");
					Acord82RootElement.AppendChild(Acord82OperatorViolations);
					Acord82OperatorViolations.SetAttribute(fieldType,fieldTypeMultiple);
					Acord82OperatorViolations.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82OPREXP"));
					Acord82OperatorViolations.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82OPREXP"));
					Acord82OperatorViolations.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82OPREXPEXTN"));
					Acord82OperatorViolations.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82OPREXPEXTN"));

					int RowCounter = 0;
					
					string tempStr="0";
					//To fill the Operators and MVR details on main Page.
					foreach(DataRow Row in DSTempDataSet.Tables[0].Rows)
					{
						if(tempStr!=Row["DRIVER_ID"].ToString())
						{
							XmlElement Acord82ViolationElement;
							Acord82ViolationElement = AcordPDFXML.CreateElement("VIOLATIONINFO");
							Acord82OperatorViolations.AppendChild(Acord82ViolationElement);
							Acord82ViolationElement.SetAttribute(fieldType,fieldTypeNormal);
							Acord82ViolationElement.SetAttribute(id,RowCounter.ToString());
							
							
							Acord82ViolationElement.InnerXml = Acord82ViolationElement.InnerXml +  "<OPERATOREXPNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["OPERATORNO"].ToString()) + "</OPERATOREXPNUM>";
							Acord82ViolationElement.InnerXml = Acord82ViolationElement.InnerXml +  "<OPERATOREXPSUMMARY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["EXPDESCRIPTION"].ToString()) + "</OPERATOREXPSUMMARY>";
							RowCounter++;
						}
						tempStr=Row["DRIVER_ID"].ToString();
							
					}
					tempStr="0";
					foreach(DataRow Row in DSTempDataSet.Tables[0].Rows)
					{
						if(tempStr==Row["DRIVER_ID"].ToString())
						{
							XmlElement Acord82ViolationElement1;
							Acord82ViolationElement1 = AcordPDFXML.CreateElement("VIOLATIONINFO");
							Acord82OperatorViolations.AppendChild(Acord82ViolationElement1);
							Acord82ViolationElement1.SetAttribute(fieldType,fieldTypeNormal);
							Acord82ViolationElement1.SetAttribute(id,RowCounter.ToString());
							
						
							Acord82ViolationElement1.InnerXml = Acord82ViolationElement1.InnerXml +  "<OPERATOREXPNUM " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["OPERATORNO"].ToString()) + "</OPERATOREXPNUM>";
							Acord82ViolationElement1.InnerXml = Acord82ViolationElement1.InnerXml +  "<OPERATOREXPSUMMARY " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(Row["EXPDESCRIPTION"].ToString()) + "</OPERATOREXPSUMMARY>";
							RowCounter++;
						}
						tempStr=Row["DRIVER_ID"].ToString();
							
					}

				}
				#endregion
			}
		}*/
		#endregion

		#region Code for Acord 82 Underwriting And General Info Xml Generation
		private void createBoatUnderwritingGeneralXML()
		{
			double sumTtlC=0,unattach_prem=0;
			int SchPrem=0;
			DataSet Dstrailpre = new DataSet();	
			DataSet DsSchprem = new DataSet();

			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DSTempDataSet = gobjWrapper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS");
			gobjWrapper.ClearParameteres();

			//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOATDETAILS " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
			
			gobjWrapper.AddParameter("@CUSTOMER_ID",gStrClientID);
			gobjWrapper.AddParameter("@POL_ID",gStrPolicyId);
			gobjWrapper.AddParameter("@POL_VERSION_ID",gStrPolicyVersion);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DsSchprem = gobjWrapper.ExecuteDataSet("PROC_GetScheduleEquip_Premium");
			gobjWrapper.ClearParameteres();

			//DsSchprem =  gobjSqlHelper.ExecuteDataSet("PROC_GetScheduleEquip_Premium " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
			// fetch total policy premium
			foreach(DataRow BoatDetail in DSTempDataSet.Tables[0].Rows)
			{
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@BOATID",BoatDetail["BOAT_ID"]);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DataSet DSTempEngineTrailer1 = gobjWrapper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails");
				gobjWrapper.ClearParameteres();
			
				//DataSet DSTempEngineTrailer1 = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFBoatCovgDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
			
				gobjWrapper.AddParameter("@CUSTOMER_ID",gStrClientID);
				gobjWrapper.AddParameter("@POL_ID",gStrPolicyId);
				gobjWrapper.AddParameter("@POL_VERSION_ID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@BOAT_ID",BoatDetail["BOAT_ID"]);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				Dstrailpre = gobjWrapper.ExecuteDataSet("PROC_GetTrailer_Premium");
				gobjWrapper.ClearParameteres();

				//Dstrailpre = gobjSqlHelper.ExecuteDataSet("PROC_GetTrailer_Premium " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," +BoatDetail["BOAT_ID"] +  ",'" + gStrCalledFrom + "'");
				// If Proccess is not committed then fetch premiums from premium xml
				// if proccess is committed then fetch premiums from split table
				if(gStrtemp!="final")
				{					
				}
				else
				{
					// premium from split table 
					
					SchPrem=Convert.ToInt32(GetTrailerPremium(DsSchprem,"COVERAGE_PREMIUM"));
					sumTtlC += GetPremiumSch(DSTempEngineTrailer1,"B","COVERAGE_PREMIUM");
					sumTtlC += GetTrailerPremium(Dstrailpre,"COVERAGE_PREMIUM");
					sumTtlC +=Convert.ToInt32(GetPremiumUnattached(DSTempEngineTrailer1,"UAE","COVERAGE_PREMIUM"));
				}
			}
			if(gStrtemp!="final")
			{
				foreach (XmlNode SumTotalNode in GetSumTotalPremium())
				{
					if(getAttributeValue(SumTotalNode,"STEPPREMIUM")!=null && getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()!="" )
						sumTtlC += double.Parse(getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()) ;						
				}
				//	SchPrem = GetPortAccessPrem();
				// premium for unattached equipment
				foreach (XmlNode PremiumNode in GetUnattachedPremium("1"))
				{
					if(getAttributeValue(PremiumNode,"COMPONENT_CODE")=="BOAT_UNATTACH_PREMIUM" && getAttributeValue(PremiumNode,"STEPPREMIUM")!="" && getAttributeValue(PremiumNode,"STEPPREMIUM")!="Included")
					{
						unattach_prem=Convert.ToDouble(getAttributeValue(PremiumNode,"STEPPREMIUM"));
					}
				}
			}
			if (gStrPdfFor == PDFForAcord)
			{
				XmlElement Accord82BoatRemark;
				Accord82BoatRemark = AcordPDFXML.CreateElement("BOATREMARK");

	
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFUnderwritingDetails");
				gobjWrapper.ClearParameteres();

				//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFUnderwritingDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
				if(DSTempDataSet.Tables[0].Rows.Count >0)
				{
					#region Acord82 Page General Info
					XmlElement Acord82GenInfoElement;
					Acord82GenInfoElement = AcordPDFXML.CreateElement("GENERALINFO");
					Acord82RootElement.AppendChild(Acord82GenInfoElement);
					Acord82GenInfoElement.SetAttribute(fieldType,fieldTypeSingle);

					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFOAPPLIVCURADD3YR " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["LIVED_CURR_ADD_3YR"].ToString()) + "</GENINFOAPPLIVCURADD3YR>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFOOPRPHYIMPAIRMENT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PHY_MENTL_CHALLENGED"].ToString()) + "</GENINFOOPRPHYIMPAIRMENT>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFODRVLICSUSPEND " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["DRIVER_SUS_REVOKED"].ToString()) + "</GENINFODRVLICSUSPEND>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFOOPRHADACCIDENT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_CONVICTED_ACCIDENT"].ToString()) + "</GENINFOOPRHADACCIDENT>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFOOTHINSUWITHCOMP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["MULTI_POLICY_DISC_APPLIED"].ToString().Trim()) + "</GENINFOOTHINSUWITHCOMP>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFOANYLOSS3YRS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_LOSS_THREE_YEARS"].ToString()) + "</GENINFOANYLOSS3YRS>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFOCOVGDECLINED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["COVERAGE_DECLINED"].ToString()) + "</GENINFOCOVGDECLINED>";

					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<PREVIOUSADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Previous Address: " + DSTempDataSet.Tables[0].Rows[0]["YEARS_AT_PREV_ADD"].ToString()) + "</PREVIOUSADDRESS>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<DRIVERIMPAIRMENT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Driver Impairment Description: " + DSTempDataSet.Tables[0].Rows[0]["PHY_MENTL_CHALLENGED_DESC"].ToString()) + "</DRIVERIMPAIRMENT>";
					
					#endregion

					if(DSTempDataSet.Tables[0].Rows[0]["DRIVER_SUS_REVOKED_DESC"].ToString() != "" ||
						DSTempDataSet.Tables[0].Rows[0]["IS_CONVICTED_ACCIDENT_DESC"].ToString() != "" ||
						DSTempDataSet.Tables[0].Rows[0]["MULTI_POLICY_DISC_APPLIED_PP_DESC"].ToString() != "" ||
						DSTempDataSet.Tables[0].Rows[0]["ANY_LOSS_THREE_YEARS_DESC"].ToString() != "" ||
						DSTempDataSet.Tables[0].Rows[0]["COVERAGE_DECLINED_DESC"].ToString() != "")
					{
						#region Accord 82 Boat Remark Element
						Acord82RootElement.AppendChild(Accord82BoatRemark);
						Accord82BoatRemark.SetAttribute(fieldType,fieldTypeMultiple);
						Accord82BoatRemark.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD82BOATREMARKS"));
						Accord82BoatRemark.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD82BOATREMARKS"));
						Accord82BoatRemark.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD82BOATREMARKSEXTN"));
						Accord82BoatRemark.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD82BOATREMARKSEXTN"));
						#endregion

						#region Acord 82 Sheet Remarks
						XmlElement ACORD82REMARKSINFO;
						ACORD82REMARKSINFO = AcordPDFXML.CreateElement("ACORD82REMARKSINFO");
						Accord82BoatRemark.AppendChild(ACORD82REMARKSINFO);				
						ACORD82REMARKSINFO.SetAttribute(fieldType,fieldTypeNormal);
						ACORD82REMARKSINFO.SetAttribute(id,"0");
				
					
						ACORD82REMARKSINFO.InnerXml = ACORD82REMARKSINFO.InnerXml +  "<LICENSEDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Driver License Description: " + DSTempDataSet.Tables[0].Rows[0]["DRIVER_SUS_REVOKED_DESC"].ToString()) + "</LICENSEDESC>";
						ACORD82REMARKSINFO.InnerXml = ACORD82REMARKSINFO.InnerXml +  "<VIOLATIONDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Violation Description: " + DSTempDataSet.Tables[0].Rows[0]["IS_CONVICTED_ACCIDENT_DESC"].ToString()) + "</VIOLATIONDESC>";
						ACORD82REMARKSINFO.InnerXml = ACORD82REMARKSINFO.InnerXml +  "<POLICYDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Multipolicy Description: " + DSTempDataSet.Tables[0].Rows[0]["MULTI_POLICY_DISC_APPLIED_PP_DESC"].ToString()) + "</POLICYDESC>";
						ACORD82REMARKSINFO.InnerXml = ACORD82REMARKSINFO.InnerXml +  "<LOSSDESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Loss Description: " + DSTempDataSet.Tables[0].Rows[0]["ANY_LOSS_THREE_YEARS_DESC"].ToString()) + "</LOSSDESC>";
						ACORD82REMARKSINFO.InnerXml = ACORD82REMARKSINFO.InnerXml +  "<COVERAGEDECLINED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Coverage Declination Description: " + DSTempDataSet.Tables[0].Rows[0]["COVERAGE_DECLINED_DESC"].ToString()) + "</COVERAGEDECLINED>";					
					
						#endregion
					}
					#region Supplemental Page underwriting info
					XmlElement SupplementUnderwritingElement;
					SupplementUnderwritingElement = AcordPDFXML.CreateElement("UNDERWRITINGINFO");
					SupplementalRootElement.AppendChild(SupplementUnderwritingElement);
					SupplementUnderwritingElement.SetAttribute(fieldType,fieldTypeSingle);
					if(!DSTempDataSet.Tables[0].Rows[0]["PARTICIPATE_RACE"].ToString().Trim().Equals("0"))
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<PARTICIPATE " + fieldType + "=\"" + fieldTypeText + "\">" + "Yes" + "</PARTICIPATE>";
					else 
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<PARTICIPATE " + fieldType + "=\"" + fieldTypeText + "\">" + "No" + "</PARTICIPATE>";
					if(!DSTempDataSet.Tables[0].Rows[0]["CARRY_PASSENGER_FOR_CHARGE"].ToString().Trim().Equals("0"))
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<PASSENGERS " + fieldType + "=\"" + fieldTypeText + "\">" + "Yes" + "</PASSENGERS>";
					else
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<PASSENGERS " + fieldType + "=\"" + fieldTypeText + "\">" + "No" + "</PASSENGERS>";
					if(!DSTempDataSet.Tables[0].Rows[0]["IS_RENTED_OTHERS"].ToString().Trim().Equals("0"))
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<RENTED " + fieldType + "=\"" + fieldTypeText + "\">" +"Yes" + "</RENTED>";
					else
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<RENTED " + fieldType + "=\"" + fieldTypeText + "\">" +"No" + "</RENTED>";
					//Watercraft used other than WI/MI/IN states is not done yet as it is not implemented in Wolverine CMS.
					if(!DSTempDataSet.Tables[0].Rows[0]["IS_BOAT_USED_IN_ANY_WATER"].ToString().Trim().Equals("0"))
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<OTHERUSE " + fieldType + "=\"" + fieldTypeText + "\">" + "Yes" + "</OTHERUSE>";
					else
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<OTHERUSE " + fieldType + "=\"" + fieldTypeText + "\">" + "No" + "</OTHERUSE>";

					SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<REGISTEREDPLACE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["STATE_REG"].ToString()) + "</REGISTEREDPLACE>";
					if(!DSTempDataSet.Tables[0].Rows[0]["IS_REGISTERED_OTHERS"].ToString().Trim().Equals("0"))
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<OTHERREGISTERED " + fieldType + "=\"" + fieldTypeText + "\">" +"Yes" + "</OTHERREGISTERED>";
					else 
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<OTHERREGISTERED " + fieldType + "=\"" + fieldTypeText + "\">" +"No" + "</OTHERREGISTERED>";
					if(!DSTempDataSet.Tables[0].Rows[0]["DRINK_DRUG_VOILATION"].ToString().Trim().Equals("0"))
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<DRINKING " + fieldType + "=\"" + fieldTypeText + "\">" +"Yes" + "</DRINKING>";
					else 
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<DRINKING " + fieldType + "=\"" + fieldTypeText + "\">" +"No" + "</DRINKING>";
					if(!DSTempDataSet.Tables[0].Rows[0]["IS_BOAT_COOWNED"].ToString().Trim().Equals("0"))
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<COOWNED " + fieldType + "=\"" + fieldTypeText + "\">" + "Yes" + "</COOWNED>";
					else 
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<COOWNED " + fieldType + "=\"" + fieldTypeText + "\">" + "No" + "</COOWNED>";
					SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<PRIORINSURER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PRIOR_INSURANCE"].ToString()) + "</PRIORINSURER>";

					if (gStrPdfFor == PDFForAcord)
					{
						if(gStrtemp!="final")
						{
							if(htpremium.Contains("SUMTOTAL"))
							{
								SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<POLICYPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">" + ((sumTtlC + sumtotal + unattach_prem) + ".00")+ "</POLICYPREMIUM>";
							}
						}
						else
						{
							SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<POLICYPREMIUM " + fieldType + "=\"" + fieldTypeText + "\">" + ((sumTtlC + SchPrem) + ".00")+ "</POLICYPREMIUM>";
						}
						
					}

					//All Remarks Go here.
					if(DSTempDataSet.Tables[0].Rows[0]["PARTICIPATE_RACE_DESC"].ToString() != "")
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<REMARKPARTICIPATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("#1" + DSTempDataSet.Tables[0].Rows[0]["PARTICIPATE_RACE_DESC"].ToString()) + "</REMARKPARTICIPATE>";
					if(DSTempDataSet.Tables[0].Rows[0]["CARRY_PASSENGER_FOR_CHARGE_DESC"].ToString() != "")
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<REMARKPASSENGER " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("#2" + DSTempDataSet.Tables[0].Rows[0]["CARRY_PASSENGER_FOR_CHARGE_DESC"].ToString()) + "</REMARKPASSENGER>";
					if(DSTempDataSet.Tables[0].Rows[0]["IS_RENTED_OTHERS_DESC"].ToString() != "")
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<REMARKRENTED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("#3" + DSTempDataSet.Tables[0].Rows[0]["IS_RENTED_OTHERS_DESC"].ToString()) + "</REMARKRENTED>";
					if(DSTempDataSet.Tables[0].Rows[0]["IS_BOAT_USED_IN_ANY_WATER_DESC"].ToString() != "")
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<REMARKSWATERUSED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("#4" + DSTempDataSet.Tables[0].Rows[0]["IS_BOAT_USED_IN_ANY_WATER_DESC"].ToString()) + "</REMARKSWATERUSED>";
					if(DSTempDataSet.Tables[0].Rows[0]["IS_REGISTERED_OTHERS_DESC"].ToString() != "")
						SupplementUnderwritingElement.InnerXml = SupplementUnderwritingElement.InnerXml +  "<REMARKREGISTRATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("#6" + DSTempDataSet.Tables[0].Rows[0]["IS_REGISTERED_OTHERS_DESC"].ToString()) + "</REMARKREGISTRATION>";
					#endregion
				}
			}
		}
		/*private void createBoatUnderwritingGeneralXML()
		{
			if (gStrPdfFor == PDFForAcord)
			{
				gobjWrapper.ClearParameteres();
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFUnderwritingDetails");
				//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFUnderwritingDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
				if(DSTempDataSet.Tables[0].Rows.Count >0)
				{
					#region Acord82 Page General Info
					XmlElement Acord82GenInfoElement;
					Acord82GenInfoElement = AcordPDFXML.CreateElement("GENERALINFO");
					Acord82RootElement.AppendChild(Acord82GenInfoElement);
					Acord82GenInfoElement.SetAttribute(fieldType,fieldTypeSingle);

					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFOAPPLIVCURADD3YR " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["LIVED_CURR_ADD_3YR"].ToString()) + "</GENINFOAPPLIVCURADD3YR>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFOOPRPHYIMPAIRMENT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["PHY_MENTL_CHALLENGED"].ToString()) + "</GENINFOOPRPHYIMPAIRMENT>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFODRVLICSUSPEND " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["DRIVER_SUS_REVOKED"].ToString()) + "</GENINFODRVLICSUSPEND>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFOOPRHADACCIDENT " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_CONVICTED_ACCIDENT"].ToString()) + "</GENINFOOPRHADACCIDENT>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFOOTHINSUWITHCOMP " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["MULTI_POLICY_DISC_APPLIED"].ToString()) + "</GENINFOOTHINSUWITHCOMP>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFOANYLOSS3YRS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_LOSS_THREE_YEARS"].ToString()) + "</GENINFOANYLOSS3YRS>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFOCOVGDECLINED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["COVERAGE_DECLINED"].ToString()) + "</GENINFOCOVGDECLINED>";
					//Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<GENINFOREMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["REMARKGENINFO"].ToString()) + "</GENINFOREMARKS>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<ATTACHPHOTOGRAPH " + fieldType + "=\"" + fieldTypeText + "\">" + getPhotoAttach + "</ATTACHPHOTOGRAPH>";
					Acord82GenInfoElement.InnerXml = Acord82GenInfoElement.InnerXml +  "<ATTACHSURVEY " + fieldType + "=\"" + fieldTypeText + "\">" + getMarineSurvey + "</ATTACHSURVEY>";
				
					#endregion
				}
					
			}
		}*/
		#endregion

		#region code for dwelling info Xml
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

		private string AfterComma(int number)
		{
			string num = number.ToString();
			if(num.Length == 1)
				num = "00" + num;
			else if(num.Length == 2)
				num = "0" + num;
			return num;
		}

		private void createDwellingXML()
		{
			XmlElement DecPageDwellMultipleElement;
			DecPageDwellMultipleElement    = AcordPDFXML.CreateElement("HOME");
			
			XmlElement Acord80DwellMultipleElement;
			Acord80DwellMultipleElement    = AcordPDFXML.CreateElement("HOME");
			
			XmlElement SupplementDwellMultipleElement;
			SupplementDwellMultipleElement    = AcordPDFXML.CreateElement("HOME");
			
			XmlElement DecPageDwellMultipleElement0;
			DecPageDwellMultipleElement0    = AcordPDFXML.CreateElement("HOME0");
			
			XmlElement DecPageHomeElement0;
			DecPageHomeElement0	= AcordPDFXML.CreateElement("HOMEINFO0");

			int DwellingCtr = 0,AddInt=0;
			double sumTtl=0;
			string strProprtyFee="";
			DataSet DSTempDwellSumtotal = new DataSet();
			#region setting Dwelling Root Attribute
			if (gStrPdfFor == PDFForDecPage)
			{
				prnOrd_covCode = new string[100];
				prnOrd_attFile = new string[100];
				prnOrd = new int[100];
				#region Dwelling Root Element for DecPage
				DecPageRootElement.AppendChild(DecPageDwellMultipleElement0);
				DecPageDwellMultipleElement0.SetAttribute(fieldType,fieldTypeMultiple);
				DecPageDwellMultipleElement0.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHOME"));
				DecPageDwellMultipleElement0.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHOME"));
				DecPageDwellMultipleElement0.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHOMEEXTN"));
				DecPageDwellMultipleElement0.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHOMEEXTN"));
				#endregion

				// Added by Mohit Agarwal for Customer Details irrespective of added boats
				// 14-Mar-2007
				#region setting Customer Agency Details
				DecPageDwellMultipleElement0.AppendChild(DecPageHomeElement0);
				DecPageHomeElement0.SetAttribute(fieldType,fieldTypeNormal);
				DecPageHomeElement0.SetAttribute(id,DwellingCtr.ToString());
				if(string.Compare(gStrCalledFrom,"Policy",true)==0)
				{
					DecPageHomeElement0.InnerXml += "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNUMBER>";
					DecPageHomeElement0.InnerXml +="<POLICYVERSION " + fieldType + "=\"" + fieldTypeText + "\">" + gStrPolicyVersion + "</POLICYVERSION>";
					DecPageHomeElement0.InnerXml += "<POLICYEFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</POLICYEFFDATE>";
					DecPageHomeElement0.InnerXml += "<POLICYEXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</POLICYEXPDATE>";
				}
				DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<reason " + fieldType + "=\"" + fieldTypeText + "\">" + Reason + "</reason>";
				DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<copyTo " + fieldType + "=\"" + fieldTypeText + "\">" + CopyTo + "</copyTo>";
				DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<PRIMARYCONTACTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</PRIMARYCONTACTNAME>";
				DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<PRIMARYCONTACTADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantAddress + "</PRIMARYCONTACTADDRESS>";
				DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<PRIMARYCONTACTCITY " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantCityStZip + "</PRIMARYCONTACTCITY>";
				DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyName + "</AGENCYNAME>";
				DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyAddress + "</AGENCYADDRESS>";
				DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<AGENCYCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCitySTZip + "</AGENCYCITYSTATEZIP>";
				DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<AGENCYPHONENO " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyPhoneNumber + "</AGENCYPHONENO>";
				DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCode + "</AGENCYCODE>";
				DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencySubCode + "</AGENCYSUBCODE>";
				DecPageHomeElement0.InnerXml = DecPageHomeElement0.InnerXml +  "<AGENCYBILLING " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyBilling + "</AGENCYBILLING>";
								
				string strAdverseRep="";
				strAdverseRep = DSTempApplicantDataSet.Tables[1].Rows[0]["ADVERSE_LETTER_REQD"].ToString();
				if(gStrCopyTo != "AGENCY")
				{
					if(gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS.ToString() && gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS.ToString() 
						&& gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REWRITE_PROCESS.ToString() && gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REWRITE_PROCESS.ToString()
						&& gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REINSTATEMENT_PROCESS.ToString() && gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REINSTATEMENT_PROCESS.ToString()
						&& gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RENEWAL_PROCESS.ToString() && gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RENEWAL_PROCESS.ToString()
						&& gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_NEW_BUSINESS_PROCESS.ToString() && gStrProcessID !=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_NEW_BUSINESS_PROCESS.ToString())
					{
						if(gStrCopyTo != "AGENCY" && strNeedPage2 == "Y")
							createPage2XML(ref DecPageHomeElement0);
					}
					else if(gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS.ToString() || gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS.ToString() )
					{
						//store preassigned value of strNeedPage2
						string elginsusc=strNeedPage2;
						if(gStrCopyTo != "AGENCY" && strAdverseRep=="Y" && strNeedPage2 == "Y" && strInsuScore !="-2")
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
										createPage2AdverseXML(ref DecPageHomeElement0);
								}
								else
								{
									createPage2AdverseXML(ref DecPageHomeElement0);
								}
							}
							
						}
						else if(gStrCopyTo != "AGENCY"  && strAdverseRep=="Y" && strNeedPage2 == "Y" && strInsuScore =="-2")
						{
							//call ChkPreInsuScr function to assign values to newInsuScr,oldInsuScr
							ChkPreInsuScr();
							if(newInsuScr == oldInsuScr)
							{

							}
							else
							{
								if(elginsusc=="Y")
									createPage2NHNSAdverseXML(ref DecPageHomeElement0);
							}
						}

					}
					else if(gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RENEWAL_PROCESS.ToString() || gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RENEWAL_PROCESS.ToString())
					{
						//store preassigned value of strNeedPage2
						string elginsusc=strNeedPage2;
						// if insurance score is not no hit no score
						if(gStrCopyTo != "AGENCY" && strNeedPage2 == "Y" && strInsuScore !="-2")
						{

							//ChkPreInsuScr();
							//if(newInsuScr == oldInsuScr)
							//{

							//}
							//else
							//{
								if(oldInsuScr !="-2")
								{
									// refernce itrack 3222 if fallen in lower tier then print
									if(strNeedPage2 =="Y")
										createPage2AdverseXML(ref DecPageHomeElement0);
								}
								else
								{
									createPage2AdverseXML(ref DecPageHomeElement0);
								}
							//}
						}
							//if insuracne score is no hit no score
						else if(gStrCopyTo != "AGENCY" && strNeedPage2 == "Y" && strInsuScore =="-2")
						{
							//call ChkPreInsuScr function to assign values to newInsuScr,oldInsuScr
							ChkPreInsuScr();
							if(newInsuScr == oldInsuScr)
							{

							}
							else
							{
								if(elginsusc=="Y")
									createPage2NHNSAdverseXML(ref DecPageHomeElement0);
							}
						}
					}
					else if(gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_NEW_BUSINESS_PROCESS.ToString() || gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_NEW_BUSINESS_PROCESS.ToString())
					{
						if(gStrCopyTo != "AGENCY" && strNeedPage2 == "Y")
						{
							createPage2XML(ref DecPageHomeElement0);
						}
						else if( strNeedPage2 == "N")
						{
							createPage2PrivacyPageXML(ref DecPageHomeElement0);
						}
					}
					else if(gStrProcessID == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REWRITE_PROCESS.ToString() || gStrProcessID == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REWRITE_PROCESS.ToString())
					{
						if( strNeedPage2 == "N")
						{
							createPage2PrivacyPageXML(ref DecPageHomeElement0);
						}
						else
						{
							createPage2XML(ref DecPageHomeElement0);
						}
					}
					else
					{
						if(gStrCopyTo != "AGENCY" && strNeedPage2 == "Y")
						{
							createPage2AdverseXML(ref DecPageHomeElement0);
						}
					}
				}
				#endregion

				DecPageHomeElement0.AppendChild(DecPageDwellMultipleElement);
				DecPageDwellMultipleElement.SetAttribute(fieldType,fieldTypeMultiple);
				DecPageDwellMultipleElement.SetAttribute(PrimPDF,"");
				DecPageDwellMultipleElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHOME"));
				DecPageDwellMultipleElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHOMEEXTN"));
				DecPageDwellMultipleElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHOMEEXTN"));
			}
			else if (gStrPdfFor == PDFForAcord)
			{
				#region Dwelling Root Element for Accord 80
				Acord80RootElement.AppendChild(Acord80DwellMultipleElement);
				Acord80DwellMultipleElement.SetAttribute(fieldType,fieldTypeMultiple);
				Acord80DwellMultipleElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD80HOME"));
				Acord80DwellMultipleElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD80HOME"));
				Acord80DwellMultipleElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD80HOMEEXTN"));
				Acord80DwellMultipleElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD80HOMEEXTN"));
				#endregion

				#region Dwelling Root Element for Accord 80 Supplement
				SupplementalRootElement.AppendChild(SupplementDwellMultipleElement);
				SupplementDwellMultipleElement.SetAttribute(fieldType,fieldTypeMultiple);
				SupplementDwellMultipleElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTHOME"));
				SupplementDwellMultipleElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTHOME"));
				SupplementDwellMultipleElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTHOMEEXTN"));
				SupplementDwellMultipleElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTHOMEEXTN"));
				#endregion			
			}
			#endregion
//			gobjWrapper.ClearParameteres();
//			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
//			gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//			DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_Dwelling_Details");
			// DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_Dwelling_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
			
			if (gStrPdfFor == PDFForAcord)
			{
				#region Acord 81 Page
				if (DSTempDwellingDetailDataSet.Tables[0].Rows.Count>0)
				{
					XmlElement Acord81RatingInfo;
					Acord81RatingInfo = AcordPDFXML.CreateElement("RATINGINFO");
					Acord81RootElement.AppendChild(Acord81RatingInfo);
					Acord81RatingInfo.SetAttribute(fieldType,fieldTypeSingle);
				
					Acord81RatingInfo.InnerXml += "<APPLICANTPROTECTCLASS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellingDetailDataSet.Tables[0].Rows[0]["PROT_CLASS"].ToString()) +"</APPLICANTPROTECTCLASS>"; 
					//if(!strAppAddress1.Equals(DSTempDataSet.Tables[0].Rows[0]["LOC_ADD1"].ToString()) && !strAppAddress2.Equals(DSTempDataSet.Tables[0].Rows[0]["LOC_ADD2"].ToString()) && !strAppCity.Equals(DSTempDataSet.Tables[0].Rows[0]["LOC_CITY"].ToString()) && !strAppState.Equals(DSTempDataSet.Tables[0].Rows[0]["STATE_CODE"].ToString()) && !strAppZip.Equals(DSTempDataSet.Tables[0].Rows[0]["LOC_ZIP"].ToString())) 
					if(ApplicantAddress.Trim() != DSTempDwellingDetailDataSet.Tables[0].Rows[0]["LOC_ADDRESS"].ToString().Trim() || ApplicantCityStZip.Trim() != DSTempDwellingDetailDataSet.Tables[0].Rows[0]["LOC_CITYSTATEZIP"].ToString().Trim())
					{
						Acord81RatingInfo.InnerXml += "<LOCATIONADDRESS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellingDetailDataSet.Tables[0].Rows[0]["LOC_ADDRESS"].ToString()) +"</LOCATIONADDRESS>"; 
						Acord81RatingInfo.InnerXml += "<LOCATIONCITYSTZIP " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellingDetailDataSet.Tables[0].Rows[0]["LOC_CITYSTATEZIP"].ToString()) +"</LOCATIONCITYSTZIP>";
					}
					Acord81RatingInfo.InnerXml += "<DWELLINGTYPE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellingDetailDataSet.Tables[0].Rows[0]["DWELLING_TYPE"].ToString()) +"</DWELLINGTYPE>"; 
					Acord81RatingInfo.InnerXml += "<CONSTRUCTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellingDetailDataSet.Tables[0].Rows[0]["CONSTRUCTION_TYPE"].ToString()) +"</CONSTRUCTION>"; 
					Acord81RatingInfo.InnerXml += "<NOFAMILIES " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DSTempDwellingDetailDataSet.Tables[0].Rows[0]["NO_OF_FAMILIES"].ToString()) +"</NOFAMILIES>";
				
					//ACORD 80 ATTACHMENTS
					Acord80RootElement.SelectSingleNode("POLICY/ATTACHPROTDEVICE").InnerText = RemoveJunkXmlCharacters(DSTempDwellingDetailDataSet.Tables[0].Rows[0]["ALARM_CERT_ATTACHED"].ToString());
				}
				else
					AcordPDFXML.SelectSingleNode(RootElement).RemoveChild(SupplementalRootElement);
				#endregion
			}
			htpremium.Clear();
			if(gStrtemp!="final")
			{
				
				foreach(DataRow DwellingDetail in DSTempDwellingDetailDataSet.Tables[0].Rows)
				{
					foreach (XmlNode PremiumNode in GetPremium(DwellingDetail["DWELLING_ID"].ToString()))
					{
						if(!htpremium.Contains(getAttributeValue(PremiumNode,"COMPONENT_CODE")))
							htpremium.Add(getAttributeValue(PremiumNode,"COMPONENT_CODE"),getAttributeValue(PremiumNode,"STEPPREMIUM"));
					}
				}
			}
				// Total Policy Premium
			foreach(DataRow DwellingRow in DSTempDwellingDetailDataSet.Tables[0].Rows)
			{
				
				if(gStrtemp!="final")
				{			
					foreach (XmlNode SumTotalNode in GetHomeSumGrandPremium())
					{
						//if(getAttributeValue(SumTotalNode,"STEPPREMIUM")!=null && getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()!="" )
							sumTtl += double.Parse(SumTotalNode.InnerText.ToString().Trim()) ;
					}
				}
				else
				{
					gobjWrapper.ClearParameteres();
					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@DWELLINGID",DwellingRow["DWELLING_ID"]);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempDwellSumtotal = gobjWrapper.ExecuteDataSet("PROC_GETPDFHOMEOWNER_RISKWISE_SUMTOTAL");
					double sumtotal = GetPremiumAllHomeBoat(DSTempDwellSumtotal, "SUMTOTAL");
					if (sumtotal.ToString() != "")
						sumTtl = sumtotal; 
				} 
			}
			foreach(DataRow DwellingDetail in DSTempDwellingDetailDataSet.Tables[0].Rows)
			{
				#region Dwelling Details 
				XmlElement DecPageHomeElement;
				DecPageHomeElement	= AcordPDFXML.CreateElement("HOMEINFO");

				XmlElement Acord80HomeElement;
				Acord80HomeElement	= AcordPDFXML.CreateElement("HOMEINFO");

				XmlElement SupplementHomeElement;
				SupplementHomeElement	= AcordPDFXML.CreateElement("HOMEINFO");

				gobjWrapper.ClearParameteres();
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@DWELLINGID",DwellingDetail["DWELLING_ID"]);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DataSet DSTempDwelling = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeOwner_Coverage_Details");
				//DataSet DSTempDwelling = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeOwner_Coverage_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + DwellingDetail["DWELLING_ID"] +  ",'" + gStrCalledFrom + "'");
				
				// Other Structure details (Start)
				gobjWrapper.ClearParameteres();
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@DWELLINGID",DwellingDetail["DWELLING_ID"]);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DataSet DSTempOtherStructureDetails = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_otherstructures");

				// Other Structure details (End)


				double RskSumTtl=0;
				htpremium.Clear(); 
				///////////////////////////////////////
				//    Sum Total(start)
				//////////////////////////////////////
				foreach (XmlNode PremiumNode in GetPremium(DwellingDetail["DWELLING_ID"].ToString()))
				{
					if(!htpremium.Contains(getAttributeValue(PremiumNode,"COMPONENT_CODE")))
						htpremium.Add(getAttributeValue(PremiumNode,"COMPONENT_CODE"),getAttributeValue(PremiumNode,"STEPPREMIUM"));
				}
					
				if(gStrtemp == "temp")
				{
					foreach (XmlNode SumTotalNode in GetHomeSumTotalPremium(DwellingDetail["DWELLING_ID"].ToString(),"HOME"))
					{
						if(getAttributeValue(SumTotalNode,"STEPPREMIUM")!=null && getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()!="" )
							RskSumTtl += double.Parse(getAttributeValue(SumTotalNode,"STEPPREMIUM").ToString()) ;						
					}
				}
				else
				{
					double sumtotal = GetPremiumAllHomeBoat(DSTempDwellSumtotal, "SUMTOTAL", DwellingDetail["DWELLING_ID"].ToString(),"HOME");
						RskSumTtl += sumtotal;
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
					XmlNodeList ins_scoreNodeList = GetCreditForHomeInsScore();
					XmlNode ins_scoreNode;
					if(ins_scoreNodeList.Count > 0)
					{
						ins_scoreNode = ins_scoreNodeList.Item(0);
						String [] discRows = getAttributeValue(ins_scoreNode,"STEPDESC").Split('-');

						if(discRows.Length >= 1)
							inspercent = discRows[discRows.Length -1];
					}
				}
				// if proccess committed
				else
				{
					inspercent="";
					foreach(DataRow CoverageDetails in DSTempDwelling.Tables[1].Rows)
					{
						if(CoverageDetails["COMPONENT_CODE"].ToString() == "D_INS_SCR")
							inspercent = CoverageDetails["COM_EXT_AD"].ToString();
					}
				}
				/////////////////////////////////////
				// Insurance Score Discount(End)
				////////////////////////////////////
				/////////////////////////////////////////////
				///// Property Expense fee (Start)
				////////////////////////////////////////////////
				// if Process not committed
				if(gStrtemp !="final")
				{
					strProprtyFee="0";
					foreach(XmlNode HomeNode in GetPropertyExpenseFee(DwellingDetail["DWELLING_ID"].ToString()))
					{
						if(getAttributeValue(HomeNode,"STEPPREMIUM")!=null && getAttributeValue(HomeNode,"STEPPREMIUM").ToString()!="" )
						{
							strProprtyFee = getAttributeValue(HomeNode,"STEPPREMIUM").ToString();						
						}
					}
				}
					//if process commited
				else
				{
					strProprtyFee="0";
					foreach(DataRow CoverageDetails in DSTempDwelling.Tables[1].Rows)
					{
						if(CoverageDetails["COMPONENT_CODE"].ToString() == "PRP_EXPNS_FEE")
							strProprtyFee = CoverageDetails["COVERAGE_PREMIUM"].ToString();
					}
				}	
					// Property Expense FEE FOR AGENT'S COPY (ACCOUNTING PURPOSE)
					//DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<AGENCYACCOUNTINFORMATION  " + fieldType +"=\""+ fieldTypeText +"\">"+"**Account Information - "+"$"+RemoveJunkXmlCharacters(DollarFormat(double.Parse(strProprtyFee.ToString()))) + "**"+"</AGENCYACCOUNTINFORMATION>";
					if(gStrCopyTo == "AGENCY") //&& flgError==1)
					{
						DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<AGENCYACCOUNTINFORMATION  " + fieldType +"=\""+ fieldTypeText +"\">"+"**Account Information - "+"$"+RemoveJunkXmlCharacters(DollarFormat(double.Parse(strProprtyFee.ToString()))) + "**"+"</AGENCYACCOUNTINFORMATION>";
					}
				
				///////////////////////////////////////////////////
				///// Property Expense Fee (End)
				///////////////////////////////////////////////////
				//////////////////////////////////////////////////
				///// Policy adjusted premium (Start)
				/////////////////////////////////////////////////
				string adjPrm="";
				if(gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS.ToString() || gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS.ToString() 
					|| gStrProcessID ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_REINSTATEMENT_PROCESS.ToString() || gStrProcessID == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REINSTATEMENT_PROCESS.ToString())
				{
					if(gStrtemp !="final")
					{
						adjPrm = DollarFormat(GetEffectivePremium(CalculateEffcPremium(strOldPolicyVer),sumTtl));
					}
					else
					{
						adjPrm = DollarFormat(CalculateEffcPremium(strOldPolicyVer));
					}
					if(adjPrm !="")
						adjPrm = "$" + adjPrm;
				}
				//////////////////////////////////////////////////
				///// Policy adjusted premium (End)
				/////////////////////////////////////////////////
				/////No hit No Score
				if(strInsScore == "-2")
				{
					strInsScore = "No Hit/No Score";
				}
				//-1
				if(strInsScore == "-1")
				{
					strInsScore = "";
				}
				////////////////////////////////////////////////////////////
				////////////Policy Territoy(Start)
				///////////////////////////////////////////////////////////
				foreach (XmlNode TerritoryNode in GetHomeTerritory())
				{
					gstrBoatTerritory=getAttributeValue(TerritoryNode,"ADDRESS").ToString() ;
					if(gstrBoatTerritory.Length>0 )
					{
						int colonPos=gstrBoatTerritory.IndexOf(":");
						int lastBracketPos=gstrBoatTerritory.IndexOf(")");
						if(lastBracketPos!=-1 && colonPos!=-1)
							gstrBoatTerritory=gstrBoatTerritory.Substring((colonPos+1),(lastBracketPos-colonPos)); 

						gstrBoatTerritory = gstrBoatTerritory.Substring(1,gstrBoatTerritory.Length-2);
						break;
					}
				}
				////////////////////////////////////////////////////////////
				////////////Policy Territoy(End)
				///////////////////////////////////////////////////////////
				if (gStrPdfFor == PDFForDecPage)
				{
					#region Dwelling Element for Dec Page
					DecPageDwellMultipleElement.AppendChild(DecPageHomeElement);
					DecPageHomeElement.SetAttribute(fieldType,fieldTypeNormal);
					DecPageHomeElement.SetAttribute(id,DwellingCtr.ToString());
				
					/*if(string.Compare(gStrCalledFrom,"Policy",true)==0)
					{
						DecPageHomeElement.InnerXml = DecPageHomeElement.InnerXml +  "<POLICYNO " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNO>";
						DecPageHomeElement.InnerXml = DecPageHomeElement.InnerXml +  "<POLICYPERIODFROM " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</POLICYPERIODFROM>";
						DecPageHomeElement.InnerXml = DecPageHomeElement.InnerXml +  "<POLICYPERIODTO " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</POLICYPERIODTO>";
					}
					DecPageHomeElement.InnerXml = DecPageHomeElement.InnerXml +  "<REASON " + fieldType + "=\"" + fieldTypeText + "\">" + Reason + "</REASON>";
					DecPageHomeElement.InnerXml = DecPageHomeElement.InnerXml +  "<copyTo " + fieldType + "=\"" + fieldTypeText + "\">" + CopyTo + "</copyTo>";
					DecPageHomeElement.InnerXml = DecPageHomeElement.InnerXml +  "<PRIMARYCONTACTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</PRIMARYCONTACTNAME>";
					DecPageHomeElement.InnerXml = DecPageHomeElement.InnerXml +  "<PRIMARYCONTACTADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantAddress + "</PRIMARYCONTACTADDRESS>";
					DecPageHomeElement.InnerXml = DecPageHomeElement.InnerXml +  "<PRIMARYCONTACTCITY " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantCityStZip + "</PRIMARYCONTACTCITY>";
					DecPageHomeElement.InnerXml = DecPageHomeElement.InnerXml +  "<AGENCYNAME " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyName + "</AGENCYNAME>";
					DecPageHomeElement.InnerXml = DecPageHomeElement.InnerXml +  "<AGENCYADDRESS " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyAddress + "</AGENCYADDRESS>";
					DecPageHomeElement.InnerXml = DecPageHomeElement.InnerXml +  "<AGENCYCITYSTATEZIP " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCitySTZip + "</AGENCYCITYSTATEZIP>";
					DecPageHomeElement.InnerXml = DecPageHomeElement.InnerXml +  "<AGENCYPHONENO " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyPhoneNumber + "</AGENCYPHONENO>";
					DecPageHomeElement.InnerXml = DecPageHomeElement.InnerXml +  "<AGENCYCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyCode + "</AGENCYCODE>";
					DecPageHomeElement.InnerXml = DecPageHomeElement.InnerXml +  "<AGENCYSUBCODE " + fieldType + "=\"" + fieldTypeText + "\">" + AgencySubCode + "</AGENCYSUBCODE>";
					DecPageHomeElement.InnerXml = DecPageHomeElement.InnerXml +  "<AGENCYBILLING " + fieldType + "=\"" + fieldTypeText + "\">" + AgencyBilling + "</AGENCYBILLING>";

					//Reason Code
					DecPageHomeElement.InnerXml +="<reason_code1 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code1 + "</reason_code1>";
					DecPageHomeElement.InnerXml +="<reason_code2 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code2 + "</reason_code2>";
					DecPageHomeElement.InnerXml +="<reason_code3 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code3 + "</reason_code3>";
					DecPageHomeElement.InnerXml +="<reason_code4 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code4 + "</reason_code4>";
					*/
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<PREMISESDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["LOC_ADDRESS"].ToString()) + ", " + RemoveJunkXmlCharacters(DwellingDetail["LOC_CITYSTATEZIP"].ToString() + DwellingDetail["INFLATION_PRECENT"].ToString()) +"</PREMISESDESCRIPTION>"; 
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<OCCUPANCY " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["OCCUPANCY"].ToString())+"</OCCUPANCY>"; 
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<CONSTRUCTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["CONSTRUCTION_TYPE"].ToString())+"</CONSTRUCTION>";
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<YEARCONSTRUCTION  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["YEAR_BUILT"].ToString())+"</YEARCONSTRUCTION>";
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<MARKETVALUE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetIntFormatForHome(DwellingDetail["MARKET_VALUE"].ToString()))+"</MARKETVALUE>";
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<COUNTY  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["COUNTY"].ToString()) +"</COUNTY>";
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<FIREHYDRANT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["HYDRANTDEC"].ToString() + " FT")+"</FIREHYDRANT>";
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<FIRESTATION  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["FIRE_STATION_DIST"].ToString() + " Miles" )+"</FIRESTATION>";
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<PROTECTIONCLASS  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["PROT_CLASS"].ToString())+"</PROTECTIONCLASS>";
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<TERRITORY  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["LOC_CITY"].ToString())+"</TERRITORY>";
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<INFLATIONPERCENTAGE  " + fieldType +"=\""+ fieldTypeText +"\">"+""+"</INFLATIONPERCENTAGE>";
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<INSURANCESCORE " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(DwellingDetail["CUSTOMER_INSURANCE_SCORE"].ToString()) +"</INSURANCESCORE>";
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<INSURANCEPERCENTAGE " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(inspercent) +"</INSURANCEPERCENTAGE>";
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<PERCENTAGETYPE " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(DwellingDetail["CUSTOMER_INSURANCE_SCORE_TYPE"].ToString()) +"</PERCENTAGETYPE>";
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<TOTAL_ANNUAL_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">$"+ RemoveJunkXmlCharacters(DollarFormat(sumTtl)) +"</TOTAL_ANNUAL_PREMIUM>";
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<ADDITIONAL_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(adjPrm) +"</ADDITIONAL_PREMIUM>";
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<TOTAL_LOCATION_PREMIUM " + fieldType +"=\""+ fieldTypeText +"\">$"+ RemoveJunkXmlCharacters(DollarFormat(RskSumTtl)) +"</TOTAL_LOCATION_PREMIUM>";					
					DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml +  "<SIGNATUREDATE  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(System.DateTime.Today.ToString("MM/dd/yyyy") )+"</SIGNATUREDATE>";
					DecPageHomeElement.InnerXml = DecPageHomeElement.InnerXml +  "<ALL_POLICY_TYPE " + fieldType + "=\"" + fieldTypeText + "\">" +  RemoveJunkXmlCharacters(DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_TYPE"].ToString()) + "</ALL_POLICY_TYPE>";
			
					#endregion
				}	
				else if (gStrPdfFor == PDFForAcord)
				{
					#region Dwelling Element for Accord 80	
					Acord80DwellMultipleElement.AppendChild(Acord80HomeElement);
					Acord80HomeElement.SetAttribute(fieldType,fieldTypeNormal);
					Acord80HomeElement.SetAttribute(id,DwellingCtr.ToString());

					string dwellingID=	DwellingDetail["DWELLING_ID"].ToString();
			
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<HOFORM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["HO_FORM"].ToString()) +"</HOFORM>"; 
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<ENDORSEMENTDESC " + fieldType +"=\""+ fieldTypeText +"\">See Homeowners Application Supplement</ENDORSEMENTDESC>"; 
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<DEPOSITAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["RECEIVED_PRMIUM"].ToString()) +"</DEPOSITAMOUNT>"; 
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<ESTIMATEDTOTALPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">$"+ (DollarFormat(sumTtl)) +"</ESTIMATEDTOTALPREMIUM>"; 
					if (sumTtl!=0)
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<BALANCEAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+ (sumTtl - (DwellingDetail["RECEIVED_PRMIUM"].ToString()==""?0:double.Parse(DwellingDetail["RECEIVED_PRMIUM"].ToString()) ) ) + ".00" +"</BALANCEAMOUNT>"; 
					
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<CONSTRUCTIONCODE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["EXTERIOR_CONSTRUCTION"].ToString()) +"</CONSTRUCTIONCODE>"; 
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<EXTERIOR_CONSTRUCTION_DESC " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["EXTERIOR_CONSTRUCTION_DESC"].ToString()) +"</EXTERIOR_CONSTRUCTION_DESC>"; 
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<YRBUILT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["YEAR_BUILT"].ToString()) +"</YRBUILT>"; 
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<MARKETVALUE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["MARKET_VALUE"].ToString()) +"</MARKETVALUE>"; 
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<NOAPTS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["NEED_OF_UNITS"].ToString()) +"</NOAPTS>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<REPLACEMENTCOST " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["REPLACEMENT_COST"].ToString()) +"</REPLACEMENTCOST>";	
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<CONSTRUCTIONTYPE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["STRUCTURE_TYPE"].ToString()) +"</CONSTRUCTIONTYPE>";	
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<USAGETYPE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["USAGE_TYPE"].ToString()) +"</USAGETYPE>";	
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<FARM " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["ANY_FORMING"].ToString()) +"</FARM>";	
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<BUILDERSRISK " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["COC"].ToString()) +"</BUILDERSRISK>";	
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<COMPDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["COMP_DATE"].ToString()) +"</COMPDATE>";	
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<NOFAMILIES " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["NO_OF_FAMILIES"].ToString()) +"</NOFAMILIES>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PURCHASEDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["PUR_YR_PRICE"].ToString()) +"</PURCHASEDATE>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PROTECTIONCLASS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["PROT_CLASS"].ToString()) +"</PROTECTIONCLASS>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<DISTANCEHYDRANT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["HYDRANTDEC"].ToString()) +"</DISTANCEHYDRANT>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<DISTANCEFIRESTATION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["FIRE_STATION_DIST"].ToString()) +"</DISTANCEFIRESTATION>";
					if(DwellingDetail["CENT_ST_BURG_FIRE"].ToString()=="Y" || DwellingDetail["CENT_ST_FIRE"].ToString()=="Y")
					{
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PROTECTIONTYPESMOKECENTRAL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 1 +"</PROTECTIONTYPESMOKECENTRAL>";
					}	
					else
					{
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PROTECTIONTYPESMOKECENTRAL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 0 +"</PROTECTIONTYPESMOKECENTRAL>";
					}	

					if(DwellingDetail["CENT_ST_BURG_FIRE"].ToString()=="Y" || DwellingDetail["CENT_ST_BURG"].ToString()=="Y")
					{
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PROTECTIONTYPEBURGLARCENTRAL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 1 +"</PROTECTIONTYPEBURGLARCENTRAL>";
					}	
					else
					{
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PROTECTIONTYPEBURGLARCENTRAL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 0 +"</PROTECTIONTYPEBURGLARCENTRAL>";
					}	

					if(DwellingDetail["DIR_FIRE_AND_POLICE"].ToString()=="Y" || DwellingDetail["DIR_FIRE"].ToString()=="Y")
					{
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PROTECTIONTYPESMOKEDIRECT " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 1 +"</PROTECTIONTYPESMOKEDIRECT>";
					}	
					else
					{
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PROTECTIONTYPESMOKEDIRECT " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 0 +"</PROTECTIONTYPESMOKEDIRECT>";
					}	
					if(DwellingDetail["DIR_FIRE_AND_POLICE"].ToString()=="Y" || DwellingDetail["DIR_POLICE"].ToString()=="Y")
					{
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PROTECTIONTYPEBURGLARDIRECT " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 1 +"</PROTECTIONTYPEBURGLARDIRECT>";
					}	
					else
					{
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PROTECTIONTYPEBURGLARDIRECT " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 0 +"</PROTECTIONTYPEBURGLARDIRECT>";
					}	

					if(DwellingDetail["LOC_FIRE_GAS"].ToString()=="Y" || DwellingDetail["TWO_MORE_FIRE"].ToString()=="Y")
					{
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PROTECTIONTYPESMOKELOCAL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 1 +"</PROTECTIONTYPESMOKELOCAL>";
					}	
					else
					{
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PROTECTIONTYPESMOKELOCAL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ 0 +"</PROTECTIONTYPESMOKELOCAL>";
					}	
				
					if(DwellingDetail["PRIMARY_HEAT_OTHER_DESC"].ToString().Length <= 0)
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PRIMARYHEATTYPE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["PHEAT_TYPE"].ToString()) +"</PRIMARYHEATTYPE>";	
					else
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PRIMARYHEATTYPE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["PRIMARY_HEAT_OTHER_DESC"].ToString()) +"</PRIMARYHEATTYPE>";	

					if(DwellingDetail["SECONDARY_HEAT_OTHER_DESC"].ToString().Length <= 0)
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<SECONDARYHEATTYPE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["SHEAT_TYPE"].ToString()) +"</SECONDARYHEATTYPE>";
					else
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<SECONDARYHEATTYPE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["SECONDARY_HEAT_OTHER_DESC"].ToString()) +"</SECONDARYHEATTYPE>";

					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<RENOVATIONTYPEPARTWIRING " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["WIRING"].ToString()) +"</RENOVATIONTYPEPARTWIRING>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<RENOVATIONTYPEWIRINGYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["WIRING_UPDATE_YEAR"].ToString()) +"</RENOVATIONTYPEWIRINGYEAR>";

					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<RENOVATIONTYPEPARTPLUMBING " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["PLUMBING"].ToString()) +"</RENOVATIONTYPEPARTPLUMBING>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<RENOVATIONTYPEPLUMBINGYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["PLUMBING_UPDATE_YEAR"].ToString()) +"</RENOVATIONTYPEPLUMBINGYEAR>";

					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<RENOVATIONTYPEPARTHEATING " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["HEATING"].ToString()) +"</RENOVATIONTYPEPARTHEATING>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<RENOVATIONTYPEHEATINGYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["HEATING_UPDATE_YEAR"].ToString()) +"</RENOVATIONTYPEHEATINGYEAR>";

					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<RENOVATIONTYPEPARTROOFING " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["ROOFING"].ToString()) +"</RENOVATIONTYPEPARTROOFING>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<RENOVATIONTYPEROOFINGYEAR " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["ROOFING_UPDATE_YEAR"].ToString()) +"</RENOVATIONTYPEROOFINGYEAR>";

					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<NOAMPS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["NO_OF_AMPS"].ToString()) +"</NOAMPS>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<CIRCUITBREAKERS " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["CIRCUIT_BREAKERS"].ToString()) +"</CIRCUITBREAKERS>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<FOUNDATIONOPEN " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["FOUNDATION"].ToString()) +"</FOUNDATIONOPEN>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<OCCUPANCY_CODE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["OCCUPANCY_CODE"].ToString()) +"</OCCUPANCY_CODE>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<NOWEEKSRENTED " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(DwellingDetail["NO_WEEKS_RENTED"].ToString()) +"</NOWEEKSRENTED>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<VISIBLETONEIGHBOURS " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["NEIGHBOURS_VISIBLE"].ToString()) +"</VISIBLETONEIGHBOURS>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<ISSWIMMINGPOOL " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["SWIMMING_POOL"].ToString()) +"</ISSWIMMINGPOOL>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<SWIMMINGPOOLPOSITION " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["SWIMMING_POOL_TYPE"].ToString()) +"</SWIMMINGPOOLPOSITION>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<SWIMMINGPOOLAPPROVEDFENCE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["APPROVED_FENCE"].ToString()) +"</SWIMMINGPOOLAPPROVEDFENCE>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<SWIMMINGPOOLDIVINGBOARD " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["DIVING_BOARD"].ToString()) +"</SWIMMINGPOOLDIVINGBOARD>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<SWIMMINGPOOLSLICE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["SLIDE"].ToString()) +"</SWIMMINGPOOLSLICE>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<INSPECTED " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["INSPECTED"].ToString()) +"</INSPECTED>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<OCCUPIEDDAILY " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["OCCUPIED_DAILY"].ToString()) +"</OCCUPIEDDAILY>";
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<TERRCODE " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(DwellingDetail["TERR"].ToString()) +"</TERRCODE>";
					string premgrp = "";
					foreach (XmlNode PremiumNode in GetRentalPremGroup())
					{
						premgrp=getAttributeValue(PremiumNode,"GROUPDESC").ToString() ;
						if(premgrp.Length>0 )
						{
							premgrp = premgrp.Replace("Premium Group:","");
							if(premgrp.IndexOf(",") > 1)
								premgrp = premgrp.Substring(1, premgrp.IndexOf(",")-1);
							else
								premgrp = premgrp.Substring(1, premgrp.Length - 1);
							break;
						}
					}
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PREMGROUP " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ RemoveJunkXmlCharacters(premgrp)  +"</PREMGROUP>";

					if(DwellingDetail["LOOKUP_VALUE_DESC"].ToString()!="")
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<ROOFMATERIAL " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(DwellingDetail["LOOKUP_VALUE_DESC"].ToString())  +"</ROOFMATERIAL>";					
					else
						Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<ROOFMATERIAL " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(DwellingDetail["ROOF_OTHER_DESC"].ToString())  +"</ROOFMATERIAL>";

				
					Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<SPRINKLER " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+ RemoveJunkXmlCharacters(DwellingDetail["SPRINKER"].ToString())  +"</SPRINKLER>";
					#endregion

					#region Dwelling Element for Supplement	
					SupplementDwellMultipleElement.AppendChild(SupplementHomeElement);
					SupplementHomeElement.SetAttribute(fieldType,fieldTypeNormal);
					SupplementHomeElement.SetAttribute(id,DwellingCtr.ToString());

					SupplementHomeElement.InnerXml= SupplementHomeElement.InnerXml + "<APPLICANTNAME " + fieldType +"=\""+ fieldTypeText +"\">"+ ApplicantName +"</APPLICANTNAME>";
					SupplementHomeElement.InnerXml= SupplementHomeElement.InnerXml + "<APPLICANTNO " + fieldType +"=\""+ fieldTypeText +"\">"+ PolicyNumber +"</APPLICANTNO>";
					SupplementHomeElement.InnerXml= SupplementHomeElement.InnerXml + "<INSURANCESCORE " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(DwellingDetail["CUSTOMER_INSURANCE_SCORE"].ToString()) +"</INSURANCESCORE>";
					SupplementHomeElement.InnerXml= SupplementHomeElement.InnerXml + "<INSURANCEPERCENTAGE " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(inspercent) +"</INSURANCEPERCENTAGE>";
					SupplementHomeElement.InnerXml= SupplementHomeElement.InnerXml + "<ENCLOSEDSWIMMINGPOOL " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(DwellingDetail["IS_SWIMPOLL_HOTTUB"].ToString()) +"</ENCLOSEDSWIMMINGPOOL>";
					SupplementHomeElement.InnerXml= SupplementHomeElement.InnerXml +  "<PERCENTAGETYPE " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(DwellingDetail["CUSTOMER_INSURANCE_SCORE_TYPE"].ToString()) +"</PERCENTAGETYPE>";
					SupplementHomeElement.InnerXml= SupplementHomeElement.InnerXml +  "<insuranceCredit " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(sumtotal.ToString()) +"</insuranceCredit>";
					#endregion
				}
				#endregion

				#region Coverages
				//DataSet DSTempDwelling = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeOwner_Coverage_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + DwellingDetail["DWELLING_ID"] +  ",'" + gStrCalledFrom + "'");
				//double dblSumTotal=0;
				//int RowCounter=0;
				//double red_covc = 0.00;
				string strDec="",CovLimit="",strPrem="";
				
				foreach(DataRow CoverageDetails in DSTempDwelling.Tables[0].Rows)
				{
					#region Dec Page Coverages
					string CovCode = CoverageDetails["COV_CODE"].ToString();
					//formating for Deductible
					strDec = CoverageDetails["DEDUCTIBLE"].ToString();
					if(strDec=="0" || strDec=="$0.00" || strDec=="$0" || strDec=="0.00" || strDec=="")
						strDec="";
					else									
						strDec = GetIntFormat(strDec);

					// formating for Limit
					CovLimit = CoverageDetails["LIMIT_1"].ToString().Trim();
					if(CovLimit!="0 Extended From Primary")
					{
						if(CoverageDetails["LIMIT_2"].ToString() != "" && CovLimit != ""  && CovLimit != "0" && CoverageDetails["LIMIT_2"].ToString() != "0" && CoverageDetails["LIMIT_2"].ToString() != "0.00")
						{
							CovLimit=GetIntFormat(CovLimit);
							//CovLimit += "/" + GetIntFormat(CoverageDetails["LIMIT_2"].ToString());										
						}
						else
						{
							//if(CoverageDetails["LIMIT_2"].ToString()!="" && CoverageDetails["LIMIT_2"].ToString() != "0" && CoverageDetails["LIMIT_2"].ToString() != "0.00")
							//	CovLimit = System.Convert.ToString(int.Parse(CovLimit) +  int.Parse(CoverageDetails["LIMIT_2"].ToString()));
							CovLimit=GetIntFormat(CovLimit);
						}
					}
					else if(CovLimit=="0 Extended From Primary" || CovLimit=="Extended From Primary")
					{
						CovLimit=CovLimit.Replace("0","").Trim();
					}
					if(CovLimit=="$0" || CovLimit=="$0/$0" || CovLimit=="0")
						CovLimit="";
					// formating for Premium
					if(gStrtemp == "temp")
					{
						strPrem=GetPremiumBeforeCommit(DSTempDwelling,CovCode,htpremium);
						// Add property expense fee to base premium
						if(CovCode=="DWELL" && (strPrem!="" && strPrem!="0") && (strProprtyFee!="" && strProprtyFee!="0") && DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_TYPE"].ToString()!="HO-4 Tenants" && DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_TYPE"].ToString()!="HO-6 Unit Owners")
						{
							if(strPrem.IndexOf(".00")>=0)
								strPrem=strPrem.Replace(".00","");
							strPrem = System.Convert.ToString(int.Parse(strPrem) + int.Parse(strProprtyFee));
							strPrem =strPrem +".00";
						}
						else if(CovCode=="EBUSPP" && (strPrem!="" && strPrem!="0") && (strProprtyFee!="" && strProprtyFee!="0") && DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_TYPE"].ToString()=="HO-4 Tenants" && DSTempPolicyDataSet.Tables[0].Rows[0]["POLICY_TYPE"].ToString()=="HO-6 Unit Owners")
						{
							if(strPrem.IndexOf(".00")>=0)
								strPrem=strPrem.Replace(".00","");
							strPrem = System.Convert.ToString(int.Parse(strPrem) + int.Parse(strProprtyFee));
							strPrem =strPrem +".00";
						}
						if(strPrem !="" && strPrem !="0.00" && strPrem !="0" && strPrem !="Extended")
						{
							strPrem = "$" + strPrem;
						}
						else if(strPrem !="Extended")
						{
							strPrem = "Included";
						}
					}
					else
					{
						strPrem=GetPremium(DSTempDwelling,CovCode);
						if(strPrem !="" && strPrem !="0.00" && strPrem !="0" && strPrem !="Extended")
						{
							strPrem = "$" + strPrem;
						}
						else if(strPrem !="Extended")
						{
							strPrem = "Included";
						}
					}
					switch(CoverageDetails["COV_CODE"].ToString())
					{
						case "DWELL":
							if (gStrPdfFor == PDFForDecPage)
							{
								DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml + "<PC_COVERAGEADWELLING " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</PC_COVERAGEADWELLING>";
								DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml + "<PC_COVERAGEADWELLINGLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</PC_COVERAGEADWELLINGLIMIT>";
								DecPageHomeElement.InnerXml += "<PC_COVERAGEADWELLINGPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</PC_COVERAGEADWELLINGPREMIUM>";
							}
							else if (gStrPdfFor == PDFForAcord)
								Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<DWELLINGAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString()) +"</DWELLINGAMOUNT>"; 
							break;
						case "OS":
							if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
								continue;
							if (gStrPdfFor == PDFForDecPage)
							{
								DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml + "<PC_COVERAGEBOTHERSTRUCTURES " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</PC_COVERAGEBOTHERSTRUCTURES>";
								DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml + "<PC_COVERAGEBOTHERSTRUCTURESLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</PC_COVERAGEBOTHERSTRUCTURESLIMIT>";
								DecPageHomeElement.InnerXml += "<PC_COVERAGEBOTHERSTRUCTURESPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</PC_COVERAGEBOTHERSTRUCTURESPREMIUM>";
							}
							else if (gStrPdfFor == PDFForAcord)
								Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<OTHERSTRUCTURESAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit) +"</OTHERSTRUCTURESAMOUNT>"; 
							break;
						case "EBUSPP":
							if (gStrPdfFor == PDFForDecPage)
							{
								DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml + "<PC_COVERAGECUNSCHEDULEDPERSONALPROPERTY " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</PC_COVERAGECUNSCHEDULEDPERSONALPROPERTY>";
								DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml + "<PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYLIMIT>";
								DecPageHomeElement.InnerXml += "<PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</PC_COVERAGECUNSCHEDULEDPERSONALPROPERTYPREMIUM>";
							}
							else if (gStrPdfFor == PDFForAcord)
								Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PERSONALPROPERTYAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit) +"</PERSONALPROPERTYAMOUNT>"; 
							break;
						case "LOSUR":
							if (gStrPdfFor == PDFForDecPage)
							{
								DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml + "<PC_COVERAGEDLOSSOFUSE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</PC_COVERAGEDLOSSOFUSE>";
								DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml + "<PC_COVERAGEDLOSSOFUSELIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</PC_COVERAGEDLOSSOFUSELIMIT>";
								DecPageHomeElement.InnerXml += "<PC_COVERAGEDLOSSOFUSEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</PC_COVERAGEDLOSSOFUSEPREMIUM>";
							}
							else if (gStrPdfFor == PDFForAcord)
								Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<LOSSOFUSEAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit) +"</LOSSOFUSEAMOUNT>"; 
							break;
						case "PL":
							if (gStrPdfFor == PDFForDecPage)
							{
								strRVLiabLim = CoverageDetails["LIMIT_1"].ToString();
								DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml + "<PERSONALLIABILITYLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</PERSONALLIABILITYLIMIT>";							
								if(CovLimit=="Extended From Primary")
									DecPageHomeElement.InnerXml += "<PERSONALLIABILITYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+"N/A"+"</PERSONALLIABILITYPREMIUM>";
								else// if(strPrem!="Included")
									DecPageHomeElement.InnerXml += "<PERSONALLIABILITYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</PERSONALLIABILITYPREMIUM>";
								//else
									//DecPageHomeElement.InnerXml += "<PERSONALLIABILITYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+""+"</PERSONALLIABILITYPREMIUM>";
							}
							else if (gStrPdfFor == PDFForAcord)
								Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<PERSONALLIABILITY " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit) +"</PERSONALLIABILITY>"; 
							break;
						case "MEDPM":
							if (gStrPdfFor == PDFForDecPage)
							{
								strRVMedLim = CoverageDetails["LIMIT_1"].ToString();
								DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml + "<MEDICALPAYMENTSLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</MEDICALPAYMENTSLIMIT>";							
								if(CovLimit=="Extended From Primary")
									DecPageHomeElement.InnerXml += "<MEDICALPAYMENTSPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</MEDICALPAYMENTSPREMIUM>";
								else //if(strPrem!="Included")
									DecPageHomeElement.InnerXml += "<MEDICALPAYMENTSPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</MEDICALPAYMENTSPREMIUM>";
								//se
									//cPageHomeElement.InnerXml += "<MEDICALPAYMENTSPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+""+"</MEDICALPAYMENTSPREMIUM>";
							}
							else if (gStrPdfFor == PDFForAcord)
								Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<MEDICALPAYMENTSAMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit) +"</MEDICALPAYMENTSAMOUNT>"; 
							break;						
						case "APD":
							if (gStrPdfFor == PDFForDecPage)
								DecPageHomeElement.InnerXml= DecPageHomeElement.InnerXml + "<PC_COVERAGEALLPERILDEDUC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</PC_COVERAGEALLPERILDEDUC>";
							if (gStrPdfFor == PDFForAcord)
							{
								if(CoverageDetails["DEDUCTIBLE"]!=null && CoverageDetails["DEDUCTIBLE"].ToString()!="" )
									if(double.Parse(CoverageDetails["DEDUCTIBLE"].ToString()) > 0.00 )
									{
										Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<ALL_PERIL_CHECK " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+1+"</ALL_PERIL_CHECK>"; 
										Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<ALL_PERIL_AMOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(strDec) +"</ALL_PERIL_AMOUNT>"; 
									}
									else
										Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<ALL_PERIL_CHECK " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+0.00+"</ALL_PERIL_CHECK>"; 
							}
							break;		
						case "EBEP11":
							if (gStrPdfFor == PDFForAcord)
								Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<REPLACEMENTCOSTDWELLING " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+1+"</REPLACEMENTCOSTDWELLING>"; 
							break;
						case "EBRCPP":
							if (gStrPdfFor == PDFForAcord)
								Acord80HomeElement.InnerXml= Acord80HomeElement.InnerXml +  "<REPLACEMENTCOSTCONTENTS " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+1+"</REPLACEMENTCOSTCONTENTS>"; 
							break;

					}
					#endregion
				}
				#endregion

				#region Endorsements
				XmlElement DecPageHomeEndmts;
				DecPageHomeEndmts= AcordPDFXML.CreateElement("ENDORSEMENTS");

				XmlElement SupplementHomeEndmts;
				SupplementHomeEndmts= AcordPDFXML.CreateElement("ENDORSEMENTS");

				
							

				if (gStrPdfFor == PDFForDecPage)
				{
					#region Declaration Page Dwelling Endorsements
					DecPageHomeElement.AppendChild(DecPageHomeEndmts);
					DecPageHomeEndmts.SetAttribute(fieldType,fieldTypeMultiple);
					DecPageHomeEndmts.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHOMEEND"));
					DecPageHomeEndmts.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHOMEEND"));
					DecPageHomeEndmts.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHOMEENDEXTN"));
					DecPageHomeEndmts.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHOMEENDEXTN"));
					#endregion
				}
				else if (gStrPdfFor == PDFForAcord)
				{
					#region Supplemental Page Dwelling Endorsements
					SupplementHomeElement.AppendChild(SupplementHomeEndmts);
					SupplementHomeEndmts.SetAttribute(fieldType,fieldTypeMultiple);
					SupplementHomeEndmts.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTHOMEEND"));
					SupplementHomeEndmts.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTHOMEEND"));
					SupplementHomeEndmts.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTHOMEENDEXTN"));
					SupplementHomeEndmts.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTHOMEENDEXTN"));
					#endregion
				}	
				//int endCtr=0;
				int DecPageEndCtr=1;
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
					}
				}
				foreach(DataRow CoverageDetails in DSTempDwelling.Tables[0].Rows)
				{
					string CovCode = CoverageDetails["COV_CODE"].ToString();
					if (CovCode!="DWELL" && CovCode!="OS" && CovCode!="EBUSPP" && CovCode!="LOSUR" && CovCode!="PL" && CovCode!="MEDPM" && CovCode!="APD" && CovCode!="PFE")
					{	
						#region DECPAGE
						if(gStrPdfFor.Equals(PDFForDecPage))
						{
							//if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							//{
								XmlElement DecPageDwellEndmtElement;
								DecPageDwellEndmtElement = AcordPDFXML.CreateElement("ENDORSEMENTSINFO");
								DecPageHomeEndmts.AppendChild(DecPageDwellEndmtElement);
								DecPageDwellEndmtElement.SetAttribute(fieldType,fieldTypeNormal);
								DecPageDwellEndmtElement.SetAttribute(id,(DecPageEndCtr-1).ToString() );

								// Policy Holder details(Start)
								DecPageDwellEndmtElement.InnerXml   = DecPageDwellEndmtElement.InnerXml +  "<PRIMARYCONTACTNAME " + fieldType + "=\"" + fieldTypeText + "\">" + ApplicantName + "</PRIMARYCONTACTNAME>";
								DecPageDwellEndmtElement.InnerXml += "<POLICYNUMBER " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyNumber + "</POLICYNUMBER>";
								DecPageDwellEndmtElement.InnerXml += "<POLICYVERSION " + fieldType + "=\"" + fieldTypeText + "\">" + gStrPolicyVersion + "</POLICYVERSION>";
								DecPageDwellEndmtElement.InnerXml += "<POLICYEFFDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyEffDate + "</POLICYEFFDATE>";
								DecPageDwellEndmtElement.InnerXml += "<POLICYEXPDATE " + fieldType + "=\"" + fieldTypeText + "\">" + PolicyExpDate + "</POLICYEXPDATE>";
								// Policy Holder details (End)
							

								//formating for Deductible
								strDec = CoverageDetails["DEDUCTIBLE"].ToString();
								if(strDec=="0" || strDec=="$0.00" || strDec=="$0" || strDec=="0.00" || strDec=="")
									strDec="N/A";
								else									
									strDec = GetIntFormat(strDec);

								// formating for Limit
								CovLimit = CoverageDetails["LIMIT_1"].ToString().Trim();
								if(CoverageDetails["LIMIT_2"].ToString() != "" && CovLimit != ""  && CovLimit != "0" && CoverageDetails["LIMIT_2"].ToString() != "0" && CoverageDetails["LIMIT_2"].ToString() != "0.00")
								{
									CovLimit=GetIntFormat(CovLimit);
									//CovLimit += "/" + GetIntFormat(CoverageDetails["LIMIT_2"].ToString());										
								}
								else
								{
									//if(CoverageDetails["LIMIT_2"].ToString()!="" && CoverageDetails["LIMIT_2"].ToString() != "0" && CoverageDetails["LIMIT_2"].ToString() != "0.00")
										//CovLimit = System.Convert.ToString(int.Parse(CovLimit) + int.Parse(CoverageDetails["LIMIT_2"].ToString()));
									CovLimit=GetIntFormat(CovLimit);
								}
								if(CovLimit=="$0" || CovLimit=="$0/$0" || CovLimit=="0" )
									CovLimit="";
								if(CovLimit=="")
									CovLimit="Included";
								// formating for Premium
								if(gStrtemp == "temp")
								{
									strPrem=GetPremiumBeforeCommit(DSTempDwelling,CovCode,htpremium);
									if(strPrem !="")
									{
										strPrem = "$" + strPrem;
									}
									else
									{
										strPrem = "Included";
									}
								}
								else
								{
									strPrem=GetPremium(DSTempDwelling,CovCode);
									if(strPrem !="" && strPrem!="Applied" && strPrem!="Included")
									{
										strPrem = "$" + strPrem;
									}
									else
									{
										strPrem = "Included";
									}
								}

								#region SWITCH CASE FOR DEC PAGE (Cov Code)
								switch(CovCode)
								{
									case	"BPCES"	:
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
								
										if (gStrPdfFor == PDFForDecPage)
										{
											double HO71_prem = 0;
											try {HO71_prem	=	double.Parse(htpremium["CLASS_A"].ToString());}	
											catch (Exception ex)
                                            { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex); }
											try {HO71_prem	+=	double.Parse(htpremium["CLASS_B"].ToString());}	
											catch (Exception ex)
                                            { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex); }
											try {HO71_prem	+=	double.Parse(htpremium["CLASS_C"].ToString());}	
											catch (Exception ex)
                                            { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex); }
											try {HO71_prem	+=	double.Parse(htpremium["CLASS_D"].ToString());}	
											catch (Exception ex)
                                            { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex); }
											if(HO71_prem != 0)
											{
												gstrGetPremium = HO71_prem.ToString();
												gintGetindex	=	gstrGetPremium.IndexOf(".");
												if(gintGetindex==	-1)		
													DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
												else
													DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
											}
											else
											{
												DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";

											}
											DecPageEndCtr++;
										}
										break;
									case "HO61":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "EBP20":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "EBP22":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "EBRCPP":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "HO214":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "HO216":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "HO300":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "SEWER":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "LLE382":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "FRAUD":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "WBSPO":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "EROK":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										if(stCode == "MI")
											DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("10% - $250")+"</HM_ENDDEDUCTIBLE>";
										else
											DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "HO9":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "EBIF96":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "EBOS40":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
										// Other Structure On Premises Rented to Others  and Repair Cast AND replacement cast  
										foreach(DataRow row in DSTempOtherStructureDetails.Tables[0].Rows)
										{
											if(row["PREMISES_LOCATION"].ToString().ToUpper().Trim()=="ON-PREMISES/RENTED-TO-OTHERS" && (row["COVERAGE_BASIS"].ToString().ToUpper().Trim()=="REPAIR" || row["COVERAGE_BASIS"].ToString().ToUpper().Trim()=="REPLACEMENT") && row["SATELLITE_EQUIPMENT"].ToString()!="10963")
											{
												
												DecPageDwellEndmtElement = AcordPDFXML.CreateElement("ENDORSEMENTSINFO");
												DecPageHomeEndmts.AppendChild(DecPageDwellEndmtElement);
												DecPageDwellEndmtElement.SetAttribute(fieldType,fieldTypeNormal);
												DecPageDwellEndmtElement.SetAttribute(id,(DecPageEndCtr-1).ToString() );

												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+"    -"+RemoveJunkXmlCharacters(row["PREMISES_DESCRIPTION"].ToString())+"</HM_ENDDESCRIPTION>";
												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</HM_ENDEDITIONDATE>";
												if(row["INSURING_VALUE"].ToString()!=null && row["INSURING_VALUE"].ToString()!="")
													DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetIntFormatForHome(row["INSURING_VALUE"].ToString()))+"</HM_ENDLIMIT>";
												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</HM_ENDDEDUCTIBLE>";
												DecPageEndCtr++;
											}
											
										}
									}
										break;
									case "EBOS489":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
										// Other Structure On Premises With satelite to no and Repair Cast  
										foreach(DataRow row in DSTempOtherStructureDetails.Tables[0].Rows)
										{
											if(row["PREMISES_LOCATION"].ToString().ToUpper().Trim()=="ON-PREMISES" && row["COVERAGE_BASIS"].ToString().ToUpper().Trim()=="REPAIR" && row["SATELLITE_EQUIPMENT"].ToString()!="10963")
											{
												
												DecPageDwellEndmtElement = AcordPDFXML.CreateElement("ENDORSEMENTSINFO");
												DecPageHomeEndmts.AppendChild(DecPageDwellEndmtElement);
												DecPageDwellEndmtElement.SetAttribute(fieldType,fieldTypeNormal);
												DecPageDwellEndmtElement.SetAttribute(id,(DecPageEndCtr-1).ToString() );

												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+"    -"+RemoveJunkXmlCharacters(row["PREMISES_DESCRIPTION"].ToString())+"</HM_ENDDESCRIPTION>";
												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</HM_ENDEDITIONDATE>";
												if(row["INSURING_VALUE"].ToString()!=null && row["INSURING_VALUE"].ToString()!="")
													DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetIntFormatForHome(row["INSURING_VALUE"].ToString()))+"</HM_ENDLIMIT>";
												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</HM_ENDDEDUCTIBLE>";
												DecPageEndCtr++;
											}
											
										}

									}
										break;
									case "EBDUC":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "EBPPOP":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "EBCCSL":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "EBCCSM":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "ESCCSS":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "EBCCSI":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "EBCCSF":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "IBUSP":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "IBUSPA":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "IBUSPO":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "IBUSPOA":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "EBICC53":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "EBSS490":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
										// Other Structure On Premises With satelite to no and Repair Cast and Replacement Cast 
										foreach(DataRow row in DSTempOtherStructureDetails.Tables[0].Rows)
										{
											if(row["PREMISES_LOCATION"].ToString().ToUpper().Trim()=="OFF-PREMISES" && (row["COVERAGE_BASIS"].ToString().ToUpper().Trim()=="REPAIR" || row["COVERAGE_BASIS"].ToString().ToUpper().Trim()=="REPLACEMENT") && row["SATELLITE_EQUIPMENT"].ToString()!="10963")
											{
												
												DecPageDwellEndmtElement = AcordPDFXML.CreateElement("ENDORSEMENTSINFO");
												DecPageHomeEndmts.AppendChild(DecPageDwellEndmtElement);
												DecPageDwellEndmtElement.SetAttribute(fieldType,fieldTypeNormal);
												DecPageDwellEndmtElement.SetAttribute(id,(DecPageEndCtr-1).ToString() );

												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+"    -"+RemoveJunkXmlCharacters(row["PREMISES_DESCRIPTION"].ToString())+"</HM_ENDDESCRIPTION>";
												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</HM_ENDEDITIONDATE>";
												if(row["INSURING_VALUE"].ToString()!=null && row["INSURING_VALUE"].ToString()!="")
													DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetIntFormatForHome(row["INSURING_VALUE"].ToString()))+"</HM_ENDLIMIT>";
												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</HM_ENDDEDUCTIBLE>";
												DecPageEndCtr++;
											}
											
										}

									}
										break;
									case "EBAIRP":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "REDUC":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "OSTDISH":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "BUMC":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "LF330":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "SP350":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "CWC373":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "SPP900":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "HO864":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "IFGHO":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "HO100":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "PFBF2":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "WP865":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "NSPC220":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "HO417":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "HO48":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
										// Other Structure On Premises With satelite to no and Replacement Cast  
										foreach(DataRow row in DSTempOtherStructureDetails.Tables[0].Rows)
										{
											if(row["PREMISES_LOCATION"].ToString().ToUpper().Trim()=="ON-PREMISES" && row["COVERAGE_BASIS"].ToString().ToUpper().Trim()=="REPLACEMENT" && row["SATELLITE_EQUIPMENT"].ToString()!="10963")
											{
												
												DecPageDwellEndmtElement = AcordPDFXML.CreateElement("ENDORSEMENTSINFO");
												DecPageHomeEndmts.AppendChild(DecPageDwellEndmtElement);
												DecPageDwellEndmtElement.SetAttribute(fieldType,fieldTypeNormal);
												DecPageDwellEndmtElement.SetAttribute(id,(DecPageEndCtr-1).ToString() );

												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+"    -"+RemoveJunkXmlCharacters(row["PREMISES_DESCRIPTION"].ToString())+"</HM_ENDDESCRIPTION>";
												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("")+"</HM_ENDEDITIONDATE>";
												if(row["INSURING_VALUE"].ToString()!=null && row["INSURING_VALUE"].ToString()!="")
													DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetIntFormatForHome(row["INSURING_VALUE"].ToString()))+"</HM_ENDLIMIT>";
												DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("N/A")+"</HM_ENDDEDUCTIBLE>";
												DecPageEndCtr++;
											}
											
										}
									}
										break;
									case "HO493":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "APOBI":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "APRPR":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "IOPSO":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "IOPSS":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "IOPSL":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "IOPSI":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "PERIJ":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "REEMN":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "BPSCM":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "BPTAL":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "BPTNO":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "FLIFP":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "FLOFO":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "FLOFR":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "EOP17":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "AROF1":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "AROF2":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									case "LAS360":
									{
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
										DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
										DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
										DecPageEndCtr++;
									}
										break;
									default:
									{
										if(CovCode!="APD" && CovCode!="PFE")
										{
											DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
											DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
											DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CovLimit)+"</HM_ENDLIMIT>";
											DecPageDwellEndmtElement.InnerXml= DecPageDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strDec)+"</HM_ENDDEDUCTIBLE>";
											DecPageDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strPrem)+"</HM_ENDPREMIUM>";
											DecPageEndCtr++;
										}
									}
										break;
								}
								#endregion
//							if(CovCode!="APD" && CovCode!="PFE" && CovCode!="EBOS489")
//							{
//								DecPageEndCtr++;
//							}
							//}
						}
						#endregion
	
						#region SUPPLEMENT PAGE
						if(gStrPdfFor.Equals(PDFForAcord) )
						{
							if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
							{
								XmlElement SupplementDwellEndmtElement;
								SupplementDwellEndmtElement = AcordPDFXML.CreateElement("ENDORSEMENTSINFO");
								SupplementHomeEndmts.AppendChild(SupplementDwellEndmtElement);
								SupplementDwellEndmtElement.SetAttribute(fieldType,fieldTypeNormal);
								SupplementDwellEndmtElement.SetAttribute(id,(DecPageEndCtr-1).ToString());

								if(gStrPdfFor == PDFForAcord)
								{
									if(CovCode!="APD" && CovCode!="PFE")
									{
										SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDFORM " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["FORM_NUMBER"].ToString()) +"</HM_ENDFORM>";							
										SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDEDITIONDATE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["EDITION_DATE"].ToString())+"</HM_ENDEDITIONDATE>";
										SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDESCRIPTION " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["COV_DES"].ToString())+"</HM_ENDDESCRIPTION>";
										if(CoverageDetails["LIMIT_1"].ToString().Trim() != "0.00")
											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDLIMIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["LIMIT_1"].ToString())+"</HM_ENDLIMIT>";
										if(CoverageDetails["DEDUCTIBLE"].ToString().Trim() != "0.00")
											SupplementDwellEndmtElement.InnerXml= SupplementDwellEndmtElement.InnerXml + "<HM_ENDDEDUCTIBLE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(CoverageDetails["DEDUCTIBLE"].ToString())+"</HM_ENDDEDUCTIBLE>";
										SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(GetPremium(DSTempDwelling, CovCode))+"</HM_ENDPREMIUM>";											
									}
								}
							

								#region SWITCH CASE FOR ACCORD
								switch(CoverageDetails["COV_CODE"].ToString())
								{
									case "APD":
										break;
									case "EBNBPL":
										break;
									case "WOODS":								
										break;
									case "EBRCPP":
									case "EBP20":
										break;
									case "EBP22":
										if (gStrPdfFor == PDFForDecPage)
										{
										}
										break;
									case "EBP24":
										if (gStrPdfFor == PDFForDecPage)
										{
										}
										break;
									case "EBEP11":
										if(htpremium.Contains("EXPND_RPLC_CST"))
										{
										}
										break;
									case	"EBIF96":
										if(htpremium.Contains("FIRE_DPT"))
										{
										}
										break;
									case	"IBUSP":
										if(htpremium.Contains("BSNS_PRP_INCR"))
										{
										}
										break;
									case	"EBOS40":
										if(htpremium.Contains("OTHR_STR_RNT"))
										{
										}
										break;
									case	"EBPPC":
										break;
									case	"EBPPOP":
										if(htpremium.Contains("PRS_PRP_AWY_PRM"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"EBALEXP":
										break;
									case	"EBOS489":
										if(htpremium.Contains("OTHR_STR_RPR_CST"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"EBICC53":
										if(htpremium.Contains("CRDT_CRD"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"EBCCSL":
										if (gStrPdfFor == PDFForDecPage)
										{
									
										}
										break;
									case	"SEWER":
										if(htpremium.Contains("ORD_LW"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"EROK":
										if(htpremium.Contains("ERTHQKE"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"FRAUD":
										if(htpremium.Contains("ID_FRAUD"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"EBDUC":								
										break;
									case	"EBCASP":
										if(htpremium.Contains("UNT_OWNR_CVG_A"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"EBUNIT":
										if(htpremium.Contains("CND_UNIT"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"LAC":
										break;
									case	"EBAIRP":	
										break;
									case	"EBBAA":
										if(htpremium.Contains("BLDG_ALTR"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"EBRDC":
										if(htpremium.Contains("RNT_DLX"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"EBCDC":
										if(htpremium.Contains("CND_DLX"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"EBMC":
										break;
									case	"RECST":								
										break;
									case	"MIN":								
										break;						
									case	"EBSS490":
										if(htpremium.Contains("SPCFC_STR_PRMS"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"EBCCSM":
										break;
									case	"ESCCSS":								
										break;
									case	"EBCCSI":
										break;
									case	"EBCCSF":								
										break;
									case	"WBSPO":
										if(htpremium.Contains("BK_UP_SM_PMP"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"APOBI":
										if(htpremium.Contains("ADDL_PRM_OCC_INS"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"APRPR":
										if(htpremium.Contains("ADDL_PRM_RNTD_OTH_RES"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"APOLR":
										if(htpremium.Contains("ADDL_PRM_RNTD_1FMLY"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"APOLO":
										if(htpremium.Contains("ADDL_PRM_RNTD_2FMLY"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;


									case	"IOPSS":
										if(htpremium.Contains("INCDT_OFCE_PRV_SCHL_PRM"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"IOPSL":
										break;
									case	"IOPSI":
										if(htpremium.Contains("INCDT_OFCE_PRV_SCHL_INST"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"IOPSO":
										if(htpremium.Contains("INCDT_OFCE_PRV_SCHL_OFF_PRM"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"PERIJ":
										if(htpremium.Contains("PRS_INJRY"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"REEMN":
										if(htpremium.Contains("RESI_EMPL"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"BPCES":
										if(htpremium.Contains("BSNS_PRSUIT"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
												double HO71_prem = 0;
												try {HO71_prem	=	double.Parse(htpremium["CLASS_A"].ToString());}	
                                                catch (Exception ex)
                                                { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex); }
												try {HO71_prem	+=	double.Parse(htpremium["CLASS_B"].ToString());}	
                                                catch (Exception ex)
                                                { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex); }
												try {HO71_prem	+=	double.Parse(htpremium["CLASS_C"].ToString());}	
                                                catch (Exception ex)
                                                { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex); }
												try {HO71_prem	+=	double.Parse(htpremium["CLASS_D"].ToString());}	
                                                catch (Exception ex)
                                                { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex); }
												if(HO71_prem != 0)
												{
													gstrGetPremium = HO71_prem.ToString();
													gintGetindex	=	gstrGetPremium.IndexOf(".");
													if(gintGetindex==	-1)
														SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(gstrGetPremium + ".00")+"</HM_ENDPREMIUM>";											
													else
														SupplementDwellEndmtElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(gstrGetPremium)+"</HM_ENDPREMIUM>";											
											
												}
											}
										}
										break;
									case	"BPSCM":								
										break;
									case	"BPTAL":								
										break;
									case	"BPTNO":							
										break;
									case	"FLIFP":
										if(htpremium.Contains("INCT_FRMG_RESI_PRM"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"FLOFO":
										if(htpremium.Contains("INCT_FRMG_OPRT_INS"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"FLOFR":
										if(htpremium.Contains("INCT_FRMG_RNTD_OTH"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"OSTDISH":
										break;
									case	"HO200":
										if(htpremium.Contains("WTRBD_LBLTY"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"HO9":
										if(htpremium.Contains("SUB_SRFCE_WTR"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"REDUC":
										break;
									case	"BICYC":
										if(htpremium.Contains("BICYCLE"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"CAMER":
										if(htpremium.Contains("CMRA"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"CELLU":
										if(htpremium.Contains("CELL"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"FINEBR":
										if(htpremium.Contains("FNE_ART_WTH_BRK"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"FINEWBR":
										if(htpremium.Contains("FNE_ART_WO_BRK"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"FURS":
										if(htpremium.Contains("FUR"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"GOLF":
										if(htpremium.Contains("GOLF"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"GUNS":
										if(htpremium.Contains("GUN"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"HANDI":
										break;
									case	"HEARI":
										break;
									case	"INSUL":
										break;
									case	"JEWEL":
										if(htpremium.Contains("JWL"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"MART":
										break;
									case	"MUSIC":
										if(htpremium.Contains("MSC_NON_PRF"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"PERSOD":
										if(htpremium.Contains("PRS_CMP"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
									
										break;
									case	"PERSOL":
										if(htpremium.Contains("PRS_CMP"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										
										break;
									case	"RARE"	:
										if(htpremium.Contains("RARE_COIN"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"SALES"	:
										break;
									case	"SCUBA"	:
										break;
									case	"SILVE"	:
										if(htpremium.Contains("SLVR"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"SNOW"	:
										break;
									case	"STAMP"	:
										if(htpremium.Contains("STMP"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"TACK"	:
										break;
									case	"TOOLSP":
										break;
									case	"TOOLSB":
										break;
									case	"TRACT"	:
										break;
									case	"TRAIN"	:
										break;
									case	"WHEEL"	:
										break;
									case	"EOP17"	:	
										break;
									case	"ECOB"	:
										break;
									case	"ECOC"	:
										break;
									case	"BUMC"	:
										break;
									case	"LF330"	:
										break;
									case	"SP350"	:
										break;
									case	"LAS360":
										break;
									case	"CWC373"	:
										if(htpremium.Contains("WRKR_CMP"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;
									case	"HO214"	:
										if(htpremium.Contains("PRS_CMP"))
										{
											if(gStrPdfFor == PDFForAcord)
											{
											}
										}
										break;								
									case	"HO216"	:								
										if (gStrPdfFor == PDFForDecPage)
										{
										}
										break;
									case	"HO300"	:
										if (gStrPdfFor == PDFForDecPage)
										{
										}
										break;
									case	"HO864"	:
										if (gStrPdfFor == PDFForDecPage)
										{
										}
										break;
									case	"IFGHO"	:
										break;
									case	"WP865"	:
										if (gStrPdfFor == PDFForDecPage)
										{									
										}
										break;
									default:
										if (gStrPdfFor == PDFForDecPage)
										{
											if(CoverageDetails["COV_CODE"].ToString()=="EMO42" || CoverageDetails["COV_CODE"].ToString()=="EOP42")
											{									
											}
											if(CoverageDetails["COV_CODE"].ToString()=="EMO65" || CoverageDetails["COV_CODE"].ToString()=="EOP65")
											{
												#region Dec Page Element
										
												#endregion
											}
											if(CoverageDetails["COV_CODE"].ToString()=="EMO70" || CoverageDetails["COV_CODE"].ToString()=="EOP70")
											{
												#region Dec Page Element
										
												#endregion

											}
											if(CoverageDetails["COV_CODE"].ToString()=="EMO71" || CoverageDetails["COV_CODE"].ToString()=="EOP71")
											{
												#region Dec Page Element
										
												#endregion

											}
											if(CoverageDetails["COV_CODE"].ToString()=="EMR211" || CoverageDetails["COV_CODE"].ToString()=="EOP211")
											{
												#region Dec Page Element
										
												#endregion
											}
											if(CoverageDetails["COV_CODE"].ToString()=="EMR310")
											{
												#region Dec Page Element
										
												#endregion
											
											}
											if(CoverageDetails["COV_CODE"].ToString()=="EMO315" || CoverageDetails["COV_CODE"].ToString()=="EOP315")
											{
											
											}
											if(CoverageDetails["COV_CODE"].ToString()=="EMR320")
											{
												#region Dec Page Element
									
												#endregion
											
											}
											if(CoverageDetails["COV_CODE"].ToString()=="HO6D")
											{
											
											}
											if(CoverageDetails["COV_CODE"].ToString()=="HO4D")
											{
											
											}
										}
										else if(gStrPdfFor == PDFForAcord)
										{
											if(CoverageDetails["COV_CODE"].ToString()=="EMO42" || CoverageDetails["COV_CODE"].ToString()=="EOP42"){}
											if(CoverageDetails["COV_CODE"].ToString()=="EMO65" || CoverageDetails["COV_CODE"].ToString()=="EOP65"){}
											if(CoverageDetails["COV_CODE"].ToString()=="EMO70" || CoverageDetails["COV_CODE"].ToString()=="EOP70"){}
											if(CoverageDetails["COV_CODE"].ToString()=="EMO71" || CoverageDetails["COV_CODE"].ToString()=="EOP71"){}
											if(CoverageDetails["COV_CODE"].ToString()=="EMR211" || CoverageDetails["COV_CODE"].ToString()=="EOP211"){}
											if(CoverageDetails["COV_CODE"].ToString()=="EMR310"){}
											if(CoverageDetails["COV_CODE"].ToString()=="EMO315" || CoverageDetails["COV_CODE"].ToString()=="EOP315"){}
											if(CoverageDetails["COV_CODE"].ToString()=="EMR320"){}
											if(CoverageDetails["COV_CODE"].ToString()=="EMRSFP"){}
											if(CoverageDetails["COV_CODE"].ToString()=="EOP48"){}
											if(CoverageDetails["COV_CODE"].ToString()=="HO6D"){}
											if(CoverageDetails["COV_CODE"].ToString()=="HO4D"){}
										}									
										break;
								}
								#endregion
							}	
							DecPageEndCtr++;
						}
						#endregion
//						if(CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()==" ") //|| CoverageDetails["ENDORS_ASSOC_COVERAGE"].ToString()=="Y")
//						{
//							DecPageEndCtr++;
//							//endCtr++;
//						}						
					}									
					
					//endCtr++;
				}
				#endregion			

				#region Supplemental Page Outbuildings
				if (gStrPdfFor == PDFForAcord)
				{
					gobjWrapper.ClearParameteres();
					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@DWELLINGID",DwellingDetail["DWELLING_ID"]);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DataSet DSTempOutbuildings = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_otherstructures");
					//DataSet DSTempOutbuildings = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_otherstructures " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + DwellingDetail["DWELLING_ID"] +  ",'" + gStrCalledFrom + "'");
					if(DSTempOutbuildings.Tables[0].Rows.Count>0   )
					{
						XmlElement SupplementRootOutbuildings;
						SupplementRootOutbuildings= AcordPDFXML.CreateElement("OUTBUILDINGS");
						SupplementHomeElement.AppendChild(SupplementRootOutbuildings);
						SupplementRootOutbuildings.SetAttribute(fieldType,fieldTypeMultiple);
						SupplementRootOutbuildings.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTOUTBUILDING"));
						SupplementRootOutbuildings.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTOUTBUILDING"));
						SupplementRootOutbuildings.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTOUTBUILDINGEXTN"));
						SupplementRootOutbuildings.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTOUTBUILDINGEXTN"));

						int outCtr=0;
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
							SupplementOutbuildings.InnerXml= SupplementOutbuildings.InnerXml + "<VALUEOUTBUILDINGS " + fieldType +"=\""+ fieldTypeText +"\">"+ RemoveJunkXmlCharacters(CoverageDetails["INSURING_VALUE"].ToString()) +"</VALUEOUTBUILDINGS>";

							outCtr++;
						}
					}
				}
				#endregion

				#region Addl Interests
				XmlElement DecPageDwellAddlInt;
				DecPageDwellAddlInt = AcordPDFXML.CreateElement("ADDITIONALINT");

//				XmlElement Acord80DwellAddlInt;
//				Acord80DwellAddlInt = AcordPDFXML.CreateElement("ADDITIONALINT");
				if (gStrPdfFor == PDFForDecPage)
				{
					#region Additional Root Element int Dec Page
					DecPageHomeElement.AppendChild(DecPageDwellAddlInt);
					DecPageDwellAddlInt.SetAttribute(fieldType,fieldTypeMultiple);
					DecPageDwellAddlInt.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHOMEADDLINT"));
					DecPageDwellAddlInt.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHOMEADDLINT"));
					DecPageDwellAddlInt.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHOMEADDLINTEXTN"));
					DecPageDwellAddlInt.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHOMEADDLINTEXTN"));
					#endregion
				}
				else if (gStrPdfFor == PDFForAcord)
				{
					#region Additional Root Element Acord 80 Page
					//				Acord80HomeElement.AppendChild(Acord80DwellAddlInt);
					//				Acord80DwellAddlInt.SetAttribute(fieldType,fieldTypeMultiple);
					//				Acord80DwellAddlInt.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD80ADDLINT"));
					//				Acord80DwellAddlInt.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD80ADDLINT"));
					//				Acord80DwellAddlInt.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD80ADDLINTEXTN"));
					//				Acord80DwellAddlInt.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD80ADDLINTEXTN"));
					#endregion
				}
//				gobjWrapper.ClearParameteres();
//				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
//				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
//				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
//				gobjWrapper.AddParameter("@DWELLINGID",DwellingDetail["DWELLING_ID"]);
//				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
//				DataSet DSTempDwellinAddInt = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_Additional_Interest");
				//DataSet DSTempDwellinAddInt = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_Additional_Interest " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + "," + DwellingDetail["DWELLING_ID"] + ",'" + gStrCalledFrom + "'");
				foreach(DataRow AddlIntDetails in DSTempAddIntrst.Tables[0].Rows)
				{
					if (gStrPdfFor == PDFForDecPage)
					{
						#region AddlInt Dec Page
						XmlElement DecPageDwelAddlIntElement;
						DecPageDwelAddlIntElement = AcordPDFXML.CreateElement("ADDITIONALINTINFO");
						DecPageDwellAddlInt.AppendChild(DecPageDwelAddlIntElement);
						DecPageDwelAddlIntElement.SetAttribute(fieldType,fieldTypeNormal);
						DecPageDwelAddlIntElement.SetAttribute(id,AddInt.ToString());
					
						DecPageDwelAddlIntElement.InnerXml = DecPageDwelAddlIntElement.InnerXml +"<HOMEOWNER_SERIALNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["RANK"].ToString())+"</HOMEOWNER_SERIALNO>"; 
						DecPageDwelAddlIntElement.InnerXml = DecPageDwelAddlIntElement.InnerXml +"<HOMEOWNER_NAMEADDRESS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["HOLDER_CITYSTATEZIP"].ToString())+"</HOMEOWNER_NAMEADDRESS>"; 
						DecPageDwelAddlIntElement.InnerXml = DecPageDwelAddlIntElement.InnerXml +"<HOMEOWNER_NATUREOFINTEREST " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["LOOKUP_VALUE_DESC"].ToString())+"</HOMEOWNER_NATUREOFINTEREST>"; 
						DecPageDwelAddlIntElement.InnerXml = DecPageDwelAddlIntElement.InnerXml +"<HOMEOWNER_LOANNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["LOAN_REF_NUMBER"].ToString())+"</HOMEOWNER_LOANNO>"; 
						#endregion
					}
					else if (gStrPdfFor == PDFForAcord)
					{
						#region AddlInt Acord 80 Page
						//					XmlElement Acord80DwellAddlIntElement;
						//					Acord80DwellAddlIntElement = AcordPDFXML.CreateElement("ADDITIONALINTINFO");
						//					Acord80DwellAddlInt.AppendChild(Acord80DwellAddlIntElement);
						//					Acord80DwellAddlIntElement.SetAttribute(fieldType,fieldTypeNormal);
						//					Acord80DwellAddlIntElement.SetAttribute(id,AddInt.ToString());
						//							
						//					Acord80DwellAddlIntElement.InnerXml = Acord80DwellAddlIntElement.InnerXml +"<ADDITIONALINTERESTNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["RANK"].ToString())+"</ADDITIONALINTERESTNO>"; 
						//					Acord80DwellAddlIntElement.InnerXml = Acord80DwellAddlIntElement.InnerXml +"<ADDITIONALINTERESTADDRESS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["NAME_ADDRESS"].ToString())+"</ADDITIONALINTERESTADDRESS>"; 
						//					Acord80DwellAddlIntElement.InnerXml = Acord80DwellAddlIntElement.InnerXml +"<ADDITIONALINTERESTCITYSTATEZIP " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["HOLDERCITYSTATEZIP"].ToString())+"</ADDITIONALINTERESTCITYSTATEZIP>"; 
						//					Acord80DwellAddlIntElement.InnerXml = Acord80DwellAddlIntElement.InnerXml +"<NATUREOFINTEREST " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["NATURE_OF_INTEREST"].ToString())+"</NATUREOFINTEREST>"; 
						//					Acord80DwellAddlIntElement.InnerXml = Acord80DwellAddlIntElement.InnerXml +"<ADDITIONALINTERESTLOANNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["LOAN_REF_NUMBER"].ToString())+"</ADDITIONALINTERESTLOANNO>"; 
						//					Acord80DwellAddlIntElement.InnerXml = Acord80DwellAddlIntElement.InnerXml +"<NATUREOFINTDESC " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(AddlIntDetails["NATUREOFINT_DESC"].ToString())+"</NATUREOFINTDESC>"; 
						#endregion
					}
					AddInt++;
				}
				AddInt=0;
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
						XmlElement DecPageHOMECredit;
						DecPageHOMECredit = AcordPDFXML.CreateElement("HOMECREDIT");

						XmlElement SupplementHOMECredit;
						SupplementHOMECredit = AcordPDFXML.CreateElement("HOMECREDIT");
						
						XmlElement DecPageHOMECreditElement;
						

						XmlElement SupplementHOMECreditElement;
						SupplementHOMECreditElement = AcordPDFXML.CreateElement("HOMECREDITINFO");

						if (gStrPdfFor == PDFForDecPage)
						{
							#region Dec Page Element
							DecPageHomeElement.AppendChild(DecPageHOMECredit);
							DecPageHOMECredit.SetAttribute(fieldType,fieldTypeMultiple);
							DecPageHOMECredit.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECREDIT"));
							DecPageHOMECredit.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGECREDIT"));
							DecPageHOMECredit.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGECREDITEXTN"));
							DecPageHOMECredit.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGECREDITEXTN"));
							
							#endregion 					
						}
						else if (gStrPdfFor == PDFForAcord)
						{
							#region Supplement Page Element
							SupplementHomeElement.AppendChild(SupplementHOMECredit);
							SupplementHOMECredit.SetAttribute(fieldType,fieldTypeMultiple);
							SupplementHOMECredit.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTCREDIT"));
							SupplementHOMECredit.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTCREDIT"));
							SupplementHOMECredit.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTCREDITEXTN"));
							SupplementHOMECredit.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTCREDITEXTN"));
					
							#endregion
						}

						htpremium_dis.Clear(); 
						foreach (XmlNode CreditNode in GetCredits(DwellingDetail["DWELLING_ID"].ToString()))
						{
							if(!htpremium_dis.Contains(getAttributeValue(CreditNode,"COMPONENT_CODE")))
								htpremium_dis.Add(getAttributeValue(CreditNode,"COMPONENT_CODE"),getAttributeValue(CreditNode,"STEPPREMIUM"));
							else
								continue;
							string strCreditDisc="";
							if (gStrPdfFor == PDFForDecPage)
							{
								strCreditDisc = getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n","");
//								if(strCreditDisc.IndexOf("Insurance")>0)
//								{
//								}
//								else
//								{
								if(strCreditDisc.IndexOf("Discount")>=0)
								{
									strCreditDisc=strCreditDisc.Substring(strCreditDisc.IndexOf("Discount",strCreditDisc.LastIndexOf(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n","")))+"Discount".Length);
										
								}
								if(strCreditDisc.IndexOf("-")>=0)
								{
										strCreditDisc=strCreditDisc.Substring(strCreditDisc.IndexOf("-",strCreditDisc.LastIndexOf(strCreditDisc))+1);
								}
									DecPageHOMECreditElement = AcordPDFXML.CreateElement("HOMECREDITINFO");
									DecPageHOMECredit.AppendChild(DecPageHOMECreditElement);
									DecPageHOMECreditElement.SetAttribute(fieldType,fieldTypeNormal);
									DecPageHOMECreditElement.SetAttribute(id,CreditSurchRowCounter.ToString());							
									DecPageHOMECreditElement.InnerXml += "<CREDITDISC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strCreditDisc)+"</CREDITDISC>"; 
								//}								
							}
							else if (gStrPdfFor == PDFForAcord)
							{
	
								SupplementHOMECredit.AppendChild(SupplementHOMECreditElement);
								SupplementHOMECreditElement.SetAttribute(fieldType,fieldTypeNormal);
								SupplementHOMECreditElement.SetAttribute(id,"0");
							}
//							if(strCreditDisc.IndexOf("Insurance")>0 && gStrPdfFor == PDFForDecPage)
//							{
//							}
//							else
//							{
								CreditSurchRowCounter++;
							//}
						}

						#endregion 		
						#region Surcharges
						XmlElement DecPageHOMESurch;
						DecPageHOMESurch = AcordPDFXML.CreateElement("HOMESURCHARGE");

						XmlElement SupplementHOMESurch;
						SupplementHOMESurch = AcordPDFXML.CreateElement("HOMESURCHARGE");

						XmlElement DecPageHOMESurchElement;
						DecPageHOMESurchElement = AcordPDFXML.CreateElement("HOMESURCHARGEINFO");

//						XmlElement DecPageHOMESurchElement;
//						SupplementHOMESurchElement = AcordPDFXML.CreateElement("HOMESURCHARGEINFO");
						
					
						if (gStrPdfFor == PDFForDecPage)
						{
							#region Dec Page Element
							
							DecPageHomeElement.AppendChild(DecPageHOMESurch);
							DecPageHOMESurch.SetAttribute(fieldType,fieldTypeMultiple);
							DecPageHOMESurch.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESRCHG"));
							DecPageHOMESurch.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHG"));
							DecPageHOMESurch.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESRCHGEXTN"));
							DecPageHOMESurch.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHGEXTN"));

							#endregion 					
						}
						else if (gStrPdfFor == PDFForAcord)
						{
							#region Supplement Page Element

							SupplementHOMESurch.AppendChild(DecPageHOMESurchElement);
							DecPageHOMESurchElement.SetAttribute(fieldType,fieldTypeMultiple);
							DecPageHOMESurchElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESRCHG"));
							DecPageHOMESurchElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHG"));
							DecPageHOMESurchElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESRCHGEXTN"));
							DecPageHOMESurchElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHGEXTN"));

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
								// Remove Surcharge Word from Credit discription
								string strCreditSurch = getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n","");
								if(strCreditSurch.IndexOf("Charge")>=0)
								{
									strCreditSurch=strCreditSurch.Substring(strCreditSurch.IndexOf("Charge",strCreditSurch.LastIndexOf(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n","")))+"Charge".Length);
								}
								if(strCreditSurch.IndexOf("-")>=0)
								{
									strCreditSurch=strCreditSurch.Substring(strCreditSurch.IndexOf("-",strCreditSurch.LastIndexOf(strCreditSurch)));
								}
								DecPageHOMESurch.AppendChild(DecPageHOMESurchElement);
								DecPageHOMESurchElement.SetAttribute(fieldType,fieldTypeNormal);
								DecPageHOMESurchElement.SetAttribute(id,CreditSurchRowCounter.ToString());
								DecPageHOMESurchElement.InnerXml += "<CREDITDISC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strCreditSurch)+"</CREDITDISC>"; 
							
							}
							else if(gStrPdfFor == PDFForAcord)
							{
								SupplementHOMECredit.AppendChild(SupplementHOMECreditElement);
								SupplementHOMECreditElement.SetAttribute(fieldType,fieldTypeNormal);
								SupplementHOMECreditElement.SetAttribute(id,"0");
							}
						}
						CreditSurchRowCounter++;
						#endregion
					}
				}
				//if process commited
				else
				{
						// When process not commited
						int CreditSurchRowCounter = 0;
						#region Credits
						XmlElement DecPageHOMECredit;
						DecPageHOMECredit = AcordPDFXML.CreateElement("HOMECREDIT");

						XmlElement SupplementHOMECredit;
						SupplementHOMECredit = AcordPDFXML.CreateElement("HOMECREDIT");

						XmlElement DecPageHOMECreditElement;
						
						
						XmlElement SupplementHOMECreditElement;
						
						if (gStrPdfFor == PDFForDecPage)
						{
							#region Dec Page Element

							DecPageHomeElement.AppendChild(DecPageHOMECredit);
							DecPageHOMECredit.SetAttribute(fieldType,fieldTypeMultiple);
							DecPageHOMECredit.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECREDIT"));
							DecPageHOMECredit.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGECREDIT"));
							DecPageHOMECredit.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGECREDITEXTN"));
							DecPageHOMECredit.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGECREDITEXTN"));

							#endregion 					
						}
						else if (gStrPdfFor == PDFForAcord)
						{
							#region Supplement Page Element

							SupplementHomeElement.AppendChild(SupplementHOMECredit);
							SupplementHOMECredit.SetAttribute(fieldType,fieldTypeMultiple);
							SupplementHOMECredit.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTCREDIT"));
							SupplementHOMECredit.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTCREDIT"));
							SupplementHOMECredit.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTCREDITEXTN"));
							SupplementHOMECredit.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTCREDITEXTN"));

							#endregion
						}

					foreach (DataRow CreditNode  in DSTempDwelling.Tables[1].Rows)
					{
						if(CreditNode["COMPONENT_TYPE"].ToString()=="D" && CreditNode["COV_CODE"].ToString()==""  && (CreditNode["COVERAGE_PREMIUM"].ToString()!="0" && CreditNode["COVERAGE_PREMIUM"].ToString()!="0.00" && CreditNode["COVERAGE_PREMIUM"].ToString()!=""))
						{	
							string strCreditDisc="",strCreditPerc="";
							strCreditDisc = CreditNode["COMP_REMARKS"].ToString();
							strCreditPerc = CreditNode["COM_EXT_AD"].ToString();
							if(strCreditDisc.IndexOf("Discount")>=0)
							{
								strCreditDisc=strCreditDisc.Substring(strCreditDisc.IndexOf("Discount",strCreditDisc.LastIndexOf(strCreditDisc))+"Discount".Length);
								if(strCreditPerc!="")
									strCreditDisc=strCreditDisc+" - "+strCreditPerc;
								else
									strCreditDisc=strCreditDisc;
							}
							if(strCreditDisc.IndexOf("-")>=0)
							{
									strCreditDisc=strCreditDisc.Substring(strCreditDisc.IndexOf("-",strCreditDisc.LastIndexOf(strCreditDisc)));
							}
							if (gStrPdfFor == PDFForDecPage)
							{
								if(strCreditDisc.IndexOf("Insurance")>0)
								{
								}
								else
								{
									DecPageHOMECreditElement = AcordPDFXML.CreateElement("HOMECREDITINFO");
									DecPageHOMECredit.AppendChild(DecPageHOMECreditElement);
									DecPageHOMECreditElement.SetAttribute(fieldType,fieldTypeNormal);
									DecPageHOMECreditElement.SetAttribute(id,CreditSurchRowCounter.ToString());
									DecPageHOMECreditElement.InnerXml += "<CREDITDISC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strCreditDisc)+"</CREDITDISC>"; 
								}								
							}
							else if (gStrPdfFor == PDFForAcord)
							{
								SupplementHOMECreditElement = AcordPDFXML.CreateElement("HOMECREDITINFO");	
								SupplementHOMECredit.AppendChild(SupplementHOMECreditElement);
								SupplementHOMECreditElement.SetAttribute(fieldType,fieldTypeNormal);
								SupplementHOMECreditElement.SetAttribute(id,CreditSurchRowCounter.ToString());
								SupplementHOMECreditElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTCREDITEXTN"));
								SupplementHOMECreditElement.InnerXml +="<CREDITDISC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strCreditDisc)+"</CREDITDISC>";
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
						XmlElement DecPageHOMESurch;
						DecPageHOMESurch = AcordPDFXML.CreateElement("HOMESURCHARGE");

						XmlElement SupplementHOMESurch;
						SupplementHOMESurch = AcordPDFXML.CreateElement("HOMESURCHARGE");

						XmlElement DecPageHOMESurchElement;
						
						XmlElement SupplementHOMESurchElement;
						SupplementHOMESurchElement = AcordPDFXML.CreateElement("HOMESURCHARGEINFO");
						
					
						if (gStrPdfFor == PDFForDecPage)
						{
							#region Dec Page Element

							DecPageHomeElement.AppendChild(DecPageHOMESurch);
							DecPageHOMESurch.SetAttribute(fieldType,fieldTypeMultiple);
							DecPageHOMESurch.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESRCHG"));
							DecPageHOMESurch.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHG"));
							DecPageHOMESurch.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESRCHGEXTN"));
							DecPageHOMESurch.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHGEXTN"));

							#endregion 					
						}
						else if (gStrPdfFor == PDFForAcord)
						{
							#region Supplement Page Element

							SupplementHOMESurch.AppendChild(SupplementHOMESurchElement);
							SupplementHOMESurchElement.SetAttribute(fieldType,fieldTypeMultiple);
							SupplementHOMESurchElement.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESRCHG"));
							SupplementHOMESurchElement.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHG"));
							SupplementHOMESurchElement.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESRCHGEXTN"));
							SupplementHOMESurchElement.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHGEXTN"));

							#endregion
						}

						
					foreach (DataRow CreditNode in DSTempDwelling.Tables[1].Rows)
					{
						if(CreditNode["COMPONENT_TYPE"].ToString()=="S" && CreditNode["COV_CODE"].ToString()==""  && (CreditNode["COVERAGE_PREMIUM"].ToString()!="0" && CreditNode["COVERAGE_PREMIUM"].ToString()!="0.00" && CreditNode["COVERAGE_PREMIUM"].ToString()!=""))
						{

							
								string strCreditSurch = CreditNode["COMP_REMARKS"].ToString();
								string strCreditSurcPerc = CreditNode["COM_EXT_AD"].ToString();
								// Remove Surcharge Word from Credit discription
								if(strCreditSurch.IndexOf("Charge")>0)
								{
									strCreditSurch=strCreditSurch.Substring(strCreditSurch.IndexOf("Charge",strCreditSurch.LastIndexOf(strCreditSurch))+"Charge".Length);
								}
								if(strCreditSurch.IndexOf("Credit")>0)
								{
									strCreditSurch=strCreditSurch.Substring(strCreditSurch.IndexOf("Credit",strCreditSurch.LastIndexOf(strCreditSurch))+"Credit".Length);
								}								
								if(strCreditSurch.IndexOf("-")>0)
								{
									strCreditSurch=strCreditSurch.Substring(strCreditSurch.IndexOf("-",strCreditSurch.LastIndexOf(strCreditSurch)));
								}									
							if(strCreditSurcPerc!="")
							{
								if(strCreditSurcPerc.IndexOf('$')==-1)
									strCreditSurch=strCreditSurch+" - "+strCreditSurcPerc;
							}
							else
								strCreditSurch=strCreditSurch;
								
							if (gStrPdfFor == PDFForDecPage)
							{
								DecPageHOMESurchElement = AcordPDFXML.CreateElement("HOMESURCHARGEINFO");
								DecPageHOMESurch.AppendChild(DecPageHOMESurchElement);
								DecPageHOMESurchElement.SetAttribute(fieldType,fieldTypeNormal);
								DecPageHOMESurchElement.SetAttribute(id,CreditSurchRowCounter.ToString());
								DecPageHOMESurchElement.InnerXml += "<CREDITDISC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strCreditSurch)+"</CREDITDISC>"; 
							
							}
							else if (gStrPdfFor == PDFForAcord)
							{
								SupplementHOMESurchElement = AcordPDFXML.CreateElement("HOMESURCHARGEINFO");
								SupplementHOMECredit.AppendChild(SupplementHOMESurchElement);
								SupplementHOMESurchElement.SetAttribute(fieldType,fieldTypeNormal);
								SupplementHOMESurchElement.SetAttribute(id,CreditSurchRowCounter.ToString());
								SupplementHOMESurchElement.InnerXml +="<CREDITDISC " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(strCreditSurch)+"</CREDITDISC>";
							}
							CreditSurchRowCounter++;
						}						
					}
						
						#endregion
					
				}
				#endregion 
				//////////////////////////////////////////////////////////
				#region Creating Credit And Surcharge Xml
				////////////////////////////// Old Value
				/*if (isRateGenerated)
				{
					int CreditSurchRowCounter = 0;
					int creditAddPage = 0;
					string AdditionalExtnTxt="";

					#region Credits
					XmlElement DecPageHOMECredit;
					DecPageHOMECredit = AcordPDFXML.CreateElement("HOMECREDIT");

					XmlElement SupplementHOMECredit;
					SupplementHOMECredit = AcordPDFXML.CreateElement("HOMECREDIT");

					XmlElement SupplementHOMECreditElement;
					SupplementHOMECreditElement = AcordPDFXML.CreateElement("HOMECREDITINFO");	
					SupplementHOMECredit.AppendChild(SupplementHOMECreditElement);
					SupplementHOMECreditElement.SetAttribute(fieldType,fieldTypeNormal);
					SupplementHOMECreditElement.SetAttribute(id,"0");

					XmlElement DecPageHOMECreditElement;
					DecPageHOMECreditElement = AcordPDFXML.CreateElement("HOMECREDITINFO");
					DecPageHOMECredit.AppendChild(DecPageHOMECreditElement);
					DecPageHOMECreditElement.SetAttribute(fieldType,fieldTypeNormal);
					DecPageHOMECreditElement.SetAttribute(id,"0");

					if (gStrPdfFor == PDFForDecPage)
					{
						#region Dec Page Element
						DecPageHomeElement.AppendChild(DecPageHOMECredit);
						DecPageHOMECredit.SetAttribute(fieldType,fieldTypeMultiple);
						DecPageHOMECredit.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECREDIT"));
						DecPageHOMECredit.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGECREDIT"));
						DecPageHOMECredit.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGECREDITEXTN"));
						DecPageHOMECredit.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGECREDITEXTN"));
					
						#endregion
					}
					else if (gStrPdfFor == PDFForAcord)
					{
						#region Supplement Page Element
						SupplementHomeElement.AppendChild(SupplementHOMECredit);
						SupplementHOMECredit.SetAttribute(fieldType,fieldTypeMultiple);
						SupplementHOMECredit.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTCREDIT"));
						SupplementHOMECredit.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTCREDIT"));
						SupplementHOMECredit.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTCREDITEXTN"));
						SupplementHOMECredit.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTCREDITEXTN"));
						
						#endregion
					}

					htpremium_dis.Clear(); 
					foreach (XmlNode CreditNode in GetCredits(DwellingDetail["DWELLING_ID"].ToString()))
					{
						if(!htpremium_dis.Contains(getAttributeValue(CreditNode,"COMPONENT_CODE")))
							htpremium_dis.Add(getAttributeValue(CreditNode,"COMPONENT_CODE"),getAttributeValue(CreditNode,"STEPPREMIUM"));
						else
							continue;

						string step_desc = getAttributeValue(CreditNode,"STEPDESC").Replace(" ","");
						string step_prem = getAttributeValue(CreditNode,"STEPPREMIUM").Trim();

						if(step_desc.IndexOf("+0%") >0 || step_desc.IndexOf("-0%") >0 || step_prem == "0")
							continue;
						if (gStrPdfFor == PDFForDecPage)
						{							
							#region Dec Page
											
							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_PROT_DVC")	
							{
								DecPageHOMECreditElement.InnerXml += "<HM_PROTECTIVEDEVICE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_PROTECTIVEDEVICE>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									DecPageHOMECreditElement.InnerXml += "<HM_DEVICEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_DEVICEPREMIUM>"; 
								creditAddPage = 1;
							}

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_EXP")	
							{
								DecPageHOMECreditElement.InnerXml += "<HM_MATURITYCREDIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_MATURITYCREDIT>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									DecPageHOMECreditElement.InnerXml += "<HM_MATURITYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_MATURITYPREMIUM>"; 
							}

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_VALUED_CST")	
							{							
								DecPageHOMECreditElement.InnerXml += "<HM_RENEWALDISCOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_RENEWALDISCOUNT>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									DecPageHOMECreditElement.InnerXml += "<HM_RENEWALPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_RENEWALPREMIUM>"; 
								creditAddPage = 1;
							}

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_SMKR")	
							{							
								DecPageHOMECreditElement.InnerXml += "<HM_NONSOMKER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_NONSOMKER>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									DecPageHOMECreditElement.InnerXml += "<HM_NONSMOKERPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_NONSMOKERPREMIUM>"; 
								creditAddPage = 1;
							}

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_AGE_HOME")	
							{											
								DecPageHOMECreditElement.InnerXml += "<HM_AGEOFHOME " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_AGEOFHOME>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									DecPageHOMECreditElement.InnerXml += "<HM_AGEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_AGEPREMIUM>"; 	
							}

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_MULTIPOL")	
							{
								DecPageHOMECreditElement.InnerXml += "<HM_MULTIPOLICY " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_MULTIPOLICY>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									DecPageHOMECreditElement.InnerXml += "<HM_MULTIPPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_MULTIPPREMIUM>"; 
							}

							//Builders Risk
							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_DUC")	
							{							
								DecPageHOMECreditElement.InnerXml += "<HM_BUILDERRISK " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_BUILDERRISK>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									DecPageHOMECreditElement.InnerXml += "<HM_RISKPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_RISKPREMIUM>"; 
							}

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_SEASNL")	
							{							
								DecPageHOMECreditElement.InnerXml += "<HM_SECONDARYRESIDENCE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_SECONDARYRESIDENCE>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									DecPageHOMECreditElement.InnerXml += "<HM_RESIDENCE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_RESIDENCE>"; 
								creditAddPage = 1;
							}
							#endregion
						}
						else if (gStrPdfFor == PDFForAcord)
						{
							#region Supplement Page
							
							if (DwellingCtr%2 == 0)
								AdditionalExtnTxt="";
							else
								AdditionalExtnTxt="1";

						
							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_PROT_DVC")	
							{
								SupplementHOMECreditElement.InnerXml += "<HM_PROTECTIVEDEVICE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_PROTECTIVEDEVICE>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									SupplementHOMECreditElement.InnerXml += "<HM_DEVICEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_DEVICEPREMIUM>"; 
								creditAddPage = 1;
							}

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_EXP")	
							{
								SupplementHOMECreditElement.InnerXml += "<HM_MATURITYCREDIT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_MATURITYCREDIT>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									SupplementHOMECreditElement.InnerXml += "<HM_MATURITYPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_MATURITYPREMIUM>"; 
							}

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_VALUED_CST")	
							{							
								SupplementHOMECreditElement.InnerXml += "<HM_RENEWALDISCOUNT " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_RENEWALDISCOUNT>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									SupplementHOMECreditElement.InnerXml += "<HM_RENEWALPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_RENEWALPREMIUM>"; 
								creditAddPage = 1;
							}

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_SMKR")	
							{							
								SupplementHOMECreditElement.InnerXml += "<HM_NONSOMKER " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_NONSOMKER>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									SupplementHOMECreditElement.InnerXml += "<HM_NONSMOKERPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_NONSMOKERPREMIUM>"; 
								creditAddPage = 1;
							}

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_AGE_HOME")	
							{											
								SupplementHOMECreditElement.InnerXml += "<HM_AGEOFHOME " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_AGEOFHOME>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									SupplementHOMECreditElement.InnerXml += "<HM_AGEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_AGEPREMIUM>"; 	
							}

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_MULTIPOL")	
							{
								SupplementHOMECreditElement.InnerXml += "<HM_MULTIPOLICY " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_MULTIPOLICY>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									SupplementHOMECreditElement.InnerXml += "<HM_MULTIPPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_MULTIPPREMIUM>"; 
							}

							//Builders Risk
							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_DUC")	
							{							
								SupplementHOMECreditElement.InnerXml += "<HM_BUILDERRISK " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_BUILDERRISK>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									SupplementHOMECreditElement.InnerXml += "<HM_RISKPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_RISKPREMIUM>"; 
							}

							if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_SEASNL")	
							{							
								SupplementHOMECreditElement.InnerXml += "<HM_SECONDARYRESIDENCE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_SECONDARYRESIDENCE>"; 
								if(getAttributeValue(CreditNode,"STEPPREMIUM") != "0.00")
									SupplementHOMECreditElement.InnerXml += "<HM_RESIDENCE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_RESIDENCE>"; 
								creditAddPage = 1;
							}
							#endregion
						}

						CreditSurchRowCounter++;
					}
					if(creditAddPage == 1)
					{
						if (gStrPdfFor == PDFForAcord)
						{
							XmlElement SupplementHOMECreditElement1;
							SupplementHOMECreditElement1 = AcordPDFXML.CreateElement("HOMECREDITINFO");	
							SupplementHOMECredit.AppendChild(SupplementHOMECreditElement1);
							SupplementHOMECreditElement1.SetAttribute(fieldType,fieldTypeNormal);
							//SupplementHOMECreditElement1.SetAttribute(id,CreditSurchRowCounter.ToString());
							SupplementHOMECreditElement1.SetAttribute(id,"1");

							SupplementHOMECreditElement1.InnerXml = SupplementHOMECreditElement.InnerXml;
						}

						if (gStrPdfFor == PDFForDecPage)
						{							
							XmlElement DecPageHOMECreditElement1;
							DecPageHOMECreditElement1 = AcordPDFXML.CreateElement("HOMECREDITINFO");
							DecPageHOMECredit.AppendChild(DecPageHOMECreditElement1);
							DecPageHOMECreditElement1.SetAttribute(fieldType,fieldTypeNormal);
							DecPageHOMECreditElement1.SetAttribute(id,"1");

							DecPageHOMECreditElement1.InnerXml = DecPageHOMECreditElement.InnerXml;
						}
					}
					#endregion

					CreditSurchRowCounter = 0;
					AdditionalExtnTxt="";

					#region Surcharges
						XmlElement DecPageHOMESurch;
						DecPageHOMESurch = AcordPDFXML.CreateElement("HOMESURCHARGE");

						XmlElement SupplementHOMESurch;
						SupplementHOMESurch = AcordPDFXML.CreateElement("HOMESURCHARGE");

						XmlElement DecPageHOMESurchElement;
						DecPageHOMESurchElement = AcordPDFXML.CreateElement("HOMESURCHARGEINFO");

						XmlElement SupplementHOMESurchElement;
						SupplementHOMESurchElement = AcordPDFXML.CreateElement("HOMESURCHARGEINFO");
									

						if (gStrPdfFor == PDFForDecPage)
						{
							#region Dec Page Element
							DecPageHomeElement.AppendChild(DecPageHOMESurch);
							DecPageHOMESurch.SetAttribute(fieldType,fieldTypeMultiple);
							DecPageHOMESurch.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESRCHG"));
							DecPageHOMESurch.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHG"));
							DecPageHOMESurch.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESRCHGEXTN"));
							DecPageHOMESurch.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESRCHGEXTN"));

							DecPageHOMESurch.AppendChild(DecPageHOMESurchElement);
							DecPageHOMESurchElement.SetAttribute(fieldType,fieldTypeNormal);
							DecPageHOMESurchElement.SetAttribute(id,CreditSurchRowCounter.ToString());
							#endregion
						}
						else if (gStrPdfFor == PDFForAcord)
						{
							#region Supplement Page Element
							SupplementHomeElement.AppendChild(SupplementHOMESurch);
							SupplementHOMESurch.SetAttribute(fieldType,fieldTypeMultiple);
							SupplementHOMESurch.SetAttribute(PrimPDF,getAcordPDFNameFromXML("SUPPLEMENTSRCHG"));
							SupplementHOMESurch.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTSRCHG"));
							SupplementHOMESurch.SetAttribute(SecondPDF,getAcordPDFNameFromXML("SUPPLEMENTSRCHGEXTN"));
							SupplementHOMESurch.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("SUPPLEMENTSRCHGEXTN"));

							SupplementHOMESurch.AppendChild(SupplementHOMESurchElement);
							SupplementHOMESurchElement.SetAttribute(fieldType,fieldTypeNormal);
							SupplementHOMESurchElement.SetAttribute(id,CreditSurchRowCounter.ToString());						
							#endregion
						}

						htpremium_sur.Clear(); 
						foreach (XmlNode CreditNode in GetSurcharges(DwellingDetail["DWELLING_ID"].ToString()))
						{
							if(!htpremium_sur.Contains(getAttributeValue(CreditNode,"COMPONENT_CODE")))
								htpremium_sur.Add(getAttributeValue(CreditNode,"COMPONENT_CODE"),getAttributeValue(CreditNode,"STEPPREMIUM"));
							else
								continue;
							string step_desc = getAttributeValue(CreditNode,"STEPDESC").Replace(" ","");
							string step_prem = getAttributeValue(CreditNode,"STEPPREMIUM").Trim();

							if(step_desc.IndexOf("+0%") >0 || step_desc.IndexOf("-0%") >0 || step_prem == "0")
								continue;

							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page							
								if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="S_WOOD_STV")	
								{							
									DecPageHOMESurchElement.InnerXml += "<hm_WOODSTOVE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</hm_WOODSTOVE>"; 
									DecPageHOMESurchElement.InnerXml += "<HM_WOODSTOVEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_WOODSTOVEPREMIUM>";
								}
								else if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="S_BREED")	
								{							
									DecPageHOMESurchElement.InnerXml += "<hm_SPECIALBREEDDOG " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</hm_SPECIALBREEDDOG>"; 
									DecPageHOMESurchElement.InnerXml += "<HM_BREEDDOGPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_BREEDDOGPREMIUM>";
								}
								else //if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_VALUED_CST")	
								{							
									DecPageHOMESurchElement.InnerXml += "<hm_NEWBUSINESSPRIORLOSS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</hm_NEWBUSINESSPRIORLOSS>"; 
									DecPageHOMESurchElement.InnerXml += "<HM_NEWBUSINESSPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_NEWBUSINESSPREMIUM>";
								}
								#endregion
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
									SupplementHOMESurchElement.InnerXml += "<HM_FAMILYUNITS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</HM_FAMILYUNITS>"; 
									//DecPageBoatSurchElement.InnerXml += "<SURCHARGEPREM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</SURCHARGEPREM>";
								}
								else if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="S_WOOD_STV")	
								{							
									SupplementHOMESurchElement.InnerXml += "<hm_WOODSTOVE " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</hm_WOODSTOVE>"; 
									SupplementHOMESurchElement.InnerXml += "<HM_WOODSTOVEPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_WOODSTOVEPREMIUM>";
								}
								else if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="S_BREED")	
								{							
									SupplementHOMESurchElement.InnerXml += "<hm_SPECIALBREEDDOG " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</hm_SPECIALBREEDDOG>"; 
									SupplementHOMESurchElement.InnerXml += "<HM_BREEDDOGPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_BREEDDOGPREMIUM>";
								}
								else //if(getAttributeValue(CreditNode,"COMPONENT_CODE")=="D_VALUED_CST")	
								{							
									SupplementHOMESurchElement.InnerXml += "<hm_NEWBUSINESSPRIORLOSS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPDESC").Replace("\r\n",""))+"</hm_NEWBUSINESSPRIORLOSS>"; 
									SupplementHOMESurchElement.InnerXml += "<HM_NEWBUSINESSPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(getAttributeValue(CreditNode,"STEPPREMIUM"))+"</HM_NEWBUSINESSPREMIUM>";
								}
								#endregion
							}
							CreditSurchRowCounter++;
						}
					#endregion
				}*/
				/////////// Old Value End
				#endregion


				DwellingCtr++;
			}
		}
		#endregion

		#region Code for Dwelling Addl Interests
		private void createAcord80HomeAddlIntXml()
		{
			if (gStrPdfFor == PDFForAcord)
			{
				gobjWrapper.ClearParameteres();
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@DWELLINGID",0);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DataSet DSTempDwellinAdd = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_Additional_Interest");
				//DataSet DSTempDwellinAdd = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_Additional_Interest " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion  + ",0,'" + gStrCalledFrom + "'");
			
				#region Acord80 Page
				if (DSTempDwellinAdd.Tables[0].Rows.Count >0)
				{
					XmlElement Acord80AddlInts;
					Acord80AddlInts = AcordPDFXML.CreateElement("ADDITIONALINTERESTS");
					Acord80RootElement.AppendChild(Acord80AddlInts);
					Acord80AddlInts.SetAttribute(fieldType,fieldTypeMultiple);
					Acord80AddlInts.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD80ADDLINT"));
					Acord80AddlInts.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD80ADDLINT"));
					Acord80AddlInts.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD80ADDLINTEXTN"));
					Acord80AddlInts.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD80ADDLINTEXTN"));

					int RowCounter = 0;
					foreach(DataRow Row in DSTempDwellinAdd.Tables[0].Rows)
					{
						XmlElement Acord80ADDLINTElement;
						Acord80ADDLINTElement = AcordPDFXML.CreateElement("ADDLINTINFO");
						Acord80AddlInts.AppendChild(Acord80ADDLINTElement);
						Acord80ADDLINTElement.SetAttribute(fieldType,fieldTypeNormal);
						Acord80ADDLINTElement.SetAttribute(id,RowCounter.ToString());

						Acord80ADDLINTElement.InnerXml = Acord80ADDLINTElement.InnerXml +"<ADDITIONALINTERESTNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Row["RANK"].ToString())+"</ADDITIONALINTERESTNO>"; 
						Acord80ADDLINTElement.InnerXml = Acord80ADDLINTElement.InnerXml +"<ADDITIONALINTERESTADDRESS " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Row["NAME_ADDRESS"].ToString())+"</ADDITIONALINTERESTADDRESS>"; 
						Acord80ADDLINTElement.InnerXml = Acord80ADDLINTElement.InnerXml +"<ADDITIONALINTERESTCITYSTATEZIP " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Row["HOLDERCITYSTATEZIP"].ToString())+"</ADDITIONALINTERESTCITYSTATEZIP>"; 
						Acord80ADDLINTElement.InnerXml = Acord80ADDLINTElement.InnerXml +"<NATUREOFINTEREST " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(Row["NATURE_OF_INTEREST"].ToString())+"</NATUREOFINTEREST>"; 
						Acord80ADDLINTElement.InnerXml = Acord80ADDLINTElement.InnerXml +"<ADDITIONALINTERESTLOANNO " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(Row["LOAN_REF_NUMBER"].ToString()) +"</ADDITIONALINTERESTLOANNO>"; 
						if(Row["NATURE_OF_INTEREST"].ToString() == "3")
							Acord80ADDLINTElement.InnerXml = Acord80ADDLINTElement.InnerXml +"<NATUREOFINT_DESC " + fieldType +"=\""+ fieldTypeCheckBox +"\">"+RemoveJunkXmlCharacters(Row["LOOKUP_VALUE_DESC"].ToString())+"</NATUREOFINT_DESC>"; 

						RowCounter++;
					}
				}
				#endregion
			}
		}
		#endregion

		#region Code for Underwriting And General Info Xml Generation
		private void createDwellingUnderwritingGeneralXML()
		{
			if (gStrPdfFor == PDFForAcord)
			{
				gobjWrapper.ClearParameteres();
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempDataSet = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeownerUnderwritingDetails");
				//DSTempDataSet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeownerUnderwritingDetails " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
				if(DSTempDataSet.Tables[0].Rows.Count >0)
				{
					#region Acord80 Page General Info
					XmlElement Acord80GenInfoElement;
					Acord80GenInfoElement = AcordPDFXML.CreateElement("GENERALINFO");
					Acord80RootElement.AppendChild(Acord80GenInfoElement);
					Acord80GenInfoElement.SetAttribute(fieldType,fieldTypeSingle);
				
					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<FARMING " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_FARMING_BUSINESS_COND"].ToString()) + "</FARMING>";
					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<RESIDENCEEMPLOYEES " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_RESIDENCE_EMPLOYEE"].ToString()) + "</RESIDENCEEMPLOYEES>";
					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<OTHERRESIDENCE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_OTHER_RESI_OWNED"].ToString()) + "</OTHERRESIDENCE>";
					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<OTHERINSURANCE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_OTH_INSU_COMP"].ToString()) + "</OTHERINSURANCE>";
					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<INSURANCETRANSFERRED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["HAS_INSU_TRANSFERED_AGENCY"].ToString()) + "</INSURANCETRANSFERRED>";
					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<COVERAGEDECLINED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_COV_DECLINED_CANCELED"].ToString()) + "</COVERAGEDECLINED>";
					string any_pets = "0";
					if(DSTempDataSet.Tables[0].Rows[0]["ANIMALS_EXO_PETS_HISTORY"].ToString().Trim() != "0")
						any_pets = "1";
					else if(DSTempDataSet.Tables[0].Rows[0]["NO_OF_PETS"].ToString().Trim() != "0")
						any_pets = "1";
					else if(DSTempDataSet.Tables[0].Rows[0]["NO_HORSES"] != System.DBNull.Value && DSTempDataSet.Tables[0].Rows[0]["NO_HORSES"].ToString().Trim() != "0")
						any_pets = "1";
					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<ANIMALSPETS " + fieldType + "=\"" + fieldTypeText + "\">" + any_pets + "</ANIMALSPETS>";
					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<FIVEACRES " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["OF_ACRES"].ToString()) + "</FIVEACRES>";

					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<APPLICANTCONVICTED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["CONVICTION_DEGREE_IN_PAST"].ToString()) + "</APPLICANTCONVICTED>";
					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<BUILDINGUNDERRENOVATION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["ANY_RENOVATION"].ToString()) + "</BUILDINGUNDERRENOVATION>";
					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<PROPERTYCOMMERCIAL " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["IS_PROP_NEXT_COMMERICAL"].ToString()) + "</PROPERTYCOMMERCIAL>";
					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<TRAMPOLINE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["TRAMPOLINE"].ToString()) + "</TRAMPOLINE>";
					Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<BUILDINGUNDERCONSTRUCTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["BUILD_UNDER_CON_GEN_CONT"].ToString()) + "</BUILDINGUNDERCONSTRUCTION>";

					Acord80GenInfoElement.InnerXml =  Acord80GenInfoElement.InnerXml +  "<DATELASTINSPECTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["LAST_INSPECTED_DATE"].ToString()) + "</DATELASTINSPECTION>";
					//All Remarks Go here.
					if(DSTempDataSet.Tables[0].Rows[0]["FARMING_DESC"].ToString() != "")
						Acord80GenInfoElement.InnerXml = Acord80GenInfoElement.InnerXml +  "<ANY_FARMING_DESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #1: " + DSTempDataSet.Tables[0].Rows[0]["FARMING_DESC"].ToString()) + "</ANY_FARMING_DESC>";
					#endregion
				
					if(DSTempDataSet.Tables[0].Rows[0]["DESC_RESIDENCE_EMPLOYEE"].ToString() != "" || DSTempDataSet.Tables[0].Rows[0]["DESC_OTHER_RESIDENCE"].ToString() != "" ||
						DSTempDataSet.Tables[0].Rows[0]["DESC_OTHER_INSURANCE"].ToString() != "" || DSTempDataSet.Tables[0].Rows[0]["DESC_INSU_TRANSFERED_AGENCY"].ToString() != "" ||
						DSTempDataSet.Tables[0].Rows[0]["DESC_COV_DECLINED_CANCELED"].ToString() != "" || DSTempDataSet.Tables[0].Rows[0]["BREED_OTHER_DESCRIPTION"].ToString() != "" ||
						DSTempDataSet.Tables[0].Rows[0]["DESC_CONVICTION_DEGREE_IN_PAST"].ToString() != "" || DSTempDataSet.Tables[0].Rows[0]["DESC_RENOVATION"].ToString() != "" ||
						DSTempDataSet.Tables[0].Rows[0]["DESC_PROPERTY"].ToString() != "" || DSTempDataSet.Tables[0].Rows[0]["DESC_TRAMPOLINE"].ToString() != "" ||
						DSTempDataSet.Tables[0].Rows[0]["REMARKS"].ToString() != "")
					{
						#region Acord 80 supplement Sheet Remarks
						XmlElement ACORD80ADDLREMARKS;
						ACORD80ADDLREMARKS = AcordPDFXML.CreateElement("ACORD80ADDLREMARKS");
						Acord80RootElement.AppendChild(ACORD80ADDLREMARKS);
						ACORD80ADDLREMARKS.SetAttribute(fieldType,fieldTypeMultiple);
						ACORD80ADDLREMARKS.SetAttribute(PrimPDF,getAcordPDFNameFromXML("ACORD80REMARKS"));
						ACORD80ADDLREMARKS.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("ACORD80REMARKS"));
						ACORD80ADDLREMARKS.SetAttribute(SecondPDF,getAcordPDFNameFromXML("ACORD80REMARKSEXTN"));
						ACORD80ADDLREMARKS.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("ACORD80REMARKSEXTN"));
				
						ACORD80ADDLREMARKS.InnerXml = ACORD80ADDLREMARKS.InnerXml +  "<ALL_REMARKS " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(DSTempDataSet.Tables[0].Rows[0]["RENTAL_REMARKS_PDF"].ToString()) + "</ALL_REMARKS>";

						/*ACORD80ADDLREMARKS.InnerXml = ACORD80ADDLREMARKS.InnerXml +  "<NO_OF_RESI_EMPLOYEES " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #2: " + DSTempDataSet.Tables[0].Rows[0]["DESC_RESIDENCE_EMPLOYEE"].ToString()) + "</NO_OF_RESI_EMPLOYEES>";
						ACORD80ADDLREMARKS.InnerXml = ACORD80ADDLREMARKS.InnerXml +  "<OTHER_RESIDENCE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #4: " + DSTempDataSet.Tables[0].Rows[0]["DESC_OTHER_RESIDENCE"].ToString()) + "</OTHER_RESIDENCE>";
						ACORD80ADDLREMARKS.InnerXml = ACORD80ADDLREMARKS.InnerXml +  "<OTHER_INSURANCE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #5: " + DSTempDataSet.Tables[0].Rows[0]["DESC_OTHER_INSURANCE"].ToString()) + "</OTHER_INSURANCE>";
						ACORD80ADDLREMARKS.InnerXml = ACORD80ADDLREMARKS.InnerXml +  "<INSURANCE_TRANSFERRED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #6: " + DSTempDataSet.Tables[0].Rows[0]["DESC_INSU_TRANSFERED_AGENCY"].ToString()) + "</INSURANCE_TRANSFERRED>";
						ACORD80ADDLREMARKS.InnerXml = ACORD80ADDLREMARKS.InnerXml +  "<COVERAGE_DECLINED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #7: " + DSTempDataSet.Tables[0].Rows[0]["DESC_COV_DECLINED_CANCELED"].ToString()) + "</COVERAGE_DECLINED>";
						ACORD80ADDLREMARKS.InnerXml = ACORD80ADDLREMARKS.InnerXml +  "<BREED_OF_DOG " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #9: " + DSTempDataSet.Tables[0].Rows[0]["BREED_OTHER_DESCRIPTION"].ToString()) + "</BREED_OF_DOG>";
						ACORD80ADDLREMARKS.InnerXml = ACORD80ADDLREMARKS.InnerXml +  "<ANY_CONVICTED " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #14: " + DSTempDataSet.Tables[0].Rows[0]["DESC_CONVICTION_DEGREE_IN_PAST"].ToString()) + "</ANY_CONVICTED>";
						ACORD80ADDLREMARKS.InnerXml = ACORD80ADDLREMARKS.InnerXml +  "<CONSTRUCTION_DATE " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #19: " + DSTempDataSet.Tables[0].Rows[0]["DESC_RENOVATION"].ToString()) + "</CONSTRUCTION_DATE>";
						ACORD80ADDLREMARKS.InnerXml = ACORD80ADDLREMARKS.InnerXml +  "<PROPERTY_DESCRIPTION " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #21: " + DSTempDataSet.Tables[0].Rows[0]["DESC_PROPERTY"].ToString()) + "</PROPERTY_DESCRIPTION>";
						ACORD80ADDLREMARKS.InnerXml = ACORD80ADDLREMARKS.InnerXml +  "<TRAMPOLINE_DESC " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Question #22: " + DSTempDataSet.Tables[0].Rows[0]["DESC_TRAMPOLINE"].ToString()) + "</TRAMPOLINE_DESC>";
						ACORD80ADDLREMARKS.InnerXml = ACORD80ADDLREMARKS.InnerXml +  "<ANY_REMARK " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Any Other Remarks: " + DSTempDataSet.Tables[0].Rows[0]["REMARKS"].ToString()) + "</ANY_REMARK>";*/
						ACORD80ADDLREMARKS.InnerXml = "<ACORD80REMARKSINFO " + fieldType + "=\"" + fieldTypeNormal + "\" " + id + "=\"0\">" + ACORD80ADDLREMARKS.InnerXml + "</ACORD80REMARKSINFO>";
						#endregion
					}
				}
			}
		}
		#endregion

		#region Endorsement Wordings
		//by prAVESH ON 25 MARCH 2008 FOR REMOVING THOSE ENDORSEMENT WHICH HAS NOT BEEN CHANGED AT ENDORSEMENT /RENEWAL PROCESS
		private void RemoveEnorsementWordings()
		{
			try
			{
				if (prnOrd_covCode==null) return;
				DataSet dsDwelling = new DataSet();
				gobjWrapper.ClearParameteres();
				gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				gobjWrapper.AddParameter("@POLID",gStrPolicyId);
				gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				dsDwelling = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_Dwelling_Details");
				//dsDwelling = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_Dwelling_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");			
				foreach(DataRow DwellingDetail in dsDwelling.Tables[0].Rows)
				{
					gobjWrapper.ClearParameteres();
					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					gobjWrapper.AddParameter("@DWELLINGID",DwellingDetail["DWELLING_ID"]);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DataSet DSNewCoverageEndorsemet = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeOwner_Coverage_Details");
					//DataSet DSNewCoverageEndorsemet = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeOwner_Coverage_Details " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + "," + DwellingDetail["DWELLING_ID"] +  ",'" + gStrCalledFrom + "'");
					gobjWrapper.ClearParameteres();
					gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					gobjWrapper.AddParameter("@POLID",gStrPolicyId);
					gobjWrapper.AddParameter("@VERSIONID",goldVewrsionId);
					gobjWrapper.AddParameter("@DWELLINGID",DwellingDetail["DWELLING_ID"]);
					gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DataSet DSOldCoverageEndorsemet = gobjWrapper.ExecuteDataSet("Proc_GetPDFHomeOwner_Coverage_Details");
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
			if(gStrPdfFor == PDFForDecPage)
			{
				int BlankPagePrinted=0;
				int endorsCount = 0;
				int counter = 0;
				int intCnt = 0,Cntrl=0,inttotalpage=0,intPrivacyPage=0;
				inttotalpage += intPrivacyPage;
				//check for even and odd number of pages
				inttotalpage = inttotalpage%2;
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
					#region SWITCH CASE FOR DEC PAGE
					//switch(CoverageDetails["COV_CODE"].ToString())
					/*switch(prncovCode)
					{
						case "EBNBPL":
							break;
						case "WOODS":								
							//										//if(htpremium.Contains("S_WOOD_STV"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["S_WOOD_STV"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["S_WOOD_STV"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["S_WOOD_STV"].ToString())+"</HM_ENDPREMIUM>";


							}
										
							//dblSumTotal+=int.Parse(htpremium["S_WOOD_STV"].ToString());
						}
							break;
						case "EBRCPP":
							//										//if(htpremium.Contains("RPLC_CST"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["RPLC_CST"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RPLC_CST"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RPLC_CST"].ToString())+"</HM_ENDPREMIUM>";
										
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{
								XmlElement DecPageHomeEndoEBRCPP;
								DecPageHomeEndoEBRCPP = AcordPDFXML.CreateElement("HOMEENDORSEMENTEEBRCPP");
								DecPageRootElement.AppendChild(DecPageHomeEndoEBRCPP);
								DecPageHomeEndoEBRCPP.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEBRCPP.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEBRCPP.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEBRCPP.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBRCPP"));
									DecPageHomeEndoEBRCPP.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBRCPP"));
								}
								DecPageHomeEndoEBRCPP.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBRCPPEXTN"));
								DecPageHomeEndoEBRCPP.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBRCPPEXTN"));

								XmlElement EndoElementEBRCPP;
								EndoElementEBRCPP = AcordPDFXML.CreateElement("ENDOELEMENTEBRCPPINFO");
								DecPageHomeEndoEBRCPP.AppendChild(EndoElementEBRCPP);
								EndoElementEBRCPP.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEBRCPP.SetAttribute(id,"0");
										
											
							}	
								#endregion
							}
										
							//											//dblSumTotal+=int.Parse(htpremium["RPLC_CST"].ToString());
						}

							break;
						case "EBP20":
							//										//if(htpremium.Contains("PRFRD_PLS"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["PRFRD_PLS"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRFRD_PLS"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRFRD_PLS"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{
								XmlElement DecPageHomeEndoEBP20;
								DecPageHomeEndoEBP20 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEEBP20");
								DecPageRootElement.AppendChild(DecPageHomeEndoEBP20);
								DecPageHomeEndoEBP20.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEBP20.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEBP20.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEBP20.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBP20"));
									DecPageHomeEndoEBP20.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBP20"));
								}
								DecPageHomeEndoEBP20.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBP20EXTN"));
								DecPageHomeEndoEBP20.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBP20EXTN"));

								XmlElement EndoElementEBP20;
								EndoElementEBP20 = AcordPDFXML.CreateElement("ENDOELEMENTEBP20INFO");
								DecPageHomeEndoEBP20.AppendChild(EndoElementEBP20);
								EndoElementEBP20.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEBP20.SetAttribute(id,"0");
							}
								#endregion

							}
							//											//dblSumTotal+=int.Parse(htpremium["PRFRD_PLS"].ToString());
						}
									

							break;
						case "EBP22":
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{
								XmlElement DecPageHomeEndoEBP22;
								DecPageHomeEndoEBP22 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEEBP22");
								DecPageRootElement.AppendChild(DecPageHomeEndoEBP22);
								DecPageHomeEndoEBP22.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEBP22.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEBP22.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEBP22.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBP22"));
									DecPageHomeEndoEBP22.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBP22"));
								}
								DecPageHomeEndoEBP22.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBP22EXTN"));
								DecPageHomeEndoEBP22.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBP22EXTN"));

								XmlElement EndoElementEBP22;
								EndoElementEBP22 = AcordPDFXML.CreateElement("ENDOELEMENTEBP22INFO");
								DecPageHomeEndoEBP22.AppendChild(EndoElementEBP22);
								EndoElementEBP22.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEBP22.SetAttribute(id,"0");
							}		
								#endregion

							}
							break;
						case "EBP24":
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{
								XmlElement DecPageHomeEndoEBP24;
								DecPageHomeEndoEBP24 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEEBP24");
								DecPageRootElement.AppendChild(DecPageHomeEndoEBP24);
								DecPageHomeEndoEBP24.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEBP24.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEBP24.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEBP24.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBP24"));
									DecPageHomeEndoEBP24.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBP24"));
								}
								DecPageHomeEndoEBP24.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBP24EXTN"));
								DecPageHomeEndoEBP24.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBP24EXTN"));

								XmlElement EndoElementEBP24;
								EndoElementEBP24 = AcordPDFXML.CreateElement("ENDOELEMENTEBP24INFO");
								DecPageHomeEndoEBP24.AppendChild(EndoElementEBP24);
								EndoElementEBP24.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEBP24.SetAttribute(id,"0");
							
							}	
								#endregion

							}
							break;
						case "EBEP11":
							//										//if(htpremium.Contains("EXPND_RPLC_CST"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["EXPND_RPLC_CST"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["EXPND_RPLC_CST"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["EXPND_RPLC_CST"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoEBEP11;
								DecPageHomeEndoEBEP11 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBEP11");
								DecPageRootElement.AppendChild(DecPageHomeEndoEBEP11);
								DecPageHomeEndoEBEP11.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEBEP11.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEBEP11.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEBEP11.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBEP11"));
									DecPageHomeEndoEBEP11.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBEP11"));
								}
								DecPageHomeEndoEBEP11.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBEP11EXTN"));
								DecPageHomeEndoEBEP11.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBEP11EXTN"));

								XmlElement EndoElementEBEP11;
								EndoElementEBEP11 = AcordPDFXML.CreateElement("ENDOELEMENTEBEP11INFO");
								DecPageHomeEndoEBEP11.AppendChild(EndoElementEBEP11);
								EndoElementEBEP11.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEBEP11.SetAttribute(id,"0");
							}		
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["EXPND_RPLC_CST"].ToString());
						}
							break;
						case	"EBIF96"	:
							//										//if(htpremium.Contains("FIRE_DPT"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["FIRE_DPT"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["FIRE_DPT"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["FIRE_DPT"].ToString())+"</HM_ENDPREMIUM>";

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{	
								XmlElement DecPageHomeEndoEBIF96;
								DecPageHomeEndoEBIF96 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBIF96");
								DecPageRootElement.AppendChild(DecPageHomeEndoEBIF96);
								DecPageHomeEndoEBIF96.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEBIF96.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEBIF96.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEBIF96.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBIF96"));
									DecPageHomeEndoEBIF96.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBIF96"));
								}
								DecPageHomeEndoEBIF96.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBIF96EXTN"));
								DecPageHomeEndoEBIF96.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBIF96EXTN"));

								XmlElement EndoElementEBIF96;
								EndoElementEBIF96 = AcordPDFXML.CreateElement("ENDOELEMENTEBIF96INFO");
								DecPageHomeEndoEBIF96.AppendChild(EndoElementEBIF96);
								EndoElementEBIF96.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEBIF96.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["FIRE_DPT"].ToString());
						}
							break;
						case	"IBUSP"	:
							//										//if(htpremium.Contains("BSNS_PRP_INCR"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["BSNS_PRP_INCR"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BSNS_PRP_INCR"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BSNS_PRP_INCR"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{	
								XmlElement DecPageHomeEndoIBUSP;
								DecPageHomeEndoIBUSP = AcordPDFXML.CreateElement("HOMEENDORSEMENTIBUSP");
								DecPageRootElement.AppendChild(DecPageHomeEndoIBUSP);
								DecPageHomeEndoIBUSP.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoIBUSP.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoIBUSP.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoIBUSP.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEIBUSP"));
									DecPageHomeEndoIBUSP.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIBUSP"));
								}
								DecPageHomeEndoIBUSP.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEIBUSPEXTN"));
								DecPageHomeEndoIBUSP.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIBUSPEXTN"));

								XmlElement EndoElementIBUSP;
								EndoElementIBUSP = AcordPDFXML.CreateElement("ENDOELEMENTIBUSPINFO");
								DecPageHomeEndoIBUSP.AppendChild(EndoElementIBUSP);
								EndoElementIBUSP.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementIBUSP.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["BSNS_PRP_INCR"].ToString());
						}
							break;
						case	"EBOS40"	:
							//										//if(htpremium.Contains("OTHR_STR_RNT"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["OTHR_STR_RNT"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["OTHR_STR_RNT"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["OTHR_STR_RNT"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{	
								XmlElement DecPageHomeEndoEBOS40;
								DecPageHomeEndoEBOS40 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBOS40");
								DecPageRootElement.AppendChild(DecPageHomeEndoEBOS40);
								DecPageHomeEndoEBOS40.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEBOS40.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEBOS40.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEBOS40.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBOS40"));
									DecPageHomeEndoEBOS40.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBOS40"));
								}
								DecPageHomeEndoEBOS40.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBOS40EXTN"));
								DecPageHomeEndoEBOS40.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBOS40EXTN"));

								XmlElement EndoElementEBOS40;
								EndoElementEBOS40 = AcordPDFXML.CreateElement("ENDOELEMENTEBOS40INFO");
								DecPageHomeEndoEBOS40.AppendChild(EndoElementEBOS40);
								EndoElementEBOS40.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEBOS40.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["OTHR_STR_RNT"].ToString());
						}
							break;
						case	"EBPPC"	:
							break;
						case	"EBPPOP"	:
							//										//if(htpremium.Contains("PRS_PRP_AWY_PRM"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["PRS_PRP_AWY_PRM"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_PRP_AWY_PRM"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_PRP_AWY_PRM"].ToString())+"</HM_ENDPREMIUM>";

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{	
								XmlElement DecPageHomeEndoEBPPOP;
								DecPageHomeEndoEBPPOP = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBPPOP");
								DecPageRootElement.AppendChild(DecPageHomeEndoEBPPOP);
								DecPageHomeEndoEBPPOP.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEBPPOP.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEBPPOP.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEBPPOP.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBPPOP"));
									DecPageHomeEndoEBPPOP.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBPPOP"));
								}
								DecPageHomeEndoEBPPOP.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBPPOPEXTN"));
								DecPageHomeEndoEBPPOP.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBPPOPEXTN"));

								XmlElement EndoElementEBPPOP;
								EndoElementEBPPOP = AcordPDFXML.CreateElement("ENDOELEMENTEBPPOPINFO");
								DecPageHomeEndoEBPPOP.AppendChild(EndoElementEBPPOP);
								EndoElementEBPPOP.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEBPPOP.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["PRS_PRP_AWY_PRM"].ToString());
						}
							break;
						case	"EBALEXP"	:
							break;
						case	"EBOS489"	:
							//										//if(htpremium.Contains("OTHR_STR_RPR_CST"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["OTHR_STR_RPR_CST"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["OTHR_STR_RPR_CST"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["OTHR_STR_RPR_CST"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{	
								XmlElement DecPageHomeEndoEBOS489;
								DecPageHomeEndoEBOS489 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBOS489");
								DecPageRootElement.AppendChild(DecPageHomeEndoEBOS489);
								DecPageHomeEndoEBOS489.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEBOS489.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEBOS489.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEBOS489.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBOS489"));
									DecPageHomeEndoEBOS489.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBOS489"));
								}
								DecPageHomeEndoEBOS489.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBOS489EXTN"));
								DecPageHomeEndoEBOS489.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBOS489EXTN"));

								XmlElement EndoElementEBOS489;
								EndoElementEBOS489 = AcordPDFXML.CreateElement("ENDOELEMENTEBOS489INFO");
								DecPageHomeEndoEBOS489.AppendChild(EndoElementEBOS489);
								EndoElementEBOS489.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEBOS489.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["OTHR_STR_RPR_CST"].ToString());
						}
							break;
						case	"EBICC53"	:
							//if(htpremium.Contains("CRDT_CRD"))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									//gstrGetPremium	=	htpremium["CRDT_CRD"].ToString();
									//gintGetindex	=	gstrGetPremium.IndexOf(".");
									//												if(//gintGetindex==	-1)
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CRDT_CRD"].ToString() + ".00")+"</HM_ENDPREMIUM>";
									//												else
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CRDT_CRD"].ToString())+"</HM_ENDPREMIUM>";

									#region Dec Page Element
									if(gCtrlnChkHO65!=true)	
									{	
										XmlElement DecPageHomeEndoEBICC53;
										DecPageHomeEndoEBICC53 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBICC53");
										DecPageRootElement.AppendChild(DecPageHomeEndoEBICC53);
										DecPageHomeEndoEBICC53.SetAttribute(fieldType,fieldTypeMultiple);
										if(prnAttFile != null && prnAttFile.ToString() != "")
										{
											DecPageHomeEndoEBICC53.SetAttribute(PrimPDF,prnAttFile.ToString());
											DecPageHomeEndoEBICC53.SetAttribute(PrimPDFBlocks,"1");
										}
										else
										{
											DecPageHomeEndoEBICC53.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBICC53"));
											DecPageHomeEndoEBICC53.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBICC53"));
										}
										DecPageHomeEndoEBICC53.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBICC53EXTN"));
										DecPageHomeEndoEBICC53.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBICC53EXTN"));

										XmlElement EndoElementEBICC53;
										EndoElementEBICC53 = AcordPDFXML.CreateElement("ENDOELEMENTEBICC53INFO");
										DecPageHomeEndoEBICC53.AppendChild(EndoElementEBICC53);
										EndoElementEBICC53.SetAttribute(fieldType,fieldTypeNormal);
										EndoElementEBICC53.SetAttribute(id,"0");
										gCtrlnChkHO65=false;
									}	
									#endregion
								}
								//dblSumTotal+=int.Parse(htpremium["CRDT_CRD"].ToString());
							}
							break;
						case	"EBCCSL"	:
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								if(gCtrlnChkHO65!=true)	
								{												
									XmlElement DecPageHomeEndoEBCCSL;
									DecPageHomeEndoEBCCSL = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBCCSL");
									DecPageRootElement.AppendChild(DecPageHomeEndoEBCCSL);
									DecPageHomeEndoEBCCSL.SetAttribute(fieldType,fieldTypeMultiple);
									if(prnAttFile != null && prnAttFile.ToString() != "")
									{
										DecPageHomeEndoEBCCSL.SetAttribute(PrimPDF,prnAttFile.ToString());
										DecPageHomeEndoEBCCSL.SetAttribute(PrimPDFBlocks,"1");
									}
									else
									{
										DecPageHomeEndoEBCCSL.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBCCSL"));
										DecPageHomeEndoEBCCSL.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBCCSL"));
									}
									DecPageHomeEndoEBCCSL.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBCCSLEXTN"));
									DecPageHomeEndoEBCCSL.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBCCSLEXTN"));

									XmlElement EndoElementEBCCSL;
									EndoElementEBCCSL = AcordPDFXML.CreateElement("ENDOELEMENTEBCCSLINFO");
									DecPageHomeEndoEBCCSL.AppendChild(EndoElementEBCCSL);
									EndoElementEBCCSL.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementEBCCSL.SetAttribute(id,"0");
									gCtrlnChkHO65=true;
								}		
								#endregion
							}
							break;
						case	"SEWER"	:
							//										//if(htpremium.Contains("ORD_LW"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["ORD_LW"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ORD_LW"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ORD_LW"].ToString())+"</HM_ENDPREMIUM>";

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{	
								XmlElement DecPageHomeEndoSEWER;
								DecPageHomeEndoSEWER = AcordPDFXML.CreateElement("HOMEENDORSEMENTSEWER");
								DecPageRootElement.AppendChild(DecPageHomeEndoSEWER);
								DecPageHomeEndoSEWER.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoSEWER.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoSEWER.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoSEWER.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESEWER"));
									DecPageHomeEndoSEWER.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESEWER"));
								}
								DecPageHomeEndoSEWER.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESEWEREXTN"));
								DecPageHomeEndoSEWER.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESEWEREXTN"));

								XmlElement EndoElementSEWER;
								EndoElementSEWER = AcordPDFXML.CreateElement("ENDOELEMENTSEWERINFO");
								DecPageHomeEndoSEWER.AppendChild(EndoElementSEWER);
								EndoElementSEWER.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementSEWER.SetAttribute(id,"0");
							}
								#endregion											
							}
							//											//dblSumTotal+=int.Parse(htpremium["ORD_LW"].ToString());
						}
							break;
						case	"EROK"	:
							//										//if(htpremium.Contains("ERTHQKE"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["ERTHQKE"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ERTHQKE"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ERTHQKE"].ToString())+"</HM_ENDPREMIUM>";

								#region Dec Page Element
								
												 //if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
												{
													XmlElement DecPageHomeEndoEROK;
													DecPageHomeEndoEROK = AcordPDFXML.CreateElement("HOMEENDORSEMENTEROK");
													DecPageRootElement.AppendChild(DecPageHomeEndoEROK);
													DecPageHomeEndoEROK.SetAttribute(fieldType,fieldTypeMultiple);
													DecPageHomeEndoEROK.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEROK"));
													DecPageHomeEndoEROK.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEROK"));
													DecPageHomeEndoEROK.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEROKEXTN"));
													DecPageHomeEndoEROK.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEROKEXTN"));

													XmlElement EndoElementEROK;
													EndoElementEROK = AcordPDFXML.CreateElement("ENDOELEMENTEROKINFO");
													DecPageHomeEndoEROK.AppendChild(EndoElementEROK);
													EndoElementEROK.SetAttribute(fieldType,fieldTypeNormal);
													EndoElementEROK.SetAttribute(id,"0");
							
												}				
								#endregion 
							}
							//											//dblSumTotal+=int.Parse(htpremium["ERTHQKE"].ToString());
						}
							break;
						case	"FRAUD"	:
							//										//if(htpremium.Contains("ID_FRAUD"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["ID_FRAUD"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ID_FRAUD"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ID_FRAUD"].ToString())+"</HM_ENDPREMIUM>";

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{
								XmlElement DecPageHomeEndoFRAUD;
								DecPageHomeEndoFRAUD = AcordPDFXML.CreateElement("HOMEENDORSEMENTFRAUD");
								DecPageRootElement.AppendChild(DecPageHomeEndoFRAUD);
								DecPageHomeEndoFRAUD.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoFRAUD.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoFRAUD.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoFRAUD.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEFRAUD"));
									DecPageHomeEndoFRAUD.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEFRAUD"));
								}
								DecPageHomeEndoFRAUD.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEFRAUDEXTN"));
								DecPageHomeEndoFRAUD.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEFRAUDEXTN"));

								XmlElement EndoElementFRAUD;
								EndoElementFRAUD = AcordPDFXML.CreateElement("ENDOELEMENTFRAUDINFO");
								DecPageHomeEndoFRAUD.AppendChild(EndoElementFRAUD);
								EndoElementFRAUD.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementFRAUD.SetAttribute(id,"0");
							}	
								#endregion 
							}
							//											//dblSumTotal+=int.Parse(htpremium["ID_FRAUD"].ToString());
						}
							break;
						case	"EBDUC"	:
							#region Dec Page Element
							//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
						{												
							XmlElement DecPageHomeEndoEBDUC;
							DecPageHomeEndoEBDUC = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBDUC");
							DecPageRootElement.AppendChild(DecPageHomeEndoEBDUC);
							DecPageHomeEndoEBDUC.SetAttribute(fieldType,fieldTypeMultiple);
							if(prnAttFile != null && prnAttFile.ToString() != "")
							{
								DecPageHomeEndoEBDUC.SetAttribute(PrimPDF,prnAttFile.ToString());
								DecPageHomeEndoEBDUC.SetAttribute(PrimPDFBlocks,"1");
							}
							else
							{
								DecPageHomeEndoEBDUC.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBDUC"));
								DecPageHomeEndoEBDUC.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBDUC"));
							}
							DecPageHomeEndoEBDUC.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBDUCEXTN"));
							DecPageHomeEndoEBDUC.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBDUCEXTN"));

							XmlElement EndoElementEBDUC;
							EndoElementEBDUC = AcordPDFXML.CreateElement("ENDOELEMENTEBDUCINFO");
							DecPageHomeEndoEBDUC.AppendChild(EndoElementEBDUC);
							EndoElementEBDUC.SetAttribute(fieldType,fieldTypeNormal);
							EndoElementEBDUC.SetAttribute(id,"0");
						}				
							#endregion 
							break;

						case	"EBCASP"	:
							//if(htpremium.Contains("UNT_OWNR_CVG_A"))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									//gstrGetPremium	=	htpremium["UNT_OWNR_CVG_A"].ToString();
									//gintGetindex	=	gstrGetPremium.IndexOf(".");
									//												if(//gintGetindex==	-1)
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["UNT_OWNR_CVG_A"].ToString() + ".00")+"</HM_ENDPREMIUM>";
									//												else
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["UNT_OWNR_CVG_A"].ToString())+"</HM_ENDPREMIUM>";
								}
								//dblSumTotal+=int.Parse(htpremium["UNT_OWNR_CVG_A"].ToString());
							}
							break;
						case	"EBUNIT"	:
							//if(htpremium.Contains("CND_UNIT"))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									//gstrGetPremium	=	htpremium["CND_UNIT"].ToString();
									//gintGetindex	=	gstrGetPremium.IndexOf(".");
									//												if(//gintGetindex==	-1)
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CND_UNIT"].ToString() + ".00")+"</HM_ENDPREMIUM>";
									//												else
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CND_UNIT"].ToString())+"</HM_ENDPREMIUM>";
								}
								//dblSumTotal+=int.Parse(htpremium["CND_UNIT"].ToString());
							}
							break;
						case	"LAC"	:
							break;
						case	"EBAIRP"	:
							#region Dec Page Element
							//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
						{												
							XmlElement DecPageHomeEndoEBAIRP;
							DecPageHomeEndoEBAIRP = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBAIRP");
							DecPageRootElement.AppendChild(DecPageHomeEndoEBAIRP);
							DecPageHomeEndoEBAIRP.SetAttribute(fieldType,fieldTypeMultiple);
							if(prnAttFile != null && prnAttFile.ToString() != "")
							{
								DecPageHomeEndoEBAIRP.SetAttribute(PrimPDF,prnAttFile.ToString());
								DecPageHomeEndoEBAIRP.SetAttribute(PrimPDFBlocks,"1");
							}
							else
							{
								DecPageHomeEndoEBAIRP.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBAIRP"));
								DecPageHomeEndoEBAIRP.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBAIRP"));
							}
							DecPageHomeEndoEBAIRP.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBAIRPEXTN"));
							DecPageHomeEndoEBAIRP.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBAIRPEXTN"));

							XmlElement EndoElementEBAIRP;
							EndoElementEBAIRP = AcordPDFXML.CreateElement("ENDOELEMENTEBAIRPINFO");
							DecPageHomeEndoEBAIRP.AppendChild(EndoElementEBAIRP);
							EndoElementEBAIRP.SetAttribute(fieldType,fieldTypeNormal);
							EndoElementEBAIRP.SetAttribute(id,"0");
						}				
							#endregion 

							break;
						case	"EBBAA"	:
							//										//if(htpremium.Contains("BLDG_ALTR"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["BLDG_ALTR"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BLDG_ALTR"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BLDG_ALTR"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoEBBAA;
								DecPageHomeEndoEBBAA = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBBAA");
								DecPageRootElement.AppendChild(DecPageHomeEndoEBBAA);
								DecPageHomeEndoEBBAA.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEBBAA.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEBBAA.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEBBAA.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBBAA"));
									DecPageHomeEndoEBBAA.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBBAA"));
								}
								DecPageHomeEndoEBBAA.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBBAAEXTN"));
								DecPageHomeEndoEBBAA.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBBAAEXTN"));

								XmlElement EndoElementEBBAA;
								EndoElementEBBAA = AcordPDFXML.CreateElement("ENDOELEMENTEBBAAINFO");
								DecPageHomeEndoEBBAA.AppendChild(EndoElementEBBAA);
								EndoElementEBBAA.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEBBAA.SetAttribute(id,"0");
							}	
								#endregion 
							}
							//											//dblSumTotal+=int.Parse(htpremium["BLDG_ALTR"].ToString());
						}
							break;
						case	"EBRDC"	:
							//										//if(htpremium.Contains("RNT_DLX"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["RNT_DLX"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RNT_DLX"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RNT_DLX"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{										
								XmlElement DecPageHomeEndoEBRDC;
								DecPageHomeEndoEBRDC = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBRDC");
								DecPageRootElement.AppendChild(DecPageHomeEndoEBRDC);
								DecPageHomeEndoEBRDC.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEBRDC.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEBRDC.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEBRDC.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBRDC"));
									DecPageHomeEndoEBRDC.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBRDC"));
								}
								DecPageHomeEndoEBRDC.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBRDCEXTN"));
								DecPageHomeEndoEBRDC.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBRDCEXTN"));

								XmlElement EndoElementEBRDC;
								EndoElementEBRDC = AcordPDFXML.CreateElement("ENDOELEMENTEBRDCINFO");
								DecPageHomeEndoEBRDC.AppendChild(EndoElementEBRDC);
								EndoElementEBRDC.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEBRDC.SetAttribute(id,"0");
							}	
								#endregion 
							}
							//											//dblSumTotal+=int.Parse(htpremium["RNT_DLX"].ToString());
						}
							break;
						case	"EBCDC"	:
							//if(htpremium.Contains("CND_DLX"))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									//gstrGetPremium	=	htpremium["CND_DLX"].ToString();
									//gintGetindex	=	gstrGetPremium.IndexOf(".");
									//												if(//gintGetindex==	-1)
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CND_DLX"].ToString() + ".00")+"</HM_ENDPREMIUM>";
									//												else
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CND_DLX"].ToString())+"</HM_ENDPREMIUM>";

									#region Dec Page Element
									
												//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
												{									
													XmlElement DecPageHomeEndoEBCDC;
													DecPageHomeEndoEBCDC = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBCDC");
													DecPageRootElement.AppendChild(DecPageHomeEndoEBCDC);
													DecPageHomeEndoEBCDC.SetAttribute(fieldType,fieldTypeMultiple);
													DecPageHomeEndoEBCDC.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBCDC"));
													DecPageHomeEndoEBCDC.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBCDC"));
													DecPageHomeEndoEBCDC.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBCDCEXTN"));
													DecPageHomeEndoEBCDC.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBCDCEXTN"));

													XmlElement EndoElementEBCDC;
													EndoElementEBCDC = AcordPDFXML.CreateElement("ENDOELEMENTEBCDCINFO");
													DecPageHomeEndoEBCDC.AppendChild(EndoElementEBCDC);
													EndoElementEBCDC.SetAttribute(fieldType,fieldTypeNormal);
													EndoElementEBCDC.SetAttribute(id,"0");
												}
												
									#endregion 
								}
								//dblSumTotal+=int.Parse(htpremium["CND_DLX"].ToString());
							}
							break;
						case	"EBMC"	:

							break;

						case	"RECST"	:
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoRECST;
								DecPageHomeEndoRECST = AcordPDFXML.CreateElement("HOMEENDORSEMENTRECST");
								DecPageRootElement.AppendChild(DecPageHomeEndoRECST);
								DecPageHomeEndoRECST.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoRECST.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoRECST.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoRECST.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGERECST"));
									DecPageHomeEndoRECST.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGERECST"));
								}
								DecPageHomeEndoRECST.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGERECSTEXTN"));
								DecPageHomeEndoRECST.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGERECSTEXTN"));

								XmlElement EndoElementRECST;
								EndoElementRECST = AcordPDFXML.CreateElement("ENDOELEMENTRECSTINFO");
								DecPageHomeEndoRECST.AppendChild(EndoElementRECST);
								EndoElementRECST.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementRECST.SetAttribute(id,"0");
							}		
								#endregion 
							}
							break;

						case	"MIN"	:
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoMIN;
								DecPageHomeEndoMIN = AcordPDFXML.CreateElement("HOMEENDORSEMENTMIN");
								DecPageRootElement.AppendChild(DecPageHomeEndoMIN);
								DecPageHomeEndoMIN.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoMIN.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoMIN.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoMIN.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEMIN"));
									DecPageHomeEndoMIN.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEMIN"));
								}
								DecPageHomeEndoMIN.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEMINEXTN"));
								DecPageHomeEndoMIN.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEMINEXTN"));

								XmlElement EndoElementMIN;
								EndoElementMIN = AcordPDFXML.CreateElement("ENDOELEMENTMININFO");
								DecPageHomeEndoMIN.AppendChild(EndoElementMIN);
								EndoElementMIN.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementMIN.SetAttribute(id,"0");
							}		
								#endregion 
							}
							break;	
							
						case	"EBSS490"	:
							//										//if(htpremium.Contains("SPCFC_STR_PRMS"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//												DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["SPCFC_STR_PRMS"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoEBSS490;
								DecPageHomeEndoEBSS490 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBSS490");
								DecPageRootElement.AppendChild(DecPageHomeEndoEBSS490);
								DecPageHomeEndoEBSS490.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEBSS490.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEBSS490.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEBSS490.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBSS490"));
									DecPageHomeEndoEBSS490.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBSS490"));
								}
								DecPageHomeEndoEBSS490.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBSS490EXTN"));
								DecPageHomeEndoEBSS490.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBSS490EXTN"));

								XmlElement EndoElementEBSS490;
								EndoElementEBSS490 = AcordPDFXML.CreateElement("ENDOELEMENTEBSS490INFO");
								DecPageHomeEndoEBSS490.AppendChild(EndoElementEBSS490);
								EndoElementEBSS490.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEBSS490.SetAttribute(id,"0");
							}	
								#endregion 
							}
							//											//dblSumTotal+=int.Parse(htpremium["SPCFC_STR_PRMS"].ToString());
						}
							break;

						case	"EBCCSM"	:

							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								if(gCtrlnChkHO65!=true)	
								{												
									XmlElement DecPageHomeEndoEBCCSM;
									DecPageHomeEndoEBCCSM = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBCCSM");
									DecPageRootElement.AppendChild(DecPageHomeEndoEBCCSM);
									DecPageHomeEndoEBCCSM.SetAttribute(fieldType,fieldTypeMultiple);
									if(prnAttFile != null && prnAttFile.ToString() != "")
									{
										DecPageHomeEndoEBCCSM.SetAttribute(PrimPDF,prnAttFile.ToString());
										DecPageHomeEndoEBCCSM.SetAttribute(PrimPDFBlocks,"1");
									}
									else
									{
										DecPageHomeEndoEBCCSM.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBCCSM"));
										DecPageHomeEndoEBCCSM.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBCCSM"));
									}
									DecPageHomeEndoEBCCSM.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBCCSMEXTN"));
									DecPageHomeEndoEBCCSM.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBCCSMEXTN"));

									XmlElement EndoElementEBCCSM;
									EndoElementEBCCSM = AcordPDFXML.CreateElement("ENDOELEMENTEBCCSMINFO");
									DecPageHomeEndoEBCCSM.AppendChild(EndoElementEBCCSM);
									EndoElementEBCCSM.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementEBCCSM.SetAttribute(id,"0");
									gCtrlnChkHO65=true;
								}		
								#endregion 
							}
							break;

						case	"ESCCSS"	:
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								if(gCtrlnChkHO65!=true)	
								{													
									XmlElement DecPageHomeEndoESCCSS;
									DecPageHomeEndoESCCSS = AcordPDFXML.CreateElement("HOMEENDORSEMENTESCCSS");
									DecPageRootElement.AppendChild(DecPageHomeEndoESCCSS);
									DecPageHomeEndoESCCSS.SetAttribute(fieldType,fieldTypeMultiple);
									if(prnAttFile != null && prnAttFile.ToString() != "")
									{
										DecPageHomeEndoESCCSS.SetAttribute(PrimPDF,prnAttFile.ToString());
										DecPageHomeEndoESCCSS.SetAttribute(PrimPDFBlocks,"1");
									}
									else
									{
										DecPageHomeEndoESCCSS.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEESCCSS"));
										DecPageHomeEndoESCCSS.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEESCCSS"));
									}
									DecPageHomeEndoESCCSS.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEESCCSSEXTN"));
									DecPageHomeEndoESCCSS.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEESCCSSEXTN"));

									XmlElement EndoElementESCCSS;
									EndoElementESCCSS = AcordPDFXML.CreateElement("ENDOELEMENTESCCSSINFO");
									DecPageHomeEndoESCCSS.AppendChild(EndoElementESCCSS);
									EndoElementESCCSS.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementESCCSS.SetAttribute(id,"0");
									gCtrlnChkHO65=true;
								}		
								#endregion 
							}
							break;

						case	"EBCCSI"	:
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								if(gCtrlnChkHO65!=true)	
								{													
									XmlElement DecPageHomeEndoEBCCSI;
									DecPageHomeEndoEBCCSI = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBCCSI");
									DecPageRootElement.AppendChild(DecPageHomeEndoEBCCSI);
									DecPageHomeEndoEBCCSI.SetAttribute(fieldType,fieldTypeMultiple);
									if(prnAttFile != null && prnAttFile.ToString() != "")
									{
										DecPageHomeEndoEBCCSI.SetAttribute(PrimPDF,prnAttFile.ToString());
										DecPageHomeEndoEBCCSI.SetAttribute(PrimPDFBlocks,"1");
									}
									else
									{
										DecPageHomeEndoEBCCSI.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBCCSI"));
										DecPageHomeEndoEBCCSI.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBCCSI"));
									}
									DecPageHomeEndoEBCCSI.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBCCSIEXTN"));
									DecPageHomeEndoEBCCSI.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBCCSIEXTN"));

									XmlElement EndoElementEBCCSI;
									EndoElementEBCCSI = AcordPDFXML.CreateElement("ENDOELEMENTEBCCSIINFO");
									DecPageHomeEndoEBCCSI.AppendChild(EndoElementEBCCSI);
									EndoElementEBCCSI.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementEBCCSI.SetAttribute(id,"0");
									gCtrlnChkHO65=true;
								}		
								#endregion
							}
							break;

						case	"EBCCSF"	:

							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								if(gCtrlnChkHO65!=true)	
								{													
									XmlElement DecPageHomeEndoEBCCSF;
									DecPageHomeEndoEBCCSF = AcordPDFXML.CreateElement("HOMEENDORSEMENTEBCCSF");
									DecPageRootElement.AppendChild(DecPageHomeEndoEBCCSF);
									DecPageHomeEndoEBCCSF.SetAttribute(fieldType,fieldTypeMultiple);
									if(prnAttFile != null && prnAttFile.ToString() != "")
									{
										DecPageHomeEndoEBCCSF.SetAttribute(PrimPDF,prnAttFile.ToString());
										DecPageHomeEndoEBCCSF.SetAttribute(PrimPDFBlocks,"1");
									}
									else
									{
										DecPageHomeEndoEBCCSF.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEBCCSF"));
										DecPageHomeEndoEBCCSF.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBCCSF"));
									}
									DecPageHomeEndoEBCCSF.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEBCCSFEXTN"));
									DecPageHomeEndoEBCCSF.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEBCCSFEXTN"));

									XmlElement EndoElementEBCCSF;
									EndoElementEBCCSF = AcordPDFXML.CreateElement("ENDOELEMENTEBCCSFINFO");
									DecPageHomeEndoEBCCSF.AppendChild(EndoElementEBCCSF);
									EndoElementEBCCSF.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementEBCCSF.SetAttribute(id,"0");
									gCtrlnChkHO65=true;
								}		
								#endregion
							}
							break;


						case	"WBSPO"	:
							//										//if(htpremium.Contains("BK_UP_SM_PMP"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["BK_UP_SM_PMP"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BK_UP_SM_PMP"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BK_UP_SM_PMP"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoWBSPO;
								DecPageHomeEndoWBSPO = AcordPDFXML.CreateElement("HOMEENDORSEMENTWBSPO");
								DecPageRootElement.AppendChild(DecPageHomeEndoWBSPO);
								DecPageHomeEndoWBSPO.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoWBSPO.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoWBSPO.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoWBSPO.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEWBSPO"));
									DecPageHomeEndoWBSPO.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEWBSPO"));
								}
								DecPageHomeEndoWBSPO.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEWBSPOEXTN"));
								DecPageHomeEndoWBSPO.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEWBSPOEXTN"));

								XmlElement EndoElementWBSPO;
								EndoElementWBSPO = AcordPDFXML.CreateElement("ENDOELEMENTWBSPOINFO");
								DecPageHomeEndoWBSPO.AppendChild(EndoElementWBSPO);
								EndoElementWBSPO.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementWBSPO.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["BK_UP_SM_PMP"].ToString());
						}
							break;


						case	"APOBI"	:
							//if(htpremium.Contains("ADDL_PRM_OCC_INS"))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									//gstrGetPremium	=	htpremium["ADDL_PRM_OCC_INS"].ToString();
									//gintGetindex	=	gstrGetPremium.IndexOf(".");
									//												if(//gintGetindex==	-1)
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ADDL_PRM_OCC_INS"].ToString() + ".00")+"</HM_ENDPREMIUM>";
									//												else
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ADDL_PRM_OCC_INS"].ToString())+"</HM_ENDPREMIUM>";
								}
								//dblSumTotal+=int.Parse(htpremium["ADDL_PRM_OCC_INS"].ToString());
							}
							break;


						case	"APRPR"	:
							//										//if(htpremium.Contains("ADDL_PRM_RNTD_OTH_RES"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["ADDL_PRM_RNTD_OTH_RES"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ADDL_PRM_RNTD_OTH_RES"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ADDL_PRM_RNTD_OTH_RES"].ToString())+"</HM_ENDPREMIUM>";

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoAPRPR;
								DecPageHomeEndoAPRPR = AcordPDFXML.CreateElement("HOMEENDORSEMENTAPRPR");
								DecPageRootElement.AppendChild(DecPageHomeEndoAPRPR);
								DecPageHomeEndoAPRPR.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoAPRPR.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoAPRPR.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoAPRPR.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAPRPR"));
									DecPageHomeEndoAPRPR.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAPRPR"));
								}
								DecPageHomeEndoAPRPR.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEAPRPREXTN"));
								DecPageHomeEndoAPRPR.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAPRPREXTN"));

								XmlElement EndoElementAPRPR;
								EndoElementAPRPR = AcordPDFXML.CreateElement("ENDOELEMENTAPRPRINFO");
								DecPageHomeEndoAPRPR.AppendChild(EndoElementAPRPR);
								EndoElementAPRPR.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementAPRPR.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["ADDL_PRM_RNTD_OTH_RES"].ToString());
						}
							break;


						case	"APOLR"	:
							//if(htpremium.Contains("ADDL_PRM_RNTD_1FMLY"))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									//gstrGetPremium	=	htpremium["ADDL_PRM_RNTD_1FMLY"].ToString();
									//gintGetindex	=	gstrGetPremium.IndexOf(".");
									//												if(//gintGetindex==	-1)
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ADDL_PRM_RNTD_1FMLY"].ToString() + ".00")+"</HM_ENDPREMIUM>";
									//												else
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ADDL_PRM_RNTD_1FMLY"].ToString())+"</HM_ENDPREMIUM>";
								}
								//dblSumTotal+=int.Parse(htpremium["ADDL_PRM_RNTD_1FMLY"].ToString());
							}
							break;


						case	"APOLO"	:
							//if(htpremium.Contains("ADDL_PRM_RNTD_2FMLY"))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									//gstrGetPremium	=	htpremium["ADDL_PRM_RNTD_2FMLY"].ToString();
									//gintGetindex	=	gstrGetPremium.IndexOf(".");
									//												if(//gintGetindex==	-1)
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ADDL_PRM_RNTD_2FMLY"].ToString() + ".00")+"</HM_ENDPREMIUM>";
									//												else
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["ADDL_PRM_RNTD_2FMLY"].ToString())+"</HM_ENDPREMIUM>";

								}
								//dblSumTotal+=int.Parse(htpremium["ADDL_PRM_RNTD_2FMLY"].ToString());
							}
							break;


						case	"IOPSS"	:
							//										//if(htpremium.Contains("INCDT_OFCE_PRV_SCHL_PRM"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["INCDT_OFCE_PRV_SCHL_PRM"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCDT_OFCE_PRV_SCHL_PRM"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCDT_OFCE_PRV_SCHL_PRM"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoIOPSS;
								DecPageHomeEndoIOPSS = AcordPDFXML.CreateElement("HOMEENDORSEMENTIOPSS");
								DecPageRootElement.AppendChild(DecPageHomeEndoIOPSS);
								DecPageHomeEndoIOPSS.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoIOPSS.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoIOPSS.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoIOPSS.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEIOPSS"));
									DecPageHomeEndoIOPSS.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIOPSS"));
								}
								DecPageHomeEndoIOPSS.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEIOPSSEXTN"));
								DecPageHomeEndoIOPSS.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIOPSSEXTN"));

								XmlElement EndoElementIOPSS;
								EndoElementIOPSS = AcordPDFXML.CreateElement("ENDOELEMENTIOPSSINFO");
								DecPageHomeEndoIOPSS.AppendChild(EndoElementIOPSS);
								EndoElementIOPSS.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementIOPSS.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["INCDT_OFCE_PRV_SCHL_PRM"].ToString());
						}
							break;


						case	"IOPSL"	:
							break;


						case	"IOPSI"	:
							//										//if(htpremium.Contains("INCDT_OFCE_PRV_SCHL_INST"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["INCDT_OFCE_PRV_SCHL_INST"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCDT_OFCE_PRV_SCHL_INST"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCDT_OFCE_PRV_SCHL_INST"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoIOPSI;
								DecPageHomeEndoIOPSI = AcordPDFXML.CreateElement("HOMEENDORSEMENTIOPSI");
								DecPageRootElement.AppendChild(DecPageHomeEndoIOPSI);
								DecPageHomeEndoIOPSI.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoIOPSI.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoIOPSI.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoIOPSI.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEIOPSI"));
									DecPageHomeEndoIOPSI.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIOPSI"));
								}
								DecPageHomeEndoIOPSI.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEIOPSIEXTN"));
								DecPageHomeEndoIOPSI.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIOPSIEXTN"));

								XmlElement EndoElementIOPSI;
								EndoElementIOPSI = AcordPDFXML.CreateElement("ENDOELEMENTIOPSIINFO");
								DecPageHomeEndoIOPSI.AppendChild(EndoElementIOPSI);
								EndoElementIOPSI.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementIOPSI.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["INCDT_OFCE_PRV_SCHL_INST"].ToString());
						}
							break;


						case	"IOPSO"	:
							//										//if(htpremium.Contains("INCDT_OFCE_PRV_SCHL_OFF_PRM"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["INCDT_OFCE_PRV_SCHL_OFF_PRM"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCDT_OFCE_PRV_SCHL_OFF_PRM"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCDT_OFCE_PRV_SCHL_OFF_PRM"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoIOPSO;
								DecPageHomeEndoIOPSO = AcordPDFXML.CreateElement("HOMEENDORSEMENTIOPSO");
								DecPageRootElement.AppendChild(DecPageHomeEndoIOPSO);
								DecPageHomeEndoIOPSO.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoIOPSO.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoIOPSO.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoIOPSO.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEIOPSO"));
									DecPageHomeEndoIOPSO.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIOPSO"));
								}
								DecPageHomeEndoIOPSO.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEIOPSOEXTN"));
								DecPageHomeEndoIOPSO.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIOPSOEXTN"));

								XmlElement EndoElementIOPSO;
								EndoElementIOPSO = AcordPDFXML.CreateElement("ENDOELEMENTIOPSOINFO");
								DecPageHomeEndoIOPSO.AppendChild(EndoElementIOPSO);
								EndoElementIOPSO.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementIOPSO.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["INCDT_OFCE_PRV_SCHL_OFF_PRM"].ToString());
						}
							break;


						case	"PERIJ"	:
							//										//if(htpremium.Contains("PRS_INJRY"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["PRS_INJRY"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_INJRY"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_INJRY"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoPERIJ;
								DecPageHomeEndoPERIJ = AcordPDFXML.CreateElement("HOMEENDORSEMENTPERIJ");
								DecPageRootElement.AppendChild(DecPageHomeEndoPERIJ);
								DecPageHomeEndoPERIJ.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoPERIJ.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoPERIJ.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoPERIJ.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEPERIJ"));
									DecPageHomeEndoPERIJ.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEPERIJ"));
								}
								DecPageHomeEndoPERIJ.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEPERIJEXTN"));
								DecPageHomeEndoPERIJ.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEPERIJEXTN"));

								XmlElement EndoElementPERIJ;
								EndoElementPERIJ = AcordPDFXML.CreateElement("ENDOELEMENTPERIJINFO");
								DecPageHomeEndoPERIJ.AppendChild(EndoElementPERIJ);
								EndoElementPERIJ.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementPERIJ.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["PRS_INJRY"].ToString());
						}
							break;


						case	"REEMN"	:
							//if(htpremium.Contains("RESI_EMPL"))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									//gstrGetPremium	=	htpremium["RESI_EMPL"].ToString();
									//gintGetindex	=	gstrGetPremium.IndexOf(".");
									//												if(//gintGetindex==	-1)
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RESI_EMPL"].ToString() + ".00")+"</HM_ENDPREMIUM>";
									//												else
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RESI_EMPL"].ToString())+"</HM_ENDPREMIUM>";
								}
								//dblSumTotal+=int.Parse(htpremium["RESI_EMPL"].ToString());
							}
							break;

						case	"BPCES"	:

							//										//if(htpremium.Contains("BSNS_PRSUIT"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoBPCES;
								DecPageHomeEndoBPCES = AcordPDFXML.CreateElement("HOMEENDORSEMENTBPCES");
								DecPageRootElement.AppendChild(DecPageHomeEndoBPCES);
								DecPageHomeEndoBPCES.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoBPCES.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoBPCES.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoBPCES.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEBPCES"));
									DecPageHomeEndoBPCES.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBPCES"));
								}
								DecPageHomeEndoBPCES.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEBPCESEXTN"));
								DecPageHomeEndoBPCES.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBPCESEXTN"));

								XmlElement EndoElementBPCES;
								EndoElementBPCES = AcordPDFXML.CreateElement("ENDOELEMENTBPCESINFO");
								DecPageHomeEndoBPCES.AppendChild(EndoElementBPCES);
								EndoElementBPCES.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementBPCES.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["BSNS_PRSUIT"].ToString());
						}
							break;

						case	"BPSCM"	:
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoBPSCM;
								DecPageHomeEndoBPSCM = AcordPDFXML.CreateElement("HOMEENDORSEMENTBPSCM");
								DecPageRootElement.AppendChild(DecPageHomeEndoBPSCM);
								DecPageHomeEndoBPSCM.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoBPSCM.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoBPSCM.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoBPSCM.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEBPSCM"));
									DecPageHomeEndoBPSCM.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBPSCM"));
								}
								DecPageHomeEndoBPSCM.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEBPSCMEXTN"));
								DecPageHomeEndoBPSCM.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBPSCMEXTN"));

								XmlElement EndoElementBPSCM;
								EndoElementBPSCM = AcordPDFXML.CreateElement("ENDOELEMENTBPSCMINFO");
								DecPageHomeEndoBPSCM.AppendChild(EndoElementBPSCM);
								EndoElementBPSCM.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementBPSCM.SetAttribute(id,"0");
							}		
								#endregion
							}
							break;

						case	"BPTAL"	:
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoBPTAL;
								DecPageHomeEndoBPTAL = AcordPDFXML.CreateElement("HOMEENDORSEMENTBPTAL");
								DecPageRootElement.AppendChild(DecPageHomeEndoBPTAL);
								DecPageHomeEndoBPTAL.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoBPTAL.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoBPTAL.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoBPTAL.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEBPTAL"));
									DecPageHomeEndoBPTAL.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBPTAL"));
								}
								DecPageHomeEndoBPTAL.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEBPTALEXTN"));
								DecPageHomeEndoBPTAL.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBPTALEXTN"));

								XmlElement EndoElementBPTAL;
								EndoElementBPTAL = AcordPDFXML.CreateElement("ENDOELEMENTBPTALINFO");
								DecPageHomeEndoBPTAL.AppendChild(EndoElementBPTAL);
								EndoElementBPTAL.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementBPTAL.SetAttribute(id,"0");
							}		
								#endregion
							}
							break;


						case	"BPTNO"	:

							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoBPTNO;
								DecPageHomeEndoBPTNO = AcordPDFXML.CreateElement("HOMEENDORSEMENTBPTNO");
								DecPageRootElement.AppendChild(DecPageHomeEndoBPTNO);
								DecPageHomeEndoBPTNO.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoBPTNO.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoBPTNO.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoBPTNO.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEBPTNO"));
									DecPageHomeEndoBPTNO.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBPTNO"));
								}
								DecPageHomeEndoBPTNO.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEBPTNOEXTN"));
								DecPageHomeEndoBPTNO.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEBPTNOEXTN"));

								XmlElement EndoElementBPTNO;
								EndoElementBPTNO = AcordPDFXML.CreateElement("ENDOELEMENTBPTNOINFO");
								DecPageHomeEndoBPTNO.AppendChild(EndoElementBPTNO);
								EndoElementBPTNO.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementBPTNO.SetAttribute(id,"0");
							}		
								#endregion
							}
							break;


						case	"FLIFP"	:
							//										//if(htpremium.Contains("INCT_FRMG_RESI_PRM"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["INCT_FRMG_RESI_PRM"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCT_FRMG_RESI_PRM"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCT_FRMG_RESI_PRM"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoFLIFP;
								DecPageHomeEndoFLIFP = AcordPDFXML.CreateElement("HOMEENDORSEMENTFLIFP");
								DecPageRootElement.AppendChild(DecPageHomeEndoFLIFP);
								DecPageHomeEndoFLIFP.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoFLIFP.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoFLIFP.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoFLIFP.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEFLIFP"));
									DecPageHomeEndoFLIFP.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEFLIFP"));
								}
								DecPageHomeEndoFLIFP.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEFLIFPEXTN"));
								DecPageHomeEndoFLIFP.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEFLIFPEXTN"));

								XmlElement EndoElementFLIFP;
								EndoElementFLIFP = AcordPDFXML.CreateElement("ENDOELEMENTFLIFPINFO");
								DecPageHomeEndoFLIFP.AppendChild(EndoElementFLIFP);
								EndoElementFLIFP.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementFLIFP.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["INCT_FRMG_RESI_PRM"].ToString());
						}
							break;
						case	"FLOFO"	:
							//										//if(htpremium.Contains("INCT_FRMG_OPRT_INS"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["INCT_FRMG_OPRT_INS"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCT_FRMG_OPRT_INS"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCT_FRMG_OPRT_INS"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
																			
												//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
												{
													XmlElement DecPageHomeEndoFLOFO;
													DecPageHomeEndoFLOFO = AcordPDFXML.CreateElement("HOMEENDORSEMENTFLOFO");
													DecPageRootElement.AppendChild(DecPageHomeEndoFLOFO);
													DecPageHomeEndoFLOFO.SetAttribute(fieldType,fieldTypeMultiple);
													DecPageHomeEndoFLOFO.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEFLOFO"));
													DecPageHomeEndoFLOFO.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEFLOFO"));
													DecPageHomeEndoFLOFO.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEFLOFOEXTN"));
													DecPageHomeEndoFLOFO.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEFLOFOEXTN"));

													XmlElement EndoElementFLOFO;
													EndoElementFLOFO = AcordPDFXML.CreateElement("ENDOELEMENTFLOFOINFO");
													DecPageHomeEndoFLOFO.AppendChild(EndoElementFLOFO);
													EndoElementFLOFO.SetAttribute(fieldType,fieldTypeNormal);
													EndoElementFLOFO.SetAttribute(id,"0");
												}
																
								#endregion
										
							}
							//											//dblSumTotal+=int.Parse(htpremium["INCT_FRMG_OPRT_INS"].ToString());
						}
							break;
						case	"FLOFR"	:
							//if(htpremium.Contains("INCT_FRMG_RNTD_OTH"))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									//gstrGetPremium	=	htpremium["INCT_FRMG_RNTD_OTH"].ToString();
									//gintGetindex	=	gstrGetPremium.IndexOf(".");
									//												if(//gintGetindex==	-1)
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCT_FRMG_RNTD_OTH"].ToString() + ".00")+"</HM_ENDPREMIUM>";
									//												else
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["INCT_FRMG_RNTD_OTH"].ToString() )+"</HM_ENDPREMIUM>";
								}
								//dblSumTotal+=int.Parse(htpremium["INCT_FRMG_RNTD_OTH"].ToString());
							}
							break;


						case	"OSTDISH"	:
									
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoOSTDISH;
								DecPageHomeEndoOSTDISH = AcordPDFXML.CreateElement("HOMEENDORSEMENTOSTDISH");
								DecPageRootElement.AppendChild(DecPageHomeEndoOSTDISH);
								DecPageHomeEndoOSTDISH.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoOSTDISH.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoOSTDISH.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoOSTDISH.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEOSTDISH"));
									DecPageHomeEndoOSTDISH.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOSTDISH"));
								}
								DecPageHomeEndoOSTDISH.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEOSTDISHEXTN"));
								DecPageHomeEndoOSTDISH.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEOSTDISHEXTN"));

								XmlElement EndoElementOSTDISH;
								EndoElementOSTDISH = AcordPDFXML.CreateElement("ENDOELEMENTOSTDISHINFO");
								DecPageHomeEndoOSTDISH.AppendChild(EndoElementOSTDISH);
								EndoElementOSTDISH.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementOSTDISH.SetAttribute(id,"0");
							}		
								#endregion
							}
							break;


						case	"HO200"	:
							//										//if(htpremium.Contains("WTRBD_LBLTY"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["WTRBD_LBLTY"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["WTRBD_LBLTY"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["WTRBD_LBLTY"].ToString())+"</HM_ENDPREMIUM>";
											
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoHO200;
								DecPageHomeEndoHO200 = AcordPDFXML.CreateElement("HOMEENDORSEMENTHO200");
								DecPageRootElement.AppendChild(DecPageHomeEndoHO200);
								DecPageHomeEndoHO200.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoHO200.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoHO200.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoHO200.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHO200"));
									DecPageHomeEndoHO200.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO200"));
								}
								DecPageHomeEndoHO200.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHO200EXTN"));
								DecPageHomeEndoHO200.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO200EXTN"));

								XmlElement EndoElementHO200;
								EndoElementHO200 = AcordPDFXML.CreateElement("ENDOELEMENTHO200INFO");
								DecPageHomeEndoHO200.AppendChild(EndoElementHO200);
								EndoElementHO200.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementHO200.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["WTRBD_LBLTY"].ToString());
						}
							break;
						case	"HO9"	:
							//										//if(htpremium.Contains("SUB_SRFCE_WTR"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["SUB_SRFCE_WTR"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["SUB_SRFCE_WTR"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["SUB_SRFCE_WTR"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoHO9;
								DecPageHomeEndoHO9 = AcordPDFXML.CreateElement("HOMEENDORSEMENTHO9");
								DecPageRootElement.AppendChild(DecPageHomeEndoHO9);
								DecPageHomeEndoHO9.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoHO9.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoHO9.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoHO9.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHO9"));
									DecPageHomeEndoHO9.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO9"));
								}
								DecPageHomeEndoHO9.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHO9EXTN"));
								DecPageHomeEndoHO9.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO9EXTN"));

								XmlElement EndoElementHO9;
								EndoElementHO9 = AcordPDFXML.CreateElement("ENDOELEMENTHO9INFO");
								DecPageHomeEndoHO9.AppendChild(EndoElementHO9);
								EndoElementHO9.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementHO9.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["SUB_SRFCE_WTR"].ToString());
						}
							break;
						case	"REDUC"	:

							break;
						case	"BICYC"	:
							//if(htpremium.Contains("BICYCLE"))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									//gstrGetPremium	=	htpremium["BICYCLE"].ToString();
									//gintGetindex	=	gstrGetPremium.IndexOf(".");
									//												if(//gintGetindex==	-1)
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BICYCLE"].ToString() + ".00")+"</HM_ENDPREMIUM>";
									//												else
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["BICYCLE"].ToString())+"</HM_ENDPREMIUM>";
								}
								//dblSumTotal+=int.Parse(htpremium["BICYCLE"].ToString());
							}
							break;
						case	"CAMER"	:
							//if(htpremium.Contains("CMRA"))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									//gstrGetPremium	=	htpremium["CMRA"].ToString();
									//gintGetindex	=	gstrGetPremium.IndexOf(".");
									//												if(//gintGetindex==	-1)
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CMRA"].ToString() + ".00")+"</HM_ENDPREMIUM>";
									//												else
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CMRA"].ToString())+"</HM_ENDPREMIUM>";
								}
								//dblSumTotal+=int.Parse(htpremium["CMRA"].ToString());
							}
							break;
						case	"CELLU"	:
							//if(htpremium.Contains("CELL"))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									//gstrGetPremium	=	htpremium["CELL"].ToString();
									//gintGetindex	=	gstrGetPremium.IndexOf(".");
									//												if(//gintGetindex==	-1)
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CELL"].ToString() + ".00")+"</HM_ENDPREMIUM>";
									//												else
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["CELL"].ToString())+"</HM_ENDPREMIUM>";
								}
								//dblSumTotal+=int.Parse(htpremium["CELL"].ToString());
							}
							break;
						case	"FINEBR"	:
							//if(htpremium.Contains("FNE_ART_WTH_BRK"))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									//gstrGetPremium	=	htpremium["FNE_ART_WTH_BRK"].ToString();
									//gintGetindex	=	gstrGetPremium.IndexOf(".");
									//												if(//gintGetindex==	-1)
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["FNE_ART_WTH_BRK"].ToString() + ".00")+"</HM_ENDPREMIUM>";
									//												else
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["FNE_ART_WTH_BRK"].ToString())+"</HM_ENDPREMIUM>";
								}
								//dblSumTotal+=int.Parse(htpremium["FNE_ART_WTH_BRK"].ToString());
							}
							break;
						case	"FINEWBR"	:
							//if(htpremium.Contains("FNE_ART_WO_BRK"))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									//gstrGetPremium	=	htpremium["FNE_ART_WO_BRK"].ToString();
									//gintGetindex	=	gstrGetPremium.IndexOf(".");
									//												if(//gintGetindex==	-1)
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["FNE_ART_WO_BRK"].ToString() + ".00")+"</HM_ENDPREMIUM>";
									//												else
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["FNE_ART_WO_BRK"].ToString())+"</HM_ENDPREMIUM>";
								}
								//dblSumTotal+=int.Parse(htpremium["FNE_ART_WO_BRK"].ToString());
							}
							break;
						case	"FURS"	:
							//if(htpremium.Contains("FUR"))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									//gstrGetPremium	=	htpremium["FUR"].ToString();
									//gintGetindex	=	gstrGetPremium.IndexOf(".");
									//												if(//gintGetindex==	-1)
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["FUR"].ToString() + ".00")+"</HM_ENDPREMIUM>";
									//												else
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["FUR"].ToString())+"</HM_ENDPREMIUM>";
								}
								//dblSumTotal+=int.Parse(htpremium["FUR"].ToString());
							}
							break;
						case	"GOLF"	:
							//if(htpremium.Contains("GOLF"))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									//gstrGetPremium	=	htpremium["GOLF"].ToString();
									//gintGetindex	=	gstrGetPremium.IndexOf(".");
									//												if(//gintGetindex==	-1)
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["GOLF"].ToString() + ".00")+"</HM_ENDPREMIUM>";
									//												else
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["GOLF"].ToString())+"</HM_ENDPREMIUM>";
								}
								//dblSumTotal+=int.Parse(htpremium["GOLF"].ToString());
							}
							break;
						case	"GUNS"	:
							//if(htpremium.Contains("GUN"))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									//gstrGetPremium	=	htpremium["GUN"].ToString();
									//gintGetindex	=	gstrGetPremium.IndexOf(".");
									//												if(//gintGetindex==	-1)
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["GUN"].ToString() + ".00")+"</HM_ENDPREMIUM>";
									//												else
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["GUN"].ToString())+"</HM_ENDPREMIUM>";
								}
								//dblSumTotal+=int.Parse(htpremium["GUN"].ToString());
							}
							break;
						case	"HANDI"	:
							break;
						case	"HEARI"	:
							break;
						case	"INSUL"	:
							break;
						case	"JEWEL"	:
							//if(htpremium.Contains("JWL"))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									//gstrGetPremium	=	htpremium["JWL"].ToString();
									//gintGetindex	=	gstrGetPremium.IndexOf(".");
									//												if(//gintGetindex==	-1)
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["JWL"].ToString() + ".00")+"</HM_ENDPREMIUM>";
									//												else
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["JWL"].ToString())+"</HM_ENDPREMIUM>";
								}
								//dblSumTotal+=int.Parse(htpremium["JWL"].ToString());
							}
							break;
						case	"MART"	:
							break;
						case	"MUSIC"	:
							//if(htpremium.Contains("MSC_NON_PRF"))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									//gstrGetPremium	=	htpremium["MSC_NON_PRF"].ToString();
									//gintGetindex	=	gstrGetPremium.IndexOf(".");
									//												if(//gintGetindex==	-1)
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["MSC_NON_PRF"].ToString() + ".00")+"</HM_ENDPREMIUM>";
									//												else
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["MSC_NON_PRF"].ToString())+"</HM_ENDPREMIUM>";
								}
								//dblSumTotal+=int.Parse(htpremium["MSC_NON_PRF"].ToString());
							}
							break;
						case	"PERSOD"	:
							//if(htpremium.Contains("PRS_CMP"))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									//gstrGetPremium	=	htpremium["PRS_CMP"].ToString();
									//gintGetindex	=	gstrGetPremium.IndexOf(".");
									//												if(//gintGetindex==	-1)
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_CMP"].ToString() + ".00")+"</HM_ENDPREMIUM>";
									//												else
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_CMP"].ToString())+"</HM_ENDPREMIUM>";
								}
								//dblSumTotal+=int.Parse(htpremium["PRS_CMP"].ToString());
							}
									
							break;
						case	"PERSOL"	:
							//if(htpremium.Contains("PRS_CMP"))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									//gstrGetPremium	=	htpremium["PRS_CMP"].ToString();
									//gintGetindex	=	gstrGetPremium.IndexOf(".");
									//												if(//gintGetindex==	-1)
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_CMP"].ToString() + ".00")+"</HM_ENDPREMIUM>";
									//												else
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_CMP"].ToString())+"</HM_ENDPREMIUM>";

								}
								////dblSumTotal+=int.Parse(htpremium["PRS_CMP"].ToString());
							}
										
							break;
						case	"RARE"	:
							//if(htpremium.Contains("RARE_COIN"))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									//gstrGetPremium	=	htpremium["RARE_COIN"].ToString();
									//gintGetindex	=	gstrGetPremium.IndexOf(".");
									//												if(//gintGetindex==	-1)
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RARE_COIN"].ToString() + ".00")+"</HM_ENDPREMIUM>";
									//												else
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["RARE_COIN"].ToString())+"</HM_ENDPREMIUM>";

								}
								////dblSumTotal+=int.Parse(htpremium["RARE_COIN"].ToString());
							}
							break;
						case	"SALES"	:
							break;
						case	"SCUBA"	:
							break;
						case	"SILVE"	:
							//if(htpremium.Contains("SLVR"))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									//gstrGetPremium	=	htpremium["SLVR"].ToString();
									//gintGetindex	=	gstrGetPremium.IndexOf(".");
									//												if(//gintGetindex==	-1)
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["SLVR"].ToString() + ".00")+"</HM_ENDPREMIUM>";
									//												else
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["SLVR"].ToString())+"</HM_ENDPREMIUM>";
								}
								////dblSumTotal+=int.Parse(htpremium["SLVR"].ToString());
							}
							break;
						case	"SNOW"	:
							break;
						case	"STAMP"	:
							//if(htpremium.Contains("STMP"))
							{
								if (gStrPdfFor == PDFForDecPage)
								{
									//gstrGetPremium	=	htpremium["STMP"].ToString();
									//gintGetindex	=	gstrGetPremium.IndexOf(".");
									//												if(//gintGetindex==	-1)
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["STMP"].ToString() + ".00")+"</HM_ENDPREMIUM>";
									//												else
									//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["STMP"].ToString())+"</HM_ENDPREMIUM>";
								}
								////dblSumTotal+=int.Parse(htpremium["STMP"].ToString());
							}
							break;
						case	"TACK"	:
							break;
						case	"TOOLSP"	:
							break;
						case	"TOOLSB"	:
							break;
						case	"TRACT"	:
							break;
						case	"TRAIN"	:
							break;
						case	"WHEEL"	:
							break;
						case	"EOP17"	:

							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{	
								XmlElement DecPageHomeEndoEOP17;
								DecPageHomeEndoEOP17 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEOP17");
								DecPageRootElement.AppendChild(DecPageHomeEndoEOP17);
								DecPageHomeEndoEOP17.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoEOP17.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoEOP17.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoEOP17.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEOP17"));
									DecPageHomeEndoEOP17.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEOP17"));
								}
								DecPageHomeEndoEOP17.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEOP17EXTN"));
								DecPageHomeEndoEOP17.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEOP17EXTN"));

								XmlElement EndoElementEOP17;
								EndoElementEOP17 = AcordPDFXML.CreateElement("ENDOELEMENTEOP17INFO");
								DecPageHomeEndoEOP17.AppendChild(EndoElementEOP17);
								EndoElementEOP17.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementEOP17.SetAttribute(id,"0");
							}		
								#endregion
							}

							break;
						case	"ECOB"	:
							break;
						case	"ECOC"	:
							break;
						case	"BUMC"	:
							break;
						case	"LF330"	:
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoLF330;
								DecPageHomeEndoLF330 = AcordPDFXML.CreateElement("HOMEENDORSEMENTLF330");
								DecPageRootElement.AppendChild(DecPageHomeEndoLF330);
								DecPageHomeEndoLF330.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoLF330.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoLF330.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoLF330.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGELF330"));
									DecPageHomeEndoLF330.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGELF330"));
								}
								DecPageHomeEndoLF330.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGELF330EXTN"));
								DecPageHomeEndoLF330.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGELF330EXTN"));

								XmlElement EndoElementLF330;
								EndoElementLF330 = AcordPDFXML.CreateElement("ENDOELEMENTLF330INFO");
								DecPageHomeEndoLF330.AppendChild(EndoElementLF330);
								EndoElementLF330.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementLF330.SetAttribute(id,"0");
							}		
								#endregion
							}
							break;
						case	"SP350"	:

							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
																				
											//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
											{		
												XmlElement DecPageHomeEndoSP350;
												DecPageHomeEndoSP350 = AcordPDFXML.CreateElement("HOMEENDORSEMENTSP350");
												DecPageRootElement.AppendChild(DecPageHomeEndoSP350);
												DecPageHomeEndoSP350.SetAttribute(fieldType,fieldTypeMultiple);
												DecPageHomeEndoSP350.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESP350"));
												DecPageHomeEndoSP350.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESP350"));
												DecPageHomeEndoSP350.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESP350EXTN"));
												DecPageHomeEndoSP350.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESP350EXTN"));

												XmlElement EndoElementSP350;
												EndoElementSP350 = AcordPDFXML.CreateElement("ENDOELEMENTSP350INFO");
												DecPageHomeEndoSP350.AppendChild(EndoElementSP350);
												EndoElementSP350.SetAttribute(fieldType,fieldTypeNormal);
												EndoElementSP350.SetAttribute(id,"0");
											}		
								#endregion
							}
							break;
						case	"LAS360"	:


							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoLAS360;
								DecPageHomeEndoLAS360 = AcordPDFXML.CreateElement("HOMEENDORSEMENTLAS360");
								DecPageRootElement.AppendChild(DecPageHomeEndoLAS360);
								DecPageHomeEndoLAS360.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoLAS360.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoLAS360.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoLAS360.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGELAS360"));
									DecPageHomeEndoLAS360.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGELAS360"));
								}
								DecPageHomeEndoLAS360.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGELAS360EXTN"));
								DecPageHomeEndoLAS360.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGELAS360EXTN"));

								XmlElement EndoElementLAS360;
								EndoElementLAS360 = AcordPDFXML.CreateElement("ENDOELEMENTLAS360INFO");
								DecPageHomeEndoLAS360.AppendChild(EndoElementLAS360);
								EndoElementLAS360.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementLAS360.SetAttribute(id,"0");
							}		
								#endregion
							}
									
							break;
						case	"CWC373"	:
							//										//if(htpremium.Contains("WRKR_CMP"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["WRKR_CMP"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["WRKR_CMP"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["WRKR_CMP"].ToString())+"</HM_ENDPREMIUM>";

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoCWC373;
								DecPageHomeEndoCWC373 = AcordPDFXML.CreateElement("HOMEENDORSEMENTCWC373");
								DecPageRootElement.AppendChild(DecPageHomeEndoCWC373);
								DecPageHomeEndoCWC373.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoCWC373.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoCWC373.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoCWC373.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGECWC373"));
									DecPageHomeEndoCWC373.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGECWC373"));
								}
								DecPageHomeEndoCWC373.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGECWC373EXTN"));
								DecPageHomeEndoCWC373.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGECWC373EXTN"));

								XmlElement EndoElementCWC373;
								EndoElementCWC373 = AcordPDFXML.CreateElement("ENDOELEMENTCWC373INFO");
								DecPageHomeEndoCWC373.AppendChild(EndoElementCWC373);
								EndoElementCWC373.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementCWC373.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["WRKR_CMP"].ToString());
						}
							break;

						case	"LLE382"	:
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoLLE382;
								DecPageHomeEndoLLE382 = AcordPDFXML.CreateElement("HOMEENDORSEMENTLLE382");
								DecPageRootElement.AppendChild(DecPageHomeEndoLLE382);
								DecPageHomeEndoLLE382.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoLLE382.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoLLE382.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoLLE382.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGELLE382"));
									DecPageHomeEndoLLE382.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGELLE382"));
								}
								DecPageHomeEndoLLE382.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGELLE382EXTN"));
								DecPageHomeEndoLLE382.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGELLE382EXTN"));

								XmlElement EndoElementLLE382;
								EndoElementLLE382 = AcordPDFXML.CreateElement("ENDOELEMENTLLE382INFO");
								DecPageHomeEndoLLE382.AppendChild(EndoElementLLE382);
								EndoElementLLE382.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementLLE382.SetAttribute(id,"0");
							}		
								#endregion
							}
							break;
						case	"CBF4"	:

							break;
						case	"CF5"	:

							break;
						case	"CUOF6"	:

							break;
						case	"SPP900"	:
							if (gStrPdfFor == PDFForDecPage)
							{
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoSPP900;
								DecPageHomeEndoSPP900 = AcordPDFXML.CreateElement("HOMEENDORSEMENTSPP900");
								DecPageRootElement.AppendChild(DecPageHomeEndoSPP900);
								DecPageHomeEndoSPP900.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoSPP900.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoSPP900.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoSPP900.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGESPP900"));
									DecPageHomeEndoSPP900.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGESPP900"));
								}
								DecPageHomeEndoSPP900.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGESPP900EXTN"));
								DecPageHomeEndoSPP900.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGESPP900EXTN"));

								XmlElement EndoElementSPP900;
								EndoElementSPP900 = AcordPDFXML.CreateElement("ENDOELEMENTSPP900INFO");
								DecPageHomeEndoSPP900.AppendChild(EndoElementSPP900);
								EndoElementSPP900.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementSPP900.SetAttribute(id,"0");
							}		
								#endregion

							}
							break;

						case	"CO100"	:
							break;
						case	"HO100"	:
							break;
						case	"PFBF2"	:
							break;
						case	"NSPC220"	:
							if (gStrPdfFor == PDFForDecPage)
							{

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoNSPC220;
								DecPageHomeEndoNSPC220 = AcordPDFXML.CreateElement("HOMEENDORSEMENTNSPC220");
								DecPageRootElement.AppendChild(DecPageHomeEndoNSPC220);
								DecPageHomeEndoNSPC220.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoNSPC220.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoNSPC220.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoNSPC220.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGENSPC220"));
									DecPageHomeEndoNSPC220.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGENSPC220"));
								}
								DecPageHomeEndoNSPC220.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGENSPC220EXTN"));
								DecPageHomeEndoNSPC220.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGENSPC220EXTN"));

								XmlElement EndoElementNSPC220;
								EndoElementNSPC220 = AcordPDFXML.CreateElement("ENDOELEMENTNSPC220INFO");
								DecPageHomeEndoNSPC220.AppendChild(EndoElementNSPC220);
								EndoElementNSPC220.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementNSPC220.SetAttribute(id,"0");
							}		
								#endregion
							}
							break;

						case	"PFSF3"	:

							break;
						case	"IBUSPA"	:
							if (gStrPdfFor == PDFForDecPage)
							{

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoIBUSPA;
								DecPageHomeEndoIBUSPA = AcordPDFXML.CreateElement("HOMEENDORSEMENTIBUSPA");
								DecPageRootElement.AppendChild(DecPageHomeEndoIBUSPA);
								DecPageHomeEndoIBUSPA.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoIBUSPA.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoIBUSPA.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoIBUSPA.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEIBUSPA"));
									DecPageHomeEndoIBUSPA.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIBUSPA"));
								}
								DecPageHomeEndoIBUSPA.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEIBUSPAEXTN"));
								DecPageHomeEndoIBUSPA.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIBUSPAEXTN"));

								XmlElement EndoElementIBUSPA;
								EndoElementIBUSPA = AcordPDFXML.CreateElement("ENDOELEMENTIBUSPAINFO");
								DecPageHomeEndoIBUSPA.AppendChild(EndoElementIBUSPA);
								EndoElementIBUSPA.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementIBUSPA.SetAttribute(id,"0");
							}		
								#endregion
							}
							break;


						case	"IBUSPO"	:
							if (gStrPdfFor == PDFForDecPage)
							{

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoIBUSPO;
								DecPageHomeEndoIBUSPO = AcordPDFXML.CreateElement("HOMEENDORSEMENTIBUSPO");
								DecPageRootElement.AppendChild(DecPageHomeEndoIBUSPO);
								DecPageHomeEndoIBUSPO.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoIBUSPO.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoIBUSPO.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoIBUSPO.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEIBUSPO"));
									DecPageHomeEndoIBUSPO.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIBUSPO"));
								}
								DecPageHomeEndoIBUSPO.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEIBUSPOEXTN"));
								DecPageHomeEndoIBUSPO.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIBUSPOEXTN"));

								XmlElement EndoElementIBUSPO;
								EndoElementIBUSPO = AcordPDFXML.CreateElement("ENDOELEMENTIBUSPOINFO");
								DecPageHomeEndoIBUSPO.AppendChild(EndoElementIBUSPO);
								EndoElementIBUSPO.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementIBUSPO.SetAttribute(id,"0");
							}	
								#endregion
							}
							break;


						case	"IBUSPOA"	:
							if (gStrPdfFor == PDFForDecPage)
							{

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoIBUSPOA;
								DecPageHomeEndoIBUSPOA = AcordPDFXML.CreateElement("HOMEENDORSEMENTIBUSPOA");
								DecPageRootElement.AppendChild(DecPageHomeEndoIBUSPOA);
								DecPageHomeEndoIBUSPOA.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoIBUSPOA.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoIBUSPOA.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoIBUSPOA.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEIBUSPOA"));
									DecPageHomeEndoIBUSPOA.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIBUSPOA"));
								}
								DecPageHomeEndoIBUSPOA.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEIBUSPOAEXTN"));
								DecPageHomeEndoIBUSPOA.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEIBUSPOAEXTN"));

								XmlElement EndoElementIBUSPOA;
								EndoElementIBUSPOA = AcordPDFXML.CreateElement("ENDOELEMENTIBUSPOAINFO");
								DecPageHomeEndoIBUSPOA.AppendChild(EndoElementIBUSPOA);
								EndoElementIBUSPOA.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementIBUSPOA.SetAttribute(id,"0");
							}	
								#endregion
							}
							break;

						case	"AROF1"	:
							if (gStrPdfFor == PDFForDecPage)
							{

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoAROF1;
								DecPageHomeEndoAROF1 = AcordPDFXML.CreateElement("HOMEENDORSEMENTAROF1");
								DecPageRootElement.AppendChild(DecPageHomeEndoAROF1);
								DecPageHomeEndoAROF1.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoAROF1.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoAROF1.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoAROF1.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAROF1"));
									DecPageHomeEndoAROF1.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAROF1"));
								}
								DecPageHomeEndoAROF1.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEAROF1EXTN"));
								DecPageHomeEndoAROF1.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAROF1EXTN"));

								XmlElement EndoElementAROF1;
								EndoElementAROF1 = AcordPDFXML.CreateElement("ENDOELEMENTAROF1INFO");
								DecPageHomeEndoAROF1.AppendChild(EndoElementAROF1);
								EndoElementAROF1.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementAROF1.SetAttribute(id,"0");
							}	
								#endregion
							}
							break;


						case	"AROF2"	:
							if (gStrPdfFor == PDFForDecPage)
							{

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoAROF2;
								DecPageHomeEndoAROF2 = AcordPDFXML.CreateElement("HOMEENDORSEMENTAROF2");
								DecPageRootElement.AppendChild(DecPageHomeEndoAROF2);
								DecPageHomeEndoAROF2.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoAROF2.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoAROF2.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoAROF2.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEAROF2"));
									DecPageHomeEndoAROF2.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAROF2"));
								}
								DecPageHomeEndoAROF2.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEAROF2EXTN"));
								DecPageHomeEndoAROF2.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEAROF2EXTN"));

								XmlElement EndoElementAROF2;
								EndoElementAROF2= AcordPDFXML.CreateElement("ENDOELEMENTAROF2INFO");
								DecPageHomeEndoAROF2.AppendChild(EndoElementAROF2);
								EndoElementAROF2.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementAROF2.SetAttribute(id,"0");
							}	
								#endregion
							}
							break;
						case	"HO417"	:
							if (gStrPdfFor == PDFForDecPage)
							{
	
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoHO417;
								DecPageHomeEndoHO417 = AcordPDFXML.CreateElement("HOMEENDORSEMENTHO417");
								DecPageRootElement.AppendChild(DecPageHomeEndoHO417);
								DecPageHomeEndoHO417.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoHO417.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoHO417.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoHO417.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHO417"));
									DecPageHomeEndoHO417.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO417"));
								}
								DecPageHomeEndoHO417.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHO417EXTN"));
								DecPageHomeEndoHO417.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO417EXTN"));

								XmlElement EndoElementHO417;
								EndoElementHO417= AcordPDFXML.CreateElement("ENDOELEMENTHO417INFO");
								DecPageHomeEndoHO417.AppendChild(EndoElementHO417);
								EndoElementHO417.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementHO417.SetAttribute(id,"0");
							}
											
								#endregion
							}
							break;


						case	"HO61"	:
							if (gStrPdfFor == PDFForDecPage)
							{

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoHO61;
								DecPageHomeEndoHO61 = AcordPDFXML.CreateElement("HOMEENDORSEMENTHO61");
								DecPageRootElement.AppendChild(DecPageHomeEndoHO61);
								DecPageHomeEndoHO61.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoHO61.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoHO61.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoHO61.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHO61"));
									DecPageHomeEndoHO61.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO61"));
								}
								DecPageHomeEndoHO61.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHO61EXTN"));
								DecPageHomeEndoHO61.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO61EXTN"));

								XmlElement EndoElementHO61;
								EndoElementHO61= AcordPDFXML.CreateElement("ENDOELEMENTHO61INFO");
								DecPageHomeEndoHO61.AppendChild(EndoElementHO61);
								EndoElementHO61.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementHO61.SetAttribute(id,"0");
							}	
								#endregion
							}
							break;


						case	"HO214"	:
							//										//if(htpremium.Contains("PRS_CMP"))
						{
							if (gStrPdfFor == PDFForDecPage)
							{
								//gstrGetPremium	=	htpremium["PRS_CMP"].ToString();
								//gintGetindex	=	gstrGetPremium.IndexOf(".");
								//												if(//gintGetindex==	-1)
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_CMP"].ToString() + ".00")+"</HM_ENDPREMIUM>";
								//												else
								//													DecPageRootElement.InnerXml += "<HM_ENDPREMIUM " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(htpremium["PRS_CMP"].ToString())+"</HM_ENDPREMIUM>";
								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoHO214;
								DecPageHomeEndoHO214 = AcordPDFXML.CreateElement("HOMEENDORSEMENTHO214");
								DecPageRootElement.AppendChild(DecPageHomeEndoHO214);
								DecPageHomeEndoHO214.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoHO214.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoHO214.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoHO214.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHO214"));
									DecPageHomeEndoHO214.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO214"));
								}
								DecPageHomeEndoHO214.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHO214EXTN"));
								DecPageHomeEndoHO214.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO214EXTN"));

								XmlElement EndoElementHO214;
								EndoElementHO214= AcordPDFXML.CreateElement("ENDOELEMENTHO214INFO");
								DecPageHomeEndoHO214.AppendChild(EndoElementHO214);
								EndoElementHO214.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementHO214.SetAttribute(id,"0");
							}	
								#endregion
							}
							//											//dblSumTotal+=int.Parse(htpremium["PRS_CMP"].ToString());
						}
							break;
								
						case	"HO216"	:
								
							if (gStrPdfFor == PDFForDecPage)
							{

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoHO216;
								DecPageHomeEndoHO216 = AcordPDFXML.CreateElement("HOMEENDORSEMENTHO216");
								DecPageRootElement.AppendChild(DecPageHomeEndoHO216);
								DecPageHomeEndoHO216.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoHO216.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoHO216.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoHO216.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHO216"));
									DecPageHomeEndoHO216.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO216"));
								}
								DecPageHomeEndoHO216.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHO216EXTN"));
								DecPageHomeEndoHO216.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO216EXTN"));

								XmlElement EndoElementHO216;
								EndoElementHO216= AcordPDFXML.CreateElement("ENDOELEMENTHO216INFO");
								DecPageHomeEndoHO216.AppendChild(EndoElementHO216);
								EndoElementHO216.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementHO216.SetAttribute(id,"0");
							}		
								#endregion
							}
							break;

						case	"HO300"	:
							if (gStrPdfFor == PDFForDecPage)
							{

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{													
								XmlElement DecPageHomeEndoHO300;
								DecPageHomeEndoHO300 = AcordPDFXML.CreateElement("HOMEENDORSEMENTHO300");
								DecPageRootElement.AppendChild(DecPageHomeEndoHO300);
								DecPageHomeEndoHO300.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoHO300.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoHO300.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoHO300.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHO300"));
									DecPageHomeEndoHO300.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO300"));
								}
								DecPageHomeEndoHO300.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHO300EXTN"));
								DecPageHomeEndoHO300.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO300EXTN"));

								XmlElement EndoElementHO300;
								EndoElementHO300= AcordPDFXML.CreateElement("ENDOELEMENTHO300INFO");
								DecPageHomeEndoHO300.AppendChild(EndoElementHO300);
								EndoElementHO300.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementHO300.SetAttribute(id,"0");
							}	
								#endregion
							}
							break;

						case	"HO864"	:

							if (gStrPdfFor == PDFForDecPage)
							{

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoHO864;
								DecPageHomeEndoHO864 = AcordPDFXML.CreateElement("HOMEENDORSEMENTHO864");
								DecPageRootElement.AppendChild(DecPageHomeEndoHO864);
								DecPageHomeEndoHO864.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoHO864.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoHO864.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoHO864.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEHO864"));
									DecPageHomeEndoHO864.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO864"));
								}
								DecPageHomeEndoHO864.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEHO864EXTN"));
								DecPageHomeEndoHO864.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEHO864EXTN"));

								XmlElement EndoElementHO864;
								EndoElementHO864= AcordPDFXML.CreateElement("ENDOELEMENTHO864INFO");
								DecPageHomeEndoHO864.AppendChild(EndoElementHO864);
								EndoElementHO864.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementHO864.SetAttribute(id,"0");
							}	
								#endregion
							}
							break;


						case	"IFGHO"	:

							break;

						case	"WP865"	:
							if (gStrPdfFor == PDFForDecPage)
							{

								#region Dec Page Element
								//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
							{												
								XmlElement DecPageHomeEndoWP865;
								DecPageHomeEndoWP865 = AcordPDFXML.CreateElement("HOMEENDORSEMENTWP865");
								DecPageRootElement.AppendChild(DecPageHomeEndoWP865);
								DecPageHomeEndoWP865.SetAttribute(fieldType,fieldTypeMultiple);
								if(prnAttFile != null && prnAttFile.ToString() != "")
								{
									DecPageHomeEndoWP865.SetAttribute(PrimPDF,prnAttFile.ToString());
									DecPageHomeEndoWP865.SetAttribute(PrimPDFBlocks,"1");
								}
								else
								{
									DecPageHomeEndoWP865.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEWP865"));
									DecPageHomeEndoWP865.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEWP865"));
								}
								DecPageHomeEndoWP865.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEWP865EXTN"));
								DecPageHomeEndoWP865.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEWP865EXTN"));

								XmlElement EndoElementWP865;
								EndoElementWP865= AcordPDFXML.CreateElement("ENDOELEMENTWP865INFO");
								DecPageHomeEndoWP865.AppendChild(EndoElementWP865);
								EndoElementWP865.SetAttribute(fieldType,fieldTypeNormal);
								EndoElementWP865.SetAttribute(id,"0");
							}	
								#endregion
							}
							break;

						default:
							if (gStrPdfFor == PDFForDecPage)
							{
								if(prncovCode=="EMO42" || prncovCode=="EOP42")
								{
									#region Dec Page Element
									//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
								{												
									XmlElement DecPageHomeEndoEMO42;
									DecPageHomeEndoEMO42 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEMO42");
									DecPageRootElement.AppendChild(DecPageHomeEndoEMO42);
									DecPageHomeEndoEMO42.SetAttribute(fieldType,fieldTypeMultiple);
									if(prnAttFile != null && prnAttFile.ToString() != "")
									{
										DecPageHomeEndoEMO42.SetAttribute(PrimPDF,prnAttFile.ToString());
										DecPageHomeEndoEMO42.SetAttribute(PrimPDFBlocks,"1");
									}
									else
									{
										DecPageHomeEndoEMO42.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEMO42"));
										DecPageHomeEndoEMO42.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMO42"));
									}
									DecPageHomeEndoEMO42.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEMO42EXTN"));
									DecPageHomeEndoEMO42.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMO42EXTN"));

									XmlElement EndoElementEMO42;
									EndoElementEMO42= AcordPDFXML.CreateElement("ENDOELEMENTEMO42INFO");
									DecPageHomeEndoEMO42.AppendChild(EndoElementEMO42);
									EndoElementEMO42.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementEMO42.SetAttribute(id,"0");
								}
									#endregion
								}
								if(prncovCode=="EMO65" || prncovCode=="EOP65")
								{
									#region Dec Page Element
									if(gCtrlnChkHO65!=true)	
									{											
										XmlElement DecPageHomeEndoEMO65;
										DecPageHomeEndoEMO65 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEMO65");
										DecPageRootElement.AppendChild(DecPageHomeEndoEMO65);
										DecPageHomeEndoEMO65.SetAttribute(fieldType,fieldTypeMultiple);
										if(prnAttFile != null && prnAttFile.ToString() != "")
										{
											DecPageHomeEndoEMO65.SetAttribute(PrimPDF,prnAttFile.ToString());
											DecPageHomeEndoEMO65.SetAttribute(PrimPDFBlocks,"1");
										}
										else
										{
											DecPageHomeEndoEMO65.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEMO65"));
											DecPageHomeEndoEMO65.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMO65"));
										}
										DecPageHomeEndoEMO65.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEMO65EXTN"));
										DecPageHomeEndoEMO65.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMO65EXTN"));

										XmlElement EndoElementEMO65;
										EndoElementEMO65= AcordPDFXML.CreateElement("ENDOELEMENTEMO65INFO");
										DecPageHomeEndoEMO65.AppendChild(EndoElementEMO65);
										EndoElementEMO65.SetAttribute(fieldType,fieldTypeNormal);
										EndoElementEMO65.SetAttribute(id,"0");
										gCtrlnChkHO65=true;
									}				
									#endregion
								}
								if(prncovCode=="EMO70" || prncovCode=="EOP70")
								{
									#region Dec Page Element
									//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
								{											
									XmlElement DecPageHomeEndoEMO70;
									DecPageHomeEndoEMO70 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEMO70");
									DecPageRootElement.AppendChild(DecPageHomeEndoEMO70);
									DecPageHomeEndoEMO70.SetAttribute(fieldType,fieldTypeMultiple);
									if(prnAttFile != null && prnAttFile.ToString() != "")
									{
										DecPageHomeEndoEMO70.SetAttribute(PrimPDF,prnAttFile.ToString());
										DecPageHomeEndoEMO70.SetAttribute(PrimPDFBlocks,"1");
									}
									else
									{
										DecPageHomeEndoEMO70.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEMO70"));
										DecPageHomeEndoEMO70.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMO70"));
									}
									DecPageHomeEndoEMO70.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEMO70EXTN"));
									DecPageHomeEndoEMO70.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMO70EXTN"));

									XmlElement EndoElementEMO70;
									EndoElementEMO70= AcordPDFXML.CreateElement("ENDOELEMENTEMO70INFO");
									DecPageHomeEndoEMO70.AppendChild(EndoElementEMO70);
									EndoElementEMO70.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementEMO70.SetAttribute(id,"0");
								}
									#endregion

								}
								if(prncovCode=="EMO71" || prncovCode=="EOP71")
								{
									#region Dec Page Element
									//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
								{											
									XmlElement DecPageHomeEndoEMO71;
									DecPageHomeEndoEMO71 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEMO71");
									DecPageRootElement.AppendChild(DecPageHomeEndoEMO71);
									DecPageHomeEndoEMO71.SetAttribute(fieldType,fieldTypeMultiple);
									if(prnAttFile != null && prnAttFile.ToString() != "")
									{
										DecPageHomeEndoEMO71.SetAttribute(PrimPDF,prnAttFile.ToString());
										DecPageHomeEndoEMO71.SetAttribute(PrimPDFBlocks,"1");
									}
									else
									{
										DecPageHomeEndoEMO71.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEMO71"));
										DecPageHomeEndoEMO71.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMO71"));
									}
									DecPageHomeEndoEMO71.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEMO71EXTN"));
									DecPageHomeEndoEMO71.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMO71EXTN"));

									XmlElement EndoElementEMO71;
									EndoElementEMO71= AcordPDFXML.CreateElement("ENDOELEMENTEMO71INFO");
									DecPageHomeEndoEMO71.AppendChild(EndoElementEMO71);
									EndoElementEMO71.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementEMO71.SetAttribute(id,"0");
								}
									#endregion

								}
								if(prncovCode=="EMR211" || prncovCode=="EOP211")
								{
									#region Dec Page Element
									//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
								{											
									XmlElement DecPageHomeEndoEMR211;
									DecPageHomeEndoEMR211 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEMR211");
									DecPageRootElement.AppendChild(DecPageHomeEndoEMR211);
									DecPageHomeEndoEMR211.SetAttribute(fieldType,fieldTypeMultiple);
									if(prnAttFile != null && prnAttFile.ToString() != "")
									{
										DecPageHomeEndoEMR211.SetAttribute(PrimPDF,prnAttFile.ToString());
										DecPageHomeEndoEMR211.SetAttribute(PrimPDFBlocks,"1");
									}
									else
									{
										DecPageHomeEndoEMR211.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEMR211"));
										DecPageHomeEndoEMR211.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMR211"));
									}
									DecPageHomeEndoEMR211.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEMR211EXTN"));
									DecPageHomeEndoEMR211.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMR211EXTN"));

									XmlElement EndoElementEMR211;
									EndoElementEMR211= AcordPDFXML.CreateElement("ENDOELEMENTEMR211INFO");
									DecPageHomeEndoEMR211.AppendChild(EndoElementEMR211);
									EndoElementEMR211.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementEMR211.SetAttribute(id,"0");
								}
									#endregion
								}
								if(prncovCode=="EMR310")
								{
									#region Dec Page Element
									//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
								{											
									XmlElement DecPageHomeEndoEMR310;
									DecPageHomeEndoEMR310 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEMR310");
									DecPageRootElement.AppendChild(DecPageHomeEndoEMR310);
									DecPageHomeEndoEMR310.SetAttribute(fieldType,fieldTypeMultiple);
									if(prnAttFile != null && prnAttFile.ToString() != "")
									{
										DecPageHomeEndoEMR310.SetAttribute(PrimPDF,prnAttFile.ToString());
										DecPageHomeEndoEMR310.SetAttribute(PrimPDFBlocks,"1");
									}
									else
									{
										DecPageHomeEndoEMR310.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEMR310"));
										DecPageHomeEndoEMR310.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMR310"));
									}
									DecPageHomeEndoEMR310.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEMR310EXTN"));
									DecPageHomeEndoEMR310.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMR310EXTN"));

									XmlElement EndoElementEMR310;
									EndoElementEMR310= AcordPDFXML.CreateElement("ENDOELEMENTEMR310INFO");
									DecPageHomeEndoEMR310.AppendChild(EndoElementEMR310);
									EndoElementEMR310.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementEMR310.SetAttribute(id,"0");
								}
									#endregion
											
								}

								if(prncovCode=="EMO315" || prncovCode=="EOP315")
								{
									
								}
								if(prncovCode=="EMR320")
								{
									#region Dec Page Element
									//if(CoverageDetails["ENDORS_PRINT"].ToString() !="N" )	
								{											
									XmlElement DecPageHomeEndoEMR320;
									DecPageHomeEndoEMR320 = AcordPDFXML.CreateElement("HOMEENDORSEMENTEMR320");
									DecPageRootElement.AppendChild(DecPageHomeEndoEMR320);
									DecPageHomeEndoEMR320.SetAttribute(fieldType,fieldTypeMultiple);
									if(prnAttFile != null && prnAttFile.ToString() != "")
									{
										DecPageHomeEndoEMR320.SetAttribute(PrimPDF,prnAttFile.ToString());
										DecPageHomeEndoEMR320.SetAttribute(PrimPDFBlocks,"1");
									}
									else
									{
										DecPageHomeEndoEMR320.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEEMR320"));
										DecPageHomeEndoEMR320.SetAttribute(PrimPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMR320"));
									}
									DecPageHomeEndoEMR320.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEEMR320EXTN"));
									DecPageHomeEndoEMR320.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEEMR320EXTN"));

									XmlElement EndoElementEMR320;
									EndoElementEMR320= AcordPDFXML.CreateElement("ENDOELEMENTEMR320INFO");
									DecPageHomeEndoEMR320.AppendChild(EndoElementEMR320);
									EndoElementEMR320.SetAttribute(fieldType,fieldTypeNormal);
									EndoElementEMR320.SetAttribute(id,"0");
								}
									#endregion
											
								}
								if(prncovCode=="HO6D")
								{
									
								}
								if(prncovCode=="HO4D")
								{
									
								}
							}
																		
							break;
					}*/
					#endregion
					XmlElement DecPageHomeEndo;
					XmlElement EndoElementHOME;
					if(BlankPagePrinted==0 && inttotalpage!=0)
					{
						DecPageHomeEndo = AcordPDFXML.CreateElement("HOMEENDORSEMENT" + "_" + 0);
						DecPageRootElement.AppendChild(DecPageHomeEndo);
						DecPageHomeEndo.SetAttribute(fieldType,fieldTypeMultiple);

						DecPageHomeEndo.SetAttribute(PrimPDF,getAcordPDFNameFromXML("DECPAGEBLANKDOCUMENT"));
						DecPageHomeEndo.SetAttribute(PrimPDFBlocks,"1");

						DecPageHomeEndo.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEBLANKDOCUMENT"));
						DecPageHomeEndo.SetAttribute(SecondPDFBlocks,"1");

						
						EndoElementHOME = AcordPDFXML.CreateElement("ENDOELEMENTHOMEINFO" + "_" + 0);
						DecPageHomeEndo.AppendChild(EndoElementHOME);
						DecPageHomeEndo.SetAttribute(fieldType,fieldTypeNormal);
						DecPageHomeEndo.SetAttribute(id,"0");
						//intCnt++;
						BlankPagePrinted++;
					}
						
					if(inttotalpage!=0)
					{
						Cntrl=intCnt+1;
						DecPageHomeEndo = AcordPDFXML.CreateElement("HOMEENDORSEMENT" + "_" + Cntrl);
						DecPageRootElement.AppendChild(DecPageHomeEndo);
						DecPageHomeEndo.SetAttribute(fieldType,fieldTypeMultiple);

						DecPageHomeEndo.SetAttribute(PrimPDF,prnAttFile);
						DecPageHomeEndo.SetAttribute(PrimPDFBlocks,"1");

						DecPageHomeEndo.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEDPWORDEXTN"));
						DecPageHomeEndo.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDPWORDEXTN"));

						
						EndoElementHOME = AcordPDFXML.CreateElement("ENDOELEMENTHOMEINFO" + "_" + Cntrl);
						DecPageHomeEndo.AppendChild(EndoElementHOME);
						EndoElementHOME.SetAttribute(fieldType,fieldTypeNormal);
						EndoElementHOME.SetAttribute(id,"0");
						intCnt++;
					}
					else
					{
						DecPageHomeEndo = AcordPDFXML.CreateElement("HOMEENDORSEMENT" + "_" + intCnt);
						DecPageRootElement.AppendChild(DecPageHomeEndo);
						DecPageHomeEndo.SetAttribute(fieldType,fieldTypeMultiple);

						DecPageHomeEndo.SetAttribute(PrimPDF,prnAttFile);
						DecPageHomeEndo.SetAttribute(PrimPDFBlocks,"1");

						DecPageHomeEndo.SetAttribute(SecondPDF,getAcordPDFNameFromXML("DECPAGEDPWORDEXTN"));
						DecPageHomeEndo.SetAttribute(SecondPDFBlocks,getAcordPDFBlockFromXML("DECPAGEDPWORDEXTN"));

						
						EndoElementHOME = AcordPDFXML.CreateElement("ENDOELEMENTHOMEINFO" + "_" + intCnt);
						DecPageHomeEndo.AppendChild(EndoElementHOME);
						EndoElementHOME.SetAttribute(fieldType,fieldTypeNormal);
						EndoElementHOME.SetAttribute(id,"0");
						intCnt++;
					}
					counter++;
				}
			}

		}

		#endregion Endorsement Wordings

		#region Addition Wordings
		private void createAddWordingsXML()
		{
			//string lob_id="1";
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
				if(gStrProcessID != null && gStrProcessID != "" && gStrProcessID != "0")
				{
					//DataSet DSAddWordSet = new DataSet();
					//DSAddWordSet = gobjWrapper.ExecuteDataSet("Proc_GetAddWordingsData " + state_id + "," + lob_id + "," + gStrProcessID);
			
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
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<priv_pol " + fieldType + "=\"" + fieldTypeText + "\">"  + RemoveJunkXmlCharacters(" ") + "</priv_pol>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<priv_text " + fieldType + "=\"" + fieldTypeText + "\">" +RemoveJunkXmlCharacters(" ")+ "</priv_text>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_coll " + fieldType + "=\"" + fieldTypeText + "\">"+ RemoveJunkXmlCharacters(" ") + "</inf_coll>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<infr_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(" ") + "</infr_txt>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<bull1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(" ") +"</bull1>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_bull1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(" ") +"</inf_bull1>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_bull2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(" ") + "</inf_bull2>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_bull3 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(" ") + "</inf_bull3>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_disc " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(" ") + "</inf_disc>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<inf_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(" ") + "</inf_txt>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<wolv_mut " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(" ") + "</wolv_mut>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<wolv_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(" ") + "</wolv_txt>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<sec_proc " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(" ") + "</sec_proc>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<sec_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(" ") + "</sec_txt>";
					
			
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_hd1 " + fieldType + "=\"" + fieldTypeText + "\">" +RemoveJunkXmlCharacters("FCRA ADVERSE ACTION DISCLOSURE 15 USC 1681m & 16 CFR 601,")+ "</fcra_hd1>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_hd2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("MIB Nos.  2003-1 & 2003-2 & IC 27-2-21 Sec. 19") + "</fcra_hd2>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("Based upon information contained in a consumer report supplied by Trans Union, P. O. Box 1000, Chester, PA 19022, toll free telephone number 800-645-1938, Wolverine Mutual Insurance Company has calculated your insurance score and determined your premium discounts which you will find on your declarations page. Trans Union did not make the decision to take this action and cannot give you the specific reason for it. However, you have a right to dispute directly to Trans Union the accuracy or completeness of any information furnished by it, and you have a right to obtain a free copy of the consumer report from Trans Union if you request it not later than 60 days after receipt of this notice.") + "</fcra_txt>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<fcra_reas " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters("The following factors in your credit history identified by Trans Union were the primary influences in determining your insurance score and associated premium discount:") + "</fcra_reas>";

			DecPageInfoElement.InnerXml +="<reason_code1 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code1 + "</reason_code1>";
			DecPageInfoElement.InnerXml +="<reason_code2 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code2 + "</reason_code2>";
			DecPageInfoElement.InnerXml +="<reason_code3 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code3 + "</reason_code3>";
			DecPageInfoElement.InnerXml +="<reason_code4 " + fieldType + "=\"" + fieldTypeText + "\">" + reason_code4 + "</reason_code4>";
				
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<not_pol " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(" ") + "</not_pol>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<not_txt " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(" ") + "</not_txt>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<not_txt1 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(" ") + "</not_txt1>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<not_txt2 " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(" ") + "</not_txt2>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<ins_add " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(" ") + "</ins_add>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<hotline " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(" ") + "</hotline>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<website " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(" ") + "</website>";
			DecPageInfoElement.InnerXml = DecPageInfoElement.InnerXml +  "<foot_left " + fieldType + "=\"" + fieldTypeText + "\">" + RemoveJunkXmlCharacters(" ") + "</foot_left>";
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
			gobjWrapper.ClearParameteres();
			gobjWrapper.AddParameter("@CUSTOMERID",gStrClientID);
			gobjWrapper.AddParameter("@POLID",gStrPolicyId);
			gobjWrapper.AddParameter("@VERSIONID",goldVewrsionId);
			gobjWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
			DataSet dsoldInsuScr = gobjWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
			gobjWrapper.ClearParameteres();

			//DataSet dsoldInsuScr = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + gStrClientID + "," + gStrPolicyId + "," + goldVewrsionId + ",'" + gStrCalledFrom + "'");
			
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
		
		private double GetPrecentDed(string grandpercent, string insuredval)
		{
			double granddeduc = 0;
			if(grandpercent.IndexOf("%") > 0)
			{
				granddeduc = double.Parse(insuredval.Replace(",","")) * double.Parse(grandpercent.Replace("%","").Replace("-","").Trim()) / 100.00;
				if(grandpercent.Replace("%","").Replace("-","").Trim() == "1" && granddeduc < 100.00)
					granddeduc = 100.00;
				else if(grandpercent.Replace("%","").Replace("-","").Trim() == "2" && granddeduc < 200.00)
					granddeduc = 200.00;
				else if(grandpercent.Replace("%","").Replace("-","").Trim() == "5" && granddeduc < 500.00)
					granddeduc = 500.00;
			}
			return granddeduc;
		}

	}
}