/******************************************************************************************
<Author				: -  Amit k. Mishra
<Start Date			: -October 10th,2011
<End Date			: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
Purpose			        : - 
*******************************************************************************************/
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Maintenance;
using Cms.DataLayer;
using System.Web.UI.WebControls;
using Cms.Model.Maintenance.Reinsurance;

namespace Cms.BusinessLayer.BlCommon
{
   
    public class ClsCurrentCreditRating : Cms.BusinessLayer.BlCommon.ClsCommon, IDisposable
    {
       

        private const string MNT_CURRENT_RATING_LIST= "MNT_CURRENT_RATING_LIST";

        #region Private Instance Variables
        private bool boolTransactionLog;
        //private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateReinsurer";
        #endregion

      

        #region private Utility Functions
        #endregion

        public ClsCurrentCreditRating()
        {
            boolTransactionLog = base.TransactionLogRequired;
        }

        #region Add(Insert) functions
        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="ObjCurrentCreditRatingInfo">Model class object.</param>
        /// <returns>No of records effected.</returns>
        public int Add(ClsCurrentCreditRatingInfo ObjCurrentCreditRatingInfo)
        {
            //string		strStoredProc	=	"Proc_INSERT_CURRENTRATING";
            string strStoredProc = "Proc_INSERT_CURRENTRATING";//"MNT_CURRENT_RATING_LIST";

            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {

                //objDataWrapper.AddParameter("@RATING_ID", ObjCurrentCreditRatingInfo.RATING_ID);
                objDataWrapper.AddParameter("@AGENCY_ID", ObjCurrentCreditRatingInfo.AGENCY_ID);
                objDataWrapper.AddParameter("@COMPANY_ID", ObjCurrentCreditRatingInfo.COMPANY_ID);
                objDataWrapper.AddParameter("@COMPANY_TYPE", ObjCurrentCreditRatingInfo.COMPANY_TYPE);
                objDataWrapper.AddParameter("@RATING", ObjCurrentCreditRatingInfo.RATING);
                objDataWrapper.AddParameter("@EFFECTIVE_YEAR", ObjCurrentCreditRatingInfo.EFFECTIVE_YEAR);
                
                objDataWrapper.AddParameter("@CREATED_BY", ObjCurrentCreditRatingInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME", ObjCurrentCreditRatingInfo.CREATED_DATETIME);
                objDataWrapper.AddParameter("@MODIFIED_BY", ObjCurrentCreditRatingInfo.MODIFIED_BY);



                if (ObjCurrentCreditRatingInfo.LAST_UPDATED_DATETIME != DateTime.MinValue)
                    objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", ObjCurrentCreditRatingInfo.LAST_UPDATED_DATETIME);
                else
                    objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", System.DBNull.Value);

                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@RATING_ID", ObjCurrentCreditRatingInfo.RATING_ID, SqlDbType.Int, ParameterDirection.ReturnValue);

                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    ObjCurrentCreditRatingInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/AddRating.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(ObjCurrentCreditRatingInfo);

                    XmlDocument XMLDoc = new XmlDocument();
                    XMLDoc.LoadXml(strTranXML);
                    XmlElement root = XMLDoc.DocumentElement;
                    XmlNode node = root.SelectSingleNode("Map[@field='AGENCY_ID']");
                    if (node != null)
                    {
                        node.Attributes["NewValue"].InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.ComputeFederalID(node.Attributes["NewValue"].InnerText);
                        node.Attributes["OldValue"].InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.ComputeFederalID(node.Attributes["OldValue"].InnerText);
                    }
                    strTranXML = XMLDoc.InnerXml.ToString();

                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 229;
                    objTransactionInfo.RECORDED_BY = ObjCurrentCreditRatingInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = "";
                    objTransactionInfo.CHANGE_XML = strTranXML;
                    objTransactionInfo.CUSTOM_INFO = ObjCurrentCreditRatingInfo.CUSTOM_INFO;
                    //Executing the query
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);

                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                int intRATING_ID = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                if (intRATING_ID == -1)
                {

                   return -1;
                }
                else
                {
                    ObjCurrentCreditRatingInfo.RATING_ID = intRATING_ID;
                    return intRATING_ID;
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
        #endregion

        #region GetRatingLogXml
        public DataSet GetRatingLogXml(string RATING_LOG_ID)
        {
            //Customer
            string strStoredProc = "Proc_GetRatingById";
            DataSet dsRating = new DataSet();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            try
            {
                objDataWrapper.AddParameter("@RATING_ID", RATING_LOG_ID);
                dsRating = objDataWrapper.ExecuteDataSet(strStoredProc);
                //if (dsRating.Tables[0].Rows.Count != 0)
                //{
                    return dsRating;
                //}
            }
            catch (Exception exc)
            {
                throw (exc);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }
        #endregion

        #region get old Data
        public DataSet GetOldData(string Company_id)
        {
            string strStoredProc = "Proc_GetLatestRatingById";
            DataSet objDataSet = null;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@COMPANY_ID", Company_id);
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
       
        //#region Update method
        ///// <summary>
        ///// Update method that recieves Model object to save.
        ///// </summary>
        ///// <param name="objOldReinsurerInfo">Model object having old information</param>
        ///// <param name="ObjReinsurerInfo">Model object having new information(form control's value)</param>
        ///// <returns>No. of rows updated (1 or 0)</returns>
        //public int Update(ClsReinsurerInfo objOldReinsurerInfo, ClsReinsurerInfo ObjReinsurerInfo)
        //{
        //    string strStoredProc = "MNT_REIN_UPDATE_REINSURER";
        //    string strTranXML;
        //    int returnResult = 0;


        //    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
        //    DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
        //    try
        //    {

        //        objDataWrapper.AddParameter("@REIN_COMPANY_ID", ObjReinsurerInfo.REIN_COMAPANY_ID);
        //        objDataWrapper.AddParameter("@REIN_COMPANY_CODE", ObjReinsurerInfo.REIN_COMAPANY_CODE);
        //        objDataWrapper.AddParameter("@REIN_COMPANY_NAME", ObjReinsurerInfo.REIN_COMAPANY_NAME);
        //        objDataWrapper.AddParameter("@REIN_COMPANY_TYPE", ObjReinsurerInfo.REIN_COMPANY_TYPE);

        //        objDataWrapper.AddParameter("@REIN_COMPANY_ADD1", ObjReinsurerInfo.REIN_COMAPANY_ADD1);
        //        objDataWrapper.AddParameter("@REIN_COMPANY_ADD2", ObjReinsurerInfo.REIN_COMAPANY_ADD2);
        //        objDataWrapper.AddParameter("@REIN_COMPANY_CITY", ObjReinsurerInfo.REIN_COMAPANY_CITY);
        //        objDataWrapper.AddParameter("@REIN_COMPANY_COUNTRY", ObjReinsurerInfo.REIN_COMAPANY_COUNTRY);
        //        objDataWrapper.AddParameter("@REIN_COMPANY_STATE", ObjReinsurerInfo.REIN_COMAPANY_STATE);
        //        objDataWrapper.AddParameter("@REIN_COMPANY_ZIP", ObjReinsurerInfo.REIN_COMAPANY_ZIP);
        //        objDataWrapper.AddParameter("@REIN_COMPANY_PHONE", ObjReinsurerInfo.REIN_COMAPANY_PHONE);
        //        objDataWrapper.AddParameter("@REIN_COMPANY_EXT", ObjReinsurerInfo.REIN_COMAPANY_EXT);

        //        //objDataWrapper.AddParameter("@REIN_COMPANY_EXT",ObjReinsurerInfo.REIN_COMPANY_EXT);
        //        objDataWrapper.AddParameter("@REIN_COMPANY_FAX", ObjReinsurerInfo.REIN_COMAPANY_FAX);
        //        objDataWrapper.AddParameter("@REIN_COMPANY_SPEED_DIAL", ObjReinsurerInfo.REIN_COMPANY_SPEED_DIAL);

        //        objDataWrapper.AddParameter("@REIN_COMPANY_MOBILE", ObjReinsurerInfo.REIN_COMAPANY_MOBILE);
        //        objDataWrapper.AddParameter("@REIN_COMPANY_EMAIL", ObjReinsurerInfo.REIN_COMAPANY_EMAIL);
        //        objDataWrapper.AddParameter("@REIN_COMPANY_NOTE", ObjReinsurerInfo.REIN_COMAPANY_NOTE);
        //        objDataWrapper.AddParameter("@REIN_COMPANY_ACC_NUMBER", ObjReinsurerInfo.REIN_COMAPANY_ACC_NUMBER);

        //        objDataWrapper.AddParameter("@M_REIN_COMPANY_ADD_1", ObjReinsurerInfo.M_REIN_COMPANY_ADD_1);
        //        objDataWrapper.AddParameter("@M_REIN_COMPANY_ADD_2", ObjReinsurerInfo.M_RREIN_COMPANY_ADD_2);
        //        objDataWrapper.AddParameter("@M_REIN_COMPANY_CITY", ObjReinsurerInfo.M_REIN_COMPANY_CITY);
        //        objDataWrapper.AddParameter("@M_REIN_COMPANY_COUNTRY", ObjReinsurerInfo.M_REIN_COMPANY_COUNTRY);

        //        objDataWrapper.AddParameter("@M_REIN_COMPANY_STATE", ObjReinsurerInfo.M_REIN_COMPANY_STATE);
        //        objDataWrapper.AddParameter("@M_REIN_COMPANY_ZIP", ObjReinsurerInfo.M_REIN_COMPANY_ZIP);


        //        objDataWrapper.AddParameter("@M_REIN_COMPANY_PHONE", ObjReinsurerInfo.M_REIN_COMPANY_PHONE);
        //        objDataWrapper.AddParameter("@M_REIN_COMPANY_FAX", ObjReinsurerInfo.M_REIN_COMPANY_FAX);

        //        objDataWrapper.AddParameter("@M_REIN_COMPANY_EXT", ObjReinsurerInfo.M_REIN_COMPANY_EXT);

        //        //objDataWrapper.AddParameter("@M_REIN_COMPANY_EXT",ObjReinsurerInfo.M_REIN_COMPANY_EXT);
        //        objDataWrapper.AddParameter("@REIN_COMPANY_WEBSITE", ObjReinsurerInfo.REIN_COMPANY_WEBSITE);
        //        objDataWrapper.AddParameter("@REIN_COMPANY_IS_BROKER", ObjReinsurerInfo.REIN_COMPANY_IS_BROKER);
        //        objDataWrapper.AddParameter("@PRINCIPAL_CONTACT", ObjReinsurerInfo.PRINCIPAL_CONTACT);
        //        objDataWrapper.AddParameter("@OTHER_CONTACT", ObjReinsurerInfo.OTHER_CONTACT);
        //        objDataWrapper.AddParameter("@FEDERAL_ID", ObjReinsurerInfo.FEDERAL_ID);
        //        objDataWrapper.AddParameter("@ROUTING_NUMBER", ObjReinsurerInfo.ROUTING_NUMBER);
        //        objDataWrapper.AddParameter("@RISK_CLASSIFICATION", ObjReinsurerInfo.RISK_CLASSIFICATION);
        //        objDataWrapper.AddParameter("@AGENCY_CLASSIFICATION", ObjReinsurerInfo.AGENCY_CLASSIFICATION);


        //        if (ObjReinsurerInfo.TERMINATION_DATE != DateTime.MinValue)
        //            objDataWrapper.AddParameter("@TERMINATION_DATE", ObjReinsurerInfo.TERMINATION_DATE);
        //        else
        //            objDataWrapper.AddParameter("@TERMINATION_DATE", System.DBNull.Value);

        //        objDataWrapper.AddParameter("@TERMINATION_REASON", ObjReinsurerInfo.TERMINATION_REASON);
        //        objDataWrapper.AddParameter("@DOMICILED_STATE", ObjReinsurerInfo.DOMICILED_STATE);
        //        objDataWrapper.AddParameter("@NAIC_CODE", ObjReinsurerInfo.NAIC_CODE);
        //        objDataWrapper.AddParameter("@AM_BEST_RATING", ObjReinsurerInfo.AM_BEST_RATING);


        //        if (ObjReinsurerInfo.EFFECTIVE_DATE != DateTime.MinValue)
        //            objDataWrapper.AddParameter("@EFFECTIVE_DATE", ObjReinsurerInfo.EFFECTIVE_DATE);
        //        else
        //            objDataWrapper.AddParameter("@EFFECTIVE_DATE", System.DBNull.Value);

        //        objDataWrapper.AddParameter("@COMMENTS", ObjReinsurerInfo.COMMENTS);
        //        objDataWrapper.AddParameter("@MODIFIED_BY", ObjReinsurerInfo.MODIFIED_BY);
        //        objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", ObjReinsurerInfo.LAST_UPDATED_DATETIME);

        //        //added By chetna
        //        objDataWrapper.AddParameter("@SUSEP_NUM", ObjReinsurerInfo.SUSEP_NUM);
        //        objDataWrapper.AddParameter("@COM_TYPE", ObjReinsurerInfo.COM_TYPE);
        //        objDataWrapper.AddParameter("@DISTRICT", ObjReinsurerInfo.DISTRICT);
        //        objDataWrapper.AddParameter("@BANK_NUMBER", ObjReinsurerInfo.BANK_NUMBER);
        //        objDataWrapper.AddParameter("@BANK_BRANCH_NUMBER", ObjReinsurerInfo.BANK_BRANCH_NUMBER);
        //        objDataWrapper.AddParameter("@CARRIER_CNPJ", ObjReinsurerInfo.CARRIER_CNPJ);
        //        objDataWrapper.AddParameter("@BANK_ACCOUNT_TYPE", ObjReinsurerInfo.BANK_ACCOUNT_TYPE);
        //        objDataWrapper.AddParameter("@PAYMENT_METHOD", ObjReinsurerInfo.PAYMENT_METHOD);
        //        if (base.TransactionLogRequired)
        //        {
        //            //strTranXML = objBuilder.GetTransactionLogXML(objOldReinsurerInfo,ObjReinsurerInfo);

        //            ObjReinsurerInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/addReinsurer.aspx.resx");
        //            objBuilder.GetUpdateSQL(objOldReinsurerInfo, ObjReinsurerInfo, out strTranXML, "N");

        //            XmlDocument XMLDoc = new XmlDocument();
        //            XMLDoc.LoadXml(strTranXML);
        //            XmlElement root = XMLDoc.DocumentElement;
        //            XmlNode node = root.SelectSingleNode("Map[@field='FEDERAL_ID']");
        //            if (node != null)
        //            {
        //                node.Attributes["NewValue"].InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.ComputeFederalID(node.Attributes["NewValue"].InnerText);
        //                node.Attributes["OldValue"].InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.ComputeFederalID(node.Attributes["OldValue"].InnerText);
        //            }
                       
                   
        //            strTranXML = XMLDoc.InnerXml.ToString();

        //            Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
        //            objTransactionInfo.TRANS_TYPE_ID = 230;
        //            objTransactionInfo.RECORDED_BY = ObjReinsurerInfo.MODIFIED_BY;
        //            objTransactionInfo.TRANS_DESC = "";
        //            objTransactionInfo.CHANGE_XML = strTranXML;
        //            objTransactionInfo.CUSTOM_INFO = ObjReinsurerInfo.CUSTOM_INFO;
        //            returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);

        //        }
        //        else
        //        {
        //            returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
        //        }
        //        objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
        //        return returnResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //    finally
        //    {
        //        if (objDataWrapper != null)
        //        {
        //            objDataWrapper.Dispose();
        //        }
        //        if (objBuilder != null)
        //        {
        //            objBuilder = null;
        //        }
        //    }
        //}
        //#endregion
        //#region GET DATA FOR EDIT MODE
        //public DataSet GetDataForPageControls(string REIN_COMPANY_ID)
        //{
        //    string strStoredProc = "MNT_REIN_GETXML_REINSURER";
        //    DataSet objDataSet = null;
        //    try
        //    {
        //        DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
        //        objDataWrapper.AddParameter("@REIN_COMPANY_ID", REIN_COMPANY_ID);
        //        objDataWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID);

        //        objDataSet = objDataWrapper.ExecuteDataSet(strStoredProc);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //    finally
        //    {

        //    }
        //    return objDataSet;
        //}
        //#endregion
        //#region FETCHING DATA
        //public DataSet FetchData(int REIN_COMPANY_ID)
        //{
        //    string strStoredProc = "Proc_FetchReisurernfo";
        //    DataSet dsCount = null;

        //    try
        //    {
        //        DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
        //        objDataWrapper.AddParameter("@REIN_COMPANY_ID", REIN_COMPANY_ID, SqlDbType.Int);

        //        dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //    finally
        //    {

        //    }
        //    return dsCount;
        //}
        //#endregion
        //#region DEACTIVATE OR ACTIVATE CONTRACT TYPE

        //public int GetDeactivateActivate(string REIN_COMPANY_ID, string Status_Check)
        //{
        //    string strSql = "Proc_MNT_REIN_DEACTIVATE_ACTIVATE_REINSURER";
        //    DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
        //    objDataWrapper.AddParameter("@REIN_COMPANY_ID", REIN_COMPANY_ID);
        //    objDataWrapper.AddParameter("@STATUS_CHECK", Status_Check, System.Data.SqlDbType.NVarChar);

        //    //DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
        //    int returnResult = objDataWrapper.ExecuteNonQuery(strSql);
        //    return returnResult;
        //}

        //#endregion


    }
}
