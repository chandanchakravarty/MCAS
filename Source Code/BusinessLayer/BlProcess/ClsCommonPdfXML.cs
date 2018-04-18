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
	/// <summary>
	/// <CreatedBy>Deepak Gupta</CreatedBy>
	/// <Dated>19-June-2006</Dated>
	/// <Purpose>To Common Functions for all Acord PDF xmls</Purpose>
	/// </summary>
	public class ClsCommonPdfXML : ClsCommon
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

		
		public const int BILL_TYPE_MORTGAGEE = 11276;
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

		public string PolicyNumber;
		public string PolicyEffDate;
		public string PolicyExpDate;
        public string PolicyType;      //added by uday on 13-Feb-2008
		public string Reason="";
		public string CopyTo;
		public string ApplicantName;
		public string ApplicantAddress;
		public string ApplicantCityStZip;

		//add By kranti for print check transaction log
		public string PayeeName;
		public string CheckDate;
		public string CheckAmount;
		public string CheckNumber;

		//Added by Mohit Agarwal 9-Jul-2007
		public double notice_amount = 0;
		public double MinimumDue	= 0;
		public string base_due_date = "";

		// Added by Mohit Agarwal 18-Jan-2007
		public string reason_code1;
		public string reason_code2;
		public string reason_code3;
		public string reason_code4;

		public string CustomerAddress;
		public string CustomerCityStZip;
		
		public string AgencyName;
		public string AgencyAddress;
		public string AgencyCitySTZip;
		public string AgencyPhoneNumber;
		public string AgencyCode;
		public string AgencySubCode;
		public string AgencyBilling;

		public XmlDocument AcordPDFXML;
		public XmlDocument CheckPDFXML;
		
		//DataSet declaration for Pdf Xml
		public DataSet DSTempDataSet;
		public DataSet DSTempPolicyDataSet = new DataSet();		
		public DataSet DSTempApplicantDataSet = new DataSet();
		public DataSet DstempDocument = new DataSet();
		public DataSet DSAddWordSet = new DataSet();
		public DataSet DSTempAutoDetailDataSet = new DataSet();
		public DataSet DSTempRateDataSet = new DataSet();
		public DataSet DSTempAdjustDataset = new DataSet();
		public DataSet DSTempOperator = new DataSet();
		public DataSet DSTempUnderWritDataSet = new DataSet();
		public DataSet DSTempPriorLossDataSet = new DataSet();

		private DataWrapper objDataWrapper;

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
		private int addlintindex = -1;
		private int addintdec=1;
		private string addlintPDFName = "";
		public string strPolicyProcessID="";
		public DataWrapper objWrapper;
		#endregion
		
		
		protected string InputBase ; 
		protected string OutputPath ;
		protected string FinalBasePath;

		#region Constructor

		public ClsCommonPdfXML()
		{
//			if(objWrapper==null)
//				objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);
//			// Initialise Input & Out Path to base path based from calling Applcation (CMS/EOD)
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
				InputBase = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()).ToString() + "\\INPUTPDFs\\" ;
                OutputPath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()).ToString() + "\\OUTPUTPDFs\\";
                FinalBasePath = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString() + "/OUTPUTPDFs/";
			}
			//InitailizePdfDataTable();
		}
		public ClsCommonPdfXML(DataWrapper objWraper)
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
                InputBase = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()).ToString() + "\\INPUTPDFs\\";
                OutputPath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()).ToString() + "\\OUTPUTPDFs\\";
                FinalBasePath = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString() + "/OUTPUTPDFs/";
			}
			//InitailizePdfDataTable();
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
		//Initialize DataTable for pdf genration
		public void InitailizePdfDataTable(string strCustomerId, string strAppId, string strAppVersionId, string strCalledFrom, string gStrProcessID)
		{
			// Get policy information 
			objDataWrapper = new DataWrapper(ConnStr,CommandType.Text);
			if(DSTempPolicyDataSet!=null && DSTempPolicyDataSet.Tables.Count<=0 )
				DSTempPolicyDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFPolicyDetails " + strCustomerId + "," + strAppId + "," + strAppVersionId + ",'" + strCalledFrom + "'");

			//Get Applicant information
			if(DSTempApplicantDataSet!=null && DSTempApplicantDataSet.Tables.Count<=0)
				DSTempApplicantDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFApplicantDetails " + strCustomerId + "," + strAppId + "," + strAppVersionId + ",'" + gStrCalledFrom + "'");

			//Get Document Message
			if(DstempDocument!=null && DstempDocument.Tables.Count<=0)
				DstempDocument = objDataWrapper.ExecuteDataSet("Proc_GetPDFDocumentMessage " + "DEC_PAGE" + "");

			//Get Additional Wording
			if(gStrProcessID=="")
			{
				gStrProcessID = GetPolicyProcess( strCustomerId ,strAppId ,strAppVersionId , gStrCalledFrom);
			}
			if(gStrProcessID!="")
			{
				if(DSAddWordSet!=null && DSAddWordSet.Tables.Count<=0)
					DSAddWordSet = objDataWrapper.ExecuteDataSet("Proc_GetAddWordingsData " +  "0," +  "0," + gStrProcessID);
			}
			//Get Auto Details
			if(DSTempAutoDetailDataSet!=null && DSTempAutoDetailDataSet.Tables.Count<=0)
				DSTempAutoDetailDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFAuto_Details " + strCustomerId + "," + strAppId + "," + strAppVersionId + ",'" + gStrCalledFrom + "'");			

			//Get rate XML
			if(DSTempRateDataSet!=null && DSTempRateDataSet.Tables.Count<=0)
				DSTempRateDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFQuoteRateXML " + strCustomerId + "," + strAppId + "," + strAppVersionId + ",'" + gStrCalledFrom + "','" + "AUTOP" + "'");

			//Get Adjusted Prem
			//DSTempAdjustDataset = new DataSet();
			//if(DSTempAdjustDataset==null)
			//	DSTempAdjustDataset = objDataWrapper.ExecuteDataSet("PROC_GetpdfAdjusted_Premium " + gStrClientID + "," + gStrPolicyId + "," + newVersion +"");			

			// Get Prior Loss Information
			if(DSTempPriorLossDataSet!=null && DSTempPriorLossDataSet.Tables.Count<=0)
				DSTempPriorLossDataSet = objDataWrapper.ExecuteDataSet("Proc_GetPDFPriorPolicyAndLossDetails " + strCustomerId + ",'AUTOP','POLICY'");

			//Get Underwriting details
			if(DSTempUnderWritDataSet!=null && DSTempUnderWritDataSet.Tables.Count<=0)
				DSTempUnderWritDataSet =  objDataWrapper.ExecuteDataSet("Proc_GetPDFAutoUnderwritingDetails " + strCustomerId + "," + strAppId + "," + strAppVersionId + ",'" + gStrCalledFrom + "'");
				
		}
		//Added by Mohit Agarwal 17-Apr-2007
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
		//Added by asfa (20-Feb-2008) - iTrack issue #3331
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
		public string GetPolicyStatus(string StrCalledFrom,int StrPolicyId,int StrPolicyVersion,int StrClientID, DataWrapper ObjRraper)
		{
			DataSet DSTempDataSet = new DataSet();
			string stStatus="";
			ObjRraper.ClearParameteres();
			ObjRraper.AddParameter("@Customer_Id",StrClientID);
			ObjRraper.AddParameter("@AppPol_Id",StrPolicyId);
			ObjRraper.AddParameter("@AppPolVersion_Id",StrPolicyVersion);
			ObjRraper.AddParameter("@CalledFrom",StrCalledFrom);
			DSTempDataSet = ObjRraper.ExecuteDataSet("Proc_GetPDFStateCode");
			ObjRraper.ClearParameteres();
			//DSTempDataSet = new DataWrapper(ConnStr,CommandType.Text).ExecuteDataSet("Proc_GetPDFStateCode " + StrClientID + "," + StrPolicyId + "," + StrPolicyVersion + ",'" + StrCalledFrom + "'" );
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
		#endregion

		#region Common Rate,Discount,Surcharges,Premium Functions
		#region Loading Rate XML
		//		public void LoadRateXML()
		//		{
		//			DataSet DSTempDataSet = new DataSet();
		//			DSTempDataSet = new DataWrapper(ConnStr,CommandType.Text).ExecuteDataSet("Proc_GetPDFQuoteRateXML " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "'");
		//			if (DSTempDataSet.Tables[0].Rows.Count>0)
		//			{
		//				if (DSTempDataSet.Tables[0].Rows[DSTempDataSet.Tables[0].Rows.Count-1]["QUOTE_XML"].ToString().Trim() !="")
		//				{
		//					RateXmlDocument.LoadXml(DSTempDataSet.Tables[0].Rows[DSTempDataSet.Tables[0].Rows.Count-1]["QUOTE_XML"].ToString().Trim().Replace("<?xml-stylesheet type='text/xsl' href='FinalXML.xsl'?><!DOCTYPE people [<!ATTLIST homepage xlink:type CDATA #FIXED 'simple' xmlns:xlink CDATA #FIXED 'http://www.w3.org/1999/xlink'> <!ATTLIST person id ID #IMPLIED>]>",""));
		//					isRateGenerated = true;
		//				}
		//			}
		//		}

		public void LoadRateXML(string strLOB)
		{
			try


			{
				DataSet DSTempDataSet = new DataSet();
				DSTempDataSet = new DataWrapper(ConnStr,CommandType.Text).ExecuteDataSet("Proc_GetPDFQuoteRateXML " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "','" + strLOB + "'");
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
		public void LoadRateXML(string strLOB,DataWrapper objDataWrapper)
		{
			try


			{
				DataSet DSTempDataSet = new DataSet();
				objDataWrapper.AddParameter("@CUSTOMERID",gStrClientID);	
				objDataWrapper.AddParameter("@POLID",gStrPolicyId);	
				objDataWrapper.AddParameter("@VERSIONID",gStrPolicyVersion);	
				objDataWrapper.AddParameter("@CALLEDFROM",gStrCalledFrom);	
				objDataWrapper.AddParameter("@LOB",strLOB);	
				DSTempDataSet=	objDataWrapper.ExecuteDataSet("Proc_GetPDFQuoteRateXML");
				objDataWrapper.ClearParameteres();
				//DSTempDataSet=	objDataWrapper.ExecuteDataSet("Proc_GetPDFQuoteRateXML " + gStrClientID + "," + gStrPolicyId + "," + gStrPolicyVersion + ",'" + gStrCalledFrom + "','" + strLOB + "'");
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
		#endregion

		public XmlNodeList GetTransfer_Renewal(string EntityID)
		{
			// Transfer Renewal Discount
			return (RateXmlDocument.SelectNodes("//RISK[@ID='" + EntityID + "']/STEP[@COMPONENT_CODE='D_XFR' and @STEPPREMIUM!='0']"));	
		}

		//Added by Mohit Agarwal 26-Sep-2007
		public XmlNodeList GetRentalTerritory()
		{
			return( RateXmlDocument.SelectNodes("//STEP[@TS='0']"));
				//PRODUCTDESC DP-2 - REPLACE (Territory :
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

		//Added by: Mohit Agarwal 25-Jan-2007
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

		public static void InsertPremiumNoticeProccess(string strCustomerId,string strAppId, string strAppVersionId, string strproccessInfo,DataWrapper objDataWrapper)
		{
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",strCustomerId);
				objDataWrapper.AddParameter("@POLICY_ID",strAppId);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",strAppVersionId);
				objDataWrapper.AddParameter("@PROCCESS_INFORMATION",strproccessInfo);
				objDataWrapper.ExecuteNonQuery("PROC_GETPREMIUMNOTICEPROCCESS_INFO");
				objDataWrapper.ClearParameteres();
			}
			
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{}

		}

		#region GeneratePDFProxy method to generate PDF
		public string GeneratePdfProxy(string strCustomerId, string strAppId, string strAppVersionId, string strCalledFrom,string strCalledForPDF, string strLOB, string getRequeststring,ref string SystemID, string getRequestPrintChk, string CHECK_ID, string temp)
		{
			string PDFName="";
			PDFName = GeneratePdfProxy(strCustomerId, strAppId, strAppVersionId, strCalledFrom, strCalledForPDF, strLOB, "", getRequeststring, ref SystemID, getRequestPrintChk, CHECK_ID, temp);
			return PDFName;
		}
  

		public string GeneratePdfProxy(string strCustomerId, string strAppId, string strAppVersionId, string strCalledFrom,string strCalledForPDF, string strLOB, string strProcessID, string getRequeststring,ref string SystemID, string getRequestPrintChk, string CHECK_ID, string temp)
		{
			try
			{
				string strAcordPDFXml="",PDFName="",strCheck="CHK",strCheckPDFXml="",stateCode="",strtempproc="",
					strInputPath,strOutputPath,strInputXml,
					strInputBase,strFinalBasePath,strAlternateInputPath;
				// new data wrapper to store premium notice proccess information
				StringBuilder returnString= new StringBuilder(); 
				//DataWrapper objDataWrapper;
				if(objWrapper==null)
                    objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.ON);	
//				// Initalize dataset
				//InitailizePdfDataTable(strCustomerId,strAppId,strAppVersionId,strCalledFrom,gStrProcessID);
				string check_nums = "";
				string PdfNoWordingFileName="";
				if(strCalledForPDF == "DECPAGE" && getRequestPrintChk == "" && CHECK_ID == "CUSTOMER")
				{
					PdfNoWordingFileName=GeneratePdfProxy(strCustomerId, strAppId, strAppVersionId, strCalledFrom, strCalledForPDF, strLOB, strProcessID, getRequeststring, ref SystemID, getRequestPrintChk, "CUSTOMER-NOWORD", temp);
					ClsPolicyProcess.NoWordingPDFFileName=PdfNoWordingFileName;
				}
				// Initialise Input & Out Path to base path based from calling Applcation (CMS/EOD)
				if(IsEODProcess)
				{
					//				strInputBase = WebAppUNCPath +  "\\CmsWeb\\INPUTPDFs\\";
					strInputBase = UploadPath + "\\INPUTPDFs\\";
					strInputBase=  System.IO.Path.GetFullPath(strInputBase);
					strOutputPath = UploadPath + "\\OUTPUTPDFs\\";
					strOutputPath = System.IO.Path.GetFullPath(strOutputPath);
					strFinalBasePath = UploadURL + "/OUTPUTPDFs/";
				
					// store input and out path information
					returnString.Append("InputBase Path=");
					returnString.Append(strInputBase);
					returnString.Append("OutputPath =");
					returnString.Append(strOutputPath);
					returnString.Append("FinalBase Path =");
					returnString.Append(strFinalBasePath);
					strtempproc = returnString.ToString();
								
				}
				else
				{
					//				strInputBase = System.Web.HttpContext.Current.Request.PhysicalApplicationPath.ToString() + "CmsWeb\\INPUTPDFs\\" ;
					strInputBase = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()).ToString() + "\\INPUTPDFs\\" ;
					strOutputPath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()).ToString() + "\\OUTPUTPDFs\\" ;
					strFinalBasePath = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString() + "/OUTPUTPDFs/";
					// store input and out path information
					returnString.Append("InputBase Path=");
					returnString.Append(strInputBase);
					returnString.Append("OutputPath =");
					returnString.Append(strOutputPath);
					returnString.Append("FinalBase Path =");
					returnString.Append(strFinalBasePath);
					strtempproc = returnString.ToString();
				}
				// store input and out path information
				//InsertPremiumNoticeProccess(strCustomerId, strAppId, strAppVersionId,strtempproc,objDataWrapper);
				string agency_code = "";
				string canc_type = "";
				string canc_type_act = "";
				string AddlInt_tlog = "";
				string blank_num = "";
				string custRecp_custInfo = "";

				string veh_id="0",addint_id="0";
				string Addilname="";
				canc_type = SystemID;
				canc_type_act = SystemID;

				if(canc_type == "COMPANY" || canc_type == "AGENTS")
					canc_type = "";
				else if(canc_type == "NSF_NOREPLACE")
					canc_type = "NSF";

				if(canc_type == "NREN_NOTICE_NOTIFICATION" || canc_type == "NREN_NOTICE_NO_NOTIFICATION")
					canc_type = "AGN_TERM_NREN";
				else if(getRequestPrintChk.Equals("NREN_NOTICE"))
					canc_type = "";

				base_due_date = "";

				if(temp == null || temp == "")
					temp = "temp";
			
			
				if(getRequestPrintChk.Equals("CHECKPDFPRINT"))
				{
					if(CHECK_ID != null && CHECK_ID != "" && CHECK_ID != "Insured")
					{
						Cms.BusinessLayer.BlProcess.ClsCheckPdfXml objCheckPdfXML = new ClsCheckPdfXml(CHECK_ID);

						strCheckPDFXml = objCheckPdfXML.getCheckPDFXml(ref blank_num, ref check_nums);
						if(blank_num == "BLANK_NUM")
							return "BLANK_NUM";
						PayeeName = objCheckPdfXML.PayeeName;
						CheckDate = objCheckPdfXML.CheckDate;
						CheckAmount = objCheckPdfXML.CheckAmount;
						CheckNumber = objCheckPdfXML.CheckNumber ;
					}
					else if(strCustomerId != null && strCustomerId != "")
					{
						base_due_date = getRequeststring;
						//					if(base_due_date.IndexOf("/") < 0)
						//						base_due_date = "";
						try
						{
							base_due_date = Convert.ToDateTime(getRequeststring).ToString();
						}
						catch(Exception)
						{
							base_due_date = "";
						}

						try{ strLOB=new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().GetPolicyLob("POLICY",int.Parse(strAppId),int.Parse(strAppVersionId),int.Parse(strCustomerId)); }
						catch(Exception){}
						Cms.BusinessLayer.BlProcess.ClsPremNotPdfXML objPremNotPdfXML = new ClsPremNotPdfXML(strCustomerId,strAppId,strAppVersionId,strLOB,base_due_date,"INSURED",strCalledForPDF,-1);

						string bill_plan = "";
						if(IsEODProcess)
							strCheckPDFXml = objPremNotPdfXML.getPremNotPDFXml("EOD", ref bill_plan);
						else
							strCheckPDFXml = objPremNotPdfXML.getPremNotPDFXml("Virtual", ref bill_plan);
						// store input and out path information
						//InsertPremiumNoticeProccess(strCustomerId, strAppId, strAppVersionId,strCheckPDFXml,objDataWrapper);
						notice_amount = objPremNotPdfXML.notice_amount;
						MinimumDue    = objPremNotPdfXML.MinimumDue ;
						base_due_date = objPremNotPdfXML.due_date;
						try
						{
							//if(IsEODProcess && bill_plan.IndexOf("Mortgagee Bill from Inception") >= 0)
							if(IsEODProcess && bill_plan == BILL_TYPE_MORTGAGEE.ToString() && getRequestPrintChk != "ADDLINT")
								GeneratePdfProxy(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF, strLOB, getRequeststring, ref SystemID, "ADDLINT", "PREM_NOTICE", temp);
						}
						catch(Exception ex)
						{
							System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
							addInfo.Add("Err Descriptor ","Error while generating PDF.");
							addInfo.Add("CustomerID" ,strCustomerId);
							addInfo.Add("PolicyID",strAppId);
							addInfo.Add("PolicyVersionID",strAppVersionId);
							addInfo.Add("PolicyVersionID",strCalledFrom);
							addInfo.Add("PolicyVersionID",strCalledForPDF);
							addInfo.Add("ProcessID",gStrProcessID);
							ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex,addInfo );

							throw(new Exception("Error while generating PDF.",ex));

						}
					}
				}
				else if(getRequestPrintChk.Equals("CANC_NOTICE"))
				{
					stateCode=new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().SetStateCode(strCalledFrom,int.Parse(strAppId),int.Parse(strAppVersionId),int.Parse(strCustomerId)); 
					//				if(strCalledForPDF == "DECPAGE")
					Cms.BusinessLayer.BlProcess.ClsCancNoticePdfXML objCancNoticePdfXML = new ClsCancNoticePdfXML(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,strLOB,stateCode,CHECK_ID, canc_type);
				
					if(canc_type == "NONPAYDB")
						strAcordPDFXml = objCancNoticePdfXML.getCancNotNonPayDBPDFXml();
					else
						strAcordPDFXml = objCancNoticePdfXML.getCancNotPDFXml();

					notice_amount = objCancNoticePdfXML.notice_amount;
					MinimumDue    = objCancNoticePdfXML.MinimumDue ;
					base_due_date= objCancNoticePdfXML.base_due_date;
                    //Praveen Xml Generated(07/09/2010)
				}
				else if(getRequestPrintChk.Equals("CERT_MAIL"))
				{
					stateCode=new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().SetStateCode(strCalledFrom,int.Parse(strAppId),int.Parse(strAppVersionId),int.Parse(strCustomerId)); 
					//				if(strCalledForPDF == "CLIENT")
					strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsCancNoticePdfXML(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,strLOB,stateCode,CHECK_ID, canc_type).getCertMailPDFXml();
				}
				else if(getRequestPrintChk.Equals("NREN_NOTICE"))
				{
					stateCode=new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().SetStateCode(strCalledFrom,int.Parse(strAppId),int.Parse(strAppVersionId),int.Parse(strCustomerId)); 
					Cms.BusinessLayer.BlProcess.ClsNRenNoticePdfXML objNRenNoticePdfXML = new ClsNRenNoticePdfXML(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,strLOB,stateCode,CHECK_ID);
					//				if(strCalledForPDF == "DECPAGE")
					if(canc_type == "")
						strAcordPDFXml = objNRenNoticePdfXML.getNRenNotPDFXml();
					else
						strAcordPDFXml = objNRenNoticePdfXML.getNRenATrmPDFXml();
					base_due_date= objNRenNoticePdfXML.base_due_date;

				}
				else if(getRequestPrintChk.Equals("REINS_NOTICE"))
				{
					stateCode=new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().SetStateCode(strCalledFrom,int.Parse(strAppId),int.Parse(strAppVersionId),int.Parse(strCustomerId)); 
					//				if(strCalledForPDF == "DECPAGE")
					Cms.BusinessLayer.BlProcess.ClsReinsNoticePdfXML objReinsNoticePdfXML = new ClsReinsNoticePdfXML(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,strLOB,stateCode,CHECK_ID);
					strAcordPDFXml = objReinsNoticePdfXML.getReinsNotPDFXml();
					base_due_date= objReinsNoticePdfXML.base_due_date;
				}
				else if(getRequestPrintChk.Equals("UMB_LETTER"))
				{
					strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsUmbLetterPdfXML(strCustomerId, strAppId, strAppVersionId).getUmbLetterPdfXML();
				}
				else if(getRequestPrintChk.Equals("AUTO_ID_CARD"))
				{
					if(strLOB != "PPA" && strLOB != "MOT")
						return "";
					stateCode=new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().SetStateCode(strCalledFrom,int.Parse(strAppId),int.Parse(strAppVersionId),int.Parse(strCustomerId),objWrapper); 
					if(strLOB == "MOT")
						strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsAutoIdPdfXML(strCustomerId, strAppId, strAppVersionId, strCalledFrom,stateCode,strLOB,objWrapper).getAutoIdPDFXml();
					if(strAcordPDFXml=="")
						return "";
				}
				else if(getRequestPrintChk.Equals("CUST_RECEIPT"))
				{
					strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsCustAgencyPaymentsPdfXML(CHECK_ID, strCalledFrom).getCustAgencyPaymentsPDFXml(ref custRecp_custInfo);
				}
				else if(getRequestPrintChk.Equals("ADDLINT"))
				{
					//strCalledFrom = "Application";

					stateCode=new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().SetStateCode(strCalledFrom,int.Parse(strAppId),int.Parse(strAppVersionId),int.Parse(strCustomerId),objWrapper); 
					if(CHECK_ID == "PREM_NOTICE")
					{
						strLOB=new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().GetPolicyLob(strCalledFrom,int.Parse(strAppId),int.Parse(strAppVersionId),int.Parse(strCustomerId),objWrapper); 
						strCalledFrom = "POLICY";
					}
					#region Add Int iteration
					try
					{
						DataWrapper gobjSqlHelper;
						gobjSqlHelper = new DataWrapper(ConnStr,CommandType.Text);
						DataSet DSTempDwellinAdd=null;
						if(strLOB == "HOME" || strLOB == "RENT")
						{
							objWrapper.ClearParameteres();
							objWrapper.AddParameter("@CUSTOMERID",strCustomerId);
							objWrapper.AddParameter("@POLID",strAppId);
							objWrapper.AddParameter("@VERSIONID",strAppVersionId);
							objWrapper.AddParameter("@DWELLINGID",0);
							objWrapper.AddParameter("@CALLEDFROM",strCalledFrom);
							DSTempDwellinAdd = objWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_Additional_Interest");
							objWrapper.ClearParameteres();
							//DSTempDwellinAdd = gobjSqlHelper.ExecuteDataSet("Proc_GetPDFHomeowner_Additional_Interest " + strCustomerId + "," + strAppId + "," + strAppVersionId  + ",0,'" + strCalledFrom + "'");
						}
						else if(strLOB == "MOT" || strLOB == "PPA")
						{
							objWrapper.ClearParameteres();
							objWrapper.AddParameter("@CUSTOMERID",strCustomerId);
							objWrapper.AddParameter("@POLID",strAppId);
							objWrapper.AddParameter("@VERSIONID",strAppVersionId);
							objWrapper.AddParameter("@VEHICLEID",0);
							objWrapper.AddParameter("@CALLEDFROM",strCalledFrom);
							DSTempDwellinAdd = objWrapper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS");
							objWrapper.ClearParameteres();
							//DSTempDwellinAdd = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS " + strCustomerId + "," + strAppId + "," + strAppVersionId  + ",0,'" + strCalledFrom + "'");
						}
						else if(strLOB == "WAT" )
						{
							objWrapper.ClearParameteres();
							objWrapper.AddParameter("@CUSTOMERID",strCustomerId);
							objWrapper.AddParameter("@POLID",strAppId);
							objWrapper.AddParameter("@VERSIONID",strAppVersionId);
							objWrapper.AddParameter("@BOATID",0);
							objWrapper.AddParameter("@CALLEDFROM",strCalledFrom);
							DSTempDwellinAdd = objWrapper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS");
							objWrapper.ClearParameteres();
							//DSTempDwellinAdd = gobjSqlHelper.ExecuteDataSet("PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS " + strCustomerId + "," + strAppId + "," + strAppVersionId  + ",0,'" + strCalledFrom + "'");
						}
						addlintindex++;
						addintdec++;
						#endregion 

						if(DSTempDwellinAdd==null || DSTempDwellinAdd.Tables[0].Rows.Count == 0)
							return "";

						if(DSTempDwellinAdd!=null && addlintindex < DSTempDwellinAdd.Tables[0].Rows.Count)
						{
							if(strLOB == "HOME" || strLOB == "RENT")
							{
								veh_id = DSTempDwellinAdd.Tables[0].Rows[addlintindex]["DWELLING_ID"].ToString();
								string holder_name = DSTempDwellinAdd.Tables[0].Rows[addlintindex]["NAME_ADDRESS"].ToString();
								holder_name = holder_name.Substring(0, holder_name.IndexOf(","));
								AddlInt_tlog = ";Holder Name: " + holder_name;
							}
							else if(strLOB == "MOT" || strLOB == "PPA")
							{
								veh_id = DSTempDwellinAdd.Tables[0].Rows[addlintindex]["VEHICLE_ID"].ToString();
								AddlInt_tlog = ";Holder Name: " + DSTempDwellinAdd.Tables[0].Rows[addlintindex]["HOLDER_NAME"].ToString();
							}
							else if(strLOB == "WAT")
							{
								veh_id = DSTempDwellinAdd.Tables[0].Rows[addlintindex]["BOAT_ID"].ToString();
								AddlInt_tlog = ";Holder Name: " + DSTempDwellinAdd.Tables[0].Rows[addlintindex]["HOLDER_NAME"].ToString();
							}
							addint_id = DSTempDwellinAdd.Tables[0].Rows[addlintindex]["ADD_INT_ID"].ToString();
							Addilname=DSTempDwellinAdd.Tables[0].Rows[addlintindex]["ADD_INT_NAME"].ToString();//addint_id=addlintindex.ToString();
							if(CHECK_ID == "PREM_NOTICE")
							{
								try
								{
									base_due_date = Convert.ToDateTime(getRequeststring).ToString();
								}
								catch(Exception)
								{
									base_due_date = "";
								}
								// store due date
								//InsertPremiumNoticeProccess(strCustomerId, strAppId, strAppVersionId,base_due_date,objDataWrapper);
								string bill_plan="";
								Cms.BusinessLayer.BlProcess.ClsPremNotPdfXML objPremNotPdfXML = new ClsPremNotPdfXML(strCustomerId,strAppId,strAppVersionId,strLOB,base_due_date,"ADDL_INT",strCalledFrom, addlintindex);
								if(IsEODProcess)
									strCheckPDFXml = objPremNotPdfXML.getPremNotPDFXml("EOD", ref bill_plan);
								else
									strCheckPDFXml = objPremNotPdfXML.getPremNotPDFXml("Virtual", ref bill_plan);
								// store Check pdf xml
								//InsertPremiumNoticeProccess(strCustomerId, strAppId, strAppVersionId,strCheckPDFXml,objDataWrapper);
								notice_amount = objPremNotPdfXML.notice_amount;
								MinimumDue    = objPremNotPdfXML.MinimumDue; 
								base_due_date = objPremNotPdfXML.due_date;
							}
							else if(strCalledForPDF == "DECPAGE")
								strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsAddIntPdfXML(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,strLOB,stateCode,strProcessID,addlintindex,objWrapper).getAddIntPDFXml();
							else if(strCalledForPDF == "CANC_NOTICE")
							{
								if(canc_type == "NONPAYDB")
									strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsCancNoticePdfXML(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,strLOB,stateCode,CHECK_ID, canc_type ).getCancNotAINonPayDbPDFXml(addlintindex);
								else
									strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsCancNoticePdfXML(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,strLOB,stateCode,CHECK_ID, canc_type).getCancNotAIPDFXml(addlintindex);
							}
							else if(strCalledForPDF == "NREN_NOTICE")
								strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsNRenNoticePdfXML(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,strLOB,stateCode,CHECK_ID).getNRenNotAIPDFXml(addlintindex);
							else if(strCalledForPDF == "REINS_NOTICE")
								strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsReinsNoticePdfXML(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,strLOB,stateCode,CHECK_ID).getReinsNotAIPDFXml(addlintindex);
							else if(strCalledForPDF == "CERT_MAIL")
								strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsCancNoticePdfXML(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,strLOB,stateCode,CHECK_ID, canc_type).getCertMailAIPDFXml(addlintindex);
						}

						if(DSTempDwellinAdd!=null && addlintindex < DSTempDwellinAdd.Tables[0].Rows.Count-1)
						{
							//if((canc_type == "NONPAYDB") || (canc_type == "NONPAYDBMEMO"))
							agency_code = canc_type;
							addlintPDFName += GeneratePdfProxy(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF, strLOB, strProcessID, getRequeststring, ref agency_code, getRequestPrintChk, CHECK_ID, temp);
							addlintPDFName += "~";
						}
						else if(strLOB == "HOME")
						{
							addlintindex = -1;
							try
							{
								addlintPDFName += GeneratePdfProxy(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF, "WAT", strProcessID, getRequeststring, ref agency_code, getRequestPrintChk, "HOME", temp);
								addlintPDFName += "~";
							}
							catch (Exception ex)
							{
                                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
							}
						}
					}
					catch(Exception ex)
					{
						System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
						addInfo.Add("Err Descriptor ","Error while generating PDF.");
						addInfo.Add("CustomerID" ,strCustomerId);
						addInfo.Add("PolicyID",strAppId);
						addInfo.Add("PolicyVersionID",strAppVersionId);
						addInfo.Add("ProcessID",strProcessID);
						addInfo.Add("CalledFrom",strCalledFrom);
						addInfo.Add("CalledForPDF",strCalledForPDF);						
						ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex,addInfo );
						//return "";
						throw(new Exception("Error while generating PDF.",ex));
					}
				}
				else
				{
					try
					{
						stateCode=new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().SetStateCode(strCalledFrom,int.Parse(strAppId),int.Parse(strAppVersionId),int.Parse(strCustomerId),objWrapper); 
						switch(strLOB.ToUpper())
						{
							case "WAT":
								strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsBoatPdfXML(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,stateCode,strProcessID,CHECK_ID,temp,objWrapper).getBoatAcordPDFXml();
								break;
							case "HOME":
								//strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsHomePdfXML(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,stateCode,strProcessID,CHECK_ID,temp).getHomeAcordPDFXml();
								break;
							case "RENT":
								strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsRedwPdfXML(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,stateCode,strProcessID,CHECK_ID,temp,objWrapper).getRentalAcordPDFXml();
								break;
							case "PPA":
								//strAcordPDFXml = new Cms.BusinessLayer.BlProcess.ClsAutoPDFXML(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,stateCode,strProcessID,CHECK_ID,temp,objWrapper).getAutoAcordPDFXml();
								break;
							case "MOT":
								strAcordPDFXml = new Cms.BusinessLayer.BlProcess.clsMotorCyclePdfXml(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,stateCode,strProcessID,CHECK_ID,temp,objWrapper).getMotorCycleAcordPDFXml();
								break;
							case "UMB":
								strAcordPDFXml = new Cms.BusinessLayer.BlProcess.clsUmbrellaPdfXml(strCustomerId, strAppId, strAppVersionId, strCalledFrom,strCalledForPDF,stateCode,strProcessID,CHECK_ID).getUmbrellaAcordPDFXml();
								break;
                                //Added By Chetna to save XML for aviation(XML is not generated at this time.
                                //Start
                            case "AVIATION":
                                AcordPDFXML = new XmlDocument();
                                AcordPDFXML.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?> <" + RootElement + "/>");
                                strAcordPDFXml = AcordPDFXML.OuterXml;
                                InsertAviationXml(strCustomerId, strAppId, strAppVersionId, CHECK_ID, strAcordPDFXml);
                                break;
                                //End
						}
					}
					catch(Exception ex)
					{
						System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
						addInfo.Add("Err Descriptor ","Error while generating PDF.");
						addInfo.Add("CustomerID" ,strCustomerId);
						addInfo.Add("PolicyID",strAppId);
						addInfo.Add("PolicyVersionID",strAppVersionId);
						addInfo.Add("CalledFrom",strCalledFrom);
						addInfo.Add("CalledForPDF",strCalledForPDF);
						addInfo.Add("ProcessID",gStrProcessID);
						ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex,addInfo );
						//return "";
						throw(new Exception("Error while generating PDF.",ex));
					}
				}
				AcordPDF.AcordXMLParser objEbixAcordPDF = new AcordPDF.AcordXMLParser();
				if(strCustomerId!=null && strCustomerId!="")
					objEbixAcordPDF.ClientId = int.Parse(strCustomerId);
				if(strAppId!=null && strAppId!="")
					objEbixAcordPDF.PolicyId = int.Parse(strAppId);
				if(strAppVersionId!=null && strAppVersionId!="")
					objEbixAcordPDF.PolicyVersion = int.Parse(strAppVersionId);
				if(veh_id!="")
					objEbixAcordPDF.VehicleDwellingBoat = int.Parse(veh_id);
				if(addint_id!="")
					objEbixAcordPDF.AddInt = int.Parse(addint_id);

				if(getRequestPrintChk.Equals("CHECKPDFPRINT"))
				{
					if(CHECK_ID != null && CHECK_ID != "" && CHECK_ID != "Insured")
						objEbixAcordPDF.LobCode = strCheck;
					else if(strCustomerId != null && strCustomerId != "")
						objEbixAcordPDF.LobCode = "PREM";
				}
				else if(getRequestPrintChk.Equals("CANC_NOTICE"))
				{
					stateCode=new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().SetStateCode(strCalledFrom,int.Parse(strAppId),int.Parse(strAppVersionId),int.Parse(strCustomerId)); 
					ClsPolicyProcess objPolicyProcess = new ClsPolicyProcess();
					objPolicyProcess.BeginTransaction();
					if(canc_type == "")
						objEbixAcordPDF.LobCode = objPolicyProcess.GetCancellationCode(strLOB,stateCode,CHECK_ID,"CANC");
					else
						objEbixAcordPDF.LobCode = objPolicyProcess.GetCancellationCode("ALL","ALL",CHECK_ID,canc_type);
					objPolicyProcess.CommitTransaction();
				}
				else if(getRequestPrintChk.Equals("CERT_MAIL"))
				{
					stateCode=new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().SetStateCode(strCalledFrom,int.Parse(strAppId),int.Parse(strAppVersionId),int.Parse(strCustomerId)); 
					ClsPolicyProcess objPolicyProcess = new ClsPolicyProcess();
					objPolicyProcess.BeginTransaction();
					objEbixAcordPDF.LobCode = objPolicyProcess.GetCancellationCode("ALL","ALL",CHECK_ID,"CERT_MAIL");
					objPolicyProcess.CommitTransaction();
				}
				else if(getRequestPrintChk.Equals("NREN_NOTICE"))
				{
					stateCode=new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().SetStateCode(strCalledFrom,int.Parse(strAppId),int.Parse(strAppVersionId),int.Parse(strCustomerId)); 
					ClsPolicyProcess objPolicyProcess = new ClsPolicyProcess();
					objPolicyProcess.BeginTransaction();
					if(canc_type == "")
						objEbixAcordPDF.LobCode = objPolicyProcess.GetCancellationCode(strLOB,stateCode,CHECK_ID,"NREN");
					else
					{
						if(stateCode == "MI")
						{
							if(strLOB ==  "HOME" || strLOB == "RENT")
								objEbixAcordPDF.LobCode = objPolicyProcess.GetCancellationCode(strLOB,stateCode,CHECK_ID,canc_type);
							else if(strLOB ==  "WAT")
								objEbixAcordPDF.LobCode = objPolicyProcess.GetCancellationCode(strLOB,stateCode,CHECK_ID,canc_type);
							else
								objEbixAcordPDF.LobCode = objPolicyProcess.GetCancellationCode("ALL",stateCode,CHECK_ID,canc_type);
						}
							//if(stateCode == "IN")
						else
						{

							stateCode = "IN";
							if(strLOB ==  "HOME" || strLOB == "RENT" || strLOB == "WAT")
								objEbixAcordPDF.LobCode = objPolicyProcess.GetCancellationCode("ALL",stateCode,CHECK_ID,canc_type);
							else
							{
								if(stateCode=="IN" && canc_type=="AGN_TERM_NREN")
								{
									objEbixAcordPDF.LobCode = objPolicyProcess.GetCancellationCode("ALL",stateCode,CHECK_ID,canc_type);
								}
								else
								{
									objEbixAcordPDF.LobCode = objPolicyProcess.GetCancellationCode(strLOB,stateCode,CHECK_ID,canc_type);
								}
							}
						}
					}
					objPolicyProcess.CommitTransaction();
				}
				else if(getRequestPrintChk.Equals("REINS_NOTICE"))
				{
					//stateCode=new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().SetStateCode(strCalledFrom,int.Parse(strAppId),int.Parse(strAppVersionId),int.Parse(strCustomerId)); 
					ClsPolicyProcess objPolicyProcess = new ClsPolicyProcess();
					objPolicyProcess.BeginTransaction();
					objEbixAcordPDF.LobCode = objPolicyProcess.GetCancellationCode("ALL","ALL",CHECK_ID,"REINS");
					objPolicyProcess.CommitTransaction();
				}
				else if(getRequestPrintChk.Equals("CUST_RECEIPT"))
				{
					objEbixAcordPDF.LobCode = "RECEIPT";
				}
				else if(getRequestPrintChk.Equals("UMB_LETTER"))
				{
					//stateCode=new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().SetStateCode(strCalledFrom,int.Parse(strAppId),int.Parse(strAppVersionId),int.Parse(strCustomerId)); 
					ClsPolicyProcess objPolicyProcess = new ClsPolicyProcess();
					objPolicyProcess.BeginTransaction();
					objEbixAcordPDF.LobCode = objPolicyProcess.GetCancellationCode("UMB","ALL","ALL","UMB_LETTER");
					objPolicyProcess.CommitTransaction();
				}
				else if(getRequestPrintChk.Equals("ADDLINT"))
				{
					ClsPolicyProcess objPolicyProcess = new ClsPolicyProcess();
					objPolicyProcess.BeginTransaction();
					if(CHECK_ID == "PREM_NOTICE")
						objEbixAcordPDF.LobCode = "PREM";
					else if(strCalledForPDF == "DECPAGE")
					{
						if(CHECK_ID == "HOME")
							objEbixAcordPDF.LobCode = "HOME" + "_ADDLINT" + "_" + Addilname;
						else
							objEbixAcordPDF.LobCode = strLOB + "_ADDLINT_" + Addilname;
					}
					else if((strCalledForPDF == "CANC_NOTICE") && (canc_type != "INSREQ"))
					{
						if(canc_type == "NONPAYDB")
							objEbixAcordPDF.LobCode = objPolicyProcess.GetCancellationCode("ALL","ALL",CHECK_ID,"NONPAYDB") + "_" + Addilname;
						else if(canc_type == "NONPAYDBMEMO")
							objEbixAcordPDF.LobCode = objPolicyProcess.GetCancellationCode("ALL","ALL",CHECK_ID,"NONPAYDBMEMO") + "_" + Addilname;
						else
							objEbixAcordPDF.LobCode = objPolicyProcess.GetCancellationCode("ALL","ALL",CHECK_ID,"CANC") + "_" + Addilname;
					}
					else if((strCalledForPDF == "CANC_NOTICE") && (canc_type == "INSREQ"))
						objEbixAcordPDF.LobCode = objPolicyProcess.GetCancellationCode("ALL","ALL",CHECK_ID,canc_type) + "_" + Addilname;
					else if(strCalledForPDF == "NREN_NOTICE")
						objEbixAcordPDF.LobCode = objPolicyProcess.GetCancellationCode("ALL","ALL",CHECK_ID,"NREN") + "_" + Addilname;
					else if(strCalledForPDF == "REINS_NOTICE")
						objEbixAcordPDF.LobCode = objPolicyProcess.GetCancellationCode("ALL","ALL",CHECK_ID,"REINS") + "_" + Addilname;
					else if(strCalledForPDF == "CERT_MAIL")
						objEbixAcordPDF.LobCode = objPolicyProcess.GetCancellationCode("ALL","ALL",CHECK_ID,"CERT_MAIL")+ "_" + Addilname;
					objPolicyProcess.CommitTransaction();
				}
				else if(getRequestPrintChk.Equals("AUTO_ID_CARD"))
				{
					objEbixAcordPDF.LobCode = strLOB + "_AUTO_ID_CARD";
				}
				else 
				{
					if(getRequeststring.Equals("UNDERLAYING_POL"))
					{
						//objEbixAcordPDF.LobCode = gstrLOBCODE.ToString();
						objEbixAcordPDF.LobCode = strLOB;
					

					}
					else 
					{
						objEbixAcordPDF.LobCode = strLOB;//GetLOBString();
					}
					if(strCalledForPDF == PDFForDecPage)
					{
						if(CHECK_ID == "CUSTOMER-NOWORD")
							objEbixAcordPDF.LobCode += "_DEC_PAGE_NOWORD";
						else if(CHECK_ID == "CUSTOMER")
							objEbixAcordPDF.LobCode += "_DEC_PAGE_C";
						else if(CHECK_ID == "AGENCY")
							objEbixAcordPDF.LobCode += "_DEC_PAGE_A";
						else
							objEbixAcordPDF.LobCode += "_DEC_PAGE";
					}
					else if(strCalledForPDF == PDFForAcord)
						objEbixAcordPDF.LobCode += "_ACORD";
				}
				if(getRequestPrintChk.Equals("CHECKPDFPRINT"))
				{
					strInputXml = strCheckPDFXml;
					strInputPath = strInputBase +strCheck.ToString().ToUpper().Trim() + "\\" ;
					strOutputPath = strOutputPath + SystemID+ "\\" + strCheck;

					objEbixAcordPDF.InputXml = strCheckPDFXml;
					objEbixAcordPDF.InputPath = strInputPath;
					objEbixAcordPDF.OutputPath = strOutputPath;
				}
				else if(getRequestPrintChk.Equals("CUST_RECEIPT"))
				{
					strInputXml = strAcordPDFXml;
					objEbixAcordPDF.InputXml = strInputXml;
					objEbixAcordPDF.InputPath = strInputBase +strCheck.ToString().ToUpper().Trim() + "\\";
					objEbixAcordPDF.OutputPath = strOutputPath + SystemID+ "\\" + strCheck;
				}
				else
				{
					strInputXml = strAcordPDFXml;
					objEbixAcordPDF.InputXml = strInputXml;
					DataSet DSTempDataSet = new DataSet();
					DataWrapper objSqlHelper = new DataWrapper(ConnStr,CommandType.Text);
					
					
					objWrapper.ClearParameteres();
					objWrapper.AddParameter("@Customer_Id",strCustomerId);
					objWrapper.AddParameter("@AppPol_Id",strAppId);
					objWrapper.AddParameter("@AppPolVersion_Id",strAppVersionId);
					objWrapper.AddParameter("@CalledFrom",strCalledFrom);
					DSTempDataSet = objWrapper.ExecuteDataSet("Proc_GetPDFAgencyCode");
					objWrapper.ClearParameteres();

					//DSTempDataSet = objSqlHelper.ExecuteDataSet("Proc_GetPDFAgencyCode " +  strCustomerId + "," + strAppId + "," + strAppVersionId + ",'" + strCalledFrom + "'");
					agency_code = DSTempDataSet.Tables[0].Rows[0]["AGENCY_CODE"].ToString();

					if(agency_code.EndsWith("."))
						agency_code = agency_code.Substring(0, agency_code.Length-1);

					if(CHECK_ID != "PREM_NOTICE")
						SystemID = agency_code;

					if(getRequeststring.Equals("UNDERLAYING_POL"))
					{
						strInputPath = strInputBase +strLOB + "\\" + stateCode.ToUpper().Trim()  + "\\" ;
					}
					else
					{
						strInputPath = strInputBase +strLOB + "\\" + stateCode.ToUpper().Trim()  + "\\" ;
					}

					strAlternateInputPath = strOutputPath + "EndorsementAttachment\\" + strLOB + "\\" + stateCode.ToUpper().Trim()  + "\\" ;
					 
					if(getRequestPrintChk.Equals("ADDLINT"))
					{
						if(CHECK_ID == "PREM_NOTICE")
						{
							strInputXml = strCheckPDFXml;
							strInputPath = strInputBase +strCheck.ToString().ToUpper().Trim() + "\\" ;
							// store inputpath in case of additional interest
							//InsertPremiumNoticeProccess(strCustomerId, strAppId, strAppVersionId,strInputPath,objDataWrapper);
							strOutputPath = strOutputPath + SystemID+ "\\" + strCheck;
							// store output path in case of additional interest
							//InsertPremiumNoticeProccess(strCustomerId, strAppId, strAppVersionId,strOutputPath,objDataWrapper);
							objEbixAcordPDF.InputXml = strCheckPDFXml;
							objEbixAcordPDF.InputPath = strInputPath;
							objEbixAcordPDF.OutputPath = strOutputPath;							

						}
						else if((canc_type == "NONPAYDB")||(canc_type == "NONPAYDBMEMO")||(canc_type == "NSF")||(canc_type == "NONPAY")||(canc_type == "INSREQ") || (strCalledForPDF == "CANC_NOTICE"))
						{
							strInputPath = strInputBase   + "HOME\\IN\\";
							strOutputPath = strOutputPath + agency_code + "\\" + strCustomerId + "\\" + "POLICY\\CANC_NOTICE\\" + temp;
						}
						else if(strCalledForPDF == "NREN_NOTICE")
						{
							strInputPath = strInputBase +"HOME\\IN\\";
							strOutputPath = strOutputPath + agency_code + "\\" + strCustomerId + "\\" + "POLICY\\NREN_NOTICE\\" + temp;
						}
						else if(strCalledForPDF == "REINS_NOTICE")
						{
							strInputPath = strInputBase + "HOME\\IN\\";
							strOutputPath = strOutputPath + agency_code + "\\" + strCustomerId + "\\" + "POLICY\\REINS_NOTICE\\" + temp;
						}
						else if(strCalledForPDF == "CERT_MAIL")
						{
							strInputPath = strInputBase + "HOME\\IN\\";
							strOutputPath = strOutputPath + agency_code + "\\" + strCustomerId + "\\" + "POLICY\\CANC_NOTICE\\" + temp;
						}
						else
						{
							strInputPath = strInputBase +strLOB + "\\" + stateCode.ToUpper().Trim()  + "\\" ;
							strOutputPath = strOutputPath + agency_code + "\\" + strCustomerId + "\\" + strCalledFrom + "\\" + temp;
						}
					}
					else if(getRequestPrintChk.Equals("CANC_NOTICE"))
					{
						if((canc_type == "NONPAYDB")||(canc_type == "NONPAYDBMEMO")||(canc_type == "NSF")||(canc_type == "NONPAY")||(canc_type == "INSREQ"))
							strInputPath = strInputBase + "HOME\\IN\\";
						strOutputPath = strOutputPath + agency_code + "\\" + strCustomerId + "\\" + "POLICY\\CANC_NOTICE\\" + temp;
					}
					else if(getRequestPrintChk.Equals("CERT_MAIL"))
					{
						strInputPath = strInputBase + "HOME\\IN\\";
						if(strCalledForPDF == "CANC_NOTICE")
							strOutputPath = strOutputPath + agency_code + "\\" + strCustomerId + "\\" + "POLICY\\CANC_NOTICE\\" + temp;
						else if(strCalledForPDF == "NREN_NOTICE")
							strOutputPath = strOutputPath + agency_code + "\\" + strCustomerId + "\\" + "POLICY\\NREN_NOTICE\\" + temp;
						else
							strOutputPath = strOutputPath + agency_code + "\\" + strCustomerId + "\\" + "POLICY\\CANC_NOTICE\\" + temp;
					}
					else if(getRequestPrintChk.Equals("NREN_NOTICE"))
					{
						if(canc_type != "")
							strInputPath = strInputBase + "HOME\\" + stateCode.ToUpper().Trim()  + "\\" ;
						strOutputPath = strOutputPath + agency_code + "\\" + strCustomerId + "\\" + "POLICY\\NREN_NOTICE\\" + temp;
					}
					else if(getRequestPrintChk.Equals("REINS_NOTICE"))
					{
						strInputPath = strInputBase + "HOME\\IN\\";
						strOutputPath = strOutputPath + agency_code + "\\" + strCustomerId + "\\" + "POLICY\\REINS_NOTICE\\" + temp;
					}
					else if(getRequestPrintChk.Equals("UMB_LETTER"))
					{
						strInputPath = strInputBase + "UMB\\IN\\";
						strOutputPath = strOutputPath + agency_code + "\\" + strCustomerId + "\\" + "POLICY\\UMB_LETTER\\" + temp;
					}
					else if(getRequestPrintChk.Equals("AUTO_ID_CARD"))
					{
						strInputPath = strInputBase + "PPA\\" + stateCode.ToUpper().Trim()  + "\\";
						strOutputPath = strOutputPath + agency_code + "\\" + strCustomerId + "\\" + strCalledFrom + "\\" + temp;
					}
					else
					{
						strOutputPath = strOutputPath + agency_code + "\\" + strCustomerId + "\\" + strCalledFrom + "\\" + temp;
					}
					objEbixAcordPDF.InputPath = strInputPath;
					objEbixAcordPDF.AlternateInputPath = strAlternateInputPath;
					objEbixAcordPDF.OutputPath = strOutputPath;
				}

			
				//			strImpersonationUserId = System.Configuration.ConfigurationSettings.AppSettings.Get("IUserName").ToString().Trim();
				//			strImpersonationPassword = System.Configuration.ConfigurationSettings.AppSettings.Get("IPassWd").ToString().Trim();
				//			strImpersonationDomain = System.Configuration.ConfigurationSettings.AppSettings.Get("IDomain").ToString().Trim();

				objEbixAcordPDF.ImpersonationUserId = ImpersonationUserId;
				// store imporsenationuserid
				//InsertPremiumNoticeProccess(strCustomerId, strAppId, strAppVersionId,ImpersonationUserId,objDataWrapper);
				objEbixAcordPDF.ImpersonationPassword = ImpersonationPassword;
				// store imperonation password
				//InsertPremiumNoticeProccess(strCustomerId, strAppId, strAppVersionId,ImpersonationPassword,objDataWrapper);
				objEbixAcordPDF.ImpersonationDomain = ImpersonationDomain;
				// store imperonation password
				//InsertPremiumNoticeProccess(strCustomerId, strAppId, strAppVersionId,ImpersonationDomain,objDataWrapper);
			
				try
				{
					string TranLogMess="";
					string policy_status="";
					
					if(strCalledFrom == "POLICY" && strCustomerId != "" && strAppId != "" && strAppVersionId != "")
					{
						try 
						{ 
							policy_status=	new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().GetPolicyStatus(strCalledFrom,int.Parse(strAppId),int.Parse(strAppVersionId),int.Parse(strCustomerId),objWrapper); 
						}
						catch(Exception ex)
						{
							Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
							return "";
						}
					}

					if((policy_status.ToUpper().Trim() == "SUSPENDED" || policy_status == "UISSUE") && strCalledForPDF=="DECPAGE")
						try 
						{
							PDFName = objEbixAcordPDF.GeneratePDF("APPLICATION", strCalledForPDF);
						}
						catch(Exception ex)
						{
							System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
							addInfo.Add("Err Descriptor ","Error while generating PDF.");
							addInfo.Add("CustomerID" ,strCustomerId);
							addInfo.Add("PolicyID",strAppId);
							addInfo.Add("PolicyVersionID",strAppVersionId);
							addInfo.Add("CalledFrom",strCalledFrom);
							addInfo.Add("CalledForPDF",strCalledForPDF);
							addInfo.Add("ProcessID",gStrProcessID);
							ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex,addInfo );
							//return "";
							throw(new Exception("Error while generating PDF.",ex));
						}
					else
						try 
						{
							PDFName = objEbixAcordPDF.GeneratePDF(strCalledFrom, strCalledForPDF);
						}
						catch(Exception ex)
						{
							System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
							addInfo.Add("Err Descriptor ","Error while generating PDF.");
							addInfo.Add("CustomerID" ,strCustomerId);
							addInfo.Add("PolicyID",strAppId);
							addInfo.Add("PolicyVersionID",strAppVersionId);
							addInfo.Add("CalledFrom",strCalledFrom);
							addInfo.Add("CalledForPDF",strCalledForPDF);
							addInfo.Add("ProcessID",gStrProcessID);
							ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex,addInfo );
							//return "";
							throw(new Exception("Error while generating PDF.",ex));
						}
					// store pdf name
					//InsertPremiumNoticeProccess(strCustomerId, strAppId, strAppVersionId,PDFName,objDataWrapper);
					string PDFFinalPath; 

					//ITrack 3251 24-Dec-07
					if(SystemID.Trim().EndsWith("."))
						SystemID = SystemID.Substring(0,SystemID.LastIndexOf("."));

					if(getRequestPrintChk.Equals("CHECKPDFPRINT"))
					{
						if(CHECK_ID != null && CHECK_ID != "" && CHECK_ID != "Insured")
						{
							strCustomerId = "0";
							strAppId = "0";
							strAppVersionId = "0";
							TranLogMess = "Check (Number(s): " + check_nums + ") PDF generated successfully";
							PDFFinalPath = strFinalBasePath + SystemID + "/" + "CHK/";
						}
						else
						{
							if(IsEODProcess)
								TranLogMess = "Premium Notice PDF generated successfully";
							else
								TranLogMess = "Virtual Premium Notice PDF generated successfully";
							PDFFinalPath = strFinalBasePath + SystemID + "/" + "CHK/";
							// store transaction log message
							//InsertPremiumNoticeProccess(strCustomerId, strAppId, strAppVersionId,TranLogMess,objDataWrapper);
							// store pdf final path
							//InsertPremiumNoticeProccess(strCustomerId, strAppId, strAppVersionId,PDFFinalPath,objDataWrapper);
						}
					}
					else if(getRequestPrintChk.Equals("CUST_RECEIPT"))
					{
						strCustomerId = "0";
						strAppId = "0";
						strAppVersionId = "0";
						TranLogMess = "Customer Agency Receipt generated";
						PDFFinalPath = strFinalBasePath + SystemID + "/" + "CHK/";
					}
					else if(getRequestPrintChk.Equals("CANC_NOTICE"))
					{
						if(CHECK_ID.ToUpper().Equals("AGENT"))
						{
							if(canc_type == "NONPAYDBMEMO")
								TranLogMess = "Cancellation Memo PDF for Agency generated successfully";
							else if(canc_type == "NONPAYDB")
								TranLogMess = "Cancellation Notice Non Payment DB PDF for Agency generated successfully";
							else if(canc_type == "NONPAY")
								TranLogMess = "Cancellation Notice Non Payment PDF for Agency generated successfully";
							else
								TranLogMess = "Cancellation Notice PDF for Agency generated successfully";
						}
						else
						{
							if(canc_type == "NONPAYDBMEMO")
								TranLogMess = "Cancellation Memo PDF for Customer generated successfully";
							else if(canc_type == "NONPAYDB")
								TranLogMess = "Cancellation Notice Non Payment DB PDF for Customer generated successfully";
							else if(canc_type == "NONPAY")
								TranLogMess = "Cancellation Notice Non Payment PDF for Customer generated successfully";
							else
								TranLogMess = "Cancellation Notice PDF for Customer generated successfully";
						}
						//PDFFinalPath = System.Configuration.ConfigurationSettings.AppSettings.Get("UploadURL").ToString() + "/OUTPUTPDFs/" + SystemID + "/" + strCustomerId + "/" + "POLICY/CANC_NOTICE/" + temp + "/";
						PDFFinalPath = strFinalBasePath + SystemID + "/" + strCustomerId + "/" + "POLICY/CANC_NOTICE/" + temp + "/";
					}
					else if(getRequestPrintChk.Equals("CERT_MAIL"))
					{
						if(CHECK_ID.ToUpper() == "CLIENT")
						{
							TranLogMess = "Certified Letter Template - Client PDF generated successfully";				
							//PDFFinalPath = System.Configuration.ConfigurationSettings.AppSettings.Get("UploadURL").ToString() + "/OUTPUTPDFs/" + SystemID + "/" + strCustomerId + "/" + "POLICY/CANC_NOTICE/" + temp + "/";
							PDFFinalPath = strFinalBasePath + SystemID + "/" + strCustomerId + "/" + "POLICY/CANC_NOTICE/" + temp + "/";
						}
						else
						{
							TranLogMess = "Certificate of Mailing - Insured PDF generated successfully";
							if(strCalledForPDF == "NREN_NOTICE")
								//PDFFinalPath = System.Configuration.ConfigurationSettings.AppSettings.Get("UploadURL").ToString() + "/OUTPUTPDFs/" + SystemID + "/" + strCustomerId + "/" + "POLICY/NREN_NOTICE/" + temp + "/";
								PDFFinalPath = strFinalBasePath+ SystemID + "/" + strCustomerId + "/" + "POLICY/NREN_NOTICE/" + temp + "/";
							else
								PDFFinalPath = strFinalBasePath + SystemID + "/" + strCustomerId + "/" + "POLICY/CANC_NOTICE/" + temp + "/";
						}
					}
					else if(getRequestPrintChk.Equals("NREN_NOTICE"))
					{
						if(canc_type == "")
						{
							if(CHECK_ID.ToUpper().Equals("AGENT"))
								TranLogMess = "Non-Renewal Notice PDF for Agency generated successfully";
							else
								TranLogMess = "Non-Renewal Notice PDF for Customer generated successfully";
						}
						else
						{
							if(CHECK_ID.ToUpper().Equals("AGENT"))
								TranLogMess = "Non-Renewal Agency Terminated Notice PDF for Agency generated successfully";
							else
								TranLogMess = "Non-Renewal Agency Terminated Notice PDF for Customer generated successfully";
						}
						PDFFinalPath = strFinalBasePath + SystemID + "/" + strCustomerId + "/" + "POLICY/NREN_NOTICE/" + temp + "/";
					}
					else if(getRequestPrintChk.Equals("REINS_NOTICE"))
					{
						if(CHECK_ID.ToUpper().Equals("AGENT"))
							TranLogMess = "Reinstatement Notice PDF for Agency generated successfully";
						else
							TranLogMess = "Reinstatement Notice PDF for Customer generated successfully";
						PDFFinalPath = strFinalBasePath + SystemID + "/" + strCustomerId + "/" + "POLICY/REINS_NOTICE/" + temp + "/";
					}
					else if(getRequestPrintChk.Equals("UMB_LETTER"))
					{
						TranLogMess = "Umbrella Letter PDF generated successfully";
						PDFFinalPath = strFinalBasePath + SystemID + "/" + strCustomerId + "/" + "POLICY/UMB_LETTER/" + temp+ "/";
					}
					else if(getRequestPrintChk.Equals("AUTO_ID_CARD"))
					{
						TranLogMess = "Auto ID Card PDF generated successfully";
						PDFFinalPath = strFinalBasePath + SystemID + "/" + strCustomerId + "/" + strCalledFrom + "/" + temp + "/";
					}
					else if(getRequestPrintChk.Equals("ADDLINT"))
					{
						PDFFinalPath = strFinalBasePath + SystemID + "/" + strCustomerId + "/" + strCalledFrom + "/" + temp + "/";
						if(CHECK_ID == "PREM_NOTICE")
						{
							strCalledFrom = "PREM_NOTICE";
							if(IsEODProcess)
								TranLogMess = "Premium Notice Additional Interest PDF generated successfully";
							else
								TranLogMess = "Virtual Premium Notice Additional Interest PDF generated successfully";
							PDFFinalPath = strFinalBasePath + SystemID + "/" + "CHK/";
							// store pdf final path in addiional interst
							//InsertPremiumNoticeProccess(strCustomerId, strAppId, strAppVersionId,PDFFinalPath,objDataWrapper);
							// store transaction log message in addiional interst
							//InsertPremiumNoticeProccess(strCustomerId, strAppId, strAppVersionId,TranLogMess,objDataWrapper);
						}
						else if(strCalledForPDF == "DECPAGE")
						{
							TranLogMess = "Additional Interest PDF generated successfully";
							PDFFinalPath = strFinalBasePath + SystemID + "/" + strCustomerId + "/" + strCalledFrom + "/" + temp + "/";
						}
						else if(strCalledForPDF == "CANC_NOTICE")
						{
							if(canc_type == "NONPAYDBMEMO")
								TranLogMess = "Cancellation Memo PDF for Additional Interest generated successfully";
							else if(canc_type == "NONPAYDB")
								TranLogMess = "Cancellation Notice Non Payment DB PDF for Additional Interest generated successfully";
							else
								TranLogMess = "Cancellation Notice PDF for Additional Interest generated successfully";
							PDFFinalPath = strFinalBasePath + SystemID + "/" + strCustomerId + "/" + "POLICY/CANC_NOTICE/" + temp + "/";
						}
						else if(strCalledForPDF == "NREN_NOTICE")
						{
							TranLogMess = "Non-Renewal Notice PDF for Additional Interest generated successfully";
							PDFFinalPath = strFinalBasePath + SystemID + "/" + strCustomerId + "/" + "POLICY/NREN_NOTICE/" + temp + "/";
						}
						else if(strCalledForPDF == "REINS_NOTICE")
						{
							TranLogMess = "Reinstatement Notice PDF for Additional Interest generated successfully";
							PDFFinalPath = strFinalBasePath + SystemID + "/" + strCustomerId + "/" + "POLICY/REINS_NOTICE/" + temp + "/";
						}
						else if(strCalledForPDF == "CERT_MAIL")
						{
							TranLogMess = "Certificate of Mailing - Additional Interest PDF generated successfully";
							PDFFinalPath = strFinalBasePath + SystemID + "/" + strCustomerId + "/" + "POLICY/CANC_NOTICE/" + temp + "/";
						}
					}
					else
					{
						if(strCalledForPDF == PDFForDecPage)
						{
							switch(CHECK_ID)
							{
								case ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_AGENCY:
									TranLogMess = "Declaration Page PDF for Agency generated successfully";
									break;
								case ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_CUSTOMER:
									TranLogMess = "Declaration Page PDF for Customer with Additional Wordings generated successfully";
									break;
								case "CUSTOMER-NOWORD":
									TranLogMess = "Declaration Page PDF for Customer generated successfully";
									break;
								default:
									TranLogMess = "Declaration Page PDF generated successfully";
									break;
							}
						}
						else if(strCalledForPDF == PDFForAcord)
							TranLogMess = "Acord PDF generated successfully";
						else
							TranLogMess = "PDF generated successfully";
						PDFFinalPath = strFinalBasePath + SystemID + "/" + strCustomerId + "/" + strCalledFrom + "/" + temp + "/";
					}

					string PDFlink = PDFName + "<COMMON_PDF_URL=window.open(\"" + PDFFinalPath + PDFName + "\")>";
					if(getRequestPrintChk.Equals("CUST_RECEIPT"))
						PDFlink = custRecp_custInfo + PDFName + "<COMMON_PDF_URL=window.open(\"" + PDFFinalPath + PDFName + "\")>";
					if((getRequestPrintChk.Equals("CHECKPDFPRINT")) || CHECK_ID == "PREM_NOTICE" || (getRequestPrintChk.Equals("CUST_RECEIPT")) || (temp == "final")||(getRequestPrintChk.Equals("CANC_NOTICE"))||(getRequestPrintChk.Equals("NREN_NOTICE"))||(getRequestPrintChk.Equals("REINS_NOTICE")))
					{
						if((CHECK_ID == "PREM_NOTICE") || (getRequestPrintChk.Equals("CHECKPDFPRINT") && (CHECK_ID == null || CHECK_ID == "" || CHECK_ID == "Insured")))
						{
							strCalledFrom = "PREM_NOTICE";
							#region Add Print Job Prem Notice
							Cms.Model.Policy.Process.ClsPrintJobsInfo objPrintJobsInfo = new Cms.Model.Policy.Process.ClsPrintJobsInfo();
							objPrintJobsInfo.CUSTOMER_ID = int.Parse(strCustomerId);
							objPrintJobsInfo.POLICY_ID = int.Parse(strAppId);
							objPrintJobsInfo.POLICY_VERSION_ID = int.Parse(strAppVersionId);
							objPrintJobsInfo.PROCESS_ID		=	100 ;// Not Clear what PrOCESS ID WILL GO ON PREM NOTICE SO 100
							objPrintJobsInfo.PROCESS_ROW_ID	=	100 ;// Not Clear what PrOCESS ID WILL GO ON PREM NOTICE SO 100
							objPrintJobsInfo.ENTITY_TYPE = "PREM_NOTICE";
							objPrintJobsInfo.FILE_NAME = PDFName;
							objPrintJobsInfo.ONDEMAND_FLAG ="Y";
							objPrintJobsInfo.DOCUMENT_CODE = "PREM";
							objPrintJobsInfo.PRINT_DATETIME = DateTime.Now;
							objPrintJobsInfo.PRINTED_DATETIME = DateTime.Now;
							objPrintJobsInfo.CREATED_DATETIME = DateTime.Now;
							if(IsEODProcess)
								objPrintJobsInfo.CREATED_BY = EODUserID;
							else
								objPrintJobsInfo.CREATED_BY = int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());

							objPrintJobsInfo.URL_PATH = PDFFinalPath.Substring(0, PDFFinalPath.Length-1);

							DataSet DSTempDataSet = new DataSet();
							DataWrapper objSqlHelper = new DataWrapper(ConnStr,CommandType.Text);
							DSTempDataSet = objSqlHelper.ExecuteDataSet("Proc_GetPDFAgencyCode " +  strCustomerId + "," + strAppId + "," + strAppVersionId + ",'POLICY'");
							string agency_id = DSTempDataSet.Tables[0].Rows[0]["AGENCY_ID"].ToString();
							// store agency id
							//InsertPremiumNoticeProccess(strCustomerId, strAppId, strAppVersionId,agency_id,objDataWrapper);
							try { objPrintJobsInfo.AGENCY_ID = int.Parse(agency_id); } 
							catch (Exception ex)
                            { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex); }
							ClsPolicyProcess objPolicyProcess = new ClsPolicyProcess();
							objPolicyProcess.BeginTransaction();
							objPolicyProcess.AddPrintJobs(objPrintJobsInfo);
							objPolicyProcess.CommitTransaction();
							#endregion Add Print Job Prem Notice
					
						}
						if(IsEODProcess)
						{
							WritePDFTransactionLog(int.Parse(strCustomerId),int.Parse(strAppId), int.Parse(strAppVersionId),TranLogMess,EODUserID , PDFlink, strCalledFrom, AddlInt_tlog, objWrapper);
						}
						else
						{
							WritePDFTransactionLog(int.Parse(strCustomerId),int.Parse(strAppId), int.Parse(strAppVersionId),TranLogMess, int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString()), PDFlink, strCalledFrom, AddlInt_tlog, objWrapper);
						}
						// store transaction log
						//InsertPremiumNoticeProccess(strCustomerId, strAppId, strAppVersionId,TranLogMess,objDataWrapper);
					}
					//Added by Mohit Agarwal 9-Jul-2007
					if((CHECK_ID == "Insured") && ((strCalledFrom == "PREM_NOTICE") || (getRequestPrintChk.Equals("CANC_NOTICE"))||(getRequestPrintChk.Equals("NREN_NOTICE"))||(getRequestPrintChk.Equals("REINS_NOTICE"))))
					{
						string tran_desc="Notice sent";
						ClsCustBalanceInfo objCustBalanceInfo = new ClsCustBalanceInfo();
						if(getRequestPrintChk.Equals("CANC_NOTICE"))
						{
							if(canc_type_act == "NONPAYDB")
								tran_desc = "Non Pay DB cancellation notice sent";
							else if(canc_type_act == "NONPAY")
								tran_desc = "Non Pay cancellation notice sent";
							else if(canc_type_act == "NSF")
								tran_desc = "NSF Replace cancellation notice sent";
							else if(canc_type_act == "NSF_NOREPLACE")
								tran_desc = "NSF/No Replace cancellation notice sent";
							else if (canc_type_act=="COMPANY")  //copany Requeest Underwriting
								//tran_desc = "Company Request(Underwriting) cancellation notice sent";
								//Space added after Request For Itrack Issue #5242.
								tran_desc = "Company Request (U/W) cancellation notice sent"; //Ravindra(09-19-2008): As discussed with Abbie
							else if (canc_type_act=="AGENTS") //AGENTS_REQUEST
								tran_desc = "Agents Request cancellation notice sent";
							else if(canc_type_act == "INSREQ")
								tran_desc = "Insured Request cancellation notice sent";
							else if(canc_type_act == "NONPAYDBMEMO")
								tran_desc = "Non Pay DB Memo sent";
							else
								tran_desc = "Past Due cancellation notice sent";

							objCustBalanceInfo.NOTICE_TYPE = "CANC_NOTICE";
						}
						else if(getRequestPrintChk.Equals("NREN_NOTICE"))
						{
							if(canc_type_act == "NREN_NOTICE_NOTIFICATION")
								tran_desc = "Non-Renewal Agency Terminated notice sent";
							else if(canc_type_act == "NREN_NOTICE_NO_NOTIFICATION")
								tran_desc = "Non-Renewal Agency Terminated notice sent";
							else
								tran_desc = "Non-Renewal notice sent";
							objCustBalanceInfo.NOTICE_TYPE = "NREN_NOTICE";

						}
						else if(getRequestPrintChk.Equals("REINS_NOTICE"))
						{
							tran_desc = "Re-instatement notice sent";
							objCustBalanceInfo.NOTICE_TYPE = "REINS_NOTICE";
						}
						if(strCalledFrom == "PREM_NOTICE")
						{
							tran_desc = "Premium notice sent";
							objCustBalanceInfo.NOTICE_TYPE = "PREM_NOTICE";
							// store transaction description
							//InsertPremiumNoticeProccess(strCustomerId, strAppId, strAppVersionId,tran_desc,objDataWrapper);
						}

						objCustBalanceInfo.CUSTOMER_ID = int.Parse(strCustomerId);
						objCustBalanceInfo.POLICY_ID = int.Parse(strAppId);
						objCustBalanceInfo.POLICY_VERSION_ID = int.Parse(strAppVersionId);
						objCustBalanceInfo.TRAN_DESC = tran_desc;
						objCustBalanceInfo.UPDATED_FROM = "T";
						objCustBalanceInfo.CREATED_DATE = DateTime.Now;
						objCustBalanceInfo.AMOUNT = notice_amount;
						objCustBalanceInfo.MIN_DUE = MinimumDue;
						objCustBalanceInfo.NOTICE_URL = PDFFinalPath + PDFName;
						if(base_due_date != "")
							objCustBalanceInfo.DUE_DATE = DateTime.Parse(base_due_date);
						AddCustBalance(objCustBalanceInfo);
						// store notice url
						//InsertPremiumNoticeProccess(strCustomerId, strAppId, strAppVersionId,objCustBalanceInfo.NOTICE_URL,objDataWrapper);
					}
				}
				catch(Exception ex)
				{
					System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
					addInfo.Add("Err Descriptor ","Error while generating PDF.");
					addInfo.Add("CustomerID" ,strCustomerId);
					addInfo.Add("PolicyID",strAppId);
					addInfo.Add("PolicyVersionID",strAppVersionId);
					addInfo.Add("CalledFrom",strCalledFrom);
					addInfo.Add("CalledForPDF",strCalledForPDF);
					addInfo.Add("ProcessID",gStrProcessID);
					ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex,addInfo );
					//return "";
					throw(new Exception("Error while generating PDF.",ex));
				}

				if(blank_num == "BLANK_NUM_SOME")
					PDFName = "BLANK_NUM_SOME~" + PDFName;

				//Following code to call the web service for the printing of PDFs has been commented and put on hold
				//			AcordWebServices.AcordWebServices objAcordWebServices = new Cms.BusinessLayer.BlProcess.AcordWebServices.AcordWebServices();
				//			PDFName = objAcordWebServices.GeneratePDF(int.Parse(strCustomerId==""?"0":strCustomerId),int.Parse(strAppId==""?"0":strAppId),int.Parse(strAppVersionId==""?"0":strAppVersionId),strLOB,strInputXml, strInputPath, strOutputPath, strImpersonationUserId, strImpersonationPassword, strImpersonationDomain, strCalledFrom);
				if(getRequestPrintChk.Equals("ADDLINT"))
				{
					addlintPDFName += PDFName;
					return addlintPDFName;
				}
				else
				{
					// store pdf name
					//InsertPremiumNoticeProccess(strCustomerId, strAppId, strAppVersionId,PDFName,objDataWrapper);
					return PDFName;
				}
			}
			catch(Exception objExp)
			{
				System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
				addInfo.Add("Err Descriptor ","Error while generating PDF.");
				addInfo.Add("CustomerID" ,strCustomerId);
				addInfo.Add("PolicyID",strAppId);
				addInfo.Add("PolicyVersionID",strAppVersionId);
				addInfo.Add("ProcessID",strProcessID);
				addInfo.Add("CalledFrom",strCalledFrom);
				addInfo.Add("CalledForPDF",strCalledForPDF);		
				ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp,addInfo );
				//return objExp.Message;
				throw(new Exception("Error while generating PDF.",objExp));
			}
		}

		#endregion	

		// Added by Mohit Agarwal 30-Jan-2007 for making transaction log entries for Pdfs
		public void WriteTransactionLog(int CustomerID, int App_PolicyID, int App_PolicyVersionID, string TransactionDescription, int RecordedBy, string CustomDesc, string strCalledFrom, string AddlInt_tlog)
		{
			try
			{
				if (objWrapper==null)
					objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
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
				else if(strCalledFrom.Equals("CUST_RECEIPT"))
				{
				}
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
				else if(strCalledFrom.ToUpper().Equals("PREM_NOTICE"))
				{
					objTransactionInfo.POLICY_ID				=	App_PolicyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	App_PolicyVersionID;
				}
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
			catch(Exception ex)
			{

				System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
				addInfo.Add("Err Descriptor ","Error while inserting transaction log.");
				addInfo.Add("CustomerID" ,CustomerID.ToString());
				addInfo.Add("PolicyID",App_PolicyID.ToString());
				addInfo.Add("PolicyVersionID",App_PolicyVersionID.ToString());
				addInfo.Add("ProcessID",gStrProcessID);
				addInfo.Add("TransactionDescription",TransactionDescription);
				ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex,addInfo );
				//return objExp.Message;
				throw(new Exception("Error while inserting transaction log.",ex));
			}
			finally
			{
				
			}
		}

		public void WritePDFTransactionLog(int CustomerID, int App_PolicyID, int App_PolicyVersionID, string TransactionDescription, int RecordedBy, string CustomDesc, string strCalledFrom, string AddlInt_tlog, DataWrapper objDataWrapper)
		{
			try
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
				else if(strCalledFrom.Equals("CUST_RECEIPT"))
				{
				}
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
				else if(strCalledFrom.ToUpper().Equals("PREM_NOTICE"))
				{
					objTransactionInfo.POLICY_ID				=	App_PolicyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	App_PolicyVersionID;
				}
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
			catch(Exception ex)
			{

				System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
				addInfo.Add("Err Descriptor ","Error while inserting transaction log.");
				addInfo.Add("CustomerID" ,CustomerID.ToString());
				addInfo.Add("PolicyID",App_PolicyID.ToString());
				addInfo.Add("PolicyVersionID",App_PolicyVersionID.ToString());
				addInfo.Add("ProcessID",gStrProcessID);
				addInfo.Add("TransactionDescription",TransactionDescription);
				ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex,addInfo );
				//return objExp.Message;
				throw(new Exception("Error while inserting transaction log.",ex));
			}
			finally
			{
				
			}
		}

		public void WriteTransactionLog(int CustomerID, int App_PolicyID, int App_PolicyVersionID, string TransactionDescription, int RecordedBy, string CustomDesc, string strCalledFrom, string AddlInt_tlog,DataWrapper objDataWrapper )
		{
			try
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
				else if(strCalledFrom.Equals("CUST_RECEIPT"))
				{
				}
				else if(strCalledFrom.ToUpper().Equals("CHECKPDFPRINT"))
				{
					if(CustomerID == 0)
					{
						objTransactionInfo.CUSTOM_INFO = "";
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
						objTransactionInfo.CUSTOM_INFO += ";" + CustomDesc;
					}
				}
				else if(strCalledFrom.ToUpper().Equals("PREM_NOTICE"))
				{
					objTransactionInfo.POLICY_ID				=	App_PolicyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	App_PolicyVersionID;
				}
				else
					return;
				objTransactionInfo.RECORDED_BY				=	RecordedBy;
				objTransactionInfo.TRANS_DESC				=	TransactionDescription;
				if(TransactionDescription.IndexOf("Additional Interest") >= 0 && AddlInt_tlog != "")
				{
					objTransactionInfo.CUSTOM_INFO = AddlInt_tlog;
					objTransactionInfo.CUSTOM_INFO += ";" + CustomDesc;
				}
				objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				objDataWrapper.ClearParameteres();
			}
			catch(Exception ex)
			{

				System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
				addInfo.Add("Err Descriptor ","Error while inserting transaction log.");
				addInfo.Add("CustomerID" ,CustomerID.ToString());
				addInfo.Add("PolicyID",App_PolicyID.ToString());
				addInfo.Add("PolicyVersionID",App_PolicyVersionID.ToString());
				addInfo.Add("ProcessID",gStrProcessID);
				addInfo.Add("TransactionDescription",TransactionDescription);
				ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex,addInfo );
				//return objExp.Message;
				throw(new Exception("Error while inserting transaction log.",ex));
			}
			finally
			{
				
			}
		}

		#region AddCustBalance(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="ObjAgencyInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public void AddCustBalance(ClsCustBalanceInfo ObjCustBalanceInfo)
		{
			//string		strStoredProc	=	"Proc_InsertACT_CUSTOMER_BALANCE_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper ;//= new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			if (objWrapper==null)
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			else
			objDataWrapper= objWrapper;
		
			AddCustBalance(ObjCustBalanceInfo,objDataWrapper);
		}

		
		public void AddCustBalance(ClsCustBalanceInfo ObjCustBalanceInfo, DataWrapper objDataWrapper )
		{
			string		strStoredProc	=	"Proc_InsertACT_CUSTOMER_BALANCE_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			try
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@CUSTOMER_ID",ObjCustBalanceInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",ObjCustBalanceInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",ObjCustBalanceInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@AMOUNT",ObjCustBalanceInfo.AMOUNT);

				objDataWrapper.AddParameter("@TRAN_DESC",ObjCustBalanceInfo.TRAN_DESC);
				objDataWrapper.AddParameter("@UPDATED_FROM",ObjCustBalanceInfo.UPDATED_FROM);
				objDataWrapper.AddParameter("@CREATED_DATE",ObjCustBalanceInfo.CREATED_DATE);
				if(ObjCustBalanceInfo.DUE_DATE != DateTime.MinValue)
					objDataWrapper.AddParameter("@DUE_DATE",ObjCustBalanceInfo.DUE_DATE);
				objDataWrapper.AddParameter("@NOTICE_URL",ObjCustBalanceInfo.NOTICE_URL);
				objDataWrapper.AddParameter("@NOTICE_TYPE",ObjCustBalanceInfo.NOTICE_TYPE);
				objDataWrapper.AddParameter("@MIN_DUE",ObjCustBalanceInfo.MIN_DUE);
				objDataWrapper.AddParameter("@TOTAL_PREMIUM_DUE",ObjCustBalanceInfo.TOTAL_PREMIUM_DUE);
				objDataWrapper.ExecuteNonQuery(strStoredProc);

				objDataWrapper.ClearParameteres();
		
			}
			catch(Exception ex)
			{

				System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
				addInfo.Add("Err Descriptor ","Error while adding Customer balance.");
				addInfo.Add("CustomerID" ,ObjCustBalanceInfo.CUSTOMER_ID.ToString());
				addInfo.Add("PolicyID",ObjCustBalanceInfo.POLICY_ID.ToString());
				addInfo.Add("PolicyVersionID",ObjCustBalanceInfo.POLICY_VERSION_ID.ToString());
				addInfo.Add("PolicyVersionID",ObjCustBalanceInfo.TRAN_DESC);
				addInfo.Add("PolicyVersionID",ObjCustBalanceInfo.UPDATED_FROM);
				addInfo.Add("ProcessID",gStrProcessID);
				ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex,addInfo );
				throw(new Exception("Error while adding Customer balance.",ex));
			}
		}

		#endregion

        #region Method to save XML for Aviation(Added By Chetna) 
        private void InsertAviationXml(string strCustomerId, string strAppId, string strAppVersionId, string strCopyTo, string strcutomerxml)
        {
            try
            {
                //DataWrapper gobjDataWrapper;
                objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", strCustomerId);
                objDataWrapper.AddParameter("@POLICY_ID", strAppId);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", strAppVersionId);
                objDataWrapper.AddParameter("@CALLED_FOR", strCopyTo);
                objDataWrapper.AddParameter("@CUSTOMER_XML", strcutomerxml);
                objDataWrapper.ExecuteNonQuery("PROC_INSERTXMLFORPDF");
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                //throw new Exception(ex.Message);
            }
            finally
            { }
        }
        #endregion
        //---------------- Modification By Praveen Kumar 08/09/2010 Starts -----------
        public String GenerateCanellationNoticePdf(string strCustomerId, string PolicyID, string PolicyVersionID, string strCalledFrom, string strCalledForPDF, string strLOB, string getRequeststring, ref string SystemID, string getRequestPrintChk, string CHECK_ID, string temp)
        {
            String GeneratedPDFName="",PDFXml="";
            DataSet DSTemp;
            DSTemp = FetchCanellationNoticePdfXml(strCustomerId, PolicyID, PolicyVersionID);
            PDFXml = GenerateXMLDocumentdata(DSTemp);
            //PDFXml= DSTemp.GetXml().Replace("NewDataSet","CANCELLATION_NOTICE");
            //PDFXml = PDFXml.Replace("Table", "STATE_XML");
            //save XML In DB
            if (PDFXml != "" && PDFXml != "<POLICY_CANCEL_NOTICE></POLICY_CANCEL_NOTICE>")
                this.SavePolicyCancDocumentXml(int.Parse(strCustomerId), int.Parse(PolicyID), int.Parse(PolicyVersionID), PDFXml, "CANCE_NOTICE");
            //string fileName = "";
            //fileName = GeneratePolicyPremNoticepdfFromXML(CustomerID, PolicyID, PolicyVersionID, PDFXml);
            
            ClsGenerateCancNotice objGenerateCanceNotice = new ClsGenerateCancNotice();
            String  stateCode = new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().SetStateCode(strCalledFrom, int.Parse(PolicyID), int.Parse(PolicyVersionID), int.Parse(strCustomerId));
           
            objGenerateCanceNotice.CarrierSystemID = CarrierSystemID;
            objGenerateCanceNotice.AgencyCode = ClsCommon.FetchValueFromXML("AGENCY_CODE", PDFXml);
            objGenerateCanceNotice.CustomerID = int.Parse(strCustomerId);
            objGenerateCanceNotice.PolicyID = int.Parse(PolicyID);
            objGenerateCanceNotice.PolicyVersionID = int.Parse(PolicyVersionID);
            objGenerateCanceNotice.StateName = stateCode;
            objGenerateCanceNotice.LoBName = strLOB;
            objGenerateCanceNotice.InPutFileName = "CancellationNoticeTemplate.pdf";
            objGenerateCanceNotice.OutPutFileName = ClsCancellationProcess.DocumentName;

            String Path = System.Configuration.ConfigurationSettings.AppSettings["UploadURL"].ToString();
            objGenerateCanceNotice.pdfPath = System.Web.HttpContext.Current.Server.MapPath(Path);
            objGenerateCanceNotice.ImpersonationUserId  = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName").ToString().Trim();
            objGenerateCanceNotice.ImpersonationPassword = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd").ToString().Trim();
            objGenerateCanceNotice.ImpersonationDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain").ToString().Trim();

            GeneratedPDFName = objGenerateCanceNotice.GenerateCancellationNoticePDF(PDFXml);

            return GeneratedPDFName;
        }
        public DataSet FetchCanellationNoticePdfXml(string CustomerID, string PolicyId, string PolVersionId)
        {
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolVersionId);
                //objWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID);
                //DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetProductPolicyCancellationNoticeData");
                DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetCancellationNoticeDataforPdf");
                objWrapper.ClearParameteres();
                return dsTemp;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        private void SavePolicyCancDocumentXml(int CustomerID, int PolicyId, int PolVersionId, String strPolicyDocXML, string doctype)
        {
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolVersionId);
                objWrapper.AddParameter("@DOC_XML", strPolicyDocXML);
                objWrapper.AddParameter("@DOC_TYPE", doctype);
                int retVal = objWrapper.ExecuteNonQuery("Proc_SavePolicyDocumentXML");
                objWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public string GenerateXMLDocumentdata(DataSet ds)
        {
            string PdfDataxml = "";
            string tableXML = "";
            StringBuilder strBuilder = new StringBuilder();
            try
            {
                strBuilder.Append("<POLICY_CANCEL_NOTICE>");
                if (ds != null)
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        tableXML = "";
                        foreach (DataRow dr in dt.Rows)
                        {
                            tableXML += dr.ItemArray[0].ToString();

                        }
                        strBuilder.Append(tableXML);
                    }
                }
                strBuilder.Append("</POLICY_CANCEL_NOTICE>");
            }
            catch (Exception ex) { throw new Exception("No data Found to generate pdf XML : " + ex.Message); }
            PdfDataxml = strBuilder.ToString();
            return PdfDataxml;
        }
      
        //---------------- Modification By Praveen Kumar 08/09/2010 Ends -----------
    }
}
