/******************************************************************************************
<Author				: -   Ajit Singh Chahal 
<Start Date				: -	6/30/2005 9:34:13 AM
<End Date				: -	
<Description				: - 	BL for check information.
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
using Cms.Model.Account;
using Cms.BusinessLayer.BlCommon;
using System.Collections;
using System.Web.UI.WebControls;

namespace Cms.BusinessLayer.BlAccount
{

	/// <summary>
	/// BL for check information.
	/// </summary>
	public class ClsChecks : Cms.BusinessLayer.BlAccount.ClsAccount
	{
		private const	string		ACT_CHECK_INFORMATION			=	"ACT_CHECK_INFORMATION";
		
		public struct CHECK_TYPES
		{
			public const string AGENCY_COMMISSION_CHECKS = "2472";
			public const string PREMIUM_REFUND_CHECKS_FOR_RETURN_PREMIUM_PAYMENT= "2474";
			public const string PREMIUM_REFUND_CHECKS_FOR_OVER_PAYMENT= "9935";
			public const string PREMIUM_REFUND_CHECKS_FOR_SUSPENSE_AMOUNT="9936";
			public const string CLAIMS_CHECKS= "9937";
			public const string VENDOR_CHECKS= "9938";
			public const string MISCELLANEOUS_OTHER_CHECKS= "9940";
			public const string REINSURANCE_PREMIUM_CHECKS= "9945";
		}
	
		public int intRetVal;
		#region Private Instance Variables
		private			bool		boolTransactionLog;
		private         bool        is_Special_Handling;   
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateACT_CHECK_INFORMATION";
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
		public bool Check_Special_handling
		{
			set
			{
				is_Special_Handling	=	value;
			}
			get
			{
				return is_Special_Handling;
			}
		}


		#endregion

		#region private Utility Functions
		#endregion

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsChecks()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		public ClsChecks(bool transactionLogRequired):this()
		{
			base.TransactionLogRequired = transactionLogRequired;
		}
		#endregion
		
		#region Add(Insert) functions
		/*public int Add(ClsChecksInfo objChecksInfo)
		{
			string		strStoredProc	=	"Proc_InsertACT_CHECK_INFORMATION";
			return Add(objChecksInfo,strStoredProc);
		}*/
		public int Add(ClsChecksInfo objChecksInfo)//,string strStoredProc)
		{
			DataWrapper objDataWrapper=null;
			try
			{
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				int retResult=Add(objChecksInfo,objDataWrapper,-1);
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return retResult;
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
		/*public int Add(ArrayList objChecksInfo,ArrayList OPEN_ITEM_ID )
		{	
			string strStoredProc	=	"Proc_InsertACT_CHECK_INFORMATION";
			return Add(objChecksInfo,OPEN_ITEM_ID,strStoredProc);
		}*/
		public int Add(ArrayList objChecksInfo,ArrayList OPEN_ITEM_ID)//,string strStoredProc)
		{
			DataWrapper objDataWrapper=null;
			try
			{
				int retResult=0,i=0;
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				foreach(ClsChecksInfo objCheck  in objChecksInfo)
				{
					//SetPayeeInfo(objCheck,objDataWrapper);
					retResult=Add(objCheck,objDataWrapper,int.Parse(OPEN_ITEM_ID[i].ToString()));
					i++;
					if(retResult<=0)
						break;
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return retResult;
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

		public int Add(ArrayList objChecksInfo)
		{
			DataWrapper objDataWrapper=null;
			try
			{
				int retResult=0,i=0;
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				foreach(ClsChecksInfo objCheck  in objChecksInfo)
				{
					//SetPayeeInfo(objCheck,objDataWrapper);
					retResult=Add(objCheck,objDataWrapper,0);
					i++;
					if(retResult<=0)
						break;
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return retResult;
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
		/*public int Add(ClsChecksInfo objChecksInfo,DataWrapper objDataWrapper,int OPEN_ITEM_ID)
		{
			string		strStoredProc	=	"Proc_InsertACT_CHECK_INFORMATION";
			return Add(objChecksInfo,objDataWrapper,OPEN_ITEM_ID,strStoredProc);
		}*/
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objChecksInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsChecksInfo objChecksInfo,DataWrapper objDataWrapper,int OPEN_ITEM_ID)//,string strStoredProc)
		{
			string		strStoredProc	=	"Proc_InsertACT_CHECK_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CHECK_TYPE",objChecksInfo.CHECK_TYPE);
			objDataWrapper.AddParameter("@SELECT_FROM",objChecksInfo.SELECT_FROM);
			objDataWrapper.AddParameter("@ACCOUNT_ID",objChecksInfo.ACCOUNT_ID);
			objDataWrapper.AddParameter("@CHECK_NUMBER",objChecksInfo.CHECK_NUMBER);
			objDataWrapper.AddParameter("@CHECK_DATE",objChecksInfo.CHECK_DATE);
			objDataWrapper.AddParameter("@CHECK_AMOUNT",objChecksInfo.CHECK_AMOUNT);
			objDataWrapper.AddParameter("@CHECK_NOTE",objChecksInfo.CHECK_NOTE);
			objDataWrapper.AddParameter("@PAYEE_ENTITY_ID",objChecksInfo.PAYEE_ENTITY_ID);
			objDataWrapper.AddParameter("@PAYEE_ENTITY_TYPE",objChecksInfo.PAYEE_ENTITY_TYPE);
			objDataWrapper.AddParameter("@PAYEE_ENTITY_NAME",objChecksInfo.PAYEE_ENTITY_NAME);
			objDataWrapper.AddParameter("@PAYEE_ADD1",objChecksInfo.PAYEE_ADD1);
			objDataWrapper.AddParameter("@PAYEE_ADD2",objChecksInfo.PAYEE_ADD2);
			objDataWrapper.AddParameter("@PAYEE_CITY",objChecksInfo.PAYEE_CITY);
			objDataWrapper.AddParameter("@PAYEE_STATE",objChecksInfo.PAYEE_STATE);
			objDataWrapper.AddParameter("@PAYEE_ZIP",objChecksInfo.PAYEE_ZIP);
			objDataWrapper.AddParameter("@PAYEE_NOTE",objChecksInfo.PAYEE_NOTE);
			objDataWrapper.AddParameter("@PAYMENT_MODE",objChecksInfo.PAYMENT_MODE);
			objDataWrapper.AddParameter("@CREATED_IN",objChecksInfo.CREATED_IN);
			objDataWrapper.AddParameter("@DIV_ID",objChecksInfo.DIV_ID);
			objDataWrapper.AddParameter("@DEPT_ID",objChecksInfo.DEPT_ID);
			objDataWrapper.AddParameter("@PC_ID",objChecksInfo.PC_ID);
			objDataWrapper.AddParameter("@IS_COMMITED",objChecksInfo.IS_COMMITED);
			if(objChecksInfo.DATE_COMMITTED == DateTime.MinValue)
				objDataWrapper.AddParameter("@DATE_COMMITTED",DBNull.Value);
			else
				objDataWrapper.AddParameter("@DATE_COMMITTED",objChecksInfo.DATE_COMMITTED);

			objDataWrapper.AddParameter("@COMMITED_BY",objChecksInfo.COMMITED_BY);
			objDataWrapper.AddParameter("@IN_RECON",objChecksInfo.IN_RECON);
			objDataWrapper.AddParameter("@AVAILABLE_BALANCE",objChecksInfo.AVAILABLE_BALANCE);
			objDataWrapper.AddParameter("@CUSTOMER_ID",objChecksInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@POLICY_ID",objChecksInfo.POLICY_ID);
			objDataWrapper.AddParameter("@POLICY_VER_TRACKING_ID",objChecksInfo.POLICY_VER_TRACKING_ID);
			objDataWrapper.AddParameter("@GL_UPDATE",objChecksInfo.GL_UPDATE);
			objDataWrapper.AddParameter("@IS_BNK_RECONCILED",objChecksInfo.IS_BNK_RECONCILED);
			objDataWrapper.AddParameter("@CHECKSIGN_1",objChecksInfo.CHECKSIGN_1);
			objDataWrapper.AddParameter("@CHECKSIGN_2",objChecksInfo.CHECKSIGN_2);
			objDataWrapper.AddParameter("@CHECK_MEMO",objChecksInfo.CHECK_MEMO);
			objDataWrapper.AddParameter("@IS_BNK_RECONCILED_VOID",objChecksInfo.IS_BNK_RECONCILED_VOID);
			objDataWrapper.AddParameter("@IN_BNK_RECON",objChecksInfo.IN_BNK_RECON);
			objDataWrapper.AddParameter("@SPOOL_STATUS",objChecksInfo.SPOOL_STATUS);
			objDataWrapper.AddParameter("@MANUAL_CHECK",objChecksInfo.MANUAL_CHECK);
			objDataWrapper.AddParameter("@TRAN_TYPE",objChecksInfo.TRAN_TYPE);
			objDataWrapper.AddParameter("@IS_DISPLAY_ON_STUB",objChecksInfo.IS_DISPLAY_ON_STUB);
			objDataWrapper.AddParameter("@OPEN_ITEM_ID",OPEN_ITEM_ID);
			
			objDataWrapper.AddParameter("@OPEN_ITEM_LIST",objChecksInfo.OPEN_ITEM_ROW_IDS);

			objDataWrapper.AddParameter("@IS_ACTIVE",objChecksInfo.IS_ACTIVE);
			objDataWrapper.AddParameter("@CREATED_BY",objChecksInfo.CREATED_BY);
			objDataWrapper.AddParameter("@CREATED_DATETIME",objChecksInfo.CREATED_DATETIME);
			objDataWrapper.AddParameter("@MODifIED_BY",objChecksInfo.MODIFIED_BY);
			objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objChecksInfo.LAST_UPDATED_DATETIME);
			SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@CHECK_ID",objChecksInfo.CHECK_ID,SqlDbType.Int,ParameterDirection.Output);

			int returnResult = 0;
			if(TransactionLogRequired)
			{
				objChecksInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/aspx/AddCheck.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objChecksInfo);
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				objTransactionInfo.RECORDED_BY		=	objChecksInfo.CREATED_BY;
                objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1659", "");// "Check(s) Has Been Created Successfully";				
				objTransactionInfo.CHANGE_XML		=	strTranXML;
				//Executing the query
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
			}
			else
			{
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
			}
			int CHECK_ID = int.Parse(objSqlParameter.Value.ToString());
			objDataWrapper.ClearParameteres();
					
			if (CHECK_ID == -1)//check number already assigned to another check.
			{
				return -1;
			}
			else if (CHECK_ID == -2)//check number exceeds the max limit.
			{
					
				return -2;
			}
			else
			{
				objChecksInfo.CHECK_ID = CHECK_ID;
				return returnResult;
			}
				
				
		}

		


		/// <summary>
		///  Save the Agency commission check info
		/// </summary>
		/// <param name="agencyID"></param>
		/// <param name="month"></param>
		/// <param name="year"></param>
		/// <param name="amount"></param>
		/// <returns></returns>
		public int Save(int agencyID,int month,int year,double amount,out int checkID)
		{
			string		strStoredProc	=	"Proc_Insert_Agency_Commission_Check";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,System.Data.CommandType.StoredProcedure);
			try
			{
				if(agencyID!=0)
					objDataWrapper.AddParameter("@AGENCY_ID",agencyID);
				else
					objDataWrapper.AddParameter("@AGENCY_ID",DBNull.Value);
				
				if(month!=0)
					objDataWrapper.AddParameter("@MONTH",month);
				else
					objDataWrapper.AddParameter("@MONTH",DBNull.Value);

				if(year!=0)
					objDataWrapper.AddParameter("@YEAR",year);
				else
					objDataWrapper.AddParameter("@YEAR",DBNull.Value);

				
				if(amount!=0.0)
					objDataWrapper.AddParameter("@AMOUNT",amount);
				else
					objDataWrapper.AddParameter("@AMOUNT",DBNull.Value);	
				
				objDataWrapper.AddParameter("@CHECK_TYPE",CHECK_TYPES.AGENCY_COMMISSION_CHECKS.ToString());
				objDataWrapper.AddParameter("@CREATED_DATETIME",DateTime.Now);	
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@CHECK_ID",0,SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlParameter2  = (SqlParameter) objDataWrapper.AddParameter("@RETURN_VALUE",0,SqlDbType.Int,ParameterDirection.ReturnValue);

				int returnResult = 0;
				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				
				returnResult = int.Parse(objSqlParameter2.Value.ToString());

				if(objSqlParameter2.Value!=null && int.Parse(objSqlParameter2.Value.ToString())>0)
					returnResult= int.Parse(objSqlParameter2.Value.ToString());				

				if(objSqlParameter.Value!=null && int.Parse(objSqlParameter.Value.ToString())>0)
					checkID= int.Parse(objSqlParameter.Value.ToString());
				else
					checkID=0;
					
				return returnResult;
				
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

		#region FetchCheck Info: Added On 
		public DataSet FetchAgencyChecks(int month,int year,string agencyName,string commType)
		{
			DataSet ds = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
				objDataWrapper.AddParameter("@MONTH", month, SqlDbType.Int);
				objDataWrapper.AddParameter("@YEAR", year, SqlDbType.Int);
				objDataWrapper.AddParameter("@AGENCY_NAME", agencyName, SqlDbType.VarChar);
				objDataWrapper.AddParameter("@COMM_TYPE", commType, SqlDbType.VarChar);
				ds = objDataWrapper.ExecuteDataSet("Proc_FetchAgencyChecksPayments");
				
				if (ds.Tables[0].Rows.Count > 0)				
					return ds;
				else
					return null;
				
				//ds.Dispose();
			}
			catch(Exception objEx)
			{
				throw(objEx);				
			}
			finally
			{
				if(objDataWrapper!=null)
					objDataWrapper.Dispose(); 			
			}

		}
		public DataSet FetchTempAgencyChecks(int month,int year,int AgenID,string commType)
		{
			DataSet ds = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
				if(month == 0)
					objDataWrapper.AddParameter("@MONTH", null, SqlDbType.Int);
				else
					objDataWrapper.AddParameter("@MONTH", month, SqlDbType.Int);

				if(year == 0)
					objDataWrapper.AddParameter("@YEAR", null, SqlDbType.Int);
				else
					objDataWrapper.AddParameter("@YEAR", year, SqlDbType.Int);
				objDataWrapper.AddParameter("@AGENCY_ID", AgenID, SqlDbType.Int);
				objDataWrapper.AddParameter("@COMM_TYPE", commType, SqlDbType.VarChar);
						

				ds = objDataWrapper.ExecuteDataSet("Proc_FetchAgencyChecksPayments");

				return ds;				
				//				if (ds.Tables[0].Rows.Count > 0)				
				//					return ds;
				//				else
				//					return null;
				
				
			}
			catch(Exception objEx)
			{
				throw(objEx);				
			}
			finally
			{
				if(objDataWrapper!=null)
					objDataWrapper.Dispose(); 			
			}

		}

		#endregion

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldChecksInfo">Model object having old information</param>
		/// <param name="objChecksInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 || 0)</returns>
		public int Update(ClsChecksInfo objOldChecksInfo,ClsChecksInfo objChecksInfo)
		{
			
			string		strStoredProc	=	"Proc_UpdateACT_CHECK_INFORMATION";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CHECK_ID",objChecksInfo.CHECK_ID);
				objDataWrapper.AddParameter("@CHECK_TYPE",objChecksInfo.CHECK_TYPE);
				//objDataWrapper.AddParameter("@SELECT_FROM",objChecksInfo.SELECT_FROM);
				objDataWrapper.AddParameter("@ACCOUNT_ID",objChecksInfo.ACCOUNT_ID);
					
				objDataWrapper.AddParameter("@CHECK_NUMBER",objChecksInfo.CHECK_NUMBER);
				//objDataWrapper.AddParameter("@CHECK_DATE",objChecksInfo.CHECK_DATE);
				objDataWrapper.AddParameter("@CHECK_AMOUNT",objChecksInfo.CHECK_AMOUNT);
				objDataWrapper.AddParameter("@CHECK_NOTE",objChecksInfo.CHECK_NOTE);
				objDataWrapper.AddParameter("@PAYEE_ENTITY_ID",objChecksInfo.PAYEE_ENTITY_ID);
				objDataWrapper.AddParameter("@PAYEE_ENTITY_TYPE",objChecksInfo.PAYEE_ENTITY_TYPE);
				objDataWrapper.AddParameter("@PAYEE_ENTITY_NAME",objChecksInfo.PAYEE_ENTITY_NAME);
				objDataWrapper.AddParameter("@PAYEE_ADD1",objChecksInfo.PAYEE_ADD1);
				objDataWrapper.AddParameter("@PAYEE_ADD2",objChecksInfo.PAYEE_ADD2);
				objDataWrapper.AddParameter("@PAYEE_CITY",objChecksInfo.PAYEE_CITY);
				objDataWrapper.AddParameter("@PAYEE_STATE",objChecksInfo.PAYEE_STATE);
				objDataWrapper.AddParameter("@PAYEE_ZIP",objChecksInfo.PAYEE_ZIP);
				/*objDataWrapper.AddParameter("@PAYEE_NOTE",objChecksInfo.PAYEE_NOTE);
					objDataWrapper.AddParameter("@CREATED_IN",objChecksInfo.CREATED_IN);
					objDataWrapper.AddParameter("@DIV_ID",objChecksInfo.DIV_ID);
					objDataWrapper.AddParameter("@DEPT_ID",objChecksInfo.DEPT_ID);
					objDataWrapper.AddParameter("@PC_ID",objChecksInfo.PC_ID);
					objDataWrapper.AddParameter("@IS_COMMITED",objChecksInfo.IS_COMMITED);
					if(objChecksInfo.DATE_COMMITTED == DateTime.MinValue)
						objDataWrapper.AddParameter("@DATE_COMMITTED",DBNull.Value);
					else
						objDataWrapper.AddParameter("@DATE_COMMITTED",objChecksInfo.DATE_COMMITTED);
					objDataWrapper.AddParameter("@COMMITED_BY",objChecksInfo.COMMITED_BY);
					objDataWrapper.AddParameter("@IN_RECON",objChecksInfo.IN_RECON);
					objDataWrapper.AddParameter("@AVAILABLE_BALANCE",objChecksInfo.AVAILABLE_BALANCE);*/
				objDataWrapper.AddParameter("@CUSTOMER_ID",objChecksInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objChecksInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VER_TRACKING_ID",objChecksInfo.POLICY_VER_TRACKING_ID);
				/*objDataWrapper.AddParameter("@GL_UPDATE",objChecksInfo.GL_UPDATE);
					objDataWrapper.AddParameter("@IS_BNK_RECONCILED",objChecksInfo.IS_BNK_RECONCILED);*/
				objDataWrapper.AddParameter("@CHECKSIGN_1",objChecksInfo.CHECKSIGN_1);
				objDataWrapper.AddParameter("@CHECKSIGN_2",objChecksInfo.CHECKSIGN_2);
				objDataWrapper.AddParameter("@CHECK_MEMO",objChecksInfo.CHECK_MEMO);
				/*objDataWrapper.AddParameter("@IS_BNK_RECONCILED_VOID",objChecksInfo.IS_BNK_RECONCILED_VOID);
					objDataWrapper.AddParameter("@IN_BNK_RECON",objChecksInfo.IN_BNK_RECON);
					objDataWrapper.AddParameter("@SPOOL_STATUS",objChecksInfo.SPOOL_STATUS);*/
				objDataWrapper.AddParameter("@TRAN_TYPE",objChecksInfo.TRAN_TYPE);
				objDataWrapper.AddParameter("@IS_DISPLAY_ON_STUB",objChecksInfo.IS_DISPLAY_ON_STUB);
				objDataWrapper.AddParameter("@MODifIED_BY",objChecksInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objChecksInfo.LAST_UPDATED_DATETIME);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ERROR_STATUS",objChecksInfo.CHECK_ID,SqlDbType.Int,ParameterDirection.Output);
				if(base.TransactionLogRequired) 
				{
					objChecksInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/aspx/AddCheck.aspx.resx");
					objBuilder.GetUpdateSQL(objOldChecksInfo,objChecksInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objChecksInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1660", "");// "Check Has Been Updated Successfully";
					objTransactionInfo.CUSTOM_INFO		=   "Payee Name: " + objChecksInfo.PAYEE_ENTITY_NAME +";Check Type: " + GetCheckTypeOnId(objChecksInfo.CHECK_ID);
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				objDataWrapper.ClearParameteres();

				int errorStatus = int.Parse(objSqlParameter.Value.ToString());
				if (errorStatus == -1)//check number already assigned to another check.
				{
					return -1;
				}
				else if (errorStatus == -2)//check number exceeds the max limit.
				{
					
					return -2;
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

		/// <summary>
		///  Save the Agency commission check info
		/// </summary>
		/// <param name="agencyID"></param>
		/// <param name="month"></param>
		/// <param name="year"></param>
		/// <param name="amount"></param>
		/// <returns></returns>
		public int Update(int agencyID,int month,int year,double amount, int checkID)
		{
			string		strStoredProc	=	"Proc_Update_Agency_Commission_Check";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,System.Data.CommandType.StoredProcedure);
			try
			{
				if(agencyID!=0)
					objDataWrapper.AddParameter("@AGENCY_ID",agencyID);
				else
					objDataWrapper.AddParameter("@AGENCY_ID",DBNull.Value);
				
				if(month!=0)
					objDataWrapper.AddParameter("@MONTH",month);
				else
					objDataWrapper.AddParameter("@MONTH",DBNull.Value);

				if(year!=0)
					objDataWrapper.AddParameter("@YEAR",year);
				else
					objDataWrapper.AddParameter("@YEAR",DBNull.Value);

				
				if(amount!=0.0)
					objDataWrapper.AddParameter("@AMOUNT",amount);
				else
					objDataWrapper.AddParameter("@AMOUNT",DBNull.Value);	
				
				objDataWrapper.AddParameter("@CHECK_TYPE",CHECK_TYPES.AGENCY_COMMISSION_CHECKS.ToString());
				objDataWrapper.AddParameter("@CREATED_DATETIME",DateTime.Now);	
				
				objDataWrapper.AddParameter("@CHECK_ID",checkID);
				SqlParameter objSqlParameter2  = (SqlParameter) objDataWrapper.AddParameter("@RETURN_VALUE",0,SqlDbType.Int,ParameterDirection.ReturnValue);

				int returnResult = 0;
				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				
				returnResult = int.Parse(objSqlParameter2.Value.ToString());

				if(objSqlParameter2.Value!=null && int.Parse(objSqlParameter2.Value.ToString())>0)
					returnResult= int.Parse(objSqlParameter2.Value.ToString());				

				//				if(objSqlParameter.Value!=null && int.Parse(objSqlParameter.Value.ToString())<=0)
				//					checkID= int.Parse(objSqlParameter.Value.ToString());
				//				else
				//					checkID=0;
					
				return 1;
				
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

		#region "Delete"
		/// <summary>
		/// Delete the whole record of spefied id 
		/// </summary>
		/// <returns>Nos of rows deleted</returns>
		public int Delete(string CHECK_ID,int userID)
		{
			try
			{
				/*Calling the stored procedure to get the maximum Journal entry no*/
				/*(String strStoredProc = "Proc_Delete_ACT_CHECK_INFORMATION";
				int Value;

				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
				objDataWrapper.AddParameter("@CHECK_ID",CHECK_ID);
				
				Value = objDataWrapper.ExecuteNonQuery(strStoredProc);

				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();

				return Value ;*/
				String strStoredProc = "Proc_Delete_ACT_CHECK_INFORMATION";
				int Value;
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CHECK_ID",CHECK_ID);
				SqlParameter objRetVal = (SqlParameter) objDataWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);
				if(TransactionLogRequired) 
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					if (userID!= 0)
						objTransactionInfo.RECORDED_BY		=  	userID;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1661", "");// "Check Has Been Deleted Successfully.";

					ClsDistributeCashReceipt obj = new ClsDistributeCashReceipt();
					objTransactionInfo.CUSTOM_INFO		=	"Payee Name :" + obj.FetchPayeeNameCheck(int.Parse(CHECK_ID)) +";Check Type:" + GetCheckTypeOnId(int.Parse(CHECK_ID)) ;

					Value = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
				}
				else
					Value = objDataWrapper.ExecuteNonQuery(strStoredProc);

				Value = int.Parse(objRetVal.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();

				return Value ;
			}
			catch (Exception objEx)
			{
				throw(objEx);
			}
		}
		#endregion



		public DataSet FetchCheckInformationOnId(int CHECK_ID)
		{
			string	strStoredProc =	"Proc_FetchCheckInformationOnId";
			
			try
			{
				DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
				objWrapper.AddParameter("@CHECK_ID",CHECK_ID);
				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

				return ds ;
			}
			catch
			{
				return null;
			}	
		}

		public DataSet FetchCheckInformationOnId(int CHECK_ID , DataWrapper objWrapper)
		{
			string	strStoredProc =	"Proc_FetchCheckInformationOnId";
			
			try
			{
				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@CHECK_ID",CHECK_ID);
				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

				objWrapper.ClearParameteres();

				return ds ;
			}
			catch
			{
				return null;
			}	
		}

		#region "commit"

		public int Commit(ArrayList al,out ArrayList alStatus)
		{
			//call from index page for multiple checks cmomit
			DataWrapper objWrapper;
			objWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure, 
				DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
			

			bool SaveStatus = true;

			
			alStatus = new ArrayList();
			try
			{
				for(int i = 0; i < al.Count; i++ )
				{
					
					intRetVal = Commit((ClsChecksInfo)al[i],objWrapper);						
					objWrapper.ClearParameteres();
					alStatus.Add(intRetVal);
					if (intRetVal <= 0 )
					{
						//Some error occured, hence updating the save status flag
						SaveStatus = false;
						if(intRetVal == -10)
							Check_Special_handling = true;
					}
				}
				if (SaveStatus == false)
				{
					//Some error occured while saving deposit details, hence rollbacking
					objWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
					//intRetVal change For Itrack Issue #5640.
					//return intRetVal;
					intRetVal = -4;
				}							
				objWrapper.CommitTransaction(DataLayer.DataWrapper.CloseConnection.YES);

				return intRetVal;
			}
			catch (Exception objExp)
			{
				objWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				throw objExp;
			}
			finally
			{
				objWrapper.Dispose();
			}
		}



		/// <summary>
		/// called from index page
		/// </summary>
		/// <param name="objChecksInfo"></param>
		/// <param name="objDataWrapper"></param>
		/// <returns></returns>
		public int Commit(ClsChecksInfo objChecksInfo, DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_CommitACT_CHECK_INFORMATION";

			
			//Fetch Check Information  
			DataSet FetchCheckInformation = FetchCheckInformationOnId(objChecksInfo.CHECK_ID,objDataWrapper);
			
			string CHECK_TYPE = "";
			string PAYEE_ENTITY_NAME = "" ;
			string CHECK_AMOUNT = "";
			string CHECK_DATE = "" ;
			int CUSTOMER_ID = 0,POLICY_ID=0,POLICY_VERSION_ID=0;
			int CHECK_TYPE_ID=0;
			int EFT = 0;

			if(FetchCheckInformation.Tables[0].Rows.Count > 0)
			{
				CHECK_TYPE	=	FetchCheckInformation.Tables[0].Rows[0]["LOOKUP_VALUE_DESC"].ToString();
				PAYEE_ENTITY_NAME	=	FetchCheckInformation.Tables[0].Rows[0]["PAYEE_ENTITY_NAME"].ToString();
				CHECK_AMOUNT	=	FetchCheckInformation.Tables[0].Rows[0]["CHECK_AMOUNT_FORMATTED"].ToString();
				CHECK_DATE	=	FetchCheckInformation.Tables[0].Rows[0]["CHECK_DATE"].ToString();
				
				/*//Added Transaction Log in Customer Assitant Incase of OP and refund Checks:
				 *	2474 Checks for Cancellation and Change Premium Payment
					9935 Premium Refund Checks for Over Payment
					9936 Premium Refund Checks for Suspense Amount*/
				EFT = Convert.ToInt32(FetchCheckInformation.Tables[0].Rows[0]["PAYMENT_MODE"].ToString());
				CHECK_TYPE_ID = Convert.ToInt32(FetchCheckInformation.Tables[0].Rows[0]["CHECK_TYPE"].ToString());
				if(CHECK_TYPE_ID == int.Parse(CHECK_TYPES.PREMIUM_REFUND_CHECKS_FOR_RETURN_PREMIUM_PAYMENT) 
					|| CHECK_TYPE_ID == int.Parse(CHECK_TYPES.PREMIUM_REFUND_CHECKS_FOR_OVER_PAYMENT)
					|| CHECK_TYPE_ID == int.Parse(CHECK_TYPES.PREMIUM_REFUND_CHECKS_FOR_SUSPENSE_AMOUNT) )
				{
					CUSTOMER_ID =  Convert.ToInt32(FetchCheckInformation.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
					POLICY_ID	= Convert.ToInt32(FetchCheckInformation.Tables[0].Rows[0]["POLICY_ID"].ToString());
					POLICY_VERSION_ID = Convert.ToInt32(FetchCheckInformation.Tables[0].Rows[0]["POLICY_VER_TRACKING_ID"].ToString());
				}
			}	
			//end  Fetch Check Information

			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			
			try 
			{
				objDataWrapper.AddParameter("@CHECK_ID",objChecksInfo.CHECK_ID);
				
				objDataWrapper.AddParameter("@DIV_ID",ConfigurationManager.AppSettings.Get("DIV_ID"));
                objDataWrapper.AddParameter("@DEPT_ID", ConfigurationManager.AppSettings.Get("DEPT_ID"));
                objDataWrapper.AddParameter("@PC_ID", ConfigurationManager.AppSettings.Get("PC_ID"));

				objDataWrapper.AddParameter("@IS_COMMITED",objChecksInfo.IS_COMMITED);
				objDataWrapper.AddParameter("@DATE_COMMITTED",objChecksInfo.DATE_COMMITTED);

				objDataWrapper.AddParameter("@MODifIED_BY",objChecksInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objChecksInfo.LAST_UPDATED_DATETIME);
				SqlParameter objSqlRetParameter  = (SqlParameter) objDataWrapper.AddParameter("@RetVal",objChecksInfo.CHECK_ID,SqlDbType.Int,ParameterDirection.ReturnValue);

				if(base.TransactionLogRequired) 
				{
					objChecksInfo.TransactLabel = ClsCommon.MapTransactionLabel("Account/aspx/AddCheck.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objChecksInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.CLIENT_ID		=	CUSTOMER_ID;
					objTransactionInfo.POLICY_ID		=	POLICY_ID;
					objTransactionInfo.POLICY_VER_TRACKING_ID		=	POLICY_VERSION_ID;
					objTransactionInfo.RECORDED_BY		=	objChecksInfo.MODIFIED_BY ;

					if(EFT == 11788)
						objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1662","");//"Check Has Been Committed (EFT payment).";
					else
                        objTransactionInfo.TRANS_DESC   =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1663", "");// "Check Has Been Committed";

					objTransactionInfo.CUSTOM_INFO		=	"Check Type :" + CHECK_TYPE + ";Payee Name : " + PAYEE_ENTITY_NAME + ";Check Amount : " + CHECK_AMOUNT + ";Check Date :" + DateTime.Parse(CHECK_DATE) ;
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				if(objSqlRetParameter.Value!=null && int.Parse(objSqlRetParameter.Value.ToString())<=0)
					return int.Parse(objSqlRetParameter.Value.ToString());
				else
					return returnResult;
				
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}

		// called from Detail page
		public int Commit(ClsChecksInfo objChecksInfo)
		{
			string		strStoredProc	=	"Proc_CommitACT_CHECK_INFORMATION";
			//string strTranXML;
			//int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try 
			{
				int RetVal = Commit(objChecksInfo, objDataWrapper);

				if (RetVal > 0)
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return RetVal;

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
			
		#region void check"
		public int VoidCheck(ArrayList al,out ArrayList alStatus)
		{
			//call from index page for multiple checks cmomit
			DataWrapper objWrapper;
			objWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure, 
				DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
			

			bool SaveStatus = true;

			int intRetVal;
			alStatus = new ArrayList();
			try
			{
				for(int i = 0; i < al.Count; i++ )
				{
					
					/*********Added by Kranti/Mohit Agarwal 17-May 2007 **/
					string checkXML;
					ClsChecksInfo objtempCheck;
					objtempCheck = (ClsChecksInfo)al[i];
					string ACCOUNT_ID="";
					if(objtempCheck.CHECK_ID!=0 && objtempCheck.CHECK_ID.ToString().Length>0)
					{
						checkXML=ClsChecks.GetXmlForEditPageControls(objtempCheck.CHECK_ID.ToString(),ref ACCOUNT_ID);

						try
						{
							objtempCheck.PAYEE_ENTITY_NAME = ClsCommon.FetchValueFromXML("PAYEE_ENTITY_ID",checkXML);
							objtempCheck.CHECK_AMOUNT = double.Parse(ClsCommon.FetchValueFromXML("CHECK_AMOUNT",checkXML));
							objtempCheck.CHECK_DATE = DateTime.Parse(ClsCommon.FetchValueFromXML("CHECK_DATE",checkXML));
							objtempCheck.CHECK_NUMBER = ClsCommon.FetchValueFromXML("CHECK_NUMBER",checkXML);
						}
						catch
						{
						}
					}
					/* ************************/
					//					intRetVal = VoidCheck((ClsChecksInfo)al[i],objWrapper);						
					intRetVal = VoidCheck(objtempCheck,objWrapper);						
					objWrapper.ClearParameteres();
					alStatus.Add(intRetVal);
					if (intRetVal <= 0 )
					{
						//Some error occured, hence updating the save status flag
						SaveStatus = false;
					}
				}
				if (SaveStatus == false)
				{
					//Some error occured while saving deposit details, hence rollbacking
					objWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
					return -1;
				}							
				objWrapper.CommitTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				return 1;
			}
			catch (Exception objExp)
			{
				objWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				throw objExp;
			}
			finally
			{
				objWrapper.Dispose();
			}
		}
		/// <summary>
		/// Get Check Type On Check Id
		/// </summary>
		/// <param name="depositId"></param>
		/// <returns></returns>
		public string GetCheckTypeOnId(int checkId)
		{
			string	strStoredProc =	"Proc_FetchCheckTypeOnId";

			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CHECK_ID",checkId);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			try
			{
				return ds.Tables[0].Rows[0]["LOOKUP_VALUE_DESC"].ToString();
			}
			catch
			{
				return "";
			}
		}
		/// <summary>
		/// Get Check TEMP CHECK for TL : On Check Id
		/// </summary>
		/// <param name="depositId"></param>
		/// <returns></returns>
		public string GetTempCheckType(int checkId)
		{
			string	strStoredProc =	"Proc_FetchTempCheckType";

			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CHECK_ID",checkId);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			try
			{
				return ds.Tables[0].Rows[0]["LOOKUP_VALUE_DESC"].ToString();
			}
			catch
			{
				return "";
			}
		}

		//Itrack Issue 4733 
		public DataSet GetCustomerId(int check_Id)
		{
		
			string strStoreProc = "Proc_Get_Customer_Id";

			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);
			objWrapper.AddParameter("@CHECK_ID",check_Id);
			DataSet ds = objWrapper.ExecuteDataSet(strStoreProc);  	
			return ds;

		    
		}


		public int VoidCheck(ClsChecksInfo objChecksInfo,DataWrapper objDataWrapper)
		{
				 			
			string		strStoredProc	=	"Proc_VoidACT_CHECK_INFORMATION";
			
			//Fetch Check Information  
			DataSet FetchCheckInformation = FetchCheckInformationOnId(objChecksInfo.CHECK_ID);
			
			string CHECK_TYPE = "";			

			if(FetchCheckInformation.Tables[0].Rows.Count > 0)
			{
				CHECK_TYPE	=	FetchCheckInformation.Tables[0].Rows[0]["LOOKUP_VALUE_DESC"].ToString();
			}	
			//end  Fetch Check Information

			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CHECK_ID",objChecksInfo.CHECK_ID);
				
				objDataWrapper.AddParameter("@DIV_ID",ConfigurationManager.AppSettings.Get("DIV_ID"));
                objDataWrapper.AddParameter("@DEPT_ID", ConfigurationManager.AppSettings.Get("DEPT_ID"));
                objDataWrapper.AddParameter("@PC_ID", ConfigurationManager.AppSettings.Get("PC_ID"));

				objDataWrapper.AddParameter("@MODifIED_BY",objChecksInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objChecksInfo.LAST_UPDATED_DATETIME);
				SqlParameter objSqlRetParameter  = (SqlParameter) objDataWrapper.AddParameter("@RetVal",null,SqlDbType.Int,ParameterDirection.ReturnValue);

				if(base.TransactionLogRequired) 
				{
					

					objChecksInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/aspx/AddCheck.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objChecksInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;

					//Itrack Issue 4733 
					DataSet ds = null;
					ds = GetCustomerId(objChecksInfo.CHECK_ID);
					if(ds.Tables[0].Rows.Count > 0)
					{
						objTransactionInfo.CLIENT_ID		=	 int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
						objTransactionInfo.POLICY_ID        =	 int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
						objTransactionInfo.POLICY_VER_TRACKING_ID = int.Parse(ds.Tables[0].Rows[0]["POLICY_VER_TRACKING_ID"].ToString());
					}					
	
					objTransactionInfo.RECORDED_BY		=	objChecksInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1664", "");// "Check Has Been Voided";
					objTransactionInfo.CUSTOM_INFO		=	"Check Type :" + CHECK_TYPE ;

					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc); 
				}
				if(objSqlRetParameter.Value!=null && int.Parse(objSqlRetParameter.Value.ToString())<=0)
					return int.Parse(objSqlRetParameter.Value.ToString());
				else
				{
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					return returnResult;
				}
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
		public int VoidCheck(ClsChecksInfo objChecksInfo)
		{
				 			
			string		strStoredProc	=	"Proc_VoidACT_CHECK_INFORMATION";

			//Fetch Check Information  
			DataSet FetchCheckInformation = FetchCheckInformationOnId(objChecksInfo.CHECK_ID);
			
			string CHECK_TYPE = "";

			if(FetchCheckInformation.Tables[0].Rows.Count > 0)
			{
				CHECK_TYPE	=	FetchCheckInformation.Tables[0].Rows[0]["LOOKUP_VALUE_DESC"].ToString();
			}	
			//end  Fetch Check Information

			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CHECK_ID",objChecksInfo.CHECK_ID);

                objDataWrapper.AddParameter("@DIV_ID", ConfigurationManager.AppSettings.Get("DIV_ID"));
                objDataWrapper.AddParameter("@DEPT_ID", ConfigurationManager.AppSettings.Get("DEPT_ID"));
                objDataWrapper.AddParameter("@PC_ID", ConfigurationManager.AppSettings.Get("PC_ID"));

				objDataWrapper.AddParameter("@MODifIED_BY",objChecksInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objChecksInfo.LAST_UPDATED_DATETIME);
				SqlParameter objSqlRetParameter  = (SqlParameter) objDataWrapper.AddParameter("@RetVal",null,SqlDbType.Int,ParameterDirection.ReturnValue);

				if(base.TransactionLogRequired) 
				{
					objChecksInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/aspx/AddCheck.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objChecksInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objChecksInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1664", "");//"Check Has Been Voided";
					objTransactionInfo.CUSTOM_INFO		=	"Check Type :" + CHECK_TYPE ;
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				if(objSqlRetParameter.Value!=null && int.Parse(objSqlRetParameter.Value.ToString())<=0)
					return int.Parse(objSqlRetParameter.Value.ToString());
				else
				{
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					return returnResult;
				}
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

		#region Agency Distribution
		public int  ExecAgencyDistribution(int Check_ID,int Account_ID,string strDesc)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter("@CHECK_ID", Check_ID);
				objWrapper.AddParameter("@ACCOUNT_ID",Account_ID);
				objWrapper.AddParameter("@DESCRIPTION",strDesc);
				return objWrapper.ExecuteNonQuery("Proc_DistributeAgencyCheck");

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in Exec_Postcustomer_transaction\n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}
		
		}

		public DataSet GetAgencyCommDistributionInfo(int Check_ID)
		{
			try
			{
				DataSet dsTemp = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CHECK_ID",Check_ID,SqlDbType.Int );
				dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetAgencyCommDistributionInfo");
				return dsTemp;
			}
			catch(Exception exc)
			{throw(exc);}
			finally
			{}
		
		}
		#endregion
		#region Commit On All Pages
		public int  ExecCommitAllChecks(string isCommited,int userID,int div_ID,int dept_ID,int pc_ID)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
				
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				  
				objWrapper.AddParameter("@IS_COMMITED", isCommited);
				objWrapper.AddParameter("@DATE_COMMITTED", DateTime.Now);
				objWrapper.AddParameter("@MODIFIED_BY ", userID);
				objWrapper.AddParameter("@LAST_UPDATED_DATETIME", DateTime.Now);
				objWrapper.AddParameter("@DIV_ID", div_ID);
				objWrapper.AddParameter("@DEPT_ID", dept_ID);
				objWrapper.AddParameter("@PC_ID", pc_ID);
                
				return objWrapper.ExecuteNonQuery("Proc_CommitAllChecks");

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in ExecCommitAllChecks\n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}
		
		}

		#endregion

		#region "Misc"
		/// <summary>
		/// To get status of check.
		/// </summary>
		/// <param name="INVOICE_ID"></param>
		/// <returns>status of invoice </returns>
		/// N-Not distributed, 
		/// D-Distributed, 
		/// C-Committed
		public static string GetCheckStatus(string CHECK_ID)
		{
			string strSql = "Proc_GetStatus_ACT_CHECK_INFORMATION";
			SqlParameter[] param = new SqlParameter[1];
			param[0] = new SqlParameter("@CHECK_ID",CHECK_ID);
			string status=DataWrapper.ExecuteScalar(ConnStr,CommandType.StoredProcedure,strSql,param).ToString();
			return status;		
		}

		public static DataSet GetChecksForPrint(DateTime fromDate,DateTime toDate,int checkType)
		{
			string strSql = "Proc_GetChecksForPrint";
								 
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@fromDate",fromDate);
			objDataWrapper.AddParameter("@toDate",toDate);
			objDataWrapper.AddParameter("@checkType",checkType);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}
		public static DataSet GetChecksForPrintToPDF(int AccountId)
		{
			string strSql = "Proc_GetChecksForPrintToPDF";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@ACCOUNT_ID",AccountId);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}
		
		public static DataSet GetBankAccountInformationForChecks(int AccountId)
		{
			string strSql = "Proc_GetBankAccountInformationForChecks";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@ACCOUNT_ID",AccountId);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}
		#endregion
		#region "GetxmlMethods"
		public static string GetXmlForEditPageControls(string CHECK_ID,ref string ACCOUNT_ID)
		{
			string strSql = "Proc_GetXML_ACT_CHECK_INFORMATION";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CHECK_ID",CHECK_ID);
			SqlParameter objRetVal = (SqlParameter) objDataWrapper.AddParameter("@ACCOUNT_ID",ACCOUNT_ID,SqlDbType.Int,ParameterDirection.Output);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			ACCOUNT_ID = objRetVal.Value.ToString();
			return objDataSet.GetXml();
		}
		public static string GetXmlForEditPageControls(string CHECK_ID)
		{
			string ACCOUNT_ID="";
			return GetXmlForEditPageControls(CHECK_ID,ref ACCOUNT_ID);
		}
		#endregion
			
		#region "Automation of check process code"
		public static DataSet GetOpenItemsForCheck(string fromDate,string toDate,string ITEM_STATUS,string polNum)
		{
			string strSql = "Proc_GetOpenItemsForCheck";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			if(fromDate!=null && fromDate.Length<=0)
				objDataWrapper.AddParameter("@fromDate",DBNull.Value);
			else
				objDataWrapper.AddParameter("@fromDate",fromDate);
			if(toDate!=null && toDate.Length<=0)
				objDataWrapper.AddParameter("@toDate",DBNull.Value);
			else
				objDataWrapper.AddParameter("@toDate",toDate);
			objDataWrapper.AddParameter("@ITEM_STATUS",ITEM_STATUS);
			objDataWrapper.AddParameter("@POLICY_NUMBER",polNum);

			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}

		public static DataSet GetOpenItemsForCheckWithoutStatus(string fromDate,string toDate,string fromPayDate, string toPayDate,string polNum, string isOverPay)
		{
			DataSet objDataSet = null;
			DataWrapper objDataWrapper = null;
			try
			{
				//string strSql = "Proc_GetOpenItemsForCheck";
				string strSql = "Proc_GetOpenItemsForCheck_WithoutStatus";
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
				if(fromDate!=null && fromDate.Length<=0)
					objDataWrapper.AddParameter("@FromEffDate",DBNull.Value);
				else
					objDataWrapper.AddParameter("@FromEffDate",fromDate);
				if(toDate!=null && toDate.Length<=0)
					objDataWrapper.AddParameter("@ToEffDate",DBNull.Value);
				else
					objDataWrapper.AddParameter("@ToEffDate",toDate);
				//objDataWrapper.AddParameter("@ITEM_STATUS",ITEM_STATUS);
				if(fromPayDate != null && fromPayDate.Length<=0)
					objDataWrapper.AddParameter("@FromPayDate",DBNull.Value);
				else
					objDataWrapper.AddParameter("@FromPayDate", fromPayDate);
				if(toPayDate != null && toPayDate.Length<=0)
					objDataWrapper.AddParameter("@ToPayDate", DBNull.Value);
				else
					objDataWrapper.AddParameter("@ToPayDate", toPayDate);
				objDataWrapper.AddParameter("@POLICY_NUMBER",polNum);
				objDataWrapper.AddParameter("@IsOverPay",isOverPay);
				
				objDataSet = objDataWrapper.ExecuteDataSet(strSql);
				return objDataSet;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objDataSet!=null)
					objDataSet.Dispose();

				if(objDataWrapper!=null)
					objDataWrapper.Dispose();

			}
			

		}


		/*private void SetPayeeInfo(ClsChecksInfo objcheck,DataWrapper objDataWrapper)
		{
			//DataWrapper objDataWrapper=null;
			try
			{
				string strSql = "Proc_GetPayeeInfoForCheck";
				//objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
				switch(objcheck.CHECK_TYPE)
				{			
					case CHECK_TYPES.AGENCY_COMMISSION_CHECKS:
						break;
					case CHECK_TYPES.CLAIMS_CHECKS:
						break;
					case CHECK_TYPES.MISCELLANEOUS_OTHER_CHECKS:
						break;
					case CHECK_TYPES.PREMIUM_REFUND_CHECKS_FOR_OVER_PAYMENT:
						objDataWrapper.AddParameter("@CHECK_TYPE",ClsChecks.CHECK_TYPES.PREMIUM_REFUND_CHECKS_FOR_OVER_PAYMENT);
						objDataWrapper.AddParameter("@IDEN_ROW_ID",objcheck.CHECK_ID);
						DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
						objcheck.PAYEE_ENTITY_NAME  = objDataSet.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString();
						objcheck.CHECK_ID			= int.Parse(objDataSet.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
						break;
					case CHECK_TYPES.PREMIUM_REFUND_CHECKS_FOR_RETURN_PREMIUM_PAYMENT:
						break;
					case CHECK_TYPES.PREMIUM_REFUND_CHECKS_FOR_SUSPENSE_AMOUNT:
						break;
					case CHECK_TYPES.REINSURANCE_PREMIUM_CHECKS:
						break;
					case CHECK_TYPES.VENDOR_CHECKS:
						break;
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			
		}*/
		#endregion

		#region "Editable grid related code- Misc checks"
		public int CopyDistributionRecords(int ACTUAL_CHECK_ID,int TEMP_CHECK_ID,DataWrapper objDataWrapper)
		{
			string strSql = "PROC_CopyFrom_TEMP_ACT_DISTRIBUTION_DETAILS";
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@ACTUAL_CHECK_ID",ACTUAL_CHECK_ID);
			objDataWrapper.AddParameter("@TEMP_CHECK_ID",TEMP_CHECK_ID);
			int ret = objDataWrapper.ExecuteNonQuery(strSql);
			objDataWrapper.ClearParameteres();
			return ret;
		}
		public int AddTempChecksToActual(ArrayList objChecksInfo,ArrayList checkIds)
		{
			DataWrapper objDataWrapper=null;
			try
			{
				int retResult=0,i=0;
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				foreach(ClsChecksInfo objCheck  in objChecksInfo)
				{
					retResult=Add(objCheck,objDataWrapper,0);
					if(retResult<=0)
						break;
					CopyDistributionRecords(objCheck.CHECK_ID,Convert.ToInt32(checkIds[i].ToString()),objDataWrapper);
					i++;
				}
				if(objChecksInfo.Count>0)
				{
					ClsChecksInfo obj = (ClsChecksInfo)objChecksInfo[0];
					foreach(int id  in checkIds)
						DeleteFromTempCheckTable(obj.CREATED_BY,id,objDataWrapper);
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return retResult;
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
		/*FOR ACT AGENCY COMMISION CHECKS DEC 06*/
		public int AddTempCommChecksToActual(ArrayList objChecksInfo,ArrayList checkIds)
		{
			DataWrapper objDataWrapper=null;
			try
			{
				int retResult=0,i=0;
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				foreach(ClsChecksInfo objCheck  in objChecksInfo)
				{
					retResult=AddAgnCommChecks(objCheck,objDataWrapper,0);
					if(retResult<=0)
						break;
					i++;
				}
				if(objChecksInfo.Count>0)
				{
					ClsChecksInfo obj = (ClsChecksInfo)objChecksInfo[0];
					foreach(int id  in checkIds)
						DeleteFromTempCheckTable(obj.CREATED_BY,id,objDataWrapper);
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return retResult;
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


		//Added By Ravindra

		public int AddTempVendorChecksToActual(ArrayList objChecksInfo,ArrayList checkIds)
		{

			DataWrapper objDataWrapper=null;
			try
			{
				int retResult=0,i=0;
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				foreach(ClsChecksInfo objCheck  in objChecksInfo)
				{
					retResult=AddVendorCheck(objCheck,objDataWrapper,0);
					if(retResult<=0)
						break;
					i++;
				}
				if(objChecksInfo.Count>0)
				{
					ClsChecksInfo obj = (ClsChecksInfo)objChecksInfo[0];
					foreach(int id  in checkIds)
						DeleteFromTempCheckTable(obj.CREATED_BY,id,objDataWrapper);
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return retResult;
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
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objChecksInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int AddAgnCommChecks(ClsChecksInfo objChecksInfo,DataWrapper objDataWrapper,int OPEN_ITEM_ID)//,string strStoredProc)
		{
			string		strStoredProc	=	"Proc_InsertACT_CHECK_INFORMATION";
			
			DateTime	RecordDate		=	DateTime.Now;
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CHECK_TYPE",objChecksInfo.CHECK_TYPE);
			objDataWrapper.AddParameter("@SELECT_FROM",objChecksInfo.SELECT_FROM);
			objDataWrapper.AddParameter("@ACCOUNT_ID",objChecksInfo.ACCOUNT_ID);
			objDataWrapper.AddParameter("@CHECK_NUMBER",objChecksInfo.CHECK_NUMBER);
			objDataWrapper.AddParameter("@CHECK_DATE",objChecksInfo.CHECK_DATE);
			objDataWrapper.AddParameter("@CHECK_AMOUNT",objChecksInfo.CHECK_AMOUNT);
			objDataWrapper.AddParameter("@CHECK_NOTE",objChecksInfo.CHECK_NOTE);
			objDataWrapper.AddParameter("@PAYEE_ENTITY_ID",objChecksInfo.PAYEE_ENTITY_ID);
			objDataWrapper.AddParameter("@PAYEE_ENTITY_TYPE",objChecksInfo.PAYEE_ENTITY_TYPE);
			objDataWrapper.AddParameter("@PAYEE_ENTITY_NAME",objChecksInfo.PAYEE_ENTITY_NAME);
			objDataWrapper.AddParameter("@PAYEE_ADD1",objChecksInfo.PAYEE_ADD1);
			objDataWrapper.AddParameter("@PAYEE_ADD2",objChecksInfo.PAYEE_ADD2);
			objDataWrapper.AddParameter("@PAYEE_CITY",objChecksInfo.PAYEE_CITY);
			objDataWrapper.AddParameter("@PAYEE_STATE",objChecksInfo.PAYEE_STATE);
			objDataWrapper.AddParameter("@PAYEE_ZIP",objChecksInfo.PAYEE_ZIP);
			objDataWrapper.AddParameter("@PAYEE_NOTE",objChecksInfo.PAYEE_NOTE);
			objDataWrapper.AddParameter("@CREATED_IN",objChecksInfo.CREATED_IN);
			objDataWrapper.AddParameter("@DIV_ID",objChecksInfo.DIV_ID);
			objDataWrapper.AddParameter("@DEPT_ID",objChecksInfo.DEPT_ID);
			objDataWrapper.AddParameter("@PC_ID",objChecksInfo.PC_ID);
			objDataWrapper.AddParameter("@IS_COMMITED",objChecksInfo.IS_COMMITED);
			objDataWrapper.AddParameter("@PAYMENT_MODE",objChecksInfo.PAYMENT_MODE);
			if(objChecksInfo.DATE_COMMITTED == DateTime.MinValue)
				objDataWrapper.AddParameter("@DATE_COMMITTED",DBNull.Value);
			else
				objDataWrapper.AddParameter("@DATE_COMMITTED",objChecksInfo.DATE_COMMITTED);

			objDataWrapper.AddParameter("@COMMITED_BY",objChecksInfo.COMMITED_BY);
			objDataWrapper.AddParameter("@IN_RECON",objChecksInfo.IN_RECON);
			objDataWrapper.AddParameter("@AVAILABLE_BALANCE",objChecksInfo.AVAILABLE_BALANCE);
			objDataWrapper.AddParameter("@CUSTOMER_ID",objChecksInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@POLICY_ID",objChecksInfo.POLICY_ID);
			objDataWrapper.AddParameter("@POLICY_VER_TRACKING_ID",objChecksInfo.POLICY_VER_TRACKING_ID);
			objDataWrapper.AddParameter("@GL_UPDATE",objChecksInfo.GL_UPDATE);
			objDataWrapper.AddParameter("@IS_BNK_RECONCILED",objChecksInfo.IS_BNK_RECONCILED);
			objDataWrapper.AddParameter("@CHECKSIGN_1",objChecksInfo.CHECKSIGN_1);
			objDataWrapper.AddParameter("@CHECKSIGN_2",objChecksInfo.CHECKSIGN_2);
			objDataWrapper.AddParameter("@CHECK_MEMO",objChecksInfo.CHECK_MEMO);
			objDataWrapper.AddParameter("@IS_BNK_RECONCILED_VOID",objChecksInfo.IS_BNK_RECONCILED_VOID);
			objDataWrapper.AddParameter("@IN_BNK_RECON",objChecksInfo.IN_BNK_RECON);
			objDataWrapper.AddParameter("@SPOOL_STATUS",objChecksInfo.SPOOL_STATUS);
			objDataWrapper.AddParameter("@MANUAL_CHECK",objChecksInfo.MANUAL_CHECK);
			objDataWrapper.AddParameter("@TRAN_TYPE",objChecksInfo.TRAN_TYPE);
			objDataWrapper.AddParameter("@IS_DISPLAY_ON_STUB",objChecksInfo.IS_DISPLAY_ON_STUB);
			objDataWrapper.AddParameter("@OPEN_ITEM_ID",OPEN_ITEM_ID);
			objDataWrapper.AddParameter("@IS_ACTIVE",objChecksInfo.IS_ACTIVE);
			objDataWrapper.AddParameter("@CREATED_BY",objChecksInfo.CREATED_BY);
			objDataWrapper.AddParameter("@CREATED_DATETIME",objChecksInfo.CREATED_DATETIME);
			objDataWrapper.AddParameter("@MODifIED_BY",objChecksInfo.MODIFIED_BY);
			objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objChecksInfo.LAST_UPDATED_DATETIME);
			objDataWrapper.AddParameter("@MONTH",objChecksInfo.MONTH);
			objDataWrapper.AddParameter("@YEAR",objChecksInfo.YEAR);
			objDataWrapper.AddParameter("@COMM_TYPE",objChecksInfo.COMM_TYPE);
			objDataWrapper.AddParameter("@TEMP_CHECK_ID",objChecksInfo.TMP_CHECK_ID );

			SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@CHECK_ID",objChecksInfo.CHECK_ID,SqlDbType.Int,ParameterDirection.Output);

			int returnResult = 0;
			if(TransactionLogRequired)
			{
				objChecksInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/aspx/AddCheck.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objChecksInfo);
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				objTransactionInfo.RECORDED_BY		=	objChecksInfo.CREATED_BY;
                objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1659", "");//"Check(s) Has Been Created Successfully";
				objTransactionInfo.CHANGE_XML		=	strTranXML;
				//Executing the query
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
			}
			else
			{
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
			}
			int CHECK_ID = int.Parse(objSqlParameter.Value.ToString());
			objDataWrapper.ClearParameteres();
					
			if (CHECK_ID == -1)//check number already assigned to another check.
			{
				return -1;
			}
			else if (CHECK_ID == -2)//check number exceeds the max limit.
			{
					
				return -2;
			}
			else
			{
				objChecksInfo.CHECK_ID = CHECK_ID;
				return returnResult;
			}
				
				
		}

		public int AddVendorCheck(ClsChecksInfo objChecksInfo,DataWrapper objDataWrapper,int OPEN_ITEM_ID)//,string strStoredProc)
		{
			string		strStoredProc	=	"Proc_InsertACT_CHECK_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CHECK_TYPE",objChecksInfo.CHECK_TYPE);
			objDataWrapper.AddParameter("@SELECT_FROM",objChecksInfo.SELECT_FROM);
			objDataWrapper.AddParameter("@ACCOUNT_ID",objChecksInfo.ACCOUNT_ID);
			objDataWrapper.AddParameter("@CHECK_NUMBER",objChecksInfo.CHECK_NUMBER);
			objDataWrapper.AddParameter("@CHECK_DATE",objChecksInfo.CHECK_DATE);
			objDataWrapper.AddParameter("@CHECK_AMOUNT",objChecksInfo.CHECK_AMOUNT);
			objDataWrapper.AddParameter("@CHECK_NOTE",objChecksInfo.CHECK_NOTE);
			objDataWrapper.AddParameter("@PAYEE_ENTITY_ID",objChecksInfo.PAYEE_ENTITY_ID);
			objDataWrapper.AddParameter("@PAYEE_ENTITY_TYPE",objChecksInfo.PAYEE_ENTITY_TYPE);
			objDataWrapper.AddParameter("@PAYEE_ENTITY_NAME",objChecksInfo.PAYEE_ENTITY_NAME);
			objDataWrapper.AddParameter("@PAYEE_ADD1",objChecksInfo.PAYEE_ADD1);
			objDataWrapper.AddParameter("@PAYEE_ADD2",objChecksInfo.PAYEE_ADD2);
			objDataWrapper.AddParameter("@PAYEE_CITY",objChecksInfo.PAYEE_CITY);
			objDataWrapper.AddParameter("@PAYEE_STATE",objChecksInfo.PAYEE_STATE);
			objDataWrapper.AddParameter("@PAYEE_ZIP",objChecksInfo.PAYEE_ZIP);
			objDataWrapper.AddParameter("@PAYEE_NOTE",objChecksInfo.PAYEE_NOTE);
			objDataWrapper.AddParameter("@CREATED_IN",objChecksInfo.CREATED_IN);
			objDataWrapper.AddParameter("@DIV_ID",objChecksInfo.DIV_ID);
			objDataWrapper.AddParameter("@DEPT_ID",objChecksInfo.DEPT_ID);
			objDataWrapper.AddParameter("@PC_ID",objChecksInfo.PC_ID);
			objDataWrapper.AddParameter("@IS_COMMITED",objChecksInfo.IS_COMMITED);
			if(objChecksInfo.DATE_COMMITTED == DateTime.MinValue)
				objDataWrapper.AddParameter("@DATE_COMMITTED",DBNull.Value);
			else
				objDataWrapper.AddParameter("@DATE_COMMITTED",objChecksInfo.DATE_COMMITTED);

			objDataWrapper.AddParameter("@COMMITED_BY",objChecksInfo.COMMITED_BY);
			objDataWrapper.AddParameter("@IN_RECON",objChecksInfo.IN_RECON);
			objDataWrapper.AddParameter("@AVAILABLE_BALANCE",objChecksInfo.AVAILABLE_BALANCE);
			objDataWrapper.AddParameter("@CUSTOMER_ID",objChecksInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@POLICY_ID",objChecksInfo.POLICY_ID);
			objDataWrapper.AddParameter("@POLICY_VER_TRACKING_ID",objChecksInfo.POLICY_VER_TRACKING_ID);
			objDataWrapper.AddParameter("@GL_UPDATE",objChecksInfo.GL_UPDATE);
			objDataWrapper.AddParameter("@IS_BNK_RECONCILED",objChecksInfo.IS_BNK_RECONCILED);
			objDataWrapper.AddParameter("@CHECKSIGN_1",objChecksInfo.CHECKSIGN_1);
			objDataWrapper.AddParameter("@CHECKSIGN_2",objChecksInfo.CHECKSIGN_2);
			objDataWrapper.AddParameter("@CHECK_MEMO",objChecksInfo.CHECK_MEMO);
			objDataWrapper.AddParameter("@IS_BNK_RECONCILED_VOID",objChecksInfo.IS_BNK_RECONCILED_VOID);
			objDataWrapper.AddParameter("@IN_BNK_RECON",objChecksInfo.IN_BNK_RECON);
			objDataWrapper.AddParameter("@SPOOL_STATUS",objChecksInfo.SPOOL_STATUS);
			objDataWrapper.AddParameter("@MANUAL_CHECK",objChecksInfo.MANUAL_CHECK);
			objDataWrapper.AddParameter("@TRAN_TYPE",objChecksInfo.TRAN_TYPE);
			objDataWrapper.AddParameter("@IS_DISPLAY_ON_STUB",objChecksInfo.IS_DISPLAY_ON_STUB);
			objDataWrapper.AddParameter("@OPEN_ITEM_ID",OPEN_ITEM_ID);
			objDataWrapper.AddParameter("@IS_ACTIVE",objChecksInfo.IS_ACTIVE);
			objDataWrapper.AddParameter("@CREATED_BY",objChecksInfo.CREATED_BY);
			objDataWrapper.AddParameter("@CREATED_DATETIME",objChecksInfo.CREATED_DATETIME);
			objDataWrapper.AddParameter("@MODifIED_BY",objChecksInfo.MODIFIED_BY);
			objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objChecksInfo.LAST_UPDATED_DATETIME);
			objDataWrapper.AddParameter("@TEMP_CHECK_ID",objChecksInfo.TMP_CHECK_ID );
			objDataWrapper.AddParameter("@PAYMENT_MODE",objChecksInfo.PAYMENT_MODE);
			SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@CHECK_ID",objChecksInfo.CHECK_ID,SqlDbType.Int,ParameterDirection.Output);

			int returnResult = 0;
			if(TransactionLogRequired)
			{
				objChecksInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/aspx/AddCheck.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objChecksInfo);
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				objTransactionInfo.RECORDED_BY		=	objChecksInfo.CREATED_BY;
                objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1659", "");//"Check(s) Has Been Created Successfully";
				objTransactionInfo.CHANGE_XML		=	strTranXML;
				//Executing the query
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
			}
			else
			{
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
			}
			int CHECK_ID = int.Parse(objSqlParameter.Value.ToString());
			objDataWrapper.ClearParameteres();
					
			if (CHECK_ID == -1)//check number already assigned to another check.
			{
				return -1;
			}
			else if (CHECK_ID == -2)//check number exceeds the max limit.
			{
					
				return -2;
			}
			else
			{
				objChecksInfo.CHECK_ID = CHECK_ID;
				return returnResult;
			}
				
				
		}


		/**********************************************************/

		public int SaveTempData(ArrayList objChecksInfoForAdd,ArrayList objChecksInfoforUpdate)
		{
			int retResult=0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			if(objChecksInfoForAdd.Count>0)
			{
				retResult=AddToTemp(objChecksInfoForAdd,objDataWrapper);
			}
			if(objChecksInfoforUpdate.Count>0)
			{
				retResult=UpdateToTemp(objChecksInfoforUpdate,objDataWrapper);
			}
			return retResult;
		}
		//Added for Agency Commision Checks : SaveTempCommData() 
		public int SaveTempDataAgen(ArrayList objChecksInfoForAdd,ArrayList objChecksInfoforUpdate)
		{
			int retResult=0;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			if(objChecksInfoForAdd.Count>0)
				retResult=AddToTempAgen(objChecksInfoForAdd,objDataWrapper);
			if(objChecksInfoforUpdate.Count>0)
				retResult=UpdateToTempAgen(objChecksInfoforUpdate,objDataWrapper);
			return retResult;
		}
		public int AddToTemp(ArrayList objChecksInfo,DataWrapper objDataWrapper)
		{
			try
			{
				int retResult=0,i=0;
		
				foreach(ClsChecksInfo objCheck in objChecksInfo)
				{
					retResult=AddToTemp(objCheck,objDataWrapper);
					i++;
					if(retResult<=0)
						break;
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return retResult;
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
		public int AddToTempAgen(ArrayList objChecksInfo,DataWrapper objDataWrapper)
		{
			try
			{
				int retResult=0,i=0;
		
				foreach(ClsChecksInfo objCheck in objChecksInfo)
				{
					retResult=AddToTempAgen(objCheck,objDataWrapper);
					i++;
					if(retResult<=0)
						break;
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return retResult;
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
		public int AddToTemp(ClsChecksInfo objChecksInfo,DataWrapper objDataWrapper)
		{
			string strStoredProc="Proc_InsertTEMP_ACT_CHECK_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CHECK_TYPE",objChecksInfo.CHECK_TYPE);
			objDataWrapper.AddParameter("@ACCOUNT_ID",objChecksInfo.ACCOUNT_ID);
			objDataWrapper.AddParameter("@CHECK_DATE",objChecksInfo.CHECK_DATE);
			objDataWrapper.AddParameter("@CHECK_AMOUNT",objChecksInfo.CHECK_AMOUNT);
			objDataWrapper.AddParameter("@CHECK_NOTE",objChecksInfo.CHECK_NOTE);
			objDataWrapper.AddParameter("@PAYEE_ENTITY_ID",objChecksInfo.PAYEE_ENTITY_ID);
			objDataWrapper.AddParameter("@PAYEE_ENTITY_TYPE",objChecksInfo.PAYEE_ENTITY_TYPE);
			objDataWrapper.AddParameter("@PAYEE_ENTITY_NAME",objChecksInfo.PAYEE_ENTITY_NAME);
			objDataWrapper.AddParameter("@PAYEE_ADD1",objChecksInfo.PAYEE_ADD1);
			objDataWrapper.AddParameter("@PAYEE_ADD2",objChecksInfo.PAYEE_ADD2);
			objDataWrapper.AddParameter("@PAYEE_CITY",objChecksInfo.PAYEE_CITY);
			objDataWrapper.AddParameter("@PAYEE_STATE",objChecksInfo.PAYEE_STATE);
			objDataWrapper.AddParameter("@PAYEE_ZIP",objChecksInfo.PAYEE_ZIP);
			objDataWrapper.AddParameter("@PAYEE_NOTE",objChecksInfo.PAYEE_NOTE);
			objDataWrapper.AddParameter("@CUSTOMER_ID",objChecksInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@POLICY_ID",objChecksInfo.POLICY_ID);
			objDataWrapper.AddParameter("@POLICY_VER_TRACKING_ID",objChecksInfo.POLICY_VER_TRACKING_ID);
			objDataWrapper.AddParameter("@CREATED_BY",objChecksInfo.CREATED_BY);
			objDataWrapper.AddParameter("@CREATED_DATETIME",objChecksInfo.CREATED_DATETIME);
			objDataWrapper.AddParameter("@MODifIED_BY",objChecksInfo.MODIFIED_BY);
			objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objChecksInfo.LAST_UPDATED_DATETIME);
			objDataWrapper.AddParameter("@PAYMENT_MODE",objChecksInfo.PAYMENT_MODE);
			
			SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@CHECK_ID",objChecksInfo.CHECK_ID,SqlDbType.Int,ParameterDirection.Output);

			int returnResult = 0;

			////////////////////////////
			if(TransactionLogRequired)
			{
				objChecksInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/aspx/AddCheck.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objChecksInfo);
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				objTransactionInfo.RECORDED_BY		=	objChecksInfo.CREATED_BY;
                objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1665", "");//"Check(s) Has Been Saved Successfully";
				objTransactionInfo.CHANGE_XML		=	strTranXML;
				//Executing the query
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
			}
			else
			{
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
			}
			int CHECK_ID = int.Parse(objSqlParameter.Value.ToString());
			objDataWrapper.ClearParameteres();
					
			
			objChecksInfo.CHECK_ID = CHECK_ID;
			return returnResult;
			
			///////////////////////////////////
				
			//			returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
			//				
			//			int CHECK_ID = int.Parse(objSqlParameter.Value.ToString());
			//			objDataWrapper.ClearParameteres();
			//				
			//				
			//			objChecksInfo.CHECK_ID = CHECK_ID;
			//			return returnResult;
			
		}
		
		public int AddToTempAgen(ClsChecksInfo objChecksInfo,DataWrapper objDataWrapper)
		{
			string strStoredProc="Proc_InsertTEMP_ACT_CHECK_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CHECK_TYPE",objChecksInfo.CHECK_TYPE);
			objDataWrapper.AddParameter("@ACCOUNT_ID",objChecksInfo.ACCOUNT_ID);
			objDataWrapper.AddParameter("@CHECK_DATE",objChecksInfo.CHECK_DATE);
			objDataWrapper.AddParameter("@CHECK_AMOUNT",objChecksInfo.CHECK_AMOUNT);
			objDataWrapper.AddParameter("@CHECK_NOTE",objChecksInfo.CHECK_NOTE);
			objDataWrapper.AddParameter("@PAYEE_ENTITY_ID",objChecksInfo.PAYEE_ENTITY_ID);
			objDataWrapper.AddParameter("@PAYEE_ENTITY_TYPE",objChecksInfo.PAYEE_ENTITY_TYPE);
			objDataWrapper.AddParameter("@PAYEE_ENTITY_NAME",objChecksInfo.PAYEE_ENTITY_NAME);
			objDataWrapper.AddParameter("@PAYEE_ADD1",objChecksInfo.PAYEE_ADD1);
			objDataWrapper.AddParameter("@PAYEE_ADD2",objChecksInfo.PAYEE_ADD2);
			objDataWrapper.AddParameter("@PAYEE_CITY",objChecksInfo.PAYEE_CITY);
			objDataWrapper.AddParameter("@PAYEE_STATE",objChecksInfo.PAYEE_STATE);
			objDataWrapper.AddParameter("@PAYEE_ZIP",objChecksInfo.PAYEE_ZIP);
			objDataWrapper.AddParameter("@PAYEE_NOTE",objChecksInfo.PAYEE_NOTE);
			objDataWrapper.AddParameter("@CUSTOMER_ID",objChecksInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@POLICY_ID",objChecksInfo.POLICY_ID);
			objDataWrapper.AddParameter("@POLICY_VER_TRACKING_ID",objChecksInfo.POLICY_VER_TRACKING_ID);
			objDataWrapper.AddParameter("@CREATED_BY",objChecksInfo.CREATED_BY);
			objDataWrapper.AddParameter("@CREATED_DATETIME",objChecksInfo.CREATED_DATETIME);
			objDataWrapper.AddParameter("@MODifIED_BY",objChecksInfo.MODIFIED_BY);
			objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objChecksInfo.LAST_UPDATED_DATETIME);
			objDataWrapper.AddParameter("@MONTH",objChecksInfo.MONTH);
			objDataWrapper.AddParameter("@YEAR",objChecksInfo.YEAR);
			objDataWrapper.AddParameter("@COMM_TYPE",objChecksInfo.COMM_TYPE);
			objDataWrapper.AddParameter("@PAYMENT_MODE",objChecksInfo.PAYMENT_MODE);
			objDataWrapper.AddParameter("@AGENCY_ID",objChecksInfo.AGENCY_ID );
			SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@CHECK_ID",objChecksInfo.CHECK_ID,SqlDbType.Int,ParameterDirection.Output);

			int returnResult = 0;

			////////////////////////////
			if(TransactionLogRequired)
			{
				objChecksInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/aspx/AG_AgencyCommissionChecks.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objChecksInfo);
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				objTransactionInfo.RECORDED_BY		=	objChecksInfo.CREATED_BY;
                objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1665", "");// "Check(s) Has Been Saved Successfully";
				objTransactionInfo.CHANGE_XML		=	strTranXML;
				//Executing the query
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
			}
			else
			{
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
			}
			int CHECK_ID = int.Parse(objSqlParameter.Value.ToString());
			objDataWrapper.ClearParameteres();
					
			
			objChecksInfo.CHECK_ID = CHECK_ID;
			return returnResult;
			
			///////////////////////////////////
			///
				
			//returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				
			//int CHECK_ID = int.Parse(objSqlParameter.Value.ToString());
			//objDataWrapper.ClearParameteres();
				
				
			//objChecksInfo.CHECK_ID = CHECK_ID;
			//return returnResult;
			
		}
		
		//PROC_GET_TEMP_CHECK_DATA
		public static DataSet GetTempCheckData(int LOGGED_IN_USER_ID, int CheckType)
		{
			string strSql = "PROC_GET_TEMP_CHECK_DATA";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@LOGGED_IN_USER_ID",LOGGED_IN_USER_ID);
			objDataWrapper.AddParameter("@CHECK_TYPE",CheckType);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}
		private int DeleteFromTempCheckTable(int LOGGED_IN_USER_ID,DataWrapper objDataWrapper)
		{
			string strSql = "PROC_DELETE_TEMP_ACT_CHECK_INFORMATION";
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@LOGGED_IN_USER_ID",LOGGED_IN_USER_ID);
			int ret = objDataWrapper.ExecuteNonQuery(strSql);
			objDataWrapper.ClearParameteres();
			return ret;
			
		}
		public int DeleteSelectedChecksFromTempCheckTable(ArrayList checkIds,int userId)
		{
			DataWrapper objDataWrapper=null;
			try
			{
				int retResult=0,i=0;
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				//ClsChecksInfo temp = (ClsChecksInfo) objChecksInfo[0];
				foreach(int id  in checkIds)
				{
					//retResult=DeleteFromTempCheckTable(userId,id,objDataWrapper);
					retResult=DeleteFromTempCheckTable(userId,id);
					i++;
					if(retResult<=0)
						break;
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return retResult;
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
		private int DeleteFromTempCheckTable(int LOGGED_IN_USER_ID,int CHECK_ID,DataWrapper objDataWrapper)
		{
			string strSql = "PROC_DELETE_CHECK_TEMP_ACT_CHECK_INFORMATION";
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@LOGGED_IN_USER_ID",LOGGED_IN_USER_ID);
			objDataWrapper.AddParameter("@CHECK_ID",CHECK_ID);
			int ret = objDataWrapper.ExecuteNonQuery(strSql);
			objDataWrapper.ClearParameteres();
			return ret;
			/*String strStoredProc = "PROC_DELETE_CHECK_TEMP_ACT_CHECK_INFORMATION";
			int Value;
			//DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@LOGGED_IN_USER_ID", LOGGED_IN_USER_ID);
			objDataWrapper.AddParameter("@CHECK_ID",CHECK_ID);
			SqlParameter objRetVal = (SqlParameter) objDataWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);
			if(TransactionLogRequired) 
			{
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	3;
				if (LOGGED_IN_USER_ID!= 0)
					objTransactionInfo.RECORDED_BY		=  	LOGGED_IN_USER_ID;  
				objTransactionInfo.TRANS_DESC		=	"Check(s) Has Been Deleted Successfully.";

				ClsDistributeCashReceipt obj = new ClsDistributeCashReceipt();
				objTransactionInfo.CUSTOM_INFO		=	"Payee Name :" + obj.FetchPayeeName(CHECK_ID) +";Check Type:" + GetCheckTypeOnId(CHECK_ID) ;

				Value = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
			}
			else
				Value = objDataWrapper.ExecuteNonQuery(strStoredProc);

			Value = int.Parse(objRetVal.Value.ToString());
			objDataWrapper.ClearParameteres();
			objDataWrapper.Dispose();

			return Value ;*/
		}
		//New Delete Function for Delete Checks Dec 13 2007(Modified Dur to TL)
		private int DeleteFromTempCheckTable(int LOGGED_IN_USER_ID,int CHECK_ID)
		{			
			String strStoredProc = "PROC_DELETE_CHECK_TEMP_ACT_CHECK_INFORMATION";
			int Value;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@LOGGED_IN_USER_ID", LOGGED_IN_USER_ID);
			objDataWrapper.AddParameter("@CHECK_ID",CHECK_ID);
			SqlParameter objRetVal = (SqlParameter) objDataWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);
			if(TransactionLogRequired) 
			{
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	3;
				if (LOGGED_IN_USER_ID!= 0)
					objTransactionInfo.RECORDED_BY		=  	LOGGED_IN_USER_ID;
                objTransactionInfo.TRANS_DESC           =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1666", "");// "Check(s) Has Been Deleted Successfully.";

				ClsDistributeCashReceipt obj = new ClsDistributeCashReceipt();
				objTransactionInfo.CUSTOM_INFO		=	"Payee Name :" + obj.FetchPayeeName(CHECK_ID) +";Check Type:" + GetTempCheckType(CHECK_ID) ;

				Value = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
			}
			else
				Value = objDataWrapper.ExecuteNonQuery(strStoredProc);

			Value = int.Parse(objRetVal.Value.ToString());
			objDataWrapper.ClearParameteres();
			objDataWrapper.Dispose();

			return Value ;
		}

		public bool ConfirmDistributionForTempChecks(ArrayList objCheckIds)
		{
			string strSql = "PROC_GetDistributionForTempChecks";
			DataTable objTable = DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,strSql).Tables[0];
			
			foreach(int CHECK_ID in objCheckIds)
			{
				DataRow[] objRow = objTable.Select("CHECK_ID="+CHECK_ID.ToString());		
				if(objRow.Length>0)	
				{
					double distributedAmount = Convert.ToDouble(objRow[0]["amount"].ToString());//0th row is taken as only one check id is possible with group by
					double checkamount = Convert.ToDouble(objRow[0]["CHECK_AMOUNT"].ToString());
					if(distributedAmount<checkamount)
						return false;
				}
				else
					return false;
			}
			
			return true;
		}

		
		public int UpdateToTemp(ArrayList objChecksInfo,DataWrapper objDataWrapper)
		{
			try
			{
				int retResult=0,i=0;
				foreach(ClsChecksInfo objCheck  in objChecksInfo)
				{
					retResult=UpdateToTemp(objCheck,objDataWrapper);
					i++;
					if(retResult<=0)
						break;
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return retResult;
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
		public int UpdateToTempAgen(ArrayList objChecksInfo,DataWrapper objDataWrapper)
		{
			try
			{
				int retResult=0,i=0;
				foreach(ClsChecksInfo objCheck  in objChecksInfo)
				{
					retResult=UpdateToTempAgen(objCheck,objDataWrapper);
					i++;
					if(retResult<=0)
						break;
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return retResult;
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
		public int UpdateToTemp(ClsChecksInfo objChecksInfo,DataWrapper objDataWrapper)
		{
			string strStoredProc="PROC_UPDATE_TEMP_ACT_CHECK_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CHECK_TYPE",objChecksInfo.CHECK_TYPE);
			objDataWrapper.AddParameter("@ACCOUNT_ID",objChecksInfo.ACCOUNT_ID);
			objDataWrapper.AddParameter("@CHECK_DATE",objChecksInfo.CHECK_DATE);
			objDataWrapper.AddParameter("@CHECK_AMOUNT",objChecksInfo.CHECK_AMOUNT);
			objDataWrapper.AddParameter("@CHECK_NOTE",objChecksInfo.CHECK_NOTE);
			objDataWrapper.AddParameter("@PAYEE_ENTITY_ID",objChecksInfo.PAYEE_ENTITY_ID);
			objDataWrapper.AddParameter("@PAYEE_ENTITY_TYPE",objChecksInfo.PAYEE_ENTITY_TYPE);
			objDataWrapper.AddParameter("@PAYEE_ENTITY_NAME",objChecksInfo.PAYEE_ENTITY_NAME);
			objDataWrapper.AddParameter("@PAYEE_ADD1",objChecksInfo.PAYEE_ADD1);
			objDataWrapper.AddParameter("@PAYEE_ADD2",objChecksInfo.PAYEE_ADD2);
			objDataWrapper.AddParameter("@PAYEE_CITY",objChecksInfo.PAYEE_CITY);
			objDataWrapper.AddParameter("@PAYEE_STATE",objChecksInfo.PAYEE_STATE);
			objDataWrapper.AddParameter("@PAYEE_ZIP",objChecksInfo.PAYEE_ZIP);
			objDataWrapper.AddParameter("@PAYEE_NOTE",objChecksInfo.PAYEE_NOTE);
			objDataWrapper.AddParameter("@CUSTOMER_ID",objChecksInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@POLICY_ID",objChecksInfo.POLICY_ID);
			objDataWrapper.AddParameter("@POLICY_VER_TRACKING_ID",objChecksInfo.POLICY_VER_TRACKING_ID);
			objDataWrapper.AddParameter("@CREATED_BY",objChecksInfo.CREATED_BY);
			objDataWrapper.AddParameter("@CREATED_DATETIME",objChecksInfo.CREATED_DATETIME);
			objDataWrapper.AddParameter("@MODifIED_BY",objChecksInfo.MODIFIED_BY);
			objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objChecksInfo.LAST_UPDATED_DATETIME);
			objDataWrapper.AddParameter("@CHECK_ID",objChecksInfo.CHECK_ID);
			objDataWrapper.AddParameter("@PAYMENT_MODE",objChecksInfo.PAYMENT_MODE);
			
			int returnResult = 0;
			////////////////////////////
			if(TransactionLogRequired)
			{
				objChecksInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/aspx/AddCheck.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objChecksInfo);
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				objTransactionInfo.RECORDED_BY		=	objChecksInfo.CREATED_BY;
                objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1667", "");// "Check(s) Has Been Updated Successfully";
				objTransactionInfo.CHANGE_XML		=	strTranXML;
				//Executing the query
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
			}
			else
			{
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
			}
			//int CHECK_ID = int.Parse(objSqlParameter.Value.ToString());
			objDataWrapper.ClearParameteres();		
			
			//objChecksInfo.CHECK_ID = CHECK_ID;
			return returnResult;
			
			///////////////////////////////////
				
			//			returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
			//				
			//			//int CHECK_ID = int.Parse(objSqlParameter.Value.ToString());
			//			objDataWrapper.ClearParameteres();			
			//			return returnResult;
			
		}
		
		public int UpdateToTempAgen(ClsChecksInfo objChecksInfo,DataWrapper objDataWrapper)
		{
			string strStoredProc="PROC_UPDATE_TEMP_ACT_CHECK_INFORMATION";
			DateTime	RecordDate		=	DateTime.Now;
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CHECK_TYPE",objChecksInfo.CHECK_TYPE);
			objDataWrapper.AddParameter("@ACCOUNT_ID",objChecksInfo.ACCOUNT_ID);
			objDataWrapper.AddParameter("@CHECK_DATE",objChecksInfo.CHECK_DATE);
			objDataWrapper.AddParameter("@CHECK_AMOUNT",objChecksInfo.CHECK_AMOUNT);
			objDataWrapper.AddParameter("@CHECK_NOTE",objChecksInfo.CHECK_NOTE);
			objDataWrapper.AddParameter("@PAYEE_ENTITY_ID",objChecksInfo.PAYEE_ENTITY_ID);
			objDataWrapper.AddParameter("@PAYEE_ENTITY_TYPE",objChecksInfo.PAYEE_ENTITY_TYPE);
			objDataWrapper.AddParameter("@PAYEE_ENTITY_NAME",objChecksInfo.PAYEE_ENTITY_NAME);
			objDataWrapper.AddParameter("@PAYEE_ADD1",objChecksInfo.PAYEE_ADD1);
			objDataWrapper.AddParameter("@PAYEE_ADD2",objChecksInfo.PAYEE_ADD2);
			objDataWrapper.AddParameter("@PAYEE_CITY",objChecksInfo.PAYEE_CITY);
			objDataWrapper.AddParameter("@PAYEE_STATE",objChecksInfo.PAYEE_STATE);
			objDataWrapper.AddParameter("@PAYEE_ZIP",objChecksInfo.PAYEE_ZIP);
			objDataWrapper.AddParameter("@PAYEE_NOTE",objChecksInfo.PAYEE_NOTE);
			objDataWrapper.AddParameter("@CUSTOMER_ID",objChecksInfo.CUSTOMER_ID);
			objDataWrapper.AddParameter("@POLICY_ID",objChecksInfo.POLICY_ID);
			objDataWrapper.AddParameter("@POLICY_VER_TRACKING_ID",objChecksInfo.POLICY_VER_TRACKING_ID);
			objDataWrapper.AddParameter("@CREATED_BY",objChecksInfo.CREATED_BY);
			objDataWrapper.AddParameter("@CREATED_DATETIME",objChecksInfo.CREATED_DATETIME);
			objDataWrapper.AddParameter("@MODifIED_BY",objChecksInfo.MODIFIED_BY);
			objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objChecksInfo.LAST_UPDATED_DATETIME);
			objDataWrapper.AddParameter("@CHECK_ID",objChecksInfo.CHECK_ID);
			objDataWrapper.AddParameter("@MONTH",objChecksInfo.MONTH);
			objDataWrapper.AddParameter("@YEAR",objChecksInfo.YEAR);
			objDataWrapper.AddParameter("@COMM_TYPE",objChecksInfo.COMM_TYPE);
			objDataWrapper.AddParameter("@PAYMENT_MODE",objChecksInfo.PAYMENT_MODE);
			
			//SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@CHECK_ID",objChecksInfo.CHECK_ID,SqlDbType.Int,ParameterDirection.Output);

			int returnResult = 0;

			////////////////////////////
			if(TransactionLogRequired)
			{																
				objChecksInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/aspx/AG_AgencyCommissionChecks.aspx.resx");
				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				string strTranXML = objBuilder.GetTransactionLogXML(objChecksInfo);
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				objTransactionInfo.RECORDED_BY		=	objChecksInfo.CREATED_BY;
                objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1667", "");//"Check(s) Has Been Updated Successfully";
				objTransactionInfo.CHANGE_XML		=	strTranXML;
				//Executing the query
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
			}
			else
			{
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
			}
			//int CHECK_ID = int.Parse(objSqlParameter.Value.ToString());
			objDataWrapper.ClearParameteres();
					
			
			//objChecksInfo.CHECK_ID = CHECK_ID;
			return returnResult;
			
			///////////////////////////////////
			///
				
			//returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				
			//int CHECK_ID = int.Parse(objSqlParameter.Value.ToString());
			//objDataWrapper.ClearParameteres();			
			//return returnResult;
			
		}
		
		#endregion

		#region "Editable grid related code- Reinsurance checks"
		public static DataTable GetReinsuranceCompanies()
		{
			string strSql = "Proc_FetchReinsuranceContracts";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.Tables[0];
		}
		public static void GetReinsuranceCompaniesInDropDown(DropDownList combo)
		{
			combo.DataSource = GetReinsuranceCompanies();
			combo.DataTextField = "Rein_Comapany_NAME";
			combo.DataValueField = "Rein_Comapany_ID";
			combo.DataBind();
		}
		#endregion

		#region "Editable grid related code- Vendor checks"
			
		#endregion

		#region Payee Info
		public static DataSet GetPayeeInfo(int CustID, int PolicyID, int PolicyVerID)
		{
			string strSql = "Proc_GetPayeeInfoForCheck";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustID);
			objDataWrapper.AddParameter("@POLICY_ID",PolicyID);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVerID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}
		#endregion

		
	}

	#region "PDF printing code"
	public struct PdfFields
	{
		//public string checkNumber;
		public string textAmount;
		public string voidInfo;
		public string checkDate;
		public string amount;
		public string payeeEntityName;
		public string payeeAddress;
		public string checkMemo;
		public string signatureFilePath1;
		public string signatureFilePath2;
	}

	
	public class CurrencyToText
	{
		public CurrencyToText()
		{
			
		}
		public  string strFinalNumber ="";
		string[]	 WORDS_TABLE_ONES = new string[10] ;
		string[] WORDS_TABLE_TENS=new string[20] ;
		double subnumber;
			
		public  string FindWordValue(double NumToConvert, string strdenom)
		{

			double my_number;

			WORDS_TABLE_ONES[0] = "";
			WORDS_TABLE_ONES[1] = " ONE";
			WORDS_TABLE_ONES[2] = " TWO";
			WORDS_TABLE_ONES[3] = " THREE";
			WORDS_TABLE_ONES[4] = " FOUR";
			WORDS_TABLE_ONES[5] = " FIVE";
			WORDS_TABLE_ONES[6] = " SIX";
			WORDS_TABLE_ONES[7] = " SEVEN";
			WORDS_TABLE_ONES[8] = " EIGHT";
			WORDS_TABLE_ONES[9] = " NINE";

			WORDS_TABLE_TENS[0] = "";
			WORDS_TABLE_TENS[1] = " TEN";
			WORDS_TABLE_TENS[2] = " TWENTY";
			WORDS_TABLE_TENS[3] = " THIRTY";
			WORDS_TABLE_TENS[4] = " FORTY";
			WORDS_TABLE_TENS[5] = " FifTY";
			WORDS_TABLE_TENS[6] = " SIXTY";
			WORDS_TABLE_TENS[7] = " SEVENTY";
			WORDS_TABLE_TENS[8] = " EIGHTY";
			WORDS_TABLE_TENS[9] = " NINETY";
			WORDS_TABLE_TENS[10] = "";
			WORDS_TABLE_TENS[11] = " ELEVEN";
			WORDS_TABLE_TENS[12] = " TWELVE";
			WORDS_TABLE_TENS[13] = " THIRTEEN";
			WORDS_TABLE_TENS[14] = " FOURTEEN";
			WORDS_TABLE_TENS[15] = " FIFTEEN";
			WORDS_TABLE_TENS[16] = " SIXTEEN";
			WORDS_TABLE_TENS[17] = " SEVENTEEN";
			WORDS_TABLE_TENS[18] = " EIGHTEEN";
			WORDS_TABLE_TENS[19] = " NINETEEN";

			
			my_number = NumToConvert;
			strFinalNumber = "";

			string[] subnumber = NumToConvert.ToString().Trim().Split('.');

			if (double.Parse(subnumber[0]) > 0 )
				num2words (double.Parse(subnumber[0]));
			else
				strFinalNumber = " zero";
			

			if (strdenom == "D" || strdenom == "P" )
				if (double.Parse(subnumber[0]) > 1 )
					strFinalNumber = strFinalNumber;
				else
					strFinalNumber = strFinalNumber;
		
	

			if (subnumber.Length > 0 )
				if (subnumber[1].Length < 2 )
					subnumber[1] = subnumber[1] + "0";
			
		    
			if( strdenom == "D" || strdenom == "P" )
				if( double.Parse(subnumber[1]) > 1 )
					strFinalNumber = strFinalNumber ;
				else
					strFinalNumber = strFinalNumber ;
			else
				strFinalNumber = strFinalNumber + " POINT";
		
		    
			strFinalNumber = strFinalNumber + " AND ";
		
			if( double.Parse(subnumber[1]) > 0 )
				// 'num2words (double.Parse(subnumber[1]))
				strFinalNumber = strFinalNumber + subnumber[1] + " /100 ";
			else
				strFinalNumber = strFinalNumber + " 00/100 ";
			
			//else 
			//	strFinalNumber = strFinalNumber & " AND 00/100 ";
					
			if( strdenom == "D" )
				strFinalNumber = strFinalNumber + " DOLLARS";
			else if (strdenom == "P" )
				strFinalNumber = strFinalNumber + " POUNDS";
		

			return strFinalNumber;
		}
		private string num2words(double my_number)
		{
			double intMed =0,x=0;
			if (my_number > 999999999 && my_number < 1000000000000) 
				intMed = my_number / 1000000000;
			//'strFinalNumber = strFinalNumber & num2words(intMed) & " THOUSAND"
			if (intMed >= 100000000) 
				x = intMed;
			intMed = intMed / 100000000;
			strFinalNumber = strFinalNumber + lowerWords(intMed) + " HUNDRED MILLION ";
			intMed = x % 100000000;
			x = intMed;
			if (intMed >= 10000000) 
				strFinalNumber = strFinalNumber + lowerWords(intMed);
		   
			if ((intMed < 10000000) && (intMed > 0) )
				strFinalNumber = strFinalNumber + lowerWords(intMed);
		
				//'intMed = intMed % 100
			else
				x = intMed;
			intMed = intMed / 100000;
			if (intMed >= 100000 )
				strFinalNumber = strFinalNumber + lowerWords(intMed) + " HUNDRED THOUSAND ";
			else if (intMed < 100000 && intMed > 999) 
				strFinalNumber = strFinalNumber + lowerWords(intMed);
			else if (intMed <= 999 && intMed >= 1 )
				strFinalNumber = strFinalNumber + lowerWords(intMed);
		
			intMed = x % 100000;
			x = intMed;
			if (intMed >= 1 && intMed < 100000) 
				strFinalNumber = strFinalNumber + lowerWords(intMed);
		
		        
			//	 ' if (intMed < 10) && (intMed > 0) 
			//'     strFinalNumber = strFinalNumber & lowerWords(intMed)
			//' }
			intMed = intMed / 1000000000;
			//' strFinalNumber = strFinalNumber & lowerWords(x)
		
			strFinalNumber = strFinalNumber + " Billion ";
			my_number = my_number % 1000000000;
		    
	

			if (my_number > 999999 && my_number < 1000000000) 
				intMed = my_number / 1000000;
			//'strFinalNumber = strFinalNumber & num2words(intMed) & " THOUSAND"
			if (intMed >= 100000) 
				x = intMed;
			intMed = intMed / 100000;
			strFinalNumber = strFinalNumber + lowerWords(intMed) + " THOUSAND ";
			intMed = x % 100000;
			x = intMed;
			if (intMed >= 10000) 
				strFinalNumber = strFinalNumber + lowerWords(intMed);
		
		        
			if ((intMed < 10000) && (intMed > 0) )
				strFinalNumber = strFinalNumber + lowerWords(intMed);
		
				//					  'intMed = intMed % 100
			else
				x = intMed;
			intMed = intMed / 100;
			if (x >= 100 )
				strFinalNumber = strFinalNumber + lowerWords(intMed) + " HUNDRED ";
			else
				strFinalNumber = strFinalNumber + lowerWords(intMed);
		
			intMed = x % 100;
			x = intMed;
			if (intMed >= 1 && intMed < 100) 
				strFinalNumber = strFinalNumber + lowerWords(intMed);
		
		        
			//												  ' if (intMed < 10) && (intMed > 0) 
			//'     strFinalNumber = strFinalNumber & lowerWords(intMed)
			//	' }
			intMed = intMed / 1000000;
			//	' strFinalNumber = strFinalNumber & lowerWords(x)
		
			strFinalNumber = strFinalNumber + " MILLION ";
			my_number = my_number % 1000000;
		    
		

			if (my_number > 999 && my_number < 1000000) 
				intMed = my_number / 1000;
			//	'strFinalNumber = strFinalNumber & num2words(intMed) & " THOUSAND"
			if (intMed >= 100) 
				x = intMed;
			intMed = intMed  /100;
			strFinalNumber = strFinalNumber + lowerWords(intMed) + " HUNDRED ";
			intMed = x % 100;
			x = intMed;
			if (intMed >= 10) 
				strFinalNumber = strFinalNumber + lowerWords(intMed);
		
		        
			if( (intMed < 10) && (intMed > 0) )
				strFinalNumber = strFinalNumber + lowerWords(intMed);
		
				//'intMed = intMed % 100;
			else
				x = intMed;
			intMed = intMed / 1000;
			strFinalNumber = strFinalNumber + lowerWords(x);
											  
			strFinalNumber = strFinalNumber + " THOUSAND ";
			my_number = my_number % 1000;
		

			if (my_number >= 100) 
				intMed = my_number/  100;
			strFinalNumber = strFinalNumber + lowerWords(intMed) + " HUNDRED";
			my_number = my_number % 100;
		

			if (my_number >= 10) 
			{
				strFinalNumber = strFinalNumber + lowerWords(my_number);
				return strFinalNumber;
			}

			if ((my_number < 10) && (my_number > 0) )
				strFinalNumber = strFinalNumber + lowerWords(my_number);
			return strFinalNumber;																	 
		}
	
		private string lowerWords(double imy_num)
		{
			int my_num = (int) imy_num;
			if (my_num.ToString().Trim().Length < 2 )
				return WORDS_TABLE_ONES[my_num];
			else
				if( Right(my_num,1) == 0)
				return WORDS_TABLE_TENS[Left(my_num, 1)];
			else
				if( Left(my_num, 1) == 1 )
				return WORDS_TABLE_TENS[(Right(my_num, 1))] + 10;
																												   
			else
				return WORDS_TABLE_TENS[Left(my_num, 1)] + WORDS_TABLE_ONES[Right(my_num, 1)];
																										 																				   
		}			
		private int Left(int inum,int chars)
		{
			string num = inum.ToString().Trim();
			int len = num.Length;
			return int.Parse(num.Substring(0,chars));
		}																																								   
		private int Right(int inum,int chars)
		{
			string num = inum.ToString().Trim();
			int len = num.Length;
			return int.Parse(num.Substring(len-chars,len-1));
		}
	}
	#endregion																																																				   
																																																								   
}


