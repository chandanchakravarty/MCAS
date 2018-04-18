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

namespace Cms.Policies.Aspx
{
	/// <summary>
	/// Summary description for PolicyAutoGeneralInformationIFrame.
	/// </summary>
	public class PolicyAutoGeneralInformationIFrame : Cms.Policies.policiesbase
	{
		#region Local form variables
		//START:*********** Local form variables *************		
		public string strCalledFrom =  "";
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.HtmlControls.HtmlTableRow formTable;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;

		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;	
		
		
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			SetClientTop();
			//cltClientTop.Visible = true;
           
			base.ScreenId="229";
			 SetWorkflow();

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************			
			if(Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"] != "")
			{
				strCalledFrom = Request.QueryString["CalledFrom"];
			}						
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.PolicyAutoGeneralInformationIFrame" ,System.Reflection.Assembly.GetExecutingAssembly());
			//Added by Charles on 24-Dec-09 for Itrack 6830
			string strStateID=Cms.BusinessLayer.BlApplication.ClsLocation.GetPolStateNameOnStateID(int.Parse(GetCustomerID()),int.Parse(GetPolicyID()),int.Parse(GetPolicyVersionID())).Rows[0]["STATE_ID"].ToString().Trim();
			if(strStateID == "14")//Applicable for Indiana Only
			{
				TabCtl.TabTitles="Underwriting Questions,Underwriting Tier";
				TabCtl.TabURLs = "PolicyAutoGeneralInformation.aspx?CalledFrom=" + strCalledFrom 
					+ "?&,../../Aspx/PolicyPPGeneralInformation_Additional.aspx";
			}
			//Added till here
		}
		
		private void SetWorkflow()
		{
			myWorkFlow.IsTop = true;
			myWorkFlow.ScreenID = base.ScreenId;
			myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
			myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
			myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
			myWorkFlow.WorkflowModule="POL";
		}

		private void SetClientTop()
		{
			cltClientTop.CustomerID = int.Parse(GetCustomerID());
			cltClientTop.PolicyID  = int.Parse(GetPolicyID());
			cltClientTop.PolicyVersionID  = int.Parse(GetPolicyVersionID());
			cltClientTop.ShowHeaderBand ="Policy";
			cltClientTop.Visible = true;

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
