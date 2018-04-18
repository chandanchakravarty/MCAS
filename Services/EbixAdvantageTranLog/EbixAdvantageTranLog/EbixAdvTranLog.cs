using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;


namespace EbixAdvantageTranLog
{
    public partial class EbixAdvTranLog : ServiceBase
    {
       
        private System.Timers.Timer timLogInterval;
        TLogUpdater objLogUpdater = null;

        public EbixAdvTranLog()
        {
            InitializeComponent();
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
            ServicesToRun = new System.ServiceProcess.ServiceBase[] { new EbixAdvTranLog() };

            System.ServiceProcess.ServiceBase.Run(ServicesToRun);
        }

        protected override void OnStart(string[] args)
        {
            StartLogUpdation();
        }

        protected override void OnStop()
        {
            timLogInterval.Enabled = false;
            if (objLogUpdater != null)
                objLogUpdater.Dispose();
        }
        protected override void OnCustomCommand(int command)
        {
            //base.OnCustomCommand (command);
            timLogInterval.Enabled = false;
            switch (command)
            {
                case 128://5
                    timLogInterval.Interval = 1000 * 60 * 5;
                    break;
                case 129://15
                    timLogInterval.Interval = 1000 * 60 * 15;
                    break;
                case 130://30
                    timLogInterval.Interval = 1000 * 60 * 30;
                    break;
                case 131://60
                    timLogInterval.Interval = 1000 * 60 * 60;
                    break;
                default:
                    timLogInterval.Interval = 1000 * 60 * 5;
                    break;
            }
            timLogInterval.Enabled = true;
        }
        private void WriteLogFile(LogEventArgs e)
        {
            //AppDomain.CurrentDomain.BaseDirectory
            TextWriter tw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Brics_t_log.log", true);
            if (e.status == LogEventArgs.LogStatus.ERROR)
            {
                tw.WriteLine("Error occured updating transaction log: Date:" + DateTime.Now + " Error:" + e.ActualException.Message + " +Stack:" + e.ActualException.StackTrace + "\n");
            }
            else
            {
                tw.WriteLine("Transaction Log updated successfully: Date:" + DateTime.Now + "\n");
            }
            tw.Close();
        }
        private void timLogInterval_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            StartLogUpdation();
        }
        private void LogUpdateHandeler(object sender, LogEventArgs e)
        {
            try
            {
                WriteLogFile(e);
                timLogInterval.Enabled = true;
            }
            catch (Exception ex) { }
            finally
            {
                if (objLogUpdater != null)
                    objLogUpdater.Dispose();
            }
        }
        private void StartLogUpdation()
        {
            try
            {
                timLogInterval.Enabled = false;
                objLogUpdater = new TLogUpdater();
                objLogUpdater.LogUpdated += new LogUpdateListeners(this.LogUpdateHandeler);
                objLogUpdater.UpdateLog();
            }
            catch (Exception ex)
            {
                if (objLogUpdater != null)
                    objLogUpdater.Dispose();
            }
        }
    }
}
