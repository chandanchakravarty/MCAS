namespace EbixAdvantageTranLog
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.tLogServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.tLogServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // tLogServiceProcessInstaller
            // 
            this.tLogServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.tLogServiceProcessInstaller.Password = null;
            this.tLogServiceProcessInstaller.Username = null;
            this.tLogServiceProcessInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.tLogServiceProcessInstaller_AfterInstall);
            // 
            // tLogServiceInstaller
            // 
            this.tLogServiceInstaller.DisplayName = "EbixAdvantage TranLog Update Service";
            this.tLogServiceInstaller.ServiceName = "EbixAdvTLogService";
            this.tLogServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.tLogServiceProcessInstaller,
            this.tLogServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller tLogServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller tLogServiceInstaller;
    }
}