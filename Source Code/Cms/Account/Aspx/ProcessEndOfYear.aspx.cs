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
using Cms.CmsWeb.Controls;


namespace Account
{
	/// <summary>
	/// Summary description for ProcessEndOfYear.
	/// </summary>
	public class ProcessEndOfYear : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblProcess;
		protected Cms.CmsWeb.Controls.CmsButton btnProcess;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFiscalID;
		public int year = 0;
		private void Page_Load(object sender, System.EventArgs e)
		{

			base.ScreenId="435";
			btnProcess.CmsButtonClass		=	CmsButtonType.Execute;
			btnProcess.PermissionString		=	gstrSecurityXML;
			//DisableButton(document.getElementById('btnCCSave'));
			btnProcess.Attributes.Add ("onClick","javascript:DisableButton(this);");
			// Put user code to initialize the page here
			SetFiscalDateCaption();

			
		}
		private void SetFiscalDateCaption()
		{
			year = 0;
			int fiscalID = 0;
			int is_EOY_Processed = 0;
			DateTime EOY_Date;
			try
			{
				Cms.BusinessLayer.BlAccount.ClsAccount objAct = new Cms.BusinessLayer.BlAccount.ClsAccount();
				objAct.GetFiscalDateEOY(DateTime.Now,out year,out fiscalID,out is_EOY_Processed,out EOY_Date);

				if(is_EOY_Processed!=0)
				{
					btnProcess.Visible = false;
					lblProcess.Visible = true;
					lblProcess.Text = "End Of Year for fiscal " + year + " has already been processed on " + EOY_Date.ToString();					

				}
				else
				{
					btnProcess.Text = "Process End Of Year for fiscal " + year.ToString();
				}


				//Set Fiscal ID
				hidFiscalID.Value = fiscalID.ToString();
				
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
			}
		}

		private void ProcessEndofYear()
		{
			Cms.BusinessLayer.BlAccount.ClsAccount objAct = new Cms.BusinessLayer.BlAccount.ClsAccount();
			int fiscal_ID = 0;
			int retVal = 0;
			try
			{				
				if(hidFiscalID.Value!="")
				{
					fiscal_ID = int.Parse(hidFiscalID.Value.ToString());
					retVal = objAct.ProcessEndofYear(fiscal_ID,int.Parse(GetUserId()),year);
					if(retVal>0)
					{
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
						lblMessage.Visible = true;
						btnProcess.Enabled = false;
					}
					else
					{
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
						lblMessage.Visible = true;
					}
				}
				else
				{
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
					lblMessage.Visible = true;

				}
				

			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
			}


		}


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnProcess_Click(object sender, System.EventArgs e)
		{
			ProcessEndofYear();			
		}
	}
}
