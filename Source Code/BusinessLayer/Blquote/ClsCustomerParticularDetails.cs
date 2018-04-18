using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using Cms.BusinessLayer.BlQuote;
using Cms.Model.Quote;
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
using System.IO;
using Cms.BusinessLayer.BlApplication;
using Cms;
using System.Globalization;

namespace Cms.BusinessLayer.BlQuote
{
    public class ClsCustomerParticularDetails : clsquote
    {
        public DataSet FetchCustomerParticularDetail(int customerId, int Id)
        {
            string strStoredProc = "Proc_GetCustomerParticularInfo";
            DataSet dsQuote = null;

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerId, SqlDbType.Int);
                objDataWrapper.AddParameter("@ID", Id, SqlDbType.Int);
                dsQuote = objDataWrapper.ExecuteDataSet(strStoredProc);
                return dsQuote;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            
        }

        public int Add(ClsCustomerParticluarInfo objCustomerInfo, out int iCustomerID)
        {

            string strStoredProc = "";
            strStoredProc = "Proc_Insert_CustomerParticulars";

            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", objCustomerInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@QUOTE_ID", objCustomerInfo.QUOTE_ID);
                objDataWrapper.AddParameter("@CUSTOMER_CODE", objCustomerInfo.CUSTOMER_CODE);
                objDataWrapper.AddParameter("@CUSTOMER_TYPE", objCustomerInfo.CUSTOMER_TYPE);
                objDataWrapper.AddParameter("@CUSTOMER_FIRST_NAME", objCustomerInfo.CUSTOMER_FIRST_NAME);
                objDataWrapper.AddParameter("@CUSTOMER_MIDDLE_NAME", objCustomerInfo.CUSTOMER_MIDDLE_NAME);
                objDataWrapper.AddParameter("@CUSTOMER_LAST_NAME", objCustomerInfo.CUSTOMER_LAST_NAME);
                objDataWrapper.AddParameter("@DATE_OF_BIRTH", objCustomerInfo.DATE_OF_BIRTH);
                objDataWrapper.AddParameter("@GENDER", objCustomerInfo.GENDER);
                objDataWrapper.AddParameter("@NATIONALITY", objCustomerInfo.NATIONALITY);
                objDataWrapper.AddParameter("@IS_HOME_EMPLOYEE", objCustomerInfo.IS_HOME_EMPLOYEE);
                objDataWrapper.AddParameter("@CUSTOMER_OCCU", objCustomerInfo.CUSTOMER_OCCU);
                objDataWrapper.AddParameter("@DRIVER_EXP_YEAR", objCustomerInfo.DRIVER_EXP_YEAR);
                objDataWrapper.AddParameter("@ANY_CLAIM", objCustomerInfo.ANY_CLAIM);
                objDataWrapper.AddParameter("@EXIST_NCD_LESS_10", objCustomerInfo.EXIST_NCD_LESS_10);
                objDataWrapper.AddParameter("@EXISTING_NCD", objCustomerInfo.EXISTING_NCD);
                objDataWrapper.AddParameter("@DEMERIT_DISCOUNT", objCustomerInfo.DEMERIT_DISCOUNT);
                objDataWrapper.AddParameter("@CUSTOMER_AGENCY_ID", objCustomerInfo.CUSTOMER_AGENCY_ID);
                objDataWrapper.AddParameter("@IS_ACTIVE", objCustomerInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@CREATED_BY", objCustomerInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME", objCustomerInfo.CREATED_DATETIME);
                objDataWrapper.AddParameter("@MODIFIED_BY", objCustomerInfo.MODIFIED_BY);
                objDataWrapper.AddParameter("DATE_OF_QUOTATION", objCustomerInfo.DATE_OF_QUOTATION);
                //objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objCustomerInfo.LAST_UPDATED_DATETIME);


                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@ID", null, SqlDbType.Int, ParameterDirection.Output);

                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    objCustomerInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\GeneralInformation.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objCustomerInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.CLIENT_ID = objCustomerInfo.CUSTOMER_ID;
                    objTransactionInfo.APP_ID = 0;
                    objTransactionInfo.APP_VERSION_ID = 0;
                    objTransactionInfo.RECORDED_BY = objCustomerInfo.CREATED_BY;
                    objTransactionInfo.RECORD_DATE_TIME = objCustomerInfo.CREATED_DATETIME;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1448", ""); //"Rate has been added";
                    objTransactionInfo.CHANGE_XML = strTranXML;

                    //Executing the query
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }

                
                returnResult = iCustomerID = int.Parse(objSqlParameter.Value.ToString());
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

        public int Update(ClsCustomerParticluarInfo objCustomerInfo)
        {
            string strTranXML;
            int returnResult = 0;
            string strStoredProc = "";
            strStoredProc = "Proc_Update_CustomerParticulars";
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@ID", objCustomerInfo.ID);
                objDataWrapper.AddParameter("@CUSTOMER_ID", objCustomerInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@QUOTE_ID", objCustomerInfo.QUOTE_ID);
                objDataWrapper.AddParameter("@CUSTOMER_CODE", objCustomerInfo.CUSTOMER_CODE);
                objDataWrapper.AddParameter("@CUSTOMER_TYPE", objCustomerInfo.CUSTOMER_TYPE);
                objDataWrapper.AddParameter("@CUSTOMER_FIRST_NAME", objCustomerInfo.CUSTOMER_FIRST_NAME);
                objDataWrapper.AddParameter("@CUSTOMER_MIDDLE_NAME", objCustomerInfo.CUSTOMER_MIDDLE_NAME);
                objDataWrapper.AddParameter("@CUSTOMER_LAST_NAME", objCustomerInfo.CUSTOMER_LAST_NAME);
                objDataWrapper.AddParameter("@DATE_OF_BIRTH", objCustomerInfo.DATE_OF_BIRTH);
                objDataWrapper.AddParameter("@GENDER", objCustomerInfo.GENDER);
                objDataWrapper.AddParameter("@NATIONALITY", objCustomerInfo.NATIONALITY);
                objDataWrapper.AddParameter("@IS_HOME_EMPLOYEE", objCustomerInfo.IS_HOME_EMPLOYEE);
                objDataWrapper.AddParameter("@CUSTOMER_OCCU", objCustomerInfo.CUSTOMER_OCCU);
                objDataWrapper.AddParameter("@DRIVER_EXP_YEAR", objCustomerInfo.DRIVER_EXP_YEAR);
                objDataWrapper.AddParameter("@ANY_CLAIM", objCustomerInfo.ANY_CLAIM);
                objDataWrapper.AddParameter("@EXIST_NCD_LESS_10", objCustomerInfo.EXIST_NCD_LESS_10);
                objDataWrapper.AddParameter("@EXISTING_NCD", objCustomerInfo.EXISTING_NCD);
                objDataWrapper.AddParameter("@DEMERIT_DISCOUNT", objCustomerInfo.DEMERIT_DISCOUNT);
                objDataWrapper.AddParameter("@CUSTOMER_AGENCY_ID", objCustomerInfo.CUSTOMER_AGENCY_ID);
                objDataWrapper.AddParameter("@IS_ACTIVE", objCustomerInfo.IS_ACTIVE);
                //objDataWrapper.AddParameter("@CREATED_BY", objCustomerInfo.CREATED_BY);
                //objDataWrapper.AddParameter("@CREATED_DATETIME", objCustomerInfo.CREATED_DATETIME);
                objDataWrapper.AddParameter("@MODIFIED_BY", objCustomerInfo.MODIFIED_BY);
                objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objCustomerInfo.LAST_UPDATED_DATETIME);

                if (TransactionLogRequired)
                {
                    objCustomerInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"application\aspx\GeneralInformation.aspx.resx");
                    strTranXML = objBuilder.GetTransactionLogXML(objCustomerInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 3;
                    objTransactionInfo.RECORDED_BY = objCustomerInfo.MODIFIED_BY;
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
