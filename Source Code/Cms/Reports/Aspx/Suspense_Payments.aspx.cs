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

//using Cms.DataLayer;
//using Cms.CmsWeb;

namespace Reports.Aspx
{
	public class Suspense_Payments : Cms.CmsWeb.cmsbase
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
		protected System.Web.UI.WebControls.Panel Panel1;
		protected Cms.CmsWeb.Controls.CmsButton btnReport;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			hlkExpirationStartDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtExpirationStartDate,document.forms[0].txtExpirationStartDate)");			
			hlkExpirationEndDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtExpirationEndDate,document.forms[0].txtExpirationEndDate)");			
			revExpirationStartDate.ValidationExpression = aRegExpDate;
			revExpirationEndDate.ValidationExpression = aRegExpDate;	
			base.ScreenId="380";
			btnReport.CmsButtonClass                 =   Cms.CmsWeb.Controls.CmsButtonType.Read; 
			btnReport.PermissionString				=	gstrSecurityXML;	

			btnReport.Attributes.Add("onClick","ShowReport();return false;");
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
