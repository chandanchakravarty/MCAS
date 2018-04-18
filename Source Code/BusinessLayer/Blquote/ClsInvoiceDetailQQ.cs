
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
    public class ClsInvoiceDetailQQ : clsquote
    {
        //public DataSet FetchCustomerParticularDetail(int customerId, int Id)
        public DataSet FetchInvoiceDetail(int Id)
        {
            string strStoredProc = "Proc_Get_QQ_INVOICE_PARTICULAR_MARINE";
            DataSet dsQuote = null;

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@ID", Id, SqlDbType.Int);
                dsQuote = objDataWrapper.ExecuteDataSet(strStoredProc);
                return dsQuote;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            
        }
        public int AddQQCustomer(Cms.Model.Quote.ClsInvoiceDetailQQInfo objInvoiceInfo, out int iCustomerID)
        {
            
             string strStoredProc = "";
            strStoredProc = "Proc_InsertMarineQQCustomerGenInfo";

            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {
                
                
                objDataWrapper.AddParameter("@CUSTOMER_TYPE", objInvoiceInfo.CUSTOMER_TYPE);
                objDataWrapper.AddParameter("@CUSTOMER_FIRST_NAME", objInvoiceInfo.COMPANY_NAME);
                objDataWrapper.AddParameter("@CUSTOMER_BUSINESS_TYPE", objInvoiceInfo.BUSINESS_TYPE);
               
                objDataWrapper.AddParameter("@IS_ACTIVE", objInvoiceInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@CREATED_BY", objInvoiceInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME",Convert.ToDateTime( DateTime.Now.ToShortDateString()));
                


                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@CUSTOMER_ID", null, SqlDbType.Int, ParameterDirection.Output);
                objInvoiceInfo.ID = Convert.ToInt32(objSqlParameter.Value);
                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    objInvoiceInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"CmsWeb\aspx\InvoiceDetailQQ.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = "";
                    if (objInvoiceInfo.TransactLabel != "")
                    {
                        strTranXML = objBuilder.GetTransactionLogXML(objInvoiceInfo);
                    }

                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.CLIENT_ID = objInvoiceInfo.CUSTOMER_ID;
                    objTransactionInfo.APP_ID = 0;
                    objTransactionInfo.APP_VERSION_ID = 0;
                    objTransactionInfo.RECORDED_BY = objInvoiceInfo.CREATED_BY;
                    objTransactionInfo.RECORD_DATE_TIME = objInvoiceInfo.CREATED_DATETIME;
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
        public int Add(Cms.Model.Quote.ClsInvoiceDetailQQInfo objInvoiceInfo, out int iCustomerID)
        {

            string strStoredProc = "";
            strStoredProc = "PROC_INSERT_QQ_INVOICE_PARTICULAR_MARINE";

            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
      
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", objInvoiceInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@QUOTE_ID", objInvoiceInfo.QUOTE_ID);
                objDataWrapper.AddParameter("@CUSTOMER_TYPE", objInvoiceInfo.CUSTOMER_TYPE);
                objDataWrapper.AddParameter("@COMPANY_NAME", objInvoiceInfo.COMPANY_NAME);
                objDataWrapper.AddParameter("@BUSINESS_TYPE", objInvoiceInfo.BUSINESS_TYPE);
                objDataWrapper.AddParameter("@OPEN_COVER_NO", objInvoiceInfo.OPEN_COVER_NO);
                objDataWrapper.AddParameter("@INVOICE_AMOUNT", objInvoiceInfo.INVOICE_AMOUNT);
                objDataWrapper.AddParameter("@INVOICE_TYPE", objInvoiceInfo.INVOICE_TYPE);
                objDataWrapper.AddParameter("@CURRENCY_TYPE", objInvoiceInfo.CURRENCY_TYPE);
                objDataWrapper.AddParameter("@BILLING_CURRENCY", objInvoiceInfo.BILLING_CURRENCY);
                objDataWrapper.AddParameter("@MARK_UP_RATE_PERC", objInvoiceInfo.MARK_UP_RATE_PERC);
                objDataWrapper.AddParameter("@IS_ACTIVE", objInvoiceInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@CREATED_BY", objInvoiceInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME", objInvoiceInfo.CREATED_DATETIME);
                objDataWrapper.AddParameter("@DATE_OF_QUOTATION", objInvoiceInfo.DATE_OF_QUOTATION);
                objDataWrapper.AddParameter("@POLICY_ID", objInvoiceInfo.POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", objInvoiceInfo.POLICY_VERSION_ID);


                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@ID", null, SqlDbType.Int, ParameterDirection.Output);
                objInvoiceInfo.ID = Convert.ToInt32(objSqlParameter.Value);
                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    objInvoiceInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"CmsWeb\aspx\InvoiceDetailQQ.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = "";
                    if (objInvoiceInfo.TransactLabel != "")
                    {
                        strTranXML = objBuilder.GetTransactionLogXML(objInvoiceInfo);
                    }
                   
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.CLIENT_ID = objInvoiceInfo.CUSTOMER_ID;
                    objTransactionInfo.APP_ID = 0;
                    objTransactionInfo.APP_VERSION_ID = 0;
                    objTransactionInfo.RECORDED_BY = objInvoiceInfo.CREATED_BY;
                    objTransactionInfo.RECORD_DATE_TIME = objInvoiceInfo.CREATED_DATETIME;
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

        public int Update(ClsInvoiceDetailQQInfo objInvoiceInfo)
        {
            string strTranXML;
            int returnResult = 0;
            string strStoredProc = "";
            strStoredProc = "Proc_Update_QQ_INVOICE_PARTICULAR_MARINE";
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@ID", objInvoiceInfo.ID);
                objDataWrapper.AddParameter("@CUSTOMER_ID", objInvoiceInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@QUOTE_ID", objInvoiceInfo.QUOTE_ID);
                objDataWrapper.AddParameter("@CUSTOMER_TYPE", objInvoiceInfo.CUSTOMER_TYPE);
                objDataWrapper.AddParameter("@COMPANY_NAME", objInvoiceInfo.COMPANY_NAME);
                objDataWrapper.AddParameter("@BUSINESS_TYPE", objInvoiceInfo.BUSINESS_TYPE);
                objDataWrapper.AddParameter("@OPEN_COVER_NO", objInvoiceInfo.OPEN_COVER_NO);
                objDataWrapper.AddParameter("@INVOICE_AMOUNT", objInvoiceInfo.INVOICE_AMOUNT);
                objDataWrapper.AddParameter("@INVOICE_TYPE", objInvoiceInfo.INVOICE_TYPE);
                objDataWrapper.AddParameter("@CURRENCY_TYPE", objInvoiceInfo.CURRENCY_TYPE);
                objDataWrapper.AddParameter("@BILLING_CURRENCY", objInvoiceInfo.BILLING_CURRENCY);
                objDataWrapper.AddParameter("@MARK_UP_RATE_PERC", objInvoiceInfo.MARK_UP_RATE_PERC);
                objDataWrapper.AddParameter("@IS_ACTIVE", objInvoiceInfo.IS_ACTIVE);
           
                //objDataWrapper.AddParameter("@CREATED_BY", objCustomerInfo.CREATED_BY);
                //objDataWrapper.AddParameter("@CREATED_DATETIME", objCustomerInfo.CREATED_DATETIME);
                objDataWrapper.AddParameter("@MODIFIED_BY", objInvoiceInfo.MODIFIED_BY);
                objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objInvoiceInfo.LAST_UPDATED_DATETIME);

                if (TransactionLogRequired)
                {
                    objInvoiceInfo.TransactLabel = ClsCommon.MapTransactionLabel(@"CmsWeb\aspx\InvoiceDetailQQ.aspx.resx");
                    strTranXML = "";
                    if (objInvoiceInfo.TransactLabel != "")
                    {
                        strTranXML = objBuilder.GetTransactionLogXML(objInvoiceInfo);
                    }
                   
                    
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 3;
                    objTransactionInfo.RECORDED_BY = objInvoiceInfo.MODIFIED_BY;
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

        public DataSet FetchInvoiceParticularDetail(int customerId, int Id)
        {
            string strStoredProc = "Proc_GetInvoiceParticularInfo";
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
    }
}
