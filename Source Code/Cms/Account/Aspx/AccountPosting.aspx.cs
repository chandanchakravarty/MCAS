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

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for AccountPosting.
	/// </summary>
	public class AccountPosting : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.TextBox txtFrom;
		protected System.Web.UI.WebControls.TextBox txtTo;
		protected System.Web.UI.WebControls.TextBox txtDateFrom;
		protected System.Web.UI.WebControls.TextBox txtDateTo;
		protected System.Web.UI.WebControls.TextBox txtAccount;
        protected System.Web.UI.WebControls.DropDownList cmbUpdatedFrom;
		protected System.Web.UI.WebControls.DropDownList cmbLob;
		protected System.Web.UI.WebControls.DropDownList cmbState;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDateFrom;
		protected System.Web.UI.WebControls.RegularExpressionValidator revToDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFrom;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTo;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAccount;
		protected System.Web.UI.WebControls.HyperLink hlkDateFrom;
		protected System.Web.UI.WebControls.CustomValidator csvDateFrom;
		protected System.Web.UI.WebControls.HyperLink hlkDateTo;
		protected System.Web.UI.WebControls.CustomValidator csvDateTo;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAccount;
		protected Cms.CmsWeb.Controls.CmsButton btnReport;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			//btnReport.Attributes.Add("onclick","javascript:AccountDetails();");
			hlkDateFrom.Attributes.Add("OnClick","fPopCalendar(document.AccountPosting.txtDateFrom,document.AccountPosting.txtDateFrom)"); //Javascript Implementation for Calender				
			hlkDateTo.Attributes.Add("OnClick","fPopCalendar(document.AccountPosting.txtDateTo,document.AccountPosting.txtDateTo)"); //Javascript Implementation for Calender				
			
			base.ScreenId = "200";

			btnReport.CmsButtonClass	= CmsButtonType.Execute;
			btnReport.PermissionString	= gstrSecurityXML;

			if(!Page.IsPostBack)
			{
				FillCombo();
				SetValidators();
			}
			//FillCombo();
			
		}

		private void SetValidators()
		{
			revDateFrom.ValidationExpression = aRegExpDate;
			revToDate.ValidationExpression	= aRegExpDate;
			revFrom.ValidationExpression = aRegExpInteger;
			revTo.ValidationExpression = aRegExpInteger;
			revAccount.ValidationExpression = aRegExpDouble;

			rfvAccount.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("894");
			revTo.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
			revFrom.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
			revAccount.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
			revDateFrom.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			revToDate.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			
			csvDateFrom.ErrorMessage				= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("448");
			csvDateTo.ErrorMessage				= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("446");
			//csvCheckDate.ErrorMessage				= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("447");
		}
		private void FillCombo()
		{
			DataTable dtLOBs = Cms.CmsWeb.ClsFetcher.LOBs;
			cmbLob.DataSource			= dtLOBs;
			cmbLob.DataTextField		= "LOB_DESC";
			cmbLob.DataValueField		= "LOB_ID";
			cmbLob.DataBind();
			cmbLob.Items.Insert(0,new ListItem("",""));
			cmbLob.SelectedIndex=0;

			#region "Loading singleton"
			DataTable dt;
			
			dt = Cms.CmsWeb.ClsFetcher.State;
			cmbState.DataSource		= dt;
			cmbState.DataTextField	= "State_Name";
			cmbState.DataValueField	= "State_Id";
			cmbState.DataBind();
			cmbState.Items.Insert(0,"");
			cmbState.SelectedIndex=0;
			#endregion//Loading singleton
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
			this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnReport_Click(object sender, System.EventArgs e)
		{
			string strValue = "<script>"
				 + "window.open('AccountDetails.aspx?frmSource=" + txtFrom.Text + "&toSource=" + txtTo.Text + "&frmDate=" + txtDateFrom.Text + "&toDate=" + txtDateTo.Text + "&acc=" + txtAccount.Text + "&updFrom=" + cmbUpdatedFrom.SelectedItem.Value+ "&lob=" + cmbLob.SelectedValue + "&state=" + cmbState.SelectedValue + "');</script>";

			if(!ClientScript.IsClientScriptBlockRegistered("AccountDetails"))
				ClientScript.RegisterClientScriptBlock(this.GetType(),"AccountDetails",strValue);
				
		}
	}
}
