/******************************************************************************************
<Author				: -   Shrikant Bhatt
<Start Date				: -	4/28/2005 5:40:57 PM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using Cms.Model.Application;
using System.Data;
using System.Data.SqlClient ;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;
namespace Cms.BusinessLayer.BlApplication
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsAutoIDInformation : Cms.BusinessLayer.BlApplication.clsapplication,IDisposable
	{
		private const	string		APP_AUTO_ID_CARD_INFO			=	"APP_AUTO_ID_CARD_INFO";

		#region Private Instance Variables
		private bool boolTransactionRequired			= true;
		//  private int _AUTO_CARD_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateAuto";
		#endregion

		#region Public Properties
		
		
		public bool TransactionRequired
		{
			get
			{
				return boolTransactionRequired;
			}
			set
			{
				boolTransactionRequired=value;
			}
		}
		#endregion

		#region private Utility Functions
		#endregion

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsAutoIDInformation()
		{
			//TransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		
		/// <summary>
		/// Inserts/Updates record in APP_AUTO_ID_CARD_INFO
		/// </summary>
		/// <param name="objAutoIDInfo"></param>
		/// <param name="objDataWrapper"></param>
		/// <returns></returns>
		public int Save(Cms.Model.Application.ClsAutoIDInfo objAutoIDInfo,DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_SaveAPP_AUTO_ID_CARD_INFO";
			DateTime	RecordDate		=	DateTime.Now;
			
			
				objDataWrapper.AddParameter("@CUSTOMER_ID",objAutoIDInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objAutoIDInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objAutoIDInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objAutoIDInfo.VEHICLE_ID);
				
				if(objAutoIDInfo.ID_EFFECTIVE_DATE == DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@ID_EFFECTIVE_DATE",null);
				}
				else
				{
					objDataWrapper.AddParameter("@ID_EFFECTIVE_DATE",objAutoIDInfo.ID_EFFECTIVE_DATE);
				}
				
				if(objAutoIDInfo.ID_EXPITATION_DATE == DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@ID_EXPITATION_DATE",null);
				}
				else
				{
					objDataWrapper.AddParameter("@ID_EXPITATION_DATE",objAutoIDInfo.ID_EXPITATION_DATE);
				}
				objDataWrapper.AddParameter("@NAME_TYPE",objAutoIDInfo.NAME_TYPE);
				objDataWrapper.AddParameter("@NAME_ID",objAutoIDInfo.NAME_ID);
				objDataWrapper.AddParameter("@A_NAME",objAutoIDInfo.NAME);
				objDataWrapper.AddParameter("@ADDRESS1",objAutoIDInfo.ADDRESS1);
				objDataWrapper.AddParameter("@ADDRESS2",objAutoIDInfo.ADDRESS2);
				objDataWrapper.AddParameter("@CITY",objAutoIDInfo.CITY);
				objDataWrapper.AddParameter("@STATE",objAutoIDInfo.STATE);
				objDataWrapper.AddParameter("@ZIP",objAutoIDInfo.ZIP);
				objDataWrapper.AddParameter("@EMAIL",objAutoIDInfo.EMAIL);
				objDataWrapper.AddParameter("@NAME_PRINT",objAutoIDInfo.NAME_PRINT);
				objDataWrapper.AddParameter("@STATE_TYPE",objAutoIDInfo.STATE_TYPE);
				objDataWrapper.AddParameter("@IS_ACTIVE",objAutoIDInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objAutoIDInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",RecordDate);
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);
				objDataWrapper.AddParameter("@SPECIAL_WORD_FRONT",objAutoIDInfo.SPECIAL_WORD_FRONT);
				objDataWrapper.AddParameter("@SPECIAL_WORD_BACK",objAutoIDInfo.SPECIAL_WORD_BACK);
				objDataWrapper.AddParameter("@IS_UPDATED",objAutoIDInfo.IS_UPDATED);

				//SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@AUTO_CARD_ID",objAutoIDInfo.AUTO_CARD_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				return returnResult;
		}


		public int Add(Cms.Model.Application.ClsAutoIDInfo objAutoIDInfo,DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_InsertAPP_AUTO_ID_CARD_INFO";
			DateTime	RecordDate		=	DateTime.Now;
			//Set transaction label in the new object, if required
			if (this.boolTransactionRequired)
			{
				objAutoIDInfo.TransactLabel= Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel(@"application\Aspx\AddAutoIDInformation.aspx.resx");
			}

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objAutoIDInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objAutoIDInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objAutoIDInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objAutoIDInfo.VEHICLE_ID);
				
				if(objAutoIDInfo.ID_EFFECTIVE_DATE == DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@ID_EFFECTIVE_DATE",null);
				}
				else
				{
					objDataWrapper.AddParameter("@ID_EFFECTIVE_DATE",objAutoIDInfo.ID_EFFECTIVE_DATE);
				}
				
				if(objAutoIDInfo.ID_EXPITATION_DATE == DateTime.MinValue)
				{
					objDataWrapper.AddParameter("@ID_EXPITATION_DATE",null);
				}
				else
				{
					objDataWrapper.AddParameter("@ID_EXPITATION_DATE",objAutoIDInfo.ID_EXPITATION_DATE);
				}
				objDataWrapper.AddParameter("@NAME_TYPE",objAutoIDInfo.NAME_TYPE);
				objDataWrapper.AddParameter("@NAME_ID",objAutoIDInfo.NAME_ID);
				objDataWrapper.AddParameter("@A_NAME",objAutoIDInfo.NAME);
				objDataWrapper.AddParameter("@ADDRESS1",objAutoIDInfo.ADDRESS1);
				objDataWrapper.AddParameter("@ADDRESS2",objAutoIDInfo.ADDRESS2);
				objDataWrapper.AddParameter("@CITY",objAutoIDInfo.CITY);
				objDataWrapper.AddParameter("@STATE",objAutoIDInfo.STATE);
				objDataWrapper.AddParameter("@ZIP",objAutoIDInfo.ZIP);
				objDataWrapper.AddParameter("@EMAIL",objAutoIDInfo.EMAIL);
				objDataWrapper.AddParameter("@NAME_PRINT",objAutoIDInfo.NAME_PRINT);
				objDataWrapper.AddParameter("@STATE_TYPE",objAutoIDInfo.STATE_TYPE);
				objDataWrapper.AddParameter("@IS_ACTIVE",objAutoIDInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objAutoIDInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objAutoIDInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);
				objDataWrapper.AddParameter("@SPECIAL_WORD_FRONT",objAutoIDInfo.SPECIAL_WORD_FRONT);
				objDataWrapper.AddParameter("@SPECIAL_WORD_BACK",objAutoIDInfo.SPECIAL_WORD_BACK);
				objDataWrapper.AddParameter("@IS_UPDATED",objAutoIDInfo.IS_UPDATED);

				//SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@AUTO_CARD_ID",objAutoIDInfo.AUTO_CARD_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objAutoIDInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.APP_ID = objAutoIDInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objAutoIDInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objAutoIDInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objAutoIDInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"New auto id information is added";	
					objTransactionInfo.CHANGE_XML		=	strTranXML;


					objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					returnResult	=	1;
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
			
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objAutoIDInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(Cms.Model.Application.ClsAutoIDInfo objAutoIDInfo)
		{
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				return Add(objAutoIDInfo,objDataWrapper);
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

		public int Update(Cms.Model.Application.ClsAutoIDInfo objOldAutoIDInfo,Cms.Model.Application.ClsAutoIDInfo objAutoIDInfo, DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_UpdateAUTO_ID_CARD_INFO";
			DateTime	RecordDate		=	DateTime.Now;

			if (this.boolTransactionRequired)
			{
				objAutoIDInfo.TransactLabel= Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel(@"application\Aspx\AddAutoIDInformation.aspx.resx");
			}

			try
			{
				objDataWrapper.AddParameter("@CUSTOMER_ID",objAutoIDInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@APP_ID",objAutoIDInfo.APP_ID);
				objDataWrapper.AddParameter("@APP_VERSION_ID",objAutoIDInfo.APP_VERSION_ID);
				objDataWrapper.AddParameter("@VEHICLE_ID",objAutoIDInfo.VEHICLE_ID);
				if(objAutoIDInfo.ID_EFFECTIVE_DATE.Ticks == 0)
				{
					objDataWrapper.AddParameter("@ID_EFFECTIVE_DATE",null);
				}
				else
				{
					objDataWrapper.AddParameter("@ID_EFFECTIVE_DATE",objAutoIDInfo.ID_EFFECTIVE_DATE);
				}
				if(objAutoIDInfo.ID_EXPITATION_DATE.Ticks == 0)
				{
					objDataWrapper.AddParameter("@ID_EXPITATION_DATE",null);
				}
				else
				{
					objDataWrapper.AddParameter("@ID_EXPITATION_DATE",objAutoIDInfo.ID_EXPITATION_DATE);
				}
				objDataWrapper.AddParameter("@NAME_TYPE",objAutoIDInfo.NAME_TYPE);
				objDataWrapper.AddParameter("@NAME_ID",objAutoIDInfo.NAME_ID);
				objDataWrapper.AddParameter("@A_NAME",objAutoIDInfo.NAME);
				objDataWrapper.AddParameter("@ADDRESS1",objAutoIDInfo.ADDRESS1);
				objDataWrapper.AddParameter("@ADDRESS2",objAutoIDInfo.ADDRESS2);
				objDataWrapper.AddParameter("@CITY",objAutoIDInfo.CITY);
				objDataWrapper.AddParameter("@STATE",objAutoIDInfo.STATE);
				objDataWrapper.AddParameter("@ZIP",objAutoIDInfo.ZIP);
				objDataWrapper.AddParameter("@EMAIL",objAutoIDInfo.EMAIL);
				objDataWrapper.AddParameter("@NAME_PRINT",objAutoIDInfo.NAME_PRINT);
				objDataWrapper.AddParameter("@STATE_TYPE",objAutoIDInfo.STATE_TYPE);
				objDataWrapper.AddParameter("@IS_ACTIVE",objAutoIDInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objAutoIDInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",null);
				objDataWrapper.AddParameter("@MODIFIED_BY",objAutoIDInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objAutoIDInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@SPECIAL_WORD_FRONT",objAutoIDInfo.SPECIAL_WORD_FRONT);
				objDataWrapper.AddParameter("@SPECIAL_WORD_BACK",objAutoIDInfo.SPECIAL_WORD_BACK);
				objDataWrapper.AddParameter("@IS_UPDATED",objAutoIDInfo.IS_UPDATED);
				//SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@AUTO_CARD_ID",objAutoIDInfo.AUTO_CARD_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objOldAutoIDInfo,objAutoIDInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					
					objTransactionInfo.APP_ID = objAutoIDInfo.APP_ID;
					objTransactionInfo.APP_VERSION_ID = objAutoIDInfo.APP_VERSION_ID;
					objTransactionInfo.CLIENT_ID = objAutoIDInfo.CUSTOMER_ID;
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objAutoIDInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Auto id information is modified";	
					objTransactionInfo.CHANGE_XML		=	strTranXML;


					objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
					returnResult	=	1;
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
	
		public int Update(Cms.Model.Application.ClsAutoIDInfo objOldAutoIDInfo,Cms.Model.Application.ClsAutoIDInfo objAutoIDInfo)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				return Update(objOldAutoIDInfo,objAutoIDInfo,objDataWrapper);
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
			//Set transaction label in the new object, if required
			
		}
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldAutoIDInfo">Model object having old information</param>
		/// <param name="objAutoIDInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update1(Cms.Model.Application.ClsAutoIDInfo objOldAutoIDInfo,Cms.Model.Application.ClsAutoIDInfo objAutoIDInfo)
		{
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			string strTranXML;
			int returnResult = 0;
			objBuilder.TableName = objOldAutoIDInfo.TableInfo.TableName;
			objBuilder.WhereClause = " WHERE AUTO_CARD_ID = " + objOldAutoIDInfo.AUTO_CARD_ID.ToString();
			string strUpdate = objBuilder.GetUpdateSQL(objOldAutoIDInfo,objAutoIDInfo,out strTranXML);

			if(strUpdate != "")
			{
				strUpdate	=	"IF Not Exists(SELECT AUTO_CARD_ID"
					+ " FROM APP_AUTO_ID_CARD_INFO "
					+ " WHERE AUTO_CARD_ID = '" + ReplaceInvalidCharecter(objAutoIDInfo.AUTO_CARD_ID.ToString()) 
					+ "' AND AUTO_CARD_ID<>" + objOldAutoIDInfo.AUTO_CARD_ID
					+ ")"
					+ strUpdate;
				DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.Text,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				try 
				{
					if(boolTransactionRequired) 
					{
						objAutoIDInfo.TransactLabel = ClsCommon.MapTransactionLabel(".aspx.resx");Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	3;
						objTransactionInfo.RECORDED_BY		=	objAutoIDInfo.MODIFIED_BY;
						objTransactionInfo.APP_ID			=	objAutoIDInfo.APP_ID;
						objTransactionInfo.APP_VERSION_ID	=	objAutoIDInfo.APP_VERSION_ID;
						objTransactionInfo.TRANS_DESC		=	"Auto id information is modified";
						objTransactionInfo.CHANGE_XML		=	strTranXML;
						returnResult = objDataWrapper.ExecuteNonQuery(strUpdate,objTransactionInfo);

					}
					else
					{
						returnResult = objDataWrapper.ExecuteNonQuery(strUpdate);
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
			else
			{
				return 0;
			}
		}



		#endregion

		/// <summary>
		/// This function is used to generate XML based on the Customer Id, Application Id and Application Version Id
		/// </summary>
		/// <param name="intCustId">Customer Id</param>
		/// <param name="intAppId">Application Id</param>
		/// <param name="intAppVersionId">Application Version Id</param>
		/// <returns></returns>
		public DataSet FillAdditionalInterestDetails(int CustomerID,int AppID,int AddVersionID,int VehicleID,out string strAutoIDXML)
		{
			string		strStoredProc	=	"Proc_GetAutoIDCardDetails";//Will be replaced with CONST
					
			DataSet dsAccounts = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID,SqlDbType.Int);
			objDataWrapper.AddParameter("@APP_ID",AppID,SqlDbType.Int);
			objDataWrapper.AddParameter("@APP_VERSION_ID",AddVersionID,SqlDbType.Int);
			objDataWrapper.AddParameter("@VEHICLE_ID",VehicleID,SqlDbType.Int);
			
			dsAccounts = objDataWrapper.ExecuteDataSet(strStoredProc);
			strAutoIDXML = dsAccounts.GetXml();
			return dsAccounts;

		}
		
		public static bool isUpdated(int CustomerID,int AppID,int AddVersionID,int VehicleID)
		{
			string		strStoredProc	=	"Proc_GetAutoIDCardUpdated";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID ",CustomerID);
			objDataWrapper.AddParameter("@APP_ID",AppID);
			objDataWrapper.AddParameter("@APP_VERSION_ID",AddVersionID);
			objDataWrapper.AddParameter("@VEHICLE_ID",VehicleID);
			int updated;
			try
			{
				updated = Convert.ToInt32(objDataWrapper.ExecuteDataSet(strStoredProc).Tables[0].Rows[0][0]);
			}
			catch
			{
				updated = 0;
			}
			if(updated == 0)
				return false;
			else
				return true;
		}
		
	}
}
