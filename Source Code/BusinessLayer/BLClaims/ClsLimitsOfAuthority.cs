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
	/// Summary description for ClsLimitsOfAuthority.
	/// </summary>
	public class ClsLimitsOfAuthority : Cms.BusinessLayer.BLClaims.ClsClaims
	{
		private const	string		GetCLM_AUTHORITY_LIMIT			=	"Proc_GetCLM_AUTHORITY_LIMIT";
		private const	string		InsertCLM_AUTHORITY_LIMIT  		=	"Proc_InsertCLM_AUTHORITY_LIMIT";
		private const	string		UpdateCLM_AUTHORITY_LIMIT  		=	"Proc_UpdateCLM_AUTHORITY_LIMIT";
		private const	string		DeleteCLM_AUTHORITY_LIMIT  		=	"Proc_DeleteCLM_AUTHORITY_LIMIT";
		private const	string		GET_NEXT_AUTHORITY_LEVEL		=	"Proc_GET_NEXT_AUTHORITY_LEVEL";
		
		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsLimitsOfAuthority()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion
		#region Private Instance Variables
		private			bool		boolTransactionLog;
		// private int _AGENCY_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_CLM_AUTHORITY_LIMIT_DEACTIVATE_ACTIVATE";
		#endregion
		
		#region Add(Insert) functions
		public int Add(ClsLimitsOfAuthorityInfo objLimitsOfAuthorityInfo)
		{
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);			
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			try
			{
				objDataWrapper.AddParameter("@AUTHORITY_LEVEL",objLimitsOfAuthorityInfo.AUTHORITY_LEVEL);
				objDataWrapper.AddParameter("@TITLE",objLimitsOfAuthorityInfo.TITLE);
				objDataWrapper.AddParameter("@PAYMENT_LIMIT",objLimitsOfAuthorityInfo.PAYMENT_LIMIT);
				if(objLimitsOfAuthorityInfo.RESERVE_LIMIT!=-1)
					objDataWrapper.AddParameter("@RESERVE_LIMIT",objLimitsOfAuthorityInfo.RESERVE_LIMIT);
				else
					objDataWrapper.AddParameter("@RESERVE_LIMIT",System.DBNull.Value);
				objDataWrapper.AddParameter("@CLAIM_ON_DUMMY_POLICY",objLimitsOfAuthorityInfo.CLAIM_ON_DUMMY_POLICY);
				objDataWrapper.AddParameter("@CREATED_BY",objLimitsOfAuthorityInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objLimitsOfAuthorityInfo.CREATED_DATETIME);

                //Added by Agniswar for Singapore Implementation
                objDataWrapper.AddParameter("@REOPEN_CLAIM_LIMIT", objLimitsOfAuthorityInfo.REOPEN_CLAIM_LIMIT);
                objDataWrapper.AddParameter("@GRATIA_CLAIM_AMOUNT", objLimitsOfAuthorityInfo.GRATIA_CLAIM_AMOUNT);
				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@LIMIT_ID",objLimitsOfAuthorityInfo.LIMIT_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired) 
				{
					objLimitsOfAuthorityInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/claims/LimitsOfAuthorityDetails.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objLimitsOfAuthorityInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objLimitsOfAuthorityInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Limits of Authority has been added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(InsertCLM_AUTHORITY_LIMIT,objTransactionInfo);
				}
				else
					returnResult	= objDataWrapper.ExecuteNonQuery(InsertCLM_AUTHORITY_LIMIT);
				

				int LIMIT_ID = 0;
				if (returnResult>0)
				{
					LIMIT_ID = int.Parse(objSqlParameter.Value.ToString());					
					objLimitsOfAuthorityInfo.LIMIT_ID = LIMIT_ID;
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
		public int Update(ClsLimitsOfAuthorityInfo objOldLimitsOfAuthorityInfo,ClsLimitsOfAuthorityInfo objLimitsOfAuthorityInfo,int intAuthorityLevel)
		{
			int returnResult = 0;	
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);						
			try 
			{
				objDataWrapper.AddParameter("@AUTHORITY_LEVEL",objLimitsOfAuthorityInfo.AUTHORITY_LEVEL);
				objDataWrapper.AddParameter("@TITLE",objLimitsOfAuthorityInfo.TITLE);
				objDataWrapper.AddParameter("@PAYMENT_LIMIT",objLimitsOfAuthorityInfo.PAYMENT_LIMIT);
				if(objLimitsOfAuthorityInfo.RESERVE_LIMIT!=-1)
					objDataWrapper.AddParameter("@RESERVE_LIMIT",objLimitsOfAuthorityInfo.RESERVE_LIMIT);
				else
					objDataWrapper.AddParameter("@RESERVE_LIMIT",System.DBNull.Value);
				objDataWrapper.AddParameter("@CLAIM_ON_DUMMY_POLICY",objLimitsOfAuthorityInfo.CLAIM_ON_DUMMY_POLICY);
				objDataWrapper.AddParameter("@MODIFIED_BY",objLimitsOfAuthorityInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objLimitsOfAuthorityInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@LIMIT_ID",objLimitsOfAuthorityInfo.LIMIT_ID);

                //Added by Agniswar for Singapore Implementation
                objDataWrapper.AddParameter("@REOPEN_CLAIM_LIMIT", objLimitsOfAuthorityInfo.REOPEN_CLAIM_LIMIT);
                objDataWrapper.AddParameter("@GRATIA_CLAIM_AMOUNT", objLimitsOfAuthorityInfo.GRATIA_CLAIM_AMOUNT);

				if(TransactionLogRequired) 
				{
					objLimitsOfAuthorityInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/maintenance/claims/LimitsOfAuthorityDetails.aspx.resx");
					string strTranXML = objBuilder.GetTransactionLogXML(objOldLimitsOfAuthorityInfo,objLimitsOfAuthorityInfo);					
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objLimitsOfAuthorityInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Limits of Authority has been modified";
					objTransactionInfo.CUSTOM_INFO      =   "Authority Level=" + intAuthorityLevel;
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_AUTHORITY_LIMIT,objTransactionInfo);
				}
				else
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_AUTHORITY_LIMIT);
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

		#region GetLimitsOfAuthority
		/// <summary>
		/// Returns only the locations associated with an application
		/// </summary>
		/// <param name="intCustomerID"></param>
		/// <param name="intAppID"></param>
		/// <param name="intAppVersionID"></param>
		/// <returns></returns>
		public static string GetLimitsOfAuthority(int intLimit_ID)
		{
			DataSet ds = GetValuesLimitsOfAuthority(intLimit_ID);		
			if(ds!=null && ds.Tables.Count>0)
				return Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(ds.Tables[0]);
			else
				return "";
		}
		
		public static DataSet GetValuesLimitsOfAuthority(int intLimit_ID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@LIMIT_ID",intLimit_ID);
			DataSet dsTemp = objWrapper.ExecuteDataSet(GetCLM_AUTHORITY_LIMIT);	
			return dsTemp;
		}

		#endregion


		#region Delete Limits of Authority 
		public int Delete(int intLimit_ID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@LIMIT_ID",intLimit_ID);
			int retValue	=	 objWrapper.ExecuteNonQuery(DeleteCLM_AUTHORITY_LIMIT);			
			return retValue;
		}
		#endregion

		#region Get Next Authority Level
		public static string GetNextAuthorityLevel()
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);							
			SqlParameter objSqlParameter  = (SqlParameter) objWrapper.AddParameter("@NextAuthorityLimit",0,SqlDbType.Int,ParameterDirection.ReturnValue);
			objWrapper.ExecuteNonQuery(GET_NEXT_AUTHORITY_LEVEL);
			string retValue = objSqlParameter.Value.ToString();
			if(retValue=="-1")
				return "";
			else
				return retValue;
		}
		#endregion

		#region Activate Deactivate

		public int ActivateDeactivate(int LimitID, string IsActive)
		{
			int returnResult = 0;	
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);						
			try 
			{
				objDataWrapper.AddParameter("@LIMIT_ID",LimitID);
				objDataWrapper.AddParameter("@IS_ACTIVE",IsActive);
				
				returnResult = objDataWrapper.ExecuteNonQuery("Proc_ActivateDeactiveCLM_AUTHORITY_LIMIT");
				
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
			}
		}

		#endregion
	}
}
