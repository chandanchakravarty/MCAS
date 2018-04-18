namespace Cms.CmsWeb.WebControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

/******************************************************************************************
	<Author					: - >
	<Start Date				: -	>
	<End Date				: - >
	<Description			: - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - >
	<Modified By			: - >
	<Purpose				: - >
*******************************************************************************************/

	/// <summary>
	///		Summary description for menu.
	/// </summary>
	public class Menu : System.Web.UI.UserControl
	{
		private string strColorScheme = "1";
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			ColorScheme =  ((cmsbase)this.Page).GetColorScheme(); // Session["colorScheme"] == null ? "1" : Session["colorScheme"].ToString();
		}

		public string ColorScheme
		{
			set
			{
				strColorScheme = value;
			}
			get
			{
				return strColorScheme;
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
