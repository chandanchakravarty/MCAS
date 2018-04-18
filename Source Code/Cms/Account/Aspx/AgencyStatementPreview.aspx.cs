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

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for AgencyStatementPreview.
	/// </summary>
	public class AgencyStatementPreview : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_NAME;
		protected System.Web.UI.WebControls.DropDownList cmbMonth;
		protected System.Web.UI.HtmlControls.HtmlImage imgAGENCY_NAME;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnPrint;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_NAME;
		protected System.Web.UI.WebControls.DropDownList cmbYEAR;
		protected System.Web.UI.WebControls.DropDownList CmbCommType;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			base.ScreenId = "222";
			
			btnReset.PermissionString = gstrSecurityXML;
			btnReset.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;

			btnPrint.PermissionString = gstrSecurityXML;
			btnPrint.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.Attributes.Add("onclick","javascript:document.forms[0].reset();return false;");			
			btnPrint.Attributes.Add("onclick","javascript:ShowPrintPreview(); return false;");
			if(! this.IsPostBack )
			{
				int currYear = System.DateTime.Now.Year ;
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
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnPrint_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
