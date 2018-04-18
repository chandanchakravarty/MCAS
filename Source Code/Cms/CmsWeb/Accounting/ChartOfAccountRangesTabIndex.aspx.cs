/******************************************************************************************
<Author					: -   Ajit Singh Chahal
<Start Date				: -	  4/28/2005 9:06:31 PM
<End Date				: -	
<Description			: -   Code behind for tab Index.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
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

namespace Cms.CmsWeb.Maintenance.Accounting
{
	/// <summary>
	/// Summary description for ChartOfAccountRangesTabIndex.
	/// </summary>
	public class ChartOfAccountRangesTabIndex : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.HtmlControls.HtmlTableRow formTable;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl; //sneha
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId	=	"124";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl"); //sneha
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
