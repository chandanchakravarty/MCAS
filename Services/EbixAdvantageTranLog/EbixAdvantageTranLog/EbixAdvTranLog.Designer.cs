namespace EbixAdvantageTranLog
{
    partial class EbixAdvTranLog
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
            //components = new System.ComponentModel.Container();
            //this.ServiceName = "Service1";

            this.timLogInterval = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.timLogInterval)).BeginInit();
            // 
            // timLogInterval
            // 
            this.timLogInterval.Interval = 600000;
            this.timLogInterval.Elapsed += new System.Timers.ElapsedEventHandler(this.timLogInterval_Elapsed);
            // 
            // TLogService
            // 
            this.ServiceName = "Ebix Advantage Transaction Log Service";
            ((System.ComponentModel.ISupportInitialize)(this.timLogInterval)).EndInit();

        }

        #endregion
    }
}
