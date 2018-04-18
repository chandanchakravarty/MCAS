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
	public class PolicyExpirationList : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label lblCustomer;
		protected System.Web.UI.WebControls.ListBox lstCustomerName;
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.Label lblExpirationStartDate;
		protected System.Web.UI.WebControls.TextBox txtExpirationStartDate;
		protected System.Web.UI.WebControls.HyperLink hlkExpirationStartDate;
		protected System.Web.UI.WebControls.Label lblExpirationEndDate;
		protected System.Web.UI.WebControls.TextBox txtExpirationEndDate;
		protected System.Web.UI.WebControls.HyperLink hlkExpirationEndDate;
		protected System.Web.UI.WebControls.Label lblAgent;
		protected System.Web.UI.WebControls.Label lblUnderwriter;
		protected System.Web.UI.WebControls.ListBox lstUnderWriterName;
		protected System.Web.UI.WebControls.Label lblLOB;
		protected System.Web.UI.WebControls.ListBox lstLOB;
		protected System.Web.UI.WebControls.RadioButton rdAddress1;
		protected System.Web.UI.WebControls.RadioButton rdAddress2;
		protected System.Web.UI.WebControls.Label lblBillType;
		protected System.Web.UI.WebControls.ListBox lstBillType;
		protected System.Web.UI.WebControls.RegularExpressionValidator revExpirationStartDate;
		protected System.Web.UI.WebControls.CompareValidator cmpExpirationDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revExpirationEndDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEffectiveEndDate;
		protected System.Web.UI.WebControls.Label lblPolicyStaus;
		protected System.Web.UI.WebControls.ListBox lstPolicyStatus;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_NAME;
		protected System.Web.UI.HtmlControls.HtmlImage imgAGENCY_NAME;
		
		protected System.Web.UI.WebControls.Label lblOrderBy_2;
		protected System.Web.UI.WebControls.Label lblOrderBy_1;
		protected System.Web.UI.WebControls.ListBox lstOrderBy;
        protected System.Web.UI.WebControls.Button btnSel;
		protected System.Web.UI.WebControls.Button btnDeSel;


		protected System.Web.UI.WebControls.Label lblAddress;
		protected Cms.CmsWeb.Controls.CmsButton btnReport;
		protected string strAgencyID ="",strQuery="";
        Cms.DataLayer.DataWrapper objDataWrapper; 
        //new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text , DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);

		private void Page_Load(object sender, System.EventArgs e)
		{
			hlkExpirationStartDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtExpirationStartDate,document.forms[0].txtExpirationStartDate)");			
			hlkExpirationEndDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtExpirationEndDate,document.forms[0].txtExpirationEndDate)");			
			revExpirationStartDate.ValidationExpression = aRegExpDate;
			revExpirationEndDate.ValidationExpression = aRegExpDate;	
			btnSel.Attributes.Add("onClick","funcSetOrderBy();return false;");
			btnDeSel.Attributes.Add("onClick","funcRemoveOrderBy();return false;");
			base.ScreenId="328";
			
			btnReport.CmsButtonClass                 =   Cms.CmsWeb.Controls.CmsButtonType.Read; 
			btnReport.PermissionString				=	gstrSecurityXML;
            Response.Redirect("PolicyStatusList.aspx");
            return;

            objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
			btnReport.Attributes.Add("onClick","ShowReport();return false;");
			/* AGENCY VALUES
			 * Check the SystemID of the logged in user.
			 * If the user is not a Wolverine user then display records of that agency ONLY
			 * else the normal flow follows */
			string  strSystemID			 = GetSystemId();
			
			string  strCarrierSystemID = CarrierSystemID;
			if ( strSystemID.Trim().ToUpper() != strCarrierSystemID.Trim().ToUpper())
			{
				DataSet objDataSet =Cms.BusinessLayer.BlCommon.ClsAgency.GetAgencyIDAndNameFromCode(strSystemID);

				if (objDataSet.Tables[0].Rows.Count > 0 )
				{
					string strAgencyName = objDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString();
					strAgencyID = objDataSet.Tables[0].Rows[0]["AGENCY_ID"].ToString();
					hidAGENCY_ID.Value = strAgencyID;

					// Agency TextBox
					txtAGENCY_NAME.Text = strAgencyName;
					imgAGENCY_NAME.Visible=false;
					fillListBox(strAgencyID);
				}
			}

			// Customer ListBox
			strAgencyID = hidAGENCY_ID.Value;
			if(strAgencyID != null && strAgencyID!="")
			{
				fillListBox(strAgencyID);
			}

			if(!IsPostBack)
			{
				try
				{				
					/*Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text , DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
					DataSet ds = objDataWrapper.ExecuteDataSet("Select distinct (isnull(CUSTOMER_FIRST_NAME,'') + ' ' + isnull(CUSTOMER_MIDDLE_NAME,'') + ' '+ isnull(CUSTOMER_LAST_NAME,'')) as CUSTOMER_FIRST_NAME ,CUSTOMER_ID  FROM CLT_CUSTOMER_LIST order by customer_first_name");
					lstCustomerName.DataSource = ds.Tables[0];					
					lstCustomerName.DataTextField = "customer_first_name";
					lstCustomerName.DataValueField = "customer_id";
					lstCustomerName.DataBind();
					this.lstCustomerName.Items.Insert(0,"All");
					this.lstCustomerName.SelectedIndex =0;

					DataSet ds1 = objDataWrapper.ExecuteDataSet("select (isnull(AGENCY_DISPLAY_NAME,'') + ' ') as AgencyName ,Agency_id as ID from MNT_AGENCY_LIST order by AgencyName");
					lstAgentName.DataSource = ds1.Tables[0];					
					lstAgentName.DataTextField = "AgencyName";
					lstAgentName.DataValueField = "ID";
					lstAgentName.DataBind();
					this.lstAgentName.Items.Insert(0,"All");
					this.lstAgentName.SelectedIndex =0;*/

					DataSet ds2 = objDataWrapper.ExecuteDataSet("Select (isnull(User_Fname,'') + ' ' + isnull(User_Lname,'')) as UnderwriterName ,User_id as ID  FROM MNT_USER_LIST WHERE USER_TYPE_ID IN (SELECT USER_TYPE_ID FROM MNT_USER_TYPES WHERE USER_TYPE_CODE='UWT') order by UnderwriterName");
					lstUnderWriterName.DataSource = ds2.Tables[0];					
					lstUnderWriterName.DataTextField = "UnderwriterName";
					lstUnderWriterName.DataValueField = "ID";
					lstUnderWriterName.DataBind();
					this.lstUnderWriterName.Items.Insert(0,"All");
					this.lstUnderWriterName.SelectedIndex =0;

					DataSet ds3 = objDataWrapper.ExecuteDataSet("select (isnull(LOB_DESC,'') + ' ') as LOB ,LOB_ID as ID from MNT_LOB_MASTER order by LOB");
					lstLOB.DataSource = ds3.Tables[0];					
					lstLOB.DataTextField = "LOB";
					lstLOB.DataValueField = "ID";
					lstLOB.DataBind();
					this.lstLOB.Items.Insert(0,"All");
					this.lstLOB.SelectedIndex =0;

					DataSet ds4 = objDataWrapper.ExecuteDataSet("SELECT MLV.LOOKUP_VALUE_DESC as BillType , MLV.LOOKUP_VALUE_CODE as ID FROM MNT_LOOKUP_VALUES MLV INNER JOIN MNT_LOOKUP_TABLES MLT ON MLV.LOOKUP_ID = MLT.LOOKUP_ID WHERE MLT.LOOKUP_NAME = 'BLCODE' ORDER BY MLV.LOOKUP_VALUE_DESC ASC ");
					lstBillType.DataSource = ds4.Tables[0];					
					lstBillType.DataTextField = "BillType";
					lstBillType.DataValueField = "ID";
					lstBillType.DataBind();
					this.lstBillType.Items.Insert(0,"All");
					this.lstBillType.SelectedIndex =0;

					//Remove Negate Procee For Itrack Issue 6059 Note 1 .
					DataSet ds5 = objDataWrapper.ExecuteDataSet("SELECT POLICY_DESCRIPTION as POLICYSTATUS ,POLICY_STATUS_CODE as CODE FROM POL_POLICY_STATUS_MASTER where POLICY_DESCRIPTION not in ('Negate','Negate in progress')  ORDER BY POLICYSTATUS ");
					lstPolicyStatus.DataSource = ds5.Tables[0];					
					lstPolicyStatus.DataTextField = "POLICYSTATUS";
					lstPolicyStatus.DataValueField = "CODE";
					lstPolicyStatus.DataBind();
					this.lstPolicyStatus.Items.Insert(0,"All");
					this.lstPolicyStatus.SelectedIndex =0;									
				}
				catch(Exception ex)
				{					
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				}
			}	
		}

		private void fillListBox(string AgencyID)
		{
			
			this.lstCustomerName.Items.Clear();	
			strQuery = " Select distinct (isnull(CUSTOMER_FIRST_NAME,'') + ' ' + isnull(CUSTOMER_MIDDLE_NAME,'') + ' '+ isnull(CUSTOMER_LAST_NAME,'')) as CUSTOMER_FIRST_NAME ,CUSTOMER_ID  FROM CLT_CUSTOMER_LIST  where customer_agency_id = '" + strAgencyID + "' order by customer_first_name";
			DataSet ds = objDataWrapper.ExecuteDataSet(strQuery);
			lstCustomerName.DataSource = ds.Tables[0];					
			lstCustomerName.DataTextField = "customer_first_name";
			lstCustomerName.DataValueField = "customer_id";
			lstCustomerName.DataBind();
			this.lstCustomerName.Items.Insert(0,"All");
			this.lstCustomerName.SelectedIndex =0;	

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
