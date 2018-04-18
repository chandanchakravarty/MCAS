/******************************************************************************************
<Author					: - Ajit Singh Chahal
<Start Date				: -	4/1/2005 12:19:17 PM
<End Date				: -	
<Description			: - Code Behind for Add Vendor.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 12/04/2005
<Modified By			: - Anurag Verma
<Purpose				: - added "/" in text of activatedeactivate button

<Modified Date			: - 17/05/2005
<Modified By			: - Gaurav
<Purpose				: - added Web Grid Functionality

<Modified Date			: - 15/06/2005
<Modified By			: - Priya
<Purpose				: -Added Delete button functionality

<Modified Date			: - 26/08/2005
<Modified By			: - Anurag Verma
<Purpose				: - Applying Null Check for buttons on aspx page
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
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Maintenance; 
using System.IO;
using System.Xml;
using Cms.CmsWeb.Utils;

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Code Behind class for Add Vendor.
	/// </summary>
	public class AddVendor : Cms.CmsWeb.cmsbase
	{

		#region Page Variable Declarations
		protected System.Web.UI.WebControls.TextBox txtVENDOR_CODE;
		protected System.Web.UI.WebControls.TextBox txtVENDOR_FNAME;
		protected System.Web.UI.WebControls.TextBox txtVENDOR_LNAME;
		protected System.Web.UI.WebControls.TextBox txtVENDOR_ADD1;
		protected System.Web.UI.WebControls.TextBox txtVENDOR_ADD2;
		protected System.Web.UI.WebControls.TextBox txtVENDOR_CITY;
		protected System.Web.UI.WebControls.DropDownList cmbVENDOR_COUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbVENDOR_STATE;
		protected System.Web.UI.WebControls.TextBox txtVENDOR_ZIP;
		protected System.Web.UI.WebControls.TextBox txtVENDOR_PHONE;
		protected System.Web.UI.WebControls.TextBox txtVENDOR_EXT;
		protected System.Web.UI.WebControls.TextBox txtVENDOR_FAX;
		protected System.Web.UI.WebControls.TextBox txtVENDOR_MOBILE;
		protected System.Web.UI.WebControls.TextBox txtVENDOR_EMAIL;
		protected System.Web.UI.WebControls.DropDownList cmbVENDOR_SALUTATION;
		protected System.Web.UI.WebControls.TextBox txtVENDOR_FEDERAL_NUM;
		protected System.Web.UI.WebControls.TextBox txtVENDOR_NOTE;
		protected System.Web.UI.WebControls.TextBox txtVENDOR_ACC_NUMBER;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVENDOR_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACCOUNT_TYPE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREVERIFIED_AC;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		//Added By Raghav For Special Handling/
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidREQ_SPECIAL_HANDLING;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVENDOR_FNAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVENDOR_LNAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVENDOR_SALUTATION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVENDOR_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVENDOR_ADD1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVENDOR_COUNTRY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVENDOR_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVENDOR_CITY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVENDOR_ZIP;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVENDOR_EMAIL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVENDOR_FEDERAL_NUM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVENDOR_ACC_NUMBER;

		protected System.Web.UI.WebControls.RegularExpressionValidator revVENDOR_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revVENDOR_PHONE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revVENDOR_EXT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revVENDOR_FAX;
		protected System.Web.UI.WebControls.RegularExpressionValidator revVENDOR_MOBILE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revVENDOR_EMAIL;
		protected System.Web.UI.WebControls.RegularExpressionValidator revVENDOR_FEDERAL_NUM;
		protected System.Web.UI.WebControls.RegularExpressionValidator revVENDOR_ACC_NUMBER;
		protected System.Web.UI.WebControls.CustomValidator csvVENDOR_NOTE;
		protected System.Web.UI.WebControls.Label capVENDOR_LNAME;
		protected System.Web.UI.WebControls.Label capVENDOR_FNAME;
		protected System.Web.UI.WebControls.Label capVENDOR_SALUTATION;
		protected System.Web.UI.WebControls.Label capVENDOR_CODE;
		protected System.Web.UI.WebControls.Label capVENDOR_ADD1;
		protected System.Web.UI.WebControls.Label capVENDOR_ADD2;
		protected System.Web.UI.WebControls.Label capVENDOR_CITY;
		protected System.Web.UI.WebControls.Label capVENDOR_COUNTRY;
		protected System.Web.UI.WebControls.Label capVENDOR_STATE;
		protected System.Web.UI.WebControls.Label capVENDOR_ZIP;
		protected System.Web.UI.WebControls.Label capVENDOR_PHONE;
		protected System.Web.UI.WebControls.Label capVENDOR_EXT;
		protected System.Web.UI.WebControls.Label capVENDOR_FAX;
		protected System.Web.UI.WebControls.Label capVENDOR_MOBILE;
		protected System.Web.UI.WebControls.Label capVENDOR_EMAIL;
        protected System.Web.UI.WebControls.Label capCHKDETIL; //sneha
        protected System.Web.UI.WebControls.Label capMAILDETL; //SNEHA
        protected System.Web.UI.WebControls.Label capBANKINFO; //sneha
        protected System.Web.UI.WebControls.Label capCONTCTDETL; //SNEHA
        protected System.Web.UI.WebControls.Label capCPF;
        protected System.Web.UI.WebControls.TextBox txtCPF;
        protected System.Web.UI.WebControls.Label capDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.TextBox txtDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.Label capACTIVITY;
        protected System.Web.UI.WebControls.DropDownList cmbACTIVITY;
        protected System.Web.UI.WebControls.Label capREGIONAL_ID;
        protected System.Web.UI.WebControls.TextBox txtREGIONAL_IDENTIFICATION;
        protected System.Web.UI.WebControls.Label capREGIONAL_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.TextBox txtREG_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.Label capREGIONAL_ID_ISSUE;
        protected System.Web.UI.WebControls.TextBox txtREG_ID_ISSUE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCPF_CNPJ;
        protected System.Web.UI.WebControls.HyperLink hlkREG_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.HyperLink hlkDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.RegularExpressionValidator revDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.RegularExpressionValidator revREGIONAL_ID_ISSUE_DATE;
        
        //Added By Raghav for Special Handling.
		protected System.Web.UI.WebControls.Label capREQ_SPECIAL_HANDLING; 
		protected System.Web.UI.WebControls.CheckBox chkREQ_SPECIAL_HANDLING;

		protected System.Web.UI.WebControls.Label capVENDOR_FEDERAL_NUM;
		protected System.Web.UI.WebControls.Label capVENDOR_ACC_NUMBER;
		protected System.Web.UI.WebControls.Label capVENDOR_NOTE;
		protected System.Web.UI.WebControls.Label capREVERIFIED_AC;
        protected System.Web.UI.WebControls.Label capMessages;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTAB_TITLES;

		string oldXML,strPageMode;
		private string	strRowId, strFormSaved;
		protected System.Web.UI.WebControls.Label lblMessage;
		private int	intLoggedInUserID;

		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
//		protected System.Web.UI.WebControls.TextBox txtBUSI_OWNERNAME;
//		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBUSI_OWNERNAME;
		protected System.Web.UI.WebControls.Label capCOMPANY_NAME;
		protected System.Web.UI.WebControls.TextBox txtCOMPANY_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOMPANY_NAME;
		protected System.Web.UI.WebControls.Label capCHK_MAIL_ADD1;
		protected System.Web.UI.WebControls.TextBox txtCHK_MAIL_ADD1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCHK_MAIL_ADD1;
		protected System.Web.UI.WebControls.Label capMAIL_1099_ADD1;
		protected System.Web.UI.WebControls.TextBox txtMAIL_1099_ADD1;
		protected System.Web.UI.WebControls.Label capCHK_MAIL_ADD2;
		protected System.Web.UI.WebControls.TextBox txtCHK_MAIL_ADD2;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCHK_MAIL_ADD2;
		protected System.Web.UI.WebControls.Label capMAIL_1099_ADD2;
		protected System.Web.UI.WebControls.TextBox txtMAIL_1099_ADD2;
		protected System.Web.UI.WebControls.Label capCHK_MAIL_CITY;
		protected System.Web.UI.WebControls.TextBox txtCHK_MAIL_CITY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCHK_MAIL_CITY;
		protected System.Web.UI.WebControls.Label capMAIL_1099_CITY;
		protected System.Web.UI.WebControls.TextBox txtMAIL_1099_CITY;
		protected System.Web.UI.WebControls.Label capCHK_MAIL_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCHK_MAIL_STATE;
		protected System.Web.UI.WebControls.Label capMAIL_1099_STATE;
		protected System.Web.UI.WebControls.Label capMAIL_1099_NAME;

		protected System.Web.UI.WebControls.Label capACCOUNT_ISVERIFIED;
		protected System.Web.UI.WebControls.Label lblACCOUNT_ISVERIFIED;
		protected System.Web.UI.WebControls.Label capACCOUNT_VERIFIED_DATE;
		protected System.Web.UI.WebControls.Label lblACCOUNT_VERIFIED_DATE;
		
		protected System.Web.UI.WebControls.Label capCOUNTRY;
		protected System.Web.UI.WebControls.TextBox txtCOUNTRY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOUNTRY;
		protected System.Web.UI.WebControls.TextBox txtMAIL_1099_ZIP;
		protected System.Web.UI.WebControls.TextBox txtMAIL_1099_NAME;
		protected System.Web.UI.WebControls.TextBox txtMAIL_1099_COUNTRY;
		protected System.Web.UI.WebControls.TextBox txtACCOUNT_VERIFIED_DATE;
		protected System.Web.UI.WebControls.DropDownList cmbCHKCOUNTRY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCHKCOUNTRY;
		protected System.Web.UI.WebControls.Label capMAIL_1099_COUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbMAIL_1099_COUNTRY;
		protected System.Web.UI.WebControls.Label capCHK_MAIL_ZIP;
		protected System.Web.UI.WebControls.TextBox txtCHK_MAIL_ZIP;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCHK_MAIL_ZIP;
		protected System.Web.UI.WebControls.Label capMAIL_1099_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revCHK_MAIL_ZIP;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMAIL_1099_ZIP;
		protected System.Web.UI.WebControls.DropDownList cmbCHK_MAIL_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbMAIL_1099_STATE;
		protected System.Web.UI.WebControls.Label capCHKCOUNTRY;
		protected System.Web.UI.WebControls.Label capPROCESS_1099_OPT;
		protected System.Web.UI.WebControls.DropDownList cmbPROCESS_1099_OPT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPROCESS_1099_OPT;
		protected System.Web.UI.WebControls.Label capW9_FORM;
		protected System.Web.UI.WebControls.DropDownList cmbW9_FORM;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIL_1099_ADD1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIL_1099_ADD2;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIL_1099_CITY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIL_1099_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIL_1099_COUNTRY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIL_1099_ZIP;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIL_1099_NAME;
		protected System.Web.UI.WebControls.CheckBox chkCopyData;
		protected System.Web.UI.WebControls.Image imgCHK_MAIL_ZIP;
		protected System.Web.UI.WebControls.Image imgMAIL_1099_ZIP;
		protected System.Web.UI.WebControls.Image imgVENDOR_ZIP;

        protected System.Web.UI.WebControls.HyperLink hlkCHK_MAIL_ZIPLookUp;
        protected System.Web.UI.WebControls.HyperLink hlkMAIL_1099_ZIPLookUp;
        protected System.Web.UI.WebControls.HyperLink hlkVENDOR_ZIPLookUp;
		protected System.Web.UI.WebControls.Label capBANK_NAME;
		protected System.Web.UI.WebControls.TextBox txtBANK_NAME;
		protected System.Web.UI.WebControls.Label lbl_BANK_NAME;
		protected System.Web.UI.WebControls.RegularExpressionValidator revBANK_NAME;
		protected System.Web.UI.WebControls.Label capBANK_BRANCH;
		protected System.Web.UI.WebControls.TextBox txtBANK_BRANCH;
		protected System.Web.UI.WebControls.Label lbl_BANK_BRANCH;
		clsVendor  objVendor ;

		protected System.Web.UI.WebControls.Label lblREASON;
		protected System.Web.UI.WebControls.Label capREASON;
		protected System.Web.UI.WebControls.RegularExpressionValidator revROUTING_NUMBER;
		protected System.Web.UI.WebControls.CustomValidator csvVERIFY_ROUTING_NUMBER_LENGHT;
		protected System.Web.UI.WebControls.CustomValidator csvVERIFY_ROUTING_NUMBER;
		protected System.Web.UI.WebControls.CustomValidator csvROUTING_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvROUTING_NUMBER;
		protected System.Web.UI.WebControls.Label lbl_ROUTING_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtROUTING_NUMBER;
		protected System.Web.UI.WebControls.Label capROUTING_NUMBER;
		protected System.Web.UI.WebControls.CustomValidator csvBANK_ACCOUNT_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBANK_ACCOUNT_NUMBER;
		protected System.Web.UI.WebControls.Label lbl_BANK_ACCOUNT_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtBANK_ACCOUNT_NUMBER;
		protected System.Web.UI.WebControls.Label capBANK_ACCOUNT_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvALLOWS_EFT;
		protected System.Web.UI.WebControls.DropDownList cmbALLOWS_EFT;
		protected System.Web.UI.WebControls.Label capALLOWS_EFT;
		protected System.Web.UI.WebControls.RadioButton rdbACC_CASH_ACC_TYPEO;
		protected System.Web.UI.WebControls.Label capACCOUNT_TYPE;
		protected System.Web.UI.WebControls.RadioButton rdbACC_CASH_ACC_TYPET;
		protected System.Web.UI.WebControls.Label lblACC_CASH_ACC_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBANK_NAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBANK_BRANCH;
		protected System.Web.UI.WebControls.CheckBox chkCopyAdd;
		protected System.Web.UI.WebControls.RegularExpressionValidator revBANK_ACCOUNT_NUMBER;
		protected System.Web.UI.WebControls.CheckBox chkREVERIFIED_AC;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCancel;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidEdit;
		protected System.Web.UI.WebControls.Label capBANK_ACCOUNT_NUMBER_HID;
		protected System.Web.UI.WebControls.Label capVENDOR_FEDERAL_NUM_HID;
		protected System.Web.UI.WebControls.Label capROUTING_NUMBER_HID;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidBANK_ACCOUNT_NUMBER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVENDOR_FEDERAL_NUM;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidROUTING_NUMBER;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCHK_MAIL_STATE_2;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCHK_MAIL_STATE_3;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCHK_MAIL_STATE_4;
        //added By Chetna
        protected System.Web.UI.WebControls.Label capSUSEP_NUM;
        protected System.Web.UI.WebControls.TextBox txtSUSEP_NUM;
        protected System.Web.UI.WebControls.CompareValidator cpvDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.CompareValidator cpvREG_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.CompareValidator cpv2REG_ID_ISSUE_DATE_FUTURE;
		#endregion

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			
			rfvVENDOR_CODE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"81");
			rfvCHK_MAIL_ZIP.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"37");
			rfvCOMPANY_NAME.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"484"); 
			rfvCHK_MAIL_CITY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"56");
			rfvCHK_MAIL_STATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"35");
			rfvCHKCOUNTRY.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"33");
			rfvMAIL_1099_CITY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"56");
			rfvMAIL_1099_STATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"35");
			rfvMAIL_1099_COUNTRY.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"33");
			rfvMAIL_1099_ZIP.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"37");
			rfvVENDOR_FEDERAL_NUM.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"82");
			csvVENDOR_NOTE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"445");
            revCHK_MAIL_ZIP.ValidationExpression     =    aRegExpZipBrazil;// aRegExpZip;
			revMAIL_1099_ZIP.ValidationExpression	=aRegExpZipBrazil;//	aRegExpZip;
            revVENDOR_ZIP.ValidationExpression = aRegExpZipBrazil;//	aRegExpZip;
          //  revVENDOR_PHONE.ValidationExpression	=	aRegExpPhone;
			revVENDOR_EXT.ValidationExpression		=	aRegExpExtn;
			//revVENDOR_FAX.ValidationExpression		=	aRegExpFax;
		//	revVENDOR_MOBILE.ValidationExpression	=	aRegExpPhone;
			revVENDOR_EMAIL.ValidationExpression	=	aRegExpEmail;
			revVENDOR_FEDERAL_NUM.ValidationExpression	=	aRegExpFederalID;
			revVENDOR_ACC_NUMBER.ValidationExpression	=	aRegExpClientName;
			revVENDOR_ZIP.ErrorMessage				=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1181");
            revCHK_MAIL_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1181");
            revMAIL_1099_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1181");
		//	revVENDOR_PHONE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
			revVENDOR_EXT.ErrorMessage				=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("25");
			//revVENDOR_FAX.ErrorMessage				=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("15");
			//revVENDOR_MOBILE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("16");
			revVENDOR_EMAIL.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("124");
			revVENDOR_FEDERAL_NUM.ErrorMessage		=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("434");
			revVENDOR_ACC_NUMBER.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"85");
            revBANK_ACCOUNT_NUMBER.ValidationExpression = aRegExpBankAccountNumber;
			revBANK_ACCOUNT_NUMBER.ErrorMessage		= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("903");
            rfvCHK_MAIL_ADD1.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "87");   //sneha
            rfvPROCESS_1099_OPT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "88"); //sneha
            rfvMAIL_1099_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "89"); //sneha
            rfvMAIL_1099_ADD1.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "90"); //sneha
            rfvALLOWS_EFT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "91"); //sneha
            rfvBANK_ACCOUNT_NUMBER.ErrorMessage  =Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"92"); //sneha
            revCPF_CNPJ.ValidationExpression = aRegExpCpf_Cnpj;
            revREGIONAL_ID_ISSUE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1380");
            revCPF_CNPJ.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1081");
            revDATE_OF_BIRTH.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1380");
            revDATE_OF_BIRTH.ValidationExpression = aRegExpDate;
            revREGIONAL_ID_ISSUE_DATE.ValidationExpression = aRegExpDate;
            csvBANK_ACCOUNT_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "93");
            rfvROUTING_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "94");
            csvROUTING_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "95");
            csvVERIFY_ROUTING_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "96");
            csvVERIFY_ROUTING_NUMBER_LENGHT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "97");
            revROUTING_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "98");
            cpvDATE_OF_BIRTH.ErrorMessage = ClsMessages.FetchGeneralMessage("481");
            cpvREG_ID_ISSUE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1402");
            cpv2REG_ID_ISSUE_DATE_FUTURE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1403");

            if (GetLanguageID() == "1")
            {
                revVENDOR_PHONE.ValidationExpression = aRegExpPhone;
                revVENDOR_FAX.ValidationExpression = aRegExpFax;
                revVENDOR_MOBILE.ValidationExpression = aRegExpPhone;
                revVENDOR_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
                revVENDOR_FAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("15");
                revVENDOR_MOBILE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("16");
            }

            else
            {
                revVENDOR_PHONE.ValidationExpression = aRegExpAgencyPhone;
                revVENDOR_FAX.ValidationExpression = aRegExpAgencyPhone;
                revVENDOR_MOBILE.ValidationExpression = aRegExpAgencyPhone;
                revVENDOR_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1913");
                revVENDOR_FAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1921");
                revVENDOR_MOBILE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1922");
            }

		}
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
            Ajax.Utility.RegisterTypeForAjax(typeof(AddVendor)); 

			revROUTING_NUMBER.ValidationExpression =aRegExpDoublePositiveNonZeroStartWithZero;
			txtVENDOR_PHONE.Attributes.Add("onBlur","javascript:DisableExt('txtVENDOR_PHONE','txtVENDOR_EXT');FormatPhone();");
			chkCopyData.Attributes.Add("onClick","javascript:CopyData('txtCHK_MAIL_ADD1','txtMAIL_1099_ADD1');CopyData('txtCHK_MAIL_ADD2','txtMAIL_1099_ADD2');CopyData('txtCHK_MAIL_CITY','txtMAIL_1099_CITY');CopyData('txtCHK_MAIL_ZIP','txtMAIL_1099_ZIP');CopyData('cmbCHK_MAIL_STATE','cmbMAIL_1099_STATE');");        
			chkCopyAdd.Attributes.Add("onClick","javascript:CopyAdd('txtCHK_MAIL_ADD1','txtVENDOR_ADD1');CopyAdd('txtCHK_MAIL_ADD2','txtVENDOR_ADD2');CopyAdd('txtCHK_MAIL_CITY','txtVENDOR_CITY');CopyAdd('txtCHK_MAIL_ZIP','txtVENDOR_ZIP');CopyAdd('cmbCHK_MAIL_STATE','cmbVENDOR_STATE');");        
			btnSave.Attributes.Add("onClick","javascript: setEFTFields();");
            hlkREG_ID_ISSUE_DATE.Attributes.Add("OnClick", "fPopCalendar(txtREG_ID_ISSUE_DATE,txtREG_ID_ISSUE_DATE)"); //Javascript Implementation for Calender				
            hlkDATE_OF_BIRTH.Attributes.Add("OnClick", "fPopCalendar(txtDATE_OF_BIRTH,txtDATE_OF_BIRTH)");
			intLoggedInUserID	= int.Parse(base.GetUserId());
			base.ScreenId="32_0";
			lblMessage.Visible = false;
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333"); //sneha
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass					=	CmsButtonType.Write;
			btnReset.PermissionString				=	gstrSecurityXML;
	
			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;
	
			btnSave.CmsButtonClass					=	CmsButtonType.Write;
			btnSave.PermissionString				=	gstrSecurityXML;
	
			btnDelete.CmsButtonClass				=	Cms.CmsWeb.Controls.CmsButtonType.Delete;
			btnDelete.PermissionString				=	gstrSecurityXML;
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            Page.DataBind();
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddVendor",System.Reflection.Assembly.GetExecutingAssembly());
			
			if(hidVENDOR_ID.Value != null && hidVENDOR_ID.Value.ToString().Length > 0)
				strPageMode = "Edit";
			else
				strPageMode = "Add";

			if(!Page.IsPostBack)
			{
				imgMAIL_1099_ZIP.Attributes.Add("style","cursor:hand");

                base.VerifyAddress(hlkMAIL_1099_ZIPLookUp, txtMAIL_1099_ADD1, txtMAIL_1099_ADD2, txtMAIL_1099_CITY, cmbMAIL_1099_STATE, txtMAIL_1099_ZIP);

				imgVENDOR_ZIP.Attributes.Add("style","cursor:hand");

                base.VerifyAddress(hlkVENDOR_ZIPLookUp, txtVENDOR_ADD1, txtVENDOR_ADD2
					, txtVENDOR_CITY, cmbVENDOR_STATE, txtVENDOR_ZIP);

				imgCHK_MAIL_ZIP.Attributes.Add("style","cursor:hand");

                base.VerifyAddress(hlkCHK_MAIL_ZIPLookUp, txtCHK_MAIL_ADD1, txtCHK_MAIL_ADD2
                    , txtCHK_MAIL_CITY, cmbCHK_MAIL_STATE,  txtCHK_MAIL_ZIP);
				
				SetErrorMessages();
				SetCaptions();
                hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCon");

             
				
				#region "Loading singleton"
				DataTable dt = Cms.CmsWeb.ClsFetcher.Country;

                cmbVENDOR_COUNTRY.DataSource		= dt;
				cmbVENDOR_COUNTRY.DataTextField	= "Country_Name";
				cmbVENDOR_COUNTRY.DataValueField	= "Country_Id";
				cmbVENDOR_COUNTRY.DataBind();
                cmbVENDOR_COUNTRY.Items.Insert(0, "");

				cmbCHKCOUNTRY.DataSource =dt;
				cmbCHKCOUNTRY.DataTextField ="Country_name";
				cmbCHKCOUNTRY.DataValueField ="Country_id";
				cmbCHKCOUNTRY.DataBind();
                cmbCHKCOUNTRY.Items.Insert(0, "");

				cmbMAIL_1099_COUNTRY.DataSource =dt;
				cmbMAIL_1099_COUNTRY.DataTextField ="Country_name";
				cmbMAIL_1099_COUNTRY.DataValueField ="Country_id";
				cmbMAIL_1099_COUNTRY.DataBind();
                cmbMAIL_1099_COUNTRY.Items.Insert(0, "");


                //Code added by Shikha for itrack 1129

                ClsStates objStates = new ClsStates();
                DataTable dtStates = objStates.GetStatesForCountry(Convert.ToInt32(dt.Rows[0]["Country_Id"].ToString()));
                if (dtStates != null && dtStates.Rows.Count > 0)
                {
                    cmbVENDOR_STATE.DataSource = dtStates;
                    cmbVENDOR_STATE.DataTextField = STATE_NAME;
                    cmbVENDOR_STATE.DataValueField = STATE_ID;
                    cmbVENDOR_STATE.DataBind();
                    cmbVENDOR_STATE.Items.Insert(0, "");
                }

                if (dtStates != null && dtStates.Rows.Count > 0)
                {
                    cmbCHK_MAIL_STATE.DataSource = dtStates;
                    cmbCHK_MAIL_STATE.DataTextField = STATE_NAME;
                    cmbCHK_MAIL_STATE.DataValueField = STATE_ID;
                    cmbCHK_MAIL_STATE.DataBind();
                    cmbCHK_MAIL_STATE.Items.Insert(0, "");
                }

                if (dtStates != null && dtStates.Rows.Count > 0)
                {
                    cmbMAIL_1099_STATE.DataSource = dtStates;
                    cmbMAIL_1099_STATE.DataTextField = STATE_NAME;
                    cmbMAIL_1099_STATE.DataValueField = STATE_ID;
                    cmbMAIL_1099_STATE.DataBind();
                    cmbMAIL_1099_STATE.Items.Insert(0, "");
                }


                //dt = Cms.CmsWeb.ClsFetcher.State;
                //cmbVENDOR_STATE.DataSource		= dt;
                //cmbVENDOR_STATE.DataTextField	= "State_Name";
                //cmbVENDOR_STATE.DataValueField	= "State_Id";
                //cmbVENDOR_STATE.DataBind();

                //cmbCHK_MAIL_STATE.DataSource =dt;
                //cmbCHK_MAIL_STATE.DataTextField ="State_Name";
                //cmbCHK_MAIL_STATE.DataValueField ="State_Id";
                //cmbCHK_MAIL_STATE.DataBind();

                //cmbMAIL_1099_STATE.DataSource =dt;
                //cmbMAIL_1099_STATE.DataTextField ="State_Name";
                //cmbMAIL_1099_STATE.DataValueField ="State_Id";
                //cmbMAIL_1099_STATE.DataBind();

  				#endregion//Loading singleton

				if(Request.QueryString["VENDOR_ID"]!=null && Request.QueryString["VENDOR_ID"].ToString().Length>0)
				{
					SetXml(Request.QueryString["VENDOR_ID"]);
					if(hidOldData.Value.Length > 0)
						hidACCOUNT_TYPE.Value = GetEFTInfo(hidOldData.Value);
					strPageMode = "Edit";

					hidREVERIFIED_AC.Value = ClsCommon.FetchValueFromXML("REVERIFIED_AC",hidOldData.Value);
					
				}
				else if(hidVENDOR_ID.Value != null && hidVENDOR_ID.Value.ToString().Length > 0)
				{
					SetXml(hidVENDOR_ID.Value.ToString());
					if(hidOldData.Value.Length > 0)
						hidACCOUNT_TYPE.Value = GetEFTInfo(hidOldData.Value);
					strPageMode = "Edit";

					hidREVERIFIED_AC.Value = ClsCommon.FetchValueFromXML("REVERIFIED_AC",hidOldData.Value);
				   
				}
				else
					strPageMode = "Add";
				
				cmbPROCESS_1099_OPT.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("ESPPO");
				cmbPROCESS_1099_OPT.DataTextField	= "LookupDesc";
				cmbPROCESS_1099_OPT.DataValueField	= "LookupID";
				cmbPROCESS_1099_OPT.DataBind();
				cmbPROCESS_1099_OPT.Items.Insert(0,""); 
				Cms.BusinessLayer.BlCommon.ClsCommon.RemoveOptionFromDropdownByValue(cmbPROCESS_1099_OPT,"14123");
				
				cmbW9_FORM.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("W9FORM");
				cmbW9_FORM.DataTextField	= "LookupDesc";
				cmbW9_FORM.DataValueField	= "LookupID";
				cmbW9_FORM.DataBind();
				cmbW9_FORM.Items.Insert(0,""); 
				
				cmbALLOWS_EFT.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
				cmbALLOWS_EFT.DataTextField="LookupDesc";
				cmbALLOWS_EFT.DataValueField="LookupID";
				cmbALLOWS_EFT.DataBind();

                cmbVENDOR_SALUTATION.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("%SALU");
                cmbVENDOR_SALUTATION.DataTextField = "LookupDesc";
                cmbVENDOR_SALUTATION.DataValueField = "LookupID";
                cmbVENDOR_SALUTATION.DataBind();

                DataTable dp = Cms.BusinessLayer.BlApplication.ClsLocation.SELECTACTIVITYTYPE();
                cmbACTIVITY.DataSource = dp;
                cmbACTIVITY.DataTextField = "ACTIVITY_DESC";
                cmbACTIVITY.DataValueField = "ACTIVITY_ID";
                cmbACTIVITY.DataBind();
                cmbACTIVITY.Items.Insert(0, "");
                cmbACTIVITY.SelectedIndex = 0;
                // Added by Amit Mishra for Screen Customization on 23rd sep 2011
                 if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "cmsweb/support/PageXML/" + GetSystemId(), "AddVendor.xml"))
                    setPageControls(Page, Request.PhysicalApplicationPath + "cmsweb/support/PageXml/" + GetSystemId() + "/AddVendor.xml");
			}
		}//end pageload
		private string GetEFTInfo(string oldxml)
		{
			DataSet dsEFTInfo = new DataSet();
			try
			{
				//Create a StringReader to hold the XML
				StringReader xmlStrReader = new StringReader(oldxml);

				//Stuff XML into a DataSet
				dsEFTInfo.ReadXml(xmlStrReader);

				if(dsEFTInfo.Tables.Count > 0 && dsEFTInfo.Tables[0].Rows.Count >0)
					return dsEFTInfo.Tables[0].Rows[0]["ACCOUNT_TYPE"].ToString();
				else
					return "";
			}
			catch//(Exception ex)
			{
				return "";
			}
			
		}
        private void FillStates(DropDownList cmbSTATE, int COUNTRY_ID)
        {
            ClsStates objStates = new ClsStates();
            DataTable dtStates = objStates.GetStatesForCountry(COUNTRY_ID);
            if (dtStates != null && dtStates.Rows.Count > 0)
            {
                cmbSTATE.DataSource = dtStates;
                cmbSTATE.DataTextField = STATE_NAME;
                cmbSTATE.DataValueField = STATE_ID;
                cmbSTATE.DataBind();
                cmbSTATE.Items.Insert(0, "");
            }

        }

		private void SetXml(string strVendorId)
		{
           hidOldData.Value = clsVendor.GetXmlForPageControls(strVendorId,int.Parse(GetLanguageID()));

			#region Decrypting
			if(hidOldData.Value.IndexOf("NewDataSet") >= 0)
			{
				XmlDocument objxml = new XmlDocument();

				objxml.LoadXml(hidOldData.Value);

				XmlNode node = objxml.SelectSingleNode("NewDataSet");
				foreach(XmlNode nodes in node.SelectNodes("Table"))
				{
					XmlNode noder1 = nodes.SelectSingleNode("VENDOR_FEDERAL_NUM");
					//noder1.InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder1.InnerText);
					hidVENDOR_FEDERAL_NUM.Value = noder1.InnerText;
					string strVENDOR_FEDERAL_NUM = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder1.InnerText);
					if(strVENDOR_FEDERAL_NUM.Trim() != "")//Done for Itrack Issue 6063 on 7 July 09
					{
						string strvaln = "";
						for(int len=0; len < strVENDOR_FEDERAL_NUM.Length-4; len++)
							strvaln += "x";

						strvaln += strVENDOR_FEDERAL_NUM.Substring(strvaln.Length, strVENDOR_FEDERAL_NUM.Length - strvaln.Length);
						capVENDOR_FEDERAL_NUM_HID.Text = strvaln;
					}
					else
						capVENDOR_FEDERAL_NUM_HID.Text = "";
						
					XmlNode noder2 = nodes.SelectSingleNode("BANK_ACCOUNT_NUMBER");
					//noder2.InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder2.InnerText);
					hidBANK_ACCOUNT_NUMBER.Value = noder2.InnerText;
					string strBANK_ACCOUNT_NUMBER = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder2.InnerText);
					if(strBANK_ACCOUNT_NUMBER.Trim() != "")//Done for Itrack Issue 6063 on 7 July 09
					{
						string strvaln = "";
						for(int len=0; len < strBANK_ACCOUNT_NUMBER.Length-4; len++)
							strvaln += "x";

						strvaln += strBANK_ACCOUNT_NUMBER.Substring(strvaln.Length, strBANK_ACCOUNT_NUMBER.Length - strvaln.Length);
						capBANK_ACCOUNT_NUMBER_HID.Text = strvaln;
					}
					else
						capBANK_ACCOUNT_NUMBER_HID.Text = "";

					XmlNode noder3 = nodes.SelectSingleNode("ROUTING_NUMBER");
					//noder3.InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder3.InnerText);
					hidROUTING_NUMBER.Value = noder3.InnerText;
					string strROUTING_NUMBER = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder3.InnerText);
					if(strROUTING_NUMBER.Trim() != "")//Done for Itrack Issue 6063 on 7 July 09
					{
						string strvaln = "";
						for(int len=0; len < strROUTING_NUMBER.Length-4; len++)
							strvaln += "x";

						strvaln += strROUTING_NUMBER.Substring(strvaln.Length, strROUTING_NUMBER.Length - strvaln.Length);
						capROUTING_NUMBER_HID.Text = strvaln;
					}
					else
						capROUTING_NUMBER_HID.Text = "";
				}
                if (ClsCommon.FetchValueFromXML("CHKCOUNTRY", hidOldData.Value) != "")
                    FillStates(cmbCHK_MAIL_STATE, int.Parse(ClsCommon.FetchValueFromXML("CHKCOUNTRY", hidOldData.Value)));
                if (ClsCommon.FetchValueFromXML("CHK_MAIL_STATE", hidOldData.Value) != "")
                {
                    cmbCHK_MAIL_STATE.SelectedValue = ClsCommon.FetchValueFromXML("CHK_MAIL_STATE", hidOldData.Value);
                    rfvCHK_MAIL_STATE.Enabled = false;
                }
                if (ClsCommon.FetchValueFromXML("MAIL_1099_COUNTRY", hidOldData.Value) != "")
                    FillStates(cmbMAIL_1099_STATE, int.Parse(ClsCommon.FetchValueFromXML("MAIL_1099_COUNTRY", hidOldData.Value)));
                if (ClsCommon.FetchValueFromXML("MAIL_1099_STATE", hidOldData.Value) != "")
                {
                    cmbMAIL_1099_STATE.SelectedValue = ClsCommon.FetchValueFromXML("MAIL_1099_STATE", hidOldData.Value);
                   
                }

                if (ClsCommon.FetchValueFromXML("VENDOR_COUNTRY", hidOldData.Value) != "")
                    FillStates(cmbVENDOR_STATE, int.Parse(ClsCommon.FetchValueFromXML("VENDOR_COUNTRY", hidOldData.Value)));
                if (ClsCommon.FetchValueFromXML("VENDOR_STATE", hidOldData.Value) != "")
                {
                    cmbVENDOR_STATE.SelectedValue = ClsCommon.FetchValueFromXML("VENDOR_STATE", hidOldData.Value);

                }

				//hidOldData.Value = objxml.OuterXml;
				objxml = null;
			}
			#endregion Decrypting

			lblACCOUNT_ISVERIFIED.Text = ClsCommon.FetchValueFromXML("ACCOUNT_ISVERIFIED",hidOldData.Value);
			lblACCOUNT_VERIFIED_DATE.Text = ClsCommon.FetchValueFromXML("ACCOUNT_VERIFIED_DATE",hidOldData.Value);
		}

		#region Method code to do form's processing
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

		/// <summary>
		/// Fetch form's value and stores into variables.
		/// </summary>
		private ClsVendorInfo getFormValue()
		{
			ClsVendorInfo objVendorInfo = new ClsVendorInfo();
            objVendorInfo.VENDOR_CODE=txtVENDOR_CODE.Text;
			objVendorInfo.VENDOR_FNAME=txtVENDOR_FNAME.Text;
			objVendorInfo.VENDOR_LNAME=txtVENDOR_LNAME.Text;
			objVendorInfo.VENDOR_ADD1=txtVENDOR_ADD1.Text;
			objVendorInfo.VENDOR_ADD2=txtVENDOR_ADD2.Text;
			objVendorInfo.VENDOR_CITY=txtVENDOR_CITY.Text;
			objVendorInfo.VENDOR_COUNTRY=cmbVENDOR_COUNTRY.SelectedValue;
            if (cmbVENDOR_STATE.SelectedValue != "")
                objVendorInfo.VENDOR_STATE = cmbVENDOR_STATE.SelectedValue;
            else
                objVendorInfo.VENDOR_STATE = hidCHK_MAIL_STATE_4.Value;
			//objVendorInfo.VENDOR_STATE=cmbVENDOR_STATE.SelectedValue;
			objVendorInfo.VENDOR_ZIP=txtVENDOR_ZIP.Text;
			objVendorInfo.VENDOR_PHONE=txtVENDOR_PHONE.Text;
			objVendorInfo.VENDOR_EXT=txtVENDOR_EXT.Text;
			objVendorInfo.VENDOR_FAX=txtVENDOR_FAX.Text;
			objVendorInfo.VENDOR_MOBILE=txtVENDOR_MOBILE.Text;
			objVendorInfo.VENDOR_EMAIL=txtVENDOR_EMAIL.Text;
			objVendorInfo.VENDOR_SALUTATION=cmbVENDOR_SALUTATION.SelectedValue;
            if (cmbACTIVITY.SelectedItem != null && cmbACTIVITY.SelectedItem.Value != "")
                objVendorInfo.ACTIVITY = int.Parse(cmbACTIVITY.SelectedItem.Value);
            if (txtDATE_OF_BIRTH.Text != "")
            {
                objVendorInfo.DATE_OF_BIRTH = ConvertToDate(txtDATE_OF_BIRTH.Text);
            }
            if (txtREG_ID_ISSUE_DATE.Text != "")
            {
                objVendorInfo.REG_ID_ISSUE_DATE = ConvertToDate(txtREG_ID_ISSUE_DATE.Text);//REGIONAL_ID_ISSUE_DATE}
            }
            objVendorInfo.REG_ID_ISSUE = txtREG_ID_ISSUE.Text.ToString();
            objVendorInfo.REGIONAL_IDENTIFICATION = txtREGIONAL_IDENTIFICATION.Text.ToString();
            objVendorInfo.CPF = txtCPF.Text.ToString();
			if(txtVENDOR_FEDERAL_NUM.Text != "")
			{
				objVendorInfo.VENDOR_FEDERAL_NUM=BusinessLayer.BlCommon.ClsCommon.EncryptString(txtVENDOR_FEDERAL_NUM.Text);
				txtVENDOR_FEDERAL_NUM.Text = "";
			}
			else
				objVendorInfo.VENDOR_FEDERAL_NUM=hidVENDOR_FEDERAL_NUM.Value;

			objVendorInfo.VENDOR_NOTE=	txtVENDOR_NOTE.Text;
			objVendorInfo.VENDOR_ACC_NUMBER=txtVENDOR_ACC_NUMBER.Text;
			objVendorInfo.IS_ACTIVE=  hidIS_ACTIVE.Value;
			objVendorInfo.CREATED_BY=  int.Parse(GetUserId()) ;
			//added By Pravesh
//			objVendorInfo.BUSI_OWNERNAME =txtBUSI_OWNERNAME.Text;
			objVendorInfo.COMPANY_NAME =txtCOMPANY_NAME.Text;
			objVendorInfo.CHK_MAIL_ADD1=txtCHK_MAIL_ADD1.Text;
			objVendorInfo.CHK_MAIL_ADD2=txtCHK_MAIL_ADD2.Text;
			objVendorInfo.CHK_MAIL_CITY=txtCHK_MAIL_CITY.Text;
            if (cmbCHK_MAIL_STATE.SelectedValue != "")
                objVendorInfo.CHK_MAIL_STATE = cmbCHK_MAIL_STATE.SelectedValue;
            else
            objVendorInfo.CHK_MAIL_STATE = hidCHK_MAIL_STATE_2.Value;
			objVendorInfo.CHKCOUNTRY=cmbCHKCOUNTRY.SelectedValue;
			objVendorInfo.CHK_MAIL_ZIP=txtCHK_MAIL_ZIP.Text;
			objVendorInfo.MAIL_1099_ADD1=txtMAIL_1099_ADD1.Text;
			objVendorInfo.MAIL_1099_ADD2=txtMAIL_1099_ADD2.Text;
			objVendorInfo.MAIL_1099_CITY=txtMAIL_1099_CITY.Text;
            if (cmbMAIL_1099_STATE.SelectedValue != "")
                objVendorInfo.MAIL_1099_STATE = cmbMAIL_1099_STATE.SelectedValue;
            else
                objVendorInfo.MAIL_1099_STATE = hidCHK_MAIL_STATE_3.Value;
			//objVendorInfo.MAIL_1099_STATE=cmbMAIL_1099_STATE.SelectedValue;
			objVendorInfo.MAIL_1099_COUNTRY=cmbMAIL_1099_COUNTRY.SelectedValue;
			objVendorInfo.MAIL_1099_ZIP=txtMAIL_1099_ZIP.Text;
			objVendorInfo.MAIL_1099_NAME=txtMAIL_1099_NAME.Text;
			objVendorInfo.PROCESS_1099_OPT =cmbPROCESS_1099_OPT.SelectedValue;
			objVendorInfo.W9_FORM =cmbW9_FORM.SelectedValue;

            //Added By Chetna
            objVendorInfo.SUSEP_NUM = txtSUSEP_NUM.Text;

			objVendorInfo.ALLOWS_EFT=Convert.ToInt32(cmbALLOWS_EFT.SelectedValue);	
			if(objVendorInfo.ALLOWS_EFT==int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.NO).ToString()))
			{
				objVendorInfo.ACCOUNT_TYPE		 = null;
				objVendorInfo.BANK_NAME			 = "";
				objVendorInfo.BANK_BRANCH		 = "";
				objVendorInfo.DFI_ACCOUNT_NUMBER = "";
				objVendorInfo.ROUTING_NUMBER	 = "";
			}
			else
			{
				if(rdbACC_CASH_ACC_TYPEO.Checked)//Checking
					objVendorInfo.ACCOUNT_TYPE = Convert.ToString((int)EFTCodes.CheckingAccount);
				else if(rdbACC_CASH_ACC_TYPET.Checked)//Saving
					objVendorInfo.ACCOUNT_TYPE = Convert.ToString((int)EFTCodes.SavingAccount);

				objVendorInfo.BANK_NAME			= txtBANK_NAME.Text;
				objVendorInfo.BANK_BRANCH		= txtBANK_BRANCH.Text;
				if(txtBANK_ACCOUNT_NUMBER.Text != "")
				{
					objVendorInfo.DFI_ACCOUNT_NUMBER= BusinessLayer.BlCommon.ClsCommon.EncryptString(txtBANK_ACCOUNT_NUMBER.Text);
					txtBANK_ACCOUNT_NUMBER.Text = "";
				}
				else
					objVendorInfo.DFI_ACCOUNT_NUMBER= hidBANK_ACCOUNT_NUMBER.Value;

				if(txtROUTING_NUMBER.Text != "")
				{
					objVendorInfo.ROUTING_NUMBER	= BusinessLayer.BlCommon.ClsCommon.EncryptString(txtROUTING_NUMBER.Text);
					txtROUTING_NUMBER.Text = "";
				}
				else
					objVendorInfo.ROUTING_NUMBER	= hidROUTING_NUMBER.Value;

			}

			//Reverify Model Objects
			if(chkREVERIFIED_AC.Checked == true)
				objVendorInfo.REVERIFIED_AC = int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.YES).ToString()); //YES
			else
				objVendorInfo.REVERIFIED_AC = int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.NO).ToString()); //NO

			//Added By Raghav For Special Handling
			if(chkREQ_SPECIAL_HANDLING.Checked == true)
				objVendorInfo.REQ_SPECIAL_HANDLING = int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.YES).ToString()); //YES
			else
				objVendorInfo.REQ_SPECIAL_HANDLING = int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.NO).ToString()); //NO


			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidVENDOR_ID.Value;
			oldXML  = hidOldData.Value;
			return objVendorInfo;
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
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
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
			int intReturnValue;
			try
			{
				ClsVendorInfo objVendorInfo = getFormValue();
				
			
				objVendor = new  clsVendor(true);
				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					//Mapping feild and Lebel to maintain the transction log into the database.
					objVendorInfo.TransactLabel	=	MapTransactionLabel(objResourceMgr,this);

					//Setting properties which do not corresponds to page controls.
					objVendorInfo.IS_ACTIVE = "Y";
					objVendorInfo.MODIFIED_BY = objVendorInfo.CREATED_BY;
					objVendorInfo.CREATED_DATETIME = objVendorInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					intReturnValue=objVendor.add(objVendorInfo);
					if(intReturnValue>0)
					{
						//hidVENDOR_ID.Value = objVendor.VENDOR_ID.ToString();
						hidVENDOR_ID.Value=intReturnValue.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						SetXml(hidVENDOR_ID.Value);
					}
					else if( objVendor.VENDOR_ID == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value			=		"2";
						btnActivateDeactivate.Enabled = false;
					}
					else
					{
						lblMessage.Text			=	ClsMessages.FetchGeneralMessage("20");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{
					
					ClsVendorInfo objOldVendorInfo = new ClsVendorInfo();
					//Setting  the Old Page details(XML File containing old details) into the Model Object

					//Mapping feild and Lebel to maintain the transction log into the database.
					objVendorInfo.TransactLabel	=	MapTransactionLabel(objResourceMgr,this);

					base.PopulateModelObject(objOldVendorInfo,hidOldData.Value);
					
					//Setting values for which there are no corresponding UI control in the page.
                    objOldVendorInfo.VENDOR_ID	= objVendorInfo.VENDOR_ID	=	 int.Parse(hidVENDOR_ID.Value);
					objVendorInfo.MODIFIED_BY				=	int.Parse(GetUserId());
					objVendorInfo.LAST_UPDATED_DATETIME	=	DateTime.Now;

					intReturnValue				=	objVendor.Update(objVendorInfo,objOldVendorInfo);

					if(intReturnValue>0)
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"31");
						hidFormSaved.Value			=	"1";
						SetXml(hidVENDOR_ID.Value);
					}
					else if(intReturnValue == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.FetchGeneralMessage("20");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				}			
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+" - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				//ExceptionManager.Publish(ex);
			}
			finally
			{
//				if(objVendor!= null)
//					objVendor.Dispose();
			}
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
				stuTransactionInfo objStuTransactionInfo = new  stuTransactionInfo();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				
				objStuTransactionInfo.loggedInUserName = GetUserName();
				strRowId		=	hidVENDOR_ID.Value;
				clsVendor objVendor = new  clsVendor();
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "Vendor Deactivated Succesfully.";
					objVendor.TransactionInfoParams = objStuTransactionInfo;
					objVendor.ActivateDeactivate(strRowId,"N");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Vendor Activated Succesfully.";
					objVendor.TransactionInfoParams = objStuTransactionInfo;
					objVendor.ActivateDeactivate(strRowId,"Y");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}
				hidFormSaved.Value			=	"1";
				SetXml(hidVENDOR_ID.Value.ToString());
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
				if(objVendor!= null)
					objVendor.Dispose();
			}
		}
		#endregion

		private void SetCaptions()
		{
			capVENDOR_CODE.Text						=		objResourceMgr.GetString("txtVENDOR_CODE");
			capVENDOR_FNAME.Text					=		objResourceMgr.GetString("txtVENDOR_FNAME");
			capVENDOR_LNAME.Text					=		objResourceMgr.GetString("txtVENDOR_LNAME");
			capVENDOR_ADD1.Text						=		objResourceMgr.GetString("txtVENDOR_ADD1");
			capVENDOR_ADD2.Text						=		objResourceMgr.GetString("txtVENDOR_ADD2");
			capVENDOR_CITY.Text						=		objResourceMgr.GetString("txtVENDOR_CITY");
			capVENDOR_COUNTRY.Text					=		objResourceMgr.GetString("cmbVENDOR_COUNTRY");
			capVENDOR_STATE.Text					=		objResourceMgr.GetString("cmbVENDOR_STATE");
			capVENDOR_ZIP.Text						=		objResourceMgr.GetString("txtVENDOR_ZIP");
			capVENDOR_PHONE.Text					=		objResourceMgr.GetString("txtVENDOR_PHONE");
			capVENDOR_EXT.Text						=		objResourceMgr.GetString("txtVENDOR_EXT");
			capVENDOR_FAX.Text						=		objResourceMgr.GetString("txtVENDOR_FAX");
			capVENDOR_MOBILE.Text					=		objResourceMgr.GetString("txtVENDOR_MOBILE");
			capVENDOR_EMAIL.Text					=		objResourceMgr.GetString("txtVENDOR_EMAIL");
			capVENDOR_SALUTATION.Text				=		objResourceMgr.GetString("cmbVENDOR_SALUTATION");
			capVENDOR_FEDERAL_NUM.Text				=		objResourceMgr.GetString("txtVENDOR_FEDERAL_NUM");
			capVENDOR_NOTE.Text						=		objResourceMgr.GetString("txtVENDOR_NOTE");
			capVENDOR_ACC_NUMBER.Text				=		objResourceMgr.GetString("txtVENDOR_ACC_NUMBER");
            
			//by pravesh
//			capBUSI_OWNERNAME.Text				    =		objResourceMgr.GetString("txtBUSI_OWNERNAME");
			capCOMPANY_NAME.Text				=		objResourceMgr.GetString("txtCOMPANY_NAME");
			capCHK_MAIL_ADD1.Text				=		objResourceMgr.GetString("txtCHK_MAIL_ADD1");
			capCHK_MAIL_ADD2.Text				=		objResourceMgr.GetString("txtCHK_MAIL_ADD2");
			capCHK_MAIL_CITY.Text				=		objResourceMgr.GetString("txtCHK_MAIL_CITY");
			capCHK_MAIL_STATE.Text				=		objResourceMgr.GetString("txtCHK_MAIL_STATE");
			capCHKCOUNTRY.Text					=		objResourceMgr.GetString("txtCHKCOUNTRY");
			capCHK_MAIL_ZIP.Text				=		objResourceMgr.GetString("txtCHK_MAIL_ZIP");
            capCHKDETIL.Text                    =       objResourceMgr.GetString("capCHKDETIL"); //SNEHA
            capMAILDETL.Text                    =       objResourceMgr.GetString("capMAILDETL"); //SNEHA
            chkCopyData.Text                    =       objResourceMgr.GetString("chkCopyData"); //sneha
            capBANKINFO.Text                    =       objResourceMgr.GetString("capBANKINFO"); //sneha
            capCONTCTDETL.Text                  =       objResourceMgr.GetString("capCONTCTDETL"); //SNEHA
            chkCopyAdd.Text                     =       objResourceMgr.GetString("chkCopyAdd"); //SNEHA
            capACCOUNT_ISVERIFIED.Text          =       objResourceMgr.GetString("capACCOUNT_ISVERIFIED"); //sneha
            capACCOUNT_TYPE.Text                =       objResourceMgr.GetString("capACCOUNT_TYPE"); //sneha
            rdbACC_CASH_ACC_TYPEO.Text          =       objResourceMgr.GetString("rdbACC_CASH_ACC_TYPEO"); //sneha
            rdbACC_CASH_ACC_TYPET.Text          =       objResourceMgr.GetString("rdbACC_CASH_ACC_TYPET"); //sneha
            capACCOUNT_VERIFIED_DATE.Text       =       objResourceMgr.GetString("capACCOUNT_VERIFIED_DATE"); //sneha
            capCPF.Text = objResourceMgr.GetString("txtCPF");
            capDATE_OF_BIRTH.Text = objResourceMgr.GetString("txtDATE_OF_BIRTH");
            capACTIVITY.Text = objResourceMgr.GetString("cmbACTIVITY");
            capREGIONAL_ID.Text = objResourceMgr.GetString("txtREGIONAL_IDENTIFICATION");
            capREGIONAL_ID_ISSUE_DATE.Text = objResourceMgr.GetString("txtREG_ID_ISSUE_DATE");
            capREGIONAL_ID_ISSUE.Text = objResourceMgr.GetString("txtREG_ID_ISSUE");
			


			
			capBANK_ACCOUNT_NUMBER.Text					=		objResourceMgr.GetString("txtBANK_ACCOUNT_NUMBER");
			capROUTING_NUMBER.Text						=	    objResourceMgr.GetString("txtROUTING_NUMBER");
			capBANK_NAME.Text							=		objResourceMgr.GetString("txtBANK_NAME");
			capBANK_BRANCH.Text							=		objResourceMgr.GetString("txtBANK_BRANCH");
			capALLOWS_EFT.Text							=		objResourceMgr.GetString("cmbALLOWS_EFT");
			capREASON.Text								=		objResourceMgr.GetString("capREASON");
			
			capMAIL_1099_ADD1.Text				=		objResourceMgr.GetString("txtMAIL_1099_ADD1");
			capMAIL_1099_ADD2.Text				=		objResourceMgr.GetString("txtMAIL_1099_ADD2");
			capMAIL_1099_CITY.Text				=		objResourceMgr.GetString("txtMAIL_1099_CITY");
			capMAIL_1099_STATE.Text				=		objResourceMgr.GetString("txtMAIL_1099_STATE");
			capMAIL_1099_COUNTRY.Text			=		objResourceMgr.GetString("txtMAIL_1099_COUNTRY");
			capMAIL_1099_ZIP.Text				=		objResourceMgr.GetString("txtMAIL_1099_ZIP");
			capMAIL_1099_NAME.Text				=		objResourceMgr.GetString("txtMAIL_1099_NAME");
			capPROCESS_1099_OPT.Text			=      	objResourceMgr.GetString("txtPROCESS_1099_OPT");
			capW9_FORM.Text						=      	objResourceMgr.GetString("txtW9_FORM");
         
			capREVERIFIED_AC.Text				=		objResourceMgr.GetString("chkREVERIFIED_AC");
		//Added By Raghav For Special HAndling
			capREQ_SPECIAL_HANDLING.Text        =        objResourceMgr.GetString("chkREQ_SPECIAL_HANDLING");   

            //Added By Chetna
            capSUSEP_NUM.Text = objResourceMgr.GetString("txtSUSEP_NUM");
            hidEdit.Value = objResourceMgr.GetString("hidEdit");
            hidCancel.Value = objResourceMgr.GetString("hidCancel");

		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int intRetVal;	
			int intVendorID = int.Parse(hidVENDOR_ID.Value);
			objVendor = new Cms.BusinessLayer.BlCommon.clsVendor();
			ClsVendorInfo objVendorInfo = getFormValue();
			objVendorInfo.MODIFIED_BY = objVendorInfo.CREATED_BY;
			objVendorInfo.CREATED_DATETIME = objVendorInfo.LAST_UPDATED_DATETIME = DateTime.Now;
			intRetVal = objVendor.Delete(objVendorInfo,intVendorID);
			if(intRetVal>0)
			{
				//lblMessage.Text		 =	ClsMessages.GetMessage(base.ScreenId,"127");
				lblDelete.Text	=	ClsMessages.GetMessage(base.ScreenId,"127");
				hidFormSaved.Value	 =	"5";
				hidOldData.Value	 =  "";
				trBody.Attributes.Add("style","display:none");
			}
			else if(intRetVal == -1)
			{
				//lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"128");
				lblDelete.Text=ClsMessages.GetMessage(base.ScreenId,"86");
				hidFormSaved.Value		=	"2";
			}
			//lblMessage.Visible = true;
			lblDelete.Visible=true;
		}

		private void btnReset_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(Request.Url.ToString());
		}

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet AjaxFillState(string CountryID)
        {
            try
            {
                CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();
                string result = "";
                result = obj.FillState(CountryID);
                DataSet ds = new DataSet();
                ds.ReadXml(new System.IO.StringReader(result));

                return ds;
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return null;
            }
        }

       
       
	}
}
