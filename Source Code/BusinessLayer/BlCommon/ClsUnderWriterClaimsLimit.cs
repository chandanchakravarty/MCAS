using System;
using System.Text;
using System.Xml;
using Cms.DataLayer;
using System.Data;
using System.Configuration;
using Cms.Model.Maintenance;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


namespace Cms.BusinessLayer.BlCommon
{

    public class ClsUnderWriterClaimsLimit : Cms.BusinessLayer.BlCommon.ClsCommon, IDisposable
    {
        private const string MNT_ACCUMULATION_CRITERIA_MASTER = "MNT_UNDERWRITING_CLAIM_LIMITS";

        #region Private Instance Variables

        private bool boolTransactionLog;
        private const string ACTIVATE_DEACTIVATE_PROC = "MNT_UNDERWRITING_CLAIM_LIMITS";

        #endregion

        #region Public Properties

        public bool TransactionLog
        {
            set
            {
                boolTransactionLog = value;
            }
            get
            {
                return boolTransactionLog;
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// deafault constructor
        /// </summary>
        public ClsUnderWriterClaimsLimit()
        {
            boolTransactionLog = base.TransactionLogRequired;
            base.strActivateDeactivateProcedure = ACTIVATE_DEACTIVATE_PROC;
        }
        #endregion

        #region Add(Insert) functions
        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="objBudgetCategoryInfo ">Model class object.</param>
        /// <returns>No of records effected.</returns>
        public int Add(ClsUnderwritingAuthorityLimitsInfo objUnderwritingAuthorityLimitsInfo, string XmlFilePath)
        {
            string strStoredProc = "INSERT_MNT_UNDERWRITING_CLAIM_LIMITS";
            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {
                objDataWrapper.AddParameter("@USER_ID", objUnderwritingAuthorityLimitsInfo.USER_ID);
                objDataWrapper.AddParameter("@COUNTRY_ID", objUnderwritingAuthorityLimitsInfo.COUNTRY_ID);
                objDataWrapper.AddParameter("@LOB_ID", objUnderwritingAuthorityLimitsInfo.LOB_ID);
                objDataWrapper.AddParameter("@PML_LIMIT", objUnderwritingAuthorityLimitsInfo.PML_LIMIT);
                objDataWrapper.AddParameter("@PREMIUM_APPROVAL_LIMIT", objUnderwritingAuthorityLimitsInfo.PREMIUM_APPROVAL_LIMIT);
                objDataWrapper.AddParameter("@CLAIM_RESERVE_LIMIT", objUnderwritingAuthorityLimitsInfo.CLAIM_RESERVE_LIMIT);
                objDataWrapper.AddParameter("@CLAIM_REOPEN", objUnderwritingAuthorityLimitsInfo.CLAIM_REOPEN);
                objDataWrapper.AddParameter("@CLAIM_SETTLMENT_LIMIT", objUnderwritingAuthorityLimitsInfo.CLAIM_SETTLMENT_LIMIT);
                objDataWrapper.AddParameter("@CREATED_BY", objUnderwritingAuthorityLimitsInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME", objUnderwritingAuthorityLimitsInfo.CREATED_DATETIME);

                objDataWrapper.AddParameter("@EFFECTIVEDATE", objUnderwritingAuthorityLimitsInfo.EffectiveDate);
                objDataWrapper.AddParameter("@ENDDATE", objUnderwritingAuthorityLimitsInfo.EndDate);

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@ASSIGN_ID", objUnderwritingAuthorityLimitsInfo.ASSIGN_ID, SqlDbType.Int, ParameterDirection.Output);

                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    //objFundTypeInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddBudgetCatgory.aspx.resx");
                    objUnderwritingAuthorityLimitsInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objUnderwritingAuthorityLimitsInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 229;
                    objTransactionInfo.RECORDED_BY = objUnderwritingAuthorityLimitsInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = "";
                    objTransactionInfo.CHANGE_XML = strTranXML;
                    //Executing the query
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }


                int ASSIGN_ID;
                if (objSqlParameter.Value != null && objSqlParameter.Value.ToString() != "")
                {
                    ASSIGN_ID = int.Parse(objSqlParameter.Value.ToString());
                }
                else
                {
                    ASSIGN_ID = -1;
                }
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                if (ASSIGN_ID == -1)
                {
                    return -1;
                }
                else
                {
                    objUnderwritingAuthorityLimitsInfo.ASSIGN_ID = ASSIGN_ID;
                    return returnResult;
                }
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
        
        public DataTable GetLobForCountry(int Country_ID)
        {
            string strProcedure = "Proc_GetLobByCountryID";
            DataSet objDataSet = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                objDataWrapper.AddParameter("@COUNTRY_ID", Country_ID);
                objDataSet = objDataWrapper.ExecuteDataSet(strProcedure);
                return objDataSet.Tables[0];
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

        public static string GetXmlForPageControls(string ASSIGN_ID)
        {
            string strSql = "Get_MNT_UNDERWRITING_CLAIM_LIMITS";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@ASSIGN_ID", int.Parse(ASSIGN_ID));
            DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
            return objDataSet.GetXml();
        }
    
        #region Update method
        /// <summary>
        /// Update method that recieves Model object to save.
        /// </summary>
        /// <param name="objOldBudgetCategoryInfo ">Model object having old information</param>
        /// <param name="objBudgetCategoryInfo ">Model object having new information(form control's value)</param>
        /// <returns>No. of rows updated (1 or 0)</returns>
        public int Update(ClsUnderwritingAuthorityLimitsInfo objOldUnderwritingAuthorityLimitsInfo, ClsUnderwritingAuthorityLimitsInfo objUnderwritingAuthorityLimitsInfo, string XmlFilePath)
        {
            string strStoredProc = "UPDATE_MNT_UNDERWRITING_CLAIM_LIMITS";
            string strTranXML;
            int returnResult = 0;
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@USER_ID", objUnderwritingAuthorityLimitsInfo.USER_ID);
                objDataWrapper.AddParameter("@COUNTRY_ID", objUnderwritingAuthorityLimitsInfo.COUNTRY_ID);
                objDataWrapper.AddParameter("@LOB_ID", objUnderwritingAuthorityLimitsInfo.LOB_ID);
                objDataWrapper.AddParameter("@PML_LIMIT", objUnderwritingAuthorityLimitsInfo.PML_LIMIT);
                objDataWrapper.AddParameter("@PREMIUM_APPROVAL_LIMIT", objUnderwritingAuthorityLimitsInfo.PREMIUM_APPROVAL_LIMIT);
                objDataWrapper.AddParameter("@CLAIM_RESERVE_LIMIT", objUnderwritingAuthorityLimitsInfo.CLAIM_RESERVE_LIMIT);
                objDataWrapper.AddParameter("@CLAIM_REOPEN", objUnderwritingAuthorityLimitsInfo.CLAIM_REOPEN);
                objDataWrapper.AddParameter("@CLAIM_SETTLMENT_LIMIT", objUnderwritingAuthorityLimitsInfo.CLAIM_SETTLMENT_LIMIT);
                objDataWrapper.AddParameter("@MODIFIED_BY", objUnderwritingAuthorityLimitsInfo.MODIFIED_BY);
                objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objUnderwritingAuthorityLimitsInfo.LAST_UPDATED_DATETIME);
                objDataWrapper.AddParameter("@ASSIGN_ID", objUnderwritingAuthorityLimitsInfo.ASSIGN_ID);


                //NOTE: For TFS Bug # 3322
                //The Effective and End dates are not needed to be updated as Underwriting limit history is being maintained.                


                if (base.TransactionLogRequired)
                {
                    //objBudgetCategoryInfo .TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddBudgetCatgory.aspx.resx");
                    objUnderwritingAuthorityLimitsInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);

                    objBuilder.GetUpdateSQL(objOldUnderwritingAuthorityLimitsInfo, objUnderwritingAuthorityLimitsInfo, out strTranXML);

                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 3;
                    objTransactionInfo.RECORDED_BY = objUnderwritingAuthorityLimitsInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = "Information Has Been Updated";
                    objTransactionInfo.CHANGE_XML = strTranXML;
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);

                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return returnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (objDataWrapper != null)
                {
                    objDataWrapper.Dispose();
                }
                if (objBuilder != null)
                {
                    objBuilder = null;
                }
            }
        }
        #endregion
    }

}