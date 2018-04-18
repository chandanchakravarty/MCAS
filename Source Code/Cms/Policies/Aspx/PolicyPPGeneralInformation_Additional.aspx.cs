/******************************************************************************************
<Author					: - Charles Gomes
<Start Date				: -	12/22/2009 
<End Date				: - 12/23/2009	
<Description			: - This File is used to Add and update Private Passenger Automobile Underwriting Tier Information
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
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
using System.Xml;

using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.Model;
using Cms.BusinessLayer.BlApplication;
using System.Reflection;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Policy;
using Cms.BusinessLayer.BlCommon;

namespace Cms.Policies.Aspx
{	
	public class PolicyPPGeneralInformation_Additional : Cms.Policies.policiesbase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.HyperLink hlkCalandarDate;
	        
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppEffDate;

		protected System.Web.UI.WebControls.RegularExpressionValidator revUNTIER_ASSIGNED_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revUNDERWRITING_TIER;

		protected System.Web.UI.HtmlControls.HtmlTableRow trMessage;
		protected System.Web.UI.WebControls.Panel PanelHide;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;

		protected System.Web.UI.WebControls.Label capUNDERWRITING_TIER;
		protected System.Web.UI.WebControls.Label capUNTIER_ASSIGNED_DATE;
		protected System.Web.UI.WebControls.Label capCAP_INC;
		protected System.Web.UI.WebControls.Label capMAX_RATE_INC;
		protected System.Web.UI.WebControls.Label capCAP_DEC;
		protected System.Web.UI.WebControls.Label capMAX_RATE_DEC;
		protected System.Web.UI.WebControls.Label capCAP_RATE_CHANGE_REL;
		protected System.Web.UI.WebControls.Label capCAP_MIN_MAX_ADJUST;
		protected System.Web.UI.WebControls.Label capACL_PREMIUM;

		protected System.Web.UI.WebControls.TextBox txtUNDERWRITING_TIER;
		protected System.Web.UI.WebControls.TextBox txtUNTIER_ASSIGNED_DATE;
		protected System.Web.UI.WebControls.CheckBox chkCAP_INC;	
		protected System.Web.UI.WebControls.CheckBox chkCAP_DEC;		
		protected System.Web.UI.WebControls.TextBox txtCAP_RATE_CHANGE_REL;
		protected System.Web.UI.WebControls.TextBox txtCAP_MIN_MAX_ADJUST;
		protected System.Web.UI.WebControls.TextBox txtACL_PREMIUM;
		
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;		 	
		#endregion

		#region Local form variables
		string oldXML;
		private string strRowId;
		private string strFormSaved;
		protected Cms.BusinessLayer.BlClient.ClsCustomer objcltCustomer=new Cms.BusinessLayer.BlClient.ClsCustomer();		
		Cms.BusinessLayer.BlApplication.ClsPPGeneralInformation  objPPGeneralInformation ;		
		System.Resources.ResourceManager objResourceMgr;
		#endregion

		#region Set Validators ErrorMessages
	
		private void SetErrorMessages()
		{			
			revUNTIER_ASSIGNED_DATE.ValidationExpression	= aRegExpDate;
			revUNTIER_ASSIGNED_DATE.ErrorMessage			= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");	
			revUNDERWRITING_TIER.ValidationExpression		= aRegExpUnderwritingTier;
			revUNDERWRITING_TIER.ErrorMessage				= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1061");	
		}
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="229_0";			
						
			if((GetCustomerID()!="" && GetCustomerID()!="0") && (GetPolicyID()!="" && GetPolicyID()!="0" && GetPolicyID()!=null) && (GetPolicyVersionID()!="" && GetPolicyVersionID()!="0" && GetPolicyVersionID()!=null))
			{
				hidOldData.Value = ClsPPGeneralInformation.GetPolicyXml_UW_Tier(int.Parse(GetCustomerID()),int.Parse(GetPolicyID()),int.Parse(GetPolicyVersionID()));				
			}
			if (hidOldData.Value == "")
			{
				hidCUSTOMER_ID.Value = "NEW";
			}

			if((GetCustomerID()!="" && GetCustomerID()!="0") && (GetPolicyID()!="" && GetPolicyID()!="0") && (GetPolicyVersionID()!="" && GetPolicyVersionID()!="0"))
			{
				trMessage.Attributes.Add("style","display:inline");    
				cltClientTop.CustomerID = int.Parse(GetCustomerID());
				cltClientTop.PolicyID = GetPolicyID() == "" ? 0 : int.Parse(GetPolicyID());
				cltClientTop.PolicyVersionID = GetPolicyVersionID() == "" ? 0 : int.Parse(GetPolicyVersionID());
            
				cltClientTop.ShowHeaderBand ="Policy";
				cltClientTop.Visible = true;        
			}
			else
			{
				trMessage.Attributes.Add("style","display:none");  
				capMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("109");      
				capMessage.Visible=true; 
				cltClientTop.Visible = false;
			}

			lblMessage.Visible = false;
			SetErrorMessages();

			btnReset.CmsButtonClass			=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass			=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;
			
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.PolicyPPGeneralInformation_Additional" ,Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				hidAppEffDate.Value = new clsWatercraftInformation().GetPolEffectiveDate(Convert.ToInt32(GetCustomerID()),Convert.ToInt32(GetPolicyID()),Convert.ToInt32(GetPolicyVersionID())).Trim(); 

				btnReset.Attributes.Add("onclick","javascript:ResetForm('POL_UNDERWRITING_TIER');return false;");			
				hlkCalandarDate.Attributes.Add("OnClick","fPopCalendar(document.POL_UNDERWRITING_TIER.txtUNTIER_ASSIGNED_DATE,document.POL_UNDERWRITING_TIER.txtUNTIER_ASSIGNED_DATE)");
				txtACL_PREMIUM.Attributes.Add("onkeyup","javascript:if(event.keyCode==13){this.value=formatCurrency(this.value)}");
				
				SetCaptions();
			
			}
			SetWorkFlowControl();
		}

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsUnderwritingTierInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsUnderwritingTierInfo objPPGeneralInformation_AdditionalInfo = new ClsUnderwritingTierInfo();		
		
			if(txtUNDERWRITING_TIER.Text.Trim()!= "")
				objPPGeneralInformation_AdditionalInfo.UNDERWRITING_TIER				= txtUNDERWRITING_TIER.Text.Trim().ToUpper();
			if(txtUNTIER_ASSIGNED_DATE.Text.Trim()!= "")
				objPPGeneralInformation_AdditionalInfo.UNTIER_ASSIGNED_DATE			= Convert.ToDateTime(txtUNTIER_ASSIGNED_DATE.Text.Trim());	
			
			if(PanelHide.Visible)
			{
				if(chkCAP_INC.Checked)
					objPPGeneralInformation_AdditionalInfo.CAP_INC							= "Y";
				else
					objPPGeneralInformation_AdditionalInfo.CAP_INC							= "N";
				if(chkCAP_DEC.Checked)
					objPPGeneralInformation_AdditionalInfo.CAP_DEC							= "Y";
				else
					objPPGeneralInformation_AdditionalInfo.CAP_DEC							= "N";

				if(txtCAP_RATE_CHANGE_REL.Text.Trim()!= "")
					objPPGeneralInformation_AdditionalInfo.CAP_RATE_CHANGE_REL				= double.Parse(txtCAP_RATE_CHANGE_REL.Text.Trim());
				if(txtCAP_MIN_MAX_ADJUST.Text.Trim()!= "")
					objPPGeneralInformation_AdditionalInfo.CAP_MIN_MAX_ADJUST				= double.Parse(txtCAP_MIN_MAX_ADJUST.Text.Trim());
				if(txtACL_PREMIUM.Text.Trim()!= "")
					objPPGeneralInformation_AdditionalInfo.ACL_PREMIUM						= double.Parse(txtACL_PREMIUM.Text.Trim());			
			}
	
			objPPGeneralInformation_AdditionalInfo.CUSTOMER_ID							=	GetCustomerID() == "" ? 0 : int.Parse(GetCustomerID()) ;
			objPPGeneralInformation_AdditionalInfo.POLICY_ID							=	GetPolicyID() == "" ? 0 : int.Parse(GetPolicyID()) ;
			objPPGeneralInformation_AdditionalInfo.POLICY_VERSION_ID					=	GetPolicyVersionID()== "" ? 0 : int.Parse(GetPolicyVersionID()) ;
   
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidCUSTOMER_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objPPGeneralInformation_AdditionalInfo;
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{			
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
		}
		#endregion

		#region "Web Event Handlers"
		/// <summary>
		/// If form is posted back then add entry in database using the BL object
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	
				objPPGeneralInformation = new  ClsPPGeneralInformation();
				
				ClsUnderwritingTierInfo objPPGeneralInformation_AdditionalInfo = GetFormValue();

				if(hidOldData.Value=="0" || hidOldData.Value =="")
				{
					objPPGeneralInformation_AdditionalInfo.CREATED_BY = int.Parse(GetUserId());
					objPPGeneralInformation_AdditionalInfo.IS_ACTIVE ="Y";
					objPPGeneralInformation_AdditionalInfo.CREATED_DATETIME = DateTime.Now;
					
					intRetVal = objPPGeneralInformation.Add(objPPGeneralInformation_AdditionalInfo);

					if(intRetVal>0)
					{						
						lblMessage.Text			=	ClsMessages.FetchGeneralMessage("29");
						hidFormSaved.Value			=	"1";
						hidOldData.Value = ClsPPGeneralInformation.GetPolicyXml_UW_Tier(objPPGeneralInformation_AdditionalInfo.CUSTOMER_ID,objPPGeneralInformation_AdditionalInfo.POLICY_ID ,objPPGeneralInformation_AdditionalInfo.POLICY_VERSION_ID);
						hidCUSTOMER_ID.Value=objPPGeneralInformation_AdditionalInfo.CUSTOMER_ID.ToString() ;
						strRowId=hidCUSTOMER_ID.Value ;
						hidIS_ACTIVE.Value = "Y";
						SetWorkFlowControl();

						base.OpenEndorsementDetails();
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.FetchGeneralMessage("18");
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.FetchGeneralMessage("20");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				} 
				else //UPDATE CASE
				{
					ClsUnderwritingTierInfo objOldPPGeneralInformation_AdditionalInfo = new ClsUnderwritingTierInfo();
					
					base.PopulateModelObject(objOldPPGeneralInformation_AdditionalInfo,hidOldData.Value);
										
					objPPGeneralInformation_AdditionalInfo.MODIFIED_BY = int.Parse(GetUserId());
					objPPGeneralInformation_AdditionalInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					objPPGeneralInformation_AdditionalInfo.IS_ACTIVE = hidIS_ACTIVE.Value;
					
					intRetVal	= objPPGeneralInformation.Update(objOldPPGeneralInformation_AdditionalInfo,objPPGeneralInformation_AdditionalInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	ClsMessages.FetchGeneralMessage("31");
						hidFormSaved.Value		=	"1";
						
						hidOldData.Value = ClsPPGeneralInformation.GetPolicyXml_UW_Tier(objPPGeneralInformation_AdditionalInfo.CUSTOMER_ID,objPPGeneralInformation_AdditionalInfo.POLICY_ID ,objPPGeneralInformation_AdditionalInfo.POLICY_VERSION_ID);
						SetWorkFlowControl();

						base.OpenEndorsementDetails();
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	ClsMessages.FetchGeneralMessage("18");
						hidFormSaved.Value		=	"1";
					}
					else 
					{
						lblMessage.Text			=	ClsMessages.FetchGeneralMessage("20");
						hidFormSaved.Value		=	"1";
					}
					lblMessage.Visible = true;
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value	= "2";
				
			}
			finally												
			{
				if(objPPGeneralInformation!= null)
					objPPGeneralInformation.Dispose();
			}
		}
	
		#endregion

		/// <summary>
		/// This Function is used to Set the Captions of the label, from Resource Manager
		/// </summary>
		private void SetCaptions()
		{
			capUNDERWRITING_TIER.Text		=	objResourceMgr.GetString("txtUNDERWRITING_TIER");
			capUNTIER_ASSIGNED_DATE.Text	=	objResourceMgr.GetString("txtUNTIER_ASSIGNED_DATE");
			capCAP_INC.Text					=	objResourceMgr.GetString("chkCAP_INC");
			capMAX_RATE_INC.Text			=	objResourceMgr.GetString("txtMAX_RATE_INC");
			capCAP_DEC.Text					=	objResourceMgr.GetString("chkCAP_DEC");
			capMAX_RATE_DEC.Text			=	objResourceMgr.GetString("txtMAX_RATE_DEC");
			capCAP_RATE_CHANGE_REL.Text		=	objResourceMgr.GetString("txtCAP_RATE_CHANGE_REL");
			capCAP_MIN_MAX_ADJUST.Text		=	objResourceMgr.GetString("txtCAP_MIN_MAX_ADJUST");
			capACL_PREMIUM.Text				=	objResourceMgr.GetString("txtACL_PREMIUM");																
		}		
	
		private void SetWorkFlowControl()
		{
			myWorkFlow.ScreenID	=	base.ScreenId;				
			myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
			myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
			myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
			myWorkFlow.GetScreenStatus();
			myWorkFlow.SetScreenStatus();
			myWorkFlow.WorkflowModule="POL";
		}		

		
	}
}
