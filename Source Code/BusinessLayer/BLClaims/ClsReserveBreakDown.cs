/******************************************************************************************
<Author					: - Sumit Chhabra
<Start Date				: -	01/06/2006
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
	public class ClsReserveBreakDown :  Cms.BusinessLayer.BLClaims.ClsClaims
	{
		private const	string		CLM_ACTIVITY_RESERVE_BREAKDOWN				=	"CLM_ACTIVITY_RESERVE_BREAKDOWN";
		private const	string		GetCLM_ACTIVITY_RESERVE_BREAKDOWN			=	"Proc_GetCLM_ACTIVITY_RESERVE_BREAKDOWN";
		private const	string		InsertCLM_ACTIVITY_RESERVE_BREAKDOWN		=	"Proc_InsertCLM_ACTIVITY_RESERVE_BREAKDOWN";
		private const	string		UpdateCLM_ACTIVITY_RESERVE_BREAKDOWN		=	"Proc_UpdateCLM_ACTIVITY_RESERVE_BREAKDOWN";
		
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
		public ClsReserveBreakDown()
		{
			boolTransactionLog	= base.TransactionLogRequired;			
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objReserveBreakDownInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsReserveBreakDownInfo objReserveBreakDownInfo)
		{
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CLAIM_ID",objReserveBreakDownInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@ACTIVITY_ID",objReserveBreakDownInfo.ACTIVITY_ID);
				objDataWrapper.AddParameter("@TRANSACTION_CODE",objReserveBreakDownInfo.TRANSACTION_CODE);
				objDataWrapper.AddParameter("@BASIS",objReserveBreakDownInfo.BASIS);
				objDataWrapper.AddParameter("@VALUE",objReserveBreakDownInfo.VALUE);
				objDataWrapper.AddParameter("@AMOUNT",objReserveBreakDownInfo.AMOUNT);
				objDataWrapper.AddParameter("@CREATED_BY",objReserveBreakDownInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objReserveBreakDownInfo.CREATED_DATETIME);				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@RESERVE_BREAKDOWN_ID",objReserveBreakDownInfo.RESERVE_BREAKDOWN_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objReserveBreakDownInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddInsuredVehicle.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objReserveBreakDownInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objReserveBreakDownInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Record Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(InsertCLM_ACTIVITY_RESERVE_BREAKDOWN,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(InsertCLM_ACTIVITY_RESERVE_BREAKDOWN);
				}
				int RESERVE_BREAKDOWN_ID=-1;

				if(returnResult>0)
					RESERVE_BREAKDOWN_ID = int.Parse(objSqlParameter.Value.ToString());

				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (RESERVE_BREAKDOWN_ID != -1)
					objReserveBreakDownInfo.RESERVE_BREAKDOWN_ID = RESERVE_BREAKDOWN_ID;
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
		/// <param name="objOldInsuredVehicleInfo">Model object having old information</param>
		/// <param name="objReserveBreakDownInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsReserveBreakDownInfo objOldReserveBreakDownInfo,ClsReserveBreakDownInfo objReserveBreakDownInfo)
		{
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CLAIM_ID",objReserveBreakDownInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@ACTIVITY_ID",objReserveBreakDownInfo.ACTIVITY_ID);
				objDataWrapper.AddParameter("@RESERVE_BREAKDOWN_ID",objReserveBreakDownInfo.RESERVE_BREAKDOWN_ID);
				objDataWrapper.AddParameter("@TRANSACTION_CODE",objReserveBreakDownInfo.TRANSACTION_CODE);
				objDataWrapper.AddParameter("@BASIS",objReserveBreakDownInfo.BASIS);
				objDataWrapper.AddParameter("@VALUE",objReserveBreakDownInfo.VALUE);
				objDataWrapper.AddParameter("@AMOUNT",objReserveBreakDownInfo.AMOUNT);
				objDataWrapper.AddParameter("@MODIFIED_BY",objReserveBreakDownInfo.MODIFIED_BY);								
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objReserveBreakDownInfo.LAST_UPDATED_DATETIME);

				if(base.TransactionLogRequired) 
				{
					objReserveBreakDownInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/ReserveBreakDownDetails.aspx.resx");
					objBuilder.GetUpdateSQL(objOldReserveBreakDownInfo,objReserveBreakDownInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objReserveBreakDownInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Information Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_ACTIVITY_RESERVE_BREAKDOWN,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_ACTIVITY_RESERVE_BREAKDOWN);
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
		public static DataTable GetXmlForPageControls(string CLAIM_ID, string ACTIVITY_ID, string RESERVE_BREAKDOWN_ID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);
			objDataWrapper.AddParameter("@ACTIVITY_ID",ACTIVITY_ID);
			objDataWrapper.AddParameter("@RESERVE_BREAKDOWN_ID",RESERVE_BREAKDOWN_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(GetCLM_ACTIVITY_RESERVE_BREAKDOWN);
			if(objDataSet!=null && objDataSet.Tables.Count>0)
				return objDataSet.Tables[0];
			else
				return null;
		}
		#endregion
		
	}
}
