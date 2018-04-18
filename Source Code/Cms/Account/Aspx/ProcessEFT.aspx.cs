#region Namespaces
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.BusinessLayer.BlAccount;
using Cms.Model.Account;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
#endregion

namespace Cms.Account.Aspx
{

	public class ProcessEFT : Cms.Account.AccountBase//System.Web.UI.Page//
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.Controls.CmsButton btnLaunchEFT;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId= "396";
			btnLaunchEFT.CmsButtonClass   = Cms.CmsWeb.Controls.CmsButtonType.Execute;
			btnLaunchEFT.PermissionString = gstrSecurityXML;
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			
			InitializeComponent();
			base.OnInit(e);
		}
		
		private void InitializeComponent()
		{    
			this.btnLaunchEFT.Click += new System.EventHandler(this.btnLaunchEFT_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnLaunchEFT_Click(object sender, System.EventArgs e)
		{
			try
			{
				ClsEFT objBL = new ClsEFT(int.Parse(GetUserId()),GetSystemId());
				objBL.LocalDirectoty = @"\\ravinder\cms-res\NACHA Files\";
				objBL.HostName ="209.94.22.241";
				objBL.UserName="ftpguest";
				objBL.Password="ftpguest";
				objBL.FTPDirectoty = "EBXDV25/NACHA-Ftp";
				objBL.Start();
			}
			catch(Exception Ex)
			{
				lblMessage.Text = Ex.Message;
				lblMessage.Visible=true;
				return;
			}
		}
	}
}
