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
	public class ClsPayee : Cms.BusinessLayer.BLClaims.ClsClaims
	{

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objPayeeInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
        public int Add(ClsPayeeInfo objPayeeInfo, int ActivityReason)
		{
			string		strStoredProc	=	"Proc_InsertCLM_PAYEE";
			DateTime	RecordDate		=	DateTime.Now;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				//Done for Itrack Issue 6932 on 19 Jan 2010
				DataSet ds = new DataSet();
				objDataWrapper.AddParameter("@CLAIM_ID",objPayeeInfo.CLAIM_ID);
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				objDataWrapper.AddParameter("@ACTIVITY_ID",objPayeeInfo.ACTIVITY_ID);
				//Done for Itrack Issue 6932 on 1 Feb 2010
				ds = objDataWrapper.ExecuteDataSet("Proc_GetXMLProcCLM_ACTIVITY");
				string []activityReason = ds.Tables[0].Rows[0]["ACTIVITY_REASON"].ToString().Split('^');
				//string activityDesc = activityReason[1].ToString();

				objDataWrapper.AddParameter("@EXPENSE_ID",objPayeeInfo.EXPENSE_ID);
				//objDataWrapper.AddParameter("@PAYEE_ACTIVITY_ID",objPayeeInfo.PAYEE_ACTIVITY_ID);
				objDataWrapper.AddParameter("@PARTY_ID",objPayeeInfo.PARTY_ID);
               objDataWrapper.AddParameter("@PAYMENT_METHOD", objPayeeInfo.PAYMENT_METHOD);
				objDataWrapper.AddParameter("@ADDRESS1",objPayeeInfo.ADDRESS1);
				objDataWrapper.AddParameter("@ADDRESS2",objPayeeInfo.ADDRESS2);
				objDataWrapper.AddParameter("@CITY",objPayeeInfo.CITY);
				objDataWrapper.AddParameter("@STATE",objPayeeInfo.STATE);
				objDataWrapper.AddParameter("@ZIP",objPayeeInfo.ZIP);
				objDataWrapper.AddParameter("@COUNTRY",objPayeeInfo.COUNTRY);
				objDataWrapper.AddParameter("@NARRATIVE",objPayeeInfo.NARRATIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objPayeeInfo.CREATED_BY);
				objDataWrapper.AddParameter("@AMOUNT",objPayeeInfo.AMOUNT);
                objDataWrapper.AddParameter("@ACTIVITY_REASON", ActivityReason);
				//Added PAYEE_PARTY_ID For Itrack 5124
				objDataWrapper.AddParameter("@PAYEE_PARTY_ID",objPayeeInfo.PAYEE_PARTY_ID);
				//objDataWrapper.AddParameter("@INVOICED_BY",objPayeeInfo.INVOICED_BY);
				objDataWrapper.AddParameter("@INVOICE_NUMBER",objPayeeInfo.INVOICE_NUMBER);
				if(objPayeeInfo.INVOICE_DATE.Ticks!=0)
					objDataWrapper.AddParameter("@INVOICE_DATE",objPayeeInfo.INVOICE_DATE);
				else
					objDataWrapper.AddParameter("@INVOICE_DATE",null);

                if (objPayeeInfo.INVOICE_DUE_DATE.Ticks != 0)
                    objDataWrapper.AddParameter("@INVOICE_DUE_DATE", objPayeeInfo.INVOICE_DUE_DATE);
                else
                    objDataWrapper.AddParameter("@INVOICE_DUE_DATE", null);
                objDataWrapper.AddParameter("@INVOICE_SERIAL_NUMBER", objPayeeInfo.INVOICE_SERIAL_NUMBER);
       

				objDataWrapper.AddParameter("@SERVICE_TYPE",objPayeeInfo.SERVICE_TYPE);
				objDataWrapper.AddParameter("@SERVICE_DESCRIPTION",objPayeeInfo.SERVICE_DESCRIPTION);
				objDataWrapper.AddParameter("@SECONDARY_PARTY_ID",objPayeeInfo.SECONDARY_PARTY_ID);
				objDataWrapper.AddParameter("@FIRST_NAME",objPayeeInfo.FIRST_NAME);
				objDataWrapper.AddParameter("@LAST_NAME",objPayeeInfo.LAST_NAME);
				objDataWrapper.AddParameter("@TO_ORDER_DESC",objPayeeInfo.TO_ORDER_DESC);
                objDataWrapper.AddParameter("@REIN_RECOVERY_NUMBER", objPayeeInfo.REIN_RECOVERY_NUMBER);
                objDataWrapper.AddParameter("@RECOVERY_TYPE", objPayeeInfo.RECOVERY_TYPE);	

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@PAYEE_ID",objPayeeInfo.PAYEE_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@DETAIL_TYPE_ID",int.Parse(activityReason[1].ToString()));
				ds = objDataWrapper.ExecuteDataSet("Proc_GetValuesCLM_TYPE_DETAIL");
				string activityDesc = ds.Tables[0].Rows[0]["DETAIL_TYPE_DESCRIPTION"].ToString();

				if(TransactionLogRequired) 
				{
					//Done for Itrack Issue 6932 on 1 Feb 2010
					//if(activityReason[0].ToString() == "11775")
						objPayeeInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddPayee.aspx.resx");
					//else if(activityReason[0] == "11774")
						//objPayeeInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddExpensePayee.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objPayeeInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					//Done for Itrack Issue 6932 on 19 Jan 2010
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objPayeeInfo.CREATED_BY;
					objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1854", "");// "New Payee Details is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
                    objTransactionInfo.CUSTOM_INFO = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1794","")+" "+ claimNumber + ";"+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1815","")+" # : " + objPayeeInfo.ACTIVITY_ID + ";"+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1855","")+" : " + activityDesc;//"Claim Number : " +claimNumber + ";Activity # : " + objPayeeInfo.ACTIVITY_ID + ";Activity Description : " + activityDesc; //Done for Itrack Issue 6932 on 1 Feb 2010
					objDataWrapper.ExecuteNonQuery(objTransactionInfo);
				}
//				else
//					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
	
				if(returnResult>0 && int.Parse(objSqlParameter.Value.ToString()) > 0)
				{
					objPayeeInfo.PAYEE_ID = int.Parse(objSqlParameter.Value.ToString());
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
		public int Update(ClsPayeeInfo objOldPayeeInfo,ClsPayeeInfo objPayeeInfo,int ActivityReason)
		{
			string		strStoredProc	=	"Proc_UpdateCLM_PAYEE";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				//Done for Itrack Issue 6932 on 15 Jan 2010
				DataSet ds = new DataSet();
				objDataWrapper.AddParameter("@CLAIM_ID",objPayeeInfo.CLAIM_ID);
				//Done for Itrack Issue 6932 on 1 Feb 2010
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				objDataWrapper.AddParameter("@ACTIVITY_ID",objPayeeInfo.ACTIVITY_ID);
				ds = objDataWrapper.ExecuteDataSet("Proc_GetXMLProcCLM_ACTIVITY");
				string []activityReason = ds.Tables[0].Rows[0]["ACTIVITY_REASON"].ToString().Split('^');
				//string activityDesc = activityReason[1].ToString();

				objDataWrapper.AddParameter("@EXPENSE_ID",objPayeeInfo.EXPENSE_ID);
				objDataWrapper.AddParameter("@PAYEE_ID",objPayeeInfo.PAYEE_ID);
				objDataWrapper.AddParameter("@PARTY_ID",objPayeeInfo.PARTY_ID);
         		objDataWrapper.AddParameter("@PAYMENT_METHOD",objPayeeInfo.PAYMENT_METHOD);
				objDataWrapper.AddParameter("@ADDRESS1",objPayeeInfo.ADDRESS1);
				objDataWrapper.AddParameter("@ADDRESS2",objPayeeInfo.ADDRESS2);
				objDataWrapper.AddParameter("@CITY",objPayeeInfo.CITY);
				objDataWrapper.AddParameter("@STATE",objPayeeInfo.STATE);
				objDataWrapper.AddParameter("@ZIP",objPayeeInfo.ZIP);
				objDataWrapper.AddParameter("@COUNTRY",objPayeeInfo.COUNTRY);
				objDataWrapper.AddParameter("@NARRATIVE",objPayeeInfo.NARRATIVE);
				objDataWrapper.AddParameter("@MODIFIED_BY",objPayeeInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@AMOUNT",objPayeeInfo.AMOUNT);
				objDataWrapper.AddParameter("@PAYEE_PARTY_ID",objPayeeInfo.PAYEE_PARTY_ID);
                objDataWrapper.AddParameter("@ACTIVITY_REASON", ActivityReason);
				//objDataWrapper.AddParameter("@INVOICED_BY",objPayeeInfo.INVOICED_BY);
				objDataWrapper.AddParameter("@INVOICE_NUMBER",objPayeeInfo.INVOICE_NUMBER);
				if(objPayeeInfo.INVOICE_DATE.Ticks!=0)
					objDataWrapper.AddParameter("@INVOICE_DATE",objPayeeInfo.INVOICE_DATE);
				else
					objDataWrapper.AddParameter("@INVOICE_DATE",null);

                if (objPayeeInfo.INVOICE_DUE_DATE.Ticks != 0)
                    objDataWrapper.AddParameter("@INVOICE_DUE_DATE", objPayeeInfo.INVOICE_DUE_DATE);
                else
                    objDataWrapper.AddParameter("@INVOICE_DUE_DATE", null);
                objDataWrapper.AddParameter("@INVOICE_SERIAL_NUMBER", objPayeeInfo.INVOICE_SERIAL_NUMBER);
       

				objDataWrapper.AddParameter("@SERVICE_TYPE",objPayeeInfo.SERVICE_TYPE);
				objDataWrapper.AddParameter("@SERVICE_DESCRIPTION",objPayeeInfo.SERVICE_DESCRIPTION);
				objDataWrapper.AddParameter("@SECONDARY_PARTY_ID",objPayeeInfo.SECONDARY_PARTY_ID);
				objDataWrapper.AddParameter("@FIRST_NAME",objPayeeInfo.FIRST_NAME);
				objDataWrapper.AddParameter("@LAST_NAME",objPayeeInfo.LAST_NAME);
				objDataWrapper.AddParameter("@TO_ORDER_DESC",objPayeeInfo.TO_ORDER_DESC);
                objDataWrapper.AddParameter("@REIN_RECOVERY_NUMBER", objPayeeInfo.REIN_RECOVERY_NUMBER);
                objDataWrapper.AddParameter("@RECOVERY_TYPE", objPayeeInfo.RECOVERY_TYPE);	
				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@DETAIL_TYPE_ID",int.Parse(activityReason[1].ToString()));
				ds = objDataWrapper.ExecuteDataSet("Proc_GetValuesCLM_TYPE_DETAIL");
				string activityDesc = ds.Tables[0].Rows[0]["DETAIL_TYPE_DESCRIPTION"].ToString();

				if(TransactionLogRequired) 
				{
					//Done for Itrack Issue 6932 on 1 Feb 2010
                    //if(activityReason[0].ToString() == "11775")
						objPayeeInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddPayee.aspx.resx");
                    //else if(activityReason[0] == "11774")
                    //    objPayeeInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddExpensePayee.aspx.resx");
					
					strTranXML = objBuilder.GetTransactionLogXML(objOldPayeeInfo,objPayeeInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					//Done for Itrack Issue 6932 on 1 Feb 2010
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objPayeeInfo.MODIFIED_BY;
					objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1856", "");// "Payee Details is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
                    objTransactionInfo.CUSTOM_INFO = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1794","")+" "+ claimNumber + ";"+ Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1815","")+"# : " + objPayeeInfo.ACTIVITY_ID + ";"+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1855","") +": " + activityDesc; //"Claim Number : " + claimNumber + ";Activity # : " + objPayeeInfo.ACTIVITY_ID + ";Activity Description : " + activityDesc; //Done for Itrack Issue 6932 on 1 Feb 2010
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
		public DataSet GetValuesForPageControls(string CLAIM_ID, string ACTIVITY_ID,   int LangID)
		{
			string strSql = "Proc_GetValuesForCLM_PAYEE";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",int.Parse(CLAIM_ID));
			objDataWrapper.AddParameter("@ACTIVITY_ID",int.Parse(ACTIVITY_ID));
			//objDataWrapper.AddParameter("@EXPENSE_ID",int.Parse(EXPENSE_ID));
            // MODIFIED BY SANTOSH KR GAUTAM ON 19 JUL 2011 (REF ITRACK :1029)
			//objDataWrapper.AddParameter("@PAYEE_ID",int.Parse(PAYEE_ID));
            objDataWrapper.AddParameter("@LANG_ID", LangID);

			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}
        public string GetXmlForPageControls(string CLAIM_ID, string ACTIVITY_ID, string PAYEE_ID, int LangID)
		{
            DataSet objDataSet = GetValuesForPageControls(CLAIM_ID, ACTIVITY_ID,  LangID);
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

		#region Check for existence of a payee against a claim
		public int AnyPayeeForClaim(string strClaimId, string strActivityId)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objDataWrapper.AddParameter("@CLAIM_ID",strClaimId);
				objDataWrapper.AddParameter("@ACTIVITY_ID",strActivityId);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RETURN_VAL",null,SqlDbType.Int,ParameterDirection.ReturnValue);
				objDataWrapper.ExecuteNonQuery("Proc_CheckPayeeForClaim");
				if(Convert.ToInt32(objSqlParameter.Value.ToString())>0)
					return Convert.ToInt32(objSqlParameter.Value.ToString());
						else
					return -1;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		#endregion
	}
}
