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
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Account;
using Cms.CmsWeb;
using  Cms.BusinessLayer.BlCommon.Accounting;


namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for PostCustomerTransaction.
	/// </summary>
	public class PostCustomerTransaction : Cms.Account.AccountBase
	{
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected System.Web.UI.WebControls.Label capAPP_EFFECTIVE_DATE;
		protected System.Web.UI.WebControls.TextBox txtDATE;
		protected System.Web.UI.WebControls.HyperLink hlkCalandarDate;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDATE;
		protected System.Web.UI.WebControls.TextBox txtCustomerDetail;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCustomerDetail;
		protected System.Web.UI.HtmlControls.HtmlImage imgCustomer;
		protected Cms.CmsWeb.Controls.CmsButton btnExecute;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomer_ID;
		protected System.Web.UI.WebControls.RegularExpressionValidator revtxtDATE;		
		protected System.Web.UI.WebControls.Label lblMessage;
		
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			// Put user code to initialize the page here
			base.ScreenId = "302";
			try
			{
				// setting security 
				btnExecute.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Read;
				btnExecute.PermissionString = gstrSecurityXML;

				btnReset.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Read;
				btnReset.PermissionString = gstrSecurityXML;
				
				//Javascript Implementation for Calender					
				hlkCalandarDate.Attributes.Add("OnClick","fPopCalendar(document.Form1.txtDATE,document.Form1.txtDATE)"); 
				
				revtxtDATE.ValidationExpression	= aRegExpDate;
				revtxtDATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			}
			catch(Exception ex)
			{
				throw ex;
			}

		}
		
		/// <summary>
		/// 
		/// </summary>
		

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
			this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnExecute_Click(object sender, System.EventArgs e)
		{
		
			ClsAccount objClsAccount = new ClsAccount();
			try 
			{
				int intResult=0;
				if (txtDATE.Text.Trim() != "")
					intResult = objClsAccount.ExecPostCustomerTransaction(int.Parse(hidCustomer_ID.Value),ConvertToDate(txtDATE.Text.Trim().ToString()));
				else
					lblMessage.Text = "Please enter Date.";
				if(intResult>=0)
				{
					// lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("31");
					lblMessage.Text = "One or more transactions has been posted.";
				}
				
				lblMessage.Visible=true;
			}
			catch(Exception objExp)
			{
				lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("21") + "\n " + objExp.Message.ToString();
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
				lblMessage.Visible=true;
			}			
		}
	}
}
