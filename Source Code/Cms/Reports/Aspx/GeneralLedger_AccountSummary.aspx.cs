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

namespace Reports.Aspx
{
	public class GeneralLedger_AccountSummary : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.Label lblExpirationStartDate;
		protected System.Web.UI.WebControls.TextBox txtExpirationStartDate;
		protected System.Web.UI.WebControls.HyperLink hlkExpirationStartDate;
		protected System.Web.UI.WebControls.Label lblExpirationEndDate;
		protected System.Web.UI.WebControls.TextBox txtExpirationEndDate;
		protected System.Web.UI.WebControls.HyperLink hlkExpirationEndDate;
		protected System.Web.UI.WebControls.Label lblAgent;		
		protected System.Web.UI.WebControls.Label lblLOB;
		protected System.Web.UI.WebControls.ListBox lstLOB;
		protected System.Web.UI.WebControls.RadioButton rdAddress1;
		protected System.Web.UI.WebControls.RadioButton rdAddress2;
		protected System.Web.UI.WebControls.RegularExpressionValidator revExpirationStartDate;
		protected System.Web.UI.WebControls.CompareValidator cmpExpirationDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revExpirationEndDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEffectiveEndDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_NAME;
		protected System.Web.UI.HtmlControls.HtmlImage imgAGENCY_NAME;
		protected System.Web.UI.WebControls.Label lblState;
		protected System.Web.UI.WebControls.ListBox lstStateName;
		protected System.Web.UI.WebControls.Label lblAccount;
		protected System.Web.UI.WebControls.ListBox lstAccount;
		protected System.Web.UI.WebControls.Label lblTransaction;
		protected System.Web.UI.WebControls.ListBox lstTransaction;
		protected System.Web.UI.WebControls.Label lblFromRange;
		protected System.Web.UI.WebControls.TextBox txtFromRange;
		protected System.Web.UI.WebControls.Label lblToRange;
		protected System.Web.UI.WebControls.TextBox txtToRange;
		protected System.Web.UI.WebControls.CustomValidator csvAMOUNTto;
		protected System.Web.UI.WebControls.CustomValidator csvAMOUNT;
		protected System.Web.UI.WebControls.CheckBox CheckBox1;
		protected System.Web.UI.WebControls.Label lblPolicynumber;
		protected System.Web.UI.WebControls.TextBox txtPolicyNumber;
		protected System.Web.UI.WebControls.DropDownList cmbMonth;
		protected System.Web.UI.WebControls.TextBox txtyear;
		protected System.Web.UI.WebControls.CheckBox Checkbox2;
		protected System.Web.UI.WebControls.Label lblPolicies;
		protected System.Web.UI.WebControls.ListBox lstPolicies;
		protected System.Web.UI.WebControls.CheckBox Checkbox3;
		protected System.Web.UI.WebControls.Label lblVendor;
		protected System.Web.UI.WebControls.ListBox lstVendorList;
		protected System.Web.UI.WebControls.Label capPOLICY_ID;
		protected System.Web.UI.WebControls.TextBox txtPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.WebControls.CustomValidator csvYear;
		protected System.Web.UI.WebControls.RegularExpressionValidator revYear;
		protected System.Web.UI.WebControls.RangeValidator rngYEAR;
		protected System.Web.UI.WebControls.Label lblAddress;
		protected Cms.CmsWeb.Controls.CmsButton btnReport;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="331_0";

			btnReport.CmsButtonClass                 =   Cms.CmsWeb.Controls.CmsButtonType.Read; 
			btnReport.PermissionString				=	gstrSecurityXML;	

			btnReport.Attributes.Add("onClick","ShowReport();return false;");

			hlkExpirationStartDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtExpirationStartDate,document.forms[0].txtExpirationStartDate)");			
			hlkExpirationEndDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtExpirationEndDate,document.forms[0].txtExpirationEndDate)");			
			revExpirationStartDate.ValidationExpression = aRegExpDate;
			revExpirationEndDate.ValidationExpression = aRegExpDate;	
			csvAMOUNT.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"163");
			csvAMOUNTto.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"163");
			CheckBox1.Attributes.Add("onclick","javascript:ShowContact();");

			revYear.ValidationExpression = aRegExpInteger;
			revYear.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"163");

			rngYEAR.MaximumValue = (DateTime.Now.Year+1).ToString();
			rngYEAR.MinimumValue = aAppMinYear  ;
			rngYEAR.ErrorMessage =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"673");

			Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text , DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
			string strAgencyID ="",strQuery="";
			strAgencyID = hidAGENCY_ID.Value;
			if(strAgencyID != null && strAgencyID!="")
			{
				this.lstPolicies.Items.Clear();	
				//strQuery = " Select (isnull(Policy_Number,'')) as PolicyNumber, (isnull(Policy_Number,'')) as ID  FROM POL_CUSTOMER_POLICY_LIST  where Agency_id = '" + strAgencyID + "' order by PolicyNumber";
				strQuery = "Select (isnull(Policy_Number,'')) as PolicyNumber,Customer_id,Policy_Id,policy_version_id,convert(varchar,Customer_id) + '-' + convert(varchar,Policy_Id) + '-' + convert(varchar,policy_version_id) as ID  , agency_id FROM POL_CUSTOMER_POLICY_LIST where Agency_id = '" + strAgencyID + "' order by PolicyNumber";
				DataSet ds = objDataWrapper.ExecuteDataSet(strQuery);
				//if (ds.Tables[0].Rows.Count > 0)
				//{
				lstPolicies.DataSource = ds.Tables[0];					
				lstPolicies.DataTextField = "PolicyNumber";
				lstPolicies.DataValueField = "ID";
				lstPolicies.DataBind();
				this.lstPolicies.Items.Insert(0,"All");
				this.lstPolicies.SelectedIndex =0;
				//}
			}

			if(!IsPostBack)
			{
				try
				{				
					DataSet ds1 = objDataWrapper.ExecuteDataSet("select distinct (isnull(ACC_DISP_NUMBER,'')) as NAME ,Account_id as ID from act_gl_accounts order by Name");
					lstAccount.DataSource = ds1.Tables[0];					
					lstAccount.DataTextField = "NAME";
					lstAccount.DataValueField = "ID";
					lstAccount.DataBind();
					this.lstAccount.Items.Insert(0,"All");
					this.lstAccount.SelectedIndex =0;	

					DataSet ds2 = objDataWrapper.ExecuteDataSet("select (isnull(STATE_NAME,'')) as StateName ,STATE_ID as ID from MNT_COUNTRY_STATE_LIST where IS_ACTIVE ='Y' order by StateName");
					lstStateName.DataSource = ds2.Tables[0];					
					lstStateName.DataTextField = "StateName";
					lstStateName.DataValueField = "ID";
					lstStateName.DataBind();
					this.lstStateName.Items.Insert(0,"All");
					this.lstStateName.SelectedIndex =0;

					DataSet ds3 = objDataWrapper.ExecuteDataSet("select (isnull(LOB_DESC,'') + ' ') as LOB ,LOB_ID as ID from MNT_LOB_MASTER order by LOB");
					lstLOB.DataSource = ds3.Tables[0];					
					lstLOB.DataTextField = "LOB";
					lstLOB.DataValueField = "ID";
					lstLOB.DataBind();
					this.lstLOB.Items.Insert(0,"All");
					this.lstLOB.SelectedIndex =0;

					DataSet ds4 = objDataWrapper.ExecuteDataSet("select distinct (isnull(display_description,'')) as NAME ,tran_code as ID from act_transaction_codes order by Name");
					lstTransaction.DataSource = ds4.Tables[0];					
					lstTransaction.DataTextField = "NAME";
					lstTransaction.DataValueField = "ID";
					lstTransaction.DataBind();
					this.lstTransaction.Items.Insert(0,"All");
					this.lstTransaction.SelectedIndex =0;
	
					DataSet ds5 = objDataWrapper.ExecuteDataSet("Select isnull(vendor_fname,'') + ' ' + isnull(vendor_lname,'')  as NAME ,VENDOR_ID as ID FROM mnt_vendor_list order by Name");
					lstVendorList.DataSource = ds5.Tables[0];					
					lstVendorList.DataTextField = "NAME";
					lstVendorList.DataValueField = "ID";
					lstVendorList.DataBind();
					this.lstVendorList.Items.Insert(0,"All");
					this.lstVendorList.SelectedIndex =0;	
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
			InitializeComponent();
			base.OnInit(e);
		}
				
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
