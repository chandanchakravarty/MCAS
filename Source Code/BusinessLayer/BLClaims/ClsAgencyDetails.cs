/******************************************************************************************
<Author					: - Sumit Chhabra
<Start Date				: -	5/24/2006
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
	public class ClsAgencyDetails :  Cms.BusinessLayer.BLClaims.ClsClaims
	{
		private const	string		CLM_AGENCY				=	"CLM_AGENCY";
		private const	string		GetCLM_AGENCY			=	"Proc_GetCLM_AGENCY";
		private const	string		InsertCLM_AGENCY		=	"Proc_InsertCLM_AGENCY";
		private const	string		UpdateCLM_AGENCY		=	"Proc_UpdateCLM_AGENCY";
		
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
		public ClsAgencyDetails()
		{
			boolTransactionLog	= base.TransactionLogRequired;			
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in ADDRESS1 object to database.
		/// </summary>
		/// <param name="objAgencyDetailsInfo">ADDRESS1 class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsAgencyDetailsInfo objAgencyDetailsInfo)
		{
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CLAIM_ID",objAgencyDetailsInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@AGENCY_CODE",objAgencyDetailsInfo.AGENCY_CODE);
				objDataWrapper.AddParameter("@AGENCY_SUB_CODE",objAgencyDetailsInfo.AGENCY_SUB_CODE);
				objDataWrapper.AddParameter("@AGENCY_CUSTOMER_ID",objAgencyDetailsInfo.AGENCY_CUSTOMER_ID);
				objDataWrapper.AddParameter("@AGENCY_PHONE",objAgencyDetailsInfo.AGENCY_PHONE);
				objDataWrapper.AddParameter("@AGENCY_FAX",objAgencyDetailsInfo.AGENCY_FAX);						
				objDataWrapper.AddParameter("@CREATED_BY",objAgencyDetailsInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objAgencyDetailsInfo.CREATED_DATETIME);				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@AGENCY_ID",objAgencyDetailsInfo.AGENCY_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objAgencyDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddWatercraftPropertyDamaged.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objAgencyDetailsInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objAgencyDetailsInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Agency Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(InsertCLM_AGENCY,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(InsertCLM_AGENCY);
				}
				int AGENCY_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (AGENCY_ID == -1)
				{
					return -1;
				}
				else
				{
					objAgencyDetailsInfo.AGENCY_ID = AGENCY_ID;
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
		/// Update method that recieves ADDRESS1 object to save.
		/// </summary>
		/// <param name="objOldInsuredVehicleInfo">ADDRESS1 object haADDRESS2g old information</param>
		/// <param name="objAgencyDetailsInfo">ADDRESS1 object haADDRESS2g new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsAgencyDetailsInfo objOldAgencyDetailsInfo,ClsAgencyDetailsInfo objAgencyDetailsInfo)
		{
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@AGENCY_ID",objAgencyDetailsInfo.AGENCY_ID);				
				objDataWrapper.AddParameter("@CLAIM_ID",objAgencyDetailsInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@AGENCY_CODE",objAgencyDetailsInfo.AGENCY_CODE);
				objDataWrapper.AddParameter("@AGENCY_SUB_CODE",objAgencyDetailsInfo.AGENCY_SUB_CODE);
				objDataWrapper.AddParameter("@AGENCY_CUSTOMER_ID",objAgencyDetailsInfo.AGENCY_CUSTOMER_ID);
				objDataWrapper.AddParameter("@AGENCY_PHONE",objAgencyDetailsInfo.AGENCY_PHONE);
				objDataWrapper.AddParameter("@AGENCY_FAX",objAgencyDetailsInfo.AGENCY_FAX);	
				objDataWrapper.AddParameter("@MODIFIED_BY",objAgencyDetailsInfo.MODIFIED_BY);				
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objAgencyDetailsInfo.LAST_UPDATED_DATETIME);				
				

				if(base.TransactionLogRequired) 
				{
					objAgencyDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddWatercraftPropertyDamaged.aspx.resx");
					objBuilder.GetUpdateSQL(objOldAgencyDetailsInfo,objAgencyDetailsInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objAgencyDetailsInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Agency Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_AGENCY,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_AGENCY);
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
		public static DataTable GetOldDataForPageControls(string CLAIM_ID, string AGENCY_ID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);
			objDataWrapper.AddParameter("@AGENCY_ID",AGENCY_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(GetCLM_AGENCY);
			if(objDataSet!=null && objDataSet.Tables.Count>0)
				return objDataSet.Tables[0];
			else
				return null;
		}
		#endregion
		
	}
}
