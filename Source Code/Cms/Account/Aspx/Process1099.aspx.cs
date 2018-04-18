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
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for Process1099.
	/// </summary>
	public class Process1099 :  Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblProcess;
		protected Cms.CmsWeb.Controls.CmsButton btnProcess;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidYear;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFiscalID;
		

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			base.ScreenId="426";
			btnProcess.CmsButtonClass		=	CmsButtonType.Execute;
			btnProcess.PermissionString		=	gstrSecurityXML;

			//string caption = "Process 1099-Year " + DateTime.Now.Year;	
			//btnProcess.Text = caption;
			//btnProcess.Enabled = false; //temp
			SetFiscalDateCaption();

		}

		private void SetFiscalDateCaption()
		{
			int year = 0;
			int fiscalID = 0;
			int is_Ten99_Processed = 0;
			DateTime ten99_Date;
			try
			{
				Cms.BusinessLayer.BlAccount.ClsAccount objAct = new Cms.BusinessLayer.BlAccount.ClsAccount();
				objAct.GetFiscalDateTen99(DateTime.Now,out year,out fiscalID,out is_Ten99_Processed,out ten99_Date);

				if(is_Ten99_Processed!=0)
				{
					btnProcess.Visible = false;
					lblProcess.Visible = true;
					lblProcess.Text = "1099 for fiscal " + year + " has already been processed on " + ten99_Date.ToString();					

				}
				else
				{
					btnProcess.Text = "Process 1099 for fiscal " + year.ToString();
				}


				//Set Year and Fiscal ID
				hidYear.Value = year.ToString();
				hidFiscalID.Value = fiscalID.ToString();
				
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
			int retVal = 0;
			int ten99Year = 0;
			int fiscal_ID = 0;
			if(hidYear.Value!="")
                ten99Year = int.Parse(hidYear.Value.ToString());

			if(hidFiscalID.Value!="")
				fiscal_ID = int.Parse(hidFiscalID.Value);

			try
			{
                retVal =  Cms.BusinessLayer.BlAccount.ClsAccount.Process1099(ten99Year,int.Parse(GetUserId()),fiscal_ID);       
				if(retVal > 0)
				{
					lblMessage.Text = "1099 Processed Successfully.";
					lblMessage.Visible = true;
					btnProcess.Enabled = false;

				}
				else
				{
					lblMessage.Text = "1099 not Processed.";
					lblMessage.Visible = true;
				}


			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
			}

					
		}
	}
}
