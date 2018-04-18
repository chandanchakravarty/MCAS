/******************************************************************************************
<Author					: - Vijay Arora
<Start Date				: -	01-12-2005
<End Date				: -	
<Description			: - Class for Add and Update for UnderWriting Question of Policy WaterCraft
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
using Cms.CmsWeb; 
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.BusinessLayer.BlApplication;
//using Cms.Model.Application.Watercrafts;
using System.Resources;
using System.Reflection;


namespace Cms.Policies.Aspx.Watercrafts
{
	/// <summary>
	/// Summary description for PolicyAddWatercraftGenInfo.
	/// </summary>
	public class PolicyAddWatercraftGenInfo : Cms.Policies.policiesbase
	{
		
		#region Page Controls

		protected System.Web.UI.WebControls.Label lblMessage;
		//protected System.Web.UI.WebControls.Label capHAS_CURR_ADD_THREE_YEARS;
		//protected System.Web.UI.WebControls.DropDownList cmbHAS_CURR_ADD_THREE_YEARS;
		protected System.Web.UI.WebControls.Label capPHY_MENTL_CHALLENGED;
		protected System.Web.UI.WebControls.DropDownList cmbPHY_MENTL_CHALLENGED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPHY_MENTL_CHALLENGED;
		protected System.Web.UI.WebControls.Label capDRIVER_SUS_REVOKED;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_SUS_REVOKED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_SUS_REVOKED;
		protected System.Web.UI.WebControls.Label capIS_CONVICTED_ACCIDENT;
		protected System.Web.UI.WebControls.DropDownList cmbIS_CONVICTED_ACCIDENT;
		//protected System.Web.UI.WebControls.Label capOTHER_POLICY_NUMBER_LIST;
		//protected System.Web.UI.WebControls.DropDownList cmbANY_OTH_INSU_COMP;
		protected System.Web.UI.WebControls.Label capANY_LOSS_THREE_YEARS;
		protected System.Web.UI.WebControls.DropDownList cmbANY_LOSS_THREE_YEARS;
		protected System.Web.UI.WebControls.Label capCOVERAGE_DECLINED;
		protected System.Web.UI.WebControls.DropDownList cmbCOVERAGE_DECLINED;
		protected System.Web.UI.WebControls.Label capIS_RENTED_OTHERS;
		protected System.Web.UI.WebControls.DropDownList cmbIS_RENTED_OTHERS;
		protected System.Web.UI.WebControls.Label capIS_REGISTERED_OTHERS;
		protected System.Web.UI.WebControls.DropDownList cmbIS_REGISTERED_OTHERS;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRowId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidActivePolicyList;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		protected System.Web.UI.WebControls.Label capMessage;        
		protected System.Web.UI.WebControls.Label capANY_BOAT_AMPHIBIOUS;
		protected System.Web.UI.WebControls.Label capANY_BOAT_AMPHIBIOUS_DESC;
		protected System.Web.UI.WebControls.TextBox txtANY_BOAT_AMPHIBIOUS_DESC;
		protected System.Web.UI.WebControls.DropDownList cmbANY_BOAT_AMPHIBIOUS;
		protected System.Web.UI.HtmlControls.HtmlTableRow trMessage;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelect;

		#endregion
            
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		protected System.Web.UI.HtmlControls.HtmlForm APP_WATERCRAFT_GEN_INFO;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTextVal;
		//protected System.Web.UI.WebControls.TextBox txtHAS_CURR_ADD_THREE_YEARS_DESC;
		protected System.Web.UI.WebControls.Label capPHY_MENTL_CHALLENGED_DESC;
		protected System.Web.UI.WebControls.Label capDRIVER_SUS_REVOKED_DESC;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_SUS_REVOKED_DESC;
		protected System.Web.UI.WebControls.Label capIS_CONVICTED_ACCIDENT_DESC;
		protected System.Web.UI.WebControls.TextBox txtIS_CONVICTED_ACCIDENT_DESC;
		protected System.Web.UI.WebControls.Label capANY_LOSS_THREE_YEARS_DESC;
		protected System.Web.UI.WebControls.TextBox txtANY_LOSS_THREE_YEARS_DESC;
		protected System.Web.UI.WebControls.Label capCOVERAGE_DECLINED_DESC;
		protected System.Web.UI.WebControls.TextBox txtCOVERAGE_DECLINED_DESC;
		protected System.Web.UI.WebControls.Label capIS_RENTED_OTHERS_DESC;
		protected System.Web.UI.WebControls.TextBox txtIS_RENTED_OTHERS_DESC;
		protected System.Web.UI.WebControls.Label capIS_REGISTERED_OTHERS_DESC;
		protected System.Web.UI.WebControls.TextBox txtIS_REGISTERED_OTHERS_DESC;
		//protected System.Web.UI.WebControls.Label lblHAS_CURR_ADD_THREE_YEARS_DESC;
		protected System.Web.UI.WebControls.Label lblPHY_MENTL_CHALLENGED_DESC;
		protected System.Web.UI.WebControls.Label lblDRIVER_SUS_REVOKED_DESC;
		protected System.Web.UI.WebControls.Label lblANY_LOSS_THREE_YEARS_DESC;
		protected System.Web.UI.WebControls.Label lblCOVERAGE_DECLINED_DESC;
		protected System.Web.UI.WebControls.Label lblIS_RENTED_OTHERS_DESC;
		protected System.Web.UI.WebControls.Label lblIS_REGISTERED_OTHERS_DESC;
		protected System.Web.UI.WebControls.Label lblIS_CONVICTED_ACCIDENT_DESC;
		//protected System.Web.UI.WebControls.Label capHAS_CURR_ADD_THREE_YEARS_DESC;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvHAS_CURR_ADD_THREE_YEARS_DESC;
		protected System.Web.UI.WebControls.TextBox txtPHY_MENTL_CHALLENGED_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPHY_MENTL_CHALLENGED_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_SUS_REVOKED_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_CONVICTED_ACCIDENT_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_LOSS_THREE_YEARS_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOVERAGE_DECLINED_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_RENTED_OTHERS_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_REGISTERED_OTHERS_DESC;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvtHAS_CURR_ADD_THREE_YEARS_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvtANY_LOSS_THREE_YEARS_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvtCOVERAGE_DECLINED_DESC;
		//protected System.Web.UI.WebControls.TextBox txtOTHER_POLICY_NUMBER_LIST;
		//protected System.Web.UI.WebControls.Label lblANY_OTH_INSU_COMP_DESC;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_OTH_INSU_COMP_DESC;
		//protected System.Web.UI.WebControls.Label capcapANY_OTH_INSU_COMP_DESC;
		//protected System.Web.UI.WebControls.Label capANY_OTH_INSU_COMP;
		//protected System.Web.UI.WebControls.Label lblOTHER_POLICY_NUMBER_LIST;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvOTHER_POLICY_NUMBER_LIST;
		protected System.Web.UI.WebControls.Label capDRINK_DRUG_VOILATION;
		protected System.Web.UI.WebControls.DropDownList cmbDRINK_DRUG_VOILATION;
		protected System.Web.UI.WebControls.Label capMINOR_VIOLATION;
		protected System.Web.UI.WebControls.DropDownList cmbMINOR_VIOLATION;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist2;
		protected System.Web.UI.WebControls.Label capPARTICIPATE_RACE;
		protected System.Web.UI.WebControls.Label capPARTICIPATE_RACE_DESC;
		protected System.Web.UI.WebControls.TextBox txtPARTICIPATE_RACE_DESC;
		protected System.Web.UI.WebControls.Label lblPARTICIPATE_RACE_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPARTICIPATE_RACE_DESC;
		protected System.Web.UI.WebControls.DropDownList cmbPARTICIPATE_RACE;
		protected System.Web.UI.WebControls.Label capCARRY_PASSENGER_FOR_CHARGE;
		protected System.Web.UI.WebControls.DropDownList cmbCARRY_PASSENGER_FOR_CHARGE;
		protected System.Web.UI.WebControls.Label capCARRY_PASSENGER_FOR_CHARGE_DESC;
		protected System.Web.UI.WebControls.TextBox txtCARRY_PASSENGER_FOR_CHARGE_DESC;
		protected System.Web.UI.WebControls.Label lblCARRY_PASSENGER_FOR_CHARGE_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCARRY_PASSENGER_FOR_CHARGE_DESC;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvHAS_CURR_ADD_THREE_YEARS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_CONVICTED_ACCIDENT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOVERAGE_DECLINED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_LOSS_THREE_YEARS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRINK_DRUG_VOILATION;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_OTH_INSU_COMP;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCARRY_PASSENGER_FOR_CHARGE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_RENTED_OTHERS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPARTICIPATE_RACE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_REGISTERED_OTHERS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMINOR_VIOLATION;
		//protected System.Web.UI.WebControls.Label capIS_CREDIT;
		//protected System.Web.UI.WebControls.DropDownList cmbIS_CREDIT;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_CREDIT;
		//protected System.Web.UI.WebControls.Label capCREDIT_DETAILS;
		//protected System.Web.UI.WebControls.TextBox txtCREDIT_DETAILS;
	//	protected System.Web.UI.WebControls.Label lblCREDIT_DETAILS;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvCREDIT_DETAILS;
		protected System.Web.UI.WebControls.Label capPRIOR_INSURANCE_CARRIER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPRIOR_INSURANCE_CARRIER;
		protected System.Web.UI.WebControls.Label capPRIOR_INSURANCE_CARRIER_DESC;
		protected System.Web.UI.WebControls.TextBox txtPRIOR_INSURANCE_CARRIER_DESC;
		protected System.Web.UI.WebControls.Label lblPRIOR_INSURANCE_CARRIER_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPRIOR_INSURANCE_CARRIER_DESC;
		protected System.Web.UI.WebControls.DropDownList cmbIS_PRIOR_INSURANCE_CARRIER;
		protected System.Web.UI.WebControls.Label capHAS_CURR_ADD_THREE_YEARS;
		protected System.Web.UI.WebControls.DropDownList cmbHAS_CURR_ADD_THREE_YEARS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHAS_CURR_ADD_THREE_YEARS;
		protected System.Web.UI.WebControls.Label capHAS_CURR_ADD_THREE_YEARS_DESC;
		protected System.Web.UI.WebControls.TextBox txtHAS_CURR_ADD_THREE_YEARS_DESC;
		protected System.Web.UI.WebControls.Label lblHAS_CURR_ADD_THREE_YEARS_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHAS_CURR_ADD_THREE_YEARS_DESC; 
       
		protected System.Web.UI.WebControls.DropDownList cmbIS_BOAT_COOWNED;
		protected System.Web.UI.WebControls.Label capIS_BOAT_COOWNED;
		protected System.Web.UI.WebControls.TextBox txtIS_BOAT_COOWNED_DESC;
		protected System.Web.UI.WebControls.Label capIS_BOAT_COOWNED_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_BOAT_COOWNED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_BOAT_COOWNED_DESC;
		protected System.Web.UI.WebControls.Label lblIS_BOAT_COOWNED_DESC;

		protected System.Web.UI.WebControls.Label capMULTI_POLICY_DISC_APPLIED_PP_DESC;
		protected System.Web.UI.WebControls.DropDownList cmbMULTI_POLICY_DISC_APPLIED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMULTI_POLICY_DISC_APPLIED;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyCount;
		protected System.Web.UI.WebControls.Label capMULTI_POLICY_DISC_APPLIED_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMULTI_POLICY_DISC_APPLIED_PP_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_BOAT_AMPHIBIOUS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_BOAT_AMPHIBIOUS_DESC;
		protected System.Web.UI.WebControls.TextBox txtMULTI_POLICY_DISC_APPLIED_PP_DESC;
		protected System.Web.UI.WebControls.Label lblMULTIPOLICY_DISC_DESC;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.WebControls.Label lblANY_BOAT_AMPHIBIOUS_DESC;
		protected System.Web.UI.WebControls.Label capANY_BOAT_RESIDENCE;
		protected System.Web.UI.WebControls.DropDownList cmbANY_BOAT_RESIDENCE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_BOAT_RESIDENCE;
		protected System.Web.UI.WebControls.Label capANY_BOAT_RESIDENCE_DESC;
		protected System.Web.UI.WebControls.TextBox txtANY_BOAT_RESIDENCE_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_BOAT_RESIDENCE_DESC;
		protected System.Web.UI.WebControls.Label lblANY_BOAT_RESIDENCE_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label cap;
		protected System.Web.UI.WebControls.DropDownList cmb;
		protected System.Web.UI.WebControls.TextBox txtANY_BOAT_RESIDENCE;
		protected System.Web.UI.WebControls.Label LBLANY_BOAT_RESIDENCE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfv;

		protected System.Web.UI.WebControls.Label capIS_BOAT_USED_IN_ANY_WATER;
		protected System.Web.UI.WebControls.DropDownList cmbIS_BOAT_USED_IN_ANY_WATER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_BOAT_USED_IN_ANY_WATER;
		protected System.Web.UI.WebControls.Label capIS_BOAT_USED_IN_ANY_WATER_DESC;
		protected System.Web.UI.WebControls.TextBox txtIS_BOAT_USED_IN_ANY_WATER_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_BOAT_USED_IN_ANY_WATER_DESC;
		protected System.Web.UI.WebControls.Label lblIS_BOAT_USED_IN_ANY_WATER_DESC;
		
		
		//Defining the business layer class object
		ClsWatercraftGenInformation  objWatercraftGenInformation ;
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
			//rfvCREDIT_DETAILS.ErrorMessage					=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("604");
			rfvPHY_MENTL_CHALLENGED.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			rfvDRIVER_SUS_REVOKED.ErrorMessage				=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129") ;
			//rfvHAS_CURR_ADD_THREE_YEARS.ErrorMessage		=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129") ;
			rfvIS_CONVICTED_ACCIDENT.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129") ;
			rfvDRINK_DRUG_VOILATION.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129") ;
			rfvANY_LOSS_THREE_YEARS.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129") ;
			//rfvIS_CREDIT.ErrorMessage						=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129") ;
			rfvIS_REGISTERED_OTHERS.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129") ;
			rfvMINOR_VIOLATION.ErrorMessage					=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129") ;
			//rfvOTHER_POLICY_NUMBER_LIST.ErrorMessage		=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("620") ;
			rfvIS_CONVICTED_ACCIDENT_DESC.ErrorMessage		=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("626") ;
			rfvDRIVER_SUS_REVOKED_DESC.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("570") ;
			rfvIS_REGISTERED_OTHERS_DESC.ErrorMessage		=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("624") ;
			rfvIS_RENTED_OTHERS_DESC.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("623") ;
			rfvPHY_MENTL_CHALLENGED_DESC.ErrorMessage		=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("588") ;
			rfvANY_LOSS_THREE_YEARS_DESC.ErrorMessage		=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("621") ;
			//rfvHAS_CURR_ADD_THREE_YEARS_DESC.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416") ;
			rfvCOVERAGE_DECLINED_DESC.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("622") ;
			rfvPARTICIPATE_RACE_DESC.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("625") ;
			//rfvCARRY_PASSENGER_FOR_CHARGE_DESC.ErrorMessage =	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416") ;
			rfvCARRY_PASSENGER_FOR_CHARGE_DESC.ErrorMessage =	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("730") ;
			//rfvANY_OTH_INSU_COMP.ErrorMessage				=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129"); 
			rfvCOVERAGE_DECLINED.ErrorMessage				=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");   
			rfvIS_RENTED_OTHERS.ErrorMessage				=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");   			
			rfvPARTICIPATE_RACE.ErrorMessage				=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");   			
			rfvCARRY_PASSENGER_FOR_CHARGE.ErrorMessage		=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");   			
			rfvPRIOR_INSURANCE_CARRIER_DESC.ErrorMessage	=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("612");   			
			rfvPRIOR_INSURANCE_CARRIER.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			rfvIS_BOAT_COOWNED.ErrorMessage			=	  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			//rfvIS_BOAT_COOWNED_DESC.ErrorMessage	=	  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416");
			rfvIS_BOAT_COOWNED_DESC.ErrorMessage	=	  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("729");
			rfvIS_BOAT_COOWNED.ErrorMessage			=	  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			//rfvIS_BOAT_COOWNED_DESC.ErrorMessage	=	  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416");

			//added By shafi
			rfvMULTI_POLICY_DISC_APPLIED.ErrorMessage         = ClsMessages.GetMessage(base.ScreenId,"129");  
			rfvMULTI_POLICY_DISC_APPLIED_PP_DESC.ErrorMessage =	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("801") ;
			rfvANY_BOAT_AMPHIBIOUS.ErrorMessage				  = ClsMessages.GetMessage(base.ScreenId,"129");  
			rfvANY_BOAT_AMPHIBIOUS_DESC.ErrorMessage		  =	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("625") ;
			rfvANY_BOAT_RESIDENCE.ErrorMessage				  = ClsMessages.GetMessage(base.ScreenId,"129");  
			rfvANY_BOAT_RESIDENCE_DESC.ErrorMessage			  =	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("745") ;

			rfvIS_BOAT_USED_IN_ANY_WATER_DESC.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("997");   			
			rfvIS_BOAT_USED_IN_ANY_WATER.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");



		}
		#endregion					  

		private void SetCaptions()
		{
			capMINOR_VIOLATION.Text							=	objResourceMgr.GetString("cmbMINOR_VIOLATION");
			capDRINK_DRUG_VOILATION.Text					=	objResourceMgr.GetString("cmbDRINK_DRUG_VOILATION");
			//capHAS_CURR_ADD_THREE_YEARS.Text				=	objResourceMgr.GetString("cmbHAS_CURR_ADD_THREE_YEARS");
			capPHY_MENTL_CHALLENGED.Text					=	objResourceMgr.GetString("cmbPHY_MENTL_CHALLENGED");
			capDRIVER_SUS_REVOKED.Text						=	objResourceMgr.GetString("cmbDRIVER_SUS_REVOKED");
			capIS_CONVICTED_ACCIDENT.Text					=	objResourceMgr.GetString("cmbIS_CONVICTED_ACCIDENT");
			//capOTHER_POLICY_NUMBER_LIST.Text				=	objResourceMgr.GetString("txtOTHER_POLICY_NUMBER_LIST");
			capANY_LOSS_THREE_YEARS.Text					=	objResourceMgr.GetString("cmbANY_LOSS_THREE_YEARS");
			capCOVERAGE_DECLINED.Text						=	objResourceMgr.GetString("cmbCOVERAGE_DECLINED");
			//capIS_CREDIT.Text								=	objResourceMgr.GetString("cmbIS_CREDIT");
			//capCREDIT_DETAILS.Text							=	objResourceMgr.GetString("txtCREDIT_DETAILS");
			capIS_RENTED_OTHERS.Text						=	objResourceMgr.GetString("cmbIS_RENTED_OTHERS");
			capIS_REGISTERED_OTHERS.Text					=	objResourceMgr.GetString("cmbIS_REGISTERED_OTHERS");
			//capHAS_CURR_ADD_THREE_YEARS_DESC.Text			=	objResourceMgr.GetString("txtHAS_CURR_ADD_THREE_YEARS_DESC");
			capPHY_MENTL_CHALLENGED_DESC.Text				=   objResourceMgr.GetString("txtPHY_MENTL_CHALLENGED_DESC");
			capDRIVER_SUS_REVOKED_DESC.Text					=	objResourceMgr.GetString("txtDRIVER_SUS_REVOKED_DESC");
			capIS_CONVICTED_ACCIDENT_DESC.Text				=	objResourceMgr.GetString("txtIS_CONVICTED_ACCIDENT_DESC");
			capANY_LOSS_THREE_YEARS_DESC.Text				=   objResourceMgr.GetString("txtANY_LOSS_THREE_YEARS_DESC");
			capCOVERAGE_DECLINED_DESC.Text					=   objResourceMgr.GetString("txtCOVERAGE_DECLINED_DESC");
			capIS_RENTED_OTHERS_DESC.Text					= 	objResourceMgr.GetString("txtIS_RENTED_OTHERS_DESC");
			capIS_REGISTERED_OTHERS_DESC.Text				=	objResourceMgr.GetString("txtIS_REGISTERED_OTHERS_DESC");
			capPARTICIPATE_RACE_DESC.Text					=	objResourceMgr.GetString("txtPARTICIPATE_RACE_DESC"); 
			capCARRY_PASSENGER_FOR_CHARGE_DESC.Text			=	objResourceMgr.GetString("txtCARRY_PASSENGER_FOR_CHARGE_DESC"); 
			capPARTICIPATE_RACE.Text						=	objResourceMgr.GetString("cmbPARTICIPATE_RACE"); 	 
			capCARRY_PASSENGER_FOR_CHARGE.Text				=	objResourceMgr.GetString("cmbCARRY_PASSENGER_FOR_CHARGE"); 	 
			capPRIOR_INSURANCE_CARRIER.Text					=	objResourceMgr.GetString("cmbIS_PRIOR_INSURANCE_CARRIER");
			capPRIOR_INSURANCE_CARRIER_DESC.Text			=	objResourceMgr.GetString("txtPRIOR_INSURANCE_CARRIER_DESC");
			capIS_BOAT_COOWNED.Text							=   objResourceMgr.GetString("cmbIS_BOAT_COOWNED"); 	 
			capIS_BOAT_COOWNED_DESC.Text                     =   objResourceMgr.GetString("txtIS_BOAT_COOWNED_DESC");

			//Added By Shafi 20 March 2006
			capMULTI_POLICY_DISC_APPLIED_PP_DESC.Text    =       objResourceMgr.GetString("cmbMULTI_POLICY_DISC_APPLIED");
			capMULTI_POLICY_DISC_APPLIED_DESC.Text	     =	     objResourceMgr.GetString("txtMULTI_POLICY_DISC_APPLIED_PP_DESC");
			capANY_BOAT_AMPHIBIOUS.Text					 =       objResourceMgr.GetString("cmbANY_BOAT_AMPHIBIOUS");
			capANY_BOAT_AMPHIBIOUS_DESC.Text	         =	     objResourceMgr.GetString("txtANY_BOAT_AMPHIBIOUS_DESC");
			capANY_BOAT_RESIDENCE.Text					 =       objResourceMgr.GetString("cmbANY_BOAT_RESIDENCE");
			capANY_BOAT_RESIDENCE_DESC.Text				 =	     objResourceMgr.GetString("txtANY_BOAT_RESIDENCE_DESC");

			capIS_BOAT_USED_IN_ANY_WATER.Text			= 		objResourceMgr.GetString("cmbIS_BOAT_USED_IN_ANY_WATER");
			capIS_BOAT_USED_IN_ANY_WATER_DESC.Text		=		objResourceMgr.GetString("txtIS_BOAT_USED_IN_ANY_WATER_DESC");			



		}



		private void Page_Load(object sender, System.EventArgs e)
		{
			btnReset.Attributes.Add("onclick","javascript:ResetForm('APP_WATERCRAFT_GEN_INFO');ResetForm1();return false;");
			imgSelect.Attributes.Add("onclick","javascript:OpenLookupCheck('')");		
			
			#region SETTING SCREEN ID
			if(GetLOBString()=="WAT")
			{
				base.ScreenId="250_0"; 
				hidCalledFrom.Value="WAT";
			}
			else if(GetLOBString()=="HOME")
				//Added for Underwriting Information.
				//base.ScreenId="147_0";
			{
				base.ScreenId="240_0"; 
				hidCalledFrom.Value = "HOME";
			}
			else if(GetLOBString()=="RENT")
				base.ScreenId="165_0"; 				
			#endregion

			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass			=	CmsButtonType.Write ;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass			=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.Watercrafts.PolicyAddWatercraftGenInfo" ,System.Reflection.Assembly.GetExecutingAssembly());
			
			if(!Page.IsPostBack)
			{
				PopulateHiddenFields();
				GenerateXML();
				SetCaptions();
				PopulateComboBox();
				CheckExistanceOfPolicy();

				#region Set Workflow Control
				SetWorkflow();
				#endregion

			}
		}

		#region CheckExistanceOfPolicy
		/// <summary>
		/// 
		/// </summary>
		private void CheckExistanceOfPolicy()
		{
			/* if  for the customer there is an existing policy of  Home 
				Multi Policy discount Field Will be By Default Yes
			*/
			try
			{
				if(GetCustomerID()!="")
				{
					int retValue=ClsWatercraftGenInformation.CheckExistancePolicyHome(int.Parse(GetCustomerID()) );
					if ( retValue >= 1 )
					{
						hidPolicyCount.Value  = "MULTI";
						if (hidCalledFrom.Value == "HOME")
						{
							cmbMULTI_POLICY_DISC_APPLIED.SelectedIndex=2;
							cmbMULTI_POLICY_DISC_APPLIED.Enabled = false;
						}
						//If 1 or more Home pol are existing then get list of Active Policies
						hidActivePolicyList.Value = ClsWatercraftGenInformation.getHomeOwnerActivePolicyList(int.Parse(GetCustomerID()));
					}
					else
					{
						hidPolicyCount.Value  = "";
						hidActivePolicyList.Value	= "";
					}
			

				}
			}
			catch (Exception ex)
			{
				lblMessage.Text	=	ClsMessages.FetchGeneralMessage("728") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		}
		#endregion
		
		/// <summary>
		/// populating hidden fields
		/// </summary>
		private void PopulateHiddenFields()
		{
			hidCUSTOMER_ID.Value    = GetCustomerID().ToString();
			hidPOLICY_ID.Value      = GetPolicyID();
			hidPOLICY_VERSION_ID.Value = GetPolicyVersionID();
			
		}

		private void PopulateComboBox()
		{
			/*cmbHAS_CURR_ADD_THREE_YEARS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbHAS_CURR_ADD_THREE_YEARS.DataTextField="LookupDesc"; 
			cmbHAS_CURR_ADD_THREE_YEARS.DataValueField="LookupCode";
			cmbHAS_CURR_ADD_THREE_YEARS.DataBind();
			cmbHAS_CURR_ADD_THREE_YEARS.Items.Insert(0,"");	
			cmbHAS_CURR_ADD_THREE_YEARS.SelectedIndex=1;  */

			cmbMINOR_VIOLATION.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbMINOR_VIOLATION.DataTextField="LookupDesc"; 
			cmbMINOR_VIOLATION.DataValueField="LookupCode";
			cmbMINOR_VIOLATION.DataBind();
			cmbMINOR_VIOLATION.Items.Insert(0,"");	
			cmbMINOR_VIOLATION.SelectedIndex=1;

			cmbDRINK_DRUG_VOILATION.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbDRINK_DRUG_VOILATION.DataTextField="LookupDesc"; 
			cmbDRINK_DRUG_VOILATION.DataValueField="LookupCode";
			cmbDRINK_DRUG_VOILATION.DataBind();
			cmbDRINK_DRUG_VOILATION.Items.Insert(0,"");	
			cmbDRINK_DRUG_VOILATION.SelectedIndex=1;
			
			cmbPHY_MENTL_CHALLENGED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbPHY_MENTL_CHALLENGED.DataTextField="LookupDesc"; 
			cmbPHY_MENTL_CHALLENGED.DataValueField="LookupCode";
			cmbPHY_MENTL_CHALLENGED.DataBind();
			cmbPHY_MENTL_CHALLENGED.Items.Insert(0,"");
			cmbPHY_MENTL_CHALLENGED.SelectedIndex=1;

			cmbDRIVER_SUS_REVOKED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbDRIVER_SUS_REVOKED.DataTextField="LookupDesc"; 
			cmbDRIVER_SUS_REVOKED.DataValueField="LookupCode";
			cmbDRIVER_SUS_REVOKED.DataBind();
			cmbDRIVER_SUS_REVOKED.Items.Insert(0,"");
			cmbDRIVER_SUS_REVOKED.SelectedIndex=1;

			cmbIS_CONVICTED_ACCIDENT.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbIS_CONVICTED_ACCIDENT.DataTextField="LookupDesc"; 
			cmbIS_CONVICTED_ACCIDENT.DataValueField="LookupCode";
			cmbIS_CONVICTED_ACCIDENT.DataBind();
			cmbIS_CONVICTED_ACCIDENT.Items.Insert(0,"");
			cmbIS_CONVICTED_ACCIDENT.SelectedIndex=1;

			//cmbANY_OTH_INSU_COMP.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			//cmbANY_OTH_INSU_COMP.DataTextField="LookupDesc"; 
			//cmbANY_OTH_INSU_COMP.DataValueField="LookupCode";
			//cmbANY_OTH_INSU_COMP.DataBind();
			//cmbANY_OTH_INSU_COMP.Items.Insert(0,"");
			//cmbANY_OTH_INSU_COMP.SelectedIndex=1;

			cmbANY_LOSS_THREE_YEARS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbANY_LOSS_THREE_YEARS.DataTextField="LookupDesc"; 
			cmbANY_LOSS_THREE_YEARS.DataValueField="LookupCode";
			cmbANY_LOSS_THREE_YEARS.DataBind();
			cmbANY_LOSS_THREE_YEARS.Items.Insert(0,"");
			cmbANY_LOSS_THREE_YEARS.SelectedIndex=1;

			cmbCOVERAGE_DECLINED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbCOVERAGE_DECLINED.DataTextField="LookupDesc"; 
			cmbCOVERAGE_DECLINED.DataValueField="LookupCode";
			cmbCOVERAGE_DECLINED.DataBind();
			cmbCOVERAGE_DECLINED.Items.Insert(0,"");
			cmbCOVERAGE_DECLINED.SelectedIndex=1;

			/*cmbIS_CREDIT.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbIS_CREDIT.DataTextField="LookupDesc"; 
			cmbIS_CREDIT.DataValueField="LookupCode";
			cmbIS_CREDIT.DataBind();
			cmbIS_CREDIT.Items.Insert(0,"");
			cmbIS_CREDIT.SelectedIndex=1;  */

			cmbIS_RENTED_OTHERS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbIS_RENTED_OTHERS.DataTextField="LookupDesc"; 
			cmbIS_RENTED_OTHERS.DataValueField="LookupCode";
			cmbIS_RENTED_OTHERS.DataBind();
			cmbIS_RENTED_OTHERS.Items.Insert(0,"");
			cmbIS_RENTED_OTHERS.SelectedIndex=1;
			
			cmbIS_REGISTERED_OTHERS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbIS_REGISTERED_OTHERS.DataTextField="LookupDesc"; 
			cmbIS_REGISTERED_OTHERS.DataValueField="LookupCode";
			cmbIS_REGISTERED_OTHERS.DataBind();
			cmbIS_REGISTERED_OTHERS.Items.Insert(0,"");
			cmbIS_REGISTERED_OTHERS.SelectedIndex=1;

			cmbPARTICIPATE_RACE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbPARTICIPATE_RACE.DataTextField="LookupDesc"; 
			cmbPARTICIPATE_RACE.DataValueField="LookupCode";
			cmbPARTICIPATE_RACE.DataBind();
			cmbPARTICIPATE_RACE.Items.Insert(0,"");
			cmbPARTICIPATE_RACE.SelectedIndex=1;
			
			cmbCARRY_PASSENGER_FOR_CHARGE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbCARRY_PASSENGER_FOR_CHARGE.DataTextField="LookupDesc"; 
			cmbCARRY_PASSENGER_FOR_CHARGE.DataValueField="LookupCode";
			cmbCARRY_PASSENGER_FOR_CHARGE.DataBind();
			cmbCARRY_PASSENGER_FOR_CHARGE.Items.Insert(0,"");
			cmbCARRY_PASSENGER_FOR_CHARGE.SelectedIndex=1;

			cmbIS_PRIOR_INSURANCE_CARRIER.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbIS_PRIOR_INSURANCE_CARRIER.DataTextField="LookupDesc"; 
			cmbIS_PRIOR_INSURANCE_CARRIER.DataValueField="LookupCode";
			cmbIS_PRIOR_INSURANCE_CARRIER.DataBind();
			cmbIS_PRIOR_INSURANCE_CARRIER.Items.Insert(0,"");
			cmbIS_PRIOR_INSURANCE_CARRIER.SelectedIndex=1;

			
			cmbIS_BOAT_COOWNED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbIS_BOAT_COOWNED.DataTextField="LookupDesc"; 
			cmbIS_BOAT_COOWNED.DataValueField="LookupCode";
			cmbIS_BOAT_COOWNED.DataBind();
			cmbIS_BOAT_COOWNED.Items.Insert(0,"");
			cmbIS_BOAT_COOWNED.SelectedIndex=1;

			//Added By Shafi 20/03/2006
			cmbMULTI_POLICY_DISC_APPLIED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbMULTI_POLICY_DISC_APPLIED.DataTextField="LookupDesc"; 
			cmbMULTI_POLICY_DISC_APPLIED.DataValueField="LookupCode";
			cmbMULTI_POLICY_DISC_APPLIED.DataBind();
			cmbMULTI_POLICY_DISC_APPLIED.Items.Insert(0,"");

			cmbANY_BOAT_AMPHIBIOUS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbANY_BOAT_AMPHIBIOUS.DataTextField="LookupDesc"; 
			cmbANY_BOAT_AMPHIBIOUS.DataValueField="LookupCode";
			cmbANY_BOAT_AMPHIBIOUS.DataBind();
			cmbANY_BOAT_AMPHIBIOUS.Items.Insert(0,"");

			cmbANY_BOAT_RESIDENCE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbANY_BOAT_RESIDENCE.DataTextField="LookupDesc"; 
			cmbANY_BOAT_RESIDENCE.DataValueField="LookupCode";
			cmbANY_BOAT_RESIDENCE.DataBind();
			cmbANY_BOAT_RESIDENCE.Items.Insert(0,"");

			cmbIS_BOAT_USED_IN_ANY_WATER.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbIS_BOAT_USED_IN_ANY_WATER.DataTextField="LookupDesc"; 
			cmbIS_BOAT_USED_IN_ANY_WATER.DataValueField="LookupCode";
			cmbIS_BOAT_USED_IN_ANY_WATER.DataBind();
			cmbIS_BOAT_USED_IN_ANY_WATER.Items.Insert(0,"");


		}

		/// <summary>
		/// Fetching data from app_home_rating_info and saving in hidOldData hidden fields
		/// </summary>
		private void GenerateXML()
		{
			Cms.BusinessLayer.BlApplication.ClsWatercraftGenInformation objWatercraftGenInfo=new ClsWatercraftGenInformation();  
			try
			{
				DataSet ds=new DataSet(); 
				ds=objWatercraftGenInfo.FetchPolicyWaterCraftGenInfo(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value));
				if(ds.Tables[0].Rows.Count>0 )
					hidOldData.Value=ds.GetXml(); 

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
				if(objWatercraftGenInfo!= null)
					objWatercraftGenInfo.Dispose();
			}  
		}




		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private Cms.Model.Policy.Watercraft.ClsPolicyWatercraftGenInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			Cms.Model.Policy.Watercraft.ClsPolicyWatercraftGenInfo objWatercraftGenInfo = new Cms.Model.Policy.Watercraft.ClsPolicyWatercraftGenInfo();
			
			//objWatercraftGenInfo.HAS_CURR_ADD_THREE_YEARS       =	cmbHAS_CURR_ADD_THREE_YEARS.SelectedValue;			
			objWatercraftGenInfo.PHY_MENTL_CHALLENGED           =	cmbPHY_MENTL_CHALLENGED.SelectedValue;
			objWatercraftGenInfo.DRIVER_SUS_REVOKED             =	cmbDRIVER_SUS_REVOKED.SelectedValue;
			objWatercraftGenInfo.IS_CONVICTED_ACCIDENT          =	cmbIS_CONVICTED_ACCIDENT.SelectedValue;
			//objWatercraftGenInfo.ANY_OTH_INSU_COMP              =	cmbANY_OTH_INSU_COMP.SelectedValue;
			objWatercraftGenInfo.MINOR_VIOLATION				=	cmbMINOR_VIOLATION.SelectedValue;
			objWatercraftGenInfo.DRINK_DRUG_VOILATION			=	cmbDRINK_DRUG_VOILATION.SelectedValue;
			//if(cmbANY_OTH_INSU_COMP.SelectedValue=="1")
			//	objWatercraftGenInfo.OTHER_POLICY_NUMBER_LIST       =	txtOTHER_POLICY_NUMBER_LIST.Text;
			//else
				objWatercraftGenInfo.OTHER_POLICY_NUMBER_LIST       =	System.DBNull.Value.ToString() ;

			objWatercraftGenInfo.ANY_LOSS_THREE_YEARS           =	cmbANY_LOSS_THREE_YEARS.SelectedValue;
			objWatercraftGenInfo.COVERAGE_DECLINED              =	cmbCOVERAGE_DECLINED.SelectedValue;
			 objWatercraftGenInfo.IS_BOAT_COOWNED               =	cmbIS_BOAT_COOWNED.SelectedValue;
			//objWatercraftGenInfo.IS_CREDIT                      =	cmbIS_CREDIT.SelectedValue;
			/*if(cmbIS_CREDIT.SelectedValue=="1")
				objWatercraftGenInfo.CREDIT_DETAILS             =	txtCREDIT_DETAILS.Text;
			else
				objWatercraftGenInfo.CREDIT_DETAILS             =	System.DBNull.Value.ToString();	*/
   
			objWatercraftGenInfo.IS_RENTED_OTHERS               =	cmbIS_RENTED_OTHERS.SelectedValue;
			objWatercraftGenInfo.IS_REGISTERED_OTHERS           =	cmbIS_REGISTERED_OTHERS.SelectedValue;
			objWatercraftGenInfo.CUSTOMER_ID                    =   hidCUSTOMER_ID.Value==""?0:int.Parse(hidCUSTOMER_ID.Value);
			objWatercraftGenInfo.POLICY_ID                      =   hidPOLICY_ID.Value==""?0:int.Parse(hidPOLICY_ID.Value); 
			objWatercraftGenInfo.POLICY_VERSION_ID              =   hidPOLICY_VERSION_ID.Value==""?0:int.Parse(hidPOLICY_VERSION_ID.Value); 

			/*if(cmbHAS_CURR_ADD_THREE_YEARS.SelectedValue=="1")
				objWatercraftGenInfo.HAS_CURR_ADD_THREE_YEARS_DESC	= txtHAS_CURR_ADD_THREE_YEARS_DESC.Text;
			else
				objWatercraftGenInfo.HAS_CURR_ADD_THREE_YEARS_DESC	= System.DBNull.Value.ToString();  */ 

			if(cmbPHY_MENTL_CHALLENGED.SelectedValue=="1")
				objWatercraftGenInfo.PHY_MENTL_CHALLENGED_DESC		= txtPHY_MENTL_CHALLENGED_DESC.Text;
			else
				objWatercraftGenInfo.PHY_MENTL_CHALLENGED_DESC		= System.DBNull.Value.ToString();   

			if(cmbDRIVER_SUS_REVOKED.SelectedValue=="1")
				objWatercraftGenInfo.DRIVER_SUS_REVOKED_DESC		=txtDRIVER_SUS_REVOKED_DESC.Text;
			else
				objWatercraftGenInfo.DRIVER_SUS_REVOKED_DESC		= System.DBNull.Value.ToString();

			if(cmbIS_CONVICTED_ACCIDENT.SelectedValue=="1")
				objWatercraftGenInfo.IS_CONVICTED_ACCIDENT_DESC		= txtIS_CONVICTED_ACCIDENT_DESC.Text;
			else
				objWatercraftGenInfo.IS_CONVICTED_ACCIDENT_DESC		= System.DBNull.Value.ToString();

			if(cmbANY_LOSS_THREE_YEARS.SelectedValue=="1")
				objWatercraftGenInfo.ANY_LOSS_THREE_YEARS_DESC		= txtANY_LOSS_THREE_YEARS_DESC.Text;
			else
				objWatercraftGenInfo.ANY_LOSS_THREE_YEARS_DESC		= System.DBNull.Value.ToString();

			if(cmbCOVERAGE_DECLINED.SelectedValue=="1")
				objWatercraftGenInfo.COVERAGE_DECLINED_DESC			= txtCOVERAGE_DECLINED_DESC.Text;
			else
				objWatercraftGenInfo.COVERAGE_DECLINED_DESC			= System.DBNull.Value.ToString();
			if(cmbIS_RENTED_OTHERS.SelectedValue=="1")
				objWatercraftGenInfo.IS_RENTED_OTHERS_DESC			= txtIS_RENTED_OTHERS_DESC.Text; 
			else
				objWatercraftGenInfo.IS_RENTED_OTHERS_DESC			= System.DBNull.Value.ToString();

			if(cmbIS_REGISTERED_OTHERS.SelectedValue=="1")
				objWatercraftGenInfo.IS_REGISTERED_OTHERS_DESC		= txtIS_REGISTERED_OTHERS_DESC.Text;
			else
				objWatercraftGenInfo.IS_REGISTERED_OTHERS_DESC		= System.DBNull.Value.ToString();

			if(cmbIS_BOAT_COOWNED.SelectedValue=="1")
				objWatercraftGenInfo.IS_BOAT_COOWNED_DESC	 = txtIS_BOAT_COOWNED_DESC.Text;
			else
				objWatercraftGenInfo.IS_BOAT_COOWNED_DESC		= System.DBNull.Value.ToString();

			if(cmbPARTICIPATE_RACE.SelectedItem!=null)  
			{
				objWatercraftGenInfo.PARTICIPATE_RACE=cmbPARTICIPATE_RACE.SelectedItem.Value;   
				if(cmbPARTICIPATE_RACE.SelectedItem.Value=="1")
				{
					objWatercraftGenInfo.PARTICIPATE_RACE_DESC=txtPARTICIPATE_RACE_DESC.Text.Trim(); 
				}
				else
				{
					objWatercraftGenInfo.PARTICIPATE_RACE_DESC=System.DBNull.Value.ToString();
				}
			}
			else
			{
				objWatercraftGenInfo.PARTICIPATE_RACE=System.DBNull.Value.ToString();   
				objWatercraftGenInfo.PARTICIPATE_RACE_DESC=System.DBNull.Value.ToString();
			}

			if(cmbCARRY_PASSENGER_FOR_CHARGE.SelectedItem!=null)  
			{
				objWatercraftGenInfo.CARRY_PASSENGER_FOR_CHARGE=cmbCARRY_PASSENGER_FOR_CHARGE.SelectedItem.Value;   
				if(cmbCARRY_PASSENGER_FOR_CHARGE.SelectedItem.Value=="1")
				{
					objWatercraftGenInfo.CARRY_PASSENGER_FOR_CHARGE_DESC=txtCARRY_PASSENGER_FOR_CHARGE_DESC.Text.Trim(); 
				}
				else
				{
					objWatercraftGenInfo.CARRY_PASSENGER_FOR_CHARGE_DESC=System.DBNull.Value.ToString();
				}
			}
			else
			{
				objWatercraftGenInfo.CARRY_PASSENGER_FOR_CHARGE=System.DBNull.Value.ToString();   
				objWatercraftGenInfo.CARRY_PASSENGER_FOR_CHARGE_DESC=System.DBNull.Value.ToString() ;
			}

			if(cmbIS_PRIOR_INSURANCE_CARRIER.SelectedItem!=null)  
			{
				objWatercraftGenInfo.IS_PRIOR_INSURANCE_CARRIER =cmbIS_PRIOR_INSURANCE_CARRIER.SelectedItem.Value ;
				
				if(cmbIS_PRIOR_INSURANCE_CARRIER.SelectedItem.Value =="1")
				{
					objWatercraftGenInfo.PRIOR_INSURANCE_CARRIER_DESC =txtPRIOR_INSURANCE_CARRIER_DESC.Text.Trim() ;
				}
				else
				{
					
					objWatercraftGenInfo.PRIOR_INSURANCE_CARRIER_DESC =System.DBNull.Value.ToString();
				}
			}
			else
			{   
				objWatercraftGenInfo.IS_PRIOR_INSURANCE_CARRIER=System.DBNull.Value.ToString();   
				objWatercraftGenInfo.PRIOR_INSURANCE_CARRIER_DESC=System.DBNull.Value.ToString() ;
			}
			//Added By Shafi
			
			objWatercraftGenInfo.MULTI_POLICY_DISC_APPLIED 		=	cmbMULTI_POLICY_DISC_APPLIED.SelectedValue;
			
			if(cmbMULTI_POLICY_DISC_APPLIED.SelectedValue == "0")
			{
				txtMULTI_POLICY_DISC_APPLIED_PP_DESC.Text ="";
			}
			objWatercraftGenInfo.MULTI_POLICY_DISC_APPLIED_PP_DESC =	txtMULTI_POLICY_DISC_APPLIED_PP_DESC.Text;

			// ANY_BOAT_AMPHIBIOUS
			objWatercraftGenInfo.ANY_BOAT_AMPHIBIOUS 		=	cmbANY_BOAT_AMPHIBIOUS.SelectedValue;
			
			if(cmbANY_BOAT_AMPHIBIOUS.SelectedValue == "0")
			{
				txtANY_BOAT_AMPHIBIOUS_DESC.Text ="";
			}
			objWatercraftGenInfo.ANY_BOAT_AMPHIBIOUS_DESC =	txtANY_BOAT_AMPHIBIOUS_DESC.Text;

			// ANY_BOAT_RESIDENCE
			objWatercraftGenInfo.ANY_BOAT_RESIDENCE 		=	cmbANY_BOAT_RESIDENCE.SelectedValue;
			
			if(cmbANY_BOAT_RESIDENCE.SelectedValue == "0")
			{
				txtANY_BOAT_RESIDENCE_DESC.Text ="";
			}
			objWatercraftGenInfo.ANY_BOAT_RESIDENCE_DESC =	txtANY_BOAT_RESIDENCE_DESC.Text;

			//Is Any Boat in Any waters
			if(cmbIS_BOAT_USED_IN_ANY_WATER.SelectedItem!=null)
			{
				objWatercraftGenInfo.IS_BOAT_USED_IN_ANY_WATER		  = cmbIS_BOAT_USED_IN_ANY_WATER.SelectedItem.Value ;
				if(cmbIS_BOAT_USED_IN_ANY_WATER.SelectedItem.Value =="1")
				{
					objWatercraftGenInfo.IS_BOAT_USED_IN_ANY_WATER_DESC =	txtIS_BOAT_USED_IN_ANY_WATER_DESC.Text.Trim() ;
				}
				else
				{
					objWatercraftGenInfo.IS_BOAT_USED_IN_ANY_WATER_DESC = System.DBNull.Value.ToString();
				}

			}
			else
			{   
				objWatercraftGenInfo.IS_BOAT_USED_IN_ANY_WATER		  =	System.DBNull.Value.ToString();   
				objWatercraftGenInfo.IS_BOAT_USED_IN_ANY_WATER_DESC	  =	System.DBNull.Value.ToString() ;
			}


			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			if(hidOldData.Value=="0")
				strRowId	=	hidRowId.Value;
			else
				strRowId	=	"";
            
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objWatercraftGenInfo;
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
				objWatercraftGenInformation = new  ClsWatercraftGenInformation();

				//Retreiving the form values into model class object
				Cms.Model.Policy.Watercraft.ClsPolicyWatercraftGenInfo objWatercraftGenInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objWatercraftGenInfo.CREATED_BY = int.Parse(GetUserId());
					objWatercraftGenInfo.CREATED_DATETIME = DateTime.Now;
					objWatercraftGenInfo.IS_ACTIVE="Y";
					//Calling the add method of business layer class
					intRetVal = objWatercraftGenInformation.AddPolicyWaterCraftGenInfo(objWatercraftGenInfo);

					if(intRetVal>0)
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value		=	"1";
						hidRowId.Value          =   "1";
						hidIS_ACTIVE.Value = "Y";

						//seting the tworkflow
						SetWorkflow();

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
					Cms.Model.Policy.Watercraft.ClsPolicyWatercraftGenInfo objOldWatercraftGenInfo = new Cms.Model.Policy.Watercraft.ClsPolicyWatercraftGenInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldWatercraftGenInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
                    
					objWatercraftGenInfo.MODIFIED_BY = int.Parse(GetUserId());
					objWatercraftGenInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                    
					//Updating the record using business layer class object
					intRetVal	= objWatercraftGenInformation.UpdatePolicyWaterCraftGenInfo(objOldWatercraftGenInfo,objWatercraftGenInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidRowId.Value          =   "1";
						hidFormSaved.Value		=	"0";

						//seting the tworkflow
						SetWorkflow();

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
				// ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objWatercraftGenInformation!= null)
					objWatercraftGenInformation.Dispose();
			}
			GenerateXML();
		}
		#endregion


		private void SetWorkflow()
		{
			if(base.ScreenId	==	"250_0" || base.ScreenId == "147_0" || base.ScreenId == "165_0" || base.ScreenId == "240_0")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule="POL";
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}

	}
}
