/******************************************************************************************
<Author				: -     Avijit Goswami
<Start Date			: -	    21/01/2012
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
using Cms.Model.Policy;
using Cms.BusinessLayer.Blapplication;


namespace Cms.BusinessLayer.Blapplication
{
    public class ClsAircraftDetails : Cms.BusinessLayer.BlCommon.ClsCommon, IDisposable
    {
        private const string POL_MARINECARGO_AIRCRAFT = "POL_MARINECARGO_AIRCRAFT";
        private bool boolTransactionLog;
        private const string ACTIVATE_DEACTIVATE_PROC = "PROC_ACTIVATE_DEACTIVATE_MARINE_CARGO_SETTLING_AGENTS";

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
                
        public ClsAircraftDetails()
        {
            boolTransactionLog = base.TransactionLogRequired;
            base.strActivateDeactivateProcedure = ACTIVATE_DEACTIVATE_PROC;
        }        
        
        public int Add(ClsAircraftInfo objClsAircraftInfo)
        {
            string strStoredProc = "PROC_INSERT_POL_MARINECARGO_AIRCRAFT";
            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@AIRCRAFT_NUMBER", objClsAircraftInfo.AIRCRAFT_NUMBER);
                objDataWrapper.AddParameter("@AIRLINE", objClsAircraftInfo.AIRLINE);
                objDataWrapper.AddParameter("@AIRCRAFT_FROM", objClsAircraftInfo.AIRCRAFT_FROM);
                objDataWrapper.AddParameter("@AIRCRAFT_TO", objClsAircraftInfo.AIRCRAFT_TO);
                objDataWrapper.AddParameter("@AIRWAY_BILL", objClsAircraftInfo.AIRWAY_BILL);                
                objDataWrapper.AddParameter("@IS_ACTIVE", objClsAircraftInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@CREATED_BY", objClsAircraftInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME", objClsAircraftInfo.CREATED_DATETIME);

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@AIRCRAFT_ID", objClsAircraftInfo.AIRCRAFT_ID, SqlDbType.Int, ParameterDirection.Output);
                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    string strTranXML = "";
                    objClsAircraftInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/MariTime/AddAircraftInformation.aspx.resx");                    
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    if (objClsAircraftInfo.TransactLabel != "")
                    {
                        strTranXML = objBuilder.GetTransactionLogXML(objClsAircraftInfo);
                    }
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.RECORDED_BY = objClsAircraftInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = "Record Has Been Added";
                    objTransactionInfo.CHANGE_XML = strTranXML;                    
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                int AIRCRAFT_ID = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (AIRCRAFT_ID == -1)
                {
                    return -1;
                }
                else
                {
                    objClsAircraftInfo.AIRCRAFT_ID = AIRCRAFT_ID;
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

        public int Update(ClsAircraftInfo objOldClsAircraftInfo, ClsAircraftInfo objClsAircraftInfo)
        {
            string strTranXML;
            int returnResult = 0;
            TransactionLogRequired = true;
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            string strStoredProc = "PROC_UPDATE_POL_MARINECARGO_AIRCRAFT";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@AIRCRAFT_ID", objOldClsAircraftInfo.AIRCRAFT_ID);
                objDataWrapper.AddParameter("@AIRCRAFT_NUMBER", objClsAircraftInfo.AIRCRAFT_NUMBER);
                objDataWrapper.AddParameter("@AIRLINE", objClsAircraftInfo.AIRLINE);
                objDataWrapper.AddParameter("@AIRCRAFT_FROM", objClsAircraftInfo.AIRCRAFT_FROM);
                objDataWrapper.AddParameter("@AIRCRAFT_TO", objClsAircraftInfo.AIRCRAFT_TO);
                objDataWrapper.AddParameter("@AIRWAY_BILL", objClsAircraftInfo.AIRWAY_BILL);
                objDataWrapper.AddParameter("@IS_ACTIVE", objClsAircraftInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@MODIFIED_BY", objClsAircraftInfo.MODIFIED_BY);
                objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objClsAircraftInfo.LAST_UPDATED_DATETIME);
                if (TransactionLogRequired)
                {
                    objClsAircraftInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/MariTime/AddAircraftInformation.aspx.resx");
                    string strUpdate = objBuilder.GetUpdateSQL(objOldClsAircraftInfo, objClsAircraftInfo, out strTranXML);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 3;
                    objTransactionInfo.RECORDED_BY = objClsAircraftInfo.MODIFIED_BY;
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

        //  #region ActivateDeactivate() function
        //public int ActivateDeactivateDefaultValues(ClsCurrencyMasterDetailsInfo objDefaultValuesInfo, string strIS_ACTIVE)
        //{
        //    string strStoredProc = "ACTIVATE_DEACTIVATE_MNT_CURRENCY_MASTER";
        //    DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
        //    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
        //    try
        //    {
        //        objDataWrapper.AddParameter("@CURRENCY_ID", objDefaultValuesInfo.CURRENCY_ID);
        //        objDataWrapper.AddParameter("@IS_ACTIVE", strIS_ACTIVE);

        //        int returnResult = 0;
        //        if (TransactionLogRequired)
        //        {
        //            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
        //            objTransactionInfo.TRANS_TYPE_ID = 3;
        //            objTransactionInfo.RECORDED_BY = objDefaultValuesInfo.CREATED_BY;
        //            objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
        //            if (strIS_ACTIVE.ToUpper() == "Y")
        //                objTransactionInfo.TRANS_DESC = "Accumulation Reference Default Value is Activated.";
        //            else
        //                objTransactionInfo.TRANS_DESC = "Accumulation Reference Default Value is Deactivated.";
        //                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
        //        }
        //        else
        //                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
        //        objDataWrapper.ClearParameteres();
        //        objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
        //        return returnResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //    finally
        //    {
        //        if (objDataWrapper != null) objDataWrapper.Dispose();
        //    }
        //}
        //#endregion    
        #region "GetxmlMethods"
        public DataSet GetDataForPageControls(string AIRCRAFT_ID)
        {
            string strStoredProc = "PROC_GET_POL_MARINECARGO_AIRCRAFT";
            DataSet objDataSet = null;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@AIRCRAFT_ID", AIRCRAFT_ID);

                objDataSet = objDataWrapper.ExecuteDataSet(strStoredProc);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }
            return objDataSet;
        }

        #endregion

        #region  "Delete AIRCRAFTID"
        public void DeletePageControl(string AIRCRAFT_ID)
        {
            string strStoredProc = "PROC_DELETE_POL_MARINECARGO_AIRCRAFT";
            DataSet objDataSet = null;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@AIRCRAFT_ID", AIRCRAFT_ID);
                objDataSet = objDataWrapper.ExecuteDataSet(strStoredProc);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }
        }
        #endregion
    }
}
