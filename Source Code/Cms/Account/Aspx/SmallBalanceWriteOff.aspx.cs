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
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer;
using Cms.BusinessLayer.BlAccount;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for SmallBalanceWriteOff.
	/// </summary>
	public class SMALL_BAL_WRITEOFF :  Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.HyperLink hlkCalandarDate;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.TextBox txtDATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revtxtDATE;
		protected Cms.CmsWeb.Controls.CmsButton btnWriteOff;
		protected System.Web.UI.WebControls.Label capAPP_EFFECTIVE_DATE;
		
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			hlkCalandarDate.Attributes.Add("OnClick","fPopCalendar(document.SMALL_BAL_WRITEOFF.txtDATE,document.SMALL_BAL_WRITEOFF.txtDATE)"); //Javascript Implementation for Calender				
			btnWriteOff.Attributes.Add("Onclick","javascript:HideShowTransactionInProgress();");
			base.ScreenId = "375";
			btnWriteOff.CmsButtonClass	= CmsButtonType.Read;
			btnWriteOff.PermissionString	= gstrSecurityXML;

						 
			if(!Page.IsPostBack)
			{
				txtDATE.Text = DateTime.Now.ToString("MM/dd/yyyy");
				SetErrorMessages();
			}
			
		}
		#region SET VALIDATOR MESSAGES
		private void SetErrorMessages()
		{
			rfvDATE.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"95");
			revtxtDATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("502","22");
			revtxtDATE.ValidationExpression =aRegExpDate;
			
		}

		#endregion


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
			this.btnWriteOff.Click += new System.EventHandler(this.btnWriteOff_Click);
			this.ID = "SMALL_BAL_WRITEOFF";
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnWriteOff_Click(object sender, System.EventArgs e)
		{
			ClsAccount objClsAccount = new ClsAccount();
			
			try 
			{
				int intResult=0;
				if (txtDATE.Text.Trim() != "")
					intResult = objClsAccount.ExecSmallBalanceWriteOff(ConvertToDate(txtDATE.Text.Trim().ToString()),int.Parse(GetUserId()));
				else
					lblMessage.Text = "Please enter Date.";
				if(intResult>=0)
				{
					lblMessage.Text = "Small balance writes off successfully.";
				}
				else
				{
					lblMessage.Text = "Small balance writes off not succeeded.";
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
