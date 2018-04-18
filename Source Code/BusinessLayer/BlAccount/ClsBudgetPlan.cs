/******************************************************************************************
<Author						: -   Manoj Rathore
<Start Date					: -	  6/22/2007 10:11:44 AM
<End Date					: -	
<Description				: - 	Business Logic for Budget Plan.
<Review Date				: - 
<Reviewed By				: - 	
Modification History
<Modified Date				: - 
<Modified By				: - 
<Purpose					: - 
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
namespace Cms.BusinessLayer.BlAccount
{
	/// <summary>
	/// Summary description for ClsBudgetPlan.
	/// </summary>
	public class ClsBudgetPlan : Cms.BusinessLayer.BlAccount.ClsAccount
	{
		private const	string		ACT_BUDGET_PLAN			=	"ACT_BUDGET_PLAN";
		public ClsBudgetPlan()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objBudgetPlanInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(Cms.Model.Account.ClsBudgetPlanInfo objBudgetPlanInfo)
		{
			string		strStoredProc	=	"Proc_InsertACT_BUDGET_PLAN";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			int returnResult;

			try
			{
				objDataWrapper.AddParameter("@GL_ID",objBudgetPlanInfo.GL_ID);
				objDataWrapper.AddParameter("@BUDGET_CATEGORY_ID",objBudgetPlanInfo.BUDGET_CATEGORY_ID);		
				objDataWrapper.AddParameter("@ACCOUNT_ID",objBudgetPlanInfo.ACCOUNT_ID);	
				objDataWrapper.AddParameter("@FISCAL_ID",objBudgetPlanInfo.FISCAL_ID);
				if(objBudgetPlanInfo.JAN_BUDGET != 0)
					objDataWrapper.AddParameter("@JAN_BUDGET",objBudgetPlanInfo.JAN_BUDGET);
				else
					objDataWrapper.AddParameter("@JAN_BUDGET",System.DBNull.Value);	

				if(objBudgetPlanInfo.FEB_BUDGET != 0)
					objDataWrapper.AddParameter("@FEB_BUDGET",objBudgetPlanInfo.FEB_BUDGET);
				else
					objDataWrapper.AddParameter("@FEB_BUDGET",System.DBNull.Value);	

				if(objBudgetPlanInfo.MARCH_BUDGET != 0)
					objDataWrapper.AddParameter("@MARCH_BUDGET",objBudgetPlanInfo.MARCH_BUDGET);
				else
					objDataWrapper.AddParameter("@MARCH_BUDGET",System.DBNull.Value);	
				if(objBudgetPlanInfo.APRIL_BUDGET != 0)
					objDataWrapper.AddParameter("@APRIL_BUDGET",objBudgetPlanInfo.APRIL_BUDGET);
				else
					objDataWrapper.AddParameter("@APRIL_BUDGET",System.DBNull.Value);	
				
				if(objBudgetPlanInfo.MAY_BUDGET != 0)
					objDataWrapper.AddParameter("@MAY_BUDGET",objBudgetPlanInfo.MAY_BUDGET);
				else
					objDataWrapper.AddParameter("@MAY_BUDGET",System.DBNull.Value);
				if(objBudgetPlanInfo.JUNE_BUDGET != 0)
					objDataWrapper.AddParameter("@JUNE_BUDGET",objBudgetPlanInfo.JUNE_BUDGET);
				else
					objDataWrapper.AddParameter("@JUNE_BUDGET",System.DBNull.Value);
				if(objBudgetPlanInfo.JULY_BUDGET != 0)
					objDataWrapper.AddParameter("@JULY_BUDGET",objBudgetPlanInfo.JULY_BUDGET);          
				else
					objDataWrapper.AddParameter("@JULY_BUDGET",System.DBNull.Value);
				if(objBudgetPlanInfo.AUG_BUDGET != 0)
					objDataWrapper.AddParameter("@AUG_BUDGET",objBudgetPlanInfo.AUG_BUDGET);
				else
					objDataWrapper.AddParameter("@AUG_BUDGET",System.DBNull.Value);
				if(objBudgetPlanInfo.SEPT_BUDGET != 0)
					objDataWrapper.AddParameter("@SEPT_BUDGET",objBudgetPlanInfo.SEPT_BUDGET);
				else
					objDataWrapper.AddParameter("@SEPT_BUDGET",System.DBNull.Value);
				if(objBudgetPlanInfo.OCT_BUDGET != 0)
					objDataWrapper.AddParameter("@OCT_BUDGET",objBudgetPlanInfo.OCT_BUDGET);
				else
					objDataWrapper.AddParameter("@OCT_BUDGET",System.DBNull.Value);
				if(objBudgetPlanInfo.NOV_BUDGET != 0)
					objDataWrapper.AddParameter("@NOV_BUDGET",objBudgetPlanInfo.NOV_BUDGET);
				else
					objDataWrapper.AddParameter("@NOV_BUDGET",System.DBNull.Value);
				if(objBudgetPlanInfo.DEC_BUDGET != 0)
					objDataWrapper.AddParameter("@DEC_BUDGET",objBudgetPlanInfo.DEC_BUDGET);
				else
					objDataWrapper.AddParameter("@DEC_BUDGET",System.DBNull.Value);	
				
				SqlParameter objSqlParameter = (SqlParameter) objDataWrapper.AddParameter("@IDEN_ROW_ID", SqlDbType.Int, ParameterDirection.Output);
				SqlParameter objSqlReturnParameter = (SqlParameter) objDataWrapper.AddParameter("@RETURN_PARAM", SqlDbType.Int, ParameterDirection.ReturnValue);
				if(TransactionLogRequired)
				{
					objBudgetPlanInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/aspx/AddPlanBudget.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objBudgetPlanInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objBudgetPlanInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1657", "");// "Plan Budget Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				objDataWrapper.ClearParameteres();
				if(objSqlReturnParameter!=null && int.Parse(objSqlReturnParameter.Value.ToString())>0)			
				{
					objBudgetPlanInfo.IDEN_ROW_ID = int.Parse(objSqlParameter.Value.ToString());					
				}
				
				return int.Parse(objSqlReturnParameter.Value.ToString());
				
			
				
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
			return returnResult;
		}
		#endregion
		/// <summary>
		/// Fatch Vendor Name and Invoice Number
		/// </summary>
		/// <param name="venderId"></param>
		/// <returns></returns>
		public DataSet FatchBudgetPlanNo(int IDENROWID)
		{
			string	strStoredProc =	"Proc_FatchBudgetPlanNo";
			
			try
			{
				DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
				objWrapper.AddParameter("@IDEN_ROW_ID",IDENROWID);
				DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);

				return ds ;
			}
			catch
			{
				return null;
			}	
		}
		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldBudgetPlanInfo">Model object having old information</param>
		/// <param name="objBudgetPlanInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsBudgetPlanInfo objOldBudgetPlanInfo,ClsBudgetPlanInfo objBudgetPlanInfo,string GLeadger,string BudgetCategory)
		{
			//Fetch Budget Plan 
			DataSet BudgetPlanNo = FatchBudgetPlanNo(objBudgetPlanInfo.GL_ID);
			
			string		strStoredProc	=	"Proc_UpdateACT_BUDGET_PLAN";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.ON);
			try 
			{
				
				objDataWrapper.AddParameter("@IDEN_ROW_ID",objBudgetPlanInfo.IDEN_ROW_ID);
				objDataWrapper.AddParameter("@GL_ID",objBudgetPlanInfo.GL_ID);
				objDataWrapper.AddParameter("@BUDGET_CATEGORY_ID",objBudgetPlanInfo.BUDGET_CATEGORY_ID);
				objDataWrapper.AddParameter("@ACCOUNT_ID",objBudgetPlanInfo.ACCOUNT_ID);

				objDataWrapper.AddParameter("@FISCAL_ID",objBudgetPlanInfo.FISCAL_ID);
				objDataWrapper.AddParameter("@JAN_BUDGET",objBudgetPlanInfo.JAN_BUDGET);
				objDataWrapper.AddParameter("@FEB_BUDGET",objBudgetPlanInfo.FEB_BUDGET);
				objDataWrapper.AddParameter("@MARCH_BUDGET",objBudgetPlanInfo.MARCH_BUDGET);
				objDataWrapper.AddParameter("@APRIL_BUDGET",objBudgetPlanInfo.APRIL_BUDGET);
				objDataWrapper.AddParameter("@MAY_BUDGET",objBudgetPlanInfo.MAY_BUDGET);
				objDataWrapper.AddParameter("@JUNE_BUDGET",objBudgetPlanInfo.JUNE_BUDGET);
				objDataWrapper.AddParameter("@JULY_BUDGET",objBudgetPlanInfo.JULY_BUDGET);                	
				objDataWrapper.AddParameter("@AUG_BUDGET",objBudgetPlanInfo.AUG_BUDGET);
				objDataWrapper.AddParameter("@SEPT_BUDGET",objBudgetPlanInfo.SEPT_BUDGET);
				objDataWrapper.AddParameter("@OCT_BUDGET",objBudgetPlanInfo.OCT_BUDGET);
				objDataWrapper.AddParameter("@NOV_BUDGET",objBudgetPlanInfo.NOV_BUDGET);
				objDataWrapper.AddParameter("@DEC_BUDGET",objBudgetPlanInfo.DEC_BUDGET);
			
				SqlParameter objSqlReturnParameter = (SqlParameter) objDataWrapper.AddParameter("@RETURN_PARAM", SqlDbType.Int, ParameterDirection.ReturnValue);
			
				if(base.TransactionLogRequired) 
				{
					objBudgetPlanInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/aspx/AddPlanBudget.aspx.resx");
					objBuilder.GetUpdateSQL(objOldBudgetPlanInfo,objBudgetPlanInfo,out strTranXML, "N");

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objBudgetPlanInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1658", "");// "Plan Budget Has Been Updated";
					objTransactionInfo.CUSTOM_INFO		=   "General Ledger:" + GLeadger + "<br>Budget Category:" + BudgetCategory;
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				
				if(objSqlReturnParameter!=null && int.Parse(objSqlReturnParameter.Value.ToString())<0)			
				{
					return int.Parse(objSqlReturnParameter.Value.ToString());					
				}
				else
				return 1;
			}
			catch(Exception ex)
			{
				return -1;
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
			return 1;
		}
		#endregion
		#region "GetxmlMethods"
		public static string GetXmlForPageControls(string IDEN_ROW_ID)
		{
			string strSql = "PROC_GETXMLACT_BUDGET_PLAN";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@IDEN_ROW_ID",IDEN_ROW_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.GetXml();
		}
		#endregion
	}
}
