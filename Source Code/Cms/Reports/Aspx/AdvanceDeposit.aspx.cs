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
	public class AdvanceDeposit : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label lblCustomer;
		protected System.Web.UI.WebControls.DropDownList cmbMonth;
		protected System.Web.UI.WebControls.TextBox txtYear;
		protected System.Web.UI.WebControls.RangeValidator rnvYear;
		protected System.Web.UI.WebControls.RegularExpressionValidator revYear;		
		protected System.Web.UI.WebControls.ListBox lstCustomerName;
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.Label lblAgent;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_NAME;
		protected System.Web.UI.HtmlControls.HtmlImage imgAGENCY_NAME;
		protected Cms.CmsWeb.Controls.CmsButton btnReport;
		protected string strQuery="";
		public string strAgencyID="";

        Cms.DataLayer.DataWrapper objDataWrapper;
		
	
		private void Page_Load(object sender, System.EventArgs e)
		{

            objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
			base.ScreenId="409";
			btnReport.CmsButtonClass                 =   Cms.CmsWeb.Controls.CmsButtonType.Read; 
			btnReport.PermissionString				=	gstrSecurityXML;	
             SetErrorMessages();
			btnReport.Attributes.Add("onClick","ShowReport();return false;");

			string  strSystemID			 = GetSystemId();

            string strCarrierSystemID = CarrierSystemID;//System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString();
			if ( strSystemID.Trim().ToUpper() != strCarrierSystemID.Trim().ToUpper())
			{
				DataSet objDataSet =Cms.BusinessLayer.BlCommon.ClsAgency.GetAgencyIDAndNameFromCode(strSystemID);

				if (objDataSet.Tables[0].Rows.Count > 0 )
				{
					string strAgencyName = objDataSet.Tables[0].Rows[0]["AGENCY_DISPLAY_NAME"].ToString();
					strAgencyID = objDataSet.Tables[0].Rows[0]["AGENCY_ID"].ToString();
					txtAGENCY_NAME.Text = strAgencyName;
					imgAGENCY_NAME.Visible=false;
					fillListBox(strAgencyID);
				}
			}

			strAgencyID = hidAGENCY_ID.Value;
			if(strAgencyID != null && strAgencyID!="")
			{
				fillListBox(strAgencyID);
			}

			//Added By Raghav
			txtYear.Text = DateTime.Now.Year.ToString();
			cmbMonth.SelectedValue = DateTime.Now.Month.ToString();
		 
			}       
	     	
		private void fillListBox(string AgencyID)
		{
			
			this.lstCustomerName.Items.Clear();	
			//strQuery = " Select distinct (isnull(CUSTOMER_FIRST_NAME,'') + ' ' + isnull(CUSTOMER_MIDDLE_NAME,'') + ' '+ isnull(CUSTOMER_LAST_NAME,'')) as CUSTOMER_FIRST_NAME ,CUSTOMER_ID  FROM CLT_CUSTOMER_LIST  where customer_agency_id = '" + strAgencyID + "' order by customer_first_name";
			strQuery = "Select distinct (isnull(CUSTOMER_FIRST_NAME,'') + ' ' + isnull(CUSTOMER_MIDDLE_NAME,'') + ' '+ " +
						" isnull(CUSTOMER_LAST_NAME,'')) as CUSTOMER_FIRST_NAME ,PCL.CUSTOMER_ID as CUSTOMER_ID FROM CLT_CUSTOMER_LIST  INNER JOIN " +
						" POL_CUSTOMER_POLICY_LIST PCL ON CLT_CUSTOMER_LIST.CUSTOMER_ID=PCL.CUSTOMER_ID " +
						" where PCL.AGENCY_ID = '" + strAgencyID + "' order by customer_first_name ";

			DataSet ds = objDataWrapper.ExecuteDataSet(strQuery);
			lstCustomerName.DataSource = ds.Tables[0];					
			lstCustomerName.DataTextField = "customer_first_name";
			lstCustomerName.DataValueField = "customer_id";
			lstCustomerName.DataBind();
			this.lstCustomerName.Items.Insert(0,"All");
			this.lstCustomerName.SelectedIndex =0;	

		}
		//Added By Raghav
		private void SetErrorMessages()
		{
			revYear.ValidationExpression = aRegExpInteger;
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
