/******************************************************************************************
	<Author					: -> Ravindra Kumar Gupta
	<Start Date				: -> 04-03-2006
	<End Date				: ->
	<Description			: -> 
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
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Policies.Aspx.GeneralLiability
{
	/// <summary>
	/// Summary description for PolicyGeneralLocationIndex.
	/// </summary>
	public class PolicyGeneralLocationIndex :Cms.Policies.policiesbase 
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.HtmlControls.HtmlTableRow formTable;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected Cms.CmsWeb.WebControls.WorkFlow	myWorkFlow;

	
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId	=	"283";
			int intCustomerId = GetCustomerID()=="" ? 0 : int.Parse(GetCustomerID());
			int intPolicyId = GetPolicyID ()=="" ? 0 : int.Parse(GetPolicyID ());
			int intPolicyVersionId = GetPolicyVersionID () == "" ? 0 : int.Parse(GetPolicyVersionID ());
			
			cltClientTop.PolicyID=intPolicyId; 
			cltClientTop.CustomerID = intCustomerId;
			cltClientTop.PolicyVersionID =intPolicyVersionId;
			cltClientTop.ShowHeaderBand = "Policy";
			cltClientTop.Visible= true;

			if(intPolicyId !=0 && intCustomerId!=0 && intPolicyVersionId !=0)
			{
				formTable.Attributes.Add("style","display:inline;");  
			}
			else
			{
				formTable.Attributes.Add("style","display:none;");  
				capMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("109");       
				capMessage.Visible=true; 
			}
			SetWorkflow();    
		}
		
		private void SetWorkflow()
		{
			if(base.ScreenId == "283")
			{
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule="POL";
			}
			else
			{
				myWorkFlow.Display	=	false;
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
