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

namespace Reports.Aspx
{	
	public class EarnedPremium :  Cms.CmsWeb.cmsbase	
	{
		protected System.Web.UI.WebControls.DropDownList cmbMonth;
		protected System.Web.UI.WebControls.DropDownList cmbYEAR;
		protected Cms.CmsWeb.Controls.CmsButton btnReport;
		protected Cms.CmsWeb.Controls.CmsButton btnReportGrpBy;		
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="408";

			btnReport.CmsButtonClass                =   Cms.CmsWeb.Controls.CmsButtonType.Read; 
			btnReport.PermissionString				=	gstrSecurityXML;	

			btnReportGrpBy.CmsButtonClass                =   Cms.CmsWeb.Controls.CmsButtonType.Read; 
			btnReportGrpBy.PermissionString				=	gstrSecurityXML;	

			btnReport.Attributes.Add("onClick","ShowReport('N');return false;");
			btnReportGrpBy.Attributes.Add("onClick","ShowReport('G');return false;");

			if(! this.IsPostBack )
			{
				int currYear = System.DateTime.Now.Year;
				int prevYear =currYear -1;
				int prYear  =prevYear-1;
				cmbYEAR.Items.Add(new ListItem(currYear.ToString(),currYear.ToString()));
				cmbYEAR.Items.Add(new ListItem(prevYear.ToString(),prevYear.ToString()));
				cmbYEAR.Items.Add(new ListItem(prYear.ToString(),prYear.ToString()));				
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
