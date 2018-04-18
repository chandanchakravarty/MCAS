namespace Cms.CmsWeb.WebControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Configuration;
	/// <summary>
	///		Summary description for Footer.
	/// </summary>
	public class Footer : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblFooter;

		private void Page_Load(object sender, System.EventArgs e)
		{
			lblFooter.Text = ConfigurationSettings.AppSettings["FOOTER_TEXT"].ToString() + "; Web Server : " + Request.ServerVariables["LOCAL_ADDR"].ToString();
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
