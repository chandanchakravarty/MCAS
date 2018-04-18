using System;
using System.Data;
using System.Data.SqlClient;
using Cms.DataLayer;
using Cms.Model.Policy;

namespace Cms.BusinessLayer.BlCommon
{
    public class ClsNatureBusiness : Cms.BusinessLayer.BlCommon.ClsCommon
    {
        private bool boolTransactionRequired = true;
        public bool TransactionRequired
        {
            get
            {
                return boolTransactionRequired;
            }
            set
            {
                boolTransactionRequired = value;
            }
        }

        public int AddNatureBusiness(ClsNatureBusinessInfo objNatureBusinessInfo, string XmlFilePath)
        {
            
            DateTime RecordDate = DateTime.Now;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            if (this.boolTransactionRequired)
            {
                objNatureBusinessInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);
            }

            objDataWrapper.AddParameter("@CUSTOMER_ID", objNatureBusinessInfo.CUSTOMER_ID, SqlDbType.Int);
            objDataWrapper.AddParameter("@POLICY_ID", objNatureBusinessInfo.POLICY_ID);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", objNatureBusinessInfo.POLICY_VERSION_ID);
            //objDataWrapper.AddParameter("@BUSINESS_ID", objNatureBusinessInfo.BUSINESS_ID);
            objDataWrapper.AddParameter("@BUSINESS_NATURE", objNatureBusinessInfo.BUSINESS_NATURE);
            objDataWrapper.AddParameter("@PRIMARY_OPERATION", objNatureBusinessInfo.PRIMARY_OPERATION);
            objDataWrapper.AddParameter("@BUSINESS_START_DATE", objNatureBusinessInfo.BUSINESS_START_DATE);
            objDataWrapper.AddParameter("@OTHER_OPERATION", objNatureBusinessInfo.OTHER_OPERATION);
            objDataWrapper.AddParameter("@REPAIR_WORK", objNatureBusinessInfo.REPAIR_WORK);
            objDataWrapper.AddParameter("@PREMISES_WORK", objNatureBusinessInfo.PREMISES_WORK);
            objDataWrapper.AddParameter("@RETAIL_STORE", objNatureBusinessInfo.RETAIL_STORE);


            SqlParameter retParam = (SqlParameter)objDataWrapper.AddParameter("@BUSINESS_ID", SqlDbType.Int, ParameterDirection.Output);
            
            int returnResult = 0;

            try
            {
                //if transaction required
                if (TransactionLogRequired)
                {
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objNatureBusinessInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                    objTransactionInfo.POLICY_ID = objNatureBusinessInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = objNatureBusinessInfo.POLICY_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objNatureBusinessInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.RECORDED_BY = objNatureBusinessInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = "Record Has Been Added";
                    objTransactionInfo.CHANGE_XML = strTranXML;


                    returnResult = objDataWrapper.ExecuteNonQuery("INSERT_POL_NATURE_OF_BUSINESS", objTransactionInfo);
                    //returnResult = 1;
                }
                else//if no transaction required
                {
                    returnResult = objDataWrapper.ExecuteNonQuery("INSERT_POL_NATURE_OF_BUSINESS");

                }

                //int retVal = Convert.ToInt32(retParam.Value);

                int BUSINESS_ID;
                if (retParam.Value != null && retParam.Value.ToString() != "")
                {
                    BUSINESS_ID = int.Parse(retParam.Value.ToString());
                    objNatureBusinessInfo.BUSINESS_ID = BUSINESS_ID;
                }
                else
                {
                    BUSINESS_ID = -1;
                }

                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                return BUSINESS_ID;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }

        }

        public int UpdateNatureBusiness(ClsNatureBusinessInfo objNatureBusinessInfo, string XmlFilePath)
        {

            DateTime RecordDate = DateTime.Now;

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            if (this.boolTransactionRequired)
            {
                objNatureBusinessInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFilePath);
            }

            objDataWrapper.AddParameter("@CUSTOMER_ID", objNatureBusinessInfo.CUSTOMER_ID, SqlDbType.Int);
            objDataWrapper.AddParameter("@POLICY_ID", objNatureBusinessInfo.POLICY_ID);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", objNatureBusinessInfo.POLICY_VERSION_ID);
            objDataWrapper.AddParameter("@BUSN_ID", objNatureBusinessInfo.BUSINESS_ID);
            objDataWrapper.AddParameter("@BUSINESS_NATURE", objNatureBusinessInfo.BUSINESS_NATURE);
            objDataWrapper.AddParameter("@PRIMARY_OPERATION", objNatureBusinessInfo.PRIMARY_OPERATION);
            objDataWrapper.AddParameter("@BUSINESS_START_DATE", objNatureBusinessInfo.BUSINESS_START_DATE);
            objDataWrapper.AddParameter("@OTHER_OPERATION", objNatureBusinessInfo.OTHER_OPERATION);
            objDataWrapper.AddParameter("@REPAIR_WORK", objNatureBusinessInfo.REPAIR_WORK);
            objDataWrapper.AddParameter("@PREMISES_WORK", objNatureBusinessInfo.PREMISES_WORK);
            objDataWrapper.AddParameter("@RETAIL_STORE", objNatureBusinessInfo.RETAIL_STORE);


            //SqlParameter retParam = (SqlParameter)objDataWrapper.AddParameter("@BUSINESS_ID", SqlDbType.Int, ParameterDirection.Output);

            int returnResult = 0;

            try
            {
                //if transaction required
                if (TransactionLogRequired)
                {
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objNatureBusinessInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();

                    objTransactionInfo.POLICY_ID = objNatureBusinessInfo.POLICY_ID;
                    objTransactionInfo.POLICY_VER_TRACKING_ID = objNatureBusinessInfo.POLICY_VERSION_ID;
                    objTransactionInfo.CLIENT_ID = objNatureBusinessInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.RECORDED_BY = objNatureBusinessInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = "Record Has Been Added";
                    objTransactionInfo.CHANGE_XML = strTranXML;


                    returnResult = objDataWrapper.ExecuteNonQuery("UPDATE_POL_NATURE_OF_BUSINESS", objTransactionInfo);
                    //returnResult = 1;
                }
                else//if no transaction required
                {
                    returnResult = objDataWrapper.ExecuteNonQuery("UPDATE_POL_NATURE_OF_BUSINESS");

                }

                //int retVal = Convert.ToInt32(retParam.Value);

                //int BUSINESS_ID;
                //if (retParam.Value != null && retParam.Value.ToString() != "")
                //{
                //    BUSINESS_ID = int.Parse(retParam.Value.ToString());
                //    objNatureBusinessInfo.BUSINESS_ID = BUSINESS_ID;
                //}
                //else
                //{
                //    BUSINESS_ID = -1;
                //}

                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                return returnResult;
            }
            catch (Exception ex)
            {
                objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                throw (ex);
            }

        }

        public DataSet GetBusinessNature(string CUST_ID,string POL_ID, string POL_VERSION_ID)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@CUSTOMER_ID", CUST_ID);
            sqlParams[1] = new SqlParameter("@POLICY_ID", POL_ID);
            sqlParams[2] = new SqlParameter("@POLICY_VERSION_ID", POL_VERSION_ID);
            DataSet ds = null;
            try
            {
                ds = DataWrapper.ExecuteDataset(ConnStr, CommandType.StoredProcedure, "GET_POL_NATURE_OF_BUSINESS", sqlParams);
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (ds != null)
                    ds.Dispose();
            }
            //return ds;
        }
    }
}