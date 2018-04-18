/******************************************************************************************
<Author					: -   Ajit Singh Chahal
<Start Date				: -	  7/8/2005 12:47:00 PM
<End Date				: -	
<Description			: -   BL for Bank Reconciliation.
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
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Account;
using Cms.BusinessLayer.BlCommon;
using System.Collections;
namespace Cms.BusinessLayer.BlAccount
{
	/// <summary>
	/// ClsBankReconciliation
	/// </summary>
	public class ClsBankRconciliation : Cms.BusinessLayer.BlAccount.ClsAccount
	{
	
		private const	string		ACT_BANK_RECONCILIATION			=	"ACT_BANK_RECONCILIATION";

		#region Private Instance Variables
		private	bool boolTransactionLog;
		//private int _AC_RECONCILIATION_ID;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateBankReconciliation";
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
		public ClsBankRconciliation()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		public ClsBankRconciliation(bool transactionLogRequired):this()
		{
			base.TransactionLogRequired = transactionLogRequired;
		}
		#endregion

		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objBankReconciliationInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsBankRconciliationInfo objBankReconciliationInfo,string File)
		{
			string		strStoredProc	=	"Proc_InsertACT_BANK_RECONCILIATION";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try
			{
				objDataWrapper.AddParameter("@ACCOUNT_ID",objBankReconciliationInfo.ACCOUNT_ID);
				objDataWrapper.AddParameter("@DIV_ID",ConfigurationManager.AppSettings.Get("DIV_ID"));
                objDataWrapper.AddParameter("@DEPT_ID", ConfigurationManager.AppSettings.Get("DEPT_ID"));
                objDataWrapper.AddParameter("@PC_ID", ConfigurationManager.AppSettings.Get("PC_ID"));
				objDataWrapper.AddParameter("@STATEMENT_DATE",objBankReconciliationInfo.STATEMENT_DATE);
				objDataWrapper.AddParameter("@STARTING_BALANCE",objBankReconciliationInfo.STARTING_BALANCE);
				objDataWrapper.AddParameter("@ENDING_BALANCE",objBankReconciliationInfo.ENDING_BALANCE);
				objDataWrapper.AddParameter("@BANK_CHARGES_CREDITS",objBankReconciliationInfo.BANK_CHARGES_CREDITS);
				//is input on commit only
				objDataWrapper.AddParameter("@LAST_RECONCILED",DBNull.Value);
				objDataWrapper.AddParameter("@IS_ACTIVE",objBankReconciliationInfo.IS_ACTIVE);
				objDataWrapper.AddParameter("@CREATED_BY",objBankReconciliationInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objBankReconciliationInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",objBankReconciliationInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objBankReconciliationInfo.LAST_UPDATED_DATETIME);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@AC_RECONCILIATION_ID",objBankReconciliationInfo.AC_RECONCILIATION_ID,SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlRetParameter  = (SqlParameter) objDataWrapper.AddParameter("@RetVal",objBankReconciliationInfo.AC_RECONCILIATION_ID,SqlDbType.Int,ParameterDirection.ReturnValue);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objBankReconciliationInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/aspx/AddBankRecon.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objBankReconciliationInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objBankReconciliationInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1578", "");// "Record Has Been Added";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				if(objSqlRetParameter.Value.ToString().Equals("-2"))
				{
					return -2;
				}
				int AC_RECONCILIATION_ID = int.Parse(objSqlParameter.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				if(File!="")
				{
					//Set File Naming Conventions
					string[] fileArray;
					fileArray = File.Split('.');
                    File = fileArray[0].ToString() + "_" + AC_RECONCILIATION_ID.ToString() + "." + fileArray[1].ToString();

                    // Saving Uploaded File into respective table : ACT_BANK_RECON_UPLOAD_FILE
					DataWrapper objUploadFileWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
					objUploadFileWrapper.AddParameter("@FILE_NAME",File);
					objUploadFileWrapper.AddParameter("@FILE_DESC",null);
					objUploadFileWrapper.AddParameter("@AC_RECONCILIATION_ID",AC_RECONCILIATION_ID);
					objUploadFileWrapper.AddParameter("@CREATED_BY",objBankReconciliationInfo.CREATED_BY);
					objUploadFileWrapper.AddParameter("@CREATED_DATETIME",objBankReconciliationInfo.CREATED_DATETIME);
					int REF_FILE_ID = objUploadFileWrapper.ExecuteNonQuery("Proc_InsertACT_BANK_RECON_UPLOAD_FILE");
					if (AC_RECONCILIATION_ID == -1)
					{
						return -1;
					}
					else
					{
						objBankReconciliationInfo.AC_RECONCILIATION_ID = AC_RECONCILIATION_ID;
						objBankReconciliationInfo.REF_FILE_ID = REF_FILE_ID;
						return returnResult;
					}
				}
				else
				{
					objBankReconciliationInfo.AC_RECONCILIATION_ID = AC_RECONCILIATION_ID;
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
		/// <param name="objOldBankReconciliationInfo">Model object having old information</param>
		/// <param name="objBankReconciliationInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsBankRconciliationInfo objOldBankReconciliationInfo,ClsBankRconciliationInfo objBankReconciliationInfo,string File)
		{
			string		strStoredProc	=	"Proc_UpdateACT_BANK_RECONCILIATION";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@AC_RECONCILIATION_ID",objBankReconciliationInfo.AC_RECONCILIATION_ID);
				objDataWrapper.AddParameter("@ACCOUNT_ID",objBankReconciliationInfo.ACCOUNT_ID);

                objDataWrapper.AddParameter("@DIV_ID", ConfigurationManager.AppSettings.Get("DIV_ID"));
                objDataWrapper.AddParameter("@DEPT_ID", ConfigurationManager.AppSettings.Get("DEPT_ID"));
                objDataWrapper.AddParameter("@PC_ID", ConfigurationManager.AppSettings.Get("PC_ID"));
				

				objDataWrapper.AddParameter("@STATEMENT_DATE",objBankReconciliationInfo.STATEMENT_DATE);
				objDataWrapper.AddParameter("@STARTING_BALANCE",objBankReconciliationInfo.STARTING_BALANCE);
				objDataWrapper.AddParameter("@ENDING_BALANCE",objBankReconciliationInfo.ENDING_BALANCE);
				objDataWrapper.AddParameter("@BANK_CHARGES_CREDITS",objBankReconciliationInfo.BANK_CHARGES_CREDITS);
				
				objDataWrapper.AddParameter("@MODIFIED_BY",objBankReconciliationInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objBankReconciliationInfo.LAST_UPDATED_DATETIME);
				//objDataWrapper.AddParameter("@UPLOAD_FILE",File);
				SqlParameter objSqlRetParameter  = (SqlParameter) objDataWrapper.AddParameter("@RetVal",objBankReconciliationInfo.AC_RECONCILIATION_ID,SqlDbType.Int,ParameterDirection.ReturnValue);
				if(base.TransactionLogRequired) 
				{
					objBankReconciliationInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/aspx/AddBankRecon.aspx.resx");
					objBuilder.GetUpdateSQL(objOldBankReconciliationInfo,objBankReconciliationInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objBankReconciliationInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1579", "");// "Information Has Been Updated";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				if(objSqlRetParameter.Value.ToString().Equals("-2"))
				{
					return -2;
				}
				else
				{
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

					if(File!="")
					{

						// Saving Uploaded File into respective table : ACT_BANK_RECON_UPLOAD_FILE
						DataWrapper objUploadFileWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
						objUploadFileWrapper.AddParameter("@FILE_NAME",File);
						objUploadFileWrapper.AddParameter("@FILE_DESC",null);
						objUploadFileWrapper.AddParameter("@AC_RECONCILIATION_ID",objBankReconciliationInfo.AC_RECONCILIATION_ID);
						objUploadFileWrapper.AddParameter("@CREATED_BY",objBankReconciliationInfo.CREATED_BY);
						if(objBankReconciliationInfo.CREATED_DATETIME.Ticks != 0)
							objUploadFileWrapper.AddParameter("@CREATED_DATETIME",objBankReconciliationInfo.CREATED_DATETIME);
						else
							objUploadFileWrapper.AddParameter("@CREATED_DATETIME",null);
						objUploadFileWrapper.ExecuteNonQuery("Proc_InsertACT_BANK_RECON_UPLOAD_FILE");
						return returnResult;
					}
					else
					{
						return returnResult;
					}
				}
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
	
		#region "Delete"
		/// <summary>
		/// Delete the whole record of spefied id 
		/// </summary>
		/// <returns>Nos of rows deleted</returns>
		public int Delete(ClsBankRconciliationInfo objBankReconciliationInfo)
		{
			try
			{
				/*Calling the stored procedure to get the maximum Journal entry no*/
				String strStoredProc = "Proc_DelACT_BANK_RECONCILIATION";
				int Value;

				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
				objDataWrapper.AddParameter("@AC_RECONCILIATION_ID",objBankReconciliationInfo.AC_RECONCILIATION_ID);
				
				if(TransactionLogRequired)
				{
					objBankReconciliationInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/aspx/AddBankRecon.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objBankReconciliationInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	objBankReconciliationInfo.CREATED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1580", "");// "Bank Reconciliation Record has been deleted.";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					Value	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					Value	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
				
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();

				return Value ;
			}
			catch (Exception objEx)
			{
				throw(objEx);
			}
		}
		#endregion

		#region "GetxmlMethods"
		public static string GetXmlForPageControls(string AC_RECONCILIATION_ID)
		{
			string strSql = "Proc_GetXML_ACT_BANK_RECONCILIATION";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@AC_RECONCILIATION_ID",AC_RECONCILIATION_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.GetXml();
		}
		#endregion

		#region "Misc"
		//to fill items details in popup
		public static DataSet GetReconcileItemsDetails(string AC_RECONCILIATION_ID,string ACCOUNT_ID,bool CopyRecords,bool isAddMode)
		{
			string strSql = "Proc_Get_ACT_BANK_RECONCILIATION_ITEMS_DETAILS";
			SqlParameter[] param = new SqlParameter[4];
			param[0] = new SqlParameter("@ACCOUNT_ID",ACCOUNT_ID);
			param[1] = new SqlParameter("@AC_RECONCILIATION_ID",AC_RECONCILIATION_ID);
			if(CopyRecords)
				param[2] = new SqlParameter("@CopyRecords","Y");
			else
				param[2] = new SqlParameter("@CopyRecords","N");
			if(isAddMode)
				param[3] = new SqlParameter("@isAddMode","Y");
			else
				param[3] = new SqlParameter("@isAddMode","N");
			return DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,strSql,param);			 		
		}

		/// <summary>
		/// To get status of check.
		/// </summary>
		/// <param name="INVOICE_ID"></param>
		/// <returns>status of invoice </returns>
		/// N-Not distributed, 
		/// D-Distributed, 
		/// C-Committed
		public static string GetReconciliationStatus(string AC_RECONCILIATION_ID)
		{
			string strSql = "Proc_GetStatus_ACT_BANK_RECONCILIATION";
			SqlParameter[] param = new SqlParameter[1];
			param[0] = new SqlParameter("@AC_RECONCILIATION_ID",AC_RECONCILIATION_ID);
			string status=DataWrapper.ExecuteScalar(ConnStr,CommandType.StoredProcedure,strSql,param).ToString();
			return status;		
		}

		public static string GetPreviousEndingBalance(string ACCOUNT_ID)
		{
			string strSql = "Proc_GetPreviousEndingBalance";
			SqlParameter[] param = new SqlParameter[1];
			param[0] = new SqlParameter("@ACCOUNT_ID",ACCOUNT_ID);
			DataTable objDataTable = DataWrapper.ExecuteDataset(ConnStr,CommandType.StoredProcedure,strSql,param).Tables[0];
			if(objDataTable.Rows.Count<=0)
				return "";		
			else
				return objDataTable.Rows[0]["ENDING_BALANCE"].ToString();
		}

		public static int DeleteReconciliationItem(ArrayList IDENTITY_ROW_IDs)
		{
			int intRetval=1;
			DataWrapper objDataWrapper=null;
			try
			{
				string strSql = "Proc_Delete_ACT_BANK_RECONCILIATION_ITEMS_DETAILS";
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				for(int i=0;i<IDENTITY_ROW_IDs.Count;i++)
				{
					objDataWrapper.AddParameter("@IDENTITY_ROW_ID",IDENTITY_ROW_IDs[i].ToString());
					intRetval=objDataWrapper.ExecuteNonQuery(strSql);
					if(intRetval<=0)break;
					objDataWrapper.ClearParameteres();
				}
				if(intRetval>0)
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);			
				else
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);			
				return intRetval;
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
		public static int SaveReconciliationItem(ArrayList IDENTITY_ROW_IDs,string AC_RECONCILIATION_ID)
		{
			int intRetval=1;
			DataWrapper objDataWrapper=null;
			try
			{
				string strSql = "Proc_Save_ACT_BANK_RECONCILIATION_ITEMS_DETAILS";
				objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				for(int i=0;i<IDENTITY_ROW_IDs.Count;i++)
				{
					objDataWrapper.AddParameter("@IDENTITY_ROW_ID",IDENTITY_ROW_IDs[i].ToString());
					objDataWrapper.AddParameter("@AC_RECONCILIATION_ID",AC_RECONCILIATION_ID);
					intRetval=objDataWrapper.ExecuteNonQuery(strSql);
					if(intRetval<=0)break;
					objDataWrapper.ClearParameteres();
				}
				if(intRetval>0)
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);			
				else
					objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);			
				return intRetval;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			finally //http://localhost/cms/Web.config
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
		}

		public static DataSet GetReconDetails(int Account_ID,int Recon_ID,string Called_From)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, 
				DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
			string	strStoredProc =	"Proc_GetBankReconDetails";
			objWrapper.AddParameter("@AC_RECONCILIATION_ID", Recon_ID);
			objWrapper.AddParameter("@ACCOUNT_ID",Account_ID);
			objWrapper.AddParameter("@CALLED_FROM",Called_From);
			DataSet dsRec = objWrapper.ExecuteDataSet(strStoredProc);
			objWrapper.ClearParameteres();
			return dsRec;
		}
		// Insert Uploaded file details into ACT_BANK_RECON_CHECK_FILE
		public static DataSet AddBankReconUploadFileDetails(ClsBankRconciliationInfo objInfo)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);	
            string	strStoredProc =	"Proc_InsertACT_BANK_RECON_CHECK_FILE";
			try
			{
				objWrapper.AddParameter("@REF_FILE_ID",objInfo.REF_FILE_ID);
				objWrapper.AddParameter("@ACCOUNT_ID",objInfo.ACCOUNT_ID);
				objWrapper.AddParameter("@ACCOUNT_NUMBER",objInfo.ACCOUNT_NUMBER);
				objWrapper.AddParameter("@SERIAL_NUMBER",objInfo.SERIAL_NUMBER);
				objWrapper.AddParameter("@CHECK_DATE",objInfo.CHECK_DATE);
				objWrapper.AddParameter("@ADDITIONAL_DATA",objInfo.ADDITIONAL_DATA);
				objWrapper.AddParameter("@SEQUENCE_NUMBER",objInfo.SEQUENCE_NUMBER);
				objWrapper.AddParameter("@RECON_GROUP_ID",objInfo.RECON_GROUP_ID);
				objWrapper.AddParameter("@MATCHED_RECORD_STATUS",objInfo.MATCHED_RECORD_STATUS);
				objWrapper.AddParameter("@ERROR_DESC",objInfo.ERROR_DESC);
				objWrapper.AddParameter("@AMOUNT",objInfo.AMOUNT);
				objWrapper.AddParameter("@CREATED_BY",objInfo.CREATED_BY);
				objWrapper.AddParameter("@CREATED_DATETIME",objInfo.CREATED_DATETIME);
				DataSet dsRec = objWrapper.ExecuteDataSet(strStoredProc);
				objWrapper.ClearParameteres();
				return dsRec;
			}
			catch(Exception Ex)
			{
				throw(Ex);
			
			}
			
		}
		// Get Uploaded File Info
		public int GetReconUploadedFileID(int ReconID)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.Text);
			string strSQL = "SELECT [FILE_ID] FROM ACT_BANK_RECON_UPLOAD_FILE WHERE AC_RECONCILIATION_ID = " +ReconID;
			DataSet objDS = new DataSet();
			objDS = objWrapper.ExecuteDataSet(strSQL);
			int RetFileID = (int)(objDS.Tables[0].Rows[0]["FILE_ID"]);
			return RetFileID;
		}

		//Added By Raghav Gupta to fill a Dataset
		
		public static DataSet BankReconciliationInput(string ACCOUNT_ID , string YEAR)
		{
			string strSq = "Proc_BankReconHistory_Details";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@ACCOUNT_ID",ACCOUNT_ID);
			objDataWrapper.AddParameter("@year",YEAR);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSq);
			return objDataSet;		
		}
		
		public static DataSet BankreconciliationReport(string REF_FILE_ID , int MATCHED_RECORD_STATUS )
		{
			string strPrc ="Proc_Bank_Reconciliation_Report";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@REF_FILE_ID",REF_FILE_ID);
            objDataWrapper.AddParameter("@MATCHED_RECORD_STATUS",MATCHED_RECORD_STATUS); 
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strPrc);
			return objDataSet;

		}		

		#endregion

		#region "commit"
		public int Commit(ClsBankRconciliationInfo objBankReconciliationInfo)
		{
			string		strStoredProc	=	"Proc_CommitACT_BANK_RECONCILIATION";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@AC_RECONCILIATION_ID",objBankReconciliationInfo.AC_RECONCILIATION_ID);

                objDataWrapper.AddParameter("@DIV_ID", ConfigurationManager.AppSettings.Get("DIV_ID"));
                objDataWrapper.AddParameter("@DEPT_ID", ConfigurationManager.AppSettings.Get("DEPT_ID"));
                objDataWrapper.AddParameter("@PC_ID", ConfigurationManager.AppSettings.Get("PC_ID"));

				objDataWrapper.AddParameter("@IS_COMMITED",objBankReconciliationInfo.IS_COMMITED);
				objDataWrapper.AddParameter("@DATE_COMMITED",objBankReconciliationInfo.DATE_COMMITED);
				objDataWrapper.AddParameter("@COMMITTED_BY",objBankReconciliationInfo.COMMITTED_BY);
				objDataWrapper.AddParameter("@MODIFIED_BY",objBankReconciliationInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objBankReconciliationInfo.LAST_UPDATED_DATETIME);
				SqlParameter objSqlRetParameter  = (SqlParameter) objDataWrapper.AddParameter("@RetVal",objBankReconciliationInfo.AC_RECONCILIATION_ID,SqlDbType.Int,ParameterDirection.ReturnValue);

				if(base.TransactionLogRequired) 
				{
					objBankReconciliationInfo.TransactLabel = ClsCommon.MapTransactionLabel("/Account/aspx/AddBankRecon.aspx.resx");
																				 
																				 
					strTranXML = objBuilder.GetTransactionLogXML(objBankReconciliationInfo);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objBankReconciliationInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1581", "");// "Bank Reconciliation Has Been Committed";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				if(objSqlRetParameter.Value!=null && int.Parse(objSqlRetParameter.Value.ToString())<=0)
					return int.Parse(objSqlRetParameter.Value.ToString());
				else
				{
					objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
					return returnResult;
				}
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

		#region ReProcess Bank Recon Details
		public int ReProcessBankReconDetails(int accReconId,int accountId,int createdBy)
		{
			string		strStoredProc	=	"Proc_REPROCESS_BANK_RECONCILIATION_ITEMS_DETAILS";
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@AC_RECONCILIATION_ID",accReconId);
				objDataWrapper.AddParameter("@ACCOUNT_ID",accountId);
				
				if(base.TransactionLogRequired) 
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	createdBy;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1582", "");// "Reconciliation Reprocessed successfully.";
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



		
	}
	
}
