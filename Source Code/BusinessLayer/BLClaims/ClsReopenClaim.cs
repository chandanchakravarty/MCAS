/******************************************************************************************
<Author					: -   Vijay Arora
<Start Date				: -	6/19/2006 3:49:11 PM
<End Date				: -	
<Description			: - 	Buiness Layer class for Reopen of the Claims
<Review Date			: - 
<Reviewed By			: - 	
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

namespace Cms.BusinessLayer.BLClaims
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsReopenClaim : Cms.BusinessLayer.BLClaims.ClsClaims
	{
		
		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objROC">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsReopenClaimInfo objROCInfo)
		{
			string		strStoredProc	=	"Proc_InsertCLM_REOPEN_CLAIM";
			DateTime	RecordDate		=	DateTime.Now;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CLAIM_ID",objROCInfo.CLAIM_ID);
				//Done for Itrack Issue 6932 on 1 Feb 2010
				DataSet ds = new DataSet();
				ds = objDataWrapper.ExecuteDataSet("Proc_GetCLM_CLAIM_INFO");
				string claimNumber = ds.Tables[0].Rows[0]["CLAIM_NUMBER"].ToString();
				int customerID = int.Parse(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				int policyID = int.Parse(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
				int policyVersionId = int.Parse(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				objDataWrapper.AddParameter("@REOPEN_DATE",objROCInfo.REOPEN_DATE);
				objDataWrapper.AddParameter("@REOPEN_BY",objROCInfo.REOPEN_BY);
				objDataWrapper.AddParameter("@REASON",objROCInfo.REASON);
				objDataWrapper.AddParameter("@CREATED_BY",objROCInfo.CREATED_BY);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@REOPEN_ID",objROCInfo.REOPEN_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired) 
				{
					objROCInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddReopenClaim.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objROCInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					//Done for Itrack Issue 6932 on 1 Feb 2010
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	policyID;
					objTransactionInfo.POLICY_VER_TRACKING_ID =	policyVersionId;
					objTransactionInfo.RECORDED_BY		=	objROCInfo.CREATED_BY;
					objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1946", "");//"Claim is Re-opened";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
                    objTransactionInfo.CUSTOM_INFO = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1810", "") +claimNumber;//Done for Itrack Issue 6932 on 1 Feb 2010
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					

				if(returnResult>0 && objSqlParameter.Value!="" && int.Parse(objSqlParameter.Value.ToString()) > 0)
				{
					objROCInfo.REOPEN_ID = int.Parse(objSqlParameter.Value.ToString());
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
		/// <param name="objOldROC">Model object having old information</param>
		/// <param name="objROC">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsReopenClaimInfo objOldROCInfo,ClsReopenClaimInfo objROCInfo)
		{
			string		strStoredProc	=	"Proc_UpdateCLM_REOPEN_CLAIM";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CLAIM_ID",objROCInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@REOPEN_ID",objROCInfo.REOPEN_ID);
				objDataWrapper.AddParameter("@REASON",objROCInfo.REASON);
				objDataWrapper.AddParameter("@MODIFIED_BY",objROCInfo.MODIFIED_BY);
				
				if(TransactionLogRequired) 
				{
					objROCInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddReopenClaim.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldROCInfo,objROCInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objROCInfo.MODIFIED_BY;
					objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1947", ""); //"Re-open claim reason is modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
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
		public string GetXmlForPageControls(string CLAIM_ID, string REOPEN_ID)
		{
			DataSet objDataSet = GetValuesForPageControls(CLAIM_ID,REOPEN_ID);
			return objDataSet.GetXml();
		}

		public DataSet GetValuesForPageControls(string CLAIM_ID, string REOPEN_ID)
		{
			string strSql = "Proc_GetValuesCLM_REOPEN_CLAIM";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",int.Parse(CLAIM_ID));
			objDataWrapper.AddParameter("@REOPEN_ID",int.Parse(REOPEN_ID));
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}
		#endregion
	}
}
