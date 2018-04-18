/******************************************************************************************
<Author					: - Vijay Joshi
<Start Date				: -	12/22/2005 8:32:38 PM
<End Date				: -	
<Description			: - Business class for policy endorsement process.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/

using System;
using Cms.Model.Policy.Process;
using Cms.DataLayer;
using System.Data;
using Cms.BusinessLayer.BlCommon;
using System.Data.SqlClient;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlAccount;
using System.Collections;
using Cms.BusinessLayer.BlQuote;
using System.Text;

namespace Cms.BusinessLayer.BlProcess
{
    /// <summary>
    /// Summary description for ClsEndorsmentProcess.
    /// </summary>
    public class ClsEndorsmentProcess : ClsPolicyProcess, IDisposable
    {
        public const string CALLEDFROM = "TOTAL";
        public const int CARRY_FORWARD_END = 14834;
        public const int OVERRIDE_END = 14835;
        public bool END_REISSUE = false;
        public ClsEndorsmentProcess()
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


        #region Overrided function
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
                string NewDispVersion = "";
                CreatePolicyNewVersion(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID, objProcessInfo.CREATED_BY.ToString(), out NewVersionID, out NewDispVersion);
                TransactionDescription.Append(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1476", "") + NewDispVersion + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1477", ""));//New version (//) of Policy has been created.;

                //Adding some properties in process info model object
                UpdateProcessInfoModel(objProcessInfo, NewVersionID);

                //Updating the status
                objProcessInfo.POLICY_CURRENT_STATUS = POLICY_STATUS_UNDER_ENDORSEMENT;
                //objProcessInfo.POLICY_PREVIOUS_STATUS = POLICY_STATUS_UNDER_ENDORSEMENT;
                //Populating the endorsement info model object
                ClsEndorsementDetailInfo objEndorsementInfo = PopulateEndorsementDetailInfo(objProcessInfo);
                //Inserting the record into endorsement log
                AddEndorsementLog(objEndorsementInfo);
                objProcessInfo.ENDORSEMENT_NO = objEndorsementInfo.ENDORSEMENT_NO;
                //Sets the Policy Status of newer version as under endorsement
                string strStatusDes = "", strNewStatus = "";
                SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID, out strStatusDes, out strNewStatus);
                TransactionDescription.Append(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1339", "") + strStatusDes + ".;");//Policy status has been updated to
                objProcessInfo.POLICY_CURRENT_STATUS = strNewStatus;
                //Starting the process
                bool retval = base.StartProcess(objProcessInfo);

                //Writing the transaction log
                WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID,
                    GetTransactionLogDesc(objProcessInfo.PROCESS_ID), objProcessInfo.COMPLETED_BY, TransactionDescription.ToString(), TransactionChangeXML.ToString());


                //Commiting the database transaction
                base.CommitTransaction();
                return retval;
            }
            catch (Exception objExp)
            {
                base.RollbackTransaction();
                throw (new Exception("Error occured while starting endorsement process \n" + objExp.Message));
            }

        }

        /// <summary>
        /// To commit the Endorsement Process	 
        /// </summary>
        /// <param name="objProcessInfo"></param>
        /// <returns>boolean</returns>
        public override bool CommitProcess(ClsProcessInfo objProcessInfo)
        {
            return this.CommitProcess(objProcessInfo, "");
        }
        public override bool CommitProcess(ClsProcessInfo objProcessInfo, string strCalledFrom)
        {
            /*	- complete the follow up that was generated by the system at the beginning of the process
                - record any changes that changed by the user in the process log 
                - record in the transaction history the details from the Endorsement Process Process Screen including printing options 
                - change the status from Endorsement in Progress to Active 
                - Send the printing details (packing slip to the printer)
                - make the necessary accounting entry for this transaction based on Payment plan and prepare the Premium notice 
                - Put  a note in the Accounting transaction history 
            */
            try
            {
                //Beging the transaction
                base.BeginTransaction();
                ClsPolicyErrMsg.strMessage = "";
                //Reseting the transaction descrition text
                TransactionDescription = new System.Text.StringBuilder();
                // Checking the eligibility, for the newer version of policy
                //if (objProcessInfo.ENDORSEMENT_OPTION != CARRY_FORWARD_END || objProcessInfo.ENDORSEMENT_OPTION != OVERRIDE_END)//not for caary forword endorsement
                //{
                if (base.CheckProcessEligibility(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID) != 1)
                {
                    base.RollbackTransaction();
                    return false;
                }
                //Checking the out of sequence endorsement check
                if (objProcessInfo.ENDORSEMENT_RE_ISSUE != (int)enumYESNO_LOOKUP_UNIQUE_ID.YES)
                    if (OutOfSequenceEndorsement(objProcessInfo))
                    {
                        //Rollbacking the transaction
                        base.RollbackTransaction();
                        return false;
                    }
                //  }
                //commit process
                bool retval = base.CommitProcess(objProcessInfo, "NEWVERSION");
                if (retval == false) //Checking the return status
                {
                    base.RollbackTransaction();//Rollbacking the transaction
                    return retval;
                }

                #region verifying the rules
                //Verifying the manadatory data 
                //bool validXML;	
                //ClsRatingAndUnderwritingRules objRule = new ClsRatingAndUnderwritingRules();

                //if(objRule.VerifyPolicy(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, "","") == false)
                //{
                ////Rules not verified
                ////Rollbacking the transaction
                //base.RollbackTransaction();
                //return false;
                //}
                //TransactionDescription.Append("Underwritting rules verified.;");
                //Updating the newer version of policy as active
                //////////
                #endregion
                //Populating the endorsement info model object
                ClsEndorsementDetailInfo objEndorsementInfo = PopulateEndorsementDetailInfo(objProcessInfo);
                objEndorsementInfo.ENDORSEMENT_STATUS = "COMMIT";

                //Updating the edorsement log
                UpdateEndorsementLog(objEndorsementInfo);
                TransactionDescription.Append(BlCommon.ClsCommon.FetchGeneralMessage("1425", ";"));//Endorsement log updated as completed.;");
                // Code moved before Open items creation
                objEndorsementInfo.ENDORSEMENT_DATE = objProcessInfo.EFFECTIVE_DATETIME;
                int CanceledBoletoNo = 0;
                if (objProcessInfo.ENDORSEMENT_RE_ISSUE == (int)enumYESNO_LOOKUP_UNIQUE_ID.YES)
                {
                    END_REISSUE = true;

                    CanceledBoletoNo = this.CancelReissuedVersionBoleto(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,
                                                                 objProcessInfo.SOURCE_VERSION_ID, objProcessInfo.COMPLETED_BY);

                    DataSet dsversion = Proc_GetPolicyDisplayVersion(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID,
                                                                 objProcessInfo.SOURCE_VERSION_ID, null);
                    string PolicydispStatus = "";
                   
                    if (dsversion != null && dsversion.Tables.Count > 0 && dsversion.Tables[0].Rows.Count > 0)
                        PolicydispStatus = dsversion.Tables[0].Rows[0]["POLICY_DISP_VERSION"].ToString();
                    dsversion.Dispose();
                    if (CanceledBoletoNo > 0)
                    {
                        TransactionDescription.Append(ClsCommon.FetchGeneralMessage("1981", "") + PolicydispStatus + ClsCommon.FetchGeneralMessage("1982", ";"));
                    }
                    this.UpdateEndorsementLogDetails(objEndorsementInfo, "REISSUE", objProcessInfo.SOURCE_VERSION_ID, ClsCommon.FetchGeneralMessage("1973", ";"));
                }
                #region posting premium
                //Calling the rating engine and posting the premium of the new policy
                string strTransactionDesc = "";
                int intResult = PostPolicyProductPremiumChangeInBusiness(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,
                    objProcessInfo.CUSTOMER_ID, objProcessInfo.EFFECTIVE_DATETIME, objProcessInfo.COMPLETED_BY,
                     objProcessInfo.PROCESS_ID, objProcessInfo.POLICY_VERSION_ID, out strTransactionDesc);
                //if (GetPolicyLOBID(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID) == 38)
                //    intResult = 1;
                if (intResult == -1 || intResult == -2)
                {
                    base.RollbackTransaction();
                    Cms.BusinessLayer.BlCommon.ClsDiary objClsDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
                    Cms.Model.Diary.TodolistInfo objModel = new Cms.Model.Diary.TodolistInfo();

                    if (intResult == -1)
                    {
                        ClsPolicyErrMsg.strMessage = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1478", ""); //"Premium could not be posted and hence process could not be Completed.Please Try Later!.";
                        return false;//objModel.SUBJECTLINE = "Please select new Billing plan.";
                        //objModel.NOTE = "Selected Billing plan has been deactivated.";
                    }
                    else
                    {
                        ClsPolicyErrMsg.strMessage = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1479", ""); //"Commission not found, so the process could not be committed.";
                        objModel.SUBJECTLINE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1960", "");
                        objModel.NOTE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1961", "");
                    }

                    objModel.LISTTYPEID = 15;
                    objModel.POLICY_ID = objProcessInfo.POLICY_ID;
                    objModel.POLICY_VERSION_ID = objProcessInfo.NEW_POLICY_VERSION_ID;
                    objModel.CUSTOMER_ID = objProcessInfo.CUSTOMER_ID;
                    if (IsEODProcess)
                        objModel.CREATED_BY = EODUserID;
                    else
                        objModel.CREATED_BY = objProcessInfo.COMPLETED_BY;
                    objModel.CREATED_DATETIME = DateTime.Now;
                    objModel.RECDATE = DateTime.Now;
                    objModel.LAST_UPDATED_DATETIME = DateTime.Now;
                    objModel.MODULE_ID = (int)BlCommon.ClsDiary.enumModuleMaster.POLICY_PROCESS;
                    objModel.LOB_ID = objProcessInfo.LOB_ID;
                    objClsDiary.DiaryEntryfromSetup(objModel);
                    //retval= false;
                    return false;
                }
                else
                {
                    TransactionDescription.Append(BlCommon.ClsCommon.FetchGeneralMessage("1426", ";"));//"Premium has been posted for change in business.;");
                    TransactionDescription.Append(strTransactionDesc);
                }

                #endregion

                //Printing Jobs -if internal change then not required at all
                if (objProcessInfo.LOB_ID.ToString() == "8")
                    GenerateAviationPdf(objProcessInfo);
                else
                    //PrintJobsForEndorsement(objProcessInfo);
                    //if (GetPolicyLOBID(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID) != 38)
                    //{
                        GeneratePolicyEndorsementDocuments(objProcessInfo);
                    //}
                    
                SetEndorsementPolStatus(objProcessInfo);

                //Entring the record in notice generation
                //				Cms.Model.Policy.Process.ClsPolicyProcessNoticInfo objProcessNoticeInfo = base.GetProcessNoticeInfo(objProcessInfo);
                //				objProcessNoticeInfo.POLICY_VERSION_ID = objProcessInfo.NEW_POLICY_VERSION_ID;
                //				objProcessNoticeInfo.NOTICE_DESCRIPTION = "Premium notice to be printed for endorsement process.";
                //				objProcessNoticeInfo.NOTICE_TYPE = "NEN";
                //				AddProcessNotice(objProcessNoticeInfo, objWrapper);
                //
                //				TransactionDescription.Append("Request sent for printing of Premium Notice.;");

                //Writing the transaction log
                if (strCalledFrom == Cms.CmsWeb.cmsbase.COMMIT_ANYWAYS_RULES)
                {
                    //AddDiaryEntryForRulesViolation(objProcessInfo);
                    TransactionDescription.Append(Cms.CmsWeb.cmsbase.COMMIT_ANYWAYS_RULES);
                }
                //if (objProcessInfo.ENDORSEMENT_RE_ISSUE == (int)enumYESNO_LOOKUP_UNIQUE_ID.YES)
                //{
                //    string Desc = "";
                //    string PolicyDisplayVersion = "";
                //    DataSet ds =  Proc_GetPolicyDisplayVersion(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.SOURCE_VERSION_ID,null);
                //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //        PolicyDisplayVersion = ds.Tables[0].Rows[0]["POLICY_DISP_VERSION"].ToString();
                //    Desc = ";" + ClsCommon.FetchGeneralMessage("1973", "") + " = " + PolicyDisplayVersion.ToString() + ";";
                //    TransactionDescription.Append(Desc);

                //}
                /*objEndorsementInfo.ENDORSEMENT_DATE = objProcessInfo.EFFECTIVE_DATETIME;
                int CanceledBoletoNo = 0;
                if (objProcessInfo.ENDORSEMENT_RE_ISSUE == (int)enumYESNO_LOOKUP_UNIQUE_ID.YES)
                {

                    CanceledBoletoNo = this.CancelReissuedVersionBoleto(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,
                                                                 objProcessInfo.SOURCE_VERSION_ID, objProcessInfo.COMPLETED_BY);

                    DataSet dsversion = Proc_GetPolicyDisplayVersion(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID,
                                                                 objProcessInfo.SOURCE_VERSION_ID, null);
                    string PolicydispStatus = "";
                    if (dsversion != null && dsversion.Tables.Count > 0 && dsversion.Tables[0].Rows.Count > 0)
                        PolicydispStatus = dsversion.Tables[0].Rows[0]["POLICY_DISP_VERSION"].ToString();
                    dsversion.Dispose();
                    if (CanceledBoletoNo > 0)
                    {
                        TransactionDescription.Append(ClsCommon.FetchGeneralMessage("1981", "") + PolicydispStatus + ClsCommon.FetchGeneralMessage("1982", ""));
                    }
                    this.UpdateEndorsementLogDetails(objEndorsementInfo, "REISSUE", objProcessInfo.SOURCE_VERSION_ID, ClsCommon.FetchGeneralMessage("1973", ""));
                }*/
                //reconcile policy open items // 
                if (objProcessInfo.PROCESS_TYPE == "NPAYENDCAN") // Non Pay Endorsement Cancellation - added By Pravesh on 16 June 11
                    base.RecocileOpenItems(objProcessInfo, objProcessInfo.SOURCE_VERSION_ID);

                WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,
                    GetTransactionLogDesc(objProcessInfo.PROCESS_ID), objProcessInfo.COMPLETED_BY, TransactionDescription.ToString());

                //Committing the transaction
                base.CommitTransaction();


                return retval;
            }
            catch (Exception objExp)
            {
                base.RollbackTransaction();
                throw (objExp);//new Exception("Error occurred while Committing endorsement process \n" + objExp.Message, objExp));
            }
        }

        /// <summary>
        ///  set policy status in case of endorsement
        /// </summary>
        /// <param name="objProcessInfo"></param>
        /// 
        private void SetEndorsementPolStatus(ClsProcessInfo objProcessInfo)
        {
            // PolicyStatusDesc = "";
            string strStoredProc = "Proc_SetEndorsementPolicyStatus";
            //			Cms.DataLayer.DataWrapper objWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure);
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", objProcessInfo.CUSTOMER_ID);
                objWrapper.AddParameter("@POLICY_ID", objProcessInfo.POLICY_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", objProcessInfo.POLICY_VERSION_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID_NEW", objProcessInfo.NEW_POLICY_VERSION_ID);

                SqlParameter Param = (SqlParameter)objWrapper.AddParameter("@POLICY_STATUS_DESC", null, SqlDbType.VarChar, ParameterDirection.Output, 30);

                if (objProcessInfo.PROCESS_ID == 0)
                    objWrapper.AddParameter("@PROCESS_ID", null);
                else
                    objWrapper.AddParameter("@PROCESS_ID", objProcessInfo.PROCESS_ID);

                objWrapper.AddParameter("@POLICY_STATUS", POLICY_STATUS_INACTIVE);

                objWrapper.ExecuteNonQuery(strStoredProc);
                //PolicyStatusDesc = Param.Value.ToString();
                objWrapper.ClearParameteres();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

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
                TransactionDescription.Append(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1482", "") + objProcessInfo.NEW_POLICY_VERSION_ID.ToString("#.0")
                    + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1481", ""));//"New version ("//) of policy has been deleted.;

                //Populating the endorsement info model object
                ClsEndorsementDetailInfo objEndorsementInfo = PopulateEndorsementDetailInfo(objProcessInfo);
                objEndorsementInfo.ENDORSEMENT_STATUS = "CANCEL";

                //Updating the endorsement log
                UpdateEndorsementLog(objEndorsementInfo);
                TransactionDescription.Append(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1480", ""));//Endorsement Log updated as cancelled.;

                //Changing the status of older policy as active policy
                //SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, POLICY_STATUS_NORMAL);
                //TransactionDescription.Append("Status of previous version " + objProcessInfo.POLICY_VERSION_ID.ToString("#.0") + " has been marked as Active.;");

                //Writing the transaction log
                WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID,
                    GetTransactionLogDesc(objProcessInfo.PROCESS_ID), objProcessInfo.COMPLETED_BY, TransactionDescription.ToString());


                //Commiting  the transaction
                base.CommitTransaction();

                return retval;
            }
            catch (Exception objExp)
            {
                base.RollbackTransaction();
                throw (new Exception("Error occured while RollbackProcess endorsement process \n" + objExp.Message));
            }
        }



        /// <summary>
        ///  Make diary entries in case of suspense endorsement.
        /// </summary>
        /// <param name="objProcessInfo"></param>
        public bool SuspenseEndorsementJobs(ClsProcessInfo objProcessInfo)
        {
            try
            {
                //Beging the transaction
                base.BeginTransaction();

                //Reseting the transaction descrition text
                TransactionDescription = new System.Text.StringBuilder();

                //Get and Set the diary entry for this process
                /*objProcessInfo.DIARY_LIST_ID = GetDiaryListID(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID,objProcessInfo.ROW_ID);
                if (objProcessInfo.DIARY_LIST_ID > 0)
                {
                    int iResult = 0;
                    iResult = SetDiaryEntryStatus(objProcessInfo.DIARY_LIST_ID, "N");
                    if (iResult > 0)
                    {
                        TransactionDescription.Append("\n Updated the Diary Entry Status.;");
                    }
                }*/
                int iResult = SetDiaryEntryStatus(objProcessInfo, "N");
                //TransactionDescription.Append("\n Update the Diary Entry Status.;");
                if (iResult > 0)
                    TransactionDescription.Append("\n Diary entry status has been updated.;");

                #region ADD NEW DIARY ENTRY
                //finding underwriter id for sending diary entry as TO  USERID 


                Cms.BusinessLayer.BlCommon.ClsDiary objClsDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
                Cms.Model.Diary.TodolistInfo objModel = new Cms.Model.Diary.TodolistInfo();

                int DiaryListTypeId;
                string strDiaryDesc = GetTransactionLogDesc(objProcessInfo.PROCESS_ID, out DiaryListTypeId);

                //Commented by Anurag Verma on 20/03/2007 as these properties are removed
                //objModel.POLICYCLIENTID = objProcessInfo.CUSTOMER_ID;
                objModel.LISTTYPEID = DiaryListTypeId;
                objModel.POLICY_ID = objProcessInfo.POLICY_ID;
                objModel.POLICY_VERSION_ID = objProcessInfo.NEW_POLICY_VERSION_ID;
                objModel.CUSTOMER_ID = objProcessInfo.CUSTOMER_ID;
                if (IsEODProcess)
                    objModel.FROMUSERID = EODUserID;
                else
                    objModel.FROMUSERID = objProcessInfo.COMPLETED_BY;
                if (IsEODProcess)
                    objModel.CREATED_BY = EODUserID;
                else
                    objModel.CREATED_BY = objProcessInfo.COMPLETED_BY;
                objModel.MODULE_ID = (int)BlCommon.ClsDiary.enumModuleMaster.POLICY_PROCESS;
                objModel.LOB_ID = objProcessInfo.LOB_ID;
                objModel.PROCESS_ROW_ID = objProcessInfo.ROW_ID;
                //				objModel.TOUSERID = objProcessInfo.UNDERWRITER;
                objModel.CREATED_DATETIME = DateTime.Now;
                //				objModel.RECDATE = DateTime.Now;
                //				objModel.FOLLOWUPDATE = DateTime.Now;    
                //				objModel.LAST_UPDATED_DATETIME = DateTime.Now;
                //				objModel.RECDATE = DateTime.Now;					
                //				objModel.STARTTIME = (System.DateTime)DateTime.Now;
                //				objModel.ENDTIME = (System.DateTime)DateTime.Now;				
                objModel.LISTOPEN = "Y";
                objModel.NOTE = strDiaryDesc;
                objModel.SUBJECTLINE = "Underwriter Review/Awaiting Details";
                //objModel.NOTE = objProcessInfo.DIARY_NOTE;
                int listid = 0;
                //listid=objClsDiary.AddPolicyEntry(objModel,base.objWrapper); 
                objClsDiary.DiaryEntryfromSetup(objModel, objWrapper);

                if (listid != 0)
                    objProcessInfo.DIARY_LIST_ID = listid;
                #endregion


                //Updating the status of new version of policy
                SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID);
                TransactionDescription.Append("Status of new version (" + objProcessInfo.POLICY_VERSION_ID.ToString("#.0")
                    + ") of policy has been updated as Endorsement in Suspense Policy.;");

                //Sets the Process Status on which the process has been launched.
                //SetProcessStatus(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID,objProcessInfo.ROW_ID,PROCESS_STATUS_COMPLETE);
                //TransactionDescription.Append("\n Process Status has been updated.;");


                //Committing the transaction
                base.CommitTransaction();
                return true;

            }
            catch (Exception ex)
            { throw (ex); }
        }


        public void AddDiaryEntryForSuspenseEnd(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
        {
            Cms.Model.Diary.TodolistInfo objModelInfo = new Cms.Model.Diary.TodolistInfo();
            Cms.BusinessLayer.BlCommon.ClsDiary objDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
            try
            {


                Cms.BusinessLayer.BlCommon.ClsDiary objClsDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
                Cms.Model.Diary.TodolistInfo objModel = new Cms.Model.Diary.TodolistInfo();
                int DiaryListTypeId;
                string strDiaryDesc = GetTransactionLogDesc(objProcessInfo.PROCESS_ID, out DiaryListTypeId);

                //Commented by Anurag Verma on 20/03/2007 as these properties are removed
                //objModel.POLICYCLIENTID = objProcessInfo.CUSTOMER_ID;
                objModel.LISTTYPEID = DiaryListTypeId;
                objModel.POLICY_ID = objProcessInfo.POLICY_ID;
                objModel.POLICY_VERSION_ID = objProcessInfo.POLICY_VERSION_ID;
                objModel.CUSTOMER_ID = objProcessInfo.CUSTOMER_ID;
                if (IsEODProcess)
                    objModel.FROMUSERID = EODUserID;
                else
                    objModel.FROMUSERID = objProcessInfo.COMPLETED_BY;
                if (IsEODProcess)
                    objModel.CREATED_BY = EODUserID;
                else
                    objModel.CREATED_BY = objProcessInfo.COMPLETED_BY;

                objModel.TOUSERID = objProcessInfo.UNDERWRITER;
                objModel.CREATED_DATETIME = DateTime.Now;
                objModel.RECDATE = DateTime.Now;
                objModel.FOLLOWUPDATE = DateTime.Now;
                objModel.LAST_UPDATED_DATETIME = DateTime.Now;
                objModel.RECDATE = DateTime.Now;
                objModel.STARTTIME = (System.DateTime)DateTime.Now;
                objModel.ENDTIME = (System.DateTime)DateTime.Now;
                objModel.LISTOPEN = "Y";
                objModel.NOTE = objModel.SUBJECTLINE = strDiaryDesc;
                objClsDiary.Add(objModel);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding new diary entry for commit anyway", ex);
            }
            finally
            {
                if (objModelInfo != null)
                    objModelInfo = null;
                if (objDiary != null)
                    objDiary = null;
            }

        }


        #endregion

        private ClsEndorsementDetailInfo PopulateEndorsementDetailInfo(ClsProcessInfo objProcessInfo)
        {
            //int i = new int();

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
            return objInfo;
        }

        //protected override 

        #region UpdateEndorsementLog
        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="objEndorsementDetailInfo">Model class object.</param>
        /// <returns>No of records effected.</returns>
        public int UpdateEndorsementLog(ClsEndorsementDetailInfo objEndorsementDetailInfo)
        {
            string strStoredProc = "Proc_UpdateStatusPOL_POLICY_ENDORSEMENTS";
            DateTime RecordDate = DateTime.Now;

            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@POLICY_ID", objEndorsementDetailInfo.POLICY_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", objEndorsementDetailInfo.POLICY_VERSION_ID);
                objWrapper.AddParameter("@CUSTOMER_ID", objEndorsementDetailInfo.CUSTOMER_ID);
                objWrapper.AddParameter("@ENDORSEMENT_NO", objEndorsementDetailInfo.ENDORSEMENT_NO);

                objWrapper.AddParameter("@ENDORSEMENT_STATUS", objEndorsementDetailInfo.ENDORSEMENT_STATUS);

                objWrapper.AddParameter("@MODIFIED_BY", objEndorsementDetailInfo.MODIFIED_BY);
                objWrapper.AddParameter("@LAST_UPDATED_DATETIME", DateTime.Now);

                int returnResult = objWrapper.ExecuteNonQuery(strStoredProc);

                objWrapper.ClearParameteres();
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (new Exception("Error occured while inserting record in endorsement log. \n" + ex.Message));
            }
        }
        #endregion

        #region AddEndorsementLog
        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="objEndorsementDetailInfo">Model class object.</param>
        /// <returns>No of records effected.</returns>
        public int AddEndorsementLog(ClsEndorsementDetailInfo objEndorsementDetailInfo)
        {
            string strStoredProc = "Proc_InsertPOL_POLICY_ENDORSEMENTS";
            DateTime RecordDate = DateTime.Now;

            try
            {
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

                int returnResult = objWrapper.ExecuteNonQuery(strStoredProc);

                objEndorsementDetailInfo.ENDORSEMENT_NO = Convert.ToInt32(objParam.Value);
                objWrapper.ClearParameteres();
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (new Exception("Error occured while inserting record in endorsement log. \n" + ex.Message));
            }
        }
        #endregion

        #region AddEndorsementLogDetails
        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="objEndorsementDetailInfo">Model class object.</param>
        /// <returns>No of records effected.</returns>
        public int AddEndorsementLogDetails(ClsEndorsementDetailInfo objEndorsementDetailInfo)
        {
            string strStoredProc = "Proc_InsertPOL_POLICY_ENDORSEMENTS_DETAILS";
            DateTime RecordDate = DateTime.Now;
            base.BeginTransaction();

            try
            {
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
                base.CommitTransaction();

                return returnResult;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw (ex);
            }
            finally
            {
                if (objWrapper != null) objWrapper.Dispose();
            }
        }
        #endregion

        #region Update endorsement details functions
        public int UpdateEndorsementLogDetails(ClsEndorsementDetailInfo objEndorsementDetailInfo, string CalledFrom, int SourceVersion, string CustomMessage)
        {
            string strStoredProc = "Proc_UpdatePOL_POLICY_ENDORSEMENTS_DETAILS";
            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {
                objDataWrapper.AddParameter("@POLICY_ID", objEndorsementDetailInfo.POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", objEndorsementDetailInfo.POLICY_VERSION_ID);
                objDataWrapper.AddParameter("@CUSTOMER_ID", objEndorsementDetailInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@ENDORSEMENT_NO", objEndorsementDetailInfo.ENDORSEMENT_NO);
                objDataWrapper.AddParameter("@ENDORSEMENT_DATE", objEndorsementDetailInfo.ENDORSEMENT_DATE);
                objDataWrapper.AddParameter("@ENDORSEMENT_TYPE", objEndorsementDetailInfo.ENDORSEMENT_TYPE);
                objDataWrapper.AddParameter("@ENDORSEMENT_DESC", objEndorsementDetailInfo.ENDORSEMENT_DESC);
                objDataWrapper.AddParameter("@REMARKS", objEndorsementDetailInfo.REMARKS);
                objDataWrapper.AddParameter("@ENDORSEMENT_DETAIL_ID", objEndorsementDetailInfo.ENDORSEMENT_DETAIL_ID);
                objDataWrapper.AddParameter("@CALLED_FROM", CalledFrom);
                objDataWrapper.AddParameter("@SOURCE_VERSION_ID", SourceVersion);
                objDataWrapper.AddParameter("@ADDITION_INFO", CustomMessage);
                int returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

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

        /// <summary>
        /// Updates the information passed in model object to database.
        /// </summary>
        /// <param name="objEndorsementDetailInfo">Model class object.</param>
        /// <returns>No of records effected.</returns>
        /// 
        public int UpdateEndorsementLogDetails(ClsEndorsementDetailInfo objEndorsementDetailInfo)
        {
            string strStoredProc = "Proc_UpdatePOL_POLICY_ENDORSEMENTS_DETAILS";
            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {
                objDataWrapper.AddParameter("@POLICY_ID", objEndorsementDetailInfo.POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", objEndorsementDetailInfo.POLICY_VERSION_ID);
                objDataWrapper.AddParameter("@CUSTOMER_ID", objEndorsementDetailInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@ENDORSEMENT_NO", objEndorsementDetailInfo.ENDORSEMENT_NO);
                objDataWrapper.AddParameter("@ENDORSEMENT_DATE", objEndorsementDetailInfo.ENDORSEMENT_DATE);
                objDataWrapper.AddParameter("@ENDORSEMENT_TYPE", objEndorsementDetailInfo.ENDORSEMENT_TYPE);
                objDataWrapper.AddParameter("@ENDORSEMENT_DESC", objEndorsementDetailInfo.ENDORSEMENT_DESC);
                objDataWrapper.AddParameter("@REMARKS", objEndorsementDetailInfo.REMARKS);
                objDataWrapper.AddParameter("@ENDORSEMENT_DETAIL_ID", objEndorsementDetailInfo.ENDORSEMENT_DETAIL_ID);

                int returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

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
        #endregion

        #region UpdateProcessInfoModel
        private void UpdateProcessInfoModel(ClsProcessInfo objProcessInfo, int NewVersionId)
        {
            objProcessInfo.NEW_POLICY_VERSION_ID = NewVersionId;
            objProcessInfo.NEW_CUSTOMER_ID = objProcessInfo.CUSTOMER_ID;
            objProcessInfo.NEW_POLICY_ID = objProcessInfo.POLICY_ID;
            //objProcessInfo.ENDORSEMENT_NO = 
        }
        #endregion

        #region  Premium Posting
        public int PostPolicyProductPremiumChangeInBusiness(int PolicyId, int PolicyVersionId, int CustomerId,
            DateTime ChangeEffectiveDate, int UserID, int ProcessType, int OldVersion, out string TransactionDesc)
        {

            try
            {
                //double dblPremium = 0, dblOtherFees = 0, dblMCCAFees = 0; 
                //Fetching the appliation details
                int AppID, AppVersionID, LobId, PremiumProcessTableRowId, retVal = 0;
                int Tran_TYPE = 0;
                int CO_INSURANCE = 0;
                string strPolicyCurrencyId = "";
                string Plan_type = "";
                GetAppLobDetails(CustomerId, PolicyId, PolicyVersionId, out AppID, out AppVersionID, out LobId, out strPolicyCurrencyId, out Tran_TYPE, out CO_INSURANCE, out Plan_type);

                double dblFeesToReserve = 0;//, TotalGrossPremium = 0;
              //  bool IsSamePolicy = false;
                TransactionDesc = "";





                //post Co Insurance Premium
                Insert_COInsurance_Premium(CustomerId, PolicyId, PolicyVersionId, UserID);


                ClsGenerateQuote objQuote = new ClsGenerateQuote(mSystemID);
                int quodeId;
                objQuote.GeneratePolicyQuote(CustomerId, PolicyId, PolicyVersionId, LobId.ToString(), out quodeId, UserID.ToString());

                //Call Split Premium 
                Cms.BusinessLayer.BlQuote.clsSplitPremium objSplitPremium = new Cms.BusinessLayer.BlQuote.clsSplitPremium();
                ArrayList arrPremiumXML = objSplitPremium.SplitPremiumsPol(CustomerId, PolicyId, PolicyVersionId, ProcessType.ToString(), objWrapper);
                //Fetch  Premium XML of Previous Version
                ArrayList arrOldPremiumXML = objSplitPremium.GetPremiumXMLFromQuote(CustomerId, PolicyId, OldVersion, objWrapper, "ENDORSE");
                //- For converted policies no recorded will be there in 
                //Quote table so rate will be generated for old version also
                if (arrOldPremiumXML.Count == 0)
                {
                    objQuote.GeneratePolicyQuote(CustomerId, PolicyId, OldVersion, LobId.ToString(), out quodeId, UserID.ToString());
                    arrOldPremiumXML = objSplitPremium.GetPremiumXMLFromQuote(CustomerId, PolicyId, OldVersion, objWrapper, "ENDORSE");
                }

                bool DoPostings = false;

                ArrayList arrPremium = GetPremiumDetailsChangeInBusiness(CustomerId, PolicyId, PolicyVersionId,
                    arrOldPremiumXML, arrPremiumXML, ProcessType, LobId, out PremiumProcessTableRowId,
                    out dblFeesToReserve, out DoPostings, Tran_TYPE, Plan_type);
                DoPostings = true;//added by Lalit June 08,2011//for open item .itrack # 913
                if (DoPostings)
                {
                    string Trandesc = "";
                    retVal = base.PostOpenItems(CustomerId, PolicyId, PolicyVersionId, ProcessType, UserID, out Trandesc,END_REISSUE);
                    TransactionDesc += Trandesc;

                    #region previous posting on endorsement,code Commented By lalit Dec 7,2010
                    /*
                    int CommissionClass = 0, OldCommissionClass = 0,RiskID=1;
                    double TotalNetPremium=0,TotalMCCAFees=0,TotalOtherFees=0,TotalInforcePremium=0;
                    for (int i = 0; i < arrPremium.Count; i++)
                    {
                        Premium objPremium = (Premium)arrPremium[i];

                        TotalNetPremium+=objPremium.NetPremium;
                        TotalMCCAFees+= objPremium.MCCAFees;
                        TotalOtherFees+= objPremium.OtherFees;
                        TotalInforcePremium += objPremium.InforcePremium;
                        //If there is no change in premium amount
                        //if (objPremium.NetPremium == 0)
                        //    continue;
                        TotalGrossPremium += objPremium.GrossPremium;
                    }
                        objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                        objWrapper.AddParameter("@APP_ID", AppID);
                        objWrapper.AddParameter("@APP_VERSION_ID", AppVersionID);

                        objWrapper.AddParameter("@POLICY_ID", PolicyId);
                        objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                        objWrapper.AddParameter("@CHANGE_EFFECTIVE_DATE", ChangeEffectiveDate);

                        objWrapper.AddParameter("@CHANGE_PREMIUM_AMOUNT", TotalNetPremium);

                        objWrapper.AddParameter("@PARAM1", PremiumProcessTableRowId);
                        objWrapper.AddParameter("@PARAM2", UserID);
                        objWrapper.AddParameter("@PARAM3", null);
                        objWrapper.AddParameter("@PARAM4", null);

                        objWrapper.AddParameter("@RISK_ID", RiskID);
                        objWrapper.AddParameter("@RISK_TYPE", "PRODUCT"); //objPremium.RiskType);

                        objWrapper.AddParameter("@COMMISSION_CLASS", CommissionClass);

                        objWrapper.AddParameter("@MCCA_FEES", TotalMCCAFees);
                        objWrapper.AddParameter("@OTHER_FEES", TotalOtherFees);

                        // Change premium will be distributed based on Inforce Premium
                        objWrapper.AddParameter("@TOTAL_INFORCE_PREMIUM", TotalInforcePremium);

                        if (!IsSamePolicy)
                        {
                            objWrapper.AddParameter("@FEES_TO_REVERSE", dblFeesToReserve);
                            IsSamePolicy = true;
                        }
                        else
                        {
                            objWrapper.AddParameter("@FEES_TO_REVERSE", 0);
                        }
                        SqlParameter objParam = (SqlParameter)objWrapper.AddParameter
                            ("@RetVal", null, SqlDbType.Int, ParameterDirection.Output);
                        objWrapper.ExecuteNonQuery("Proc_ChangePremiumProductPolicyOpenItems");
                        if (objParam.Value != DBNull.Value)
                            retVal = Convert.ToInt32(objParam.Value);
                        else
                            retVal = -1;
                        objWrapper.ClearParameteres();

                        //If Commission class has changed in endorsement for Risk do commission adjustments
                        if (retVal == 1)
                        {
                            if (CommissionClass != OldCommissionClass)
                            {
                                objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                                objWrapper.AddParameter("@RISK_ID", RiskID);
                                objWrapper.AddParameter("@RISK_TYPE", "PRODUCT");
                                objWrapper.AddParameter("@NEW_COMMISSION_CLASS", CommissionClass);
                                objWrapper.AddParameter("@CHANGE_EFFECTIVE_DATE", ChangeEffectiveDate);
                                objWrapper.AddParameter("@PPD_ROW_ID", PremiumProcessTableRowId);
                                objWrapper.AddParameter("@USER_ID", UserID);
                                objWrapper.ExecuteNonQuery("Proc_DoPostEndorsementAdjustments");
                                objWrapper.ClearParameteres();
                            }
                        }
                    }
                    Add to Tran log
                    TransactionDesc += "Total premium amount invoiced $" + TotalGrossPremium.ToString() + ";";

                    TransactionDesc += ClsCommon.FetchGeneralMessage("1117", " " + FormatNumber(TotalGrossPremium.ToString())) + ";";
                    */
                    #endregion End Commented Code of Post open items
                }

                objSplitPremium.UpdateWrittenEnforcePremiumSplit(CustomerId, PolicyId, PolicyVersionId, OldVersion, ProcessType.ToString(), objWrapper);
                objWrapper.ClearParameteres();
                //post reinsurance premium
                Insert_ReInsurance_Premium(CustomerId, PolicyId, PolicyVersionId, ProcessType, UserID);

                return retVal;
            }
            catch (Exception objExp)
            {
                objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (objExp);
            }
        }
        public int PostPolicyPremiumChangeInBusiness(int PolicyId, int PolicyVersionId, int CustomerId,
            DateTime ChangeEffectiveDate, int UserID, int ProcessType, int OldVersion, out string TransactionDesc)
        {

            try
            {
                //double dblPremium = 0, dblOtherFees = 0, dblMCCAFees = 0; 

                //Fetching the appliation details
                int AppID, AppVersionID, LobId, PremiumProcessTableRowId, retVal = 0;
                //GetAppLobDetails(CustomerId, PolicyId,PolicyVersionId, out AppID, out AppVersionID, out LobId);

                string strPolicyCurrencyId = "";
                GetAppLobDetails(CustomerId, PolicyId, PolicyVersionId, out AppID, out AppVersionID, out LobId, out strPolicyCurrencyId);


                double dblFeesToReserve = 0, TotalGrossPremium = 0;
                bool IsSamePolicy = false;
                TransactionDesc = "";
                //Ravindra (10-09-2006)
                ClsGenerateQuote objQuote = new ClsGenerateQuote(mSystemID);
                int quodeId;
                objQuote.GeneratePolicyQuote(CustomerId, PolicyId, PolicyVersionId, LobId.ToString(), out quodeId, UserID.ToString());

                //Call Split Premium 
                Cms.BusinessLayer.BlQuote.clsSplitPremium objSplitPremium = new Cms.BusinessLayer.BlQuote.clsSplitPremium();
                ArrayList arrPremiumXML = objSplitPremium.SplitPremiumsPol(CustomerId, PolicyId, PolicyVersionId, ProcessType.ToString(), objWrapper);
                //ArrayList arrPremiumXML=SplitPremiumsPol(CustomerId,PolicyId,PolicyVersionId,"Change It",objWrapper); 

                //Fetch  Premium XML of Previous Version

                ArrayList arrOldPremiumXML = objSplitPremium.GetPremiumXMLFromQuote(CustomerId, PolicyId, OldVersion, objWrapper, "ENDORSE");

                //Ravindra(08-29-2007) - For converted policies no recorded will be there in 
                //Quote table so rate will be generated for old version also
                if (arrOldPremiumXML.Count == 0)
                {
                    objQuote.GeneratePolicyQuote(CustomerId, PolicyId, OldVersion, LobId.ToString(), out quodeId, UserID.ToString());
                    arrOldPremiumXML = objSplitPremium.GetPremiumXMLFromQuote(CustomerId, PolicyId, OldVersion, objWrapper, "ENDORSE");
                }

                //				for(int j=0 ; j < arrPremiumXML.Count ; j++)
                //				{
                //					PremiumDetails objPremiumdetails = (PremiumDetails)arrPremiumXML[j];
                //					PremiumDetails objOldPremiumdetails;
                //					string strOldPremiumXML;
                //					if (arrOldPremiumXML.Count>0)
                //					{
                //						objOldPremiumdetails = (PremiumDetails)arrOldPremiumXML[j];
                //						strOldPremiumXML=objOldPremiumdetails.PremiumXML;
                //					}
                //					else
                //						strOldPremiumXML="";
                //					//ArrayList arrPremium = GetPremiumDetailsChangeInBusiness(CustomerId,PolicyId,PolicyVersionId,
                //					//	objOldPremiumdetails.PremiumXML ,objPremiumdetails.PremiumXML,
                //					//	objPremiumdetails.QuoteType,ProcessType,LobId,out PremiumProcessTableRowId,out dblFeesToReserve);
                //
                //					ArrayList arrPremium = GetPremiumDetailsChangeInBusiness(CustomerId,PolicyId,PolicyVersionId,
                //						strOldPremiumXML ,objPremiumdetails.PremiumXML,
                //						objPremiumdetails.QuoteType,ProcessType,LobId,out PremiumProcessTableRowId,out dblFeesToReserve);
                //						
                //				}


                bool DoPostings = false;

                ArrayList arrPremium = GetPremiumDetailsChangeInBusiness(CustomerId, PolicyId, PolicyVersionId,
                    arrOldPremiumXML, arrPremiumXML, ProcessType, LobId, out PremiumProcessTableRowId,
                    out dblFeesToReserve, out DoPostings);

                if (DoPostings)
                {
                    for (int i = 0; i < arrPremium.Count; i++)
                    {
                        Premium objPremium = (Premium)arrPremium[i];

                        int CommissionClass, OldCommissionClass;


                        if (objPremium.IsRiskDeleted)
                        {
                            CommissionClass = GetCommissionClass(CustomerId, PolicyId, OldVersion,
                                objPremium.RiskID, objWrapper);

                            OldCommissionClass = CommissionClass;

                        }
                        else if (objPremium.IsRiskAdded)
                        {
                            CommissionClass = GetCommissionClass(CustomerId, PolicyId, PolicyVersionId,
                                objPremium.RiskID, objWrapper);
                            OldCommissionClass = CommissionClass;
                        }
                        else
                        {
                            CommissionClass = GetCommissionClass(CustomerId, PolicyId, PolicyVersionId,
                                objPremium.RiskID, objWrapper);

                            OldCommissionClass = GetCommissionClass(CustomerId, PolicyId, OldVersion,
                                objPremium.RiskID, objWrapper);
                        }

                        //If there is no change in premium amount
                        if (objPremium.NetPremium == 0)
                            continue;

                        TotalGrossPremium += objPremium.GrossPremium;

                        objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                        objWrapper.AddParameter("@APP_ID", null);
                        objWrapper.AddParameter("@APP_VERSION_ID", null);

                        objWrapper.AddParameter("@POLICY_ID", PolicyId);
                        objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                        objWrapper.AddParameter("@CHANGE_EFFECTIVE_DATE", ChangeEffectiveDate);

                        objWrapper.AddParameter("@CHANGE_PREMIUM_AMOUNT", objPremium.NetPremium);

                        objWrapper.AddParameter("@PARAM1", PremiumProcessTableRowId);
                        objWrapper.AddParameter("@PARAM2", UserID);
                        objWrapper.AddParameter("@PARAM3", null);
                        objWrapper.AddParameter("@PARAM4", null);

                        objWrapper.AddParameter("@RISK_ID", objPremium.RiskID);
                        objWrapper.AddParameter("@RISK_TYPE", objPremium.RiskType);

                        objWrapper.AddParameter("@COMMISSION_CLASS", CommissionClass);

                        objWrapper.AddParameter("@MCCA_FEES", objPremium.MCCAFees);
                        objWrapper.AddParameter("@OTHER_FEES", objPremium.OtherFees);

                        // Added By Ravindra(08-29-2007) 
                        // Change premium will be distributed based on Inforce Premium
                        objWrapper.AddParameter("@TOTAL_INFORCE_PREMIUM", objPremium.InforcePremium);

                        if (!IsSamePolicy)
                        {
                            objWrapper.AddParameter("@FEES_TO_REVERSE", dblFeesToReserve);
                            IsSamePolicy = true;
                        }
                        else
                        {
                            objWrapper.AddParameter("@FEES_TO_REVERSE", 0);
                        }
                        SqlParameter objParam = (SqlParameter)objWrapper.AddParameter
                            ("@RetVal", null, SqlDbType.Int, ParameterDirection.Output);
                        objWrapper.ExecuteNonQuery("Proc_ChangePremiumPolicyOpenItems");
                        if (objParam.Value != DBNull.Value)
                            retVal = Convert.ToInt32(objParam.Value);
                        else
                            retVal = -1;
                        objWrapper.ClearParameteres();

                        //If Commission class has changed in endorsement for Risk do commission adjustments
                        if (retVal == 1)
                        {
                            if (CommissionClass != OldCommissionClass)
                            {
                                objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);
                                objWrapper.AddParameter("@RISK_ID", objPremium.RiskID);
                                objWrapper.AddParameter("@RISK_TYPE", objPremium.RiskType);
                                objWrapper.AddParameter("@NEW_COMMISSION_CLASS", CommissionClass);
                                objWrapper.AddParameter("@CHANGE_EFFECTIVE_DATE", ChangeEffectiveDate);
                                objWrapper.AddParameter("@PPD_ROW_ID", PremiumProcessTableRowId);
                                objWrapper.AddParameter("@USER_ID", UserID);
                                objWrapper.ExecuteNonQuery("Proc_DoPostEndorsementAdjustments");
                                objWrapper.ClearParameteres();
                            }
                        }
                    }
                    //Ravindra(09-25-2009)Record should be created in Spliy with Zero written Premium 
                    //					//Update Written and change in premium in premium split table --added by Pravesh on 12 dec 2007
                    //				
                    //					objSplitPremium.UpdateWrittenEnforcePremiumSplit(CustomerId,PolicyId,PolicyVersionId,OldVersion,ProcessType.ToString(),objWrapper);
                    //					objWrapper.ClearParameteres();
                    //					//end ehre
                    //Add to Tran log

                    //TransactionDesc+="Total premium amount invoiced $" + TotalGrossPremium.ToString() + ";";
                    //Added by pradeep Kushwaha on 20-07-2010
                    //TransactionDesc += ClsCommon.FetchGeneralMessage("1117", " " + GetPolicyCurrencySymbol(strPolicyCurrencyId)) + TotalGrossPremium.ToString() + ";";
                    TransactionDesc += ClsCommon.FetchGeneralMessage("1117", " " + FormatNumber(TotalGrossPremium.ToString())) + ";";


                }

                objSplitPremium.UpdateWrittenEnforcePremiumSplit(CustomerId, PolicyId, PolicyVersionId, OldVersion, ProcessType.ToString(), objWrapper);
                objWrapper.ClearParameteres();

                return retVal;
            }
            catch (Exception objExp)
            {
                objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (objExp);
            }
        }
        // by P.K Chandel  on 19 Aug 2010 remove Premium XML parsing dependency
        public System.Collections.ArrayList GetProductPremiumDetailsChangeInBusiness(int CustomerId, int PolicyId,
            int PolicyVersionId, int oldPolicyVersionId, int ProcessType, int LobId, out int PremiumProcessTableRowID,
            out double dblFeesToReverse, out bool DoPostings)
        {
            PremiumProcessTableRowID = 0;
            int returnResult = 0;
            dblFeesToReverse = 0;
            ArrayList arrPremium = new ArrayList();
            ArrayList arrOldPremium = new ArrayList();
            //ArrayList arrTemp = null;
            DoPostings = false;
            string strFinalPremium = "";
            double dblPremium = 0, dblOtherFees = 0, dblMCCAFees = 0, dblGrossPremium;
            double dblNetFinalPremium = 0, dblTemp = 0;
            dblGrossPremium = 0;
            try
            {
                // fetching premium details of Current Version
                DataSet dsPremium = GetPolicyPremiumFromBillingInfo(CustomerId, PolicyId, PolicyVersionId);
                if (dsPremium.Tables[0].Rows.Count == 0)
                {
                    throw (new Exception("Process could not be committed. Please complete billing Info Tab."));
                }
                Premium objPremium = new Premium();
                objPremium.NetPremium = Convert.ToDouble(dsPremium.Tables[0].Rows[0]["TOTAL_PREMIUM"].ToString());
                objPremium.MCCAFees = 0;
                objPremium.OtherFees = 0;
                objPremium.GrossPremium = Convert.ToDouble(dsPremium.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString());
                arrPremium.Add(objPremium);
                strFinalPremium = "<PREMIUM><GROSSPREMIUM>" + objPremium.GrossPremium.ToString() + "</GROSSPREMIUM>" +
                                  "<NETPREMIUM>" + objPremium.NetPremium.ToString() + "</NETPREMIUM>" +
                                  "<OTHERFEES>0</OTHERFEES><MCCAFEES>0</MCCAFEES>" +
                                  "</PREMIUM>";
                // fetching premium details of previous Version
                DataSet dsOldPremium = GetPolicyPremiumFromBillingInfo(CustomerId, PolicyId, oldPolicyVersionId);
                if (dsOldPremium.Tables[0].Rows.Count == 0)
                {
                    throw (new Exception("Process could not be committed. Please complete billing Info Tab."));
                }
                objPremium = new Premium();
                objPremium.NetPremium = Convert.ToDouble(dsOldPremium.Tables[0].Rows[0]["TOTAL_PREMIUM"].ToString());
                objPremium.MCCAFees = 0;
                objPremium.OtherFees = 0;
                objPremium.GrossPremium = Convert.ToDouble(dsOldPremium.Tables[0].Rows[0]["TOTAL_AMOUNT"].ToString());
                arrOldPremium.Add(objPremium);
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
                InsertFinalPremium(CustomerId, PolicyId, PolicyVersionId, "O", ProcessType, strFinalPremium, strFinalPremium, dblNetFinalPremium);
                PremiumProcessTableRowID = returnResult;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return arrPremium;
            }
            // Calculating Differences in NetPremium for each Risks
            bool IsRiskDeleted;
            for (int i = 0; i < arrPremium.Count; i++)
            {
                for (int j = 0; j < arrOldPremium.Count; j++)
                {
                    if (((Premium)arrOldPremium[j]).RiskID == ((Premium)arrPremium[i]).RiskID
                        &&
                        ((Premium)arrOldPremium[j]).RiskType == ((Premium)arrPremium[i]).RiskType
                        )
                    {
                        ((Premium)arrPremium[i]).GrossPremium = ((Premium)arrPremium[i]).GrossPremium - ((Premium)arrOldPremium[j]).GrossPremium;
                        ((Premium)arrPremium[i]).NetPremium = ((Premium)arrPremium[i]).NetPremium - ((Premium)arrOldPremium[j]).NetPremium;
                        ((Premium)arrPremium[i]).OtherFees = ((Premium)arrPremium[i]).OtherFees - ((Premium)arrOldPremium[j]).OtherFees;
                        ((Premium)arrPremium[i]).MCCAFees = ((Premium)arrPremium[i]).MCCAFees - ((Premium)arrOldPremium[j]).MCCAFees;

                        ((Premium)arrPremium[i]).IsRiskAdded = false;
                    }
                }
            }

            //Changed implementation of Fees to reverse for deleted risks
            // instead of calculating fees to reverse for deleted risks deleted risks will be added to 
            // premium array with negative values thus fees & premium amount will be reversed automatically
            // for concerned risk taking prorata calulation method in account.

            //Finding deleted Risks
            for (int j = 0; j < arrOldPremium.Count; j++)
            {
                IsRiskDeleted = true;
                for (int i = 0; i < arrPremium.Count; i++)
                {

                    if (((Premium)arrOldPremium[j]).RiskID == ((Premium)arrPremium[i]).RiskID
                        &&
                        ((Premium)arrOldPremium[j]).RiskType == ((Premium)arrPremium[i]).RiskType
                        )
                    {
                        IsRiskDeleted = false;
                    }
                }
                if (IsRiskDeleted)
                {
                    ((Premium)arrOldPremium[j]).IsRiskDeleted = true;

                }
            }
            // Adding deleted risks with negative amount to premium array
            for (int j = 0; j < arrOldPremium.Count; j++)
            {
                if (((Premium)arrOldPremium[j]).IsRiskDeleted)
                {
                    ((Premium)arrOldPremium[j]).GrossPremium = (((Premium)arrOldPremium[j]).GrossPremium) * -1;
                    ((Premium)arrOldPremium[j]).NetPremium = (((Premium)arrOldPremium[j]).NetPremium) * -1;
                    ((Premium)arrOldPremium[j]).MCCAFees = (((Premium)arrOldPremium[j]).MCCAFees) * -1;
                    ((Premium)arrOldPremium[j]).OtherFees = (((Premium)arrOldPremium[j]).OtherFees) * -1;

                    ((Premium)arrOldPremium[j]).IsRiskAdded = false;
                    arrPremium.Add(arrOldPremium[j]);
                }
            }

            //dblFeesToReverse = dblMCCAFeesToReserve  + dblOtherFeesToReserve ;
            Premium objTotalPremium = new Premium();

            for (int i = 0; i < arrPremium.Count; i++)
            {

                Premium objPremium = (Premium)(arrPremium[i]);

                //For Saving  Inforce Premium
                objPremium.InforceFees = objPremium.MCCAFees + objPremium.OtherFees;
                objPremium.InforcePremium = objPremium.GrossPremium;

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

            dblOtherFees = dblOtherFees - dblFeesToReverse;

            double MinPremium = 0;
            try
            {
                objWrapper.ClearParameteres();
                DataSet ds = objWrapper.ExecuteDataSet("Proc_GetMinPremiumForEnforsement");
                MinPremium = Convert.ToDouble(ds.Tables[0].Rows[0]["MIN_END_PREM"]);
            }
            catch (Exception ex)
            {
                ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(new Exception("Error while fetching Min Premium from System Param", ex));
            }

            //Condition if premium is not 0
            if (dblPremium != 0)
            {
                //if(Math.Abs(dblPremium) > Math.Abs(MinPremium))
                if (Math.Abs(dblGrossPremium) > Math.Abs(MinPremium))
                {

                    //Total Premum Calculation
                    //CalculateChangeEffectivePremium(CustomerId,PolicyId , PolicyVersionId , objTotalPremium, ref dblTemp );
                    CalculateChangeEffectivePremiumPolicy(CustomerId, PolicyId, PolicyVersionId, objTotalPremium, ref dblTemp, CALLEDFROM);

                    //There could be difference in Total as premium is pro rarted and rounded at risk level
                    if (objTotalPremium.GrossPremium != dblGrossPremium)
                    {
                        Premium objPremium = (Premium)(arrPremium[0]);

                        double dblDifference = objTotalPremium.GrossPremium - dblGrossPremium;
                        dblGrossPremium = dblGrossPremium + dblDifference;

                        objPremium.GrossPremium += dblDifference;

                        dblDifference = (objTotalPremium.MCCAFees + objTotalPremium.OtherFees) - (dblMCCAFees + dblOtherFees) - dblTemp;
                        dblMCCAFees = dblMCCAFees + dblDifference;

                        objPremium.MCCAFees += dblDifference;

                        dblDifference = (dblGrossPremium - dblMCCAFees - dblOtherFees) - dblPremium;

                        dblPremium = dblPremium + dblDifference;

                        objPremium.NetPremium += dblDifference;

                    }

                    DoPostings = true;
                    //Making new premium xml
                    string strPostedPremium = "<PREMIUM><GROSSPREMIUM> "
                        + Convert.ToString(dblGrossPremium)
                        + "</GROSSPREMIUM><NETPREMIUM>" + dblPremium.ToString()
                        + "</NETPREMIUM><OTHERFEES>" + dblOtherFees.ToString()
                        + "</OTHERFEES><MCCAFEES>" + dblMCCAFees.ToString()
                        + "</MCCAFEES></PREMIUM>";

                    //Saving the Premium XML.
                    returnResult = InsertFinalPremium(CustomerId, PolicyId, PolicyVersionId, "O", ProcessType, strFinalPremium, strPostedPremium, dblPremium);

                    PremiumProcessTableRowID = returnResult;

                    //For Saving Risk Level premium & Inforce Premium
                    for (int i = 0; i < arrPremium.Count; i++)
                    {
                        Premium objPremium = (Premium)(arrPremium[i]);

                        //Ravindra(05-18-2009): If no change in premium do not insert record in PPD
                        if (objPremium.NetPremium == 0)
                            continue;

                        InsertIntoPremiumSubDetails(CustomerId, PolicyId, PolicyVersionId, objPremium.RiskID,
                            objPremium.RiskType, PremiumProcessTableRowID, objPremium.NetPremium,
                            objPremium.MCCAFees + objPremium.OtherFees, objPremium.GrossPremium,
                            objPremium.InforcePremium, objPremium.InforceFees);
                    }

                }
            }

            return arrPremium;
        }

        public System.Collections.ArrayList GetPremiumDetailsChangeInBusiness(int CustomerId, int PolicyId,
          int PolicyVersionId, ArrayList arrOldPremiumXML, ArrayList arrPremiumXML,
          int ProcessType, int LobId, out int PremiumProcessTableRowID, out double dblFeesToReverse,
          out bool DoPostings)
        {
            return GetPremiumDetailsChangeInBusiness(CustomerId, PolicyId,
            PolicyVersionId, arrOldPremiumXML, arrPremiumXML,
            ProcessType, LobId, out PremiumProcessTableRowID, out dblFeesToReverse,
            out DoPostings, 0, "");

        }


        public System.Collections.ArrayList GetPremiumDetailsChangeInBusiness(int CustomerId, int PolicyId,
            int PolicyVersionId, ArrayList arrOldPremiumXML, ArrayList arrPremiumXML,
            int ProcessType, int LobId, out int PremiumProcessTableRowID, out double dblFeesToReverse,
            out bool DoPostings, int Tran_Type, string MasterPolBillPlanType)
        {
            PremiumProcessTableRowID = 0;
            int returnResult = 0;
            dblFeesToReverse = 0;
            ArrayList arrPremium = new ArrayList();
            ArrayList arrOldPremium = new ArrayList();
            ArrayList arrTemp = null;
            DoPostings = false;
            string strFinalPremium = "";
            double dblPremium = 0, dblOtherFees = 0, dblMCCAFees = 0, dblGrossPremium;
            double dblNetFinalPremium = 0, dblTemp = 0;
            dblGrossPremium = 0;
            try
            {
                for (int i = 0; i < arrPremiumXML.Count; i++)
                {
                    PremiumDetails objPremiumdetails = (PremiumDetails)arrPremiumXML[i];
                    arrTemp = GetPremiumFromXML(objPremiumdetails.PremiumXML, objPremiumdetails.QuoteType, out strFinalPremium, out dblNetFinalPremium);
                    for (int j = 0; j < arrTemp.Count; j++)
                    {
                        Premium objPremium = (Premium)arrTemp[j];
                        arrPremium.Add(objPremium);
                    }
                }

                for (int i = 0; i < arrOldPremiumXML.Count; i++)
                {
                    PremiumDetails objPremiumdetails = (PremiumDetails)arrOldPremiumXML[i];
                    arrTemp = GetPremiumFromXML(objPremiumdetails.PremiumXML, objPremiumdetails.QuoteType, out strFinalPremium, out dblNetFinalPremium);
                    for (int j = 0; j < arrTemp.Count; j++)
                    {
                        Premium objPremium = (Premium)arrTemp[j];
                        arrOldPremium.Add(objPremium);
                    }
                }
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
                InsertFinalPremium(CustomerId, PolicyId, PolicyVersionId, "O", ProcessType, strFinalPremium, strFinalPremium, dblNetFinalPremium);
                PremiumProcessTableRowID = returnResult;
                throw ex;
                //return arrPremium;
            }
            // Calculating Differences in NetPremium for each Risks
           // bool IsRiskDeleted;
            //if master policy with monthly auto billing 
            //then no need to calculate premium difference
            if (MasterPolBillPlanType != "MAUTO")
            {
                for (int i = 0; i < arrPremium.Count; i++)
                {
                    for (int j = 0; j < arrOldPremium.Count; j++)
                    {
                        //commented by Lalit for itrack 913 for open items
                        //if (((Premium)arrOldPremium[j]).RiskID == ((Premium)arrPremium[i]).RiskID
                        //    &&
                        //    ((Premium)arrOldPremium[j]).RiskType == ((Premium)arrPremium[i]).RiskType
                        //    )
                        //{
                        //now coverage premium have individual version transaction remium.no need to calculate premium difference from previos version
                        //i-track # 1126
                        ((Premium)arrPremium[i]).GrossPremium = ((Premium)arrPremium[i]).GrossPremium;// -((Premium)arrOldPremium[j]).GrossPremium;
                        ((Premium)arrPremium[i]).NetPremium = ((Premium)arrPremium[i]).NetPremium;// -((Premium)arrOldPremium[j]).NetPremium;
                        ((Premium)arrPremium[i]).OtherFees = ((Premium)arrPremium[i]).OtherFees;// -((Premium)arrOldPremium[j]).OtherFees;
                        ((Premium)arrPremium[i]).MCCAFees = ((Premium)arrPremium[i]).MCCAFees;// -((Premium)arrOldPremium[j]).MCCAFees;

                        ((Premium)arrPremium[i]).IsRiskAdded = false;
                        // }
                    }
                }

                //Ravindra(07-03-2007) Changed implementation of Fees to reverse for deleted risks
                // instead of calculating fes to reverse for deleted risks deleted risks will be added to 
                // premium array with negative values thus fees & premium amount will be reversed automatically
                // for concerned risk taking prorata calulation method in account.

                //Finding deleted Risks
                //Deactivated risk have transaction premium to be refund so deactivated risk preium should be pick from covrage page

                /*
                for (int j = 0; j < arrOldPremium.Count; j++)
                {
                    IsRiskDeleted = true;
                    for (int i = 0; i < arrPremium.Count; i++)
                    {

                        if (((Premium)arrOldPremium[j]).RiskID == ((Premium)arrPremium[i]).RiskID
                            &&
                            ((Premium)arrOldPremium[j]).RiskType == ((Premium)arrPremium[i]).RiskType
                            )
                        {
                            IsRiskDeleted = false;
                        }
                    }
                    if (IsRiskDeleted)
                    {
                        ((Premium)arrOldPremium[j]).IsRiskDeleted = true;

                        /*dblOtherFeesToReserve  = dblOtherFeesToReserve  + ((Premium)arrOldPremium[j]).OtherFees ;
                        dblMCCAFeesToReserve  = dblMCCAFeesToReserve  + ((Premium)arrOldPremium[j]).MCCAFees ;

                    }
                 }
                */
                // Adding deleted risks with negative amount to premium array
                /* Do not consider previous varsion premium .itrack # 1126
                 for (int j = 0; j < arrOldPremium.Count; j++)
                 {
                     if (((Premium)arrOldPremium[j]).IsRiskDeleted)
                     {
                         ((Premium)arrOldPremium[j]).GrossPremium = (((Premium)arrOldPremium[j]).GrossPremium) * -1;
                         ((Premium)arrOldPremium[j]).NetPremium = (((Premium)arrOldPremium[j]).NetPremium) * -1;
                         ((Premium)arrOldPremium[j]).MCCAFees = (((Premium)arrOldPremium[j]).MCCAFees) * -1;
                         ((Premium)arrOldPremium[j]).OtherFees = (((Premium)arrOldPremium[j]).OtherFees) * -1;

                         ((Premium)arrOldPremium[j]).IsRiskAdded = false;

                         arrPremium.Add(arrOldPremium[j]);
                     }
                 }*/
            }

            //dblFeesToReverse = dblMCCAFeesToReserve  + dblOtherFeesToReserve ;

            Premium objTotalPremium = new Premium();

            for (int i = 0; i < arrPremium.Count; i++)
            {

                Premium objPremium = (Premium)(arrPremium[i]);

                //Added By Ravindra(08-02-2007) For Saving  Inforce Premium
                objPremium.InforceFees = objPremium.MCCAFees + objPremium.OtherFees;
                //Added By Ravindra(08-02-2007)  Ends here

                //Added By Ravindra(08-24-2007)
                objPremium.InforcePremium = objPremium.GrossPremium;

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

            dblOtherFees = dblOtherFees - dblFeesToReverse;

            double MinPremium = 0;
            try
            {
                objWrapper.ClearParameteres();
                DataSet ds = objWrapper.ExecuteDataSet("Proc_GetMinPremiumForEnforsement");
                MinPremium = Convert.ToDouble(ds.Tables[0].Rows[0]["MIN_END_PREM"]);
            }
            catch (Exception ex)
            {
                ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(new Exception("Error while fetching Min Premium from System Param", ex));
            }

            //Condition Added By Ravindra(08-16-2007) 
            if (dblPremium != 0)
            {
                //if(Math.Abs(dblPremium) > Math.Abs(MinPremium))
                if (Math.Abs(dblGrossPremium) > Math.Abs(MinPremium))
                {

                    //Total Premum Calculation
                    //CalculateChangeEffectivePremium(CustomerId,PolicyId , PolicyVersionId , objTotalPremium, ref dblTemp );
                    CalculateChangeEffectivePremiumPolicy(CustomerId, PolicyId, PolicyVersionId, objTotalPremium, ref dblTemp, CALLEDFROM);

                    //There could be difference in Total as premium is pro rarted and rounded at risk level
                    if (objTotalPremium.GrossPremium != dblGrossPremium)
                    {
                        Premium objPremium = (Premium)(arrPremium[0]);

                        double dblDifference = objTotalPremium.GrossPremium - dblGrossPremium;
                        dblGrossPremium = dblGrossPremium + dblDifference;

                        objPremium.GrossPremium += dblDifference;

                        dblDifference = (objTotalPremium.MCCAFees + objTotalPremium.OtherFees) - (dblMCCAFees + dblOtherFees) - dblTemp;
                        dblMCCAFees = dblMCCAFees + dblDifference;

                        objPremium.MCCAFees += dblDifference;

                        dblDifference = (dblGrossPremium - dblMCCAFees - dblOtherFees) - dblPremium;

                        dblPremium = dblPremium + dblDifference;

                        objPremium.NetPremium += dblDifference;

                    }

                    DoPostings = true;
                    //Making new premium xml
                    string strPostedPremium = "<PREMIUM><GROSSPREMIUM> "
                        + Convert.ToString(dblGrossPremium)
                        + "</GROSSPREMIUM><NETPREMIUM>" + dblPremium.ToString()
                        + "</NETPREMIUM><OTHERFEES>" + dblOtherFees.ToString()
                        + "</OTHERFEES><MCCAFEES>" + dblMCCAFees.ToString()
                        + "</MCCAFEES></PREMIUM>";

                    //Saving the Premium XML.
                    returnResult = InsertFinalPremium(CustomerId, PolicyId, PolicyVersionId, "O", ProcessType, strFinalPremium, strPostedPremium, dblPremium);

                    PremiumProcessTableRowID = returnResult;

                    //Added By Ravindra(08-02-2007) For Saving Risk Level premium & Inforce Premium
                    for (int i = 0; i < arrPremium.Count; i++)
                    {
                        Premium objPremium = (Premium)(arrPremium[i]);

                        //Ravindra(05-18-2009): If no change in premium do not insert record in PPD
                        if (objPremium.NetPremium == 0)
                            continue;

                        InsertIntoPremiumSubDetails(CustomerId, PolicyId, PolicyVersionId, objPremium.RiskID,
                            objPremium.RiskType, PremiumProcessTableRowID, objPremium.NetPremium,
                            objPremium.MCCAFees + objPremium.OtherFees, objPremium.GrossPremium,
                            objPremium.InforcePremium, objPremium.InforceFees);
                    }
                    //Added By Ravindra(08-02-2007)  Ends here
                }
            }

            return arrPremium;
        }
        /*
                public void CalculateChangeEffectivePremium(int CustomerID,int PolicyID,int PolicyVersionID,
                                Premium objPremium,ref double FeesToReverse)
                {
                    try
                    {
                        objWrapper.ClearParameteres();
                        objWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
                        objWrapper.AddParameter("@POLICY_ID",PolicyID);
                        objWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID);
                        objWrapper.AddParameter("@GROSS_PREMIUM_AMOUNT",objPremium.GrossPremium);
                        objWrapper.AddParameter("@MCCA_FEES",objPremium.MCCAFees);
                        objWrapper.AddParameter("@OTHER_FEES",objPremium.OtherFees);
                        objWrapper.AddParameter("@FEES_TO_REVERSE",FeesToReverse);
                        SqlParameter objChangePremium  = (SqlParameter) objWrapper.AddParameter("@CHANGE_PREMIUM_AMOUNT",
                            SqlDbType.Decimal,ParameterDirection.Output);
                        SqlParameter objChangeMCCAFees  = (SqlParameter) objWrapper.AddParameter("@CHANGE_MCCA_FEES",
                            SqlDbType.Decimal,ParameterDirection.Output);
                        SqlParameter objChangeOSTAFees = (SqlParameter) objWrapper.AddParameter("@CHANGE_OTHER_FEES",
                            SqlDbType.Decimal,ParameterDirection.Output);
                        SqlParameter objChangeReverseFees = (SqlParameter) objWrapper.AddParameter("@CHANGE_FEES_TO_REVERSE",
                            SqlDbType.Decimal,ParameterDirection.Output);

			
                        objWrapper.ExecuteNonQuery("Proc_CalculateChangeInPremium");

                        objPremium.GrossPremium = Convert.ToDouble(objChangePremium.Value); 
                        objPremium.MCCAFees = Convert.ToDouble(objChangeMCCAFees.Value);
                        objPremium.OtherFees = Convert.ToDouble(objChangeOSTAFees.Value);
                        objPremium.NetPremium = objPremium.GrossPremium - objPremium.MCCAFees - objPremium.OtherFees;
                        FeesToReverse = Convert.ToDouble(objChangeReverseFees.Value); 
                        objWrapper.ClearParameteres();
                    }
                    catch(Exception ex)
                    {
                        throw(ex);
                    }
                }
        */
        /*	public System.Collections.ArrayList  GetPremiumDetailsChangeInBusiness(int AppID, int AppVersionID, int PolicyId, int PolicyVersionId
                , int CustomerId, int LobId, out double dblPremium, out double dblOtherFees, out double dblMCCAFees
                , out int PremiumProcessTableRowID, int OldVersion )
            {

                //double dblOldPremium = 0, dblOldOtherFees = 0, dblOldMCCAFees = 0; 
                dblPremium		= 0;
                dblOtherFees	= 0;
                dblMCCAFees		= 0;
                PremiumProcessTableRowID = 0;
                System.Collections.ArrayList arrPremium = new System.Collections.ArrayList();
                System.Collections.ArrayList arrOldPremium = new System.Collections.ArrayList();

                try
                {
                    //Generating the input xml, for rating engine
                    ClsGeneralInformation objGeneralInfo = new ClsGeneralInformation();
                    string strXML = "";
                    string strOldXML = "";
                    string strFinalPremium = "";
                    string strOldPremium = "";

                    try
                    {
                        strXML = objGeneralInfo.GetPolicyInputXML(CustomerId, PolicyId, PolicyVersionId, LobId.ToString());

                        //Retreiving the premium xml
                        //strFinalPremium = GetPremium(strXML, LobId.ToString(), PolicyId, PolicyVersionId, CustomerId);
                        arrPremium = GetPremium(strXML, LobId.ToString(), PolicyId, PolicyVersionId, CustomerId,out strFinalPremium);

                        strOldXML = objGeneralInfo.GetPolicyInputXML(CustomerId, PolicyId, OldVersion, LobId.ToString());
                        arrOldPremium  = GetPremium(strOldXML , LobId.ToString(), PolicyId, OldVersion, CustomerId,out strOldPremium );
                    }
                    catch
                    {
                        strFinalPremium = "<PREMIUM><NETPREMIUM>400</NETPREMIUM><OTHERFEES>0</OTHERFEES><MCCAFEES>0</MCCAFEES></PREMIUM>";
                        Premium objPremium = new Premium();
                        objPremium.RiskID=1;
                        objPremium.NetPremium=400;
                        objPremium.GrossPremium=400;
                        objPremium.OtherFees=0;
                        objPremium.MCCAFees=0;
                        arrPremium.Add(objPremium);
                    }

                    //Retreivng different nodes containing difft amounts from premium xml
                    //Whicih will passed in output variable
    //				System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
    //				doc.LoadXml(strFinalPremium);
    //
    //				System.Xml.XmlNode node;
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

                    //Fetching old premium values
                //	GetPreviousVersionPremiumDetails(PolicyId, OldVersion, CustomerId, out dblOldPremium, out dblOldOtherFees, out dblOldMCCAFees);

                    //Saving the change premium 
    //				dblPremium = dblPremium - dblOldPremium;
    //				dblOtherFees = dblOtherFees - dblOldOtherFees;
    //				dblMCCAFees = dblMCCAFees - dblOldMCCAFees;


                    //Added By Ravindra (09-22-2006)
                    // Calculating Differences in NetPremium for each Risks
                    for(int i=0;i < arrPremium.Count;i++)
                    {
                        for(int j=0;j < arrOldPremium.Count ;j++)
                        {
                            if(((Premium) arrOldPremium[j]).RiskID  == ((Premium)arrPremium[i]).RiskID )
                            {
						
                                ((Premium)arrPremium[i]).GrossPremium = ((Premium)arrPremium[i]).GrossPremium - ((Premium)arrOldPremium[j]).GrossPremium;
                                ((Premium)arrPremium[i]).NetPremium  = ((Premium)arrPremium[i]).NetPremium - ((Premium)arrOldPremium[j]).NetPremium ;
                                ((Premium)arrPremium[i]).OtherFees = ((Premium)arrPremium[i]).OtherFees - ((Premium)arrOldPremium[j]).OtherFees ;
                                ((Premium)arrPremium[i]).MCCAFees = ((Premium)arrPremium[i]).MCCAFees - ((Premium)arrOldPremium[j]).MCCAFees ;
                            }
                        }
                    }
			
                    for(int i=0;i < arrPremium.Count;i++)
                    {
                        dblPremium = ((Premium)arrPremium[i]).NetPremium + dblPremium;
                        dblOtherFees = ((Premium)arrPremium[i]).OtherFees + dblOtherFees;
                        dblMCCAFees = ((Premium)arrPremium[i]).MCCAFees + dblMCCAFees;
                    }


                    //Making new premium xml
                    string strPostedPremium = "<PREMIUM><NETPREMIUM>" +  dblPremium.ToString()
                        + "</NETPREMIUM><OTHERFEES>" + dblOtherFees.ToString()
                        + "</OTHERFEES><MCCAFEES>" + dblMCCAFees.ToString()
                        + "</MCCAFEES></PREMIUM>";

                    //Saving the Premium XML.
                    int returnResult = InsertFinalPremium(CustomerId, PolicyId, PolicyVersionId, "O", strFinalPremium, strPostedPremium);

                    PremiumProcessTableRowID = returnResult;
                    //return 1;
                    return arrPremium;

                }
                catch(Exception objExp)
                {
                    throw (objExp);
                }

            }
    */
        //Commented By Ravindra(08-24-2007) Not Used
        /*
        public void GetPreviousVersionPremiumDetails(int PolicyId, int PolicyVersionId, int CustomerId
            , out double dblPremium, out double dblOtherFees, out double dblMCCAFees)
        {
			
            dblPremium		= 0;
            dblOtherFees	= 0;
            dblMCCAFees		= 0;

            try
            {
                Cms.DataLayer.DataWrapper objWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure);

                objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId);

                DataSet ds = objWrapper.ExecuteDataSet("Proc_GetPolicyPremiumXML");

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string PremiumXml = ds.Tables[0].Rows[0]["PREMIUM_XML"].ToString();

                    //Retreivng different nodes containing difft amounts from premium xml
                    //Whicih will passed in output variable
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    doc.LoadXml(PremiumXml);

                    System.Xml.XmlNode node;


                    node = doc.SelectSingleNode("//PREMIUM/NETPREMIUM");
                    if (node != null)
                        dblPremium = int.Parse(node.InnerXml);

                    node = doc.SelectSingleNode("//PREMIUM/OTHERFEES");
                    if (node != null)
                        dblOtherFees = int.Parse(node.InnerXml);
			
                    node = doc.SelectSingleNode("//PREMIUM/MCCAFEES");
                    if (node != null)
                        dblMCCAFees = int.Parse(node.InnerXml);
                }

            }
            catch (Exception objExp)
            {
                throw(objExp);
            }
        }*/
        #endregion

        #region Check Endorsement Log Before Commit
        /// <summary>
        /// IsEndorsementLogAvailable()
        /// </summary>
        /// <param name="objProcess"></param>
        /// <returns></returns>
        public bool IsEndorsementLogAvailable(ClsProcessInfo objProcess)
        {
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.Text, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
                DataSet ds = objDataWrapper.ExecuteDataSet("Proc_Check_Endorsement_Log " + objProcess.CUSTOMER_ID + "," + objProcess.POLICY_ID + "," + objProcess.NEW_POLICY_VERSION_ID);
                if (ds.Tables.Count == 0)
                    return false;

                if (ds.Tables[0].Rows.Count == 0)
                    return false;

                if (ds.Tables[0].Rows[0][0] == DBNull.Value)
                    return false;

                return true;


            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion


        #region OutOfSequenceEndorsement
        /// <summary>
        /// Checks whether the endorsement id out of sequence endorsement
        /// </summary>
        /// <param name="objProcess"></param>
        /// <returns>True if out of sequence else false</returns>
        public bool OutOfSequenceEndorsement(ClsProcessInfo objProcess)
        {
            /*DataSet ds = DataWrapper.ExecuteDataset(ClsCommon.ConnStr, CommandType.Text, "SELECT MAX(EFFECTIVE_DATETIME) FROM POL_POLICY_PROCESS "
                    + " WHERE POLICY_ID = " + objProcess.POLICY_ID 
                    + " AND CUSTOMER_ID = " + objProcess.CUSTOMER_ID 
                    + " AND PROCESS_ID = " + Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS
                    + " AND PROCESS_STATUS = '" + PROCESS_STATUS_COMPLETE + "'");
            */
            //if (objWrapper==null)
            DataWrapper objDataWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.Text, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            DataSet ds = objDataWrapper.ExecuteDataSet("Proc_GetOutOfSequenceEndorsement " + objProcess.CUSTOMER_ID + "," + objProcess.POLICY_ID + "," + Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS + "," + objProcess.CO_APPLICANT_ID);


            if (ds.Tables.Count == 0)
                return false;

            if (ds.Tables[0].Rows.Count == 0)
                return false;

            if (ds.Tables[0].Rows[0][0] == DBNull.Value)
                return false;


            if (objProcess.EFFECTIVE_DATETIME >= Convert.ToDateTime(ds.Tables[0].Rows[0][0]))
            {
                return false;
            }
            else
            {
                if (objProcess.CO_APPLICANT_ID != int.Parse(ds.Tables[0].Rows[0]["CO_APPLCIANT_ID"].ToString()))
                {
                    return false;
                }

            }



            //Else endorsement is out of sequence endorsement
            return true;

        }
        #endregion

        #region PrintJobs
        /// <summary>
        /// Makes entries for print jobs
        /// </summary>
        /// <param name="objProcess"></param>
        /// <returns>void</returns>
        public void GeneratePolicyEndorsementDocuments(ClsProcessInfo objProcessInfo)
        {
            GeneratePolicyDocuments(objProcessInfo);
        }
        public void PrintJobsForEndorsement(ClsProcessInfo objProcessInfo)
        {
            try
            {
                #region  generating PDFs
                string strInsuredFile = "", strAgencyFile = "", strAddIntFile = "", strFileNameAccord = "", strAutoIdCardPage = "";

                int tmpInsured = objProcessInfo.INSURED;
                int tmpAgencyPrint = objProcessInfo.AGENCY_PRINT;
                int oldVersion = objProcessInfo.POLICY_VERSION_ID;
                objProcessInfo.POLICY_VERSION_ID = objProcessInfo.NEW_POLICY_VERSION_ID;
                if (objProcessInfo.SEND_ALL == (int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.YES))
                    getAdditionalInterest(objProcessInfo);
                //generatting dec page for Additional Interest
                bool IsAddIntExists = IsAddIntExist(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.LOB_ID.ToString(), "POLICY");
                if (objProcessInfo.LOB_ID == ClsRenewalProcess.POLICY_LOB_AUTO || objProcessInfo.LOB_ID == ClsRenewalProcess.POLICY_LOB_HOME)
                {
                    string SucessfullPdf = GeneratePDF(objProcessInfo);
                    strInsuredFile = WordingPDFFileName;
                    strAgencyFile = AgentWordingPDFFileName;
                    strAutoIdCardPage = AutoIdCardPDFFileName;
                    strAddIntFile = AdditionalIntrstPDFFileName;
                    strFileNameAccord = AcordPDFFileName;
                }
                else
                {
                    if (IsAddIntExists)
                        strAddIntFile = GeneratePDF(objProcessInfo, "ADDLINT");
                    //generatting dec page for indured
                    objProcessInfo.INSURED = 1;
                    objProcessInfo.AGENCY_PRINT = 0;
                    strInsuredFile = GeneratePDF(objProcessInfo, "DECPAGE");
                    //generatting dec page for Agency
                    objProcessInfo.INSURED = 0;
                    objProcessInfo.AGENCY_PRINT = 1;
                    strAgencyFile = GeneratePDF(objProcessInfo, "DECPAGE");
                    //generating Accord this is commeting as per Itrack Issue 1858 page 4 but uncomment on 12 june as Pawan Asked
                    strFileNameAccord = GeneratePDF(objProcessInfo, "ACORD");
                    //generating auto ID card
                    strAutoIdCardPage = GeneratePDF(objProcessInfo, BlCommon.ClsCommon.DOCUMENT_TYPE_AUTO_ID_CARD);
                }
                objProcessInfo.INSURED = tmpInsured;
                objProcessInfo.AGENCY_PRINT = tmpAgencyPrint;
                #endregion
                #region adding printing in Print job
                ClsPolicyProcess objPolicyProcesses = new ClsPolicyProcess();
                if (objProcessInfo.INTERNAL_CHANGE == Convert.ToString((int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.NO)))
                {
                    ClsPrintJobsInfo objPrintJobsInfo = GetPrintJobsValues(objProcessInfo);
                    if (objProcessInfo.PRINTING_OPTIONS == (int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.NO))
                    {
                        //customer
                        // do entry in print job other than No print required --chnanged on 3 april 2008

                        objPrintJobsInfo.ENTITY_TYPE = ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_CUSTOMER;
                        objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
                        objPrintJobsInfo.FILE_NAME = strInsuredFile;
                        if (objProcessInfo.INSURED != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString()))
                        {
                            objPrintJobsInfo.FILE_NAME = strInsuredFile;
                            objPrintJobsInfo.ENTITY_TYPE = ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_CUSTOMER;
                            AddPrintJobs(objPrintJobsInfo);
                        }
                        else
                        {
                            // Do entry for non printing files
                            objPrintJobsInfo.FILE_NAME = NoWordingPDFFileName;
                            objPrintJobsInfo.ENTITY_TYPE = ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_CUSTOMER_NOWORD;
                            AddPdfFileLog(objPrintJobsInfo);
                        }
                        //objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_ACORD;						
                        //AddPrintJobs(objPrintJobsInfo);	
                        //agency

                        objPrintJobsInfo.ENTITY_TYPE = ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_AGENCY;
                        objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
                        objPrintJobsInfo.FILE_NAME = strAgencyFile;
                        if (objProcessInfo.AGENCY_PRINT != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString()))
                            AddPrintJobs(objPrintJobsInfo);
                        else // Do entry for non printing files
                            AddPdfFileLog(objPrintJobsInfo);
                        //objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_ACORD;						
                        //AddPrintJobs(objPrintJobsInfo);	
                        //additional interest
                        if (IsAddIntExists == true)
                        {
                            if (objProcessInfo.ADD_INT_ID != "")
                            {
                                string strAddlInt = "";
                                objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_ADDL_INTEREST;
                                objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
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
                                        if (objProcessInfo.ADD_INT != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString()))
                                            AddPrintJobs(objPrintJobsInfo);
                                        else // Do entry for non printing files
                                            AddPdfFileLog(objPrintJobsInfo);
                                    }
                                }
                            }
                        }
                        //auto ID Card
                        if (objProcessInfo.NO_COPIES == 0)
                            objProcessInfo.NO_COPIES = 2; //default value
                        if (objProcessInfo.STATE_ID.ToString() == "14" || objProcessInfo.STATE_CODE == "IN")
                            objProcessInfo.NO_COPIES = 1; //itrack 4869
                        if ((objProcessInfo.LOB_ID == ClsRenewalProcess.POLICY_LOB_AUTO || objProcessInfo.LOB_ID == ClsRenewalProcess.POLICY_LOB_CYCL) && strAutoIdCardPage != "")
                        {
                            objPrintJobsInfo.DOCUMENT_CODE = objPrintJobsInfo.ENTITY_TYPE = "AUTO_ID_CARD";
                            objPrintJobsInfo.FILE_NAME = strAutoIdCardPage;
                            if (objProcessInfo.AUTO_ID_CARD != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString()))
                            {
                                for (int i = 0; i < objProcessInfo.NO_COPIES; i++)
                                    AddPrintJobs(objPrintJobsInfo);
                            }
                            else // Do entry for non printing files
                                AddPdfFileLog(objPrintJobsInfo);
                        }
                        // Do entry for non printing files
                        objPrintJobsInfo.ENTITY_TYPE = ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_CUSTOMER;
                        objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
                        objPrintJobsInfo.FILE_NAME = strInsuredFile;
                        AddPdfFileLog(objPrintJobsInfo);

                        // Do entry for non printing files
                        objPrintJobsInfo.ENTITY_TYPE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_ACORD;
                        objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_ACORD;
                        objPrintJobsInfo.FILE_NAME = strFileNameAccord;
                        AddPdfFileLog(objPrintJobsInfo);
                    }
                    else // Do entry for non printing files
                    {
                        //customer
                        // Do entry for non printing files
                        objPrintJobsInfo.ENTITY_TYPE = ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_CUSTOMER;
                        objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
                        objPrintJobsInfo.FILE_NAME = strInsuredFile;
                        AddPdfFileLog(objPrintJobsInfo);

                        //agency
                        // Do entry for non printing files
                        objPrintJobsInfo.ENTITY_TYPE = ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_AGENCY;
                        objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
                        objPrintJobsInfo.FILE_NAME = strAgencyFile;
                        AddPdfFileLog(objPrintJobsInfo);

                        //additional interest
                        if (IsAddIntExists == true)
                        {
                            if (objProcessInfo.ADD_INT_ID != "")
                            {
                                string strAddlInt = "";
                                objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_ADDL_INTEREST;
                                objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
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
                                        AddPdfFileLog(objPrintJobsInfo);
                                    }
                                }
                            }
                        }
                        //auto ID Card
                        if ((objProcessInfo.LOB_ID == ClsRenewalProcess.POLICY_LOB_AUTO || objProcessInfo.LOB_ID == ClsRenewalProcess.POLICY_LOB_CYCL) && strAutoIdCardPage != "")
                        {
                            objPrintJobsInfo.DOCUMENT_CODE = objPrintJobsInfo.ENTITY_TYPE = "AUTO_ID_CARD";
                            objPrintJobsInfo.FILE_NAME = strAutoIdCardPage;
                            AddPdfFileLog(objPrintJobsInfo);
                        }

                        // Do entry for non printing files
                        objPrintJobsInfo.ENTITY_TYPE = ClsPolicyProcess.PRINT_JOBS_ENTITY_TYPE_CUSTOMER_NOWORD;
                        objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
                        objPrintJobsInfo.FILE_NAME = NoWordingPDFFileName;
                        AddPdfFileLog(objPrintJobsInfo);

                        // Do entry for non printing files
                        objPrintJobsInfo.ENTITY_TYPE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_ACORD;
                        objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_ACORD;
                        objPrintJobsInfo.FILE_NAME = strFileNameAccord;
                        AddPdfFileLog(objPrintJobsInfo);
                    }
                }
                objProcessInfo.POLICY_VERSION_ID = oldVersion;
                #endregion
            }

            catch (Exception exc)
            {
                throw (new Exception("Error while generating PDFs", exc));
            }
            finally
            { }
        }
        #endregion
        /// <summary>
        /// Add diary Entry if Endoresement Commited anyway
        /// </summary>
        /// <param name="objProcessInfo"></param>
        public void AddDiaryEntryForRulesViolation(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
        {
            Cms.Model.Diary.TodolistInfo objModelInfo = new Cms.Model.Diary.TodolistInfo();
            Cms.BusinessLayer.BlCommon.ClsDiary objDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
            try
            {
                objModelInfo.FROMUSERID = objProcessInfo.CREATED_BY;
                objModelInfo.LISTTYPEID = (int)BlCommon.ClsDiary.enumDiaryType.ENDORSEMENT_REQUESTS;//
                objModelInfo.NOTE = objModelInfo.SUBJECTLINE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1962", "");//"Endorsement Process has been committed with some rules violations";
                objModelInfo.NOTE += Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1963", "");//" (Please click on the image to see the list of rules violated)";
                //				objModelInfo.SYSTEMFOLLOWUPID   =   15;
                objModelInfo.LISTOPEN = "Y";
                objModelInfo.CUSTOMER_ID = objProcessInfo.CUSTOMER_ID;
                objModelInfo.POLICY_ID = objProcessInfo.POLICY_ID;
                objModelInfo.POLICY_VERSION_ID = objProcessInfo.NEW_POLICY_VERSION_ID;

                if (IsEODProcess)
                    objModelInfo.CREATED_BY = objModelInfo.MODIFIED_BY = EODUserID;
                else
                    objModelInfo.CREATED_BY = objModelInfo.MODIFIED_BY = objProcessInfo.COMPLETED_BY;
                objModelInfo.CREATED_DATETIME = objModelInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;
                objModelInfo.PROCESS_ROW_ID = objProcessInfo.ROW_ID;
                objModelInfo.RULES_VERIFIED = (int)clsprocess.enumDIARY_RULES_VERIFIED.RULES_VERIFICATION_IMAGE;
                objModelInfo.LOB_ID = objProcessInfo.LOB_ID;
                objModelInfo.MODULE_ID = (int)BlCommon.ClsDiary.enumModuleMaster.POLICY_PROCESS;
                objDiary.DiaryEntryfromSetup(objModelInfo, objWrapper);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding new diary entry for commit anyway", ex);
            }
            finally
            {
                if (objModelInfo != null)
                    objModelInfo = null;
                if (objDiary != null)
                    objDiary = null;
            }

        }

        public string rein_Install(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID)
        {
            try
            {
                string result = "";
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                //string strStoredProc = "PROC_GET_POLICY_REIN_INSLT_DETAILS";
                objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                DataSet ds = objDataWrapper.ExecuteDataSet("PROC_GET_POLICY_REIN_INSLT_DETAILS");

                result = ds.Tables[0].Rows[0]["RESULT"].ToString();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void DeleteEndProcess(int CustomerId, int Policy_Id, int Policy_Version_Id)
        {

            objWrapper.ClearParameteres();
            objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
            objWrapper.AddParameter("@POLICY_ID", Policy_Id);
            objWrapper.AddParameter("@POLICY_VERSION_ID", Policy_Version_Id);
            objWrapper.ExecuteNonQuery("PROC_DeleteEndProcessInfo");
            objWrapper.ClearParameteres();

            DeletePolicyVersion(CustomerId, Policy_Id, Policy_Version_Id);

        }
        public bool StartOutOfSequenceEndorsement(ClsProcessInfo objClsProcessInfo, ClsProcessInfo objoldClsProcessInfo, out int PolicyVersionId, out int RowID, out int BasePolicyVrID, string CalledFrom)
        {

            //DataSet dsVersions = null;
            bool Complete = true;
           // string strStatusDes = "", strNewStatus = "";
            try
            {
                DateTime ExpiryDate = objClsProcessInfo.EXPIRY_DATE;
                int CustomerId = objClsProcessInfo.CUSTOMER_ID;
                int PolicyID = objClsProcessInfo.POLICY_ID;
                int CurrentPolicy_Version_Id = objClsProcessInfo.NEW_POLICY_VERSION_ID;
                //int PolicyBaseVersionID;
                string CreatedBy = objClsProcessInfo.CREATED_BY.ToString();
                PolicyVersionId = CurrentPolicy_Version_Id;
                RowID = objClsProcessInfo.ROW_ID;
                BasePolicyVrID = 0;
                //DateTime PolicyExpDate = ExpiryDate;
                //string TransactionDesc = "";
                StringBuilder TransactionD = new StringBuilder();
                DeleteEndProcess(CustomerId, PolicyID, CurrentPolicy_Version_Id);
                if (CalledFrom.ToUpper() == "REISSUE")
                {
                    Complete = ReIssueEndorsement(CustomerId, PolicyID, CurrentPolicy_Version_Id, out PolicyVersionId, out RowID, out BasePolicyVrID, objClsProcessInfo);
                }
                else
                {
                    Complete = CarryForEndorsement(CustomerId, PolicyID, CurrentPolicy_Version_Id, out PolicyVersionId, out RowID, out BasePolicyVrID, objClsProcessInfo);
                }


                //PolicyBaseVersionID = int.Parse(dr["NEW_POLICY_VERSION_ID"].ToString());//dsVersions.Tables[0].Rows
                //RowID = int.Parse(dr["ROW_ID"].ToString());//dsVersions.Tables[0].Rows
                //CreatePolicyNewVersion(PolicyID, PolicyBaseVersionID, CustomerId, CreatedBy, out PolicyVersionId);
                //objClsProcessInfo.NEW_POLICY_VERSION_ID = PolicyVersionId;
                //objClsProcessInfo.ROW_ID = RowID;
                //base.UpdateProcessInformation(objoldClsProcessInfo, objClsProcessInfo);
                //WriteTransactionLog(CustomerId, PolicyID, PolicyVersionId, TransactionDescription.ToString(),
                //    GetTransactionLogDesc(objClsProcessInfo.PROCESS_ID), objClsProcessInfo.COMPLETED_BY, TransactionDescription.ToString(), TransactionChangeXML.ToString());

                //WriteTransactionLog(CustomerId, PolicyID, PolicyVersionId,
                // GetTransactionLogDesc(objClsProcessInfo.PROCESS_ID), objClsProcessInfo.COMPLETED_BY, TransactionD.ToString(), TransactionChangeXML.ToString());


                return Complete;

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        private Boolean StartNewProcess(ClsProcessInfo objProcessInfo, out int NewPolicyVersion, out string TranDesc, string CALLED_FROM)
        {
            try
            {
                //BEging the transaction
                //base.BeginTransaction();

                //Reseting the transaction descrition text
                TransactionDescription = new System.Text.StringBuilder();



                //Creating new version of policy
                int NewVersionID = 0;
                string DisplayVersion = "";
                int rettval = 0;
                if (objProcessInfo.BASE_POLICY_DISP_VERSION.ToString() != "")
                {
                    DisplayVersion = this.CreateDisplayVersion(objProcessInfo.BASE_POLICY_DISP_VERSION.ToString(), CALLED_FROM);
                }

                CreatePolicyNewVersion(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID, objProcessInfo.CREATED_BY.ToString(), out NewVersionID);
                if (DisplayVersion != "")
                    rettval = base.UpdatePolicyDisplayVersion(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, NewVersionID, DisplayVersion);
                if (rettval > 1)
                    TransactionDescription.Append
                        (ClsCommon.FetchGeneralMessage("1476", "") + DisplayVersion + ClsCommon.FetchGeneralMessage("1477", "") + ";");

                //Adding some properties in process info model object
                UpdateProcessInfoModel(objProcessInfo, NewVersionID);

                //Updating the status
                //objProcessInfo.POLICY_CURRENT_STATUS = POLICY_STATUS_UNDER_ENDORSEMENT;
                //objProcessInfo.POLICY_PREVIOUS_STATUS = POLICY_STATUS_UNDER_ENDORSEMENT;
                //Populating the endorsement info model object
                ClsEndorsementDetailInfo objEndorsementInfo = PopulateEndorsementDetailInfo(objProcessInfo);
                //Inserting the record into endorsement log
                AddEndorsementLog(objEndorsementInfo);
                objProcessInfo.ENDORSEMENT_NO = objEndorsementInfo.ENDORSEMENT_NO;
                //Sets the Policy Status of newer version as under endorsement
                string strStatusDes = "";//, strNewStatus = "";

                TransactionDescription.Append(ClsCommon.FetchGeneralMessage("1488", "") + " " + strStatusDes + ".;");
                //objProcessInfo.POLICY_CURRENT_STATUS = strNewStatus;
                //Starting the process 1488
                bool retval = base.StartProcess(objProcessInfo);

                //Writing the transaction log
                //WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID,
                //GetTransactionLogDesc(objProcessInfo.PROCESS_ID), objProcessInfo.COMPLETED_BY, TransactionDescription.ToString(), TransactionChangeXML.ToString());

                NewPolicyVersion = NewVersionID;
                //Commiting the database transaction
                //base.CommitTransaction();
                TranDesc = TransactionDescription.ToString();
                return retval;
            }
            catch (Exception objExp)
            {
                //base.RollbackTransaction();
                throw (new Exception("Error occured while starting endorsement process \n" + objExp.Message));
            }

        }

        private ClsProcessInfo SetProcesInfoForNewVersion(DataRow dr, int RevertBackPolicyVersion, DateTime PolicyExpiryDate, int intENDORSEMENT_OPTION)
        {
            ClsProcessInfo objProcessInfo = new ClsProcessInfo();
            if (dr != null)
            {
                objProcessInfo.POLICY_ID = int.Parse(dr["POLICY_ID"].ToString()); //int.Parse(hidPOLICY_ID.Value);
                objProcessInfo.POLICY_VERSION_ID = int.Parse(dr["NEW_POLICY_VERSION_ID"].ToString());//int.Parse(hidPOLICY_VERSION_ID.Value);
                objProcessInfo.CUSTOMER_ID = int.Parse(dr["CUSTOMER_ID"].ToString());


                if (dr["EFFECTIVE_DATETIME"].ToString() != "")
                    objProcessInfo.EFFECTIVE_DATETIME = ConvertToDate(dr["EFFECTIVE_DATETIME"].ToString());


                objProcessInfo.PROPERTY_INSPECTION_CREDIT = "";

                if (dr["ENDORSEMENT_NO"].ToString() != "")
                    objProcessInfo.ENDORSEMENT_NO = int.Parse(dr["ENDORSEMENT_NO"].ToString());

                objProcessInfo.COMMENTS = "";

                objProcessInfo.NEW_CUSTOMER_ID = int.Parse(dr["CUSTOMER_ID"].ToString());//int.Parse(hidCUSTOMER_ID.Value);
                objProcessInfo.NEW_POLICY_ID = int.Parse(dr["POLICY_ID"].ToString());

                objProcessInfo.NEW_POLICY_VERSION_ID = 0;// int.Parse(dr["NEW_POLICY_VERSION_ID"].ToString());

                objProcessInfo.UNDERWRITER = Convert.ToInt32(0);

                objProcessInfo.COMPLETED_BY = int.Parse(dr["CREATED_BY"].ToString());
                objProcessInfo.CREATED_BY = int.Parse(dr["CREATED_BY"].ToString());
                if (dr["POLICY_PREVIOUS_STATUS"].ToString().ToUpper() != "SUSPENDED")
                    objProcessInfo.POLICY_PREVIOUS_STATUS = dr["POLICY_PREVIOUS_STATUS"].ToString();//objProcess.GetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID);
                objProcessInfo.POLICY_CURRENT_STATUS = dr["POLICY_CURRENT_STATUS"].ToString(); //"UENDRS";//objProcess.GetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID);
                objProcessInfo.PROCESS_STATUS = dr["PROCESS_STATUS"].ToString();//"PENDING";

                objProcessInfo.INTERNAL_CHANGE = "";//Convert.ToString((int)(enumYESNO_LOOKUP_CODE.NO));

                objProcessInfo.PRINTING_OPTIONS = 0;// int.Parse(Convert.ToString(hidPrinting_Options.Value) == "" ? "0" : hidPrinting_Options.Value);

                //if (cmbINSURED.SelectedItem != null && cmbINSURED.SelectedItem.Value != "")
                objProcessInfo.INSURED = 0;//cmbINSURED.SelectedItem.Value);

                // if (cmbAGENCY_PRINT.SelectedItem != null && cmbAGENCY_PRINT.SelectedItem.Value != "")
                objProcessInfo.AGENCY_PRINT = 0; //cmbAGENCY_PRINT.SelectedItem.Value);

                // if (cmbADVERSE_LETTER_REQD.SelectedItem != null && cmbADVERSE_LETTER_REQD.SelectedItem.Value != "")
                objProcessInfo.ADVERSE_LETTER_REQD = 0;//int.Parse(cmbADVERSE_LETTER_REQD.SelectedItem.Value);


                objProcessInfo.ADD_INT = 0;//int.Parse(cmbADD_INT.SelectedItem.Value);

                objProcessInfo.SEND_ALL = 0;//enumYESNO_LOOKUP_CODE.YES;


                objProcessInfo.AUTO_ID_CARD = int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString());

                objProcessInfo.NO_COPIES = 0;//int.Parse(txtNO_COPIES.Text.Trim());

                objProcessInfo.LOB_ID = 0;//int.Parse(hidLOB_ID.Value);
                //if (hidSTATE_CODE.Value != "")
                objProcessInfo.STATE_CODE = "";//hidSTATE_CODE.Value;
                //if (hidSTATE_ID.Value != "" && hidSTATE_ID.Value != "0")
                objProcessInfo.STATE_ID = 0;//int.Parse(hidSTATE_ID.Value);

                objProcessInfo.PROCESS_ID = int.Parse(dr["PROCESS_ID"].ToString()); //3;//Convert.ToToInt32(.Value);

                // if (hidROW_ID.Value != null && hidROW_ID.Value != "")
                objProcessInfo.ROW_ID = int.Parse(dr["ROW_ID"].ToString());

                //if (txtCOINSURANCE_NUMBER.Text.Trim() != "")
                objProcessInfo.COINSURANCE_NUMBER = "0";//txtCOINSURANCE_NUMBER.Text.Trim();

                //if (cmbENDORSEMENT_TYPE.SelectedItem != null && cmbENDORSEMENT_TYPE.SelectedValue != "")
                objProcessInfo.ENDORSEMENT_TYPE = int.Parse(dr["ENDORSEMENT_TYPE"].ToString());//int.Parse(cmbENDORSEMENT_TYPE.SelectedValue);
                objProcessInfo.CREATED_DATETIME = DateTime.Now;
                objProcessInfo.REVERT_BACK = RevertBackPolicyVersion.ToString();
                if (dr["EXPIRY_DATE"].ToString() != "")
                    objProcessInfo.EXPIRY_DATE = DateTime.Parse(dr["EXPIRY_DATE"].ToString());
                else
                {
                    objProcessInfo.EXPIRY_DATE = PolicyExpiryDate;
                }
                objProcessInfo.ENDORSEMENT_OPTION = intENDORSEMENT_OPTION;

                objProcessInfo.SOURCE_VERSION_ID = RevertBackPolicyVersion; //record current new version id as source version id of nested created versions

                objProcessInfo.BASE_POLICY_DISP_VERSION = dr["POLICY_DISP_VERSION"].ToString();
            }
            return objProcessInfo;

        }

        private string CreateDisplayVersion(string PolicyDisp_Version, string CALLED_FROM)
        {
            if (CALLED_FROM == "RE_ISSUE")
            {
                string PolicyVers = "";
                string[] arPolicyVers = PolicyDisp_Version.Split('.');
                if (arPolicyVers[0] != null && arPolicyVers[1] != null)
                {
                    string FirstDigit = arPolicyVers[0].ToString();
                    string LastDigit = arPolicyVers[1].ToString();
                    int iLastDigit = LastDigit == "" ? 0 : int.Parse(LastDigit) + 1;
                    PolicyVers = FirstDigit.ToString() + "." + iLastDigit.ToString();
                }
                return PolicyVers;
            }
            else
            {
                string PolicyVers = "";
                PolicyVers = PolicyDisp_Version.ToString() + ".1";
                return PolicyVers;
            }
        }

        private bool CarryForEndorsement(int CustomerId, int PolicyID, int CurrentPolicy_Version_Id, out int PolicyVersionId, out int RowID, out int BasePolicyVrID, ClsProcessInfo objClsProcessInfo)
        {
            bool Complete = true;
            BasePolicyVrID = 0;
            RowID = objClsProcessInfo.ROW_ID;
            DateTime ExpiryDate = objClsProcessInfo.EXPIRY_DATE;
            PolicyVersionId = CurrentPolicy_Version_Id;
            StringBuilder TransactionD = new StringBuilder();
            DataSet dsVersions;
            string TransactionDesc = "", strStatusDes = "", strNewStatus = "";
            dsVersions = GetPolicyEndorsementVersions(CustomerId, PolicyID, CurrentPolicy_Version_Id, objClsProcessInfo.EFFECTIVE_DATETIME, null);
            if (dsVersions != null && dsVersions.Tables.Count > 0 && dsVersions.Tables[0].Rows.Count > 0)
            {
                //delete all endorsement process 

                int i = 1;
                int CountRow = dsVersions.Tables[0].Rows.Count;
                foreach (DataRow dr in dsVersions.Tables[0].Rows)
                {
                    ClsProcessInfo objProcessInfo;
                    objProcessInfo = SetProcesInfoForNewVersion(dr, PolicyVersionId, ExpiryDate, objClsProcessInfo.ENDORSEMENT_OPTION);
                    //if (objClsProcessInfo.ENDORSEMENT_OPTION == OVERRIDE_END)
                    //objProcessInfo.POLICY_VERSION_ID = PolicyVersionId;

                    objProcessInfo.ENDORSEMENT_TYPE = objClsProcessInfo.ENDORSEMENT_TYPE;

                    if (i == 1)
                    {
                        objProcessInfo.EFFECTIVE_DATETIME = objClsProcessInfo.EFFECTIVE_DATETIME;
                        objProcessInfo.PROCESS_STATUS = PROCESS_STATUS_PENDING;//objClsProcessInfo.EFFECTIVE_DATETIME;
                        objProcessInfo.POLICY_CURRENT_STATUS = POLICY_STATUS_UNDER_ENDORSEMENT;//objClsProcessInfo.EFFECTIVE_DATETIME;
                        objProcessInfo.PROCESS_ID = 3;//.POLICY_CURRENT_STATUS = POLICY_STATUS_UNDER_ENDORSEMENT;//objClsProcessInfo.EFFECTIVE_DATETIME;
                    }
                    else
                    {
                        objProcessInfo.POLICY_VERSION_ID = BasePolicyVrID;
                    }


                    if (objProcessInfo != null)
                        if (!StartNewProcess(objProcessInfo, out CurrentPolicy_Version_Id, out TransactionDesc, "CARRYFORWARD"))
                        {
                            throw new Exception("Unable to start the process");
                            //break;
                        }
                    if (i == 1)
                    {
                        PolicyVersionId = CurrentPolicy_Version_Id;
                        RowID = objProcessInfo.ROW_ID;
                        BasePolicyVrID = objProcessInfo.POLICY_VERSION_ID;
                        SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, CurrentPolicy_Version_Id, objProcessInfo.PROCESS_ID, out strStatusDes, out strNewStatus);

                    }
                    else
                    {
                        UpdatePolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, CurrentPolicy_Version_Id, POLICY_STATUS_SYS_GENRATED_END);// objProcessInfo.POLICY_CURRENT_STATUS);
                    }
                    i += 1;
                    TransactionD.Append(TransactionDesc);

                }

            }
            else
                Complete = false;

            return Complete;

        }

        private bool ReIssueEndorsement(int CustomerId, int PolicyID, int CurrentPolicy_Version_Id, out int PolicyVersionId, out int RowID, out int BasePolicyVrID, ClsProcessInfo objClsProcessInfo)
        {
            try
            {
                bool Complete = true;
                BasePolicyVrID = 0;
                RowID = objClsProcessInfo.ROW_ID;
                DateTime ExpiryDate = objClsProcessInfo.EXPIRY_DATE;
                PolicyVersionId = CurrentPolicy_Version_Id;
                StringBuilder TransactionD = new StringBuilder();
                string strTransactionDesc = "";
                string strStatusDes, strNewStatus;

                //objProcessInfo = SetProcesInfoForNewVersion(dr, PolicyVersionId, ExpiryDate, objClsProcessInfo.ENDORSEMENT_OPTION);
                //objClsProcessInfo.POLICY_VERSION_ID = CurrentPolicy_Version_Id;
                objClsProcessInfo.ENDORSEMENT_RE_ISSUE = (int)enumYESNO_LOOKUP_UNIQUE_ID.YES;
                objClsProcessInfo.SOURCE_VERSION_ID = objClsProcessInfo.POLICY_VERSION_ID;
                objClsProcessInfo.CREATED_DATETIME = DateTime.Now;
                //get current current term max version ,display version 
                DataSet DsPolicy = Proc_GetPolicyDisplayVersion(CustomerId, PolicyID, objClsProcessInfo.POLICY_VERSION_ID, "MAXVERISON");
                if (DsPolicy != null && DsPolicy.Tables.Count > 0 && DsPolicy.Tables[0].Rows.Count > 0)
                {
                    objClsProcessInfo.BASE_POLICY_DISP_VERSION = DsPolicy.Tables[0].Rows[0]["POLICY_DISP_VERSION"].ToString();
                }
                if (!StartNewProcess(objClsProcessInfo, out CurrentPolicy_Version_Id, out strTransactionDesc, "RE_ISSUE"))
                {
                    throw new Exception("Unable to start the process");
                    //break;
                }
                else
                {
                    PolicyVersionId = objClsProcessInfo.NEW_POLICY_VERSION_ID;
                    RowID = objClsProcessInfo.ROW_ID;
                    BasePolicyVrID = objClsProcessInfo.POLICY_VERSION_ID;
                    SetPolicyStatus(objClsProcessInfo.CUSTOMER_ID, objClsProcessInfo.POLICY_ID, CurrentPolicy_Version_Id, objClsProcessInfo.PROCESS_ID, out strStatusDes, out strNewStatus);

                }

                return Complete;
            }
            catch (Exception ex) { throw ex; }
        }

        private DataSet Proc_GetPolicyDisplayVersion(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, string CALLED_FROM)
        {
            try
            {
                DataSet DsPolicy = null;
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objWrapper.AddParameter("@POLICY_ID", POLICY_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                objWrapper.AddParameter("@CALLED_FROM", CALLED_FROM);
                DsPolicy = objWrapper.ExecuteDataSet("Proc_GetPolicyDisplayVersion");
                objWrapper.ClearParameteres();
                return DsPolicy;
            }
            catch (Exception ex) { throw ex; }
        }

        public static DataSet ChkEndorsementDate(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, int CO_APPLICANT_ID, DateTime EffectiveDate, DateTime ExpiryDate)
        {
            DataSet ds;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            try
            {
                string strstoreProc = "Proc_GetPolicyEndorsementDiff";
                objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                objDataWrapper.AddParameter("@ENDORSEMENTEFFECTIVE_DATE", EffectiveDate);
                objDataWrapper.AddParameter("@ENDORSEMENTEXPIRY_DATE", ExpiryDate);
                objDataWrapper.AddParameter("@CO_APPLICANT", CO_APPLICANT_ID);
                ds = objDataWrapper.ExecuteDataSet(strstoreProc);
                objDataWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.Dispose();
            }


            return ds;
        }
        public int CancelReissuedVersionBoleto(int iCustomerId, int iPolicyId, int iPolicyVersionId, int iReissuedVersionId, int ModifiedBy)
        {
            string strStoredProc = "Proc_CancelReissuedVersionBoletos";
            DateTime RecordDate = DateTime.Now;
            int CanceledBoletoNo = 0;
            try
            {

                objWrapper.ClearParameteres();
                SqlParameter sParam1 = (SqlParameter)objWrapper.AddParameter("@NO_CANCELLLED_BOLETOS", null, SqlDbType.Int, ParameterDirection.Output);
                objWrapper.AddParameter("@POLICY_ID", iPolicyId);
                objWrapper.AddParameter("@POLICY_VERSION_ID", iPolicyVersionId);
                objWrapper.AddParameter("@CUSTOMER_ID", iCustomerId);
                objWrapper.AddParameter("@RE_ISSUED_VERSION_ID", iReissuedVersionId);
                objWrapper.AddParameter("@MODIFIED_BY", ModifiedBy);
                objWrapper.AddParameter("@LAST_UPDATED_DATETIME", DateTime.Now.Date);
                int returnResult = objWrapper.ExecuteNonQuery(strStoredProc);
                objWrapper.ClearParameteres();
                if (returnResult > 0)
                {
                    CanceledBoletoNo = (int)sParam1.Value;
                }
                return CanceledBoletoNo;
            }
            catch (Exception ex)
            {
                throw (new Exception("Error occured while updating cancelled version boletos. \n" + ex.Message));
            }
        }

        #region Launch Non Pay Endorsement Cancellation

        public bool LaunchAutoEndorsement(ClsProcessInfo objProcessInfo)
        {
            StringBuilder strBuilder = new StringBuilder();
            base.BeginTransaction();
            try
            {
                objProcessInfo.ENDORSEMENT_RE_ISSUE = (int)enumYESNO_LOOKUP_UNIQUE_ID.YES;
                objProcessInfo.PROCESS_TYPE = "NPAYENDCAN";
                if (this.StartProcess(objProcessInfo))
                {

                    DataSet dsversion = Proc_GetPolicyDisplayVersion(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID,
                                                                objProcessInfo.SOURCE_VERSION_ID, null);
                    string PolicydispStatus = "";
                    if (dsversion != null && dsversion.Tables.Count > 0 && dsversion.Tables[0].Rows.Count > 0)
                        PolicydispStatus = dsversion.Tables[0].Rows[0]["POLICY_DISP_VERSION"].ToString();
                    dsversion.Dispose();

                    strBuilder.Append(";Endorsement Cancellation Launched on policy version(" + PolicydispStatus + ")  Sucessfully;");
                    if (DeactivatePolicyRisk(objProcessInfo.CUSTOMER_ID
                                            , objProcessInfo.POLICY_ID
                                            , objProcessInfo.NEW_POLICY_VERSION_ID
                                            , objProcessInfo.LOB_ID, objProcessInfo.SOURCE_VERSION_ID))
                    {
                        strBuilder.Append("Policy version(" + PolicydispStatus + ")  Risk Deactivated;");
                    }

                    WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,
                    GetTransactionLogDesc(objProcessInfo.PROCESS_ID), objProcessInfo.COMPLETED_BY, strBuilder.ToString());
                    base.CommitTransaction();
                    return true;

                }
                else
                {
                    base.RollbackTransaction();
                    return false;
                }
            }
            catch (Exception Ex)
            {
                base.RollbackTransaction();
                throw new Exception("Error While Launch Endorsement Cancellation" + Ex.InnerException);
            }
        }



        public bool DeactivatePolicyRisk(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID,int POLICY_LOB,int SOURCE_VERSION)
        {
            bool Active = true;
            string strstoreProc = "PROC_DEACTIVATE_RISK";
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objWrapper.AddParameter("@POLICY_ID", POLICY_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                objWrapper.AddParameter("@CANCEL_VERSION_ID", SOURCE_VERSION);
                objWrapper.AddParameter("@LOB_ID", POLICY_LOB);
                objWrapper.ExecuteNonQuery(strstoreProc);
                objWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                Active = false;
                throw (ex);
            }
            finally
            {
                objWrapper.ClearParameteres();
                //objWrapper.Dispose();
            }
            return Active;
        }

        #endregion

    }
}