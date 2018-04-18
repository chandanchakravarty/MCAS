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
using Cms.Account;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for DepositTabIndex.
	/// </summary>
	public class DepositTabIndex : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;

		private void Page_Load(object sender, System.EventArgs e)
		{
			//Fetching the query string
			GetQueryString();

			//Setting the screen id
			SetScreenId();

			TabCtl.TabURLs = "DepositIndex.aspx?CalledFrom=" + hidCalledFrom.Value + "&";
			
		}

		/// <summary>
		/// Fetch the query string from URL and saves it in hidden control
		/// </summary>
		private void GetQueryString()
		{
			hidCalledFrom.Value = Request.Params["CalledFrom"];
		}

		/// <summary>
		/// Sets the screen id of this page
		/// </summary>
		private void SetScreenId()
		{
			switch(hidCalledFrom.Value.ToUpper())
			{
				case "CUST":		//customer receipt
					base.ScreenId = "187_0";
					break;
				case "AGNC":		//agency receipt
					base.ScreenId = "187_1";
					break;
				case "CLAM":		//Claim receipt
					base.ScreenId = "187_2";
					break;
				case "RINS":		//Rinsurance receipt
					base.ScreenId = "187_3";
					break;
				case "MISC":		//Miscelleneous receipt
					base.ScreenId = "187_4";
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
