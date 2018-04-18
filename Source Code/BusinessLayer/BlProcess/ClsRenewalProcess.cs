/******************************************************************************************
<Author				: -		Vijay Arora
<Start Date			: -		17-01-2006
<End Date			: -		
<Description		: - 	Buisness Layer for Policy Renewal Process.
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
using Cms.CmsWeb;


namespace Cms.BusinessLayer.BlProcess
{
    /// <summary>
    /// Summary description for ClsCancellationProcess.
    /// </summary>
    public class ClsRenewalProcess : ClsPolicyProcess
    {

        public const int POLICY_LOB_HOME = 1;
        public const int POLICY_LOB_AUTO = 2;
        public const int POLICY_LOB_CYCL = 3;
        public const int POLICY_LOB_BOAT = 4;
        public const int POLICY_LOB_UMB = 5;
        public const int POLICY_LOB_RENT = 6;
        public const int POLICY_LOB_GEN = 7;



        public ClsRenewalProcess()
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

        //Changes By Ravindra(07-30-2007)
        //For returning detailed status description 
        public override bool StartProcess(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
        {
            string StatusDescriptor = "";
            return StartProcess(objProcessInfo, out StatusDescriptor);
        }
        public bool StartProcess(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo,
            out string StatusDescriptor)
        {
            //Calling the base class start process methos which will
            //insert the record in POL_POLICY_PROCESS table
            //and will do the transaction log entry
            Cms.BusinessLayer.BlProcess.ClsPolicyErrMsg.strMessage = "";
            try
            {
                StatusDescriptor = "";
                base.BeginTransaction();

                //Reseting the transaction descrition text
                TransactionDescription = new System.Text.StringBuilder();

                objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_RENEWAL_PROCESS;

                //Checking the eligibility
                if (base.CheckProcessEligibility(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.PROCESS_ID) != 1)
                {
                    base.RollbackTransaction();
                    return false;
                }

                ClsNonRenewProcess objNonRenewProcess = new ClsNonRenewProcess();
                //CHECK Agency Termination Verification		BY PRAVESH 
                //if(base.AgenyTerminationVerification(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID) !=1)
                if (base.AgenyTerminationVerification(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID) != 1)
                {
                    //Go to the Non Renewal Process - WK Non Renewal Agy Terminated  
                    bool returnVal = objNonRenewProcess.PrepareProcessObjectAutoStart(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, "AGENCY", objProcessInfo);
                    string strSubjectLine = "";
                    if (returnVal)
                    {
                        strSubjectLine = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1890", ""); //"Renewal Process could not be started as Either Agency Terminated or deactivated and hence  Non Renewal Process started.";
                        Cms.BusinessLayer.BlProcess.ClsPolicyErrMsg.strNonRenewFlag = "True";
                    }
                    else
                    {
                        strSubjectLine = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1891", "");//"Agency Terminated and Non Renewal Process Could not be started on this policy.";
                        Cms.BusinessLayer.BlProcess.ClsPolicyErrMsg.strNonRenewFlag = "False";
                    }
                    //Commentted by Pravesh on 9 jan 08 as per Itrack 2724/2725
                    //Itrack No. 3361 Manoj Rathore (agency terminated)
                    AddDiaryEntry(objProcessInfo, strSubjectLine,Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1892","") , false, objWrapper, BlCommon.ClsDiary.enumDiaryType.AGENCY_TERMINATED.ToString());//"Renewal Expiry date for the agency has been terminated or agency has been deactivated."
                    base.CommitTransaction();
                    Cms.BusinessLayer.BlProcess.ClsPolicyErrMsg.strMessage = strSubjectLine;
                    StatusDescriptor = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1892", ""); // "Renewal Expiry date for the agency has been terminated or agency has been deactivated.";
                    return false;
                }
                //check for do not renewal
                //if (CheckDoNotRenew(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID) ==1)
                if (CheckDoNotRenew(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID) == 1)
                {
                    //Go to  Non Renewal Process see WF  Company (Non Renewal Process) 
                    bool returnVal = objNonRenewProcess.PrepareProcessObjectAutoStart(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, "", objProcessInfo);
                    string strSubjectLine = "";
                    if (returnVal)
                    {
                        strSubjectLine = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1895", "")+"\""+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1896","")+"\""+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1897",""); //"Renewal Process could not be started as This Policy has been checked for \"Do Not Renew\". and hence Non Renewal Process started";
                        Cms.BusinessLayer.BlProcess.ClsPolicyErrMsg.strNonRenewFlag = "True";
                    }
                    else
                    {
                        strSubjectLine = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1893", "");// "Can't launch Renewal and Non Renewal Process could not be started on this policy.";
                        Cms.BusinessLayer.BlProcess.ClsPolicyErrMsg.strNonRenewFlag = "False";
                    }

                    AddDiaryEntry(objProcessInfo, strSubjectLine, Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1898","")+" \""+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1896","")+"\".", false);//"This Policy has been checked for \"Do Not Renew\"."
                    base.CommitTransaction();
                    Cms.BusinessLayer.BlProcess.ClsPolicyErrMsg.strMessage = strSubjectLine;
                    StatusDescriptor = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1898","")+" \""+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1896","")+"\"";// "This Policy has been checked for \"Do Not Renew\"";
                    return false;
                }
                //
                //Creating new version of policy
                int intNewPolicyVersion = 0;
                    string intNewDispVersion = "";
                string TransactionDesc = "";
                System.Collections.ArrayList arChangeBoatIds;
                CreatePolicyNewVersionForRenewal(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID,
                    objProcessInfo.CUSTOMER_ID, objProcessInfo.CREATED_BY.ToString(), out intNewPolicyVersion,
                    out intNewDispVersion, out TransactionDesc, objProcessInfo.LOB_ID, out arChangeBoatIds);
                objProcessInfo.NEW_POLICY_VERSION_ID = intNewPolicyVersion;
                //Added  by Lalit March 14,2011
                //Genrate new application Number on renwal
                #region Genrate new Application No and Update 
                
                ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
                DataSet DsPolicy = objGeneralInformation.GetPolicyDataSet(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, intNewPolicyVersion);
                int AgencyId = 0;
                if (DsPolicy != null && DsPolicy.Tables.Count > 0 && DsPolicy.Tables[0].Rows.Count > 0) 
                {
                    if (DsPolicy.Tables[0].Rows[0]["AGENCY_ID"].ToString()!="")
                    AgencyId = int.Parse(DsPolicy.Tables[0].Rows[0]["AGENCY_ID"].ToString());
                }
                //int AgencyID=
                String NewAppNo = ClsGeneralInformation.GenerateApplicationNumber(objProcessInfo.LOB_ID, AgencyId);

                int Update = objGeneralInformation.UpdatePolicyNo(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID,
                    intNewPolicyVersion, NewAppNo, objProcessInfo.PROCESS_ID, objWrapper);
                if (Update > 0) 
                {
                    TransactionDescription.Append(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1899","") + NewAppNo + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1900","")+" ;"); //"New Application Number : " + NewAppNo + " has been genrated.;"

                }

                #endregion Genrate new Application No and Update



                //TransactionDescription.Append("New version (" + intNewPolicyVersion.ToString("#.0") + ") of Policy has been created.;");
                TransactionDescription.Append(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1476","") + intNewDispVersion +Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1901","") +";");
                TransactionDescription.Append(TransactionDesc);
                ClsWatercraftCoverages objWaterCoverage;
                if ((objProcessInfo.LOB_ID == POLICY_LOB_BOAT || objProcessInfo.LOB_ID == POLICY_LOB_HOME) && arChangeBoatIds != null)
                {
                    objWaterCoverage = new ClsWatercraftCoverages();
                    for (int intBoatCount = 0; intBoatCount < arChangeBoatIds.Count; intBoatCount++)
                    {
                        if (IsEODProcess)
                        {
                            objWaterCoverage.createdby = EODUserID;
                        }
                        objWaterCoverage.UpdateCoveragesByRulePolicy(objWrapper, objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, RuleType.RiskDependent, int.Parse(arChangeBoatIds[intBoatCount].ToString()));
                    }
                    TransactionDescription.Append(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1902","")+";");
                }

                //Updating the status
                objProcessInfo.POLICY_CURRENT_STATUS = POLICY_STATUS_UNDER_RENEW;
                TransactionDesc = "";
                // Property Inflation adjustment 
                PropertyInflationAdjustment(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, out TransactionDesc);
                TransactionDescription.Append(TransactionDesc);
                //fetching MVR in case of Auto and Cyclelob
                if (objProcessInfo.LOB_ID == POLICY_LOB_AUTO || objProcessInfo.LOB_ID == POLICY_LOB_CYCL)
                {

                    DataSet dsUpdatedMVR = RequestMVR(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID);
                    if (dsUpdatedMVR.Tables[0].Rows.Count > 0)
                    {
                        int returnVal = InsertDriverMVR(dsUpdatedMVR, objProcessInfo);
                        if (returnVal > 0)
                        {
                            TransactionDescription.Append("\n "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1903","")+";");
                        }

                    }
                    if (objProcessInfo.LOB_ID == POLICY_LOB_AUTO)
                    {
                        Cms.BusinessLayer.BlApplication.ClsDriverDetail objAutoDriver = new ClsDriverDetail();
                        objAutoDriver.UpdateVehicleClassPolNew(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objWrapper);
                        TransactionDescription.Append("\n"+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1904","") +";"); //Vehicle Class has been updated.
                    }
                    else
                    {
                        this.UpdateVehicleClassForMotor(dsUpdatedMVR, objProcessInfo);
                        TransactionDescription.Append("\n "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1905","")+";");//Motorcycle Class has been updated.
                    }
                }
                //
                //Calling the base class function, for starting the process
                bool retVal = base.StartProcess(objProcessInfo);
                if (retVal == true)
                {
                    //if (objProcessInfo.LOB_ID==POLICY_LOB_UMB)
                    //	this.SendUmbrellaRenewalLetter(objProcessInfo);
                    //Sets the Policy Status of newer version as under renwal
                    //SetPolicyStatus(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID ,objProcessInfo.PROCESS_ID);
                    //TransactionDescription.Append("Policy status has been updated.;");
                    //Update the Policy Display Version of Newly created policy version.
                    //UpdatePolicyDisplayVersion(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,"1.0"); 
                    //Updating the status of new version of policy
                    string strStatusDes = "", strNewstatus = "";
                    SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID, out strStatusDes, out strNewstatus);
                    TransactionDescription.Append(Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1490","") + objProcessInfo.NEW_POLICY_VERSION_ID.ToString("#.0")
                        + Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1491","")+" " + strStatusDes + ".;");
                    //Writing the transaction log
                    WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID,
                        GetTransactionLogDesc(objProcessInfo.PROCESS_ID), objProcessInfo.COMPLETED_BY, TransactionDescription.ToString(), TransactionChangeXML.ToString());
                    //WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, 
                    //	"Renewal Start Process completed Successfully", objProcessInfo.COMPLETED_BY, TransactionDescription.ToString() );

                    base.CommitTransaction();
                }
                else
                {
                    StatusDescriptor = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1894", "");// " Policy not eligible for selected policy.";
                    base.RollbackTransaction();
                }
                //Cms.BusinessLayer.BlProcess.ClsPolicyErrMsg.strMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("685");  //"Renewal process launched successfully.";
                return retVal;
            }
            catch (Exception exc)
            {
                base.RollbackTransaction();
                throw (exc);
            }

        }
        //in case of umbrella Send  Umbrella Renewal Letter UMRENLT 
        public int SendUmbrellaRenewalLetter(Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo)
        {
            string strUnbrellaNoticeFile = "";
            int tmpInsured = objProcessInfo.INSURED;
            int tmpAgencyPrint = objProcessInfo.AGENCY_PRINT;
            int oldVersion = objProcessInfo.POLICY_VERSION_ID;
            objProcessInfo.POLICY_VERSION_ID = objProcessInfo.NEW_POLICY_VERSION_ID;
            ClsPolicyProcess.PrintingErrorFlag = false;
            base.BeginTransaction();
            try
            {
                //generating PDF for Umbrella Renewal Letter UMRENLT 
                strUnbrellaNoticeFile = GeneratePDF(objProcessInfo, enumPROCESS_CANCELLATION_PDF_TYPES.UMB_LETTER.ToString());
                #region Add Print Jobs for Umbrella Renewal Letter UMRENLT

                objProcessInfo.INSURED = tmpInsured;
                objProcessInfo.AGENCY_PRINT = tmpAgencyPrint;
                ClsPrintJobsInfo objPrintJobsInfo = GetPrintJobsValues(objProcessInfo, "NEWVERSION");
                //if (objProcessInfo.PRINTING_OPTIONS ==(int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.NO))
                //{
                if (strUnbrellaNoticeFile != "")
                {
                    objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER;
                    objPrintJobsInfo.FILE_NAME = strUnbrellaNoticeFile;
                    //objPrintJobsInfo.ONDEMAND_FLAG ="Y";UMB ALL ALL UMB_LETTER
                    objPrintJobsInfo.DOCUMENT_CODE = GetCancellationCode("UMB", "ALL", "ALL", "UMB_LETTER");
                    //objPrintJobsInfo.URL_PATH = ClsPolicyProcess.PRINT_JOBS_URL_PATH + objProcessInfo.AGENCY_CODE + "/" + objProcessInfo.CUSTOMER_ID.ToString() + "/" + "POLICY/CANC_NOTICE" + "/" + "final";
                    AddPrintJobs(objPrintJobsInfo);
                }
                //}
                #endregion
                objProcessInfo.POLICY_VERSION_ID = oldVersion;
                base.CommitTransaction();
                return 1;
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                ClsPolicyProcess.PrintingErrorFlag = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return -1;
            }

        }

        /// <summary>
        ///  Property inflation adjustment for Homeowner
        /// </summary>
        /// <param name="objClsProcessInfo"></param>
        private void PropertyInflationAdjustment(int CustomerId, int PolicyId, int PolicyVersionId, out string TransactionDesc)
        {

            try
            {
                string strdwellingIDs = GetDwellingIDs_Pol(CustomerId, PolicyId, PolicyVersionId);
                TransactionDesc = "";
                string[] arrDwellingID = new string[0];
                if (strdwellingIDs != "-1")
                {
                    arrDwellingID = strdwellingIDs.Split('^');
                }
                //update for each dwelling
                for (int iCounter = 0; iCounter < arrDwellingID.Length; iCounter++)
                {
                    objWrapper.AddParameter("@CUSTOMER_ID", CustomerId, SqlDbType.Int);
                    objWrapper.AddParameter("@POLICY_ID", PolicyId, SqlDbType.Int);
                    objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId, SqlDbType.Int);// new version id 
                    objWrapper.AddParameter("@DWELLING_ID", arrDwellingID[iCounter]);
                    SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@TRAN_DESC", null, SqlDbType.VarChar, ParameterDirection.Output, 500);
                    SqlParameter objSqlParameter1 = (SqlParameter)objWrapper.AddParameter("@LOB_ID", null, SqlDbType.NVarChar, ParameterDirection.Output, 5);

                    int retVal = objWrapper.ExecuteNonQuery("Proc_PropertyInflationAdjustment");
                    TransactionDesc = TransactionDesc + objSqlParameter.Value.ToString();
                    if (retVal < 0) continue;
                    // update coverages 
                    if (objSqlParameter1.Value.ToString() == "1")
                    {
                        // for Homeowner
                        ClsHomeCoverages objClsHomeCoverages = new ClsHomeCoverages();
                        if (IsEODProcess)
                        {
                            objClsHomeCoverages.createdby = EODUserID;
                        }
                        objClsHomeCoverages.UpdateCoveragesByRulePolicy(objWrapper, CustomerId, PolicyId, PolicyVersionId, RuleType.RiskDependent, int.Parse(arrDwellingID[iCounter]));
                    }
                    else if (objSqlParameter1.Value.ToString() == "6")
                    {
                        // for Rental
                        ClsHomeCoverages objClsHomeCoverages = new ClsHomeCoverages("RENTAL");
                        if (IsEODProcess)
                        {
                            objClsHomeCoverages.createdby = EODUserID;
                        }

                        objClsHomeCoverages.UpdateCoveragesByRulePolicy(objWrapper, CustomerId, PolicyId, PolicyVersionId, RuleType.RiskDependent, int.Parse(arrDwellingID[iCounter]));
                    }
                    objWrapper.ClearParameteres();
                    if (TransactionDesc != "")
                        TransactionDesc = TransactionDesc +Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1906","") +" ;";
                }


            }
            catch (Exception ex)
            {
                throw (new Exception("Error occured while adjusting property inflation.\n" + ex.Message));
            }
        }

        /// <summary>
        /// Gets a  Location Ids against an application
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns>Location Ids  as a carat separated string . if no location exist then it returns -1</returns>
        private string GetDwellingIDs_Pol(int customerID, int PolicyID, int PolicyVersionID)
        {
            string strStoredProc = "Proc_GetDwellingIDs_Pol";
            try
            {
                //DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
                DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
                string strDwellingID = "-1";
                if (ds.Tables[0] != null && ds.Tables[0].Rows != null)
                {
                    int intCount = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < intCount; i++)
                    {
                        if (i == 0)
                        {
                            strDwellingID = ds.Tables[0].Rows[i][0].ToString();
                        }
                        else
                        {
                            strDwellingID = strDwellingID + '^' + ds.Tables[0].Rows[i][0].ToString();
                        }
                    }
                }
                objWrapper.ClearParameteres();
                return strDwellingID;
            }
            catch (Exception ex)
            {
                throw (new Exception("Error occured while getting  Dwelling Ids.\n" + ex.Message));
            }
        }


        /// <summary>
        /// This function will create new version in case of renewal.
        /// Difference from CreatePolicyNewVersion is that, this function will also set 
        /// Policy inception, effective and expiry date for the new version of the policy
        /// </summary>
        /// <param name="PolicyId"></param>
        /// <param name="PolicyVersionId"></param>
        /// <param name="CustomerId"></param>
        /// <param name="CreatedBy"></param>
        /// <param name="NewVersionID"></param>
        public void CreatePolicyNewVersionForRenewal(int PolicyId, int PolicyVersionId, int CustomerId, string CreatedBy, out int NewVersionID, int LobID)
        {
            string newDispVersion;
            string TranDesc = "";
            CreatePolicyNewVersionForRenewal(PolicyId, PolicyVersionId, CustomerId, CreatedBy, out NewVersionID, out newDispVersion, out TranDesc, LobID);
        }
        public void CreatePolicyNewVersionForRenewal(int PolicyId, int PolicyVersionId, int CustomerId, string CreatedBy, out int NewVersionID, out string NewDisp_version, out string TransDesc, int LobID)
        {
            System.Collections.ArrayList arListBoatChanged;
            CreatePolicyNewVersionForRenewal(PolicyId, PolicyVersionId, CustomerId, CreatedBy, out NewVersionID, out NewDisp_version, out TransDesc, LobID, out arListBoatChanged);
        }
        public void CreatePolicyNewVersionForRenewal(int PolicyId, int PolicyVersionId, int CustomerId, string CreatedBy, out int NewVersionID, out string NewDisp_version, out string TransDesc, int LobID, out System.Collections.ArrayList arListBoatChanged)
        {
            int returnResult = 0;
            TransDesc = "";
            arListBoatChanged = null;
            try
            {
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerId, SqlDbType.Int);
                objWrapper.AddParameter("@POLICY_ID", PolicyId, SqlDbType.Int);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionId, SqlDbType.Int);
                objWrapper.AddParameter("@CREATED_BY", CreatedBy, SqlDbType.Int);
                SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@NEW_VERSION", SqlDbType.Int, ParameterDirection.Output);
                SqlParameter objSqlParameter1 = (SqlParameter)objWrapper.AddParameter("@NEW_DISP_VERSION",null, SqlDbType.NVarChar, ParameterDirection.Output,50);
                SqlParameter objSqlParameter2 = (SqlParameter)objWrapper.AddParameter("@TRAN_DESC", null, SqlDbType.NVarChar, ParameterDirection.Output, 500);

                //Added by Anurag Verma on 21/03/2007 for receiving ineligible coverage count for diary entry
                SqlParameter objSqlParameter3 = (SqlParameter)objWrapper.AddParameter("@INVALID_COVERAGE", SqlDbType.Int, ParameterDirection.Output);
                SqlParameter objSqlParameter4 = (SqlParameter)objWrapper.AddParameter("@COVERAGE_BASE_CHANGED_BOAT_ID", null, SqlDbType.NVarChar, ParameterDirection.Output, 100);
                //1 is passed in case of renewal and 0 is default value
                objWrapper.AddParameter("@Renewal", "1", SqlDbType.Int);

                int new_APP_Version_ID;
                    
                string  New_Disp_version = "";
                returnResult = objWrapper.ExecuteNonQuery("Proc_PolicyCreateNewVersion");

                objWrapper.ClearParameteres();

                new_APP_Version_ID = int.Parse(objSqlParameter.Value.ToString());
                New_Disp_version = objSqlParameter1.Value.ToString();
                if (objSqlParameter2.Value != System.DBNull.Value)
                    TransDesc = objSqlParameter2.Value.ToString();
                NewVersionID = new_APP_Version_ID;
                NewDisp_version = New_Disp_version;

                //Added by Anurag verma on 21/03/2007 for making diary entry
                int intErrorCnt = int.Parse(objSqlParameter3.Value.ToString());
                if (intErrorCnt > 0)
                    base.AddDiaryEntry(CustomerId, PolicyId, new_APP_Version_ID, LobID, Convert.ToInt32(CreatedBy));
                string returnValue = objSqlParameter4.Value.ToString();
                returnValue = returnValue.TrimEnd(',');
                string[] strListBoatChanged = returnValue.Split(',');
                if (strListBoatChanged.Length > 0 && returnValue != "")
                    arListBoatChanged = new System.Collections.ArrayList(strListBoatChanged);

                #region Update Underwriting Tier
                ClsUnderwritingTier objTier = new ClsUnderwritingTier();
                objTier.UpdateUnderwritingTier(CustomerId, PolicyId, new_APP_Version_ID, "POLICY", "Y", objWrapper);
                #endregion


            }
            catch (Exception ex)
            {
                throw (new Exception("Error occured while creating new version of policy.\n" + ex.Message));
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

        /// <summary>
        /// Post the Return Premium
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

        /// <summary>
        /// Override the base commit process.
        /// </summary>
        /// <param name="objProcessInfo"></param>
        /// <returns></returns>
        public override bool CommitProcess(ClsProcessInfo objProcessInfo)
        {
            return CommitProcess(objProcessInfo, "");
        }
        public override bool CommitProcess(ClsProcessInfo objProcessInfo, string CalledFrom)
        {
            string StatusDescriptor = "";
            return CommitProcess(objProcessInfo, CalledFrom, out StatusDescriptor);
        }
        public bool CommitProcess(ClsProcessInfo objProcessInfo, string CalledFrom,
            out string StatusDescriptor)
        {
            bool retval = false;
            string CmsWebUrl = "";
            StatusDescriptor = " ";
            try
            {
                objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_RENEWAL_PROCESS;
                //ClsNonRenewProcess  objNonRenewProcess=new ClsNonRenewProcess(); 
                base.BeginTransaction();
                //int PremiumAmout = 0, MCCAFee = 0, OtherFee = 0;
                //int ReturnPremium = 0;
                //string Desc = "";
                int returnResult = 0;
                //int LOBID=	Cms.BusinessLayer.BlCommon.ClsCommon.GetPolicyLOBID(objProcessInfo.NEW_CUSTOMER_ID,objProcessInfo.NEW_POLICY_ID,objProcessInfo.NEW_POLICY_VERSION_ID);           

                //Checking the eligibility, for the newer version of policy
                if (base.CheckProcessEligibility(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID) != 1)
                {
                    base.RollbackTransaction();
                    StatusDescriptor = ClsMessages.FetchGeneralMessage("1279"); //"Policy is not eligible for this process";
                    return false;
                }
                /* moved to process start
                //CHECK Agency Termination Verification		BY PRAVESH 
                if(base.AgenyTerminationVerification(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID) !=1)
                {
                    //Go to the Non Renewal Process - WK Non Renewal Agy Terminated  
                    bool retVal=objNonRenewProcess.PrepareProcessObjectAutoStart(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,"AGENCY");
					
                    //base.RollbackTransaction();	
                    //RenewalInSuspense(objProcessInfo);
                    string strSubjectLine ="Agency Terminated";
                    AddDiaryEntry(objProcessInfo,strSubjectLine,"Renewal Expiry date for the agency has been terminated.");
                    base.CommitTransaction();
                    return false;
                } */
                //check for do not renewal
                if (CheckDoNotRenew(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID) == 1)
                {
                    string strSubjectLine = "";
                    strSubjectLine = ClsMessages.FetchGeneralMessage("1280");//"Renewal Process could not be committed as This Policy has been checked for \"Do Not Renew\".";
                    AddDiaryEntry(objProcessInfo, strSubjectLine, ClsMessages.FetchGeneralMessage("1281"), false);//"This Policy has been checked for \"Do Not Renew\"."
                    Cms.BusinessLayer.BlProcess.ClsPolicyErrMsg.strMessage = strSubjectLine;
                    base.CommitTransaction();
                    StatusDescriptor = ClsMessages.FetchGeneralMessage("1281");//"This Policy has been checked for \"Do Not Renew\".";
                    return false;
                }
                //check for status If the Status is Endorsement in Progress  / or Cancel Non Payment DB or Cancellation/NSF Replace  then additional Requirement
                //int status= Cms.BusinessLayer.BlApplication.ClsGeneralInformation.GetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID) ; 

                //Check the Validity of New Insurance Score Otherwise Call the Insurance Score Method
                #region GetInsurance Score
                if (objProcessInfo.LOB_ID < 9)//not implimented for brazil Lobs
                    if (CheckInsuranceScoreValidity(objProcessInfo.CUSTOMER_ID) == true && IsScoreToFetched)
                    {

                        returnResult = GetInsertInsuranceScore(objProcessInfo.CUSTOMER_ID, objProcessInfo.COMPLETED_BY);
                        if (returnResult == -3) //address not validated refer this policy
                        {
                            StatusDescriptor = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1907", "")+ " \""+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1908","")+"\".";//"Customer Address Could not be validated.This Policy has been marked for \"Refer to Underwriter\".";
                            UpdatePolicyReferToUnderWriter(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, "ADDRESS_NOT_VALID");
                            Cms.BusinessLayer.BlProcess.ClsPolicyErrMsg.strMessage = StatusDescriptor;
                            base.CommitTransaction();
                            return false;
                        }
                        else if (returnResult != 0)
                        {
                            TransactionDescription.Append("\n"+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1909","")+";");//Insurance Score has been updated.
                        }
                    }
                #endregion

                // request an updated MVR for Auto and Motercycle
                /* this code shift to on Start process by pravesh on 3rd april 2007
                if (LOBID == POLICY_LOB_AUTO || LOBID == POLICY_LOB_CYCL)
                {
					
                    DataSet dsUpdatedMVR = RequestMVR(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID);
                    if (dsUpdatedMVR.Tables[0].Rows.Count >0)  
                    {
                        InsertDriverMVR(dsUpdatedMVR,objProcessInfo);
                        TransactionDescription.Append ("\n Driver MVR has been fetched.;");
                        if (LOBID == POLICY_LOB_AUTO )
                        {
                            Cms.BusinessLayer.BlApplication.ClsDriverDetail  objAutoDriver = new ClsDriverDetail();  
                            objAutoDriver.UpdateVehicleClassPolNew(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID,  objProcessInfo.NEW_POLICY_VERSION_ID);
                            TransactionDescription.Append ("\n Vehicle Class has been updated.;");
                        }
                        else
                        {
                            this.UpdateVehicleClassForMotor(dsUpdatedMVR,objProcessInfo);	
                            TransactionDescription.Append ("\n Vehicle Class has been updated.;");
                        }

						
                    }
                }*/
                //verify the rule
                string strRulesStatus = "0";
                string strRulesStatusReason = "";
                bool valid = false;
                //not called for commmit any way then verify the rules
                if (CalledFrom != "COMMITANYWAY")
                {
                    //string strRulesHTML = Cms.BusinessLayer.BlProcess.clsprocess.strHTML(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,out valid,out strRulesStatus);
                    string strSystemID, strColorScheme;
                    if (IsEODProcess)
                    {
                        strColorScheme = "3";
                        strSystemID = EODSystemID;
                        CmsWebUrl = BlCommon.ClsCommon.CmsWebUrl;
                    }
                    else
                    {
                        CmsWebUrl = System.Configuration.ConfigurationSettings.AppSettings["CmsWebUrl"].ToString();
                        Cms.CmsWeb.cmsbase objBase = new Cms.CmsWeb.cmsbase();
                        strColorScheme = objBase.GetColorScheme();
                        strSystemID = objBase.GetSystemId();
                    }

                    /*Quote_RuleWebServices.wscmsweb objRule = new Cms.BusinessLayer.BlProcess.Quote_RuleWebServices.wscmsweb(CmsWebUrl );

                    string strRulesHTML = objRule.VerifyRules(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,
                        objProcessInfo.NEW_POLICY_VERSION_ID,objProcessInfo.LOB_ID.ToString(),objProcessInfo.CREATED_BY,
                        strSystemID,strColorScheme,out valid,out strRulesStatus);*/


                    ClsRatingAndUnderwritingRules objRule = new ClsRatingAndUnderwritingRules(SystemID);
                    string strRulesHTML = objRule.VerifyPolicy(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,
                        objProcessInfo.LOB_ID.ToString(), out valid, strColorScheme, out strRulesStatus, "REN");


                    if (valid && strRulesStatus == "0") // then commit
                    {
                        valid = true;
                    }
                    else
                    {
                        // chk here for referred/rejected cases
                        strRulesStatusReason = base.ChkReferedRejCaese(strRulesHTML);
                        valid = false;
                    }

                    if (!valid)
                    {
                        //Refer to Underwriters - Instructions 
                        //Then check off on the Refer to Underwriters on the Renewal Instruction Tab

                        #region Commented by Lalit ,pollicy is reffered  to under writer
                        //if pollicy is under reffered rule .  then check policy reffer to underwrite check box on policy information page
                        //this inmpliemntation is not rquired in brazile implimentation .i-track # 1109
                        /*
                        UpdatePolicyReferToUnderWriter(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, "VIOLATION");
                        if (objProcessInfo.LOB_ID != POLICY_LOB_UMB)  //for other than Umbrella
                        {
                            string strSubjectLine = ClsMessages.FetchGeneralMessage("1282");//"Process could not be committed rule could not verified and this Policy has been marked as \"refer to underwriter\".";
                            AddDiaryEntry(objProcessInfo, strSubjectLine, strRulesStatusReason, true);
                            //TransactionDescription.Append("\n A change was to refer to underwriter in policy detail's Renewal Instructions.;"); 
                            TransactionDescription.Append("\n" + ClsMessages.FetchGeneralMessage("1283")); //1283 " Process could not be committed because this Policy was marked as \"refer to underwriter\".;");
                            //retval= false;
                        }
                        //goto renewal in suspense proces

                        RenewalInSuspense(objProcessInfo);
                        //AddDiaryEntry(objProcessInfo,strSubjectLine,strRulesStatusReason,true);
                        TransactionDescription.Append("\n" + ClsMessages.FetchGeneralMessage("1284"));// Renewal in suspense has been recorded.;");
                        base.WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,
                           ClsMessages.FetchGeneralMessage("1285"), objProcessInfo.COMPLETED_BY, TransactionDescription.ToString());  //strRulesHTML);// "Renewal process could not be committed"

                        
                        //Cms.BusinessLayer.BlProcess.ClsPolicyErrMsg.strMessage = ClsMessages.FetchGeneralMessage("1286");//"This policy set to renewal in suspense as rule violated.";
                        */
                        #endregion
                        base.CommitTransaction();
                        StatusDescriptor = ClsMessages.FetchGeneralMessage("1287");//"Underwriting rules not verified for policy - Renewal in suspense ";
                        return false;

                    }
                }
                else
                {
                    //TransactionDescription.Append(Cms.CmsWeb.cmsbase.COMMIT_ANYWAYS_RULES);
                    //string strSubjectLine ="The Renewal process was committed Anyway.";
                    //AddDiaryEntry(objProcessInfo,strSubjectLine,strRulesStatusReason,true);
                    //AddDiaryEntry(objProcessInfo,strSubjectLine,"Renewal Process committed anyway.",true);
                    //TransactionDescription.Append("\n Renewal Process committed anyway.;"); 

                }
                if (UpdatePolicyReferToUnderWriter(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID) == 2)
                {
                    string strSubjectLine = ClsMessages.FetchGeneralMessage("1288");//"A change was to refer to underwriter in policy detail's Renewal Instructions ";
                    string strNotes = ClsMessages.FetchGeneralMessage("1289");//"The check box for refer to underwriter is checked off in  policy detail's Renewal Instructions";
                    AddDiaryEntry(objProcessInfo, strSubjectLine, strNotes, false);
                    TransactionDescription.Append("\n" + ClsMessages.FetchGeneralMessage("1289"));// The check box for refer to underwriter is checked off in  policy detail's Renewal Instructions.;"); ////A change was to refer to underwriter in policy detail's Renewal Instructions.;"); 
                    //The check box for refer to underwriter is checked off in  policy detail's Renewal Instructions
                    //retval= false;
                    //goto renewal in suspense
                    RenewalInSuspense(objProcessInfo, false);
                    TransactionDescription.Append("\n " + ClsMessages.FetchGeneralMessage("1284"));//Renewal in suspense has been recorded.;");
                    //TransactionDescription.Append(Cms.CmsWeb.cmsbase.COMMIT_ANYWAYS_RULES);
                    base.WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,
                       ClsMessages.FetchGeneralMessage("1285"), objProcessInfo.COMPLETED_BY, TransactionDescription.ToString()); //  " A change was to refer to underwriter in policy detail's Renewal Instructions.");
                    //"Renewal process could not be committed"
                    base.CommitTransaction();
                    Cms.BusinessLayer.BlProcess.ClsPolicyErrMsg.strMessage = ClsMessages.FetchGeneralMessage("1283");//"Renewal process could not be committed because this policy was set to \"Refer to Underwriter\"";  // and hence set to renewal in suspense";
                    StatusDescriptor = ClsMessages.FetchGeneralMessage("1283");//"Renewal process could not be committed because this policy was set to \"Refer to Underwriter\"";
                    return false;
                }

                //get display version and  Effective Date of new policy
                ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
                DataSet dsPolicy = objGenInfo.GetPolicyDetails(objProcessInfo.CUSTOMER_ID, 0, 0, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID);
                string strDisplayVersion = "", strOldDisplayVersion = "";
                DateTime PolicyExpiryDate = DateTime.MinValue, NewPolicyEffective = DateTime.MinValue;
                if (dsPolicy.Tables[0].Rows.Count > 0)
                {
                    if (dsPolicy.Tables[0].Rows[0]["POLICY_DISP_VERSION"] != null)
                        strDisplayVersion = dsPolicy.Tables[0].Rows[0]["POLICY_DISP_VERSION"].ToString();
                    if (dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"] != null)
                        NewPolicyEffective = Convert.ToDateTime(dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString());

                }
                dsPolicy = objGenInfo.GetPolicyDetails(objProcessInfo.CUSTOMER_ID, 0, 0, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID);
                if (dsPolicy.Tables[0].Rows[0]["APP_EXPIRATION_DATE"] != null)
                {
                    DateTime strPolicyExpiryDate = Convert.ToDateTime(dsPolicy.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString());
                    PolicyExpiryDate = strPolicyExpiryDate;
                    if (dsPolicy.Tables[0].Rows[0]["POLICY_DISP_VERSION"] != null)
                        strOldDisplayVersion = dsPolicy.Tables[0].Rows[0]["POLICY_DISP_VERSION"].ToString();
                }
                dsPolicy.Clear();
                dsPolicy.Dispose();
                if (NewPolicyEffective != DateTime.MinValue)
                    objProcessInfo.EFFECTIVE_DATETIME = NewPolicyEffective;
                //Updating the is_active of new policy as active
                //SetPolicyIsActive(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, "Y");
                //TransactionDescription.Append("New version (" + objProcessInfo.NEW_POLICY_VERSION_ID.ToString("#.0") 
                //TransactionDescription.Append("New version (" + strDisplayVersion 
                //	+ ") of policy marked as active.;");
                string strPolicyStatusDesc = "", strNewStatus = "";
                //Update effecive date before commit
                UpdateProcessInfoBeforeCommit(objProcessInfo);
                //Posting the premium
                string TransDesc;
                //bool retval;
                //generate New policy No
                String Policy_Number, SUSEP_LOB_CODE;
                UpdateNewPolicyNo(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID, out Policy_Number, out SUSEP_LOB_CODE);

                SetPolicyIsActive(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, "Y");//, Policy_Number, SUSEP_LOB_CODE, objProcessInfo.PROCESS_ID);


                LoggedInUserId = objProcessInfo.COMPLETED_BY;
                int intResult = PostPolicyPremium(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID,
                    objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.COMPLETED_BY, objProcessInfo.PROCESS_ID,
                    out TransDesc, "O", objProcessInfo.PROCESS_ID.ToString());
                //Split Premium XML used for Document Merge
                //SplitPremiums(objProcessInfo.CUSTOMER_ID,0,0,objProcessInfo.POLICY_ID,
                //	objProcessInfo.NEW_POLICY_VERSION_ID,objProcessInfo.PROCESS_ID.ToString());

                if (intResult == -3 || intResult == -2)
                {
                    Cms.BusinessLayer.BlCommon.ClsDiary objClsDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
                    Cms.Model.Diary.TodolistInfo objModel = new Cms.Model.Diary.TodolistInfo();

                    if (intResult == -3)
                    {
                        ClsPolicyErrMsg.strMessage = ClsMessages.FetchGeneralMessage("1119"); //1119//"Selected Billing plan has been deactivated.Please select new Billing plan.";
                        objModel.SUBJECTLINE = ClsMessages.FetchGeneralMessage("1120");//"Please select new Billing plan.";
                        objModel.NOTE = ClsMessages.FetchGeneralMessage("1119"); //"Selected Billing plan has been deactivated.";
                        StatusDescriptor = ClsMessages.FetchGeneralMessage("1119");//"Selected Billing plan has been deactivated.";
                    }
                    else
                    {
                        ClsPolicyErrMsg.strMessage = ClsMessages.FetchGeneralMessage("1121"); //"Commission not found,So process not comitted.";
                        objModel.SUBJECTLINE = ClsMessages.FetchGeneralMessage("1290");//"Renewal Process Commit Failed.";
                        objModel.NOTE = ClsMessages.FetchGeneralMessage("1123"); //"No Commission found.";
                        StatusDescriptor = ClsMessages.FetchGeneralMessage("1123");// "No Commission found.";
                    }
                    base.RollbackTransaction();
                    //objModel.TOUSERID =objProcessInfo.UNDERWRITER; 
                    objModel.LISTTYPEID = (int)BlCommon.ClsDiary.enumDiaryType.PROCESS_PENDING_REQUEST;      //15;
                    objModel.PROCESS_ROW_ID = objProcessInfo.ROW_ID;
                    objModel.POLICY_ID = objProcessInfo.POLICY_ID;
                    objModel.POLICY_VERSION_ID = objProcessInfo.NEW_POLICY_VERSION_ID; //objProcessInfo.POLICY_VERSION_ID;
                    objModel.CUSTOMER_ID = objProcessInfo.CUSTOMER_ID;
                    if (IsEODProcess)
                        objModel.CREATED_BY = EODUserID;
                    else
                        objModel.CREATED_BY = objProcessInfo.CREATED_BY;
                    objModel.CREATED_DATETIME = DateTime.Now;
                    objModel.RECDATE = DateTime.Now;
                    //objModel.FOLLOWUPDATE = DateTime.Now;
                    objModel.LAST_UPDATED_DATETIME = DateTime.Now;
                    //objModel.STARTTIME = (System.DateTime)DateTime.Now;
                    //objModel.ENDTIME = (System.DateTime)DateTime.Now;					
                    objModel.MODULE_ID = (int)BlCommon.ClsDiary.enumModuleMaster.POLICY_PROCESS;
                    objModel.LOB_ID = objProcessInfo.LOB_ID;

                    //objClsDiary.Add(objModel,"");
                    objClsDiary.DiaryEntryfromSetup(objModel);
                    retval = false;
                    //base.RollbackTransaction();
                }
                else
                {
                    TransactionDescription.Append("\n" + TransDesc + ";");
                    //Commented By Ravindra (01-09-2008)
                    //Update the Open Items and Changed the Status to OverPayment(OP) to NormalPayment(NP).
                    //UpdateOverPaymentOpenItems(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID);
                    TransactionDescription.Append("\n " + ClsMessages.FetchGeneralMessage("1291"));//Update the Open Items and Changed the Status from Over Payment (OP) to Normal Payment (NP).;");

                    //Updating the status of new version of policy
                    if (PolicyExpiryDate.Date <= System.DateTime.Now.Date)
                    {
                        SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, POLICY_STATUS_NORMAL);
                        TransactionDescription.Append(ClsMessages.FetchGeneralMessage("1292") + " (" + strDisplayVersion
                            + ") " + ClsMessages.FetchGeneralMessage("1293")); //as Active Policy.;");
                        objProcessInfo.POLICY_CURRENT_STATUS = POLICY_STATUS_NORMAL;
                        //Updating the status to Inactive of the previous policy
                        SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, POLICY_STATUS_INACTIVE);
                        TransactionDescription.Append(ClsMessages.FetchGeneralMessage("1294") + " (" + strOldDisplayVersion + ") " + ClsMessages.FetchGeneralMessage("1295"));//of policy marked as In-Active.;");
                    }
                    else
                    {
                        SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID, out strPolicyStatusDesc, out strNewStatus);
                        TransactionDescription.Append(ClsMessages.FetchGeneralMessage("1292") + " (" + strDisplayVersion
                            + ") " + ClsMessages.FetchGeneralMessage("1296") + " " + strPolicyStatusDesc + ".;"); //as Active Policy.;");
                        objProcessInfo.POLICY_CURRENT_STATUS = strNewStatus;
                    }

                    retval = base.CommitProcess(objProcessInfo, "NEWVERSION");
                    //Checking the return status
                    if (retval == false)
                    {
                        //Rollbacking the transaction
                        base.RollbackTransaction();
                        StatusDescriptor = ClsMessages.FetchGeneralMessage("1297");//"Policy not eligible for selecetd process ";
                        return retval;
                    }

                    if (CalledFrom == "COMMITANYWAY")
                    {
                        TransactionDescription.Append(Cms.CmsWeb.cmsbase.COMMIT_ANYWAYS_RULES);
                        //string strSubjectLine ="The Renewal process was committed Anyway.";
                        //AddDiaryEntry(objProcessInfo,strSubjectLine,"Renewal Process committed anyway.",true,objWrapper,CalledFrom);
                        TransactionDescription.Append("\n " + ClsMessages.FetchGeneralMessage("1298"));//Renewal Process committed anyway.;");

                    }
                    base.WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID,
                        GetTransactionLogDesc(objProcessInfo.PROCESS_ID), objProcessInfo.COMPLETED_BY, TransactionDescription.ToString());

                    //base.CommitTransaction();
                    //
                    //base.BeginTransaction(); 
                    string fileNameInsured = "";
                    string fileNameAgency = "";
                    string fileNameAccord = "";
                    string fileNameAddInt = "", strAutoIdCardPage = "";
                    int oldPolicyVersion = objProcessInfo.POLICY_VERSION_ID;
                    objProcessInfo.POLICY_VERSION_ID = objProcessInfo.NEW_POLICY_VERSION_ID;
                    if (objProcessInfo.SEND_ALL == (int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.YES))
                        getAdditionalInterest(objProcessInfo);
                    int tmpInsured = objProcessInfo.INSURED;
                    int tmpAgency = objProcessInfo.AGENCY_PRINT;
                    //gegerating additional Interest PDF
                    bool IsAddIntExists = IsAddIntExist(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.LOB_ID.ToString(), "POLICY", objWrapper);
                    if (objProcessInfo.LOB_ID == POLICY_LOB_AUTO)
                    {
                        string SucessfullPdf = GeneratePDF(objProcessInfo);
                        fileNameInsured = WordingPDFFileName;
                        fileNameAgency = AgentWordingPDFFileName;
                        strAutoIdCardPage = AutoIdCardPDFFileName;
                        fileNameAddInt = AdditionalIntrstPDFFileName;
                        fileNameAccord = AcordPDFFileName;
                    }
                    else
                    {
                        #region Commented Code
                        //if (IsAddIntExists)
                        //    fileNameAddInt = GeneratePDF(objProcessInfo, "ADDLINT");
                        ////generate dec page for Agency	
                        //objProcessInfo.INSURED = 0;
                        //objProcessInfo.AGENCY_PRINT = 1;
                        //fileNameAgency = GeneratePDF(objProcessInfo, "DECPAGE");
                        ////generate dec page for customer
                        //objProcessInfo.INSURED = 1;
                        //objProcessInfo.AGENCY_PRINT = 0;
                        //fileNameInsured = GeneratePDF(objProcessInfo, "DECPAGE");
                        ////generate Accord page 
                        //fileNameAccord = GeneratePDF(objProcessInfo, "ACORD");
                        ////generating auto ID card
                        //strAutoIdCardPage = GeneratePDF(objProcessInfo, BlCommon.ClsCommon.DOCUMENT_TYPE_AUTO_ID_CARD);
                        #endregion

                        base.GeneratePolicyDocuments(objProcessInfo);
                    }
                    #region Commented Code of print job entry
                    //objProcessInfo.POLICY_VERSION_ID = oldPolicyVersion;
                    //objProcessInfo.INSURED = tmpInsured;
                    //objProcessInfo.AGENCY_PRINT = tmpAgency;
                    //ClsPrintJobsInfo objPrintJobsInfo = GetPrintJobsValues(objProcessInfo, "NEWVERSION");

                    //TransactionDescription.Append("\n Printing Jobs has been saved.");

                    //Start Commented code of genrate document and print job entry
                    //commented By Lalit Dec 14,2010
                   
                    //if (objProcessInfo.PRINTING_OPTIONS == (int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.NO))
                    //{
                    //    // do entry in print job other than No print required --chnanged on 3 april 2008
                    //    //if(objProcessInfo.INSURED  == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString())  )
                    //    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
                    //    if (objProcessInfo.INSURED != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString()))
                    //    {
                    //        objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER;
                    //        objPrintJobsInfo.FILE_NAME = fileNameInsured;
                    //        AddPrintJobs(objPrintJobsInfo);
                    //    }
                    //    else // Do entry for non printing files
                    //    {
                    //        objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER_NOWORD;
                    //        objPrintJobsInfo.FILE_NAME = NoWordingPDFFileName;
                    //        AddPdfFileLog(objPrintJobsInfo);
                    //    }
                    //    // do entry in print job other than No print required --chnanged on 3 april 2008
                    //    //if(objProcessInfo.AGENCY_PRINT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString()))

                    //    objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_AGENCY;
                    //    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
                    //    objPrintJobsInfo.FILE_NAME = fileNameAgency;
                    //    if (objProcessInfo.AGENCY_PRINT != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString()))
                    //        AddPrintJobs(objPrintJobsInfo);
                    //    else // Do entry for non printing files
                    //        AddPdfFileLog(objPrintJobsInfo);
                    //    // do entry in print job other than No print required --chnanged on 3 april 2008
                    //    if (IsAddIntExists == true)
                    //    {
                    //        if (objProcessInfo.ADD_INT_ID != "")
                    //        {
                    //            string strAddlInt = "";
                    //            objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_ADDL_INTEREST;
                    //            objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
                    //            string[] addIntIdArr = objProcessInfo.ADD_INT_ID.Split('~');
                    //            if (addIntIdArr != null && addIntIdArr.Length > 0)
                    //            {
                    //                for (int jCounter = 0; jCounter < addIntIdArr.Length; jCounter++)
                    //                {
                    //                    string[] strArr = addIntIdArr[jCounter].Split('^');
                    //                    if (strArr == null || strArr.Length < 1)
                    //                        continue;

                    //                    strAddlInt = strArr[0].ToString() + "_" + objPrintJobsInfo.CUSTOMER_ID.ToString() + "_" + objPrintJobsInfo.POLICY_ID.ToString() + "_" + objPrintJobsInfo.POLICY_VERSION_ID.ToString() + "_" + strArr[1].ToString() + "_" + strArr[2].ToString();
                    //                    objPrintJobsInfo.FILE_NAME = FindAddIntFileName(fileNameAddInt, strAddlInt);
                    //                    if (objProcessInfo.ADD_INT != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString()))
                    //                        AddPrintJobs(objPrintJobsInfo);
                    //                    else // Do entry for non printing files
                    //                        AddPdfFileLog(objPrintJobsInfo);
                    //                }
                    //            }
                    //        }
                    //    }
                    //    //auto ID Card
                    //    if (objProcessInfo.NO_COPIES == 0)
                    //        objProcessInfo.NO_COPIES = 2; //default value
                    //    if (objProcessInfo.STATE_CODE == "IN")
                    //        objProcessInfo.NO_COPIES = 1;
                    //    if ((objProcessInfo.LOB_ID == POLICY_LOB_AUTO || objProcessInfo.LOB_ID == POLICY_LOB_CYCL) && strAutoIdCardPage != "")
                    //    {
                    //        objPrintJobsInfo.DOCUMENT_CODE = objPrintJobsInfo.ENTITY_TYPE = "AUTO_ID_CARD";
                    //        objPrintJobsInfo.FILE_NAME = strAutoIdCardPage;
                    //        if (objProcessInfo.AUTO_ID_CARD != int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.NOT_REQUIRED).ToString()))
                    //        {
                    //            for (int i = 0; i < objProcessInfo.NO_COPIES; i++)
                    //                AddPrintJobs(objPrintJobsInfo);
                    //        }
                    //        else // Do entry for non printing files
                    //            AddPdfFileLog(objPrintJobsInfo);
                    //    }
                    //    // Do entry for non printing files
                    //    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
                    //    objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER;
                    //    objPrintJobsInfo.FILE_NAME = fileNameInsured;
                    //    AddPdfFileLog(objPrintJobsInfo);
                    //    // Do entry for non printing files
                    //    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_ACORD;
                    //    objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_DOCUMENT_CODE_ACORD;
                    //    objPrintJobsInfo.FILE_NAME = fileNameAccord;
                    //    AddPdfFileLog(objPrintJobsInfo);
                    //}
                    //else
                    //{
                    //    // Do entry for non printing files
                    //    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
                    //    objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER_NOWORD;
                    //    objPrintJobsInfo.FILE_NAME = NoWordingPDFFileName;
                    //    AddPdfFileLog(objPrintJobsInfo);

                    //    // Do entry for non printing files
                    //    objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_AGENCY;
                    //    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
                    //    objPrintJobsInfo.FILE_NAME = fileNameAgency;
                    //    AddPdfFileLog(objPrintJobsInfo);

                    //    if (IsAddIntExists == true)
                    //    {
                    //        if (objProcessInfo.ADD_INT_ID != "")
                    //        {
                    //            string strAddlInt = "";
                    //            objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_ADDL_INTEREST;
                    //            objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
                    //            string[] addIntIdArr = objProcessInfo.ADD_INT_ID.Split('~');
                    //            if (addIntIdArr != null && addIntIdArr.Length > 0)
                    //            {
                    //                for (int jCounter = 0; jCounter < addIntIdArr.Length; jCounter++)
                    //                {
                    //                    string[] strArr = addIntIdArr[jCounter].Split('^');
                    //                    if (strArr == null || strArr.Length < 1)
                    //                        continue;
                    //                    strAddlInt = strArr[0].ToString() + "_" + objPrintJobsInfo.CUSTOMER_ID.ToString() + "_" + objPrintJobsInfo.POLICY_ID.ToString() + "_" + objPrintJobsInfo.POLICY_VERSION_ID.ToString() + "_" + strArr[1].ToString() + "_" + strArr[2].ToString();
                    //                    objPrintJobsInfo.FILE_NAME = FindAddIntFileName(fileNameAddInt, strAddlInt);
                    //                    // Do entry for non printing files
                    //                    AddPdfFileLog(objPrintJobsInfo);
                    //                }
                    //            }
                    //        }
                    //    }
                    //    //auto ID Card
                    //    if ((objProcessInfo.LOB_ID == POLICY_LOB_AUTO || objProcessInfo.LOB_ID == POLICY_LOB_CYCL) && strAutoIdCardPage != "")
                    //    {
                    //        objPrintJobsInfo.DOCUMENT_CODE = objPrintJobsInfo.ENTITY_TYPE = "AUTO_ID_CARD";
                    //        objPrintJobsInfo.FILE_NAME = strAutoIdCardPage;
                    //        // Do entry for non printing files
                    //        AddPdfFileLog(objPrintJobsInfo);
                    //    }
                    //    // Do entry for non printing files
                    //    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_DEC_PAGE;
                    //    objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_ENTITY_TYPE_CUSTOMER;
                    //    objPrintJobsInfo.FILE_NAME = fileNameInsured;
                    //    AddPdfFileLog(objPrintJobsInfo);
                    //    // Do entry for non printing files
                    //    objPrintJobsInfo.DOCUMENT_CODE = ClsPolicyProcess.PRINT_JOBS_DOCUMENT_CODE_ACORD;
                    //    objPrintJobsInfo.ENTITY_TYPE = PRINT_JOBS_DOCUMENT_CODE_ACORD;
                    //    objPrintJobsInfo.FILE_NAME = fileNameAccord;
                    //    AddPdfFileLog(objPrintJobsInfo);
                    //}
                    #endregion
                    //End

                    base.CommitTransaction();
                }
                return retval;
            }
            catch (System.Net.WebException objEx)
            {
                Exception customEx = new Exception("Error while calling webservice for rule. URL : " + CmsWebUrl, objEx);
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(customEx);
                base.RollbackTransaction();
                throw (customEx);
            }
            catch (System.DllNotFoundException exx)
            {
                //if(retval)
                //	base.CommitTransaction();
                //else
                base.RollbackTransaction();
                //				if (CalledFrom!="COMMITANYWAY")
                //					Cms.BusinessLayer.BlProcess.ClsPolicyErrMsg.strMessage="Process committed but Unable to generate PDF";
                //				else
                //					Cms.BusinessLayer.BlProcess.ClsPolicyErrMsg.strMessage="Process committed Anyway but Unable to generate PDF";
                //				return retval;
                throw new Exception("Process could not be committed. Unable to generate PDF", exx);
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw (ex);
            }
        }

        /// <summary>
        /// Override the base rollback process.
        /// </summary>
        /// <param name="objProcessInfo"></param>
        /// <returns></returns>
        public override bool RollbackProcess(ClsProcessInfo objProcessInfo)
        {
            try
            {

                base.BeginTransaction();
                //Checking the eligibility for current version on the policy 
                if (base.CheckProcessEligibility(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.PROCESS_ID) != 1)
                {
                    base.RollbackTransaction();
                    return false;
                }


                if (base.RollbackProcess(objProcessInfo, "NEWVERSION") == true)
                {
                    //Marking the over payment to return over payment 
                    if (GetOpenItemStatusExist(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, "OP") == true)
                    {
                        SetOpenItemStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, "OP", "ROP");
                        TransactionDescription.Append("\n "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1910","")+";");
                    }

                    //Deleting the newer version
                    base.DeletePolicyVersion(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID);
                    TransactionDescription.Append("New version (" + objProcessInfo.NEW_POLICY_VERSION_ID.ToString("#.0")
                        + ") of policy has been deleted.;");


                    //Changing the status of older policy as active policy
                    //SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, POLICY_STATUS_NORMAL);
                    //TransactionDescription.Append("Status of previous version " + objProcessInfo.POLICY_VERSION_ID.ToString("#.0") + " has been marked as Active.;");


                    //Updating the transaction log
                    base.WriteTransactionLog(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID,
                        GetTransactionLogDesc(objProcessInfo.PROCESS_ID), objProcessInfo.COMPLETED_BY, TransactionDescription.ToString());

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
                throw (ex);
            }
        }


        /// <summary>
        /// It will check for Insurance score validity.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        private bool CheckInsuranceScoreValidity(int CustomerID)
        {
            int returnResult = 0;
            objWrapper.ClearParameteres();

            objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
            SqlParameter objSqlParameter = (SqlParameter)objWrapper.AddParameter("@RETURN_VALUE", SqlDbType.Int, ParameterDirection.Output);

            returnResult = objWrapper.ExecuteNonQuery("Proc_CheckValidity_Insurance_Score");

            if (objSqlParameter.Value != System.DBNull.Value)
            {
                returnResult = Convert.ToInt32(objSqlParameter.Value);
            }

            objWrapper.ClearParameteres();

            if (returnResult == 1)
                return false;
            else
                return true;


        }
        /// <summary>
        /// Update Vehicle Class for Motor while fetching MVR
        /// </summary>
        /// <param name="dsDrivers"></param>
        /// <param name="objProcessInfo"></param>
        private void UpdateVehicleClassForMotor(DataSet dsDrivers, ClsProcessInfo objProcessInfo)
        {
            ClsDriverDetail objDriverDetail = new ClsDriverDetail();
            /*DateTime DriverDOB;
            int VehicleID=0;
            for (int i=0;i<= dsDrivers.Tables[0].Rows.Count-1;i++)
            {
                if (dsDrivers.Tables[0].Rows[i]["DRIVER_DOB"]!=System.DBNull.Value) 
                  DriverDOB=Convert.ToDateTime(dsDrivers.Tables[0].Rows[i]["DRIVER_DOB"].ToString());
                else
                    DriverDOB=DateTime.MinValue; 
                if (dsDrivers.Tables[0].Rows[i]["VEHICLE_ID"]!=System.DBNull.Value) 
                    VehicleID=int.Parse(dsDrivers.Tables[0].Rows[i]["VEHICLE_ID"].ToString());
                else
                    VehicleID=0;
                objDriverDetail.SetMotorVehicleClassRuleForPolicy(objWrapper,objProcessInfo.CUSTOMER_ID ,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID,
                    int.Parse(dsDrivers.Tables[0].Rows[i]["DRIVER_ID"].ToString()),
                        VehicleID, DriverDOB); 
            }*/
            objDriverDetail.UpdateMotorVehicleClassPOL(objWrapper, objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID);
        }
        ///<summary> It will request Updated Mvr
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="POLICY_ID"></param>
        /// <param name="Policy_varsion_ID"></param>
        /// <returns></returns> by pravesh
        private DataSet RequestMVR(int intCUSTOMER_ID, int intPOLICY_ID, int intPOLICY_VERSION_ID)
        {
            objWrapper.ClearParameteres();
            objWrapper.AddParameter("@CUSTOMER_ID", intCUSTOMER_ID);
            objWrapper.AddParameter("@POLICY_ID", intPOLICY_ID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", intPOLICY_VERSION_ID);

            try
            {
                DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_CheckMVR_Requird");
                objWrapper.ClearParameteres();
                return dsTemp;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        /// <summary>
        /// Add diary Entry 
        /// </summary>
        /// <returns></returns>
        public void AddDiaryEntry(ClsProcessInfo objProcessInfo, string strSubjectLine, string strNotes, bool RuleViolate)
        {
            AddDiaryEntry(objProcessInfo, strSubjectLine, strNotes, RuleViolate, null);
        }
        public void AddDiaryEntry(ClsProcessInfo objProcessInfo, string strSubjectLine, string strNotes, bool RuleViolate, Cms.DataLayer.DataWrapper objNewWrapper)
        {
            AddDiaryEntry(objProcessInfo, strSubjectLine, strNotes, RuleViolate, objNewWrapper, "");
        }
        public void AddDiaryEntry(ClsProcessInfo objProcessInfo, string strSubjectLine, string strNotes, bool RuleViolate, Cms.DataLayer.DataWrapper objNewWrapper, string strCalledFrom)
        {
            try
            {
                Cms.BusinessLayer.BlCommon.ClsDiary objClsDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
                Cms.Model.Diary.TodolistInfo objModel = new Cms.Model.Diary.TodolistInfo();
                if (IsEODProcess)
                    objModel.FROMUSERID = EODUserID;
                else
                    objModel.FROMUSERID = objProcessInfo.CREATED_BY;
                //objModel.TOUSERID           =   objProcessInfo.UNDERWRITER;     
                objModel.SUBJECTLINE = strSubjectLine; //"A change was to refer to underwriter in policy detail's Renewal Instructions ";
                objModel.NOTE = strNotes; // "The check box for refer to underwriter is checked off in  policy detail's Renewal Instructions";					
                objModel.PROCESS_ROW_ID = objProcessInfo.ROW_ID;
                if (strCalledFrom == "COMMITANYWAY")
                    objModel.LISTTYPEID = (int)BlCommon.ClsDiary.enumDiaryType.RENEWAL_REQUESTS;
                //agency terminated Itrack No.3361 Manoj Rathore
                else if (strCalledFrom == BlCommon.ClsDiary.enumDiaryType.AGENCY_TERMINATED.ToString())
                    objModel.LISTTYPEID = (int)BlCommon.ClsDiary.enumDiaryType.AGENCY_TERMINATED; //     .15;
                else if (strCalledFrom == BlCommon.ClsDiary.enumDiaryType.PENDING_RENEWAL_FOLLOWUP.ToString())
                    objModel.LISTTYPEID = (int)BlCommon.ClsDiary.enumDiaryType.PENDING_RENEWAL_FOLLOWUP;
                else
                    objModel.LISTTYPEID = (int)BlCommon.ClsDiary.enumDiaryType.PROCESS_PENDING_REQUEST; //     .15;
                objModel.POLICY_ID = objProcessInfo.POLICY_ID;
                if (objProcessInfo.NEW_POLICY_VERSION_ID != 0)
                    objModel.POLICY_VERSION_ID = objProcessInfo.NEW_POLICY_VERSION_ID;
                else
                    objModel.POLICY_VERSION_ID = objProcessInfo.POLICY_VERSION_ID;
                objModel.CUSTOMER_ID = objProcessInfo.CUSTOMER_ID;
                if (IsEODProcess)
                    objModel.CREATED_BY = EODUserID;
                else
                    objModel.CREATED_BY = objProcessInfo.CREATED_BY;
                objModel.LISTOPEN = "Y";
                objModel.CREATED_DATETIME = DateTime.Now;
                //objModel.FOLLOWUPDATE = DateTime.Now;
                objModel.LAST_UPDATED_DATETIME = DateTime.Now;
                objModel.RECDATE = DateTime.Now;
                //objModel.STARTTIME = (System.DateTime)DateTime.Now;
                //objModel.ENDTIME = (System.DateTime)DateTime.Now;	
                objModel.MODULE_ID = (int)BlCommon.ClsDiary.enumModuleMaster.POLICY_PROCESS;
                objModel.LOB_ID = objProcessInfo.LOB_ID;
                if (RuleViolate == true)
                {
                    objModel.RULES_VERIFIED = 1;
                    if (objNewWrapper != null)
                        objClsDiary.DiaryEntryfromSetup(objModel, objNewWrapper);
                    else
                    {
                        objClsDiary.DiaryEntryfromSetup(objModel, objWrapper);
                    }
                }
                else
                {
                    objModel.RULES_VERIFIED = 0;

                    if (objNewWrapper != null)
                        objClsDiary.DiaryEntryfromSetup(objModel, objNewWrapper);
                    else
                    {
                        objWrapper.ClearParameteres();
                        objClsDiary.DiaryEntryfromSetup(objModel, objWrapper);
                    }
                }
                //objClsDiary.Add(objModel,"");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        /// <summary>
        /// set status of policy Renewal in suspense and add diary entry
        /// </summary>
        /// <param name="objProcessInfo"></param>
        private void RenewalInSuspense(ClsProcessInfo objProcessInfo)
        {
            RenewalInSuspense(objProcessInfo, true);
        }
        /// <summary>
        /// set status of policy Renewal in suspense and add diary entry
        /// </summary>
        /// <param name="objProcessInfo"></param>
        private void RenewalInSuspense(ClsProcessInfo objProcessInfo, bool ruleFlag)
        {
            SetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, POLICY_STATUS_RENEWAL_SUSPENSE);
            //add diary enrty
            Cms.BusinessLayer.BlCommon.ClsDiary objClsDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
            Cms.Model.Diary.TodolistInfo objModelInfo = new Cms.Model.Diary.TodolistInfo();

            //Commented By Ravindra(06/18/2007): No need to pick from Session ,available 
            //in model object itself
            if (IsEODProcess)
                objModelInfo.FROMUSERID = EODUserID;
            else
                objModelInfo.FROMUSERID = objProcessInfo.CREATED_BY;
            //objModelInfo.FROMUSERID         =   int.Parse(System.Web.HttpContext.Current.Session["userId"].ToString());    						

            //objModelInfo.TOUSERID           =   objProcessInfo.UNDERWRITER;  
            objModelInfo.LISTTYPEID = (int)BlCommon.ClsDiary.enumDiaryType.RENEWAL_IN_SUSPENSE; //20;// Renewal in Suspense			
            //objModelInfo.PRIORITY           =   "M";//Medium Priority
            objModelInfo.PROCESS_ROW_ID = objProcessInfo.ROW_ID;
            objModelInfo.NOTE = objModelInfo.SUBJECTLINE = "Renewal in Suspense / Refer to Underwriters";
            if (ruleFlag)
                objModelInfo.NOTE += Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1911", "");//"(Please click on the image to see the list of rules violated)";
            objModelInfo.LISTOPEN = "Y";
            objModelInfo.CUSTOMER_ID = objProcessInfo.CUSTOMER_ID;
            objModelInfo.POLICY_ID = objProcessInfo.POLICY_ID;
            objModelInfo.POLICY_VERSION_ID = objProcessInfo.NEW_POLICY_VERSION_ID;
            //objModelInfo.FOLLOWUPDATE       =   System.DateTime.Today.AddDays(1);
            //objModelInfo.STARTTIME            =	System.DateTime.Now;
            //objModelInfo.ENDTIME              =	System.DateTime.Now; 
            objModelInfo.RECDATE = DateTime.Now;
            if (IsEODProcess)
                objModelInfo.CREATED_BY = objModelInfo.MODIFIED_BY = EODUserID;
            else
                objModelInfo.CREATED_BY = objModelInfo.MODIFIED_BY = objProcessInfo.CREATED_BY;
            objModelInfo.MODULE_ID = (int)BlCommon.ClsDiary.enumModuleMaster.POLICY_PROCESS;
            objModelInfo.LOB_ID = objProcessInfo.LOB_ID;
            objModelInfo.CREATED_DATETIME = objModelInfo.LAST_UPDATED_DATETIME = System.DateTime.Now;
            if (ruleFlag)
            {
                objModelInfo.RULES_VERIFIED = 1;
                objClsDiary.DiaryEntryfromSetup(objModelInfo);

            }
            else
            {
                objModelInfo.RULES_VERIFIED = 0;
                objClsDiary.DiaryEntryfromSetup(objModelInfo, objWrapper);
            }
            //objClsDiary.Add(objModelInfo,"");
            objClsDiary.Dispose();

        }
        /// <summary>
        /// It will get the Insurance Score.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        private int GetInsertInsuranceScore(int CustomerID, int CreatedBy)
        {
            Cms.BusinessLayer.BlClient.ClsCustomer objCustomer = new Cms.BusinessLayer.BlClient.ClsCustomer();
            Cms.Model.Client.ClsCustomerInfo objCustInfo = new Cms.Model.Client.ClsCustomerInfo();
            Cms.Model.Client.ClsCustomerInfo objCustOldInfo = new Cms.Model.Client.ClsCustomerInfo();
            string strUserName, strPassword, strAccountNumber, strUrl;
            if (IsEODProcess)
            {
                strUserName = IIXUserName;
                strPassword = IIXPassword;
                strAccountNumber = IIXAccountNumber;
                strUrl = IIXUrl;
            }
            else
            {
                System.Collections.Specialized.NameValueCollection dic = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationManager.GetSection("IIXSettings");
                strUserName = dic["UserName"].ToString();
                strPassword = dic["Password"].ToString();
                strAccountNumber = dic["AccountNumber"].ToString();
                strUrl = dic["URL"].ToString();
            }
            Cms.CmsWeb.Utils.Utility objUtility = new Cms.CmsWeb.Utils.Utility(strUserName, strPassword, strAccountNumber, strUrl);

            Cms.CmsWeb.Utils.CreditScoreDetails objScore;

            int intScore = -1;

            try
            {
                DataSet dsCustomer = Cms.BusinessLayer.BlClient.ClsCustomer.GetCustomerDetails(CustomerID);
                PopulateModelObject(objCustInfo, dsCustomer.GetXml().ToString());
                PopulateModelObject(objCustOldInfo, dsCustomer.GetXml().ToString());
                objScore = objUtility.GetCustomerCreditScore(objCustInfo);
                intScore = objScore.Score;
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("ADDRESS_NOT_VALIDATED") != -1) //refer policy
                {
                    return -3;
                }
                else
                {
                    throw (ex);
                }
            }

            //			if ( intScore == -1 )
            //			{
            //				return 0;
            //			}

            objCustInfo.CustomerInsuranceScore = intScore;
            objCustInfo.FACTOR1 = objScore.FirstFactor;
            objCustInfo.FACTOR2 = objScore.SecondFactor;
            objCustInfo.FACTOR3 = objScore.ThirdFactor;
            objCustInfo.FACTOR4 = objScore.FourthFactor;
            objCustInfo.MODIFIED_BY = CreatedBy;
            //Update the insurance score details in the database
            int result = objCustomer.SetInsuranceScore(objCustInfo, objCustOldInfo);

            return result;

        }
        /// <summary>
        /// It will Fetch MVR for drivers.
        /// </summary>
        /// <param name=dsDrivers></param>
        /// <param name=""></param>
        private int InsertDriverMVR(DataSet dsUpdatedMVR, ClsProcessInfo objProcessInfo)
        {
            int ReturnResult = 0;
            if (dsUpdatedMVR.Tables[0].Rows.Count > 0) //fetch mvr
            {
                /////
                //DataSet objDSDriverViolDetail;
                //System.Xml.XmlNode objNode = null;
                //string strXmlQuery;
                string strUserName, strPassword, strAccountNumber, strUrl;
                if (IsEODProcess)
                {
                    strUserName = IIXUserName;
                    strPassword = IIXPassword;
                    strAccountNumber = IIXAccountNumber;
                    strUrl = IIXUrl;
                }
                else
                {
                    System.Collections.Specialized.NameValueCollection dic = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationManager.GetSection("IIXSettings");
                    strUserName = dic["UserName"].ToString();
                    strPassword = dic["Password"].ToString();
                    strAccountNumber = dic["AccountNumber"].ToString();
                    strUrl = dic["URL"].ToString();
                }
                Cms.CmsWeb.Utils.Utility objMVRUtil = new Cms.CmsWeb.Utils.Utility(strUserName, strPassword, strAccountNumber, strUrl);
                Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
                int nCount;
                string strLOBID = "";
                string strStateID;
                // for each driver or operator getting a list of violation from iix web service
                for (nCount = 0; nCount <= dsUpdatedMVR.Tables[0].Rows.Count - 1; nCount++)
                {
                    string strDateOfBirth = "";
                    string strDriverId = dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_ID"].ToString();
                    string strDriverName = dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString();
                    strDriverName += dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_MNAME"] == null ? "" : " " + dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_MNAME"].ToString() + " " + dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString();
                    if (dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_DOB"] != null)
                    {
                        strDateOfBirth = Convert.ToDateTime(dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_DOB"].ToString()).ToString("MMddyyyy");
                        strDriverName += "-{" + Convert.ToDateTime(dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_DOB"].ToString()).ToString("MM/dd/yyyy") + "}";
                    }
                    strLOBID = dsUpdatedMVR.Tables[0].Rows[nCount]["LOB_ID"].ToString();
                    strStateID = Convert.ToString(dsUpdatedMVR.Tables[0].Rows[nCount]["STATE_ID"].ToString());
                    // xml retrived from web service
                    System.Xml.XmlDocument objDriverResponse;
                    objDriverResponse = new System.Xml.XmlDocument();

                    objDriverResponse = objMVRUtil.GetViolation(dsUpdatedMVR.Tables[0].Rows[nCount]["STATE_CODE"] == null ? "" : dsUpdatedMVR.Tables[0].Rows[nCount]["STATE_CODE"].ToString(),
                        dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_DRIV_LIC"] == null ? "" : dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_DRIV_LIC"].ToString(),
                        dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_LNAME"] == null ? "" : dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_LNAME"].ToString(),
                        dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_FNAME"] == null ? "" : dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_FNAME"].ToString(),
                        dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_SUFFIX"] == null ? "" : dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_SUFFIX"].ToString(),
                        dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_MNAME"] == null ? "" : dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_MNAME"].ToString(),
                        strDateOfBirth,
                        dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_SEX"] == null ? "" : dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_SEX"].ToString(), "", objProcessInfo.CUSTOMER_ID.ToString(), objProcessInfo.POLICY_ID.ToString(), objProcessInfo.NEW_POLICY_VERSION_ID.ToString());
                    if (objDriverResponse.DocumentElement.SelectNodes("Error").Count > 0)
                    {
                        continue;
                    }
                    Cms.BusinessLayer.BlApplication.ClsMvrInformation objMvr = new Cms.BusinessLayer.BlApplication.ClsMvrInformation();
                    // added common method
                    ReturnResult = objMvr.SetDriverViolations(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.LOB_ID.ToString(), objProcessInfo.CREATED_BY, strStateID, strDriverId, strDriverName, objDriverResponse, "PROCESS", objWrapper);
                    #region  Insert Violation commented/ made common method in ClsMvrInformation
                    /*
					if (objDriverResponse.DocumentElement.SelectNodes("Violation").Count >0)
					{
						// if violations come from iix web service 
						int nViolationRec= objDriverResponse.DocumentElement.ChildNodes.Count; 
						string strViolationCode="";
						int nNode;
						// getting all violation code 
						for (nNode =0; nNode<= nViolationRec - 1; nNode++)
						{
							
							if (objDriverResponse.DocumentElement.SelectNodes("Violation").Item(nNode).Attributes["AViolation_code"].InnerText !="")
							{
								if (strViolationCode =="")
								{
									strViolationCode ="'"+objDriverResponse.DocumentElement.SelectNodes("Violation").Item(nNode).Attributes["AViolation_code"].InnerText +"'"; 
								}
								else
								{
									strViolationCode =strViolationCode +",'"+ objDriverResponse.DocumentElement.SelectNodes("Violation").Item(nNode).Attributes["AViolation_code"].InnerText +"'"; 
								}
							}
						}
						if(strViolationCode=="")
						{
							strViolationCode="''";
						}
						// mapp all violation code with wolverine violation code and get details 
						objDSDriverViolDetail= objGenInfo.GetViolationRecords(strViolationCode,strLOBID.ToString(),strStateID);
					
					

						// creating a mver object to store all info retrived 
						//Cms.Model.Application.ClsMvrInfo objMVRInfo= new Cms.Model.Application.ClsMvrInfo();
						Cms.Model.Policy.ClsPolicyAutoMVR objMVRInfo= new Cms.Model.Policy.ClsPolicyAutoMVR(); 
						objMVRInfo.POLICY_ID =objProcessInfo.POLICY_ID;
						objMVRInfo.CUSTOMER_ID = int.Parse(objProcessInfo.CUSTOMER_ID.ToString());
						objMVRInfo.POLICY_VERSION_ID = objProcessInfo.NEW_POLICY_VERSION_ID ;
						objMVRInfo.CREATED_BY= objProcessInfo.CREATED_BY;
						objMVRInfo.CREATED_DATETIME = DateTime.Now;  

						string[] objArrCode = strViolationCode.Split(','); 
						bool bIsExist; 
						string strDescription="";
						string strVcode =""; 
						for ( nNode=0; nNode<= objArrCode.Length  - 1; nNode++)
						{
							strDescription="";
							strVcode =""; 
							bIsExist= false; 
							if (objDSDriverViolDetail != null)
							{
								if(objDSDriverViolDetail.Tables[0]!= null)
								{
									for (int nRecord =0; nRecord <=objDSDriverViolDetail.Tables[0].Rows.Count -1 ;nRecord ++)
									{
										string strSSVCode= objDSDriverViolDetail.Tables[0].Rows[nRecord]["SSV_CODE"].ToString().Trim();
										string strVCode = objArrCode[nNode].Replace("'","").Trim()   ;
										if( strSSVCode == strVCode  )
										{
											bIsExist= true;
											break;
										}
									}
								}
							}
							// if iix violation not mapped then put this entry in exception table for future use 
								
							if (bIsExist== false)
							{
								if(objArrCode[nNode].ToString() != "''")
								{
									strXmlQuery="/ResultData/Violation[@AViolation_code="+objArrCode[nNode].ToString().Trim()+"]";
									objNode=objDriverResponse.DocumentElement.SelectSingleNode(strXmlQuery); 
									strDescription=objNode.Attributes["viol_description"].InnerText.Trim()   ; 
									strVcode =objNode.Attributes["AViolation_code"].InnerText.Trim(); 
									string strMVRDate =objNode.Attributes["viol_date"].InnerText.Trim(); 
									if (strMVRDate!="")
									{
										strMVRDate=strMVRDate.Substring(0,2).ToString()  + "/" +  strMVRDate.Substring(2,2) + "/" + strMVRDate.Substring(4,4);
										objMVRInfo.MVR_DATE= Convert.ToDateTime(strMVRDate);
									}
									
									objMVRInfo.DRIVER_ID =int.Parse(dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_ID"].ToString()) ;
									int resultID=objGenInfo.InsertUnmatchedMVRViolationDetail(objMVRInfo,strVcode,strDescription,"0","0",false);
									if (resultID==-1) continue;
									objMVRInfo.MVR_DEATH ="N"; 
									objMVRInfo.MVR_AMOUNT=0; 
									objMVRInfo.VIOLATION_ID =resultID;
									objMVRInfo.IS_ACTIVE ="Y";
									//objMVRInfo.VIOLATION_TYPE =int.Parse("13220");
									Cms.BusinessLayer.BlApplication.ClsMvrInformation objMvr = new Cms.BusinessLayer.BlApplication.ClsMvrInformation();
								
									//if ppa or motercycle
									if (strLOBID == "2" )
									{
										objMvr.Add(objMVRInfo, "PPA");
									}
									else if ( strLOBID == "3")
									{
										objMvr.Add(objMVRInfo, "MOT");
									}
								}
							}

						}
						// now insert all mapped violations and its details 
						if (objDSDriverViolDetail != null)
						{
							if (objDSDriverViolDetail.Tables[0] != null)
							{
								for (int nRecord =0; nRecord <=objDSDriverViolDetail.Tables[0].Rows.Count -1 ;nRecord ++)
								{
									strXmlQuery="/ResultData/Violation[@AViolation_code='"+objDSDriverViolDetail.Tables[0].Rows[nRecord]["SSV_CODE"].ToString().Trim()  +"']";
									objNode=objDriverResponse.DocumentElement.SelectSingleNode(strXmlQuery); 
						
									objMVRInfo.DRIVER_ID=  int.Parse (dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_ID"]==null?"": dsUpdatedMVR.Tables[0].Rows[nCount]["DRIVER_ID"].ToString() ); 
									if (objNode.Attributes["viol_date"].InnerText !="")
									{
										string date = objNode.Attributes["viol_date"].InnerText ; 
										objMVRInfo.MVR_DATE= new DateTime(int.Parse( date.Substring(4,4)),int.Parse(date.Substring(0,2)),int.Parse(date.Substring(2,2))) ;
									}
									objMVRInfo.MVR_DEATH ="N"; 
									objMVRInfo.MVR_AMOUNT=0; 
									objMVRInfo.VIOLATION_ID =int.Parse ( objDSDriverViolDetail.Tables[0].Rows[nRecord]["VIOLATION_ID"]== null?"":objDSDriverViolDetail.Tables[0].Rows[nRecord]["VIOLATION_ID"].ToString()) ;
									objMVRInfo.IS_ACTIVE =objDSDriverViolDetail.Tables[0].Rows[nRecord]["IS_ACTIVE"]== null?"":objDSDriverViolDetail.Tables[0].Rows[nRecord]["IS_ACTIVE"].ToString() ;
									Cms.BusinessLayer.BlApplication.ClsMvrInformation objMvr = new Cms.BusinessLayer.BlApplication.ClsMvrInformation();
									// if watercraft 
									int RetVal=0;
									if (strLOBID == "4")
									{
										RetVal=objMvr.AddForWater(objMVRInfo);
										
									}
										//if ppa or motercycle
									else if (strLOBID == "2" )
									{
										RetVal=objMvr.Add(objMVRInfo, "PPA");
										
									}
									else if ( strLOBID == "3")
									{
										RetVal=objMvr.Add(objMVRInfo, "MOT");
										
									}
								ReturnResult=ReturnResult+RetVal;
								}
							}
						}
					} //END IF
					*/
                    #endregion
                } //END FOR
            }
            return ReturnResult;
        }

        /// <summary>
        /// It will update the Over Payment Open Items Status.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        private void UpdateOverPaymentOpenItems(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            DataSet dsTempOP;
            DataSet dsTempDuePrem;
            dsTempOP = GetOverPaymentOpenItems(CustomerID, PolicyID, PolicyVersionID);
            //dsTempOP Table 0 has total over payment rows
            //dsTempOP Table 1 has total over payment amount
            if (dsTempOP.Tables[0].Rows.Count > 0 && dsTempOP.Tables[1].Rows.Count > 0)
            {
                decimal TotalOverPaymentAmount = Convert.ToDecimal(dsTempOP.Tables[1].Rows[0][0].ToString());

                dsTempDuePrem = GetDuePremiumOpenItems(CustomerID, PolicyID, PolicyVersionID);

                if (dsTempDuePrem.Tables[0].Rows.Count > 0)
                {
                    for (int count = 0; count < dsTempDuePrem.Tables[0].Rows.Count; count++)
                    {
                        if (TotalOverPaymentAmount <= 0)
                        {
                            decimal TempTotalDue = 0;
                            int RowID = 0;
                            decimal AmountUpdated = 0;

                            TempTotalDue = Convert.ToDecimal(dsTempDuePrem.Tables[0].Rows[count]["Total_Due"].ToString());
                            RowID = Convert.ToInt32(dsTempDuePrem.Tables[0].Rows[count]["IDEN_ROW_ID"].ToString());

                            decimal TempAmount = Math.Abs(TotalOverPaymentAmount);

                            if (TempTotalDue <= TempAmount)
                                AmountUpdated = TempTotalDue;
                            else
                                AmountUpdated = Math.Abs(TotalOverPaymentAmount);


                            int result = UpdateOverPaymentAdjustmentOpenItems(CustomerID, PolicyID, PolicyVersionID, RowID, AmountUpdated);

                            // Deduct the TotalOverPaymet (For eg. - 350 = -350 + 100)
                            TotalOverPaymentAmount = TotalOverPaymentAmount + TempTotalDue;

                        }
                        else
                        {
                            break;
                        }
                    }

                }


                //Update the overpayment open items.
                for (int countOP = 0; countOP < dsTempOP.Tables[0].Rows.Count; countOP++)
                {
                    decimal TempDue = 0;
                    int RowIDOP = 0;
                    TempDue = Convert.ToDecimal(dsTempOP.Tables[0].Rows[countOP]["TOTAL_DUE"].ToString());
                    RowIDOP = Convert.ToInt32(dsTempOP.Tables[0].Rows[countOP]["IDEN_ROW_ID"].ToString());

                    int result = UpdateOverPaymentItemsOpen(CustomerID, PolicyID, PolicyVersionID, RowIDOP, TempDue, "NP");
                }
            }
        }


        /// <summary>
        /// It will get the Over Payment Open Items.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns></returns>
        private DataSet GetOverPaymentOpenItems(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            objWrapper.ClearParameteres();
            objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
            objWrapper.AddParameter("@POLICY_ID", PolicyID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);

            DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetOverPaymentOpenItems");
            objWrapper.ClearParameteres();
            return dsTemp;
        }


        /// <summary>
        /// It will get the Due Premium Open Items.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <returns></returns>
        private DataSet GetDuePremiumOpenItems(int CustomerID, int PolicyID, int PolicyVersionID)
        {
            try
            {
                objWrapper.ClearParameteres();
                objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objWrapper.AddParameter("@POLICY_ID", PolicyID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);

                DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetDuePremiumOpenItems");
                objWrapper.ClearParameteres();
                return dsTemp;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// It will Update the OverPayment Open Items.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="RowID"></param>
        /// <param name="TotalPaid"></param>
        /// <returns></returns>
        private int UpdateOverPaymentAdjustmentOpenItems(int CustomerID, int PolicyID, int PolicyVersionID, int RowID, decimal TotalPaid)
        {

            objWrapper.ClearParameteres();
            objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
            objWrapper.AddParameter("@POLICY_ID", PolicyID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
            objWrapper.AddParameter("@IDEN_ROW_ID", RowID);
            objWrapper.AddParameter("@TOTAL_PAID", TotalPaid);

            int result = objWrapper.ExecuteNonQuery("Proc_UpdateOverPaymentAdjustmentOpenItems");
            objWrapper.ClearParameteres();
            return result;
        }


        /// <summary>
        /// It will update the OverPayment Open Items and their status.
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="PolicyID"></param>
        /// <param name="PolicyVersionID"></param>
        /// <param name="RowID"></param>
        /// <param name="TotalPaid"></param>
        /// <param name="ItemStatus"></param>
        /// <returns></returns>
        private int UpdateOverPaymentItemsOpen(int CustomerID, int PolicyID, int PolicyVersionID, int RowID, decimal TotalPaid, string ItemStatus)
        {
            objWrapper.ClearParameteres();
            objWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
            objWrapper.AddParameter("@POLICY_ID", PolicyID);
            objWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID);
            objWrapper.AddParameter("@IDEN_ROW_ID", RowID);
            objWrapper.AddParameter("@TOTAL_PAID", TotalPaid);
            objWrapper.AddParameter("@ITEM_STATUS", ItemStatus);

            int result = objWrapper.ExecuteNonQuery("Proc_UpdateOverPaymentItemsOpen");
            objWrapper.ClearParameteres();
            return result;
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


    }
}
