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
	public class ClsPayment : Cms.BusinessLayer.BLClaims.ClsClaims
	{

		private const	string		InsertCLM_ACTIVITY_PAYMENT		=	"Proc_InsertCLM_ACTIVITY_PAYMENT";
		private const	string		UpdateCLM_ACTIVITY_PAYMENT		=	"Proc_UpdateCLM_ACTIVITY_PAYMENT";
		private const	string		GetCLM_ACTIVITY_PAYMENT			=	"Proc_GetCLM_ACTIVITY_PAYMENT";
		private const	string		GetCLM_ACTIVITY_PAYMENT_HOME	=	"Proc_GetCLM_ACTIVITY_PAYMENT_HOME";
		private const	string		UpdateReserveCLM_ACTIVITY		=	"Proc_UpdateReserveCLM_ACTIVITY";
		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsPayment()
		{}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objPaymentInfo">Model class object.</param>
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
				Cms.Model.Claims.ClsPaymentInfo objclaimID = (ClsPaymentInfo)aPaymentList[0];
				objDataWrapper.AddParameter("@CLAIM_ID",objclaimID.CLAIM_ID);
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				customerId = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				policyId = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				for(int i = 0; i < aPaymentList.Count; i++ )
				{
					Cms.Model.Claims.ClsPaymentInfo objPaymentInfo = (ClsPaymentInfo)aPaymentList[i];

					objDataWrapper.ClearParameteres();
					objDataWrapper.AddParameter("@CLAIM_ID",objPaymentInfo.CLAIM_ID);
					objDataWrapper.AddParameter("@RESERVE_ID",objPaymentInfo.RESERVE_ID);
					objDataWrapper.AddParameter("@ACTIVITY_ID",objPaymentInfo.ACTIVITY_ID);
					objDataWrapper.AddParameter("@PAYMENT_AMOUNT",objPaymentInfo.PAYMENT_AMOUNT);
					objDataWrapper.AddParameter("@ACTION_ON_PAYMENT",objPaymentInfo.ACTION_ON_PAYMENT);
					objDataWrapper.AddParameter("@CREATED_BY",objPaymentInfo.CREATED_BY);
					objDataWrapper.AddParameter("@CREATED_DATETIME",objPaymentInfo.CREATED_DATETIME);
					objDataWrapper.AddParameter("@VEHICLE_ID",objPaymentInfo.VEHICLE_ID);

					objDataWrapper.AddParameter("@DRACCTS",objPaymentInfo.DRACCTS);
					objDataWrapper.AddParameter("@CRACCTS",objPaymentInfo.CRACCTS);
					objDataWrapper.AddParameter("@PAYMENT_METHOD",objPaymentInfo.PAYMENT_METHOD);
					objDataWrapper.AddParameter("@CHECK_NUMBER",objPaymentInfo.CHECK_NUMBER);
					objDataWrapper.AddParameter("@ACTUAL_RISK_ID",objPaymentInfo.ACTUAL_RISK_ID);
					objDataWrapper.AddParameter("@ACTUAL_RISK_TYPE",objPaymentInfo.ACTUAL_RISK_TYPE);
					
					//Added for Itrack Issue 7775 on 13 Aug 2010
					outstanding = outstanding + objPaymentInfo.PAYMENT_AMOUNT;

					returnResult = objDataWrapper.ExecuteNonQuery(InsertCLM_ACTIVITY_PAYMENT);					
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
					objTransactionInfo.TRANS_DESC		=	"Payment Details has been Added";						
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
		/// <param name="objPaymentInfo">Model object having new information(form control's value)</param>
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
				Cms.Model.Claims.ClsPaymentInfo objclaimID = (ClsPaymentInfo)aPaymentList[0];
				objDataWrapper.AddParameter("@CLAIM_ID",objclaimID.CLAIM_ID);
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				customerId = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				policyId = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				for(int i = 0; i < aPaymentList.Count; i++ )
				{
					Cms.Model.Claims.ClsPaymentInfo objPaymentInfo = (ClsPaymentInfo)aPaymentList[i];

					objDataWrapper.ClearParameteres();
					objDataWrapper.AddParameter("@CLAIM_ID",objPaymentInfo.CLAIM_ID);
					objDataWrapper.AddParameter("@PAYMENT_ID",objPaymentInfo.PAYMENT_ID);
					objDataWrapper.AddParameter("@RESERVE_ID",objPaymentInfo.RESERVE_ID);
					objDataWrapper.AddParameter("@ACTIVITY_ID",objPaymentInfo.ACTIVITY_ID);
					objDataWrapper.AddParameter("@PAYMENT_AMOUNT",objPaymentInfo.PAYMENT_AMOUNT);
					objDataWrapper.AddParameter("@ACTION_ON_PAYMENT",objPaymentInfo.ACTION_ON_PAYMENT);
					objDataWrapper.AddParameter("@MODIFIED_BY",objPaymentInfo.MODIFIED_BY);
					objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objPaymentInfo.LAST_UPDATED_DATETIME);
					objDataWrapper.AddParameter("@VEHICLE_ID",objPaymentInfo.VEHICLE_ID);

					objDataWrapper.AddParameter("@DRACCTS",objPaymentInfo.DRACCTS);
					objDataWrapper.AddParameter("@CRACCTS",objPaymentInfo.CRACCTS);
					objDataWrapper.AddParameter("@PAYMENT_METHOD",objPaymentInfo.PAYMENT_METHOD);
					objDataWrapper.AddParameter("@CHECK_NUMBER",objPaymentInfo.CHECK_NUMBER);
					objDataWrapper.AddParameter("@ACTUAL_RISK_ID",objPaymentInfo.ACTUAL_RISK_ID);
					objDataWrapper.AddParameter("@ACTUAL_RISK_TYPE",objPaymentInfo.ACTUAL_RISK_TYPE);

					//Added for Itrack Issue 7775 on 13 Aug 2010
					outstanding = outstanding + objPaymentInfo.PAYMENT_AMOUNT;

					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_ACTIVITY_PAYMENT);				
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
					objTransactionInfo.TRANS_DESC		=	"Payment Details has been Updated";						
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
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(GetCLM_ACTIVITY_PAYMENT);
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
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(GetCLM_ACTIVITY_PAYMENT_HOME);
			if(objDataSet!=null && objDataSet.Tables.Count>0)
				return objDataSet;
			else
				return null;
		}
		#endregion

		public void PopulatePaymentMethodDropDown(System.Web.UI.WebControls.DropDownList objDropDown,string LookupName,string ValueToRemove)
		{
			objDropDown.DataSource =  Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup(LookupName);  //Action on Payment Lookup
			objDropDown.DataTextField="LookupDesc";
			objDropDown.DataValueField="LookupID";
			objDropDown.DataBind();
			if(ValueToRemove!="")
				ClsCommon.RemoveOptionFromDropdownByValue(objDropDown,ValueToRemove);
		}


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
	}
}
