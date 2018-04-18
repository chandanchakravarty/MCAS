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
using Cms.BusinessLayer.BlAccount;
using Cms.Model.Account;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.BusinessLayer.BlCommon;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for AG_CheckListingIframe.
	/// </summary>
	public class AG_CheckListingIframe : Cms.Account.AccountBase
	{
		
		public string TypeID = "";
		public string LinkPageName = "";
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;

		private void Page_Load(object sender, System.EventArgs e)
		{
			
			if(Request["TypeID"] != null)
			{
				TypeID = Request["TypeID"].ToString();
				LinkPageName = "AG_CheckListing.aspx?TypeID=" + TypeID.Trim();
			}
			else
			{
				//LinkPageName = "AG_CheckListing.aspx";
				LinkPageName = "CreateCheck.aspx";
			}
			TabCtl.TabURLs = LinkPageName;

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
