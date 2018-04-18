using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.ServiceProcess;
using EODCommon;
using System.IO;
using System.Text;
using System.Xml ;
namespace ServiceManagerDesktop
{
	/// <summary>
	/// Summary description for frmMain.
	/// </summary>
	public partial class frmMain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblMsg;
		//private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ContextMenu mnuMainMenu;
		private System.Windows.Forms.NotifyIcon trayIcon;
		private System.Windows.Forms.MenuItem miStartEOD;
		private System.Windows.Forms.MenuItem miPauseEOD;
		private System.Windows.Forms.MenuItem miStopEOD;
		private System.Windows.Forms.MenuItem miAbout;
		private System.Windows.Forms.MenuItem miShowEODStatus;
		private System.Windows.Forms.MenuItem miRunEft;
		private System.Windows.Forms.MenuItem miAccounting;
		private System.Windows.Forms.MenuItem mnPolProcess;
		private System.Windows.Forms.MenuItem menuItem1;
		private ServiceController EODController= null;

		public frmMain()
		{

			
			InitializeComponent();
			
			EODController = new ServiceController("EbixAdvantage EOD Service");
			
			// File StreamReader
			try
			{
				string FilePath = AppDomain.CurrentDomain.BaseDirectory + "Manager.INI";
				StreamReader objSR	= new StreamReader(FilePath);
				string strMachineName = objSR.ReadLine();
				objSR.Close();
				EODController.MachineName = strMachineName;
			}
			catch(Exception ex)
			{
				MessageBox.Show("Can't find INI file. Please close the application by clicking Exit Menu ","Critical Error");
				Application.Exit(); 
			}
		}

		[STAThread]
		static void Main() 
		{
			Application.Run(new frmMain());
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
        //protected override void Dispose( bool disposing )
        //{
        //    if( disposing )
        //    {
        //        if(components != null)
        //        {
        //            components.Dispose();
        //        }
        //    }
        //    base.Dispose( disposing );
        //}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMain));
			this.lblMsg = new System.Windows.Forms.Label();
			this.mnuMainMenu = new System.Windows.Forms.ContextMenu();
			this.miStartEOD = new System.Windows.Forms.MenuItem();
			this.miPauseEOD = new System.Windows.Forms.MenuItem();
			this.miStopEOD = new System.Windows.Forms.MenuItem();
			this.miShowEODStatus = new System.Windows.Forms.MenuItem();
			this.miRunEft = new System.Windows.Forms.MenuItem();
			this.miAccounting = new System.Windows.Forms.MenuItem();
			this.mnPolProcess = new System.Windows.Forms.MenuItem();
			this.miAbout = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.SuspendLayout();
			// 
			// lblMsg
			// 
			this.lblMsg.Location = new System.Drawing.Point(16, 232);
			this.lblMsg.Name = "lblMsg";
			this.lblMsg.Size = new System.Drawing.Size(248, 16);
			this.lblMsg.TabIndex = 1;
			// 
			// mnuMainMenu
			// 
			this.mnuMainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.miStartEOD,
																						this.miPauseEOD,
																						this.miStopEOD,
																						this.miShowEODStatus,
																						this.miRunEft,
																						this.miAccounting,
																						this.mnPolProcess,
																						this.miAbout,
																						this.menuItem1});
			// 
			// miStartEOD
			// 
			this.miStartEOD.Index = 0;
			this.miStartEOD.Text = "Start Service";
			this.miStartEOD.Click += new System.EventHandler(this.miStartEOD_Click);
			// 
			// miPauseEOD
			// 
			this.miPauseEOD.Index = 1;
			this.miPauseEOD.Text = "Pause EOD Service";
			this.miPauseEOD.Click += new System.EventHandler(this.miPauseEOD_Click);
			// 
			// miStopEOD
			// 
			this.miStopEOD.Index = 2;
			this.miStopEOD.Text = "Stop EOD Service";
			this.miStopEOD.Click += new System.EventHandler(this.miStopEOD_Click);
			// 
			// miShowEODStatus
			// 
			this.miShowEODStatus.Index = 3;
			this.miShowEODStatus.Text = "Show Process Status";
			this.miShowEODStatus.Click += new System.EventHandler(this.miShowEODStatus_Click);
			// 
			// miRunEft
			// 
			this.miRunEft.Index = 4;
			this.miRunEft.Text = "Run EFT Process";
			this.miRunEft.Click += new System.EventHandler(this.miRunEft_Click);
			// 
			// miAccounting
			// 
			this.miAccounting.Index = 5;
			this.miAccounting.Text = "Run Accounting Thread";
			this.miAccounting.Click += new System.EventHandler(this.miAccounting_Click);
			// 
			// mnPolProcess
			// 
			this.mnPolProcess.Index = 6;
			this.mnPolProcess.Text = "Run Policy Processing";
			this.mnPolProcess.Click += new System.EventHandler(this.mnPolProcess_Click);
			// 
			// miAbout
			// 
			this.miAbout.Index = 7;
			this.miAbout.Text = "About";
			this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 8;
			this.menuItem1.Text = "Exit";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// trayIcon
			// 
			this.trayIcon.ContextMenu = this.mnuMainMenu;
			this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
			this.trayIcon.Text = "EbixAdvantage Service Manager";
			this.trayIcon.Visible = true;
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(304, 197);
			this.Controls.Add(this.lblMsg);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmMain";
			this.Text = "EbixAdvantage End Of Day Service Manager";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.ResumeLayout(false);

		}
		#endregion
		
	

		private void frmMain_Load(object sender, System.EventArgs e)
		{
			EODController.Refresh();
			if(EODController.Status == ServiceControllerStatus.Running)
			{
				miStartEOD.Enabled=false;
				miStopEOD.Enabled = true;
				miPauseEOD.Enabled=true;
			}
			else if(EODController.Status == ServiceControllerStatus.Stopped)
			{
				miStartEOD.Enabled=true;
				miStopEOD.Enabled = false;
				miPauseEOD.Enabled=false;
			}
			else if(EODController.Status == ServiceControllerStatus.Paused)
			{
				miStartEOD.Enabled=true;
				miStopEOD.Enabled = false;
				miPauseEOD.Enabled=false;
			}
		}
		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated (e);
			this.Hide();
		}

		private void miPauseEOD_Click(object sender, System.EventArgs e)
		{
			EODController.ExecuteCommand((int)ProcessCommands.PauseEODProcess);
		}

		private void miStartEOD_Click(object sender, System.EventArgs e)
		{
		
			EODController.Refresh();
			if(EODController.Status == ServiceControllerStatus.Stopped)
			{
				EODController.Start();
			}
			else if(EODController.Status == ServiceControllerStatus.Paused )
			{
				EODController.Continue();
			}
			EODController.Refresh();
			miStartEOD.Enabled=false;
			miStopEOD.Enabled = true;
			miPauseEOD.Enabled=true;
		}

		private void miStopEOD_Click(object sender, System.EventArgs e)
		{
			EODController.Refresh();
			if(EODController.Status == ServiceControllerStatus.Running)
			{
				EODController.Stop();
			}
			miStartEOD.Enabled=true;
			miStopEOD.Enabled = false;
			miPauseEOD.Enabled=false;
			EODController.Refresh();
		}

		private void miShowEODStatus_Click(object sender, System.EventArgs e)
		{
			//MessageBox("EOD Service is " + EODController.Status.ToString());
		}

		private void miAbout_Click(object sender, System.EventArgs e)
		{
			Form objAbt = new frmAbout();
			objAbt.Show();
		}

		
		private void miRunEft_Click(object sender, System.EventArgs e)
		{
			try
			{
				int command = (int)ProcessCommands.RunEFT;
				EODController.ExecuteCommand(command );
			}
			catch(Exception ex)
			{
				throw (ex);
			}
		}

		private void miAccounting_Click(object sender, System.EventArgs e)
		{
			try
			{
				EODController.ExecuteCommand((int)ProcessCommands.RunAccounting );
			}
			catch(Exception ex)
			{
				throw (ex);
			}
		}

		private void mnPolProcess_Click(object sender, System.EventArgs e)
		{
			try
			{
				EODController.ExecuteCommand((int)ProcessCommands.RunPolProcess);
			}
			catch(Exception ex)
			{
				throw(ex);
			}
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			this.Dispose();
		}

	}
}
