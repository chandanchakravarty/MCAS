/******************************************************************************************
<Author				    : -  Priya Arora
<Start Date				: -	 Jan 07,2006
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
Purpose				     : - 
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
    /// <summary>
    /// Summary description for ClsReinsurer.
    /// </summary>
    public class ClsReinsurer : Cms.BusinessLayer.BlCommon.ClsCommon, IDisposable
    {
        //private const	string		MNT_Reinsurer_LIST			=	"MNT_Reinsurer_LIST";

        private const string MNT_REIN_COMPANY_LIST = "MNT_REIN_COMPANY_LIST";

        #region Private Instance Variables
        private bool boolTransactionLog;
        //private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateReinsurer";
        #endregion

        #region Public Properties
        /*
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
		*/
        #endregion

        #region private Utility Functions
        #endregion

        public ClsReinsurer()
        {
            boolTransactionLog = base.TransactionLogRequired;
            base.strActivateDeactivateProcedure = "Proc_MNT_REIN_DEACTIVATE_ACTIVATE_REINSURER";
        }

        #region Add(Insert) functions
        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="ObjReinsurerInfo">Model class object.</param>
        /// <returns>No of records effected.</returns>
        public int Add(ClsReinsurerInfo ObjReinsurerInfo)
        {
            //string		strStoredProc	=	"Proc_InsertReinsurer";
            string strStoredProc = "Proc_MNT_REIN_INSERT_REINSURER";//"MNT_REIN_INSERT_REINSURER";//changed by abhinav

            DateTime RecordDate = DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

            try
            {

                objDataWrapper.AddParameter("@REIN_COMPANY_CODE", ObjReinsurerInfo.REIN_COMAPANY_CODE);
                objDataWrapper.AddParameter("@REIN_COMPANY_NAME", ObjReinsurerInfo.REIN_COMAPANY_NAME);
                objDataWrapper.AddParameter("@REIN_COMPANY_TYPE", ObjReinsurerInfo.REIN_COMPANY_TYPE);

                objDataWrapper.AddParameter("@REIN_COMPANY_ADD1", ObjReinsurerInfo.REIN_COMAPANY_ADD1);
                objDataWrapper.AddParameter("@REIN_COMPANY_ADD2", ObjReinsurerInfo.REIN_COMAPANY_ADD2);
                objDataWrapper.AddParameter("@REIN_COMPANY_CITY", ObjReinsurerInfo.REIN_COMAPANY_CITY);
                objDataWrapper.AddParameter("@REIN_COMPANY_COUNTRY", ObjReinsurerInfo.REIN_COMAPANY_COUNTRY);
                objDataWrapper.AddParameter("@REIN_COMPANY_STATE", ObjReinsurerInfo.REIN_COMAPANY_STATE);
                objDataWrapper.AddParameter("@REIN_COMPANY_ZIP", ObjReinsurerInfo.REIN_COMAPANY_ZIP);
                objDataWrapper.AddParameter("@REIN_COMPANY_PHONE", ObjReinsurerInfo.REIN_COMAPANY_PHONE);
                objDataWrapper.AddParameter("@REIN_COMPANY_EXT", ObjReinsurerInfo.REIN_COMAPANY_EXT);

                //objDataWrapper.AddParameter("@REIN_COMPANY_EXT",ObjReinsurerInfo.REIN_COMPANY_EXT);
                objDataWrapper.AddParameter("@REIN_COMPANY_FAX", ObjReinsurerInfo.REIN_COMAPANY_FAX);
                objDataWrapper.AddParameter("@REIN_COMPANY_SPEED_DIAL", ObjReinsurerInfo.REIN_COMPANY_SPEED_DIAL);

                objDataWrapper.AddParameter("@REIN_COMPANY_MOBILE", ObjReinsurerInfo.REIN_COMAPANY_MOBILE);
                objDataWrapper.AddParameter("@REIN_COMPANY_EMAIL", ObjReinsurerInfo.REIN_COMAPANY_EMAIL);
                objDataWrapper.AddParameter("@REIN_COMPANY_NOTE", ObjReinsurerInfo.REIN_COMAPANY_NOTE);
                objDataWrapper.AddParameter("@REIN_COMPANY_ACC_NUMBER", ObjReinsurerInfo.REIN_COMAPANY_ACC_NUMBER);

                objDataWrapper.AddParameter("@M_REIN_COMPANY_ADD_1", ObjReinsurerInfo.M_REIN_COMPANY_ADD_1);
                objDataWrapper.AddParameter("@M_REIN_COMPANY_ADD_2", ObjReinsurerInfo.M_RREIN_COMPANY_ADD_2);
                objDataWrapper.AddParameter("@M_REIN_COMPANY_CITY", ObjReinsurerInfo.M_REIN_COMPANY_CITY);
                objDataWrapper.AddParameter("@M_REIN_COMPANY_COUNTRY", ObjReinsurerInfo.M_REIN_COMPANY_COUNTRY);

                objDataWrapper.AddParameter("@M_REIN_COMPANY_STATE", ObjReinsurerInfo.M_REIN_COMPANY_STATE);
                objDataWrapper.AddParameter("@M_REIN_COMPANY_ZIP", ObjReinsurerInfo.M_REIN_COMPANY_ZIP);


                objDataWrapper.AddParameter("@M_REIN_COMPANY_PHONE", ObjReinsurerInfo.M_REIN_COMPANY_PHONE);
                objDataWrapper.AddParameter("@M_REIN_COMPANY_FAX", ObjReinsurerInfo.M_REIN_COMPANY_FAX);

                objDataWrapper.AddParameter("@M_REIN_COMPANY_EXT", ObjReinsurerInfo.M_REIN_COMPANY_EXT);

                //objDataWrapper.AddParameter("@M_REIN_COMPANY_EXT",ObjReinsurerInfo.M_REIN_COMPANY_EXT);
                objDataWrapper.AddParameter("@REIN_COMPANY_WEBSITE", ObjReinsurerInfo.REIN_COMPANY_WEBSITE);
                objDataWrapper.AddParameter("@REIN_COMPANY_IS_BROKER", ObjReinsurerInfo.REIN_COMPANY_IS_BROKER);
                objDataWrapper.AddParameter("@PRINCIPAL_CONTACT", ObjReinsurerInfo.PRINCIPAL_CONTACT);
                objDataWrapper.AddParameter("@OTHER_CONTACT", ObjReinsurerInfo.OTHER_CONTACT);
                objDataWrapper.AddParameter("@FEDERAL_ID", ObjReinsurerInfo.FEDERAL_ID);
                objDataWrapper.AddParameter("@ROUTING_NUMBER", ObjReinsurerInfo.ROUTING_NUMBER);
                objDataWrapper.AddParameter("@RISK_CLASSIFICATION", ObjReinsurerInfo.RISK_CLASSIFICATION);
                objDataWrapper.AddParameter("@AGENCY_CLASSIFICATION", ObjReinsurerInfo.AGENCY_CLASSIFICATION);


                if (ObjReinsurerInfo.TERMINATION_DATE != DateTime.MinValue)
                    objDataWrapper.AddParameter("@TERMINATION_DATE", ObjReinsurerInfo.TERMINATION_DATE);
                else
                    objDataWrapper.AddParameter("@TERMINATION_DATE", System.DBNull.Value);

                objDataWrapper.AddParameter("@TERMINATION_REASON", ObjReinsurerInfo.TERMINATION_REASON);
                objDataWrapper.AddParameter("@DOMICILED_STATE", ObjReinsurerInfo.DOMICILED_STATE);
                objDataWrapper.AddParameter("@NAIC_CODE", ObjReinsurerInfo.NAIC_CODE);
                objDataWrapper.AddParameter("@AM_BEST_RATING", ObjReinsurerInfo.AM_BEST_RATING);
                //added By Chetna
                objDataWrapper.AddParameter("@SUSEP_NUM", ObjReinsurerInfo.SUSEP_NUM);
                objDataWrapper.AddParameter("@COM_TYPE", ObjReinsurerInfo.COM_TYPE);

                if (ObjReinsurerInfo.EFFECTIVE_DATE != DateTime.MinValue)
                    objDataWrapper.AddParameter("@EFFECTIVE_DATE", ObjReinsurerInfo.EFFECTIVE_DATE);
                else
                    objDataWrapper.AddParameter("@EFFECTIVE_DATE", System.DBNull.Value);

                objDataWrapper.AddParameter("@COMMENTS", ObjReinsurerInfo.COMMENTS);
                objDataWrapper.AddParameter("@CREATED_BY", ObjReinsurerInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME", ObjReinsurerInfo.CREATED_DATETIME);
                objDataWrapper.AddParameter("@DISTRICT", ObjReinsurerInfo.DISTRICT);
                objDataWrapper.AddParameter("@BANK_NUMBER", ObjReinsurerInfo.BANK_NUMBER);
                objDataWrapper.AddParameter("@BANK_BRANCH_NUMBER", ObjReinsurerInfo.BANK_BRANCH_NUMBER);
                objDataWrapper.AddParameter("@CARRIER_CNPJ", ObjReinsurerInfo.CARRIER_CNPJ);
                objDataWrapper.AddParameter("@BANK_ACCOUNT_TYPE", ObjReinsurerInfo.BANK_ACCOUNT_TYPE);
                objDataWrapper.AddParameter("@PAYMENT_METHOD", ObjReinsurerInfo.PAYMENT_METHOD);



                SqlParameter objSqlParameter = (SqlParameter)objDataWrapper.AddParameter("@REIN_COMPANY_ID", ObjReinsurerInfo.REIN_COMAPANY_ID, SqlDbType.Int, ParameterDirection.Output);

                int returnResult = 0;
                if (TransactionLogRequired)
                {
                    ObjReinsurerInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/addReinsurer.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(ObjReinsurerInfo);

                    XmlDocument XMLDoc = new XmlDocument();
                    XMLDoc.LoadXml(strTranXML);
                    XmlElement root = XMLDoc.DocumentElement;
                    XmlNode node = root.SelectSingleNode("Map[@field='FEDERAL_ID']");
                    if (node != null)
                    {
                        node.Attributes["NewValue"].InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.ComputeFederalID(node.Attributes["NewValue"].InnerText);
                        node.Attributes["OldValue"].InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.ComputeFederalID(node.Attributes["OldValue"].InnerText);
                    }
                    strTranXML = XMLDoc.InnerXml.ToString();

                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 229;
                    objTransactionInfo.RECORDED_BY = ObjReinsurerInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = "";
                    objTransactionInfo.CHANGE_XML = strTranXML;
                    objTransactionInfo.CUSTOM_INFO = ObjReinsurerInfo.CUSTOM_INFO;
                    //Executing the query
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);

                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                int intREIN_COMAPANY_ID = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

                if (intREIN_COMAPANY_ID == -1)
                {

                    //objDataWrapper.ClearParameteres();
                    //objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
                    //objDataWrapper.(DataWrapper.CloseConnection.YES);
                    return -1;
                }
                else
                {
                    //int REIN_COMPANY_ID = int.Parse(objSqlParameter.Value.ToString());
                    //objDataWrapper.ClearParameteres();
                    //objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                    ObjReinsurerInfo.REIN_COMAPANY_ID = intREIN_COMAPANY_ID;
                    return intREIN_COMAPANY_ID;
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


        #region Update method
        /// <summary>
        /// Update method that recieves Model object to save.
        /// </summary>
        /// <param name="objOldReinsurerInfo">Model object having old information</param>
        /// <param name="ObjReinsurerInfo">Model object having new information(form control's value)</param>
        /// <returns>No. of rows updated (1 or 0)</returns>
        public int Update(ClsReinsurerInfo objOldReinsurerInfo, ClsReinsurerInfo ObjReinsurerInfo)
        {
            string strStoredProc = "MNT_REIN_UPDATE_REINSURER";
            string strTranXML;
            int returnResult = 0;


            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            try
            {

                objDataWrapper.AddParameter("@REIN_COMPANY_ID", ObjReinsurerInfo.REIN_COMAPANY_ID);
                objDataWrapper.AddParameter("@REIN_COMPANY_CODE", ObjReinsurerInfo.REIN_COMAPANY_CODE);
                objDataWrapper.AddParameter("@REIN_COMPANY_NAME", ObjReinsurerInfo.REIN_COMAPANY_NAME);
                objDataWrapper.AddParameter("@REIN_COMPANY_TYPE", ObjReinsurerInfo.REIN_COMPANY_TYPE);

                objDataWrapper.AddParameter("@REIN_COMPANY_ADD1", ObjReinsurerInfo.REIN_COMAPANY_ADD1);
                objDataWrapper.AddParameter("@REIN_COMPANY_ADD2", ObjReinsurerInfo.REIN_COMAPANY_ADD2);
                objDataWrapper.AddParameter("@REIN_COMPANY_CITY", ObjReinsurerInfo.REIN_COMAPANY_CITY);
                objDataWrapper.AddParameter("@REIN_COMPANY_COUNTRY", ObjReinsurerInfo.REIN_COMAPANY_COUNTRY);
                objDataWrapper.AddParameter("@REIN_COMPANY_STATE", ObjReinsurerInfo.REIN_COMAPANY_STATE);
                objDataWrapper.AddParameter("@REIN_COMPANY_ZIP", ObjReinsurerInfo.REIN_COMAPANY_ZIP);
                objDataWrapper.AddParameter("@REIN_COMPANY_PHONE", ObjReinsurerInfo.REIN_COMAPANY_PHONE);
                objDataWrapper.AddParameter("@REIN_COMPANY_EXT", ObjReinsurerInfo.REIN_COMAPANY_EXT);

                //objDataWrapper.AddParameter("@REIN_COMPANY_EXT",ObjReinsurerInfo.REIN_COMPANY_EXT);
                objDataWrapper.AddParameter("@REIN_COMPANY_FAX", ObjReinsurerInfo.REIN_COMAPANY_FAX);
                objDataWrapper.AddParameter("@REIN_COMPANY_SPEED_DIAL", ObjReinsurerInfo.REIN_COMPANY_SPEED_DIAL);

                objDataWrapper.AddParameter("@REIN_COMPANY_MOBILE", ObjReinsurerInfo.REIN_COMAPANY_MOBILE);
                objDataWrapper.AddParameter("@REIN_COMPANY_EMAIL", ObjReinsurerInfo.REIN_COMAPANY_EMAIL);
                objDataWrapper.AddParameter("@REIN_COMPANY_NOTE", ObjReinsurerInfo.REIN_COMAPANY_NOTE);
                objDataWrapper.AddParameter("@REIN_COMPANY_ACC_NUMBER", ObjReinsurerInfo.REIN_COMAPANY_ACC_NUMBER);

                objDataWrapper.AddParameter("@M_REIN_COMPANY_ADD_1", ObjReinsurerInfo.M_REIN_COMPANY_ADD_1);
                objDataWrapper.AddParameter("@M_REIN_COMPANY_ADD_2", ObjReinsurerInfo.M_RREIN_COMPANY_ADD_2);
                objDataWrapper.AddParameter("@M_REIN_COMPANY_CITY", ObjReinsurerInfo.M_REIN_COMPANY_CITY);
                objDataWrapper.AddParameter("@M_REIN_COMPANY_COUNTRY", ObjReinsurerInfo.M_REIN_COMPANY_COUNTRY);

                objDataWrapper.AddParameter("@M_REIN_COMPANY_STATE", ObjReinsurerInfo.M_REIN_COMPANY_STATE);
                objDataWrapper.AddParameter("@M_REIN_COMPANY_ZIP", ObjReinsurerInfo.M_REIN_COMPANY_ZIP);


                objDataWrapper.AddParameter("@M_REIN_COMPANY_PHONE", ObjReinsurerInfo.M_REIN_COMPANY_PHONE);
                objDataWrapper.AddParameter("@M_REIN_COMPANY_FAX", ObjReinsurerInfo.M_REIN_COMPANY_FAX);

                objDataWrapper.AddParameter("@M_REIN_COMPANY_EXT", ObjReinsurerInfo.M_REIN_COMPANY_EXT);

                //objDataWrapper.AddParameter("@M_REIN_COMPANY_EXT",ObjReinsurerInfo.M_REIN_COMPANY_EXT);
                objDataWrapper.AddParameter("@REIN_COMPANY_WEBSITE", ObjReinsurerInfo.REIN_COMPANY_WEBSITE);
                objDataWrapper.AddParameter("@REIN_COMPANY_IS_BROKER", ObjReinsurerInfo.REIN_COMPANY_IS_BROKER);
                objDataWrapper.AddParameter("@PRINCIPAL_CONTACT", ObjReinsurerInfo.PRINCIPAL_CONTACT);
                objDataWrapper.AddParameter("@OTHER_CONTACT", ObjReinsurerInfo.OTHER_CONTACT);
                objDataWrapper.AddParameter("@FEDERAL_ID", ObjReinsurerInfo.FEDERAL_ID);
                objDataWrapper.AddParameter("@ROUTING_NUMBER", ObjReinsurerInfo.ROUTING_NUMBER);
                objDataWrapper.AddParameter("@RISK_CLASSIFICATION", ObjReinsurerInfo.RISK_CLASSIFICATION);
                objDataWrapper.AddParameter("@AGENCY_CLASSIFICATION", ObjReinsurerInfo.AGENCY_CLASSIFICATION);


                if (ObjReinsurerInfo.TERMINATION_DATE != DateTime.MinValue)
                    objDataWrapper.AddParameter("@TERMINATION_DATE", ObjReinsurerInfo.TERMINATION_DATE);
                else
                    objDataWrapper.AddParameter("@TERMINATION_DATE", System.DBNull.Value);

                objDataWrapper.AddParameter("@TERMINATION_REASON", ObjReinsurerInfo.TERMINATION_REASON);
                objDataWrapper.AddParameter("@DOMICILED_STATE", ObjReinsurerInfo.DOMICILED_STATE);
                objDataWrapper.AddParameter("@NAIC_CODE", ObjReinsurerInfo.NAIC_CODE);
                objDataWrapper.AddParameter("@AM_BEST_RATING", ObjReinsurerInfo.AM_BEST_RATING);


                if (ObjReinsurerInfo.EFFECTIVE_DATE != DateTime.MinValue)
                    objDataWrapper.AddParameter("@EFFECTIVE_DATE", ObjReinsurerInfo.EFFECTIVE_DATE);
                else
                    objDataWrapper.AddParameter("@EFFECTIVE_DATE", System.DBNull.Value);

                objDataWrapper.AddParameter("@COMMENTS", ObjReinsurerInfo.COMMENTS);
                objDataWrapper.AddParameter("@MODIFIED_BY", ObjReinsurerInfo.MODIFIED_BY);
                objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", ObjReinsurerInfo.LAST_UPDATED_DATETIME);

                //added By chetna
                objDataWrapper.AddParameter("@SUSEP_NUM", ObjReinsurerInfo.SUSEP_NUM);
                objDataWrapper.AddParameter("@COM_TYPE", ObjReinsurerInfo.COM_TYPE);
                objDataWrapper.AddParameter("@DISTRICT", ObjReinsurerInfo.DISTRICT);
                objDataWrapper.AddParameter("@BANK_NUMBER", ObjReinsurerInfo.BANK_NUMBER);
                objDataWrapper.AddParameter("@BANK_BRANCH_NUMBER", ObjReinsurerInfo.BANK_BRANCH_NUMBER);
                objDataWrapper.AddParameter("@CARRIER_CNPJ", ObjReinsurerInfo.CARRIER_CNPJ);
                objDataWrapper.AddParameter("@BANK_ACCOUNT_TYPE", ObjReinsurerInfo.BANK_ACCOUNT_TYPE);
                objDataWrapper.AddParameter("@PAYMENT_METHOD", ObjReinsurerInfo.PAYMENT_METHOD);
                if (base.TransactionLogRequired)
                {
                    //strTranXML = objBuilder.GetTransactionLogXML(objOldReinsurerInfo,ObjReinsurerInfo);

                    ObjReinsurerInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/addReinsurer.aspx.resx");
                    objBuilder.GetUpdateSQL(objOldReinsurerInfo, ObjReinsurerInfo, out strTranXML, "N");

                    XmlDocument XMLDoc = new XmlDocument();
                    XMLDoc.LoadXml(strTranXML);
                    XmlElement root = XMLDoc.DocumentElement;
                    XmlNode node = root.SelectSingleNode("Map[@field='FEDERAL_ID']");
                    if (node != null)
                    {
                        node.Attributes["NewValue"].InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.ComputeFederalID(node.Attributes["NewValue"].InnerText);
                        node.Attributes["OldValue"].InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.ComputeFederalID(node.Attributes["OldValue"].InnerText);
                    }
                       
                   
                    strTranXML = XMLDoc.InnerXml.ToString();

                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID = 230;
                    objTransactionInfo.RECORDED_BY = ObjReinsurerInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = "";
                    objTransactionInfo.CHANGE_XML = strTranXML;
                    objTransactionInfo.CUSTOM_INFO = ObjReinsurerInfo.CUSTOM_INFO;
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
        #endregion
        #region GET DATA FOR EDIT MODE
        public DataSet GetDataForPageControls(string REIN_COMPANY_ID)
        {
            string strStoredProc = "MNT_REIN_GETXML_REINSURER";
            DataSet objDataSet = null;
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@REIN_COMPANY_ID", REIN_COMPANY_ID);
                objDataWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID);

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
        #region FETCHING DATA
        public DataSet FetchData(int REIN_COMPANY_ID)
        {
            string strStoredProc = "Proc_FetchReisurernfo";
            DataSet dsCount = null;

            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@REIN_COMPANY_ID", REIN_COMPANY_ID, SqlDbType.Int);

                dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }
            return dsCount;
        }
        #endregion
        #region DEACTIVATE OR ACTIVATE CONTRACT TYPE

        public int GetDeactivateActivate(string REIN_COMPANY_ID, string Status_Check)
        {
            string strSql = "Proc_MNT_REIN_DEACTIVATE_ACTIVATE_REINSURER";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
            objDataWrapper.AddParameter("@REIN_COMPANY_ID", REIN_COMPANY_ID);
            objDataWrapper.AddParameter("@STATUS_CHECK", Status_Check, System.Data.SqlDbType.NVarChar);

            //DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
            int returnResult = objDataWrapper.ExecuteNonQuery(strSql);
            return returnResult;
        }

        #endregion

    }
}
