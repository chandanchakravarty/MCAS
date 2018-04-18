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
    public class ClsQuoteDetails : clsquote
    {
        public DataSet FetchCustomerPolicyDetail(int customerId, int PolicyId, int PolicyVersionID)
        {
            string strStoredProc = "Proc_GetCustomerPolicyDetail";
            DataSet dsPOL_CUST = null;

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerId, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_ID", PolicyId, SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_VERSION_ID", PolicyVersionID, SqlDbType.Int);
                dsPOL_CUST = objDataWrapper.ExecuteDataSet(strStoredProc);
                return dsPOL_CUST;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public DataSet FetchVehicleRatingDetail(int customerId, int PolicyId, int PolicyVersionID,int QuoteId)
        {
            string strStoredProc = "Proc_GetVehicleRatingDetail";
            DataSet dsPOL_CUST = null;

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerId, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyId, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID, SqlDbType.Int);
                objDataWrapper.AddParameter("@QUOTE_ID", QuoteId, SqlDbType.Int);
                dsPOL_CUST = objDataWrapper.ExecuteDataSet(strStoredProc);
                return dsPOL_CUST;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public DataSet FetchMotorQuoteDetail(int customerId, int PolicyId, int PolicyVersionID)
        {
            string strStoredProc = "Proc_Fetch_Motor_QQ_Details";
            DataSet dsPOL_CUST = null;

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerId, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyId, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID, SqlDbType.Int);               
                dsPOL_CUST = objDataWrapper.ExecuteDataSet(strStoredProc);
                return dsPOL_CUST;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public DataSet GetQuickQuoteVehicleXml(int customerId, int PolicyId, int PolicyVersionID)
        {
            string strStoredProc = "Proc_GetQuickQuoteVehicle_Xml";
            DataSet dsPOL_CUST = null;

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", customerId, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyId, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolicyVersionID, SqlDbType.Int);
                dsPOL_CUST = objDataWrapper.ExecuteDataSet(strStoredProc);
                return dsPOL_CUST;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public DataSet FetchDataForQQRqXml(int CUST_ID, int POL_ID, int POL_VERSION_ID)
        {
            string strStoredProc = "Proc_FetchQuickQuoteDataForRqXml";
            DataSet dsQQRqXml = null;

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CUST_ID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_ID", POL_ID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", POL_VERSION_ID, SqlDbType.Int);
                dsQQRqXml = objDataWrapper.ExecuteDataSet(strStoredProc);
                return dsQQRqXml;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //Created by Kuldeep to Get Premium when Application is sumitted without Quick Quote on 10-Jan-2012
        public DataSet FetchpQuoteDataForRXml(int CUST_ID, int POL_ID, int POL_VERSION_ID)
        {
            string strStoredProc = "Proc_FetchpQuoteDataForRXml";
            DataSet dsQQRqXml = null;

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CUST_ID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_ID", POL_ID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", POL_VERSION_ID, SqlDbType.Int);
                dsQQRqXml = objDataWrapper.ExecuteDataSet(strStoredProc);
                return dsQQRqXml;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        //Created by Kuldeep to Get Premium edited from billing info tab (21_feb_2012)

        public DataSet FetchBillingInfoPRemiumDetails(int CUST_ID, int POL_ID, int POL_VERSION_ID)
        {
            string strStoredProc = "PROC_GET_POL_BILLING_DETAILS";
            DataSet dsQQRqXml = null;

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CUST_ID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_ID", POL_ID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", POL_VERSION_ID, SqlDbType.Int);
                dsQQRqXml = objDataWrapper.ExecuteDataSet(strStoredProc);
                return dsQQRqXml;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //Created by Kuldeep to Get Marine Cargo Quote Details (22_Mar_2012)

        public DataSet FetchpQuoteDataForMarineXml(int CUST_ID, int POL_ID, int POL_VERSION_ID)
        {
            string strStoredProc = "Proc_FetchQuickQuoteForMarineXML";
            DataSet dsQQRqXml = null;

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID", CUST_ID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_ID", POL_ID, SqlDbType.Int);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", POL_VERSION_ID, SqlDbType.Int);
                dsQQRqXml = objDataWrapper.ExecuteDataSet(strStoredProc);
                return dsQQRqXml;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public int Add(ClsQuoteDetailsInfo objCustomerInfo)
        {

            string strStoredProc = "";
            strStoredProc = "Proc_Insert_MOTOR_QUOTE_DETAILS";

            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", objCustomerInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", objCustomerInfo.POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", objCustomerInfo.POLICY_VERSION_ID);
                objDataWrapper.AddParameter("@QUOTE_ID", objCustomerInfo.QUOTE_ID);
                objDataWrapper.AddParameter("@YEAR_OF_REG", objCustomerInfo.YEAR_OF_REG);
                objDataWrapper.AddParameter("@MAKE", objCustomerInfo.MAKE);
                objDataWrapper.AddParameter("@MODEL", objCustomerInfo.MODEL);
                objDataWrapper.AddParameter("@MODEL_TYPE", objCustomerInfo.MODEL_TYPE);
                objDataWrapper.AddParameter("@ENG_CAPACITY", objCustomerInfo.ENG_CAPACITY);
                objDataWrapper.AddParameter("@NO_OF_DRIVERS", objCustomerInfo.NO_OF_DRIVERS);
                objDataWrapper.AddParameter("@ANY_CLAIM", objCustomerInfo.ANY_CLAIM);
                objDataWrapper.AddParameter("@NO_OF_CLAIM", objCustomerInfo.NO_OF_CLAIM);
                objDataWrapper.AddParameter("@TOTAL_CLAIM_AMT", objCustomerInfo.TOTAL_CLAIM_AMT);
                objDataWrapper.AddParameter("@COVERAGE_TYPE", objCustomerInfo.COVERAGE_TYPE);
                objDataWrapper.AddParameter("@NO_CLAIM_DISCOUNT", objCustomerInfo.NO_CLAIM_DISCOUNT);
                objDataWrapper.AddParameter("@NAMED_DRIVER_AMT", objCustomerInfo.NAMED_DRIVER_AMT);
                objDataWrapper.AddParameter("@UNNAMED_DRIVER_AMT", objCustomerInfo.UNNAMED_DRIVER_AMT);

                objDataWrapper.AddParameter("@IS_ACTIVE", objCustomerInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@CREATED_BY", objCustomerInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME", objCustomerInfo.CREATED_DATETIME);
                objDataWrapper.AddParameter("@MODIFIED_BY", objCustomerInfo.MODIFIED_BY);
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

        public int Update(ClsQuoteDetailsInfo objCustomerInfo)
        {
            string strTranXML;
            int returnResult = 0;
            string strStoredProc = "";
            strStoredProc = "Proc_Update_MOTOR_QUOTE_DETAILS";
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", objCustomerInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", objCustomerInfo.POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", objCustomerInfo.POLICY_VERSION_ID);
                objDataWrapper.AddParameter("@QUOTE_ID", objCustomerInfo.QUOTE_ID);
                objDataWrapper.AddParameter("@YEAR_OF_REG", objCustomerInfo.YEAR_OF_REG);
                objDataWrapper.AddParameter("@MAKE", objCustomerInfo.MAKE);
                objDataWrapper.AddParameter("@MODEL", objCustomerInfo.MODEL);
                objDataWrapper.AddParameter("@MODEL_TYPE", objCustomerInfo.MODEL_TYPE);
                objDataWrapper.AddParameter("@ENG_CAPACITY", objCustomerInfo.ENG_CAPACITY);
                objDataWrapper.AddParameter("@NO_OF_DRIVERS", objCustomerInfo.NO_OF_DRIVERS);
                objDataWrapper.AddParameter("@ANY_CLAIM", objCustomerInfo.ANY_CLAIM);
                objDataWrapper.AddParameter("@NO_OF_CLAIM", objCustomerInfo.NO_OF_CLAIM);
                objDataWrapper.AddParameter("@TOTAL_CLAIM_AMT", objCustomerInfo.TOTAL_CLAIM_AMT);
                objDataWrapper.AddParameter("@COVERAGE_TYPE", objCustomerInfo.COVERAGE_TYPE);
                objDataWrapper.AddParameter("@NO_CLAIM_DISCOUNT", objCustomerInfo.NO_CLAIM_DISCOUNT);
                objDataWrapper.AddParameter("@NAMED_DRIVER_AMT", objCustomerInfo.NAMED_DRIVER_AMT);
                objDataWrapper.AddParameter("@UNNAMED_DRIVER_AMT", objCustomerInfo.UNNAMED_DRIVER_AMT);

                objDataWrapper.AddParameter("@IS_ACTIVE", objCustomerInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@CREATED_BY", objCustomerInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME", objCustomerInfo.CREATED_DATETIME);
                objDataWrapper.AddParameter("@MODIFIED_BY", objCustomerInfo.MODIFIED_BY);

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

        public int UpdateRateXML(int custID,int PolID,int PolVersionID,int QuoteID,string RqXML,string RsXML)
        {            
            int returnResult = 0;
            string strStoredProc = "";
            strStoredProc = "Proc_Update_RATEXML";
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", custID);
                objDataWrapper.AddParameter("@POLICY_ID", PolID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolVersionID);
                objDataWrapper.AddParameter("@QUOTE_ID", QuoteID);
                objDataWrapper.AddParameter("@REQUEST_XML", RqXML);
                objDataWrapper.AddParameter("@RESPONSE_XML", RsXML);
                
                
                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
               
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

        public int UpdateQQPremium(int custID, int PolID, int PolVersionID, string strPremium)
        {
            int returnResult = 0;
            string strStoredProc = "";
            strStoredProc = "Proc_Update_QuickQuotePremium";
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", custID);
                objDataWrapper.AddParameter("@POLICY_ID", PolID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolVersionID);
                objDataWrapper.AddParameter("@PREMIUM", double.Parse(strPremium));
                


                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

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

        public int UpdateQuoteDetailsPremium(int custID, int PolID, int PolVersionID, int QuoteID, string BASE_PREMIUM, string DEMERIT_DISC_AMT, string GST_AMOUNT, string FINAL_PREMIUM)
        {
            int returnResult = 0;
            string strStoredProc = "";
            strStoredProc = "Proc_Update_QuoteDetails_Premium";
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", custID);
                objDataWrapper.AddParameter("@POLICY_ID", PolID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolVersionID);
                objDataWrapper.AddParameter("@QUOTE_ID", QuoteID);
                objDataWrapper.AddParameter("@BASE_PREMIUM", BASE_PREMIUM);
                objDataWrapper.AddParameter("@DEMERIT_DISC_AMT", DEMERIT_DISC_AMT);
                objDataWrapper.AddParameter("@GST_AMOUNT", GST_AMOUNT);
                objDataWrapper.AddParameter("@FINAL_PREMIUM", FINAL_PREMIUM);


                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

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


        public int UpdateQuoteDetailsPremiumForMarine(int custID, int PolID, int PolVersionID, int QuoteID, double Sum_insured, double Calculated_Premium)
        {
            int returnResult = 0;
            string strStoredProc = "";
            strStoredProc = "Proc_Update_QuoteDetails_PremiumForMarine ";
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", custID);
                objDataWrapper.AddParameter("@POLICY_ID", PolID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolVersionID);
                objDataWrapper.AddParameter("@QUOTE_ID", QuoteID);
                objDataWrapper.AddParameter("@SUM_INSURED ", Sum_insured);
                objDataWrapper.AddParameter("@CALCULATED_PREMIUM", Calculated_Premium);



                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

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
        //created by kuldeep to get premium if application is straight away without Quick Quote on Sunday 15/01/2012
        public int UpdateQuoteDetailsPremiumNonQQ(int custID, int PolID, int PolVersionID, int QuoteID, string BASE_PREMIUM, string DEMERIT_DISC_AMT, string GST_AMOUNT, string FINAL_PREMIUM)
        {
            int returnResult = 0;
            string strStoredProc = "";
            strStoredProc = "Proc_Update_QuoteDetails_Premium_NONQQ";
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", custID);
                objDataWrapper.AddParameter("@POLICY_ID", PolID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolVersionID);
                objDataWrapper.AddParameter("@VEHICLE_ID", QuoteID);
                objDataWrapper.AddParameter("@BASE_PREMIUM", BASE_PREMIUM);
                objDataWrapper.AddParameter("@DEMERIT_DISC_AMT", DEMERIT_DISC_AMT);
                objDataWrapper.AddParameter("@GST_AMOUNT", GST_AMOUNT);
                objDataWrapper.AddParameter("@FINAL_PREMIUM", FINAL_PREMIUM);


                returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

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
        public string GetQuickQuoteXML(int customerID,int policyID,int policyVersionID)
        {
            StringBuilder returnString = new StringBuilder();
            string strReturnXML = "";
            DataSet dsTempXML;
            string strpolicy;
            string strOutXml = "";

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                returnString.Remove(0, returnString.Length);
                returnString.Append("<QUICKQUOTE><POLICY>");

                // Get the app details
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@policyID", policyID);
                objDataWrapper.AddParameter("@policyVERSIONID", policyVersionID);
                dsTempXML = objDataWrapper.ExecuteDataSet("Proc_GetQQVehicleRatingInfo");//temporary change on 15-jan-2012 in place of Proc_GetQuoteVehicleRatingInfo to get quote details and success
                objDataWrapper.ClearParameteres();
                //strReturnXML = dsTempXML.GetXml(); --Commented on 18 April 2008
                strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]); //Added on 10 April 2008 : praveen
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");
                returnString.Append(strReturnXML);

                strpolicy = strReturnXML;

                /*----------Primary Applicant Info--------*/
                returnString.Append("<PRIMARYAPPLICANT>");
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@POLICYID", policyID);
                objDataWrapper.AddParameter("@POLICYVERSIONID", policyVersionID);
                //objDataWrapper.AddParameter("@USERID",System.Web.HttpContext.Current.Session["userId"].ToString());
                //objDataWrapper.AddParameter("@CALLEDFROM", "POL");
                dsTempXML = objDataWrapper.ExecuteDataSet("Proc_GetPrimaryApplicantInfoQQ");
                objDataWrapper.ClearParameteres();
                //strReturnXML = dsTempXML.GetXml();
                strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]); //Added on 10 April 2008 : praveen
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                returnString.Append(strReturnXML);
                returnString.Append("</PRIMARYAPPLICANT>");
                /*----------Primary Applicant Info--------*/

                returnString.Append("</POLICY>");
                returnString.Append("</QUICKQUOTE>");

                returnString = returnString.Replace(",", "");
                returnString = returnString.Replace("\r\n", "");

                XmlDocument returnXMLDoc = new XmlDocument();
                returnXMLDoc.LoadXml(returnString.ToString());

                strOutXml = returnXMLDoc.OuterXml;

            }
            catch (Exception exc)
            {
                throw (exc);
            }
            return strOutXml.ToString();
        }

        public string GetQuickQuoteXMLForMarine(int customerID, int policyID, int policyVersionID)
        {
            StringBuilder returnString = new StringBuilder();
            string strReturnXML = "";
            DataSet dsTempXML;
            string strpolicy;
            string strOutXml = "";

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                returnString.Remove(0, returnString.Length);
                returnString.Append("<QUICKQUOTE><POLICY>");

                // Get the app details
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@policyID", policyID);
                objDataWrapper.AddParameter("@policyVERSIONID", policyVersionID);
                dsTempXML = objDataWrapper.ExecuteDataSet("Proc_GetQQMarineRatingInfo");//temporary change on 15-jan-2012 in place of Proc_GetQuoteVehicleRatingInfo to get quote details and success
                objDataWrapper.ClearParameteres();
                //strReturnXML = dsTempXML.GetXml(); --Commented on 18 April 2008
                strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]); //Added on 10 April 2008 : praveen
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");
                returnString.Append(strReturnXML);

                strpolicy = strReturnXML;

                /*----------Primary Applicant Info--------*/
                returnString.Append("<PRIMARYAPPLICANT>");
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@POLICYID", policyID);
                objDataWrapper.AddParameter("@POLICYVERSIONID", policyVersionID);
                //objDataWrapper.AddParameter("@USERID",System.Web.HttpContext.Current.Session["userId"].ToString());
                //objDataWrapper.AddParameter("@CALLEDFROM", "POL");
                dsTempXML = objDataWrapper.ExecuteDataSet("Proc_GetPrimaryApplicantInfoQQ");
                objDataWrapper.ClearParameteres();
                //strReturnXML = dsTempXML.GetXml();
                strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]); //Added on 10 April 2008 : praveen
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                returnString.Append(strReturnXML);
                returnString.Append("</PRIMARYAPPLICANT>");
                /*----------Primary Applicant Info--------*/

                returnString.Append("</POLICY>");
                returnString.Append("</QUICKQUOTE>");

                returnString = returnString.Replace(",", "");
                returnString = returnString.Replace("\r\n", "");

                XmlDocument returnXMLDoc = new XmlDocument();
                returnXMLDoc.LoadXml(returnString.ToString());

                strOutXml = returnXMLDoc.OuterXml;

            }
            catch (Exception exc)
            {
                throw (exc);
            }
            return strOutXml.ToString();
        }
        //cREATED BY KULDEEP TO GET PREMIUM IF APPLICATION IS GENERATED WITHOUT QQ BY KULDEEP ON 15/1/2012


        public string GetQuoteXMLNonQQ(int customerID, int policyID, int policyVersionID)
        {
            StringBuilder returnString = new StringBuilder();
            string strReturnXML = "";
            DataSet dsTempXML;
            string strpolicy;
            string strOutXml = "";

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
                returnString.Remove(0, returnString.Length);
                returnString.Append("<QUICKQUOTE><POLICY>");

                // Get the app details
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@policyID", policyID);
                objDataWrapper.AddParameter("@policyVERSIONID", policyVersionID);
                dsTempXML = objDataWrapper.ExecuteDataSet("Proc_GetQuoteVehicleRatingInfo");//temporary change on 15-jan-2012 in place of Proc_GetQuoteVehicleRatingInfo to get quote details and success
                objDataWrapper.ClearParameteres();
                //strReturnXML = dsTempXML.GetXml(); --Commented on 18 April 2008
                strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]); //Added on 10 April 2008 : praveen
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");
                returnString.Append(strReturnXML);

                strpolicy = strReturnXML;

                /*----------Primary Applicant Info--------*/
                returnString.Append("<PRIMARYAPPLICANT>");
                objDataWrapper.AddParameter("@CUSTOMERID", customerID);
                objDataWrapper.AddParameter("@POLICYID", policyID);
                objDataWrapper.AddParameter("@POLICYVERSIONID", policyVersionID);
                //objDataWrapper.AddParameter("@USERID",System.Web.HttpContext.Current.Session["userId"].ToString());
                //objDataWrapper.AddParameter("@CALLEDFROM", "POL");
                dsTempXML = objDataWrapper.ExecuteDataSet("Proc_GetPrimaryApplicantInfoQQ");
                objDataWrapper.ClearParameteres();
                //strReturnXML = dsTempXML.GetXml();
                strReturnXML = ClsCommon.GetXML(dsTempXML.Tables[0]); //Added on 10 April 2008 : praveen
                strReturnXML = strReturnXML.Replace("<NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("</NewDataSet>", "");
                strReturnXML = strReturnXML.Replace("<Table>", "");
                strReturnXML = strReturnXML.Replace("</Table>", "");
                strReturnXML = strReturnXML.Replace("'", "H673GSUYD7G3J73UDH");
                returnString.Append(strReturnXML);
                returnString.Append("</PRIMARYAPPLICANT>");
                /*----------Primary Applicant Info--------*/

                returnString.Append("</POLICY>");
                returnString.Append("</QUICKQUOTE>");

                returnString = returnString.Replace(",", "");
                returnString = returnString.Replace("\r\n", "");

                XmlDocument returnXMLDoc = new XmlDocument();
                returnXMLDoc.LoadXml(returnString.ToString());

                strOutXml = returnXMLDoc.OuterXml;

            }
            catch (Exception exc)
            {
                throw (exc);
            }
            return strOutXml.ToString();
        }
        public int Update_Motor_QQ_Detail(ClsQuoteDetailsInfo objQuoteDetailsInfo, ClsCustomerParticluarInfo objCustomerInfo)
        {
            string strTranXML;
            int returnResult = 0;
            string strStoredProc = "";
            strStoredProc = "Proc_Update_Motor_QQ_Details";
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID", objQuoteDetailsInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", objQuoteDetailsInfo.POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", objQuoteDetailsInfo.POLICY_VERSION_ID);

                objDataWrapper.AddParameter("@CUSTOMER_ADDRESS1", objCustomerInfo.CUSTOMER_ADDRESS1);
                objDataWrapper.AddParameter("@CUSTOMER_ADDRESS2", objCustomerInfo.CUSTOMER_ADDRESS2);
                objDataWrapper.AddParameter("@CUSTOMER_CITY", objCustomerInfo.CUSTOMER_CITY);
                objDataWrapper.AddParameter("@CUSTOMER_STATE", objCustomerInfo.CUSTOMER_STATE);
                objDataWrapper.AddParameter("@CUSTOMER_ZIP", objCustomerInfo.CUSTOMER_ZIP);
                objDataWrapper.AddParameter("@MARITAL_STATUS", objCustomerInfo.MARITAL_STATUS);
                objDataWrapper.AddParameter("@CUSTOMER_CONTACT_NO", objCustomerInfo.CUSTOMER_CONTACT_NO);
                objDataWrapper.AddParameter("@PASSPORT_NO", objCustomerInfo.PASSPORT_NO);
                objDataWrapper.AddParameter("@EXISTING_INSURER", objCustomerInfo.EXISTING_INSURER);
                objDataWrapper.AddParameter("@EXISTING_POL_NUM", objCustomerInfo.EXISTING_POL_NUM);
                objDataWrapper.AddParameter("@EXIST_POL_EXP_DATE", objCustomerInfo.EXIST_POL_EXP_DATE);
                objDataWrapper.AddParameter("@VEHICLE_NO", objCustomerInfo.VEHICLE_NO);

                objDataWrapper.AddParameter("@IS_NEW", objQuoteDetailsInfo.IS_NEW);
                objDataWrapper.AddParameter("@REGISTRATION_NO", objQuoteDetailsInfo.REGISTRATION_NO);
                objDataWrapper.AddParameter("@COVER_NOTE_NO", objQuoteDetailsInfo.COVER_NOTE_NO);
                objDataWrapper.AddParameter("@DATE_LTA_REGISTRATION", objQuoteDetailsInfo.DATE_LTA_REGISTRATION);
                objDataWrapper.AddParameter("@ENGINE_NO", objQuoteDetailsInfo.ENGINE_NO);
                objDataWrapper.AddParameter("@CHASSIS_NO", objQuoteDetailsInfo.CHASSIS_NO);
                objDataWrapper.AddParameter("@IS_UNDER_HIRE", objQuoteDetailsInfo.IS_UNDER_HIRE);
                objDataWrapper.AddParameter("@FINANCE_COMP_NAME", objQuoteDetailsInfo.FINANCE_COMP_NAME);
                objDataWrapper.AddParameter("@IS_DEMERIT_POINT", objQuoteDetailsInfo.IS_DEMERIT_POINT);
                objDataWrapper.AddParameter("@DEMERIT_DESC", objQuoteDetailsInfo.DEMERIT_DESC);
                objDataWrapper.AddParameter("@IS_REJECTED", objQuoteDetailsInfo.IS_REJECTED);
                objDataWrapper.AddParameter("@REJECTED_DESC", objQuoteDetailsInfo.REJECTED_DESC);
                objDataWrapper.AddParameter("@IS_DISEASE", objQuoteDetailsInfo.IS_DISEASE);
                objDataWrapper.AddParameter("@DISEASE_DESC", objQuoteDetailsInfo.DISEASE_DESC);

                

               

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
        //Created by Kuldeep on 12-03-2012 as per TFS 2912
        public DataSet GetInsuranceComapniesDataset()
        {
            string strStoredProc = "PROC_GET_INSURANCE_COMAPANY_LIST";//WILL FETCH LIST OF COMPANIES WHERE TYPE IS INSURANCE

            DataSet dsAccounts = new DataSet();

            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
           
            dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc);

            return dsAccounts;

        }
    }
}
