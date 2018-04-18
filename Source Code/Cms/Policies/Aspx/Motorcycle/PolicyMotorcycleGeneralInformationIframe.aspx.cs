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
	public class PolicyMotorcycleGeneralInformationIframe :  Cms.Policies.policiesbase
	{		
		#region 
		protected System.Web.UI.WebControls.Label capANY_PRIOR_LOSSES;
		protected System.Web.UI.WebControls.DropDownList cmbANY_PRIOR_LOSSES;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_PRIOR_LOSSES;
		protected System.Web.UI.WebControls.Label capANY_PRIOR_LOSSES_DESC;
		protected System.Web.UI.WebControls.TextBox txtANY_PRIOR_LOSSES_DESC;
		protected System.Web.UI.WebControls.Label lblANY_PRIOR_LOSSES_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_PRIOR_LOSSES_DESC;
		protected System.Web.UI.WebControls.Label capAPPLY_PERS_UMB_POL;
		protected System.Web.UI.WebControls.DropDownList cmbAPPLY_PERS_UMB_POL;
		protected System.Web.UI.WebControls.Label capAPPLY_PERS_UMB_POL_DESC;
		protected System.Web.UI.WebControls.TextBox txtAPPLY_PERS_UMB_POL_DESC;
		protected System.Web.UI.WebControls.Label lblAPPLY_PERS_UMB_POL_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAPPLY_PERS_UMB_POL_DESC;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capANY_NON_OWNED_VEH;
		protected System.Web.UI.WebControls.DropDownList cmbANY_NON_OWNED_VEH;
		protected System.Web.UI.WebControls.Label capEXISTING_DMG;
		protected System.Web.UI.WebControls.DropDownList cmbEXISTING_DMG;
		protected System.Web.UI.WebControls.Label capANY_OTH_INSU_COMP;
		protected System.Web.UI.WebControls.DropDownList cmbANY_OTH_INSU_COMP;
		protected System.Web.UI.WebControls.Label capDRIVER_SUS_REVOKED;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_SUS_REVOKED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_SUS_REVOKED;
		protected System.Web.UI.WebControls.Label capPHY_MENTL_CHALLENGED;
		protected System.Web.UI.WebControls.DropDownList cmbPHY_MENTL_CHALLENGED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPHY_MENTL_CHALLENGED;
		protected System.Web.UI.WebControls.Label capANY_FINANCIAL_RESPONSIBILITY;
		protected System.Web.UI.WebControls.DropDownList cmbANY_FINANCIAL_RESPONSIBILITY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_FINANCIAL_RESPONSIBILITY;
		protected System.Web.UI.WebControls.Label capINS_AGENCY_TRANSFER;
		protected System.Web.UI.WebControls.DropDownList cmbINS_AGENCY_TRANSFER;
		protected System.Web.UI.WebControls.Label capCOVERAGE_DECLINED;
		protected System.Web.UI.WebControls.DropDownList cmbCOVERAGE_DECLINED;
		protected System.Web.UI.WebControls.Label capAGENCY_VEH_INSPECTED;
		protected System.Web.UI.WebControls.DropDownList cmbAGENCY_VEH_INSPECTED;
		protected System.Web.UI.WebControls.Label capSALVAGE_TITLE;
		protected System.Web.UI.WebControls.DropDownList cmbSALVAGE_TITLE;
		protected System.Web.UI.WebControls.Label capIS_COMMERCIAL_USE;
		protected System.Web.UI.WebControls.DropDownList cmbIS_COMMERCIAL_USE;
		protected System.Web.UI.WebControls.Label capIS_USEDFOR_RACING;
		protected System.Web.UI.WebControls.DropDownList cmbIS_USEDFOR_RACING;
		protected System.Web.UI.WebControls.Label capIS_COST_OVER_DEFINED_LIMIT;
		protected System.Web.UI.WebControls.DropDownList cmbIS_COST_OVER_DEFINED_LIMIT;
		protected System.Web.UI.WebControls.Label capIS_MORE_WHEELS;
		protected System.Web.UI.WebControls.DropDownList cmbIS_MORE_WHEELS;
		protected System.Web.UI.WebControls.Label capIS_EXTENDED_FORKS;
		protected System.Web.UI.WebControls.DropDownList cmbIS_EXTENDED_FORKS;
		protected System.Web.UI.WebControls.Label capIS_LICENSED_FOR_ROAD;
		protected System.Web.UI.WebControls.DropDownList cmbIS_LICENSED_FOR_ROAD;
		protected System.Web.UI.WebControls.Label capIS_MODIFIED_INCREASE_SPEED;
		protected System.Web.UI.WebControls.DropDownList cmbIS_MODIFIED_INCREASE_SPEED;
		protected System.Web.UI.WebControls.Label capIS_MODIFIED_KIT;
		protected System.Web.UI.WebControls.DropDownList cmbIS_MODIFIED_KIT;
		protected System.Web.UI.WebControls.Label capIS_TAKEN_OUT;
		protected System.Web.UI.WebControls.DropDownList cmbIS_TAKEN_OUT;
		protected System.Web.UI.WebControls.Label capIS_CONVICTED_CARELESS_DRIVE;
		protected System.Web.UI.WebControls.DropDownList cmbIS_CONVICTED_CARELESS_DRIVE;
		protected System.Web.UI.WebControls.Label capIS_CONVICTED_ACCIDENT;
		protected System.Web.UI.WebControls.DropDownList cmbIS_CONVICTED_ACCIDENT;
		protected System.Web.UI.WebControls.Label capREMARKS;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;        
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_COST_OVER_DEFINED_LIMIT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRowId;
		protected System.Web.UI.WebControls.DropDownList cmbIS_OTHER_THAN_INSURED;	
		protected System.Web.UI.HtmlControls.HtmlTableRow trIS_TAKEN_OUT;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;

		///
		
		/*protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_SUS_REVOKED;		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPHY_MENTL_CHALLENGED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_FINANCIAL_RESPONSIBILITY;*/
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_NON_OWNED_VEH_MC_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXISTING_DMG_MC_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_OTH_INSU_COMP_MC_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_SUS_REVOKED_MC_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPHY_MENTL_CHALLENGED_MC_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMULTI_POLICY_DISC_APPLIED_MC_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSALVAGE_TITLE_MC_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_FINANCIAL_RESPONSIBILITY_MC_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINS_AGENCY_TRANSFER_MC_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_VEH_INSPECTED_MC_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOVERAGE_DECLINED_MC_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_CONVICTED_CARELESS_DRIVE_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_TAKEN_OUT_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_CONVICTED_ACCIDENT_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_COST_OVER_DEFINED_LIMIT_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_MORE_WHEELS_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_COMMERCIAL_USE_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_MODIFIED_KIT_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_MODIFIED_INCREASE_SPEED_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_LICENSED_FOR_ROAD_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_EXTENDED_FORKS_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_USEDFOR_RACING_DESC;
		/////
		#endregion 
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.CustomValidator csvREMARKS;
		protected System.Web.UI.HtmlControls.HtmlTableRow trMessage;
		protected System.Web.UI.HtmlControls.HtmlTableRow messageID;
		protected System.Web.UI.HtmlControls.HtmlForm POL_AUTO_GEN_INFO;
		protected System.Web.UI.WebControls.Label capMULTI_POLICY_DISC_APPLIED;
		protected System.Web.UI.WebControls.DropDownList cmbMULTI_POLICY_DISC_APPLIED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMULTI_POLICY_DISC_APPLIED;
		protected System.Web.UI.WebControls.TextBox txtANY_NON_OWNED_VEH_MC_DESC;
		protected System.Web.UI.WebControls.TextBox txtEXISTING_DMG_MC_DESC;
		protected System.Web.UI.WebControls.TextBox txtANY_OTH_INSU_COMP_MC_DESC;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_SUS_REVOKED_MC_DESC;
		protected System.Web.UI.WebControls.TextBox txtPHY_MENTL_CHALLENGED_MC_DESC;
		protected System.Web.UI.WebControls.TextBox txtANY_FINANCIAL_RESPONSIBILITY_MC_DESC;
		protected System.Web.UI.WebControls.TextBox txtINS_AGENCY_TRANSFER_MC_DESC;
		protected System.Web.UI.WebControls.TextBox txtCOVERAGE_DECLINED_MC_DESC;
		protected System.Web.UI.WebControls.TextBox txtAGENCY_VEH_INSPECTED_MC_DESC;
		protected System.Web.UI.WebControls.TextBox txtSALVAGE_TITLE_MC_DESC;
		protected System.Web.UI.WebControls.TextBox txtIS_COMMERCIAL_USE_DESC;
		protected System.Web.UI.WebControls.TextBox txtIS_USEDFOR_RACING_DESC;
		protected System.Web.UI.WebControls.TextBox txtIS_COST_OVER_DEFINED_LIMIT_DESC;
		protected System.Web.UI.WebControls.TextBox txtIS_MORE_WHEELS_DESC;
		protected System.Web.UI.WebControls.TextBox txtIS_EXTENDED_FORKS_DESC;
		protected System.Web.UI.WebControls.TextBox txtIS_LICENSED_FOR_ROAD_DESC;
		protected System.Web.UI.WebControls.TextBox txtIS_MODIFIED_INCREASE_SPEED_DESC;
		protected System.Web.UI.WebControls.TextBox txtIS_MODIFIED_KIT_DESC;
		protected System.Web.UI.WebControls.TextBox txtIS_TAKEN_OUT_DESC;
		protected System.Web.UI.WebControls.TextBox txtIS_CONVICTED_CARELESS_DRIVE_DESC;
		protected System.Web.UI.WebControls.Label capANY_NON_OWNED_VEH_MC_DESC;
		protected System.Web.UI.WebControls.Label capEXISTING_DMG_MC_DESC;
		protected System.Web.UI.WebControls.Label capINS_AGENCY_TRANSFER_MC_DESC;
		protected System.Web.UI.WebControls.Label capANY_OTH_INSU_COMP_MC_DESC;
		protected System.Web.UI.WebControls.Label capDRIVER_SUS_REVOKED_MC_DESC;
		protected System.Web.UI.WebControls.Label capPHY_MENTL_CHALLENGED_MC_DESC;
		protected System.Web.UI.WebControls.Label capANY_FINANCIAL_RESPONSIBILITY_MC_DESC;
		protected System.Web.UI.WebControls.Label capCOVERAGE_DECLINED_MC_DESC;
		protected System.Web.UI.WebControls.Label capAGENCY_VEH_INSPECTED_MC_DESC;
		protected System.Web.UI.WebControls.Label capSALVAGE_TITLE_MC_DESC;
		protected System.Web.UI.WebControls.Label capIS_COMMERCIAL_USE_DESC;
		protected System.Web.UI.WebControls.Label capIS_USEDFOR_RACING_DESC;
		protected System.Web.UI.WebControls.Label capIS_COST_OVER_DEFINED_LIMIT_DESC;
		protected System.Web.UI.WebControls.Label capIS_MORE_WHEELS_DESC;
		protected System.Web.UI.WebControls.Label capIS_EXTENDED_FORKS_DESC;
		protected System.Web.UI.WebControls.Label capIS_LICENSED_FOR_ROAD_DESC;
		protected System.Web.UI.WebControls.Label capIS_MODIFIED_INCREASE_SPEED_DESC;
		protected System.Web.UI.WebControls.Label capIS_MODIFIED_KIT_DESC;
		protected System.Web.UI.WebControls.Label capIS_TAKEN_OUT_DESC;
		protected System.Web.UI.WebControls.Label capIS_CONVICTED_CARELESS_DRIVE_DESC;
		protected System.Web.UI.WebControls.Label capIS_CONVICTED_ACCIDENT_DESC;
		protected System.Web.UI.WebControls.TextBox txtIS_CONVICTED_ACCIDENT_DESC;
		protected System.Web.UI.WebControls.Label capMULTI_POLICY_DISC_APPLIED_MC_DESC;
		protected System.Web.UI.WebControls.TextBox txtMULTI_POLICY_DISC_APPLIED_MC_DESC;
		protected System.Web.UI.WebControls.Label lblANY_NON_OWNED_VEH_MC_DESC;
		protected System.Web.UI.WebControls.Label lblEXISTING_DMG_MC_DESC;
		protected System.Web.UI.WebControls.Label lblANY_OTH_INSU_COMP_MC_DESC;
		protected System.Web.UI.WebControls.Label lblDRIVER_SUS_REVOKED_MC_DESC;
		protected System.Web.UI.WebControls.Label lblPHY_MENTL_CHALLENGED_MC_DESC;
		protected System.Web.UI.WebControls.Label lblANY_FINANCIAL_RESPONSIBILITY_MC_DESC;
		protected System.Web.UI.WebControls.Label lblINS_AGENCY_TRANSFER_MC_DESC;
		protected System.Web.UI.WebControls.Label lblCOVERAGE_DECLINED_MC_DESC;
		protected System.Web.UI.WebControls.Label lblAGENCY_VEH_INSPECTED_MC_DESC;
		protected System.Web.UI.WebControls.Label lblSALVAGE_TITLE_MC_DESC;
		protected System.Web.UI.WebControls.Label lblIS_COMMERCIAL_USE_DESC;
		protected System.Web.UI.WebControls.Label lblIS_USEDFOR_RACING_DESC;
		protected System.Web.UI.WebControls.Label lblIS_COST_OVER_DEFINED_LIMIT_DESC;
		protected System.Web.UI.WebControls.Label lblIS_EXTENDED_FORKS_DESC;
		protected System.Web.UI.WebControls.Label lblIS_LICENSED_FOR_ROAD_DESC;
		protected System.Web.UI.WebControls.Label lblIS_MODIFIED_INCREASE_SPEED_DESC;
		protected System.Web.UI.WebControls.Label lblIS_MODIFIED_KIT_DESC;
		protected System.Web.UI.WebControls.Label lblIS_TAKEN_OUT_DESC;
		protected System.Web.UI.WebControls.Label lblIS_CONVICTED_CARELESS_DRIVE_DESC;
		protected System.Web.UI.WebControls.Label lblIS_CONVICTED_ACCIDENT_DESC;
		protected System.Web.UI.WebControls.Label lblMULTI_POLICY_DISC_APPLIED_MC_DESC;
		protected System.Web.UI.WebControls.TextBox txtREMARKS;
		protected System.Web.UI.WebControls.Label lblIS_MORE_WHEELS_DESC;
		/*protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_NON_OWNED_VEH_MC_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXISTING_DMG_MC_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_OTH_INSU_COMP_MC_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_SUS_REVOKED_MC_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPHY_MENTL_CHALLENGED_MC_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMULTI_POLICY_DISC_APPLIED_MC_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSALVAGE_TITLE_MC_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_FINANCIAL_RESPONSIBILITY_MC_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINS_AGENCY_TRANSFER_MC_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_VEH_INSPECTED_MC_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOVERAGE_DECLINED_MC_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_CONVICTED_CARELESS_DRIVE_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_TAKEN_OUT_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_CONVICTED_ACCIDENT_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_COST_OVER_DEFINED_LIMIT_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_MORE_WHEELS_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_COMMERCIAL_USE_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_MODIFIED_KIT_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_MODIFIED_INCREASE_SPEED_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_LICENSED_FOR_ROAD_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_EXTENDED_FORKS_DESC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_USEDFOR_RACING_DESC;*/
		protected System.Web.UI.WebControls.Label capCURR_RES_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbCURR_RES_TYPE;
		protected System.Web.UI.WebControls.Label capIS_OTHER_THAN_INSURED;
		protected System.Web.UI.WebControls.Label capFULL_NAME;
		protected System.Web.UI.WebControls.TextBox txtFullName;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFULL_NAME;
		protected System.Web.UI.WebControls.Label capDOB;
		protected System.Web.UI.WebControls.TextBox txtDATE_OF_BIRTH;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDOB;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDOB;
		protected System.Web.UI.WebControls.CustomValidator csvDOB;
		protected System.Web.UI.WebControls.Label capDRIV_LIC;
		protected System.Web.UI.WebControls.TextBox txtDrivingLisence;
		protected System.Web.UI.WebControls.Label capMOTORCYCLE;
		protected System.Web.UI.WebControls.DropDownList cmbWhichCycle;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMOTORCYCLE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected System.Web.UI.WebControls.HyperLink hlkDRIVER_DOB;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label capVIN;
		protected System.Web.UI.WebControls.TextBox txtVIN;
		protected System.Web.UI.WebControls.Label capINSURED_VEH_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtINSURED_VEH_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINSURED_VEH_NUMBER;
		protected System.Web.UI.WebControls.Label capVEHICLE_YEAR;
		protected System.Web.UI.WebControls.TextBox txtVEHICLE_YEAR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVEHICLE_YEAR;
		protected System.Web.UI.WebControls.RangeValidator rngVEHICLE_YEAR;
		protected System.Web.UI.WebControls.RangeValidator rngYEARS_INSU;
		protected System.Web.UI.WebControls.RangeValidator rngYEARS_INSU_WOL;
		protected System.Web.UI.WebControls.Label capMAKE;
		protected System.Web.UI.WebControls.TextBox txtMAKE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAKE;
		protected System.Web.UI.WebControls.Label capMODEL;
		protected System.Web.UI.WebControls.TextBox txtMODEL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMODEL;
		protected System.Web.UI.WebControls.Label capMOTORCYCLE_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbMOTORCYCLE_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMOTORCYCLE_TYPE;
		protected System.Web.UI.WebControls.Label capVEHICLE_CC;
		protected System.Web.UI.WebControls.TextBox txtVEHICLE_CC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVEHICLE_CC;
		protected System.Web.UI.WebControls.RegularExpressionValidator revVEHICLE_CC;
		protected System.Web.UI.WebControls.Label capAPP_VEHICLE_CLASS;
		protected System.Web.UI.WebControls.DropDownList cmbAPP_VEHICLE_CLASS;
		protected System.Web.UI.WebControls.Label Label1;
		protected Cms.CmsWeb.Controls.CmsButton btnPullCustomerAddress;
		protected System.Web.UI.WebControls.Label capGRG_ADD1;
		protected System.Web.UI.WebControls.TextBox txtGRG_ADD1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvGRG_ADD1;
		protected System.Web.UI.WebControls.Label capGRG_ADD2;
		protected System.Web.UI.WebControls.TextBox txtGRG_ADD2;
		protected System.Web.UI.WebControls.Label capGRG_CITY;
		protected System.Web.UI.WebControls.TextBox txtGRG_CITY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvGRG_CITY;
		protected System.Web.UI.WebControls.Label capGRG_COUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbGRG_COUNTRY;
		protected System.Web.UI.WebControls.Label capGRG_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbGRG_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvGRG_STATE;
		protected System.Web.UI.WebControls.Label capGRG_ZIP;
		protected System.Web.UI.WebControls.TextBox txtGRG_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revGRG_ZIP;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvGRG_ZIP;
		protected System.Web.UI.WebControls.Label capREGISTERED_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbREGISTERED_STATE;
		protected System.Web.UI.WebControls.Label capTERRITORY;
		protected System.Web.UI.WebControls.TextBox txtTERRITORY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTERRITORY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTERRITORY;
		protected System.Web.UI.WebControls.Label capAMOUNT;
		protected System.Web.UI.WebControls.TextBox txtAMOUNT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAMOUNT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAMOUNT;
		protected System.Web.UI.WebControls.Label capVEHICLE_AGE;
		protected System.Web.UI.WebControls.TextBox txtVEHICLE_AGE;
		protected Cms.CmsWeb.Controls.CmsButton btnCopy;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelectForVehicleMake;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelectVehicleModel;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVehicleID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPPID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMakeCode;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_LOB;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCheckZipSubmit;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCheckMakeSubmit;
		protected System.Web.UI.HtmlControls.HtmlForm APP_AUTO_GEN_INFO;       
		//Added By Shafi
		protected System.Web.UI.WebControls.Label capYEARS_INSU_WOL;
		protected System.Web.UI.WebControls.Label capYEARS_INSU;
		protected System.Web.UI.WebControls.TextBox txtYEARS_INSU;
		protected System.Web.UI.WebControls.TextBox	txtYEARS_INSU_WOL;
		protected System.Web.UI.WebControls.RegularExpressionValidator revYEARS_INSU;
		protected System.Web.UI.WebControls.RegularExpressionValidator revYEARS_INSU_WOL;
		protected System.Web.UI.WebControls.CustomValidator csvYEARS_INSU_WOL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_NON_OWNED_VEH;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXISTING_DMG;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvANY_OTH_INSU_COMP;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINS_AGENCY_TRANSFER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOVERAGE_DECLINED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_VEH_INSPECTED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSALVAGE_TITLE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_COMMERCIAL_USE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_USEDFOR_RACING;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_COST_OVER_DEFINED_LIMIT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_MORE_WHEELS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_EXTENDED_FORKS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_LICENSED_FOR_ROAD;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_MODIFIED_INCREASE_SPEED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_MODIFIED_KIT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_TAKEN_OUT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_CONVICTED_CARELESS_DRIVE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_CONVICTED_ACCIDENT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCURR_RES_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_OTHER_THAN_INSURED;
		//Defining the business layer class object
		Cms.BusinessLayer.BlApplication.ClsPPGeneralInformation  objPPGeneralInformation ;
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
			rfvANY_NON_OWNED_VEH.ErrorMessage      = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			rfvEXISTING_DMG.ErrorMessage		   = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			rfvANY_OTH_INSU_COMP.ErrorMessage      = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			rfvINS_AGENCY_TRANSFER.ErrorMessage    =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			rfvCOVERAGE_DECLINED.ErrorMessage	   =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			rfvAGENCY_VEH_INSPECTED.ErrorMessage   =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			rfvSALVAGE_TITLE.ErrorMessage		   =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			rfvIS_COMMERCIAL_USE.ErrorMessage	   =     Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			rfvIS_USEDFOR_RACING.ErrorMessage	   =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			rfvIS_COST_OVER_DEFINED_LIMIT.ErrorMessage	 =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			rfvIS_MORE_WHEELS.ErrorMessage			     =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			rfvIS_EXTENDED_FORKS.ErrorMessage		     =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			rfvIS_LICENSED_FOR_ROAD.ErrorMessage		 =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			rfvIS_MODIFIED_INCREASE_SPEED.ErrorMessage	 =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			rfvIS_MODIFIED_KIT.ErrorMessage				 =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			rfvIS_TAKEN_OUT.ErrorMessage			     =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			rfvIS_CONVICTED_CARELESS_DRIVE.ErrorMessage	 =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			rfvIS_CONVICTED_ACCIDENT.ErrorMessage		 =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			rfvCURR_RES_TYPE.ErrorMessage				 =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			rfvIS_OTHER_THAN_INSURED.ErrorMessage		 =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			csvYEARS_INSU_WOL.ErrorMessage               =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("616");

			rfvDRIVER_SUS_REVOKED.ErrorMessage			      = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			rfvPHY_MENTL_CHALLENGED.ErrorMessage			  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			rfvANY_FINANCIAL_RESPONSIBILITY.ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("129");
			csvREMARKS.ErrorMessage                           = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("440");  
			rfvMULTI_POLICY_DISC_APPLIED.ErrorMessage         = ClsMessages.GetMessage(base.ScreenId,"129");  
			rfvAGENCY_VEH_INSPECTED_MC_DESC.ErrorMessage= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416") ;
			rfvANY_FINANCIAL_RESPONSIBILITY_MC_DESC.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("571") ;
			rfvANY_NON_OWNED_VEH_MC_DESC.ErrorMessage=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416") ;
			rfvANY_OTH_INSU_COMP_MC_DESC.ErrorMessage=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416") ;
			rfvCOVERAGE_DECLINED_MC_DESC.ErrorMessage=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416") ;
			rfvDRIVER_SUS_REVOKED_MC_DESC.ErrorMessage=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416") ;
			rfvEXISTING_DMG_MC_DESC.ErrorMessage= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416") ;
			rfvINS_AGENCY_TRANSFER_MC_DESC.ErrorMessage=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416") ;
			rfvIS_COMMERCIAL_USE_DESC.ErrorMessage=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416") ;
			rfvIS_CONVICTED_ACCIDENT_DESC.ErrorMessage=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416") ;
			rfvIS_CONVICTED_CARELESS_DRIVE_DESC.ErrorMessage= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416") ;
			rfvIS_COST_OVER_DEFINED_LIMIT_DESC.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416") ;
			rfvIS_EXTENDED_FORKS_DESC.ErrorMessage=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416") ;
			rfvIS_LICENSED_FOR_ROAD_DESC.ErrorMessage=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416") ;
			rfvIS_MODIFIED_INCREASE_SPEED_DESC.ErrorMessage=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416") ;
			rfvIS_MODIFIED_KIT_DESC.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416") ;
			rfvIS_MORE_WHEELS_DESC.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416") ;
			rfvIS_TAKEN_OUT_DESC.ErrorMessage=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416") ;
			rfvMULTI_POLICY_DISC_APPLIED_MC_DESC.ErrorMessage=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416") ;
			rfvIS_USEDFOR_RACING_DESC.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416") ;
			rfvPHY_MENTL_CHALLENGED_MC_DESC.ErrorMessage= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416") ;
			rfvSALVAGE_TITLE_MC_DESC.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("416") ;			
			revDOB.ValidationExpression=aRegExpDate;		
			revDOB.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("179");
			rfvDOB.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("482");
			rfvFULL_NAME.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("34");
			csvDOB.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("481");	
			
			rfvAGENCY_VEH_INSPECTED_MC_DESC.ErrorMessage= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("574") ;
			rfvANY_FINANCIAL_RESPONSIBILITY_MC_DESC.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("571") ;
			rfvANY_NON_OWNED_VEH_MC_DESC.ErrorMessage=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("567") ;
			rfvANY_OTH_INSU_COMP_MC_DESC.ErrorMessage=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("569") ;
			rfvCOVERAGE_DECLINED_MC_DESC.ErrorMessage=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("573") ;
			rfvDRIVER_SUS_REVOKED_MC_DESC.ErrorMessage=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("570") ;
			rfvEXISTING_DMG_MC_DESC.ErrorMessage= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("568") ;
			rfvINS_AGENCY_TRANSFER_MC_DESC.ErrorMessage=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("572") ;
			rfvIS_COMMERCIAL_USE_DESC.ErrorMessage=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("576") ;
			rfvIS_CONVICTED_ACCIDENT_DESC.ErrorMessage=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("586") ;
			rfvIS_CONVICTED_CARELESS_DRIVE_DESC.ErrorMessage= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("585") ;
			rfvIS_COST_OVER_DEFINED_LIMIT_DESC.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("578") ;
			rfvIS_EXTENDED_FORKS_DESC.ErrorMessage=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("580") ;
			rfvIS_LICENSED_FOR_ROAD_DESC.ErrorMessage=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("581") ;
			rfvIS_MODIFIED_INCREASE_SPEED_DESC.ErrorMessage=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("582") ;
			rfvIS_MODIFIED_KIT_DESC.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("583") ;
			rfvIS_MORE_WHEELS_DESC.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("579") ;
			rfvIS_TAKEN_OUT_DESC.ErrorMessage=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("584") ;
			rfvMULTI_POLICY_DISC_APPLIED_MC_DESC.ErrorMessage=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("587") ;
			rfvIS_USEDFOR_RACING_DESC.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("577") ;
			rfvPHY_MENTL_CHALLENGED_MC_DESC.ErrorMessage= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("588") ;
			rfvSALVAGE_TITLE_MC_DESC.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("575") ;		
			rngYEARS_INSU_WOL.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
			rngYEARS_INSU.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");

			rfvANY_PRIOR_LOSSES.ErrorMessage             =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("843");
			rfvANY_PRIOR_LOSSES_DESC.ErrorMessage        =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("844");
			rfvAPPLY_PERS_UMB_POL_DESC.ErrorMessage      =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("849");
			
		
		

			//added By shafi
			revYEARS_INSU.ValidationExpression      =     aRegExpInteger; 
			revYEARS_INSU_WOL.ValidationExpression  =     aRegExpInteger;
			csvYEARS_INSU_WOL.ErrorMessage          =     Cms.CmsWeb.ClsMessages.FetchGeneralMessage("616");
			revYEARS_INSU.ErrorMessage              =     Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163"); 
			revYEARS_INSU_WOL.ErrorMessage          =     Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
		}
		#endregion    
		#region Page Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="241";					
			
			hlkDRIVER_DOB.Attributes.Add("onClick","fPopCalendar(document.APP_AUTO_GEN_INFO.txtDATE_OF_BIRTH, document.APP_AUTO_GEN_INFO.txtDATE_OF_BIRTH)");	
			
			cmbIS_OTHER_THAN_INSURED.Attributes.Add("onChange","javascript:ChangeInsuredListed();");
			//btnReset.Attributes.Add("onclick","javascript:ResetForm('POL_AUTO_GEN_INFO');ResetControls();return false;");
			btnReset.Attributes.Add("onclick","javascript:ResetForm('APP_AUTO_GEN_INFO');ResetControls();return false;");
			
			/* Commented by Charles on 2-Jul-09 for Itrack issue 6012
			 
			//Added by Charles on 24-Jun-2009 for Itrack 6003
			btnSave.Attributes.Add("onclick","javascript:alert('Transfer/Renewal discount will be readjusted, please verify again at driver screen.');");
			*/

			if((GetCustomerID()!="" && GetCustomerID()!="0") && (GetPolicyID()!="" && GetPolicyID()!="0") && (GetPolicyVersionID()!="" && GetPolicyVersionID()!="0"))
			{
				trMessage.Attributes.Add("style","display:inline");
				messageID.Attributes.Add("style","display:none");  
				cltClientTop.CustomerID = int.Parse(GetCustomerID());
				cltClientTop.PolicyID = int.Parse(GetPolicyID());
				cltClientTop.PolicyVersionID = int.Parse(GetPolicyVersionID());        
				cltClientTop.ShowHeaderBand ="Policy";
				cltClientTop.Visible = true;        
			}
			else
			{
				trMessage.Attributes.Add("style","display:none");  
				messageID.Attributes.Add("style","display:inline");
				capMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("109");      
				capMessage.Visible=true; 
			}
			lblMessage.Visible = false;
			SetErrorMessages();
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;            
			btnSave.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.Motorcycle.PolicyMotorcycleGeneralInformationIframe",System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				if(GetPolicyID()!="" && GetPolicyVersionID()!="" && GetCustomerID()!="")
					PopulateHiddenFields();
				//Rule will be defined at Motor Info level
				//SetMotorCycleRule();
				//cmbIS_COST_OVER_DEFINED_LIMIT.Enabled=false;
				GenerateXML();
				SetCaptions();
				PopulateComboBox();
			}
			//Commented by ak
			SetWorkflow();
			
			// Swastika : 26th Apr'06 : GI # 2616
			// Check if state is Indiana/Michigan then hide UQ: "Taken out exceeding 30 days".
			
			int CustId		 = int.Parse(GetCustomerID());
			string PolId	 = GetPolicyID();
			string PolVerId  = GetPolicyVersionID();
			int stateId		 = ClsVehicleInformation.GetStateIdForpolicy(CustId,PolId,PolVerId);
			if (stateId.ToString() == ((int)enumState.Indiana).ToString() || stateId.ToString() == ((int)enumState.Michigan).ToString())
			{
				trIS_TAKEN_OUT.Visible=false;
			} 
			else
			{
				trIS_TAKEN_OUT.Visible=true;
			}
		}//end pageload

		#endregion 
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsPolicyGeneralInfo GetFormValue()	
		{
			//Creating the Model object for holding the New data
			ClsPolicyGeneralInfo objMotorGeneralInfo;			
			objMotorGeneralInfo = new ClsPolicyGeneralInfo();			
			if(cmbIS_OTHER_THAN_INSURED.SelectedValue!=null && cmbIS_OTHER_THAN_INSURED.SelectedValue!="")
				objMotorGeneralInfo.IS_OTHER_THAN_INSURED=	cmbIS_OTHER_THAN_INSURED.SelectedValue;
			if(cmbCURR_RES_TYPE.SelectedValue!=null && cmbCURR_RES_TYPE.SelectedValue!="")
				objMotorGeneralInfo.CURR_RES_TYPE=	cmbCURR_RES_TYPE.SelectedValue;
			if(cmbWhichCycle.SelectedValue!=null && cmbWhichCycle.SelectedValue!="")
				objMotorGeneralInfo.WhichCycle=	cmbWhichCycle.SelectedValue;
			if(txtDrivingLisence.Text.Trim()!="")
				objMotorGeneralInfo.DrivingLisence = txtDrivingLisence.Text.Trim();
			//if(txtCompanyName.Text.Trim()!="")
			//	objMotorGeneralInfo.CompanyName = txtCompanyName.Text.Trim();
			//if(txtPolicyNumber.Text.Trim()!="")
			//objMotorGeneralInfo.PolicyNumber = txtPolicyNumber.Text.Trim();
			if(txtFullName.Text.Trim()!="")
				objMotorGeneralInfo.FullName = txtFullName.Text.Trim();
			if(txtDATE_OF_BIRTH.Text.Trim()!="")
				objMotorGeneralInfo.DATE_OF_BIRTH = Convert.ToDateTime(txtDATE_OF_BIRTH.Text);
			//
			objMotorGeneralInfo.ANY_NON_OWNED_VEH						=	cmbANY_NON_OWNED_VEH.SelectedValue;
			if(cmbANY_NON_OWNED_VEH.SelectedValue == "0")
			{
				txtANY_NON_OWNED_VEH_MC_DESC.Text = "";
			}
			objMotorGeneralInfo.ANY_NON_OWNED_VEH_MC_DESC				=	txtANY_NON_OWNED_VEH_MC_DESC.Text;
			//
			objMotorGeneralInfo.EXISTING_DMG							=	cmbEXISTING_DMG.SelectedValue;
			if(cmbEXISTING_DMG.SelectedValue == "0")
			{
				txtEXISTING_DMG_MC_DESC.Text = "";
			}
			objMotorGeneralInfo.EXISTING_DMG_MC_DESC					=	txtEXISTING_DMG_MC_DESC.Text;
			//
			objMotorGeneralInfo.ANY_OTH_INSU_COMP						=	cmbANY_OTH_INSU_COMP.SelectedValue;
			if(cmbANY_OTH_INSU_COMP.SelectedValue == "0")
			{
				txtANY_OTH_INSU_COMP_MC_DESC.Text = "";
			}
			objMotorGeneralInfo.ANY_OTH_INSU_COMP_MC_DESC				=	txtANY_OTH_INSU_COMP_MC_DESC.Text;
			//
			objMotorGeneralInfo.DRIVER_SUS_REVOKED						=	cmbDRIVER_SUS_REVOKED.SelectedValue;
			if(cmbDRIVER_SUS_REVOKED.SelectedValue == "0")
			{
				txtDRIVER_SUS_REVOKED_MC_DESC.Text = "";
			}
			objMotorGeneralInfo.DRIVER_SUS_REVOKED_MC_DESC				=	txtDRIVER_SUS_REVOKED_MC_DESC.Text;
			//
			objMotorGeneralInfo.PHY_MENTL_CHALLENGED					=	cmbPHY_MENTL_CHALLENGED.SelectedValue;
			if(cmbPHY_MENTL_CHALLENGED.SelectedValue == "0")
			{
				txtPHY_MENTL_CHALLENGED_MC_DESC.Text ="";
			}
			objMotorGeneralInfo.PHY_MENTL_CHALLENGED_MC_DESC			=	txtPHY_MENTL_CHALLENGED_MC_DESC.Text;
			//
			objMotorGeneralInfo.ANY_FINANCIAL_RESPONSIBILITY			=	cmbANY_FINANCIAL_RESPONSIBILITY.SelectedValue;
			if(cmbANY_FINANCIAL_RESPONSIBILITY.SelectedValue == "0")
			{
				txtANY_FINANCIAL_RESPONSIBILITY_MC_DESC.Text = "";
			}
			objMotorGeneralInfo.ANY_FINANCIAL_RESPONSIBILITY_MC_DESC	=	txtANY_FINANCIAL_RESPONSIBILITY_MC_DESC.Text;
			//
			objMotorGeneralInfo.INS_AGENCY_TRANSFER						=	cmbINS_AGENCY_TRANSFER.SelectedValue;
			if(cmbINS_AGENCY_TRANSFER.SelectedValue == "0")
			{
				txtINS_AGENCY_TRANSFER_MC_DESC.Text ="";
			}
			objMotorGeneralInfo.INS_AGENCY_TRANSFER_MC_DESC				=	txtINS_AGENCY_TRANSFER_MC_DESC.Text;
			//
			objMotorGeneralInfo.COVERAGE_DECLINED						=	cmbCOVERAGE_DECLINED.SelectedValue;
			if(cmbCOVERAGE_DECLINED.SelectedValue == "0")
			{
				txtCOVERAGE_DECLINED_MC_DESC.Text = "";
			}
			objMotorGeneralInfo.COVERAGE_DECLINED_MC_DESC				=	txtCOVERAGE_DECLINED_MC_DESC.Text;
			//
			objMotorGeneralInfo.AGENCY_VEH_INSPECTED					=	cmbAGENCY_VEH_INSPECTED.SelectedValue;
			if(cmbAGENCY_VEH_INSPECTED.SelectedValue == "0")
			{
				txtAGENCY_VEH_INSPECTED_MC_DESC.Text = "";
			}
			objMotorGeneralInfo.AGENCY_VEH_INSPECTED_MC_DESC			=	txtAGENCY_VEH_INSPECTED_MC_DESC.Text;
			//           
			objMotorGeneralInfo.SALVAGE_TITLE							=	cmbSALVAGE_TITLE.SelectedValue;
			if(cmbSALVAGE_TITLE.SelectedValue == "0")
			{
				txtSALVAGE_TITLE_MC_DESC.Text = "";
			}
			objMotorGeneralInfo.SALVAGE_TITLE_MC_DESC					=	txtSALVAGE_TITLE_MC_DESC.Text;
			//
			objMotorGeneralInfo.IS_COMMERCIAL_USE						=	cmbIS_COMMERCIAL_USE.SelectedValue;
			if(cmbIS_COMMERCIAL_USE.SelectedValue == "0")
			{
				txtIS_COMMERCIAL_USE_DESC.Text ="";
			}
			objMotorGeneralInfo.IS_COMMERCIAL_USE_DESC					=	txtIS_COMMERCIAL_USE_DESC.Text;           
			objMotorGeneralInfo.IS_USEDFOR_RACING						=	cmbIS_USEDFOR_RACING.SelectedValue;
			if(cmbIS_USEDFOR_RACING.SelectedValue == "0")
			{
				txtIS_USEDFOR_RACING_DESC.Text = "";
			}
			objMotorGeneralInfo.IS_USEDFOR_RACING_DESC					=	txtIS_USEDFOR_RACING_DESC.Text;
			//Value for IS_COST_OVER_DEFINED_LIMIT will be fetched from hidden variable as the combo-box
			//is disabled and inaccessible at server side			
			objMotorGeneralInfo.IS_COST_OVER_DEFINED_LIMIT				=	cmbIS_COST_OVER_DEFINED_LIMIT.SelectedValue;
			//objMotorGeneralInfo.IS_COST_OVER_DEFINED_LIMIT				=	hidIS_COST_OVER_DEFINED_LIMIT.Value;			
			if(objMotorGeneralInfo.IS_COST_OVER_DEFINED_LIMIT == "0")
			{
				txtIS_COST_OVER_DEFINED_LIMIT_DESC.Text ="";
			}
			else
				objMotorGeneralInfo.IS_COST_OVER_DEFINED_LIMIT_DESC			=	txtIS_COST_OVER_DEFINED_LIMIT_DESC.Text;
			//
			objMotorGeneralInfo.IS_MORE_WHEELS							=	cmbIS_MORE_WHEELS.SelectedValue;
			if(cmbIS_MORE_WHEELS.SelectedValue == "0")
			{
				txtIS_MORE_WHEELS_DESC.Text = "";
			}
			objMotorGeneralInfo.IS_MORE_WHEELS_DESC						=	txtIS_MORE_WHEELS_DESC.Text;
			//
			objMotorGeneralInfo.IS_EXTENDED_FORKS						=	cmbIS_EXTENDED_FORKS.SelectedValue;
			if(cmbIS_EXTENDED_FORKS.SelectedValue == "0")
			{
				txtIS_EXTENDED_FORKS_DESC.Text ="";
			}
			objMotorGeneralInfo.IS_EXTENDED_FORKS_DESC					=	txtIS_EXTENDED_FORKS_DESC.Text;
			//
			objMotorGeneralInfo.IS_LICENSED_FOR_ROAD					=	cmbIS_LICENSED_FOR_ROAD.SelectedValue;
			if(cmbIS_LICENSED_FOR_ROAD.SelectedValue == "0")
			{
				txtIS_LICENSED_FOR_ROAD_DESC.Text ="";
			}
			objMotorGeneralInfo.IS_LICENSED_FOR_ROAD_DESC				=	txtIS_LICENSED_FOR_ROAD_DESC.Text;
			//
			objMotorGeneralInfo.IS_MODIFIED_INCREASE_SPEED				=	cmbIS_MODIFIED_INCREASE_SPEED.SelectedValue;
			if(cmbIS_MODIFIED_INCREASE_SPEED.SelectedValue == "0")
			{
				txtIS_MODIFIED_INCREASE_SPEED_DESC.Text = "";
			}
			objMotorGeneralInfo.IS_MODIFIED_INCREASE_SPEED_DESC			=	txtIS_MODIFIED_INCREASE_SPEED_DESC.Text;
			//
			objMotorGeneralInfo.IS_MODIFIED_KIT							=	cmbIS_MODIFIED_KIT.SelectedValue;
			if(cmbIS_MODIFIED_KIT.SelectedValue == "0")
			{
				txtIS_MODIFIED_KIT_DESC.Text ="";
			}
			objMotorGeneralInfo.IS_MODIFIED_KIT_DESC					=	txtIS_MODIFIED_KIT_DESC.Text;
			//
			objMotorGeneralInfo.IS_TAKEN_OUT							=	cmbIS_TAKEN_OUT.SelectedValue;
			if(cmbIS_TAKEN_OUT.SelectedValue == "0")
			{
				txtIS_TAKEN_OUT_DESC.Text ="";
			}
			objMotorGeneralInfo.IS_TAKEN_OUT_DESC						=	txtIS_TAKEN_OUT_DESC.Text;
			//
			objMotorGeneralInfo.IS_CONVICTED_CARELESS_DRIVE				=	cmbIS_CONVICTED_CARELESS_DRIVE.SelectedValue;
			if(cmbIS_CONVICTED_CARELESS_DRIVE.SelectedValue == "0")
			{
				txtIS_CONVICTED_CARELESS_DRIVE_DESC.Text ="";
			}
			objMotorGeneralInfo.IS_CONVICTED_CARELESS_DRIVE_DESC		=	txtIS_CONVICTED_CARELESS_DRIVE_DESC.Text;
			//
			objMotorGeneralInfo.IS_CONVICTED_ACCIDENT					=	cmbIS_CONVICTED_ACCIDENT.SelectedValue;
			if(cmbIS_CONVICTED_ACCIDENT.SelectedValue == "0")
			{
				txtIS_CONVICTED_ACCIDENT_DESC.Text ="";
			}
			objMotorGeneralInfo.IS_CONVICTED_ACCIDENT_DESC				=	txtIS_CONVICTED_ACCIDENT_DESC.Text;
			//
			objMotorGeneralInfo.MULTI_POLICY_DISC_APPLIED 				=	cmbMULTI_POLICY_DISC_APPLIED.SelectedValue;
			if(cmbMULTI_POLICY_DISC_APPLIED.SelectedValue == "0")
			{
				txtMULTI_POLICY_DISC_APPLIED_MC_DESC.Text ="";
			}
			if(txtYEARS_INSU.Text.Trim()!="")
			    objMotorGeneralInfo.YEARS_INSU              = int.Parse(txtYEARS_INSU .Text.ToString().Trim());
			else
               objMotorGeneralInfo.YEARS_INSU               = 0;
			if(txtYEARS_INSU_WOL.Text.Trim()!="")
			  objMotorGeneralInfo.YEARS_INSU_WOL            = int.Parse(txtYEARS_INSU_WOL.Text.ToString().Trim());
            else
				objMotorGeneralInfo.YEARS_INSU_WOL          = 0;

			if(cmbANY_PRIOR_LOSSES.SelectedItem!=null && cmbANY_PRIOR_LOSSES.SelectedItem.Value!="")
			{
				objMotorGeneralInfo.ANY_PRIOR_LOSSES = cmbANY_PRIOR_LOSSES.SelectedItem.Value;
			}
			if(cmbAPPLY_PERS_UMB_POL.SelectedItem!=null && cmbAPPLY_PERS_UMB_POL.SelectedItem.Value!="")
			{
				objMotorGeneralInfo.APPLY_PERS_UMB_POL = int.Parse(cmbAPPLY_PERS_UMB_POL.SelectedItem.Value);
			}
			if(objMotorGeneralInfo.ANY_PRIOR_LOSSES == "1" && txtANY_PRIOR_LOSSES_DESC.Text.Trim()!="")
			{
				objMotorGeneralInfo.ANY_PRIOR_LOSSES_DESC = txtANY_PRIOR_LOSSES_DESC.Text.Trim();
			}
			if(objMotorGeneralInfo.APPLY_PERS_UMB_POL == 1 && txtAPPLY_PERS_UMB_POL_DESC.Text.Trim()!="")
			{
				objMotorGeneralInfo.APPLY_PERS_UMB_POL_DESC = txtAPPLY_PERS_UMB_POL_DESC.Text.Trim();
			}

			

			objMotorGeneralInfo.MULTI_POLICY_DISC_APPLIED_MC_DESC		=	txtMULTI_POLICY_DISC_APPLIED_MC_DESC.Text;            
			objMotorGeneralInfo.REMARKS									=	txtREMARKS.Text;		
			objMotorGeneralInfo.POLICY_ID								=	hidPolicyID.Value			==""?0:int.Parse(hidPolicyID.Value); 
			objMotorGeneralInfo.POLICY_VERSION_ID						=	hidPolicyVersionID.Value	==""?0:int.Parse(hidPolicyVersionID.Value); 
			objMotorGeneralInfo.CUSTOMER_ID								=	hidCUSTOMER_ID.Value	==""?0:int.Parse(hidCUSTOMER_ID.Value); 
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			oldXML		= hidOldData.Value;
            if(hidOldData.Value=="")
                strRowId = "NEW";
            else
			    strRowId = hidCUSTOMER_ID.Value;
			//Returning the model object
			return objMotorGeneralInfo;
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
				objPPGeneralInformation = new  ClsPPGeneralInformation();
				//Retreiving the form values into model class object
				ClsPolicyGeneralInfo objMotorGeneralInfo = GetFormValue();
				
				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objMotorGeneralInfo.CREATED_BY = int.Parse(GetUserId());
					objMotorGeneralInfo.CREATED_DATETIME = DateTime.Now;
					objMotorGeneralInfo.IS_ACTIVE           = "Y";
					//Calling the add method of business layer class
					intRetVal = objPPGeneralInformation.AddPolicyMotorCycleGeneralInformation(objMotorGeneralInfo);
					if(intRetVal>0)
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						GenerateXML();
						hidCUSTOMER_ID.Value=objMotorGeneralInfo.CUSTOMER_ID.ToString() ;
						strRowId=hidCUSTOMER_ID.Value ;
						SetWorkflow();	//Setting the workflow

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
					ClsPolicyGeneralInfo objOldMotorGeneralInfo;
					objOldMotorGeneralInfo = new ClsPolicyGeneralInfo();
					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldMotorGeneralInfo,hidOldData.Value);
					//Setting those values into the Model object which are not in the page           
					objMotorGeneralInfo.MODIFIED_BY = int.Parse(GetUserId());
					objMotorGeneralInfo.LAST_UPDATED_DATETIME = DateTime.Now;           
					//Updating the record using business layer class object
					intRetVal	= objPPGeneralInformation.UpdatePolicyMotorCycleGenInformation(objOldMotorGeneralInfo,objMotorGeneralInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GenerateXML();
						SetWorkflow();	//Setting the workflow

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
		#region 
		/// <summary>
		/// populating hidden fields
		/// </summary>
		private void PopulateHiddenFields()
		{
			hidCUSTOMER_ID.Value    = GetCustomerID();
			hidPolicyID.Value         = GetPolicyID();
			hidPolicyVersionID.Value = GetPolicyVersionID();            
		}

		/*private void SetMotorCycleRule()
		{
			ClsPPGeneralInformation objMotorCycle=new ClsPPGeneralInformation(); 
			int intResult =objMotorCycle.CheckForAmountPolicy(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value));
			if( intResult > 0 )
			{   
				cmbIS_COST_OVER_DEFINED_LIMIT.SelectedIndex=1;					
				hidAmount.Value="1";

			}
			else
			{
				cmbIS_COST_OVER_DEFINED_LIMIT.SelectedIndex =0;
				hidAmount.Value="0";
			}
			cmbIS_COST_OVER_DEFINED_LIMIT.Enabled=false;
		}*/
		private void PopulateComboBox()
		{
			IList objIlist = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbANY_NON_OWNED_VEH.DataSource = objIlist;
			cmbANY_NON_OWNED_VEH.DataTextField="LookupDesc"; 
			cmbANY_NON_OWNED_VEH.DataValueField="LookupCode";
			cmbANY_NON_OWNED_VEH.DataBind();
			cmbANY_NON_OWNED_VEH.Items.Insert(0,"");
			cmbEXISTING_DMG.DataSource =objIlist;
			cmbEXISTING_DMG.DataTextField="LookupDesc"; 
			cmbEXISTING_DMG.DataValueField="LookupCode";
			cmbEXISTING_DMG.DataBind();
			cmbEXISTING_DMG.Items.Insert(0,"");
			cmbANY_OTH_INSU_COMP.DataSource = objIlist;
			cmbANY_OTH_INSU_COMP.DataTextField="LookupDesc"; 
			cmbANY_OTH_INSU_COMP.DataValueField="LookupCode";
			cmbANY_OTH_INSU_COMP.DataBind();
			cmbANY_OTH_INSU_COMP.Items.Insert(0,"");
			cmbDRIVER_SUS_REVOKED.DataSource = objIlist;
			cmbDRIVER_SUS_REVOKED.DataTextField="LookupDesc"; 
			cmbDRIVER_SUS_REVOKED.DataValueField="LookupCode";
			cmbDRIVER_SUS_REVOKED.DataBind();
			cmbDRIVER_SUS_REVOKED.Items.Insert(0,"");
			cmbPHY_MENTL_CHALLENGED.DataSource = objIlist;
			cmbPHY_MENTL_CHALLENGED.DataTextField="LookupDesc"; 
			cmbPHY_MENTL_CHALLENGED.DataValueField="LookupCode";
			cmbPHY_MENTL_CHALLENGED.DataBind();
			cmbPHY_MENTL_CHALLENGED.Items.Insert(0,"");
			cmbANY_FINANCIAL_RESPONSIBILITY.DataSource = objIlist;
			cmbANY_FINANCIAL_RESPONSIBILITY.DataTextField="LookupDesc"; 
			cmbANY_FINANCIAL_RESPONSIBILITY.DataValueField="LookupCode";
			cmbANY_FINANCIAL_RESPONSIBILITY.DataBind();
			cmbANY_FINANCIAL_RESPONSIBILITY.Items.Insert(0,"");
			cmbINS_AGENCY_TRANSFER.DataSource = objIlist;
			cmbINS_AGENCY_TRANSFER.DataTextField="LookupDesc"; 
			cmbINS_AGENCY_TRANSFER.DataValueField="LookupCode";
			cmbINS_AGENCY_TRANSFER.DataBind();
			cmbINS_AGENCY_TRANSFER.Items.Insert(0,"");
			cmbCOVERAGE_DECLINED.DataSource =objIlist;
			cmbCOVERAGE_DECLINED.DataTextField="LookupDesc"; 
			cmbCOVERAGE_DECLINED.DataValueField="LookupCode";
			cmbCOVERAGE_DECLINED.DataBind();
			cmbCOVERAGE_DECLINED.Items.Insert(0,"");
			cmbAGENCY_VEH_INSPECTED.DataSource = objIlist;
			cmbAGENCY_VEH_INSPECTED.DataTextField="LookupDesc"; 
			cmbAGENCY_VEH_INSPECTED.DataValueField="LookupCode";
			cmbAGENCY_VEH_INSPECTED.DataBind();
			cmbAGENCY_VEH_INSPECTED.Items.Insert(0,"");
			cmbSALVAGE_TITLE.DataSource =objIlist;
			cmbSALVAGE_TITLE.DataTextField="LookupDesc"; 
			cmbSALVAGE_TITLE.DataValueField="LookupCode";
			cmbSALVAGE_TITLE.DataBind();
			cmbSALVAGE_TITLE.Items.Insert(0,"");
			cmbIS_COMMERCIAL_USE.DataSource = objIlist;
			cmbIS_COMMERCIAL_USE.DataTextField="LookupDesc"; 
			cmbIS_COMMERCIAL_USE.DataValueField="LookupCode";
			cmbIS_COMMERCIAL_USE.DataBind();
			cmbIS_COMMERCIAL_USE.Items.Insert(0,"");
			cmbIS_USEDFOR_RACING.DataSource = objIlist;
			cmbIS_USEDFOR_RACING.DataTextField="LookupDesc"; 
			cmbIS_USEDFOR_RACING.DataValueField="LookupCode";
			cmbIS_USEDFOR_RACING.DataBind();
			cmbIS_USEDFOR_RACING.Items.Insert(0,"");
			cmbIS_COST_OVER_DEFINED_LIMIT.DataSource =objIlist;
			cmbIS_COST_OVER_DEFINED_LIMIT.DataTextField="LookupDesc"; 
			cmbIS_COST_OVER_DEFINED_LIMIT.DataValueField="LookupCode";
			cmbIS_COST_OVER_DEFINED_LIMIT.DataBind();
			//cmbIS_COST_OVER_DEFINED_LIMIT.Items.Insert(0,"");
			cmbIS_MORE_WHEELS.DataSource =objIlist;
			cmbIS_MORE_WHEELS.DataTextField="LookupDesc"; 
			cmbIS_MORE_WHEELS.DataValueField="LookupCode";
			cmbIS_MORE_WHEELS.DataBind();
			cmbIS_MORE_WHEELS.Items.Insert(0,"");
			cmbIS_EXTENDED_FORKS.DataSource = objIlist;
			cmbIS_EXTENDED_FORKS.DataTextField="LookupDesc"; 
			cmbIS_EXTENDED_FORKS.DataValueField="LookupCode";
			cmbIS_EXTENDED_FORKS.DataBind();
			cmbIS_EXTENDED_FORKS.Items.Insert(0,"");			
			cmbIS_LICENSED_FOR_ROAD.DataSource = objIlist;
			cmbIS_LICENSED_FOR_ROAD.DataTextField="LookupDesc"; 
			cmbIS_LICENSED_FOR_ROAD.DataValueField="LookupCode";
			cmbIS_LICENSED_FOR_ROAD.DataBind();
			cmbIS_LICENSED_FOR_ROAD.Items.Insert(0,"");
			cmbIS_MODIFIED_INCREASE_SPEED.DataSource = objIlist;
			cmbIS_MODIFIED_INCREASE_SPEED.DataTextField="LookupDesc"; 
			cmbIS_MODIFIED_INCREASE_SPEED.DataValueField="LookupCode";
			cmbIS_MODIFIED_INCREASE_SPEED.DataBind();
			cmbIS_MODIFIED_INCREASE_SPEED.Items.Insert(0,"");
			cmbIS_MODIFIED_KIT.DataSource = objIlist;
			cmbIS_MODIFIED_KIT.DataTextField="LookupDesc"; 
			cmbIS_MODIFIED_KIT.DataValueField="LookupCode";
			cmbIS_MODIFIED_KIT.DataBind();
			cmbIS_MODIFIED_KIT.Items.Insert(0,"");			
			cmbIS_TAKEN_OUT.DataSource = objIlist;
			cmbIS_TAKEN_OUT.DataTextField="LookupDesc"; 
			cmbIS_TAKEN_OUT.DataValueField="LookupCode";
			cmbIS_TAKEN_OUT.DataBind();
			cmbIS_TAKEN_OUT.Items.Insert(0,"");
			cmbIS_CONVICTED_CARELESS_DRIVE.DataSource = objIlist;
			cmbIS_CONVICTED_CARELESS_DRIVE.DataTextField="LookupDesc"; 
			cmbIS_CONVICTED_CARELESS_DRIVE.DataValueField="LookupCode";
			cmbIS_CONVICTED_CARELESS_DRIVE.DataBind();
			cmbIS_CONVICTED_CARELESS_DRIVE.Items.Insert(0,"");
			cmbIS_CONVICTED_ACCIDENT.DataSource = objIlist;
			cmbIS_CONVICTED_ACCIDENT.DataTextField="LookupDesc"; 
			cmbIS_CONVICTED_ACCIDENT.DataValueField="LookupCode";
			cmbIS_CONVICTED_ACCIDENT.DataBind();
			cmbIS_CONVICTED_ACCIDENT.Items.Insert(0,"");
			cmbMULTI_POLICY_DISC_APPLIED.DataSource = objIlist;
			cmbMULTI_POLICY_DISC_APPLIED.DataTextField="LookupDesc"; 
			cmbMULTI_POLICY_DISC_APPLIED.DataValueField="LookupCode";
			cmbMULTI_POLICY_DISC_APPLIED.DataBind();
			cmbMULTI_POLICY_DISC_APPLIED.Items.Insert(0,"");
			cmbIS_OTHER_THAN_INSURED.DataSource = objIlist;
			cmbIS_OTHER_THAN_INSURED.DataTextField="LookupDesc"; 
			cmbIS_OTHER_THAN_INSURED.DataValueField="LookupCode";
			cmbIS_OTHER_THAN_INSURED.DataBind();
			cmbIS_OTHER_THAN_INSURED.Items.Insert(0,"");

			cmbANY_PRIOR_LOSSES.DataSource = objIlist;
			cmbANY_PRIOR_LOSSES.DataTextField="LookupDesc"; 
			cmbANY_PRIOR_LOSSES.DataValueField="LookupCode";
			cmbANY_PRIOR_LOSSES.DataBind();
			cmbANY_PRIOR_LOSSES.Items.Insert(0,"");

			cmbAPPLY_PERS_UMB_POL.DataSource = objIlist;
			cmbAPPLY_PERS_UMB_POL.DataTextField="LookupDesc"; 
			cmbAPPLY_PERS_UMB_POL.DataValueField="LookupCode";
			cmbAPPLY_PERS_UMB_POL.DataBind();

			// Added by Mohit on 22/09/2005.
			// code for populating drop down with motor cycles for customer,application & application version.			  
			DataTable dt=ClsPPGeneralInformation.GetPolicyMotorCycle(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value));
			cmbWhichCycle.DataSource = dt.DefaultView;
			cmbWhichCycle.DataTextField	= "VEHICLE_DESC";
			cmbWhichCycle.DataValueField	= "VEHICLE_ID";
			cmbWhichCycle.DataBind();
			cmbWhichCycle.Items.Insert(0,"");
			cmbWhichCycle.Items.Insert(1,new ListItem("None","0"));
		}
		private void SetCaptions()
		{
			capANY_NON_OWNED_VEH.Text						=	 objResourceMgr.GetString("cmbANY_NON_OWNED_VEH");
			capANY_NON_OWNED_VEH_MC_DESC.Text					=    objResourceMgr.GetString("txtANY_NON_OWNED_VEH_MC_DESC");
			capEXISTING_DMG.Text						    =	 objResourceMgr.GetString("cmbEXISTING_DMG");
			capEXISTING_DMG_MC_DESC.Text					    =	 objResourceMgr.GetString("txtEXISTING_DMG_MC_DESC");
			capANY_OTH_INSU_COMP.Text						=	 objResourceMgr.GetString("cmbANY_OTH_INSU_COMP");
			capANY_OTH_INSU_COMP_MC_DESC.Text					=	 objResourceMgr.GetString("txtANY_OTH_INSU_COMP_MC_DESC");
			capDRIVER_SUS_REVOKED.Text						=	 objResourceMgr.GetString("cmbDRIVER_SUS_REVOKED");
			capDRIVER_SUS_REVOKED_MC_DESC.Text					=    objResourceMgr.GetString("txtDRIVER_SUS_REVOKED_MC_DESC");
			capPHY_MENTL_CHALLENGED.Text					=    objResourceMgr.GetString("cmbPHY_MENTL_CHALLENGED");
			capPHY_MENTL_CHALLENGED_MC_DESC.Text		    =	objResourceMgr.GetString("txtPHY_MENTL_CHALLENGED_MC_DESC");
			capANY_FINANCIAL_RESPONSIBILITY.Text			=		objResourceMgr.GetString("cmbANY_FINANCIAL_RESPONSIBILITY");
			capANY_FINANCIAL_RESPONSIBILITY_MC_DESC.Text	=	objResourceMgr.GetString("txtANY_FINANCIAL_RESPONSIBILITY_MC_DESC");
			capINS_AGENCY_TRANSFER.Text						=		objResourceMgr.GetString("cmbINS_AGENCY_TRANSFER");
			capINS_AGENCY_TRANSFER_MC_DESC.Text				=        objResourceMgr.GetString("txtINS_AGENCY_TRANSFER_MC_DESC");
			capCOVERAGE_DECLINED.Text						=		objResourceMgr.GetString("cmbCOVERAGE_DECLINED");
			capCOVERAGE_DECLINED_MC_DESC.Text				=   objResourceMgr.GetString("txtCOVERAGE_DECLINED_MC_DESC");
			capAGENCY_VEH_INSPECTED.Text						=		objResourceMgr.GetString("cmbAGENCY_VEH_INSPECTED");
			capAGENCY_VEH_INSPECTED_MC_DESC.Text			=		objResourceMgr.GetString("txtAGENCY_VEH_INSPECTED_MC_DESC");
			capSALVAGE_TITLE.Text						=		objResourceMgr.GetString("cmbSALVAGE_TITLE");
			capSALVAGE_TITLE_MC_DESC.Text				=       objResourceMgr.GetString("txtSALVAGE_TITLE_MC_DESC");
			capREMARKS.Text						        =		objResourceMgr.GetString("txtREMARKS");
			capIS_COMMERCIAL_USE.Text						=		objResourceMgr.GetString("cmbIS_COMMERCIAL_USE");
			capIS_COMMERCIAL_USE_DESC.Text					 =		 objResourceMgr.GetString("txtIS_COMMERCIAL_USE_DESC");
			capIS_USEDFOR_RACING.Text						=		objResourceMgr.GetString("cmbIS_USEDFOR_RACING");
			capIS_USEDFOR_RACING_DESC.Text					=       objResourceMgr.GetString("txtIS_USEDFOR_RACING_DESC");
			capIS_COST_OVER_DEFINED_LIMIT.Text				=		objResourceMgr.GetString("cmbIS_COST_OVER_DEFINED_LIMIT");
			capIS_COST_OVER_DEFINED_LIMIT_DESC.Text				=	  objResourceMgr.GetString("txtIS_COST_OVER_DEFINED_LIMIT_DESC");
			capIS_MORE_WHEELS.Text						=		objResourceMgr.GetString("cmbIS_MORE_WHEELS");
			capIS_MORE_WHEELS_DESC.Text					=	   objResourceMgr.GetString("txtIS_MORE_WHEELS_DESC");
			capIS_EXTENDED_FORKS.Text						=		objResourceMgr.GetString("cmbIS_EXTENDED_FORKS");
			capIS_EXTENDED_FORKS_DESC.Text					=	  objResourceMgr.GetString("txtIS_EXTENDED_FORKS_DESC");
			capIS_LICENSED_FOR_ROAD.Text						=		objResourceMgr.GetString("cmbIS_LICENSED_FOR_ROAD");
			capIS_LICENSED_FOR_ROAD_DESC.Text					=		objResourceMgr.GetString("txtIS_LICENSED_FOR_ROAD_DESC");
			capIS_MODIFIED_INCREASE_SPEED.Text						=		objResourceMgr.GetString("cmbIS_MODIFIED_INCREASE_SPEED");
			capIS_MODIFIED_INCREASE_SPEED_DESC.Text					=	objResourceMgr.GetString("txtIS_MODIFIED_INCREASE_SPEED_DESC");
			capIS_MODIFIED_KIT.Text						=		objResourceMgr.GetString("cmbIS_MODIFIED_KIT");
			capIS_MODIFIED_KIT_DESC.Text				=		 objResourceMgr.GetString("txtIS_MODIFIED_KIT_DESC");
			capIS_TAKEN_OUT.Text						=		objResourceMgr.GetString("cmbIS_TAKEN_OUT");
			capIS_TAKEN_OUT_DESC.Text					=		objResourceMgr.GetString("txtIS_TAKEN_OUT_DESC");
			capIS_CONVICTED_CARELESS_DRIVE.Text			=		objResourceMgr.GetString("cmbIS_CONVICTED_CARELESS_DRIVE");
			capIS_CONVICTED_CARELESS_DRIVE_DESC.Text	=	   objResourceMgr.GetString("txtIS_CONVICTED_CARELESS_DRIVE_DESC");
			capIS_CONVICTED_ACCIDENT.Text				=		objResourceMgr.GetString("cmbIS_CONVICTED_ACCIDENT");
			capIS_CONVICTED_ACCIDENT_DESC.Text			=	 objResourceMgr.GetString("txtIS_CONVICTED_ACCIDENT_DESC");
			capMULTI_POLICY_DISC_APPLIED.Text           =      objResourceMgr.GetString("cmbMULTI_POLICY_DISC_APPLIED");
			capMULTI_POLICY_DISC_APPLIED_MC_DESC.Text		=	  objResourceMgr.GetString("txtMULTI_POLICY_DISC_APPLIED_MC_DESC");
			capFULL_NAME.Text                            =objResourceMgr.GetString("txtFullName");
			capDRIV_LIC.Text                              =objResourceMgr.GetString("txtDrivingLisence");
			capDOB.Text                                   =objResourceMgr.GetString("txtDATE_OF_BIRTH");
			capMOTORCYCLE.Text                             =objResourceMgr.GetString("cmbWhichCycle");

			//Added By Shafi 16=01-2006
			capYEARS_INSU.Text                                 =   objResourceMgr.GetString("txtYEARS_INSU");
			capYEARS_INSU_WOL.Text                             =   objResourceMgr.GetString("txtYEARS_INSU_WOL");

			capANY_PRIOR_LOSSES.Text			            =	objResourceMgr.GetString("cmbANY_PRIOR_LOSSES");
			capANY_PRIOR_LOSSES_DESC.Text					=	objResourceMgr.GetString("txtANY_PRIOR_LOSSES_DESC");
			capAPPLY_PERS_UMB_POL.Text						=	objResourceMgr.GetString("cmbAPPLY_PERS_UMB_POL");
			capAPPLY_PERS_UMB_POL_DESC.Text					=	objResourceMgr.GetString("txtAPPLY_PERS_UMB_POL_DESC");

		}
		private void SetWorkflow()
		{
			if(base.ScreenId == "241")
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
		/// <summary>
		/// fetching data based on query string values
		/// </summary>
		private void GenerateXML()
		{
			Cms.BusinessLayer.BlApplication.ClsPPGeneralInformation objPPGeneralInformation=new ClsPPGeneralInformation();  			
			try
			{
				DataSet ds=new DataSet(); 
				ds=objPPGeneralInformation.FetchPolicyMotorGenInfoData(int.Parse(hidPolicyID.Value),int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPolicyVersionID.Value));
                if (ds.Tables[0].Rows.Count > 0)
                    hidOldData.Value = ds.GetXml();
                else
                    hidOldData.Value = "";
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
	}
}
