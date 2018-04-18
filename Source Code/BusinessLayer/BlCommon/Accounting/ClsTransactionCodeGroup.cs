/******************************************************************************************
<Author				: -   Ajit Singh Chahal
<Start Date				: -	6/7/2005 7:37:50 PM
<End Date				: -	
<Description				: - 	BL for Transacton code group.
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - 
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Maintenance.Accounting;
using System.Web.UI.WebControls;

namespace Cms.BusinessLayer.BlCommon.Accounting
{
	/// <summary>
	/// BL for Transacton code group.
	/// </summary>
	public class ClsTransactionCodeGroup : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{
		private const	string		ACT_TRAN_CODE_GROUP			=	"ACT_TRAN_CODE_GROUP";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		//private int _TRAN_GROUP_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateTransactionGroups";
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
		public ClsTransactionCodeGroup()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		public ClsTransactionCodeGroup(bool transactionLogRequired):this()
		{
			base.TransactionLogRequired = transactionLogRequired;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objTransactionCodeGroupInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsTransactionCodeGroupInfo objTransactionCodeGroupInfo)
		{              
			string		strStoredProc	=	"Proc_InsertACT_TRAN_CODE_GROUP";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@COUNTRY_ID",objTransactionCodeGroupInfo.COUNTRY_ID);
				objDataWrapper.AddParameter("@STATE_ID",objTransactionCodeGroupInfo.STATE_ID);
				objDataWrapper.AddParameter("@LOB_ID",objTransactionCodeGroupInfo.LOB_ID);
				objDataWrapper.AddParameter("@SUB_LOB_ID",objTransactionCodeGroupInfo.SUB_LOB_ID);
				objDataWrapper.AddParameter("@CLASS_RISK",objTransactionCodeGroupInfo.CLASS_RISK);
				objDataWrapper.AddParameter("@TERM",objTransactionCodeGroupInfo.TERM);
				objDataWrapper.AddParameter("@POLICY_TYPE",objTransactionCodeGroupInfo.POLICY_TYPE);
				objDataWrapper.AddParameter("@IS_ACTIVE",objTransactionCodeGroupInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objTransactionCodeGroupInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objTransactionCodeGroupInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",objTransactionCodeGroupInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objTransactionCodeGroupInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@NEW_BUSINESS",objTransactionCodeGroupInfo.NEW_BUSINESS);
				objDataWrapper.AddParameter("@CHANGE_IN_NEW_BUSINESS",objTransactionCodeGroupInfo.CHANGE_IN_NEW_BUSINESS);
				objDataWrapper.AddParameter("@RENEWAL",objTransactionCodeGroupInfo.RENEWAL);
				objDataWrapper.AddParameter("@CHANGE_IN_RENEWAL",objTransactionCodeGroupInfo.CHANGE_IN_RENEWAL);
				objDataWrapper.AddParameter("@REINSTATE_SAME_TERM",objTransactionCodeGroupInfo.REINSTATE_SAME_TERM);
				objDataWrapper.AddParameter("@REINSTATE_NEW_TERM",objTransactionCodeGroupInfo.REINSTATE_NEW_TERM);
				objDataWrapper.AddParameter("@CANCELLATION",objTransactionCodeGroupInfo.CANCELLATION);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@TRAN_GROUP_ID",objTransactionCodeGroupInfo .TRAN_GROUP_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objTransactionCodeGroupInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddTransactionCodeGroup.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objTransactionCodeGroupInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objTransactionCodeGroupInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Record Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int TRAN_GROUP_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (TRAN_GROUP_ID == -1)
				{
					return -1;
				}
				else
				{
					objTransactionCodeGroupInfo.TRAN_GROUP_ID = TRAN_GROUP_ID;
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
		/// <param name="objOldTransactionCodeGroupInfo">Model object having old information</param>
		/// <param name="objTransactionCodeGroupInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsTransactionCodeGroupInfo objOldTransactionCodeGroupInfo,ClsTransactionCodeGroupInfo objTransactionCodeGroupInfo)
		{
			string		strStoredProc	=	"Proc_UpdateACT_TRAN_CODE_GROUP";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@TRAN_GROUP_ID",objTransactionCodeGroupInfo.TRAN_GROUP_ID);
				objDataWrapper.AddParameter("@COUNTRY_ID",objTransactionCodeGroupInfo.COUNTRY_ID);
				objDataWrapper.AddParameter("@STATE_ID",objTransactionCodeGroupInfo.STATE_ID);
				objDataWrapper.AddParameter("@LOB_ID",objTransactionCodeGroupInfo.LOB_ID);
				objDataWrapper.AddParameter("@SUB_LOB_ID",objTransactionCodeGroupInfo.SUB_LOB_ID);
				objDataWrapper.AddParameter("@CLASS_RISK",objTransactionCodeGroupInfo.CLASS_RISK);
				objDataWrapper.AddParameter("@TERM",objTransactionCodeGroupInfo.TERM);
				objDataWrapper.AddParameter("@POLICY_TYPE",objTransactionCodeGroupInfo.POLICY_TYPE);
				objDataWrapper.AddParameter("@MODIFIED_BY",objTransactionCodeGroupInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objTransactionCodeGroupInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@NEW_BUSINESS",objTransactionCodeGroupInfo.NEW_BUSINESS);
				objDataWrapper.AddParameter("@CHANGE_IN_NEW_BUSINESS",objTransactionCodeGroupInfo.CHANGE_IN_NEW_BUSINESS);
				objDataWrapper.AddParameter("@RENEWAL",objTransactionCodeGroupInfo.RENEWAL);
				objDataWrapper.AddParameter("@CHANGE_IN_RENEWAL",objTransactionCodeGroupInfo.CHANGE_IN_RENEWAL);
				objDataWrapper.AddParameter("@REINSTATE_SAME_TERM",objTransactionCodeGroupInfo.REINSTATE_SAME_TERM);
				objDataWrapper.AddParameter("@REINSTATE_NEW_TERM",objTransactionCodeGroupInfo.REINSTATE_NEW_TERM);
				objDataWrapper.AddParameter("@CANCELLATION",objTransactionCodeGroupInfo.CANCELLATION);
				

				if(base.TransactionLogRequired) 
				{
					objTransactionCodeGroupInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddTransactionCodeGroup.aspx.resx");
					objBuilder.GetUpdateSQL(objOldTransactionCodeGroupInfo,objTransactionCodeGroupInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objTransactionCodeGroupInfo.MODIFIED_BY;
					objTransactionInfo.TRANS_DESC		=	"Information Has Been Updated";
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

		#region "Get Xml Methods"
		public static DataSet GetXmlForPageControls(string TRAN_GROUP_ID)	
		{
			string spName= "Proc_GetTransactionCodeGroupXML";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.AddParameter("@TRAN_GROUP_ID",TRAN_GROUP_ID);
			return objDataWrapper.ExecuteDataSet(spName);
		}
		#endregion
	}
}
