/******************************************************************************************
<Author				: -   Anurag Verma
<Start Date			: -	  16/11/2005
<End Date			: -	  
<Description		: -   Policy Homeowner General Information Screen
<Review Date		: - 
<Reviewed By		: - 	
Modification History

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

namespace Cms.Policies.aspx.HomeOwners
{
	/// <summary>
	/// Summary description for PolicyGeneralInfoFrame.
	/// </summary>
	public class PolicyGeneralInfoFrame : Cms.Policies.policiesbase
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

			#region SETTING SCREEN ID
            if (GetLOBString() == "HOME")
                base.ScreenId = "240";		 //base.ScreenId="60";		 
            else if (GetLOBString() == "RENT")
                base.ScreenId = "158";
            else if (GetLOBString() == "MTIME")
                base.ScreenId = "572";
			#endregion
			
			SetWorkflow();
			if(Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"] != "")
			{
				strCalledFrom = Request.QueryString["CalledFrom"];
			}						
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************			
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.aspx.HomeOwners.PolicyGeneralInfoFrame" ,System.Reflection.Assembly.GetExecutingAssembly());		
			//TabCtl.TabURLs = "PolicyHomeownerGeneralInfo.aspx?CalledFrom=" + strCalledFrom.ToUpper() + "&";
			

		// Working Code Commented ::  Discount& surcharges screen removed

		/*	if(strCalledFrom.Trim().ToUpper().Equals("HOME"))
			{
				TabCtl.TabURLs = "PolicyHomeownerGeneralInfo.aspx?CalledFrom=" + strCalledFrom.ToUpper() 
					+ "&,../../Aspx/PolicyShowDiscountAndSurcharges.aspx"
					+ "?&,../../Aspx/Watercraft/PolicyAddWatercraftGenInfo.aspx?&transferdata="
					+ "&,../../aspx/PolicyShowDiscountAndSurcharges.aspx?CalledFrom=HWAT&transferdata";

				TabCtl.TabTitles="Home Underwriting Questions,Home Dist/Surchrg,Watercraft Underwriting Questions,Watercraft Dist/Surchrg";
			}
			else
			{
				TabCtl.TabURLs = "PolicyHomeownerGeneralInfo.aspx?CalledFrom=" + strCalledFrom 
					+ "&,../../Aspx/PolicyShowDiscountAndSurcharges.aspx";
					

				TabCtl.TabTitles="Underwriting Questions,Dist/Surchrg";

			}
		*/
            if (strCalledFrom.Trim().ToUpper().Equals("MTIME"))
            {
                TabCtl.TabURLs = "../MariTime/PolicyUWQMarine.aspx?CalledFrom=" + strCalledFrom;
                TabCtl.TabTitles = "Marine Cargo Underwriting Questions";
            }
			else if(strCalledFrom.Trim().ToUpper().Equals("HOME"))
			{
				TabCtl.TabURLs = "PolicyHomeownerGeneralInfo.aspx?CalledFrom=" + strCalledFrom.ToUpper() 
					+ "?&,../../Aspx/Watercraft/PolicyAddWatercraftGenInfo.aspx?&transferdata=";

				TabCtl.TabTitles="Fire Underwriting Questions";
			}
			else
			{
				TabCtl.TabURLs = "PolicyHomeownerGeneralInfo.aspx?CalledFrom=" + strCalledFrom;
				TabCtl.TabTitles="Underwriting Questions";

			}
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
