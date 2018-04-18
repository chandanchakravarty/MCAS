/******************************************************************************************
<Author				: -	Pravesh K Chandel
<Start Date			: -	29 March 2010
<End Date			: -	
<Description		: -	Class file for Policy Product Coverages
<Review Date		: -  
<Reviewed By		: - 	
*******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Policy;
using Cms.BusinessLayer.BlCommon;
using System.Collections;

namespace Cms.BusinessLayer.BlApplication
{
    public class ClsProductCoverages : ClsCoverages
    {
        #region Private Instance Variables
        //private bool boolTransactionRequired;
        //private int thiscreatedby;
        //private int thisModifiedby;



        //DataTable dtProduct = null;
       // bool AutoRemoveXMLLoaded;
        internal string strLOB = "";
        #endregion



        #region Coverages --Policy

        /// <summary>
        /// Retirns a dataset after applying business rules for motorcycle
        /// </summary>
        /// <returns>dataset after filteration of records (coverages) that are no longer required </returns>
        public DataSet GetPolicyProductCoverages(int customerID, int polID, int polVersionID, int riskID, string polType, int langID)
        {
            try
            {
                //fetching dataset with all coverages
                DataSet dsCoverages = null;
                string strStoredProc = "Proc_Get_POL_PRODUCT_COVERAGES";
                DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

                objWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objWrapper.AddParameter("@POL_ID", polID);
                objWrapper.AddParameter("@POL_VERSION_ID", polVersionID);
                objWrapper.AddParameter("@RISK_ID", riskID);
                objWrapper.AddParameter("@POL_TYPE", polType);
                objWrapper.AddParameter("@LANG_ID", langID);

                dsCoverages = objWrapper.ExecuteDataSet(strStoredProc);

                return dsCoverages;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion
        #region save - update product Coverages
        /// <summary>
        /// Saves Product coverages 
        /// </summary>
        /// <param name="alNewCoverages"></param>
        /// <param name="strOldXML"></param>
        /// <returns></returns>
        public int SavePolicyProductCoverages(ArrayList alNewCoverages, string strOldXML, string strCustomInfo, string POL_STATUS, string Transaction_Type)
        {
            string strStoredProc = "Proc_SAVE_POLICY_PRODUCT_COVERAGES";
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            SqlCommand cmdCoverage = new SqlCommand();
            cmdCoverage.CommandText = strStoredProc;
            cmdCoverage.CommandType = CommandType.StoredProcedure;

            SqlParameter[] param = new SqlParameter[16];
            XmlElement root = null;
            XmlDocument xmlDoc = new XmlDocument();

            StringBuilder sbTranXML = new StringBuilder();

            sbTranXML.Append("<root>");

            if (strOldXML != "")
            {
                //strOldXML = ReplaceXMLCharacters(strOldXML);
                xmlDoc.LoadXml(strOldXML);
                root = xmlDoc.DocumentElement; //holds the root of the transaction XML
            }

            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

            int customerID = 0;
            int policyID = 0;
            int policyVersionID = 0;
            int riskID = 0;

            try
            {
                for (int i = 0; i < alNewCoverages.Count; i++)
                {
                    Cms.Model.Policy.ClsPolicyCoveragesInfo objNew = (Cms.Model.Policy.ClsPolicyCoveragesInfo)alNewCoverages[i];

                    customerID = objNew.CUSTOMER_ID;
                    policyID = objNew.POLICY_ID;
                    policyVersionID = objNew.POLICY_VERSION_ID;
                    riskID = objNew.RISK_ID;

                    objWrapper.ClearParameteres();
                    objWrapper.AddParameter("@CUSTOMER_ID", objNew.CUSTOMER_ID);
                    objWrapper.AddParameter("@POLICY_ID", objNew.POLICY_ID);
                    objWrapper.AddParameter("@POLICY_VERSION_ID", objNew.POLICY_VERSION_ID);
                    objWrapper.AddParameter("@RISK_ID", objNew.RISK_ID);
                    objWrapper.AddParameter("@COVERAGE_ID", objNew.COVERAGE_ID);
                    objWrapper.AddParameter("@COVERAGE_CODE_ID", objNew.COVERAGE_CODE_ID);
                    objWrapper.AddParameter("@LIMIT_1_TYPE", objNew.LIMIT_1_TYPE);
                    objWrapper.AddParameter("@LIMIT_2_TYPE", objNew.LIMIT_2_TYPE);
                    objWrapper.AddParameter("@DEDUCTIBLE_1", DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_1));
                    objWrapper.AddParameter("@DEDUCTIBLE_2", DefaultValues.GetDoubleNullFromNegative(objNew.DEDUCTIBLE_2));
                    objWrapper.AddParameter("@LIMIT_1", DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_1));
                    objWrapper.AddParameter("@LIMIT_2", DefaultValues.GetDoubleNullFromNegative(objNew.LIMIT_2));
                    objWrapper.AddParameter("@LIMIT1_AMOUNT_TEXT", objNew.LIMIT1_AMOUNT_TEXT);
                    objWrapper.AddParameter("@LIMIT2_AMOUNT_TEXT", objNew.LIMIT2_AMOUNT_TEXT);
                    objWrapper.AddParameter("@DEDUCTIBLE1_AMOUNT_TEXT", objNew.DEDUCTIBLE1_AMOUNT_TEXT);
                    objWrapper.AddParameter("@DEDUCTIBLE2_AMOUNT_TEXT", objNew.DEDUCTIBLE2_AMOUNT_TEXT);
                    objWrapper.AddParameter("@DEDUCTIBLE_1_TYPE", objNew.DEDUCTIBLE_1_TYPE);
                    objWrapper.AddParameter("@DEDUCTIBLE_2_TYPE", objNew.DEDUCTIBLE_2_TYPE);
                    ////objWrapper.AddParameter("@WRITTEN_PREMIUM", DefaultValues.GetDoubleNullFromNegative(objNew.WRITTEN_PREMIUM));
                    ////objWrapper.AddParameter("@FULL_TERM_PREMIUM", DefaultValues.GetDoubleNullFromNegative(objNew.FULL_TERM_PREMIUM));
                    objWrapper.AddParameter("@WRITTEN_PREMIUM", DefaultValues.GetDoubleNullFromNegativeMiniValue(objNew.WRITTEN_PREMIUM));
                    objWrapper.AddParameter("@FULL_TERM_PREMIUM", DefaultValues.GetDoubleNullFromNegativeMiniValue(objNew.FULL_TERM_PREMIUM));
                    objWrapper.AddParameter("@LIMIT_ID", DefaultValues.GetIntNullFromNegative(objNew.LIMIT_ID));
                    objWrapper.AddParameter("@DEDUC_ID", DefaultValues.GetIntNullFromNegative(objNew.DEDUC_ID));
                    objWrapper.AddParameter("@ADD_INFORMATION", objNew.ADD_INFORMATION);

                    objWrapper.AddParameter("@RI_APPLIES", objNew.RI_APPLIES);
                    objWrapper.AddParameter("@MINIMUM_DEDUCTIBLE", DefaultValues.GetDoubleNullFromNegative(objNew.MINIMUM_DEDUCTIBLE));
                    objWrapper.AddParameter("@DEDUCTIBLE_REDUCES", objNew.DEDUCTIBLE_REDUCES);
                    objWrapper.AddParameter("@INITIAL_RATE", DefaultValues.GetDoubleNullFromNegative(objNew.INITIAL_RATE));
                    objWrapper.AddParameter("@FINAL_RATE", DefaultValues.GetDoubleNullFromNegative(objNew.FINAL_RATE));
                    objWrapper.AddParameter("@AVERAGE_RATE", objNew.AVERAGE_RATE);
                    objWrapper.AddParameter("@IS_ACTIVE", objNew.IS_ACTIVE);
                    //ankit
                    if (objNew.IDEMNITY_PERIOD > 0)
                        objWrapper.AddParameter("@Indemnity_Period", objNew.IDEMNITY_PERIOD);
                    else
                        objWrapper.AddParameter("@Indemnity_Period", System.DBNull.Value);

                    objWrapper.AddParameter("@CREATED_BY", objNew.CREATED_BY);


                    if (objNew.CREATED_DATETIME != DateTime.MinValue)
                        objWrapper.AddParameter("@CREATED_DATETIME", objNew.CREATED_DATETIME);
                    else
                        objWrapper.AddParameter("@CREATED_DATETIME", System.DBNull.Value);
                    objWrapper.AddParameter("@MODIFIED_BY", objNew.MODIFIED_BY);

                    if (objNew.LAST_UPDATED_DATETIME != DateTime.MinValue)
                        objWrapper.AddParameter("@LAST_UPDATED_DATETIME", objNew.LAST_UPDATED_DATETIME);
                    else
                        objWrapper.AddParameter("@LAST_UPDATED_DATETIME", System.DBNull.Value);

                    objWrapper.AddParameter("@POL_STATUS", POL_STATUS);
                    if (Transaction_Type != "")
                        objWrapper.AddParameter("@TRANSACTION_TYPE", int.Parse(Transaction_Type));

                    string strTranXML = "";

                    objTransactionInfo.RECORDED_BY = objNew.CREATED_BY;

                    if (objNew.ACTION == "I")
                    {
                        //Insert
                        objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/AddPolicyCoverages.aspx.resx");
                        SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                        strTranXML = objBuilder.GetTransactionLogXML(objNew);

                        objTransactionInfo.TRANS_TYPE_ID = 193;
                        objTransactionInfo.POLICY_ID = objNew.POLICY_ID;
                        objTransactionInfo.POLICY_VER_TRACKING_ID = objNew.POLICY_VERSION_ID;
                        objTransactionInfo.CLIENT_ID = objNew.CUSTOMER_ID;
                        //objTransactionInfo.RECORDED_BY		=	objNew.MODIFIED_BY;
                        objTransactionInfo.TRANS_DESC = "Product Coverage added.";
                        objTransactionInfo.CHANGE_XML = strTranXML;

                        sbTranXML.Append(strTranXML);
                        //objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
                        objWrapper.ExecuteNonQuery(strStoredProc);
                        objWrapper.ClearParameteres();

                    }
                    else if (objNew.ACTION == "U")
                    {
                        objTransactionInfo.TRANS_TYPE_ID = 193;
                        //Update	
                        objNew.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/addPolicyCoverages.aspx.resx");
                        objTransactionInfo.TRANS_DESC = "Product coverage updated.";
                        strTranXML = this.GetPolicyHomeTranXML(objNew, strOldXML, objNew.COVERAGE_ID, root);
                        if (strTranXML != "<LabelFieldMapping></LabelFieldMapping>")
                            sbTranXML.Append(strTranXML);
                        //objWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
                        objWrapper.ExecuteNonQuery(strStoredProc);
                        objWrapper.ClearParameteres();
                    }

                }
                objWrapper.ClearParameteres();
                //Delete Coverages/////////////////////////////////////
                //strCustomInfo = "";
                for (int i = 0; i < alNewCoverages.Count; i++)
                {

                    Cms.Model.Policy.ClsPolicyCoveragesInfo objDelete = (Cms.Model.Policy.ClsPolicyCoveragesInfo)alNewCoverages[i];

                    if (objDelete.ACTION == "D")
                    {
                        objTransactionInfo.TRANS_TYPE_ID = 193;
                        objWrapper.AddParameter("@CUSTOMER_ID", objDelete.CUSTOMER_ID);
                        objWrapper.AddParameter("@POLICY_ID", objDelete.POLICY_ID);
                        objWrapper.AddParameter("@POLICY_VERSION_ID", objDelete.POLICY_VERSION_ID);
                        objWrapper.AddParameter("@RISK_ID", objDelete.RISK_ID);
                        objWrapper.AddParameter("@COVERAGE_ID", objDelete.COVERAGE_ID);
                        //Delete the coverage
                        objWrapper.ExecuteNonQuery("Proc_Delete_POL_PRODUCT_COVERAGES");
                        //Get Tran log
                        objDelete.TransactLabel = ClsCommon.MapTransactionLabel("policies/aspx/AddPolicyCoverages.aspx.resx");
                        SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                        //string strTranXML = objBuilder.GetTransactionLogXML(objDelete);
                        string strTranXML = objBuilder.GetDeleteTransactionLogXML(objDelete);

                        sbTranXML.Append(strTranXML);

                        objWrapper.ClearParameteres();
                    }
                }
                sbTranXML.Append("</root>");

                //if(sbTranXML.ToString()!="<root></root>")
                //	strCustomInfo+=";Following coverages have been added/updated";

                if (sbTranXML.ToString() != "<root></root>")// || strCustomInfo!="")
                {

                    objTransactionInfo.POLICY_ID = policyID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = policyVersionID;
                    objTransactionInfo.CLIENT_ID = customerID;

                    objTransactionInfo.TRANS_DESC = "";
                    if (sbTranXML.ToString() != "<root></root>")
                        objTransactionInfo.CHANGE_XML = sbTranXML.ToString();

                    objTransactionInfo.CUSTOM_INFO = strCustomInfo;

                    objWrapper.ClearParameteres();

                    objWrapper.ExecuteNonQuery(objTransactionInfo);
                }
                /*	//Update Policy Coverages///////////////
                    if(alNewCoverages.Count>0)
                    {
                        UpdatePolicyCoverages(alNewCoverages,objWrapper,vehicleID,customerID,policyID,policyVersionID);
                        /////////////////////////////////////////
                        //Update relevant endorsements from coverages/////////
                        UpdateEndorsmentPolicy(objWrapper,customerID,policyID,policyVersionID,vehicleID);
                        //UpdatePolicyEndorsements(customerID,policyID,policyVersionID,vehicleID,objWrapper);
                    }
                    /////////////////////
                    */

            }
            catch (Exception ex)
            {
                //tran.Rollback();
                //conn.Close();
                objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                if (ex.InnerException != null)
                {
                    string message = ex.InnerException.Message.ToLower();


                    if (message.StartsWith("violation of primary key"))
                    {
                        return -2;
                    }

                }

                throw (ex);
            }
            objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            return 1;
        }

        public int UpdateRisk(int lob, int customer, int policy, int policyversion, string risk)
        {
            DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            SqlCommand cmdCoverage = new SqlCommand();
            try
            {
                string strStoredProc = "Proc_GetBasicSumLimit";

                cmdCoverage.CommandText = strStoredProc;
                cmdCoverage.CommandType = CommandType.StoredProcedure;
                objWrapper.AddParameter("@LOB_ID", lob);
                objWrapper.AddParameter("@CUSTOMER_ID", customer);
                objWrapper.AddParameter("@POLICY_ID", policy);
                objWrapper.AddParameter("@POLICY_VERSION_ID", policyversion);
                objWrapper.AddParameter("@PRODUCT_RISK_ID", risk);
                objWrapper.ExecuteNonQuery(strStoredProc);
                objWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                //tran.Rollback();
                //conn.Close();
                objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                if (ex.InnerException != null)
                {
                    string message = ex.InnerException.Message.ToLower();


                    if (message.StartsWith("please try again"))
                    {
                        return -2;
                    }

                }

                throw (ex);
            }
            objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
            return 1;
        }
        #endregion
    }
}
