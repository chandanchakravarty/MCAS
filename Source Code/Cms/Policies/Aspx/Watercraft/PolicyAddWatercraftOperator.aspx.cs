/******************************************************************************************
<Author					: -		Vijay Arora
<Start Date				: -		24-11-2005
<End Date				: -	
<Description			: - 	Policy Watercraft operator details
<Review Date			: - 
<Reviewed By			: - 	

Modification History
--------------------
<Modified Date			: - 18/05/2006
<Modified By			: - RPSINGH
<Purpose				: - Changes in FillDiscounts functions.
							All discount on the page are not to be shown here
							1. DRIVER_DIESEL_DISCOUNT
							2. NAVIGATION_DISCOUNT
							3. SHORE_STATION_CREDIT
							4. HALON_FIRE_DISCOUNT
							5. MULTIPLE_DISCOUNT
							
<Modified Date			: - 13/09/2006
<Modified By			: - PKASANA
<Purpose				: - Added in Assigned Boats functions
												
							
							
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
//using Cms.Model.Application.Watercrafts;
using  Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.CmsWeb.Controls;
using Cms.Model.Policy.Watercraft;
using Cms.Model.Application;
using System.Xml;
using Cms.BusinessLayer.BlCommon;

namespace Cms.Policies.Aspx.Watercrafts
{
	/// <summary>
	/// Summary description for AddWatercraftDriverDetails.
	/// </summary>
	public class PolicyAddWatercraftOperator  : Cms.Policies.policiesbase
	{
		#region PageControl Variable
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capAPP_VEHICLE_PRIN_OCC_ID;
		protected System.Web.UI.WebControls.Label capDRIVER_FNAME;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_FNAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_FNAME;
		protected System.Web.UI.WebControls.Label capDRIVER_MNAME;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_MNAME;
		protected System.Web.UI.WebControls.Label capDRIVER_LNAME;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_LNAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_LNAME;
		protected System.Web.UI.WebControls.Label capDRIVER_CODE;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_CODE;
		protected System.Web.UI.WebControls.Label capDRIVER_SUFFIX;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_SUFFIX;
		protected System.Web.UI.WebControls.Label capDRIVER_ADD1;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_ADD1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_ADD1;
		protected System.Web.UI.WebControls.Label capDRIVER_ADD2;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_ADD2;
		protected System.Web.UI.WebControls.Label capDRIVER_CITY;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_CITY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_CITY;
		protected System.Web.UI.WebControls.Label capDRIVER_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_STATE;
		protected System.Web.UI.WebControls.Label capDRIVER_ZIP;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_ZIP;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_ZIP;
		protected System.Web.UI.WebControls.Label capDRIVER_COUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_COUNTRY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_COUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbAPP_VEHICLE_PRIN_OCC_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvAPP_VEHICLE_PRIN_OCC_ID;
		protected System.Web.UI.WebControls.Label capDRIVER_DOB;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_DOB;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_DOB;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_DOB;
		protected System.Web.UI.WebControls.Label capDRIVER_SSN;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_SSN;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDRIVER_SSN;
		protected System.Web.UI.WebControls.Label capDRIVER_SEX;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_SEX;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_SEX;
		protected System.Web.UI.WebControls.Label capDRIVER_DRIV_LIC;
		protected System.Web.UI.WebControls.Label capDRIVER_LIC_STATE;
		protected System.Web.UI.WebControls.Label capDRIVER_COST_GAURAD_AUX;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDRIVER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_INFO;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomInfo;
		protected System.Web.UI.WebControls.CustomValidator csvDRIVER_DOB;
		protected System.Web.UI.HtmlControls.HtmlTableRow trRecVehMsg;
		protected System.Web.UI.HtmlControls.HtmlTableRow trRecVehField;
		protected System.Web.UI.HtmlControls.HtmlTableRow trRechVehHeader;
		protected System.Web.UI.WebControls.Label lblRecVehicleMsg;
		protected System.Web.UI.WebControls.Label capREC_VEH_ID;
		protected System.Web.UI.WebControls.DropDownList cmbREC_VEH_ID;
		protected System.Web.UI.WebControls.Label capAPP_REC_VEHICLE_PRIN_OCC_ID;
		protected System.Web.UI.WebControls.DropDownList cmbAPP_REC_VEHICLE_PRIN_OCC_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;	
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSeletedData;
		//Done for Itrack Issue 6737 on 17 Nov 09
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidRecSeletedData;
		protected System.Web.UI.WebControls.Label capVIOLATIONS;
		protected System.Web.UI.WebControls.DropDownList cmbVIOLATIONS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVIOLATIONS;
		protected System.Web.UI.WebControls.Label capMVR_ORDERED;
		protected System.Web.UI.WebControls.DropDownList cmbMVR_ORDERED;
		protected System.Web.UI.WebControls.Label capDATE_ORDERED;
		protected System.Web.UI.WebControls.TextBox txtDATE_ORDERED;
		protected System.Web.UI.WebControls.HyperLink hlkDATE_ORDERED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDATE_ORDERED;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDATE_ORDERED;
		protected System.Web.UI.WebControls.CustomValidator csvDATE_ORDERED;
		protected System.Web.UI.WebControls.CustomValidator csvDRIVER_COST_GAURAD_AUX;
		protected System.Web.UI.WebControls.Label capMARITAL_STATUS;
		protected System.Web.UI.WebControls.DropDownList cmbMARITAL_STATUS;
		protected System.Web.UI.WebControls.Image imgZipLookup;
		protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
		protected System.Web.UI.WebControls.Table tblAssignedVeh;
		//Done for Itrack Issue 6737 on 17 Nov 09
		protected System.Web.UI.WebControls.Table tblAssignedRecVeh;
		protected System.Web.UI.WebControls.Label capDRIVER_DRIV_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_DRIV_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_DRIV_TYPE;
		protected System.Web.UI.HtmlControls.HtmlTableRow trViolationMsg;
		protected System.Web.UI.HtmlControls.HtmlTableRow trViolationField;
		protected System.Web.UI.HtmlControls.HtmlTableRow trViolationSec;
		protected System.Web.UI.WebControls.Label lblViolationMsg;
		protected System.Web.UI.WebControls.Label lblViolationPoints;
		protected System.Web.UI.WebControls.Label capViolationPoints;
		protected System.Web.UI.WebControls.Label	capSSN_NO_HID; 
		protected Cms.CmsWeb.Controls.CmsButton btnSSN_NO; 	
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSSN_NO;

		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		protected string curDate="";
		public string delStr=""; 	
		string strCalledFor="" ;

		#endregion

		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.WebControls.HyperLink hlkCalandarDate1;
		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFor;
		protected System.Web.UI.WebControls.Label capPullCustomerAddress;
		protected Cms.CmsWeb.Controls.CmsButton btnPullCustomerAddress;
		protected Cms.CmsWeb.Controls.CmsButton btnCopyDefaultCustomer;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;

		protected System.Web.UI.WebControls.Label lblVehicleMsg;
		protected System.Web.UI.WebControls.Label capVEHICLE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbVEHICLE_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVEHICLE_ID;
		protected System.Web.UI.WebControls.Label capPERCENT_DRIVEN;
		protected System.Web.UI.HtmlControls.HtmlTableRow trVehMsg;
		protected System.Web.UI.HtmlControls.HtmlTableRow trVehField;
		//Done for Itrack Issue 6737 on 17 Nov 09
		protected System.Web.UI.HtmlControls.HtmlTableRow trVehRecField_Home;
		ClsDriverDetail  objDriverDetails ;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_DRIV_LIC;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_DRIV_LIC;
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_LIC_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_LIC_STATE;
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
		protected System.Web.UI.HtmlControls.HtmlTableRow trDRIVER_DIESEL_DISCOUNT;
		protected System.Web.UI.HtmlControls.HtmlTableRow trHALON_FIRE_DISCOUNT;
		protected System.Web.UI.HtmlControls.HtmlTableRow trNAVIGATION_DISCOUNT;
		protected System.Web.UI.HtmlControls.HtmlTableRow trSHORE_STATION_CREDIT;
		protected System.Web.UI.HtmlControls.HtmlTableRow trMULTIPLE_DISCOUNT;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected System.Web.UI.WebControls.Label capWAT_SAFETY_COURSE;
		protected System.Web.UI.WebControls.DropDownList cmbWAT_SAFETY_COURSE;
		protected System.Web.UI.WebControls.Label capCERT_COAST_GUARD;
		protected System.Web.UI.WebControls.DropDownList cmbCERT_COAST_GUARD;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_COST_GAURAD_AUX;
		//protected System.Web.UI.WebControls.RangeValidator rngDRIVER_COST_GAURAD_AUX; //Commented by Charles on 6-Nov-09 for Itrack 6721
		protected System.Web.UI.WebControls.DropDownList cmbDRIVER_COST_GAURAD_AUX;
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
		//protected System.Web.UI.WebControls.Label capMARITAL_STATUS;
		//protected System.Web.UI.WebControls.DropDownList cmbMARITAL_STATUS;  
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDRIVER_COST_GAURAD_AUX;
		protected System.Web.UI.WebControls.Label capMVR_REMARKS;
		protected System.Web.UI.WebControls.TextBox txtMVR_REMARKS;
		protected System.Web.UI.WebControls.CustomValidator csvMVR_REMARKS;
		protected System.Web.UI.WebControls.Label capMVR_STATUS;
		protected System.Web.UI.WebControls.DropDownList cmbMVR_STATUS;
		protected System.Web.UI.HtmlControls.HtmlGenericControl spnDRIVER_COST_GAURAD_AUX; //Added by Charles on 6-Nov-09 for Itrack 6721
		#endregion

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvDRIVER_FNAME.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvDRIVER_LNAME.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvDRIVER_CODE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvDRIVER_ADD1.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			rfvDRIVER_CITY.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			rfvDRIVER_STATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			rfvDRIVER_ZIP.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
			rfvDRIVER_COUNTRY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"8");
			rfvDRIVER_DOB.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");
			rfvDRIVER_SEX.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"10");
			revDRIVER_ZIP.ValidationExpression		=	aRegExpZip;
			revDRIVER_DOB.ValidationExpression		=	aRegExpDate;
			revDRIVER_SSN.ValidationExpression		=	aRegExpSSN;
			revDRIVER_ZIP.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"11");
			revDRIVER_DOB.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"13");
			revDRIVER_SSN.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"14");
			csvDRIVER_DOB.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"15");
			rfvVEHICLE_ID.ErrorMessage              = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"36");
			rfvAPP_VEHICLE_PRIN_OCC_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"35");
			rfvDRIVER_DRIV_LIC.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"190");
			rfvDRIVER_LIC_STATE.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"111");
			//Commented by Charles on 6-Nov-09 for Itrack 6721
			//rngDRIVER_COST_GAURAD_AUX.MinimumValue  = aAppMinYear;
			//rngDRIVER_COST_GAURAD_AUX.MaximumValue  = (DateTime.Now.Year).ToString();
			//rngDRIVER_COST_GAURAD_AUX.ErrorMessage  = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"37");
			//csvDRIVER_COST_GAURAD_AUX.ErrorMessage  = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"37");
			csvDRIVER_COST_GAURAD_AUX.ErrorMessage	  =	"Year must be numeric and must lie between Date of Birth and current year."; //Added by Charles on 6-Nov-09 for Itrack 6721
			rfvDRIVER_COST_GAURAD_AUX.ErrorMessage =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("833");
			revDATE_ORDERED.ValidationExpression		=	aRegExpDate;
			rfvVIOLATIONS.ErrorMessage =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("878");
			rfvDATE_ORDERED.ErrorMessage =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("879");
			revDATE_ORDERED.ErrorMessage =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("179");
			csvDATE_ORDERED.ErrorMessage =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("880");
			rfvDRIVER_DRIV_TYPE.ErrorMessage  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("112");

			
		}

		private void SetLicErrorMessage()
		{
			string strOldLicenseState = "";
			if(hidOldData.Value != "0")
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
			this.cmbDRIVER_COUNTRY.SelectedIndex = int.Parse(aCountry);
			curDate=DateTime.Now.ToString() ;  

			string AssgnVeh="",AssgnRecVeh="";//Done for Itrack Issue 6737 on 17 Nov 09			
			if(hidSeletedData.Value == "0")
			{
				AssgnVeh = "<script> " + 
					" Assigned_Veh(); "+
					"</script> ";
				ClientScript.RegisterStartupScript(this.GetType(),"Test",AssgnVeh);
			}

			//Done for Itrack Issue 6737 on 17 Nov 09
			if(hidRecSeletedData.Value == "0")
			{
				AssgnRecVeh = "<script> " + 
					" AssignedRec_Veh(); "+
					"</script> ";
				ClientScript.RegisterStartupScript(this.GetType(),"TestRec",AssgnRecVeh);
			}

			btnReset.Attributes.Add("onclick","javascript:return ResetForm1('" + Page.Controls[0].ID + "' );");
			
			btnCopyDefaultCustomer.Attributes.Add("onclick","javascript:return FillCustomerName();");
			//Done for Itrack Issue 6737 on 17 Nov 09
			//btnSave.Attributes.Add("onClick","javascript:return SaveClientSide();");
			btnSave.Attributes.Add("onClick","javascript:return SaveClientSide();RecVehicle_SaveClientSide();");
			txtDRIVER_FNAME.Attributes.Add("onBlur","javascript:return GenerateDriverCode(\"txtDRIVER_FNAME\");");
			txtDRIVER_LNAME.Attributes.Add("onBlur","javascript:return GenerateDriverCode(\"txtDRIVER_LNAME\");");

			// Added by Swarup on 30-mar-2007
			imgZipLookup.Attributes.Add("style","cursor:hand");
			base.VerifyAddress(hlkZipLookup, txtDRIVER_ADD1,txtDRIVER_ADD2
				, txtDRIVER_CITY, cmbDRIVER_STATE, txtDRIVER_ZIP);


			//for calendar picker
			hlkCalandarDate1.Attributes.Add("OnClick","fPopCalendar(document.APP_WATERCRAFT_DRIVER_DETAILS.txtDRIVER_DOB,document.APP_WATERCRAFT_DRIVER_DETAILS.txtDRIVER_DOB)");  	
				hlkDATE_ORDERED.Attributes.Add("OnClick","fPopCalendar(document.APP_WATERCRAFT_DRIVER_DETAILS.txtDATE_ORDERED,document.APP_WATERCRAFT_DRIVER_DETAILS.txtDATE_ORDERED)");  	
			if(Request.QueryString["CalledFrom"]!=null)
				hidCalledFrom.Value =Request.QueryString["CalledFrom"].ToString();

			//Used to check the selected lob
			if(Request.QueryString["CalledFor"]!=null)
				strCalledFor = Request.QueryString["CalledFor"].ToString();

			#region SETTING SCREEN ID
			if(hidCalledFrom.Value=="WAT")
			{
				base.ScreenId="247_0"; 							
			}
			else if(hidCalledFrom.Value=="RENT")
			{
				base.ScreenId="167_0"; 	
			}
			else if(hidCalledFrom.Value=="Home")
			{//applied for the Operator
				//base.ScreenId="149_0"; 	
				base.ScreenId="252_0";

			}
			else if (hidCalledFrom.Value=="UMB")
			{
				base.ScreenId="223_0"; 	
			}

			#endregion
			
			SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass			=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;
			btnSave.CmsButtonClass			=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;
			
			btnPullCustomerAddress.CmsButtonClass	= CmsButtonType.Write;
			btnPullCustomerAddress.PermissionString = gstrSecurityXML;
			btnPullCustomerAddress.CausesValidation	= false;

			btnCopyDefaultCustomer.CmsButtonClass=CmsButtonType.Write;
			btnCopyDefaultCustomer.PermissionString=gstrSecurityXML;

			btnDelete.CmsButtonClass		=	CmsButtonType.Delete;
			btnDelete.PermissionString	=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;

			RequiredPullCustAdd(txtDRIVER_ADD1, txtDRIVER_ADD2, txtDRIVER_CITY
				, cmbDRIVER_COUNTRY, cmbDRIVER_STATE, txtDRIVER_ZIP
				, btnPullCustomerAddress);
			
			btnPullCustomerAddress.Attributes.Add("onClick","javascript:PullCustomerAddress("
				+ "document.getElementById('" + txtDRIVER_ADD1.ID + "'),"
				+ "document.getElementById('" + txtDRIVER_ADD2.ID + "'),"
				+ "document.getElementById('" + txtDRIVER_CITY.ID + "'),"
				+ "document.getElementById('" + cmbDRIVER_COUNTRY.ID + "'),"
				+ "document.getElementById('" + cmbDRIVER_STATE.ID + "'),"
				+ "document.getElementById('" + txtDRIVER_ZIP.ID + "')"
				+ ");SetRegisteredState();return false;");

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.Watercrafts.PolicyAddWatercraftOperator" ,System.Reflection.Assembly.GetExecutingAssembly());
			
		//	SetLabelPercentage();
		//	SetAttribute();

			if(!Page.IsPostBack)
			{
			
				if(Request.QueryString["CalledFor"]!=null)
					hidCalledFor.Value =Request.QueryString["CalledFor"].ToString();

				cmbDRIVER_COUNTRY.SelectedIndex = Convert.ToInt32(aCountry);

				GetOldDataXML();
				fxnAssignedVehicle(); //get Assigned boats 
				if(hidLOB_ID.Value==((int)enumLOB.HOME).ToString())//Done for Itrack Issue 6789 on 3 Dec 09
				{
					fxnAssignedRecreationalVehicle();//get Reacreational Assigned vehicles
				}
				SetCaptions();

				#region "FILL CONTROLS"

				cmbCERT_COAST_GUARD.DataSource		= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
				cmbCERT_COAST_GUARD.DataTextField	= "LookupDesc";
				cmbCERT_COAST_GUARD.DataValueField	= "LookupID";
				cmbCERT_COAST_GUARD.DataBind();
				cmbCERT_COAST_GUARD.Items.Insert(0,"");

				cmbWAT_SAFETY_COURSE.DataSource		= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
				cmbWAT_SAFETY_COURSE.DataTextField	= "LookupDesc";
				cmbWAT_SAFETY_COURSE.DataValueField	= "LookupID";
				cmbWAT_SAFETY_COURSE.DataBind();
				cmbWAT_SAFETY_COURSE.Items.Insert(0,"");

				DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
				cmbDRIVER_COUNTRY.DataSource		= dt;
				cmbDRIVER_COUNTRY.DataTextField		= "Country_Name";
				cmbDRIVER_COUNTRY.DataValueField	= "Country_Id";
				cmbDRIVER_COUNTRY.DataBind();
					
				dt = Cms.CmsWeb.ClsFetcher.State;
				cmbDRIVER_STATE.DataSource		= dt;
				cmbDRIVER_STATE.DataTextField	= "State_Name";
				cmbDRIVER_STATE.DataValueField	= "State_Id";
				cmbDRIVER_STATE.DataBind();
				cmbDRIVER_STATE.Items.Insert(0,"");

				cmbDRIVER_LIC_STATE.DataSource		=  Cms.CmsWeb.ClsFetcher.State; 
				cmbDRIVER_LIC_STATE.DataTextField	= "State_Name";
				cmbDRIVER_LIC_STATE.DataValueField	= "State_Id";
				cmbDRIVER_LIC_STATE.DataBind();					 
				cmbDRIVER_LIC_STATE.Items.Insert(0,"");
				/*Sumit Chhabra:March 28,2006. Value for Gender to contain M/F values hard coded at page level
				cmbDRIVER_SEX.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("SEXCD",null,"Y");
				cmbDRIVER_SEX.DataTextField	= "LookupDesc";
				cmbDRIVER_SEX.DataValueField	= "LookupID";
				cmbDRIVER_SEX.DataBind();
				cmbDRIVER_SEX.Items.Insert(0,""); */

				cmbAPP_VEHICLE_PRIN_OCC_ID.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("WTRPO",null,"Y");
				cmbAPP_VEHICLE_PRIN_OCC_ID.DataTextField	= "LookupDesc";
				cmbAPP_VEHICLE_PRIN_OCC_ID.DataValueField	= "LookupID";
				cmbAPP_VEHICLE_PRIN_OCC_ID.DataBind();
				cmbAPP_VEHICLE_PRIN_OCC_ID.Items.Insert(0,""); 

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

				cmbMARITAL_STATUS.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("MARST");
				cmbMARITAL_STATUS.DataTextField	=	"LookupDesc";
				cmbMARITAL_STATUS.DataValueField=	"LookupCode";
				cmbMARITAL_STATUS.DataBind();
				cmbMARITAL_STATUS.Items.Insert(0,""); 

				//Populating the driver type
				cmbDRIVER_DRIV_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("DRTCD");
				cmbDRIVER_DRIV_TYPE.DataTextField = "LookupDesc";
				cmbDRIVER_DRIV_TYPE.DataValueField = "LookupID";
				cmbDRIVER_DRIV_TYPE.DataBind();
				cmbDRIVER_DRIV_TYPE.Items.Insert(0,new ListItem("",""));
				cmbDRIVER_DRIV_TYPE.SelectedIndex=0;


				DataSet dsTemp;
				if(hidCalledFrom.Value.ToUpper()=="UMB")
					dsTemp = ClsUmbrellaGen.FetchUmbrellaBoatInfo(int.Parse (hidAppID.Value) ,int.Parse (hidCustomerID.Value) , int.Parse(hidAppVersionID.Value ));
				else//Watercraft and Home Watercraft and Rental Watercraft
					dsTemp = clsWatercraftInformation.FetchPolicyBoatInfo(int.Parse (hidCustomerID.Value) , int.Parse (hidPolicyID.Value) , int.Parse(hidPolicyVersionID.Value ));

				if (dsTemp!=null && dsTemp.Tables[0].Rows.Count>0)
				{
					cmbVEHICLE_ID.DataSource= dsTemp;
					cmbVEHICLE_ID.DataTextField	= "Boat";
					cmbVEHICLE_ID.DataValueField	= "Boat_ID";
					cmbVEHICLE_ID.DataBind();
				}
				if(hidLOB_ID.Value==((int)enumLOB.HOME).ToString() && dsTemp!=null && dsTemp.Tables.Count>1 && dsTemp.Tables[1]!=null && dsTemp.Tables[1].Rows.Count>0)
				{
					cmbREC_VEH_ID.DataSource= dsTemp.Tables[1];
					cmbREC_VEH_ID.DataTextField	= "REC_VEH";
					cmbREC_VEH_ID.DataValueField	= "REC_VEH_ID";
					cmbREC_VEH_ID.DataBind();
					cmbREC_VEH_ID.Items.Insert(0,"");
					trRecVehMsg.Attributes.Add ("style","display:none;"); 
					//Done for Itrack Issue 6737 on 17 Nov 09
					//trRecVehField.Attributes.Add ("style","display:inline;");
					trVehRecField_Home.Attributes.Add ("style","display:inline;"); 
					trRechVehHeader.Attributes.Add ("style","display:inline;"); 

					cmbAPP_REC_VEHICLE_PRIN_OCC_ID.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("WTRPO",null,"Y");
					cmbAPP_REC_VEHICLE_PRIN_OCC_ID.DataTextField	= "LookupDesc";
					cmbAPP_REC_VEHICLE_PRIN_OCC_ID.DataValueField	= "LookupID";
					cmbAPP_REC_VEHICLE_PRIN_OCC_ID.DataBind();
					cmbAPP_REC_VEHICLE_PRIN_OCC_ID.Items.Insert(0,""); 
				}
				else
				{
					if(hidLOB_ID.Value==((int)enumLOB.HOME).ToString())
					{
						lblRecVehicleMsg.Text	=  "No recreational vehicles added until now. Please click <a href='#' onclick='RedirectToRecVeh();'>here</a> to add recreational vehicles";						
						trRecVehMsg.Attributes.Add ("style","display:inline;"); 
						//Done for Itrack Issue 6737 on 17 Nov 09
						//trRecVehField.Attributes.Add ("style","display:none;"); 
						trVehRecField_Home.Attributes.Add ("style","display:none;");
						trRechVehHeader.Attributes.Add ("style","display:inline;"); 
					}
					
				}

				//if no vehicle is available
				//	-- show link to go add vehicle screen
				//  -- else show the dropdown to select primary vehicle
				if(cmbVEHICLE_ID.Items.Count<1)
				{					
					lblVehicleMsg.Text	=  "No boats added until now. Please click <a href='#' onclick='Redirect();'>here</a> to add boats";
					trVehMsg.Attributes.Add ("style","display:inline;"); 
					trVehField.Attributes.Add ("style","display:none;"); 
					rfvVEHICLE_ID.Enabled = false;
					rfvAPP_VEHICLE_PRIN_OCC_ID.Enabled = false;

					if(hidLOB_ID.Value==((int)enumLOB.HOME).ToString()) //Added by Charles on 6-Nov-09 for Itrack 6721
					{
						spnDRIVER_COST_GAURAD_AUX.Visible=false;
						rfvDRIVER_COST_GAURAD_AUX.Enabled=false;						
					}
				}
				else
				{
					cmbVEHICLE_ID.Items.Insert(0,"");
					trVehMsg.Attributes.Add ("style","display:none;"); 
					trVehField.Attributes.Add ("style","display:inline;"); 
					rfvVEHICLE_ID.Enabled = true;
					rfvAPP_VEHICLE_PRIN_OCC_ID.Enabled = true;

					if(hidLOB_ID.Value==((int)enumLOB.HOME).ToString()) //Added by Charles on 6-Nov-09 for Itrack 6721
					{
						spnDRIVER_COST_GAURAD_AUX.Visible=true;
						rfvDRIVER_COST_GAURAD_AUX.Enabled=true;						
					}
				}

				#endregion 

				#region Set Workflow Control
				SetWorkflow();
				#endregion

			//	FillDiscountSectionDropDownList();
				FillDiscounts();

			}
	//		SetLicErrorMessage();
			if(hidCalledFrom.Value.ToUpper()!="UMB")
				GetViolationPoints();
		} 
		#endregion
		
		#region Violation Points
		private void GetViolationPoints()
		{
			Cms.BusinessLayer.BlCommon.ClsQuickQuote objQuickQuote = new Cms.BusinessLayer.BlCommon.ClsQuickQuote();
			if(hidDRIVER_ID.Value!="" && hidDRIVER_ID.Value!="0" && hidDRIVER_ID.Value.ToUpper()!="NEW")
			{
				DataTable dtTemp = objQuickQuote.GetMVRPoints(int.Parse(hidCustomerID.Value),int.Parse(hidPolicyID.Value),int.Parse(hidPolicyVersionID.Value),int.Parse(hidDRIVER_ID.Value),"POL");
				if(dtTemp!=null && dtTemp.Rows.Count>0)
				{
					int TotalViolationPoints = int.Parse(dtTemp.Rows[0]["TOTAL_VIOLATION_POINT"].ToString());
					if(TotalViolationPoints!=-1)
						lblViolationPoints.Text = TotalViolationPoints.ToString();				
					else
						lblViolationPoints.Text = "";					

				}

				if(lblViolationPoints.Text == "" )
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
		#endregion

		/// <summary>
		/// setting attribute for the checkbox to show discount percentage when they are checked
		/// </summary>
//		private void SetAttribute()
//		{
//			cmbDRIVER_COST_GAURAD_AUX.Attributes.Add("onchange","ShowDiscountPercentage('" + cmbDRIVER_COST_GAURAD_AUX.ClientID + "','" + capCoastGuard.ClientID + "')");
//		}

		/// <summary>
		/// fetching discount percentage from product factor master for Auto LOB
		/// </summary>
		private void SetLabelPercentage()
		{
			//capCoastGuard.Text="10%"; 
			//Do not delete
			//Fator master file for Watercraft LOB has not been prepared
			/*try
			{
				XmlDocument xDoc=new XmlDocument();
				xDoc.Load(Server.MapPath(Request.ApplicationPath  + "/cmsweb/xsl/quote/masterdata/productfactorsmaster_auto.xml")); 
				//xDoc.Load(@"c:/copy.xml"); 
				XmlNodeList xNodeList=xDoc.SelectNodes("PRODUCTMASTER/PRODUCT[@ID='AUTO-P']/FACTOR[@ID='DRIVERDISCOUNT']/NODE[@ID ='DRIVERDISC']/ATTRIBUTES/@SAFEDRIVERCREDIT"); 
				capSafeDriver.Text = " - (" + xNodeList[0].InnerText + "%)";
				xNodeList=xDoc.SelectNodes("PRODUCTMASTER/PRODUCT[@ID='AUTO-P']/FACTOR[@ID='DRIVERDISCOUNT']/NODE[@ID ='DRIVERDISC']/ATTRIBUTES/@PREMIERDRIVERCREDIT"); 
				capPremierDriver.Text = " - (" + xNodeList[0].InnerText + "%)";
				xNodeList=xDoc.SelectNodes("PRODUCTMASTER/PRODUCT[@ID='AUTO-P']/FACTOR[@ID='GOODSTUDENT']/NODE[@ID ='GOODSTUDENTDISCOUNT']/ATTRIBUTES/@CREDIT"); 
				capGoodStudent.Text = " - (" + xNodeList[0].InnerText + "%)";
				
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"5") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value		=		"2";
			}*/
		}
		
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private  ClsPolicyWatercraftOperatorInfo  GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsPolicyWatercraftOperatorInfo objWatercraftOperatorInfo;
			objWatercraftOperatorInfo = new ClsPolicyWatercraftOperatorInfo();			

			objWatercraftOperatorInfo.DRIVER_FNAME=	txtDRIVER_FNAME.Text;
			objWatercraftOperatorInfo.DRIVER_MNAME=	txtDRIVER_MNAME.Text;
			objWatercraftOperatorInfo.DRIVER_LNAME=	txtDRIVER_LNAME.Text;
			objWatercraftOperatorInfo.DRIVER_CODE=	txtDRIVER_CODE.Text;
			objWatercraftOperatorInfo.DRIVER_SUFFIX=	txtDRIVER_SUFFIX.Text;
			objWatercraftOperatorInfo.DRIVER_ADD1=	txtDRIVER_ADD1.Text;
			objWatercraftOperatorInfo.DRIVER_ADD2=	txtDRIVER_ADD2.Text;
			objWatercraftOperatorInfo.DRIVER_CITY=	txtDRIVER_CITY.Text;
			objWatercraftOperatorInfo.DRIVER_STATE=	cmbDRIVER_STATE.SelectedValue;
			objWatercraftOperatorInfo.DRIVER_ZIP=	txtDRIVER_ZIP.Text;

			objWatercraftOperatorInfo.MVR_CLASS = txtMVR_CLASS.Text;
			objWatercraftOperatorInfo.MVR_LIC_CLASS = txtMVR_LIC_CLASS.Text;
			objWatercraftOperatorInfo.MVR_LIC_RESTR = txtMVR_LIC_RESTR.Text;
			objWatercraftOperatorInfo.MVR_DRIV_LIC_APPL = txtMVR_DRIV_LIC_APPL.Text;

			objWatercraftOperatorInfo.MVR_REMARKS = txtMVR_REMARKS.Text;
			objWatercraftOperatorInfo.MVR_STATUS = cmbMVR_STATUS.SelectedValue;

			objWatercraftOperatorInfo.DRIVER_COUNTRY=	cmbDRIVER_COUNTRY.SelectedValue;
			objWatercraftOperatorInfo.MARITAL_STATUS= cmbMARITAL_STATUS.SelectedValue;
			if(txtDRIVER_DOB.Text.Trim() !="")
				objWatercraftOperatorInfo.DRIVER_DOB	=	Convert.ToDateTime(txtDRIVER_DOB.Text);
			//objWatercraftOperatorInfo.DRIVER_SSN=	txtDRIVER_SSN.Text;
			if(txtDRIVER_SSN.Text.Trim()!="")
			{
				objWatercraftOperatorInfo.DRIVER_SSN			=	Cms.BusinessLayer.BlCommon.ClsCommon.EncryptString(txtDRIVER_SSN.Text.Trim());
				txtDRIVER_SSN.Text = "";
			}
			else
				objWatercraftOperatorInfo.DRIVER_SSN			= hidSSN_NO.Value;


			if(cmbDRIVER_SEX.SelectedItem.Value != null && cmbDRIVER_SEX.SelectedItem.Text.Trim() != "") 
				objWatercraftOperatorInfo.DRIVER_SEX=cmbDRIVER_SEX.SelectedItem.Value;

			if(cmbDRIVER_DRIV_TYPE.SelectedValue!="")
				objWatercraftOperatorInfo.DRIVER_DRIV_TYPE= int.Parse(cmbDRIVER_DRIV_TYPE.SelectedValue);
			else
				objWatercraftOperatorInfo.DRIVER_DRIV_TYPE = 0;


			objWatercraftOperatorInfo.DRIVER_DRIV_LIC=	txtDRIVER_DRIV_LIC.Text;
			objWatercraftOperatorInfo.DRIVER_LIC_STATE=	cmbDRIVER_LIC_STATE.SelectedValue;
			
			objWatercraftOperatorInfo.POLICY_ID =int.Parse(hidPolicyID.Value);
			objWatercraftOperatorInfo.POLICY_VERSION_ID =int.Parse(hidPolicyVersionID.Value );
			objWatercraftOperatorInfo.CUSTOMER_ID=int.Parse(hidCustomerID.Value);
			
			if(txtDRIVER_COST_GAURAD_AUX.Text.Trim() !="")
				objWatercraftOperatorInfo.DRIVER_COST_GAURAD_AUX	=	Convert.ToInt32(txtDRIVER_COST_GAURAD_AUX.Text);
		
			if(cmbVEHICLE_ID.SelectedItem!=null) 
				objWatercraftOperatorInfo.VEHICLE_ID=Convert.ToInt32(cmbVEHICLE_ID.SelectedItem.Value==""?"0":cmbVEHICLE_ID.SelectedItem.Value);
			
			if(cmbAPP_VEHICLE_PRIN_OCC_ID.SelectedItem!=null) 
			//objWatercraftOperatorInfo.APP_VEHICLE_PRIN_OCC_ID = Convert.ToInt32(cmbAPP_VEHICLE_PRIN_OCC_ID.SelectedValue); 
				objWatercraftOperatorInfo.APP_VEHICLE_PRIN_OCC_ID=Convert.ToInt32(cmbAPP_VEHICLE_PRIN_OCC_ID.SelectedItem.Value==""?"0":cmbAPP_VEHICLE_PRIN_OCC_ID.SelectedItem.Value);

			//added by praveen
			objWatercraftOperatorInfo.ASSIGNED_VEHICLE = hidSeletedData.Value;
			//Done for Itrack Issue 6737 on 17 Nov 09
			objWatercraftOperatorInfo.ASSIGNED_REC_VEHICLE = hidRecSeletedData.Value;

			if(hidDRIVER_ID.Value!="" && hidDRIVER_ID.Value!="NEW" && hidDRIVER_ID.Value!="New")
				objWatercraftOperatorInfo.DRIVER_ID=int.Parse(hidDRIVER_ID.Value);
			else
				hidCustomInfo.Value=";Operator Name = " + txtDRIVER_FNAME.Text + " " + txtDRIVER_LNAME.Text +  ";Operator Code = " +  txtDRIVER_CODE.Text;		

			if(cmbCERT_COAST_GUARD.SelectedItem!=null)
				objWatercraftOperatorInfo.CERT_COAST_GUARD=int.Parse(cmbCERT_COAST_GUARD.SelectedItem.Value==""?"0":cmbCERT_COAST_GUARD.SelectedItem.Value);

			if(cmbWAT_SAFETY_COURSE.SelectedItem!=null)
				objWatercraftOperatorInfo.WAT_SAFETY_COURSE=int.Parse(cmbWAT_SAFETY_COURSE.SelectedItem.Value==""?"0":cmbWAT_SAFETY_COURSE.SelectedItem.Value);
			if(hidLOB_ID.Value==((int)enumLOB.HOME).ToString())
			{
				//Done for Itrack Issue 6737 on 17 Nov 09

//				if(cmbREC_VEH_ID.SelectedItem!=null && cmbREC_VEH_ID.SelectedItem.Value!="")
//					objWatercraftOperatorInfo.REC_VEH_ID = int.Parse(cmbREC_VEH_ID.SelectedItem.Value);
//				if(cmbAPP_REC_VEHICLE_PRIN_OCC_ID.SelectedItem!=null && cmbAPP_REC_VEHICLE_PRIN_OCC_ID.SelectedItem.Value!="")
//					objWatercraftOperatorInfo.APP_REC_VEHICLE_PRIN_OCC_ID = int.Parse(cmbAPP_REC_VEHICLE_PRIN_OCC_ID.SelectedItem.Value);

				if(cmbREC_VEH_ID.SelectedItem!=null) 
					objWatercraftOperatorInfo.REC_VEH_ID=Convert.ToInt32(cmbREC_VEH_ID.SelectedItem.Value==""?"0":cmbREC_VEH_ID.SelectedItem.Value);
			
				if(cmbAPP_REC_VEHICLE_PRIN_OCC_ID.SelectedItem!=null) 
					objWatercraftOperatorInfo.APP_REC_VEHICLE_PRIN_OCC_ID=Convert.ToInt32(cmbAPP_REC_VEHICLE_PRIN_OCC_ID.SelectedItem.Value==""?"0":cmbAPP_REC_VEHICLE_PRIN_OCC_ID.SelectedItem.Value);
			}
			if(cmbVIOLATIONS.SelectedItem!=null && cmbVIOLATIONS.SelectedItem.Value!="")
				objWatercraftOperatorInfo.VIOLATIONS= int.Parse(cmbVIOLATIONS.SelectedItem.Value);
			if(cmbMVR_ORDERED.SelectedItem!=null && cmbMVR_ORDERED.SelectedItem.Value!="")
				objWatercraftOperatorInfo.MVR_ORDERED= int.Parse(cmbMVR_ORDERED.SelectedItem.Value);
			
			if(txtDATE_ORDERED.Text.Trim() !="")
				objWatercraftOperatorInfo.DATE_ORDERED	=	Convert.ToDateTime(txtDATE_ORDERED.Text);
			objWatercraftOperatorInfo.MODIFIED_BY=int.Parse(GetUserId());

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			oldXML		= hidOldData.Value;

			if (hidOldData.Value != "")
			{
				strRowId=hidDRIVER_ID.Value;					
			}
			else
			{
				strRowId="NEW";					
			}	

			//Returning the model object
			return objWatercraftOperatorInfo;
		}
		#endregion

//		#region GetFormValueForUmbrella
//		private ClsUmbrellaOperatorInfo GetFormValueForUmbrella()
//		{
//			ClsUmbrellaOperatorInfo objUmbrellaOperatorInfo	=	new ClsUmbrellaOperatorInfo();			
//			objUmbrellaOperatorInfo.DRIVER_FNAME=	txtDRIVER_FNAME.Text;
//			objUmbrellaOperatorInfo.DRIVER_MNAME=	txtDRIVER_MNAME.Text;
//			objUmbrellaOperatorInfo.DRIVER_LNAME=	txtDRIVER_LNAME.Text;
//			objUmbrellaOperatorInfo.DRIVER_CODE=	txtDRIVER_CODE.Text;
//			objUmbrellaOperatorInfo.DRIVER_SUFFIX=	txtDRIVER_SUFFIX.Text;
//			objUmbrellaOperatorInfo.DRIVER_ADD1=	txtDRIVER_ADD1.Text;
//			objUmbrellaOperatorInfo.DRIVER_ADD2=	txtDRIVER_ADD2.Text;
//			objUmbrellaOperatorInfo.DRIVER_CITY=	txtDRIVER_CITY.Text;
//			objUmbrellaOperatorInfo.DRIVER_STATE=	cmbDRIVER_STATE.SelectedValue;
//			objUmbrellaOperatorInfo.DRIVER_ZIP=	txtDRIVER_ZIP.Text;
//			objUmbrellaOperatorInfo.DRIVER_COUNTRY=	cmbDRIVER_COUNTRY.SelectedValue;
//			
//			if(txtDRIVER_DOB.Text.Trim() !="")
//				objUmbrellaOperatorInfo.DRIVER_DOB	=	Convert.ToDateTime(txtDRIVER_DOB.Text);
//			objUmbrellaOperatorInfo.DRIVER_SSN=	txtDRIVER_SSN.Text;
//
//			if(cmbDRIVER_SEX.SelectedItem.Value != null && cmbDRIVER_SEX.SelectedItem.Text.Trim() != "") 
//				objUmbrellaOperatorInfo.DRIVER_SEX=cmbDRIVER_SEX.SelectedItem.Value;
//			
//			objUmbrellaOperatorInfo.DRIVER_DRIV_LIC=	txtDRIVER_DRIV_LIC.Text;
//			objUmbrellaOperatorInfo.DRIVER_LIC_STATE=	cmbDRIVER_LIC_STATE.SelectedValue;
//			
//			objUmbrellaOperatorInfo.APP_ID =int.Parse(hidAppID.Value);
//			objUmbrellaOperatorInfo.APP_VERSION_ID =int.Parse(hidAppVersionID.Value );
//			objUmbrellaOperatorInfo.CUSTOMER_ID=int.Parse(hidCustomerID.Value);
//
//			//			if(chkDRIVER_COST_GAURAD_AUX.Checked==true)
//			//			{
//			//				objUmbrellaOperatorInfo.DRIVER_COST_GAURAD_AUX=1;
//			//			}
//			//			else
//			//			{
//			//				objUmbrellaOperatorInfo.DRIVER_COST_GAURAD_AUX=0;
//			//			}
//
//			//modified by vj on 20-10-2005
//			objUmbrellaOperatorInfo.DRIVER_COST_GAURAD_AUX = Convert.ToInt32(cmbDRIVER_COST_GAURAD_AUX.SelectedValue);
//
//			if(cmbVEHICLE_ID.SelectedItem!=null) 
//				objUmbrellaOperatorInfo.VEHICLE_ID=cmbVEHICLE_ID.SelectedItem.Value==""?"0":cmbVEHICLE_ID.SelectedItem.Value;
//			
//			//commented by vj on 17-10-2005
//			//if(txtPERCENT_DRIVEN!=null)
//			//	objUmbrellaOperatorInfo.PERCENT_DRIVEN=txtPERCENT_DRIVEN.Text.Trim()==""?0:double.Parse(txtPERCENT_DRIVEN.Text.Trim());  
//
//			//added by vj on 17-10-2005
//			objUmbrellaOperatorInfo.APP_VEHICLE_PRIN_OCC_ID = Convert.ToInt32(cmbAPP_VEHICLE_PRIN_OCC_ID.SelectedValue); 
//			
//			//hard coded value to test driver detail insert and update
//			//			objUmbrellaOperatorInfo.VEHICLE_ID="1";
//			//			objUmbrellaOperatorInfo.PERCENT_DRIVEN=15.5; 
//
//
//			//These  assignments are common to all pages.
//			//strFormSaved	=	hidFormSaved.Value;
//			//strRowId		=	hidDRIVER_ID.Value;
//			//oldXML			= hidOldData.Value;
//			
//			//--------------------------------------------
//			//These  assignments are common to all pages.
//			strFormSaved	=	hidFormSaved.Value;
//			oldXML		= hidOldData.Value;
//			//Returning the model object
//			if (hidOldData.Value != "")
//			{
//				strRowId=hidDRIVER_ID.Value;					
//			}
//			else
//			{
//				strRowId="NEW";					
//			}	
//			//---------------------------------------------
//			return objUmbrellaOperatorInfo;
//		}
//		#endregion

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
				objDriverDetails = new  ClsDriverDetail();
				objDriverDetails.LoggedInUserId = int.Parse(GetUserId());
				ClsUmbrellaOperatorInfo	objUmbrellaOperatorInfo=new ClsUmbrellaOperatorInfo();
				ClsPolicyWatercraftOperatorInfo objWatercraftOperatorInfo=new ClsPolicyWatercraftOperatorInfo() ;

				//Retreiving the form values into model class object
//				if(hidCalledFrom.Value.ToUpper()=="UMB")
//					objUmbrellaOperatorInfo=GetFormValueForUmbrella();
//				else
				objWatercraftOperatorInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
//					if(hidCalledFrom.Value.ToUpper()=="UMB")
//					{
//						#region Insert for UMB
//						objUmbrellaOperatorInfo.CREATED_BY = int.Parse(GetUserId());
//						objUmbrellaOperatorInfo.CREATED_DATETIME = DateTime.Now;
//
//						//Calling the add method of business layer class						
//						intRetVal = objDriverDetails.AddUmbrellaOperatorDetails(objUmbrellaOperatorInfo);
//						if(intRetVal>0)
//						{
//							hidDRIVER_ID.Value = objUmbrellaOperatorInfo.DRIVER_ID.ToString();
//							lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"24");
//							hidFormSaved.Value			=	"1";
//							hidIS_ACTIVE.Value = "Y";
//							btnDelete.Attributes.Add("style","display:inline");  
//							hidOldData.Value = ClsDriverDetail.FetchUmbrellaDriverInfoXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidAppID.Value ==""?"0":hidAppID.Value),int.Parse (hidAppVersionID.Value==""?"0":hidAppVersionID.Value),int.Parse (hidDRIVER_ID.Value==""?"0":hidDRIVER_ID.Value));							
//							SetWorkflow();
//							base.OpenEndorsementDetails();
//						}
//						else if(intRetVal == -1)
//						{
//							lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"25");
//							hidFormSaved.Value			=		"2";
//						}
//						else
//						{
//							lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"26");
//							hidFormSaved.Value			=	"2";
//						}
//						#endregion
//					}
//					else
//					{
					#region Insert for WAT
						objWatercraftOperatorInfo.CREATED_BY = int.Parse(GetUserId());
						objWatercraftOperatorInfo.CREATED_DATETIME = DateTime.Now;

						//Calling the add method of business layer class
						intRetVal = objDriverDetails.AddPolicyWatercraftOperator(objWatercraftOperatorInfo);
						if(intRetVal>0)
						{
							hidDRIVER_ID.Value		= objWatercraftOperatorInfo.DRIVER_ID.ToString();
							lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"24");
							hidFormSaved.Value		=	"1";
							hidIS_ACTIVE.Value		= "Y";
							btnActivateDeactivate.Text="Deactivate";
							btnDelete.Attributes.Add("style","display:inline"); 
							btnActivateDeactivate.Attributes.Add("style","display:inline"); 
							hidOldData.Value = clsWatercraftInformation.FetchPolicyWatercraftOperatorInfoXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPolicyID.Value ==""?"0":hidPolicyID.Value),int.Parse (hidPolicyVersionID.Value==""?"0":hidPolicyVersionID.Value),int.Parse (hidDRIVER_ID.Value==""?"0":hidDRIVER_ID.Value));
							ShowSSN();
							fxnAssignedVehicle();
							if(hidLOB_ID.Value==((int)enumLOB.HOME).ToString())//Done for Itrack Issue 6798 on 18 Dec 09
							{
								fxnAssignedRecreationalVehicle();//Done for Itrack Issue 6737 on 17 Nov 09
							}
							SetWorkflow();
							base.OpenEndorsementDetails();
						}
						else if(intRetVal == -1)
						{
							lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"25");
							hidFormSaved.Value			=		"2";
						}
						else
						{
							lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"26");
							hidFormSaved.Value			=	"2";
						}					
						#endregion
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{
//					if(hidCalledFrom.Value.ToUpper()=="UMB")
//					{
//						//Creating the Model object for holding the Old data						
//						ClsUmbrellaOperatorInfo objOldUmbrellaOperatorInfo;
//						objOldUmbrellaOperatorInfo = new ClsUmbrellaOperatorInfo();
//
//						//Setting  the Old Page details(XML File containing old details) into the Model Object
//						base.PopulateModelObject(objOldUmbrellaOperatorInfo,hidOldData.Value);
//
//						//Setting those values into the Model object which are not in the page
//						objUmbrellaOperatorInfo.DRIVER_ID = int.Parse (strRowId ==""?"0":strRowId);
//						objUmbrellaOperatorInfo.MODIFIED_BY = int.Parse(GetUserId());
//						objUmbrellaOperatorInfo.LAST_UPDATED_DATETIME = DateTime.Now;
//					 
//
//						//Updating the record using business layer class object
//						////intRetVal	= objDriverDetails.UpdateWatercraftOperator(objOldWatercraftOperatorInfo,objWatercraftOperatorInfo);
//						intRetVal	= objDriverDetails.UpdateUmbrellaOperatorDetails(objOldUmbrellaOperatorInfo,objUmbrellaOperatorInfo);
//
//						if( intRetVal > 0 )			// update successfully performed
//						{
//							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"27");
//							hidFormSaved.Value		=	"1";
//							hidOldData.Value = ClsDriverDetail.FetchUmbrellaDriverInfoXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidAppID.Value ==""?"0":hidAppID.Value),int.Parse (hidAppVersionID.Value==""?"0":hidAppVersionID.Value),int.Parse (hidDRIVER_ID.Value==""?"0":hidDRIVER_ID.Value));							
//							SetWorkflow();
//							base.OpenEndorsementDetails();
//
//						}
//						else if(intRetVal == -1)	// Duplicate code exist, update failed
//						{
//							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"25");
//							hidFormSaved.Value		=	"1";
//						}
//						else 
//						{
//							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"26");
//							hidFormSaved.Value		=	"1";
//						}
//					}
//					else
//					{

					//Creating the Model object for holding the Old data
					ClsPolicyWatercraftOperatorInfo objOldWatercraftOperatorInfo = new ClsPolicyWatercraftOperatorInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldWatercraftOperatorInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objWatercraftOperatorInfo.DRIVER_ID = int.Parse (strRowId ==""?"0":strRowId);
					objWatercraftOperatorInfo.MODIFIED_BY = int.Parse(GetUserId());
					objWatercraftOperatorInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					//Added 13 sep 2007
					string strXML = clsWatercraftInformation.FillPolBoats(int.Parse(hidCustomerID.Value)
						, int.Parse(hidPolicyID.Value)
						, int.Parse(hidPolicyVersionID.Value)
						, int.Parse(hidDRIVER_ID.Value=="NEW"?"0":hidDRIVER_ID.Value));

					//Done for Itrack Issue 6737 on 17 Nov 09
					string strRecXML = clsWatercraftInformation.FillPolReacreationalVehicles(int.Parse(hidCustomerID.Value)
						, int.Parse(hidPolicyID.Value)
						, int.Parse(hidPolicyVersionID.Value)
						, int.Parse(hidDRIVER_ID.Value=="NEW"?"0":hidDRIVER_ID.Value));
					//Updating the record using business layer class object
					intRetVal	= objDriverDetails.UpdatePolicyWatercraftOperator(objOldWatercraftOperatorInfo,objWatercraftOperatorInfo,strXML,strRecXML);//Done for Itrack Issue 6737 on 17 Nov 09
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"27");
						hidFormSaved.Value		=	"1";
						hidOldData.Value = clsWatercraftInformation.FetchPolicyWatercraftOperatorInfoXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPolicyID.Value ==""?"0":hidPolicyID.Value),int.Parse (hidPolicyVersionID.Value==""?"0":hidPolicyVersionID.Value),int.Parse (hidDRIVER_ID.Value==""?"0":hidDRIVER_ID.Value));
						ShowSSN();
						fxnAssignedVehicle();
						if(hidLOB_ID.Value==((int)enumLOB.HOME).ToString())//Done for Itrack Issue 6798 on 18 Dec 09
						{
							fxnAssignedRecreationalVehicle();//Done for Itrack Issue 6737 on 17 Nov 09
						}
						SetWorkflow();
						base.OpenEndorsementDetails();

					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"25");
						hidFormSaved.Value		=	"1";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"26");
						hidFormSaved.Value		=	"1";
					}					
					
				}
				lblMessage.Visible = true;
				FillDiscounts();
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"28") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
			}
			finally
			{
				if(objDriverDetails!= null)
					objDriverDetails.Dispose();
			}
		}


		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			objDriverDetails = new  ClsDriverDetail();
		
			ClsPolicyWatercraftOperatorInfo objWatercraftOperatorInfo=new ClsPolicyWatercraftOperatorInfo() ;

			int result;
			try
			{
//				if(hidCalledFrom.Value.ToUpper()=="UMB")
//					result=objDriverDetails.DeleteDriver(int.Parse(hidCustomerID.Value), int.Parse(hidAppID.Value ), int.Parse(hidAppVersionID.Value),int.Parse(hidDRIVER_ID.Value),hidCalledFrom.Value.ToString(),strCalledFor); 
//				else
				objWatercraftOperatorInfo = GetFormValue();
				result=objDriverDetails.DeletePolicyWaterCraftOperator(objWatercraftOperatorInfo,hidCustomInfo.Value); 
				if(result>=1)
				{

					lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","127");
					hidDRIVER_ID.Value=""; 
					hidFormSaved.Value = "5";
					hidOldData.Value = "";
					trBody.Attributes.Add("style","display:none");
					delStr="1";
					SetWorkflow();
					base.OpenEndorsementDetails();
				}
				else if (result == -1)
				{
					lblDelete.Text		=	ClsMessages.GetMessage(base.ScreenId,"128");
					hidFormSaved.Value		=	"2";
				}	
				lblMessage.Visible=false;
				lblDelete.Visible = true;
			}
			catch(Exception exc)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"28") + " - " + exc.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
				hidFormSaved.Value			=	"2";
			}

			
		}
		
		#endregion

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
			capDRIVER_DOB.Text						=		objResourceMgr.GetString("txtDRIVER_DOB");
			capDRIVER_SSN.Text						=		objResourceMgr.GetString("txtDRIVER_SSN");
			capDRIVER_SEX.Text						=		objResourceMgr.GetString("cmbDRIVER_SEX");
			capDRIVER_DRIV_LIC.Text					=		objResourceMgr.GetString("txtDRIVER_DRIV_LIC");
			capDRIVER_LIC_STATE.Text				=		objResourceMgr.GetString("cmbDRIVER_LIC_STATE");
			capDRIVER_COST_GAURAD_AUX.Text			=		objResourceMgr.GetString("txtDRIVER_COST_GAURAD_AUX");
			capVEHICLE_ID.Text						=		objResourceMgr.GetString("cmbVEHICLE_ID");  
			capAPP_VEHICLE_PRIN_OCC_ID.Text			=		objResourceMgr.GetString("cmbAPP_VEHICLE_PRIN_OCC_ID");  			
			capCERT_COAST_GUARD.Text				=		objResourceMgr.GetString("cmbCERT_COAST_GUARD");  
			capWAT_SAFETY_COURSE.Text				=		objResourceMgr.GetString("cmbWAT_SAFETY_COURSE"); 
			capAPP_REC_VEHICLE_PRIN_OCC_ID.Text		=		objResourceMgr.GetString("cmbAPP_REC_VEHICLE_PRIN_OCC_ID");  
			capREC_VEH_ID.Text						=		objResourceMgr.GetString("cmbREC_VEH_ID");  
			capVIOLATIONS.Text						=		objResourceMgr.GetString("cmbVIOLATIONS");  
			capMVR_ORDERED.Text						=		objResourceMgr.GetString("cmbMVR_ORDERED");  
			capDATE_ORDERED.Text					=		objResourceMgr.GetString("txtDATE_ORDERED");  
			capMARITAL_STATUS.Text					=		objResourceMgr.GetString("cmbMARITAL_STATUS");
			capDRIVER_DRIV_TYPE.Text					=		objResourceMgr.GetString("cmbDRIVER_DRIV_TYPE");

		}

		private void GetOldDataXML()
		{
			//If DRIVER_ID is null then it is add mode else edit
			if (Request.QueryString["DRIVER_ID"]!=null && Request.QueryString["DRIVER_ID"].ToString()!="")
			{
				//In case of edit we get these keys as querystring
				if (Request.QueryString["CUSTOMER_ID"]!=null && Request.QueryString["CUSTOMER_ID"].ToString()!="")
				{
					hidCustomerID.Value = Request.QueryString["CUSTOMER_ID"].ToString();
				}

				if (Request.QueryString["POLICY_ID"]!=null && Request.QueryString["POLICY_ID"].ToString()!="")
				{
					hidPolicyID.Value = Request.QueryString["POLICY_ID"].ToString();
				}
				if (Request.QueryString["POLICY_VERSION_ID"]!=null && Request.QueryString["POLICY_VERSION_ID"].ToString()!="")
				{
					hidPolicyVersionID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();
				}				
				hidDRIVER_ID.Value = Request.QueryString["DRIVER_ID"].ToString();

//				if (hidCalledFrom.Value == "UMB")
//				{
//					hidOldData.Value = ClsUmbrellaGen.FetchUmbrellaOperatorInfoXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidAppID.Value ==""?"0":hidAppID.Value),int.Parse (hidAppVersionID.Value==""?"0":hidAppVersionID.Value),int.Parse (hidDRIVER_ID.Value==""?"0":hidDRIVER_ID.Value));
//				}
//				else
//				{
				hidOldData.Value = clsWatercraftInformation.FetchPolicyWatercraftOperatorInfoXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPolicyID.Value ==""?"0":hidPolicyID.Value),int.Parse (hidPolicyVersionID.Value==""?"0":hidPolicyVersionID.Value),int.Parse (hidDRIVER_ID.Value==""?"0":hidDRIVER_ID.Value));
//				}
                if (ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString() != "")//Added By Pradeep Kushwaha on 14-sep-2010
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString());

				btnDelete.Attributes.Add("style","display:inline"); 
				btnActivateDeactivate.Attributes.Add("style","display:inline"); 
				ShowSSN();
			}
			else 
			{
				//In case of add we take these keys from the session
				hidCustomerID.Value = GetCustomerID();
				hidPolicyID.Value =GetPolicyID();
				hidPolicyVersionID.Value =GetPolicyVersionID(); 
				hidDRIVER_ID.Value ="NEW";
				btnDelete.Attributes.Add("style","display:none");
				btnActivateDeactivate.Attributes.Add("style","display:none");  
			}
			if(GetLOBID()!=null && GetLOBID()!="")
				hidLOB_ID.Value = GetLOBID();
			
			if (hidCustomerID.Value != "")
			{
				hidCUSTOMER_INFO.Value=clsWatercraftInformation.GetCustomerNameXML(int.Parse(hidCustomerID.Value));
			}
		}
		private void ShowSSN()
		{
			//added by pravesh for encripted SSN number
			if(hidOldData.Value.IndexOf("NewDataSet") >= 0)
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
			}
		}
		private void FillDiscounts()
		{			
			trDRIVER_DIESEL_DISCOUNT.Attributes.Add("style","display:none");  
			trNAVIGATION_DISCOUNT.Attributes.Add("style","display:none"); 
			trSHORE_STATION_CREDIT.Attributes.Add("style","display:none");  
			trHALON_FIRE_DISCOUNT.Attributes.Add("style","display:none");  
			trMULTIPLE_DISCOUNT.Attributes.Add("style","display:none");  
		}

		private void SetWorkflow()
		{//Added ScreenID for the Operator POL HOME.
			if(base.ScreenId	==	"247_0" || base.ScreenId	==	"167_0" || base.ScreenId	==	"252_0" || base.ScreenId	==	"223_0")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;			
				myWorkFlow.AddKeyValue("CUSTOMER_ID",hidCustomerID.Value);
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule="POL";
				
				if ( hidDRIVER_ID.Value != "" && hidDRIVER_ID.Value.ToUpper() != "NEW" )
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

		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			int intRetval  =  0 ;//Added by Charles on 22-Oct-09 for Itrack 6603
			try
			{
				ClsDriverDetail objDriverDetail	= new ClsDriverDetail();
				
				ClsPolicyWatercraftOperatorInfo objWatercraftOperatorInfo=new ClsPolicyWatercraftOperatorInfo();

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{					
			
					objWatercraftOperatorInfo = GetFormValue();
					intRetval = objDriverDetail.ActivateDeactivatePolicyWatercraftOperator(objWatercraftOperatorInfo,"N",""); //intRetval added by Charles on 22-Oct-09 for Itrack 6603
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
				//	btnActivateDeactivate.Text = "Activate";
					hidIS_ACTIVE.Value="N";

				}
				else
				{									
					
					objWatercraftOperatorInfo = GetFormValue();
					intRetval = objDriverDetail.ActivateDeactivatePolicyWatercraftOperator(objWatercraftOperatorInfo,"Y",""); //intRetval added by Charles on 22-Oct-09 for Itrack 6603
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
				//	btnActivateDeactivate.Text = "Deactivate";
					hidIS_ACTIVE.Value="Y";
				}
			
				hidFormSaved.Value			=	"0";
				
				//Generating the XML again			
				hidOldData.Value = clsWatercraftInformation.FetchPolicyWatercraftOperatorInfoXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPolicyID.Value ==""?"0":hidPolicyID.Value),int.Parse (hidPolicyVersionID.Value==""?"0":hidPolicyVersionID.Value),int.Parse (hidDRIVER_ID.Value==""?"0":hidDRIVER_ID.Value));

                if (ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString() != "")//Added By Pradeep Kushwaha on 14-sep-2010
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString());

				fxnAssignedVehicle();
				fxnAssignedRecreationalVehicle();//Done for Itrack Issue 6737 on 17 Nov 09
				ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid(1," + hidDRIVER_ID.Value + ");</script>");
				if(intRetval > 0 )//Added by Charles on 22-Oct-09 for Itrack 6603
				{
					base.OpenEndorsementDetails();
				}

			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				
			}
			finally
			{
				lblMessage.Visible = true;
							
			}
		} 

		protected void fxnAssignedVehicle()
		{
			int rowCnt;
			int rowCtr;
		
				
			string strXML = clsWatercraftInformation.FillPolBoats(int.Parse(hidCustomerID.Value)
				, int.Parse(hidPolicyID.Value)
				, int.Parse(hidPolicyVersionID.Value)
				, int.Parse(hidDRIVER_ID.Value=="NEW"?"0":hidDRIVER_ID.Value));

			System.Xml.XmlDocument objXmlDoc = new System.Xml.XmlDocument();
			objXmlDoc.LoadXml(strXML);

			int VehicleCount = objXmlDoc.SelectSingleNode("NewDataSet").ChildNodes.Count;
			if(VehicleCount < 1)
			{
				lblVehicleMsg.Text	=  "No boats added until now. Please click <a href='#' onclick='Redirect();'>here</a> to add boats";
				trVehMsg.Attributes.Add ("style","display:inline;"); 
					
			}
			else
			{
				
				trVehMsg.Attributes.Add ("style","display:none;"); 
				
			}
			rowCnt  = VehicleCount;

			IList objList =  Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("WTRPO",null,"Y");

			tblAssignedVeh.GridLines = GridLines.Both; 
			tblAssignedVeh.Attributes.Add("TotalRows",VehicleCount.ToString());

			for(rowCtr=1; rowCtr <= rowCnt; rowCtr++) 
			{
						
				TableRow tRow = new TableRow();	
				tRow.ID = "ID_" + rowCtr;
				tblAssignedVeh.Rows.Add(tRow);		
			
				TableCell tCellDrive  = new TableCell();
				TableCell tCellVeh	  = new TableCell();
				TableCell tCellAs     = new TableCell();
				TableCell tCellDriver = new TableCell();

				Label lblHidVehID	= new Label();
				Label lblDrive		= new Label();
				Label lblVehicle	= new Label();
				Label lblAs			= new Label();
				DropDownList drpDrv	= new DropDownList();

				lblDrive.Text		= " Operates ";
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
				drpDrv.Items.Insert(0,"");		

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
				drpDrv.Attributes.Add("onchange","javascript:return Assigned_Veh();");

					
			}

			
		}
		//Done for Itrack Issue 6737 on 17 Nov 09
		protected void fxnAssignedRecreationalVehicle()
		{
			int rowRecCnt;
			int rowRecCtr;
		
				
			string strRecXML = clsWatercraftInformation.FillPolReacreationalVehicles(int.Parse(hidCustomerID.Value)
				, int.Parse(hidPolicyID.Value)
				, int.Parse(hidPolicyVersionID.Value)
				, int.Parse(hidDRIVER_ID.Value=="NEW"?"0":hidDRIVER_ID.Value));

			System.Xml.XmlDocument objXmlDoc = new System.Xml.XmlDocument();
			objXmlDoc.LoadXml(strRecXML);

			int RecVehicleCount = objXmlDoc.SelectSingleNode("NewDataSet").ChildNodes.Count;
			if(RecVehicleCount < 1)
			{
				lblRecVehicleMsg.Text	=  "No reacreational vehicles added until now. Please click <a href='#' onclick='RedirectToRecVeh();'>here</a> to add reacreational vehicles";
				trRecVehMsg.Attributes.Add ("style","display:inline;"); 
					
			}
			else
			{
				
				trRecVehMsg.Attributes.Add ("style","display:none;"); 
				
			}
			rowRecCnt  = RecVehicleCount;

			IList objList =  Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("WTRPO",null,"Y");

			tblAssignedRecVeh.GridLines = GridLines.Both; 
			tblAssignedRecVeh.Attributes.Add("TotalRecRows",RecVehicleCount.ToString());

			for(rowRecCtr=1; rowRecCtr <= rowRecCnt; rowRecCtr++) 
			{
				TableRow tRecRow = new TableRow();	
				tRecRow.ID = "IDRec_" + rowRecCtr;
				tblAssignedRecVeh.Rows.Add(tRecRow);		
			
				TableCell tCellRecDrive  = new TableCell();
				TableCell tCellRecVeh	  = new TableCell();
				TableCell tCellRecAs     = new TableCell();
				TableCell tCellRecDriver = new TableCell();

				Label lblHidRecVehID	= new Label();
				Label lblRecDrive		= new Label();
				Label lblRecVehicle	= new Label();
				Label lblRecAs			= new Label();
				DropDownList drpDrvRec	= new DropDownList();

				lblRecDrive.Text		= " Operates ";
				lblHidRecVehID.Text	= objXmlDoc.SelectSingleNode("NewDataSet").ChildNodes.Item(rowRecCtr-1).ChildNodes.Item(0).InnerXml;
				lblRecAs.Text			= " as ";
				lblRecVehicle.Text		= objXmlDoc.SelectSingleNode("NewDataSet").ChildNodes.Item(rowRecCtr-1).ChildNodes.Item(1).InnerXml;
						
				tRecRow.CssClass			= "midcolora";
				tCellRecDrive.CssClass		= "midcolora";
				tCellRecVeh.CssClass		= "midcolora";
				tCellRecAs.CssClass		= "midcolora";
				tCellRecDriver.CssClass	= "midcolora";
						
				tCellRecDrive.Width		= Unit.Percentage(10);
				tCellRecVeh.Width			= Unit.Percentage(20);
				tCellRecAs.Width			= Unit.Percentage(5);
				tCellRecDriver.Width		= Unit.Percentage(32);

				lblHidRecVehID.Visible	=  false;

				drpDrvRec	= new DropDownList();
				drpDrvRec.DataSource=objList;
				drpDrvRec.DataTextField	= "LookupDesc";
				drpDrvRec.DataValueField	= "LookupID";
				drpDrvRec.DataBind();						
				drpDrvRec.Items.Insert(0,"");		

				string strSelectedValInDrpRec = objXmlDoc.SelectSingleNode("NewDataSet").ChildNodes.Item(rowRecCtr-1).ChildNodes.Item(3).InnerXml;
				ListItem LIRec;
				LIRec = drpDrvRec.Items.FindByValue(strSelectedValInDrpRec);
				if (LIRec  != null)
					LIRec.Selected=true;
				else
					drpDrvRec.SelectedIndex=-1;
					
				tCellRecDrive.Controls.Add (lblRecDrive);
				tCellRecVeh.Controls.Add (lblRecVehicle);
				tCellRecVeh.Controls.Add (lblHidRecVehID);
				tCellRecAs.Controls.Add (lblRecAs);
				tCellRecDriver.Controls.Add (drpDrvRec);

				tRecRow.Cells.Add(tCellRecDrive);
				tRecRow.Cells.Add(tCellRecVeh);
				tRecRow.Cells.Add(tCellRecAs);
				tRecRow.Cells.Add(tCellRecDriver);

				tRecRow.Attributes.Add("RowRecVehID",lblHidRecVehID.Text);
				drpDrvRec.Attributes.Add("onchange","javascript:return AssignedRec_Veh();");

					
			}

			
		}

	
	}
}

