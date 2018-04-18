namespace EbixEODServiceManager
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
            this.EODServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.EODServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // EODServiceProcessInstaller
            // 
            this.EODServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.EODServiceProcessInstaller.Password = null;
            this.EODServiceProcessInstaller.Username = null;
            // 
            // EODServiceInstaller
            // 
            this.EODServiceInstaller.ServiceName = "EbixAdvantage EOD Service";
            this.EODServiceInstaller.DisplayName = "EbixAdvantage EOD Service";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.EODServiceProcessInstaller,
            this.EODServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller EODServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller EODServiceInstaller;
    }
}