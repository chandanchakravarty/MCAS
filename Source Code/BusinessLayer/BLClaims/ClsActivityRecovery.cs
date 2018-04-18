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
using System.Web.UI.WebControls;

namespace Cms.BusinessLayer.BLClaims
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsActivityRecovery : Cms.BusinessLayer.BLClaims.ClsClaims
	{

		private const	string		InsertCLM_ACTIVITY_RECOVERY		=	"Proc_InsertCLM_ACTIVITY_RECOVERY";
		private const	string		UpdateCLM_ACTIVITY_RECOVERY		=	"Proc_UpdateCLM_ACTIVITY_RECOVERY";
		private const	string		GetCLM_ACTIVITY_RECOVERY		=	"Proc_GetCLM_ACTIVITY_RECOVERY";
		private const	string		GetCLM_ACTIVITY_RECOVERY_HOME	=	"Proc_GetCLM_ACTIVITY_RECOVERY_HOME";
		private const	string		UpdateRecoveryCLM_ACTIVITY		=	"Proc_UpdateRecoveryCLM_ACTIVITY";
		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsActivityRecovery()
		{}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objActivityRecoveryInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ArrayList aRecoveryList)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			int returnResult=0;
			try
			{
				//Added for Itrack Issue 7775 on 13 Aug 2010
				double outstanding = 0.0;
				string claimNumber = "";
				int customerId = 0,policyId = 0,policyVersionId = 0,createdBy = 0,activityId = 0;

				DataSet ds = new DataSet();
				Cms.Model.Claims.ClsActivityRecoveryInfo objclaimID = (ClsActivityRecoveryInfo)aRecoveryList[0];
				objDataWrapper.AddParameter("@CLAIM_ID",objclaimID.CLAIM_ID);
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				customerId = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				policyId = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
				createdBy = objclaimID.CREATED_BY;
				activityId = objclaimID.ACTIVITY_ID;

				for(int i = 0; i < aRecoveryList.Count; i++ )
				{
					Cms.Model.Claims.ClsActivityRecoveryInfo objActivityRecoveryInfo = (ClsActivityRecoveryInfo)aRecoveryList[i];

					objDataWrapper.ClearParameteres();
					objDataWrapper.AddParameter("@CLAIM_ID",objActivityRecoveryInfo.CLAIM_ID);
					objDataWrapper.AddParameter("@RESERVE_ID",objActivityRecoveryInfo.RESERVE_ID);
					objDataWrapper.AddParameter("@ACTIVITY_ID",objActivityRecoveryInfo.ACTIVITY_ID);
					objDataWrapper.AddParameter("@AMOUNT",objActivityRecoveryInfo.AMOUNT);
					objDataWrapper.AddParameter("@ACTION_ON_RECOVERY",objActivityRecoveryInfo.ACTION_ON_RECOVERY);
					objDataWrapper.AddParameter("@CREATED_BY",objActivityRecoveryInfo.CREATED_BY);
					objDataWrapper.AddParameter("@CREATED_DATETIME",objActivityRecoveryInfo.CREATED_DATETIME);
					objDataWrapper.AddParameter("@VEHICLE_ID",objActivityRecoveryInfo.VEHICLE_ID);

					objDataWrapper.AddParameter("@DRACCTS",objActivityRecoveryInfo.DRACCTS);
					objDataWrapper.AddParameter("@CRACCTS",objActivityRecoveryInfo.CRACCTS);
					objDataWrapper.AddParameter("@PAYMENT_METHOD",objActivityRecoveryInfo.PAYMENT_METHOD);
					objDataWrapper.AddParameter("@CHECK_NUMBER",objActivityRecoveryInfo.CHECK_NUMBER);
					objDataWrapper.AddParameter("@ACTUAL_RISK_ID",objActivityRecoveryInfo.ACTUAL_RISK_ID);
					objDataWrapper.AddParameter("@ACTUAL_RISK_TYPE",objActivityRecoveryInfo.ACTUAL_RISK_TYPE);

					//Added for Itrack Issue 7775 on 13 Aug 2010
					outstanding = outstanding + objActivityRecoveryInfo.AMOUNT;

					returnResult = objDataWrapper.ExecuteNonQuery(InsertCLM_ACTIVITY_RECOVERY);					
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
					objTransactionInfo.TRANS_DESC		=	"Recovery Details has been Added";						
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
		/// <param name="objOldActivityRecoveryInfo">Model object having old information</param>
		/// <param name="objActivityRecoveryInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ArrayList aRecoveryList)
		{			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			int returnResult=0;
			try
			{
				//Added for Itrack Issue 7775 on 13 Aug 2010
				double outstanding = 0.0;
				string claimNumber = "";
				int customerId = 0,policyId = 0,policyVersionId = 0,createdBy = 0,activityId = 0;

				DataSet ds = new DataSet();
				Cms.Model.Claims.ClsActivityRecoveryInfo objclaimID = (ClsActivityRecoveryInfo)aRecoveryList[0];
				objDataWrapper.AddParameter("@CLAIM_ID",objclaimID.CLAIM_ID);
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				customerId = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				policyId = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
				createdBy = objclaimID.CREATED_BY;
				activityId = objclaimID.ACTIVITY_ID;

				for(int i = 0; i < aRecoveryList.Count; i++ )
				{
					Cms.Model.Claims.ClsActivityRecoveryInfo objActivityRecoveryInfo = (ClsActivityRecoveryInfo)aRecoveryList[i];

					objDataWrapper.ClearParameteres();
					objDataWrapper.AddParameter("@CLAIM_ID",objActivityRecoveryInfo.CLAIM_ID);
					objDataWrapper.AddParameter("@RECOVERY_ID",objActivityRecoveryInfo.RECOVERY_ID);
					objDataWrapper.AddParameter("@RESERVE_ID",objActivityRecoveryInfo.RESERVE_ID);
					objDataWrapper.AddParameter("@ACTIVITY_ID",objActivityRecoveryInfo.ACTIVITY_ID);
					objDataWrapper.AddParameter("@AMOUNT",objActivityRecoveryInfo.AMOUNT);
					objDataWrapper.AddParameter("@ACTION_ON_RECOVERY",objActivityRecoveryInfo.ACTION_ON_RECOVERY);
					objDataWrapper.AddParameter("@MODIFIED_BY",objActivityRecoveryInfo.MODIFIED_BY);
					objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objActivityRecoveryInfo.LAST_UPDATED_DATETIME);
					objDataWrapper.AddParameter("@VEHICLE_ID",objActivityRecoveryInfo.VEHICLE_ID);

					objDataWrapper.AddParameter("@DRACCTS",objActivityRecoveryInfo.DRACCTS);
					objDataWrapper.AddParameter("@CRACCTS",objActivityRecoveryInfo.CRACCTS);
					objDataWrapper.AddParameter("@PAYMENT_METHOD",objActivityRecoveryInfo.PAYMENT_METHOD);
					objDataWrapper.AddParameter("@CHECK_NUMBER",objActivityRecoveryInfo.CHECK_NUMBER);
					objDataWrapper.AddParameter("@ACTUAL_RISK_ID",objActivityRecoveryInfo.ACTUAL_RISK_ID);
					objDataWrapper.AddParameter("@ACTUAL_RISK_TYPE",objActivityRecoveryInfo.ACTUAL_RISK_TYPE);

					//Added for Itrack Issue 7775 on 13 Aug 2010
					outstanding = outstanding + objActivityRecoveryInfo.AMOUNT;

					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_ACTIVITY_RECOVERY);				
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
					objTransactionInfo.TRANS_DESC		=	"Recovery Details has been Updated";						
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
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(GetCLM_ACTIVITY_RECOVERY);
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
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(GetCLM_ACTIVITY_RECOVERY_HOME);
			if(objDataSet!=null && objDataSet.Tables.Count>0)
				return objDataSet;
			else
				return null;
		}
		#endregion


		public int LoadAcntgDropDowns(System.Web.UI.WebControls.DropDownList cmbDrAccts,System.Web.UI.WebControls.DropDownList cmbCrAccts, string ACTION_ON_PAYMENT)
		{
			//DataSet DS = BusinessLayer.BLClaims.ClsDefaultValues.GetLedgerAcctsForClaims(Convert.ToInt32(cmbACTION_ON_PAYMENT.SelectedValue));
			DataSet DS = BusinessLayer.BLClaims.ClsDefaultValues.GetLedgerAcctsForClaims(Convert.ToInt32(ACTION_ON_PAYMENT));
			if(DS==null || DS.Tables[0]==null || DS.Tables[0].Rows.Count<1)
			{
				return -1;
			}
			//If more then one acct exist then add a BLANK Option and default BLANK.
			//If single account exists then default that and disable dropdown

			cmbDrAccts.DataSource=DS.Tables[0];
			cmbDrAccts.DataTextField="DESC_TO_SHOW";
			cmbDrAccts.DataValueField="ACCOUNT_ID";
			cmbDrAccts.DataBind();

			ListItem lItem = new ListItem("","");
			if (DS.Tables[0].Rows.Count > 1)
			{
				//ListItem LI_DR = new ListItem("","");
				//cmbDrAccts.Items.Add(LI_DR);
				cmbDrAccts.Items.Insert(0,lItem);
				//cmbDrAccts.ClearSelection();				
				//cmbDrAccts.Items.FindByValue("").Selected = true;
			}
			else
			{
				cmbDrAccts.Enabled=false;
			}

			
			cmbCrAccts.DataSource = DS.Tables[1];
			cmbCrAccts.DataTextField="DESC_TO_SHOW";
			cmbCrAccts.DataValueField="ACCOUNT_ID";
			cmbCrAccts.DataBind();


			if (DS.Tables[0].Rows.Count > 1)
			{
				//ListItem LI_CR = new ListItem();
				//cmbCrAccts.Items.Add(LI_CR);
				cmbCrAccts.Items.Insert(0,lItem);
				//cmbCrAccts.ClearSelection();
				//cmbCrAccts.Items.FindByValue("").Selected = true;
			}
			else
			{
				cmbCrAccts.Enabled=false;
			}
			return 1;
			//GetOldData();
		}		


		#region Update Activity with the Payment Amount
		public static int UpdateRecoveryActivity(string strCLAIM_ID,string strACTIVITY_ID)
		{
			try
			{
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CLAIM_ID",strCLAIM_ID);
				objDataWrapper.AddParameter("@ACTIVITY_ID",strACTIVITY_ID);				
				int returnResult = objDataWrapper.ExecuteNonQuery(UpdateRecoveryCLM_ACTIVITY);			
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
