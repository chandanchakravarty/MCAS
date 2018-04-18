/******************************************************************************************
<Author				: -   Vijay Arora
<Start Date			: -	  08-11-2005
<End Date			: -	 
<Description		: -  Class to Add/Update the UnderWriting Question for Automobile LOB.
<Review Date		: - 
<Reviewed By		: - 	
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
using Cms.Model.Application;
using Cms.BusinessLayer.BlApplication;
using System.Reflection;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Policy;

namespace Cms.Policies.Aspx
{
	/// <summary>
	/// This Class is used to Add and Update General information for Private Passenger Automobile
	/// </summary>
	public class PolicyAutoGeneralInformation : Cms.Policies.policiesbase	
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.DropDownList cmbANY_NON_OWNED_VEH;
		protected System.Web.UI.WebControls.DropDownList cmbCAR_MODIFIED;
		protected System.Web.UI.WebControls.DropDownList cmbEXISTING_DMG;
		protected System.Web.UI.WebControls.DropDownList cmbANY_CAR_AT_SCH;
		protected System.Web.UI.WebControls.DropDownList cmbANY_OTH_AUTO_INSU;
		//protected System.Web.UI.WebControls.DropDownList cmbANY_OTH_INSU_COMP;
		protected System.Web.UI.WebControls.DropDownList cmbH_MEM_IN_MILITARY;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_SUS_REVOKED;
		protected System.Web.UI.WebControls.DropDownList cmbPHY_MENTL_CHALLENGED;
		protected System.Web.UI.WebControls.DropDownList cmbANY_FINANCIAL_RESPONSIBILITY;
		protected System.Web.UI.WebControls.DropDownList cmbINS_AGENCY_TRANSFER;
		protected System.Web.UI.WebControls.DropDownList cmbCOVERAGE_DECLINED;
		protected System.Web.UI.WebControls.DropDownList cmbAGENCY_VEH_INSPECTED;
		protected System.Web.UI.WebControls.DropDownList cmbUSE_AS_TRANSPORT_FEE;
		protected System.Web.UI.WebControls.DropDownList cmbSALVAGE_TITLE;
		protected System.Web.UI.WebControls.DropDownList cmbANY_ANTIQUE_AUTO;
		protected System.Web.UI.WebControls.TextBox txtREMARKS;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_NON_OWNED_VEH;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCAR_MODIFIED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXISTING_DMG;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_CAR_AT_SCH;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_OTH_AUTO_INSU;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_SUS_REVOKED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPHY_MENTL_CHALLENGED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_FINANCIAL_RESPONSIBILITY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINS_AGENCY_TRANSFER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOVERAGE_DECLINED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_VEH_INSPECTED;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSE_AS_TRANSPORT_FEE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSALVAGE_TITLE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_ANTIQUE_AUTO;

		protected System.Web.UI.WebControls.Label capANY_NON_OWNED_VEH;
		protected System.Web.UI.WebControls.Label capCAR_MODIFIED;
		protected System.Web.UI.WebControls.Label capEXISTING_DMG;
		protected System.Web.UI.WebControls.Label capANY_CAR_AT_SCH;
		protected System.Web.UI.WebControls.Label capANY_OTH_AUTO_INSU;
		//protected System.Web.UI.WebControls.Label capANY_OTH_INSU_COMP;
		protected System.Web.UI.WebControls.Label capH_MEM_IN_MILITARY;
		protected System.Web.UI.WebControls.Label capDRIVER_SUS_REVOKED;
		protected System.Web.UI.WebControls.Label capPHY_MENTL_CHALLENGED;
		protected System.Web.UI.WebControls.Label capANY_FINANCIAL_RESPONSIBILITY;
		
		protected System.Web.UI.WebControls.Label capINS_AGENCY_TRANSFER;
		protected System.Web.UI.WebControls.Label capCOVERAGE_DECLINED;
		protected System.Web.UI.WebControls.Label capAGENCY_VEH_INSPECTED;
		protected System.Web.UI.WebControls.Label capUSE_AS_TRANSPORT_FEE;
		protected System.Web.UI.WebControls.Label capSALVAGE_TITLE;
		protected System.Web.UI.WebControls.Label capANY_ANTIQUE_AUTO;
		protected System.Web.UI.WebControls.Label capREMARKS;
		protected System.Web.UI.HtmlControls.HtmlForm APP_AUTO_GEN_INFO;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected System.Web.UI.WebControls.Label capMULTI_POLICY_DISC_APPLIED_PP_DESC;
		protected System.Web.UI.WebControls.DropDownList cmbMULTI_POLICY_DISC_APPLIED;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;

		//Added By Shafi
		protected System.Web.UI.WebControls.Label capYEARS_INSU_WOL;
		protected System.Web.UI.WebControls.Label capYEARS_INSU;
		protected System.Web.UI.WebControls.TextBox txtYEARS_INSU;
		protected System.Web.UI.WebControls.TextBox	txtYEARS_INSU_WOL;
		protected System.Web.UI.WebControls.RegularExpressionValidator revYEARS_INSU;
		protected System.Web.UI.WebControls.RegularExpressionValidator revYEARS_INSU_WOL;
		protected System.Web.UI.WebControls.CustomValidator csvYEARS_INSU_WOL;
		protected System.Web.UI.WebControls.RangeValidator rngYEARS_INSU;
		protected System.Web.UI.WebControls.RangeValidator rngYEARS_INSU_WOL;

		//Added by Swastika on 14th Mar'06 for Pol Iss #96
		//protected System.Web.UI.WebControls.CompareValidator cmpYEARS_INSU_WOL;
		#endregion

		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		
		private string strRowId;			// Holds value of New or Edit mode
		private string strFormSaved;		// Holds value of the Form state whether it is in new , update or failed mode 
		//private int	intLoggedInUserID;		// Holds the Id of the current Logged in user
		//int intCustId;						// Holds the Customer Id
		//int intAppId;						// Holds the Application Id
		//int intAppVersionId;				// Holds the Application Version Id
		//int intPolicyId;					// Holds the Policy ID.
		//int intPolicyVersionId;				// Holds the Policy Version ID.
		
		//Defining the business layer class object
		Cms.BusinessLayer.BlApplication.ClsPPGeneralInformation  objPPGeneralInformation ;
		protected System.Web.UI.HtmlControls.HtmlTableRow trMessage;
		protected System.Web.UI.WebControls.Label capANY_NON_OWNED_VEH_PP_DESC;
		protected System.Web.UI.WebControls.Label capCAR_MODIFIED_DESC;
		protected System.Web.UI.WebControls.TextBox txtCAR_MODIFIED_DESC;
		protected System.Web.UI.WebControls.Label capEXISTING_DMG_PP_DESC;
		protected System.Web.UI.WebControls.TextBox txtEXISTING_DMG_DESC;
		protected System.Web.UI.WebControls.Label capANY_CAR_AT_SCH_DESC;
		protected System.Web.UI.WebControls.TextBox txtANY_CAR_AT_SCH_DESC;
		protected System.Web.UI.WebControls.Label capANY_OTH_AUTO_INSU_DESC;
		protected System.Web.UI.WebControls.TextBox txtANY_OTH_AUTO_INSU_DESC;
		//protected System.Web.UI.WebControls.Label capANY_OTH_INSU_COMP_PP_DESC;
		//protected System.Web.UI.WebControls.TextBox txtANY_OTH_INSU_COMP_PP_DESC;
		protected System.Web.UI.WebControls.Label capH_MEM_IN_MILITARY_DESC;
		protected System.Web.UI.WebControls.Label capDRIVER_SUS_REVOKED_PP_DESC;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_SUS_REVOKED_PP_DESC;
		protected System.Web.UI.WebControls.Label capPHY_MENTL_CHALLENGED_PP_DESC;
		protected System.Web.UI.WebControls.TextBox txtPHY_MENTL_CHALLENGED_PP_DESC;
		protected System.Web.UI.WebControls.Label capANY_FINANCIAL_RESPONSIBILITY_PP_DESC;
		protected System.Web.UI.WebControls.TextBox txtANY_FINANCIAL_RESPONSIBILITY_PP_DESC;
		protected System.Web.UI.WebControls.Label capINS_AGENCY_TRANSFER_PP_DESC;
		protected System.Web.UI.WebControls.TextBox txtINS_AGENCY_TRANSFER_PP_DESC;
		protected System.Web.UI.WebControls.Label capCOVERAGE_DECLINED_PP_DESC;
		protected System.Web.UI.WebControls.TextBox txtCOVERAGE_DECLINED_PP_DESC;
		protected System.Web.UI.WebControls.Label capAGENCY_VEH_INSPECTED_PP_DESC;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_VEH_INSPECTED_PP_DESC;
		protected System.Web.UI.WebControls.Label capUSE_AS_TRANSPORT_FEE_DESC;
		protected System.Web.UI.WebControls.TextBox txtUSE_AS_TRANSPORT_FEE_DESC;
		protected System.Web.UI.WebControls.Label capSALVAGE_TITLE_PP_DESC;
		protected System.Web.UI.WebControls.TextBox txtSALVAGE_TITLE_PP_DESC;
		protected System.Web.UI.WebControls.Label capANY_ANTIQUE_AUTO_DESC;
		protected System.Web.UI.WebControls.TextBox txtANY_ANTIQUE_AUTO_DESC;
		protected System.Web.UI.WebControls.TextBox txtMULTI_POLICY_DISC_APPLIED_PP_DESC;
		protected System.Web.UI.WebControls.Label lblMEMBER_DESC;
		//protected System.Web.UI.WebControls.Label lblOTHER_INSU_DESC;
		protected System.Web.UI.WebControls.Label lblAUTO_INSU_DESC;
		protected System.Web.UI.WebControls.Label lblDAMAGED_DESC;
		protected System.Web.UI.WebControls.Label lblCAR_MOD_DESC;
		protected System.Web.UI.WebControls.Label lblDRIVER_LIC_DESC;
		protected System.Web.UI.WebControls.Label lbldRIVER_IMP_DESC;
		protected System.Web.UI.WebControls.Label lblINSU_TRANS_DESC;
		protected System.Web.UI.WebControls.Label lblMULTIPOLICY_DISC_DESC;
		protected System.Web.UI.WebControls.Label lblCOVERAGE_DESC;
		protected System.Web.UI.WebControls.Label lblDRIVER_NO_DESC;
		protected System.Web.UI.WebControls.Label lblAGENT_INS_DESC;
		protected System.Web.UI.WebControls.Label lblSALVAGE_DESC;
		protected System.Web.UI.WebControls.Label lblTRANSPORT_DESC;
		protected System.Web.UI.WebControls.Label lblVEHICLE_DESC;
		protected System.Web.UI.WebControls.Label lblCAR_DESC;
		protected System.Web.UI.WebControls.TextBox txtANY_NON_OWNED_VEH_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_FINANCIAL_RESPONSIBILITY_PP_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvH_MEM_IN_MILITARY_DESC;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_OTH_INSU_COMP_PP_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPHY_MENTL_CHALLENGED_PP_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINS_AGENCY_TRANSFER_PP_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_OTH_AUTO_INSU_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXISTING_DMG_PP_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_NON_OWNED_VEH_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOVERAGE_DECLINED_PP_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_SUS_REVOKED_PP_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_ANTIQUE_AUTO_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSALVAGE_TITLE_PP_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_VEH_INSPECTED_PP_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMULTI_POLICY_DISC_APPLIED_PP_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_CAR_AT_SCH_DESC;
		protected System.Web.UI.WebControls.Label lblANY_ANTIQUE_AUTO_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCAR_MODIFIED_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSE_AS_TRANSPORT_FEE_DESC;
		protected System.Web.UI.WebControls.CustomValidator  csvREMARKS;
		protected System.Web.UI.WebControls.TextBox txtANY_NON_OWNED_VEH_PP_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_NON_OWNED_VEH_PP_DESC;
		protected System.Web.UI.WebControls.TextBox txtEXISTING_DMG_PP_DESC;
		protected System.Web.UI.WebControls.Label MULTI_POLICY_DISC_APPLIED_PP_DESC;
		protected System.Web.UI.WebControls.Label capCURR_RES_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbCURR_RES_TYPE;
		protected System.Web.UI.WebControls.Label capIS_OTHER_THAN_INSURED;
		protected System.Web.UI.WebControls.DropDownList cmbIS_OTHER_THAN_INSURED;
		protected System.Web.UI.WebControls.Label capFull_Name;
		protected System.Web.UI.WebControls.TextBox txtFullName;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFullName;
		protected System.Web.UI.WebControls.Label capDOB;
		protected System.Web.UI.WebControls.TextBox txtDATE_OF_BIRTH;
		protected System.Web.UI.WebControls.HyperLink hlkDATE_OF_BIRTH;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDATE_OF_BIRTH;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDATE_OF_BIRTH;
		protected System.Web.UI.WebControls.CustomValidator csvDATE_OF_BIRTH;
		protected System.Web.UI.WebControls.Label capDrivingLisence;
		protected System.Web.UI.WebControls.TextBox txtDrivingLisence;
		protected System.Web.UI.WebControls.Label capInsuredElsewhere;
		protected System.Web.UI.WebControls.DropDownList cmbInsuredElseWhere;
		protected System.Web.UI.WebControls.Label capCompanyName;
		protected System.Web.UI.WebControls.TextBox txtCompanyName;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCompnayName;
		protected System.Web.UI.WebControls.Label capPolicyNumber;
		protected System.Web.UI.WebControls.TextBox txtPolicyNumber;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPolicyNumber;
		protected System.Web.UI.WebControls.Label capMULTI_POLICY_DISC_APPLIED_DESC;
		protected System.Web.UI.WebControls.Label capCOST_EQUIPMENT_DESC;
		protected System.Web.UI.WebControls.TextBox txtCOST_EQUIPMENT_DESC;
		protected System.Web.UI.HtmlControls.HtmlTableRow trCarModify;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCOST_EQUIPMENT_DESC;
  		protected System.Web.UI.WebControls.DropDownList cmbH_MEM_IN_MILITARY_DESC;

		protected System.Web.UI.WebControls.Label capANY_PRIOR_LOSSES;
		protected System.Web.UI.WebControls.DropDownList cmbANY_PRIOR_LOSSES;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_PRIOR_LOSSES;
		protected System.Web.UI.WebControls.Label capANY_PRIOR_LOSSES_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_PRIOR_LOSSES_DESC;
		protected System.Web.UI.WebControls.TextBox txtANY_PRIOR_LOSSES_DESC;
		protected System.Web.UI.WebControls.Label lblPRIOR_LOSSES;



		//END:*********** Local variables *************
		System.Resources.ResourceManager objResourceMgr;  // Declare Instance of Resource Manager

		#endregion

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvANY_NON_OWNED_VEH.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			rfvCAR_MODIFIED.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			rfvEXISTING_DMG.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			rfvANY_CAR_AT_SCH.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			rfvANY_OTH_AUTO_INSU.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			//rfvANY_OTH_INSU_COMP.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			//rfvH_MEM_IN_MILITARY.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			rfvDRIVER_SUS_REVOKED.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			rfvPHY_MENTL_CHALLENGED.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			rfvANY_FINANCIAL_RESPONSIBILITY.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			rfvINS_AGENCY_TRANSFER.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			rfvCOVERAGE_DECLINED.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			rfvAGENCY_VEH_INSPECTED.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			//rfvUSE_AS_TRANSPORT_FEE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			rfvSALVAGE_TITLE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			rfvANY_ANTIQUE_AUTO.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"12");
			rfvANY_CAR_AT_SCH_DESC.ErrorMessage					=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("632") ;
			rfvANY_ANTIQUE_AUTO_DESC.ErrorMessage=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("644") ;
			rfvANY_FINANCIAL_RESPONSIBILITY_PP_DESC.ErrorMessage=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("638") ;
			rfvANY_NON_OWNED_VEH_PP_DESC.ErrorMessage       =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("567");
			rfvANY_OTH_AUTO_INSU_DESC.ErrorMessage=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("633") ;
			//rfvANY_OTH_INSU_COMP_PP_DESC.ErrorMessage=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("634") ;
			rfvCOVERAGE_DECLINED_PP_DESC.ErrorMessage=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("640") ;
			rfvDRIVER_SUS_REVOKED_PP_DESC.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("636") ;
			rfvEXISTING_DMG_PP_DESC.ErrorMessage=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("568") ;
			rfvH_MEM_IN_MILITARY_DESC.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("635") ;
			rfvAGENCY_VEH_INSPECTED_PP_DESC.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("641") ;
			rfvINS_AGENCY_TRANSFER_PP_DESC.ErrorMessage=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("639") ;
			rfvMULTI_POLICY_DISC_APPLIED_PP_DESC.ErrorMessage=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("669") ;
			rfvPHY_MENTL_CHALLENGED_PP_DESC.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("637") ;
			rfvSALVAGE_TITLE_PP_DESC.ErrorMessage=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("643") ;
//			rfvAGENCY_VEH_INSPECTED_PP_DESC.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416") ;
			rfvCAR_MODIFIED_DESC.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("645") ;
			rfvUSE_AS_TRANSPORT_FEE_DESC.ErrorMessage=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("642") ;
			csvREMARKS.ErrorMessage						= 	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("440") ;
			rfvFullName.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("483");
			csvDATE_OF_BIRTH.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("481");
			rfvDATE_OF_BIRTH.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("482");
			rfvCompnayName.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("484");
			rfvPolicyNumber.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("485");
			revDATE_OF_BIRTH.ValidationExpression = aRegExpDate;
			revDATE_OF_BIRTH.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			revCOST_EQUIPMENT_DESC.ValidationExpression= aRegExpDoublePositiveNonZero;
			revCOST_EQUIPMENT_DESC.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");
			rfvANY_PRIOR_LOSSES.ErrorMessage					=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("843");
			rfvANY_PRIOR_LOSSES_DESC.ErrorMessage				=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("844");

			//added By shafi
			revYEARS_INSU.ValidationExpression      =     aRegExpInteger; 
			revYEARS_INSU_WOL.ValidationExpression  =     aRegExpInteger;
		    csvYEARS_INSU_WOL.ErrorMessage          =     Cms.CmsWeb.ClsMessages.FetchGeneralMessage("616");
			//cmpYEARS_INSU_WOL.ErrorMessage			=	  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("616");
			revYEARS_INSU.ErrorMessage              =     Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163"); 
			revYEARS_INSU_WOL.ErrorMessage          =     Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
			rngYEARS_INSU_WOL.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
			rngYEARS_INSU.ErrorMessage				=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");

				
		}
		#endregion

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="229";

			//btnReset.Attributes.Add("onclick","javascript:return ResetForm('APP_AUTO_GEN_INFO');");
			btnSave.Attributes.Add("onclick","javascript:SaveUnderw();alert('Safe/Premier discounts will be readjusted, please verify again at driver screen.');");
			btnReset.Attributes.Add("onclick","javascript:return ResetClick();");
			txtCOST_EQUIPMENT_DESC.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");
			//btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");

			//hlkDATE_OF_BIRTH.Attributes.Add("OnClick","fPopCalendar(document.APP_AUTO_GEN_INFO.txtDATE_OF_BIRTH,document.APP_AUTO_GEN_INFO.txtDATE_OF_BIRTH)");
			hlkDATE_OF_BIRTH.Attributes.Add("onClick","fPopCalendar(document.APP_AUTO_GEN_INFO.txtDATE_OF_BIRTH,document.APP_AUTO_GEN_INFO.txtDATE_OF_BIRTH)");
			cmbIS_OTHER_THAN_INSURED.Attributes.Add("onChange","javascript:Check();ApplyColor();ChangeColor();");
			cmbInsuredElseWhere.Attributes.Add("onChange","javascript:Check1();ChangeColor();");
			cmbCAR_MODIFIED.Attributes.Add("onChange","javascript:ShowCarModifyRow();");

			if(GetCustomerID() != null && GetPolicyID() != null && GetPolicyVersionID() != null && GetCustomerID().ToString() != "" && GetPolicyID().ToString() != "" && GetPolicyVersionID().ToString() != "")
			{
				hidOldData.Value = ClsPPGeneralInformation.GetPolicyXml(int.Parse(GetCustomerID()),int.Parse(GetPolicyID()),int.Parse(GetPolicyVersionID()));
			}
			if (hidOldData.Value == "")
			{
				hidCUSTOMER_ID.Value = "NEW";
			}

			ShowClientTopControl(int.Parse(GetCustomerID()));

//			if(GetCustomerID() != null && GetPolicyID() != null && GetPolicyVersionID() != null && GetCustomerID().ToString() != "" && GetPolicyID().ToString() != "" && GetPolicyVersionID().ToString() != "")
//			{
//				trMessage.Attributes.Add("style","display:inline");    
//				cltClientTop.CustomerID = int.Parse(GetCustomerID());
//				cltClientTop.PolicyID=int.Parse( GetPolicyID());
//				cltClientTop.PolicyVersionID =int.Parse( GetPolicyVersionID());
//
//				cltClientTop.ShowHeaderBand ="Policy";
//				cltClientTop.Visible = true;        
//			}
//			else
//			{
//				trMessage.Attributes.Add("style","display:none");  
//				capMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("109");      
//				capMessage.Visible=true; 
//			}
			
			lblMessage.Visible = false;
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass			=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass			=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.PolicyAutoGeneralInformation" ,Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				DataTable dt = Cms.CmsWeb.ClsFetcher.State;
				cmbH_MEM_IN_MILITARY_DESC.DataSource		= dt;
				cmbH_MEM_IN_MILITARY_DESC.DataTextField	= "State_Name";
				cmbH_MEM_IN_MILITARY_DESC.DataValueField	= "State_Id";
				cmbH_MEM_IN_MILITARY_DESC.DataBind();
				cmbH_MEM_IN_MILITARY_DESC.Items.Insert(0,"");

				SetCaptions();
				PopulateComboBox();
			}
			SetWorkFlowControl();

		}//end pageload
		#endregion

		//Method added by Charles on 29-Dec-09 for Itrack 6830
		private void Page_PreRender(object sender, System.EventArgs e)
		{
			string strStateID=Cms.BusinessLayer.BlApplication.ClsLocation.GetPolStateNameOnStateID(int.Parse(GetCustomerID()),int.Parse(GetPolicyID()),int.Parse(GetPolicyVersionID())).Rows[0]["STATE_ID"].ToString().Trim();
			if(strStateID != "14")
			{
				myWorkFlow.RemoveScreen(base.ScreenId + "_0");
			}
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

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsPolicyGeneralInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsPolicyGeneralInfo objPPGeneralInformationInfo = new ClsPolicyGeneralInfo();
						
			if(cmbIS_OTHER_THAN_INSURED.SelectedValue!=null && cmbIS_OTHER_THAN_INSURED.SelectedValue!="")
			{
				objPPGeneralInformationInfo.IS_OTHER_THAN_INSURED=	cmbIS_OTHER_THAN_INSURED.SelectedValue;
			}
			
			if (cmbIS_OTHER_THAN_INSURED.SelectedValue ==null || cmbIS_OTHER_THAN_INSURED.SelectedValue =="" || cmbIS_OTHER_THAN_INSURED.SelectedValue == "0")
			{	
				txtFullName.Text="";
				txtDATE_OF_BIRTH.Text="";
				txtDrivingLisence.Text="";
				txtCompanyName.Text="";
				txtPolicyNumber.Text="";
			}

			if(cmbInsuredElseWhere.SelectedValue!=null && cmbInsuredElseWhere.SelectedValue!="")
			{
				objPPGeneralInformationInfo.InsuredElseWhere=	cmbInsuredElseWhere.SelectedValue;
			}
			if(cmbInsuredElseWhere.SelectedValue == null || cmbInsuredElseWhere.SelectedValue == "" || cmbInsuredElseWhere.SelectedValue == "0")
			{
				txtCompanyName.Text="";
				txtPolicyNumber.Text="";			
			}


			
			if(cmbCURR_RES_TYPE.SelectedValue!=null && cmbCURR_RES_TYPE.SelectedValue!="")
				objPPGeneralInformationInfo.CURR_RES_TYPE=	cmbCURR_RES_TYPE.SelectedValue;

			

			if(txtDrivingLisence.Text.Trim()!="")
				objPPGeneralInformationInfo.DrivingLisence = txtDrivingLisence.Text.Trim();
			if(txtCompanyName.Text.Trim()!="")
				objPPGeneralInformationInfo.CompanyName = txtCompanyName.Text.Trim();
			if(txtPolicyNumber.Text.Trim()!="")
				objPPGeneralInformationInfo.PolicyNumber = txtPolicyNumber.Text.Trim();
			if(txtFullName.Text.Trim()!="")
				objPPGeneralInformationInfo.FullName = txtFullName.Text.Trim();
			if(txtDATE_OF_BIRTH.Text.Trim()!="")
				objPPGeneralInformationInfo.DATE_OF_BIRTH = Convert.ToDateTime(txtDATE_OF_BIRTH.Text);
			
			objPPGeneralInformationInfo.ANY_NON_OWNED_VEH				=	cmbANY_NON_OWNED_VEH.SelectedValue;
			if(cmbANY_NON_OWNED_VEH.SelectedValue == "0")
			{
				txtANY_NON_OWNED_VEH_PP_DESC.Text ="";
			}
			objPPGeneralInformationInfo.ANY_NON_OWNED_VEH_PP_DESC		=	txtANY_NON_OWNED_VEH_PP_DESC.Text;
			
			objPPGeneralInformationInfo.CAR_MODIFIED					=	cmbCAR_MODIFIED.SelectedValue;
			
			if(cmbCAR_MODIFIED.SelectedValue == "0")
			{
				txtCAR_MODIFIED_DESC.Text ="";
				txtCOST_EQUIPMENT_DESC.Text="";
			}
			else
			{
				
				if (txtCOST_EQUIPMENT_DESC.Text =="")
				{
					objPPGeneralInformationInfo.COST_EQUIPMENT_DESC="0";
				}
				else			
				{
					objPPGeneralInformationInfo.COST_EQUIPMENT_DESC		=   txtCOST_EQUIPMENT_DESC.Text;
				}
			}



			objPPGeneralInformationInfo.CAR_MODIFIED_DESC				=	txtCAR_MODIFIED_DESC.Text;

			objPPGeneralInformationInfo.EXISTING_DMG					=	cmbEXISTING_DMG.SelectedValue;
			if(cmbEXISTING_DMG.SelectedValue =="0")
			{
				txtEXISTING_DMG_PP_DESC.Text ="";
			}
			objPPGeneralInformationInfo.EXISTING_DMG_PP_DESC			=	txtEXISTING_DMG_PP_DESC.Text;

			objPPGeneralInformationInfo.ANY_CAR_AT_SCH					=	cmbANY_CAR_AT_SCH.SelectedValue;
			if(cmbANY_CAR_AT_SCH.SelectedValue == "0")
			{
				txtANY_CAR_AT_SCH_DESC.Text ="";
			}
			objPPGeneralInformationInfo.ANY_CAR_AT_SCH_DESC				=	txtANY_CAR_AT_SCH_DESC.Text;

			objPPGeneralInformationInfo.ANY_OTH_AUTO_INSU				=	cmbANY_OTH_AUTO_INSU.SelectedValue;
			if(cmbANY_OTH_AUTO_INSU.SelectedValue =="0")
			{
				txtANY_OTH_AUTO_INSU_DESC.Text ="";
			}
			objPPGeneralInformationInfo.ANY_OTH_AUTO_INSU_DESC			=	txtANY_OTH_AUTO_INSU_DESC.Text;

			//objPPGeneralInformationInfo.ANY_OTH_INSU_COMP				=	cmbANY_OTH_INSU_COMP.SelectedValue;

			//if(cmbANY_OTH_INSU_COMP.SelectedValue =="0")
			//{
				//txtANY_OTH_INSU_COMP_PP_DESC.Text = "";
			//}
			//objPPGeneralInformationInfo.ANY_OTH_INSU_COMP_PP_DESC		=	txtANY_OTH_INSU_COMP_PP_DESC.Text;

//			if(cmbH_MEM_IN_MILITARY_DESC.SelectedValue!=null && cmbH_MEM_IN_MILITARY_DESC.SelectedValue!="")
//			{
//				objPPGeneralInformationInfo.H_MEM_IN_MILITARY_DESC=	cmbH_MEM_IN_MILITARY_DESC.SelectedValue;
//			}
//			else
//			{
//				objPPGeneralInformationInfo.H_MEM_IN_MILITARY_DESC= "";
//			}

			//objPPGeneralInformationInfo.H_MEM_IN_MILITARY				=	cmbH_MEM_IN_MILITARY.SelectedValue;
			
			//objPPGeneralInformationInfo.H_MEM_IN_MILITARY_DESC			=	txtH_MEM_IN_MILITARY_DESC.Text;

			objPPGeneralInformationInfo.H_MEM_IN_MILITARY				=	cmbH_MEM_IN_MILITARY.SelectedValue;	
			if(cmbH_MEM_IN_MILITARY.SelectedItem.Text.ToUpper().Trim()  == "YES")
			{
				if(cmbH_MEM_IN_MILITARY_DESC.SelectedValue!=null && cmbH_MEM_IN_MILITARY_DESC.SelectedValue!="")
				{
					objPPGeneralInformationInfo.H_MEM_IN_MILITARY_DESC=	cmbH_MEM_IN_MILITARY_DESC.SelectedValue;
				}
				else
				{
					objPPGeneralInformationInfo.H_MEM_IN_MILITARY_DESC= "";
				}
			}
			else
			{
				objPPGeneralInformationInfo.H_MEM_IN_MILITARY_DESC= "";
			}


			objPPGeneralInformationInfo.DRIVER_SUS_REVOKED				=	cmbDRIVER_SUS_REVOKED.SelectedValue;

			if(cmbDRIVER_SUS_REVOKED.SelectedValue =="0")
			{
				txtDRIVER_SUS_REVOKED_PP_DESC.Text = "";
			}
			objPPGeneralInformationInfo.DRIVER_SUS_REVOKED_PP_DESC		=	txtDRIVER_SUS_REVOKED_PP_DESC.Text;

			objPPGeneralInformationInfo.PHY_MENTL_CHALLENGED			=	cmbPHY_MENTL_CHALLENGED.SelectedValue;

			if(cmbPHY_MENTL_CHALLENGED.SelectedValue == "0")
			{
				txtPHY_MENTL_CHALLENGED_PP_DESC.Text ="";
			}
			objPPGeneralInformationInfo.PHY_MENTL_CHALLENGED_PP_DESC	=	txtPHY_MENTL_CHALLENGED_PP_DESC.Text;

			objPPGeneralInformationInfo.ANY_FINANCIAL_RESPONSIBILITY	=	cmbANY_FINANCIAL_RESPONSIBILITY.SelectedValue;

			if(cmbANY_FINANCIAL_RESPONSIBILITY.SelectedValue =="0")
			{
				txtANY_FINANCIAL_RESPONSIBILITY_PP_DESC.Text ="";
			}

			objPPGeneralInformationInfo.ANY_FINANCIAL_RESPONSIBILITY_PP_DESC =txtANY_FINANCIAL_RESPONSIBILITY_PP_DESC.Text;

			objPPGeneralInformationInfo.INS_AGENCY_TRANSFER				=	cmbINS_AGENCY_TRANSFER.SelectedValue;

			if(cmbINS_AGENCY_TRANSFER.SelectedValue =="0")
			{
				txtINS_AGENCY_TRANSFER_PP_DESC.Text ="";
			}
			objPPGeneralInformationInfo.INS_AGENCY_TRANSFER_PP_DESC		=	txtINS_AGENCY_TRANSFER_PP_DESC.Text;

			objPPGeneralInformationInfo.COVERAGE_DECLINED				=	cmbCOVERAGE_DECLINED.SelectedValue;

			if(cmbCOVERAGE_DECLINED.SelectedValue =="0")
			{
				txtCOVERAGE_DECLINED_PP_DESC.Text ="";
			}
			objPPGeneralInformationInfo.COVERAGE_DECLINED_PP_DESC		=	txtCOVERAGE_DECLINED_PP_DESC.Text;

			objPPGeneralInformationInfo.AGENCY_VEH_INSPECTED			=	cmbAGENCY_VEH_INSPECTED.SelectedValue;

			if(cmbAGENCY_VEH_INSPECTED.SelectedValue =="0")
			{
				txtAGENCY_VEH_INSPECTED_PP_DESC.Text ="";
			}
			objPPGeneralInformationInfo.AGENCY_VEH_INSPECTED_PP_DESC	=	txtAGENCY_VEH_INSPECTED_PP_DESC.Text;

			objPPGeneralInformationInfo.USE_AS_TRANSPORT_FEE			=	cmbUSE_AS_TRANSPORT_FEE.SelectedValue;

			if(cmbUSE_AS_TRANSPORT_FEE.SelectedValue == "0")
			{
				txtUSE_AS_TRANSPORT_FEE_DESC.Text = "";
			}
			objPPGeneralInformationInfo.USE_AS_TRANSPORT_FEE_DESC		=	txtUSE_AS_TRANSPORT_FEE_DESC.Text;

			objPPGeneralInformationInfo.SALVAGE_TITLE					=	cmbSALVAGE_TITLE.SelectedValue;

			if(cmbSALVAGE_TITLE.SelectedValue == "0")
			{
				txtSALVAGE_TITLE_PP_DESC.Text ="";
			}
			objPPGeneralInformationInfo.SALVAGE_TITLE_PP_DESC			=	txtSALVAGE_TITLE_PP_DESC.Text;

			objPPGeneralInformationInfo.ANY_ANTIQUE_AUTO				=	cmbANY_ANTIQUE_AUTO.SelectedValue;

			if(cmbANY_ANTIQUE_AUTO.SelectedValue =="0")
			{
				txtANY_ANTIQUE_AUTO_DESC.Text ="";
			}
			objPPGeneralInformationInfo.ANY_ANTIQUE_AUTO_DESC			=	txtANY_ANTIQUE_AUTO_DESC.Text;
			
			objPPGeneralInformationInfo.MULTI_POLICY_DISC_APPLIED 		=	cmbMULTI_POLICY_DISC_APPLIED.SelectedValue;
			
			if(cmbMULTI_POLICY_DISC_APPLIED.SelectedValue == "0")
			{
				txtMULTI_POLICY_DISC_APPLIED_PP_DESC.Text ="";
			}
			objPPGeneralInformationInfo.MULTI_POLICY_DISC_APPLIED_PP_DESC =	txtMULTI_POLICY_DISC_APPLIED_PP_DESC.Text;
			
			objPPGeneralInformationInfo.REMARKS							=	txtREMARKS.Text;

			objPPGeneralInformationInfo.ANY_PRIOR_LOSSES				=	cmbANY_PRIOR_LOSSES.SelectedValue;

			if(cmbANY_PRIOR_LOSSES.SelectedValue == "0")
			{
				txtANY_PRIOR_LOSSES_DESC.Text ="";
			}
			objPPGeneralInformationInfo.ANY_PRIOR_LOSSES_DESC			=	txtANY_PRIOR_LOSSES_DESC.Text;

			//Added By Shafi
			if(txtYEARS_INSU.Text.Trim()!="")
				objPPGeneralInformationInfo.YEARS_INSU              = int.Parse(txtYEARS_INSU .Text.ToString().Trim());
			else
				objPPGeneralInformationInfo.YEARS_INSU               = 0;
			if(txtYEARS_INSU_WOL.Text.Trim()!="")
				objPPGeneralInformationInfo.YEARS_INSU_WOL            = int.Parse(txtYEARS_INSU_WOL.Text.ToString().Trim());
			else
				objPPGeneralInformationInfo.YEARS_INSU_WOL          = 0;
			
			objPPGeneralInformationInfo.CUSTOMER_ID						=	GetCustomerID() == "" ? 0 : int.Parse(GetCustomerID()) ;
			objPPGeneralInformationInfo.POLICY_ID 						=	GetPolicyID() == "" ? 0 : int.Parse(GetPolicyID()) ;
			objPPGeneralInformationInfo.POLICY_VERSION_ID 				=	GetPolicyVersionID()== "" ? 0 : int.Parse(GetPolicyVersionID()) ;
           

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidCUSTOMER_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objPPGeneralInformationInfo;
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
			this.PreRender += new System.EventHandler(this.Page_PreRender);//Added by Charles on 29-Dec-09 for Itrack 6830
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
				objPPGeneralInformation = new  ClsPPGeneralInformation();

				//Retreiving the form values into model class object
				ClsPolicyGeneralInfo objPPGeneralInformationInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objPPGeneralInformationInfo.CREATED_BY = int.Parse(GetUserId());
					objPPGeneralInformationInfo.IS_ACTIVE ="Y";
					objPPGeneralInformationInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objPPGeneralInformation.AddPolicyGeneralInformation(objPPGeneralInformationInfo);

					if(intRetVal>0)
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidOldData.Value = ClsPPGeneralInformation.GetPolicyXml(int.Parse(GetCustomerID()),int.Parse(GetPolicyID()),int.Parse(GetPolicyVersionID()));
						hidCUSTOMER_ID.Value=objPPGeneralInformationInfo.CUSTOMER_ID.ToString() ;
						strRowId=hidCUSTOMER_ID.Value ;
						hidIS_ACTIVE.Value = "Y";
						SetWorkFlowControl();

						//Showing the endorsement popup window
						base.OpenEndorsementDetails();
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
					ClsPolicyGeneralInfo objOldPPGeneralInformationInfo = new ClsPolicyGeneralInfo();
					
					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldPPGeneralInformationInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objPPGeneralInformationInfo.MODIFIED_BY = int.Parse(GetUserId());
					objPPGeneralInformationInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					objPPGeneralInformationInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

					//Updating the record using business layer class object
					intRetVal	= objPPGeneralInformation.UpdatePolicyGeneralInformation(objOldPPGeneralInformationInfo,objPPGeneralInformationInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"31");
						hidFormSaved.Value		=	"1";
						
						hidOldData.Value = ClsPPGeneralInformation.GetPolicyXml(int.Parse(GetCustomerID()),int.Parse(GetPolicyID()),int.Parse(GetPolicyVersionID()));
						SetWorkFlowControl();

						//Showing the endorsement popup window
						base.OpenEndorsementDetails();
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
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
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
			capANY_NON_OWNED_VEH.Text					=		objResourceMgr.GetString("cmbANY_NON_OWNED_VEH");
			capCAR_MODIFIED.Text						=		objResourceMgr.GetString("cmbCAR_MODIFIED");
			capEXISTING_DMG.Text						=		objResourceMgr.GetString("cmbEXISTING_DMG");
			capANY_CAR_AT_SCH.Text						=		objResourceMgr.GetString("cmbANY_CAR_AT_SCH");
			capANY_OTH_AUTO_INSU.Text					=		objResourceMgr.GetString("cmbANY_OTH_AUTO_INSU");
			//capANY_OTH_INSU_COMP.Text					=		objResourceMgr.GetString("cmbANY_OTH_INSU_COMP");
			capH_MEM_IN_MILITARY.Text					=		objResourceMgr.GetString("cmbH_MEM_IN_MILITARY");
			capDRIVER_SUS_REVOKED.Text					=		objResourceMgr.GetString("cmbDRIVER_SUS_REVOKED");
			capPHY_MENTL_CHALLENGED.Text				=		objResourceMgr.GetString("cmbPHY_MENTL_CHALLENGED");
			capANY_FINANCIAL_RESPONSIBILITY.Text		=		objResourceMgr.GetString("cmbANY_FINANCIAL_RESPONSIBILITY");
			capINS_AGENCY_TRANSFER.Text					=		objResourceMgr.GetString("cmbINS_AGENCY_TRANSFER");
			capCOVERAGE_DECLINED.Text					=		objResourceMgr.GetString("cmbCOVERAGE_DECLINED");
			capAGENCY_VEH_INSPECTED.Text				=		objResourceMgr.GetString("cmbAGENCY_VEH_INSPECTED");
			capUSE_AS_TRANSPORT_FEE.Text				=		objResourceMgr.GetString("cmbUSE_AS_TRANSPORT_FEE");
			capSALVAGE_TITLE.Text						=		objResourceMgr.GetString("cmbSALVAGE_TITLE");
			capANY_ANTIQUE_AUTO.Text					=		objResourceMgr.GetString("cmbANY_ANTIQUE_AUTO");
			capREMARKS.Text								=		objResourceMgr.GetString("txtREMARKS");
			capMULTI_POLICY_DISC_APPLIED_PP_DESC.Text   =       objResourceMgr.GetString("cmbMULTI_POLICY_DISC_APPLIED");
			capANY_NON_OWNED_VEH_PP_DESC.Text           =		objResourceMgr.GetString("txtANY_NON_OWNED_VEH_DESC");
			capCAR_MODIFIED_DESC.Text					=		objResourceMgr.GetString("txtCAR_MODIFIED_DESC");
			capEXISTING_DMG_PP_DESC.Text				=	    objResourceMgr.GetString("txtEXISTING_DMG_PP_DESC");
			capANY_CAR_AT_SCH_DESC.Text					=		objResourceMgr.GetString("txtANY_CAR_AT_SCH_DESC");
			capANY_OTH_AUTO_INSU_DESC.Text				=       objResourceMgr.GetString("txtANY_OTH_AUTO_INSU_DESC");
			//capANY_OTH_INSU_COMP_PP_DESC.Text			=		objResourceMgr.GetString("txtANY_OTH_INSU_COMP_PP_DESC");	
			capH_MEM_IN_MILITARY_DESC.Text				=       objResourceMgr.GetString("cmbH_MEM_IN_MILITARY_DESC");	
			capDRIVER_SUS_REVOKED_PP_DESC.Text			=	    objResourceMgr.GetString("txtDRIVER_SUS_REVOKED_PP_DESC");	
			capPHY_MENTL_CHALLENGED_PP_DESC.Text		=       objResourceMgr.GetString("txtPHY_MENTL_CHALLENGED_PP_DESC");	
			capANY_FINANCIAL_RESPONSIBILITY_PP_DESC.Text=       objResourceMgr.GetString("txtANY_FINANCIAL_RESPONSIBILITY_PP_DESC");	
			capINS_AGENCY_TRANSFER_PP_DESC.Text			=       objResourceMgr.GetString("txtINS_AGENCY_TRANSFER_PP_DESC");	
			capCOVERAGE_DECLINED_PP_DESC.Text			=       objResourceMgr.GetString("txtCOVERAGE_DECLINED_PP_DESC");
			capAGENCY_VEH_INSPECTED_PP_DESC.Text		=	    objResourceMgr.GetString("txtAGENCY_VEH_INSPECTED_PP_DESC");
			capUSE_AS_TRANSPORT_FEE_DESC.Text			=	    objResourceMgr.GetString("txtUSE_AS_TRANSPORT_FEE_DESC");
			capSALVAGE_TITLE_PP_DESC.Text		    	=	    objResourceMgr.GetString("txtSALVAGE_TITLE_PP_DESC");
			capANY_ANTIQUE_AUTO_DESC.Text				=		objResourceMgr.GetString("txtANY_ANTIQUE_AUTO_DESC");
			capMULTI_POLICY_DISC_APPLIED_DESC.Text	    =	    objResourceMgr.GetString("txtMULTI_POLICY_DISC_APPLIED_PP_DESC");
			capANY_PRIOR_LOSSES.Text					=		objResourceMgr.GetString("cmbANY_PRIOR_LOSSES");
			capANY_PRIOR_LOSSES_DESC.Text				=		objResourceMgr.GetString("txtANY_PRIOR_LOSSES_DESC");
			capCOST_EQUIPMENT_DESC.Text				    =       objResourceMgr.GetString("txtCOST_EQUIPMENT_DESC");
			capYEARS_INSU_WOL.Text                      =		objResourceMgr.GetString("txtYEARS_INSU_WOL");
			//Added By Shafi 17-01-2006
			capYEARS_INSU.Text                          =		objResourceMgr.GetString("txtYEARS_INSU");
			//capYEARS_INSU_WOL.Text                             =   objResourceMgr.GetString("txtYEARS_INSU_WOL");
			
		}
		private void PopulateComboBox()
		{									  

			
			cmbANY_NON_OWNED_VEH.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbANY_NON_OWNED_VEH.DataTextField="LookupDesc"; 
			cmbANY_NON_OWNED_VEH.DataValueField="LookupCode";
			cmbANY_NON_OWNED_VEH.DataBind();
			cmbANY_NON_OWNED_VEH.Items.Insert(0,"");
			
			cmbIS_OTHER_THAN_INSURED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbIS_OTHER_THAN_INSURED.DataTextField="LookupDesc"; 
			cmbIS_OTHER_THAN_INSURED.DataValueField="LookupCode";
			cmbIS_OTHER_THAN_INSURED.DataBind();
			//cmbIS_OTHER_THAN_INSURED.Items.Insert(0,"");

			cmbInsuredElseWhere.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbInsuredElseWhere.DataTextField="LookupDesc"; 
			cmbInsuredElseWhere.DataValueField="LookupCode";
			cmbInsuredElseWhere.DataBind();
			cmbInsuredElseWhere.Items.Insert(0,"");

			cmbCAR_MODIFIED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbCAR_MODIFIED.DataTextField="LookupDesc"; 
			cmbCAR_MODIFIED.DataValueField="LookupCode";
			cmbCAR_MODIFIED.DataBind();
			cmbCAR_MODIFIED.Items.Insert(0,"");
			
			cmbEXISTING_DMG.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbEXISTING_DMG.DataTextField="LookupDesc"; 
			cmbEXISTING_DMG.DataValueField="LookupCode";
			cmbEXISTING_DMG.DataBind();
			cmbEXISTING_DMG.Items.Insert(0,"");

			
			cmbANY_CAR_AT_SCH.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbANY_CAR_AT_SCH.DataTextField="LookupDesc"; 
			cmbANY_CAR_AT_SCH.DataValueField="LookupCode";
			cmbANY_CAR_AT_SCH.DataBind();
			cmbANY_CAR_AT_SCH.Items.Insert(0,"");

			
			cmbANY_OTH_AUTO_INSU.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbANY_OTH_AUTO_INSU.DataTextField="LookupDesc"; 
			cmbANY_OTH_AUTO_INSU.DataValueField="LookupCode";
			cmbANY_OTH_AUTO_INSU.DataBind();
			cmbANY_OTH_AUTO_INSU.Items.Insert(0,"");

			
			//cmbANY_OTH_INSU_COMP.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			//cmbANY_OTH_INSU_COMP.DataTextField="LookupDesc"; 
			//cmbANY_OTH_INSU_COMP.DataValueField="LookupCode";
			//cmbANY_OTH_INSU_COMP.DataBind();
			//cmbANY_OTH_INSU_COMP.Items.Insert(0,"");

			
			cmbH_MEM_IN_MILITARY.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbH_MEM_IN_MILITARY.DataTextField="LookupDesc"; 
			cmbH_MEM_IN_MILITARY.DataValueField="LookupCode";
			cmbH_MEM_IN_MILITARY.DataBind();
			cmbH_MEM_IN_MILITARY.Items.Insert(0,"");

			
			cmbDRIVER_SUS_REVOKED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbDRIVER_SUS_REVOKED.DataTextField="LookupDesc"; 
			cmbDRIVER_SUS_REVOKED.DataValueField="LookupCode";
			cmbDRIVER_SUS_REVOKED.DataBind();
			cmbDRIVER_SUS_REVOKED.Items.Insert(0,"");

			
			cmbPHY_MENTL_CHALLENGED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbPHY_MENTL_CHALLENGED.DataTextField="LookupDesc"; 
			cmbPHY_MENTL_CHALLENGED.DataValueField="LookupCode";
			cmbPHY_MENTL_CHALLENGED.DataBind();
			cmbPHY_MENTL_CHALLENGED.Items.Insert(0,"");

			
			cmbANY_FINANCIAL_RESPONSIBILITY.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbANY_FINANCIAL_RESPONSIBILITY.DataTextField="LookupDesc"; 
			cmbANY_FINANCIAL_RESPONSIBILITY.DataValueField="LookupCode";
			cmbANY_FINANCIAL_RESPONSIBILITY.DataBind();
			cmbANY_FINANCIAL_RESPONSIBILITY.Items.Insert(0,"");

			
			cmbINS_AGENCY_TRANSFER.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbINS_AGENCY_TRANSFER.DataTextField="LookupDesc"; 
			cmbINS_AGENCY_TRANSFER.DataValueField="LookupCode";
			cmbINS_AGENCY_TRANSFER.DataBind();
			cmbINS_AGENCY_TRANSFER.Items.Insert(0,"");

			
			cmbCOVERAGE_DECLINED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbCOVERAGE_DECLINED.DataTextField="LookupDesc"; 
			cmbCOVERAGE_DECLINED.DataValueField="LookupCode";
			cmbCOVERAGE_DECLINED.DataBind();
			cmbCOVERAGE_DECLINED.Items.Insert(0,"");

			
			cmbAGENCY_VEH_INSPECTED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbAGENCY_VEH_INSPECTED.DataTextField="LookupDesc"; 
			cmbAGENCY_VEH_INSPECTED.DataValueField="LookupCode";
			cmbAGENCY_VEH_INSPECTED.DataBind();
			cmbAGENCY_VEH_INSPECTED.Items.Insert(0,"");

			
			cmbUSE_AS_TRANSPORT_FEE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbUSE_AS_TRANSPORT_FEE.DataTextField="LookupDesc"; 
			cmbUSE_AS_TRANSPORT_FEE.DataValueField="LookupCode";
			cmbUSE_AS_TRANSPORT_FEE.DataBind();
			cmbUSE_AS_TRANSPORT_FEE.Items.Insert(0,"");

			
			cmbSALVAGE_TITLE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbSALVAGE_TITLE.DataTextField="LookupDesc"; 
			cmbSALVAGE_TITLE.DataValueField="LookupCode";
			cmbSALVAGE_TITLE.DataBind();
			cmbSALVAGE_TITLE.Items.Insert(0,"");

			
			cmbANY_ANTIQUE_AUTO.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbANY_ANTIQUE_AUTO.DataTextField="LookupDesc"; 
			cmbANY_ANTIQUE_AUTO.DataValueField="LookupCode";
			cmbANY_ANTIQUE_AUTO.DataBind();
			cmbANY_ANTIQUE_AUTO.Items.Insert(0,"");


				
			cmbMULTI_POLICY_DISC_APPLIED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbMULTI_POLICY_DISC_APPLIED.DataTextField="LookupDesc"; 
			cmbMULTI_POLICY_DISC_APPLIED.DataValueField="LookupCode";
			cmbMULTI_POLICY_DISC_APPLIED.DataBind();
			cmbMULTI_POLICY_DISC_APPLIED.Items.Insert(0,"");

			cmbANY_PRIOR_LOSSES.DataSource		= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbANY_PRIOR_LOSSES.DataTextField	= "LookupDesc"; 
			cmbANY_PRIOR_LOSSES.DataValueField	= "LookupCode";
			cmbANY_PRIOR_LOSSES.DataBind();
			cmbANY_PRIOR_LOSSES.Items.Insert(0,"");
			
			
			
		}

		private void ShowClientTopControl(int intCustomerId)
		{
			if (intCustomerId != 0)
			{
				cltClientTop.CustomerID = intCustomerId;
				cltClientTop.PolicyID = GetPolicyID() == "" ? 0 : int.Parse(GetPolicyID());
				cltClientTop.PolicyVersionID = GetPolicyVersionID() == "" ? 0 : int.Parse(GetPolicyVersionID());

				cltClientTop.Visible = true;
				cltClientTop.ShowHeaderBand = "Policy";
			}
			else
			{
				cltClientTop.Visible = false;
			}
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
