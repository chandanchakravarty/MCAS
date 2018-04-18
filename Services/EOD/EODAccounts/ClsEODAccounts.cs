using System;
using Cms.BusinessLayer.BlAccount  ;
using Cms.BusinessLayer.BlCommon;
using System.Threading ;
using EODCommon;
using System.Xml;
using Cms.DataLayer;
using Cms.BusinessLayer.BlProcess ;
using System.Data;

namespace EODAccounts
{
	/// <summary>
	/// Summary description for ClsEODAccounts.
	/// </summary>
	class ClsEFTThread:ClsEODCommon
	{

		private Thread objEFTThread;
		
		private void ProcessEFT()
		{
			ClsEODCommon.AccountingLocked = true ; 

			EODLogInfo objLog = new EODLogInfo();
			objLog.Activity = (int)EODActivities.Accounting;
			objLog.SubActivity = (int) EODActivities.EFT;
			objLog.StartDateTime = DateTime.Now;
			objLog.ActivtyDescription= "Starting EFT Processing";
			try
			{
				ClsEFT objEFT = new ClsEFT(EOD_USER_ID,CarrierSystemID)	;
				
				string XMLPath;
				XMLPath = AppDomain.CurrentDomain.BaseDirectory + "EODParameters.XML";
				XmlDocument configXML = new XmlDocument();
				configXML.Load(XMLPath);
				XmlNode EFTParameters = configXML.SelectSingleNode("Root/EFTParameters");
				
				objEFT.HostName = EFTParameters.SelectSingleNode("NACHAHostName").InnerText.Trim();
				objEFT.UserName = EFTParameters.SelectSingleNode("NACHAUserName").InnerText.Trim();
				objEFT.Password = EFTParameters.SelectSingleNode("NACHAPassword").InnerText.Trim();
				objEFT.FTPDirectoty= EFTParameters.SelectSingleNode("NACHARemoteDirectory").InnerText.Trim();
				objEFT.LocalDirectoty = EFTParameters.SelectSingleNode("NACHALocalUploadDirectory").InnerText.Trim();
				
				objEFT.Start();
				objLog.Status = ActivityStatus.SUCCEDDED ;
				objLog.EndDateTime = DateTime.Now;
				WriteLog(objLog);

			}
			catch(Exception ex)
			{
				objLog.Status = ActivityStatus.FAILED; 
				objLog.EndDateTime = DateTime.Now;
				objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
				ClsEODCommon.WriteLog(objLog);
				throw(ex);
			}
			finally
			{
				ClsEODCommon.AccountingLocked = false ; 

				try
				{
					objEFTThread.Abort();
				}
				catch(ThreadAbortException ex)
				{
					ClsEODAccounts.EFTProcessStatus = (int)EODStatus.Completed;
					objEFTThread = null;
				}
			}
		}

		public int StartThread()
		{
			try
			{
				if(objEFTThread == null)
				{
					objEFTThread = new Thread(new System.Threading.ThreadStart(ProcessEFT));
					objEFTThread.Priority = ThreadPriority.Normal;
					objEFTThread.Start();
				}
				else
				{
					if(objEFTThread.ThreadState == ThreadState.Suspended )
					{
						objEFTThread.Resume();
					}
					else if(objEFTThread.ThreadState == ThreadState.Stopped)
					{
						objEFTThread.Start();
					}
				}
				
			}
			catch(Exception ex)
			{
				throw(ex);
			}
			return 1;
		}

		public int PauseThread()
		{
			try
			{
				if(objEFTThread.ThreadState == ThreadState.Running)
				{
					objEFTThread.Suspend(); 
				}
			}
			catch(Exception ex)
			{
				throw(ex);
			}

			return 1;
		}

		public int StopThread()
		{
			try
			{
				objEFTThread.Abort();
			}
			catch(ThreadAbortException ex)
			{
				objEFTThread = null;
			}
			
			return 1;
		}

		
	}
	
	
	
	
	public class ClsEODAccounts: ClsEODCommon
	{
		private ClsEFTThread objEFTThread;
		private Thread objACCThread;
		public static int EFTProcessStatus;
		public static int ACCProcessStatus;
		public static int ACCSubActivity;

		static ClsEODAccounts()
		{
			EFTProcessStatus = 0;
			ACCProcessStatus = 0;
			ACCSubActivity   = 0;
		}
		public ClsEODAccounts()
		{
			
		}
				
		/// <summary>
		/// Start self managed thread EFT processing 
		/// </summary>
		/// <returns></returns>
		public int StartEFTProcess()
		{
			if(EFTProcessStatus != (int)EODStatus.InProgress)
			{
				objEFTThread= new ClsEFTThread();
				objEFTThread.StartThread();
			}
			EFTProcessStatus = (int) EODStatus.InProgress;
			return 1;			
		}
		public int StopEFTProcess()
		{
			objEFTThread.StopThread();
			objEFTThread = null;
			EFTProcessStatus = (int)EODStatus.NotStarted;
			return 1;
		}

		public int PauseEFTProcess()
		{
			objEFTThread.PauseThread();
			return 1;
		}


		/// <summary>
		/// Start Job Processor thread for Accounting tasks other than EFT processing
		/// </summary>
		public int StartAccountingThread()
		{
			try
			{
				if(objACCThread == null)
				{
					objACCThread = new Thread(new System.Threading.ThreadStart(AccountProcessing));
					objACCThread.Start();
				}
				else
				{
					if(objACCThread.ThreadState == ThreadState.Suspended)
					{
						objACCThread.Resume();
					}
					if(objACCThread.ThreadState == ThreadState.Stopped)
					{
						objACCThread.Start();
					}

				}

			}
			catch(Exception ex)
			{
				throw(ex);
			}


			return 1;
		}


		public bool SpoolPremiumNotices()
		{
			bool RetVal = false; 
			EODLogInfo objLog = new EODLogInfo();
			objLog.Activity = (int) EODActivities.Spooling;
			try
			{
				objLog.ActivtyDescription = "Adding records to Premium Notice Spool completed";
				objLog.SubActivity = (int) EODActivities.Spooling ; 
				objLog.StartDateTime = DateTime.Now;
				StartSpoolToPremiumNotice();
				
				RetVal = true; 
				
				objLog.Status = ActivityStatus.SUCCEDDED ;
				objLog.EndDateTime = DateTime.Now;
				ClsEODCommon.WriteLog(objLog);
				
			}
			catch(Exception ex)
			{
				objLog.Status = ActivityStatus.FAILED; 
				objLog.EndDateTime = DateTime.Now;
				objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
				ClsEODCommon.WriteLog(objLog);
			}
			return RetVal; 
		}

		public bool GeneratePremiumNotice()
		{
			bool RetVal = false; 

			EODLogInfo objLog = new EODLogInfo();
			objLog.Activity = (int) EODActivities.Accounting;
			try
			{
				objLog.SubActivity = (int) EODActivities.PremiumNoticeGeneration ;
				objLog.ActivtyDescription = "Premium Notice Generation process ends";
				objLog.StartDateTime = DateTime.Now;
				GeneratePremiumNotices();

				RetVal = true; 

				objLog.Status = ActivityStatus.SUCCEDDED;
				objLog.EndDateTime = DateTime.Now;
				ClsEODCommon.WriteLog(objLog);

				ACCProcessStatus = (int)EODStatus.Completed;
			
			}
			catch(Exception ex)
			{
				objLog.Status = ActivityStatus.FAILED; 
				objLog.EndDateTime = DateTime.Now;
				objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
				ClsEODCommon.WriteLog(objLog);
			}

			return RetVal; 
		}

		public void ReconCustomerNegativeItems()
		{
			EODLogInfo objLog = new EODLogInfo();
			objLog.Activity = (int) EODActivities.Accounting;
			objLog.SubActivity = (int) EODActivities.Accounting  ;
			objLog.ActivtyDescription = "Auto reconciling customer negative items";
			objLog.StartDateTime = DateTime.Now;

			try
			{
				AddCustomerNegativeItemsToSpool();
				CustomerOpenItemRecon();
				objLog.Status = ActivityStatus.SUCCEDDED;
				objLog.EndDateTime = DateTime.Now;
				ClsEODCommon.WriteLog(objLog);
			}
			catch(Exception ex)
			{
				objLog.Status = ActivityStatus.FAILED; 
				objLog.EndDateTime = DateTime.Now;
				objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
				ClsEODCommon.WriteLog(objLog);
			}
		}

		private void AccountProcessing()
		{
			ClsEODCommon.AccountingLocked = true;
			ACCProcessStatus = (int) EODStatus.InProgress;
			EODLogInfo objLog = new EODLogInfo();
			objLog.Activity = (int) EODActivities.Accounting;
			try
			{
				objLog.SubActivity = (int) EODActivities.RecurringJournalEntries ;
				objLog.ActivtyDescription = "Recurring Journal Entries";
				objLog.StartDateTime = DateTime.Now;
				StartRecurringJE();
				objLog.Status = ActivityStatus.SUCCEDDED;
				objLog.EndDateTime = DateTime.Now;
				ClsEODCommon.WriteLog(objLog);
			}
			catch(Exception ex)
			{
				objLog.Status = ActivityStatus.FAILED; 
				objLog.EndDateTime = DateTime.Now;
				objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
				ClsEODCommon.WriteLog(objLog);
			}

			try
			{

				objLog.SubActivity = (int) EODActivities.PreBillToNormal  ;
				objLog.ActivtyDescription = "Pre-bill to Normal postings";
				objLog.StartDateTime = DateTime.Now;
				StartPrebillToNormal();
				objLog.Status = ActivityStatus.SUCCEDDED;
				objLog.EndDateTime = DateTime.Now;
				ClsEODCommon.WriteLog(objLog);
			}
			catch(Exception ex)
			{
				objLog.Status = ActivityStatus.FAILED; 
				objLog.EndDateTime = DateTime.Now;
				objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
				ClsEODCommon.WriteLog(objLog);
			}

			try
			{

				objLog.SubActivity = (int) EODActivities.CreditCard  ;
				objLog.ActivtyDescription = "Batch Sweep Of Credit Card  ";
				objLog.StartDateTime = DateTime.Now;
				string Result = StartCreditCardSweep();
				objLog.ActivtyDescription+=Result;
				objLog.Status = ActivityStatus.SUCCEDDED;
				objLog.EndDateTime = DateTime.Now;
				ClsEODCommon.WriteLog(objLog);

			}
			catch(Exception ex)
			{
				objLog.Status = ActivityStatus.FAILED; 
				objLog.EndDateTime = DateTime.Now;
				objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
				ClsEODCommon.WriteLog(objLog);
			}

			
			ClsEODCommon.AccountingLocked = false;
			try
			{
				objACCThread.Abort();
			}
			catch(ThreadAbortException ex)
			{		
				objACCThread = null;
			}
		
		}

		private string StartCreditCardSweep()
		{
			ClsCreditCard objCreditCard = new ClsCreditCard();

			objCreditCard.PayPalAPI.UserName    = PayPalUserName;
			objCreditCard.PayPalAPI.VendorName  = PayPalVendor;
			objCreditCard.PayPalAPI.HostName    = PayPalHostName;
			objCreditCard.PayPalAPI.PartnerName = PaypalPartner;
			objCreditCard.PayPalAPI.Password 	  = PayPalPassword;

			return objCreditCard.StartBatchSweep(EOD_USER_ID);
		}

		private void StartRecurringJE()
		{
			ClsJournalEntryMaster objJE = new ClsJournalEntryMaster();
			ACCSubActivity = (int) EODStatus.ProcessingJE;
			objJE.ProcessRecurringJEs(ClsEODCommon.EOD_USER_ID);
		}

		private void  StartPrebillToNormal()
		{
			ACCSubActivity = (int) EODStatus.ProcessingPrebill ;
			ClsAccount.ProcessPrebillToNormalPosting();
		}

		private void CustomerOpenItemRecon()
		{
			ACCSubActivity = (int) EODStatus.ProcessingCustRecon ;
			ClsAccount.ReconCustomerOpenItems(EOD_USER_ID);
		}
		
		private void GeneratePremiumNotices()
		{

			EODLogInfo objLog = new EODLogInfo();
			objLog.Activity = (int)EODActivities.Accounting ;
			objLog.SubActivity = (int) EODActivities.PremiumNoticeGeneration ;

			int CustomerID, PolicyID, PolicyVersionID;
			string DueDate ;
			
			int SpoolID;

			string strCnnString = Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr;
			DataWrapper objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO , DataWrapper.SetAutoCommit.ON);
			objDataWrapper.ClearParameteres();
			try
			{
				objLog.ActivtyDescription = "Fetching Policies To Generate Premium notices : ";
				objLog.StartDateTime = System.DateTime.Now ;
				DataSet ds =  objDataWrapper.ExecuteDataSet("Proc_GetPoliciesToGeneratePremiumNotice");
				DataTable dtPolicies  = null;
				if(ds != null && ds.Tables.Count > 0 )
				{
					dtPolicies = ds.Tables[0];
					objLog.ActivtyDescription += dtPolicies.Rows.Count.ToString() + " Records to process";
					objLog.Status= ActivityStatus.SUCCEDDED;
					objLog.EndDateTime = DateTime.Now;
					WriteLog(objLog);
				}
				
				for(int i = 0; i< dtPolicies.Rows.Count; i++)
				{
					
					SpoolID = 0;

					CustomerID  = Convert.ToInt32(dtPolicies.Rows[i]["CUSTOMER_ID"]);
					PolicyID    = Convert.ToInt32(dtPolicies.Rows[i]["POLICY_ID"]);
					PolicyVersionID = Convert.ToInt32(dtPolicies.Rows[i]["POLICY_VERSION_ID"]);
					DueDate  = dtPolicies.Rows[i]["NOTICE_DUE_DATE"].ToString();
					SpoolID = Convert.ToInt32(dtPolicies.Rows[i]["SPOOL_ID"]);
			
					try
					{
						objLog.ActivtyDescription  = "Generating Premium Notice  : ";
						objLog.StartDateTime = DateTime.Now;
						objLog.ClientID = Convert.ToInt32(CustomerID);
						objLog.PolicyID = Convert.ToInt32(PolicyID);
						objLog.PolicyVersionID = PolicyVersionID; 
 
						ClsNotices objNotice = new ClsNotices();
						objNotice.GeneratePremiumNotice(CustomerID , PolicyID , PolicyVersionID , ClsEODCommon.CarrierSystemID  , DueDate);  

						objDataWrapper.ClearParameteres(); 
						objDataWrapper.AddParameter("@SPOOL_ID",SpoolID);
						objDataWrapper.ExecuteNonQuery("Proc_UpdtaePremiumNoticeSpool");
						objDataWrapper.ClearParameteres(); 


						objLog.Status= ActivityStatus.SUCCEDDED;
						objLog.EndDateTime = DateTime.Now;
						WriteLog(objLog);

					}
					catch(Exception ex)
					{
						objLog.Status = ActivityStatus.FAILED; 
						objLog.EndDateTime = DateTime.Now;
						objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
						WriteLog(objLog);
						continue;
					}
				}
	     
			}
			catch(Exception ex)
			{
				objLog.Status = ActivityStatus.FAILED; 
				objLog.EndDateTime = DateTime.Now;
				objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
				WriteLog(objLog);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		}

		public void StartBatchCommitOfDeposits()
		{
			EODLogInfo objLog = new EODLogInfo();
			objLog.Activity = (int) EODActivities.Accounting ;
			objLog.SubActivity = (int) EODActivities.Accounting ;
			objLog.ActivtyDescription = "Batch commit of Deposits.";

			objLog.StartDateTime = DateTime.Now;
			string strCnnString = Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr;
			DataWrapper objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES , DataWrapper.SetAutoCommit.ON);
			try
			{
				objDataWrapper.AddParameter("@COMMITTED_BY",EOD_USER_ID );
				objDataWrapper.AddParameter("@DATE_COMMITED",DateTime.Now );
				objDataWrapper.AddParameter("@CALLED_FOR","BATCH");
				
				objDataWrapper.ExecuteNonQuery("PROC_COMMIT_SPOOLED_DEPOSITS");
				
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				objLog.Status = ActivityStatus.FAILED; 
				objLog.EndDateTime = DateTime.Now;
				objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
				ClsEODCommon.WriteLog(objLog);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		}

		public void CommitSpooledDeposits()
		{
			EODLogInfo objLog = new EODLogInfo();
			objLog.Activity = (int) EODActivities.Accounting ;
			objLog.SubActivity = (int) EODActivities.Accounting ;
			objLog.ActivtyDescription = "Batch commit of Deposits.";

			objLog.StartDateTime = DateTime.Now;
			string strCnnString = Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr;
			DataWrapper objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES , DataWrapper.SetAutoCommit.ON);
			try
			{
				objDataWrapper.AddParameter("@COMMITTED_BY",EOD_USER_ID );
				objDataWrapper.AddParameter("@DATE_COMMITED",DateTime.Now );
				objDataWrapper.ExecuteNonQuery("PROC_COMMIT_SPOOLED_DEPOSITS");
				
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
//				//Log only if failed
//				objLog.Status = ActivityStatus.SUCCEDDED ; 
//				objLog.EndDateTime = DateTime.Now;
//				ClsEODCommon.WriteLog(objLog);
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				objLog.Status = ActivityStatus.FAILED; 
				objLog.EndDateTime = DateTime.Now;
				objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
				ClsEODCommon.WriteLog(objLog);
			}
			finally
			{
				objDataWrapper.Dispose();
			}
		}

		public void GeneratePostivePay()
		{
			EODLogInfo objLog = new EODLogInfo();
			objLog.Activity = (int)EODActivities.Accounting ;
			objLog.SubActivity = (int) EODActivities.PositivePayFileGeneraiion;  
			ClsAttachment lImpertionation =  new ClsAttachment();
			try
			{
				if (lImpertionation.ImpersonateUser(ClsCommon.ImpersonationUserId,ClsCommon.ImpersonationPassword,
					ClsCommon.ImpersonationDomain ))
				{
					string XMLPath;
					XMLPath = AppDomain.CurrentDomain.BaseDirectory + "EODParameters.XML";
					XmlDocument configXML = new XmlDocument();
					configXML.Load(XMLPath);
					XmlNode RTLParameters = configXML.SelectSingleNode("Root/PositivePayParameters");
					string LocalUploadDirectory = RTLParameters.SelectSingleNode("LocalUploadDirectory").InnerText.Trim();
						
					try
					{
				
						objLog.ActivtyDescription = "Generating Positive Pay file  ";
						objLog.StartDateTime = System.DateTime.Now ;
				
						DataSet objDataSet = new DataSet();
						Cms.BusinessLayer.BlAccount.ClsAccount objAcc = new Cms.BusinessLayer.BlAccount.ClsAccount();
						string strBuilder = "";
				
						objDataSet = objAcc.GetPositivePay(DateTime.Now);
		
						// 10  16     192503     240005 2008  1  3    173667.87WOLVERINE MUTUA
						if(objDataSet!=null)
						{
							objLog.ActivtyDescription =objLog.ActivtyDescription +  " , " + objDataSet.Tables.Count.ToString()  + " accounts to process. ";
							foreach(DataRow dr_Acc in objDataSet.Tables[0].Rows)
							{
								//Creating Files for Each Acct Number :
								string strAccNumber = dr_Acc["ACCOUNT_NUMBER"].ToString();
								//Time Stamp 
								string timeStamp = " " + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" +  DateTime.Now.Day;
								string CompleFileName = LocalUploadDirectory  + strAccNumber + timeStamp + ".txt";
								System.IO.TextWriter tx = new System.IO.StreamWriter(CompleFileName);

								foreach(DataRow dr in objDataSet.Tables[1].Rows)
								{
									strBuilder = "";
									//Writing Files accordingto ActNumbers
									if(dr_Acc["ACCOUNT_NUMBER"].ToString().Trim() == dr["ACCOUNT_NUMBER"].ToString().Trim())
									{
										strBuilder  = dr["DETAIL_INDICATOR"].ToString() + 
											dr["BANK_NUMBER"].ToString() + 
											dr["ACCOUNT_NUMBER"].ToString() +
											dr["CHECK_NUMBER"].ToString() +
											dr["ISSUED_DATE"].ToString() +
											dr["AMOUNT"].ToString() +
											dr["ADDITIONAL_DATA"].ToString()+
											dr["VOID"].ToString() ;
									}

									if(strBuilder!="")
										tx.WriteLine(strBuilder.ToString());
				
								}
								//Calculating Total
								foreach(DataRow dr_total in objDataSet.Tables[2].Rows)
								{
								
									strBuilder = "";
									//Writing Files accordingto ActNumbers
									if(dr_Acc["ACCOUNT_NUMBER"].ToString().Trim() == dr_total["ACCOUNT_NUMBER"].ToString().Trim())
									{
										strBuilder  = dr_total["DETAIL_INDICATOR"].ToString() +
											dr_total["BANK_NUMBER"].ToString() +
											dr_total["ACCOUNT_NUMBER"].ToString() + 
											dr_total["CHECK_NUMBER"].ToString() + 
											dr_total["FILE_DATE"].ToString() +
											dr_total["AMOUNT"].ToString() ;
							
									}

									if(strBuilder!="")
										tx.WriteLine(strBuilder.ToString());
				
								}

								////Calculating File Total Records
								foreach(DataRow dr_total_Rec in objDataSet.Tables[3].Rows)
								{
								
									strBuilder = "";
									//Writing Files accordingto ActNumbers
									if(dr_Acc["ACCOUNT_NUMBER"].ToString().Trim() == dr_total_Rec["ACCOUNT_NUMBER"].ToString().Trim())
									{
										strBuilder  = dr_total_Rec["DETAIL_INDICATOR"].ToString()  +
											dr_total_Rec["BANK_NUMBER"].ToString() +
											dr_total_Rec["FILLER"].ToString() +
											dr_total_Rec["CHECK_NUMBER"].ToString() +
											dr_total_Rec["FILE_DATE"].ToString()  +
											dr_total_Rec["AMOUNT"].ToString() ;
							
									}

									if(strBuilder!="")
										tx.WriteLine(strBuilder.ToString());
				
								}

								tx.Close();
							}
				
						}
						objLog.Status= ActivityStatus.SUCCEDDED;
						objLog.EndDateTime = DateTime.Now;
						WriteLog(objLog);

					}
					catch(Exception ex)
					{
						objLog.Status = ActivityStatus.FAILED ;
						objLog.EndDateTime = System.DateTime.Now;
						objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
						WriteLog(objLog);
					}
				}
			}
			catch(Exception ex)
			{
				objLog.Status = ActivityStatus.FAILED ;
				objLog.EndDateTime = System.DateTime.Now;
				objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
				WriteLog(objLog);
			}

		}
		/// <summary>
		/// Write off Small Balances (Will be called at End of Month)
		/// </summary>
		public void WriteOffSmallBalance()
		{
			EODLogInfo objLog = new EODLogInfo();
			objLog.Activity = (int)EODActivities.Accounting ;
			objLog.SubActivity = (int) EODActivities.SmallBalanceWriteOff  ;

			try
			{
				objLog.ActivtyDescription = "Writing Off Small Balances " ;
				objLog.StartDateTime  = DateTime.Now;
				ClsAccount objAccount = new ClsAccount();
				objAccount.ExecSmallBalanceWriteOff(DateTime.Now,EOD_USER_ID);
				objLog.Status = ActivityStatus.SUCCEDDED;
				objLog.EndDateTime = DateTime.Now;
				WriteLog(objLog);
			}
			catch(Exception ex)
			{
				objLog.Status = ActivityStatus.FAILED; 
				objLog.EndDateTime = DateTime.Now;
				objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
				WriteLog(objLog);
			}
 
		}

		/********************************************************************************
		 * Added by Ravindra (12-05-2007) : To Process Earned Premium at month end
		 * ******************************************************************************/
		/// <summary>
		/// To Process Earned Premium report. Will be called from End of Month
		/// </summary>
		/// <param name="Month">Month for which report to be processed</param>
		/// <param name="Year">Year for which report to be processed</param>
		public bool ProcessEarnedPremium(int Month, int Year)
		{
			bool ReturnStatus = false; 

			EODLogInfo objLog = new EODLogInfo();
			objLog.Activity = (int)EODActivities.Accounting ;
			objLog.SubActivity = (int) EODActivities.EarnedPremiumProcessing  ;

			string strCnnString = Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr;
			DataWrapper objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES  , DataWrapper.SetAutoCommit.ON);
			objDataWrapper.ClearParameteres();
			try
			{
				objLog.ActivtyDescription = "Processing Earned Premium for month :  " + Month.ToString()  + "  Year : " + Year.ToString();
				objLog.StartDateTime = System.DateTime.Now ;
				objDataWrapper.AddParameter("@MONTH",Month);
				objDataWrapper.AddParameter("@YEAR", Year);
				objDataWrapper.ExecuteNonQuery("Proc_ProcessEarnedPremiumReport");
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				ReturnStatus  = true;

				objLog.Status = ActivityStatus.SUCCEDDED;
				objLog.EndDateTime = DateTime.Now;
				WriteLog(objLog); 
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				objLog.Status = ActivityStatus.FAILED; 
				objLog.EndDateTime = DateTime.Now;
				objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
				WriteLog(objLog);
			}
			finally
			{
				objDataWrapper.Dispose();
			}

			return ReturnStatus ; 
		}

		/// <summary>
		/// To process Regular Commission . Will be called at End of Month
		/// </summary>

		public void ProcessAgencyCommission(int Month)
		{
			EODLogInfo objLog = new EODLogInfo();
			objLog.Activity = (int)EODActivities.Accounting ;
			objLog.SubActivity = (int) EODActivities.AgencyStatementRegular  ;
			try
			{
				objLog.ActivtyDescription = "Processing Agency Statement - Regular " ;
				objLog.StartDateTime  = DateTime.Now;
				ClsAgencyStatement objAgencyStatement = new ClsAgencyStatement();
				objAgencyStatement.SaveAgencyStatement(Month,DateTime.Now.Year,"REG",EOD_USER_ID);
				objLog.Status = ActivityStatus.SUCCEDDED;
				objLog.EndDateTime = DateTime.Now;
				WriteLog(objLog);
			}
			catch(Exception ex)
			{
				objLog.Status = ActivityStatus.FAILED; 
				objLog.EndDateTime = DateTime.Now;
				objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
				WriteLog(objLog);
			}
			
		}

		/// <summary>
		/// To process Additional Commission Will be called at Bi-Monthly
		/// </summary>
		public void ProcessAgencyAdditionalCommission(int Month)
		{
			EODLogInfo objLog = new EODLogInfo();
			objLog.Activity = (int)EODActivities.Accounting ;
			objLog.SubActivity = (int) EODActivities.AgencyStatementAdditional  ;
			try
			{
				objLog.ActivtyDescription = "Processing Agency Statement - Additional " ;
				objLog.StartDateTime  = DateTime.Now;
				ClsAgencyStatement objAgencyStatement = new ClsAgencyStatement();
				objAgencyStatement.SaveAgencyStatement(Month,DateTime.Now.Year , "ADC",EOD_USER_ID);
				objLog.Status = ActivityStatus.SUCCEDDED;
				objLog.EndDateTime = DateTime.Now;
				WriteLog(objLog);
			}
			catch(Exception ex)
			{
				objLog.Status = ActivityStatus.FAILED; 
				objLog.EndDateTime = DateTime.Now;
				objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
				WriteLog(objLog);
			}
		}

		
		
		#region Spooling Related Methods
		private void AddCustomerNegativeItemsToSpool()
		{
			string strCnnString = Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr;
			DataWrapper objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES  , DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.ClearParameteres();
			try
			{
				objDataWrapper.ExecuteNonQuery("Proc_AddCustomerNegativeItemsToSpool");
	     
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				objDataWrapper.Dispose();
				throw(ex);
			}
			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			objDataWrapper.Dispose();
		}
		
		public int StartSpoolToCreditCard()
		{
			string strCnnString = Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr;
			DataWrapper objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES  , DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.ClearParameteres();
			try
			{
				objDataWrapper.AddParameter("@USER_ID",EOD_USER_ID);
				objDataWrapper.ExecuteNonQuery("Proc_AddPoliciesToCreditCardSpool");
	     
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				objDataWrapper.Dispose();
				throw(ex);
			}
			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			objDataWrapper.Dispose();
			return 1;
		}

		public int StartSpoolToEFT()
		{
			string strCnnString = Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr;
			DataWrapper objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES  , DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.ClearParameteres();
			try
			{
				objDataWrapper.ExecuteNonQuery("Proc_AddPrenoteRecordsToEFTSpool");

				objDataWrapper.ExecuteNonQuery("Proc_AddPoliciesToEFTSpool");
	     
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				objDataWrapper.Dispose();
				throw(ex);
			}
			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			objDataWrapper.Dispose();
			return 1;
		}
		private void StartSpoolToPremiumNotice()
		{
			//Proc_AddPoliciesToPremiumNoticeSpool
			string strCnnString = Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr;
			DataWrapper objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES  , DataWrapper.SetAutoCommit.OFF);
			objDataWrapper.ClearParameteres();
			try
			{
				objDataWrapper.ExecuteNonQuery("Proc_AddPoliciesToPremiumNoticeSpool");
	     
			}
			catch(Exception ex)
			{
				objDataWrapper.RollbackTransaction(DataWrapper.CloseConnection.YES);
				objDataWrapper.Dispose();
				throw(ex);
			}
			objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);
			objDataWrapper.Dispose();

		}
		#endregion
		
	}
}
