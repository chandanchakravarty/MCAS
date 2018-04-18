/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date				: -	6/1/2006 3:20:48 PM
<End Date				: -	
<Description				: - 	Business Layer Class for Claim Payee
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Claims;
using Cms.BusinessLayer.BlCommon;

namespace Cms.BusinessLayer.BLClaims
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsRecoveryPayer : Cms.BusinessLayer.BLClaims.ClsClaims
	{

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objPayeeInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsRecoveryPayerInfo objPayeeInfo)
		{
			string		strStoredProc	=	"Proc_InsertCLM_ACTIVITY_RECOVERY_PAYER";
			DateTime	RecordDate		=	DateTime.Now;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				//Done for Itrack Issue 6932 on 10 Feb 2010
				DataSet ds = new DataSet();
				objDataWrapper.AddParameter("@CLAIM_ID",objPayeeInfo.CLAIM_ID);
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
				
				objDataWrapper.AddParameter("@ACTIVITY_ID",objPayeeInfo.ACTIVITY_ID);		
				ds = objDataWrapper.ExecuteDataSet("Proc_GetXMLProcCLM_ACTIVITY");
				string []activityReason = ds.Tables[0].Rows[0]["ACTIVITY_REASON"].ToString().Split('^');
				
				objDataWrapper.AddParameter("@CREATED_BY",objPayeeInfo.CREATED_BY);				
				objDataWrapper.AddParameter("@CREATED_DATETIME",objPayeeInfo.CREATED_DATETIME);				
				objDataWrapper.AddParameter("@RECEIVED_FROM",objPayeeInfo.RECEIVED_FROM);				
				objDataWrapper.AddParameter("@RECEIVED_DATE",objPayeeInfo.RECEIVED_DATE);				
				objDataWrapper.AddParameter("@RECOVERY_TYPE",objPayeeInfo.RECOVERY_TYPE);				
				objDataWrapper.AddParameter("@DESCRIPTION",objPayeeInfo.DESCRIPTION);				
				objDataWrapper.AddParameter("@CHECK_NUMBER",objPayeeInfo.CHECK_NUMBER);				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@PAYER_ID",objPayeeInfo.PAYER_ID,SqlDbType.Int,ParameterDirection.Output);
				int returnResult = 0;
				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@DETAIL_TYPE_ID",int.Parse(activityReason[1].ToString()));
				ds = objDataWrapper.ExecuteDataSet("Proc_GetValuesCLM_TYPE_DETAIL");
				string activityDesc = ds.Tables[0].Rows[0]["DETAIL_TYPE_DESCRIPTION"].ToString();

				
				if(TransactionLogRequired) 
				{
					objPayeeInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddRecoveryPayer.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objPayeeInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					//Done for Itrack Issue 6932 on 10 Feb 2010
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objPayeeInfo.CREATED_BY;
					objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
					objTransactionInfo.TRANS_DESC		=	"Payer Details has been added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " +claimNumber + ";Activity # : " + objPayeeInfo.ACTIVITY_ID + ";Activity Description : " + activityDesc; //Done for Itrack Issue 6932 on 10 Feb 2010
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}
//				else
//					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					

				if(returnResult>0 && int.Parse(objSqlParameter.Value.ToString()) > 0)
				{
					objPayeeInfo.PAYER_ID = int.Parse(objSqlParameter.Value.ToString());
				}

				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

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

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldPayeeInfo">Model object having old information</param>
		/// <param name="objPayeeInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsRecoveryPayerInfo objOldPayeeInfo,ClsRecoveryPayerInfo objPayeeInfo,string strCALLED_FROM)
		{
			string		strStoredProc	=	"Proc_UpdateCLM_ACTIVITY_RECOVERY_PAYER";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				//Done for Itrack Issue 6932 on 10 Feb 2010
				DataSet ds = new DataSet();
				objDataWrapper.AddParameter("@CLAIM_ID",objPayeeInfo.CLAIM_ID);
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
				
				objDataWrapper.AddParameter("@ACTIVITY_ID",objPayeeInfo.ACTIVITY_ID);		
				ds = objDataWrapper.ExecuteDataSet("Proc_GetXMLProcCLM_ACTIVITY");
				string []activityReason = ds.Tables[0].Rows[0]["ACTIVITY_REASON"].ToString().Split('^');
			
				objDataWrapper.AddParameter("@PAYER_ID",objPayeeInfo.PAYER_ID);					
				objDataWrapper.AddParameter("@MODIFIED_BY",objPayeeInfo.MODIFIED_BY);				
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objPayeeInfo.LAST_UPDATED_DATETIME);				
				objDataWrapper.AddParameter("@RECEIVED_FROM",objPayeeInfo.RECEIVED_FROM);				
				objDataWrapper.AddParameter("@RECEIVED_DATE",objPayeeInfo.RECEIVED_DATE);				
				objDataWrapper.AddParameter("@RECOVERY_TYPE",objPayeeInfo.RECOVERY_TYPE);				
				objDataWrapper.AddParameter("@DESCRIPTION",objPayeeInfo.DESCRIPTION);				
				objDataWrapper.AddParameter("@CHECK_NUMBER",objPayeeInfo.CHECK_NUMBER);		
				
				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@DETAIL_TYPE_ID",int.Parse(activityReason[1].ToString()));
				ds = objDataWrapper.ExecuteDataSet("Proc_GetValuesCLM_TYPE_DETAIL");
				string activityDesc = ds.Tables[0].Rows[0]["DETAIL_TYPE_DESCRIPTION"].ToString();
				
				if(TransactionLogRequired) 
				{
					objPayeeInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddRecoveryPayer.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldPayeeInfo,objPayeeInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					//Done for Itrack Issue 6932 on 10 Feb 2010
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objPayeeInfo.MODIFIED_BY;
					objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
					objTransactionInfo.TRANS_DESC		=	"Payer Details has been modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	"Claim Number : " +claimNumber + ";Activity # : " + objPayeeInfo.ACTIVITY_ID + ";Activity Description : " + activityDesc; //Done for Itrack Issue 6932 on 10 Feb 2010
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}	
//				else
//					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					
				objDataWrapper.ClearParameteres();
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
		public DataSet GetValuesForPageControls(string CLAIM_ID, string ACTIVITY_ID,  string PAYER_ID)
		{
			string strSql = "Proc_GetValuesForCLM_ACTIVITY_RECOVERY_PAYER";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",int.Parse(CLAIM_ID));
			objDataWrapper.AddParameter("@ACTIVITY_ID",int.Parse(ACTIVITY_ID));			
			objDataWrapper.AddParameter("@PAYER_ID",int.Parse(PAYER_ID));
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}	
		public string GetXmlForPageControls(string CLAIM_ID, string ACTIVITY_ID, string PAYER_ID)
		{
			DataSet objDataSet = GetValuesForPageControls(CLAIM_ID,ACTIVITY_ID,PAYER_ID);
			return objDataSet.GetXml();
		}

		public string GetPartiesDefaultValues(string CLAIM_ID, string PARTY_ID, int LANG_ID)
		{
			string strTemp = "";
			int noCol = 0;
            DataSet dsTemp = ClsAddPartyDetails.GetValuesForParty(CLAIM_ID, PARTY_ID, LANG_ID);
			noCol = dsTemp.Tables[0].Columns.Count;

			if(dsTemp.Tables[0].Rows.Count>0)
			{
				for (int count = 0; count < noCol; count++)
				{
					strTemp += dsTemp.Tables[0].Rows[0][count];
					strTemp += "^";
				}
			}
			
			strTemp = strTemp.Substring(strTemp.Length,-1);
			return strTemp;
		}

		public DataSet GetPayeeDetails(string CLAIM_ID, string ACTIVITY_ID, string EXPENSE_ID)
		{
			string strSql = "Proc_GetPayeeNameReference";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",int.Parse(CLAIM_ID));
			objDataWrapper.AddParameter("@ACTIVITY_ID",int.Parse(ACTIVITY_ID));
			objDataWrapper.AddParameter("@EXPENSE_ID",int.Parse(EXPENSE_ID));
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}
		public DataSet GetPayeeDetailsForPayment(string CLAIM_ID, string ACTIVITY_ID)
		{
			string strSql = "Proc_GetPayeeNameReferenceForPayment";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",int.Parse(CLAIM_ID));
			objDataWrapper.AddParameter("@ACTIVITY_ID",int.Parse(ACTIVITY_ID));			
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}
		#endregion
	}
}
