/******************************************************************************************
<Author				: -		Pravesh K. chandel
<Start Date			: -		21 feb-2007
<End Date			: -		
<Description		: - 	Buisness Layer for Policy Rewrite Process.
<Review Date		: - 
<Reviewed By		: - 	
*******************************************************************************************/


using System;
using System.Data;
using System.Data.SqlClient;
using Cms.DataLayer;
using Cms.Model.Policy.Process;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using System.Xml;
using Cms.BusinessLayer.BlQuote;
using System.Collections;
using Cms.Model.Quote;

namespace Cms.BusinessLayer.BlProcess
{
    /// <summary>
    /// Summary description for ClsRollbackAfterCommit.
    /// </summary>
    public class ClsRollbackAfterCommit : ClsPolicyProcess
    {
        public ClsRollbackAfterCommit()
        {
            //
            // TODO: Add constructor logic here
            //
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
        /// <summary>
        /// Override the StartProcess Method
        /// </summary>
        /// <param name="objProcessInfo"></param>
        /// <returns></returns>
        public override bool StartProcess(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
        {
            //Calling the base class start process methos which will
            //insert the record in POL_POLICY_PROCESS table
            //and will do the transaction log entry
            try
            {
                base.BeginTransaction();

                //Reseting the transaction descrition text
                TransactionDescription = new System.Text.StringBuilder();

                objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_REVERT_PROCESS;

                //Checking the eligibility
                if (base.CheckProcessEligibility(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.PROCESS_ID) != 1)
                {
                    base.RollbackTransaction();
                    return false;
                }
                //Creating new version of policy
                // find previos version on which Process was commited
                int intNewPolicyVersion = 0;
                string newDispVersion = "";
                //CreatePolicyNewVersion(objProcessInfo.POLICY_ID, objProcessInfo.BASE_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID, objProcessInfo.CREATED_BY.ToString(), out intNewPolicyVersion,out newDispVersion);
                CreatePolicyNewVersion(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID, objProcessInfo.CREATED_BY.ToString(), out intNewPolicyVersion, out newDispVersion);
                objProcessInfo.NEW_POLICY_VERSION_ID = intNewPolicyVersion;
                TransactionDescription.Append(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1482", "") + newDispVersion + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1901", "") + ";"); //("New version (" + newDispVersion + ") of Policy has been created.;");

                //Updating the status
                objProcessInfo.POLICY_CURRENT_STATUS = POLICY_STATUS_UNDER_REVERT;

                //string TransactionDesc = "";
                //Calling the base class function, for starting the process
                bool retVal = base.StartProcess(objProcessInfo);
                if (!retVal)
                {
                    base.RollbackTransaction();
                    return retVal;
                }
                //Sets the Policy Status of newer version as under Revert
                string strPolicyStatusDec = "", strPolicyNewStatus = "";
                SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID, out strPolicyStatusDec, out strPolicyNewStatus);
                //TransactionDescription.Append("Policy status has been updated.;");
                TransactionDescription.Append(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1490", "") + newDispVersion
                    + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1491", "") + "  " + strPolicyStatusDec + ".;");         //("Status of new version (" + newDispVersion + ") of policy has been updated to " + strPolicyStatusDec + ".;");

                //UpdatePolicyDisplayVersion(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,objProcessInfo.BASE_POLICY_DISP_VERSION); 

                //Updating the status of new version of policy
                //SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID);
                //TransactionDescription.Append("Status of new version (" + objProcessInfo.NEW_POLICY_VERSION_ID.ToString ("#.0") 
                //	+ ") of policy has been updated.;");

                //Writing the transaction log
                //WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, 
                //	GetTransactionLogDesc(objProcessInfo.PROCESS_ID) , objProcessInfo.COMPLETED_BY, TransactionDescription.ToString() );
                WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID,
                    Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1916", ""), objProcessInfo.COMPLETED_BY, TransactionDescription.ToString(), TransactionChangeXML.ToString());//"Revert back Process launched Successfully"

                base.CommitTransaction();
                return retVal;
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
                string CustomMessge = "";
                //Beging the transaction
                base.BeginTransaction();

                //Reseting the transaction descrition text
                TransactionDescription = new System.Text.StringBuilder();
                //int LOBID=	Cms.BusinessLayer.BlCommon.ClsCommon.GetPolicyLOBID(objProcessInfo.NEW_CUSTOMER_ID,objProcessInfo.NEW_POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID);           
                //Checking the eligibility, for the newer version of policy
                if (base.CheckProcessEligibility(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID) != 1)
                {
                    base.RollbackTransaction();
                    return false;
                }
                bool retval;
                string[] strRevertProcesInfo = objProcessInfo.LAST_REVERT_BACK.Split('^');
                int intPolicyID = int.Parse(strRevertProcesInfo[1].ToString());
                int intPolicyVersionID = int.Parse(strRevertProcesInfo[2].ToString());
                int intProcessID = int.Parse(strRevertProcesInfo[3].ToString());
                int intRowID = int.Parse(strRevertProcesInfo[4].ToString());
                int intPolicyBaseVersionID = int.Parse(strRevertProcesInfo[5].ToString());
                int intNewVersionId = 0;
                string NewDisplayVersionId = "";
                string strPreviousStatus = "";
                #region get display version of new policy

                Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();

                //Added By Lalit get revert version display version
                DataSet dsRevertVersionDetails = objGenInfo.GetPolicyDetails(objProcessInfo.CUSTOMER_ID, 0, 0, objProcessInfo.POLICY_ID, intPolicyVersionID);
                string strRevertDispVersion = "";
                if (dsRevertVersionDetails != null && dsRevertVersionDetails.Tables.Count > 0 && dsRevertVersionDetails.Tables[0].Rows.Count > 0)
                {
                    if (dsRevertVersionDetails.Tables[0].Rows[0]["POLICY_DISP_VERSION"] != null)
                        strRevertDispVersion = dsRevertVersionDetails.Tables[0].Rows[0]["POLICY_DISP_VERSION"].ToString();
                }
                dsRevertVersionDetails.Dispose();

                DataSet dsPolicyVersion = objGenInfo.GetPolicyDetails(objProcessInfo.CUSTOMER_ID, 0, 0, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID);
                string strDisplayVersion = "";
                if (dsPolicyVersion.Tables[0].Rows.Count > 0)
                {
                    if (dsPolicyVersion.Tables[0].Rows[0]["POLICY_DISP_VERSION"] != null)
                        strDisplayVersion = dsPolicyVersion.Tables[0].Rows[0]["POLICY_DISP_VERSION"].ToString();
                }
                objGenInfo.Dispose();

                #endregion
                #region deleting  This New Version Creasted At launch and creating New Version taking Base Version
                DeletePolicyVersion(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID);
                //TransactionDescription.Append("version (" + strDisplayVersion 
                //	+ ") of policy has been deleted.;");

                //creating New Version taking Base Version of Last Process to Revert.
                CreatePolicyNewVersion(intPolicyID, intPolicyBaseVersionID, objProcessInfo.CUSTOMER_ID, objProcessInfo.CREATED_BY.ToString(), out intNewVersionId, out NewDisplayVersionId,"0","UNDO");
                objProcessInfo.NEW_POLICY_VERSION_ID = intNewVersionId;
                #endregion

                #region R E V E R T     P R E M I U M       P O S T I N G
                //if commited Endorsement Process was rollbacked
                //if (intProcessID==POLICY_COMMIT_ENDORSEMENT_PROCESS)
                //{
                //fetchching all commetd Process		
                DataSet dsPreviousProcess = base.FillPrevousProcessInfo(objProcessInfo, "ALLCOMMIT");
                if (dsPreviousProcess != null)
                {
                    string TranString = "";
                    //Posting Reverse Premium of Commited Process
                    int result = PostReversePremium(objProcessInfo, dsPreviousProcess, intPolicyVersionID, intPolicyBaseVersionID, out TranString);
                    if (result == -2)
                    {
                        base.RollbackTransaction();
                        return false;
                    }

                    //int result = PostReversePremium(objProcessInfo, intPolicyBaseVersionID, out TranString);
                    TransactionDescription.Append(TranString);
                    //fetching Previous Policy Status before process Commit
                    DataSet dsChangedData = dsPreviousProcess.GetChanges(System.Data.DataRowState.Modified);
                    base.UpdatedataSet(dsChangedData, dsChangedData.Tables[0].TableName.ToString());

                    DataRow[] drPrevious = dsPreviousProcess.Tables[0].Select("PROCESS_UNIQUE_ID='" + objProcessInfo.LAST_REVERT_BACK + "'");
                    if (drPrevious.Length > 0)
                        strPreviousStatus = drPrevious[0]["POLICY_PREVIOUS_STATUS"].ToString();
                    //dsPreviousProcess.AcceptChanges();

                }
                #endregion
                #region updating display version and policy status
                //updating display version
                UpdatePolicyDisplayVersion(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, strDisplayVersion);
                //TransactionDescription.Append(ClsCommon.FetchGeneralMessage("1476", "") + strDisplayVersion
                //    + ClsCommon.FetchGeneralMessage("1477", ""));

                //Updating the status of new version of policy
                if (strPreviousStatus == "")
                    strPreviousStatus = GetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, intPolicyVersionID);
                SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, strPreviousStatus);
                //TransactionDescription.Append(ClsCommon.FetchGeneralMessage("1490", "") + strDisplayVersion + ClsCommon.FetchGeneralMessage("1863", ""));
                objProcessInfo.POLICY_CURRENT_STATUS = strPreviousStatus;
                #endregion
                //set effective date of revert process as currnet date
                objProcessInfo.EFFECTIVE_DATETIME = System.DateTime.Now;
                //Insert Endorsemnt Log
                string TranDesc = "";
                int EndorsementNo = 0;

                //objProcessInfo.ENDORSEMENT_NO = EndorsementNo;
                retval = base.CommitProcess(objProcessInfo);
                //Checking the return status
                if (retval == false)
                {
                    //Rollbacking the transaction
                    base.RollbackTransaction();
                    return retval;
                }
                //Updating the status of previous policy as inactive
                UpdatePolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, intPolicyBaseVersionID, objProcessInfo.NEW_POLICY_VERSION_ID, POLICY_STATUS_INACTIVE);
                SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, intPolicyBaseVersionID, POLICY_STATUS_INACTIVE);
                //TransactionDescription.Append("Old version (" + objProcessInfo.POLICY_VERSION_ID.ToString("#.0") 
                //	+ ") of policy marked as inactive.;");
                //Updating the status of new policy as active
                //SetPolicyIsActive (objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, "Y");
                //TransactionDescription.Append("New version (" + objProcessInfo.NEW_POLICY_VERSION_ID.ToString("#.0") 
                //	+ ") of policy marked as active.;");

                #region Add Print Jobs
                /*
				//string strAgencyCode = GetPDFAgencyCode(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID, "POLICY");
				ClsPrintJobsInfo objPrintJobsInfo = GetPrintJobsValues(objProcessInfo);

				if(objProcessInfo.INSURED == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString()))
				{
					objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER;
					objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;						
					AddPrintJobs(objPrintJobsInfo);	
					objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_ACORD;						
					AddPrintJobs(objPrintJobsInfo);	

				}

				if(objProcessInfo.AGENCY_PRINT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString()))
				{
					objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_AGENCY;
					objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
					AddPrintJobs(objPrintJobsInfo);	
					objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_ACORD;						
					AddPrintJobs(objPrintJobsInfo);	
				}

				if(objProcessInfo.ADD_INT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString()))
				{
					objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_ADDL_INTEREST;
					objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;						
					AddPrintJobs(objPrintJobsInfo);	
					objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_ACORD;						
					AddPrintJobs(objPrintJobsInfo);											
					GeneratePDF(objProcessInfo,"ADDLINT");
				}
				objProcessInfo.INSURED = 1;
				objProcessInfo.AGENCY_PRINT = 0;
				GeneratePDF(objProcessInfo,"DECPAGE");

				objProcessInfo.INSURED = 0;
				objProcessInfo.AGENCY_PRINT = 1;
				GeneratePDF(objProcessInfo,"DECPAGE");
				GeneratePDF(objProcessInfo,"ACORD");
				/*
				DataSet dsPrintDoc= base.GetPrintDocuments(objProcessInfo.PROCESS_ID)  ; 
				for (int i=0;i<dsPrintDoc.Tables[0].Rows.Count -1;i++)
				{
					string doc_code=dsPrintDoc.Tables[0].Rows[i]["DOCUMENT_CODE"].ToString();   
					objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("ALL","ALL","Agent",doc_code );
					AddPrintJobs(objPrintJobsInfo);	

				}
				dsPrintDoc.Clear();
				dsPrintDoc.Dispose(); 
				*/

                #endregion
                ///Added by Lalit for print endorsement Document,March 21,2011
                #region Print Endorsement Document
                GeneratePolicyDocuments(objProcessInfo);
                #endregion
                string Desc = "";
                Desc = TransactionDescription.ToString().TrimStart(';').ToString();
                CustomMessge = ";" + ClsCommon.FetchGeneralMessage("1806", "") + " = " + strRevertDispVersion.ToString() + ";";
                Desc += CustomMessge;

                //Writing the transaction log
                WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,
                    //ClsCommon.FetchGeneralMessage("1768", "")
                         GetTransactionLogDesc(objProcessInfo.PROCESS_ID)
                        , objProcessInfo.COMPLETED_BY, Desc);

                InsertEndorsementLog(objProcessInfo, out TranDesc, out EndorsementNo);
                TransactionDescription.Append(TranDesc);

                //Committing the transaction
                base.CommitTransaction();
                return retval;
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
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
                    + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1917", "") + ";"); //("New version (" + objProcessInfo.NEW_POLICY_VERSION_ID.ToString("#.0")+ ") of policy has been deleted.;");


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
        private void UpdatePolicyStatus(int CustomerID, int PolicyID, int PolicyBaseVersionID, int PolicyNewVersionID, string PolicyStatus)
        {

            string strStoredProc = "Proc_SetPolicyStatusAfterRevert";
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_BASE_VERSION_ID", PolicyBaseVersionID);
                objWrapper.AddParameter("@NEW_POLICY_VERSION_ID", PolicyNewVersionID);

                objWrapper.AddParameter("@POLICY_STATUS", PolicyStatus);
                objWrapper.ExecuteNonQuery(strStoredProc);
                objWrapper.ClearParameteres();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// Update the Policy Display Version.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="DisplayVersion"></param>
        /// <returns></returns>
        private int UpdatePolicyDisplayVersion(int CustomerID, int PolicyID, int PolicyVersionID, string DisplayVersion)
        {

            objWrapper.ClearParameteres();
            objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
            objWrapper.AddParameter("@POLICY_ID", PolicyID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
            objWrapper.AddParameter("@POLICY_DISP_VERSION", DisplayVersion);
            int result = objWrapper.ExecuteNonQuery("Proc_UpdatePolicyDisplayVersion");
            objWrapper.ClearParameteres();
            return result;
        }
        private DataSet GetPostedPremiumProcessDetails(int CustomerID, int PolicyID, int PolicyVersionID, string ProcessType)
        {

            objWrapper.ClearParameteres();
            objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
            objWrapper.AddParameter("@POLICY_ID", PolicyID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
            objWrapper.AddParameter("@PROCESS_TYPE", ProcessType);
            DataSet ds = objWrapper.ExecuteDataSet("Proc_GetPostedPremiumProcessDetails");
            objWrapper.ClearParameteres();
            return ds;
        }
        private int PostReversePremium(ClsProcessInfo objProcessInfo, DataSet dsPreviousProcess, int intPolicyBaseNewVersionID, int intPolicyBaseVersionID, out string TransactionDesc)
        {
            string strProcessType = "", strVersionIdsToRevert = "";
            TransactionDesc = "";
            try
            {
                for (int i = 0; i < dsPreviousProcess.Tables[0].Rows.Count; i++)
                {
                    //if (dsPreviousProcess.Tables[0].Rows[i]["PROCESS_UNIQUE_ID"].ToString()!=objProcessInfo.LAST_REVERT_BACK && int.Parse(dsPreviousProcess.Tables[0].Rows[i]["POLICY_VERSION_ID"].ToString())> intPolicyBaseVersionID)
                    if (int.Parse(dsPreviousProcess.Tables[0].Rows[i]["NEW_POLICY_VERSION_ID"].ToString()) >= intPolicyBaseNewVersionID)
                    {
                        strProcessType = strProcessType + dsPreviousProcess.Tables[0].Rows[i]["PROCESS_ID"].ToString() + ",";
                    }
                }
                strProcessType = strProcessType.TrimEnd(',');
                if (strProcessType == "")
                    return 0;
                //				DataSet dsPostedPremiumDetails=this.GetPostedPremiumProcessDetails(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,intPolicyBaseVersionID,strProcessType);

                for (int i = 0; i < dsPreviousProcess.Tables[0].Rows.Count; i++)
                {
                    //reverse premioum Posting of Commited PRocess

                    //if (dsPreviousProcess.Tables[0].Rows[i]["PROCESS_UNIQUE_ID"].ToString()!=objProcessInfo.LAST_REVERT_BACK && int.Parse(dsPreviousProcess.Tables[0].Rows[i]["POLICY_VERSION_ID"].ToString())> intPolicyBaseVersionID)
                    if (int.Parse(dsPreviousProcess.Tables[0].Rows[i]["NEW_POLICY_VERSION_ID"].ToString()) >= intPolicyBaseNewVersionID)
                    {
                        dsPreviousProcess.Tables[0].Rows[i].BeginEdit();
                        dsPreviousProcess.Tables[0].Rows[i]["REVERT_BACK"] = "Y";
                        dsPreviousProcess.Tables[0].Rows[i].EndEdit();

                        //DataRow[] drPremium=  dsPostedPremiumDetails.Tables[0].Select("PROCESS_TYPE=" + dsPreviousProcess.Tables[0].Rows[i]["PROCESS_ID"].ToString() + " and Policy_version_id=" + dsPreviousProcess.Tables[0].Rows[i]["NEW_POLICY_VERSION_ID"].ToString());
                        //update policy version as in active
                        if (int.Parse(dsPreviousProcess.Tables[0].Rows[i]["NEW_POLICY_VERSION_ID"].ToString()) != intPolicyBaseVersionID)
                        {
                            SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, int.Parse(dsPreviousProcess.Tables[0].Rows[i]["NEW_POLICY_VERSION_ID"].ToString()), POLICY_STATUS_INACTIVE);
                            strVersionIdsToRevert = strVersionIdsToRevert + dsPreviousProcess.Tables[0].Rows[i]["NEW_POLICY_VERSION_ID"].ToString() + ",";
                        }
                        //						if (drPremium.Length==0)
                        //							continue;
                        //						else
                        //						{
                        //							string strPostedPremiumXML=drPremium[0]["POSTED_PREMIUM_XML"].ToString();
                        //							strPostedPremiumXML=getReversePostedPremiumXML(strPostedPremiumXML);
                        //							string strPremiumXML=drPremium[0]["PREMIUM_XML"].ToString();
                        //							double dblPostedPremium =   double.Parse(drPremium[0]["PREMIUM_AMOUNT"].ToString());
                        //							//do reverse Primium Posting
                        //							int retVal = InsertFinalPremium(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID, "O",objProcessInfo.PROCESS_ID,strPremiumXML,strPostedPremiumXML,-dblPostedPremium);
                        //						}
                        //update policy version as in active
                        //SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, int.Parse(dsPreviousProcess.Tables[0].Rows[i]["NEW_POLICY_VERSION_ID"].ToString()), POLICY_STATUS_INACTIVE);
                    }
                }
                string tranString = "";
                strVersionIdsToRevert = strVersionIdsToRevert.TrimEnd(',');
                //int RetVal=PostPolicyPremiumForRevertProcess(objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID,objProcessInfo.CUSTOMER_ID,DateTime.Now ,objProcessInfo.COMPLETED_BY ,objProcessInfo.PROCESS_ID,intPolicyBaseVersionID,out tranString ,strProcessType);
                //int RetVal = PostPolicyPremiumForRevertProcess(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID, intPolicyBaseVersionID, objProcessInfo.COMPLETED_BY, objProcessInfo.PROCESS_ID, strVersionIdsToRevert);
                int RetVal = PostPolicyRevercePremium(objProcessInfo, intPolicyBaseVersionID, out tranString);
                TransactionDesc = tranString;
                return RetVal;
            }
            catch (Exception ex)
            {
                throw (new Exception(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1915", ""), ex));//"Error While Reverse Premium Posting."//
            }

        }

        private string getReversePostedPremiumXML(string strPostedPremiumXML)
        {
            string strReversePostedPremiumXML = "";
            if (strPostedPremiumXML == "")
                return strReversePostedPremiumXML;

            XmlDocument PremDoc = new XmlDocument();
            PremDoc.LoadXml(strPostedPremiumXML);
            //<PREMIUM><GROSSPREMIUM>1285</GROSSPREMIUM><NETPREMIUM>1285</NETPREMIUM><MCCAFEES>0</MCCAFEES><OTHERFEES>0</OTHERFEES></PREMIUM>
            XmlNode GROSSPREMIUMNode = PremDoc.SelectSingleNode("PREMIUM/GROSSPREMIUM");
            if (GROSSPREMIUMNode != null)
                GROSSPREMIUMNode.InnerText = Convert.ToString((-1 * Convert.ToDouble(GROSSPREMIUMNode.InnerText)));
            XmlNode NetPremiumNode = PremDoc.SelectSingleNode("PREMIUM/NETPREMIUM");
            if (NetPremiumNode != null)
                NetPremiumNode.InnerText = Convert.ToString((-1 * Convert.ToDouble(NetPremiumNode.InnerText)));
            XmlNode OTHERFEESNode = PremDoc.SelectSingleNode("PREMIUM/OTHERFEES");
            if (OTHERFEESNode != null)
                OTHERFEESNode.InnerText = Convert.ToString((-1 * Convert.ToDouble(OTHERFEESNode.InnerText)));
            XmlNode MCCAFEESNode = PremDoc.SelectSingleNode("PREMIUM/MCCAFEES");
            if (MCCAFEESNode != null)
                MCCAFEESNode.InnerText = Convert.ToString((-1 * Convert.ToDouble(MCCAFEESNode.InnerText)));
            strReversePostedPremiumXML = PremDoc.InnerXml;
            return strReversePostedPremiumXML;
        }
        //new approach to revert premium posting with installment adjustment written on 9 may 2008 by pravesh
        public int PostPolicyPremiumForRevertProcess(int PolicyId, int PolicyVersionId, int CustomerId, int OldVersion, int UserID, int ProcessType, string strVersionsToRevert)
        {
            int retVal;
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerId);
                objWrapper.AddParameter("@POLICY_ID", PolicyId);
                objWrapper.AddParameter("@NEW_POLICY_VERSION_ID", PolicyVersionId);
                objWrapper.AddParameter("@REVET_VERSIONS", strVersionsToRevert);
                objWrapper.AddParameter("@PROCESS_ID", ProcessType);
                objWrapper.AddParameter("@USER_ID", UserID);
                objWrapper.AddParameter("@BASE_VERSION", OldVersion);
                objWrapper.ExecuteNonQuery("Proc_PostRevertBackPremium");
                objWrapper.ClearParameteres();

                //Update Written and change in premium in premium split table
                Cms.BusinessLayer.BlQuote.clsSplitPremium objSplitPremium = new Cms.BusinessLayer.BlQuote.clsSplitPremium();
                retVal = objSplitPremium.UpdateWrittenEnforcePremiumSplit(CustomerId, PolicyId, PolicyVersionId, OldVersion, ProcessType.ToString(), objWrapper);
                objSplitPremium.Dispose();
                objWrapper.ClearParameteres();
                return retVal;
            }
            catch (Exception objExp)
            {
                objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (objExp);
            }

        }
        //old approach to revert posting
        public int PostPolicyPremiumForRevertProcess(int PolicyId, int PolicyVersionId, int CustomerId,
            DateTime ChangeEffectiveDate, int UserID, int ProcessType, int OldVersion, out string TransactionDesc, string strProcessType)
        {

            try
            {

                //Fetching the appliation details
                int PremiumProcessTableRowId, retVal = 0;
                //GetAppLobDetails(CustomerId, PolicyId,PolicyVersionId, out AppID, out AppVersionID, out LobId); 
                double dblFeesToReserve = 0, TotalGrossPremium = 0, TotalNetPremium = 0, TotalInforcePremium = 0, Inforcefees = 0, StatsFees = 0;//TotalOther = 0,
                bool IsSamePolicy = false;
                TransactionDesc = "";

                DataSet dsPostedPremiumDetails = this.GetPostedPremiumProcessDetails(CustomerId, PolicyId, OldVersion, strProcessType);
                foreach (DataRow drPremium in dsPostedPremiumDetails.Tables[1].Rows)
                {
                    TotalGrossPremium = TotalGrossPremium + double.Parse(drPremium["GROSS_PREMIUM"].ToString());
                    TotalNetPremium = TotalNetPremium + double.Parse(drPremium["NET_PREMIUM"].ToString());
                    StatsFees = StatsFees + double.Parse(drPremium["STATS_FEES"].ToString());
                    TotalInforcePremium = TotalInforcePremium + double.Parse(drPremium["INFORCE_PREMIUM"].ToString());
                }

                string strPostedPremiumXML = "", strPremiumXML = "";

                double dblPostedPremium = TotalGrossPremium;
                if (dblPostedPremium != 0)
                {
                    strPostedPremiumXML = "<PREMIUM>";
                    strPostedPremiumXML += "<GROSSPREMIUM>" + TotalGrossPremium.ToString() + "</GROSSPREMIUM>";
                    strPostedPremiumXML += "<NETPREMIUM>" + TotalNetPremium.ToString() + "</NETPREMIUM>";
                    strPostedPremiumXML += "<MCCAFEES>" + StatsFees.ToString() + "</MCCAFEES>";
                    strPostedPremiumXML += "<OTHERFEES>" + "0" + "</OTHERFEES>";
                    strPostedPremiumXML += "</PREMIUM>";
                    strPremiumXML = strPostedPremiumXML;
                    strPostedPremiumXML = getReversePostedPremiumXML(strPostedPremiumXML);

                    //do reverse Primium Posting
                    int ProcessRowID = InsertFinalPremium(CustomerId, PolicyId, PolicyVersionId, "O", ProcessType, strPremiumXML, strPostedPremiumXML, -dblPostedPremium);
                    PremiumProcessTableRowId = ProcessRowID;
                    int RiskID = 0;
                    string RiskType = "";
                    System.Collections.ArrayList arrPremium = new System.Collections.ArrayList();
                    foreach (DataRow drPremium in dsPostedPremiumDetails.Tables[1].Rows)
                    {

                        TotalGrossPremium = double.Parse(drPremium["GROSS_PREMIUM"].ToString());
                        TotalNetPremium = double.Parse(drPremium["NET_PREMIUM"].ToString());
                        StatsFees = double.Parse(drPremium["STATS_FEES"].ToString());
                        TotalInforcePremium = double.Parse(drPremium["INFORCE_PREMIUM"].ToString());
                        Inforcefees = double.Parse(drPremium["INFORCE_FEES"].ToString());
                        RiskID = int.Parse(drPremium["RISK_ID"].ToString());
                        RiskType = drPremium["RISK_TYPE"].ToString().Trim();

                        Premium objPremium = new Premium();
                        objPremium.GrossPremium = TotalGrossPremium * -1;
                        objPremium.InforcePremium = TotalInforcePremium * -1;
                        objPremium.RiskID = RiskID;
                        objPremium.RiskType = RiskType;
                        objPremium.InforceFees = Inforcefees * -1;
                        objPremium.OtherFees = 0;
                        objPremium.MCCAFees = 0;
                        objPremium.NetPremium = TotalNetPremium * -1;
                        //objPremium.IsRiskDeleted

                        arrPremium.Add(objPremium);

                        InsertIntoPremiumSubDetails(CustomerId, PolicyId, PolicyVersionId, RiskID, RiskType, ProcessRowID, TotalNetPremium * -1, StatsFees * -1, TotalGrossPremium * -1, TotalInforcePremium * -1, Inforcefees * -1);
                    }



                    for (int i = 0; i < arrPremium.Count; i++)
                    {
                        //sub details sum 

                        Premium objPremium = (Premium)arrPremium[i];

                        int CommissionClass = GetCommissionClass(CustomerId, PolicyId, PolicyVersionId,
                            objPremium.RiskID, objWrapper);

                        //If there is no change in premium amount
                        if (objPremium.NetPremium == 0)
                            continue;

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
                        TotalGrossPremium += objPremium.GrossPremium;
                    }
                }
                //Update Written and change in premium in premium split table
                Cms.BusinessLayer.BlQuote.clsSplitPremium objSplitPremium = new Cms.BusinessLayer.BlQuote.clsSplitPremium();
                objSplitPremium.UpdateWrittenEnforcePremiumSplit(CustomerId, PolicyId, PolicyVersionId, OldVersion, ProcessType.ToString(), objWrapper);
                objSplitPremium.Dispose();
                objWrapper.ClearParameteres();

                //Add to Tran log
                //TransactionDesc+="Total premium amount invoiced $" + TotalGrossPremium.ToString() + ";";
                //Added by Pradeep Kushwaha on 27-July-2010
                TransactionDesc += ClsCommon.FetchGeneralMessage("1117", " " + FormatNumber(TotalGrossPremium.ToString())) + ";";

                return retVal;
            }
            catch (Exception objExp)
            {
                objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (objExp);
            }
        }


        #region Post Reverce premium

        private int PostPolicyRevercePremium(ClsProcessInfo objProcessInfo, int BasVersionId, out string TranDesc)
        {
            int RetVal = 0;//, PremiumProcessTableRowId = 0;
            //double dblFeesToReserve = 0;
            TranDesc = "";
            String OutTranDesc = "";
            try
            {
                //Revert installment premium
                RetVal = PostEndorsementRevercePremiun(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID,
                  BasVersionId, objProcessInfo.COMPLETED_BY);
                if (RetVal > 0)
                {
                    TranDesc = "";

                    //Revert co-insurance premium 
                    base.Insert_COInsurance_Premium(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.COMPLETED_BY);

                    //Revert Ri-insurance premium 
                    base.Insert_ReInsurance_Premium(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID, objProcessInfo.COMPLETED_BY);

                    //int UpdateSplitDetail = 0;

                    ClsGenerateQuote objQuote = new ClsGenerateQuote(mSystemID);
                    //int quodeId;
                    //objQuote.GeneratePolicyQuote(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.LOB_ID.ToString(), out quodeId, objProcessInfo.COMPLETED_BY.ToString());
                    this.GeneratePolicyQote(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID,
                        objProcessInfo.POLICY_VERSION_ID, objProcessInfo.COMPLETED_BY, objProcessInfo.LOB_ID, objProcessInfo.EFFECTIVE_DATETIME, out OutTranDesc);

                    TranDesc = TranDesc + OutTranDesc + ";";

                    //Call Split Premium 
                    Cms.BusinessLayer.BlQuote.clsSplitPremium objSplitPremium = new Cms.BusinessLayer.BlQuote.clsSplitPremium();
                    ArrayList arrPremiumXML = objSplitPremium.SplitPremiumsPol(objProcessInfo.CUSTOMER_ID,
                        objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID.ToString(), objWrapper);


                    ArrayList arrOldPremiumXML = objSplitPremium.GetPremiumXMLFromQuote(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, BasVersionId, objWrapper, "ENDORSE");




                    int iPostItem = 0, UpdateSplitDetail = 0;
                    OutTranDesc = "";
                    objWrapper.ClearParameteres();
                    iPostItem = PostOpenItems(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID,
                      objProcessInfo.CREATED_BY, out OutTranDesc);

                    TranDesc = TranDesc + OutTranDesc + ";";

                    UpdateSplitDetail = objSplitPremium.UpdateWrittenEnforcePremiumSplit(objProcessInfo.CUSTOMER_ID,
                        objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, BasVersionId, objProcessInfo.PROCESS_ID.ToString(), objWrapper);
                    objSplitPremium.Dispose();

                    //Reconcile Open Items after Endorsement Revert Back
                    base.RecocileOpenItems(objProcessInfo, BasVersionId);


                }

                return RetVal;


            }
            catch (Exception Ex) { throw Ex; }

        }

        public int PostEndorsementRevercePremiun(int CUSTOMER_ID, int POLICY_ID,
            int POLICY_VERSION_ID, int PROCESS_ID, int POLICY_BASE_VERSION_ID, int CREATED_BY)
        {
            int RET_VAL = 0;
            string Sp_Name = "Proc_PostPolicyRevertBackPremiun";
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objWrapper.AddParameter("@POLICY_ID", POLICY_ID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                objWrapper.AddParameter("@BASE_VERSION_ID", POLICY_BASE_VERSION_ID);
                objWrapper.AddParameter("@PROCESS_ID", PROCESS_ID);
                objWrapper.AddParameter("@CREATED_BY", CREATED_BY);
                objWrapper.AddParameter("@CREATED_DATETIME", ConvertToDate(DateTime.Now.ToShortDateString()));
                SqlParameter objSqlParam = (SqlParameter)objWrapper.AddParameter("@RET_VAL", RET_VAL, SqlDbType.Int, ParameterDirection.Output);
                int Ret = objWrapper.ExecuteNonQuery(Sp_Name);
                if (Ret > 0)
                {
                    RET_VAL = int.Parse(objSqlParam.Value.ToString());
                }
                objWrapper.ClearParameteres();
                return RET_VAL;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        #endregion

        #region Entry in Endorsement Log Table
        public void InsertEndorsementLog(ClsProcessInfo objProcessInfo, out string TranDesc, out int EndoNo)
        {
            EndoNo = 0;
            ClsEndorsementDetailInfo objEndorsementInfo = PopulateEndorsementDetailInfo(objProcessInfo);
            objEndorsementInfo.ENDORSEMENT_STATUS = "COM";
            objEndorsementInfo.ENDORSEMENT_TYPE = objProcessInfo.ENDORSEMENT_TYPE;
            objEndorsementInfo.ENDORSEMENT_DESC = objProcessInfo.OTHER_REASON;
            objEndorsementInfo.CREATED_DATETIME = objProcessInfo.COMPLETED_DATETIME; //ConvertToDate(DateTime.Now.ToShortDateString());
            objEndorsementInfo.ENDORSEMENT_DATE = objProcessInfo.COMPLETED_DATETIME; ;//ConvertToDate(DateTime.Now.ToShortDateString());
            objEndorsementInfo.CREATED_BY = objProcessInfo.COMPLETED_BY;

            //Updating the edorsement log
            //UpdateEndorsementLog(objEndorsementInfo);
            int EndorsementNo = 0;
            AddEndorsementLog(objEndorsementInfo, out EndorsementNo, objProcessInfo.PROCESS_ID.ToString());
            EndoNo = EndorsementNo;
            objEndorsementInfo.ENDORSEMENT_NO = EndorsementNo;
            string TranID = "";
            if (System.Web.HttpContext.Current.Session["EndorsementTranIds"] != null)
                TranID = System.Web.HttpContext.Current.Session["EndorsementTranIds"].ToString();

            objEndorsementInfo.TRANS_ID = TranID;

            AddEndorsementLogDetails(objEndorsementInfo);
            TranDesc = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1422", "") + ";";

        }
        #endregion

        #region Genrate Qote xml

        private int GeneratePolicyQote(int CustomerId, int PolicyId, int PolicyVersionId,
            int UserId, int LobId, DateTime EffectiveDate, out String OutTranDesc)
        {
            OutTranDesc = "";
            try
            {
                if (EffectiveDate == DateTime.MinValue)
                    EffectiveDate = Convert.ToDateTime("01/01/2010");
                string strInputXML = "", FinalXml = "";
                int policyQuodeID, returnValue;
                ClsGenerateQuote objQuote = new ClsGenerateQuote(mSystemID);
                string strPolicyQuote = objQuote.GetPolicyQuoteCode(CustomerId, PolicyId, PolicyVersionId);
                strInputXML = objQuote.GetPolicyProductInputXML(CustomerId, PolicyId, PolicyVersionId, LobId, 1, UserId, "");
                FinalXml = objQuote.GetQuoteXML(strInputXML, LobId.ToString());

                if (FinalXml.Trim() != "")
                {
                    //returnString=strOutputXML;
                    //save the quote	
                    //ClsGenerateQuote objGenerateQuote = new ClsGenerateQuote();
                    ClsGeneratePolicyQuoteInfo objGeneratePolicyQuoteInfo = new ClsGeneratePolicyQuoteInfo();
                    objGeneratePolicyQuoteInfo.CUSTOMER_ID = CustomerId;
                    objGeneratePolicyQuoteInfo.POLICY_ID = PolicyId;
                    objGeneratePolicyQuoteInfo.POLICY_VERSION_ID = PolicyVersionId;
                    objGeneratePolicyQuoteInfo.QUOTE_TYPE = strPolicyQuote;
                    objGeneratePolicyQuoteInfo.QUOTE_DESCRIPTION = "Premium"; //"New Quote"
                    objGeneratePolicyQuoteInfo.IS_ACCEPTED = "N";
                    objGeneratePolicyQuoteInfo.QUOTE_XML = FinalXml.Trim();
                    objGeneratePolicyQuoteInfo.CREATED_BY = UserId;
                    objGeneratePolicyQuoteInfo.CREATED_DATETIME = DateTime.Now;
                    objGeneratePolicyQuoteInfo.IS_ACTIVE = "Y";
                    objGeneratePolicyQuoteInfo.QUOTE_INPUT_XML = strInputXML;
                    //Added by Manoj Rathore Sept.08.2009
                    objGeneratePolicyQuoteInfo.RATE_EFFECTIVE_DATE = EffectiveDate;
                    objGeneratePolicyQuoteInfo.BUSINESS_TYPE = "";
                    policyQuodeID = objQuote.Add_Pol(objGeneratePolicyQuoteInfo, objWrapper);
                    returnValue = 1;
                    OutTranDesc = "";
                    //OutTranDesc = "Qote has been genrated";
                }
                else
                {
                    policyQuodeID = 0;
                    returnValue = 3; // 3 = quote not generated.(error)							
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion


        public int CheckClaimStatus(ClsProcessInfo objProcessInfo)
        {
            //objProcessInfo
            //int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, int BASE_VERSION_ID
            string[] strRevertProcesInfo = objProcessInfo.LAST_REVERT_BACK.Split('^');
            int intPolicyID = int.Parse(strRevertProcesInfo[1].ToString());
            int intPolicyVersionID = int.Parse(strRevertProcesInfo[2].ToString());
            int intProcessID = int.Parse(strRevertProcesInfo[3].ToString());
            int intRowID = int.Parse(strRevertProcesInfo[4].ToString());
            int intPolicyBaseVersionID = int.Parse(strRevertProcesInfo[5].ToString());

            int RET_VAL = 0;
            DataWrapper objDataWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.ON);

            objDataWrapper.AddParameter("@CUSTOMER_ID", objProcessInfo.CUSTOMER_ID);
            objDataWrapper.AddParameter("@POLICY_ID", objProcessInfo.POLICY_ID);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", objProcessInfo.NEW_POLICY_VERSION_ID);
            objDataWrapper.AddParameter("@BASE_VERSION_ID", intPolicyBaseVersionID);
            SqlParameter objSqlParam = (SqlParameter)objDataWrapper.AddParameter("@RET_VAL", RET_VAL, SqlDbType.Int, ParameterDirection.Output);
            objDataWrapper.ExecuteDataSet("Proc_GetEndorsementClaimStaus");
            if (objSqlParam.Value.ToString() != "") 
            {
                RET_VAL = (int)objSqlParam.Value;
            }
            return RET_VAL;
        }
    }
}

