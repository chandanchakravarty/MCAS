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

namespace Cms.CmsWeb.aspx
{
	/// <summary>
	/// Summary description for CapitalRaterLogin.
	/// </summary>
	
	//Kasana -- Parvesh
	public class CapitalRaterLogin : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblMessage;
			
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			
				if(Request.QueryString["GUID"]!=null && Request.QueryString["GUID"]!="")
				{
					ClearCookies();		
					if(Request.QueryString["SYSTEM_ID"]!=null && Request.QueryString["SYSTEM_ID"]!="")
						Session["SYSTEM_ID"] = Request.QueryString["SYSTEM_ID"];

					if(Request.QueryString["USER_ID"]!=null && Request.QueryString["USER_ID"]!="")
						Session["USER_ID"] = Request.QueryString["USER_ID"];			
				

					Session["GUID"] = Request.QueryString["GUID"];
					Response.Redirect("/cms/cmsweb/aspx/login.aspx");
				}

			//Catching Exception 
			Cms.CmsWeb.Maintenance.CapitalRateComparison_Test objPage;
			objPage = (Cms.CmsWeb.Maintenance.CapitalRateComparison_Test)Context.Handler;
			lblMessage.Text = objPage.CapitalException.ToString();
		
			
		}
		private void ClearCookies()
		{
			Response.Cookies["ckUserId"].Expires=DateTime.Now.AddYears(-30);
			Response.Cookies["ckUserTypeId"].Expires=DateTime.Now.AddYears(-30);
			Response.Cookies["ckUserNm"].Expires=DateTime.Now.AddYears(-30);
			Response.Cookies["ckSysId"].Expires=DateTime.Now.AddYears(-30);
			Response.Cookies["ckImgFld"].Expires=DateTime.Now.AddYears(-30);
			Response.Cookies["ckImgFld"].Expires=DateTime.Now.AddYears(-30);
			Response.Cookies["ckUserFLNm"].Expires=DateTime.Now.AddYears(-30);
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
