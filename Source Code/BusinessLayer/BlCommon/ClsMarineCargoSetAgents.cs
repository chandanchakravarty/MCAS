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
   public class ClsMarineCargoSetAgents : Cms.BusinessLayer.BlCommon.ClsCommon, IDisposable
    {
       private const string MARINE_CARGO_SETTLING_AGENTS = "PROC_INSERT_MARINE_CARGO_SETTLING_AGENTS";
        private	bool	boolTransactionLog;
        private const string ACTIVATE_DEACTIVATE_PROC = "PROC_ACTIVATE_DEACTIVATE_MARINE_CARGO_SETTLING_AGENTS"; 
       
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

        #region Constructors
        public ClsMarineCargoSetAgents()
        {
            boolTransactionLog	= base.TransactionLogRequired;
            base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;           
        }
        #endregion
        #region Add(Insert) functions        
            public int Add(ClsMarineCargoSetAgentsInfo objClsMarineCargoSetAgentsInfo)
            {
            string strStoredProc = "PROC_INSERT_MARINE_CARGO_SETTLING_AGENTS";
            DateTime	RecordDate		=	DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);                       
            try
            {                
                objDataWrapper.AddParameter("@AGENT_CODE", objClsMarineCargoSetAgentsInfo.AGENT_CODE);
                objDataWrapper.AddParameter("@AGENT_NAME", objClsMarineCargoSetAgentsInfo.AGENT_NAME);
                objDataWrapper.AddParameter("@AGENT_ADDRESS1", objClsMarineCargoSetAgentsInfo.AGENT_ADDRESS1);
                objDataWrapper.AddParameter("@AGENT_ADDRESS2", objClsMarineCargoSetAgentsInfo.AGENT_ADDRESS2);
                objDataWrapper.AddParameter("@AGENT_CITY", objClsMarineCargoSetAgentsInfo.AGENT_CITY);
                objDataWrapper.AddParameter("@AGENT_COUNTRY", objClsMarineCargoSetAgentsInfo.AGENT_COUNTRY);
                objDataWrapper.AddParameter("@AGENT_SURVEY_CODE", objClsMarineCargoSetAgentsInfo.AGENT_SURVEY_CODE);
                objDataWrapper.AddParameter("@IS_ACTIVE", objClsMarineCargoSetAgentsInfo.IS_ACTIVE);

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@AGENT_ID", objClsMarineCargoSetAgentsInfo.AGENT_ID, SqlDbType.Int, ParameterDirection.Output);
                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    string strTranXML = "";
                    objClsMarineCargoSetAgentsInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddMarineCargoSetAgents.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    if (objClsMarineCargoSetAgentsInfo.TransactLabel != "")
                    {
                        strTranXML = objBuilder.GetTransactionLogXML(objClsMarineCargoSetAgentsInfo);
                    }
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.RECORDED_BY = objClsMarineCargoSetAgentsInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = "Record Has Been Added";
                    objTransactionInfo.CHANGE_XML = strTranXML;
                    //Executing the query
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                int Agent_Id = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (Agent_Id == -1)
                {
                    return -1;
                }
                else
                {
                    objClsMarineCargoSetAgentsInfo.AGENT_ID = Agent_Id;
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

            public int Update(ClsMarineCargoSetAgentsInfo objOldClsMarineCargoSetAgentsInfo, ClsMarineCargoSetAgentsInfo objClsMarineCargoSetAgentsInfo)
            {
                string strTranXML;
                int returnResult = 0;
                TransactionLogRequired = true;
                SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                string strStoredProc = "PROC_UPDATE_MARINE_CARGO_SETTLING_AGENTS";
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                try
                {
                    objDataWrapper.AddParameter("@AGENT_ID", objClsMarineCargoSetAgentsInfo.AGENT_ID);
                    objDataWrapper.AddParameter("@AGENT_CODE", objClsMarineCargoSetAgentsInfo.AGENT_CODE);
                    objDataWrapper.AddParameter("@AGENT_NAME", objClsMarineCargoSetAgentsInfo.AGENT_NAME);
                    objDataWrapper.AddParameter("@AGENT_ADDRESS1", objClsMarineCargoSetAgentsInfo.AGENT_ADDRESS1);
                    objDataWrapper.AddParameter("@AGENT_ADDRESS2", objClsMarineCargoSetAgentsInfo.AGENT_ADDRESS2);
                    objDataWrapper.AddParameter("@AGENT_CITY", objClsMarineCargoSetAgentsInfo.AGENT_CITY);
                    objDataWrapper.AddParameter("@AGENT_COUNTRY", objClsMarineCargoSetAgentsInfo.AGENT_COUNTRY);
                    objDataWrapper.AddParameter("@AGENT_SURVEY_CODE", objClsMarineCargoSetAgentsInfo.AGENT_SURVEY_CODE);
                    objDataWrapper.AddParameter("@IS_ACTIVE", objClsMarineCargoSetAgentsInfo.IS_ACTIVE);

                    if (TransactionLogRequired)
                    {
                        objClsMarineCargoSetAgentsInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsweb\Maintenance\AddMarineCargoSetAgents.aspx.resx");
                        string strUpdate = objBuilder.GetUpdateSQL(objOldClsMarineCargoSetAgentsInfo, objClsMarineCargoSetAgentsInfo, out strTranXML);
                        Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                        objTransactionInfo.TRANS_TYPE_ID = 3;
                        objTransactionInfo.RECORDED_BY = objClsMarineCargoSetAgentsInfo.MODIFIED_BY;
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
        public DataSet GetDataForPageControls(string AGENT_ID)
        {
            string strStoredProc = "Proc_Get_MARINE_CARGO_SETTLING_AGENTS";
            DataSet objDataSet = null;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@AGENT_ID", AGENT_ID);                

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


    }
}