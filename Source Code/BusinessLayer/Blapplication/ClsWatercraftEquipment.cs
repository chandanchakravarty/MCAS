/******************************************************************************************
<Author					: -   Anurag Verma
<Start Date				: -	5/18/2005 17:55:24 
<End Date				: -	
<Description				: - 	Business login implemented for watercraftequipment information screen
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: -  30-11-2005
<Modified By			: -  Vijay Arora
<Purpose				: -  Added the Policy Functions region
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
	/// Summary description for ClsWatercraftEquipment.
	/// </summary>
	public class ClsWatercraftEquipment: Cms.BusinessLayer.BlApplication.clsapplication  
	{
        private const	string		APP_WATERCRAFT_EQUIP_DETAILLS			=	"APP_WATERCRAFT_EQUIP_DETAILLS";

        #region Private Instance Variables
        private			bool		boolTransactionLog;
        // private int _EQUIP_ID;        
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
    
        #region Add(Insert) functions
        /// <summary>
        /// Saves the information passed in model object to database.
        /// </summary>
        /// <param name="objWatercraftEquimentInfo">Model class object.</param>
        /// <returns>No of records effected.</returns>
        /*public int Add(ClsWatercraftEquipmentsInfo objWatercraftEquimentInfo)
        {
            string		strStoredProc	=	"Proc_InsertAPP_WATERCRAFT_EQUIP_DETAILLS";
            DateTime	RecordDate		=	DateTime.Now;
            DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

            try
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftEquimentInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@APP_ID",objWatercraftEquimentInfo.APP_ID);
                objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftEquimentInfo.APP_VERSION_ID);

                if(objWatercraftEquimentInfo.EQUIP_NO==0)
                    objDataWrapper.AddParameter("@EQUIP_NO",System.DBNull.Value);
                else
                    objDataWrapper.AddParameter("@EQUIP_NO",objWatercraftEquimentInfo.EQUIP_NO);

                objDataWrapper.AddParameter("@EQUIP_TYPE",objWatercraftEquimentInfo.EQUIP_TYPE);
                objDataWrapper.AddParameter("@SHIP_TO_SHORE",objWatercraftEquimentInfo.SHIP_TO_SHORE);

                if(objWatercraftEquimentInfo.YEAR==0)
                   objDataWrapper.AddParameter("@YEAR",System.DBNull.Value );
                else
                    objDataWrapper.AddParameter("@YEAR",objWatercraftEquimentInfo.YEAR);
 
                objDataWrapper.AddParameter("@MAKE",objWatercraftEquimentInfo.MAKE);
                objDataWrapper.AddParameter("@MODEL",objWatercraftEquimentInfo.MODEL);
                objDataWrapper.AddParameter("@SERIAL_NO",objWatercraftEquimentInfo.SERIAL_NO);
                
                if(objWatercraftEquimentInfo.INSURED_VALUE==0)
                    objDataWrapper.AddParameter("@INSURED_VALUE",System.DBNull.Value);
                else
                    objDataWrapper.AddParameter("@INSURED_VALUE",objWatercraftEquimentInfo.INSURED_VALUE);
				//commented by Pravesh on 17 sep 07 as new option '0' is added to the deductible
               // if(objWatercraftEquimentInfo.EQUIP_AMOUNT==0 || objWatercraftEquimentInfo.EQUIP_AMOUNT ==.00 || objWatercraftEquimentInfo.EQUIP_AMOUNT==0.00)
				//	objDataWrapper.AddParameter("@EQUIP_AMOUNT",System.DBNull.Value);
				//else
					objDataWrapper.AddParameter("@EQUIP_AMOUNT",objWatercraftEquimentInfo.EQUIP_AMOUNT);
                
				//RPSNGH - 10 May 2006
				objDataWrapper.AddParameter("@OTHER_DESCRIPTION",objWatercraftEquimentInfo.OTHER_DESCRIPTION);
				objDataWrapper.AddParameter("@EQUIPMENT_TYPE",objWatercraftEquimentInfo.EQUIPMENT_TYPE);
				//End of addition

                objDataWrapper.AddParameter("@ASSOCIATED_BOAT",objWatercraftEquimentInfo.ASSOCIATED_BOAT);
                objDataWrapper.AddParameter("@IS_ACTIVE",objWatercraftEquimentInfo.IS_ACTIVE);
                objDataWrapper.AddParameter("@CREATED_BY",objWatercraftEquimentInfo.CREATED_BY);
                objDataWrapper.AddParameter("@CREATED_DATETIME",objWatercraftEquimentInfo.CREATED_DATETIME);
                SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@EQUIP_ID",objWatercraftEquimentInfo.EQUIP_ID,SqlDbType.Int,ParameterDirection.Output);

                int returnResult = 0;
                if(TransactionLogRequired)
                {
                    objWatercraftEquimentInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/Watercrafts/AddEquipmentGenInformation.aspx.resx");
                    SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
                    string strTranXML = objBuilder.GetTransactionLogXML(objWatercraftEquimentInfo);
                    Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
                    objTransactionInfo.TRANS_TYPE_ID	=	1;
                    objTransactionInfo.RECORDED_BY		=	objWatercraftEquimentInfo.CREATED_BY;
					objTransactionInfo.APP_ID			=	objWatercraftEquimentInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objWatercraftEquimentInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objWatercraftEquimentInfo.CUSTOMER_ID;
                    objTransactionInfo.TRANS_DESC		=	"New watercraft's equipment is added";
                    objTransactionInfo.CHANGE_XML		=	strTranXML;
                    //Executing the query
                    returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
                }
                else
                {
                    returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
                }
                int EQUIP_ID = int.Parse(objSqlParameter.Value.ToString());
                objDataWrapper.ClearParameteres();
                objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
                if (EQUIP_ID == -1)
                {
                    return -1;
                }
                else
                {
                    objWatercraftEquimentInfo.EQUIP_ID = EQUIP_ID;
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


		/// <summary>
		/// Overloded Method to save equipment details from acord xml
		/// </summary>
		/// <param name="objWatercraftEquimentInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		/*public int Add(ClsWatercraftEquipmentsInfo objWatercraftEquimentInfo, DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_InsertAPP_WATERCRAFT_EQUIP_DETAILS_ACORD";
			DateTime	RecordDate		=	DateTime.Now;
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftEquimentInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objWatercraftEquimentInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftEquimentInfo.APP_VERSION_ID);

				if(objWatercraftEquimentInfo.EQUIP_NO==0)
					objDataWrapper.AddParameter("@EQUIP_NO",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@EQUIP_NO",objWatercraftEquimentInfo.EQUIP_NO);

				objDataWrapper.AddParameter("@EQUIP_TYPE",objWatercraftEquimentInfo.EQUIP_TYPE);
				objDataWrapper.AddParameter("@SHIP_TO_SHORE",System.DBNull.Value);

				if(objWatercraftEquimentInfo.YEAR==0)
					objDataWrapper.AddParameter("@YEAR",System.DBNull.Value );
				else
					objDataWrapper.AddParameter("@YEAR",objWatercraftEquimentInfo.YEAR);
 
				objDataWrapper.AddParameter("@MAKE",objWatercraftEquimentInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL","");
				objDataWrapper.AddParameter("@SERIAL_NO",objWatercraftEquimentInfo.SERIAL_NO);
                
				if(objWatercraftEquimentInfo.INSURED_VALUE==0)
					objDataWrapper.AddParameter("@INSURED_VALUE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@INSURED_VALUE",objWatercraftEquimentInfo.INSURED_VALUE);
				if(objWatercraftEquimentInfo.EQUIP_AMOUNT==0 || objWatercraftEquimentInfo.EQUIP_AMOUNT ==.00 || objWatercraftEquimentInfo.EQUIP_AMOUNT==0.00)
					objDataWrapper.AddParameter("@EQUIP_AMOUNT",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@EQUIP_AMOUNT",objWatercraftEquimentInfo.EQUIP_AMOUNT);
                

				//RPSNGH - 10 May 2006
				objDataWrapper.AddParameter("@OTHER_DESCRIPTION",objWatercraftEquimentInfo.OTHER_DESCRIPTION);
				objDataWrapper.AddParameter("@EQUIPMENT_TYPE",objWatercraftEquimentInfo.EQUIPMENT_TYPE);
				//End of addition


				objDataWrapper.AddParameter("@ASSOCIATED_BOAT",objWatercraftEquimentInfo.ASSOCIATED_BOAT);
				objDataWrapper.AddParameter("@IS_ACTIVE",objWatercraftEquimentInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objWatercraftEquimentInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",DateTime.Now);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@EQUIP_ID",objWatercraftEquimentInfo.EQUIP_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objWatercraftEquimentInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/Watercrafts/AddEquipmentGenInformation.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objWatercraftEquimentInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objWatercraftEquimentInfo.CREATED_BY;
					objTransactionInfo.APP_ID			=	objWatercraftEquimentInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objWatercraftEquimentInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objWatercraftEquimentInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"New watercraft's equipment is added from Acord";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int EQUIP_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (EQUIP_ID == -1)
				{
					return -1;
				}
				else
				{
					objWatercraftEquimentInfo.EQUIP_ID = EQUIP_ID;
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
        /// <param name="objOldWatercraftEquimentInfo">Model object having old information</param>
        /// <param name="objWatercraftEquimentInfo">Model object having new information(form control's value)</param>
        /// <returns>No. of rows updated (1 or 0)</returns>
       /* public int Update(ClsWatercraftEquipmentsInfo objOldWatercraftEquimentInfo,ClsWatercraftEquipmentsInfo objWatercraftEquimentInfo)
        {
            string strTranXML;
            int returnResult = 0;
            string strStoredProc="Proc_UpdateAPP_WATERCRAFT_EQUIP_DETAILLS";
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
            try 
            {
                objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftEquimentInfo.CUSTOMER_ID);
                objDataWrapper.AddParameter("@APP_ID",objWatercraftEquimentInfo.APP_ID);
                objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftEquimentInfo.APP_VERSION_ID);
                objDataWrapper.AddParameter("@EQUIP_ID",objWatercraftEquimentInfo.EQUIP_ID);
                if(objWatercraftEquimentInfo.EQUIP_NO==0)
                    objDataWrapper.AddParameter("@EQUIP_NO",System.DBNull.Value);
                else
                    objDataWrapper.AddParameter("@EQUIP_NO",objWatercraftEquimentInfo.EQUIP_NO);
                objDataWrapper.AddParameter("@EQUIP_TYPE",objWatercraftEquimentInfo.EQUIP_TYPE);
                objDataWrapper.AddParameter("@SHIP_TO_SHORE",objWatercraftEquimentInfo.SHIP_TO_SHORE);
                if(objWatercraftEquimentInfo.YEAR==0)
                    objDataWrapper.AddParameter("@YEAR",System.DBNull.Value );
                else
                    objDataWrapper.AddParameter("@YEAR",objWatercraftEquimentInfo.YEAR);
                objDataWrapper.AddParameter("@MAKE",objWatercraftEquimentInfo.MAKE);
                objDataWrapper.AddParameter("@MODEL",objWatercraftEquimentInfo.MODEL);
                objDataWrapper.AddParameter("@SERIAL_NO",objWatercraftEquimentInfo.SERIAL_NO);
             
                if(objWatercraftEquimentInfo.INSURED_VALUE==0)
                    objDataWrapper.AddParameter("@INSURED_VALUE",System.DBNull.Value);
                else
                    objDataWrapper.AddParameter("@INSURED_VALUE",objWatercraftEquimentInfo.INSURED_VALUE);
				//if(objWatercraftEquimentInfo.EQUIP_AMOUNT==0) commented by Pravesh on 17 sep 07 as new option '0' is added to the deductible
				//	objDataWrapper.AddParameter("@EQUIP_AMOUNT",System.DBNull.Value);
				//else
					objDataWrapper.AddParameter("@EQUIP_AMOUNT",objWatercraftEquimentInfo.EQUIP_AMOUNT);

				                
				//RPSNGH - 10 May 2006
				objDataWrapper.AddParameter("@OTHER_DESCRIPTION",objWatercraftEquimentInfo.OTHER_DESCRIPTION);
				objDataWrapper.AddParameter("@EQUIPMENT_TYPE",objWatercraftEquimentInfo.EQUIPMENT_TYPE);
				//End of addition
                
                objDataWrapper.AddParameter("@ASSOCIATED_BOAT",objWatercraftEquimentInfo.ASSOCIATED_BOAT);                
                objDataWrapper.AddParameter("@MODIFIED_BY",objWatercraftEquimentInfo.MODIFIED_BY);
                objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objWatercraftEquimentInfo.LAST_UPDATED_DATETIME);
                if(TransactionLogRequired) 
                {      
                    objWatercraftEquimentInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Application/Aspx/Watercrafts/AddEquipmentGenInformation.aspx.resx");
                    strTranXML							= objBuilder.GetTransactionLogXML(objOldWatercraftEquimentInfo,objWatercraftEquimentInfo);
					if(strTranXML	==	"<LabelFieldMapping></LabelFieldMapping>" || strTranXML == "")
						returnResult	=	objDataWrapper.ExecuteNonQuery(strStoredProc);
					else				
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objWatercraftEquimentInfo.MODIFIED_BY;
						objTransactionInfo.APP_ID			=	objWatercraftEquimentInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID	=	objWatercraftEquimentInfo.APP_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objWatercraftEquimentInfo.CUSTOMER_ID;
						objTransactionInfo.TRANS_DESC		=	"Watercraft equipment information is modified";
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
        }*/
        #endregion

        #region FETCHING DATA
        public DataSet FetchData(int appId,int customerId,int appVersionId,int equipId)
        {
            string		strStoredProc	=	"Proc_FetchEquipmentInfo";
            DataSet dsCount=null;
           		
            try
            {
                DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
                objDataWrapper.AddParameter("@CUSTOMER_ID",customerId,SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_ID",appId,SqlDbType.Int);
                objDataWrapper.AddParameter("@APP_VERSION_ID",appVersionId,SqlDbType.Int);           
                objDataWrapper.AddParameter("@EQUIP_ID",equipId,SqlDbType.Int);           
                

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

		#region ACTIVATE/DEACTIVATE
		//Added by Swastika on 31st Mar'06 for SDLC>Design>Buttons Fxn. #111 : Deletes the App level Equipment Info.
		/*public void ActivateDeactivateAppEquipment(ClsWatercraftEquipmentsInfo objWatercraftEquipmentInfo,string strStatus)
		{
			
			string		strStoredProc	=	"Proc_ActivateDeactivateAppEquipment";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftEquipmentInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objWatercraftEquipmentInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftEquipmentInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@EQUIP_ID",objWatercraftEquipmentInfo.EQUIP_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);

				int returnResult = 0;
				if(TransactionLogRequired)
				{																			
					objWatercraftEquipmentInfo.TransactLabel = ClsCommon.MapTransactionLabel("Application/aspx/Watercrafts/AddEquipmentGenInformation.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID			=	objWatercraftEquipmentInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objWatercraftEquipmentInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objWatercraftEquipmentInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objWatercraftEquipmentInfo.MODIFIED_BY;
					if(strStatus.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	"Equipment is Activated";
					else
						objTransactionInfo.TRANS_DESC		=	"Equipment is Deactivated";
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

		#region DELETE
		//Added by Swastika on 31st Mar'06 for SDLC>Design>Buttons Fxn. #111 : Deletes the App level Equipment Info.
		/*public int Delete(ClsWatercraftEquipmentsInfo objWatercraftEquimentInfo)
		{
			int returnResult = 0;
			string		strStoredProc	=	"Proc_DeleteAPP_EQUIPMENT_INFORMATION";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftEquimentInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objWatercraftEquimentInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objWatercraftEquimentInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@EQUIP_ID",objWatercraftEquimentInfo.EQUIP_ID);
					

				if(TransactionLogRequired) 
				{			
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;   //verify
					objTransactionInfo.RECORDED_BY		=	objWatercraftEquimentInfo.MODIFIED_BY;
					objTransactionInfo.APP_ID			=	objWatercraftEquimentInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID	=	objWatercraftEquimentInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objWatercraftEquimentInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Equipment has been Deleted";
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

		#region GET EQUIP_NO (App/Pol)
		public int GetNewWatercraftEquipNumber(int CustomerID,int AppID, int AppVersionID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_ID",AppID,SqlDbType.Int);
				objDataWrapper.AddParameter("@APP_VERSION_ID",AppVersionID,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetAppNewWatercraftEquipNumber");
				return int.Parse(dsTemp.Tables[0].Rows[0][0].ToString());		
			} 
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}
		public int GetPolNewWatercraftEquipNumber(int CustomerID,int PolicyID, int PolicyVersionID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID,SqlDbType.Int);
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetPolNewWatercraftEquipNumber");
				return int.Parse(dsTemp.Tables[0].Rows[0][0].ToString());		
			} 
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		}
		#endregion

		#region POLICY FUNCTIONS.

		public DataSet FetchPolicyWaterCraftEquipmentData(int CustomerID,int PolicyID,int PolicyVersionID,int EquipmentID)
		{
			string		strStoredProc	=	"Proc_FetchPolicyWaterCraftEquipmentInfo";
			DataSet dsCount=null;
           		
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID,SqlDbType.Int);           
				objDataWrapper.AddParameter("@EQUIP_ID",EquipmentID,SqlDbType.Int);           
                

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


		public int AddPolicyWaterCraftEquipment(Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEquipmentInfo objWatercraftEquimentInfo)
		{
			string		strStoredProc	=	"Proc_InsertPOL_WATERCRAFT_EQUIP_DETAILLS";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftEquimentInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objWatercraftEquimentInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWatercraftEquimentInfo.POLICY_VERSION_ID);

				if(objWatercraftEquimentInfo.EQUIP_NO==0)
					objDataWrapper.AddParameter("@EQUIP_NO",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@EQUIP_NO",objWatercraftEquimentInfo.EQUIP_NO);

				objDataWrapper.AddParameter("@EQUIP_TYPE",objWatercraftEquimentInfo.EQUIP_TYPE);
				objDataWrapper.AddParameter("@SHIP_TO_SHORE",objWatercraftEquimentInfo.SHIP_TO_SHORE);

				if(objWatercraftEquimentInfo.YEAR==0)
					objDataWrapper.AddParameter("@YEAR",System.DBNull.Value );
				else
					objDataWrapper.AddParameter("@YEAR",objWatercraftEquimentInfo.YEAR);

				objDataWrapper.AddParameter("@MAKE",objWatercraftEquimentInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objWatercraftEquimentInfo.MODEL);
				objDataWrapper.AddParameter("@SERIAL_NO",objWatercraftEquimentInfo.SERIAL_NO);
                
				if(objWatercraftEquimentInfo.INSURED_VALUE==0)
					objDataWrapper.AddParameter("@INSURED_VALUE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@INSURED_VALUE",objWatercraftEquimentInfo.INSURED_VALUE);

				objDataWrapper.AddParameter("@ASSOCIATED_BOAT",objWatercraftEquimentInfo.ASSOCIATED_BOAT);
				objDataWrapper.AddParameter("@IS_ACTIVE",objWatercraftEquimentInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objWatercraftEquimentInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objWatercraftEquimentInfo.CREATED_DATETIME);
				
				//Added By Shafi
				//commented by Pravesh on 17 sep 07 as new option '0' is added to the deductible
				//objDataWrapper.AddParameter("@EQUIP_AMOUNT",DefaultValues.GetDoubleNull(objWatercraftEquimentInfo.EQUIP_AMOUNT));
				objDataWrapper.AddParameter("@EQUIP_AMOUNT",objWatercraftEquimentInfo.EQUIP_AMOUNT);

				objDataWrapper.AddParameter("@OTHER_DESCRIPTION",objWatercraftEquimentInfo.OTHER_DESCRIPTION);
				objDataWrapper.AddParameter("@EQUIPMENT_TYPE",objWatercraftEquimentInfo.EQUIPMENT_TYPE);
				

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@EQUIP_ID",objWatercraftEquimentInfo.EQUIP_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objWatercraftEquimentInfo.TransactLabel = ClsCommon.MapTransactionLabel("/policies/Aspx/Watercraft/PolicyWatercraftAddEquipment.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objWatercraftEquimentInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objWatercraftEquimentInfo.CREATED_BY;
					objTransactionInfo.POLICY_ID 			=	objWatercraftEquimentInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	objWatercraftEquimentInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objWatercraftEquimentInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"New policy watercraft's equipment is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int EQUIP_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (EQUIP_ID == -1)
				{
					return -1;
				}
				else
				{
					objWatercraftEquimentInfo.EQUIP_ID = EQUIP_ID;
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



		public int UpdatePolicyWaterCraftEquipment(Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEquipmentInfo objOldWatercraftEquimentInfo,Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEquipmentInfo objWatercraftEquimentInfo)
		{
			string strTranXML;
			int returnResult = 0;
			string strStoredProc="Proc_UpdatePOL_WATERCRAFT_EQUIP_DETAILLS";
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftEquimentInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objWatercraftEquimentInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWatercraftEquimentInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@EQUIP_ID",objWatercraftEquimentInfo.EQUIP_ID);
				if(objWatercraftEquimentInfo.EQUIP_NO==0)
					objDataWrapper.AddParameter("@EQUIP_NO",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@EQUIP_NO",objWatercraftEquimentInfo.EQUIP_NO);
				objDataWrapper.AddParameter("@EQUIP_TYPE",objWatercraftEquimentInfo.EQUIP_TYPE);
				objDataWrapper.AddParameter("@SHIP_TO_SHORE",objWatercraftEquimentInfo.SHIP_TO_SHORE);
				if(objWatercraftEquimentInfo.YEAR==0)
					objDataWrapper.AddParameter("@YEAR",System.DBNull.Value );
				else
					objDataWrapper.AddParameter("@YEAR",objWatercraftEquimentInfo.YEAR);
				objDataWrapper.AddParameter("@MAKE",objWatercraftEquimentInfo.MAKE);
				objDataWrapper.AddParameter("@MODEL",objWatercraftEquimentInfo.MODEL);
				objDataWrapper.AddParameter("@SERIAL_NO",objWatercraftEquimentInfo.SERIAL_NO);
             
				if(objWatercraftEquimentInfo.INSURED_VALUE==0)
					objDataWrapper.AddParameter("@INSURED_VALUE",System.DBNull.Value);
				else
					objDataWrapper.AddParameter("@INSURED_VALUE",objWatercraftEquimentInfo.INSURED_VALUE);
				objDataWrapper.AddParameter("@ASSOCIATED_BOAT",objWatercraftEquimentInfo.ASSOCIATED_BOAT);                
				objDataWrapper.AddParameter("@MODIFIED_BY",objWatercraftEquimentInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objWatercraftEquimentInfo.LAST_UPDATED_DATETIME);
				//Added By Shafi
				//commented by Pravesh on 17 sep 07 as new option '0' is added to the deductible
				//objDataWrapper.AddParameter("@EQUIP_AMOUNT",DefaultValues.GetDoubleNull(objWatercraftEquimentInfo.EQUIP_AMOUNT));
				objDataWrapper.AddParameter("@EQUIP_AMOUNT",objWatercraftEquimentInfo.EQUIP_AMOUNT);

				objDataWrapper.AddParameter("@OTHER_DESCRIPTION",objWatercraftEquimentInfo.OTHER_DESCRIPTION);
				objDataWrapper.AddParameter("@EQUIPMENT_TYPE",objWatercraftEquimentInfo.EQUIPMENT_TYPE);
				

				if(TransactionLogRequired) 
				{      
					objWatercraftEquimentInfo.TransactLabel = ClsCommon.MapTransactionLabel("/policies/Aspx/Watercraft/PolicyWatercraftAddEquipment.aspx.resx");
					strTranXML							= objBuilder.GetTransactionLogXML(objOldWatercraftEquimentInfo,objWatercraftEquimentInfo);
					if(strTranXML=="" || strTranXML=="<LabelFieldMapping></LabelFieldMapping>")
						returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
					else
					{
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objWatercraftEquimentInfo.MODIFIED_BY;
						objTransactionInfo.POLICY_ID 		=	objWatercraftEquimentInfo.POLICY_ID;
						objTransactionInfo.POLICY_VER_TRACKING_ID =	objWatercraftEquimentInfo.POLICY_VERSION_ID;
						objTransactionInfo.CLIENT_ID		=	objWatercraftEquimentInfo.CUSTOMER_ID;
						objTransactionInfo.TRANS_DESC		=	"Policy watercraft equipment information is modified";
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

		//Added by Swastika on 31st Mar'06 for SDLC>Design>Buttons Fxn. #111 : Deletes the Pol level Equipment Info.
		public int DeletePolEquipmentInfo(Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEquipmentInfo objWatercraftEquipmentInfo)
		{
			int returnResult = 0;
			string		strStoredProc	=	"Proc_DeletePOL_EQUIPMENT_INFORMATION";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftEquipmentInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objWatercraftEquipmentInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWatercraftEquipmentInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@EQUIP_ID",objWatercraftEquipmentInfo.EQUIP_ID);
					

				if(TransactionLogRequired) 
				{			
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;   //verify
					objTransactionInfo.RECORDED_BY		=	objWatercraftEquipmentInfo.MODIFIED_BY;
					objTransactionInfo.POLICY_ID			=	objWatercraftEquipmentInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objWatercraftEquipmentInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objWatercraftEquipmentInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_DESC		=	"Equipment has been Deleted";
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

		//Added by Swastika on 31st Mar'06 for SDLC>Design>Buttons Fxn. #111 : Activates/Deactivates the Pol level Equipment Info.
		public void ActivateDeactivatePolEquipment(Cms.Model.Policy.Watercraft.ClsPolicyWatercraftEquipmentInfo objWatercraftEquipmentInfo,string strStatus)
		{
			
			string		strStoredProc	=	"Proc_ActivateDeactivatePolEquipment";			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objWatercraftEquipmentInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objWatercraftEquipmentInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objWatercraftEquipmentInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@EQUIP_ID",objWatercraftEquipmentInfo.EQUIP_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strStatus);

				int returnResult = 0;
				if(TransactionLogRequired)
				{																			
					objWatercraftEquipmentInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/Watercraft/PolicyWatercraftAddEquipment.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.APP_ID			=	objWatercraftEquipmentInfo.POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID	=	objWatercraftEquipmentInfo.POLICY_VERSION_ID;
					objTransactionInfo.CLIENT_ID		=	objWatercraftEquipmentInfo.CUSTOMER_ID;
					objTransactionInfo.RECORDED_BY		=	objWatercraftEquipmentInfo.MODIFIED_BY;
					if(strStatus.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	"Equipment is Activated";
					else
						objTransactionInfo.TRANS_DESC		=	"Equipment is Deactivated";
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
		}

		#endregion
 
    }
}
