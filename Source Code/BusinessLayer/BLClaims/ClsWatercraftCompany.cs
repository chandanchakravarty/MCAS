/******************************************************************************************
<Author					: - Sumit Chhabra
<Start Date				: -	5/19/2006 
<End Date				: -	
<Description			: - 	
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
	public class ClsWatercraftCompany :  Cms.BusinessLayer.BLClaims.ClsClaims
	{
		private const	string		CLM_CLAIM_COMPANY			=	"CLM_CLAIM_COMPANY";
		private const	string		GetCLM_CLAIM_COMPANY			=	"Proc_GetCLM_CLAIM_COMPANY";
		private const	string		InsertCLM_CLAIM_COMPANY		=	"Proc_InsertCLM_CLAIM_COMPANY";
		private const	string		UpdateCLM_CLAIM_COMPANY		=	"Proc_UpdateCLM_CLAIM_COMPANY";
		
		#region Private Instance Variables
		private			bool		boolTransactionLog;	
		
		#endregion

		#region Public Properties
		public bool TransactionLog
		{
			set
			{
				boolTransactionLog	=	value;
			}
			get
			{
				return boolTransactionLog;
			}
		}

		
		#endregion

		#region private Utility Functions
		#endregion

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsWatercraftCompany()
		{
			boolTransactionLog	= base.TransactionLogRequired;			
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objWatercraftCompanyInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsWatercraftCompanyInfo objWatercraftCompanyInfo)
		{
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CLAIM_ID",objWatercraftCompanyInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@AGENCY_ID",objWatercraftCompanyInfo.AGENCY_ID);
				objDataWrapper.AddParameter("@NAIC_CODE",objWatercraftCompanyInfo.NAIC_CODE);
				objDataWrapper.AddParameter("@REFERENCE_NUMBER",objWatercraftCompanyInfo.REFERENCE_NUMBER);
				objDataWrapper.AddParameter("@CAT_NUMBER",objWatercraftCompanyInfo.CAT_NUMBER);
				if(objWatercraftCompanyInfo.EFFECTIVE_DATE!=DateTime.MinValue)
					objDataWrapper.AddParameter("@EFFECTIVE_DATE",objWatercraftCompanyInfo.EFFECTIVE_DATE);
				else
					objDataWrapper.AddParameter("@EFFECTIVE_DATE",System.DBNull.Value);

				if(objWatercraftCompanyInfo.EXPIRATION_DATE!=DateTime.MinValue)
					objDataWrapper.AddParameter("@EXPIRATION_DATE",objWatercraftCompanyInfo.EXPIRATION_DATE);
				else
					objDataWrapper.AddParameter("@EXPIRATION_DATE",System.DBNull.Value);
				objDataWrapper.AddParameter("@CONTACT_NAME",objWatercraftCompanyInfo.CONTACT_NAME);
				objDataWrapper.AddParameter("@CONTACT_ADDRESS1",objWatercraftCompanyInfo.CONTACT_ADDRESS1);
				objDataWrapper.AddParameter("@CONTACT_ADDRESS2",objWatercraftCompanyInfo.CONTACT_ADDRESS2);
				objDataWrapper.AddParameter("@CONTACT_CITY",objWatercraftCompanyInfo.CONTACT_CITY);
				objDataWrapper.AddParameter("@CONTACT_STATE",objWatercraftCompanyInfo.CONTACT_STATE);
				objDataWrapper.AddParameter("@CONTACT_COUNTRY",objWatercraftCompanyInfo.CONTACT_COUNTRY);
				objDataWrapper.AddParameter("@CONTACT_ZIP",objWatercraftCompanyInfo.CONTACT_ZIP);				
				objDataWrapper.AddParameter("@CONTACT_HOMEPHONE",objWatercraftCompanyInfo.CONTACT_HOMEPHONE);
				objDataWrapper.AddParameter("@CONTACT_WORKPHONE",objWatercraftCompanyInfo.CONTACT_WORKPHONE);
				objDataWrapper.AddParameter("@PREVIOUSLY_REPORTED",objWatercraftCompanyInfo.PREVIOUSLY_REPORTED);
				objDataWrapper.AddParameter("@INSURED_CONTACT_ID",objWatercraftCompanyInfo.INSURED_CONTACT_ID);
				objDataWrapper.AddParameter("@ACCIDENT_DATE_TIME",objWatercraftCompanyInfo.ACCIDENT_DATE_TIME);				
				objDataWrapper.AddParameter("@CREATED_BY",objWatercraftCompanyInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objWatercraftCompanyInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@LOSS_TIME_AM_PM",objWatercraftCompanyInfo.LOSS_TIME_AM_PM);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@COMPANY_ID",objWatercraftCompanyInfo.COMPANY_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objWatercraftCompanyInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddWatercraftCompany.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objWatercraftCompanyInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objWatercraftCompanyInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Watercraft Company Record Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(InsertCLM_CLAIM_COMPANY,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(InsertCLM_CLAIM_COMPANY);
				}
				int COMPANY_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (COMPANY_ID == -1)
				{
					return -1;
				}
				else
				{
					objWatercraftCompanyInfo.COMPANY_ID = COMPANY_ID;
					return returnResult;
				}				
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
		/// <param name="objOldInsuredVehicleInfo">Model object having old information</param>
		/// <param name="objWatercraftCompanyInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsWatercraftCompanyInfo objOldWatercraftCompanyInfo,ClsWatercraftCompanyInfo objWatercraftCompanyInfo)
		{
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@COMPANY_ID",objWatercraftCompanyInfo.COMPANY_ID);
				objDataWrapper.AddParameter("@CLAIM_ID",objWatercraftCompanyInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@AGENCY_ID",objWatercraftCompanyInfo.AGENCY_ID);
				objDataWrapper.AddParameter("@NAIC_CODE",objWatercraftCompanyInfo.NAIC_CODE);
				objDataWrapper.AddParameter("@REFERENCE_NUMBER",objWatercraftCompanyInfo.REFERENCE_NUMBER);
				objDataWrapper.AddParameter("@CAT_NUMBER",objWatercraftCompanyInfo.CAT_NUMBER);
				if(objWatercraftCompanyInfo.EFFECTIVE_DATE!=DateTime.MinValue)
					objDataWrapper.AddParameter("@EFFECTIVE_DATE",objWatercraftCompanyInfo.EFFECTIVE_DATE);
				else
					objDataWrapper.AddParameter("@EFFECTIVE_DATE",System.DBNull.Value);

				if(objWatercraftCompanyInfo.EXPIRATION_DATE!=DateTime.MinValue)
					objDataWrapper.AddParameter("@EXPIRATION_DATE",objWatercraftCompanyInfo.EXPIRATION_DATE);
				else
					objDataWrapper.AddParameter("@EXPIRATION_DATE",System.DBNull.Value);
			
				objDataWrapper.AddParameter("@CONTACT_NAME",objWatercraftCompanyInfo.CONTACT_NAME);
				objDataWrapper.AddParameter("@CONTACT_ADDRESS1",objWatercraftCompanyInfo.CONTACT_ADDRESS1);
				objDataWrapper.AddParameter("@CONTACT_ADDRESS2",objWatercraftCompanyInfo.CONTACT_ADDRESS2);
				objDataWrapper.AddParameter("@CONTACT_CITY",objWatercraftCompanyInfo.CONTACT_CITY);
				objDataWrapper.AddParameter("@CONTACT_STATE",objWatercraftCompanyInfo.CONTACT_STATE);
				objDataWrapper.AddParameter("@CONTACT_COUNTRY",objWatercraftCompanyInfo.CONTACT_COUNTRY);				
				objDataWrapper.AddParameter("@CONTACT_ZIP",objWatercraftCompanyInfo.CONTACT_ZIP);
				objDataWrapper.AddParameter("@CONTACT_HOMEPHONE",objWatercraftCompanyInfo.CONTACT_HOMEPHONE);
				objDataWrapper.AddParameter("@CONTACT_WORKPHONE",objWatercraftCompanyInfo.CONTACT_WORKPHONE);
				objDataWrapper.AddParameter("@PREVIOUSLY_REPORTED",objWatercraftCompanyInfo.PREVIOUSLY_REPORTED);
				objDataWrapper.AddParameter("@INSURED_CONTACT_ID",objWatercraftCompanyInfo.INSURED_CONTACT_ID);
				objDataWrapper.AddParameter("@ACCIDENT_DATE_TIME",objWatercraftCompanyInfo.ACCIDENT_DATE_TIME);				
				objDataWrapper.AddParameter("@MODIFIED_BY",objWatercraftCompanyInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objWatercraftCompanyInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@LOSS_TIME_AM_PM",objWatercraftCompanyInfo.LOSS_TIME_AM_PM);

				if(base.TransactionLogRequired) 
				{
					objWatercraftCompanyInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddWatercraftCompany.aspx.resx");
					objBuilder.GetUpdateSQL(objOldWatercraftCompanyInfo,objWatercraftCompanyInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objWatercraftCompanyInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Watercraft Company Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_CLAIM_COMPANY,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_CLAIM_COMPANY);
				}
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
		public static DataTable GetOldDataCLM_CLAIM_COMPANY(string CLAIM_ID, string AGENCY_ID,string COMPANY_ID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);
			objDataWrapper.AddParameter("@AGENCY_ID",AGENCY_ID);
			objDataWrapper.AddParameter("@COMPANY_ID",COMPANY_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(GetCLM_CLAIM_COMPANY);
			if(objDataSet!=null && objDataSet.Tables.Count>0)
				return objDataSet.Tables[0];
			else
				return null;
		}
		#endregion
		
	}
}
