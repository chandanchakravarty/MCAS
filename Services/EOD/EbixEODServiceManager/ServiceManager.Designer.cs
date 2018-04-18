namespace EbixEODServiceManager
{
    partial class ServiceManager
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        #region Private Variables Declaration

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.Timers.Timer timServiceInterval;
        private System.Timers.Timer timSpoolInterval;
        private System.ComponentModel.Container Modelcomponents = null;

        private System.Xml.XmlDocument xmlDoc;
        private System.DateTime LastEFTRun;
        private System.DateTime LastAccountRun;
        private System.DateTime LastProcessRun;
        

        private int EFTScheduleHR;
        private int EFTScheduleMIN;
        private bool RunEFTNow = false;
       
        private int AccountEODScheduleHR;
        private int AccountEODScheduleMIN;
        private bool RunACCNow = false;
      

        private int ProcessEODScheduleHR;
        private int ProcessEODScheduleMIN;
        private bool RunPOLProcessNow = false;

        private int ClaimsEODScheduleHR;
        private int ClaimsEODScheduleMIN;
        private bool RunClaimsNow = false;
        
        private string ClaimSchType;
        private System.DateTime LastClaimRun;
        private System.DateTime NextClaimRun;
        private System.DateTime LastClaimReserveUpdate;
        private System.DateTime NextClaimReserveUpdate;

        private int AccSpoolScheduleHR;
        private int AccSpoolScheduleMIN;
        private System.DateTime LastAccSpoolRun;

        //PREM_NOTICES
        private int PremNoticeScheduleHR;
        private int PremNoticeScheduleMIN;
        private System.DateTime LastPremNoticeRun;
        private bool PNGenerationInProgress;

        private int FollowupScheduleHR;
        private int FollowupScheduleMIN;
        private System.DateTime LastFollowUpRun;

        private int AgnRegularScheduleHR;
        private int AgnRegularScheduleMIN;
        private string AgnRegSchType;
        private System.DateTime LastAgnRegularRun;
        private System.DateTime NextAgnRegularRun;
        private bool REGCOMM_InProgress;

        private int AgnAddScheduleHR;
        private int AgnAddScheduleMIN;
        private string AgnAddSchType;
        private System.DateTime LastAgnAddRun;
        private System.DateTime NextAgnAddRun;
        private bool ADDCOMM_InProgress;

        private int WriteOffScheduleHR;
        private int WriteOffScheduleMIN;
        private string WriteOffSchType;
        private System.DateTime LastWriteOffRun;
        private System.DateTime NextWriteOffRun;
        private bool WRITEOFF_InProgress;

        private int EarnedPremScheduleHR;
        private int EarnedPremScheduleMIN;
        private string EarnedPremSchType;
        private System.DateTime LastEarnedPremRun;
        private System.DateTime NextEarnedPremRun;
        private bool EPR_InProcress;


        private int ReportScheduleHR;
        private int ReportScheduleMIN;
        private string ReportSchType;
        private System.DateTime LastReportRun;
        private System.DateTime NextReportRun;
        private bool REPORT_InProgress;

        private System.Threading.Thread objPolicyProcessThread;
        private System.Timers.Timer timDeposit;
        private System.Threading.Thread objAccSpoolingThread;
        private System.Threading.Thread objClaimThread;

        private int SpoolCounter;

        private bool DEPOSITS_InProcess;
        private bool RECON_InProcess;
        private bool Claim_InProcess;

        #endregion
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // ServiceManager
            // 
            //this.ServiceName = "EbixEODService";
            this.timServiceInterval = new System.Timers.Timer();
            this.timSpoolInterval = new System.Timers.Timer();
            this.timDeposit = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.timServiceInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timSpoolInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timDeposit)).BeginInit();
            // 
            // timServiceInterval
            // 
            this.timServiceInterval.Enabled = true;
            this.timServiceInterval.Interval = 100000;
            this.timServiceInterval.Elapsed += new System.Timers.ElapsedEventHandler(this.timServiceInterval_Elapsed);
            // 
            // timSpoolInterval
            // 
            this.timSpoolInterval.Enabled = true;
            this.timSpoolInterval.Interval = 100000;
            this.timSpoolInterval.Elapsed += new System.Timers.ElapsedEventHandler(this.timSpoolInterval_Elapsed);
            // 
            // timDeposit
            // 
            this.timDeposit.Enabled = true;
            //Set timer of 10 Mins
            this.timDeposit.Interval = (60000) * 10;
            this.timDeposit.Elapsed += new System.Timers.ElapsedEventHandler(timDeposit_Elapsed);
            // 
            // ServiceManager
            // 
            this.ServiceName = "EbixAdvantage EOD Process Manager";
            ((System.ComponentModel.ISupportInitialize)(this.timServiceInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timSpoolInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timDeposit)).EndInit();

        }

        #endregion
    }
}
