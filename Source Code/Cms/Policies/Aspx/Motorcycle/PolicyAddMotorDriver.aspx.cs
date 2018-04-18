/******************************************************************************************
<Author					: - Vijay Arora
<Start Date				: -	14-11-2005
<End Date				: -	
<Description			: - Class for Add / Edit / Delete Policy Motor Driver.
<Review Date			: - 
<Reviewed By			: - 	

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
using System.Xml;
using System.Globalization;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.CustomException;
using Cms.CmsWeb.WebControls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Application;
using Cms.CmsWeb.Controls;
using Cms.Model.Policy;


namespace Cms.Policies.Aspx.Motorcycle
{
	/// <summary>
	/// Summary description for AddMotorDriverDetails.
	/// </summary>
	public class PolicyAddMotorDriver : Cms.Policies.policiesbase
	{
		#region Page controls declaration
		protected System.Web.UI.WebControls.TextBox txtDRIVER_FNAME;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_MNAME;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_LNAME;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_CODE;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_SUFFIX;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_ADD1;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_ADD2;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_CITY;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbNO_CYCLE_ENDMT;
		protected System.Web.UI.WebControls.Label capNO_CYCLE_ENDMT;		
		protected System.Web.UI.WebControls.Label capAPP_VEHICLE_PRIN_OCC_ID;
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
		//protected System.Web.UI.WebControls.TextBox txtDRIVER_LIC_CLASS;
		//protected System.Web.UI.WebControls.DropDownList cmbDRIVER_OCC_CODE;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_OCC_CLASS;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_INCOME;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_DRIV_TYPE;
		protected System.Web.UI.WebControls.Label capDRIVER_DRIV_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbCYCL_WITH_YOU;
		protected System.Web.UI.WebControls.Label capCYCL_WITH_YOU;
		protected System.Web.UI.WebControls.DropDownList cmbCOLL_STUD_AWAY_HOME;
		protected System.Web.UI.WebControls.Label capCOLL_STUD_AWAY_HOME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDataValue1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDataValue2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomInfo;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidStateID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_EFFECTIVE_DATE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMVRPoints;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_FNAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_LNAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_SEX;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_LIC_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_ZIP;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCYCL_WITH_YOU;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOLL_STUD_AWAY_HOME;
		//Added by Swastika on 28th Feb'06 for Gen Iss # 2366
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDATE_LICENSED;

		protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_CODE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_HOME_PHONE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_MOBILE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_BUSINESS_PHONE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_EXT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_DOB;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_SSN;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_INCOME;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.WebControls.ClientTop clientTop;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label	capSSN_NO_HID; 
		//protected Cms.CmsWeb.Controls.CmsButton btnSSN_NO; 	
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSSN_NO;

		//added by vj on 17-10-2005
		//protected System.Web.UI.WebControls.RequiredFieldValidator  rfvVEHICLE_DRIVER;
		protected System.Web.UI.WebControls.DropDownList cmbAPP_VEHICLE_PRIN_OCC_ID;
		protected string MOTOR_DRIVER_OPERATES_CYCLE = "11941";

		protected System.Web.UI.WebControls.Label capVIOLATIONS;
		protected System.Web.UI.WebControls.DropDownList cmbVIOLATIONS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVIOLATIONS;
		protected System.Web.UI.WebControls.Label capMVR_ORDERED;
		protected System.Web.UI.WebControls.DropDownList cmbMVR_ORDERED;
		protected System.Web.UI.WebControls.Label capDATE_ORDERED;
		protected System.Web.UI.WebControls.TextBox txtDATE_ORDERED;
		protected System.Web.UI.WebControls.Table tblAssignedVeh;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSeletedData;
		protected System.Web.UI.WebControls.Image imgZipLookup;
		protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
		public string strAssignXml;
		#endregion

		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		public string strLicenceLimit="3";
		public string PolEffDate="";
		//creating resource manager object (used for reading field and label mapping)
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
		//protected System.Web.UI.WebControls.Label capDRIVER_LIC_CLASS;
		//protected System.Web.UI.WebControls.Label capDRIVER_OCC_CODE;
		protected System.Web.UI.WebControls.Label capDRIVER_OCC_CLASS;
		protected System.Web.UI.WebControls.Label capDRIVER_INCOME;
		//protected System.Web.UI.WebControls.Label capDRIVER_BROADEND_NOFAULT;
		protected System.Web.UI.WebControls.Label capDRIVER_DRINK_VIOLATION;
		//protected System.Web.UI.WebControls.Label capDRIVER_LIC_SUSPENDED;
		protected System.Web.UI.WebControls.Label capDRIVER_US_CITIZEN;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDRIVER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppVersionId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		//protected System.Web.UI.WebControls.Label capDRIVER_FAX;
		//protected System.Web.UI.WebControls.TextBox txtDRIVER_FAX;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_FAX;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_ADD1; - Commented by Sibin on 28 Nov 08 for Itrack Issue 5061
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_CITY; -Commented by Sibin for Itrack Issue 5061
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_DOB;
		protected System.Web.UI.WebControls.HyperLink hlkDRIVER_DOB;
		protected System.Web.UI.WebControls.Label capRELATIONSHIP;
		protected System.Web.UI.WebControls.DropDownList cmbRELATIONSHIP;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvRELATIONSHIP; -Commented by Sibin for Itrack Issue 5061
		protected Cms.CmsWeb.Controls.CmsButton btnPullCustomerAddress;
		protected System.Web.UI.WebControls.Label Label1;
		protected Cms.CmsWeb.Controls.CmsButton btnCopyDefaultCustomer;
		protected Cms.CmsWeb.Controls.CmsButton btnCopyAppDrivers;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_INFO;
		protected System.Web.UI.WebControls.CustomValidator csvDRIVER_DOB;
		//protected System.Web.UI.WebControls.CustomValidator csvDATE_EXP_DOB;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnDATE_EXP_DOB;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_DRINK_VIOLATION;
		//protected System.Web.UI.WebControls.DropDownList cmbDRIVER_LIC_SUSPENDED;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_US_CITIZEN;
		//protected System.Web.UI.WebControls.DropDownList cmbDRIVER_BROADEND_NOFAULT;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;

		//Defining the business layer class object
		ClsDriverDetail  objDriverDetail ;
		
		protected System.Web.UI.WebControls.Label capSAFE_DRIVER;
		protected System.Web.UI.WebControls.CheckBox chkSAFE_DRIVER;
		protected System.Web.UI.WebControls.Label capSafeDriver;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.CheckBox chkDRIVER_GOOD_STUDENT;
		protected System.Web.UI.WebControls.Label capGoodStudent;
		protected System.Web.UI.WebControls.Label capDRIVER_PREF_RISK;
		protected System.Web.UI.WebControls.CheckBox chkDRIVER_PREF_RISK;
		protected System.Web.UI.WebControls.Label capPremierDriver;
		protected System.Web.UI.WebControls.CustomValidator csvDISC_TYPE;
		protected System.Web.UI.WebControls.Label lblVehicleMsg;
		protected System.Web.UI.WebControls.Label capVEHICLE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbVEHICLE_ID;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvVEHICLE_ID;
		protected System.Web.UI.WebControls.Label capPERCENT_DRIVEN;
		protected System.Web.UI.HtmlControls.HtmlTableRow trVehMsg;
		protected System.Web.UI.HtmlControls.HtmlTableRow trVehField;
		protected System.Web.UI.WebControls.Label capMatureDriver;
		//protected System.Web.UI.WebControls.Label capPreferedRisk;
		
		// Commented by Charles on 2-Jul-09 for Itrack issue 6012
		//protected System.Web.UI.WebControls.Label capTransferExperienceRenewalCredit;
		
		/* Commented by Charles on 2-Jul-09 for Itrack issue 6012
		// Added by Charles on 5-Jun-2009 for Itrack issue 5744
		public int intYearsWithWolverine;
		public int intYearsContInsured;
		//Added Till Here
		*/

		public string calledFrom="";
		//protected System.Web.UI.WebControls.DropDownList cmbMATURE_DRIVER;     
		//protected System.Web.UI.WebControls.DropDownList cmbPREFERRED_RISK;  
		
		// Commented by Charles on 2-Jul-09 for Itrack issue 6012
		//protected System.Web.UI.WebControls.DropDownList cmbTRANSFEREXPERIENCE_RENEWALCREDIT; 
		
		protected System.Web.UI.WebControls.Label capMATURE_DRIVER;     
		//protected System.Web.UI.WebControls.Label capPREFERRED_RISK;
		protected System.Web.UI.WebControls.TextBox txtDATE_LICENSED;
		protected System.Web.UI.WebControls.HyperLink hlkDATE_LICENSED;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDATE_LICENSED;
		protected System.Web.UI.WebControls.CustomValidator csvDATE_LICENSED;
		protected System.Web.UI.WebControls.Label capDATE_LICENSED;
		protected System.Web.UI.WebControls.Label capPREFERRED_RISK;
		protected System.Web.UI.WebControls.Label capPreferedRisk;
		protected System.Web.UI.WebControls.HyperLink hlkDATE_ORDERED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDATE_ORDERED;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDATE_ORDERED;
		protected System.Web.UI.WebControls.CustomValidator csvDATE_ORDERED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVEHICLE_ID;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_DRIV_LIC;
		protected System.Web.UI.WebControls.Label capMVR_CLASS;
		protected System.Web.UI.WebControls.TextBox txtMVR_CLASS;
		protected System.Web.UI.WebControls.Label capMVR_LIC_CLASS;
		protected System.Web.UI.WebControls.TextBox txtMVR_LIC_CLASS;
		protected System.Web.UI.WebControls.Label capMVR_LIC_RESTR;
		protected System.Web.UI.WebControls.TextBox txtMVR_LIC_RESTR;
		protected System.Web.UI.WebControls.Label capMVR_DRIV_LIC_APPL;
		protected System.Web.UI.WebControls.TextBox txtMVR_DRIV_LIC_APPL;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDRIVER_DRIV_LIC;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDRIVER_DRIV_LIC_SELINDEX;		
		
		//Commented by Charles on 2-Jul-2009 for Itrack 6012
		//protected System.Web.UI.WebControls.Label capTRANSFEREXPERIENCE_RENEWALCREDIT; 
		
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


		//Added by Sibin on 28 Nov 08 for Itrack Issue 5061
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_DRIV_LIC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_US_CITIZEN;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_DRINK_VIOLATION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvNO_CYCLE_ENDMT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_DRIV_TYPE;
		protected System.Web.UI.WebControls.Label lblViolationPoints;
		protected System.Web.UI.WebControls.Label capViolationPoints;
		protected System.Web.UI.WebControls.Label lblAccidentPoints;
		protected System.Web.UI.WebControls.Label capAccidentPoints;
		protected System.Web.UI.HtmlControls.HtmlTableRow trViolationMsg;
		protected System.Web.UI.HtmlControls.HtmlTableRow trViolationField;
		protected System.Web.UI.HtmlControls.HtmlTableRow trViolationSec;
		protected System.Web.UI.WebControls.Label lblViolationMsg;//Sibin
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
			rfvDRIVER_SEX.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"189");
			rfvDRIVER_LIC_STATE.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"111");
			//rfvRELATIONSHIP.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"402"); - Commented by Sibin for Itrack Issue 5061
			rfvDRIVER_ZIP.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"37");
			//rfvDRIVER_ADD1.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"787"); - Commented by Sibin on 28 Nov 08 for Itrack Issue 5061
			//rfvDRIVER_CITY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"56"); - Commented by Sibin on 28 Nov 08 for Itrack Issue 5061				
			rfvDRIVER_DOB.ErrorMessage =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"162");

			rfvDRIVER_DRIV_LIC.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"527"); // Added by Sibin on 28 Nov 08 for Itrack Issue 5061
			rfvDRIVER_US_CITIZEN.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"531"); // Added by Sibin on 28 Nov 08 for Itrack Issue 5061
			rfvDRIVER_DRINK_VIOLATION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"528"); // Added by Sibin on 28 Nov 08 for Itrack Issue 5061
			rfvNO_CYCLE_ENDMT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1"); // Added by Sibin on 28 Nov 08 for Itrack Issue 5061
			rfvDRIVER_DRIV_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1052"); // Added by Manoj Rathore itrack # 5881
			
			revDRIVER_CODE.ValidationExpression			= aRegExpClientName	;
			revDRIVER_ZIP.ValidationExpression			= aRegExpZip;
			revDRIVER_HOME_PHONE.ValidationExpression	= aRegExpPhone	;
			revDRIVER_MOBILE.ValidationExpression		= aRegExpPhone;
			revDRIVER_BUSINESS_PHONE.ValidationExpression=aRegExpPhone;
			revDRIVER_EXT.ValidationExpression			= aRegExpExtn;
			//revDRIVER_FAX.ValidationExpression			= aRegExpFax;
			revDRIVER_DOB.ValidationExpression			= aRegExpDate;
			revDRIVER_SSN.ValidationExpression			= aRegExpSSN;
			revDATE_LICENSED.ValidationExpression		= aRegExpDate;
			revDATE_ORDERED.ValidationExpression		=	aRegExpDate;

			revDRIVER_CODE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"64");
			revDRIVER_ZIP.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"24");
			revDRIVER_HOME_PHONE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"14");
			revDRIVER_MOBILE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"16");
			revDRIVER_BUSINESS_PHONE.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"14");
			revDRIVER_EXT.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"25");
			//revDRIVER_FAX.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"15");
			revDRIVER_DOB.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"22");
			revDRIVER_SSN.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"130");
			revDATE_LICENSED.ErrorMessage				=  "<br>" + Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"22");

			csvDATE_LICENSED.ErrorMessage				=  "<br>" + Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"199");
			csvDRIVER_DOB.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"496");
			//csvDATE_EXP_DOB.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("225");
			spnDATE_EXP_DOB.InnerHtml		=	"<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("225");

//			rfvVEHICLE_ID.ErrorMessage              = ClsMessages.FetchGeneralMessage("607");
//			rfvVEHICLE_DRIVER.ErrorMessage         = ClsMessages.FetchGeneralMessage("593");
			//Added by Swastika on 28th Feb'06 for Gen Iss # 2366
			rfvDATE_LICENSED.ErrorMessage				= "<br>" + Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"710");
			rfvCYCL_WITH_YOU.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"852");
			rfvCOLL_STUD_AWAY_HOME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"851");
			revLOSSREPORT_DATETIME.ValidationExpression		=	aRegExpDate;
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
		private void getPolicyEffectiveYear()
		{	
			PolEffDate  = new Cms.BusinessLayer.BlApplication.clsWatercraftInformation().GetPolEffectiveDate(int.Parse(GetCustomerID()),int.Parse(GetPolicyID()),int.Parse(GetPolicyVersionID()));			
		}
 
		/* Commented by Charles on 2-Jul-09 for Itrack issue 6012
		//Method Added by Charles for Itrack Issue 5744 on 5-Jun-2009
		private void getPolYearsWolvContInsured()
		{	
			DataSet dsTemp=new DataSet();
			dsTemp= new Cms.BusinessLayer.BlApplication.ClsDriverDetail().GetPolYearsWolvYearsContIns(Convert.ToInt16(GetCustomerID()),Convert.ToInt16(GetPolicyID()),Convert.ToInt16(GetPolicyVersionID()));
			if(dsTemp.Tables[0].Rows.Count>0)
			{
				intYearsWithWolverine=Convert.ToInt16(dsTemp.Tables[0].Rows[0]["YEARSCONTINSUREDWITHWOLVERINE"]);
				intYearsContInsured=Convert.ToInt16(dsTemp.Tables[0].Rows[0]["YEARSCONTINSURED"]);	
			}
		}
		*/

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			hidCalledFrom.Value = Request.Params["CalledFrom"];
			switch(hidCalledFrom.Value.ToUpper())
			{
				
				case "MOT" :
					base.ScreenId	=	"237_0";
					break;
				case "UMB" :
					base.ScreenId	=	"84_0";
					break;
				default:
					base.ScreenId	=	"237_0";
					break;
			}
			getPolicyEffectiveYear();
			
			// Commented by Charles on 2-Jul-09 for Itrack issue 6012
			//getPolYearsWolvContInsured();

			calledFrom		=	hidCalledFrom.Value.ToUpper();

			//lblMessage.Visible = false;
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass		=	CmsButtonType.Write;
			btnReset.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass		=	CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;

			btnPullCustomerAddress.CmsButtonClass	=	CmsButtonType.Write;
			btnPullCustomerAddress.PermissionString	=	gstrSecurityXML;

			btnCopyAppDrivers.CmsButtonClass	=	CmsButtonType.Read;
			btnCopyAppDrivers.PermissionString	=	gstrSecurityXML;
			
			btnCopyDefaultCustomer.CmsButtonClass	=	CmsButtonType.Write;
			btnCopyDefaultCustomer.PermissionString	=	gstrSecurityXML;

			btnDelete.CmsButtonClass	=	CmsButtonType.Delete;
			btnDelete.PermissionString =	gstrSecurityXML;
		
			
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			
			base.RequiredPullCustAdd(txtDRIVER_ADD1, txtDRIVER_ADD2
				, txtDRIVER_CITY, cmbDRIVER_COUNTRY, cmbDRIVER_STATE
				, txtDRIVER_ZIP, btnPullCustomerAddress);

			// Added by Swarup on 05-apr-2007
			imgZipLookup.Attributes.Add("style","cursor:hand");
			base.VerifyAddress(hlkZipLookup, txtDRIVER_ADD1,txtDRIVER_ADD2
				, txtDRIVER_CITY, cmbDRIVER_STATE, txtDRIVER_ZIP);
			
			hlkLOSSREPORT_DATETIME.Attributes.Add("OnClick","fPopCalendar(document.APP_DRIVER_DETAILS.txtLOSSREPORT_DATETIME,document.APP_DRIVER_DETAILS.txtLOSSREPORT_DATETIME)");  	
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.Motorcycle.PolicyAddMotorDriver" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				SetControlsAttributes();
				SetErrorMessages();
				SetAttribute();	
				GetQueryString();
				GetOldDataXML();
				SetCaptions();

                string strSysID = GetSystemId();
                if (strSysID == "ALBAUAT")
                    strSysID = "ALBA";

                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "/Policies/support/PageXML/" + strSysID, "PolicyAddMotorDriver.xml"))
                    setPageControls(Page, Request.PhysicalApplicationPath + "/Policies/support/PageXml/" + strSysID + "/PolicyAddMotorDriver.xml");

				GetPolicyInfo();
				PopulateComboBox();
				fxnAssignedVehicle();
				SetWorkflow();
                FillDiscountSectionDropDownList();
                //FillRiskDiscount();
                //SetRiskLabelPercentage();
				//SetLabelPercentage();
			}

			//Added by Sibin for Itrack Issue 5061 
                if (hidCalledFrom.Value.ToUpper() != "UMB")
                    GetViolationPoints();
			//SetLicErrorMessage();
		}//end pageload
		#endregion

		#region FillDiscountSectionDropDownList
		private void FillDiscountSectionDropDownList()
		{
		
			/*cmbMATURE_DRIVER.Items.Insert(0,"No"); 
			cmbMATURE_DRIVER.Items.Insert(1,"Yes"); 
			cmbMATURE_DRIVER.Items[0].Value = "0";
			cmbMATURE_DRIVER.Items[1].Value = "1";
			*/
			/*The following control is being removed and instead discount will be shown similar to application:Sumit Chhabra
			cmbPREFERRED_RISK.Items.Insert(0,"No"); 
			cmbPREFERRED_RISK.Items.Insert(1,"Yes"); 
			cmbPREFERRED_RISK.Items[0].Value = "0";
			cmbPREFERRED_RISK.Items[1].Value = "1";*/

			/* Commented by Charles on 2-Jul-09 for Itrack issue 6012
				cmbTRANSFEREXPERIENCE_RENEWALCREDIT.Items.Insert(0,"No"); 
				cmbTRANSFEREXPERIENCE_RENEWALCREDIT.Items.Insert(1,"Yes"); 
				cmbTRANSFEREXPERIENCE_RENEWALCREDIT.Items[0].Value = "0";
				cmbTRANSFEREXPERIENCE_RENEWALCREDIT.Items[1].Value = "1";
			
				//Added by Charles on 24-Jun-2009 for Itrack 6003
				cmbTRANSFEREXPERIENCE_RENEWALCREDIT.SelectedIndex=1;

			*/
		
		}
		#endregion

		 //Added By Sibin for Itrack Issue 5061 on 28 Nov 08

		#region Violation Points

		
		private void GetViolationPoints()
		{
			Cms.BusinessLayer.BlCommon.ClsQuickQuote objQuickQuote = new Cms.BusinessLayer.BlCommon.ClsQuickQuote();
			if(hidDRIVER_ID.Value!="" && hidDRIVER_ID.Value!="0" && hidDRIVER_ID.Value.ToUpper()!="NEW")
			{
				DataTable dtTemp = objQuickQuote.GetMVRPointsForSurcharge(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value),int.Parse(hidDRIVER_ID.Value),"POL");
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
                    trViolationMsg.Attributes.Add("style", "display:inline");
                    lblViolationMsg.Text = "No violations added";
                    //if (GetSystemId() != "S001" && GetSystemId() != "SUAT")
                    //{
                    //    trViolationMsg.Attributes.Add("style", "display:inline");
                    //    lblViolationMsg.Text = "No violations added";
                    //}
				}				
			}
			else
			{
				trViolationField.Attributes.Add("style","display:none");
				trViolationSec.Attributes.Add("style","display:none");
                trViolationMsg.Attributes.Add("style", "display:inline");
                lblViolationMsg.Text = "No violations added";
                //if (GetSystemId() != "S001" && GetSystemId() != "SUAT")
                //{
                //    trViolationMsg.Attributes.Add("style", "display:inline");
                //    lblViolationMsg.Text = "No violations added";
                //}
			}
		}


		#endregion


		private void SetAttribute()
		{
			//cmbMATURE_DRIVER.Attributes.Add("onchange","ShowDiscountPercentage('" + cmbMATURE_DRIVER.ClientID + "','" + capMatureDriver.ClientID + "')");
			//cmbPREFERRED_RISK.Attributes.Add("onchange","ShowDiscountPercentage('" + cmbPREFERRED_RISK.ClientID + "','" + capPreferedRisk.ClientID + "')");
			
			// Commented by Charles on 2-Jul-09 for Itrack issue 6012
			//cmbTRANSFEREXPERIENCE_RENEWALCREDIT.Attributes.Add("onchange","ShowDiscountPercentage('" + cmbTRANSFEREXPERIENCE_RENEWALCREDIT.ClientID + "','" + capTransferExperienceRenewalCredit.ClientID + "')");
			
			//cmbTRANSFEREXPERIENCE_RENEWALCREDIT.Attributes.Add("onchange","ShowDiscountPercentage()");
			//cmbMATURE_DRIVER.Attributes.Add("onchange","ShowDiscountPercentage()");
			cmbCOLL_STUD_AWAY_HOME.Attributes.Add("onChange","javascript:cmbCOLL_STUD_AWAY_HOME_Change();");
		}
		
		private void SetLabelPercentage()
		{
			//if (cmbTRANSFEREXPERIENCE_RENEWALCREDIT.Items.ToString()!="1")
			//{
			string strFilePath = GetMasterDataPathForDiscount();
			XmlDocument xDoc = new XmlDocument();
			xDoc.Load(strFilePath);
			XmlNodeList xNodeList=xDoc.SelectNodes("PRODUCTMASTER/PRODUCT/FACTOR[@ID='MATUREOPERATOR']/NODE[@ID='MATUREOPERATOR_CREDIT']/ATTRIBUTES/@CREDIT");
			capMatureDriver.Text = "  (" + xNodeList[0].InnerText + "%)";
			//capPreferedRisk.Text = " - (20%)";
			
			/* Commented by Charles on 2-Jul-09 for Itrack 6012
			xNodeList=xDoc.SelectNodes("PRODUCTMASTER/PRODUCT/FACTOR[@ID='TRANSFER_RENEWAL']/NODE[@ID='TRANSFER_RENEWAL_DISCOUNT']/ATTRIBUTES/@CREDIT");
			capTransferExperienceRenewalCredit.Text = "  (" + xNodeList[0].InnerText + "%)";
			*/
		
			//	}

			
		}
		
		private void SetControlsAttributes()
		{
			btnReset.Attributes.Add("onclick","javascript:return ResetForm1('" + Page.Controls[0].ID + "' );");
			hlkDRIVER_DOB.Attributes.Add("OnClick","fPopCalendar(document.APP_DRIVER_DETAILS.txtDRIVER_DOB, document.APP_DRIVER_DETAILS.txtDRIVER_DOB)");
			hlkDATE_LICENSED.Attributes.Add("OnClick","fPopCalendar(document.APP_DRIVER_DETAILS.txtDATE_LICENSED, document.APP_DRIVER_DETAILS.txtDATE_LICENSED)");
			btnCopyDefaultCustomer.Attributes.Add("onclick","javascript:return FillCustomerName();");
			btnCopyAppDrivers.Attributes.Add("OnClick","javascript:return CoypApplicationDrivers();");
			txtDRIVER_BUSINESS_PHONE.Attributes.Add("onBlur","javascript:return CheckIfPhoneEmpty();");
			txtDRIVER_FNAME.Attributes.Add("onBlur","javascript:return GenerateDriverCode(\"txtDRIVER_FNAME\");");
			txtDRIVER_LNAME.Attributes.Add("onBlur","javascript:return GenerateDriverCode(\"txtDRIVER_LNAME\");");
			txtDRIVER_BUSINESS_PHONE.Attributes.Add("onBlur","javascript:return CheckIfPhoneEmpty();");
			
			/* Added javacript functions EnableDisableMatureDiscount();EnableDisableRiskDiscount();CollegeStudent();ShowDiscountPercentage();
			 * for txtDRIVER_DOB for Itrack issue 5744 on 23-April-09
			*/ // EnableDisableTransferExperienceRenewalCredit(); removed by Charles on 2-Jul-2009 for Itrack issue 6012
			txtDRIVER_DOB.Attributes.Add("onBlur","javascript:CompareExpDateWithDOB();EnableDisableMatureDiscount();EnableDisableRiskDiscount();CollegeStudent();ShowDiscountPercentage();");
			
			//Added by Charles on 6-May-2009 for Itrack Issue 5744
			txtDRIVER_DOB.Attributes.Add("onkeyup","javascript:if(event.keyCode==13)CollegeStudent();");

			//EnableDisableTransferExperienceRenewalCredit(); removed by Charles on 2-Jul-2009 for Itrack issue 6012
			txtDATE_LICENSED.Attributes.Add("onBlur","javascript:EnableDisableRiskDiscount();CompareExpDateWithDOB();");
			
			//btnSave.Attributes.Add("onClick","javascript : Page_ClientValidate();CompareExpDateWithDOB();return Page_IsValid;");
			//Added by Manoj Rathore Itrack # 5881 on 26 may 2009
			//cmbDRIVER_DRIV_TYPE.Attributes.Add("onChange","javascript:CollegeStudent();");
            //cmbDRIVER_DRIV_TYPE.Attributes.Add("onChange","javascript:CollegeStudent();setDriv_Lic_Rfv();");//Added by Sibin for Itrack Issue 5061 on 28 Nov 08
			btnSave.Attributes.Add("onClick","javascript:setDriv_Lic_Rfv();return SaveClientSide();");//Added setDriv_Lic_Rfv() by Sibin for Itrack Issue 5061 on 28 Nov 08
			hlkDATE_ORDERED.Attributes.Add("OnClick","fPopCalendar(document.APP_DRIVER_DETAILS.txtDATE_ORDERED, document.APP_DRIVER_DETAILS.txtDATE_ORDERED)");
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

			 
			cmbDRIVER_STATE.DataSource		= Cms.CmsWeb.ClsFetcher.ActiveState ;
			cmbDRIVER_STATE.DataTextField	= "State_Name";
			cmbDRIVER_STATE.DataValueField	= "State_Id";
			cmbDRIVER_STATE.DataBind();

			cmbDRIVER_LIC_STATE.DataSource = Cms.CmsWeb.ClsFetcher.State;
			cmbDRIVER_LIC_STATE.DataTextField	= "State_Name";
			cmbDRIVER_LIC_STATE.DataValueField	= "State_Id";
			cmbDRIVER_LIC_STATE.DataBind();
			cmbDRIVER_LIC_STATE.Items.Insert(0,"");

			/*cmbDRIVER_BROADEND_NOFAULT.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbDRIVER_BROADEND_NOFAULT.DataTextField="LookupDesc"; 
			cmbDRIVER_BROADEND_NOFAULT.DataValueField="LookupCode";
			cmbDRIVER_BROADEND_NOFAULT.DataBind();
			cmbDRIVER_BROADEND_NOFAULT.Items.Insert(0,"");*/

			cmbNO_CYCLE_ENDMT.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbNO_CYCLE_ENDMT.DataTextField="LookupDesc"; 
			cmbNO_CYCLE_ENDMT.DataValueField="LookupCode";
			cmbNO_CYCLE_ENDMT.DataBind();
			cmbNO_CYCLE_ENDMT.Items.Insert(0,"");

			cmbCYCL_WITH_YOU.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbCYCL_WITH_YOU.DataTextField="LookupDesc"; 
			cmbCYCL_WITH_YOU.DataValueField="LookupCode";
			cmbCYCL_WITH_YOU.DataBind();
			cmbCYCL_WITH_YOU.Items.Insert(0,"");

			cmbCOLL_STUD_AWAY_HOME.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbCOLL_STUD_AWAY_HOME.DataTextField="LookupDesc"; 
			cmbCOLL_STUD_AWAY_HOME.DataValueField="LookupCode";
			cmbCOLL_STUD_AWAY_HOME.DataBind();
			cmbCOLL_STUD_AWAY_HOME.Items.Insert(0,"");

			cmbDRIVER_DRINK_VIOLATION.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbDRIVER_DRINK_VIOLATION.DataTextField="LookupDesc"; 
			cmbDRIVER_DRINK_VIOLATION.DataValueField="LookupCode";
			cmbDRIVER_DRINK_VIOLATION.DataBind();
			cmbDRIVER_DRINK_VIOLATION.Items.Insert(0,"");

			/*cmbDRIVER_LIC_SUSPENDED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbDRIVER_LIC_SUSPENDED.DataTextField="LookupDesc"; 
			cmbDRIVER_LIC_SUSPENDED.DataValueField="LookupCode";
			cmbDRIVER_LIC_SUSPENDED.DataBind();
			cmbDRIVER_LIC_SUSPENDED.Items.Insert(0,"");*/
            if (GetSystemId().ToUpper() != "S001" && GetSystemId().ToUpper() != "SUAT")
            {
                cmbDRIVER_US_CITIZEN.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
                cmbDRIVER_US_CITIZEN.DataTextField = "LookupDesc";
                cmbDRIVER_US_CITIZEN.DataValueField = "LookupCode";
                cmbDRIVER_US_CITIZEN.DataBind();
                cmbDRIVER_US_CITIZEN.Items.Insert(0, "");
            }

			#endregion//Loading singleton

			
			//Populating the lob
			
			cmbRELATIONSHIP.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("DRACD");
			cmbRELATIONSHIP.DataTextField = "LookupDesc";
			cmbRELATIONSHIP.DataValueField = "LookupID";
			cmbRELATIONSHIP.DataBind();
			cmbRELATIONSHIP.Items.Insert(0,"");

			//Populating the Occupation code
			/*cmbDRIVER_OCC_CODE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("%OCC");
			cmbDRIVER_OCC_CODE.DataTextField = "LookupDesc";
			cmbDRIVER_OCC_CODE.DataValueField = "LookupID";
			cmbDRIVER_OCC_CODE.DataBind();*/

			cmbDRIVER_OCC_CLASS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("%OCC"); //OCCCL changed to %OCC for Itrack Issue 5061 on 28 Nov 08
			cmbDRIVER_OCC_CLASS.DataTextField = "LookupDesc";
			cmbDRIVER_OCC_CLASS.DataValueField = "LookupID";
			cmbDRIVER_OCC_CLASS.DataBind();
			cmbDRIVER_OCC_CLASS.Items.Insert(0,new ListItem("",""));
			cmbDRIVER_OCC_CLASS.SelectedIndex=0;

			cmbDRIVER_MART_STAT.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("MARST");
			cmbDRIVER_MART_STAT.DataTextField = "LookupDesc";
			cmbDRIVER_MART_STAT.DataValueField = "LookupCode";
			cmbDRIVER_MART_STAT.DataBind();
			cmbDRIVER_MART_STAT.Items.Insert(0,"");

			cmbAPP_VEHICLE_PRIN_OCC_ID.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("VHIDPO");
			cmbAPP_VEHICLE_PRIN_OCC_ID.DataTextField = "LookupDesc";
			cmbAPP_VEHICLE_PRIN_OCC_ID.DataValueField = "LookupID";
			cmbAPP_VEHICLE_PRIN_OCC_ID.DataBind();
			cmbAPP_VEHICLE_PRIN_OCC_ID.Items.Insert(0,"");

			ClsVehicleInformation.FillPolicyMotorDriverVehicleInfo(cmbVEHICLE_ID
				, int.Parse(hidCUSTOMER_ID.Value)
				, int.Parse(hidPolicyID.Value)
				, int.Parse(hidPolicyVersionID.Value));
			

//			if(cmbVEHICLE_ID.Items.Count<1)
//			{
//				lblVehicleMsg.Text	=  "No motorcycle added until now. Please click <a href='#' onclick='Redirect();'>here</a> to add motorcycle. ";
//				trVehMsg.Attributes.Add ("style","display:inline;"); 
//				trVehField.Attributes.Add ("style","display:none;"); 
////				rfvVEHICLE_ID.Enabled=false;
//			}
//			else
//			{
//				cmbVEHICLE_ID.Items.Insert(0,"");
//				trVehMsg.Attributes.Add ("style","display:none;"); 
//				trVehField.Attributes.Add ("style","display:inline;"); 
////				rfvVEHICLE_ID.Enabled=true;
//			}
			cmbDRIVER_DRIV_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("MTD");
			cmbDRIVER_DRIV_TYPE.DataTextField = "LookupDesc";
			cmbDRIVER_DRIV_TYPE.DataValueField = "LookupID";
			cmbDRIVER_DRIV_TYPE.DataBind();
			cmbDRIVER_DRIV_TYPE.Items.Insert(0,new ListItem("",""));

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

			//Added by Mohit Agarwal 30-Oct-2007
			cmbLOSSREPORT_ORDER.DataSource		= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbLOSSREPORT_ORDER.DataTextField	= "LookupDesc";
			cmbLOSSREPORT_ORDER.DataValueField	= "LookupID";
			cmbLOSSREPORT_ORDER.DataBind();
			cmbLOSSREPORT_ORDER.Items.Insert(0,"");
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
			
			objDriverDetailInfo.POLICY_ID = int.Parse(hidPolicyID.Value);
			objDriverDetailInfo.POLICY_VERSION_ID = int.Parse(hidPolicyVersionID.Value) ;
			objDriverDetailInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);

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

			//objDriverDetailInfo.DRIVER_FAX = txtDRIVER_FAX.Text;
			objDriverDetailInfo.DRIVER_COUNTRY = cmbDRIVER_COUNTRY.SelectedValue;
			objDriverDetailInfo.DRIVER_HOME_PHONE = txtDRIVER_HOME_PHONE.Text;
			objDriverDetailInfo.DRIVER_BUSINESS_PHONE =	txtDRIVER_BUSINESS_PHONE.Text;
			objDriverDetailInfo.DRIVER_EXT = txtDRIVER_EXT.Text;
			objDriverDetailInfo.DRIVER_MOBILE = txtDRIVER_MOBILE.Text;
			if(cmbDRIVER_DRINK_VIOLATION.SelectedItem!=null && cmbDRIVER_DRINK_VIOLATION.SelectedItem.Value!="")
				objDriverDetailInfo.DRIVER_DRINK_VIOLATION=cmbDRIVER_DRINK_VIOLATION.SelectedValue;
			//objDriverDetailInfo.DRIVER_LIC_SUSPENDED=cmbDRIVER_LIC_SUSPENDED.SelectedValue;

			//Added by Sibin for Itrack Issue 5061 on 28 Nov 2008
			if(cmbDRIVER_DRIV_TYPE.SelectedItem!=null && cmbDRIVER_DRIV_TYPE.SelectedItem.Value!="" && cmbDRIVER_DRIV_TYPE.SelectedValue=="11942")
			{  
				objDriverDetailInfo.DRIVER_DRIV_TYPE = cmbDRIVER_DRIV_TYPE.SelectedItem.Value;
				rfvDRIVER_DRIV_LIC.Enabled=false;
			}

			else
			{
				objDriverDetailInfo.DRIVER_DRIV_TYPE = cmbDRIVER_DRIV_TYPE.SelectedItem.Value;
				rfvDRIVER_DRIV_LIC.Enabled=true;
			}
			
			if (txtDRIVER_DOB.Text.Trim()!="")
			{
				objDriverDetailInfo.DRIVER_DOB	=	ConvertToDate(txtDRIVER_DOB.Text);
				int DriverAge = System.DateTime.Now.Year - Convert.ToDateTime(txtDRIVER_DOB.Text).Year;
				
				
				if(DriverAge<=25 && objDriverDetailInfo.DRIVER_DRIV_TYPE==MOTOR_DRIVER_OPERATES_CYCLE && cmbCOLL_STUD_AWAY_HOME.SelectedItem!=null && cmbCOLL_STUD_AWAY_HOME.SelectedItem.Value!="")
				{
					objDriverDetailInfo.COLL_STUD_AWAY_HOME = int.Parse(cmbCOLL_STUD_AWAY_HOME.SelectedItem.Value);
					if(objDriverDetailInfo.COLL_STUD_AWAY_HOME ==1 && cmbCYCL_WITH_YOU.SelectedItem!=null && cmbCYCL_WITH_YOU.SelectedItem.Value!="")
						objDriverDetailInfo.CYCL_WITH_YOU = int.Parse(cmbCYCL_WITH_YOU.SelectedItem.Value);
				}
			}

			//objDriverDetailInfo.DRIVER_SSN = txtDRIVER_SSN.Text;
			if(txtDRIVER_SSN.Text.Trim()!="")
			{
				objDriverDetailInfo.DRIVER_SSN			=	Cms.BusinessLayer.BlCommon.ClsCommon.EncryptString(txtDRIVER_SSN.Text.Trim());
				txtDRIVER_SSN.Text = "";
			}
			else
				objDriverDetailInfo.DRIVER_SSN			= hidSSN_NO.Value;

			objDriverDetailInfo.DRIVER_MART_STAT = cmbDRIVER_MART_STAT.SelectedValue;
			objDriverDetailInfo.DRIVER_SEX = cmbDRIVER_SEX.SelectedValue;
			objDriverDetailInfo.DRIVER_DRIV_LIC = txtDRIVER_DRIV_LIC.Text;
			objDriverDetailInfo.DRIVER_LIC_STATE = cmbDRIVER_LIC_STATE.SelectedValue;
			//objDriverDetailInfo.DRIVER_LIC_CLASS = txtDRIVER_LIC_CLASS.Text;
			
			if(txtDATE_LICENSED.Text.Trim() != "")
				objDriverDetailInfo.DATE_LICENSED = ConvertToDate(txtDATE_LICENSED.Text);
			
			if(cmbRELATIONSHIP.SelectedItem!=null && cmbRELATIONSHIP.SelectedValue!="")
				objDriverDetailInfo.RELATIONSHIP = int.Parse(cmbRELATIONSHIP.SelectedItem.Value);
		
			//objDriverDetailInfo.DRIVER_OCC_CODE = cmbDRIVER_OCC_CODE.SelectedValue;
			objDriverDetailInfo.DRIVER_OCC_CLASS = cmbDRIVER_OCC_CLASS.SelectedValue;

			if(cmbVEHICLE_ID.SelectedItem!=null && cmbVEHICLE_ID.SelectedValue != "" )
				objDriverDetailInfo.VEHICLE_ID = Convert.ToInt32(cmbVEHICLE_ID.SelectedValue);
			else
				objDriverDetailInfo.VEHICLE_ID = -1;


			objDriverDetailInfo.DRIVER_US_CITIZEN=cmbDRIVER_US_CITIZEN.SelectedValue;

			if(cmbAPP_VEHICLE_PRIN_OCC_ID.SelectedIndex > 0)
				objDriverDetailInfo.APP_VEHICLE_PRIN_OCC_ID = Convert.ToInt32(cmbAPP_VEHICLE_PRIN_OCC_ID.SelectedValue); 
			objDriverDetailInfo.ASSIGNED_VEHICLE = hidSeletedData.Value;
	
			//objDriverDetailInfo.MATURE_DRIVER = cmbMATURE_DRIVER.SelectedValue;
			//objDriverDetailInfo.PREFERRED_RISK = cmbPREFERRED_RISK.SelectedValue;
			if(txtDATE_LICENSED.Text.Trim()!="")
			{
				if(Convert.ToInt32(hidMVRPoints.Value)<=2 && (System.DateTime.Now.Year - Convert.ToDateTime(txtDATE_LICENSED.Text).Year) > Convert.ToInt32(strLicenceLimit))
				{
					objDriverDetailInfo.PREFERRED_RISK="1";
				}
				else
					objDriverDetailInfo.PREFERRED_RISK="0";
			}
			else
				objDriverDetailInfo.PREFERRED_RISK="0";

			// Commented by Charles on 2-Jul-09 for Itrack issue 6012
			//objDriverDetailInfo.TRANSFEREXPERIENCE_RENEWALCREDIT = cmbTRANSFEREXPERIENCE_RENEWALCREDIT.SelectedValue;
			
			if(cmbNO_CYCLE_ENDMT.SelectedItem!=null && cmbNO_CYCLE_ENDMT.SelectedItem.Value!="")
			{
				objDriverDetailInfo.NO_CYCLE_ENDMT=cmbNO_CYCLE_ENDMT.SelectedValue;
			}
			if(hidDRIVER_ID.Value!="" && hidDRIVER_ID.Value.ToUpper()!="NEW")
				objDriverDetailInfo.DRIVER_ID=Int32.Parse(hidDRIVER_ID.Value);

			objDriverDetailInfo.MODIFIED_BY = int.Parse(GetUserId());
			objDriverDetailInfo.VIOLATIONS= int.Parse(cmbVIOLATIONS.SelectedItem.Value==""?"0":cmbVIOLATIONS.SelectedItem.Value);
			objDriverDetailInfo.MVR_ORDERED= int.Parse(cmbMVR_ORDERED.SelectedItem.Value==""?"0":cmbMVR_ORDERED.SelectedItem.Value);
			
			if(txtDATE_ORDERED.Text.Trim() !="")
				objDriverDetailInfo.DATE_ORDERED	=	Convert.ToDateTime(txtDATE_ORDERED.Text);

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
				ClsPolicyDriverInfo objDriverDetailInfo = GetFormValue();

				if(hidDRIVER_ID.Value.ToUpper().Equals("NEW")) //save case
				{
					objDriverDetailInfo.CREATED_BY = int.Parse(GetUserId());
					objDriverDetailInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					//intRetVal = objDriverDetail.AddPolicyMotorDriver(objDriverDetailInfo);
					intRetVal = objDriverDetail.AddPolicyMotorDriver(objDriverDetailInfo,hidCalledFrom.Value,hidCustomInfo.Value);

					if(intRetVal>0)
					{
						hidDRIVER_ID.Value		= objDriverDetailInfo.DRIVER_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value		=	"1";
						hidIS_ACTIVE.Value		= "Y";

						//Retreiving the old values in old xml
						GetOldDataXML();
						fxnAssignedVehicle();
						//Settignt th workflow
						SetWorkflow();

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

					//Creating the Model object for holding the Old data
					ClsPolicyDriverInfo objOldDriverDetailInfo = new ClsPolicyDriverInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldDriverDetailInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objDriverDetailInfo.DRIVER_ID = int.Parse(hidDRIVER_ID.Value);
					objDriverDetailInfo.MODIFIED_BY = int.Parse(GetUserId());
					objDriverDetailInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					objDriverDetailInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

					GetOldDataXML();
					strAssignXml = ClsVehicleInformation.FillPolVehicle(int.Parse(hidCUSTOMER_ID.Value)
						, int.Parse(hidPolicyID.Value)
						, int.Parse(hidPolicyVersionID.Value)
						, int.Parse(hidDRIVER_ID.Value==""?"0":hidDRIVER_ID.Value));
			
					 //Updating the record using business layer class object
					//intRetVal	= objDriverDetail.UpdatePolicyMotorDriver(objOldDriverDetailInfo,objDriverDetailInfo);
//					intRetVal	= objDriverDetail.UpdatePolicyMotorDriver(objOldDriverDetailInfo,objDriverDetailInfo,hidCalledFrom.Value,hidCustomInfo.Value);
					intRetVal	= objDriverDetail.UpdatePolicyMotorDriver(objOldDriverDetailInfo,objDriverDetailInfo,hidCalledFrom.Value,hidCustomInfo.Value,strAssignXml);
					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";

						//Retreiving the old values in old xml
						GetOldDataXML();
						fxnAssignedVehicle();
						//Settignt th workflow
						SetWorkflow();

						//Opening the endorsement details widnow
						OpenEndorsementDetails();
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
				//FillRiskDiscount();
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
		}

		/// <summary>
		/// Activates and deactivates  .
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			int intRetVal;
			try
			{
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				objDriverDetail =  new ClsDriverDetail();

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "Deactivated Succesfully.";
					objDriverDetail.TransactionInfoParams = objStuTransactionInfo;

				intRetVal =	objDriverDetail.ActivateDeactivatePolicyMotorDriver(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPolicyID.Value), int.Parse(hidPolicyVersionID.Value), int.Parse(hidDRIVER_ID.Value), int.Parse(GetUserId()), "N",hidCustomInfo.Value);

					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Activated Succesfully.";
					objDriverDetail.TransactionInfoParams = objStuTransactionInfo;
					
					intRetVal  = objDriverDetail.ActivateDeactivatePolicyMotorDriver(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPolicyID.Value), int.Parse(hidPolicyVersionID.Value), int.Parse(hidDRIVER_ID.Value),int.Parse(GetUserId()), "Y",hidCustomInfo.Value);

					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}
				if(intRetVal>0)
				{
					base.OpenEndorsementDetails();
				}
				hidFormSaved.Value			=	"0";
				
				//Generating the XML again
				GetOldDataXML();
				fxnAssignedVehicle();
				ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid(1," + hidDRIVER_ID.Value + ");</script>");

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


		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int intRetVal;	//For retreiving the return value of business class save function
			ClsDriverDetail objDriverDetail = new  ClsDriverDetail();			
			//Retreiving the form values into model class object
			ClsPolicyDriverInfo objDriverDetailInfo = new ClsPolicyDriverInfo();//GetFormValue(); //Commented by Charles on 10-Sep-09 for Itrack 6375
			//Added by Charles on 10-Sep-09 for Itrack 6375	
			base.PopulateModelObject(objDriverDetailInfo,hidOldData.Value);
			objDriverDetailInfo.CUSTOMER_ID=int.Parse(hidCUSTOMER_ID.Value);
			objDriverDetailInfo.POLICY_ID=int.Parse(hidPolicyID.Value);
			objDriverDetailInfo.POLICY_VERSION_ID=int.Parse(hidPolicyVersionID.Value);
			objDriverDetailInfo.DRIVER_ID = int.Parse(hidDRIVER_ID.Value);
			objDriverDetailInfo.MODIFIED_BY = int.Parse(GetUserId());//Added till here			
			
			//intRetVal = objDriverDetail.DeleteDriver(intCustomerID,intAppId,intAppVerId,intVehicleId,strCalledFrom);
			intRetVal = objDriverDetail.DeleteMotorDriverPolicy(objDriverDetailInfo,hidCustomInfo.Value);
			
			if(intRetVal>0)
			{
				lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","127");
				//lblMessage.Text		 =	ClsMessages.GetMessage(base.ScreenId,"127");
				hidFormSaved.Value = "5";
				hidOldData.Value = "";
				trBody.Attributes.Add("style","display:none");
				base.OpenEndorsementDetails();
				SetWorkflow();
			}
			else if(intRetVal == -1)
			{
				lblDelete.Text		=	ClsMessages.GetMessage(base.ScreenId,"128");
				hidFormSaved.Value		=	"2";
			}
			lblDelete.Visible = true;
		}


		private void SetCaptions()
		{
			capDRIVER_FNAME.Text					=		objResourceMgr.GetString("txtDRIVER_FNAME");
			capDRIVER_MNAME.Text					=		objResourceMgr.GetString("txtDRIVER_MNAME");
			capDRIVER_LNAME.Text					=		objResourceMgr.GetString("txtDRIVER_LNAME");
			capDRIVER_CODE.Text						=		objResourceMgr.GetString("txtDRIVER_CODE");
			capDRIVER_SUFFIX.Text					=		objResourceMgr.GetString("txtDRIVER_SUFFIX");
			capDRIVER_ADD1.Text						=		objResourceMgr.GetString("txtDRIVER_ADD1");
			capDRIVER_ADD2.Text						=		objResourceMgr.GetString("txtDRIVER_ADD2");
			capDRIVER_CITY.Text						=		objResourceMgr.GetString("txtDRIVER_CITY");
			capDRIVER_STATE.Text					=		objResourceMgr.GetString("cmbDRIVER_STATE");
			capDRIVER_ZIP.Text						=		objResourceMgr.GetString("txtDRIVER_ZIP");
			capDRIVER_COUNTRY.Text					=		objResourceMgr.GetString("cmbDRIVER_COUNTRY");
			capDRIVER_HOME_PHONE.Text				=		objResourceMgr.GetString("txtDRIVER_HOME_PHONE");
			capDRIVER_BUSINESS_PHONE.Text			=		objResourceMgr.GetString("txtDRIVER_BUSINESS_PHONE");
			capDRIVER_EXT.Text						=		objResourceMgr.GetString("txtDRIVER_EXT");
			//capDRIVER_FAX.Text						=		objResourceMgr.GetString("txtDRIVER_FAX");
			capDRIVER_MOBILE.Text					=		objResourceMgr.GetString("txtDRIVER_MOBILE");
			capDRIVER_DOB.Text						=		objResourceMgr.GetString("txtDRIVER_DOB");
			capDRIVER_SSN.Text						=		objResourceMgr.GetString("txtDRIVER_SSN");
			capDRIVER_MART_STAT.Text				=		objResourceMgr.GetString("cmbDRIVER_MART_STAT");
			capDRIVER_SEX.Text						=		objResourceMgr.GetString("cmbDRIVER_SEX");
			capDRIVER_DRIV_LIC.Text					=		objResourceMgr.GetString("txtDRIVER_DRIV_LIC");
			capDRIVER_LIC_STATE.Text				=		objResourceMgr.GetString("cmbDRIVER_LIC_STATE");
			//capDRIVER_LIC_CLASS.Text				=		objResourceMgr.GetString("txtDRIVER_LIC_CLASS");
			capDATE_LICENSED.Text					=		objResourceMgr.GetString("txtDATE_LICENSED");
			//capDRIVER_OCC_CODE.Text					=		objResourceMgr.GetString("cmbDRIVER_OCC_CODE");
			capDRIVER_OCC_CLASS.Text				=		objResourceMgr.GetString("cmbDRIVER_OCC_CLASS");
			capRELATIONSHIP.Text					=		objResourceMgr.GetString("cmbRELATIONSHIP");		
			//capDRIVER_BROADEND_NOFAULT.Text			=		objResourceMgr.GetString("cmbDRIVER_BROADEND_NOFAULT");
			capDRIVER_DRINK_VIOLATION.Text			=		objResourceMgr.GetString("cmbDRIVER_DRINK_VIOLATION");
			//capDRIVER_LIC_SUSPENDED.Text			=		objResourceMgr.GetString("cmbDRIVER_LIC_SUSPENDED");
			capDRIVER_US_CITIZEN.Text				=		objResourceMgr.GetString("cmbDRIVER_US_CITIZEN");
			capNO_CYCLE_ENDMT.Text						=		objResourceMgr.GetString("cmbNO_CYCLE_ENDMT");			
			capMATURE_DRIVER.Text				=		objResourceMgr.GetString("cmbMATURE_DRIVER"); 
			
			//Uncommented on 23-April-09 for Itrack issue 5744
			capPREFERRED_RISK.Text				=		objResourceMgr.GetString("cmbPREFERRED_RISK");  

			//Commented by Charles on 2-Jul-2009 for Itrack 6012
			//capTRANSFEREXPERIENCE_RENEWALCREDIT.Text	=		objResourceMgr.GetString("cmbTRANSFEREXPERIENCE_RENEWALCREDIT");  

			capVEHICLE_ID.Text						=		objResourceMgr.GetString("cmbVEHICLE_ID");  
			capAPP_VEHICLE_PRIN_OCC_ID.Text			=		objResourceMgr.GetString("cmbAPP_VEHICLE_PRIN_OCC_ID");  
			capDRIVER_DRIV_TYPE.Text						=		objResourceMgr.GetString("cmbDRIVER_DRIV_TYPE");			
			capCOLL_STUD_AWAY_HOME.Text						=		objResourceMgr.GetString("cmbCOLL_STUD_AWAY_HOME");			
			capCYCL_WITH_YOU.Text						=		objResourceMgr.GetString("cmbCYCL_WITH_YOU");			
			capLOSSREPORT_DATETIME.Text		=		objResourceMgr.GetString("txtLOSSREPORT_DATETIME");
			capLOSSREPORT_ORDER.Text  =objResourceMgr.GetString("cmbLOSSREPORT_ORDER");
			
		}

	
		//This method is used to fetch the query string values
		private void GetQueryString()
		{
			hidCalledFrom.Value		= Request.Params["CalledFrom"];
			hidCUSTOMER_ID.Value	= Request.Params["CUSTOMER_ID"];
			if (Request["DRIVER_ID"] != null)
				hidDRIVER_ID.Value		= Request.Params["DRIVER_ID"];
			else
				hidDRIVER_ID.Value		= "";
			hidPolicyID.Value = Request["POLICY_ID"];
			hidPolicyVersionID.Value = Request["POLICY_VERSION_ID"]; 
			
		}

		//Used to fetch the old xml
		private void GetOldDataXML()
		{
			if (hidDRIVER_ID.Value != "") //In case of update
			{
				hidOldData.Value = Cms.BusinessLayer.BlApplication.ClsDriverDetail.GetPolicyMotorDriverXML(
					int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPolicyID.Value) 
					, int.Parse(hidPolicyVersionID.Value), int.Parse(hidDRIVER_ID.Value));
                string hidFieldValue = "";
                capSSN_NO_HID.Text = ClsCommon.GetEncriptedFormatedString(hidOldData.Value, ref hidFieldValue, "DRIVER_SSN");
                hidSSN_NO.Value=hidFieldValue;
                if (ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString() != "")//Added By Pradeep Kushwaha on 20-sep-2010
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString());
            
				//added by pravesh for encripted SSN number
				/*if(hidOldData.Value.IndexOf("NewDataSet") >= 0)
				{
					XmlDocument objxml = new XmlDocument();

					objxml.LoadXml(hidOldData.Value);

					XmlNode node = objxml.SelectSingleNode("NewDataSet");
					foreach(XmlNode nodes in node.SelectNodes("Table"))
					{
						XmlNode noder1 = nodes.SelectSingleNode("DRIVER_SSN");

						hidSSN_NO.Value = noder1.InnerText;
						//noder1.InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder1.InnerText);
						string strSSN_NO = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder1.InnerText);
						if(strSSN_NO.Trim() != "")//Done for Itrack Issue 6063 on 7 July 09
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
					objxml = null;
				}*/
			}
			else
			{
				//If parameters not passed 
				hidOldData.Value					= "";
			}

			
			//For coping existing customer as driver.
			if (hidCUSTOMER_ID.Value != "")
			{
				hidCUSTOMER_INFO.Value=Cms.BusinessLayer.BlApplication.ClsDriverDetail.GetCustomerNameXML(int.Parse(hidCUSTOMER_ID.Value));
			}
			//End.
			
		}
		private string GetMasterDataPathForDiscount()
		{
			ClsGeneralInformation objGeneralInfo = new ClsGeneralInformation();			 						
			//Get the input xml from the procs for the current
			string strInputXML = "";string strPath = "";
			strInputXML		= objGeneralInfo.GetPolicyInputXML(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value),"3");
			Cms.BusinessLayer.BlQuote.ClsGenerateQuote objQuote = new Cms.BusinessLayer.BlQuote.ClsGenerateQuote(CarrierSystemID);
			strInputXML=strInputXML.Replace("&AMP;","&amp;");
			strPath = objQuote.GetProductFactorMasterPath(strInputXML,"CYCL");
			return strPath;
		}

		private void GetPolicyInfo()
		{
			//hidStateID.Value= Convert.ToString(ClsVehicleInformation.GetStateIdForApplication(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidAppId.Value),int.Parse(hidAppVersionId.Value)));
			//hidStateID.Value= Convert.ToString(ClsVehicleInformation.GetStateIdForpolicy(int.Parse(hidCUSTOMER_ID.Value),hidPolicyID.Value,hidPolicyVersionID.Value));
			ClsGeneralInformation objGeneralInfo = new ClsGeneralInformation();
			DataSet dsPolicyInfo = objGeneralInfo.GetPolicyDataSet(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value));
			if(dsPolicyInfo!=null && dsPolicyInfo.Tables.Count>0 && dsPolicyInfo.Tables[0].Rows.Count>0)
			{                 

				hidStateID.Value = dsPolicyInfo.Tables[0].Rows[0]["STATE_ID"].ToString();
                TimeSpan dateDiff = Convert.ToDateTime(ConvertDBDateToCulture(dsPolicyInfo.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString())) - Convert.ToDateTime(ClsVehicleInformation.MOTOR_NO_ENDOR_DATE);
				if(dateDiff.Ticks < 0)
				{
					capNO_CYCLE_ENDMT.Visible = false;
					cmbNO_CYCLE_ENDMT.Visible = false;
				}

				
			}
			
		}

		private void SetRiskLabelPercentage()
		{
			try
			{
				//XmlDocument xDoc=new XmlDocument();
				/*if(hidStateID.Value=="14")
					xDoc.Load(Server.MapPath(Request.ApplicationPath  + "/cmsweb/xsl/quote/masterdata/Motorcycle/ProductFactorsMaster_CYCLE_Indiana.xml"));
				else
					xDoc.Load(Server.MapPath(Request.ApplicationPath  + "/cmsweb/xsl/quote/masterdata/Motorcycle/ProductFactorsMaster_CYCLE_Michigan.xml"));				*/

				string strFilePath = GetMasterDataPathForDiscount();
				XmlDocument xDoc=new XmlDocument();
				xDoc.Load(strFilePath);
				XmlNodeList xNodeList=xDoc.SelectNodes("PRODUCTMASTER/PRODUCT/FACTOR[@ID='PREFERREDRISK']/NODE[@ID ='PREFERREDRISKDISCOUNT']/ATTRIBUTES/@CREDIT");												
				capPreferedRisk.Text = "  (" + xNodeList[0].InnerText + "%)";
				//xDoc.Load(@"c:/copy.xml"); 
				//XmlNodeList xNodeList=xDoc.SelectNodes("PRODUCTMASTER/PRODUCT[@ID='AUTO-P']/FACTOR[@ID='DRIVERDISCOUNT']/NODE[@ID ='DRIVERDISC']/ATTRIBUTES/@SAFEDRIVERCREDIT"); 

				////XmlNodeList xNodeList=xDoc.SelectNodes("PRODUCTMASTER/PRODUCT/FACTOR[@ID='PREFERREDRISK']/NODE[@ID ='PREFERREDRISKDISCOUNT']/ATTRIBUTES/@CREDIT");												
				////capPreferedRisk.Text = "  (" + xNodeList[0].InnerText + "%)";
				
				/*xNodeList=xDoc.SelectNodes("PRODUCTMASTER/PRODUCT[@ID='AUTO-P']/FACTOR[@ID='DRIVERDISCOUNT']/NODE[@ID ='DRIVERDISC']/ATTRIBUTES/@PREMIERDRIVERCREDIT"); 
				capPremierDriver.Text = " - (" + xNodeList[0].InnerText + "%)";
				xNodeList=xDoc.SelectNodes("PRODUCTMASTER/PRODUCT[@ID='AUTO-P']/FACTOR[@ID='GOODSTUDENT']/NODE[@ID ='GOODSTUDENTDISCOUNT']/ATTRIBUTES/@CREDIT"); 
				capGoodStudent.Text = " - (" + xNodeList[0].InnerText + "%)";*/
				
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"5") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value		=		"2";
			}
		}
		private void FillRiskDiscount()
		{			
			objDriverDetail = new  ClsDriverDetail();
			
			//returnResult=objDriverDetail.GetMVRPointsForPolicy(hidCUSTOMER_ID.Value,hidAppId.Value,hidAppVersionId.Value,hidDRIVER_ID.Value);			
			hidMVRPoints.Value=objDriverDetail.GetMVRPointsForPolicy(hidCUSTOMER_ID.Value,hidPolicyID.Value,hidPolicyVersionID.Value,hidDRIVER_ID.Value).ToString();			
			
			
			
		}


		private void SetWorkflow()
		{
			if(base.ScreenId == "237_0")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",hidCUSTOMER_ID.Value);
				myWorkFlow.AddKeyValue("POLICY_ID",hidPolicyID.Value);
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",hidPolicyVersionID.Value);

				if ( hidDRIVER_ID.Value != null && hidDRIVER_ID.Value != "" && hidDRIVER_ID.Value.ToUpper() != "NEW")
				{
					myWorkFlow.AddKeyValue("DRIVER_ID",hidDRIVER_ID.Value);
				}
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}

		
		protected void fxnAssignedVehicle()
		{
			int rowCnt;
			int rowCtr;
				
			string strXML = ClsVehicleInformation.FillPolVehicle(int.Parse(hidCUSTOMER_ID.Value)
				, int.Parse(hidPolicyID.Value)
				, int.Parse(hidPolicyVersionID.Value)
				, int.Parse(hidDRIVER_ID.Value==""?"0":hidDRIVER_ID.Value));
			
			strAssignXml = strXML;
			XmlDocument objXmlDoc = new XmlDocument();
			objXmlDoc.LoadXml(strXML);
				
			int VehicleCount = objXmlDoc.SelectSingleNode("NewDataSet").ChildNodes.Count;
			if(VehicleCount < 1)
			{
				lblVehicleMsg.Text	=  "No motorcycle added until now. Please click <a href='#' onclick='Redirect();'>here</a> to add motorcycles";
				trVehMsg.Attributes.Add ("style","display:inline;"); 
					
			}
			else
			{
				
				trVehMsg.Attributes.Add ("style","display:none;"); 
				
			}
			rowCnt  = VehicleCount;
				

			IList objList = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("VHIDPO");

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
		
	}
}
