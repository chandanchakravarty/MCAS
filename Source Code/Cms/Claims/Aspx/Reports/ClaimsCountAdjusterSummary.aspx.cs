/******************************************************************************************
	<Author					: - > Vijay Arora
	<Start Date				: -	> 23-08-2006
	<End Date				: - >
	<Description			: - > Report screen for Claims Count by Adjuster with Summary
	<Review Date			: - >
	<Reviewed By			: - >
*******************************************************************************************/
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
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BLClaims;
using Cms.Claims;
using Cms.Model.Claims;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using Cms.CmsWeb.Controls; 

namespace Cms.Claims.Aspx.Reports
{
	/// <summary>
	/// Summary description for ClaimsCountAdjusterSummary.
	/// </summary>
	public class ClaimsCountAdjusterSummary : Cms.Claims.ClaimBase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnDisplay;
		protected System.Web.UI.WebControls.Label capFromDate;
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.HyperLink hlkFromDate;
		protected System.Web.UI.WebControls.Label capToDate;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.HyperLink hlkToDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revToDate;
		protected System.Web.UI.WebControls.CustomValidator csvToDate;
		protected System.Web.UI.WebControls.ListBox cmbPartyTypes;
		protected System.Web.UI.WebControls.ListBox cmbSelectedPartyTypes;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAssignPartyTypes;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnUnAssignPartyTypes;
		protected System.Web.UI.WebControls.Label capPartyTypes;
		protected System.Web.UI.WebControls.Label capSelectedPartyTypes;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFromDate;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="345_0";
			
			btnDisplay.CmsButtonClass		=	CmsButtonType.Write;
			btnDisplay.PermissionString		=	gstrSecurityXML;

			btnReset.CmsButtonClass		=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;
			
			hlkFromDate.Attributes.Add("OnClick","fPopCalendar(document.ADJUSTER_SUMMARY.txtFromDate,document.ADJUSTER_SUMMARY.txtFromDate)"); //Javascript Implementation for Date
			hlkToDate.Attributes.Add("OnClick","fPopCalendar(document.ADJUSTER_SUMMARY.txtToDate,document.ADJUSTER_SUMMARY.txtToDate)"); //Javascript Implementation for Date

			cmbPartyTypes.Items.Insert(0,"First Item");
			cmbPartyTypes.Items[0].Value = "0";
			cmbPartyTypes.Items.Insert(1,"Second Item");
			cmbPartyTypes.Items.Insert(2,"Third Item");
			cmbPartyTypes.Items.Insert(3,"Fourth Item");
			cmbPartyTypes.Items.Insert(4,"Fifth Item");
			
			cmbPartyTypes.Items[1].Value = "1";
			cmbPartyTypes.Items[2].Value = "2";
			cmbPartyTypes.Items[3].Value = "3";
			cmbPartyTypes.Items[4].Value = "4";
			

			revFromDate.ValidationExpression = aRegExpDate; 
			revToDate.ValidationExpression = aRegExpDate; 
			csvToDate.ErrorMessage = "Should not greater than to date";
			revFromDate.ErrorMessage = "enter correct date";
			revToDate.ErrorMessage = "enter correct date";
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
