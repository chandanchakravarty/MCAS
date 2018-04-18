/******************************************************************************************
	<Author					: - > HARMANJEET SINGH
	<Start Date				: -	>APRIL 20, 2007
	<End Date				: - >
	<Description			: - > 
	<Review Date			: - >
	<Reviewed By			: - >
	
Modification History

	<Modified Date			: - > 
	<Modified By			: - > 
	<Purpose				: - >
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
using System.Resources;
using System.Reflection;
using System.Xml;
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher;
using Cms.Model.Maintenance.Reinsurance;

namespace Cms.CmsWeb.Maintenance
{
    /// <summary>
    /// Summary description for AddReinsurer.
    /// </summary>
    public class AddReinsurer : Cms.CmsWeb.cmsbase
    {
        //string CalledFrom;
        //protected System.Web.UI.WebControls.Label lblDelete;

        #region DECLARATION


        ClsReinsurer objReinsurer;
        System.Resources.ResourceManager objResourceMgr;
        private string strRowId, strFormSaved;
        //string oldXML;
        private string NewscreenId;
       // protected System.Web.UI.HtmlControls.HtmlInputHidden hidREIN_COMPANY_CODE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidREIN_COMPANY_ID;
        #region AUTOMATIC CONTROL
        protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
        protected System.Web.UI.WebControls.Label lblDelete;
        protected System.Web.UI.WebControls.Label lblMessage;

        protected System.Web.UI.WebControls.Label capREIN_COMAPANY_NAME;
        protected System.Web.UI.WebControls.TextBox txtREIN_COMAPANY_NAME;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_COMAPANY_NAME;
        protected System.Web.UI.WebControls.Label capREIN_COMAPANY_CODE;
        protected System.Web.UI.WebControls.TextBox txtREIN_COMAPANY_CODE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_COMAPANY_CODE;
        protected System.Web.UI.WebControls.Label capREIN_COMAPANY_TYPE;
        protected System.Web.UI.WebControls.DropDownList cmbREIN_COMAPANY_TYPE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_COMAPANY_TYPE;
        protected System.Web.UI.WebControls.Label capREIN_COMAPANY_ADD1;
        protected System.Web.UI.WebControls.TextBox txtREIN_COMAPANY_ADD1;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_COMAPANY_ADD1;
        protected System.Web.UI.WebControls.Label capREIN_COMAPANY_ADD2;
        protected System.Web.UI.WebControls.Label capMAILING; //SNEHA
        protected System.Web.UI.WebControls.Label capphysical; //sneha
        protected System.Web.UI.WebControls.Label capOTHER; //sneha
        protected System.Web.UI.WebControls.TextBox txtREIN_COMAPANY_ADD2;
        protected System.Web.UI.WebControls.Label capREIN_COMAPANY_CITY;
        protected System.Web.UI.WebControls.TextBox txtREIN_COMAPANY_CITY;
        protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_COMAPANY_CITY;
        protected System.Web.UI.WebControls.RegularExpressionValidator revMOBILE;
        protected System.Web.UI.WebControls.Label capREIN_COMAPANY_COUNTRY;
        protected System.Web.UI.WebControls.DropDownList cmbREIN_COMAPANY_COUNTRY_SIN;
        protected System.Web.UI.WebControls.TextBox txtREIN_COMAPANY_COUNTRY;
        protected System.Web.UI.WebControls.Label capREIN_COMAPANY_STATE;
        //		protected System.Web.UI.WebControls.DropDownList cmbREIN_COMAPANY_STATE;
        protected System.Web.UI.WebControls.TextBox txtREIN_COMAPANY_STATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_COMAPANY_STATE;
        protected System.Web.UI.WebControls.Label capREIN_COMAPANY_ZIP;
        protected System.Web.UI.WebControls.TextBox txtREIN_COMAPANY_ZIP;
        protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_COMAPANY_ZIP;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_COMAPANY_ZIP;
        protected System.Web.UI.WebControls.Label capREIN_COMAPANY_PHONE;
        protected System.Web.UI.WebControls.TextBox txtREIN_COMAPANY_PHONE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_COMAPANY_PHONE;
        protected System.Web.UI.WebControls.Label capREIN_COMAPANY_EXT;
        protected System.Web.UI.WebControls.TextBox txtREIN_COMAPANY_EXT;
        protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_COMAPANY_EXT;
        protected System.Web.UI.WebControls.Label capREIN_COMAPANY_FAX;
        protected System.Web.UI.WebControls.TextBox txtREIN_COMAPANY_FAX;
        protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_COMAPANY_FAX;
        protected System.Web.UI.WebControls.Label capM_REIN_COMPANY_ADD_1;
        protected System.Web.UI.WebControls.TextBox txtM_REIN_COMPANY_ADD_1;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvM_REIN_COMPANY_ADD_1;
        protected System.Web.UI.WebControls.Label capM_RREIN_COMPANY_ADD_2;
        protected System.Web.UI.WebControls.TextBox txtM_RREIN_COMPANY_ADD_2;
        protected System.Web.UI.WebControls.Label capM_REIN_COMPANY_CITY;
        protected System.Web.UI.WebControls.TextBox txtM_REIN_COMPANY_CITY;
        protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_COMPANY_CITY;
        protected System.Web.UI.WebControls.Label capM_REIN_COMPANY_COUNTRY;
     	protected System.Web.UI.WebControls.DropDownList cmbM_REIN_COMPANY_COUNTRY_SIN;
        protected System.Web.UI.WebControls.TextBox txtM_REIN_COMPANY_COUNTRY;
        protected System.Web.UI.WebControls.Label capM_REIN_COMPANY_STATE;
        //		protected System.Web.UI.WebControls.DropDownList cmbM_REIN_COMPANY_STATE;
        protected System.Web.UI.WebControls.TextBox txtM_REIN_COMPANY_STATE;
        protected System.Web.UI.WebControls.Label capM_REIN_COMPANY_ZIP;
        protected System.Web.UI.WebControls.TextBox txtM_REIN_COMPANY_ZIP;
        protected System.Web.UI.WebControls.HyperLink hlkMZipLookup;
        protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_COMPANY_ZIP;
        protected System.Web.UI.WebControls.Label capM_REIN_COMPANY_PHONE;
        protected System.Web.UI.WebControls.TextBox txtM_REIN_COMPANY_PHONE;
    	protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_COMPANY_PHONE;//Added by Amit for tfs bug  #833
        protected System.Web.UI.WebControls.Label capM_REIN_COMPANY_EXT;
        protected System.Web.UI.WebControls.TextBox txtM_REIN_COMPANY_EXT;
        protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_COMPANY_EXT;
        protected System.Web.UI.WebControls.Label capM_REIN_COMPANY_FAX;
        protected System.Web.UI.WebControls.TextBox txtM_REIN_COMPANY_FAX;
    		protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_COMPANY_FAX;
        protected System.Web.UI.WebControls.Label capREIN_COMAPANY_MOBILE;
        protected System.Web.UI.WebControls.TextBox txtREIN_COMAPANY_MOBILE;
        		protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_COMAPANY_MOBILE;
        protected System.Web.UI.WebControls.Label capREIN_COMAPANY_EMAIL;
        protected System.Web.UI.WebControls.TextBox txtREIN_COMAPANY_EMAIL;
        protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_COMAPANY_EMAIL;
        protected System.Web.UI.WebControls.Label capREIN_COMPANY_WEBSITE;
        protected System.Web.UI.WebControls.TextBox txtREIN_COMPANY_WEBSITE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_COMPANY_WEBSITE;
        protected System.Web.UI.WebControls.Label capREIN_COMPANY_IS_BROKER;
        protected System.Web.UI.WebControls.DropDownList cmbREIN_COMPANY_IS_BROKER;
        protected System.Web.UI.WebControls.Label capPRINCIPAL_CONTACT;
        protected System.Web.UI.WebControls.TextBox txtPRINCIPAL_CONTACT;
        protected System.Web.UI.WebControls.Label capOTHER_CONTACT;
        protected System.Web.UI.WebControls.TextBox txtOTHER_CONTACT;
        protected System.Web.UI.WebControls.Label capFEDERAL_ID;
        protected System.Web.UI.WebControls.TextBox txtFEDERAL_ID;
        protected System.Web.UI.WebControls.RegularExpressionValidator revFEDERAL_ID;
        protected System.Web.UI.WebControls.RegularExpressionValidator revNAIC_CODE;
        protected System.Web.UI.WebControls.Label capNAIC_CODE;
        //		protected System.Web.UI.WebControls.DropDownList cmbNAIC_CODE;
        protected System.Web.UI.WebControls.TextBox txtNAIC_CODE;
        //		protected System.Web.UI.WebControls.RegularExpressionValidator revNAIC_CODE;
        protected System.Web.UI.WebControls.Label capAM_BEST_RATING;
        protected System.Web.UI.WebControls.Label capEFFECTIVE_DATE;
        protected System.Web.UI.WebControls.HyperLink hlkEFFECTIVE_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_DATE;
        protected System.Web.UI.WebControls.Label capTERMINATION_DATE;
        protected System.Web.UI.WebControls.TextBox txtTERMINATION_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revTERMINATION_DATE;
        protected System.Web.UI.WebControls.Label capTERMINATION_REASON;
        protected System.Web.UI.WebControls.TextBox txtTERMINATION_REASON;
        protected System.Web.UI.WebControls.Label capCOMMENTS;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidREIN_COMAPANY_ID;
        protected System.Web.UI.WebControls.HyperLink hlkTERMINATION_DATE;
        //added By Chetna
        protected System.Web.UI.WebControls.Label capSUSEP_NUM;
        protected System.Web.UI.WebControls.TextBox txtSUSEP_NUM;
        protected System.Web.UI.WebControls.Label capCOM_TYPE;
        protected System.Web.UI.WebControls.DropDownList cmbCOM_TYPE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revBANK_BRANCH;
        
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvACCOUNT_TYPE;
        protected System.Web.UI.WebControls.DropDownList cmbACCOUNT_TYPE;
        protected System.Web.UI.WebControls.Label capBANK_NUMBER;
        protected System.Web.UI.WebControls.Label capDISTRICT;
        protected System.Web.UI.WebControls.Label capBANK_BRANCH;
        protected System.Web.UI.WebControls.Label capACCOUNT_TYPE;
        protected System.Web.UI.WebControls.Label capACCOUNT_NUMBER;
        protected System.Web.UI.WebControls.Label capCARRIER_CNPJ;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCARRIER_CNPJ;
        public string  javasciptmsg,  javasciptCNPJmsg,CPF_invalid,  CNPJ_invalid;
        protected System.Web.UI.WebControls.TextBox txtDISTRICT;
        protected System.Web.UI.WebControls.TextBox txtBANK_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtBANK_BRANCH;
        protected System.Web.UI.WebControls.DropDownList cmbPAYMENT_METHOD;
        protected System.Web.UI.WebControls.TextBox txtCARRIER_CNPJ;
        protected System.Web.UI.WebControls.Label capPAYMENT_METHOD;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAYMENT_METHOD;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvBANK_NUMBER;
       

        //added by abhinav
        protected System.Web.UI.WebControls.TextBox txtAGENCY_CLASSIFICATION;
        protected System.Web.UI.WebControls.Label capAGENCY_CLASSIFICATION;
        protected System.Web.UI.WebControls.TextBox txtRISK_CLASSIFICATION;
        protected System.Web.UI.WebControls.Label capRISK_CLASSIFICATION;
       
        # endregion AUTOMATIC CONTROL
        #region BUTTON CONTROL
        protected Cms.CmsWeb.Controls.CmsButton btnCopyPhysicalAddress;
        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected RequiredFieldValidator rfvREIN_COMAPANY_TYPE;
        protected RequiredFieldValidator rfvTERMINATION_DATE;
       // protected CompareValidator cpvFINAL_DATE;
        protected CompareValidator cpvTERMINATION_DATE;// Added by amit for tfs bug# 833
        # endregion
        # region IMAGE CONTROL
        //		protected System.Web.UI.WebControls.Image imgZipLookup;
        //		protected System.Web.UI.WebControls.Image imgMZipLookup;

        # endregion

        # region HYPERLINK CONTROL
        protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
        # endregion
        #region HIDDEN VARIABLES
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
        protected System.Web.UI.WebControls.Label lblCopy_Address;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidReset;
        protected System.Web.UI.WebControls.Label capREIN_COMAPANY_NOTE;
        protected System.Web.UI.WebControls.TextBox txtREIN_COMAPANY_NOTE;
        protected System.Web.UI.WebControls.Label capREIN_COMAPANY_ACC_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtREIN_COMAPANY_ACC_NUMBER;
        //		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_COMAPANY_ACC_NUMBER;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvFEDERAL_ID;
        protected System.Web.UI.WebControls.Label capDOMICILED_STATE;
        protected System.Web.UI.WebControls.TextBox txtDOMICILED_STATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDOMICILED_STATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvNAIC_CODE;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvAM_BEST_RATING;
        protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_DATE;
        protected System.Web.UI.WebControls.Label capREIN_COMPANY_SPEED_DIAL;
        protected System.Web.UI.WebControls.TextBox txtREIN_COMPANY_SPEED_DIAL;
        protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_COMPANY_SPEED_DIAL;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_DATE;
        protected System.Web.UI.WebControls.TextBox txtAM_BEST_RATING;
        protected System.Web.UI.WebControls.RegularExpressionValidator revAM_BEST_RATING;
        protected System.Web.UI.WebControls.TextBox txtCOMMENTS;
        protected System.Web.UI.WebControls.RegularExpressionValidator revACCOUNT_NUMBER;
        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        //		protected System.Web.UI.WebControls.RequiredFieldValidator rfvM_REIN_COMPANY_CITY;
        //		protected System.Web.UI.WebControls.RequiredFieldValidator rfvM_REIN_COMPANY_COUNTRY;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_COMAPANY_COUNTRY;
        //		protected System.Web.UI.WebControls.RequiredFieldValidator rfvM_REIN_COMPANY_STATE;
        //		protected System.Web.UI.WebControls.RequiredFieldValidator rfvM_REIN_COMPANY_ZIP;

        protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_COMAPANY_PHONE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_COMAPANY_EXT;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_COMAPANY_FAX;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvM_REIN_COMPANY_PHONE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvM_REIN_COMPANY_EXT;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvM_REIN_COMPANY_FAX;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvREIN_COMAPANY_MOBILE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvSUSEP_NUM; //Changed by Aditya for TFS bug # 923

        protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_COMAPANY_PHONE_CHECK;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_COMAPANY_EXT_CHECK;    changed by amit for tfs bug# 833
        // protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_COMAPANY_FAX_CHECK;    changed by amit for tfs bug# 833
        // protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_COMPANY_PHONE_CHECK;  changed by amit for tfs bug# 833
       // protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_COMPANY_EXT_CHECK;
       // protected System.Web.UI.WebControls.RegularExpressionValidator revM_REIN_COMPANY_FAX_CHECK;
       // protected System.Web.UI.WebControls.RegularExpressionValidator revREIN_COMAPANY_MOBILE_CHECK;
        protected System.Web.UI.WebControls.RegularExpressionValidator revSUSEP_NUM;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFEDERAL_ID;
        protected System.Web.UI.WebControls.Label capFEDERAL_ID_HID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTAB_TITLES;
        protected System.Web.UI.WebControls.Label capMessages;
        

        # endregion

        # endregion DECELARATION

        # region HARMANJEET SINGH
        #region Set Validators ErrorMessages
        /// <summary>
        /// Method to set validation control error masessages.
        /// Parameters: none
        /// Return Type: none
        /// </summary>
        private void SetErrorMessages()
        {
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            rfvREIN_COMAPANY_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("935");
            rfvREIN_COMAPANY_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("992");
            rfvREIN_COMAPANY_ADD1.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("909");
            rfvM_REIN_COMPANY_ADD_1.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("909");
           
            //			rfvREIN_COMAPANY_ACC_NUMBER.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage("G","628");
            //rfvFEDERAL_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "10"); //"Please enter Federal ID Number"; //SNEHA
           // rfvDOMICILED_STATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "15"); //"Please enter Domiciled State"; //sneha
           // rfvNAIC_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "11");//"Please enter NAIC Code";  //SNEHA
            //rfvAM_BEST_RATING.ErrorMessage				=	"Please enter A.M. Best Rating";
            rfvEFFECTIVE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "13"); //"Please enter Effective Date";//SNEHA

            revFEDERAL_ID.ValidationExpression = aRegExpFederalID;
            //revFEDERAL_ID.ErrorMessage				=		  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"24");


            revREIN_COMAPANY_CITY.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("39");
            revREIN_COMAPANY_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1084");
            //			revREIN_COMAPANY_PHONE.ErrorMessage     	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
            //			revREIN_COMAPANY_EXT.ErrorMessage     	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");

            //revREIN_COMPANY_SPEED_DIAL.ErrorMessage   	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("25");
            //			revREIN_COMAPANY_FAX.ErrorMessage	    	= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("15");

            revREIN_COMAPANY_CITY.ValidationExpression = aRegExpClientName;
            revREIN_COMAPANY_ZIP.ValidationExpression = aRegExpZipBrazil;
            //			revREIN_COMAPANY_PHONE.ValidationExpression=aRegExpPhone;
            //			revREIN_COMAPANY_EXT.ValidationExpression=aRegExpPhone;

            revREIN_COMAPANY_PHONE.ValidationExpression = aRegExpAgencyPhone;
            //revREIN_COMAPANY_EXT_CHECK.ValidationExpression = aRegExpPhoneBrazil;     changed by amit for tfs bug# 833
            revREIN_COMAPANY_EXT.ValidationExpression = aRegExpAgencyPhone;
            //revREIN_COMAPANY_FAX_CHECK.ValidationExpression = aRegExpPhoneBrazil;
            revREIN_COMAPANY_FAX.ValidationExpression = aRegExpAgencyPhone;             //added by Amit for tfs bug #833
            //revM_REIN_COMPANY_PHONE_CHECK.ValidationExpression = aRegExpPhoneBrazil;
            revM_REIN_COMPANY_PHONE.ValidationExpression = aRegExpAgencyPhone;//Added by Amit for tfs bug #833

            //revM_REIN_COMPANY_EXT_CHECK.ValidationExpression = aRegExpPhoneBrazil;
            revM_REIN_COMPANY_EXT.ValidationExpression =aRegExpAgencyPhone; //Changed by amit for tfs bug#833
            revM_REIN_COMPANY_FAX.ValidationExpression = aRegExpAgencyPhone; //Changed by amit for tfs bug#833
            // revM_REIN_COMPANY_FAX_CHECK.ValidationExpression = aRegExpPhoneBrazil;//Changed by amit for tfs bug#833
           // revREIN_COMAPANY_MOBILE_CHECK.ValidationExpression = aRegExpPhoneAll;
            //revREIN_COMAPANY_MOBILE_CHECK.ValidationExpression = aRegExpPhoneBrazil;
            revREIN_COMAPANY_MOBILE.ValidationExpression = aRegExpAgencyPhone;
            this.revREIN_COMAPANY_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083");
            //this.revREIN_COMAPANY_EXT_CHECK.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083");changed by amit for tfs bug# 833
            this.revREIN_COMAPANY_EXT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083");
            
            //this.revREIN_COMAPANY_FAX_CHECK.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1085");//Changed by amit for tfs bug #833
            this.revREIN_COMAPANY_FAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1085");//Added by amit for tfs bug #833
           // this.revM_REIN_COMPANY_PHONE_CHECK.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083");
            this.revM_REIN_COMPANY_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083");//Added by amit for tfs bug #833
            this.revM_REIN_COMPANY_EXT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083");
            // this.revM_REIN_COMPANY_EXT_CHECK.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083");//Added by amit for tfs bug #833
            
            //this.revM_REIN_COMPANY_FAX_CHECK.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1085");
            this.revM_REIN_COMPANY_FAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1085");
            //this.revREIN_COMAPANY_MOBILE_CHECK.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("999");
           
            //this.revREIN_COMAPANY_MOBILE_CHECK.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1086");
            this.revREIN_COMAPANY_MOBILE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1086");

            rfvREIN_COMAPANY_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1007");
            rfvREIN_COMAPANY_EXT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1008");
            rfvREIN_COMAPANY_FAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("138");
            rfvM_REIN_COMPANY_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1007");
            rfvM_REIN_COMPANY_EXT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1008");
            rfvM_REIN_COMPANY_FAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("138");
           // rfvREIN_COMAPANY_MOBILE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("60");

            //revREIN_COMPANY_SPEED_DIAL.ValidationExpression=aRegExpExtn;
            //			revREIN_COMAPANY_FAX.ValidationExpression = aRegExpFax;

            revM_REIN_COMPANY_CITY.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("39");
            //			revM_REIN_COMPANY_CITY.ErrorMessage      = "Please enter valid city name";//Cms.CmsWeb.ClsMessages.FetchGeneralMessage("39");
            revM_REIN_COMPANY_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1084");
            //revM_REIN_COMPANY_PHONE.ErrorMessage     = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
            //revM_REIN_COMPANY_EXT.ErrorMessage     = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
            //revM_REIN_COMPANY_SPEED_DIAL.ErrorMessage       = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("25");
            //			revM_REIN_COMPANY_FAX.ErrorMessage	    = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("15");

            revM_REIN_COMPANY_CITY.ValidationExpression = aRegExpClientName;
            //			revM_REIN_COMPANY_CITY.ValidationExpression 	=	"^[a-zA-Z0-9]*"; //aRegExpClientName;
            revM_REIN_COMPANY_ZIP.ValidationExpression = aRegExpZipBrazil;
            //revM_REIN_COMPANY_PHONE.ValidationExpression	=	aRegExpPhone;
            //revM_REIN_COMPANY_EXT.ValidationExpression		=	aRegExpPhone;
            //revM_REIN_COMPANY_SPEED_DIAL.ValidationExpression	    =	aRegExpExtn;
            //revM_REIN_COMPANY_FAX.ValidationExpression	    =	aRegExpFax;

            //revREIN_COMAPANY_MOBILE.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("16");
            revREIN_COMAPANY_EMAIL.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("124");
            revREIN_COMPANY_WEBSITE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("737");
            revTERMINATION_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            revEFFECTIVE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            revSUSEP_NUM.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");

            //revEFFECTIVE_DATE.ErrorMessage	    =	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");

            //	revREIN_COMAPANY_MOBILE.ValidationExpression	= aRegExpPhone;
            revREIN_COMAPANY_EMAIL.ValidationExpression = aRegExpEmail;
            revREIN_COMPANY_WEBSITE.ValidationExpression = aRegExpSiteUrlWithoutHttp;
            revTERMINATION_DATE.ValidationExpression = aRegExpDate;
            this.revEFFECTIVE_DATE.ValidationExpression = aRegExpDate;
            //revEFFECTIVE_DATE.ValidationExpression	    = aRegExpDate;

            //revFEDERAL_ID.ValidationExpression				= "^[a-zA-Z0-9]{2}[-]{1}[a-zA-Z0-9]{7}";//aRegExpFederalID;
            revSUSEP_NUM.ValidationExpression =aRegExpSUSEPNUM;//revSUSEP_NUM
            revSUSEP_NUM.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1095");
            //revSUSEP_NUM.ErrorMessage = "Please enter 5 digit number";
            //revFEDERAL_ID.ValidationExpression				= aRegExpAlphaNumStrict;
            revNAIC_CODE.ValidationExpression = aRegExpAlphaNumStrict;
            //revFEDERAL_ID.ErrorMessage						= "Correct Format is ##-#######";//ClsMessages.FetchGeneralMessage("434");
            revFEDERAL_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1787");//"Please enter valid Federal ID Number(8-9 digits)";
            revNAIC_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "12"); //"Please enter valid NAIC Code"; //SNEHA
            revREIN_COMPANY_SPEED_DIAL.ValidationExpression ="^[0-9]{4}";
            revREIN_COMPANY_SPEED_DIAL.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1788");//"Please enter 4 digit number";
            this.revAM_BEST_RATING.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1789");//"Please eneter 5 digit alphanumeric number";
            //this.revCOMMENTS.ErrorMessage                   = "Please enter aplhanumeric number";
            //this.revCOMMENTS.ValidationExpression           = aRegExpTextArea255;//"/w+|/W+ "; //"[a-zA-Z0-9]";// "^[a-zA-Z0-9]{*}+$";
            this.revAM_BEST_RATING.ValidationExpression = "^[a-zA-Z0-9]{5}";
            //			rfvM_REIN_COMPANY_CITY.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("56");
            //			rfvM_REIN_COMPANY_COUNTRY.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1009");
            rfvREIN_COMAPANY_COUNTRY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");
            //			rfvM_REIN_COMPANY_STATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("35");
            //			rfvM_REIN_COMPANY_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("37");
            rfvREIN_COMAPANY_STATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");
            rfvREIN_COMAPANY_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("37");
            rfvREIN_COMAPANY_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
            rfvTERMINATION_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
            //cpvFINAL_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("10009");// Changed by amit fo tfs bug #833
            cpvTERMINATION_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("10009");
            revBANK_BRANCH.ValidationExpression = aRegExpBankAccountNumber;
            revBANK_BRANCH.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1401");
            revACCOUNT_NUMBER.ValidationExpression = aRegExpBankAccountNumber; //Changed by Aditya for TFS BUG # 2246
            revACCOUNT_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1404");
            
            rfvACCOUNT_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1201");
            revCARRIER_CNPJ.ValidationExpression = aRegExpCpf_Cnpj;
            revCARRIER_CNPJ.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1203");
            javasciptCNPJmsg = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1203");
            CNPJ_invalid = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1206");
            rfvPAYMENT_METHOD.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1207");
            rfvBANK_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1208");
            rfvSUSEP_NUM.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "14"); //SNEHA ////Changed by Aditya for TFS bug # 923
        }
        
        #endregion


        private void Page_Load(object sender, System.EventArgs e)
        {
            
            //cmbREIN_COMAPANY_COUNTRY.SelectedIndex=int.Parse(aCountry); 		           
            txtREIN_COMAPANY_PHONE.Attributes.Add("onBlur", "javascript:DisableExt('txtREIN_COMAPANY_PHONE','txtREIN_COMAPANY_EXT');");
            txtM_REIN_COMPANY_PHONE.Attributes.Add("onBlur", "javascript:ValidateControl('txtM_REIN_COMPANY_PHONE','rfvM_REIN_COMPANY_PHONE');");//added by avijit for tfs 834
            txtM_REIN_COMPANY_ADD_1.Attributes.Add("onBlur", "javascript:ValidateControl('txtM_REIN_COMPANY_ADD_1','rfvM_REIN_COMPANY_ADD_1');");//added by avijit for tfs 834
            //btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
            btnCopyPhysicalAddress.Attributes.Add("onclick", "javascript:return CopyPhysicalAddress();");
            hlkTERMINATION_DATE.Attributes.Add("OnClick", "fPopCalendar(document.MNT_REIN_COMPANY_LIST.txtTERMINATION_DATE,document.MNT_REIN_COMPANY_LIST.txtTERMINATION_DATE)");
            //START APRIL 10 Harmanjeet
            hlkEFFECTIVE_DATE.Attributes.Add("OnClick", "fPopCalendar(document.MNT_REIN_COMPANY_LIST.txtEFFECTIVE_DATE,document.MNT_REIN_COMPANY_LIST.txtEFFECTIVE_DATE)");
            txtREIN_COMAPANY_NAME.Attributes.Add("onBlur", "javascript:GenerateCustomerCode(\"txtREIN_COMAPANY_NAME\");");
          
            //END Harmanjeet		
            // Added by Swarup on 30-mar-2007
            /*imgZipLookup.Attributes.Add("style","cursor:hand");
            base.VerifyAddress(hlkZipLookup, txtREIN_COMAPANY_ADD1,txtREIN_COMAPANY_ADD2
                , txtREIN_COMAPANY_CITY, txtREIN_COMAPANY_STATE, txtREIN_COMAPANY_ZIP);
            imgMZipLookup.Attributes.Add("style","cursor:hand");
            base.VerifyAddress(hlkMZipLookup, txtM_REIN_COMPANY_ADD_1,txtM_RREIN_COMPANY_ADD_2
                , txtM_REIN_COMPANY_CITY, txtM_REIN_COMPANY_STATE, txtM_REIN_COMPANY_ZIP);*/

            btnReset.Attributes.Add("onclick", "javascript:return Reset();");

            btnReset.Text = Cms.CmsWeb.ClsMessages.FetchGeneralButtonsText("btnReset");
            btnSave.Text = Cms.CmsWeb.ClsMessages.FetchGeneralButtonsText("btnSave");
            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");


            base.ScreenId = "263_0";
            NewscreenId = "478_1";
            lblMessage.Visible = false;
           
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

           
            btnCopyPhysicalAddress.CmsButtonClass = CmsButtonType.Write;
            btnCopyPhysicalAddress.PermissionString = gstrSecurityXML;

            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddReinsurer", System.Reflection.Assembly.GetExecutingAssembly());
            //ReadOnlyFields();

            if (!Page.IsPostBack)
            {
                SetErrorMessages();
               
                if (Request.Params["REIN_COMAPANY_ID"] != null)
                {
                    if (Request.Params["REIN_COMAPANY_ID"].ToString() != "")
                    {
                        hidREIN_COMAPANY_ID.Value = Request.Params["REIN_COMAPANY_ID"].ToString();
                        //GenerateXML(hidREIN_COMAPANY_ID.Value);
                        txtSUSEP_NUM.ReadOnly = true;
                        txtREIN_COMAPANY_CODE.ReadOnly = true;
                    }
                }
                

               
                //if(Request.QueryString["REIN_COMPANY_ID"] !=null && Request.QueryString["REIN_COMPANY_ID"].ToString().Length >0)
                //    hidREIN_COMAPANY_ID.Value = Request.QueryString["REIN_COMPANY_ID"].ToString();

            
    
               
                hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCon");
           
                SetCaptions();
                if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "CmsWeb/support/PageXML/" + GetSystemId(), "AddReinsurer.xml"))
                {
                    setPageControls(Page, @Request.PhysicalApplicationPath + "/CmsWeb/support/PageXML/" + GetSystemId() + "/AddReinsurer.xml");
                }

              
                SetDropdownList();
                #region "Loading singleton"
                //using singleton object for country and state dropdown
             DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
             /*
         cmbREIN_COMAPANY_COUNTRY.DataSource		= dt;
          cmbREIN_COMAPANY_COUNTRY.DataTextField	= "Country_Name";
          cmbREIN_COMAPANY_COUNTRY.DataValueField	= "Country_Id";
          cmbREIN_COMAPANY_COUNTRY.DataBind();
          cmbREIN_COMAPANY_COUNTRY.Items[0].Selected=true; */
             /*
           cmbM_REIN_COMPANY_COUNTRY.DataSource		= dt;
            cmbM_REIN_COMPANY_COUNTRY.DataTextField	= "Country_Name";
            cmbM_REIN_COMPANY_COUNTRY.DataValueField	= "Country_Id";
            cmbM_REIN_COMPANY_COUNTRY.DataBind();
            cmbM_REIN_COMPANY_COUNTRY.Items[0].Selected=true;
               
           dt = Cms.CmsWeb.ClsFetcher.State;
           cmbREIN_COMAPANY_STATE.DataSource		= dt;
           cmbREIN_COMAPANY_STATE.DataTextField	= "STATE_NAME";
           cmbREIN_COMAPANY_STATE.DataValueField	= "STATE_ID";
           cmbREIN_COMAPANY_STATE.DataBind();
           cmbREIN_COMAPANY_STATE.Items.Insert(0,"");*/

                //START APRIL 11 HARMANJEET
                cmbREIN_COMAPANY_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("REINTY");
                //cmbSEX.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("SEX");
                cmbREIN_COMAPANY_TYPE.DataTextField = "LookupDesc";
                cmbREIN_COMAPANY_TYPE.DataValueField = "LookupID";
                cmbREIN_COMAPANY_TYPE.DataBind();
                cmbREIN_COMAPANY_TYPE.Items.Insert(0, "");
                //END HARMANJEET

                //added By Chetna
                cmbCOM_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CMPTY");
                cmbCOM_TYPE.DataTextField = "LookupDesc";
                cmbCOM_TYPE.DataValueField = "LookupID";
                cmbCOM_TYPE.DataBind();
                cmbCOM_TYPE.Items.Insert(0, "");
                /*this.cmbNAIC_CODE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("NAIC");
                this.cmbNAIC_CODE.DataTextField="LookupDesc";
                this.cmbNAIC_CODE.DataValueField="LookupDesc";
                this.cmbNAIC_CODE.DataBind();
                this.cmbNAIC_CODE.Items.Insert(0,"");*/

                /*dt = Cms.CmsWeb.ClsFetcher.State;
                cmbM_REIN_COMPANY_STATE.DataSource		= dt;
                cmbM_REIN_COMPANY_STATE.DataTextField	= "STATE_NAME";
                cmbM_REIN_COMPANY_STATE.DataValueField	= "STATE_ID";
                cmbM_REIN_COMPANY_STATE.DataBind();
                cmbM_REIN_COMPANY_STATE.Items.Insert(0,"");*/


                cmbACCOUNT_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("ACTYP");
                cmbACCOUNT_TYPE.DataTextField = "LookupDesc";
                cmbACCOUNT_TYPE.DataValueField = "LookupID";
                cmbACCOUNT_TYPE.DataBind();
                cmbACCOUNT_TYPE.Items.Insert(0, "");

                #endregion//Loading singleton 
               
                GetDataForEditMode();
            }
        }

        #region SET CAPATION
        private void SetCaptions()
        {
          
            
            capREIN_COMAPANY_NAME.Text = objResourceMgr.GetString("txtREIN_COMAPANY_NAME");
            capREIN_COMAPANY_CODE.Text = objResourceMgr.GetString("txtREIN_COMAPANY_CODE");
            capREIN_COMAPANY_ADD1.Text = objResourceMgr.GetString("txtREIN_COMAPANY_ADD1");
            capREIN_COMAPANY_ADD2.Text = objResourceMgr.GetString("txtREIN_COMAPANY_ADD2");
            capREIN_COMAPANY_CITY.Text = objResourceMgr.GetString("txtREIN_COMAPANY_CITY");
            capREIN_COMAPANY_COUNTRY.Text = objResourceMgr.GetString("cmbREIN_COMAPANY_COUNTRY");
            this.capREIN_COMAPANY_STATE.Text = objResourceMgr.GetString("cmbREIN_COMAPANY_STATE");
            capREIN_COMAPANY_ZIP.Text = objResourceMgr.GetString("txtREIN_COMAPANY_ZIP");
            capREIN_COMAPANY_PHONE.Text = objResourceMgr.GetString("txtREIN_COMAPANY_PHONE");
            capREIN_COMAPANY_EXT.Text = objResourceMgr.GetString("txtREIN_COMAPANY_EXT");
            capREIN_COMAPANY_FAX.Text = objResourceMgr.GetString("txtREIN_COMAPANY_FAX");
            capREIN_COMAPANY_MOBILE.Text = objResourceMgr.GetString("txtREIN_COMAPANY_MOBILE");
            capREIN_COMAPANY_EMAIL.Text = objResourceMgr.GetString("txtREIN_COMAPANY_EMAIL");
            capREIN_COMAPANY_NOTE.Text = objResourceMgr.GetString("txtREIN_COMAPANY_NOTE");
            capREIN_COMAPANY_ACC_NUMBER.Text = objResourceMgr.GetString("txtREIN_COMAPANY_ACC_NUMBER");
            capM_REIN_COMPANY_ADD_1.Text = objResourceMgr.GetString("txtM_REIN_COMPANY_ADD_1");
            capM_RREIN_COMPANY_ADD_2.Text = objResourceMgr.GetString("txtM_RREIN_COMPANY_ADD_2");
            capM_REIN_COMPANY_CITY.Text = objResourceMgr.GetString("txtM_REIN_COMPANY_CITY");
            capM_REIN_COMPANY_COUNTRY.Text = objResourceMgr.GetString("cmbM_REIN_COMPANY_COUNTRY");
            capM_REIN_COMPANY_STATE.Text = objResourceMgr.GetString("cmbM_REIN_COMPANY_STATE");
            capM_REIN_COMPANY_ZIP.Text = objResourceMgr.GetString("txtM_REIN_COMPANY_ZIP");
            capM_REIN_COMPANY_PHONE.Text = objResourceMgr.GetString("txtM_REIN_COMPANY_PHONE");
            capM_REIN_COMPANY_FAX.Text = objResourceMgr.GetString("txtM_REIN_COMPANY_FAX");
            capM_REIN_COMPANY_EXT.Text = objResourceMgr.GetString("txtM_REIN_COMPANY_EXT");
            capREIN_COMPANY_WEBSITE.Text = objResourceMgr.GetString("txtREIN_COMPANY_WEBSITE");
            capREIN_COMPANY_IS_BROKER.Text = objResourceMgr.GetString("cmbREIN_COMPANY_IS_BROKER");
            capPRINCIPAL_CONTACT.Text = objResourceMgr.GetString("txtPRINCIPAL_CONTACT");
            capOTHER_CONTACT.Text = objResourceMgr.GetString("txtOTHER_CONTACT");
            capFEDERAL_ID.Text = objResourceMgr.GetString("txtFEDERAL_ID");
            //capROUTING_NUMBER.Text						=	   objResourceMgr.GetString("txtROUTING_NUMBER");
            lblCopy_Address.Text = objResourceMgr.GetString("lblCopy_Address");
            capTERMINATION_DATE.Text = objResourceMgr.GetString("txtTERMINATION_DATE");
            capTERMINATION_REASON.Text = objResourceMgr.GetString("txtTERMINATION_REASON");
            //START Harmanjeet April 09 2007
            this.capAM_BEST_RATING.Text = this.objResourceMgr.GetString("txtAM_BEST_RATING");
            this.capEFFECTIVE_DATE.Text = this.objResourceMgr.GetString("txtEFFECTIVE_DATE");
            this.capNAIC_CODE.Text = this.objResourceMgr.GetString("cmbNAIC_CODE");
            this.capCOMMENTS.Text = this.objResourceMgr.GetString("txtCOMMENTS");
            this.capREIN_COMPANY_SPEED_DIAL.Text = this.objResourceMgr.GetString("txtREIN_COMPANY_SPEED_DIAL");
            //this.capM_REIN_COMPANY_SPEED_DIAL.Text=this.objResourceMgr.GetString("txtM_REIN_COMPANY_SPEED_DIAL");
            this.capDOMICILED_STATE.Text = this.objResourceMgr.GetString("txtDOMICILED_STATE");
            this.capREIN_COMAPANY_TYPE.Text = this.objResourceMgr.GetString("cmbREIN_COMAPANY_TYPE");
            capSUSEP_NUM.Text = objResourceMgr.GetString("txtSUSEP_NUM");
            capCOM_TYPE.Text = objResourceMgr.GetString("cmbCOM_TYPE");
            capBANK_NUMBER.Text = objResourceMgr.GetString("txtBANK_NUMBER");
            capDISTRICT.Text = objResourceMgr.GetString("txtDISTRICT");
            capBANK_BRANCH.Text = objResourceMgr.GetString("txtBANK_BRANCH");
            capACCOUNT_TYPE.Text = objResourceMgr.GetString("cmbACCOUNT_TYPE");
            capPAYMENT_METHOD.Text = objResourceMgr.GetString("cmbPAYMENT_METHOD");
            capCARRIER_CNPJ.Text = objResourceMgr.GetString("txtCARRIER_CNPJ");
            capphysical.Text = objResourceMgr.GetString("capphysical"); //SNEHA
            capMAILING.Text = objResourceMgr.GetString("capMAILING"); //sneha
            capOTHER.Text = objResourceMgr.GetString("capOTHER"); //sneha
            capRISK_CLASSIFICATION.Text = objResourceMgr.GetString("txtRISK_CLASSIFICATION");
            capAGENCY_CLASSIFICATION.Text = objResourceMgr.GetString("txtAGENCY_CLASSIFICATION");
            btnCopyPhysicalAddress.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9"); //sneha
            //END Harmanjeet
            
          
            
        }

        # endregion SET CAPATION

        # region SET DROP DOWN LIST
        private void SetDropdownList()
        {
            cmbREIN_COMPANY_IS_BROKER.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
            cmbREIN_COMPANY_IS_BROKER.DataTextField = "LookupDesc";
            cmbREIN_COMPANY_IS_BROKER.DataValueField = "LookupCode";
            cmbREIN_COMPANY_IS_BROKER.DataBind();
            cmbREIN_COMPANY_IS_BROKER.Items.Insert(0, "");


            cmbACCOUNT_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("ACTYP");
            cmbACCOUNT_TYPE.DataTextField = "LookupDesc";
            cmbACCOUNT_TYPE.DataValueField = "LookupID";
            cmbACCOUNT_TYPE.DataBind();
            cmbACCOUNT_TYPE.Items.Insert(0, "");

            cmbPAYMENT_METHOD.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PYMTY");
            cmbPAYMENT_METHOD.DataTextField = "LookupDesc";
            cmbPAYMENT_METHOD.DataValueField = "LookupID";
            cmbPAYMENT_METHOD.DataBind();
            cmbPAYMENT_METHOD.Items.Insert(0, "");	

            DataTable dtCountry = Cms.CmsWeb.ClsFetcher.AllCountry;
            cmbREIN_COMAPANY_COUNTRY_SIN.DataSource = dtCountry;
            cmbREIN_COMAPANY_COUNTRY_SIN.DataTextField = "COUNTRY_NAME";
            cmbREIN_COMAPANY_COUNTRY_SIN.DataValueField = "COUNTRY_NAME";
            cmbREIN_COMAPANY_COUNTRY_SIN.DataBind();


            cmbM_REIN_COMPANY_COUNTRY_SIN.DataSource = dtCountry;
            cmbM_REIN_COMPANY_COUNTRY_SIN.DataTextField = "Country_Name";
            cmbM_REIN_COMPANY_COUNTRY_SIN.DataValueField = "COUNTRY_NAME";
            cmbM_REIN_COMPANY_COUNTRY_SIN.DataBind();
            cmbM_REIN_COMPANY_COUNTRY_SIN.Items[0].Selected = true;
            
        }
        # endregion SET DROP DOWN LIST

        # region  G E T   D A T A   F O R   E D I T   M O D E

        private void GetDataForEditMode()
        {
            try
            {
                objReinsurer = new ClsReinsurer();
                if (this.hidREIN_COMAPANY_ID.Value == "" || this.hidREIN_COMAPANY_ID.Value == "0") return;
                DataSet oDs = objReinsurer.GetDataForPageControls(this.hidREIN_COMAPANY_ID.Value);
                hidOldData.Value = oDs.GetXml();
                setEncryptXml();

                if (oDs.Tables[0].Rows.Count > 0)
                {
                    DataRow oDr = oDs.Tables[0].Rows[0];

                    # region TEXT BOX
                    this.txtREIN_COMAPANY_CODE.Text = oDr["REIN_COMAPANY_CODE"].ToString();
                    this.txtREIN_COMAPANY_NAME.Text = oDr["REIN_COMAPANY_NAME"].ToString();
                    this.txtREIN_COMAPANY_ADD1.Text = oDr["REIN_COMAPANY_ADD1"].ToString();
                    this.txtREIN_COMAPANY_ADD2.Text = oDr["REIN_COMAPANY_ADD2"].ToString();
                    this.txtREIN_COMAPANY_CITY.Text = oDr["REIN_COMAPANY_CITY"].ToString();
                    this.txtREIN_COMAPANY_ZIP.Text = oDr["REIN_COMAPANY_ZIP"].ToString();
                    this.txtREIN_COMAPANY_PHONE.Text = oDr["REIN_COMAPANY_PHONE"].ToString();
                    this.txtREIN_COMAPANY_EXT.Text = oDr["REIN_COMAPANY_EXT"].ToString();
                    this.txtREIN_COMAPANY_FAX.Text = oDr["REIN_COMAPANY_FAX"].ToString();
                    this.txtREIN_COMAPANY_MOBILE.Text = oDr["REIN_COMAPANY_MOBILE"].ToString();
                    this.txtREIN_COMAPANY_EMAIL.Text = oDr["REIN_COMAPANY_EMAIL"].ToString();
                    this.txtREIN_COMAPANY_NOTE.Text = oDr["REIN_COMAPANY_NOTE"].ToString();
                    this.txtREIN_COMAPANY_ACC_NUMBER.Text = oDr["REIN_COMAPANY_ACC_NUMBER"].ToString();
                    this.txtM_REIN_COMPANY_ADD_1.Text = oDr["M_REIN_COMPANY_ADD_1"].ToString();
                    this.txtM_RREIN_COMPANY_ADD_2.Text = oDr["M_RREIN_COMPANY_ADD_2"].ToString();
                    this.txtM_REIN_COMPANY_CITY.Text = oDr["M_REIN_COMPANY_CITY"].ToString();
                    this.txtM_REIN_COMPANY_ZIP.Text = oDr["M_REIN_COMPANY_ZIP"].ToString();
                    this.txtM_REIN_COMPANY_PHONE.Text = oDr["M_REIN_COMPANY_PHONE"].ToString();
                    this.txtM_REIN_COMPANY_FAX.Text = oDr["M_REIN_COMPANY_FAX"].ToString();
                    this.txtM_REIN_COMPANY_EXT.Text = oDr["M_REIN_COMPANY_EXT"].ToString();
                    this.txtREIN_COMPANY_WEBSITE.Text = oDr["REIN_COMPANY_WEBSITE"].ToString();
                    this.txtEFFECTIVE_DATE.Text = oDr["EFFECTIVE_DATE"].ToString();
                    this.txtCOMMENTS.Text = oDr["COMMENTS"].ToString();
                    this.txtDOMICILED_STATE.Text = oDr["DOMICILED_STATE"].ToString();
                    this.txtREIN_COMPANY_SPEED_DIAL.Text = oDr["REIN_COMPANY_SPEED_DIAL"].ToString().Trim();
                    this.txtPRINCIPAL_CONTACT.Text = oDr["PRINCIPAL_CONTACT"].ToString();
                    this.txtOTHER_CONTACT.Text = oDr["OTHER_CONTACT"].ToString();
                    //this.txtFEDERAL_ID.Text=oDr["FEDERAL_ID"].ToString();
                    this.txtM_REIN_COMPANY_EXT.Text = oDr["M_REIN_COMPANY_EXT"].ToString();
                    this.txtTERMINATION_DATE.Text = oDr["TERMINATION_DATE"].ToString();
                    this.txtTERMINATION_REASON.Text = oDr["TERMINATION_REASON"].ToString();
                    this.txtAM_BEST_RATING.Text = oDr["AM_BEST_RATING"].ToString();
                    this.txtNAIC_CODE.Text = oDr["NAIC_CODE"].ToString();

                    this.txtREIN_COMAPANY_COUNTRY.Text = oDr["REIN_COMAPANY_COUNTRY"].ToString();
                    this.txtM_REIN_COMPANY_COUNTRY.Text = oDr["M_REIN_COMPANY_COUNTRY"].ToString();
                    this.txtM_REIN_COMPANY_STATE.Text = oDr["M_REIN_COMPANY_STATE"].ToString();
                    this.txtREIN_COMAPANY_STATE.Text = oDr["REIN_COMAPANY_STATE"].ToString();
                    //added By Chetna
                    this.txtSUSEP_NUM.Text = oDr["SUSEP_NUM"].ToString().PadLeft(5,'0'); 
                    this.txtDISTRICT.Text = oDr["DISTRICT"].ToString();
                    this.txtBANK_NUMBER.Text = oDr["BANK_NUMBER"].ToString();
                    this.txtBANK_BRANCH.Text = oDr["BANK_BRANCH_NUMBER"].ToString();
                    this.txtCARRIER_CNPJ.Text = oDr["CARRIER_CNPJ"].ToString();
                    this.txtAGENCY_CLASSIFICATION.Text = oDr["AGENCY_CLASSIFICATION"].ToString();
                    this.txtRISK_CLASSIFICATION.Text = oDr["RISK_CLASSIFICATION"].ToString();
                    # endregion TEXT BOX
                    # region COMBO BOX
                    ListItem li = new ListItem();

                    li = this.cmbREIN_COMPANY_IS_BROKER.Items.FindByValue(oDr["REIN_COMPANY_IS_BROKER"].ToString());
                    cmbREIN_COMPANY_IS_BROKER.SelectedIndex = cmbREIN_COMPANY_IS_BROKER.Items.IndexOf(li);

                    /*li = this.cmbNAIC_CODE.Items.FindByValue(oDr["NAIC_CODE"].ToString());
                    cmbNAIC_CODE.SelectedIndex = cmbNAIC_CODE.Items.IndexOf(li);
					
                    li = this.cmbREIN_COMAPANY_STATE.Items.FindByValue(oDr["REIN_COMPANY_STATE"].ToString());
                    cmbREIN_COMAPANY_STATE.SelectedIndex = cmbREIN_COMAPANY_STATE.Items.IndexOf(li);*/

                    li = this.cmbREIN_COMAPANY_COUNTRY_SIN.Items.FindByValue(oDr["REIN_COMAPANY_COUNTRY"].ToString());
                    cmbREIN_COMAPANY_COUNTRY_SIN.SelectedIndex = cmbREIN_COMAPANY_COUNTRY_SIN.Items.IndexOf(li);

                    /*li = this.cmbM_REIN_COMPANY_STATE.Items.FindByValue(oDr["M_REIN_COMPANY_STATE"].ToString());
                    cmbM_REIN_COMPANY_STATE.SelectedIndex = cmbM_REIN_COMPANY_STATE.Items.IndexOf(li);*/
					
                    li = this.cmbM_REIN_COMPANY_COUNTRY_SIN.Items.FindByValue(oDr["M_REIN_COMPANY_COUNTRY"].ToString());
                    cmbM_REIN_COMPANY_COUNTRY_SIN.SelectedIndex = cmbM_REIN_COMPANY_COUNTRY_SIN.Items.IndexOf(li);

                    li = this.cmbREIN_COMAPANY_TYPE.Items.FindByValue(oDr["REIN_COMAPANY_TYPE"].ToString());
                    cmbREIN_COMAPANY_TYPE.SelectedIndex = cmbREIN_COMAPANY_TYPE.Items.IndexOf(li);
                    li = this.cmbCOM_TYPE.Items.FindByValue(oDr["COM_TYPE"].ToString());
                    cmbCOM_TYPE.SelectedIndex = cmbCOM_TYPE.Items.IndexOf(li);
                    li = this.cmbACCOUNT_TYPE.Items.FindByValue(oDr["BANK_ACCOUNT_TYPE"].ToString());
                    cmbACCOUNT_TYPE.SelectedIndex = cmbACCOUNT_TYPE.Items.IndexOf(li);
                    li = this.cmbPAYMENT_METHOD.Items.FindByValue(oDr["PAYMENT_METHOD"].ToString());
                    cmbPAYMENT_METHOD.SelectedIndex = cmbPAYMENT_METHOD.Items.IndexOf(li);

                    # endregion COMBO BOX
                    # region IS ACTIVE
                    hidIS_ACTIVE.Value = oDr["IS_ACTIVE"].ToString().ToUpper();
                    if (oDr["IS_ACTIVE"].ToString() == "Y")
                        btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("Y");
                    if (oDr["IS_ACTIVE"].ToString() == "N")
                        btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("N");
                    # endregion IS ACTIVE
                }
            }
            catch (Exception oEx)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
            }
            finally { }
        }

        # endregion G E T   D A T A   F O R   E D I T   M O D E

        #region GetFormValue
        /// <summary>
        /// Fetch form's value and stores into model class object and return that object.
        /// </summary>

        private ClsReinsurerInfo GetFormValue()
        {
            //Creating the Model object for holding the New data
            ClsReinsurerInfo objReinsurerInfo;
            objReinsurerInfo = new ClsReinsurerInfo();

            objReinsurerInfo.REIN_COMAPANY_CODE = txtREIN_COMAPANY_CODE.Text;
            objReinsurerInfo.REIN_COMAPANY_NAME = txtREIN_COMAPANY_NAME.Text;
            objReinsurerInfo.REIN_COMPANY_TYPE = this.cmbREIN_COMAPANY_TYPE.SelectedValue;//txtREIN_COMAPANY_NAME.Text;

            objReinsurerInfo.REIN_COMAPANY_ADD1 = txtREIN_COMAPANY_ADD1.Text;
            objReinsurerInfo.REIN_COMAPANY_ADD2 = txtREIN_COMAPANY_ADD2.Text;
            objReinsurerInfo.REIN_COMAPANY_CITY = txtREIN_COMAPANY_CITY.Text;
            objReinsurerInfo.REIN_COMAPANY_COUNTRY = txtREIN_COMAPANY_COUNTRY.Text;
            //			objReinsurerInfo.REIN_COMAPANY_COUNTRY=	cmbREIN_COMAPANY_COUNTRY.SelectedValue;
            //			objReinsurerInfo.REIN_COMAPANY_STATE  =	cmbREIN_COMAPANY_STATE.SelectedValue;
            objReinsurerInfo.REIN_COMAPANY_STATE = txtREIN_COMAPANY_STATE.Text;
            objReinsurerInfo.REIN_COMAPANY_ZIP = txtREIN_COMAPANY_ZIP.Text;
            objReinsurerInfo.REIN_COMAPANY_PHONE = txtREIN_COMAPANY_PHONE.Text;
            objReinsurerInfo.REIN_COMAPANY_EXT = txtREIN_COMAPANY_EXT.Text;
            objReinsurerInfo.REIN_COMAPANY_FAX = txtREIN_COMAPANY_FAX.Text;
            objReinsurerInfo.REIN_COMPANY_SPEED_DIAL = this.txtREIN_COMPANY_SPEED_DIAL.Text;

            objReinsurerInfo.REIN_COMAPANY_MOBILE = txtREIN_COMAPANY_MOBILE.Text;
            objReinsurerInfo.REIN_COMAPANY_EMAIL = txtREIN_COMAPANY_EMAIL.Text;
            objReinsurerInfo.REIN_COMAPANY_NOTE = txtREIN_COMAPANY_NOTE.Text;
            objReinsurerInfo.REIN_COMAPANY_ACC_NUMBER = txtREIN_COMAPANY_ACC_NUMBER.Text;

            objReinsurerInfo.M_REIN_COMPANY_ADD_1 = txtM_REIN_COMPANY_ADD_1.Text;
            objReinsurerInfo.M_RREIN_COMPANY_ADD_2 = txtM_RREIN_COMPANY_ADD_2.Text;
            objReinsurerInfo.M_REIN_COMPANY_CITY = txtM_REIN_COMPANY_CITY.Text;
            objReinsurerInfo.M_REIN_COMPANY_COUNTRY = txtM_REIN_COMPANY_COUNTRY.Text;
            //			objReinsurerInfo.M_REIN_COMPANY_COUNTRY= cmbM_REIN_COMPANY_COUNTRY.SelectedValue;
            //			objReinsurerInfo.M_REIN_COMPANY_STATE= cmbM_REIN_COMPANY_STATE.SelectedValue;
            objReinsurerInfo.M_REIN_COMPANY_STATE = txtM_REIN_COMPANY_STATE.Text;
            objReinsurerInfo.M_REIN_COMPANY_ZIP = txtM_REIN_COMPANY_ZIP.Text;
            objReinsurerInfo.M_REIN_COMPANY_PHONE = txtM_REIN_COMPANY_PHONE.Text;
            objReinsurerInfo.M_REIN_COMPANY_FAX = txtM_REIN_COMPANY_FAX.Text;
            objReinsurerInfo.M_REIN_COMPANY_EXT = txtM_REIN_COMPANY_EXT.Text;

            objReinsurerInfo.REIN_COMPANY_WEBSITE = txtREIN_COMPANY_WEBSITE.Text;
            //added By Chetna
            objReinsurerInfo.SUSEP_NUM = txtSUSEP_NUM.Text;
            objReinsurerInfo.COM_TYPE = cmbCOM_TYPE.SelectedValue;
            //objReinsurerInfo.REIN_COMPANY_IS_BROKER= cmbREIN_COMPANY_IS_BROKER.SelectedValue;

            if (txtPRINCIPAL_CONTACT.Text.Trim() != "")
                objReinsurerInfo.PRINCIPAL_CONTACT = txtPRINCIPAL_CONTACT.Text;
            if (txtOTHER_CONTACT.Text.Trim() != "")
                objReinsurerInfo.OTHER_CONTACT = txtOTHER_CONTACT.Text;

            //if(txtFEDERAL_ID.Text.Trim()!="")
            //	objReinsurerInfo.FEDERAL_ID =  txtFEDERAL_ID.Text;
            if (txtFEDERAL_ID.Text.Trim() != "")
            {
                objReinsurerInfo.FEDERAL_ID = Cms.BusinessLayer.BlCommon.ClsCommon.EncryptString(txtFEDERAL_ID.Text.Trim());
                txtFEDERAL_ID.Text = "";
            }
            else
                objReinsurerInfo.FEDERAL_ID = hidFEDERAL_ID.Value;

            objReinsurerInfo.ROUTING_NUMBER = txtM_REIN_COMPANY_EXT.Text;

            if (txtTERMINATION_DATE.Text.Trim() != "")
                objReinsurerInfo.TERMINATION_DATE = Convert.ToDateTime(txtTERMINATION_DATE.Text);
            objReinsurerInfo.TERMINATION_REASON = txtTERMINATION_REASON.Text;

            objReinsurerInfo.AM_BEST_RATING = this.txtAM_BEST_RATING.Text;
            objReinsurerInfo.DOMICILED_STATE = this.txtDOMICILED_STATE.Text;
            //			objReinsurerInfo.NAIC_CODE=cmbNAIC_CODE.SelectedValue;;
            objReinsurerInfo.NAIC_CODE = this.txtNAIC_CODE.Text;

            if (txtEFFECTIVE_DATE.Text.Trim() != "")
                objReinsurerInfo.EFFECTIVE_DATE = Convert.ToDateTime(txtEFFECTIVE_DATE.Text);

            //objReinsurerInfo.COMMENTS= this.txtREIN_COMAPANY_NAME.Text;
            objReinsurerInfo.COMMENTS = this.txtCOMMENTS.Text;

            objReinsurerInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

            //These  assignments are common to all pages.
            //strFormSaved	=	hidFormSaved.Value;
            //strRowId		=	hidREIN_COMAPANY_ID.Value;
            //oldXML		= hidOldData.Value;
            //Returning the model object
            //These  assignments are common to all pages.
            strFormSaved = hidFormSaved.Value;
            strRowId = this.hidREIN_COMAPANY_ID.Value;
            //oldXML			=	hidOldData.Value;
            //Returning the model object
            objReinsurerInfo.DISTRICT = txtDISTRICT.Text;
            objReinsurerInfo.BANK_NUMBER = txtBANK_NUMBER.Text;
            objReinsurerInfo.BANK_BRANCH_NUMBER = txtBANK_BRANCH.Text;
            objReinsurerInfo.CARRIER_CNPJ = txtCARRIER_CNPJ.Text;
            objReinsurerInfo.BANK_ACCOUNT_TYPE = int.Parse(this.cmbACCOUNT_TYPE.SelectedValue);
            objReinsurerInfo.PAYMENT_METHOD = int.Parse(this.cmbPAYMENT_METHOD.SelectedValue);
            objReinsurerInfo.AGENCY_CLASSIFICATION = txtAGENCY_CLASSIFICATION.Text;
            objReinsurerInfo.RISK_CLASSIFICATION = txtRISK_CLASSIFICATION.Text;


            return objReinsurerInfo;
        }


        #endregion


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
                objReinsurer = new ClsReinsurer();

                //Retreiving the form values into model class object
                ClsReinsurerInfo objReinsurerInfo = GetFormValue();
                objReinsurerInfo.CUSTOM_INFO = objResourceMgr.GetString("txtREIN_COMAPANY_NAME") + ":" + objReinsurerInfo.REIN_COMAPANY_NAME + "<br>"
                                      + objResourceMgr.GetString("txtREIN_COMAPANY_CODE") + ":" + objReinsurerInfo.REIN_COMAPANY_CODE + "<br>"
                                      + this.objResourceMgr.GetString("cmbREIN_COMAPANY_TYPE") + ":" + cmbREIN_COMAPANY_TYPE.Items[cmbREIN_COMAPANY_TYPE.SelectedIndex].Text;
                if (strRowId.ToUpper().Equals("NEW")) //Add Mode
                {
                    objReinsurerInfo.CREATED_BY = int.Parse(GetUserId());
                    objReinsurerInfo.CREATED_DATETIME = DateTime.Now;
                    objReinsurerInfo.IS_ACTIVE = "Y";
                    
                    //Calling the add method of business layer class
                    intRetVal = objReinsurer.Add(objReinsurerInfo);

                    if (intRetVal > 0)
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        hidREIN_COMAPANY_ID.Value = objReinsurerInfo.REIN_COMAPANY_ID.ToString();
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                        hidFormSaved.Value = "1";
                        this.hidOldData.Value = objReinsurer.GetDataForPageControls(this.hidREIN_COMAPANY_ID.Value).GetXml();
                        setEncryptXml();
                        //txtSUSEP_NUM.ReadOnly = true;
                        hidIS_ACTIVE.Value = "Y";
                        txtSUSEP_NUM.ReadOnly = true;
                        txtREIN_COMAPANY_CODE.ReadOnly = true;
                    }
                    else if (intRetVal == -1)
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "5");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                        hidFormSaved.Value = "2";
                    }
                    lblMessage.Visible = true;
                } 
                   // end save case
                else //UPDATE CASE
                {

                    //Creating the Model object for holding the Old data
                    ClsReinsurerInfo objOldReinsurerInfo;
                    objOldReinsurerInfo = new ClsReinsurerInfo();

                    //Setting  the Old Page details(XML File containing old details) into the Model Object
                    base.PopulateModelObject(objOldReinsurerInfo, hidOldData.Value);

                    //Setting those values into the Model object which are not in the page
                    if (strRowId != "")
                        objReinsurerInfo.REIN_COMAPANY_ID = int.Parse(strRowId);
                    objReinsurerInfo.MODIFIED_BY = int.Parse(GetUserId());
                    objReinsurerInfo.LAST_UPDATED_DATETIME = DateTime.Now;


                    //Updating the record using business layer class object
                    intRetVal = objReinsurer.Update(objOldReinsurerInfo, objReinsurerInfo);
                    if (intRetVal > 0)			// update successfully performed
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "31");
                        hidFormSaved.Value = "1";
                        this.hidOldData.Value = objReinsurer.GetDataForPageControls(strRowId).GetXml();
                        setEncryptXml();


                    }

                    else if (intRetVal == -1)	// Duplicate code exist, update failed
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "18");
                        hidFormSaved.Value = "1";
                    }
                    else
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "20");
                        hidFormSaved.Value = "1";
                    }
                    lblMessage.Visible = true;
                }


                if (hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
                {
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("Y");
                }

                else
                {
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("N");
                }
            }

            catch (Exception ex)
            {
                ClsMessages.SetCustomizedXml(GetLanguageCode());
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";
                //ExceptionManager.Publish(ex);

            }
            finally
            {
                if (objReinsurer != null)
                    objReinsurer.Dispose();
            }




        }

        #endregion

        //Added by Mohit Agarwal 24-Sep 08 to Encrypt Federal Id
        private void setEncryptXml()
        {
            //Added by Mohit Agarwal 23-Sep-08
           
                if (hidOldData.Value.IndexOf("NewDataSet") >= 0)
                {
                    XmlDocument objxml = new XmlDocument();

                    objxml.LoadXml(hidOldData.Value);

                    XmlNode node = objxml.SelectSingleNode("NewDataSet");
                    foreach (XmlNode nodes in node.SelectNodes("Table"))
                    {
                        XmlNode noder1 = nodes.SelectSingleNode("FEDERAL_ID");

                        hidFEDERAL_ID.Value = noder1.InnerText;
                        //noder1.InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder1.InnerText);
                        try
                        {
                            string strFEDERAL_ID = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder1.InnerText);
                            if (strFEDERAL_ID != "")
                            {
                                string strvaln = "";
                                for (int len = 0; len < strFEDERAL_ID.Length - 4; len++)
                                    strvaln += "x";

                                strvaln += strFEDERAL_ID.Substring(strvaln.Length, strFEDERAL_ID.Length - strvaln.Length);
                                capFEDERAL_ID_HID.Text = strvaln;
                            }
                            else
                                capFEDERAL_ID_HID.Text = "";
                        }
                        catch
                        {

                        }
                    }
                    objxml = null;
                }
           
        }

        /// <summary>
        /// fetching data based on query string values
        /// </summary>
        private void GenerateXML(string REIN_COMPANY_ID)
        {
            string strREIN_COMPANY_ID = REIN_COMPANY_ID;

            objReinsurer = new ClsReinsurer();


            if (strREIN_COMPANY_ID != "" && strREIN_COMPANY_ID != null)
            {
                try
                {
                    DataSet ds = new DataSet();
                    ds = objReinsurer.GetDataForPageControls(strREIN_COMPANY_ID);
                    hidOldData.Value = ds.GetXml();
                    setEncryptXml();
                    //hidFormSaved.Value="1"; 
                }
                catch (Exception ex)
                {
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                    lblMessage.Visible = true;
                    Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                    hidFormSaved.Value = "2";

                }
                finally
                {
                    if (objReinsurer != null)
                        objReinsurer.Dispose();
                }

            }

        }


        /*	#region DEACTIVATE ACTIVATE BUTTON CLICK
		
            private void btnActivate_Click(object sender, System.EventArgs e)
            {
		
                int lintResult=0;
                objReinsurer = new ClsReinsurer();
			
						
                if(btnActivate.Text=="Deactivate")
                {
						  
                    lintResult=objReinsurer.GetDeactivateActivate(hidREIN_COMAPANY_ID.Value.ToString(),"N");
                    btnActivate.Text="Activate";
                    lblMessage.Visible = true;
                    lblMessage.Text ="Information deactivated successfully.";
                }
                else
                {
				
                    lintResult=objReinsurer.GetDeactivateActivate(hidREIN_COMAPANY_ID.Value.ToString(),"Y");
                    btnActivate.Text="Deactivate";
                    lblMessage.Visible = true;
                    lblMessage.Text ="Information activated successfully.";

                }
			
            }
		
            #endregion*/

        #region DEACTIVATE ACTIVATE BUTTON CLICK
        private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
        {
            try
            {
                Cms.BusinessLayer.BlCommon.stuTransactionInfo objStuTransactionInfo = new Cms.BusinessLayer.BlCommon.stuTransactionInfo();
                objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
                objStuTransactionInfo.loggedInUserName = GetUserName();
                //objDepartment =  new ClsDepartment();
                Cms.BusinessLayer.BlCommon.ClsReinsurer objReinsurer = new ClsReinsurer();

                Model.Maintenance.Reinsurance.ClsReinsurerInfo objReinsurerInfo=new ClsReinsurerInfo();
               // objReinsurerInfo = GetFormValue();

                string strRetVal = "";
                string strCustomInfo = objResourceMgr.GetString("txtREIN_COMAPANY_NAME")+":" +objReinsurerInfo.REIN_COMAPANY_NAME + "<br>"
                                      + objResourceMgr.GetString("txtREIN_COMAPANY_CODE")+":" + objReinsurerInfo.REIN_COMAPANY_CODE + "<br>"
                                      + this.objResourceMgr.GetString("cmbREIN_COMAPANY_TYPE")+":" + cmbREIN_COMAPANY_TYPE.Items[cmbREIN_COMAPANY_TYPE.SelectedIndex].Text;
                /*strCustomInfo = "Line Of Business:" + cmbREIN_LINE_OF_BUSINESS.Items[cmbREIN_LINE_OF_BUSINESS.SelectedIndex].Text +"<br>"
                    +"State:" + cmbREIN_STATE.Items[cmbREIN_STATE.SelectedIndex].Text +"<br>"
                    +"1st Split Coverage:" + cmbREIN_IST_SPLIT_COVERAGE.Items[cmbREIN_IST_SPLIT_COVERAGE.SelectedIndex].Text +"<br>"
                    +"2nd Split Coverage:" + cmbREIN_2ND_SPLIT_COVERAGE.Items[cmbREIN_2ND_SPLIT_COVERAGE.SelectedIndex].Text;*/
                if (hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
                {
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
                    objStuTransactionInfo.transactionDescription = ClsMessages.GetMessage(NewscreenId, "9");
                    objReinsurer.TransactionInfoParams = objStuTransactionInfo;
                    strRetVal = objReinsurer.ActivateDeactivate(hidREIN_COMAPANY_ID.Value, "N", strCustomInfo);
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "41");
                    hidIS_ACTIVE.Value = "N";
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("N");
                }
                else
                {
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
                    objStuTransactionInfo.transactionDescription = ClsMessages.GetMessage(NewscreenId, "10");
                    objReinsurer.TransactionInfoParams = objStuTransactionInfo;
                    objReinsurer.ActivateDeactivate(hidREIN_COMAPANY_ID.Value, "Y", strCustomInfo);
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "40");
                    hidIS_ACTIVE.Value = "Y";
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchActivateDeactivateButtonsText("Y");
                }

                //				if (strRetVal == "-1")
                //				{
                //					/*Profit Center is assigned*/
                //					lblMessage.Text =  ClsMessages.GetMessage(base.ScreenId,"513");
                //					lblDelete.Visible = false;
                //				}
                hidOldData.Value = objReinsurer.GetDataForPageControls(hidREIN_COMAPANY_ID.Value.ToString()).GetXml();
                setEncryptXml();
                hidFormSaved.Value = "0";
                //GetOldDataXML();
                hidReset.Value = "0";

            }
            catch (Exception ex)
            {
                ClsMessages.SetCustomizedXml(GetLanguageCode());
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
                lblMessage.Visible = true;
                if (objReinsurer != null)
                    objReinsurer.Dispose();
            }
        }
        #endregion

        private void GetOldDataXML()
        {
            Cms.BusinessLayer.BlCommon.ClsReinsurer objReinsurer = new ClsReinsurer();
            if (hidREIN_COMAPANY_ID.Value.ToString() != "")
            {
                this.hidOldData.Value = objReinsurer.GetDataForPageControls(strRowId).GetXml();
                setEncryptXml();
            }
        }



        # endregion HARMANJEET

    }
}
