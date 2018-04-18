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
using Cms.Model.Policy;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon ;
using Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.CmsWeb.Controls;
namespace Cms.Policies.Aspx.Motorcycle
{
	/// <summary>
	/// Summary description for PolicyMotorcycleGeneralInformation.
	/// </summary>
	///
	public class PolicyMotorcycleGeneralInformation :  Cms.Policies.policiesbase
	{
		
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.HtmlControls.HtmlTableRow formTable;		
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;	
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.HtmlControls.HtmlTableRow messageTable;
	
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			
			#region SETTING SCREEN ID

			if(GetLOBString()=="WAT")
				base.ScreenId="71"; 
			else if(GetLOBString()=="HOME")
				base.ScreenId="147"; 
			else if(GetLOBString()=="RENT")
				base.ScreenId="165"; 
			else if(GetLOBString()=="MOT")
				base.ScreenId="48"; 		
	
			#endregion

			base.ScreenId="48"; 
			SetWorkflow();
			SetClientTop();		
		}

		private void SetWorkflow()
		{
			myWorkFlow.IsTop = true;
			myWorkFlow.ScreenID = base.ScreenId;
			myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
			myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
			myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
			myWorkFlow.WorkflowModule ="POL";
		}

		private void SetClientTop()
		{
			int customer_ID=GetCustomerID()==""?0:int.Parse(GetCustomerID());
			int policy_ID=GetPolicyID()==""?0:int.Parse(GetPolicyID());
			int policy_Version_ID=GetPolicyVersionID()==""?0:int.Parse(GetPolicyVersionID());

			if(policy_ID!=0 && customer_ID!=0 && policy_Version_ID!=0)
			{
				formTable.Attributes.Add("style","display:inline;");  
				//         messageTable.Attributes.Add("style","display:none;");  
				int flag=0;
				cltClientTop.CustomerID = int.Parse(GetCustomerID());
				
				if(GetPolicyID()!="" && GetPolicyID()!=null && GetPolicyID()!="0")
				{
					cltClientTop.PolicyID = int.Parse(GetPolicyID());
					flag=1;
				}

				if(GetPolicyVersionID()!="" && GetPolicyVersionID()!=null && GetPolicyVersionID()!="0")
				{
					cltClientTop.PolicyVersionID = int.Parse(GetPolicyVersionID());
					flag=2;
				}
                
				if(flag>0)
					cltClientTop.ShowHeaderBand ="Policy";
				else
					cltClientTop.ShowHeaderBand ="Client";

				cltClientTop.Visible = true;			
			}
			else
			{
				formTable.Attributes.Add("style","display:none;");  
				//                messageTable.Attributes.Add("style","display:inline;");  
				capMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("109");       
				capMessage.Visible=true; 
			} 

		}

	#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
	
	#endregion

	}
}
