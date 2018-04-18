using System;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlAccount;
using Cms.Model.Policy.Process;
using System.Data;
using System.Data.SqlClient;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Policy;

namespace Cms.BusinessLayer.BlProcess
{
    /// <summary>
    /// Summary description for ClsNewBusinessProcess.
    /// </summary>
    public class ClsNewBusinessProcess : ClsPolicyProcess
    {
        public ClsNewBusinessProcess()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected override bool OnWriteTransactionLog()
        {
            return false;
        }

        public bool SubmitAppStartNBSProcess(int iCustomerId, int iPolId, int iPolVersionId, string strLOB_ID, int userID, string appNumber, out int iretval)
        {
            bool retval = false;
            ClsGeneralInformation.SetPolicyStatus(iCustomerId, iPolId, iPolVersionId, "Suspended", userID, strLOB_ID, appNumber, objWrapper);
            Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
            iretval = 0;
            //int iretval = 0;
            //int AgencyTerminationFlag = AgenyTerminationVerification(iCustomerId, iPolId, iPolVersionId, ClsPolicyProcess.POLICY_NEW_BUSINESS_PROCESS);
            //if (AgencyTerminationFlag != 1)
            //{
            //    iretval = 5;
            //    return false;
            //}

            objProcessInfo.CUSTOMER_ID = iCustomerId;
            objProcessInfo.POLICY_ID = iPolId;
            objProcessInfo.NEW_POLICY_ID = iPolId;
            objProcessInfo.POLICY_VERSION_ID = iPolVersionId;
            objProcessInfo.NEW_POLICY_VERSION_ID = iPolVersionId;
            objProcessInfo.CREATED_BY = userID;
            objProcessInfo.CREATED_DATETIME = DateTime.Now;
            objProcessInfo.LOB_ID = int.Parse(strLOB_ID);
            objProcessInfo.POLICY_PREVIOUS_STATUS = "Suspended";
            objProcessInfo.POLICY_PREVIOUS_STATUS = "Suspended";
            //Default values given for printing of documents so that entry should go to print jobs table				
            objProcessInfo.INSURED = (int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL;
            objProcessInfo.AGENCY_PRINT = (int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL;
            objProcessInfo.ADD_INT = (int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL;
            objProcessInfo.AUTO_ID_CARD = (int)Cms.BusinessLayer.BlProcess.clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL;
            if (objProcessInfo.LOB_ID == (int)enumLOB.AUTOP || objProcessInfo.LOB_ID == (int)enumLOB.CYCL)
                objProcessInfo.NO_COPIES = Cms.BusinessLayer.BlCommon.ClsCommon.PRINT_OPTIONS_AUTO_CYCL_NO_OF_COPIES;
            try
            {

                retval = StartProcess(objProcessInfo);
                //if (retval == true)
                //{
                //    // Check policy mandatory infos if all ok then commit process
                //    Cms.BusinessLayer.BlApplication.ClsRatingAndUnderwritingRules objRules = new Cms.BusinessLayer.BlApplication.ClsRatingAndUnderwritingRules(CarrierSystemID);


                //}
                iretval = 1;
                return retval;
            }
            catch (Exception exp)
            {
                throw (exp);
            }

        }

        #region Overrided functions
        public override bool StartProcess(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
        {
            //Calling the base class start process methos which will
            //insert the record in POL_POLICY_PROCESS table
            //and will do the transaction log entry
            try
            {
                base.BeginTransaction();
                objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_NEW_BUSINESS_PROCESS;
                bool retval = base.StartProcess(objProcessInfo);
                if (retval)
                {
                    //UPDATE COVERAGES FOR PROCESS DEPENDEND in case of Home LOB
                    if (objProcessInfo.LOB_ID == ClsRenewalProcess.POLICY_LOB_HOME)
                    {
                        ClsHomeCoverages objHomeCoverage = new ClsHomeCoverages();
                        objHomeCoverage.UpdateCoveragesByRulePolicy(objWrapper, objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, BlApplication.RuleType.ProcessDependend);
                        objHomeCoverage.Dispose();
                    }
                    WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID
                        , GetTransactionLogDesc(objProcessInfo.PROCESS_ID), objProcessInfo.CREATED_BY, TransactionDescription.ToString(), TransactionChangeXML.ToString());
                }
                base.CommitTransaction();
                return retval;
            }
            catch (Exception objExp)
            {
                base.RollbackTransaction();
                throw (new Exception("Unable to start process.\n" + objExp.Message));
            }

        }

        public void AddDiaryEntryForRulesViolation(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
        {
            Cms.Model.Diary.TodolistInfo objModelInfo = new Cms.Model.Diary.TodolistInfo();
            Cms.BusinessLayer.BlCommon.ClsDiary objDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
            try
            {
                if (IsEODProcess)
                    objModelInfo.FROMUSERID = EODUserID;
                else
                    objModelInfo.FROMUSERID = objProcessInfo.CREATED_BY;
                //				objModelInfo.TOUSERID           =   objProcessInfo.UNDERWRITER;            
                objModelInfo.LISTTYPEID = (int)BlCommon.ClsDiary.enumDiaryType.NEW_BUSINESS_REQUESTS;//7;//New Business Requests
                //				objModelInfo.PRIORITY           =   "M";//Medium Priority
                objModelInfo.NOTE = objModelInfo.SUBJECTLINE = "New Business Process has been committed with some rules violations";
                objModelInfo.NOTE += " (Please click on the image to see the list of rules violated)";
                //				objModelInfo.SYSTEMFOLLOWUPID   =   15;
                objModelInfo.LISTOPEN = "Y";
                objModelInfo.CUSTOMER_ID = objProcessInfo.CUSTOMER_ID;
                objModelInfo.POLICY_ID = objProcessInfo.POLICY_ID;
                objModelInfo.POLICY_VERSION_ID = objProcessInfo.POLICY_VERSION_ID;
                //				objModelInfo.FOLLOWUPDATE       =   System.DateTime.Today.AddDays(7);
                //				objModelInfo.STARTTIME            =	System.DateTime.Now;
                //				objModelInfo.ENDTIME              =	System.DateTime.Today.AddDays(7);
                objModelInfo.RECDATE = DateTime.Now;
                if (IsEODProcess)
                    objModelInfo.CREATED_BY = objModelInfo.MODIFIED_BY = EODUserID;
                else
                    objModelInfo.CREATED_BY = objModelInfo.MODIFIED_BY = objProcessInfo.CREATED_BY;
                objModelInfo.CREATED_DATETIME = objModelInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;
                objModelInfo.PROCESS_ROW_ID = objProcessInfo.ROW_ID;
                objModelInfo.RULES_VERIFIED = (int)clsprocess.enumDIARY_RULES_VERIFIED.RULES_VERIFICATION_IMAGE;
                objModelInfo.LOB_ID = objProcessInfo.LOB_ID;
                objModelInfo.MODULE_ID = (int)BlCommon.ClsDiary.enumModuleMaster.POLICY_PROCESS;
                objDiary.DiaryEntryfromSetup(objModelInfo, objWrapper);
                //objDiary.Add(objModelInfo,base.objWrapper);
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

        /*public bool CommitProcess(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo, string strCalledFrom)
        {
            bool returnValue = CommitProcess(objProcessInfo);
            if(strCalledFrom==Cms.CmsWeb.cmsbase.COMMIT_ANYWAYS_RULES && returnValue==true)
            {
                AddDiaryEntryForRulesViolation(objProcessInfo);
            }
            return returnValue;
        }*/
        public override bool CommitProcess(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
        {
            return CommitProcess(objProcessInfo, ".");
        }
        public override bool CommitProcess(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo, string strCalledFrom)
        {
            bool retval = false;
            int intResult;
            try
            {
                string strAgencyDecPage, strInsuredDecPage, strAcordForm, strAddIntPageConcat, strAutoIdCardPage;
                strAgencyDecPage = strInsuredDecPage = strAcordForm = strAddIntPageConcat = strAutoIdCardPage = "";
                base.BeginTransaction();
                objProcessInfo.PROCESS_ID = ClsPolicyProcess.POLICY_COMMIT_NEW_BUSINESS_PROCESS;
                //Checking the eligibility, for the newer version of policy
                if (base.CheckProcessEligibility(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.PROCESS_ID) != 1)
                {
                    base.RollbackTransaction();
                    ClsPolicyErrMsg.strMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("594");
                    return false;
                }

                //Posting the premium
                string TransDesc, strIsRewritePolicy = "", strPremiumTerm = "F";

                Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
                DataSet dsPolicyVersion = objGenInfo.GetPolicyDetails(objProcessInfo.CUSTOMER_ID, 0, 0, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID);

                if (dsPolicyVersion.Tables[0].Rows.Count > 0)
                {
                    if (dsPolicyVersion.Tables[0].Rows[0]["IS_REWRITE_POLICY"] != null)
                        strIsRewritePolicy = dsPolicyVersion.Tables[0].Rows[0]["IS_REWRITE_POLICY"].ToString();
                }
                dsPolicyVersion.Dispose();
                objGenInfo.Dispose();


                UpdateNewPolicyNo(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.PROCESS_ID);


                if (strIsRewritePolicy == "Y")
                    strPremiumTerm = "O";
                //if(objProcessInfo.LOB_ID == 1)
                //{
                //    intResult = 1;
                //    TransDesc = "";
                //}
                //else
                //{
                intResult = PostPolicyProcessPremium(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID,
                            objProcessInfo.POLICY_VERSION_ID, objProcessInfo.COMPLETED_BY, objProcessInfo.PROCESS_ID,
                            out TransDesc, strPremiumTerm, objProcessInfo.PROCESS_ID.ToString());
                //}

                
                if (intResult == -3 || intResult == -2)
                {
                    base.RollbackTransaction();
                    Cms.BusinessLayer.BlCommon.ClsDiary objClsDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
                    Cms.Model.Diary.TodolistInfo objModel = new Cms.Model.Diary.TodolistInfo();

                    if (intResult == -3)
                    {
                        ClsPolicyErrMsg.strMessage = FetchGeneralMessage("1119", ""); //"Selected Billing plan has been deactivated. Please select new Billing plan.";
                        objModel.SUBJECTLINE = FetchGeneralMessage("1120", "");// "Please select new Billing plan.";
                        objModel.NOTE = FetchGeneralMessage("1119", ""); //"Selected Billing plan has been deactivated.";
                    }
                    else
                    {
                        ClsPolicyErrMsg.strMessage = FetchGeneralMessage("1121", ""); //"Commission not found, so the process could not be committed.";
                        objModel.SUBJECTLINE = FetchGeneralMessage("1122", "");  //"New Business Process Commit Failed.";
                        objModel.NOTE = FetchGeneralMessage("1123", ""); //"No Commission found.";					
                    }

                    //Added (11-July-2008) - iTrack #4478
                    objModel.RECORDED_BY = objProcessInfo.COMPLETED_BY;
                    objModel.FROMUSERID = objProcessInfo.COMPLETED_BY;
                    objModel.PROCESS_ROW_ID = objProcessInfo.ROW_ID;

                    objModel.LISTTYPEID = 15;
                    objModel.POLICY_ID = objProcessInfo.POLICY_ID;
                    objModel.POLICY_VERSION_ID = objProcessInfo.POLICY_VERSION_ID;
                    objModel.CUSTOMER_ID = objProcessInfo.CUSTOMER_ID;
                    if (IsEODProcess)
                        objModel.CREATED_BY = EODUserID;
                    else
                        objModel.CREATED_BY = objProcessInfo.COMPLETED_BY;
                    objModel.CREATED_DATETIME = DateTime.Now;
                    objModel.RECDATE = DateTime.Now;
                    //objModel.FOLLOWUPDATE = DateTime.Now;    
                    objModel.LAST_UPDATED_DATETIME = DateTime.Now;
                    //objModel.RECDATE = DateTime.Now;
                    objModel.MODULE_ID = (int)BlCommon.ClsDiary.enumModuleMaster.POLICY_PROCESS;
                    objModel.LOB_ID = objProcessInfo.LOB_ID;

                    //	objModel.STARTTIME = (System.DateTime)DateTime.Now;
                    //	objModel.ENDTIME = (System.DateTime)DateTime.Now;

                    //objClsDiary.Add(objModel,"");					
                    objClsDiary.DiaryEntryfromSetup(objModel);
                    retval = false;

                }
                else
                {
                    TransactionDescription.Append("\n" + TransDesc + ";");
                    retval = base.CommitProcess(objProcessInfo, "");

                    int AppId, AppVersionId, LobId;
                    GetAppLobDetails(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, out AppId, out AppVersionId, out LobId);
                    /*if (EntryNoticeTable(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID,AppId, AppVersionId) == true)
                    {
                        //Enter the Details in Notice Table Generation.				
                        Cms.Model.Policy.Process.ClsPolicyProcessNoticInfo objProcessNoticeInfo = base.GetProcessNoticeInfo(objProcessInfo);
                        objProcessNoticeInfo.NOTICE_DESCRIPTION = "Premium Notice to be Printed for this Process";
                        objProcessNoticeInfo.NOTICE_TYPE = "NNB";
                        AddProcessNotice(objProcessNoticeInfo, objWrapper);
                        TransactionDescription.Append("\n Notice Page Entry has been done.;");

                        //Enter the Details in for Declartion Page Generation.
                        Cms.Model.Policy.Process.ClsPolicyProcessNoticInfo objNoticeInfo = base.GetProcessNoticeInfo(objProcessInfo);
                        objNoticeInfo.NOTICE_DESCRIPTION = "Declaration Page to be Printed for this Process";
                        objNoticeInfo.NOTICE_TYPE = "DNB";
                        AddProcessNotice(objNoticeInfo, objWrapper);
                        TransactionDescription.Append("\n Declaration Page Entry has been done.;");
                    }*/
                    if (strCalledFrom == Cms.CmsWeb.cmsbase.COMMIT_ANYWAYS_RULES)
                    {
                        //AddDiaryEntryForRulesViolation(objProcessInfo);
                        SetDiaryEntryStatus(objProcessInfo, "N");
                        TransactionDescription.Append(Cms.CmsWeb.cmsbase.COMMIT_ANYWAYS_RULES);
                    }
                    base.WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID,
                        GetTransactionLogDesc(objProcessInfo.PROCESS_ID), objProcessInfo.COMPLETED_BY, TransactionDescription.ToString());

                    //Commit the transaction..we will generate PDF and save the entries after it
                    // Code is commented by Swarup for not submitting process while error in printing error occurred.
                    //base.CommitTransaction();
                    //Generate PDFs
                    //Start the transaction again to generate PDFs and save data at print jobs
                    // Code is commented by Swarup for not submitting process while error in printing error occurred.
                    //base.BeginTransaction();
                    #region Generate Policy Documents/Pdf-Xml
                    if (objProcessInfo.LOB_ID.ToString() == "8")
                    {
                        GenerateAviationPdf(objProcessInfo);
                        base.CommitTransaction();
                        return retval;
                    }
                    //int tmpInsured = objProcessInfo.INSURED;
                    //int tmpAgencyPrint = objProcessInfo.AGENCY_PRINT;	
                    //if (objProcessInfo.SEND_ALL==(int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.YES))
                    //    getAdditionalInterest(objProcessInfo);
                    //ClsPolicyProcess.PrintingErrorFlag = false;
                    try
                    {
                        this.GeneratePolicyDocuments(objProcessInfo); //Commented by Kuldeep onn temporary basis because getting error on policy commit when policy is created from QQ
                        #region Code Commented
                        //    //generatting dec page for Additional Interest
                        //    bool IsAddIntExists=IsAddIntExist(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID,objProcessInfo.LOB_ID.ToString(),"POLICY");
                        //    if(objProcessInfo.LOB_ID==ClsRenewalProcess.POLICY_LOB_AUTO || objProcessInfo.LOB_ID==ClsRenewalProcess.POLICY_LOB_HOME)
                        //    {
                        //        string SucessfullPdf = GeneratePDF(objProcessInfo);
                        //        strInsuredDecPage=WordingPDFFileName;
                        //        strAgencyDecPage=AgentWordingPDFFileName;
                        //        strAutoIdCardPage=AutoIdCardPDFFileName;
                        //        strAddIntPageConcat=AdditionalIntrstPDFFileName;
                        //        strAcordForm=AcordPDFFileName;
                        //    }
                        //    else
                        //    {
                        //        if (IsAddIntExists)
                        //            strAddIntPageConcat = GeneratePDF(objProcessInfo,"ADDLINT");
                        //        objProcessInfo.INSURED =0;
                        //        objProcessInfo.AGENCY_PRINT =1;
                        //        strAgencyDecPage = GeneratePDF(objProcessInfo,"DECPAGE");
                        //        objProcessInfo.INSURED =1;
                        //        objProcessInfo.AGENCY_PRINT =0;
                        //        strInsuredDecPage = GeneratePDF(objProcessInfo,"DECPAGE");
                        //        strAcordForm = GeneratePDF(objProcessInfo,"ACORD");
                        //        strAutoIdCardPage = GeneratePDF(objProcessInfo,BlCommon.ClsCommon.DOCUMENT_TYPE_AUTO_ID_CARD);						
                        //    }
                        //    objProcessInfo.INSURED =tmpInsured;
                        //    objProcessInfo.AGENCY_PRINT =tmpAgencyPrint;
                        //    //string strAgencyCode = GetPDFAgencyCode(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID, "POLICY");
                        //    ClsPrintJobsInfo objPrintJobsInfo = GetPrintJobsValues(objProcessInfo);
                        //    if(objProcessInfo.PRINTING_OPTIONS == (int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.NO))
                        //    {
                        //        // do entry in print job other than No print required --chnanged on 3 april 2008
                        //            objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER;
                        //            objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;						
                        //            objPrintJobsInfo.FILE_NAME = strInsuredDecPage;
                        //        if(objProcessInfo.INSURED != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString()))
                        //            AddPrintJobs(objPrintJobsInfo);	
                        //            //						objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_ACORD;						
                        //            //						objPrintJobsInfo.FILE_NAME = strAcordForm;
                        //            //						AddPrintJobs(objPrintJobsInfo);	
                        //        else // Do entry for non printing files
                        //            AddPdfFileLog(objPrintJobsInfo);
                        //        // do entry in print job other than No print required --chnanged on 3 april 2008
                        //            objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_AGENCY;
                        //            objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
                        //            objPrintJobsInfo.FILE_NAME = strAgencyDecPage;
                        //        if(objProcessInfo.AGENCY_PRINT != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString()))
                        //            AddPrintJobs(objPrintJobsInfo);	
                        //            //						objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_ACORD;						
                        //            //						objPrintJobsInfo.FILE_NAME = strAcordForm;
                        //            //						AddPrintJobs(objPrintJobsInfo);	
                        //        else // Do entry for non printing files
                        //            AddPdfFileLog(objPrintJobsInfo);
                        //        // do entry in print job other than No print required --chnanged on 3 april 2008
                        //        if(objProcessInfo.ADD_INT_ID!="" && IsAddIntExists==true)
                        //        {
                        //            string strAddlInt="";
                        //            objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_ADDL_INTEREST;
                        //            objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;						
                        //            string [] addIntIdArr = objProcessInfo.ADD_INT_ID.Split('~');
                        //            if(addIntIdArr!=null && addIntIdArr.Length>0)
                        //            {
                        //                for(int jCounter=0;jCounter<addIntIdArr.Length;jCounter++)
                        //                {
                        //                    string [] strArr = addIntIdArr[jCounter].Split('^');
                        //                    if(strArr==null || strArr.Length<1)
                        //                        continue;

                        //                    strAddlInt = strArr[0].ToString() + "_" + objPrintJobsInfo.CUSTOMER_ID.ToString() + "_" + objPrintJobsInfo.POLICY_ID.ToString() + "_" + objPrintJobsInfo.POLICY_VERSION_ID.ToString() + "_" + strArr[1].ToString()  + "_" + strArr[2].ToString();
                        //                    objPrintJobsInfo.FILE_NAME = FindAddIntFileName(strAddIntPageConcat,strAddlInt);
                        //                    if(objProcessInfo.ADD_INT != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString()))
                        //                        AddPrintJobs(objPrintJobsInfo);	
                        //                    else // Do entry for non printing files
                        //                        AddPdfFileLog(objPrintJobsInfo);
                        //                }
                        //            }
                        //        }
                        //        if(strAutoIdCardPage!="")
                        //        {
                        //            objPrintJobsInfo.DOCUMENT_CODE = objPrintJobsInfo.ENTITY_TYPE = "AUTO_ID_CARD";						
                        //            objPrintJobsInfo.FILE_NAME = strAutoIdCardPage;
                        //            if(objProcessInfo.AUTO_ID_CARD != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString()) && objProcessInfo.NO_COPIES>0)
                        //            {
                        //                for(int i=0;i<objProcessInfo.NO_COPIES;i++)						
                        //                    AddPrintJobs(objPrintJobsInfo);	
                        //            }
                        //            else // Do entry for non printing files
                        //                AddPdfFileLog(objPrintJobsInfo);
                        //        }
                        //        // Do entry for non printing files
                        //        objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER_NOWORD;
                        //        objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;						
                        //        objPrintJobsInfo.FILE_NAME = NoWordingPDFFileName;
                        //        AddPdfFileLog(objPrintJobsInfo);	

                        //        // Do entry for non printing files
                        //        objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_DOCUMENT_CODE_ACORD;
                        //        objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_ACORD;						
                        //        objPrintJobsInfo.FILE_NAME = strAcordForm;
                        //        AddPdfFileLog(objPrintJobsInfo);	
                        //    }
                        //    else
                        //    {
                        //        // do entry for non printing files 
                        //        objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER;
                        //        objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;						
                        //        objPrintJobsInfo.FILE_NAME = strInsuredDecPage;
                        //        AddPdfFileLog(objPrintJobsInfo);

                        //        // do entry for non printing files
                        //        objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_AGENCY;
                        //        objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
                        //        objPrintJobsInfo.FILE_NAME = strAgencyDecPage;
                        //        AddPdfFileLog(objPrintJobsInfo);	

                        //        // Do entry for non printing files
                        //        if(objProcessInfo.ADD_INT_ID!="" && IsAddIntExists==true)
                        //        {
                        //            string strAddlInt="";
                        //            objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_ADDL_INTEREST;
                        //            objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;						
                        //            string [] addIntIdArr = objProcessInfo.ADD_INT_ID.Split('~');
                        //            if(addIntIdArr!=null && addIntIdArr.Length>0)
                        //            {
                        //                for(int jCounter=0;jCounter<addIntIdArr.Length;jCounter++)
                        //                {
                        //                    string [] strArr = addIntIdArr[jCounter].Split('^');
                        //                    if(strArr==null || strArr.Length<1)
                        //                        continue;

                        //                    strAddlInt = strArr[0].ToString() + "_" + objPrintJobsInfo.CUSTOMER_ID.ToString() + "_" + objPrintJobsInfo.POLICY_ID.ToString() + "_" + objPrintJobsInfo.POLICY_VERSION_ID.ToString() + "_" + strArr[1].ToString()  + "_" + strArr[2].ToString();
                        //                    objPrintJobsInfo.FILE_NAME = FindAddIntFileName(strAddIntPageConcat,strAddlInt);
                        //                    AddPdfFileLog(objPrintJobsInfo);								
                        //                }
                        //            }
                        //        }
                        //        // Do entry for non printing files
                        //        if(strAutoIdCardPage!="")
                        //        {
                        //            objPrintJobsInfo.DOCUMENT_CODE = objPrintJobsInfo.ENTITY_TYPE = "AUTO_ID_CARD";						
                        //            objPrintJobsInfo.FILE_NAME = strAutoIdCardPage;
                        //            AddPdfFileLog(objPrintJobsInfo);	
                        //        }

                        //        // Do entry for non printing files
                        //        objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER_NOWORD;
                        //        objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;						
                        //        objPrintJobsInfo.FILE_NAME = NoWordingPDFFileName;
                        //        AddPdfFileLog(objPrintJobsInfo);	

                        //        // Do entry for non printing files
                        //        objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_DOCUMENT_CODE_ACORD;
                        //        objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_ACORD;						
                        //        objPrintJobsInfo.FILE_NAME = strAcordForm;
                        //        AddPdfFileLog(objPrintJobsInfo);	
                        //    }		
                        #endregion

                        base.CommitTransaction();
                        ClsPolicyProcess.PrintingErrorFlag = false;
                    }
                    catch(Exception ex)
                    {
                        ClsPolicyProcess.PrintingErrorFlag = true;
                        retval = false;
                        base.RollbackTransaction();
                    }
                    #endregion
                }

                return retval;
            }
            catch (System.DllNotFoundException ex)
            {
                if (retval)
                    base.CommitTransaction();
                else
                    base.RollbackTransaction();
                throw (ex);
            }
            catch (Exception objExp)
            {
                base.RollbackTransaction();
                //throw(new Exception("Unable to commit process.\n" + objExp.Message));
                throw (new Exception("Unable to commit process.\n" + objExp.Message.ToString(), objExp)); // + "\n" + objExp.InnerException.ToString() ));
            }

        }

        public override bool RollbackProcess(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
        {

            try
            {
                base.BeginTransaction();
                objProcessInfo.PROCESS_ID = ClsPolicyProcess.POLICY_ROLLBACK_NEW_BUSINESS_PROCESS;

                bool retval = base.RollbackProcess(objProcessInfo);
                if (retval == false)
                {
                    base.RollbackTransaction();
                    return retval;
                }
                string itemStatus = GetSuspensePaymentStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID);
                //MArking the suspense payment to return suspense payment
                if (itemStatus.Trim() == "SP")
                {
                    SetSuspensePaymentStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, "RSP");
                    TransactionDescription.Append("\n" + FetchGeneralMessage("1340", ""));
                }
                //Marking the over payment to return over payment 
                else if (itemStatus.Trim() == "OP")
                {
                    SetSuspensePaymentStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, "ROP");
                    //TransactionDescription.Append("\n Update the Suspense Payment Status.;");
                    TransactionDescription.Append("\n" + FetchGeneralMessage("1340", ""));
                }
                //UPDATE COVERAGES FOR PROCESS DEPENDEND in case of Home LOB
                if (objProcessInfo.LOB_ID == ClsRenewalProcess.POLICY_LOB_HOME)
                {
                    ClsHomeCoverages objHomeCoverage = new ClsHomeCoverages();
                    objHomeCoverage.UpdateCoveragesByRulePolicy(objWrapper, objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, BlApplication.RuleType.ProcessDependend);
                    objHomeCoverage.Dispose();
                }

                bool RejecttranLog = false;
                RejecttranLog = this.RejectPolicy(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CREATED_BY, objProcessInfo.LOB_ID, objWrapper);

                //generate refusal letter
                ClsProductPdfXml objProductPdfXml = new ClsProductPdfXml(this.objWrapper);
                objProductPdfXml.generateRefusalLettter(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CREATED_BY, objProcessInfo.PROCESS_ID, objProcessInfo.ROW_ID);

                //Updating the transaction log
                base.WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID,
                    GetTransactionLogDesc(objProcessInfo.PROCESS_ID), objProcessInfo.COMPLETED_BY, TransactionDescription.ToString());

                //Added By Lalit jan 05,2011
                //New implimentation Rollback as reject policy


                base.CommitTransaction();
                return retval;
            }
            catch (Exception objExp)
            {
                base.RollbackTransaction();
                throw (new Exception(FetchGeneralMessage("1341", "") + ".\n" + objExp.Message));
            }

        }
        /// <summary>
        /// Reject Policy 
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="PolicyId"></param>
        /// <param name="PolicyVersionId"></param>
        /// <param name="objDataWrapper"></param>
        /// <returns></returns>
        public bool RejectPolicy(int CustomerId, int PolicyId, int PolicyVersionId, int UserID, int PolicyLob, DataWrapper objDataWrapper)
        {
            String CalledFrom = "POL";
            ClsPolicyInfo objPolicyInfo = new ClsPolicyInfo();
            objPolicyInfo.CUSTOMER_ID = CustomerId;
            objPolicyInfo.POLICY_ID = PolicyId;
            objPolicyInfo.POLICY_VERSION_ID = PolicyVersionId;
            objPolicyInfo.APP_ID = PolicyId;
            objPolicyInfo.APP_VERSION_ID = PolicyVersionId;
            objPolicyInfo.POLICY_LOB = PolicyLob.ToString();
            objPolicyInfo.CREATED_BY = UserID;
            bool Rejected = true;
            string strProcedure = "Proc_RejectAppPol";
            int RetVal;
            try
            {
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", objPolicyInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", objPolicyInfo.POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", objPolicyInfo.POLICY_VERSION_ID);
                objDataWrapper.AddParameter("@CALLEDFROM", CalledFrom);

                RetVal = objDataWrapper.ExecuteNonQuery(strProcedure);

                if (RetVal > 0)
                {
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();

                    objPolicyInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"policies\aspx\PolicyGeneralInfo.aspx.resx");

                    string strTranXML = objBuilder.GetTransactionLogXML(objPolicyInfo);
                    if (CalledFrom == "APP")
                        objTransactionInfo.TRANS_TYPE_ID = 242;
                    else if (CalledFrom == "POL")
                        objTransactionInfo.TRANS_TYPE_ID = 243;

                    objTransactionInfo.POLICY_ID = objPolicyInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = objPolicyInfo.POLICY_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objPolicyInfo.CUSTOMER_ID;
                    objTransactionInfo.RECORDED_BY = objPolicyInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = "";

                    objTransactionInfo.CHANGE_XML = strTranXML;

                    objDataWrapper.ExecuteNonQuery(objTransactionInfo);
                    //stBuilder.Append();

                    Int32 ListTypeID = (int)ClsDiary.enumDiaryType.POLICY_REJECTED;
                    Int32 Module_ID = (int)ClsDiary.enumModuleMaster.APPLICATION;

                    String SubLine = String.Empty;
                    String Notes = String.Empty;

                    SubLine = FetchGeneralMessage("1161", "");
                    Notes = FetchGeneralMessage("1162", "") + " " + FetchGeneralMessage("1163", "");


                    ClsGeneralInformation objGeninfo = new ClsGeneralInformation();
                    objGeninfo.AddDiaryEntry(objPolicyInfo.CUSTOMER_ID, objPolicyInfo.APP_ID, objPolicyInfo.APP_VERSION_ID, objPolicyInfo.POLICY_ID, objPolicyInfo.POLICY_VERSION_ID, objPolicyInfo.CREATED_BY, Module_ID, ListTypeID, SubLine, Notes, objPolicyInfo.POLICY_LOB);

                }
                else
                {
                    Rejected = false;
                }
            }
            catch (Exception ex) { throw ex; }

            return Rejected;
        }


        public bool DeclinePolicy(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
        {

            try
            {
                base.BeginTransaction();

                objProcessInfo.PROCESS_ID = ClsPolicyProcess.POLICY_DECLINE;
                //Updating the status of application as cancel
                UpdateApplicationStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, "N");
                TransactionDescription.Append("\n "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1474",""));//Application Status has been updated.;

                SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, "REJECT");
                TransactionDescription.Append("\n "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1475",""));//Policy Status has been updated to Cancelled.;

                //Updating the transaction log
                base.WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID,
                    GetTransactionLogDesc(objProcessInfo.PROCESS_ID), objProcessInfo.COMPLETED_BY, TransactionDescription.ToString());
                base.CommitTransaction();
                return true;
            }
            catch (Exception objExp)
            {
                base.RollbackTransaction();
                throw (new Exception("Unable to decline the policy.\n" + objExp.Message));
            }

        }
        #endregion

        private Cms.Model.Policy.Process.ClsPolicyProcessNoticInfo PopulateNoticeInfo(Cms.Model.Policy.Process.ClsProcessInfo objProcess)
        {
            Cms.Model.Policy.Process.ClsPolicyProcessNoticInfo objNotice = new Cms.Model.Policy.Process.ClsPolicyProcessNoticInfo();

            objNotice.POLICY_ID = objProcess.POLICY_ID;
            objNotice.POLICY_VERSION_ID = objProcess.POLICY_VERSION_ID;
            objNotice.CUSTOMER_ID = objProcess.CUSTOMER_ID;
            objNotice.CREATED_BY = objProcess.CREATED_BY;
            objNotice.CREATED_DATETIME = objProcess.CREATED_DATETIME;
            objNotice.NOTICE_DESCRIPTION = "Policy has been issued.";
            objNotice.ROW_ID = objProcess.ROW_ID;
            return objNotice;
        }

        private void UpdateApplicationStatus(int CustomerID, int PolicyID, int PolicyVersionID, string IsActive)
        {

            try
            {
                SetApplicationStatus(CustomerID, PolicyID, PolicyVersionID, IsActive);
                SetPolicyIsActive(CustomerID, PolicyID, PolicyVersionID, IsActive);
            }
            catch (Exception objExp)
            {
                throw (objExp);
            }
        }
        /*
                private Cms.Model.Policy.Process.ClsPrintJobsInfo GetPrintJobsValues(ClsProcessInfo objProcessInfo)
                {
                    Cms.Model.Policy.Process.ClsPrintJobsInfo objPrintJobsInfo = new ClsPrintJobsInfo();
                    objPrintJobsInfo.CUSTOMER_ID = objProcessInfo.CUSTOMER_ID;
                    objPrintJobsInfo.POLICY_ID = objProcessInfo.POLICY_ID;
                    objPrintJobsInfo.POLICY_VERSION_ID = objProcessInfo.POLICY_VERSION_ID;
                    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE;
                    objPrintJobsInfo.PRINT_DATETIME = System.DateTime.Now;
                    objPrintJobsInfo.PRINTED_DATETIME = System.DateTime.Now;
                    if(System.Web.HttpContext.Current.Session["systemId"]!=null && System.Web.HttpContext.Current.Session["systemId"].ToString()!="")
                        objPrintJobsInfo.URL_PATH = ClsPolicyProcess.PRINT_JOBS_URL_PATH + System.Web.HttpContext.Current.Session["systemId"].ToString();
                    else
                        objPrintJobsInfo.URL_PATH = ClsPolicyProcess.PRINT_JOBS_URL_PATH;
			
                    objPrintJobsInfo.ONDEMAND_FLAG = ClsPolicyProcess.PRINT_JOBS_ON_DEMAND_FLAG;						
                    objPrintJobsInfo.CREATED_DATETIME = System.DateTime.Now;
                    objPrintJobsInfo.CREATED_BY = objProcessInfo.CREATED_BY;
                    return objPrintJobsInfo;
                }

        */
        private bool EntryNoticeTable(int CustomerID, int PolicyID, int PolicyVersionID, int AppID, int AppVersionID)
        {
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@APP_ID", AppID);
                objWrapper.AddParameter("@APP_VERSION_ID", AppVersionID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);

                SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@RETVAL", SqlDbType.Int, ParameterDirection.Output);

                int returnResult = objWrapper.ExecuteNonQuery("Proc_GetNoticeTableEntryApplicable");

                returnResult = int.Parse(objSqlParameter.Value.ToString());
                objWrapper.ClearParameteres();

                bool returnValue;
                if (returnResult == 0)
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }


                objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return returnValue;
            }
            catch (Exception objExp)
            {
                throw new Exception("Error while fetching data NoticeTableEntryApplicable", objExp);
            }

        }


        //Added by Ruchika Chauhan on 7-Feb-2012 for TFS # 3643
        public bool ValidateUnderwriterLimits(int CustomerID, int PolicyID, int PolicyVersionID,int BaseCurrency,int Underwriter)
        {
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            bool returnValue;

            try
            {
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);               
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                objWrapper.AddParameter("@BASE_CURRENCY", BaseCurrency);
                objWrapper.AddParameter("@UNDERWRITER", Underwriter);

                SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@RETVAL", SqlDbType.Int, ParameterDirection.Output);

                int returnResult = objWrapper.ExecuteNonQuery("PROC_VALIDATE_UNDERWRITER_LIMITS");

                returnResult = int.Parse(objSqlParameter.Value.ToString());
                objWrapper.ClearParameteres();

                if (returnResult == 1)
                {
                    returnValue = true;
                }
                else
                {
                    returnValue = false;
                }
                objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return returnValue;
            }
            catch (Exception objExp)
            {
                throw new Exception("Error while validating Underwriter Authority Limits", objExp);
            }

        }
    }
}
