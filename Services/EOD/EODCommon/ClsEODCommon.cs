using System;
using Cms.BusinessLayer.BlCommon;
using System.Xml;
using System.IO;
using Cms.DataLayer;
using System.Data;
using System.Text;

namespace EODCommon
{
	public struct ActivityScheduleInfo
	{
		public string ActivityCode;
		public string ScheduleType;
		public string Day;
		public string Date;
		public string Hour;
		public string Minutes;
		public DateTime LastRunDate;
		public string LastRunHour;
		public string LastRunMinutes;
	}
	
	public sealed class ScheduleType
	{
		public static readonly string RunDaily = "Daily";
		public static readonly string RunMonthly = "Monthly";
		public static readonly string RunBiMonthly = "Monthly";
		public static readonly string RunWeekly= "Weekly";
	}


	public enum EODActivities
	{
		ServiceManager = 10,
		SpoolThread	 = 11,
		ActivityThread	 = 12,
		Accounting	=	100	,
		EFT	=	101	,
		RecurringJournalEntries	=	102	,
		PreBillToNormal	=	103	,
		PremiumNoticeGeneration	=	104	,
		CreditCard	=	105	,
		AgencyStatementRegular	= 106 , 
		AgencyStatementAdditional = 107 , 
		SmallBalanceWriteOff	  = 108 , 
		EarnedPremiumProcessing  = 109 , 
		PositivePayFileGeneraiion  = 110 , 
		PolicyProcess	=	200	,
		CancellationLaunch	=	201	,
		CancellationRollBack	=	202	,
		CancellationCommit	=	203	,
		FollowUpCreation	= 204,
		RenewalLaunch  = 205,
		RenewalCommit = 206,
		RenewalFollowUp = 207,
		Spooling	=	300	,
		EFTSpooling	=	301	,
		CreditCardSpooling	=	302	,
		MonthEndReports		= 401,
        ClaimsProcessing = 500,
        AutoClaimsClose		= 501,
        ClaimReserveUpdate		= 502
	}

	public class EODLogInfo
	{
		private string mActivtyDescription ;
		private string mStatus;
		private int mActivity;
		private int mSubActivity;
		private string mAdditionalInfo;
		private int mClientID;
		private int mPolicyID;
		private int mPolicyVersionID;
		private DateTime mStartDateTime;
		private DateTime mEndDateTime;

		#region Public Properties
		public string ActivtyDescription 
		{
			get
			{
				return mActivtyDescription ;
			}
			set
			{
				mActivtyDescription = value;
			}
		}

		public string Status
		{
			get
			{
				return mStatus;
			}
			set
			{
				mStatus = value;
			}
		}

		public int Activity
		{
			get 
			{
				return mActivity;
			}
			set
			{
				mActivity = value;
			}
		}
		public int SubActivity
		{
			get
			{
				return mSubActivity;
			}
			set 
			{
				mSubActivity = value;
			}
		}

		public string AdditionalInfo
		{
			get
			{
				return mAdditionalInfo;
			}
			set
			{
				mAdditionalInfo = value;
			}
		}

		public int ClientID
		{
			get 
			{
				return mClientID;
			}
			set
			{
				mClientID = value;
			}
		}

		public int PolicyID
		{
			get
			{
				return mPolicyID;
			}
			set
			{
				mPolicyID = value;
			}
		}
		public int PolicyVersionID
		{
			get
			{
				return mPolicyVersionID;
			}
			set
			{
				mPolicyVersionID = value;
			}
		}
		public DateTime StartDateTime
		{
			get 
			{
				return mStartDateTime;
			}
			set
			{
				mStartDateTime = value;
			}
		}
		public DateTime EndDateTime
		{
			get 
			{
				return mEndDateTime;
			}
			set 
			{
				mEndDateTime = value;
			}
		}

		#endregion

		public void Reset()
		{
			mActivtyDescription = "";
			mStatus ="";
			mAdditionalInfo="";
			mClientID=0;
			mPolicyID= 0;
			mPolicyVersionID= 0 ;
			mStartDateTime = DateTime.MinValue ;
			mEndDateTime = DateTime.MinValue ;
		}
		public void ResetAcivityInfo()
		{
			mActivity = 0;
			mSubActivity = 0;
		}

	}
	public sealed class ActivityStatus
	{
		public static readonly string FAILED = "FAILED ";
		public static readonly string SUCCEDDED = "SUCCEEDED" ; //"SUCCEDDED";
	}
	public enum EODStatus
	{
		NotStarted = 0,
		InProgress = 1,
		Completed = 2,
		Paused = 3,
		ProcessingJE =10,
		ProcessingPrebill = 11,
		ProcessingCustRecon = 12
		
	}

	public enum ProcessCommands
	{
		StartEODProcess =129,
		StopEODProcess  =130,
		PauseEODProcess =131,
		RunEFT			=201,
		RunAccounting	=202,
		RunPolProcess	=220
	}
	/// <summary>
	/// Summary description for ClsEODCommon.
	/// </summary>
	public class ClsEODCommon
	{
		public static int EOD_USER_ID;
		public static string CarrierSystemID;
		public static string PayPalHostName;
		public static string PaypalPartner;
		public static string PayPalUserName;
		public static string PayPalPassword;
		public static string PayPalVendor;
		public static bool AccountingLocked;

		public ClsEODCommon()
		{
		}

		static ClsEODCommon()
		{
			ClsCommon.IsEODProcess = true;
			AccountingLocked = false; 
			string strCnnStr = "This will have Connection String";
			try
			{
				string XMLPath;
				XMLPath = AppDomain.CurrentDomain.BaseDirectory + "EODParameters.XML";
				XmlDocument configXML = new XmlDocument();
				configXML.Load(XMLPath);
				XmlNode EodParameters = configXML.SelectSingleNode("Root/EODParameters");
				
				ClsCommon.WebAppUNCPath = EodParameters.SelectSingleNode("WebAppUNCPath").InnerText.Trim();
				strCnnStr=EodParameters.SelectSingleNode("connStr").InnerText.Trim();
				ClsCommon.ConnStr = strCnnStr;

				Cms.ExceptionPublisher.ExceptionManagement.DefaultPublisher.CnnString = strCnnStr;

				EOD_USER_ID = Convert.ToInt32(EodParameters.SelectSingleNode("BricsUser").InnerText.Trim());
				CarrierSystemID = EodParameters.SelectSingleNode("CarrierSystemID").InnerText.Trim();
				ClsCommon.EODUserID = EOD_USER_ID;
				ClsCommon.CmsWebUrl = EodParameters.SelectSingleNode("CmsWebUrl").InnerText.Trim();
				ClsCommon.UploadPath =EodParameters.SelectSingleNode("UploadPath").InnerText.Trim();
				ClsCommon.ImpersonationUserId =EodParameters.SelectSingleNode("ImpersonationUserId").InnerText.Trim();
				ClsCommon.ImpersonationPassword = EodParameters.SelectSingleNode("ImpersonationPassword").InnerText.Trim();
				ClsCommon.ImpersonationDomain = EodParameters.SelectSingleNode("ImpersonationDomain").InnerText.Trim();
				ClsCommon.EODSystemID =  EodParameters.SelectSingleNode("EODSystemID").InnerText.Trim();
				ClsCommon.UploadURL =  EodParameters.SelectSingleNode("UploadURL").InnerText.Trim();
				ClsCommon.IIXUrl = EodParameters.SelectSingleNode("IIXUrl").InnerText.Trim();
				ClsCommon.IIXUserName = EodParameters.SelectSingleNode("IIXUserName").InnerText.Trim();
				ClsCommon.IIXPassword = EodParameters.SelectSingleNode("IIXPassword").InnerText.Trim();
				ClsCommon.IIXAccountNumber = EodParameters.SelectSingleNode("IIXAccountNumber").InnerText.Trim();
				ClsCommon.VehicleClassXml =  EodParameters.SelectSingleNode("VehicleClassXml").InnerText.Trim();
				ClsCommon.ServiceAuthenticationToken = EodParameters.SelectSingleNode("AuthenticationToken").InnerText.Trim();

				ClsCommon.ConfigFileName= EodParameters.SelectSingleNode("BRICSConfigFile").InnerText.Trim();
                ClsCommon.BL_LANG_ID = 1;
                ClsCommon.BL_LANG_CULTURE = "en-US";
                ClsCommon.SetCustomizedXml(ClsCommon.BL_LANG_CULTURE);

				PayPalHostName =  EodParameters.SelectSingleNode("PayPalHostName").InnerText.Trim();
				PaypalPartner  =  EodParameters.SelectSingleNode("PaypalPartner").InnerText.Trim();
				PayPalUserName =  EodParameters.SelectSingleNode("PayPalUserName").InnerText.Trim();
				PayPalPassword =  EodParameters.SelectSingleNode("PayPalPassword").InnerText.Trim();
				PayPalVendor   =  EodParameters.SelectSingleNode("PayPalVendor").InnerText.Trim();

				string FetchInsuranceScore = EodParameters.SelectSingleNode("FetchInsuranceScore").InnerText.Trim().ToUpper();

				if(FetchInsuranceScore == "YES")
				{
					ClsCommon.IsScoreToFetched = true;
				}
				else
				{
					ClsCommon.IsScoreToFetched = false;
				}

			}
			catch(Exception  ex)
			{
				string msg  =ex.Message ;
				TextWriter tw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory+"\\CriticalError.log",true);
				tw.WriteLine("Critical Error , while initialising common params  Date: "+DateTime.Now );
				tw.WriteLine( " Error:"+ ex.Message + "   Stack: "+ex.StackTrace );
				if(ex.InnerException != null && ex.InnerException.Message != null)
				{
					tw.WriteLine("Inner Exception : " + ex.InnerException.Message + " Inner Exception Trace : "  + ex.InnerException.StackTrace);
				}
				tw.WriteLine("-----------------\n\r  Connection String from Param XML");
				tw.WriteLine(strCnnStr);
				tw.WriteLine("-----------------\n\r");
				tw.WriteLine("*****************************************************************");
				tw.Close();	
			}
		}
		public static string GetAdditionalInfo(Exception exception)
		{
			Exception objEx = exception;
			
			StringBuilder strInfo=  new StringBuilder();
			strInfo.Append(" Exception Information : ");
			while(true)
			{
				if(objEx == null)
					break;
				strInfo.Append(objEx.Message);				
				objEx = objEx.InnerException;
				if(objEx == null)
					break;

				strInfo.Append(" :: Inner Exception Information : ");
			}

			if ( exception != null )
				strInfo.AppendFormat(Environment.NewLine +  " :: Stack Trace :  " + exception.StackTrace);
			else
				strInfo.AppendFormat("{0}{0}No Exception.{0}", Environment.NewLine);

			return strInfo.ToString();
		}
		public static void WriteLog(EODLogInfo objInfo)
		{
			try
			{
				string strCnnString ="";
				strCnnString = Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr;
				Cms.DataLayer.DataWrapper  objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO , DataWrapper.SetAutoCommit.ON );
				try
				{
					objDataWrapper.AddParameter("@ACTIVITY_DESCRIPTION", objInfo.ActivtyDescription ); 
			
					if(objInfo.Status != "")
						objDataWrapper.AddParameter("@STATUS", objInfo.Status   ); 
					else
						objDataWrapper.AddParameter("@STATUS", DBNull.Value   ); 

					if(objInfo.Activity != 0)
						objDataWrapper.AddParameter("@ACTIVITY", objInfo.Activity  ); 
					else
						objDataWrapper.AddParameter("@ACTIVITY", DBNull.Value ); 

					if(objInfo.SubActivity != 0)
						objDataWrapper.AddParameter("@SUB_ACTIVITY", objInfo.SubActivity ); 
					else
						objDataWrapper.AddParameter("@SUB_ACTIVITY", DBNull.Value   ); 

					if(objInfo.ClientID != 0)
						objDataWrapper.AddParameter("@CLIENT_ID", objInfo.ClientID ); 
					else
						objDataWrapper.AddParameter("@CLIENT_ID", DBNull.Value   ); 

					if(objInfo.PolicyID != 0)
						objDataWrapper.AddParameter("@POLICY_ID", objInfo.PolicyID ); 
					else
						objDataWrapper.AddParameter("@POLICY_ID", DBNull.Value   ); 

					if(objInfo.PolicyVersionID != 0)
						objDataWrapper.AddParameter("@POLICY_VERSOIN_ID", objInfo.PolicyVersionID ); 
					else
						objDataWrapper.AddParameter("@POLICY_VERSOIN_ID", DBNull.Value   ); 

					if(objInfo.AdditionalInfo != "")
						objDataWrapper.AddParameter("@ADDITIONAL_INFO", objInfo.AdditionalInfo ); 
					else
						objDataWrapper.AddParameter("@ADDITIONAL_INFO", DBNull.Value   ); 

					if(objInfo.StartDateTime != DateTime.MinValue )
						objDataWrapper.AddParameter("@START_DATETIME", objInfo.StartDateTime  ); 
					else
						objDataWrapper.AddParameter("@START_DATETIME", DBNull.Value   ); 

					if(objInfo.EndDateTime != DateTime.MinValue )
						objDataWrapper.AddParameter("@END_DATETIME", objInfo.EndDateTime  ); 
					else
						objDataWrapper.AddParameter("@END_DATETIME", DBNull.Value   ); 
 
					objDataWrapper.ExecuteNonQuery("Proc_AddEODProcessLog");
				}
				catch(Exception ex)
				{
					string msg  =ex.Message ;
					TextWriter tw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory+"\\CriticalError.log",true);
					tw.WriteLine("Critical Error , while writing Process Log  Date: "+DateTime.Now );
					tw.WriteLine( " Error:"+ ex.Message + "   Stack: "+ex.StackTrace );
					if(ex.InnerException != null && ex.InnerException.Message != null)
					{
						tw.WriteLine("Inner Exception : " + ex.InnerException.Message + " Inner Exception Trace : "  + ex.InnerException.StackTrace);
					}
				
					tw.WriteLine("Information to be logged : ");
					tw.WriteLine("Activity Description :: " + objInfo.ActivtyDescription );
					tw.WriteLine("Activity ID :: " + objInfo.Activity.ToString() + " -- " + objInfo.SubActivity.ToString()  );
					tw.WriteLine("Activity Status :: " + objInfo.Status );
					tw.WriteLine("Additional Info :: " + objInfo.AdditionalInfo  );
					tw.WriteLine("Customer Data :: " + objInfo.ClientID.ToString()  + " -  " + objInfo.PolicyID.ToString() + " - " + objInfo.PolicyVersionID.ToString()  );
					tw.WriteLine("Activity Start Time :: " + objInfo.StartDateTime.ToString()  );
					tw.WriteLine("Activity End Time :: " + objInfo.EndDateTime.ToString()  );
					tw.WriteLine("*****************************************************************");
					tw.Close();	
				}
				finally
				{
					objInfo.Reset();
					objDataWrapper.Dispose();
				}
			}
			catch(Exception ex)
			{
				string msg  =ex.Message ;
				TextWriter tw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory+"\\CriticalError.log",true);
				tw.WriteLine("Critical Error , while writing Process Log  Date: "+DateTime.Now );
				tw.WriteLine( " Error:"+ ex.Message + "   Stack: "+ex.StackTrace );
				if(ex.InnerException != null && ex.InnerException.Message != null)
				{
					tw.WriteLine("Inner Exception : " + ex.InnerException.Message + " Inner Exception Trace : "  + ex.InnerException.StackTrace);
				}
				
				tw.WriteLine("Information to be logged : ");
				tw.WriteLine("Activity Description :: " + objInfo.ActivtyDescription );
				tw.WriteLine("Activity ID :: " + objInfo.Activity.ToString() + " -- " + objInfo.SubActivity.ToString()  );
				tw.WriteLine("Activity Status :: " + objInfo.Status );
				tw.WriteLine("Additional Info :: " + objInfo.AdditionalInfo  );
				tw.WriteLine("Customer Data :: " + objInfo.ClientID.ToString()  + " -  " + objInfo.PolicyID.ToString() + " - " + objInfo.PolicyVersionID.ToString()  );
				tw.WriteLine("Activity Start Time :: " + objInfo.StartDateTime.ToString()  );
				tw.WriteLine("Activity End Time :: " + objInfo.EndDateTime.ToString()  );
				tw.WriteLine("*****************************************************************");
				tw.Close();	
			}
			
			
		}

		public void SaveReportSnapshot()
		{

			EODLogInfo objLog = new EODLogInfo();
			objLog.Activity = (int)EODActivities.MonthEndReports ;
			objLog.SubActivity = (int) EODActivities.MonthEndReports ;
			DataWrapper objDataWrapper;
			string strCnnString = Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr;
			
			objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES , DataWrapper.SetAutoCommit.OFF);
			try
			{
				objLog.StartDateTime = DateTime.Now;
				objLog.ActivtyDescription = "Saving Snapshot of Daily Loss Summary Report";

				objDataWrapper.AddParameter("@AS_ON_DATE" , DateTime.Now);
				objDataWrapper.ExecuteNonQuery("Proc_Save_Daily_Loss_summary_Report");
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				objLog.Status = ActivityStatus.SUCCEDDED ; 
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
			
		}

		public void ProcessMonthEndReports()
		{

			EODLogInfo objLog = new EODLogInfo();
			objLog.Activity = (int)EODActivities.MonthEndReports ;
			objLog.SubActivity = (int) EODActivities.MonthEndReports ;
			DataWrapper objDataWrapper;
			string strCnnString = Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr;
			
			objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES , DataWrapper.SetAutoCommit.OFF);
			try
			{
				objLog.StartDateTime = DateTime.Now;
				objLog.ActivtyDescription = "Processing Agency Production data";

				objDataWrapper.AddParameter("@MONTH" , DateTime.Now.Month);
				objDataWrapper.AddParameter("@YEAR" , DateTime.Now.Year );
				objDataWrapper.ExecuteNonQuery("Proc_SaveAgencyProductionData");
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				objLog.Status = ActivityStatus.SUCCEDDED ; 
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
			
			objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES , DataWrapper.SetAutoCommit.OFF);
			try
			{
				objLog.StartDateTime = DateTime.Now;
				objLog.ActivtyDescription = "Saving Agency Production Report Snapshot.";

				objDataWrapper.AddParameter("@MONTH" , DateTime.Now.Month);
				objDataWrapper.AddParameter("@YEAR" , DateTime.Now.Year );
				objDataWrapper.ExecuteNonQuery("Proc_SaveAgencyPRoductionSnapShot");
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				objLog.Status = ActivityStatus.SUCCEDDED ; 
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
			
			objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES , DataWrapper.SetAutoCommit.OFF);
			try
			{
				objLog.StartDateTime = DateTime.Now;
				objLog.ActivtyDescription = "Processing Advance Payment Report";

				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@MONTH" , DateTime.Now.Month);
				objDataWrapper.AddParameter("@YEAR" , DateTime.Now.Year );
				objDataWrapper.ExecuteNonQuery("Proc_SaveAdvancePaymentReport");
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				objLog.Status = ActivityStatus.SUCCEDDED ; 
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
			objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES , DataWrapper.SetAutoCommit.OFF);
			try
			{
				objLog.StartDateTime = DateTime.Now;
				objLog.ActivtyDescription = "Processing Suspense Payment Report";

				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@DAY" , DateTime.Now.Day);
				objDataWrapper.AddParameter("@MONTH" , DateTime.Now.Month);
				objDataWrapper.AddParameter("@YEAR" , DateTime.Now.Year );
				objDataWrapper.ExecuteNonQuery("Proc_SaveSuspensePaymentReport");
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				objLog.Status = ActivityStatus.SUCCEDDED ; 
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
			objDataWrapper = new DataWrapper(strCnnString, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.YES , DataWrapper.SetAutoCommit.OFF);
			try
			{
				objLog.StartDateTime = DateTime.Now;
				objLog.ActivtyDescription = "Processing Reinsurance Premium Report";

				objDataWrapper.ClearParameteres();
				objDataWrapper.AddParameter("@MONTH" , DateTime.Now.Month);
				objDataWrapper.AddParameter("@YEAR" , DateTime.Now.Year );
				objDataWrapper.ExecuteNonQuery("Proc_SAVE_REINSURANCE_BREAKDOWN_DETAILS");
				objDataWrapper.ExecuteNonQuery("Proc_SAVE_REINSURANCE_BREAKDOWN_DETAILS_WAT");
				objDataWrapper.ExecuteNonQuery("Proc_SAVE_REINSURANCE_BREAKDOWN_DETAILS_AUTO");
				objDataWrapper.CommitTransaction(DataWrapper.CloseConnection.YES);

				objLog.Status = ActivityStatus.SUCCEDDED ; 
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

			objDataWrapper.Dispose();
		}
	}
}
