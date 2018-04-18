/******************************************************************************************
	<Author					: -> Ravindra Kumar Gupta
	<Start Date				: -> 03-22-2006
	<End Date				: ->
	<Description			: -> Coverage Limits Tab page for umbrella policy
	<Review Date			: ->
	<Reviewed By			: ->
	
	Modification History
******************************************************************************************/

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using Cms.ExceptionPublisher.ExceptionManagement;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;


namespace Cms.Policies.Aspx.Umbrella
{
	/// <summary>
	/// Summary description for PolicyUmbrellaLimitsTab.
	/// </summary>
	public class PolicyUmbrellaLimitsTab :Cms.Policies.policiesbase 
	{
		#region PageControls Declaration
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm POL_UMB_LIMIT_TAB;
		protected System.Web.UI.HtmlControls.HtmlTableRow formTable;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected Cms.CmsWeb.WebControls.WorkFlow	myWorkFlow;
		#endregion
	
		#region Page Load Event Handler
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId	=	"273";
			int customer_ID = GetCustomerID()=="" ? 0 : int.Parse(GetCustomerID());
			int policy_ID = GetPolicyID() =="" ? 0 : int.Parse(GetPolicyID()); 
			int policy_Version_ID = GetPolicyVersionID() == "" ? 0 : int.Parse(GetPolicyVersionID());
			
			cltClientTop.CustomerID = customer_ID;
			cltClientTop.PolicyID  = policy_ID ;
			cltClientTop.PolicyVersionID  = policy_Version_ID;
			cltClientTop.ShowHeaderBand = "Policy";
			cltClientTop.Visible= true;

			if(policy_ID!=0 && customer_ID!=0 && policy_Version_ID!=0)
			{
				formTable.Attributes.Add("style","display:inline;");  
			}
			else
			{
				formTable.Attributes.Add("style","display:none;");  
				capMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("109");       
				capMessage.Visible=true; 
			}
			SetWorkFlow();    
		}
		#endregion

		#region SetWorkFlow Function
		private void SetWorkFlow()
		{
			if(base.ScreenId == "273")
			{
				myWorkFlow.ScreenID=base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID ());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule ="POL";
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
			
		}
		#endregion

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
