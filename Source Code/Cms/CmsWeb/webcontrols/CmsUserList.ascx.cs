namespace Cms.CmsWeb.WebControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Cms.BusinessLayer.BlCommon;

/******************************************************************************************
	<Author					: Anshuman Sharan- >
	<Start Date				: March 11, 2005-	>
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
	///		Summary description for CmsUserList.
	/// </summary>
	public class CmsUserList : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DropDownList cmbUserList;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			cmbUserList.DataSource		=	ClsCommon.GetUserList();
			cmbUserList.DataTextField	=	"username";
			cmbUserList.DataValueField	=	"userid";
			cmbUserList.DataBind();
		}
		public int GetUserId()
		{
			return int.Parse(cmbUserList.SelectedItem.Value);
			//return 1;
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
