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
	public class AgentList : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.Label lblAgent;
		protected System.Web.UI.WebControls.DropDownList lstHierarchy;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_NAME;
		protected System.Web.UI.HtmlControls.HtmlImage imgAGENCY_NAME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidHierarchySelected;
		protected System.Web.UI.WebControls.Label lblHierarchy;
		protected Cms.CmsWeb.Controls.CmsButton btnReport;
					
		private void Page_Load(object sender, System.EventArgs e)
		{
			Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text , DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
			string strAgencyID ="";
			strAgencyID = hidAGENCY_ID.Value;
			base.ScreenId="334";
			
			btnReport.CmsButtonClass                 =   Cms.CmsWeb.Controls.CmsButtonType.Read; 
			btnReport.PermissionString				=	gstrSecurityXML;	

			btnReport.Attributes.Add("onClick","ShowReport();return false;");
			if(!IsPostBack)
			{
				try
				{
					/*Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.Text , DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
					DataSet ds = objDataWrapper.ExecuteDataSet("select (isnull(AGENCY_DISPLAY_NAME,'') + ' ') as AgencyName ,Agency_id as ID from MNT_AGENCY_LIST order by AgencyName");
					lstAgentName.DataSource = ds.Tables[0];					
					lstAgentName.DataTextField = "AgencyName";
					lstAgentName.DataValueField = "ID";
					lstAgentName.DataBind();
					this.lstAgentName.Items.Insert(0,"All");
					this.lstAgentName.SelectedIndex =0;*/
					this.lstHierarchy.Items.Insert(0,"No");
					this.lstHierarchy.Items.Insert(1,"Yes");
					this.lstHierarchy.SelectedIndex =1;
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
