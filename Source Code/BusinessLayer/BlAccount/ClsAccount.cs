using System;
using Cms.DataLayer;
using System.Data;
using System.Data.SqlClient;
using Cms.Model.Account;

namespace Cms.BusinessLayer.BlAccount
{
	/// <summary>
	/// Summary description for ClsAccount.
	/// </summary>
	public class ClsAccount : Cms.BusinessLayer.BlCommon.ClsCommon
	{
		private			bool		boolTransactionLog;
		public ClsAccount()
		{
			boolTransactionLog	= base.TransactionLogRequired;
			//
			// TODO: Add constructor logic here
			//
			
		}
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
		
		//Added by Asfa (15-May-2008) - iTrack issue #4195
		public static void EFTsweepOperation(string IDEN_ROW_IDs, string Operation)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@IDEN_ROW_ID",IDEN_ROW_IDs);
			objWrapper.AddParameter("@OPERATION",Operation);
			objWrapper.ExecuteNonQuery("PROC_PERFORM_EFT_SWEEP_OPERATION");			
		}

		public static void CreditCardSweepOperation(string IDEN_ROW_IDs, string Operation)
		{
			DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.NO,DataWrapper.SetAutoCommit.OFF);				
			objWrapper.AddParameter("@IDEN_ROW_ID",IDEN_ROW_IDs);
			objWrapper.AddParameter("@OPERATION",Operation);
			objWrapper.ExecuteNonQuery("PROC_PERFORM_CREDIT_CARD_SWEEP_OPERATION");			
		}
		//End by Asfa

        // Adding Comments for Merge and Copmare test
        // Another comment
		public int  ExecPostCustomerTransaction(int Customer_ID,DateTime To_Date)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter("@CUSTOMER_ID", Customer_ID);
				objWrapper.AddParameter("@TO_DATE",To_Date);
				return objWrapper.ExecuteNonQuery("Proc_DueCustomerTransactions");

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in Exec_Postcustomer_transaction\n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}
		
		}
		public int  ExecSmallBalanceWriteOff(DateTime To_Date,int userID)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			string strSql = "Proc_EOD_WriteOffAmount";
			
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter("@BALANCE_WRITE_OFF_DATE",To_Date);
				objWrapper.AddParameter("@USER_ID", userID);

				int returnResult = 0;
				if(TransactionLogRequired)
				{
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
					
						objTransactionInfo.RECORDED_BY		=	userID;
                        objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1649", "");//"Small Balance Write off processed.";
						returnResult	= objWrapper.ExecuteNonQuery(strSql,objTransactionInfo);
				
				}
				else
				{
					returnResult	= objWrapper.ExecuteNonQuery(strSql);
				}

				return returnResult;

				//return objWrapper.ExecuteNonQuery("Proc_EOD_WriteOffAmount");

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in ExecSmallBalanceWriteOff\n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}
		
		}

		#region For EDO
		public static int ProcessPrebillToNormalPosting()
		{
			DataWrapper objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objWrapper.ExecuteNonQuery("Proc_ProcessPrebillToNormalPosting");
				objWrapper.CommitTransaction (DataWrapper.CloseConnection.YES);
				return 1 ; 
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
		}
		public static void ReconCustomerOpenItems(int UserID)
		{
			DataWrapper objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			try
			{
				objWrapper.AddParameter("@USER_ID",UserID);
				objWrapper.ExecuteNonQuery("Proc_ReconCustomerOpenItem");
				objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
			catch(Exception ex)
			{
				objWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				throw(ex);
			}
		}

		#endregion

		public int ExecClearAcctData(int Customer_ID)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter("@CustomerId", Customer_ID);
				return objWrapper.ExecuteNonQuery("Proc_CleanAccountingDateCompletly");

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in ExecClearAcctData\n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}
		}

		public DataSet GetCustomerPolicies(int Customer_ID)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter("@CustomerId", Customer_ID);
				return objWrapper.ExecuteDataSet("Proc_Get_Policy");

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in ExecClearPolData\n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}
		}

		/// <summary>
		/// GetFiscalYearActiveCancPolicies
		/// Enter Transaction Log Entries for Each generation
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public DataSet  GetFiscalYearActiveCancPolicies(int userID)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			string strSql = "Proc_GetFiscalYearActiveCancPolicies";
			DataSet dsTemp = null;
			
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				
				
				if(TransactionLogRequired)
				{
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY		=	userID;
                    objTransactionInfo.TRANS_DESC = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1650", "");// "RTL Hot File Generated.";

					objWrapper.ExecuteNonQuery(objTransactionInfo);
					objWrapper.ClearParameteres();

					dsTemp = objWrapper.ExecuteDataSet(strSql);

				
				}
				else
				{
					dsTemp = objWrapper.ExecuteDataSet(strSql);
				}

				return dsTemp ;
		

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in GetFiscalYearActiveCancPolicies\n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}
		
		}

		public DataSet GetPositivePay(DateTime dt)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter("@DATE_PRINTED", dt);
				return objWrapper.ExecuteDataSet("Proc_GetPositivePay");

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in GetPositivePay\n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}
		}

		public int ExecClearPolData(int Customer_ID,int POLICY_ID, int POLICY_VERSION_ID)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter("@CustomerId", Customer_ID);
				objWrapper.AddParameter("@Polid",POLICY_ID);
				objWrapper.AddParameter("@Polversionid",POLICY_VERSION_ID);
				return objWrapper.ExecuteNonQuery("Proc_DeletePolicy");

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in ExecClearPolData\n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}
		}
		public int ExecClearPolData(int Customer_ID)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter("@CustomerId", Customer_ID);
				//objWrapper.AddParameter("@Polid",POLICY_ID);
				//objWrapper.AddParameter("@Polversionid",POLICY_VERSION_ID);
				return objWrapper.ExecuteNonQuery("Proc_DeletePolicy");

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in ExecClearPolData\n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}
		}
		public static void GetCustomerDropDown(System.Web.UI.WebControls.DropDownList combo)
		{
			string strStoredProc = "Proc_GetCustomerInfoforAccounts";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet(strStoredProc).Tables[0];
			combo.Items.Clear();
			combo.Items.Add("");
			for(int i=0;i<objDataTable.DefaultView.Count;i++)
			{
				combo.Items.Add(new System.Web.UI.WebControls.ListItem(objDataTable.DefaultView[i]["CUSTOMER_NAME"].ToString(),objDataTable.DefaultView[i]["CUSTOMER_ID"].ToString()));
			}
		}
		public static void GetCustomerDropDownforPol(System.Web.UI.WebControls.DropDownList combo)
		{
			string strStoredProc = "Proc_GetCustomerInfoforPol";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet(strStoredProc).Tables[0];
			combo.Items.Clear();
			combo.Items.Add("");
			for(int i=0;i<objDataTable.DefaultView.Count;i++)
			{
				combo.Items.Add(new System.Web.UI.WebControls.ListItem(objDataTable.DefaultView[i]["CUSTOMER_NAME"].ToString(),objDataTable.DefaultView[i]["CUSTOMER_ID"].ToString()));
			}
		}
		

		// Fetch Policy Info on basis of Policy Number
		public static string GetInfoFromPolicyNum(string PolNum)
		{
			try
			{
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objWrapper.AddParameter("@POLICY_NUMBER",PolNum);
				string strSQL = "Proc_GetInfoFromPolicyNum";
				DataSet objDS = objWrapper.ExecuteDataSet(strSQL);

				if(objDS != null && objDS.Tables.Count > 0 && objDS.Tables[0].Rows.Count > 0)
				{
					string strCustomerID = objDS.Tables[0].Rows[0]["CUSTOMER_ID"].ToString();
					string strPolicyID = objDS.Tables[0].Rows[0]["POLICY_ID"].ToString();
					string strPolVerID = objDS.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString();
					string AgencyName =  objDS.Tables[0].Rows[0]["AGENCY_NAME"].ToString();
					string insuredName = objDS.Tables[0].Rows[0]["CUSTOMER_NAME"].ToString();
					string AgencyID	   = objDS.Tables[0].Rows[0]["AGENCY_ID"].ToString(); 
					string billType		= objDS.Tables[0].Rows[0]["BILL_TYPE"].ToString(); 
					string strRetVal = strPolicyID +'^'+ strCustomerID +'^'+ PolNum +'^'+ strPolVerID+'^'+ AgencyName+'^'+ insuredName+'^'+ AgencyID+'^'+ billType;
					return strRetVal;
				}
				else
					return "0";
					
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
									
		}
		// Validate Policy Number for a customer
		public static string ValidatePolicyNumForCust(string PolNum,int CustID)
		{
			try
			{
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objWrapper.AddParameter("@POLICY_NUMBER",PolNum);
				objWrapper.AddParameter("@CUSTOMER_ID",CustID);
				string strSQL = "Proc_ValidatePolNumForCust";
				DataSet objDS = objWrapper.ExecuteDataSet(strSQL);

				if(objDS != null && objDS.Tables.Count > 0 && objDS.Tables[0].Rows.Count > 0)
				{
					string strRetVal  = objDS.Tables[0].Rows[0]["POLICY_NUMBER_COUNT"].ToString(); 
					return strRetVal;
				}
				else
					return "0";
					
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
			
		}
		//Code added by Shikha Dixit for itrack# 4906.
		public static string GetResintatementFee(string Policy_Number)
		{
			try
			{
				DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter ("@Policy_number", Policy_Number);
				string strSQL = "Proc_Act_FetchResintatementFee";
				DataSet objDS = objWrapper.ExecuteDataSet(strSQL);				
				
					if(objDS != null && objDS.Tables.Count > 0 && objDS.Tables[0].Rows.Count > 0)
					{
						string ReinsFee;
						ReinsFee= objDS.Tables[0].Rows[0]["REINSTATEMENT_FEES"].ToString() 
							+ "^" + objDS.Tables[0].Rows[0]["STATUS"].ToString()						
							+ "^"  +  objDS.Tables[0].Rows[0]["CUSTOMER_ID"].ToString()
							+ "^" +  objDS.Tables[0].Rows[0]["POLICY_ID"].ToString() 
							+ "^" +  objDS.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString();	 
						
						return ReinsFee;
					}
				else
					return "0";
			}
				//return objWrapper.ExecuteNonQuery("Proc_Act_FetchResintatementFee");
			
			catch (Exception objExp)
			{
				throw(objExp);
			}
		}
		//End of code.
        
		//Added for Itrack Issue #4906 Charge_Late_Fee_Screen
		public static string GetLateFee(string Policy_Number)
		{
			try
			{
				DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter ("@Policy_number", Policy_Number);
				string strSQL = "Proc_Act_FetchLateFee";
				DataSet objDS = objWrapper.ExecuteDataSet(strSQL);				
				
				if(objDS != null && objDS.Tables.Count > 0 && objDS.Tables[0].Rows.Count > 0)
				{
					string LateFee;
					LateFee= objDS.Tables[0].Rows[0]["LATE_FEES"].ToString() 
						+ "^" + objDS.Tables[0].Rows[0]["STATUS"].ToString()						
						+ "^"  +  objDS.Tables[0].Rows[0]["CUSTOMER_ID"].ToString()
						+ "^" +  objDS.Tables[0].Rows[0]["POLICY_ID"].ToString() 
						+ "^" +  objDS.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString();	 
						
					return LateFee;
				}
				else
					return "0";
			}
				//return objWrapper.ExecuteNonQuery("Proc_Act_FetchResintatementFee");
			
			catch (Exception objExp)
			{
				throw(objExp);
			}
		}
		//End of code.

			//Added For Charge Installment Fee screen
		public static string GetInstallmentFee(string Policy_Number)
		{
			try
			{
				DataWrapper objWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter ("@Policy_number", Policy_Number);
				string strSQL = "Proc_Act_FetchInstallmentFee";
				DataSet objDS = objWrapper.ExecuteDataSet(strSQL);				
				
				if(objDS != null && objDS.Tables.Count > 0 && objDS.Tables[0].Rows.Count > 0)
				{
					string InstallmentFee;
					InstallmentFee = objDS.Tables[0].Rows[0]["INSTALLMENT_FEES"].ToString() 
						+ "^" + objDS.Tables[0].Rows[0]["STATUS"].ToString()						
						+ "^"  +  objDS.Tables[0].Rows[0]["CUSTOMER_ID"].ToString()
						+ "^" +  objDS.Tables[0].Rows[0]["POLICY_ID"].ToString() 
						+ "^" +  objDS.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString();	 
						
					return InstallmentFee;
				}
				else
					return "0";
			}
				//return objWrapper.ExecuteNonQuery("Proc_Act_FetchResintatementFee");
			
			catch (Exception objExp)
			{
				throw(objExp);
			}
		}
		//End of code.

		// Validate Policy Num
		public static int ValidatePolicyNum(string PolNum)
		{
			try
			{
				DataWrapper objWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				objWrapper.AddParameter("@POLICY_NUMBER",PolNum);
				SqlParameter objSqlParam = (SqlParameter)objWrapper.AddParameter("@OUTPUT",SqlDbType.Int,ParameterDirection.Output);
				
				string strSQL = "Proc_ValidatePolicyNum";
				objWrapper.ExecuteNonQuery(strSQL);
				int retValue = int.Parse(objSqlParam.Value.ToString());
				return retValue;
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
									
		}
        //Added By Pradeep Kushwaha on 11-06-2010
        #region Get Customer and Agency Open Items
        public static DataSet GetPolicyOpenItems(string policyNo)
        {
            string strSql = "Proc_FetchOpenItems";
            try
            {

                DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure);

                if (policyNo != null && policyNo.Length <= 0)
                {
                    objDataWrapper.AddParameter("@POLICYNO", DBNull.Value);
                    objDataWrapper.AddParameter("@lang_id", BlCommon.ClsCommon.BL_LANG_ID); //Added by aditya on 29-08-2011 for itrack # 1327
                }
                else
                    objDataWrapper.AddParameter("@POLICYNO", policyNo);
                    objDataWrapper.AddParameter("@lang_id", BlCommon.ClsCommon.BL_LANG_ID); //Added by aditya on 29-08-2011 for itrack # 1327

                DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
                return objDataSet;
            }
            catch (Exception objEx)
            {
                throw (objEx);
            }

        }
        
        #endregion
        //End Added 
        #region GET WRITEEN PREMIUM OFF / POSTING
        public static DataSet GetWriteOffAmountDetail(string policyNo)
		{
			string strSql = "Proc_FetchWriteoffAmount";
			try
			{

				DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
				if(policyNo!=null && policyNo.Length<=0)
					objDataWrapper.AddParameter("@POLICYNO",DBNull.Value);
				else
					objDataWrapper.AddParameter("@POLICYNO",policyNo);
				
				DataSet objDataSet = objDataWrapper.ExecuteDataSet(strSql);
				return objDataSet;
			}
			catch(Exception objEx)
			{
				throw(objEx);
			}
					
		}
		public int ProcessPremiumWriteOffAmount(int idenRowId,double cAmount,double amount, DateTime To_Date,int postingFlag,int userID,int groupID,double totalBalance,string policyNo)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			string strSql = "Proc_Premium_WriteOffAmount";

			int custId = 0;
			int policyId = 0;
			int policyVersionId = 0;
			DataSet dsTemp = new DataSet();
            dsTemp = GetPolicyDetails(policyNo);
			if(dsTemp!=null && dsTemp.Tables.Count>0 && dsTemp.Tables[0].Rows.Count > 0)
			{
				custId = int.Parse(dsTemp.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
				policyId = int.Parse(dsTemp.Tables[0].Rows[0]["POLICY_ID"].ToString());
				policyVersionId = int.Parse(dsTemp.Tables[0].Rows[0]["POLICY_VERSION_ID"].ToString());
			}
			
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter("@IDEN_ROW_ID", idenRowId);
				objWrapper.AddParameter("@CUMULATIVE_WRITE_OFF_AMOUNT",cAmount);	
				objWrapper.AddParameter("@WRITE_OFF_AMOUNT",amount);	
				objWrapper.AddParameter("@PREMIUM_WRITE_OFF_DATE",To_Date);
				objWrapper.AddParameter("@IS_POSTING", postingFlag);
				objWrapper.AddParameter("@USER_ID", userID);
				objWrapper.AddParameter("@GROUP_DETAIL_ID", groupID);
				SqlParameter objSqlParam = (SqlParameter)objWrapper.AddParameter("@GROUP_ID",SqlDbType.Int,ParameterDirection.Output);


				
				
				int returnResult = 0;
				if(TransactionLogRequired)
				{
					if(postingFlag == 0) //Translog Consolidated entry 
					{
						SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
						Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						objTransactionInfo.TRANS_TYPE_ID	=	1;
					
						objTransactionInfo.RECORDED_BY				=	userID;
						objTransactionInfo.CLIENT_ID				=	custId;
						objTransactionInfo.POLICY_ID				=	policyId;
						objTransactionInfo.POLICY_VER_TRACKING_ID	=	policyVersionId;

                        objTransactionInfo.TRANS_DESC       = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1651", "");// "Premium Amount Written off.";
						objTransactionInfo.CUSTOM_INFO		=	"Total Balance Before Write Off :"+ totalBalance +"; Total Amount Written Off :" + cAmount;
						//Executing the query
						returnResult	= objWrapper.ExecuteNonQuery(strSql,objTransactionInfo);
					}
					else
						returnResult	= objWrapper.ExecuteNonQuery(strSql);
				}
				else
				{
					returnResult	= objWrapper.ExecuteNonQuery(strSql);
				}

				int retValue = int.Parse(objSqlParam.Value.ToString());
				return retValue;

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in ProcessPremiumWriteOffAmount\n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
				dsTemp = null;
			}
		}
		#region Process End of Year / GetFiscalDateEOY
		public int GetFiscalDateEOY(DateTime forDate,out int year,out int fiscalID,out int is_EOY_Processed,out DateTime EOY_Date)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			string strSql = "Proc_GetFiscalDateForEOY";
			int returnResult = 0;
					
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter("@FOR_DATE",forDate);
				SqlParameter objSqlParam = (SqlParameter)objWrapper.AddParameter("@YEAR",SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlParam_ID = (SqlParameter)objWrapper.AddParameter("@FISCAL_ID",SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlParam_EOY_FLAG = (SqlParameter)objWrapper.AddParameter("@EOY_PROCESSED",SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlParam_EOY_DATE = (SqlParameter)objWrapper.AddParameter("@EOY_PROCESSING_DATE",SqlDbType.DateTime,ParameterDirection.Output);
				returnResult	= objWrapper.ExecuteNonQuery(strSql);
			
				int retValue = int.Parse(objSqlParam.Value.ToString());

				if(objSqlParam.Value!=null)
					year = int.Parse(objSqlParam.Value.ToString());
				else
					year = 0;


				if(objSqlParam_ID.Value!=null)
					fiscalID = int.Parse(objSqlParam_ID.Value.ToString());
				else
					fiscalID = 0;

				if(objSqlParam_EOY_FLAG.Value!=null)
					is_EOY_Processed = int.Parse(objSqlParam_EOY_FLAG.Value.ToString());
				else
					is_EOY_Processed = 0;

				if(objSqlParam_EOY_DATE.Value!=null && objSqlParam_EOY_DATE.Value.ToString()!="")
					EOY_Date = DateTime.Parse(objSqlParam_EOY_DATE.Value.ToString());
				else
					EOY_Date = DateTime.Now;


				return retValue;

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in Proc_GetFiscalDateForEOY\n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
				
			}
		}

		public int ProcessEndofYear(int fiscalID,int userID,int year)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			string strSql = "Proc_PostEndOfYear";
			
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter("@OLD_FISCAL_ID", fiscalID);
				objWrapper.AddParameter("@USER_ID",userID);				
								
				int returnResult = 0;
				if(TransactionLogRequired)
				{
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					objTransactionInfo.RECORDED_BY				=	userID;
                    objTransactionInfo.TRANS_DESC           = Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1652","")+" " + year + " "+Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1653","");//"End Of Year for fiscal " + year + " has been processed Successfully.";
					//Executing the query
					returnResult	= objWrapper.ExecuteNonQuery(strSql,objTransactionInfo);
					
				}
				else
				{
					returnResult	= objWrapper.ExecuteNonQuery(strSql);
				}

				return returnResult;

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in ProcessEndofYear\n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
				
			}
		}
		#endregion


		#endregion
		#region PROCESS 1099 / SAVE / UPDATE / GET / FREEZE / SET FISCAL CAPTION
		#region Set Fiscal Caption
		public int GetFiscalDateTen99(DateTime forDate,out int year,out int fiscalID,out int is_Ten99_Processed,out DateTime ten99_Date)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			string strSql = "Proc_GetFiscalDateFor1099";
			int returnResult = 0;
					
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter("@FOR_DATE",forDate);
				SqlParameter objSqlParam = (SqlParameter)objWrapper.AddParameter("@YEAR",SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlParam_ID = (SqlParameter)objWrapper.AddParameter("@FISCAL_ID",SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlParam_TEN99_FLAG = (SqlParameter)objWrapper.AddParameter("@TEN99_PROCESSED",SqlDbType.Int,ParameterDirection.Output);
				SqlParameter objSqlParam_TEN99_DATE = (SqlParameter)objWrapper.AddParameter("@TEN99_PROCESSING_DATE",SqlDbType.DateTime,ParameterDirection.Output);
				returnResult	= objWrapper.ExecuteNonQuery(strSql);
			
				int retValue = int.Parse(objSqlParam.Value.ToString());

				if(objSqlParam.Value!=null)
					year = int.Parse(objSqlParam.Value.ToString());
				else
					year = 0;


				if(objSqlParam_ID.Value!=null)
					fiscalID = int.Parse(objSqlParam_ID.Value.ToString());
				else
					fiscalID = 0;

				if(objSqlParam_TEN99_FLAG.Value!=null)
					is_Ten99_Processed = int.Parse(objSqlParam_TEN99_FLAG.Value.ToString());
				else
					is_Ten99_Processed = 0;

				if(objSqlParam_TEN99_DATE.Value!=null && objSqlParam_TEN99_DATE.Value.ToString()!="")
					ten99_Date = DateTime.Parse(objSqlParam_TEN99_DATE.Value.ToString());
				else
					ten99_Date = DateTime.Now;


				return retValue;

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in Proc_GetFiscalDateForEOY\n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
				
			}
		}
		#endregion
		#region PROCESS 1099 
		public static int Process1099(int year,int userId,int fiscal_ID)
		{
            string strCarrierSystemID = CarrierSystemID; //System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString().ToUpper();
			try
			{
				DataWrapper objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr,CommandType.StoredProcedure);
				objWrapper.AddParameter("@YEAR",year);
				objWrapper.AddParameter("@PROCESSED_BY",userId);
				objWrapper.AddParameter("@PAYORS_CARRIER_CODE",strCarrierSystemID);   
				objWrapper.AddParameter("@OLD_FISCAL_ID",fiscal_ID);   
   			    
				return objWrapper.ExecuteNonQuery("Proc_Process_1099");
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
		#endregion
		#region GET 1099
		/// <summary>
		/// Returns the data in the form of XML of specified 1099 id : 4 march 2008
		/// </summary>
		/// <param name="intDepositId">1099 form id whose data will be returned</param>
		/// <returns>XML of data</returns>
		public static string Get1099Info(int int1099Id )
		{
			String strStoredProc = "Proc_GetForm1099";
			DataSet ds = new DataSet();
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure);
				
			try
			{
				objDataWrapper.AddParameter("@FORM_1099_ID",int1099Id);
				
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
		#region SAVE FORM 1099
		/// <summary>
		/// Saves the information passed in model object to database.
		/// </summary>
		/// <param name="objPriorPolicyInfo">Model class object.</param>
		/// <returns>No of records effected.</returns>
		public int AddForm1099(Cms.Model.Account.ClsForm1099 objForm1099Info)
		{
			SqlParameter objParam;
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
            string strCarrierSystemID = CarrierSystemID; //System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString();

			try
			{
				/*--Called from proc and Screen  */
				objParam = (SqlParameter)objDataWrapper.AddParameter("@FORM_1099_ID",null,System.Data.SqlDbType.Int,System.Data.ParameterDirection.Output );
				objDataWrapper.AddParameter("@ENTITY_TYPE",objForm1099Info.ENTITY_TYPE);
				objDataWrapper.AddParameter("@PAYORS_CARRIER_CODE",strCarrierSystemID);      
				objDataWrapper.AddParameter("@RENTS",objForm1099Info.RENTS);
				objDataWrapper.AddParameter("@ROYALATIES",objForm1099Info.ROYALATIES);
				objDataWrapper.AddParameter("@OTHERINCOME",objForm1099Info.OTHERINCOME);
				objDataWrapper.AddParameter("@FEDERAL_INCOME_TAXWITHHELD",objForm1099Info.FEDERAL_INCOME_TAXWITHHELD);
				objDataWrapper.AddParameter("@FISHING_BOAT_PROCEEDS",objForm1099Info.FISHING_BOAT_PROCEEDS);
				objDataWrapper.AddParameter("@MEDICAL_AND_HEALTH_CARE_PRODUCTS",objForm1099Info.MEDICAL_AND_HEALTH_CARE_PRODUCTS);
				objDataWrapper.AddParameter("@NON_EMPLOYEMENT_COMPENSATION",objForm1099Info.NON_EMPLOYEMENT_COMPENSATION);
				objDataWrapper.AddParameter("@SUBSTITUTE_PAYMENTS",objForm1099Info.SUBSTITUTE_PAYMENTS);
				objDataWrapper.AddParameter("@PAYER_MADE_DIRECT_SALES",objForm1099Info.PAYER_MADE_DIRECT_SALES);
				objDataWrapper.AddParameter("@CROP_INSURANCE_PROCEED",objForm1099Info.CROP_INSURANCE_PROCEED);
				objDataWrapper.AddParameter("@EXCESS_GOLDEN_PARACHUTE_PAYMENTS",objForm1099Info.EXCESS_GOLDEN_PARACHUTE_PAYMENTS);
				objDataWrapper.AddParameter("@GROSS_PROCEEDS_PAID_TO_AN_ATTORNEY",objForm1099Info.GROSS_PROCEEDS_PAID_TO_AN_ATTORNEY);
				objDataWrapper.AddParameter("@STATE_TAX_WITHHELD",objForm1099Info.STATE_TAX_WITHHELD);
				objDataWrapper.AddParameter("@STATE_PAYER_STATE_NO",objForm1099Info.STATE_PAYER_STATE_NO);
				objDataWrapper.AddParameter("@STATE_INCOME",objForm1099Info.STATE_INCOME);
				objDataWrapper.AddParameter("@RECIPIENT_IDENTIFICATION",objForm1099Info.RECIPIENT_IDENTIFICATION);
				objDataWrapper.AddParameter("@RECIPIENT_NAME",objForm1099Info.RECIPIENT_NAME);
				objDataWrapper.AddParameter("@RECIPIENT_STREET_ADDRESS1",objForm1099Info.RECIPIENT_STREET_ADDRESS1);
				objDataWrapper.AddParameter("@RECIPIENT_STREET_ADDRESS2",objForm1099Info.RECIPIENT_STREET_ADDRESS2);
				objDataWrapper.AddParameter("@RECIPIENT_CITY",objForm1099Info.RECIPIENT_CITY);
				objDataWrapper.AddParameter("@RECIPIENT_STATE",objForm1099Info.RECIPIENT_STATE);
				objDataWrapper.AddParameter("@RECIPIENT_ZIP",objForm1099Info.RECIPIENT_ZIP);
				objDataWrapper.AddParameter("@ACCOUNT_NO",objForm1099Info.ACCOUNT_NO);
				objDataWrapper.AddParameter("@CREATED_BY",objForm1099Info.CREATED_BY);
				objDataWrapper.AddParameter("@CREATED_DATETIME",objForm1099Info.CREATED_DATETIME);
				objDataWrapper.AddParameter("@MODIFIED_BY", null);
				objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", null);
				objDataWrapper.AddParameter("@INSERTUPDATE", "I");
				//Added By Raghav For Itrack Issue # 4797
				objDataWrapper.AddParameter("@FED_SSN_1099",objForm1099Info.FED_SSN_1099); 

				int returnResult = 0;
				if(TransactionLogRequired)
				{
					objForm1099Info.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel("account/Aspx/AddForm1099.aspx.resx");
					SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
					string strTranXML = objBuilder.GetTransactionLogXML(objForm1099Info);
					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
					objTransactionInfo.TRANS_TYPE_ID	=	1;
					
					objTransactionInfo.RECORDED_BY		=	objForm1099Info.CREATED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1654", "");// "New Form 1099 has been added.";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					//Executing the query
					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_Insert_FORM_1099",objTransactionInfo);
					
				}
				else
				{
					returnResult	= objDataWrapper.ExecuteNonQuery("Proc_Insert_FORM_1099");
				}
				
				//Filling the APP_PRIOR_CARRIER_INFO_ID fiels, which proc has set
				objForm1099Info.FORM_1099_ID = int.Parse(objParam.Value.ToString());

				//Adding Trans ID in 1099 Details to get manual Updations of 1099
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@FORM_1099_ID", objForm1099Info.FORM_1099_ID);
				objDataWrapper.AddParameter("@TRANS_ID",objDataWrapper.TransactionIDs.ToString());
				int Result	= objDataWrapper.ExecuteNonQuery("Proc_Insert_TransLog_1099Check_Details");
				//END		

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
		#endregion Process 1099
		#region UPDATE FORM 1099
		/// <summary>
		/// Update method that recieves Model object to save.
		/// </summary>
		/// <param name="objOldPriorPolicyInfo">Model object having old information</param>
		/// <param name="objPriorPolicyInfo">Model object having new information(form control's value)</param>
		/// <returns>No. of rows updated (1 or 0)</returns>
		public int UpdateForm1099(Cms.Model.Account.ClsForm1099 objOldForm1099Info,Cms.Model.Account.ClsForm1099 objForm1099Info)
		{
			SqlUpdateBuilder objBuilder = new SqlUpdateBuilder();
			string strTranXML;
            string strCarrierSystemID = CarrierSystemID; //System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString();
			int returnResult = 0;
						
			DataWrapper objDataWrapper = new DataWrapper(ConnStr, CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
				
			//Adding the parameters to the datawrapper class object
			
			objDataWrapper.AddParameter("@FORM_1099_ID", objOldForm1099Info.FORM_1099_ID);
			objDataWrapper.AddParameter("@ENTITY_TYPE",objForm1099Info.ENTITY_TYPE);
			objDataWrapper.AddParameter("@PAYORS_CARRIER_CODE",strCarrierSystemID);       
			objDataWrapper.AddParameter("@RENTS",objForm1099Info.RENTS);
			objDataWrapper.AddParameter("@ROYALATIES",objForm1099Info.ROYALATIES);
			objDataWrapper.AddParameter("@OTHERINCOME",objForm1099Info.OTHERINCOME);
			objDataWrapper.AddParameter("@FEDERAL_INCOME_TAXWITHHELD",objForm1099Info.FEDERAL_INCOME_TAXWITHHELD);
			objDataWrapper.AddParameter("@FISHING_BOAT_PROCEEDS",objForm1099Info.FISHING_BOAT_PROCEEDS);
			objDataWrapper.AddParameter("@MEDICAL_AND_HEALTH_CARE_PRODUCTS",objForm1099Info.MEDICAL_AND_HEALTH_CARE_PRODUCTS);
			objDataWrapper.AddParameter("@NON_EMPLOYEMENT_COMPENSATION",objForm1099Info.NON_EMPLOYEMENT_COMPENSATION);
			objDataWrapper.AddParameter("@SUBSTITUTE_PAYMENTS",objForm1099Info.SUBSTITUTE_PAYMENTS);
			objDataWrapper.AddParameter("@PAYER_MADE_DIRECT_SALES",objForm1099Info.PAYER_MADE_DIRECT_SALES);
			objDataWrapper.AddParameter("@CROP_INSURANCE_PROCEED",objForm1099Info.CROP_INSURANCE_PROCEED);
			objDataWrapper.AddParameter("@EXCESS_GOLDEN_PARACHUTE_PAYMENTS",objForm1099Info.EXCESS_GOLDEN_PARACHUTE_PAYMENTS);
			objDataWrapper.AddParameter("@GROSS_PROCEEDS_PAID_TO_AN_ATTORNEY",objForm1099Info.GROSS_PROCEEDS_PAID_TO_AN_ATTORNEY);
			objDataWrapper.AddParameter("@STATE_TAX_WITHHELD",objForm1099Info.STATE_TAX_WITHHELD);
			objDataWrapper.AddParameter("@STATE_PAYER_STATE_NO",objForm1099Info.STATE_PAYER_STATE_NO);
			objDataWrapper.AddParameter("@STATE_INCOME",objForm1099Info.STATE_INCOME);
			objDataWrapper.AddParameter("@RECIPIENT_IDENTIFICATION",objForm1099Info.RECIPIENT_IDENTIFICATION);
			objDataWrapper.AddParameter("@RECIPIENT_NAME",objForm1099Info.RECIPIENT_NAME);
			objDataWrapper.AddParameter("@RECIPIENT_STREET_ADDRESS1",objForm1099Info.RECIPIENT_STREET_ADDRESS1);
			objDataWrapper.AddParameter("@RECIPIENT_STREET_ADDRESS2",objForm1099Info.RECIPIENT_STREET_ADDRESS2);
			objDataWrapper.AddParameter("@RECIPIENT_CITY",objForm1099Info.RECIPIENT_CITY);
			objDataWrapper.AddParameter("@RECIPIENT_STATE",objForm1099Info.RECIPIENT_STATE);
			objDataWrapper.AddParameter("@RECIPIENT_ZIP",objForm1099Info.RECIPIENT_ZIP);
			objDataWrapper.AddParameter("@ACCOUNT_NO",objForm1099Info.ACCOUNT_NO);
			objDataWrapper.AddParameter("@CREATED_BY",null);
			objDataWrapper.AddParameter("@CREATED_DATETIME",null);
			objDataWrapper.AddParameter("@MODIFIED_BY", objForm1099Info.MODIFIED_BY);
			objDataWrapper.AddParameter("@LAST_UPDATED_DATETIME", objForm1099Info.LAST_UPDATED_DATETIME);
			objDataWrapper.AddParameter("@INSERTUPDATE", "U");
			//Added By Raghav For Itrack Issue # 4797
			objDataWrapper.AddParameter("@FED_SSN_1099",objForm1099Info.FED_SSN_1099); 
			try 
			{
				if(TransactionLogRequired) 
				{
					objForm1099Info.TransactLabel = Cms.BusinessLayer.BlCommon.ClsCommon.MapTransactionLabel(
						"account/Aspx/AddForm1099.aspx.resx");
						
					strTranXML		=	objBuilder.GetTransactionLogXML(objOldForm1099Info,objForm1099Info);

					Cms.Model.Maintenance.ClsTransactionInfo objTransactionInfo = new Cms.Model.Maintenance.ClsTransactionInfo();
						
					objTransactionInfo.TRANS_TYPE_ID	=	3;
					
					objTransactionInfo.RECORDED_BY		=	objForm1099Info.MODIFIED_BY;
                    objTransactionInfo.TRANS_DESC       =   Cms.BusinessLayer.BlCommon.ClsCommon.FetchGeneralMessage("1655", "");// "Form 1099 has been Modified.";
					objTransactionInfo.CHANGE_XML		=	strTranXML;
					returnResult = objDataWrapper.ExecuteNonQuery("Proc_Insert_FORM_1099",objTransactionInfo);

				}
				else
				{
					returnResult = objDataWrapper.ExecuteNonQuery("Proc_Insert_FORM_1099");
				}

				//Adding Trans ID in 1099 Details to get manual Updations of 1099
				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@FORM_1099_ID", objOldForm1099Info.FORM_1099_ID);
				objDataWrapper.AddParameter("@TRANS_ID",objDataWrapper.TransactionIDs.ToString());
				int Result	= objDataWrapper.ExecuteNonQuery("Proc_Insert_TransLog_1099Check_Details");
				//END
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
		#endregion UPDATE FORM 1099
		#region Freeze 1099
		public static int Freeze1099(int formId)
		{
			try
			{
				DataWrapper objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr,CommandType.StoredProcedure);
				objWrapper.AddParameter("@FORM_1099_ID",formId);
				return objWrapper.ExecuteNonQuery("Proc_Freeze_1099");
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}
		#endregion
		#region 1099 Check Details
		public DataSet Get1099CheckDetails(int formId)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter("@FORM_1099_ID",formId);
				return objWrapper.ExecuteDataSet("Proc_1099_Check_Details");

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in Get1099CheckDetails\n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}
		}

		#endregion
		#region Get Transaction Details 1099
		public DataSet GetTransactionDetails1099(int formId)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter("@FORM_1099_ID",formId);
				return objWrapper.ExecuteDataSet("Proc_GetTransDetails_1099");

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in GetTransactionDetails1099\n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}
		}

		#endregion
		#region Fill 1099 YEAR
		public static void GetForm1099YeardropDown(System.Web.UI.WebControls.DropDownList combo)
		{
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.Text);
			DataTable  objDataTable = objDataWrapper.ExecuteDataSet("SELECT DISTINCT YEAR_1099 AS YEAR_1099  FROM FORM_1099 WITH(NOLOCK) where YEAR_1099 IS NOT NULL ORDER BY YEAR_1099 DESC").Tables[0];
			combo.Items.Clear();
			combo.DataSource = objDataTable;
			combo.DataTextField = "YEAR_1099";
			combo.DataValueField = "YEAR_1099";
			combo.DataBind();   
			
		}
		#endregion

		#endregion FORM 1099

		public DataSet GetPolicyDetails(string policyNumber)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter("@POLICY_NUM", policyNumber);
				DataSet dsTemp = new DataSet();
				dsTemp	= objWrapper.ExecuteDataSet("proc_getpolicy_details");
				return dsTemp;
				
			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in GetPolicyDetails\n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}
		}
		#region Get CC Notes for report
		public DataSet GetCCNoteDetails(int spoolId)
		{
			Cms.DataLayer.DataWrapper objWrapper = null;
			
			try
			{
				objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
				objWrapper.AddParameter("@SPOOL_ID", spoolId);
				return objWrapper.ExecuteDataSet("Proc_GetCCNoteDetails");

			}
			catch(Exception objExp)
			{
				throw( new Exception("Error occured in GetCCNoteDetails\n " + objExp.Message));
			}
			finally
			{
				objWrapper.Dispose();
			}
		}
		#endregion
        public DataSet GetRecordData(string POLICY_NUMBER)
        {
            Cms.DataLayer.DataWrapper objWrapper = null;

            try
            {
                
                objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);

                objWrapper.AddParameter("@POLICY_NUMBER", POLICY_NUMBER);
                DataSet ds = objWrapper.ExecuteDataSet("Proc_GetPolicyDisplayVersionNo");
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objWrapper.Dispose();
            }
        }


        //Added by Shikha Chourasiya on 21-nov for #tfs 2647
        public DataSet GetReinsuranceInstallmentData(string POLICY_NUMBER)
        {
            Cms.DataLayer.DataWrapper objWrapper = null;

            try
            {

                objWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);

                objWrapper.AddParameter("@POLICY_NUMBER", POLICY_NUMBER);
                DataSet ds = objWrapper.ExecuteDataSet("PROC_REINSURANCE_INSTALLMENT_DETAILS");
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objWrapper.Dispose();
            }
        }
        public DataSet fetchPolicyDataforPdfXml(int CustomerID, int PolicyId, int PolVersionId)
        {
            Cms.DataLayer.DataWrapper objDataWrapper = null;
            try
            {

                objDataWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
                objDataWrapper.ClearParameteres();
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyId);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolVersionId);
                objDataWrapper.AddParameter("@LANG_ID", 2);
                DataSet dsTemp = objDataWrapper.ExecuteDataSet("Proc_GetProductPDFPolicyDetails");
                objDataWrapper.ClearParameteres();
                return dsTemp;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }
        public void SavePolicyDocumentXml(int CustomerID, int PolicyId, int PolVersionId, String strPolicyDocXML, string doctype, int ClaimID, int ActivityID)
        {
            Cms.DataLayer.DataWrapper objDataWrapper = null;
            try
            {
                objDataWrapper = new DataWrapper(BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure);
               
                objDataWrapper.AddParameter("@CUSTOMER_ID", CustomerID);
                objDataWrapper.AddParameter("@POLICY_ID", PolicyId);
                objDataWrapper.AddParameter("@POLICY_VERSION_ID", PolVersionId);
                objDataWrapper.AddParameter("@DOC_XML", strPolicyDocXML);
                objDataWrapper.AddParameter("@DOC_TYPE", doctype);
                objDataWrapper.AddParameter("@CLAIM_ID", ClaimID);
                objDataWrapper.AddParameter("@ACTIVITY_ID", ActivityID);
                int retVal = objDataWrapper.ExecuteNonQuery("Proc_UpdatePolicyDocumentXML");
                objDataWrapper.ClearParameteres();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objDataWrapper.Dispose();
            }
        }



	}
}
