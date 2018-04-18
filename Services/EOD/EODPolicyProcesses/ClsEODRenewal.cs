using System;
using EODCommon;
using Cms.BusinessLayer.BlProcess;
using System.Data ;
using Cms.Model.Policy.Process;
using Cms.DataLayer;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Diary;

namespace EODPolicyProcesses
{
	/// <summary>
	/// Summary description for ClsEODRenewal.
	/// </summary>
	public class ClsEODRenewal: ClsEODPolicyProcess
	{
		string strCnnString ;
		Cms.DataLayer.DataWrapper  objDataWrapper;
		public ClsEODRenewal()
		{
			strCnnString = Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr;
			objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
		}

		public void DoCleanUP()
		{
			objDataWrapper.Dispose();
		}

		#region LaunchRenewal method
		/// <summary>
		/// Will Launch Renewal on Policies which are 45 days (in case of umbrella 60 days)  to expire
		/// </summary>
		public void LaunchRenewal()
		{
			DataTable dtPolicies;
			
			EODLogInfo objLog = new EODLogInfo();
			objLog.Activity = (int)EODActivities.PolicyProcess ;
			objLog.SubActivity = (int) EODActivities.RenewalLaunch ;

			try
			{
				objLog.ActivtyDescription = "Fetching Policies To Start Renewal : ";
				objLog.StartDateTime = System.DateTime.Now ;
				dtPolicies = GetPoliciesToStartRenewal(); 
			}
			catch(Exception ex)
			{
				objLog.Status = ActivityStatus.FAILED; 
				objLog.EndDateTime = DateTime.Now;
				objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
				WriteLog(objLog);
				return;
			}
			objLog.ActivtyDescription += dtPolicies.Rows.Count.ToString() + " Records to process";
			objLog.Status= ActivityStatus.SUCCEDDED;
			objLog.EndDateTime = DateTime.Now;
			WriteLog(objLog);
			for(int i = 0 ; i<dtPolicies.Rows.Count; i++)
			{
				try
				{
					DataRow dr = dtPolicies.Rows[i];
					ClsProcessInfo objProcessInfo = new ClsProcessInfo();
					
					string FROM_AS400 = dr["FROM_AS400"].ToString();

					if(FROM_AS400 == "Y")
					{
						try
						{
							TodolistInfo objTodo=new TodolistInfo();
							ClsDiary objDiary = new ClsDiary();
							objTodo.FROMENTITYID  = EOD_USER_ID;
							objTodo.FROMUSERID    = EOD_USER_ID;
							objTodo.SUBJECTLINE = "Corrective user process is pending on Policy from AS400";
							objTodo.NOTE = "Policy is due for renewal. But as policy is from AS400 system and corrective user is pending can not launch renewal";
							objTodo.LISTTYPEID  = 37;
							objTodo.CUSTOMER_ID = Convert.ToInt32(dr["CUSTOMER_ID"]);
							objTodo.POLICY_ID = Convert.ToInt32(dr["POLICY_ID"]);
							objTodo.POLICY_VERSION_ID = Convert.ToInt32(dr["POLICY_VERSION_ID"]);
							objTodo.CREATED_BY  = EOD_USER_ID;
							objTodo.CREATED_DATETIME = DateTime.Now;
							objTodo.LAST_UPDATED_DATETIME =  DateTime.Now;;
							objTodo.RECDATE =  DateTime.Now;
							objTodo.FOLLOWUPDATE =  DateTime.Now.AddDays(1);
							objTodo.LISTOPEN = "Y";
							objTodo.MODULE_ID = (int)ClsDiary.enumModuleMaster.POLICY_PROCESS;  
							objTodo.LOB_ID = Convert.ToInt32(dr["LOB_ID"]);
							objDiary.DiaryEntryfromSetup(objTodo);
						}
						catch(Exception ex)
						{
						}
						continue;
					}

					ClsRenewalProcess objRenewalProcess = new ClsRenewalProcess();

					objProcessInfo.PROCESS_ID = ClsPolicyProcess.POLICY_RENEWAL_PROCESS;
					objProcessInfo.CUSTOMER_ID = Convert.ToInt32(dr["CUSTOMER_ID"]);
					objProcessInfo.POLICY_ID   = Convert.ToInt32(dr["POLICY_ID"]);
					objProcessInfo.POLICY_VERSION_ID = Convert.ToInt32(dr["POLICY_VERSION_ID"]);
					objProcessInfo.LOB_ID = Convert.ToInt32(dr["LOB_ID"]);
					objProcessInfo.CREATED_BY = EOD_USER_ID;
					objProcessInfo.COMPLETED_BY = EOD_USER_ID;
					objProcessInfo.CREATED_DATETIME = System.DateTime.Now;

					// Default printing options.
					objProcessInfo.POLICY_PREVIOUS_STATUS = objRenewalProcess.GetPolicyStatus(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID); //GetPolicyStatus();

					if(objProcessInfo.LOB_ID == (int)Cms.CmsWeb.cmsbase.enumLOB.AUTOP 
						|| objProcessInfo.LOB_ID == (int)Cms.CmsWeb.cmsbase.enumLOB.CYCL ) 
					{
						objProcessInfo.AUTO_ID_CARD = (int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL; 					
						objProcessInfo.NO_COPIES = Cms.BusinessLayer.BlCommon.ClsCommon.PRINT_OPTIONS_AUTO_CYCL_NO_OF_COPIES;
					}

					objProcessInfo.PRINTING_OPTIONS = (int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.NO);
					objProcessInfo.INSURED = (int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL; 
					objProcessInfo.SEND_ALL = (int)(Cms.CmsWeb.cmsbase.enumYESNO_LOOKUP_CODE.YES);

					objProcessInfo.AGENCY_PRINT = (int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL; 
					objProcessInfo.ADD_INT  = (int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL; 

					objLog.ActivtyDescription  = "Launching Renewal process on policy : ";
					objLog.StartDateTime = DateTime.Now;
					objLog.ClientID = objProcessInfo.CUSTOMER_ID;
					objLog.PolicyID = objProcessInfo.POLICY_ID ;
					objLog.PolicyVersionID = objProcessInfo.POLICY_VERSION_ID ;


					
					

					string StatusDescriptor = "" ;
					if(objRenewalProcess.StartProcess(objProcessInfo, out StatusDescriptor))
					{
						//in case of umbrella Send  Umbrella Renewal Letter 
						if (objProcessInfo.LOB_ID==ClsRenewalProcess.POLICY_LOB_UMB)
						{
							if (CheckUmbrellaRenewalNotice(objProcessInfo.CUSTOMER_ID ,objProcessInfo.POLICY_ID 
								,objProcessInfo.POLICY_VERSION_ID )>1)
							{
								objLog.ActivtyDescription += "Sending Unbrella Renewal Letter ";
								int retUmbrellaLetter=objRenewalProcess.SendUmbrellaRenewalLetter(objProcessInfo);
								if (retUmbrellaLetter==-1)
									objLog.ActivtyDescription += "Unbrella Renewal Letter Could not be generated.";
								else	
									objRenewalProcess.AddDiaryEntry(objProcessInfo," Umbrella Renewal Letter","Umbrella Renewal Letter",false);   
							}
						}

						objLog.Status = ActivityStatus.SUCCEDDED;
						objLog.EndDateTime = System.DateTime.Now;
						WriteLog(objLog);
					}
					else
					{
						objLog.Status= ActivityStatus.FAILED ;
						objLog.EndDateTime = DateTime.Now;
						objLog.AdditionalInfo="Can't Launch renewal : " + StatusDescriptor; 
						WriteLog(objLog);
					}
				}
				catch(Exception ex)
				{
					objLog.Status = ActivityStatus.FAILED ;
					objLog.EndDateTime = System.DateTime.Now;
					objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
					WriteLog(objLog);
					continue;
				}
			}
		}
		#endregion
		
		#region LaunchAutoCommit Method
		/// <summary>
		/// Will Commit Pending Reneal of Policies 
		/// </summary>
		public void LaunchAutoCommit()
		{
			DataTable dtPolicies ;
			EODLogInfo objLog = new EODLogInfo();
			objLog.Activity = (int)EODActivities.PolicyProcess ;
			objLog.SubActivity = (int) EODActivities.RenewalCommit  ;

			try
			{
				objLog.ActivtyDescription = "Fetching Policies To Perform Renewal Commit ";
				objLog.StartDateTime = System.DateTime.Now ;
				dtPolicies = GetPoliciesToCommitRenewal();
			}
			catch(Exception ex)
			{
				objLog.Status = ActivityStatus.FAILED; 
				objLog.EndDateTime = DateTime.Now;
				objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
				WriteLog(objLog);
				return;
			}
			
			objLog.ActivtyDescription += dtPolicies.Rows.Count.ToString() + " Records to process";
			objLog.Status= ActivityStatus.SUCCEDDED;
			objLog.EndDateTime = DateTime.Now;
			WriteLog(objLog);

			int CustomerID = 0, PolicyID = 0,PolicyVersionID = 0;
			ClsPolicyProcess objProcess = new ClsPolicyProcess();
			for(int i = 0 ; i<dtPolicies.Rows.Count; i++)
			{
				try
				{
					DataRow dr = dtPolicies.Rows[i];
					CustomerID = Convert.ToInt32(dr["CUSTOMER_ID"]);
					PolicyID   = Convert.ToInt32(dr["POLICY_ID"]);
					PolicyVersionID = Convert.ToInt32(dr["POLICY_VERSION_ID"]);
					
					objLog.ActivtyDescription  = "Initializing Process Info : ";
					objLog.StartDateTime = DateTime.Now;
					objLog.ClientID = CustomerID;
					objLog.PolicyID = PolicyID;
					
					ClsProcessInfo objProcessInfo = objProcess.GetRunningProcess(CustomerID,PolicyID); //,PolicyVersionID );
					objProcessInfo.LOB_ID = Convert.ToInt32(dr["LOB_ID"]);

					objLog.ActivtyDescription += " Committing Renewal : ";
					objLog.PolicyVersionID = objProcessInfo.POLICY_VERSION_ID ;
					
					ClsRenewalProcess objRenewalProcess = new ClsRenewalProcess();
					objProcessInfo.COMPLETED_DATETIME = DateTime.Now;
					objProcessInfo.EFFECTIVE_DATETIME = DateTime.Now;
					objProcessInfo.COMPLETED_BY = EOD_USER_ID;
					string StatusDescriptor = "";	
					if(objRenewalProcess.CommitProcess(objProcessInfo,"" ,out StatusDescriptor ))
					{
						objLog.Status= ActivityStatus.SUCCEDDED;
						objLog.EndDateTime = DateTime.Now;
						WriteLog(objLog);
					}
					else
					{
						objLog.Status= ActivityStatus.FAILED ;
						objLog.EndDateTime = DateTime.Now;
						objLog.AdditionalInfo="Can't Commit renewal : "  + StatusDescriptor;
						WriteLog(objLog);
					}
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

		#endregion

		#region SendRenewalPendingFollowUp Method
		/// <summary>
		/// Will Send Follow-up for Pending Reneal Policies 
		/// </summary>
		public void SendRenewalPendingFollowUp()
		{
			Cms.DataLayer.DataWrapper objWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES, DataWrapper.SetAutoCommit.OFF);
			
			DataTable dtPolicies ;
			EODLogInfo objLog = new EODLogInfo();
			objLog.Activity = (int)EODActivities.PolicyProcess ;
			objLog.SubActivity = (int) EODActivities.RenewalFollowUp   ;

			try
			{
				objLog.ActivtyDescription = "Fetching Policies To Send Renewal Followup";
				objLog.StartDateTime = System.DateTime.Now ;
				dtPolicies = GetPoliciesToSendFollowUp();
			}
			catch(Exception ex)
			{
				objLog.Status = ActivityStatus.FAILED; 
				objLog.EndDateTime = DateTime.Now;
				objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
				WriteLog(objLog);
				objWrapper.Dispose();
				return;
			}
						
			objLog.ActivtyDescription += dtPolicies.Rows.Count.ToString() + " Records to process";
			objLog.Status= ActivityStatus.SUCCEDDED;
			objLog.EndDateTime = DateTime.Now;
			WriteLog(objLog);
			
			for(int i = 0 ; i<dtPolicies.Rows.Count; i++)
			{
				try
				{
					DataRow dr = dtPolicies.Rows[i];
					ClsProcessInfo objProcessInfo = new ClsProcessInfo();
					int DaysToExpire	= Convert.ToInt32(dr["DAYS_TOEXPIRE"]);
					
					objProcessInfo.PROCESS_ID = ClsPolicyProcess.POLICY_RENEWAL_PROCESS;
					objProcessInfo.CUSTOMER_ID = Convert.ToInt32(dr["CUSTOMER_ID"]);
					objProcessInfo.POLICY_ID   = Convert.ToInt32(dr["POLICY_ID"]);
					objProcessInfo.POLICY_VERSION_ID = Convert.ToInt32(dr["POLICY_VERSION_ID"]);
					objProcessInfo.LOB_ID = Convert.ToInt32(dr["LOB_ID"]);
					objProcessInfo.CREATED_BY = EOD_USER_ID;
					objProcessInfo.COMPLETED_BY = EOD_USER_ID;
					objProcessInfo.CREATED_DATETIME = System.DateTime.Now;
					
					
					ClsRenewalProcess objRenewalProcess = new ClsRenewalProcess();
					
					if(DaysToExpire ==35) //only to under writer
					{
						objLog.ActivtyDescription  = "Sending Renewal Follow-up for policy : Days To Expire " + DaysToExpire;
						objLog.StartDateTime = DateTime.Now;
						objLog.ClientID = objProcessInfo.CUSTOMER_ID;
						objLog.PolicyID = objProcessInfo.POLICY_ID ;
						objLog.PolicyVersionID = objProcessInfo.POLICY_VERSION_ID ;

						objWrapper.ClearParameteres();
						objRenewalProcess.AddDiaryEntry(objProcessInfo,"Policy renewal due 35 Day Notice.","This Policy is going to expiry in next 35 days.EOD could not perform commit on it.",false,objWrapper,Cms.BusinessLayer.BlCommon.ClsDiary.enumDiaryType.PENDING_RENEWAL_FOLLOWUP.ToString() );   
						
						objLog.Status = ActivityStatus.SUCCEDDED;
						objLog.EndDateTime = System.DateTime.Now;
						WriteLog(objLog);
					}
					else  if(DaysToExpire ==10)//to both Under writer and to Manager
					{
						objLog.ActivtyDescription  = "Sending Renewal Follow-up for policy : Days To Expire " + DaysToExpire;
						objLog.StartDateTime = DateTime.Now;
						objLog.ClientID = objProcessInfo.CUSTOMER_ID;
						objLog.PolicyID = objProcessInfo.POLICY_ID ;
						objLog.PolicyVersionID = objProcessInfo.POLICY_VERSION_ID ;

						objWrapper.ClearParameteres();
						objRenewalProcess.AddDiaryEntry(objProcessInfo,"Policy renewal due 10 Day Notice","This Policy is going to exore in next 10 days.EOD could not perform commit on it.",false,objWrapper,Cms.BusinessLayer.BlCommon.ClsDiary.enumDiaryType.PENDING_RENEWAL_FOLLOWUP.ToString());   

						objLog.Status = ActivityStatus.SUCCEDDED;
						objLog.EndDateTime = System.DateTime.Now;
						WriteLog(objLog);
					}
					
				}
				catch(Exception ex)
				{
					objLog.Status = ActivityStatus.FAILED;
					objLog.EndDateTime = System.DateTime.Now;
					objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
					WriteLog(objLog);
					continue;
				}
			}
			objWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

		}
		#endregion

		#region Private Utility Functions 
		private int CheckUmbrellaRenewalNotice(int CustomerID,int PolicyID,int PolicyVersionID)
		{
			string Proc_CheckUmbrellaRenewalNotice	=	"Proc_CheckUmbrellaRenewalNotice";
			int RetVal = 0;
			objDataWrapper.ClearParameteres();
			objDataWrapper.AddParameter("@CUSTOMER_ID",CustomerID);
			objDataWrapper.AddParameter("@POLICY_ID",PolicyID);
			objDataWrapper.AddParameter("@POLICY_VERSION_ID",PolicyVersionID);
			try
			{
				RetVal = objDataWrapper.ExecuteNonQuery(Proc_CheckUmbrellaRenewalNotice); 
		   	}
			catch(Exception ex)
			{
				objDataWrapper.ClearParameteres();
				throw(ex);
			}
			objDataWrapper.ClearParameteres();
			return RetVal ;
		}

		private DataTable GetPoliciesToStartRenewal()
		{
			objDataWrapper.ClearParameteres();
			DataSet ds = objDataWrapper.ExecuteDataSet("Proc_GetPoliciesToStartRenewal");
			return ds.Tables[0];
		}

		private DataTable GetPoliciesToCommitRenewal()
		{
			objDataWrapper.ClearParameteres();
			DataSet ds = objDataWrapper.ExecuteDataSet("Proc_GetPoliciesToComitRenewal");
			return ds.Tables[0];
		}
		private DataTable GetPoliciesToSendFollowUp()
		{
			objDataWrapper.ClearParameteres();
			DataSet ds = objDataWrapper.ExecuteDataSet("Proc_GetPoliciesWithPendingRenewal");
			return ds.Tables[0];
		}
		#endregion
	}
}
