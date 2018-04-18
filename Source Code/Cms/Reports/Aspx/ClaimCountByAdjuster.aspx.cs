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
	public class ClaimCountByAdjuster : Cms.CmsWeb.cmsbase
	{
		//protected System.Web.UI.WebControls.Label lblExpirationStartDate;
		//protected System.Web.UI.WebControls.TextBox txtExpirationStartDate;
		//protected System.Web.UI.WebControls.HyperLink hlkExpirationStartDate;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revExpirationStartDate;
		//protected System.Web.UI.WebControls.CompareValidator cmpExpirationDate;
		//protected System.Web.UI.WebControls.Label lblExpirationEndDate;
		//protected System.Web.UI.WebControls.TextBox txtExpirationEndDate;
		//protected System.Web.UI.WebControls.HyperLink hlkExpirationEndDate;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revExpirationEndDate;
		protected System.Web.UI.WebControls.Label lblLOB;
		protected System.Web.UI.WebControls.ListBox lstLOB;
		protected System.Web.UI.WebControls.Label lblFirstSort;
		protected System.Web.UI.WebControls.DropDownList cmbFirstSort;
		protected System.Web.UI.WebControls.Label lblSecondSort;
		protected System.Web.UI.WebControls.DropDownList cmbSecondSort;
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.Label lblPartyType;
		protected System.Web.UI.WebControls.ListBox lstPartyType;
		protected System.Web.UI.WebControls.Label lblAdjuster;
		protected System.Web.UI.WebControls.ListBox lstAdjuster;
		protected System.Web.UI.WebControls.Label lblClaimStatus;
		protected System.Web.UI.WebControls.ListBox lstClaimStatus;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnReport;

		protected System.Web.UI.WebControls.DropDownList cmbYEAR;
		protected System.Web.UI.WebControls.DropDownList cmbMonth;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			//hlkExpirationStartDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtExpirationStartDate,document.forms[0].txtExpirationStartDate)");			
			//hlkExpirationEndDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtExpirationEndDate,document.forms[0].txtExpirationEndDate)");			
			//revExpirationStartDate.ValidationExpression = aRegExpDate;
			//revExpirationEndDate.ValidationExpression = aRegExpDate;	
			base.ScreenId="363";
			btnReport.CmsButtonClass                 =   Cms.CmsWeb.Controls.CmsButtonType.Read; 
			btnReport.PermissionString				=	gstrSecurityXML;	

			btnReport.Attributes.Add("onClick","ShowReport();return false;");

			Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text , DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
			
			if(!IsPostBack)
			{
				try
				{	
					DataSet ds1 = objDataWrapper.ExecuteDataSet("select d.detail_type_description as TYPE ,d.detail_type_id as ID From clm_type_master m join clm_type_detail d on m.type_id = d.type_id where m.type_description='Party Types'order by detail_type_description");
					lstPartyType.DataSource = ds1.Tables[0];					
					lstPartyType.DataTextField = "TYPE";
					lstPartyType.DataValueField = "ID";
					lstPartyType.DataBind();
					this.lstPartyType.Items.Insert(0,"All");
					this.lstPartyType.SelectedIndex =0;

					DataSet ds2 = objDataWrapper.ExecuteDataSet("select adjuster_name as NAME,adjuster_id as ID From clm_adjuster order by adjuster_name");
					lstAdjuster.DataSource = ds2.Tables[0];					
					lstAdjuster.DataTextField = "NAME";
					lstAdjuster.DataValueField = "ID";
					lstAdjuster.DataBind();
					this.lstAdjuster.Items.Insert(0,"All");
					this.lstAdjuster.SelectedIndex =0;

					DataSet ds3 = objDataWrapper.ExecuteDataSet("select v.lookup_value_desc as TYPE,v.lookup_unique_id as ID From mnt_lookup_tables t join mnt_lookup_values v on v.lookup_id = t.lookup_id where t.lookup_name = 'CLMST' order by lookup_value_desc");
					lstClaimStatus.DataSource = ds3.Tables[0];					
					lstClaimStatus.DataTextField = "TYPE";
					lstClaimStatus.DataValueField = "ID";
					lstClaimStatus.DataBind();
					this.lstClaimStatus.Items.Insert(0,"All");
					this.lstClaimStatus.SelectedIndex =0;
	
					DataSet ds4 = objDataWrapper.ExecuteDataSet("select (isnull(LOB_DESC,'') + ' ') as LOB ,LOB_ID as ID from MNT_LOB_MASTER order by LOB");
					lstLOB.DataSource = ds4.Tables[0];					
					lstLOB.DataTextField = "LOB";
					lstLOB.DataValueField = "ID";
					lstLOB.DataBind();
					this.lstLOB.Items.Insert(0,"All");
					this.lstLOB.SelectedIndex =0;	
				
					int currYear = System.DateTime.Now.Year;
					int prevYear =currYear -1;
					int prYear  =prevYear-1;
					cmbYEAR.Items.Add(new ListItem(currYear.ToString(),currYear.ToString()));
					cmbYEAR.Items.Add(new ListItem(prevYear.ToString(),prevYear.ToString()));
					cmbYEAR.Items.Add(new ListItem(prYear.ToString(),prYear.ToString()));				

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
