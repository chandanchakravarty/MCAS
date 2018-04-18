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
	public class ClsPaymentBreakDown :  Cms.BusinessLayer.BLClaims.ClsClaims
	{
		private const	string		CLM_ACTIVITY_PAYMENT_BREAKDOWN				=	"CLM_ACTIVITY_PAYMENT_BREAKDOWN";
		private const	string		GetCLM_ACTIVITY_PAYMENT_BREAKDOWN			=	"Proc_GetCLM_ACTIVITY_PAYMENT_BREAKDOWN";
		private const	string		InsertCLM_ACTIVITY_PAYMENT_BREAKDOWN		=	"Proc_InsertCLM_ACTIVITY_PAYMENT_BREAKDOWN";
		private const	string		UpdateCLM_ACTIVITY_PAYMENT_BREAKDOWN		=	"Proc_UpdateCLM_ACTIVITY_PAYMENT_BREAKDOWN";
		private const	string		GetCLM_ACTIVITY_DATA						=	"Proc_GetCLM_ACTIVITY_DATA";
		
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
		public ClsPaymentBreakDown()
		{
			boolTransactionLog	= base.TransactionLogRequired;			
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objPaymentBreakDownInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsPaymentBreakDownInfo objPaymentBreakDownInfo)
		{
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@CLAIM_ID",objPaymentBreakDownInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@ACTIVITY_ID",objPaymentBreakDownInfo.ACTIVITY_ID);
				objDataWrapper.AddParameter("@TRANSACTION_CODE",objPaymentBreakDownInfo.TRANSACTION_CODE);
				objDataWrapper.AddParameter("@COVERAGE_ID",objPaymentBreakDownInfo.COVERAGE_ID);				
				objDataWrapper.AddParameter("@PAID_AMOUNT",objPaymentBreakDownInfo.PAID_AMOUNT);
				objDataWrapper.AddParameter("@CREATED_BY",objPaymentBreakDownInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objPaymentBreakDownInfo.CREATED_DATETIME);				
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@PAYMENT_BREAKDOWN_ID",objPaymentBreakDownInfo.PAYMENT_BREAKDOWN_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objPaymentBreakDownInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/AddInsuredVehicle.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objPaymentBreakDownInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objPaymentBreakDownInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Record Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(InsertCLM_ACTIVITY_PAYMENT_BREAKDOWN,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(InsertCLM_ACTIVITY_PAYMENT_BREAKDOWN);
				}
				int PAYMENT_BREAKDOWN_ID=-1;

				if(returnResult>0)
					PAYMENT_BREAKDOWN_ID = int.Parse(objSqlParameter.Value.ToString());

				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (PAYMENT_BREAKDOWN_ID != -1)
					objPaymentBreakDownInfo.PAYMENT_BREAKDOWN_ID = PAYMENT_BREAKDOWN_ID;
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
		/// <param name="objPaymentBreakDownInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsPaymentBreakDownInfo objOldPaymentBreakDownInfo,ClsPaymentBreakDownInfo objPaymentBreakDownInfo)
		{
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CLAIM_ID",objPaymentBreakDownInfo.CLAIM_ID);
				objDataWrapper.AddParameter("@ACTIVITY_ID",objPaymentBreakDownInfo.ACTIVITY_ID);
				objDataWrapper.AddParameter("@PAYMENT_BREAKDOWN_ID",objPaymentBreakDownInfo.PAYMENT_BREAKDOWN_ID);
				objDataWrapper.AddParameter("@TRANSACTION_CODE",objPaymentBreakDownInfo.TRANSACTION_CODE);
				objDataWrapper.AddParameter("@COVERAGE_ID",objPaymentBreakDownInfo.COVERAGE_ID);				
				objDataWrapper.AddParameter("@PAID_AMOUNT",objPaymentBreakDownInfo.PAID_AMOUNT);
				objDataWrapper.AddParameter("@MODIFIED_BY",objPaymentBreakDownInfo.MODIFIED_BY);								
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objPaymentBreakDownInfo.LAST_UPDATED_DATETIME);

				if(base.TransactionLogRequired) 
				{
					objPaymentBreakDownInfo.TransactLabel = ClsCommon.MapTransactionLabel("/claims/aspx/ReserveBreakDownDetails.aspx.resx");
					objBuilder.GetUpdateSQL(objOldPaymentBreakDownInfo,objPaymentBreakDownInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objPaymentBreakDownInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Information Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_ACTIVITY_PAYMENT_BREAKDOWN,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(UpdateCLM_ACTIVITY_PAYMENT_BREAKDOWN);
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
		public static DataTable GetXmlForPageControls(string CLAIM_ID, string ACTIVITY_ID, string PAYMENT_BREAKDOWN_ID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);
			objDataWrapper.AddParameter("@ACTIVITY_ID",ACTIVITY_ID);
			objDataWrapper.AddParameter("@PAYMENT_BREAKDOWN_ID",PAYMENT_BREAKDOWN_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(GetCLM_ACTIVITY_PAYMENT_BREAKDOWN);
			if(objDataSet!=null && objDataSet.Tables.Count>0)
				return objDataSet.Tables[0];
			else
				return null;
		}
		#endregion

		#region "GetActivityLevelData"
		public static DataTable GetActivityData(string CLAIM_ID, string ACTIVITY_ID,int LANG_ID)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CLAIM_ID",CLAIM_ID);
			objDataWrapper.AddParameter("@ACTIVITY_ID",ACTIVITY_ID);
            objDataWrapper.AddParameter("@LANG_ID", LANG_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(GetCLM_ACTIVITY_DATA);
			if(objDataSet!=null && objDataSet.Tables.Count>0)
				return objDataSet.Tables[0];
			else
				return null;
		}
		#endregion
		
	}
}
