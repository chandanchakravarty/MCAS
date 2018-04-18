using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Xml;
using System.Threading;
using EODAccounts;
using EODCommon;
using EODPolicyProcesses;
using EODClaims;
using System.IO;


namespace EbixEODServiceManager
{
    public partial class ServiceManager : ServiceBase
    {

     
        #region Service Related ethods

        public ServiceManager()
        {
            // This call is required by the Windows.Forms Component Designer.
            InitializeComponent();

            LastAccountRun = DateTime.MinValue;
            LastAccSpoolRun = DateTime.MinValue; ;
            LastEFTRun = DateTime.MinValue;
            LastFollowUpRun = DateTime.MinValue;
            LastProcessRun = DateTime.MinValue;
            LastPremNoticeRun = DateTime.MinValue;
            LastClaimRun = DateTime.MinValue;
            LastClaimReserveUpdate = DateTime.MinValue;
            SpoolCounter = 0;
        }
        // The main entry point for the process
        static void Main()
        {
            System.ServiceProcess.ServiceBase[] ServicesToRun;

            // More than one user Service may run within the same process. To add
            // another service to this process, change the following line to
            // create a second service object. For example,
            //
            //   ServicesToRun = new System.ServiceProcess.ServiceBase[] {new Service1(), new MySecondUserService()};
            //

            ServicesToRun = new System.ServiceProcess.ServiceBase[] { new ServiceManager() };

            System.ServiceProcess.ServiceBase.Run(ServicesToRun);
        }
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        protected override void OnStart(string[] args)
        {
            DoConfigInitialisation();
        }

        protected override void OnStop()
        {
        }
        protected override void OnCustomCommand(int command)
        {
            switch (command)
            {
                /*case (int)ProcessCommands.RunEFT:
                    RunEFTNow = true;
                    break;
                case (int)ProcessCommands.RunAccounting:
                    RunACCNow = true;
                    break;
                 */
                case (int)ProcessCommands.RunPolProcess:
                    RunPOLProcessNow = true;
                    break;
            }
        }

        #endregion
        #region Timer Events

        private void timServiceInterval_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DoScheduledActivities();
        }

        private void timSpoolInterval_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //DoScheduledSpooling(); commented on 3 jan 2011
        }

        #endregion

        #region Initialising Functions
        private void DoConfigInitialisation()
        {
            try
            {
                EPR_InProcress = false;
                REPORT_InProgress = false;
                WRITEOFF_InProgress = false;
                ADDCOMM_InProgress = false;
                REGCOMM_InProgress = false;
                Claim_InProcess = false;

                DEPOSITS_InProcess = false;
                RECON_InProcess = false;
                PNGenerationInProgress = false;

                string strFileName = AppDomain.CurrentDomain.BaseDirectory + "EODParameters.XML";
                xmlDoc = new XmlDocument();
                xmlDoc.Load(strFileName);
                XmlNode EODSchedule = xmlDoc.SelectSingleNode("Root/EODSchedule");
                XmlNodeList ActivityNodes = EODSchedule.SelectNodes("Activity");

                WriteLogFile("Initialising service parameters");
                foreach (XmlNode ActivityNode in ActivityNodes)
                {
                    #region Commented
                    /*
					string strActivityDes = ActivityNode.Attributes["DESCRIPTION"].Value;
					ActivityScheduleInfo objSchInfo = new ActivityScheduleInfo();
					objSchInfo.ActivityCode = strActivity;
					XmlNode schNode = ActivityNode.SelectNodes("Schedule");
					objSchInfo.ScheduleType = schNode.Attributes["RUN"].Value.Trim();
					XmlNode timeNode = schNode.SelectSingleNode("Time");
					objSchInfo.Hour = timeNode.SelectSingleNode("Hour").InnerText.Trim();
					objSchInfo.Minutes = timeNode.SelectSingleNode("Minutes").InnerText.Trim();
					objSchInfo.Date = schNode.SelectSingleNode("Date").InnerText.Trim();
					objSchInfo.Day = schNode.SelectSingleNode("Day").InnerText.Trim();

					objSchInfo.LastRunHour = ActivityNode.SelectSingleNode("LastRun/Time/Hour").InnerText.Trim();
					objSchInfo.LastRunMinutes = ActivityNode.SelectSingleNode("LastRun/Time/Minutes").InnerText.Trim();
					objSchInfo.LastRunDate = ActivityNode.SelectSingleNode("LastRun/Date").InnerText.Trim();

					arrSchedule.Add(objSchInfo);

					WriteLogFile(strActivityDes + "  initialised.");*/
                    #endregion

                    string strActivity = ActivityNode.Attributes["NAME"].Value;
                    switch (strActivity)
                    {
                        case "EFT":
                            this.EFTScheduleHR = Convert.ToInt32(ActivityNode.Attributes["TIME_HR"].Value);
                            this.EFTScheduleMIN = Convert.ToInt32(ActivityNode.Attributes["TIME_MIN"].Value);
                            WriteLogFile("EFTSchedule initialised");
                            break;
                        case "ACCOUNTING":
                            this.AccountEODScheduleHR = Convert.ToInt32(ActivityNode.Attributes["TIME_HR"].Value);
                            this.AccountEODScheduleMIN = Convert.ToInt32(ActivityNode.Attributes["TIME_MIN"].Value);
                            WriteLogFile("Accounting Schedule initialised");
                            break;

                        //case "PREM_NOTICES":
                        //    this.PremNoticeScheduleHR = Convert.ToInt32(ActivityNode.Attributes["TIME_HR"].Value);
                        //    this.PremNoticeScheduleMIN = Convert.ToInt32(ActivityNode.Attributes["TIME_MIN"].Value);
                        //    break;

                        case "POL_PROCESS":
                            this.ProcessEODScheduleHR = Convert.ToInt32(ActivityNode.Attributes["TIME_HR"].Value);
                            this.ProcessEODScheduleMIN = Convert.ToInt32(ActivityNode.Attributes["TIME_MIN"].Value);
                            WriteLogFile("Process Schedule initialised");
                            break;
                        case "CLAIMS":
                            this.ClaimsEODScheduleHR = Convert.ToInt32(ActivityNode.Attributes["TIME_HR"].Value);
                            this.ClaimsEODScheduleMIN = Convert.ToInt32(ActivityNode.Attributes["TIME_MIN"].Value);
                            
                            this.ClaimSchType = ActivityNode.Attributes["RUN"].Value.Trim();
                            if (ActivityNode.Attributes["LAST_RUN"].Value.Trim() != "")
                            {
                                LastClaimRun = Convert.ToDateTime(ActivityNode.Attributes["LAST_RUN"].Value.Trim());
                            }
                            else
                            {
                                LastClaimRun = DateTime.MinValue;
                            }
                            if (ActivityNode.Attributes["LAST_RESERVE_UPDATE"].Value.Trim() != "")
                            {
                                LastClaimReserveUpdate = Convert.ToDateTime(ActivityNode.Attributes["LAST_RESERVE_UPDATE"].Value.Trim());
                            }
                            else
                            {
                                LastClaimReserveUpdate = DateTime.MinValue;
                            }
                            
                            NextClaimRun = GetLastDayOfMonth(DateTime.Now);
                            NextClaimReserveUpdate = GetLastDayOfMonth(DateTime.Now);
                            WriteLogFile("Claims Schedule initialised");
                            break;
                        //case "ACC_SPOOLING":
                        //    this.AccSpoolScheduleHR = Convert.ToInt32(ActivityNode.Attributes["TIME_HR"].Value);
                        //    this.AccSpoolScheduleMIN = Convert.ToInt32(ActivityNode.Attributes["TIME_MIN"].Value);
                        //    WriteLogFile("Account Spooling Schedule initialised");
                        //    break;
                        case "FOLLOWUP":
                            this.FollowupScheduleHR = Convert.ToInt32(ActivityNode.Attributes["TIME_HR"].Value);
                            this.FollowupScheduleMIN = Convert.ToInt32(ActivityNode.Attributes["TIME_MIN"].Value);
                            WriteLogFile("Followup Schedule initialised");
                            break;
                        /*case "AGN_REGULAR":
                            this.AgnRegSchType = ActivityNode.Attributes["RUN"].Value.Trim();
                            this.AgnRegularScheduleHR = Convert.ToInt32(ActivityNode.Attributes["TIME_HR"].Value);
                            this.AgnRegularScheduleMIN = Convert.ToInt32(ActivityNode.Attributes["TIME_MIN"].Value);

                            if (ActivityNode.Attributes["LAST_RUN"].Value.Trim() != "")
                            {
                                LastAgnRegularRun = Convert.ToDateTime(ActivityNode.Attributes["LAST_RUN"].Value.Trim());
                            }
                            else
                            {
                                LastAgnRegularRun = DateTime.MinValue;
                            }
                            NextAgnRegularRun = GetLastDayOfMonth(DateTime.Now);
                            WriteLogFile("Agency Regular Statement Schedule initialised");
                            break;
                        case "AGN_ADDITIONAL":
                            this.AgnAddSchType = ActivityNode.Attributes["RUN"].Value.Trim();
                            this.AgnAddScheduleHR = Convert.ToInt32(ActivityNode.Attributes["TIME_HR"].Value);
                            this.AgnAddScheduleMIN = Convert.ToInt32(ActivityNode.Attributes["TIME_MIN"].Value);

                            if (ActivityNode.Attributes["LAST_RUN"].Value.Trim() != "")
                            {
                                LastAgnAddRun = Convert.ToDateTime(ActivityNode.Attributes["LAST_RUN"].Value.Trim());
                                NextAgnAddRun = GetLastDayOfNextToNextMonth(LastAgnAddRun);
                            }
                            else
                            {
                                LastAgnAddRun = DateTime.MinValue;
                                NextAgnAddRun = GetLastDayOfMonth(DateTime.Now);
                            }
                            WriteLogFile("Agency Additional Statement Schedule initialised");
                            break;
                        case "SM_WRITE_OFF":
                            this.WriteOffSchType = ActivityNode.Attributes["RUN"].Value.Trim();
                            this.WriteOffScheduleHR = Convert.ToInt32(ActivityNode.Attributes["TIME_HR"].Value);
                            this.WriteOffScheduleMIN = Convert.ToInt32(ActivityNode.Attributes["TIME_MIN"].Value);

                            if (ActivityNode.Attributes["LAST_RUN"].Value.Trim() != "")
                            {
                                LastWriteOffRun = Convert.ToDateTime(ActivityNode.Attributes["LAST_RUN"].Value.Trim());
                            }
                            else
                            {
                                LastWriteOffRun = DateTime.MinValue;
                            }
                            NextWriteOffRun = GetLastDayOfMonth(DateTime.Now);
                            WriteLogFile("Small Balance Write off Schedule initialised");
                            break;

                        case "EARNED_PREMIUM":
                            this.EarnedPremSchType = ActivityNode.Attributes["RUN"].Value.Trim();
                            this.EarnedPremScheduleHR = Convert.ToInt32(ActivityNode.Attributes["TIME_HR"].Value);
                            this.EarnedPremScheduleMIN = Convert.ToInt32(ActivityNode.Attributes["TIME_MIN"].Value);

                            if (ActivityNode.Attributes["LAST_RUN"].Value.Trim() != "")
                            {
                                LastEarnedPremRun = Convert.ToDateTime(ActivityNode.Attributes["LAST_RUN"].Value.Trim());
                            }
                            else
                            {
                                LastEarnedPremRun = DateTime.MinValue;
                            }
                            NextEarnedPremRun = GetLastDayOfMonth(DateTime.Now);
                            WriteLogFile("Earned Premium Processing Schedule initialised");
                            break;
                        case "REPORTS":
                            this.ReportSchType = ActivityNode.Attributes["RUN"].Value.Trim();
                            this.ReportScheduleHR = Convert.ToInt32(ActivityNode.Attributes["TIME_HR"].Value);
                            this.ReportScheduleMIN = Convert.ToInt32(ActivityNode.Attributes["TIME_MIN"].Value);

                            if (ActivityNode.Attributes["LAST_RUN"].Value.Trim() != "")
                            {
                                LastReportRun = Convert.ToDateTime(ActivityNode.Attributes["LAST_RUN"].Value.Trim());
                            }
                            else
                            {
                                LastReportRun = DateTime.MinValue;
                            }
                            NextReportRun = GetLastDayOfMonth(DateTime.Now);
                            WriteLogFile("Month END Reports Processing Schedule initialised");
                            break;
                         */ 
                    }
                }


            }
            catch (Exception ex)
            {
                WriteLogFile(ex);
            }
        }

        #endregion

        #region Scheduling Functions

        private void DoScheduledActivities()
        {
            int CurrentTimeHR = System.DateTime.Now.Hour;
            int CurrentTimeMIN = System.DateTime.Now.Minute;

            int CurrentMonth = System.DateTime.Now.Month;
            int CurrentYear = System.DateTime.Now.Year;
            int CurrentDay = System.DateTime.Now.Day;

            //End of MOnth Activities
            //Process Month END reports
            /*
            if (CurrentYear == NextReportRun.Year && CurrentMonth == NextReportRun.Month
                && CurrentDay == NextReportRun.Day && CurrentTimeHR >= ReportScheduleHR
                && CurrentTimeMIN >= ReportScheduleMIN
                && LastReportRun.Date != System.DateTime.Now.Date
                && REPORT_InProgress == false
                && ClsEODCommon.AccountingLocked == false)
            {

                EODLogInfo objLog = new EODLogInfo();
                objLog.Activity = (int)EODActivities.MonthEndReports;
                objLog.SubActivity = (int)EODActivities.MonthEndReports;
                objLog.StartDateTime = DateTime.Now;
                objLog.ActivtyDescription = "Starting Month END Processing for reports";

                try
                {
                    ClsEODCommon objCommon = new ClsEODCommon();

                    REPORT_InProgress = true;
                    ClsEODCommon.AccountingLocked = true;
                    objCommon.ProcessMonthEndReports();

                    LastReportRun = NextReportRun;
                    REPORT_InProgress = false;
                    ClsEODCommon.AccountingLocked = false;


                    string strFileName = AppDomain.CurrentDomain.BaseDirectory + "EODParameters.XML";
                    XmlDocument ConfigXML = new XmlDocument();
                    ConfigXML.Load(strFileName);
                    ConfigXML.SelectSingleNode("Root/EODSchedule/Activity[@NAME='REPORTS']").Attributes["LAST_RUN"].Value = DateTime.Now.ToShortDateString();
                    ConfigXML.Save(strFileName);
                    objLog.Status = ActivityStatus.SUCCEDDED;
                    objLog.EndDateTime = DateTime.Now;
                    ClsEODCommon.WriteLog(objLog);
                }
                catch (Exception ex)
                {
                    objLog.Status = ActivityStatus.FAILED;
                    objLog.EndDateTime = DateTime.Now;
                    objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
                    ClsEODCommon.WriteLog(objLog);
                }
                finally
                {
                    REPORT_InProgress = false;
                    ClsEODCommon.AccountingLocked = false;
                }
                NextReportRun = GetLastDayOfNextMonth(NextReportRun);
            }

            //Process Agency Statement Regular
            if (CurrentYear == NextAgnRegularRun.Year && CurrentMonth == NextAgnRegularRun.Month
                && CurrentDay == NextAgnRegularRun.Day && CurrentTimeHR >= AgnRegularScheduleHR
                && CurrentTimeMIN >= AgnRegularScheduleMIN
                && LastAgnRegularRun.Date != System.DateTime.Now.Date
                && REGCOMM_InProgress == false
                && ClsEODCommon.AccountingLocked == false)
            {
                ClsEODAccounts objAcc = new ClsEODAccounts();
                REGCOMM_InProgress = true;
                ClsEODCommon.AccountingLocked = true;
                objAcc.ProcessAgencyCommission(NextAgnRegularRun.Month);
                LastAgnRegularRun = NextAgnRegularRun;
                REGCOMM_InProgress = false;
                ClsEODCommon.AccountingLocked = false;

                try
                {
                    string strFileName = AppDomain.CurrentDomain.BaseDirectory + "EODParameters.XML";
                    XmlDocument ConfigXML = new XmlDocument();
                    ConfigXML.Load(strFileName);
                    ConfigXML.SelectSingleNode("Root/EODSchedule/Activity[@NAME='AGN_REGULAR']").Attributes["LAST_RUN"].Value = DateTime.Now.ToShortDateString();

                    ConfigXML.Save(strFileName);
                }
                catch (Exception ex)
                {
                }
                NextAgnRegularRun = GetLastDayOfNextMonth(NextAgnRegularRun);

            }

            //Process Agency Statement Additional
            if (CurrentYear == NextAgnAddRun.Year && CurrentMonth == NextAgnAddRun.Month
                && CurrentDay == NextAgnAddRun.Day && CurrentTimeHR >= AgnAddScheduleHR
                && CurrentTimeMIN >= AgnAddScheduleMIN
                && LastAgnAddRun.Date != System.DateTime.Now.Date
                && ADDCOMM_InProgress == false
                && ClsEODCommon.AccountingLocked == false)
            {
                ClsEODAccounts objAcc = new ClsEODAccounts();
                ADDCOMM_InProgress = true;
                ClsEODCommon.AccountingLocked = true;
                objAcc.ProcessAgencyAdditionalCommission(NextAgnAddRun.Month);
                LastAgnAddRun = NextAgnAddRun;
                ADDCOMM_InProgress = false;
                ClsEODCommon.AccountingLocked = false;
                try
                {
                    string strFileName = AppDomain.CurrentDomain.BaseDirectory + "EODParameters.XML";
                    XmlDocument ConfigXML = new XmlDocument();
                    ConfigXML.Load(strFileName);
                    ConfigXML.SelectSingleNode("Root/EODSchedule/Activity[@NAME='AGN_ADDITIONAL']").Attributes["LAST_RUN"].Value = DateTime.Now.ToShortDateString();

                    ConfigXML.Save(strFileName);
                }
                catch (Exception ex)
                {
                }
                NextAgnAddRun = GetLastDayOfNextToNextMonth(NextAgnAddRun);
            }

            //Temporary Change: Small Balance will be done daily 
            //Process SmallBalance Writeoff
            //			if(CurrentYear == NextWriteOffRun.Year  && CurrentMonth == NextWriteOffRun .Month 
            //				&& CurrentDay == NextWriteOffRun.Day && CurrentTimeHR == WriteOffScheduleHR
            //				&& CurrentTimeMIN >= WriteOffScheduleMIN 
            //				&& LastWriteOffRun.Date != System.DateTime.Now.Date )
            //			{

            if (CurrentTimeHR >= WriteOffScheduleHR && CurrentTimeMIN >= WriteOffScheduleMIN
                && LastWriteOffRun.Date != System.DateTime.Now.Date
                && WRITEOFF_InProgress == false
                && ClsEODCommon.AccountingLocked == false)
            {
                ClsEODAccounts objAcc = new ClsEODAccounts();
                WRITEOFF_InProgress = true;
                ClsEODCommon.AccountingLocked = true;
                objAcc.WriteOffSmallBalance();
                //LastWriteOffRun = NextWriteOffRun;
                LastWriteOffRun = DateTime.Now;
                WRITEOFF_InProgress = false;
                ClsEODCommon.AccountingLocked = false;

                try
                {
                    string strFileName = AppDomain.CurrentDomain.BaseDirectory + "EODParameters.XML";
                    XmlDocument ConfigXML = new XmlDocument();
                    ConfigXML.Load(strFileName);
                    ConfigXML.SelectSingleNode("Root/EODSchedule/Activity[@NAME='SM_WRITE_OFF']").Attributes["LAST_RUN"].Value = DateTime.Now.ToShortDateString();

                    ConfigXML.Save(strFileName);
                }
                catch (Exception ex)
                {
                }
                NextWriteOffRun = GetLastDayOfNextMonth(NextWriteOffRun);
            }

            //Process Earned Premium
            if (CurrentYear == NextEarnedPremRun.Year && CurrentMonth == NextEarnedPremRun.Month
                && CurrentDay == NextEarnedPremRun.Day && CurrentTimeHR >= EarnedPremScheduleHR
                && CurrentTimeMIN >= EarnedPremScheduleMIN
                && LastEarnedPremRun.Date != System.DateTime.Now.Date
                && EPR_InProcress == false
                && ClsEODCommon.AccountingLocked == false)
            {

                EODLogInfo objLog = new EODLogInfo();
                objLog.Activity = (int)EODActivities.Accounting;
                objLog.SubActivity = (int)EODActivities.Accounting;
                objLog.StartDateTime = DateTime.Now;
                objLog.ActivtyDescription = "Starting Earned Premium Processing";

                ClsEODAccounts objAcc = new ClsEODAccounts();

                EPR_InProcress = true;
                ClsEODCommon.AccountingLocked = true;

                if (objAcc.ProcessEarnedPremium(NextEarnedPremRun.Month, NextEarnedPremRun.Year))
                {
                    LastEarnedPremRun = NextEarnedPremRun;
                    try
                    {
                        string strFileName = AppDomain.CurrentDomain.BaseDirectory + "EODParameters.XML";
                        XmlDocument ConfigXML = new XmlDocument();
                        ConfigXML.Load(strFileName);
                        ConfigXML.SelectSingleNode("Root/EODSchedule/Activity[@NAME='EARNED_PREMIUM']").Attributes["LAST_RUN"].Value = DateTime.Now.ToShortDateString();
                        ConfigXML.Save(strFileName);
                        objLog.Status = ActivityStatus.SUCCEDDED;
                        objLog.EndDateTime = DateTime.Now;
                        ClsEODCommon.WriteLog(objLog);
                    }
                    catch (Exception ex)
                    {
                        objLog.Status = ActivityStatus.FAILED;
                        objLog.EndDateTime = DateTime.Now;
                        objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
                        ClsEODCommon.WriteLog(objLog);
                    }
                    NextEarnedPremRun = GetLastDayOfNextMonth(NextEarnedPremRun);
                }

                EPR_InProcress = false;
                ClsEODCommon.AccountingLocked = false;

            }


            if (RunACCNow == true ||
                (CurrentTimeHR >= AccountEODScheduleHR && CurrentTimeMIN >= AccountEODScheduleMIN
                    && LastAccountRun.Date != System.DateTime.Now.Date)
                )
            {
                if (ClsEODCommon.AccountingLocked == false)
                {
                    //Set Last Run to today so that next Timer iteration does't start this thread
                    LastAccountRun = DateTime.Now;
                    EODLogInfo objLog = new EODLogInfo();
                    objLog.Activity = (int)EODActivities.ServiceManager;
                    objLog.SubActivity = (int)EODActivities.ActivityThread;
                    objLog.StartDateTime = DateTime.Now;
                    objLog.ActivtyDescription = "Starting Accounting Thread";

                    try
                    {
                        RunACCNow = false;
                        ClsEODAccounts objEODAcc = new ClsEODAccounts();
                        objEODAcc.StartAccountingThread();
                        objLog.Status = ActivityStatus.SUCCEDDED;
                        objLog.EndDateTime = DateTime.Now;
                        ClsEODCommon.WriteLog(objLog);
                    }
                    catch (Exception ex)
                    {
                        objLog.Status = ActivityStatus.FAILED;
                        objLog.EndDateTime = DateTime.Now;
                        objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
                        ClsEODCommon.WriteLog(objLog);
                    }
                }
            }
            if (RunEFTNow == true ||
                (CurrentTimeHR >= EFTScheduleHR && CurrentTimeMIN >= EFTScheduleMIN
                    && LastEFTRun.Date != System.DateTime.Now.Date)
                )
            {
                if (ClsEODCommon.AccountingLocked == false)
                {
                    LastEFTRun = DateTime.Now;

                    EODLogInfo objLog = new EODLogInfo();
                    objLog.Activity = (int)EODActivities.ServiceManager;
                    objLog.SubActivity = (int)EODActivities.ActivityThread;
                    objLog.StartDateTime = DateTime.Now;
                    objLog.ActivtyDescription = "Starting EFT Thread";
                    try
                    {
                        RunEFTNow = false;
                        ClsEODAccounts objEODAcc = new ClsEODAccounts();
                        objEODAcc.StartEFTProcess();
                        objLog.Status = ActivityStatus.SUCCEDDED;
                        objLog.EndDateTime = DateTime.Now;
                        ClsEODCommon.WriteLog(objLog);
                    }
                    catch (Exception ex)
                    {
                        objLog.Status = ActivityStatus.FAILED;
                        objLog.EndDateTime = DateTime.Now;
                        objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
                        ClsEODCommon.WriteLog(objLog);
                    }
                }
            }
            */
            if (RunPOLProcessNow == true ||
                (CurrentTimeHR >= ProcessEODScheduleHR && CurrentTimeMIN >= ProcessEODScheduleMIN
                && LastProcessRun.Date != System.DateTime.Now.Date)
                )
            {
                if (ClsEODCommon.AccountingLocked == false)
                {
                    LastProcessRun = DateTime.Now;
                    EODLogInfo objLog = new EODLogInfo();
                    objLog.Activity = (int)EODActivities.ServiceManager;
                    objLog.SubActivity = (int)EODActivities.ActivityThread;
                    objLog.StartDateTime = DateTime.Now;
                    objLog.ActivtyDescription = "Starting Policy Processing Thread";
                    try
                    {
                        if (objPolicyProcessThread == null)
                        {
                            RunPOLProcessNow = false;
                            objPolicyProcessThread = new Thread(new ThreadStart(PolProcessThread));
                            objPolicyProcessThread.Priority = ThreadPriority.Normal;
                            objPolicyProcessThread.Start();
                            objLog.Status = ActivityStatus.SUCCEDDED;
                            objLog.EndDateTime = DateTime.Now;
                            ClsEODCommon.WriteLog(objLog);
                        }
                        else
                        {
                            if (objPolicyProcessThread.ThreadState == System.Threading.ThreadState.Suspended)
                            {
                                objLog.ActivtyDescription += " .. Thread already there in sleep mode.Trying to resume thread. ";
                                objPolicyProcessThread.Resume();
                                objLog.Status = ActivityStatus.SUCCEDDED;
                                objLog.EndDateTime = DateTime.Now;
                                ClsEODCommon.WriteLog(objLog);
                            }
                            else if (objPolicyProcessThread.ThreadState == System.Threading.ThreadState.Stopped)
                            {
                                objPolicyProcessThread.Start();
                                objLog.Status = ActivityStatus.SUCCEDDED;
                                objLog.EndDateTime = DateTime.Now;
                                ClsEODCommon.WriteLog(objLog);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        objLog.Status = ActivityStatus.FAILED;
                        objLog.EndDateTime = DateTime.Now;
                        objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
                        ClsEODCommon.WriteLog(objLog);
                    }
                }
            }

            if (RunClaimsNow == true ||
               (CurrentTimeHR >= ClaimsEODScheduleHR && CurrentTimeMIN >= ClaimsEODScheduleMIN
               && LastClaimRun.Date != System.DateTime.Now.Date)
               )
            {
                if (ClsEODCommon.AccountingLocked == false)
                {
                    LastClaimRun = DateTime.Now;
                    EODLogInfo objLog = new EODLogInfo();
                    objLog.Activity = (int)EODActivities.ServiceManager;
                    objLog.SubActivity = (int)EODActivities.ActivityThread;
                    objLog.StartDateTime = DateTime.Now;
                    objLog.ActivtyDescription = "Starting Claims Processing Thread";
                    try
                    {
                        if (objClaimThread == null)
                        {
                            RunClaimsNow = false;
                            objClaimThread = new Thread(new ThreadStart(ClaimThread));
                            objClaimThread.Priority = ThreadPriority.Normal;
                            objClaimThread.Start();
                            objLog.Status = ActivityStatus.SUCCEDDED;
                            objLog.EndDateTime = DateTime.Now;
                            ClsEODCommon.WriteLog(objLog);
                        }
                        else
                        {
                            if (objClaimThread.ThreadState == System.Threading.ThreadState.Suspended)
                            {
                                objLog.ActivtyDescription += " .. Thread already there in sleep mode.Trying to resume thread. ";
                                objClaimThread.Resume();
                                objLog.Status = ActivityStatus.SUCCEDDED;
                                objLog.EndDateTime = DateTime.Now;
                                ClsEODCommon.WriteLog(objLog);
                            }
                            else if (objClaimThread.ThreadState == System.Threading.ThreadState.Stopped)
                            {
                                objClaimThread.Start();
                                objLog.Status = ActivityStatus.SUCCEDDED;
                                objLog.EndDateTime = DateTime.Now;
                                ClsEODCommon.WriteLog(objLog);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        objLog.Status = ActivityStatus.FAILED;
                        objLog.EndDateTime = DateTime.Now;
                        objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
                        ClsEODCommon.WriteLog(objLog);
                    }
                }
            }
            /* if (
                 (CurrentTimeHR >= PremNoticeScheduleHR && CurrentTimeMIN >= PremNoticeScheduleMIN
                 && LastPremNoticeRun.Date != System.DateTime.Now.Date && PNGenerationInProgress == false)
                 )
             {
                 PNGenerationInProgress = true;
                 if (ClsEODCommon.AccountingLocked == false)
                 {
                     ClsEODCommon.AccountingLocked = true;

                     bool ThreadSuccedded = false;

                     ClsEODAccounts objAcc = new ClsEODAccounts();

                     if (objAcc.SpoolPremiumNotices())
                     {
                         if (objAcc.GeneratePremiumNotice())
                         {
                             ThreadSuccedded = true;
                         }
                     }

                     if (ThreadSuccedded)
                     {
                         LastPremNoticeRun = DateTime.Now;
                     }
                     ClsEODCommon.AccountingLocked = false;
                 }
                 PNGenerationInProgress = false;
             }*/
        }

        private void DoScheduledSpooling()
        {
            int CurrentTimeHR = System.DateTime.Now.Hour;
            int CurrentTimeMIN = System.DateTime.Now.Minute;

            if (this.AccSpoolScheduleHR == CurrentTimeHR && AccSpoolScheduleMIN <= CurrentTimeMIN
                && LastAccSpoolRun.Date != DateTime.Now.Date
                && ClsEODCommon.AccountingLocked == false)
            {
                LastAccSpoolRun = DateTime.Now;

                EODLogInfo objLog = new EODLogInfo();
                objLog.Activity = (int)EODActivities.ServiceManager;
                objLog.SubActivity = (int)EODActivities.SpoolThread;
                objLog.StartDateTime = DateTime.Now;
                objLog.ActivtyDescription = "Starting Accounting Spool Thread";
                try
                {
                    if (objAccSpoolingThread == null)
                    {
                        objAccSpoolingThread = new Thread(new ThreadStart(AccSpoolThread));
                        objAccSpoolingThread.Priority = ThreadPriority.Normal;
                        //objAccSpoolingThread.Start(); commneted on 27 dec for temp
                        objLog.Status = ActivityStatus.SUCCEDDED;
                        objLog.EndDateTime = DateTime.Now;
                        ClsEODCommon.WriteLog(objLog);
                    }
                    else
                    {
                        if (objAccSpoolingThread.ThreadState == System.Threading.ThreadState.Suspended)
                        {
                            objLog.ActivtyDescription += " .. Thread already there in sleep mode.Trying to resume thread. ";
                            objAccSpoolingThread.Resume();
                            objLog.Status = ActivityStatus.SUCCEDDED;
                            objLog.EndDateTime = DateTime.Now;
                            ClsEODCommon.WriteLog(objLog);
                        }
                        else if (objAccSpoolingThread.ThreadState == System.Threading.ThreadState.Stopped)
                        {
                            objAccSpoolingThread.Start();
                            objLog.Status = ActivityStatus.SUCCEDDED;
                            objLog.EndDateTime = DateTime.Now;
                            ClsEODCommon.WriteLog(objLog);
                        }

                    }
                }
                catch (Exception ex)
                {
                    objLog.Status = ActivityStatus.FAILED;
                    objLog.EndDateTime = DateTime.Now;
                    objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
                    ClsEODCommon.WriteLog(objLog);
                }

            }


            if (this.FollowupScheduleHR == CurrentTimeHR && FollowupScheduleMIN <= CurrentTimeMIN
                && LastFollowUpRun.Date != DateTime.Now.Date)
            {
                LastFollowUpRun = DateTime.Now;

                EODLogInfo objLog = new EODLogInfo();
                objLog.Activity = (int)EODActivities.PolicyProcess;
                objLog.SubActivity = (int)EODActivities.FollowUpCreation;
                objLog.StartDateTime = DateTime.Now;
                objLog.ActivtyDescription = "Sending Follow up for pending processes";
                try
                {
                    ClsEODPolicyProcess objPrc = new ClsEODPolicyProcess();
                    objPrc.AddFollowup();
                    objLog.Status = ActivityStatus.SUCCEDDED;
                    objLog.EndDateTime = DateTime.Now;
                    ClsEODCommon.WriteLog(objLog);
                }
                catch (Exception ex)
                {
                    objLog.Status = ActivityStatus.FAILED;
                    objLog.EndDateTime = DateTime.Now;
                    objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
                    ClsEODCommon.WriteLog(objLog);
                }

            }
        }

        #endregion

        #region Process Thread
        private void PolProcessThread()
        {
            EODLogInfo objLog = new EODLogInfo();
            objLog.Activity = (int)EODActivities.PolicyProcess;
            objLog.SubActivity = (int)EODActivities.PolicyProcess;
            ClsEODCommon.AccountingLocked = true;
            try
            {
                ClsEODCancellation objCancel = new ClsEODCancellation();

                objLog.ActivtyDescription = "Auto Launch of cancellation rollback completed";
                objLog.StartDateTime = DateTime.Now;
                objCancel.LaunchAutoRollBack();
                objLog.EndDateTime = DateTime.Now;
                objLog.Status = ActivityStatus.SUCCEDDED;
                ClsEODCommon.WriteLog(objLog);

                /* Stop Auto Cancellation Commit.itrack # 1392.commented by Lalit July 19 2011.
                objLog.ActivtyDescription = "Auto Launch of cancellation commit completed";
                objLog.StartDateTime = DateTime.Now;
                objCancel.LaunchAutoCommit();
                objLog.EndDateTime = DateTime.Now;
                objLog.Status = ActivityStatus.SUCCEDDED;
                ClsEODCommon.WriteLog(objLog);
                */

                //Perform Renewal
                /* NO Auto Renewal would happned ni EbixAdvantage - Brazil
                ClsEODRenewal objRenewal = new ClsEODRenewal();
                objLog.ActivtyDescription = "Auto Launch of Renewal completed";
                objLog.StartDateTime = DateTime.Now;
                objRenewal.LaunchRenewal();
                objLog.EndDateTime = DateTime.Now;
                objLog.Status = ActivityStatus.SUCCEDDED;
                ClsEODCommon.WriteLog(objLog);

                objRenewal = new ClsEODRenewal();
                objLog.ActivtyDescription = "Auto Launch of Renewal Commit completed";
                objLog.StartDateTime = DateTime.Now;
                objRenewal = new ClsEODRenewal();
                objRenewal.LaunchAutoCommit();
                objLog.EndDateTime = DateTime.Now;
                objLog.Status = ActivityStatus.SUCCEDDED;
                ClsEODCommon.WriteLog(objLog);

                objLog.ActivtyDescription = "Sending Follow-up for Renewal completed";
                objLog.StartDateTime = DateTime.Now;
                objRenewal = new ClsEODRenewal();
                objRenewal.SendRenewalPendingFollowUp();
                objLog.EndDateTime = DateTime.Now;
                objLog.Status = ActivityStatus.SUCCEDDED;
                ClsEODCommon.WriteLog(objLog);
                
                objRenewal.DoCleanUP();
                */
                objLog.ActivtyDescription = "Auto Launch of cancellation completed";
                objLog.StartDateTime = DateTime.Now;
                objCancel.LaunchCancellation();
                objLog.EndDateTime = DateTime.Now;
                objLog.Status = ActivityStatus.SUCCEDDED;
                ClsEODCommon.WriteLog(objLog);

                ClsEODPolicyProcess objPolProcess = new ClsEODPolicyProcess();
                objPolProcess.UpdatePolicyStatus();
                /*
                objPolProcess.GenerateRTLHotFile();

                ClsEODAccounts objAcc = new ClsEODAccounts();
                objAcc.GeneratePostivePay();

                ClsEODCommon eodCommon = new ClsEODCommon();
                eodCommon.SaveReportSnapshot();
                */
            }
            catch (Exception ex)
            {
                objLog.Status = ActivityStatus.FAILED;
                objLog.EndDateTime = DateTime.Now;
                objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
                ClsEODCommon.WriteLog(objLog);
            }
            finally
            {
                ClsEODCommon.AccountingLocked = false;
                try
                {
                    objPolicyProcessThread.Abort();
                }
                catch (ThreadAbortException ex)
                {
                    objPolicyProcessThread = null;
                }

            }
        }

        #endregion
        #region Claim Thread
        private void ClaimThread()
        {
            EODLogInfo objLog = new EODLogInfo();
            objLog.Activity = (int)EODActivities.ClaimsProcessing;
            objLog.SubActivity = (int)EODActivities.ClaimsProcessing;
            ClsEODCommon.AccountingLocked = true;
            try
            {
                ClsEODClaims objClaims = new ClsEODClaims();
                // Closing claim if reserve is 0               
                try
                {
                    objLog.ActivtyDescription = "Auto Launch of Claims Close completed";
                    objLog.StartDateTime = DateTime.Now;
                    objClaims.LaunchAutoCloseClaims();
                    objLog.EndDateTime = DateTime.Now;
                    objLog.Status = ActivityStatus.SUCCEDDED;
                    ClsEODCommon.WriteLog(objLog);
                    //LastClaimRun = NextClaimRun;
                    NextClaimRun = GetLastDayOfNextMonth(NextClaimRun);
                }
                catch (Exception ex)
                {
                    objLog.Status = ActivityStatus.FAILED;
                    objLog.EndDateTime = DateTime.Now;
                    objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
                    ClsEODCommon.WriteLog(objLog);
                }
                // updating reserve at month end               
                int CurrentMonth = System.DateTime.Now.Month;
                int CurrentYear = System.DateTime.Now.Year;
                int CurrentDay = System.DateTime.Now.Day;
                try
                {
                    if (CurrentYear == NextClaimRun.Year && CurrentMonth == NextClaimRun.Month
                       && CurrentDay == NextClaimReserveUpdate.Day && LastClaimReserveUpdate.Date != System.DateTime.Now.Date
                       )
                    {
                        Claim_InProcess = true;
                        objLog.ActivtyDescription = "Auto Launch of Claims Reserve update completed";
                        objLog.StartDateTime = DateTime.Now;
                        objClaims.LaunchAutoUpdateReserve();
                        objLog.EndDateTime = DateTime.Now;

                        LastClaimReserveUpdate = NextClaimReserveUpdate;
                        try
                        {
                            string strFileName = AppDomain.CurrentDomain.BaseDirectory + "EODParameters.XML";
                            XmlDocument ConfigXML = new XmlDocument();
                            ConfigXML.Load(strFileName);
                            ConfigXML.SelectSingleNode("Root/EODSchedule/Activity[@NAME='CLAIMS']").Attributes["LAST_RESERVE_UPDATE"].Value = DateTime.Now.ToShortDateString();
                            ConfigXML.Save(strFileName);
                            ConfigXML = null;
                            objLog.Status = ActivityStatus.SUCCEDDED;
                            ClsEODCommon.WriteLog(objLog);
                        }
                        catch (Exception ex)
                        {
                            objLog.ActivtyDescription = ": but Config file last run date update failed";
                            objLog.Status = ActivityStatus.SUCCEDDED;
                            objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
                            ClsEODCommon.WriteLog(objLog);
                        }

                        NextClaimReserveUpdate = GetLastDayOfNextMonth(NextClaimReserveUpdate);
                    }

                    Claim_InProcess = false;
                    ClsEODCommon.AccountingLocked = false;

                }
                catch (Exception ex)
                {
                    objLog.Status = ActivityStatus.FAILED;
                    objLog.EndDateTime = DateTime.Now;
                    objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
                    ClsEODCommon.WriteLog(objLog);
                }
               
            }
            catch (Exception ex)
            {
                objLog.Status = ActivityStatus.FAILED;
                objLog.EndDateTime = DateTime.Now;
                objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
                ClsEODCommon.WriteLog(objLog);
            }
            finally
            {
                ClsEODCommon.AccountingLocked = false;
                try
                {
                    objClaimThread.Abort();
                }
                catch (ThreadAbortException ex)
                {
                    objClaimThread = null;
                }

            }
        }

        #endregion
        #region Spooling Threads
        private void AccSpoolThread()
        {
            ClsEODCommon.AccountingLocked = true;

            //Ravindra commit batch commit of deposits to EOD
            ClsEODAccounts objAcc = new ClsEODAccounts();
            objAcc.StartBatchCommitOfDeposits();

            EODLogInfo objLog = new EODLogInfo();
            objLog.Activity = (int)EODActivities.Spooling;

            ClsEODAccounts objEODAccount = new ClsEODAccounts();
            try
            {
                objLog.ActivtyDescription = "Adding records to EFT Spool completed";
                objLog.SubActivity = (int)EODActivities.EFTSpooling;
                objLog.StartDateTime = DateTime.Now;
                objEODAccount.StartSpoolToEFT();
                objLog.Status = ActivityStatus.SUCCEDDED;
                objLog.EndDateTime = DateTime.Now;
                ClsEODCommon.WriteLog(objLog);
            }
            catch (Exception ex)
            {
                objLog.Status = ActivityStatus.FAILED;
                objLog.EndDateTime = DateTime.Now;
                objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
                ClsEODCommon.WriteLog(objLog);
            }
            try
            {
                objLog.ActivtyDescription = "Adding records to Credit Card Spool completed";
                objLog.SubActivity = (int)EODActivities.CreditCardSpooling;
                objLog.StartDateTime = DateTime.Now;
                objEODAccount.StartSpoolToCreditCard();
                objLog.Status = ActivityStatus.SUCCEDDED;
                objLog.EndDateTime = DateTime.Now;
                ClsEODCommon.WriteLog(objLog);
            }
            catch (Exception ex)
            {
                objLog.Status = ActivityStatus.FAILED;
                objLog.EndDateTime = DateTime.Now;
                objLog.AdditionalInfo = ClsEODCommon.GetAdditionalInfo(ex);
                ClsEODCommon.WriteLog(objLog);
            }
            ClsEODCommon.AccountingLocked = false;

            try
            {
                objAccSpoolingThread.Abort();
            }
            catch (ThreadAbortException ex)
            {
                objAccSpoolingThread = null;
            }


        }

        #endregion

        #region Utility Functions

        private DateTime GetLastDayOfNextToNextMonth(DateTime dt)
        {
            int NextMonth = dt.Month + 1;
            int Year = dt.Year;
            if (NextMonth > 12)
            {
                NextMonth = 1;
                Year++;
            }
            return GetLastDayOfNextMonth(new DateTime(Year, NextMonth, 1));
        }

        private DateTime GetLastDayOfNextMonth(DateTime dt)
        {
            int NextMonth = dt.Month + 1;
            int Year = dt.Year;
            if (NextMonth > 12)
            {
                NextMonth = 1;
                Year++;
            }
            return GetLastDayOfMonth(new DateTime(Year, NextMonth, 1));
        }

        private DateTime GetLastDayOfMonth(DateTime dt)
        {
            DateTime dteFirstDayNextMonth;
            int NextMonth = dt.Month + 1;
            int Year = dt.Year;
            if (NextMonth > 12)
            {
                NextMonth = 1;
                Year++;
            }
            dteFirstDayNextMonth = new DateTime(Year, NextMonth, 1);
            return dteFirstDayNextMonth.AddDays(-1);
        }

        private void WriteLogFile(string str)
        {
            TextWriter tw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\EODLogData.log", true);
            tw.WriteLine(" Date: " + DateTime.Now + "     Step:- " + str + "\n\r");
            tw.Close();

        }
        private void WriteLogFile(Exception ex)
        {
            //AppDomain.CurrentDomain.BaseDirectory
            TextWriter tw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\EODLog.log", true);
            tw.WriteLine("Error occured : Date:" + DateTime.Now + " Error:" + ex.Message + " Stack:" + ex.StackTrace + "\n\r");
            tw.Close();
        }

        #endregion
        private void timDeposit_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            return;
            if (!DEPOSITS_InProcess)
            {
                if (!ClsEODCommon.AccountingLocked)
                {
                    ClsEODAccounts objAcc = new ClsEODAccounts();
                    DEPOSITS_InProcess = true;
                    ClsEODCommon.AccountingLocked = true;
                    objAcc.CommitSpooledDeposits();
                    DEPOSITS_InProcess = false;
                    ClsEODCommon.AccountingLocked = false;
                    SpoolCounter++;
                }
            }

            if (SpoolCounter >= 3)
            {
                if (!RECON_InProcess)
                {
                    if (!ClsEODCommon.AccountingLocked)
                    {
                        ClsEODAccounts objAcc = new ClsEODAccounts();
                        ClsEODCommon.AccountingLocked = true;
                        RECON_InProcess = true;
                        objAcc.ReconCustomerNegativeItems();
                        ClsEODCommon.AccountingLocked = false;
                        RECON_InProcess = false;
                        SpoolCounter = 0;
                    }
                }
            }

        }
    }
}
