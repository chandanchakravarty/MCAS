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
	/// Summary description for ClsLossCodes.
	/// </summary>
	public class ClsLossCodes : Cms.BusinessLayer.BLClaims.ClsClaims
	{
		private const	string		GetCLM_LOSS_CODES						=	"Proc_GetCLM_LOSS_CODES";
		private const	string		InsertCLM_LOSS_CODES  					=	"Proc_InsertCLM_LOSS_CODES";		
		private const	string		DeleteCLM_EXPERT_SERVICE_PROVIDERS  	=	"Proc_DeleteCLM_EXPERT_SERVICE_PROVIDERS";
		private const	string		GetREMAINING_LOSS_CODES					=	"Proc_GetREMAINING_LOSS_CODES";
		private const	string		GetREMAINING_LOB_LOSS_CODES				=	"Proc_GetRemainingLOBForLossCodes";		
		
		
		public ClsLossCodes()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Add(Insert) functions
		public int Add(Cms.Model.Maintenance.Claims.ClsLossCodesInfo objLossCodesInfo,string strLossCodeTypes)
		{			
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			try
			{	
				objDataWrapper.AddParameter("@LOSS_CODE_TYPES",strLossCodeTypes);
				objDataWrapper.AddParameter("@LOB_ID",objLossCodesInfo.LOB_ID);
				objDataWrapper.AddParameter("@CREATED_BY",objLossCodesInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objLossCodesInfo.CREATED_DATETIME);

				int returnResult = 0;
				if(TransactionLogRequired) 
				{
					objLossCodesInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/claims/AddLossCodes.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objLossCodesInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objLossCodesInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Loss Codes is added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(InsertCLM_LOSS_CODES,objTransactionInfo);
				}
				else
					returnResult	= objDataWrapper.ExecuteNonQuery(InsertCLM_LOSS_CODES);
				
				
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

		#region GetLossCodes
		/// <summary>
		/// Returns only the locations associated with an application
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <param name="intAppID"></param>
		/// <param name="intAppVersionID"></param>
		/// <returns></returns>
		public static DataTable GetLossCodes(int LOB_ID,int LangID)													  
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@LOB_ID",LOB_ID);
            objWrapper.AddParameter("@LANG_ID", LangID);
			DataSet ds = objWrapper.ExecuteDataSet(GetCLM_LOSS_CODES);			
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}

		#endregion

		#region Get Remaining Loss Codes Not Assigned Yet
		/// <summary>
		/// Returns only the locations associated with an application
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <param name="intAppID"></param>
		/// <param name="intAppVersionID"></param>
		/// <returns></returns>
		public static DataTable GetRemainingLossCodes(string LOB_ID, int TYPE_ID)													  
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@LOB_ID",LOB_ID);
			objWrapper.AddParameter("@TYPE_ID",TYPE_ID);
            //objWrapper.AddParameter("Lang_ID", ClsCommon.BL_LANG_ID);
			DataSet ds = objWrapper.ExecuteDataSet(GetREMAINING_LOSS_CODES);			
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}

		#endregion

		#region Get Remaining LOBs Not Assigned Yet
		/// <summary>
		/// Returns only the locations associated with an application
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <param name="intAppID"></param>
		/// <param name="intAppVersionID"></param>
		/// <returns></returns>
		public static DataTable GetRemainingLOBForLossCodes()  
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);
            objWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID);			
			DataSet ds = objWrapper.ExecuteDataSet(GetREMAINING_LOB_LOSS_CODES);			
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}

		#endregion
		
	}
}
