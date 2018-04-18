/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date				: -	6/6/2006 10:05:47 AM
<End Date				: -	
<Description				: - 	Business Layer Class for Claims Expense Activity Breakdown
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
namespace Cms
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsActivityExpenseBreakdown : Cms.BusinessLayer.BLClaims.ClsClaims
	{
		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objAEBInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsActivityExpenseBreakdownInfo objAEBInfo)
		{
			string		strStoredProc	=	"Proc_InsertCLM_ACTIVITY_EXPENSE_BREAKDOWN";
			DateTime	RecordDate		=	DateTime.Now;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CLAIM_ID",objAEBInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@EXPENSE_ID",objAEBInfo.EXPENSE_ID);
				objDataWrapper.AddParameter("@TRANSACTION_CODE",objAEBInfo.TRANSACTION_CODE);
				objDataWrapper.AddParameter("@COVERAGE_ID",objAEBInfo.COVERAGE_ID);
				objDataWrapper.AddParameter("@PAID_AMOUNT",objAEBInfo.PAID_AMOUNT);
				objDataWrapper.AddParameter("@CREATED_BY",objAEBInfo.CREATED_BY);
				objDataWrapper.AddParameter("@ACTIVITY_ID",objAEBInfo.ACTIVITY_ID);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@EXPENSE_BREAKDOWN_ID",objAEBInfo.EXPENSE_BREAKDOWN_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired) 
				{
					objAEBInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddActivityExpenseBreakdown.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objAEBInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objAEBInfo.CREATED_BY;
					objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
					objTransactionInfo.TRANS_DESC		=	"New Expense Activity Breakdown is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
					
				if(returnResult>0 && int.Parse(objSqlParameter.Value.ToString()) > 0)
				{			
					objAEBInfo.EXPENSE_BREAKDOWN_ID = int.Parse(objSqlParameter.Value.ToString());
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
		/// <param name="objOldAEBInfo">Model object having old information</param>
		/// <param name="objAEBInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsActivityExpenseBreakdownInfo objOldAEBInfo,ClsActivityExpenseBreakdownInfo objAEBInfo)
		{
			string		strStoredProc	=	"Proc_UpdateCLM_ACTIVITY_EXPENSE_BREAKDOWN";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CLAIM_ID",objAEBInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@EXPENSE_ID",objAEBInfo.EXPENSE_ID);
				objDataWrapper.AddParameter("@EXPENSE_BREAKDOWN_ID",objAEBInfo.EXPENSE_BREAKDOWN_ID);
				objDataWrapper.AddParameter("@TRANSACTION_CODE",objAEBInfo.TRANSACTION_CODE);
				objDataWrapper.AddParameter("@COVERAGE_ID",objAEBInfo.COVERAGE_ID);
				objDataWrapper.AddParameter("@PAID_AMOUNT",objAEBInfo.PAID_AMOUNT);
				objDataWrapper.AddParameter("@MODIFIED_BY",objAEBInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@ACTIVITY_ID",objAEBInfo.ACTIVITY_ID);
				
				if(TransactionLogRequired) 
				{
					objAEBInfo.TransactLabel = ClsCommon.MapTransactionLabel("Claims/Aspx/AddActivityExpenseBreakdown.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldAEBInfo,objAEBInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objAEBInfo.MODIFIED_BY;
					objTransactionInfo.RECORD_DATE_TIME = DateTime.Now;
					objTransactionInfo.TRANS_DESC		=	"Expense Breakdown Activity is modified";
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
		public DataSet GetValuesForPageControls(string CLAIM_ID, string EXPENSE_ID, string EXPENSE_BREAKDOWN_ID, string ACTIVITY_ID)
		{
			string strSql = "Proc_GetValuesForCLM_ACTIVITY_EXPENSE_BREAKDOWN";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",int.Parse(CLAIM_ID));
			objDataWrapper.AddParameter("@EXPENSE_ID",int.Parse(EXPENSE_ID));
			objDataWrapper.AddParameter("@EXPENSE_BREAKDOWN_ID",int.Parse(EXPENSE_BREAKDOWN_ID));
			objDataWrapper.AddParameter("@ACTIVITY_ID",int.Parse(ACTIVITY_ID));
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}	
		public string GetXmlForPageControls(string CLAIM_ID, string EXPENSE_ID, string EXPENSE_BREAKDOWN_ID, string ACTIVITY_ID)
		{
			DataSet objDataSet = GetValuesForPageControls(CLAIM_ID,EXPENSE_ID,EXPENSE_BREAKDOWN_ID,ACTIVITY_ID);
			return objDataSet.GetXml();
		}
		#endregion
	}
}
