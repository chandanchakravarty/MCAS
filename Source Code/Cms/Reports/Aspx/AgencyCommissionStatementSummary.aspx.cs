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
using Cms.CmsWeb.Controls;

using Cms.DataLayer;
using Cms.CmsWeb;

namespace Reports.Aspx
{	
	public class AgencyCommissionStatementSummary :  Cms.CmsWeb.cmsbase	
	{
		protected System.Web.UI.WebControls.TextBox txtAGENCY_NAME;
		protected System.Web.UI.WebControls.DropDownList cmbMonth;
		protected System.Web.UI.WebControls.DropDownList cmbYEAR;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnPrint;
		protected System.Web.UI.HtmlControls.HtmlImage imgAGENCY_NAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;
		protected System.Web.UI.WebControls.Label lblAgency;
		protected System.Web.UI.WebControls.ListBox lstAgencyName;
		protected Cms.CmsWeb.Controls.CmsButton btnReport;
		protected string strCalledfrom="";
		//Added By Raghav For Itrack Issue #4617
		protected string strCurrentMonth="0";
		protected string strCurrentYear="0";
		public string strCarrier = "";
		public string strAgencyId = "";
	    
		private void Page_Load(object sender, System.EventArgs e)
		{

			strCarrier = CarrierSystemID;
			strAgencyId = GetSystemId();
			strCalledfrom=Request.QueryString["CALLEDFROM"].ToString();
			Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text , DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
			
			if(strCalledfrom == "SR")
				base.ScreenId="370";
			else if(strCalledfrom == "SA")
				base.ScreenId="371";
			else if(strCalledfrom == "ACSC")
				base.ScreenId="372";

			btnReport.CmsButtonClass                 =   Cms.CmsWeb.Controls.CmsButtonType.Execute; 
			btnReport.PermissionString				=	gstrSecurityXML;	

			btnReport.Attributes.Add("onClick","ShowReport();return false;");

			if(! this.IsPostBack )
			{
				//Added By Raghav For Itrack Issue #4617
				strCurrentMonth = System.DateTime.Now.Month.ToString();
				strCurrentYear = System.DateTime.Now.Year.ToString();
				int currYear = System.DateTime.Now.Year;
				int prevYear =currYear -1;
				int prYear  =prevYear-1;
				cmbYEAR.Items.Add(new ListItem(currYear.ToString(),currYear.ToString()));
				cmbYEAR.Items.Add(new ListItem(prevYear.ToString(),prevYear.ToString()));
				cmbYEAR.Items.Add(new ListItem(prYear.ToString(),prYear.ToString()));	
			
				// Modified by Mohit Agarwal, 15-Feb for hiding other agencies for Agency login
				string AgencyId = GetSystemId();
				if(AgencyId.ToUpper() != CarrierSystemID.ToUpper())//System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToUpper())
				{
					//TO MOVE TO LOCAL VSS
					//string selStmt = "select RTRIM(ISNULL (AGENCY_CODE,'')) +'-'+ cast(isnull(NUM_AGENCY_CODE,'') as varchar) +'-'+ (isnull(AGENCY_DISPLAY_NAME,'')) as Name ,AGENCY_ID as ID from MNT_AGENCY_LIST where AGENCY_CODE = '" + AgencyId + "' order by AGENCY_DISPLAY_NAME,AGENCY_CODE";
					//Sorted on the basis of agency_name.
                    string selStmt = "select (isnull(AGENCY_DISPLAY_NAME,'')) + '-' +RTRIM(ISNULL (AGENCY_CODE,'')) +'-'+ cast(isnull(NUM_AGENCY_CODE,'') as varchar) as Name ,AGENCY_ID as ID from MNT_AGENCY_LIST where AGENCY_CODE = '" + AgencyId + "' order by AGENCY_DISPLAY_NAME,AGENCY_CODE";  
					DataSet ds2 = objDataWrapper.ExecuteDataSet(selStmt);
					lstAgencyName.DataSource = ds2.Tables[0];					
					lstAgencyName.DataTextField = "Name";
					lstAgencyName.DataValueField = "ID";
					lstAgencyName.DataBind();
					this.lstAgencyName.SelectedIndex =0;
//					lstAgencyName.Enabled = false;
				}
				else
				{
					//DataSet ds2 = objDataWrapper.ExecuteDataSet("select RTRIM(ISNULL (AGENCY_CODE,'')) +'-'+ cast(isnull(NUM_AGENCY_CODE,'') as varchar) +'-'+ (isnull(AGENCY_DISPLAY_NAME,'')) as Name ,AGENCY_ID as ID from MNT_AGENCY_LIST order by AGENCY_DISPLAY_NAME,AGENCY_CODE");
					//Sorted on the basis of agency_name.
					DataSet ds2 = objDataWrapper.ExecuteDataSet("select (isnull(AGENCY_DISPLAY_NAME,'')) + '-' + RTRIM(ISNULL (AGENCY_CODE,'')) +'-'+ cast(isnull(NUM_AGENCY_CODE,'') as varchar) as Name ,AGENCY_ID as ID from MNT_AGENCY_LIST order by AGENCY_DISPLAY_NAME,AGENCY_CODE");  
					lstAgencyName.DataSource = ds2.Tables[0];					
					lstAgencyName.DataTextField = "Name";
					lstAgencyName.DataValueField = "ID";
					lstAgencyName.DataBind();
					this.lstAgencyName.Items.Insert(0,"All");
					this.lstAgencyName.SelectedIndex =0;
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
