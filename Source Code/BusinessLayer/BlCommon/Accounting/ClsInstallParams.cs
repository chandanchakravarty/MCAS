/******************************************************************************************
<Author					: - Vijay Joshi
<Start Date				: -	6/6/2005 4:03:42 PM
<End Date				: -	
<Description			: - Business logic class for Installment parameters screen.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Data.SqlClient;
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
using Cms.Model.Maintenance.Accounting;

namespace Cms.BusinessLayer.BlCommon.Accounting
{
	/// <summary>
	/// Business logic class for Installment Parameters screen.
	/// </summary>
	public class ClsInstallParams : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		private const string ACT_INSTALL_PARAMS = "ACT_INSTALL_PARAMS";
		private const string GET_INSTALL_PARAMS_INFO = "proc_GetACT_INSTALL_PARAMS";
		private const string GET_ASSIGNED_UNASSIGNEDUSERS = "Proc_GetInstallParamsAssignedUnassignedUsers";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		//private int _INSTALL_DAYS_IN_ADVANCE;
		private const string ACTIVATE_DEACTIVATE_PROC	= "";
		#endregion

		#region private Utility Functions
		#endregion

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsInstallParams()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objInstallParamsInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsInstallParamsInfo objInstallParamsInfo)
		{
			string		strStoredProc	=	"Proc_InsertACT_INSTALL_PARAMS";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@INSTALL_NOTIFY_ACCOUNTEXE",objInstallParamsInfo.INSTALL_NOTIFY_ACCOUNTEXE);
				objDataWrapper.AddParameter("@INSTALL_NOTIFY_CSR",objInstallParamsInfo.INSTALL_NOTIFY_CSR);
				objDataWrapper.AddParameter("@INSTALL_NOTIFY_UNDERWRITER",objInstallParamsInfo.INSTALL_NOTIFY_UNDERWRITER);
				objDataWrapper.AddParameter("@INSTALL_NOTIFY_OTHER_USERS",objInstallParamsInfo.INSTALL_NOTIFY_OTHER_USERS);
				objDataWrapper.AddParameter("@IS_ACTIVE","Y");
				objDataWrapper.AddParameter("@CREATED_BY",objInstallParamsInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objInstallParamsInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",null);
				objDataWrapper.AddParameter("@INSTALL_DAYS_IN_ADVANCE",objInstallParamsInfo.INSTALL_DAYS_IN_ADVANCE);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objInstallParamsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/InstallParams.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objInstallParamsInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objInstallParamsInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Installment parameters has been successfully saved.";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
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
		/// <param name="objOldInstallParamsInfo">Model object having old information</param>
		/// <param name="objInstallParamsInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsInstallParamsInfo objOldInstallParamsInfo,ClsInstallParamsInfo objInstallParamsInfo)
		{
			string strStoredProc = "Proc_InsertACT_INSTALL_PARAMS";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@INSTALL_NOTIFY_ACCOUNTEXE",objInstallParamsInfo.INSTALL_NOTIFY_ACCOUNTEXE);
				objDataWrapper.AddParameter("@INSTALL_NOTIFY_CSR",objInstallParamsInfo.INSTALL_NOTIFY_CSR);
				objDataWrapper.AddParameter("@INSTALL_NOTIFY_UNDERWRITER",objInstallParamsInfo.INSTALL_NOTIFY_UNDERWRITER);
				objDataWrapper.AddParameter("@INSTALL_NOTIFY_OTHER_USERS",objInstallParamsInfo.INSTALL_NOTIFY_OTHER_USERS);
				objDataWrapper.AddParameter("@IS_ACTIVE",objInstallParamsInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",null);
				objDataWrapper.AddParameter("@CREATED_DATETIME",null);
				objDataWrapper.AddParameter("@MODIFIED_BY",objInstallParamsInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objInstallParamsInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@INSTALL_DAYS_IN_ADVANCE",objInstallParamsInfo.INSTALL_DAYS_IN_ADVANCE);

				if(TransactionLogRequired) 
				{
					objInstallParamsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/InstallParams.aspx.resx");string strUpdate = objBuilder.GetUpdateSQL(objOldInstallParamsInfo,objInstallParamsInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objInstallParamsInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Installment parameters has been successfully updated.";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
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

		#region GetInstallParams
		/// <summary>
		/// Returns the datatable with record from ACT_INSTALL_PARAMS table
		/// </summary>
		/// <returns></returns>
		public static DataSet GetInstallParams()
		{

			DataSet dsInstallInfo = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				dsInstallInfo = objDataWrapper.ExecuteDataSet(GET_INSTALL_PARAMS_INFO);
				return dsInstallInfo;
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		}
		#endregion

		#region GetInstallParams
		/// <summary>
		/// Returns the dataset of assigned and unassigned other users
		/// </summary>
		/// <returns></returns>
		public static DataSet GetInstallParamsAssignedUnassignedUsers(string SystemId)
		{

			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@AGENCY_CODE", SystemId);
				ds = objDataWrapper.ExecuteDataSet(GET_ASSIGNED_UNASSIGNEDUSERS);

				ds.Tables[0].TableName = "AssignedUsers";
				ds.Tables[1].TableName = "UnAssignedUsers";

				return ds;
			}
			catch(Exception exc)
			{
				throw (exc);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		}
		#endregion

		
	}
}
