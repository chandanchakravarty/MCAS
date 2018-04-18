using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
using Cms.Model.Maintenance;
using Cms.Model.Quote;

namespace Cms.BusinessLayer.BlQuote
{
    public class ClsQQMarineRiskDetails: Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
    {
        //Added by Ruchika Chauhan on 17-March-2012 for TFS Bug # 4171
        public static DataSet GetCountryForPageControls()
        {
            string strSql = "Proc_Get_COUNTRY_FROM_MNT_PORT_MASTER";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
            return objDataSet;
        }

        //Added by Ruchika Chauhan on 19-March-2012 for TFS Bug # 4171
        public static DataSet GetVesselNameForPageControls()
        {
            string strSql = "Proc_Get_MNT_VESSEL_MASTER";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
            return objDataSet;
        }


        public static DataSet GetAppEffectiveDate(int customerId, int PolicyId, int PolicyVersionID, int QuoteId)
        {
            string strSql = "Proc_Get_VoyageFromDate";
            DataSet dsDate = null;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerId, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyId, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID, SqlDbType.Int);
                objDataWrapper.AddParameter("@QUOTE_ID", QuoteId, SqlDbType.Int);
                dsDate = objDataWrapper.ExecuteDataSet(strSql);
                return dsDate;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public int Add(ClsQQMarineRiskDetailsInfo objNewQQMarineRiskInfo)
        {

            string strStoredProc = "";
            strStoredProc = "Proc_Insert_QQ_MARINECARGO_RISK_DETAILS";

            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", objNewQQMarineRiskInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", objNewQQMarineRiskInfo.POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", objNewQQMarineRiskInfo.POLICY_VERSION_ID);
                objDataWrapper.AddParameter("@QUOTE_ID", objNewQQMarineRiskInfo.QUOTE_ID);

                objDataWrapper.AddParameter("@VOYAGE_TO", objNewQQMarineRiskInfo.VOYAGE_TO);
                objDataWrapper.AddParameter("@VOYAGE_FROM", objNewQQMarineRiskInfo.VOYAGE_FROM);
                objDataWrapper.AddParameter("@THENCE_TO", objNewQQMarineRiskInfo.THENCE_TO);
                objDataWrapper.AddParameter("@VESSEL", objNewQQMarineRiskInfo.VESSEL);
                objDataWrapper.AddParameter("@AIRCRAFT_NUMBER", objNewQQMarineRiskInfo.AIRCRAFT_NUMBER);
                objDataWrapper.AddParameter("@LAND_TRANSPORT", objNewQQMarineRiskInfo.LAND_TRANSPORT);
                objDataWrapper.AddParameter("@VOYAGE_FROM_DATE", objNewQQMarineRiskInfo.VOYAGE_FROM_DATE);
                objDataWrapper.AddParameter("@QUANTITY_DESCRIPTION", objNewQQMarineRiskInfo.QUANTITY_DESCRIPTION);
                objDataWrapper.AddParameter("@INSURANCE_CONDITIONS1", objNewQQMarineRiskInfo.INSURANCE_CONDITIONS1);
                objDataWrapper.AddParameter("@INSURANCE_CONDITIONS1_SELECTION", objNewQQMarineRiskInfo.INSURANCE_CONDITIONS1_SELECTION);
                objDataWrapper.AddParameter("@INSURANCE_CONDITIONS2", objNewQQMarineRiskInfo.INSURANCE_CONDITIONS2);
                objDataWrapper.AddParameter("@INSURANCE_CONDITIONS3", objNewQQMarineRiskInfo.INSURANCE_CONDITIONS3);
                

                objDataWrapper.AddParameter("@IS_ACTIVE", objNewQQMarineRiskInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@CREATED_BY", objNewQQMarineRiskInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME", objNewQQMarineRiskInfo.CREATED_DATETIME);
                objDataWrapper.AddParameter("@MODIFIED_BY", objNewQQMarineRiskInfo.MODIFIED_BY);

                int returnResult = 0;
             
                if (TransactionLogRequired)
                {
                    objNewQQMarineRiskInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsweb\aspx\QQMarineRiskDetails.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = "";
                    if (objNewQQMarineRiskInfo.TransactLabel != "")
                    {

                        strTranXML = objBuilder.GetTransactionLogXML(objNewQQMarineRiskInfo);
                    }
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.CLIENT_ID = objNewQQMarineRiskInfo.CUSTOMER_ID;
                    objTransactionInfo.APP_ID = 0;
                    objTransactionInfo.APP_VERSION_ID = 0;
                    objTransactionInfo.RECORDED_BY = objNewQQMarineRiskInfo.CREATED_BY;
                    objTransactionInfo.RECORD_DATE_TIME = objNewQQMarineRiskInfo.CREATED_DATETIME;
                    //objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1448", ""); 
                    objTransactionInfo.CHANGE_XML = strTranXML;

                    //Executing the query
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);



                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }

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

        public DataSet GetQQ_MarineCargo_Risk_Details(int customerId, int PolicyId, int PolicyVersionID, int QuoteId)
        {
            string strStoredProc = "PROC_GET_QQ_MARINECARGO_RISK_DETAILS";
            DataSet dsMarine = null;

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerId, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyId, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID, SqlDbType.Int);
                objDataWrapper.AddParameter("@QUOTE_ID", QuoteId, SqlDbType.Int);
                dsMarine = objDataWrapper.ExecuteDataSet(strStoredProc);
                return dsMarine;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public int Update(ClsQQMarineRiskDetailsInfo  objMarineRiskInfo)
        {
            string strTranXML="";
            int returnResult = 0;
            string strStoredProc = "";
            strStoredProc = "Proc_Insert_QQ_MARINECARGO_RISK_DETAILS";
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", objMarineRiskInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", objMarineRiskInfo.POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", objMarineRiskInfo.POLICY_VERSION_ID);
                objDataWrapper.AddParameter("@QUOTE_ID", objMarineRiskInfo.QUOTE_ID);

                objDataWrapper.AddParameter("@VOYAGE_TO", objMarineRiskInfo.VOYAGE_TO);
                objDataWrapper.AddParameter("@VOYAGE_FROM", objMarineRiskInfo.VOYAGE_FROM);
                objDataWrapper.AddParameter("@THENCE_TO", objMarineRiskInfo.THENCE_TO);
                objDataWrapper.AddParameter("@VESSEL", objMarineRiskInfo.VESSEL);
                objDataWrapper.AddParameter("@AIRCRAFT_NUMBER", objMarineRiskInfo.AIRCRAFT_NUMBER);
                objDataWrapper.AddParameter("@LAND_TRANSPORT", objMarineRiskInfo.LAND_TRANSPORT);
                objDataWrapper.AddParameter("@VOYAGE_FROM_DATE", objMarineRiskInfo.VOYAGE_FROM_DATE);
                objDataWrapper.AddParameter("@QUANTITY_DESCRIPTION", objMarineRiskInfo.QUANTITY_DESCRIPTION);
                objDataWrapper.AddParameter("@INSURANCE_CONDITIONS1", objMarineRiskInfo.INSURANCE_CONDITIONS1);
                objDataWrapper.AddParameter("@INSURANCE_CONDITIONS1_SELECTION", objMarineRiskInfo.INSURANCE_CONDITIONS1_SELECTION);
                objDataWrapper.AddParameter("@INSURANCE_CONDITIONS2", objMarineRiskInfo.INSURANCE_CONDITIONS2);
                objDataWrapper.AddParameter("@INSURANCE_CONDITIONS3", objMarineRiskInfo.INSURANCE_CONDITIONS3);

                objDataWrapper.AddParameter("@IS_ACTIVE", objMarineRiskInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objMarineRiskInfo.LAST_UPDATED_DATETIME);
                objDataWrapper.AddParameter("@MODIFIED_BY", objMarineRiskInfo.MODIFIED_BY);

                if (TransactionLogRequired)
                {
                    objMarineRiskInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"cmsweb\aspx\QQMarineRiskDetails.aspx.resx");
                    if (objMarineRiskInfo.TransactLabel != "")
                    {
                        strTranXML = objBuilder.GetTransactionLogXML(objMarineRiskInfo);
                    }                    
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 3;
                    objTransactionInfo.RECORDED_BY = objMarineRiskInfo.MODIFIED_BY;
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

    }
}
