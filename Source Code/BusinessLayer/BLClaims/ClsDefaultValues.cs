/******************************************************************************************
<Author					: -		Vijay Arora
<Start Date				: -		4/20/2006 5:29:42 PM
<End Date				: -	
<Description			: - 	Business Layer for Default Value Types
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
using Cms.Model.Maintenance.Claims;
using Cms.BusinessLayer.BlCommon;

namespace Cms.BusinessLayer.BLClaims
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsDefaultValues : Cms.BusinessLayer.BLClaims.ClsClaims
	{
      
		#region Constructors
		public ClsDefaultValues()
		{

		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objDVInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsDefaultValuesInfo objDVInfo)
		{
			string		strStoredProc	=	"Proc_InsertCLM_TYPE_DETAIL";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			try
			{
				objDataWrapper.AddParameter("@DETAIL_TYPE_DESCRIPTION",objDVInfo.DETAIL_TYPE_DESCRIPTION);
				objDataWrapper.AddParameter("@TYPE_ID",objDVInfo.TYPE_ID);
				objDataWrapper.AddParameter("@TRANSACTION_CODE",objDVInfo.TRANSACTION_CODE);
				objDataWrapper.AddParameter("@TRANSACTION_CATEGORY",objDVInfo.TRANSACTION_CATEGORY);
				objDataWrapper.AddParameter("@IS_SYSTEM_GENERATED",objDVInfo.IS_SYSTEM_GENERATED);
				objDataWrapper.AddParameter("@CREATED_BY",objDVInfo.CREATED_BY);

                //Added by Agniswar for Singapore Implementation
                objDataWrapper.AddParameter("@LOSS_TYPE_CODE", objDVInfo.LOSS_TYPE_CODE);
                objDataWrapper.AddParameter("@LOSS_DEPARTMENT", objDVInfo.LOSS_DEPARTMENT);
                objDataWrapper.AddParameter("@LOSS_EXTRA_COVER", objDVInfo.LOSS_EXTRA_COVER);

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DETAIL_TYPE_ID",objDVInfo.DETAIL_TYPE_ID,SqlDbType.Int,ParameterDirection.Output);

				objDataWrapper.AddParameter("@SelectedDebitLedgers",objDVInfo.SelectedDebitLedgers);
				objDataWrapper.AddParameter("@SelectedCreditLedgers",objDVInfo.SelectedCreditLedgers);


				int returnResult = 0;
				if(TransactionLogRequired) 
				{
					objDVInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/claims/AddDefaultValueClaims.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objDVInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objDVInfo.CREATED_BY; 
					objTransactionInfo.RECORD_DATE_TIME =	DateTime.Now; 
					objTransactionInfo.TRANS_DESC		=	"Claims Default Value is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				if (returnResult>0)
				{
					objDVInfo.DETAIL_TYPE_ID = int.Parse(objSqlParameter.Value.ToString());					
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
		/// <param name="objOldDVInfo">Model object having old information</param>
		/// <param name="objDVInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsDefaultValuesInfo objOldDVInfo,ClsDefaultValuesInfo objDVInfo)
		{
			string		strStoredProc	=	"Proc_UpdateCLM_TYPE_DETAIL";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@DETAIL_TYPE_ID",objDVInfo.DETAIL_TYPE_ID);
				objDataWrapper.AddParameter("@DETAIL_TYPE_DESCRIPTION",objDVInfo.DETAIL_TYPE_DESCRIPTION);
				objDataWrapper.AddParameter("@TRANSACTION_CODE",objDVInfo.TRANSACTION_CODE);
				objDataWrapper.AddParameter("@TRANSACTION_CATEGORY",objDVInfo.TRANSACTION_CATEGORY);
				objDataWrapper.AddParameter("@IS_SYSTEM_GENERATED",objDVInfo.IS_SYSTEM_GENERATED);
				objDataWrapper.AddParameter("@MODIFIED_BY",objDVInfo.MODIFIED_BY);

                //Added by Agniswar for Singapore Implementation
                objDataWrapper.AddParameter("@LOSS_TYPE_CODE", objDVInfo.LOSS_TYPE_CODE);
                objDataWrapper.AddParameter("@LOSS_DEPARTMENT", objDVInfo.LOSS_DEPARTMENT);
                objDataWrapper.AddParameter("@LOSS_EXTRA_COVER", objDVInfo.LOSS_EXTRA_COVER);

				objDataWrapper.AddParameter("@SelectedDebitLedgers",objDVInfo.SelectedDebitLedgers);
				objDataWrapper.AddParameter("@SelectedCreditLedgers",objDVInfo.SelectedCreditLedgers);

				if(TransactionLogRequired) 
				{
					objDVInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/claims/AddDefaultValueClaims.aspx.resx");
					strTranXML = objBuilder.GetTransactionLogXML(objOldDVInfo,objDVInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objDVInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Claims Default Value is modified";
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
		public static string GetXmlForPageControls(string DETAIL_TYPE_ID)
		{
			string strSql = "Proc_GetValuesCLM_TYPE_DETAIL";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@DETAIL_TYPE_ID",int.Parse(DETAIL_TYPE_ID));
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.GetXml();
		}
		#endregion

		#region General Method to fetch details data for CLM_TYPE_MASTER values
		public static DataTable GetDefaultValuesDetails(int Type_ID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);							
			objWrapper.AddParameter("@TYPE_ID",Type_ID);
            objWrapper.AddParameter("@LANG_ID", BL_LANG_ID);
			DataSet ds = objWrapper.ExecuteDataSet("Proc_GetDefaultValuesDetails");			
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}

        public static DataTable GetDefaultValuesDetails(int TypeID, int TranCode)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);							
			objWrapper.AddParameter("@TYPE_ID",TypeID);
			objWrapper.AddParameter("@TRANSACTION_CODE",TranCode);
            objWrapper.AddParameter("@LANG_ID", BL_LANG_ID);
			DataSet ds = objWrapper.ExecuteDataSet("Proc_GetDefaultValuesDetails");			
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}
		#endregion

		#region ActivateDeactivate() function
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objDVInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int ActivateDeactivateDefaultValues(ClsDefaultValuesInfo objDefaultValuesInfo,string strIS_ACTIVE)
		{
			string		strStoredProc	=	"Proc_ActivateDeactivateCLM_TYPE_DETAIL";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			try
			{
				objDataWrapper.AddParameter("@DETAIL_TYPE_ID",objDefaultValuesInfo.DETAIL_TYPE_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",strIS_ACTIVE);				
				
				int returnResult = 0;
				if(TransactionLogRequired) 
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objDefaultValuesInfo.CREATED_BY; 
					objTransactionInfo.RECORD_DATE_TIME =	DateTime.Now; 
					if(strIS_ACTIVE.ToUpper()=="Y")
						objTransactionInfo.TRANS_DESC		=	"Claims Default Value is Activated.";					
					else
						objTransactionInfo.TRANS_DESC		=	"Claims Default Value is Deactivated.";					
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				
				
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


       
		public static DataTable GetLedgerAcctsForClaims()
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);										
			DataSet ds = objWrapper.ExecuteDataSet("GetLedgerAcctsForClaims");			
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}

		public static DataSet GetLedgerAcctsForClaims(int TranCodeID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);										
			objWrapper.AddParameter("@CLAIM_TRAN_CODE",TranCodeID);
			return (objWrapper.ExecuteDataSet("PROC_GET_LEDGER_FOR_CLAIM_TRANSACTION_CODE"));						
		}


	}
}
