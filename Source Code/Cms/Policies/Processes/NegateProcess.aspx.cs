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

namespace Policies.Processes
{
	/// <summary>
	/// Summary description for NegateProcess.
	/// </summary>
	public class NegateProcess : Cms.Policies.policiesbase
	{
		protected Cms.CmsWeb.WebControls.PolicyTop cltPolicyTop;
        protected System.Web.UI.WebControls.Label capHeader;
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="5000_28";
			cltPolicyTop.CustomerID = Convert.ToInt32(Request.Params["CUSTOMER_ID"]);
			cltPolicyTop.PolicyID = Convert.ToInt32(Request.Params["POLICY_ID"]);
			cltPolicyTop.PolicyVersionID = Convert.ToInt32(Request.Params["policyVersionID"]);
            capHeader.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1238");
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
