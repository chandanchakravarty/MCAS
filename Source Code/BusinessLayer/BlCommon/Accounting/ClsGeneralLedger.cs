/******************************************************************************************
<Author				: -   Ajit Singh Chahal
<Start Date				: -	5/16/2005 3:58:52 PM
<End Date				: -	
<Description				: - 	Business Logic for General Ledger.
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
using Cms.Model.Maintenance;
using System.Web.UI.WebControls;

namespace Cms.BusinessLayer.BlCommon.Accounting
{
	/// <summary>
	/// Business Logic for General Ledger.
	/// </summary>
	public class ClsGeneralLedger : Cms.BusinessLayer.BlCommon.ClsCommon,IDisposable
	{

		private const	string		ACT_GENERAL_LEDGER			=	"ACT_GENERAL_LEDGER";
		
		#region Private Instance Variables
		private			bool		boolTransactionLog;
		// private int _GL_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateACT_GENERAL_LEDGER";
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
		public ClsGeneralLedger()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		public ClsGeneralLedger(bool transactionLogRequired):this()
		{
			base.TransactionLogRequired = transactionLogRequired;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objGeneralLedgerInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsGeneralLedgerInfo objGeneralLedgerInfo)
		{
			string		strStoredProc	=	"Proc_InsertACT_GENERAL_LEDGER";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@LEDGER_NAME",objGeneralLedgerInfo.LEDGER_NAME);
				objDataWrapper.AddParameter("@FISCAL_BEGIN_DATE",objGeneralLedgerInfo.FISCAL_BEGIN_DATE);
				objDataWrapper.AddParameter("@FISCAL_END_DATE",objGeneralLedgerInfo.FISCAL_END_DATE);
				objDataWrapper.AddParameter("@MONTH_BEGINING",objGeneralLedgerInfo.MONTH_BEGINING);
				objDataWrapper.AddParameter("@FORBID_POSTING",objGeneralLedgerInfo.FORBID_POSTING);
				objDataWrapper.AddParameter("@SMALL_BALANCE",objGeneralLedgerInfo.SMALL_BALANCE);
				objDataWrapper.AddParameter("@IS_SYSTEM_LOCK",objGeneralLedgerInfo.IS_SYSTEM_LOCK);
				objDataWrapper.AddParameter("@IS_SYS_OUT_OF_BAL",objGeneralLedgerInfo.IS_SYS_OUT_OF_BAL);

				if(objGeneralLedgerInfo.LAST_EOD_RUN_DATE_TIME==DateTime.Parse("1/1/0001"))
					objDataWrapper.AddParameter("@LAST_EOD_RUN_DATE_TIME",DBNull.Value);
				else
					objDataWrapper.AddParameter("@LAST_EOD_RUN_DATE_TIME",objGeneralLedgerInfo.LAST_EOD_RUN_DATE_TIME);

				if(objGeneralLedgerInfo.LAST_EOM_RUN_DATE_TIME==DateTime.Parse("1/1/0001"))
					objDataWrapper.AddParameter("@LAST_EOM_RUN_DATE_TIME",DBNull.Value);
				else
					objDataWrapper.AddParameter("@LAST_EOM_RUN_DATE_TIME",objGeneralLedgerInfo.LAST_EOM_RUN_DATE_TIME);

				if(objGeneralLedgerInfo.LAST_EOY_RUN_DATE_TIME==DateTime.Parse("1/1/0001"))
					objDataWrapper.AddParameter("@LAST_EOY_RUN_DATE_TIME",DBNull.Value);
				else
					objDataWrapper.AddParameter("@LAST_EOY_RUN_DATE_TIME",objGeneralLedgerInfo.LAST_EOY_RUN_DATE_TIME);

				objDataWrapper.AddParameter("@ACC_SORT_ORDER",objGeneralLedgerInfo.ACC_SORT_ORDER);
				objDataWrapper.AddParameter("@IS_ACTIVE",objGeneralLedgerInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objGeneralLedgerInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objGeneralLedgerInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",objGeneralLedgerInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objGeneralLedgerInfo.LAST_UPDATED_DATETIME);
				//-- 13-jun-2005 ---- changes made as fiscal id is added to table
				//SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@GL_ID",objGeneralLedgerInfo.GL_ID,SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@FISCAL_ID",objGeneralLedgerInfo.FISCAL_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objGeneralLedgerInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddGeneralLedgers.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objGeneralLedgerInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objGeneralLedgerInfo.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Record Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int FISCAL_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if (FISCAL_ID == -1)
				{
					return -1;
				}
				else
				{
					objGeneralLedgerInfo.FISCAL_ID = FISCAL_ID;
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
		/// <param name="objOldGeneralLedgerInfo">Model object having old information</param>
		/// <param name="objGeneralLedgerInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsGeneralLedgerInfo objOldGeneralLedgerInfo,ClsGeneralLedgerInfo objGeneralLedgerInfo)
		{
			string		strStoredProc	=	"Proc_UpdateACT_GENERAL_LEDGER";
			string strTranXML;
			int returnResult = 1;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@GL_ID",objGeneralLedgerInfo.GL_ID);
				objDataWrapper.AddParameter("@FISCAL_ID",objGeneralLedgerInfo.FISCAL_ID);
				objDataWrapper.AddParameter("@LEDGER_NAME",objGeneralLedgerInfo.LEDGER_NAME);
				objDataWrapper.AddParameter("@FISCAL_BEGIN_DATE",objGeneralLedgerInfo.FISCAL_BEGIN_DATE);
				objDataWrapper.AddParameter("@FISCAL_END_DATE",objGeneralLedgerInfo.FISCAL_END_DATE);
				objDataWrapper.AddParameter("@MONTH_BEGINING",objGeneralLedgerInfo.MONTH_BEGINING);
				objDataWrapper.AddParameter("@FORBID_POSTING",objGeneralLedgerInfo.FORBID_POSTING);
				objDataWrapper.AddParameter("@SMALL_BALANCE",objGeneralLedgerInfo.SMALL_BALANCE);
				objDataWrapper.AddParameter("@IS_SYSTEM_LOCK",objGeneralLedgerInfo.IS_SYSTEM_LOCK);
				objDataWrapper.AddParameter("@IS_SYS_OUT_OF_BAL",objGeneralLedgerInfo.IS_SYS_OUT_OF_BAL);
				if(objGeneralLedgerInfo.LAST_EOD_RUN_DATE_TIME==DateTime.Parse("1/1/0001"))
					objDataWrapper.AddParameter("@LAST_EOD_RUN_DATE_TIME",DBNull.Value);
				else
					objDataWrapper.AddParameter("@LAST_EOD_RUN_DATE_TIME",objGeneralLedgerInfo.LAST_EOD_RUN_DATE_TIME);

				if(objGeneralLedgerInfo.LAST_EOM_RUN_DATE_TIME==DateTime.Parse("1/1/0001"))
					objDataWrapper.AddParameter("@LAST_EOM_RUN_DATE_TIME",DBNull.Value);
				else
					objDataWrapper.AddParameter("@LAST_EOM_RUN_DATE_TIME",objGeneralLedgerInfo.LAST_EOM_RUN_DATE_TIME);

				if(objGeneralLedgerInfo.LAST_EOY_RUN_DATE_TIME==DateTime.Parse("1/1/0001"))
					objDataWrapper.AddParameter("@LAST_EOY_RUN_DATE_TIME",DBNull.Value);
				else
					objDataWrapper.AddParameter("@LAST_EOY_RUN_DATE_TIME",objGeneralLedgerInfo.LAST_EOY_RUN_DATE_TIME);
				objDataWrapper.AddParameter("@ACC_SORT_ORDER",objGeneralLedgerInfo.ACC_SORT_ORDER);
				objDataWrapper.AddParameter("@MODIFIED_BY",objGeneralLedgerInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objGeneralLedgerInfo.LAST_UPDATED_DATETIME);
				if(base.TransactionLogRequired) 
				{
					objGeneralLedgerInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddGeneralLedgers.aspx.resx");
					objBuilder.GetUpdateSQL(objOldGeneralLedgerInfo,objGeneralLedgerInfo,out strTranXML);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
	

					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objGeneralLedgerInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1579", "");//"Information Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					if(strTranXML!="")
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

		#region Update: Asset method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldGeneralLedgerInfo">Model object having old information</param>
		/// <param name="objGeneralLedgerInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update_Asset(ClsPostingInterfaceInfo objOldPostingInterfaceInfo,ClsPostingInterfaceInfo objPostingInterfaceInfo)
		{
			string		strStoredProc	=	"Proc_UpdatePostingInterface_Asset";
			string strTranXML;
			int returnResult = 1;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@GL_ID",objPostingInterfaceInfo.GL_ID);
				objDataWrapper.AddParameter("@FISCAL_ID",objPostingInterfaceInfo.FISCAL_ID);
				objDataWrapper.AddParameter("@AST_UNCOLL_PRM_CUSTOMER",objPostingInterfaceInfo.AST_UNCOLL_PRM_CUSTOMER);
				objDataWrapper.AddParameter("@AST_UNCOLL_PRM_AGENCY",objPostingInterfaceInfo.AST_UNCOLL_PRM_AGENCY);
				objDataWrapper.AddParameter("@AST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL",objPostingInterfaceInfo.AST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL);
				objDataWrapper.AddParameter("@AST_PRM_WRIT_SUSPENSE_DIRECT_BILL",objPostingInterfaceInfo.AST_PRM_WRIT_SUSPENSE_DIRECT_BILL);
				objDataWrapper.AddParameter("@AST_PRM_WRIT_SUSPENSE_AGENCY_BILL",objPostingInterfaceInfo.AST_PRM_WRIT_SUSPENSE_AGENCY_BILL);
				objDataWrapper.AddParameter("@AST_MCCA_FEE_SUSPENSE_DIRECT_BILL",objPostingInterfaceInfo.AST_MCCA_FEE_SUSPENSE_DIRECT_BILL);
				objDataWrapper.AddParameter("@AST_MCCA_FEE_SUSPENSE_AGENCY_BILL",objPostingInterfaceInfo.AST_MCCA_FEE_SUSPENSE_AGENCY_BILL);
				objDataWrapper.AddParameter("@AST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL",objPostingInterfaceInfo.AST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL);
				objDataWrapper.AddParameter("@AST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL",objPostingInterfaceInfo.AST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL);
				objDataWrapper.AddParameter("@AST_COMM_RECV_REINS_EXCESS_CONTRACT",objPostingInterfaceInfo.AST_COMM_RECV_REINS_EXCESS_CONTRACT);
				objDataWrapper.AddParameter("@AST_COMM_RECV_REINS_UMBRELLA_CONTRACT",objPostingInterfaceInfo.AST_COMM_RECV_REINS_UMBRELLA_CONTRACT);
				
				objDataWrapper.AddParameter("@AST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL",objPostingInterfaceInfo.AST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL);
				objDataWrapper.AddParameter("@AST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL",objPostingInterfaceInfo.AST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL);
				objDataWrapper.AddParameter("@AST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL",objPostingInterfaceInfo.AST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL);
				objDataWrapper.AddParameter("@AST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL",objPostingInterfaceInfo.AST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL);
				objDataWrapper.AddParameter("@AST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL",objPostingInterfaceInfo.AST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL);

				
				objDataWrapper.AddParameter("@MODIFIED_BY",objPostingInterfaceInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objPostingInterfaceInfo.LAST_UPDATED_DATETIME);

				if(base.TransactionLogRequired) 
				{
					objPostingInterfaceInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/PiAsset.aspx.resx");
					objBuilder.GetUpdateSQL(objOldPostingInterfaceInfo,objPostingInterfaceInfo,out strTranXML);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
	

					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objPostingInterfaceInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1579", "");//"Information Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					if(strTranXML!="")
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

		#region Update: Liability method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldGeneralLedgerInfo">Model object having old information</param>
		/// <param name="objGeneralLedgerInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update_Liability(ClsPostingInterfaceInfo objOldPostingInterfaceInfo,ClsPostingInterfaceInfo objPostingInterfaceInfo)
		{
			string		strStoredProc	=	"Proc_UpdatePostingInterface_Liability";
			string strTranXML;
			int returnResult = 1;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@GL_ID",objPostingInterfaceInfo.GL_ID);
				objDataWrapper.AddParameter("@FISCAL_ID",objPostingInterfaceInfo.FISCAL_ID);
				objDataWrapper.AddParameter("@LIB_COMM_PAYB_AGENCY_BILL",objPostingInterfaceInfo.LIB_COMM_PAYB_AGENCY_BILL);
				objDataWrapper.AddParameter("@LIB_COMM_PAYB_DIRECT_BILL",objPostingInterfaceInfo.LIB_COMM_PAYB_DIRECT_BILL);
				objDataWrapper.AddParameter("@LIB_REINS_PAYB_EXCESS_CONTRACT",objPostingInterfaceInfo.LIB_REINS_PAYB_EXCESS_CONTRACT);
				objDataWrapper.AddParameter("@LIB_REINS_PAYB_CAT_CONTRACT",objPostingInterfaceInfo.LIB_REINS_PAYB_CAT_CONTRACT);
				objDataWrapper.AddParameter("@LIB_REINS_PAYB_MCCA",objPostingInterfaceInfo.LIB_REINS_PAYB_MCCA);
				objDataWrapper.AddParameter("@LIB_REINS_PAYB_UMBRELLA",objPostingInterfaceInfo.LIB_REINS_PAYB_UMBRELLA);
				objDataWrapper.AddParameter("@LIB_REINS_PAYB_FACULTATIVE",objPostingInterfaceInfo.LIB_REINS_PAYB_FACULTATIVE);
				objDataWrapper.AddParameter("@LIB_OUT_DRAFTS",objPostingInterfaceInfo.LIB_OUT_DRAFTS);
				objDataWrapper.AddParameter("@LIB_ADVCE_PRM_DEPOSIT",objPostingInterfaceInfo.LIB_ADVCE_PRM_DEPOSIT);
				objDataWrapper.AddParameter("@LIB_ADVCE_PRM_DEPOSIT_2M",objPostingInterfaceInfo.LIB_ADVCE_PRM_DEPOSIT_2M);
				objDataWrapper.AddParameter("@LIB_UNEARN_PRM",objPostingInterfaceInfo.LIB_UNEARN_PRM);
				objDataWrapper.AddParameter("@LIB_UNEARN_PRM_MCCA",objPostingInterfaceInfo.LIB_UNEARN_PRM_MCCA);
				objDataWrapper.AddParameter("@LIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS",objPostingInterfaceInfo.LIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS);
				objDataWrapper.AddParameter("@LIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS",objPostingInterfaceInfo.LIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS);
				objDataWrapper.AddParameter("@LIB_UNEARN_PRM_OTH_STATE_ASSES_FEE",objPostingInterfaceInfo.LIB_UNEARN_PRM_OTH_STATE_ASSES_FEE);
				//objDataWrapper.AddParameter("@LIB_TAX_PAYB",objPostingInterfaceInfo.LIB_TAX_PAYB);
				objDataWrapper.AddParameter("@LIB_VENDOR_PAYB",objPostingInterfaceInfo.LIB_VENDOR_PAYB);
				objDataWrapper.AddParameter("@LIB_COLL_ON_NONISSUED_POLICY",objPostingInterfaceInfo.LIB_COLL_ON_NONISSUED_POLICY);
				objDataWrapper.AddParameter("@MODIFIED_BY",objPostingInterfaceInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objPostingInterfaceInfo.LAST_UPDATED_DATETIME);

				if(base.TransactionLogRequired) 
				{
					objPostingInterfaceInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/PiLiabilities.aspx.resx");
					objBuilder.GetUpdateSQL(objOldPostingInterfaceInfo,objPostingInterfaceInfo,out strTranXML);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
	

					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objPostingInterfaceInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1579", "");//"Information Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					if(strTranXML!="")
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

		#region Update: Equity method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldGeneralLedgerInfo">Model object having old information</param>
		/// <param name="objGeneralLedgerInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update_Equity(ClsPostingInterfaceInfo objOldPostingInterfaceInfo,ClsPostingInterfaceInfo objPostingInterfaceInfo)
		{
			string		strStoredProc	=	"Proc_UpdatePostingInterface_Equity";
			string strTranXML;
			int returnResult = 1;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@GL_ID",objPostingInterfaceInfo.GL_ID);
				objDataWrapper.AddParameter("@FISCAL_ID",objPostingInterfaceInfo.FISCAL_ID);
				objDataWrapper.AddParameter("@EQU_TRANSFER",objPostingInterfaceInfo.EQU_TRANSFER);
				objDataWrapper.AddParameter("@EQU_UNASSIGNED_SURPLUS",objPostingInterfaceInfo.EQU_UNASSIGNED_SURPLUS);
				objDataWrapper.AddParameter("@MODIFIED_BY",objPostingInterfaceInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objPostingInterfaceInfo.LAST_UPDATED_DATETIME);

				if(base.TransactionLogRequired) 
				{
					objPostingInterfaceInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/PiEquity.aspx.resx");
					objBuilder.GetUpdateSQL(objOldPostingInterfaceInfo,objPostingInterfaceInfo,out strTranXML);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
	

					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objPostingInterfaceInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1579", "");// "Information Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					if(strTranXML!="")
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

		#region Update_Bank_AC_Mapping
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldGeneralLedgerInfo">Model object having old information</param>
		/// <param name="objGeneralLedgerInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update_Bank_AC_Mapping(ClsPostingInterfaceInfo objOldPostingInterfaceInfo,ClsPostingInterfaceInfo objPostingInterfaceInfo)
		{
			string		strStoredProc	=	"Proc_UpdateBankAccount_Mapping";
			string strTranXML;
			int returnResult = 1;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@GL_ID",objPostingInterfaceInfo.GL_ID);		
				objDataWrapper.AddParameter("@FISCAL_ID",objPostingInterfaceInfo.FISCAL_ID);	
				objDataWrapper.AddParameter("@BNK_OVER_PAYMENT",objPostingInterfaceInfo.BNK_OVER_PAYMENT);
				objDataWrapper.AddParameter("@BNK_RETURN_PRM_PAYMENT",objPostingInterfaceInfo.BNK_RETURN_PRM_PAYMENT);
				objDataWrapper.AddParameter("@BNK_SUSPENSE_AMOUNT",objPostingInterfaceInfo.BNK_SUSPENSE_AMOUNT);
				objDataWrapper.AddParameter("@BNK_CLAIMS_DEFAULT_AC",objPostingInterfaceInfo.BNK_CLAIMS_DEFAULT_AC);
				objDataWrapper.AddParameter("@BNK_REINSURANCE_DEFAULT_AC",objPostingInterfaceInfo.BNK_REINSURANCE_DEFAULT_AC);
				objDataWrapper.AddParameter("@BNK_DEPOSITS_DEFAULT_AC",objPostingInterfaceInfo.BNK_DEPOSITS_DEFAULT_AC);
				objDataWrapper.AddParameter("@MODIFIED_BY",objPostingInterfaceInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objPostingInterfaceInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@BNK_MISC_CHK_DEFAULT_AC",objPostingInterfaceInfo.BNK_MISC_CHK_DEFAULT_AC);
				objDataWrapper.AddParameter("@CLM_CHECK_DEFAULT_AC",objPostingInterfaceInfo.CLM_CHECK_DEFAULT_AC);
				objDataWrapper.AddParameter("@BNK_CUST_DEP_EFT_CARD",objPostingInterfaceInfo.BNK_CUST_DEP_EFT_CARD);
				objDataWrapper.AddParameter("@BNK_AGEN_CHK_DEFAULT_AC",objPostingInterfaceInfo.BNK_AGEN_CHK_DEFAULT_AC);

				if(base.TransactionLogRequired) 
				{
					objPostingInterfaceInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/BankAccountMapping.aspx.resx");
					objBuilder.GetUpdateSQL(objOldPostingInterfaceInfo,objPostingInterfaceInfo,out strTranXML);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
	

					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objPostingInterfaceInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1579", "");// "Information Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					if(strTranXML!="")
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

		#region Update: Income method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldGeneralLedgerInfo">Model object having old information</param>
		/// <param name="objGeneralLedgerInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update_Income(ClsPostingInterfaceInfo objOldPostingInterfaceInfo,ClsPostingInterfaceInfo objPostingInterfaceInfo)
		{
			string		strStoredProc	=	"Proc_UpdatePostingInterface_Income";
			string strTranXML;
			int returnResult = 1;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@GL_ID",objPostingInterfaceInfo.GL_ID);
				objDataWrapper.AddParameter("@FISCAL_ID",objPostingInterfaceInfo.FISCAL_ID);
				objDataWrapper.AddParameter("@INC_PRM_WRTN",objPostingInterfaceInfo.INC_PRM_WRTN);
				objDataWrapper.AddParameter("@INC_PRM_WRTN_MCCA",objPostingInterfaceInfo.INC_PRM_WRTN_MCCA);
				objDataWrapper.AddParameter("@INC_PRM_WRTN_OTH_STATE_ASSESS_FEE",objPostingInterfaceInfo.INC_PRM_WRTN_OTH_STATE_ASSESS_FEE);
				objDataWrapper.AddParameter("@INC_REINS_CEDED_EXCESS_CON",objPostingInterfaceInfo.INC_REINS_CEDED_EXCESS_CON);
				objDataWrapper.AddParameter("@INC_REINS_CEDED_CAT_CON",objPostingInterfaceInfo.INC_REINS_CEDED_CAT_CON);
				objDataWrapper.AddParameter("@INC_REINS_CEDED_UMBRELLA_CON",objPostingInterfaceInfo.INC_REINS_CEDED_UMBRELLA_CON);
				objDataWrapper.AddParameter("@INC_REINS_CEDED_FACUL_CON",objPostingInterfaceInfo.INC_REINS_CEDED_FACUL_CON);
				objDataWrapper.AddParameter("@INC_REINS_CEDED_MCCA_CON",objPostingInterfaceInfo.INC_REINS_CEDED_MCCA_CON);
				objDataWrapper.AddParameter("@INC_CHG_UNEARN_PRM",objPostingInterfaceInfo.INC_CHG_UNEARN_PRM);
				objDataWrapper.AddParameter("@INC_CHG_UNEARN_PRM_MCCA",objPostingInterfaceInfo.INC_CHG_UNEARN_PRM_MCCA);
				objDataWrapper.AddParameter("@INC_CHG_UNEARN_PRM_OTH_STATE_FEE",objPostingInterfaceInfo.INC_CHG_UNEARN_PRM_OTH_STATE_FEE);
				objDataWrapper.AddParameter("@INC_CHG_CEDED_UNEARN_MCCA",objPostingInterfaceInfo.INC_CHG_CEDED_UNEARN_MCCA);
				objDataWrapper.AddParameter("@INC_CHG_CEDED_UNEARN_UMBRELLA_REINS",objPostingInterfaceInfo.INC_CHG_CEDED_UNEARN_UMBRELLA_REINS);
				objDataWrapper.AddParameter("@INC_INSTALLMENT_FEES",objPostingInterfaceInfo.INC_INSTALLMENT_FEES);
				objDataWrapper.AddParameter("@INC_RE_INSTATEMENT_FEES",objPostingInterfaceInfo.INC_RE_INSTATEMENT_FEES);
				objDataWrapper.AddParameter("@INC_NON_SUFFICIENT_FUND_FEES",objPostingInterfaceInfo.INC_NON_SUFFICIENT_FUND_FEES);
				objDataWrapper.AddParameter("@INC_LATE_FEES",objPostingInterfaceInfo.INC_LATE_FEES);
				objDataWrapper.AddParameter("@INC_SERVICE_CHARGE",objPostingInterfaceInfo.INC_SERVICE_CHARGE);
				objDataWrapper.AddParameter("@INC_CONVENIENCE_FEE",objPostingInterfaceInfo.INC_CONVENIENCE_FEE);

                //Added by Pradeep Kushwaha on 30-August-2010
                objDataWrapper.AddParameter("@INC_INTEREST_AMOUNT", objPostingInterfaceInfo.INC_INTEREST_AMOUNT);
                objDataWrapper.AddParameter("@INC_POLICY_TAXES", objPostingInterfaceInfo.INC_POLICY_TAXES);
                objDataWrapper.AddParameter("@INC_POLICY_FEES", objPostingInterfaceInfo.INC_POLICY_FEES);
                //Added till here 

				objDataWrapper.AddParameter("@MODIFIED_BY",objPostingInterfaceInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objPostingInterfaceInfo.LAST_UPDATED_DATETIME);

				if(base.TransactionLogRequired) 
				{
					objPostingInterfaceInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/PiIncome.aspx.resx");
					objBuilder.GetUpdateSQL(objOldPostingInterfaceInfo,objPostingInterfaceInfo,out strTranXML);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
	

					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objPostingInterfaceInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1579", "");// "Information has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					if(strTranXML!="")
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

		#region Update: Expense method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldGeneralLedgerInfo">Model object having old information</param>
		/// <param name="objGeneralLedgerInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update_Expense(ClsPostingInterfaceInfo objOldPostingInterfaceInfo,ClsPostingInterfaceInfo objPostingInterfaceInfo)
		{
			string		strStoredProc	=	"Proc_UpdatePostingInterface_Expense";
			string strTranXML;
			int returnResult = 1;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@GL_ID",objPostingInterfaceInfo.GL_ID);
				objDataWrapper.AddParameter("@FISCAL_ID",objPostingInterfaceInfo.FISCAL_ID);
				objDataWrapper.AddParameter("@EXP_COMM_INCURRED",objPostingInterfaceInfo.EXP_COMM_INCURRED);
				objDataWrapper.AddParameter("@EXP_REINS_COMM_EXCESS_CON",objPostingInterfaceInfo.EXP_REINS_COMM_EXCESS_CON);
				objDataWrapper.AddParameter("@EXP_REINS_COMM_UMBRELLA_CON",objPostingInterfaceInfo.EXP_REINS_COMM_UMBRELLA_CON);
				objDataWrapper.AddParameter("@EXP_ASSIGNED_CLAIMS",objPostingInterfaceInfo.EXP_ASSIGNED_CLAIMS);
				objDataWrapper.AddParameter("@EXP_REINS_PAID_LOSSES",objPostingInterfaceInfo.EXP_REINS_PAID_LOSSES);
				objDataWrapper.AddParameter("@EXP_REINS_PAID_LOSSES_CAT",objPostingInterfaceInfo.EXP_REINS_PAID_LOSSES_CAT);
				objDataWrapper.AddParameter("@EXP_SMALL_BALANCE_WRITE_OFF",objPostingInterfaceInfo.EXP_SMALL_BALANCE_WRITE_OFF);
				objDataWrapper.AddParameter("@MODIFIED_BY",objPostingInterfaceInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objPostingInterfaceInfo.LAST_UPDATED_DATETIME);

				if(base.TransactionLogRequired) 
				{
					objPostingInterfaceInfo.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/PiExpenses.aspx.resx");
					objBuilder.GetUpdateSQL(objOldPostingInterfaceInfo,objPostingInterfaceInfo,out strTranXML);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
	

					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objPostingInterfaceInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1579", "");//"Information Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					if(strTranXML!="")
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
		public static string GetXmlForPageControls(string fiscalYear)
		{
			string GL_ID="";
			string FISCAL_ID="";
			return GetXmlForPageControls(fiscalYear,ref GL_ID,ref FISCAL_ID);
		}
		public static string GetXmlForPageControls(string fiscalYear,ref string GL_ID,ref string FISCAL_ID)
		{
			string strSql = "select FISCAL_ID,year(FISCAL_BEGIN_DATE) as FiscalYearFrom,convert(varchar,FISCAL_BEGIN_DATE,101) as FISCAL_BEGIN_DATE,convert(varchar,FISCAL_END_DATE,101) as FISCAL_END_DATE,GL_ID,LEDGER_NAME,month(FISCAL_BEGIN_DATE) as FISCAL_BEGIN_MONTH,datediff(mm,FISCAL_BEGIN_DATE,FISCAL_END_DATE)+1 as FISCAL_END_MONTH,MONTH_BEGINING,FORBID_POSTING,SMALL_BALANCE,IS_ACTIVE";
			strSql += " from ACT_GENERAL_LEDGER";
			strSql += " where  year(FISCAL_BEGIN_DATE)="+fiscalYear;
			DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);
			if(objDataSet.Tables[0].Rows.Count<=0)
				return "";
			else
			{
				GL_ID = objDataSet.Tables[0].Rows[0]["GL_ID"].ToString();
				FISCAL_ID = objDataSet.Tables[0].Rows[0]["FISCAL_ID"].ToString();
				return objDataSet.GetXml();
			}
		}
		public static string GetXmlForPageControlsForNewFiscalYear()
		{
			string strSql = "select  isnull(year(max(FISCAL_begin_DATE))+1, year(getdate()))  as FiscalYearFrom,isnull(month(max(FISCAL_END_DATE))+1,4) as FISCAL_BEGIN_MONTH,isnull(month(max(FISCAL_END_DATE)),3) as FISCAL_END_MONTH";
			strSql += " from ACT_GENERAL_LEDGER";
			DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);
			if(objDataSet.Tables[0].Rows.Count<=0)
				return "";
			else
				return objDataSet.GetXml();
		}

		public static string GetXmlForPageControls_Asset(string GL_ID,string FISCAL_ID)
		{
			string strSql = "select GL_ID,AST_UNCOLL_PRM_CUSTOMER,AST_UNCOLL_PRM_AGENCY,AST_UNCOLL_PRM_SUSPENSE_AGENCY_BILL,AST_PRM_WRIT_SUSPENSE_DIRECT_BILL,AST_PRM_WRIT_SUSPENSE_AGENCY_BILL,AST_MCCA_FEE_SUSPENSE_DIRECT_BILL,AST_MCCA_FEE_SUSPENSE_AGENCY_BILL,AST_OTHER_STATE_ASSMT_FEE_SUSPENSE_DIRECT_BILL,AST_OTHER_STATE_ASSMT_FEE_SUSPENSE_AGENCY_BILL,AST_COMM_RECV_REINS_EXCESS_CONTRACT,AST_COMM_RECV_REINS_UMBRELLA_CONTRACT,AST_UNCOLL_PREM_IN_SUSPENSE_DIRECT_BILL     ,AST_COMM_EXPENSE_IN_SUSPENSE_DIRECT_BILL     ,AST_COMM_PAYABLE_IN_SUSPENSE_DIRECT_BILL     ,AST_COMM_EXPENSE_IN_SUSPENSE_AGENCY_BILL     ,AST_COMM_PAYABLE_IN_SUSPENSE_AGENCY_BILL     ";
			strSql += " from ACT_GENERAL_LEDGER";
			strSql += " where  GL_ID="+GL_ID;
			strSql += " and FISCAL_ID="+FISCAL_ID;
			DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);
			if(objDataSet.Tables[0].Rows.Count<=0)
				return "";
			else
				return objDataSet.GetXml();
		}
		
		public static string GetXmlForPageControls_Liability(string GL_ID,string FISCAL_ID)
		{
			//Removed: LIB_TAX_PAYB, on 29/7/2005
			string strSql = "select GL_ID,	LIB_COMM_PAYB_AGENCY_BILL ,LIB_COMM_PAYB_DIRECT_BILL			,	LIB_REINS_PAYB_EXCESS_CONTRACT			,	LIB_REINS_PAYB_CAT_CONTRACT			,	LIB_REINS_PAYB_MCCA			,	LIB_REINS_PAYB_UMBRELLA			,	LIB_REINS_PAYB_FACULTATIVE			,	LIB_OUT_DRAFTS			,	LIB_ADVCE_PRM_DEPOSIT			,	LIB_ADVCE_PRM_DEPOSIT_2M			,	LIB_UNEARN_PRM			,	LIB_UNEARN_PRM_MCCA			,	LIB_UNEARN_PRM_CEDED_UNEARN_MCCA_REINS			,	LIB_UNEARN_PRM_CEDED_UNEARN_UMBRELLA_REINS			,	LIB_UNEARN_PRM_OTH_STATE_ASSES_FEE			,		LIB_VENDOR_PAYB			,	LIB_COLL_ON_NONISSUED_POLICY			";
			strSql += " from ACT_GENERAL_LEDGER";
			strSql += " where  GL_ID="+GL_ID;
			strSql += " and FISCAL_ID="+FISCAL_ID;
			DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);
			if(objDataSet.Tables[0].Rows.Count<=0)
				return "";
			else
				return objDataSet.GetXml();
		}
		
		public static string GetXmlForPageControls_Equity(string GL_ID,string FISCAL_ID)
		{
			string strSql = "select GL_ID,EQU_TRANSFER,EQU_UNASSIGNED_SURPLUS";
			strSql += " from ACT_GENERAL_LEDGER";
			strSql += " where  GL_ID="+GL_ID;
			strSql += " and FISCAL_ID ="+FISCAL_ID;
			DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);
			if(objDataSet.Tables[0].Rows.Count<=0)
				return "";
			else
				return objDataSet.GetXml();
		}
		
		public static string GetXmlForPageControls_Bnk_AC_Mapping(string GL_ID,string FISCAL_ID)
		{
			string strSql = "select GL_ID,BNK_OVER_PAYMENT,BNK_RETURN_PRM_PAYMENT,BNK_SUSPENSE_AMOUNT,BNK_CLAIMS_DEFAULT_AC,BNK_REINSURANCE_DEFAULT_AC,BNK_DEPOSITS_DEFAULT_AC,BNK_MISC_CHK_DEFAULT_AC, CLM_CHECK_DEFAULT_AC,BNK_CUST_DEP_EFT_CARD,BNK_AGEN_CHK_DEFAULT_AC,BNK_VEN_CHK_DEFAULT_AC";
			strSql += " from ACT_GENERAL_LEDGER";
			strSql += " where  GL_ID="+GL_ID;
			strSql += " and FISCAL_ID="+FISCAL_ID;
			DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);
			if(objDataSet.Tables[0].Rows.Count<=0)
				return "";
			else
				return objDataSet.GetXml();
		}

		public static string GetXmlForPageControls_Income(string GL_ID,string FISCAL_ID)
		{
            string strSql = "select GL_ID,INC_PRM_WRTN		,		INC_PRM_WRTN_MCCA		,		INC_PRM_WRTN_OTH_STATE_ASSESS_FEE		,		INC_REINS_CEDED_EXCESS_CON		,		INC_REINS_CEDED_CAT_CON		,		INC_REINS_CEDED_UMBRELLA_CON		,		INC_REINS_CEDED_FACUL_CON		,		INC_REINS_CEDED_MCCA_CON		,		INC_CHG_UNEARN_PRM		,		INC_CHG_UNEARN_PRM_MCCA		,		INC_CHG_UNEARN_PRM_OTH_STATE_FEE		,		INC_CHG_CEDED_UNEARN_MCCA		,		INC_CHG_CEDED_UNEARN_UMBRELLA_REINS		,		INC_INSTALLMENT_FEES		,		INC_RE_INSTATEMENT_FEES		,		INC_NON_SUFFICIENT_FUND_FEES		,		INC_LATE_FEES ,   INC_SERVICE_CHARGE  , INC_CONVENIENCE_FEE , INC_INTEREST_AMOUNT , INC_POLICY_TAXES , INC_POLICY_FEES";
			strSql += " from ACT_GENERAL_LEDGER";
			strSql += " where  GL_ID="+GL_ID;
			strSql += " and FISCAL_ID="+FISCAL_ID;
			DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);
			if(objDataSet.Tables[0].Rows.Count<=0)
				return "";
			else
				return objDataSet.GetXml();
		}
		
		public static string GetXmlForPageControls_Expense(string GL_ID,string FISCAL_ID)
		{
			string strSql = "select GL_ID,	EXP_COMM_INCURRED		,		EXP_REINS_COMM_EXCESS_CON		,	EXP_REINS_COMM_UMBRELLA_CON		,		EXP_ASSIGNED_CLAIMS		,		EXP_REINS_PAID_LOSSES		,		EXP_REINS_PAID_LOSSES_CAT,EXP_SMALL_BALANCE_WRITE_OFF";		
			strSql += " from ACT_GENERAL_LEDGER";
			strSql += " where  GL_ID="+GL_ID;
			strSql += "and FISCAL_ID="+FISCAL_ID;
			DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);
			if(objDataSet.Tables[0].Rows.Count<=0)
				return "";
			else
				return objDataSet.GetXml();
		}
		
		#region Get Fiscal ID from Current Date
		public static int GetFiscalID()
		{
			int FiscalID ;  // Will be fetched Based on Date
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);	
			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@FOR_DATE",DateTime.Now, SqlDbType.DateTime);
			SqlParameter objParam = (SqlParameter) objWrapper.AddParameter 
				("@FISCAL_ID", null,SqlDbType.Int,ParameterDirection.Output);	

			objWrapper.ExecuteNonQuery("Proc_GetFiscalIDForCurrentDate");

			FiscalID = Convert.ToInt32(objParam.Value);
			objWrapper.ClearParameteres();
			return FiscalID;

		}
		#endregion
	
		#endregion
		#region "Get Xml Methods for Minimum Premium Amount"
		public static string GetXmlForPageControlsMinimumPremium(string ROW_ID)
		{
            string strSql = "select t1.COUNTRY_ID,t1.ROW_ID,t1.STATE_ID,t1.LOB_ID,t1.SUB_LOB_ID,convert(varchar,t1.EFFECTIVE_FROM_DATE,case when " + ClsCommon.BL_LANG_ID + "=1 then 101 else 103 end) as EFFECTIVE_FROM_DATE,convert(varchar,t1.EFFECTIVE_TO_DATE,case when " + ClsCommon.BL_LANG_ID + "=1 then 101 else 103 end) as EFFECTIVE_TO_DATE,t1.PREMIUM_AMT AS PREMIUM_AMT,t2.COUNTRY_NAME";
			strSql += " from ACT_MINIMUM_PREM_CANCEL t1,MNT_COUNTRY_LIST t2";
			strSql += " where  t1.ROW_ID="+ int.Parse(ROW_ID) +" and t1.COUNTRY_ID=t2.COUNTRY_ID";
			DataSet objDataSet = DataWrapper.ExecuteDataset(ConnStr,CommandType.Text,strSql);
			if(objDataSet.Tables[0].Rows.Count == 0)
				return "";
			else
				return objDataSet.GetXml();
		}
		#endregion
		#region Save Mathed for  minimum premium at cancellation
		public int SaveMinimumPremium(ClsMinimumPremAmt objMinimumPremAMt)
		{
			string		strStoredProc	=	"Proc_SAVE_ACT_MINIMUM_PREMIUM_AMOUNT";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@LOB_ID",objMinimumPremAMt.LOB_ID);
				objDataWrapper.AddParameter("@SUB_LOB_ID",objMinimumPremAMt.SUB_LOB_ID);
				objDataWrapper.AddParameter("@STATE_ID",objMinimumPremAMt.STATE_ID);
				objDataWrapper.AddParameter("@COUNTRY_ID",objMinimumPremAMt.COUNTRY_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",objMinimumPremAMt.IS_ACTIVE);
				objDataWrapper.AddParameter("@EFFECTIVE_FROM_DATE",objMinimumPremAMt.EFFECTIVE_FROM_DATE);
				objDataWrapper.AddParameter("@EFFECTIVE_TO_DATE",objMinimumPremAMt.EFFECTIVE_TO_DATE);
				objDataWrapper.AddParameter("@PREMIUM_AMT",objMinimumPremAMt.PREMIUM_AMT);
				objDataWrapper.AddParameter("@CREATED_BY",objMinimumPremAMt.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objMinimumPremAMt.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",objMinimumPremAMt.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objMinimumPremAMt.LAST_UPDATED_DATETIME);
				//SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@GL_ID",objGeneralLedgerInfo.GL_ID,SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ROW_ID",objMinimumPremAMt.ROW_ID,SqlDbType.Int,ParameterDirection.Output);
				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objMinimumPremAMt.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddMinimumPremAmt.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objMinimumPremAMt);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					int intSubLOB = 0;
					intSubLOB = objMinimumPremAMt.SUB_LOB_ID;
					int intLOB = 0;
					intLOB = objMinimumPremAMt.LOB_ID;
					string strSubLOB ="";
					int intSTATE = 0;
					intSTATE = objMinimumPremAMt.STATE_ID;
					string strSTATE = "";

					if(intSubLOB == 1)
					{
						if(intLOB ==2)
						{
							strSubLOB = "Trailblazer";
						}
						else if(intLOB ==3 ||intLOB ==5)
						{
							strSubLOB = "Regular";
						}
					}
					else if(intSubLOB == 0)
					{
						strSubLOB = "All";
					}
					switch(intSTATE)
					{
						case 0:
							strSTATE ="All";
							break;
						case 14:
							strSTATE ="Indiana";
							break;
						case 22:
							strSTATE ="Michigan";
							break;
						case 49:
							strSTATE ="Wisconsin";
							break;
					}
					
//					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='STATE_ID' and @NewValue='0']","NewValue","All");
//					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.ReplaceNodeAttributeValue(strTranXML,"LabelFieldMapping/Map[@field='SUB_LOB_ID' and @NewValue='0']","NewValue","All");

					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='STATE_ID']");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='SUB_LOB_ID']");											
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objMinimumPremAMt.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Minimum Premium Amount has been added.";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					objTransactionInfo.CUSTOM_INFO		=	";State = " + strSTATE + "<br>SubLOB = " + strSubLOB;	

					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int ROW_ID=0;
				if (objMinimumPremAMt.ROW_ID == 0)
				{
					ROW_ID = int.Parse(objSqlParameter.Value.ToString());
					objDataWrapper.ClearParameteres();
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				}
				else
				{
					ROW_ID=objMinimumPremAMt.ROW_ID;
					objDataWrapper.ClearParameteres();
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				}
				if (ROW_ID == -1)
				{
					return -1;
				}
				else
				{
					objMinimumPremAMt.ROW_ID = ROW_ID;
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
		#region  update Minimum premmium amount

		public int UpdateMinimumPremium(ClsMinimumPremAmt objOldobjMinimumPremAmt, ClsMinimumPremAmt objMinimumPremAMt)
		{
			string		strStoredProc	=	"Proc_UPDATE_ACT_MINIMUM_PREMIUM_AMOUNT";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@LOB_ID",objMinimumPremAMt.LOB_ID);
				objDataWrapper.AddParameter("@SUB_LOB_ID",objMinimumPremAMt.SUB_LOB_ID);
				objDataWrapper.AddParameter("@STATE_ID",objMinimumPremAMt.STATE_ID);
				objDataWrapper.AddParameter("@COUNTRY_ID",objMinimumPremAMt.COUNTRY_ID);
				objDataWrapper.AddParameter("@IS_ACTIVE",objMinimumPremAMt.IS_ACTIVE);
				objDataWrapper.AddParameter("@EFFECTIVE_FROM_DATE",objMinimumPremAMt.EFFECTIVE_FROM_DATE);
				objDataWrapper.AddParameter("@EFFECTIVE_TO_DATE",objMinimumPremAMt.EFFECTIVE_TO_DATE);
				objDataWrapper.AddParameter("@PREMIUM_AMT",objMinimumPremAMt.PREMIUM_AMT);
				objDataWrapper.AddParameter("@CREATED_BY",objMinimumPremAMt.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objMinimumPremAMt.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",objMinimumPremAMt.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objMinimumPremAMt.LAST_UPDATED_DATETIME);
				//SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@GL_ID",objGeneralLedgerInfo.GL_ID,SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ROW_ID",objMinimumPremAMt.ROW_ID,SqlDbType.Int,ParameterDirection.InputOutput);
				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objMinimumPremAMt.TransactLabel = ClsCommon.MapTransactionLabel("/cmsweb/accounting/AddMinimumPremAmt.aspx.resx");
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objOldobjMinimumPremAmt, objMinimumPremAMt);
					
					//Getting Information of old Data
					int intOldSubLOB = 0;
					intOldSubLOB = objOldobjMinimumPremAmt.SUB_LOB_ID;
					int intOldLOB = 0;
					intOldLOB = objOldobjMinimumPremAmt.LOB_ID;
					string strOldSubLOB ="";
					int intOldSTATE = 0;
					intOldSTATE = objOldobjMinimumPremAmt.STATE_ID;
					string strOldSTATE = "";

					if(intOldSubLOB == 1)
					{
						if(intOldLOB ==2)
						{
							strOldSubLOB = "Trailblazer";
						}
						else if(intOldLOB ==3 ||intOldLOB ==5)
						{
							strOldSubLOB = "Regular";
						}
					}
					else if(intOldSubLOB == 0)
					{
						strOldSubLOB = "All";
					}
					switch(intOldSTATE)
					{
						case 0:
							strOldSTATE ="All";
							break;
						case 14:
							strOldSTATE ="Indiana";
							break;
						case 22:
							strOldSTATE ="Michigan";
							break;
						case 49:
							strOldSTATE ="Wisconsin";
							break;
					}
					//Getting Information of New Data
					int intSubLOB = 0;
					intSubLOB = objMinimumPremAMt.SUB_LOB_ID;
					int intLOB = 0;
					intLOB = objMinimumPremAMt.LOB_ID;
					string strLOB ="";
					string strSubLOB ="";
					int intSTATE = 0;
					intSTATE = objMinimumPremAMt.STATE_ID;
					string strSTATE = "";

					if(intSubLOB == 1)
					{
						if(intLOB ==2)
						{
							strSubLOB = "Trailblazer";
						}
						else if(intLOB ==3 ||intLOB ==5)
						{
							strSubLOB = "Regular";
						}
					}
					else if(intSubLOB == 0)
					{
						strSubLOB = "All";
					}
					switch(intSTATE)
					{
						case 0:
							strSTATE ="All";
							break;
						case 14:
							strSTATE ="Indiana";
							break;
						case 22:
							strSTATE ="Michigan";
							break;
						case 49:
							strSTATE ="Wisconsin";
							break;
					}

					switch(intLOB)
					{
						case 1:
							strLOB ="Homeowners";
							break;
						case 2:
							strLOB ="Automobile";
							break;
						case 3:
							strLOB ="Motorcycle";
							break;
						case 4:
							strLOB ="Watercraft";
							break;
						case 5:
							strLOB ="Umbrella";
							break;
						case 6:
							strLOB ="Rental";
							break;
						case 7:
							strLOB ="General Liability";
							break;
					}

					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='STATE_ID']");
					strTranXML = Cms.BusinessLayer.BlCommon.ClsCommon.RemoveNode(strTranXML,"LabelFieldMapping/Map[@field='SUB_LOB_ID']");											
					if(intOldSubLOB != intSubLOB && intOldSTATE != intSTATE)
					{
						objTransactionInfo.CUSTOM_INFO = "; State: From:- "  + strOldSTATE +  " To:- " + strSTATE +"<br>"+
							"; SubLOB: From:- "   + strOldSubLOB + " To:- " + strSubLOB;	
					}
					else if (intOldSubLOB != intSubLOB && intOldSTATE == intSTATE)
					{
						objTransactionInfo.CUSTOM_INFO = "; SubLOB: From:- "   + strOldSubLOB + " To:- " + strSubLOB;	
					}
					else if(intOldSubLOB == intSubLOB && intOldSTATE != intSTATE)
					{
						objTransactionInfo.CUSTOM_INFO = "; State: From:- "  + strOldSTATE +  " To:- " + strSTATE ;
					}
					else 
					{
						objTransactionInfo.CUSTOM_INFO = "; State:- "  + strOldSTATE +  "<br>"+
							"; LOB:- "  + strLOB +  "<br>"+
							"; Sub LOB:- "   + strOldSubLOB;
					}
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objMinimumPremAMt.CREATED_BY;
					objTransactionInfo.TRANS_DESC		=	"Minimum Premium Amount has been updated.";
					objTransactionInfo.CHANGE_XML		=	strTranXML;

					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				int ROW_ID=0;
				if (objMinimumPremAMt.ROW_ID == 0)
				{
					ROW_ID = int.Parse(objSqlParameter.Value.ToString());
					objDataWrapper.ClearParameteres();
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				}
				else
				{
					ROW_ID=objMinimumPremAMt.ROW_ID;
					objDataWrapper.ClearParameteres();
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				}
				if (ROW_ID == -1)
				{
					return -1;
				}
				else
				{
					objMinimumPremAMt.ROW_ID = ROW_ID;
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

		#region 
		public static string GetGLName()
		{
			string strSql = "Proc_GetGLName";
			object obj = DataWrapper.ExecuteScalar(ConnStr,CommandType.Text,strSql);
			if(obj==null || obj.ToString().Length<=0)
				return "";
			else
				return obj.ToString();
		}
		#endregion

		#region "Fill Drop down Functions"
		public static void GetFiscalYearsIndropDown(DropDownList cmbFiscalYearFrom, string selectedYear)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.Text);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("select FISCAL_BEGIN_DATE,FISCAL_ID from ACT_GENERAL_LEDGER").Tables[0];
			cmbFiscalYearFrom.Items.Clear();
			string year="";
			//added by uday to get current year
			string curyear = DateTime.Now.Year.ToString();
			//
			for(int i=0;i<objDataTable.DefaultView.Count;i++)			
			{
				year = DateTime.Parse(objDataTable.DefaultView[i]["FISCAL_BEGIN_DATE"].ToString()).Year.ToString();
				cmbFiscalYearFrom.Items.Add(new ListItem(year,objDataTable.DefaultView[i]["FISCAL_ID"].ToString()));				
				if(selectedYear!=null && selectedYear.Length>0 && selectedYear.Equals(year))
					cmbFiscalYearFrom.SelectedIndex = i;
				//added by uday
				else if(year == curyear)
					cmbFiscalYearFrom.SelectedIndex = i;
				//
			}
		}
		public static void GetFiscalYearsIndropDown(DropDownList cmbFiscalYearFrom)
		{
			GetFiscalYearsIndropDown(cmbFiscalYearFrom,null);

		}
		public static void GetGeneralLedgersIndropDown(DropDownList combo)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.Text);
            DataTable objDataTable = objDataWrapper.ExecuteDataSet("select FISCAL_ID,LEDGER_NAME +' ('+ convert(varchar,FISCAL_BEGIN_DATE,CASE WHEN " + ClsCommon.BL_LANG_ID + "=2 THEN 103 ELSE 103 END) + ' - '+ convert(varchar,FISCAL_END_DATE,CASE WHEN " + ClsCommon.BL_LANG_ID + "=2 THEN 103 ELSE 103 END)+')' as LEDGER_NAME from ACT_GENERAL_LEDGER  where LAST_EOY_RUN_DATE_TIME is null").Tables[0];
			combo.Items.Clear();
			combo.DataSource = objDataTable;
			combo.DataTextField = "LEDGER_NAME";
			combo.DataValueField = "FISCAL_ID";
			combo.DataBind();   
//			if(objDataTable.Rows.Count>0)
//     	  		return objDataTable.Rows[0]["FISCAL_END_DATE"].ToString();
//			else
//				return "";
		}
		
		
		public static void GetGeneralLedgerIndropDown(DropDownList combo)
		{
			string		strStoredProc	=	"Proc_GetGenralLadgerIndropDown";
			DataWrapper objDataWrapper  = new DataWrapper(ConnStr,CommandType.StoredProcedure );
			DataTable objDataTable      = objDataWrapper.ExecuteDataSet(strStoredProc).Tables[0];
			combo.Items.Clear();
			combo.DataSource = objDataTable;
			combo.DataTextField = "LEDGER_NAME";
			combo.DataValueField = "FISCAL";
			combo.DataBind();   
		}

		public static string GetGeneralLedgersEOYIndropDown(DropDownList combo)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.Text);
			DataSet ds = GetGeneralLedgersEOYRecords();
			combo.Items.Clear();
			combo.DataSource = ds.Tables[0];
			combo.DataTextField = "LEDGER_NAME";
			combo.DataValueField = "FISCAL_ID";
			combo.DataBind();  
			if(ds.Tables[0].Rows.Count>0)
      			return ds.Tables[0].Rows[0]["FISCAL_END_DATE"].ToString();	
			else
				return "";
		}
			
		public static DataSet GetGeneralLedgersEOYRecords()
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.Text);
			DataSet  objDataSet = objDataWrapper.ExecuteDataSet("select FISCAL_ID,LEDGER_NAME +' ('+ convert(varchar,FISCAL_BEGIN_DATE,101) + ' - '+ convert(varchar,FISCAL_END_DATE,101)+')' as LEDGER_NAME  ,convert(varchar,FISCAL_END_DATE,101)as FISCAL_END_DATE  from ACT_GENERAL_LEDGER where GETDATE() > FISCAL_END_DATE ");//where POSTING_LOCK_DATE is not null");
			return objDataSet;	
		}

		#endregion

		#region "Lock Posting dates"
		public static int SavePostingLockDate(int FISCAL_ID,DateTime POSTING_LOCK_DATE)
		{
			string strSql = "SavePostingLockDate_ACT_GENERAL_LEDGER";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@FISCAL_ID",FISCAL_ID);
			objDataWrapper.AddParameter("@POSTING_LOCK_DATE",POSTING_LOCK_DATE);
			return objDataWrapper.ExecuteNonQuery(strSql);
		}
		public static DataSet GetFiscalPeriod(int FISCAL_ID)
		{
			string strSql = "Proc_GetGLFiscalPeriod";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@FISCAL_ID",FISCAL_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet;
		}
		#endregion
	}
}

