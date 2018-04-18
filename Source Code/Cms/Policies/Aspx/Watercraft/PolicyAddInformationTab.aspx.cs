/******************************************************************************************
<Author					: - Vijay Arora
<Start Date				: -	01-12-2005
<End Date				: -	
<Description			: - Class for WaterCraft Underwriting Questions Information Tab.
<Review Date			: - 
<Reviewed By			: - 	
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

namespace Cms.Policies.Aspx.Watercrafts
{
	/// <summary>
	/// Summary description for AddInformationTab.
	/// </summary>
	public class PolicyAddInformationTab : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.HtmlControls.HtmlTableRow messageTable;
		protected System.Web.UI.HtmlControls.HtmlTableRow formTable;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;

        
        
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
        
    
		private void Page_Load(object sender, System.EventArgs e)
		{
			#region SETTING SCREEN ID
			if(GetLOBString()=="WAT")
			{
				base.ScreenId="250" + "_0";
				hidCalledFrom.Value="WAT";
			}
			else if(GetLOBString()=="HOME")
			{
				base.ScreenId="147" + "_0"; 
				hidCalledFrom.Value="HWAT";
			}
			else if(GetLOBString()=="RENT")
				base.ScreenId="165"; 
		
			#endregion
			// Put user code to initialize the page here

			#region set Workflow cntrol
			myWorkFlow.ScreenID = base.ScreenId;
			myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
			myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
			myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
			myWorkFlow.WorkflowModule="POL";
			#endregion
            
			int customer_ID=GetCustomerID()==""?0:int.Parse(GetCustomerID());
			int policy_ID=GetPolicyID()==""?0:int.Parse(GetPolicyID());
			int policy_Version_ID=GetPolicyVersionID() ==""?0:int.Parse(GetPolicyVersionID());

			if(policy_ID!=0 && customer_ID!=0 && policy_Version_ID!=0)
			{
				formTable.Attributes.Add("style","display:inline;");  
				cltClientTop.CustomerID = int.Parse(GetCustomerID());
				cltClientTop.PolicyID = int.Parse(GetPolicyID());
				cltClientTop.PolicyVersionID = int.Parse(GetPolicyVersionID());
        
				cltClientTop.ShowHeaderBand ="Policy";
				cltClientTop.Visible = true;           
			}
			else
			{
				formTable.Attributes.Add("style","display:none;");  
				capMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("109");       
				capMessage.Visible=true; 
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
