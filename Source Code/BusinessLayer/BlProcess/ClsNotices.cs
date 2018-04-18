using System;
using System.Xml;
using System.Data;
using Cms.DataLayer;
using System.Collections;
using Cms.Model.Account;
using Cms.BusinessLayer.BlCommon;
using System.Text;
using BlGeneratePdf;
namespace Cms.BusinessLayer.BlProcess
{
	/// <summary>
	/// Summary description for ClsNotices.
	/// </summary>
	public class ClsNotices : ClsCommonPdfXML
	{
		private DataSet dsNonPayDB;

		
		//11865 LienHolder
		public const int LienHolder = 11865;
		private string mPropertyAddress , mAgencyCode;
		private int mLobID;
		public ClsNotices()
		{
			mPropertyAddress = "";
		}

        public string GeneratePremiumNotice(int CustomerID, int PolicyID, int PolicyVersionID, string CARRIER_CODE, string DueDate, int LangID)
        {
            string strReturnXML = "", PDFName = "";
            int IS_EOD;

            if (IsEODProcess)
                IS_EOD = 1;
            else
                IS_EOD = 0;

            DataWrapper objDatawrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            DataSet dsPNotice = null;

            objDatawrapper.AddParameter("@CUSTOMER_ID", CustomerID, SqlDbType.Int);
            objDatawrapper.AddParameter("@POLICY_ID", PolicyID, SqlDbType.Int);
            objDatawrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID, SqlDbType.Int);
            objDatawrapper.AddParameter("@NOTICE_DUE_DATE", DueDate, SqlDbType.Date);
            objDatawrapper.AddParameter("@IS_EOD", IS_EOD, SqlDbType.SmallInt);
            objDatawrapper.AddParameter("@CARRIER_CODE", CARRIER_CODE, SqlDbType.NVarChar);
            objDatawrapper.AddParameter("@LANG_ID", LangID, SqlDbType.Int);

            dsPNotice = objDatawrapper.ExecuteDataSet("Proc_GetPremiumNoticeDetails");

            StringBuilder returnString = new StringBuilder();

            returnString.Remove(0, returnString.Length);

            returnString.Append("<PremiumNotice><Policy>");
            strReturnXML = ClsCommon.GetXML(dsPNotice.Tables[0]);
            strReturnXML = strReturnXML.Replace("<NewDataSet>", "").Replace("</NewDataSet>", "").Replace("<Table>", "").Replace("</Table>", "").Replace("\r\n", "");
            returnString.Append(strReturnXML);
            returnString.Append("</Policy>");

            strReturnXML = ClsCommon.GetXMLWithOutRemovejunk(dsPNotice.Tables[1]);
            strReturnXML = strReturnXML.Replace("<NewDataSet>", "").Replace("</NewDataSet>", "").Replace("<Table>", "").Replace("</Table>", "").Replace("\r\n", "");
            returnString.Append(strReturnXML);

            strReturnXML = ClsCommon.GetXMLWithOutRemovejunk(dsPNotice.Tables[2]);
            strReturnXML = strReturnXML.Replace("<NewDataSet>", "").Replace("</NewDataSet>", "").Replace("<Table>", "").Replace("</Table>", "").Replace("\r\n", "");
            returnString.Append(strReturnXML);

            returnString.Append("</PremiumNotice>");

            BlGeneratePdf.ControlParse objControlParse = new BlGeneratePdf.ControlParse();

            objControlParse.CUSTOMER_Id = CustomerID;
            objControlParse.Policy_Id = PolicyID;
            objControlParse.Policy_Version = PolicyVersionID;
            objControlParse.IDomain = System.Configuration.ConfigurationManager.AppSettings.Get("IDomain").ToString();
            objControlParse.IUserName = System.Configuration.ConfigurationManager.AppSettings.Get("IUserName").ToString();
            objControlParse.IPassWd = System.Configuration.ConfigurationManager.AppSettings.Get("IPassWd").ToString();
            if (IsEODProcess)
            {
                objControlParse.PdfTemplatePath = ClsCommon.GetKeyValueWithIP("PremiumNotice_Template_Path");
                objControlParse.MapXmlFilePath = ClsCommon.GetKeyValueWithIP("PDFCONTROLNAMEMAPPING_XML__Path");
                objControlParse.PdfOutPutPath = System.IO.Path.GetFullPath(UploadPath + "\\OUTPUTPDFs\\");
            }
            else
            {
                objControlParse.PdfTemplatePath = ClsCommon.GetKeyValueWithIP("PremiumNotice_Template_Path");
                objControlParse.MapXmlFilePath = ClsCommon.GetKeyValueWithIP("PDFCONTROLNAMEMAPPING_XML__Path");
                objControlParse.PdfOutPutPath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()).ToString() + "\\OUTPUTPDFs\\";
            }
            objControlParse.InputXml = returnString.ToString();
            
            PDFName = objControlParse.GeneratePdf();

            return PDFName; 
        }
		public string GeneratePremiumNotice(int CustomerID , int PolicyID, int PolicyVersionID, string SystemID, string DueDate )
		{
			string TranLogMess ="" , PDFFinalPath ;
			DataWrapper objDatawrapper =new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES ,DataWrapper.SetAutoCommit.OFF);	
			double MinDue , TotalDue, TotalPremiumDue;
			string PDFlink ="" ,PDFName ="";
			
			string CalledFrom ="PREM_NOTICE";

			try
			{
				ClsPremNotPdfXML objXml = new ClsPremNotPdfXML(CustomerID, PolicyID , PolicyVersionID,DueDate, objDatawrapper);
	
				string strNoticeXML = objXml.GetXMLForInsured(out MinDue , out TotalDue , out TotalPremiumDue);

				AcordPDF.AcordXMLParser objEbixAcordPDF = new AcordPDF.AcordXMLParser();

				objEbixAcordPDF.ClientId = CustomerID;
				objEbixAcordPDF.PolicyId = PolicyID;
				objEbixAcordPDF.PolicyVersion = PolicyVersionID;
				objEbixAcordPDF.LobCode = "PREM";


				string strInputPath = InputBase  + "CHK\\" ;
				string strOutputPath = OutputPath + SystemID+ "\\CHK";

				objEbixAcordPDF.InputXml = strNoticeXML;
				objEbixAcordPDF.InputPath = strInputPath;
				objEbixAcordPDF.OutputPath = strOutputPath;

				objEbixAcordPDF.ImpersonationUserId = ImpersonationUserId;
				objEbixAcordPDF.ImpersonationPassword = ImpersonationPassword;
				objEbixAcordPDF.ImpersonationDomain = ImpersonationDomain;
				
				PDFName = objEbixAcordPDF.GeneratePDF("CHK", CalledFrom );  

				//Write Tran log 
				PDFFinalPath = FinalBasePath + SystemID + "/" + "CHK/";
				PDFlink = PDFName + "<COMMON_PDF_URL=window.open(\"" + PDFFinalPath + PDFName + "\")>";

				if(IsEODProcess)
					TranLogMess = "Premium Notice PDF generated successfully";
				else
					TranLogMess = "Virtual Premium Notice PDF generated successful";

				if(IsEODProcess)
				{
					WriteTransactionLog(CustomerID , PolicyID , PolicyVersionID,TranLogMess,EODUserID , PDFlink, CalledFrom, "",objDatawrapper);
				} 
				else
				{
					WriteTransactionLog(CustomerID , PolicyID , PolicyVersionID,TranLogMess, int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString()), PDFlink, CalledFrom, "",objDatawrapper);
				}

				if(IsEODProcess)
				{
					//Create an entry in Customer Transaction History
					ClsCustBalanceInfo objCustBalanceInfo = new ClsCustBalanceInfo();
					objCustBalanceInfo.TRAN_DESC = "Premium notice sent";

					objCustBalanceInfo.CUSTOMER_ID = CustomerID ;
					objCustBalanceInfo.POLICY_ID = PolicyID;
					objCustBalanceInfo.POLICY_VERSION_ID = PolicyVersionID;
					objCustBalanceInfo.UPDATED_FROM = "T";
					objCustBalanceInfo.CREATED_DATE = DateTime.Now;
					objCustBalanceInfo.AMOUNT = TotalDue;
					objCustBalanceInfo.MIN_DUE = MinDue ;
					objCustBalanceInfo.TOTAL_PREMIUM_DUE = TotalPremiumDue;
					objCustBalanceInfo.NOTICE_TYPE  = "PREM_NOTICE" ;  
					objCustBalanceInfo.NOTICE_URL = PDFFinalPath + PDFName;
					if(DueDate != "")
						objCustBalanceInfo.DUE_DATE = DateTime.Parse(DueDate);
					AddCustBalance(objCustBalanceInfo,objDatawrapper);

					//Add to print Jobs 
					ClsPolicyProcess objPolicyProcess = new ClsPolicyProcess();
					//string agency_id ;

					Cms.Model.Policy.Process.ClsPrintJobsInfo objPrintJobsInfo = new Cms.Model.Policy.Process.ClsPrintJobsInfo();
					objPrintJobsInfo.CUSTOMER_ID = CustomerID;
					objPrintJobsInfo.POLICY_ID = PolicyID;
					objPrintJobsInfo.POLICY_VERSION_ID = PolicyVersionID;
					objPrintJobsInfo.PROCESS_ID		=	100 ;// Not Clear what PrOCESS ID WILL GO ON PREM NOTICE SO 100
					objPrintJobsInfo.PROCESS_ROW_ID	=	100 ;// Not Clear what PrOCESS ID WILL GO ON PREM NOTICE SO 100
					objPrintJobsInfo.ENTITY_TYPE = "PREM_NOTICE";
					objPrintJobsInfo.FILE_NAME = PDFName;
					objPrintJobsInfo.ONDEMAND_FLAG ="N";
					objPrintJobsInfo.DOCUMENT_CODE = "PREM";
					objPrintJobsInfo.PRINT_DATETIME = DateTime.Now;
					//objPrintJobsInfo.PRINTED_DATETIME = DateTime.Now;
					objPrintJobsInfo.CREATED_DATETIME = DateTime.Now;
					objPrintJobsInfo.CREATED_BY = EODUserID;
					objPrintJobsInfo.URL_PATH = PDFFinalPath.Substring(0, PDFFinalPath.Length-1);
					
					/*try 
					{ 
						DataSet DSTempDataSet = new DataSet();

						objDatawrapper.ClearParameteres();
						objDatawrapper.AddParameter("@Customer_Id" , CustomerID);
						objDatawrapper.AddParameter("@AppPol_Id",PolicyID);
						objDatawrapper.AddParameter("@AppPolVersion_Id",PolicyVersionID);
						objDatawrapper.AddParameter("@CalledFrom" , "POLICY");
						DSTempDataSet  = objDatawrapper.ExecuteDataSet("Proc_GetPDFAgencyCode");
						objDatawrapper.ClearParameteres();

						agency_id = DSTempDataSet.Tables[0].Rows[0]["AGENCY_ID"].ToString();
						objPrintJobsInfo.AGENCY_ID = int.Parse(agency_id); 
					} 
					catch(Exception ex) 
					{
						System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
						addInfo.Add("Err Descriptor ","Error while fetching PDF agency code while generating premium notice");
						addInfo.Add("CustomerID" ,CustomerID.ToString());
						addInfo.Add("PolicyID",PolicyID.ToString());
						ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex,addInfo );
					}*/

					objPrintJobsInfo.AGENCY_ID = objXml.AgencyID; 

					objPolicyProcess.AddPrintJobs(objPrintJobsInfo , objDatawrapper);

		
					//If this Policy has to be billed to Mortgagee  generate a notice for Mortgagee 
					if(objXml.BillMortgagee)
					{

						strNoticeXML = objXml.GetXMLForMortgagee(out MinDue , out TotalDue);
						objEbixAcordPDF.InputXml = strNoticeXML;
						PDFName = objEbixAcordPDF.GeneratePDF("CHK", CalledFrom );  
						PDFlink = PDFName + "<COMMON_PDF_URL=window.open(\"" + PDFFinalPath + PDFName + "\")>";

						TranLogMess = "Premium Notice for Mortgagee generated successfully";
						WriteTransactionLog(CustomerID , PolicyID , PolicyVersionID,TranLogMess,EODUserID , PDFlink, CalledFrom, "");
						objPrintJobsInfo.FILE_NAME = PDFName;
						objPolicyProcess.AddPrintJobs(objPrintJobsInfo , objDatawrapper);

					}

				}

				objDatawrapper.CommitTransaction(DataWrapper.CloseConnection.YES );
			}
			catch(Exception ex)
			{
				objDatawrapper.RollbackTransaction(DataWrapper.CloseConnection.YES );
				System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
				addInfo.Add("Err Descriptor ","Error while generating premium notice");
				addInfo.Add("CustomerID" ,CustomerID.ToString());
				addInfo.Add("PolicyID",PolicyID.ToString());
				ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex,addInfo );
				throw(ex);
			}
			finally
			{
				objDatawrapper.Dispose();
			}
			return PDFName;
		}

        public String GenerateCanceNoticePDF(int CustomerID, int PolicyID, int PolicyVersionID, DataWrapper objDataWrapper)
        {
            string CalledFrom = "POLICY";
            /******************************************************/
            //Added by Praveen Kumar 10/09/2010
            try
            {
                ClsCommonPdfXML objClsCommonPdf = new ClsCommonPdfXML();
                String GeneratedPDFName = "", PDFXml = "";
                DataSet DSTemp;
                // DSTemp = objClsCommonPdf.FetchCanellationNoticePdfXml(CustomerID.ToString(), PolicyID.ToString(), PolicyVersionID.ToString());
                base.objWrapper = objDataWrapper;

                objDataWrapper.ClearParameteres();

                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                DSTemp = objDataWrapper.ExecuteDataSet("Proc_GetCancellationNoticeDataforPdf");
                objDataWrapper.ClearParameteres();

                PDFXml = objClsCommonPdf.GenerateXMLDocumentdata(DSTemp);
                //PDFXml= DSTemp.GetXml().Replace("NewDataSet","CANCELLATION_NOTICE");
                //PDFXml = PDFXml.Replace("Table", "STATE_XML");
                String strLOB = "";
                ClsGenerateCancNotice objGenerateCanceNotice = new ClsGenerateCancNotice();
                String stateCode = new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().SetStateCode(CalledFrom, PolicyID, PolicyVersionID, CustomerID);

                try { strLOB = new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().GetPolicyLob("POLICY", PolicyID, PolicyVersionID, CustomerID); }
                catch (Exception) { }

                objGenerateCanceNotice.CarrierSystemID = CarrierSystemID;
                objGenerateCanceNotice.AgencyCode = ClsCommon.FetchValueFromXML("AGENCY_CODE", PDFXml);
                objGenerateCanceNotice.CustomerID = CustomerID;
                objGenerateCanceNotice.PolicyID = PolicyID;
                objGenerateCanceNotice.PolicyVersionID = PolicyVersionID;
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
            catch (Exception ex)
            { throw (ex); }
            //Added by Praveen Kumar 10/09/2010
            ///******************************************************/
        }
        public string GeneratePastDueNoticeForInsured(int CustomerID, int PolicyID, int PolicyVersionID, DataWrapper objDataWrapper)
		{
			string CalledFrom ="POLICY";
            //Added by Praveen kumar 10-09-2010 starts
         return   this.GenerateCanceNoticePDF(CustomerID, PolicyID, PolicyVersionID, objDataWrapper);
            //Added by Praveen kumar 10-09-2010 ends

            #region "This is previous code take fron wolverine this code is not use in Cancellation Notice Generation"
           
            base.objWrapper = objDataWrapper;

			objDataWrapper.ClearParameteres();

			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
			objDataWrapper.AddParameter("@POLICY_ID",PolicyID);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID);
			dsNonPayDB  =  objDataWrapper.ExecuteDataSet("Proc_GetCancellationNoticeNonPayDBData");
			objDataWrapper.ClearParameteres();
			

			mLobID  = Convert.ToInt32(dsNonPayDB.Tables[0].Rows[0]["LOB_ID"]);
			mAgencyCode = dsNonPayDB.Tables[0].Rows[0]["AGENCY_CODE"].ToString().Trim();
			string PDFlink ="" ,PDFName ="";
			string TranLogMess ="" , PDFFinalPath ;
			DateTime DueDate ;
			double MinDue , TotalDue , TotalPremiumDue;

			MinDue = double.Parse(dsNonPayDB.Tables[0].Rows[0]["MINIMUM_DUE"].ToString().Replace("$","").Replace(",","")); 
			TotalDue = double.Parse(dsNonPayDB.Tables[0].Rows[0]["TOTAL_DUE"].ToString().Replace("$","").Replace(",","")); 
			TotalPremiumDue = double.Parse(dsNonPayDB.Tables[0].Rows[0]["TOTAL_PREMIUM_DUE"].ToString().Replace("$","").Replace(",","")); 

			DueDate  = Convert.ToDateTime(dsNonPayDB.Tables[0].Rows[0]["DUE_DATE"]);
			
			if(mLobID == (int)BlCommon.ClsCommon.enumLOB.HOME  || mLobID == (int)BlCommon.ClsCommon.enumLOB.REDW )
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@CUSTOMERID",CustomerID);
				objDataWrapper.AddParameter("@POLID",PolicyID);
				objDataWrapper.AddParameter("@VERSIONID",PolicyVersionID);
				objDataWrapper.AddParameter("@CALLEDFROM","POLICY");
				DataSet DSLocDataSet  = objDataWrapper.ExecuteDataSet("Proc_GetPDFHomeowner_Dwelling_Details");
				objDataWrapper.ClearParameteres();

				if(DSLocDataSet != null && DSLocDataSet.Tables[0].Rows.Count > 0)
				{
					mPropertyAddress = DSLocDataSet.Tables[0].Rows[0]["LOC_ADDRESS"].ToString() + ", " + DSLocDataSet.Tables[0].Rows[0]["LOC_CITYSTATEZIP"].ToString();
				}
			}


			ClsCancNoticePdfXML objXml = new ClsCancNoticePdfXML(objDataWrapper);

			string strNoticeXML = objXml.GetNonPayDBXMLForInsured(dsNonPayDB , mPropertyAddress);

			AcordPDF.AcordXMLParser objEbixAcordPDF = new AcordPDF.AcordXMLParser();

			objEbixAcordPDF.ClientId = CustomerID;
			objEbixAcordPDF.PolicyId = PolicyID;
			objEbixAcordPDF.PolicyVersion = PolicyVersionID;
			
			//Change this (to optimise)
			ClsPolicyProcess objPolicyProcess = new ClsPolicyProcess();
			objPolicyProcess.BeginTransaction();
			objEbixAcordPDF.LobCode = objPolicyProcess.GetCancellationCode("ALL","ALL","Insured","NONPAYDB");
			objPolicyProcess.CommitTransaction();
					
			string strInputPath = InputBase  + "HOME\\IN\\" ;
			string strOutputPath = OutputPath + mAgencyCode  + "\\" + CustomerID.ToString() + "\\" + "POLICY\\CANC_NOTICE\\final";

			objEbixAcordPDF.InputXml = strNoticeXML;
			objEbixAcordPDF.InputPath = strInputPath;
			objEbixAcordPDF.OutputPath = strOutputPath;

			objEbixAcordPDF.ImpersonationUserId = ImpersonationUserId;
			objEbixAcordPDF.ImpersonationPassword = ImpersonationPassword;
			objEbixAcordPDF.ImpersonationDomain = ImpersonationDomain;
				
			PDFName = objEbixAcordPDF.GeneratePDF(CalledFrom,"DECPAGE"); 

			//Write Tran log 
			PDFFinalPath = FinalBasePath + mAgencyCode + "/" + CustomerID.ToString() + "/POLICY/CANC_NOTICE/final/";
			PDFlink = PDFName + "<COMMON_PDF_URL=window.open(\"" + PDFFinalPath + PDFName + "\")>";

			TranLogMess = "Cancellation Notice Non Payment DB PDF for Customer generated successfully";

           
			if(IsEODProcess)
			{
				WriteTransactionLog(CustomerID , PolicyID , PolicyVersionID,TranLogMess,EODUserID , PDFlink, CalledFrom, "",objDataWrapper);
			} 
			else
			{
				WriteTransactionLog(CustomerID , PolicyID , PolicyVersionID,TranLogMess, int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString()), PDFlink, CalledFrom, "",objDataWrapper);
			}

			//Create an entry in Customer Transaction History
			ClsCustBalanceInfo objCustBalanceInfo = new ClsCustBalanceInfo();
			objCustBalanceInfo.TRAN_DESC = "Non Pay DB cancellation notice sent";

			objCustBalanceInfo.CUSTOMER_ID = CustomerID ;
			objCustBalanceInfo.POLICY_ID = PolicyID;
			objCustBalanceInfo.POLICY_VERSION_ID = PolicyVersionID;
			objCustBalanceInfo.UPDATED_FROM = "T";
			objCustBalanceInfo.CREATED_DATE = DateTime.Now;
			objCustBalanceInfo.AMOUNT = TotalDue;
			objCustBalanceInfo.MIN_DUE = MinDue ;
			objCustBalanceInfo.TOTAL_PREMIUM_DUE = TotalPremiumDue ;
			objCustBalanceInfo.NOTICE_URL = PDFFinalPath + PDFName;
			objCustBalanceInfo.DUE_DATE = DueDate;
			objCustBalanceInfo.NOTICE_TYPE = "CANC_NOTICE";
			AddCustBalance(objCustBalanceInfo,objDataWrapper);

			return PDFName;

            #endregion
        }

        public string GeneratePastDueNoticeForAgency(int CustomerID, int PolicyID, int PolicyVersionID, DataWrapper objDataWrapper)
		{
			string CalledFrom ="POLICY";

            /******************************************************/
            //Added by Praveen Kumar 10/09/2010 starts

            return this.GenerateCanceNoticePDF(CustomerID, PolicyID, PolicyVersionID, objDataWrapper);
            
            //Added by Praveen Kumar 10/09/2010 ends
            ///******************************************************/
           
            #region "This is previous code take fron wolverine this code is not use in Cancellation Notice Generation"
		
           string PDFlink ="" ,PDFName ="";
			string TranLogMess ="" , PDFFinalPath ;
		
			ClsCancNoticePdfXML objXml = new ClsCancNoticePdfXML(objDataWrapper);

			string strNoticeXML = objXml.GetNonPayDBXMLForAgent(dsNonPayDB , mPropertyAddress);

			AcordPDF.AcordXMLParser objEbixAcordPDF = new AcordPDF.AcordXMLParser();

			objEbixAcordPDF.ClientId = CustomerID;
			objEbixAcordPDF.PolicyId = PolicyID;
			objEbixAcordPDF.PolicyVersion = PolicyVersionID;
			
			//Change this (to optimise)
			ClsPolicyProcess objPolicyProcess = new ClsPolicyProcess();
			objPolicyProcess.BeginTransaction();
			objEbixAcordPDF.LobCode = objPolicyProcess.GetCancellationCode("ALL","ALL","Agent","NONPAYDB");
			objPolicyProcess.CommitTransaction();
					
			string strInputPath = InputBase  + "HOME\\IN\\" ;
			string strOutputPath = OutputPath + mAgencyCode  + "\\" + CustomerID.ToString() + "\\" + "POLICY\\CANC_NOTICE\\final";

			objEbixAcordPDF.InputXml = strNoticeXML;
			objEbixAcordPDF.InputPath = strInputPath;
			objEbixAcordPDF.OutputPath = strOutputPath;

			objEbixAcordPDF.ImpersonationUserId = ImpersonationUserId;
			objEbixAcordPDF.ImpersonationPassword = ImpersonationPassword;
			objEbixAcordPDF.ImpersonationDomain = ImpersonationDomain;
				
			PDFName = objEbixAcordPDF.GeneratePDF(CalledFrom,"DECPAGE"); 

			//Write Tran log 
			PDFFinalPath = FinalBasePath + mAgencyCode + "/" + CustomerID.ToString() + "/POLICY/CANC_NOTICE/final/";
			PDFlink = PDFName + "<COMMON_PDF_URL=window.open(\"" + PDFFinalPath + PDFName + "\")>";

			TranLogMess = "Cancellation Notice Non Payment DB PDF for Agency generated successfully";

			if(IsEODProcess)
			{
				WriteTransactionLog(CustomerID , PolicyID , PolicyVersionID,TranLogMess,EODUserID , PDFlink, CalledFrom, "",objDataWrapper);
			} 
			else
			{
				WriteTransactionLog(CustomerID , PolicyID , PolicyVersionID,TranLogMess, int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString()), PDFlink, CalledFrom, "",objDataWrapper);
			}

			return PDFName;
            #endregion
        }

        public string GeneratePastDueNoticeForAdditionalInterests(int CustomerID, int PolicyID, int PolicyVersionID, DataWrapper objDataWrapper)
		{

			string CalledFrom ="POLICY";


            /******************************************************/
            //Added by Praveen Kumar 10/09/2010 starts

            return this.GenerateCanceNoticePDF(CustomerID, PolicyID, PolicyVersionID, objDataWrapper);

            //Added by Praveen Kumar 10/09/2010 ends
            ///******************************************************/

            #region "This is previous code take fron wolverine this code is not use in Cancellation Notice Generation"

			string PDFlink ="" ,PDFName ="", arrPDFNames = "", AdditionalInfo = "";
			string TranLogMess ="" , PDFFinalPath ,strDocCode="";
		
			string strNoticeXML ; 
			bool AddWordings = false;

			ClsCancNoticePdfXML objXml = new ClsCancNoticePdfXML(objDataWrapper);
			AcordPDF.AcordXMLParser objEbixAcordPDF = new AcordPDF.AcordXMLParser();

			objEbixAcordPDF.ClientId = CustomerID;
			objEbixAcordPDF.PolicyId = PolicyID;
			objEbixAcordPDF.PolicyVersion = PolicyVersionID;
			
			//Change this (to optimise)
			ClsPolicyProcess objPolicyProcess = new ClsPolicyProcess();
			objPolicyProcess.BeginTransaction();
			//objEbixAcordPDF.LobCode = objPolicyProcess.GetCancellationCode("ALL","ALL","ADDLINT","NONPAYDB");
			strDocCode = objPolicyProcess.GetCancellationCode("ALL","ALL","ADDLINT","NONPAYDB");
			objPolicyProcess.CommitTransaction();
					
			string strInputPath = InputBase  + "HOME\\IN\\" ;
			string strOutputPath = OutputPath + mAgencyCode  + "\\" + CustomerID.ToString() + "\\" + "POLICY\\CANC_NOTICE\\final";

			
			objEbixAcordPDF.InputPath = strInputPath;
			objEbixAcordPDF.OutputPath = strOutputPath;

			objEbixAcordPDF.ImpersonationUserId = ImpersonationUserId;
			objEbixAcordPDF.ImpersonationPassword = ImpersonationPassword;
			objEbixAcordPDF.ImpersonationDomain = ImpersonationDomain;

			PDFFinalPath = FinalBasePath +  mAgencyCode + "/" + CustomerID.ToString() + "/POLICY/CANC_NOTICE/final/";

			string Name , Address1, Address2 ="";
			
			int AddIntID = 0, NatureOfAdint =0;

			string StoreProc = "";
			objDataWrapper.ClearParameteres();

			//Fetch AdditionalInterssts
	
			objDataWrapper.AddParameter("@CUSTOMERID",CustomerID );
			objDataWrapper.AddParameter("@POLID",PolicyID);
			objDataWrapper.AddParameter("@VERSIONID",PolicyVersionID);

			DataSet dsAddInt=null;
			if(mLobID == (int)BlCommon.ClsCommon.enumLOB.HOME || mLobID == (int)BlCommon.ClsCommon.enumLOB.REDW )
			{
				StoreProc = "Proc_GetPDFHomeowner_Additional_Interest";
				objDataWrapper.AddParameter("@DWELLINGID",0);
			}
			else if(mLobID == (int)BlCommon.ClsCommon.enumLOB.AUTOP || mLobID == (int)BlCommon.ClsCommon.enumLOB.CYCL )
			{
				StoreProc = "PROC_GETPDF_AUTO_ADDITIONAL_INT_DETAILS";
				objDataWrapper.AddParameter("@VEHICLEID",0);
			}
			else if(mLobID == (int)BlCommon.ClsCommon.enumLOB.BOAT )
			{
				StoreProc = "PROC_GETPDF_BOAT_ADDITIONAL_INT_DETAILS";
				objDataWrapper.AddParameter("@BOATID",0);
			}
			objDataWrapper.AddParameter("@CALLEDFROM","POLICY");
		
			dsAddInt  = objDataWrapper.ExecuteDataSet(StoreProc);
			
			objDataWrapper.ClearParameteres();

			for(AddIntID = 0 ; AddIntID < dsAddInt.Tables[0].Rows.Count ; AddIntID++)
			{
				objEbixAcordPDF.LobCode=strDocCode;
				AddWordings = false;
				Name = dsAddInt.Tables[0].Rows[AddIntID]["HOLDER_NAME"].ToString();
				Address1 = dsAddInt.Tables[0].Rows[AddIntID]["ADDRESS"].ToString();
					
				AdditionalInfo = ";Holder Name: " + Name ;
				if(mLobID == (int)BlCommon.ClsCommon.enumLOB.HOME || mLobID == (int)BlCommon.ClsCommon.enumLOB.REDW )
				{
					Address2 = dsAddInt.Tables[0].Rows[AddIntID]["HOLDERCITYSTATEZIP"].ToString();
					NatureOfAdint = Convert.ToInt32(dsAddInt.Tables[0].Rows[AddIntID]["ADD_INT_TYPE"]);

					if(NatureOfAdint == LienHolder)
					{
						AddWordings = true; 
					}
				}
				else if(mLobID == (int)BlCommon.ClsCommon.enumLOB.AUTOP  || mLobID == (int)BlCommon.ClsCommon.enumLOB.CYCL )
				{
					Address2 = dsAddInt.Tables[0].Rows[AddIntID]["CITYSTATEZIP"].ToString();
				}
				else if(mLobID == (int)BlCommon.ClsCommon.enumLOB.BOAT )
				{
					Address2 = dsAddInt.Tables[0].Rows[AddIntID]["CITYSTATEZIP"].ToString();
				}

				//iTrack 3995(28) always add additional wordings to lien holder's copy
				AddWordings = true; 

				if(Address1.Trim().EndsWith(","))
					Address1 = Address1.Substring(0, Address1.LastIndexOf(","));
				// changed by Pravesh on 14 april for Add Int File Name
				//objEbixAcordPDF.AddInt = AddIntID + 1;
				if(mLobID == (int)BlCommon.ClsCommon.enumLOB.HOME || mLobID == (int)BlCommon.ClsCommon.enumLOB.REDW )
				{
					objEbixAcordPDF.VehicleDwellingBoat =  int.Parse(dsAddInt.Tables[0].Rows[AddIntID]["DWELLING_ID"].ToString());
				}
				else if(mLobID == (int)BlCommon.ClsCommon.enumLOB.AUTOP || mLobID == (int)BlCommon.ClsCommon.enumLOB.CYCL )
				{
					objEbixAcordPDF.VehicleDwellingBoat =  int.Parse(dsAddInt.Tables[0].Rows[AddIntID]["VEHICLE_ID"].ToString());
				}
				else if(mLobID == (int)BlCommon.ClsCommon.enumLOB.BOAT )
				{
					objEbixAcordPDF.VehicleDwellingBoat =  int.Parse(dsAddInt.Tables[0].Rows[AddIntID]["BOAT_ID"].ToString());
				}
				objEbixAcordPDF.LobCode +=  "_" +  dsAddInt.Tables[0].Rows[AddIntID]["ADD_INT_NAME"].ToString();
				objEbixAcordPDF.AddInt = int.Parse(dsAddInt.Tables[0].Rows[AddIntID]["ADD_INT_ID"].ToString()) ;
				//end here
				strNoticeXML = objXml.GetNonPayDBXMLForAddIntrest(dsNonPayDB,Name , Address1 , Address2 , mPropertyAddress,AddWordings );

				objEbixAcordPDF.InputXml = strNoticeXML;

				PDFName = objEbixAcordPDF.GeneratePDF(CalledFrom,"CANC_NOTICE"); 

				//Write Tran log 
				PDFlink = PDFName + "<COMMON_PDF_URL=window.open(\"" + PDFFinalPath + PDFName + "\")>";

				TranLogMess = "Cancellation Notice Non Payment DB PDF for Additional Interest generated successfully";

				if(IsEODProcess)
				{
					WriteTransactionLog(CustomerID , PolicyID , PolicyVersionID,TranLogMess,EODUserID , PDFlink, CalledFrom, AdditionalInfo,objDataWrapper);
				} 
				else
				{
					WriteTransactionLog(CustomerID , PolicyID , PolicyVersionID,TranLogMess, int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString()), PDFlink, CalledFrom, AdditionalInfo,objDataWrapper);
				}
				if(AddIntID == 0)
				{
					arrPDFNames = PDFName;
				}
				else
				{
					arrPDFNames = arrPDFNames + "~"  + PDFName ; 
				}
			}
			return arrPDFNames;
            #endregion

        }
     }
}