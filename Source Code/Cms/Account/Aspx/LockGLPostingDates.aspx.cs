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
using System.Reflection; //sneha
using System.Resources;//sneha
using Cms.BusinessLayer.BlCommon.Accounting;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for LockGLPostingDates.
	/// </summary>
	public class LockGLPostingDates : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblFiscalPeriod;
        protected System.Web.UI.WebControls.Label capGENERAL_LEDGER; //sneha
        protected System.Web.UI.WebControls.Label capHEADMSG;//sneha
        protected System.Web.UI.WebControls.Label capFISCAL_PERIOD;//sneha
        protected System.Web.UI.WebControls.Label capLOCK_DATE;//sneha
		protected System.Web.UI.WebControls.TextBox txtTotal;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.WebControls.TextBox txtLOCK_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkLOCK_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOCK_DATE;
		protected System.Web.UI.WebControls.CustomValidator cstLOCK_DATE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidBeginDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEndDate;
//		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		public string FiscalPeriod="";
		public DateTime beginDate = DateTime.MinValue;
		public DateTime endDate   = DateTime.MinValue;
        System.Resources.ResourceManager objResourceMgr; //sneha
		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
        /// 
        
		private void SetErrorMessages()
		{
			rfvLOCK_DATE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			cstLOCK_DATE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
		}
		#endregion

        
        private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "125_2";
			//btnReset.Attributes.Add ("onclick","javascript:return ResetForm('ACT_CURRENT_DEPOSIT_LINE_ITEMS');");
            objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.LockGLPostingDates", System.Reflection.Assembly.GetExecutingAssembly());
			btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString = gstrSecurityXML;
			
			SetErrorMessages();
            
			//btnReset.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
			//btnReset.PermissionString = gstrSecurityXML;
			hlkLOCK_DATE.Attributes.Add("OnClick","fPopCalendar(document.frmLockDates.txtLOCK_DATE,document.frmLockDates.txtLOCK_DATE)");
			if (! Page.IsPostBack)
			{
				DataSet objDataSet = ClsGeneralLedger.GetFiscalPeriod(int.Parse(Session["FISCAL_ID"].ToString()));
                if (objDataSet.Tables[0].Rows[0]["FISCAL_BEGIN_DATE"].ToString() != "" && objDataSet.Tables[0].Rows[0]["FISCAL_END_DATE"].ToString() != "")
                    lblFiscalPeriod.Text =ConvertToDateCulture(Convert.ToDateTime(objDataSet.Tables[0].Rows[0]["FISCAL_BEGIN_DATE"].ToString()))+" - "+ConvertToDateCulture(Convert.ToDateTime(objDataSet.Tables[0].Rows[0]["FISCAL_END_DATE"].ToString()));

                if (objDataSet.Tables[0].Rows[0]["FISCAL_BEGIN_DATE"].ToString() != "")
                    hidBeginDate.Value =ConvertToDateCulture(Convert.ToDateTime( objDataSet.Tables[0].Rows[0]["FISCAL_BEGIN_DATE"].ToString()));
                
                if(objDataSet.Tables[0].Rows[0]["FISCAL_END_DATE"].ToString()!="")
                    hidEndDate.Value = ConvertToDateCulture(Convert.ToDateTime(objDataSet.Tables[0].Rows[0]["FISCAL_END_DATE"].ToString()));
                
                if(objDataSet.Tables[0].Rows[0]["POSTING_LOCK_DATE"].ToString()!="")
                    txtLOCK_DATE.Text = ConvertToDateCulture(Convert.ToDateTime(objDataSet.Tables[0].Rows[0]["POSTING_LOCK_DATE"].ToString()));

                SetCaptions(); //sneha
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
        private void SetCaptions()
        {
            capGENERAL_LEDGER.Text = objResourceMgr.GetString("capGENERAL_LEDGER"); //sneha
            capHEADMSG.Text = objResourceMgr.GetString("capHEADMSG"); //sneha
            capFISCAL_PERIOD.Text = objResourceMgr.GetString("capFISCAL_PERIOD"); //sneha
            capLOCK_DATE.Text = objResourceMgr.GetString("capLOCK_DATE"); //sneha
        }
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			int intRetVal=1;
			try
			{
				if(Session["FISCAL_ID"]!=null && Session["FISCAL_ID"].ToString().Length>0)
					intRetVal=ClsGeneralLedger.SavePostingLockDate(int.Parse(Session["FISCAL_ID"].ToString()),DateTime.Parse(txtLOCK_DATE.Text));
				else
					intRetVal=-1;
				if(intRetVal>0)
				{
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
				}
				else
				{
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
				}
				lblMessage.Visible = true;	
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				ExceptionManager.Publish(ex);
			}
			
		}
	}
}
