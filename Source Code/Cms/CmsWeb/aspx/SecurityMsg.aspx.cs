/******************************************************************************************
	<Author					: - > Anurag Verma
	<Start Date				: -	> 01/09/2005
	<End Date				: - > 
	<Description			: - > Message page for users to show whether it has rights or not
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	
	<Modified Date			: - > 
	<Modified By			: - > 
	<Purpose				: - > 
	
	
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

namespace Cms.CmsWeb.Aspx
{
	/// <summary>
	/// Summary description for SecurityMsg.
	/// </summary>
	public class SecurityMsg : Cms.CmsWeb.cmsbase  
	{
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
        protected System.Web.UI.WebControls.Label lblSecurityScr;
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			int customer_ID=GetCustomerID()==""?0:int.Parse(GetCustomerID());
			//ITrack # 6220- 8th Aug 2009 -Manoj
			System.Web.UI.WebControls.Image imgFetchUndisc =	(System.Web.UI.WebControls.Image)cltClientTop.FindControl("imgFetchUndisc");
			if (imgFetchUndisc!=null)
				imgFetchUndisc.Visible=false;

			System.Web.UI.WebControls.Image CustomerDetailQ2 =	(System.Web.UI.WebControls.Image)cltClientTop.FindControl("CustomerDetailQ2");
			if (CustomerDetailQ2!=null)
			{
				cltClientTop.Visible=false;
			}


			if(customer_ID!=0)
			{
				if(Session["appID"]!=null && Session["appID"].ToString() !="")
				{
					cltClientTop.CustomerID = customer_ID;
					cltClientTop.ApplicationID = int.Parse(GetAppID());							
					cltClientTop.AppVersionID=  int.Parse(GetAppVersionID());
					cltClientTop.ShowHeaderBand ="Application";
				}
				
				if(Session["policyID"]!=null && Session["policyID"].ToString()!="")
				{
					cltClientTop.CustomerID = customer_ID;
					cltClientTop.PolicyID = int.Parse(GetPolicyID());
					cltClientTop.PolicyVersionID  = int.Parse(GetPolicyVersionID());
					cltClientTop.ShowHeaderBand="Policy"; 
				}
				
				//Done for Itrack Issue 6539 on 13 Oct 09
				//if(Session["appID"].ToString()=="" && (Session["policyID"]==null))
				if(Session["appID"]==null || Session["appID"].ToString()=="" || (Session["policyID"]==null) || (Session["policyID"].ToString()==""))
				{
					cltClientTop.CustomerID = customer_ID;
					cltClientTop.ShowHeaderBand ="Client";
				}
			}
            lblSecurityScr.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2005");
			// Put user code to initialize the page here
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
