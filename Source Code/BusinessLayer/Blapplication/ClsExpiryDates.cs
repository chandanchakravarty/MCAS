/******************************************************************************************
<Author				: -   Anurag Verma
<Start Date				: -	4/28/2005 10:44:25 AM
<End Date				: -	
<Description				: - 	contains functionality for add, update and activate/ deactivate function
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Anshuman
<Modified By			: - June 07, 2005
<Purpose				: - transaction description modified
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using Cms.DataLayer;
using System.Data.SqlClient;
using System.Configuration;
using Cms.Model.Application.PriorLoss;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;  

namespace Cms.BusinessLayer.BlApplication
{
    /// <summary>
    /// 
    /// </summary>
    public class ClsExpiryDates : Cms.BusinessLayer.BlApplication.clsapplication,IDisposable
    {
        private const	string		APP_EXPIRY_DATES			=	"APP_EXPIRY_DATES";

        #region Private Instance Variables
        private			bool		boolTransactionLog;
        // private int _EXPDT_ID;
        private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateAPP_EXPIRY_DATES";
        #endregion

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

        #region private Utility Functions
        #endregion

        #region Constructors
        /// <summary>
        /// deafault constructor
        /// </summary>
        public ClsExpiryDates()
        {
            boolTransactionLog	= base.TransactionLogRequired;
            base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
        }
        #endregion

        #region Add(Insert) functions
        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="objExpiryDatesModel">Model class object.</param>
        /// <returns>No of records effected.</returns>
        public int Add(ClsExpiryDatesInfo objExpiryDatesModel)
        {
            string		strStoredProc	=	"Proc_InsertAPP_EXPIRY_DATES";
            DateTime	RecordDate		=	DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID",objExpiryDatesModel.CUSTOMER_ID);
                objDataWrapper.AddParameter("@EXPDT_LOB",objExpiryDatesModel.EXPDT_LOB);
                objDataWrapper.AddParameter("@EXPDT_CARR",ReplaceInvalidCharecter(objExpiryDatesModel.EXPDT_CARR));
                if(objExpiryDatesModel.EXPDT_DATE.Year!=1900 )
                    objDataWrapper.AddParameter("@EXPDT_DATE",objExpiryDatesModel.EXPDT_DATE);
                else
                    objDataWrapper.AddParameter("@EXPDT_DATE",System.DBNull.Value);

                objDataWrapper.AddParameter("@EXPDT_PREM",objExpiryDatesModel.EXPDT_PREM);

                if(objExpiryDatesModel.EXPDT_CONT_DATE.Year!=1900 )
                    objDataWrapper.AddParameter("@EXPDT_CONT_DATE",objExpiryDatesModel.EXPDT_CONT_DATE);
                else
                    objDataWrapper.AddParameter("@EXPDT_CONT_DATE",System.DBNull.Value);

                objDataWrapper.AddParameter("@EXPDT_CSR",objExpiryDatesModel.EXPDT_CSR);
                objDataWrapper.AddParameter("@EXPDT_PROD",objExpiryDatesModel.EXPDT_PROD);
                objDataWrapper.AddParameter("@EXPDT_NOTES",ReplaceInvalidCharecter(objExpiryDatesModel.EXPDT_NOTES));
                objDataWrapper.AddParameter("@POLICY_NUMBER",ReplaceInvalidCharecter(objExpiryDatesModel.POLICY_NUMBER));            
                objDataWrapper.AddParameter("@IS_ACTIVE",objExpiryDatesModel.IS_ACTIVE);
                objDataWrapper.AddParameter("@CREATED_BY",objExpiryDatesModel.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME",objExpiryDatesModel.CREATED_DATETIME);
                objDataWrapper.AddParameter("@MODIFIED_BY",objExpiryDatesModel.MODIFIED_BY);
                objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objExpiryDatesModel.LAST_UPDATED_DATETIME);
                SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@EXPDT_ID",objExpiryDatesModel.EXPDT_ID,SqlDbType.Int,ParameterDirection.Output);

                int returnResult = 0;
                if(TransactionLogRequired)
                {
                    objExpiryDatesModel.TransactLabel = ClsCommon.MapTransactionLabel("/Application/PriorLoss/AddExpiryDates.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objExpiryDatesModel);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objExpiryDatesModel.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objExpiryDatesModel.APP_VERSION_ID;
					
					objTransactionInfo.CLIENT_ID		=	objExpiryDatesModel.CUSTOMER_ID;
                    objTransactionInfo.RECORDED_BY		=	objExpiryDatesModel.CREATED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1711", "");// "New Expiration date is added";
                    objTransactionInfo.CHANGE_XML		=	strTranXML;
                    //Executing the query
                    returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
                }
                else
                {
                    returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                int EXPDT_ID = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (EXPDT_ID == -1)
                {
                    return -1;
                }
                else
                {
                    objExpiryDatesModel.EXPDT_ID = EXPDT_ID;
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

        #region Update method
        /// <summary>
        /// Update method that recieves Model object to save.
        /// </summary>
        /// <param name="objOldExpiryDatesModel">Model object having old information</param>
        /// <param name="objExpiryDatesModel">Model object having new information(form control's value)</param>
        /// <returns>No. of rows updated (1 or 0)</returns>
        public int Update(ClsExpiryDatesInfo objOldExpiryDatesModel,ClsExpiryDatesInfo objExpiryDatesModel)
        {
            string strTranXML;
            string		strStoredProc	=	"Proc_UpdateAPP_EXPIRY_DATES";
            int returnResult = 0;
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
            try 
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID",objExpiryDatesModel.CUSTOMER_ID);
                
                objDataWrapper.AddParameter("@EXPDT_ID",objExpiryDatesModel.EXPDT_ID);
                objDataWrapper.AddParameter("@EXPDT_LOB",objExpiryDatesModel.EXPDT_LOB);
                objDataWrapper.AddParameter("@EXPDT_CARR",ReplaceInvalidCharecter(objExpiryDatesModel.EXPDT_CARR));
                if(objExpiryDatesModel.EXPDT_DATE.Year!=1900 )
                    objDataWrapper.AddParameter("@EXPDT_DATE",objExpiryDatesModel.EXPDT_DATE);
                else
                    objDataWrapper.AddParameter("@EXPDT_DATE",System.DBNull.Value);

                objDataWrapper.AddParameter("@EXPDT_PREM",objExpiryDatesModel.EXPDT_PREM);

                if(objExpiryDatesModel.EXPDT_CONT_DATE.Year!=1900 )
                    objDataWrapper.AddParameter("@EXPDT_CONT_DATE",objExpiryDatesModel.EXPDT_CONT_DATE);
                else
                    objDataWrapper.AddParameter("@EXPDT_CONT_DATE",System.DBNull.Value);
                objDataWrapper.AddParameter("@EXPDT_CSR",objExpiryDatesModel.EXPDT_CSR);
                objDataWrapper.AddParameter("@EXPDT_PROD",objExpiryDatesModel.EXPDT_PROD);
                objDataWrapper.AddParameter("@EXPDT_NOTES",ReplaceInvalidCharecter(objExpiryDatesModel.EXPDT_NOTES));
                objDataWrapper.AddParameter("@POLICY_NUMBER",objExpiryDatesModel.POLICY_NUMBER);                
                objDataWrapper.AddParameter("@MODIFIED_BY",objExpiryDatesModel.MODIFIED_BY);
                objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objExpiryDatesModel.LAST_UPDATED_DATETIME);
                if(TransactionLogRequired) 
                {
					objExpiryDatesModel.TransactLabel = ClsCommon.MapTransactionLabel("/Application/PriorLoss/AddExpiryDates.aspx.resx");
                    strTranXML = objBuilder.GetTransactionLogXML(objOldExpiryDatesModel,objExpiryDatesModel);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.APP_ID = objExpiryDatesModel.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objExpiryDatesModel.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objExpiryDatesModel.CUSTOMER_ID;
                    objTransactionInfo.RECORDED_BY		=	objExpiryDatesModel.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1712", "");// "Expiration date is modified";
                    objTransactionInfo.CHANGE_XML		=	strTranXML;

                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                return returnResult;
            }
            catch(Exception ex)
            {
                throw(ex);
            }
            finally
            {
                if(objDataWrapper != null) 
                {
                    objDataWrapper.Dispose();
                }
                if(objBuilder != null) 
                {
                    objBuilder = null;
                }
            }
        }
        #endregion
        #region FETCHING DATA
        public string FetchData(int expdtId,int customer_Id)
        {
            string		strStoredProc	=	"Proc_FetchExpiryDatesInfo";
            DataSet dsCount=null;
           			
            SqlParameter [] sparam=new SqlParameter[1];
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@EXPDT_ID",expdtId,SqlDbType.Int);

                objDataWrapper.AddParameter("@CUSTOMER_ID",customer_Id,SqlDbType.Int);

                dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
            }
            catch(Exception ex)
            {
                throw(ex);
            }
            finally
            {
				
            }
            return dsCount.GetXml();
        }
        #endregion

    }
}
