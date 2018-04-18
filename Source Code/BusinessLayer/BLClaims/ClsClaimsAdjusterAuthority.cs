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
	/// Summary description for ClsClaimsAdjusterAuthority.
	/// </summary>
	public class ClsClaimsAdjusterAuthority : Cms.BusinessLayer.BLClaims.ClsClaims
	{
		private const	string		GetCLM_ADJUSTER_AUTHORITY				=	"Proc_GetCLM_ADJUSTER_AUTHORITY";
		private const	string		InsertCLM_ADJUSTER_AUTHORITY			=	"Proc_InsertCLM_ADJUSTER_AUTHORITY";
		private const	string		UpdateCLM_ADJUSTER_AUTHORITY			=	"Proc_UpdateCLM_ADJUSTER_AUTHORITY";
		private const	string		DeleteCLM_EXPERT_SERVICE_PROVIDERS  	=	"Proc_DeleteCLM_EXPERT_SERVICE_PROVIDERS";
		
		
		public ClsClaimsAdjusterAuthority()
		{
			base.strActivateDeactivateProcedure = "Proc_ActivateDeactivateCLM_ADJUSTER_AUTHORITY";
		}

		public static DataTable FetchAuthorityLimits()
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);										
			DataSet ds = objWrapper.ExecuteDataSet("Proc_GetAuthorityLimits");			
			if(ds!=null && ds.Tables.Count>0)
				return ds.Tables[0];
			else
				return null;
		}

//		public static DataSet GetRemainingLOB(int intADJUSTER_ID)
//		{
//			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);										
//			objWrapper.AddParameter("@ADJUSTER_ID",intADJUSTER_ID);
//			DataSet ds = objWrapper.ExecuteDataSet("Proc_GetRemainingLOB");			
//			if(ds!=null && ds.Tables.Count>0)
//				return ds;
//			else
//				return null;
//		}

//		public static DataTable FetchAuthorityAmounts(int intLIMIT_ID)
//		{
//			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);							
//			objWrapper.AddParameter("@LIMIT_ID",intLIMIT_ID);
//			DataSet ds = objWrapper.ExecuteDataSet("Proc_GetAuthorityAmounts");			
//			if(ds!=null && ds.Tables.Count>0)
//				return ds.Tables[0];
//			else
//				return null;
//		}

		#region Add(Insert) functions
		public int Add(Cms.Model.Maintenance.Claims.ClsClaimsAdjusterAuthorityInfo objClaimsAdjusterAuthorityInfo,string strAdjusterName)
		{			
			string []adjuster = strAdjusterName.Split('^');
			string adjusterName ="" ;
			string adjusterType ="" ;
			if(adjuster.Length > 1)
			{
				adjusterName = adjuster[0];
				adjusterType = adjuster[1];
			}
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			try
			{	
				objDataWrapper.AddParameter("@LOB_ID",objClaimsAdjusterAuthorityInfo.LOB_ID);
				objDataWrapper.AddParameter("@LIMIT_ID",objClaimsAdjusterAuthorityInfo.LIMIT_ID);
				objDataWrapper.AddParameter("@ADJUSTER_ID",objClaimsAdjusterAuthorityInfo.ADJUSTER_ID);
				objDataWrapper.AddParameter("@EFFECTIVE_DATE",objClaimsAdjusterAuthorityInfo.EFFECTIVE_DATE);				
				objDataWrapper.AddParameter("@CREATED_BY",objClaimsAdjusterAuthorityInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objClaimsAdjusterAuthorityInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@NOTIFY_AMOUNT",objClaimsAdjusterAuthorityInfo.NOTIFY_AMOUNT);
				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ADJUSTER_AUTHORITY_ID",objClaimsAdjusterAuthorityInfo.ADJUSTER_AUTHORITY_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired) 
				{
					objClaimsAdjusterAuthorityInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/claims/AddClaimsAdjusterAuthority.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objClaimsAdjusterAuthorityInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objClaimsAdjusterAuthorityInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Claims Adjuster Authority has been added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	"Adjuster Name=" + adjusterName +"<br>"+ "Adjuster Type=" + adjusterType;
					returnResult = objDataWrapper.ExecuteNonQuery(InsertCLM_ADJUSTER_AUTHORITY,objTransactionInfo);
				}
				else
					returnResult	= objDataWrapper.ExecuteNonQuery(InsertCLM_ADJUSTER_AUTHORITY);
				

				int ADJUSTER_AUTHORITY_ID = 0;
				if (returnResult>0)
				{
					ADJUSTER_AUTHORITY_ID = int.Parse(objSqlParameter.Value.ToString());					
					objClaimsAdjusterAuthorityInfo.ADJUSTER_AUTHORITY_ID = ADJUSTER_AUTHORITY_ID;
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
		/// <param name="objOldLocationInfo">Model object having old information</param>
		/// <param name="objLocationInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(Cms.Model.Maintenance.Claims.ClsClaimsAdjusterAuthorityInfo objOldClaimsAdjusterAuthorityInfo,Cms.Model.Maintenance.Claims.ClsClaimsAdjusterAuthorityInfo objClaimsAdjusterAuthorityInfo,string strAdjusterName)
		{
			string []adjusters = strAdjusterName.Split('^');
			string adjustersName ="" ;
			string adjustersType ="" ;
			if(adjusters.Length > 1)
			{
				adjustersName = adjusters[0];
				adjustersType = adjusters[1];
			}
			int returnResult = 0;	
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);						
			try 
			{
				objDataWrapper.AddParameter("@ADJUSTER_AUTHORITY_ID",objClaimsAdjusterAuthorityInfo.ADJUSTER_AUTHORITY_ID);
				//objDataWrapper.AddParameter("@LOB_ID",objClaimsAdjusterAuthorityInfo.LOB_ID);
				objDataWrapper.AddParameter("@LIMIT_ID",objClaimsAdjusterAuthorityInfo.LIMIT_ID);
				objDataWrapper.AddParameter("@ADJUSTER_ID",objClaimsAdjusterAuthorityInfo.ADJUSTER_ID);
				objDataWrapper.AddParameter("@EFFECTIVE_DATE",objClaimsAdjusterAuthorityInfo.EFFECTIVE_DATE);				
				objDataWrapper.AddParameter("@MODIFIED_BY",objClaimsAdjusterAuthorityInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objClaimsAdjusterAuthorityInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@NOTIFY_AMOUNT",objClaimsAdjusterAuthorityInfo.NOTIFY_AMOUNT);
				
				if(TransactionLogRequired) 
				{
					objClaimsAdjusterAuthorityInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/claims/AddClaimsAdjusterAuthority.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objOldClaimsAdjusterAuthorityInfo,objClaimsAdjusterAuthorityInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objClaimsAdjusterAuthorityInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Claims Authority Limits has been modified";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	"Adjuster Name=" + adjustersName +"<br>"+ "Adjuster Type=" + adjustersType;
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_ADJUSTER_AUTHORITY,objTransactionInfo);
				}
				else
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_ADJUSTER_AUTHORITY);
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

//		#region GetLimitsOfAuthority
//		/// <summary>
//		/// Returns only the locations associated with an application
//		/// </summary>
//		/// <param name="intCustomerID"></param>
//		/// <param name="intAppID"></param>
//		/// <param name="intAppVersionID"></param>
//		/// <returns></returns>
//		public static string GetExpertServiceProviders(int intEXPERT_SERVICE_ID)													  
//		{
//			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
//			objWrapper.AddParameter("@EXPERT_SERVICE_ID",intEXPERT_SERVICE_ID);
//			DataSet ds = objWrapper.ExecuteDataSet(GetCLM_EXPERT_SERVICE_PROVIDERS);			
//			if(ds!=null && ds.Tables.Count>0)
//				return Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(ds.Tables[0]);
//			else
//				return "";
//		}
//
//		#endregion

		#region GetClaimsAdjusters
		public static string GetClaimsAdjusters(int intADJUSTER_AUTHORITY_ID,int intADJUSTER_ID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);							
			objWrapper.AddParameter("@ADJUSTER_AUTHORITY_ID",intADJUSTER_AUTHORITY_ID);
			objWrapper.AddParameter("@ADJUSTER_ID",intADJUSTER_ID);
			DataSet ds = objWrapper.ExecuteDataSet(GetCLM_ADJUSTER_AUTHORITY);
			if(ds!=null && ds.Tables.Count>0)
				return Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(ds.Tables[0]);
			else
				return "";
		}
		#endregion

//		#region Delete Expert Service Provider
//		public int Delete(int intEXPERT_SERVICE_ID)
//		{
//			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
//			objWrapper.AddParameter("@EXPERT_SERVICE_ID",intEXPERT_SERVICE_ID);
//			int retValue = objWrapper.ExecuteNonQuery(DeleteCLM_EXPERT_SERVICE_PROVIDERS);			
//			return retValue;
//		}
//		#endregion
	}
}
