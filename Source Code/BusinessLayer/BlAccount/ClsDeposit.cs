/******************************************************************************************
<Author					: -   Vijay Joshi
<Start Date				: -	6/20/2005 2:35:07 PM
<End Date				: -	
<Description			: - 	Code behind files for 
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 30-Oct-2006
<Modified By			: - Mohit Agarwal
<Purpose				: - Added a paramter in GetArInfo for Stored Procedure DateTime param
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.Model.Account;
using System.Collections;
using PayPal.Payments.Common.Utility;
using PayPal.Payments.Communication;
using PayPal.Payments.DataObjects;
using Cms.BusinessLayer.BlCommon;
namespace Cms.BusinessLayer.BlAccount
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsDeposit : Cms.BusinessLayer.BlAccount.ClsAccount
	{
		private const	string		ACT_CURRENT_DEPOSITS			=	"ACT_CURRENT_DEPOSITS";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateACT_CURRENT_DEPOSITS";
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

		#region Constructors
		/// <summary>
		/// deafault constructor
		/// </summary>
		public ClsDeposit()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		#endregion
	
		#region Add(Insert) functions
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objDepositInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int Add(ClsDepositInfo objDepositInfo,DataWrapper objDataWrapper)
		{
			string		strStoredProc	=	"Proc_InsertACT_CURRENT_DEPOSITS";
			DateTime	RecordDate		=	DateTime.Now;
			try
			{
				objDataWrapper.AddParameter("@GL_ID",objDepositInfo.GL_ID);
				objDataWrapper.AddParameter("@FISCAL_ID",objDepositInfo.FISCAL_ID);
				objDataWrapper.AddParameter("@ACCOUNT_ID",objDepositInfo.ACCOUNT_ID);
				objDataWrapper.AddParameter("@DEPOSIT_NUMBER",objDepositInfo.DEPOSIT_NUMBER);
				objDataWrapper.AddParameter("@DEPOSIT_TRAN_DATE",objDepositInfo.DEPOSIT_TRAN_DATE);
				objDataWrapper.AddParameter("@TOTAL_DEPOSIT_AMOUNT",objDepositInfo.TOTAL_DEPOSIT_AMOUNT);
				objDataWrapper.AddParameter("@DEPOSIT_NOTE",objDepositInfo.DEPOSIT_NOTE);
				objDataWrapper.AddParameter("@IS_BNK_RECONCILED",objDepositInfo.IS_BNK_RECONCILED);
				objDataWrapper.AddParameter("@IN_BNK_RECON",objDepositInfo.IN_BNK_RECON);
                objDataWrapper.AddParameter("@DEPOSIT_TYPE", objDepositInfo.DEPOSIT_TYPE);
				if (IsEODProcess)
					objDataWrapper.AddParameter("@CREATED_BY",EODUserID);
				else
					objDataWrapper.AddParameter("@CREATED_BY",objDepositInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objDepositInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@RECEIPT_MODE",objDepositInfo.RECEIPT_MODE);
				objDataWrapper.AddParameter("@RTL_FILE",objDepositInfo.RTL_FILE);
				objDataWrapper.AddParameter("@ACCOUNT_BALANCE",objDepositInfo.ACCOUNT_BALANCE);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DEPOSIT_ID",objDepositInfo.DEPOSIT_ID,SqlDbType.Int,ParameterDirection.Output);

				int returnResult = 0;
				if(TransactionLog)
				{
					objDepositInfo.TransactLabel = BlCommon.ClsCommon.MapTransactionLabel("/account/aspx/AddDeposit.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objDepositInfo);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					if (IsEODProcess)
						objTransactionInfo.RECORDED_BY		=	EODUserID;
					else
						objTransactionInfo.RECORDED_BY		=	objDepositInfo.CREATED_BY;
					//Changed by swarup as itrack issue # 3273
					if(objDepositInfo.RECEIPT_MODE == (int)PaymentModes.CreditCard)
						objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1682","");//"Deposit (Credit Card) Information has been added.";
					else if(objDepositInfo.RECEIPT_MODE == (int)PaymentModes.Check)
						objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1683","");//"Deposit (Check) Information has been added.";
					else if(objDepositInfo.RECEIPT_MODE == (int)PaymentModes.EFT)
						objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1684","");//"Deposit (EFT-Sweep) Information has been added.";
					//Added For Itrack Issue #6568.
					else if(objDepositInfo.RECEIPT_MODE    ==  (int) PaymentModes.AlreadyProcesed)
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1685", "");// "Deposit (Already Processed) Information has been added.";
					//objTransactionInfo.TRANS_DESC		=	"Deposit Information has been added.";
					//objTransactionInfo.CUSTOM_INFO		=   
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				
				int DEPOSIT_ID = 0;
				if (returnResult > 0)
					DEPOSIT_ID = int.Parse(objSqlParameter.Value.ToString());

				
				objDataWrapper.ClearParameteres();
								
				if (DEPOSIT_ID == -1)
				{
					return -1;
				}
				else
				{
					objDepositInfo.DEPOSIT_ID = DEPOSIT_ID;
					return returnResult;
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			
		}

		public int Add(ClsDepositInfo objDepositInfo)
		{
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			int RetVal;
			try
			{
				RetVal= Add(objDepositInfo,objDataWrapper);
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
			finally
			{
				if(objDataWrapper != null) objDataWrapper.Dispose();
			}
			return RetVal;
		}
		#endregion

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldDepositInfo">Model object having old information</param>
		/// <param name="objDepositInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsDepositInfo objOldDepositInfo,ClsDepositInfo objDepositInfo)
		{
			string strStoredProc = "Proc_UpdateACT_CURRENT_DEPOSITS";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@DEPOSIT_ID",objDepositInfo.DEPOSIT_ID);
				objDataWrapper.AddParameter("@GL_ID",objDepositInfo.GL_ID);
				objDataWrapper.AddParameter("@FISCAL_ID",objDepositInfo.FISCAL_ID);
				objDataWrapper.AddParameter("@ACCOUNT_ID",objDepositInfo.ACCOUNT_ID);
				objDataWrapper.AddParameter("@DEPOSIT_NUMBER",objDepositInfo.DEPOSIT_NUMBER);
				objDataWrapper.AddParameter("@DEPOSIT_TRAN_DATE",objDepositInfo.DEPOSIT_TRAN_DATE);
				objDataWrapper.AddParameter("@TOTAL_DEPOSIT_AMOUNT",objDepositInfo.TOTAL_DEPOSIT_AMOUNT);
				objDataWrapper.AddParameter("@DEPOSIT_NOTE",objDepositInfo.DEPOSIT_NOTE);

				objDataWrapper.AddParameter("@IS_BNK_RECONCILED",objDepositInfo.IS_BNK_RECONCILED);
				objDataWrapper.AddParameter("@IN_BNK_RECON",objDepositInfo.IN_BNK_RECON);
                objDataWrapper.AddParameter("@DEPOSIT_TYPE",objDepositInfo.DEPOSIT_TYPE);
				objDataWrapper.AddParameter("@MODIFIED_BY",objDepositInfo.MODIFIED_BY);
				//objDataWrapper.AddParameter("@CREATED_DATETIME",objDepositInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objDepositInfo.LAST_UPDATED_DATETIME);
				objDataWrapper.AddParameter("@RECEIPT_MODE",objDepositInfo.RECEIPT_MODE);
				objDataWrapper.AddParameter("@RTL_FILE",objDepositInfo.RTL_FILE);
				//Uncommented By Raghav For ITrack #5105 
				objDataWrapper.AddParameter("@ACCOUNT_BALANCE",objDepositInfo.ACCOUNT_BALANCE);

				if(TransactionLogRequired)
				{
					objDepositInfo.TransactLabel =BlCommon.ClsCommon.MapTransactionLabel("/account/aspx/AddDeposit.aspx.resx");
					//string strUpdate = objBuilder.GetUpdateSQL(objOldDepositInfo,objDepositInfo,out strTranXML);
					objBuilder.GetUpdateSQL(objOldDepositInfo,objDepositInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					if (IsEODProcess)
						objTransactionInfo.RECORDED_BY		=	EODUserID;
					else
						objTransactionInfo.RECORDED_BY		=	objDepositInfo.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC       = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1686", "");// "Deposit Information has been modified.";
					objTransactionInfo.CUSTOM_INFO		=   "Deposit Number :" + objDepositInfo.DEPOSIT_NUMBER ;
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				objDataWrapper.ClearParameteres();
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

		#region GetDepositInfo
		/// <summary>
		/// Returns the data in the form of XML of specified deposit id
		/// </summary>
		/// <param name="intDepositId">Deposit id whose data will be returned</param>
		/// <returns>XML of data</returns>
		public static string GetDepositInfo(int intDepositId )
		{
			String strStoredProc = "Proc_GetACT_CURRENT_DEPOSITS";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@DEPOSIT_ID",intDepositId);
                objDataWrapper.AddParameter("@LANG_ID",ClsCommon.BL_LANG_ID);
				
				ds = objDataWrapper.ExecuteDataSet(strStoredProc);
				
				if (ds.Tables[0].Rows.Count != 0)
				{
					return ds.GetXml();
				}
				else
				{
					return "";
				}
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
		
		#region Commit
		/// <summary>
		/// Commits the specifed deposit entry
		/// </summary>
		/// <param name="Deposit">Deposit id to commit</param>
		/// <returns>True if sucessfull else false</returns>
		
		public int Commit(int DepositId, int CommitedBy)
		{
			try
			{
				/*Calling the stored procedure to get the maximum Deposit entry no*/
				int Value;
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				Value = Commit(DepositId,CommitedBy,objDataWrapper);
				objDataWrapper.Dispose();
				return Value;
			}
			catch (Exception objEx)
			{
				throw(objEx);
			}
		}
		#region Commit to Spool
		/// <summary>
		/// Commented on 31 August 2009 : Praveen kasana
		/// </summary>
		/// <param name="DepositId"></param>
		/// <param name="ModifiedBy"></param>
		/// <returns></returns>
		/*public int CommitToSpool(int DepositId, int ModifiedBy)
		{
			try
			{
				//Calling the stored procedure to get the maximum Deposit entry no
				int Value;
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				Value = CommitToSpool(DepositId,ModifiedBy,objDataWrapper,0);
				objDataWrapper.Dispose();
				return Value;
			}
			catch (Exception objEx)
			{
				throw(objEx);
			}
		}*/
		/// <summary>
		/// 
		/// </summary>
		/// <param name="DepositId"></param>
		/// <param name="ModifiedBy"></param>
		/// <returns></returns>
		public int CommitToSpool(int DepositId, int ModifiedBy)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			try
			{
				string strDepositNO = GetDepositNumber(DepositId , objDataWrapper);
				string		strStoredProc	=	"Proc_CommitToSpoolACT_CURRENT_DEPOSITS";
				int returnResult = 0;
				objDataWrapper.AddParameter("@DEPOSIT_ID", DepositId);
				objDataWrapper.AddParameter("@DATE_COMMITED_TO_SPOOL", DateTime.Now);
				objDataWrapper.AddParameter("@MODIFIED_BY", ModifiedBy);
				
				SqlParameter objParam = (SqlParameter)objDataWrapper.AddParameter("@RetVal", SqlDbType.Int, ParameterDirection.ReturnValue);
				if(base.TransactionLogRequired) 
				{
					ClsDepositInfo objDepositInfo  = new ClsDepositInfo();
					objDepositInfo.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel("Account/Aspx/AddDeposit.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID		=	1;
					objTransactionInfo.RECORDED_BY			=	ModifiedBy;

					//Commented on 31 AUgust 2009
					/*if(Receipt_Mode == (int)PaymentModes.CreditCard)
						objTransactionInfo.TRANS_DESC		=	"Deposit (Credit Card) committed to Spool successfully.";
					else if(Receipt_Mode == (int)PaymentModes.Check)
						objTransactionInfo.TRANS_DESC		=	"Deposit (Check) committed to Spool successfully.";
					else if(Receipt_Mode == (int)PaymentModes.EFT)
						objTransactionInfo.TRANS_DESC		=	"Deposit (EFT-Sweep) committed to Spool successfully.";
					else
						objTransactionInfo.TRANS_DESC		=	"Deposit committed to Spool successfully.";*/

                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1675", "");// "Deposit committed to Spool successfully.";
                    objTransactionInfo.CUSTOM_INFO			=	"Deposit Number : " + strDepositNO;

					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				if(objParam.Value!=null && int.Parse(objParam.Value.ToString())<=0)
					return int.Parse(objParam.Value.ToString());
				else if(returnResult > 0)
				{
					returnResult = 1;
					//If Commit is Successful then Insert Diary Entry for Cancelled Policies:
					SendCancelledPolicyDiaryReminder(DepositId,ModifiedBy,objDataWrapper);
					return returnResult;
				}
				else
					return returnResult;
					
			}
			catch (Exception objEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
				throw(objEx);
			}
		}

		#endregion
		//Ravindra(09-13-2007) As this function is called in between a transaction same data warrper to be 
		//used may create locking problem other wise . Added ClearParameters() 
		//This same function is defined in ClsDepositDetails too , removed that and made this static
		
		/// <summary>
		/// Get Deposit Number
		/// </summary>
		/// <param name="depositId"></param>
		/// <returns></returns>
		public static string GetDepositNumber(int depositId, DataWrapper objWrapper)
		{
			string	strStoredProc =	"Proc_FetchDepositNumberOnId";
			string strDepNo = "";
			//DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
			try
			{
				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@DEPOSIT_ID",depositId);
				DataSet dsTemp = objWrapper.ExecuteDataSet(strStoredProc);	
				objWrapper.ClearParameteres();
				if (dsTemp.Tables[0].Rows.Count > 0)
				{
                    strDepNo = dsTemp.Tables[0].Rows[0]["DEPOSIT_NUMBER"].ToString();
				}

				return strDepNo;

			}
			catch(Exception ex)
			{
				throw(ex);
			}
					
		}
		#region Fetch Customer ID,Policy ID, Policy Version ID for Payment Received on Policy
		/// <summary>
		/// 
		/// </summary>
		/// <param name="depositId"></param>
		/// <param name="depositType"></param>
		/// <param name="policyNo"></param>
		/// <param name="objWrapper"></param>
		/// <returns></returns>
		public static DataSet GetDepositNumber(int depositId,string depositType,string policyNo, DataWrapper objWrapper)
		{
			string	strStoredProc =	"Proc_FetchDepositNumberOnId";
			try
			{
				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@DEPOSIT_ID",depositId);
				objWrapper.AddParameter("@DEPOSIT_TYPE",depositType);
				objWrapper.AddParameter("@POLICY_NUMBER",policyNo);
				DataSet dsTemp = objWrapper.ExecuteDataSet(strStoredProc);	
				objWrapper.ClearParameteres();
				return dsTemp;
				
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		
		}
		#endregion
		public int Commit(int DepositId, int CommitedBy,DataWrapper objDataWrapper)
		{
			return Commit(DepositId,CommitedBy,objDataWrapper,0);
		}
		public int Commit(int DepositId, int CommitedBy,DataWrapper objDataWrapper,int Receipt_Mode)
		{
			try
			{
				string strDepositNO = GetDepositNumber(DepositId , objDataWrapper);
                //string strStoredProc = "Proc_CommitACT_CURRENT_DEPOSITS";//Commented on 03-12-2010
                string strStoredProc = "Proc_Commit_Deposit";//Newly Added SP by Anurag on (03-12-2010)
				int returnResult = 0;
				objDataWrapper.AddParameter("@DEPOSIT_ID", DepositId);
				objDataWrapper.AddParameter("@DATE_COMMITED", DateTime.Now);
				objDataWrapper.AddParameter("@COMMITTED_BY", CommitedBy);
				objDataWrapper.AddParameter("@PARAM1", null);
				objDataWrapper.AddParameter("@PARAM2", null);
				objDataWrapper.AddParameter("@PARAM3", null);
				objDataWrapper.AddParameter("@PARAM4", null);
				SqlParameter objParam = (SqlParameter)objDataWrapper.AddParameter("@RetVal", SqlDbType.Int, ParameterDirection.ReturnValue);
				if(base.TransactionLogRequired) 
				{
					ClsDepositInfo objDepositInfo  = new ClsDepositInfo();
					objDepositInfo.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel("Account/Aspx/AddDeposit.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					if (IsEODProcess)
						objTransactionInfo.RECORDED_BY		=	EODUserID;
					else
						objTransactionInfo.RECORDED_BY		=	CommitedBy;
					if(Receipt_Mode == (int)PaymentModes.CreditCard)
						objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1676","");//"Deposit (Credit Card) committed successfully.";
					else if(Receipt_Mode == (int)PaymentModes.Check)
						objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1677","");//"Deposit (Check) committed successfully.";
					else if(Receipt_Mode == (int)PaymentModes.EFT)
						objTransactionInfo.TRANS_DESC		=	Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1678","");//"Deposit (EFT-Sweep) committed successfully.";
					else
                        objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1679", "");// "Deposit committed successfully.";
					objTransactionInfo.CUSTOM_INFO		=	"Deposit Number : " + strDepositNO;

					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				}

				if(objParam.Value!=null && int.Parse(objParam.Value.ToString())<=0)
					return int.Parse(objParam.Value.ToString());
				else if(returnResult > 0)
				{
					returnResult = 1;
					//If Commit is Successful then Insert Diary Entry for Cancelled Policies:
					SendCancelledPolicyDiaryReminder(DepositId,CommitedBy,objDataWrapper);
					return returnResult;
				}
				else
					return returnResult;
					
			}
			catch (Exception objEx)
			{
				throw(objEx);
			}
		}
		#endregion

		#region Cancelled policy for Diary from Deposit Commit : Added on 25 feb 2008  : Kasana
		public DataSet GetCancelledPolicyForDiaryReminder(int intDepositId,DataWrapper objDataWrapper)
		{
			
			try
			{
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@DEPOSIT_ID", intDepositId);
				DataSet dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetCancelledPolicyForDiaryReminder");
				objDataWrapper.ClearParameteres();
				return dsTemp;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}

		#endregion
		#region Diary Entry fro Cancelled Policieswhile Commiting a Deposit : Added on 25 feb 2008  : Kasana
		public void SendCancelledPolicyDiaryReminder(int intdepositId,int CommitedBy ,DataWrapper objDataWrapper)
		{
			try
			{

				DataSet dsTemp = GetCancelledPolicyForDiaryReminder(intdepositId,objDataWrapper);
				int CustomerID = 0, PolicyID = 0, PolicyVersionID = 0;
				string strPolicyNumber = "";
				
			
				if (dsTemp.Tables[0].Rows.Count > 0)
				{
					for (int count = 0; count < dsTemp.Tables[0].Rows.Count;count++)
					{
						CustomerID = Convert.ToInt32(dsTemp.Tables[0].Rows[count]["CUSTOMER_ID"].ToString());
						PolicyID = Convert.ToInt32(dsTemp.Tables[0].Rows[count]["POLICY_ID"].ToString());
						PolicyVersionID = Convert.ToInt32(dsTemp.Tables[0].Rows[count]["POLICY_VERSION_ID"].ToString());
						strPolicyNumber = dsTemp.Tables[0].Rows[count]["POLICY_NUMBER"].ToString();

						//Model for Diary
						Cms.Model.Diary.TodolistInfo objToDoListInfo = new Cms.Model.Diary.TodolistInfo();
						objToDoListInfo.CUSTOMER_ID = CustomerID;
						objToDoListInfo.POLICY_ID = PolicyID;
						objToDoListInfo.POLICY_VERSION_ID = PolicyVersionID;
						objToDoListInfo.POLICY_NUMBER = strPolicyNumber;
						objToDoListInfo.LISTTYPEID =(int)Cms.BusinessLayer.BlCommon.ClsDiary.enumDiaryType.DEPOSIT_ON_CANCELLED_POLICY;  
						objToDoListInfo.RECDATE = System.DateTime.Now;
						objToDoListInfo.MODULE_ID=(int)Cms.BusinessLayer.BlCommon.ClsDiary.enumModuleMaster.POLICY_PROCESS;  
						objToDoListInfo.LISTOPEN ="Y";
						objToDoListInfo.FOLLOWUPDATE= System.DateTime.Now;
						objToDoListInfo.SUBJECTLINE = "Deposit on Cancelled Policy.";
						objToDoListInfo.CREATED_BY = CommitedBy;
					
						//Set up Dairy
						Cms.BusinessLayer.BlCommon.ClsDiary objDiary = new Cms.BusinessLayer.BlCommon.ClsDiary();
						objDiary.DiaryEntryfromSetup(objToDoListInfo);
					
						
					}
				}
			}
			catch (Exception objEx)
			{
				throw(objEx);
			}
		

		}
		#endregion

		#region Delete

		//Ravindra(09-12-2007) Created a Overload which accept Datawrapper as parameter
		public int Delete(int intDepositId,ClsDepositInfo objDepositInfo ,int CREATED_BY , DataWrapper objDataWrapper)
		{
			try
			{
				String strStoredProc = "Proc_DeleteACT_CURRENT_DEPOSITS";
				int Value;
				objDataWrapper.ClearParameteres();			
				objDataWrapper.AddParameter("@DEPOSIT_ID", intDepositId);
				SqlParameter objRetVal = (SqlParameter) objDataWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);
				if(TransactionLogRequired) 
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					if (IsEODProcess)
						objTransactionInfo.RECORDED_BY		=	EODUserID;
					else
						objTransactionInfo.RECORDED_BY		= CREATED_BY ;
					objTransactionInfo.TRANS_TYPE_ID	=	3;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1680", "");// "Deposit Information has been deleted.";
					objTransactionInfo.CUSTOM_INFO		=	"Deposit Number : " + objDepositInfo.DEPOSIT_NUMBER ;//intDepositId.ToString();
					Value = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
				}
				else
					Value = objDataWrapper.ExecuteNonQuery(strStoredProc);

				Value = int.Parse(objRetVal.Value.ToString());
				objDataWrapper.ClearParameteres();
				return Value ;
			}
			catch (Exception objEx)
			{
				throw(objEx);
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="intDepositId"></param>
		/// <param name="objDepositInfo"></param>
		/// <param name="CREATED_BY"></param>
		/// <returns></returns>
		public int Delete(int intDepositId,ClsDepositInfo objDepositInfo ,int CREATED_BY)
		{
			try
			{
				/*Calling the stored procedure to get the maximum Deposit entry no*/
				//String strStoredProc = "Proc_DeleteACT_CURRENT_DEPOSITS";
				int Value;

				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
				
				Value = Delete(intDepositId,objDepositInfo,CREATED_BY,objDataWrapper);

				objDataWrapper.Dispose();

				return Value ;
			}
			catch (Exception objEx)
			{
				throw(objEx);
			}
		}
		/// <summary>
		/// Delete the whole record of specified id 
		/// </summary>
		/// <returns>Nos of rows deleted</returns>
		/// 
		/*public int Delete(int intDepositId,ClsDepositInfo objDepositInfo)
		{
			try
			{
				String strStoredProc = "Proc_DeleteACT_CURRENT_DEPOSITS";
				int Value;

				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
				objDataWrapper.AddParameter("@DEPOSIT_ID", intDepositId);
				SqlParameter objRetVal = (SqlParameter) objDataWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);
				if(TransactionLogRequired) 
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					//objTransactionInfo.RECORDED_BY		= CREATED_BY ;
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.TRANS_DESC		=	"Deposit Information has been deleted.";
					objTransactionInfo.CUSTOM_INFO		=	"Deposit Number : " + objDepositInfo.DEPOSIT_NUMBER ;//intDepositId.ToString();
					Value = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
				}
				else
					Value = objDataWrapper.ExecuteNonQuery(strStoredProc);

				Value = int.Parse(objRetVal.Value.ToString());
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();

				return Value ;
			}
			catch (Exception objEx)
			{
				throw(objEx);
			}
		}*/
		#endregion

		#region Copy
		/// <summary>
		/// Copy the whole record of specified id and return the new DepositId
		/// </summary>
		/// <returns></returns>
		public int Copy(int DepositId)
		{
			try
			{
				String strStoredProc = "Proc_CopyACT_CURRENT_DEPOSITS";
				int Value;

				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
				objDataWrapper.AddParameter("@DEPOSIT_ID", DepositId);
				SqlParameter objSqlParameter = (SqlParameter) objDataWrapper.AddParameter("@NEW_DEPOSIT_ID", SqlDbType.Int, ParameterDirection.Output);

				if (objDataWrapper.ExecuteNonQuery(strStoredProc) > 0)
				{
					Value = int.Parse(objSqlParameter.Value.ToString());
				}
				else
				{
					Value = -1;
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

		#region GetGLAccountXML
		/// <summary>
		/// Retreivs the General ledger info of GL of the specfied Deposit
		/// </summary>
		/// <param name="Deposit">Deposit id whose ledger's info will be fetched</param>
		/// <returns>XML of general ledger</returns>
		public static string GetGLAccountXML(int DepositID)
		{
			try
			{
				//For retreiving the general kedger id , first retreivin the Deposit information
				string strXML = GetDepositInfo(DepositID);
				
				//Loading the xml returned by above function
				System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
				doc.LoadXml(strXML);
				
				//Retreiving the general ledger
				System.Xml.XmlNode nodeGl = doc.SelectSingleNode("/NewDataSet/Table/GL_ID");
				if ( nodeGl != null)
				{
					//Retreiving the fiscal id
					System.Xml.XmlNode nodeFiscal = doc.SelectSingleNode("/NewDataSet/Table/FISCAL_ID");
					if (nodeFiscal != null)
					{
						//Retreiving the xml from gl account table
						string strGL = nodeGl.InnerXml;
						string strRetVal = "";

						DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
						objDataWrapper.AddParameter("@GL_ID", nodeGl.InnerText);
						objDataWrapper.AddParameter("@FISCAL_ID", nodeFiscal.InnerText);

						DataSet ds = objDataWrapper.ExecuteDataSet("Proc_GetACT_GENERAL_LEDGER_POSTING_INFO");

						if (ds.Tables[0].Rows.Count > 0)
						{
							strRetVal = ds.GetXml();
						}

						return strRetVal;
					}
				}
			
				return "";
			}
			catch (Exception objExp)
			{
				throw(objExp);
			}
		}
		#endregion

		#region ChildRecordExists
		/// <summary>
		/// Checks whther child records of deposit exists or not
		/// </summary>
		/// <param name="DepositId">Deposit id whose child record will be searched</param>
		/// <returns>True if child records exists else false</returns>
		public bool ChildRecordExists(int DepositId)
		{
			try
			{
				/*Calling the stored procedure to check*/
				String strSQL = "Proc_ExistsACT_CURRENT_DEPOSIT_LINE_ITEMS";
				bool Value;

				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
				objDataWrapper.AddParameter("@DEPOSIT_ID", DepositId);
				SqlParameter objSqlParameter = (SqlParameter) objDataWrapper.AddParameter("@EXISTS", SqlDbType.Bit, ParameterDirection.Output);

				objDataWrapper.ExecuteNonQuery(strSQL);
				
				Value = (bool)objSqlParameter.Value;
				
				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
				
				return Value;
				
			}
			catch (Exception objEx)
			{
				throw(objEx);
			}
		}
		#endregion

		#region UpdateTapeTotal
		/// <summary>
		/// Updates the tape total field of the specified deposit
		/// </summary>
		internal static void UpdateTapeTotal(DataWrapper objWrapper, int intDepositId, double dblTapeTotal)
		{
			try
			{
				String strStoredProc = "Proc_UpdateACT_CURRENT_DEPOSITS_TAPETOTAL";

				objWrapper.AddParameter("@DEPOSIT_ID", intDepositId);
				objWrapper.AddParameter("@TAPE_TOTAL", dblTapeTotal);
				objWrapper.ExecuteNonQuery(strStoredProc);
				objWrapper.ClearParameteres();
			}
			catch (Exception objEx)
			{
				throw(objEx);
			}	
		}
		#endregion

		#region Confirm
		/// <summary>
		/// Confirms the any specified deposit
		/// </summary>
		/// <param name="Deposit_id">Deposit Id which should be confirmed</param>
		public void Confirm(int Deposit_id)
		{
			DataWrapper objWrapper;
			objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
				
			try
			{
				Confirm(objWrapper, Deposit_id);
			}
			catch(Exception objExp)
			{
				throw(objExp);
			}
			finally
			{
				objWrapper.Dispose();
			}
		}

		/// <summary>
		/// Confirms the any specified deposit
		/// </summary>
		/// <param name="objWrapper">Data wrapper object, which will be used for calling the proc</param>
		/// <param name="Deposit_id">Deposit Id which should be confirmed</param>
		internal void Confirm(DataWrapper objWrapper, int Deposit_id)
		{
			try
			{
				String strStoredProc = "Proc_ConfirmACT_CURRENT_DEPOSITS";

				objWrapper.AddParameter("@DEPOSIT_ID", Deposit_id);
				objWrapper.ExecuteNonQuery(strStoredProc);
				objWrapper.ClearParameteres();
			}
			catch(Exception objExp)
			{
				throw(objExp);
			}
		}
		#endregion

		#region "GetcustomerBalance"
		/// <summary>
		/// Returns details of customer balance 
		/// </summary>
		/// <param name="CustomerId">Customer identification number</param>
		/// <returns>Datatable of customer balances information.</returns>
		public DataSet GetCustomerBalance(int CustomerId , int pageSize, int currentPageIndex, string PolicyNumber)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			DataSet dtBalance;
			try
			{
				objWrapper = new Cms.DataLayer.DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, System.Data.CommandType.StoredProcedure);
				objWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
				objWrapper.AddParameter("@FROM",currentPageIndex);
				objWrapper.AddParameter("@TO",pageSize);
				objWrapper.AddParameter("@POLICY_NUMBER",PolicyNumber);
				dtBalance = objWrapper.ExecuteDataSet("Proc_GetCustomerBalances");
			}
			catch (Exception objExp)
			{
				throw(new Exception("Error occured in GetCustomerBalance function.\n" + objExp.Message.ToString()));
			}
			finally
			{
				objWrapper.Dispose();
			}
			return dtBalance;
		}
		#endregion

		#region "GetArInfo"
		/// <summary>
		/// Returns details of customer balance 
		/// </summary>
		/// <param name="CustomerId">Customer identification number</param>
		/// <returns>Datatable of customer balances information.</returns>
		public DataSet GetArInfo(int CustomerId,int Policy_Id,string strPolNumber,DateTime PastTrans,string Agency_Code,string LangId)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			DataSet dtBalance;
			try
			{
				objWrapper = new Cms.DataLayer.DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, System.Data.CommandType.StoredProcedure);
				objWrapper.AddParameter("@CUSTOMER_ID",CustomerId);
				objWrapper.AddParameter("@POLICY_ID",Policy_Id);
				objWrapper.AddParameter("@POLICY_NUMBER",strPolNumber);
				objWrapper.AddParameter("@PAST_TRANS_DATE",PastTrans);
				objWrapper.AddParameter("@Agency_Code",Agency_Code);
                objWrapper.AddParameter("@LANG_ID", LangId);
				//dtBalance = objWrapper.ExecuteDataSet("PROC_AR_REPORT");
				//dtBalance = objWrapper.ExecuteDataSet("Proc_AccountEnquiry");
                dtBalance = objWrapper.ExecuteDataSet("Proc_AccountEnquiry_New"); // added new proc by Pravesh on 16 July 2010
			}
			catch (Exception objExp)
			{
				throw(new Exception("Error occured in GetArInfo function.\n" + objExp.Message.ToString()));
			}
			finally
			{
				objWrapper.Dispose();
			}
			return dtBalance;
		}

		public DataSet GetAdjustmentDetails(int OpenItemID,int Policy_Id, string CalledFrom)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			DataSet dsDetails;
			try
			{
				objWrapper = new Cms.DataLayer.DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, System.Data.CommandType.StoredProcedure);
				objWrapper.AddParameter("@OPEN_ITEM_ID",OpenItemID);
				objWrapper.AddParameter("@POLICY_ID",Policy_Id);
				objWrapper.AddParameter("@CALLED_FROM",CalledFrom);
				dsDetails= objWrapper.ExecuteDataSet("Proc_GetDepositAdjustmentDetails");
			}
			catch 
			{
				throw(new Exception("Error occured while fetching adjustment details."));
			}
			finally
			{
				objWrapper.Dispose();
			}
			return dsDetails;
		}
		#endregion
		#region Agency Code for Agency
		public DataSet GetAgencyCodeForAgency(string policyNo,string agencyCombinedCode)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			DataSet dsAgency;
			try
			{
				objWrapper = new Cms.DataLayer.DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, System.Data.CommandType.StoredProcedure);
				objWrapper.AddParameter("@POLICY_NUMBER",policyNo);
				objWrapper.AddParameter("@AGENCY_COMBINED_CODE",agencyCombinedCode);
				dsAgency = objWrapper.ExecuteDataSet("Proc_GetAgencyCodeForAgency");
			}
			catch (Exception objExp)
			{
				throw(new Exception("Error occured in GetAgencyCodeForAgency function.\n" + objExp.Message.ToString()));
			}
			finally
			{
				objWrapper.Dispose();
			}
			return dsAgency;


		}

		#endregion

		#region Get Payment Records
		//Added by Sibin for Itrack Issue 4914 on 9 Feb 09
		public static DataSet GetPaymentDetail(string policyNo)
		{
			string strSql = "Proc_FetchPaymentInformations";
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			if(policyNo!=null && policyNo.Length<=0)
				objDataWrapper.AddParameter("@POLICYNO",DBNull.Value);
			else
				objDataWrapper.AddParameter("@POLICYNO",policyNo);
			
			
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			string s = objDataSet.GetXml();
			return objDataSet;
		}
		#endregion

		#region Payment Reversal
		//sibin
		//Changed by Shikha against #4914.
		public int ReversePayment(string PolNum,int customerID,int lineITEM_ID,int PolicyId, int PolicyVerId, int CreatedBy,int DepositNo,double RecieptAmount,int UserId,bool flag,string PayPalUser , string PayPalVendor , string PayPalHost ,string PayPalPartner , string PayPalPassword )
		{
			string paymentProcName = "Proc_ReversePayment";	
			int result =0; int Payment_mode = 0 ; string Reference_Id = ""; int Entity_Id = 0;
			string Entity_type = "";
			int ReturnCode = 0 ; string sweep = ""; int Date_Flag = 0;
			PayPalResponse objPayPalResponse = new PayPalResponse();
			if (flag == true)
			{
				sweep = "Yes";
			}
			else
			{
				sweep ="No";
			}

			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				//string paymentProcName = "Proc_ReversePayment";	

				//objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@POLICYNO",PolNum);
				objDataWrapper.AddParameter("@CUSTOMER_ID",customerID,SqlDbType.Int);
				objDataWrapper.AddParameter("@CD_LINE_ITEM_ID",lineITEM_ID,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@USER_ID",UserId,SqlDbType.Int);

				SqlParameter objSqlParameter1 = (SqlParameter) objDataWrapper.AddParameter("@RESULT",null,SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlParameter2 = (SqlParameter) objDataWrapper.AddParameter("@PAYMENT_MODE",SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlParameter3 = (SqlParameter) objDataWrapper.AddParameter("@REFERENCE_ID",null,SqlDbType.VarChar,ParameterDirection.Output,50);
				SqlParameter objSqlParameter4 = (SqlParameter) objDataWrapper.AddParameter("@ENTITY_ID",null,SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlParameter5 = (SqlParameter) objDataWrapper.AddParameter("@ENTITY_TYPE",null,SqlDbType.VarChar,ParameterDirection.Output,20);
				SqlParameter objSqlParameter6 = (SqlParameter) objDataWrapper.AddParameter("@DATE_FLAG",null,SqlDbType.Int,ParameterDirection.Output);
				
				string strTranXML = "Deposit Number : " + DepositNo +"<BR>"	+ "Amount Reversed : $" 
									+ String.Format("{0:n}",RecieptAmount) +"<BR>" + "Return Funds : " + sweep;
				if(TransactionLogRequired)
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	CreatedBy;
					objTransactionInfo.CLIENT_ID		=	customerID;
					objTransactionInfo.POLICY_ID		=	PolicyId;
					objTransactionInfo.POLICY_VER_TRACKING_ID = PolicyVerId;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1681", "");// "Payment Reversed successfully.";
					objTransactionInfo.CUSTOM_INFO		=	strTranXML;

					result	= objDataWrapper.ExecuteNonQuery(paymentProcName,objTransactionInfo);

					if(objSqlParameter2 != null && objSqlParameter2.Value != DBNull.Value )
					{
						Payment_mode = Convert.ToInt32(objSqlParameter2.Value);
					}
					if(objSqlParameter3 != null && objSqlParameter3.Value != DBNull.Value )
					{
						Reference_Id = objSqlParameter3.Value.ToString();
					}
					if(objSqlParameter4 != null && objSqlParameter4.Value != DBNull.Value)
					{
						Entity_Id = Convert.ToInt32(objSqlParameter4.Value);
					}
					if(objSqlParameter5 != null && objSqlParameter5.Value != DBNull.Value)
					{
						Entity_type = objSqlParameter5.Value.ToString();
					}
					if(objSqlParameter6 != null && objSqlParameter6.Value != DBNull.Value)
					{
						Date_Flag = Convert.ToInt32(objSqlParameter6.Value);
					}
					
				}
				else
				{
					result	= objDataWrapper.ExecuteNonQuery(paymentProcName);
				}
				
				objDataWrapper.ClearParameteres();
				if (result > 0)
				{
					if (flag == true)
					{
						if (Payment_mode == (int)PaymentModes.CreditCard)
						{
							ClsCreditCard objCreditCardBL = new ClsCreditCard(); 
							objCreditCardBL.PayPalAPI.UserName    = PayPalUser;
							objCreditCardBL.PayPalAPI.VendorName  = PayPalVendor;
							objCreditCardBL.PayPalAPI.HostName    = PayPalHost; 
							objCreditCardBL.PayPalAPI.PartnerName = PayPalPartner; 
							objCreditCardBL.PayPalAPI.Password 	  = PayPalPassword;
						
							if(objCreditCardBL.ReverseSalesTransaction(Reference_Id,RecieptAmount,PolNum,customerID,PolicyId,PolicyVerId,UserId,Date_Flag))
							{
								objDataWrapper.CommitTransaction(DataLayer.DataWrapper.CloseConnection.YES);
								ReturnCode = 1 ; 
							}
							else
							{
								objDataWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
								ReturnCode = -1 ; //Unable to reverse transaction with Credit Card processor.
							}
					
						}
						else if (Payment_mode == (int)PaymentModes.EFT)
						{
							EFTReveresal(customerID, PolNum, PolicyId, PolicyVerId, RecieptAmount,Entity_Id,Entity_type);
							objDataWrapper.CommitTransaction(DataLayer.DataWrapper.CloseConnection.YES);
							ReturnCode = 1 ; 
						}
					}
					else
					{
						objDataWrapper.CommitTransaction(DataLayer.DataWrapper.CloseConnection.YES);
						ReturnCode = 1 ;
					}
				}
				else
				{
					objDataWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES );
					ReturnCode = -2;   
				}
				
			}
			catch(Exception ex)
			{ 
				objDataWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				
				System.Collections.Specialized.NameValueCollection addInfo = new System.Collections.Specialized.NameValueCollection();
				addInfo.Add("Err Descriptor ","Error while reversing payment");
				addInfo.Add("Policy No.", PolNum);
				addInfo.Add("CustomerID.", customerID.ToString());
				addInfo.Add("PolicyID.", PolicyId.ToString());
				addInfo.Add("DepositNo.", DepositNo.ToString());
				addInfo.Add("LineItemID.", lineITEM_ID.ToString());
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex,addInfo);
				ReturnCode = -10 ; 
			}
			return ReturnCode;
		}
	//Added till here
		#endregion
		#region
		//Added for #4914 by Shikha Dixit.
		public void EFTReveresal(int customerID,string PolNum,int PolicyId,int PolicyVerId,double RecieptAmount,int Entity_Id,string Entity_type)
		{
			string EFTReversalProcName = "Proc_EOD_AddREcordToEFTSpool";
	
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objDataWrapper.ClearParameteres();
				//IF Customer Paymnet from Agency 
				if (Entity_type == "AGN")
				{
					objDataWrapper.AddParameter("@ENTITY_ID",Entity_Id);
					objDataWrapper.AddParameter("@ENTITY_TYPE","AGN");
				}

				else
				{
					objDataWrapper.AddParameter("@ENTITY_ID",customerID );
					objDataWrapper.AddParameter("@ENTITY_TYPE","CUST");
				}
				objDataWrapper.AddParameter("@POLICY_NUMBER",PolNum);
				objDataWrapper.AddParameter("@POLICY_ID",PolicyId,SqlDbType.Int);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVerId,SqlDbType.Int);
				objDataWrapper.AddParameter("@REF_CHECK_ID",DBNull.Value);
				objDataWrapper.AddParameter("@REF_DEPOSIT_ID",DBNull.Value);
				objDataWrapper.AddParameter("@REF_DEP_DETAIL_ID",DBNull.Value);
				objDataWrapper.AddParameter("@TRANSACTION_AMOUNT",RecieptAmount);
				objDataWrapper.AddParameter("@TRANSACTION_CODE",(int)EFTCodes.CreditEntry,SqlDbType.Int);
				objDataWrapper.AddParameter("@ACC_TRAN_REQUIRED","N");
				//IF Customer Paymnet from Agency 
				if (Entity_type == "AGN")
				{
					objDataWrapper.AddParameter("@ACCOUNT_TO_USE",2);
				}
				else
				{
					objDataWrapper.AddParameter("@ACCOUNT_TO_USE",1);
				}
				//IF Customer Paymnet from Agency 
				if (Entity_type == "AGN")
				{
					objDataWrapper.AddParameter("@BRICS_TRAN_TYPE",(int)BRICSTransactionType.CustomerPaymentFromAgency ,SqlDbType.Int);
				}
				else
				{
					objDataWrapper.AddParameter("@BRICS_TRAN_TYPE",(int)BRICSTransactionType.InsuredPremium,SqlDbType.Int);
				}
				objDataWrapper.ExecuteNonQuery(EFTReversalProcName);
				objDataWrapper.CommitTransaction(DataLayer.DataWrapper.CloseConnection.YES);
			}	
			
			catch
			{
				objDataWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				//return -1;
			}
		}
		//End of addition.
		#endregion
	}
}
