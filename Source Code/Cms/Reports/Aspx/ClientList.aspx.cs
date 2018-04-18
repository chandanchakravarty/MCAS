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
	public class ClientList : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.Label lblAgent;
		protected System.Web.UI.WebControls.Label lblAddress;
		protected System.Web.UI.WebControls.Label lblNameFormat;
		protected System.Web.UI.WebControls.DropDownList lstNameFormat;
		protected System.Web.UI.WebControls.Label Address;
		protected System.Web.UI.WebControls.Label lblState;
		protected System.Web.UI.WebControls.Label lblZip;
		protected System.Web.UI.WebControls.TextBox txtZip;
		protected System.Web.UI.WebControls.Label lblCustomer;
		protected System.Web.UI.WebControls.ListBox lstCustomerName;
		protected System.Web.UI.WebControls.ListBox lstStateName;
		protected System.Web.UI.WebControls.RadioButton rdClientType3;
		protected System.Web.UI.WebControls.RadioButton rdAddress2;
		protected System.Web.UI.WebControls.Label lblClientsType;
		protected System.Web.UI.WebControls.RadioButton rdClientType1;
		protected System.Web.UI.WebControls.RadioButton rdClientType2;
		protected System.Web.UI.WebControls.RadioButton rdNameFormat1;
		protected System.Web.UI.WebControls.RadioButton rdNameFormat2;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCUSTOMER_ZIP;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_NAME;
		protected System.Web.UI.HtmlControls.HtmlImage imgAGENCY_NAME;
		protected System.Web.UI.WebControls.RadioButton rdAddress1;
		protected Cms.CmsWeb.Controls.CmsButton btnReport;
		public string strAgencyID="";
		protected string strQuery="";
        Cms.DataLayer.DataWrapper objDataWrapper; 
		
					
		private void Page_Load(object sender, System.EventArgs e)
		{

            objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
			revCUSTOMER_ZIP.ValidationExpression		= aRegExpZip;
			revCUSTOMER_ZIP.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"24");
			
			/* AGENCY VALUES
				 * Check the SystemID of the logged in user.
				 * If the user is not a Wolverine user then display records of that agency ONLY
				 * else the normal flow follows */
			base.ScreenId="332";
			btnReport.CmsButtonClass                 =   Cms.CmsWeb.Controls.CmsButtonType.Read; 
			btnReport.PermissionString				=	gstrSecurityXML;	

			btnReport.Attributes.Add("onClick","ShowReport();return false;");

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
				//Added by Mohit Agarwal 17-Jan	
			else
				this.lstCustomerName.Items.Clear();

					
			if(!IsPostBack)
			{
				try
				{				
				
					DataSet ds2 = objDataWrapper.ExecuteDataSet("select (isnull(STATE_NAME,'')) as StateName ,STATE_ID as ID from MNT_COUNTRY_STATE_LIST order by StateName");
					lstStateName.DataSource = ds2.Tables[0];					
					lstStateName.DataTextField = "StateName";
					lstStateName.DataValueField = "ID";
					lstStateName.DataBind();
					this.lstStateName.Items.Insert(0,"All");
					this.lstStateName.SelectedIndex =0;
				}
				catch(Exception ex)
				{
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				}
				
			}	
		}
		protected void fillListBox(string AgencyID)
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
