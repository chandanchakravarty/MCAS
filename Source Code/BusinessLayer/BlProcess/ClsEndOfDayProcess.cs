/******************************************************************************************
<Author				: -		Vijay Arora
<Start Date			: -		01-02-2006
<End Date			: -		
<Description		: - 	Buisness Layer for End of the Day Process.
<Review Date		: - 
<Reviewed By		: - 	
*******************************************************************************************/ 
using System;
using System.Data;
using System.Data.SqlClient;
using Cms.DataLayer;
using Cms.Model.Policy.Process;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Diary;
using Cms.Model.Policy;

namespace Cms.BusinessLayer.BlProcess
{
	/// <summary>
	/// Summary description for ClsEndOfDayProcess.
	/// </summary>
	/// 
	

	public class ClsEndOfDayProcess : ClsPolicyProcess
	{
		
		#region Local Variable Declarations
		private int userID = 0;
		private ClsEODTransactionMasterInfo objMasterInfo;
		private ClsEODTransactionDetailInfo objDetailInfo;
		private int MasterRowID = 0;
		//private int DetailRowID = 0;
		private int returnResult = 0;
		#endregion

		#region Const Declartion
		private const string EOD_COMPLETE = "COMPLETE";
		private const string EOD_PENDING = "PENDING";
		private const string EOD_ERROR = "ERROR";

		private const int EOD_DIARY_REMINDER = 1;
		private const int EOD_DR_POLICY_REVIEW = 2;
		private const int EOD_NON_RENEW = 3;
		private const int EOD_CANCELLATION = 4;
		private const int EOD_RENEWAL = 5;
		private const int EOD_RECUR_JENTRY = 6;
		private const int EOD_PBILL_NORMAL = 7;

		#endregion
		
		#region Constructor
		public ClsEndOfDayProcess()
		{}
		#endregion

		#region Properties
		public int CurrentUserID
		{
			get
			{
				return userID;
			}
			set
			{
				userID = value;
			}
		
		}
		#endregion

		#region EOD Diary Reminder 

		/// <summary>
		/// It will select and send the diary reminder for pending processes.
		/// </summary>
		public void SendPendingProcessDiaryReminder()
		{
			
			try
			{

				//Insert the Master Entry
				objMasterInfo = SetMasterModel(EOD_DIARY_REMINDER,EOD_PENDING,0);
				MasterRowID = InsertMasterEntry(objMasterInfo);
				
				base.BeginTransaction();
			
				DataSet dsTemp = GetProcessPendingDiaryReminder();
				int CustomerID = 0, PolicyID = 0, PolicyVersionID = 0, returnResult = 0, RowID = 0;
				
				
			
				if (dsTemp.Tables[0].Rows.Count > 0)
				{
					for (int count = 0; count < dsTemp.Tables[0].Rows.Count;count++)
					{
						CustomerID = Convert.ToInt32(dsTemp.Tables[0].Rows[count]["CUSTOMER_ID"].ToString());
						PolicyID = Convert.ToInt32(dsTemp.Tables[0].Rows[count]["POLICY_ID"].ToString());
						PolicyVersionID = Convert.ToInt32(dsTemp.Tables[0].Rows[count]["POLICY_VERSION_ID"].ToString());
						RowID = Convert.ToInt32(dsTemp.Tables[0].Rows[count]["ROW_ID"].ToString());

						DiaryReminders(CustomerID,PolicyID,PolicyVersionID,RowID,0);

					}
				}
				
				//Update the Master Entry
				objMasterInfo = SetMasterModel(EOD_DIARY_REMINDER,EOD_COMPLETE,MasterRowID);
				returnResult = UpdateMasterEntry(objMasterInfo);
				base.CommitTransaction();
			}
			catch//(Exception ex)
			{
				base.RollbackTransaction();
			}
		}


		/// <summary>
		/// Send the Diary Reminders.
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <param name="PolicyID"></param>
		/// <param name="PolicyVersionID"></param>
		/// <param name="RowID"></param>
		/// <param name="DetailRowID"></param>
		private void DiaryReminders(int CustomerID, int PolicyID, int PolicyVersionID, int RowID, int DetailRowID)
		{
		
			TodolistInfo objToDo; 
			ClsDiary objDiary = new ClsDiary();
			int UnderWriter = 0;
			
			UnderWriter = GetPolicyUnderWriter(CustomerID, PolicyID,PolicyVersionID);
			objToDo = SetToDoModel();	
			objToDo.CUSTOMER_ID = CustomerID;
			objToDo.POLICY_ID = PolicyID;
			objToDo.POLICY_VERSION_ID = PolicyVersionID; 
			objToDo.TOUSERID = 	UnderWriter;
			objToDo.LISTTYPEID = 15;
			objToDo.SUBJECTLINE = "Process has been pending for this Policy";
						
			try
			{
				if (DetailRowID == 0)
				{
					//Insert the Detail Entry
					objDetailInfo = SetDetailModel(EOD_DIARY_REMINDER,CustomerID,PolicyID,PolicyVersionID,EOD_PENDING,null,0,0,RowID);
					DetailRowID = InsertDetailEntry(objDetailInfo); 
				}

				//Execute the Statement
				returnResult = objDiary.AddPolicyEntry(objToDo);
							
				//Update the Diary Entry ID in Process Table.
				UpdateDiaryListID(CustomerID, PolicyID, PolicyVersionID,RowID,returnResult);
							
				//Update the Detail Entry
				objDetailInfo = SetDetailModel(EOD_DIARY_REMINDER,CustomerID,PolicyID,PolicyVersionID,EOD_COMPLETE,null,DetailRowID,0,RowID);
				returnResult = UpdateDetailEntry(objDetailInfo);
			}
			catch (Exception ex)
			{
				int result = 0;
				objDetailInfo = SetDetailModel(EOD_DIARY_REMINDER,objDetailInfo.CUSTOMER_ID,objDetailInfo.POLICY_ID,objDetailInfo.POLICY_VERSION_ID,EOD_ERROR,ex.Message,DetailRowID,0,RowID);
				result = UpdateDetailEntry(objDetailInfo);
			}
		
		}


		/// <summary>
		/// It will set the Diary Model Entries.
		/// </summary>
		/// <returns></returns>
		private TodolistInfo SetToDoModel()
		{
			TodolistInfo objToDo = new TodolistInfo();
			objToDo.RECDATE = DateTime.Now;
			objToDo.FOLLOWUPDATE = DateTime.Now;
			objToDo.LISTOPEN = "Y";
			objToDo.PRIORITY = "M";
			

			return objToDo;
		}



		/// <summary>
		/// Returns the Pending Process Diary Reminder Records.
		/// </summary>
		/// <returns></returns>
		private DataSet GetProcessPendingDiaryReminder()
		{
			
			try
			{
				objWrapper.ClearParameteres();
				DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetProcessPendingDiaryReminder");
				objWrapper.ClearParameteres();
				return dsTemp;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}


		
		/// <summary>
		/// Returns the underwriter of policy.
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <param name="PolicyID"></param>
		/// <param name="PolicyVersionID"></param>
		/// <returns></returns>
		private int GetPolicyUnderWriter(int CustomerID, int PolicyID, int PolicyVersionID)
		{
			
			try
			{
				int result = 0;
				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
				objWrapper.AddParameter("@POLICY_ID",PolicyID);
				objWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID);
			
				DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetPolicyUnderWriter");
				objWrapper.ClearParameteres();
		
				if (dsTemp.Tables[0].Rows.Count > 0)
				{
					result = Convert.ToInt32(dsTemp.Tables[0].Rows[0][0].ToString());
				}
				
				return result;
				
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}


		private void UpdateDiaryListID(int CustomerID, int PolicyID, int PolicyVersionID, int RowID, int DairyListID)
		{
			
			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
			objWrapper.AddParameter("@POLICY_ID",PolicyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID);
			objWrapper.AddParameter("@ROW_ID",RowID);
			objWrapper.AddParameter("@DIARY_LIST_ID",DairyListID);

			int result = objWrapper.ExecuteNonQuery("Proc_UpdateDiaryReminderID");
			objWrapper.ClearParameteres();
		
		}

		#endregion

		#region Policy Renewal Review Diary Reminder
		/// <summary>
		/// Returns the dataset contains records for policy renewal diary reminder to be send.
		/// </summary>
		/// <returns></returns>
		private DataSet GetPolicyRenewalDiaryReminder()
		{
			
			try
			{
				objWrapper.ClearParameteres();
				DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetPolicyRenewalDiaryReminder");
				objWrapper.ClearParameteres();
				return dsTemp;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}

		/// <summary>
		/// Update the status of Policy Renewal Review or not.
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <param name="PolicyID"></param>
		/// <param name="PolicyVersionID"></param>
		private void UpdatePolicyRenewalReview(int CustomerID, int PolicyID, int PolicyVersionID)
		{
			
			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
			objWrapper.AddParameter("@POLICY_ID",PolicyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID);

			int result = objWrapper.ExecuteNonQuery("Proc_UpdateRenewalReview");
			objWrapper.ClearParameteres();
		
		}

		/// <summary>
		/// Send the Diary Reminder for Policy Renewal Review.
		/// </summary>
		public void SendPolicyRenewalReviewDiaryReminder()
		{
			try
			{

				//Insert the Master Entry
				objMasterInfo = SetMasterModel(EOD_DR_POLICY_REVIEW,EOD_PENDING,0);
				MasterRowID = InsertMasterEntry(objMasterInfo);

				base.BeginTransaction();
			
				DataSet dsTemp = GetPolicyRenewalDiaryReminder();
				int CustomerID = 0, PolicyID = 0, PolicyVersionID = 0,returnResult = 0;
			
				if (dsTemp.Tables[0].Rows.Count > 0)
				{
					for (int count = 0; count < dsTemp.Tables[0].Rows.Count;count++)
					{
						CustomerID = Convert.ToInt32(dsTemp.Tables[0].Rows[count]["CUSTOMER_ID"].ToString());
						PolicyID = Convert.ToInt32(dsTemp.Tables[0].Rows[count]["POLICY_ID"].ToString());
						PolicyVersionID = Convert.ToInt32(dsTemp.Tables[0].Rows[count]["POLICY_VERSION_ID"].ToString());

						ReviewDiaryReminder(CustomerID,PolicyID,PolicyVersionID,0);
						
					}
				}

				//Update the Master Entry
				objMasterInfo = SetMasterModel(EOD_DR_POLICY_REVIEW,EOD_COMPLETE,MasterRowID);
				returnResult = UpdateMasterEntry(objMasterInfo);

				base.CommitTransaction();
			}
			catch//(Exception ex)
			{
				base.RollbackTransaction();
			}
		}



		/// <summary>
		/// Send the Policy Renewal Diary Reminder.
		/// </summary>
		/// <param name="CustomerID"></param>
		/// <param name="PolicyID"></param>
		/// <param name="PolicyVersionID"></param>
		/// <param name="DetailRowID"></param>
		private void ReviewDiaryReminder(int CustomerID, int PolicyID, int PolicyVersionID, int DetailRowID)
		{
			
			TodolistInfo objToDo; 
			ClsDiary objDiary = new ClsDiary();
			int UnderWriter = 0;

			UnderWriter = GetPolicyUnderWriter(CustomerID, PolicyID,PolicyVersionID);

			objToDo = SetToDoModel();	
			objToDo.CUSTOMER_ID = CustomerID;
			objToDo.POLICY_ID = PolicyID;
			objToDo.POLICY_VERSION_ID = PolicyVersionID; 
			objToDo.TOUSERID = 	UnderWriter;
			objToDo.LISTTYPEID = 16;
			objToDo.SUBJECTLINE = "Policy Renewal Review Request before Expiration";
						
			try
			{
				if (DetailRowID == 0)
				{
					//Insert the Detail Entry
					objDetailInfo = SetDetailModel(EOD_DR_POLICY_REVIEW,CustomerID,PolicyID,PolicyVersionID,EOD_PENDING,null,0,0,0);
					DetailRowID = InsertDetailEntry(objDetailInfo); 
				}

				returnResult = objDiary.AddPolicyEntry(objToDo);
						
				if (returnResult > 0)
				{
					UpdatePolicyRenewalReview(CustomerID,PolicyID,PolicyVersionID);	
				}

				//Update the Detail Entry
				objDetailInfo = SetDetailModel(EOD_DR_POLICY_REVIEW,CustomerID,PolicyID,PolicyVersionID,EOD_COMPLETE,null,DetailRowID,0,0);
				returnResult = UpdateDetailEntry(objDetailInfo);
			}
			catch(Exception ex)
			{
				int result = 0;
				objDetailInfo = SetDetailModel(EOD_DR_POLICY_REVIEW,objDetailInfo.CUSTOMER_ID,objDetailInfo.POLICY_ID,objDetailInfo.POLICY_VERSION_ID,EOD_ERROR,ex.Message,DetailRowID,0,0);
				result = UpdateDetailEntry(objDetailInfo);
						
			}
		
		}

		#endregion

		#region Auto Trigger of Non-Renew Process

		/// <summary>
		/// Returns the records on which the non renew process has been launched.
		/// </summary>
		/// <returns></returns>
		private DataSet GetExpiredPolicy()
		{
			
			try
			{
				base.BeginTransaction();
				objWrapper.ClearParameteres();
				DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetAutoTriggerNonRenewPolicy");
				objWrapper.ClearParameteres();
				base.CommitTransaction();
				return dsTemp;
			}
			catch(Exception ex)
			{
				base.RollbackTransaction();
				throw(ex);
			}
		}


		/// <summary>
		/// Auto trigger the Non Renew Process.
		/// </summary>
		public void LaunchNonRenewProcess()
		{
		
			//Insert the Master Entry
			objMasterInfo = SetMasterModel(EOD_NON_RENEW,EOD_PENDING,0);
			MasterRowID = InsertMasterEntry(objMasterInfo);
			int CustomerID = 0, PolicyID = 0, PolicyVersionID = 0;			
			
			DataSet dsTemp = GetExpiredPolicy();

			if (dsTemp.Tables[0].Rows.Count > 0)
			{
				for (int count = 0; count < dsTemp.Tables[0].Rows.Count; count++)
				{
					CustomerID = int.Parse(dsTemp.Tables[0].Rows[count]["CUSTOMER_ID"].ToString());	
					PolicyID = int.Parse(dsTemp.Tables[0].Rows[count]["POLICY_ID"].ToString());	
					PolicyVersionID = int.Parse(dsTemp.Tables[0].Rows[count]["POLICY_VERSION_ID"].ToString());	

					SetNonRenewProcess(CustomerID, PolicyID,PolicyVersionID,0);
				}
				
			}

			//Update the Master Entry
			objMasterInfo = SetMasterModel(EOD_NON_RENEW,EOD_COMPLETE,MasterRowID);
			returnResult = UpdateMasterEntry(objMasterInfo);

		}


		private void SetNonRenewProcess(int CustomerID, int PolicyID, int PolicyVersionID,int DetailRowID)
		{
			Cms.Model.Policy.Process.ClsProcessInfo  objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
			ClsNonRenewProcess  objProcess = new ClsNonRenewProcess();
			bool retval = false;
		
			objProcessInfo.CUSTOMER_ID = CustomerID;
			objProcessInfo.POLICY_ID = PolicyID;
			objProcessInfo.POLICY_VERSION_ID = PolicyVersionID;
			objProcessInfo.CREATED_BY = userID;
			objProcessInfo.CREATED_DATETIME = DateTime.Now;

			try
			{
						
				if (DetailRowID == 0)
				{
					//Insert the Detail Entry
					objDetailInfo = SetDetailModel(EOD_NON_RENEW,objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID,EOD_PENDING,null,0,0,0);
					DetailRowID = InsertDetailEntry(objDetailInfo); 
				}

				retval = objProcess.StartProcess(objProcessInfo);

				if (retval == true)
				{
					objProcessInfo.COMPLETED_BY = userID;
					objProcessInfo.COMPLETED_DATETIME = DateTime.Now;
					retval = objProcess.CommitProcess(objProcessInfo);
				}
						
				//Update the Detail Entry
				objDetailInfo = SetDetailModel(EOD_NON_RENEW,objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID,EOD_COMPLETE,null,DetailRowID,0,0);
				returnResult = UpdateDetailEntry(objDetailInfo);
			}
			catch(Exception ex)
			{
				int result = 0;
				objDetailInfo = SetDetailModel(EOD_NON_RENEW,objDetailInfo.CUSTOMER_ID,objDetailInfo.POLICY_ID,objDetailInfo.POLICY_VERSION_ID,EOD_ERROR,ex.Message,DetailRowID,0,0);
				result = UpdateDetailEntry(objDetailInfo);
						
			}
		}

		
		#endregion

		#region Auto Trigger of Cancellation Process

		/// <summary>
		/// Returns the records on which the cancellation process has been launched.
		/// </summary>
		/// <returns></returns>
		private DataSet GetCancelledPolicy()
		{
			
			try
			{
				base.BeginTransaction();
				objWrapper.ClearParameteres();
				DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetAutoTriggerCancelPolicy");
				objWrapper.ClearParameteres();
				base.CommitTransaction();
				return dsTemp;
			}
			catch(Exception ex)
			{
				base.RollbackTransaction();
				throw(ex);
			}
		}


		/// <summary>
		/// Auto trigger the Cancellation Process.
		/// </summary>
		public void LaunchCancellationProcess()
		{
		
			//Insert the Master Entry
			objMasterInfo = SetMasterModel(EOD_CANCELLATION,EOD_PENDING,0);
			MasterRowID = InsertMasterEntry(objMasterInfo);
						
			int CustomerID = 0, PolicyID = 0, PolicyVersionID = 0;
			DataSet dsTemp = GetCancelledPolicy();

			if (dsTemp.Tables[0].Rows.Count > 0)
			{
				for (int count = 0; count < dsTemp.Tables[0].Rows.Count; count++)
				{
					CustomerID = int.Parse(dsTemp.Tables[0].Rows[count]["CUSTOMER_ID"].ToString());	
					PolicyID = int.Parse(dsTemp.Tables[0].Rows[count]["POLICY_ID"].ToString());	
					PolicyVersionID = int.Parse(dsTemp.Tables[0].Rows[count]["POLICY_VERSION_ID"].ToString());	
				
					SetCancellationProcess(CustomerID, PolicyID, PolicyVersionID,0);

				}
				
			}

			//Update the Master Entry
			objMasterInfo = SetMasterModel(EOD_CANCELLATION,EOD_COMPLETE,MasterRowID);
			returnResult = UpdateMasterEntry(objMasterInfo);

		}


		private void SetCancellationProcess(int CustomerID, int PolicyID, int PolicyVersionID, int DetailRowID)
		{
		
			Cms.Model.Policy.Process.ClsProcessInfo  objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
			ClsCancellationProcess  objProcess = new ClsCancellationProcess();
			bool retval = false;
			
			objProcessInfo.CUSTOMER_ID = CustomerID;
			objProcessInfo.POLICY_ID = PolicyID;
			objProcessInfo.POLICY_VERSION_ID = PolicyVersionID;
			objProcessInfo.CREATED_BY = userID;
			objProcessInfo.CREATED_DATETIME = DateTime.Now;

			try
			{
						
				if (DetailRowID == 0)
				{
					//Insert the Detail Entry
					objDetailInfo = SetDetailModel(EOD_CANCELLATION,objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID,EOD_PENDING,null,0,0,0);
					DetailRowID = InsertDetailEntry(objDetailInfo); 
				}


				retval = objProcess.StartProcess(objProcessInfo);

				if (retval == true)
				{
					objProcessInfo.COMPLETED_BY = userID;
					objProcessInfo.COMPLETED_DATETIME = DateTime.Now;
					retval = objProcess.CommitProcess(objProcessInfo);
				}

				//Update the Detail Entry
				objDetailInfo = SetDetailModel(EOD_CANCELLATION,objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID,EOD_COMPLETE,null,DetailRowID,0,0);
				returnResult = UpdateDetailEntry(objDetailInfo);
			}
			catch(Exception ex)
			{
				int result = 0;
				objDetailInfo = SetDetailModel(EOD_CANCELLATION,objDetailInfo.CUSTOMER_ID,objDetailInfo.POLICY_ID,objDetailInfo.POLICY_VERSION_ID,EOD_ERROR,ex.Message,DetailRowID,0,0);
				result = UpdateDetailEntry(objDetailInfo);
			}
		
		}
		
		#endregion

		#region Auto Trigger of Renewal Process

		/// <summary>
		/// Returns the records on which the renewal process has been launched.
		/// </summary>
		/// <returns></returns>
		private DataSet GetRenewalPolicy()
		{
			
			try
			{
				base.BeginTransaction();
				objWrapper.ClearParameteres();
				DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetAutoTriggerRenewalPolicy");
				objWrapper.ClearParameteres();
				base.CommitTransaction();
				return dsTemp;
			}
			catch(Exception ex)
			{
				base.RollbackTransaction();
				throw(ex);
			}
		}


		/// <summary>
		/// Auto trigger the Renewal Process.
		/// </summary>
		public void LaunchRenewalProcess()
		{
		
			//Insert the Master Entry
			objMasterInfo = SetMasterModel(EOD_RENEWAL,EOD_PENDING,0);
			MasterRowID = InsertMasterEntry(objMasterInfo);
			
			int CustomerID = 0, PolicyID = 0, PolicyVersionID = 0;
			DataSet dsTemp = GetRenewalPolicy();

			if (dsTemp.Tables[0].Rows.Count > 0)
			{
				for (int count = 0; count < dsTemp.Tables[0].Rows.Count; count++)
				{
					
					CustomerID = int.Parse(dsTemp.Tables[0].Rows[count]["CUSTOMER_ID"].ToString());	
					PolicyID  = int.Parse(dsTemp.Tables[0].Rows[count]["POLICY_ID"].ToString());	
					PolicyVersionID = int.Parse(dsTemp.Tables[0].Rows[count]["POLICY_VERSION_ID"].ToString());	

					SetRenewalProcess(CustomerID, PolicyID, PolicyVersionID, 0);
				
				}
				
			}

			//Update the Master Entry
			objMasterInfo = SetMasterModel(EOD_RENEWAL,EOD_COMPLETE,MasterRowID);
			returnResult = UpdateMasterEntry(objMasterInfo);
		}
		

		private void SetRenewalProcess(int CustomerID, int PolicyID, int PolicyVersionID, int DetailRowID)
		{
			
			Cms.Model.Policy.Process.ClsProcessInfo  objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
			ClsRenewalProcess  objProcess = new ClsRenewalProcess();
			bool retval = false;

			objProcessInfo.CUSTOMER_ID = CustomerID;
			objProcessInfo.POLICY_ID = PolicyID;
			objProcessInfo.POLICY_VERSION_ID = PolicyVersionID;
			objProcessInfo.CREATED_BY = userID;
			objProcessInfo.CREATED_DATETIME = DateTime.Now;

			try
			{
				
				if (DetailRowID == 0)
				{
					//Insert the Detail Entry
					objDetailInfo = SetDetailModel(EOD_RENEWAL,objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID,EOD_PENDING,null,0,0,0);
					DetailRowID = InsertDetailEntry(objDetailInfo);
				}
						
				retval = objProcess.StartProcess(objProcessInfo);

				if (retval == true)
				{
					objProcessInfo.COMPLETED_BY = userID;
					objProcessInfo.COMPLETED_DATETIME = DateTime.Now;
					retval = objProcess.CommitProcess(objProcessInfo);
				}

				//Update the Detail Entry
				objDetailInfo = SetDetailModel(EOD_RENEWAL,objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID,EOD_COMPLETE,null,DetailRowID,0,0);
				returnResult = UpdateDetailEntry(objDetailInfo);
			}
			catch(Exception ex)
			{
				int result = 0;
				objDetailInfo = SetDetailModel(EOD_RENEWAL,objDetailInfo.CUSTOMER_ID,objDetailInfo.POLICY_ID,objDetailInfo.POLICY_VERSION_ID,EOD_ERROR,ex.Message,DetailRowID,0,0);
				result = UpdateDetailEntry(objDetailInfo);
			}
		}

		#endregion

		#region Auto Posting of Recurring Journal Entries
		/// <summary>
		/// Gets the Recurring Journal Entries.
		/// </summary>
		/// <returns></returns>
		private DataSet GetRecurringJournalEntries()
		{
			try
			{
				base.BeginTransaction();
				objWrapper.ClearParameteres();
				DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetRecurJournalEntries");
				objWrapper.ClearParameteres();
				base.CommitTransaction();
				return dsTemp;
			}
			catch(Exception ex)
			{
				base.RollbackTransaction();
				throw(ex);
			}
		}


		/// <summary>
		/// Lauch the Recurring Journal Entries by EOD.
		/// </summary>
		public void LaunchRecurringJournalEntries()
		{
		
			DataSet dsTemp = GetRecurringJournalEntries();
			int JournalID = 0, DayOfWk = 0, Frequency = 0;
			DateTime EndDate = DateTime.MinValue, LastValidPostingDate = DateTime.MinValue;
			
			try
			{
				//Insert the Master Entry
				objMasterInfo = SetMasterModel(EOD_RECUR_JENTRY,EOD_PENDING,0);
				MasterRowID = InsertMasterEntry(objMasterInfo);

				base.BeginTransaction();
			
				if (dsTemp.Tables[0].Rows.Count > 0)
				{
					for (int count = 0; count < dsTemp.Tables[0].Rows.Count; count++)
					{
						
						JournalID = Convert.ToInt32(dsTemp.Tables[0].Rows[count]["JOURNAL_ID"].ToString());
						Frequency = Convert.ToInt32(dsTemp.Tables[0].Rows[count]["FREQUENCY"].ToString());
						DayOfWk = Convert.ToInt32(dsTemp.Tables[0].Rows[count]["DAY_OF_THE_WK"].ToString());
						EndDate = Convert.ToDateTime(dsTemp.Tables[0].Rows[count]["END_DATE"].ToString());
						LastValidPostingDate = Convert.ToDateTime(dsTemp.Tables[0].Rows[count]["LAST_VALID_POSTING_DATE"].ToString());

						SetRecurrJournalEntry(Frequency, EndDate, LastValidPostingDate, DayOfWk, JournalID, 0);
				
					}
				
				}

				//Update the Master Entry
				objMasterInfo = SetMasterModel(EOD_RECUR_JENTRY,EOD_COMPLETE,MasterRowID);
				returnResult = UpdateMasterEntry(objMasterInfo);
				
				base.CommitTransaction();
			}
			catch(Exception ex)
			{
				base.RollbackTransaction();
				throw(ex);
			}
		}



		private void SetRecurrJournalEntry(int Frequency, DateTime EndDate, DateTime LastValidPostingDate, int DayOfWeek, int JournalID, int DetailRowID)
		{
		
				
				objWrapper.ClearParameteres();
				objWrapper.AddParameter("@FREQUENCY",Frequency);
				objWrapper.AddParameter("@END_DATE",EndDate);
				objWrapper.AddParameter("@LAST_VALID_POSTING_DATE",LastValidPostingDate);
				objWrapper.AddParameter("@DAY_OF_WK",DayOfWeek);
				objWrapper.AddParameter("@JOURNAL_ID",JournalID);
				
				try
				{
					if (DetailRowID == 0)
					{
						//Insert the Detail Entry
						objDetailInfo = SetDetailModel(EOD_RECUR_JENTRY,0,0,0,EOD_PENDING,null,0,JournalID,0);
						DetailRowID = InsertDetailEntry(objDetailInfo);
					}
							
					returnResult	= objWrapper.ExecuteNonQuery("Proc_PostAutoRecurJournalEntries");
					objWrapper.ClearParameteres();
								
					//Update the Detail Entry
					objDetailInfo = SetDetailModel(EOD_RECUR_JENTRY,0,0,0,EOD_COMPLETE,null,DetailRowID,JournalID,0);
					returnResult = UpdateDetailEntry(objDetailInfo);
				}
				catch(Exception ex)
				{
					int result = 0;
					objDetailInfo = SetDetailModel(EOD_RECUR_JENTRY,0,0,0,EOD_ERROR,ex.Message,DetailRowID,JournalID,0);
					result = UpdateDetailEntry(objDetailInfo);
				}
		}


		private DataSet GetRecurJournalEntryDetail(int JournalID)
		{
			try
			{
				base.BeginTransaction();
				objWrapper.ClearParameteres();

				objWrapper.AddParameter("@JOURNAL_ID",JournalID);
				DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetRecurringEntryDetails");
				objWrapper.ClearParameteres();
				base.CommitTransaction();
				return dsTemp;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}


		#endregion

		#region Auto Trigger of Moving Prebill to Normal Postings 

		/// <summary>
		/// Returns the records of which prebill posting has to move in normal postings
		/// </summary>
		/// <returns></returns>
		private DataSet GetPrebillToNormalPostingPolicy()
		{
			
			try
			{
				base.BeginTransaction();
				objWrapper.ClearParameteres();
				DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetPolicyForPrebillToNormal");
				objWrapper.ClearParameteres();
				base.CommitTransaction();
				return dsTemp;
			}
			catch(Exception ex)
			{
				base.RollbackTransaction();
				throw(ex);
			}
		}


		/// <summary>
		/// Auto trigger the Renewal Process.
		/// </summary>
		public void LaunchMovingPrebillToNormal()
		{
			
			Cms.Model.Policy.Process.ClsProcessInfo  objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
			int CustomerID = 0, PolicyID = 0, PolicyVersionID = 0;
			ClsRenewalProcess  objProcess = new ClsRenewalProcess();
			DataSet dsTemp = GetPrebillToNormalPostingPolicy();

			//Insert the Master Entry
			objMasterInfo = SetMasterModel(EOD_PBILL_NORMAL,EOD_PENDING,0);
			MasterRowID = InsertMasterEntry(objMasterInfo);
			
			base.BeginTransaction();

			try
			{
				if (dsTemp.Tables[0].Rows.Count > 0)
				{
					for (int count = 0; count < dsTemp.Tables[0].Rows.Count; count++)
					{
						
							CustomerID = int.Parse(dsTemp.Tables[0].Rows[count]["CUSTOMER_ID"].ToString());	
							PolicyID = int.Parse(dsTemp.Tables[0].Rows[count]["POLICY_ID"].ToString());	
							PolicyVersionID = int.Parse(dsTemp.Tables[0].Rows[count]["POLICY_VERSION_ID"].ToString());
						
							SetMovingPrebillToNormal(CustomerID, PolicyID, PolicyVersionID, 0);		
					}
				
				}

				//Update the Master Entry
				objMasterInfo = SetMasterModel(EOD_PBILL_NORMAL,EOD_COMPLETE,MasterRowID);
				returnResult = UpdateMasterEntry(objMasterInfo);

				base.CommitTransaction();
			}
			catch(Exception ex)
			{
				base.RollbackTransaction();
				throw(ex);
			}

		}


		private void SetMovingPrebillToNormal(int CustomerID, int PolicyID, int PolicyVersionID, int DetailRowID)
		{
		
			objWrapper.ClearParameteres();
			objWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
			objWrapper.AddParameter("@POLICY_ID",PolicyID);
			objWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID);
							
			try
			{
				
				if (DetailRowID == 0)
				{
					//Insert the Detail Entry
					objDetailInfo = SetDetailModel(EOD_PBILL_NORMAL,CustomerID,PolicyID,PolicyVersionID,EOD_PENDING,null,0,0,0);
					DetailRowID = InsertDetailEntry(objDetailInfo);
				}

				returnResult = objWrapper.ExecuteNonQuery("Proc_GetAutoMovingPrebillToNormal"); 
				objWrapper.ClearParameteres();

				//Update the Detail Entry
				objDetailInfo = SetDetailModel(EOD_PBILL_NORMAL,CustomerID,PolicyID,PolicyVersionID,EOD_COMPLETE,null,DetailRowID,0,0);
				returnResult = UpdateDetailEntry(objDetailInfo);
			}
			catch(Exception ex)
			{
				int result = 0;
				objDetailInfo = SetDetailModel(EOD_PBILL_NORMAL,CustomerID,PolicyID,PolicyVersionID,EOD_ERROR,ex.Message,DetailRowID,0,0);
				result = UpdateDetailEntry(objDetailInfo);
			}
		
		}
		
		#endregion
		
		#region Insert and Update Master and Detail Tracking Entries
		/// <summary>
		/// Insert the Entry in EOD Master Table.
		/// </summary>
		/// <param name="objMaster"></param>
		/// <returns></returns>
		private int InsertMasterEntry(ClsEODTransactionMasterInfo objMaster)
		{
			string		strStoredProc	=	"Proc_InsertPOL_EOD_TRANSACTIONS_MASTER";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			int returnResult = 0;
		
			try
			{
								
				objDataWrapper.AddParameter("@TRANSACTION_ID",objMaster.TRANSACTION_ID);
				objDataWrapper.AddParameter("@DATE_OF_LAUNCH",objMaster.DATE_OF_LAUNCH);
				objDataWrapper.AddParameter("@STATUS",objMaster.STATUS);

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ROW_ID",SqlDbType.Int,ParameterDirection.Output);
				
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				if ( objSqlParameter.Value != System.DBNull.Value )
				{
					returnResult = Convert.ToInt32(objSqlParameter.Value);
				}
				
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


		/// <summary>
		/// Updates the Master Entry Table.
		/// </summary>
		/// <param name="objMaster"></param>
		/// <returns></returns>
		private int UpdateMasterEntry(ClsEODTransactionMasterInfo objMaster)
		{
			string		strStoredProc	=	"Proc_UpdatePOL_EOD_TRANSACTIONS_MASTER";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			int returnResult = 0;
		
			try
			{
								
				objDataWrapper.AddParameter("@ROW_ID",objMaster.ROW_ID);
				objDataWrapper.AddParameter("@STATUS",objMaster.STATUS);
				
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
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



		

		/// <summary>
		/// Insert the Entry in EOD Detail Table.
		/// </summary>
		/// <param name="objDetail"></param>
		/// <returns></returns>
		private int InsertDetailEntry(ClsEODTransactionDetailInfo objDetail)
		{
			string		strStoredProc	=	"Proc_InsertPOL_EOD_TRANSACTIONS_DETAIL";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			int returnResult = 0;
		
			try
			{
								
				objDataWrapper.AddParameter("@TRANSACTION_ID",objDetail.TRANSACTION_ID);
				objDataWrapper.AddParameter("@DATE_OF_LAUNCH",objDetail.DATE_OF_LAUNCH);
				objDataWrapper.AddParameter("@CUSTOMER_ID",objDetail.CUSTOMER_ID);
				objDataWrapper.AddParameter("@POLICY_ID",objDetail.POLICY_ID);
				objDataWrapper.AddParameter("@POLICY_VERSION_ID",objDetail.POLICY_VERSION_ID);
				objDataWrapper.AddParameter("@STATUS",objDetail.STATUS);
				objDataWrapper.AddParameter("@ERROR_DESCRIPTION",objDetail.ERROR_DESCRIPTION);
				objDataWrapper.AddParameter("@JOURNAL_ID",objDetail.JOURNAL_ID);
				objDataWrapper.AddParameter("@PROCESS_DIARY_LIST_ID",objDetail.PROCESS_DIARY_LIST_ID);

				SqlParameter objSqlParameter  = (SqlParameter) objDataWrapper.AddParameter("@ROW_ID",SqlDbType.Int,ParameterDirection.Output);
				
				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				if ( objSqlParameter.Value != System.DBNull.Value )
				{
					returnResult = Convert.ToInt32(objSqlParameter.Value);
				}
				
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


		/// <summary>
		/// Updates the Detail Entry.
		/// </summary>
		/// <param name="objDetail"></param>
		/// <returns></returns>
		private int UpdateDetailEntry(ClsEODTransactionDetailInfo objDetail)
		{
			string		strStoredProc	=	"Proc_UpdatePOL_EOD_TRANSACTIONS_DETAIL";
			DateTime	RecordDate		=	DateTime.Now;
			DataWrapper objDataWrapper = new DataWrapper(ConnStr,CommandType.StoredProcedure,DataWrapper.MaintainTransaction.YES,DataWrapper.SetAutoCommit.OFF);
			int returnResult = 0;
		
			try
			{
								
				objDataWrapper.AddParameter("@ROW_ID",objDetail.ROW_ID);
				objDataWrapper.AddParameter("@STATUS",objDetail.STATUS);
				objDataWrapper.AddParameter("@ERROR_DESCRIPTION",objDetail.ERROR_DESCRIPTION);

				returnResult	= objDataWrapper.ExecuteNonQuery(strStoredProc);
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




		/// <summary>
		/// Set the Master Model.
		/// </summary>
		/// <param name="TransID"></param>
		/// <param name="Status"></param>
		private ClsEODTransactionMasterInfo SetMasterModel(int TransID, string Status, int RowID)
		{
			try
			{
				ClsEODTransactionMasterInfo objMasterInfo = new ClsEODTransactionMasterInfo();
				objMasterInfo.TRANSACTION_ID = TransID;
				objMasterInfo.DATE_OF_LAUNCH = DateTime.Now; 
				objMasterInfo.STATUS = Status;
				objMasterInfo.ROW_ID = RowID;
				return objMasterInfo;
			}	
			catch(Exception ex)
			{
				throw(ex);
			}

		}

		/// <summary>
		/// Set the Model for Detail Entry.
		/// </summary>
		/// <param name="TransID"></param>
		/// <param name="CustomerID"></param>
		/// <param name="PolicyID"></param>
		/// <param name="PolicyVersionID"></param>
		/// <param name="Status"></param>
		/// <param name="ErrorDesc"></param>
		/// <returns></returns>
	
		private ClsEODTransactionDetailInfo SetDetailModel(int TransID, int CustomerID, int PolicyID, int PolicyVersionID, string Status, string ErrorDesc, int RowID, int JournalID, int ProcessDiaryListID)
		{
			try
			{
				ClsEODTransactionDetailInfo  objDetailInfo = new ClsEODTransactionDetailInfo(); 
				objDetailInfo.TRANSACTION_ID = TransID;
				objDetailInfo.DATE_OF_LAUNCH = DateTime.Now;
				objDetailInfo.CUSTOMER_ID = CustomerID;
				objDetailInfo.POLICY_ID = PolicyID;
				objDetailInfo.POLICY_VERSION_ID = PolicyVersionID;
				objDetailInfo.STATUS = Status;
				objDetailInfo.ERROR_DESCRIPTION = ErrorDesc;
				objDetailInfo.ROW_ID = RowID;
				objDetailInfo.JOURNAL_ID = JournalID;
				objDetailInfo.PROCESS_DIARY_LIST_ID = ProcessDiaryListID;
				return objDetailInfo;
			}	
			catch(Exception ex)
			{
				throw(ex);
			}
		
		}

		#endregion

		#region Launch EOD Process
		/// <summary>
		/// Launches the EOD Process.
		/// </summary>
		/// <returns></returns>
		public bool LaunchEODProcess()
		{
			try
			{
				ResumeEODProcess();
				SendPendingProcessDiaryReminder();
				SendPolicyRenewalReviewDiaryReminder();
				LaunchNonRenewProcess();
				LaunchCancellationProcess();
				LaunchRenewalProcess(); 
				LaunchRecurringJournalEntries();
				LaunchMovingPrebillToNormal();
				return true;
			}
			catch//(Exception ex)
			{
				return false;
			}
		}

		#endregion

		#region Resume EOD Process
		
		private void ResumeEODProcess()
		{
			
			DataSet dsTemp = GetPendingMasterEntries();
			DataSet dsTempDetail, dsTempJournalEntry;
			DateTime DateOfLaunch = DateTime.MinValue;
			int TransactionID = 0, MasterRowID = 0, result = 0;
			int CustomerID = 0, PolicyID = 0, PolicyVersionID = 0, JournalID = 0, ProcessDiaryListID = 0, DetailRowID = 0;
		
			if (dsTemp.Tables[0].Rows.Count > 0)
			{
				for (int count = 0; count < dsTemp.Tables[0].Rows.Count; count ++)
				{
					
					DateOfLaunch = Convert.ToDateTime(dsTemp.Tables[0].Rows[count]["DATE_OF_LAUNCH"].ToString());
					TransactionID = Convert.ToInt32(dsTemp.Tables[0].Rows[count]["TRANSACTION_ID"].ToString());
					MasterRowID = Convert.ToInt32(dsTemp.Tables[0].Rows[count]["ROW_ID"].ToString());
					dsTempDetail = GetPendingDetailEntries(DateOfLaunch,TransactionID);
					
					
					if (dsTempDetail.Tables[0].Rows.Count > 0)
					{
						
						for (int countDetail = 0; countDetail < dsTempDetail.Tables[0].Rows.Count; countDetail++)
						{
							
							CustomerID = Convert.ToInt32(dsTempDetail.Tables[0].Rows[countDetail]["CUSTOMER_ID"].ToString()); 
							PolicyID = Convert.ToInt32(dsTempDetail.Tables[0].Rows[countDetail]["POLICY_ID"].ToString()); 
							PolicyVersionID = Convert.ToInt32(dsTempDetail.Tables[0].Rows[countDetail]["POLICY_VERSION_ID"].ToString()); 
							JournalID = Convert.ToInt32(dsTempDetail.Tables[0].Rows[countDetail]["JOURNAL_ID"].ToString()); 
							ProcessDiaryListID = Convert.ToInt32(dsTempDetail.Tables[0].Rows[countDetail]["PROCESS_DIARY_LIST_ID"].ToString()); 
							DetailRowID = Convert.ToInt32(dsTempDetail.Tables[0].Rows[countDetail]["ROW_ID"].ToString()); 

							switch (TransactionID)
							{
								case 1:		//Diary reminder
									DiaryReminders(CustomerID,PolicyID,PolicyVersionID,ProcessDiaryListID,DetailRowID);
									break;
								case 2:		//Diary reminder for reviewing policy before expiration
									ReviewDiaryReminder(CustomerID,PolicyID,PolicyVersionID,DetailRowID);
									break;
								case 3:		//Auto triggering of non-renewal process
									SetNonRenewProcess(CustomerID,PolicyID,PolicyVersionID,DetailRowID);
									break;
								case 4:		//Auto triggering of cancellation process
									SetCancellationProcess(CustomerID,PolicyID,PolicyVersionID,DetailRowID);
									break;
								case 5:		//Auto triggering of renewal process
									SetRenewalProcess(CustomerID,PolicyID,PolicyVersionID,DetailRowID);
									break;
								case 6:		//Recurring journal entry
									dsTempJournalEntry = GetRecurJournalEntryDetail(JournalID);
									foreach (DataRow dr in dsTempJournalEntry.Tables[0].Rows)
									{
										SetRecurrJournalEntry(int.Parse(dr["FREQUENCY"].ToString()),DateTime.Parse(dr["END_DATE"].ToString()),
											DateTime.Parse(dr["LAST_VALID_POSTING_DATE"].ToString()),int.Parse(dr["DAY_OF_THE_WK"].ToString()),
											JournalID,DetailRowID);
									}
									break;
								case 7:		//Moving prebill postings ot normal postings
									SetMovingPrebillToNormal(CustomerID,PolicyID,PolicyVersionID,DetailRowID);
									break;
							}
						}

					}

					//Update the Master Pending Entries
					objMasterInfo = SetMasterModel(TransactionID,EOD_COMPLETE,MasterRowID);
					result = UpdateMasterEntry(objMasterInfo);
				}
			}
		}
		
		private DataSet GetPendingMasterEntries()
		{
			try
			{
				base.BeginTransaction();
				objWrapper.ClearParameteres();
				DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetEODPendingMasterEntries");
				objWrapper.ClearParameteres();
				base.CommitTransaction();
				return dsTemp;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}


		private DataSet GetPendingDetailEntries(DateTime DateOfLaunch, int TransactionID)
		{
			try
			{
				base.BeginTransaction();
				objWrapper.ClearParameteres();

				objWrapper.AddParameter("@DATE_OF_LAUNCH",DateOfLaunch);
				objWrapper.AddParameter("@TRANSACTION_ID",TransactionID);

				DataSet dsTemp = objWrapper.ExecuteDataSet("Proc_GetEODPendingDetailEntries");
				objWrapper.ClearParameteres();
				base.CommitTransaction();
				return dsTemp;
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}

		#endregion

	}
}
