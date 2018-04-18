/******************************************************************************************
<Author					: -   Anurag Verma
<Start Date				: -	5/18/2005 11:32:24 AM
<End Date				: -	
<Description				: - 	Business login implemented for watercraft trailer information screen
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: -		Vijay Arora
<Modified By			: -		28-11-2005
<Purpose				: -		Added the Policy WaterCraft Trailer Functions.
*******************************************************************************************/ 

using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlApplication;
//using Cms.Model.Application.Watercrafts;
using Cms.DataLayer; 

namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// Summary description for ClsTrailer.
	/// </summary>
	public class ClsTrailer : Cms.BusinessLayer.BlApplication.clsapplication   
	{
        private const	string		APP_WATERCRAFT_TRAILER_INFO			=	"APP_WATERCRAFT_TRAILER_INFO";

        #region Private Instance Variables
            private			bool		boolTransactionLog;
          //  private int _TRAILER_ID;
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

		public ClsTrailer()
		{
			//
			// TODO: Add constructor logic here
            boolTransactionLog	= base.TransactionLogRequired;
		}

        #region FETCHING DATA
        public DataSet FetchData(int appId,int customerId,int appVersionId,int trailerId)
        {
            string		strStoredProc	=	"Proc_FetchTrailerInfo";
            DataSet dsCount=null;
           		
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_ID",appId,SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionId,SqlDbType.Int);           
                objDataWrapper.AddParameter("@TRAILER_ID",trailerId,SqlDbType.Int);           
                

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
         
        #region Add(Insert) functions
        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="objTrailerInfo">Model class object.</param>
        /// <returns>No of records effected.</returns>
       /* public int Add(ClsWatercraftTrailerInfo objTrailerInfo)
        {
            string		strStoredProc	=	"Proc_InsertAPP_WATERCRAFT_TRAILER_INFO";
            DateTime	RecordDate		=	DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
              
            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID",objTrailerInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@APP_ID",objTrailerInfo.APP_ID);
                objDataWrapper.AddParameter("@APP_VERSION_ID",objTrailerInfo.APP_VERSION_ID);
                if(objTrailerInfo.TRAILER_NO==0)
                   objDataWrapper.AddParameter("@TRAILER_NO",System.DBNull.Value);
                else
                   objDataWrapper.AddParameter("@TRAILER_NO",objTrailerInfo.TRAILER_NO);

                if(objTrailerInfo.YEAR==0)
                    objDataWrapper.AddParameter("@YEAR",System.DBNull.Value);
                else
                    objDataWrapper.AddParameter("@YEAR",objTrailerInfo.YEAR);

                objDataWrapper.AddParameter("@MANUFACTURER",objTrailerInfo.MANUFACTURER);
				objDataWrapper.AddParameter("@MODEL",objTrailerInfo.MODEL);
                objDataWrapper.AddParameter("@SERIAL_NO",objTrailerInfo.SERIAL_NO);
             

                if(objTrailerInfo.INSURED_VALUE ==0)
                    objDataWrapper.AddParameter("@INSURED_VALUE",System.DBNull.Value);
                else
                    objDataWrapper.AddParameter("@INSURED_VALUE",objTrailerInfo.INSURED_VALUE);

                objDataWrapper.AddParameter("@ASSOCIATED_BOAT",objTrailerInfo.ASSOCIATED_BOAT);
                objDataWrapper.AddParameter("@IS_ACTIVE",objTrailerInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@CREATED_BY",objTrailerInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objTrailerInfo.CREATED_DATETIME);
                objDataWrapper.AddParameter("@TRAILER_TYPE",objTrailerInfo.TRAILER_TYPE);
				objDataWrapper.AddParameter("@TRAILER_DED",objTrailerInfo.TRAILER_DED);
				objDataWrapper.AddParameter("@TRAILER_DED_ID",objTrailerInfo.TRAILER_DED_ID);
				objDataWrapper.AddParameter("@TRAILER_DED_AMOUNT_TEXT",objTrailerInfo.TRAILER_DED_AMOUNT_TEXT);
                
                SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@TRAILER_ID",objTrailerInfo.TRAILER_ID,SqlDbType.Int,ParameterDirection.Output);

                int returnResult = 0;
                if(TransactionLogRequired)
                {
                    objTrailerInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/Watercrafts/AddTrailerInformation.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objTrailerInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID	=	1;
                    objTransactionInfo.RECORDED_BY		=	objTrailerInfo.CREATED_BY;
					objTransactionInfo.APP_ID			=	objTrailerInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objTrailerInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objTrailerInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC		=	"New trailer information is added";
                    objTransactionInfo.CHANGE_XML		=	strTranXML;
				    //Executing the query
                    returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
                }
                else
                {
                    returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                int TRAILER_ID = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (TRAILER_ID == -1)
                {
                    return -1;
                }
                else
                {
                    objTrailerInfo.TRAILER_ID = TRAILER_ID;
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
        }*/
        #endregion

        #region Update method
        /// <summary>
        /// Update method that recieves Model object to save.
        /// </summary>
        /// <param name="objOldTrailerInfo">Model object having old information</param>
        /// <param name="objTrailerInfo">Model object having new information(form control's value)</param>
        /// <returns>No. of rows updated (1 or 0)</returns>
       /* public int Update(ClsWatercraftTrailerInfo objOldTrailerInfo,ClsWatercraftTrailerInfo objTrailerInfo)
        {
            string strTranXML;
            string		strStoredProc	=	"Proc_UpdateAPP_WATERCRAFT_TRAILER_INFO";
            int returnResult = 0;
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
            try 
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID",objTrailerInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@APP_ID",objTrailerInfo.APP_ID);
                objDataWrapper.AddParameter("@APP_VERSION_ID",objTrailerInfo.APP_VERSION_ID);
                objDataWrapper.AddParameter("@TRAILER_ID",objTrailerInfo.TRAILER_ID);       
				//SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@TRAILER_ID",objTrailerInfo.TRAILER_ID,SqlDbType.Int,ParameterDirection.Output);

                if(objTrailerInfo.TRAILER_NO==0)
                    objDataWrapper.AddParameter("@TRAILER_NO",System.DBNull.Value);
                else
                    objDataWrapper.AddParameter("@TRAILER_NO",objTrailerInfo.TRAILER_NO);

             
                if(objTrailerInfo.YEAR==0)
                    objDataWrapper.AddParameter("@YEAR",System.DBNull.Value);
                else
                    objDataWrapper.AddParameter("@YEAR",objTrailerInfo.YEAR);

                objDataWrapper.AddParameter("@MANUFACTURER",objTrailerInfo.MANUFACTURER);
				objDataWrapper.AddParameter("@MODEL",objTrailerInfo.MODEL);
                objDataWrapper.AddParameter("@SERIAL_NO",objTrailerInfo.SERIAL_NO);
       
          
                if(objTrailerInfo.INSURED_VALUE==0)
                    objDataWrapper.AddParameter("@INSURED_VALUE",System.DBNull.Value);
                else
                    objDataWrapper.AddParameter("@INSURED_VALUE",objTrailerInfo.INSURED_VALUE);
                
                objDataWrapper.AddParameter("@ASSOCIATED_BOAT",objTrailerInfo.ASSOCIATED_BOAT);
                objDataWrapper.AddParameter("@MODIFIED_BY",objTrailerInfo.MODIFIED_BY);
                objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objTrailerInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@TRAILER_TYPE",objTrailerInfo.TRAILER_TYPE);
				objDataWrapper.AddParameter("@TRAILER_DED",objTrailerInfo.TRAILER_DED);
				objDataWrapper.AddParameter("@TRAILER_DED_ID",objTrailerInfo.TRAILER_DED_ID);
				objDataWrapper.AddParameter("@TRAILER_DED_AMOUNT_TEXT",objTrailerInfo.TRAILER_DED_AMOUNT_TEXT);

                if(TransactionLogRequired) 
                {
                    objTrailerInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/Watercrafts/AddTrailerInformation.aspx.resx");Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					strTranXML		 = objBuilder.GetTransactionLogXML(objOldTrailerInfo,objTrailerInfo);
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else				
					{	
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objTrailerInfo.MODIFIED_BY;
						objTransactionInfo.APP_ID			=	objTrailerInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID	=	objTrailerInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objTrailerInfo.CUSTOMER_ID;
						objTransactionInfo.TRANS_DESC		=	"Information Has Been Updated";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

                }
                else
                {
                    returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
//				int TRAILER_ID = int.Parse(objSqlParameter.Value.ToString());
//				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//				if (TRAILER_ID == -1)
//				{
//					return -1;
//				}
//				else
//				{
					return returnResult;
//				}
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
        }*/
        #endregion

		#region Delete Method
		//Added by Swastika on 31st Mar'06 for SDLC>Design>Buttons Fxn. #103 : Deletes the App level Trailer Info.
		/*public int Delete(ClsWatercraftTrailerInfo objTrailerInfo)
		{
			int returnResult = 0;
			string		strStoredProc	=	"Proc_DeleteAPP_TRAILER_INFORMATION";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objTrailerInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objTrailerInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objTrailerInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@TRAILER_ID",objTrailerInfo.TRAILER_ID);
					

				if(TransactionLogRequired) 
				{
					objTrailerInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/Watercrafts/AddTrailerInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objTrailerInfo);		
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;   //verify
					objTransactionInfo.RECORDED_BY		=	objTrailerInfo.MODIFIED_BY;
					objTransactionInfo.APP_ID			=	objTrailerInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objTrailerInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objTrailerInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Trailer has been Deleted";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				

				if(returnResult > 0)
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				else
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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
			}
		}*/
		#endregion
		
		#region ACTIVATE/DEACTIVATE
		//Added by Swastika on 31st Mar'06 for SDLC>Design>Buttons Fxn. #103 : Activate/Deactivate the App level Trailer Info.
		/*public void ActivateDeactivateAppTrailer(ClsWatercraftTrailerInfo objTrailerInfo,string strStatus)
		{
			
			string		strStoredProc	=	"Proc_ActivateDeactivateAppTrailer";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objTrailerInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objTrailerInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objTrailerInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@TRAILER_ID",objTrailerInfo.TRAILER_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);

				int returnResult = 0;
				if(TransactionLogRequired)
				{																			
					objTrailerInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/Watercrafts/AddTrailerInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objTrailerInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID		=	1;
					objTransactionInfo.APP_ID				=	objTrailerInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID		=	objTrailerInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID			=	objTrailerInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY			=	objTrailerInfo.MODIFIED_BY;
					if(strStatus.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	"Trailer is Activated";
					else
						objTransactionInfo.TRANS_DESC		=	"Trailer is Deactivated";
					//objTransactionInfo.CUSTOM_INFO		=	strCustomInfo;					
					//objTransactionInfo.CUSTOM_INFO		=	";Operator Name = " + strDriverName + ";Operator Code = " + strDriverCode;					
					objTransactionInfo.CHANGE_XML			=	strTranXML;
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
			}
			catch(Exception ex)
			{
				throw(ex);				
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}	
		}*/
		#endregion

		#region Get New trailer No. (App/Pol)
		public int GetNewWatercraftTrailerNumber(int CustomerID,int AppID, int AppVersionID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetAppNewWatercraftTrailerNumber");
				return int.Parse(dsTemp.Tables[0].Rows[0][0].ToString());		
			} 
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}

		public int GetPolNewWatercraftTrailerNumber(int CustomerID,int policyID, int policyVersionID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",policyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolNewWatercraftTrailerNumber");
				return int.Parse(dsTemp.Tables[0].Rows[0][0].ToString());		
			} 
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}
		#endregion

		#region POLICY FUNCTIONS

		/// <summary>
		/// Fetch the Policy WaterCraft Trailer Information.
		/// </summary>
		/// <param name="customerID"></param>
		/// <param name="policyID"></param>
		/// <param name="policyVersionID"></param>
		/// <param name="trailerID"></param>
		/// <returns></returns>
		public DataSet FetchPolicyWaterCraftTrailer(int customerID, int policyID, int policyVersionID,int trailerID)
		{
			string		strStoredProc	=	"Proc_FetchPolicyWaterCraftTrailer";
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",policyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVersionID,SqlDbType.Int);           
				objDataWrapper.AddParameter("@TRAILER_ID",trailerID,SqlDbType.Int);           
                

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


		/// <summary>
		/// Saves the Policy WaterCraft Trailer Information
		/// </summary>
		/// <param name="objTrailerInfo"></param>
		/// <returns></returns>
		public int AddPolicyWaterCraftTrailer(Cms.Model.Policy.Watercraft.ClsPolicyWatercraftTrailerInfo objTrailerInfo)
		{
			string		strStoredProc	=	"Proc_InsertPOL_WATERCRAFT_TRAILER_INFO";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
              
			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objTrailerInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objTrailerInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objTrailerInfo.POLICY_VERSION_ID);
				if(objTrailerInfo.TRAILER_NO==0)
					objDataWrapper.AddParameter("@TRAILER_NO",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@TRAILER_NO",objTrailerInfo.TRAILER_NO);

				if(objTrailerInfo.YEAR==0)
					objDataWrapper.AddParameter("@YEAR",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@YEAR",objTrailerInfo.YEAR);
				objDataWrapper.AddParameter("@MODEL",objTrailerInfo.MODEL);
				objDataWrapper.AddParameter("@MANUFACTURER",objTrailerInfo.MANUFACTURER);
				objDataWrapper.AddParameter("@SERIAL_NO",objTrailerInfo.SERIAL_NO);
             

				if(objTrailerInfo.INSURED_VALUE ==0)
					objDataWrapper.AddParameter("@INSURED_VALUE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@INSURED_VALUE",objTrailerInfo.INSURED_VALUE);

				objDataWrapper.AddParameter("@ASSOCIATED_BOAT",objTrailerInfo.ASSOCIATED_BOAT);
				objDataWrapper.AddParameter("@IS_ACTIVE",objTrailerInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objTrailerInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objTrailerInfo.CREATED_DATETIME);
                objDataWrapper.AddParameter("@TRAILER_TYPE",objTrailerInfo.TRAILER_TYPE);
				objDataWrapper.AddParameter("@TRAILER_DED",objTrailerInfo.TRAILER_DED);
				objDataWrapper.AddParameter("@TRAILER_DED_ID",objTrailerInfo.TRAILER_DED_ID);
				objDataWrapper.AddParameter("@TRAILER_DED_AMOUNT_TEXT",objTrailerInfo.TRAILER_DED_AMOUNT_TEXT);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@TRAILER_ID",objTrailerInfo.TRAILER_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objTrailerInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Policies/Aspx/Watercraft/PolicyAddWatercraftTrailer.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objTrailerInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objTrailerInfo.CREATED_BY;
					objTransactionInfo.POLICY_ID 		=	objTrailerInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objTrailerInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objTrailerInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Policy WaterCraft Trailer Information Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int TRAILER_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (TRAILER_ID == -1)
				{
					return -1;
				}
				else
				{
					objTrailerInfo.TRAILER_ID = TRAILER_ID;
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


		/// <summary>
		/// Update the Policy WaterCraft Trailer Information
		/// </summary>
		/// <param name="objOldTrailerInfo"></param>
		/// <param name="objTrailerInfo"></param>
		/// <returns></returns>
		public int UpdatePolicyWaterCraftTrailer(Cms.Model.Policy.Watercraft.ClsPolicyWatercraftTrailerInfo objOldTrailerInfo,Cms.Model.Policy.Watercraft.ClsPolicyWatercraftTrailerInfo objTrailerInfo)
		{
			string strTranXML;
			string		strStoredProc	=	"Proc_UpdatePOL_WATERCRAFT_TRAILER_INFO";
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objTrailerInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objTrailerInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objTrailerInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@TRAILER_ID",objTrailerInfo.TRAILER_ID);       

				if(objTrailerInfo.TRAILER_NO==0)
					objDataWrapper.AddParameter("@TRAILER_NO",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@TRAILER_NO",objTrailerInfo.TRAILER_NO);

             
				if(objTrailerInfo.YEAR==0)
					objDataWrapper.AddParameter("@YEAR",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@YEAR",objTrailerInfo.YEAR);
				objDataWrapper.AddParameter("@MODEL",objTrailerInfo.MODEL);
				objDataWrapper.AddParameter("@MANUFACTURER",objTrailerInfo.MANUFACTURER);
				objDataWrapper.AddParameter("@SERIAL_NO",objTrailerInfo.SERIAL_NO);
       
          
				if(objTrailerInfo.INSURED_VALUE==0)
					objDataWrapper.AddParameter("@INSURED_VALUE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@INSURED_VALUE",objTrailerInfo.INSURED_VALUE);
                
				objDataWrapper.AddParameter("@ASSOCIATED_BOAT",objTrailerInfo.ASSOCIATED_BOAT);
				objDataWrapper.AddParameter("@MODIFIED_BY",objTrailerInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objTrailerInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@TRAILER_TYPE",objTrailerInfo.TRAILER_TYPE);
				objDataWrapper.AddParameter("@TRAILER_DED",objTrailerInfo.TRAILER_DED);
				objDataWrapper.AddParameter("@TRAILER_DED_ID",objTrailerInfo.TRAILER_DED_ID);
				objDataWrapper.AddParameter("@TRAILER_DED_AMOUNT_TEXT",objTrailerInfo.TRAILER_DED_AMOUNT_TEXT);
				if(TransactionLogRequired) 
				{
					
					objTrailerInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Policies/Aspx/Watercraft/PolicyAddWatercraftTrailer.aspx.resx");
					strTranXML		 = objBuilder.GetTransactionLogXML(objOldTrailerInfo,objTrailerInfo);
					if(strTranXML=="" || strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
						
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objTrailerInfo.MODIFIED_BY;
						objTransactionInfo.POLICY_ID 		=	objTrailerInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID	=	objTrailerInfo.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objTrailerInfo.CUSTOMER_ID;
						objTransactionInfo.TRANS_DESC		=	"Policy WaterCraft Trailer Information Has Been Updated";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					}

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

		//Added by Swastika on 6th Apr'06 for SDLC>Design>Buttons Fxn. #103 : Activate/Deactivate the Pol level Trailer Info.
		public void ActivateDeactivatePolTrailer(Cms.Model.Policy.Watercraft.ClsPolicyWatercraftTrailerInfo objTrailerInfo,string strStatus)
		{
			
			string		strStoredProc	=	"Proc_ActivateDeactivatePolTrailer";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objTrailerInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objTrailerInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objTrailerInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@TRAILER_ID",objTrailerInfo.TRAILER_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);

				int returnResult = 0;
				if(TransactionLogRequired)
				{																			
					objTrailerInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Watercraft/PolicyAddWatercraftTrailer.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objTrailerInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID			=	1;
					objTransactionInfo.POLICY_ID				=	objTrailerInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objTrailerInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID				=	objTrailerInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY				=	objTrailerInfo.MODIFIED_BY;
					if(strStatus.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC			=	"Trailer is Activated";
					else
						objTransactionInfo.TRANS_DESC			=	"Trailer is Deactivated";
					objTransactionInfo.CHANGE_XML				=	strTranXML;
						returnResult		= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);				
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
		//Added by Swastika on 31st Mar'06 for SDLC>Design>Buttons Fxn. #103 : Deletes the Pol level Trailer Info.
		public int DeletePolTrailer(Cms.Model.Policy.Watercraft.ClsPolicyWatercraftTrailerInfo objTrailerInfo)
		{
			int returnResult = 0;
			string		strStoredProc	=	"Proc_DeletePOL_TRAILER_INFORMATION";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objTrailerInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objTrailerInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objTrailerInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@TRAILER_ID",objTrailerInfo.TRAILER_ID);
					

				if(TransactionLogRequired) 
				{
					objTrailerInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Watercraft/PolicyAddWatercraftTrailer.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objTrailerInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;   //verify
					objTransactionInfo.RECORDED_BY		=	objTrailerInfo.MODIFIED_BY;
					objTransactionInfo.POLICY_ID			=	objTrailerInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objTrailerInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objTrailerInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Trailer has been Deleted";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				

				if(returnResult > 0)
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				else
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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
			}
		}
		#endregion
		//added by Pravesh
		public DataSet GetDeductToRemoveBasedOnType(string strTrailerType ,DataSet objDataSet)
		{
			string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/Coverages/WatercraftCoverages.xml");
			//string xml = "";
			StringBuilder sbXML = new StringBuilder();
			
			DataTable dtLimits = objDataSet.Tables[1];	
			DataTable dtStateLob =objDataSet.Tables[0];	
			string LOBID=dtStateLob.Rows[0]["LOB_ID"].ToString();
			//Read Coverages to remove from XML file
			XmlDocument doc = new XmlDocument();

			doc.Load(filePath);
			sbXML.Append("<Coverages>");
			XmlNode node = doc.SelectSingleNode("Root/Boat[@ID='" +  strTrailerType + "']");	
			if (node!=null)
			{
				XmlNode removeNode = node.SelectSingleNode("Remove");
				XmlNodeList removeNodeLimit = removeNode.SelectNodes("CoverageLimit");
				StringBuilder  sbRemoveLimit =new StringBuilder(); 
				foreach(XmlNode removeNodeList in removeNodeLimit )
				{
					string covID = removeNodeList.Attributes["COV_ID"].Value;
					XmlNodeList removeLimitList = removeNodeList.SelectNodes("LIMIT_DEDUC");
					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"COMP\" Remove=\"N\" Mandatory=\"N\">");
					//Loop thru each node to remove
				
					foreach(XmlNode remNode in removeLimitList)
					{
						string strLIMIT_DEDUC_ID = remNode.Attributes["LIMIT_DEDUC_ID"].Value;
						string Amount = remNode.Attributes["Amount"].Value;
						if ( sbRemoveLimit.ToString() == "" )
						{
							sbRemoveLimit.Append(strLIMIT_DEDUC_ID);
						}
						else
						{
							sbRemoveLimit.Append("," + strLIMIT_DEDUC_ID );
						}
				
					}
					DataRow[] drLimits = dtLimits.Select("COV_ID=" + covID  + " and LIMIT_DEDUC_ID IN (" + sbRemoveLimit.ToString() + ")");
					foreach(DataRow drLimit in drLimits)
					{	
						if ( drLimit["LIMIT_DEDUC_ID"] != System.DBNull.Value )
						{
						
							sbXML.Append("<Limit id='"+drLimit["LIMIT_DEDUC_ID"]+"' amount='"+drLimit["LIMIT_DEDUC_AMOUNT"]+"' type='"+drLimit["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
							sbXML.Append("</Limit>");	
						
						}
					}
					sbXML.Append("</Coverage>");
				}
			}
			//Removing Limt on the basis of LOB
			XmlNode node3 = doc.SelectSingleNode("Root/LOB[@ID='" +  LOBID + "']");	
			if (node3!=null)
			{
				XmlNode removeNode3 = node3.SelectSingleNode("Remove");
				XmlNodeList removeNodeLimit3 = removeNode3.SelectNodes("CoverageLimit");
				StringBuilder  sbRemoveLimit3 =new StringBuilder(); 
				foreach(XmlNode removeNodeList in removeNodeLimit3 )
				{
					string covID = removeNodeList.Attributes["COV_ID"].Value;
					XmlNodeList removeLimitList = removeNodeList.SelectNodes("LIMIT_DEDUC");
					sbXML.Append("<Coverage COV_ID=\"" + covID + "\" COV_CODE=\"COMP\" Remove=\"N\" Mandatory=\"N\">");
					//Loop thru each coveages to remove
				
					foreach(XmlNode remNode in removeLimitList)
					{
						string strLIMIT_DEDUC_ID = remNode.Attributes["LIMIT_DEDUC_ID"].Value;
						string Amount = remNode.Attributes["Amount"].Value;
						if ( sbRemoveLimit3.ToString() == "" )
						{
							sbRemoveLimit3.Append(strLIMIT_DEDUC_ID);
						}
						else
						{
							sbRemoveLimit3.Append("," + strLIMIT_DEDUC_ID );
						}
				
					}
					DataRow[] drLimits = dtLimits.Select("COV_ID=" + covID  + " and LIMIT_DEDUC_ID IN (" + sbRemoveLimit3.ToString() + ")");
					foreach(DataRow drLimit in drLimits)
					{	
						if ( drLimit["LIMIT_DEDUC_ID"] != System.DBNull.Value )
						{
						
							sbXML.Append("<Limit id='"+drLimit["LIMIT_DEDUC_ID"]+"' amount='"+drLimit["LIMIT_DEDUC_AMOUNT"]+"' type='"+drLimit["LIMIT_DEDUC_TYPE"]+"' Remove=\"Y\" Default=\"Y\">");
							sbXML.Append("</Limit>");	
						
						}
					}
					sbXML.Append("</Coverage>");
				}
			}

			//end here
			sbXML.Append("</Coverages>");
			Cms.BusinessLayer.BlApplication.ClsCoverages objCoverage=new ClsCoverages();
			objDataSet=objCoverage.DeleteCoverageOptions(objDataSet,sbXML.ToString()).Copy();
			return objDataSet;
			
		}

    }
}

