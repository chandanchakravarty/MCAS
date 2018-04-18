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
using Cms.DataLayer;
using Cms.CmsWeb;
using System.Text ; 
namespace Reports.Aspx
{
	public class MonthlyCheck : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label lblExpirationStartDate;
		protected System.Web.UI.WebControls.TextBox txtExpirationStartDate;
		protected System.Web.UI.WebControls.HyperLink hlkExpirationStartDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revExpirationStartDate;
		protected System.Web.UI.WebControls.CompareValidator cmpExpirationDate;
		protected System.Web.UI.WebControls.Label lblExpirationEndDate;
		protected System.Web.UI.WebControls.TextBox txtExpirationEndDate;
		protected System.Web.UI.WebControls.HyperLink hlkExpirationEndDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revExpirationEndDate;
		protected System.Web.UI.WebControls.Label lblFromRange;
		protected System.Web.UI.WebControls.TextBox txtFromRange;
		protected System.Web.UI.WebControls.CustomValidator csvAMOUNT;
		protected System.Web.UI.WebControls.Label lblToRange;
		protected System.Web.UI.WebControls.TextBox txtToRange;
		protected System.Web.UI.WebControls.CustomValidator csvAMOUNTto;
		protected System.Web.UI.WebControls.Label capPOLICY_ID;
		protected System.Web.UI.WebControls.TextBox txtPOLICY_ID;
		protected System.Web.UI.WebControls.Label lblAccount;
		protected System.Web.UI.WebControls.ListBox lstAccount;
		protected System.Web.UI.WebControls.Label lblCheckType;
		protected System.Web.UI.WebControls.ListBox lstCheckType;
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
		protected System.Web.UI.WebControls.Label lblPayee;
		protected System.Web.UI.WebControls.TextBox txtPayee;
		protected System.Web.UI.WebControls.Label lblCheckNumber;
		protected System.Web.UI.WebControls.TextBox txtCheckNo;
		protected System.Web.UI.WebControls.Label lblClaimNumber;
		protected System.Web.UI.WebControls.TextBox txtClaimNo;
		protected System.Web.UI.WebControls.Label lblFirstSort;
		protected System.Web.UI.WebControls.DropDownList cmbFirstSort;
		protected System.Web.UI.WebControls.Label lblVoidChecks;
		protected System.Web.UI.WebControls.CheckBox chkVoidChecks;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected Cms.CmsWeb.Controls.CmsButton btnReport;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			hlkExpirationStartDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtExpirationStartDate,document.forms[0].txtExpirationStartDate)");			
			hlkExpirationEndDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtExpirationEndDate,document.forms[0].txtExpirationEndDate)");			
			revExpirationStartDate.ValidationExpression = aRegExpDate;
			revExpirationEndDate.ValidationExpression = aRegExpDate;	
			csvAMOUNT.ErrorMessage	    =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"163");
			csvAMOUNTto.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"163");
			Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text , DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
			base.ScreenId="360";
			btnReport.CmsButtonClass                 =   Cms.CmsWeb.Controls.CmsButtonType.Read; 
			btnReport.PermissionString				=	gstrSecurityXML;	

			btnReport.Attributes.Add("onClick","ShowReport();return false;");
			if(!IsPostBack)
			{
				try
				{				
					//DataSet ds1 = objDataWrapper.ExecuteDataSet("select distinct (isnull(ACC_DISP_NUMBER,'')) as NAME ,Account_id as ID from act_gl_accounts order by Name");

					
					StringBuilder sbQuery = new StringBuilder();

					sbQuery.Append("SELECT t1.account_id,case when t1.acc_parent_id is null then t1.ACC_DESCRIPTION + ' : ' +  isnull(t1.ACC_DISP_NUMBER,'')  ");
					sbQuery.Append("else  isnull(t2.acc_description,'') + ' - ' + t1.ACC_DESCRIPTION  + ' : ' + isnull(t1.ACC_DISP_NUMBER,'') end as ACC_DESCRIPTION ");
					sbQuery.Append("FROM ACT_GL_ACCOUNTS t1 LEFT OUTER JOIN  ACT_GL_ACCOUNTS t2 ON t2.account_id = t1.acc_parent_id ");
					sbQuery.Append("WHERE t1.ACC_TYPE_ID=1 and t1.ACC_LEVEL_TYPE='AS' and t1.ACC_CASH_ACCOUNT='Y' ORDER BY t1.account_id  ");

					DataSet ds1 = objDataWrapper.ExecuteDataSet(sbQuery.ToString());

					lstAccount.DataSource = ds1.Tables[0];					
					lstAccount.DataTextField = "ACC_DESCRIPTION";
					lstAccount.DataValueField = "account_id";
					lstAccount.DataBind();
					this.lstAccount.Items.Insert(0,"All");
					this.lstAccount.SelectedIndex =0;	

					Cms.BusinessLayer.BlCommon.ClsCommon.BindLookupLST(ref lstCheckType,"CPAYTP");
					ListItem Li = new ListItem();
                    Li = lstCheckType.Items.FindByText("Claims Checks");
					this.lstCheckType.Items.Remove(Li);
					this.lstCheckType.Items.Insert(0,"All");
					this.lstCheckType.SelectedIndex =0;

/*					DataSet ds2 = objDataWrapper.ExecuteDataSet("select distinct LOOKUP_VALUE_DESC AS NAME,LOOKUP_UNIQUE_ID AS ID from mnt_lookup_values where LOOKUP_VALUE_DESC is not null");
					lstCheckType.DataSource = ds2.Tables[0];					
					lstCheckType.DataTextField = "Name";
					lstCheckType.DataValueField = "ID";
					lstCheckType.DataBind();
					this.lstCheckType.Items.Insert(0,"All");
					this.lstCheckType.SelectedIndex =0;
					*/
				}
				catch(Exception ex)
				{					
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				}
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
