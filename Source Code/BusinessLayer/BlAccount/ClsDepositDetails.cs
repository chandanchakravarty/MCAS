/******************************************************************************************
<Author					: - Vijay Joshi
<Start Date				: -	6/22/2005 7:09:49 PM
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: -  Vijay Arora
<Modified By			: -  28-03-2006
<Purpose				: -  Comment the RECEIPT_FROM_NAME variable condition to save the name from misc. Deposits.
*******************************************************************************************/ 
using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Cms.BusinessLayer.BlCommon;
using Cms.DataLayer;
using Cms.Model.Account;
using System.Collections;
using Model.Account;
//using Cms.BusinessLayer.BlCommon;  

namespace Cms.BusinessLayer.BlAccount
{
	/// <summary>
	/// 
	/// </summary>
	public class ClsDepositDetails : Cms.BusinessLayer.BlAccount.ClsAccount
	{
		private const	string		ACT_CURRENT_DEPOSIT_LINE_ITEMS			=	"ACT_CURRENT_DEPOSIT_LINE_ITEMS";

		#region Private Instance Variables
		private			bool		boolTransactionLog;
		private const string ACTIVATE_DEACTIVATE_PROC	= "Proc_ActivateDeactivateACT_CURRENT_DEPOSIT_LINE_ITEMS";
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
		public ClsDepositDetails()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			base.strActivateDeactivateProcedure	= ACTIVATE_DEACTIVATE_PROC;
		}
		public ClsDepositDetails(bool transactionLogRequired):this()
		{
			base.TransactionLogRequired = transactionLogRequired;
		}
		#endregion

		//This Function will be called from EFT process or Check Scanning process
		public int AddDepositDetail(DataWrapper objDataWrapper,ClsDepositDetailsInfo objDepositDetailsInfo,
			string strCreatedFrom,int Receipt_Mode)
		{
			return Add(objDataWrapper ,objDepositDetailsInfo,strCreatedFrom,Receipt_Mode);
		}
		public int AddDepositDetail(DataWrapper objDataWrapper,ClsDepositDetailsInfo objDepositDetailsInfo,
			string strCreatedFrom)
		{
			return Add(objDataWrapper ,objDepositDetailsInfo,strCreatedFrom,0);
		}
	
		//Commented By Ravindra(09-13-2007) : Why to have mutiple definitions in ClsDeposit & here
		//Call for this function inside this class changed to ClsDeposit.GetDepositNumber()
		/*
		public string GetDepositNumber(int depositId)
		{
			string	strStoredProc =	"Proc_FetchDepositNumberOnId";

			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@DEPOSIT_ID",depositId);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			try
			{
				return ds.Tables[0].Rows[0]["DEPOSIT_NUMBER"].ToString();
			}
			catch(Exception ex)
			{
				return "";
			}
		}*/

		#region Add(Insert) functions
		private int Add(DataLayer.DataWrapper objDataWrapper, ClsDepositDetailsInfo objDepositDetailsInfo,string strCreatedFrom,int Receipt_Mode)
		{
			string		strStoredProc	=	"Proc_InsertUpdateACT_CURRENT_DEPOSIT_LINE_ITEMS";
			DateTime	RecordDate		=	DateTime.Now;
			try
			{
				DataSet ds =  ClsDeposit.GetDepositNumber(objDepositDetailsInfo.DEPOSIT_ID,objDepositDetailsInfo.DEPOSIT_TYPE,objDepositDetailsInfo.POLICY_NO,objDataWrapper);
				string strDepositNO = "";
				int custID = 0,policyId=0,policyVerId=0;
				if (ds.Tables[0].Rows.Count > 0)
				{
					strDepositNO	= ds.Tables[0].Rows[0]["DEPOSIT_NUMBER"].ToString();
					custID			= Convert.ToInt32(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
					policyId		= Convert.ToInt32(ds.Tables[0].Rows[0]["POLICY_ID"].ToString());
					policyVerId		= Convert.ToInt32(ds.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

				}
				objDataWrapper.AddParameter("@DEPOSIT_ID", objDepositDetailsInfo.DEPOSIT_ID);
				objDataWrapper.AddParameter("@LINE_ITEM_INTERNAL_NUMBER", objDepositDetailsInfo.LINE_ITEM_INTERNAL_NUMBER);
				objDataWrapper.AddParameter("@ACCOUNT_ID", objDepositDetailsInfo.ACCOUNT_ID);
				objDataWrapper.AddParameter("@RECEIPT_AMOUNT", objDepositDetailsInfo.RECEIPT_AMOUNT);
				objDataWrapper.AddParameter("@PAYOR_TYPE", null);
				objDataWrapper.AddParameter("@RECEIPT_FROM_CODE", null);
				objDataWrapper.AddParameter("@CLAIM_NUMBER", objDepositDetailsInfo.CLAIM_NUMBER);
				if( objDepositDetailsInfo.RTL_BATCH_NUMBER.Trim() == "")
				{
					objDataWrapper.AddParameter("@RTL_BATCH_NUMBER", DBNull.Value);
				}
				else
				{
					objDataWrapper.AddParameter("@RTL_BATCH_NUMBER", objDepositDetailsInfo.RTL_BATCH_NUMBER);
				}

				if(objDepositDetailsInfo.RTL_GROUP_NUMBER.Trim() == "")
				{
					objDataWrapper.AddParameter("@RTL_GROUP_NUMBER", DBNull.Value);
				}
				else
				{
					objDataWrapper.AddParameter("@RTL_GROUP_NUMBER", objDepositDetailsInfo.RTL_GROUP_NUMBER);
				}
				if (objDepositDetailsInfo.MONTH_YEAR == 0)
				{
					objDataWrapper.AddParameter("@MONTH_YEAR", null);
				}
				else
				{
					objDataWrapper.AddParameter("@MONTH_YEAR", objDepositDetailsInfo.MONTH_YEAR);
				}
				if (objDepositDetailsInfo.POLICY_MONTH == 0)
				{
					objDataWrapper.AddParameter("@POLICY_MONTH", null);
				}
				else
				{
					objDataWrapper.AddParameter("@POLICY_MONTH", objDepositDetailsInfo.POLICY_MONTH);
				}

				if (objDepositDetailsInfo.RECEIPT_FROM_ID == 0)
				{
					objDataWrapper.AddParameter("@RECEIPT_FROM_ID", null);
				}
				else
				{
					objDataWrapper.AddParameter("@RECEIPT_FROM_ID", objDepositDetailsInfo.RECEIPT_FROM_ID);
				}

				objDataWrapper.AddParameter("@RECEIPT_FROM_NAME", objDepositDetailsInfo.RECEIPT_FROM_NAME);

				if (objDepositDetailsInfo.POLICY_ID != 0)
				{
					objDataWrapper.AddParameter("@POLICY_ID",objDepositDetailsInfo.POLICY_ID);
					objDataWrapper.AddParameter("@POLICY_VERSION_ID",objDepositDetailsInfo.POLICY_VERSION_ID);
				}
				else if (objDepositDetailsInfo.DEPOSIT_TYPE == "CUST")
				{
					objDataWrapper.AddParameter("@POLICY_ID",policyId);
					objDataWrapper.AddParameter("@POLICY_VERSION_ID",policyVerId);
				}
				else
				{
					objDataWrapper.AddParameter("@POLICY_ID",null);
					objDataWrapper.AddParameter("@POLICY_VERSION_ID",null);
				}

				objDataWrapper.AddParameter("@POLICY_NUMBER", objDepositDetailsInfo.POLICY_NO);
				objDataWrapper.AddParameter("@CREATED_BY",objDepositDetailsInfo.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objDepositDetailsInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@DEPOSIT_TYPE",objDepositDetailsInfo.DEPOSIT_TYPE);
				objDataWrapper.AddParameter("@CD_LINE_ITEM_ID", objDepositDetailsInfo.CD_LINE_ITEM_ID);
				objDataWrapper.AddParameter("@PAGE_ID", objDepositDetailsInfo.PAGE_ID);
				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DEPOSIT_DETAIL_ID",SqlDbType.Int,ParameterDirection.Output);
				if(strCreatedFrom != null)
				{
					objDataWrapper.AddParameter("@CREATED_FROM", strCreatedFrom);
				}
				SqlParameter objRetVal = (SqlParameter) objDataWrapper.AddParameter("@RetValue",SqlDbType.Int,ParameterDirection.ReturnValue);
				int returnResult = 0;
				if(TransactionLogRequired)
				{	
					objDepositDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("account/aspx/DepositDetails.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					//Itrack #3933 Note #5
					objTransactionInfo.TRANS_TYPE_ID	=	94;
					objTransactionInfo.CLIENT_ID		=	custID;
					objTransactionInfo.POLICY_ID		=	policyId;
					objTransactionInfo.POLICY_VER_TRACKING_ID = policyVerId;
                    //End Itrack #3933 Note #5


					if (IsEODProcess)
						objTransactionInfo.RECORDED_BY = EODUserID;
					else
						objTransactionInfo.RECORDED_BY		=	objDepositDetailsInfo.CREATED_BY;
					if(objDepositDetailsInfo.CD_LINE_ITEM_ID > 0)
					{
						if(Receipt_Mode == (int)PaymentModes.CreditCard)
							objTransactionInfo.TRANS_DESC		=	"Deposit (Credit Card) Line Item(s) Modified successfully.";
						else if(Receipt_Mode == (int)PaymentModes.Check)
							objTransactionInfo.TRANS_DESC		=	"Deposit (Check) Line Item(s) Modified successfully.";
						else if(Receipt_Mode == (int)PaymentModes.EFT)
							objTransactionInfo.TRANS_DESC		=	"Deposit (EFT-Sweep) Line Item(s) Modified successfully.";
						else
							objTransactionInfo.TRANS_DESC		=	"Deposit Line Item(s) Modified successfully.";
						//Formated FOr Itrack issue # 6598
						objTransactionInfo.CUSTOM_INFO		=	"Deposit Number :" + strDepositNO +";Deposit Type :" + objDepositDetailsInfo.DEPOSIT_TYPE + ";Receipt Amount :" + "$" +  String.Format("{0:0,0.00}", objDepositDetailsInfo.RECEIPT_AMOUNT);

					}
					else
					{
						if(Receipt_Mode == (int)PaymentModes.CreditCard)
							objTransactionInfo.TRANS_DESC		=	"Deposit (Credit Card) Line Item(s) added successfully.";
						else if(Receipt_Mode == (int)PaymentModes.Check)
							objTransactionInfo.TRANS_DESC		=	"Deposit (Check) Line Item(s) added successfully.";
						else if(Receipt_Mode == (int)PaymentModes.EFT)
							objTransactionInfo.TRANS_DESC		=	"Deposit (EFT-Sweep) Line Item(s) added successfully.";
						else if(strCreatedFrom == "RTL")
							objTransactionInfo.TRANS_DESC		=	"Deposit Line Item(s) added successfully through RTL.";
						else
							objTransactionInfo.TRANS_DESC		=	"Deposit Line Item(s) added successfully.";
						//Formated FOr Itrack issue # 6598
						objTransactionInfo.CUSTOM_INFO		=	"Deposit Number :" + strDepositNO +";Deposit Type :" + objDepositDetailsInfo.DEPOSIT_TYPE + ";Receipt Amount :" + "$" +  String.Format("{0:0,0.00}", objDepositDetailsInfo.RECEIPT_AMOUNT);
					}
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				}				
				returnResult = int.Parse(objRetVal.Value.ToString());
				if (returnResult > 0 )
				{
					objDepositDetailsInfo.CD_LINE_ITEM_ID = int.Parse(objSqlParameter.Value.ToString());
				}
				objDataWrapper.ClearParameteres();
				//Ravindra (09-13-2007) To be committed from the calling function 
				//If commiting at lineitem level then will not be able to rollback current transaction
				//as datawrapper implementation remove the transaction after committing , so call for this 
				//function even from same data wrapper will create a different transaction 
				//objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
				return returnResult;
			}
			catch(Exception ex)
			{
				throw(ex);
			}

		}
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objDepositDetailsInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		private int Add(DataLayer.DataWrapper objDataWrapper, ClsDepositDetailsInfo objDepositDetailsInfo)
		{
			return Add(objDataWrapper,objDepositDetailsInfo,null,0);
		}

		#region Import RTL File
		public int ImportRTLFile(ArrayList objArrLst)
		{
			DataWrapper objWrapper;
			objWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
			int intRetVal=0;
			int i=0;
			int temp=0;
			int currentpage = 18;
			int intRandNum = ClsDepositDetails.GetMaxLineItemID();
					
			try
			{
					foreach(ClsDepositDetailsInfo objDepBL in objArrLst)
					{
						
						if(i >= currentpage) 
						{
							intRandNum = ClsDepositDetails.GetMaxLineItemID();
							i = 0;
						}
						objDepBL.PAGE_ID = intRandNum.ToString();

						if(objDepBL.RECEIPT_AMOUNT!=0.0) //Pass 0.0 if AMount is not valid in RTL File
							intRetVal=Add(objWrapper,objDepBL,"RTL",0);
						else
							intRetVal = -2; //Invalid Record


						//Loging RTL Process
						if(intRetVal == 1) //Successful Policies
						{
							i++;//Increment Counter only if the Import is successful
							LogRTLProcess(objDepBL.DEPOSIT_ID,objDepBL.POLICY_NO,"Policy Found in Database","SUCCEEDED","","",objDepBL.RECEIPT_AMOUNT.ToString());
						}
						else if(intRetVal == -2) //Invalid Policies
						{
							if(objDepBL.RECEIPT_AMOUNT == 0.0) //Update Log if 0.0 AMount is not valid in RTL File
								LogRTLProcess(objDepBL.DEPOSIT_ID,objDepBL.POLICY_NO,"Invalid Amount Format","FAILED","","Invalid Amount Format",objDepBL.RECEIPT_AMOUNT.ToString());
							else
								LogRTLProcess(objDepBL.DEPOSIT_ID,objDepBL.POLICY_NO,"Policy Not Found in Database","FAILED","","Invalid Policy Number",objDepBL.RECEIPT_AMOUNT.ToString());

						}
						else if(intRetVal == -10) //AB Type Policies
							LogRTLProcess(objDepBL.DEPOSIT_ID,objDepBL.POLICY_NO,"Only DB Policy Number can be used.Please try again.","FAILED","","AB Type Policy.",objDepBL.RECEIPT_AMOUNT.ToString());
						//End Log process
						
						if(intRetVal<=0)
						{
							//break;
							if(intRetVal == -2 || intRetVal == -10)
							{
								temp ++;
							}
							if(temp == objArrLst.Count)
								intRetVal = -3;//Nothing Imported
							else
								intRetVal = -2;//Partial Imported							
						}
						else
							continue;
					

					}
				
				objWrapper.CommitTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				if(temp > 0 && intRetVal!=-3) //Policy Number Does not exists
                    return -2;
				else if(intRetVal == -3)//Nothing Imported
					return -3;
				else
					return 1;//Fully Imported

			}
			catch(Exception objExp)
			{
				objWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				throw objExp;
			}
		}
		#endregion


		#endregion

		#region Edit: Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldDepositDetailsInfo">Model object having old information</param>
		/// <param name="objDepositDetailsInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsDepositDetailsInfo objOldDepositDetailsInfo,ClsDepositDetailsInfo objDepositDetailsInfo)
		{
			string		strStoredProc	=	"Proc_UpdateACT_CURRENT_DEPOSIT_LINE_ITEMS_Edited";
			string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try 
			{
				objDataWrapper.AddParameter("@CD_LINE_ITEM_ID",objDepositDetailsInfo.CD_LINE_ITEM_ID);
				objDataWrapper.AddParameter("@LINE_ITEM_INTERNAL_NUMBER",objDepositDetailsInfo.LINE_ITEM_INTERNAL_NUMBER);
				objDataWrapper.AddParameter("@DIV_ID",objDepositDetailsInfo.DIV_ID);
				objDataWrapper.AddParameter("@DEPT_ID",objDepositDetailsInfo.DEPT_ID);
				objDataWrapper.AddParameter("@PC_ID",objDepositDetailsInfo.PC_ID);
				//objDataWrapper.AddParameter("@ACCOUNT_ID",objDepositDetailsInfo.ACCOUNT_ID);
				objDataWrapper.AddParameter("@BANK_NAME",objDepositDetailsInfo.BANK_NAME);
				objDataWrapper.AddParameter("@CHECK_NUM",objDepositDetailsInfo.CHECK_NUM);
				//objDataWrapper.AddParameter("@RECEIPT_NUM",objDepositDetailsInfo.RECEIPT_NUM);
				objDataWrapper.AddParameter("@RECEIPT_AMOUNT",objDepositDetailsInfo.RECEIPT_AMOUNT);
				//objDataWrapper.AddParameter("@RECEIPT_FROM_CODE",objDepositDetailsInfo.RECEIPT_FROM_CODE);
				objDataWrapper.AddParameter("@PAYOR_TYPE",objDepositDetailsInfo.PAYOR_TYPE);
				if(objDepositDetailsInfo.PAYOR_TYPE.ToUpper().Equals("OTH"))
					//if payer type is other data will go into RECEIPT_FROM_NAME and not into RECEIPT_FROM_ID
				{
					objDataWrapper.AddParameter("@RECEIPT_FROM_NAME",objDepositDetailsInfo.RECEIPT_FROM_NAME);
					objDataWrapper.AddParameter("@RECEIPT_FROM_ID",DBNull.Value);
				}
				else
				{
					objDataWrapper.AddParameter("@RECEIPT_FROM_ID",objDepositDetailsInfo.RECEIPT_FROM_ID);
					objDataWrapper.AddParameter("@RECEIPT_FROM_NAME",DBNull.Value);
				}
				//objDataWrapper.AddParameter("@BILL_TYPE",objDepositDetailsInfo.BILL_TYPE);
				objDataWrapper.AddParameter("@LINE_ITEM_DESCRIPTION",objDepositDetailsInfo.LINE_ITEM_DESCRIPTION);
				//objDataWrapper.AddParameter("@CUSTOMER_ID",objDepositDetailsInfo.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objDepositDetailsInfo.POLICY_ID);
				//objDataWrapper.AddParameter("@POLICY_VERSION_ID",objDepositDetailsInfo.POLICY_VERSION_ID);
				//objDataWrapper.AddParameter("@IN_RECON",objDepositDetailsInfo.IN_RECON);
				//objDataWrapper.AddParameter("@AVAILABLE_BALANCE",objDepositDetailsInfo.AVAILABLE_BALANCE);
				objDataWrapper.AddParameter("@REF_CUSTOMER_ID",objDepositDetailsInfo.REF_CUSTOMER_ID);
				//objDataWrapper.AddParameter("@IS_BNK_RECONCILED",objDepositDetailsInfo.IS_BNK_RECONCILED);
				//objDataWrapper.AddParameter("@IS_ACTIVE",objDepositDetailsInfo.IS_ACTIVE);
				//objDataWrapper.AddParameter("@CREATED_BY",objDepositDetailsInfo.CREATED_BY);
				//objDataWrapper.AddParameter("@CREATED_DATETIME",objDepositDetailsInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY",objDepositDetailsInfo.MODIFIED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME",objDepositDetailsInfo.LAST_UPDATED_DATETIME);
				
				if(base.TransactionLogRequired) 
				{
					objDepositDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("/account/aspx/EditDeposit.aspx.resx");
					objBuilder.GetUpdateSQL(objOldDepositDetailsInfo,objDepositDetailsInfo,out strTranXML);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					objTransactionInfo.RECORDED_BY		=	objDepositDetailsInfo.MODIFIED_BY;
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

		#region Update method
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldDepositDetailsInfo">Model object having old information</param>
		/// <param name="objDepositDetailsInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int Update(ClsDepositDetailsInfo objDepositDetailsInfo)
		{
			string strStoredProc = "Proc_UpdateACT_CURRENT_DEPOSIT_LINE_ITEMS";
			//string strTranXML;
			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);

			try 
			{
				objDataWrapper.AddParameter("@DEPOSIT_ID", objDepositDetailsInfo.DEPOSIT_ID);
				objDataWrapper.AddParameter("@LINE_ITEM_INTERNAL_NUMBER", objDepositDetailsInfo.LINE_ITEM_INTERNAL_NUMBER);
				objDataWrapper.AddParameter("@ACCOUNT_ID", objDepositDetailsInfo.ACCOUNT_ID);
				objDataWrapper.AddParameter("@RECEIPT_AMOUNT", objDepositDetailsInfo.RECEIPT_AMOUNT);
				objDataWrapper.AddParameter("@PAYOR_TYPE", objDepositDetailsInfo.PAYOR_TYPE);
				objDataWrapper.AddParameter("@RECEIPT_FROM_ID", objDepositDetailsInfo.RECEIPT_FROM_ID);
				objDataWrapper.AddParameter("@RECEIPT_FROM_CODE", objDepositDetailsInfo.RECEIPT_FROM_CODE);
				objDataWrapper.AddParameter("@RECEIPT_FROM_NAME", objDepositDetailsInfo.RECEIPT_FROM_NAME);
				objDataWrapper.AddParameter("@POLICY_ID", objDepositDetailsInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID", objDepositDetailsInfo.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@MODIFIED_BY", objDepositDetailsInfo.CREATED_BY);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objDepositDetailsInfo.CREATED_DATETIME);
				objDataWrapper.AddParameter("@CD_LINE_ITEM_ID", objDepositDetailsInfo.CD_LINE_ITEM_ID);

				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
				
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

//
		public int UpdateReconcileStatus(int intCD_LINE_ITEM,string strStatus ,int CREATED_BY)
		{

			string	strStoredProc	=	"Proc_UpdateReconStatus_AGN";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			objDataWrapper.AddParameter("@CD_LINE_ITEM_ID", intCD_LINE_ITEM);
			objDataWrapper.AddParameter("@STATUS", strStatus );
			
			if(TransactionLogRequired) 
			{
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	3;
				objTransactionInfo.RECORDED_BY		=  CREATED_BY;
				objTransactionInfo.TRANS_DESC		=	"Agency Reconciliation Status has been modified.";

				DataSet dsDeposit  =  GetDepositId(intCD_LINE_ITEM);
				string strDEPOSIT_NUMBER = dsDeposit.Tables[0].Rows[0]["DEPOSIT_NUMBER"].ToString();
				objTransactionInfo.CUSTOM_INFO		=	"Deposit Number :"+strDEPOSIT_NUMBER+";Status :"+ strStatus  ;
				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc, objTransactionInfo);
			}
			else
				returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
			objDataWrapper.ClearParameteres();
			objDataWrapper.Dispose();
			return returnResult ;
		}

//

		#region UpdateDeposit
		private int UpdateDeposit(DataLayer.DataWrapper objDataWrapper, ClsDepositDetailsInfo objDepositDetailsInfo)
		{
			//string strStoredProc = "Proc_UpdateACT_CURRENT_DEPOSIT_LINE_ITEMS";
			string	strStoredProc	=	"Proc_InsertUpdateACT_CURRENT_DEPOSIT_LINE_ITEMS";
			int returnResult = 0;
			//string strTranXML;

			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();


			
			objDataWrapper.AddParameter("@DEPOSIT_ID", objDepositDetailsInfo.DEPOSIT_ID);
			objDataWrapper.AddParameter("@LINE_ITEM_INTERNAL_NUMBER", objDepositDetailsInfo.LINE_ITEM_INTERNAL_NUMBER);
			objDataWrapper.AddParameter("@ACCOUNT_ID", objDepositDetailsInfo.ACCOUNT_ID);
			objDataWrapper.AddParameter("@RECEIPT_AMOUNT", objDepositDetailsInfo.RECEIPT_AMOUNT);
			objDataWrapper.AddParameter("@PAYOR_TYPE", null);
			objDataWrapper.AddParameter("@POLICY_NUMBER", objDepositDetailsInfo.POLICY_NO);
			objDataWrapper.AddParameter("@CLAIM_NUMBER", objDepositDetailsInfo.CLAIM_NUMBER);
			objDataWrapper.AddParameter("@RECEIPT_FROM_NAME", objDepositDetailsInfo.RECEIPT_FROM_NAME);
			
			if (objDepositDetailsInfo.POLICY_MONTH == 0)
			{
				objDataWrapper.AddParameter("@POLICY_MONTH", null);
			}
			else
			{
				objDataWrapper.AddParameter("@POLICY_MONTH", objDepositDetailsInfo.POLICY_MONTH);
			}

			if (objDepositDetailsInfo.MONTH_YEAR == 0)
			{
				objDataWrapper.AddParameter("@MONTH_YEAR", null);
			}
			else
			{
				objDataWrapper.AddParameter("@MONTH_YEAR", objDepositDetailsInfo.MONTH_YEAR);
			}

			if (objDepositDetailsInfo.RECEIPT_FROM_ID == 0)
			{
				objDataWrapper.AddParameter("@RECEIPT_FROM_ID", null);
				objDataWrapper.AddParameter("@RECEIPT_FROM_CODE", null);
			}
			else
			{
				objDataWrapper.AddParameter("@RECEIPT_FROM_ID", objDepositDetailsInfo.RECEIPT_FROM_ID);
				objDataWrapper.AddParameter("@RECEIPT_FROM_CODE", objDepositDetailsInfo.RECEIPT_FROM_CODE);
			}

			if (objDepositDetailsInfo.POLICY_ID != 0)
			{
				objDataWrapper.AddParameter("@POLICY_ID", objDepositDetailsInfo.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID", objDepositDetailsInfo.POLICY_VERSION_ID);
			}
			else
			{
				objDataWrapper.AddParameter("@POLICY_ID", null);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID", null);
			}

			objDataWrapper.AddParameter("@MODIFIED_BY", objDepositDetailsInfo.MODIFIED_BY);
			objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objDepositDetailsInfo.LAST_UPDATED_DATETIME);
			objDataWrapper.AddParameter("@DEPOSIT_TYPE",objDepositDetailsInfo.DEPOSIT_TYPE);
			objDataWrapper.AddParameter("@CD_LINE_ITEM_ID", objDepositDetailsInfo.CD_LINE_ITEM_ID);

			SqlParameter objRetVal = (SqlParameter)objDataWrapper.AddParameter("@RET_VAL", SqlDbType.Int, ParameterDirection.ReturnValue);

			SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@DEPOSIT_DETAIL_ID",SqlDbType.Int,ParameterDirection.Output);
			if(TransactionLogRequired)
			{	
				objDepositDetailsInfo.TransactLabel = ClsCommon.MapTransactionLabel("account/aspx/DepositDetails.aspx.resx");
//				SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
				Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
				objTransactionInfo.TRANS_TYPE_ID	=	1;
				objTransactionInfo.RECORDED_BY		=	objDepositDetailsInfo.CREATED_BY;
				objTransactionInfo.TRANS_DESC		=	"Deposit Line Item(s) Modified successfully.";
				//objTransactionInfo.CUSTOM_INFO		=	";Deposit Type = " + objDepositDetailsInfo.DEPOSIT_TYPE + ";Receipt Amount = " + objDepositDetailsInfo.RECEIPT_AMOUNT;
               //changes made by uday on 30 Nov
			  //objTransactionInfo.CUSTOM_INFO		=	"Deposit Number :" + GetDepositId(objDepositDetailsInfo.CD_LINE_ITEM_ID)+";Deposit Id :" + objDepositDetailsInfo.DEPOSIT_ID + ";Deposit Type :" + objDepositDetailsInfo.DEPOSIT_TYPE + ";Receipt Amount :" + objDepositDetailsInfo.RECEIPT_AMOUNT;
				DataSet dsDeposit  =  GetDepositId(objDepositDetailsInfo.CD_LINE_ITEM_ID);
				string strDEPOSIT_NUMBER = dsDeposit.Tables[0].Rows[0]["DEPOSIT_NUMBER"].ToString();
				objTransactionInfo.CUSTOM_INFO		=	"Deposit Number :" + strDEPOSIT_NUMBER+";Deposit Type :" + objDepositDetailsInfo.DEPOSIT_TYPE + ";Receipt Amount :" + objDepositDetailsInfo.RECEIPT_AMOUNT;
			   //
				

				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc,objTransactionInfo);
			}
			else
			{
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
			}
			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			objDataWrapper.ClearParameteres();
			//returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);
			//objDataWrapper.ClearParameteres();
			
			return Convert.ToInt32(objRetVal.Value);
			
		}
		#endregion
		/// <summary>
		/// Get Deposit  Id based on CD_LINE_ITEM
		/// </summary>
		/// <param name="intCD_LINE_ITEM"></param>
		/// <returns></returns>
		public DataSet GetDepositId(int intCD_LINE_ITEM)
		{
			string	strStoredProc =	"Proc_FetchDepositId";

			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@CD_LINE_ITEM_ID",intCD_LINE_ITEM);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			return ds;
			
		}



		public DataSet GetCoverages(int DepositID, int currentPageIndex,int pageSize)
		{
			string	strStoredProc =	"Proc_GetACT_CURRENT_DEPOSIT_LINE_ITEMS";

			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@DEPOSIT_ID",DepositID);
			objWrapper.AddParameter("@PAGE_SIZE",pageSize);
			objWrapper.AddParameter("@CURRENT_PAGE_INDEX",currentPageIndex);

			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		}

		//Added By Ravindra (12-06-2006)
		public DataSet GetDepositLineItems(int DepositID,string strDepositType ,
				int currentPageIndex,int pageSize)
		{
			string	strStoredProc =	"Proc_GetACT_CURRENT_DEPOSIT_LINE_ITEMS";

			DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
			objWrapper.AddParameter("@DEPOSIT_ID",DepositID);
			objWrapper.AddParameter("@PAGE_SIZE",pageSize);
			objWrapper.AddParameter("@CURRENT_PAGE_INDEX",currentPageIndex);
			objWrapper.AddParameter("@DEPOSIT_TYPE",strDepositType);
			DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
			
			return ds;
		}

		public int Save(ArrayList al)
		{
			try
			{
				for(int i = 0; i < al.Count; i++ )
				{
					//					if (((ClsDepositDetailsInfo)al[i]).CD_LINE_ITEM_ID <= 0)
					//						Add((ClsDepositDetailsInfo)al[i]);
					//					else
					//						Update((ClsDepositDetailsInfo)al[i]);
				}
			}
			catch (Exception objExp)
			{
				throw objExp;
			}
			return 1;
		}

		
		public int Delete(ArrayList al,int CREATED_BY)
		{

			ClsDepositDetailsInfo objClsDepositDetailsInfo = new ClsDepositDetailsInfo();
			
			try
			{
				for(int i = 0; i < al.Count; i++ )
				{
					
					if (((ClsDepositDetailsInfo)al[i]).CD_LINE_ITEM_ID > 0)
						Delete(((ClsDepositDetailsInfo)al[i]).CD_LINE_ITEM_ID,((ClsDepositDetailsInfo)al[i]).CUSTOMER_ID,CREATED_BY);
				}
			}
			catch (Exception objExp)
			{
				throw objExp;
			}
			return 1;
		}

		public int Delete(ArrayList al/*,int CREATED_BY*/)
		{

			ClsDepositDetailsInfo objClsDepositDetailsInfo = new ClsDepositDetailsInfo();
			
			try
			{
				for(int i = 0; i < al.Count; i++ )
				{
					
					if (((ClsDepositDetailsInfo)al[i]).CD_LINE_ITEM_ID > 0)
						Delete(((ClsDepositDetailsInfo)al[i]).CD_LINE_ITEM_ID,((ClsDepositDetailsInfo)al[i]).CUSTOMER_ID);
				}
			}
			catch (Exception objExp)
			{
				throw objExp;
			}
			return 1;
		}

		#region FETCH AGENCY DEPOSITS 
		
			public DataSet FetchAgencyDepositsLineItems(int month,int year,string AgencyName,int depositID)
			{
				DataSet ds = new DataSet();
			
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

				try
				{
					objDataWrapper.AddParameter("@MONTH", month, SqlDbType.Int);
					objDataWrapper.AddParameter("@YEAR", year, SqlDbType.Int);
					objDataWrapper.AddParameter("@AGENCY_NAME", AgencyName, SqlDbType.VarChar);
					objDataWrapper.AddParameter("@DEPOSIT_ID", depositID, SqlDbType.Int);
					ds = objDataWrapper.ExecuteDataSet("Proc_FetchAgencyDepositLineItems");
				
					if (ds.Tables[0].Rows.Count > 0)				
						return ds;
					else
						return null;
				
					//ds.Dispose();
				}
				catch(Exception objEx)
				{
					throw(objEx);				
				}
				finally
				{
					if(objDataWrapper!=null)
						objDataWrapper.Dispose(); 			
				}
			}
		public DataSet FetchAgencyDeposits(int depositID)
		{
			DataSet ds = new DataSet();
			
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			try
			{
				objDataWrapper.AddParameter("@MONTH", null, SqlDbType.Int);
				objDataWrapper.AddParameter("@YEAR", null, SqlDbType.Int);
				objDataWrapper.AddParameter("@AGENCY_NAME", null, SqlDbType.VarChar);
				objDataWrapper.AddParameter("@DEPOSIT_ID", depositID, SqlDbType.Int);
				ds = objDataWrapper.ExecuteDataSet("Proc_FetchAgencyDeposit");
				
				if (ds.Tables[0].Rows.Count > 0)				
					return ds;
				else
					return null;
				
				//ds.Dispose();
			}
			catch(Exception objEx)
			{
				throw(objEx);				
			}
			finally
			{
				if(objDataWrapper!=null)
					objDataWrapper.Dispose(); 			
			}
		}
        
		#endregion

		#region Delete

		/// <summary>
		/// Delete the whole record of spefied id 
		/// </summary>
		/// <returns>Nos of rows deleted</returns>
		public int Delete(int intLineItemId)
		{
			return Delete(intLineItemId,0);
		}

		public int Delete(int intLineItemId,int CUSTOMER_ID, int CREATED_BY)
		{
			try
			{
				/*Calling the stored procedure to get the maximum Journal entry no*/
				String strStoredProc = "Proc_DeleteACT_CURRENT_DEPOSIT_LINE_ITEMS";
				int Value;
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CD_LINE_ITEM_ID", intLineItemId);
				SqlParameter objRetVal = (SqlParameter) objDataWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);
				if(TransactionLogRequired) 
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					if (CREATED_BY!= 0)
						objTransactionInfo.RECORDED_BY		=  	CREATED_BY ;  
					objTransactionInfo.TRANS_DESC		=	"Deposit Line Item(s) deleted successfully.";
					//objTransactionInfo.TRANS_DESC		=	"Deposit Distribution is deleted successfully.";
					//objTransactionInfo.CUSTOM_INFO		=	intLineItemId.ToString();
					DataSet dsDeposit  =  GetDepositId(intLineItemId);
					string strDEPOSIT_NUMBER = dsDeposit.Tables[0].Rows[0]["DEPOSIT_NUMBER"].ToString();
					string strDEPOSIT_ID = dsDeposit.Tables[0].Rows[0]["DEPOSIT_ID"].ToString();
					string strDEPOSIT_TYPE = dsDeposit.Tables[0].Rows[0]["DEPOSIT_TYPE"].ToString();
					string strRECEIPT_AMOUNT = dsDeposit.Tables[0].Rows[0]["RECEIPT_AMOUNT"].ToString();
					objTransactionInfo.CUSTOM_INFO		=	"Deposit Number :" + strDEPOSIT_NUMBER +";Deposit Type :" + strDEPOSIT_TYPE + ";Receipt Amount :" + strRECEIPT_AMOUNT;
                    //objTransactionInfo.CUSTOM_INFO		=	"Deposit Number :" + GetDepositId(intLineItemId);

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
		}

		public int Delete(int intLineItemId, int CREATED_BY)
		{
			try
			{
				/*Calling the stored procedure to get the maximum Journal entry no*/
				String strStoredProc = "Proc_DeleteACT_CURRENT_DEPOSIT_LINE_ITEMS";
				int Value;
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objDataWrapper.AddParameter("@CD_LINE_ITEM_ID", intLineItemId);
				SqlParameter objRetVal = (SqlParameter) objDataWrapper.AddParameter("@RetVal",SqlDbType.Int,ParameterDirection.ReturnValue);
				if(TransactionLogRequired) 
				{
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					//objTransactionInfo.TRANS_TYPE_ID	=	3;
					if (CREATED_BY!= 0)
					objTransactionInfo.RECORDED_BY		=  	CREATED_BY ;  
					objTransactionInfo.TRANS_DESC		=	"Deposit Line Item(s) deleted successfully.";
					#region GET DEPOSIT DETAILS FOR TRANSLOG:
					string strDEPOSIT_NUMBER = "",strDEPOSIT_TYPE="",strRECEIPT_AMOUNT="";
					int intCustomerID=0,intPolicyId = 0,intPolicyVerId = 0;
                    DataSet dsDeposit  =  GetDepositId(intLineItemId);
					if(dsDeposit!=null)
					{
						if(dsDeposit.Tables[0].Rows.Count > 0)
						{
							strDEPOSIT_NUMBER = dsDeposit.Tables[0].Rows[0]["DEPOSIT_NUMBER"].ToString();
							strDEPOSIT_TYPE = dsDeposit.Tables[0].Rows[0]["DEPOSIT_TYPE"].ToString();
							strRECEIPT_AMOUNT = dsDeposit.Tables[0].Rows[0]["RECEIPT_AMOUNT"].ToString();
							//Itrack #3933
							if(strDEPOSIT_TYPE == "CUST")
							{
								intCustomerID   = Convert.ToInt32(dsDeposit.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
								intPolicyId		= Convert.ToInt32(dsDeposit.Tables[0].Rows[0]["POLICY_ID"].ToString());
								intPolicyVerId   = Convert.ToInt32(dsDeposit.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());

								//Added on March 25 Itrack #3933
								objTransactionInfo.TRANS_TYPE_ID				=	94;
								objTransactionInfo.CLIENT_ID					=	intCustomerID;
								objTransactionInfo.POLICY_ID					=	intPolicyId;
								objTransactionInfo.POLICY_VER_TRACKING_ID		=	intPolicyVerId;
							}
							else
							{
								objTransactionInfo.TRANS_TYPE_ID	=	3;

							}
						}
					}
					#endregion
					objTransactionInfo.CUSTOM_INFO		=	"Deposit Number :" + strDEPOSIT_NUMBER +";Deposit Type :" + strDEPOSIT_TYPE + ";Receipt Amount :" + strRECEIPT_AMOUNT;

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
		}
		#endregion

		#region PUBLIC UTILITY METHODS
		/// <summary>
		/// Fetches all the active installment plans in the act_installment_plan table
		/// </summary>
		/// <returns>Dataset</returns>
		public static DataSet FetchInstallmentPlans()
		{
			try
			{
				string	strStoredProc =	"Proc_FetchInstallmentPlans";
				DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
	
				DataSet dsInstallmentPlans = objWrapper.ExecuteDataSet(strStoredProc);
				return dsInstallmentPlans;
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{
			
			}
		}

		/// <summary>
		/// Returns the Active Installment Plans
		/// </summary>
		/// <returns></returns>
		public static DataSet FetchActiveInstallmentPlans(int PlanID)
		{
			try
			{
				string	strStoredProc =	"Proc_FetchActiveInstallmentPlans";
				DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);	
				if(PlanID != 0)
					objWrapper.AddParameter("@PLAN_ID", PlanID);
				DataSet dsInstallmentPlans = objWrapper.ExecuteDataSet(strStoredProc);
				return dsInstallmentPlans;
			}
			catch(Exception exc)
			{
				throw(exc);
			}
			finally
			{
			
			}
		}

		#endregion

		#region "Edit: GetxmlMethods"
		public static string GetXmlForEditPageControls(string CD_LINE_ITEM_ID)
		{
			string strSql = "Proc_GetEditXML_ACT_CURRENT_DEPOSIT_LINE_ITEMS";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			objDataWrapper.AddParameter("@CD_LINE_ITEM_ID",CD_LINE_ITEM_ID);
			DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
			return objDataSet.GetXml();
		}
		#endregion

		/*#region "Edit: Get applied status"
		public static string 
		#endregion*/
	
		/// <summary>
		/// Adds the customer deposit line items to database
		/// </summary>
		/// <param name="objDepositDetailsInfo">Model object of ClsDepositDetailsInfo</param>
		public int AddCustomerDepositLineItems(Cms.Model.Account.ClsDepositDetailsInfo objDepositDetailsInfo, double dblTapeTotal)
		{
			DataWrapper objWrapper;
			objWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure, 
				DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);

			int intRetVal;

			try
			{
				if (objDepositDetailsInfo.CD_LINE_ITEM_ID <= 0)
				{
					intRetVal = Add(objWrapper, objDepositDetailsInfo);
				}
				else
				{
					UpdateDeposit(objWrapper, objDepositDetailsInfo);
				}
				objWrapper.CommitTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				return 1;
			}
			catch (Exception objExp)
			{
				objWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				throw objExp;
			}
			finally
			{
				objWrapper.Dispose();
			}

		}

		/// <summary>
		/// Adds the customer deposit line items to database
		/// </summary>
		/// <param name="al">Array list containing the array of ClsDepositDetailsInfo objects</param>
		public int AddCustomerDepositLineItems(ArrayList al, out ArrayList alStatus,  double dblTapeTotal, bool Confirm)
		{
			DataWrapper objWrapper;
			objWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure, 
				DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
			

			bool SaveStatus = true;

			int intRetVal;
			alStatus = new ArrayList();
			try
			{
				for(int i = 0; i < al.Count; i++ )
				{
					if (((ClsDepositDetailsInfo)al[i]).CD_LINE_ITEM_ID <= 0)
					{
						intRetVal = Add(objWrapper, (ClsDepositDetailsInfo)al[i]);
						
						alStatus.Add(intRetVal);
						if (intRetVal <= 0 )
						{
							//Some error occured, hence updating the save status flag
							SaveStatus = false;
						}
					}
					else
					{
						intRetVal = UpdateDeposit(objWrapper, (ClsDepositDetailsInfo)al[i]);
						alStatus.Add(intRetVal);
						if (intRetVal <= 0 )
						{
							//Some error occured, hence updating the save status flag
							SaveStatus = false;
						}
					}
				}

				if (SaveStatus == false)
				{
					//Some error occured while saving deposit details, hence rollbacking
					objWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
					return -1;
				}
				
				if (al.Count > 0)
				{
					//Updating the tape total
					objWrapper.ClearParameteres();
					ClsDeposit.UpdateTapeTotal(objWrapper, ((ClsDepositDetailsInfo)al[0]).DEPOSIT_ID, dblTapeTotal);
				}
				// confirm & commit only in case of save & confirm btn
				if (Confirm)
				{
					//Confirming the deposit 
					ClsDeposit objDeposit = new ClsDeposit();
					objDeposit.Confirm(objWrapper, ((ClsDepositDetailsInfo)al[0]).DEPOSIT_ID);
					objDeposit.Dispose();			
				}	
				objWrapper.CommitTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				
				return 1;
			}
			catch (Exception objExp)
			{
				objWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				throw objExp;
			}
			finally
			{
				objWrapper.Dispose();
			}
		}
		
		/// <summary>
		/// Adds the claim receipts, reinsurance receipts and misc line items to database
		/// </summary>
		/// <param name="al">Array list containing the array of ClsDepositDetailsInfo objects</param>
		/// <param name="dblTapeTotal">Tape total</param>
		public int AddOtherDepositLineItems(ArrayList al, double dblTapeTotal, bool confirm)
		{
			DataWrapper objWrapper;
			objWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure, 
				DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
			
			int intRetVal;
			
			try
			{
				for(int i = 0; i < al.Count; i++ )
				{
					if (((ClsDepositDetailsInfo)al[i]).CD_LINE_ITEM_ID <= 0)
					{
						intRetVal = Add(objWrapper, (ClsDepositDetailsInfo)al[i]);
						
						
						if (intRetVal <= 0 )
						{
							objWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
							//UpdateErrorInfo(al, out alStatus, i);
							return -1;
						}
					}
					else
					{
						UpdateDeposit(objWrapper, (ClsDepositDetailsInfo)al[i]);
					}
				}
				
				if (al.Count > 0)
				{
					//Updating the tape total
					objWrapper.ClearParameteres();
					ClsDeposit.UpdateTapeTotal(objWrapper, ((ClsDepositDetailsInfo)al[0]).DEPOSIT_ID, dblTapeTotal);
				}

				//Confirming the deposit 
				if(confirm)
				{
					ClsDeposit objDeposit = new ClsDeposit();
					objDeposit.Confirm(objWrapper, ((ClsDepositDetailsInfo)al[0]).DEPOSIT_ID);
					objDeposit.Dispose();
				}


				objWrapper.CommitTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				return 1;
			}
			catch (Exception objExp)
			{
				objWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				throw objExp;
			}
			finally
			{
				objWrapper.Dispose();
			}
		}

		/// <summary>
		/// Saves the information to the database
		/// </summary>
		/// <param name="objDepositDetailsInfo">Model object</param>
		/// <returns>1 if saves sucessfull else error code</returns>
		public int AddAgencyDepositLineItems(ClsDepositDetailsInfo objDepositDetailsInfo)
		{
			DataWrapper objWrapper;
			objWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure, 
				DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
			
			int intRetVal;
			
			try
			{
				intRetVal = Add(objWrapper, objDepositDetailsInfo);
						
				if (intRetVal <= 0 )
				{
					objWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
					return -1;
				}
					
				//Confirming the deposit 
				ClsDeposit objDeposit = new ClsDeposit();
				objDeposit.Confirm(objWrapper, objDepositDetailsInfo.DEPOSIT_ID);
				objDeposit.Dispose();
				
				objWrapper.CommitTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				
				return intRetVal;
			}
			catch (Exception objExp)
			{
				objWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				throw objExp;
			}
			finally
			{
				objWrapper.Dispose();
			}
		}

		/// <summary>
		/// Update the information to the database
		/// </summary>
		/// <param name="objDepositDetailsInfo">Model object</param>
		/// <returns>1 if saves sucessfull else error code</returns>
		public int UpdateAgencyDepositLineItems(ClsDepositDetailsInfo objDepositDetailsInfo)
		{
			DataWrapper objWrapper;
			objWrapper = new DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure, 
				DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
			
			int intRetVal;
			
			try
			{
				intRetVal = UpdateDeposit(objWrapper, objDepositDetailsInfo);
						
				if (intRetVal <= 0 )
				{
					objWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
					return -1;
				}
					
				//Confirming the deposit 
				ClsDeposit objDeposit = new ClsDeposit();
				objDeposit.Confirm(objWrapper, (objDepositDetailsInfo.DEPOSIT_ID));
				objDeposit.Dispose();
				
				objWrapper.CommitTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				
				return intRetVal;
			}
			catch (Exception objExp)
			{
				objWrapper.RollbackTransaction(DataLayer.DataWrapper.CloseConnection.YES);
				throw objExp;
			}
			finally
			{
				objWrapper.Dispose();
			}
		}

		/// <summary>
		/// Returns the xml of line items of any deposit
		/// </summary>
		/// <param name="DepositID">Id of the deposit</param>
		/// <returns>String representing the xml</returns>
		public string GetXml(int DepositID)
		{
			try
			{
				/*Calling the stored procedure to get the maximum Journal entry no*/
				String strStoredProc = "Proc_GETACT_CURRENT_DEPOSIT_LINE_ITEMS_XML";
				
				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			
				objDataWrapper.AddParameter("@DEPOSIT_ID", DepositID);

				DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);

				objDataWrapper.ClearParameteres();
				objDataWrapper.Dispose();
				
				if (ds.Tables[0].Rows.Count > 0)
					return ds.GetXml();
				else
					return "";
			}
			catch (Exception objEx)
			{
				throw(objEx);
			}
		}


		/// <summary>
		/// Gets the dataset of records of agency open items for the specified agency and month
		/// </summary>
		/// <param name="intAgencyId">Id of agency</param>
		/// <param name="intMonth">Month Number 1 means jan, 2 means feb</param>
		/// <returns></returns>, 
		public DataSet GetAgencyOpenItems(int intAgencyId, int intMonth, int intYear, int LineItemId)
		{
			try
			{
				/*Calling the stored procedure to get the maximum Journal entry no*/
				String strStoredProc = "Proc_Get_ACT_AGENCY_OPEN_ITEMS";
				
				DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
			
				objDataWrapper.AddParameter("@AGENCY_ID", intAgencyId);
				objDataWrapper.AddParameter("@MONTH", intMonth);
				objDataWrapper.AddParameter("@YEAR", intYear);
				objDataWrapper.AddParameter("@CD_LINE_ITEM_ID", LineItemId);

				DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);

				return ds;
			}
			catch(Exception objExp)
			{
				throw(objExp);
			}
		}

		// Used in Customer deposits to fetch the max of line item id to be further used as page_id
		public static int GetMaxLineItemID()
		{
			try
			{
				String strQuery = "SELECT  ISNULL(MAX(ISNULL(CD_LINE_ITEM_ID,0)),0) FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS (NOLOCK)";
				SqlDataAdapter tmpDA = new SqlDataAdapter(strQuery,ConnStr);	
				DataSet ds = new DataSet();
				tmpDA.Fill(ds);
				return int.Parse(ds.Tables[0].Rows[0][0].ToString());	
			}
			catch
			{
				throw ( new Exception("Error occured. Please try again !."));
			}
		}
		//RTL LOG
		public void LogRTLProcess(int depositID, string polNumber, string desc , string status, string filename, string addInfo, string strAmount)
		{

			string	strStoredProc	=	"Proc_RTL_PROCESS_LOG";
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);

			int returnResult = 0;
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			objDataWrapper.AddParameter("@DEPOSIT_ID", depositID);
			objDataWrapper.AddParameter("@POLICY_NUMBER", polNumber);
			objDataWrapper.AddParameter("@ACTIVITY_DESCRIPTION", desc);
			objDataWrapper.AddParameter("@STATUS", status);
            objDataWrapper.AddParameter("@FILE_NAME", filename);
			objDataWrapper.AddParameter("@ADDITIONAL_INFO", addInfo);
			objDataWrapper.AddParameter("@AMOUNT", strAmount);

			returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

			objDataWrapper.ClearParameteres();
			objDataWrapper.Dispose();
			
		}
		#region CHECK FOR VALID DATA IN RTL FILE
		//IsRTLAmountValid().
		public bool IsRTLAmountValid(string amount)
		{
			try
			{
				string strAmount		=   amount.Substring(8,7);
				int strAmountLen		=	strAmount.Length;
				strAmount				=	strAmount.Insert(strAmountLen - 2,".");
				Convert.ToDouble(strAmount);
				return true;
			} 
			catch 
			{
				return false;
			}
		} 
		//IS  RTL_GROUP_NUMBER
		public bool IsRTLGroupNumber(string groupNumber)
		{
			try
			{
				groupNumber.Substring(25,8).ToString();
				return true;
			} 
			catch 
			{
				return false;
			}
		} 
      //IS RTL_BATCH_NUMBER
		public bool IsRTLBatchNumber(string batchNumber)
		{
			try
			{
				batchNumber.Substring(17,8).ToString();				
				return true;
			} 
			catch 
			{
				return false;
			}
		} 
    
		
		//IsRTLPolicyValid()
		public bool IsRTLPolicyValid(string policy)
		{
			try
			{
				policy.Substring(0,8).ToString();
				return true;
			} 
			catch 
			{
				return false;
			}
		} 
		#endregion


        #region Add Deposit Details Added by Pradeep Kushwaha on 25-oct-2010
        
        public int ImportRTLFileForOurNumber(ArrayList objArrLst,int UserId)
        {

            //Cms.EbixDataLayer.DataWrapper objWrapper = new Cms.EbixDataLayer.DataWrapper(ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
            Cms.EbixDataLayer.DataWrapper objWrapper = new Cms.EbixDataLayer.DataWrapper(ConnStr, CommandType.StoredProcedure, Cms.EbixDataLayer.DataWrapper.MaintainTransaction.YES, Cms.EbixDataLayer.DataWrapper.SetAutoCommit.OFF);


            int intRetVal = 0;
            int i = 0;
            int temp = 0;
            int currentpage = 18;
            int intRandNum = ClsDepositDetails.GetMaxLineItemID();
            try
            {
                DataSet ds = GetSysParm();
                double TOLERANCE_LIMIT = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    TOLERANCE_LIMIT = Convert.ToDouble(ds.Tables[0].Rows[0]["BOLETO_TOLERANCE_LIMIT"].ToString());

                }
                //double TOLERANCE_LIMIT = Convert.ToDouble(GetSysParm());//Commented for itrack-913/966 by pradeep Kushwaha 2-May-2011
                //double TOLERANCE_LIMIT = Convert.ToDouble(0);

                for (int CountList = 0; CountList < objArrLst.Count; CountList++)
                {
                    if (i >= currentpage)
                    {
                        intRandNum = ClsDepositDetails.GetMaxLineItemID();
                        i = 0;
                    }
                    ClsAddDepositDetailsinfo objAddDepositDetailsinfo = (ClsAddDepositDetailsinfo)objArrLst[CountList];
                    
                    objAddDepositDetailsinfo.PAGE_ID.CurrentValue = intRandNum.ToString();
                    //Added by Pradeep Kushwaha -itrack#1722/TFS#1890
                    //Only records with Record ID "1" must be regarded and uploaded
                    if (objAddDepositDetailsinfo.PAYMENT_DATE.CurrentValue <= DateTime.Now && objAddDepositDetailsinfo.RECORD_ID.CurrentValue==1)//Added for itrack 1506
                    {
                        if (objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue != 0.0)
                        {
                            String[] SplitOurNumberDetails = this.GetInstallmentDetailsUsingOurNumber(objAddDepositDetailsinfo.OUR_NUMBER.CurrentValue).Split('^');

                            #region Our Number Validation details
                            if (SplitOurNumberDetails[0] != "" && SplitOurNumberDetails.Length > 0)
                            {
                                if (SplitOurNumberDetails[9].ToString().Trim() != "")
                                {
                                    #region Commnet for itrack No tolrence Limit check is required
                                    double receiptAmount = objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue;

                                    objAddDepositDetailsinfo.RISK_PREMIUM.CurrentValue = double.Parse(SplitOurNumberDetails[3]);

                                    double TotalAmount = double.Parse(SplitOurNumberDetails[7]);
                                    double DEF_VALUE = receiptAmount - TotalAmount;

                                    double absDEF_VALUE = Math.Abs(DEF_VALUE);
                                    if (absDEF_VALUE > TOLERANCE_LIMIT)
                                    {
                                        objAddDepositDetailsinfo.IS_EXCEPTION.CurrentValue = "Y";
                                        objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue = 403;//403-Change in Original Receipt Amount. 292;//292-Beyond Tolerance Limit
                                    }
                                    else
                                        objAddDepositDetailsinfo.IS_EXCEPTION.CurrentValue = "N";

                                    if (objAddDepositDetailsinfo.RISK_PREMIUM.CurrentValue == double.MinValue)
                                    {
                                        objAddDepositDetailsinfo.RISK_PREMIUM.CurrentValue = Convert.ToDouble(null);
                                        objAddDepositDetailsinfo.RISK_PREMIUM.CurrentValue += DEF_VALUE;
                                    }
                                    else
                                        objAddDepositDetailsinfo.RISK_PREMIUM.CurrentValue += DEF_VALUE;
                                    #endregion

                                    //objAddDepositDetailsinfo.IS_EXCEPTION.CurrentValue = "N";
                                    //objAddDepositDetailsinfo.RISK_PREMIUM.CurrentValue = double.Parse(SplitOurNumberDetails[3]);
                                    objAddDepositDetailsinfo.CUSTOMER_ID.CurrentValue = int.Parse(SplitOurNumberDetails[0]);
                                    objAddDepositDetailsinfo.POLICY_ID.CurrentValue = int.Parse(SplitOurNumberDetails[1]);
                                    objAddDepositDetailsinfo.POLICY_VERSION_ID.CurrentValue = int.Parse(SplitOurNumberDetails[2]);
                                    objAddDepositDetailsinfo.FEE.CurrentValue = double.Parse(SplitOurNumberDetails[4]);
                                    objAddDepositDetailsinfo.TAX.CurrentValue = double.Parse(SplitOurNumberDetails[5]);
                                    objAddDepositDetailsinfo.INTEREST.CurrentValue = double.Parse(SplitOurNumberDetails[6]);
                                    objAddDepositDetailsinfo.BOLETO_NO.CurrentValue = int.Parse(SplitOurNumberDetails[9]);
                                    objAddDepositDetailsinfo.OUR_NUMBER.CurrentValue = SplitOurNumberDetails[8];
                                    objAddDepositDetailsinfo.INSTALLMENT_NO.CurrentValue = int.Parse(SplitOurNumberDetails[11]);
                                    objAddDepositDetailsinfo.POLICY_NUMBER.CurrentValue = SplitOurNumberDetails[12];

                                    //if (objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue > double.Parse(SplitOurNumberDetails[7]) ||
                                    //    objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue < double.Parse(SplitOurNumberDetails[7]) )
                                    //{
                                    //    objAddDepositDetailsinfo.IS_EXCEPTION.CurrentValue = "Y";
                                    //    objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue = 403;//Change in Original Receipt Amount.
                                    //}
                                    objAddDepositDetailsinfo.IS_ACTIVE.CurrentValue = "Y";
                                    if (SplitOurNumberDetails[10].Trim().ToString() == "N")
                                    {
                                        objAddDepositDetailsinfo.IS_EXCEPTION.CurrentValue = "Y";
                                        objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue = 293;////Already cancelled Boleto
                                        objAddDepositDetailsinfo.IS_ACTIVE.CurrentValue = "N";
                                    }

                                    if (objAddDepositDetailsinfo.LATE_FEE.CurrentValue == double.MinValue)
                                        objAddDepositDetailsinfo.LATE_FEE.CurrentValue = Convert.ToDouble(null);

                                    intRetVal = 1;

                                }
                                else
                                    intRetVal = -2;
                            }
                            else
                                intRetVal = -2;

                            #endregion

                            if (intRetVal == 1 && intRetVal != 0)
                            {
                                if (objAddDepositDetailsinfo.RequiredTransactionLog)
                                {
                                    if (CountList == 0)
                                        objAddDepositDetailsinfo.CALLED_FROM.CurrentValue = "RTL";
                                    else
                                        objAddDepositDetailsinfo.CALLED_FROM.CurrentValue = String.Empty;

                                    intRetVal = this.AddDepositLineItemsInfo(objAddDepositDetailsinfo, objWrapper);
                                    intRetVal = 1;
                                    objWrapper.ClearParameteres();
                                }
                            }
                        }//if (objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue != 0.0)
                        else
                            intRetVal = -2; //Invalid Record
                    }//if (objAddDepositDetailsinfo.PAYMENT_DATE.CurrentValue <= DateTime.Now)
                    else
                        intRetVal = -2; //Payment Date can't be future date  
                    
                    //Loging RTL Process
                    if (intRetVal == 1) //Successful Our Number
                    {
                        i++;//Increment Counter only if the Import is successful
                        LogRTLProcessForOurNumber(objAddDepositDetailsinfo.DEPOSIT_ID.CurrentValue, 
                            objAddDepositDetailsinfo.OUR_NUMBER.CurrentValue,
                             ClsCommon.FetchGeneralMessage("1880", ""), ClsCommon.FetchGeneralMessage("1879", ""), "", "", 
                            objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue, 
                            objAddDepositDetailsinfo.LATE_FEE.CurrentValue,
                            objAddDepositDetailsinfo.POLICY_NUMBER.CurrentValue, 397, 398, 398, objAddDepositDetailsinfo);
                        
                    }
                    else if (intRetVal == -2) //Invalid Our Number
                    {
                        this.SetValueInModel(objAddDepositDetailsinfo);//Added by Pradeep Kushwaha on 13-April-2011
                      
                        //Added by Pradeep Kushwaha -itrack#1722/TFS#1890
                        //Only records with Record ID "1" must be regarded and uploaded
                        if (objAddDepositDetailsinfo.RECORD_ID.CurrentValue==0)
                        {   //448-'Invalid Record'
                            LogRTLProcessForOurNumber(objAddDepositDetailsinfo.DEPOSIT_ID.CurrentValue,
                                                           objAddDepositDetailsinfo.OUR_NUMBER.CurrentValue.ToString(),
                                                            "", ClsCommon.FetchGeneralMessage("1878", ""), "",
                                                            "",
                                                           objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue,
                                                           objAddDepositDetailsinfo.LATE_FEE.CurrentValue, "", 396, 448, 448, objAddDepositDetailsinfo);
                        }//if (objAddDepositDetailsinfo.RECORD_ID.CurrentValue==0)
                        //Added for itrack- 1506 on 10-Aug-2011 - Payment date con't be future date
                        else if (objAddDepositDetailsinfo.PAYMENT_DATE.CurrentValue > DateTime.Now)
                        {   //437-Receipt date cannot be future date. 
                            LogRTLProcessForOurNumber(objAddDepositDetailsinfo.DEPOSIT_ID.CurrentValue,
                                                           objAddDepositDetailsinfo.OUR_NUMBER.CurrentValue.ToString(),
                                                            "", ClsCommon.FetchGeneralMessage("1878", ""), "",
                                                            "",
                                                           objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue,
                                                           objAddDepositDetailsinfo.LATE_FEE.CurrentValue, "", 396, 437, 437, objAddDepositDetailsinfo);
                        }//if (objAddDepositDetailsinfo.PAYMENT_DATE.CurrentValue <= DateTime.Now)
                        else if (objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue == 0.0) //Update Log if 0.0 AMount is not valid in RTL File
                            LogRTLProcessForOurNumber(objAddDepositDetailsinfo.DEPOSIT_ID.CurrentValue,
                                objAddDepositDetailsinfo.OUR_NUMBER.CurrentValue.ToString(),
                                 ClsCommon.FetchGeneralMessage("1881", ""), ClsCommon.FetchGeneralMessage("1878", ""), "",
                                 ClsCommon.FetchGeneralMessage("1881", ""),
                                objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue,
                                objAddDepositDetailsinfo.LATE_FEE.CurrentValue, "", 396, 399, 399, objAddDepositDetailsinfo);
                        else
                        {
                            LogRTLProcessForOurNumber(objAddDepositDetailsinfo.DEPOSIT_ID.CurrentValue,
                                objAddDepositDetailsinfo.OUR_NUMBER.CurrentValue.ToString(),
                                ClsCommon.FetchGeneralMessage("1877", ""), ClsCommon.FetchGeneralMessage("1878", ""), "", ClsCommon.FetchGeneralMessage("1882", ""),
                                objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue,
                                objAddDepositDetailsinfo.LATE_FEE.CurrentValue, "", 396, 400, 395, objAddDepositDetailsinfo);
                            if (objAddDepositDetailsinfo.RequiredTransactionLog)
                            {
                                if (CountList == 0)
                                    objAddDepositDetailsinfo.CALLED_FROM.CurrentValue = "RTL";
                                else
                                    objAddDepositDetailsinfo.CALLED_FROM.CurrentValue = String.Empty;

                                intRetVal = this.AddDepositLineItemsInfo(objAddDepositDetailsinfo, objWrapper);
                                objWrapper.ClearParameteres();
                            }
                        }
                        
                    }
                    objAddDepositDetailsinfo.Dispose();
                    //End Log process
                    if (intRetVal <= 0)
                    {
                        //break;
                        if (intRetVal == -2)
                            temp++;
                        if (temp == objArrLst.Count)
                            intRetVal = -3;//Nothing Imported
                        else
                            intRetVal = -2;//Partial Imported	
                        
                    }
                    else
                        continue;
                  
                  
                }
                objWrapper.CommitTransaction(Cms.EbixDataLayer.DataWrapper.CloseConnection.YES);
                if (temp > 0 && intRetVal != -3) //Our Number Does not exists
                    return -2;
                else if (intRetVal == -3)//Nothing Imported
                    return -3;
                else
                    return 1;//Fully Imported

            }
            catch (Exception objExp)
            {
                objWrapper.RollbackTransaction(Cms.EbixDataLayer.DataWrapper.CloseConnection.YES);
                throw (objExp);
            }
        }
        //Added by Pradeep Kushwaha on 13-April-2011 for i-Track-913 and 966
        /// <summary>
        /// To Set value in model class's property while importing not found OurNumber
        /// </summary>
        /// <param name="objAddDepositDetailsinfo"></param>
        private void SetValueInModel(ClsAddDepositDetailsinfo objAddDepositDetailsinfo)
        {
            
            if (objAddDepositDetailsinfo.RISK_PREMIUM.CurrentValue == double.MinValue)
                objAddDepositDetailsinfo.RISK_PREMIUM.CurrentValue = Convert.ToDouble(null);
            if (objAddDepositDetailsinfo.TOTAL_PREMIUM_COLLECTION.CurrentValue == double.MinValue)
                objAddDepositDetailsinfo.TOTAL_PREMIUM_COLLECTION.CurrentValue = Convert.ToDouble(null);
            
            if (objAddDepositDetailsinfo.FEE.CurrentValue == double.MinValue)
                objAddDepositDetailsinfo.FEE.CurrentValue = Convert.ToDouble(null);
            if (objAddDepositDetailsinfo.TAX.CurrentValue == double.MinValue)
                objAddDepositDetailsinfo.TAX.CurrentValue = Convert.ToDouble(null);
            if (objAddDepositDetailsinfo.INTEREST.CurrentValue == double.MinValue)
                objAddDepositDetailsinfo.INTEREST.CurrentValue = Convert.ToDouble(null);

            if (objAddDepositDetailsinfo.LATE_FEE.CurrentValue == double.MinValue)
                objAddDepositDetailsinfo.LATE_FEE.CurrentValue = Convert.ToDouble(null);
            if (objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue == double.MinValue)
                objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue = Convert.ToDouble(null);

           
            if (objAddDepositDetailsinfo.CUSTOMER_ID.CurrentValue == int.MinValue)
                objAddDepositDetailsinfo.CUSTOMER_ID.CurrentValue = Convert.ToInt32(null);
            if (objAddDepositDetailsinfo.POLICY_ID.CurrentValue == int.MinValue)
                objAddDepositDetailsinfo.POLICY_ID.CurrentValue = Convert.ToInt32(null);
            if (objAddDepositDetailsinfo.POLICY_VERSION_ID.CurrentValue == int.MinValue)
                objAddDepositDetailsinfo.POLICY_VERSION_ID.CurrentValue = Convert.ToInt32(null);
            
           
            objAddDepositDetailsinfo.IS_ACTIVE.CurrentValue = "N";


            objAddDepositDetailsinfo.IS_EXCEPTION.CurrentValue = "Y";
            objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue = 395; //Our Number Not Found in Database

        

        }
        /// <summary>
        /// Use to Import Co-Insurance Data from list one bye one 
        /// </summary>
        /// <param name="objArrLst"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        //Added by Pradeep Kushwaha on 12-Jan-2011
        public int ImportRTLFileOfCoInsurance(ArrayList objArrLst, int UserId)
        {
            Cms.EbixDataLayer.DataWrapper objWrapper = new Cms.EbixDataLayer.DataWrapper(ConnStr, CommandType.StoredProcedure, Cms.EbixDataLayer.DataWrapper.MaintainTransaction.YES, Cms.EbixDataLayer.DataWrapper.SetAutoCommit.OFF);

            int intRetVal = 0;
            int i = 0;
            int temp = 0;
            int currentpage = 18;
            int intRandNum = ClsDepositDetails.GetMaxLineItemID();
            try
            {
                DataSet ds = GetSysParm();
                double TOLERANCE_LIMIT_PERCENTAGE = 0;
                double TOLERANCE_LIMIT_AMOUNT = 0;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    TOLERANCE_LIMIT_PERCENTAGE = Convert.ToDouble(ds.Tables[0].Rows[0]["COACC_TOLERANCE_LIMIT_PERCENTAGE"].ToString());
                    TOLERANCE_LIMIT_AMOUNT = Convert.ToDouble(ds.Tables[0].Rows[0]["COACC_TOLERANCE_LIMIT_AMOUNT"].ToString());

                }
                for (int CountList = 0; CountList < objArrLst.Count; CountList++)
                {
                    int iProcess = 0;//Added to check the process 
                    if (i >= currentpage)
                    {
                        intRandNum = ClsDepositDetails.GetMaxLineItemID();
                        i = 0;
                    }
                    ClsAddDepositDetailsinfo objAddDepositDetailsinfo = (ClsAddDepositDetailsinfo)objArrLst[CountList];

                    objAddDepositDetailsinfo.PAGE_ID.CurrentValue = intRandNum.ToString();
                    if (objAddDepositDetailsinfo.PAYMENT_DATE.CurrentValue <= DateTime.Now)//Added for itrack 1506 to check payment date cannot be future date
                    {
                        if (objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue != 0.0)
                        {
                            DataSet dsLeaderPolicyDetails = this.GetLeaderPolicyDetails(objAddDepositDetailsinfo.LEADER_POLICY_ID.CurrentValue.ToString(),
                                objAddDepositDetailsinfo.LEADER_DOC_ID.CurrentValue.ToString(),
                                objAddDepositDetailsinfo.INSTALLMENT_NO.CurrentValue,
                                "COINS_IMPORT");
                            if (dsLeaderPolicyDetails != null && dsLeaderPolicyDetails.Tables[0].Rows.Count > 0)
                            {
                                if (dsLeaderPolicyDetails.Tables[0].Rows[0]["LEADER_POLICY_NUMBER"].ToString().Trim() == objAddDepositDetailsinfo.LEADER_POLICY_ID.CurrentValue.ToString().Trim())
                                {
                                    objAddDepositDetailsinfo.CUSTOMER_ID.CurrentValue = int.Parse(dsLeaderPolicyDetails.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
                                    objAddDepositDetailsinfo.POLICY_ID.CurrentValue = int.Parse(dsLeaderPolicyDetails.Tables[0].Rows[0]["POLICY_ID"].ToString());
                                    objAddDepositDetailsinfo.POLICY_VERSION_ID.CurrentValue = int.Parse(dsLeaderPolicyDetails.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
                                    objAddDepositDetailsinfo.RECEIPT_FROM_ID.CurrentValue = int.Parse(dsLeaderPolicyDetails.Tables[0].Rows[0]["COINSURANCE_ID"].ToString());

                                    DataSet dsInstall = null;
                                    dsInstall = this.GetInstallmentDetails(objAddDepositDetailsinfo.CUSTOMER_ID.CurrentValue,
                                        objAddDepositDetailsinfo.POLICY_ID.CurrentValue,
                                        objAddDepositDetailsinfo.POLICY_VERSION_ID.CurrentValue,
                                        objAddDepositDetailsinfo.INSTALLMENT_NO.CurrentValue,
                                                   "COINS_DEPOSIT");
                                    //Modifed by Pradeep Kushwaha as itrack 1148/1363 as discussed with Anurag  
                                    double NET_INSTALLMENT_AMOUNT = 0;
                                    if (dsInstall != null && dsInstall.Tables[0].Rows.Count > 0)
                                    {
                                        //-- Co Commission Amt - itrack 1148 as discussed with Anurag      
                                        double CO_COMM_AMT = 0;
                                        CO_COMM_AMT = Convert.ToDouble(dsInstall.Tables[0].Rows[0]["CO_COMM_AMT"]);

                                        NET_INSTALLMENT_AMOUNT = (Convert.ToDouble(dsInstall.Tables[0].Rows[0]["INSTALLMENT_AMOUNT"]) - CO_COMM_AMT
                                             + Convert.ToDouble(dsInstall.Tables[0].Rows[0]["INTEREST_AMOUNT"]));

                                        double receiptAmount = objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue;

                                        double DEF_VALUE = receiptAmount - NET_INSTALLMENT_AMOUNT;
                                        double absDEF_VALUE = Math.Abs(DEF_VALUE);

                                        double TOLERANCE_LIMIT_PERCENTAGE_AMOUNT = (NET_INSTALLMENT_AMOUNT * TOLERANCE_LIMIT_PERCENTAGE / 100);

                                        if ((absDEF_VALUE > TOLERANCE_LIMIT_AMOUNT) || (absDEF_VALUE > TOLERANCE_LIMIT_PERCENTAGE_AMOUNT))
                                        {
                                            objAddDepositDetailsinfo.IS_EXCEPTION.CurrentValue = "Y";
                                            objAddDepositDetailsinfo.EXCEPTION_REASON.CurrentValue = 409;//Change in Original Net Installment Amount.
                                        }
                                        else
                                            objAddDepositDetailsinfo.IS_EXCEPTION.CurrentValue = "N";

                                        if (objAddDepositDetailsinfo.RequiredTransactionLog)
                                        {
                                            intRetVal = this.AddDepositLineItemsInfo(objAddDepositDetailsinfo, objWrapper);
                                            intRetVal = 1;
                                            objWrapper.ClearParameteres();
                                        }

                                    }
                                    else
                                    {
                                        intRetVal = -2;
                                        iProcess = -3;//Process is not committed in the system.
                                    }


                                }
                                else
                                {
                                    intRetVal = -2;
                                    iProcess = -2;//Invalid Leader Policy Number
                                }
                            }
                            else
                            {
                                intRetVal = -2;
                                iProcess = -2;//Invalid Leader Policy Number
                            }

                        }//if (objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue != 0.0)
                        else
                        {
                            intRetVal = -2; //Invalid Record
                            iProcess = -4;//Invalid Amount Format
                        }
                    } //if (objAddDepositDetailsinfo.PAYMENT_DATE.CurrentValue <= DateTime.Now)
                    else
                    {
                        intRetVal = -2; //Invalid Record
                        iProcess = -5;//Receipt date cannot be future date. -itrack 1506
                    }

                    //Loging RTL Process
                    if (intRetVal == 1) //Successful Added 
                    {
                        i++;//Increment Counter only if the Import is successful
                        SetMinValue(objAddDepositDetailsinfo);
                        LogRTLProcessoCoInsurance(ClsCommon.FetchGeneralMessage("1979", ""), ClsCommon.FetchGeneralMessage("1879", ""), "", "", objAddDepositDetailsinfo, 397, 405, 405);

                    }
                    else if (intRetVal == -2) //Invalid Amount Format
                    {
                        //Modified by Pradeep as discussed by Anurag itrack - 1148 /1363
                        SetMinValue(objAddDepositDetailsinfo);

                        if (iProcess == -4)//Invalid Amount Format
                            LogRTLProcessoCoInsurance(ClsCommon.FetchGeneralMessage("1881", ""), ClsCommon.FetchGeneralMessage("1878", ""), "", ClsCommon.FetchGeneralMessage("1881", ""), objAddDepositDetailsinfo, 396, 399, 399);
                        else if (iProcess == -5)//437-Receipt date cannot be future date. -itrack 1506
                            LogRTLProcessoCoInsurance("", ClsCommon.FetchGeneralMessage("1878", ""), "", ClsCommon.FetchGeneralMessage("1881", ""), objAddDepositDetailsinfo, 396, 437, 437);
                        else if (iProcess == -3)//Process is not committed in the system.
                            LogRTLProcessoCoInsurance("", ClsCommon.FetchGeneralMessage("1878", ""), "", "", objAddDepositDetailsinfo, 396, 399, 433);
                        else if (iProcess == -2)//Invalid Leader Policy Number
                            LogRTLProcessoCoInsurance(ClsCommon.FetchGeneralMessage("1978", ""), ClsCommon.FetchGeneralMessage("1878", ""), "", ClsCommon.FetchGeneralMessage("1978", ""), objAddDepositDetailsinfo, 396, 404, 404);

                        #region Comment Log exception for itrack 1148, to maintain proper log exception
                        //if (objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue == 0.0) //Update Log if 0.0 AMount is not valid in RTL File
                        //{
                        //    SetMinValue(objAddDepositDetailsinfo);
                        //    LogRTLProcessoCoInsurance(ClsCommon.FetchGeneralMessage("1881", ""), ClsCommon.FetchGeneralMessage("1878", ""), "", ClsCommon.FetchGeneralMessage("1881", ""), objAddDepositDetailsinfo, 396, 399, 399);                           

                        //}
                        //else
                        //{
                        //    SetMinValue(objAddDepositDetailsinfo);
                        //    LogRTLProcessoCoInsurance(ClsCommon.FetchGeneralMessage("1978", ""), ClsCommon.FetchGeneralMessage("1878", ""), "", ClsCommon.FetchGeneralMessage("1978", ""), objAddDepositDetailsinfo, 396, 404, 404);
                        //}
                        #endregion
                        //Till here 

                    }
                    objAddDepositDetailsinfo.Dispose();
                    //End Log process
                    if (intRetVal <= 0)
                    {
                        //break;
                        if (intRetVal == -2)
                            temp++;
                        if (temp == objArrLst.Count)
                            intRetVal = -3;//Nothing Imported
                        else
                            intRetVal = -2;//Partial Imported	

                    }
                    else
                        continue;


                }
                objWrapper.CommitTransaction(Cms.EbixDataLayer.DataWrapper.CloseConnection.YES);
                if (temp > 0 && intRetVal != -3) //Our Number Does not exists
                    return -2;
                else if (intRetVal == -3)//Nothing Imported
                    return -3;
                else
                    return 1;//Fully Imported

            }
            catch (Exception objExp)
            {
                objWrapper.RollbackTransaction(Cms.EbixDataLayer.DataWrapper.CloseConnection.YES);
                throw (objExp);
            }
        }

        //Added by Pradeep Kushwaha on 13-May-2011
        public DataSet GetLeaderPolicyDetails(String LEADER_POLICY_NUMBER,String CO_ENDORSEMENT_NO,Int32 INSTALLMENT_NO,String CALLED_FROM)
        {
            try
            {
                /*Calling the stored procedure*/
                String strStoredProc = "Proc_GetLeaderPolicyDetails";

                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

                objDataWrapper.AddParameter("@LEADER_POLICY_NUMBER", LEADER_POLICY_NUMBER);
                objDataWrapper.AddParameter("@CO_ENDORSEMENT_NO", CO_ENDORSEMENT_NO);
                objDataWrapper.AddParameter("@INSTALLMENT_NO", INSTALLMENT_NO);
                objDataWrapper.AddParameter("@CALLED_FROM", CALLED_FROM);
                
                DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);

                objDataWrapper.ClearParameteres();
                objDataWrapper.Dispose();
                return ds;
                
            }
            catch (Exception objEx)
            {
                throw (objEx);
            }
        }
        //get Intallment Details
        public DataSet GetInstallmentDetails(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID, int INSTALLMENT_NO, String CALLED_FROM)
        {
            try
            {
                /*Calling the stored procedure*/
                String strStoredProc = "Proc_GetInstallmentDetails";

                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

                objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
                objDataWrapper.AddParameter("@POLICY_ID", POLICY_ID);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
                objDataWrapper.AddParameter("@CALLED_FROM", CALLED_FROM);
                objDataWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID);
                if(INSTALLMENT_NO!=0)
                objDataWrapper.AddParameter("@INSTALLMENT_NO", INSTALLMENT_NO);
                else
                    objDataWrapper.AddParameter("@INSTALLMENT_NO", System.DBNull.Value);

                DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);

                objDataWrapper.ClearParameteres();
                objDataWrapper.Dispose();
                return ds;

            }
            catch (Exception objEx)
            {
                throw (objEx);
            }
        }
        //get Intallment Details
        public DataSet GetInstallmentDetailsForCoInsurance(String LEADER_POLICY_NUMBER, String @CALLED_FOR)
        {
            try
            {
                /*Calling the stored procedure*/
                String strStoredProc = "Proc_GetIntallmentDetailsforDepositItem";

                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

                objDataWrapper.AddParameter("@POLICY_NUMBER", LEADER_POLICY_NUMBER);
                objDataWrapper.AddParameter("@CALLED_FOR", @CALLED_FOR);
                objDataWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID);

                DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);

                objDataWrapper.ClearParameteres();
                objDataWrapper.Dispose();
                return ds;

            }
            catch (Exception objEx)
            {
                throw (objEx);
            }
        }
        //Added till here 
        private void SetMinValue(ClsAddDepositDetailsinfo objAddDepositInfo)
        {
            if (objAddDepositInfo.RECEIPT_AMOUNT.CurrentValue == double.MinValue)
                objAddDepositInfo.RECEIPT_AMOUNT.CurrentValue = Convert.ToDouble(null);
            if (objAddDepositInfo.LATE_FEE.CurrentValue == double.MinValue)
                objAddDepositInfo.LATE_FEE.CurrentValue = Convert.ToDouble(null);

           
        }
        public void LogRTLProcessForOurNumber(int depositID, string OurNumber, string desc,
            string status, string filename, string addInfo, double strAmount, double strLatefee,
            string POLICY_NUMBER, int STATUS_ID, int ADDITIONAL_INFO_ID, int ACTIVITY_DESCRIPTION_ID, ClsAddDepositDetailsinfo objAddDepositDetailsinfo)
        {

            string strStoredProc = "Proc_RTL_PROCESS_LOG";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            int returnResult = 0;
            SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
            objDataWrapper.AddParameter("@DEPOSIT_ID", depositID);
            objDataWrapper.AddParameter("@OUR_NUMBER", OurNumber);
            objDataWrapper.AddParameter("@ACTIVITY_DESCRIPTION", desc);
            objDataWrapper.AddParameter("@STATUS", status);
            objDataWrapper.AddParameter("@FILE_NAME", filename);
            objDataWrapper.AddParameter("@ADDITIONAL_INFO", addInfo);
            objDataWrapper.AddParameter("@AMOUNT", strAmount);
            objDataWrapper.AddParameter("@LATE_FEE", strLatefee);
            objDataWrapper.AddParameter("@POLICY_NUMBER", POLICY_NUMBER);
            objDataWrapper.AddParameter("@STATUS_ID", STATUS_ID);
            objDataWrapper.AddParameter("@ADDITIONAL_INFO_ID", ADDITIONAL_INFO_ID);
            objDataWrapper.AddParameter("@ACTIVITY_DESCRIPTION_ID", ACTIVITY_DESCRIPTION_ID);
            objDataWrapper.AddParameter("@PAYMENT_DATE", objAddDepositDetailsinfo.PAYMENT_DATE.CurrentValue);//Added to insert payment date - itrack 1506 -
            
            
            returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

            objDataWrapper.ClearParameteres();
            objDataWrapper.Dispose();

        }
        /// <summary>
        /// Use to create RTL Log History of Co-Insurance Data
        /// </summary>
        /// <param name="desc"></param>
        /// <param name="status"></param>
        /// <param name="filename"></param>
        /// <param name="addInfo"></param>
        /// <param name="objAddDepositDetailsinfo"></param>
        //Added by Pradeep Kushwaha on 12-Jan-2011
        public void LogRTLProcessoCoInsurance(string desc, string status, string filename,string addInfo,  ClsAddDepositDetailsinfo objAddDepositDetailsinfo,
             int STATUS_ID, int ADDITIONAL_INFO_ID, int ACTIVITY_DESCRIPTION_ID)
        {

            string strStoredProc = "Proc_RTL_PROCESS_LOG";
            DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

            int returnResult = 0;

            objDataWrapper.AddParameter("@DEPOSIT_ID", objAddDepositDetailsinfo.DEPOSIT_ID.CurrentValue);
            objDataWrapper.AddParameter("@ACTIVITY_DESCRIPTION", desc);
            objDataWrapper.AddParameter("@STATUS", status);
            objDataWrapper.AddParameter("@FILE_NAME", filename);
            objDataWrapper.AddParameter("@ADDITIONAL_INFO", addInfo);
            objDataWrapper.AddParameter("@AMOUNT", objAddDepositDetailsinfo.RECEIPT_AMOUNT.CurrentValue);

            objDataWrapper.AddParameter("@STATUS_ID", STATUS_ID);
            objDataWrapper.AddParameter("@ADDITIONAL_INFO_ID", ADDITIONAL_INFO_ID);
            objDataWrapper.AddParameter("@ACTIVITY_DESCRIPTION_ID", ACTIVITY_DESCRIPTION_ID);
            

            objDataWrapper.AddParameter("@COINS_CARRIER_LEADER", objAddDepositDetailsinfo.COINS_CARRIER_LEADER.CurrentValue.ToString());
            objDataWrapper.AddParameter("@POLICY_HOLDER_NAME", objAddDepositDetailsinfo.POLICY_HOLDER_NAME.CurrentValue.ToString());
            objDataWrapper.AddParameter("@SUSEP_CLASS_OF_BUSINESS", objAddDepositDetailsinfo.SUSEP_CLASS_OF_BUSINESS.CurrentValue.ToString());
            objDataWrapper.AddParameter("@LEADER_POLICY_ID", objAddDepositDetailsinfo.LEADER_POLICY_ID.CurrentValue.ToString());
            objDataWrapper.AddParameter("@LEADER_DOC_ID", objAddDepositDetailsinfo.LEADER_DOC_ID.CurrentValue.ToString());
            objDataWrapper.AddParameter("@BRANCH_COINS_ID", objAddDepositDetailsinfo.BRANCH_COINS_ID.CurrentValue.ToString());
            objDataWrapper.AddParameter("@COINSURANCE_ID", objAddDepositDetailsinfo.COINSURANCE_ID.CurrentValue.ToString());

            objDataWrapper.AddParameter("@PAYMENT_DATE", objAddDepositDetailsinfo.PAYMENT_DATE.CurrentValue);
            objDataWrapper.AddParameter("@RISK_PREMIUM", objAddDepositDetailsinfo.RISK_PREMIUM.CurrentValue);
            objDataWrapper.AddParameter("@COMMISSION_AMOUNT", objAddDepositDetailsinfo.COMMISSION_AMOUNT.CurrentValue);
            objDataWrapper.AddParameter("@FEE", objAddDepositDetailsinfo.FEE.CurrentValue);
            objDataWrapper.AddParameter("@INTEREST", objAddDepositDetailsinfo.INTEREST.CurrentValue);
            objDataWrapper.AddParameter("@INSTALLMENT_NO", objAddDepositDetailsinfo.INSTALLMENT_NO.CurrentValue);
            objDataWrapper.AddParameter("@NO_OF_INSTALLMENTS", objAddDepositDetailsinfo.NO_OF_INSTALLMENTS.CurrentValue);


            returnResult = objDataWrapper.ExecuteNonQuery(strStoredProc);

            objDataWrapper.ClearParameteres();
            objDataWrapper.Dispose();

        }
        public bool IsRTLOurNumberValid(string OurNumber)
        {
            try
            {
                OurNumber.Substring(18, 12).ToString();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //Added by Pradeep -on 08-Dec-2011 iTrack#1722/TFS#1890
        public bool ValidOurNunber(string OurNumber)
        {
            try
            {
                OurNumber.Substring(62, 12).ToString();
                return true;
            }
            catch
            {
                return false;
            }
        }
       
        public bool IsRTLPaidPremiumnValid(string amount)
        {
            Boolean returnValue = false;
            try
            {
                //Modified by Pradeep -on 08-Dec-2011 iTrack#1722/TFS#1890
                string strAmount = amount.Substring(152, 13);
                int strAmountLen = strAmount.Length;
                if(ClsCommon.BL_LANG_ID==2)
                    strAmount = strAmount.Insert(strAmountLen - 2, ",");
                else
                    strAmount = strAmount.Insert(strAmountLen - 2, ".");
                Convert.ToDouble(strAmount);
                returnValue=true;
            }
            catch(Exception ex)
            {
                returnValue = false;
                throw (new Exception("Error:" + ClsCommon.FetchGeneralMessage("1409", "") + "", ex.InnerException));
                
            }
            return returnValue;
        }
        //Modified by Pradeep -on 08-Dec-2011 iTrack#1722/TFS#1890
        public bool IsRTLFinancial_PenaltyValid(string LateFee)
        {
            try
            {
                string strAmount = LateFee.Substring(266, 13);
                int strAmountLen = strAmount.Length;
                if (ClsCommon.BL_LANG_ID == 2)
                    strAmount = strAmount.Insert(strAmountLen - 2, ",");
                else
                    strAmount = strAmount.Insert(strAmountLen - 2, ".");
                Convert.ToDouble(strAmount);
                return true;
            }
            catch
            {
                return false;
            }
        }
        //Added by Pradeep -on 08-Dec-2011 iTrack#1722/TFS#1890
        public bool IsRTLLateFeeValid(string LateFee)
        {
            try
            {
                string strAmount = LateFee.Substring(279, 13);
                int strAmountLen = strAmount.Length;
                if (ClsCommon.BL_LANG_ID == 2)
                    strAmount = strAmount.Insert(strAmountLen - 2, ",");
                else
                    strAmount = strAmount.Insert(strAmountLen - 2, ".");
                Convert.ToDouble(strAmount);
                return true;
            }
            catch
            {
                return false;
            }
        }
        //Added by Pradeep -on 08-Dec-2011 iTrack#1722/TFS#1890
        public bool IsRTLDeductiblelatefeeValid(string LateFee)
        {
            try
            {
                string strAmount = LateFee.Substring(292, 13);
                int strAmountLen = strAmount.Length;
                if (ClsCommon.BL_LANG_ID == 2)
                    strAmount = strAmount.Insert(strAmountLen - 2, ",");
                else
                    strAmount = strAmount.Insert(strAmountLen - 2, ".");
                Convert.ToDouble(strAmount);
                return true;
            }
            catch
            {
                return false;
            }
        }
        //Modified by Pradeep -on 08-Dec-2011 iTrack#1722/TFS#1890
        public bool IsRTLValidDate(string Date,ref DateTime validDate)
        {
            Boolean returnValue = false;
            try
            {
                string strPAID_DAY = Date.Substring(110, 2);
                string strPAID_MONTH = Date.Substring(112, 2);
                string strPAID_YEAR ="20"+ Date.Substring(114, 2);
                
                validDate = new DateTime(int.Parse(strPAID_YEAR), int.Parse(strPAID_MONTH), int.Parse(strPAID_DAY));
                returnValue = true;
            }
            catch (Exception ex)
            {
                returnValue = false;
                throw (new Exception("Error:" + ClsCommon.FetchGeneralMessage("1410", "") + "", ex.InnerException));

            }
            return returnValue;
        }

        #region V A L I D A T E while I M P O R T I N G Co-Insurance Data
        //Validate Co-Insurance Data while importing Added By Pradeep Kushwaha on 11-jan-2010
        
        public bool ValidateValue(string value,int StartPosition, int Endposition)
        {
            try
            {
                value.Substring(StartPosition, Endposition).ToString();
                return true;
            }
            catch (Exception ex)
            {
                return  false;
                throw (new Exception("Error:" + ClsCommon.FetchGeneralMessage("1411", "") + "", ex.InnerException));
            }
            
        }
        public bool IsRTLPaymentDateValid(string Date, ref DateTime validDate)
        {
            Boolean returnValue = false;
            try
            {
                string strPAID_DAY = Date.Substring(76, 2);
                string strPAID_MONTH = Date.Substring(78, 2);
                string strPAID_YEAR = Date.Substring(80, 4);

                validDate = new DateTime(int.Parse(strPAID_YEAR), int.Parse(strPAID_MONTH), int.Parse(strPAID_DAY));
                returnValue = true;
            }
            catch (Exception ex)
            {
                returnValue = false;
                throw (new Exception("Error: " + ClsCommon.FetchGeneralMessage("1410", "") + "", ex.InnerException));

            }
            return returnValue;
        }
        public bool ValidateNumber(string value, int StartPosition, int Endposition)
        {
            try
            {
                int.Parse(value.Substring(StartPosition, Endposition).ToString().Trim());
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw (new Exception("Error:" + ClsCommon.FetchGeneralMessage("1411", "") + "", ex.InnerException));
            }

        }
        public bool IsPremiumAmountValid(string amount)
        {
            Boolean returnValue = false;
            try
            {
                string strAmount = amount.Substring(84, 13);
                int strAmountLen = strAmount.Length;
                if(ClsCommon.BL_LANG_ID==2)
                    strAmount = strAmount.Insert(strAmountLen - 2, ",");
                else
                    strAmount = strAmount.Insert(strAmountLen - 2, ".");
                Convert.ToDouble(strAmount);
                returnValue=true;
            }
            catch(Exception ex)
            {
                returnValue = false;
                throw (new Exception("Error:" + ClsCommon.FetchGeneralMessage("1412", "") + "", ex.InnerException));
                
            }
            return returnValue;
        }
        public bool IsCommissionAmountValid(string amount)
        {
            Boolean returnValue = false;
            try
            {
                string strAmount = amount.Substring(97, 13);
                int strAmountLen = strAmount.Length;
                if (ClsCommon.BL_LANG_ID == 2)
                    strAmount = strAmount.Insert(strAmountLen - 2, ",");
                else
                    strAmount = strAmount.Insert(strAmountLen - 2, ".");
                Convert.ToDouble(strAmount);
                returnValue = true;
            }
            catch (Exception ex)
            {
                returnValue = false;
                throw (new Exception("Error:" + ClsCommon.FetchGeneralMessage("1413", "") + "", ex.InnerException));

            }
            return returnValue;
        }
        public bool IsCoinsuranceFeeAmountValid(string amount)
        {
            Boolean returnValue = false;
            try
            {
                string strAmount = amount.Substring(110, 13);
                int strAmountLen = strAmount.Length;
                if (ClsCommon.BL_LANG_ID == 2)
                    strAmount = strAmount.Insert(strAmountLen - 2, ",");
                else
                    strAmount = strAmount.Insert(strAmountLen - 2, ".");
                Convert.ToDouble(strAmount);
                returnValue = true;
            }
            catch (Exception ex)
            {
                returnValue = false;
                throw (new Exception("Error:"+ClsCommon.FetchGeneralMessage("1414", "") +"", ex.InnerException));

            }
            return returnValue;
        }
        public bool IsInterestValid(string amount)
        {
            Boolean returnValue = false;
            try
            {
                string strAmount = amount.Substring(123, 13);
                int strAmountLen = strAmount.Length;
                if (ClsCommon.BL_LANG_ID == 2)
                    strAmount = strAmount.Insert(strAmountLen - 2, ",");
                else
                    strAmount = strAmount.Insert(strAmountLen - 2, ".");
                Convert.ToDouble(strAmount);
                returnValue = true;
            }
            catch (Exception ex)
            {
                returnValue = false;
                throw (new Exception("Error:" + ClsCommon.FetchGeneralMessage("1415", "") + "", ex.InnerException));

            }
            return returnValue;
        }
        public bool IsNetPremiumValid(string amount)
        {
            Boolean returnValue = false;
            try
            {
                string strAmount = amount.Substring(136, 13);
                int strAmountLen = strAmount.Length;
                if (ClsCommon.BL_LANG_ID == 2)
                    strAmount = strAmount.Insert(strAmountLen - 2, ",");
                else
                    strAmount = strAmount.Insert(strAmountLen - 2, ".");
                Convert.ToDouble(strAmount);
                returnValue = true;
            }
            catch (Exception ex)
            {
                returnValue = false;
                throw (new Exception("Error:" + ClsCommon.FetchGeneralMessage("1416", "") + "", ex.InnerException));

            }
            return returnValue;
        }
        //Validate Co-Insurance Data while importing Added till here 

        #endregion

        /// <summary>
        /// Get the Policy Status based on Customer ID, Policy id and Policy version id
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="polID"></param>
        /// <param name="polVersionID"></param>
        /// <returns></returns>
        public String GetPolicyStatus(int customerID, int polID, int polVersionID)
        {
            try
            {
                string strStoredProc = "Proc_GetPolicyStatusforDepositItem";
                string polStat = "";
                DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

                objWrapper.AddParameter("@CUSTOMER_ID", customerID);
                objWrapper.AddParameter("@POLICY_ID", polID);
                objWrapper.AddParameter("@POLICY_VERSION_ID", polVersionID);

                DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
                if (ds != null)
                    if (ds.Tables[0].Rows.Count > 0)
                        polStat = ds.Tables[0].Rows[0]["POLICY_DESCRIPTION"].ToString();

                return polStat;
            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
        /// <summary>
        /// Get the Policy Details based on Customer ID, Policy id and Policy version id
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="polID"></param>
        /// <param name="polVersionID"></param>
        /// <returns></returns>
        public String GetPolicyDetailsData(string POLICY_NUMBER)
        {
            try
            {
                string strStoredProc = "Proc_GetIntallmentDetailsforDepositItem";
                string retValue = "";
                DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
       
                objWrapper.AddParameter("@OUR_NUMBER", System.DBNull.Value);
                objWrapper.AddParameter("@POLICY_NUMBER", POLICY_NUMBER);
                objWrapper.AddParameter("@CALLED_FOR", "POL_N");
                objWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID);


                DataSet ds = objWrapper.ExecuteDataSet(strStoredProc);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    retValue = ds.Tables[0].Rows[0][0].ToString();
                    retValue += "^" + ds.Tables[0].Rows[0][1].ToString();
                    retValue += "^" + ds.Tables[0].Rows[0][2].ToString();
                    retValue += "^" + ds.Tables[0].Rows[0][3].ToString();
                    retValue += "^" + ds.Tables[0].Rows[0][4].ToString();
                }
                return retValue;
            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetSysParm()
        {
            try
            {
                
                String strStoredProc = "Proc_GetSystemParams";

                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

                DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);

                return ds;
            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
       /// <summary>
       /// Get the Installment details data based on the Our Number - Boleto number
       /// </summary>
       /// <param name="OUR_NUMBER"></param>
       /// <returns></returns>
        public String GetInstallmentDetailsUsingOurNumber(String OUR_NUMBER)
        {
            try
            {
                String retValue = String.Empty;
                /*Calling the stored procedure to get the installment details data based on the our number*/
                String strStoredProc = "Proc_GetIntallmentDetailsforDepositItem";

                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

                objDataWrapper.AddParameter("@OUR_NUMBER", OUR_NUMBER);
                objDataWrapper.AddParameter("@POLICY_NUMBER", System.DBNull.Value);
                objDataWrapper.AddParameter("@CALLED_FOR", "OUR_N");
                objDataWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID);

                DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);

                if (ds!=null &&  ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    retValue = ds.Tables[0].Rows[0][0].ToString();
                    retValue +="^"+ ds.Tables[0].Rows[0][1].ToString();
                    retValue +="^"+ ds.Tables[0].Rows[0][2].ToString();
                    retValue += "^" + ds.Tables[0].Rows[0][3].ToString();
                    retValue += "^" + ds.Tables[0].Rows[0][4].ToString();
                    retValue += "^" + ds.Tables[0].Rows[0][5].ToString();
                    retValue += "^" + ds.Tables[0].Rows[0][6].ToString();
                    retValue += "^" + ds.Tables[0].Rows[0][7].ToString();
                    retValue += "^" + ds.Tables[0].Rows[0][8].ToString();//BOLETO_NO
                    retValue += "^" + ds.Tables[0].Rows[0][9].ToString();//BOLETO_ID
                    retValue += "^" + ds.Tables[0].Rows[0][10].ToString();//is_active
                    retValue += "^" + ds.Tables[0].Rows[0][11].ToString();//Installment_no
                    retValue += "^" + ds.Tables[0].Rows[0][12].ToString();//POLICY_NUMBER

                }

                return retValue;
            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
        /// <summary>
        /// Get the Deposit Line items Info Data based on Customer id , policy id , policy version id
        /// </summary>
        /// <returns></returns>
        public Boolean FetchDepositLineItemsData(ref ClsAddDepositDetailsinfo ObjAddDepositDetailsinfo)
        {
            Boolean returnValue = false;
            DataSet dsCount = null;
            try
            {
                dsCount = ObjAddDepositDetailsinfo.FetchData();
                if (dsCount.Tables[0].Rows.Count != 0)
                {
                    ClsCommon.PopulateEbixPageModel(dsCount, ObjAddDepositDetailsinfo);
                    returnValue = true;

                }//if (dsCount.Tables[0].Rows.Count != 0)
                else
                    returnValue = false;
            }
            catch (Exception ex)
            { throw (ex); }
            finally { }
            return returnValue;
        }// public Boolean FetchDepositLineItemsData(ref ClsAddDepositDetailsinfo ObjAddDepositDetailsinfo)
        /// <summary>
        /// Insert the Deposit Line items Data
        /// </summary>
        /// <param name="objAddDepositDetailsinfo"></param>
        /// <returns></returns>
        public int AddDepositLineItemsInfo(ClsAddDepositDetailsinfo objAddDepositDetailsinfo,Cms.EbixDataLayer.DataWrapper objWrapper)
        {
            int returnValue = 0;

            if (objAddDepositDetailsinfo.RequiredTransactionLog)
            {
                objAddDepositDetailsinfo.TransactLabel = ClsCommon.MapTransactionLabel("Account/Aspx/AddDepositDetails.aspx.resx");

                returnValue = objAddDepositDetailsinfo.AddDepositLineItemsData(objWrapper, objAddDepositDetailsinfo.DEPOSIT_TYPE.CurrentValue.ToString());

            }// if (objAddDepositDetailsinfo.RequiredTransactionLog)
            return returnValue;
        }//public int AddDepositLineItemsInfo(ClsAddDepositDetailsinfo objAddDepositDetailsinfo)

      /// <summary>
      /// Insert the Deposit Line items Data
      /// </summary>
      /// <param name="objAddDepositDetailsinfo"></param>
      /// <returns></returns>
        public int AddDepositLineItemsInfo(ClsAddDepositDetailsinfo objAddDepositDetailsinfo)
        {
            int returnValue = 0;

            if (objAddDepositDetailsinfo.RequiredTransactionLog)
            { 
                objAddDepositDetailsinfo.TransactLabel = ClsCommon.MapTransactionLabel("Account/Aspx/AddDepositDetails.aspx.resx");
                objAddDepositDetailsinfo.CUSTOM_INFO = ClsCommon.BL_LANG_ID == 2 ? "Número de Depósito = " + objAddDepositDetailsinfo.DEPOSIT_NUMBER.CurrentValue.ToString() : "Deposit Number = " + objAddDepositDetailsinfo.DEPOSIT_NUMBER.CurrentValue.ToString(); 
                returnValue = objAddDepositDetailsinfo.AddDepositLineItemsData();

            }// if (objAddDepositDetailsinfo.RequiredTransactionLog)
            return returnValue;
        }//public int AddDepositLineItemsInfo(ClsAddDepositDetailsinfo objAddDepositDetailsinfo)

        /// <summary>
        /// Update The Deposit Line items Info Data  
        /// </summary>
        /// <param name="ObjAddDepositDetailsinfo"></param>
        /// <returns></returns>
        public int UpdateDepositLineItemsInfo(ClsAddDepositDetailsinfo ObjAddDepositDetailsinfo)
        {
            int returnValue = 0;

            if (ObjAddDepositDetailsinfo.RequiredTransactionLog)
            {
                ObjAddDepositDetailsinfo.TransactLabel = ClsCommon.MapTransactionLabel("Account/Aspx/AddDepositDetails.aspx.resx");
                ObjAddDepositDetailsinfo.CUSTOM_INFO = ClsCommon.BL_LANG_ID == 2 ? "Número de Depósito = " + ObjAddDepositDetailsinfo.DEPOSIT_NUMBER.CurrentValue.ToString() : "Deposit Number = " + ObjAddDepositDetailsinfo.DEPOSIT_NUMBER.CurrentValue.ToString(); 
    
                returnValue = ObjAddDepositDetailsinfo.UpdateDepositLineItemsData();

            }//  if (ObjAddDepositDetailsinfo.RequiredTransactionLog)

            return returnValue;
        }// public int UpdateDepositLineItemsInfo(ClsAddDepositDetailsinfo ObjAddDepositDetailsinfo)

        public int DeleteDepositLineItemInfo(ClsAddDepositDetailsinfo ObjAddDepositDetailsinfo)
        {
            int returnValue = 0;

            if (ObjAddDepositDetailsinfo.RequiredTransactionLog)
            {
                ObjAddDepositDetailsinfo.TransactLabel = ClsCommon.MapTransactionLabel("Account/Aspx/AddDepositDetails.aspx.resx");
                ObjAddDepositDetailsinfo.CUSTOM_INFO = ClsCommon.BL_LANG_ID == 2 ? "Número de Depósito = " + ObjAddDepositDetailsinfo.DEPOSIT_NUMBER.CurrentValue.ToString() : "Deposit Number = " + ObjAddDepositDetailsinfo.DEPOSIT_NUMBER.CurrentValue.ToString(); 
                returnValue = ObjAddDepositDetailsinfo.DeleteDepositLineItemsData();

            }//  if (ObjAddDepositDetailsinfo.RequiredTransactionLog)

            return returnValue;
        }// public int DeleteDepositLineItemInfo(ClsAddDepositDetailsinfo ObjAddDepositDetailsinfo)
       
        public int ApproveDepositLineItemInfo(ClsAddDepositDetailsinfo ObjAddDepositDetailsinfo)
        {
            int returnValue = 0;

            if (ObjAddDepositDetailsinfo.RequiredTransactionLog)
            {
                ObjAddDepositDetailsinfo.TransactLabel = ClsCommon.MapTransactionLabel("Account/Aspx/AddDepositDetails.aspx.resx");
                ObjAddDepositDetailsinfo.CUSTOM_INFO = ClsCommon.BL_LANG_ID == 2 ? "Número de Depósito = " + ObjAddDepositDetailsinfo.DEPOSIT_NUMBER.CurrentValue.ToString() : "Deposit Number = " + ObjAddDepositDetailsinfo.DEPOSIT_NUMBER.CurrentValue.ToString(); 
                returnValue = ObjAddDepositDetailsinfo.ApproveDepositLineItemsData();

            }//  if (ObjAddDepositDetailsinfo.RequiredTransactionLog)

            return returnValue;
        }// public int ApproveDepositLineItemInfo(ClsAddDepositDetailsinfo ObjAddDepositDetailsinfo)


        public int GenerateReceiptOfDepositLineItemInfo(ClsAddDepositDetailsinfo ObjAddDepositDetailsinfo)
        {
            int returnValue = 0;

            if (ObjAddDepositDetailsinfo.RequiredTransactionLog)
            {
                ObjAddDepositDetailsinfo.TransactLabel = ClsCommon.MapTransactionLabel("Account/Aspx/AddDepositDetails.aspx.resx");
                ObjAddDepositDetailsinfo.CUSTOM_INFO = ClsCommon.BL_LANG_ID == 2 ? "Número de Depósito = " + ObjAddDepositDetailsinfo.DEPOSIT_NUMBER.CurrentValue.ToString() : "Deposit Number = " + ObjAddDepositDetailsinfo.DEPOSIT_NUMBER.CurrentValue.ToString(); 
                returnValue = ObjAddDepositDetailsinfo.GenerateReceiptDepositLineItemsData();

            }//  if (ObjAddDepositDetailsinfo.RequiredTransactionLog)

            return returnValue;
        }// public int GenerateReceiptOfDepositLineItemInfo(ClsAddDepositDetailsinfo ObjAddDepositDetailsinfo)


        /// <summary>
        /// Get the Receipt generated details based on @DEPOSIT_ID,@CD_LINE_ITEM_ID and @RECEIPT_NUM 
        /// </summary>
        /// <returns></returns>
        public DataSet FetchGeneratedReceiptDepositLineItemsData(int DEPOSIT_ID, int CD_LINE_ITEM_ID, string RECEIPT_NUM,int User_id)
        {
            DataSet dsCount = null;
            try
            {
                ClsAddDepositDetailsinfo ObjAddDepositDetailsinfo = new ClsAddDepositDetailsinfo();
                dsCount = ObjAddDepositDetailsinfo.FetchDataReceiptGeneratedData(DEPOSIT_ID, CD_LINE_ITEM_ID, RECEIPT_NUM, User_id);
               
            }
            catch (Exception ex)
            { throw (ex); }
            finally { }
            return dsCount;
        }// public DataSet FetchGeneratedReceiptDepositLineItemsData(ref ClsAddDepositDetailsinfo ObjAddDepositDetailsinfo)

        /// <summary>
        /// Get the Deposit Line items exception details - to show the report
        /// </summary>
        /// <param name="OUR_NUMBER"></param>
        /// <returns></returns>
        public DataSet GetDepositLineItemExceptionDetails(Int32 Deposit_id)
        {
            try
            {
                String strStoredProc = "Proc_GetDepositLineItemExceptionDetails";

                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

                objDataWrapper.AddParameter("@DEPOSIT_ID", Deposit_id);
                objDataWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID);
                DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);
                return ds;
            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
        #endregion

        //Added by Pradeep Kushwaha on 20-Aug-2011 -itrack 1049 to display the validation message for Noraml and Co-Insu
        /// <summary>
        /// Get the Deposit Line items exception details - to show the Validation message
        /// </summary>
        /// <param name="OUR_NUMBER"></param>
        /// <returns></returns>
        public DataSet GetDepositLineItemDetails(Int32 Deposit_id,String Deposit_type)
        {
            try
            {
                String strStoredProc = "Proc_GetDepositLineItemDetails";

                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

                objDataWrapper.AddParameter("@DEPOSIT_ID", Deposit_id);
                objDataWrapper.AddParameter("@DEPOSIT_TYPE", Deposit_type);
                objDataWrapper.AddParameter("@LANG_ID", ClsCommon.BL_LANG_ID);
                DataSet ds = objDataWrapper.ExecuteDataSet(strStoredProc);
                return ds;
            }
            catch (Exception exp)
            {
                throw (exp);
            }
        }
    }
}
