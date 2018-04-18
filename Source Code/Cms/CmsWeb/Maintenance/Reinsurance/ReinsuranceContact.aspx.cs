/* ***************************************************************************************
   Author		: Harmanjeet Singh
   Creation Date: April 24, 2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: This file is used for REINSURANCE CONTACTS. 
   Comments		: 
   ------------------------------------------------------------------------------------- 
   History	Date	     Modified By		Description
   
   ------------------------------------------------------------------------------------- 
   *****************************************************************************************/


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
using System.Configuration ; 
using System.Xml ; 
using Cms.ExceptionPublisher.ExceptionManagement;  
using Cms.CmsWeb;
using Cms.CmsWeb.Utils ;   
using System.Resources; 
using System.Reflection; 
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlCommon;  
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 
using Cms.Model.Maintenance.Reinsurance;

namespace Cms.CmsWeb.Maintenance.Reinsurance
{
	/// <summary>
	/// Summary description for ReinsuranceContact.
	/// </summary>
	public class ReinsuranceContact : Cms.CmsWeb.cmsbase 
	{
		

		#region DECLARATION VARIABLES

		//protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;

		//protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREIN_CONTACT_ID;
		//protected System.Web.UI.HtmlControls.HtmlInputHidden hidREIN_CONTACT_CODE;
		
		

		System.Resources.ResourceManager objResourceMgr;

		private string strRowId, strFormSaved;
		//string oldXML;
		protected System.Web.UI.WebControls.TextBox txtPRINCIPAL_CONTACT;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Image imgZipLookup;
		protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
		protected System.Web.UI.WebControls.Image imgMZipLookup;
		protected System.Web.UI.WebControls.HyperLink hlkMZipLookup;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_CONTACT_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbREIN_CONTACT_TYPE;

		protected System.Web.UI.WebControls.Label capREIN_CONTACT_NAME;
		protected System.Web.UI.WebControls.TextBox txtREIN_CONTACT_NAME;
		
		protected System.Web.UI.WebControls.Label capREIN_CONTACT_CODE;
		protected System.Web.UI.WebControls.TextBox txtREIN_CONTACT_CODE;
		
		protected System.Web.UI.WebControls.Label capREIN_CONTACT_POSITION;
		protected System.Web.UI.WebControls.TextBox txtREIN_CONTACT_POSITION;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_CONTACT_POSITION;
		
		protected System.Web.UI.WebControls.Label capREIN_CONTACT_SALUTATION;
		protected System.Web.UI.WebControls.TextBox txtREIN_CONTACT_SALUTATION;
		
		protected System.Web.UI.WebControls.Label capREIN_CONTACT_ADDRESS_1;
		
		protected System.Web.UI.WebControls.Label capREIN_CONTACT_ADDRESS_2;
		
		protected System.Web.UI.WebControls.Label capREIN_CONTACT_CITY;
		
		protected System.Web.UI.WebControls.Label capREIN_CONTACT_COUNTRY;
//		protected System.Web.UI.WebControls.DropDownList cmbREIN_CONTACT_COUNTRY;

		protected System.Web.UI.WebControls.TextBox txtREIN_CONTACT_COUNTRY;
		
		protected System.Web.UI.WebControls.Label capREIN_CONTACT_STATE;
//		protected System.Web.UI.WebControls.DropDownList cmbREIN_CONTACT_STATE;
        protected System.Web.UI.WebControls.DropDownList cmbREIN_CONTACT_SALUTATION;
		protected System.Web.UI.WebControls.TextBox txtREIN_CONTACT_STATE;

		protected System.Web.UI.WebControls.Label capREIN_CONTACT_ZIP;
		
		protected System.Web.UI.WebControls.Label capREIN_CONTACT_PHONE_1;
		
		protected System.Web.UI.WebControls.Label capREIN_CONTACT_EXT_1;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_CONTACT_EXT_1;
		
		protected System.Web.UI.WebControls.Label capREIN_CONTACT_PHONE_2;
		
		protected System.Web.UI.WebControls.Label capM_REIN_CONTACT_EXT_2;
		protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_CONTACT_EXT_2;
	
		//protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_CONTACT_SPEED_DIAL;
		protected System.Web.UI.WebControls.Label capM_REIN_CONTACT_ADDRESS_1;
		protected System.Web.UI.WebControls.TextBox txtM_REIN_CONTACT_ADDRESS_1;
		
		protected System.Web.UI.WebControls.Label capM_REIN_CONTACT_CITY;
		
		protected System.Web.UI.WebControls.Label capM_REIN_CONTACT_COUNTRY;
//		protected System.Web.UI.WebControls.DropDownList cmbM_REIN_CONTACT_COUNTRY;
		protected System.Web.UI.WebControls.TextBox txtM_REIN_CONTACT_COUNTRY;
		protected System.Web.UI.WebControls.Label capM_REIN_CONTACT_STATE;
//		protected System.Web.UI.WebControls.DropDownList cmbM_REIN_CONTACT_STATE;
		protected System.Web.UI.WebControls.TextBox txtM_REIN_CONTACT_STATE;
		protected System.Web.UI.WebControls.Label capM_REIN_CONTACT_ZIP;
	
		protected System.Web.UI.WebControls.Label capM_REIN_CONTACT_EXT_1;
		protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_CONTACT_EXT_1;
		
		//protected System.Web.UI.WebControls.Label capM_REIN_CONTACT_EXT_2;
		//protected System.Web.UI.WebControls.TextBox txtM_REIN_CONTACT_EXT_2;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_CONTACT_EXT_2;
		
		protected System.Web.UI.WebControls.Label capREIN_CONTACT_MOBILE;
		
		protected System.Web.UI.WebControls.Label capREIN_CONTACT_FAX;
		
		protected System.Web.UI.WebControls.Label capREIN_CONTACT_SPEED_DIAL;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_CONTACT_SPEED_DIAL;
		
		protected System.Web.UI.WebControls.Label capREIN_CONTACT_EMAIL_ADDRESS;
        protected System.Web.UI.WebControls.Label capMAND;
        protected System.Web.UI.WebControls.Label capPHYSICAL; 
        protected System.Web.UI.WebControls.Label capMAIL; 
        protected System.Web.UI.WebControls.Label capOTHER; 
		protected System.Web.UI.WebControls.Label capREIN_CONTACT_DESC;

		
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected System.Web.UI.WebControls.Label lblCopy_Address;

		protected System.Web.UI.WebControls.Label capM_REIN_CONTACT_MOBILE;
		protected System.Web.UI.WebControls.TextBox txtM_REIN_CONTACT_MOBILE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_CONTACT_MOBILE;
		protected System.Web.UI.WebControls.Label capM_REIN_CONTACT_FAX;
		protected System.Web.UI.WebControls.TextBox txtM_REIN_CONTACT_FAX;
		protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_CONTACT_FAX;
		protected System.Web.UI.WebControls.Label cap_REIN_CONTACT_SPEED_DIAL;
		protected System.Web.UI.WebControls.TextBox txtM_REIN_CONTACT_SPEED_DIAL;
		protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_CONTACT_SPEED_DIAL;
		protected System.Web.UI.WebControls.Label capCOMMENTS;
		protected System.Web.UI.WebControls.TextBox txtCOMMENTS;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidReset;

		protected System.Web.UI.WebControls.Label capREIN_CONTACT_EXT_2;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_CONTACT_EXT_2;
		protected System.Web.UI.WebControls.Label capM_REIN_CONTACT_EXPHONE_1;
		protected System.Web.UI.WebControls.Label capM_REIN_CONTACT_EXTPHONE_2;
		protected System.Web.UI.WebControls.Label capM_REIN_CONTACT_PHONE_1;
		protected System.Web.UI.WebControls.Label capM_REIN_CONTACT_PHONE_2;
		protected System.Web.UI.WebControls.Label capM_REIN_CONTACT_ADDRESS_2;
		protected System.Web.UI.WebControls.TextBox txtREIN_CONTACT_CONTRACT_DESC;
		protected Cms.CmsWeb.Controls.CmsButton btnPullMailingAddress;
		protected Cms.CmsWeb.Controls.CmsButton btnPullPhysicalAddress;
		protected System.Web.UI.WebControls.Label Label1;
		protected Cms.CmsWeb.Controls.CmsButton btnBackToReinsurer;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCompany_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAddress1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAddress2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidState;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCountry;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidZip;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPhone1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPhone2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFax;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidM_Address1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidM_Address2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidM_City;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidM_State;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidM_Country;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidM_Zip;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidM_Phone1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidM_Phone2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidM_Fax;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidREINCONTACTSALUTATION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_CONTACT_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_CONTACT_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_CONTACT_ADDRESS_1;
		protected System.Web.UI.WebControls.TextBox txtM_REIN_CONTACT_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_CONTACT_ZIP;
		protected System.Web.UI.WebControls.TextBox txtM_REIN_CONTACT_PHONE_1;
//		protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_CONTACT_PHONE_1;
		protected System.Web.UI.WebControls.TextBox txtM_REIN_CONTACT_EXT_1;
		protected System.Web.UI.WebControls.TextBox txtM_REIN_CONTACT_PHONE_2;
//		protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_CONTACT_PHONE_2;
		protected System.Web.UI.WebControls.TextBox txtM_REIN_CONTACT_EXT_2;
		protected System.Web.UI.WebControls.TextBox txtREIN_CONTACT_MOBILE;
//		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_CONTACT_MOBILE;
		protected System.Web.UI.WebControls.TextBox txtREIN_CONTACT_FAX;
//		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_CONTACT_FAX;
		protected System.Web.UI.WebControls.TextBox txtREIN_CONTACT_SPEED_DIAL;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_CONTACT_SPEED_DIAL;
		protected System.Web.UI.WebControls.TextBox txtREIN_CONTACT_EMAIL_ADDRESS;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_CONTACT_EMAIL_ADDRESS;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_CONTACT_ZIP;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_CONTACT_PHONE_1;
		//protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_CONTACT_PHONE_2;
		protected System.Web.UI.WebControls.TextBox txtREIN_CONTACT_ZIP;
		protected System.Web.UI.WebControls.TextBox txtREIN_CONTACT_PHONE_1;
		protected System.Web.UI.WebControls.TextBox txtREIN_CONTACT_EXT_1;
		protected System.Web.UI.WebControls.TextBox txtREIN_CONTACT_PHONE_2;
		protected System.Web.UI.WebControls.TextBox txtREIN_CONTACT_EXT_2;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_CONTACT_NAME;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_CONTACT_SALUTATION;
		protected System.Web.UI.WebControls.TextBox txtREIN_CONTACT_ADDRESS_1;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_CONTACT_ADDRESS_1;
		protected System.Web.UI.WebControls.TextBox txtREIN_CONTACT_ADDRESS_2;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_CONTACT_ADDRESS_2;
		protected System.Web.UI.WebControls.TextBox txtREIN_CONTACT_CITY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_CONTACT_CITY;
		protected System.Web.UI.WebControls.TextBox txtM_REIN_CONTACT_ADDRESS_2;
		protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_CONTACT_ADDRESS_2;
		protected System.Web.UI.WebControls.TextBox txtM_REIN_CONTACT_CITY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_CONTACT_CITY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvM_REIN_CONTACT_ADDRESS_1;
		protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_CONTACT_ADDRESS_1;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREIN_COMAPANY_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_CONTACT_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_CONTACT_ZIP;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvM_REIN_CONTACT_STATE;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvM_REIN_CONTACT_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_CONTACT_PHONE_1;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_CONTACT_PHONE_2;
		protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_CONTACT_PHONE_1;
		protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_CONTACT_PHONE_2;
		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_CONTACT_MOBILE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_CONTACT_FAX_CHECK;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_CONTACT_PHONE_1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_CONTACT_PHONE_2;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvM_REIN_CONTACT_PHONE_1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvM_REIN_CONTACT_PHONE_2;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_CONTACT_MOBILE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_CONTACT_FAX;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_CONTACT_COUNTRY;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvM_REIN_CONTACT_COUNTRY;
		ClsReinsuranceContact objReinsuranceContact;

		# endregion DECLARATION VARIABLES
		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			
		
			rfvREIN_CONTACT_CODE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("53");
			rfvREIN_CONTACT_NAME.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("991");
			rfvREIN_CONTACT_ADDRESS_1.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("742");
			rfvM_REIN_CONTACT_ADDRESS_1.ErrorMessage		=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("742");
			
			rfvREIN_CONTACT_STATE.ErrorMessage	 = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("35");
			rfvREIN_CONTACT_ZIP.ErrorMessage	 = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("37");
//			rfvM_REIN_CONTACT_STATE.ErrorMessage	 = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("35");
//			rfvM_REIN_CONTACT_ZIP.ErrorMessage	 = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("37");
			//rfvREIN_CONTACT_ACC_NUMBER.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage("G","628");
			//revREIN_CONTACT_CITY.ErrorMessage      	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("39");
            this.revREIN_CONTACT_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1084");
            this.revM_REIN_CONTACT_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1084");
			
			//this.revREIN_CONTACT_PHONE_1.ErrorMessage     	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
			//this.revREIN_CONTACT_PHONE_2.ErrorMessage     	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
			this.revREIN_CONTACT_EXT_1.ErrorMessage     	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("25");
			this.revREIN_CONTACT_EXT_2.ErrorMessage     	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("25");
			
			//this.revM_REIN_CONTACT_PHONE_1.ErrorMessage     	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
			//this.revM_REIN_CONTACT_PHONE_2.ErrorMessage     	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
			this.revM_REIN_CONTACT_EXT_1.ErrorMessage     	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("25");
			this.revM_REIN_CONTACT_EXT_2.ErrorMessage     	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("25");
			
			
			//this.revREIN_CONTACT_MOBILE.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("16");
			//this.revREIN_CONTACT_FAX.ErrorMessage	    	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("15");
			this.revREIN_CONTACT_SPEED_DIAL.ErrorMessage		="Please enter 4 digit number";
			this.revREIN_CONTACT_EMAIL_ADDRESS.ErrorMessage	    =	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("124");

            this.revREIN_CONTACT_PHONE_1.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083");
            this.revREIN_CONTACT_PHONE_2.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083");
            this.revM_REIN_CONTACT_PHONE_1.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083");
            this.revM_REIN_CONTACT_PHONE_2.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083");
			this.revREIN_CONTACT_MOBILE.ErrorMessage     	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("999");
            //this.revM_REIN_CONTACT_FAX_CHECK.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1085");

            revREIN_CONTACT_ZIP.ValidationExpression = aRegExpZipBrazil;
			//revREIN_CONTACT_PHONE_1.ValidationExpression=aRegExpPhone;
			//revREIN_CONTACT_PHONE_2.ValidationExpression=aRegExpPhone;
			revREIN_CONTACT_EXT_1.ValidationExpression=@"^[\w\W]{0,5}$";
			revREIN_CONTACT_EXT_2.ValidationExpression=@"^[\w\W]{0,5}$";

            revM_REIN_CONTACT_ZIP.ValidationExpression = aRegExpZipBrazil;
			//revM_REIN_CONTACT_PHONE_1.ValidationExpression=aRegExpPhone;
            revREIN_CONTACT_PHONE_1.ValidationExpression = aRegExpPhoneBrazil;
            revREIN_CONTACT_PHONE_2.ValidationExpression = aRegExpPhoneBrazil;
            revM_REIN_CONTACT_PHONE_1.ValidationExpression = aRegExpPhoneBrazil;
            revM_REIN_CONTACT_PHONE_2.ValidationExpression = aRegExpPhoneBrazil;
			revREIN_CONTACT_MOBILE.ValidationExpression = aRegExpPhoneAll;
            //revM_REIN_CONTACT_FAX_CHECK.ValidationExpression = aRegExpPhoneBrazil;
			//revM_REIN_CONTACT_PHONE_2.ValidationExpression=aRegExpPhone;
			revM_REIN_CONTACT_EXT_1.ValidationExpression=@"^[\w\W]{0,5}$";
			revM_REIN_CONTACT_EXT_2.ValidationExpression=@"^[\w\W]{0,5}$";
			
			//revREIN_CONTACT_FAX.ValidationExpression = aRegExpFax;
			//revREIN_CONTACT_MOBILE.ValidationExpression	= aRegExpPhone;
			revREIN_CONTACT_EMAIL_ADDRESS.ValidationExpression     = aRegExpEmail;
			//revREIN_CONTACT_EMAIL_ADDRESS.ValidationExpression     = aRegExpClientName;
			
			revREIN_CONTACT_SPEED_DIAL.ValidationExpression="^[0-9]{4}";
			
			this.revREIN_CONTACT_NAME.ValidationExpression = @"^[\w\W]{0,75}$";
			this.revREIN_CONTACT_NAME.ErrorMessage			= "Please enter valid name";
			this.revREIN_CONTACT_POSITION.ValidationExpression=@"^[\w\W]{0,30}$";
			this.revREIN_CONTACT_POSITION.ErrorMessage ="Please enter valid position";
			this.revREIN_CONTACT_SALUTATION.ValidationExpression=@"^[\w\W]{0,50}$";  //"^[a-zA-Z0-9]{1,50}";
			this.revREIN_CONTACT_SALUTATION.ErrorMessage ="Please enter valid Salutation";
			this.revREIN_CONTACT_ADDRESS_1.ValidationExpression=@"^[\w\W]{0,50}$";
			this.revREIN_CONTACT_ADDRESS_1.ErrorMessage="Please eneter valid address";
			this.revREIN_CONTACT_ADDRESS_2.ValidationExpression=@"^[\w\W]{0,50}$";
			this.revREIN_CONTACT_ADDRESS_2.ErrorMessage="Please eneter valid address";
			this.revREIN_CONTACT_CITY.ValidationExpression=@"^[\w\W]{0,50}$";
			this.revREIN_CONTACT_CITY.ErrorMessage= "Please eneter valid city";
			
			this.revM_REIN_CONTACT_CITY.ValidationExpression=@"^[\w\W]{0,50}$";//"^[a-zA-Z0-9]{1,50}";
			this.revM_REIN_CONTACT_CITY.ErrorMessage= "Please eneter valid city";
			this.revM_REIN_CONTACT_ADDRESS_1.ValidationExpression=@"^[\w\W]{0,50}$";//"^[a-zA-Z0-9]{1,50}";
			this.revM_REIN_CONTACT_ADDRESS_1.ErrorMessage="Please eneter valid address";
			this.revM_REIN_CONTACT_ADDRESS_2.ValidationExpression=@"^[\w\W]{0,50}$";//"^[a-zA-Z0-9]{1,50}";
			this.revM_REIN_CONTACT_ADDRESS_2.ErrorMessage="Please eneter valid address";

	
			rfvREIN_CONTACT_PHONE_1.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1007");
			rfvREIN_CONTACT_PHONE_2.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1008");
			rfvM_REIN_CONTACT_PHONE_1.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1007");
			rfvM_REIN_CONTACT_PHONE_2.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1008");
			rfvREIN_CONTACT_MOBILE.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("60");
			rfvREIN_CONTACT_FAX.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("138");
			rfvREIN_CONTACT_COUNTRY.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1009");
//			rfvM_REIN_CONTACT_COUNTRY.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1009");
		}

		#endregion

	
		private void Page_Load(object sender, System.EventArgs e)
		{
			this.btnPullPhysicalAddress.Attributes.Add("onclick","return CopyPhysicalAddress();");
			this.btnPullMailingAddress.Attributes.Add("onclick","return CopyMailingAddress();");

			
			//cmbREIN_CONTACT_COUNTRY.SelectedIndex=int.Parse(aCountry); 		           
			//txtREIN_CONTACT_PHONE.Attributes.Add("onBlur","javascript:DisableExt('txtREIN_CONTACT_PHONE','txtREIN_CONTACT_EXT');");
			//btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			btnReset.Attributes.Add("onClick","javascript:return Reset();");
			//this.btnPullMailingAddress.Attributes.Add("onclick","javascript:return CopyPhysicalAddress();") ;
			txtREIN_CONTACT_NAME.Attributes.Add ("onBlur","javascript:GenerateCustomerCode(\"txtREIN_CONTACT_NAME\");");
			btnBackToReinsurer.Attributes.Add("onclick","setURL();");
			//END Harmanjeet		
			// Added by Swarup on 30-mar-2007
			/*imgZipLookup.Attributes.Add("style","cursor:hand");
			base.VerifyAddress(hlkZipLookup, txtREIN_CONTACT_ADDRESS_1,txtREIN_CONTACT_ADDRESS_2
				, txtREIN_CONTACT_CITY, cmbREIN_CONTACT_STATE, txtREIN_CONTACT_ZIP);
			imgMZipLookup.Attributes.Add("style","cursor:hand");
			base.VerifyAddress(hlkMZipLookup, txtM_REIN_CONTACT_ADDRESS_1,txtREIN_CONTACT_ADDRESS_2
				, txtM_REIN_CONTACT_CITY, cmbM_REIN_CONTACT_STATE, txtM_REIN_CONTACT_ZIP);*/

             //PHYSICAL ADRESS 
            imgZipLookup.Attributes.Add("style","cursor:hand");
            base.VerifyAddressState(hlkZipLookup,txtREIN_CONTACT_ADDRESS_1 , txtREIN_CONTACT_ADDRESS_2
				,txtREIN_CONTACT_CITY ,txtREIN_CONTACT_STATE,txtREIN_CONTACT_ZIP);  
			
			//MAILING ADDRESS 
			imgZipLookup.Attributes.Add("style","cursor:hand");
			base.VerifyAddressState(hlkMZipLookup,txtM_REIN_CONTACT_ADDRESS_1 , txtM_REIN_CONTACT_ADDRESS_2
				,txtM_REIN_CONTACT_CITY ,txtM_REIN_CONTACT_STATE,txtM_REIN_CONTACT_ZIP);  

			base.ScreenId="263_0";
			lblMessage.Visible = false;
			//SetErrorMessages();

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	   =	CmsButtonType.Write;
			btnReset.PermissionString  =	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	  =	 CmsButtonType.Write;
			btnSave.PermissionString  =	 gstrSecurityXML;

			
			this.btnPullMailingAddress.CmsButtonClass =     CmsButtonType.Write;
			btnPullMailingAddress.PermissionString =	gstrSecurityXML;

			

			this.btnPullPhysicalAddress.CmsButtonClass =     CmsButtonType.Write;
			btnPullPhysicalAddress.PermissionString =	gstrSecurityXML;


			this.btnBackToReinsurer.CmsButtonClass =CmsButtonType.Write;
			this.btnBackToReinsurer.PermissionString = gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			//objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Reinsurance.AddReinsurer" ,System.Reflection.Assembly.GetExecutingAssembly());
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Reinsurance.ReinsuranceContact" ,System.Reflection.Assembly.GetExecutingAssembly());

			//ReadOnlyFields();

			if(!Page.IsPostBack)
			{																 
				
				
             	hidCompany_ID.Value = Request.Params["REIN_COMAPANY_ID"].ToString();
				
				
				if (Request.Params["REIN_CONTACT_ID"] != null)
				{
					if (Request.Params["REIN_CONTACT_ID"].ToString() != "")
					{
						hidREIN_CONTACT_ID.Value = Request.Params["REIN_CONTACT_ID"].ToString();
						GenerateXML(hidREIN_CONTACT_ID.Value);
					}
				}
				if(Request.QueryString["REIN_COMAPANY_ID"]!= null)
				{
					hidREIN_COMAPANY_ID.Value = Request.QueryString["REIN_COMAPANY_ID"].ToString();				
				}
                SetErrorMessages();
                SetCaptions();
                //-------------------------Sneha For itrack 834-----------------------------------
                string strSysID = GetSystemId();
                if (strSysID == "ALBAUAT")
                    strSysID = "ALBA";

                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "cmsweb/support/PageXML/" + strSysID, "ReinsuranceContact.xml"))
                    setPageControls(Page, Request.PhysicalApplicationPath + "cmsweb/support/PageXml/" + strSysID + "/ReinsuranceContact.xml");

                //------------------------End----------------------------------------
               
				SetDropdownList();
                
				#region "Loading singleton"
				//using singleton object for country and state dropdown
				
				DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
				/*cmbREIN_CONTACT_COUNTRY.DataSource		= dt;
				cmbREIN_CONTACT_COUNTRY.DataTextField	= "Country_Name";
				cmbREIN_CONTACT_COUNTRY.DataValueField	= "Country_Id";
				cmbREIN_CONTACT_COUNTRY.DataBind();
				cmbREIN_CONTACT_COUNTRY.Items[0].Selected=true;  */

/*				cmbM_REIN_CONTACT_COUNTRY.DataSource		= dt;
				cmbM_REIN_CONTACT_COUNTRY.DataTextField	= "Country_Name";
				cmbM_REIN_CONTACT_COUNTRY.DataValueField	= "Country_Id";
				cmbM_REIN_CONTACT_COUNTRY.DataBind();
				cmbM_REIN_CONTACT_COUNTRY.Items[0].Selected=true; 

				dt = Cms.CmsWeb.ClsFetcher.State;
				cmbREIN_CONTACT_STATE.DataSource		= dt;
				cmbREIN_CONTACT_STATE.DataTextField	= "STATE_NAME";
				cmbREIN_CONTACT_STATE.DataValueField	= "STATE_ID";
				cmbREIN_CONTACT_STATE.DataBind();
				cmbREIN_CONTACT_STATE.Items.Insert(0,"");*/
				
				//START APRIL 11 HARMANJEET
				//cmbREIN_CONTACT_TYPE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("REINTY");
				//cmbSEX.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("SEX");
				//cmbREIN_CONTACT_TYPE.DataTextField="LookupDesc";
				//cmbREIN_CONTACT_TYPE.DataValueField="LookupCode";
				//cmbREIN_CONTACT_TYPE.DataBind();
				//cmbREIN_CONTACT_TYPE.Items.Insert(0,"");	
				//END HARMANJEET

//				dt = Cms.CmsWeb.ClsFetcher.State;
//				cmbM_REIN_CONTACT_STATE.DataSource		= dt;
//				cmbM_REIN_CONTACT_STATE.DataTextField	= "STATE_NAME";
//				cmbM_REIN_CONTACT_STATE.DataValueField	= "STATE_ID";
//				cmbM_REIN_CONTACT_STATE.DataBind();
//				cmbM_REIN_CONTACT_STATE.Items.Insert(0,"");
				

				#endregion//Loading singleton
				GetDataForEditMode();
				getAddressValues();
				
				//if(Request.QueryString["REIN_CONTACT_ID"] !=null && Request.QueryString["REIN_CONTACT_ID"].ToString().Length >0)
				//	hidREIN_CONTACT_ID.Value = Request.QueryString["REIN_CONTACT_ID"].ToString();
			}
		}

		private void SetDropdownList()
		{
			//cmbREIN_CONTACT_IS_BROKER.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			//cmbREIN_CONTACT_IS_BROKER.DataTextField="LookupDesc"; 
			//cmbREIN_CONTACT_IS_BROKER.DataValueField="LookupCode";
			//cmbREIN_CONTACT_IS_BROKER.DataBind();
			//cmbREIN_CONTACT_IS_BROKER.Items.Insert(0,"");
		}
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsReinsuranceContactInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			Cms.Model.Maintenance.Reinsurance.ClsReinsuranceContactInfo objReinsuranceContactInfo;
			objReinsuranceContactInfo = new ClsReinsuranceContactInfo();
            if (cmbREIN_CONTACT_SALUTATION.SelectedValue != "")
            {
                hidREINCONTACTSALUTATION.Value = cmbREIN_CONTACT_SALUTATION.Text;
            }
            else
            {
                hidREINCONTACTSALUTATION.Value = txtREIN_CONTACT_SALUTATION.Text;
            }
			objReinsuranceContactInfo.REIN_CONTACT_CODE  =	txtREIN_CONTACT_CODE.Text;
			objReinsuranceContactInfo.REIN_CONTACT_NAME  =	txtREIN_CONTACT_NAME.Text;
			
			objReinsuranceContactInfo.REIN_CONTACT_POSITION=this.txtREIN_CONTACT_POSITION.Text;
            objReinsuranceContactInfo.REIN_CONTACT_SALUTATION =hidREINCONTACTSALUTATION.Value; //this.txtREIN_CONTACT_SALUTATION.Text;

			objReinsuranceContactInfo.REIN_CONTACT_ADDRESS_1  =	txtREIN_CONTACT_ADDRESS_1.Text;
			objReinsuranceContactInfo.REIN_CONTACT_ADDRESS_2 =	txtREIN_CONTACT_ADDRESS_2.Text;
			objReinsuranceContactInfo.REIN_CONTACT_CITY  =	txtREIN_CONTACT_CITY.Text;
//			objReinsuranceContactInfo.REIN_CONTACT_COUNTRY=	cmbREIN_CONTACT_COUNTRY.SelectedValue;
			objReinsuranceContactInfo.REIN_CONTACT_COUNTRY=txtREIN_CONTACT_COUNTRY.Text;
//			objReinsuranceContactInfo.REIN_CONTACT_STATE  =	cmbREIN_CONTACT_STATE.SelectedValue;
			objReinsuranceContactInfo.REIN_CONTACT_STATE  =	txtREIN_CONTACT_STATE.Text;
			objReinsuranceContactInfo.REIN_CONTACT_ZIP    =	txtREIN_CONTACT_ZIP.Text;
			objReinsuranceContactInfo.REIN_CONTACT_PHONE_1  =	txtREIN_CONTACT_PHONE_1.Text;
			objReinsuranceContactInfo.REIN_CONTACT_PHONE_2   =	txtREIN_CONTACT_PHONE_2.Text;
			objReinsuranceContactInfo.REIN_CONTACT_EXT_1 = this.txtREIN_CONTACT_EXT_1.Text;
			objReinsuranceContactInfo.REIN_CONTACT_EXT_2 = this.txtREIN_CONTACT_EXT_2.Text;

			
			objReinsuranceContactInfo.M_REIN_CONTACT_ADDRESS_1= txtM_REIN_CONTACT_ADDRESS_1.Text;
			objReinsuranceContactInfo.M_REIN_CONTACT_ADDRESS_2= this.txtM_REIN_CONTACT_ADDRESS_2.Text;
			objReinsuranceContactInfo.M_REIN_CONTACT_CITY =txtM_REIN_CONTACT_CITY.Text;			
//			objReinsuranceContactInfo.M_REIN_CONTACT_COUNTRY= cmbM_REIN_CONTACT_COUNTRY.SelectedValue;
			objReinsuranceContactInfo.M_REIN_CONTACT_COUNTRY= txtM_REIN_CONTACT_COUNTRY.Text;
//			objReinsuranceContactInfo.M_REIN_CONTACT_STATE= cmbM_REIN_CONTACT_STATE.SelectedValue;
			objReinsuranceContactInfo.M_REIN_CONTACT_STATE= txtM_REIN_CONTACT_STATE.Text;
			objReinsuranceContactInfo.M_REIN_CONTACT_ZIP= txtM_REIN_CONTACT_ZIP.Text;
			objReinsuranceContactInfo.M_REIN_CONTACT_PHONE_1= txtM_REIN_CONTACT_PHONE_1.Text;
			objReinsuranceContactInfo.M_REIN_CONTACT_PHONE_2= txtM_REIN_CONTACT_PHONE_2.Text;
			objReinsuranceContactInfo.M_REIN_CONTACT_EXT_1 = this.txtM_REIN_CONTACT_EXT_1.Text;
			objReinsuranceContactInfo.M_REIN_CONTACT_EXT_2 = this.txtM_REIN_CONTACT_EXT_2.Text;


			objReinsuranceContactInfo.REIN_CONTACT_MOBILE =	txtREIN_CONTACT_MOBILE.Text;
			objReinsuranceContactInfo.REIN_CONTACT_FAX    =	txtREIN_CONTACT_FAX.Text;
			objReinsuranceContactInfo.REIN_CONTACT_SPEED_DIAL=this.txtREIN_CONTACT_SPEED_DIAL.Text;
			objReinsuranceContactInfo.REIN_CONTACT_EMAIL_ADDRESS = this.txtREIN_CONTACT_EMAIL_ADDRESS.Text;
			objReinsuranceContactInfo.REIN_CONTACT_CONTRACT_DESC=this.txtREIN_CONTACT_CONTRACT_DESC.Text;
			objReinsuranceContactInfo.REIN_CONTACT_COMMENTS=this.txtCOMMENTS.Text;
			objReinsuranceContactInfo.REIN_COMAPANY_ID = int.Parse(hidREIN_COMAPANY_ID.Value);
					
			//objReinsuranceContactInfo.IS_ACTIVE=hidIS_ACTIVE.Value;
			
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidREIN_CONTACT_ID.Value;
			//oldXML		= hidOldData.Value;
			//Returning the model object

			return objReinsuranceContactInfo;
		}
		#endregion
		
		# region PULL ADDRESS VALUES
		private void getAddressValues()
		{
			try
			{
				ClsReinsurer objReinsurer=new ClsReinsurer();
				string str=Request.QueryString["REIN_COMAPANY_ID"];
				//Response.Write(str);
				DataSet oDs=objReinsurer.GetDataForPageControls(str);
				if(oDs.Tables[0].Rows.Count > 0)
				{
					DataRow oDr=oDs.Tables[0].Rows[0];

                    hidAddress1.Value = oDr["REIN_COMAPANY_ADD1"].ToString();
                    hidAddress2.Value = oDr["REIN_COMAPANY_ADD2"].ToString();
                    hidCity.Value = oDr["REIN_COMAPANY_CITY"].ToString();
                    hidZip.Value = oDr["REIN_COMAPANY_ZIP"].ToString();
					//hidState.Value=oDr["REIN_COMPANY_STATE"].ToString();
                    hidState.Value = oDr["REIN_COMAPANY_STATE"].ToString();
                    hidCountry.Value = oDr["REIN_COMAPANY_COUNTRY"].ToString();
                    hidPhone1.Value = oDr["REIN_COMAPANY_PHONE"].ToString();
                    hidPhone2.Value = oDr["REIN_COMAPANY_EXT"].ToString();
                    hidFax.Value = oDr["REIN_COMAPANY_FAX"].ToString();

					hidM_Address1.Value=oDr["M_REIN_COMPANY_ADD_1"].ToString();
					hidM_Address2.Value=oDr["M_RREIN_COMPANY_ADD_2"].ToString();
					hidM_City.Value=oDr["M_REIN_COMPANY_CITY"].ToString();
					hidM_Zip.Value=oDr["M_REIN_COMPANY_ZIP"].ToString();
					hidM_State.Value=oDr["M_REIN_COMPANY_STATE"].ToString();
					hidM_Country.Value=oDr["M_REIN_COMPANY_COUNTRY"].ToString();
					hidM_Phone1.Value=oDr["M_REIN_COMPANY_PHONE"].ToString();
					hidM_Phone2.Value=oDr["M_REIN_COMPANY_EXT"].ToString();
					hidM_Fax.Value=oDr["M_REIN_COMPANY_FAX"].ToString();


				}

			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
			finally{}

		}

		# endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
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
				int intRetVal;	//For retreiving the return value of business class save function
				objReinsuranceContact = new ClsReinsuranceContact();

				//Retreiving the form values into model class object
				ClsReinsuranceContactInfo objReinsuranceContactInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objReinsuranceContactInfo.CREATED_BY = int.Parse(GetUserId());
					objReinsuranceContactInfo.CREATED_DATETIME = DateTime.Now;
					//objReinsuranceContactInfo.IS_ACTIVE="Y"; 

					//Calling the add method of business layer class
					intRetVal = objReinsuranceContact.Add(objReinsuranceContactInfo);

					if(intRetVal>0)
					{
						hidREIN_CONTACT_ID.Value = objReinsuranceContactInfo.REIN_CONTACT_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						this.hidOldData.Value	= objReinsuranceContact.GetDataForPageControls(this.hidREIN_CONTACT_ID.Value).GetXml();
                      
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
					ClsReinsuranceContactInfo objOldReinsuranceContactInfo;
					objOldReinsuranceContactInfo = new ClsReinsuranceContactInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldReinsuranceContactInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					if(strRowId!="")
						objReinsuranceContactInfo.REIN_CONTACT_ID = int.Parse(strRowId);
					objReinsuranceContactInfo.MODIFIED_BY = int.Parse(GetUserId());
					objReinsuranceContactInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                    

					//Updating the record using business layer class object
					intRetVal	= objReinsuranceContact.Update(objOldReinsuranceContactInfo,objReinsuranceContactInfo);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						this.hidOldData.Value	= objReinsuranceContact.GetDataForPageControls(strRowId).GetXml();
                   
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
                if (hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
                {
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("Y");
                }

                else
                {
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("N");
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
				if(objReinsuranceContact!= null)
					objReinsuranceContact.Dispose();
			}

			//if(hidREIN_CONTACT_ID.Value.ToUpper()!="NEW")
			//	GenerateXML(hidREIN_CONTACT_ID.Value);
		}
		#endregion

		/// <summary>
		/// fetching data based on query string values
		/// </summary>
		private void GenerateXML(string REIN_CONTACT_ID)
		{
			string strREIN_CONTACT_ID=REIN_CONTACT_ID;
            
			objReinsuranceContact=new ClsReinsuranceContact(); 
  
			
			if(strREIN_CONTACT_ID!="" && strREIN_CONTACT_ID!=null)
			{
				try
				{
					DataSet ds=new DataSet(); 
					ds=objReinsuranceContact.GetDataForPageControls(strREIN_CONTACT_ID);
					hidOldData.Value=ds.GetXml(); 

					//hidFormSaved.Value="1"; 
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
					if(objReinsuranceContact!= null)
						objReinsuranceContact.Dispose();
				}  
                
			}
                
		}

		private void SetCaptions()
		{
			capREIN_CONTACT_NAME.Text					=		objResourceMgr.GetString("txtREIN_CONTACT_NAME");
			capREIN_CONTACT_CODE.Text					=		objResourceMgr.GetString("txtREIN_CONTACT_CODE");
			this.capREIN_CONTACT_POSITION.Text			=		objResourceMgr.GetString("txtREIN_CONTACT_POSITION");
			this.capREIN_CONTACT_SALUTATION.Text		=		objResourceMgr.GetString("txtREIN_CONTACT_SALUTATION");
			this.capREIN_CONTACT_STATE.Text				=		objResourceMgr.GetString("cmbREIN_CONTACT_STATE");
			capREIN_CONTACT_ADDRESS_1 .Text				=		objResourceMgr.GetString("txtREIN_CONTACT_ADDRESS_1");
			capREIN_CONTACT_ADDRESS_2.Text				=		objResourceMgr.GetString("txtREIN_CONTACT_ADDRESS_2");
			capREIN_CONTACT_CITY.Text					=		objResourceMgr.GetString("txtREIN_CONTACT_CITY");
			capREIN_CONTACT_COUNTRY.Text				=		objResourceMgr.GetString("cmbREIN_CONTACT_COUNTRY");
			//capREIN_CONTACT_STATE.Text					=		objResourceMgr.GetString("cmbAREIN_CONTACT_STATE");
			capREIN_CONTACT_ZIP.Text					=		objResourceMgr.GetString("txtREIN_CONTACT_ZIP");
			capREIN_CONTACT_PHONE_1.Text				=		objResourceMgr.GetString("txtREIN_CONTACT_PHONE_1");
			capREIN_CONTACT_PHONE_2.Text				=		objResourceMgr.GetString("txtREIN_CONTACT_PHONE_2");
			capREIN_CONTACT_EXT_1.Text				=		objResourceMgr.GetString("txtREIN_CONTACT_EXT_1");
			capREIN_CONTACT_EXT_2.Text				=		objResourceMgr.GetString("txtREIN_CONTACT_EXT_2");
			capM_REIN_CONTACT_EXT_1.Text				=		objResourceMgr.GetString("txtM_REIN_CONTACT_EXT_1");
			capM_REIN_CONTACT_EXT_2.Text				=		objResourceMgr.GetString("txtM_REIN_CONTACT_EXT_2");
		
			capREIN_CONTACT_FAX.Text					=		objResourceMgr.GetString("txtREIN_CONTACT_FAX");
			capREIN_CONTACT_MOBILE.Text	     		=		objResourceMgr.GetString("txtREIN_CONTACT_MOBILE");
			capREIN_CONTACT_EMAIL_ADDRESS.Text			=		objResourceMgr.GetString("txtREIN_CONTACT_EMAIL_ADDRESS");
			capM_REIN_CONTACT_ADDRESS_1.Text				=		objResourceMgr.GetString("txtM_REIN_CONTACT_ADDRESS_1");
			capM_REIN_CONTACT_ADDRESS_2.Text				=		objResourceMgr.GetString("txtM_RREIN_CONTACT_ADDRESS_2");
			capM_REIN_CONTACT_CITY.Text					=		objResourceMgr.GetString("txtM_REIN_CONTACT_CITY");
			capM_REIN_CONTACT_COUNTRY.Text				=		objResourceMgr.GetString("cmbM_REIN_CONTACT_COUNTRY");
			capM_REIN_CONTACT_STATE.Text				=	    objResourceMgr.GetString("cmbM_REIN_CONTACT_STATE");
			capM_REIN_CONTACT_ZIP.Text					=		objResourceMgr.GetString("txtM_REIN_CONTACT_ZIP");
			capM_REIN_CONTACT_PHONE_1.Text				=	   objResourceMgr.GetString("txtM_REIN_CONTACT_PHONE_1");
			this.capREIN_CONTACT_DESC.Text					=	   objResourceMgr.GetString("txtREIN_CONTACT_CONTRACT_DESC");
			capM_REIN_CONTACT_PHONE_2.Text				=	   objResourceMgr.GetString("txtM_REIN_CONTACT_PHONE_2");
			this.capCOMMENTS.Text =	this.objResourceMgr.GetString("txtCOMMENTS");
			this.capREIN_CONTACT_SPEED_DIAL.Text=this.objResourceMgr.GetString("txtREIN_CONTACT_SPEED_DIAL");
            capMAND.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168"); 
            capPHYSICAL.Text = objResourceMgr.GetString("capPHYSICAL"); 
            capMAIL.Text = objResourceMgr.GetString("capMAIL"); 
            capOTHER.Text = objResourceMgr.GetString("capOTHER"); 
            btnPullMailingAddress.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "17");
            btnPullPhysicalAddress.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "16"); 
            btnBackToReinsurer.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "18"); 
			//END Harmanjeet
		}

		

		/*#region DEACTIVATE ACTIVATE BUTTON CLICK
		
		private void btnActivate_Click(object sender, System.EventArgs e)
		{
		
			objReinsuranceContact = new ClsReinsuranceContact();
			
						
			if(btnActivate.Text=="Deactivate")
			{
						  
				int intStatusCheck=objReinsuranceContact.GetDeactivateActivate(this.hidREIN_CONTACT_ID.Value.ToString(),"N");
				btnActivate.Text="Activate";
				lblMessage.Visible = true;
				lblMessage.Text ="Information deactivated successfully.";
			}
			else
			{
				
				int intStatusCheck=objReinsuranceContact.GetDeactivateActivate(this.hidREIN_CONTACT_ID.Value.ToString(),"Y");
				btnActivate.Text="Deactivate";
				lblMessage.Visible = true;
				lblMessage.Text ="Information activated successfully.";

			}
			
		}
		
		#endregion */
		#region DEACTIVATE ACTIVATE BUTTON CLICK
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				//objDepartment =  new ClsDepartment();
				Cms.BusinessLayer.BlCommon.ClsReinsuranceContact objReinsuranceContact =new ClsReinsuranceContact();

				Model.Maintenance.Reinsurance.ClsReinsuranceContactInfo objReinsuranceContactInfo;
				objReinsuranceContactInfo = GetFormValue();
				
				string strRetVal = "";
				string strCustomInfo ="Contact Name:"+ objReinsuranceContactInfo.REIN_CONTACT_NAME+"<br>"
					+"Contact Code:" + objReinsuranceContactInfo.REIN_CONTACT_CODE ;
					
				/*strCustomInfo = "Line Of Business:" + cmbREIN_LINE_OF_BUSINESS.Items[cmbREIN_LINE_OF_BUSINESS.SelectedIndex].Text +"<br>"
					+"State:" + cmbREIN_STATE.Items[cmbREIN_STATE.SelectedIndex].Text +"<br>"
					+"1st Split Coverage:" + cmbREIN_IST_SPLIT_COVERAGE.Items[cmbREIN_IST_SPLIT_COVERAGE.SelectedIndex].Text +"<br>"
					+"2nd Split Coverage:" + cmbREIN_2ND_SPLIT_COVERAGE.Items[cmbREIN_2ND_SPLIT_COVERAGE.SelectedIndex].Text;*/
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "Reinsurer Contact Information Has Been Deactivated Successfully.";
					objReinsuranceContact.TransactionInfoParams = objStuTransactionInfo;
					strRetVal = objReinsuranceContact.ActivateDeactivate(hidREIN_CONTACT_ID.Value,"N",strCustomInfo);
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
                    btnActivateDeactivate.Text = "Activate";
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Reinsurer Contact Information Has Been Activated Successfully.";
					objReinsuranceContact.TransactionInfoParams = objStuTransactionInfo;
					objReinsuranceContact.ActivateDeactivate(hidREIN_CONTACT_ID.Value,"Y",strCustomInfo);
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
                    btnActivateDeactivate.Text = "DeActivate";
				}

				//				if (strRetVal == "-1")
				//				{
				//					/*Profit Center is assigned*/
				//					lblMessage.Text =  ClsMessages.GetMessage(base.ScreenId,"513");
				//					lblDelete.Visible = false;
				//				}
				
				hidFormSaved.Value			=	"0";
				GetOldDataXML();
				hidReset.Value="0";

			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;
				if(objReinsuranceContact!= null)
					objReinsuranceContact.Dispose();
			}
		}
		#endregion

		private void GetOldDataXML()
		{
			Cms.BusinessLayer.BlCommon.ClsReinsuranceContact objReinsuranceContact= new ClsReinsuranceContact();
			if (hidREIN_CONTACT_ID.Value.ToString() != "" )
            {

                DataSet dsCustomer = objReinsuranceContact.GetDataForPageControls(strRowId);
                //DataTable dt = dsCustomer.Tables[0];
                //string strXML = "";
                //strXML = ClsCommon.GetXMLEncoded(dt);
                //FetchCountryState(strXML);
                this.hidOldData.Value = dsCustomer.GetXml();

				//this.hidOldData.Value	= objReinsuranceContact.GetDataForPageControls(strRowId).GetXml();
			}
		}
		# region  G E T   D A T A   F O R   E D I T   M O D E 

		private void GetDataForEditMode()
		{
			try
			{
				objReinsuranceContact = new ClsReinsuranceContact();
				DataSet oDs = objReinsuranceContact.GetDataForPageControls(this.hidREIN_CONTACT_ID.Value);
				if(oDs.Tables[0].Rows.Count >0)
				{
					DataRow oDr  = oDs.Tables[0].Rows[0];
					
					//if(oDr["REIN_COMAPANY_NAME"].ToString() != "")
					//	txtREIN_COMAPANY_NAME.Text		= oDr["REIN_COMAPANY_NAME"].ToString();
					
					this.txtREIN_CONTACT_CODE.Text=oDr["REIN_CONTACT_CODE"].ToString();
					txtREIN_CONTACT_NAME.Text=oDr["REIN_CONTACT_NAME"].ToString();
					txtREIN_CONTACT_POSITION.Text=oDr["REIN_CONTACT_POSITION"].ToString();
					txtREIN_CONTACT_SALUTATION.Text=oDr["REIN_CONTACT_SALUTATION"].ToString();

					//this.txttxtREIN_COMAPANY_NAME.Text=oDr["REIN_COMAPANY_CODE"].ToString();

					txtREIN_CONTACT_ADDRESS_1.Text=oDr["REIN_CONTACT_ADDRESS_1"].ToString();
					txtREIN_CONTACT_ADDRESS_2.Text=oDr["REIN_CONTACT_ADDRESS_2"].ToString();
					txtREIN_CONTACT_CITY.Text=oDr["REIN_CONTACT_CITY"].ToString();
					//cmbREIN_CONTACT_COUNTRY.SelectedValue=oDr["REIN_CONTACT_COUNTRY"].ToString();
					//cmbREIN_CONTACT_STATE.SelectedValue=oDr["REIN_CONTACT_STATE"].ToString();
					txtREIN_CONTACT_COUNTRY.Text=oDr["REIN_CONTACT_COUNTRY"].ToString();
					txtREIN_CONTACT_STATE.Text=oDr["REIN_CONTACT_STATE"].ToString();

					txtREIN_CONTACT_ZIP.Text=oDr["REIN_CONTACT_ZIP"].ToString();
					txtREIN_CONTACT_PHONE_1.Text=oDr["REIN_CONTACT_PHONE_1"].ToString();
					txtREIN_CONTACT_PHONE_2.Text=oDr["REIN_CONTACT_PHONE_2"].ToString();

					txtREIN_CONTACT_EXT_1.Text=oDr["REIN_CONTACT_EXT_1"].ToString();
					txtREIN_CONTACT_EXT_2.Text=oDr["REIN_CONTACT_EXT_2"].ToString();

					txtREIN_CONTACT_FAX.Text=oDr["REIN_CONTACT_FAX"].ToString();
					txtREIN_CONTACT_MOBILE.Text=oDr["REIN_CONTACT_MOBILE"].ToString();
					txtREIN_CONTACT_SPEED_DIAL.Text=oDr["REIN_CONTACT_SPEED_DIAL"].ToString();

					txtREIN_CONTACT_EMAIL_ADDRESS.Text=oDr["REIN_CONTACT_EMAIL_ADDRESS"].ToString();
					txtREIN_CONTACT_CONTRACT_DESC.Text=oDr["REIN_CONTACT_CONTRACT_DESC"].ToString();
					txtCOMMENTS.Text=oDr["REIN_CONTACT_COMMENTS"].ToString();

					txtM_REIN_CONTACT_ADDRESS_1.Text=oDr["M_REIN_CONTACT_ADDRESS_1"].ToString();
					txtM_REIN_CONTACT_ADDRESS_2.Text=oDr["M_REIN_CONTACT_ADDRESS_2"].ToString();
					txtM_REIN_CONTACT_CITY.Text=oDr["M_REIN_CONTACT_CITY"].ToString();			
					//cmbM_REIN_CONTACT_COUNTRY.SelectedValue=oDr["M_REIN_CONTACT_COUNTRY"].ToString();
					// cmbM_REIN_CONTACT_STATE.SelectedValue=oDr["M_REIN_CONTACT_STATE"].ToString();

					txtM_REIN_CONTACT_COUNTRY.Text=oDr["M_REIN_CONTACT_COUNTRY"].ToString();
					txtM_REIN_CONTACT_STATE.Text=oDr["M_REIN_CONTACT_STATE"].ToString();

					txtM_REIN_CONTACT_ZIP.Text=oDr["M_REIN_CONTACT_ZIP"].ToString();
					txtM_REIN_CONTACT_PHONE_1.Text=oDr["M_REIN_CONTACT_PHONE_1"].ToString();
					txtM_REIN_CONTACT_PHONE_2.Text=oDr["M_REIN_CONTACT_PHONE_2"].ToString();

					
					txtM_REIN_CONTACT_EXT_1.Text=oDr["M_REIN_CONTACT_EXT_1"].ToString();
					txtM_REIN_CONTACT_EXT_2.Text=oDr["M_REIN_CONTACT_EXT_2"].ToString();
                    if (oDr["IS_ACTIVE"].ToString() == "Y")
                        btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("Y");
                    if (oDr["IS_ACTIVE"].ToString() == "N")
                        btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("N");
					
				}
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
			finally{}
		}

		# endregion G E T   D A T A   F O R   E D I T   M O D E

		


		
	}
}
