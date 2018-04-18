using System;
using System.Xml;
using System.Data.SqlClient;
using Cms.DataLayer;
using System.Data;
using Cms.Model.Quote;
using System.Configuration;



namespace Cms.BusinessLayer.BlCommon
{
    /// <summary>
    /// Summary description for ClsQuoteDriverInformation.
    /// </summary>
    public class ClsQuoteDriverInformation : Cms.BusinessLayer.BlCommon.ClsCommon, IDisposable
    {
        //string strSysID = GetSystemId();

        public ClsQuoteDriverInformation()
        {
            //
            // TODO: Add constructor logic here 
            //
        }


        public int Add(ClsQuoteDriverInformationInfo ObjQuoteDriverInfInfo)
        {
            string strStoredProc = "Proc_InsertQQ_Driver_Information";
            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {
                objDataWrapper.AddParameter("@QUOTE_ID", ObjQuoteDriverInfInfo.QUOTE_ID);
                objDataWrapper.AddParameter("@DRIVER_NAME", ObjQuoteDriverInfInfo.DRIVER_NAME);

                if (ObjQuoteDriverInfInfo.DRIVER_CODE == "")
                    objDataWrapper.AddParameter("@DRIVER_CODE", null);
                else
                    objDataWrapper.AddParameter("@DRIVER_CODE", ObjQuoteDriverInfInfo.DRIVER_CODE);

                objDataWrapper.AddParameter("@DRIVER_GENDER", ObjQuoteDriverInfInfo.DRIVER_GENDER);

                if (ObjQuoteDriverInfInfo.DRIVER_DOB != DateTime.MinValue)
                    objDataWrapper.AddParameter("@DRIVER_DOB", ObjQuoteDriverInfInfo.DRIVER_DOB);
                else
                    objDataWrapper.AddParameter("@DRIVER_DOB", System.DBNull.Value);

                objDataWrapper.AddParameter("@DRIVER_TYPE", ObjQuoteDriverInfInfo.DRIVER_TYPE);

                if (ObjQuoteDriverInfInfo.DRIVER_LICENSE_NO == "")
                    objDataWrapper.AddParameter("@DRIVER_LICENSE_NO", null);
                else
                    objDataWrapper.AddParameter("@DRIVER_LICENSE_NO", ObjQuoteDriverInfInfo.DRIVER_LICENSE_NO);

                if (ObjQuoteDriverInfInfo.DRIVER_LICENSED_DATE != DateTime.MinValue)
                    objDataWrapper.AddParameter("@DRIVER_LICENSED_DATE", ObjQuoteDriverInfInfo.DRIVER_LICENSED_DATE);
                else
                    objDataWrapper.AddParameter("@DRIVER_LICENSED_DATE", System.DBNull.Value);

                objDataWrapper.AddParameter("@DRIVER_DRUG_VIOLATION", ObjQuoteDriverInfInfo.DRIVER_DRUG_VIOLATION);
                objDataWrapper.AddParameter("@CREATED_BY", ObjQuoteDriverInfInfo.CREATED_BY);

                if (ObjQuoteDriverInfInfo.CREATED_DATETIME != DateTime.MinValue)
                    objDataWrapper.AddParameter("@CREATED_DATETIME", ObjQuoteDriverInfInfo.CREATED_DATETIME);
                else
                    objDataWrapper.AddParameter("@CREATED_DATETIME", System.DBNull.Value);

                objDataWrapper.AddParameter("@IS_ACTIVE", ObjQuoteDriverInfInfo.IS_ACTIVE);

                int returnResult = 0;

                if (TransactionLogRequired)
                {
                    String XmlFullFilePath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "cmsweb/support/PageXml/s001/QuoteDriverInformation.xml";
                    ObjQuoteDriverInfInfo.TransactLabel = ClsCommon.GetTransactionLabelFromXml(XmlFullFilePath);
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(ObjQuoteDriverInfInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 1;
                    objTransactionInfo.RECORDED_BY = ObjQuoteDriverInfInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("2117", "");// "New QuoteDriverInformation is added";				

                    objTransactionInfo.CHANGE_XML = strTranXML;

                    objTransactionInfo.CUSTOM_INFO = ";DRIVER_CODE = " + ObjQuoteDriverInfInfo.DRIVER_CODE + ";Quote_ID = " + ObjQuoteDriverInfInfo.QUOTE_ID;

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

    }
}
