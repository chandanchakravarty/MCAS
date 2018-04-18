using System;
using System.Text;
using System.Collections;
using System.Xml;
using System.Data;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;
using System.IO;
using Cms.Model.Account;

namespace Cms.BusinessLayer.BlProcess
{
	enum LOBType
	{
		HOME	=	1,
		PPA		=	2,
		MOT		=	3,
		RENT	=	6,
		WAT		=	4,
		UMB		=	5
	}
	/// <summary>
	/// <CreatedBy>Neeraj Singh</CreatedBy>
	/// <Dated>20-August-2009</Dated>
	/// <Purpose>To Common Functions for all Acord PDF xml and Pdf Genration using Acord Dll</Purpose>
	/// </summary>
	public class ClsCommonPdf : ClsCommon
	{
		#region Constants
		public const string RootElement="ACORDXML";
		public const string CheckRootElement="CheckPDFXML";
		public const string CalledFromPolicy ="POLICY";
		public const string CalledFromApplication="APPLICATION";
		public const string RootElementForAllPDF = "ACORD";
		public const string PrimPDF="PRIMPDF";
		public const string SecondPDF="SECONDPDF";
		public const string PrimPDFBlocks="PRIMPDFBLOCKS";
		public const string SecondPDFBlocks="SECONDPDFBLOCKS";

		public const string fieldType="FIELDTYPE";
		public const string imageType="ISIMAGE";
		public const string fieldTypeSingle="S";
		public const string fieldTypeMultiple="M";
		public const string fieldTypeNormal="N";
		public const string fieldTypeText="T";
		public const string fieldTypeCheckBox="C";
		public const string fieldTypeImage="I";
		public const string imageTypeYes="Y";
		public const string imageTypeNo="N";

		public const string floatX = "FLOATX";
		public const string floatY = "FLOATY";
		public const string floatW = "FLOATW";
		public const string floatH = "FLOATH";
		public const string pageNo = "PAGENO";
		public const string imgPath= "IMAGEPATH";

		public const string id="ID";
		public const string STATE_MICHIGAN="MI";
		public const string STATE_INDIANA="IN";
		public const string STATE_WISCONSIN="WI";

		public const string PDFForDecPage="DECPAGE";
		public const string PDFForAcord="ACORD";
		#endregion

		#region Global Declarations for All PDF XML Classes.
		public string gStrClientID="0";
		public string gStrPolicyId="0";
		public string gStrPolicyVersion="0";
		public string gStrProcessID="";
		public string gStrCalledFrom="";   //"POLICY"/"APPLICATION"
		public string gStrPdfFor="";		//"ACORD"/"DECPAGE"
		public string gStrCopyTo="";
		public string gStrtemp="";
		public string gStrLob="";
		public string gStateCode="";
		public string gAgencyCode="";
		public string gstrTemp="";

		public string PolicyNumber;
		public string PolicyEffDate;
		public string PolicyExpDate;
		public string PolicyType;      
		public string Reason="";
		public string CopyTo;
		public string ApplicantName;
		public string ApplicantAddress;
		public string ApplicantCityStZip;
		public string CustomerAddress;
		public string CustomerCityStZip;

		public string reason_code1;
		public string reason_code2;
		public string reason_code3;
		public string reason_code4;
		
		public string AgencyName;
		public string AgencyAddress;
		public string AgencyCitySTZip;
		public string AgencyPhoneNumber;
		public string AgencyCode;
		public string AgencySubCode;
		public string AgencyBilling;

		public XmlDocument AcordPDFXML;

		
		//DataSet declaration for Pdf Xml
		public	DataSet DSTempDataSet;
		private   DataSet DSTempPolicyDataSet= new DataSet() ;		
		private   DataSet DSTempApplicantDataSet = new DataSet();
		private   DataSet DstempDocument = new DataSet();
		private   DataSet DSAddWordSet = new DataSet();
		private   DataSet DSTempAutoDetailDataSet = new DataSet();
		private   DataSet DSTempRateDataSet = new DataSet();
		private   DataSet DSAdjustDataset = new DataSet();
		private   DataSet DSOperator = new DataSet();
		private   DataSet DSTempUnderWritDataSet = new DataSet();
		private   DataSet DSTempPriorLossDataSet = new DataSet();
		private   DataSet DSTempAddIntrst = new DataSet();
		private   DataSet DSTempEquipment = new DataSet();
		private   DataSet DSTempOperators = new DataSet();
		private	  DataSet DsTempHomeBoatAddIntrst = new DataSet();

		public Hashtable MonthName = new Hashtable();
		public XmlDocument PDFVersionsXML;
		public XmlNode PDFVersionLOBNode;

		public XmlDocument PDFInsScoresXML;
		public XmlNode PDFInsScoresLOBNode;
		public string strNeedPage2 = "N";
		public int currTerm = 0;
		public int applyInsScore = 0;

		public XmlDocument RateXmlDocument = new XmlDocument();
		public bool isRateGenerated = false;
		public string strPolicyProcessID="";
		public string strAcordPDFXml="";
		public DataWrapper objWrapper;

		public string NoWordingPDFFileName="";
		public string WordingPDFFileName="";
		public string AgentWordingPDFFileName="";
		public string AdditionalIntrstPDFFileName="";
		public string AcordPDFFileName="";
		public string AutoIdCardPDFFileName="";

		#endregion
		
		
		protected string InputBase ; 
		protected string OutputPath ;
		protected string FinalBasePath;

		public  int ctrFlWord;
		private  string outXml;
		public string gCopyTo="";
				
		// Variables for Check Genration
		public string CHECK_ID="";
		public string PayeeName="";
		public string CheckDate="";
		public string CheckAmount="";
		public string CheckNumber="";
		public ArrayList alPdfName;
		StringBuilder returnString= new StringBuilder(); 
		AcordPDF.AcordXMLParser gobjEbixAcordPDF = new AcordPDF.AcordXMLParser();

		#region Constructor

		public ClsCommonPdf()
		{
			// Initialise Input & Out Path to base path based from calling Applcation (CMS/EOD)
			if(IsEODProcess)
			{
				
				InputBase = UploadPath + "\\INPUTPDFs\\";
				InputBase=  System.IO.Path.GetFullPath(InputBase);
				OutputPath = UploadPath + "\\OUTPUTPDFs\\";
				OutputPath = System.IO.Path.GetFullPath(OutputPath);
				FinalBasePath = UploadURL + "/OUTPUTPDFs/";
			}
			else
			{
				InputBase  = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()).ToString() + "\\INPUTPDFs\\" ;
                OutputPath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()).ToString() + "\\OUTPUTPDFs\\";
                FinalBasePath = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString() + "/OUTPUTPDFs/";
			}
			//InitailizePdfDataTable();
		}
		/*public ClsCommonPdf(DataWrapper objWraper)
		{
			if(this.objWrapper!=null)
				this.objWrapper=objWraper;
			// Initialise Input & Out Path to base path based from calling Applcation (CMS/EOD)
			if(IsEODProcess)
			{
				
				InputBase = UploadPath + "\\INPUTPDFs\\";
				InputBase=  System.IO.Path.GetFullPath(InputBase);
				OutputPath = UploadPath + "\\OUTPUTPDFs\\";
				OutputPath = System.IO.Path.GetFullPath(OutputPath);
				FinalBasePath = UploadURL + "/OUTPUTPDFs/";
			}
			else
			{
				InputBase = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationSettings.AppSettings.Get("UploadURL").ToString()).ToString() + "\\INPUTPDFs\\" ;
				OutputPath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationSettings.AppSettings.Get("UploadURL").ToString()).ToString() + "\\OUTPUTPDFs\\" ;
				FinalBasePath = System.Configuration.ConfigurationSettings.AppSettings.Get("UploadURL").ToString() + "/OUTPUTPDFs/";
			}
			//InitailizePdfDataTable();
		}*/

		public ClsCommonPdf(string strCustomerId, string strAppId, string strAppVersionId, string strCalledFrom, string strStateCode, string strLob, string strProcessId,  string Agency_Code, DataWrapper objWraper, string temp)
		{
			try
			{
				this.objWrapper=objWraper;
				// Initialise Input & Out Path to base path based from calling Applcation (CMS/EOD)
				if(IsEODProcess)
				{
				
					InputBase = UploadPath + "\\INPUTPDFs\\";
					InputBase=  System.IO.Path.GetFullPath(InputBase);
					OutputPath = UploadPath + "\\OUTPUTPDFs\\";
					OutputPath = System.IO.Path.GetFullPath(OutputPath);
					FinalBasePath = UploadURL + "/OUTPUTPDFs/";
				}
				else
				{
					InputBase  = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()).ToString() + "\\INPUTPDFs\\" ;
                    OutputPath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()).ToString() + "\\OUTPUTPDFs\\";
                    FinalBasePath = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString() + "/OUTPUTPDFs/";
				}
				gStrClientID=strCustomerId;
				gStrPolicyId=strAppId;
				gStrPolicyVersion=strAppVersionId;
				gStrCalledFrom=strCalledFrom;
				gStateCode=strStateCode;
				gStrLob=strLob;
				gStrProcessID=strProcessId;
				gAgencyCode=Agency_Code;
				gstrTemp=temp;
				InitailizePdfPolicyDataSet();
				outXml = GetPdfFullWordingXml();
			}
			catch(Exception ex)
			{
				DoCleanUpUnusedObj(ex);
				throw(new Exception("Error while generating PDF.",ex));
			}
		}
		#endregion

		#region common functions

		public string AfterComma(int number)
		{
			string num = number.ToString();
			if(num.Length == 1)
				num = "00" + num;
			else if(num.Length == 2)
				num = "0" + num;
			return num;
		}

		public string DollarFormat(double amount)
		{
			int lim_cov=0;
			string limcov = "";
			try
			{
				lim_cov = Convert.ToInt32(amount);
				int lim_million = lim_cov /1000;
				if(lim_million / 1000 > 0)
				{
					limcov = (lim_million /1000).ToString() + "," + AfterComma(lim_million %1000);
				}
				else
					limcov = lim_million.ToString();
				if(lim_cov / 1000 > 0)
				{
					limcov += "," + AfterComma(lim_cov %1000) + ".00";
				}
				else
					limcov = lim_cov.ToString() + ".00";
			}
			catch//(Exception ex)
			{
				return "";
			}
			return limcov;
		}
		public string GetIntFormat(string amount)
		{
			int lim_cov=0;
			string limcov = "";

			try
			{
				if(amount.IndexOf(".00")>0)
				{
					amount=amount.Replace(".00","");
				}
				
				lim_cov = Convert.ToInt32(amount);
				int lim_million = lim_cov /1000;
				if(lim_million / 1000 > 0)
				{
					limcov = (lim_million /1000).ToString() + "," + AfterComma(lim_million %1000);
				}
				else
					limcov = lim_million.ToString();
				if(lim_cov / 1000 > 0)
				{
					limcov += "," + AfterComma(lim_cov %1000) ;
					limcov = "$" + limcov;
				}
				else
					limcov = "$" + lim_cov.ToString();
			}
			catch//(Exception ex)
			{
				return "";
			}
			return limcov;

		}
		public string GetIntFormatForHome(string amount)
		{
			//int lim_cov=0;
			string limcov = "";

			try
			{
				if(amount.IndexOf(".00")>0)
				{
					amount=amount.Replace(".00","");
				}
				
				limcov = "$" + amount.ToString();
			}
			catch//(Exception ex)
			{
				return "";
			}
			return limcov;

		}
		public string NumberFormat(string amount)
		{
			int lim_cov=0;
			string amt = "";
			try
			{
				string[] stramount = new string[0];
				stramount = amount.Split('.');
				lim_cov = int.Parse(stramount[0]);
				int lim_million = lim_cov /1000;
				if(lim_million / 1000 > 0)
				{
					amt = (lim_million /1000).ToString() + "," + AfterComma(lim_million %1000);
				}
				else
					amt = lim_million.ToString();
				if(lim_cov / 1000 > 0)
				{
					amt += "," + AfterComma(lim_cov %1000) + "."+ stramount[1];
				}
				else
					amt = lim_cov.ToString() + "."+ stramount[1];
			}
			catch//(Exception ex)
			{
				return "";
			}
			return amt;
		}
		#endregion
		#region New Pdf genration logic for policy commit
		//Genrate Pdf when called from Menu to view dec or accord page
        public string GenratePdfForMenu(string strCustomerId, string strAppId, string strAppVersionId, string strCalledFrom, string strCalledForPDF, string strStateCode, string strLob, string strProcessId, ref string Agency_Code, string temp)
		{
			string PdfName="";
			try
			{
				DataWrapper objWrap =  new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);
				this.objWrapper = objWrap;
				gStrClientID=strCustomerId;
				gStrPolicyId=strAppId;
				gStrPolicyVersion=strAppVersionId;
				gStrCalledFrom=strCalledFrom;
				gStateCode=strStateCode;
				gStrLob=strLob;
				gstrTemp=temp;
				gStrProcessID=strProcessId;
				InitailizePdfPolicyDataSet();
				Agency_Code=gAgencyCode;
				PdfName=PdfForCustomerDecPage(strCalledForPDF,"CUSTOMER");
			}
			catch(Exception ex)
			{
				DoCleanUpUnusedObj(ex);
				throw(new Exception("Error while generating PDF.",ex));
			}
			return PdfName;
		}
		//genrate pdf when called from Process commit
		public ArrayList GenratePdfForPolicyCommit()
		{
			try
			{
				//ClsPolicyProcess objProcessInfo = new ClsPolicyProcess();
				alPdfName = new ArrayList();
				if(gStrProcessID==ClsPolicyProcess.POLICY_COMMIT_NEW_BUSINESS_PROCESS.ToString() || gStrProcessID==ClsPolicyProcess.POLICY_COMMIT_RENEWAL_PROCESS.ToString()  || gStrProcessID==ClsPolicyProcess.POLICY_COMMIT_REWRITE_PROCESS.ToString())
				{
					AdditionalIntrstPDFFileName = PdfForAditionalInterest(ClsPolicyProcess.PDF_DEC_PAGE);
					alPdfName.Add(AdditionalIntrstPDFFileName);
					AgentWordingPDFFileName = PdfForCustomerDecPage(ClsPolicyProcess.PDF_DEC_PAGE,ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_AGENCY);
					alPdfName.Add(AgentWordingPDFFileName);
					NoWordingPDFFileName = PdfForCustomerDecPage(ClsPolicyProcess.PDF_DEC_PAGE,ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_CUSTOMER_NOWORD);
					alPdfName.Add(NoWordingPDFFileName);
					WordingPDFFileName = PdfForCustomerDecPage(ClsPolicyProcess.PDF_DEC_PAGE,ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_CUSTOMER);				
					alPdfName.Add(WordingPDFFileName);
					AcordPDFFileName = PdfForCustomerDecPage(ClsPolicyProcess.PDF_ACORD,"");
					alPdfName.Add(AcordPDFFileName);
					if( gStrLob == (((int)(LOBType.MOT)).ToString()) || gStrLob == ((int)(LOBType.PPA)).ToString())
					{
						AutoIdCardPDFFileName = PdfForAutoIdCard(ClsPolicyProcess.PDF_DEC_PAGE);						
					}
					alPdfName.Add(AutoIdCardPDFFileName);
				}
				else if(gStrProcessID==ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS.ToString() || gStrProcessID==ClsPolicyProcess.POLICY_COMMIT_REINSTATEMENT_PROCESS.ToString())
				{
					AdditionalIntrstPDFFileName = PdfForAditionalInterest(ClsPolicyProcess.PDF_DEC_PAGE);
					alPdfName.Add(AdditionalIntrstPDFFileName);
					AgentWordingPDFFileName = PdfForCustomerDecPage(ClsPolicyProcess.PDF_DEC_PAGE,ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_AGENCY);				
					alPdfName.Add(AgentWordingPDFFileName);
					NoWordingPDFFileName = PdfForCustomerDecPage(ClsPolicyProcess.PDF_DEC_PAGE,ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_CUSTOMER_NOWORD);
					alPdfName.Add(NoWordingPDFFileName);
					WordingPDFFileName = PdfForCustomerDecPage(ClsPolicyProcess.PDF_DEC_PAGE,ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_CUSTOMER);
					alPdfName.Add(WordingPDFFileName);
					AcordPDFFileName = PdfForCustomerDecPage(ClsPolicyProcess.PDF_ACORD,"");
					alPdfName.Add(AcordPDFFileName);
					if( gStrLob == (((int)(LOBType.MOT)).ToString()) || gStrLob == ((int)(LOBType.PPA)).ToString())
					{
						AutoIdCardPDFFileName = PdfForAutoIdCard(ClsPolicyProcess.PDF_DEC_PAGE);
					}
					alPdfName.Add(AutoIdCardPDFFileName);
				}
				else if(gStrProcessID==ClsPolicyProcess.POLICY_COMMIT_CORRECTIVE_USER_PROCESS.ToString())
				{
					alPdfName.Add(AdditionalIntrstPDFFileName);
					AgentWordingPDFFileName = PdfForCustomerDecPage(ClsPolicyProcess.PDF_DEC_PAGE,ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_AGENCY);				
					alPdfName.Add(AgentWordingPDFFileName);
					alPdfName.Add(NoWordingPDFFileName);
					//ClsPolicyProcess.AdditionalIntrstPDFFileName = PdfForAditionalInterest(ClsPolicyProcess.PDF_DEC_PAGE);
					//ClsPolicyProcess.NoWordingPDFFileName = PdfForCustomerDecPage(ClsPolicyProcess.PDF_DEC_PAGE,ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_CUSTOMER_NOWORD);
					WordingPDFFileName = PdfForCustomerDecPage(ClsPolicyProcess.PDF_DEC_PAGE,ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_CUSTOMER);
					alPdfName.Add(WordingPDFFileName);
					alPdfName.Add(AcordPDFFileName);
					alPdfName.Add(AutoIdCardPDFFileName);
					//ClsPolicyProcess.AcordPDFFileName = PdfForCustomerDecPage(ClsPolicyProcess.PDF_ACORD,"");
					//ClsPolicyProcess.AutoIdCardPDFFileName = PdfForAutoIdCard(ClsPolicyProcess.PDF_DEC_PAGE);
				}
				return alPdfName;
			}
			catch(Exception ex)
			{
				DoCleanUpUnusedObj(ex);
				throw(new Exception("Error while generating PDF.",ex));
			}
		}
		
		public string PrintChecks(string strCalledFrom,ref string agency_code,string strCHECK_ID)
		{
			objWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			string CheckName="";
			gStrCalledFrom = strCalledFrom;
			CHECK_ID = strCHECK_ID;
			try
			{
				CHECK_ID=strCHECK_ID;
				gAgencyCode=agency_code;
				CheckName = GenratePdfForCheck();
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(new Exception("Error while generating Check.",ex));
			}
			return CheckName;
		}
		private string GenratePdfForCheck()
		{
			string check_nums = "",Chk_name="", blank_num="", TranLogMess="", strCheckPDFXml="";
			ChkGenrationPathSet();
			Cms.BusinessLayer.BlProcess.ClsCheckPdfXml objCheckPdfXML = new ClsCheckPdfXml(CHECK_ID,objWrapper);
			strCheckPDFXml = objCheckPdfXML.getCheckPDFXml(ref blank_num, ref check_nums);
			PayeeName = objCheckPdfXML.PayeeName;
			CheckDate = objCheckPdfXML.CheckDate;
			CheckAmount = objCheckPdfXML.CheckAmount;
			CheckNumber = objCheckPdfXML.CheckNumber ;
			if(blank_num == "BLANK_NUM")
				return "BLANK_NUM";
			gobjEbixAcordPDF.InputXml = strCheckPDFXml;
			Chk_name = ImporsonatAndGenratePdfFile();
			TranLogMess = "Check (Number(s): " + check_nums + ") PDF generated successfully";
			ChkTransactionLogDetail("",Chk_name,TranLogMess);
			return Chk_name;
		}
		//Initialize DataTable for pdf genration
		public void InitailizePdfPolicyDataSet()
		{
			// Get policy information 
			if(DSTempPolicyDataSet!=null )
			{
				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				objWrapper.AddParameter("@POLID",gStrPolicyId);
				objWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				objWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempPolicyDataSet = objWrapper.ExecuteDataSet("Proc_GetPDFPolicyDetails");
			}
			//return DSTempPolicyDataSet;
			//Get Applicant information
			if(DSTempApplicantDataSet!=null)
			{
				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				objWrapper.AddParameter("@POLID",gStrPolicyId);
				objWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				objWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				DSTempApplicantDataSet = objWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails");
			}

			//Get Document Message
			if(DstempDocument!=null)
			{
				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@DOCUMENT_CODE","DEC_PAGE");
				DstempDocument = objWrapper.ExecuteDataSet("Proc_GetPDFDocumentMessage");
			}

			if(gStrProcessID!=""  && gStrProcessID != "0")
			{
				if(DSAddWordSet!=null)
				{
					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@STATE_ID","0");
					objWrapper.AddParameter("@PROCESS_ID",gStrProcessID);
					if(gStrLob == (((int)(LOBType.MOT)).ToString()) || gStrLob == ((int)(LOBType.PPA)).ToString())
						objWrapper.AddParameter("@LOB_ID","0");
					else if(gStrLob == (((int)(LOBType.HOME)).ToString()))
						objWrapper.AddParameter("@LOB_ID",(((int)(LOBType.HOME)).ToString()));
					else if(gStrLob == ((int)(LOBType.RENT)).ToString())
						objWrapper.AddParameter("@LOB_ID",((int)(LOBType.RENT)).ToString());
					DSAddWordSet = objWrapper.ExecuteDataSet("Proc_GetAddWordingsData");
				}
			}
			//Get Auto Details
			if(DSTempAutoDetailDataSet!=null)
			{
				if(gStrLob == (((int)(LOBType.MOT)).ToString()) || gStrLob == ((int)(LOBType.PPA)).ToString())
				{
					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					objWrapper.AddParameter("@POLID",gStrPolicyId);
					objWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					objWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempAutoDetailDataSet = objWrapper.ExecuteDataSet("Proc_GetPDFAuto_Details");
				}
				else if(gStrLob == (((int)(LOBType.HOME)).ToString()) || gStrLob == ((int)(LOBType.RENT)).ToString())
				{
					//(Home Risk detail)
					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					objWrapper.AddParameter("@POLID",gStrPolicyId);
					objWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					//objWrapper.AddParameter("@DWELLINGID",0);
					objWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempAutoDetailDataSet =  objWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_Dwelling_Details");
					objWrapper.ClearParameteres();
				}
			}

			//Get rate XML
			if(DSTempRateDataSet!=null && gstrTemp=="temp")
			{
				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				objWrapper.AddParameter("@POLID",gStrPolicyId);
				objWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				objWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				if(gStrLob == (((int)(LOBType.MOT)).ToString()) || gStrLob == ((int)(LOBType.PPA)).ToString())
					objWrapper.AddParameter("@LOB","AUTOP");
				else if(gStrLob == (((int)(LOBType.HOME)).ToString()) || gStrLob == ((int)(LOBType.RENT)).ToString())
					objWrapper.AddParameter("@LOB","HOME");
				DSTempRateDataSet = objWrapper.ExecuteDataSet("Proc_GetPDFQuoteRateXML");
			}
			if(DSTempPriorLossDataSet!=null)
			{
				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				objWrapper.AddParameter("@DATAFOR","POLICY");
				if(gStrLob == (((int)(LOBType.HOME)).ToString()) || gStrLob == ((int)(LOBType.RENT)).ToString())
					objWrapper.AddParameter("@LOBCODE","HOME");
				else if(gStrLob == (((int)(LOBType.MOT)).ToString()) || gStrLob == ((int)(LOBType.PPA)).ToString())
					objWrapper.AddParameter("@LOBCODE","AUTOP");
				DSTempPriorLossDataSet = objWrapper.ExecuteDataSet("Proc_GetPDFPriorPolicyAndLossDetails");
			}
			//Get additional interest details
			if(DSTempAddIntrst!=null)
			{
				if(gStrLob == (((int)(LOBType.HOME)).ToString()) || gStrLob == ((int)(LOBType.RENT)).ToString())
				{
					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					objWrapper.AddParameter("@POLID",gStrPolicyId);
					objWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					objWrapper.AddParameter("@DWELLINGID",0);
					objWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempAddIntrst = objWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_Additional_Interest");
					objWrapper.ClearParameteres();
					
					if(gStrLob == (((int)(LOBType.HOME)).ToString()))
					{
						objWrapper.ClearParameteres();
						objWrapper.AddParameter("@CUSTOMERID",gStrClientID);
						objWrapper.AddParameter("@POLID",gStrPolicyId);
						objWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
						objWrapper.AddParameter("@BOATID",0);
						objWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
						DsTempHomeBoatAddIntrst = objWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS");
						objWrapper.ClearParameteres();
					}
					
				}
				else if(gStrLob == (((int)(LOBType.MOT)).ToString()) || gStrLob == ((int)(LOBType.PPA)).ToString())
				{
					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					objWrapper.AddParameter("@POLID",gStrPolicyId);
					objWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					objWrapper.AddParameter("@VEHICLEID",0);
					objWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempAddIntrst = objWrapper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS");
					objWrapper.ClearParameteres();
				}
				else if(gStrLob == ((int)(LOBType.WAT)).ToString())
				{
					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					objWrapper.AddParameter("@POLID",gStrPolicyId);
					objWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					objWrapper.AddParameter("@BOATID",0);
					objWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempAddIntrst = objWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS");
					objWrapper.ClearParameteres();
				}				
			}
			// Get Schedule Misc Equip
			if(DSTempEquipment!=null)
			{
				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@CUSTOMERID",gStrClientID);
				objWrapper.AddParameter("@POLID",gStrPolicyId);
				objWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
				objWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
				objWrapper.AddParameter("@VEHICLEID",0);
				DSTempEquipment = objWrapper.ExecuteDataSet("Proc_GetPDFAuto_MiscEquipment");
				objWrapper.ClearParameteres();
			}
			// Get Auto Operator details
			if(DSTempOperators!=null)
			{
				if(gStrLob == (((int)(LOBType.MOT)).ToString()) || gStrLob == ((int)(LOBType.PPA)).ToString())
				{
					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					objWrapper.AddParameter("@POLID",gStrPolicyId);
					objWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					objWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					objWrapper.AddParameter("@VEHICLEID","0");
					DSTempOperators = objWrapper.ExecuteDataSet("Proc_GetPDFAutoOperatorDtls");
					objWrapper.ClearParameteres();
				}
				else if(gStrLob == (((int)(LOBType.HOME)).ToString()))
				{
					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					objWrapper.AddParameter("@POLID",gStrPolicyId);
					objWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					objWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					DSTempOperators = objWrapper.ExecuteDataSet("Proc_GetPDFOperatorDtls");
					objWrapper.ClearParameteres();
				}
			}
			// Get agency Code
			if(gAgencyCode=="")
			{
				gAgencyCode=DSTempPolicyDataSet.Tables[0].Rows[0]["AGENCY_CODE"].ToString().Trim();
				if(gAgencyCode.EndsWith("."))
					gAgencyCode = gAgencyCode.Substring(0, gAgencyCode.Length-1);
			}
			// Get state Code
			if(gStateCode=="")
				gStateCode = DSTempPolicyDataSet.Tables[0].Rows[0]["STATE_CODE"].ToString().Trim();
		}
		private void DoCleanUpUnusedObj(Exception ex)
		{
			// Exception Publishing
			System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
			addInfo.Add("Err Descriptor ","Error while generating PDF.");
			addInfo.Add("CustomerID" ,gStrClientID.ToString());
			addInfo.Add("PolicyID",gStrPolicyId.ToString());
			addInfo.Add("PolicyVersionID",gStrPolicyVersion.ToString());
			addInfo.Add("ProcessID",gStrProcessID);
			addInfo.Add("TransactionDescription","");
			ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex,addInfo );
			//Unused Data Dispose.
			if(DSTempPolicyDataSet!=null)
			{
				DSTempPolicyDataSet.Dispose();
			}
			if(DSTempApplicantDataSet!=null)
			{
				DSTempApplicantDataSet.Dispose();
			}
			if(DstempDocument!=null)
			{
				DstempDocument.Dispose();
			}
			if(DSAddWordSet!=null)
			{
				DSAddWordSet.Dispose();
			}
			if(DSTempAutoDetailDataSet!=null)
			{
				DSTempAutoDetailDataSet.Dispose();
			}
			if(DSTempPriorLossDataSet!=null)
			{
				DSTempPriorLossDataSet.Dispose();
			}
			if(DSTempAddIntrst!=null)
			{
				DSTempAddIntrst.Dispose();
			}
			if(DSTempEquipment!=null)
			{
				DSTempEquipment.Dispose();
			}
			if(DSTempOperators!=null)
			{
				DSTempOperators.Dispose();
			}
			if(DSTempDataSet!=null)
			{
				DSTempDataSet.Dispose();
			}
		}
		public string GetPdfFullWordingXml()
		{
			string FullWordPdfXml="";
			if(gStrLob==(((int)(LOBType.MOT)).ToString()) || gStrLob == ((int)(LOBType.PPA)).ToString())
                FullWordPdfXml =  new Cms.BusinessLayer.BlProcess.ClsAutoPDFXML(gStrClientID, gStrPolicyId, gStrPolicyVersion, CalledFromPolicy,PDFForDecPage,gStateCode,gStrProcessID,ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_CUSTOMER,gstrTemp,"",objWrapper,DSTempPolicyDataSet,DSTempApplicantDataSet,DstempDocument,DSAddWordSet,DSTempAutoDetailDataSet,DSTempRateDataSet,DSTempPriorLossDataSet,DSTempUnderWritDataSet,DSTempAddIntrst,DSTempEquipment,DSTempOperators).getAutoAcordPDFXml();
			else
				FullWordPdfXml = new Cms.BusinessLayer.BlProcess.ClsHomePdfXML(gStrClientID, gStrPolicyId, gStrPolicyVersion, CalledFromPolicy,PDFForDecPage,gStateCode,gStrProcessID,ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_CUSTOMER,gstrTemp,objWrapper,outXml,DSTempPolicyDataSet,DSTempApplicantDataSet,DstempDocument,DSAddWordSet,DSTempAutoDetailDataSet,DSTempRateDataSet,DSTempPriorLossDataSet,DSTempAddIntrst,DSTempOperators).getHomeAcordPDFXml();
			return FullWordPdfXml;
		}
		// Pdf for additinal interest all lobs
		public string PdfForAditionalInterest(string strCalledFor)
		{
			string strAddInrstPdfName="", strAllAdditName="";
			gStrPdfFor=strCalledFor;
			string  Risk_id="", AddlInt_tlog="", addint_id="",Addilname="";
			int addlintindex=0;
			CommonPdfGenrationCode();
			// itrate for each additinal intrest
			foreach(DataRow Row in DSTempAddIntrst.Tables[0].Rows)
			{
				if(gStrLob == (((int)(LOBType.HOME)).ToString()) || gStrLob == ((int)(LOBType.RENT)).ToString())
				{
					Risk_id = Row["DWELLING_ID"].ToString();
					string holder_name = Row["NAME_ADDRESS"].ToString();
					holder_name = holder_name.Substring(0, holder_name.IndexOf(","));
					AddlInt_tlog = ";Holder Name: " + holder_name;
				}
				else if(gStrLob == (((int)(LOBType.MOT)).ToString()) || gStrLob == ((int)(LOBType.PPA)).ToString())
				{
					Risk_id = Row["VEHICLE_ID"].ToString();
					AddlInt_tlog = ";Holder Name: " + Row["HOLDER_NAME"].ToString();
				}
				else if(gStrLob == ((int)(LOBType.WAT)).ToString())
				{
					Risk_id = Row["BOAT_ID"].ToString();
					AddlInt_tlog = ";Holder Name: " + Row["HOLDER_NAME"].ToString();
				}
				addint_id = Row["ADD_INT_ID"].ToString();
				Addilname=Row["ADD_INT_NAME"].ToString();
				strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsAddIntPdfXML(gStrClientID, gStrPolicyId, gStrPolicyVersion, gStrCalledFrom,gStrPdfFor,gStrLob,gStateCode,gStrProcessID,addlintindex,objWrapper,DSTempPolicyDataSet,DSTempApplicantDataSet,DSTempAutoDetailDataSet,DSAddWordSet,DstempDocument,DSTempAddIntrst).getAddIntPDFXml();
				gobjEbixAcordPDF.VehicleDwellingBoat = int.Parse(Risk_id);
				gobjEbixAcordPDF.AddInt = int.Parse(addint_id);
				if(gStrLob==((int)(LOBType.PPA)).ToString())
					gobjEbixAcordPDF.LobCode = "PPA" + "_ADDLINT_" + Addilname;
				else if(gStrLob==((int)(LOBType.MOT)).ToString())
					gobjEbixAcordPDF.LobCode = "MOT" + "_ADDLINT_" + Addilname;
				else if(gStrLob==((int)(LOBType.HOME)).ToString())
					gobjEbixAcordPDF.LobCode = "HOME" + "_ADDLINT_" + Addilname;
				gobjEbixAcordPDF.InputXml = strAcordPDFXml;
				strAddInrstPdfName = ImporsonatAndGenratePdfFile();
				addlintindex++;
				if(strAddInrstPdfName!="")
				{
					string TranLogMess = "Additional Interest PDF generated successfully";
					TransactionLogDetail( AddlInt_tlog,strAddInrstPdfName, TranLogMess);
				}
				strAllAdditName +=strAddInrstPdfName+"~";
			}
			// Run a loop for Watercraft Additinal Intrest attached with HomeOwner 
			if(gStrLob==((int)(LOBType.HOME)).ToString())
			{
				if(DsTempHomeBoatAddIntrst.Tables[0].Rows.Count>0)
				{
					addlintindex=0;
					foreach(DataRow Row in DsTempHomeBoatAddIntrst.Tables[0].Rows)
					{
						Risk_id = Row["BOAT_ID"].ToString();
						AddlInt_tlog = ";Holder Name: " + Row["HOLDER_NAME"].ToString();
						addint_id = Row["ADD_INT_ID"].ToString();
						Addilname=Row["ADD_INT_NAME"].ToString();
						strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsAddIntPdfXML(gStrClientID, gStrPolicyId, gStrPolicyVersion, gStrCalledFrom,gStrPdfFor,((int)(LOBType.WAT)).ToString(),gStateCode,gStrProcessID,addlintindex,objWrapper,DSTempPolicyDataSet,DSTempApplicantDataSet,DSTempAutoDetailDataSet,DSAddWordSet,DstempDocument,DsTempHomeBoatAddIntrst).getAddIntPDFXml();
						gobjEbixAcordPDF.VehicleDwellingBoat = int.Parse(Risk_id);
						gobjEbixAcordPDF.AddInt = int.Parse(addint_id);
						gobjEbixAcordPDF.LobCode = "WAT" + "_ADDLINT_" + Addilname;
						gobjEbixAcordPDF.InputXml = strAcordPDFXml;
						strAddInrstPdfName = ImporsonatAndGenratePdfFile();
						addlintindex++;
						if(strAddInrstPdfName!="")
						{
							string TranLogMess = "Additional Interest PDF generated successfully";
							TransactionLogDetail( AddlInt_tlog,strAddInrstPdfName, TranLogMess);
						}
						strAllAdditName +=strAddInrstPdfName+"~";
					}
				}
			}
			return strAllAdditName;
		}
		// Pdf for Auto Id Card
		public string PdfForAutoIdCard(string strCalledFor)
		{
			string strAutoIDPdfName="";
			gStrPdfFor=strCalledFor;
			CommonPdfGenrationCode();
			strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsAutoIdPdfXML(gStrClientID, gStrPolicyId, gStrPolicyVersion, gStrCalledFrom,gStateCode,gStrProcessID,objWrapper,DSTempApplicantDataSet).getAutoIdPDFXml();
			if(strAcordPDFXml=="")
				return "";
			if(gStrClientID!=null && gStrClientID!="")
				gobjEbixAcordPDF.ClientId = int.Parse(gStrClientID);
			if(gStrPolicyId!=null && gStrPolicyId!="")
				gobjEbixAcordPDF.PolicyId = int.Parse(gStrPolicyId);
			if(gStrPolicyVersion!=null && gStrPolicyVersion!="")
				gobjEbixAcordPDF.PolicyVersion = int.Parse(gStrPolicyVersion);
			if(gStrLob==((int)(LOBType.PPA)).ToString())
				gobjEbixAcordPDF.LobCode = "PPA" + "_AUTO_ID_CARD";
			else if(gStrLob==((int)(LOBType.MOT)).ToString())
				gobjEbixAcordPDF.LobCode = "MOT" + "_AUTO_ID_CARD";
			 
			gobjEbixAcordPDF.InputXml = strAcordPDFXml;
			strAutoIDPdfName = ImporsonatAndGenratePdfFile();
			if(strAutoIDPdfName!="")
			{
				string TranLogMess = "Auto ID Card PDF generated successfully";
				TransactionLogDetail( "",strAutoIDPdfName, TranLogMess);
			}
			return strAutoIDPdfName;
		}
		// Pdf for Auto Id Card
		public string PdfForCustomerDecPage(string strCalledFor, string strGenratePdfFor)
		{
			string strCustPdfName="",  TranLogMess="";
			gStrPdfFor=strCalledFor;
			CommonPdfGenrationCode();
			if(gStrLob==((int)(LOBType.PPA)).ToString())
			{
				gobjEbixAcordPDF.LobCode = "PPA";
				strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsAutoPDFXML(gStrClientID, gStrPolicyId, gStrPolicyVersion, gStrCalledFrom,gStrPdfFor,gStateCode,gStrProcessID,strGenratePdfFor,gstrTemp,outXml,objWrapper,DSTempPolicyDataSet,DSTempApplicantDataSet,DstempDocument,DSAddWordSet,DSTempAutoDetailDataSet,DSTempRateDataSet,DSTempPriorLossDataSet,DSTempUnderWritDataSet,DSTempAddIntrst,DSTempEquipment,DSTempOperators).getAutoAcordPDFXml();
			}
			else if(gStrLob == (((int)(LOBType.MOT)).ToString()))
				gobjEbixAcordPDF.LobCode = "MOT";
			else if(gStrLob == (((int)(LOBType.HOME)).ToString()))
			{
				gobjEbixAcordPDF.LobCode = "HOME";
				strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsHomePdfXML(gStrClientID, gStrPolicyId, gStrPolicyVersion, gStrCalledFrom,gStrPdfFor,gStateCode,gStrProcessID,strGenratePdfFor,gstrTemp,objWrapper,outXml,DSTempPolicyDataSet,DSTempApplicantDataSet,DstempDocument,DSAddWordSet,DSTempAutoDetailDataSet,DSTempRateDataSet,DSTempPriorLossDataSet,DSTempAddIntrst,DSTempOperators).getHomeAcordPDFXml();
			}
			else if(gStrLob == (((int)(LOBType.RENT)).ToString()))
				gobjEbixAcordPDF.LobCode = "RENT";
			else if(gStrLob == (((int)(LOBType.WAT)).ToString()))
				gobjEbixAcordPDF.LobCode = "WAT";	
			
			//reset following values if additional interest present 
			if(DSTempAddIntrst!=null && DSTempAddIntrst.Tables[0].Rows.Count>0)
			{
				gobjEbixAcordPDF.VehicleDwellingBoat = 0;
				gobjEbixAcordPDF.AddInt = 0;
			}

			if(strCalledFor == PDFForDecPage)
			{
				if(strGenratePdfFor == ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_CUSTOMER_NOWORD)
				{
					gobjEbixAcordPDF.LobCode += "_DEC_PAGE_NOWORD";
					TranLogMess = "Declaration Page PDF for Customer generated successfully";
				}
				else if(strGenratePdfFor == ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_CUSTOMER)
				{
					gobjEbixAcordPDF.LobCode += "_DEC_PAGE_C";
					TranLogMess = "Declaration Page PDF for Customer with Additional Wordings generated successfully";
				}
				else if(strGenratePdfFor == ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_AGENCY)
				{
					gobjEbixAcordPDF.LobCode += "_DEC_PAGE_A";
					TranLogMess = "Declaration Page PDF for Agency generated successfully";
				}
				else
				{
					gobjEbixAcordPDF.LobCode += "_DEC_PAGE";
					TranLogMess = "Declaration Page PDF generated successfully";
				}
			}
			else if(strCalledFor == PDFForAcord)
			{
				gobjEbixAcordPDF.LobCode += "_ACORD";
				TranLogMess = "Acord PDF generated successfully";
			}
			gobjEbixAcordPDF.InputXml = strAcordPDFXml;
            //Commented By Chetna to stop Pdf Generation
			//strCustPdfName = ImporsonatAndGenratePdfFile();
			if(gstrTemp!="temp")
				TransactionLogDetail("",strCustPdfName, TranLogMess);
			return strCustPdfName;
		}
		private void CommonPdfGenrationCode()
		{
			if(gStrClientID!=null && gStrClientID!="")
				gobjEbixAcordPDF.ClientId = int.Parse(gStrClientID);
			if(gStrPolicyId!=null && gStrPolicyId!="")
				gobjEbixAcordPDF.PolicyId = int.Parse(gStrPolicyId);
			if(gStrPolicyVersion!=null && gStrPolicyVersion!="")
				gobjEbixAcordPDF.PolicyVersion = int.Parse(gStrPolicyVersion);
				
			if(gStrLob == (((int)(LOBType.HOME)).ToString()))
			{
				gobjEbixAcordPDF.InputPath =  InputBase +"HOME" + "\\" + gStateCode.ToUpper().Trim()  + "\\" ;
				gobjEbixAcordPDF.AlternateInputPath = OutputPath + "EndorsementAttachment\\" + "HOME" + "\\" + gStateCode.ToUpper().Trim()  + "\\" ;
			}
			if(gStrLob == ((int)(LOBType.RENT)).ToString())
			{
				gobjEbixAcordPDF.InputPath =  InputBase +"RENT" + "\\" + gStateCode.ToUpper().Trim()  + "\\" ;
				gobjEbixAcordPDF.AlternateInputPath = OutputPath + "EndorsementAttachment\\" + "RENT" + "\\" + gStateCode.ToUpper().Trim()  + "\\" ;
			}
			if(gStrLob == (((int)(LOBType.MOT)).ToString()))
			{
				gobjEbixAcordPDF.InputPath =  InputBase +"MOT" + "\\" + gStateCode.ToUpper().Trim()  + "\\" ;
				gobjEbixAcordPDF.AlternateInputPath = OutputPath + "EndorsementAttachment\\" + "MOT" + "\\" + gStateCode.ToUpper().Trim()  + "\\" ;
			}
			if(gStrLob == ((int)(LOBType.PPA)).ToString())
			{
				gobjEbixAcordPDF.InputPath =  InputBase +"PPA" + "\\" + gStateCode.ToUpper().Trim()  + "\\" ;
				gobjEbixAcordPDF.AlternateInputPath = OutputPath + "EndorsementAttachment\\" + "PPA" + "\\" + gStateCode.ToUpper().Trim()  + "\\" ;
			}
			gobjEbixAcordPDF.OutputPath = OutputPath + gAgencyCode + "\\" + gStrClientID + "\\" + gStrCalledFrom + "\\" + gstrTemp;
		}
		private void ChkGenrationPathSet()
		{
			gobjEbixAcordPDF.LobCode = "CHK";
			gobjEbixAcordPDF.InputPath = InputBase +"CHK" + "\\";
			gobjEbixAcordPDF.OutputPath = OutputPath + gAgencyCode+ "\\" + "CHK";
		}
		private string ImporsonatAndGenratePdfFile()
		{
			string PDFName="";
			gobjEbixAcordPDF.ImpersonationUserId = ImpersonationUserId;
			gobjEbixAcordPDF.ImpersonationPassword = ImpersonationPassword;
			gobjEbixAcordPDF.ImpersonationDomain = ImpersonationDomain;
			// Genrate Pdf File
			PDFName = gobjEbixAcordPDF.GeneratePDF(gStrCalledFrom, gStrPdfFor);
				
			return PDFName;
		}
		private void TransactionLogDetail(string AddlInt_tlog, string PDFName, string TranLogMess)
		{
			string PDFFinalPath = FinalBasePath + gAgencyCode + "/" + gStrClientID + "/" + gStrCalledFrom + "/" + gstrTemp + "/";
			string PDFlink = PDFName + "<COMMON_PDF_URL=window.open(\"" + PDFFinalPath + PDFName + "\")>";
			if(IsEODProcess)
			{
				WritePDFTransactionLog(int.Parse(gStrClientID),int.Parse(gStrPolicyId), int.Parse(gStrPolicyVersion),TranLogMess,EODUserID , PDFlink, gStrCalledFrom, AddlInt_tlog, objWrapper);
			}
			else
			{
				WritePDFTransactionLog(int.Parse(gStrClientID),int.Parse(gStrPolicyId), int.Parse(gStrPolicyVersion),TranLogMess, int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString()), PDFlink, gStrCalledFrom, AddlInt_tlog, objWrapper);
			}
		}
		private void ChkTransactionLogDetail(string AddlInt_tlog, string PDFName, string TranLogMess)
		{
			string PDFFinalPath = FinalBasePath + gAgencyCode + "/" + "CHK/";
			string PDFlink = PDFName + "<COMMON_PDF_URL=window.open(\"" + PDFFinalPath + PDFName + "\")>";
			if(IsEODProcess)
			{
				WritePDFTransactionLog(0,0,0,TranLogMess,EODUserID , PDFlink, gStrCalledFrom, AddlInt_tlog, objWrapper);
			}
			else
			{
				WritePDFTransactionLog(0,0,0,TranLogMess, int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString()), PDFlink, gStrCalledFrom, AddlInt_tlog, objWrapper);
			}
		}
		#endregion
		
		#region Get Policy, State, Lob Code
		//used to retrieve state code from application or policy details
		public string SetStateCode(string StrCalledFrom,int StrPolicyId,int StrPolicyVersion,int StrClientID)
		{
			DataSet DSTempDataSet = new DataSet();
			string stCode="";
			DSTempDataSet = new DataWrapper(ConnStr,CommandType.Text).ExecuteDataSet("Proc_GetPDFStateCode " + StrClientID + "," + StrPolicyId + "," + StrPolicyVersion + ",'" + StrCalledFrom + "'" );
			if (DSTempDataSet.Tables[0].Rows.Count>0)
			{
				if (DSTempDataSet.Tables[0].Rows[0]["STATE_CODE"].ToString().Trim() !="")
				{
					stCode= DSTempDataSet.Tables[0].Rows[0]["STATE_CODE"].ToString();
				}
			}
			return stCode;
		}
		public string SetStateCode(string StrCalledFrom,int StrPolicyId,int StrPolicyVersion,int StrClientID,DataWrapper objRaper)
		{
			DataSet DSTempDataSet = new DataSet();
			string stCode="";
			objRaper.ClearParameteres();
			objRaper.AddParameter("@Customer_Id",StrClientID);
			objRaper.AddParameter("@AppPol_Id",StrPolicyId);
			objRaper.AddParameter("@AppPolVersion_Id",StrPolicyVersion);
			objRaper.AddParameter("@CalledFrom",StrCalledFrom);
			DSTempDataSet = objRaper.ExecuteDataSet("Proc_GetPDFStateCode");
			objRaper.ClearParameteres();
			//DSTempDataSet = new DataWrapper(ConnStr,CommandType.Text).ExecuteDataSet("Proc_GetPDFStateCode " + StrClientID + "," + StrPolicyId + "," + StrPolicyVersion + ",'" + StrCalledFrom + "'" );
			if (DSTempDataSet.Tables[0].Rows.Count>0)
			{
				if (DSTempDataSet.Tables[0].Rows[0]["STATE_CODE"].ToString().Trim() !="")
				{
					stCode= DSTempDataSet.Tables[0].Rows[0]["STATE_CODE"].ToString();
				}
			}
			return stCode;
		}
		public string GetPolicyStatus(string StrCalledFrom,int StrPolicyId,int StrPolicyVersion,int StrClientID)
		{
			DataSet DSTempDataSet = new DataSet();
			string stStatus="";
			DSTempDataSet = new DataWrapper(ConnStr,CommandType.Text).ExecuteDataSet("Proc_GetPDFStateCode " + StrClientID + "," + StrPolicyId + "," + StrPolicyVersion + ",'" + StrCalledFrom + "'" );
			if (DSTempDataSet.Tables[0].Rows.Count>0)
			{
				if (DSTempDataSet.Tables[0].Rows[0]["POLICY_STATUS"].ToString().Trim() !="")
				{
					stStatus= DSTempDataSet.Tables[0].Rows[0]["POLICY_STATUS"].ToString();
				}
			}
			return stStatus;
		}
		public string GetPolicyProcess(string StrCustomerId, string StrPolicyId,string StrPolicyVersion,string StrCalledFrom)
		{
			DataSet dsTempDataSet = new DataSet();
			dsTempDataSet = new DataWrapper(ConnStr,CommandType.Text).ExecuteDataSet("Proc_GetPDFPolicyDetails " + StrCustomerId + "," + StrPolicyId + "," + StrPolicyVersion + ",'" + StrCalledFrom + "'");
			if(dsTempDataSet.Tables.Count>0 && dsTempDataSet.Tables[0].Rows[0]["PROCESS_ID"].ToString()!="")
			{
				strPolicyProcessID=dsTempDataSet.Tables[0].Rows[0]["PROCESS_ID"].ToString();
			}
			return strPolicyProcessID;
		}

		public string GetPolicyProcess(string StrCustomerId, string StrPolicyId,string StrPolicyVersion,string StrCalledFrom, DataWrapper objtaWrapper)
		{
			DataSet dsTempDataSet = new DataSet();
			objtaWrapper.ClearParameteres();
			objtaWrapper.AddParameter("@CUSTOMERID",StrCustomerId);
			objtaWrapper.AddParameter("@POLID",StrPolicyId);
			objtaWrapper.AddParameter("@VERSIONID",StrPolicyVersion);
			objtaWrapper.AddParameter("@CALLEDFROM",StrCalledFrom);
			dsTempDataSet = objtaWrapper.ExecuteDataSet("Proc_GetPDFPolicyDetails");
			objtaWrapper.ClearParameteres();//+ StrCustomerId + "," + StrPolicyId + "," + StrPolicyVersion + ",'" + StrCalledFrom + "'");
			if(dsTempDataSet.Tables.Count>0 && dsTempDataSet.Tables[0].Rows[0]["PROCESS_ID"].ToString()!="")
			{
				strPolicyProcessID=dsTempDataSet.Tables[0].Rows[0]["PROCESS_ID"].ToString();
			}
			return strPolicyProcessID;
		}

		public string GetPolicyLob(string StrCalledFrom,int StrPolicyId,int StrPolicyVersion,int StrClientID)
		{
			DataSet DSTempDataSet = new DataSet();
			string stStatus="";
			DSTempDataSet = new DataWrapper(ConnStr,CommandType.Text).ExecuteDataSet("Proc_GetPDFStateCode " + StrClientID + "," + StrPolicyId + "," + StrPolicyVersion + ",'" + StrCalledFrom + "'" );
			if (DSTempDataSet.Tables[0].Rows.Count>0)
			{
				if (DSTempDataSet.Tables[0].Rows[0]["LOB"].ToString().Trim() !="")
				{
					stStatus= DSTempDataSet.Tables[0].Rows[0]["LOB"].ToString();
				}
			}
			return stStatus;
		}
		public string GetPolicyLob(string StrCalledFrom,int StrPolicyId,int StrPolicyVersion,int StrClientID, DataWrapper objRaper)
		{
			DataSet DSTempDataSet = new DataSet();
			string stStatus="";
			objRaper.ClearParameteres();
			objRaper.AddParameter("@Customer_Id",StrClientID);
			objRaper.AddParameter("@AppPol_Id",StrPolicyId);
			objRaper.AddParameter("@AppPolVersion_Id",StrPolicyVersion);
			objRaper.AddParameter("@CalledFrom",StrCalledFrom);
			DSTempDataSet = objRaper.ExecuteDataSet("Proc_GetPDFStateCode");
			objRaper.ClearParameteres();
			//DSTempDataSet = new DataWrapper(ConnStr,CommandType.Text).ExecuteDataSet("Proc_GetPDFStateCode " + StrClientID + "," + StrPolicyId + "," + StrPolicyVersion + ",'" + StrCalledFrom + "'" );
			if (DSTempDataSet.Tables[0].Rows.Count>0)
			{
				if (DSTempDataSet.Tables[0].Rows[0]["LOB"].ToString().Trim() !="")
				{
					stStatus= DSTempDataSet.Tables[0].Rows[0]["LOB"].ToString();
				}
			}
			return stStatus;
		}
		#endregion

		//used to remove junk xml characters
		public void SetPDFVersionLobNode(string LOB,DateTime PolicyEffDate)
		{
			PDFVersionsXML = new XmlDocument();
			PDFVersionsXML.Load(CmsWebUrl + "PdfVersions.xml");

			DateTime PDFLobEffDate;
			DateTime PDFLobExpDate;
			foreach(XmlNode Node in PDFVersionsXML.SelectNodes("PDFVERSIONS/LOB[@LOBNAME='" + LOB.Trim().ToUpper() + "']"))
			{
				PDFLobEffDate = DateTime.Parse(getAttributeValue(Node,"EFFECTIVEDATE"));
				
				if (getAttributeValue(Node,"EXPIRYDATE").Trim() == "")
					PDFLobExpDate = System.DateTime.Now;
				else
					PDFLobExpDate = DateTime.Parse(getAttributeValue(Node,"EXPIRYDATE"));

				
				//if((PolicyEffDate >= PDFLobEffDate && PolicyEffDate <=PDFLobExpDate))
				if((PolicyEffDate >= PDFLobEffDate))
				{
					PDFVersionLOBNode = Node;
					return;
				}
			}
		}

		public void SetPDFInsScoresLobNode(string LOB,DateTime PolicyEffDate)
		{
			PDFInsScoresXML = new XmlDocument();
			PDFInsScoresXML.Load(CmsWebUrl + "PdfInsScores.xml");

			//			DateTime PDFLobEffDate;
			//			DateTime PDFLobExpDate;
			foreach(XmlNode Node in PDFInsScoresXML.SelectNodes("PDFINSSCORES/LOB[@LOBNAME='" + LOB.Trim().ToUpper() + "']"))
			{
				//				PDFLobEffDate = DateTime.Parse(getAttributeValue(Node,"EFFECTIVEDATE"));
				//				
				//				if (getAttributeValue(Node,"EXPIRYDATE").Trim() == "")
				//					PDFLobExpDate = System.DateTime.Now;
				//				else
				//					PDFLobExpDate = DateTime.Parse(getAttributeValue(Node,"EXPIRYDATE"));
				//
				//				
				//				//if((PolicyEffDate >= PDFLobEffDate && PolicyEffDate <=PDFLobExpDate))
				//				if((PolicyEffDate >= PDFLobEffDate))
				//				{
				PDFInsScoresLOBNode = Node;
				return;
				//				}
			}
		}

		public bool IsInsScorePage2(int term, string state, string eff_Date, string ins_score)
		{
			string tagNam = "";
			if(term <= 1)
				tagNam = "FTERM";
			else //if(term >= 2)
				tagNam = "NTERM";
			tagNam += state;

			try
			{
				DateTime AppEff = DateTime.Parse(eff_Date);
				DateTime effFrom = DateTime.Parse(PDFInsScoresLOBNode.SelectSingleNode(tagNam.ToUpper().Trim()).Attributes["EFFECTIVEDATE"].Value.ToString());
				DateTime effTo = DateTime.Parse(PDFInsScoresLOBNode.SelectSingleNode(tagNam.ToUpper().Trim()).Attributes["EXPIRYDATE"].Value.ToString());
				string insscorexml = PDFInsScoresLOBNode.SelectSingleNode(tagNam.ToUpper().Trim()).Attributes["INSSCORELOW"].Value.ToString();
				if((AppEff >= effFrom) && (AppEff <= effTo) && (int.Parse(ins_score) < int.Parse(insscorexml)))
					return true;
				else
					return false;
			}
			catch(Exception)
			{
				return false;
			}
		}

		public string getAttributeValue(System.Xml.XmlNode node,string strAttName)
		{
			foreach(XmlAttribute attri in node.Attributes)
			{
				if(attri.Name.ToUpper().Trim() == strAttName.ToUpper().Trim())
				{
					return attri.Value;
				}
			}
			return "";
		}
		public string RemoveJunkXmlCharacters(string strNodeContent)
		{
			strNodeContent = strNodeContent.Replace("&","&amp;");
			strNodeContent = strNodeContent.Replace("<","&lt;");
			strNodeContent = strNodeContent.Replace(">","&gt;");
			strNodeContent = strNodeContent.Replace("'","&apos;");
			strNodeContent = strNodeContent.Replace("\"","&quot;");
			return strNodeContent;
		}

		public void FillMonth()
		{
			if(MonthName.Count==0)
			{
				MonthName.Add("0","");
				MonthName.Add("1","Jan");
				MonthName.Add("2","Feb");
				MonthName.Add("3","Mar");
				MonthName.Add("4","Apr");
				MonthName.Add("5","May");
				MonthName.Add("6","Jun");
				MonthName.Add("7","Jul");
				MonthName.Add("8","Aug");
				MonthName.Add("9","Sep");
				MonthName.Add("10","Oct");
				MonthName.Add("11","Nov");
				MonthName.Add("12","Dec");
			}
		}

		//public string getAcordPDFNameFromXML(XmlDocument PDFVersionsXML,string LOBName,string PdfXmlNodeName)
		public string getAcordPDFNameFromXML(string PdfXmlNodeName)
		{
			//return PDFVersionsXML.SelectSingleNode("PDFVERSIONS/LOB[@LOBNAME='" + LOBName.ToUpper().Trim() + "' and @ISACTIVE='Y']/" + PdfXmlNodeName.ToUpper().Trim()).Attributes["PDFNAME"].Value.ToString();
			return PDFVersionLOBNode.SelectSingleNode(PdfXmlNodeName.ToUpper().Trim()).Attributes["PDFNAME"].Value.ToString();
		}

		//public string getAcordPDFBlockFromXML(XmlDocument PDFVersionsXML,string LOBName,string PdfXmlNodeName)
		public string getAcordPDFBlockFromXML(string PdfXmlNodeName)
		{
			//return PDFVersionsXML.SelectSingleNode("PDFVERSIONS/LOB[@LOBNAME='" + LOBName.ToUpper().Trim() + "' and @ISACTIVE='Y']/" + PdfXmlNodeName.ToUpper().Trim()).Attributes["BLOCKS"].Value.ToString();
			return PDFVersionLOBNode.SelectSingleNode(PdfXmlNodeName.ToUpper().Trim()).Attributes["BLOCKS"].Value.ToString();
		}
		

		#region Common Rate,Discount,Surcharges,Premium Functions  and Split Table Data Set
		public void LoadRateXML(string strLOB)
		{
			try
			{
				if (DSTempDataSet.Tables[0].Rows.Count>0)
				{
					if (DSTempDataSet.Tables[0].Rows[DSTempDataSet.Tables[0].Rows.Count-1]["QUOTE_XML"].ToString().Trim() !="")
					{
						RateXmlDocument.LoadXml(DSTempDataSet.Tables[0].Rows[DSTempDataSet.Tables[0].Rows.Count-1]["QUOTE_XML"].ToString().Trim().Replace("<?xml-stylesheet type='text/xsl' href='FinalXML.xsl'?><!DOCTYPE people [<!ATTLIST homepage xlink:type CDATA #FIXED 'simple' xmlns:xlink CDATA #FIXED 'http://www.w3.org/1999/xlink'> <!ATTLIST person id ID #IMPLIED>]>",""));
						isRateGenerated = true;
					}
				}
			}
			catch(Exception ex)
			{
				System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
				addInfo.Add("Err Descriptor ","Error while loading XML.");
				addInfo.Add("CustomerID" ,gStrClientID);
				addInfo.Add("PolicyID",gStrPolicyId);
				addInfo.Add("PolicyVersionID",gStrPolicyVersion);
				addInfo.Add("ProcessID",gStrProcessID);
				ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex,addInfo );
				throw(new Exception("Error while generating PDF.",ex));

				//throw(ex);
			}
		}
		public void LoadRateXML(string strLOB,DataWrapper objWrapper)
		{
			try
			{
				if(DSTempRateDataSet!=null && strLOB=="HOME-BOAT")
				{
					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@CUSTOMERID",gStrClientID);
					objWrapper.AddParameter("@POLID",gStrPolicyId);
					objWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);
					objWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);
					objWrapper.AddParameter("@LOB","HOME-BOAT");
					DSTempRateDataSet = objWrapper.ExecuteDataSet("Proc_GetPDFQuoteRateXML");
				}
				if (DSTempRateDataSet.Tables[0].Rows.Count>0)
				{
					if (DSTempRateDataSet.Tables[0].Rows[DSTempRateDataSet.Tables[0].Rows.Count-1]["QUOTE_XML"].ToString().Trim() !="")
					{
						RateXmlDocument.LoadXml(DSTempRateDataSet.Tables[0].Rows[DSTempRateDataSet.Tables[0].Rows.Count-1]["QUOTE_XML"].ToString().Trim().Replace("<?xml-stylesheet type='text/xsl' href='FinalXML.xsl'?><!DOCTYPE people [<!ATTLIST homepage xlink:type CDATA #FIXED 'simple' xmlns:xlink CDATA #FIXED 'http://www.w3.org/1999/xlink'> <!ATTLIST person id ID #IMPLIED>]>",""));
						isRateGenerated = true;
					}
				}
			}
			catch(Exception ex)
			{
				System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
				addInfo.Add("Err Descriptor ","Error while loading XML.");
				addInfo.Add("CustomerID" ,gStrClientID);
				addInfo.Add("PolicyID",gStrPolicyId);
				addInfo.Add("PolicyVersionID",gStrPolicyVersion);
				addInfo.Add("ProcessID",gStrProcessID);
				ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex,addInfo );
				throw(new Exception("Error while generating PDF.",ex));

				//throw(ex);
			}
		}
		public void LoadRateXML(string strLOB,DataSet lDSetRate)
		{
			try
			{
				if (lDSetRate.Tables[0].Rows.Count>0)
				{
					if (lDSetRate.Tables[0].Rows[lDSetRate.Tables[0].Rows.Count-1]["QUOTE_XML"].ToString().Trim() !="")
					{
						RateXmlDocument.LoadXml(lDSetRate.Tables[0].Rows[lDSetRate.Tables[0].Rows.Count-1]["QUOTE_XML"].ToString().Trim().Replace("<?xml-stylesheet type='text/xsl' href='FinalXML.xsl'?><!DOCTYPE people [<!ATTLIST homepage xlink:type CDATA #FIXED 'simple' xmlns:xlink CDATA #FIXED 'http://www.w3.org/1999/xlink'> <!ATTLIST person id ID #IMPLIED>]>",""));
						isRateGenerated = true;
					}
				}
			}
			catch(Exception ex)
			{
				System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
				addInfo.Add("Err Descriptor ","Error while loading XML.");
				addInfo.Add("CustomerID" ,gStrClientID);
				addInfo.Add("PolicyID",gStrPolicyId);
				addInfo.Add("PolicyVersionID",gStrPolicyVersion);
				addInfo.Add("ProcessID",gStrProcessID);
				ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex,addInfo );
				throw(new Exception("Error while generating PDF.",ex));

				//throw(ex);
			}
		}
		public string GetPremiumAll(DataSet DSCov, string Comp_code,string risk_id)
		{
			try
			{
				if(DSCov.Tables.Count > 1)
				{
					foreach(DataRow premRow in DSCov.Tables[1].Rows)
					{
						if(premRow["COMPONENT_CODE"].ToString() == Comp_code && premRow["RISK_ID"].ToString() == risk_id)
							return premRow["COVERAGE_PREMIUM"].ToString();
					}
				}
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);				
			}
			return "";
		}
		// get sum of premium irrespective of risk id
		public double GetPremiumAllHomeBoat(DataSet DSCov, string Comp_code,string risk_id)
		{
			try
			{
				double intSumTotal=0;
				if(DSCov.Tables.Count >= 1)
				{
					foreach(DataRow premRow in DSCov.Tables[0].Rows)
					{
						if(premRow["COMPONENT_CODE"].ToString() == Comp_code && premRow["RISK_ID"].ToString() == risk_id)
						{
							if(premRow["COVERAGE_PREMIUM"].ToString()!="" && (premRow["RISK_TYPE"].ToString().ToUpper().Trim()=="HOME" || premRow["RISK_TYPE"].ToString().ToUpper().Trim()=="RV"))
								intSumTotal+=double.Parse(premRow["COVERAGE_PREMIUM"].ToString());
						}
					}
				}
				return intSumTotal;
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);				
			}
			return 0;
		}
		// get sum of premium irrespective of risk id
		public double GetPremiumAllHomeBoat(DataSet DSCov, string Comp_code)
		{
			try
			{
				double intSumTotal=0;
				if(DSCov.Tables.Count >= 1)
				{
					foreach(DataRow premRow in DSCov.Tables[0].Rows)
					{
						if(premRow["COMPONENT_CODE"].ToString() == Comp_code)
						{
							if(premRow["COVERAGE_PREMIUM"].ToString()!="")
								intSumTotal+=double.Parse(premRow["COVERAGE_PREMIUM"].ToString());
						}
					}
				}
				return intSumTotal;
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);				
			}
			return 0;
		}
		//Get sum total of risk by risk type
		public double GetPremiumAllHomeBoat(DataSet DSCov, string Comp_code,string risk_id,string risk_type)
		{
			try
			{
				double intSumTotal=0;
				if(DSCov.Tables.Count >= 1)
				{
					foreach(DataRow premRow in DSCov.Tables[0].Rows)
					{
						if(premRow["COMPONENT_CODE"].ToString() == Comp_code && premRow["RISK_ID"].ToString() == risk_id)
						{
							if(premRow["COVERAGE_PREMIUM"].ToString()!="" && premRow["RISK_TYPE"].ToString().ToUpper().Trim()=="HOME")
								intSumTotal+=double.Parse(premRow["COVERAGE_PREMIUM"].ToString());
							//else if(premRow["COVERAGE_PREMIUM"].ToString()!="" && premRow["RISK_TYPE"].ToString().ToUpper().Trim()=="RV")
							//	intSumTotal+=double.Parse(premRow["COVERAGE_PREMIUM"].ToString());
						}
					}
				}
				return intSumTotal;
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);				
			}
			return 0;
		}

		//Get Auto POlicy Total Premium After Policy Commit
		public string GetSumTotalPremium(DataSet DSCov, string Comp_code,string risk_id)
		{
			try
			{
				if(DSCov.Tables.Count > 0)
				{
					foreach(DataRow premRow in DSCov.Tables[0].Rows)
					{
						if(premRow["COMPONENT_CODE"].ToString() == Comp_code && premRow["RISK_ID"].ToString() == risk_id)
							return premRow["COVERAGE_PREMIUM"].ToString();
					}
				}
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);				
			}
			return "";
		}
		// Get boat deuctible 
		// adjusted premium risk wise
		public string GetDeducWat(DataSet DSCov, string Cov_Code)
		{
			try
			{
				if(DSCov.Tables.Count > 0)
				{
					foreach(DataRow premRow in DSCov.Tables[0].Rows)
					{
						if(premRow["COV_CODE"].ToString() == Cov_Code)
							return premRow["DEDUCTIBLE_1"].ToString();
					}
				}
			}
			catch//(Exception ex)
			{
			}
			return "";
		}
		// adjusted premium risk wise
		public string GetPremiumRedw(DataSet DSCov, string Comp_code,string risktype)
		{
			try
			{
				if(DSCov.Tables.Count > 1)
				{
					foreach(DataRow premRow in DSCov.Tables[1].Rows)
					{
						if(premRow["COMPONENT_CODE"].ToString() == Comp_code && premRow["RISK_TYPE"].ToString() == risktype)
							return premRow["WRITTEN_PREMIUM"].ToString();
					}
				}
			}
			catch//(Exception ex)
			{
			}
			return "";
		}
		//uNATTACHED  equipment premium
		public double GetPremiumUnattached(DataSet DSCov,string risktype,string PremiumType)
		{
			double sumall = 0.0;
			try
			{
				if(DSCov.Tables.Count > 2)
				{
					foreach(DataRow premRow in DSCov.Tables[3].Rows)
					{
						if(premRow["RISK_TYPE"].ToString() == risktype)
							if(premRow[PremiumType].ToString()!="" || premRow[PremiumType].ToString()!="0" || premRow[PremiumType].ToString()!="0.00")
							{
								sumall +=  double.Parse(premRow[PremiumType].ToString());
							}
					}
				}
			}
			catch//(Exception ex)
			{
			}
			return sumall;
		}
		// added for trialer premium
		public double GetTrailerPremium(DataSet DsTrai,string PremiumType)
		{
			double sumall = 0.0;
			try
			{
				if(DsTrai.Tables.Count > 0)
				{
					foreach(DataRow PremRow in DsTrai.Tables[0].Rows)
					{
						sumall += double.Parse(PremRow[PremiumType].ToString());
					}

				}
			}
			catch//(Exception ex)
			{
			}
			return sumall;
		}
		// added for Schedule Equipment
		public double GetPremiumSch(DataSet DSCov,string risktype,string PremiumType)
		{
			double sumall = 0.0;
			try
			{
				if(DSCov.Tables.Count > 2)
				{
					foreach(DataRow premRow in DSCov.Tables[2].Rows)
					{
						if(premRow["RISK_TYPE"].ToString() == risktype)
							sumall +=  double.Parse(premRow[PremiumType].ToString());
					}
				}
			}
			catch//(Exception ex)
			{
			}
			return sumall;
		}

		public double GetPremiumBoat(DataSet DSCov,string risktype,string riskid,string PremiumType)
		{
			double sumall = 0.0;
			try
			{
				if(DSCov.Tables.Count > 2)
				{
					foreach(DataRow premRow in DSCov.Tables[2].Rows)
					{
						if(premRow["RISK_TYPE"].ToString() == risktype && premRow["RISK_ID"].ToString() == riskid)
							sumall =  double.Parse(premRow[PremiumType].ToString());
					}
				}
			}
			catch//(Exception ex)
			{
			}
			return sumall;
		}

		// get total premium for rental dwelling
		public double GetPremiumRental(DataSet DSCov,string risktype,string riskid,string PremiumType)
		{
			double sumall = 0.0;
			try
			{
				if(DSCov.Tables.Count > 2)
				{
					foreach(DataRow premRow in DSCov.Tables[2].Rows)
					{
						if(premRow["RISK_TYPE"].ToString() == risktype && premRow["RISK_ID"].ToString() == riskid)
							sumall =  double.Parse(premRow[PremiumType].ToString());
					}
				}
			}
			catch//(Exception ex)
			{
			}
			return sumall;
		}
		public double GetPremiumAll(DataSet DSCov, string Comp_code)
		{
			double sumall = 0.0;
			try
			{
				if(DSCov.Tables.Count > 1)
				{
					foreach(DataRow premRow in DSCov.Tables[1].Rows)
					{
						if(premRow["COMPONENT_CODE"].ToString() == Comp_code && premRow["COVERAGE_PREMIUM"].ToString() != "")
							sumall +=  double.Parse(premRow["COVERAGE_PREMIUM"].ToString());
					}
				}
			}
			catch//(Exception ex)
			{
			}
			return sumall;
		}

		public string GetPremium(DataSet DSCov, string cov_code)
		{
			try
			{
				if(DSCov.Tables.Count > 1)
				{
					foreach(DataRow premRow in DSCov.Tables[1].Rows)
					{
						if(premRow["COV_CODE"].ToString() == cov_code)
							return premRow["COVERAGE_PREMIUM"].ToString();
					}
				}
			}
			catch//(Exception ex)
			{
			}
			return "";
		}
		public string GetTotalRiskPremium(DataSet DSCov, string Comp_code)
		{
			try
			{
				if(DSCov.Tables.Count > 1)
				{
					foreach(DataRow premRow in DSCov.Tables[1].Rows)
					{
						if(premRow["COMPONENT_CODE"].ToString() == Comp_code)
							return premRow["COVERAGE_PREMIUM"].ToString();
					}
				}
			}
			catch//(Exception ex)
			{
			}
			return "";
		}
		public string GetPremiumBeforeCommit(DataSet DSCov, string cov_code, Hashtable htpremium)
		{
			try
			{
				string strCOMPONENT_CODE="";
				if(DSCov.Tables[0].Rows.Count > 0)
				{
					foreach(DataRow premRow in DSCov.Tables[0].Rows)
					{
						if(premRow["COV_CODE"].ToString() == cov_code)
						{
							strCOMPONENT_CODE = premRow["COMPONENT_CODE"].ToString();
							break;
						}
					}
					if(strCOMPONENT_CODE != "")
					{
						if (htpremium.Count != 0)
						{
							IDictionaryEnumerator Enumerator = htpremium.GetEnumerator();
							while (Enumerator.MoveNext())
							{
								for(int i=0 ; i< DSCov.Tables[0].Rows.Count ; i++)
								{
									if(Enumerator.Key.Equals(strCOMPONENT_CODE))
									{
										//string boat_ded1= DSTempEngineTrailer1.Tables[0].Rows[i]["DEDUCTIBLE_1"].ToString();
										//string boat_Limit1= DSTempEngineTrailer1.Tables[0].Rows[i]["LIMIT_1"].ToString();
										//BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATLIMIT  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters("$" + boat_Limit1.Replace(".00",""))+"</BOATLIMIT>";
										//BoatElementDecPage.InnerXml= BoatElementDecPage.InnerXml +  "<BOATDED  " + fieldType +"=\""+ fieldTypeText +"\">"+RemoveJunkXmlCharacters(boat_ded1.Replace(".00",""))+"</BOATDED>";
										string covPrem = Enumerator.Value.ToString();
										int lintGetPremium = covPrem.IndexOf(".");
										if(lintGetPremium == -1)
										{
											covPrem= covPrem + ".00";
										}
										return covPrem;
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
			return "";
		}
		public string GetRVPremiumBeforeCommit(string strCOMPONENT_CODE, Hashtable htpremium)
		{
			try
			{
					if(strCOMPONENT_CODE != "")
					{
						if (htpremium.Count != 0)
						{
							IDictionaryEnumerator Enumerator = htpremium.GetEnumerator();
							while (Enumerator.MoveNext())
							{
//								for(int i=0 ; i< DSCov.Tables[0].Rows.Count ; i++)
//								{
									if(Enumerator.Key.Equals(strCOMPONENT_CODE))
									{
										string covPrem = Enumerator.Value.ToString();
										int lintGetPremium = covPrem.IndexOf(".");
										if(lintGetPremium == -1)
										{
											covPrem= covPrem + ".00";
										}
										return covPrem;
									}
								//}
							}
						}
					}
				}
			catch(Exception ex)
			{
				throw(ex);
			}
			return "";
		}
		public XmlNodeList GetTransfer_Renewal(string EntityID)
		{
			// Transfer Renewal Discount
			return (RateXmlDocument.SelectNodes("//RISK[@ID='" + EntityID + "']/STEP[@COMPONENT_TYPE='D_XFR' and @STEPPREMIUM!='0']"));	
		}

		//Added by Mohit Agarwal 26-Sep-2007
		public XmlNodeList GetRentalTerritory()
		{
			return( RateXmlDocument.SelectNodes("//STEP[@TS='0']"));
			//PRODUCTDESC DP-2 - REPLACE (Territory :
		}
		public XmlNodeList GetHomeTerritory()
		{
			return (RateXmlDocument.SelectNodes("//RISK[@TYPE='HOME']"));	
		}
		public XmlNodeList GetRentalPremGroup()
		{
			return( RateXmlDocument.SelectNodes("//STEP[contains(@GROUPDESC,'Premium Group:')]"));
		}
		
		public XmlNodeList GetMinPremSch()
		{

			return (RateXmlDocument.SelectNodes("//STEP[@COMPONENT_TYPE='P' and @STEPPREMIUM!='' and @STEPPREMIUM!='0' and @COMPONENT_CODE='MINIMUMPREMIUM_SCH']")); 
			//return (RateXmlDocument.SelectNodes("//RISK[@ID='" + EntityID + "']/STEP[@COMPONENT_TYPE='D' and @STEPDESC!='' and @STEPDESC!='0']")); 

		}

		public XmlNodeList GetCreditForInsScore()
		{

			return (RateXmlDocument.SelectNodes("//STEP[@COMPONENT_TYPE='D' and @STEPDESC!='' and @STEPDESC!='0' and @COMPONENT_CODE='BOAT_INS_SCORE']")); 
			//return (RateXmlDocument.SelectNodes("//RISK[@ID='" + EntityID + "']/STEP[@COMPONENT_TYPE='D' and @STEPDESC!='' and @STEPDESC!='0']")); 

		}

		public XmlNodeList GetCreditForHomeInsScore()
		{

			return (RateXmlDocument.SelectNodes("//STEP[@COMPONENT_TYPE='D' and @STEPDESC!='' and @STEPDESC!='0' and @COMPONENT_CODE='D_INS_SCR']")); 
			//return (RateXmlDocument.SelectNodes("//RISK[@ID='" + EntityID + "']/STEP[@COMPONENT_TYPE='D' and @STEPDESC!='' and @STEPDESC!='0']")); 

		}

		public XmlNodeList GetCreditForMotInsScore()
		{

			return (RateXmlDocument.SelectNodes("//STEP[@COMPONENT_TYPE='D' and @STEPDESC!='' and @STEPDESC!='0' and @COMPONENT_CODE='D_INS_SCR']")); 
			//return (RateXmlDocument.SelectNodes("//RISK[@ID='" + EntityID + "']/STEP[@COMPONENT_TYPE='D' and @STEPDESC!='' and @STEPDESC!='0']")); 

		}

		public XmlNodeList GetCredits(string EntityID)
		{
			//return (RateXmlDocument.SelectNodes("//STEP[@COMPONENT_TYPE='D' and @STEPDESC!='' and @STEPDESC!='0' and @STEPPREMIUM!='' and @STEPPREMIUM!='0']"));	
			return (RateXmlDocument.SelectNodes("//RISK[@ID='" + EntityID + "']/STEP[@COMPONENT_TYPE='D' and @STEPDESC!='' and @STEPDESC!='0' and @STEPDESC!=' 0 ' and @STEPPREMIUM!='0' and  @STEPPREMIUM!='0.00']"));	
		}

		public XmlNodeList GetCredits()
		{
			return (RateXmlDocument.SelectNodes("//STEP[@COMPONENT_TYPE='D' and @STEPDESC!='' and @STEPDESC!='0' and @STEPPREMIUM!='' and @STEPPREMIUM!='0' ]"));	
			//return (RateXmlDocument.SelectNodes("//RISK[@ID='" + EntityID + "']/STEP[@COMPONENT_TYPE='D' and @STEPDESC!='' and @STEPDESC!='0']"));	
		}

		public XmlNodeList GetSurcharges(string EntityID)
		{
			//return (RateXmlDocument.SelectNodes("//STEP[@COMPONENT_TYPE='S' and @STEPDESC!='' and @STEPDESC!='0' and @STEPPREMIUM!='' and @STEPPREMIUM!='0']"));	
			return (RateXmlDocument.SelectNodes("//RISK[@ID='" + EntityID + "']/STEP[@COMPONENT_TYPE='S' and @STEPDESC!='' and @STEPDESC!='0'  and @STEPPREMIUM!='' and @STEPPREMIUM!='0' and  @STEPPREMIUM!='0.00' ]"));	
		}

		public XmlNodeList GetSurcharges()
		{
			return (RateXmlDocument.SelectNodes("//STEP[@COMPONENT_TYPE='S' and @STEPDESC!='' and @STEPDESC!='0' and @STEPPREMIUM!='' and @STEPPREMIUM!='0']"));	
			//return (RateXmlDocument.SelectNodes("//RISK[@ID='" + EntityID + "']/STEP[@COMPONENT_TYPE='S' and @STEPDESC!='' and @STEPDESC!='0']"));	
		}

		public XmlNodeList GetPremium(string EntityID)
		{
			//return (RateXmlDocument.SelectNodes("//STEP[@COMPONENT_TYPE='D' and @STEPDESC!='' and @STEPDESC!='0' and @STEPPREMIUM!='' and @STEPPREMIUM!='0']"));	
			return (RateXmlDocument.SelectNodes("//RISK[@ID='" + EntityID + "']/STEP[@COMPONENT_TYPE='P' and @STEPPREMIUM!='' and @STEPPREMIUM!='0' and @STEPPREMIUM!='Included' and @STEPPREMIUM!='Extended']"));	
		}
		public XmlNodeList GetUnattachedPremium(string EntityID)
		{
			//return (RateXmlDocument.SelectNodes("//STEP[@COMPONENT_TYPE='D' and @STEPDESC!='' and @STEPDESC!='0' and @STEPPREMIUM!='' and @STEPPREMIUM!='0']"));	
			return (RateXmlDocument.SelectNodes("//RISK[@ID='" + EntityID + "' and @TYPE='UAE']/STEP[@COMPONENT_TYPE='P' and @STEPPREMIUM!='' and @STEPPREMIUM!='0' and @STEPPREMIUM!='Included' and @STEPPREMIUM!='Extended']"));	
		}

		public XmlNodeList GetPropertyExpenseFee(string EntityID)
		{
			return (RateXmlDocument.SelectNodes("//RISK[@ID='" + EntityID + "']/STEP[@COMPONENT_TYPE='P' and @COMPONENT_CODE='PRP_EXPNS_FEE' and @STEPPREMIUM!='' and @STEPPREMIUM!='0' and @STEPPREMIUM!='Included' and @STEPPREMIUM!='Extended']"));	
		}
		public XmlNodeList GetBoatPremium(string EntityID)
		{
			//return (RateXmlDocument.SelectNodes("//STEP[@COMPONENT_TYPE='D' and @STEPDESC!='' and @STEPDESC!='0' and @STEPPREMIUM!='' and @STEPPREMIUM!='0']"));	
			return (RateXmlDocument.SelectNodes("//RISK[@ID='" + EntityID + "' and @TYPE='B']/STEP[@COMPONENT_TYPE='P' and @STEPPREMIUM!='' and @STEPPREMIUM!='0' and @STEPPREMIUM!='Included' and @STEPPREMIUM!='Extended']"));	
		}

		public XmlNodeList GetAutoPremium(string EntityID)
		{
			//return (RateXmlDocument.SelectNodes("//STEP[@COMPONENT_TYPE='D' and @STEPDESC!='' and @STEPDESC!='0' and @STEPPREMIUM!='' and @STEPPREMIUM!='0']"));	
			return (RateXmlDocument.SelectNodes("//RISK[@ID='" + EntityID + "']/STEP[@COMPONENT_TYPE='P' and @STEPPREMIUM!='' and @STEPPREMIUM!='0' and @STEPPREMIUM!='Included' and @STEPPREMIUM!='Extended']"));	
		}

		public XmlNodeList GetTrailerPremium(string EntityID)
		{
			//return (RateXmlDocument.SelectNodes("//STEP[@COMPONENT_TYPE='D' and @STEPDESC!='' and @STEPDESC!='0' and @STEPPREMIUM!='' and @STEPPREMIUM!='0']"));	
			return (RateXmlDocument.SelectNodes("//RISK[@ID='" + EntityID + "' and @TYPE='T']/STEP[@COMPONENT_TYPE='P' and @STEPPREMIUM!='' and @STEPPREMIUM!='0' and @STEPPREMIUM!='Included' and @STEPPREMIUM!='Extended']"));	
		}
		public XmlNodeList GetRVPremium(string EntityID)
		{
			return (RateXmlDocument.SelectNodes("//RISK[@ID='" + EntityID + "' and @TYPE='RV']/STEP[@COMPONENT_TYPE='P' and @STEPPREMIUM!='' and @STEPPREMIUM!='0' and @STEPPREMIUM!='Included' and @STEPPREMIUM!='Extended']"));	
		}

		public XmlNodeList GetPremium()
		{
			//return (RateXmlDocument.SelectNodes("//STEP[@COMPONENT_TYPE='D' and @STEPDESC!='' and @STEPDESC!='0' and @STEPPREMIUM!='' and @STEPPREMIUM!='0']"));	
			return (RateXmlDocument.SelectNodes("//STEP[@COMPONENT_TYPE='P' and @STEPPREMIUM!='' and @STEPPREMIUM!='0' and @STEPPREMIUM!='Included' and @STEPPREMIUM!='Extended']"));	
		}

		public XmlNodeList GetSchPremium()
		{
			//return (RateXmlDocument.SelectNodes("//STEP[@COMPONENT_TYPE='D' and @STEPDESC!='' and @STEPDESC!='0' and @STEPPREMIUM!='' and @STEPPREMIUM!='0']"));	
			//			return (RateXmlDocument.SelectNodes("//SCHEDULEDMISCSPORTS/STEP[@COMPONENT_TYPE='P' and @STEPPREMIUM!='' and @STEPPREMIUM!='0' and @STEPPREMIUM!='Included' and @STEPPREMIUM!='Extended']"));	
			return (RateXmlDocument.SelectNodes("//RISK[@TYPE='MSE']/STEP[@COMPONENT_TYPE='P' and @STEPPREMIUM!='' and @STEPPREMIUM!='0' and @STEPPREMIUM!='Included' and @STEPPREMIUM!='Extended']"));	
		}
		public XmlNodeList GetSchArtPremium()
		{
			return (RateXmlDocument.SelectNodes("//RISK[@TYPE='HOME']/STEP[@COMPONENT_TYPE='P' and (@COMPONENT_CODE='BICYCLE' or @COMPONENT_CODE='CMRA' or @COMPONENT_CODE='CELL' or @COMPONENT_CODE='FUR' or @COMPONENT_CODE='GOLF' or @COMPONENT_CODE='GUN' or @COMPONENT_CODE='JWL' or @COMPONENT_CODE='MSC_NON_PRF' or @COMPONENT_CODE='PRS_CMP' or @COMPONENT_CODE='SLVR' or @COMPONENT_CODE='STMP' or @COMPONENT_CODE='RARE_COIN' or @COMPONENT_CODE='FNE_ART_WO_BRK' or @COMPONENT_CODE='FNE_ART_WTH_BRK' or @COMPONENT_CODE='HNDCP_ELCT' or @COMPONENT_CODE='HRNG_AD' or @COMPONENT_CODE='INSLN_PMP' or @COMPONENT_CODE='MRT_KY_AMWY' or @COMPONENT_CODE='PC_LPTP' or @COMPONENT_CODE='SLMN_SPPLY' or @COMPONENT_CODE='SCB_DVNG_EQP' or @COMPONENT_CODE='SNW_SKY' or @COMPONENT_CODE='TCK_SDDL' or @COMPONENT_CODE='TOOLS_PRM' or @COMPONENT_CODE='TOOLS_BSNS' or @COMPONENT_CODE='TRCTR' or @COMPONENT_CODE='TRN_CLLCTN' or @COMPONENT_CODE='WHLCHR')and @STEPPREMIUM!='0' and @STEPPREMIUM!='Included' and @STEPPREMIUM!='Extended']"));	
		}
		public XmlNodeList GetSumTotalPremium()
		{
			return (RateXmlDocument.SelectNodes("//STEP[@COMPONENT_CODE='SUMTOTAL' and @STEPDESC!='' and @STEPDESC!='0' and @STEPPREMIUM!='' and @STEPPREMIUM!='Included' and @STEPPREMIUM!='Extended']"));	
			//return (RateXmlDocument.SelectSingleNode("//STEP[@COMPONENT_CODE='SUMTOTAL' and @STEPPREMIUM!='' and @STEPPREMIUM!='0' and @STEPPREMIUM!='Included']").Attributes["STEPPREMIUM"].Value);	
		}
		
		public XmlNodeList GetUnattachedPremium()
		{
			return (RateXmlDocument.SelectNodes("//STEP[@COMPONENT_CODE='BOAT_UNATTACH_PREMIUM' and @STEPDESC!='' and @STEPDESC!='0' and @STEPPREMIUM!='' and @STEPPREMIUM!='Included' and @STEPPREMIUM!='Extended']"));	
			//return (RateXmlDocument.SelectSingleNode("//STEP[@COMPONENT_CODE='SUMTOTAL' and @STEPPREMIUM!='' and @STEPPREMIUM!='0' and @STEPPREMIUM!='Included']").Attributes["STEPPREMIUM"].Value);	
		}

		public XmlNodeList GetSumTotalPremium(string strEntityID )
		{
			return (RateXmlDocument.SelectNodes("//RISK[@ID='" + strEntityID + "']/STEP[@COMPONENT_CODE='SUMTOTAL' and @STEPDESC!='' and @STEPDESC!='0' and @STEPPREMIUM!='' and @STEPPREMIUM!='Included' and @STEPPREMIUM!='Extended']"));	
			//return (RateXmlDocument.SelectSingleNode("//STEP[@COMPONENT_CODE='SUMTOTAL' and @STEPPREMIUM!='' and @STEPPREMIUM!='0' and @STEPPREMIUM!='Included']").Attributes["STEPPREMIUM"].Value);	
		}
		public XmlNodeList GetHomeSumTotalPremium(string strEntityID,string strType)
		{
			return (RateXmlDocument.SelectNodes("//RISK[@ID='" + strEntityID + "' and @TYPE='" + strType + "']/STEP[@COMPONENT_CODE='SUMTOTAL' and @STEPDESC!='' and @STEPDESC!='0' and @STEPPREMIUM!='' and @STEPPREMIUM!='Included' and @STEPPREMIUM!='Extended']"));	
			//return (RateXmlDocument.SelectSingleNode("//STEP[@COMPONENT_CODE='SUMTOTAL' and @STEPPREMIUM!='' and @STEPPREMIUM!='0' and @STEPPREMIUM!='Included']").Attributes["STEPPREMIUM"].Value);	
		}
		public XmlNodeList GetHomeSumGrandPremium()
		{
			return (RateXmlDocument.SelectNodes("//GRANDTOTAL"));	
		}
		public XmlNodeList GetSumTotalTrailerPremium(string EntityID)
		{
			return (RateXmlDocument.SelectNodes("//RISK[@ID='" + EntityID + "' and @TYPE='T']/STEP[@COMPONENT_TYPE='P' and @COMPONENT_CODE='SUMTOTAL' and @STEPDESC!='' and @STEPDESC!='0']"));	
			//return (RateXmlDocument.SelectSingleNode("//STEP[@COMPONENT_CODE='SUMTOTAL' and @STEPPREMIUM!='' and @STEPPREMIUM!='0' and @STEPPREMIUM!='Included']").Attributes["STEPPREMIUM"].Value);	
		}

		public XmlNodeList GetSumTotalTrailerPremium()
		{
			return (RateXmlDocument.SelectNodes("//RISK[@TYPE='T']/STEP[@COMPONENT_TYPE='P' and @COMPONENT_CODE='SUMTOTAL' and @STEPDESC!='' and @STEPDESC!='0']"));	
			//return (RateXmlDocument.SelectSingleNode("//STEP[@COMPONENT_CODE='SUMTOTAL' and @STEPPREMIUM!='' and @STEPPREMIUM!='0' and @STEPPREMIUM!='Included']").Attributes["STEPPREMIUM"].Value);	
		}

		public XmlNodeList GetInsuranceDiscountPercent(string EntityID)
		{
			return (RateXmlDocument.SelectNodes("//RISK[@ID='" + EntityID + "']/STEP[@COMPONENT_TYPE='D' and @COMPONENT_CODE='D_INS_SCR' and @STEPDESC!='' and @STEPDESC!='0']"));	
			//return (RateXmlDocument.SelectSingleNode("//STEP[@COMPONENT_CODE='SUMTOTAL' and @STEPPREMIUM!='' and @STEPPREMIUM!='0' and @STEPPREMIUM!='Included']").Attributes["STEPPREMIUM"].Value);	
		}
		
		public XmlNodeList GetMCCAFee(string EntityID)
		{
			return (RateXmlDocument.SelectNodes("//RISK[@ID='" + EntityID + "']/STEP[@COMPONENT_TYPE='P' and @COMPONENT_CODE='MCCAFEE' and @STEPDESC!='' and @STEPPREMIUM!='0']"));	
			//return (RateXmlDocument.SelectSingleNode("//STEP[@COMPONENT_CODE='SUMTOTAL' and @STEPPREMIUM!='' and @STEPPREMIUM!='0' and @STEPPREMIUM!='Included']").Attributes["STEPPREMIUM"].Value);	
		}

		public XmlNodeList GetMiscScheduleEquipmentCompColl(string EntityID)
		{
			return (RateXmlDocument.SelectNodes("//RISK[@ID='" + EntityID + "']/STEP[@COMPONENT_TYPE='P' and (@COMPONENT_CODE='XTR_COMP' or @COMPONENT_CODE='XTR_COLL') and @STEPDESC!='' and @STEPDESC!='0']"));	
			//return (RateXmlDocument.SelectSingleNode("//STEP[@COMPONENT_CODE='SUMTOTAL' and @STEPPREMIUM!='' and @STEPPREMIUM!='0' and @STEPPREMIUM!='Included']").Attributes["STEPPREMIUM"].Value);	
		}
		
		#endregion

		
		#region Transaction Log Function
		public void WritePDFTransactionLog(int CustomerID, int App_PolicyID, int App_PolicyVersionID, string TransactionDescription, int RecordedBy, string CustomDesc, string strCalledFrom, string AddlInt_tlog, DataWrapper objDataWrapper)
		{
			Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
			objTransactionInfo.CUSTOM_INFO				=	CustomDesc;
			objTransactionInfo.TRANS_TYPE_ID			=	3;
			objTransactionInfo.CLIENT_ID				=	CustomerID;
			if(strCalledFrom.ToUpper().Equals("POLICY"))
			{
				objTransactionInfo.POLICY_ID				=	App_PolicyID;
				objTransactionInfo.POLICY_VER_TRACKING_ID	=	App_PolicyVersionID;
			}
			else if(strCalledFrom.ToUpper().Equals("APPLICATION"))
			{
				objTransactionInfo.APP_ID					=	App_PolicyID;
				objTransactionInfo.APP_VERSION_ID			=	App_PolicyVersionID;
			}
				/*else if(strCalledFrom.Equals("CUST_RECEIPT"))
				{
				}*/
				else if(strCalledFrom.ToUpper().Equals("CHECKPDFPRINT"))
				{
					if(CustomerID == 0)
					{
						objTransactionInfo.CUSTOM_INFO = "";
						//objTransactionInfo.CUSTOM_INFO += CustomDesc + ";" ;
						string []PayeeArr = PayeeName.Split('~');
						string []NumArr = CheckNumber.Split('~');
						string []DateArr = CheckDate.Split('~');
						string []AmountArr = CheckAmount.Split('~');

						int indexCheck;
						for(indexCheck = 0; indexCheck < PayeeArr.Length; indexCheck++)
						{
							if(NumArr[indexCheck] == "BLANK")
								continue;

							objTransactionInfo.CUSTOM_INFO += "Payee Name = " + PayeeArr[indexCheck];
							if(indexCheck < NumArr.Length)
								objTransactionInfo.CUSTOM_INFO += ";Check Number = " + NumArr[indexCheck];
							if(indexCheck < DateArr.Length)
								objTransactionInfo.CUSTOM_INFO += ";Check Date = " + DateArr[indexCheck];
							if(indexCheck < AmountArr.Length)
								objTransactionInfo.CUSTOM_INFO += ";Check Amount = " + AmountArr[indexCheck] + ";;";
						}
						//Changes for Itrack #5293
						objTransactionInfo.CUSTOM_INFO += CustomDesc + ";" ; 
					}
				}
				/*else if(strCalledFrom.ToUpper().Equals("PREM_NOTICE"))
				{
					objTransactionInfo.POLICY_ID				=	App_PolicyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	App_PolicyVersionID;
				}*/
			else
				return;
			objTransactionInfo.RECORDED_BY				=	RecordedBy;
			objTransactionInfo.TRANS_DESC				=	TransactionDescription;
			if(TransactionDescription.IndexOf("Additional Interest") >= 0 && AddlInt_tlog != "")
			{
				objTransactionInfo.CUSTOM_INFO = AddlInt_tlog;
				objTransactionInfo.CUSTOM_INFO += ";" + CustomDesc;
			}
			objWrapper.ExecuteNonQuery(objTransactionInfo);
			objWrapper.ClearParameteres();
			
		}

		#endregion
	}
}
