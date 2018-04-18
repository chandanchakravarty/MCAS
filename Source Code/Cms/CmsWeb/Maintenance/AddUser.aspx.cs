/******************************************************************************************
<Author				: -   Gaurav Tyagi
<Start Date				: -	5/9/2005 1:40:20 PM
<End Date				: -	
<Description				: - 	This file is used to Add / Update /Activate-Deactivate User Information
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - This file is used to


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
using Cms.CmsWeb.Controls;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.BusinessLayer.BlCommon;
using System.Xml;
using Cms.Model.Maintenance;
using Cms.ExceptionPublisher;
namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// This class is used to Add / Update /Activate-Deactivate User Information
	/// </summary>
	public class AddUser : Cms.CmsWeb.cmsbase
	{
		#region Page controls declaration		
		protected System.Web.UI.WebControls.TextBox txtADJUSTER_CODE;
		protected System.Web.UI.WebControls.Label capADJUSTER_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvADJUSTER_CODE;
		protected System.Web.UI.WebControls.TextBox txtUSER_LOGIN_ID;
		protected System.Web.UI.WebControls.TextBox txtUSER_TYPE_ID;
		protected System.Web.UI.WebControls.TextBox txtUSER_PWD;
		protected System.Web.UI.WebControls.TextBox txtUSER_TITLE;
		protected System.Web.UI.WebControls.TextBox txtUSER_FNAME;
		protected System.Web.UI.WebControls.TextBox txtUSER_LNAME;
		protected System.Web.UI.WebControls.TextBox txtUSER_INITIALS;
		protected System.Web.UI.WebControls.TextBox txtUSER_ADD1;
		protected System.Web.UI.WebControls.TextBox txtUSER_ADD2;
		protected System.Web.UI.WebControls.TextBox txtUSER_CITY;
		protected System.Web.UI.WebControls.TextBox txtUSER_NOTES;//***********//
		protected System.Web.UI.WebControls.DropDownList cmbUSER_STATE;
		protected System.Web.UI.WebControls.TextBox txtUSER_ZIP;
		protected System.Web.UI.WebControls.TextBox txtUSER_PHONE;
		protected System.Web.UI.WebControls.TextBox txtUSER_EXT;
		protected System.Web.UI.WebControls.TextBox txtUSER_FAX;
		protected System.Web.UI.WebControls.TextBox txtUSER_MOBILE;
		protected System.Web.UI.WebControls.TextBox txtUSER_EMAIL;
		protected System.Web.UI.WebControls.CheckBox chkUSER_SPR;
		protected System.Web.UI.WebControls.CheckBox chkPINK_SLIP_NOTIFY;
		protected System.Web.UI.WebControls.DropDownList cmbUSER_MGR_ID;
		protected System.Web.UI.WebControls.DropDownList cmbUSER_DEF_DIV_ID;
		protected System.Web.UI.WebControls.DropDownList cmbUSER_DEF_DEPT_ID;
		protected System.Web.UI.WebControls.DropDownList cmbUSER_DEF_PC_ID;
		protected System.Web.UI.WebControls.DropDownList cmbUSER_TIME_ZONE;
		protected System.Web.UI.WebControls.TextBox txtIS_ACTIVE;
		protected System.Web.UI.WebControls.Image imgUSER_ZIP;
		protected System.Web.UI.WebControls.HyperLink hlkUSER_ZIP;
		//Added by Sibin for Itrack Issue 4994 on 9 Dec 08
		protected System.Web.UI.WebControls.Label capCHANGE_PWD_NEXT_LOGIN;
		protected System.Web.UI.WebControls.Label capUSER_LOCKED;
		protected System.Web.UI.WebControls.DropDownList cmbCHANGE_PWD_NEXT_LOGIN;
		protected System.Web.UI.WebControls.DropDownList cmbUSER_LOCKED;
		//Added till here
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_CODE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_LOGIN;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLIC_BRICS_USER;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLoginId;
       

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSER_TYPE_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSER_FNAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSER_LNAME;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSER_ADD1;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSER_CITY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSER_LOGIN_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSER_STATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSER_ZIP;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSER_INITIALS;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSER_EMAIL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSER_PWD;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSER_DEF_DIV_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSER_DEF_DEPT_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSER_DEF_PC_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSER_TIME_ZONE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIS_ACTIVE;
		protected System.Web.UI.WebControls.CustomValidator csvDIV_ZIP;
		//Added by Sibin on 21 Jan 09 for Itrack Issue 4994
		protected System.Web.UI.WebControls.RegularExpressionValidator revUSER_PWD;
		protected System.Web.UI.WebControls.RegularExpressionValidator revUSER_CONFIRM_PWD;
        protected System.Web.UI.WebControls.CustomValidator csvREG_ID_ISSUE_DATE;
		//Added till here
		//protected string strCalledFrom;
		protected System.Web.UI.WebControls.Label lblMessage;
        //int UNDERWRITER_USER_TYPE_ID=13;
        //added By Abhinav
        protected System.Web.UI.WebControls.Label capMessages;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTAB_TITLES;
        protected System.Web.UI.WebControls.Label capCPF;
        protected System.Web.UI.WebControls.TextBox txtCPF;
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
        protected System.Web.UI.WebControls.RegularExpressionValidator revREGIONAL_ID_ISSUE_DATE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidactivate;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDeactivate;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidinactive;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidactive;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidState;
		#endregion

		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		string strPageMode="";
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		private string []strDefaultHeirarchy;		// Holds the value for Default Heirarchy
		public string strCalledFrom="";
		private const int USER_TYPE_CSR = 44;

		protected System.Web.UI.WebControls.Label capUSER_TYPE;
		protected System.Web.UI.WebControls.Label lblStatus;
		protected System.Web.UI.WebControls.Label capUSER_TITLE;
        protected System.Web.UI.WebControls.Label capUSER_FNAME;
		protected System.Web.UI.WebControls.Label capUSER_LNAME;
		protected System.Web.UI.WebControls.Label capUSER_ADD1;
		protected System.Web.UI.WebControls.Label capUSER_ADD2;
		protected System.Web.UI.WebControls.Label capUSER_CITY;
		protected System.Web.UI.WebControls.Label capUSER_COUNTRY;
		protected System.Web.UI.WebControls.DropDownList cmbUSER_COUNTRY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSER_COUNTRY;
		protected System.Web.UI.WebControls.Label capUSER_STATE;
		protected System.Web.UI.WebControls.Label capUSER_ZIP;
		protected System.Web.UI.WebControls.Label capUSER_INITIALS;
		protected System.Web.UI.WebControls.Label capUSER_PHONE;
		protected System.Web.UI.WebControls.Label capUSER_EXT;
		protected System.Web.UI.WebControls.Label capUSER_FAX;
		protected System.Web.UI.WebControls.Label capUSER_MOBILE;
		protected System.Web.UI.WebControls.Label capUSER_EMAIL;
		protected System.Web.UI.WebControls.Label capUSER_LOGIN_ID;
		protected System.Web.UI.WebControls.Label capUSER_PWD;
		protected System.Web.UI.WebControls.Label capUSER_CONFIRM_PWD;
		protected System.Web.UI.WebControls.TextBox txtUSER_CONFIRM_PWD;

		//Added by Sibin for Itrack Issue 4173 on 15 Jan 09
		protected System.Web.UI.WebControls.Label capNON_RESI_LICENSE_EXP_DATE;
		protected System.Web.UI.WebControls.Label capNON_RESI_LICENSE_STATUS;
		protected System.Web.UI.WebControls.Label capNON_RESI_LICENSE_EXP_DATE2;
		protected System.Web.UI.WebControls.Label capNON_RESI_LICENSE_STATUS2;
		protected System.Web.UI.WebControls.TextBox txtNON_RESI_LICENSE_EXP_DATE;
		protected System.Web.UI.WebControls.TextBox txtNON_RESI_LICENSE_EXP_DATE2;
		protected System.Web.UI.WebControls.DropDownList cmbNON_RESI_LICENSE_STATUS;
		protected System.Web.UI.WebControls.DropDownList cmbNON_RESI_LICENSE_STATUS2;
		protected System.Web.UI.WebControls.Image imgNON_RESI_LICENSE_EXP_DATE;
		protected System.Web.UI.WebControls.Image imgNON_RESI_LICENSE_EXP_DATE2;
		protected System.Web.UI.WebControls.HyperLink hlkNON_RESI_LICENSE_EXP_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkNON_RESI_LICENSE_EXP_DATE2;
		protected System.Web.UI.WebControls.RegularExpressionValidator revNON_RESI_LICENSE_EXP_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revNON_RESI_LICENSE_EXP_DATE2;
		//Added till here

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSER_CONFIRM_PWD;
		protected System.Web.UI.WebControls.Label capUSER_MGR_ID;
		protected System.Web.UI.WebControls.Label capUSER_SPR;
		protected System.Web.UI.WebControls.Label capPINK_SLIP_NOTIFY;		
		protected System.Web.UI.WebControls.Label capUSER_TYPE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbUSER_TYPE_ID;
		protected System.Web.UI.WebControls.Label capDefault_Hierarchy;
		protected System.Web.UI.WebControls.DropDownList cmbDefault_Hierarchy;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDefault_Hierarchy;
		protected System.Web.UI.WebControls.Label capUSER_TIME_ZONE;
		protected System.Web.UI.WebControls.Label capUSER_NOTES;//******************//
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidUSER_ID;
		protected System.Web.UI.WebControls.RegularExpressionValidator revUSER_FNAME;
		protected System.Web.UI.WebControls.RegularExpressionValidator revLastName;
		
        //protected System.Web.UI.WebControls.RegularExpressionValidator revZip;Changed by amit k Mishra for tfs bug#836
        protected System.Web.UI.WebControls.RegularExpressionValidator revUSER_ZIP;//Added By Amit k mishra for tfs bug #836

        protected System.Web.UI.WebControls.RegularExpressionValidator revUserInitials;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revPhone;//Changed by Amit k mishra for tfs bug #836
        protected System.Web.UI.WebControls.RegularExpressionValidator revUSER_PHONE;//Added by Amit k mishra for tfs Bug #836
		protected System.Web.UI.WebControls.RegularExpressionValidator revExtn;
        protected System.Web.UI.WebControls.RegularExpressionValidator revUSER_FAX;//Added by Amit k mishra for tfs Bug #836
        //protected System.Web.UI.WebControls.RegularExpressionValidator revFax;//Changed by Amit k mishra for tfs bug #836
		//protected System.Web.UI.WebControls.RegularExpressionValidator revMobile;//Changes By Amit Mishra for tfs bug #836
        protected System.Web.UI.WebControls.RegularExpressionValidator revUSER_MOBILE;//Added by Amit k mishra for tfs Bug #836
        protected System.Web.UI.WebControls.RegularExpressionValidator revEmail;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSignOnId;
		protected System.Web.UI.WebControls.CompareValidator cvPassword;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidUSER_TYPE_ID;
		protected System.Web.UI.WebControls.Label capSUB_CODE;
		protected System.Web.UI.WebControls.TextBox txtSUB_CODE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSUB_CODE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSUB_CODE;
		protected Cms.CmsWeb.Controls.CmsButton btnCopyAddress;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAgencyDetails;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ADD1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ADD2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_CITY;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_STATE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ZIP;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_COUNTRY;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_PHONE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_FAX;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidReset;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDATE_OF_BIRTH_NEW;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDATE_EXPIRY_NEW;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidNON_RESI_LICENSE_EXP_DATE_NEW;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidNON_RESI_LICENSE_EXP_DATE2_NEW;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidREG_ID_ISSUE_DATE_NEW;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidUSER_COUNTRY;
        protected System.Web.UI.WebControls.CompareValidator cpvREG_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.CompareValidator cpv2REG_ID_ISSUE_DATE_FUTURE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidZipeCodeVerificationMsg;// changed by praveer for itrack no 1553/TFS# 626 

        //protected System.Web.UI.WebControls.Label capSSN_NO;
        //protected System.Web.UI.WebControls.TextBox txtSSN_NO;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revSSN_NO;
		protected System.Web.UI.WebControls.Label capDATE_OF_BIRTH;
		protected System.Web.UI.WebControls.TextBox txtDATE_OF_BIRTH;
		protected System.Web.UI.WebControls.HyperLink hlkDATE_OF_BIRTH;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDATE_OF_BIRTH;
		protected System.Web.UI.WebControls.Label capDRIVER_LIC_NO;
		protected System.Web.UI.WebControls.TextBox txtDRIVER_LIC_NO;
		protected System.Web.UI.WebControls.Label capDATE_EXPIRY;
		protected System.Web.UI.WebControls.TextBox txtDATE_EXPIRY;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDATE_EXPIRY;
		protected System.Web.UI.WebControls.Label capLICENSE_STATUS;
		protected System.Web.UI.WebControls.DropDownList cmbLICENSE_STATUS;
		protected System.Web.UI.WebControls.HyperLink hlkDATE_EXPIRY;
		protected System.Web.UI.WebControls.Label capNON_RESI_LICENSE_STATE;
		protected System.Web.UI.WebControls.DropDownList cmbNON_RESI_LICENSE_STATE;
		protected System.Web.UI.WebControls.Label capNON_RESI_LICENSE_NO;
		protected System.Web.UI.WebControls.TextBox txtNON_RESI_LICENSE_NO;
		//************Added by Manoj Rathore(6th Nov 2006)**********//
		protected System.Web.UI.WebControls.Label capNON_RESI_LICENSE_STATE2;
		protected System.Web.UI.WebControls.DropDownList cmbNON_RESI_LICENSE_STATE2;
		protected System.Web.UI.WebControls.Label capNON_RESI_LICENSE_NO2;
		protected System.Web.UI.WebControls.TextBox txtNON_RESI_LICENSE_NO2;
		//protected System.Web.UI.WebControls.Label cap_BRICS_USER;
		//protected System.Web.UI.WebControls.DropDownList cmb_BRICS_USER;
		//************//
		protected System.Web.UI.WebControls.CustomValidator csvDATE_OF_BIRTH;
		protected System.Web.UI.WebControls.Label capLIC_BRICS_USER;
		protected System.Web.UI.WebControls.DropDownList cmbLIC_BRICS_USER;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvSSN_NO;
		protected System.Web.UI.WebControls.CustomValidator csvUSER_ZIP;
        //protected System.Web.UI.WebControls.Button btnSSN_NO;
		
        //protected System.Web.UI.WebControls.Label capSSN_NO_HID;
        //protected System.Web.UI.HtmlControls.HtmlInputHidden hidSSN_NO;
		
		//Added by Sibin for Itrack Issue 5212 to remove USER_PWD and USER_CONFIRM_PWD from View Source - Done on 7 Jan 08
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPWD;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCONFIRM_PWD;
		//Added till here
		//Defining the business layer class object
		ClsUser  objUser ;
		public int intUserId = 0;
		string CalledFrom = "";
        protected string strCarrierSIN = "";
        
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
			//rfvUSER_TYPE_ID.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,);
			rfvUSER_FNAME.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"57");
            //rfvUSER_LNAME.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"58");
			rfvUSER_ADD1.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"742");
			rfvUSER_CITY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"56");
			rfvUSER_LOGIN_ID.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"67");
			rfvUSER_STATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"35");
			rfvUSER_COUNTRY.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"33");
			rfvUSER_ZIP.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"37");
			rfvUSER_INITIALS.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"66");
			rfvUSER_EMAIL.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"61");
			rfvUSER_PWD.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"68");
			rfvUSER_CONFIRM_PWD.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"69");
			rfvUSER_TIME_ZONE.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"83");
			//rfvUSER_NOTES.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"13");//***
			//rfvIS_ACTIVE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,83);
			rfvDefault_Hierarchy.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"84");
			rfvUSER_TYPE_ID.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"81");
			//rfvUSER_COLOR_SCHEME.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
			rfvSUB_CODE.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"494");
            csvREG_ID_ISSUE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2088");
			cvPassword.ErrorMessage				= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"924");

            //Commented by Ruchika Chauhan on 24-Jan-2012 for TFS Bug # 836
			//revUSER_FNAME.ValidationExpression	=	aRegExpClientName;
            revUSER_FNAME.ValidationExpression = "";

			revLastName.ValidationExpression	=	aRegExpClientName;
            //revPhone.ValidationExpression		=	aRegExpPhone;
            //revFax.ValidationExpression			=	aRegExpFax;
			revExtn.ValidationExpression		=	aRegExpExtn;
			revEmail.ValidationExpression		=	aRegExpEmail;
            //revUSER_ZIP.ValidationExpression			=	aRegExpZip; //commented by Aditya for TFS BUG # 1832
            if (GetLanguageID() == "2") //Added by Aditya for TFS BUG # 1832
            {
                //   revZip.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2079");     Commented by Amit K mishra for Tfs bug #836
                revUSER_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2079");//Added By Amit k mishra for tfs bug #836
            }
            else
            {
                //   revZip.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("24");   Commented by Amit K mishra for Tfs bug #836
                revUSER_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("24");//Added By Amit k mishra for tfs bug #836
            }
            if (GetLanguageID() == "2")
            {
                //revZip.ValidationExpression = aRegExpZipBrazil;   Commented by Amit K mishra for Tfs bug #836
                revUSER_ZIP.ValidationExpression = aRegExpZipBrazil;//Added By Amit k mishra for tfs bug #836
            }
            else
            {
                //revZip.ValidationExpression = aRegExpZipUS;   Commented by Amit K mishra for Tfs bug #836
                revUSER_ZIP.ValidationExpression = aRegExpZipUS;//Added By Amit k mishra for tfs bug #836
            }//revMobile.ValidationExpression		=	aRegExpPhone;
          
			revSignOnId.ValidationExpression	=	aRegExpAlphaNumStrict;
			revUserInitials.ValidationExpression=	aRegExpAlpha;
            //revSSN_NO.ValidationExpression	    =	aRegExpSSN;
			revSUB_CODE.ValidationExpression    =   aRegExpInteger;
			revDATE_EXPIRY.ValidationExpression =   aRegExpDate;
			revDATE_OF_BIRTH.ValidationExpression=  aRegExpDate;
			//revUSER_NOTES.ValidationExpression  =   aRegExpUserNotes;
            revCPF_CNPJ.ValidationExpression = aRegExpCpf_Cnpj;
            revREGIONAL_ID_ISSUE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            revCPF_CNPJ.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1081");
            revREGIONAL_ID_ISSUE_DATE.ValidationExpression = aRegExpDate;
			
			//Added by Sibin for Itrack Issue 4173 on 15 Jan 09
			revNON_RESI_LICENSE_EXP_DATE.ValidationExpression =   aRegExpDate;
			revNON_RESI_LICENSE_EXP_DATE2.ValidationExpression =   aRegExpDate;
			//Added till here

			revDATE_EXPIRY.ErrorMessage		=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			revDATE_OF_BIRTH.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"22");
			//revUSER_FNAME.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"920");
			revLastName.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"921");
            //revPhone.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
            //revFax.ErrorMessage				=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("15");
			revExtn.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("25");
            //revZip.ErrorMessage				=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2079"); //Commented by Aditya for TFS BUG # 1832
			revEmail.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("124");
			//revMobile.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("16");
			revSignOnId.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"922");
			revUserInitials.ErrorMessage	=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1004");
			revSUB_CODE.ErrorMessage		=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"163");
            //revSSN_NO.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("130");
			csvDATE_OF_BIRTH.ErrorMessage   =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("198");
			rfvADJUSTER_CODE.ErrorMessage   =   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("986");
            cpvREG_ID_ISSUE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1402");
            cpv2REG_ID_ISSUE_DATE_FUTURE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1403");
			//Added by Sibin for Itrack Issue 4173 on 15 Jan 09
			revNON_RESI_LICENSE_EXP_DATE.ErrorMessage		=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			revNON_RESI_LICENSE_EXP_DATE2.ErrorMessage		=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
			//Added till here
			
			//Added by Sibin on 21 Jan 09 for Itrack Issue 4994
			revUSER_PWD.ValidationExpression	= aRegExpPasswordOneNumeric;
			revUSER_PWD.ErrorMessage	=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1036");
			revUSER_CONFIRM_PWD.ValidationExpression		=aRegExpPasswordOneNumeric;
			revUSER_CONFIRM_PWD.ErrorMessage		= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1036");

            if (GetLanguageID() == "1")  //Changes done by Aditya for TFS BUG # 1832
            {
               // revPhone.ValidationExpression = aRegExpPhoneBrazil;  // changes done by Amit Mishra for Tfs bug #836
                revUSER_PHONE.ValidationExpression = aRegExpPhoneBrazil;
                //revFax.ValidationExpression = aRegExpPhoneBrazil;
                revUSER_FAX.ValidationExpression = aRegExpPhoneBrazil;//Added By Amit Mishra for Tfs bug #836
               // revMobile.ValidationExpression = aRegExpPhoneBrazil;
                revUSER_MOBILE.ValidationExpression = aRegExpPhoneBrazil;//Added By Amit Mishra for Tfs Bug #836

                //revPhone.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1864"); //changes done by Amit Mishra for Tfs bug #836
                revUSER_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1864");
                //revFax.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1864");//changes done by Amit Mishra for Tfs bug #836
                revUSER_FAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1864"); //Added By Amit Mishra for Tfs bug #836
                //revMobile.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1864");//changes done by Amit Mishra for Tfs bug #836
                revUSER_MOBILE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1864");//Added By Amit Mishra for Tfs bug #836
            } 

            else
            {
                //revPhone.ValidationExpression = aRegExpAgencyPhone;//// changes done by Amit Mishra for Tfs bug #836
                revUSER_PHONE.ValidationExpression = aRegExpAgencyPhone;
                revUSER_FAX.ValidationExpression = aRegExpAgencyPhone;//Added By Amit Mishra for Tfs bug #836
                //revFax.ValidationExpression = aRegExpAgencyPhone;////changes done by Amit Mishra for Tfs bug #836             
                //revMobile.ValidationExpression = aRegExpAgencyPhone;
                revUSER_MOBILE.ValidationExpression = aRegExpAgencyPhone;
                //revPhone.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1913");changes done by Amit Mishra for Tfs bug #836
                revUSER_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1913");
                revUSER_FAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1921");//Added By Amit Mishra for Tfs bug #836
               // revFax.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1921");//changes done by Amit Mishra for Tfs bug #836
                //revMobile.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1922");//Added By Amit Mishra for Tfs bug #836
                revUSER_MOBILE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1922");//changes done by Amit Mishra for Tfs bug #836
            }
			//Added till here
		}
		#endregion

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			Ajax.Utility.RegisterTypeForAjax(typeof(AddUser));
            strCarrierSIN = GetSystemId().ToString().ToUpper();
            if (strCarrierSIN == "S001" || strCarrierSIN == "SUAT")
            {
                hlkUSER_ZIP.Visible = false;
                imgUSER_ZIP.Visible = false;
            }
        
			// for wolverine user only, display Preferences & Assign rights tabs
////////////			string userID = GetSystemId();
////////////			if (userID != CarrierSystemID)
////////////			{trCOMPLETE_APP.Visible=false;}
////////////			else
////////////			{trCOMPLETE_APP.Visible=true;}

           

			if(Request.QueryString["CalledFrom"] != null || Request.QueryString["CalledFrom"] != "")
			{
				strCalledFrom				=	Request.QueryString["CalledFrom"];
				
			}
			if(strCalledFrom == "AGENCY")
			{
				base.ScreenId="10_1_0";

				//hidAGENCY.Value              =        strCalledFrom;
			}   
			else
				base.ScreenId ="25_0";
            Page.DataBind();
			//cmbUSER_COUNTRY.SelectedIndex = int.Parse(aCountry);
			btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
			hlkDATE_OF_BIRTH.Attributes.Add("onclick","fPopCalendar(document.MNT_USER_LIST.txtDATE_OF_BIRTH, document.MNT_USER_LIST.txtDATE_OF_BIRTH)");
            hlkREG_ID_ISSUE_DATE.Attributes.Add("onclick", "fPopCalendar(document.MNT_USER_LIST.txtREG_ID_ISSUE_DATE, document.MNT_USER_LIST.txtREG_ID_ISSUE_DATE)");
			hlkDATE_EXPIRY.Attributes.Add("onclick","fPopCalendar(document.MNT_USER_LIST.txtDATE_EXPIRY, document.MNT_USER_LIST.txtDATE_EXPIRY)");
			//Added by Sibin for Itrack Issue 4173 on 15 Jan 09
			hlkNON_RESI_LICENSE_EXP_DATE.Attributes.Add("onclick","fPopCalendar(document.MNT_USER_LIST.txtNON_RESI_LICENSE_EXP_DATE, document.MNT_USER_LIST.txtNON_RESI_LICENSE_EXP_DATE)");
			hlkNON_RESI_LICENSE_EXP_DATE2.Attributes.Add("onclick","fPopCalendar(document.MNT_USER_LIST.txtNON_RESI_LICENSE_EXP_DATE2, document.MNT_USER_LIST.txtNON_RESI_LICENSE_EXP_DATE2)");
			//Added till here
			btnCopyAddress.Attributes.Add("onclick","return copyAgencyAddress();");
            txtUSER_PHONE.Attributes.Add("onBlur", "javascript:DisableExt('txtUSER_PHONE','txtUSER_EXT');FormatPhone()");
			btnSave.Attributes.Add("onclick","javascript:Password_Change();");
            txtUSER_ADD1.Attributes.Add("onBlur", "javascript:EnableDisableRfv();");
            txtUSER_CITY.Attributes.Add("onBlur", "javascript:EnableDisableRfv();");
            txtUSER_ZIP.Attributes.Add("onBlur", "javascript:EnableDisableRfv();");
            cmbUSER_STATE.Attributes.Add("onBlur", "javascript:EnableDisableRfv();");

			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");	
			//base.ScreenId="25_0";
            hidactivate.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1314");
            hidDeactivate.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1315");
            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");
            hidinactive.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1323");
            hidactive.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1322");
			lblMessage.Visible = false;
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCon");
            //capSSN_NO.Visible = false;
            //txtSSN_NO.Visible = false;
            //revSSN_NO.Visible = false;
            //rfvSSN_NO.Visible = false;
            //capSSN_NO_HID.Visible = false;
            //btnSSN_NO.Visible = false;

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass			=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass		=	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass			=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnCopyAddress.CmsButtonClass		=	CmsButtonType.Read;
			btnCopyAddress.PermissionString		=	gstrSecurityXML;


			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddUser" ,System.Reflection.Assembly.GetExecutingAssembly());
			
			if(Request.QueryString["USER_ID"]!=null && Request.QueryString["USER_ID"].ToString().Length>0)
			{
				strPageMode = "Edit";
				hidUSER_ID.Value =Request.QueryString["USER_ID"].ToString();
			}
				
			else
			{
				strPageMode = "Add";
				intUserId = -1;
			}

			if(Request.QueryString["CalledFrom"]!=null && Request.QueryString["CalledFrom"].ToString().Length>0)
			{
				CalledFrom = Request.QueryString["CalledFrom"];
				hidAGENCY.Value=CalledFrom;
			}

            if (Request.QueryString["USER_LOGIN_ID"] != null && Request.QueryString["USER_LOGIN_ID"].ToString().Length > 0)
            {

                hidLoginId.Value = Request.QueryString["USER_LOGIN_ID"].ToString();
            }

			
			//SetXml(Request.QueryString["USER_ID"]);
			if(!Page.IsPostBack)
			{
                string strCarrierSystemID = CarrierSystemID;//System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString();
				imgUSER_ZIP.Attributes.Add("style","cursor:hand");
				cmbUSER_TYPE_ID.Attributes.Add("onChange","cmbUSER_TYPE_ID_Change();return false;");
				base.VerifyAddress(hlkUSER_ZIP,txtUSER_ADD1,txtUSER_ADD2
					, txtUSER_CITY, cmbUSER_STATE, txtUSER_ZIP);
				string strAgencyID = GetSystemId();
               
                SetCaptions();
                SetErrorMessages();
                               
				if(Request.QueryString["AGENCY_CODE"] != null || Request.QueryString["AGENCY_CODE"] != "")
				{
					hidAGENCY_CODE.Value		=	Request.QueryString["USER_SYSTEM_ID"];
				}
				GetOldDataXML();
				
                string strSysID = GetSystemId();
                if (strSysID == "ALBAUAT")
                    strSysID = "ALBA";
                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "/CmsWeb/support/PageXml/" + strSysID, "AddUser.xml"))
                    setPageControls(Page, Request.PhysicalApplicationPath + "/CmsWeb/support/PageXml/" + strSysID + "/AddUser.xml");  


				Cms.BusinessLayer.BlCommon.ClsUser.GetUserManagerDropDown(cmbUSER_MGR_ID,hidAGENCY_CODE.Value);
                
//				if(hidAGENCY_CODE.Value	== strCarrierSystemID.Trim().ToUpper())
//				if(strAgencyID.ToString().ToUpper()	== strCarrierSystemID.Trim().ToUpper())
//				{
//					hidAGENCY_LOGIN.Value = Request.QueryString["USER_SYSTEM_ID"];
//					Cms.BusinessLayer.BlCommon.ClsUser.GetUserTypeDropDown(cmbUSER_TYPE_ID);
//				}
//				else if(CalledFrom=AGENCY)
//				{
//					hidAGENCY_LOGIN.Value = "1";
//					Cms.BusinessLayer.BlCommon.ClsUser.GetAgencyUserTypeDropDown(cmbUSER_TYPE_ID);
//				}

				if(strAgencyID.ToString().ToUpper()	!= strCarrierSystemID.Trim().ToUpper() || CalledFrom!="" )
				{
					hidAGENCY_LOGIN.Value = "1";
					Cms.BusinessLayer.BlCommon.ClsUser.GetAgencyUserTypeDropDown(cmbUSER_TYPE_ID);
				}
				else
				{
					hidAGENCY_LOGIN.Value = Request.QueryString["USER_SYSTEM_ID"];
					Cms.BusinessLayer.BlCommon.ClsUser.GetUserTypeDropDown(cmbUSER_TYPE_ID);
				}
				Cms.BusinessLayer.BlCommon.ClsDivision.GetDefaultHierarchyDropDown(cmbDefault_Hierarchy);
				Cms.BusinessLayer.BlCommon.ClsCommon.GetTimeZoneDropDown(cmbUSER_TIME_ZONE);
				cmbUSER_MGR_ID.Items.Insert(0,new ListItem("",""));
				cmbUSER_TYPE_ID.Items.Insert(0,new ListItem("",""));
				cmbUSER_TIME_ZONE.Items.Insert(0,new ListItem("",""));

				#region "Loading singleton"
                DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
                //cmbUSER_COUNTRY.DataSource		= dt;
                //cmbUSER_COUNTRY.DataTextField	= "Country_Name";
                //cmbUSER_COUNTRY.DataValueField	= "Country_Id";
                //cmbUSER_COUNTRY.DataBind();

                DataTable dtCountry = Cms.CmsWeb.ClsFetcher.AllCountry; 
                cmbUSER_COUNTRY.DataSource = dtCountry;
                cmbUSER_COUNTRY.DataTextField = COUNTRY_NAME;
                cmbUSER_COUNTRY.DataValueField = COUNTRY_ID;
                cmbUSER_COUNTRY.DataBind();
                // cmbCUSTOMER_COUNTRY.SelectedItem.Text = "Brazil";
                hidUSER_COUNTRY.Value = cmbUSER_COUNTRY.SelectedItem.Text;

                if (GetSystemId().ToUpper() == "S001" || GetSystemId().ToUpper() == "SUAT")
                {
                    //if (hidUSER_COUNTRY.Value != "")
                    //{
                    //    if (hidUSER_COUNTRY.Value == "Singapore")
                    //    {
                    //        hidUSER_COUNTRY.Value = "Singapore";
                    //    }
                    //    cmbUSER_COUNTRY.SelectedIndex = cmbUSER_COUNTRY.Items.IndexOf(cmbUSER_COUNTRY.Items.FindByText(hidUSER_COUNTRY.Value));
                    //}
                    //else
                    //{
                        cmbUSER_COUNTRY.SelectedIndex = cmbUSER_COUNTRY.Items.IndexOf(cmbUSER_COUNTRY.Items.FindByText("Singapore"));
                    //}
                }
                else
                {
                    if (hidUSER_COUNTRY.Value != "")
                    {
                        if (hidUSER_COUNTRY.Value == "Brasil")
                        {
                            hidUSER_COUNTRY.Value = "Brazil";
                        }
                        cmbUSER_COUNTRY.SelectedIndex = cmbUSER_COUNTRY.Items.IndexOf(cmbUSER_COUNTRY.Items.FindByText(hidUSER_COUNTRY.Value));
                    }
                    else
                    {
                        cmbUSER_COUNTRY.SelectedIndex = cmbUSER_COUNTRY.Items.IndexOf(cmbUSER_COUNTRY.Items.FindByText("Brazil"));
                    }
                }
				dt = Cms.CmsWeb.ClsFetcher.State;
				cmbUSER_STATE.DataSource		= dt;
				cmbUSER_STATE.DataTextField	= "State_Name";
				cmbUSER_STATE.DataValueField= "State_Id";
				cmbUSER_STATE.DataBind();

				dt = Cms.CmsWeb.ClsFetcher.State;
				cmbNON_RESI_LICENSE_STATE.DataSource		= dt;
				cmbNON_RESI_LICENSE_STATE.DataTextField		= "STATE_NAME";
				cmbNON_RESI_LICENSE_STATE.DataValueField	= "STATE_ID";
				cmbNON_RESI_LICENSE_STATE.DataBind();
				cmbNON_RESI_LICENSE_STATE.Items.Insert(0,"");

				dt = Cms.CmsWeb.ClsFetcher.State;
				cmbNON_RESI_LICENSE_STATE2.DataSource		= dt;
				cmbNON_RESI_LICENSE_STATE2.DataTextField	= "STATE_NAME";
				cmbNON_RESI_LICENSE_STATE2.DataValueField	= "STATE_ID";
				cmbNON_RESI_LICENSE_STATE2.DataBind();
				cmbNON_RESI_LICENSE_STATE2.Items.Insert(0,"");				

				#endregion//Loading singleton
						
				cmbLICENSE_STATUS.DataSource		=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("LICST");
				cmbLICENSE_STATUS.DataTextField		= "LookupDesc";
				cmbLICENSE_STATUS.DataValueField	= "LookupID";
				cmbLICENSE_STATUS.DataBind();
				cmbLICENSE_STATUS.Items.Insert(0,"");

				cmbLIC_BRICS_USER.DataSource		= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
				cmbLIC_BRICS_USER.DataTextField		= "LookupDesc";
				cmbLIC_BRICS_USER.DataValueField	= "LookupID";
				cmbLIC_BRICS_USER.DataBind();
		
				//Added by Sibin for Itrack Issue 4994 on 9 Dec 08
				cmbCHANGE_PWD_NEXT_LOGIN.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
				cmbCHANGE_PWD_NEXT_LOGIN.DataTextField="LookupDesc"; 
				cmbCHANGE_PWD_NEXT_LOGIN.DataValueField="LookupID";
				cmbCHANGE_PWD_NEXT_LOGIN.DataBind();
				cmbCHANGE_PWD_NEXT_LOGIN.Items.Insert(0,"");

				cmbUSER_LOCKED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
				cmbUSER_LOCKED.DataTextField="LookupDesc"; 
				cmbUSER_LOCKED.DataValueField="LookupCode";
				cmbUSER_LOCKED.DataBind();
				cmbUSER_LOCKED.Items.Insert(0,"");

				//Added till here

				//Added by Sibin for Itrack Issue 4173 on 15 Jan 09

				cmbNON_RESI_LICENSE_STATUS.DataSource		=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("LICST");
				cmbNON_RESI_LICENSE_STATUS.DataTextField		= "LookupDesc";
				cmbNON_RESI_LICENSE_STATUS.DataValueField	= "LookupID";
				cmbNON_RESI_LICENSE_STATUS.DataBind();
				cmbNON_RESI_LICENSE_STATUS.Items.Insert(0,"");

				cmbNON_RESI_LICENSE_STATUS2.DataSource		=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("LICST");
				cmbNON_RESI_LICENSE_STATUS2.DataTextField		= "LookupDesc";
				cmbNON_RESI_LICENSE_STATUS2.DataValueField	= "LookupID";
				cmbNON_RESI_LICENSE_STATUS2.DataBind();
				cmbNON_RESI_LICENSE_STATUS2.Items.Insert(0,"");
                
                DataTable dp = Cms.BusinessLayer.BlApplication.ClsLocation.SELECTACTIVITYTYPE();
                cmbACTIVITY.DataSource = dp;
                cmbACTIVITY.DataTextField = "ACTIVITY_DESC";
                cmbACTIVITY.DataValueField = "ACTIVITY_ID";
                cmbACTIVITY.DataBind();
                cmbACTIVITY.Items.Insert(0, "");
                cmbACTIVITY.SelectedIndex = 0;


				//Added till here
               
				//Setting xml for the page to be displayed in page controls
				if(Request.QueryString["USER_ID"]!=null && Request.QueryString["USER_ID"].ToString().Length>0 && Request.QueryString["USER_LOGIN_ID"] != null && Request.QueryString["USER_LOGIN_ID"].ToString().Length>0)
				{
					SetXml(Request.QueryString["USER_ID"]);
					strPageMode = "Edit";
					txtUSER_LOGIN_ID.ReadOnly=true;
                    rfvUSER_LOGIN_ID.Enabled = false;
				}
				else if(hidUSER_ID.Value != null && hidUSER_ID.Value.ToString().Length > 0)
				{
					SetXml(hidUSER_ID.Value.ToString());
					strPageMode = "Edit";
					txtUSER_LOGIN_ID.ReadOnly=false;
                    if (GetSystemId().ToUpper() == "S001" || GetSystemId().ToUpper() == "SUAT")
                    {
                        objUser = new ClsUser();
                        txtSUB_CODE.Text = objUser.GetSubCode();
                    }
				}
				else
				{
					strPageMode = "Add";
					txtUSER_LOGIN_ID.ReadOnly=false;
                    //Added by kuldeep to get the seqential code for sub code
                    if (GetSystemId().ToUpper() == "S001" || GetSystemId().ToUpper() == "SUAT")
                    {
                        objUser = new ClsUser();
                        txtSUB_CODE.Text = objUser.GetSubCode();
                    }
				}
			
			if(intUserId == -1 && hidFormSaved.Value == "0")
			{
				CheckUserLicence();
			}
            
          
               
}

            
          
         
			SetAgencyDetails();
		}//end pageload
		#endregion

		private void CheckUserLicence()
		{
			ClsUser objUserLic = new ClsUser();  
			if(Request.QueryString["USER_SYSTEM_ID"]!= null && objUserLic!= null)
				intUserId =objUserLic.CheckUserLicence(Request.QueryString["USER_SYSTEM_ID"].ToString());

			if(intUserId == -3)
			{

				cmbLIC_BRICS_USER.SelectedIndex = cmbLIC_BRICS_USER.Items.IndexOf(cmbLIC_BRICS_USER.Items.FindByText("No"));
			}
			else
			{
				cmbLIC_BRICS_USER.SelectedIndex = cmbLIC_BRICS_USER.Items.IndexOf(cmbLIC_BRICS_USER.Items.FindByText("Yes"));
			}
		}
		private void SetAgencyDetails()
		{
			DataSet dsagency=new DataSet(); 
			dsagency=ClsUser.GetAgencyDetails(hidAGENCY_CODE.Value);
			if(dsagency!=null )
			{
				if(dsagency.Tables[0].Rows.Count>0 )
				{
					hidAGENCY_ADD1.Value=dsagency.Tables[0].Rows[0]["AGENCY_ADD1"].ToString();
					hidAGENCY_ADD2.Value=dsagency.Tables[0].Rows[0]["AGENCY_ADD2"].ToString();
					hidAGENCY_CITY.Value=dsagency.Tables[0].Rows[0]["AGENCY_CITY"].ToString();
					hidAGENCY_STATE.Value=dsagency.Tables[0].Rows[0]["AGENCY_STATE"].ToString();
					hidAGENCY_COUNTRY.Value=dsagency.Tables[0].Rows[0]["AGENCY_COUNTRY"].ToString();
					hidAGENCY_ZIP.Value=dsagency.Tables[0].Rows[0]["AGENCY_ZIP"].ToString();
					hidAGENCY_PHONE.Value=dsagency.Tables[0].Rows[0]["AGENCY_PHONE"].ToString();  
					hidAGENCY_FAX.Value=dsagency.Tables[0].Rows[0]["AGENCY_FAX"].ToString();     
				}
			}
		}

        //==================================================================
        // ADDED BY SANTOSH KUMAR GAUTAM ON 29 MARCH 2011 FOR ITRACK:1024
        //==================================================================
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
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
            catch //(Exception ex)
            {
                return null;
            }
        }   //fill State from database onchange country

        private void SetXml(string strUserId)
        {
            hidOldData.Value = ClsUser.GetXmlForPageControls(strUserId);
            //ITrack # 6199 -30 July 09 - Manoj 
            hidLIC_BRICS_USER.Value = ClsCommon.FetchValueFromXML("LIC_BRICS_USER", hidOldData.Value);
            // itrack no 677
            if (cmbDefault_Hierarchy.SelectedValue != "")
            {
                cmbDefault_Hierarchy.SelectedValue = ClsCommon.FetchValueFromXML("USER_DEF_PC_ID", hidOldData.Value);
            }
            if (ClsCommon.FetchValueFromXML("LIC_BRICS_USER", hidOldData.Value) != "10964")//Added by Sibin on 13 Jan 09 for Itrack Issue 5290
            {
                //Added by Sibin for Itrack Issue 5417 on 9 Feb 09
                if (ClsCommon.FetchValueFromXML("USER_PWD", hidOldData.Value) != "" && ClsCommon.FetchValueFromXML("USER_PWD", hidOldData.Value) != null)
                {
                    //Added by Sibin for Itrack Issue 5212 to remove USER_PWD and USER_CONFIRM_PWD from View Source - Done on 7 Jan 08
                    hidPWD.Value = Cms.BusinessLayer.BlCommon.ClsCommon.EncryptString(ClsCommon.FetchValueFromXML("USER_PWD", hidOldData.Value));
                    hidCONFIRM_PWD.Value = Cms.BusinessLayer.BlCommon.ClsCommon.EncryptString(ClsCommon.FetchValueFromXML("USER_CONFIRM_PWD", hidOldData.Value));

                    if (ClsCommon.FetchValueFromXML("DATE_OF_BIRTH", hidOldData.Value) != "" && ClsCommon.FetchValueFromXML("DATE_OF_BIRTH", hidOldData.Value) != "0")
                    {
                        DateTime RID = Convert.ToDateTime(ClsCommon.FetchValueFromXML("DATE_OF_BIRTH", hidOldData.Value));
                        this.hidDATE_OF_BIRTH_NEW.Value = ConvertToDateCulture(RID);

                    }
                    if (ClsCommon.FetchValueFromXML("REG_ID_ISSUE_DATE", hidOldData.Value) != "" && ClsCommon.FetchValueFromXML("REG_ID_ISSUE_DATE", hidOldData.Value) != "0")
                    {
                        DateTime RID = Convert.ToDateTime(ClsCommon.FetchValueFromXML("REG_ID_ISSUE_DATE", hidOldData.Value));
                        this.hidREG_ID_ISSUE_DATE_NEW.Value = ConvertToDateCulture(RID);

                    }
                    if (ClsCommon.FetchValueFromXML("DATE_EXPIRY", hidOldData.Value) != "" && ClsCommon.FetchValueFromXML("DATE_EXPIRY", hidOldData.Value) != "0")
                    {
                        DateTime RID = Convert.ToDateTime(ClsCommon.FetchValueFromXML("DATE_EXPIRY", hidOldData.Value));
                        this.hidDATE_EXPIRY_NEW.Value = ConvertToDateCulture(RID);

                    }
                    if (ClsCommon.FetchValueFromXML("NON_RESI_LICENSE_EXP_DATE", hidOldData.Value) != "" && ClsCommon.FetchValueFromXML("NON_RESI_LICENSE_EXP_DATE", hidOldData.Value) != "0")
                    {
                        DateTime RID = Convert.ToDateTime(ClsCommon.FetchValueFromXML("NON_RESI_LICENSE_EXP_DATE", hidOldData.Value));
                        this.hidNON_RESI_LICENSE_EXP_DATE_NEW.Value = ConvertToDateCulture(RID);

                    }
                    if (ClsCommon.FetchValueFromXML("NON_RESI_LICENSE_EXP_DATE2", hidOldData.Value) != "" && ClsCommon.FetchValueFromXML("NON_RESI_LICENSE_EXP_DATE2", hidOldData.Value) != "0")
                    {
                        DateTime RID = Convert.ToDateTime(ClsCommon.FetchValueFromXML("NON_RESI_LICENSE_EXP_DATE2", hidOldData.Value));
                        this.hidNON_RESI_LICENSE_EXP_DATE2_NEW.Value = ConvertToDateCulture(RID);

                    }
                    //for itrack 677
                    if (cmbDefault_Hierarchy.SelectedValue != "") {
                        cmbDefault_Hierarchy.SelectedValue = ClsCommon.FetchValueFromXML("USER_DEF_PC_ID", hidOldData.Value);
                    }


                    if (cmbUSER_COUNTRY.SelectedValue != "")
                    {
                        cmbUSER_COUNTRY.SelectedValue = ClsCommon.FetchValueFromXML("USER_COUNTRY", hidOldData.Value);
                    }
                    if (hidOldData.Value.IndexOf("NewDataSet") >= 0)
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(hidOldData.Value);

                        XmlNode nodePWD = xmlDoc.FirstChild.FirstChild;
                        XmlNode nodeUSER_PWD = nodePWD.SelectSingleNode("USER_PWD");
                        nodePWD.RemoveChild(nodeUSER_PWD);

                        XmlNode nodeUSER_CONFIRM = xmlDoc.FirstChild.FirstChild;
                        XmlNode nodeUSER_CONFIRM_PWD = nodeUSER_CONFIRM.SelectSingleNode("USER_CONFIRM_PWD");
                        nodeUSER_CONFIRM.RemoveChild(nodeUSER_CONFIRM_PWD);

                        hidOldData.Value = xmlDoc.InnerXml;

                    }
                }
            }
        }
			
			//Added till here
			
        //    //Added by Mohit Agarwal 3-Sep-08
        //    if(hidOldData.Value.IndexOf("NewDataSet") >= 0)
        //    {
        //        XmlDocument objxml = new XmlDocument();
				
        //        objxml.LoadXml(hidOldData.Value);
        //        XmlNode node = objxml.SelectSingleNode("NewDataSet");
        //        foreach(XmlNode nodes in node.SelectNodes("Table"))
        //        {
        //            XmlNode noder1 = nodes.SelectSingleNode("SSN_NO");

        //            hidSSN_NO.Value = noder1.InnerText;
        //            //noder1.InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder1.InnerText);
        //            string strSSN_NO = string.Empty;
        //            try
        //            {
        //                if (noder1.InnerText.Trim() != "")
        //                {
        //                    strSSN_NO = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder1.InnerText);
        //                }
        //                if (strSSN_NO.Trim() != "")//Done for Itrack Issue 6063 on 7 July 09
        //                {
        //                    string strvaln = "xxx-xx-";
        //                    //for(var len=0; len < document.getElementById('txtSSN_NO').value.length-4; len++)
        //                    //	txtvaln += 'x';
        //                    strvaln += strSSN_NO.Substring(strvaln.Length, strSSN_NO.Length - strvaln.Length);
        //                    capSSN_NO_HID.Text = strvaln;
        //                }
        //                else
        //                    capSSN_NO_HID.Text = "";
        //            }
        //            catch
        //            {

        //            }
        //        }
				
        //        objxml = null;
				
        //    }
        //}



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
			catch //(Exception ex)
			{
				return false;
			}
		}

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsUserInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsUserInfo objUserInfo;
			objUserInfo = new ClsUserInfo();

            //Added by Charles on 24-May-10 for Itrack 91
            if (hidUSER_ID.Value == "New")
            {
                objUserInfo.USER_LOGIN_ID = txtUSER_LOGIN_ID.Text;
            }
            else
            {
                objUserInfo.USER_LOGIN_ID = hidLoginId.Value;
            }//Added till here
			objUserInfo.USER_TITLE		=	txtUSER_TITLE.Text;
			objUserInfo.USER_FNAME		=	txtUSER_FNAME.Text;
			objUserInfo.USER_LNAME		=	txtUSER_LNAME.Text;
			objUserInfo.USER_INITIALS	=	txtUSER_INITIALS.Text;
			objUserInfo.USER_ADD1		=	txtUSER_ADD1.Text;
			objUserInfo.USER_ADD2		=	txtUSER_ADD2.Text;
			objUserInfo.USER_CITY		=	txtUSER_CITY.Text;
			objUserInfo.USER_STATE		=	cmbUSER_STATE.SelectedValue;
			objUserInfo.USER_ZIP		=	txtUSER_ZIP.Text;
			objUserInfo.USER_PHONE		=	txtUSER_PHONE.Text;
			objUserInfo.USER_EXT		=	txtUSER_EXT.Text;
			objUserInfo.USER_FAX		=	txtUSER_FAX.Text;
			objUserInfo.USER_MOBILE		=	txtUSER_MOBILE.Text;
			objUserInfo.USER_NOTES		=	txtUSER_NOTES.Text;//**************//
			objUserInfo.USER_EMAIL		=	txtUSER_EMAIL.Text.Trim();
            if (cmbACTIVITY.SelectedItem != null && cmbACTIVITY.SelectedItem.Value != "")
            objUserInfo.ACTIVITY = int.Parse(cmbACTIVITY.SelectedItem.Value);
            //objUserInfo.REG_ID_ISSUE_DATE = Convert.ToDateTime(txtREG_ID_ISSUE_DATE.Text.Trim());//REGIONAL_ID_ISSUE_DATE
            objUserInfo.REG_ID_ISSUE = txtREG_ID_ISSUE.Text.ToString();
            objUserInfo.REGIONAL_IDENTIFICATION = txtREGIONAL_IDENTIFICATION.Text.ToString();
            objUserInfo.CPF = txtCPF.Text.ToString();
            //if(txtSSN_NO.Text.Trim()!="")
            //{
            //    objUserInfo.SSN_NO			=	Cms.BusinessLayer.BlCommon.ClsCommon.EncryptString(txtSSN_NO.Text.Trim());
            //    txtSSN_NO.Text = "";
            //}
            //else
            //    objUserInfo.SSN_NO			= hidSSN_NO.Value;
            if (txtREG_ID_ISSUE_DATE.Text.Trim() != "")

                objUserInfo.REG_ID_ISSUE_DATE = Convert.ToDateTime(txtREG_ID_ISSUE_DATE.Text.Trim());

			if(txtDATE_OF_BIRTH.Text.Trim()!="")
				objUserInfo.DATE_OF_BIRTH	=	Convert.ToDateTime(txtDATE_OF_BIRTH.Text.Trim());
			if(txtDRIVER_LIC_NO.Text.Trim()!="")
				objUserInfo.DRIVER_LIC_NO=	txtDRIVER_LIC_NO.Text.Trim();
			if(txtDATE_EXPIRY.Text.Trim()!="")
				objUserInfo.DATE_EXPIRY=	Convert.ToDateTime(txtDATE_EXPIRY.Text.Trim());
			if(cmbLICENSE_STATUS.SelectedItem !=null)
			{
				objUserInfo.LICENSE_STATUS=	cmbLICENSE_STATUS.SelectedItem.Value ;
			}
			
			//Added by Sibin for Itrack Issue 4173 on 15 Jan 09
			if(txtNON_RESI_LICENSE_EXP_DATE.Text.Trim()!="")
				objUserInfo.NON_RESI_LICENSE_EXP_DATE=	Convert.ToDateTime(txtNON_RESI_LICENSE_EXP_DATE.Text.Trim());
			
			if(cmbNON_RESI_LICENSE_STATUS.SelectedItem !=null)
			{
				objUserInfo.NON_RESI_LICENSE_STATUS=	cmbNON_RESI_LICENSE_STATUS.SelectedItem.Value ;
			}

			if(txtNON_RESI_LICENSE_EXP_DATE2.Text.Trim()!="")
				objUserInfo.NON_RESI_LICENSE_EXP_DATE2=	Convert.ToDateTime(txtNON_RESI_LICENSE_EXP_DATE2.Text.Trim());
			
			if(cmbNON_RESI_LICENSE_STATUS2.SelectedItem !=null)
			{
				objUserInfo.NON_RESI_LICENSE_STATUS2=	cmbNON_RESI_LICENSE_STATUS2.SelectedItem.Value ;
			}

			//Added till here
			if(cmbNON_RESI_LICENSE_STATE.SelectedItem !=null)
			{
				objUserInfo.NON_RESI_LICENSE_STATE	=	cmbNON_RESI_LICENSE_STATE.SelectedItem.Value;
			}
			if(txtNON_RESI_LICENSE_NO.Text.Trim()!="")
				objUserInfo.NON_RESI_LICENSE_NO=	txtNON_RESI_LICENSE_NO.Text.Trim();
			objUserInfo.COUNTRY			=	int.Parse(cmbUSER_COUNTRY.SelectedValue.ToString());
            //ADDED BY Kuldeep not to save sequential code for Sub code if User is not an agency User
            if (strCalledFrom!= null)
            {
                if (txtSUB_CODE.Text.Trim() != "")
                    objUserInfo.SUB_CODE = txtSUB_CODE.Text;
            }
            else
            {
                    objUserInfo.SUB_CODE = "";
            }
		//**************Added BY Manoj Rathore (06 Nov 2006)*****************//	
			
			if(cmbNON_RESI_LICENSE_STATE2.SelectedItem !=null)
			{
				objUserInfo.NON_RESI_LICENSE_STATE2	=	cmbNON_RESI_LICENSE_STATE2.SelectedItem.Value;
			}
			if(txtNON_RESI_LICENSE_NO2.Text.Trim()!="")
			{
				objUserInfo.NON_RESI_LICENSE_NO2=	txtNON_RESI_LICENSE_NO2.Text.Trim();
			}

			

			//objUserInfo.COUNTRY			=	int.Parse(cmbUSER_COUNTRY.SelectedValue.ToString());
			//if(txtSUB_CODE.Text.Trim()!="")
			//	objUserInfo.SUB_CODE		=	txtSUB_CODE.Text;			
     	//*******************************		

			if(cmbDefault_Hierarchy.SelectedItem !=null)
			{
				strDefaultHeirarchy			=	cmbDefault_Hierarchy.SelectedValue.Trim().Split('_');
				objUserInfo.USER_DEF_DIV_ID	=	int.Parse(strDefaultHeirarchy[0].ToString());
				objUserInfo.USER_DEF_DEPT_ID=	int.Parse(strDefaultHeirarchy[1].ToString());
				objUserInfo.USER_DEF_PC_ID	=	int.Parse(strDefaultHeirarchy[2].ToString());
			}

			//*****************Added By Manoj Rathore (7th Nov 2006)*************
		/*	if(cmb_BRICS_USER.SelectedItem!=null && cmb_BRICS_USER.SelectedItem.Value!="")
			{
				objUserInfo.BRICS_USER = int.Parse(cmb_BRICS_USER.SelectedItem.Value);
			}
			
			if(objUserInfo.BRICS_USER != 10964) //No
			{
				objUserInfo.USER_LOGIN_ID	=	txtUSER_LOGIN_ID.Text;
				objUserInfo.USER_PWD		=	txtUSER_PWD.Text;
				if (cmbUSER_MGR_ID.SelectedItem!=null && cmbUSER_MGR_ID.SelectedValue.Trim() != "")
				{
					objUserInfo.USER_MGR_ID		=	int.Parse(cmbUSER_MGR_ID.SelectedValue.ToString());
				}
				if(cmbUSER_TYPE_ID.SelectedItem!=null && cmbUSER_TYPE_ID.SelectedItem.Value!="")
					objUserInfo.USER_TYPE_ID	=	int.Parse(cmbUSER_TYPE_ID.SelectedValue.ToString());
				if(chkUSER_SPR.Checked==true)
				{
					objUserInfo.USER_SPR		=	"Y";
				}
				else
				{
					objUserInfo.USER_SPR		=	"N";
				}
				if(cmbUSER_TIME_ZONE.SelectedItem!=null && cmbUSER_TIME_ZONE.SelectedItem.Value!="")
					objUserInfo.USER_TIME_ZONE	=	cmbUSER_TIME_ZONE.SelectedValue;
			}
			else
			{
				objUserInfo.USER_TYPE_ID = USER_TYPE_CSR;
				objUserInfo.USER_SPR		=	"N";
			}*/

			//*********************
			if(cmbLIC_BRICS_USER.SelectedItem!=null && cmbLIC_BRICS_USER.SelectedItem.Value!="")
			{
				objUserInfo.LIC_BRICS_USER = int.Parse(cmbLIC_BRICS_USER.SelectedItem.Value);
			}

			if(objUserInfo.LIC_BRICS_USER != 10964) //Yes
			{
                //Added by Charles on 24-May-10 for Itrack 91
                if (hidUSER_ID.Value == "New")
                {
                    objUserInfo.USER_LOGIN_ID = txtUSER_LOGIN_ID.Text;
                }
                else if (txtUSER_LOGIN_ID.Text != "")
                {
                    objUserInfo.USER_LOGIN_ID = txtUSER_LOGIN_ID.Text;
                }
                else
                {
                    objUserInfo.USER_LOGIN_ID = hidLoginId.Value;
                }//Added till here

				//Added by Sibin for Itrack Issue 5212 to remove USER_PWD and USER_CONFIRM_PWD from View Source - Done on 7 Jan 08
				if(hidPWD.Value!="")
				{
					objUserInfo.USER_PWD		=	Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(hidPWD.Value);
				}
				else
				{
					objUserInfo.USER_PWD		=	txtUSER_PWD.Text;
				}
				//Added till here
				//objUserInfo.USER_PWD		=	ClsCommon.EncryptString(txtUSER_PWD.Text);
				if (cmbUSER_MGR_ID.SelectedItem!=null && cmbUSER_MGR_ID.SelectedValue.Trim() != "")
				{
					objUserInfo.USER_MGR_ID		=	int.Parse(cmbUSER_MGR_ID.SelectedValue.ToString());
				}
				if(cmbUSER_TYPE_ID.SelectedItem!=null && cmbUSER_TYPE_ID.SelectedItem.Value!="")
				{
					objUserInfo.USER_TYPE_ID	=	int.Parse(cmbUSER_TYPE_ID.SelectedValue.ToString());
					if(objUserInfo.USER_TYPE_ID==46 && txtADJUSTER_CODE.Text.Trim()!="")
						objUserInfo.ADJUSTER_CODE = txtADJUSTER_CODE.Text.Trim();
				}
				
				if(chkUSER_SPR.Checked==true)
				{
					objUserInfo.USER_SPR		=	"Y";
				}
				else
				{
					objUserInfo.USER_SPR		=	"N";
				}					
				
				//Added by Sibin for Itrack Issue 4994 on 9 Dec 08
				if(cmbCHANGE_PWD_NEXT_LOGIN.SelectedItem!=null && cmbCHANGE_PWD_NEXT_LOGIN.SelectedItem.Value!="")
				{
					objUserInfo.CHANGE_PWD_NEXT_LOGIN		=	int.Parse(cmbCHANGE_PWD_NEXT_LOGIN.SelectedValue.ToString());
				}
				
				if(cmbUSER_LOCKED.SelectedItem!=null && cmbUSER_LOCKED.SelectedItem.Value!="")
				{
					objUserInfo.USER_LOCKED		=	cmbUSER_LOCKED.SelectedValue.ToString();
				}
				//Added till here

				if(cmbUSER_TIME_ZONE.SelectedItem!=null && cmbUSER_TIME_ZONE.SelectedItem.Value!="")
					objUserInfo.USER_TIME_ZONE	=	cmbUSER_TIME_ZONE.SelectedValue;
			}
			else
			{
				objUserInfo.USER_LOGIN_ID	 =	null;
				objUserInfo.USER_PWD		 =	null;
				objUserInfo.USER_MGR_ID		 =	0;
				objUserInfo.USER_TIME_ZONE	 =	null;
				objUserInfo.USER_NOTES		 = null;
				objUserInfo.USER_TYPE_ID	 =	0; //USER_TYPE_CSR - Done by Sibin for Itrack Issue 5139 on 3 Dec 08
				objUserInfo.USER_SPR		 =	"N";
				txtUSER_LOGIN_ID.Text		 =	null;
				txtUSER_PWD.Text			 =	null;
				cmbUSER_MGR_ID.SelectedValue =	null;
				cmbUSER_TYPE_ID.SelectedValue =  null;
				cmbUSER_TIME_ZONE.SelectedValue = null;
				txtUSER_NOTES.Text= null;
				//Added by Sibin for Itrack Issue 4994 on 9 Dec 08
				cmbCHANGE_PWD_NEXT_LOGIN.SelectedValue=null;
				cmbUSER_LOCKED.SelectedValue=null;
				//added till here
			}
			
			if(chkPINK_SLIP_NOTIFY.Checked==true)
			{
				objUserInfo.PINK_SLIP_NOTIFY		=	"Y";
			}
			else
			{
				objUserInfo.PINK_SLIP_NOTIFY		=	"N";
			}		
			
			objUserInfo.IS_ACTIVE		=	"Y";
			//objUserInfo.USER_COLOR_SCHEME=	cmbUSER_COLOR_SCHEME.SelectedValue.ToString();//GetColorScheme()==""?"1":GetColorScheme();
			// User Image folder set to "1" while creating the new user
			objUserInfo.USER_IMAGE_FOLDER = "Images1"; 
			objUserInfo.USER_COLOR_SCHEME = "1";
			objUserInfo.USER_SYSTEM_ID	=	hidAGENCY_CODE.Value==""?GetSystemId():hidAGENCY_CODE.Value;
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidUSER_ID.Value;
			oldXML		= hidOldData.Value;
			//Returning the model object

			return objUserInfo;
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
				//if(Page.IsValid == true)
				//{ 
					int intRetVal;	//For retreiving the return value of business class save function
					objUser = new  ClsUser();
					//Retreiving the form values into model class object
					ClsUserInfo objUserInfo = GetFormValue();

					if(strRowId.ToUpper().Equals("NEW")) //save case
					{
						objUserInfo.CREATED_BY = int.Parse(GetUserId());
						objUserInfo.CREATED_DATETIME = DateTime.Now;

						//Calling the add method of business layer class
						intRetVal = objUser.Add(objUserInfo);

						if(intRetVal>0)
						{
							hidUSER_ID.Value = objUserInfo.USER_ID.ToString();
							
							/* Commented by Charles on 3-Jul-09 as logic moved to Proc Level.
							if(objUserInfo.USER_TYPE_ID==13) //user is of underwritter type
							{
								ClsUser.SetUserIDForUnderwriter(UNDERWRITER_USER_TYPE_ID);
							}
							*/

							lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
							hidFormSaved.Value			=	"1";
							if(cmbLIC_BRICS_USER.SelectedValue.Equals("10963"))
								txtUSER_LOGIN_ID.ReadOnly = true;
							hidIS_ACTIVE.Value = "Y";
							SetXml(hidUSER_ID.Value);
						}
						else if(intRetVal == -4)
						{
							lblMessage.Text				=		ClsMessages.FetchGeneralMessage("987");
							hidFormSaved.Value			=		"2";
						}
						else if(intRetVal == -3)
						{
							lblMessage.Text				=		ClsMessages.FetchGeneralMessage("853");
							hidFormSaved.Value			=		"2";
						}
						else if(intRetVal == -2)
						{
							lblMessage.Text				=		ClsMessages.FetchGeneralMessage("495");
							hidFormSaved.Value			=		"2";
						}
                        //Added by Charles on 21-May-10 for Itrack 91
						else if(intRetVal == -5)
						{
							lblMessage.Text				=		ClsMessages.FetchGeneralMessage("497") ;
							hidFormSaved.Value			=		"2";
						}//Added till here
                        //Commented by Charles on 21-May-10 for Itrack 91
                        //else if (intRetVal == -1)
                        //{
                        //    lblMessage.Text = ClsMessages.FetchGeneralMessage("497");
                        //    hidFormSaved.Value = "2";
                        //}
						else
						{
							lblMessage.Text			=	ClsMessages.FetchGeneralMessage("20");
							hidFormSaved.Value			=	"2";
						}
						lblMessage.Visible = true;
						//txtUSER_LOGIN_ID.ReadOnly=true;

					} // end save case  
                 
					else //UPDATE CASE
					{

						//Creating the Model object for holding the Old data
						ClsUserInfo objOldUserInfo;
						objOldUserInfo = new ClsUserInfo();
					
						//Setting  the Old Page details(XML File containing old details) into the Model Object
						base.PopulateModelObject(objOldUserInfo,hidOldData.Value);

						//Setting those values into the Model object which are not in the page
						//					if(GetUserId().ToString()!=strRowId)
						//					{
						//						lblMessage.Text			=	"not auth";
						//					}
						//					else
						//					{
						objUserInfo.USER_ID = int.Parse(strRowId);
						objUserInfo.MODIFIED_BY = int.Parse(GetUserId());
						objUserInfo.LAST_UPDATED_DATETIME = DateTime.Now;
						objUserInfo.IS_ACTIVE = hidIS_ACTIVE.Value;
						//Updating the record using business layer class object
						intRetVal	= objUser.Update(objOldUserInfo,objUserInfo);
						if( intRetVal > 0 )			// update successfully performed
						{
						
							//Commented by Charles on 3-Jul-09 as logic moved to Proc Level.
							//ClsUser.SetUserIDForUnderwriter(UNDERWRITER_USER_TYPE_ID);
							
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"31");
							hidFormSaved.Value		=	"1";
							if(cmbLIC_BRICS_USER.SelectedValue.Equals("10964"))
								txtUSER_LOGIN_ID.ReadOnly = false;
							SetXml(hidUSER_ID.Value);
						}
						else if(intRetVal == -4)
						{
							lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"987");
							hidFormSaved.Value			=		"2";
						}
						else if(intRetVal == -3)
						{
							lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"853");
							hidFormSaved.Value			=		"2";
						}
						else if(intRetVal == -2)	// Duplicate sub code exist, update failed
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"495");
							hidFormSaved.Value		=	"2";
						}
						else if(intRetVal == -1)	// Duplicate code exist, update failed
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
							hidFormSaved.Value		=	"2";
						}
						else if(intRetVal == 0)	// Duplicate code exist, update failed
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("497");
							hidFormSaved.Value		=	"2";
						}
						else 
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"20");
							hidFormSaved.Value		=	"2";
						}
						//					}					
						lblMessage.Visible = true;

					}
				//}
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				//Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objUser!= null)
					objUser.Dispose();
			}
		}

		/// <summary>
		/// Activates and deactivates  .
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			//ClsUserInfo objUserInfo = new ClsUserInfo();
			ClsUserInfo objUserInfo = GetFormValue();

			ClsUser objUser = new ClsUser();
			int returnResult = 1;
			try
			{
				objUserInfo.USER_ID = int.Parse(hidUSER_ID.Value);
				objUserInfo.USER_SYSTEM_ID = hidAGENCY_CODE.Value;
				objUserInfo.MODIFIED_BY = int.Parse(GetUserId());				
				/*Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				objUser =  new ClsUser();*/

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					//objStuTransactionInfo.transactionDescription = "Deactivated Succesfully.";
					//objUser.TransactionInfoParams = objStuTransactionInfo;
					//objUser.ActivateDeactivate(hidUSER_ID.Value,"N");
					returnResult = objUser.ActivateDeactivateUser(objUserInfo,"N");
					if(returnResult>0)
					{
						lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
						hidIS_ACTIVE.Value="N";
						hidFormSaved.Value			=	"1";
					}
					else if(returnResult == -2)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"853");
						hidFormSaved.Value			=		"2";
					}					
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}
				}
				else
				{
					//objStuTransactionInfo.transactionDescription = "Activated Succesfully.";
					//objUser.TransactionInfoParams = objStuTransactionInfo;
					returnResult = objUser.ActivateDeactivateUser(objUserInfo,"Y");
					if(returnResult>0)
					{
						lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
						hidIS_ACTIVE.Value="Y";
						hidFormSaved.Value			=	"1";
					}
					else if(returnResult == -2)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"853");
						hidFormSaved.Value			=		"2";
					}					
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}
				}
				SetXml(hidUSER_ID.Value.ToString());
				hidReset.Value="1";
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
				if(objUser!= null)
					objUser.Dispose();
			}
		}
		#endregion

		private void SetCaptions()
		{
			capUSER_LOGIN_ID.Text					=		objResourceMgr.GetString("txtUSER_LOGIN_ID");
			capUSER_TYPE.Text						=		objResourceMgr.GetString("lblStatus");
			capUSER_PWD.Text						=		objResourceMgr.GetString("txtUSER_PWD");
			capUSER_TITLE.Text						=		objResourceMgr.GetString("txtUSER_TITLE");
			capUSER_FNAME.Text						=		objResourceMgr.GetString("txtUSER_FNAME");
			capUSER_LNAME.Text						=		objResourceMgr.GetString("txtUSER_LNAME");
			capUSER_INITIALS.Text					=		objResourceMgr.GetString("txtUSER_INITIALS");
			capUSER_ADD1.Text						=		objResourceMgr.GetString("txtUSER_ADD1");
			capUSER_ADD2.Text						=		objResourceMgr.GetString("txtUSER_ADD2");
			capUSER_CITY.Text						=		objResourceMgr.GetString("txtUSER_CITY");
			capUSER_STATE.Text						=		objResourceMgr.GetString("cmbUSER_STATE");
			capUSER_ZIP.Text						=		objResourceMgr.GetString("txtUSER_ZIP");
			capUSER_PHONE.Text						=		objResourceMgr.GetString("txtUSER_PHONE");
			capUSER_EXT.Text						=		objResourceMgr.GetString("txtUSER_EXT");
			capUSER_FAX.Text						=		objResourceMgr.GetString("txtUSER_FAX");
			capUSER_MOBILE.Text						=		objResourceMgr.GetString("txtUSER_MOBILE");
			capUSER_EMAIL.Text						=		objResourceMgr.GetString("txtUSER_EMAIL");
			capUSER_SPR.Text						=		objResourceMgr.GetString("chkUSER_SPR");			
			capPINK_SLIP_NOTIFY.Text				=		objResourceMgr.GetString("chkPINK_SLIP_NOTIFY");
			capUSER_MGR_ID.Text						=		objResourceMgr.GetString("cmbUSER_MGR_ID");
			capUSER_TYPE_ID.Text					=		objResourceMgr.GetString("cmbUSER_TYPE_ID");
			//capUSER_DEF_DEPT_ID.Text				=		objResourceMgr.GetString("cmbUSER_DEF_DEPT_ID");
			//capUSER_DEF_PC_ID.Text				=		objResourceMgr.GetString("cmbUSER_DEF_PC_ID");
			capUSER_TIME_ZONE.Text					=		objResourceMgr.GetString("cmbUSER_TIME_ZONE");
			capUSER_NOTES.Text						=		objResourceMgr.GetString("txtUSER_NOTES");//****
			//cap_BRICS_USER.Text						=		objResourceMgr.GetString("cmb_BRICS_USER");//****
			//capIS_ACTIVE.Text						=		objResourceMgr.GetString("txtIS_ACTIVE");
			//capUSER_COLOR_SCHEME.Text				=		objResourceMgr.GetString("cmbUSER_COLOR_SCHEME");
            //capSSN_NO.Text							=		objResourceMgr.GetString("txtSSN_NO");
			capDATE_OF_BIRTH.Text					=		objResourceMgr.GetString("txtDATE_OF_BIRTH");
			capDRIVER_LIC_NO.Text					=		objResourceMgr.GetString("txtDRIVER_LIC_NO");
			capDATE_EXPIRY.Text						=		objResourceMgr.GetString("txtDATE_EXPIRY");
			capLICENSE_STATUS.Text					=		objResourceMgr.GetString("cmbLICENSE_STATUS");
			capNON_RESI_LICENSE_NO.Text				=		objResourceMgr.GetString("txtNON_RESI_LICENSE_NO");
			capNON_RESI_LICENSE_STATE.Text			=		objResourceMgr.GetString("cmbNON_RESI_LICENSE_STATE");

			capNON_RESI_LICENSE_NO2.Text			=		objResourceMgr.GetString("txtNON_RESI_LICENSE_NO2");
			capNON_RESI_LICENSE_STATE2.Text			=		objResourceMgr.GetString("cmbNON_RESI_LICENSE_STATE2");
			capADJUSTER_CODE.Text					=		objResourceMgr.GetString("txtADJUSTER_CODE");
			//Added by Sibin for Itrack Issue 4994 on 9 Dec 08
			capCHANGE_PWD_NEXT_LOGIN.Text					=		objResourceMgr.GetString("cmbCHANGE_PWD_NEXT_LOGIN");
			capUSER_LOCKED.Text						=		objResourceMgr.GetString("cmbUSER_LOCKED");
			//added till here
            capLIC_BRICS_USER.Text = objResourceMgr.GetString("cmbLIC_BRICS_USER");
			//Added by Sibin for Itrack Issue 4173 on 15 Jan 09

			capNON_RESI_LICENSE_EXP_DATE.Text    =		objResourceMgr.GetString("txtNON_RESI_LICENSE_EXP_DATE");
			capNON_RESI_LICENSE_STATUS.Text			=		objResourceMgr.GetString("cmbNON_RESI_LICENSE_STATUS");
			capNON_RESI_LICENSE_EXP_DATE2.Text   =		objResourceMgr.GetString("txtNON_RESI_LICENSE_EXP_DATE2");
			capNON_RESI_LICENSE_STATUS2.Text		=		objResourceMgr.GetString("cmbNON_RESI_LICENSE_STATUS2");
			//Added till here
            capUSER_COUNTRY.Text = objResourceMgr.GetString("capUSER_COUNTRY"); 
            capUSER_CONFIRM_PWD.Text = objResourceMgr.GetString("capUSER_CONFIRM_PWD"); 
            capDefault_Hierarchy.Text = objResourceMgr.GetString("capDefault_Hierarchy"); 
            capCPF.Text = objResourceMgr.GetString("txtCPF");
            capACTIVITY.Text = objResourceMgr.GetString("cmbACTIVITY");
            capREGIONAL_ID.Text = objResourceMgr.GetString("txtREGIONAL_IDENTIFICATION");
            capREGIONAL_ID_ISSUE_DATE.Text = objResourceMgr.GetString("txtREG_ID_ISSUE_DATE");
            capREGIONAL_ID_ISSUE.Text = objResourceMgr.GetString("txtREG_ID_ISSUE");
            hidState.Value = objResourceMgr.GetString("hidState");
            btnCopyAddress.Text = objResourceMgr.GetString("btnCopyAddress");
            hidZipeCodeVerificationMsg.Value = objResourceMgr.GetString("hidZipeCodeVerificationMsg");// changed by praveer for itrack no 1553/TFS# 626 
            capSUB_CODE.Text=objResourceMgr.GetString("txtSUB_CODE");
		}
		[Ajax.AjaxMethod()]
		public string AjaxFetchZipForState(int stateID, string ZipID)
		{
			CmsWeb.webservices.ClsWebServiceCommon obj =  new CmsWeb.webservices.ClsWebServiceCommon();
			string result = "";
			result = obj.FetchZipForState(stateID,ZipID);
			return result;
			
		}
        [System.Web.Services.WebMethod] // changed by praveer for itrack no 1553/TFS# 626 
        public static String GetUserAddressDetailsUsingZipeCode(String ZIPCODE, String COUNTRYID)
        {

            CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();  //Declare the object ob common webservice

            String ReturnValue = String.Empty;
            DataSet ds = new DataSet();

            ReturnValue = obj.GetAddressDetailsBasedOnZipeCode(ZIPCODE, Convert.ToInt32(COUNTRYID));


            return ReturnValue;
        }
        
		private void GetOldDataXML()
		{

			if ( Request.Params.Count != 0 ) 
			{
			}
			else 
			{
			}
		}

		
	
	}
}
