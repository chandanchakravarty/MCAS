/******************************************************************************************
<Author				: -   Anshuman
<Start Date				: -	5/18/2005 4:42:46 PM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
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
using Cms.CmsWeb.WebControls;
using Cms.BusinessLayer.BlApplication;
using Cms.Model.Policy.Homeowners;

namespace Cms.Policies.aspx.Homeowners
{
	/// <summary>
	/// Summary description for PolicyHomeownerGeneralInfo.
	/// </summary>
	public class PolicyHomeownerGeneralInfo : Cms.Policies.policiesbase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.CustomValidator csvDESC_Location;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.Label capANY_FARMING_BUSINESS_COND;
		protected System.Web.UI.WebControls.DropDownList cmbANY_FARMING_BUSINESS_COND;
		protected System.Web.UI.WebControls.Label capDESC_BUSINESS;
		//protected System.Web.UI.WebControls.TextBox txtDESC_BUSINESS;
		protected System.Web.UI.WebControls.Label capANY_RESIDENCE_EMPLOYEE;
		protected System.Web.UI.WebControls.DropDownList cmbANY_RESIDENCE_EMPLOYEE;
		protected System.Web.UI.WebControls.Label capDESC_RESIDENCE_EMPLOYEE;
		protected System.Web.UI.WebControls.TextBox txtDESC_RESIDENCE_EMPLOYEE;
		protected System.Web.UI.WebControls.Label capANY_OTHER_RESI_OWNED;
		protected System.Web.UI.WebControls.DropDownList cmbANY_OTHER_RESI_OWNED;
		protected System.Web.UI.WebControls.Label capDESC_OTHER_RESIDENCE;
		protected System.Web.UI.WebControls.TextBox txtDESC_OTHER_RESIDENCE;
		protected System.Web.UI.WebControls.Label capANY_OTH_INSU_COMP;
		protected System.Web.UI.WebControls.DropDownList cmbANY_OTH_INSU_COMP;
		protected System.Web.UI.WebControls.Label capDESC_OTHER_INSURANCE;
		protected System.Web.UI.WebControls.TextBox txtDESC_OTHER_INSURANCE;
		protected System.Web.UI.WebControls.Label capHAS_INSU_TRANSFERED_AGENCY;
		protected System.Web.UI.WebControls.DropDownList cmbHAS_INSU_TRANSFERED_AGENCY;
		protected System.Web.UI.WebControls.Label capDESC_INSU_TRANSFERED_AGENCY;
		protected System.Web.UI.WebControls.TextBox txtDESC_INSU_TRANSFERED_AGENCY;
		protected System.Web.UI.WebControls.Label capANY_COV_DECLINED_CANCELED;
		protected System.Web.UI.WebControls.DropDownList cmbANY_COV_DECLINED_CANCELED;
		protected System.Web.UI.WebControls.Label capDESC_COV_DECLINED_CANCELED;
		protected System.Web.UI.WebControls.TextBox txtDESC_COV_DECLINED_CANCELED;
		protected System.Web.UI.WebControls.Label capBREED;
		protected System.Web.UI.WebControls.TextBox txtBREED;
		protected System.Web.UI.WebControls.Label capCONVICTION_DEGREE_IN_PAST;
		protected System.Web.UI.WebControls.DropDownList cmbCONVICTION_DEGREE_IN_PAST;
		protected System.Web.UI.WebControls.Label capDESC_CONVICTION_DEGREE_IN_PAST;
		protected System.Web.UI.WebControls.TextBox txtDESC_CONVICTION_DEGREE_IN_PAST;
		protected System.Web.UI.WebControls.Label capANY_RENOVATION;
		protected System.Web.UI.WebControls.DropDownList cmbANY_RENOVATION;
		protected System.Web.UI.WebControls.Label capDESC_RENOVATION;
		protected System.Web.UI.WebControls.TextBox txtDESC_RENOVATION;
		protected System.Web.UI.WebControls.Label capTRAMPOLINE;
		protected System.Web.UI.WebControls.DropDownList cmbTRAMPOLINE;
		protected System.Web.UI.WebControls.Label capDESC_TRAMPOLINE;
		protected System.Web.UI.WebControls.TextBox txtDESC_TRAMPOLINE;
		protected System.Web.UI.WebControls.Label capLEAD_PAINT_HAZARD;
		protected System.Web.UI.WebControls.DropDownList cmbLEAD_PAINT_HAZARD;
		protected System.Web.UI.WebControls.Label capDESC_LEAD_PAINT_HAZARD;
		protected System.Web.UI.WebControls.TextBox txtDESC_LEAD_PAINT_HAZARD;
		protected System.Web.UI.WebControls.Label capRENTERS;
		protected System.Web.UI.WebControls.DropDownList cmbRENTERS;
		protected System.Web.UI.WebControls.Label capDESC_RENTERS;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDESC_RENTERS;
		protected System.Web.UI.WebControls.TextBox txtDESC_RENTERS;
		protected System.Web.UI.WebControls.Label capBUILD_UNDER_CON_GEN_CONT;
		protected System.Web.UI.WebControls.DropDownList cmbBUILD_UNDER_CON_GEN_CONT;
		protected System.Web.UI.WebControls.Label capREMARKS;
		protected System.Web.UI.WebControls.TextBox txtREMARKS;
		protected System.Web.UI.HtmlControls.HtmlTableRow trError;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOL_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOL_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidStateID;
		protected System.Web.UI.WebControls.RangeValidator rngYEARS_INSU;
		protected System.Web.UI.WebControls.RangeValidator rngYEARS_INSU_WOL;

		//ADDED by RP
		protected System.Web.UI.WebControls.DropDownList cmbPROPERTY_ON_MORE_THAN;
		protected System.Web.UI.WebControls.TextBox	txtPROPERTY_ON_MORE_THAN_DESC;
		protected System.Web.UI.WebControls.DropDownList cmbDWELLING_MOBILE_HOME;
		protected System.Web.UI.WebControls.TextBox	txtDWELLING_MOBILE_HOME_DESC;
		protected System.Web.UI.WebControls.DropDownList cmbPROPERTY_USED_WHOLE_PART;
		protected System.Web.UI.WebControls.TextBox	txtPROPERTY_USED_WHOLE_PART_DESC;
		protected System.Web.UI.HtmlControls.HtmlTableRow trPROPERTY_ON_MORE_THAN;
		protected System.Web.UI.HtmlControls.HtmlTableRow trDWELLING_MOBILE_HOME;
		protected System.Web.UI.HtmlControls.HtmlTableRow trPROPERTY_USED_WHOLE_PART;
		protected System.Web.UI.WebControls.HyperLink hlkDESC_INSU_TRANSFERED_AGENCY;
		protected System.Web.UI.WebControls.RegularExpressionValidator regDESC_INSU_TRANSFERED_AGENCY;	
		//End of addition by RP
		protected System.Web.UI.HtmlControls.HtmlTableRow trIsAny_Horse;//Added by Charles on 9-Dec-09 for Itrack 6489
		
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;

		//---------------Added by mohit-----------------
		protected System.Web.UI.WebControls.Label capDESC_OWNER;
		//protected System.Web.UI.WebControls.Label capDESC_DECLINE;
		//End 
		//Added by Sumit for new description fields on 30-03-2006
		protected System.Web.UI.WebControls.Label capDESC_IS_SWIMPOLL_HOTTUB;
		protected System.Web.UI.WebControls.TextBox txtDESC_IS_SWIMPOLL_HOTTUB;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_IS_SWIMPOLL_HOTTUB;
		protected System.Web.UI.WebControls.Label capDESC_MULTI_POLICY_DISC_APPLIED;
		protected System.Web.UI.WebControls.TextBox txtDESC_MULTI_POLICY_DISC_APPLIED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_MULTI_POLICY_DISC_APPLIED;
		protected System.Web.UI.WebControls.Label capDESC_BUILD_UNDER_CON_GEN_CONT;
		protected System.Web.UI.WebControls.TextBox txtDESC_BUILD_UNDER_CON_GEN_CONT;
		protected System.Web.UI.WebControls.Label lblDESC_BUILD_UNDER_CON_GEN_CONT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_BUILD_UNDER_CON_GEN_CONT;
		protected System.Web.UI.WebControls.Label lblDESC_IS_SWIMPOLL_HOTTUB;
		protected System.Web.UI.WebControls.Label lblDESC_MULTI_POLICY_DISC_APPLIED;

		protected System.Web.UI.WebControls.Label capVALUED_CUSTOMER_DISCOUNT_OVERRIDE;			
		protected System.Web.UI.WebControls.DropDownList cmbVALUED_CUSTOMER_DISCOUNT_OVERRIDE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVALUED_CUSTOMER_DISCOUNT_OVERRIDE;
		protected System.Web.UI.WebControls.Label capVALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC;					
		protected System.Web.UI.WebControls.TextBox txtVALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC;
		protected System.Web.UI.WebControls.Label lblVALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC;
		protected System.Web.UI.WebControls.Label capBUILT_ON_CONTINUOUS_FOUNDATION;
		protected System.Web.UI.WebControls.Label capPROVIDE_HOME_DAY_CARE;
		protected System.Web.UI.WebControls.DropDownList cmbBUILT_ON_CONTINUOUS_FOUNDATION;
		protected System.Web.UI.WebControls.DropDownList cmbPROVIDE_HOME_DAY_CARE;

		protected System.Web.UI.WebControls.Label lblPROVIDE_HOME_DAY_CARE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPROVIDE_HOME_DAY_CARE;
		protected System.Web.UI.WebControls.Label capMODULAR_MANUFACTURED_HOME;
		protected System.Web.UI.WebControls.DropDownList cmbMODULAR_MANUFACTURED_HOME;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator1;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator2;
		protected System.Web.UI.WebControls.Label lblBUILT_ON_CONTINUOUS_FOUNDATION;

		#endregion

		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		public string strCalledFrom =  "";
		public string strOTHER_DESCRIPTION_VALUE = "";//Added by Charles on 9-Dec-09 for Itrack 6489
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		// private int	intLoggedInUserID;
		protected System.Web.UI.HtmlControls.HtmlForm POL_HOME_OWNER_GEN_INFO;
		//protected System.Web.UI.WebControls.Label lblDESC_BUSINESS;
		protected System.Web.UI.WebControls.Label lblDESC_RESIDENCE_EMPLOYEE;
		protected System.Web.UI.WebControls.Label lblDESC_OTHER_RESIDENCE;
		protected System.Web.UI.WebControls.Label lblDESC_OTHER_INSURANCE;
		protected System.Web.UI.WebControls.Label lblDESC_INSU_TRANSFERED_AGENCY;
		protected System.Web.UI.WebControls.Label lblDESC_COV_DECLINED_CANCELED;
		protected System.Web.UI.WebControls.Label lblDESC_CONVICTION_DEGREE_IN_PAST;
		protected System.Web.UI.WebControls.Label lblDESC_RENOVATION;
		protected System.Web.UI.WebControls.Label lblDESC_TRAMPOLINE;
		protected System.Web.UI.WebControls.Label lblDESC_LEAD_PAINT_HAZARD;
		protected System.Web.UI.WebControls.Label lblDESC_RENTERS;
		protected System.Web.UI.WebControls.Label lblREMARK;
		protected System.Web.UI.WebControls.Label capMULTI_POLICY_DISC_APPLIED;
		protected System.Web.UI.WebControls.DropDownList cmbMULTI_POLICY_DISC_APPLIED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMULTI_POLICY_DISC_APPLIED;
		protected System.Web.UI.WebControls.Label capNO_OF_PETS;
		protected System.Web.UI.WebControls.DropDownList cmbNO_OF_PETS;
		protected System.Web.UI.WebControls.CustomValidator csvREMARKS;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revDESC_BUSINESS;
		protected System.Web.UI.WebControls.Label capIS_SWIMPOLL_HOTTUB;
		protected System.Web.UI.WebControls.DropDownList cmbIS_SWIMPOLL_HOTTUB;
		protected System.Web.UI.WebControls.Label capANIMALS_EXO_PETS_HISTORY;
		protected System.Web.UI.WebControls.DropDownList cmbANIMALS_EXO_PETS_HISTORY;
		protected System.Web.UI.WebControls.Label capOTHER_DESCRIPTION;
		protected System.Web.UI.HtmlControls.HtmlSelect cmbOTHER_DESCRIPTION; //Changed from WebControls.DropDownList by Charles on 9-Dec-09 for Itrack 6489
		protected System.Web.UI.HtmlControls.HtmlInputText txtOTHER_DESCRIPTION;//Added by Charles on 9-Dec-09 for Itrack 6489
		protected System.Web.UI.WebControls.Label lblOTHER_DESCRIPTION;
		protected System.Web.UI.WebControls.Label lblBREED;
		protected System.Web.UI.WebControls.Label capLAST_INSPECTED_DATE;
		protected System.Web.UI.WebControls.TextBox txtLAST_INSPECTED_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkLAST_INSPECTED_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLAST_INSPECTED_DATE;
		protected System.Web.UI.WebControls.CustomValidator csvLAST_INSPECTED_DATE;
		protected System.Web.UI.WebControls.Label capIS_VACENT_OCCUPY;
		protected System.Web.UI.WebControls.DropDownList cmbIS_VACENT_OCCUPY;
		protected System.Web.UI.WebControls.Label capIS_RENTED_IN_PART;
		protected System.Web.UI.WebControls.DropDownList cmbIS_RENTED_IN_PART;
		protected System.Web.UI.WebControls.Label capIS_DWELLING_OWNED_BY_OTHER;
		protected System.Web.UI.WebControls.DropDownList cmbIS_DWELLING_OWNED_BY_OTHER;
		protected System.Web.UI.WebControls.Label capIS_PROP_NEXT_COMMERICAL;
		protected System.Web.UI.WebControls.DropDownList cmbIS_PROP_NEXT_COMMERICAL;
		protected System.Web.UI.WebControls.Label capDESC_PROPERTY;
		protected System.Web.UI.WebControls.TextBox txtDESC_PROPERTY;
		protected System.Web.UI.WebControls.Label capARE_STAIRWAYS_PRESENT;
		protected System.Web.UI.WebControls.DropDownList cmbARE_STAIRWAYS_PRESENT;
		protected System.Web.UI.WebControls.Label capDESC_STAIRWAYS;
		protected System.Web.UI.WebControls.TextBox txtDESC_STAIRWAYS;
		protected System.Web.UI.WebControls.Label capARE_ANY_ANIMAL;
		protected System.Web.UI.WebControls.DropDownList cmbARE_ANY_ANIMAL;
		protected System.Web.UI.WebControls.Label capDESC_ANIMALS;
		protected System.Web.UI.WebControls.TextBox txtDESC_ANIMALS;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist3;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox Textbox1;
		protected System.Web.UI.WebControls.HyperLink Hyperlink1;
		protected System.Web.UI.WebControls.RegularExpressionValidator Regularexpressionvalidator1;
		protected System.Web.UI.WebControls.CustomValidator Customvalidator1;
		protected System.Web.UI.WebControls.Label capIS_OWNERS_DWELLING_CHANGED;
		protected System.Web.UI.WebControls.DropDownList cmbIS_OWNERS_DWELLING_CHANGED;
		protected System.Web.UI.WebControls.TextBox Textbox2;
		protected System.Web.UI.WebControls.Label Label11;
		//protected System.Web.UI.WebControls.Label capIS_INSURANCE_DEC_CANCEL;
		//protected System.Web.UI.WebControls.DropDownList cmbIS_INSURANCE_DEC_CANCEL;
		//protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.TextBox txtDESC_DECLINE;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label lblDESC_PROPERTY;
		protected System.Web.UI.WebControls.Label lblDESC_STAIRWAYS;
		protected System.Web.UI.WebControls.Label lblDESC_ANIMALS;
		protected System.Web.UI.WebControls.TextBox txtDESC_OWNER;
		protected System.Web.UI.WebControls.Label lblDESC_OWNER;
		protected System.Web.UI.WebControls.Label lblDESC_DECLINE;
		protected System.Web.UI.WebControls.Label capDESC_VACENT_OCCUPY;
		protected System.Web.UI.WebControls.TextBox txtDESC_VACENT_OCCUPY;
		protected System.Web.UI.WebControls.Label lblDESC_VACENT_OCCUPY;
		protected System.Web.UI.WebControls.Label capDESC_RENTED_IN_PART;
		protected System.Web.UI.WebControls.TextBox txtDESC_RENTED_IN_PART;
		protected System.Web.UI.WebControls.Label lblDESC_RENTED_IN_PART;
		protected System.Web.UI.WebControls.Label capDESC_DWELLING_OWNED_BY_OTHER;
		protected System.Web.UI.WebControls.TextBox txtDESC_DWELLING_OWNED_BY_OTHER;
		protected System.Web.UI.WebControls.Label lblDESC_DWELLING_OWNED_BY_OTHER;
		protected System.Web.UI.WebControls.Label capANY_HEATING_SOURCE;
		protected System.Web.UI.WebControls.DropDownList cmbANY_HEATING_SOURCE;
		protected System.Web.UI.WebControls.TextBox txtDESC_ANY_HEATING_SOURCE;
		protected System.Web.UI.WebControls.Label lblDESC_ANY_HEATING_SOURCE;
		protected System.Web.UI.WebControls.Label capDESC_ANY_HEATING_SOURCE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDESC_RESIDENCE_EMPLOYEE;
		protected System.Web.UI.WebControls.Label capNON_SMOKER_CREDIT;
		protected System.Web.UI.WebControls.DropDownList cmbNON_SMOKER_CREDIT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_VACENT_OCCUPY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_RENTED_IN_PART;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_DWELLING_OWNED_BY_OTHER;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_BUSINESS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_PROPERTY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_STAIRWAYS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBREED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvOTHER_DESCRIPTION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_INSU_TRANSFERED_AGENCY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_OWNER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_COV_DECLINED_CANCELED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_CONVICTION_DEGREE_IN_PAST;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_LEAD_PAINT_HAZARD;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_RESIDENCE_EMPLOYEE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_OTHER_RESIDENCE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_OTHER_INSURANCE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_RENOVATION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_TRAMPOLINE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_RENTERS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_ANY_HEATING_SOURCE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_VACENT_OCCUPY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_DWELLING_OWNED_BY_OTHER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_RENTED_IN_PART;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_FARMING_BUSINESS_COND;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_PROP_NEXT_COMMERICAL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvARE_STAIRWAYS_PRESENT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANIMALS_EXO_PETS_HISTORY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNO_OF_PETS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_SWIMPOLL_HOTTUB;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHAS_INSU_TRANSFERED_AGENCY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_OWNERS_DWELLING_CHANGED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_COV_DECLINED_CANCELED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCONVICTION_DEGREE_IN_PAST;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_RESIDENCE_EMPLOYEE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLEAD_PAINT_HAZARD;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_OTHER_RESI_OWNED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_OTH_INSU_COMP;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_RENOVATION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTRAMPOLINE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvRENTERS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBUILD_UNDER_CON_GEN_CONT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_HEATING_SOURCE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNON_SMOKER_CREDIT;
		protected System.Web.UI.WebControls.Label capSWIMMING_POOL;
		protected System.Web.UI.WebControls.DropDownList cmbSWIMMING_POOL;
		protected System.Web.UI.WebControls.Label capSWIMMING_POOL_TYPE;
		protected System.Web.UI.WebControls.Label lblSWIMMING_POOL_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbSWIMMING_POOL_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSWIMMING_POOL;
		protected System.Web.UI.WebControls.Label capAny_Forming;
		protected System.Web.UI.WebControls.DropDownList cmbAny_Forming;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAny_Forming;
		protected System.Web.UI.WebControls.Label capPremises;
		protected System.Web.UI.WebControls.DropDownList cmbPremises;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPremises;
		protected System.Web.UI.WebControls.Label capOf_Acres;
		protected System.Web.UI.WebControls.TextBox txtOf_Acres;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvOf_Acres;
		protected System.Web.UI.WebControls.RegularExpressionValidator revOf_Acres;
		protected System.Web.UI.WebControls.Label capIsAny_Horse;
		protected System.Web.UI.WebControls.Label capLocation;
		protected System.Web.UI.WebControls.DropDownList cmbIsAny_Horse;
		protected System.Web.UI.WebControls.TextBox txtLocation;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLocation;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLocation;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIsAny_Horse;
		//protected System.Web.UI.WebControls.Label capOf_Acres_P;
		protected System.Web.UI.WebControls.Label capDESC_Location;
		//protected System.Web.UI.WebControls.TextBox txtOf_Acres_P;
		protected System.Web.UI.WebControls.TextBox txtDESC_Location;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_Location;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvOf_Acres_P;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revOf_Acres_P;
		protected System.Web.UI.WebControls.Label capNo_Horses;
		protected System.Web.UI.WebControls.TextBox txtNo_Horses;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNo_Horses;
		protected System.Web.UI.WebControls.RegularExpressionValidator revNo_Horses;
		protected System.Web.UI.WebControls.Panel isForming;
		protected System.Web.UI.HtmlControls.HtmlTableRow rowPremises;
		protected System.Web.UI.HtmlControls.HtmlTableRow rowHorse;
		protected System.Web.UI.HtmlControls.HtmlTableRow rowhorsetext;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.WebControls.Label capYEARS_INSU;
		protected System.Web.UI.WebControls.TextBox txtYEARS_INSU;
		protected System.Web.UI.WebControls.RegularExpressionValidator revYEARS_INSU;
		protected System.Web.UI.WebControls.Label capYEARS_INSU_WOL;
		protected System.Web.UI.WebControls.TextBox txtYEARS_INSU_WOL;
		protected System.Web.UI.WebControls.RegularExpressionValidator revYEARS_INSU_WOL;
		protected System.Web.UI.WebControls.CustomValidator csvYEARS_INSU_WOL;
		protected System.Web.UI.WebControls.Label capAPPROVED_FENCE;
		protected System.Web.UI.WebControls.DropDownList cmbAPPROVED_FENCE;
		protected System.Web.UI.WebControls.Label capDIVING_BOARD;
		protected System.Web.UI.WebControls.DropDownList cmbDIVING_BOARD;
		protected System.Web.UI.WebControls.Label capSLIDE;
		protected System.Web.UI.WebControls.DropDownList cmbSLIDE;
		protected System.Web.UI.WebControls.Label capDESC_FARMING_BUSINESS_COND;
		protected System.Web.UI.WebControls.TextBox txtDESC_FARMING_BUSINESS_COND;
		protected System.Web.UI.WebControls.Label lblDESC_FARMING_BUSINESS_COND;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_FARMING_BUSINESS_COND;
		protected System.Web.UI.WebControls.Label lblPROPERTY_ON_MORE_THAN;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPROPERTY_ON_MORE_THAN_DESC;
		protected System.Web.UI.WebControls.Label lblDWELLING_MOBILE_HOME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDWELLING_MOBILE_HOME_DESC;
		protected System.Web.UI.WebControls.Label lblPROPERTY_USED_WHOLE_PART;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPROPERTY_USED_WHOLE_PART_DESC;		
		//protected System.Web.UI.WebControls.Label lblPROVIDE_HOME_DAY_CARE;
		protected System.Web.UI.WebControls.Label capANY_PRIOR_LOSSES;
		protected System.Web.UI.WebControls.DropDownList cmbANY_PRIOR_LOSSES;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_PRIOR_LOSSES;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_PRIOR_LOSSES_DESC;
		protected System.Web.UI.WebControls.Label capANY_PRIOR_LOSSES_DESC;
		protected System.Web.UI.WebControls.TextBox txtANY_PRIOR_LOSSES_DESC;
		protected System.Web.UI.WebControls.Label lblPRIOR_LOSSES;

		protected System.Web.UI.WebControls.Label capBOAT_WITH_HOMEOWNER;
		protected System.Web.UI.WebControls.DropDownList cmbBOAT_WITH_HOMEOWNER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBOAT_WITH_HOMEOWNER;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBOAT_WITH_HOMEOWNER;		
		//Added by Charles on 9-Dec-09 for Itrack 6489
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOTHER_DESCRIPTION; 
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOTHER_DESCRIPTION_VALUE; //Added till here
		//Added for Itrack Issue 6640 on 11 Dec 09
		protected System.Web.UI.WebControls.Label capNON_WEATHER_CLAIMS;
		protected System.Web.UI.WebControls.TextBox txtNON_WEATHER_CLAIMS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNON_WEATHER_CLAIMS;
		protected System.Web.UI.WebControls.Label capWEATHER_CLAIMS;
		protected System.Web.UI.WebControls.TextBox txtWEATHER_CLAIMS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvWEATHER_CLAIMS;
		protected System.Web.UI.WebControls.RegularExpressionValidator revNON_WEATHER_CLAIMS;
		protected System.Web.UI.WebControls.RegularExpressionValidator revWEATHER_CLAIMS;
		
		//Defining the business layer class object
		Cms.BusinessLayer.BlApplication.ClsHomeGeneralInformation  objGeneralInformation ;
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
			rfvMULTI_POLICY_DISC_APPLIED.ErrorMessage		=   ClsMessages.GetMessage(base.ScreenId,"129");
			csvREMARKS.ErrorMessage							=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("440");  
			revDESC_RENTERS.ValidationExpression			=   aRegExpInteger;
			revDESC_RENTERS.ErrorMessage					=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");
			revLAST_INSPECTED_DATE.ValidationExpression		=   aRegExpDate;
			regDESC_INSU_TRANSFERED_AGENCY.ValidationExpression =	aRegExpDate;
			revLAST_INSPECTED_DATE.ErrorMessage				=   Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"22");
			csvLAST_INSPECTED_DATE.ErrorMessage				=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("486");
			revDESC_RESIDENCE_EMPLOYEE.ValidationExpression	=   aRegExpInteger;
			revDESC_RESIDENCE_EMPLOYEE.ErrorMessage			=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
			rfvDESC_RESIDENCE_EMPLOYEE.ErrorMessage			=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("660");
				
			rfvBREED.ErrorMessage							=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("653");
			rfvDESC_ANY_HEATING_SOURCE.ErrorMessage			=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("666");
			rfvDESC_CONVICTION_DEGREE_IN_PAST.ErrorMessage  =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("658");
			rfvDESC_COV_DECLINED_CANCELED.ErrorMessage      =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("657");
			rfvDESC_DWELLING_OWNED_BY_OTHER.ErrorMessage    =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("649");
			rfvDESC_INSU_TRANSFERED_AGENCY.ErrorMessage     =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("655");
			rfvDESC_LEAD_PAINT_HAZARD.ErrorMessage			=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("659");
			rfvDESC_OTHER_INSURANCE.ErrorMessage			=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("662");
			rfvDESC_OTHER_RESIDENCE.ErrorMessage			=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("661");
			rfvDESC_OWNER.ErrorMessage						=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("656");
			rfvDESC_PROPERTY.ErrorMessage					=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("651");
			rfvDESC_RENOVATION.ErrorMessage					=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("663");
			rfvDESC_RENTED_IN_PART.ErrorMessage				=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("648");
			rfvDESC_STAIRWAYS.ErrorMessage					=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("652");
			rfvDESC_TRAMPOLINE.ErrorMessage					=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("664");
			rfvDESC_VACENT_OCCUPY.ErrorMessage				=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("647");
			rfvOTHER_DESCRIPTION.ErrorMessage				=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("654");
			rfvDESC_RENTERS.ErrorMessage					=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("665");
			rfvIS_VACENT_OCCUPY.ErrorMessage				=   ClsMessages.GetMessage(base.ScreenId,"129");
			rfvIS_RENTED_IN_PART.ErrorMessage				=	ClsMessages.GetMessage(base.ScreenId,"129");
			rfvIS_DWELLING_OWNED_BY_OTHER.ErrorMessage		=	ClsMessages.GetMessage(base.ScreenId,"129");
			rfvANY_FARMING_BUSINESS_COND.ErrorMessage		=	ClsMessages.GetMessage(base.ScreenId,"129");
			rfvIS_PROP_NEXT_COMMERICAL.ErrorMessage			=	ClsMessages.GetMessage(base.ScreenId,"129");
			rfvARE_STAIRWAYS_PRESENT.ErrorMessage			=	ClsMessages.GetMessage(base.ScreenId,"129");
			rfvANIMALS_EXO_PETS_HISTORY.ErrorMessage		=	ClsMessages.GetMessage(base.ScreenId,"129");
			rfvNO_OF_PETS.ErrorMessage						=	ClsMessages.GetMessage(base.ScreenId,"129");
			rfvIS_SWIMPOLL_HOTTUB.ErrorMessage				=	ClsMessages.GetMessage(base.ScreenId,"129");
			rfvIS_OWNERS_DWELLING_CHANGED.ErrorMessage		=	ClsMessages.GetMessage(base.ScreenId,"129");
			rfvANY_COV_DECLINED_CANCELED.ErrorMessage		=	ClsMessages.GetMessage(base.ScreenId,"129");
			rfvCONVICTION_DEGREE_IN_PAST.ErrorMessage		=	ClsMessages.GetMessage(base.ScreenId,"129");
			rfvLEAD_PAINT_HAZARD.ErrorMessage				=	ClsMessages.GetMessage(base.ScreenId,"129");
			rfvANY_RESIDENCE_EMPLOYEE.ErrorMessage			=	ClsMessages.GetMessage(base.ScreenId,"129");
			rfvANY_OTHER_RESI_OWNED.ErrorMessage			=	ClsMessages.GetMessage(base.ScreenId,"129");
			rfvANY_OTH_INSU_COMP.ErrorMessage				=	ClsMessages.GetMessage(base.ScreenId,"129");
			rfvANY_RENOVATION.ErrorMessage					=	ClsMessages.GetMessage(base.ScreenId,"129");
			rfvTRAMPOLINE.ErrorMessage						=	ClsMessages.GetMessage(base.ScreenId,"129");
			rfvRENTERS.ErrorMessage							=	ClsMessages.GetMessage(base.ScreenId,"129");
			rfvANY_HEATING_SOURCE.ErrorMessage				=	ClsMessages.GetMessage(base.ScreenId,"129");
			rfvBUILD_UNDER_CON_GEN_CONT.ErrorMessage		=	ClsMessages.GetMessage(base.ScreenId,"129");
			rfvNON_SMOKER_CREDIT.ErrorMessage				=	ClsMessages.GetMessage(base.ScreenId,"129");
			rfvSWIMMING_POOL.ErrorMessage					=	ClsMessages.GetMessage(base.ScreenId,"129");
			rfvVALUED_CUSTOMER_DISCOUNT_OVERRIDE.ErrorMessage		= ClsMessages.GetMessage(base.ScreenId,"129");
			rfvVALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC.ErrorMessage	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("815");
		
			rfvPremises.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("667");
			//rfvOf_Acres.ErrorMessage= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("668");
			rfvOf_Acres.ErrorMessage= ClsMessages.GetMessage(base.ScreenId,"4");
//			rfvOf_Acres_P.ErrorMessage = ClsMessages.GetMessage(base.ScreenId,"1");
			rfvNo_Horses.ErrorMessage =ClsMessages.GetMessage(base.ScreenId,"2");
			rfvDESC_Location.ErrorMessage =ClsMessages.GetMessage(base.ScreenId,"3");
			rfvIsAny_Horse.ErrorMessage =ClsMessages.GetMessage(base.ScreenId,"129");
			revOf_Acres.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
//			revOf_Acres_P.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
			revNo_Horses.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");

//			revOf_Acres_P.ValidationExpression =aRegExpInteger;
			revOf_Acres.ValidationExpression =aRegExpInteger;
			revNo_Horses.ValidationExpression =aRegExpInteger;			
			revLocation.ValidationExpression=aRegExpInteger ;
			rfvLocation.ErrorMessage=ClsMessages.GetMessage(base.ScreenId,"5");
			revLocation.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
			revYEARS_INSU.ValidationExpression      =     aRegExpInteger; 
			revYEARS_INSU_WOL.ValidationExpression  =     aRegExpInteger;
			csvYEARS_INSU_WOL.ErrorMessage          =     Cms.CmsWeb.ClsMessages.FetchGeneralMessage("616");
			revYEARS_INSU.ErrorMessage              =     Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163"); 
			revYEARS_INSU_WOL.ErrorMessage          =     Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");

			rfvDESC_IS_SWIMPOLL_HOTTUB.ErrorMessage			= ClsMessages.GetMessage(base.ScreenId,"6");
			rfvDESC_MULTI_POLICY_DISC_APPLIED.ErrorMessage	= ClsMessages.GetMessage(base.ScreenId,"7");
			rfvDESC_BUILD_UNDER_CON_GEN_CONT.ErrorMessage	= ClsMessages.GetMessage(base.ScreenId,"8");
			this.csvDESC_Location.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("739");	
			rfvDESC_FARMING_BUSINESS_COND.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("799");	
			rfvPROPERTY_USED_WHOLE_PART_DESC.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("799");
			rfvDWELLING_MOBILE_HOME_DESC.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("649");
			rfvANY_PRIOR_LOSSES.ErrorMessage					=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("843");
			rfvANY_PRIOR_LOSSES_DESC.ErrorMessage				=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("844");
			rfvBOAT_WITH_HOMEOWNER.ErrorMessage					= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1003");
			//Added for Itrack Issue 6640 on 11 Dec 09
			rfvNON_WEATHER_CLAIMS.ErrorMessage		=		 Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1060");
			rfvWEATHER_CLAIMS.ErrorMessage		=		 Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1059");
			revNON_WEATHER_CLAIMS.ValidationExpression      =     aRegExpInteger;
			revWEATHER_CLAIMS.ValidationExpression			=     aRegExpInteger;
			revNON_WEATHER_CLAIMS.ErrorMessage              =     Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
			revWEATHER_CLAIMS.ErrorMessage              =     Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
		
		}
		#endregion
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			rowPremises.Attributes.Add("style","display:none");
			rowHorse.Attributes.Add("style","display:none");
			rowhorsetext.Attributes.Add("style","display:none");			
			#region Setting screen id
			if(Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"] != "")
			{
				strCalledFrom = Request.QueryString["CalledFrom"];
				//Added these because parmaters passed to this page were showin '?'
				//Due to time contraints this was the only solutuion insteadf of correcting the querystring
				strCalledFrom = strCalledFrom.Replace("?",""); 
				strCalledFrom = strCalledFrom.Replace("&","");
				hidCalledFrom.Value =strCalledFrom.ToUpper();
			}
			switch(strCalledFrom)
			{
				case "Home":
				case "HOME":
					//Added ScreenID for the Home Qs
					//base.ScreenId="60";
					base.ScreenId="240";					
					isForming.Visible =true;
					//trIsAny_Horse.Visible=true;//Added by Charles on 9-Dec-09 for Itrack 6489
					trPROPERTY_ON_MORE_THAN.Visible = false;
					trDWELLING_MOBILE_HOME.Visible  = false;
					trPROPERTY_USED_WHOLE_PART.Visible  = false;				
					break;
				case "Rental":
				case "RENTAL":
					base.ScreenId="260";
					isForming.Visible =false;
					//trIsAny_Horse.Visible=false;//Added by Charles on 9-Dec-09 for Itrack 6489
					trPROPERTY_ON_MORE_THAN.Visible = true;
					trDWELLING_MOBILE_HOME.Visible  = true;
					trPROPERTY_USED_WHOLE_PART.Visible  = true;
					trBOAT_WITH_HOMEOWNER.Visible = false;
					break;			
				default:
					//base.ScreenId="60";
					base.ScreenId="240";
					trPROPERTY_ON_MORE_THAN.Visible = false;
					trDWELLING_MOBILE_HOME.Visible = false;
					trPROPERTY_USED_WHOLE_PART.Visible = false;				
					break;
			}
			#endregion
			btnReset.Attributes.Add("onclick","javascript:return ResetForm1();");
			hlkLAST_INSPECTED_DATE.Attributes.Add("OnClick","fPopCalendar(document.POL_HOME_OWNER_GEN_INFO.txtLAST_INSPECTED_DATE,document.POL_HOME_OWNER_GEN_INFO.txtLAST_INSPECTED_DATE)");
			hlkDESC_INSU_TRANSFERED_AGENCY.Attributes.Add("OnClick","fPopCalendar(document.POL_HOME_OWNER_GEN_INFO.txtDESC_INSU_TRANSFERED_AGENCY,document.POL_HOME_OWNER_GEN_INFO.txtDESC_INSU_TRANSFERED_AGENCY)");

			lblMessage.Visible = false;
			trError.Visible = false;
			SetErrorMessages();
			//START:*********** getting session values (neccessary to do processing of the page)********
			hidCUSTOMER_ID.Value		=	GetCustomerID();
			hidPOL_ID.Value				=	GetPolicyID();
			hidPOL_VERSION_ID.Value		=	GetPolicyVersionID();

			//ClsHomeGeneralInformation objHome = new ClsHomeGeneralInformation();
			//string str = objHome.GetXml(Convert.ToInt32(hidCUSTOMER_ID.Value),Convert.ToInt32(hidPOL_ID.Value),Convert.ToInt32(hidPOL_VERSION_ID.Value));
			//END:************* getting session values (neccessary to do processing of the page)********

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass		=	CmsButtonType.Write ;
			btnReset.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.Homeowners.PolicyHomeownerGeneralInfo" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				if(strCalledFrom.ToUpper() == "HOME")//Added by Charles on 9-Dec-09 for Itrack 6489
				{
					btnSave.Attributes.Add("OnClick","return ValOTHER_DESCRIPTION();");
				}//Added till here
				else //Done by Sibin for Itrack Issue 6640 on 11 Dec 09
					btnSave.Attributes.Add("onClick","return Page_ClientValidate();");
				GetStateID();	
				if((hidCUSTOMER_ID.Value.Trim() == "")||(hidPOL_ID.Value.Trim() == "")||(hidPOL_VERSION_ID.Value.Trim() == ""))
				{
					trBody.Attributes.Add("style","display:none");
					lblError.Text = ClsMessages.GetMessage(base.ScreenId,"118");
					trError.Visible = true;
				}
				else
				{
					cltClientTop.CustomerID	= int.Parse(hidCUSTOMER_ID.Value);
					cltClientTop.PolicyID = int.Parse(hidPOL_ID.Value);
					cltClientTop.PolicyVersionID  = int.Parse(hidPOL_VERSION_ID.Value);
					cltClientTop.ShowHeaderBand = "Policy";
					cltClientTop.Visible	= true;
				}
				PopulateComboBox();
				GetOldDataXML();
				SetCaptions();
				PopulateControls();
				int i;
				for(i=0;i<=10;i++)
				{
					cmbNO_OF_PETS.Items.Insert(i,i.ToString());    
				}
				#region set Workflow cntrol
				SetWorkFlow();
				#endregion
			}
		}//end pageload
		#endregion
		#region Validation
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
		#endregion

		private void GetStateID()
		{
			hidStateID.Value=Cms.BusinessLayer.BlApplication.ClsVehicleInformation.GetStateIdForpolicy(Convert.ToInt32(hidCUSTOMER_ID.Value),hidPOL_ID.Value,hidPOL_VERSION_ID.Value).ToString();
		}

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsPolicyHomeownerGeneralInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsPolicyHomeownerGeneralInfo				objGeneralInfo;
			objGeneralInfo			=	new ClsPolicyHomeownerGeneralInfo();

			objGeneralInfo.CalledFrom=hidCalledFrom.Value;

			objGeneralInfo.ANY_FARMING_BUSINESS_COND =	cmbANY_FARMING_BUSINESS_COND.SelectedValue;;

			if(cmbANY_FARMING_BUSINESS_COND.SelectedValue != "1")
			{
				txtDESC_FARMING_BUSINESS_COND.Text = "";
				cmbPROVIDE_HOME_DAY_CARE.SelectedIndex = 0;	
			}
			
			objGeneralInfo.DESC_FARMING_BUSINESS_COND 	=	txtDESC_FARMING_BUSINESS_COND.Text;
			objGeneralInfo.PROVIDE_HOME_DAY_CARE		=	cmbPROVIDE_HOME_DAY_CARE.SelectedValue;
			
			objGeneralInfo.DESC_FARMING_BUSINESS_COND 	=	txtDESC_FARMING_BUSINESS_COND.Text;
			objGeneralInfo.PROVIDE_HOME_DAY_CARE		=	cmbPROVIDE_HOME_DAY_CARE.SelectedValue;

			//RPSINGH
			
			objGeneralInfo.MODULAR_MANUFACTURED_HOME		=	cmbMODULAR_MANUFACTURED_HOME.SelectedValue;

			if (objGeneralInfo.MODULAR_MANUFACTURED_HOME == "1")
				objGeneralInfo.BUILT_ON_CONTINUOUS_FOUNDATION	=	cmbBUILT_ON_CONTINUOUS_FOUNDATION.SelectedValue;
			else
				objGeneralInfo.BUILT_ON_CONTINUOUS_FOUNDATION	=	"0";
			//end
			
			objGeneralInfo.ANY_RESIDENCE_EMPLOYEE		=	cmbANY_RESIDENCE_EMPLOYEE.SelectedValue;

			if(cmbVALUED_CUSTOMER_DISCOUNT_OVERRIDE.SelectedValue!=null && cmbVALUED_CUSTOMER_DISCOUNT_OVERRIDE.SelectedValue!="")
			{
				objGeneralInfo.VALUED_CUSTOMER_DISCOUNT_OVERRIDE = cmbVALUED_CUSTOMER_DISCOUNT_OVERRIDE.SelectedValue;
			}

			if(objGeneralInfo.VALUED_CUSTOMER_DISCOUNT_OVERRIDE != "1")
				txtVALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC.Text = "";
	
			objGeneralInfo.VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC = txtVALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC.Text.Trim();

			if(txtYEARS_INSU.Text.Trim()!="")
				objGeneralInfo.YEARS_INSU              = int.Parse(txtYEARS_INSU.Text.ToString().Trim());
			else
				objGeneralInfo.YEARS_INSU               = 0;
			if(txtYEARS_INSU_WOL.Text.Trim()!="")
				objGeneralInfo.YEARS_INSU_WOL            = int.Parse(txtYEARS_INSU_WOL.Text.ToString().Trim());
			else
				objGeneralInfo.YEARS_INSU_WOL          = 0;

			if (txtLAST_INSPECTED_DATE.Text.Trim() != "")
				objGeneralInfo.LAST_INSPECTED_DATE = ConvertToDate(txtLAST_INSPECTED_DATE.Text);

			if(cmbANY_RESIDENCE_EMPLOYEE.SelectedValue== "1")
				objGeneralInfo.DESC_RESIDENCE_EMPLOYEE		=	txtDESC_RESIDENCE_EMPLOYEE.Text;
			else
				objGeneralInfo.DESC_RESIDENCE_EMPLOYEE		=	"";

			objGeneralInfo.ANY_OTHER_RESI_OWNED			=	cmbANY_OTHER_RESI_OWNED.SelectedValue;

			if(cmbANY_OTHER_RESI_OWNED.SelectedValue== "1")
				objGeneralInfo.DESC_OTHER_RESIDENCE			=	txtDESC_OTHER_RESIDENCE.Text;
			else
				objGeneralInfo.DESC_OTHER_RESIDENCE			=	"";

			objGeneralInfo.ANY_OTH_INSU_COMP			=	cmbANY_OTH_INSU_COMP.SelectedValue;

			if(cmbANY_OTH_INSU_COMP.SelectedValue== "1")
				objGeneralInfo.DESC_OTHER_INSURANCE			=	txtDESC_OTHER_INSURANCE.Text;
			else
				objGeneralInfo.DESC_OTHER_INSURANCE			=	"";

			objGeneralInfo.HAS_INSU_TRANSFERED_AGENCY	=	cmbHAS_INSU_TRANSFERED_AGENCY.SelectedValue;

			if(cmbHAS_INSU_TRANSFERED_AGENCY.SelectedValue== "1")
				objGeneralInfo.DESC_INSU_TRANSFERED_AGENCY	=	txtDESC_INSU_TRANSFERED_AGENCY.Text;
			else
				objGeneralInfo.DESC_INSU_TRANSFERED_AGENCY	=	"";

			objGeneralInfo.ANY_COV_DECLINED_CANCELED	=	cmbANY_COV_DECLINED_CANCELED.SelectedValue;

			if(cmbANY_COV_DECLINED_CANCELED.SelectedValue== "1")
				objGeneralInfo.DESC_COV_DECLINED_CANCELED	=	txtDESC_COV_DECLINED_CANCELED.Text;
			else
				objGeneralInfo.DESC_COV_DECLINED_CANCELED	=	"";

			if(cmbANIMALS_EXO_PETS_HISTORY.SelectedItem!=null)
				objGeneralInfo.ANIMALS_EXO_PETS_HISTORY		=	cmbANIMALS_EXO_PETS_HISTORY.SelectedItem.Value==""?0:int.Parse(cmbANIMALS_EXO_PETS_HISTORY.SelectedItem.Value);			

			if(strCalledFrom.ToUpper() == "RENTAL") //If check added by Charles on 9-Dec-09 for Itrack 6489
			{
				if(cmbANIMALS_EXO_PETS_HISTORY.SelectedItem!=null)
					if(cmbANIMALS_EXO_PETS_HISTORY.SelectedValue== "1")
						objGeneralInfo.BREED 			=	txtBREED.Text;
					else
						objGeneralInfo.BREED			=	"";
			}
			else //Added by Charles on 9-Dec-09 for Itrack 6489
				objGeneralInfo.BREED			=	""; 

			if(cmbIS_SWIMPOLL_HOTTUB.SelectedValue!=null && cmbIS_SWIMPOLL_HOTTUB.SelectedValue!="")
				objGeneralInfo.IS_SWIMPOLL_HOTTUB = cmbIS_SWIMPOLL_HOTTUB.SelectedValue;
			if(objGeneralInfo.IS_SWIMPOLL_HOTTUB=="1")
				objGeneralInfo.DESC_IS_SWIMPOLL_HOTTUB = txtDESC_IS_SWIMPOLL_HOTTUB.Text.Trim();
			objGeneralInfo.CONVICTION_DEGREE_IN_PAST	=	cmbCONVICTION_DEGREE_IN_PAST.SelectedValue;

			if(cmbCONVICTION_DEGREE_IN_PAST.SelectedValue== "1")
				objGeneralInfo.DESC_CONVICTION_DEGREE_IN_PAST=	txtDESC_CONVICTION_DEGREE_IN_PAST.Text;
			else
				objGeneralInfo.DESC_CONVICTION_DEGREE_IN_PAST=	"";

            objGeneralInfo.ANY_RENOVATION				=	cmbANY_RENOVATION.SelectedValue;

			if(cmbANY_RENOVATION.SelectedValue== "1")
				objGeneralInfo.DESC_RENOVATION				=	txtDESC_RENOVATION.Text;
			else
				objGeneralInfo.DESC_RENOVATION				=	"";

			objGeneralInfo.TRAMPOLINE					=	cmbTRAMPOLINE.SelectedValue;

			if(cmbTRAMPOLINE.SelectedValue== "1")
				objGeneralInfo.DESC_TRAMPOLINE				=	txtDESC_TRAMPOLINE.Text;
			else
				objGeneralInfo.DESC_TRAMPOLINE				=	"";

			objGeneralInfo.LEAD_PAINT_HAZARD			=	cmbLEAD_PAINT_HAZARD.SelectedValue;

			if(cmbLEAD_PAINT_HAZARD.SelectedValue== "1")
				objGeneralInfo.DESC_LEAD_PAINT_HAZARD		=	txtDESC_LEAD_PAINT_HAZARD.Text;
			else
				objGeneralInfo.DESC_LEAD_PAINT_HAZARD		=	"";

			objGeneralInfo.RENTERS						=	cmbRENTERS.SelectedValue;

			if(cmbRENTERS.SelectedValue== "1")
				objGeneralInfo.DESC_RENTERS					=	txtDESC_RENTERS.Text;
			else
				objGeneralInfo.DESC_RENTERS					=	"";

			objGeneralInfo.BUILD_UNDER_CON_GEN_CONT		=	cmbBUILD_UNDER_CON_GEN_CONT.SelectedValue;            
			if(objGeneralInfo.BUILD_UNDER_CON_GEN_CONT=="1")
				objGeneralInfo.DESC_BUILD_UNDER_CON_GEN_CONT = txtDESC_BUILD_UNDER_CON_GEN_CONT.Text.Trim();
			objGeneralInfo.MULTI_POLICY_DISC_APPLIED 	=	cmbMULTI_POLICY_DISC_APPLIED.SelectedValue;
			if(objGeneralInfo.MULTI_POLICY_DISC_APPLIED=="1")
				objGeneralInfo.DESC_MULTI_POLICY_DISC_APPLIED = txtDESC_MULTI_POLICY_DISC_APPLIED.Text.Trim();
			objGeneralInfo.REMARKS						=	txtREMARKS.Text;
			
			objGeneralInfo.NO_OF_PETS				=	cmbNO_OF_PETS.SelectedItem.Value==""?0:int.Parse(cmbNO_OF_PETS.SelectedItem.Value);  

			if(objGeneralInfo.NO_OF_PETS!=0)
			{
				if(strCalledFrom.ToUpper()=="RENTAL")//Added by Charles on 9-Dec-09 for Itrack 6489	
				{
					objGeneralInfo.OTHER_DESCRIPTION=cmbOTHER_DESCRIPTION.Items[cmbOTHER_DESCRIPTION.SelectedIndex].Value; //Changed by Charles on 9-Dec-09 for Itrack 6489	
				}
				else//Added by Charles on 9-Dec-09 for Itrack 6489	
				{
					if(hidOTHER_DESCRIPTION_VALUE.Value !="")
						objGeneralInfo.OTHER_DESCRIPTION = hidOTHER_DESCRIPTION_VALUE.Value;
					else
						objGeneralInfo.OTHER_DESCRIPTION="";
				}//Added till here
			}
			else
				objGeneralInfo.OTHER_DESCRIPTION="";

			objGeneralInfo.IS_RENTED_IN_PART =	cmbIS_RENTED_IN_PART.SelectedValue;

			if(cmbIS_RENTED_IN_PART.SelectedValue== "1")
				objGeneralInfo.DESC_RENTED_IN_PART=txtDESC_RENTED_IN_PART.Text;
			else
				objGeneralInfo.DESC_RENTED_IN_PART="";
			
			objGeneralInfo.IS_VACENT_OCCUPY  =	cmbIS_VACENT_OCCUPY.SelectedValue;
			
			if(cmbIS_VACENT_OCCUPY.SelectedValue== "1")
				objGeneralInfo.DESC_VACENT_OCCUPY=txtDESC_VACENT_OCCUPY.Text;
			else
				objGeneralInfo.DESC_VACENT_OCCUPY="";
			
			objGeneralInfo.IS_DWELLING_OWNED_BY_OTHER =	cmbIS_DWELLING_OWNED_BY_OTHER.SelectedValue;

			if(cmbIS_DWELLING_OWNED_BY_OTHER.SelectedValue== "1")
				objGeneralInfo.DESC_DWELLING_OWNED_BY_OTHER=txtDESC_DWELLING_OWNED_BY_OTHER.Text;
			else
				objGeneralInfo.DESC_DWELLING_OWNED_BY_OTHER="";
			
			objGeneralInfo.IS_PROP_NEXT_COMMERICAL =	cmbIS_PROP_NEXT_COMMERICAL.SelectedValue;

			if(cmbIS_PROP_NEXT_COMMERICAL.SelectedValue== "1")
				objGeneralInfo.DESC_PROPERTY=txtDESC_PROPERTY.Text;
			else
				objGeneralInfo.DESC_PROPERTY="";

			objGeneralInfo.ARE_STAIRWAYS_PRESENT =	cmbARE_STAIRWAYS_PRESENT.SelectedValue;

			if(cmbARE_STAIRWAYS_PRESENT.SelectedValue== "1")
				objGeneralInfo.DESC_STAIRWAYS=txtDESC_STAIRWAYS.Text;
			else
				objGeneralInfo.DESC_STAIRWAYS="";

			objGeneralInfo.IS_OWNERS_DWELLING_CHANGED =	cmbIS_OWNERS_DWELLING_CHANGED.SelectedValue;

			if(cmbIS_OWNERS_DWELLING_CHANGED.SelectedValue== "1")
				objGeneralInfo.DESC_OWNER=txtDESC_OWNER.Text;
			else
				objGeneralInfo.DESC_OWNER="";

			objGeneralInfo.ANY_HEATING_SOURCE=cmbANY_HEATING_SOURCE.SelectedValue;
			
			if (cmbANY_HEATING_SOURCE.SelectedValue == "1")
				objGeneralInfo.DESC_ANY_HEATING_SOURCE=txtDESC_ANY_HEATING_SOURCE.Text;
			else
				objGeneralInfo.DESC_ANY_HEATING_SOURCE="";
			//if(strCalledFrom.ToUpper()=="REN" || strCalledFrom.ToUpper()=="RENTAL")
			if((hidStateID.Value=="22" && strCalledFrom.ToUpper()=="HOME") || strCalledFrom.ToUpper()=="REN" || strCalledFrom.ToUpper()=="RENTAL")
				objGeneralInfo.NON_SMOKER_CREDIT="";
			else
				objGeneralInfo.NON_SMOKER_CREDIT=cmbNON_SMOKER_CREDIT.SelectedValue;
            
			objGeneralInfo.SWIMMING_POOL=cmbSWIMMING_POOL.SelectedValue;

			if(cmbSWIMMING_POOL.SelectedValue == "0" || cmbSWIMMING_POOL.SelectedValue == "2")
			{
				objGeneralInfo.SWIMMING_POOL_TYPE    =	System.DBNull.Value.ToString();
			}
			else
			{
				if ( cmbSWIMMING_POOL_TYPE.SelectedValue.Trim() !="")
				{
					objGeneralInfo.SWIMMING_POOL_TYPE    =	cmbSWIMMING_POOL_TYPE.SelectedValue;
				}
				if ( cmbDIVING_BOARD.SelectedValue.Trim() !="")
				{
					objGeneralInfo.DIVING_BOARD   =	Convert.ToInt32(cmbDIVING_BOARD.SelectedValue);
				}
				if ( cmbAPPROVED_FENCE.SelectedValue.Trim() !="")
				{
					objGeneralInfo.APPROVED_FENCE =	Convert.ToInt32(cmbAPPROVED_FENCE.SelectedValue);
				}
				if ( cmbSLIDE.SelectedValue.Trim() !="")
				{
					objGeneralInfo.SLIDE =	Convert.ToInt32(cmbSLIDE.SelectedValue);
				}
			}

			//RP - 5 july 2006
			if (cmbPROPERTY_ON_MORE_THAN.SelectedValue.Trim() != "")
			{
				objGeneralInfo.PROPERTY_ON_MORE_THAN    =	cmbPROPERTY_ON_MORE_THAN.SelectedValue;
				if (cmbPROPERTY_ON_MORE_THAN.SelectedValue != "1")//Incase of NO blank the text box
				{
					txtPROPERTY_ON_MORE_THAN_DESC.Text = "";
				}

				objGeneralInfo.PROPERTY_ON_MORE_THAN_DESC = txtPROPERTY_ON_MORE_THAN_DESC.Text;
			}

			if (cmbDWELLING_MOBILE_HOME.SelectedValue.Trim() != "")
			{
				objGeneralInfo.DWELLING_MOBILE_HOME    =	cmbDWELLING_MOBILE_HOME.SelectedValue;
				if (cmbDWELLING_MOBILE_HOME.SelectedValue != "1")//Incase of NO blank the text box
				{
					txtDWELLING_MOBILE_HOME_DESC.Text = "";
				}

				objGeneralInfo.DWELLING_MOBILE_HOME_DESC = txtDWELLING_MOBILE_HOME_DESC.Text;
			}

			if (cmbPROPERTY_USED_WHOLE_PART.SelectedValue.Trim() != "")
			{
				objGeneralInfo.PROPERTY_USED_WHOLE_PART    =	cmbPROPERTY_USED_WHOLE_PART.SelectedValue;
				if (cmbPROPERTY_USED_WHOLE_PART.SelectedValue != "1")//Incase of NO blank the text box
				{
					txtPROPERTY_USED_WHOLE_PART_DESC.Text = "";
				}

				objGeneralInfo.PROPERTY_USED_WHOLE_PART_DESC = txtPROPERTY_USED_WHOLE_PART_DESC.Text;
			}

			objGeneralInfo.ANY_PRIOR_LOSSES				=	cmbANY_PRIOR_LOSSES.SelectedValue;

			if(cmbANY_PRIOR_LOSSES.SelectedValue == "0")
			{
				txtANY_PRIOR_LOSSES_DESC.Text ="";
			}
			objGeneralInfo.ANY_PRIOR_LOSSES_DESC			=	txtANY_PRIOR_LOSSES_DESC.Text;
			
			objGeneralInfo.BOAT_WITH_HOMEOWNER				=	cmbBOAT_WITH_HOMEOWNER.SelectedValue;//Added on 26 sep 2007

			//Added by Sumit			
			objGeneralInfo.Location="";
			//objGeneralInfo.IsAny_Horse="";
			objGeneralInfo.DESC_Location="";
			objGeneralInfo.Any_Forming=cmbAny_Forming.SelectedItem.Value ;
			if(cmbAny_Forming.SelectedIndex ==1)
			{
				objGeneralInfo.Premises=int.Parse(cmbPremises.SelectedItem.Value);
				objGeneralInfo.Of_Acres=double.Parse(txtOf_Acres.Text.ToString());
//				if(cmbPremises.SelectedIndex  ==3)
//				{
////					objGeneralInfo.Of_Acres_P=double.Parse(txtOf_Acres_P.Text);
//					
//				}
				objGeneralInfo.DESC_Location=txtDESC_Location.Text.ToString().Trim();
				if(cmbPremises.SelectedIndex==1 || cmbPremises.SelectedIndex == 2)
				{
					objGeneralInfo.Location=txtLocation.Text.ToString().Trim();
					
				}
			}		
			objGeneralInfo.IsAny_Horse =cmbIsAny_Horse.SelectedItem.Value ;
			if(cmbIsAny_Horse.SelectedIndex ==1)
			{
				objGeneralInfo.No_Horses=int.Parse(txtNo_Horses.Text.ToString());
			}

			//Added for Itrack Issue 6640 on 11 Dec 09
			if(hidCalledFrom.Value == "HOME")
			{
				if(txtNON_WEATHER_CLAIMS.Text!= "")
					objGeneralInfo.NON_WEATHER_CLAIMS		=	int.Parse(txtNON_WEATHER_CLAIMS.Text);
				else
					objGeneralInfo.NON_WEATHER_CLAIMS		=	-1;
				
				if(txtWEATHER_CLAIMS.Text!= "")
					objGeneralInfo.WEATHER_CLAIMS			=	int.Parse(txtWEATHER_CLAIMS.Text);
				else
					objGeneralInfo.WEATHER_CLAIMS			=	-1;
			}
			else
			{
				objGeneralInfo.NON_WEATHER_CLAIMS		=	-1;
				objGeneralInfo.WEATHER_CLAIMS			=	-1;
			}
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidPOL_VERSION_ID.Value;
			oldXML			=	hidOldData.Value;
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
				ClsHomeGeneralInformation objGeneralInformation = new  ClsHomeGeneralInformation();
				
				//Retreiving the form values into model class object
				ClsPolicyHomeownerGeneralInfo  objGeneralInfo = GetFormValue();
				
				objGeneralInfo.CUSTOMER_ID		=	int.Parse(hidCUSTOMER_ID.Value);
				objGeneralInfo.POLICY_ID 		=	int.Parse(hidPOL_ID.Value);
				objGeneralInfo.POLICY_VERSION_ID=	int.Parse(hidPOL_VERSION_ID.Value);

				if(oldXML == "") //save case
				{
					objGeneralInfo.CREATED_BY		=	int.Parse(GetUserId());
					objGeneralInfo.CREATED_DATETIME =	DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objGeneralInformation.Add(objGeneralInfo);
					if(intRetVal>0)
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value		=	"0";
						hidIS_ACTIVE.Value		=	"Y";
						SetWorkFlow();

						//Showing the endorsement popup window
						base.OpenEndorsementDetails();
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value		=	"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value		=	"2";
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsPolicyHomeownerGeneralInfo objOldGeneralInfo;
					objOldGeneralInfo			=	new ClsPolicyHomeownerGeneralInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldGeneralInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objGeneralInfo.MODIFIED_BY				=	int.Parse(GetUserId());
					objGeneralInfo.LAST_UPDATED_DATETIME	= DateTime.Now;
					
					//Updating the record using business layer class object
					intRetVal					=	objGeneralInformation.Update(objOldGeneralInfo,objGeneralInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"0";
						SetWorkFlow();

						//Showing the endorsement popup window
						base.OpenEndorsementDetails();
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value		=	"2";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"2";
					}
					lblMessage.Visible = true;
				}
				if(intRetVal > 0)
					GetOldDataXML();
			}
			catch(Exception ex)
			{
				lblMessage.Text					=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible				=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value				=	"2";
			}
			finally
			{
				if(objGeneralInformation!= null)
					objGeneralInformation.Dispose();
			}
		}
		#endregion

		private void PopulateControls()
		{
			DataTable dtControls;
			string strMultiPolicy="",strHeatingSource="";
			
			ClsHomeGeneralInformation objHomeGeneralInformation=new ClsHomeGeneralInformation();			
			if((hidOldData.Value=="") && ((strCalledFrom.ToUpper()=="HOME") || (strCalledFrom.ToUpper()=="RENTAL" && hidStateID.Value=="22")))
			{
				dtControls=objHomeGeneralInformation.GetValueForPageControlsPolicy(hidCUSTOMER_ID.Value,hidPOL_ID.Value,hidPOL_VERSION_ID.Value);
				if(dtControls!=null)
				{
					if(dtControls.Rows.Count > 0)
					{
						if(dtControls.Rows[0]["MULTI_POLICY_DISC_APPLIED"] != null)
							strMultiPolicy = dtControls.Rows[0]["MULTI_POLICY_DISC_APPLIED"].ToString();
						if(dtControls.Rows[0]["ANY_HEATING_SOURCE"] != null)
							strHeatingSource = dtControls.Rows[0]["ANY_HEATING_SOURCE"].ToString();						
					}
					if(strMultiPolicy!="")
						cmbMULTI_POLICY_DISC_APPLIED.SelectedValue=strMultiPolicy;
					if(strCalledFrom=="HOME")
					{
						if(strHeatingSource!="")
							cmbANY_HEATING_SOURCE.SelectedValue=strHeatingSource;
					}
				}
			}
		}

		#region set caption from resource file
		private void SetCaptions()
		{
			rngYEARS_INSU.ErrorMessage						=	 Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
			rngYEARS_INSU_WOL.ErrorMessage					=	 Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
			capANY_FARMING_BUSINESS_COND.Text			=		objResourceMgr.GetString("cmbANY_FARMING_BUSINESS_COND");
			capDESC_FARMING_BUSINESS_COND.Text			=		objResourceMgr.GetString("txtDESC_FARMING_BUSINESS_COND");
			capPROVIDE_HOME_DAY_CARE.Text				=		objResourceMgr.GetString("cmbPROVIDE_HOME_DAY_CARE");		
			//capDESC_BUSINESS.Text						=		objResourceMgr.GetString("txtDESC_BUSINESS");
			capANY_RESIDENCE_EMPLOYEE.Text				=		objResourceMgr.GetString("cmbANY_RESIDENCE_EMPLOYEE");
			capDESC_RESIDENCE_EMPLOYEE.Text				=		objResourceMgr.GetString("txtDESC_RESIDENCE_EMPLOYEE");
			capANY_OTHER_RESI_OWNED.Text				=		objResourceMgr.GetString("cmbANY_OTHER_RESI_OWNED");
			capDESC_OTHER_RESIDENCE.Text				=		objResourceMgr.GetString("txtDESC_OTHER_RESIDENCE");
			capANY_OTH_INSU_COMP.Text					=		objResourceMgr.GetString("cmbANY_OTH_INSU_COMP");
			capDESC_OTHER_INSURANCE.Text				=		objResourceMgr.GetString("txtDESC_OTHER_INSURANCE");
			capHAS_INSU_TRANSFERED_AGENCY.Text			=		objResourceMgr.GetString("cmbHAS_INSU_TRANSFERED_AGENCY");
			capDESC_INSU_TRANSFERED_AGENCY.Text			=		objResourceMgr.GetString("txtDESC_INSU_TRANSFERED_AGENCY");
			capANY_COV_DECLINED_CANCELED.Text			=		objResourceMgr.GetString("cmbANY_COV_DECLINED_CANCELED");
			capDESC_COV_DECLINED_CANCELED.Text			=		objResourceMgr.GetString("txtDESC_COV_DECLINED_CANCELED");
			capANIMALS_EXO_PETS_HISTORY.Text			=		objResourceMgr.GetString("cmbANIMALS_EXO_PETS_HISTORY");
			capBREED.Text								=		objResourceMgr.GetString("txtBREED");
			capOTHER_DESCRIPTION.Text					=		objResourceMgr.GetString("cmbOTHER_DESCRIPTION");
			//			capMORE_THEN_FIVE_ACRES.Text				=		objResourceMgr.GetString("cmbMORE_THEN_FIVE_ACRES");
			//			capDESC_MORE_THEN_FIVE_ACRES.Text			=		objResourceMgr.GetString("txtDESC_MORE_THEN_FIVE_ACRES");
			//			capRETROFITTED_FOR_EARTHQUAKE.Text			=		objResourceMgr.GetString("cmbRETROFITTED_FOR_EARTHQUAKE");
			//			capDESC_RETRO_FOR_EARTHQUAKE.Text			=		objResourceMgr.GetString("txtDESC_RETRO_FOR_EARTHQUAKE");
			capCONVICTION_DEGREE_IN_PAST.Text			=		objResourceMgr.GetString("cmbCONVICTION_DEGREE_IN_PAST");
			capDESC_CONVICTION_DEGREE_IN_PAST.Text		=		objResourceMgr.GetString("txtDESC_CONVICTION_DEGREE_IN_PAST");
			//			capMANAGER_ON_PERMISES.Text					=		objResourceMgr.GetString("cmbMANAGER_ON_PERMISES");
			//			capDESC_MANAGER_ON_PERMISES.Text			=		objResourceMgr.GetString("txtDESC_MANAGER_ON_PERMISES");
			//			capSECURITY_ATTENDENT.Text					=		objResourceMgr.GetString("cmbSECURITY_ATTENDENT");
			//			capDESC_SECURITY_ATTENDENT.Text				=		objResourceMgr.GetString("txtDESC_SECURITY_ATTENDENT");
			//			capBUILDING_ENT_LOCKED.Text					=		objResourceMgr.GetString("cmbBUILDING_ENT_LOCKED");
			//			capDESC_BUILDING_ENT_LOCKED.Text			=		objResourceMgr.GetString("txtDESC_BUILDING_ENT_LOCKED");
			//			capANY_UNCORRECT_FIRE_CODE_VIOL.Text		=		objResourceMgr.GetString("cmbANY_UNCORRECT_FIRE_CODE_VIOL");
			//			capDESC_UNCORRECT_FIRE_CODE_VIOL.Text		=		objResourceMgr.GetString("txtDESC_UNCORRECT_FIRE_CODE_VIOL");
			capANY_RENOVATION.Text						=		objResourceMgr.GetString("cmbANY_RENOVATION");
			capDESC_RENOVATION.Text						=		objResourceMgr.GetString("txtDESC_RENOVATION");
			//			capHOUSE_FOR_SALE.Text						=		objResourceMgr.GetString("cmbHOUSE_FOR_SALE");
			//			capDESC_HOUSE_FOR_SALE.Text					=		objResourceMgr.GetString("txtDESC_HOUSE_FOR_SALE");
			//capANY_NON_RESI_PROPERTY.Text				=		objResourceMgr.GetString("cmbANY_NON_RESI_PROPERTY");
			//capDESC_NON_RESI_PROPERTY.Text				=		objResourceMgr.GetString("txtDESC_NON_RESI_PROPERTY");
			capTRAMPOLINE.Text							=		objResourceMgr.GetString("cmbTRAMPOLINE");
			capDESC_TRAMPOLINE.Text						=		objResourceMgr.GetString("txtDESC_TRAMPOLINE");
			//capSTRUCT_ORI_BUILT_FOR.Text				=		objResourceMgr.GetString("cmbSTRUCT_ORI_BUILT_FOR");
			//capDESC_STRUCT_ORI_BUILT_FOR.Text			=		objResourceMgr.GetString("txtDESC_STRUCT_ORI_BUILT_FOR");
			capLEAD_PAINT_HAZARD.Text					=		objResourceMgr.GetString("cmbLEAD_PAINT_HAZARD");
			capDESC_LEAD_PAINT_HAZARD.Text				=		objResourceMgr.GetString("txtDESC_LEAD_PAINT_HAZARD");
			//capFUEL_OIL_TANK_PERMISES.Text				=		objResourceMgr.GetString("cmbFUEL_OIL_TANK_PERMISES");
			//capDESC_FUEL_OIL_TANK_PERMISES.Text			=		objResourceMgr.GetString("txtDESC_FUEL_OIL_TANK_PERMISES");
			capRENTERS.Text								=		objResourceMgr.GetString("cmbRENTERS");
			capDESC_RENTERS.Text						=		objResourceMgr.GetString("txtDESC_RENTERS");
			capBUILD_UNDER_CON_GEN_CONT.Text			=		objResourceMgr.GetString("cmbBUILD_UNDER_CON_GEN_CONT");
			capREMARKS.Text								=		objResourceMgr.GetString("txtREMARKS");
			capMULTI_POLICY_DISC_APPLIED.Text           =       objResourceMgr.GetString("cmbMULTI_POLICY_DISC_APPLIED");
			capNO_OF_PETS.Text							=       objResourceMgr.GetString("cmbNO_OF_PETS");
			capLAST_INSPECTED_DATE.Text	=		objResourceMgr.GetString("txtLAST_INSPECTED_DATE");
			
			//-----------------------------------------Added by mohit.

			capIS_VACENT_OCCUPY.Text=objResourceMgr.GetString("cmbIS_VACENT_OCCUPY");
			capIS_RENTED_IN_PART.Text=objResourceMgr.GetString("cmbIS_RENTED_IN_PART");
			capIS_DWELLING_OWNED_BY_OTHER.Text=objResourceMgr.GetString("cmbIS_DWELLING_OWNED_BY_OTHER");
			capIS_PROP_NEXT_COMMERICAL.Text=objResourceMgr.GetString("cmbIS_PROP_NEXT_COMMERICAL");
			capARE_STAIRWAYS_PRESENT.Text=objResourceMgr.GetString("cmbARE_STAIRWAYS_PRESENT");
			capIS_OWNERS_DWELLING_CHANGED.Text=objResourceMgr.GetString("cmbIS_OWNERS_DWELLING_CHANGED");
			//capIS_INSURANCE_DEC_CANCEL.Text=objResourceMgr.GetString("cmbIS_INSURANCE_DEC_CANCEL");
						
			capDESC_PROPERTY.Text=objResourceMgr.GetString("txtDESC_PROPERTY");
			capDESC_OWNER.Text=objResourceMgr.GetString("txtDESC_OWNER");
			capDESC_STAIRWAYS.Text=objResourceMgr.GetString("txtDESC_STAIRWAYS");
			//capDESC_DECLINE.Text=objResourceMgr.GetString("txtDESC_DECLINE");
			capDESC_PROPERTY.Text=objResourceMgr.GetString("txtDESC_PROPERTY");
			capDESC_OWNER.Text=objResourceMgr.GetString("txtDESC_OWNER");
			capDESC_STAIRWAYS.Text=objResourceMgr.GetString("txtDESC_STAIRWAYS");
			capDESC_VACENT_OCCUPY.Text=objResourceMgr.GetString("txtDESC_VACENT_OCCUPY");	
			capDESC_RENTED_IN_PART.Text=objResourceMgr.GetString("txtDESC_RENTED_IN_PART");
			capDESC_DWELLING_OWNED_BY_OTHER.Text=objResourceMgr.GetString("txtDESC_DWELLING_OWNED_BY_OTHER");
			//capANY_HEATING_SOURCE.Text=objResourceMgr.GetString("cmbANY_HEATING_SOURCE");
			capANY_HEATING_SOURCE.Text=objResourceMgr.GetString("cmbANY_HEATING_SOURCE");	
			capDESC_ANY_HEATING_SOURCE.Text=objResourceMgr.GetString("txtDESC_ANY_HEATING_SOURCE");	
			capNON_SMOKER_CREDIT.Text=objResourceMgr.GetString("cmbNON_SMOKER_CREDIT");	
			capAny_Forming.Text=objResourceMgr.GetString("cmbAny_Forming");	
			capPremises.Text=objResourceMgr.GetString("cmbPremises");	
			capOf_Acres.Text=objResourceMgr.GetString("txtOf_Acres");	
			capIsAny_Horse.Text=objResourceMgr.GetString("cmbIsAny_Horse");	
//			capOf_Acres_P.Text=objResourceMgr.GetString("txtOf_Acres_P");	
			capNo_Horses.Text=objResourceMgr.GetString("txtNo_Horses");	
			capLocation.Text =objResourceMgr.GetString("txtLocation");
			capDESC_Location.Text=objResourceMgr.GetString("txtDESC_Location");
			//-------------------------------End.-----------------
			
			capYEARS_INSU.Text          =   objResourceMgr.GetString("txtYEARS_INSU");
			capYEARS_INSU_WOL.Text      =   objResourceMgr.GetString("txtYEARS_INSU_WOL");

			capDESC_IS_SWIMPOLL_HOTTUB.Text                    =   objResourceMgr.GetString("txtDESC_IS_SWIMPOLL_HOTTUB");
			capDESC_MULTI_POLICY_DISC_APPLIED.Text             =   objResourceMgr.GetString("txtDESC_MULTI_POLICY_DISC_APPLIED");
			capDESC_BUILD_UNDER_CON_GEN_CONT.Text              =   objResourceMgr.GetString("txtDESC_BUILD_UNDER_CON_GEN_CONT");
			capIS_SWIMPOLL_HOTTUB.Text							=   objResourceMgr.GetString("cmbIS_SWIMPOLL_HOTTUB");
			capDIVING_BOARD.Text								=   objResourceMgr.GetString("cmbDIVING_BOARD");
			capAPPROVED_FENCE.Text								=   objResourceMgr.GetString("cmbAPPROVED_FENCE");
			capSLIDE.Text										=   objResourceMgr.GetString("cmbSLIDE");

			capPROVIDE_HOME_DAY_CARE.Text				=		objResourceMgr.GetString("cmbPROVIDE_HOME_DAY_CARE");
			capMODULAR_MANUFACTURED_HOME.Text			=		objResourceMgr.GetString("cmbMODULAR_MANUFACTURED_HOME");							
			capBUILT_ON_CONTINUOUS_FOUNDATION.Text		=		objResourceMgr.GetString("cmbBUILT_ON_CONTINUOUS_FOUNDATION");							

			capANY_PRIOR_LOSSES.Text					=		objResourceMgr.GetString("cmbANY_PRIOR_LOSSES");
			capANY_PRIOR_LOSSES_DESC.Text				=		objResourceMgr.GetString("txtANY_PRIOR_LOSSES_DESC");
			capBOAT_WITH_HOMEOWNER.Text					=		objResourceMgr.GetString("cmbBOAT_WITH_HOMEOWNER");
            //Added for Itrack Issue 6640 on 11 Dec 09
			capNON_WEATHER_CLAIMS.Text					=		objResourceMgr.GetString("txtNON_WEATHER_CLAIMS");
			capWEATHER_CLAIMS.Text						=		objResourceMgr.GetString("txtWEATHER_CLAIMS");

		}
		#endregion

		#region get old data as XML
		private void GetOldDataXML()
		{
			if(this.hidCUSTOMER_ID.Value != "") 
			{
				hidOldData.Value = ClsHomeGeneralInformation.GetPolicyGeneralInformationXml(
					int.Parse(hidCUSTOMER_ID.Value),
					int.Parse(hidPOL_ID.Value),
					int.Parse(hidPOL_VERSION_ID.Value));

				//Added by Charles on 9-Dec-09 for Itrack 6489
				if(strCalledFrom.ToUpper()=="HOME")
				{
					hidOTHER_DESCRIPTION_VALUE.Value = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("OTHER_DESCRIPTION",hidOldData.Value); 
					strOTHER_DESCRIPTION_VALUE = hidOTHER_DESCRIPTION_VALUE.Value;
				}//Added till here
			}
		}
		#endregion

		private void PopulateComboBox()
		{	
			IList objList = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			
			//---------------------Added by mohit----------------
			cmbIS_VACENT_OCCUPY.DataSource = objList;
			cmbIS_VACENT_OCCUPY.DataTextField="LookupDesc"; 
			cmbIS_VACENT_OCCUPY.DataValueField="LookupCode";
			cmbIS_VACENT_OCCUPY.DataBind();				

			cmbIS_RENTED_IN_PART.DataSource = objList;
			cmbIS_RENTED_IN_PART.DataTextField="LookupDesc"; 
			cmbIS_RENTED_IN_PART.DataValueField="LookupCode";
			cmbIS_RENTED_IN_PART.DataBind();			
			
			cmbIS_DWELLING_OWNED_BY_OTHER.DataSource = objList;
			cmbIS_DWELLING_OWNED_BY_OTHER.DataTextField="LookupDesc"; 
			cmbIS_DWELLING_OWNED_BY_OTHER.DataValueField="LookupCode";
			cmbIS_DWELLING_OWNED_BY_OTHER.DataBind();					

			cmbIS_PROP_NEXT_COMMERICAL.DataSource = objList;
			cmbIS_PROP_NEXT_COMMERICAL.DataTextField="LookupDesc"; 
			cmbIS_PROP_NEXT_COMMERICAL.DataValueField="LookupCode";
			cmbIS_PROP_NEXT_COMMERICAL.DataBind();			

			cmbNON_SMOKER_CREDIT.DataSource = objList;
			cmbNON_SMOKER_CREDIT.DataTextField="LookupDesc"; 
			cmbNON_SMOKER_CREDIT.DataValueField="LookupCode";
			cmbNON_SMOKER_CREDIT.DataBind();	
				
			cmbIS_OWNERS_DWELLING_CHANGED.DataSource = objList;
			cmbIS_OWNERS_DWELLING_CHANGED.DataTextField="LookupDesc"; 
			cmbIS_OWNERS_DWELLING_CHANGED.DataValueField="LookupCode";
			cmbIS_OWNERS_DWELLING_CHANGED.DataBind();				

			cmbARE_STAIRWAYS_PRESENT.DataSource =objList;
			cmbARE_STAIRWAYS_PRESENT.DataTextField="LookupDesc"; 
			cmbARE_STAIRWAYS_PRESENT.DataValueField="LookupCode";
			cmbARE_STAIRWAYS_PRESENT.DataBind();			

			cmbANY_HEATING_SOURCE.DataSource =objList;
			cmbANY_HEATING_SOURCE.DataTextField="LookupDesc"; 
			cmbANY_HEATING_SOURCE.DataValueField="LookupCode";
			cmbANY_HEATING_SOURCE.DataBind();

			//---------end----------------
						
			cmbANY_COV_DECLINED_CANCELED.DataSource = objList;
			cmbANY_COV_DECLINED_CANCELED.DataTextField="LookupDesc"; 
			cmbANY_COV_DECLINED_CANCELED.DataValueField="LookupCode";
			cmbANY_COV_DECLINED_CANCELED.DataBind();	
			
			cmbANY_FARMING_BUSINESS_COND.DataSource = objList;
			cmbANY_FARMING_BUSINESS_COND.DataTextField="LookupDesc"; 
			cmbANY_FARMING_BUSINESS_COND.DataValueField="LookupCode";
			cmbANY_FARMING_BUSINESS_COND.DataBind();

			cmbPROVIDE_HOME_DAY_CARE.DataSource = objList;
			cmbPROVIDE_HOME_DAY_CARE.DataTextField="LookupDesc"; 
			cmbPROVIDE_HOME_DAY_CARE.DataValueField="LookupCode";
			cmbPROVIDE_HOME_DAY_CARE.DataBind();		

			cmbANIMALS_EXO_PETS_HISTORY.DataSource = objList;
			cmbANIMALS_EXO_PETS_HISTORY.DataTextField="LookupDesc"; 
			cmbANIMALS_EXO_PETS_HISTORY.DataValueField="LookupCode";
			cmbANIMALS_EXO_PETS_HISTORY.DataBind();				
			
			cmbANY_OTH_INSU_COMP.DataSource = objList;
			cmbANY_OTH_INSU_COMP.DataTextField="LookupDesc"; 
			cmbANY_OTH_INSU_COMP.DataValueField="LookupCode";
			cmbANY_OTH_INSU_COMP.DataBind();
				
			cmbANY_OTHER_RESI_OWNED.DataSource = objList;
			cmbANY_OTHER_RESI_OWNED.DataTextField="LookupDesc"; 
			cmbANY_OTHER_RESI_OWNED.DataValueField="LookupCode";
			cmbANY_OTHER_RESI_OWNED.DataBind();
					
			cmbANY_RENOVATION.DataSource = objList;
			cmbANY_RENOVATION.DataTextField="LookupDesc"; 
			cmbANY_RENOVATION.DataValueField="LookupCode";
			cmbANY_RENOVATION.DataBind();			 
			
			cmbANY_RESIDENCE_EMPLOYEE.DataSource =objList;
			cmbANY_RESIDENCE_EMPLOYEE.DataTextField="LookupDesc"; 
			cmbANY_RESIDENCE_EMPLOYEE.DataValueField="LookupCode";
			cmbANY_RESIDENCE_EMPLOYEE.DataBind();				
				
			cmbBUILD_UNDER_CON_GEN_CONT.DataSource = objList;
			cmbBUILD_UNDER_CON_GEN_CONT.DataTextField="LookupDesc"; 
			cmbBUILD_UNDER_CON_GEN_CONT.DataValueField="LookupCode";
			cmbBUILD_UNDER_CON_GEN_CONT.DataBind();			
			
			cmbCONVICTION_DEGREE_IN_PAST.DataSource = objList;
			cmbCONVICTION_DEGREE_IN_PAST.DataTextField="LookupDesc"; 
			cmbCONVICTION_DEGREE_IN_PAST.DataValueField="LookupCode";
			cmbCONVICTION_DEGREE_IN_PAST.DataBind();		
			
			cmbHAS_INSU_TRANSFERED_AGENCY.DataSource = objList;
			cmbHAS_INSU_TRANSFERED_AGENCY.DataTextField="LookupDesc"; 
			cmbHAS_INSU_TRANSFERED_AGENCY.DataValueField="LookupCode";
			cmbHAS_INSU_TRANSFERED_AGENCY.DataBind();					

			cmbLEAD_PAINT_HAZARD.DataSource = objList;
			cmbLEAD_PAINT_HAZARD.DataTextField="LookupDesc"; 
			cmbLEAD_PAINT_HAZARD.DataValueField="LookupCode";
			cmbLEAD_PAINT_HAZARD.DataBind();
		
			cmbMULTI_POLICY_DISC_APPLIED.DataSource =objList;
			cmbMULTI_POLICY_DISC_APPLIED.DataTextField="LookupDesc"; 
			cmbMULTI_POLICY_DISC_APPLIED.DataValueField="LookupCode";
			cmbMULTI_POLICY_DISC_APPLIED.DataBind();
		
			cmbRENTERS.DataSource =objList;
			cmbRENTERS.DataTextField="LookupDesc"; 
			cmbRENTERS.DataValueField="LookupCode";
			cmbRENTERS.DataBind();
	    					
			cmbTRAMPOLINE.DataSource = objList;
			cmbTRAMPOLINE.DataTextField="LookupDesc"; 
			cmbTRAMPOLINE.DataValueField="LookupCode";
			cmbTRAMPOLINE.DataBind();			
						
			// Added by mohit on 4/11/2005.
			cmbSWIMMING_POOL_TYPE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("SPLCD");
			cmbSWIMMING_POOL_TYPE.DataTextField="LookupDesc"; 
			cmbSWIMMING_POOL_TYPE.DataValueField="LookupCode";
			cmbSWIMMING_POOL_TYPE.DataBind();

			//Added BY Shafi On 23/01/2006
			cmbOTHER_DESCRIPTION.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("HBDOG","-1","Y");
			cmbOTHER_DESCRIPTION.DataTextField ="LookupDesc";
			cmbOTHER_DESCRIPTION.DataValueField ="LookupID";
			cmbOTHER_DESCRIPTION.DataBind();
			cmbOTHER_DESCRIPTION.Items.Insert(0,"");

			//Added by Sumit
			cmbAny_Forming.DataSource =objList ;
			cmbAny_Forming.DataTextField="LookupDesc"; 
			cmbAny_Forming.DataValueField="LookupCode";
			cmbAny_Forming.DataBind();

			cmbPremises.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("HAFO");
			cmbPremises.DataTextField ="LookupDesc";
			cmbPremises.DataValueField = "LookupID";
			cmbPremises.DataBind();
			cmbPremises.Items.Insert(0,new ListItem("",""));
			cmbPremises.SelectedIndex=0;

			cmbIsAny_Horse.DataSource =objList ;
			cmbIsAny_Horse.DataTextField="LookupDesc"; 
			cmbIsAny_Horse.DataValueField="LookupCode";
			cmbIsAny_Horse.DataBind();

			cmbDIVING_BOARD.DataSource = objList;
			cmbDIVING_BOARD.DataTextField="LookupDesc"; 
			cmbDIVING_BOARD.DataValueField="LookupCode";
			cmbDIVING_BOARD.DataBind();

			cmbAPPROVED_FENCE.DataSource = objList;
			cmbAPPROVED_FENCE.DataTextField="LookupDesc"; 
			cmbAPPROVED_FENCE.DataValueField="LookupCode";
			cmbAPPROVED_FENCE.DataBind();

			cmbSLIDE.DataSource = objList;
			cmbSLIDE.DataTextField="LookupDesc"; 
			cmbSLIDE.DataValueField="LookupCode";
			cmbSLIDE.DataBind();

			//RP
			cmbPROVIDE_HOME_DAY_CARE.DataSource = objList;
			cmbPROVIDE_HOME_DAY_CARE.DataTextField="LookupDesc"; 
			cmbPROVIDE_HOME_DAY_CARE.DataValueField="LookupCode";
			cmbPROVIDE_HOME_DAY_CARE.DataBind();

			cmbMODULAR_MANUFACTURED_HOME.DataSource = objList;
			cmbMODULAR_MANUFACTURED_HOME.DataTextField="LookupDesc"; 
			cmbMODULAR_MANUFACTURED_HOME.DataValueField="LookupCode";
			cmbMODULAR_MANUFACTURED_HOME.DataBind();

			cmbBUILT_ON_CONTINUOUS_FOUNDATION.DataSource = objList;
			cmbBUILT_ON_CONTINUOUS_FOUNDATION.DataTextField="LookupDesc"; 
			cmbBUILT_ON_CONTINUOUS_FOUNDATION.DataValueField="LookupCode";
			cmbBUILT_ON_CONTINUOUS_FOUNDATION.DataBind();

			cmbVALUED_CUSTOMER_DISCOUNT_OVERRIDE.DataSource = objList;
			cmbVALUED_CUSTOMER_DISCOUNT_OVERRIDE.DataTextField="LookupDesc"; 
			cmbVALUED_CUSTOMER_DISCOUNT_OVERRIDE.DataValueField="LookupCode";
			cmbVALUED_CUSTOMER_DISCOUNT_OVERRIDE.DataBind();

			cmbPROPERTY_ON_MORE_THAN.DataSource = objList;
			cmbPROPERTY_ON_MORE_THAN.DataTextField="LookupDesc"; 
			cmbPROPERTY_ON_MORE_THAN.DataValueField="LookupCode";
			cmbPROPERTY_ON_MORE_THAN.DataBind();

			cmbDWELLING_MOBILE_HOME.DataSource = objList;
			cmbDWELLING_MOBILE_HOME.DataTextField="LookupDesc"; 
			cmbDWELLING_MOBILE_HOME.DataValueField="LookupCode";
			cmbDWELLING_MOBILE_HOME.DataBind();

			cmbPROPERTY_USED_WHOLE_PART.DataSource = objList;
			cmbPROPERTY_USED_WHOLE_PART.DataTextField="LookupDesc"; 
			cmbPROPERTY_USED_WHOLE_PART.DataValueField="LookupCode";
			cmbPROPERTY_USED_WHOLE_PART.DataBind();
			//end of addition by RP

			cmbANY_PRIOR_LOSSES.DataSource		= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbANY_PRIOR_LOSSES.DataTextField	= "LookupDesc"; 
			cmbANY_PRIOR_LOSSES.DataValueField	= "LookupCode";
			cmbANY_PRIOR_LOSSES.DataBind();
			//cmbANY_PRIOR_LOSSES.Items.Insert(0,"");

			cmbBOAT_WITH_HOMEOWNER.DataSource		= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbBOAT_WITH_HOMEOWNER.DataTextField	= "LookupDesc"; 
			cmbBOAT_WITH_HOMEOWNER.DataValueField	= "LookupCode";
			cmbBOAT_WITH_HOMEOWNER.DataBind();
			//cmbBOAT_WITH_HOMEOWNER.Items.Insert(0,"");				
		}

		private void SetWorkFlow()
		{//Added ScreenID for the Home Qs.
			if(base.ScreenId == "240" || base.ScreenId == "260")
			{
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
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