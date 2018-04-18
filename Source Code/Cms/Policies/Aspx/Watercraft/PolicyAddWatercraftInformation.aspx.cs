/******************************************************************************************
<Author					: - Vijay Arora
<Start Date				: -	21-11-2005
<End Date				: -	
<Description			: - Class for Add / Edit / Delete of WaterCraft Rating Info.
<Review Date			: - 
<Reviewed By			: - 	

<Modified Date			: - 23 May. 2006
<Modified By			: - RPSINGH
<Purpose				: - This page is called from different places. 
							In each case Base SCREEN ID is different.
							Due to this error messages being fetched are coming wrong.
							As discussed Screen ID will be passed hard coded to get error messages
							Water craft -> 246_0
							
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
using Cms.CmsWeb.Controls;
using Cms.Policies.Aspx;
using Cms.Policies.aspx;
using Cms.BusinessLayer.BlApplication;
using Cms.CmsWeb;
using Cms.ExceptionPublisher.ExceptionManagement;
//using Cms.Model.Application.Watercrafts;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Policy.Watercraft; 

namespace Cms.Policies.Aspx.Watercrafts
{
	/// <summary>
	/// This is the presentation layer for the  Watercraft Information
	/// </summary>
	public class PolicyAddWatercraftInformation : Cms.Policies.policiesbase
	{
		
		#region Page controls declaration
		protected System.Web.UI.WebControls.Label capOTHER_POLICY;
		protected System.Web.UI.WebControls.DropDownList cmbOTHER_POLICY;
		protected System.Web.UI.WebControls.Label capIS_BOAT_EXCLUDED;
		protected System.Web.UI.WebControls.DropDownList cmbIS_BOAT_EXCLUDED;

		protected System.Web.UI.WebControls.TextBox txtBOAT_NO;
		protected System.Web.UI.WebControls.TextBox txtYEAR;
		protected System.Web.UI.WebControls.TextBox txtMAKE;
		protected System.Web.UI.WebControls.TextBox txtMODEL;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidWaterNavigateID;
		protected System.Web.UI.WebControls.DropDownList cmbSTATE_REG;
		protected System.Web.UI.WebControls.TextBox txtREG_NO;
		protected System.Web.UI.WebControls.DropDownList cmbPOWER;
		protected System.Web.UI.WebControls.DropDownList cmbHULL_TYPE;
		
		protected System.Web.UI.WebControls.DropDownList cmbHULL_MATERIAL;
		protected System.Web.UI.WebControls.DropDownList cmbFUEL_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbHULL_DESIGN;
		protected System.Web.UI.WebControls.TextBox txtDATE_PURCHASED;
		protected System.Web.UI.WebControls.TextBox txtLENGTH;
		protected System.Web.UI.WebControls.TextBox txtMAX_SPEED;
		protected System.Web.UI.WebControls.TextBox txtCOST_NEW;
		protected System.Web.UI.WebControls.TextBox txtPRESENT_VALUE;
		protected System.Web.UI.WebControls.TextBox txtBERTH_LOC;
		

		protected System.Web.UI.WebControls.Label  capUSED_PARTICIPATE;
		protected System.Web.UI.WebControls.DropDownList cmbUSED_PARTICIPATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAKE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSED_PARTICIPATE;
		protected System.Web.UI.WebControls.Label  capWATERCRAFT_CONTEST;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvWATERCRAFT_CONTEST;
		protected System.Web.UI.WebControls.TextBox txtWATERCRAFT_CONTEST;

		protected System.Web.UI.WebControls.Label  capBOAT_NO;
		protected System.Web.UI.WebControls.Label  capYEAR;
		protected System.Web.UI.WebControls.Label  capMAKE;
		protected System.Web.UI.WebControls.Label  capMODEL;
		protected System.Web.UI.WebControls.Label  capSTATE_REG;
		protected System.Web.UI.WebControls.Label  capREG_NO;
		protected System.Web.UI.WebControls.Label  capPOWER;
		protected System.Web.UI.WebControls.Label  capHULL_TYPE;
		
		protected System.Web.UI.WebControls.Label  capHULL_MATERIAL;
		protected System.Web.UI.WebControls.Label  capFUEL_TYPE;
		protected System.Web.UI.WebControls.Label  capHULL_DESIGN;
		protected System.Web.UI.WebControls.Label  capDATE_PURCHASED;
		protected System.Web.UI.WebControls.Label  capLENGTH;
		protected System.Web.UI.WebControls.Label  capMAX_SPEED;
		protected System.Web.UI.WebControls.Label  capCOST_NEW;
		protected System.Web.UI.WebControls.Label  capPRESENT_VALUE;
		protected System.Web.UI.WebControls.Label  capWATERS_NAVIGATED;
		protected System.Web.UI.WebControls.Label  capTERRITORY;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidUserID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomer;
		//protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppID;
		//protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidBOAT_ID,hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAlert;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidBoatAge;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAX_SPEED;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTERRITORY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBOAT_NO;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvYEAR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOWER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHULL_TYPE;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvHULL_MATERIAL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLENGTH;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOST_NEW;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPRESENT_VALUE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvWATERS_NAVIGATED;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDATE_PURCHASED;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLENGTH;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMAX_SPEED;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCOST_NEW;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPRESENT_VALUE;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capINCHES;
		protected System.Web.UI.WebControls.TextBox txtINCHES;
		protected System.Web.UI.WebControls.RegularExpressionValidator revINCHES;
		protected System.Web.UI.WebControls.Label capLORAN_NAV_SYSTEM;
		protected System.Web.UI.WebControls.DropDownList cmbLORAN_NAV_SYSTEM;
		protected System.Web.UI.WebControls.Label capDIESEL_ENGINE;
		protected System.Web.UI.WebControls.DropDownList cmbDIESEL_ENGINE;
		protected System.Web.UI.WebControls.Label capSHORE_STATION;
		protected System.Web.UI.WebControls.DropDownList cmbSHORE_STATION;
		protected System.Web.UI.WebControls.Label capHALON_FIRE_EXT_SYSTEM;
		protected System.Web.UI.WebControls.DropDownList cmbHALON_FIRE_EXT_SYSTEM;
		protected System.Web.UI.WebControls.Label capDUAL_OWNERSHIP;
		protected System.Web.UI.WebControls.DropDownList cmbDUAL_OWNERSHIP;
		protected System.Web.UI.WebControls.Label capREMOVE_SAILBOAT;
		protected System.Web.UI.WebControls.DropDownList cmbREMOVE_SAILBOAT;
		protected System.Web.UI.WebControls.RangeValidator rngINCHES;
		protected System.Web.UI.WebControls.RegularExpressionValidator revBOAT_NO;

		protected System.Web.UI.WebControls.TextBox txtLOCATION_ZIP;
		protected System.Web.UI.WebControls.TextBox txtLOCATION_ADDRESS;
		protected System.Web.UI.WebControls.TextBox txtLOCATION_CITY;

		protected System.Web.UI.WebControls.Label capLOCATION_ZIP;
		protected System.Web.UI.WebControls.Label capLOCATION_STATE;
		protected System.Web.UI.WebControls.Label capLOCATION_CITY;
		protected System.Web.UI.WebControls.Label capLOCATION_ADDRESS;
		protected System.Web.UI.WebControls.Label capLAY_UP_PERIOD_FROM;
		protected System.Web.UI.WebControls.Label capLAY_UP_PERIOD_TO;

		protected System.Web.UI.WebControls.DropDownList cmbLOCATION_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbLAY_UP_PERIOD_FROM_DAY;
		protected System.Web.UI.WebControls.DropDownList cmbLAY_UP_PERIOD_FROM_MONTH;
		protected System.Web.UI.WebControls.DropDownList cmbLAY_UP_PERIOD_TO_DAY;
		protected System.Web.UI.WebControls.DropDownList cmbLAY_UP_PERIOD_TO_MONTH;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOCATION_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLOCATION_ZIP;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOCATION_CITY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOCATION_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOCATION_ADDRESS;
		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerAddress;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerCity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerState;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerZip;
		protected Cms.CmsWeb.Controls.CmsButton btnPullCustomerAddress;
		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolEffDate;
		protected System.Web.UI.WebControls.CustomValidator csvLOCATION_ZIP;

		#endregion

		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
	 
		protected System.Web.UI.WebControls.HyperLink hlkPURCHASE_DATE;
		protected System.Web.UI.WebControls.CustomValidator csvDATE_PURCHASED;
		protected System.Web.UI.WebControls.RangeValidator rngYEAR;
		protected const string CALLED_FROM_UMBRELLA="UMB";
		protected int intRetVal;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_LOB;
		protected System.Web.UI.WebControls.Label capTYPE_OF_WATERCRAFT;
		protected System.Web.UI.WebControls.DropDownList cmbTYPE_OF_WATERCRAFT;
		protected System.Web.UI.WebControls.Label capINSURING_VALUE;
		protected System.Web.UI.WebControls.TextBox txtINSURING_VALUE;
		protected System.Web.UI.WebControls.Label capWATERCRAFT_LENGTH;
		protected System.Web.UI.WebControls.TextBox txtWATERCRAFT_LENGTH;
		protected System.Web.UI.WebControls.Label capWATERCRAFT_HORSE_POWER;
		protected System.Web.UI.WebControls.RegularExpressionValidator revWATERCRAFT_HORSE_POWER;
		protected System.Web.UI.WebControls.RegularExpressionValidator revWATERCRAFT_LENGTH;
		protected System.Web.UI.WebControls.TextBox txtWATERCRAFT_HORSE_POWER;
		protected System.Web.UI.WebControls.RegularExpressionValidator revINSURING_VALUE;
		protected System.Web.UI.WebControls.Label capTWIN_SINGLE;
		protected System.Web.UI.WebControls.DropDownList cmbTWIN_SINGLE;
		protected System.Web.UI.WebControls.TextBox txtSERIAL_NO;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSERIAL_NO;
		protected System.Web.UI.WebControls.Label capSERIAL_NO;
		protected System.Web.UI.WebControls.DropDownList cmbWATERS_NAVIGATED;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATE_REG;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvINSURING_VALUE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvTYPE_OF_WATERCRAFT;
		//protected System.Web.UI.WebControls.Label capDESC_OTHER_WATERCRAFT;
		protected System.Web.UI.WebControls.TextBox txtDESC_OTHER_WATERCRAFT;
		//protected System.Web.UI.WebControls.Label lblDESC_OTHER_WATERCRAFT;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_OTHER_WATERCRAFT;
		protected System.Web.UI.WebControls.TextBox txtTERRITORY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revTERRITORY;
		protected System.Web.UI.WebControls.DropDownList cmbTERRITORY;
		protected System.Web.UI.WebControls.Label capFEET;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvWATERCRAFT_HORSE_POWER;
	
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		
		//Defining the business layer class object
		Cms.BusinessLayer.BlApplication.clsWatercraftInformation objWatercraftInformation ;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppVersionID;
		protected System.Web.UI.WebControls.Label capCOV_TYPE_BASIS;
		protected System.Web.UI.WebControls.DropDownList cmbCOV_TYPE_BASIS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOV_TYPE_BASIS;
		protected System.Web.UI.WebControls.Label capPHOTO_ATTACHED;
		protected System.Web.UI.WebControls.DropDownList cmbPHOTO_ATTACHED;
		protected System.Web.UI.WebControls.Label capMARINE_SURVEY;
		protected System.Web.UI.WebControls.DropDownList cmbMARINE_SURVEY;
		protected System.Web.UI.WebControls.Label capDATE_MARINE_SURVEY;
		protected System.Web.UI.WebControls.TextBox txtDATE_MARINE_SURVEY;
		protected System.Web.UI.WebControls.HyperLink hlkDATE_MARINE_SURVEY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDATE_MARINE_SURVEY;
		protected System.Web.UI.WebControls.CustomValidator csvDATE_MARINE_SURVEY;
		protected System.Web.UI.WebControls.RangeValidator rngLENGTH;
		protected System.Web.UI.WebControls.RangeValidator rngINSURING_VALUE;
		protected System.Web.UI.WebControls.Label capPullCustomerAddress;
		protected System.Web.UI.WebControls.Label capLOSSREPORT_ORDER;
		protected System.Web.UI.WebControls.DropDownList cmbLOSSREPORT_ORDER;
		protected System.Web.UI.WebControls.Label capLOSSREPORT_DATETIME;
		protected System.Web.UI.WebControls.TextBox txtLOSSREPORT_DATETIME;
		protected System.Web.UI.WebControls.HyperLink hlkCalandarDate2;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLOSSREPORT_DATETIME;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomInfo;
		Cms.BusinessLayer.BlApplication.clsWatercraftInformation objWatercraft	= new clsWatercraftInformation();
				
		//END:*********** Local variables *************

		public string strErrMessageAgreedValueLength		= "";
		public string strErrMessageAgreedValueInsuredVal	= "";
		public string strErrMessageAgreedValueAge			= "";
		public string strCust_Info                          = "";  
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvHULL_MATERIAL;
	
		public string strErrMessageAgreedValueMessage		= "";
		
		#endregion

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			csvLOCATION_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("898");
			rfvBOAT_NO.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage("246_0","2");
			rfvYEAR.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage("246_0","1");
			rfvHULL_MATERIAL.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage("246_0","7");
			rfvLENGTH.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage("246_0","8");
			rfvMAX_SPEED.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage("246_0","9");
			rfvCOV_TYPE_BASIS.ErrorMessage		=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("765");				
			rfvTERRITORY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("764");
			rfvWATERS_NAVIGATED.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage("246_0","12");
			
			revINCHES.ValidationExpression				=	  aRegExpInteger;
			revSERIAL_NO.ValidationExpression			=	aRegExpAlphaNumSpaceStrict;
			revDATE_PURCHASED.ValidationExpression		=	aRegExpDate ;
			revDATE_MARINE_SURVEY.ValidationExpression	=	aRegExpDate ;
			revLENGTH.ValidationExpression				=	aRegExpInteger;
			
			revLENGTH.ErrorMessage						=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("778"); 
			revINCHES.ErrorMessage						=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("779"); 

			revMAX_SPEED.ValidationExpression			=	aRegExpInteger ;
			revWATERCRAFT_HORSE_POWER.ValidationExpression = aRegExpInteger;
			revINSURING_VALUE.ValidationExpression		= aRegExpCurrencyformat;
			revSERIAL_NO.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage("246_0","13" );
			revDATE_PURCHASED.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage("246_0","14");
			revDATE_MARINE_SURVEY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage("246_0","14");
			rfvWATERCRAFT_HORSE_POWER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("306_6_0","8");
			
			revMAX_SPEED.ErrorMessage					=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage ("216"); 
			revWATERCRAFT_HORSE_POWER.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage("246_0","27");
			revINSURING_VALUE.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage("246_0","28");

			csvDATE_PURCHASED.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage("246_0","19");
			csvDATE_MARINE_SURVEY.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage("246_0","35");
			rngYEAR.MaximumValue					= DateTime.Now.AddYears(1).Year.ToString();
			rngYEAR.ErrorMessage					=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("673");
			rfvSTATE_REG.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage("246_0","31");
			rfvINSURING_VALUE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage("246_0","32");
			rfvTYPE_OF_WATERCRAFT.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage("246_0","33");
			rngLENGTH.ErrorMessage					= Cms.CmsWeb.ClsMessages.GetMessage("246_0","37"); 
			
			rngINCHES.ErrorMessage					=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("627");

			rngINCHES.MinimumValue ="1";
			rngINCHES.MaximumValue ="11";
			rngINSURING_VALUE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("246_0","36");
			revBOAT_NO.ValidationExpression  = aRegExpInteger ;
			revBOAT_NO.ErrorMessage          =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");

			strErrMessageAgreedValueLength		= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("767");
			strErrMessageAgreedValueInsuredVal	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("768");
			strErrMessageAgreedValueAge			= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("769");
			strErrMessageAgreedValueMessage		= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("770");
			rfvUSED_PARTICIPATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("83_0","41");
			rfvWATERCRAFT_CONTEST.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage("83_0","42");
			rfvMAKE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("168");
			revLOSSREPORT_DATETIME.ValidationExpression		=	aRegExpDate;
		}	
		#endregion

		public string lob="WAT";

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{	
			Ajax.Utility.RegisterTypeForAjax(typeof(PolicyAddWatercraftInformation));  
			
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");			
			btnPullCustomerAddress.Attributes.Add("onClick","javascript:PullCustomerAddress();return false;");

			hlkPURCHASE_DATE.Attributes.Add("OnClick","fPopCalendar(document.APP_WATERCRAFT_INFO.txtDATE_PURCHASED,document.APP_WATERCRAFT_INFO.txtDATE_PURCHASED)"); //Javascript Implementation for Calender				
			hlkDATE_MARINE_SURVEY.Attributes.Add("OnClick","fPopCalendar(document.APP_WATERCRAFT_INFO.txtDATE_MARINE_SURVEY,document.APP_WATERCRAFT_INFO.txtDATE_MARINE_SURVEY)"); //Javascript Implementation for Calender				
			hlkCalandarDate2.Attributes.Add("OnClick","fPopCalendar(document.APP_WATERCRAFT_INFO.txtLOSSREPORT_DATETIME,document.APP_WATERCRAFT_INFO.txtLOSSREPORT_DATETIME)");  	
			
			
			
			string url=ClsCommon.GetLookupURL();

			cmbTYPE_OF_WATERCRAFT.Attributes.Add("onchange","javascript: Check();return ShowHideDiscounts('NEW');");

			#region SETTING SCREEN ID
			if(GetLOBString()=="WAT")
			{
				//base.ScreenId="72_0"; 
				base.ScreenId="246_0"; 
				lob="WWAT";
				
			}
			else if(GetLOBString()=="HOME")
			{
				//base.ScreenId="148_0"; 
				base.ScreenId="251_0"; 
				lob="HWAT";
			}
			else if(GetLOBString()=="UMB")
			{
				base.ScreenId="277_0";
				lob="UWAT";
			}
			else if(GetLOBString()=="RENT")
			{
				base.ScreenId="166_0"; 				
				lob="RWAT";
			}
			#endregion

			lblMessage.Visible = false;
			SetErrorMessages();

			//Added by Swastika on 7th Mar'06 for Pol Iss #59
			if (Request.Form["__EVENTTARGET"] == "Deactive")
			{   
				DoInActive(hidAlert.Value);
			}

			if (Request.Form["__EVENTTARGET"] == "DeleteVehicle")
			{   
				DoDelete(hidAlert.Value);
			}


			//*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass			=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass			=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnDelete.CmsButtonClass        = CmsButtonType.Delete;
			btnDelete.PermissionString      = gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;

			btnPullCustomerAddress.CmsButtonClass = CmsButtonType.Write;
			btnPullCustomerAddress.PermissionString = gstrSecurityXML;
			btnPullCustomerAddress.CausesValidation=false;



			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.Watercrafts.PolicyAddWatercraftInformation" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				//Added by Asfa (26-June-2008) - iTrack #4200
				txtLOCATION_ZIP.Attributes.Add("onBlur","javascript:return Validate();");

				cmbUSED_PARTICIPATE.Attributes.Add("onChange","javascript:USED_PARTICIPATE_Change();");
				cmbFUEL_TYPE.Attributes.Add("onChange","DieselEngineDiscount('NEW');");
				hidCustomerID.Value =GetCustomerID();

				if(Request.QueryString["CalledFrom"]!=null )
					hidCalledFrom.Value=Request.QueryString["CalledFrom"].ToString(); 
				
				if(Request.QueryString["BOATID"]!=null )
					hidBOAT_ID.Value=Request.QueryString["BOATID"].ToString(); 

				if(hidCalledFrom.Value!=CALLED_FROM_UMBRELLA)
					this.btnSave.Attributes.Add("onClick","javascript:CountWaterNavigate();HandleSaleBoat();return WatercraftAgeOnClick();");
               

				GetOldDataXML();
				
				SetCaptions();

				#region "Loading singleton"
				DataTable dt = Cms.CmsWeb.ClsFetcher.State;
				cmbSTATE_REG.DataSource		= dt;
				cmbSTATE_REG.DataTextField	= "State_Name";
				cmbSTATE_REG.DataValueField	= "State_Id";
				cmbSTATE_REG.DataBind();
				cmbSTATE_REG.Items.Insert(0,"");
	
				//Added by Mohit Agarwal 30-Oct-2007
				cmbLOSSREPORT_ORDER.DataSource		= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
				cmbLOSSREPORT_ORDER.DataTextField	= "LookupDesc";
				cmbLOSSREPORT_ORDER.DataValueField	= "LookupID";
				cmbLOSSREPORT_ORDER.DataBind();
				cmbLOSSREPORT_ORDER.Items.Insert(0,"");
				#endregion 

				FillControls();

				#region Set Workflow Control
				SetWorkflow();
				#endregion
				getPolicyEffectiveYear();

                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value.ToString()));
			}

			//TO FETCH CLIENT ADDRESS, CITY, ZIP AND STATE IN HIDDEN VARIABLES
			getCustomerAddress(GetCustomerID());
		} 
		#endregion
				
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsPolicyWatercraftInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsPolicyWatercraftInfo objWatercraftInfo = new ClsPolicyWatercraftInfo();
			
			objWatercraftInfo.BOAT_NO=	int.Parse(txtBOAT_NO.Text.Trim()==""?"0":txtBOAT_NO.Text.Trim());
			objWatercraftInfo.YEAR=	int.Parse(txtYEAR.Text.Trim() ==""?"0":txtYEAR.Text.Trim());
			objWatercraftInfo.MAKE=	txtMAKE.Text;
			objWatercraftInfo.MODEL=	txtMODEL.Text;
			objWatercraftInfo.HULL_ID_NO=	txtSERIAL_NO.Text;
			objWatercraftInfo.STATE_REG=	cmbSTATE_REG.SelectedValue;          
			//hidCustomInfo.Value=";Boat # = " + txtBOAT_NO.Text + ";Boat Make = " + txtMAKE.Text + ";Boat Model = " + txtMODEL.Text;			 
			if(cmbTYPE_OF_WATERCRAFT.SelectedItem !=null)
			{
				objWatercraftInfo.TYPE_OF_WATERCRAFT=	cmbTYPE_OF_WATERCRAFT.SelectedItem.Value ;
			}

			
			if(cmbHULL_MATERIAL.SelectedItem !=null)
			{
				objWatercraftInfo.HULL_MATERIAL=	int.Parse(cmbHULL_MATERIAL.SelectedItem.Value==""?"0":cmbHULL_MATERIAL.SelectedItem.Value);
			}
			if(cmbFUEL_TYPE.SelectedItem!=null)
			{
				objWatercraftInfo.FUEL_TYPE=	int.Parse (cmbFUEL_TYPE.SelectedItem.Value==""?"0":cmbFUEL_TYPE.SelectedItem.Value);
			}
		
			if(txtDATE_PURCHASED.Text.Trim()!="")
			{
				objWatercraftInfo.DATE_PURCHASED=	Convert.ToDateTime(txtDATE_PURCHASED.Text);
			}
			if(txtWATERCRAFT_HORSE_POWER.Text.Trim()!="")
			{
				objWatercraftInfo.WATERCRAFT_HORSE_POWER=	int.Parse (txtWATERCRAFT_HORSE_POWER.Text.Trim());
			}

			if(txtINSURING_VALUE.Text.Trim()!="")
			{
				objWatercraftInfo.INSURING_VALUE=	double.Parse (txtINSURING_VALUE.Text.Trim()==""?"0":txtINSURING_VALUE.Text.Trim()); 
			}
			objWatercraftInfo.LENGTH=	txtLENGTH.Text;
			objWatercraftInfo.INCHES=	txtINCHES.Text;
			
			objWatercraftInfo.MAX_SPEED=	double.Parse (txtMAX_SPEED.Text.Trim()==""?"0":txtMAX_SPEED.Text.Trim());
	
			objWatercraftInfo.BERTH_LOC=	"";

			// Added by Swastika on 7th Mar'06 for Pol Iss #59
			if (hidBOAT_ID.Value.ToString().ToUpper()=="NEW")
				objWatercraftInfo.BOAT_ID = 0;
			else
			{
				objWatercraftInfo.BOAT_ID = int.Parse (hidBOAT_ID.Value.ToString());
			}
			
			objWatercraftInfo.CUSTOMER_ID = int.Parse(hidCustomerID.Value);
			objWatercraftInfo.POLICY_ID = int.Parse(hidPOLICY_ID.Value);
			objWatercraftInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);

			if(cmbWATERS_NAVIGATED.SelectedItem!=null)
			{
				objWatercraftInfo.WATERS_NAVIGATED=	hidWaterNavigateID.Value;
			}

			if(cmbTWIN_SINGLE.SelectedItem!=null)
			{
				objWatercraftInfo.TWIN_SINGLE=	int.Parse (cmbTWIN_SINGLE.SelectedItem.Value==""?"0":cmbTWIN_SINGLE.SelectedItem.Value);
			}

			if (cmbTYPE_OF_WATERCRAFT.SelectedValue == "11439")
			{
				objWatercraftInfo.DESC_OTHER_WATERCRAFT=txtDESC_OTHER_WATERCRAFT.Text;
			}
			else
			{			
				objWatercraftInfo.DESC_OTHER_WATERCRAFT="";
			}

			
			if(cmbTERRITORY.SelectedItem!=null)
			{
				objWatercraftInfo.TERRITORY=cmbTERRITORY.SelectedItem.Value==""?"0":cmbTERRITORY.SelectedItem.Value;
			}
			
			if(cmbLORAN_NAV_SYSTEM.SelectedItem!=null)
				objWatercraftInfo.LORAN_NAV_SYSTEM=int.Parse(cmbLORAN_NAV_SYSTEM.SelectedItem.Value==""?"0":cmbLORAN_NAV_SYSTEM.SelectedItem.Value);

			if(cmbFUEL_TYPE.SelectedIndex == 1) // If FUEL_TYPE is Diesel, DIESEL_ENGINE will be set to YES else NO
				objWatercraftInfo.DIESEL_ENGINE=int.Parse(cmbDIESEL_ENGINE.Items[2].Value);
			else
				objWatercraftInfo.DIESEL_ENGINE=int.Parse(cmbDIESEL_ENGINE.Items[1].Value);

			if(cmbSHORE_STATION.SelectedItem!=null)
				objWatercraftInfo.SHORE_STATION=int.Parse(cmbSHORE_STATION.SelectedItem.Value==""?"0":cmbSHORE_STATION.SelectedItem.Value);

			if(cmbHALON_FIRE_EXT_SYSTEM.SelectedItem!=null)
				objWatercraftInfo.HALON_FIRE_EXT_SYSTEM=int.Parse(cmbHALON_FIRE_EXT_SYSTEM.SelectedItem.Value==""?"0":cmbHALON_FIRE_EXT_SYSTEM.SelectedItem.Value);

			if(cmbDUAL_OWNERSHIP.SelectedItem!=null)
				objWatercraftInfo.DUAL_OWNERSHIP=int.Parse(cmbDUAL_OWNERSHIP.SelectedItem.Value==""?"0":cmbDUAL_OWNERSHIP.SelectedItem.Value);

			if(cmbREMOVE_SAILBOAT.SelectedItem!=null)
				objWatercraftInfo.REMOVE_SAILBOAT=int.Parse(cmbREMOVE_SAILBOAT.SelectedItem.Value==""?"0":cmbREMOVE_SAILBOAT.SelectedItem.Value);

			if(cmbCOV_TYPE_BASIS.SelectedItem!=null)
			{
				objWatercraftInfo.COV_TYPE_BASIS=	int.Parse (cmbCOV_TYPE_BASIS.SelectedItem.Value==""?"0":cmbCOV_TYPE_BASIS.SelectedItem.Value);
			}
			if(cmbPHOTO_ATTACHED.SelectedItem!=null)
			objWatercraftInfo.PHOTO_ATTACHED=int.Parse(cmbPHOTO_ATTACHED.SelectedItem.Value==""?"0":cmbPHOTO_ATTACHED.SelectedItem.Value);
			
			if(cmbMARINE_SURVEY.SelectedItem!=null)
				objWatercraftInfo.MARINE_SURVEY=int.Parse(cmbMARINE_SURVEY.SelectedItem.Value==""?"0":cmbMARINE_SURVEY.SelectedItem.Value);
			
			if(txtDATE_MARINE_SURVEY.Text.Trim()!="")
			{
				objWatercraftInfo.DATE_MARINE_SURVEY =	Convert.ToDateTime(txtDATE_MARINE_SURVEY.Text);
			}



			objWatercraftInfo.MODIFIED_BY = int.Parse(GetUserId());
			objWatercraftInfo.LAST_UPDATED_DATETIME = DateTime.Now;
			objWatercraftInfo.CREATED_BY = int.Parse(GetUserId());
			objWatercraftInfo.CREATED_DATETIME = DateTime.Now;

			objWatercraftInfo.LOCATION_ADDRESS	= txtLOCATION_ADDRESS.Text; 
			objWatercraftInfo.LOCATION_CITY		= txtLOCATION_CITY.Text;
			objWatercraftInfo.LOCATION_STATE	= cmbLOCATION_STATE.SelectedItem.Value;
			objWatercraftInfo.LOCATION_ZIP		= txtLOCATION_ZIP.Text;
			
			if(cmbLAY_UP_PERIOD_FROM_DAY.SelectedItem!=null)
				objWatercraftInfo.LAY_UP_PERIOD_FROM_DAY	= int.Parse(cmbLAY_UP_PERIOD_FROM_DAY.SelectedItem.Value==""?"0":cmbLAY_UP_PERIOD_FROM_DAY.SelectedItem.Value); 

			if(cmbLAY_UP_PERIOD_FROM_MONTH.SelectedItem!=null)
				objWatercraftInfo.LAY_UP_PERIOD_FROM_MONTH	= int.Parse(cmbLAY_UP_PERIOD_FROM_MONTH.SelectedItem.Value==""?"0":cmbLAY_UP_PERIOD_FROM_MONTH.SelectedItem.Value);

			if(cmbLAY_UP_PERIOD_TO_DAY.SelectedItem!=null)
				objWatercraftInfo.LAY_UP_PERIOD_TO_DAY		= int.Parse(cmbLAY_UP_PERIOD_TO_DAY.SelectedItem.Value==""?"0":cmbLAY_UP_PERIOD_TO_DAY.SelectedItem.Value);

			if(cmbLAY_UP_PERIOD_TO_MONTH.SelectedItem!=null)
				objWatercraftInfo.LAY_UP_PERIOD_TO_MONTH	= int.Parse(cmbLAY_UP_PERIOD_TO_MONTH.SelectedItem.Value==""?"0":cmbLAY_UP_PERIOD_TO_MONTH.SelectedItem.Value);

			if(cmbLOSSREPORT_ORDER.SelectedValue != "")
				objWatercraftInfo.LOSSREPORT_ORDER = int.Parse(cmbLOSSREPORT_ORDER.SelectedValue);

			if(txtLOSSREPORT_DATETIME.Text.Trim() != "")
				objWatercraftInfo.LOSSREPORT_DATETIME = DateTime.Parse(txtLOSSREPORT_DATETIME.Text.Trim());

			
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidBOAT_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objWatercraftInfo;
		}		

		private Cms.Model.Policy.Umbrella.ClsWaterCraftInfo GetFormValueUmb()
		{
			//Creating the Model object for holding the New data
			Cms.Model.Policy.Umbrella.ClsWaterCraftInfo objWatercraftInfo = new Cms.Model.Policy.Umbrella.ClsWaterCraftInfo();			
			
			objWatercraftInfo.BOAT_NO=	int.Parse(txtBOAT_NO.Text.Trim()==""?"0":txtBOAT_NO.Text.Trim());
			objWatercraftInfo.YEAR=	int.Parse(txtYEAR.Text.Trim() ==""?"0":txtYEAR.Text.Trim());
			objWatercraftInfo.MAKE=	txtMAKE.Text;
			objWatercraftInfo.MODEL=	txtMODEL.Text;
			string boat_no = txtBOAT_NO.Text.Trim();
			string boat_make = txtMAKE.Text.Trim();
			string boat_model = txtMODEL.Text.Trim();
			strCust_Info = " ;Boat  # = " +  boat_no + ";Boat_make = " + boat_make + ";Boat_model = " + boat_model;                 
			/*objWatercraftInfo.HULL_ID_NO=	txtSERIAL_NO.Text;
			objWatercraftInfo.STATE_REG=	cmbSTATE_REG.SelectedValue;
			objWatercraftInfo.LOCATION_ADDRESS	= txtLOCATION_ADDRESS.Text; 
			objWatercraftInfo.LOCATION_CITY		= txtLOCATION_CITY.Text;
			objWatercraftInfo.LOCATION_STATE	= cmbLOCATION_STATE.SelectedItem.Value;
			objWatercraftInfo.LOCATION_ZIP		= txtLOCATION_ZIP.Text;*/
//			if(cmbCOV_TYPE_BASIS.SelectedItem!=null)
//			{
//				objWatercraftInfo.COV_TYPE_BASIS=	int.Parse (cmbCOV_TYPE_BASIS.SelectedItem.Value==""?"0":cmbCOV_TYPE_BASIS.SelectedItem.Value);
//			}
			if(cmbTYPE_OF_WATERCRAFT.SelectedItem !=null)
			{
				objWatercraftInfo.TYPE_OF_WATERCRAFT=	cmbTYPE_OF_WATERCRAFT.SelectedItem.Value ;
			}

			
//			if(cmbHULL_MATERIAL.SelectedItem !=null)
//			{
//				objWatercraftInfo.HULL_MATERIAL=	int.Parse(cmbHULL_MATERIAL.SelectedItem.Value==""?"0":cmbHULL_MATERIAL.SelectedItem.Value);
//			}
//			if(cmbFUEL_TYPE.SelectedItem!=null)
//			{
//				objWatercraftInfo.FUEL_TYPE=	int.Parse (cmbFUEL_TYPE.SelectedItem.Value==""?"0":cmbFUEL_TYPE.SelectedItem.Value);
//			}
		
//			if(txtDATE_PURCHASED.Text.Trim()!="")
//			{
//				objWatercraftInfo.DATE_PURCHASED=	Convert.ToDateTime(txtDATE_PURCHASED.Text);
//			}			
				
			if(txtWATERCRAFT_HORSE_POWER.Text.Trim()!="")
			{
				objWatercraftInfo.WATERCRAFT_HORSE_POWER=	Convert.ToInt32(txtWATERCRAFT_HORSE_POWER.Text);
			}

			if(txtINSURING_VALUE.Text.Trim()!="")
			{
				objWatercraftInfo.INSURING_VALUE=	double.Parse (txtINSURING_VALUE.Text.Trim()==""?"0":txtINSURING_VALUE.Text.Trim()); 
			}
			objWatercraftInfo.LENGTH=	txtLENGTH.Text;
			objWatercraftInfo.INCHES=	txtINCHES.Text;
			
			objWatercraftInfo.MAX_SPEED=	double.Parse (txtMAX_SPEED.Text.Trim()==""?"0":txtMAX_SPEED.Text.Trim());
	
			objWatercraftInfo.BERTH_LOC= "";

			// Added by Swastika on 7th Mar'06 for Pol Iss #59
			if (hidBOAT_ID.Value.ToString().ToUpper()=="NEW")
				objWatercraftInfo.BOAT_ID = 0;
			else
			{
				objWatercraftInfo.BOAT_ID = int.Parse (hidBOAT_ID.Value.ToString());
			}
			
			objWatercraftInfo.CUSTOMER_ID = int.Parse(hidCustomerID.Value);
			objWatercraftInfo.POLICY_ID = int.Parse(hidPOLICY_ID.Value);
			objWatercraftInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);

			if(cmbWATERS_NAVIGATED.SelectedItem!=null && cmbWATERS_NAVIGATED.SelectedItem.Value!="")
			{
				objWatercraftInfo.WATERS_NAVIGATED=	hidWaterNavigateID.Value;
			}

//			if(cmbTWIN_SINGLE.SelectedItem!=null)
//			{
//				objWatercraftInfo.TWIN_SINGLE=	int.Parse (cmbTWIN_SINGLE.SelectedItem.Value==""?"0":cmbTWIN_SINGLE.SelectedItem.Value);
//			}

			if (cmbTYPE_OF_WATERCRAFT.SelectedValue == "11439")
			{
				objWatercraftInfo.DESC_OTHER_WATERCRAFT=txtDESC_OTHER_WATERCRAFT.Text;
			}
			else
			{			
				objWatercraftInfo.DESC_OTHER_WATERCRAFT="";
			}

			if(cmbOTHER_POLICY.SelectedItem!=null && cmbOTHER_POLICY.SelectedItem.Value!="")
				objWatercraftInfo.OTHER_POLICY = cmbOTHER_POLICY.SelectedItem.Value;
			if(cmbIS_BOAT_EXCLUDED.SelectedItem!=null && cmbIS_BOAT_EXCLUDED.SelectedItem.Value!="")
				objWatercraftInfo.IS_BOAT_EXCLUDED = int.Parse(cmbIS_BOAT_EXCLUDED.SelectedItem.Value);
			
//			if(cmbTERRITORY.SelectedItem!=null)
//			{
//				objWatercraftInfo.TERRITORY=cmbTERRITORY.SelectedItem.Value==""?"0":cmbTERRITORY.SelectedItem.Value;
//			}
//			
//			if(cmbLORAN_NAV_SYSTEM.SelectedItem!=null)
//				objWatercraftInfo.LORAN_NAV_SYSTEM=int.Parse(cmbLORAN_NAV_SYSTEM.SelectedItem.Value==""?"0":cmbLORAN_NAV_SYSTEM.SelectedItem.Value);
//
//			if(cmbDIESEL_ENGINE.SelectedItem!=null)
//				objWatercraftInfo.DIESEL_ENGINE=int.Parse(cmbDIESEL_ENGINE.SelectedItem.Value==""?"0":cmbDIESEL_ENGINE.SelectedItem.Value);
//
//			if(cmbSHORE_STATION.SelectedItem!=null)
//				objWatercraftInfo.SHORE_STATION=int.Parse(cmbSHORE_STATION.SelectedItem.Value==""?"0":cmbSHORE_STATION.SelectedItem.Value);
//
//			if(cmbHALON_FIRE_EXT_SYSTEM.SelectedItem!=null)
//				objWatercraftInfo.HALON_FIRE_EXT_SYSTEM=int.Parse(cmbHALON_FIRE_EXT_SYSTEM.SelectedItem.Value==""?"0":cmbHALON_FIRE_EXT_SYSTEM.SelectedItem.Value);
//
//			if(cmbDUAL_OWNERSHIP.SelectedItem!=null)
//				objWatercraftInfo.DUAL_OWNERSHIP=int.Parse(cmbDUAL_OWNERSHIP.SelectedItem.Value==""?"0":cmbDUAL_OWNERSHIP.SelectedItem.Value);
//
//			if(cmbREMOVE_SAILBOAT.SelectedItem!=null)
//				objWatercraftInfo.REMOVE_SAILBOAT=int.Parse(cmbREMOVE_SAILBOAT.SelectedItem.Value==""?"0":cmbREMOVE_SAILBOAT.SelectedItem.Value);

			if(hidCalledFrom.Value==CALLED_FROM_UMBRELLA && cmbUSED_PARTICIPATE.SelectedItem!=null && cmbUSED_PARTICIPATE.SelectedItem.Value!="")
			{
				objWatercraftInfo.USED_PARTICIPATE = int.Parse(cmbUSED_PARTICIPATE.SelectedItem.Value);
				if(objWatercraftInfo.USED_PARTICIPATE == (int)enumYESNO_LOOKUP_UNIQUE_ID.YES && txtWATERCRAFT_CONTEST.Text.Trim()!="")
					objWatercraftInfo.WATERCRAFT_CONTEST = txtWATERCRAFT_CONTEST.Text.Trim();
			}

			objWatercraftInfo.MODIFIED_BY = int.Parse(GetUserId());
			objWatercraftInfo.LAST_UPDATED_DATETIME = DateTime.Now;
			objWatercraftInfo.CREATED_BY = int.Parse(GetUserId());
			objWatercraftInfo.CREATED_DATETIME = DateTime.Now;

			
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidBOAT_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objWatercraftInfo;
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
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
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
				int intRetVal = 0;	//For retreiving the return value of business class save function
				objWatercraftInformation = new  Cms.BusinessLayer.BlApplication.clsWatercraftInformation();
				ClsPolicyWatercraftInfo objWatercraftInfo = new ClsPolicyWatercraftInfo();
				Cms.Model.Policy.Umbrella.ClsWaterCraftInfo objUmbrellaWatercraftInfo = new Cms.Model.Policy.Umbrella.ClsWaterCraftInfo();
				strRowId		=	hidBOAT_ID.Value;

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
                   
					//Calling the add method of business layer class				
					if(hidCalledFrom.Value!=null && hidCalledFrom.Value.ToUpper().Trim()==CALLED_FROM_UMBRELLA)
					{
						objUmbrellaWatercraftInfo = GetFormValueUmb();
						intRetVal = objWatercraftInformation.AddPolicyUmbrellaWaterCraft(objUmbrellaWatercraftInfo);
					}
					else
					{
						//Retreiving the form values into model class object
						objWatercraftInfo = GetFormValue();
						intRetVal = objWatercraftInformation.AddPolicyWaterCraft(objWatercraftInfo);
												
					}



					if(intRetVal > 0)
					{
						
						if(hidCalledFrom.Value.ToUpper().Trim()!="" && hidCalledFrom.Value.ToUpper().Trim()== CALLED_FROM_UMBRELLA)
						{							
							hidBOAT_ID.Value = objUmbrellaWatercraftInfo.BOAT_ID.ToString();
							hidOldData.Value = clsWatercraftInformation.FetchPolicyUmbrellaWatercraftInfoXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value),int.Parse (hidBOAT_ID.Value==""?"0":hidBOAT_ID.Value));
						}
						else
						{
							hidBOAT_ID.Value = objWatercraftInfo.BOAT_ID.ToString();
							hidOldData.Value = clsWatercraftInformation.FetchPolicyWatercraftInfoXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value),int.Parse (hidBOAT_ID.Value==""?"0":hidBOAT_ID.Value));
						}
						lblMessage.Text			=	ClsMessages.GetMessage("246_0","22");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						hidBOAT_ID.Value = intRetVal.ToString();
						//Setting the workflow
                        if (ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString() != "")////Added By Pradeep Kushwaha on 21-sep-2010
                            btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString());
						base.OpenEndorsementDetails();
						SetWorkflow();
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage("246_0","23");
						hidFormSaved.Value			=		"2";
					}
					else if(intRetVal == -2)
					{
						lblMessage.Text				=		ClsMessages.GetMessage("246_0","29");
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage("246_0","24");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				
				} // end save case
				else //UPDATE CASE
				{
		
					//Creating the Model object for holding the Old data
					ClsPolicyWatercraftInfo objOldWatercraftInfo = new ClsPolicyWatercraftInfo();
					Cms.Model.Policy.Umbrella.ClsWaterCraftInfo objOldUmbrellaWatercraftInfo = new Cms.Model.Policy.Umbrella.ClsWaterCraftInfo();
					
					

					
					strRowId=hidBOAT_ID.Value.ToString();
					 
					if(hidCalledFrom.Value!=null && hidCalledFrom.Value.ToUpper().Trim()=="UMB")
					{
						objUmbrellaWatercraftInfo = GetFormValueUmb();
						//Setting those values into the Model object which are not in the page
						
						objUmbrellaWatercraftInfo.BOAT_ID = int.Parse (strRowId);
						//Updating the record using business layer class object
						intRetVal	= objWatercraftInformation.UpdatePolicyUmbrellaWaterCraft(objOldUmbrellaWatercraftInfo,objUmbrellaWatercraftInfo);
					}
					else
					{
						objWatercraftInfo = GetFormValue();
						//Setting those values into the Model object which are not in the page
						
						objWatercraftInfo.BOAT_ID = int.Parse (strRowId);
						//Setting  the Old Page details(XML File containing old details) into the Model Object
						base.PopulateModelObject(objOldWatercraftInfo,hidOldData.Value);
						//Updating the record using business layer class object
						intRetVal	= objWatercraftInformation.UpdatePolicyWaterCraft(objOldWatercraftInfo,objWatercraftInfo);
					}
					

					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage("246_0" ,"26");
						hidFormSaved.Value		=	"1";
						if(hidCalledFrom.Value.ToUpper().Trim()!="" && hidCalledFrom.Value.ToUpper().Trim()== CALLED_FROM_UMBRELLA)
						{
							hidOldData.Value = clsWatercraftInformation.FetchPolicyUmbrellaWatercraftInfoXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value),int.Parse (hidBOAT_ID.Value==""?"0":hidBOAT_ID.Value));
						}
						else
						{
							hidOldData.Value = clsWatercraftInformation.FetchPolicyWatercraftInfoXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value),int.Parse (hidBOAT_ID.Value==""?"0":hidBOAT_ID.Value));
						}
                        if (ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString() != "")////Added By Pradeep Kushwaha on 21-sep-2010
                            btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString());
						//Setting the workflow
						base.OpenEndorsementDetails();
						SetWorkflow();

					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage("246_0" ,"23");
						hidFormSaved.Value		=	"1";
					}
					else if(intRetVal == -2)
					{
						lblMessage.Text				=		ClsMessages.GetMessage("246_0","29");
						hidFormSaved.Value			=		"2";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage("246_0" ,"24");
						hidFormSaved.Value		=	"1";
					}
					lblMessage.Visible = true;
				}

			}
			catch(Exception ex)
			{
				lblMessage.Text	=	Cms.CmsWeb.ClsMessages.GetMessage("246_0","25") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				//ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objWatercraftInformation!= null)
					objWatercraftInformation.Dispose();
			}
		}

		#endregion

		private void SetCaptions()
		{
			capBOAT_NO.Text							=		objResourceMgr.GetString("txtBOAT_NO");
			capYEAR.Text							=		objResourceMgr.GetString("txtYEAR");
			capMAKE.Text							=		objResourceMgr.GetString("txtMAKE");
			capMODEL.Text							=		objResourceMgr.GetString("txtMODEL");
			capSERIAL_NO.Text						=		objResourceMgr.GetString("txtSERIAL_NO");
			capSTATE_REG.Text						=		objResourceMgr.GetString("cmbSTATE_REG");
			
			
			capHULL_MATERIAL.Text					=		objResourceMgr.GetString("cmbHULL_MATERIAL");
			capFUEL_TYPE.Text						=		objResourceMgr.GetString("cmbFUEL_TYPE");
			
			capDATE_PURCHASED.Text					=		objResourceMgr.GetString("txtDATE_PURCHASED");
			capLENGTH.Text							=		objResourceMgr.GetString("txtLENGTH");
			capMAX_SPEED.Text						=		objResourceMgr.GetString("txtMAX_SPEED");
			capWATERS_NAVIGATED.Text				=		objResourceMgr.GetString("cmbWATERS_NAVIGATED");
			capTERRITORY.Text						=		objResourceMgr.GetString("cmbTERRITORY");
			capTWIN_SINGLE.Text						=       objResourceMgr.GetString("cmbTWIN_SINGLE");
//			capDESC_OTHER_WATERCRAFT.Text			=       objResourceMgr.GetString("txtDESC_OTHER_WATERCRAFT");

			
			capHALON_FIRE_EXT_SYSTEM.Text			=       objResourceMgr.GetString("cmbHALON_FIRE_EXT_SYSTEM");
			capLORAN_NAV_SYSTEM.Text				=       objResourceMgr.GetString("cmbLORAN_NAV_SYSTEM");
			capSHORE_STATION.Text					=       objResourceMgr.GetString("cmbSHORE_STATION");
			capDUAL_OWNERSHIP.Text					=       objResourceMgr.GetString("cmbDUAL_OWNERSHIP");
			capREMOVE_SAILBOAT.Text					=       objResourceMgr.GetString("cmbREMOVE_SAILBOAT");			
			capINSURING_VALUE.Text					=       objResourceMgr.GetString("txtINSURING_VALUE");						
			capTYPE_OF_WATERCRAFT.Text				=       objResourceMgr.GetString("cmbTYPE_OF_WATERCRAFT");					
			capWATERCRAFT_HORSE_POWER.Text			=       objResourceMgr.GetString("txtWATERCRAFT_HORSE_POWER");					
			capCOV_TYPE_BASIS.Text					=		objResourceMgr.GetString("cmbCOV_TYPE_BASIS");	
			capPHOTO_ATTACHED.Text					=       objResourceMgr.GetString("cmbPHOTO_ATTACHED");
			capMARINE_SURVEY.Text					=       objResourceMgr.GetString("cmbMARINE_SURVEY");
			capDATE_MARINE_SURVEY.Text				=       objResourceMgr.GetString("txtDATE_MARINE_SURVEY");

			//Added by RPSINGH - 15 May 2006
			capLOCATION_ZIP.Text					=    objResourceMgr.GetString("capLOCATION_ZIP");
			capLOCATION_STATE.Text					=    objResourceMgr.GetString("capLOCATION_STATE");
			capLOCATION_CITY.Text					=    objResourceMgr.GetString("capLOCATION_CITY");
			capLOCATION_ADDRESS.Text				=    objResourceMgr.GetString("capLOCATION_ADDRESS");
			capLAY_UP_PERIOD_FROM.Text				=    objResourceMgr.GetString("capLAY_UP_PERIOD_FROM");
			capLAY_UP_PERIOD_TO.Text				=    objResourceMgr.GetString("capLAY_UP_PERIOD_TO");
			capUSED_PARTICIPATE.Text				=       objResourceMgr.GetString("cmbUSED_PARTICIPATE");
			capWATERCRAFT_CONTEST.Text			=       objResourceMgr.GetString("txtWATERCRAFT_CONTEST");
			
			
			rfvLOCATION_STATE.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("35");
			rfvLOCATION_ZIP.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("37");
			rfvLOCATION_ADDRESS.ErrorMessage		=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("32");
			rfvLOCATION_CITY.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("39");			

			revLOCATION_ZIP.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("24");
			revLOCATION_ZIP.ValidationExpression	=	aRegExpZip;
			capIS_BOAT_EXCLUDED.Text = objResourceMgr.GetString("cmbIS_BOAT_EXCLUDED");
			capOTHER_POLICY.Text = objResourceMgr.GetString("cmbOTHER_POLICY");
			//End of addition by RPSINGH - 15 May 2006
			capLOSSREPORT_DATETIME.Text		=		objResourceMgr.GetString("txtLOSSREPORT_DATETIME");
			capLOSSREPORT_ORDER.Text  =objResourceMgr.GetString("cmbLOSSREPORT_ORDER");
	
				
		}
				
		private void GetOldDataXML()
		{
          		
			//If BOAT_ID is null then it is add mode else edit
			if (Request.QueryString["BOATID"]!=null && Request.QueryString["BOATID"].ToString()!="")
			{
				//In case of edit we get these keys as querystring
				if (Request.QueryString["CUSTOMER_ID"]!=null && Request.QueryString["CUSTOMER_ID"].ToString()!="")
				{
					hidCustomerID.Value = Request.QueryString["CUSTOMER_ID"].ToString();
				}

				if (Request.QueryString["POLICY_ID"]!=null && Request.QueryString["POLICY_ID"].ToString()!="")
				{
					hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].ToString();
				}
				if (Request.QueryString["POLICY_VERSION_ID"]!=null && Request.QueryString["POLICY_VERSION_ID"].ToString()!="")
				{
					hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();
				}
				
				hidBOAT_ID.Value = Request.QueryString["BOATID"].ToString();

				if(hidCalledFrom.Value.ToUpper().Trim()!="" && hidCalledFrom.Value.ToUpper().Trim()== CALLED_FROM_UMBRELLA)
				{					
					hidOldData.Value = clsWatercraftInformation.FetchPolicyUmbrellaWatercraftInfoXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value),int.Parse (hidBOAT_ID.Value==""?"0":hidBOAT_ID.Value));
				}
				else
				{
					hidOldData.Value = clsWatercraftInformation.FetchPolicyWatercraftInfoXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value),int.Parse (hidBOAT_ID.Value==""?"0":hidBOAT_ID.Value));
				}
                if (ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString() != "")////Added By Pradeep Kushwaha on 21-sep-2010
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString());
            
			}
			else 
			{
				//In case of add we take these keys from the session
				hidCustomerID.Value = GetCustomerID();
				hidPOLICY_ID.Value =GetPolicyID();
				hidPOLICY_VERSION_ID.Value =GetPolicyVersionID();
				hidBOAT_ID.Value ="NEW";
				txtBOAT_NO.Text = clsWatercraftInformation.GetPolicyNewBoatNumber(int.Parse(hidCustomerID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),hidCalledFrom.Value  ).ToString();

			}

			hidAPP_LOB.Value = GetLOBID();
          
		}

		private void FillControls()
		{
			// Fuel Type
			cmbFUEL_TYPE.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("FTYCD");
			cmbFUEL_TYPE.DataTextField	= "LookupDesc";
			cmbFUEL_TYPE.DataValueField	= "LookupID";
			cmbFUEL_TYPE.DataBind();
			cmbFUEL_TYPE.Items.Insert(0,"");
		
		
			//hull material
			cmbHULL_MATERIAL.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("%HULL");
			cmbHULL_MATERIAL.DataTextField	= "LookupDesc";
			cmbHULL_MATERIAL.DataValueField	= "LookupID";
			cmbHULL_MATERIAL.DataBind();
			//ListItem LI = new ListItem ("","0");
			//cmbHULL_MATERIAL.Items.Insert(0,LI);
			cmbHULL_MATERIAL.Items.Insert(0,"");
			
		
			
			//waters navigated
			cmbWATERS_NAVIGATED.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("WNVC");
			cmbWATERS_NAVIGATED.DataTextField	= "LookupDesc";
			cmbWATERS_NAVIGATED.DataValueField	= "LookupID";
			cmbWATERS_NAVIGATED.DataBind();
			cmbWATERS_NAVIGATED.Items.Insert(0,"");
			

			//Type of WaterCraft
			//cmbTYPE_OF_WATERCRAFT.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("WCTCD",null,"Y");
			
			cmbTYPE_OF_WATERCRAFT.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupForwaterCraftType("WCTCD",null,"lookup_value_desc");
			cmbTYPE_OF_WATERCRAFT.DataTextField	= "LookupDesc";
			cmbTYPE_OF_WATERCRAFT.DataValueField	= "LookupID";
			cmbTYPE_OF_WATERCRAFT.DataBind();
			cmbTYPE_OF_WATERCRAFT.Items.Insert(0,"");

			cmbCOV_TYPE_BASIS.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CTB");
			cmbCOV_TYPE_BASIS.DataTextField	= "LookupDesc";
			cmbCOV_TYPE_BASIS.DataValueField	= "LookupID";
			cmbCOV_TYPE_BASIS.DataBind();
			cmbCOV_TYPE_BASIS.Items.Insert(0,"");

			cmbTWIN_SINGLE.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("TWSGL");
			cmbTWIN_SINGLE.DataTextField	= "LookupDesc";
			cmbTWIN_SINGLE.DataValueField	= "LookupID";
			cmbTWIN_SINGLE.DataBind();
			cmbTWIN_SINGLE.Items.Insert(0,"");
			
			cmbTERRITORY.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("Terr");
			cmbTERRITORY.DataTextField	= "LookupDesc";
			cmbTERRITORY.DataValueField	= "LookupID";
			cmbTERRITORY.DataBind();
			cmbTERRITORY.Items.Insert(0,"");

			cmbDIESEL_ENGINE.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbDIESEL_ENGINE.DataTextField	= "LookupDesc";
			cmbDIESEL_ENGINE.DataValueField	= "LookupID";
			cmbDIESEL_ENGINE.DataBind();
			cmbDIESEL_ENGINE.Items.Insert(0,"");

			cmbHALON_FIRE_EXT_SYSTEM.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbHALON_FIRE_EXT_SYSTEM.DataTextField	= "LookupDesc";
			cmbHALON_FIRE_EXT_SYSTEM.DataValueField	= "LookupID";
			cmbHALON_FIRE_EXT_SYSTEM.DataBind();
			cmbHALON_FIRE_EXT_SYSTEM.Items.Insert(0,"");

			cmbSHORE_STATION.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbSHORE_STATION.DataTextField	= "LookupDesc";
			cmbSHORE_STATION.DataValueField	= "LookupID";
			cmbSHORE_STATION.DataBind();
			cmbSHORE_STATION.Items.Insert(0,"");

			cmbDUAL_OWNERSHIP.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbDUAL_OWNERSHIP.DataTextField	= "LookupDesc";
			cmbDUAL_OWNERSHIP.DataValueField	= "LookupID";
			cmbDUAL_OWNERSHIP.DataBind();
			cmbDUAL_OWNERSHIP.Items.Insert(0,"");
			
			cmbREMOVE_SAILBOAT.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbREMOVE_SAILBOAT.DataTextField	= "LookupDesc";
			cmbREMOVE_SAILBOAT.DataValueField	= "LookupID";
			cmbREMOVE_SAILBOAT.DataBind();
			cmbREMOVE_SAILBOAT.Items.Insert(0,"");
			

			cmbLORAN_NAV_SYSTEM.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbLORAN_NAV_SYSTEM.DataTextField	= "LookupDesc";
			cmbLORAN_NAV_SYSTEM.DataValueField	= "LookupID";
			cmbLORAN_NAV_SYSTEM.DataBind();
			cmbLORAN_NAV_SYSTEM.Items.Insert(0,"");

			cmbPHOTO_ATTACHED.DataSource		= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbPHOTO_ATTACHED.DataTextField		= "LookupDesc";
			cmbPHOTO_ATTACHED.DataValueField	= "LookupID";
			cmbPHOTO_ATTACHED.DataBind();
			cmbPHOTO_ATTACHED.Items.Insert(0,"");

			cmbMARINE_SURVEY.DataSource				= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbMARINE_SURVEY.DataTextField			= "LookupDesc";
			cmbMARINE_SURVEY.DataValueField			= "LookupID";
			cmbMARINE_SURVEY.DataBind();
			cmbMARINE_SURVEY.Items.Insert(0,"");

			FillMonths(cmbLAY_UP_PERIOD_FROM_MONTH); 
			FillDays(cmbLAY_UP_PERIOD_FROM_DAY);
						
			FillMonths(cmbLAY_UP_PERIOD_TO_MONTH); 
			FillDays(cmbLAY_UP_PERIOD_TO_DAY);	
					
			cmbLOCATION_STATE.DataSource		= Cms.CmsWeb.ClsFetcher.ActiveState;
			cmbLOCATION_STATE.DataTextField		= "STATE_NAME";
			cmbLOCATION_STATE.DataValueField	= "STATE_ID";
			cmbLOCATION_STATE.DataBind();
			cmbLOCATION_STATE.Items.Insert(0,new ListItem("",""));
			cmbLOCATION_STATE.SelectedIndex=0;

			cmbUSED_PARTICIPATE.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbUSED_PARTICIPATE.DataTextField	= "LookupDesc";
			cmbUSED_PARTICIPATE.DataValueField	= "LookupID";
			cmbUSED_PARTICIPATE.DataBind();
			cmbUSED_PARTICIPATE.Items.Insert(0,"");

			if(hidCalledFrom.Value==CALLED_FROM_UMBRELLA)
			{
				cmbIS_BOAT_EXCLUDED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
				cmbIS_BOAT_EXCLUDED.DataTextField="LookupDesc"; 
				cmbIS_BOAT_EXCLUDED.DataValueField="LookupID";
				cmbIS_BOAT_EXCLUDED.DataBind();
				cmbIS_BOAT_EXCLUDED.Items.Insert(0,"");
	
				ClsUmbrellaRecrVeh objUmbrellaRecrVeh = new ClsUmbrellaRecrVeh();
				DataTable dtTemp = objUmbrellaRecrVeh.GetPolSelectedOtherPolicies(int.Parse(hidCustomerID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),ClsUmbSchRecords.CALLED_FROM_BOAT,"");
				if(dtTemp!=null && dtTemp.Rows.Count>0)
				{
					cmbOTHER_POLICY.DataSource = dtTemp;
					cmbOTHER_POLICY.DataTextField = "POLICY_NUMBER_LOB";
					cmbOTHER_POLICY.DataValueField = "POLICY_NUMBER";
					cmbOTHER_POLICY.DataBind();
					cmbOTHER_POLICY.Items.Insert(0,new ListItem("",""));

				}
			}
			

		}

		private void FillMonths(DropDownList DL)
		{
			DL.Items.Clear();
			ListItem LI;
			
			LI = new ListItem ("","0"); DL.Items.Add(LI);
			LI = new ListItem ("Jan","1"); DL.Items.Add(LI);
			LI = new ListItem ("Feb","2"); DL.Items.Add(LI);
			LI = new ListItem ("Mar","3"); DL.Items.Add(LI);
			LI = new ListItem ("Apr","4"); DL.Items.Add(LI);
			LI = new ListItem ("May","5"); DL.Items.Add(LI);
			LI = new ListItem ("Jun","6"); DL.Items.Add(LI);
			LI = new ListItem ("Jul","7"); DL.Items.Add(LI);
			LI = new ListItem ("Aug","8"); DL.Items.Add(LI);
			LI = new ListItem ("Sep","9"); DL.Items.Add(LI);
			LI = new ListItem ("Oct","10"); DL.Items.Add(LI);
			LI = new ListItem ("Nov","11"); DL.Items.Add(LI);
			LI = new ListItem ("Dec","12"); DL.Items.Add(LI);
			
			DL.ClearSelection();
			DL.Items.FindByValue("0").Selected = true;  
		}

		private void FillDays(DropDownList DL)
		{
			DL.Items.Clear();
			ListItem LI;
			int i;
			for (i=1;i<=31;i++)
			{
				LI = new ListItem (i.ToString(),i.ToString()); 
				DL.Items.Add(LI);
			}
			DL.Items.Insert(0,"");
			DL.Items.FindByText("").Selected = true;  
		}

		private void SetWorkflow()
		{//Set the Watercraft workflow in POL Homeowner
			//if(base.ScreenId	==	"246_0" || base.ScreenId == "148_0" || base.ScreenId == "83_0" || base.ScreenId == "166_0")
			if(base.ScreenId	==	"246_0" || base.ScreenId == "251_0" || base.ScreenId == "83_0" || base.ScreenId == "166_0" || base.ScreenId == "277_0")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;				
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule="POL";
				
				if ( hidBOAT_ID .Value != "" && hidBOAT_ID .Value.ToUpper()!= "NEW"  )
				{
					myWorkFlow.AddKeyValue("BOAT_ID",hidBOAT_ID.Value);
				}
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		} 


		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			
			int intBoatID = int.Parse(hidBOAT_ID.Value);
			
			objWatercraftInformation = new Cms.BusinessLayer.BlApplication.clsWatercraftInformation();
			Cms.Model.Policy.Watercraft.ClsPolicyWatercraftInfo  objWatercraftInfo = new ClsPolicyWatercraftInfo();
			Cms.Model.Policy.Umbrella.ClsWaterCraftInfo objUmbrellaWatercraftInfo = new Cms.Model.Policy.Umbrella.ClsWaterCraftInfo();			
			objWatercraftInfo = GetFormValue();
			int OperatorCount;
			//Added FOR Itrack Issue #5479
			string boat_no    = ClsCommon.FetchValueFromXML("BOAT_NO",hidOldData.Value).Trim();
			string boat_make  = ClsCommon.FetchValueFromXML("MAKE",hidOldData.Value).Trim();
			string boat_model = ClsCommon.FetchValueFromXML("MODEL",hidOldData.Value).Trim();
			strCust_Info = " ;Boat  # = " +  boat_no + ";Boat_make = " + boat_make + ";Boat_model = " + boat_model;
                 

			if(hidCalledFrom.Value!=null && hidCalledFrom.Value.ToUpper().Trim()==CALLED_FROM_UMBRELLA)				
			{
				base.PopulateModelObject(objUmbrellaWatercraftInfo,hidOldData.Value);
				OperatorCount = objWatercraftInformation.GetOperatorCountForAssignedUmbrella(objUmbrellaWatercraftInfo);
				if(OperatorCount>0)
					ShowAlertMessageForDelete();
				else					  
					intRetVal = objWatercraftInformation.DeletePolicyUmbrellaWaterCraft(hidCustomerID.Value.ToString(),hidPOLICY_ID.Value.ToString(),hidPOLICY_VERSION_ID.Value.ToString(),hidBOAT_ID.Value.ToString());
			}
			else
			{
				base.PopulateModelObject(objWatercraftInfo,hidOldData.Value);
				// Added by Swastika on 7th Mar'06 for Pol Iss #59				
				OperatorCount = objWatercraftInformation.GetOperatorCountForAssignedWatercraft(objWatercraftInfo);
				if(OperatorCount>0)
					ShowAlertMessageForDelete();
				else				
				{
					//Parameter Added For Itrack Issue #5479
					intRetVal = objWatercraftInformation.DeletePolicyWaterCraft(hidCustomerID.Value.ToString(),hidPOLICY_ID.Value.ToString(),hidPOLICY_VERSION_ID.Value.ToString(),hidBOAT_ID.Value.ToString(),GetUserId(),strCust_Info);
				}
				//intRetVal = objWatercraftInformation.Delete(objWatercraftInfo,hidCustomInfo.Value);
			}	
			
			if(intRetVal>0)
				{
					lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","127");
					hidFormSaved.Value = "5";
					hidOldData.Value = "";
					trBody.Attributes.Add("style","display:none");
					SetWorkflow();
					base.OpenEndorsementDetails();
				
				}
				else if(intRetVal == -1)
				{
					lblDelete.Visible =true;
					lblDelete.Text		=	ClsMessages.GetMessage("246_0","506");
					hidFormSaved.Value		=	"2";
				}
				//lblDelete.Visible = true;
			// If Boat is assigned to trailer/equipment, then it cannot be deleted
			 else if(intRetVal == -2)
				 {
					 lblDelete.Visible =true;
					 lblDelete.Text		=	ClsMessages.GetMessage("246_0","704");
					 hidFormSaved.Value	=	"2";
				//Added by Charles on 7-Oct-09 for Itrack 6600	 
					 if(hidIS_ACTIVE.Value=="Y")
						btnActivateDeactivate.Text="Deactivate";
					 else
						btnActivateDeactivate.Text="Activate"; //Added till here
				 }
			lblDelete.Visible = true;
			
		}

		// Added by Swastika on 7th Mar'06 for Pol Iss #59 -- show alert in case deactivate when operator is assigned
		private void ShowAlertMessage()
		{
			string strAlert = "";	
			
			strAlert = "<script> " + 
				" returnValue= getUserConfirmationForDeactivate();" + 
				" if(returnValue==6) " + 
				" document.getElementById('hidAlert').value='1'; " + 
				"else " + 
				" document.getElementById('hidAlert').value='0';" + 
				" __doPostBack('Deactive','" + hidAlert.Value + "') ; " + 
				"</script> ";
			ClientScript.RegisterStartupScript(this.GetType(),"AlertMessage",strAlert);
		}

		// Added by Swastika on 7th Mar'06 for Pol Iss #59
		private void DoInActive(string AlertValue)
		{
			int intResult=0;
			if (AlertValue.Trim() == "1")
			{
				Cms.BusinessLayer.BlApplication.clsWatercraftInformation objWatercraftInformation	= new clsWatercraftInformation();
				Cms.Model.Policy.Watercraft.ClsPolicyWatercraftInfo objWatercraftInfo=new ClsPolicyWatercraftInfo();
				Cms.Model.Policy.Umbrella.ClsWaterCraftInfo objUmbrellaWaterInfo = new Cms.Model.Policy.Umbrella.ClsWaterCraftInfo();
				if(hidCalledFrom.Value!=null && hidCalledFrom.Value.ToUpper().Trim()==CALLED_FROM_UMBRELLA)
				{
					base.PopulateModelObject(objUmbrellaWaterInfo,hidOldData.Value);		
					objWatercraftInformation.ActivateDeactivateUmbrellaWatercraftPolicy(objUmbrellaWaterInfo,"N",strCust_Info);
					hidOldData.Value = clsWatercraftInformation.FetchPolicyUmbrellaWatercraftInfoXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value),int.Parse (hidBOAT_ID.Value==""?"0":hidBOAT_ID.Value));
				}
				else
				{
					base.PopulateModelObject(objWatercraftInfo,hidOldData.Value);
					intResult =  objWatercraftInformation.ActivateDeactivateWatercraftPolicy(objWatercraftInfo,"N",strCust_Info);
					if(intResult==-2)
					{						
						lblMessage.Text = ClsMessages.FetchGeneralMessage("705");
						hidIS_ACTIVE.Value="Y";
						lblMessage.Visible = true;
						return;
					}
					hidOldData.Value = clsWatercraftInformation.FetchPolicyWatercraftInfoXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value),int.Parse (hidPOLICY_ID.Value==""?"0":hidBOAT_ID.Value));
                    if (ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString() != "")////Added By Pradeep Kushwaha on 21-sep-2010
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString());
				}
				lblMessage.Text = ClsMessages.GetMessage("246_0","41");
				hidIS_ACTIVE.Value="N";
				hidFormSaved.Value="0";
				
				lblMessage.Visible = true;
				ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid(1," + hidBOAT_ID.Value + ");</script>");
			}
		}
		// Added by Swastika on 7th Mar'06 for Pol Iss #59
		private void DoDelete(string AlertValue)
		{
			if (AlertValue.Trim() == "1")
			{
				Cms.BusinessLayer.BlApplication.clsWatercraftInformation objWatercraftInformation	= new clsWatercraftInformation();
				Cms.Model.Policy.Watercraft.ClsPolicyWatercraftInfo objWatercraftInfo=new ClsPolicyWatercraftInfo();
				objWatercraftInfo = GetFormValue();				
				
				if(hidCalledFrom.Value!=null && hidCalledFrom.Value.ToUpper().Trim()==CALLED_FROM_UMBRELLA)				
						intRetVal = objWatercraftInformation.DeletePolicyUmbrellaWaterCraft(hidCustomerID.Value.ToString(),hidPOLICY_ID.Value.ToString(),hidPOLICY_VERSION_ID.Value.ToString(),hidBOAT_ID.Value.ToString());
				else
					//GetUserid() Added For Itrack Issue #5479
					intRetVal = objWatercraftInformation.DeletePolicyWaterCraft(hidCustomerID.Value.ToString(),hidPOLICY_ID.Value.ToString(),hidPOLICY_VERSION_ID.Value.ToString(),hidBOAT_ID.Value.ToString(),GetUserId(),strCust_Info);
				if(intRetVal>0)
				{
					lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","127");
					hidFormSaved.Value = "5";
					hidOldData.Value = "";
					trBody.Attributes.Add("style","display:none");
					SetWorkflow();
					base.OpenEndorsementDetails();
				
				}
				else if(intRetVal == -1)
				{
					lblDelete.Visible =true;
					lblDelete.Text		=	ClsMessages.GetMessage("246_0","506");
					hidFormSaved.Value		=	"2";
				}
				else if(intRetVal == -2) //Boat is associated with Trailer/Equipment, can't be deleted.
				{
					lblDelete.Visible =true;
					lblDelete.Text		=	ClsMessages.GetMessage("246_0","704");
					hidFormSaved.Value	=	"2";
				}
				lblDelete.Visible = true;
			}
		}

		// Added by Swastika on 7th Mar'06 for Pol Iss #59
		private void ShowAlertMessageForDelete()
		{
			string strAlert = "";	
			
			strAlert = "<script> " + 
				" returnValue= getUserConfirmationForDelete(); " + 
				" if(returnValue==6) " + 
				" document.getElementById('hidAlert').value='1'; " + 
				"else " + 
				" document.getElementById('hidAlert').value='0';" + 
				" __doPostBack('DeleteVehicle','" + hidAlert.Value + "') ; " + 
				"</script> ";
			ClientScript.RegisterStartupScript(this.GetType(),"AlertMessage",strAlert);
		}


		// Added by Swastika on 7th Mar'06 for Pol Iss #59
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			
			try
			{				
				clsWatercraftInformation objWatercraftInformation = new  clsWatercraftInformation();
				//Retreiving the form values into model class object
				Cms.Model.Policy.Watercraft.ClsPolicyWatercraftInfo  objWatercraftInfo = new ClsPolicyWatercraftInfo();
				Cms.Model.Policy.Umbrella.ClsWaterCraftInfo objUmbrellaWatercraftInfo = new Cms.Model.Policy.Umbrella.ClsWaterCraftInfo();
				//objWatercraftInfo = GetFormValue();
				int intResult = 0;
				//Added FOR Itrack Issue #5479
				string boat_no    = ClsCommon.FetchValueFromXML("BOAT_NO",hidOldData.Value).Trim();
				string boat_make  = ClsCommon.FetchValueFromXML("MAKE",hidOldData.Value).Trim();
				string boat_model = ClsCommon.FetchValueFromXML("MODEL",hidOldData.Value).Trim();
				strCust_Info = " ;Boat  # = " +  boat_no + ";Boat_make = " + boat_make + ";Boat_model = " + boat_model;
                
				
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					if(hidCalledFrom.Value!=null && hidCalledFrom.Value.ToUpper().Trim()==CALLED_FROM_UMBRELLA)
					{
						base.PopulateModelObject(objUmbrellaWatercraftInfo,hidOldData.Value);	
						
						int OperatorCount = objWatercraftInformation.GetOperatorCountForAssignedUmbrella(objUmbrellaWatercraftInfo);
						if(OperatorCount>0)
						{
							ShowAlertMessage();
							return;
						}
						else							
							objWatercraftInformation.ActivateDeactivateUmbrellaWatercraftPolicy(objUmbrellaWatercraftInfo,"N",strCust_Info);
					}
					else
					{
						base.PopulateModelObject(objWatercraftInfo,hidOldData.Value);						
						int OperatorCount = objWatercraftInformation.GetOperatorCountForAssignedWatercraft(objWatercraftInfo);
						if(OperatorCount>0)
						{
							ShowAlertMessage();
							return;
						}
						else
							//Added For Itrack Issue #5479
							objWatercraftInfo.MODIFIED_BY = int.Parse(GetUserId());
							objWatercraftInfo.CREATED_BY = int.Parse(GetUserId());
							intResult = objWatercraftInformation.ActivateDeactivateWatercraftPolicy(objWatercraftInfo,"N",strCust_Info);							
					}
					if(intResult==-2)
					{						
						lblMessage.Text = ClsMessages.FetchGeneralMessage("705");
						hidIS_ACTIVE.Value="Y";
					}
					else
					{
                        objWatercraftInfo.IS_ACTIVE = "N";
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objWatercraftInfo.IS_ACTIVE.ToString().Trim());
						lblMessage.Text = ClsMessages.GetMessage("246_0","41");
						hidIS_ACTIVE.Value="N";
						ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid(1," + hidBOAT_ID.Value + ");</script>");
					}
				}
				else
				{					
					if(hidCalledFrom.Value!=null && hidCalledFrom.Value.ToUpper().Trim()==CALLED_FROM_UMBRELLA)
					{
						base.PopulateModelObject(objUmbrellaWatercraftInfo,hidOldData.Value);						
						objWatercraftInformation.ActivateDeactivateUmbrellaWatercraftPolicy(objUmbrellaWatercraftInfo,"Y",strCust_Info);
					}
					else
					{
						base.PopulateModelObject(objWatercraftInfo,hidOldData.Value);
						//Added For Itrack Issue #5479
						objWatercraftInfo.MODIFIED_BY = int.Parse(GetUserId());
						objWatercraftInfo.CREATED_BY = int.Parse(GetUserId());
						objWatercraftInformation.ActivateDeactivateWatercraftPolicy(objWatercraftInfo,"Y",strCust_Info);
					}
					
					//lblMessage.Text = ClsMessages.GetMessage("246_0","40");
                    objWatercraftInfo.IS_ACTIVE = "Y";
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objWatercraftInfo.IS_ACTIVE.ToString().Trim());
					lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("40");

					hidIS_ACTIVE.Value="Y";
				}
					hidFormSaved.Value			=	"0";
				
					//Generating the XML again
					if(hidCalledFrom.Value!=null && hidCalledFrom.Value.ToUpper().Trim()==CALLED_FROM_UMBRELLA)
						hidOldData.Value = clsWatercraftInformation.FetchPolicyUmbrellaWatercraftInfoXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value),int.Parse (hidPOLICY_ID.Value==""?"0":hidBOAT_ID.Value));
					else						
						hidOldData.Value = clsWatercraftInformation.FetchPolicyWatercraftInfoXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value),int.Parse (hidPOLICY_ID.Value==""?"0":hidBOAT_ID.Value));

                    if (ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString() != "")////Added By Pradeep Kushwaha on 21-sep-2010
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value).ToString());
					base.OpenEndorsementDetails();
				
					ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid(1," + hidBOAT_ID.Value + ");</script>");
				}
			
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage("246_0","21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
			}
			finally
			{
				lblMessage.Visible = true;
			}
		}

		private void getCustomerAddress(string strCustID)
		{	
			System.Xml.XmlDocument objXmlDoc = new System.Xml.XmlDocument(); 
			objXmlDoc.LoadXml ((new Cms.BusinessLayer.BlClient.ClsCustomer()).FillCustomerDetails(Convert.ToInt32(strCustID)).ToString());
			
			hidCustomerAddress.Value	= objXmlDoc.FirstChild.FirstChild.SelectSingleNode("CUSTOMER_ADDRESS1").InnerText + " " + objXmlDoc.FirstChild.FirstChild.SelectSingleNode("CUSTOMER_ADDRESS2").InnerText;			
			hidCustomerCity.Value		= objXmlDoc.FirstChild.FirstChild.SelectSingleNode("CUSTOMER_CITY").InnerText;
			hidCustomerState.Value		= objXmlDoc.FirstChild.FirstChild.SelectSingleNode("CUSTOMER_STATE").InnerText;
			hidCustomerZip.Value		= objXmlDoc.FirstChild.FirstChild.SelectSingleNode("CUSTOMER_ZIP").InnerText;			
		}

		private void getPolicyEffectiveYear()
		{	
			string strEffDate;			
			string strEffYear;
			
			objWatercraftInformation = new Cms.BusinessLayer.BlApplication.clsWatercraftInformation();			
			strEffDate = objWatercraftInformation.GetPolEffectiveDate(int.Parse(hidCustomerID.Value==""?"0":hidCustomerID.Value),int.Parse(hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value==""?"0":hidPOLICY_VERSION_ID.Value));			
			strEffYear  = Convert.ToDateTime(strEffDate).Year.ToString();   // Year component of POLICY_EFFECTIVE_DATE
			hidBoatAge.Value =  strEffYear;			
			hidPolEffDate.Value = strEffDate.Split(' ')[0];//Pol effective date
		} 

		[Ajax.AjaxMethod()]
		public string AjaxFetchZipForState(int stateID, string ZipID)
		{
			CmsWeb.webservices.ClsWebServiceCommon obj =  new CmsWeb.webservices.ClsWebServiceCommon();
			string result = "";
			result = obj.FetchZipForState(stateID,ZipID);
			return result;
			
		}
	}
 }
	
	


