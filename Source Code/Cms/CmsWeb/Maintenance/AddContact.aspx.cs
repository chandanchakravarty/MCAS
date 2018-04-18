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
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlClient;


/******************************************************************************************
<Author				: -   Ajit Singh Chahal
<Start Date				: -	3/11/2005 5:17:37 PM
<End Date				: -	
<Description				: - 	Code Behind for Adding/Updating/Deleting a contact to/from the system.
<Purpose				: - Code Behind for Code Behind Class for Adding/Updating/Deleting a contact to/from the system.
<Review Date				: - 
<Reviewed By			: - 	
Modification History

<Modified Date			: - 26/08/2005
<Modified By			: - Anurag Verma
<Purpose				: - Applying Null Check for buttons on aspx page
 
<Modified Date			: - 15/06/2010
<Modified By			: - Pradeep Kushwaha
<Purpose				: - Address Varification Based on the zipe code for Brazil implementation
*******************************************************************************************/
namespace Cms.CmsWeb.Maintenance
{
    /// <summary>
    /// Code Behind for Code Behind Class for Adding/Updating/Deleting a contact to/from the system.
    /// </summary>
    public class AddContact : Cms.CmsWeb.cmsbase
    {
        protected System.Web.UI.WebControls.TextBox txtCONTACT_CODE;
        protected System.Web.UI.WebControls.DropDownList cmbCONTACT_SALUTATION;
        protected System.Web.UI.WebControls.DropDownList cmbCONTACT_POS;
        protected System.Web.UI.WebControls.TextBox txtCONTACT_FNAME;
        protected System.Web.UI.WebControls.TextBox txtCONTACT_MNAME;
        protected System.Web.UI.WebControls.TextBox txtCONTACT_LNAME;
        protected System.Web.UI.WebControls.TextBox txtCONTACT_ADD1;
        protected System.Web.UI.WebControls.TextBox txtCONTACT_ADD2;
        protected System.Web.UI.WebControls.TextBox txtCONTACT_CITY;
        protected System.Web.UI.WebControls.DropDownList cmbCONTACT_STATE;
        protected System.Web.UI.WebControls.TextBox txtCONTACT_ZIP;
        protected System.Web.UI.WebControls.DropDownList cmbCONTACT_COUNTRY;
        protected System.Web.UI.WebControls.TextBox txtCONTACT_BUSINESS_PHONE;
        protected System.Web.UI.WebControls.TextBox txtCONTACT_EXT;
        protected System.Web.UI.WebControls.TextBox txtCONTACT_FAX;
        protected System.Web.UI.WebControls.TextBox txtCONTACT_MOBILE;
        protected System.Web.UI.WebControls.TextBox txtCONTACT_EMAIL;
        protected System.Web.UI.WebControls.TextBox txtCONTACT_PAGER;
        protected System.Web.UI.WebControls.TextBox txtCONTACT_HOME_PHONE;
        protected System.Web.UI.WebControls.TextBox txtCONTACT_TOLL_FREE;
        protected System.Web.UI.WebControls.TextBox txtCONTACT_NOTE;
        protected System.Web.UI.WebControls.TextBox txtNATIONALITY;
        protected System.Web.UI.WebControls.DropDownList cmbCONTACT_TYPE_ID;
        //protected System.Web.UI.WebControls.DropDownList cmbINDIVIDUAL_CONTACT_ID;//Commented to implement Coolite Combobox control -itrack 1557
        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
        protected System.Web.UI.WebControls.CheckBox chkAddSameAsAgent;
        protected System.Web.UI.WebControls.Label capMessages;
        protected System.Web.UI.WebControls.TextBox txtREGIONAL_ID_TYPE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCONTACT_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidZipeCodeVerificationMsg;
        protected System.Web.UI.WebControls.Label Caplook;

        protected System.Web.UI.WebControls.CompareValidator cpvREGIONAL_ID_ISSUE_DATE2;
        protected System.Web.UI.WebControls.CompareValidator cpvREGIONAL_ID_ISSUE_DATE;
        //Field validators
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvContactType;//Changed by Amit k. Mishra According to singapore Requirement
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCONTACT_TYPE_ID;//added by Amit k. Mishra According to singapore Requirement
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvIndContactId;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCONTACT_CODE;
        // protected System.Web.UI.WebControls.RequiredFieldValidator rfvSalutation;//Changed by Amit k. Mishra According to singapore Requirement
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCONTACT_SALUTATION;//added by Amit k. Mishra According to singapore Requirement
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvContactPosition;//Changed by Amit k. Mishra According to singapore Requirement
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCONTACT_POS;//added by Amit k. Mishra According to singapore Requirement
        // protected System.Web.UI.WebControls.RequiredFieldValidator rfvAddress1;//Changed by Amit k. Mishra According to singapore Requirement
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCONTACT_ADD1;//added by Amit k. Mishra According to singapore Requirement
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCONTACT_CITY;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCONTACT_ZIP;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvFirstName;//Changed by Amit k. Mishra According to singapore Requirement
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCONTACT_FNAME;//added by Amit k. Mishra According to singapore Requirement
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvLastName;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCONTACT_STATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCountry;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvMobile;
       // protected System.Web.UI.WebControls.RequiredFieldValidator rfvEmail;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvHomePhone;

        protected System.Web.UI.WebControls.RegularExpressionValidator revContactCode;
        protected System.Web.UI.WebControls.RegularExpressionValidator revFirstName;
        protected System.Web.UI.WebControls.RegularExpressionValidator revLastName;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCONTACT_MNAME;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCONTACT_ZIP;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCONTACT_BUSINESS_PHONE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revEmail;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCONTACT_MOBILE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCONTACT_HOME_PHONE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCONTACT_TOLL_FREE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCONTACT_ZIP_NUMERIC; //Changed by Aditya for TFS bug # 923
        protected System.Web.UI.WebControls.Image imgZipLookup;
        protected System.Web.UI.WebControls.HyperLink hlkZipLookup;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID_OLD;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCALLEDFROM;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCONTACT_STATE_ID;

        protected System.Web.UI.WebControls.Label capCONTACT_TYPE;
        protected System.Web.UI.WebControls.Label capContact_Code;
        protected System.Web.UI.WebControls.Label capTITLE;
        protected System.Web.UI.WebControls.Label capCONTACT_POSITION;
        protected System.Web.UI.WebControls.Label capCONTACT_FNAME;
        protected System.Web.UI.WebControls.Label capCONTACT_MNAME;
        protected System.Web.UI.WebControls.Label capCONTACT_LNAME;
        protected System.Web.UI.WebControls.Label capAGENT_ADDRESS;
        protected System.Web.UI.WebControls.Label capNUMBER;
        protected System.Web.UI.WebControls.Label capDISTRICT;
        protected System.Web.UI.WebControls.Label capADDRESS1;
        protected System.Web.UI.WebControls.Label capADDRESS2;
        protected System.Web.UI.WebControls.Label capCONTACT_CITY;
        protected System.Web.UI.WebControls.Label capCOUNTRY;
        protected System.Web.UI.WebControls.Label capCONTACT_STATE;
        protected System.Web.UI.WebControls.Label capCONTACT_ZIP;
        protected System.Web.UI.WebControls.Label capBusiness_Phone;
        protected System.Web.UI.WebControls.Label capMOBILE;
        protected System.Web.UI.WebControls.Label capFAX;
        protected System.Web.UI.WebControls.Label capCONTACT_PAGER;
        protected System.Web.UI.WebControls.Label capHome_Phone;
        protected System.Web.UI.WebControls.Label capTOLL_FREE_NO;
        protected System.Web.UI.WebControls.Label capEMAIL;
        protected System.Web.UI.WebControls.Label capNOTE;
        protected System.Web.UI.WebControls.Label capCONTACT_EXT;
        protected System.Web.UI.WebControls.Label capCPF_CNPJ;
        protected System.Web.UI.WebControls.TextBox txtCPF_CNPJ;
        protected System.Web.UI.WebControls.TextBox txtDISTRICT;
        protected System.Web.UI.WebControls.TextBox txtNUMBER;
        protected System.Web.UI.WebControls.Label capDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.TextBox txtDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.Label capACTIVITY;
        protected System.Web.UI.WebControls.DropDownList cmbACTIVITY;
        protected System.Web.UI.WebControls.Label capREGIONAL_ID;
        protected System.Web.UI.WebControls.TextBox txtREGIONAL_IDENTIFICATION;
        protected System.Web.UI.WebControls.Label capREGIONAL_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.TextBox txtREG_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.Label capREGIONAL_ID_ISSUE;
        protected System.Web.UI.WebControls.Label capNATIONALITY;
        protected System.Web.UI.WebControls.Label CapREGIONAL_ID_TYPE;
        protected System.Web.UI.WebControls.TextBox txtREG_ID_ISSUE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCPF_CNPJ;
        protected System.Web.UI.WebControls.HyperLink hlkREG_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.HyperLink hlkDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.RegularExpressionValidator revDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.RegularExpressionValidator revREGIONAL_ID_ISSUE_DATE;
        //protected System.Web.UI.HtmlControls.HtmlInputHidden hid_INDIVIDUAL_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_TYPE;
        protected System.Web.UI.HtmlControls.HtmlGenericControl spnIndContactID;
        //End: field validators

        //START:*********** Local variables to store valid control values  *************
        //int CONTACT_ID;
        //string CONTACT_CODE;
        //int CONTACT_TYPE_ID;
        //string CONTACT_SALUTATION;
        //string CONTACT_POS;
        //int INDIVIDUAL_CONTACT_ID;
        //string CONTACT_FNAME;
        //string CONTACT_MNAME;
        //string CONTACT_LNAME;
        //string CONTACT_ADD1;
        //string CONTACT_ADD2;
        //string CONTACT_CITY;
        //string CONTACT_STATE;
        //string CONTACT_ZIP;
        //string CONTACT_COUNTRY;
        //string CONTACT_BUSINESS_PHONE;
        //string CONTACT_EXT;
        //string CONTACT_FAX;
        //string CONTACT_MOBILE;
        //string CONTACT_EMAIL;
        //string CONTACT_PAGER;
        //string CONTACT_HOME_PHONE;
        //string CONTACT_TOLL_FREE;
        //string CONTACT_NOTE;
        //int CONTACT_AGENCY_ID;
        //string IS_ACTIVE;
        //int CREATED_BY;
        string oldXML;
        string strXML;
        public string javasciptmsg, javasciptCPFmsg, CPF_invalid;
        //END:*********** Local variables to store valid control values  *************
        private string strRowId, strFormSaved;
        protected System.Web.UI.WebControls.Label lblMessage;
        private int intLoggedInUserID;

        protected System.Web.UI.WebControls.RegularExpressionValidator revExt;
        protected System.Web.UI.WebControls.Label capINDIVIDUAL_CONTACT_ID;
        System.Resources.ResourceManager objResourceMgr;
        ClsContactsManager ObjContactsManager;
        protected Cms.CmsWeb.Controls.CmsButton btnBack;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvbusPhone;
        protected System.Web.UI.WebControls.Label lblINDIVIDUAL_CONTACT_NAME;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidINDIVIDUAL_CONTACT_ID;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCONTACT_PAGER;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCONTACT_FAX;
        protected Cms.CmsWeb.Controls.CmsButton btnPullCustomerAddress;
        protected System.Web.UI.WebControls.Label capPullCustomerAddress;
        protected Cms.CmsWeb.Controls.CmsButton btnDelete;
        protected System.Web.UI.WebControls.Label lblDelete;
        protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
        string strPageMode = "";
        private string strCalledFrom = "";
        #region Set Validators ErrorMessages
        /// <summary>
        /// Method to set validation control error masessages.
        /// Parameters: none
        /// Return Type: none
        /// </summary>
        private void SetErrorMessages()
        {
            rfvCONTACT_TYPE_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "51");
            rfvIndContactId.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "99");
            rfvCONTACT_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "53");
           // rfvSalutation.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "511");
            rfvCONTACT_SALUTATION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "511");//added by Amit k. Mishra According to singapore Requirement
            //rfvContactPosition.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "55");
            rfvCONTACT_POS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "55");//added by Amit k. Mishra According to singapore Requirement
            //rfvAddress1.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "787");
            rfvCONTACT_ADD1.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "787");
            rfvCONTACT_CITY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "56");
            rfvCONTACT_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "37");
            //rfvFirstName.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "57");
            rfvCONTACT_FNAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "57");//added by Amit k. Mishra According to singapore Requirement
            //rfvLastName.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "58");
            rfvCONTACT_STATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "35");
            rfvCountry.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "33");
            //rfvbusPhone.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "59");
            //rfvMobile.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "60");
            //rfvEmail.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "61");
           // rfvHomePhone.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "62");
            revCPF_CNPJ.ValidationExpression = aRegExpCpf_Cnpj;
            revREGIONAL_ID_ISSUE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1380");
            revCPF_CNPJ.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1857");
            revDATE_OF_BIRTH.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1380");
            revDATE_OF_BIRTH.ValidationExpression = aRegExpDate;
            revREGIONAL_ID_ISSUE_DATE.ValidationExpression = aRegExpDate;

            revContactCode.ValidationExpression = aRegExpClientName;
            revFirstName.ValidationExpression = aRegExpClientName;
            revLastName.ValidationExpression = aRegExpClientName;
            revCONTACT_MNAME.ValidationExpression = aRegExpClientName;
            revCONTACT_ZIP.ValidationExpression = aRegExpZipBrazil;            
            revEmail.ValidationExpression = aRegExpEmail;

            revCONTACT_BUSINESS_PHONE.ValidationExpression = aRegExpAgencyPhone;
            revCONTACT_MOBILE.ValidationExpression = aRegExpAgencyPhone;
            revCONTACT_HOME_PHONE.ValidationExpression = aRegExpAgencyPhone;
            revCONTACT_TOLL_FREE.ValidationExpression = aRegExpTollFree;
            revCONTACT_FAX.ValidationExpression = aRegExpAgencyPhone; //aRegExpFaxBrazil;
            revCONTACT_PAGER.ValidationExpression = aRegExpAgencyPhone;
            if (GetLanguageID() == "2"){

                revCONTACT_BUSINESS_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2002");
                revCONTACT_MOBILE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2000");
                revCONTACT_HOME_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1999");
                revCONTACT_TOLL_FREE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2067");
                revCONTACT_FAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1921");
                revCONTACT_PAGER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2000");

            }

            else
            {
                revCONTACT_BUSINESS_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2002");
                revCONTACT_MOBILE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2000");
                revCONTACT_HOME_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1999");
                revCONTACT_TOLL_FREE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2067");
                revCONTACT_FAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1085");
                revCONTACT_PAGER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2000");
            }

            
            
            revExt.ValidationExpression = aRegExpExtn;

            revContactCode.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
            revFirstName.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
            revLastName.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
            revCONTACT_MNAME.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
            revCONTACT_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1181");//Cms.CmsWeb.ClsMessages.FetchGeneralMessage("24");

            //revBusPhone.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083"); //Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
            revEmail.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("23");
            //revMobile.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1086");  //Cms.CmsWeb.ClsMessages.FetchGeneralMessage("16");

            //revHomePhone.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083"); //Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
            //revTollFreeNo.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1088");// Cms.CmsWeb.ClsMessages.FetchGeneralMessage("211");
            //revFax.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1085"); //Cms.CmsWeb.ClsMessages.FetchGeneralMessage("15");
            //revCONTACT_PAGER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1087");//1087 Cms.CmsWeb.ClsMessages.FetchGeneralMessage("200");
            revExt.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("25");
            cpvREGIONAL_ID_ISSUE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1402");
            cpvREGIONAL_ID_ISSUE_DATE2.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1403");
            // regular expression in case  country selected  "other".
            revCONTACT_ZIP_NUMERIC.ValidationExpression = aRegExpAlphaNumWithDash; //Changed by Aditya for TFS bug # 923
            revCONTACT_ZIP_NUMERIC.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("467"); //Changed by Aditya for TFS bug # 923
            rfvIndContactId.Visible = false;
        }
        #endregion

        protected Coolite.Ext.Web.Store StoreINDIVIDUAL_CONTACT_ID;
        protected Coolite.Ext.Web.ComboBox CCcmbINDIVIDUAL_CONTACT_ID;  //Changed by Aditya for TFS bug # 923
        public String EntityId = String.Empty;
        private void Page_Load(object sender, System.EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(AddContact));

            #region setting screen id
            if (Request.QueryString["CallFrom"] != null && Request.QueryString["CallFrom"].ToString().Trim() != "")
            {
                strCalledFrom = Request.QueryString["CallFrom"].ToString().Trim();

            }
            switch (strCalledFrom)
            {                                  


                case "CUSTOMER":
                    base.ScreenId = "134_5_0";
                    break;

                default:
                    base.ScreenId = "38_0";
                    break;
            }

            #endregion
           // base.ScreenId = "38_0";

            Page.DataBind();
            
            btnDelete.Enabled = false;
            btnActivateDeactivate.Enabled = false;
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
           
            btnReset.Attributes.Add("onclick", "javascript:return ResetTheForm();");           
            cmbCONTACT_TYPE_ID.Attributes.Add("onchange", "Javascript: ComboIndexChanged();");
            txtCONTACT_BUSINESS_PHONE.Attributes.Add("onBlur", "javascript:FormatBrazilPhone();CheckIfPhoneEmpty();DisableExt('txtCONTACT_BUSINESS_PHONE','txtCONTACT_EXT');");
            cmbCONTACT_COUNTRY.Attributes.Add("onchange", "fillstateFromCountry();");
            cmbCONTACT_STATE.Attributes.Add("onchange", "setStateId();");
            txtCONTACT_FNAME.Attributes.Add("onchange", "javascript:GenerateCustomerCode('txtCONTACT_FNAME','txtCONTACT_LNAME','txtCONTACT_CODE');");
            txtCONTACT_LNAME.Attributes.Add("onchange", "javascript:GenerateCustomerCode('txtCONTACT_FNAME','txtCONTACT_LNAME','txtCONTACT_CODE');");
            btnSave.Attributes.Add("onclick", "setStateId();");
          
            //

            // Added by Swarup on 30-mar-2007
            imgZipLookup.Attributes.Add("style", "cursor:hand");
            base.VerifyAddressDetailsBR(hlkZipLookup, txtCONTACT_ADD1, txtCONTACT_ADD2
                , txtCONTACT_CITY, cmbCONTACT_STATE, txtCONTACT_ZIP);

            //txtCONTACT_EXT.Attributes.Add("KeyPress","Javascript:DisableExt('txtCONTACT_BUSINESS_PHONE','txtCONTACT_EXT');");
            intLoggedInUserID = int.Parse(base.GetUserId());

            
            btnBack.Attributes.Add("onclick", "javascript:return DoBack();");
            //SetCultureThread(GetLanguageCode());
            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddContact", System.Reflection.Assembly.GetExecutingAssembly());
            hidZipeCodeVerificationMsg.Value = objResourceMgr.GetString("hidZipeCodeVerificationMsg");

            //SetSecurityXML(int.Parse(ScreenId), intLoggedInUserID);
            lblMessage.Visible = false;

            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************

            btnBack.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Read;
            btnBack.PermissionString = gstrSecurityXML;
            btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;
            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;
            btnDelete.CmsButtonClass = CmsButtonType.Delete;
            btnDelete.PermissionString = gstrSecurityXML;
            btnPullCustomerAddress.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnPullCustomerAddress.PermissionString = gstrSecurityXML;

            //  objResourceMgr = new System.Resources.ResourceManager("Cms.Client.Aspx.AddCustomer", System.Reflection.Assembly.GetExecutingAssembly());


            base.RequiredPullCustAdd(txtCONTACT_ADD1, txtCONTACT_ADD2, txtCONTACT_CITY , cmbCONTACT_COUNTRY, cmbCONTACT_STATE, txtCONTACT_ZIP 
                , btnPullCustomerAddress,txtNUMBER,txtDISTRICT );


            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************


            if (hidCONTACT_ID.Value != null && hidCONTACT_ID.Value.ToString().Length > 0)
                strPageMode = "Edit";
            else
                strPageMode = "Add";

            javasciptmsg = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1861");
            javasciptCPFmsg = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1857");
            //javasciptCNPJmsg = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1901");
            CPF_invalid = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1859");
            //CNPJ_invalid = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1903");



            if (!Page.IsPostBack)
            {

                Session["stateid"] = "0";
                SetCaptions();
                SetErrorMessages();               
                FillCombo();
               
                string strSysID = GetSystemId();
                if (strSysID == "ALBAUAT")
                    strSysID = "ALBA";

                //Setting xml for the page to be displayed in page controls
                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "/CmsWeb/support/PageXml/" + strSysID, "AddContact.xml"))
                    setPageControls(Page, Request.PhysicalApplicationPath + "/CmsWeb/support/PageXml/" + strSysID + "/AddContact.xml");  
                
                
                ClsContactsManager.GetContactTypesInDropDown(cmbCONTACT_TYPE_ID,ClsCommon.BL_LANG_ID);
                if (Request.QueryString["CallFrom"].Trim().ToUpper() == "CUSTOMER")
                {                   
                    FillCustomername(); // added by aditya for itrack 1483
                }
                
                
                if (Request.QueryString["CONTACT_ID"] != null && Request.QueryString["CONTACT_ID"].ToString().Length > 0)
                {
                    SetXml(Request.QueryString["CONTACT_ID"]);
                    strPageMode = "Edit";
                }
                else if (hidCONTACT_ID.Value != null && hidCONTACT_ID.Value.ToString().Length > 0)
                {
                    SetXml(hidCONTACT_ID.Value.ToString());
                    strPageMode = "Edit";
                }
                else
                    strPageMode = "Add";
            }
            if (Request.QueryString["CallFrom"] != null && Request.QueryString["CallFrom"] != "")
            {
                hidCALLEDFROM.Value = Request.QueryString["CallFrom"].ToString();
            }

            if (Request.QueryString["CONTACTTYPEID"] != null && Request.QueryString["CONTACTTYPEID"].ToString().Length > 0)
            {
                cmbCONTACT_TYPE_ID.Enabled = false;
                //cmbINDIVIDUAL_CONTACT_ID.Enabled = false;//Commented to implement Coolite Combobox control -itrack 1557
                CCcmbINDIVIDUAL_CONTACT_ID.Enabled = false;  //Changed by Aditya for TFS bug # 923
                btnBack.Visible = true;
                lblINDIVIDUAL_CONTACT_NAME.Visible = true;
                if (Request.QueryString["CallFrom"] != null)
                    if (Request.QueryString["CallFrom"].Trim().ToUpper() == "CUSTOMER")
                    {
                        //cmbINDIVIDUAL_CONTACT_ID.Visible = true;//Commented to implement Coolite Combobox control -itrack 1557
                        CCcmbINDIVIDUAL_CONTACT_ID.Visible = true;  //Changed by Aditya for TFS bug # 923
                        
                    }
                    else
                    {
                        //cmbINDIVIDUAL_CONTACT_ID.Visible = false;//Commented to implement Coolite Combobox control -itrack 1557
                        CCcmbINDIVIDUAL_CONTACT_ID.Visible = false;  //Changed by Aditya for TFS bug # 923
                        
                    }
                else
                {
                    //cmbINDIVIDUAL_CONTACT_ID.Visible = false;//Commented to implement Coolite Combobox control -itrack 1557
                    CCcmbINDIVIDUAL_CONTACT_ID.Visible = false; //Changed by Aditya for TFS bug # 923
                     
                }
                rfvIndContactId.Visible = false;
            }
            // code for providing the back to serach button only on customer Detail form tabs. 
            if (Request.QueryString["BackOption"] != null && Request.QueryString["BackOption"].ToString() == "Y")
            {
                btnBack.Visible = true;
            }



            #region "!post back"
            if (!Page.IsPostBack)
            {
                hlkREG_ID_ISSUE_DATE.Attributes.Add("OnClick", "fPopCalendar(txtREG_ID_ISSUE_DATE,txtREG_ID_ISSUE_DATE)"); //Javascript Implementation for Calender	
                hlkDATE_OF_BIRTH.Attributes.Add("OnClick", "fPopCalendar(txtDATE_OF_BIRTH,txtDATE_OF_BIRTH)");

             

                if (!(Request.QueryString["CONTACT_TYPE_ID"] == null || Request.QueryString["CONTACT_TYPE_ID"].ToString().Length == 0))
                {
                    FillIndividualContactId(Request.QueryString["CONTACT_TYPE_ID"].ToString());
                    //lblINDIVIDUAL_CONTACT_NAME.Text = GetIndividualContactName(Request.QueryString["CONTACT_TYPE_ID"].ToString());
                    //hidINDIVIDUAL_CONTACT_ID.Value  = Request.QueryString["EntityId"].ToString();
                }
                else if (!(Request.QueryString["CONTACTTYPEID"] == null || Request.QueryString["CONTACTTYPEID"].ToString().Length == 0))
                {
                    //FillIndividualContactId(Request.QueryString["CONTACTTYPEID"].ToString());
                    lblINDIVIDUAL_CONTACT_NAME.Text = GetIndividualContactName(Request.QueryString["CONTACTTYPEID"].ToString());
                    hidINDIVIDUAL_CONTACT_ID.Value = Request.QueryString["EntityId"].ToString();
                }
                else
                {
                    //ClsAgency.GetAgencyNamesInDropDown(cmbINDIVIDUAL_CONTACT_ID);//Commented to implement Coolite Combobox control -itrack 1557
                    DataTable dtAgencyDetails = new DataTable();
                    ClsAgency.GetAgencyNamesInDropDown(ref dtAgencyDetails);
                    this.FillCooliteComboBox(dtAgencyDetails, "AGENCY_ID", "AGENCY_DISPLAY_NAME", Request.QueryString["EntityId"].ToString());


                    capINDIVIDUAL_CONTACT_ID.Text = objResourceMgr.GetString("capINDIVIDUAL_CONTACT_ID"); //"Agency";
                }


                #region "Loading singleton"
                if ((Request.QueryString["CONTACT_ID"] != null && Request.QueryString["CONTACT_ID"].ToString().Length > 0))
                {
                    hidCONTACT_ID.Value = Request.QueryString["CONTACT_ID"].ToString();
                }
                else
                {
                    if (hidCONTACT_ID.Value == "")
                        hidCONTACT_ID.Value = "New";
                }


               


                if (Request.QueryString["CallFrom"] != null) 

                {
                    if (Request.QueryString["CallFrom"].Trim().ToUpper() == "CUSTOMER")  
                    {
                        cmbCONTACT_TYPE_ID.SelectedIndex = cmbCONTACT_TYPE_ID.Items.IndexOf(cmbCONTACT_TYPE_ID.Items.FindByValue("2"));
                        FillIndividualContactId(cmbCONTACT_TYPE_ID.SelectedValue.ToString());
                        //cmbINDIVIDUAL_CONTACT_ID.SelectedIndex = cmbINDIVIDUAL_CONTACT_ID.Items.IndexOf(cmbINDIVIDUAL_CONTACT_ID.Items.FindByText(Request.QueryString["EntityId"].ToString()));
                        capINDIVIDUAL_CONTACT_ID.Text = objResourceMgr.GetString("capINDIVIDUAL_CONTACT_ID_2");
                        //cmbINDIVIDUAL_CONTACT_ID.Visible = true;//Commented to implement Coolite Combobox control -itrack 1557
                        CCcmbINDIVIDUAL_CONTACT_ID.Visible = true;  //Changed by Aditya for TFS bug # 923
                        cmbCONTACT_TYPE_ID.Enabled = false;
                        //cmbINDIVIDUAL_CONTACT_ID.Enabled = false;//Commented to implement Coolite Combobox control -itrack 1557
                        CCcmbINDIVIDUAL_CONTACT_ID.Enabled = false; //Changed by Aditya for TFS bug # 923

                        lblINDIVIDUAL_CONTACT_NAME.Text = "";
                        //hidINDIVIDUAL_CONTACT_ID.Value = cmbINDIVIDUAL_CONTACT_ID.SelectedValue;//Commented to implement Coolite Combobox control -itrack 1557
                        hidINDIVIDUAL_CONTACT_ID.Value = CCcmbINDIVIDUAL_CONTACT_ID.SelectedItem.Value;  //Changed by Aditya for TFS bug # 923

                    }
                    else
                    {
                        cmbCONTACT_TYPE_ID.Enabled = true;
                        //cmbINDIVIDUAL_CONTACT_ID.Enabled = true;////Commented to implement Coolite Combobox control -itrack 1557
                        CCcmbINDIVIDUAL_CONTACT_ID.Enabled = true; //Changed by Aditya for TFS bug # 923
                         
                    }

                }
                else
                {
                    cmbCONTACT_TYPE_ID.Enabled = true;
                    //cmbINDIVIDUAL_CONTACT_ID.Enabled = true;//Commented to implement Coolite Combobox control -itrack 1557
                    CCcmbINDIVIDUAL_CONTACT_ID.Enabled = true; //Changed by Aditya for TFS bug # 923
                     
                }
                 //For Localization Settings By kuldeep
          

    #endregion
            }
            else
            {

            }

          

            #endregion

        }//end pageload

        private void FillCustomername()     // Added by aditya, for itrack - 1179
        { string CustomerId= Request.QueryString["EntityId"].ToString();
            ClsCustomer objCustomer = new ClsCustomer();
            DataTable dt = ClsCustomer.GetCustomer();
            dt = dt.Select("CUSTOMER_ID=" + CustomerId.ToString()).CopyToDataTable().Copy();
           
            hidCUSTOMER_TYPE.Value = dt.Rows[0]["CUSTOMER_TYPE"].ToString();
            
        }

        private void FillCombo()
        {
            DataTable dt2 = Cms.BusinessLayer.BlApplication.ClsLocation.SELECTACTIVITYTYPE();
            DataView dv = new DataView(dt2, "TYPE=11110", "ACTIVITY_DESC", DataViewRowState.CurrentRows);
            dv.Sort = "ACTIVITY_DESC";
            cmbACTIVITY.DataSource = dv;
            cmbACTIVITY.DataTextField = "ACTIVITY_DESC";
            cmbACTIVITY.DataValueField = "ACTIVITY_ID";
            cmbACTIVITY.DataBind();
            cmbACTIVITY.Items.Insert(0, "");
            cmbACTIVITY.SelectedIndex = 0; 

           // cmbCONTACT_SALUTATION.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("%SAL");
            DataTable dtsal = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("%SAL").Select("", "LookupDesc").CopyToDataTable<DataRow>();
            cmbCONTACT_SALUTATION.DataSource = dtsal;
            cmbCONTACT_SALUTATION.DataTextField = "LookupDesc";
            cmbCONTACT_SALUTATION.DataValueField = "LookupID";
            cmbCONTACT_SALUTATION.DataBind();
            cmbCONTACT_SALUTATION.Items.Insert(0, "");

            DataTable dt = Cms.CmsWeb.ClsFetcher.Country;

            cmbCONTACT_COUNTRY.DataSource = dt;
            cmbCONTACT_COUNTRY.DataTextField = "Country_Name";
            cmbCONTACT_COUNTRY.DataValueField = "Country_Id";
            cmbCONTACT_COUNTRY.DataBind();

            dt = Cms.CmsWeb.ClsFetcher.State;
            cmbCONTACT_STATE.DataSource = dt;
            cmbCONTACT_STATE.DataTextField = "STATE_NAME";
            cmbCONTACT_STATE.DataValueField = "STATE_ID";
            cmbCONTACT_STATE.DataBind();
            //Default Country Brazil

            cmbCONTACT_COUNTRY.SelectedIndex = cmbCONTACT_COUNTRY.Items.IndexOf(cmbCONTACT_COUNTRY.Items.FindByValue("5"));
            PopulateStateDropDown(cmbCONTACT_STATE, int.Parse(cmbCONTACT_COUNTRY.SelectedValue));

            if (hidCUSTOMER_TYPE.Value == "11110")
            {
                cmbCONTACT_POS.Items.Insert(0, (ClsCommon.BL_LANG_ID == 2 ? "CEO" : "CEO"));
                cmbCONTACT_POS.Items.Insert(1, (ClsCommon.BL_LANG_ID == 2 ? "subscritor" : "UnderWriter "));
            }
            else
            
            {
                cmbCONTACT_POS.Items.Insert(0, (ClsCommon.BL_LANG_ID == 2 ? "subscritor" : "UnderWriter "));
            }

        
        }
        private void SetXml(string contactId)
        {
        
            if (Request.QueryString["CONTACT_TYPE_ID"] != null)
                strXML = ClsContactsManager.GetXmlForPageControls(contactId, Request.QueryString["CONTACT_TYPE_ID"].ToString(), Request.QueryString["EntityId"].ToString());
            else
                strXML = ClsContactsManager.GetXmlForPageControls(contactId, Request.QueryString["CONTACTTYPEID"].ToString(), Request.QueryString["EntityId"].ToString());
            
            if (ClsCommon.FetchValueFromXML("DATE_OF_BIRTH", strXML) != "" && ClsCommon.FetchValueFromXML("DATE_OF_BIRTH", strXML) != "0")
            {
                //DateTime RID = ;
                this.txtDATE_OF_BIRTH.Text = ClsCommon.FetchValueFromXML("DATE_OF_BIRTH", strXML);

            }
            if (ClsCommon.FetchValueFromXML("REG_ID_ISSUE_DATE", strXML) != "" && ClsCommon.FetchValueFromXML("REG_ID_ISSUE_DATE", strXML) != "0")
            {
                 
                this.txtREG_ID_ISSUE_DATE.Text = ClsCommon.FetchValueFromXML("REG_ID_ISSUE_DATE", strXML);

            }


            string CONTACT_ID = ClsCommon.FetchValueFromXML("CONTACT_ID", strXML).ToString();
          
            if (CONTACT_ID != "" && CONTACT_ID != "0")
            {
                txtCONTACT_CODE.Attributes.Add("disabled", "true");
                rfvCONTACT_CODE.Attributes.Add("isValid", "true");
                rfvCONTACT_CODE.Attributes.Add("enabled", "false");
                rfvCONTACT_CODE.Style.Add("display", "none");
            }

            PopulatePageFromModelObject(this.Page, strXML);
            hidOldData.Value = strXML;
            string IsActiveStatus=ClsCommon.FetchValueFromXML("IS_ACTIVE", strXML);
            hidIS_ACTIVE.Value = IsActiveStatus.ToString();

            FetchCountryState(strXML);
            hidSTATE_ID.Value = ClsCommon.FetchValueFromXML("CONTACT_STATE", strXML);

            txtNUMBER.Text = ClsCommon.FetchValueFromXML("NUMBER", strXML);
            txtDISTRICT.Text = ClsCommon.FetchValueFromXML("DISTRICT", strXML); 
            if(hidSTATE_ID.Value!="")
            cmbCONTACT_STATE.SelectedValue = hidSTATE_ID.Value;
            if (contactId != "") 
            {
               btnActivateDeactivate.Enabled = true;
               btnDelete.Enabled = true;
               btnActivateDeactivate.Text= ClsMessages.FetchActivateDeactivateButtonsText(IsActiveStatus);
            }
            if (cmbCONTACT_STATE.SelectedValue != "")
            {
                rfvCONTACT_STATE.Attributes.Add("style", "display:none");
                rfvCONTACT_STATE.Attributes.Add("isvalid", "true");
                rfvCONTACT_STATE.Attributes.Add("enabled", "false");

            }
            EntityId = ClsCommon.FetchValueFromXML("INDIVIDUAL_CONTACT_ID", strXML);
            FillIndividualContactId(ClsCommon.FetchValueFromXML("CONTACT_TYPE_ID", strXML));
            
        }


        #region Method code to do form's processing
        /// <summary>
        /// Fetch form's value and stores into variables.
        /// </summary>
        private Model.Maintenance.ClsContactsManagerInfo getFormValue()
        {
            //Creating the Model object for holding the New data
            ClsContactsManagerInfo objContactsManagerInfo;
            objContactsManagerInfo = new ClsContactsManagerInfo();
            objContactsManagerInfo.CONTACT_CODE = txtCONTACT_CODE.Text;
            objContactsManagerInfo.CONTACT_TYPE_ID = int.Parse(cmbCONTACT_TYPE_ID.SelectedValue);
            objContactsManagerInfo.CONTACT_SALUTATION = cmbCONTACT_SALUTATION.SelectedValue;
            objContactsManagerInfo.CONTACT_POS = cmbCONTACT_POS.SelectedValue;
            //Commented to implement Coolite Combobox control -itrack 1557
            //if (objContactsManagerInfo.CONTACT_TYPE_ID != 6)//if not equal to personal
            //{
            //    if (cmbINDIVIDUAL_CONTACT_ID.Visible)
            //        objContactsManagerInfo.INDIVIDUAL_CONTACT_ID = int.Parse(cmbINDIVIDUAL_CONTACT_ID.SelectedValue);
            //    else
            //        objContactsManagerInfo.INDIVIDUAL_CONTACT_ID = int.Parse(hidINDIVIDUAL_CONTACT_ID.Value);
            //}
            //Commented till here 
            if (objContactsManagerInfo.CONTACT_TYPE_ID != 6)//if not equal to personal
            {
                if (CCcmbINDIVIDUAL_CONTACT_ID.Visible)  //Changed by Aditya for TFS bug # 923
                    objContactsManagerInfo.INDIVIDUAL_CONTACT_ID = int.Parse(CCcmbINDIVIDUAL_CONTACT_ID.SelectedItem.Value); //Changed by Aditya for TFS bug # 923
                else
                    objContactsManagerInfo.INDIVIDUAL_CONTACT_ID = int.Parse(hidINDIVIDUAL_CONTACT_ID.Value);
            }

            objContactsManagerInfo.CONTACT_FNAME = txtCONTACT_FNAME.Text;
            objContactsManagerInfo.CONTACT_MNAME = txtCONTACT_MNAME.Text;
            objContactsManagerInfo.CONTACT_LNAME = txtCONTACT_LNAME.Text;
            objContactsManagerInfo.CONTACT_ADD1 = txtCONTACT_ADD1.Text;
            objContactsManagerInfo.CONTACT_ADD2 = txtCONTACT_ADD2.Text;
            objContactsManagerInfo.CONTACT_CITY = txtCONTACT_CITY.Text;
            objContactsManagerInfo.NUMBER = txtNUMBER.Text;
            objContactsManagerInfo.DISTRICT = txtDISTRICT.Text;

            objContactsManagerInfo.CONTACT_STATE = hidSTATE_ID.Value;
            
            objContactsManagerInfo.CONTACT_ZIP = txtCONTACT_ZIP.Text;
            objContactsManagerInfo.CONTACT_COUNTRY = cmbCONTACT_COUNTRY.SelectedValue;
            objContactsManagerInfo.CONTACT_BUSINESS_PHONE = txtCONTACT_BUSINESS_PHONE.Text;
            objContactsManagerInfo.CONTACT_EXT = txtCONTACT_EXT.Text;
            objContactsManagerInfo.CONTACT_FAX = txtCONTACT_FAX.Text;
            objContactsManagerInfo.CONTACT_MOBILE = txtCONTACT_MOBILE.Text;
            objContactsManagerInfo.CONTACT_EMAIL = txtCONTACT_EMAIL.Text;
            objContactsManagerInfo.CONTACT_PAGER = txtCONTACT_PAGER.Text;
            objContactsManagerInfo.CONTACT_HOME_PHONE = txtCONTACT_HOME_PHONE.Text;
            objContactsManagerInfo.CONTACT_TOLL_FREE = txtCONTACT_TOLL_FREE.Text;
            objContactsManagerInfo.CONTACT_NOTE = txtCONTACT_NOTE.Text;

            objContactsManagerInfo.CONTACT_AGENCY_ID = base.GetAgencycode();
            objContactsManagerInfo.IS_ACTIVE = hidIS_ACTIVE.Value;
            objContactsManagerInfo.CREATED_BY = int.Parse(GetUserId());
            if (cmbACTIVITY.SelectedItem != null && cmbACTIVITY.SelectedItem.Value != "")
                objContactsManagerInfo.ACTIVITY = int.Parse(cmbACTIVITY.SelectedItem.Value);
            if (txtDATE_OF_BIRTH.Text != "")
            {
                objContactsManagerInfo.DATE_OF_BIRTH = Convert.ToDateTime(txtDATE_OF_BIRTH.Text);
            }
            if (txtREG_ID_ISSUE_DATE.Text != "")
            {
                objContactsManagerInfo.REG_ID_ISSUE_DATE = Convert.ToDateTime(txtREG_ID_ISSUE_DATE.Text);//REGIONAL_ID_ISSUE_DATE}
            }
            objContactsManagerInfo.REG_ID_ISSUE = txtREG_ID_ISSUE.Text.ToString();
            objContactsManagerInfo.REGIONAL_IDENTIFICATION = txtREGIONAL_IDENTIFICATION.Text.ToString();
            objContactsManagerInfo.CPF_CNPJ = txtCPF_CNPJ.Text.ToString();

            if (txtREGIONAL_ID_TYPE.Text != "")
            {
                objContactsManagerInfo.REGIONAL_ID_TYPE = txtREGIONAL_ID_TYPE.Text;

            }
            if (txtNATIONALITY.Text != "")
            {
                objContactsManagerInfo.NATIONALITY = txtNATIONALITY.Text;

            }
            //These assignments are common to all pages.
            strFormSaved = hidFormSaved.Value;
            strRowId = hidCONTACT_ID.Value;
            oldXML = hidOldData.Value;
            //Returning the model object

            return objContactsManagerInfo;
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
            this.cmbCONTACT_TYPE_ID.SelectedIndexChanged += new System.EventHandler(this.cmbCONTACT_TYPE_ID_SelectedIndexChanged);
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
                //getFormValue();
                {
                    int intRetVal; //For retreiving the return value of business class save function
                    ObjContactsManager = new ClsContactsManager();

                    //Retreiving the form values into model class object
                    ClsContactsManagerInfo objContactsManagerInfo = getFormValue();
                    objContactsManagerInfo.CREATED_DATETIME = DateTime.Now;
                    if (strRowId.ToUpper().Equals("NEW")) //save case 
                    {
                        objContactsManagerInfo.IS_ACTIVE = "Y";

                        intRetVal = ObjContactsManager.Add(objContactsManagerInfo,hidCALLEDFROM.Value);
                        if (intRetVal > 0)
                        {
                            //Set the coolite control value while update itrack- 1557
                            FillIndividualContactId(cmbCONTACT_TYPE_ID.SelectedValue.ToString());
                            hidCONTACT_ID.Value = ObjContactsManager.ConatctId.ToString();
                            lblMessage.Text = ClsMessages.GetMessage(ScreenId, "29");
                            hidFormSaved.Value = "1";
                            hidIS_ACTIVE.Value = "Y";
                            SetXml(ObjContactsManager.ConatctId.ToString());
                            btnActivateDeactivate.Enabled = true;
                            btnDelete.Enabled = true;
                            revCONTACT_ZIP.Enabled = false;


                        }
                        else if (ObjContactsManager.ConatctId == -1)
                        {
                            lblMessage.Text = ClsMessages.GetMessage(ScreenId, "18");
                            hidFormSaved.Value = "2";
                            btnActivateDeactivate.Enabled = false;
                        }
                        else
                        {
                            lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                            hidFormSaved.Value = "2";
                        }
                        lblMessage.Visible = true;
                    } // end save case
                    else //UPDATE CASE
                    {
                        ClsContactsManagerInfo objOldContactsManagerInfo = new ClsContactsManagerInfo();
                        base.PopulateModelObject(objOldContactsManagerInfo, hidOldData.Value);
                        objContactsManagerInfo.IS_ACTIVE = "Y";
                        objContactsManagerInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                        objContactsManagerInfo.MODIFIED_BY = int.Parse(GetUserId());
                        objContactsManagerInfo.CONTACT_ID = int.Parse(hidCONTACT_ID.Value.ToString());
                        objContactsManagerInfo.CONTACT_CODE = objOldContactsManagerInfo.CONTACT_CODE;
                        intRetVal = ObjContactsManager.Update(objContactsManagerInfo, objOldContactsManagerInfo, hidCALLEDFROM.Value);

                        /*							if((intReturnValue=ObjContactsManager.Update (CONTACT_CODE,
                                                        CONTACT_TYPE_ID,
                                                        CONTACT_SALUTATION,
                                                        CONTACT_POS,
                                                        INDIVIDUAL_CONTACT_ID,
                                                        CONTACT_FNAME,
                                                        CONTACT_MNAME,
                                                        CONTACT_LNAME,
                                                        CONTACT_ADD1,
                                                        CONTACT_ADD2,
                                                        CONTACT_CITY,
                                                        CONTACT_STATE,
                                                        CONTACT_ZIP,
                                                        CONTACT_COUNTRY,
                                                        CONTACT_BUSINESS_PHONE,
                                                        CONTACT_EXT,
                                                        CONTACT_FAX,
                                                        CONTACT_MOBILE,
                                                        CONTACT_EMAIL,
                                                        CONTACT_PAGER,
                                                        CONTACT_HOME_PHONE,
                                                        CONTACT_TOLL_FREE,
                                                        CONTACT_NOTE,
                                                        CONTACT_AGENCY_ID,int.Parse(GetUserId()),oldXML))>0)*/
                        if (intRetVal > 0)
                        {
                            //Set the coolite control value while update itrack- 1557
                            FillIndividualContactId(cmbCONTACT_TYPE_ID.SelectedValue.ToString());
                            lblMessage.Text = ClsMessages.GetMessage(ScreenId, "31");
                            hidFormSaved.Value = "1";
                            SetXml(hidCONTACT_ID.Value.ToString());
                            revCONTACT_ZIP.Enabled = false;

                        }
                        else if (intRetVal == -1)
                        {
                            lblMessage.Text = ClsMessages.GetMessage(ScreenId, "18");
                            hidFormSaved.Value = "2";
                        }
                        else
                        {
                            lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                            hidFormSaved.Value = "2";
                        }
                        lblMessage.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(ScreenId, "21") + " - " + ex.Message + "\n Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";
                //ExceptionManager.Publish(ex);
            }
            finally
            {
                if (ObjContactsManager != null)
                    ObjContactsManager.Dispose();
                // maintain selected value of country and state dropdown in case when contact code already exists
                if (cmbCONTACT_COUNTRY.SelectedValue != "")
                {
                    PopulateStateDropDown(cmbCONTACT_STATE, int.Parse(cmbCONTACT_COUNTRY.SelectedValue));
                }
                if (hidSTATE_ID.Value != "")
                    cmbCONTACT_STATE.SelectedValue = hidSTATE_ID.Value;
                if (cmbCONTACT_STATE.SelectedValue != "")
                {
                    rfvCONTACT_STATE.Attributes.Add("style", "display:none");
                    rfvCONTACT_STATE.Attributes.Add("isvalid", "true");
                    rfvCONTACT_STATE.Attributes.Add("enabled", "false");

                }
            }
        }
        /// <summary>
        /// Activates and deactivates the contact.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
        {
            try
            {
                strRowId = hidCONTACT_ID.Value;
                ClsContactsManager ObjContactsManager = new ClsContactsManager();
                Cms.BusinessLayer.BlCommon.stuTransactionInfo objStuTransactionInfo = new Cms.BusinessLayer.BlCommon.stuTransactionInfo();
                objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
                objStuTransactionInfo.loggedInUserName = GetUserName();

                string customInfo = "";
                customInfo += ";Contact Salutation = " + cmbCONTACT_SALUTATION.Items[cmbCONTACT_SALUTATION.SelectedIndex].Text;
                customInfo += ";Contact Position = " + cmbCONTACT_POS.Items[cmbCONTACT_POS.SelectedIndex].Text;
                customInfo += ";Contact First Name = " + txtCONTACT_FNAME.Text;
                customInfo += ";Contact Last Name = " + txtCONTACT_LNAME.Text;
                if (hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
                    {
                    objStuTransactionInfo.transactionDescription = "Contact List Deactivated Successfully.";
                    ObjContactsManager.TransactionInfoParams = objStuTransactionInfo;
                    ObjContactsManager.ContactActivateDeactivate(strRowId, "N", customInfo, int.Parse(GetUserId()));
                    lblMessage.Text = ClsMessages.GetMessage(ScreenId, "41");
                    hidIS_ACTIVE.Value = "N";
                }
                else
                {
                    objStuTransactionInfo.transactionDescription = "Contact List Activated Successfully.";
                    ObjContactsManager.TransactionInfoParams = objStuTransactionInfo;
                    ObjContactsManager.ContactActivateDeactivate(strRowId, "Y", customInfo,int.Parse(GetUserId()));
                    lblMessage.Text = ClsMessages.GetMessage(ScreenId, "40");
                    hidIS_ACTIVE.Value = "Y";
                }
                hidFormSaved.Value = "1";
                SetXml(hidCONTACT_ID.Value.ToString());
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
                lblMessage.Visible = true;
                if (ObjContactsManager != null)
                    ObjContactsManager.Dispose();
            }
        }

        #endregion

        private void cmbCONTACT_TYPE_ID_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //cmbINDIVIDUAL_CONTACT_ID.Visible = true;//Commented to implement Coolite Combobox control -itrack 1557
            CCcmbINDIVIDUAL_CONTACT_ID.Visible = true; //Changed by Aditya for TFS bug # 923
            rfvIndContactId.Visible = false;
            capINDIVIDUAL_CONTACT_ID.Visible = true;
            FillIndividualContactId(cmbCONTACT_TYPE_ID.SelectedValue.ToString());
            // maintain selected value of country and state dropdown in case when contact type changed.
            if (cmbCONTACT_COUNTRY.SelectedValue != "")
            {
                PopulateStateDropDown(cmbCONTACT_STATE, int.Parse(cmbCONTACT_COUNTRY.SelectedValue));
            }
            if (hidSTATE_ID.Value != "")
                cmbCONTACT_STATE.SelectedValue = hidSTATE_ID.Value;


        }
        private string GetIndividualContactName(string contactTypeId)
        {
            string entityId = Request.QueryString["EntityId"].ToString();
            string entityName = "";
            switch (contactTypeId)
            {
                case "1"://Agency
                    capINDIVIDUAL_CONTACT_ID.Text = objResourceMgr.GetString("capINDIVIDUAL_CONTACT_ID_1"); //"Agency";
                    cmbCONTACT_TYPE_ID.SelectedIndex = 0;
                    entityName = ClsAgency.GetAgencyName(entityId);
                    break;
                case "2"://Customer
                    capINDIVIDUAL_CONTACT_ID.Text = objResourceMgr.GetString("capINDIVIDUAL_CONTACT_ID_2");//"Customer";
                    cmbCONTACT_TYPE_ID.SelectedIndex = 1;
                    entityName = ClsCustomer.GetCustomerName(entityId);
                    break;
                case "3"://Finance Company
                    capINDIVIDUAL_CONTACT_ID.Text = objResourceMgr.GetString("capINDIVIDUAL_CONTACT_ID_3"); //"Finance Company";
                    cmbCONTACT_TYPE_ID.SelectedIndex = 2;
                    entityName = Cms.BusinessLayer.BlCommon.ClsFinanceCompany.GetCompanyName(entityId);
                    break;
                case "4"://Holder
                    capINDIVIDUAL_CONTACT_ID.Text = objResourceMgr.GetString("capINDIVIDUAL_CONTACT_ID_4"); //"Holder";
                    cmbCONTACT_TYPE_ID.SelectedIndex = 3;
                    entityName = ClsMortgage.GetHolderName(entityId);
                    break;
                case "5"://Industry Provider //under construction
                    /*capINDIVIDUAL_CONTACT_ID.Text = "Industry Provider";
                    cmbCONTACT_TYPE_ID.SelectedIndex=4;
                    entityName=ClsMortgage.GetHolderName(entityId);*/
                    break;
                case "6"://Personal
                    cmbCONTACT_TYPE_ID.SelectedIndex = 5;
                    //cmbINDIVIDUAL_CONTACT_ID.Visible = false;//Commented to implement Coolite Combobox control -itrack 1557
                    CCcmbINDIVIDUAL_CONTACT_ID.Visible = false; //Changed by Aditya for TFS bug # 923
                    rfvIndContactId.Visible = false;
                    capINDIVIDUAL_CONTACT_ID.Visible = false;
                    spnIndContactID.Visible = false;
                    entityName = ClsContactsManager.GetContactName(entityId);
                    break;
                case "7"://Tax Entity
                    capINDIVIDUAL_CONTACT_ID.Text = objResourceMgr.GetString("capINDIVIDUAL_CONTACT_ID_7"); //"Tax Entity";
                    cmbCONTACT_TYPE_ID.SelectedIndex = 6;
                    entityName = ClsTaxEntity.GetTaxName(entityId);
                    break;
                case "8"://Vendor
                    capINDIVIDUAL_CONTACT_ID.Text = objResourceMgr.GetString("capINDIVIDUAL_CONTACT_ID_8"); //"Vendor";
                    cmbCONTACT_TYPE_ID.SelectedIndex = 7;
                    entityName = clsVendor.GetVendorName(entityId);
                    break;
                default:

                    break;
            }
            return entityName;
        }

        /// <summary>
        /// Bind Coolite control ComboBox 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="_RecordFieldId"></param>
        /// <param name="_RecordFieldName"></param>
        /// <param name="selectedValue"></param>
        //Added by Pradeep Kushwaha - itrack - 1557
        private void FillCooliteComboBox(DataTable dt, String _RecordFieldId, String _RecordFieldName, String selectedValue)
        {
            Coolite.Ext.Web.JsonReader Reader = new Coolite.Ext.Web.JsonReader();
            Coolite.Ext.Web.RecordField RecordFieldId = new Coolite.Ext.Web.RecordField();
            RecordFieldId.Name = _RecordFieldId;  
            RecordFieldId.Mapping = _RecordFieldId; 
            Reader.Fields.Add(RecordFieldId);

            Coolite.Ext.Web.RecordField RecordFieldName = new Coolite.Ext.Web.RecordField();
            RecordFieldName.Name = _RecordFieldName; 
            RecordFieldName.Mapping = _RecordFieldName; 
            Reader.Fields.Add(RecordFieldName);

            CCcmbINDIVIDUAL_CONTACT_ID.Items.Clear(); //Changed by Aditya for TFS bug # 923
            StoreINDIVIDUAL_CONTACT_ID.ClearMeta();
            StoreINDIVIDUAL_CONTACT_ID.ClearFilter();
            StoreINDIVIDUAL_CONTACT_ID.Reader.Clear();
            StoreINDIVIDUAL_CONTACT_ID.Reader.Add(Reader);

            StoreINDIVIDUAL_CONTACT_ID.DataSource = dt;
            StoreINDIVIDUAL_CONTACT_ID.DataBind();
            CCcmbINDIVIDUAL_CONTACT_ID.StoreID = "StoreINDIVIDUAL_CONTACT_ID";  //Changed by Aditya for TFS bug # 923
            CCcmbINDIVIDUAL_CONTACT_ID.ValueField = _RecordFieldId;  //Changed by Aditya for TFS bug # 923
            CCcmbINDIVIDUAL_CONTACT_ID.DisplayField = _RecordFieldName; //Changed by Aditya for TFS bug # 923
            
            if (selectedValue != null && selectedValue.Length > 0 && dt != null)
                CCcmbINDIVIDUAL_CONTACT_ID.SelectedItem.Value = selectedValue; //Changed by Aditya for TFS bug # 923
 
        }
        //Modified by Pradeep kushwaha - itrack-1557 to implement Coolite Combobox control
        private void FillIndividualContactId(string contactTypeId)
        {
            
            String _EntityId=String.Empty;
            if (Request.QueryString["EntityId"].ToString() != "")
                _EntityId = Request.QueryString["EntityId"].ToString();
            else
                _EntityId = EntityId;

            switch (contactTypeId)
            {
                case "1"://Agency
                    capINDIVIDUAL_CONTACT_ID.Text = objResourceMgr.GetString("capINDIVIDUAL_CONTACT_ID_1");
                    //THOUGH THIS INDEX IS SET BY POPULATEXML ON CLIENT SIDE BUT WE NEED TO SET IT ON SERVER ALSO 
                    //AS OTHERWISE IN VIEW STATE THE SELECTED INDEX ALWAYS REMAINS TO BE ZERO AS IT IS CHANGED ON CLIENT SIDE ONLY;
                    //cmbCONTACT_TYPE_ID.SelectedIndex=0;

                    //ClsAgency.GetAgencyNamesInDropDown(cmbINDIVIDUAL_CONTACT_ID, Request.QueryString["EntityId"].ToString());//Commented to implement Coolite Combobox control -itrack 1557
                   
                    DataTable dtAgencyDetails = new DataTable();
                    ClsAgency.GetAgencyNamesInDropDown(ref dtAgencyDetails);
                    this.FillCooliteComboBox(dtAgencyDetails, "AGENCY_ID", "AGENCY_DISPLAY_NAME", _EntityId);

                    
                    break;
                case "2"://Customer
                    capINDIVIDUAL_CONTACT_ID.Text = objResourceMgr.GetString("capINDIVIDUAL_CONTACT_ID_2");// "Customer";
                    //cmbCONTACT_TYPE_ID.SelectedIndex=1;
                    //ClsCustomer.GetCustomerNamesInDropDown(cmbINDIVIDUAL_CONTACT_ID, Request.QueryString["EntityId"].ToString());//Commented to implement Coolite Combobox control -itrack 1557
                    
                    DataTable dtCustomerDetails = new DataTable();
                    ClsCustomer.GetCustomerNamesInDropDown(ref dtCustomerDetails);
                    this.FillCooliteComboBox(dtCustomerDetails, "CUSTOMER_ID", "CUSTOMER_FIRST_NAME", _EntityId);
                   
                
                    break;
                case "3"://Finance Company
                    capINDIVIDUAL_CONTACT_ID.Text = objResourceMgr.GetString("capINDIVIDUAL_CONTACT_ID_3"); //"Finance Company";
                    //cmbCONTACT_TYPE_ID.SelectedIndex=2;
                    //Cms.BusinessLayer.BlCommon.ClsFinanceCompany.GetFinanceCompanyNamesInDropDown(cmbINDIVIDUAL_CONTACT_ID, Request.QueryString["EntityId"].ToString());//Commented to implement Coolite Combobox control -itrack 1557
                   
                    DataTable dtCompanyNames = new DataTable();
                    Cms.BusinessLayer.BlCommon.ClsFinanceCompany.GetFinanceCompanyNamesInDropDown(ref dtCompanyNames);
                    this.FillCooliteComboBox(dtCompanyNames, "COMPANY_ID", "COMPANY_NAME", _EntityId);
                   

                    break;
                case "4"://Holder
                    capINDIVIDUAL_CONTACT_ID.Text = objResourceMgr.GetString("capINDIVIDUAL_CONTACT_ID_4");//"Holder";
                    //cmbCONTACT_TYPE_ID.SelectedIndex=3;
                    //ClsMortgage.GetHolderNamesInDropDown(cmbINDIVIDUAL_CONTACT_ID, Request.QueryString["EntityId"].ToString());//Commented to implement Coolite Combobox control -itrack 1557
                    
                    DataTable dtHolderNames = new DataTable();
                    ClsMortgage.GetHolderNamesInDropDown(ref dtHolderNames);
                    this.FillCooliteComboBox(dtHolderNames, "HOLDER_ID", "HOLDER_NAME", _EntityId);
                   
                    break;
                case "5"://Industry Provider
                    capINDIVIDUAL_CONTACT_ID.Text = objResourceMgr.GetString("capINDIVIDUAL_CONTACT_ID_5"); //"Industry Provider";
                    //cmbCONTACT_TYPE_ID.SelectedIndex=4;
                    Response.Redirect("/cms/cmsweb/Construction.html");
                    break;
                case "6"://Personal
                    //cmbCONTACT_TYPE_ID.SelectedIndex=5;
                    //cmbINDIVIDUAL_CONTACT_ID.Visible = false;//Commented to implement Coolite Combobox control -itrack 1557
                    CCcmbINDIVIDUAL_CONTACT_ID.Visible = false; //Changed by Aditya for TFS bug # 923
                    rfvIndContactId.Visible = false;
                    capINDIVIDUAL_CONTACT_ID.Visible = false;
                    spnIndContactID.Visible = false;
                    break;
                case "7"://Tax Entity
                    capINDIVIDUAL_CONTACT_ID.Text = objResourceMgr.GetString("capINDIVIDUAL_CONTACT_ID_7");//"Tax Entity";
                    //cmbCONTACT_TYPE_ID.SelectedIndex=6;
                    //ClsTaxEntity.GetTaxEntityInDropDown(cmbINDIVIDUAL_CONTACT_ID, Request.QueryString["EntityId"].ToString());//Commented to implement Coolite Combobox control -itrack 1557

                    DataTable dtTaxEntity = new DataTable();
                    ClsTaxEntity.GetTaxEntityInDropDown(ref dtTaxEntity);
                    this.FillCooliteComboBox(dtTaxEntity, "TAX_ID", "TAX_NAME", _EntityId);
                   
                    break;
                case "8"://Vendor
                    capINDIVIDUAL_CONTACT_ID.Text = objResourceMgr.GetString("capINDIVIDUAL_CONTACT_ID_8"); //"Vendor";
                    //cmbCONTACT_TYPE_ID.SelectedIndex=7;
                    //clsVendor.GetVendorNamesInDropDown(cmbINDIVIDUAL_CONTACT_ID, Request.QueryString["EntityId"].ToString());//Commented to implement Coolite Combobox control -itrack 1557

                    DataTable dtVendorNames = new DataTable();
                    clsVendor.GetVendorNamesInDropDown(ref dtVendorNames);
                    this.FillCooliteComboBox(dtVendorNames, "VENDOR_ID", "COMPANY_NAME", _EntityId);
                   
                    break;
                default:
                    {
                        //cmbINDIVIDUAL_CONTACT_ID.Items.Clear();//Commented to implement Coolite Combobox control -itrack 1557
                        CCcmbINDIVIDUAL_CONTACT_ID.Items.Clear(); //Changed by Aditya for TFS bug # 923
                        break;
                    }
            }
        }
        //Modified till here 
        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            int intRetVal;
            int intContactID = int.Parse(hidCONTACT_ID.Value);
            int userid = int.Parse(GetUserId());
            int CustomerID = 0;
            if (GetCustomerID() != "")
            {
                CustomerID = Convert.ToInt32(GetCustomerID());
               
            }
            string customInfo = "";
            customInfo += ";Contact Salutation = " + cmbCONTACT_SALUTATION.Items[cmbCONTACT_SALUTATION.SelectedIndex].Text;
            customInfo += ";Contact Position = " + cmbCONTACT_POS.Items[cmbCONTACT_POS.SelectedIndex].Text;
            customInfo += ";Contact First Name = " + txtCONTACT_FNAME.Text;
            customInfo += ";Contact Last Name = " + txtCONTACT_LNAME.Text;

            ObjContactsManager = new Cms.BusinessLayer.BlCommon.ClsContactsManager();
            intRetVal = ObjContactsManager.Delete(intContactID, customInfo, userid, CustomerID);
            if (intRetVal > 0)
            {
                lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "127");
                hidFormSaved.Value = "5";
                hidOldData.Value = "";
                trBody.Attributes.Add("style", "display:none");

            }
            else if (intRetVal == -1)
            {
                lblDelete.Text = ClsMessages.GetMessage(base.ScreenId, "128");
                hidFormSaved.Value = "2";
            }
            lblDelete.Visible = true;
        }
        private void SetCaptions()
        {
            capCONTACT_TYPE.Text = objResourceMgr.GetString("cmbCONTACT_TYPE_ID");
            capINDIVIDUAL_CONTACT_ID.Text = objResourceMgr.GetString("capINDIVIDUAL_CONTACT_ID");
            capContact_Code.Text = objResourceMgr.GetString("txtCONTACT_CODE");
            capTITLE.Text = objResourceMgr.GetString("cmbCONTACT_SALUTATION");
            capCONTACT_POSITION.Text = objResourceMgr.GetString("cmbCONTACT_POS");
            capCONTACT_FNAME.Text = objResourceMgr.GetString("txtCONTACT_FNAME");
            capCONTACT_MNAME.Text = objResourceMgr.GetString("txtCONTACT_MNAME");
            capCONTACT_LNAME.Text = objResourceMgr.GetString("txtCONTACT_LNAME");
            btnPullCustomerAddress.Text = objResourceMgr.GetString("btnPullCustomerAddress");
            //capAGENT_ADDRESS.Text = objResourceMgr.GetString("capAGENT_ADDRESS");
            capPullCustomerAddress.Text = objResourceMgr.GetString("capPullCustomerAddress");
            capADDRESS1.Text = objResourceMgr.GetString("txtCONTACT_ADD1");
            capADDRESS2.Text = objResourceMgr.GetString("txtCONTACT_ADD2");
            capCONTACT_CITY.Text = objResourceMgr.GetString("txtCONTACT_CITY");

            capCOUNTRY.Text = objResourceMgr.GetString("cmbCONTACT_COUNTRY");
            capNUMBER.Text = objResourceMgr.GetString("txtNUMBER");
            capDISTRICT.Text = objResourceMgr.GetString("txtDISTRICT");
            capCONTACT_STATE.Text = objResourceMgr.GetString("capCONTACT_STATE");
            capCONTACT_ZIP.Text = objResourceMgr.GetString("txtCONTACT_ZIP");
            capBusiness_Phone.Text = objResourceMgr.GetString("txtCONTACT_BUSINESS_PHONE");
            capMOBILE.Text = objResourceMgr.GetString("txtCONTACT_MOBILE");
            capFAX.Text = objResourceMgr.GetString("txtCONTACT_FAX");
            capCONTACT_PAGER.Text = objResourceMgr.GetString("txtCONTACT_PAGER");
            capHome_Phone.Text = objResourceMgr.GetString("txtCONTACT_HOME_PHONE");
            capTOLL_FREE_NO.Text = objResourceMgr.GetString("txtCONTACT_TOLL_FREE");
            capEMAIL.Text = objResourceMgr.GetString("txtCONTACT_EMAIL");
            capNOTE.Text = objResourceMgr.GetString("capNOTE");
            hidZipeCodeVerificationMsg.Value = objResourceMgr.GetString("hidZipeCodeVerificationMsg");
            btnActivateDeactivate.Text = objResourceMgr.GetString("btnActivateDeactivate");
            capCPF_CNPJ.Text = objResourceMgr.GetString("txtCPF_CNPJ");
            capDATE_OF_BIRTH.Text = objResourceMgr.GetString("txtDATE_OF_BIRTH");
            capACTIVITY.Text = objResourceMgr.GetString("cmbACTIVITY");
            capREGIONAL_ID.Text = objResourceMgr.GetString("txtREGIONAL_IDENTIFICATION");
            capREGIONAL_ID_ISSUE_DATE.Text = objResourceMgr.GetString("txtREG_ID_ISSUE_DATE");
            capREGIONAL_ID_ISSUE.Text = objResourceMgr.GetString("txtREG_ID_ISSUE");
            capCONTACT_EXT.Text = objResourceMgr.GetString("txtCONTACT_EXT");
            CapREGIONAL_ID_TYPE.Text = objResourceMgr.GetString("txtREGIONAL_ID_TYPE");
            capNATIONALITY.Text = objResourceMgr.GetString("txtNATIONALITY");
            Caplook.Text = objResourceMgr.GetString("hidLookup");

        }
     
        private void PopulateStateDropDown(DropDownList cmbCUSTOMER_STATE, int COUNTRY_ID)
        {
            ClsStates objStates = new ClsStates();


            if (COUNTRY_ID == 0)
                return;

            cmbCUSTOMER_STATE.Items.Clear();
            cmbCUSTOMER_STATE.SelectedIndex = -1;
            DataTable dtStates = objStates.GetStatesForCountry(COUNTRY_ID);
            if (dtStates != null && dtStates.Rows.Count > 0)
            {
                cmbCUSTOMER_STATE.DataSource = dtStates;
                cmbCUSTOMER_STATE.DataTextField = STATE_NAME;
                cmbCUSTOMER_STATE.DataValueField = STATE_ID;
                cmbCUSTOMER_STATE.DataBind();
                cmbCUSTOMER_STATE.Items.Insert(0, "");
            }
        }
        private void FetchCountryState(string strXML)
		{   
			string strSelectedCountry = ClsCommon.FetchValueFromXML("CONTACT_COUNTRY",strXML);
            string strSelectedState = ClsCommon.FetchValueFromXML("CONTACT_STATE", strXML);
            if (strSelectedCountry != "" && strSelectedCountry != "0")
            {               
                
                PopulateStateDropDown(cmbCONTACT_STATE,int.Parse(strSelectedCountry));
                if (strSelectedState != "" && strSelectedState != "0") 
                {
                    cmbCONTACT_STATE.SelectedIndex = cmbCONTACT_STATE.Items.IndexOf(cmbCONTACT_STATE.Items.FindByValue(strSelectedState));
                    hidSTATE_ID_OLD.Value = strSelectedState;
                }
            }
            else 
            {
            } 
            //Added by Sibin on 16 Oct 08
				//PopulateStateDropDown(1);				

        }
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
            catch 
            {
                return null;
            }
        }
        [System.Web.Services.WebMethod]
        public static String GetCustomerAddressDetailsUsingZipeCode(String ZIPCODE, String COUNTRYID)
        {

            CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();  //Declare the object ob common webservice

            String ReturnValue = String.Empty;
            DataSet ds = new DataSet();

            ReturnValue = obj.GetAddressDetailsBasedOnZipeCode(ZIPCODE, Convert.ToInt32(COUNTRYID));


            return ReturnValue;
        }
    }
}
            


			
	
	

