/******************************************************************************************
<Author				: -     Avijit Goswami
<Start Date			: -	    20/01/2012
<End Date			: -	
<Description		: -     Business layer class to add, update and delete
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 
<Modified By		: - 
<Purpose			: - 
*******************************************************************************************/
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
using Cms.Model.Maintenance;


namespace Cms.BusinessLayer.BlCommon
{
   public class ClsCurrencyMasterDetails : Cms.BusinessLayer.BlCommon.ClsCommon, IDisposable
    {
       private const string MNT_CURRENCY_MASTER = "Add_MNT_CURRENCY_MASTER";
        #region Private Instance Variables
        private	bool	boolTransactionLog;		
        private const string ACTIVATE_DEACTIVATE_PROC = "ACTIVATE_DEACTIVATE_MNT_CURRENCY_MASTER";       
        #endregion
       
        #region Public Properties
        public bool TransactionLog
        {
            set
            {
                boolTransactionLog	=	value;
            }
            get
            {
                return boolTransactionLog;
            }
        }
        #endregion

        #region private Utility Functions
        #endregion

        #region Constructors
        /// <summary>
        /// deafault constructor
        /// </summary>
        public ClsCurrencyMasterDetails()
        {
            boolTransactionLog	= base.TransactionLogRequired;
            base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;           
        }
        #endregion
        #region Add(Insert) functions
        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="objBudgetCategoryInfo ">Model class object.</param>
        /// <returns>No of records effected.</returns>
        public int Add(ClsCurrencyMasterDetailsInfo objCurrencyMasterDetailsInfo, string XmlFilePath)
        {
            string strStoredProc = "Proc_Add_MNT_CURRENCY_MASTER";
            DateTime	RecordDate		=	DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);                       
            try
            {
                objDataWrapper.AddParameter("@CURR_CODE", objCurrencyMasterDetailsInfo.CURR_CODE);
                objDataWrapper.AddParameter("@CURR_DESC", objCurrencyMasterDetailsInfo.CURR_DESC);
                objDataWrapper.AddParameter("@CURR_SYMBOL", objCurrencyMasterDetailsInfo.CURR_SYMBOL);
                
                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@CURRENCY_ID", objCurrencyMasterDetailsInfo.CURRENCY_ID, SqlDbType.Int, ParameterDirection.Output);
                int returnResult = 0;
                if(TransactionLogRequired)
                {
                    //objFundTypeInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddBudgetCatgory.aspx.resx");
                    objCurrencyMasterDetailsInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objCurrencyMasterDetailsInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID	=	1;
                    objTransactionInfo.RECORDED_BY = objCurrencyMasterDetailsInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC		=	"Record Has Been Added";
                    objTransactionInfo.CHANGE_XML		=	strTranXML;
                    //Executing the query
                    returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
                }
                else
                {
                    returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
                }


                int CURRENCY_ID;
                if (objSqlParameter.Value != null && objSqlParameter.Value.ToString() != "")
                {
                    CURRENCY_ID = int.Parse(objSqlParameter.Value.ToString());
                    objCurrencyMasterDetailsInfo.CURRENCY_ID = CURRENCY_ID;
                }
                else
                {
                    CURRENCY_ID = -1;
                }
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                if (CURRENCY_ID == -1)
                {
                    return -1;
                }
                else
                {
                    objCurrencyMasterDetailsInfo.CURRENCY_ID = CURRENCY_ID;
                    return returnResult;
                }
            }
            catch(Exception ex)
            {
                throw(ex);
            }
            finally
            {
                if(objDataWrapper != null) objDataWrapper.Dispose();
            }
        }
        #endregion
        #region Update method
        /// <summary>
        /// Update method that recieves Model object to save.
        /// </summary>
        /// <param name="objOldBudgetCategoryInfo ">Model object having old information</param>
        /// <param name="objBudgetCategoryInfo ">Model object having new information(form control's value)</param>
        /// <returns>No. of rows updated (1 or 0)</returns>
        public int Update(ClsCurrencyMasterDetailsInfo objOldCurrencyMasterDetailsInfo, ClsCurrencyMasterDetailsInfo objCurrencyMasterDetailsInfo, string XmlFilePath)
        {
            string strStoredProc = "Proc_Update_MNT_CURRENCY_MASTER";
            string strTranXML;
            int returnResult = 0;
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
            try 
            {
                objDataWrapper.AddParameter("@CURRENCY_ID", objCurrencyMasterDetailsInfo.CURRENCY_ID);
                objDataWrapper.AddParameter("@CURR_CODE", objCurrencyMasterDetailsInfo.CURR_CODE);
                objDataWrapper.AddParameter("@CURR_DESC", objCurrencyMasterDetailsInfo.CURR_DESC);
                objDataWrapper.AddParameter("@CURR_SYMBOL", objCurrencyMasterDetailsInfo.CURR_SYMBOL);
                //objDataWrapper.AddParameter("@CURR_PRECISION", objCurrencyMasterDetailsInfo.CURR_PRECISION);
                //objDataWrapper.AddParameter("@CURR_CALCULATEFORMAT", objCurrencyMasterDetailsInfo.CURR_CALCULATEFORMAT);
                //objDataWrapper.AddParameter("@CURR_PRINTFORMAT", objCurrencyMasterDetailsInfo.CURR_PRINTFORMAT);
                //objDataWrapper.AddParameter("@CURR_CHECKDIGITS", objCurrencyMasterDetailsInfo.CURR_CHECKDIGITS);
                //objDataWrapper.AddParameter("@CURR_CHECKDECIMAL", objCurrencyMasterDetailsInfo.CURR_CHECKDECIMAL);
                //objDataWrapper.AddParameter("@CURR_DECIMALSEPR", objCurrencyMasterDetailsInfo.CURR_DECIMALSEPR);
                //objDataWrapper.AddParameter("@CURR_THOUSANDSEPR", objCurrencyMasterDetailsInfo.CURR_THOUSANDSEPR);
                //objDataWrapper.AddParameter("@IS_ACTIVE", objCurrencyMasterDetailsInfo.IS_ACTIVE);
                //objDataWrapper.AddParameter("@CREATED_BYL", objCurrencyMasterDetailsInfo.CREATED_BY);
                //objDataWrapper.AddParameter("@CREATED_DATETIME", objCurrencyMasterDetailsInfo.CREATED_DATETIME);
                //objDataWrapper.AddParameter("@MODIFIED_BY", objCurrencyMasterDetailsInfo.MODIFIED_BY);
                //objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objCurrencyMasterDetailsInfo.LAST_UPDATED_DATETIME);               
               
                if(base.TransactionLogRequired) 
                {                    
                    objCurrencyMasterDetailsInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);
                    objBuilder.GetUpdateSQL(objOldCurrencyMasterDetailsInfo, objCurrencyMasterDetailsInfo, out strTranXML);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID	=	3;
                    objTransactionInfo.RECORDED_BY = objCurrencyMasterDetailsInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC		=	"Information Has Been Updated";
                    objTransactionInfo.CHANGE_XML		=	strTranXML;
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return returnResult;
            }
            catch(Exception ex)
            {
                throw(ex);
            }
            finally
            {
                if(objDataWrapper != null) 
                {
                    objDataWrapper.Dispose();
                }
                if(objBuilder != null) 
                {
                    objBuilder = null;
                }
            }
        }
        #endregion

          #region ActivateDeactivate() function
         //<summary>
         //Saves the information passed in model object to database.
         //</summary>
         //<param name="objDVInfo">Model class object.</param>
         //<returns>No of records effected.</returns>
        public int ActivateDeactivateDefaultValues(ClsCurrencyMasterDetailsInfo objDefaultValuesInfo, string strIS_ACTIVE)
        {
            string strStoredProc = "ACTIVATE_DEACTIVATE_MNT_CURRENCY_MASTER";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            try
            {
                objDataWrapper.AddParameter("@CURRENCY_ID", objDefaultValuesInfo.CURRENCY_ID);
                objDataWrapper.AddParameter("@IS_ACTIVE", strIS_ACTIVE);

                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 3;
                    objTransactionInfo.RECORDED_BY = objDefaultValuesInfo.CREATED_BY;
                    objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
                    if (strIS_ACTIVE.ToUpper() == "Y")
                        objTransactionInfo.TRANS_DESC = "Accumulation Reference Default Value is Activated.";
                    else
                        objTransactionInfo.TRANS_DESC = "Accumulation Reference Default Value is Deactivated.";
                        returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                }
                else
                        returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
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
        #region "GetxmlMethods"
        public DataSet GetAccumulationReference(string CURRENCY_ID)
        {
            string strSql = "Proc_Add_MNT_CURRENCY_MASTER";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@CURRENCY_ID", CURRENCY_ID);
            DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
            return objDataSet;
        }
        public static string GetXmlForPageControls(string CURRENCY_ID)
        {
            string strSql = "PROC_GET_MNT_CURRENCY_MASTER";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@CURRENCY_ID", int.Parse(CURRENCY_ID));
            DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
            return objDataSet.GetXml();
        }
        #endregion
        public static DataSet GetWolverineUsers(string systemID)
        {			
            string strSQLQuery = "SELECT ISNULL(USER_FNAME,'') + ' ' + ISNULL(USER_LNAME,'') AS WOLVERINE_USERS,[USER_ID] as WOLVERINE_USER_ID FROM MNT_USER_LIST WHERE USER_SYSTEM_ID = '" +systemID + "' and ISNULL(IS_ACTIVE,'N') = 'Y'  ORDER BY WOLVERINE_USERS ASC";
            DataSet objDS = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.Text);				
            try
            {
                objDS = objDataWrapper.ExecuteDataSet(strSQLQuery);
                return objDS;
            }
            catch(Exception exc)
            {
                throw (new Exception("Some error occurred.Please try again !. " +exc.Message));
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }
        public static  DataSet GetAccumulationCriteriaList(int LOB_ID)
        {
            string strSql = "GET_CRITERIA_LIST";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@LOB_ID", LOB_ID);
            DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
            return objDataSet;
        }
    }
}