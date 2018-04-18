using System;
using Cms.Model.Policy.Process;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;
using System.Data.SqlClient;
using System.Data;
using Cms.BusinessLayer.BlQuote;
using Cms.BusinessLayer.BlApplication;
using System.Xml;
using System.Collections;
using System.Text;
using System.IO;
using Cms.Model.Diary;
using System.Globalization;


namespace Cms.BusinessLayer.BlProcess
{
    /// <summary>
    /// Summary description for ClsPolicyProcess.
    /// </summary>
    /// 

    #region Cancellation Method Enum
    public enum CancellationMethod
    {
        ProRata = 11994,
        Flat = 11995,
        Equity = 11996,
        NotApplicable = 13028
    }
    #endregion

    #region Cancellation Reasons
    public enum CancellationReason
    {
        LapseofCoverage = 11529,
        InvalidDriversLicense = 11530,
        MajorViolation = 11531,
        Points = 11532,
        FailureToVolunteerAllInformation = 11534,
        UndisclosedDriverInformation = 11535,
        FailureToRespondToRequestedUnderwritingInformation = 11537,
        InsuredMovedOutOfState = 11540,
        NonRenewed_CancelDueToLosses = 11541,
        PropertySold = 11543,
        PropertyVacant = 11544,
        Rescission = 11545,
        RewrittenToAnotherPolicyWithinCompany = 11546,
        OtherCompanyRequestedReason = 11548,
        CancelledAtInsuredRequest = 11549,
        ProRateCancelledForNonPayment = 11550,
        EquityCancelledForNonPayment = 11551,
        CancelledFlatForNonPayment = 11552,
        CancelledForNonPayNSFCheck = 11553,
        CorrectionWithinCompany = 11554,
        Other = 11560
    }

    #endregion

    #region Entity definition for Cancellation Action rule
    public class ClsCancellationRule
    {

        private string strActUnpaidPrem;
        private string strActPaidPrem;
        private string strBillType;

        public string BILL_TYPE
        {
            get
            {
                return strBillType;
            }
            set
            {
                strBillType = value;
            }
        }

        public string ACTION_UNPAID_PREMIUM
        {
            get
            {
                return strActUnpaidPrem;
            }
            set
            {
                strActUnpaidPrem = value;
            }
        }

        public string ACTION_RETURN_PREMIUM
        {
            get
            {
                return strActPaidPrem;
            }
            set
            {
                strActPaidPrem = value;
            }
        }

    }
    #endregion

    public class ClsPolicyErrMsg
    {
        public static string strMessage = "Process has not completed successfully";
        public static string strNonRenewFlag = "False";
    }

    public class ClsPolicyProcess : clsprocess
    {

        private const string CancellationRuleXML = "/cmsweb/support/CancellationActionRule.xml";
        public const string CommissionRuleXML = "/cmsweb/support/AgencyCommissionRule.xml";

        #region Const defination for different processes
        public const int POLICY_CANCELLATION_PROCESS = 2;

        public const int POLICY_ENDORSEMENT_PROCESS = 3;
        public const int POLICY_COMMIT_ENDORSEMENT_PROCESS = 14;
        public const int POLICY_ROLLBACK_ENDORSEMENT_PROCESS = 15;
        public const int POLICY_SUSPENSE_ENDORSEMENT_PROCESS = 34;

        public const int POLICY_REINSTATEMENT_PROCESS = 4;

        public const int POLICY_NON_RENEWAL_PROCESS = 6;
        public const int POLICY_COMMIT_NON_RENEWAL_PROCESS = 20;
        public const int POLICY_ROLLBACK_NON_RENEWAL_PROCESS = 21;

        public const int POLICY_NEGATE_PROCESS = 7;

        public const int POLICY_NEW_BUSINESS_PROCESS = 24;
        public const int POLICY_COMMIT_NEW_BUSINESS_PROCESS = 25;
        public const int POLICY_ROLLBACK_NEW_BUSINESS_PROCESS = 26;
        public const int POLICY_DECLINE = 27;

        public const int POLICY_COMMIT_CANCELLATION_PROCESS = 12;
        public const int POLICY_ROLLBACK_CANCELLATION_PROCESS = 13;

        public const int POLICY_COMMIT_REINSTATEMENT_PROCESS = 16;
        public const int POLICY_ROLLBACK_REINSTATEMENT_PROCESS = 17;

        public const int POLICY_RENEWAL_PROCESS = 5;
        public const int POLICY_COMMIT_RENEWAL_PROCESS = 18;
        public const int POLICY_ROLLBACK_RENEWAL_PROCESS = 19;

        public const int POLICY_CORRECTIVE_USER_PROCESS = 8;
        public const int POLICY_COMMIT_CORRECTIVE_USER_PROCESS = 9;
        public const int POLICY_ROLLBACK_CORRECTIVE_USER_PROCESS = 10;

        public const int POLICY_RESCIND = 28;
        public const int POLICY_COMMIT_RESCIND_PROCESS = 29;
        public const int POLICY_ROLLBACK_RESCIND_PROCESS = 30;

        public const int POLICY_REWRITE_PROCESS = 31;
        public const int POLICY_COMMIT_REWRITE_PROCESS = 32;
        public const int POLICY_ROLLBACK_REWRITE_PROCESS = 33;

        public const int POLICY_REVERT_PROCESS = 35;
        public const int POLICY_COMMIT_REVERT_PROCESS = 37;
        public const int POLICY_ROLLBACK_REVERT_PROCESS = 36;

        public static string NoWordingPDFFileName = "";
        public string WordingPDFFileName = "";
        public string AgentWordingPDFFileName = "";
        public string AdditionalIntrstPDFFileName = "";
        public string AcordPDFFileName = "";
        public string AutoIdCardPDFFileName = "";

        public const string PDF_DEC_PAGE = "DECPAGE";
        public const string PDF_ACORD = "ACORD";

        #endregion

        #region Const declaration for process status
        public const string PROCESS_STATUS_PENDING = "PENDING";
        public const string PROCESS_STATUS_COMPLETE = "COMPLETE";
        public const string PROCESS_STATUS_ROLLBACK = "ROLLBACK";
        #endregion

        #region Const decclaration for policy status
        //public const string POLICY_STATUS_NORMAL = "NORMAL";
        //public const string POLICY_STATUS_RENEWED = "RENEWED";
        //public const string POLICY_STATUS_INACTIVE = "INACTIVE";

        //public const string POLICY_STATUS_UNDER_ENDORSEMENT = "UENDRS";
        //public const string POLICY_STATUS_UNDER_RENEW = "URENEW";
        //public const string POLICY_STATUS_UNDER_CORRECTIVE_USER = "UCORUSER";
        //public const string POLICY_STATUS_UNDER_ISSUE = "UISSUE";
        //public const string POLICY_STATUS_SUSPENDED = "SUSPENDED";
        public const string POLICY_STATUS_RENEWAL_SUSPENSE = "RSUSPENSE";
        //public const string POLICY_STATUS_UNDER_REWRITE = "UREWRITE";
        public const string POLICY_STATUS_SUSPENSE_ENDORSEMENT = "ESUSPENSE";
        //public const string POLICY_STATUS_SUSPENSE_REWRITE = "REWRTSUSP";
        public const string POLICY_STATUS_UNDER_REVERT = "UREVERT";
        public const string POLICY_STATUS_SUSPENSE_CANCEL = "SCANCEL";
        public const string POLICY_STATUS_UNDER_CANCEL = "UCANCL";
        public const string POLICY_STATUS_CANCELLED = "CANCEL";
        public const string POLICY_STATUS_MARKED_REINSTATE = "MREINSTATE";
        public const string POLICY_STATUS_UNDER_REINSTATE = "UREINST";
        public const string POLICY_STATUS_MARKED_NONRENEW = "MNONRENEWED";
        public const string POLICY_STATUS_EXPIRED = "EXPIRED";

        public const string POLICY_STATUS_SYS_GENRATED_END = "AUTOUENDRS";

        #endregion

        #region Const declaration for PRINT_JOBS
        //public const string PRINT_JOBS_DOCUMENT_CODE = "DEC_PAGE";		
        public const string PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE = "DEC_PAGE";
        public const string PRINT_JOBS_DOCUMENT_CODE_ACORD = "ACORD";
        public const string PRINT_JOBS_DOCUMENT_CODE_CANC_NOTICE = "POLICY/CANC_NOTICE";
        public const string PRINT_JOBS_DOCUMENT_CODE_REINSTATE_NOTICE = "REINSTATE_NOTICE";
        public const string PRINT_JOBS_URL_PATH = "/cms/Upload/OUTPUTPDFs/";
        public const string PRINT_JOBS_ON_DEMAND_FLAG = "N";

        #region Const declaration for PRINT_JOBS Entity Types
        public const string PRINT_JOBS_ENTITY_TYPE_CUSTOMER = "CUSTOMER";
        public const string PRINT_JOBS_ENTITY_TYPE_CUSTOMER_NOWORD = "CUSTOMER-NOWORD";
        public const string PRINT_JOBS_ENTITY_TYPE_AGENCY = "AGENCY";
        public const string PRINT_JOBS_ENTITY_TYPE_ADDL_INTEREST = "ADDL_INT";
        public const string PRINT_JOBS_ENTITY_TYPE_COMPANY = "COMPANY";
        public const string PRINT_JOBS_ENTITY_TYPE_CERT_MAIL = "CERT_MAIL";
        #endregion

        #region Const declaration for PRINT_JOBS Document Types
        public const string PRINT_JOBS_DEC_PAGE = "DEC_PAGE";
        public const string PRINT_JOBS_ACORD = "ACORD";
        //Flag to indicate whether a printing error has occured or not
        public static bool PrintingErrorFlag = false;

        #endregion

        #region Const Declaration for same term/other term
        public const string POLICY_SAME_TERM = "SAME_TERM";
        public const string POLICY_OTHER_TERM = "OTHER_TERM";
        #endregion
        #endregion

        #region enum declaration for Process Cancellation Types

        public struct enumPOLICY_TRANSACTION_TYPE
        {
            public const int OPEN_POLICY = 14560;
            public const int ADJUSTABLE_POLICY = 14561;
            public const int FRONTING_PREMIUM_POLICY = 14679;
            public const int SINGLE_POLICY = 14559;

        }
        public struct enumCO_INSURANCE
        {
            public const int DIRECT = 14547;
            public const int FOLLOWER = 14549;
            public const int LEADER = 14548;

        }
        public enum enumPROCESS_CANCELLATION_TYPES
        {
            NON_PAYMENT = 11969,
            RESCINDED = 11970,
            CANCEL_REINSTATEMENT = 11971,
            AGENTS_REQUEST = 11987,
            CANCEL_REWRITE = 11988,
            INSURED_REQUEST = 11989,
            COMPANY_REQUEST = 11990,
            NON_RENEWAL = 11991,
            NSF_REPLACE = 11992,
            NSF_NO_REPLACEMENT = 11993,
            AGENCY_TERMINATED_NOTIFICATION = 13026,
            AGENCY_TERMINATED_NO_NOTIFICATION = 13027
        }
        #endregion


        #region enum declaration for Agency Change Form
        public enum enumAGENCY_CHANGE_FORM_TYPES
        {
            CHANGE_IN_FORM = 13029,
            NO_CHANGE_IN_FORM = 13030
        }
        #endregion


        #region  enum declaration for process cancellation PDF type
        public enum enumPROCESS_CANCELLATION_PDF_TYPES
        {
            CANC_NOTICE = 0,
            CANC_NOTICE_FINAL = 1,
            CANC_NOTICE_FINAL_NONPAY = 2,
            CANC_NOTICE_FINAL_NONPAYDB = 3,
            CANC_NOTICE_FINAL_NSF = 4,

            CANC_NOTICE_TEMP = 5,
            CANC_NOTICE_TEMP_NONPAY = 6,
            CANC_NOTICE_TEMP_NONPAYDB = 7,
            CANC_NOTICE_TEMP_NSF = 8,
            CANC_NOTICE_TEMP_INSREQ = 9,
            CANC_NOTICE_FINAL_ADDLINT = 10,
            CANC_NOTICE_FINAL_ADDLINT_INSREQ = 11,
            REINS_NOTICE_FINAL_ADDLINT = 12,
            CANC_NOTICE_FINAL_ADDLINT_CERTMAIL = 13,
            CANC_NOTICE_FINAL_NONPAYDB_ADDLINT = 14,
            CANC_NOTICE_FINAL_NONPAYDBMEMO_ADDLINT = 15,
            CANC_NOTICE_FINAL_NONPAYDBMEMO = 16,
            CANC_NOTICE_FINAL_INSREQ = 17,
            CANC_NOTICE_FINAL_CERTMAIL = 18,
            UMB_LETTER = 19,
            CANC_NOTICE_FINAL_NSF_NOREPLACE = 20,
            CANC_NOTICE_FINAL_AGENT = 21

        }
        #endregion

        public System.Text.StringBuilder TransactionDescription = new System.Text.StringBuilder();
        public System.Text.StringBuilder TransactionChangeXML = new System.Text.StringBuilder();
        public DataWrapper objWrapper;
        protected System.Xml.XmlDocument RuleDoc;

        #region Database Transaction function
        public void BeginTransaction()
        {
            if (objWrapper == null)
                objWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
        }

        public void CommitTransaction()
        {
            objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
        }

        public void RollbackTransaction()
        {
            objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
        }
        #endregion

        protected virtual bool OnSetPolicyStatus()
        {
            return true;
        }

        protected virtual bool OnCheckProcessEligibility()
        {
            return true;
        }

        protected virtual bool OnWriteTransactionLog()
        {
            return true;
        }

        public ClsPolicyProcess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Common Process virtual function

        /// <summary>
        /// Starts the specified process, passed in model object
        /// </summary>
        /// <param name="objProcessInfo">Process info into model</param>
        /// <returns>True if sucessfull else false</returns>
        public virtual bool StartProcess(ClsProcessInfo objProcessInfo)
        {

            try
            {
                //MakeEntryforEOD();
                //Checking the Policy Process Elegibility.
                int returnResult = 0;
                if (OnCheckProcessEligibility())
                {
                    returnResult = CheckProcessEligibility(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.PROCESS_ID);

                    if (returnResult != 1)
                        return false;	//Policy not valid for the specified process
                }

                //At start process status should be pending
                objProcessInfo.PROCESS_STATUS = PROCESS_STATUS_PENDING;

                //Adding the record into policy process table (POL_POLICY_PROCESS)
                AddProcessInformation(objProcessInfo);
                TransactionDescription.Append("\n" + FetchGeneralMessage("1124", ";")); //Process has been added in process log.;");

                //Added by Anurag Verma on 19/10/2006 for adding entry in diary entry.
                #region DIARY ENTRY
                //finding underwriter id for sending diary entry as TOUSERID 


                Cms.BusinessLayer.BlCommon.ClsDiary objClsDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
                Cms.Model.Diary.TodolistInfo objModel = new Cms.Model.Diary.TodolistInfo();

                int DiaryListTypeId;
                string strDiaryDesc = GetTransactionLogDesc(objProcessInfo.PROCESS_ID, out DiaryListTypeId, objProcessInfo.CANCELLATION_TYPE);

                //Commented by Anurag Verma on 20/03/2007 as these properties are removed
                //objModel.POLICYCLIENTID = objProcessInfo.CUSTOMER_ID;
                objModel.PROCESS_ROW_ID = objProcessInfo.ROW_ID;

                objModel.LISTTYPEID = DiaryListTypeId;
                objModel.POLICY_ID = objProcessInfo.POLICY_ID;
                objModel.POLICY_VERSION_ID = objProcessInfo.POLICY_VERSION_ID;
                objModel.CUSTOMER_ID = objProcessInfo.CUSTOMER_ID;
                objModel.FROMUSERID = objModel.CREATED_BY = objProcessInfo.CREATED_BY;
                objModel.NOTE = objModel.SUBJECTLINE = strDiaryDesc;
                objModel.CREATED_DATETIME = DateTime.Now;
                objModel.RECDATE = DateTime.Now;
                objModel.LISTOPEN = "Y";
                objModel.MODULE_ID = (int)ClsDiary.enumModuleMaster.POLICY_PROCESS;
                objModel.LOB_ID = objProcessInfo.LOB_ID;

                ///Commented by Anurag Verma on 12-03-2006 for checking new diary object
                #region OLD CODE BEFORE DIARY OBJECT---DO NOT DELETE
                //objModel.TOUSERID = objProcessInfo.UNDERWRITER;
                //objModel.FOLLOWUPDATE = DateTime.Now;    
                //objModel.LAST_UPDATED_DATETIME = DateTime.Now;
                //objModel.RECDATE = DateTime.Now;					
                //objModel.STARTTIME = (System.DateTime)DateTime.Now;
                //objModel.ENDTIME = (System.DateTime)DateTime.Now;							
                //objModel.NOTE = objProcessInfo.DIARY_NOTE;
                #endregion

                //int listid=0;			
                //listid=objClsDiary.AddPolicyEntry(objModel); 
                objClsDiary.DiaryEntryfromSetup(objModel, objWrapper);
                #endregion




                if (OnSetPolicyStatus() == true)
                {
                    //Sets the Policy Status on which the process has been launched.
                    string PolicyStatusDesc, NewPolicyStatus;
                    SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.PROCESS_ID, out PolicyStatusDesc, out NewPolicyStatus);
                    TransactionDescription.Append("\n" + FetchGeneralMessage("1339", ";") + "" + PolicyStatusDesc + ".;");
                }

                //Transaction log entry will be done.
                if (OnWriteTransactionLog())
                {
                    WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID
                        , GetTransactionLogDesc(objProcessInfo.PROCESS_ID), objProcessInfo.CREATED_BY, TransactionDescription.ToString());
                }
                //Added by Anurag Verma on 12/10/2006 for application version marking for deletion
                //MarkforDeletion(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID,"Y");

                return true;
            }
            catch (Exception objExp)
            {
                throw (new Exception(FetchGeneralMessage("1338", ";") + "\n" + objExp.Message));
            }
        }

        /// <summary>
        /// Commits the process specified in model object
        /// </summary>
        /// <param name="objProcessInfo">Process information</param>
        /// <returns>True if sucessfull else false</returns>
        //Overloaded virtual function
        public virtual bool CommitProcess(ClsProcessInfo objProcessInfo)
        {
            return CommitProcess(objProcessInfo, "");
        }

        public virtual bool CommitProcess(ClsProcessInfo objProcessInfo, string strCalledFrom)
        {
            try
            {
                int returnResult = 0;

                if (OnCheckProcessEligibility())
                {
                    returnResult = CheckProcessEligibility(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.PROCESS_ID);

                    if (returnResult != 1)
                    {
                        TransactionDescription.Append("\n " + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1376", "") + ";");//Process eligibility is false.;");
                        return false;	//Policy not valid for the specified process
                    }
                    else
                    {
                        TransactionDescription.Append("\n" + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1377", "") + ";");// Process eligibility is true.;");
                    }

                }

                //Get and Set the diary entry for this process
                //				objProcessInfo.DIARY_LIST_ID = GetDiaryListID(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID,objProcessInfo.ROW_ID);
                //				if (objProcessInfo.DIARY_LIST_ID > 0)
                //				{
                //					int iResult = 0;
                //					iResult = SetDiaryEntryStatus(objProcessInfo.DIARY_LIST_ID, "N");
                //					if (iResult > 0)
                //					{
                //						//TransactionDescription.Append("\n Update the Diary Entry Status.;");
                //						TransactionDescription.Append("\n Diary entry status has been updated.;");
                //					}
                //				}
                int iResult = 0, iResult1 = 0;
                iResult = SetDiaryEntryStatus(objProcessInfo, "N");
                if (strCalledFrom == "NEWVERSION")
                    iResult1 = SetDiaryEntryStatus(objProcessInfo, "N", strCalledFrom);
                if ((iResult > 0 || iResult1 > 0) && objProcessInfo.PROCESS_ID != POLICY_COMMIT_CORRECTIVE_USER_PROCESS)
                    TransactionDescription.Append(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1378", ""));//Diary entry status has been marked completed.;");


                if (OnSetPolicyStatus())
                {
                    //Updating the status of policy on which process has been launched.
                    string PolicyStatusDesc, NewPolicyStatus;
                    SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.PROCESS_ID, out PolicyStatusDesc, out NewPolicyStatus);
                    TransactionDescription.Append("\n " + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1339", "") + PolicyStatusDesc + ".;");
                    if (NewPolicyStatus != "")
                        objProcessInfo.POLICY_CURRENT_STATUS = NewPolicyStatus;
                }

                //Update the Process table Information
                int result = UpdateProcessInformation(objProcessInfo);

                //Sets the Process Status on which the process has been launched.
                SetProcessStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.ROW_ID, PROCESS_STATUS_COMPLETE);
                TransactionDescription.Append(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1379", ""));//Process Status has been updated.;");

                if (OnWriteTransactionLog())
                {
                    //Updating the transaction log
                    WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID,
                        GetTransactionLogDesc(objProcessInfo.PROCESS_ID), objProcessInfo.COMPLETED_BY, TransactionDescription.ToString());
                }

                return true;
            }
            catch (Exception objExp)
            {
                throw (new Exception("Error occured while committing process \n" + objExp.Message));
            }
        }


        /// <summary>
        /// Rollback the process specified in model object
        /// </summary>
        /// <param name="objProcessInfo">Process information</param>
        /// <returns>True if sucessfull else false</returns>
        public virtual bool RollbackProcess(ClsProcessInfo objProcessInfo)
        {
            return RollbackProcess(objProcessInfo, "");
        }
        public virtual bool RollbackProcess(ClsProcessInfo objProcessInfo, string strCalledFor)
        {
            try
            {
                //Checking the Policy Process Elegibility.
                int returnResult = 0;

                if (OnCheckProcessEligibility())
                {
                    returnResult = CheckProcessEligibility(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.PROCESS_ID);

                    if (returnResult != 1)
                    {
                        TransactionDescription.Append("\n" + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1376", "") + ";");//Process eligibility is false.
                        return false;	//Policy not valid for the specified process
                    }
                    else
                    {
                        TransactionDescription.Append("\n " + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1377", "") + ";");//Process eligibility is true.
                    }
                }

                //Get and Set the diary entry for this process
                //				objProcessInfo.DIARY_LIST_ID = GetDiaryListID(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID,objProcessInfo.ROW_ID);
                //				SetDiaryEntryStatus(objProcessInfo.DIARY_LIST_ID, "N");
                int iResult = 0;
                iResult = SetDiaryEntryStatus(objProcessInfo, "N");
                if (strCalledFor == "NEWVERSION")
                    iResult = SetDiaryEntryStatus(objProcessInfo, "N", strCalledFor);
                //TransactionDescription.Append("\n Update the Diary Entry Status.;");
                if (iResult > 0)
                    TransactionDescription.Append("\n" + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1919", "") + ";");//Diary entry status has been updated.

                if (OnSetPolicyStatus() == true)
                {
                    //Updating the status of policy on which process has been launched.
                    string PolicyStatusDesc, NewPolicyStatus;
                    SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.PROCESS_ID, out PolicyStatusDesc, out NewPolicyStatus);
                    TransactionDescription.Append("\n  " + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1488", "") + PolicyStatusDesc + ".;");//Policy Status has been updated to
                }

                //Update the Process table Information
                int result = UpdateProcessInformation(objProcessInfo);

                //Sets the Policy Status on which the process has been launched.
                SetProcessStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.ROW_ID, PROCESS_STATUS_ROLLBACK);
                TransactionDescription.Append("\n " + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1918", "") + ";");//("\n Process Status has been updated.;");

                if (OnWriteTransactionLog())
                {
                    //Updating the transaction log
                    WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID,
                        GetTransactionLogDesc(objProcessInfo.PROCESS_ID), objProcessInfo.COMPLETED_BY, TransactionDescription.ToString());
                }

                return true;
            }
            catch (Exception objExp)
            {
                throw (new Exception("Error occured while rollbacking process \n" + objExp.Message));
            }

        }
        #endregion
        #region Process Header Functions
        /// <summary>
        /// Get the Process Header Details
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns></returns>
        public DataSet GetProcessHeaderDetails(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            DataSet dsProcess = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
            objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
            dsProcess = objDataWrapper.ExecuteDataSet("Proc_GetProcessHeaderDetails");

            if (dsProcess.Tables[0].Rows.Count > 0)
            {
                return dsProcess;
            }
            else
            {
                return null;
            }

        }
        #endregion
        #region "Process table related function

        public void getAdditionalInterest(ClsProcessInfo objProcessInfo, DataWrapper objDataWrapper)
        {
            objDataWrapper.ClearParameteres();
            DataTable dtAddIntList = null;
            Cms.BusinessLayer.BlApplication.ClsAdditionalInterest objAdditionalInterest = new Cms.BusinessLayer.BlApplication.ClsAdditionalInterest();

            dtAddIntList = objAdditionalInterest.GetAdditionalInterestList(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.LOB_ID, objDataWrapper);
            if (dtAddIntList.Rows.Count > 0)
            {
                objProcessInfo.ADD_INT_ID = "";
                for (int i = 0; i < dtAddIntList.Rows.Count; i++)
                {
                    objProcessInfo.ADD_INT_ID = objProcessInfo.ADD_INT_ID + dtAddIntList.Rows[i]["ADD_INT_ID"].ToString() + "~";

                }
                objProcessInfo.ADD_INT_ID = objProcessInfo.ADD_INT_ID.TrimEnd('~');
            }
            objDataWrapper.ClearParameteres();

        }

        public void getAdditionalInterest(ClsProcessInfo objProcessInfo)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            getAdditionalInterest(objProcessInfo, objDataWrapper);
        }


        #region Add(Insert) functions
        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="objProcessInfo">Model class object.</param>
        /// <returns>No of records effected.</returns>
        public int AddProcessInformation(ClsProcessInfo objProcessInfo)
        {
            string strStoredProc = "Proc_InsertPOL_POLICY_PROCESS";
            DateTime RecordDate = DateTime.Now;

            try
            {

                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", objProcessInfo.CUSTOMER_ID);
                objWrapper.AddParameter("@POLICY_ID", objProcessInfo.POLICY_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", objProcessInfo.POLICY_VERSION_ID);
                objWrapper.AddParameter("@PROCESS_ID", objProcessInfo.PROCESS_ID);
                objWrapper.AddParameter("@PROCESS_TYPE", objProcessInfo.PROCESS_TYPE);
                objWrapper.AddParameter("@NEW_CUSTOMER_ID", objProcessInfo.NEW_CUSTOMER_ID);
                objWrapper.AddParameter("@NEW_POLICY_ID", objProcessInfo.NEW_POLICY_ID);
                objWrapper.AddParameter("@NEW_POLICY_VERSION_ID", objProcessInfo.NEW_POLICY_VERSION_ID);
                objWrapper.AddParameter("@POLICY_PREVIOUS_STATUS", objProcessInfo.POLICY_PREVIOUS_STATUS);
                objWrapper.AddParameter("@POLICY_CURRENT_STATUS", objProcessInfo.POLICY_CURRENT_STATUS);
                objWrapper.AddParameter("@PROCESS_STATUS", objProcessInfo.PROCESS_STATUS);
                objWrapper.AddParameter("@CREATED_BY", objProcessInfo.CREATED_BY);
                objWrapper.AddParameter("@PRINTING_OPTIONS", objProcessInfo.PRINTING_OPTIONS);
                objWrapper.AddParameter("@INSURED", objProcessInfo.INSURED);
                objWrapper.AddParameter("@SEND_INSURED_COPY_TO", objProcessInfo.SEND_INSURED_COPY_TO);
                objWrapper.AddParameter("@AUTO_ID_CARD", objProcessInfo.AUTO_ID_CARD);
                objWrapper.AddParameter("@NO_COPIES", objProcessInfo.NO_COPIES);
                objWrapper.AddParameter("@AGENCY_PRINT", objProcessInfo.AGENCY_PRINT);


                if (objProcessInfo.CREATED_DATETIME.Ticks != 0)
                    objWrapper.AddParameter("@CREATED_DATETIME", objProcessInfo.CREATED_DATETIME);
                else
                    objWrapper.AddParameter("@CREATED_DATETIME", null);

                objWrapper.AddParameter("@COMPLETED_BY", objProcessInfo.COMPLETED_BY);

                if (objProcessInfo.COMPLETED_DATETIME.Ticks != 0)
                    objWrapper.AddParameter("@COMPLETED_DATETIME", objProcessInfo.COMPLETED_DATETIME);
                else
                    objWrapper.AddParameter("@COMPLETED_DATETIME", null);


                objWrapper.AddParameter("@COMMENTS", objProcessInfo.COMMENTS);
                objWrapper.AddParameter("@PRINT_COMMENTS", objProcessInfo.PRINT_COMMENTS);
                objWrapper.AddParameter("@REQUESTED_BY", objProcessInfo.REQUESTED_BY);

                if (objProcessInfo.EFFECTIVE_DATETIME.Ticks != 0)
                    objWrapper.AddParameter("@EFFECTIVE_DATETIME", objProcessInfo.EFFECTIVE_DATETIME);
                else
                    objWrapper.AddParameter("@EFFECTIVE_DATETIME", null);

                if (objProcessInfo.EXPIRY_DATE.Ticks != 0)
                    objWrapper.AddParameter("@EXPIRY_DATE", objProcessInfo.EXPIRY_DATE);
                else
                    objWrapper.AddParameter("@EXPIRY_DATE", null);

                objWrapper.AddParameter("@CANCELLATION_OPTION", objProcessInfo.CANCELLATION_OPTION);
                objWrapper.AddParameter("@CANCELLATION_TYPE", objProcessInfo.CANCELLATION_TYPE);
                //				//for rescind process
                //				objWrapper.AddParameter("@RESCIND_OPTION",objProcessInfo.RESCIND_OPTION);
                //				objWrapper.AddParameter("@RESCIND_TYPE",objProcessInfo.RESCIND_TYPE);

                objWrapper.AddParameter("@REASON", objProcessInfo.REASON);
                objWrapper.AddParameter("@OTHER_REASON", objProcessInfo.OTHER_REASON);
                objWrapper.AddParameter("@RETURN_PREMIUM", objProcessInfo.RETURN_PREMIUM);
                objWrapper.AddParameter("@PAST_DUE_PREMIUM", objProcessInfo.PAST_DUE_PREMIUM);
                objWrapper.AddParameter("@ENDORSEMENT_NO", objProcessInfo.ENDORSEMENT_NO);
                objWrapper.AddParameter("@PROPERTY_INSPECTION_CREDIT", objProcessInfo.PROPERTY_INSPECTION_CREDIT);
                objWrapper.AddParameter("@POLICY_TERMS", objProcessInfo.POLICY_TERMS);
                objWrapper.AddParameter("@NEW_POLICY_TERM_EFFECTIVE_DATE", DefaultValues.GetDateNull(objProcessInfo.NEW_POLICY_TERM_EFFECTIVE_DATE));
                objWrapper.AddParameter("@NEW_POLICY_TERM_EXPIRATION_DATE", DefaultValues.GetDateNull(objProcessInfo.NEW_POLICY_TERM_EXPIRATION_DATE));
                objWrapper.AddParameter("@DIARY_LIST_ID", DefaultValues.GetIntNull(objProcessInfo.DIARY_LIST_ID));
                objWrapper.AddParameter("@STD_LETTER_REQD", objProcessInfo.STD_LETTER_REQD);
                objWrapper.AddParameter("@CUSTOM_LETTER_REQD", objProcessInfo.CUSTOM_LETTER_REQD);
                objWrapper.AddParameter("@ADD_INT", objProcessInfo.ADD_INT);
                objWrapper.AddParameter("@ADD_INT_ID", objProcessInfo.ADD_INT_ID);
                objWrapper.AddParameter("@SEND_ALL", objProcessInfo.SEND_ALL);
                objWrapper.AddParameter("@ENDORSEMENT_TYPE", objProcessInfo.ENDORSEMENT_TYPE);//Added By Lalit desc 29,2010
                objWrapper.AddParameter("@ENDORSEMENT_OPTION", objProcessInfo.ENDORSEMENT_OPTION);//Added By Lalit desc 10,2010
                objWrapper.AddParameter("@SOURCE_VERSION_ID", objProcessInfo.SOURCE_VERSION_ID);//Added By Lalit desc 29,2010
                //insert other Rescission  date 
                objWrapper.AddParameter("@OTHER_RES_DATE_CD", objProcessInfo.OTHER_RES_DATE_CD);

                if (objProcessInfo.OTHER_RES_DATE.Ticks != 0)
                    objWrapper.AddParameter("@OTHER_RES_DATE", objProcessInfo.OTHER_RES_DATE);
                else
                    objWrapper.AddParameter("@OTHER_RES_DATE", null);


                //Due date of cancellation 

                if (objProcessInfo.DUE_DATE.Ticks != 0)
                    objWrapper.AddParameter("@DUE_DATE", objProcessInfo.DUE_DATE);
                else
                    objWrapper.AddParameter("@DUE_DATE", null);

                objWrapper.AddParameter("@INCLUDE_REASON_DESC", objProcessInfo.INCLUDE_REASON_DESC);
                objWrapper.AddParameter("@CO_APPLICANT_ID", objProcessInfo.CO_APPLICANT_ID);
                objWrapper.AddParameter("@ENDORSEMENT_RE_ISSUE", objProcessInfo.ENDORSEMENT_RE_ISSUE);
                
                SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@ROW_ID", objProcessInfo.ROW_ID,
                    SqlDbType.Int, ParameterDirection.Output);

                int returnResult = 0;
                //added by pravesh to maintain transaction on launch
                if ((objProcessInfo.PROCESS_ID == POLICY_CANCELLATION_PROCESS || objProcessInfo.PROCESS_ID == POLICY_RESCIND) && base.TransactionLogRequired)
                {
                    if (objProcessInfo.PROCESS_ID == POLICY_CANCELLATION_PROCESS)
                        objProcessInfo.TransactLabel = ClsCommon.MapTransactionLabel("/policies/processes/CancellationProcess.aspx.resx");
                    else
                        objProcessInfo.TransactLabel = ClsCommon.MapTransactionLabel("/policies/processes/RescindProcess.aspx.resx");

                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objProcessInfo);
                    TransactionChangeXML.Append("\n" + strTranXML);
                }
                else
                {
                    string strResPath = "";
                    bool tLogReqd = false;
                    ProcessTransLogReqd(objProcessInfo.PROCESS_ID, out tLogReqd, out strResPath);
                    if (base.TransactionLogRequired && tLogReqd)
                    {
                        objProcessInfo.TransactLabel = ClsCommon.MapTransactionLabel(strResPath);
                        Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                        SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                        string strTranXML = objBuilder.GetTransactionLogXML(objProcessInfo);
                        if (strTranXML != "<LabelFieldMapping></LabelFieldMapping>" && strTranXML != "")
                            TransactionChangeXML.Append("\n" + strTranXML);
                    }
                }

                //end here
                /*if(TransactionLogRequired)
                {
                    objProcessInfo.TransactLabel = ClsCommon.MapTransactionLabel("/policies/processes/EndorsementProcess.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objProcessInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
                    objTransactionInfo.TRANS_TYPE_ID			=	3;
                    objTransactionInfo.POLICY_ID				=	objProcessInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID	=	objProcessInfo.POLICY_VERSION_ID;
                    objTransactionInfo.CLIENT_ID				=	objProcessInfo.CUSTOMER_ID;
                    objTransactionInfo.RECORDED_BY				=	objProcessInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC				=	"Information Has Been Updated";
                    objTransactionInfo.CHANGE_XML				=	strTranXML;
					
                    //Executing the query
                    returnResult	= objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
                }
                else
                {*/
                returnResult = objWrapper.ExecuteNonQuery(strStoredProc);
                /*}*/

                objProcessInfo.ROW_ID = int.Parse(objSqlParameter.Value.ToString());

                objWrapper.ClearParameteres();
                return returnResult;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        #endregion

        #region Update method
        public int UpdateProcessCancelNoticeInformation(ClsProcessInfo objProcessInfo)
        {
            string strStoredProc = "Proc_UpdatePOL_POLICY_PROCESS_NOTICE";
            int returnResult = 0;

            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", objProcessInfo.CUSTOMER_ID);
                objWrapper.AddParameter("@POLICY_ID", objProcessInfo.POLICY_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", objProcessInfo.POLICY_VERSION_ID);
                objWrapper.AddParameter("@ROW_ID", objProcessInfo.ROW_ID);
                objWrapper.AddParameter("@PROCESS_ID", objProcessInfo.PROCESS_ID);
                objWrapper.AddParameter("@CANCELLATION_NOTICE_SENT", objProcessInfo.CANCELLATION_NOTICE_SENT);

                returnResult = objWrapper.ExecuteNonQuery(strStoredProc);
                objWrapper.ClearParameteres();
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        /// <summary>
        /// Update method that recieves Model object to save.
        /// </summary>
        /// <param name="objOldProcessInfo">Model object having old information</param>
        /// <param name="objProcessInfo">Model object having new information(form control's value)</param>
        /// <returns>No. of rows updated (1 or 0)</returns>
        public int UpdateProcessInformation(ClsProcessInfo objProcessInfo)
        {
            return UpdateProcessInformation(null, objProcessInfo);
            //			string		strStoredProc	=	"Proc_UpdatePOL_POLICY_PROCESS";
            //			string strTranXML;
            //			int returnResult = 0;
            //			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            //
            //			try 
            //			{
            //				objWrapper.ClearParameteres();
            //				objWrapper.AddParameter("@CUSTOMER_ID",objProcessInfo.CUSTOMER_ID);
            //				objWrapper.AddParameter("@POLICY_ID",objProcessInfo.POLICY_ID);
            //				objWrapper.AddParameter("@POLICY_VERSION_ID",objProcessInfo.POLICY_VERSION_ID);
            //				objWrapper.AddParameter("@ROW_ID",objProcessInfo.ROW_ID);
            //				objWrapper.AddParameter("@PROCESS_ID",objProcessInfo.PROCESS_ID);
            //				objWrapper.AddParameter("@PROCESS_TYPE",objProcessInfo.PROCESS_TYPE);
            //				objWrapper.AddParameter("@NEW_CUSTOMER_ID",objProcessInfo.NEW_CUSTOMER_ID);
            //				objWrapper.AddParameter("@NEW_POLICY_ID",objProcessInfo.NEW_POLICY_ID);
            //				objWrapper.AddParameter("@NEW_POLICY_VERSION_ID",objProcessInfo.NEW_POLICY_VERSION_ID);
            //				objWrapper.AddParameter("@POLICY_PREVIOUS_STATUS",objProcessInfo.POLICY_PREVIOUS_STATUS);
            //				objWrapper.AddParameter("@POLICY_CURRENT_STATUS",objProcessInfo.POLICY_CURRENT_STATUS);
            //				objWrapper.AddParameter("@PROCESS_STATUS",objProcessInfo.PROCESS_STATUS);				
            //				objWrapper.AddParameter("@AGENCY_PRINT",objProcessInfo.AGENCY_PRINT);
            //				
            //
            //				objWrapper.AddParameter("@COMPLETED_BY",objProcessInfo.COMPLETED_BY);
            //
            //				if (objProcessInfo.COMPLETED_DATETIME.Ticks != 0)
            //					objWrapper.AddParameter("@COMPLETED_DATETIME",objProcessInfo.COMPLETED_DATETIME);
            //				else
            //					objWrapper.AddParameter("@COMPLETED_DATETIME", null);
            //
            //
            //				objWrapper.AddParameter("@COMMENTS",objProcessInfo.COMMENTS);
            //				objWrapper.AddParameter("@PRINT_COMMENTS",objProcessInfo.PRINT_COMMENTS);
            //				objWrapper.AddParameter("@REQUESTED_BY",objProcessInfo.REQUESTED_BY);
            //
            //				if (objProcessInfo.EFFECTIVE_DATETIME.Ticks != 0)
            //					objWrapper.AddParameter("@EFFECTIVE_DATETIME",objProcessInfo.EFFECTIVE_DATETIME);
            //				else
            //					objWrapper.AddParameter("@EFFECTIVE_DATETIME", null);
            //
            //				if (objProcessInfo.EXPIRY_DATE.Ticks != 0)
            //					objWrapper.AddParameter("@EXPIRY_DATE",objProcessInfo.EXPIRY_DATE);
            //				else
            //					objWrapper.AddParameter("@EXPIRY_DATE", null);
            //
            //				objWrapper.AddParameter("@CANCELLATION_OPTION",objProcessInfo.CANCELLATION_OPTION);
            //				objWrapper.AddParameter("@CANCELLATION_TYPE",objProcessInfo.CANCELLATION_TYPE);
            //
            //				//for rescind process
            //				objWrapper.AddParameter("@RESCIND_OPTION",objProcessInfo.RESCIND_OPTION);
            //				objWrapper.AddParameter("@RESCIND_TYPE",objProcessInfo.RESCIND_TYPE);
            //
            //				objWrapper.AddParameter("@REASON",objProcessInfo.REASON);
            //				objWrapper.AddParameter("@OTHER_REASON",objProcessInfo.OTHER_REASON);
            //				objWrapper.AddParameter("@RETURN_PREMIUM",objProcessInfo.RETURN_PREMIUM);
            //				objWrapper.AddParameter("@PAST_DUE_PREMIUM",objProcessInfo.PAST_DUE_PREMIUM);
            //				objWrapper.AddParameter("@ENDORSEMENT_NO",objProcessInfo.ENDORSEMENT_NO);
            //				objWrapper.AddParameter("@PROPERTY_INSPECTION_CREDIT",objProcessInfo.PROPERTY_INSPECTION_CREDIT);
            //				objWrapper.AddParameter("@POLICY_TERMS",objProcessInfo.POLICY_TERMS);
            //				objWrapper.AddParameter("@NEW_POLICY_TERM_EFFECTIVE_DATE",DefaultValues.GetDateNull(objProcessInfo.NEW_POLICY_TERM_EFFECTIVE_DATE));
            //				objWrapper.AddParameter("@NEW_POLICY_TERM_EXPIRATION_DATE",DefaultValues.GetDateNull(objProcessInfo.NEW_POLICY_TERM_EXPIRATION_DATE));
            //				objWrapper.AddParameter("@PRINTING_OPTIONS",objProcessInfo.PRINTING_OPTIONS);
            //				objWrapper.AddParameter("@INSURED",objProcessInfo.INSURED);
            //				objWrapper.AddParameter("@SEND_INSURED_COPY_TO",objProcessInfo.SEND_INSURED_COPY_TO);
            //				objWrapper.AddParameter("@AUTO_ID_CARD",objProcessInfo.AUTO_ID_CARD);
            //				objWrapper.AddParameter("@NO_COPIES",objProcessInfo.NO_COPIES);
            //				objWrapper.AddParameter("@STD_LETTER_REQD",objProcessInfo.STD_LETTER_REQD);
            //				objWrapper.AddParameter("@CUSTOM_LETTER_REQD",objProcessInfo.CUSTOM_LETTER_REQD);
            //				objWrapper.AddParameter("@ADD_INT",objProcessInfo.ADD_INT);
            //				objWrapper.AddParameter("@ADD_INT_ID",objProcessInfo.ADD_INT_ID);
            //				objWrapper.AddParameter("@SEND_ALL",objProcessInfo.SEND_ALL);
            //
            //				/*	if(base.TransactionLogRequired) 
            //					{
            //						objProcessInfo.TransactLabel = ClsCommon.MapTransactionLabel("/policies/processes/EndorsementProcess.aspx.resx");
            //						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
            //						strTranXML = objBuilder.GetTransactionLogXML(objOldProcessInfo, objProcessInfo);
            //
            //						objTransactionInfo.TRANS_TYPE_ID			=	3;
            //						objTransactionInfo.POLICY_ID				=	objProcessInfo.POLICY_ID;
            //						objTransactionInfo.POLICY_VER_TRACKING_ID	=	objProcessInfo.POLICY_VERSION_ID;
            //						objTransactionInfo.CLIENT_ID				=	objProcessInfo.CUSTOMER_ID;
            //						//objTransactionInfo.RECORDED_BY				=	objProcessInfo.MODIFIED_BY;
            //						objTransactionInfo.TRANS_DESC				=	"Information Has Been Updated";
            //						objTransactionInfo.CHANGE_XML				=	strTranXML;
            //						returnResult = objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
            //
            //					}
            //					else
            //					{*/
            //				returnResult = objWrapper.ExecuteNonQuery(strStoredProc);
            //				/*}*/
            //				
            //				objWrapper.ClearParameteres();	
            //				return returnResult;
            //			}
            //			catch(Exception ex)
            //			{
            //				throw(ex);
            //			}
            //			finally
            //			{
            //				if(objBuilder != null) 
            //				{
            //					objBuilder = null;
            //				}
            //			}
        }
        public int UpdateProcessInfoBeforeCommit(ClsProcessInfo objProcessInfo)
        {
            string strStoredProc = "Proc_UpdatePOL_POLICY_PROCESS_INFO";
            int returnResult = 0;

            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", objProcessInfo.CUSTOMER_ID);
                objWrapper.AddParameter("@POLICY_ID", objProcessInfo.POLICY_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", objProcessInfo.POLICY_VERSION_ID);
                objWrapper.AddParameter("@ROW_ID", objProcessInfo.ROW_ID);
                objWrapper.AddParameter("@NEW_POLICY_VERSION_ID", objProcessInfo.NEW_POLICY_VERSION_ID);
                if (objProcessInfo.EFFECTIVE_DATETIME.Ticks != 0)
                    objWrapper.AddParameter("@EFFECTIVE_DATETIME", objProcessInfo.EFFECTIVE_DATETIME);
                else
                    objWrapper.AddParameter("@EFFECTIVE_DATETIME", null);

                objWrapper.AddParameter("@RETURN_PREMIUM", objProcessInfo.RETURN_PREMIUM);

                objWrapper.AddParameter("@CANCELLATION_OPTION", objProcessInfo.CANCELLATION_OPTION);
                objWrapper.AddParameter("@CANCELLATION_TYPE", objProcessInfo.CANCELLATION_TYPE);


                returnResult = objWrapper.ExecuteNonQuery(strStoredProc);
                objWrapper.ClearParameteres();
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Update method that recieves Model object to save.
        /// </summary>
        /// <param name="objProcessInfo">Model object having new information(form control's value)</param>
        /// <returns>No. of rows updated (1 or 0)</returns>
        public int UpdateProcessInformation(ClsProcessInfo objOldProcessInfo, ClsProcessInfo objProcessInfo)
        {
            string strStoredProc = "Proc_UpdatePOL_POLICY_PROCESS";
            int returnResult = 0;

            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", objProcessInfo.CUSTOMER_ID);
                objWrapper.AddParameter("@POLICY_ID", objProcessInfo.POLICY_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", objProcessInfo.POLICY_VERSION_ID);
                objWrapper.AddParameter("@ROW_ID", objProcessInfo.ROW_ID);
                objWrapper.AddParameter("@PROCESS_ID", objProcessInfo.PROCESS_ID);
                objWrapper.AddParameter("@PROCESS_TYPE", objProcessInfo.PROCESS_TYPE);
                objWrapper.AddParameter("@NEW_CUSTOMER_ID", objProcessInfo.NEW_CUSTOMER_ID);
                objWrapper.AddParameter("@NEW_POLICY_ID", objProcessInfo.NEW_POLICY_ID);
                objWrapper.AddParameter("@NEW_POLICY_VERSION_ID", objProcessInfo.NEW_POLICY_VERSION_ID);
                objWrapper.AddParameter("@POLICY_PREVIOUS_STATUS", objProcessInfo.POLICY_PREVIOUS_STATUS);
                objWrapper.AddParameter("@POLICY_CURRENT_STATUS", objProcessInfo.POLICY_CURRENT_STATUS);
                objWrapper.AddParameter("@PROCESS_STATUS", objProcessInfo.PROCESS_STATUS);


                objWrapper.AddParameter("@COMPLETED_BY", objProcessInfo.COMPLETED_BY);

                if (objProcessInfo.COMPLETED_DATETIME.Ticks != 0)
                    objWrapper.AddParameter("@COMPLETED_DATETIME", objProcessInfo.COMPLETED_DATETIME);
                else
                    objWrapper.AddParameter("@COMPLETED_DATETIME", null);


                objWrapper.AddParameter("@COMMENTS", objProcessInfo.COMMENTS);
                objWrapper.AddParameter("@PRINT_COMMENTS", objProcessInfo.PRINT_COMMENTS);
                objWrapper.AddParameter("@REQUESTED_BY", objProcessInfo.REQUESTED_BY);

                if (objProcessInfo.EFFECTIVE_DATETIME.Ticks != 0)
                    objWrapper.AddParameter("@EFFECTIVE_DATETIME", objProcessInfo.EFFECTIVE_DATETIME);
                else
                    objWrapper.AddParameter("@EFFECTIVE_DATETIME", null);

                if (objProcessInfo.EXPIRY_DATE.Ticks != 0)
                    objWrapper.AddParameter("@EXPIRY_DATE", objProcessInfo.EXPIRY_DATE);
                else
                    objWrapper.AddParameter("@EXPIRY_DATE", null);

                objWrapper.AddParameter("@CANCELLATION_OPTION", objProcessInfo.CANCELLATION_OPTION);
                objWrapper.AddParameter("@CANCELLATION_TYPE", objProcessInfo.CANCELLATION_TYPE);

                //				//for rescind process
                //				objWrapper.AddParameter("@RESCIND_OPTION",objProcessInfo.RESCIND_OPTION);
                //				objWrapper.AddParameter("@RESCIND_TYPE",objProcessInfo.RESCIND_TYPE);

                objWrapper.AddParameter("@REASON", objProcessInfo.REASON);
                objWrapper.AddParameter("@OTHER_REASON", objProcessInfo.OTHER_REASON);
                objWrapper.AddParameter("@RETURN_PREMIUM", objProcessInfo.RETURN_PREMIUM);
                objWrapper.AddParameter("@PAST_DUE_PREMIUM", objProcessInfo.PAST_DUE_PREMIUM);
                objWrapper.AddParameter("@ENDORSEMENT_NO", objProcessInfo.ENDORSEMENT_NO);
                objWrapper.AddParameter("@PROPERTY_INSPECTION_CREDIT", objProcessInfo.PROPERTY_INSPECTION_CREDIT);
                objWrapper.AddParameter("@POLICY_TERMS", objProcessInfo.POLICY_TERMS);
                objWrapper.AddParameter("@NEW_POLICY_TERM_EFFECTIVE_DATE", DefaultValues.GetDateNull(objProcessInfo.NEW_POLICY_TERM_EFFECTIVE_DATE));
                objWrapper.AddParameter("@NEW_POLICY_TERM_EXPIRATION_DATE", DefaultValues.GetDateNull(objProcessInfo.NEW_POLICY_TERM_EXPIRATION_DATE));
                objWrapper.AddParameter("@INTERNAL_CHANGE", objProcessInfo.INTERNAL_CHANGE);
                objWrapper.AddParameter("@PRINTING_OPTIONS", objProcessInfo.PRINTING_OPTIONS);
                objWrapper.AddParameter("@INSURED", objProcessInfo.INSURED);
                objWrapper.AddParameter("@SEND_INSURED_COPY_TO", objProcessInfo.SEND_INSURED_COPY_TO);
                objWrapper.AddParameter("@AUTO_ID_CARD", objProcessInfo.AUTO_ID_CARD);
                objWrapper.AddParameter("@NO_COPIES", objProcessInfo.NO_COPIES);
                objWrapper.AddParameter("@STD_LETTER_REQD", objProcessInfo.STD_LETTER_REQD);
                objWrapper.AddParameter("@ADVERSE_LETTER_REQD", objProcessInfo.ADVERSE_LETTER_REQD);
                objWrapper.AddParameter("@CUSTOM_LETTER_REQD", objProcessInfo.CUSTOM_LETTER_REQD);
                objWrapper.AddParameter("@ADD_INT", objProcessInfo.ADD_INT);
                objWrapper.AddParameter("@ADD_INT_ID", objProcessInfo.ADD_INT_ID);
                objWrapper.AddParameter("@SEND_ALL", objProcessInfo.SEND_ALL);
                objWrapper.AddParameter("@AGENCY_PRINT", objProcessInfo.AGENCY_PRINT);
                objWrapper.AddParameter("@APPLY_REINSTATE_FEE", objProcessInfo.APPLY_REINSTATE_FEE);
                objWrapper.AddParameter("@SAME_AGENCY", objProcessInfo.SAME_AGENCY);
                objWrapper.AddParameter("@ANOTHER_AGENCY", objProcessInfo.ANOTHER_AGENCY);
                objWrapper.AddParameter("@COINSURANCE_NUMBER", objProcessInfo.COINSURANCE_NUMBER);
                objWrapper.AddParameter("@ENDORSEMENT_TYPE", objProcessInfo.ENDORSEMENT_TYPE);
                objWrapper.AddParameter("@ENDORSEMENT_OPTION", objProcessInfo.ENDORSEMENT_OPTION);

                //update other Rescission  date 
                objWrapper.AddParameter("@OTHER_RES_DATE_CD", objProcessInfo.OTHER_RES_DATE_CD);

                if (objProcessInfo.OTHER_RES_DATE.Ticks != 0)
                    objWrapper.AddParameter("@OTHER_RES_DATE", objProcessInfo.OTHER_RES_DATE);
                else
                    objWrapper.AddParameter("@OTHER_RES_DATE", null);

                objWrapper.AddParameter("@CFD_AMT", objProcessInfo.CFD_AMT);
                //Due date of cancellation 

                if (objProcessInfo.DUE_DATE.Ticks != 0)
                    objWrapper.AddParameter("@DUE_DATE", objProcessInfo.DUE_DATE);
                else
                    objWrapper.AddParameter("@DUE_DATE", null);
                objWrapper.AddParameter("@CANCELLATION_NOTICE_SENT", objProcessInfo.CANCELLATION_NOTICE_SENT);
                if (objProcessInfo.REVERT_BACK != "")
                    objWrapper.AddParameter("@REVERT_BACK", objProcessInfo.REVERT_BACK);
                if (objProcessInfo.LAST_REVERT_BACK != "")
                    objWrapper.AddParameter("@LAST_REVERT_BACK", objProcessInfo.LAST_REVERT_BACK);

                objWrapper.AddParameter("@INCLUDE_REASON_DESC", objProcessInfo.INCLUDE_REASON_DESC);
                objWrapper.AddParameter("@CO_APPLICANT_ID", objProcessInfo.CO_APPLICANT_ID);
                objWrapper.AddParameter("@ENDORSEMENT_RE_ISSUE", objProcessInfo.ENDORSEMENT_RE_ISSUE);
                objWrapper.AddParameter("@SOURCE_VERSION_ID", objProcessInfo.SOURCE_VERSION_ID);
                
                //Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                if ((objProcessInfo.PROCESS_ID == POLICY_COMMIT_CANCELLATION_PROCESS || objProcessInfo.PROCESS_ID == POLICY_COMMIT_RESCIND_PROCESS) && base.TransactionLogRequired)
                {
                    if (objProcessInfo.PROCESS_ID == POLICY_COMMIT_CANCELLATION_PROCESS)
                        objProcessInfo.TransactLabel = ClsCommon.MapTransactionLabel("/policies/processes/CancellationProcess.aspx.resx");
                    else
                        objProcessInfo.TransactLabel = ClsCommon.MapTransactionLabel("/policies/processes/RescindProcess.aspx.resx");

                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objProcessInfo);
                    strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='SEND_INSURED_COPY_TO' and @OldValue='0']", "OldValue", "");
                    strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='SEND_INSURED_COPY_TO' and @NewValue='0']", "NewValue", "");
                    TransactionChangeXML.Append("\n" + strTranXML);
                    returnResult = objWrapper.ExecuteNonQuery(strStoredProc);
                }
                else
                {
                    string strResPath = "";
                    bool tLogReqd = false;
                    ProcessTransLogReqd(objProcessInfo.PROCESS_ID, out tLogReqd, out strResPath);
                    if (objOldProcessInfo != null && base.TransactionLogRequired && tLogReqd)
                    {
                        objProcessInfo.TransactLabel = ClsCommon.MapTransactionLabel(strResPath);
                        Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                        SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                        string strTranXML = objBuilder.GetTransactionLogXML(objOldProcessInfo, objProcessInfo);
                        if (strTranXML == "<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
                            returnResult = objWrapper.ExecuteNonQuery(strStoredProc);
                        else
                        {
                            // 11 is taken as it's 'LOOKUP_VALUE_DESC' is null in table 'MNT_LOOKUP_VALUES'. This is need when value change from null to some value.
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='REQUESTED_BY' and @OldValue='0']", "OldValue", "11");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='CANCELLATION_OPTION' and @OldValue='0']", "OldValue", "11");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='CANCELLATION_TYPE' and @OldValue='0']", "OldValue", "11");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='REASON' and @OldValue='0']", "OldValue", "11");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='RETURN_PREMIUM' and @OldValue='0']", "OldValue", "null");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='APPLY_REINSTATE_FEE' and @OldValue='null']", "OldValue", "");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='ADVERSE_LETTER_REQD' and @OldValue='null']", "OldValue", "");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='SEND_INSURED_COPY_TO' and @OldValue='0']", "OldValue", "");
                            strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML, "LabelFieldMapping/Map[@field='SEND_INSURED_COPY_TO' and @NewValue='0']", "NewValue", "");
                            objTransactionInfo.TRANS_TYPE_ID = 3;
                            objTransactionInfo.POLICY_ID = objProcessInfo.POLICY_ID;
                            if (objProcessInfo.NEW_POLICY_VERSION_ID != 0)
                                objTransactionInfo.POLICY_VER_TRACKING_ID = objProcessInfo.NEW_POLICY_VERSION_ID;
                            else
                                objTransactionInfo.POLICY_VER_TRACKING_ID = objProcessInfo.POLICY_VERSION_ID;
                            objTransactionInfo.CLIENT_ID = objProcessInfo.CUSTOMER_ID;
                            //objTransactionInfo.RECORDED_BY				=	objProcessInfo.COMPLETED_BY;
                            if (IsEODProcess)
                                objTransactionInfo.RECORDED_BY = EODUserID;
                            else
                                objTransactionInfo.RECORDED_BY = objProcessInfo.CREATED_BY;
                            objTransactionInfo.TRANS_DESC = BlCommon.ClsCommon.FetchGeneralMessage("1423", "");//"Process Log Information Has Been Updated";
                            objTransactionInfo.CUSTOM_INFO = BlCommon.ClsCommon.FetchGeneralMessage("1424", "") + "= " + DateTime.Now.ToString();
                            if (objProcessInfo.PROCESS_ID == POLICY_COMMIT_ENDORSEMENT_PROCESS || objProcessInfo.PROCESS_ID == POLICY_ENDORSEMENT_PROCESS)
                                objTransactionInfo.CUSTOM_INFO = objTransactionInfo.CUSTOM_INFO + ";\n" + objProcessInfo.COMMENTS;
                            objTransactionInfo.CHANGE_XML = strTranXML;
                            returnResult = objWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                        }
                    }
                    else
                        returnResult = objWrapper.ExecuteNonQuery(strStoredProc);
                }
                objWrapper.ClearParameteres();
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        public void UpdateNewPolicyNo(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, int PROCESS_ID, out string POlicy_Number, out string SUSEP_LOB_CODE)
        {
            try
            {
                DataSet ds = new DataSet();
                string strDiv = "";
                string APP_EFF_DATE = "";
                //string sNewPolNumber;
                DateTime dtAPP_EFF_DATE = DateTime.Now;
                int LOb = 0;
                int CO_INSURANCE = 0;
                ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
                ds = objGeneralInformation.GetPolicyDataSet(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    strDiv = ds.Tables[0].Rows[0]["DIV_ID"].ToString();
                    APP_EFF_DATE = ds.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString();
                    LOb = int.Parse(ds.Tables[0].Rows[0]["POLICY_LOB"].ToString());
                    if (ds.Tables[0].Rows[0]["CO_INSURANCE"].ToString() != "")
                        CO_INSURANCE = int.Parse(ds.Tables[0].Rows[0]["CO_INSURANCE"].ToString());
                }
                if (APP_EFF_DATE != "")
                    dtAPP_EFF_DATE = ConvertDBDateToCulture(APP_EFF_DATE); //(APP_EFF_DATE);

                 SUSEP_LOB_CODE = string.Empty;
                 POlicy_Number = ClsGeneralInformation.GenerateAppPolNumber(LOb, int.Parse(strDiv), ConvertToDate(dtAPP_EFF_DATE.ToString()), "POL", CO_INSURANCE, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, out SUSEP_LOB_CODE);

                 int retval = objGeneralInformation.UpdatePolicyNo(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, POlicy_Number, PROCESS_ID, objWrapper, SUSEP_LOB_CODE);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void UpdateNewPolicyNo(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, int PROCESS_ID)
        {
            try
            {
                DataSet ds = new DataSet();
                string strDiv = "";
                string APP_EFF_DATE = "";
                string sNewPolNumber;
                DateTime dtAPP_EFF_DATE = DateTime.Now;
                int LOb = 0;
                int CO_INSURANCE = 0;
                ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
                ds = objGeneralInformation.GetPolicyDataSet(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    strDiv = ds.Tables[0].Rows[0]["DIV_ID"].ToString();
                    APP_EFF_DATE = ds.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString();
                    LOb = int.Parse(ds.Tables[0].Rows[0]["POLICY_LOB"].ToString());
                    if (ds.Tables[0].Rows[0]["CO_INSURANCE"].ToString() != "")
                        CO_INSURANCE = int.Parse(ds.Tables[0].Rows[0]["CO_INSURANCE"].ToString());
                }
                if (APP_EFF_DATE != "")
                    dtAPP_EFF_DATE = ConvertDBDateToCulture(APP_EFF_DATE); //(APP_EFF_DATE);

                string SUSEP_LOB_CODE = string.Empty;
                sNewPolNumber = ClsGeneralInformation.GenerateAppPolNumber(LOb, int.Parse(strDiv), ConvertToDate(dtAPP_EFF_DATE.ToString()), "POL", CO_INSURANCE, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, out SUSEP_LOB_CODE);

                int retval = objGeneralInformation.UpdatePolicyNo(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, sNewPolNumber, PROCESS_ID, objWrapper, SUSEP_LOB_CODE);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #region "GetxmlMethods"

        public static DataSet GetProcessInfoDataSet(int CustomerId, int PolicyId, int PolicyVersionId, int RowID)
        {
            try
            {
                string strSql = "Proc_GetXMLPOL_POLICY_PROCESS";
                DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

                objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                objWrapper.AddParameter("@ROW_ID", RowID);

                DataSet objDataSet = objWrapper.ExecuteDataSet(strSql);
                objWrapper.ClearParameteres();
                return objDataSet;
            }
            catch (Exception objExp)
            {
                throw (new Exception("Following error occured in GetXML function. \n " + objExp.Message));
            }
        }


        public string GetOldDataXml(int CustomerId, int PolicyId, int PolicyVersionId, int RowID)
        {
            return GetProcessInfoDataSet(CustomerId, PolicyId, PolicyVersionId, RowID).GetXml();
        }
        #endregion

        #region GetRunningProcess
        public ClsProcessInfo GetRunningProcess(int CustomerID, int PolicyID)
        {
            try
            {
                DataSet ds = ClsCommon.ExecuteDataSet(
                    "SELECT * FROM POL_POLICY_PROCESS WITH(NOLOCK)"
                    + " WHERE CUSTOMER_ID=" + CustomerID.ToString()
                    + " AND POLICY_ID=" + PolicyID.ToString()
                    + " AND PROCESS_STATUS='" + PROCESS_STATUS_PENDING + "'");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Cms.Model.Policy.Process.ClsProcessInfo objProcess = new ClsProcessInfo();

                    objProcess.POLICY_ID = PolicyID;
                    objProcess.POLICY_VERSION_ID = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
                    objProcess.CUSTOMER_ID = CustomerID;
                    objProcess.COMMENTS = ds.Tables[0].Rows[0]["COMMENTS"].ToString();
                    objProcess.PROCESS_ID = int.Parse(ds.Tables[0].Rows[0]["PROCESS_ID"].ToString());

                    objProcess.NEW_POLICY_ID = int.Parse(ds.Tables[0].Rows[0]["NEW_POLICY_ID"].ToString());
                    objProcess.NEW_CUSTOMER_ID = int.Parse(ds.Tables[0].Rows[0]["NEW_CUSTOMER_ID"].ToString());
                    objProcess.NEW_POLICY_VERSION_ID = int.Parse(ds.Tables[0].Rows[0]["NEW_POLICY_VERSION_ID"].ToString());

                    objProcess.POLICY_PREVIOUS_STATUS = ds.Tables[0].Rows[0]["POLICY_PREVIOUS_STATUS"].ToString();
                    objProcess.POLICY_CURRENT_STATUS = ds.Tables[0].Rows[0]["POLICY_CURRENT_STATUS"].ToString();

                    if (ds.Tables[0].Rows[0]["EXPIRY_DATE"] != DBNull.Value)
                        objProcess.EXPIRY_DATE = Convert.ToDateTime(ds.Tables[0].Rows[0]["EXPIRY_DATE"]);

                    if (ds.Tables[0].Rows[0]["EFFECTIVE_DATETIME"] != DBNull.Value)
                        objProcess.EFFECTIVE_DATETIME = Convert.ToDateTime(ds.Tables[0].Rows[0]["EFFECTIVE_DATETIME"]);

                    if (ds.Tables[0].Rows[0]["PRINT_COMMENTS"].ToString() != "")
                        objProcess.PRINT_COMMENTS = ds.Tables[0].Rows[0]["PRINT_COMMENTS"].ToString();

                    if (ds.Tables[0].Rows[0]["CANCELLATION_OPTION"].ToString() != "")
                        objProcess.CANCELLATION_OPTION = int.Parse(ds.Tables[0].Rows[0]["CANCELLATION_OPTION"].ToString());

                    if (ds.Tables[0].Rows[0]["CANCELLATION_TYPE"].ToString() != "")
                        objProcess.CANCELLATION_TYPE = int.Parse(ds.Tables[0].Rows[0]["CANCELLATION_TYPE"].ToString());
                    if (ds.Tables[0].Rows[0]["REASON"].ToString() != "")
                        objProcess.REASON = int.Parse(ds.Tables[0].Rows[0]["REASON"].ToString());
                    if (ds.Tables[0].Rows[0]["OTHER_REASON"].ToString() != "")
                        objProcess.OTHER_REASON = ds.Tables[0].Rows[0]["OTHER_REASON"].ToString();
                    if (ds.Tables[0].Rows[0]["RETURN_PREMIUM"] != DBNull.Value)
                        objProcess.RETURN_PREMIUM = Convert.ToDouble(ds.Tables[0].Rows[0]["RETURN_PREMIUM"].ToString());
                    if (ds.Tables[0].Rows[0]["PAST_DUE_PREMIUM"] != DBNull.Value)
                        objProcess.PAST_DUE_PREMIUM = Convert.ToDouble(ds.Tables[0].Rows[0]["PAST_DUE_PREMIUM"].ToString());
                    if (ds.Tables[0].Rows[0]["PROPERTY_INSPECTION_CREDIT"] != DBNull.Value)
                        objProcess.PROPERTY_INSPECTION_CREDIT = ds.Tables[0].Rows[0]["PROPERTY_INSPECTION_CREDIT"].ToString();

                    //					if (ds.Tables[0].Rows[0]["RESCIND_TYPE"].ToString() != "")
                    //						objProcess.RESCIND_TYPE	= int.Parse(ds.Tables[0].Rows[0]["RESCIND_TYPE"].ToString());
                    //
                    //					if (ds.Tables[0].Rows[0]["RESCIND_OPTION"].ToString() != "")
                    //						objProcess.RESCIND_OPTION	= int.Parse(ds.Tables[0].Rows[0]["RESCIND_OPTION"].ToString());


                    objProcess.CREATED_BY = int.Parse(ds.Tables[0].Rows[0]["CREATED_BY"].ToString());

                    if (ds.Tables[0].Rows[0]["CREATED_DATETIME"].ToString()!="")
                    objProcess.CREATED_DATETIME = ConvertToDate(ds.Tables[0].Rows[0]["CREATED_DATETIME"].ToString());

                    if (ds.Tables[0].Rows[0]["DIARY_LIST_ID"].ToString() != "")
                        objProcess.DIARY_LIST_ID = int.Parse(ds.Tables[0].Rows[0]["DIARY_LIST_ID"].ToString());

                    if (ds.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString() != "")
                        objProcess.EFFECTIVE_DATETIME = ConvertToDate(ds.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString());
                    if (ds.Tables[0].Rows[0]["DUE_DATE"].ToString() != "")
                        objProcess.DUE_DATE = ConvertToDate(ds.Tables[0].Rows[0]["DUE_DATE"].ToString());
                    if (ds.Tables[0].Rows[0]["CANCELLATION_NOTICE_SENT"] != DBNull.Value)
                        objProcess.CANCELLATION_NOTICE_SENT = ds.Tables[0].Rows[0]["CANCELLATION_NOTICE_SENT"].ToString();
                    if (ds.Tables[0].Rows[0]["ENDORSEMENT_NO"].ToString() != "")
                        objProcess.ENDORSEMENT_NO = int.Parse(ds.Tables[0].Rows[0]["ENDORSEMENT_NO"].ToString());

                    if (ds.Tables[0].Rows[0]["NEW_POLICY_TERM_EXPIRATION_DATE"].ToString() != "")
                        objProcess.NEW_POLICY_TERM_EXPIRATION_DATE = Convert.ToDateTime(ds.Tables[0].Rows[0]["NEW_POLICY_TERM_EXPIRATION_DATE"]);

                    if (ds.Tables[0].Rows[0]["NEW_POLICY_TERM_EFFECTIVE_DATE"].ToString() != "")
                        objProcess.NEW_POLICY_TERM_EFFECTIVE_DATE = Convert.ToDateTime(ds.Tables[0].Rows[0]["NEW_POLICY_TERM_EFFECTIVE_DATE"]);

                    if (ds.Tables[0].Rows[0]["POLICY_TERMS"].ToString() != "")
                        objProcess.POLICY_TERMS = int.Parse(ds.Tables[0].Rows[0]["POLICY_TERMS"].ToString());

                    if (ds.Tables[0].Rows[0]["INTERNAL_CHANGE"].ToString() != "")
                        objProcess.INTERNAL_CHANGE = ds.Tables[0].Rows[0]["INTERNAL_CHANGE"].ToString();

                    if (ds.Tables[0].Rows[0]["ADVERSE_LETTER_REQD"].ToString() != "")
                        objProcess.ADVERSE_LETTER_REQD = Convert.ToInt32(ds.Tables[0].Rows[0]["ADVERSE_LETTER_REQD"].ToString());

                    if (ds.Tables[0].Rows[0]["AGENCY_PRINT"].ToString() != "")
                        objProcess.AGENCY_PRINT = Convert.ToInt32(ds.Tables[0].Rows[0]["AGENCY_PRINT"].ToString());
                    if (ds.Tables[0].Rows[0]["INSURED"].ToString() != "")
                        objProcess.INSURED = Convert.ToInt32(ds.Tables[0].Rows[0]["INSURED"].ToString());
                    if (ds.Tables[0].Rows[0]["SEND_INSURED_COPY_TO"].ToString() != "")
                        objProcess.SEND_INSURED_COPY_TO = Convert.ToInt32(ds.Tables[0].Rows[0]["SEND_INSURED_COPY_TO"].ToString());
                    if (ds.Tables[0].Rows[0]["ADD_INT"].ToString() != "")
                        objProcess.ADD_INT = Convert.ToInt32(ds.Tables[0].Rows[0]["ADD_INT"].ToString());
                    if (ds.Tables[0].Rows[0]["ADD_INT_ID"].ToString() != "")
                        objProcess.ADD_INT_ID = ds.Tables[0].Rows[0]["ADD_INT_ID"].ToString();
                    if (ds.Tables[0].Rows[0]["CFD_AMT"].ToString() != "")
                        objProcess.CFD_AMT = Convert.ToDouble(ds.Tables[0].Rows[0]["CFD_AMT"].ToString());

                    if (ds.Tables[0].Rows[0]["PRINTING_OPTIONS"].ToString() != "")
                        objProcess.PRINTING_OPTIONS = Convert.ToInt32(ds.Tables[0].Rows[0]["PRINTING_OPTIONS"].ToString());

                    objProcess.ROW_ID = int.Parse(ds.Tables[0].Rows[0]["ROW_ID"].ToString());
                    if (ds.Tables[0].Rows[0]["LAST_REVERT_BACK"] != DBNull.Value)
                        objProcess.LAST_REVERT_BACK = ds.Tables[0].Rows[0]["LAST_REVERT_BACK"].ToString();
                    if (ds.Tables[0].Rows[0]["REVERT_BACK"] != DBNull.Value)
                        objProcess.REVERT_BACK = ds.Tables[0].Rows[0]["REVERT_BACK"].ToString();
                    if (ds.Tables[0].Rows[0]["INCLUDE_REASON_DESC"] != DBNull.Value)
                        objProcess.INCLUDE_REASON_DESC = ds.Tables[0].Rows[0]["INCLUDE_REASON_DESC"].ToString();

                    if (ds.Tables[0].Rows[0]["SEND_ALL"] != DBNull.Value)
                        objProcess.SEND_ALL = Convert.ToInt32(ds.Tables[0].Rows[0]["SEND_ALL"]);

                    if (ds.Tables[0].Rows[0]["PROCESS_STATUS"] != DBNull.Value)
                        objProcess.PROCESS_STATUS = ds.Tables[0].Rows[0]["PROCESS_STATUS"].ToString();
                    if (ds.Tables[0].Rows[0]["REQUESTED_BY"] != DBNull.Value)
                        objProcess.REQUESTED_BY = Convert.ToInt32(ds.Tables[0].Rows[0]["REQUESTED_BY"].ToString());
                    if (ds.Tables[0].Rows[0]["AUTO_ID_CARD"] != DBNull.Value)
                        objProcess.AUTO_ID_CARD = Convert.ToInt32(ds.Tables[0].Rows[0]["AUTO_ID_CARD"].ToString());
                    if (ds.Tables[0].Rows[0]["NO_COPIES"] != DBNull.Value)
                        objProcess.NO_COPIES = Convert.ToInt32(ds.Tables[0].Rows[0]["NO_COPIES"].ToString());
                    if (ds.Tables[0].Rows[0]["STD_LETTER_REQD"] != DBNull.Value)
                        objProcess.STD_LETTER_REQD = Convert.ToInt32(ds.Tables[0].Rows[0]["STD_LETTER_REQD"].ToString());
                    if (ds.Tables[0].Rows[0]["CUSTOM_LETTER_REQD"] != DBNull.Value)
                        objProcess.CUSTOM_LETTER_REQD = Convert.ToInt32(ds.Tables[0].Rows[0]["CUSTOM_LETTER_REQD"].ToString());
                    if (ds.Tables[0].Rows[0]["OTHER_RES_DATE_CD"] != DBNull.Value)
                        objProcess.OTHER_RES_DATE_CD = ds.Tables[0].Rows[0]["OTHER_RES_DATE_CD"].ToString();
                    if (ds.Tables[0].Rows[0]["OTHER_RES_DATE"] != DBNull.Value)
                        objProcess.OTHER_RES_DATE = ConvertToDate(ds.Tables[0].Rows[0]["OTHER_RES_DATE"].ToString());
                    if (ds.Tables[0].Rows[0]["APPLY_REINSTATE_FEE"] != DBNull.Value)
                        objProcess.APPLY_REINSTATE_FEE = Convert.ToInt32(ds.Tables[0].Rows[0]["APPLY_REINSTATE_FEE"].ToString());
                    if (ds.Tables[0].Rows[0]["ENDORSEMENT_OPTION"] != DBNull.Value)
                        objProcess.ENDORSEMENT_OPTION = int.Parse(ds.Tables[0].Rows[0]["ENDORSEMENT_OPTION"].ToString());
                    if (ds.Tables[0].Rows[0]["ENDORSEMENT_TYPE"] != DBNull.Value)
                        objProcess.ENDORSEMENT_TYPE = int.Parse(ds.Tables[0].Rows[0]["ENDORSEMENT_TYPE"].ToString());

                    if (ds.Tables[0].Rows[0]["ENDORSEMENT_RE_ISSUE"] != DBNull.Value)
                        objProcess.ENDORSEMENT_RE_ISSUE = int.Parse(ds.Tables[0].Rows[0]["ENDORSEMENT_RE_ISSUE"].ToString());

                    if (ds.Tables[0].Rows[0]["SOURCE_VERSION_ID"] != DBNull.Value)
                        objProcess.SOURCE_VERSION_ID = int.Parse(ds.Tables[0].Rows[0]["SOURCE_VERSION_ID"].ToString());


                    return objProcess;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception objExp)
            {
                throw (objExp);
            }
        }
        #endregion

        #region Set the Status of the Process table record.
        /// <summary>
        /// It will set the status of the process table record.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public void SetProcessStatus(int CustomerID, int PolicyID, int PolicyVersionID, int RowID, string StatusDesc)
        {
            string strStoredProc = "Proc_SetProcessStatus";
            int returnResult = 0;

            try
            {
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objWrapper.AddParameter("@ROW_ID", RowID);
                objWrapper.AddParameter("@PROCESS_STATUS", StatusDesc);

                returnResult = objWrapper.ExecuteNonQuery(strStoredProc);
                objWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion
        #endregion

        #region Check Eligibilty of Launching the Process
        /// <summary>
        /// It will checks the Eligibility for launching the process.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public int CheckProcessEligibility(int CustomerID, int PolicyID, int PolicyVersionID, int ProcessID)
        {
            string strStoredProc = "Proc_CheckProcessEligibility";
            int returnResult = 0;

            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objWrapper.AddParameter("@PROCESS_ID", ProcessID);

                SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@RETVAL", SqlDbType.Int, ParameterDirection.Output);

                returnResult = objWrapper.ExecuteNonQuery(strStoredProc);

                if (objSqlParameter.Value != System.DBNull.Value)
                {
                    returnResult = Convert.ToInt32(objSqlParameter.Value);
                }
                objWrapper.ClearParameteres();
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region Check Credit Balance before Launching the Process
        /// <summary>
        /// It will checks the Credit Balance Eligibility before launching the process.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns></returns>
        public int CheckCreditBalanceEligibility(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            string strStoredProc = "Proc_CheckCreditBalanceEligibility";
            int returnResult = 0;

            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);

                SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@RETVAL", SqlDbType.Int, ParameterDirection.Output);

                returnResult = objWrapper.ExecuteNonQuery(strStoredProc);

                if (objSqlParameter.Value != System.DBNull.Value)
                {
                    returnResult = Convert.ToInt32(objSqlParameter.Value);
                }
                objWrapper.ClearParameteres();
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region
        ///<summary>
        ///It will checks Agency Termination Verification
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public int AgenyTerminationVerification(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            return AgenyTerminationVerification(CustomerID, PolicyID, PolicyVersionID, POLICY_RENEWAL_PROCESS);
        }
        public int AgenyTerminationVerification(int CustomerID, int PolicyID, int PolicyVersionID, int ProcessID)
        {
            string strStoredProc = "Proc_CheckAgenyTermination";
            int returnResult = 0;

            try
            {
                DataWrapper objWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objWrapper.AddParameter("@PROCESS_ID", ProcessID);

                SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@RETVAL", SqlDbType.Int, ParameterDirection.Output);

                returnResult = objWrapper.ExecuteNonQuery(strStoredProc);

                if (objSqlParameter.Value != System.DBNull.Value)
                {
                    returnResult = Convert.ToInt32(objSqlParameter.Value);
                }
                objWrapper.ClearParameteres();
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public int CheckDoNotRenew(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            string strStoredProc = "Proc_CheckDoNotRenew";
            int returnResult = 0;

            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);

                SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@RETVAL", SqlDbType.Int, ParameterDirection.Output);

                returnResult = objWrapper.ExecuteNonQuery(strStoredProc);

                if (objSqlParameter.Value != System.DBNull.Value)
                {
                    returnResult = Convert.ToInt32(objSqlParameter.Value);
                }
                objWrapper.ClearParameteres();
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }
        #endregion
        /// <summary>
        /// Updating the policy term,effective date and expiry date of new version while Reinstatement Process
        /// </summary>
        /// <param name="objProcessInfo"></param>
        /// <returns></returns>
        public int UpdatePolicyTerm_EffectiveExpirydate(ClsProcessInfo objProcessInfo)
        {
            try
            {
                string strStoredProc = "Proc_UpdatePolicyTermEffectiveDate";
                int retVal = 0;
                objWrapper.ClearParameteres();
                //DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
                objWrapper.AddParameter("@CUSTOMER_ID", objProcessInfo.CUSTOMER_ID);
                objWrapper.AddParameter("@POLICY_ID", objProcessInfo.POLICY_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", objProcessInfo.NEW_POLICY_VERSION_ID);

                //comment by kranti on 25th april 2007
                //objWrapper.AddParameter("@POLICY_EFFECTIVE_DATE",objProcessInfo.EFFECTIVE_DATETIME);				
                objWrapper.AddParameter("@POLICY_EFFECTIVE_DATE", objProcessInfo.NEW_POLICY_TERM_EFFECTIVE_DATE);


                objWrapper.AddParameter("@POLICY_EXPIRY_DATE", objProcessInfo.NEW_POLICY_TERM_EXPIRATION_DATE);
                objWrapper.AddParameter("@POLICY_TERMS", objProcessInfo.POLICY_TERMS);
                if (objProcessInfo.POLICY_TYPE != 0)
                    objWrapper.AddParameter("@POLICY_TYPE", objProcessInfo.POLICY_TYPE);

                retVal = objWrapper.ExecuteNonQuery(strStoredProc);
                objWrapper.ClearParameteres();
                return retVal;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        #region verifying the rules
        /// chk for application referred vs rejected cases
        public string ChkReferedRejCaese(string strRulesHTML)
        {
            try
            {
                System.Xml.XmlDocument objXmlDocument = new System.Xml.XmlDocument();
                strRulesHTML = strRulesHTML.Replace("\t", "");
                strRulesHTML = strRulesHTML.Replace("\r\n", "");
                strRulesHTML = strRulesHTML.Replace("\r", "");
                strRulesHTML = strRulesHTML.Replace("\n", "");
                strRulesHTML = strRulesHTML.Replace("<LINK", "<!-- <LINK");
                strRulesHTML = strRulesHTML.Replace(" rel=\"stylesheet\">", "rel=\"stylesheet/\"> -->");
                strRulesHTML = strRulesHTML.Replace("<META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-16\">", "");
                objXmlDocument.LoadXml("<RULEHTML>" + strRulesHTML + "</RULEHTML>");

                //chk for referred

                System.Xml.XmlNodeList objXmlNodeList = objXmlDocument.GetElementsByTagName("ReferedStatus");
                System.Xml.XmlNodeList objXmlNodeListRej = objXmlDocument.GetElementsByTagName("returnValue");
                if ((objXmlNodeList != null && objXmlNodeList.Count > 0) || (objXmlNodeListRej != null && objXmlNodeListRej.Count > 0))
                {

                    if (objXmlNodeListRej.Item(0).InnerText == "0")
                    {
                        return "Unable to commit process.Because Policy has been rejected.";

                    }
                    else if (objXmlNodeList.Item(0).InnerText == "0")
                    {

                        return "Unable to commit process.Because Policy has been referred.";

                    }
                    else
                    {
                        return "Unable to commit process.";
                    }
                }
                else
                {
                    return "Unable to commit process.Please fill the mandatory information.";

                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion
        #region Setting Status of the Active Policy

        /// <summary>
        /// It will the Status of the Policy on which the process has been launched.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="PolicyStatus"></param>
        /// <returns></returns>
        public void SetPolicyStatus(int CustomerID, int PolicyID, int PolicyVersionID, string PolicyStatus)
        {
            string ProcessDesc, NewPolicyStatus = "";
            SetPolicyStatus(CustomerID, PolicyID, PolicyVersionID, 0, PolicyStatus, out ProcessDesc, out NewPolicyStatus);
        }

        /// <summary>
        /// It will the Status of the Policy on which the process has been launched.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public void SetPolicyStatus(int CustomerID, int PolicyID, int PolicyVersionID, int ProcessID)
        {
            string ProcessDesc = "", NewPolicyStatus = "";
            SetPolicyStatus(CustomerID, PolicyID, PolicyVersionID, ProcessID, null, out ProcessDesc, out NewPolicyStatus);
        }

        public void SetPolicyStatus(int CustomerID, int PolicyID, int PolicyVersionID, int ProcessID, string PolicyStatus)
        {
            string ProcessDesc = "", NewPolicyStatus = "";
            SetPolicyStatus(CustomerID, PolicyID, PolicyVersionID, ProcessID, PolicyStatus, out ProcessDesc, out NewPolicyStatus);
        }

        public void SetPolicyStatus(int CustomerID, int PolicyID, int PolicyVersionID, int ProcessID, out string PolicyStatusDesc, out string NewPolicyStatus)
        {
            SetPolicyStatus(CustomerID, PolicyID, PolicyVersionID, ProcessID, null, out PolicyStatusDesc, out NewPolicyStatus);
        }

        /// <summary>
        /// It will the Status of the Policy on which the process has been launched.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="PolicyStatus"></param>
        /// <returns></returns>
        private void SetPolicyStatus(int CustomerID, int PolicyID, int PolicyVersionID, int ProcessID, string PolicyStatus, out string PolicyStatusDesc, out string NewPolicyStatus)
        {
            PolicyStatusDesc = ""; NewPolicyStatus = "";
            string strStoredProc = "Proc_SetPolicyStatus";
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objWrapper.AddParameter("@LANG_ID", BL_LANG_ID);
                SqlParameter Param = (SqlParameter)objWrapper.AddParameter("@POLICY_STATUS_DESC", null, SqlDbType.VarChar, ParameterDirection.Output, 30);
                SqlParameter prmNewPolicyStatus = (SqlParameter)objWrapper.AddParameter("@NEW_POLICY_STATUS", null, SqlDbType.VarChar, ParameterDirection.Output, 30);

                if (ProcessID == 0)
                    objWrapper.AddParameter("@PROCESS_ID", null);
                else
                    objWrapper.AddParameter("@PROCESS_ID", ProcessID);

                objWrapper.AddParameter("@POLICY_STATUS", PolicyStatus);

                objWrapper.ExecuteNonQuery(strStoredProc);
                PolicyStatusDesc = Param.Value.ToString();
                NewPolicyStatus = prmNewPolicyStatus.Value.ToString();
                objWrapper.ClearParameteres();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion

        #region Write Transaction Log
        /// <summary>
        /// It is used to Write the Transaction Log.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="TransactionDescription"></param>
        /// <returns></returns>
        public void WriteTransactionLog(int CustomerID, int PolicyID, int PolicyVersionID, string TransactionDescription, int RecordedBy, string ProcessDesc)
        {
            WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, TransactionDescription, RecordedBy, ProcessDesc, "", 0);
        }
        public void WriteTransactionLog(int CustomerID, int PolicyID, int PolicyVersionID, string TransactionDescription, int RecordedBy, string ProcessDesc, string TransactionChange)
        {
            WriteTransactionLog(CustomerID, PolicyID, PolicyVersionID, TransactionDescription, RecordedBy, ProcessDesc, TransactionChange, 0);
        }
        public void WriteTransactionLog(int CustomerID, int PolicyID, int PolicyVersionID, string TransactionDescription, int RecordedBy, string ProcessDesc, string TransactionChange, int TranTypeId)
        {
            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
            // 11 is taken as it's 'LOOKUP_VALUE_DESC' is null in table 'MNT_LOOKUP_VALUES'. This is need when value change from null to some value.
            TransactionChange = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(TransactionChange, "LabelFieldMapping/Map[@field='REQUESTED_BY' and @OldValue='0']", "OldValue", "11");
            //			TransactionChange = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(TransactionChange,"LabelFieldMapping/Map[@field='REQUESTED_BY' and @NewValue='0']","NewValue","");
            objTransactionInfo.CUSTOM_INFO = ProcessDesc;
            if (TranTypeId != 0)
                objTransactionInfo.TRANS_TYPE_ID = TranTypeId;
            else
                objTransactionInfo.TRANS_TYPE_ID = 3;
            objTransactionInfo.CLIENT_ID = CustomerID;
            objTransactionInfo.POLICY_ID = PolicyID;
            objTransactionInfo.POLICY_VER_TRACKING_ID = PolicyVersionID;
            if (IsEODProcess)
                objTransactionInfo.RECORDED_BY = EODUserID;
            else
                objTransactionInfo.RECORDED_BY = RecordedBy;
            objTransactionInfo.TRANS_DESC = TransactionDescription;
            objTransactionInfo.CHANGE_XML = TransactionChange;
            objWrapper.ExecuteNonQuery(objTransactionInfo);
            objWrapper.ClearParameteres();
        }
        #endregion

        #region Update Diary Entry Status
        /// <summary>
        /// It will update the Status of the Diary Entry.
        /// </summary>
        /// <param name="ListID"></param>
        /// <param name="ListOpen"></param>
        /// <returns></returns>
        public int SetDiaryEntryStatus(int ListID, string ListOpen)
        {
            ClsDiary objDiary = new ClsDiary();
            int returnResult = 0;

            try
            {
                returnResult = objDiary.CompleteDiaryEntry(objWrapper, ListID, ListOpen);
                objWrapper.ClearParameteres();
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion
        #region update followup date
        public int UpdateDiaryEntryFollowUpDate(ClsProcessInfo objProcessInfo, string strCalledFor)
        {
            ClsDiary objDiary = new ClsDiary();
            int returnResult = 0;

            try
            {
                //returnResult = objDiary.CompleteDiaryEntry(objWrapper, ListID, ListOpen);
                if (strCalledFor == "NEWVERSION")
                    returnResult = objDiary.UpdateDiaryEntryFollowUpDate(objWrapper, objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.ROW_ID, objProcessInfo.EFFECTIVE_DATETIME);
                else
                    returnResult = objDiary.UpdateDiaryEntryFollowUpDate(objWrapper, objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.ROW_ID, objProcessInfo.EFFECTIVE_DATETIME);
                objWrapper.ClearParameteres();
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion
        #region Update Diary Entry Status
        /// <summary>
        /// It will update the Status of the Diary Entry.
        /// </summary>
        /// <param name="ListID"></param>
        /// <param name="ListOpen"></param>
        /// <returns></returns>
        public int SetDiaryEntryStatus(ClsProcessInfo objProcessInfo, string ListOpen)
        {
            return SetDiaryEntryStatus(objProcessInfo, ListOpen, "");
        }
        public int SetDiaryEntryStatus(ClsProcessInfo objProcessInfo, string ListOpen, string strCalledFor)
        {
            ClsDiary objDiary = new ClsDiary();
            int returnResult = 0;
            string strCalledFrom;
            strCalledFrom = "";
            try
            {
                //returnResult = objDiary.CompleteDiaryEntry(objWrapper, ListID, ListOpen);
                strCalledFrom = objProcessInfo.PROCESS_ID.ToString();
                if (strCalledFor == "NEWVERSION")
                    returnResult = objDiary.CompleteDiaryEntry(objWrapper, objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.ROW_ID, ListOpen, strCalledFrom);
                else
                    returnResult = objDiary.CompleteDiaryEntry(objWrapper, objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.ROW_ID, ListOpen, strCalledFrom);

                objWrapper.ClearParameteres();
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region Add(Insert) Notice function

        /// <summary>
        /// Added the record in the Process Notice Master.
        /// </summary>
        /// <param name="objProcessNotice"></param>
        /// <returns></returns>
        public int AddProcessNotice(Cms.Model.Policy.Process.ClsPolicyProcessNoticInfo objProcessNotice, Cms.DataLayer.DataWrapper objWrapper)
        {
            string strStoredProc = "Proc_InsertPOL_POLICY_NOTICE_MASTER";
            DateTime RecordDate = DateTime.Now;

            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", objProcessNotice.CUSTOMER_ID);
                objWrapper.AddParameter("@POLICY_ID", objProcessNotice.POLICY_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", objProcessNotice.POLICY_VERSION_ID);
                objWrapper.AddParameter("@ROW_ID", objProcessNotice.ROW_ID);
                objWrapper.AddParameter("@NOTICE_DESCRIPTION", objProcessNotice.NOTICE_DESCRIPTION);
                objWrapper.AddParameter("@NOTICE_TYPE", objProcessNotice.NOTICE_TYPE);

                int returnResult = 0;
                returnResult = objWrapper.ExecuteNonQuery(strStoredProc);
                objWrapper.ClearParameteres();

                return returnResult;

            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding data to Policy Notice Master table", ex);
            }
        }

        /// <summary>
        /// Added the record in the Process Notice Master.
        /// </summary>
        /// <param name="objProcessNotice"></param>
        /// <returns></returns>
        public int AddProcessNotice(Cms.Model.Policy.Process.ClsPolicyProcessNoticInfo objProcessNotice)
        {
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            int RetVal = AddProcessNotice(objProcessNotice, objWrapper);
            objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            return RetVal;
        }
        #endregion

        #region Get Diary Entry List ID from Policy Process Table.
        /// <summary>
        /// It will get the diary entry list from policy process table.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="ProcessID"></param>
        /// <returns></returns>
        public int GetDiaryListID(int CustomerID, int PolicyID, int PolicyVersionID, int RowID)
        {
            string strStoredProc = "Proc_GetDiaryEntryListID";
            int returnResult = 0;

            try
            {
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objWrapper.AddParameter("@ROW_ID", RowID);

                SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@DIARY_LIST_ID", SqlDbType.Int, ParameterDirection.Output);

                returnResult = objWrapper.ExecuteNonQuery(strStoredProc);

                if (objSqlParameter.Value != System.DBNull.Value)
                {
                    returnResult = Convert.ToInt32(objSqlParameter.Value);
                }
                objWrapper.ClearParameteres();
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion


        #region CreateNewVersion
        /// <summary>
        /// Creates the new version of policy and gives the new version as an out parameter
        /// </summary>
        /// <param name="PolicyId">Policy id</param>
        /// <param name="PolicyVersionId">Policy Version id</param>
        /// <param name="CustomerId">Customer id</param>
        /// <param name="NewVersionID">New version that generetes</param>
        public void CreatePolicyNewVersion(int PolicyId, int PolicyVersionId, int CustomerId, string CreatedBy, out int NewVersionID)
        {
            string newDispVersion;
            CreatePolicyNewVersion(PolicyId, PolicyVersionId, CustomerId, CreatedBy, out NewVersionID, out newDispVersion);
        }
        public void CreatePolicyNewVersion(int PolicyId, int PolicyVersionId, int CustomerId, string CreatedBy, out int NewVersionID, out string NewDispVersion)
        {
            CreatePolicyNewVersion(PolicyId, PolicyVersionId, CustomerId, CreatedBy, out NewVersionID, out NewDispVersion, "0");
        }
        public void CreatePolicyNewVersion(int PolicyId, int PolicyVersionId, int CustomerId, string CreatedBy, out int NewVersionID, out string NewDispVersion, string CalledFrom)
        {
            CreatePolicyNewVersion(PolicyId, PolicyVersionId, CustomerId, CreatedBy, out NewVersionID, out NewDispVersion, CalledFrom,"");

        }
        public void CreatePolicyNewVersion(int PolicyId, int PolicyVersionId, int CustomerId, string CreatedBy, out int NewVersionID, out string NewDispVersion, string CalledFrom,string CALLED_FROM2)
        {
            int returnResult = 0;

            try
            {

                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerId, SqlDbType.Int);
                objWrapper.AddParameter("@POLICY_ID", PolicyId, SqlDbType.Int);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId, SqlDbType.Int);
                objWrapper.AddParameter("@CREATED_BY", CreatedBy, SqlDbType.Int);
                //values for @Renewal
                //  1 is passed in case of renewal ->>0 is default value ->>3 In Case of Rewrite ->>12 for CANCELLATION ->>29 FOR RECISSION ->>16 FOR REINSTATEMENT
                objWrapper.AddParameter("@Renewal", CalledFrom, SqlDbType.NVarChar);
                objWrapper.AddParameter("@CALLED_FROM", CALLED_FROM2, SqlDbType.NVarChar);

                SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@NEW_VERSION", SqlDbType.Int, ParameterDirection.Output);
                SqlParameter objSqlParameter1 = (SqlParameter)objWrapper.AddParameter("@NEW_DISP_VERSION", null, SqlDbType.NVarChar, ParameterDirection.Output, 50);

                SqlParameter objSqlParameter2 = (SqlParameter)objWrapper.AddParameter("@INVALID_COVERAGE", SqlDbType.Int, ParameterDirection.Output);
                int new_APP_Version_ID;
                string New_Disp_version;

                returnResult = objWrapper.ExecuteNonQuery("Proc_PolicyCreateNewVersion");

                objWrapper.ClearParameteres();

                new_APP_Version_ID = int.Parse(objSqlParameter.Value.ToString());
                New_Disp_version = objSqlParameter1.Value.ToString();

                NewVersionID = new_APP_Version_ID;
                NewDispVersion = New_Disp_version;

                //Added by Anurag verma on 21/03/2007 for making diary entry
                int intErrorCnt = int.Parse(objSqlParameter2.Value.ToString());
                if (intErrorCnt > 0)
                    AddDiaryEntry(CustomerId, PolicyId, new_APP_Version_ID);

            }
            catch (Exception ex)
            {
                throw (new Exception("Error occured while creating new version of policy.\n" + ex.Message));
            }

        }
        #endregion
        //Added by Praveen Overload to Description
        //public void CreatePolicyNewVersion(int PolicyId, int PolicyVersionId, int CustomerId, string CreatedBy, out int NewVersionID, out string TransDescription)
        //{
        //    string newDispVersion;
        //    CreatePolicyNewVersion(PolicyId, PolicyVersionId, CustomerId, CreatedBy, out NewVersionID, out newDispVersion, out TransDescription);
        //}
        public void CreatePolicyNewVersion(int PolicyId, int PolicyVersionId, int CustomerId, string CreatedBy, out int NewVersionID, out string NewDispVersion, out string TransDescription)
        {
            int returnResult = 0;

            try
            {

                objWrapper.AddParameter("@CUSTOMER_ID", CustomerId, SqlDbType.Int);
                objWrapper.AddParameter("@POLICY_ID", PolicyId, SqlDbType.Int);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId, SqlDbType.Int);
                objWrapper.AddParameter("@CREATED_BY", CreatedBy, SqlDbType.Int);
                SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@NEW_VERSION", SqlDbType.Int, ParameterDirection.Output);
                SqlParameter objSqlParameter1 = (SqlParameter)objWrapper.AddParameter("@NEW_DISP_VERSION", null, SqlDbType.NVarChar, ParameterDirection.Output, 50);


                SqlParameter objSqlParameter2 = (SqlParameter)objWrapper.AddParameter("@INVALID_COVERAGE", SqlDbType.Int, ParameterDirection.Output);

                SqlParameter objSqlParameter3 = (SqlParameter)objWrapper.AddParameter("@TRAN_DESC", null, SqlDbType.NVarChar, ParameterDirection.Output, 500);

                int new_APP_Version_ID;
                string New_Disp_version;

                returnResult = objWrapper.ExecuteNonQuery("Proc_PolicyCreateNewVersion");

                objWrapper.ClearParameteres();

                new_APP_Version_ID = int.Parse(objSqlParameter.Value.ToString());
                New_Disp_version = objSqlParameter1.Value.ToString();

                NewVersionID = new_APP_Version_ID;
                NewDispVersion = New_Disp_version;

                //Added by Praveen
                TransDescription = "";
                if (objSqlParameter3.Value != System.DBNull.Value)
                    TransDescription = objSqlParameter3.Value.ToString();

                //Added by Anurag verma on 21/03/2007 for making diary entry
                int intErrorCnt = int.Parse(objSqlParameter2.Value.ToString());
                if (intErrorCnt > 0)
                    AddDiaryEntry(CustomerId, PolicyId, new_APP_Version_ID);

            }
            catch (Exception ex)
            {
                throw (new Exception("Error occured while creating new version of policy.\n" + ex.Message));
            }

        }
        /// <summary>
        /// This function is used to make diary entry if ineligible coverage is found
        /// </summary>
        /// <param name="custID">Customer ID</param>
        /// <param name="polID">policy ID</param>
        /// <param name="PolverID">New Policy version ID</param>
        public void AddDiaryEntry(int custID, int polID, int PolverID, int LobID, int FromUserID)
        {
            TodolistInfo objTodo = new TodolistInfo();


            objTodo.CUSTOMER_ID = custID;
            objTodo.POLICY_ID = polID;
            objTodo.POLICY_VERSION_ID = PolverID;
            objTodo.LISTTYPEID = (int)ClsDiary.enumDiaryType.REVIEWING_POLICY_REQUEST;
            objTodo.RECDATE = System.DateTime.Now;
            objTodo.MODULE_ID = (int)ClsDiary.enumModuleMaster.APPLICATION;
            objTodo.LISTOPEN = "Y";

            objTodo.FROMUSERID = FromUserID;
            objTodo.LOB_ID = LobID;

            objTodo.FOLLOWUPDATE = System.DateTime.Now;
            objTodo.SUBJECTLINE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1920", ""); //"Ineligible Coverages Not Copied in Renewal";

            Cms.BusinessLayer.BlCommon.ClsDiary objDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
            ArrayList alresult = new ArrayList();
            try
            {
                alresult = objDiary.DiaryEntryfromSetup(objTodo);
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                if (objDiary != null)
                    objDiary.Dispose();
            }
        }

        /// <summary>
        /// This function is used to make diary entry if ineligible coverage is found
        /// </summary>
        /// <param name="custID">Customer ID</param>
        /// <param name="polID">policy ID</param>
        /// <param name="PolverID">New Policy version ID</param>
        public void AddDiaryEntry(int custID, int polID, int PolverID)
        {
            TodolistInfo objTodo = new TodolistInfo();


            objTodo.CUSTOMER_ID = custID;
            objTodo.POLICY_ID = polID;
            objTodo.POLICY_VERSION_ID = PolverID;
            objTodo.LISTTYPEID = (int)ClsDiary.enumDiaryType.REVIEWING_POLICY_REQUEST;
            objTodo.RECDATE = System.DateTime.Now;
            objTodo.MODULE_ID = (int)ClsDiary.enumModuleMaster.APPLICATION;
            objTodo.LISTOPEN = "Y";
            objTodo.FROMUSERID = int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());
            objTodo.LOB_ID = int.Parse(System.Web.HttpContext.Current.Session["lobID"].ToString());
            objTodo.FOLLOWUPDATE = System.DateTime.Now;
            objTodo.SUBJECTLINE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1920", "");//"Ineligible Coverages Not Copied in Renewal";

            Cms.BusinessLayer.BlCommon.ClsDiary objDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
            ArrayList alresult = new ArrayList();
            try
            {
                alresult = objDiary.DiaryEntryfromSetup(objTodo);
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                if (objDiary != null)
                    objDiary.Dispose();
            }
        }


        #region Delete Policy Version
        public void DeletePolicyVersion(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                int returnResult = objWrapper.ExecuteNonQuery("Proc_PolicyDeleteNewVersion");
                objWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion
        #region Update Customer Balance
        public void UpdatePolicyVersionForActCustBalance(int CustomerID, int PolicyID, int PolicyVersionID, int newPolicyVerID)
        {
            try
            {
                objWrapper.ClearParameteres();	//Charles (1-Dec-09), Itrack 6792
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objWrapper.AddParameter("@NEW_POLICY_VERSION_ID", newPolicyVerID);

                int returnResult = objWrapper.ExecuteNonQuery("Proc_UpdatePolicyOldVersion");
                objWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion

        #region Common functions

        /// <summary>
        /// Method to evaluate whether we require transaction log for the current process or not.
        /// We require T-log for save/update and not for launch/commit. For launch/commit, we have composite
        /// T-log.
        /// </summary>
        /// <param name="ProcessId">Current Process ID</param>
        /// <returns>true if T-log is required, else not</returns>
        public void ProcessTransLogReqd(int ProcessId, out bool TranLogReqd, out string ResourceFilePath)
        {
            TranLogReqd = true;
            ResourceFilePath = "";
            switch (ProcessId)
            {
                case POLICY_NEW_BUSINESS_PROCESS:
                    ResourceFilePath = "/policies/processes/NewBusinessProcess.aspx.resx";
                    break;
                case POLICY_CANCELLATION_PROCESS:
                    ResourceFilePath = "/policies/processes/CancellationProcess.aspx.resx";
                    break;
                case POLICY_RENEWAL_PROCESS:
                    ResourceFilePath = "/policies/processes/RenewalProcess.aspx.resx";
                    break;
                case POLICY_ENDORSEMENT_PROCESS:
                    ResourceFilePath = "/policies/processes/EndorsementProcess.aspx.resx";
                    break;
                case POLICY_REINSTATEMENT_PROCESS:
                    ResourceFilePath = "/policies/processes/ReinstateProcess.aspx.resx";
                    break;
                case POLICY_CORRECTIVE_USER_PROCESS:
                    ResourceFilePath = "/policies/processes/CorrectiveUserProcess.aspx.resx";
                    break;
                case POLICY_RESCIND:
                    ResourceFilePath = "/policies/processes/RescindProcess.aspx.resx";
                    break;
                case POLICY_REWRITE_PROCESS:
                    ResourceFilePath = "/policies/processes/RewriteProcess.aspx.resx";
                    break;
                case POLICY_REVERT_PROCESS:
                    ResourceFilePath = "/policies/processes/RollbackAfterCommit.aspx.resx";
                    break;
                case POLICY_NON_RENEWAL_PROCESS:
                    ResourceFilePath = "/policies/processes/NonRenewProcess.aspx.resx";
                    break;
                default:
                    ResourceFilePath = "";
                    TranLogReqd = false;
                    break;
            }
        }

        //Additional function added to obtain the diary message & diary type id
        public string GetTransactionLogDesc(int ProcessID)
        {
            int DiaryTypeId = 0;
            return GetTransactionLogDesc(ProcessID, out DiaryTypeId);
        }


        /// <summary>
        /// Returns the transaction log description for the specified process
        /// </summary>
        /// <param name="ProcessID">Process id</param>
        /// <param name="ProcessStatus">Process status</param>
        /// <returns></returns>
        public string GetTransactionLogDesc(int ProcessID, out int DiaryTypeId)
        {
            return GetTransactionLogDesc(ProcessID, out DiaryTypeId, 0);
        }
        public string GetTransactionLogDesc(int ProcessID, out int DiaryTypeId, int type)
        {
            DiaryTypeId = 7;


            switch (ProcessID)
            {
                case POLICY_CANCELLATION_PROCESS:
                    if (type == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NON_PAYMENT).ToString()))
                    {
                        DiaryTypeId = 40;
                        return FetchGeneralMessage("1129", ""); //"Nonpayment Cancellation process launched successfully."; 
                    }
                    else
                    {
                        DiaryTypeId = 3;
                        return FetchGeneralMessage("1130", ""); //"Cancellation process launched successfully.";
                    }
                case POLICY_COMMIT_CANCELLATION_PROCESS:
                    DiaryTypeId = 3;
                    return FetchGeneralMessage("1131", "");//"Cancellation process completed successfully.";					
                case POLICY_ROLLBACK_CANCELLATION_PROCESS:
                    DiaryTypeId = 3;
                    return FetchGeneralMessage("1132", ""); //"Cancellation process rolled back successfully.";
                case POLICY_RESCIND:
                    DiaryTypeId = 36;
                    return FetchGeneralMessage("1133", "");//"Rescind process launched successfully.";
                case POLICY_COMMIT_RESCIND_PROCESS:
                    DiaryTypeId = 36;
                    return FetchGeneralMessage("1134", "");//"Rescind process completed successfully";
                case POLICY_ROLLBACK_RESCIND_PROCESS:
                    DiaryTypeId = 36;
                    return FetchGeneralMessage("1135", "");//"Rescind process rolled back successfully";
                case POLICY_REINSTATEMENT_PROCESS:
                    DiaryTypeId = 4;
                    return FetchGeneralMessage("1136", ""); //"Reinstatement process launched successfully.";
                case POLICY_COMMIT_REINSTATEMENT_PROCESS:
                    DiaryTypeId = 4;
                    return FetchGeneralMessage("1137", ""); //"Reinstatement process completed successfully";
                case POLICY_ROLLBACK_REINSTATEMENT_PROCESS:
                    DiaryTypeId = 4;
                    return FetchGeneralMessage("1138", ""); //"Reinstatement process rolled back successfully";
                case POLICY_ENDORSEMENT_PROCESS:
                    DiaryTypeId = 8;
                    return FetchGeneralMessage("1139", ""); //"Endorsement process launched successfully.";
                case POLICY_SUSPENSE_ENDORSEMENT_PROCESS:
                    DiaryTypeId = 26;
                    return FetchGeneralMessage("1140", "");//"Endorsement in suspense launched successfully.";
                case POLICY_COMMIT_ENDORSEMENT_PROCESS:
                    DiaryTypeId = 8;
                    return FetchGeneralMessage("1141", ""); //"Endorsement process completed successfully.";
                case POLICY_ROLLBACK_ENDORSEMENT_PROCESS:
                    DiaryTypeId = 8;
                    return FetchGeneralMessage("1142", ""); //"Endorsement process rolled back successfully.";					
                case POLICY_NEGATE_PROCESS:
                    /*					if (ProcessStatus == PROCESS_STATUS_COMPLETE)*/
                    return FetchGeneralMessage("1143", "");//"Negate process completed successfully.";
                /*else if (ProcessStatus == PROCESS_STATUS_PENDING)
                    return "Negate process launched successfully.";
                else 
                    return "Negate process rolled back successfully.";
*/
                case POLICY_NEW_BUSINESS_PROCESS:
                    DiaryTypeId = 7;
                    return FetchGeneralMessage("1125", ""); //"New business process launched successfully.";
                case POLICY_ROLLBACK_NEW_BUSINESS_PROCESS:
                    DiaryTypeId = 7;
                    return FetchGeneralMessage("1126", ""); //"New business process rolled back successfully.";
                case POLICY_COMMIT_NEW_BUSINESS_PROCESS:
                    DiaryTypeId = 7;
                    return FetchGeneralMessage("1127", ""); //"New business process completed successfully.";
                case POLICY_DECLINE:
                    return FetchGeneralMessage("1144", ""); //"Policy has been declined successfully.";
                case POLICY_NON_RENEWAL_PROCESS:
                    DiaryTypeId = 22;
                    return FetchGeneralMessage("1145", "");//"Non-Renewal process launched successfully.";
                case POLICY_ROLLBACK_NON_RENEWAL_PROCESS:
                    DiaryTypeId = 22;
                    return FetchGeneralMessage("1146", "");//"Non-Renewal process rolled back successfully.";
                case POLICY_COMMIT_NON_RENEWAL_PROCESS:
                    DiaryTypeId = 22;
                    return FetchGeneralMessage("1147", "");//"Non-Renewal process completed successfully.";										
                case POLICY_RENEWAL_PROCESS:
                    DiaryTypeId = 2;
                    return FetchGeneralMessage("1148", "");//"Renewal process launched successfully.";
                case POLICY_ROLLBACK_RENEWAL_PROCESS:
                    DiaryTypeId = 2;
                    return FetchGeneralMessage("1149", "");//"Renewal process rolled back successfully.";
                case POLICY_COMMIT_RENEWAL_PROCESS:
                    DiaryTypeId = 2;
                    return FetchGeneralMessage("1150", "");//"Renewal process completed successfully.";
                case POLICY_REWRITE_PROCESS:
                    DiaryTypeId = 23;
                    return FetchGeneralMessage("1151", "");//"Rewrite process launched successfully.";
                case POLICY_COMMIT_REWRITE_PROCESS:
                    DiaryTypeId = 23;
                    return FetchGeneralMessage("1152", "");//"Rewrite process completed successfully.";
                case POLICY_ROLLBACK_REWRITE_PROCESS:
                    DiaryTypeId = 23;
                    return FetchGeneralMessage("1153", "");//"Rewrite process rolled back successfully.";
                case POLICY_REVERT_PROCESS:
                    DiaryTypeId = 24;
                    return FetchGeneralMessage("1154", "");//"Revert back process launched successfully.";
                case POLICY_COMMIT_REVERT_PROCESS:
                    DiaryTypeId = 24;
                    return FetchGeneralMessage("1155", "");//"Revert back process completed successfully.";
                case POLICY_ROLLBACK_REVERT_PROCESS:
                    DiaryTypeId = 24;
                    return FetchGeneralMessage("1156", "");//"Revert back process rolled back successfully.";
                case POLICY_CORRECTIVE_USER_PROCESS:
                    DiaryTypeId = 33;
                    return FetchGeneralMessage("1157", "");//"Corrective User process launched successfully.";	
                case POLICY_COMMIT_CORRECTIVE_USER_PROCESS:
                    DiaryTypeId = 33;
                    return FetchGeneralMessage("1158", "");//"Corrective User process completed successfully.";
                case POLICY_ROLLBACK_CORRECTIVE_USER_PROCESS:
                    DiaryTypeId = 33;
                    return FetchGeneralMessage("1159", "");//"Corrective User process rolled back successfully.";	  
            }
            return "";
        }



        /// <summary>
        /// Populate the Model Object from XML.
        /// </summary>
        /// <param name="objModel"></param>
        /// <param name="xmlValue"></param>
        public void PopulateModelObject(Model.IModelInfo objModel, string xmlValue)
        {
            //Get the table schema from the Model object
            System.Data.DataTable TempIndt = objModel.TableInfo;

            //	
            System.Data.DataTable TempInXml = objModel.TableInfo;

            string strValue = "";

            // Creating the XML Document
            XmlDocument xmlDoc = new XmlDocument();
            // Loading the XML Value
            xmlDoc.LoadXml(xmlValue);

            //
            foreach (System.Data.DataColumn Col in TempIndt.Columns)
            {
                try
                {
                    if (xmlDoc.SelectNodes("//" + Col.ColumnName.ToUpper().Trim()).Count > 0)
                    {
                        strValue = xmlDoc.SelectNodes("//" + Col.ColumnName.ToUpper().Trim()).Item(0).InnerText.Trim();
                    }

                    //ClsCommon.GetNodeValue(xmlDoc,"//" +  Col.ColumnName.ToUpper()).Trim();	
                    // Checking the Data Type
                    switch (TempIndt.Columns[Col.ColumnName].DataType.ToString())
                    {
                        case "System.UInt16":
                        case "System.UInt32":
                        case "System.UInt64":
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Single":
                        case "System.Double":
                        case "System.Decimal":
                            if (strValue == "")
                            {
                                TempInXml.Rows[0][Col.ColumnName] = System.DBNull.Value;
                            }
                            else
                            {
                                TempInXml.Rows[0][Col.ColumnName] = strValue;
                            }
                            break;
                        case "System.Boolean":
                        case "System.String":
                            //if(strValue != "")
                            //{
                            strValue = ClsCommon.DecodeXMLCharacters(strValue);
                            TempInXml.Rows[0][Col.ColumnName] = strValue;
                            //}

                            break;
                        case "System.DateTime":
                            if (strValue == "")
                            {
                                TempInXml.Rows[0][Col.ColumnName] = System.DBNull.Value;
                            }
                            else
                            {
                                TempInXml.Rows[0][Col.ColumnName] = strValue;
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                    continue;
                }
            }
            strValue = "";
            objModel.TableInfo = TempInXml;
        }

        public static DateTime GetCancellationDateForEquity(int CustomerID, int PolicyID, int PolicyVersionID)
        {

            DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
            objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
            SqlParameter objDate = (SqlParameter)objDataWrapper.AddParameter("@CHANGE_EFFECTIVE_DATE", SqlDbType.DateTime, ParameterDirection.Output);
            objDataWrapper.ExecuteNonQuery("Proc_CalculateEquityCancellationDate");
            return Convert.ToDateTime(objDate.Value);
        }
        public static string GetAgencyGracePeriod(int CustomerID, int PolicyID, int PolicyVersionID)
        {

            DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
            objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
            DataSet dstemp;
            dstemp = objDataWrapper.ExecuteDataSet("Proc_GetPolicyBillPlanDetails");
            string RetVal = "0";
            if (dstemp.Tables[0].Rows.Count > 0)
                RetVal = dstemp.Tables[0].Rows[0]["GRACE_PERIOD"].ToString();
            return RetVal;
        }
        public static string CalculateReturnPremium(string CustomerId, string PolicyId, string PolicyVersionId,
            string ChangeEffDate, string CancellationType, string CancellatoinOption)
        {
            double result = CalculateReturnPremium(Convert.ToInt32(CustomerId), Convert.ToInt32(PolicyId),
                Convert.ToInt32(PolicyVersionId), Convert.ToDateTime(ChangeEffDate), Convert.ToInt32(CancellationType),
                Convert.ToInt32(CancellatoinOption));
            return result.ToString();

        }

        public static double CalculateReturnPremium(int CustomerId, int PolicyId, int PolicyVersionId,
            DateTime ChangeEffDate)
        {
            return CalculateReturnPremium(CustomerId, PolicyId, PolicyVersionId, ChangeEffDate, 0, 0);
        }

        //public static double CalculateReturnPremium(int CustomerId, int PolicyId, int PolicyVersionId,
        //    DateTime ChangeEffDate, int CancellationType, int CancellatoinOption)
        //{
        //    try
        //    {
        //        DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
        //        objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
        //        objDataWrapper.AddParameter("@POLICY_ID", PolicyId);
        //        objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
        //        objDataWrapper.AddParameter("@CHANGE_EFFECTIVE_DATE", Convert.ToDateTime(ChangeEffDate));
        //        objDataWrapper.AddParameter("@CANCELLTION_TYPE", CancellationType);
        //        objDataWrapper.AddParameter("@CANCELLATION_OPTION", CancellatoinOption);
        //        SqlParameter objSqlCancelPrem = (SqlParameter)objDataWrapper.AddParameter("@CANCELLATION_PREMIUM", SqlDbType.Int, ParameterDirection.Output);
        //        objDataWrapper.ExecuteNonQuery("Proc_CalculateReturnPremium");
        //        objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
        //        if (objSqlCancelPrem != null && objSqlCancelPrem.Value.ToString() != "")
        //            return Convert.ToDouble(objSqlCancelPrem.Value);
        //        else
        //            return 0;
        //    }
        //    catch //(Exception ex)
        //    {
        //        return 0;
        //    }
        //    finally
        //    {
        //    }

        //}
        public static double CalculateReturnPremium(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID,
            DateTime CANCELLATION_DATE, int CancellationType, int CancellatoinOption)
        {
            DataSet ds = null;
            DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                objDataWrapper.AddParameter("@CANCELLATION_EFFECTIVE_DATE", CANCELLATION_DATE);
                ds = objDataWrapper.ExecuteDataSet("PROC_GET_CANCELLATION_RETURN_PREMIUM");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    double canPrm = Convert.ToDouble(ds.Tables[0].Rows[0]["RETURN_PREMIUM"]);
                    canPrm = Math.Round(canPrm, 0);
                    return canPrm;
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return 0;
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }


        public static double CalculateProRataAmount(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID,
         DateTime CANCELLATION_DATE)
        {
            DataSet ds = null;
            DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                objDataWrapper.AddParameter("@CANCELLATION_EFFECTIVE_DATE", CANCELLATION_DATE);
                ds = objDataWrapper.ExecuteDataSet("PROC_GetCacellationProAmount");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    double canPrm = Convert.ToDouble(ds.Tables[0].Rows[0]["RETURN_PREMIUM"]);
                    canPrm = Math.Round(canPrm, 0);
                    return canPrm;
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return 0;
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }

        public int PostCancellationPremium(int CustomerID, int PolicyID, int PolicyVersionID, int PolicyBaseVersion, double CancellationPremium, int SourceRow_ID,
            DateTime ChangeEffectiveDate, out string Description, out decimal CarryForwardAmount, int CommittedBy, int ProcessType,
            int CancellationType, int CancellatoinOption)
        {
            CarryForwardAmount = 0;
            ClsCancellationRule objRule = GetCancellationRule(CustomerID, PolicyID, PolicyBaseVersion, objWrapper);

            string strStoredProc = "Proc_PostPolicyProcessReturnPremium";

            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objWrapper.AddParameter("@OLD_VERSION_ID", PolicyBaseVersion);
                objWrapper.AddParameter("@CANCELLATION_PREMIUM", CancellationPremium);
                objWrapper.AddParameter("@ACTION_UNPAID_PREMIUM", objRule.ACTION_UNPAID_PREMIUM);
                objWrapper.AddParameter("@ACTION_RETURN_PREMIUM", objRule.ACTION_RETURN_PREMIUM);
                objWrapper.AddParameter("@BILL_TYPE", objRule.BILL_TYPE);
                objWrapper.AddParameter("@CHANGE_EFFECTIVE_DATE ", ChangeEffectiveDate);
                objWrapper.AddParameter("@SOURCE_ROW_ID", SourceRow_ID);
                objWrapper.AddParameter("@USER_ID", CommittedBy);

                //Added By Ravindra(08-11-2007) 
                objWrapper.AddParameter("@PROCESS_TYPE", ProcessType);
                //Ends Here
                SqlParameter objDesc = (SqlParameter)objWrapper.AddParameter("@TRANSACTION_DESC", null, SqlDbType.VarChar, ParameterDirection.Output, 1000);
                SqlParameter objCFAmt = (SqlParameter)objWrapper.AddParameter("@CFG_PREMIUM_AMOUNT", null, SqlDbType.Decimal, ParameterDirection.Output);
                SqlParameter objTotalCancellationPremium = (SqlParameter)objWrapper.AddParameter("@TOTAL_CANCELLATION_PREMIUM", null, SqlDbType.Decimal, ParameterDirection.Output);
                SqlParameter objMCCAFees = (SqlParameter)objWrapper.AddParameter("@CANCELLATION_MCCA_FEES ", null, SqlDbType.Decimal, ParameterDirection.Output);
                SqlParameter objOSTAFees = (SqlParameter)objWrapper.AddParameter("@CANCELLATION_OTHER_FEES", null, SqlDbType.Decimal, ParameterDirection.Output);


                objWrapper.AddParameter("@CANCELLTION_TYPE", CancellationType);
                objWrapper.AddParameter("@CANCELLATION_OPTION", CancellatoinOption);

                int returnResult = 0;
                returnResult = objWrapper.ExecuteNonQuery(strStoredProc);
                Description = objDesc.Value.ToString();
                CarryForwardAmount = Convert.ToDecimal(objCFAmt.Value);
                double TotalCancellationPremium = Convert.ToDouble(objTotalCancellationPremium.Value);
                double MCCAFees = Convert.ToDouble(objMCCAFees.Value);
                double OSTAFees = Convert.ToDouble(objOSTAFees.Value);
                double NetPremium = TotalCancellationPremium - MCCAFees - OSTAFees;

                //Commented By Ravindra(08-07-2007) Logic Moved to premium posting logic
                /*
                //Create Premium XML and post it to ACT_PREMIUM_PROCESS_DETAILS
                string strPremiumXML;
                strPremiumXML = "<PREMIUM><GROSSPREMIUM>" + TotalCancellationPremium.ToString() 
                    + "</GROSSPREMIUM><NETPREMIUM>" + NetPremium.ToString()
                    + "</NETPREMIUM><OTHERFEES>" + OSTAFees.ToString() 
                    + "</OTHERFEES><MCCAFEES> "+ MCCAFees.ToString() + "</MCCAFEES></PREMIUM>";
                InsertFinalPremium(CustomerID,PolicyID,PolicyVersionID,"O",ProcessType,
                    strPremiumXML,strPremiumXML,NetPremium); */


                objWrapper.ClearParameteres();

                // added by pravesh
                #region updating Written and enforce premium for this version
                //update Split Premium 
                Cms.BusinessLayer.BlQuote.clsSplitPremium objSplitPremium = new Cms.BusinessLayer.BlQuote.clsSplitPremium();
                objSplitPremium.UpdateWrittenEnforcePremiumSplit(CustomerID, PolicyID, PolicyVersionID, PolicyBaseVersion, ProcessType.ToString(), objWrapper);
                // end here
                #endregion

                return returnResult;
            }
            catch (Exception ex)
            {
                throw (new Exception("Error while Posting Premium.", ex));
            }
        }


        /// <summary>
        /// Transfer a policy to another Customer
        /// </summary>
        /// <param name="Policy_Id"></param>
        /// <param name="Policy_version_Id"></param>
        /// <param name="OldCustomer_Id"></param>
        /// <param name="NewCustomer_Id"></param>
        /// <returns></returns>
        public int TransferPolicyToNewCustomer(int Policy_Id, int Policy_version_Id, int OldCustomer_Id, int NewCustomer_Id, out int NewPolicyID, out int NewVersionID, int UserId, int intCSR, int intProducer)
        {
            int retVal = 0;
            string TransDesc = "";
            this.BeginTransaction();
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", OldCustomer_Id);
                objWrapper.AddParameter("@POLICY_ID", Policy_Id);
                objWrapper.AddParameter("@POLICY_VERSION_ID", Policy_version_Id);
                objWrapper.AddParameter("@NEW_CUSTOMER_ID", NewCustomer_Id);
                objWrapper.AddParameter("@CREATED_BY", UserId);
                objWrapper.AddParameter("@NEW_CSR", intCSR);
                objWrapper.AddParameter("@NEW_PRODUCER", intProducer);
                objWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID);
                SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@NEW_POLICY_VERSION_ID", SqlDbType.Int, ParameterDirection.Output);
                SqlParameter objSqlParameter1 = (SqlParameter)objWrapper.AddParameter("@NEW_POLICY_ID", SqlDbType.Int, ParameterDirection.Output);
                SqlParameter objSqlParameter2 = (SqlParameter)objWrapper.AddParameter("@TRAN_DESC", null, SqlDbType.NVarChar, ParameterDirection.Output, 500);
                int new_POL_Version_ID, intNewPolicyID;
                retVal = objWrapper.ExecuteNonQuery("Proc_TransferPolicy");
                objWrapper.ClearParameteres();

                new_POL_Version_ID = int.Parse(objSqlParameter.Value.ToString());
                intNewPolicyID = int.Parse(objSqlParameter1.Value.ToString());
                if (objSqlParameter2.Value != System.DBNull.Value)
                    TransDesc = objSqlParameter2.Value.ToString();

                NewVersionID = new_POL_Version_ID;
                NewPolicyID = intNewPolicyID;
                #region ENCRYPT/DECRYPT PAY_PAL_REF_ID
                //After or before saving information in table “ACT_POL_CREDIT_CARD_DETAILS” for
                //new policy we have to Decrypt the reference id (PAY_PAL_REF_ID) using old CUSTOMER_ID
                string ref_ID = "", PayPalRefID = "";

                DataSet dsTemp = null;
                dsTemp = GetPAY_PAL_REF_ID(OldCustomer_Id, Policy_Id, Policy_version_Id, objWrapper);
                if (dsTemp != null)
                {
                    if (dsTemp.Tables[0].Rows.Count > 0)
                    {
                        ref_ID = dsTemp.Tables[0].Rows[0]["PAY_PAL_REF_ID"].ToString();
                        if (ref_ID != "")
                        {
                            //DECRYPT REF ID USING OLD CUSTOMER ID
                            BlCommon.Security.ClsCrypto objCrypto = new Cms.BusinessLayer.BlCommon.Security.ClsCrypto
                                (BlCommon.Security.ClsSecurity.GetPasswordPhrase(OldCustomer_Id, Cms.BusinessLayer.BlAccount.ClsCreditCard.CREDIT_CARD_PHRASE));
                            PayPalRefID = objCrypto.Decrypt(ref_ID).ToString();

                            //ENCRYPT AGAIN USING NEW CUSTOMER ID
                            BlCommon.Security.ClsCrypto objCryptoENCRYPT = new Cms.BusinessLayer.BlCommon.Security.ClsCrypto
                                (BlCommon.Security.ClsSecurity.GetPasswordPhrase(NewCustomer_Id, Cms.BusinessLayer.BlAccount.ClsCreditCard.CREDIT_CARD_PHRASE));
                            PayPalRefID = objCryptoENCRYPT.Encrypt(PayPalRefID).ToString();

                            //UPDATE REF ID FOR NEW CUSTOMER AND POLICY ID
                            UpdatePAY_PAL_REF_ID(NewCustomer_Id, NewPolicyID, NewVersionID, PayPalRefID, objWrapper);

                        }


                    }
                }
                #endregion
                //base.AddDiaryEntry(CustomerId,PolicyId,new_APP_Version_ID,LobID,Convert.ToInt32(CreatedBy));
                string[] Trans = TransDesc.Split('~');
                this.WriteTransactionLog(NewCustomer_Id, NewPolicyID, NewVersionID, Trans[0].ToString(), UserId, "");
                if (Trans.Length > 1)
                {
                    if (Trans[1] != null)
                        this.WriteTransactionLog(OldCustomer_Id, Policy_Id, Policy_version_Id, Trans[1].ToString(), UserId, "");
                }
                this.CommitTransaction();
                return retVal;
            }
            catch (Exception ex)
            {
                this.RollbackTransaction();
                throw (new Exception("Error While Transfering Policy to New Customer", ex));

            }
        }
        #region ENCRYPT / DECRYPT PAY_PAL_ID FUNCTIONS ,GetPAY_PAL_REF_ID AND UpdatePAY_PAL_REF_ID
        /// <summary>
        /// To get PAY_PAL_REF_ID of Old Customer for Decryption while Rewrite Policy to another Customer
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolID"></param>
        /// <param name="PolVersionID"></param>
        /// <returns></returns>
        public DataSet GetPAY_PAL_REF_ID(int CustomerID, int PolID, int PolVersionID, DataWrapper objWrapper)
        {
            try
            {
                string strStoredProc = "Proc_GetPAY_PAL_REF_ID";
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POL_ID", PolID);
                objWrapper.AddParameter("@POL_VERSION_ID", PolVersionID);
                DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
                if (ds != null)
                    return ds;
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }
        public void UpdatePAY_PAL_REF_ID(int intCUSTOMER_ID, int intPOLICY_ID, int intPOLICY_VERSION_ID, string refPayPalID, DataWrapper objWrapper)
        {
            string strStoredProc = "Proc_Update_PAY_PAL_REF_ID";
            objWrapper.ClearParameteres();
            objWrapper.AddParameter("@CUSTOMER_ID", intCUSTOMER_ID);
            objWrapper.AddParameter("@POLICY_ID", intPOLICY_ID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", intPOLICY_VERSION_ID);
            objWrapper.AddParameter("@PAY_PAL_REF_ID", refPayPalID);
            int returnResult = 0;


            try
            {
                returnResult = objWrapper.ExecuteNonQuery(strStoredProc);
                objWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        /// <summary>
        /// Sets the Application Status.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="IsActive"></param>
        public void SetApplicationStatus(int CustomerID, int PolicyID, int PolicyVersionID, string IsActive)
        {
            int returnResult = 0;

            try
            {

                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objWrapper.AddParameter("@IS_ACTIVE", IsActive);

                returnResult = objWrapper.ExecuteNonQuery("Proc_SetApplicationIS_ACTIVE");
                objWrapper.ClearParameteres();

            }
            catch (Exception ex)
            {
                throw (new Exception("Error occured while updating status of the Application.\n" + ex.Message));
            }

        }
        /// <summary>
        /// Check For Notification for Agency
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns></returns>
        public int AgenyNotificationVerification(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            string strStoredProc = "Proc_CheckAgenyNotification";
            int returnResult = 0;
            DataWrapper objWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);

                SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@RETVAL", SqlDbType.Int, ParameterDirection.Output);

                returnResult = objWrapper.ExecuteNonQuery(strStoredProc);

                if (objSqlParameter.Value != System.DBNull.Value)
                {
                    returnResult = Convert.ToInt32(objSqlParameter.Value);
                }
                objWrapper.ClearParameteres();
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Set the Policy is Is Active or Not.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="IsActive"></param>
        public void SetPolicyIsActive(int CustomerID, int PolicyID, int PolicyVersionID, string IsActive)
        {
            int returnResult = 0;

            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objWrapper.AddParameter("@IS_ACTIVE", IsActive);

                returnResult = objWrapper.ExecuteNonQuery("Proc_SetPolicyIS_ACTIVE");
                objWrapper.ClearParameteres();

            }
            catch (Exception ex)
            {
                throw (new Exception("Error occured while updating isActive of the Policy.\n" + ex.Message));
            }

        }

        /// <summary>
        /// Set the Suspense Payment Status.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="strStatus"></param>
        public void SetSuspensePaymentStatus(int CustomerID, int PolicyID, int PolicyVersionID, string strStatus)
        {
            int returnResult = 0;

            try
            {

                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objWrapper.AddParameter("@ITEM_STATUS", strStatus);

                returnResult = objWrapper.ExecuteNonQuery("Proc_SetStatus_ACT_CUSTOMER_OPEN_ITEMS");
                objWrapper.ClearParameteres();

            }
            catch (Exception ex)
            {
                throw (new Exception("Error occured while updating Item Status of the Suspense payment.\n" + ex.Message));
            }

        }



        /// <summary>
        /// It will get the Suspense Payment Status.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns></returns>
        public string GetSuspensePaymentStatus(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            string returnResult = "";

            try
            {

                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);

                SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@ITEM_STATUS", null, SqlDbType.NChar, ParameterDirection.Output, 3);

                objWrapper.ExecuteNonQuery("Proc_GetStatus_ACT_CUSTOMER_OPEN_ITEMS");

                if (objSqlParameter.Value != System.DBNull.Value)
                {
                    returnResult = objSqlParameter.Value.ToString();
                }
                objWrapper.ClearParameteres();
                return returnResult;

            }
            catch (Exception ex)
            {
                throw (new Exception("Error occured while getting Item Status of the Suspense payment.\n" + ex.Message));
            }

        }

        /// <summary>
        /// It will get that open items are exist or not.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="ItemStatus"></param>
        /// <returns></returns>
        public bool GetOpenItemStatusExist(int CustomerID, int PolicyID, int PolicyVersionID, string ItemStatus)
        {

            int returnResult = 0;
            try
            {
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objWrapper.AddParameter("@ITEM_STATUS", ItemStatus);

                SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@RESULT", SqlDbType.Int, ParameterDirection.Output);

                objWrapper.ExecuteNonQuery("Proc_GetExist_ACT_CUSTOMER_OPEN_ITEMS");

                if (objSqlParameter.Value != System.DBNull.Value)
                    returnResult = Convert.ToInt32(objSqlParameter.Value.ToString());
                else
                    returnResult = 0;

                objWrapper.ClearParameteres();

                if (returnResult == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw (new Exception("Error occured while getting Item Status from Open Items.\n" + ex.Message));
            }

        }

        /// <summary>
        /// It will set the open items status.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="CheckedItemStatus"></param>
        /// <param name="SetStatus"></param>
        public void SetOpenItemStatus(int CustomerID, int PolicyID, int PolicyVersionID, string CheckedItemStatus, string SetStatus)
        {
            int returnResult = 0;

            try
            {

                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objWrapper.AddParameter("@CHECKED_ITEM_STATUS", CheckedItemStatus);
                objWrapper.AddParameter("@ITEM_STATUS", SetStatus);

                returnResult = objWrapper.ExecuteNonQuery("Proc_SetOpenItemStatus_ACT_CUSTOMER_OPEN_ITEMS");
                objWrapper.ClearParameteres();

            }
            catch (Exception ex)
            {
                throw (new Exception("Error occured while updating Item Status of the open Items.\n" + ex.Message));
            }

        }
        //Commented By Ravindra(08-24-2007) Not Used
        /*
        /// <summary>
        /// It will get the policy premium values.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="PremiumAmout"></param>
        /// <param name="MCCAFee"></param>
        /// <param name="OtherFee"></param>
        public void GetPolicyPremiumValues(int CustomerID, int PolicyID, int PolicyVersionID, out int PremiumAmout, out int MCCAFee, out int OtherFee)
        {
            string returnResult = "";
            try
            {
				
                PremiumAmout = 0;
                OtherFee = 0;
                MCCAFee = 0;

                objWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
                objWrapper.AddParameter("@POLICY_ID",PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID);

                DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetPolicyPremiumXML");
                objWrapper.ClearParameteres();

                if (dsTemp.Tables[0].Rows.Count > 0)
                {
                    returnResult = dsTemp.Tables[0].Rows[0][0].ToString();
                }

                XmlDocument objXMLDoc = new XmlDocument();
                objXMLDoc.LoadXml(returnResult);
			
                XmlNode root = objXMLDoc.DocumentElement;
                XmlNode NetPremium = root.SelectSingleNode("NETPREMIUM");
                XmlNode OtherFees = root.SelectSingleNode("OTHERFEES");
                XmlNode MCCAFees = root.SelectSingleNode("MCCAFEES");
				

                if ( NetPremium != null )
                {
                    PremiumAmout = int.Parse(NetPremium.InnerText);
                }
				

                if ( OtherFees != null )
                {
                    OtherFee = int.Parse(OtherFees.InnerText);
                }

                if ( MCCAFees != null )
                {
                    MCCAFee = int.Parse(MCCAFees.InnerText);
                }

            }
            catch(Exception ex)
            {
                throw( new Exception("Error occured.\n" + ex.Message));
            }
				
        } */

        /// <summary>
        /// Set the Return Premium on Decline
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        public void ProcessPremiumOnDecline(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            try
            {
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);

                DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_ProcessPremiumOnDecline");
                objWrapper.ClearParameteres();


            }
            catch (Exception ex)
            {
                throw (new Exception("Error occured.\n" + ex.Message));
            }

        }



        /// <summary>
        /// It will return the premium values.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="RowID"></param>
        /// <param name="PremiumAmout"></param>
        /// <param name="MCCAFee"></param>
        /// <param name="OtherFee"></param>
        public void GetReturnPremiumValues(int CustomerID, int PolicyID, int PolicyVersionID, int RowID, out int PremiumAmout, out int MCCAFee, out int OtherFee)
        {
            try
            {

                PremiumAmout = 0;
                OtherFee = 0;
                MCCAFee = 0;
                DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objWrapper.AddParameter("@ROW_ID", RowID);

                DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetPolicyReturnPremiumAmount");
                objWrapper.ClearParameteres();
                objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                if (dsTemp.Tables[0].Rows.Count > 0)
                {
                    if (dsTemp.Tables[0].Rows[0][0] != DBNull.Value)
                    {
                        PremiumAmout = int.Parse(dsTemp.Tables[0].Rows[0][0].ToString());
                    }
                    else
                    {
                        PremiumAmout = 0;
                    }

                    if (dsTemp.Tables[0].Rows[0][1] != DBNull.Value)
                    {
                        MCCAFee = int.Parse(dsTemp.Tables[0].Rows[0][1].ToString());
                    }
                    else
                    {
                        MCCAFee = 0;
                    }

                    if (dsTemp.Tables[0].Rows[0][2] != DBNull.Value)
                    {
                        OtherFee = int.Parse(dsTemp.Tables[0].Rows[0][2].ToString());
                    }
                    else
                    {
                        OtherFee = 0;
                    }

                }


            }
            catch (Exception ex)
            {
                throw (new Exception("Error occured.\n" + ex.Message));
            }

        }

        /// <summary>
        /// It will return the total premium amount.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="RowID"></param>
        /// <param name="ReturnPremiumAmout"></param>
        public void GetTotalReturnPremium(int CustomerID, int PolicyID, int PolicyVersionID, int RowID, out int ReturnPremiumAmout)
        {

            try
            {

                int PremiumAmout = 0;
                int OtherFee = 0;
                int MCCAFee = 0;

                GetReturnPremiumValues(CustomerID, PolicyID, PolicyVersionID, RowID, out PremiumAmout, out MCCAFee, out OtherFee);
                ReturnPremiumAmout = PremiumAmout + OtherFee + MCCAFee;

            }
            catch (Exception ex)
            {
                throw (new Exception("Error occured.\n" + ex.Message));
            }

        }

        #endregion


        #region PostPolicyPremium
        /// <summary>
        /// post premium when Reinstate process commited
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="PolicyId"></param>
        /// <param name="PolicyVersionId"></param>
        /// <param name="PolicyNewVersionId"></param>
        /// <param name="REINS_TYPE"></param>
        /// <param name="CHANGE_EFFECTIVE_DATE"></param>
        /// <param name="CFG_AMOUNT"></param>
        /// <param name="SOURCE_ROW_ID"></param>
        /// <param name="UserID"></param>
        /// <param name="ProcessType"></param>
        /// <param name="TransDescription"></param>
        /// <param name="Term"></param>
        /// <param name="strProcessType"></param>
        /// <returns></returns>

        public int PostReinstatementPremium(int CustomerId, int PolicyId, int PolicyVersionId, int PolicyNewVersionId, string REINS_TYPE,
            DateTime CHANGE_EFFECTIVE_DATE, double CFG_AMOUNT, int SOURCE_ROW_ID, int UserID, int ProcessType, out string TransDescription, string Term, string strProcessType, string APPLY_RINS_FEES)
        {
            TransDescription = "";
            int retVal = 0;
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                objWrapper.AddParameter("@NEW_POLICY_VERSION_ID", PolicyNewVersionId);
                objWrapper.AddParameter("@REINS_TYPE", REINS_TYPE);
                objWrapper.AddParameter("@CHANGE_EFFECTIVE_DATE", CHANGE_EFFECTIVE_DATE);
                objWrapper.AddParameter("@CFG_AMOUNT", CFG_AMOUNT);
                objWrapper.AddParameter("@SOURCE_ROW_ID", SOURCE_ROW_ID);
                objWrapper.AddParameter("@USER_ID", UserID);
                objWrapper.AddParameter("@APPLY_RINS_FEES", APPLY_RINS_FEES);

                SqlParameter objParam1 = (SqlParameter)objWrapper.AddParameter
                    ("@TRANSACTION_DESC", null, SqlDbType.VarChar, ParameterDirection.Output, 255);
                //SqlParameter objParam = (SqlParameter) objWrapper.AddParameter 
                //	("@RetVal", null,SqlDbType.Int,ParameterDirection.Output);		

                objWrapper.ExecuteNonQuery("Proc_PostReinstatementPremium");
                TransDescription = objParam1.Value.ToString();
                //retVal = Convert.ToInt32(objParam.Value);
                objWrapper.ClearParameteres();

                //Update Written and change in premium in premium split table --added by Pravesh on 12 dec 2007
                //Call Split Premium 
                Cms.BusinessLayer.BlQuote.clsSplitPremium objSplitPremium = new Cms.BusinessLayer.BlQuote.clsSplitPremium();
                if (REINS_TYPE == POLICY_SAME_TERM)
                {
                    objSplitPremium.UpdateWrittenEnforcePremiumSplit(CustomerId, PolicyId, PolicyNewVersionId, PolicyVersionId, strProcessType, objWrapper);
                    objWrapper.ClearParameteres();
                }
                //end ehre

                return retVal;
            }
            catch (Exception objExp)
            {
                //throw (new Exception("Unable to post premium." + objExp.Message.ToString()));
                throw new Exception("Unable to commit process, error while posting premium.", objExp);
            }
        }


        /// <summary>
        /// posting premium  for processes
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="PolicyId"></param>
        /// <param name="PolicyVersionId"></param>
        /// <param name="UserID"></param>
        /// <param name="ProcessType"></param>
        /// <param name="TransDescription"></param>
        /// <param name="Term"></param>
        /// <param name="strProcessType"></param>
        /// <returns></returns>
        //By Pravesh K Chandel 
        public int PostPolicyProcessPremium(int CustomerId, int PolicyId, int PolicyVersionId
            , int UserID, int ProcessType, out string TransDescription, string Term, string strProcessType)
        {
            int PostPrmRetuenVal = 0;
            string CmsWebUrl = "";
            TransDescription = "";
            int PRODUCT_TYPE = 0;
            int CO_INSURANCE = 0;
            try
            {
                //double dblPremium, dblOtherFees, dblMCCAFees; 
                int AppId, AppVersionId, LobId, retVal = 0;//, PremiumProcessTableRowId
                string strPolicyCurrencyId = "";// strIsSamePolicy = "N", 
                GetAppLobDetails(CustomerId, PolicyId, PolicyVersionId, out AppId, out AppVersionId, out LobId, out strPolicyCurrencyId, out PRODUCT_TYPE, out CO_INSURANCE);

                //Calling Wrapper class
                if (IsEODProcess)
                {
                    CmsWebUrl = ClsCommon.CmsWebUrl;
                    UserID = EODUserID;
                }
                else
                {
                    CmsWebUrl = System.Configuration.ConfigurationSettings.AppSettings["CmsWebUrl"].ToString();
                }
                ClsGenerateQuote objQuote = new ClsGenerateQuote(mSystemID);
                int quodeId;
                objQuote.GeneratePolicyQuote(CustomerId, PolicyId, PolicyVersionId, LobId.ToString(), out quodeId, UserID.ToString());
                //Call Split Premium 


                Cms.BusinessLayer.BlQuote.clsSplitPremium objSplitPremium = new Cms.BusinessLayer.BlQuote.clsSplitPremium();
                ArrayList arrPremiumXML = objSplitPremium.SplitPremiumsPol(CustomerId, PolicyId, PolicyVersionId, strProcessType, objWrapper);

                //Post Reinsurance Premium
                PostPrmRetuenVal = this.Insert_ReInsurance_Premium(CustomerId, PolicyId, PolicyVersionId, ProcessType, UserID);


                // Post Co Insurance Premium
                PostPrmRetuenVal = this.Insert_COInsurance_Premium(CustomerId, PolicyId, PolicyVersionId, UserID);


                string Trandesc = "";
                retVal = PostOpenItems(CustomerId, PolicyId, PolicyVersionId, ProcessType, UserID, out Trandesc);
                TransDescription += Trandesc;

                #region Post Open Item premium Commented Code,Commented By lalit Dec 07,2010
                /*
                ArrayList arrPremium = GetPremiumDetailsFromBillingInfo(CustomerId, PolicyId,
                               PolicyVersionId, Term,
                               ProcessType, out PremiumProcessTableRowId, PRODUCT_TYPE, CO_INSURANCE);

                double dblTotalGross = 0, dblTotalNet = 0, dblTotalMCCA = 0, dblTotalOSTA = 0,
                    dblTotalFees = 0;
                for (int i = 0; i < arrPremium.Count; i++)
                {
                    Premium objPremium = (Premium)arrPremium[i];
                    dblTotalNet += objPremium.NetPremium;
                    dblTotalMCCA += objPremium.MCCAFees;
                    dblTotalOSTA += objPremium.OtherFees;
                    dblTotalGross += objPremium.GrossPremium;
                    if ((PRODUCT_TYPE == enumPOLICY_TYPE.MASTER_POLICY || CO_INSURANCE == enumCO_INSURANCE.FOLLOWER) && dblTotalNet == 0)
                        return 1;
                }
                objWrapper.ClearParameteres();

                objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                objWrapper.AddParameter("@APP_ID", AppId);
                objWrapper.AddParameter("@APP_VERSION_ID", AppVersionId);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                objWrapper.AddParameter("@PREMIUM_AMOUNT", dblTotalNet);
                objWrapper.AddParameter("@MCCA_FEES", dblTotalMCCA);
                objWrapper.AddParameter("@OTHER_FEES", dblTotalOSTA);
                SqlParameter objParam1 = (SqlParameter)objWrapper.AddParameter
                    ("@TRANS_DESC", null, SqlDbType.VarChar, ParameterDirection.Output, 255);

                objWrapper.AddParameter("@PARAM2", PremiumProcessTableRowId);
                objWrapper.AddParameter("@PARAM3", UserID);
                objWrapper.AddParameter("@PARAM4", null);
                objWrapper.AddParameter("@RISK_ID", 1);
                objWrapper.AddParameter("@RISK_TYPE", "PRODUCT");
                objWrapper.AddParameter("@COMMISSION_CLASS", 0);
                objWrapper.AddParameter("@IS_SAME_POLICY", strIsSamePolicy);

                SqlParameter objParam = (SqlParameter)objWrapper.AddParameter
                    ("@RetVal", null, SqlDbType.Int, ParameterDirection.Output);

                SqlParameter objFees = (SqlParameter)objWrapper.AddParameter
                    ("@TOTAL_FEES", null, SqlDbType.Decimal, ParameterDirection.Output);

                objWrapper.ExecuteNonQuery("Proc_InsertProductPremiumPolicyOpenItems");

                if (strIsSamePolicy != "Y")
                {
                    TransDescription = TransDescription + objParam1.Value.ToString();
                }
                dblTotalFees += Convert.ToDouble(objFees.Value);
                retVal = Convert.ToInt32(objParam.Value);
                objWrapper.ClearParameteres();
                strIsSamePolicy = "Y";

                //TransDescription = TransDescription + " Total premium amount invoiced $" + dblTotalGross.ToString(); {0:,#,###}
                TransDescription = TransDescription + ClsCommon.FetchGeneralMessage("1117", " " + FormatNumber(dblTotalGross.ToString()));

                if (dblTotalFees != 0)
                    //TransDescription = TransDescription + " And the Total Installment fee $" + dblTotalFees.ToString();
                    TransDescription = TransDescription + ClsCommon.FetchGeneralMessage("1118", " " + FormatNumber(dblTotalFees.ToString()));

                //Insert info in Balance Tab
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                objWrapper.AddParameter("@SOURCE_ROW_ID", PremiumProcessTableRowId);
                objWrapper.ExecuteNonQuery("Proc_InsertBalanceInformation");
                objWrapper.ClearParameteres();
                /* commented by Pravesh on 26 Aug 2010
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                objWrapper.ExecuteNonQuery("ReconcileCustomerDepsits");
                objWrapper.ClearParameteres();
                */
                #endregion End Post open item premium code block

                return retVal;
                //}
                //else
                // {
                // return retVal = 1;
                //}
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public int PostPolicyPremium(int CustomerId, int PolicyId, int PolicyVersionId
            , int UserID, int ProcessType, out string TransDescription, string Term, string strProcessType)
        {
            string CmsWebUrl = "";
            TransDescription = "";
            try
            {
                //double dblPremium, dblOtherFees, dblMCCAFees; 
                int AppId, AppVersionId, LobId, PremiumProcessTableRowId, retVal = 0;

                string strPolicyCurrencyId = "";//strIsSamePolicy = "N",
                GetAppLobDetails(CustomerId, PolicyId, PolicyVersionId, out AppId, out AppVersionId, out LobId, out strPolicyCurrencyId);
                //Calling Wrapper class

                if (IsEODProcess)
                {
                    CmsWebUrl = ClsCommon.CmsWebUrl;
                    UserID = EODUserID;
                }
                else
                {
                    CmsWebUrl = System.Configuration.ConfigurationSettings.AppSettings["CmsWebUrl"].ToString();
                }

                //				BlProcess.Quote_RuleWebServices.wscmsweb objQuote = new Cms.BusinessLayer.BlProcess.Quote_RuleWebServices.wscmsweb(CmsWebUrl );
                //				string strQuoteID = objQuote.GeneratePolicyQuote(CustomerId,PolicyId,PolicyVersionId,LobId.ToString() ,UserID);

                ClsGenerateQuote objQuote = new ClsGenerateQuote(mSystemID);
                int quodeId;
                objQuote.GeneratePolicyQuote(CustomerId, PolicyId, PolicyVersionId, LobId.ToString(), out quodeId, UserID.ToString());

                //Call Split Premium 
                Cms.BusinessLayer.BlQuote.clsSplitPremium objSplitPremium = new Cms.BusinessLayer.BlQuote.clsSplitPremium();
                ArrayList arrPremiumXML = objSplitPremium.SplitPremiumsPol(CustomerId, PolicyId, PolicyVersionId, strProcessType, objWrapper);
                //ArrayList arrPremiumXML=SplitPremiumsPol(CustomerId,PolicyId,PolicyVersionId,strProcessType,objWrapper); 

                //Ravindra(10-16-2007) : If the policy is homeowner policy and boat is attached to it ,
                //existing logic creates two record in Premium Process table as there are 2 premium XMLs 
                //in Rating area. Though the premium processing done further is correct there are two records
                //for a single logic . Change will create a single record in PPD other processesing will
                //remain intact.

                //				for(int j=0 ; j < arrPremiumXML.Count ; j++)
                //				{
                //					PremiumDetails objPremiumdetails = (PremiumDetails)arrPremiumXML[j];
                //					ArrayList arrPremium = GetPremiumDetails(CustomerId,PolicyId,
                //						PolicyVersionId,objPremiumdetails.PremiumXML ,objPremiumdetails.QuoteType ,Term,
                //						ProcessType, out PremiumProcessTableRowId);
                //				}		

                //Post CO-insurance Premium,Added By Lalit Dec 17,2010
                Insert_COInsurance_Premium(CustomerId, PolicyId, PolicyVersionId, UserID);

                //Post re-insurance Premium,Added By Lalit Dec 17,2010
                Insert_ReInsurance_Premium(CustomerId, PolicyId, PolicyVersionId, ProcessType, UserID);


                ArrayList arrPremium = GetPremiumDetails(CustomerId, PolicyId,
                    PolicyVersionId, arrPremiumXML, Term,
                    ProcessType, out PremiumProcessTableRowId);

                double dblTotalGross = 0, dblTotalNet = 0, dblTotalMCCA = 0, dblTotalOSTA = 0;
                    //dblTotalFees = 0;
                for (int i = 0; i < arrPremium.Count; i++)
                {
                    Premium objPremium = (Premium)arrPremium[i];
                    dblTotalNet += objPremium.NetPremium;
                    dblTotalMCCA += objPremium.MCCAFees;
                    dblTotalOSTA += objPremium.OtherFees;
                    dblTotalGross += objPremium.GrossPremium;
                }
                // Check Minimum Premium Applicability & update Bill Plan to Full Pay 
                //if premium less than Min Premium req. for plan.

                //objWrapper.ClearParameteres();
                //objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                //objWrapper.AddParameter("@POLICY_ID", PolicyId);
                //objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);

                //objWrapper.AddParameter("@PREMIUM_AMOUNT", dblTotalNet);
                //objWrapper.AddParameter("@MCCA_FEES", dblTotalMCCA);
                //objWrapper.AddParameter("@OTHER_FEES", dblTotalOSTA);

                //SqlParameter objTranDes = (SqlParameter)objWrapper.AddParameter
                //    ("@TRANS_DESC", null, SqlDbType.VarChar, ParameterDirection.Output, 255);
                //SqlParameter objRetVal = (SqlParameter)objWrapper.AddParameter
                //    ("@RetVal", null, SqlDbType.Int, ParameterDirection.Output);

                //objWrapper.ExecuteNonQuery("Proc_CheckAndUpdatePlanApplicability");

                //int Result = Convert.ToInt32(objRetVal.Value);
                //if (Result == -1) // Full Pay plan not found abandon process
                //{
                //    throw (new Exception("Unable to post premium. System Generated full plan not found"));
                //}
                //else if (Result == 2) // Policy Migrated to Full Pay
                //{
                //    TransDescription = objTranDes.Value.ToString();
                //}
                objWrapper.ClearParameteres();

                #region  Old Premium posting Code
                //for (int i = 0; i < arrPremium.Count; i++)
                //{

                //    Premium objPremium = (Premium)arrPremium[i];

                //    int CommissionClass = GetCommissionClass(CustomerId, PolicyId, PolicyVersionId,
                //        objPremium.RiskID, objWrapper);

                //    objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                //    objWrapper.AddParameter("@APP_ID", AppId);
                //    objWrapper.AddParameter("@APP_VERSION_ID", AppVersionId);

                //    objWrapper.AddParameter("@POLICY_ID", PolicyId);
                //    objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);

                //    objWrapper.AddParameter("@PREMIUM_AMOUNT", objPremium.NetPremium);
                //    objWrapper.AddParameter("@MCCA_FEES", objPremium.MCCAFees);
                //    objWrapper.AddParameter("@OTHER_FEES", objPremium.OtherFees);

                //    SqlParameter objParam1 = (SqlParameter)objWrapper.AddParameter
                //        ("@TRANS_DESC", null, SqlDbType.VarChar, ParameterDirection.Output, 255);

                //    objWrapper.AddParameter("@PARAM2", PremiumProcessTableRowId);
                //    objWrapper.AddParameter("@PARAM3", UserID);
                //    objWrapper.AddParameter("@PARAM4", null);

                //    objWrapper.AddParameter("@RISK_ID", objPremium.RiskID);
                //    objWrapper.AddParameter("@RISK_TYPE", objPremium.RiskType);

                //    objWrapper.AddParameter("@COMMISSION_CLASS", CommissionClass);

                //    objWrapper.AddParameter("@IS_SAME_POLICY", strIsSamePolicy);

                //    //SqlParameter objParam1 = (SqlParameter)objWrapper.AddParameter("@RetVal",System.Data.SqlDbType.Int , ParameterDirection.ReturnValue);
                //    SqlParameter objParam = (SqlParameter)objWrapper.AddParameter
                //        ("@RetVal", null, SqlDbType.Int, ParameterDirection.Output);

                //    SqlParameter objFees = (SqlParameter)objWrapper.AddParameter
                //        ("@TOTAL_FEES", null, SqlDbType.Decimal, ParameterDirection.Output);

                //    objWrapper.ExecuteNonQuery("Proc_InsertPremiumPolicyOpenItems");

                //    if (strIsSamePolicy != "Y")
                //    {
                //        TransDescription = TransDescription + objParam1.Value.ToString();
                //    }
                //    dblTotalFees += Convert.ToDouble(objFees.Value);
                //    retVal = Convert.ToInt32(objParam.Value);
                //    objWrapper.ClearParameteres();
                //    strIsSamePolicy = "Y";
                //}

                ////TransDescription = TransDescription + " Total premium amount invoiced $" + dblTotalGross.ToString(); 
                ////if(dblTotalFees != 0) 
                ////    TransDescription = TransDescription + " And the Total Installment fee $" + dblTotalFees.ToString();



                ////TransDescription = TransDescription + " Total premium amount invoiced $" + dblTotalGross.ToString();
                ////TransDescription = TransDescription + ClsCommon.FetchGeneralMessage("1117", " " + GetPolicyCurrencySymbol(strPolicyCurrencyId)) + dblTotalGross.ToString();
                //TransDescription = TransDescription + ClsCommon.FetchGeneralMessage("1117", " " + FormatNumber(dblTotalGross.ToString()));

                //if (dblTotalFees != 0)
                //    //TransDescription = TransDescription + " And the Total Installment fee $" + dblTotalFees.ToString();
                //    TransDescription = TransDescription + ClsCommon.FetchGeneralMessage("1118", " " + FormatNumber(dblTotalFees.ToString()));



                ////Insert info in Balance Tab
                //objWrapper.ClearParameteres();
                //objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                //objWrapper.AddParameter("@POLICY_ID", PolicyId);
                //objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                //objWrapper.AddParameter("@SOURCE_ROW_ID", PremiumProcessTableRowId);
                //objWrapper.ExecuteNonQuery("Proc_InsertBalanceInformation");
                //objWrapper.ClearParameteres();


                #endregion

                //Added By Lalit For post premium
                string Trandesc = "";
                retVal = PostOpenItems(CustomerId, PolicyId, PolicyVersionId, ProcessType, UserID, out Trandesc);
                TransDescription += Trandesc;

                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                objWrapper.ExecuteNonQuery("ReconcileCustomerDepsits");
                objWrapper.ClearParameteres();

                return retVal;
            }
            catch (System.Net.WebException objEx)
            {
                Exception customEx = new Exception("Error while calling webservice for Quote. URL : " + CmsWebUrl, objEx);
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(customEx);
                throw (customEx);
            }
            catch (Exception objExp)
            {
                //throw (new Exception("Unable to post premium." + objExp.Message.ToString()));
                throw new Exception("Unable to commit process, error while posting premium.", objExp);
            }
        }
        protected void GeneratePolicyDocuments(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
        {
            // Generate Other Documents
            ClsProductPdfXml objProductPdfXml = new ClsProductPdfXml(this.objWrapper);
            objProductPdfXml.generateDocuments(objProcessInfo);

        }
        public ClsCancellationRule GetCancellationRule(int CustomerID, int PolicyID, int PolicyVersionID, DataWrapper objDataWrapper)
        {

            System.Collections.Hashtable CancellationRuleKeys = new System.Collections.Hashtable();
            string filePath;
            if (IsEODProcess)
            {
                string strTemp = CancellationRuleXML.Replace("/", @"\");
                filePath = WebAppUNCPath + strTemp;
                filePath = System.IO.Path.GetFullPath(filePath);
            }
            else
            {
                filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + CancellationRuleXML);
            }
            RuleDoc = new XmlDocument();
            RuleDoc.Load(filePath);
            XmlNode masterNode = RuleDoc.SelectSingleNode("Root/Master");
            string strQuery = "";
            XmlNode queryNode = masterNode.SelectSingleNode("Query[@Code='SELECT']");
            strQuery = queryNode.InnerText;
            objDataWrapper.ClearParameteres();
            objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
            objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
            DataSet dtKeyValue = objDataWrapper.ExecuteDataSet(strQuery);

            objDataWrapper.ClearParameteres();
            if (CancellationRuleKeys.Count > 0)
            {
                CancellationRuleKeys.Clear();
            }

            XmlNodeList columnNodes = masterNode.SelectNodes("Column");

            // Iterate through Columns nodes in Master node of XML and 
            // add a key in hashtable with value mapped with the returned DataSet
            if (dtKeyValue.Tables.Count > 0)
            {
                if (dtKeyValue.Tables[0].Rows.Count > 0)
                {

                    foreach (XmlNode node in columnNodes)
                    {
                        string strKey = node.Attributes["Code"].Value;
                        string strValue = "";
                        if (node.Attributes["MapColumnn"].Value.Trim() != "")
                        {
                            if (dtKeyValue.Tables[0].Rows[0][strKey] != DBNull.Value)
                            {
                                strValue = dtKeyValue.Tables[0].Rows[0][strKey].ToString().Trim();
                                //If the returned value is Negative make it Zero
                                if (strValue.StartsWith("-"))
                                {
                                    strValue = "0";
                                }
                            }
                            CancellationRuleKeys.Add(strKey, strValue);
                        }
                    }
                }
            }
            //End Setting Keys

            ClsCancellationRule objRule = new ClsCancellationRule();
            if (CancellationRuleKeys["PROCESS_PAYMENT_CODE"] == null)
            {
                objRule = null;
                //Throw Exception
            }
            string strPROCESS_PAYMENT_CODE = CancellationRuleKeys["PROCESS_PAYMENT_CODE"].ToString();
            string strStateID = CancellationRuleKeys["STATE_ID"].ToString();

            DateTime AppEffectiveDate = Convert.ToDateTime(CancellationRuleKeys["APP_EFFECTIVE_DATE"]);

            XmlNode GroupNode = null;

            if (CancellationRuleKeys["BILL_TYPE"].ToString() == "AB")
            {
                GroupNode = RuleDoc.SelectSingleNode("Root/Group[@Code='AB']");
            }
            else
            {
                GroupNode = RuleDoc.SelectSingleNode("Root/Group[@Code='" + strPROCESS_PAYMENT_CODE + "']");
            }

            ArrayList masterRuleNodeArray = GetEffectiveMasterRules(GroupNode, AppEffectiveDate, strStateID);

            EvalRuleNode(masterRuleNodeArray, CancellationRuleKeys);
            //Done for Itrack Issue 5629 on 29 April 2009
            if (CancellationRuleKeys["ACT_UPDP"].ToString() != "" && CancellationRuleKeys["ACT_UPDP"].ToString() != null)
                objRule.ACTION_UNPAID_PREMIUM = CancellationRuleKeys["ACT_UPDP"].ToString();
            if (CancellationRuleKeys["ACT_RETP"].ToString() != "" && CancellationRuleKeys["ACT_RETP"].ToString() != null)
                objRule.ACTION_RETURN_PREMIUM = CancellationRuleKeys["ACT_RETP"].ToString();
            if (CancellationRuleKeys["BILL_TYPE"].ToString() != "" && CancellationRuleKeys["BILL_TYPE"].ToString() != null)
                objRule.BILL_TYPE = CancellationRuleKeys["BILL_TYPE"].ToString();
            return objRule;

        }
        //Ravindra(07-27-2007) Foved from ClsEndorsement Process will be required in Reinstatement Process
        public void CalculateChangeEffectivePremium(int CustomerID, int PolicyID, int PolicyVersionID,
            Premium objPremium, ref double FeesToReverse)
        {
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objWrapper.AddParameter("@GROSS_PREMIUM_AMOUNT", objPremium.GrossPremium);
                objWrapper.AddParameter("@MCCA_FEES", objPremium.MCCAFees);
                objWrapper.AddParameter("@OTHER_FEES", objPremium.OtherFees);
                objWrapper.AddParameter("@FEES_TO_REVERSE", FeesToReverse);
                SqlParameter objChangePremium = (SqlParameter)objWrapper.AddParameter("@CHANGE_PREMIUM_AMOUNT",
                    SqlDbType.Decimal, ParameterDirection.Output);
                SqlParameter objChangeMCCAFees = (SqlParameter)objWrapper.AddParameter("@CHANGE_MCCA_FEES",
                    SqlDbType.Decimal, ParameterDirection.Output);
                SqlParameter objChangeOSTAFees = (SqlParameter)objWrapper.AddParameter("@CHANGE_OTHER_FEES",
                    SqlDbType.Decimal, ParameterDirection.Output);
                SqlParameter objChangeReverseFees = (SqlParameter)objWrapper.AddParameter("@CHANGE_FEES_TO_REVERSE",
                    SqlDbType.Decimal, ParameterDirection.Output);


                objWrapper.ExecuteNonQuery("Proc_CalculateChangeInPremium");

                objPremium.GrossPremium = Convert.ToDouble(objChangePremium.Value);
                objPremium.MCCAFees = Convert.ToDouble(objChangeMCCAFees.Value);
                objPremium.OtherFees = Convert.ToDouble(objChangeOSTAFees.Value);
                objPremium.NetPremium = objPremium.GrossPremium - objPremium.MCCAFees - objPremium.OtherFees;
                FeesToReverse = Convert.ToDouble(objChangeReverseFees.Value);
                objWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        /// <summary>
        /// To Rectify the Rounding Problem in Total Premium
        /// Praven Kasana (11 Sep 2009)
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="objPremium"></param>
        /// <param name="FeesToReverse"></param>
        /// <param name="calledFrom"></param>
        public void CalculateChangeEffectivePremiumPolicy(int CustomerID, int PolicyID, int PolicyVersionID,
            Premium objPremium, ref double FeesToReverse, string calledFrom)
        {
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objWrapper.AddParameter("@GROSS_PREMIUM_AMOUNT", objPremium.GrossPremium);
                objWrapper.AddParameter("@MCCA_FEES", objPremium.MCCAFees);
                objWrapper.AddParameter("@OTHER_FEES", objPremium.OtherFees);
                objWrapper.AddParameter("@FEES_TO_REVERSE", FeesToReverse);
                objWrapper.AddParameter("@CALLED_FROM", calledFrom);
                SqlParameter objChangePremium = (SqlParameter)objWrapper.AddParameter("@CHANGE_PREMIUM_AMOUNT",
                    SqlDbType.Decimal, ParameterDirection.Output);
                SqlParameter objChangeMCCAFees = (SqlParameter)objWrapper.AddParameter("@CHANGE_MCCA_FEES",
                    SqlDbType.Decimal, ParameterDirection.Output);
                SqlParameter objChangeOSTAFees = (SqlParameter)objWrapper.AddParameter("@CHANGE_OTHER_FEES",
                    SqlDbType.Decimal, ParameterDirection.Output);
                SqlParameter objChangeReverseFees = (SqlParameter)objWrapper.AddParameter("@CHANGE_FEES_TO_REVERSE",
                    SqlDbType.Decimal, ParameterDirection.Output);


                objWrapper.ExecuteNonQuery("Proc_CalculateChangeInPremium");

                objPremium.GrossPremium = Convert.ToDouble(objChangePremium.Value);
                objPremium.MCCAFees = Convert.ToDouble(objChangeMCCAFees.Value);
                objPremium.OtherFees = Convert.ToDouble(objChangeOSTAFees.Value);
                objPremium.NetPremium = objPremium.GrossPremium - objPremium.MCCAFees - objPremium.OtherFees;
                FeesToReverse = Convert.ToDouble(objChangeReverseFees.Value);
                objWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public int GetCommissionClass(int CustomerID, int PolicyID, int PolicyVersionID, int RiskID, DataWrapper objDataWrapper)
        {

            try
            {
                System.Collections.Hashtable CommissionKeys = new System.Collections.Hashtable();
                string filePath;
                if (IsEODProcess)
                {
                    string strTemp = CommissionRuleXML.Replace("/", @"\");
                    filePath = WebAppUNCPath + strTemp;
                    filePath = System.IO.Path.GetFullPath(filePath);
                }
                else
                {
                    filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + CommissionRuleXML);
                }

                RuleDoc = new XmlDocument();
                RuleDoc.Load(filePath);
                XmlNode masterNode = RuleDoc.SelectSingleNode("Root/Master");
                string strQuery = "";
                XmlNode queryNode = masterNode.SelectSingleNode("Query[@Code='SELECT']");
                strQuery = queryNode.InnerText;
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objDataWrapper.AddParameter("@RISK_ID", RiskID);
                DataSet dtKeyValue = objDataWrapper.ExecuteDataSet(strQuery);

                objDataWrapper.ClearParameteres();

                if (CommissionKeys.Count > 0)
                {
                    CommissionKeys.Clear();
                }
                XmlNodeList columnNodes = masterNode.SelectNodes("Column");

                // Iterate through Columns nodes in Master node of XML and 
                // add a key in hashtable with value mapped with the returned DataSet
                if (dtKeyValue.Tables.Count > 0)
                {
                    if (dtKeyValue.Tables[0].Rows.Count > 0)
                    {

                        foreach (XmlNode node in columnNodes)
                        {
                            string strKey = node.Attributes["Code"].Value;
                            string strValue = "";
                            if (node.Attributes["MapColumnn"].Value.Trim() != "")
                            {
                                if (dtKeyValue.Tables[0].Rows[0][strKey] != DBNull.Value)
                                {
                                    strValue = dtKeyValue.Tables[0].Rows[0][strKey].ToString().Trim();
                                    //If the returned value is Negative make it Zero
                                    if (strValue.StartsWith("-"))
                                    {
                                        strValue = "0";
                                    }
                                }
                                CommissionKeys.Add(strKey, strValue);
                            }
                        }
                    }
                }
                //End Setting Keys

                if (CommissionKeys["STATE_ID"] == null)
                {
                    //Throw Exception
                }
                string strStateID = CommissionKeys["STATE_ID"].ToString();
                string strLobID = CommissionKeys["LOB_ID"].ToString();
                DateTime AppEffectiveDate = Convert.ToDateTime(CommissionKeys["APP_EFFECTIVE_DATE"]);

                XmlNode GroupNode = RuleDoc.SelectSingleNode("Root/Group[@CodeID='" + strLobID + "']");
                ArrayList masterRuleNodeArray = GetEffectiveMasterRules(GroupNode, AppEffectiveDate, strStateID);
                EvalRuleNode(masterRuleNodeArray, CommissionKeys);
                string strCommissionClass = CommissionKeys["ACC_CLS"].ToString();
                return Convert.ToInt32(strCommissionClass);
            }
            catch (Exception objExc)
            {
                throw new Exception("Error while calculating Commission Class", objExc);
            }
        }


        protected void EvalRuleNode(ArrayList masterRuleNodeArray, Hashtable CommissionKeys)
        {
            for (int i = 0; i < masterRuleNodeArray.Count; i++)
            {
                XmlNode masterNode = (XmlNode)masterRuleNodeArray[i];
                foreach (XmlNode conditionsNode in masterNode.ChildNodes)
                {
                    XmlNodeList conditionNodeList = conditionsNode.SelectNodes("Condition");
                    foreach (XmlNode conditionNode in conditionNodeList)
                    {
                        string strResult = EvalNode(conditionNode, CommissionKeys);
                        if (strResult == "True")
                        {
                            XmlNodeList toSetNodeArray = conditionNode.SelectNodes("ToSet");
                            if (toSetNodeArray != null)
                            {
                                SetKeys(toSetNodeArray, CommissionKeys);
                            }
                            XmlNodeList subConditionsNodeArray = conditionNode.SelectNodes("SubConditions");
                            foreach (XmlNode subConditionsNode in subConditionsNodeArray)
                            {
                                //Subcondition
                                XmlNodeList subConditionNodeArray = subConditionsNode.SelectNodes("SubCondition");
                                if (subConditionNodeArray != null)
                                {
                                    foreach (XmlNode subConditionNode in subConditionNodeArray)
                                    {
                                        string strRes = EvalNode(subConditionNode, CommissionKeys);
                                        if (strRes == "True")
                                        {
                                            XmlNodeList toSetNodeList = subConditionNode.SelectNodes("ToSet");
                                            if (toSetNodeList != null)
                                            {
                                                SetKeys(toSetNodeList, CommissionKeys);
                                            }
                                            break;
                                        }

                                    }
                                }
                                //End OF Subcondition
                            }
                            break;
                        }

                    }

                }
            }

        }

        public System.Collections.ArrayList GetPremiumFromXML(string strPremiumXML, string strQuoteType,
            out string strFinalPremium, out double dblNetFinalPremium)
        {
            //NumberFormat for xml decimal value
            NumberFormatInfo decimalNumberFormat = new CultureInfo(enumCulture.US, true).NumberFormat;

            XmlDocument xmlPremiumXML = new XmlDocument();

            System.Collections.ArrayList arrPremium = new System.Collections.ArrayList();
            strFinalPremium = "";
            StringBuilder strbFinalXML = new StringBuilder();

            XmlNodeList RiskNodes = null;

            double dblTotalPremium = 0.00;
            double dblTotalFees = 0.00;
            double dblNetPremium = 0.00;

            dblNetFinalPremium = 0;
            if (strQuoteType != "0")
            {

                xmlPremiumXML.LoadXml(strPremiumXML);
                RiskNodes = xmlPremiumXML.SelectNodes("PRIMIUM/RISK");

                // switch case on the basis of the lob
                switch (strQuoteType)
                {
                    case "REDW":
                        //Caluculating Premium For Dwellings in Policy
                        foreach (XmlNode riskNode in RiskNodes)
                        {
                            double dblPremium = 0, dblFees = 0;
                            // Consider Only Dwellings As Risks
                            if (riskNode.Attributes["TYPE"].Value.Trim() == "REDW")
                            {
                                Premium objPremium = new Premium();
                                objPremium.RiskID = Convert.ToInt32(riskNode.Attributes["ID"].Value.Trim());
                                objPremium.RiskType = riskNode.Attributes["TYPE"].Value.Trim();
                                XmlNode premiumNode = riskNode.SelectSingleNode("STEP[@COMPONENT_CODE ='SUMTOTAL']");
                                if (premiumNode != null)
                                {
                                    dblPremium = Convert.ToDouble(premiumNode.Attributes["STEPPREMIUM"].Value.Trim());
                                    dblTotalPremium += dblPremium;
                                }

                                //XmlNode FeesNode = riskNode.SelectSingleNode("STEP[@STEPDESC ='-  Property Expense Fee']");
                                //PRP_EXPNS_FEE
                                XmlNode FeesNode = riskNode.SelectSingleNode("STEP[@COMPONENT_CODE ='PRP_EXPNS_FEE']");
                                if (FeesNode != null)
                                {
                                    dblFees = Convert.ToDouble(FeesNode.Attributes["STEPPREMIUM"].Value.Trim());
                                    dblTotalFees += dblFees;
                                }
                                objPremium.GrossPremium = dblPremium;
                                objPremium.NetPremium = dblPremium - dblFees;
                                objPremium.MCCAFees = dblFees;
                                objPremium.OtherFees = 0;
                                arrPremium.Add(objPremium);
                            }
                        }

                        //Fetching Premium Of other Risks
                        foreach (XmlNode riskNode in RiskNodes)
                        {
                            double dblPremium = 0, dblFees = 0;
                            //Fetch Premium of all other risks and append it to first Dwelling
                            if (riskNode.Attributes["TYPE"].Value.Trim() != "REDW")
                            {
                                Premium objPremium = (Premium)arrPremium[0];
                                XmlNode premiumNode = riskNode.SelectSingleNode("STEP[@COMPONENT_CODE ='SUMTOTAL']");
                                if (premiumNode != null)
                                {
                                    dblPremium = Convert.ToDouble(premiumNode.Attributes["STEPPREMIUM"].Value.Trim());
                                    dblTotalPremium += dblPremium;
                                }

                                //XmlNode FeesNode = riskNode.SelectSingleNode("STEP[@STEPDESC ='-  Property Expense Fee']");
                                XmlNode FeesNode = riskNode.SelectSingleNode("STEP[@COMPONENT_CODE ='PRP_EXPNS_FEE']");
                                if (FeesNode != null)
                                {
                                    dblFees = Convert.ToDouble(FeesNode.Attributes["STEPPREMIUM"].Value.Trim());
                                    dblTotalFees += dblFees;
                                }
                                objPremium.GrossPremium = objPremium.GrossPremium + dblPremium;
                                objPremium.NetPremium = objPremium.NetPremium + dblPremium - dblFees;
                                objPremium.MCCAFees = objPremium.MCCAFees + dblFees;
                                objPremium.OtherFees = objPremium.OtherFees + 0;


                            }

                        }

                        dblNetPremium = dblTotalPremium - dblTotalFees;
                        strbFinalXML.Append("<PREMIUM>");
                        //Node For GROSSPREMIUM
                        strbFinalXML.Append("<GROSSPREMIUM>");
                        strbFinalXML.Append(dblTotalPremium);
                        strbFinalXML.Append("</GROSSPREMIUM>");
                        //Node For NETPREMIUM
                        strbFinalXML.Append("<NETPREMIUM>");
                        strbFinalXML.Append(dblNetPremium);
                        strbFinalXML.Append("</NETPREMIUM>");
                        //Node for FEES
                        strbFinalXML.Append("<MCCAFEES>");
                        strbFinalXML.Append(dblTotalFees);
                        strbFinalXML.Append("</MCCAFEES>");
                        //Node for OTHRERFEES
                        strbFinalXML.Append("<OTHERFEES>");
                        strbFinalXML.Append("0");
                        strbFinalXML.Append("</OTHERFEES>");
                        strbFinalXML.Append("</PREMIUM>");

                        break;
                    case "UMB":
                        //Caluculating Premium For Each risk in Umbrella Policy
                        foreach (XmlNode riskNode in RiskNodes)
                        {
                            double dblPremium = 0, dblFees = 0;
                            if (riskNode.Attributes["TYPE"].Value.Trim() == "MSE")
                            {
                                Premium objPremium = new Premium();
                                objPremium.RiskID = Convert.ToInt32(riskNode.Attributes["ID"].Value.Trim());
                                objPremium.RiskType = riskNode.Attributes["TYPE"].Value.Trim();
                                XmlNode premiumNode = riskNode.SelectSingleNode("STEP[@COMPONENT_CODE ='SUMTOTAL']");
                                if (premiumNode != null)
                                {
                                    dblPremium = Convert.ToDouble(premiumNode.Attributes["STEPPREMIUM"].Value.Trim());
                                    dblTotalPremium += dblPremium;
                                }


                                objPremium.GrossPremium = dblPremium;
                                objPremium.NetPremium = dblPremium - dblFees;
                                objPremium.MCCAFees = dblFees;
                                objPremium.OtherFees = 0;
                                arrPremium.Add(objPremium);
                            }
                        }

                        dblNetPremium = dblTotalPremium - dblTotalFees;
                        strbFinalXML.Append("<PREMIUM>");
                        //Node For GROSSPREMIUM
                        strbFinalXML.Append("<GROSSPREMIUM>");
                        strbFinalXML.Append(dblTotalPremium);
                        strbFinalXML.Append("</GROSSPREMIUM>");
                        //Node For NETPREMIUM
                        strbFinalXML.Append("<NETPREMIUM>");
                        strbFinalXML.Append(dblNetPremium);
                        strbFinalXML.Append("</NETPREMIUM>");
                        //Node for FEES
                        strbFinalXML.Append("<MCCAFEES>");
                        strbFinalXML.Append(dblTotalFees);
                        strbFinalXML.Append("</MCCAFEES>");
                        //Node for OTHRERFEES
                        strbFinalXML.Append("<OTHERFEES>");
                        strbFinalXML.Append("0");
                        strbFinalXML.Append("</OTHERFEES>");
                        strbFinalXML.Append("</PREMIUM>");

                        break;
                    case "HOME":
                        //Caluculating Premium For Dwellings in Policy
                        foreach (XmlNode riskNode in RiskNodes)
                        {
                            double dblPremium = 0, dblFees = 0;
                            // Consider Only Dwellings As Risks
                            if (riskNode.Attributes["TYPE"].Value.Trim() == "HOME")
                            {
                                Premium objPremium = new Premium();
                                objPremium.RiskID = Convert.ToInt32(riskNode.Attributes["ID"].Value.Trim());
                                objPremium.RiskType = riskNode.Attributes["TYPE"].Value.Trim();
                                XmlNode premiumNode = riskNode.SelectSingleNode("STEP[@COMPONENT_CODE ='SUMTOTAL']");
                                if (premiumNode != null)
                                {
                                    dblPremium = Convert.ToDouble(premiumNode.Attributes["STEPPREMIUM"].Value.Trim());
                                    dblTotalPremium += dblPremium;
                                }

                                //XmlNode FeesNode = riskNode.SelectSingleNode("STEP[@STEPDESC ='-  Property Expense Fee']");
                                XmlNode FeesNode = riskNode.SelectSingleNode("STEP[@COMPONENT_CODE ='PRP_EXPNS_FEE']");
                                if (FeesNode != null)
                                {
                                    dblFees = Convert.ToDouble(FeesNode.Attributes["STEPPREMIUM"].Value.Trim());
                                    dblTotalFees += dblFees;
                                }
                                objPremium.GrossPremium = dblPremium;
                                objPremium.NetPremium = dblPremium - dblFees;
                                objPremium.MCCAFees = dblFees;
                                objPremium.OtherFees = 0;
                                arrPremium.Add(objPremium);
                            }
                        }

                        //Fetching Premium Of other Risks(excluding Watercraft)
                        foreach (XmlNode riskNode in RiskNodes)
                        {
                            double dblPremium = 0, dblFees = 0;
                            //Fetch Premium of all other risks and append it to first Dwelling
                            if (riskNode.Attributes["TYPE"].Value.Trim() != "HOME")
                            {
                                Premium objPremium = (Premium)arrPremium[0];
                                XmlNode premiumNode = riskNode.SelectSingleNode("STEP[@COMPONENT_CODE ='SUMTOTAL']");
                                if (premiumNode != null)
                                {
                                    dblPremium = Convert.ToDouble(premiumNode.Attributes["STEPPREMIUM"].Value.Trim());
                                    dblTotalPremium += dblPremium;
                                }

                                //XmlNode FeesNode = riskNode.SelectSingleNode("STEP[@STEPDESC ='-  Property Expense Fee']");
                                XmlNode FeesNode = riskNode.SelectSingleNode("STEP[@COMPONENT_CODE ='PRP_EXPNS_FEE']");
                                if (FeesNode != null)
                                {
                                    dblFees = Convert.ToDouble(FeesNode.Attributes["STEPPREMIUM"].Value.Trim());
                                    dblTotalFees += dblFees;
                                }
                                objPremium.GrossPremium = objPremium.GrossPremium + dblPremium;
                                objPremium.NetPremium = objPremium.NetPremium + dblPremium - dblFees;
                                objPremium.MCCAFees = objPremium.MCCAFees + dblFees;
                                objPremium.OtherFees = objPremium.OtherFees + 0;


                            }
                        }

                        dblNetPremium = dblTotalPremium - dblTotalFees;
                        strbFinalXML.Append("<PREMIUM>");
                        //Node For GROSSPREMIUM
                        strbFinalXML.Append("<GROSSPREMIUM>");
                        strbFinalXML.Append(dblTotalPremium);
                        strbFinalXML.Append("</GROSSPREMIUM>");
                        //Node For NETPREMIUM
                        strbFinalXML.Append("<NETPREMIUM>");
                        strbFinalXML.Append(dblNetPremium);
                        strbFinalXML.Append("</NETPREMIUM>");
                        //Node for FEES
                        strbFinalXML.Append("<MCCAFEES>");
                        strbFinalXML.Append(dblTotalFees);
                        strbFinalXML.Append("</MCCAFEES>");
                        //Node for OTHRERFEES
                        strbFinalXML.Append("<OTHERFEES>");
                        strbFinalXML.Append("0");
                        strbFinalXML.Append("</OTHERFEES>");
                        strbFinalXML.Append("</PREMIUM>");

                        break;

                    case "AUTOP":
                    case "CYCL":
                        foreach (XmlNode riskNode in RiskNodes)
                        {
                            double dblPremium = 0, dblFees = 0;
                            Premium objPremium = new Premium();
                            objPremium.RiskID = Convert.ToInt32(riskNode.Attributes["ID"].Value.Trim());
                            objPremium.RiskType = riskNode.Attributes["TYPE"].Value.Trim();
                            XmlNode premiumNode = riskNode.SelectSingleNode("STEP[@COMPONENT_CODE ='SUMTOTAL']");

                            if (premiumNode != null)
                            {
                                dblPremium = Convert.ToDouble(premiumNode.Attributes["STEPPREMIUM"].Value.Trim());
                                dblTotalPremium += dblPremium;
                            }

                            //Added by Manoj Rathore on 3 Jun. 2009 Itrack # 5933	

                            XmlNodeList FeesNodeList = riskNode.SelectNodes("STEP[@COMPONENT_CODE ='MCCAFEE']");
                            foreach (XmlNode FeesNode in FeesNodeList)
                            {
                                if (FeesNode != null)
                                {
                                    //Added by Praveen (04 Nov 2009)
                                    //If Premium XML, contains two node for MCCAFEE ,then it was adding Fee twice.
                                    if (Convert.ToDouble(FeesNode.Attributes["STEPPREMIUM"].Value.Trim()) > 0.0)
                                    {
                                        //dblFees =dblFees + Convert.ToDouble(FeesNode.Attributes["STEPPREMIUM"].Value.Trim());
                                        dblFees = Convert.ToDouble(FeesNode.Attributes["STEPPREMIUM"].Value.Trim());
                                        dblTotalFees += dblFees;
                                    }
                                }
                            }
                            /*
                            XmlNode FeesNode = riskNode.SelectSingleNode("STEP[@TS ='9']"); 
                            if(FeesNode != null)
                            {
                                dblFees = Convert.ToDouble(FeesNode.Attributes["STEPPREMIUM"].Value.Trim());
                                dblTotalFees += dblFees;
                            }*/

                            objPremium.GrossPremium = dblPremium;
                            objPremium.NetPremium = dblPremium - dblFees;
                            objPremium.MCCAFees = dblFees;
                            objPremium.OtherFees = 0;

                            arrPremium.Add(objPremium);
                        }
                        dblNetPremium = dblTotalPremium - dblTotalFees;
                        strbFinalXML.Append("<PREMIUM>");
                        //Node For GROSSPREMIUM
                        strbFinalXML.Append("<GROSSPREMIUM>");
                        strbFinalXML.Append(dblTotalPremium);
                        strbFinalXML.Append("</GROSSPREMIUM>");
                        //Node For NETPREMIUM
                        strbFinalXML.Append("<NETPREMIUM>");
                        strbFinalXML.Append(dblNetPremium);
                        strbFinalXML.Append("</NETPREMIUM>");
                        //Node for FEES
                        strbFinalXML.Append("<MCCAFEES>");
                        strbFinalXML.Append(dblTotalFees);
                        strbFinalXML.Append("</MCCAFEES>");
                        //Node for OTHRERFEES
                        strbFinalXML.Append("<OTHERFEES>");
                        strbFinalXML.Append("0");
                        strbFinalXML.Append("</OTHERFEES>");

                        strbFinalXML.Append("</PREMIUM>");

                        break;
                    case "BOAT":
                    case "HOME-BOAT":
                        foreach (XmlNode riskNode in RiskNodes)
                        {
                            double dblPremium = 0;
                            Premium objPremium = new Premium();
                            if (riskNode.Attributes["TYPE"].Value.Trim() == "B")
                            {
                                objPremium.RiskID = Convert.ToInt32(riskNode.Attributes["ID"].Value.Trim());
                                objPremium.RiskType = riskNode.Attributes["TYPE"].Value.Trim();
                                XmlNode premiumNode = riskNode.SelectSingleNode("STEP[@COMPONENT_CODE ='SUMTOTAL']");
                                dblPremium = Convert.ToDouble(premiumNode.Attributes["STEPPREMIUM"].Value.Trim());
                                dblTotalPremium += dblPremium;

                                objPremium.GrossPremium = dblPremium;
                                objPremium.NetPremium = dblPremium;
                                objPremium.MCCAFees = 0;
                                objPremium.OtherFees = 0;

                                arrPremium.Add(objPremium);
                            }
                        }
                        foreach (XmlNode riskNode in RiskNodes)
                        {
                            double dblPremium = 0;

                            if (riskNode.Attributes["TYPE"].Value.Trim() != "B")
                            {
                                Premium objPremium = (Premium)arrPremium[0];
                                XmlNode premiumNode = riskNode.SelectSingleNode("STEP[@COMPONENT_CODE ='SUMTOTAL_S']");
                                if (premiumNode != null)
                                {
                                    dblPremium = Convert.ToDouble(premiumNode.Attributes["STEPPREMIUM"].Value.Trim());
                                    dblTotalPremium += dblPremium;
                                }

                                premiumNode = riskNode.SelectSingleNode("STEP[@COMPONENT_CODE ='BOAT_UNATTACH_PREMIUM']");
                                if (premiumNode != null)
                                {
                                    dblPremium = Convert.ToDouble(premiumNode.Attributes["STEPPREMIUM"].Value.Trim());
                                    dblTotalPremium += dblPremium;
                                }

                                premiumNode = riskNode.SelectSingleNode("STEP[@COMPONENT_CODE ='SUMTOTAL']");
                                if (premiumNode != null)
                                {
                                    dblPremium = Convert.ToDouble(premiumNode.Attributes["STEPPREMIUM"].Value.Trim());
                                    dblTotalPremium += dblPremium;
                                }

                                objPremium.GrossPremium = objPremium.GrossPremium + dblPremium;
                                objPremium.NetPremium = objPremium.NetPremium + dblPremium;
                            }
                        }
                        dblNetPremium = dblTotalPremium;

                        strbFinalXML.Append("<PREMIUM>");
                        //Node For GROSSPREMIUM
                        strbFinalXML.Append("<GROSSPREMIUM>");
                        strbFinalXML.Append(dblTotalPremium);
                        strbFinalXML.Append("</GROSSPREMIUM>");
                        //Node For NETPREMIUM
                        strbFinalXML.Append("<NETPREMIUM>");
                        strbFinalXML.Append(dblNetPremium);
                        strbFinalXML.Append("</NETPREMIUM>");
                        //Node for FEES
                        strbFinalXML.Append("<MCCAFEES>");
                        strbFinalXML.Append("0");
                        strbFinalXML.Append("</MCCAFEES>");
                        //Node for OTHRERFEES
                        strbFinalXML.Append("<OTHERFEES>");
                        strbFinalXML.Append("0");
                        strbFinalXML.Append("</OTHERFEES>");

                        strbFinalXML.Append("</PREMIUM>");
                        break;
                    default:

                        foreach (XmlNode riskNode in RiskNodes)
                        {
                            double dblPremium = 0, dblFees = 0;
                            Premium objPremium = new Premium();
                            objPremium.RiskID = Convert.ToInt32(riskNode.Attributes["ID"].Value.Trim());
                            objPremium.RiskType = riskNode.Attributes["TYPE"].Value.Trim();
                            XmlNode premiumNode = riskNode.SelectSingleNode("STEP[@COMPONENT_CODE ='SUMTOTAL']");

                            if (premiumNode != null)
                            {
                                dblPremium = Convert.ToDouble(premiumNode.Attributes["STEPPREMIUM"].Value.Trim(), decimalNumberFormat);
                                dblTotalPremium += dblPremium;
                            }

                            objPremium.GrossPremium = dblPremium;
                            objPremium.NetPremium = dblPremium - dblFees;
                            objPremium.MCCAFees = dblFees;
                            objPremium.OtherFees = 0;

                            arrPremium.Add(objPremium);
                        }
                        dblNetPremium = dblTotalPremium - dblTotalFees;
                        strbFinalXML.Append("<PREMIUM>");
                        //Node For GROSSPREMIUM
                        strbFinalXML.Append("<GROSSPREMIUM>");
                        strbFinalXML.Append(dblTotalPremium);
                        strbFinalXML.Append("</GROSSPREMIUM>");
                        //Node For NETPREMIUM
                        strbFinalXML.Append("<NETPREMIUM>");
                        strbFinalXML.Append(dblNetPremium);
                        strbFinalXML.Append("</NETPREMIUM>");
                        //Node for FEES
                        strbFinalXML.Append("<MCCAFEES>");
                        strbFinalXML.Append(dblTotalFees);
                        strbFinalXML.Append("</MCCAFEES>");
                        //Node for OTHRERFEES
                        strbFinalXML.Append("<OTHERFEES>");
                        strbFinalXML.Append("0");
                        strbFinalXML.Append("</OTHERFEES>");

                        strbFinalXML.Append("</PREMIUM>");
                        break;

                }
                strFinalPremium = strbFinalXML.ToString();
                dblNetFinalPremium = dblNetPremium;
            }
            return arrPremium;

        }
        //by Pravesh K Chandel . remove Premium XML dependency

        public System.Collections.ArrayList GetPremiumDetailsFromBillingInfo(int CustomerId, int PolicyId,
           int PolicyVersionId, string Term
           , int ProcessType, out int PremiumProcessTableRowID)
        {
            return GetPremiumDetailsFromBillingInfo(CustomerId, PolicyId, PolicyVersionId, Term
                                 , ProcessType, out PremiumProcessTableRowID, 0, 0);
        }


        public System.Collections.ArrayList GetPremiumDetailsFromBillingInfo(int CustomerId, int PolicyId,
            int PolicyVersionId, string Term
            , int ProcessType, out int PremiumProcessTableRowID, int PRODUCT_TYPE, int CO_INSURANCE)
        {

            PremiumProcessTableRowID = 0;
            ArrayList arrPremium = new ArrayList();
            StringBuilder sbFinalXML = new StringBuilder();
            try
            {
                DataSet dsPremium = GetPolicyPremiumFromBillingInfo(CustomerId, PolicyId, PolicyVersionId);
                if (dsPremium.Tables[0].Rows.Count == 0)
                {
                    if (PRODUCT_TYPE != enumPOLICY_TRANSACTION_TYPE.OPEN_POLICY
                        && CO_INSURANCE != enumCO_INSURANCE.FOLLOWER)
                        return arrPremium;
                        //throw (new Exception("Process could not be committed. Please complete billing Info Tab."));
                }
                for (int j = 0; j < dsPremium.Tables[1].Rows.Count; j++)
                {
                    Premium objPremium = new Premium();
                    objPremium.RiskID = Convert.ToInt32(dsPremium.Tables[1].Rows[j]["RISK_ID"].ToString());
                    objPremium.RiskType = dsPremium.Tables[1].Rows[j]["RISK_TYPE"].ToString();
                    objPremium.NetPremium = Convert.ToDouble(dsPremium.Tables[1].Rows[j]["WRITTEN_PREMIUM"].ToString());
                    objPremium.MCCAFees = Convert.ToDouble(dsPremium.Tables[1].Rows[j]["MCCAFEES"].ToString());
                    objPremium.OtherFees = 0;
                    objPremium.GrossPremium = objPremium.MCCAFees + Convert.ToDouble(dsPremium.Tables[1].Rows[j]["WRITTEN_PREMIUM"].ToString());
                    objPremium.InforcePremium = objPremium.NetPremium;
                    arrPremium.Add(objPremium);
                }
                /*
                Premium objPremium = new Premium();

                dblTotalNet     = objPremium.NetPremium     = Convert.ToDouble(dsPremium.Tables[0].Rows[0]["TOTAL_PREMIUM"].ToString());
                dblTotalMCCA    = objPremium.MCCAFees       = 0;
                dblTotalOSTA    = objPremium.OtherFees      = 0;
                dblTotalGross   = objPremium.GrossPremium   = Convert.ToDouble(dsPremium.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString());
                arrPremium.Add(objPremium);
				*/

                //Saving Risk Level premium & Inforce Premium
                for (int i = 0; i < arrPremium.Count; i++)
                {
                    Premium objPremium = (Premium)(arrPremium[i]);
                    //If no change in premium do not insert record in PPD
                    if (objPremium.NetPremium == 0)
                        continue;

                    InsertIntoPremiumSubDetails(CustomerId, PolicyId, PolicyVersionId, objPremium.RiskID,
                        objPremium.RiskType, PremiumProcessTableRowID, objPremium.NetPremium,
                        objPremium.MCCAFees + objPremium.OtherFees, objPremium.GrossPremium,
                        objPremium.InforcePremium, objPremium.InforceFees);
                }
                return arrPremium;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public System.Collections.ArrayList GetPremiumDetails(int CustomerId, int PolicyId,
            int PolicyVersionId, ArrayList arrPremiumXML, string Term
            , int ProcessType, out int PremiumProcessTableRowID)
        {

            PremiumProcessTableRowID = 0;
            int returnResult = 0;
            double dblNetFinalPremium = 0;
            ArrayList arrPremium = new ArrayList();
            ArrayList arrTemp = new ArrayList();
            StringBuilder sbFinalXML = new StringBuilder();
            double dblTotalGross = 0, dblTotalNet = 0, dblTotalMCCA = 0, dblTotalOSTA = 0;
            string strFinalPremium = "";
            try
            {
                for (int i = 0; i < arrPremiumXML.Count; i++)
                {
                    PremiumDetails objPremiumdetails = (PremiumDetails)arrPremiumXML[i];
                    arrTemp = GetPremiumFromXML(objPremiumdetails.PremiumXML, objPremiumdetails.QuoteType, out strFinalPremium, out dblNetFinalPremium);
                    for (int j = 0; j < arrTemp.Count; j++)
                    {
                        Premium objPremium = (Premium)arrTemp[j];
                        dblTotalNet += objPremium.NetPremium;
                        dblTotalMCCA += objPremium.MCCAFees;
                        dblTotalOSTA += objPremium.OtherFees;
                        dblTotalGross += objPremium.GrossPremium;
                        arrPremium.Add(objPremium);
                    }
                }
                sbFinalXML.Append("<PREMIUM>");
                //Node For GROSSPREMIUM
                sbFinalXML.Append("<GROSSPREMIUM>");
                sbFinalXML.Append(dblTotalGross);
                sbFinalXML.Append("</GROSSPREMIUM>");
                //Node For NETPREMIUM
                sbFinalXML.Append("<NETPREMIUM>");
                sbFinalXML.Append(dblTotalNet);
                sbFinalXML.Append("</NETPREMIUM>");
                //Node for FEES
                sbFinalXML.Append("<MCCAFEES>");
                sbFinalXML.Append(dblTotalMCCA);
                sbFinalXML.Append("</MCCAFEES>");
                //Node for OTHRERFEES
                sbFinalXML.Append("<OTHERFEES>");
                sbFinalXML.Append(dblTotalOSTA);
                sbFinalXML.Append("</OTHERFEES>");
                sbFinalXML.Append("</PREMIUM>");
                strFinalPremium = sbFinalXML.ToString();
            }
            catch (Exception ex)
            {
                /*Comment by Ashu on 11 July 2011 as per Rajan Agarwal mail*/
                //strFinalPremium = "<PREMIUM><GROSSPREMIUM>400</GROSSPREMIUM><NETPREMIUM>400</NETPREMIUM><OTHERFEES>0</OTHERFEES><MCCAFEES>0</MCCAFEES></PREMIUM>";
                //Premium objPremium = new Premium();
                //objPremium.RiskID = 1;
                //objPremium.NetPremium = 400;
                //objPremium.GrossPremium = 400;
                //objPremium.OtherFees = 0;
                //objPremium.MCCAFees = 0;
                //arrPremium.Add(objPremium);
                //InsertFinalPremium(CustomerId, PolicyId, PolicyVersionId, Term, ProcessType, strFinalPremium, strFinalPremium, dblNetFinalPremium);
                //PremiumProcessTableRowID = returnResult;
                //return arrPremium;
                /*Comment by Ashu on 11 July 2011 as per Rajan Agarwal mail end*/
                /*Add by Ashu on 11 July 2011 as per Rajan Agarwal mail*/
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                throw ex;
                /*Add by Ashu on 11 July 2011 as per Rajan Agarwal mail end*/
            }
            //Saving the Premium XML.
            returnResult = InsertFinalPremium(CustomerId, PolicyId, PolicyVersionId, Term, ProcessType, strFinalPremium, strFinalPremium, dblNetFinalPremium);
            PremiumProcessTableRowID = returnResult;
            return arrPremium;
        }

        #region Commented By Ravindra (Not used AnyMore)
        /*public System.Collections.ArrayList GetPremiumDetails(int AppID, int AppVersionID, int PolicyId, 
			int PolicyVersionId, int CustomerId
			, int LobId, out double dblPremium, out double dblOtherFees, out double dblMCCAFees
			, out int PremiumProcessTableRowID, string Term)
		{
			PremiumProcessTableRowID = 0;
			dblPremium		= 0;
			dblOtherFees	= 0;
			dblMCCAFees		= 0;
			System.Collections.ArrayList arrPremium = new System.Collections.ArrayList();
			try
			{
				//Generating the input xml, for rating engine
				ClsGeneralInformation objGeneralInfo = new ClsGeneralInformation();
				
				//if ( strXML == "")
				//string strXML = objGeneralInfo.GetInputXML(CustomerId, AppID, AppVersionID, LobId.ToString());

				string strXML = "";
				string strFinalPremium = "";

				try
				{
				
					strXML = objGeneralInfo.GetPolicyInputXML(CustomerId, PolicyId, PolicyVersionId, LobId.ToString());	
			
					//Retreiving the premium xml
					arrPremium = GetPremium(strXML, LobId.ToString(), PolicyId, PolicyVersionId, CustomerId,out strFinalPremium);
				}
				catch(Exception ex)
				{
					strFinalPremium = "<PREMIUM><NETPREMIUM>400</NETPREMIUM><OTHERFEES>0</OTHERFEES><MCCAFEES>0</MCCAFEES></PREMIUM>";
					Premium objPremium = new Premium();
					objPremium.RiskID=1;
					objPremium.NetPremium=400;
					objPremium.GrossPremium=400;
					objPremium.OtherFees=0;
					objPremium.MCCAFees=0;
					arrPremium.Add(objPremium);
					return arrPremium ;
				}
				
				//Saving the Premium XML.
				int returnResult = InsertFinalPremium(CustomerId, PolicyId, PolicyVersionId, Term, strFinalPremium, strFinalPremium);

				PremiumProcessTableRowID = returnResult;
				
				return arrPremium;

				//Commented By Ravindra (09-15-2006)
//				//Retreivng different nodes containing difft amounts from premium xml
//				//Whicih will passed in output variable
//				System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
//				doc.LoadXml(strFinalPremium);
//
//				System.Xml.XmlNode node;
//

//
//				node = doc.SelectSingleNode("//PREMIUM/NETPREMIUM");
//				if (node != null)
//					dblPremium = int.Parse(node.InnerXml);
//
//				node = doc.SelectSingleNode("//PREMIUM/OTHERFEES");
//				if (node != null)
//					dblOtherFees = int.Parse(node.InnerXml);
//			
//				node = doc.SelectSingleNode("//PREMIUM/MCCAFEES");
//				if (node != null)
//					dblMCCAFees = int.Parse(node.InnerXml);
			}
			catch(Exception objExp)
			{
				
				dblPremium		= 200;
				dblOtherFees	= 0;
				dblMCCAFees		= 0;
				return null;
			}

		}


		public System.Collections.ArrayList  GetPremium(string strInputXML,string strLOB, int PolicyID
			, int PolicyVersionID, int CustomerID,out string PremiumXml)
		{
			string FinalQuoteXml;
			ClsGenerateQuote objQuote	=	new ClsGenerateQuote();
			PremiumXml="";
			System.Collections.ArrayList arrPremium = objQuote.GetPremiumFromXML(strInputXML, strLOB, PolicyID
				, PolicyVersionID, CustomerID, out FinalQuoteXml, out PremiumXml );

			SaveFinalXML(PolicyID, PolicyVersionID, CustomerID, FinalQuoteXml);

			//return PremiumXml;
			return arrPremium;
		}

		/// <summary>
		/// Saving the final xml in database
		/// </summary>
		/// <param name="?"></param>
		/// <param name="?"></param>
		/// <param name="?"></param>
		private void SaveFinalXML(int PolicyID, int PolicyVersionID, int CustomerID, string FinalXML)
		{
			try
			{
				//Saving the xml in database
				
				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@POLICY_ID", PolicyID);
				objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
				objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
				objWrapper.AddParameter("@POLICY_PREMIUM_XML", FinalXML);
				objWrapper.ExecuteNonQuery("Proc_UpdatePOL_CUSTOMER_POLICY_LIST_POLICY_PREMIUM_XML");
				
				objWrapper.ClearParameteres();

			}
			catch (Exception objExp)
			{
				throw (objExp);
			}
		}*/
        #endregion
        //by Pravesh on 08 aug 2010 to get PolicyPremium from Billing Info Tab
        public DataSet GetPolicyPremiumFromBillingInfo(int CustomerId, int PolicyId, int PolicyVersionId)
        {
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);

                DataSet ds = objWrapper.ExecuteDataSet("Proc_GetPolicyPremiumFromBillingInfo");
                objWrapper.ClearParameteres();
                return ds;
            }
            catch (Exception objExp)
            {
                throw (objExp);
            }
        }
        /// <summary>
        /// Inserts the passed rating xml in premium process table
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="PolicyId"></param>
        /// <param name="PolicyVersionId"></param>
        /// <param name="strPremiumXML"></param>
        /// <returns>Row id of inserted record</returns>
        public int InsertFinalPremium(int CustomerId, int PolicyId, int PolicyVersionId, string Term,
            int ProcessType, string strPremiumXML, string PostedPremiumXml, double dblNetFinalPremium)
        {
            try
            {
                objWrapper.ClearParameteres();

                objWrapper.AddParameter("@TRANSACTION_TYPE", null);
                objWrapper.AddParameter("@TERM_TYPE", Term);
                objWrapper.AddParameter("@PROCESS_NUM", null);
                objWrapper.AddParameter("@PROCESS_DESC", null);
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                objWrapper.AddParameter("@APP_ID", null);
                objWrapper.AddParameter("@APP_VERSION_ID", null);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                objWrapper.AddParameter("@PREMIUM_AMOUNT", dblNetFinalPremium);
                objWrapper.AddParameter("@PROCESS_STATUS", null);
                objWrapper.AddParameter("@PREMIUM_XML", strPremiumXML);
                objWrapper.AddParameter("@POSTED_PREMIUM_XML", PostedPremiumXml);
                objWrapper.AddParameter("@PROCESS_TYPE", ProcessType);

                SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@PROCESS_ID", SqlDbType.Int, ParameterDirection.Output);

                int returnResult = objWrapper.ExecuteNonQuery("Proc_InsertACT_PREMIUM_PROCESS_DETAILS");

                returnResult = int.Parse(objSqlParameter.Value.ToString());
                objWrapper.ClearParameteres();
                return returnResult;
            }
            catch (Exception objExp)
            {
                throw (objExp);
            }
        }

        //Added By Ravindra(08-02-2007) For Saving Risk Level premium & Inforce Premium
        public void InsertIntoPremiumSubDetails(int CustomerId, int PolicyId, int PolicyVersionId, int RiskID,
            string RiskType, int PremiumProcessRowID, double NetPremium, double StatsFees,
            double GrossPremium, double InforcePremium, double InforceFees)
        {
            try
            {
                objWrapper.ClearParameteres();

                if (NetPremium != 0 && GrossPremium != 0)
                {
                    objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                    objWrapper.AddParameter("@POLICY_ID", PolicyId);
                    objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                    objWrapper.AddParameter("@PPD_ROW_ID", PremiumProcessRowID);
                    objWrapper.AddParameter("@RISK_ID", RiskID);
                    objWrapper.AddParameter("@RISK_TYPE", RiskType);
                    objWrapper.AddParameter("@NET_PREMIUM", NetPremium);
                    objWrapper.AddParameter("@STATS_FEES", StatsFees);
                    objWrapper.AddParameter("@GROSS_PREMIUM", GrossPremium);
                    objWrapper.AddParameter("@INFORCE_PREMIUM", InforcePremium);
                    //Added By Ravindra(08-24-2007)
                    objWrapper.AddParameter("@INFORCE_FEES", InforceFees);
                    int returnResult = objWrapper.ExecuteNonQuery("InsertPremiumSubDetails");
                }
                objWrapper.ClearParameteres();
            }
            catch (Exception objExp)
            {
                throw (objExp);
            }
        }
        public void GetAppLobDetails(int CustomerId, int PolicyId, int PolicyVersionId, out int AppId, out int AppVersionId, out int LobId)
        {
            string PolicyCurrencyId = "";
            GetAppLobDetails(CustomerId, PolicyId, PolicyVersionId, out  AppId, out AppVersionId, out LobId, out PolicyCurrencyId);
        }
        public void GetAppLobDetails(int CustomerId, int PolicyId, int PolicyVersionId, out int AppId, out int AppVersionId, out int LobId, out string PolicyCurrencyId)
        {

            int PRODUCT_TYPE = 0;
            int CO_INSURANCE = 0;
            string PLAN_TYPE = "";
            GetAppLobDetails(CustomerId, PolicyId, PolicyVersionId, out  AppId, out AppVersionId, out LobId, out PolicyCurrencyId, out PRODUCT_TYPE, out CO_INSURANCE, out PLAN_TYPE);
        }
        public void GetAppLobDetails(int CustomerId, int PolicyId, int PolicyVersionId, out int AppId, out int AppVersionId, out int LobId, out string PolicyCurrencyId, out int PRODUCT_TYPE,
         out int CO_INSURANCE)
        {
            string PLAN_TYPE = "";
            GetAppLobDetails(CustomerId, PolicyId, PolicyVersionId, out  AppId, out AppVersionId, out LobId, out PolicyCurrencyId, out PRODUCT_TYPE, out CO_INSURANCE, out PLAN_TYPE);
        }
        public void GetAppLobDetails(int CustomerId, int PolicyId, int PolicyVersionId, out int AppId, out int AppVersionId, out int LobId, out string PolicyCurrencyId, out int PRODUCT_TYPE,
            out int CO_INSURANCE, out string PLAN_TYPE)
        {
            try
            {
                DataSet dsTemp;
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                dsTemp = objWrapper.ExecuteDataSet("Proc_GetAppLobDetailsFromPolicy");
                AppId = int.Parse(dsTemp.Tables[0].Rows[0]["APP_ID"].ToString());
                AppVersionId = int.Parse(dsTemp.Tables[0].Rows[0]["APP_VERSION_ID"].ToString());
                LobId = int.Parse(dsTemp.Tables[0].Rows[0]["POLICY_LOB"].ToString());
                PolicyCurrencyId = dsTemp.Tables[0].Rows[0]["POLICY_CURRENCY"].ToString();
                PRODUCT_TYPE = int.Parse(dsTemp.Tables[0].Rows[0]["PRODUCT_TYPE"].ToString());
                CO_INSURANCE = int.Parse(dsTemp.Tables[0].Rows[0]["CO_INSURANCE"].ToString());
                PLAN_TYPE = dsTemp.Tables[0].Rows[0]["PLAN_TYPE"].ToString();
                objWrapper.ClearParameteres();
            }
            catch (Exception objExp)
            {
                throw new Exception("Error while fetching policy details", objExp);
                //throw (objExp);
            }
        }


        public Cms.Model.Policy.Process.ClsPolicyProcessNoticInfo GetProcessNoticeInfo(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
        {
            Cms.Model.Policy.Process.ClsPolicyProcessNoticInfo objNotice = new Cms.Model.Policy.Process.ClsPolicyProcessNoticInfo();
            objNotice.POLICY_ID = objProcessInfo.POLICY_ID;
            objNotice.POLICY_VERSION_ID = objProcessInfo.POLICY_VERSION_ID;
            objNotice.CUSTOMER_ID = objProcessInfo.CUSTOMER_ID;
            objNotice.CREATED_BY = objProcessInfo.COMPLETED_BY;
            objNotice.CREATED_DATETIME = objProcessInfo.COMPLETED_DATETIME;
            objNotice.IS_ACTIVE = "Y";
            objNotice.ROW_ID = objProcessInfo.ROW_ID;
            return objNotice;
        }
        #endregion

        #region VisiblePropertyInspectionCredit
        /// <summary>
        /// Makes the property inspection credit field visible or invisible 
        /// </summary>
        /// <returns></returns>
        public bool VisbilePropertyInspectionCredit(int CustomerID, int PolicyID, int PolicyVerID, int PrevVersionID)
        {
            try
            {
                //GetAppLobDetails(
                int AppId, AppVersionId, LobId;
                objWrapper = new DataLayer.DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure);
                GetAppLobDetails(CustomerID, PolicyID, PolicyVerID, out AppId, out AppVersionId, out LobId);
                objWrapper = null;

                //if (LobId != Cms.BusinessLayer.BlProcess.clsprocess.lo

                string Query = "SELECT  "
                    + " (SELECT COUNT(*) FROM POL_LOCATIONS WHERE "
                    + " POLICY_ID = " + PolicyID.ToString()
                    + " AND CUSTOMER_ID = " + CustomerID.ToString()
                    + " AND POLICY_VERSION_ID = " + PolicyVerID.ToString()
                    + " ) - "
                    + " (SELECT COUNT(*) FROM POL_LOCATIONS WHERE "
                    + " POLICY_ID = " + PolicyID.ToString()
                    + " AND CUSTOMER_ID = " + CustomerID.ToString()
                    + " AND POLICY_VERSION_ID = " + PrevVersionID.ToString()
                    + " ) ";


                string Value = Cms.DataLayer.DataWrapper.ExecuteScalar(ClsCommon.ConnStr, CommandType.Text, Query).ToString();

                if (Value != null && Value != "")
                {
                    if (int.Parse(Value) > 0)
                        return true;
                    else
                        return false;
                }
                return false;
            }
            catch (Exception objExp)
            {
                throw (objExp);
            }
        }
        #endregion

        #region Get Policy Status
        public string GetPolicyStatus(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            DataSet dsTemp;
            try
            {

                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POL_ID", PolicyID);
                objWrapper.AddParameter("@POL_VERSION_ID", PolicyVersionID);
                dsTemp = objWrapper.ExecuteDataSet("Proc_GetPolicyStatus");
                objWrapper.ClearParameteres();
                objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                if (dsTemp.Tables[0].Rows.Count > 0)
                    return dsTemp.Tables[0].Rows[0][0].ToString();
                else
                    return null;

            }
            catch (Exception objExp)
            {
                throw (objExp);
            }

        }
        #endregion

        #region Common method to fetch Document Code for Cancellation Notice
        public string GetDocumentCodeCancelNotice(string STATE_ID, string LOB_ID)
        {

            return "";

        }

        #endregion

        #region printing jobs
        public void AddPrintJobs(Cms.Model.Policy.Process.ClsPrintJobsInfo objPrintJobsInfo)
        {
            try
            {
                string filename = "";
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", objPrintJobsInfo.CUSTOMER_ID);
                objWrapper.AddParameter("@POLICY_ID", objPrintJobsInfo.POLICY_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", objPrintJobsInfo.POLICY_VERSION_ID);
                objWrapper.AddParameter("@DOCUMENT_CODE", objPrintJobsInfo.DOCUMENT_CODE);
                objWrapper.AddParameter("@PRINT_DATETIME", objPrintJobsInfo.PRINT_DATETIME);
                //				objWrapper.AddParameter("@PRINTED_DATETIME",objPrintJobsInfo.PRINTED_DATETIME);
                objWrapper.AddParameter("@PRINTED_DATETIME", null);
                string urlPath = objPrintJobsInfo.URL_PATH;
                urlPath = urlPath.Replace("./", "/");
                objWrapper.AddParameter("@URL_PATH", urlPath);
                objWrapper.AddParameter("@ONDEMAND_FLAG", objPrintJobsInfo.ONDEMAND_FLAG);
                objWrapper.AddParameter("@PRINT_SUCCESSFUL", objPrintJobsInfo.PRINT_SUCCESSFUL);
                objWrapper.AddParameter("@DUPLEX", objPrintJobsInfo.DUPLEX);
                objWrapper.AddParameter("@CREATED_DATETIME", objPrintJobsInfo.CREATED_DATETIME);
                objWrapper.AddParameter("@CREATED_BY", objPrintJobsInfo.CREATED_BY);
                objWrapper.AddParameter("@ENTITY_TYPE", objPrintJobsInfo.ENTITY_TYPE);
                objWrapper.AddParameter("@AGENCY_ID", objPrintJobsInfo.AGENCY_ID);
                if (NoWordingPDFFileName != "" && objPrintJobsInfo.ENTITY_TYPE == ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_CUSTOMER
                    && (objPrintJobsInfo.PROCESS_ID == POLICY_COMMIT_ENDORSEMENT_PROCESS || objPrintJobsInfo.PROCESS_ID == POLICY_COMMIT_RENEWAL_PROCESS || objPrintJobsInfo.PROCESS_ID == POLICY_COMMIT_REINSTATEMENT_PROCESS))
                {
                    objWrapper.AddParameter("@FILE_NAME", NoWordingPDFFileName);
                    filename = NoWordingPDFFileName;
                }
                else
                {
                    objWrapper.AddParameter("@FILE_NAME", objPrintJobsInfo.FILE_NAME);
                    filename = objPrintJobsInfo.FILE_NAME;
                }

                objWrapper.AddParameter("@PROCESS_ID", objPrintJobsInfo.PROCESS_ROW_ID);


                if (IsPdfFileExists(urlPath, filename))
                {
                    int returnResult = objWrapper.ExecuteNonQuery("Proc_InsertPRINT_JOBS");
                }
                else
                {
                    System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
                    addInfo.Add("Err Descriptor ", "Error while adding entry in prin jobs.file not found.");
                    addInfo.Add("CustomerID", objPrintJobsInfo.CUSTOMER_ID.ToString());
                    addInfo.Add("PolicyID", objPrintJobsInfo.POLICY_ID.ToString());
                    addInfo.Add("PolicyVersionID", objPrintJobsInfo.POLICY_VERSION_ID.ToString());
                    addInfo.Add("ProcessRowID", objPrintJobsInfo.PROCESS_ID.ToString());
                    addInfo.Add("FileURLPath", objPrintJobsInfo.URL_PATH);
                    addInfo.Add("FileName", objPrintJobsInfo.FILE_NAME);
                    addInfo.Add("DOCUMENT_CODE", objPrintJobsInfo.DOCUMENT_CODE);
                    Exception ex = new Exception("Error while inserting Print job entry.File not found.");
                    ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex, addInfo);

                    throw (ex);
                }
                objWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding data to print jobs table", ex);
            }

        }
        public string GenerateAviationPdf(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
        {

            string strAviationDecPage = "";
            string InputBase;
            string OutputPath;
            string FinalBasePath;
            int oldPolicyVerId = objProcessInfo.POLICY_VERSION_ID;
            objProcessInfo.POLICY_VERSION_ID = objProcessInfo.NEW_POLICY_VERSION_ID;
            if (IsEODProcess)
            {

                InputBase = UploadPath + "\\INPUTPDFs\\";
                InputBase = System.IO.Path.GetFullPath(InputBase);
                OutputPath = UploadPath + "\\OUTPUTPDFs\\";
                OutputPath = System.IO.Path.GetFullPath(OutputPath);
                FinalBasePath = UploadURL + "/OUTPUTPDFs/";
            }
            else
            {
                InputBase  = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()).ToString() + "\\INPUTPDFs\\";
                OutputPath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString()).ToString() + "\\OUTPUTPDFs\\";
                FinalBasePath = System.Configuration.ConfigurationManager.AppSettings.Get("UploadURL").ToString() + "/OUTPUTPDFs/";
            }
            if (objProcessInfo.AGENCY_CODE == "")
            {
                objProcessInfo.AGENCY_CODE = GetPDFAgencyCode(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, "POLICY");
            }

            strAviationDecPage = "Cert_GeneralAviation" + "_" + objProcessInfo.CUSTOMER_ID.ToString() + "_" + objProcessInfo.POLICY_ID.ToString() + "_" + objProcessInfo.POLICY_VERSION_ID.ToString() + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
            string strInputFile = InputBase + "Cert_GeneralAviation.pdf";
            string strOutputFile = OutputPath + objProcessInfo.AGENCY_CODE + "\\" + objProcessInfo.CUSTOMER_ID.ToString() + "\\" + "POLICY" + "\\" + "final" + "\\" + strAviationDecPage;
            if (!System.IO.Directory.Exists(OutputPath + objProcessInfo.AGENCY_CODE))
                System.IO.Directory.CreateDirectory(OutputPath + objProcessInfo.AGENCY_CODE);
            if (!System.IO.Directory.Exists(OutputPath + objProcessInfo.AGENCY_CODE + "\\" + objProcessInfo.CUSTOMER_ID.ToString()))
                System.IO.Directory.CreateDirectory(OutputPath + objProcessInfo.AGENCY_CODE + "\\" + objProcessInfo.CUSTOMER_ID.ToString());
            if (!System.IO.Directory.Exists(OutputPath + objProcessInfo.AGENCY_CODE + "\\" + objProcessInfo.CUSTOMER_ID.ToString() + "\\" + "POLICY" + "\\" + "final"))
                System.IO.Directory.CreateDirectory(OutputPath + objProcessInfo.AGENCY_CODE + "\\" + objProcessInfo.CUSTOMER_ID.ToString() + "\\" + "POLICY" + "\\" + "final");

            System.IO.File.Copy(strInputFile, strOutputFile, true);
            ClsPrintJobsInfo objPrintJobsInfo = GetPrintJobsValues(objProcessInfo);
            if (objProcessInfo.PRINTING_OPTIONS == (int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.NO))
            {
                // do entry in print job other than No print required --chnanged on 3 april 2008
                objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER;
                objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
                objPrintJobsInfo.FILE_NAME = strAviationDecPage;
                if (objProcessInfo.INSURED != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString()))
                    AddPrintJobs(objPrintJobsInfo);
            }
            objProcessInfo.POLICY_VERSION_ID = oldPolicyVerId;
            return strAviationDecPage;
        }
        public void AddPdfFileLog(Cms.Model.Policy.Process.ClsPrintJobsInfo objPrintJobsInfo)
        {
            try
            {
                string filename = "";
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", objPrintJobsInfo.CUSTOMER_ID);
                objWrapper.AddParameter("@POLICY_ID", objPrintJobsInfo.POLICY_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", objPrintJobsInfo.POLICY_VERSION_ID);
                objWrapper.AddParameter("@DOCUMENT_CODE", objPrintJobsInfo.DOCUMENT_CODE);
                objWrapper.AddParameter("@PRINT_DATETIME", objPrintJobsInfo.PRINT_DATETIME);
                string urlPath = objPrintJobsInfo.URL_PATH;
                urlPath = urlPath.Replace("./", "/");
                objWrapper.AddParameter("@URL_PATH", urlPath);
                objWrapper.AddParameter("@CREATED_DATETIME", objPrintJobsInfo.CREATED_DATETIME);
                objWrapper.AddParameter("@CREATED_BY", objPrintJobsInfo.CREATED_BY);
                objWrapper.AddParameter("@ENTITY_TYPE", objPrintJobsInfo.ENTITY_TYPE);
                objWrapper.AddParameter("@AGENCY_ID", objPrintJobsInfo.AGENCY_ID);
                objWrapper.AddParameter("@FILE_NAME", objPrintJobsInfo.FILE_NAME);
                filename = objPrintJobsInfo.FILE_NAME;
                objWrapper.AddParameter("@PROCESS_ID", objPrintJobsInfo.PROCESS_ROW_ID);
                if (IsPdfFileExists(urlPath, filename))
                {
                    int returnResult = objWrapper.ExecuteNonQuery("Proc_InsertPDF_FILE_LOG");
                }
                else
                {
                    System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
                    addInfo.Add("Err Descriptor ", "Error while adding entry in prin jobs.file not found.");
                    addInfo.Add("CustomerID", objPrintJobsInfo.CUSTOMER_ID.ToString());
                    addInfo.Add("PolicyID", objPrintJobsInfo.POLICY_ID.ToString());
                    addInfo.Add("PolicyVersionID", objPrintJobsInfo.POLICY_VERSION_ID.ToString());
                    addInfo.Add("ProcessRowID", objPrintJobsInfo.PROCESS_ID.ToString());
                    addInfo.Add("FileURLPath", objPrintJobsInfo.URL_PATH);
                    addInfo.Add("FileName", objPrintJobsInfo.FILE_NAME);
                    addInfo.Add("DOCUMENT_CODE", objPrintJobsInfo.DOCUMENT_CODE);
                    Exception ex = new Exception("Error while inserting Pdf file log entry.File not found.");
                    ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex, addInfo);

                    throw (ex);
                }
                objWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding data to Pdf File Log table", ex);
            }
        }
        private bool IsPdfFileExists(string urlPath, string fileName)
        {
            string filePath;
            string WebURl = CmsWebUrl.ToUpper();

            filePath = WebURl.Replace("CMS/CMSWEB/", "") + @"/" + urlPath + @"\" + fileName;

            try
            {
                System.Net.WebRequest objWebReq = System.Net.WebRequest.Create(filePath);

                //Ravindra(03-30-2009): Pass credentials to webrequest, upload dricetory will be depolyed in secure mode
                objWebReq.Credentials = new System.Net.NetworkCredential(ClsCommon.ImpersonationUserId, ClsCommon.ImpersonationPassword, ClsCommon.ImpersonationDomain);

                objWebReq.Method = "HEAD";
                System.Net.WebResponse wresp = objWebReq.GetResponse();
                if (wresp is System.Net.HttpWebResponse)
                {
                    System.Net.HttpWebResponse hwresp = (System.Net.HttpWebResponse)wresp;
                    if (hwresp.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (System.Net.WebException wex)
            {
                if (wex.Response == null)
                {
                    return false;
                }
                else
                {
                    if (wex.Response is System.Net.HttpWebResponse)
                    {
                        System.Net.HttpWebResponse hwresp =
                            (System.Net.HttpWebResponse)wex.Response;
                        if (hwresp.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            throw new SystemException("File not found.");
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

            }

        }
        //Overload Added by Ravindra(03-12-2008)
        public void AddPrintJobs(Cms.Model.Policy.Process.ClsPrintJobsInfo objPrintJobsInfo, DataWrapper objWrapper)
        {
            try
            {
                string filename = "";
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", objPrintJobsInfo.CUSTOMER_ID);
                objWrapper.AddParameter("@POLICY_ID", objPrintJobsInfo.POLICY_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", objPrintJobsInfo.POLICY_VERSION_ID);
                objWrapper.AddParameter("@DOCUMENT_CODE", objPrintJobsInfo.DOCUMENT_CODE);
                objWrapper.AddParameter("@PRINT_DATETIME", objPrintJobsInfo.PRINT_DATETIME);
                //				objWrapper.AddParameter("@PRINTED_DATETIME",objPrintJobsInfo.PRINTED_DATETIME);
                objWrapper.AddParameter("@PRINTED_DATETIME", null);
                string urlPath = objPrintJobsInfo.URL_PATH;
                urlPath = urlPath.Replace("./", "/");
                objWrapper.AddParameter("@URL_PATH", urlPath);
                objWrapper.AddParameter("@ONDEMAND_FLAG", objPrintJobsInfo.ONDEMAND_FLAG);
                objWrapper.AddParameter("@PRINT_SUCCESSFUL", objPrintJobsInfo.PRINT_SUCCESSFUL);
                objWrapper.AddParameter("@DUPLEX", objPrintJobsInfo.DUPLEX);
                objWrapper.AddParameter("@CREATED_DATETIME", objPrintJobsInfo.CREATED_DATETIME);
                objWrapper.AddParameter("@CREATED_BY", objPrintJobsInfo.CREATED_BY);
                objWrapper.AddParameter("@ENTITY_TYPE", objPrintJobsInfo.ENTITY_TYPE);
                objWrapper.AddParameter("@AGENCY_ID", objPrintJobsInfo.AGENCY_ID);
                if (NoWordingPDFFileName != "" && objPrintJobsInfo.ENTITY_TYPE == ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_CUSTOMER
                    && (objPrintJobsInfo.PROCESS_ID == POLICY_COMMIT_ENDORSEMENT_PROCESS || objPrintJobsInfo.PROCESS_ID == POLICY_COMMIT_RENEWAL_PROCESS || objPrintJobsInfo.PROCESS_ID == POLICY_COMMIT_REINSTATEMENT_PROCESS))
                {
                    objWrapper.AddParameter("@FILE_NAME", NoWordingPDFFileName);
                    filename = NoWordingPDFFileName;
                }
                else
                {
                    objWrapper.AddParameter("@FILE_NAME", objPrintJobsInfo.FILE_NAME);
                    filename = objPrintJobsInfo.FILE_NAME;
                }
                objWrapper.AddParameter("@PROCESS_ID", objPrintJobsInfo.PROCESS_ROW_ID);

                if (IsPdfFileExists(urlPath, filename))
                {
                    int returnResult = objWrapper.ExecuteNonQuery("Proc_InsertPRINT_JOBS");
                }
                else
                {
                    System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
                    addInfo.Add("Err Descriptor ", "Error while adding entry in prin jobs.file not found.");
                    addInfo.Add("CustomerID", objPrintJobsInfo.CUSTOMER_ID.ToString());
                    addInfo.Add("PolicyID", objPrintJobsInfo.POLICY_ID.ToString());
                    addInfo.Add("PolicyVersionID", objPrintJobsInfo.POLICY_VERSION_ID.ToString());
                    addInfo.Add("ProcessRowID", objPrintJobsInfo.PROCESS_ID.ToString());
                    addInfo.Add("FileURLPath", objPrintJobsInfo.URL_PATH);
                    addInfo.Add("FileName", objPrintJobsInfo.FILE_NAME);
                    addInfo.Add("DOCUMENT_CODE", objPrintJobsInfo.DOCUMENT_CODE);
                    Exception ex = new Exception("Error while inserting Print job entry.File not found.");
                    ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex, addInfo);
                    throw (ex);
                }
                objWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding data to print jobs table", ex);
            }

        }
        /// <summary>
        /// to check refer to under writer if already checked off return 2 else update it
        /// </summary>
        /// <param name="intCUSTOMER_ID"></param>
        /// <param name="intPOLICY_ID"></param>
        /// <param name="intPOLICY_VERSION_ID"></param>
        /// <returns></returns>
        public int UpdatePolicyReferToUnderWriter(int intCUSTOMER_ID, int intPOLICY_ID, int intPOLICY_VERSION_ID)
        {
            return UpdatePolicyReferToUnderWriter(intCUSTOMER_ID, intPOLICY_ID, intPOLICY_VERSION_ID, "");
        }
        public int UpdatePolicyReferToUnderWriter(int intCUSTOMER_ID, int intPOLICY_ID, int intPOLICY_VERSION_ID, string strCalledFor)
        {
            objWrapper.ClearParameteres();
            objWrapper.AddParameter("@CUSTOMER_ID", intCUSTOMER_ID);
            objWrapper.AddParameter("@POLICY_ID", intPOLICY_ID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", intPOLICY_VERSION_ID);
            objWrapper.AddParameter("@CALLEDFOR", strCalledFor);

            SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@RETVAL", SqlDbType.Int, ParameterDirection.Output);
            int returnResult = 0;
            try
            {
                returnResult = objWrapper.ExecuteNonQuery("Proc_UpdatePolicyReferToUnderWriter");
                //objWrapper.ClearParameteres();
                if (objSqlParameter.Value != System.DBNull.Value)
                {
                    returnResult = Convert.ToInt32(objSqlParameter.Value);
                    return returnResult;
                }
                else
                    return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public DataSet GetPrintDocuments(int Process_ID)
        {
            try
            {
                DataSet ds;
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@PROCESS_ID", Process_ID);

                ds = objWrapper.ExecuteDataSet("Proc_GetProcess_Print_Document");
                objWrapper.ClearParameteres();
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while Geting printing data", ex);
            }

        }
        #endregion
        public string GetPDFAgencyCode(int CustomerID, int AppPolId, int AppPolVersionId, string strCalledFrom)
        {
            objWrapper.ClearParameteres();
            objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
            objWrapper.AddParameter("@AppPol_Id", AppPolId);
            objWrapper.AddParameter("@AppPolVersion_Id", AppPolVersionId);
            objWrapper.AddParameter("@CalledFrom", strCalledFrom);
            //objWrapper.ExecuteNonQuery("Proc_GetPDFAgencyCode");			

            DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetPDFAgencyCode");
            objWrapper.ClearParameteres();
            if (dsTemp != null && dsTemp.Tables.Count > 0 && dsTemp.Tables[0] != null && dsTemp.Tables[0].Rows.Count > 0 && dsTemp.Tables[0].Rows[0]["AGENCY_CODE"] != null && dsTemp.Tables[0].Rows[0]["AGENCY_CODE"].ToString() != "")
                return dsTemp.Tables[0].Rows[0]["AGENCY_CODE"].ToString();
            else
                return "";
        }
        public DataSet FetchPrevousProcessInfo(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
        {
            return FetchPrevousProcessInfo(objProcessInfo, "");
        }
        public DataSet FetchPrevousProcessInfo(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo, string strCalledFrom)
        {
            objWrapper.ClearParameteres();
            objWrapper.AddParameter("@CUSTOMER_ID", objProcessInfo.CUSTOMER_ID);
            objWrapper.AddParameter("@POLICY_ID", objProcessInfo.POLICY_ID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", objProcessInfo.POLICY_VERSION_ID);
            objWrapper.AddParameter("@PROCESS_ID", objProcessInfo.PROCESS_ID);
            objWrapper.AddParameter("@ROW_ID", objProcessInfo.ROW_ID);
            objWrapper.AddParameter("@CALLEDFROM", strCalledFrom);
            objWrapper.AddParameter("@LANG_ID", BlCommon.ClsCommon.BL_LANG_ID);
            DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetPreviousProcessInfo");
            //DataSet dsTemp =new DataSet();
            //string[] tableNames={"POL_POLICY_PROCESS"};
            //objWrapper.FillDataset(System.Data.CommandType.StoredProcedure,"Proc_GetPreviousProcessInfo", dsTemp,tableNames);
            objWrapper.ClearParameteres();
            return dsTemp;
        }
        public DataSet FillPrevousProcessInfo(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo, string strCalledFrom)
        {
            objWrapper.ClearParameteres();
            objWrapper.AddParameter("@CUSTOMER_ID", objProcessInfo.CUSTOMER_ID);
            objWrapper.AddParameter("@POLICY_ID", objProcessInfo.POLICY_ID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", objProcessInfo.POLICY_VERSION_ID);
            objWrapper.AddParameter("@PROCESS_ID", objProcessInfo.PROCESS_ID);
            objWrapper.AddParameter("@ROW_ID", objProcessInfo.ROW_ID);
            DataSet dsTemp = new DataSet();
            string[] tableNames = { "POL_POLICY_PROCESS" };
            objWrapper.FillDataset("Proc_FillPreviousProcessInfo", dsTemp, tableNames);
            objWrapper.ClearParameteres();
            return dsTemp;
        }
        public void UpdatedataSet(DataSet dsChangedDataSet, string strTableName)
        {
            objWrapper.UpdateDataset(dsChangedDataSet, strTableName);
        }
        // New Pdf Genration Function for Auto Lob
        public string GeneratePDF(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
        {
            bool IsAllPrintGenrated = false;
            Cms.BusinessLayer.BlProcess.ClsCommonPdf objCommonPdf = new Cms.BusinessLayer.BlProcess.ClsCommonPdf(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", objProcessInfo.STATE_CODE, objProcessInfo.LOB_ID.ToString(), objProcessInfo.PROCESS_ID.ToString(), objProcessInfo.AGENCY_CODE, objWrapper, "final");
            ArrayList alPdfName = objCommonPdf.GenratePdfForPolicyCommit();
            AdditionalIntrstPDFFileName = alPdfName[0].ToString();
            if (AdditionalIntrstPDFFileName != "")
                IsAllPrintGenrated = true;
            AgentWordingPDFFileName = alPdfName[1].ToString();
            if (AgentWordingPDFFileName != "")
                IsAllPrintGenrated = true;
            NoWordingPDFFileName = alPdfName[2].ToString();
            if (NoWordingPDFFileName != "")
                IsAllPrintGenrated = true;
            WordingPDFFileName = alPdfName[3].ToString();
            if (WordingPDFFileName != "")
                IsAllPrintGenrated = true;
            AcordPDFFileName = alPdfName[4].ToString();
            if (AcordPDFFileName != "")
                IsAllPrintGenrated = true;
            AutoIdCardPDFFileName = alPdfName[5].ToString();
            if (AutoIdCardPDFFileName != "")
                IsAllPrintGenrated = true;
            if (IsAllPrintGenrated == true)
                return "All policy document generated successfully.";
            else
                return "Error while genrating pdf.";
        }
        public string GeneratePDF(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo, string strcalledFor)
        {
            try
            {
                string agency_code = "";
                string PDFName = "";
                string strLOBString = "";
                Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
                strLOBString = objGenInfo.GetLobCodeForLobId(objProcessInfo.LOB_ID.ToString());
                objGenInfo.Dispose();

                /*if(System.Web.HttpContext.Current.Session["systemId"]!=null && System.Web.HttpContext.Current.Session["systemId"].ToString()!="")
                    agency_code = System.Web.HttpContext.Current.Session["systemId"].ToString();*/

                Cms.BusinessLayer.BlProcess.ClsCommonPdfXML objCommonPdfXml = new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML(objWrapper);

                if (strcalledFor == "ADDLINT")
                    PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, objProcessInfo.PROCESS_ID.ToString(), "ADDLINT", ref agency_code, "ADDLINT", "", "final");
                else if (strcalledFor == enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_ADDLINT.ToString())
                {
                    agency_code = "COMPANY";
                    //	PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(),objProcessInfo.POLICY_ID.ToString(),objProcessInfo.POLICY_VERSION_ID.ToString(),"POLICY","CANC_NOTICE",strLOBString,"ADDLINT",ref agency_code,"ADDLINT","ADDLINT", "final");
                    //Added By Praveen Kumar 10-09-2010
                    PDFName = objCommonPdfXml.GenerateCanellationNoticePdf(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "CANC_NOTICE", strLOBString, "ADDLINT", ref agency_code, "ADDLINT", "ADDLINT", "final");

                }
                else if (strcalledFor == enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_NONPAYDB_ADDLINT.ToString())
                {
                    agency_code = "NONPAYDB";
                    PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "CANC_NOTICE", strLOBString, "ADDLINT", ref agency_code, "ADDLINT", "ADDLINT", "final");
                }
                else if (strcalledFor == enumPROCESS_CANCELLATION_PDF_TYPES.UMB_LETTER.ToString())
                {
                    agency_code = "";
                    PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "UMB_LETTER", strLOBString, "UMB_LETTER", ref agency_code, "UMB_LETTER", "UMB_LETTER", "final");
                }
                else if (strcalledFor == enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_ADDLINT_CERTMAIL.ToString())
                {
                    agency_code = "CLIENT";
                    //PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(),objProcessInfo.POLICY_ID.ToString(),objProcessInfo.POLICY_VERSION_ID.ToString(),"POLICY","CERT_MAIL",strLOBString,"ADDLINT",ref agency_code,"ADDLINT","ADDLINT", "final");
                    //Added By Praveen Kumar
                    PDFName = objCommonPdfXml.GenerateCanellationNoticePdf(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "CERT_MAIL", strLOBString, "ADDLINT", ref agency_code, "ADDLINT", "ADDLINT", "final");
                }
                else if (strcalledFor == enumPROCESS_CANCELLATION_PDF_TYPES.REINS_NOTICE_FINAL_ADDLINT.ToString())
                {
                    agency_code = "COMPANY";
                    PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "REINS_NOTICE", strLOBString, "ADDLINT", ref agency_code, "ADDLINT", "ADDLINT", "final");
                }
                else if (strcalledFor == "NREN_NOTICE_ADDLINT")
                {
                    agency_code = "COMPANY";
                    PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "NREN_NOTICE", strLOBString, "ADDLINT", ref agency_code, "ADDLINT", "ADDLINT", "final");
                }
                else if (strcalledFor == "NREN_NOTICE_CERT_MAIL")
                {
                    agency_code = "COMPANY";
                    PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "NREN_NOTICE", strLOBString, "CERT_MAIL", ref agency_code, "CERT_MAIL", "Insured", "final");
                }
                else if (strcalledFor == enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_ADDLINT_INSREQ.ToString())
                {
                    agency_code = "INSREQ";
                    //oldcode
                    //PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(),objProcessInfo.POLICY_ID.ToString(),objProcessInfo.POLICY_VERSION_ID.ToString(),"POLICY","CANC_NOTICE",strLOBString,"ADDLINT",ref agency_code,"ADDLINT","ADDLINT", "final");
                    //Added By Praveen Kumar
                    PDFName = objCommonPdfXml.GenerateCanellationNoticePdf(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "CANC_NOTICE", strLOBString, "ADDLINT", ref agency_code, "ADDLINT", "ADDLINT", "final");
                }
                else if (strcalledFor == enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_NONPAYDBMEMO_ADDLINT.ToString())
                {
                    agency_code = "NONPAYDBMEMO";
                    PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "CANC_NOTICE", strLOBString, "ADDLINT", ref agency_code, "ADDLINT", "ADDLINT", "final");
                }
                else if (strcalledFor == "DECPAGE")
                {
                    if (objProcessInfo.INSURED == 1 && objProcessInfo.AGENCY_PRINT == 0)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", strcalledFor, strLOBString, objProcessInfo.PROCESS_ID.ToString(), "", ref agency_code, "", PRINT_JOBS_ENTITY_TYPE_CUSTOMER, "final");
                    else if (objProcessInfo.INSURED == 0 && objProcessInfo.AGENCY_PRINT == 1)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", strcalledFor, strLOBString, objProcessInfo.PROCESS_ID.ToString(), "", ref agency_code, "", PRINT_JOBS_ENTITY_TYPE_AGENCY, "final");
                }
                else if (strcalledFor == enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL.ToString())
                {
                    agency_code = "COMPANY";
                    if (objProcessInfo.INSURED == 1 && objProcessInfo.AGENCY_PRINT == 0)
                        //PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(),objProcessInfo.POLICY_ID.ToString(),objProcessInfo.POLICY_VERSION_ID.ToString(),"POLICY","DECPAGE",strLOBString,enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(),ref agency_code,enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(),"Insured", "final");
                        //Added By Praveen Kumar
                        PDFName = objCommonPdfXml.GenerateCanellationNoticePdf(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Insured", "final");
                    else if (objProcessInfo.INSURED == 0 && objProcessInfo.AGENCY_PRINT == 1)
                        //PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(),objProcessInfo.POLICY_ID.ToString(),objProcessInfo.POLICY_VERSION_ID.ToString(),"POLICY","DECPAGE",strLOBString,enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(),ref agency_code,enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(),"Agent", "final");
                        //Added By Praveen Kumar
                        PDFName = objCommonPdfXml.GenerateCanellationNoticePdf(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Agent", "final");
                }
                else if (strcalledFor == enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_AGENT.ToString())
                {
                    agency_code = "AGENTS";
                    if (objProcessInfo.INSURED == 1 && objProcessInfo.AGENCY_PRINT == 0)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Insured", "final");
                    else if (objProcessInfo.INSURED == 0 && objProcessInfo.AGENCY_PRINT == 1)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Agent", "final");
                }
                else if (strcalledFor == enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_NSF.ToString())
                {
                    agency_code = "NSF";
                    if (objProcessInfo.INSURED == 1 && objProcessInfo.AGENCY_PRINT == 0)
                        //PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(),objProcessInfo.POLICY_ID.ToString(),objProcessInfo.POLICY_VERSION_ID.ToString(),"POLICY","DECPAGE",strLOBString,enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(),ref agency_code,enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(),"Insured", "final");
                        //Added By Praveen Kumar 13-09-2010
                        PDFName = objCommonPdfXml.GenerateCanellationNoticePdf(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Insured", "final");

                    else if (objProcessInfo.INSURED == 0 && objProcessInfo.AGENCY_PRINT == 1)
                        //PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(),objProcessInfo.POLICY_ID.ToString(),objProcessInfo.POLICY_VERSION_ID.ToString(),"POLICY","DECPAGE",strLOBString,enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(),ref agency_code,enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(),"Agent", "final");
                        //Added By Praveen Kumar 13-09-2010
                        PDFName = objCommonPdfXml.GenerateCanellationNoticePdf(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Agent", "final");

                }
                else if (strcalledFor == enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_NSF_NOREPLACE.ToString())
                {
                    agency_code = "NSF_NOREPLACE";
                    if (objProcessInfo.INSURED == 1 && objProcessInfo.AGENCY_PRINT == 0)
                        //PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(),objProcessInfo.POLICY_ID.ToString(),objProcessInfo.POLICY_VERSION_ID.ToString(),"POLICY","DECPAGE",strLOBString,enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(),ref agency_code,enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(),"Insured", "final");
                        //Added By Praveen Kumar 13-09-2010
                        PDFName = objCommonPdfXml.GenerateCanellationNoticePdf(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Insured", "final");

                    else if (objProcessInfo.INSURED == 0 && objProcessInfo.AGENCY_PRINT == 1)
                        //PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(),objProcessInfo.POLICY_ID.ToString(),objProcessInfo.POLICY_VERSION_ID.ToString(),"POLICY","DECPAGE",strLOBString,enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(),ref agency_code,enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(),"Agent", "final");
                        //Added By Praveen Kumar 10-09-2010
                        PDFName = objCommonPdfXml.GenerateCanellationNoticePdf(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Agent", "final");

                }
                else if (strcalledFor == enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_NONPAYDBMEMO.ToString())
                {
                    agency_code = "NONPAYDBMEMO";
                    if (objProcessInfo.INSURED == 1 && objProcessInfo.AGENCY_PRINT == 0)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Insured", "final");
                    else if (objProcessInfo.INSURED == 0 && objProcessInfo.AGENCY_PRINT == 1)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Agent", "final");
                }
                else if (strcalledFor == enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_NONPAY.ToString())
                {
                    agency_code = "NONPAY";
                    if (objProcessInfo.INSURED == 1 && objProcessInfo.AGENCY_PRINT == 0)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Insured", "final");
                    else if (objProcessInfo.INSURED == 0 && objProcessInfo.AGENCY_PRINT == 1)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Agent", "final");
                }
                else if (strcalledFor == enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_NONPAYDB.ToString())
                {
                    agency_code = "NONPAYDB";
                    if (objProcessInfo.INSURED == 1 && objProcessInfo.AGENCY_PRINT == 0)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Insured", "final");
                    else if (objProcessInfo.INSURED == 0 && objProcessInfo.AGENCY_PRINT == 1)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Agent", "final");
                }
                else if (strcalledFor == enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_INSREQ.ToString())
                {
                    agency_code = "INSREQ";
                    if (objProcessInfo.INSURED == 1 && objProcessInfo.AGENCY_PRINT == 0)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Insured", "final");
                    else if (objProcessInfo.INSURED == 0 && objProcessInfo.AGENCY_PRINT == 1)
                        //PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(),objProcessInfo.POLICY_ID.ToString(),objProcessInfo.POLICY_VERSION_ID.ToString(),"POLICY","DECPAGE",strLOBString,enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(),ref agency_code,enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(),"Agent", "final");
                        //Added By Praveen Kumar 10-09-2010
                        PDFName = objCommonPdfXml.GenerateCanellationNoticePdf(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Agent", "final");
                }
                else if (strcalledFor == enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_CERTMAIL.ToString())
                {
                    agency_code = "COMPANY";
                    //PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(),objProcessInfo.POLICY_ID.ToString(),objProcessInfo.POLICY_VERSION_ID.ToString(),"POLICY","CANC_NOTICE",strLOBString,"CERT_MAIL",ref agency_code,"CERT_MAIL","Client", "final");
                    //Added By Praveen Kumar 10-09-2010
                    PDFName = objCommonPdfXml.GenerateCanellationNoticePdf(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "CANC_NOTICE", strLOBString, "CERT_MAIL", ref agency_code, "CERT_MAIL", "Client", "final");

                }
                else if (strcalledFor == enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_TEMP.ToString())
                {
                    agency_code = "";
                    if (objProcessInfo.INSURED == 1 && objProcessInfo.AGENCY_PRINT == 0)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Insured", "temp");
                    else if (objProcessInfo.INSURED == 0 && objProcessInfo.AGENCY_PRINT == 1)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Agent", "temp");
                }
                else if (strcalledFor == enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_TEMP_NSF.ToString())
                {
                    agency_code = "NSF";
                    if (objProcessInfo.INSURED == 1 && objProcessInfo.AGENCY_PRINT == 0)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Insured", "temp");
                    else if (objProcessInfo.INSURED == 0 && objProcessInfo.AGENCY_PRINT == 1)
                        //PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(),objProcessInfo.POLICY_ID.ToString(),objProcessInfo.POLICY_VERSION_ID.ToString(),"POLICY","DECPAGE",strLOBString,enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(),ref agency_code,enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(),"Agent", "temp");
                        //Added By Praveen Kumar 10-09-2010
                        PDFName = objCommonPdfXml.GenerateCanellationNoticePdf(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Agent", "temp");

                }
                else if (strcalledFor == enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_TEMP_NONPAY.ToString())
                {
                    agency_code = "NONPAY";
                    if (objProcessInfo.INSURED == 1 && objProcessInfo.AGENCY_PRINT == 0)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Insured", "temp");
                    else if (objProcessInfo.INSURED == 0 && objProcessInfo.AGENCY_PRINT == 1)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Agent", "temp");
                }
                else if (strcalledFor == enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_TEMP_NONPAYDB.ToString())
                {
                    agency_code = "NONPAYDB";
                    if (objProcessInfo.INSURED == 1 && objProcessInfo.AGENCY_PRINT == 0)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Insured", "temp");
                    else if (objProcessInfo.INSURED == 0 && objProcessInfo.AGENCY_PRINT == 1)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Agent", "temp");
                }
                else if (strcalledFor == "NREN_NOTICE")
                {
                    if (objProcessInfo.INSURED == 1 && objProcessInfo.AGENCY_PRINT == 0)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, "NREN_NOTICE", ref agency_code, "NREN_NOTICE", "Insured", "final");
                    else if (objProcessInfo.INSURED == 0 && objProcessInfo.AGENCY_PRINT == 1)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, "NREN_NOTICE", ref agency_code, "NREN_NOTICE", "Agent", "final");
                }
                else if (strcalledFor == "NREN_NOTICE_NOTIFICATION")
                {
                    agency_code = "NREN_NOTICE_NOTIFICATION";
                    if (objProcessInfo.INSURED == 1 && objProcessInfo.AGENCY_PRINT == 0)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, "NREN_NOTICE", ref agency_code, "NREN_NOTICE", "Insured", "final");
                    else if (objProcessInfo.INSURED == 0 && objProcessInfo.AGENCY_PRINT == 1)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, "NREN_NOTICE", ref agency_code, "NREN_NOTICE", "Agent", "final");
                }
                else if (strcalledFor == "NREN_NOTICE_NO_NOTIFICATION")
                {
                    agency_code = "NREN_NOTICE_NO_NOTIFICATION";
                    if (objProcessInfo.INSURED == 1 && objProcessInfo.AGENCY_PRINT == 0)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, "NREN_NOTICE", ref agency_code, "NREN_NOTICE", "Insured", "final");
                    else if (objProcessInfo.INSURED == 0 && objProcessInfo.AGENCY_PRINT == 1)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, "NREN_NOTICE", ref agency_code, "NREN_NOTICE", "Agent", "final");
                }
                else if (strcalledFor == "REINS_NOTICE")
                {
                    if (objProcessInfo.INSURED == 1 && objProcessInfo.AGENCY_PRINT == 0)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, "REINS_NOTICE", ref agency_code, "REINS_NOTICE", "Insured", "final");
                    else if (objProcessInfo.INSURED == 0 && objProcessInfo.AGENCY_PRINT == 1)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, "REINS_NOTICE", ref agency_code, "REINS_NOTICE", "Agent", "final");
                }
                else if (strcalledFor == BlCommon.ClsCommon.DOCUMENT_TYPE_AUTO_ID_CARD)
                {
                    PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, BlCommon.ClsCommon.DOCUMENT_TYPE_AUTO_ID_CARD, ref agency_code, BlCommon.ClsCommon.DOCUMENT_TYPE_AUTO_ID_CARD, "", "final");
                }
                else if (strcalledFor == enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_TEMP_INSREQ.ToString())
                {
                    agency_code = "INSREQ";
                    //					if(objProcessInfo.INSURED == 1 && objProcessInfo.AGENCY_PRINT == 0)
                    //						PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(),objProcessInfo.POLICY_ID.ToString(),objProcessInfo.POLICY_VERSION_ID.ToString(),"POLICY","DECPAGE",strLOBString,enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(),ref agency_code,enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(),"Insured", "temp");
                    if (objProcessInfo.INSURED == 1 && objProcessInfo.AGENCY_PRINT == 0)
                        PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", "DECPAGE", strLOBString, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), ref agency_code, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE.ToString(), "Agent", "temp");
                }
                else
                    PDFName = objCommonPdfXml.GeneratePdfProxy(objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.POLICY_VERSION_ID.ToString(), "POLICY", strcalledFor, strLOBString, "", ref agency_code, "", "", "final");

                return PDFName;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public string GetCancellationCode(string strLobCode, string strStateCode, string strInsAgn, string strCancelCode)
        {
            objWrapper.ClearParameteres();
            objWrapper.AddParameter("@LOB_CODE", strLobCode);
            objWrapper.AddParameter("@STATE_CODE", strStateCode);
            objWrapper.AddParameter("@INS_AGN", strInsAgn);
            objWrapper.AddParameter("@CANC_CODE", strCancelCode);

            DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetPDFCancellationCode");
            objWrapper.ClearParameteres();
            if (dsTemp != null && dsTemp.Tables.Count > 0 && dsTemp.Tables[0] != null && dsTemp.Tables[0].Rows.Count > 0 && dsTemp.Tables[0].Rows[0]["DOCUMENT_CODE"] != null && dsTemp.Tables[0].Rows[0]["DOCUMENT_CODE"].ToString() != "")
                return dsTemp.Tables[0].Rows[0]["DOCUMENT_CODE"].ToString();
            else
                return "";

        }
        public Cms.Model.Policy.Process.ClsPrintJobsInfo GetPrintJobsValues(ClsProcessInfo objProcessInfo)
        {
            return GetPrintJobsValues(objProcessInfo, "");
        }
        public Cms.Model.Policy.Process.ClsPrintJobsInfo GetPrintJobsValues(ClsProcessInfo objProcessInfo, string strCalledFor)
        {
            string AgencyCode = GetPDFAgencyCode(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, "POLICY");
            Cms.Model.Policy.Process.ClsPrintJobsInfo objPrintJobsInfo = new ClsPrintJobsInfo();
            objPrintJobsInfo.CUSTOMER_ID = objProcessInfo.CUSTOMER_ID;
            objPrintJobsInfo.POLICY_ID = objProcessInfo.POLICY_ID;
            if (strCalledFor == "NEWVERSION")
                objPrintJobsInfo.POLICY_VERSION_ID = objProcessInfo.NEW_POLICY_VERSION_ID;
            else
                objPrintJobsInfo.POLICY_VERSION_ID = objProcessInfo.POLICY_VERSION_ID;
            objPrintJobsInfo.PROCESS_ID = objProcessInfo.PROCESS_ID;
            objPrintJobsInfo.PROCESS_ROW_ID = objProcessInfo.ROW_ID;
            objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
            objPrintJobsInfo.PRINT_DATETIME = System.DateTime.Now;
            objPrintJobsInfo.PRINTED_DATETIME = System.DateTime.Now;
            //if(System.Web.HttpContext.Current.Session["systemId"]!=null && System.Web.HttpContext.Current.Session["systemId"].ToString()!="")
            //objPrintJobsInfo.URL_PATH = ClsPolicyProcess.PRINT_JOBS_URL_PATH + AgencyCode + "/" + objProcessInfo.CUSTOMER_ID.ToString() + "/" + "POLICY" + "/" + "final";
            //else
            //	objPrintJobsInfo.URL_PATH = ClsPolicyProcess.PRINT_JOBS_URL_PATH;

            //Ravindra(03-28-2008): For cancellation notice
            if (strCalledFor == "CANC_NOTICE")
            {
                //objPrintJobsInfo.URL_PATH = ClsPolicyProcess.PRINT_JOBS_URL_PATH + AgencyCode + "/" + objProcessInfo.CUSTOMER_ID.ToString() + "/" + "POLICY/CANC_NOTICE" + "/" + "final";
                //Added by Praveen Kumar on 10/09/2010
                objPrintJobsInfo.URL_PATH = ClsPolicyProcess.PRINT_JOBS_URL_PATH + "/" + CarrierSystemID + "/" + AgencyCode + "/" + objProcessInfo.CUSTOMER_ID.ToString() + "/" + "POLICY/CANC_NOTICE" + "/" + "final";
            }
            else if (strCalledFor == "REINS_NOTICE")
            {
                objPrintJobsInfo.URL_PATH = ClsPolicyProcess.PRINT_JOBS_URL_PATH + AgencyCode + "/" + objProcessInfo.CUSTOMER_ID.ToString() + "/" + "POLICY/REINS_NOTICE" + "/" + "final";
            }
            else
            {
                objPrintJobsInfo.URL_PATH = ClsPolicyProcess.PRINT_JOBS_URL_PATH + AgencyCode + "/" + objProcessInfo.CUSTOMER_ID.ToString() + "/" + "POLICY" + "/" + "final";
            }

            objPrintJobsInfo.ONDEMAND_FLAG = ClsPolicyProcess.PRINT_JOBS_ON_DEMAND_FLAG;
            objPrintJobsInfo.CREATED_DATETIME = System.DateTime.Now;
            objPrintJobsInfo.CREATED_BY = objProcessInfo.CREATED_BY;
            return objPrintJobsInfo;
        }

        /// <summary>
        /// Finds the index at which the file name is found
        /// </summary>
        /// <param name="strFileName">Concatenated string of file names returned by GeneratePdf</param>
        /// <param name="strAddlInt">Addl Into file info to be searched for</param>
        /// <returns>Returns name of the pdf file if found, else returns blank</returns>
        public string FindAddIntFileName(string strFileName, string strAddlInt)
        {
            string[] FileNameArr = strFileName.Split('~');
            if (FileNameArr == null || FileNameArr.Length < 1)
                return "";

            for (int iCounter = 0; iCounter < FileNameArr.Length; iCounter++)
            {
                if (FileNameArr[iCounter].ToString().IndexOf(strAddlInt) != -1)
                    return FileNameArr[iCounter].ToString();
            }
            return "";
        }


        //To be reimplemented: no need to fetch all data for merely checking add int
        public bool IsAddIntExist(int CustomerID, int PolicyID, int PolicyVersionID, string LOBID, string CalledFor, DataWrapper objDataWrapper)
        {
            BusinessLayer.BlApplication.clsapplication objApp = new clsapplication();
            DataSet addintds = objApp.FetchAddIntDetails(CustomerID.ToString(), PolicyID.ToString(), PolicyVersionID.ToString(), LOBID, CalledFor, objDataWrapper);
            if (addintds != null && addintds.Tables.Count > 0 && addintds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        public bool IsAddIntExist(int CustomerID, int PolicyID, int PolicyVersionID, string LOBID, string CalledFor)
        {
            BusinessLayer.BlApplication.clsapplication objApp = new clsapplication();
            DataSet addintds = objApp.FetchAddIntDetails(CustomerID.ToString(), PolicyID.ToString(), PolicyVersionID.ToString(), LOBID, CalledFor);
            if (addintds != null && addintds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// App version will be marked for deletion.
        /// </summary>
        /// <param name="CUSTOMER_ID"></param>
        /// <param name="POLICY_ID"></param>
        /// <param name="POLICY_VERSION_ID"></param>
        public void MarkforDeletion(int CUSTOMER_ID, int POL_ID, int POL_VERSION_ID, string deleteFlag)
        {
            try
            {
                objWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objWrapper.AddParameter("@POL_ID", POL_ID);
                objWrapper.AddParameter("@POL_VERSION_ID", POL_VERSION_ID);
                objWrapper.AddParameter("@DELETION_FLAG", deleteFlag);
                int returnResult = objWrapper.ExecuteNonQuery("Proc_MarkAppVersion_Deletion");
                objWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #region EOD DIARY ENTRY

        /// <summary>
        /// This function is used to read values of day and listtype attribute 
        /// </summary>
        /// <param name="alList">List type array list which contains list type attribute values</param>
        /// <returns>Day array list which contains day attribute values</returns>
        public ArrayList ReadXML(ref ArrayList alList, string tempXML)
        {
            //="<PendingFollowUP><FOLLOWUP ID=\"1\" DAYS=\"1\" LISTTYPE=\"28\"/><FOLLOWUP ID=\"2\" DAYS=\"5\" LISTTYPE=\"29\"/><FOLLOWUP ID=\"3\" DAYS=\"9\" LISTTYPE=\"30\"/></PendingFollowUP>";
            XmlDocument xDoc = new XmlDocument();
            ArrayList alDays = new ArrayList();

            try
            {
                xDoc.LoadXml(tempXML);

                if (xDoc != null)
                {
                    XmlNodeList xNdList = xDoc.SelectNodes("PendingFollowUP/FOLLOWUP");

                    if (xNdList != null && xNdList.Count > 0)
                        foreach (XmlNode xNode in xNdList)
                        {
                            string strDay = xNode.Attributes["DAYS"].Value == null ? "" : xNode.Attributes["DAYS"].Value.ToString();
                            string strListType = xNode.Attributes["LISTTYPE"].Value == null ? "" : xNode.Attributes["LISTTYPE"].Value.ToString();

                            if (strListType != "")
                                alList.Add(strListType);

                            if (strDay != "")
                                alDays.Add(strDay);

                        }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return alDays;
        }


        /// <summary>
        /// This is a function for making diary entry for EOD process 
        /// </summary>
        /// 

        public void MakeEntryforEOD(string strConfigXML)
        {
            MakeEntryforEOD(strConfigXML, 0);
        }
        public void MakeEntryforEOD(string strConfigXML, int Created_by)
        {
            DataSet dsPendingProcess = new DataSet();
            int iListCnt = 0;
            ArrayList alListType = new ArrayList();
            ArrayList alDays = new ArrayList();
            ClsDiary objdiary = new ClsDiary();

            alDays = ReadXML(ref alListType, strConfigXML);

            //fetching all pending process from POL_POLICY_PROCESS table whose status is pending and
            //the difference between today's date and their launch date is less than minmum follow up value
            dsPendingProcess = DiaryEntryEODPendingProcess();

            for (; iListCnt < alDays.Count; iListCnt++)
            {
                int intNextFollow = 0, intFollow = 0;
                int intList = 0;

                intFollow = alDays[iListCnt] == null ? 0 : int.Parse(alDays[iListCnt].ToString());

                //picking next follow up days to make a range for e.g. >10 and <20
                //if array has not reached to last index
                if ((iListCnt + 1) < alDays.Count)
                    intNextFollow = alDays[iListCnt + 1] == null ? 0 : int.Parse(alDays[iListCnt + 1].ToString());

                intList = alListType[iListCnt] == null ? 0 : int.Parse(alListType[iListCnt].ToString());

                if (dsPendingProcess != null && dsPendingProcess.Tables.Count > 0)
                {
                    string strCond = "";
                    if ((iListCnt + 1) < alDays.Count)
                        strCond = "(COUNT_DAYS>" + intFollow + " and COUNT_DAYS<=" + intNextFollow + ") and LISTTYPEID<>" + intList;
                    else
                        strCond = "COUNT_DAYS>=" + intFollow + " and LISTTYPEID<>" + intList;

                    DataRow[] dRow = dsPendingProcess.Tables[0].Select(strCond);

                    if (dRow.Length > 0)
                    {
                        foreach (DataRow row in dRow)
                        {

                            bool isEntered = false;
                            isEntered = CheckEntry(dsPendingProcess, row, intList);

                            if (isEntered)
                            {
                                TodolistInfo objTodo = new TodolistInfo();

                                objTodo.CUSTOMER_ID = row["CUSTOMER_ID"].ToString() == "" ? 0 : int.Parse(row["CUSTOMER_ID"].ToString());
                                objTodo.POLICY_ID = row["POLICY_ID"].ToString() == "" ? 0 : int.Parse(row["POLICY_ID"].ToString());
                                objTodo.POLICY_VERSION_ID = row["POLICY_VERSION_ID"].ToString() == "" ? 0 : int.Parse(row["POLICY_VERSION_ID"].ToString());
                                objTodo.LOB_ID = row["POLICY_LOB"].ToString() == "" ? 0 : int.Parse(row["POLICY_LOB"].ToString());
                                objTodo.LISTOPEN = "Y";
                                objTodo.LISTTYPEID = intList;
                                objTodo.PROCESS_ROW_ID = row["PROCESS_ROW_ID"].ToString() == "" ? 0 : int.Parse(row["PROCESS_ROW_ID"].ToString());
                                objTodo.CREATED_DATETIME = DateTime.Now;
                                objTodo.RECDATE = DateTime.Now;
                                objTodo.MODULE_ID = (int)ClsDiary.enumModuleMaster.POLICY_PROCESS;
                                objTodo.FOLLOWUPDATE = DateTime.Now;
                                objTodo.CREATED_BY = Created_by;

                                objdiary.DiaryEntryfromSetup(objTodo);
                            }
                        }
                    }
                }//end of null check of dataset
            }//end of count check of array list							
        }


        /// <summary>
        /// This function is made to check whether the list type entry for the policy is already entered or not
        /// for the given policy details
        /// </summary>
        /// <param name="dsPending">Data set contaning all pending process from the current date</param>
        /// <param name="dRow">row containing process data which is going to make entry for followup</param>
        /// <param name="listTypeToEnter">follow up which is goint to be enetered</param>
        /// <returns>whether entry is being made or not</returns>
        public bool CheckEntry(DataSet dsPending, DataRow dRow, int listTypeToEnter)
        {
            bool flagCheck = false;

            if ((dsPending != null) && (dsPending.Tables.Count > 0))
            {
                if (dRow != null)
                {
                    int cID = dRow["CUSTOMER_ID"] == null ? 0 : int.Parse(dRow["CUSTOMER_ID"].ToString());
                    int pID = dRow["POLICY_ID"] == null ? 0 : int.Parse(dRow["POLICY_ID"].ToString());
                    int pvID = dRow["POLICY_VERSION_ID"] == null ? 0 : int.Parse(dRow["POLICY_VERSION_ID"].ToString());
                    int prID = dRow["PROCESS_ROW_ID"] == null ? 0 : int.Parse(dRow["PROCESS_ROW_ID"].ToString());

                    string exp = "customer_id=" + cID + " and policy_id=" + pID + " and policy_version_id=" + pvID + " and process_row_id=" + prID + " and listtypeid=" + listTypeToEnter;

                    DataRow[] row = dsPending.Tables[0].Select(exp);
                    if (row != null && row.Length > 0)
                        flagCheck = false;
                    else
                        flagCheck = true;
                }
            }
            return flagCheck;
        }


        /// <summary>
        /// Fetching data of the process which are pending 
        /// </summary>
        /// <returns>Dataset containing rows</returns>
        public DataSet DiaryEntryEODPendingProcess()
        {
            SqlParameter[] sparam = new SqlParameter[1];
            DataSet dsTemp = new DataSet();
            try
            {
                sparam[0] = new SqlParameter("@Day", SqlDbType.Int);
                sparam[0].Value = (int)FOLLOWUP.DAY_FOLLOWUP_1;


                dsTemp = DataWrapper.ExecuteDataset(ConnStr, "Proc_GetPendingProcessforEOD", sparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTemp;

        }


        public enum FOLLOWUP
        {
            DAY_FOLLOWUP_1 = 1,
            DAY_FOLLOWUP_6 = 6,
            DAY_FOLLOWUP_7 = 7
        }


        #endregion

        String _DocumentCode;
        public String DocumentName
        {
            get
            {
                return _DocumentCode;
            }
            set
            {
                _DocumentCode = value;

            }
        }

        #region Insert Reinsurance Premium
        /// <summary>
        /// Insert Policy Re Insurance% Break Down Premium
        /// </summary>
        /// <param name="CUSTOMER_ID"></param>
        /// <param name="POLICY_ID"></param>
        /// <param name="POLICY_VERSION_ID"></param>
        /// <param name="PROCESS_ID"></param>
        /// <param name="CREATED_BY"></param>
        public int Insert_ReInsurance_Premium(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, int PROCESS_ID, int CREATED_BY)
        {
            int retval = 0;
            string Insert_Proc = "Proc_InsertPOL_REINSURANCE_BREAKDOWN_DETAILS";
            objWrapper.ClearParameteres();
            objWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
            objWrapper.AddParameter("@POLICY_ID", POLICY_ID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
            objWrapper.AddParameter("@CREATED_BY", CREATED_BY);
            objWrapper.AddParameter("@PROCESS_ID", PROCESS_ID);
            retval = objWrapper.ExecuteNonQuery(Insert_Proc);
            return retval;
        }
        #endregion

        #region Insert COI Premium
        /// <summary>
        /// Insert Policy Co Insurance% Break Down Premium
        /// </summary>
        /// <param name="CUSTOMER_ID"></param>
        /// <param name="POLICY_ID"></param>
        /// <param name="POLICY_VERSION_ID"></param>
        /// <param name="CREATED_BY"></param>
        public int Insert_COInsurance_Premium(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, int CREATED_BY)
        {
            int retval = 0;
            string Insert_Proc = "Proc_InsertPOL_CO_INSURANCE_BREAKDOWN_DETAILS";
            objWrapper.ClearParameteres();
            objWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
            objWrapper.AddParameter("@POLICY_ID", POLICY_ID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
            objWrapper.AddParameter("@CREATED_BY", CREATED_BY);
            objWrapper.AddParameter("@CREATED_DATETIME", DateTime.Now);
            retval = objWrapper.ExecuteNonQuery(Insert_Proc);
            return retval;
        }
        #endregion

        #region Post Premium Open Items
        public int PostOpenItems(int CustomerId, int PolicyId, int PolicyVersionId, int ProcessType, int UserID, out string TransDescription)
        {
            return PostOpenItems( CustomerId,  PolicyId,  PolicyVersionId,  ProcessType,  UserID, out  TransDescription,false);
        }
        public int PostOpenItems(int CustomerId, int PolicyId, int PolicyVersionId, int ProcessType, int UserID, out string TransDescription,bool ENDORSEMENT_REISSUE)
        {
            int PremiumProcessTableRowId = 0;
            int PRODUCT_TYPE = 0;
            int CO_INSURANCE = 0;
            string strIsSamePolicy = "";
            string strPolicyCurrencyId;
            string strIsRewritePolicy = "";
            string strPremiumTerm = "";
            int retVal = 0;
            TransDescription = "";
            int AppId;
            int AppVersionId;
            int LobId;
            try
            {

                Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
                DataSet dsPolicyVersion = objGenInfo.GetPolicyDetails(CustomerId, 0, 0, PolicyId, PolicyVersionId);

                if (dsPolicyVersion.Tables[0].Rows.Count > 0)
                {
                    if (dsPolicyVersion.Tables[0].Rows[0]["IS_REWRITE_POLICY"] != null)
                        strIsRewritePolicy = dsPolicyVersion.Tables[0].Rows[0]["IS_REWRITE_POLICY"].ToString();
                }
                dsPolicyVersion.Dispose();
                objGenInfo.Dispose();
                if (strIsRewritePolicy == "Y")
                    strPremiumTerm = "O";
                GetAppLobDetails(CustomerId, PolicyId, PolicyVersionId, out AppId, out AppVersionId, out LobId, out strPolicyCurrencyId, out PRODUCT_TYPE, out CO_INSURANCE);


                ArrayList arrPremium = GetPremiumDetailsFromBillingInfo(CustomerId, PolicyId,
                               PolicyVersionId, strPremiumTerm,
                               ProcessType, out PremiumProcessTableRowId, PRODUCT_TYPE, CO_INSURANCE);

                double dblTotalGross = 0, dblTotalNet = 0, dblTotalMCCA = 0, dblTotalOSTA = 0,
                    dblTotalFees = 0;
                for (int i = 0; i < arrPremium.Count; i++)
                {
                    Premium objPremium = (Premium)arrPremium[i];
                    dblTotalNet += objPremium.NetPremium;
                    dblTotalMCCA += objPremium.MCCAFees;
                    dblTotalOSTA += objPremium.OtherFees;
                    dblTotalGross += objPremium.GrossPremium;
                  
                }
                if ((PRODUCT_TYPE == enumPOLICY_TRANSACTION_TYPE.OPEN_POLICY || CO_INSURANCE == enumCO_INSURANCE.FOLLOWER) && dblTotalNet == 0 && ENDORSEMENT_REISSUE !=true  )
                    return 1;

                objWrapper.ClearParameteres();

                objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                objWrapper.AddParameter("@APP_ID", PolicyId);
                objWrapper.AddParameter("@APP_VERSION_ID", PolicyVersionId);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                objWrapper.AddParameter("@PREMIUM_AMOUNT", dblTotalNet);
                objWrapper.AddParameter("@MCCA_FEES", dblTotalMCCA);
                objWrapper.AddParameter("@OTHER_FEES", dblTotalOSTA);
                SqlParameter objParam1 = (SqlParameter)objWrapper.AddParameter
                    ("@TRANS_DESC", null, SqlDbType.VarChar, ParameterDirection.Output, 255);

                objWrapper.AddParameter("@PARAM2", PremiumProcessTableRowId);
                objWrapper.AddParameter("@PARAM3", UserID);
                objWrapper.AddParameter("@PARAM4", null);
                objWrapper.AddParameter("@RISK_ID", 1);
                objWrapper.AddParameter("@RISK_TYPE", "PRODUCT");
                objWrapper.AddParameter("@COMMISSION_CLASS", 0);
                objWrapper.AddParameter("@IS_SAME_POLICY", strIsSamePolicy);

                SqlParameter objParam = (SqlParameter)objWrapper.AddParameter
                    ("@RetVal", null, SqlDbType.Int, ParameterDirection.Output);

                SqlParameter objFees = (SqlParameter)objWrapper.AddParameter
                    ("@TOTAL_FEES", null, SqlDbType.Decimal, ParameterDirection.Output);

                objWrapper.ExecuteNonQuery("Proc_InsertProductPremiumPolicyOpenItems");

                if (strIsSamePolicy != "Y")
                {
                    TransDescription = TransDescription + objParam1.Value.ToString();
                }
                dblTotalFees += Convert.ToDouble(objFees.Value);
                retVal = Convert.ToInt32(objParam.Value);
                objWrapper.ClearParameteres();
                strIsSamePolicy = "Y";

                //TransDescription = TransDescription + " Total premium amount invoiced $" + dblTotalGross.ToString(); {0:,#,###}
                TransDescription = TransDescription + ClsCommon.FetchGeneralMessage("1117", " " + FormatNumber(dblTotalGross.ToString())) + ";";

                if (dblTotalFees != 0)
                    //TransDescription = TransDescription + " And the Total Installment fee $" + dblTotalFees.ToString();
                    TransDescription = TransDescription + ClsCommon.FetchGeneralMessage("1118", " " + FormatNumber(dblTotalFees.ToString())) + ";";

                //Insert info in Balance Tab
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                objWrapper.AddParameter("@SOURCE_ROW_ID", PremiumProcessTableRowId);
                objWrapper.ExecuteNonQuery("Proc_InsertBalanceInformation");
                objWrapper.ClearParameteres();
                return retVal;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        //update policy Display Version
        public int UpdatePolicyDisplayVersion(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, string DisplayVersions)
        {
            int retval = 0;
            try
            {
                objWrapper.ClearParameteres();
                string Insert_Proc = "Proc_UpdatePolicyDisplayVersion";
                objWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objWrapper.AddParameter("@POLICY_ID", POLICY_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                objWrapper.AddParameter("@POLICY_DISP_VERSION", DisplayVersions);
                retval = objWrapper.ExecuteNonQuery(Insert_Proc);
                objWrapper.ClearParameteres();
                return retval;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet GetPolicyEndorsementVersions(int CustomerId, int PolicyId, int PolicyVersionId, DateTime Effective_date, string CalledFrom)
        {

            DataSet DsEnd = null;
            objWrapper.ClearParameteres();
            objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
            objWrapper.AddParameter("@POLICY_ID", PolicyId);
            objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
            objWrapper.AddParameter("@END_EFFE_DATE", Effective_date);
            objWrapper.AddParameter("@CALLED_FROM", CalledFrom);
            DsEnd = objWrapper.ExecuteDataSet("Proc_GetPolicyEndorsementVersions");
            objWrapper.ClearParameteres();
            return DsEnd;
        }

        public int UpdatePolicyStatus(int CustomerID, int PolicyID, int PolicyVersionID, string PolicyStatus)
        {

            string strStoredProc = "Proc_UpdatePolicyStatus";

            try
            {

                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objWrapper.AddParameter("@POLICY_STATUS", PolicyStatus);

                SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@RESULT", SqlDbType.Int, ParameterDirection.Output);

                int returnResult = 0;
                returnResult = objWrapper.ExecuteNonQuery(strStoredProc);
                objWrapper.ClearParameteres();

                if (objSqlParameter.Value.ToString() != "")
                    returnResult = int.Parse(objSqlParameter.Value.ToString());

                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }
        public ClsEndorsementDetailInfo PopulateEndorsementDetailInfo(ClsProcessInfo objProcessInfo)
        {
           // int i = new int();

            ClsEndorsementDetailInfo objInfo = new ClsEndorsementDetailInfo();
            objInfo.POLICY_ID = objProcessInfo.POLICY_ID;
            //			objInfo.POLICY_VERSION_ID = objProcessInfo.POLICY_VERSION_ID; 
            // Changed by Swarup(07-feb-2008) as it is taking old version id when it should take new version id
            objInfo.POLICY_VERSION_ID = objProcessInfo.NEW_POLICY_VERSION_ID;
            objInfo.CUSTOMER_ID = objProcessInfo.CUSTOMER_ID;
            objInfo.CREATED_BY = objProcessInfo.CREATED_BY;
            objInfo.CREATED_DATETIME = objProcessInfo.CREATED_DATETIME;
            objInfo.ENDORSEMENT_DATE = objProcessInfo.CREATED_DATETIME;
            objInfo.ENDORSEMENT_NO = objProcessInfo.ENDORSEMENT_NO;
            objInfo.REMARKS = objProcessInfo.COMMENTS;
            return objInfo;
        }
        public int AddEndorsementLog(ClsEndorsementDetailInfo objEndorsementDetailInfo, out int EndorsementNO)
        {
            return AddEndorsementLog(objEndorsementDetailInfo, out EndorsementNO, "");
        }

        public int AddEndorsementLog(ClsEndorsementDetailInfo objEndorsementDetailInfo, out int EndorsementNO, string ProcessType)
        {
            string strStoredProc = "Proc_InsertPOL_POLICY_ENDORSEMENTS";
            DateTime RecordDate = DateTime.Now;
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@POLICY_ID", objEndorsementDetailInfo.POLICY_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", objEndorsementDetailInfo.POLICY_VERSION_ID);
                objWrapper.AddParameter("@CUSTOMER_ID", objEndorsementDetailInfo.CUSTOMER_ID);
                SqlParameter objParam = (SqlParameter)objWrapper.AddParameter("@ENDORSEMENT_NO", SqlDbType.Int, ParameterDirection.Output);

                if (objEndorsementDetailInfo.ENDORSEMENT_DATE.Ticks != 0)
                    objWrapper.AddParameter("@ENDORSEMENT_DATE", objEndorsementDetailInfo.ENDORSEMENT_DATE);
                else
                    objWrapper.AddParameter("@ENDORSEMENT_DATE", null);

                objWrapper.AddParameter("@CREATED_BY", objEndorsementDetailInfo.CREATED_BY);

                if (objEndorsementDetailInfo.CREATED_DATETIME.Ticks != 0)
                    objWrapper.AddParameter("@CREATED_DATETIME", objEndorsementDetailInfo.CREATED_DATETIME);
                else
                    objWrapper.AddParameter("@CREATED_DATETIME", null);


                objWrapper.AddParameter("@ENDORSEMENT_STATUS", objEndorsementDetailInfo.ENDORSEMENT_STATUS);

                if (ProcessType != "")
                {
                    objWrapper.AddParameter("@PROCESS_TYPE", ProcessType);
                }


                int returnResult = objWrapper.ExecuteNonQuery(strStoredProc);

                objEndorsementDetailInfo.ENDORSEMENT_NO = Convert.ToInt32(objParam.Value);
                EndorsementNO = objEndorsementDetailInfo.ENDORSEMENT_NO;
                objWrapper.ClearParameteres();
                return returnResult;


            }
            catch (Exception ex)
            {
                throw (new Exception("Error occured while inserting record in endorsement log. \n" + ex.Message));
            }
        }

        #region AddEndorsementLogDetails
        /// <summary>
        /// Save Process Log Details
        /// </summary>
        /// <param name="objEndorsementDetailInfo">Model class object.</param>
        /// <returns>No of records effected.</returns>
        public int AddEndorsementLogDetails(ClsEndorsementDetailInfo objEndorsementDetailInfo)
        {
            string strStoredProc = "Proc_InsertPOL_POLICY_ENDORSEMENTS_DETAILS";
            DateTime RecordDate = DateTime.Now;

            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@POLICY_ID", objEndorsementDetailInfo.POLICY_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", objEndorsementDetailInfo.POLICY_VERSION_ID);
                objWrapper.AddParameter("@CUSTOMER_ID", objEndorsementDetailInfo.CUSTOMER_ID);
                objWrapper.AddParameter("@ENDORSEMENT_NO", objEndorsementDetailInfo.ENDORSEMENT_NO);

                if (objEndorsementDetailInfo.ENDORSEMENT_DATE.Ticks != 0)
                    objWrapper.AddParameter("@ENDORSEMENT_DATE", objEndorsementDetailInfo.ENDORSEMENT_DATE);
                else
                    objWrapper.AddParameter("@ENDORSEMENT_DATE", null);

                objWrapper.AddParameter("@ENDORSEMENT_TYPE", objEndorsementDetailInfo.ENDORSEMENT_TYPE);
                objWrapper.AddParameter("@ENDORSEMENT_DESC", objEndorsementDetailInfo.ENDORSEMENT_DESC);
                objWrapper.AddParameter("@REMARKS", objEndorsementDetailInfo.REMARKS);
                objWrapper.AddParameter("@CREATED_BY", objEndorsementDetailInfo.CREATED_BY);
                objWrapper.AddParameter("@CREATED_DATETIME", objEndorsementDetailInfo.CREATED_DATETIME);
                objWrapper.AddParameter("@TRANS_ID", objEndorsementDetailInfo.TRANS_ID);

                SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@ENDORSEMENT_DETAIL_ID", objEndorsementDetailInfo.ENDORSEMENT_DETAIL_ID, SqlDbType.Int, ParameterDirection.Output);

                int returnResult = 0;
                /*if(TransactionLogRequired)
                {
                    objEndorsementDetailInfo.TransactLabel = ClsCommon.MapTransactionLabel("policies/processes/endorsementdetails.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objEndorsementDetailInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
                    objTransactionInfo.POLICY_ID			=	objEndorsementDetailInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID=	objEndorsementDetailInfo.POLICY_VERSION_ID;
                    objTransactionInfo.CLIENT_ID			=	objEndorsementDetailInfo.CUSTOMER_ID;

                    objTransactionInfo.TRANS_TYPE_ID	=	1;
                    objTransactionInfo.RECORDED_BY		=	objEndorsementDetailInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC		=	"Record Has Been Added";
                    objTransactionInfo.CHANGE_XML		=	strTranXML;
					
                    //Executing the query
                    returnResult	= objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
                }
                else
                {*/
                returnResult = objWrapper.ExecuteNonQuery(strStoredProc);
                /*}*/

                objEndorsementDetailInfo.ENDORSEMENT_DETAIL_ID = int.Parse(objSqlParameter.Value.ToString());

                objWrapper.ClearParameteres();


                return returnResult;
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }
        #endregion

        #region Reconsile Open items
        /// <summary>
        /// Reconcile All open items
        /// </summary>
        /// <param name="objProcessInfo"></param>
        /// <returns></returns>
        protected int RecocileOpenItems(ClsProcessInfo objProcessInfo) 
        {
            return RecocileOpenItems(objProcessInfo, 0);
        }
        /// <summary>
        /// Reconcile All open items from base version
        /// </summary>
        /// <param name="objProcessInfo"></param>
        /// <returns></returns>
        protected int RecocileOpenItems(ClsProcessInfo objProcessInfo, int BaseVersionID)
        {
            int RetVal = 0;
            string Sp_Name = "PROC_ReconcileOpenItems";
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", objProcessInfo.CUSTOMER_ID);
                objWrapper.AddParameter("@POLICY_ID", objProcessInfo.POLICY_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", objProcessInfo.NEW_POLICY_VERSION_ID);
                objWrapper.AddParameter("@COMMITTED_BY", objProcessInfo.COMPLETED_BY);
                objWrapper.AddParameter("@BASE_VERSION_ID", BaseVersionID);
                objWrapper.AddParameter("@PROCESS_ID", objProcessInfo.PROCESS_ID);
                objWrapper.AddParameter("@PROCESS_TYPE", objProcessInfo.PROCESS_TYPE);
                RetVal = objWrapper.ExecuteNonQuery(Sp_Name);
                objWrapper.ClearParameteres();
            }
            catch (Exception ex) 
            {
                throw (ex);
            }

            return RetVal;
        }

        #endregion

    }
}
