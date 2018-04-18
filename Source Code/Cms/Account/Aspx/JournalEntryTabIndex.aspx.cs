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

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for JournalEntryTabIndex.
	/// </summary>
	public class JournalEntryTabIndex : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		private string strCalledFrom;

		private void Page_Load(object sender, System.EventArgs e)
		{
			GetQueryString();

			//Setting the name of tabs
			//This function also sets the screen id of screens
			SetTabProperty();
		}

		/// <summary>
		/// Get query string from url 
		/// </summary>
		private void GetQueryString()
		{
			strCalledFrom = Request.Params["CalledFrom"];
			if ( strCalledFrom == null)
				strCalledFrom = "";
		}

		/// <summary>
		/// Makes the Tabs titles and tab urls
		/// </summary>
		/// <returns>Header string for grid</returns>
		private void SetTabProperty()
		{
			TabCtl.TabURLs = "JournalEntryMasterIndex.aspx?CalledFrom=" + strCalledFrom + "&";

			switch(strCalledFrom.ToUpper())
			{
				case "TMPLT":
					TabCtl.TabTitles = "Journal Entry Template";
					//setting the screen id
					base.ScreenId = "174";
					break;
				case "RECURR":
					TabCtl.TabTitles = "Journal Entry Recurring";
					//setting the screen id
					base.ScreenId = "175";
					break;
				case "13PER":
					TabCtl.TabTitles = "13th Period Adjustment";
					//setting the screen id
					base.ScreenId = "176";
					break;
				default:
					TabCtl.TabTitles = "Manual Journal Entry";
					//setting the screen id
					base.ScreenId = "173";
					break;
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
