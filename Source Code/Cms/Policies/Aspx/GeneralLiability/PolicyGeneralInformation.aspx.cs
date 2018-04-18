/******************************************************************************************
<Author					: - Sumit Chhabra
<Start Date				: -	03/05/2006
<End Date				: -	
<Description				: - 	This file is used for Policy Gen Lib Underwritting Question
<Review Date				: - 
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
using Cms.Model.Policy.GeneralLiability;
using Cms.BusinessLayer.BlApplication.GeneralLiability;
using Cms.CmsWeb.Controls;
using Cms.CmsWeb.WebControls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb;
namespace Cms.Policies.Aspx.GeneralLiability
{
	/// <summary>
	/// 
	/// </summary>
	public class PolicyGeneralInformation : Cms.Policies.policiesbase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.DropDownList cmbINSURANCE_DECLINED_FIVE_YEARS;
		protected System.Web.UI.WebControls.DropDownList cmbMEDICAL_PROFESSIONAL_EMPLOYEED;
		protected System.Web.UI.WebControls.DropDownList cmbEXPOSURE_RATIOACTIVE_NUCLEAR;
		protected System.Web.UI.WebControls.DropDownList cmbHAVE_PAST_PRESENT_OPERATIONS;
		protected System.Web.UI.WebControls.DropDownList cmbANY_OPERATIONS_SOLD;
		protected System.Web.UI.WebControls.DropDownList cmbMACHINERY_LOANED;
		protected System.Web.UI.WebControls.DropDownList cmbANY_WATERCRAFT_LEASED;
		protected System.Web.UI.WebControls.DropDownList cmbANY_PARKING_OWNED;
		protected System.Web.UI.WebControls.DropDownList cmbFEE_CHARGED_PARKING;
		protected System.Web.UI.WebControls.DropDownList cmbRECREATION_PROVIDED;
		protected System.Web.UI.WebControls.DropDownList cmbSWIMMING_POOL_PREMISES;
		protected System.Web.UI.WebControls.DropDownList cmbSPORTING_EVENT_SPONSORED;
		protected System.Web.UI.WebControls.DropDownList cmbSTRUCTURAL_ALTERATION_CONTEMPATED;
		protected System.Web.UI.WebControls.DropDownList cmbDEMOLITION_EXPOSURE_CONTEMPLATED;
		protected System.Web.UI.WebControls.DropDownList cmbCUSTOMER_ACTIVE_JOINT_VENTURES;
		protected System.Web.UI.WebControls.DropDownList cmbLEASE_EMPLOYEE;
		protected System.Web.UI.WebControls.DropDownList cmbLABOR_INTERCHANGE_OTH_BUSINESS;
		protected System.Web.UI.WebControls.DropDownList cmbDAY_CARE_FACILITIES;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;

		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;

		//protected System.Web.UI.WebControls.Label capHAVE_PAST_PRESENT_OPERATIONS;

		protected System.Web.UI.WebControls.Label capINSURANCE_DECLINED_FIVE_YEARS;
		protected System.Web.UI.WebControls.Label capMEDICAL_PROFESSIONAL_EMPLOYEED;
		protected System.Web.UI.WebControls.Label capEXPOSURE_RATIOACTIVE_NUCLEAR;
		protected System.Web.UI.WebControls.Label capHAVE_PAST_PRESENT_OPERATIONS;
		protected System.Web.UI.WebControls.Label capANY_OPERATIONS_SOLD;
		protected System.Web.UI.WebControls.Label capMACHINERY_LOANED;
		protected System.Web.UI.WebControls.Label capANY_WATERCRAFT_LEASED;
		protected System.Web.UI.WebControls.Label capANY_PARKING_OWNED;
		protected System.Web.UI.WebControls.Label capFEE_CHARGED_PARKING;
		protected System.Web.UI.WebControls.Label capRECREATION_PROVIDED;
		protected System.Web.UI.WebControls.Label capSWIMMING_POOL_PREMISES;
		protected System.Web.UI.WebControls.Label capSPORTING_EVENT_SPONSORED;
		protected System.Web.UI.WebControls.Label capSTRUCTURAL_ALTERATION_CONTEMPATED;
		protected System.Web.UI.WebControls.Label capDEMOLITION_EXPOSURE_CONTEMPLATED;
		protected System.Web.UI.WebControls.Label capCUSTOMER_ACTIVE_JOINT_VENTURES;
		protected System.Web.UI.WebControls.Label capLEASE_EMPLOYEE;
		protected System.Web.UI.WebControls.Label capLABOR_INTERCHANGE_OTH_BUSINESS;
		protected System.Web.UI.WebControls.Label capDAY_CARE_FACILITIES;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINSURANCE_DECLINED_FIVE_YEARS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMEDICAL_PROFESSIONAL_EMPLOYEED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXPOSURE_RATIOACTIVE_NUCLEAR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHAVE_PAST_PRESENT_OPERATIONS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_OPERATIONS_SOLD;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMACHINERY_LOANED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_WATERCRAFT_LEASED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_PARKING_OWNED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFEE_CHARGED_PARKING;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRECREATION_PROVIDED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSWIMMING_POOL_PREMISES;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSPORTING_EVENT_SPONSORED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTRUCTURAL_ALTERATION_CONTEMPATED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDEMOLITION_EXPOSURE_CONTEMPLATED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCUSTOMER_ACTIVE_JOINT_VENTURES;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLEASE_EMPLOYEE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLABOR_INTERCHANGE_OTH_BUSINESS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDAY_CARE_FACILITIES;

		protected System.Web.UI.WebControls.Label lblMessage;

		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		protected System.Web.UI.HtmlControls.HtmlForm POLICY_GENERAL_UNDERWRITING_INFO;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtADDITIONAL_COMMENTS;
		protected System.Web.UI.WebControls.Label capADDITIONAL_COMMENTS;
		protected System.Web.UI.WebControls.CustomValidator csvADDITIONAL_COMMENTS;
		protected System.Web.UI.WebControls.TextBox txtDESC_INSURANCE_DECLINED;
		protected System.Web.UI.WebControls.Label lblDESC_INSURANCE_DECLINED;
		protected System.Web.UI.WebControls.Label capDESC_MEDICAL_PROFESSIONAL;
		protected System.Web.UI.WebControls.TextBox txtDESC_MEDICAL_PROFESSIONAL;
		protected System.Web.UI.WebControls.Label lblDESC_MEDICAL_PROFESSIONAL;
		protected System.Web.UI.WebControls.Label capDESC_EXPOSURE_RATIOACTIVE;
		protected System.Web.UI.WebControls.TextBox txtDESC_EXPOSURE_RATIOACTIVE;
		protected System.Web.UI.WebControls.Label lblDESC_EXPOSURE_RATIOACTIVE;
		protected System.Web.UI.WebControls.Label capDESC_HAVE_PAST_PRESENT;
		protected System.Web.UI.WebControls.TextBox txtDESC_HAVE_PAST_PRESENT;
		protected System.Web.UI.WebControls.Label lblDESC_HAVE_PAST_PRESENT;
		protected System.Web.UI.WebControls.Label capDESC_ANY_OPERATIONS;
		protected System.Web.UI.WebControls.TextBox txtDESC_ANY_OPERATIONS;
		protected System.Web.UI.WebControls.Label lblDESC_ANY_OPERATIONS;
		protected System.Web.UI.WebControls.Label capDESC_MACHINERY_LOANED;
		protected System.Web.UI.WebControls.TextBox txtDESC_MACHINERY_LOANED;
		protected System.Web.UI.WebControls.Label lblDESC_MACHINERY_LOANED;
		protected System.Web.UI.WebControls.Label capDESC_ANY_WATERCRAFT;
		protected System.Web.UI.WebControls.Label capDESC_ANY_PARKING;
		protected System.Web.UI.WebControls.TextBox txtDESC_ANY_PARKING;
		protected System.Web.UI.WebControls.Label lblDESC_ANY_PARKING;
		protected System.Web.UI.WebControls.Label capDESC_FEE_CHARGED;
		protected System.Web.UI.WebControls.TextBox txtDESC_FEE_CHARGED;
		protected System.Web.UI.WebControls.Label lblDESC_FEE_CHARGED;
		protected System.Web.UI.WebControls.Label capDESC_RECREATION_PROVIDED;
		protected System.Web.UI.WebControls.TextBox txtDESC_RECREATION_PROVIDED;
		protected System.Web.UI.WebControls.Label lblDESC_RECREATION_PROVIDED;
		protected System.Web.UI.WebControls.Label capDESC_SWIMMING_POOL;
		protected System.Web.UI.WebControls.TextBox txtDESC_SWIMMING_POOL;
		protected System.Web.UI.WebControls.Label lblDESC_SWIMMING_POOL;
		protected System.Web.UI.WebControls.Label capDESC_SPORTING_EVENT;
		protected System.Web.UI.WebControls.TextBox txtDESC_SPORTING_EVENT;
		protected System.Web.UI.WebControls.Label lblDESC_SPORTING_EVENT;
		protected System.Web.UI.WebControls.Label capDESC_STRUCTURAL_ALTERATION;
		protected System.Web.UI.WebControls.TextBox txtDESC_STRUCTURAL_ALTERATION;
		protected System.Web.UI.WebControls.Label lblDESC_STRUCTURAL_ALTERATION;
		protected System.Web.UI.WebControls.Label capDESC_DEMOLITION_EXPOSURE;
		protected System.Web.UI.WebControls.TextBox txtDESC_DEMOLITION_EXPOSURE;
		protected System.Web.UI.WebControls.Label lblDESC_DEMOLITION_EXPOSURE;
		protected System.Web.UI.WebControls.Label capDESC_CUSTOMER_ACTIVE;
		protected System.Web.UI.WebControls.TextBox txtDESC_CUSTOMER_ACTIVE;
		protected System.Web.UI.WebControls.Label lblDESC_CUSTOMER_ACTIVE;
		protected System.Web.UI.WebControls.Label capDESC_LEASE_EMPLOYEE;
		protected System.Web.UI.WebControls.TextBox txtDESC_LEASE_EMPLOYEE;
		protected System.Web.UI.WebControls.Label lblDESC_LEASE_EMPLOYEE;
		protected System.Web.UI.WebControls.Label capDESC_LABOR_INTERCHANGE;
		protected System.Web.UI.WebControls.TextBox txtDESC_LABOR_INTERCHANGE;
		protected System.Web.UI.WebControls.Label lblDESC_LABOR_INTERCHANGE;
		protected System.Web.UI.WebControls.Label capDESC_DAY_CARE;
		protected System.Web.UI.WebControls.TextBox txtDESC_DAY_CARE;
		protected System.Web.UI.WebControls.Label lblDESC_DAY_CARE;
		protected System.Web.UI.WebControls.Label capDESC_INSURANCE_DECLINED;
		protected System.Web.UI.WebControls.TextBox txtDESC_ANY_WATERCRAFT;
		protected System.Web.UI.WebControls.Label lblDESC_ANY_WATERCRAFT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_INSURANCE_DECLINED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_MEDICAL_PROFESSIONAL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_EXPOSURE_RATIOACTIVE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_HAVE_PAST_PRESENT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_ANY_OPERATIONS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_MACHINERY_LOANED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_ANY_WATERCRAFT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_ANY_PARKING;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_FEE_CHARGED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_RECREATION_PROVIDED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_SWIMMING_POOL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_SPORTING_EVENT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_STRUCTURAL_ALTERATION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_DEMOLITION_EXPOSURE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_CUSTOMER_ACTIVE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_LEASE_EMPLOYEE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_LABOR_INTERCHANGE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_DAY_CARE;
		protected System.Web.UI.HtmlControls.HtmlForm POL_GENERAL_UNDERWRITING_INFO;
		//Defining the business layer class object
		ClsGeneralUnderwritingInformation  objGeneralInformation ;
		//END:*********** Local variables *************

		#endregion
		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			csvADDITIONAL_COMMENTS.ErrorMessage			 =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("91");
			//rfvANY_NON_OWNED_VEH.ErrorMessage		     =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			rfvDESC_ANY_OPERATIONS.ErrorMessage          =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvDESC_ANY_PARKING.ErrorMessage             =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			rfvDESC_ANY_WATERCRAFT.ErrorMessage          =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			rfvDESC_CUSTOMER_ACTIVE.ErrorMessage         =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"13");
			rfvDESC_DAY_CARE.ErrorMessage				 =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"16");
			rfvDESC_DEMOLITION_EXPOSURE.ErrorMessage     =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			rfvDESC_EXPOSURE_RATIOACTIVE.ErrorMessage    =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvDESC_FEE_CHARGED.ErrorMessage             =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			rfvDESC_HAVE_PAST_PRESENT.ErrorMessage       =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvDESC_INSURANCE_DECLINED.ErrorMessage      =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("560");
			rfvDESC_LABOR_INTERCHANGE.ErrorMessage       =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"15");
			rfvDESC_LEASE_EMPLOYEE.ErrorMessage          =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"14");
			rfvDESC_MACHINERY_LOANED.ErrorMessage        =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			rfvDESC_MEDICAL_PROFESSIONAL.ErrorMessage    =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvDESC_RECREATION_PROVIDED.ErrorMessage     =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"8");
			rfvDESC_SPORTING_EVENT.ErrorMessage          =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"10");
			rfvDESC_STRUCTURAL_ALTERATION.ErrorMessage   =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"11");
			rfvDESC_SWIMMING_POOL.ErrorMessage           =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");
		
		}
		#endregion
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnReset.Attributes.Add("onclick","javascript: ResetForm('POLICY_GENERAL_UNDERWRITING_INFO');ResetControls(); return false;");

//			string strCustomerId = GetCustomerID();
//			if (strCustomerId != null && strCustomerId.Trim() != "")
//			{
//				ShowClientTopControl(int.Parse(strCustomerId));
//				
//			}
//			else
//			{
//				cltClientTop.Visible = false;
//				
//
//			}

			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			//Temporary screen id given
			base.ScreenId="284";
			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write ;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.GeneralLiability.PolicyGeneralInformation" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				GetSessionValues();				
				GetOldDataXML();
				SetCaptions();
				#region set Workflow cntrol
				SetWorkFlow();
				#endregion
				#region "Loading singleton"
				#endregion//Loading singleton
			}
			ShowClientTopControl();
		}//end pageload
		#endregion

		private void GetSessionValues()
		{
			hidCUSTOMER_ID.Value = GetCustomerID();
			hidPOLICY_ID.Value = GetPolicyID();
			hidPOLICY_VERSION_ID.Value = GetPolicyVersionID();
		}


		/// <summary>
		/// validate posted data from form
		/// </summary>
		/// <returns>True if posted data is valid else false</returns>
		private bool doValidationCheck()
		{
			try
			{
				return true;
			}
			catch (Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				return false;
			}
		}

		private void ShowClientTopControl()
		{
			if (hidCUSTOMER_ID.Value != "0" && hidCUSTOMER_ID.Value!="")
			{				
				cltClientTop.CustomerID = int.Parse(hidCUSTOMER_ID.Value);
				cltClientTop.PolicyID = int.Parse(hidPOLICY_ID.Value);
				cltClientTop.PolicyVersionID = int.Parse(hidPOLICY_VERSION_ID.Value);
				cltClientTop.ShowHeaderBand = "Policy";				
				cltClientTop.Visible= true;
			}
			else
			{
				cltClientTop.Visible = false;
			}
		}


		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private Cms.Model.Policy.GeneralLiability.ClsGeneralInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			Cms.Model.Policy.GeneralLiability.ClsGeneralInfo objGeneralInfo = new ClsGeneralInfo();
			

			objGeneralInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
			objGeneralInfo.POLICY_ID		= int.Parse(hidPOLICY_ID.Value);
			objGeneralInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
			objGeneralInfo.INSURANCE_DECLINED_FIVE_YEARS=	cmbINSURANCE_DECLINED_FIVE_YEARS.SelectedItem.Value;
			objGeneralInfo.MEDICAL_PROFESSIONAL_EMPLOYEED=	cmbMEDICAL_PROFESSIONAL_EMPLOYEED.SelectedItem.Value;
			objGeneralInfo.EXPOSURE_RATIOACTIVE_NUCLEAR=	cmbEXPOSURE_RATIOACTIVE_NUCLEAR.SelectedItem.Value;
			objGeneralInfo.HAVE_PAST_PRESENT_OPERATIONS=	cmbHAVE_PAST_PRESENT_OPERATIONS.SelectedItem.Value;
			objGeneralInfo.ANY_OPERATIONS_SOLD=	cmbANY_OPERATIONS_SOLD.SelectedItem.Value;
			objGeneralInfo.MACHINERY_LOANED=	cmbMACHINERY_LOANED.SelectedItem.Value;
			objGeneralInfo.ANY_WATERCRAFT_LEASED=	cmbANY_WATERCRAFT_LEASED.SelectedItem.Value;
			objGeneralInfo.ANY_PARKING_OWNED=	cmbANY_PARKING_OWNED.SelectedItem.Value;
			objGeneralInfo.FEE_CHARGED_PARKING=	cmbFEE_CHARGED_PARKING.SelectedItem.Value;
			objGeneralInfo.RECREATION_PROVIDED=	cmbRECREATION_PROVIDED.SelectedItem.Value;
			objGeneralInfo.SWIMMING_POOL_PREMISES=	cmbSWIMMING_POOL_PREMISES.SelectedItem.Value;
			objGeneralInfo.SPORTING_EVENT_SPONSORED=	cmbSPORTING_EVENT_SPONSORED.SelectedItem.Value;
			objGeneralInfo.STRUCTURAL_ALTERATION_CONTEMPATED=	cmbSTRUCTURAL_ALTERATION_CONTEMPATED.SelectedItem.Value;
			objGeneralInfo.DEMOLITION_EXPOSURE_CONTEMPLATED=	cmbDEMOLITION_EXPOSURE_CONTEMPLATED.SelectedItem.Value;
			objGeneralInfo.CUSTOMER_ACTIVE_JOINT_VENTURES=	cmbCUSTOMER_ACTIVE_JOINT_VENTURES.SelectedItem.Value;
			objGeneralInfo.LEASE_EMPLOYEE=	cmbLEASE_EMPLOYEE.SelectedItem.Value;
			objGeneralInfo.LABOR_INTERCHANGE_OTH_BUSINESS=	cmbLABOR_INTERCHANGE_OTH_BUSINESS.SelectedItem.Value;
			objGeneralInfo.DAY_CARE_FACILITIES=	cmbDAY_CARE_FACILITIES.SelectedItem.Value;
			objGeneralInfo.ADDITIONAL_COMMENTS=	txtADDITIONAL_COMMENTS.Text;
			
			//-----------------------Added by mohit on 11/10/2005---------------------.
			if (cmbINSURANCE_DECLINED_FIVE_YEARS.SelectedItem.Value =="Y")
			{
				objGeneralInfo.DESC_INSURANCE_DECLINED=txtDESC_INSURANCE_DECLINED.Text;				
			}
			else
			{
				txtDESC_INSURANCE_DECLINED.Text="";
				objGeneralInfo.DESC_INSURANCE_DECLINED="";
				
			}

			if (cmbMEDICAL_PROFESSIONAL_EMPLOYEED.SelectedItem.Value == "Y")
			{
				objGeneralInfo.DESC_MEDICAL_PROFESSIONAL=txtDESC_MEDICAL_PROFESSIONAL.Text;
			}
			else
			{
				txtDESC_MEDICAL_PROFESSIONAL.Text="";
				objGeneralInfo.DESC_MEDICAL_PROFESSIONAL="";
			}
			if (cmbEXPOSURE_RATIOACTIVE_NUCLEAR.SelectedItem.Value=="Y")
			{
				objGeneralInfo.DESC_EXPOSURE_RATIOACTIVE=txtDESC_EXPOSURE_RATIOACTIVE.Text;
			}
			else
			{
				txtDESC_EXPOSURE_RATIOACTIVE.Text="";
				objGeneralInfo.DESC_EXPOSURE_RATIOACTIVE="";
			}
			if (cmbHAVE_PAST_PRESENT_OPERATIONS.SelectedItem.Value=="Y")
			{
				objGeneralInfo.DESC_HAVE_PAST_PRESENT=txtDESC_HAVE_PAST_PRESENT.Text;
			}
			else
			{
				txtDESC_HAVE_PAST_PRESENT.Text="";
				objGeneralInfo.DESC_HAVE_PAST_PRESENT="";
			}
			if (cmbANY_OPERATIONS_SOLD.SelectedItem.Value == "Y")
			{
				objGeneralInfo.DESC_ANY_OPERATIONS=txtDESC_ANY_OPERATIONS.Text;
			}
			else
			{
				txtDESC_ANY_OPERATIONS.Text="";
				objGeneralInfo.DESC_ANY_OPERATIONS="";
			}
			if(cmbMACHINERY_LOANED.SelectedItem.Value == "Y")
			{
				objGeneralInfo.DESC_MACHINERY_LOANED=txtDESC_MACHINERY_LOANED.Text;
			}
			else
			{
				txtDESC_MACHINERY_LOANED.Text="";
				objGeneralInfo.DESC_MACHINERY_LOANED="";
			}
			if(cmbANY_WATERCRAFT_LEASED.SelectedItem.Value == "Y")
			{
				objGeneralInfo.DESC_ANY_WATERCRAFT=txtDESC_ANY_WATERCRAFT.Text;
			}
			else
			{
				txtDESC_ANY_WATERCRAFT.Text="";
				objGeneralInfo.DESC_ANY_WATERCRAFT="";
			}

			if(cmbANY_PARKING_OWNED.SelectedItem.Value == "Y")
			{
				objGeneralInfo.DESC_ANY_PARKING=txtDESC_ANY_PARKING.Text;
			}
			else
			{
				txtDESC_ANY_PARKING.Text="";
				objGeneralInfo.DESC_ANY_PARKING="";
			}
			if(cmbFEE_CHARGED_PARKING.SelectedItem.Value == "Y")
			{
				objGeneralInfo.DESC_FEE_CHARGED=txtDESC_FEE_CHARGED.Text;
			}
			else
			{
				txtDESC_FEE_CHARGED.Text="";
				objGeneralInfo.DESC_FEE_CHARGED="";
			}
			if (cmbRECREATION_PROVIDED.SelectedItem.Value == "Y")
			{
				objGeneralInfo.DESC_RECREATION_PROVIDED=txtDESC_RECREATION_PROVIDED.Text;
			}
			else
			{
				txtDESC_RECREATION_PROVIDED.Text="";
				objGeneralInfo.DESC_RECREATION_PROVIDED="";
			}
			if (cmbSWIMMING_POOL_PREMISES.SelectedItem.Value =="Y")
			{
				objGeneralInfo.DESC_SWIMMING_POOL=txtDESC_SWIMMING_POOL.Text;
			}
			else
			{
				txtDESC_SWIMMING_POOL.Text="";
				objGeneralInfo.DESC_SWIMMING_POOL="";
			}
			if (cmbSPORTING_EVENT_SPONSORED.SelectedItem.Value == "Y")
			{
				objGeneralInfo.DESC_SPORTING_EVENT=txtDESC_SPORTING_EVENT.Text;
			}
			else
			{
				txtDESC_SPORTING_EVENT.Text="";
				objGeneralInfo.DESC_SPORTING_EVENT="";
			}
			if(cmbSTRUCTURAL_ALTERATION_CONTEMPATED.SelectedItem.Value == "Y")
			{
				objGeneralInfo.DESC_STRUCTURAL_ALTERATION=txtDESC_STRUCTURAL_ALTERATION.Text;
			}
			else
			{
				txtDESC_STRUCTURAL_ALTERATION.Text="";
				objGeneralInfo.DESC_STRUCTURAL_ALTERATION="";
			}
			if (cmbDEMOLITION_EXPOSURE_CONTEMPLATED.SelectedItem.Value == "Y")
			{
				objGeneralInfo.DESC_DEMOLITION_EXPOSURE=txtDESC_DEMOLITION_EXPOSURE.Text;
			}
			else
			{
				txtDESC_DEMOLITION_EXPOSURE.Text="";
				objGeneralInfo.DESC_DEMOLITION_EXPOSURE="";
			}
				
			if (cmbCUSTOMER_ACTIVE_JOINT_VENTURES.SelectedItem.Value == "Y")
			{
				objGeneralInfo.DESC_CUSTOMER_ACTIVE=txtDESC_CUSTOMER_ACTIVE.Text;
			}
			else
			{	
				txtDESC_CUSTOMER_ACTIVE.Text="";
				objGeneralInfo.DESC_CUSTOMER_ACTIVE="";
			}
			if(cmbLEASE_EMPLOYEE.SelectedItem.Value == "Y")
			{
				objGeneralInfo.DESC_LEASE_EMPLOYEE=txtDESC_LEASE_EMPLOYEE.Text;
			}
			else
			{	
				objGeneralInfo.DESC_LEASE_EMPLOYEE="";
				txtDESC_LEASE_EMPLOYEE.Text="";
			}
			if (cmbLABOR_INTERCHANGE_OTH_BUSINESS.SelectedItem.Value == "Y")
			{
				objGeneralInfo.DESC_LABOR_INTERCHANGE=txtDESC_LABOR_INTERCHANGE.Text;
			}
			else
			{
				txtDESC_LABOR_INTERCHANGE.Text="";
				objGeneralInfo.DESC_LABOR_INTERCHANGE="";
			}
			if (cmbDAY_CARE_FACILITIES.SelectedItem.Value == "Y")
			{
				objGeneralInfo.DESC_DAY_CARE=txtDESC_DAY_CARE.Text;
			}
			else
			{
				txtDESC_DAY_CARE.Text="";
				objGeneralInfo.DESC_DAY_CARE="";
			}	
			
			
			//---------------------------------------End--------------------------------.


			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidCUSTOMER_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objGeneralInfo;
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
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

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
				int intRetVal;	//For retreiving the return value of business class save function
				objGeneralInformation = new  ClsGeneralUnderwritingInformation();

				//Retreiving the form values into model class object
				ClsGeneralInfo objGeneralInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objGeneralInfo.CREATED_BY = int.Parse(GetUserId());
					objGeneralInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objGeneralInformation.AddPolicyGenLib(objGeneralInfo);

					if(intRetVal>0)
					{
						hidCUSTOMER_ID.Value = objGeneralInfo.CUSTOMER_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						GetOldDataXML();
						hidIS_ACTIVE.Value = "Y";
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsGeneralInfo objOldGeneralInfo = new ClsGeneralInfo();
					

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldGeneralInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objGeneralInfo.CUSTOMER_ID = Convert.ToInt32(strRowId);
					objGeneralInfo.MODIFIED_BY = int.Parse(GetUserId());
					objGeneralInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					objGeneralInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

					//Updating the record using business layer class object
					intRetVal	= objGeneralInformation.UpdatePolicyGenLib(objOldGeneralInfo,objGeneralInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML();
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value		=	"1";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"1";
					}
					lblMessage.Visible = true;
				}				
				SetWorkFlow();
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objGeneralInformation!= null)
					objGeneralInformation.Dispose();
			}
		}

		/// <summary>
		/// Activates and deactivates  .
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		
		#endregion
		private void SetCaptions()
		{
			capINSURANCE_DECLINED_FIVE_YEARS.Text						=		objResourceMgr.GetString("cmbINSURANCE_DECLINED_FIVE_YEARS");
			capMEDICAL_PROFESSIONAL_EMPLOYEED.Text						=		objResourceMgr.GetString("cmbMEDICAL_PROFESSIONAL_EMPLOYEED");
			capEXPOSURE_RATIOACTIVE_NUCLEAR.Text						=		objResourceMgr.GetString("cmbEXPOSURE_RATIOACTIVE_NUCLEAR");
			capHAVE_PAST_PRESENT_OPERATIONS.Text						=		objResourceMgr.GetString("cmbHAVE_PAST_PRESENT_OPERATIONS");
			capANY_OPERATIONS_SOLD.Text						=		objResourceMgr.GetString("cmbANY_OPERATIONS_SOLD");
			capMACHINERY_LOANED.Text						=		objResourceMgr.GetString("cmbMACHINERY_LOANED");
			capANY_WATERCRAFT_LEASED.Text						=		objResourceMgr.GetString("cmbANY_WATERCRAFT_LEASED");
			capANY_PARKING_OWNED.Text						=		objResourceMgr.GetString("cmbANY_PARKING_OWNED");
			capFEE_CHARGED_PARKING.Text						=		objResourceMgr.GetString("cmbFEE_CHARGED_PARKING");
			capRECREATION_PROVIDED.Text						=		objResourceMgr.GetString("cmbRECREATION_PROVIDED");
			capSWIMMING_POOL_PREMISES.Text						=		objResourceMgr.GetString("cmbSWIMMING_POOL_PREMISES");
			capSPORTING_EVENT_SPONSORED.Text						=		objResourceMgr.GetString("cmbSPORTING_EVENT_SPONSORED");
			capSTRUCTURAL_ALTERATION_CONTEMPATED.Text						=		objResourceMgr.GetString("cmbSTRUCTURAL_ALTERATION_CONTEMPATED");
			capDEMOLITION_EXPOSURE_CONTEMPLATED.Text						=		objResourceMgr.GetString("cmbDEMOLITION_EXPOSURE_CONTEMPLATED");
			capCUSTOMER_ACTIVE_JOINT_VENTURES.Text						=		objResourceMgr.GetString("cmbCUSTOMER_ACTIVE_JOINT_VENTURES");
			capLEASE_EMPLOYEE.Text						=		objResourceMgr.GetString("cmbLEASE_EMPLOYEE");
			capLABOR_INTERCHANGE_OTH_BUSINESS.Text						=		objResourceMgr.GetString("cmbLABOR_INTERCHANGE_OTH_BUSINESS");
			capDAY_CARE_FACILITIES.Text						=		objResourceMgr.GetString("cmbDAY_CARE_FACILITIES");
			capADDITIONAL_COMMENTS.Text						=		objResourceMgr.GetString("txtADDITIONAL_COMMENTS");
			capDESC_INSURANCE_DECLINED.Text						=		objResourceMgr.GetString("txtDESC_INSURANCE_DECLINED");
			capDESC_MEDICAL_PROFESSIONAL.Text						=		objResourceMgr.GetString("txtDESC_MEDICAL_PROFESSIONAL");
			capDESC_EXPOSURE_RATIOACTIVE.Text						=		objResourceMgr.GetString("txtDESC_EXPOSURE_RATIOACTIVE");
			capDESC_HAVE_PAST_PRESENT.Text						=		objResourceMgr.GetString("txtDESC_HAVE_PAST_PRESENT");
			capDESC_ANY_OPERATIONS.Text						=		objResourceMgr.GetString("txtDESC_ANY_OPERATIONS");
			capDESC_MACHINERY_LOANED.Text						=		objResourceMgr.GetString("txtDESC_MACHINERY_LOANED");
			capDESC_ANY_WATERCRAFT.Text						=		objResourceMgr.GetString("txtDESC_ANY_WATERCRAFT");
			capDESC_ANY_PARKING.Text						=		objResourceMgr.GetString("txtDESC_ANY_PARKING");
			capDESC_FEE_CHARGED.Text						=		objResourceMgr.GetString("txtDESC_FEE_CHARGED");
			capDESC_RECREATION_PROVIDED.Text						=		objResourceMgr.GetString("txtDESC_RECREATION_PROVIDED");
			capDESC_SWIMMING_POOL.Text						=		objResourceMgr.GetString("txtDESC_SWIMMING_POOL");
			capDESC_SPORTING_EVENT.Text						=		objResourceMgr.GetString("txtDESC_SPORTING_EVENT");
			capDESC_STRUCTURAL_ALTERATION.Text						=		objResourceMgr.GetString("txtDESC_STRUCTURAL_ALTERATION");
			capDESC_DEMOLITION_EXPOSURE.Text						=		objResourceMgr.GetString("txtDESC_DEMOLITION_EXPOSURE");
			capDESC_CUSTOMER_ACTIVE.Text						=		objResourceMgr.GetString("txtDESC_CUSTOMER_ACTIVE");
			capDESC_LEASE_EMPLOYEE.Text						=		objResourceMgr.GetString("txtDESC_LEASE_EMPLOYEE");
			capDESC_LABOR_INTERCHANGE.Text						=		objResourceMgr.GetString("txtDESC_LABOR_INTERCHANGE");
			capDESC_DAY_CARE.Text						=		objResourceMgr.GetString("txtDESC_DAY_CARE");
		}
		private void GetOldDataXML()
		{
			hidOldData.Value =ClsGeneralUnderwritingInformation.GetPolicyXmlForControls(Convert.ToInt32(hidCUSTOMER_ID.Value),Convert.ToInt32(hidPOLICY_ID.Value),Convert.ToInt32(hidPOLICY_VERSION_ID.Value));
		}
		private void SetWorkFlow()
		{
			//Temporary screen id given
			if(base.ScreenId == "284")
			{
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",hidCUSTOMER_ID.Value);
				myWorkFlow.AddKeyValue("POLICY_ID",hidPOLICY_ID.Value);
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",hidPOLICY_VERSION_ID.Value);
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
				myWorkFlow.WorkflowModule="POL";
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}		
	}
}
