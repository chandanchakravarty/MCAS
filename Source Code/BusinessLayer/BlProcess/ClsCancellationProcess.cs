/******************************************************************************************
<Author				: -		Vijay Arora
<Start Date			: -		23-12-2005
<End Date			: -		
<Description		: - 	Buisness Layer for Policy Cancellation Process.
<Review Date		: - 
<Reviewed By		: - 	
*******************************************************************************************/


using System;
using System.Data;
using System.Data.SqlClient;
using Cms.DataLayer;
using Cms.Model.Policy.Process;
using Cms.BusinessLayer.BlAccount;
using Cms.CmsWeb;

namespace Cms.BusinessLayer.BlProcess
{
    /// <summary>
    /// Summary description for ClsCancellationProcess.
    /// </summary>
    public class ClsCancellationProcess : ClsPolicyProcess
    {
        public static string BILL_PAYMENT_DIRECT_BILL = "DB";
        public ClsCancellationProcess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected override bool OnWriteTransactionLog()
        {
            return false;
        }

        protected override bool OnSetPolicyStatus()
        {
            return false;
        }

        protected override bool OnCheckProcessEligibility()
        {
            return false;
        }
        //Ravindra(08-01-2007) :
        public override bool StartProcess(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
        {
            string StatusDescriptor = "";
            return this.StartProcess(objProcessInfo, out StatusDescriptor);
        }


        public bool StartCancellationProcess(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo,
            out string StatusDescriptor)
        {
            try
            {
                StatusDescriptor = "";

                //Reseting the transaction descrition text
                TransactionDescription = new System.Text.StringBuilder();

                //Checking the eligibility
                if (base.CheckProcessEligibility(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.PROCESS_ID) != 1)
                {
                    //StatusDescriptor = "Policy not eligible for selected process";
                    StatusDescriptor = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1483", ""); //"This process can't be launched on the chosen policy.";
                    ClsPolicyErrMsg.strMessage = StatusDescriptor;
                    return false;
                }
                //Itrack # 6145 -26 Aug 09 -Manoj
                //If there is Credit Balance from prior term system will not start cancellation process and user will be prompted with a message for the same
                if (base.CheckCreditBalanceEligibility(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID) != 1)
                {
                    StatusDescriptor = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1484", ""); //"This policy has a credit balance from the prior term that must be reconciled before launching the cancellation process.  Please see Accounting.";
                    ClsPolicyErrMsg.strMessage = StatusDescriptor;
                    return false;
                }
                //Creating new version of policy
                int NewVersionID = 0;
                string NewDispVersionID = "";
                CreatePolicyNewVersion(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID, objProcessInfo.CREATED_BY.ToString(), out NewVersionID, out NewDispVersionID, "12");

                TransactionDescription.Append(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1482", "") + NewDispVersionID + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1477", ""));//"New version ("//) of Policy has been created.;

                objProcessInfo.NEW_POLICY_VERSION_ID = NewVersionID;
                //updating the status of new Version of the policy
                SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, POLICY_STATUS_UNDER_CANCEL);
                objProcessInfo.POLICY_CURRENT_STATUS = POLICY_STATUS_UNDER_CANCEL;
                //Starting the process
                bool retval = base.StartProcess(objProcessInfo);
                if (retval)
                {
                    //Write transaction log entry
                    int listtypeid;
                    base.WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID
                        , GetTransactionLogDesc(objProcessInfo.PROCESS_ID, out listtypeid, objProcessInfo.CANCELLATION_TYPE), objProcessInfo.CREATED_BY, TransactionDescription.ToString(), TransactionChangeXML.ToString());
                }
                else
                {
                    StatusDescriptor = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1485", ""); //"Policy not eligible for the selected process";
                    ClsPolicyErrMsg.strMessage = StatusDescriptor;
                }
                return retval;
            }
            catch (Exception exc)
            {
                throw (exc);
            }

        }

        public bool StartProcess(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo,
            out string StatusDescriptor)
        {
            //Calling the base class start process methos which will
            //insert the record in POL_POLICY_PROCESS table
            //and will do the transaction log entry
            try
            {

                base.BeginTransaction();

                //Starting the process
                bool retval = StartCancellationProcess(objProcessInfo, out  StatusDescriptor);
                if (retval)
                {
                    //Commiting the database transaction
                    base.CommitTransaction();
                }
                else
                {
                    base.RollbackTransaction();
                }
                return retval;
            }
            catch (Exception exc)
            {
                base.RollbackTransaction();
                throw (exc);
            }

        }

        public bool StartAndGenerateNotice(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo,
            out string StatusDescriptor)
        {
            try
            {

                base.BeginTransaction();

                //Starting the process
                bool retval = StartCancellationProcess(objProcessInfo, out StatusDescriptor);
                if (retval)
                {

                    if (1 == 1)//GenerateNoticesForCancellation(objProcessInfo))//move at cancellation commit ,commented  by lalit.April 19 2011,i-track 1104
                    {
                        base.CommitTransaction();
                    }
                    else
                    {
                        StatusDescriptor += Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1486", ""); //" Error while generating canellation notice " ; 
                        base.RollbackTransaction();
                    }
                }
                else
                {
                    base.RollbackTransaction();
                }
                return retval;
            }
            catch (Exception exc)
            {
                base.RollbackTransaction();
                throw (exc);
            }

        }
        /// <summary>
        /// For Transaction entry ,send notice(pdf)
        /// </summary>
        public void AddTransactionAndNotice(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
        {

            try
            {
                //ClsProcessInfo objProcessInfo = new ClsProcessInfo();

                //				//start Transaction Log by kranti
                //				TransactionDescription.Append("\n Notice Page Entry has been done;");
                //
                //				base.WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, 
                //					GetTransactionLogDesc(objProcessInfo.PROCESS_ID) , objProcessInfo.COMPLETED_BY,TransactionDescription.ToString(),TransactionChangeXML.ToString());
                //
                //				base.CommitTransaction();
                //				//End Transaction Log


                //For send notice (pdf)

                //for taking bill_type 
                //				DataSet ds = GetAgencyPhoneNo(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID);
                //				string billType = ds.Tables[0].Rows[0]["BILL_TYPE"].ToString();
                string billType = objProcessInfo.BILL_TYPE;


                ClsPrintJobsInfo objPrintJobsInfo = GetPrintJobsValues(objProcessInfo);
                string stateCode = new Cms.BusinessLayer.BlProcess.ClsCommonPdfXML().SetStateCode("POLICY", objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
                string AgencyCode = GetPDFAgencyCode(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, "POLICY");

                objProcessInfo.INSURED = 1;
                objProcessInfo.AGENCY_PRINT = 0;
                if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NSF_NO_REPLACEMENT).ToString()) || objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NSF_REPLACE).ToString()))
                    GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_TEMP_NSF.ToString());
                else if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NON_PAYMENT).ToString()))
                {
                    if (billType == BILL_PAYMENT_DIRECT_BILL)
                        GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_TEMP_NONPAYDB.ToString());
                    else
                        GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_TEMP_NONPAY.ToString());
                }
                else //if(objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.COMPANY_REQUEST).ToString()))
                    GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_TEMP.ToString());

                objProcessInfo.INSURED = 0;
                objProcessInfo.AGENCY_PRINT = 1;
                if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NSF_NO_REPLACEMENT).ToString()) || objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NSF_REPLACE).ToString()))
                    GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_TEMP_NSF.ToString());
                else if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NON_PAYMENT).ToString()))
                {
                    if (billType == BILL_PAYMENT_DIRECT_BILL)
                        GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_TEMP_NONPAYDB.ToString());
                    else
                        GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_TEMP_NONPAY.ToString());
                }
                else //if(objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.COMPANY_REQUEST).ToString()))
                    GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_TEMP.ToString());
                //End send notice (pdf)
            }
            catch (Exception ex)
            {
                //base.RollbackTransaction();
                throw (ex);
            }

        }

        /// <summary>
        /// Gets the Agency Phone No.
        /// </summary>
        /// <param name="intCustomerID"></param>
        /// <param name="intPolicyID"></param>
        /// <param name="intPolicyVersionID"></param>
        /// <returns></returns>
        public DataSet GetAgencyPhoneNo(int intCustomerID, int intPolicyID, int intPolicyVersionID)
        {
            string strStoredProc = "Proc_GetAgencyPhoneNo";
            DataSet ds = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                objDataWrapper.AddParameter("@CustomerID", intCustomerID);
                objDataWrapper.AddParameter("@PolicyID", intPolicyID);
                objDataWrapper.AddParameter("@PolicyVersionID", intPolicyVersionID);
                ds = objDataWrapper.ExecuteDataSet(strStoredProc);
                return ds;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }


        //		public void PostReturnPremium(int CustomerID, int PolicyID, int PolicyVersionID, int CalcMethod, int RowID)
        //		{
        //			string		strStoredProc	=	"Proc_GetPolicyProcessReturnPremium";
        //			DateTime	RecordDate		=	DateTime.Now;
        //			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
        //		
        //			try
        //			{
        //				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
        //				objDataWrapper.AddParameter("@POLICY_ID",PolicyID);
        //				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID);
        //
        //			}
        //			catch(Exception ex)
        //			{
        //			}
        //		}
        /// <summary>
        /// Gets the Return Premium
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="CalcMethod"></param>
        /// <returns></returns>
        public int PostReturnPremium(int CustomerID, int PolicyID, int PolicyVersionID, int CalcMethod, int RowID, int PremiumAmount, int MCCAFee, int OtherFee, out string Description)
        {
            string strStoredProc = "Proc_GetPolicyProcessReturnPremium";
            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objDataWrapper.AddParameter("@CALCULATE_METHOD", CalcMethod);
                objDataWrapper.AddParameter("@ROW_ID", RowID);
                objDataWrapper.AddParameter("@PREMIUM_AMOUNT", PremiumAmount);
                objDataWrapper.AddParameter("@MCCA_FEE", MCCAFee);
                objDataWrapper.AddParameter("@OTHER_FEE", OtherFee);

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@TRAN_DESC", null, SqlDbType.VarChar, ParameterDirection.Output, 1000);

                int returnResult = 0;
                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

                Description = objSqlParameter.Value.ToString();

                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null) objDataWrapper.Dispose();
            }
        }
        //Ravindra(08-01-2007)  
        public override bool CommitProcess(ClsProcessInfo objProcessInfo)
        {
            string StatusDescriptor = "";
            return this.CommitProcess(objProcessInfo, out StatusDescriptor);
        }
        public bool CommitProcess(ClsProcessInfo objProcessInfo,
            out string StatusDescriptor)
        {
            StatusDescriptor = "";
            try
            {
                base.TransactionChangeXML.Length = 0;
                ClsPolicyErrMsg.strMessage = "";
                objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_CANCELLATION_PROCESS;

                base.BeginTransaction();
                if (base.CheckProcessEligibility(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID) != 1)
                {
                    base.RollbackTransaction();
                    ClsPolicyErrMsg.strMessage = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1279", "");//"This Process is not Eligible on This Policy."; 
                    StatusDescriptor = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1279", ""); //"This Process is not Eligible on This Policy."; 
                    return false;
                }

                bool retVal;
                decimal CarryForwardAmount = 0;
                string Desc = "";
                string billType = objProcessInfo.BILL_TYPE;//DB-8460				

                TransactionDescription.Append(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1374", "") + " : " + getCancellationTypeStringById(objProcessInfo.CANCELLATION_TYPE) + ";");

                //GetPolicyPremiumValues(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID,out PremiumAmout,out MCCAFee,out OtherFee);
                //int returnResult = PostReturnPremium(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID,objProcessInfo.CANCELLATION_OPTION,objProcessInfo.ROW_ID,PremiumAmout,MCCAFee,OtherFee,out Desc);

                //				I Have to add for modify diary Entry
                //				string strSubjectLine="cancellation subject ";
                //				string strNotes = /*strRulesStatusReason; //*/"The check box for refer to underwriter is checked off in  policy details's Renewal Instructions";					
                //				AddDiaryEntry(objProcessInfo,strSubjectLine,strNotes);
                //				TransactionDescription.Append("\n A change was to refer to underwriter in policy details's Renewal Instructions.;"); 
                //	


                //int NON_PAY = (int)enumPROCESS_CANCELLATION_TYPES.NON_PAYMENT;
                //if (PolicyTranType(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID)
                //    != (int)ClsPolicyProcess.enumPOLICY_TRANSACTION_TYPE.OPEN_POLICY
                //    //&& objProcessInfo.CANCELLATION_TYPE !=
                //    //(int)ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.NON_PAYMENT
                //    && objProcessInfo.RETURN_PREMIUM != 0
                //    )
              // {
                     #region Premium Posting
                    //Added by lalit Nov 30,2010

                    //Insert canceled return premium Installment for 
                    //Calcel Boletos
                    int calncelBoletos = CancelPolicyBoletos(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CREATED_BY, objProcessInfo.CANCELLATION_TYPE, objProcessInfo.RETURN_PREMIUM);
                    //Append cancellation transaction log
                    if (calncelBoletos > 2)
                    {
                        TransactionDescription.Append(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1375", "") + " : " + (calncelBoletos - 2).ToString() + ";");
                        //TransactionDescription.Append("Return Premium : " + objProcessInfo.RETURN_PREMIUM.ToString() + ";");
                    }

                    //post Co-Insurance premium
                    base.Insert_COInsurance_Premium(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CREATED_BY);

                    //commented by lalit for new implimentation according to discussion ,dec 7 2010
                    //int returnResult = base.PostCancellationPremium(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID,
                    //    objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.RETURN_PREMIUM, objProcessInfo.ROW_ID,
                    //    objProcessInfo.EFFECTIVE_DATETIME, out Desc, out CarryForwardAmount,
                    //    objProcessInfo.COMPLETED_BY, objProcessInfo.PROCESS_ID, objProcessInfo.CANCELLATION_TYPE,
                    //    objProcessInfo.CANCELLATION_OPTION);
                    //CarryForwardAmount = 0;

                    string TranDesc = "";
                    int ret = 0;
                    //Added By Lalit 
                    ret = base.PostOpenItems(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID, objProcessInfo.CREATED_BY, out TranDesc);
                    if (ret > 0)
                        TransactionDescription.Append(TranDesc);
                    // added by pravesh
                    #region updating Written and enforce premium for this version
                    //update Split Premium 
                    Cms.BusinessLayer.BlQuote.clsSplitPremium objSplitPremium = new Cms.BusinessLayer.BlQuote.clsSplitPremium();
                    objSplitPremium.UpdateWrittenEnforcePremiumSplit(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.PROCESS_ID.ToString(), objWrapper);
                    #endregion
                    // end here
                    //post Re-Insurance premium
                    base.Insert_ReInsurance_Premium(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID, objProcessInfo.CREATED_BY);

                    //reconcile policy open items
                    base.RecocileOpenItems(objProcessInfo);
                     
                    #endregion

                    //RecocileOpenItems

                    if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.CANCEL_REINSTATEMENT).ToString()))
                        objProcessInfo.CFD_AMT = Convert.ToDouble(CarryForwardAmount);
                    
                        
                
                //}

                //Update Status of Both Versions of policy As per cancellation effective date
                // If cancellation effective date is todays date or les set status of  policies as inactive & cancelled
                //if(objProcessInfo.EFFECTIVE_DATETIME <= System.DateTime.Now)
                if (DateTime.Compare(objProcessInfo.EFFECTIVE_DATETIME.Date, System.DateTime.Now.Date) <= 0)
                {
                    SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID,
                        objProcessInfo.POLICY_VERSION_ID, objProcessInfo.PROCESS_ID, POLICY_STATUS_INACTIVE);
                    SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID,
                        objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID, POLICY_STATUS_CANCELLED);
                    TransactionDescription.Append(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("526", ""));//"Policy status has been updated.;");
                    objProcessInfo.POLICY_CURRENT_STATUS = POLICY_STATUS_CANCELLED;

                }
                // Set status of base version on which cancellation is launched as Active (leave the previous
                // status intact & set status of new policy as Marked for Cancellation(SCANCEL)
                else
                {
                    SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID,
                        objProcessInfo.NEW_POLICY_VERSION_ID, POLICY_STATUS_SUSPENSE_CANCEL);
                    objProcessInfo.POLICY_CURRENT_STATUS = POLICY_STATUS_SUSPENSE_CANCEL;
                    TransactionDescription.Append(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("526", "") + ";");
                }



                retVal = base.CommitProcess(objProcessInfo);
                if (!retVal)
                {
                    base.RollbackTransaction();
                    //ClsPolicyErrMsg.strMessage ="Process could not be committed.";
                    StatusDescriptor = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1279", "");//"This Process is not Eligible on This Policy."; 
                    return false;
                }
                TransactionDescription.Append("\n " + Desc + ";");




                //add follow up in case of NSF Replacement/Non Payment
                if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NSF_REPLACE).ToString()) || objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NON_PAYMENT).ToString()))
                {
                    if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NSF_REPLACE).ToString()))
                        AddDiary(objProcessInfo, ((int)BlCommon.ClsDiary.enumDiaryType.CANCELLATION_REQUESTS_NSF).ToString());
                    else if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NON_PAYMENT).ToString()))
                        AddDiary(objProcessInfo, ((int)BlCommon.ClsDiary.enumDiaryType.CANCELLATION_REQUESTS_NONPAYMENT).ToString());
                }
                //Entring the record in notice generation
                //Cms.Model.Policy.Process.ClsPolicyProcessNoticInfo objProcessNoticeInfo = base.GetProcessNoticeInfo(objProcessInfo);
                //objProcessNoticeInfo.NOTICE_DESCRIPTION = "Return Premium or Cancellation be Printed for this Process";
                //AddProcessNotice(objProcessNoticeInfo, objWrapper);

                //TransactionDescription.Append("\n Notice Page Entry has been done;");				
                //	base.CommitTransaction();
                // GenerateCancellationNotices(objProcessInfo);

                //	base.BeginTransaction();			
                if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NON_PAYMENT).ToString()) && objProcessInfo.BILL_TYPE == BILL_PAYMENT_DIRECT_BILL)
                {
                    //					//if Cancellation Type is Non Payment DB, then system will charge late fees against this policy
                    //					BlCommon.Accounting.ClsAddNSFEntry objAddNSF= new Cms.BusinessLayer.BlCommon.Accounting.ClsAddNSFEntry();
                    //					int dblLateFees = GetLateFeesForPolicy(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID);
                    //					objAddNSF.ChargeLateFees(objWrapper,objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID,dblLateFees);
                    //					TransactionDescription.Append("Late Fees written.;");



                    //bool returnvalue=GenerateCancellationMemo(objProcessInfo); 
                    //if (!returnvalue)
                    //{
                    //    base.RollbackTransaction();
                    //    ClsPolicyErrMsg.strMessage ="Cancellation Memo could not be generated."; 
                    //    StatusDescriptor = "Error generating cancellation memo."; 
                    //    return false;	
                    //}
                    //else
                    //	TransactionDescription.Append("Cancellation Memo could not be generated.;");
                }
                //Added by Lalit March 31 ,2011.
                //i-track # 1039
                //genrate boleto for charged premium
                #region Genrate Policy B O L E T O for charged premium
                ClsProductPdfXml objProductPdfXml = new ClsProductPdfXml(this.objWrapper);
                objProductPdfXml.generatePolicyBoletos(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID
                    , objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.COMPLETED_BY, "", objProcessInfo.PROCESS_ID, objProcessInfo.ROW_ID, objProcessInfo.AGENCY_CODE
                    ,this.SystemID
                    );
                //objProcessInfo


                #endregion

                //Added By Lalit April 19 2011
                //genrate cancellation Notice if cancellation Notice was not genrated from button
                //cancellation notice will be genrated for Non Pay cancellation
                //i-track # 1104
                if (objProcessInfo.CANCELLATION_TYPE ==
                     (int)ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.NON_PAYMENT
                    && objProcessInfo.CANCELLATION_NOTICE_SENT.Trim() != "Y"
                    )
                {
                    if (GenerateNoticesForCancellation(objProcessInfo))
                    {
                        TransactionDescription.Append(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1279", "") + ";");
                    }

                }
               
                // generate Policy Documents on Policy cancellation same as Endorsement Cancellation refer iTrack 1449 -- added by Pravesh on 7 Dec 2011
                    objProductPdfXml.generateDocuments(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID
                    , objProcessInfo.NEW_POLICY_VERSION_ID, "PROCESS", objProcessInfo.COMPLETED_BY, objProcessInfo.PROCESS_ID, objProcessInfo.ROW_ID);

                base.WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,
                     GetTransactionLogDesc(objProcessInfo.PROCESS_ID), objProcessInfo.COMPLETED_BY, TransactionDescription.ToString(), TransactionChangeXML.ToString());



                #region Entry in Endorsement Log table
                ClsEndorsementDetailInfo objEndorsementInfo = PopulateEndorsementDetailInfo(objProcessInfo);
                objEndorsementInfo.ENDORSEMENT_STATUS = "COM";
                objEndorsementInfo.ENDORSEMENT_TYPE = objProcessInfo.ENDORSEMENT_TYPE;
                objEndorsementInfo.ENDORSEMENT_DESC = objProcessInfo.OTHER_REASON;
                //Updating the edorsement log
                //UpdateEndorsementLog(objEndorsementInfo);
                int EndorsementNo = 0;
                AddEndorsementLog(objEndorsementInfo, out EndorsementNo, objProcessInfo.PROCESS_ID.ToString());

                objEndorsementInfo.ENDORSEMENT_NO = EndorsementNo;
                string TranID = "";
                if (!IsEODProcess)
                {
                    if (System.Web.HttpContext.Current.Session["EndorsementTranIds"] != null)
                        TranID = System.Web.HttpContext.Current.Session["EndorsementTranIds"].ToString();
                }
                objEndorsementInfo.TRANS_ID = TranID;

                AddEndorsementLogDetails(objEndorsementInfo);
                TransactionDescription.Append("\n" + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1422", ""));
                objProcessInfo.ENDORSEMENT_NO = EndorsementNo;
                #endregion

                base.CommitTransaction();

                return true;
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw (ex);
            }
        }
        /// <summary>
        /// Add diary Entry for cancellation process
        /// </summary>
        /// <returns></returns>
        //		public void AddDiaryEntry(ClsProcessInfo objProcessInfo,string strSubjectLine,string strNotes)
        //		{
        //			try
        //			{
        //				Cms.BusinessLayer.BlCommon.ClsDiary objClsDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
        //				Cms.Model.Diary.TodolistInfo objModel = new Cms.Model.Diary.TodolistInfo();
        //				objModel.SUBJECTLINE = strSubjectLine; //"A change was to refer to underwriter in policy details's Renewal Instructions ";
        //				objModel.NOTE =   strNotes; // "The check box for refer to underwriter is checked off in  policy details's Renewal Instructions";					
        //				objModel.LISTTYPEID =15;
        //				objModel.POLICY_ID = objProcessInfo.POLICY_ID;
        //				objModel.POLICY_VERSION_ID = objProcessInfo.POLICY_VERSION_ID;
        //				objModel.CUSTOMER_ID = objProcessInfo.CUSTOMER_ID;
        //				objModel.CREATED_BY = objProcessInfo.CREATED_BY;
        //				objModel.CREATED_DATETIME = DateTime.Now;
        //				objModel.RECDATE = DateTime.Now;
        //				objModel.FOLLOWUPDATE = DateTime.Now;
        //				objModel.LAST_UPDATED_DATETIME =DateTime.Now;
        //				objModel.RECDATE = DateTime.Now;
        //				objModel.STARTTIME = (System.DateTime)DateTime.Now;
        //				objModel.ENDTIME = (System.DateTime)DateTime.Now;	
        //				objModel.RULES_VERIFIED =1;
        //				objClsDiary.Add(objModel,"");
        //			}
        //			catch(Exception ex)
        //			{
        //				throw(ex); 
        //			}
        //			
        //		}

        //		public int PostCancellationPremium(int CustomerID,int PolicyID, int PolicyVersionID,
        //				double CancellationPremium,int SourceRow_ID,
        //			DateTime ChangeEffectiveDate,out string Description,out decimal CarryForwardAmount,int CommittedBy)
        //
        //		{
        //
        //			CarryForwardAmount = 0;
        //			ClsCancellationRule objRule = GetCancellationRule( CustomerID,PolicyID,PolicyVersionID,objWrapper);
        //
        //			string		strStoredProc	=	"Proc_PostPolicyProcessReturnPremium";
        //	
        //			try
        //			{
        //				objWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
        //				objWrapper.AddParameter("@POLICY_ID",PolicyID);
        //				objWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID);
        //				objWrapper.AddParameter("@CANCELLATION_PREMIUM",CancellationPremium);
        //				objWrapper.AddParameter("@ACTION_UNPAID_PREMIUM",objRule.ACTION_UNPAID_PREMIUM );
        //				objWrapper.AddParameter("@ACTION_RETURN_PREMIUM",objRule.ACTION_RETURN_PREMIUM );
        //				objWrapper.AddParameter("@BILL_TYPE",objRule.BILL_TYPE );
        //				objWrapper.AddParameter("@CHANGE_EFFECTIVE_DATE ",ChangeEffectiveDate );
        //				objWrapper.AddParameter("@SOURCE_ROW_ID",SourceRow_ID);
        //				objWrapper.AddParameter("@USER_ID",CommittedBy );
        //				
        //				SqlParameter objDesc  = (SqlParameter) objWrapper.AddParameter("@TRANSACTION_DESC",null,SqlDbType.VarChar,ParameterDirection.Output,1000);
        //				SqlParameter objCFAmt  = (SqlParameter) objWrapper.AddParameter("@CFG_PREMIUM_AMOUNT",null,SqlDbType.Decimal,ParameterDirection.Output); 
        //
        //				int returnResult = 0;
        //				returnResult	= objWrapper.ExecuteNonQuery(strStoredProc);
        //
        //				Description = objDesc.Value.ToString();
        //				CarryForwardAmount = Convert.ToDecimal(objCFAmt.Value);
        //
        //				return returnResult;
        //			}
        //			catch(Exception ex)
        //			{
        //				throw(ex);
        //			}
        //		}

        public bool RollbackProcess(ClsProcessInfo objProcessInfo, bool IsManual)
        {

            try
            {
                base.BeginTransaction();
                if (objProcessInfo.CANCELLATION_NOTICE_SENT == "Y")
                {
                    //Waive unpaid Late Fees
                    ClsFeeReversal objFees = new ClsFeeReversal();
                    objFees.ReverseLateFees(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID
                        , objWrapper);

                    //Ravindra(08-27-2008): Update due dates of open items if manual rollback of Non Pay Cancellation
                    if (IsManual == true && objProcessInfo.CANCELLATION_TYPE == (int)enumPROCESS_CANCELLATION_TYPES.NON_PAYMENT)
                    {
                        objWrapper.ClearParameteres();
                        objWrapper.AddParameter("@CUSTOMER_ID", objProcessInfo.CUSTOMER_ID);
                        objWrapper.AddParameter("@POLICY_ID", objProcessInfo.POLICY_ID);
                        objWrapper.AddParameter("@POLICY_VERSION_ID", objProcessInfo.NEW_POLICY_VERSION_ID);
                        objWrapper.ExecuteNonQuery("Proc_UpdateDueDatesAtCancelRollback");
                        objWrapper.ClearParameteres();

                    }

                    GenerateReinstatementNotices(objProcessInfo);
                }
                //Updating customer balance info: //Moved here from GenerateReinstatementNotices function, Charles (18-Nov-09), Itrack 6648
                UpdatePolicyVersionForActCustBalance(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.NEW_POLICY_VERSION_ID);

                base.WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID,
                    GetTransactionLogDesc(objProcessInfo.PROCESS_ID), objProcessInfo.COMPLETED_BY, TransactionDescription.ToString());
                //base.CommitTransaction();				

                //base.BeginTransaction();


                //Delete New version 
                base.DeletePolicyVersion(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID);
                base.RollbackProcess(objProcessInfo, "NEWVERSION");
                base.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw (ex);
                //return false;
            }
        }


        public override bool RollbackProcess(ClsProcessInfo objProcessInfo)
        {
            return RollbackProcess(objProcessInfo, false);
        }
        /// <summary>
        /// Generate Cancelltion Memo in case of Non Payment DB cancellation type
        /// </summary>
        /// <param name="objProcessInfo"></param>
        /// <returns></returns>
        public bool GenerateCancellationMemo(ClsProcessInfo objProcessInfo)
        {
            string strInsuredFile = "", strAgencyFile = "", strAddIntFile = "";
            int tmpInsured = objProcessInfo.INSURED;
            int tmpAgencyPrint = objProcessInfo.AGENCY_PRINT;
            int oldVersion = objProcessInfo.POLICY_VERSION_ID;
            objProcessInfo.POLICY_VERSION_ID = objProcessInfo.NEW_POLICY_VERSION_ID;
            ClsPolicyProcess.PrintingErrorFlag = false;
            base.BeginTransaction();
            try
            {
                #region //generating PDF Memo for additional interest
                if (objProcessInfo.SEND_ALL == (int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.YES))
                    getAdditionalInterest(objProcessInfo, objWrapper);
                bool IsAddIntExists = IsAddIntExist(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.LOB_ID.ToString(), "POLICY", objWrapper);
                if (IsAddIntExists)
                    strAddIntFile = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_NONPAYDBMEMO_ADDLINT.ToString());
                #endregion
                #region //generating PDF for Customer
                objProcessInfo.INSURED = 1;
                objProcessInfo.AGENCY_PRINT = 0;
                strInsuredFile = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_NONPAYDBMEMO.ToString());
                #endregion
                #region//generating PDF Memo for Agency
                objProcessInfo.INSURED = 0;
                objProcessInfo.AGENCY_PRINT = 1;
                strAgencyFile = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_NONPAYDBMEMO.ToString());
                #endregion
                #region Add Print Jobs for Cancellation Memo
                objProcessInfo.INSURED = tmpInsured;
                objProcessInfo.AGENCY_PRINT = tmpAgencyPrint;
                //ClsPrintJobsInfo objPrintJobsInfo = GetPrintJobsValues(objProcessInfo);
                //objPrintJobsInfo.URL_PATH = ClsPolicyProcess.PRINT_JOBS_URL_PATH + objProcessInfo.AGENCY_CODE + "/" + objProcessInfo.CUSTOMER_ID.ToString() + "/" + "POLICY/CANC_NOTICE" + "/" + "final";

                //Ravindra(04-14-2008): Agency Code already fetched in GetPrintJobsValues
                ClsPrintJobsInfo objPrintJobsInfo = GetPrintJobsValues(objProcessInfo, "CANC_NOTICE");
                if (objProcessInfo.PRINTING_OPTIONS == (int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.NO))
                {
                    #region //adding job for Insured/Customer in printing table
                    if ((strInsuredFile != "" && objProcessInfo.INSURED != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED).ToString())))
                    {
                        objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER;
                        objPrintJobsInfo.FILE_NAME = strInsuredFile;
                        if (objProcessInfo.INSURED == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString()))
                            objPrintJobsInfo.ONDEMAND_FLAG = "Y";
                        else
                            objPrintJobsInfo.ONDEMAND_FLAG = "N";
                        objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL", "ALL", "Insured", "NONPAYDBMEMO");

                        AddPrintJobs(objPrintJobsInfo);
                    }
                    #endregion
                    #region //adding print job for Agency in print table
                    if (objProcessInfo.AGENCY_PRINT != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED).ToString()))
                    {
                        objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_AGENCY;
                        objPrintJobsInfo.FILE_NAME = strAgencyFile;
                        if (objProcessInfo.AGENCY_PRINT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString()))
                            objPrintJobsInfo.ONDEMAND_FLAG = "Y";
                        else
                            objPrintJobsInfo.ONDEMAND_FLAG = "N";
                        objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL", "ALL", "Agent", "NONPAYDBMEMO");
                        AddPrintJobs(objPrintJobsInfo);
                    }
                    #endregion
                    #region//additional Interest in print job table
                    if (IsAddIntExists == true && objProcessInfo.ADD_INT != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED).ToString()))
                    {
                        if (objProcessInfo.ADD_INT_ID != "")
                        {
                            string strAddlInt = "";
                            objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_ADDL_INTEREST;
                            objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL", "ALL", "ADDLINT", "NONPAYDBMEMO");
                            if (objProcessInfo.ADD_INT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString()))
                                objPrintJobsInfo.ONDEMAND_FLAG = "Y";
                            else
                                objPrintJobsInfo.ONDEMAND_FLAG = "N";
                            string[] addIntIdArr = objProcessInfo.ADD_INT_ID.Split('~');
                            if (addIntIdArr != null && addIntIdArr.Length > 0)
                            {
                                for (int jCounter = 0; jCounter < addIntIdArr.Length; jCounter++)
                                {
                                    string[] strArr = addIntIdArr[jCounter].Split('^');
                                    if (strArr == null || strArr.Length < 1)
                                        continue;

                                    strAddlInt = strArr[0].ToString() + "_" + objPrintJobsInfo.CUSTOMER_ID.ToString() + "_" + objPrintJobsInfo.POLICY_ID.ToString() + "_" + objPrintJobsInfo.POLICY_VERSION_ID.ToString() + "_" + strArr[1].ToString() + "_" + strArr[2].ToString();
                                    objPrintJobsInfo.FILE_NAME = FindAddIntFileName(strAddIntFile, strAddlInt);
                                    objPrintJobsInfo.DUPLEX = "Y";
                                    AddPrintJobs(objPrintJobsInfo);
                                }
                            }

                        }
                    }
                    #endregion
                }
                #endregion
                objProcessInfo.POLICY_VERSION_ID = oldVersion;
                return true;
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                ClsPolicyProcess.PrintingErrorFlag = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return false;
            }
        }

        /**************************Private Member function to Generate Canc Notices *************/

        private bool GenerateNoticesForCancellation(ClsProcessInfo objProcessInfo)
        {
            int retval;
            // added by pravesh on 19 jan 11, 
            ClsProductPdfXml objProductPdfXml = new ClsProductPdfXml(this.objWrapper);
            try
            {
                objProductPdfXml.generateCancellationDocuments(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, "PROCESS", objProcessInfo.CREATED_BY, objProcessInfo.PROCESS_ID, objProcessInfo.ROW_ID);
                objProcessInfo.CANCELLATION_NOTICE_SENT = "Y";
                retval = base.UpdateProcessCancelNoticeInformation(objProcessInfo);

                if (retval > -1)
                {
                    base.UpdateDiaryEntryFollowUpDate(objProcessInfo, "");
                    return true;
                }
                else
                {
                    objProcessInfo.CANCELLATION_NOTICE_SENT = "N";
                    return false;
                }
            }
            catch (Exception ex)
            {
                System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while generating cancellation notices");
                addInfo.Add("CustomerID", objProcessInfo.CUSTOMER_ID.ToString());
                addInfo.Add("PolicyID", objProcessInfo.POLICY_ID.ToString());
                ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex, addInfo);
                throw ex;
            }

            // return true;
            string strInsuredFile = "", strAgencyFile = "", strAddIntFile = "", strFileCOMAI = "", strFileCLTIN = "";
            string billType = objProcessInfo.BILL_TYPE;
            int tmpInsured = objProcessInfo.INSURED;
            int tmpAgencyPrint = objProcessInfo.AGENCY_PRINT;
            int oldVersion = objProcessInfo.POLICY_VERSION_ID;
            objProcessInfo.POLICY_VERSION_ID = objProcessInfo.NEW_POLICY_VERSION_ID;
            ClsPolicyProcess.PrintingErrorFlag = false;
            DocumentName = DocumentCode(objProcessInfo);


            try
            {
                if (objProcessInfo.SEND_ALL == (int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.YES))
                    getAdditionalInterest(objProcessInfo, objWrapper);
                string strTemp = objProcessInfo.ADD_INT_ID;
                bool IsAddIntExists = IsAddIntExist(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.LOB_ID.ToString(), "POLICY", objWrapper);
                //Changed by Ravindra(03-13-2008): To Generate Pas Due Notices for Non Pay DB cancellation
                //Code below (in else part) is no longer applicable for Non Pay DB Notices
                if (objProcessInfo.CANCELLATION_TYPE == (int)enumPROCESS_CANCELLATION_TYPES.NON_PAYMENT && billType == BILL_PAYMENT_DIRECT_BILL)
                {
                    ClsNotices objNotice = new ClsNotices();
                    strInsuredFile = objNotice.GeneratePastDueNoticeForInsured(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objWrapper);

                    strAgencyFile = objNotice.GeneratePastDueNoticeForAgency(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objWrapper);

                    if (IsAddIntExists)
                    {
                        strAddIntFile = objNotice.GeneratePastDueNoticeForAdditionalInterests(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objWrapper);
                    }
                }
                else
                {

                    //Generating PDF for additional interest 

                    if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NON_PAYMENT).ToString()))
                    {
                        if (billType == BILL_PAYMENT_DIRECT_BILL)
                        {
                            //if Cancellation Type is Non Payment DB, then system will charge late fees against this policy
                            BlCommon.Accounting.ClsAddNSFEntry objAddNSF = new Cms.BusinessLayer.BlCommon.Accounting.ClsAddNSFEntry();
                            int dblLateFees = GetLateFeesForPolicy(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID);
                            objAddNSF.ChargeLateFees(objWrapper, objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, dblLateFees);
                            //TransactionDescription.Append("Late Fees written.;");

                            strAddIntFile = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_NONPAYDB_ADDLINT.ToString());
                        }
                        else
                            strAddIntFile = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_ADDLINT.ToString());
                    }
                    else if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.INSURED_REQUEST).ToString()))
                        strAddIntFile = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_ADDLINT_INSREQ.ToString());
                    else if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.CANCEL_REINSTATEMENT).ToString())
                                || objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.CANCEL_REWRITE).ToString())
                            )
                        strAddIntFile = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_ADDLINT_INSREQ.ToString());
                    else
                        strAddIntFile = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_ADDLINT.ToString());
                    //generating COMAI Notice for Company Cancellation request.
                    //this condition changed as per itrack 4237 , remove for can/rewrite and can/reinstate
                    if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.COMPANY_REQUEST).ToString())
                        //	|| objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.CANCEL_REINSTATEMENT).ToString()) 
                        //	|| objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.CANCEL_REWRITE).ToString()) 
                        || objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.RESCINDED).ToString())
                        )
                    {
                        strFileCOMAI = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_ADDLINT_CERTMAIL.ToString());
                        strFileCLTIN = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_CERTMAIL.ToString());
                    }

                    //generating PDF for Agency
                    objProcessInfo.INSURED = 0;
                    objProcessInfo.AGENCY_PRINT = 1;
                    if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NSF_NO_REPLACEMENT).ToString()))
                        strAgencyFile = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_NSF_NOREPLACE.ToString());
                    else if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NSF_REPLACE).ToString()))
                        strAgencyFile = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_NSF.ToString());
                    else if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NON_PAYMENT).ToString()))
                    {
                        if (billType == BILL_PAYMENT_DIRECT_BILL)
                        {
                            strAgencyFile = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_NONPAYDB.ToString());
                        }
                        else
                            strAgencyFile = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_NONPAY.ToString());
                    }
                    else if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.INSURED_REQUEST).ToString()))
                        strAgencyFile = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_INSREQ.ToString());
                    else if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.AGENTS_REQUEST).ToString()))
                        strAgencyFile = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_AGENT.ToString());

                    else if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.CANCEL_REINSTATEMENT).ToString())
                        || objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.CANCEL_REWRITE).ToString())
                        )
                        strAgencyFile = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_INSREQ.ToString());
                    else
                        strAgencyFile = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL.ToString());


                    //generating PDF for Customer
                    objProcessInfo.INSURED = 1;
                    objProcessInfo.AGENCY_PRINT = 0;
                    if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NSF_NO_REPLACEMENT).ToString()))
                        strInsuredFile = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_NSF_NOREPLACE.ToString());
                    else if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NON_PAYMENT).ToString()))
                    {
                        if (billType == BILL_PAYMENT_DIRECT_BILL)
                        {
                            // genrate cancelation notice for non pay db
                            strInsuredFile = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_NONPAYDB.ToString());

                        }
                        else
                        {
                            strInsuredFile = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_NONPAY.ToString());
                            //strFileCOMIN=GeneratePDF(objProcessInfo,);
                        }
                    }
                    else if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.INSURED_REQUEST).ToString()))
                    {
                        //in case of insured request no document will be send to insured
                        //GeneratePDF(objProcessInfo,enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_TEMP_INSREQ.ToString());
                    }
                    else if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NSF_REPLACE).ToString()))
                    {
                        //no Copy
                    }
                    else if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.AGENTS_REQUEST).ToString()))
                        strInsuredFile = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_AGENT.ToString());

                    else if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.CANCEL_REINSTATEMENT).ToString())
                        || objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.CANCEL_REWRITE).ToString())
                        )
                        strInsuredFile = "";//GeneratePDF(objProcessInfo,enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL_NONPAYDBMEMO.ToString()); commented as per Itrack 4419 and 4237 and asked by Rajan

                    else if (objProcessInfo.CANCELLATION_TYPE != int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.INSURED_REQUEST).ToString()))
                        strInsuredFile = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.CANC_NOTICE_FINAL.ToString());
                }



                //Add Print Jobs for Cancellation Notice
                objProcessInfo.INSURED = tmpInsured;
                objProcessInfo.AGENCY_PRINT = tmpAgencyPrint;
                //Ravindra(03-28-2008): Agency ID already fetched in GetPrintJobsValues
                ClsPrintJobsInfo objPrintJobsInfo = GetPrintJobsValues(objProcessInfo, "CANC_NOTICE");
                //objPrintJobsInfo.URL_PATH = ClsPolicyProcess.PRINT_JOBS_URL_PATH + objProcessInfo.AGENCY_CODE + "/" + objProcessInfo.CUSTOMER_ID.ToString() + "/" + "POLICY/CANC_NOTICE" + "/" + "final";
                if (objProcessInfo.PRINTING_OPTIONS == (int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.NO))
                {
                    #region //adding job for Insured/Customer in printing table
                    // do entry in print job other than No print required --chnanged on 3 april 2008
                    //if((strInsuredFile!="" && objProcessInfo.INSURED == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL).ToString())) || objProcessInfo.INSURED == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString()))
                    if ((strInsuredFile != "" && objProcessInfo.INSURED != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED).ToString())))
                    {
                        objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER;
                        objPrintJobsInfo.FILE_NAME = strInsuredFile;
                        if (objProcessInfo.INSURED == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString()))
                            objPrintJobsInfo.ONDEMAND_FLAG = "Y";
                        else
                            objPrintJobsInfo.ONDEMAND_FLAG = "N";

                        if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NSF_NO_REPLACEMENT).ToString()))  //|| objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NSF_REPLACE).ToString()))
                            objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL", "ALL", "Insured", "NSF");
                        else if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NON_PAYMENT).ToString()))
                        {
                            if (billType == BILL_PAYMENT_DIRECT_BILL)
                                objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL", "ALL", "Insured", "NONPAYDB");
                            else
                                objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL", "ALL", "Insured", "NONPAY");
                        }
                        else
                            objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode(BlCommon.ClsCommon.GetLobCodeForLobId(objProcessInfo.LOB_ID.ToString()), objProcessInfo.STATE_CODE, "Insured", "CANC");

                        AddPrintJobs(objPrintJobsInfo);
                        //adding CLTIN 
                        if (strFileCLTIN != "")
                        {
                            objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER;
                            objPrintJobsInfo.FILE_NAME = strFileCLTIN;
                            objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL", "ALL", "Client", "CERT_MAIL");
                            AddPrintJobs(objPrintJobsInfo);
                        }

                    }
                    #endregion
                    #region //adding print job for Agency in print table
                    // do entry in print job other than No print required --chnanged on 3 april 2008
                    if (objProcessInfo.AGENCY_PRINT != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED).ToString()))
                    {
                        objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_AGENCY;
                        objPrintJobsInfo.FILE_NAME = strAgencyFile;
                        if (objProcessInfo.AGENCY_PRINT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString()))
                            objPrintJobsInfo.ONDEMAND_FLAG = "Y";
                        else
                            objPrintJobsInfo.ONDEMAND_FLAG = "N";
                        if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NSF_NO_REPLACEMENT).ToString()) || objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NSF_REPLACE).ToString()))
                            objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL", "ALL", "Agent", "NSF");
                        else if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NON_PAYMENT).ToString()))
                        {
                            if (billType == BILL_PAYMENT_DIRECT_BILL)
                                objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL", "ALL", "Agent", "NONPAYDB");
                            else
                                objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL", "ALL", "Agent", "NONPAY");
                        }
                        else
                            objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode(BlCommon.ClsCommon.GetLobCodeForLobId(objProcessInfo.LOB_ID.ToString()), objProcessInfo.STATE_CODE, "Agent", "CANC");

                        //objPrintJobsInfo.URL_PATH+= ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_CANC_NOTICE;
                        AddPrintJobs(objPrintJobsInfo);
                    }
                    #endregion
                    #region//additional Interest in print job table
                    // do entry in print job other than No print required --chnanged on 3 april 2008
                    if ((objProcessInfo.ADD_INT != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED).ToString())) && IsAddIntExists == true)
                    {
                        if (objProcessInfo.ADD_INT_ID != "")
                        {
                            string strAddlInt = "";
                            objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_ADDL_INTEREST;
                            if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NON_PAYMENT).ToString())
                                    && billType == BILL_PAYMENT_DIRECT_BILL)
                            {
                                objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL", "ALL", "ADDLINT", "NONPAYDB");
                            }
                            else
                            {
                                objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL", "ALL", "ADDLINT", "CANC");
                            }
                            if (objProcessInfo.ADD_INT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString()))
                                objPrintJobsInfo.ONDEMAND_FLAG = "Y";
                            else
                                objPrintJobsInfo.ONDEMAND_FLAG = "N";
                            string[] addIntIdArr = objProcessInfo.ADD_INT_ID.Split('~');
                            if (addIntIdArr != null && addIntIdArr.Length > 0)
                            {
                                for (int jCounter = 0; jCounter < addIntIdArr.Length; jCounter++)
                                {
                                    string[] strArr = addIntIdArr[jCounter].Split('^');
                                    if (strArr == null || strArr.Length < 1)
                                        continue;

                                    strAddlInt = strArr[0].ToString() + "_" + objPrintJobsInfo.CUSTOMER_ID.ToString() + "_" + objPrintJobsInfo.POLICY_ID.ToString() + "_" + objPrintJobsInfo.POLICY_VERSION_ID.ToString() + "_" + strArr[1].ToString() + "_" + strArr[2].ToString();
                                    objPrintJobsInfo.FILE_NAME = FindAddIntFileName(strAddIntFile, strAddlInt);
                                    AddPrintJobs(objPrintJobsInfo);
                                    //adding entry for COMAI notice
                                    if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.COMPANY_REQUEST).ToString())
                                        //										|| objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.CANCEL_REINSTATEMENT).ToString())
                                        //										||objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.CANCEL_REWRITE).ToString())
                                        || objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.RESCINDED).ToString())
                                        )
                                    {
                                        objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_ADDL_INTEREST;
                                        objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL", "ALL", "ADDLINT", "CERT_MAIL");
                                        objPrintJobsInfo.FILE_NAME = FindAddIntFileName(strFileCOMAI, strAddlInt); //strFileCOMAI; 
                                        AddPrintJobs(objPrintJobsInfo);
                                    }

                                }
                            }

                        }
                    }
                    #endregion
                }


                objProcessInfo.POLICY_VERSION_ID = oldVersion;
                //updating Process log for cancelation Notices
                objProcessInfo.CANCELLATION_NOTICE_SENT = "Y";
                retval = base.UpdateProcessCancelNoticeInformation(objProcessInfo);

                if (retval > -1)
                {
                    base.UpdateDiaryEntryFollowUpDate(objProcessInfo, "");
                    return true;
                }
                else
                {
                    objProcessInfo.CANCELLATION_NOTICE_SENT = "N";
                    return false;
                }
            }
            catch (Exception ex)
            {
                System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
                addInfo.Add("Err Descriptor ", "Error while generating cancellation notices");
                addInfo.Add("CustomerID", objProcessInfo.CUSTOMER_ID.ToString());
                addInfo.Add("PolicyID", objProcessInfo.POLICY_ID.ToString());
                ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex, addInfo);

                ClsPolicyProcess.PrintingErrorFlag = true;
                return false;
            }
        }

        /************************* END ************************************************************/
        public bool GenerateCancellationNotices(ClsProcessInfo objProcessInfo)
        {

            base.BeginTransaction();
            try
            {
                if (GenerateNoticesForCancellation(objProcessInfo))
                {
                    base.CommitTransaction();
                    return true;
                }
                else
                {
                    base.RollbackTransaction();
                    return false;
                }
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return false;
            }

        }
        public bool GenerateReinstatementNotices(ClsProcessInfo objProcessInfo)
        {
            int oldVersion = objProcessInfo.POLICY_VERSION_ID;
            objProcessInfo.POLICY_VERSION_ID = objProcessInfo.NEW_POLICY_VERSION_ID;
            //Add entry to prints table for reinstate notice when cancellation type is company request
            if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.COMPANY_REQUEST).ToString())
                || objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.CANCEL_REINSTATEMENT).ToString())
                || objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.CANCEL_REWRITE).ToString())
                || objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NSF_REPLACE).ToString())
                || objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NSF_NO_REPLACEMENT).ToString())
                || objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NON_PAYMENT).ToString()))
            {

                ClsPrintJobsInfo objPrintJobsInfo = GetPrintJobsValues(objProcessInfo, "REINS_NOTICE");
                string stateCode = objProcessInfo.STATE_CODE;
                string AgencyCode = objProcessInfo.AGENCY_CODE;
                objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL", "ALL", "Insured", "REINS");
                //objPrintJobsInfo.URL_PATH = ClsPolicyProcess.PRINT_JOBS_URL_PATH + AgencyCode + "/" + objProcessInfo.CUSTOMER_ID.ToString() + "/" + "POLICY/REINS_NOTICE" + "/" + "final";
                //objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL","ALL","Agent","REINS");
                int tmpInsured = objProcessInfo.INSURED;
                int tmpAgency = objProcessInfo.AGENCY_PRINT;

                string fileNameInsured = "";
                string fileNameAgency = "";
                //generating NOTICE for Customer/Insured
                //Generate notices for case other than NonPayment-DB
                if (!((objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NON_PAYMENT).ToString())) && objProcessInfo.BILL_TYPE == BILL_PAYMENT_DIRECT_BILL))
                {
                    objProcessInfo.INSURED = 1;
                    objProcessInfo.AGENCY_PRINT = 0;
                    fileNameInsured = GeneratePDF(objProcessInfo, "REINS_NOTICE");
                    //generating NOTICE for Agency
                    objProcessInfo.INSURED = 0;
                    objProcessInfo.AGENCY_PRINT = 1;
                    fileNameAgency = GeneratePDF(objProcessInfo, "REINS_NOTICE");
                    //adding Entry in print job table for customer/insured
                    objProcessInfo.INSURED = tmpInsured;
                    objProcessInfo.AGENCY_PRINT = tmpAgency;
                    if (objProcessInfo.INSURED != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED).ToString()))
                    {
                        objPrintJobsInfo.FILE_NAME = fileNameInsured;
                        objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER;
                        AddPrintJobs(objPrintJobsInfo);
                    }
                    //adding Entry in print job table for agency
                    if (objProcessInfo.AGENCY_PRINT != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED).ToString()))
                    {
                        objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL", "ALL", "Agent", "REINS");
                        objPrintJobsInfo.FILE_NAME = fileNameAgency;
                        objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_AGENCY;
                        AddPrintJobs(objPrintJobsInfo);
                    }
                }
                //by pravesh for all additional interst
                //if (objProcessInfo.ADD_INT_ID !="") commented as REinstatement Notices will be generated in either case
                //{
                string strAddlInt = "", fileNameAddInt = "";
                if (objProcessInfo.SEND_ALL == (int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.YES))
                    getAdditionalInterest(objProcessInfo, objWrapper);
                bool IsAddIntExists = IsAddIntExist(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.LOB_ID.ToString(), "POLICY");
                if (IsAddIntExists)
                    fileNameAddInt = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.REINS_NOTICE_FINAL_ADDLINT.ToString());
                #region//additional Interest in print job table

                objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_ADDL_INTEREST;
                objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL", "ALL", "ADDLINT", "REINS");
                // do entry in print job other than No print required --chnanged on 3 april 2008
                if (IsAddIntExists == true && fileNameAddInt != "" && (objProcessInfo.ADD_INT != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.NOT_REQUIRED).ToString())))
                {
                    if (objProcessInfo.ADD_INT_ID != "")
                    {
                        if (objProcessInfo.ADD_INT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString()))
                            objPrintJobsInfo.ONDEMAND_FLAG = "Y";
                        else
                            objPrintJobsInfo.ONDEMAND_FLAG = "N";
                        string[] addIntIdArr = objProcessInfo.ADD_INT_ID.Split('~');
                        if (addIntIdArr != null && addIntIdArr.Length > 0)
                        {
                            for (int jCounter = 0; jCounter < addIntIdArr.Length; jCounter++)
                            {
                                string[] strArr = addIntIdArr[jCounter].Split('^');
                                if (strArr == null || strArr.Length < 1)
                                    continue;
                                strAddlInt = strArr[0].ToString() + "_" + objPrintJobsInfo.CUSTOMER_ID.ToString() + "_" + objPrintJobsInfo.POLICY_ID.ToString() + "_" + objPrintJobsInfo.POLICY_VERSION_ID.ToString() + "_" + strArr[1].ToString() + "_" + strArr[2].ToString();
                                objPrintJobsInfo.FILE_NAME = FindAddIntFileName(fileNameAddInt, strAddlInt);
                                AddPrintJobs(objPrintJobsInfo);

                            }
                        }
                    }
                }

                #endregion




            }
            objProcessInfo.POLICY_VERSION_ID = oldVersion;
            //Updating customer balance info: //Moved to RollbackProcess function, Charles (18-Nov-09), Itrack 6648
            //UpdatePolicyVersionForActCustBalance(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID,objProcessInfo.NEW_POLICY_VERSION_ID);

            return true;
        }
        /// <summary>
        /// adding diary entry for follow up
        /// </summary>
        /// <param name="objProcessInfo"></param>
        /// <param name="strCalledFor"></param>
        public void AddDiary(ClsProcessInfo objProcessInfo, string strCalledFor)
        {
            Cms.Model.Diary.TodolistInfo objTodo = new Cms.Model.Diary.TodolistInfo();
            objTodo.CUSTOMER_ID = objProcessInfo.CUSTOMER_ID;
            objTodo.POLICY_ID = objProcessInfo.POLICY_ID;
            objTodo.POLICY_VERSION_ID = objProcessInfo.NEW_POLICY_VERSION_ID;
            if (strCalledFor == ((int)BlCommon.ClsDiary.enumDiaryType.CANCELLATION_REQUESTS_NSF).ToString())
            {
                objTodo.LISTTYPEID = (int)BlCommon.ClsDiary.enumDiaryType.CANCELLATION_REQUESTS_NSF;
                objTodo.SUBJECTLINE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1950","");//"Commit Cancellation / NSF";
                objTodo.NOTE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1950", "");//"Commit Cancellation / NSF";
            }
            else if (strCalledFor == ((int)BlCommon.ClsDiary.enumDiaryType.CANCELLATION_REQUESTS_NONPAYMENT).ToString())
            {
                objTodo.LISTTYPEID = (int)BlCommon.ClsDiary.enumDiaryType.CANCELLATION_REQUESTS_NONPAYMENT;
                objTodo.SUBJECTLINE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1949", "");//"Commit Cancellation / Non Payment";
                objTodo.NOTE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1949", "");//"Commit Cancellation / Non Payment";
            }
            //objTodo.RECDATE =System.DateTime.Now;
            objTodo.MODULE_ID = (int)BlCommon.ClsDiary.enumModuleMaster.POLICY_PROCESS;
            objTodo.LISTOPEN = "Y";
            //			if(IsEODProcess)
            //				objTodo.FROMUSERID = EODUserID; 
            //			else 
            if (objProcessInfo.COMPLETED_BY != 0)
                objTodo.FROMUSERID = objProcessInfo.COMPLETED_BY;
            else
                objTodo.FROMUSERID = objProcessInfo.CREATED_BY;
            objTodo.RECDATE = DateTime.Now;
            objTodo.LOB_ID = objProcessInfo.LOB_ID;
            objTodo.FOLLOWUPDATE = objProcessInfo.EFFECTIVE_DATETIME;

            if (IsEODProcess)
                objTodo.CREATED_BY = EODUserID;
            else
                objTodo.CREATED_BY = objProcessInfo.CREATED_BY;

            Cms.BusinessLayer.BlCommon.ClsDiary objDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
            //ArrayList alresult=new ArrayList(); 
            try
            {
                //alresult=
                objDiary.DiaryEntryfromSetup(objTodo, objWrapper);
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
        private string getCancellationTypeStringById(int CancellationTypeID)
        {
            string cancellationString = "";
            switch (CancellationTypeID)
            {
                case 11987:
                    cancellationString = "Agents Request";
                    break;
                case 11971:
                    cancellationString = "Cancel & Reinstatement";
                    break;
                case 11988:
                    cancellationString = "Cancel/Rewrite";
                    break;
                case 11990:
                    {
                        if (BL_LANG_ID == 2)
                            cancellationString = "Pedido de seguradora";
                        else
                            cancellationString = "Company Request";
                    }
                    break;
                case 11989:
                    {
                        if (BL_LANG_ID == 2)
                            cancellationString = "Pedido do cliente";
                        else
                            cancellationString = "Insured Request";
                    }
                    break;
                case 11969:
                    {
                        if (BL_LANG_ID == 2)
                            cancellationString = "Não-Pagamento";
                        else
                            cancellationString = "Non Payment";
                    }
                    break;
                case 11993:
                    cancellationString = "NSF/ No replacement";
                    break;
                case 11992:
                    cancellationString = "NSF/ Replace";
                    break;
                case 11970:
                    cancellationString = "Rescind";
                    break;
                default:
                    cancellationString = "Cancellation Requested/ Underwriting";
                    break;
            }
            return cancellationString;

        }
        public int GetLateFeesForPolicy(int CustomerId, int PolicyId, int PolicyVersionID)
        {
            DataWrapper objDataWrapper;
            int LateFee = 0;
            //if (objWrapper==null)
            objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            //else
            //	objDataWrapper=objWrapper;

            try
            {
                string strStoredProc = "Proc_GetPolicyLateFee";
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyId);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);

                DataSet dsTemp = objDataWrapper.ExecuteDataSet(strStoredProc);
                if (dsTemp.Tables[0].Rows.Count > 0)
                    LateFee = int.Parse(dsTemp.Tables[0].Rows[0]["LATE_FEES"].ToString());
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return LateFee;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (new Exception("Error while fetching late Fee.", ex));
            }
        }

        /// <summary>
        /// Calls the procedure Proc_GetKeysForCancellation to retrieve key values for Cancellation Process
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="PolicyId"></param>
        /// <param name="PolicyVersionId"></param>
        /// <returns></returns>
        public DataTable GetKeysForCancellation(int CustomerId, int PolicyId, int PolicyVersionId)
        {
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            DataSet ds = null;
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyId);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                ds = objDataWrapper.ExecuteDataSet("Proc_GetKeysForCancellation");
                if (ds != null)
                    return ds.Tables[0];
                else
                    return null;
            }
            catch// (Exception ex)
            {
                return null;
            }
            finally
            {
                if (objDataWrapper != null)
                    objDataWrapper = null;
                if (ds != null)
                    ds = null;
            }

        }
        public String DocumentCode(ClsProcessInfo objProcessInfo)
        {
            String billType = objProcessInfo.BILL_TYPE;
            //Added by Praveen kumar Starts 10-09-2010
            ClsPrintJobsInfo objPrintJobsInfo = GetPrintJobsValues(objProcessInfo, "CANC_NOTICE");
            if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NSF_NO_REPLACEMENT).ToString()) || objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NSF_REPLACE).ToString()))
                objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL", "ALL", "Agent", "NSF");
            else if (objProcessInfo.CANCELLATION_TYPE == int.Parse(((int)enumPROCESS_CANCELLATION_TYPES.NON_PAYMENT).ToString()))
            {
                if (billType == BILL_PAYMENT_DIRECT_BILL)
                    objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL", "ALL", "Agent", "NONPAYDB");
                else
                    objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL", "ALL", "Agent", "NONPAY");
            }
            else
                objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode(BlCommon.ClsCommon.GetLobCodeForLobId(objProcessInfo.LOB_ID.ToString()), objProcessInfo.STATE_CODE, "Agent", "CANC");
            String DocumentCode = objPrintJobsInfo.DOCUMENT_CODE.ToString();
            return DocumentCode;

        }
        static String _DocumentCode;
        public static String DocumentName
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
        //Added by Praveen kumar Starts 10-09-2010 Ends

        //calcel Boleto on commit
        public int CancelPolicyBoletos(int CustomerId, int PolicyId, int PolicyVersionId, int userId, int CancellationType, double Premium)
        {

            int Ret = 0;
            try
            {
                objWrapper.ClearParameteres();
                SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@RET_VAL", null, SqlDbType.Int, ParameterDirection.Output, 1000);

                objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                objWrapper.AddParameter("@CREATED_BY", userId);
                objWrapper.AddParameter("@CANCEL_TYPE", CancellationType);
                objWrapper.AddParameter("@RETURN_PREMIUM", Premium);
                Ret = objWrapper.ExecuteNonQuery("Proc_CANCEL_POLICY_BOLETOS");
                if (Ret > 0)
                {
                    Ret = (int)objSqlParameter.Value;
                }

                return Ret;
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return Ret;
            }

        }
        public static DataTable GetPolicyCancellationDate(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID)
        {

            DataTable returnval = null;
            DataSet ds;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                ds = objDataWrapper.ExecuteDataSet("Proc_GetNonPayCancellationDate");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    returnval = ds.Tables[0];

                return returnval;
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return null;
            }

        }
        private int PolicyTranType(int CustomerId, int PolicyId, int PolicyVersionId)
        {



            DataSet Ds = null;
            int TranType = 14559;
            try
            {

                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                Ds = objWrapper.ExecuteDataSet("Proc_GetPolicyTranType");
                if (Ds != null && Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                {
                    if (Ds.Tables[0].Rows[0]["TRANSACTION_TYPE"].ToString().Trim() != "")
                        TranType = int.Parse(Ds.Tables[0].Rows[0]["TRANSACTION_TYPE"].ToString());
                }

                return TranType;
            }
            catch (Exception ex) { throw ex; }

        }
        //public void GenRetPremiumInstall(int CUSTOMER_ID ,int POLICY_ID, int POLICY_VERSION_ID,double RETURN_PREMIUM,DateTime CANCELLATION_DATE)
        //{
        //    DataTable returnval = null;
        //    DataSet ds;
        //    try
        //    {
        //        DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
        //        objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
        //        objDataWrapper.AddParameter("@POLICY_ID", POLICY_ID);
        //        objDataWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
        //        objDataWrapper.AddParameter("@RETURN_PREMIUM", RETURN_PREMIUM);
        //        objDataWrapper.AddParameter("@CANCELLATION_DATE", CANCELLATION_DATE);
        //        ds = objDataWrapper.ExecuteDataSet("Proc_GetNonPayCancellationDate");
        //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            returnval = ds.Tables[0];


        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }


        //}
    }
}
