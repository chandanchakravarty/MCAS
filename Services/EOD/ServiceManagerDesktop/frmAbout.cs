using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ServiceManagerDesktop
{
	/// <summary>
	/// Summary description for frmAbout.
	/// </summary>
	public class frmAbout : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblMain;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label1;
		private System.Timers.Timer timUnload;
		private System.ComponentModel.IContainer components;

		public frmAbout()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblMain = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.timUnload = new System.Timers.Timer();
			((System.ComponentModel.ISupportInitialize)(this.timUnload)).BeginInit();
			this.SuspendLayout();
			// 
			// lblMain
			// 
			this.lblMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblMain.Location = new System.Drawing.Point(16, 24);
			this.lblMain.Name = "lblMain";
			this.lblMain.Size = new System.Drawing.Size(248, 24);
			this.lblMain.TabIndex = 0;
			this.lblMain.Text = "EbixAdvantage EOD Service Manager";
			// 
			// linkLabel1
			// 
			this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.linkLabel1.Location = new System.Drawing.Point(136, 101);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(72, 16);
			this.linkLabel1.TabIndex = 1;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = " Ebix Inc";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(48, 104);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Powered By";
			// 
			// timUnload
			// 
			this.timUnload.Enabled = true;
			this.timUnload.Interval = 2500;
			this.timUnload.SynchronizingObject = this;
			this.timUnload.Elapsed += new System.Timers.ElapsedEventHandler(this.timUnload_Elapsed);
			// 
			// frmAbout
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.AliceBlue;
			this.ClientSize = new System.Drawing.Size(292, 160);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.lblMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.KeyPreview = true;
			this.Name = "frmAbout";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmAbout";
			this.Load += new System.EventHandler(this.frmAbout_Load);
			((System.ComponentModel.ISupportInitialize)(this.timUnload)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void timUnload_Tick(object sender, System.EventArgs e)
		{
			this.Dispose();
		}

		private void frmAbout_Load(object sender, System.EventArgs e)
		{
		
		}

		private void timUnload_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			this.Dispose();
		}
	}
}
