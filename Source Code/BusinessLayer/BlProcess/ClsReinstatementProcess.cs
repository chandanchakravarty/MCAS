/******************************************************************************************
<Author						: -		Vijay Arora
<Start Date					: -		21-12-2005
<End Date					: -	
<Description				: - 	Class for Reinstate Policy Process.
<Review Date				: - 
<Reviewed By				: - 	
*******************************************************************************************/
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Policy.Process;
using System.Collections;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlQuote;

namespace Cms.BusinessLayer.BlProcess
{
    /// <summary>
    /// Summary description for ClsReinstatementProcess.
    /// </summary>
    public class ClsReinstatementProcess : Cms.BusinessLayer.BlProcess.ClsPolicyProcess
    {
        public ClsReinstatementProcess()
        {
        }

        #region overrided function
        protected override bool OnSetPolicyStatus()
        {
            return false;
        }

        protected override bool OnCheckProcessEligibility()
        {
            return false;
        }

        protected override bool OnWriteTransactionLog()
        {
            return false;
        }
        #endregion
        private const int LAPSE_COVERAGE = 14244;
        private const int NO_LAPSE_COVERAGE = 14245;
        /// <summary>
        /// Gets the LOB ID.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns></returns>
        public int GetLOBID(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            string strStoredProc = "Proc_GetIDLOB";
            DataSet dsTemp = new DataSet();

            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

            objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
            objWrapper.AddParameter("@POLICY_ID", PolicyID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);

            dsTemp = objWrapper.ExecuteDataSet(strStoredProc);

            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(dsTemp.Tables[0].Rows[0][0]);
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// Get Policy Cancelation Date
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns></returns>
        public static DateTime GetPolicyCancel_RenewedDate(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            double CarryForwarAmount = 0;
            return GetPolicyCancel_RenewedDate(CustomerID, PolicyID, PolicyVersionID, out CarryForwarAmount);
        }
        public static DateTime GetPolicyCancel_RenewedDate(int CustomerID, int PolicyID, int PolicyVersionID, out double CarryForwarAmount)
        {
            string strStoredProc = "Proc_GetPolicyCancelRenewedDate";
            DateTime returnResult;
            int retVal = 0;
            CarryForwarAmount = 0;
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);
            try
            {
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);

                SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@RETVAL", null, SqlDbType.VarChar, ParameterDirection.Output, 15);
                SqlParameter objSqlParameter1 = (SqlParameter)objWrapper.AddParameter("@CFD_AMOUNT", SqlDbType.Float, ParameterDirection.Output);

                retVal = objWrapper.ExecuteNonQuery(strStoredProc);
                if (objSqlParameter1.Value != System.DBNull.Value)
                {
                    CarryForwarAmount = Convert.ToDouble(objSqlParameter1.Value);
                }
                if (objSqlParameter.Value != System.DBNull.Value)
                {
                    returnResult = ConvertDBDateToCulture(objSqlParameter.Value.ToString());
                }
                else
                    returnResult = DateTime.MinValue;
                objWrapper.ClearParameteres();
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                objWrapper.Dispose();
            }

        }


        /// <summary>
        /// Overides the Start Process Method of base.
        /// </summary>
        /// <param name="objProcessInfo"></param>
        /// <returns></returns>
        public override bool StartProcess(ClsProcessInfo objProcessInfo)
        {
            try
            {
                //BEging the transaction
                base.BeginTransaction();

                //Reseting the transaction descrition text
                TransactionDescription = new System.Text.StringBuilder();

                //Checking the eligibility
                if (base.CheckProcessEligibility(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.PROCESS_ID) != 1)
                {
                    base.RollbackTransaction();
                    return false;
                }

                //Creating new version of policy
                int NewVersionID = 0;
                string newDispVersion = "";
                CreatePolicyNewVersion(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID, objProcessInfo.CREATED_BY.ToString(), out NewVersionID, out newDispVersion, "16");
                TransactionDescription.Append("New version (" + newDispVersion + ") of policy has been created.;");
                //Adding some properties in process info model object
                objProcessInfo.NEW_POLICY_VERSION_ID = NewVersionID;
                //Sets the Policy Status of newer version as under Reinstatement
                string strNewStatusDesc = "", strNewStatus = "";
                SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID, out strNewStatusDesc, out strNewStatus);
                TransactionDescription.Append("Policy status has been updated to " + strNewStatusDesc + ".;");
                //Starting the process
                objProcessInfo.POLICY_CURRENT_STATUS = strNewStatus;
                bool retval = base.StartProcess(objProcessInfo);
                //Writing the transaction log
                WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID,
                    GetTransactionLogDesc(objProcessInfo.PROCESS_ID), objProcessInfo.COMPLETED_BY, TransactionDescription.ToString(), TransactionChangeXML.ToString());
                //WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, 
                //	"Reinstament Process started on this Policy.", objProcessInfo.COMPLETED_BY, TransactionDescription.ToString());


                //Commiting the database transaction
                base.CommitTransaction();
                return retval;
            }
            catch (Exception exc)
            {
                base.RollbackTransaction();
                throw (exc);
            }

        }


        public override bool CommitProcess(ClsProcessInfo objProcessInfo)
        {
            try
            {
                //Beging the transaction
                base.BeginTransaction();

                //Reseting the transaction descrition text
                TransactionDescription = new System.Text.StringBuilder();

                //Checking the eligibility, for the newer version of policy
                if (base.CheckProcessEligibility(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID) != 1)
                {
                    base.RollbackTransaction();
                    return false;
                }
                bool retval;
                /*
                bool retval = base.CommitProcess (objProcessInfo);
				
                //Checking the return status
                if (retval == false)
                {
                    //Rollbacking the transaction
                    base.RollbackTransaction();
                    return retval;
                }
                */
                //get display version of new policy
                Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
                DataSet dsPolicyVersion = objGenInfo.GetPolicyDetails(objProcessInfo.CUSTOMER_ID, 0, 0, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID);
                string strDisplayVersion = "";
                if (dsPolicyVersion.Tables[0].Rows.Count > 0)
                {
                    if (dsPolicyVersion.Tables[0].Rows[0]["POLICY_DISP_VERSION"] != null)
                        strDisplayVersion = dsPolicyVersion.Tables[0].Rows[0]["POLICY_DISP_VERSION"].ToString();
                }
                dsPolicyVersion.Clear();
                dsPolicyVersion.Dispose();

                string strStatusDesc = "", strNewStatus = "";
                //Update Insurance Score from customer if reinstate with lapse
                #region Commented Code of insurance scrore
                //if (objProcessInfo.CANCELLATION_TYPE==LAPSE_COVERAGE)
                //{
                //    UpdatePolicyInsuranceScore(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID,out strStatusDesc);
                //    TransactionDescription.Append(strStatusDesc);
                //}
                #endregion
                //Updating Policy terms,policy effective date and policy ecxpiry date as per new policy term,policy effective date and expiry date on reinstatement process's screeen 
                /*Commented by Pravesh on 9 july 2007 Not to change term.effective and expiry date of policy as per itrack Issue 2128
                UpdatePolicyTerm_EffectiveExpirydate(objProcessInfo);  
                TransactionDescription.Append("New version (" + strDisplayVersion
                    + ") of policy updated for new policy term,policy effective date and policy expiry date.;");
                */
                #region posting premium
                string TransDesc = "";
                double CarryFDAmt = 0;
                //DataSet dsPolicy;
                //int PreviousTerm=0;
                DateTime PreEffectiveDate = DateTime.MinValue, POLICY_CANCEL_DATE = DateTime.MinValue;
                int intResult;
                /* commented by Pravesh as Now Cancellation date will be compare instead
                  Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGeneralInformation = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
                dsPolicy = objGeneralInformation.GetPolicyDataSet(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID);
                if (dsPolicy.Tables[0].Rows.Count >0)
                {
                    if(dsPolicy.Tables[0].Rows[0]["APP_TERMS"]!=null && dsPolicy.Tables[0].Rows[0]["APP_TERMS"].ToString()!="")
                        PreviousTerm = int.Parse(dsPolicy.Tables[0].Rows[0]["APP_TERMS"].ToString()); 
                    if(dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"]!=null && dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()!="")
                        PreEffectiveDate = Convert.ToDateTime(dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()); 
                    dsPolicy.Clear();
                }*/
                POLICY_CANCEL_DATE = GetPolicyCancel_RenewedDate(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, out CarryFDAmt); // ).ToString("MM/dd/yyyy");            
                /*dsPolicy==FetchPrevousProcessInfo(objProcessInfo);
                if (dsPolicy.Tables[0].Rows.Count >0)
                {
                    if(dsPolicy.Tables[0].Rows[0]["CFD_AMT"]!=null && dsPolicy.Tables[0].Rows[0]["CFD_AMT"].ToString()!="")
                        CarryFDAmt = int.Parse(dsPolicy.Tables[0].Rows[0]["CFD_AMT"].ToString()); 
                    dsPolicy.Clear(); 
                }
                dsPolicy.Dispose(); 
                */
                string Apply_Reinstate_Fee = "N";
                if (objProcessInfo.APPLY_REINSTATE_FEE == 10963)
                    Apply_Reinstate_Fee = "Y";
                //if (PreEffectiveDate.Date ==objProcessInfo.EFFECTIVE_DATETIME.Date ) 
                if (POLICY_CANCEL_DATE.Date == objProcessInfo.EFFECTIVE_DATETIME.Date)
                {
                    //Commented By Lalit May 05,2010
                    //PostReinstatementPremium(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID, 
                    //    objProcessInfo.POLICY_VERSION_ID,objProcessInfo.NEW_POLICY_VERSION_ID,POLICY_SAME_TERM,objProcessInfo.EFFECTIVE_DATETIME,CarryFDAmt,objProcessInfo.ROW_ID,objProcessInfo.COMPLETED_BY,objProcessInfo.PROCESS_ID,
                    //    out TransDesc,"O",objProcessInfo.PROCESS_ID.ToString(),Apply_Reinstate_Fee);

                    PostPolicyProcessPremium(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,
                      objProcessInfo.COMPLETED_BY, objProcessInfo.PROCESS_ID, out TransDesc, objProcessInfo.POLICY_TERMS.ToString(), objProcessInfo.PROCESS_TYPE);
                    TransactionDescription.Append("\n" + TransDesc + ";");
                }
                else
                {

                    //Changed By Ravindra(07-27-2007)
                    /*intResult = PostPolicyPremium (objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID, 
                        objProcessInfo.NEW_POLICY_VERSION_ID,objProcessInfo.COMPLETED_BY,objProcessInfo.PROCESS_ID,
                        out TransDesc,"O",objProcessInfo.PROCESS_ID.ToString());*/

                    intResult = PostPolicyProcessPremium(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,
                objProcessInfo.COMPLETED_BY, objProcessInfo.PROCESS_ID, out TransDesc, objProcessInfo.POLICY_TERMS.ToString(), objProcessInfo.PROCESS_TYPE);
                    TransactionDescription.Append("\n" + TransDesc + ";");

                    //int PPDRowID;
                    //May 05,2011
                    #region Commented by Lalit for posting re-instatement process
                    //intResult = PostReinstatementPremiumOtherTerm(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID, 
                    //    objProcessInfo.NEW_POLICY_VERSION_ID,objProcessInfo.COMPLETED_BY,objProcessInfo.PROCESS_ID,
                    //    out TransDesc,"O",objProcessInfo.PROCESS_ID.ToString(),objProcessInfo.EFFECTIVE_DATETIME ,objProcessInfo.POLICY_VERSION_ID, out PPDRowID);
                    ////Changes by ravindra ends here

                    //TransactionDescription.Append("\n" + TransDesc + ";");

                    ////objProcessInfo.ROW_ID replaced by Out param --> PPDRowID
                    //PostReinstatementPremium(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID, 
                    //    objProcessInfo.POLICY_VERSION_ID,objProcessInfo.NEW_POLICY_VERSION_ID,POLICY_OTHER_TERM,objProcessInfo.EFFECTIVE_DATETIME,CarryFDAmt,PPDRowID,objProcessInfo.COMPLETED_BY,objProcessInfo.PROCESS_ID,
                    //    out TransDesc,"O",objProcessInfo.PROCESS_ID.ToString(),Apply_Reinstate_Fee);

                    #endregion


                    TransactionDescription.Append("\n" + TransDesc);



                    //bool retval;				
                    if (intResult == -3 || intResult == -2)
                    {
                        base.RollbackTransaction();
                        Cms.BusinessLayer.BlCommon.ClsDiary objClsDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
                        Cms.Model.Diary.TodolistInfo objModel = new Cms.Model.Diary.TodolistInfo();

                        if (intResult == -3)
                        {
                            ClsPolicyErrMsg.strMessage = "Selected Billing plan has been deactivated.Please select new Billing Plan.";
                            objModel.SUBJECTLINE = "Please select new Billing plan.";
                            objModel.NOTE = "Selected Billing plan has been deactivated.";
                        }
                        else
                        {
                            ClsPolicyErrMsg.strMessage = "Commission not found,So process not comitted.";
                            objModel.SUBJECTLINE = "Reinstate Process Commit Failed.";
                            objModel.NOTE = "No Commission found.";
                        }
                        //						objModel.TOUSERID =objProcessInfo.UNDERWRITER; 
                        objModel.LISTTYPEID = (int)BlCommon.ClsDiary.enumDiaryType.PROCESS_PENDING_REQUEST;//15;
                        objModel.POLICY_ID = objProcessInfo.POLICY_ID;
                        objModel.POLICY_VERSION_ID = objProcessInfo.NEW_POLICY_VERSION_ID; //objProcessInfo.POLICY_VERSION_ID;
                        objModel.CUSTOMER_ID = objProcessInfo.CUSTOMER_ID;
                        if (IsEODProcess)
                            objModel.CREATED_BY = EODUserID;
                        else
                            objModel.CREATED_BY = objProcessInfo.COMPLETED_BY;
                        objModel.CREATED_DATETIME = DateTime.Now;
                        objModel.RECDATE = DateTime.Now;
                        //objModel.FOLLOWUPDATE = DateTime.Now;    
                        objModel.LAST_UPDATED_DATETIME = DateTime.Now;
                        objModel.LOB_ID = objProcessInfo.LOB_ID;
                        objModel.MODULE_ID = (int)BlCommon.ClsDiary.enumModuleMaster.POLICY_PROCESS;
                        //objModel.RECDATE = DateTime.Now;

                        //						objModel.STARTTIME = (System.DateTime)DateTime.Now;
                        //						objModel.ENDTIME = (System.DateTime)DateTime.Now;

                        //objClsDiary.Add(objModel,"");					
                        objClsDiary.DiaryEntryfromSetup(objModel);

                        retval = false;
                        return false;
                    }
                }
                #endregion
                //Updating the status of new version of policy
                if (DateTime.Compare(objProcessInfo.EFFECTIVE_DATETIME.Date, System.DateTime.Now.Date) <= 0)
                {
                    SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID,
                        objProcessInfo.POLICY_VERSION_ID, objProcessInfo.PROCESS_ID, POLICY_STATUS_INACTIVE);

                    SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID, out strStatusDesc, out strNewStatus);
                    TransactionDescription.Append("Status of new version (" + strDisplayVersion
                        + ") of policy has been updated to " + strStatusDesc + ".;");
                    objProcessInfo.POLICY_CURRENT_STATUS = strNewStatus;

                }
                // status intact & set status of new policy as Marked for Cancellation(SCANCEL)
                else
                {
                    SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID, POLICY_STATUS_MARKED_REINSTATE);
                    TransactionDescription.Append("Status of new version (" + strDisplayVersion
                        + ") of policy has been Marked for Reinstatement.;");
                    objProcessInfo.POLICY_CURRENT_STATUS = POLICY_STATUS_MARKED_REINSTATE;
                }

                retval = base.CommitProcess(objProcessInfo, "NEWVERSION");

                //Checking the return status
                if (retval == false)
                {
                    //Rollbacking the transaction
                    base.RollbackTransaction();
                    return retval;
                }

                if (objProcessInfo.PRINTING_OPTIONS == (int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.YES))
                    TransactionDescription.Append("\n \"No printing Required at all\" is checked hence no entry in Print job;");
                //Writing the transaction log
                WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,
                    GetTransactionLogDesc(objProcessInfo.PROCESS_ID), objProcessInfo.COMPLETED_BY, TransactionDescription.ToString());

                //Committing the transaction
                //				base.CommitTransaction();
                //


                //Comment by Lalit,May 04,2011
                //for new implimentation of brazil ,Reinstatement print requirement not clear
                #region Print Job Entry Commented Code
                //                #region Add Print Jobs for Reinstatement Notice
                ////				try
                ////				{
                ////					base.BeginTransaction(); 
                //                    ClsPrintJobsInfo objPrintJobsInfo = GetPrintJobsValues(objProcessInfo,"NEWVERSION");
                //                    string stateCode=new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().SetStateCode("POLICY",objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID,objProcessInfo.CUSTOMER_ID); 
                //                    string AgencyCode = GetPDFAgencyCode(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID, "POLICY");
                //                    objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL","ALL","Insured","REINS");
                //                    //objPrintJobsInfo.URL_PATH = ClsPolicyProcess.PRINT_JOBS_URL_PATH + AgencyCode + "/" + objProcessInfo.CUSTOMER_ID.ToString() + "/" + "POLICY/REINS_NOTICE" + "/" + "final";
                //                    objPrintJobsInfo.URL_PATH = ClsPolicyProcess.PRINT_JOBS_URL_PATH + AgencyCode + "/" + objProcessInfo.CUSTOMER_ID.ToString() + "/" + "POLICY" + "/" + "final";
                //                    //objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL","ALL","Agent","REINS");
                //                    int tmpInsured=objProcessInfo.INSURED;
                //                    int tmpAgency=objProcessInfo.AGENCY_PRINT;  
                //                    int oldPolicyVersion=objProcessInfo.POLICY_VERSION_ID; 
                //                    objProcessInfo.POLICY_VERSION_ID=objProcessInfo.NEW_POLICY_VERSION_ID;
                //                    if (objProcessInfo.SEND_ALL==(int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.YES))
                //                        getAdditionalInterest(objProcessInfo);
                //                   string fileNameInsured="";
                //                   string fileNameAgency="";
                //                   string fileNameAddInt="";
                //                   string strAcordForm="";
                //                   string strAutoIdCardPage="";
                //                    //string fileNameInsuredNotice="";
                //                    //string fileNameAgencyNotice="";
                //                    //string fileNameAddIntNotice="";
                //                    //generating additional interst PDF
                //                    bool IsAddIntExists=IsAddIntExist(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID,objProcessInfo.LOB_ID.ToString(),"POLICY");

                //                if(objProcessInfo.LOB_ID==ClsRenewalProcess.POLICY_LOB_AUTO || objProcessInfo.LOB_ID==ClsRenewalProcess.POLICY_LOB_HOME)
                //                {
                //                    string SucessfullPdf = GeneratePDF(objProcessInfo);
                //                    fileNameInsured=WordingPDFFileName;
                //                    fileNameAgency=AgentWordingPDFFileName;
                //                    strAutoIdCardPage=AutoIdCardPDFFileName;
                //                    fileNameAddInt=AdditionalIntrstPDFFileName;
                //                    strAcordForm=AcordPDFFileName;
                //                }
                //                else
                //                {
                //                    if (IsAddIntExists)
                //                        fileNameAddInt=GeneratePDF(objProcessInfo,"ADDLINT");
                //                    //fileNameAddIntNotice=
                //                    //generating NOTICE and Dec page for Customer/Insured
                //                    objProcessInfo.INSURED = 1;
                //                    objProcessInfo.AGENCY_PRINT = 0;
                //                    //fileNameInsuredNotice=GeneratePDF(objProcessInfo,"REINS_NOTICE");
                //                    fileNameInsured=GeneratePDF(objProcessInfo,"DECPAGE");
                //                    //generating NOTICE and Dec page for Agency
                //                    objProcessInfo.INSURED = 0;
                //                    objProcessInfo.AGENCY_PRINT = 1;
                //                    //fileNameAgencyNotice = GeneratePDF(objProcessInfo,"REINS_NOTICE");
                //                    fileNameAgency=GeneratePDF(objProcessInfo,"DECPAGE");
                //                    //accord form generation commented as per issue 1860 of Itrack but uncommented as Pawan Asked
                //                    strAcordForm = GeneratePDF(objProcessInfo,"ACORD");
                //                    strAutoIdCardPage = GeneratePDF(objProcessInfo,BlCommon.ClsCommon.DOCUMENT_TYPE_AUTO_ID_CARD);						
                //                    //adding Entry in print job table for customer/insured
                //                    objProcessInfo.INSURED=tmpInsured;
                //                    objProcessInfo.AGENCY_PRINT=tmpAgency;  
                //                }
                //                if (objProcessInfo.PRINTING_OPTIONS ==(int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.NO))
                //                {
                //                    // do entry in print job other than No print required --chnanged on 3 april 2008
                //                    //if(objProcessInfo.INSURED  == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL).ToString())|| objProcessInfo.INSURED  == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString()))
                //                    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
                //                    if(objProcessInfo.INSURED  != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED).ToString()))
                //                    {
                //                        objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER;
                //                        objPrintJobsInfo.FILE_NAME =fileNameInsured;
                //                        AddPrintJobs(objPrintJobsInfo);	
                //                        /*
                //                            objPrintJobsInfo.FILE_NAME =fileNameInsuredNotice;
                //                            AddPrintJobs(objPrintJobsInfo);	*/
                //                    }
                //                    else // Do entry for non printing files
                //                    { 
                //                        objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER_NOWORD;
                //                        objPrintJobsInfo.FILE_NAME =NoWordingPDFFileName;
                //                        AddPdfFileLog(objPrintJobsInfo);	
                //                    }
                //                    //adding Entry in print job table for agency
                //                    // do entry in print job other than No print required --chnanged on 3 april 2008
                //                    //if(objProcessInfo.AGENCY_PRINT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL).ToString()) || objProcessInfo.AGENCY_PRINT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString()))


                //                    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
                //                    objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_AGENCY;
                //                    objPrintJobsInfo.FILE_NAME =fileNameAgency;
                //                    if(objProcessInfo.AGENCY_PRINT != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED).ToString()))
                //                        AddPrintJobs(objPrintJobsInfo);
                //                    else // Do entry for non printing files
                //                        AddPdfFileLog(objPrintJobsInfo);	
                //                    /*
                //                            objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL","ALL","Agent","REINS");
                //                            objPrintJobsInfo.FILE_NAME =fileNameAgencyNotice;
                //                            AddPrintJobs(objPrintJobsInfo);	*/


                //                    //objProcessInfo.POLICY_VERSION_ID=oldPolicyVersion; 
                //                    //adding Entry in print job table for Additional interests
                //                    // do entry in print job other than No print required --chnanged on 3 april 2008
                //                    if(IsAddIntExists==true)
                //                    {
                //                        if (objProcessInfo.ADD_INT_ID !="")
                //                        {
                //                            string strAddlInt="";
                //                            objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_ADDL_INTEREST;
                //                            objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;						
                //                            string [] addIntIdArr = objProcessInfo.ADD_INT_ID.Split('~');
                //                            if(addIntIdArr!=null && addIntIdArr.Length>0)
                //                            {
                //                                for(int jCounter=0;jCounter<addIntIdArr.Length;jCounter++)
                //                                {
                //                                    string [] strArr = addIntIdArr[jCounter].Split('^');
                //                                    if(strArr==null || strArr.Length<1)
                //                                        continue;

                //                                    strAddlInt = strArr[0].ToString()  + "_" + objPrintJobsInfo.CUSTOMER_ID.ToString() + "_" + objPrintJobsInfo.POLICY_ID.ToString() + "_" + objPrintJobsInfo.POLICY_VERSION_ID.ToString() + "_" + strArr[1].ToString()  + "_" + strArr[2].ToString();
                //                                    objPrintJobsInfo.FILE_NAME = FindAddIntFileName(fileNameAddInt,strAddlInt);
                //                                    if((objProcessInfo.ADD_INT != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED).ToString()) ))
                //                                        AddPrintJobs(objPrintJobsInfo);								
                //                                    else // Do entry for non printing files
                //                                        AddPdfFileLog(objPrintJobsInfo);
                //                                }
                //                            }

                //                        }
                //                    }
                //                    //auto ID Card as per Itrack 4869
                //                    if (objProcessInfo.NO_COPIES==0)
                //                        objProcessInfo.NO_COPIES=2; //default value
                //                    if(objProcessInfo.STATE_CODE=="IN" || objProcessInfo.STATE_ID==14)
                //                        objProcessInfo.NO_COPIES=1;
                //                    if((objProcessInfo.LOB_ID ==ClsRenewalProcess.POLICY_LOB_AUTO  || objProcessInfo.LOB_ID == ClsRenewalProcess.POLICY_LOB_CYCL) && strAutoIdCardPage!="")
                //                    {
                //                        objPrintJobsInfo.DOCUMENT_CODE = objPrintJobsInfo.ENTITY_TYPE = "AUTO_ID_CARD";						
                //                        objPrintJobsInfo.FILE_NAME = strAutoIdCardPage;
                //                        if(objProcessInfo.AUTO_ID_CARD != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString()))
                //                        {
                //                            for(int i=0;i<objProcessInfo.NO_COPIES;i++)						
                //                                AddPrintJobs(objPrintJobsInfo);	
                //                        }
                //                        else // Do entry for non printing files
                //                            AddPdfFileLog(objPrintJobsInfo);
                //                    }

                //                    // Do entry for non printing files
                //                    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
                //                    objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER;													
                //                    objPrintJobsInfo.FILE_NAME =fileNameInsured; 
                //                    AddPdfFileLog(objPrintJobsInfo);
                //                    // Do entry for non printing files
                //                    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_ACORD;
                //                    objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_DOCUMENT_CODE_ACORD;													
                //                    objPrintJobsInfo.FILE_NAME =strAcordForm; 
                //                    AddPdfFileLog(objPrintJobsInfo);
                //                }
                //                else
                //                {
                //                    // Do entry for non printing files
                //                    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
                //                    objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER_NOWORD;
                //                    objPrintJobsInfo.FILE_NAME =NoWordingPDFFileName;
                //                    AddPdfFileLog(objPrintJobsInfo);	

                //                    // Do entry for non printing files							
                //                    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
                //                    objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_AGENCY;
                //                    objPrintJobsInfo.FILE_NAME =fileNameAgency;
                //                    AddPdfFileLog(objPrintJobsInfo);	

                //                    // Do entry for non printing files
                //                    if(IsAddIntExists==true)
                //                    {
                //                        if (objProcessInfo.ADD_INT_ID !="")
                //                        {
                //                            string strAddlInt="";
                //                            objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_ADDL_INTEREST;
                //                            objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;						
                //                            string [] addIntIdArr = objProcessInfo.ADD_INT_ID.Split('~');
                //                            if(addIntIdArr!=null && addIntIdArr.Length>0)
                //                            {
                //                                for(int jCounter=0;jCounter<addIntIdArr.Length;jCounter++)
                //                                {
                //                                    string [] strArr = addIntIdArr[jCounter].Split('^');
                //                                    if(strArr==null || strArr.Length<1)
                //                                        continue;

                //                                    strAddlInt = strArr[0].ToString()  + "_" + objPrintJobsInfo.CUSTOMER_ID.ToString() + "_" + objPrintJobsInfo.POLICY_ID.ToString() + "_" + objPrintJobsInfo.POLICY_VERSION_ID.ToString() + "_" + strArr[1].ToString()  + "_" + strArr[2].ToString();
                //                                    objPrintJobsInfo.FILE_NAME = FindAddIntFileName(fileNameAddInt,strAddlInt);
                //                                    AddPdfFileLog(objPrintJobsInfo);
                //                                }
                //                            }

                //                        }
                //                    }
                //                    // Do entry for non printing files
                //                    if((objProcessInfo.LOB_ID ==ClsRenewalProcess.POLICY_LOB_AUTO  || objProcessInfo.LOB_ID == ClsRenewalProcess.POLICY_LOB_CYCL) && strAutoIdCardPage!="")
                //                    {
                //                        objPrintJobsInfo.DOCUMENT_CODE = objPrintJobsInfo.ENTITY_TYPE = "AUTO_ID_CARD";						
                //                        objPrintJobsInfo.FILE_NAME = strAutoIdCardPage;
                //                        AddPdfFileLog(objPrintJobsInfo);
                //                    }

                //                    // Do entry for non printing files
                //                    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
                //                    objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER;													
                //                    objPrintJobsInfo.FILE_NAME =fileNameInsured; 
                //                    AddPdfFileLog(objPrintJobsInfo);
                //                    // Do entry for non printing files
                //                    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_ACORD;
                //                    objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_DOCUMENT_CODE_ACORD;													
                //                    objPrintJobsInfo.FILE_NAME =strAcordForm; 
                //                    AddPdfFileLog(objPrintJobsInfo);
                //                }
                //                /*
                //                DataSet dsPrintDoc= base.GetPrintDocuments(objProcessInfo.PROCESS_ID)  ; 
                //                for (int i=0;i<dsPrintDoc.Tables[0].Rows.Count -1;i++)
                //                {
                //                    string doc_code=dsPrintDoc.Tables[0].Rows[i]["DOCUMENT_CODE"].ToString();   
                //                    if (doc_code=="")
                //                    objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL","ALL","Agent",doc_code );
                //                    AddPrintJobs(objPrintJobsInfo);	

                //                }
                //                dsPrintDoc.Clear();
                //                dsPrintDoc.Dispose(); 
                //                */

                //                #endregion
                #endregion

                base.CommitTransaction();
                //				
                //				}
                //				catch(Exception exx)
                //				{
                //					base.RollbackTransaction();
                //					//base.CommitTransaction(); 
                //					//throw new Exception("Process Committed but Unable to generate PDF",exx);
                //					ClsPolicyProcess.PrintingErrorFlag = true;
                //					ClsPolicyErrMsg.strMessage = "Process Committed but Unable to generate PDF";
                //					return retval;
                //				}





                return retval;
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw (ex);
            }
        }
        private void UpdatePolicyInsuranceScore(int CustomerId, int PolicyId, int PolicyVersionId, out string strTransDesc)
        {
            strTransDesc = "";
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@TRAN_DESC", null, SqlDbType.NVarChar, ParameterDirection.Output, 100);
                objWrapper.ExecuteNonQuery("Proc_UpdatePolicyInsuranceScore");
                objWrapper.ClearParameteres();
                if (objSqlParameter.Value != System.DBNull.Value)
                    strTransDesc = objSqlParameter.Value.ToString();
            }
            catch (Exception ex)
            {
                throw (new Exception("Error while updating policy Insurance Score.", ex));
            }
        }
        //Added By Ravindra(07-27-2007) 
        public int PostReinstatementPremiumOtherTerm(int CustomerId, int PolicyId, int PolicyVersionId
            , int UserID, int ProcessType, out string TransDescription, string Term, string strProcessType
            , DateTime ChangeEffectiveDate, int PolicybaseVersionId, out int PPDRowID)
        {
            TransDescription = "";
            try
            {
                //double dblPremium, dblOtherFees, dblMCCAFees; 
                int AppId, AppVersionId, LobId, PremiumProcessTableRowId = 0, retVal = 0;

                string strIsSamePolicy = "N";

                GetAppLobDetails(CustomerId, PolicyId, PolicyVersionId, out AppId, out AppVersionId, out LobId);
                //Calling Wrapper class

                if (IsEODProcess)
                {
                    UserID = EODUserID;
                }

                ClsGenerateQuote objQuote = new ClsGenerateQuote(mSystemID);
                int quodeId;
                objQuote.GeneratePolicyQuote(CustomerId, PolicyId, PolicyVersionId, LobId.ToString(), out quodeId, UserID.ToString());

                //Call Split Premium 
                Cms.BusinessLayer.BlQuote.clsSplitPremium objSplitPremium = new Cms.BusinessLayer.BlQuote.clsSplitPremium();
                ArrayList arrPremiumXML = objSplitPremium.SplitPremiumsPol(CustomerId, PolicyId, PolicyVersionId, strProcessType, objWrapper);

                DateTime PolicyExpirationDate = DateTime.MaxValue;

                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@TRAN_EFFECTIVE_DATE", PolicyExpirationDate);
                objWrapper.AddParameter("@CUST_ID", CustomerId);
                objWrapper.AddParameter("@POL_ID", PolicyId);
                objWrapper.AddParameter("@UPDATE_PROCESSED", 0);
                objWrapper.AddParameter("@CALLED_FROM", 12);
                objWrapper.ExecuteNonQuery("Proc_ReconCustomerOpenItem");
                objWrapper.ClearParameteres();


                for (int j = 0; j < arrPremiumXML.Count; j++)
                {
                    PremiumDetails objPremiumdetails = (PremiumDetails)arrPremiumXML[j];
                    ArrayList arrPremium = GetPremiumForLapseInCoverage(CustomerId, PolicyId,
                        PolicyVersionId, objPremiumdetails.PremiumXML, objPremiumdetails.QuoteType,
                        Term, ProcessType.ToString(), out PremiumProcessTableRowId);

                    for (int i = 0; i < arrPremium.Count; i++)
                    {

                        Premium objPremium = (Premium)arrPremium[i];

                        int CommissionClass = GetCommissionClass(CustomerId, PolicyId, PolicyVersionId,
                            objPremium.RiskID, objWrapper);

                        objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                        objWrapper.AddParameter("@POLICY_ID", PolicyId);
                        objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);

                        objWrapper.AddParameter("@EFFECTIVE_PREMIUM", objPremium.NetPremium);
                        objWrapper.AddParameter("@EFFECTIVE_OSTA_FEES", objPremium.MCCAFees + objPremium.OtherFees);
                        objWrapper.AddParameter("@EFFECTIVE_DATE", ChangeEffectiveDate);
                        objWrapper.AddParameter("@COMMISSION_CLASS", CommissionClass);
                        objWrapper.AddParameter("@SOURCE_ROW_ID", PremiumProcessTableRowId);
                        objWrapper.AddParameter("@RISK_ID", objPremium.RiskID);
                        objWrapper.AddParameter("@RISK_TYPE", objPremium.RiskType);
                        objWrapper.AddParameter("@USER_ID", UserID);
                        objWrapper.AddParameter("@INFORCE_PREMIUM", objPremium.InforcePremium);
                        objWrapper.AddParameter("@INFORCE_FEES", objPremium.InforceFees);
                        objWrapper.AddParameter("@IS_SAME_POLICY", strIsSamePolicy);

                        SqlParameter objParam = (SqlParameter)objWrapper.AddParameter
                            ("@RETVAL", null, SqlDbType.Int, ParameterDirection.Output);

                        SqlParameter objExpDate = (SqlParameter)objWrapper.AddParameter("@POLICY_EXPIRATION_DATE", null, SqlDbType.DateTime,
                            ParameterDirection.Output);
                        objWrapper.ExecuteNonQuery("Proc_DistributeReinstatementPremium");

                        retVal = Convert.ToInt32(objParam.Value);

                        if (objExpDate != null && objExpDate.Value != DBNull.Value)
                        {
                            PolicyExpirationDate = Convert.ToDateTime(objExpDate.Value);
                        }

                        objWrapper.ClearParameteres();
                        strIsSamePolicy = "Y";

                    }


                    //Ravindra(10-17-200): 
                    //Reconcile any negative item pending from previous version even if it is due in future

                    objWrapper.ClearParameteres();
                    objWrapper.AddParameter("@TRAN_EFFECTIVE_DATE", PolicyExpirationDate);
                    objWrapper.AddParameter("@CUST_ID", CustomerId);
                    objWrapper.AddParameter("@POL_ID", PolicyId);
                    objWrapper.AddParameter("@UPDATE_PROCESSED", 0);
                    objWrapper.ExecuteNonQuery("Proc_ReconCustomerOpenItem");
                    objWrapper.ClearParameteres();


                    //Insert info in Balance Tab
                    objWrapper.ClearParameteres();
                    objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                    objWrapper.AddParameter("@POLICY_ID", PolicyId);
                    objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                    objWrapper.AddParameter("@SOURCE_ROW_ID", PremiumProcessTableRowId);
                    objWrapper.ExecuteNonQuery("Proc_InsertBalanceInformation");
                    objWrapper.ClearParameteres();

                }
                //Update Written and change in premium in premium split table --added by Pravesh on 12 dec 2007
                objSplitPremium.UpdateWrittenEnforcePremiumSplit(CustomerId, PolicyId, PolicyVersionId, PolicybaseVersionId, strProcessType, objWrapper);
                objWrapper.ClearParameteres();
                //end ehre

                PPDRowID = PremiumProcessTableRowId;

                return retVal;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to commit process, error while posting premium.", ex);
            }

        }

        private ArrayList GetPremiumForLapseInCoverage(int CustomerId, int PolicyId,
            int PolicyVersionId, string strPremiumXML, string strQuoteType,
            string Term, string ProcessType,
            out int PremiumProcessTableRowID)
        {
            PremiumProcessTableRowID = 0;
            int returnResult = 0;
            ArrayList arrPremium = new ArrayList();
            string strFinalPremium = "";
            string strPostedPremium = "";
            double dblPremium = 0, dblOtherFees = 0, dblMCCAFees = 0, dblGrossPremium = 0;
            double dblNetFinalPremium = 0, dblFeesToReverse = 0, dblTemp = 0;

            Premium objTotalPremium = new Premium();

            try
            {
                arrPremium = GetPremiumFromXML(strPremiumXML, strQuoteType, out strFinalPremium, out dblNetFinalPremium);
                for (int i = 0; i < arrPremium.Count; i++)
                {

                    Premium objPremium = (Premium)(arrPremium[i]);

                    objPremium.InforcePremium = objPremium.GrossPremium;
                    objPremium.InforceFees = objPremium.MCCAFees + objPremium.OtherFees;

                    objTotalPremium.GrossPremium += objPremium.GrossPremium;
                    objTotalPremium.NetPremium += objPremium.NetPremium;
                    objTotalPremium.MCCAFees += objPremium.MCCAFees;
                    objTotalPremium.OtherFees += objPremium.OtherFees;


                    CalculateChangeEffectivePremium(CustomerId, PolicyId, PolicyVersionId, objPremium, ref dblFeesToReverse);

                    dblGrossPremium = objPremium.GrossPremium + dblGrossPremium;
                    dblPremium = objPremium.NetPremium + dblPremium;
                    dblOtherFees = objPremium.OtherFees + dblOtherFees;
                    dblMCCAFees = objPremium.MCCAFees + dblMCCAFees;
                }


                CalculateChangeEffectivePremium(CustomerId, PolicyId, PolicyVersionId, objTotalPremium, ref dblTemp);

                //There could be difference in Total as premium is pro rarted and rounded at risk level
                if (objTotalPremium.GrossPremium != dblGrossPremium)
                {
                    Premium objPremium = (Premium)(arrPremium[0]);

                    double dblDifference = objTotalPremium.GrossPremium - dblGrossPremium;
                    dblGrossPremium = dblGrossPremium + dblDifference;

                    objPremium.GrossPremium += dblDifference;

                    dblDifference = (objTotalPremium.MCCAFees + objTotalPremium.OtherFees) - (dblMCCAFees + dblOtherFees);
                    dblMCCAFees = dblMCCAFees + dblDifference;

                    objPremium.MCCAFees += dblDifference;

                    dblDifference = (dblGrossPremium - dblMCCAFees - dblOtherFees) - dblPremium;

                    dblPremium = dblPremium + dblDifference;

                    objPremium.NetPremium += dblDifference;

                }

                //Making new premium xml
                strPostedPremium = "<PREMIUM><GROSSPREMIUM> "
                    + Convert.ToString(dblGrossPremium)
                    + "</GROSSPREMIUM><NETPREMIUM>" + dblPremium.ToString()
                    + "</NETPREMIUM><OTHERFEES>" + dblOtherFees.ToString()
                    + "</OTHERFEES><MCCAFEES>" + dblMCCAFees.ToString()
                    + "</MCCAFEES></PREMIUM>";

            }
            catch (Exception ex)
            {
                strFinalPremium = "<PREMIUM><GROSSPREMIUM>400</GROSSPREMIUM><NETPREMIUM>400</NETPREMIUM><OTHERFEES>0</OTHERFEES><MCCAFEES>0</MCCAFEES></PREMIUM>";
                Premium objPremium = new Premium();
                objPremium.RiskID = 1;
                objPremium.NetPremium = 400;
                objPremium.GrossPremium = 400;
                objPremium.OtherFees = 0;
                objPremium.MCCAFees = 0;
                arrPremium.Add(objPremium);
                InsertFinalPremium(CustomerId, PolicyId, PolicyVersionId, Term, Convert.ToInt32(ProcessType), strFinalPremium, strFinalPremium, dblNetFinalPremium);
                PremiumProcessTableRowID = returnResult;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return arrPremium;
            }
            //Saving the Premium XML.
            returnResult = InsertFinalPremium(CustomerId, PolicyId, PolicyVersionId, Term, Convert.ToInt32(ProcessType), strFinalPremium, strPostedPremium, dblNetFinalPremium);
            PremiumProcessTableRowID = returnResult;
            return arrPremium;
        }

        //Added By Ravindra(07-27-2007) End here

        public override bool RollbackProcess(ClsProcessInfo objProcessInfo)
        {
            try
            {
                //BEging the transaction
                base.BeginTransaction();

                //Reseting the transaction descrition text
                TransactionDescription = new System.Text.StringBuilder();


                //Checking the eligibility
                if (base.CheckProcessEligibility(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID) != 1)
                {
                    base.RollbackTransaction();
                    return false;
                }

                bool retval = base.RollbackProcess(objProcessInfo, "NEWVERSION");

                //Checking the return status
                if (retval == false)
                {
                    //Rollbacking the transaction
                    base.RollbackTransaction();
                    return retval;
                }

                //Deleting the newer version
                base.DeletePolicyVersion(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID);
                TransactionDescription.Append("New version (" + objProcessInfo.NEW_POLICY_VERSION_ID.ToString("#.0")
                    + ") of policy has been deleted.;");


                //Writing the transaction log
                WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID,
                    GetTransactionLogDesc(objProcessInfo.PROCESS_ID), objProcessInfo.COMPLETED_BY, TransactionDescription.ToString());


                //Commiting  the transaction
                base.CommitTransaction();

                return retval;
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw (ex);
            }
        }

    }
}
