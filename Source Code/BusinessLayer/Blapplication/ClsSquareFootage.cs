/******************************************************************************************
<Author				: -   Anurag Verma
<Start Date				: -	5/12/2005 6:53:46 PM
<End Date				: -	
<Description				: - 	Business Logic for Square Footage module
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Application.HomeOwners;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;  

namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsSquareFootage.
	/// </summary>
	public class ClsSquareFootage : Cms.BusinessLayer.BlApplication.clsapplication   
	{
        private const	string		APP_SQR_FOOT_IMPROVEMENTS			=	"APP_SQR_FOOT_IMPROVEMENTS";

        #region Private Instance Variables
        private			bool		boolTransactionLog;
        
        private const string ACTIVATE_DEACTIVATE_PROC	= "";
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

        #region Constructors
        /// <summary>
        /// deafault constructor
        /// </summary>
        public ClsSquareFootage()
        {
            boolTransactionLog	= base.TransactionLogRequired;
            base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
        }
        #endregion


        #region Add(Insert) functions
        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="objSquareFootageInfo">Model class object.</param>
        /// <returns>No of records effected.</returns>
        public int Add(ClsSquareFootageInfo objSquareFootageInfo)
        {
            string		strStoredProc	=	"Proc_INSERTAPP_SQR_FOOT_IMPROVEMENTS";
            DateTime	RecordDate		=	DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

            try
            {
				objDataWrapper.AddParameter("@CUSTOMER_ID",objSquareFootageInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objSquareFootageInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objSquareFootageInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objSquareFootageInfo.DWELLING_ID);
				
				if(objSquareFootageInfo.TOT_SQR_FOOTAGE != 0)
					objDataWrapper.AddParameter("@TOT_SQR_FOOTAGE",objSquareFootageInfo.TOT_SQR_FOOTAGE);			
				else
					objDataWrapper.AddParameter("@TOT_SQR_FOOTAGE",null);		
				
				if(objSquareFootageInfo.GARAGE_SQR_FOOTAGE != 0)
					objDataWrapper.AddParameter("@GARAGE_SQR_FOOTAGE",objSquareFootageInfo.GARAGE_SQR_FOOTAGE);
				else
					objDataWrapper.AddParameter("@GARAGE_SQR_FOOTAGE",null);
				
				if(objSquareFootageInfo.BREEZE_SQR_FOOTAGE != 0)
					objDataWrapper.AddParameter("@BREEZE_SQR_FOOTAGE",objSquareFootageInfo.BREEZE_SQR_FOOTAGE);
				else
					objDataWrapper.AddParameter("@BREEZE_SQR_FOOTAGE",null);
				
				if(objSquareFootageInfo.BASMT_SQR_FOOTAGE != 0)				
					objDataWrapper.AddParameter("@BASMT_SQR_FOOTAGE",objSquareFootageInfo.BASMT_SQR_FOOTAGE);
				else
                    objDataWrapper.AddParameter("@BASMT_SQR_FOOTAGE",null);
                
				if(objSquareFootageInfo.WIRING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@WIRING_RENOVATION",objSquareFootageInfo.WIRING_RENOVATION);
				else
					objDataWrapper.AddParameter("@WIRING_RENOVATION",null);

				if(objSquareFootageInfo.WIRING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",objSquareFootageInfo.WIRING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",null);
				
				
				if(objSquareFootageInfo.PLUMBING_RENOVATION != 0)				
                    objDataWrapper.AddParameter("@PLUMBING_RENOVATION",objSquareFootageInfo.PLUMBING_RENOVATION);
				else
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",null);
				
				if(objSquareFootageInfo.PLUMBING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",objSquareFootageInfo.PLUMBING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",null);
				
				if(objSquareFootageInfo.HEATING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@HEATING_RENOVATION",objSquareFootageInfo.HEATING_RENOVATION);
				else
					objDataWrapper.AddParameter("@HEATING_RENOVATION",null);
				
				if(objSquareFootageInfo.HEATING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",objSquareFootageInfo.HEATING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",null);

				if(objSquareFootageInfo.ROOFING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",objSquareFootageInfo.ROOFING_RENOVATION);
				else
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",null);
				
				if(objSquareFootageInfo.ROOFING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",objSquareFootageInfo.ROOFING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",null);
				
				if(objSquareFootageInfo.NO_OF_AMPS != 0)				
		                objDataWrapper.AddParameter("@NO_OF_AMPS",objSquareFootageInfo.NO_OF_AMPS);
				else
					    objDataWrapper.AddParameter("@NO_OF_AMPS",null);
				
				if(objSquareFootageInfo.CIRCUIT_BREAKERS != "")				
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",objSquareFootageInfo.CIRCUIT_BREAKERS);
				else
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",null);
				
//				if(objSquareFootageInfo.EXTERIOR_PAINT != 0)								
//					objDataWrapper.AddParameter("@EXTERIOR_PAINT",objSquareFootageInfo.EXTERIOR_PAINT);
//				else
//					objDataWrapper.AddParameter("@EXTERIOR_PAINT",null);
//				
//				if(objSquareFootageInfo.EXTERIOR_PAINT_YEAR != 0)								
//					objDataWrapper.AddParameter("@EXTERIOR_PAINT_YEAR",objSquareFootageInfo.EXTERIOR_PAINT_YEAR);
//				else
//					objDataWrapper.AddParameter("@EXTERIOR_PAINT_YEAR",null);            

                int returnResult = 0;
                if(TransactionLogRequired)
                {
                    objSquareFootageInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/HomeOwners/AddSquareFootage.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objSquareFootageInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID = objSquareFootageInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objSquareFootageInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objSquareFootageInfo.CUSTOMER_ID;
                    objTransactionInfo.RECORDED_BY		=	objSquareFootageInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC		=	"New square footage information is added";
                    objTransactionInfo.CHANGE_XML		=	strTranXML;
                    //Executing the query
                    returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
                }
                else
                {
                    returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (returnResult == -1)
                {
                    return -1;
                }
                else
                {
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
        /// <param name="objOldSquareFootageInfo">Model object having old information</param>
        /// <param name="objSquareFootageInfo">Model object having new information(form control's value)</param>
        /// <returns>No. of rows updated (1 or 0)</returns>
        public int Update(ClsSquareFootageInfo objOldSquareFootageInfo,ClsSquareFootageInfo objSquareFootageInfo)
        {
            string strTranXML;
            string strStoredProc="Proc_UPDATEAPP_SQR_FOOT_IMPROVEMENTS";
            int returnResult = 0;
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
            try 
            {
				objDataWrapper.AddParameter("@CUSTOMER_ID",objSquareFootageInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objSquareFootageInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objSquareFootageInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@DWELLING_ID",objSquareFootageInfo.DWELLING_ID);
				
				if(objSquareFootageInfo.TOT_SQR_FOOTAGE != 0)
					objDataWrapper.AddParameter("@TOT_SQR_FOOTAGE",objSquareFootageInfo.TOT_SQR_FOOTAGE);			
				else
					objDataWrapper.AddParameter("@TOT_SQR_FOOTAGE",null);		
				
				if(objSquareFootageInfo.GARAGE_SQR_FOOTAGE != 0)
					objDataWrapper.AddParameter("@GARAGE_SQR_FOOTAGE",objSquareFootageInfo.GARAGE_SQR_FOOTAGE);
				else
					objDataWrapper.AddParameter("@GARAGE_SQR_FOOTAGE",null);
				
				if(objSquareFootageInfo.BREEZE_SQR_FOOTAGE != 0)
					objDataWrapper.AddParameter("@BREEZE_SQR_FOOTAGE",objSquareFootageInfo.BREEZE_SQR_FOOTAGE);
				else
					objDataWrapper.AddParameter("@BREEZE_SQR_FOOTAGE",null);
				
				if(objSquareFootageInfo.BASMT_SQR_FOOTAGE != 0)				
					objDataWrapper.AddParameter("@BASMT_SQR_FOOTAGE",objSquareFootageInfo.BASMT_SQR_FOOTAGE);
				else
					objDataWrapper.AddParameter("@BASMT_SQR_FOOTAGE",null);
                
				if(objSquareFootageInfo.WIRING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@WIRING_RENOVATION",objSquareFootageInfo.WIRING_RENOVATION);
				else
					objDataWrapper.AddParameter("@WIRING_RENOVATION",null);

				if(objSquareFootageInfo.WIRING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",objSquareFootageInfo.WIRING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@WIRING_UPDATE_YEAR",null);
				
				
				if(objSquareFootageInfo.PLUMBING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",objSquareFootageInfo.PLUMBING_RENOVATION);
				else
					objDataWrapper.AddParameter("@PLUMBING_RENOVATION",null);
				
				if(objSquareFootageInfo.PLUMBING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",objSquareFootageInfo.PLUMBING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@PLUMBING_UPDATE_YEAR",null);
				
				if(objSquareFootageInfo.HEATING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@HEATING_RENOVATION",objSquareFootageInfo.HEATING_RENOVATION);
				else
					objDataWrapper.AddParameter("@HEATING_RENOVATION",null);
				
				if(objSquareFootageInfo.HEATING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",objSquareFootageInfo.HEATING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@HEATING_UPDATE_YEAR",null);

				if(objSquareFootageInfo.ROOFING_RENOVATION != 0)				
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",objSquareFootageInfo.ROOFING_RENOVATION);
				else
					objDataWrapper.AddParameter("@ROOFING_RENOVATION",null);
				
				if(objSquareFootageInfo.ROOFING_UPDATE_YEAR != 0)				
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",objSquareFootageInfo.ROOFING_UPDATE_YEAR);
				else
					objDataWrapper.AddParameter("@ROOFING_UPDATE_YEAR",null);
				
				if(objSquareFootageInfo.NO_OF_AMPS != 0)				
					objDataWrapper.AddParameter("@NO_OF_AMPS",objSquareFootageInfo.NO_OF_AMPS);
				else
					objDataWrapper.AddParameter("@NO_OF_AMPS",null);
				
				if(objSquareFootageInfo.CIRCUIT_BREAKERS != "")				
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",objSquareFootageInfo.CIRCUIT_BREAKERS);
				else
					objDataWrapper.AddParameter("@CIRCUIT_BREAKERS",null);
				
//				if(objSquareFootageInfo.EXTERIOR_PAINT != 0)								
//					objDataWrapper.AddParameter("@EXTERIOR_PAINT",objSquareFootageInfo.EXTERIOR_PAINT);
//				else
//					objDataWrapper.AddParameter("@EXTERIOR_PAINT",null);
//				
//				if(objSquareFootageInfo.EXTERIOR_PAINT_YEAR != 0)								
//					objDataWrapper.AddParameter("@EXTERIOR_PAINT_YEAR",objSquareFootageInfo.EXTERIOR_PAINT_YEAR);
//				else
//					objDataWrapper.AddParameter("@EXTERIOR_PAINT_YEAR",null);     
                if(TransactionLogRequired) 
                {
                    strTranXML = objBuilder.GetTransactionLogXML(objOldSquareFootageInfo,objSquareFootageInfo);

                    objSquareFootageInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/HomeOwners/AddSquareFootage.aspx.resx");Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID	=	3;
                    objTransactionInfo.RECORDED_BY		=	objSquareFootageInfo.MODIFIED_BY;
					objTransactionInfo.APP_ID			=	objSquareFootageInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objSquareFootageInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objSquareFootageInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC		=	"Square footage information is modified";
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
        public DataSet FetchData(int appId,int customerId,int appVersionId,int dwellingId)
        {
            string		strStoredProc	=	"Proc_FetchSquareFootage";
            DataSet dsCount=null;
           		
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_ID",appId,SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionId,SqlDbType.Int);
                objDataWrapper.AddParameter("@DWELLING_ID",dwellingId,SqlDbType.Int);
                

                dsCount = objDataWrapper.ExecuteDataSet(strStoredProc);
			
            }
            catch(Exception ex)
            {
                throw(ex);
            }
            finally
            {
				
            }
            return dsCount;
        }
        #endregion


    }
}
