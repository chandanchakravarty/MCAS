/******************************************************************************************
	<Author					: - > Sumit Chhabra
	<Start Date				: -	> May 10,2006
	<End Date				: - >
	<Description			: - > Page is used to attach claims to policies
	<Review Date			: - >
	<Reviewed By			: - >
	
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
using Cms.CmsWeb;
using System.Resources; 
using System.Reflection; 
using Cms.Model.Maintenance.Claims;
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 
using Cms.BusinessLayer.BLClaims;




namespace Cms.Claims.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class MatchClaims : Cms.Claims.ClaimBase
	{
		#region Page controls declaration
		
		
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************		
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;		
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label capREASSIGN_CLAIM_TO;
		protected System.Web.UI.WebControls.TextBox txtREASSIGN_CLAIM_TO;
		protected Cms.CmsWeb.Controls.CmsButton btnReassignNow;
		protected Cms.CmsWeb.Controls.CmsButton btnSearchPolicy;
		protected Cms.CmsWeb.Controls.CmsButton btnBack;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMessage;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMATCHED;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCONTINUE_WITH_LOB;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDUMMY_CLAIM_NUMBER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREASSIGN_CLAIM_TO;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnREASSIGN_CLAIM_TO;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDUMMY_POLICY_ID;	
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_NUMBER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidBackFromPolicy;
		

		#endregion
	

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{						
			base.ScreenId="300_0";
			
			lblMessage.Visible = false;
			

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReassignNow.CmsButtonClass	=	CmsButtonType.Write;
			btnReassignNow.PermissionString		=	gstrSecurityXML;

			btnSearchPolicy.CmsButtonClass	=	CmsButtonType.Write;
			btnSearchPolicy.PermissionString		=	gstrSecurityXML;			
			
			btnBack.CmsButtonClass	=	CmsButtonType.Write;
			btnBack.PermissionString		=	gstrSecurityXML;			
			
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.MatchClaims" ,System.Reflection.Assembly.GetExecutingAssembly());
			
			//Reassign again,continuing with different LOB
			if (Request.Form["__EVENTTARGET"] == "CONTINUE")
			{   
				btnReassignNow_Click(null,null);
				return;
			}

			if(!Page.IsPostBack)
			{				
				btnReassignNow.Attributes.Add("onClick","javascript:return ConfirmationMessage();");
				btnSearchPolicy.Attributes.Add("onClick","javascript:return RedirectToSearchPolicy();");				
				btnBack.Attributes.Add("onClick","javascript:return GoBack();");
				GetQueryStringValues();
				SetCaptions();	
				SetErrorMessages();
				SetPolicyNumber();
				CheckUserAuthorityLevel();
			}
		}
		#endregion	

		#region Check Authority Level of the current user and set buttons accordingly
		private void CheckUserAuthorityLevel()
		{
			bool dtAuthority = ClsMatchClaims.GetUserAuthorityLevel(GetUserId(),hidDUMMY_POLICY_ID.Value);
			if(!dtAuthority)
			{
				lblMessage.Text			=	ClsMessages.FetchGeneralMessage("826");		
				lblMessage.Visible		=	true;
				btnReassignNow.Visible = false;
			}
			else
				btnReassignNow.Visible = true;


	}
		#endregion

		#region Set Policy Number as selected from the Search Policy Page
		private void SetPolicyNumber()
		{
			if(hidBackFromPolicy.Value=="1")
				txtREASSIGN_CLAIM_TO.Text = hidPOLICY_NUMBER.Value.Trim();
		}
	#endregion

		#region Get Query String Values
		private void GetQueryStringValues()
		{
			if(Request.QueryString["DUMMY_POLICY_ID"]!=null && Request.QueryString["DUMMY_POLICY_ID"].ToString()!="")
				hidDUMMY_POLICY_ID.Value = Request.QueryString["DUMMY_POLICY_ID"].ToString();

			if(Request.QueryString["CUSTOMER_ID"]!=null && Request.QueryString["CUSTOMER_ID"].ToString()!="" )
				hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();

			if(Request.QueryString["POLICY_ID"]!=null && Request.QueryString["POLICY_ID"].ToString()!="" )
				hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].ToString();

			if(Request.QueryString["POLICY_VERSION_ID"]!=null && Request.QueryString["POLICY_VERSION_ID"].ToString()!="" )
				hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();
			
			if(Request.QueryString["POLICY_NUMBER"]!=null && Request.QueryString["POLICY_NUMBER"].ToString()!="" )
				hidPOLICY_NUMBER.Value = Request.QueryString["POLICY_NUMBER"].ToString();
			else
				hidPOLICY_NUMBER.Value = "";
			if(Request.QueryString["BackFromPolicy"]!=null && Request.QueryString["BackFromPolicy"].ToString()!="" )
				hidBackFromPolicy.Value = Request.QueryString["BackFromPolicy"].ToString();
			else
				hidBackFromPolicy.Value = "";
			

			if(Request.QueryString["DUMMY_CLAIM_NUMBER"]!=null && Request.QueryString["DUMMY_CLAIM_NUMBER"].ToString()!="" )
				hidDUMMY_CLAIM_NUMBER.Value = Request.QueryString["DUMMY_CLAIM_NUMBER"].ToString();
			else
				hidDUMMY_CLAIM_NUMBER.Value = "";

			if(Request.QueryString["CLAIM_ID"]!=null && Request.QueryString["CLAIM_ID"].ToString()!="" )
				hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();
			else
				hidCLAIM_ID.Value = "";

			if(Request.QueryString["LOB_ID"]!=null && Request.QueryString["LOB_ID"].ToString()!="" )
				hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();
			else
				hidLOB_ID.Value = "";
			
			
		}
		#endregion
	
		#region Set Captions for labels on the page
		private void SetCaptions()
		{
			capREASSIGN_CLAIM_TO.Text					=		objResourceMgr.GetString("txtREASSIGN_CLAIM_TO");
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{			
			this.btnReassignNow.Click += new System.EventHandler(this.btnReassignNow_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Set Error Messages for validators on the page
		private void SetErrorMessages()
		{
			rfvREASSIGN_CLAIM_TO.ErrorMessage						=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("34");			
		}
		#endregion

		private void SetStartUpScript(string FunctionName)
		{
			
			ClientScript.RegisterStartupScript(this.GetType(),"StartUpScript","<script>" + FunctionName + "();</script>");			
		}

		#region Set Session Values
		private void SetSessionValues()
		{
			if(hidCUSTOMER_ID.Value!="" && hidCUSTOMER_ID.Value!="0")
				SetCustomerID(hidCUSTOMER_ID.Value);
			if(hidPOLICY_ID.Value!="" && hidPOLICY_ID.Value!="0")
				SetPolicyID(hidPOLICY_ID.Value);
			if(hidPOLICY_VERSION_ID.Value!="" && hidPOLICY_VERSION_ID.Value!="0")
				SetPolicyVersionID(hidPOLICY_VERSION_ID.Value);
			if(hidCLAIM_ID.Value!="" && hidCLAIM_ID.Value!="0")
				SetClaimID(hidCLAIM_ID.Value);
			else
				SetClaimID("");
			
			

		}
		#endregion

		private void btnReassignNow_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	
				//For retreiving the return value of business class save function
				ClsMatchClaims objMatchClaims = new ClsMatchClaims();

				//Submit the current policy to be attached to the current dummy claim id								
				
				//Calling the add method of business layer class
				intRetVal = objMatchClaims.AssignClaimToPolicy(hidCUSTOMER_ID.Value,hidPOLICY_ID.Value,hidPOLICY_VERSION_ID.Value,hidDUMMY_POLICY_ID.Value,hidCLAIM_ID.Value,hidCONTINUE_WITH_LOB.Value);
				if(intRetVal>0)
				{			
					hidMATCHED.Value = "1";
					hidPOLICY_VERSION_ID.Value = intRetVal.ToString();
					SetSessionValues();
					lblMessage.Text			=	"Claim Number " + hidDUMMY_CLAIM_NUMBER.Value + " has been successfully attached to the system policy " + hidPOLICY_NUMBER.Value;
					btnReassignNow.Visible = false;
					btnSearchPolicy.Visible = false;
					lblMessage.Visible = true;
				}					
				else if(intRetVal==-1) //LOB of policy does not match dummy policy LOB
				{
					SetStartUpScript("LOBConfirmationMessage");
				}
				else if(intRetVal==-2) //loss date does not match with any version of policy
				{
					//lblMessage.Text			=	ClsMessages.FetchGeneralMessage("760");					
					hidMessage.Value  			=	ClsMessages.FetchGeneralMessage("827");					
					SetStartUpScript("AlertMessageForLossDate");
				}
				else 
				{
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"21");
					lblMessage.Visible = true;
				}	
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);				
			    
			}
			finally
			{
				//				if(objAgency!= null)
				//					objAgency.Dispose();
			}
		
		}

		
	}
}
