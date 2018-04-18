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
using Cms.Model.Policy;

namespace Cms.BusinessLayer.BlApplication
{
    public class ClsVoyageInformation : Cms.BusinessLayer.BlCommon.ClsCommon, IDisposable
    {
        public static DataSet GetCountryForPageControls()
        {
            string strSql = "Proc_Get_COUNTRY_FROM_MNT_PORT_MASTER";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
            return objDataSet;
        }

        public int Add(ClsVoyageInformationInfo objVoyageInfo, out int voyage_info_id)
        {

            string strStoredProc = "";            
            strStoredProc = "Proc_Insert_POL_MARINECARGO_VOYAGE_INFORMATION";

            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", objVoyageInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", objVoyageInfo.POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", objVoyageInfo.POLICY_VERSION_ID);                

                objDataWrapper.AddParameter("@VOYAGE_TO", objVoyageInfo.VOYAGE_TO);
                objDataWrapper.AddParameter("@VOYAGE_FROM", objVoyageInfo.VOYAGE_FROM);
                objDataWrapper.AddParameter("@THENCE_TO", objVoyageInfo.THENCE_TO);
                objDataWrapper.AddParameter("@QUANTITY_DESCRIPTION", objVoyageInfo.QUANTITY_DESCRIPTION);

                objDataWrapper.AddParameter("@IS_ACTIVE", objVoyageInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@CREATED_BY", objVoyageInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME", objVoyageInfo.CREATED_DATETIME);               

                
                int returnResult = 0;

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@VOYAGE_INFO_ID", null, SqlDbType.Int, ParameterDirection.Output);                                        

                if (TransactionLogRequired)
                {
                    objVoyageInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"Policies\Aspx\MariTime\AddVoyageInformation.aspx.resx");
                    
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = "";
                    if (objVoyageInfo.TransactLabel != "")
                    {

                        strTranXML = objBuilder.GetTransactionLogXML(objVoyageInfo);
                    }
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.CLIENT_ID = objVoyageInfo.CUSTOMER_ID;
                    objTransactionInfo.APP_ID = 0;
                    objTransactionInfo.APP_VERSION_ID = 0;
                    objTransactionInfo.RECORDED_BY = objVoyageInfo.CREATED_BY;
                    objTransactionInfo.RECORD_DATE_TIME = objVoyageInfo.CREATED_DATETIME;
                    //objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1448", ""); 
                    objTransactionInfo.CHANGE_XML = strTranXML;

                    //Executing the query
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                    voyage_info_id = int.Parse(objSqlParameter.Value.ToString());
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                    voyage_info_id = int.Parse(objSqlParameter.Value.ToString());
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

        public int Update(ClsVoyageInformationInfo objVoyageInfo)
        {
            string strTranXML = "";
            int returnResult = 0;
            string strStoredProc = "";
            strStoredProc = "Proc_Update_POL_MARINECARGO_VOYAGE_INFORMATION";
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@VOYAGE_INFO_ID", objVoyageInfo.VOYAGE_INFO_ID);
                //objDataWrapper.AddParameter("@CUSTOMER_ID", objVoyageInfo.CUSTOMER_ID);
                //objDataWrapper.AddParameter("@POLICY_ID", objVoyageInfo.POLICY_ID);
                //objDataWrapper.AddParameter("@POLICY_VERSION_ID", objVoyageInfo.POLICY_VERSION_ID);                

                objDataWrapper.AddParameter("@VOYAGE_TO", objVoyageInfo.VOYAGE_TO);
                objDataWrapper.AddParameter("@VOYAGE_FROM", objVoyageInfo.VOYAGE_FROM);
                objDataWrapper.AddParameter("@THENCE_TO", objVoyageInfo.THENCE_TO);
                objDataWrapper.AddParameter("@QUANTITY_DESCRIPTION", objVoyageInfo.QUANTITY_DESCRIPTION);

                objDataWrapper.AddParameter("@MODIFIED_BY", objVoyageInfo.MODIFIED_BY);
                objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objVoyageInfo.LAST_UPDATED_DATETIME);
                

                if (TransactionLogRequired)
                {
                    objVoyageInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"Policies\Aspx\MariTime\AddVoyageInformation.aspx.resx");
                    if (objVoyageInfo.TransactLabel != "")
                    {
                        strTranXML = objBuilder.GetTransactionLogXML(objVoyageInfo);
                    }
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 3;
                    objTransactionInfo.RECORDED_BY = objVoyageInfo.MODIFIED_BY;
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

        public DataSet GetPOL_MARINECARGO_VOYAGE_INFO(int VOYAGE_INFO_ID)
        {
            string strStoredProc = "PROC_GET_POL_MARINECARGO_VOYAGE_INFORMATION";
            DataSet dsVOYAGE = null;

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@VOYAGE_INFO_ID", VOYAGE_INFO_ID, SqlDbType.Int);
                //objDataWrapper.AddParameter("@POLICY_ID", PolicyId, SqlDbType.Int);
                //objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID, SqlDbType.Int);
                //objDataWrapper.AddParameter("@QUOTE_ID", QuoteId, SqlDbType.Int);
                dsVOYAGE = objDataWrapper.ExecuteDataSet(strStoredProc);
                return dsVOYAGE;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

    }
}
