/******************************************************************************************
<Author				: -   Sumit Chhabra
<Start Date			: -	 06/02/2006 
<End Date			: -	
<Description		: - 	
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Claims;
using Cms.BusinessLayer.BlCommon;
using System.Text;
using System.Collections;

namespace Cms.BusinessLayer.BLClaims
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsActivityExpense : Cms.BusinessLayer.BLClaims.ClsClaims
	{

		private const	string		InsertCLM_ACTIVITY_EXPENSE		=	"Proc_InsertCLM_ACTIVITY_EXPENSE";
		private const	string		UpdateCLM_ACTIVITY_EXPENSE		=	"Proc_UpdateCLM_ACTIVITY_EXPENSE";
		private const	string		GetCLM_ACTIVITY_EXPENSE			=	"Proc_GetCLM_ACTIVITY_EXPENSE";
		private const	string		GetCLM_ACTIVITY_EXPENSE_HOME	=	"Proc_GetCLM_ACTIVITY_EXPENSE_HOME";
		private const	string		UpdateReserveCLM_ACTIVITY		=	"Proc_UpdateReserveCLM_ACTIVITY";
		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsActivityExpense()
		{}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objActivityExpenseInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ArrayList aPaymentList)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			int returnResult=0;
			try
			{
				//Added for Itrack Issue 7775 on 13 Aug 2010
				double outstanding = 0.0;
				string claimNumber = "";
				int customerId = 0,policyId = 0,policyVersionId = 0;

				DataSet ds = new DataSet();
				Cms.Model.Claims.ClsActivityExpenseInfo objclaimID = (ClsActivityExpenseInfo)aPaymentList[0];
				objDataWrapper.AddParameter("@CLAIM_ID",objclaimID.CLAIM_ID);
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				customerId = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				policyId = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				for(int i = 0; i < aPaymentList.Count; i++ )
				{
					Cms.Model.Claims.ClsActivityExpenseInfo objActivityExpenseInfo = (ClsActivityExpenseInfo)aPaymentList[i];

					objDataWrapper.ClearParameteres();
					objDataWrapper.AddParameter("@CLAIM_ID",objActivityExpenseInfo.CLAIM_ID);
					objDataWrapper.AddParameter("@RESERVE_ID",objActivityExpenseInfo.RESERVE_ID);
					objDataWrapper.AddParameter("@ACTIVITY_ID",objActivityExpenseInfo.ACTIVITY_ID);
					objDataWrapper.AddParameter("@PAYMENT_AMOUNT",objActivityExpenseInfo.PAYMENT_AMOUNT);
					objDataWrapper.AddParameter("@ACTION_ON_PAYMENT",objActivityExpenseInfo.ACTION_ON_PAYMENT);
					objDataWrapper.AddParameter("@CREATED_BY",objActivityExpenseInfo.CREATED_BY);
					objDataWrapper.AddParameter("@CREATED_DATETIME",objActivityExpenseInfo.CREATED_DATETIME);
					objDataWrapper.AddParameter("@VEHICLE_ID",objActivityExpenseInfo.VEHICLE_ID);
					if(objActivityExpenseInfo.ADDITIONAL_EXPENSE==0 || objActivityExpenseInfo.ADDITIONAL_EXPENSE==0.0)
						objDataWrapper.AddParameter("@ADDITIONAL_EXPENSE",System.DBNull.Value);
					else
						objDataWrapper.AddParameter("@ADDITIONAL_EXPENSE",objActivityExpenseInfo.ADDITIONAL_EXPENSE);

					objDataWrapper.AddParameter("@DRACCTS",objActivityExpenseInfo.DRACCTS);
					objDataWrapper.AddParameter("@CRACCTS",objActivityExpenseInfo.CRACCTS);
					objDataWrapper.AddParameter("@PAYMENT_METHOD",objActivityExpenseInfo.PAYMENT_METHOD);
					objDataWrapper.AddParameter("@CHECK_NUMBER",objActivityExpenseInfo.CHECK_NUMBER);
					objDataWrapper.AddParameter("@ACTUAL_RISK_ID",objActivityExpenseInfo.ACTUAL_RISK_ID);
					objDataWrapper.AddParameter("@ACTUAL_RISK_TYPE",objActivityExpenseInfo.ACTUAL_RISK_TYPE);

					//Added for Itrack Issue 7775 on 13 Aug 2010
					outstanding = outstanding + objActivityExpenseInfo.PAYMENT_AMOUNT;

					returnResult = objDataWrapper.ExecuteNonQuery(InsertCLM_ACTIVITY_EXPENSE);					
					objDataWrapper.ClearParameteres();
				}
		
				//Added for Itrack Issue 7775 on 13 Aug 2010
				if(TransactionLogRequired) 
				{
					string strTranXML = "";
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.CLIENT_ID		=	customerId;
					objTransactionInfo.POLICY_ID		=	policyId;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objclaimID.CREATED_BY;
					objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
					objTransactionInfo.TRANS_DESC		=	"Expense Details has been Added";						
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " + claimNumber + "; Activity # : " + objclaimID.ACTIVITY_ID + "; Total Payment = " + String.Format("{0:n}",outstanding);
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
					
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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
		/// <param name="objOldPaymentInfo">Model object having old information</param>
		/// <param name="objActivityExpenseInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ArrayList aPaymentList)
		{			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			int returnResult=0;
			try
			{
				//Added for Itrack Issue 7775 on 13 Aug 2010
				double outstanding = 0.0;
				string claimNumber = "";
				int customerId = 0,policyId = 0,policyVersionId = 0;

				DataSet ds = new DataSet();
				Cms.Model.Claims.ClsActivityExpenseInfo objclaimID = (ClsActivityExpenseInfo)aPaymentList[0];
				objDataWrapper.AddParameter("@CLAIM_ID",objclaimID.CLAIM_ID);
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				customerId = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				policyId = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
				objclaimID.CREATED_BY = objclaimID.CREATED_BY;

				for(int i = 0; i < aPaymentList.Count; i++ )
				{
					Cms.Model.Claims.ClsActivityExpenseInfo objActivityExpenseInfo = (ClsActivityExpenseInfo)aPaymentList[i];

					objDataWrapper.ClearParameteres();
					objDataWrapper.AddParameter("@CLAIM_ID",objActivityExpenseInfo.CLAIM_ID);
					objDataWrapper.AddParameter("@EXPENSE_ID",objActivityExpenseInfo.EXPENSE_ID);
					objDataWrapper.AddParameter("@RESERVE_ID",objActivityExpenseInfo.RESERVE_ID);
					objDataWrapper.AddParameter("@ACTIVITY_ID",objActivityExpenseInfo.ACTIVITY_ID);
					objDataWrapper.AddParameter("@PAYMENT_AMOUNT",objActivityExpenseInfo.PAYMENT_AMOUNT);
					objDataWrapper.AddParameter("@ACTION_ON_PAYMENT",objActivityExpenseInfo.ACTION_ON_PAYMENT);
					objDataWrapper.AddParameter("@MODIFIED_BY",objActivityExpenseInfo.MODIFIED_BY);
					objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objActivityExpenseInfo.LAST_UPDATED_DATETIME);
					objDataWrapper.AddParameter("@VEHICLE_ID",objActivityExpenseInfo.VEHICLE_ID);
					if(objActivityExpenseInfo.ADDITIONAL_EXPENSE==0 || objActivityExpenseInfo.ADDITIONAL_EXPENSE==0.0)
						objDataWrapper.AddParameter("@ADDITIONAL_EXPENSE",System.DBNull.Value);
					else
						objDataWrapper.AddParameter("@ADDITIONAL_EXPENSE",objActivityExpenseInfo.ADDITIONAL_EXPENSE);

					objDataWrapper.AddParameter("@DRACCTS",objActivityExpenseInfo.DRACCTS);
					objDataWrapper.AddParameter("@CRACCTS",objActivityExpenseInfo.CRACCTS);
					objDataWrapper.AddParameter("@PAYMENT_METHOD",objActivityExpenseInfo.PAYMENT_METHOD);
					objDataWrapper.AddParameter("@CHECK_NUMBER",objActivityExpenseInfo.CHECK_NUMBER);
					objDataWrapper.AddParameter("@ACTUAL_RISK_ID",objActivityExpenseInfo.ACTUAL_RISK_ID);
					objDataWrapper.AddParameter("@ACTUAL_RISK_TYPE",objActivityExpenseInfo.ACTUAL_RISK_TYPE);

					//Added for Itrack Issue 7775 on 13 Aug 2010
					outstanding = outstanding + objActivityExpenseInfo.PAYMENT_AMOUNT;

					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_ACTIVITY_EXPENSE);				
					objDataWrapper.ClearParameteres();
				}
	
				//Added for Itrack Issue 7775 on 13 Aug 2010
				if(TransactionLogRequired) 
				{		
					string strTranXML = "";
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.CLIENT_ID		=	customerId;
					objTransactionInfo.POLICY_ID		=	policyId;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objclaimID.CREATED_BY;
					objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
					objTransactionInfo.TRANS_DESC		=	"Expense Details has been updated";						
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " + claimNumber + "; Activity # : " + objclaimID.ACTIVITY_ID + "; Total Payment = " + String.Format("{0:n}",outstanding);
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
					
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
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

		#region "GetxmlMethods"
		
		public static DataSet GetOldDataForPageControls(string CLAIM_ID, string ACTIVITY_ID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",int.Parse(CLAIM_ID));
			objDataWrapper.AddParameter("@ACTIVITY_ID",int.Parse(ACTIVITY_ID));
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(GetCLM_ACTIVITY_EXPENSE);
			if(objDataSet!=null && objDataSet.Tables.Count>0)
				return objDataSet;
			else
				return null;
		}
		public static DataSet GetOldDataForHome(string CLAIM_ID, string ACTIVITY_ID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",int.Parse(CLAIM_ID));
			objDataWrapper.AddParameter("@ACTIVITY_ID",int.Parse(ACTIVITY_ID));
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(GetCLM_ACTIVITY_EXPENSE_HOME);
			if(objDataSet!=null && objDataSet.Tables.Count>0)
				return objDataSet;
			else
				return null;
		}
		#endregion

		#region Update Activity with the Payment Amount
		public static int UpdateReserveActivity(string strCLAIM_ID,string strACTIVITY_ID, string strPAYMENT_ACTION)
		{
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);
				objDataWrapper.AddParameter("@ACTIVITY_ID",strACTIVITY_ID);
				objDataWrapper.AddParameter("@PAYMENT_ACTION",strPAYMENT_ACTION);
				//objDataWrapper.AddParameter("@PAYEE_PARTIES_ID",strPAYEE);
				int returnResult = objDataWrapper.ExecuteNonQuery(UpdateReserveCLM_ACTIVITY);			
				return returnResult;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{}
		}
		#endregion

        public DataSet GetClaimAllParties(string CLAIM_ID, int ACTIVITY_ID,int RECOVERY_TYPE)
		{
			string strSql = "Proc_GetClaimAllParties";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",int.Parse(CLAIM_ID));
            objDataWrapper.AddParameter("@ACTIVITY_ID", ACTIVITY_ID);
            objDataWrapper.AddParameter("@RECOVERY_TYPE", RECOVERY_TYPE);
            
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}
	}
}
