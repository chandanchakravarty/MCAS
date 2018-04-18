/******************************************************************************************
<Author				: -		Vijay Arora
<Start Date			: -		07-11-2005
<End Date			: -	
<Description		: - 	Policy Add/Edit/Delete Driver Page
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		:		
<Modified By		:
<Purpose			: 

<Modified Date			: - 10/02/2006
<Modified By			: - Shafee
<Purpose				: - Generate Driver Code
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
using Cms.BusinessLayer.BlApplication;
using Cms.CmsWeb;
using System.Xml; 
using Cms.CustomException;
using Cms.CmsWeb.WebControls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Policy;
using Cms.CmsWeb.Controls;
using Cms.Model.Diary; 
using Cms.Model.Application;
using Cms.BusinessLayer.BlCommon;  


namespace Cms.Policies.Aspx
{
	/// <summary>
	/// Show the Add Driver Detail page.
	/// </summary>
	public class AddPolicyDriver : Cms.Policies.policiesbase//System.Web.UI.Page
	{
		
		#region Page controls declaration
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDateDiff;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_FNAME;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_MNAME;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_LNAME;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_CODE;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_SUFFIX;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_ADD1;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_ADD2;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_CITY;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbWAIVER_WORK_LOSS_BENEFITS;
		protected System.Web.UI.WebControls.Label capWAIVER_WORK_LOSS_BENEFITS;
		protected System.Web.UI.WebControls.Label lblViolationPoints;
		protected System.Web.UI.WebControls.Label capViolationPoints;
		protected System.Web.UI.WebControls.Label lblAccidentPoints;
		protected System.Web.UI.WebControls.Label capAccidentPoints;
		protected System.Web.UI.WebControls.DropDownList cmbFULL_TIME_STUDENT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFULL_TIME_STUDENT;
		protected System.Web.UI.WebControls.Label	capPARENTS_INSURANCE;
		protected System.Web.UI.WebControls.DropDownList cmbPARENTS_INSURANCE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPARENTS_INSURANCE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDataValue1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDataValue2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomInfo;

		protected System.Web.UI.WebControls.Label capIN_MILITARY;
		protected System.Web.UI.WebControls.DropDownList cmbIN_MILITARY;
		protected System.Web.UI.WebControls.Label capHAVE_CAR;
		protected System.Web.UI.WebControls.DropDownList cmbHAVE_CAR;
		protected System.Web.UI.WebControls.Label capSTATIONED_IN_US_TERR;
		protected System.Web.UI.WebControls.DropDownList cmbSTATIONED_IN_US_TERR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIN_MILITARY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHAVE_CAR;

		protected System.Web.UI.WebControls.Label capFULL_TIME_STUDENT;
		protected System.Web.UI.WebControls.DropDownList cmbSIGNED_WAIVER_BENEFITS_FORM;
		protected System.Web.UI.WebControls.Label capSIGNED_WAIVER_BENEFITS_FORM;
		protected System.Web.UI.WebControls.DropDownList cmbSUPPORT_DOCUMENT;
		protected System.Web.UI.WebControls.Label capSUPPORT_DOCUMENT;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_ZIP;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_COUNTRY;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_HOME_PHONE;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_BUSINESS_PHONE;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_EXT;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_MOBILE;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_DOB;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_SSN;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_MART_STAT;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_SEX;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_DRIV_LIC;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_LIC_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbRELATIONSHIP;
		protected System.Web.UI.WebControls.TextBox txtDATE_LICENSED;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_DRIV_TYPE;
		
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_OCC_CLASS;
		//protected System.Web.UI.WebControls.TextBox txtDRIVER_DRIVERLOYER_NAME;//Commented by Sibin for Itrack Issue 5060 on 26 Nov 08
		//protected System.Web.UI.WebControls.TextBox txtDRIVER_DRIVERLOYER_ADD;//Commented by Sibin for Itrack Issue 5060 on 26 Nov 08

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidState_id;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidNO_DEPENDENTS; 
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_DOB;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSUPPORT_DOCUMENT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_INCOME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_FNAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_LNAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_COUNTRY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_SEX;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_LIC_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_DRIV_TYPE;
		//ADDED PRAVEEN KUMAR(22-01-2009):ITRACK 5330
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATIONED_IN_US_TERR;
		//END PRAVEEN KUMAR
		//Added by Swastika for Gen Iss # 2366 on 28th Feb'06
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDATE_LICENSED;

		protected System.Web.UI.WebControls.Label	capNO_DEPENDENTS;
		protected System.Web.UI.WebControls.DropDownList cmbNO_DEPENDENTS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNO_DEPENDENTS;

		protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_CODE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_HOME_PHONE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_MOBILE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_BUSINESS_PHONE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_EXT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_DOB;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_SSN;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDATE_LICENSED;
		protected System.Web.UI.WebControls.Image imgZipLookup;
		protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
		protected System.Web.UI.WebControls.CustomValidator csvDRIVER_ZIP;
		protected System.Web.UI.WebControls.Label	capSSN_NO_HID; 
		protected Cms.CmsWeb.Controls.CmsButton btnSSN_NO; 	
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSSN_NO; 


		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.WebControls.ClientTop clientTop;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		public string DriverAge ="";
		public string AppEffcDate = "";
		public string PolEffDate="";
		public int intYearsWithWolverine;//Added by Sibin for Itrack Issue 5428 on 18 Feb 09

		#endregion

		#region Local form variables
		string oldXML;
		public string strAssignXml;
		//public string strWaiverBenefitsLimit = "50";
		//Limit increased
		public string strWaiverBenefitsLimit = "60";
		
		System.Resources.ResourceManager objResourceMgr;

		private string strRowId, strFormSaved;
		protected System.Web.UI.WebControls.Label capDRIVER_FNAME;
		protected System.Web.UI.WebControls.Label capDRIVER_MNAME;
		protected System.Web.UI.WebControls.Label capDRIVER_LNAME;
		protected System.Web.UI.WebControls.Label capDRIVER_CODE;
		protected System.Web.UI.WebControls.Label capDRIVER_SUFFIX;
		protected System.Web.UI.WebControls.Label capDRIVER_ADD1;
		protected System.Web.UI.WebControls.Label capDRIVER_ADD2;
		protected System.Web.UI.WebControls.Label capDRIVER_CITY;
		protected System.Web.UI.WebControls.Label capDRIVER_STATE;
		protected System.Web.UI.WebControls.Label capDRIVER_ZIP;
		protected System.Web.UI.WebControls.Label capDRIVER_COUNTRY;
		protected System.Web.UI.WebControls.Label capDRIVER_HOME_PHONE;
		protected System.Web.UI.WebControls.Label capDRIVER_MOBILE;
		protected System.Web.UI.WebControls.Label capDRIVER_BUSINESS_PHONE;
		protected System.Web.UI.WebControls.Label capDRIVER_EXT;
		protected System.Web.UI.WebControls.Label capDRIVER_DOB;
		protected System.Web.UI.WebControls.Label capDRIVER_SSN;
		protected System.Web.UI.WebControls.Label capDRIVER_MART_STAT;
		protected System.Web.UI.WebControls.Label capDRIVER_SEX;
		protected System.Web.UI.WebControls.Label capDRIVER_DRIV_LIC;
		protected System.Web.UI.WebControls.Label capDRIVER_LIC_STATE;
		protected System.Web.UI.WebControls.Label capDATE_LICENSED;
		protected System.Web.UI.WebControls.Label capDRIVER_DRIV_TYPE;
		protected System.Web.UI.WebControls.Label capDRIVER_OCC_CLASS;
		//protected System.Web.UI.WebControls.Label capDRIVER_DRIVERLOYER_NAME; //Commented by Sibin for Itrack Issue 5060 on 26 Nov 08
		//protected System.Web.UI.WebControls.Label capDRIVER_DRIVERLOYER_ADD; //Commented by Sibin for Itrack Issue 5060 on 26 Nov 08
		protected System.Web.UI.WebControls.Label capDRIVER_INCOME;
		protected System.Web.UI.WebControls.Label capDRIVER_PHYS_MED_IMPAIRE;
		protected System.Web.UI.WebControls.Label capDRIVER_DRINK_VIOLATION;
		protected System.Web.UI.WebControls.Label capDRIVER_LIC_SUSPENDED;
		protected System.Web.UI.WebControls.Label capDRIVER_VOLUNTEER_POLICE_FIRE;
		protected System.Web.UI.WebControls.Label capDRIVER_US_CITIZEN;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDRIVER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDRIVER_DRIV_LIC_SELINDEX;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDRIVER_DRIV_LIC;
		protected System.Web.UI.WebControls.HyperLink hlkOCCURENCE_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkDATE_LICENSED;
		protected System.Web.UI.WebControls.Label Label1;
		protected Cms.CmsWeb.Controls.CmsButton btnPullCustomerAddress;
		protected System.Web.UI.WebControls.CustomValidator csvDRIVER_DOB;
		protected System.Web.UI.WebControls.CustomValidator csvDATE_LICENSED;
		//protected System.Web.UI.WebControls.CustomValidator csvDISC_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_ZIP;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_INFO;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDiscTypeLen;
		protected Cms.CmsWeb.Controls.CmsButton btnCopyDefaultCustomer;
		//protected Cms.CmsWeb.Controls.CmsButton btnCopyAppDrivers;
		protected System.Web.UI.WebControls.Label capRELATIONSHIP;
		protected System.Web.UI.HtmlControls.HtmlTableRow trLicMed;

		
		//Defining the business layer class object
		ClsDriverDetail  objDriverDetail ;
		private const string CALLED_FROM_MOTORCYCLE="MOT";
		private const string CALLED_FROM_APP="APP";
		private const string CALLED_FROM_WATERCRAFT="WAT";
		private const string CALLED_FROM_CLIENT="CLT";
		private const string CALLED_FROM_RENT="RENT";
		private const string CALLED_FROM_UMBRELLA="UMB";

		//protected System.Web.UI.WebControls.CustomValidator csvDATE_EXP_DOB;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnDATE_EXP_DOB;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_PHYS_MED_IMPAIRE;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_DRINK_VIOLATION;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_LIC_SUSPENDED;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_VOLUNTEER_POLICE_FIRE;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist2;		
		protected System.Web.UI.WebControls.Label capDISC_TYPE;    		
		
		protected System.Web.UI.WebControls.Label lblVehicleMsg;		
		protected System.Web.UI.WebControls.Label capDISC_DATE;
		protected System.Web.UI.WebControls.TextBox txtDISC_DATE;
		protected System.Web.UI.WebControls.HyperLink Hyperlink1;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revDISC_DATE;
		//protected System.Web.UI.WebControls.CustomValidator csvDISC_DATE;
		protected System.Web.UI.WebControls.Label capVEHICLE_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVEHICLE_ID;

		protected System.Web.UI.WebControls.Label capPERCENT_DRIVEN;
		protected System.Web.UI.WebControls.HyperLink hlkDiscount_DATE;
		protected System.Web.UI.HtmlControls.HtmlTableRow trVehField;
		protected System.Web.UI.HtmlControls.HtmlTableRow trVehMsg;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_US_CITIZEN;
		protected System.Web.UI.WebControls.Label capDRIVER_STUD_DIST_OVER_HUNDRED;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_STUD_DIST_OVER_HUNDRED;
		protected System.Web.UI.WebControls.Label capSAFE_DRIVER_RENEWAL_DISCOUNT;
		protected System.Web.UI.WebControls.Label capGoodStudent;
		protected System.Web.UI.WebControls.Label capPremierDriver;
		protected System.Web.UI.WebControls.Label capSAFE_DRIVER;
		protected System.Web.UI.WebControls.Label capDRIVER_GOOD_STUDENT;
		protected System.Web.UI.WebControls.Label capDRIVER_PREF_RISK;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.WebControls.RequiredFieldValidator  rfvVEHICLE_DRIVER;
		protected System.Web.UI.WebControls.DropDownList cmbAPP_VEHICLE_PRIN_OCC_ID;
		
		protected System.Web.UI.WebControls.HyperLink hlkCalandarDate2;

		protected System.Web.UI.WebControls.DropDownList cmbVEHICLE_ID;
		public string strCalledFrom = "";

		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_PREF_RISK;     // for Premier Driver
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_GOOD_STUDENT;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_INCOME;
		protected System.Web.UI.WebControls.TextBox DRIVER_VOLUNTEER_POLICE_FIRE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_DRIV_LIC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_DRINK_VIOLATION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_STUD_DIST_OVER_HUNDRED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_VOLUNTEER_POLICE_FIRE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_US_CITIZEN;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppVersionId;
		protected System.Web.UI.WebControls.Label capDRIVER_COST_GAURAD_AUX;
		protected System.Web.UI.WebControls.DropDownList cmbOP_DRIVER_COST_GAURAD_AUX;
		protected System.Web.UI.WebControls.Label capCoastGuard;
		protected System.Web.UI.WebControls.Label capDRIVER_DIESEL_DISCOUNT;
		protected System.Web.UI.WebControls.Label capDriverDiscount;
		protected System.Web.UI.WebControls.Label capHALON_FIRE_DISCOUNT;
		protected System.Web.UI.WebControls.Label capHalonFireDiscount;
		protected System.Web.UI.WebControls.Label capNAVIGATION_DISCOUNT;
		protected System.Web.UI.WebControls.Label capNavigationDiscount;
		protected System.Web.UI.WebControls.Label capSHORE_STATION_CREDIT;
		protected System.Web.UI.WebControls.Label capShoreStationCredit;
		protected System.Web.UI.WebControls.Label capMULTIPLE_DISCOUNT;
		protected System.Web.UI.WebControls.Label capMultipleDiscount;
		protected System.Web.UI.WebControls.Label lblOpVehMsg;
		protected System.Web.UI.WebControls.Label capOP_VEHICLE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbOP_VEHICLE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbOP_APP_VEHICLE_PRIN_OCC_ID;
		protected System.Web.UI.HtmlControls.HtmlTableRow trOperator;
		protected System.Web.UI.HtmlControls.HtmlTableRow trDRIVER_DIESEL_DISCOUNT;
		protected System.Web.UI.HtmlControls.HtmlTableRow trHALON_FIRE_DISCOUNT;
		protected System.Web.UI.HtmlControls.HtmlTableRow trNAVIGATION_DISCOUNT;
		protected System.Web.UI.HtmlControls.HtmlTableRow trSHORE_STATION_CREDIT;
		protected System.Web.UI.HtmlControls.HtmlTableRow trMULTIPLE_DISCOUNT;
		protected System.Web.UI.HtmlControls.HtmlTableRow trOpVehMsg;
		protected System.Web.UI.HtmlControls.HtmlTableRow trOpVehField;  // for Good Student
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnVEHICLE_ID;
		protected System.Web.UI.WebControls.Label capOP_APP_VEHICLE_PRIN_OCC_ID;
		protected System.Web.UI.WebControls.Label capEXT_NON_OWN_COVG_INDIVI;
		protected System.Web.UI.WebControls.DropDownList cmbEXT_NON_OWN_COVG_INDIVI;
		protected System.Web.UI.WebControls.Label capFORM_F95;
		protected System.Web.UI.WebControls.DropDownList cmbFORM_F95;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label lblViolationMsg;
		protected System.Web.UI.HtmlControls.HtmlTableRow trViolationSec;
		protected System.Web.UI.HtmlControls.HtmlTableRow trViolationMsg;
		protected System.Web.UI.HtmlControls.HtmlTableRow trViolationField;
		protected System.Web.UI.WebControls.Table tblAssignedVeh;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSeletedData;  
		protected System.Web.UI.WebControls.DropDownList cmbSAFE_DRIVER_RENEWAL_DISCOUNT; // for Safe Driver Renewal Discount


		protected System.Web.UI.WebControls.Label capVIOLATIONS;
		protected System.Web.UI.WebControls.DropDownList cmbVIOLATIONS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVIOLATIONS;
		protected System.Web.UI.WebControls.Label capMVR_ORDERED;
		protected System.Web.UI.WebControls.DropDownList cmbMVR_ORDERED;
		protected System.Web.UI.WebControls.TextBox txtDATE_ORDERED;
		protected System.Web.UI.WebControls.Label capDATE_ORDERED;
		protected System.Web.UI.WebControls.TextBox txtMVR_CLASS;
		protected System.Web.UI.WebControls.Label capMVR_CLASS;
		protected System.Web.UI.WebControls.TextBox txtMVR_LIC_CLASS;
		protected System.Web.UI.WebControls.Label capMVR_LIC_CLASS;
		protected System.Web.UI.WebControls.TextBox txtMVR_LIC_RESTR;
		protected System.Web.UI.WebControls.Label capMVR_LIC_RESTR;
		protected System.Web.UI.WebControls.TextBox txtMVR_DRIV_LIC_APPL;
		protected System.Web.UI.WebControls.Label capMVR_DRIV_LIC_APPL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDATE_ORDERED;
		protected System.Web.UI.WebControls.CustomValidator csvDATE_ORDERED;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDATE_ORDERED;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_DRIV_LIC;
		protected System.Web.UI.WebControls.Label capMVR_REMARKS;
		protected System.Web.UI.WebControls.TextBox txtMVR_REMARKS;
		protected System.Web.UI.WebControls.CustomValidator csvMVR_REMARKS;
		protected System.Web.UI.WebControls.Label capMVR_STATUS;
		protected System.Web.UI.WebControls.DropDownList cmbMVR_STATUS;

		protected System.Web.UI.WebControls.Label capLOSSREPORT_ORDER;
		protected System.Web.UI.WebControls.DropDownList cmbLOSSREPORT_ORDER;
		protected System.Web.UI.WebControls.Label capLOSSREPORT_DATETIME;
		protected System.Web.UI.WebControls.TextBox txtLOSSREPORT_DATETIME;
		protected System.Web.UI.WebControls.HyperLink hlkLOSSREPORT_DATETIME;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLOSSREPORT_DATETIME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFORM_F95;	
		
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
			rfvDRIVER_FNAME.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"57");
			rfvDRIVER_LNAME.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"58");
			rfvDRIVER_CODE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"38");
			rfvDRIVER_STATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"35");
			rfvDRIVER_COUNTRY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"33");
			rfvDRIVER_SEX.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"189");
			rfvDRIVER_LIC_STATE.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"111");
			rfvDRIVER_DRIV_TYPE.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"112");

			revDRIVER_CODE.ValidationExpression			= aRegExpClientName	;
			revDRIVER_ZIP.ValidationExpression			= aRegExpZip;
			revDRIVER_HOME_PHONE.ValidationExpression	= aRegExpPhone	;
			revDRIVER_MOBILE.ValidationExpression		= aRegExpPhone;
			revDRIVER_BUSINESS_PHONE.ValidationExpression=aRegExpPhone;
			revDRIVER_EXT.ValidationExpression			= aRegExpExtn;
			revDRIVER_DOB.ValidationExpression			= aRegExpDate;
			revDRIVER_SSN.ValidationExpression			= aRegExpSSN;
			revDATE_LICENSED.ValidationExpression		= aRegExpDate;

			revDRIVER_CODE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"64");
			revDRIVER_ZIP.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"24");
			revDRIVER_HOME_PHONE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"14");
			revDRIVER_MOBILE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"16");
			revDRIVER_BUSINESS_PHONE.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"14");
			revDRIVER_EXT.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"25");
			revDRIVER_DOB.ErrorMessage					= "<br>" + Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"179");
			revDATE_ORDERED.ValidationExpression		=	aRegExpDate;
			revDRIVER_SSN.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"130");
			revDATE_LICENSED.ErrorMessage				=  "<br>" + Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"22");

			csvDRIVER_DOB.ErrorMessage				=  "<br>" + Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"198");
			csvDATE_LICENSED.ErrorMessage				=  "<br>" + Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"199");
			//csvDISC_TYPE.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");

			rfvDRIVER_ZIP.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"37");
			//csvDATE_EXP_DOB.ErrorMessage			=	"<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("225");
			spnDATE_EXP_DOB.InnerHtml					=	"<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("225");

			
			rfvVEHICLE_ID.ErrorMessage = ClsMessages.FetchGeneralMessage("562");
			rfvVEHICLE_DRIVER.ErrorMessage = ClsMessages.FetchGeneralMessage("563");

			rfvDRIVER_DRIV_LIC.ErrorMessage=ClsMessages.FetchGeneralMessage("527");
			rfvDRIVER_DRINK_VIOLATION.ErrorMessage=ClsMessages.FetchGeneralMessage("528");
			rfvDRIVER_STUD_DIST_OVER_HUNDRED.ErrorMessage=ClsMessages.FetchGeneralMessage("529");
			rfvDRIVER_VOLUNTEER_POLICE_FIRE.ErrorMessage=ClsMessages.FetchGeneralMessage("530");
			rfvDRIVER_US_CITIZEN.ErrorMessage=ClsMessages.FetchGeneralMessage("531");
			rfvDRIVER_DOB.ErrorMessage              ="<br>" +Cms.CmsWeb.ClsMessages.FetchGeneralMessage("162");
			rfvDRIVER_INCOME.ErrorMessage =ClsMessages.FetchGeneralMessage("618");
			//Added by Swastika for Gen Iss # 2366 on 28th Feb'06
			rfvDATE_LICENSED.ErrorMessage				= "<br>" +Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"710");

			rfvSUPPORT_DOCUMENT.ErrorMessage				= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("840");
			rfvIN_MILITARY.ErrorMessage					=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("841");
			rfvHAVE_CAR.ErrorMessage					=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("842");
			
			revLOSSREPORT_DATETIME.ValidationExpression		=	aRegExpDate;
			rfvPARENTS_INSURANCE.ErrorMessage					=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1025");
			rfvFULL_TIME_STUDENT.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1028");
			
			//ADDED BY PRAVEEN KUMAR(22-01-2009):ITRACK 5330
			rfvSTATIONED_IN_US_TERR.ErrorMessage					=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1037");
			//END PRAVEEN KUMAR
			//			rfvVEHICLE_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			//			rfvVEHICLE_DRIVER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");


		}

		private void SetLicErrorMessage()
		{
			string strOldLicenseState = "";
			if(hidOldData.Value != "")
				strOldLicenseState = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("DRIVER_LIC_STATE",hidOldData.Value);

			if(cmbDRIVER_LIC_STATE.SelectedValue != "")
				strOldLicenseState = cmbDRIVER_LIC_STATE.SelectedValue;

			if(hidDRIVER_DRIV_LIC.Value != "0")
			{
				//				ListItem listitem = cmbDRIVER_LIC_STATE.Items.FindByValue(hidDRIVER_DRIV_LIC.Value);
				//				cmbDRIVER_LIC_STATE.SelectedIndex = cmbDRIVER_LIC_STATE.Items.IndexOf(listitem);
				strOldLicenseState = hidDRIVER_DRIV_LIC.Value;
				//				hidDRIVER_DRIV_LIC.Value = "0";
			}

//			switch(strOldLicenseState)
//			{
//				case "14": revDRIVER_DRIV_LIC.ValidationExpression = aRegExpDrivLicIN;
//					revDRIVER_DRIV_LIC.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("980");
//					revDRIVER_DRIV_LIC.Enabled = true;
//					break;
//				case "22": revDRIVER_DRIV_LIC.ValidationExpression = aRegExpDrivLicMI;
//					revDRIVER_DRIV_LIC.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("981");
//					revDRIVER_DRIV_LIC.Enabled = true;
//					break;
//				case "49": revDRIVER_DRIV_LIC.ValidationExpression = aRegExpDrivLicWI;
//					revDRIVER_DRIV_LIC.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("982");
//					revDRIVER_DRIV_LIC.Enabled = true;
//					break;
//				default: revDRIVER_DRIV_LIC.ValidationExpression = aRegExpDrivLicIN;
//					revDRIVER_DRIV_LIC.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("980");
//					revDRIVER_DRIV_LIC.Enabled = false;
//					break;
//			}
		}

		#endregion

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{		
			
			Ajax.Utility.RegisterTypeForAjax(typeof(AddPolicyDriver)); 

			#region setting screen id
			if(Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"] != "")
			{
				strCalledFrom =	Request.QueryString["CalledFrom"].ToString().ToUpper();

			}
			switch(strCalledFrom.ToUpper())
			{
				case "PPA" :				
					base.ScreenId	=	"228_0";					
					break;
				case "mot" :
				case "MOT" :
					base.ScreenId	=	"237_0";					
					break;
				case "UMB" :				
					base.ScreenId	=	"278_0";					
					break;				
				default :
					base.ScreenId	=	"45_0";					
					break;
			}
			#endregion
			getPolicyEffectiveYear();
			getPolicyYearsWithWolverine();//Added by Sibin for Itrack Issue 5428 on 18 Feb 09
			txtDRIVER_BUSINESS_PHONE.Attributes.Add("onBlur","javascript:return CheckIfPhoneEmpty();");			
			txtDRIVER_DOB.Attributes.Add("onBlur","javascript: CompareExpDateWithDOB();FetchAjaxResponse();return EnableDisableControls();");
			txtDRIVER_DOB.Attributes.Add("onkeypress","javascript:change_ddl();");
			txtDATE_LICENSED.Attributes.Add("onBlur","javascript: CompareExpDateWithDOB();");

			//Added  By Swarup on 11-Dec-2006

			hlkCalandarDate2.Attributes.Add("OnClick","fPopCalendar(document.APP_DRIVER_DETAILS.txtDATE_ORDERED,document.APP_DRIVER_DETAILS.txtDATE_ORDERED)"); 
			hlkLOSSREPORT_DATETIME.Attributes.Add("OnClick","fPopCalendar(document.APP_DRIVER_DETAILS.txtLOSSREPORT_DATETIME,document.APP_DRIVER_DETAILS.txtLOSSREPORT_DATETIME)");  	
            
			//btnActivateDeactivate.Attributes.Add("onclick","javascript:document.APP_DRIVER_DETAILS.reset();"); 
			btnSave.Attributes.Add("onClick","fxnDisableDependents();CheckForDriverType();EnableDisableControls();driverDiscount(1); return SaveClientSide();");
			SetControlsAttributes();

			SetErrorMessages();

			btnReset.CmsButtonClass		=	CmsButtonType.Write;
			btnReset.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;
			
			btnPullCustomerAddress.CmsButtonClass	=	CmsButtonType.Write;
			btnPullCustomerAddress.PermissionString =	gstrSecurityXML;

			btnCopyDefaultCustomer.CmsButtonClass =     CmsButtonType.Write;
			btnCopyDefaultCustomer.PermissionString =	gstrSecurityXML;

			//btnCopyAppDrivers.CmsButtonClass =     CmsButtonType.Write;
			//btnCopyAppDrivers.PermissionString =	gstrSecurityXML;

			btnDelete.CmsButtonClass	=	CmsButtonType.Delete ;
			btnDelete.PermissionString =	gstrSecurityXML;

			
			base.RequiredPullCustAdd(txtDRIVER_ADD1, txtDRIVER_ADD2
				, txtDRIVER_CITY, cmbDRIVER_COUNTRY, cmbDRIVER_STATE
				, txtDRIVER_ZIP, btnPullCustomerAddress);


			


			//Attaching the javascript function on click event
			btnPullCustomerAddress.Attributes.Add("onClick","javascript:PullCustomerAddress("
				+ "document.getElementById('" + txtDRIVER_ADD1.ID + "'),"
				+ "document.getElementById('" + txtDRIVER_ADD2.ID + "'),"
				+ "document.getElementById('" + txtDRIVER_CITY.ID + "'),"
				+ "document.getElementById('" + cmbDRIVER_COUNTRY.ID + "'),"
				+ "document.getElementById('" + cmbDRIVER_STATE.ID + "'),"
				+ "document.getElementById('" + txtDRIVER_ZIP.ID + "')"
				+ ");SetRegisteredState();return false;");

			SetAttribute();
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.AddPolicyDriver" ,System.Reflection.Assembly.GetExecutingAssembly());
			
			if(!Page.IsPostBack)
			{
				
				//GetSessionValues();
				GetQueryString();	
				cmbSTATIONED_IN_US_TERR.Attributes.Add("onChange","javascript:return HideShowAssignedVehicleSection();");
				// Added by Swarup on 30-mar-2007
				imgZipLookup.Attributes.Add("style","cursor:hand");
				base.VerifyAddress(hlkZipLookup, txtDRIVER_ADD1,txtDRIVER_ADD2
					, txtDRIVER_CITY, cmbDRIVER_STATE, txtDRIVER_ZIP);

				//Get The state State	
				int state_id=Cms.BusinessLayer.BlApplication.ClsVehicleInformation.GetStateIdForpolicy(int.Parse(hidCUSTOMER_ID.Value.ToString()),hidPolicyId.Value.ToString(),hidPolicyVersionId.Value.ToString());
				hidState_id.Value =Convert.ToString(state_id);
				//Check for Michigan and auto
				if(hidCalledFrom.Value.ToString().ToUpper() =="PPA" && state_id==22)
				{
					enableValidators();
				}
				else
				{
					disableValidators();
				}				
				GetOldDataXML();
				fxnAssignedVehicle();
				GetDriverCount();
				SetCaptions();
				#region "Loading singleton"
				DataTable dt = Cms.CmsWeb.ClsFetcher.State;
				cmbDRIVER_STATE.DataSource		= dt;
				cmbDRIVER_STATE.DataTextField	= "State_Name";
				cmbDRIVER_STATE.DataValueField	= "State_Id";
				cmbDRIVER_STATE.DataBind();
				//	cmbDRIVER_STATE.Items.Insert(0,"");

				//Added by Mohit Agarwal 30-Oct-2007
				cmbLOSSREPORT_ORDER.DataSource		= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
				cmbLOSSREPORT_ORDER.DataTextField	= "LookupDesc";
				cmbLOSSREPORT_ORDER.DataValueField	= "LookupID";
				cmbLOSSREPORT_ORDER.DataBind();
				cmbLOSSREPORT_ORDER.Items.Insert(0,"");
				#endregion 

				PopulateComboBox();
				FillDiscountSectionDropDownList();

				//Added By Sumit(03-22-2006)
				if(hidCalledFrom.Value.ToUpper()=="UMB")
				{
					FillOperatorDiscountDropDownList();
					DataSet dsTemp;					
					dsTemp = ClsDriverDetail.FetchPolicyUmbrellaBoatInfo(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPolicyId.Value),int.Parse(hidPolicyVersionId.Value));
					if (dsTemp!=null && dsTemp.Tables[0].Rows.Count>0)
					{
						cmbOP_VEHICLE_ID.DataSource= dsTemp;
						cmbOP_VEHICLE_ID.DataTextField	= "Boat";
						cmbOP_VEHICLE_ID.DataValueField	= "Boat_ID";
						cmbOP_VEHICLE_ID.DataBind();
					}

					if(cmbOP_VEHICLE_ID.Items.Count<1)
					{
						lblOpVehMsg.Text	=  "No boats added until now. Please click <a href='#' onclick='RedirectToBoat();'>here</a> to add boats";
						trOpVehMsg.Attributes.Add ("style","display:inline;"); 
						trOpVehField.Attributes.Add ("style","display:none;"); 
						//rfvVEHICLE_ID.Enabled=false;
					}
					else
					{
						cmbOP_VEHICLE_ID.Items.Insert(0,"");
						trOpVehMsg.Attributes.Add ("style","display:none;"); 
						trOpVehField.Attributes.Add ("style","display:inline;"); 
						//rfvVEHICLE_ID.Enabled=true;
					}

					cmbOP_APP_VEHICLE_PRIN_OCC_ID.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("VHIDPO");
					cmbOP_APP_VEHICLE_PRIN_OCC_ID.DataTextField	= "LookupDesc";
					cmbOP_APP_VEHICLE_PRIN_OCC_ID.DataValueField	= "LookupID";
					cmbOP_APP_VEHICLE_PRIN_OCC_ID.DataBind();
					cmbOP_APP_VEHICLE_PRIN_OCC_ID.Items.Insert(0,""); 
					//cmbOP_VEHICLE_ID.Items.Insert (0,"");
					
					this.trOperator .Visible =true;
					rfvVEHICLE_ID.Enabled=false;
					rfvVEHICLE_DRIVER.Enabled=false;
					spnVEHICLE_ID.Attributes.Add("Style","Display:none");
				}
				else
				{
					this.trOperator.Visible =false;
					rfvVEHICLE_ID.Enabled=true;
					rfvVEHICLE_DRIVER.Enabled=true;
					spnVEHICLE_ID.Attributes.Add("Style","Display:inline");					
				}
				//////////////////////////////////////				

			}
			if(hidCalledFrom.Value.ToUpper()!="UMB")
				GetViolationPoints();
			SetWorkFlowControl();
			SetLabelPercentage();
			btnDelete.Enabled = false;

			//SetLicErrorMessage();
		}//end pageload
		#endregion

		#region AJAX CALL TO FILL DRIVER DROPDOWNS
		[Ajax.AjaxMethod()]
		public string AjaxCallFunction(int intAge) //Call on DOB Change Ajax Call
		{
			return fxnAssignedVehicleFromAjax(intAge);
		}		
		protected string fxnAssignedVehicleFromAjax(int intAge)
		{		
			DataSet ds = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupForDriverAssignedAs(intAge);
			return ds.GetXml();
		}
		[Ajax.AjaxMethod()]
		public string AjaxFetchZipForState(int stateID, string ZipID)
		{
			CmsWeb.webservices.ClsWebServiceCommon obj =  new CmsWeb.webservices.ClsWebServiceCommon();
			string result = "";
			result = obj.FetchZipForState(stateID,ZipID);
			return result;
			
		}
		#endregion

		protected void fxnAssignedVehicle()
		{
			int rowCnt;
			int rowCtr;
				
			string strXML = ClsVehicleInformation.FillPolVehicle(int.Parse(hidCUSTOMER_ID.Value)
				, int.Parse(hidPolicyId.Value)
				, int.Parse(hidPolicyVersionId.Value)
				, int.Parse(hidDRIVER_ID.Value==""?"0":hidDRIVER_ID.Value));

			XmlDocument objXmlDoc = new XmlDocument();
			objXmlDoc.LoadXml(strXML);
				
			int VehicleCount = objXmlDoc.SelectSingleNode("NewDataSet").ChildNodes.Count;

			if(VehicleCount < 1)
			{
				lblVehicleMsg.Text	=  "No vehicle added until now. Please click <a href='#' onclick='Redirect();'>here</a> to add vehicles";
				trVehMsg.Attributes.Add ("style","display:inline;"); 
				//trVehField.Attributes.Add ("style","display:none;"); 
				//rfvVEHICLE_ID.Enabled=false;
			}
			else
			{
				//cmbVEHICLE_ID.Items.Insert(0,"");
				trVehMsg.Attributes.Add ("style","display:none;"); 
				//trVehField.Attributes.Add ("style","display:inline;"); 
				//rfvVEHICLE_ID.Enabled=true;
			}

			rowCnt  = VehicleCount;
				



			//IList objList = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("VHIDPO");
			IList objList = null;
			string driver_Age = "0";
			if(DriverAge!="")
			{
				driver_Age = CalculateDriverAge(PolEffDate.Trim(),DriverAge);
				//intAge  = (int)((double)new TimeSpan(DateTime.Parse(PolEffDate.Trim()).Subtract(DateTime.Parse(DriverAge.ToString())).Ticks).Days / 365.25); 
			}	
			
			objList = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupForDriverAssignedVehicles(int.Parse(driver_Age));

			tblAssignedVeh.GridLines = GridLines.Both; 
			tblAssignedVeh.Attributes.Add("TotalRows",VehicleCount.ToString());

			for(rowCtr=1; rowCtr <= rowCnt; rowCtr++) 
			{
						
				TableRow tRow = new TableRow();	
				tRow.ID = "ID_" + rowCtr;
				tblAssignedVeh.Rows.Add(tRow);		
						
				//Vech ID
				//objXmlDoc.SelectSingleNode("NewDataSet").ChildNodes.Item(rowCtr-1).ChildNodes.Item(0).InnerXml

				//Model Make
				//objXmlDoc.SelectSingleNode("NewDataSet").ChildNodes.Item(rowCtr-1).ChildNodes.Item(1).InnerXml

				//Assigned Vech ID
				//objXmlDoc.SelectSingleNode("NewDataSet").ChildNodes.Item(rowCtr-1).ChildNodes.Item(2).InnerXml

				//Prin/Occ
				//objXmlDoc.SelectSingleNode("NewDataSet").ChildNodes.Item(rowCtr-1).ChildNodes.Item(3).InnerXml

				TableCell tCellDrive  = new TableCell();
				TableCell tCellVeh	  = new TableCell();
				TableCell tCellAs     = new TableCell();
				TableCell tCellDriver = new TableCell();

				Label lblHidVehID	= new Label();
				Label lblDrive		= new Label();
				Label lblVehicle	= new Label();
				Label lblAs			= new Label();
				DropDownList drpDrv	= new DropDownList();

				lblDrive.Text		= " Drive ";
				lblHidVehID.Text	= objXmlDoc.SelectSingleNode("NewDataSet").ChildNodes.Item(rowCtr-1).ChildNodes.Item(0).InnerXml;
				lblAs.Text			= " as ";
				lblVehicle.Text		= objXmlDoc.SelectSingleNode("NewDataSet").ChildNodes.Item(rowCtr-1).ChildNodes.Item(1).InnerXml;
				tRow.CssClass			= "midcolora";
				tCellDrive.CssClass		= "midcolora";
				tCellVeh.CssClass		= "midcolora";
				tCellAs.CssClass		= "midcolora";
				tCellDriver.CssClass	= "midcolora";
						
				tCellDrive.Width		= Unit.Percentage(10);
				tCellVeh.Width			= Unit.Percentage(20);
				tCellAs.Width			= Unit.Percentage(5);
				tCellDriver.Width		= Unit.Percentage(32);

				lblHidVehID.Visible	=  false;

				drpDrv	= new DropDownList();
				drpDrv.DataSource=objList;
				drpDrv.DataTextField	= "LookupDesc";
				drpDrv.DataValueField	= "LookupID";
				drpDrv.DataBind();						

				string strSelectedValInDrp = objXmlDoc.SelectSingleNode("NewDataSet").ChildNodes.Item(rowCtr-1).ChildNodes.Item(3).InnerXml;
				ListItem LI;
				LI = drpDrv.Items.FindByValue(strSelectedValInDrp);
				if (LI  != null)
					LI.Selected=true;
				else
					drpDrv.SelectedIndex=-1;
					
				tCellDrive.Controls.Add (lblDrive);
				tCellVeh.Controls.Add (lblVehicle);
				tCellVeh.Controls.Add (lblHidVehID);
				tCellAs.Controls.Add (lblAs);
				tCellDriver.Controls.Add (drpDrv);

				tRow.Cells.Add(tCellDrive);
				tRow.Cells.Add(tCellVeh);
				tRow.Cells.Add(tCellAs);
				tRow.Cells.Add(tCellDriver);

				tRow.Attributes.Add("RowVehID",lblHidVehID.Text);

					
			}
		}
		

		private void GetViolationPoints()
		{
			Cms.BusinessLayer.BlCommon.ClsQuickQuote objQuickQuote = new Cms.BusinessLayer.BlCommon.ClsQuickQuote();
			if(hidDRIVER_ID.Value!="" && hidDRIVER_ID.Value!="0" && hidDRIVER_ID.Value.ToUpper()!="NEW")
			{
				/*string strViolation = "<VIOLATIONPOINTS>" + objDriverDetail.GetAppViolationPoints(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidAppId.Value),int.Parse(hidAppVersionId.Value),int.Parse(hidDRIVER_ID.Value)) + "</VIOLATIONPOINTS>";
				XmlDocument xDoc = new XmlDocument();
				xDoc.LoadXml(strViolation);
				XmlNode xNode = xDoc.SelectSingleNode("//MVR");
				if(xNode!=null && xNode.InnerText!="")
					lblViolationPoints.Text = xNode.InnerText.ToString();
				xNode= null;

				xNode = xDoc.SelectSingleNode("//SUMOFACCIDENTPOINTS");
				if(xNode!=null && xNode.InnerText!="")
					lblAccidentPoints.Text = xNode.InnerText.ToString();*/
				DataTable dtTemp = objQuickQuote.GetMVRPointsForSurcharge(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPolicyId.Value),int.Parse(hidPolicyVersionId.Value),int.Parse(hidDRIVER_ID.Value),"POL");
				if(dtTemp!=null && dtTemp.Rows.Count>0)
				{
					int ViolationPoints = int.Parse(dtTemp.Rows[0]["SUM_MVR_POINTS"].ToString());
					int AccidentPoints = int.Parse(dtTemp.Rows[0]["ACCIDENT_POINTS"].ToString());
					if(ViolationPoints!=-1)
						lblViolationPoints.Text = ViolationPoints.ToString();				
					else
						lblViolationPoints.Text = "";

					if(AccidentPoints!=-1)
						lblAccidentPoints.Text = AccidentPoints.ToString();				
					else
						lblAccidentPoints.Text = "";

				}

				if(lblViolationPoints.Text == "" && lblAccidentPoints.Text == "")
				{
					trViolationField.Attributes.Add("style","display:none");
					trViolationSec.Attributes.Add("style","display:none");
					trViolationMsg.Attributes.Add("style","display:inline");
					lblViolationMsg.Text = "No violations added";
				}				
			}
			else
			{
				trViolationField.Attributes.Add("style","display:none");
				trViolationSec.Attributes.Add("style","display:none");
				trViolationMsg.Attributes.Add("style","display:inline");
				lblViolationMsg.Text = "No violations added";
			}
		}

		private void FillOperatorDiscountDropDownList()
		{
		
			cmbOP_DRIVER_COST_GAURAD_AUX.Items.Insert(0,"No"); 
			cmbOP_DRIVER_COST_GAURAD_AUX.Items.Insert(1,"Yes"); 
			cmbOP_DRIVER_COST_GAURAD_AUX.Items[0].Value = "0";
			cmbOP_DRIVER_COST_GAURAD_AUX.Items[1].Value = "1";
		
		}

		#region FillDiscountSectionDropDownList
		private void FillDiscountSectionDropDownList()
		{
		
			cmbDRIVER_PREF_RISK.Items.Insert(0,"No"); 
			cmbDRIVER_PREF_RISK.Items.Insert(1,"Yes"); 
			cmbDRIVER_PREF_RISK.Items[0].Value = "0";
			cmbDRIVER_PREF_RISK.Items[1].Value = "1";

			cmbDRIVER_GOOD_STUDENT.Items.Insert(0,"No"); 
			cmbDRIVER_GOOD_STUDENT.Items.Insert(1,"Yes"); 
			cmbDRIVER_GOOD_STUDENT.Items[0].Value = "0";
			cmbDRIVER_GOOD_STUDENT.Items[1].Value = "1";

			cmbSAFE_DRIVER_RENEWAL_DISCOUNT.Items.Insert(0,"No"); 
			cmbSAFE_DRIVER_RENEWAL_DISCOUNT.Items.Insert(1,"Yes"); 
			cmbSAFE_DRIVER_RENEWAL_DISCOUNT.Items[0].Value = "0";
			cmbSAFE_DRIVER_RENEWAL_DISCOUNT.Items[1].Value = "1";
		
		}
		#endregion
		private void getPolicyEffectiveYear()
		{	
			//string strEffDate;			
			PolEffDate  = new Cms.BusinessLayer.BlApplication.clsWatercraftInformation().GetPolEffectiveDate(int.Parse(GetCustomerID()),int.Parse(GetPolicyID()),int.Parse(GetPolicyVersionID()));			
		} 

		//Added by Sibin for Itrack Issue 5428 on 18 Feb 09
		private void getPolicyYearsWithWolverine()
		{	
			intYearsWithWolverine = new Cms.BusinessLayer.BlApplication.ClsDriverDetail().GetPolicyInsuredWithWolverine(Convert.ToInt32(GetCustomerID()),Convert.ToInt32(GetPolicyID()),Convert.ToInt32(GetPolicyVersionID()));//Done by Sibin for Itrack Issue 5495,5496 on 26 Feb 09
		}
		/// <summary>
		/// Fetching drivers count for the customer and application to enable and disable copy application driver button
		/// </summary>
		private void GetDriverCount()
		{
			
			int driverCnt=Cms.BusinessLayer.BlApplication.ClsDriverDetail.GetPolicyDriverCount(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPolicyId.Value), int.Parse(hidPolicyVersionId.Value));
			if(driverCnt==0)
			{
				//btnCopyAppDrivers.Visible=false;  	
			}
			else
			{
				//btnCopyAppDrivers.Visible=true;  	
			}
		}
		private void enableValidators()
		{
			rfvDRIVER_INCOME.Enabled =true;
			rfvNO_DEPENDENTS.Enabled=true;
			rfvNO_DEPENDENTS.ErrorMessage           =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"615");
			rfvDRIVER_INCOME.ErrorMessage           =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"614");
		}
		private void disableValidators()
		{
			rfvDRIVER_INCOME.Enabled=false;
			rfvNO_DEPENDENTS.Enabled=false;
			capNO_DEPENDENTS.Visible=false;
			cmbNO_DEPENDENTS.Attributes.Add("style","display:none");
		}
		/// <summary>
		/// setting attribute for the checkbox to show discount percentage when they are checked
		/// </summary>
		private void SetAttribute()
		{
			cmbOP_DRIVER_COST_GAURAD_AUX.Attributes.Add("onchange","ShowDiscountPercentage('" + cmbOP_DRIVER_COST_GAURAD_AUX.ClientID + "','" + capCoastGuard.ClientID + "')");
			cmbDRIVER_GOOD_STUDENT.Attributes.Add("onchange","ShowDiscountPercentage('" + cmbDRIVER_GOOD_STUDENT.ClientID + "','" + capGoodStudent.ClientID + "')");
			cmbDRIVER_PREF_RISK.Attributes.Add("onchange","ShowDiscountPercentage('" + cmbDRIVER_PREF_RISK.ClientID + "','" + capPremierDriver.ClientID + "');driverDiscount(1);");
			cmbSAFE_DRIVER_RENEWAL_DISCOUNT.Attributes.Add("onchange","ShowDiscountPercentage('" + cmbSAFE_DRIVER_RENEWAL_DISCOUNT.ClientID + "','" + capSAFE_DRIVER_RENEWAL_DISCOUNT.ClientID + "');driverDiscount(1);");
			cmbFULL_TIME_STUDENT.Attributes.Add("onChange","cmbFULL_TIME_STUDENT_Change(true);");
			cmbWAIVER_WORK_LOSS_BENEFITS.Attributes.Add("onChange","cmbWAIVER_WORK_LOSS_BENEFITS_Change(true);");			
		}

		/// <summary>
		/// fetching discount percentage from product factor master for Auto LOB
		/// </summary>
		private void SetLabelPercentage()
		{
			//Function Modified by Sibin on 2 Feb 2009 for Itrack Issue 5381
			capCoastGuard.Text="10%";
			try
			{
				XmlDocument xDoc=new XmlDocument();
				string strDriverPolicyXML = "" ,strDriverPolicy = "" ,strLOB = "AUTOP";
				strDriverPolicyXML	= Cms.BusinessLayer.BlApplication.ClsGeneralInformation.FetchDriverPolicyInputXML(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPolicyId.Value), int.Parse(hidPolicyVersionId.Value));	
				Cms.BusinessLayer.BlQuote.ClsGenerateQuote objQuote = new Cms.BusinessLayer.BlQuote.ClsGenerateQuote(CarrierSystemID);
				strDriverPolicy = objQuote.GetProductFactorMasterPath(strDriverPolicyXML,strLOB);
				xDoc.Load(strDriverPolicy); 
				XmlNodeList xNodeList=xDoc.SelectNodes("PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERDISCOUNT']/NODE[@ID ='DRIVERDISC']/ATTRIBUTES/@SAFEDRIVERCREDIT"); 
				capSAFE_DRIVER_RENEWAL_DISCOUNT.Text = " - (" + xNodeList[0].InnerText + "%)";
				xNodeList=xDoc.SelectNodes("PRODUCTMASTER/PRODUCT/FACTOR[@ID='DRIVERDISCOUNT']/NODE[@ID ='DRIVERDISC']/ATTRIBUTES/@PREMIERDRIVERCREDIT"); 
				capPremierDriver.Text = " - (" + xNodeList[0].InnerText + "%)";
				xNodeList=xDoc.SelectNodes("PRODUCTMASTER/PRODUCT/FACTOR[@ID='GOODSTUDENT']/NODE[@ID ='GOODSTUDENTDISCOUNT']/ATTRIBUTES/@CREDIT"); 
				capGoodStudent.Text = " - (" + xNodeList[0].InnerText + "%)";
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"5") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value		=		"2";
			}
		}
		
		private void SetControlsAttributes()
		{
			btnReset.Attributes.Add("onclick","javascript:return ResetForm1('" + Page.Controls[0].ID + "' );");
			hlkOCCURENCE_DATE.Attributes.Add("OnClick","fPopCalendar(document.APP_DRIVER_DETAILS.txtDRIVER_DOB, document.APP_DRIVER_DETAILS.txtDRIVER_DOB)");
			hlkDATE_LICENSED.Attributes.Add("OnClick","fPopCalendar(document.APP_DRIVER_DETAILS.txtDATE_LICENSED, document.APP_DRIVER_DETAILS.txtDATE_LICENSED)");
			cmbDRIVER_STATE.Attributes.Add("OnChange","javascript:FillDriverState();");
			txtDRIVER_FNAME.Attributes.Add ("onBlur","javascript:GenerateDriverCode(\"txtDRIVER_FNAME\");");
			txtDRIVER_LNAME.Attributes.Add ("onBlur","javascript:GenerateDriverCode(\"txtDRIVER_LNAME\");");
			btnCopyDefaultCustomer.Attributes.Add("onclick","javascript:return FillCustomerName();");
			//btnCopyAppDrivers.Attributes.Add("OnClick","javascript:return CoypApplicationDrivers();");
		}

		
		#region PopulatecomboBox
		private void PopulateComboBox()
		{
			
			#region "Loading singleton"
			DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
			cmbDRIVER_COUNTRY.DataSource		= dt;
			cmbDRIVER_COUNTRY.DataTextField		= "Country_Name";
			cmbDRIVER_COUNTRY.DataValueField	= "Country_Id";
			cmbDRIVER_COUNTRY.DataBind();

			 
			//cmbDRIVER_STATE.DataSource		=Cms.CmsWeb.ClsFetcher.ActiveState;
			//cmbDRIVER_STATE.DataTextField	= "State_Name";
			//cmbDRIVER_STATE.DataValueField	= "State_Id";
			//cmbDRIVER_STATE.DataBind();

			cmbDRIVER_LIC_STATE.DataSource = Cms.CmsWeb.ClsFetcher.State ;
			cmbDRIVER_LIC_STATE.DataTextField	= "State_Name";
			cmbDRIVER_LIC_STATE.DataValueField	= "State_Id";
			cmbDRIVER_LIC_STATE.DataBind();
			cmbDRIVER_LIC_STATE.Items.Insert(0,new ListItem("",""));
			cmbDRIVER_LIC_STATE.SelectedIndex=0;

			#endregion//Loading singleton

			
			//Populating the Relationship
			cmbRELATIONSHIP.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("DRACD");
			cmbRELATIONSHIP.DataTextField = "LookupDesc";
			cmbRELATIONSHIP.DataValueField = "LookupID";
			cmbRELATIONSHIP.DataBind();
			cmbRELATIONSHIP.Items.Insert(0,new ListItem("",""));
			cmbRELATIONSHIP.SelectedIndex=0;

			//Populating the driver type
			cmbDRIVER_DRIV_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("DRTCD");
			cmbDRIVER_DRIV_TYPE.DataTextField = "LookupDesc";
			cmbDRIVER_DRIV_TYPE.DataValueField = "LookupID";
			cmbDRIVER_DRIV_TYPE.DataBind();
			cmbDRIVER_DRIV_TYPE.Items.Insert(0,new ListItem("",""));
			cmbDRIVER_DRIV_TYPE.SelectedIndex=0;

			//Populating the Occupation class
			cmbDRIVER_OCC_CLASS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("%OCC");//Changed lookup from OCCCL to %OCC
			cmbDRIVER_OCC_CLASS.DataTextField = "LookupDesc";
			cmbDRIVER_OCC_CLASS.DataValueField = "LookupID";
			cmbDRIVER_OCC_CLASS.DataBind();
			cmbDRIVER_OCC_CLASS.Items.Insert(0,new ListItem("",""));
			cmbDRIVER_OCC_CLASS.SelectedIndex=0;
			
			cmbDRIVER_MART_STAT.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("Marst");
			cmbDRIVER_MART_STAT.DataTextField = "LookupDesc";
			cmbDRIVER_MART_STAT.DataValueField = "LookupCode";
			cmbDRIVER_MART_STAT.DataBind();
			cmbDRIVER_MART_STAT.Items.Insert(0,"");			
			cmbDRIVER_MART_STAT.SelectedIndex=0;

			cmbDRIVER_DRINK_VIOLATION.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbDRIVER_DRINK_VIOLATION.DataTextField="LookupDesc"; 
			cmbDRIVER_DRINK_VIOLATION.DataValueField="LookupCode";
			cmbDRIVER_DRINK_VIOLATION.DataBind();
			cmbDRIVER_DRINK_VIOLATION.Items.Insert(0,"");			

			cmbWAIVER_WORK_LOSS_BENEFITS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbWAIVER_WORK_LOSS_BENEFITS.DataTextField="LookupDesc"; 
			cmbWAIVER_WORK_LOSS_BENEFITS.DataValueField="LookupCode";
			cmbWAIVER_WORK_LOSS_BENEFITS.DataBind();
			cmbWAIVER_WORK_LOSS_BENEFITS.Items.Insert(0,"");

			cmbSIGNED_WAIVER_BENEFITS_FORM.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbSIGNED_WAIVER_BENEFITS_FORM.DataTextField="LookupDesc"; 
			cmbSIGNED_WAIVER_BENEFITS_FORM.DataValueField="LookupCode";
			cmbSIGNED_WAIVER_BENEFITS_FORM.DataBind();			

			
			cmbDRIVER_STUD_DIST_OVER_HUNDRED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbDRIVER_STUD_DIST_OVER_HUNDRED.DataTextField="LookupDesc"; 
			cmbDRIVER_STUD_DIST_OVER_HUNDRED.DataValueField="LookupCode";
			cmbDRIVER_STUD_DIST_OVER_HUNDRED.DataBind();
			cmbDRIVER_STUD_DIST_OVER_HUNDRED.Items.Insert(0,"");

			cmbDRIVER_LIC_SUSPENDED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbDRIVER_LIC_SUSPENDED.DataTextField="LookupDesc"; 
			cmbDRIVER_LIC_SUSPENDED.DataValueField="LookupCode";
			cmbDRIVER_LIC_SUSPENDED.DataBind();
			cmbDRIVER_LIC_SUSPENDED.Items.Insert(0,"");

			cmbDRIVER_VOLUNTEER_POLICE_FIRE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbDRIVER_VOLUNTEER_POLICE_FIRE.DataTextField="LookupDesc"; 
			cmbDRIVER_VOLUNTEER_POLICE_FIRE.DataValueField="LookupCode";
			cmbDRIVER_VOLUNTEER_POLICE_FIRE.DataBind();
			cmbDRIVER_VOLUNTEER_POLICE_FIRE.Items.Insert(0,"");

			cmbDRIVER_US_CITIZEN.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbDRIVER_US_CITIZEN.DataTextField="LookupDesc"; 
			cmbDRIVER_US_CITIZEN.DataValueField="LookupCode";
			cmbDRIVER_US_CITIZEN.DataBind();
			//cmbDRIVER_US_CITIZEN.Items.Insert(0,"");
			//cmbDRIVER_US_CITIZEN.SelectedIndex = 2;

			cmbDRIVER_PHYS_MED_IMPAIRE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbDRIVER_PHYS_MED_IMPAIRE.DataTextField="LookupDesc"; 
			cmbDRIVER_PHYS_MED_IMPAIRE.DataValueField="LookupCode";
			cmbDRIVER_PHYS_MED_IMPAIRE.DataBind();
			cmbDRIVER_PHYS_MED_IMPAIRE.Items.Insert(0,"");

			cmbIN_MILITARY.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbIN_MILITARY.DataTextField="LookupDesc"; 
			cmbIN_MILITARY.DataValueField="LookupID";
			cmbIN_MILITARY.DataBind();
			cmbIN_MILITARY.Items.Insert(0,"");

			cmbPARENTS_INSURANCE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PRTINS");
			cmbPARENTS_INSURANCE.DataTextField="LookupDesc"; 
			cmbPARENTS_INSURANCE.DataValueField="LookupID";
			cmbPARENTS_INSURANCE.DataBind();
			cmbPARENTS_INSURANCE.Items.Insert(0,"");

			cmbSTATIONED_IN_US_TERR.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbSTATIONED_IN_US_TERR.DataTextField="LookupDesc"; 
			cmbSTATIONED_IN_US_TERR.DataValueField="LookupID";
			cmbSTATIONED_IN_US_TERR.DataBind();
			cmbSTATIONED_IN_US_TERR.Items.Insert(0,"");
			
			cmbHAVE_CAR.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbHAVE_CAR.DataTextField="LookupDesc"; 
			cmbHAVE_CAR.DataValueField="LookupID";
			cmbHAVE_CAR.DataBind();
			cmbHAVE_CAR.Items.Insert(0,"");

			cmbFORM_F95.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbFORM_F95.DataTextField="LookupDesc"; 
			cmbFORM_F95.DataValueField="LookupID";
			cmbFORM_F95.DataBind();
			cmbFORM_F95.Items.Insert(0,"");

			cmbEXT_NON_OWN_COVG_INDIVI.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbEXT_NON_OWN_COVG_INDIVI.DataTextField="LookupDesc"; 
			cmbEXT_NON_OWN_COVG_INDIVI.DataValueField="LookupID";
			cmbEXT_NON_OWN_COVG_INDIVI.DataBind();
			cmbEXT_NON_OWN_COVG_INDIVI.Items.Insert(0,"");

			cmbSUPPORT_DOCUMENT.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbSUPPORT_DOCUMENT.DataTextField="LookupDesc"; 
			cmbSUPPORT_DOCUMENT.DataValueField="LookupCode";
			cmbSUPPORT_DOCUMENT.DataBind();
			cmbSUPPORT_DOCUMENT.Items.Insert(0,"");
			cmbSUPPORT_DOCUMENT.SelectedIndex = 0;
			

			cmbFULL_TIME_STUDENT.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbFULL_TIME_STUDENT.DataTextField="LookupDesc"; 
			cmbFULL_TIME_STUDENT.DataValueField="LookupCode";
			cmbFULL_TIME_STUDENT.DataBind();
						

			cmbAPP_VEHICLE_PRIN_OCC_ID.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("VHIDPO");
			cmbAPP_VEHICLE_PRIN_OCC_ID.DataTextField="LookupDesc"; 
			cmbAPP_VEHICLE_PRIN_OCC_ID.DataValueField="LookupID";
			cmbAPP_VEHICLE_PRIN_OCC_ID.DataBind();
			cmbAPP_VEHICLE_PRIN_OCC_ID.Items.Insert(0,"");

			cmbDRIVER_INCOME.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("DRINC");
			cmbDRIVER_INCOME.DataTextField="LookupDesc"; 
			cmbDRIVER_INCOME.DataValueField="LookupID";
			cmbDRIVER_INCOME.DataBind();
			cmbDRIVER_INCOME.Items.Insert(0,"");

			//Added by Swarup 29-Nov-2006
			cmbVIOLATIONS.DataSource		= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbVIOLATIONS.DataTextField	= "LookupDesc";
			cmbVIOLATIONS.DataValueField	= "LookupID";
			cmbVIOLATIONS.DataBind();
			cmbVIOLATIONS.Items.Insert(0,"");

			//Added by Mohit Agarwal 23-Jul ITrack 2183
			cmbMVR_STATUS.Items.Clear();
			ListItem list = new ListItem("", "0");
			cmbMVR_STATUS.Items.Add(list);
			list = new ListItem("Clear", "C");
			cmbMVR_STATUS.Items.Add(list);
			list = new ListItem("Non Clear", "V");
			cmbMVR_STATUS.Items.Add(list);
			list = new ListItem("Not Found", "N");
			cmbMVR_STATUS.Items.Add(list);
			list = new ListItem("Error/Reject", "E");
			cmbMVR_STATUS.Items.Add(list);
			
			cmbMVR_ORDERED.DataSource		= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbMVR_ORDERED.DataTextField	= "LookupDesc";
			cmbMVR_ORDERED.DataValueField	= "LookupID";
			cmbMVR_ORDERED.DataBind();
			cmbMVR_ORDERED.Items.Insert(0,"");

			if(hidCalledFrom.Value == "PPA")
			{
				cmbNO_DEPENDENTS.DataSource   =  Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("NODEP");
				cmbNO_DEPENDENTS.DataTextField="LookupDesc"; 
				cmbNO_DEPENDENTS.DataValueField="LookupID";
				cmbNO_DEPENDENTS.DataBind();
				cmbNO_DEPENDENTS.Items.Insert(0,"");
				//cmbNO_DEPENDENTS.SelectedIndex =1;
			}


			if(hidCalledFrom.Value.ToUpper()==CALLED_FROM_UMBRELLA)
			{
				/*	ClsVehicleInformation.FillPolicyVehicleInfo(cmbVEHICLE_ID
						, int.Parse(hidCUSTOMER_ID.Value)
						, int.Parse(hidPolicyId.Value)
						, int.Parse(hidPolicyVersionId.Value));
				}
				else
				{*/
				ClsVehicleInformation.FillPolicyUmbrellaVehicleInfo(cmbVEHICLE_ID
					, int.Parse(hidCUSTOMER_ID.Value)
					, int.Parse(hidPolicyId.Value)
					, int.Parse(hidPolicyVersionId.Value));
			}
			
			
		}
		#endregion

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsPolicyDriverInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsPolicyDriverInfo objDriverDetailInfo = new ClsPolicyDriverInfo();
			int flag=0;
			
			objDriverDetailInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
			objDriverDetailInfo.POLICY_ID = int.Parse(hidPolicyId.Value);
			objDriverDetailInfo.POLICY_VERSION_ID = int.Parse(hidPolicyVersionId.Value);
			objDriverDetailInfo.DRIVER_FNAME = txtDRIVER_FNAME.Text;
			objDriverDetailInfo.DRIVER_MNAME = txtDRIVER_MNAME.Text;
			objDriverDetailInfo.DRIVER_LNAME = txtDRIVER_LNAME.Text;
			objDriverDetailInfo.DRIVER_CODE = txtDRIVER_CODE.Text;
			objDriverDetailInfo.DRIVER_SUFFIX = txtDRIVER_SUFFIX.Text;
			objDriverDetailInfo.DRIVER_ADD1 = txtDRIVER_ADD1.Text;
			objDriverDetailInfo.DRIVER_ADD2 = txtDRIVER_ADD2.Text;
			objDriverDetailInfo.DRIVER_CITY = txtDRIVER_CITY.Text;
			objDriverDetailInfo.DRIVER_STATE = cmbDRIVER_STATE.SelectedValue;
			objDriverDetailInfo.DRIVER_ZIP = txtDRIVER_ZIP.Text;
			objDriverDetailInfo.DRIVER_COUNTRY = cmbDRIVER_COUNTRY.SelectedValue;
			objDriverDetailInfo.DRIVER_HOME_PHONE = txtDRIVER_HOME_PHONE.Text;
			objDriverDetailInfo.DRIVER_BUSINESS_PHONE =	txtDRIVER_BUSINESS_PHONE.Text;
			objDriverDetailInfo.DRIVER_EXT = txtDRIVER_EXT.Text;
			objDriverDetailInfo.DRIVER_MOBILE = txtDRIVER_MOBILE.Text;

			objDriverDetailInfo.MVR_CLASS = txtMVR_CLASS.Text;
			objDriverDetailInfo.MVR_LIC_CLASS = txtMVR_LIC_CLASS.Text;
			objDriverDetailInfo.MVR_LIC_RESTR = txtMVR_LIC_RESTR.Text;
			objDriverDetailInfo.MVR_DRIV_LIC_APPL = txtMVR_DRIV_LIC_APPL.Text;

			objDriverDetailInfo.MVR_REMARKS = txtMVR_REMARKS.Text;
			objDriverDetailInfo.MVR_STATUS = cmbMVR_STATUS.SelectedValue;
			
			if(cmbLOSSREPORT_ORDER.SelectedValue != "")
				objDriverDetailInfo.LOSSREPORT_ORDER = int.Parse(cmbLOSSREPORT_ORDER.SelectedValue);

			if(txtLOSSREPORT_DATETIME.Text.Trim() != "")
				objDriverDetailInfo.LOSSREPORT_DATETIME = DateTime.Parse(txtLOSSREPORT_DATETIME.Text.Trim());

			if (txtDRIVER_DOB.Text.Trim()!="")
				objDriverDetailInfo.DRIVER_DOB	=	ConvertToDate(txtDRIVER_DOB.Text);

			//objDriverDetailInfo.DRIVER_SSN = txtDRIVER_SSN.Text;
			if(txtDRIVER_SSN.Text.Trim()!="")
			{
				objDriverDetailInfo.DRIVER_SSN			=	Cms.BusinessLayer.BlCommon.ClsCommon.EncryptString(txtDRIVER_SSN.Text.Trim());
				txtDRIVER_SSN.Text = "";
			}
			else
				objDriverDetailInfo.DRIVER_SSN			= hidSSN_NO.Value;

			if(cmbDRIVER_MART_STAT.SelectedItem !=null)
				objDriverDetailInfo.DRIVER_MART_STAT = cmbDRIVER_MART_STAT.SelectedValue;
			
			if(cmbDRIVER_SEX.SelectedItem !=null)
				objDriverDetailInfo.DRIVER_SEX = cmbDRIVER_SEX.SelectedValue;
			
			
			objDriverDetailInfo.DRIVER_DRIV_LIC = txtDRIVER_DRIV_LIC.Text;
			objDriverDetailInfo.DRIVER_LIC_STATE = cmbDRIVER_LIC_STATE.SelectedValue;
			
			if(txtDATE_LICENSED.Text.Trim() != "")
				objDriverDetailInfo.DATE_LICENSED = ConvertToDate(txtDATE_LICENSED.Text);

			if(cmbDRIVER_DRIV_TYPE.SelectedItem !=null)
				objDriverDetailInfo.DRIVER_DRIV_TYPE = cmbDRIVER_DRIV_TYPE.SelectedValue;
			
			if(cmbDRIVER_OCC_CLASS.SelectedItem !=null)
				objDriverDetailInfo.DRIVER_OCC_CLASS = cmbDRIVER_OCC_CLASS.SelectedValue;

			if(cmbDRIVER_PHYS_MED_IMPAIRE.SelectedItem!=null)
				objDriverDetailInfo.DRIVER_PHYS_MED_IMPAIRE=cmbDRIVER_PHYS_MED_IMPAIRE.SelectedValue;
			if(cmbDRIVER_DRIV_TYPE.SelectedValue=="11603")
			{
				if(cmbDRIVER_DRINK_VIOLATION.SelectedItem !=null)
					objDriverDetailInfo.DRIVER_DRINK_VIOLATION=cmbDRIVER_DRINK_VIOLATION.SelectedValue;
			}
			if(txtDRIVER_DOB.Text!="")
			{
				// changed to appeffective date to get diff
				//if((System.DateTime.Now.Year - Convert.ToDateTime(txtDRIVER_DOB.Text).Year) > 25)
				//if((int)((double)new TimeSpan(DateTime.Parse(PolEffDate.Trim()).Subtract(DateTime.Parse(txtDRIVER_DOB.Text)).Ticks).Days / 365.25) > 25) 
				//	flag=1;

				//Itrack 5736
				string driver_Age = "0";
				driver_Age = CalculateDriverAge(PolEffDate.Trim(),txtDRIVER_DOB.Text.Trim());
				if(Convert.ToInt32(driver_Age) >= 25)
					flag=1;


			}
			if(hidCalledFrom.Value.ToUpper()=="PPA" && (flag == 1))
			{
				objDriverDetailInfo.DRIVER_GOOD_STUDENT="";
				objDriverDetailInfo.DRIVER_STUD_DIST_OVER_HUNDRED="";			
			}
			else
			{
				objDriverDetailInfo.DRIVER_GOOD_STUDENT = cmbDRIVER_GOOD_STUDENT.SelectedValue;

				if(cmbDRIVER_STUD_DIST_OVER_HUNDRED.SelectedValue!=null)
					objDriverDetailInfo.DRIVER_STUD_DIST_OVER_HUNDRED=cmbDRIVER_STUD_DIST_OVER_HUNDRED.SelectedValue;

				if(cmbIN_MILITARY.SelectedValue !=null &&  cmbIN_MILITARY.SelectedValue != "")
					objDriverDetailInfo.IN_MILITARY = int.Parse(cmbIN_MILITARY.SelectedValue);

				if(objDriverDetailInfo.IN_MILITARY == 10963)
				{

					//If Driver is above 25 or equal the STATIONED_IN_US_TERR will be null.
					string driver_Age = "0";
					driver_Age = CalculateDriverAge(PolEffDate.Trim(),txtDRIVER_DOB.Text.Trim() );
					if(Convert.ToInt32(driver_Age) >= 25 )
					{
						//if(cmbSTATIONED_IN_US_TERR.SelectedValue !=null &&  cmbSTATIONED_IN_US_TERR.SelectedValue != "")
						//	objDriverDetailInfo.STATIONED_IN_US_TERR = System.DBNull.Value;
					}
					else
					{
						if(cmbSTATIONED_IN_US_TERR.SelectedValue !=null &&  cmbSTATIONED_IN_US_TERR.SelectedValue != "")
							objDriverDetailInfo.STATIONED_IN_US_TERR = int.Parse(cmbSTATIONED_IN_US_TERR.SelectedValue);
					}

					//if(cmbSTATIONED_IN_US_TERR.SelectedValue !=null &&  cmbSTATIONED_IN_US_TERR.SelectedValue != "")
					//	objDriverDetailInfo.STATIONED_IN_US_TERR = int.Parse(cmbSTATIONED_IN_US_TERR.SelectedValue);
				}
				if(objDriverDetailInfo.IN_MILITARY == 10963 || objDriverDetailInfo.DRIVER_STUD_DIST_OVER_HUNDRED == "1")
				{
					if(cmbHAVE_CAR.SelectedValue !=null &&  cmbHAVE_CAR.SelectedValue != "")
						objDriverDetailInfo.HAVE_CAR = int.Parse(cmbHAVE_CAR.SelectedValue);
				}
				//				if(objDriverDetailInfo.DRIVER_STUD_DIST_OVER_HUNDRED == "1")
				//				{
				if(cmbPARENTS_INSURANCE.SelectedItem!=null && cmbPARENTS_INSURANCE.SelectedItem.Value!="")
					objDriverDetailInfo.PARENTS_INSURANCE = int.Parse(cmbPARENTS_INSURANCE.SelectedItem.Value);
				//				}

			}

			if(objDriverDetailInfo.DRIVER_GOOD_STUDENT=="1")
			{
				if(cmbFULL_TIME_STUDENT.SelectedItem!=null && cmbFULL_TIME_STUDENT.SelectedItem.Value!="")
					objDriverDetailInfo.FULL_TIME_STUDENT = int.Parse(cmbFULL_TIME_STUDENT.SelectedItem.Value);

				if(objDriverDetailInfo.FULL_TIME_STUDENT == 1) //yes
				{
					if(cmbSUPPORT_DOCUMENT.SelectedItem!=null && cmbSUPPORT_DOCUMENT.SelectedItem.Value!="")
						objDriverDetailInfo.SUPPORT_DOCUMENT = int.Parse(cmbSUPPORT_DOCUMENT.SelectedItem.Value);
				}
			}

			if(cmbFORM_F95.SelectedValue !=null &&  cmbFORM_F95.SelectedValue != "")
				objDriverDetailInfo.FORM_F95 = int.Parse(cmbFORM_F95.SelectedValue);

			if(cmbEXT_NON_OWN_COVG_INDIVI.SelectedValue !=null &&  cmbEXT_NON_OWN_COVG_INDIVI.SelectedValue != "")
				objDriverDetailInfo.EXT_NON_OWN_COVG_INDIVI = int.Parse(cmbEXT_NON_OWN_COVG_INDIVI.SelectedValue);
			
			
			//objDriverDetailInfo.DRIVER_DRIVERLOYER_NAME = txtDRIVER_DRIVERLOYER_NAME.Text; //Commented by Sibin for Itrack Issue 5060 on 26 Nov 08
			//objDriverDetailInfo.DRIVER_DRIVERLOYER_ADD = txtDRIVER_DRIVERLOYER_ADD.Text; //Commented by Sibin for Itrack Issue 5060 on 26 Nov 08

			//added By Shafi
			//Check for Auto and Michigan
			if(hidCalledFrom.Value.ToString().ToUpper()=="PPA" && hidState_id.Value =="22")
				objDriverDetailInfo.NO_DEPENDENTS =int.Parse(hidNO_DEPENDENTS.Value);//int.Parse(cmbNO_DEPENDENTS.SelectedValue);
			else
				objDriverDetailInfo.NO_DEPENDENTS=0;
			if(cmbDRIVER_INCOME.SelectedIndex >0)
				objDriverDetailInfo.DRIVER_INCOME=Convert.ToDouble(cmbDRIVER_INCOME.SelectedValue);


			if(cmbRELATIONSHIP.SelectedValue.ToString() != "")
				objDriverDetailInfo.RELATIONSHIP = int.Parse(cmbRELATIONSHIP.SelectedItem.Value);
			else
				objDriverDetailInfo.RELATIONSHIP=0;


			//if(cmbDRIVER_STUD_DIST_OVER_HUNDRED.SelectedValue!=null)
			//	objDriverDetailInfo.DRIVER_STUD_DIST_OVER_HUNDRED=cmbDRIVER_STUD_DIST_OVER_HUNDRED.SelectedValue;

			if(cmbDRIVER_LIC_SUSPENDED.SelectedValue!=null)
				objDriverDetailInfo.DRIVER_LIC_SUSPENDED=cmbDRIVER_LIC_SUSPENDED.SelectedValue;


			if(cmbDRIVER_VOLUNTEER_POLICE_FIRE.SelectedValue!=null)
				objDriverDetailInfo.DRIVER_VOLUNTEER_POLICE_FIRE=cmbDRIVER_VOLUNTEER_POLICE_FIRE.SelectedValue;
			
			if(cmbDRIVER_US_CITIZEN.SelectedValue!=null)
				objDriverDetailInfo.DRIVER_US_CITIZEN = cmbDRIVER_US_CITIZEN.SelectedValue;
			if(objDriverDetailInfo.DRIVER_DRIV_TYPE=="11603")
			{
				if(cmbVEHICLE_ID.SelectedItem!=null && cmbVEHICLE_ID.SelectedItem.Value!="")  
					objDriverDetailInfo.VEHICLE_ID=cmbVEHICLE_ID.SelectedItem.Value==""?Convert.ToInt32("0"):Convert.ToInt32(cmbVEHICLE_ID.SelectedValue);
				if(cmbAPP_VEHICLE_PRIN_OCC_ID.SelectedItem!=null && cmbAPP_VEHICLE_PRIN_OCC_ID.SelectedItem.Value!="")
					objDriverDetailInfo.APP_VEHICLE_PRIN_OCC_ID = Convert.ToInt32(cmbAPP_VEHICLE_PRIN_OCC_ID.SelectedValue);  
			} 
			
			

			//objDriverDetailInfo.DRIVER_GOOD_STUDENT = cmbDRIVER_GOOD_STUDENT.SelectedValue;
			if(hidDateDiff.Value != "" && hidDateDiff.Value != "NaN")
			{
				if(Convert.ToInt32(hidDateDiff.Value)>=19) //Available only when age of driver is greater than 19
					objDriverDetailInfo.DRIVER_PREF_RISK = cmbDRIVER_PREF_RISK.SelectedValue;
			}
			objDriverDetailInfo.SAFE_DRIVER_RENEWAL_DISCOUNT = Convert.ToInt32(cmbSAFE_DRIVER_RENEWAL_DISCOUNT.SelectedValue); 

			//Flag for Waiver Risk Benefits
			int WaiverFlag=0;
			if(txtDRIVER_DOB.Text!="")
			{
				// CHANGED TO APP EFECTIVE DATE FROM SYSTEM DATETIME NOW
				//if((System.DateTime.Now.Year - Convert.ToDateTime(txtDRIVER_DOB.Text).Year) >= Convert.ToInt32(strWaiverBenefitsLimit))
				//if((int)((double)new TimeSpan(DateTime.Parse(PolEffDate.Trim()).Subtract(DateTime.Parse(txtDRIVER_DOB.Text)).Ticks).Days / 365.25) >= Convert.ToInt32(strWaiverBenefitsLimit))
				//	WaiverFlag=1;

				//Itrack 5736
				string driver_Age = "0";
				driver_Age = CalculateDriverAge(PolEffDate.Trim(),txtDRIVER_DOB.Text.Trim());
				if(Convert.ToInt32(driver_Age) >= Convert.ToInt32(strWaiverBenefitsLimit))
					WaiverFlag=1;

			}
			if(hidCalledFrom.Value.ToUpper()=="PPA" && (WaiverFlag == 1) &&  hidState_id.Value=="22")
			{
				if(cmbWAIVER_WORK_LOSS_BENEFITS.SelectedValue!=null)
					objDriverDetailInfo.WAIVER_WORK_LOSS_BENEFITS=cmbWAIVER_WORK_LOSS_BENEFITS.SelectedValue;	
			}
			else
			{
				objDriverDetailInfo.WAIVER_WORK_LOSS_BENEFITS="";
			}
			if(objDriverDetailInfo.WAIVER_WORK_LOSS_BENEFITS=="1")
			{
				if(cmbSIGNED_WAIVER_BENEFITS_FORM.SelectedItem!=null && cmbSIGNED_WAIVER_BENEFITS_FORM.SelectedItem.Value!="")
					objDriverDetailInfo.SIGNED_WAIVER_BENEFITS_FORM = int.Parse(cmbSIGNED_WAIVER_BENEFITS_FORM.SelectedItem.Value);
			}

			//Added By Sumit(03/22/2006) For Operator
			if(cmbOP_DRIVER_COST_GAURAD_AUX.SelectedIndex >0)
				objDriverDetailInfo.OP_DRIVER_COST_GAURAD_AUX = Convert.ToInt32(cmbOP_DRIVER_COST_GAURAD_AUX.SelectedValue);

			if(cmbOP_VEHICLE_ID.SelectedIndex  > 0) 
				objDriverDetailInfo.OP_VEHICLE_ID=Convert.ToInt32 (cmbOP_VEHICLE_ID.SelectedItem.Value);
			
			if(cmbOP_APP_VEHICLE_PRIN_OCC_ID.SelectedIndex > 0)
				objDriverDetailInfo.OP_APP_VEHICLE_PRIN_OCC_ID = Convert.ToInt32(cmbOP_APP_VEHICLE_PRIN_OCC_ID.SelectedValue); 

			objDriverDetailInfo.ASSIGNED_VEHICLE = hidSeletedData.Value;
		

			objDriverDetailInfo.CREATED_BY = int.Parse(GetUserId());
			objDriverDetailInfo.CREATED_DATETIME = DateTime.Now;
			objDriverDetailInfo.MODIFIED_BY = int.Parse(GetUserId());

			// Added by swarup on 15-Dec-2006
			objDriverDetailInfo.VIOLATIONS= int.Parse(cmbVIOLATIONS.SelectedItem.Value==""?"0":cmbVIOLATIONS.SelectedItem.Value);
			objDriverDetailInfo.MVR_ORDERED= int.Parse(cmbMVR_ORDERED.SelectedItem.Value==""?"0":cmbMVR_ORDERED.SelectedItem.Value);
			if(txtDATE_ORDERED.Text.Trim() !="")
				objDriverDetailInfo.DATE_ORDERED	=	Convert.ToDateTime(txtDATE_ORDERED.Text);

			objDriverDetailInfo.LAST_UPDATED_DATETIME = DateTime.Now;
			if(hidDRIVER_ID.Value!="" && hidDRIVER_ID.Value!="New")
			{
				objDriverDetailInfo.DRIVER_ID=Int32.Parse(hidDRIVER_ID.Value);				
			}
			else
			{
				hidCustomInfo.Value=";Driver Name = " + txtDRIVER_FNAME.Text + " " + txtDRIVER_LNAME.Text + ";Driver Code = " + txtDRIVER_CODE.Text;
			}
	
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidDRIVER_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objDriverDetailInfo;
		}

		private Cms.Model.Policy.Umbrella.ClsDriverOperatorInfo GetFormValueUmb()
		{
			//Creating the Model object for holding the New data
			Cms.Model.Policy.Umbrella.ClsDriverOperatorInfo objDriverDetailInfo = new Cms.Model.Policy.Umbrella.ClsDriverOperatorInfo();
			int flag=0;
			
			objDriverDetailInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
			objDriverDetailInfo.POLICY_ID = int.Parse(hidPolicyId.Value);
			objDriverDetailInfo.POLICY_VERSION_ID = int.Parse(hidPolicyVersionId.Value);
			objDriverDetailInfo.DRIVER_FNAME = txtDRIVER_FNAME.Text;
			objDriverDetailInfo.DRIVER_MNAME = txtDRIVER_MNAME.Text;
			objDriverDetailInfo.DRIVER_LNAME = txtDRIVER_LNAME.Text;
			objDriverDetailInfo.DRIVER_CODE = txtDRIVER_CODE.Text;
			objDriverDetailInfo.DRIVER_SUFFIX = txtDRIVER_SUFFIX.Text;
			objDriverDetailInfo.DRIVER_ADD1 = txtDRIVER_ADD1.Text;
			objDriverDetailInfo.DRIVER_ADD2 = txtDRIVER_ADD2.Text;
			objDriverDetailInfo.DRIVER_CITY = txtDRIVER_CITY.Text;
			objDriverDetailInfo.DRIVER_STATE = cmbDRIVER_STATE.SelectedValue;
			objDriverDetailInfo.DRIVER_ZIP = txtDRIVER_ZIP.Text;
			objDriverDetailInfo.DRIVER_COUNTRY = cmbDRIVER_COUNTRY.SelectedValue;
			objDriverDetailInfo.DRIVER_HOME_PHONE = txtDRIVER_HOME_PHONE.Text;
			objDriverDetailInfo.DRIVER_BUSINESS_PHONE =	txtDRIVER_BUSINESS_PHONE.Text;
			objDriverDetailInfo.DRIVER_EXT = txtDRIVER_EXT.Text;
			objDriverDetailInfo.DRIVER_MOBILE = txtDRIVER_MOBILE.Text;
			
			if (txtDRIVER_DOB.Text.Trim()!="")
				objDriverDetailInfo.DRIVER_DOB	=	ConvertToDate(txtDRIVER_DOB.Text);

			objDriverDetailInfo.DRIVER_SSN = txtDRIVER_SSN.Text;
			
			if(cmbDRIVER_MART_STAT.SelectedItem !=null)
				objDriverDetailInfo.DRIVER_MART_STAT = cmbDRIVER_MART_STAT.SelectedValue;
			
			if(cmbDRIVER_SEX.SelectedItem !=null)
				objDriverDetailInfo.DRIVER_SEX = cmbDRIVER_SEX.SelectedValue;
			
			
			objDriverDetailInfo.DRIVER_DRIV_LIC = txtDRIVER_DRIV_LIC.Text;
			objDriverDetailInfo.DRIVER_LIC_STATE = cmbDRIVER_LIC_STATE.SelectedValue;
			
			if(txtDATE_LICENSED.Text.Trim() != "")
				objDriverDetailInfo.DATE_LICENSED = ConvertToDate(txtDATE_LICENSED.Text);

			if(cmbDRIVER_DRIV_TYPE.SelectedItem !=null)
				objDriverDetailInfo.DRIVER_DRIV_TYPE = cmbDRIVER_DRIV_TYPE.SelectedValue;
			
			if(cmbDRIVER_OCC_CLASS.SelectedItem !=null)
				objDriverDetailInfo.DRIVER_OCC_CLASS = cmbDRIVER_OCC_CLASS.SelectedValue;

			if(cmbDRIVER_PHYS_MED_IMPAIRE.SelectedItem!=null)
				objDriverDetailInfo.DRIVER_PHYS_MED_IMPAIRE=cmbDRIVER_PHYS_MED_IMPAIRE.SelectedValue;
			if(cmbDRIVER_DRIV_TYPE.SelectedValue=="11603")
			{
				if(cmbDRIVER_DRINK_VIOLATION.SelectedItem !=null)
					objDriverDetailInfo.DRIVER_DRINK_VIOLATION=cmbDRIVER_DRINK_VIOLATION.SelectedValue;
			}
			if(txtDRIVER_DOB.Text!="")
			{
				// Changed to app effective date from system date time 
				//if((System.DateTime.Now.Year - Convert.ToDateTime(txtDRIVER_DOB.Text).Year) > 25)
				//if((int)((double)new TimeSpan(DateTime.Parse(PolEffDate.Trim()).Subtract(DateTime.Parse(txtDRIVER_DOB.Text)).Ticks).Days / 365.25) > 25)
				//	flag=1;

				string driver_Age = "0";
				driver_Age = CalculateDriverAge(PolEffDate.Trim(),txtDRIVER_DOB.Text.Trim());
				if(Convert.ToInt32(driver_Age) >= 25)
					flag=1;


			}
			if(hidCalledFrom.Value.ToUpper()=="PPA" && (flag == 1))
			{
				objDriverDetailInfo.DRIVER_GOOD_STUDENT="";
				objDriverDetailInfo.DRIVER_STUD_DIST_OVER_HUNDRED="";			
			}
			else
			{
				objDriverDetailInfo.DRIVER_GOOD_STUDENT = cmbDRIVER_GOOD_STUDENT.SelectedValue;

				if(cmbDRIVER_STUD_DIST_OVER_HUNDRED.SelectedValue!=null)
					objDriverDetailInfo.DRIVER_STUD_DIST_OVER_HUNDRED=cmbDRIVER_STUD_DIST_OVER_HUNDRED.SelectedValue;
			}

			
			//objDriverDetailInfo.DRIVER_DRIVERLOYER_NAME = txtDRIVER_DRIVERLOYER_NAME.Text; //Commented by Sibin for Itrack Issue 5060 on 26 Nov 08
			//objDriverDetailInfo.DRIVER_DRIVERLOYER_ADD = txtDRIVER_DRIVERLOYER_ADD.Text; //Commented by Sibin for Itrack Issue 5060 on 26 Nov 08

			//added By Shafi
			//Check for Auto and Michigan
			if(hidCalledFrom.Value.ToString().ToUpper()=="PPA" && hidState_id.Value =="22")
				objDriverDetailInfo.NO_DEPENDENTS =int.Parse(cmbNO_DEPENDENTS.SelectedValue);
			else
				objDriverDetailInfo.NO_DEPENDENTS=0;
			if(cmbDRIVER_INCOME.SelectedIndex >0)
				objDriverDetailInfo.DRIVER_INCOME=Convert.ToDouble(cmbDRIVER_INCOME.SelectedValue);


			if(cmbRELATIONSHIP.SelectedValue.ToString() != "")
				objDriverDetailInfo.RELATIONSHIP = int.Parse(cmbRELATIONSHIP.SelectedItem.Value);
			else
				objDriverDetailInfo.RELATIONSHIP=0;


			//if(cmbDRIVER_STUD_DIST_OVER_HUNDRED.SelectedValue!=null)
			//	objDriverDetailInfo.DRIVER_STUD_DIST_OVER_HUNDRED=cmbDRIVER_STUD_DIST_OVER_HUNDRED.SelectedValue;

			if(cmbDRIVER_LIC_SUSPENDED.SelectedValue!=null)
				objDriverDetailInfo.DRIVER_LIC_SUSPENDED=cmbDRIVER_LIC_SUSPENDED.SelectedValue;


			if(cmbDRIVER_VOLUNTEER_POLICE_FIRE.SelectedValue!=null)
				objDriverDetailInfo.DRIVER_VOLUNTEER_POLICE_FIRE=cmbDRIVER_VOLUNTEER_POLICE_FIRE.SelectedValue;
			
			if(cmbDRIVER_US_CITIZEN.SelectedValue!=null)
				objDriverDetailInfo.DRIVER_US_CITIZEN = cmbDRIVER_US_CITIZEN.SelectedValue;
			if(cmbDRIVER_DRIV_TYPE.SelectedValue=="11603")
			{
				if(cmbVEHICLE_ID.SelectedItem!=null && cmbVEHICLE_ID.SelectedItem.Text!="")  
					objDriverDetailInfo.VEHICLE_ID=cmbVEHICLE_ID.SelectedItem.Value==""?Convert.ToInt32("0"):Convert.ToInt32(cmbVEHICLE_ID.SelectedValue);
			} 
			if(cmbDRIVER_DRIV_TYPE.SelectedValue=="11603")
			{
				if(cmbAPP_VEHICLE_PRIN_OCC_ID.SelectedItem!=null && cmbAPP_VEHICLE_PRIN_OCC_ID.SelectedItem.Text!="")
					objDriverDetailInfo.APP_VEHICLE_PRIN_OCC_ID = Convert.ToInt32(cmbAPP_VEHICLE_PRIN_OCC_ID.SelectedValue);  
			}

			//objDriverDetailInfo.DRIVER_GOOD_STUDENT = cmbDRIVER_GOOD_STUDENT.SelectedValue;
			objDriverDetailInfo.DRIVER_PREF_RISK = cmbDRIVER_PREF_RISK.SelectedValue;
			objDriverDetailInfo.SAFE_DRIVER_RENEWAL_DISCOUNT = Convert.ToInt32(cmbSAFE_DRIVER_RENEWAL_DISCOUNT.SelectedValue); 

			//Flag for Waiver Risk Benefits
			int WaiverFlag=0;
			if(txtDRIVER_DOB.Text!="")
			{
				// Changed to app effective date from sysytem date
				//if((System.DateTime.Now.Year - Convert.ToDateTime(txtDRIVER_DOB.Text).Year) >= Convert.ToInt32(strWaiverBenefitsLimit))
				//if((int)((double)new TimeSpan(DateTime.Parse(PolEffDate.Trim()).Subtract(DateTime.Parse(txtDRIVER_DOB.Text)).Ticks).Days / 365.25) >= Convert.ToInt32(strWaiverBenefitsLimit))
				//	WaiverFlag=1;

				string driver_Age = "0";
				driver_Age = CalculateDriverAge(PolEffDate.Trim(),txtDRIVER_DOB.Text.Trim());
				if(Convert.ToInt32(driver_Age) >= Convert.ToInt32(strWaiverBenefitsLimit))
					WaiverFlag=1;

			}
			if(hidCalledFrom.Value.ToUpper()=="PPA" && (WaiverFlag == 1) &&  hidState_id.Value=="22")
			{
				if(cmbWAIVER_WORK_LOSS_BENEFITS.SelectedValue!=null)
					objDriverDetailInfo.WAIVER_WORK_LOSS_BENEFITS=cmbWAIVER_WORK_LOSS_BENEFITS.SelectedValue;	
			}
			else
			{
				objDriverDetailInfo.WAIVER_WORK_LOSS_BENEFITS="";
			}

			//Added By Sumit(03/22/2006) For Operator
			if(cmbOP_DRIVER_COST_GAURAD_AUX.SelectedIndex >0)
				objDriverDetailInfo.OP_DRIVER_COST_GAURAD_AUX = Convert.ToInt32(cmbOP_DRIVER_COST_GAURAD_AUX.SelectedValue);

			if(cmbOP_VEHICLE_ID.SelectedIndex  > 0) 
				objDriverDetailInfo.OP_VEHICLE_ID=Convert.ToInt32 (cmbOP_VEHICLE_ID.SelectedItem.Value);
			
			if(cmbOP_APP_VEHICLE_PRIN_OCC_ID.SelectedIndex > 0)
				objDriverDetailInfo.OP_APP_VEHICLE_PRIN_OCC_ID = Convert.ToInt32(cmbOP_APP_VEHICLE_PRIN_OCC_ID.SelectedValue); 

			objDriverDetailInfo.CREATED_BY = int.Parse(GetUserId());
			objDriverDetailInfo.CREATED_DATETIME = DateTime.Now;
			objDriverDetailInfo.MODIFIED_BY = int.Parse(GetUserId());
			objDriverDetailInfo.LAST_UPDATED_DATETIME = DateTime.Now;


			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidDRIVER_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objDriverDetailInfo;
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
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
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
				objDriverDetail = new  ClsDriverDetail();
				objDriverDetail.LoggedInUserId	= int.Parse(GetUserId());
				//Retreiving the form values into model class object
				ClsPolicyDriverInfo objDriverDetailInfo = new ClsPolicyDriverInfo();
				Cms.Model.Policy.Umbrella.ClsDriverOperatorInfo objUmbrellaDriverDetailInfo = new Cms.Model.Policy.Umbrella.ClsDriverOperatorInfo();					
				strRowId		=	hidDRIVER_ID.Value;								   
				if(hidDRIVER_ID.Value.ToUpper().Equals("NEW")) //save case
				{
					
//					if(strCalledFrom.ToUpper()!=CALLED_FROM_UMBRELLA)
//					{
						objDriverDetailInfo = GetFormValue();						
						intRetVal = objDriverDetail.AddPolicyDriverDetails(objDriverDetailInfo, strCalledFrom,hidCustomInfo.Value);
//					}
//					else
//					{
//						objUmbrellaDriverDetailInfo = GetFormValueUmb();
//						intRetVal = objDriverDetail.AddPolicyUmbrellaDriverDetails(objUmbrellaDriverDetailInfo);
//					}


					if(intRetVal>0)
					{
//						if(strCalledFrom.ToUpper()!=CALLED_FROM_UMBRELLA)
							hidDRIVER_ID.Value		= objDriverDetailInfo.DRIVER_ID.ToString();
//						else
//							hidDRIVER_ID.Value		=	objUmbrellaDriverDetailInfo.DRIVER_ID.ToString();
						lblMessage.Text			= ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value		= "1";
						hidIS_ACTIVE.Value		= "Y";

						//Retreiving the old values in old xml
						GetOldDataXML();
						fxnAssignedVehicle();
						SetWorkFlowControl();
						// Cnahged to app effective date from system date time
						int dateDiff=DateTime.Compare(Convert.ToDateTime(PolEffDate.Trim()),Convert.ToDateTime(txtDRIVER_DOB.Text).AddYears(16));
						if(cmbDRIVER_DRIV_TYPE.SelectedValue=="3477"  )
							SetDiaryEntryForSetup("Ex");
							//SetDiaryEntryForExcluded();
						else if(cmbDRIVER_DRIV_TYPE.SelectedValue =="3478" && dateDiff != -1)
							SetDiaryEntryForSetup("Li");
							//SetDiaryEntryNotLiscned();

						//Opening the endorsement details page
						base.OpenEndorsementDetails();
						
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text			=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value		=		"2";
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

					//Updating the record using business layer class object
//					if(strCalledFrom.ToUpper()!=CALLED_FROM_UMBRELLA)
//					{
						//Creating the Model object for holding the Old data
						ClsPolicyDriverInfo objOldDriverDetailInfo = new ClsPolicyDriverInfo();

						//Setting  the Old Page details(XML File containing old details) into the Model Object
						base.PopulateModelObject(objOldDriverDetailInfo,hidOldData.Value);
						objDriverDetailInfo = GetFormValue();
						//Setting those values into the Model object which are not in the page
						objDriverDetailInfo.DRIVER_ID = int.Parse(hidDRIVER_ID.Value);
					
						objDriverDetailInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

					GetOldDataXML();
					strAssignXml = ClsVehicleInformation.FillPolVehicle(int.Parse(hidCUSTOMER_ID.Value)
						, int.Parse(hidPolicyId.Value)
						, int.Parse(hidPolicyVersionId.Value)
						, int.Parse(hidDRIVER_ID.Value==""?"0":hidDRIVER_ID.Value));

					intRetVal	= objDriverDetail.UpdatePolicyDriver(objOldDriverDetailInfo,objDriverDetailInfo,hidCalledFrom.Value,hidCustomInfo.Value,strAssignXml);
					//intRetVal	= objDriverDetail.UpdatePolicyDriver(objOldDriverDetailInfo,objDriverDetailInfo,strCalledFrom,hidCustomInfo.Value);
//					}
//					else
//					{
//						//Creating the Model object for holding the Old data
//						Cms.Model.Policy.Umbrella.ClsDriverOperatorInfo objOldUmbrellaDriverDetailInfo = new Cms.Model.Policy.Umbrella.ClsDriverOperatorInfo();
//
//						//Setting  the Old Page details(XML File containing old details) into the Model Object
//						base.PopulateModelObject(objOldUmbrellaDriverDetailInfo,hidOldData.Value);
//						objUmbrellaDriverDetailInfo = GetFormValueUmb();
//						//Setting those values into the Model object which are not in the page
//						objUmbrellaDriverDetailInfo.DRIVER_ID = int.Parse(hidDRIVER_ID.Value);
//					
//						objUmbrellaDriverDetailInfo.IS_ACTIVE = hidIS_ACTIVE.Value;						
//						intRetVal	= objDriverDetail.UpdatePolicyUmbrellaDriver(objOldUmbrellaDriverDetailInfo,objUmbrellaDriverDetailInfo);
//					}
					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";

						//Get old value of Driver Type						
						string strOldDriverType = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("DRIVER_DRIV_TYPE",hidOldData.Value);
						//Retreiving the old values in old xml
						GetOldDataXML();
						fxnAssignedVehicle();
						SetWorkFlowControl();
						// Changed to App efective date from system date
						//int dateDiff=DateTime.Compare(System.DateTime.Now,Convert.ToDateTime(txtDRIVER_DOB.Text).AddYears(16));
						int dateDiff=DateTime.Compare(Convert.ToDateTime(PolEffDate.Trim()),Convert.ToDateTime(txtDRIVER_DOB.Text).AddYears(16));
						//3478--Not Licensed..3477--Excluded Driver
						if(strOldDriverType!=null && cmbDRIVER_DRIV_TYPE.SelectedValue=="3477" && cmbDRIVER_DRIV_TYPE.SelectedValue != strOldDriverType)
							SetDiaryEntryForSetup("Ex");
							//SetDiaryEntryForExcluded();
						else if(strOldDriverType!=null && cmbDRIVER_DRIV_TYPE.SelectedValue =="3478"  && dateDiff != -1 &&  cmbDRIVER_DRIV_TYPE.SelectedValue != strOldDriverType)
							SetDiaryEntryForSetup("Li");
							//SetDiaryEntryNotLiscned();

						//Opening the endorsement details page
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
				//ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objDriverDetail!= null)
					objDriverDetail.Dispose();
			}
			GetDriverCount();
		}

		private void SetDiaryEntryForExcluded()
		{
			TodolistInfo objTodo=new TodolistInfo();
			if(GetAppID()!="")
				objTodo.APP_ID=int.Parse(GetAppID());
			if(GetAppVersionID() !="")
				objTodo.APP_VERSION_ID =int.Parse(GetAppVersionID());
			objTodo.CUSTOMER_ID =int.Parse(hidCUSTOMER_ID.Value);
			objTodo.POLICY_ID =int.Parse(hidPolicyId.Value);
			objTodo.POLICY_VERSION_ID =int.Parse(hidPolicyVersionId.Value);
			objTodo.LISTTYPEID =12;
			objTodo.TOUSERID = objTodo.CREATED_BY   =int.Parse(GetUserId());			 
			objTodo.CUSTOMER_NAME =GetUserName();
			objTodo.RECDATE =System.DateTime.Now;
			objTodo.PRIORITY ="M";
			objTodo.STARTTIME =System.DateTime.Now;
			objTodo.FOLLOWUPDATE =System.DateTime.Now;
			objTodo.ENDTIME  =  System.DateTime.Now;

			//objTodo.SUBJECTLINE =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("671") + hidDRIVER_ID.Value;
			//objTodo.NOTE        =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("671") + hidDRIVER_ID.Value;
			objTodo.SUBJECTLINE =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("671");
			objTodo.NOTE =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("671") + " Driver Name = " + 
				txtDRIVER_FNAME.Text.Trim() + " " + txtDRIVER_LNAME.Text.Trim() + 
				", Date of Birth = " + txtDRIVER_DOB.Text.Trim() + ", Driver Code = " + txtDRIVER_CODE.Text.Trim();
			Cms.BusinessLayer.BlCommon.ClsDiary objDiary=new Cms.BusinessLayer.BlCommon.ClsDiary();
			try
			{
				int intResult=objDiary.AddPolicyEntry(objTodo);
			}
			catch(Exception ex)
			{
				ExceptionManager.Publish(ex);
				lblMessage.Text			=	ClsMessages.GetMessage(ScreenId,"21")
					+ ex.Message + "\n Try again!";
					
				
			}
			finally
			{
				if(objDiary != null) 
					objDiary.Dispose();
			}
          
          

		}


		/// <summary>
		/// This function is the new implementation of diary changes.
		/// </summary>
		/// <param name="whereString">this parameter is used for identifying whether the call has been made from
		/// ExcludedDriver or Not Licensed driver
		/// </param>
		private void SetDiaryEntryForSetup(string whereString)
		{
			TodolistInfo objTodo=new TodolistInfo();
			if(GetAppID()!="")
				objTodo.APP_ID=int.Parse(GetAppID());
			if(GetAppVersionID() !="")
				objTodo.APP_VERSION_ID =int.Parse(GetAppVersionID());

			objTodo.CUSTOMER_ID =int.Parse(hidCUSTOMER_ID.Value);
			objTodo.POLICY_ID =int.Parse(hidPolicyId.Value);
			objTodo.POLICY_VERSION_ID =int.Parse(hidPolicyVersionId.Value);

			objTodo.LISTTYPEID =(int)ClsDiary.enumDiaryType.FOLLOW_UPS;  
			objTodo.CUSTOMER_NAME =GetUserName();
			objTodo.RECDATE =System.DateTime.Now;
				

			objTodo.MODULE_ID=(int)ClsDiary.enumModuleMaster.POLICY;  
			objTodo.LISTOPEN ="Y";
			objTodo.FROMUSERID = int.Parse(GetUserId());
			objTodo.LOB_ID = int.Parse(GetLOBID()); 

			//if call has been made for excluded driver
			if(whereString.ToUpper().Equals("EX")) 
			{
				objTodo.NOTE =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("671") + " Driver Name = " + 
					txtDRIVER_FNAME.Text.Trim() + " " + txtDRIVER_LNAME.Text.Trim() + 
					", Date of Birth = " + txtDRIVER_DOB.Text.Trim() + ", Driver Code = " + txtDRIVER_CODE.Text.Trim();

				objTodo.SUBJECTLINE =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("671");
			}
				//if call has been made for unlicensed driver
			else if (whereString.ToUpper().Equals("LI"))
			{
				objTodo.NOTE =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("672") + " Driver Name = " + 
					txtDRIVER_FNAME.Text.Trim() + " " + txtDRIVER_LNAME.Text.Trim() + 
					", Date of Birth = " + txtDRIVER_DOB.Text.Trim() + ", Driver Code = " + txtDRIVER_CODE.Text.Trim();

				objTodo.SUBJECTLINE =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("672");
			}
			Cms.BusinessLayer.BlCommon.ClsDiary objDiary=new Cms.BusinessLayer.BlCommon.ClsDiary();
			ArrayList alresult=new ArrayList(); 
			try
			{
				alresult=objDiary.DiaryEntryfromSetup(objTodo);
			}
			catch(Exception ex)
			{
				ExceptionManager.Publish(ex);
				lblMessage.Text			=	ClsMessages.GetMessage(ScreenId,"21")
					+ ex.Message + "\n Try again!";
					
				
			}
			finally
			{
				if(objDiary != null) 
					objDiary.Dispose();
			}
          
          

		}




		private void SetDiaryEntryNotLiscned()
		{
			TodolistInfo objTodo=new TodolistInfo();
			if(GetAppID()!="")
				objTodo.APP_ID=int.Parse(GetAppID());
			if(GetAppVersionID() !="")
				objTodo.APP_VERSION_ID =int.Parse(GetAppVersionID());
			objTodo.CUSTOMER_ID =int.Parse(hidCUSTOMER_ID.Value);
			objTodo.POLICY_ID =int.Parse(hidPolicyId.Value);
			objTodo.POLICY_VERSION_ID =int.Parse(hidPolicyVersionId.Value);
			objTodo.LISTTYPEID =12;
			objTodo.TOUSERID = objTodo.CREATED_BY    =int.Parse(GetUserId());
			objTodo.CUSTOMER_NAME =GetUserName();
			objTodo.RECDATE =System.DateTime.Now;
			objTodo.PRIORITY ="M";
			objTodo.STARTTIME =System.DateTime.Now;
			objTodo.FOLLOWUPDATE =System.DateTime.Now;
			objTodo.ENDTIME  =  System.DateTime.Now;

			//objTodo.SUBJECTLINE =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("672") + hidDRIVER_ID.Value;
			//objTodo.NOTE        =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("672") + hidDRIVER_ID.Value;
			objTodo.SUBJECTLINE =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("672");
			objTodo.NOTE =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("672") + " Driver Name = " + 
				txtDRIVER_FNAME.Text.Trim() + " " + txtDRIVER_LNAME.Text.Trim() + 
				", Date of Birth = " + txtDRIVER_DOB.Text.Trim() + ", Driver Code = " + txtDRIVER_CODE.Text.Trim();
			Cms.BusinessLayer.BlCommon.ClsDiary objDiary=new Cms.BusinessLayer.BlCommon.ClsDiary();
			try
			{
				int intResult=objDiary.AddPolicyEntry(objTodo);
			}
			catch(Exception ex)
			{
				ExceptionManager.Publish(ex);
				lblMessage.Text			=	ClsMessages.GetMessage(ScreenId,"21")
					+ ex.Message + "\n Try again!";
					
				
			}
			finally
			{
				if(objDiary != null) 
					objDiary.Dispose();
			}
          
          

			//Clsdi     objDiary		=	new ClsDiary();

		}


		/// <summary>
		/// Activates and deactivates  .
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				int qresult=0;
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				int intUserID = int.Parse(GetUserId());

				objDriverDetail =  new ClsDriverDetail();

				if(hidIS_ACTIVE.Value.ToString().Trim().ToUpper() == "Y")
				{
					objStuTransactionInfo.transactionDescription = "Deactivated Succesfully.";
					objDriverDetail.TransactionInfoParams = objStuTransactionInfo;
					/// Sumit Chhabra:03/12/2007
					/// Additional parameter strCustomInfo has been added to display driver name and code at t-log
					if(strCalledFrom.ToUpper()!=CALLED_FROM_UMBRELLA)
						qresult  =  objDriverDetail.ActivateDeactivatePolicyDriver(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPolicyId.Value ), int.Parse(hidPolicyVersionId.Value),int.Parse(hidDRIVER_ID.Value), "N",intUserID,hidCustomInfo.Value);
					else
						qresult  =  objDriverDetail.ActivateDeactivateUmbrellaPolicyDriver(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPolicyId.Value ), int.Parse(hidPolicyVersionId.Value),int.Parse(hidDRIVER_ID.Value), "N",intUserID,hidCustomInfo.Value);
					if(qresult>0)
					{
						lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
						hidIS_ACTIVE.Value="N";
						base.OpenEndorsementDetails();
					}
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Activated Succesfully.";
					objDriverDetail.TransactionInfoParams = objStuTransactionInfo;
					/// Sumit Chhabra:03/12/2007
					/// Additional parameter strCustomInfo has been added to display driver name and code at t-log
//					if(strCalledFrom.ToUpper()!=CALLED_FROM_UMBRELLA)
						qresult  =  objDriverDetail.ActivateDeactivatePolicyDriver(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPolicyId.Value ), int.Parse(hidPolicyVersionId.Value),int.Parse(hidDRIVER_ID.Value), "Y",intUserID,hidCustomInfo.Value);
//					else
//						qresult  =  objDriverDetail.ActivateDeactivateUmbrellaPolicyDriver(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPolicyId.Value ), int.Parse(hidPolicyVersionId.Value),int.Parse(hidDRIVER_ID.Value), "Y",intUserID);
					if(qresult>0)
					{
						lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
						hidIS_ACTIVE.Value="Y";
						base.OpenEndorsementDetails();
					}


					
				}
				
				//Opening the endorsement details page
				//hidFormSaved.Value			=	"1";
				hidFormSaved.Value			=	"0";
				
				//Generating the XML again
				GetOldDataXML();			
				fxnAssignedVehicle();

                ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID", "<script>RefreshWebGrid(1," + hidDRIVER_ID.Value + ");</script>");

			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;
				if(objDriverDetail!= null)
					objDriverDetail.Dispose();
			}
		}
		#endregion

		private void SetCaptions()
		{
			capDRIVER_FNAME.Text						=		objResourceMgr.GetString("txtDRIVER_FNAME");
			capDRIVER_MNAME.Text						=		objResourceMgr.GetString("txtDRIVER_MNAME");
			capDRIVER_LNAME.Text						=		objResourceMgr.GetString("txtDRIVER_LNAME");
			capDRIVER_CODE.Text						=		objResourceMgr.GetString("txtDRIVER_CODE");
			capDRIVER_SUFFIX.Text						=		objResourceMgr.GetString("txtDRIVER_SUFFIX");
			capDRIVER_ADD1.Text						=		objResourceMgr.GetString("txtDRIVER_ADD1");
			capDRIVER_ADD2.Text						=		objResourceMgr.GetString("txtDRIVER_ADD2");
			capDRIVER_CITY.Text						=		objResourceMgr.GetString("txtDRIVER_CITY");
			capDRIVER_STATE.Text						=		objResourceMgr.GetString("cmbDRIVER_STATE");
			capDRIVER_ZIP.Text						=		objResourceMgr.GetString("txtDRIVER_ZIP");
			capDRIVER_COUNTRY.Text						=		objResourceMgr.GetString("cmbDRIVER_COUNTRY");
			capDRIVER_HOME_PHONE.Text						=		objResourceMgr.GetString("txtDRIVER_HOME_PHONE");
			capDRIVER_BUSINESS_PHONE.Text						=		objResourceMgr.GetString("txtDRIVER_BUSINESS_PHONE");
			capDRIVER_EXT.Text						=		objResourceMgr.GetString("txtDRIVER_EXT");
			capDRIVER_MOBILE.Text						=		objResourceMgr.GetString("txtDRIVER_MOBILE");
			capDRIVER_DOB.Text						=		objResourceMgr.GetString("txtDRIVER_DOB");
			capDRIVER_SSN.Text						=		objResourceMgr.GetString("txtDRIVER_SSN");
			capDRIVER_MART_STAT.Text						=		objResourceMgr.GetString("cmbDRIVER_MART_STAT");
			capDRIVER_SEX.Text						=		objResourceMgr.GetString("cmbDRIVER_SEX");
			capDRIVER_DRIV_LIC.Text					=		objResourceMgr.GetString("txtDRIVER_DRIV_LIC");
			capDRIVER_LIC_STATE.Text				=		objResourceMgr.GetString("cmbDRIVER_LIC_STATE");
			capDATE_LICENSED.Text					=		objResourceMgr.GetString("txtDATE_LICENSED");
			capDRIVER_DRIV_TYPE.Text				=		objResourceMgr.GetString("cmbDRIVER_DRIV_TYPE");
			capDRIVER_OCC_CLASS.Text				=		objResourceMgr.GetString("cmbDRIVER_OCC_CLASS");
			//capDRIVER_DRIVERLOYER_NAME.Text				=		objResourceMgr.GetString("txtDRIVER_DRIVERLOYER_NAME");//Commented by Sibin for Itrack Issue 5060 on 26 Nov 08
			//capDRIVER_DRIVERLOYER_ADD.Text				=		objResourceMgr.GetString("txtDRIVER_DRIVERLOYER_ADD");//Commented by Sibin for Itrack Issue 5060 on 26 Nov 08
			capDRIVER_INCOME.Text					=		objResourceMgr.GetString("cmbDRIVER_INCOME");
			capDRIVER_PHYS_MED_IMPAIRE.Text			=		objResourceMgr.GetString("cmbDRIVER_PHYS_MED_IMPAIRE");
			capDRIVER_DRINK_VIOLATION.Text			=		objResourceMgr.GetString("cmbDRIVER_DRINK_VIOLATION");
			capDRIVER_STUD_DIST_OVER_HUNDRED.Text	=   	objResourceMgr.GetString("cmbDRIVER_STUD_DIST_OVER_HUNDRED");
			capDRIVER_LIC_SUSPENDED.Text			=		objResourceMgr.GetString("cmbDRIVER_LIC_SUSPENDED");
			capDRIVER_VOLUNTEER_POLICE_FIRE.Text	=		objResourceMgr.GetString("cmbDRIVER_VOLUNTEER_POLICE_FIRE");
			capDRIVER_US_CITIZEN.Text				=		objResourceMgr.GetString("cmbDRIVER_US_CITIZEN");			

			capDRIVER_GOOD_STUDENT.Text						=		objResourceMgr.GetString("cmbDRIVER_GOOD_STUDENT"); 
			capDRIVER_PREF_RISK.Text					=		objResourceMgr.GetString("cmbDRIVER_PREF_RISK");  
			capSAFE_DRIVER.Text						=		objResourceMgr.GetString("cmbSAFE_DRIVER_RENEWAL_DISCOUNT");  

			capVEHICLE_ID.Text						=		objResourceMgr.GetString("cmbVEHICLE_ID"); 
			capNO_DEPENDENTS.Text                   =       objResourceMgr.GetString("cmbNO_DEPENDENTS"); 
			capWAIVER_WORK_LOSS_BENEFITS.Text		=       objResourceMgr.GetString("cmbWAIVER_WORK_LOSS_BENEFITS"); 
			capRELATIONSHIP.Text					=       objResourceMgr.GetString("cmbRELATIONSHIP"); 			
			//Added By Sumit(03/22/2006) For Operator
			this.capOP_VEHICLE_ID.Text =objResourceMgr.GetString("cmbOP_VEHICLE_ID");
			this.capDRIVER_COST_GAURAD_AUX.Text		=objResourceMgr.GetString("cmbOP_DRIVER_COST_GAURAD_AUX");
			this.capOP_APP_VEHICLE_PRIN_OCC_ID.Text =objResourceMgr.GetString("cmbOP_APP_VEHICLE_PRIN_OCC_ID");	
			capEXT_NON_OWN_COVG_INDIVI.Text			=		objResourceMgr.GetString("cmbEXT_NON_OWN_COVG_INDIVI"); 
			capFORM_F95.Text						=		objResourceMgr.GetString("cmbFORM_F95"); 
			capFULL_TIME_STUDENT.Text						=		objResourceMgr.GetString("cmbFULL_TIME_STUDENT"); 
			capSUPPORT_DOCUMENT.Text						=		objResourceMgr.GetString("cmbSUPPORT_DOCUMENT");
			capSIGNED_WAIVER_BENEFITS_FORM.Text						=		objResourceMgr.GetString("cmbSIGNED_WAIVER_BENEFITS_FORM");
			capIN_MILITARY.Text						=		objResourceMgr.GetString("cmbIN_MILITARY");
			capSTATIONED_IN_US_TERR.Text						=		objResourceMgr.GetString("cmbSTATIONED_IN_US_TERR");
			capHAVE_CAR.Text						=		objResourceMgr.GetString("cmbHAVE_CAR");
			capPARENTS_INSURANCE.Text							=		objResourceMgr.GetString("cmbPARENTS_INSURANCE");			
			

			capVIOLATIONS.Text						=		objResourceMgr.GetString("cmbVIOLATIONS");  
			capMVR_ORDERED.Text						=		objResourceMgr.GetString("cmbMVR_ORDERED");  
			capDATE_ORDERED.Text					=		objResourceMgr.GetString("txtDATE_ORDERED");  
			capLOSSREPORT_DATETIME.Text		=		objResourceMgr.GetString("txtLOSSREPORT_DATETIME");
			capLOSSREPORT_ORDER.Text  =objResourceMgr.GetString("cmbLOSSREPORT_ORDER");
		}


	
		//This method is used to fetch the query string values
		private void GetQueryString()
		{
			if (Request["CalledFrom"] != null && Request["CalledFrom"].ToString() != "")
				hidCalledFrom.Value		= Request.Params["CalledFrom"].ToString();
	
			if (Request["CUSTOMER_ID"] != null && Request["CUSTOMER_ID"].ToString() != "")
				hidCUSTOMER_ID.Value	= Request.Params["CUSTOMER_ID"].ToString();

			if (Request["POLICY_ID"] != null && Request["POLICY_ID"].ToString() != "")
				hidPolicyId.Value			= Request.Params["POLICY_ID"].ToString();

			if (Request["POLICY_VERSION_ID"] != null && Request["POLICY_VERSION_ID"].ToString() != "")
				hidPolicyVersionId.Value 	= Request.Params["POLICY_VERSION_ID"].ToString();

			if (Request["DRIVER_ID"] != null && Request["DRIVER_ID"].ToString() != "")
				hidDRIVER_ID.Value		= Request.Params["DRIVER_ID"];
			
		}

		//Used to fetch the old xml
		private void GetOldDataXML()
		{
			if (hidDRIVER_ID.Value != "" && hidCUSTOMER_ID.Value != "")
			{				
				if(strCalledFrom.ToUpper()!=CALLED_FROM_UMBRELLA)
				{
					hidOldData.Value = Cms.BusinessLayer.BlApplication.ClsDriverDetail.GetPolicyDriverDetailsXML(
						int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPolicyId.Value) 
						, int.Parse(hidPolicyVersionId.Value), int.Parse(hidDRIVER_ID.Value));

					DriverAge = ClsCommon.FetchValueFromXML("DRIVER_DOB",hidOldData.Value);
					AppEffcDate =  ClsCommon.FetchValueFromXML("APP_EFFECTIVE_DATE",hidOldData.Value);
				}
				else
				{
					hidOldData.Value = Cms.BusinessLayer.BlApplication.ClsDriverDetail.GetPolicyUmbrellaDriverDetailsXML(
						int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPolicyId.Value) 
						, int.Parse(hidPolicyVersionId.Value), int.Parse(hidDRIVER_ID.Value));

					DriverAge = ClsCommon.FetchValueFromXML("DRIVER_DOB",hidOldData.Value);
				}
				//added by pravesh for encripted SSN number
				if(hidOldData.Value.IndexOf("NewDataSet") >= 0)
				{
					XmlDocument objxml = new XmlDocument();

					objxml.LoadXml(hidOldData.Value);

					XmlNode node = objxml.SelectSingleNode("NewDataSet");
					foreach(XmlNode nodes in node.SelectNodes("Table"))
					{
						XmlNode noder1 = nodes.SelectSingleNode("DRIVER_SSN");
                        if (noder1 != null)
                        {
                            hidSSN_NO.Value = noder1.InnerText;
                            //noder1.InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder1.InnerText);
                            string strSSN_NO = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder1.InnerText);
                            if (strSSN_NO.Trim() != "")//Done for Itrack Issue 6063 on 7 July 09
                            {
                                string strvaln = "xxx-xx-";
                                //for(var len=0; len < document.getElementById('txtSSN_NO').value.length-4; len++)
                                //	txtvaln += 'x';
                                strvaln += strSSN_NO.Substring(strvaln.Length, strSSN_NO.Length - strvaln.Length);
                                capSSN_NO_HID.Text = strvaln;
                            }
                            else
                                capSSN_NO_HID.Text = "";
                        }
                        else
                            capSSN_NO_HID.Text = "";
					}
					objxml = null;
				}
			}
			else
			{
				//If parameters not passed 
				hidOldData.Value = "";
			}

			
			if (hidCUSTOMER_ID.Value != "")
			{
				hidCUSTOMER_INFO.Value=Cms.BusinessLayer.BlApplication.ClsDriverDetail.GetCustomerNameXML(int.Parse(hidCUSTOMER_ID.Value));
			}
            if (ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString() != "")////Added By Pradeep Kushwaha on 10-sep-2010
                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString());
            
		}

		private void SetWorkFlowControl()
		{
			if(base.ScreenId == "228_0" || base.ScreenId == "49_0" || base.ScreenId == "73_0" || base.ScreenId == "84_0" || base.ScreenId == "278_0")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				if (Request.QueryString["DRIVER_ID"]!=null && Request.QueryString["DRIVER_ID"].ToString()!="")
				{
					myWorkFlow.AddKeyValue("DRIVER_ID",Request.QueryString["DRIVER_ID"]);
				}
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}

		/// <summary>
		/// This event is used to delete the driver.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int intRetVal = 0;	
			string strPriorLossDrivers="";//Added for Itrack Issue 5457 on 14 April 2009
			int intCustomerID = int.Parse(hidCUSTOMER_ID.Value);
			int intPolicyId=  int.Parse(hidPolicyId.Value);
			int intPolicyVersionId	= int.Parse(hidPolicyVersionId.Value);
			int intDriverId = int.Parse(hidDRIVER_ID.Value);
			int intUserID = int.Parse(GetUserId());
			string strCalledFrom=hidCalledFrom.Value;
			ClsDriverDetail objDriverDetail = new  ClsDriverDetail();
			strPriorLossDrivers = objDriverDetail.CheckDriverDelete(intCustomerID,intPolicyId,intPolicyVersionId,intDriverId,"POL");//Added for Itrack Issue 5457 on 14 April 2009
			/// Sumit Chhabra:03/12/2007
			/// Additional parameter strCustomInfo has been added to display driver name and code at t-log
//			if(strCalledFrom.ToUpper()!=CALLED_FROM_UMBRELLA)
			if(strPriorLossDrivers == "")//Added condition for Itrack Issue 5457 on 14 April 2009
			{
				intRetVal = objDriverDetail.DeletePolicyDriver(intCustomerID,intPolicyId,intPolicyVersionId,intDriverId, intUserID,hidCustomInfo.Value);
				//			else
				//				intRetVal = objDriverDetail.DeletePolicyUmbrellaDriver(intCustomerID,intPolicyId,intPolicyVersionId,intDriverId, intUserID,hidCustomInfo.Value);
				if(intRetVal>0)
				{
					lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","127");
					hidFormSaved.Value = "5";
					hidOldData.Value = "";
					trBody.Attributes.Add("style","display:none");
					SetWorkFlowControl();
					base.OpenEndorsementDetails();
				}
				else if(intRetVal == -1)
				{
					lblDelete.Text		=	ClsMessages.GetMessage(base.ScreenId,"128");
					hidFormSaved.Value		=	"2";
				}
				lblDelete.Visible = true;
			}
			else
			{
				string []strLossId = strPriorLossDrivers.Split('~');
				lblMessage.Text = "Driver cannot be deleted as it is being used in Prior Loss Details (" + strLossId[0] + "). You can deactivate this driver if required. Deactivating the driver will unassign the prior loss (Loss ID - " + strLossId[1] + ").";
				fxnAssignedVehicle();
			}
		}

		private string CalculateDriverAge(string Date_1 ,string Date_2)
		{
			string ageDiff = ClsCommon.DateDiffAsString(DateTime.Parse(Date_1.Trim()),DateTime.Parse(Date_2.Trim()));
			string[] arrayDate = ageDiff.Split(':');
			return arrayDate[0].ToString();
		}
		
	}
}
