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
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.BusinessLayer.BlCommon;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for AgencyCommissionDistribution.
	/// </summary>
	public class AgencyCommissionDistribution : Cms.Account.AccountBase
	{
		protected Cms.CmsWeb.Controls.CmsButton btnApplyAmount;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected System.Web.UI.WebControls.DropDownList cmbACCOUNT_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvACCOUNT_ID;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblBalAmt;
		protected System.Web.UI.WebControls.Label lblDiffAmt;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLIENT_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnClose;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAccountID;
		protected System.Web.UI.WebControls.Label lblUncolPremAB;
		protected System.Web.UI.WebControls.Label lblCommPayAB;
		protected System.Web.UI.WebControls.Label lblCommPayDB;
		protected System.Web.UI.WebControls.TextBox txtDESCRIPTION;
		protected System.Web.UI.HtmlControls.HtmlTableRow trDESC;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAdjAcc;
		protected int intcheck_id = 0;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			base.ScreenId = "375";
			btnApplyAmount.CmsButtonClass	= CmsButtonType.Read;
			btnApplyAmount.PermissionString	= gstrSecurityXML;
			btnClose.CmsButtonClass			=	CmsButtonType.Execute;
			btnClose.PermissionString		=	gstrSecurityXML;
			btnClose.Attributes.Add("onClick","javascript:window.close();");
			GetQueryString();
			
			
			if(! IsPostBack)
			{
				ClsGlAccounts.GetAllGLAccountsInDropDown(cmbACCOUNT_ID);
				rfvACCOUNT_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("340");
				GetAgencyDistributionInfo(intcheck_id);
				cmbACCOUNT_ID.SelectedIndex=cmbACCOUNT_ID.Items.IndexOf(cmbACCOUNT_ID.Items.FindByValue(hidAccountID.Value.ToString()));
			}
			
		
		}

		/// <summary>
		/// Retreives the query string from url
		/// </summary>
		private void GetQueryString()
		{
			intcheck_id = int.Parse(Request.QueryString["CHECK_ID"].ToString());
		}
		private void GetAgencyDistributionInfo(int check_id)
		{
			ClsChecks objClsCheck = new ClsChecks();
			DataSet ds = objClsCheck.GetAgencyCommDistributionInfo(check_id);
			lblBalAmt.Text = ds.Tables[0].Rows[0]["AMT_AGENCY_BALANCE"].ToString();
			lblDiffAmt.Text = ds.Tables[0].Rows[0]["DIFFERENCE_AMOUNT"].ToString();
			lblUncolPremAB.Text = ds.Tables[0].Rows[0]["AMT_UNCOLLECTED_PREMIUM_AB"].ToString();
			lblCommPayAB.Text = ds.Tables[0].Rows[0]["AMT_COMMISSION_PAYABLE_AB"].ToString();
			lblCommPayDB.Text = ds.Tables[0].Rows[0]["AMT_COMMISSION_PAYABLE_DB"].ToString();
			txtDESCRIPTION.Text  = ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();
			hidAccountID.Value = ds.Tables[0].Rows[0]["ACCOUNT_FOR_ADJUSTMENT"].ToString();
			// display Description textbox / Adj amount combo only when there is some difference amount
			if(lblDiffAmt.Text.Equals("") || lblDiffAmt.Text.Equals("0"))
			{
				trDESC.Visible = false;
				trAdjAcc.Visible = false;
				btnApplyAmount.Visible = false;
			}
			else
			{
				trDESC.Visible = true;
				trAdjAcc.Visible = true;
				btnApplyAmount.Visible = true;
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
			this.btnApplyAmount.Click += new System.EventHandler(this.btnApplyAmount_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnApplyAmount_Click(object sender, System.EventArgs e)
		{
			ClsChecks objClsCheck = new ClsChecks();
			string strDesc = "";
			strDesc = txtDESCRIPTION.Text.Trim();
			try 
			{
				int intResult=0;
				intResult = objClsCheck.ExecAgencyDistribution(intcheck_id,int.Parse(cmbACCOUNT_ID.SelectedValue.ToString().Trim()),strDesc);
				if(intResult>=0)
				{
					lblMessage.Text = "Difference amount has been adjusted successfully against the selected account.";
				}
				else
				{
					lblMessage.Text = "Unable to adjust the difference Amount.";
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
