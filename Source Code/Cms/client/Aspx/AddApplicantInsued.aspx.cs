/******************************************************************************************
<Author				: -   Mohit Gupta
<Start Date				: -	8/30/2005 4:47:44 PM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 10th Apr'06
<Modified By			: - Swastika Gaur
<Purpose				: - Added multiple fields for Employer (Address,City,State,Ph,Email,Country)

<Modified Date			: - 15th Nov'06
<Modified By			: - Mohit Agarwal
<Purpose				: - Added drop down for relationship and removed lookup icons for title and this

<Modified Date		    : - 07-06-2010
<Modified By		    : - Pradeep Kushwaha
<Purpose			    : - Address Varification based on the Zipe code
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
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.BusinessLayer.BlClient;
using System.Xml;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Client;
using Cms.CmsWeb.Utils;

namespace Cms.Client.Aspx
{
    /// <summary>
    /// Summary description for AddApplicantInsued.
    /// </summary>
    public class AddApplicantInsued : Cms.Client.clientbase
    {
        #region Page controls declaration
        public string javasciptmsg, javasciptCPFmsg, javasciptCNPJmsg,CPF_invalid,CNPJ_invalid;
        protected System.Web.UI.WebControls.DropDownList cmbTITLE;
        protected System.Web.UI.WebControls.TextBox txtSUFFIX;
        protected System.Web.UI.WebControls.TextBox txtFIRST_NAME;
        protected System.Web.UI.WebControls.TextBox txtMIDDLE_NAME;
        protected System.Web.UI.WebControls.TextBox txtLAST_NAME;
        protected System.Web.UI.WebControls.TextBox txtADDRESS1;
        protected System.Web.UI.WebControls.TextBox txtADDRESS2;
        protected System.Web.UI.WebControls.TextBox txtCITY;
        protected System.Web.UI.WebControls.DropDownList cmbCOUNTRY;
        protected System.Web.UI.WebControls.DropDownList cmbSTATE;
        protected System.Web.UI.WebControls.TextBox txtZIP_CODE;
        protected System.Web.UI.WebControls.TextBox txtPHONE;
        protected System.Web.UI.WebControls.TextBox txtMOBILE;
        //protected System.Web.UI.WebControls.TextBox txtBUSINESS_PHONE;
        protected System.Web.UI.WebControls.TextBox txtEXT;
        protected System.Web.UI.WebControls.TextBox txtEMAIL;
        protected System.Web.UI.WebControls.Label Caplook;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

        protected Cms.CmsWeb.Controls.CmsButton btnAddNewQuickQuote;
        protected Cms.CmsWeb.Controls.CmsButton btnAddNewApplication;

        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        //btn Back To customer Assistent ,Date March 15,2010

        protected Cms.CmsWeb.Controls.CmsButton btnbacktocustomerassistent;


        protected System.Web.UI.WebControls.RequiredFieldValidator rfvTITLE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvFIRST_NAME;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvADDRESS1;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvCOUNTRY;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvZIP_CODE;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfv2txtCO_APPL_DOB;
       // protected System.Web.UI.WebControls.RequiredFieldValidator rfvLAST_NAME;

        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Label lblAllPolicy;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidBackToApplication;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID_OLD;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidEMPL_STATE_ID_OLD;
        protected System.Web.UI.WebControls.CustomValidator csvZIP_CODE;
        protected System.Web.UI.WebControls.CustomValidator csvCO_APPLI_EMPL_ZIP_CODE;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revBANK_NUMBER;
        protected System.Web.UI.WebControls.Label CapMessage;

        #endregion

        #region Local form variables
        //START:*********** Local form variables *************
        string oldXML;
        public string customerName, FirstName;
        //creating resource manager object (used for reading field and label mapping)
        System.Resources.ResourceManager objResourceMgr;
        private string strRowId, strFormSaved;
        //private int intLoggedInUserID;
        
        protected System.Web.UI.WebControls.Label capBANK_NAME;
        protected System.Web.UI.WebControls.Label capBANK_NUMBER;
        protected System.Web.UI.WebControls.Label capBANK_BRANCH;
        protected System.Web.UI.WebControls.Label capACCOUNT_NUMBER;
        protected System.Web.UI.WebControls.Label capACCOUNT_TYPE;
        protected System.Web.UI.WebControls.TextBox txtBANK_NAME;
        protected System.Web.UI.WebControls.TextBox txtBANK_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtBANK_BRANCH;
        protected System.Web.UI.WebControls.TextBox txtACCOUNT_NUMBER;
        protected System.Web.UI.WebControls.DropDownList cmbACCOUNT_TYPE;
        protected System.Web.UI.WebControls.Label capTITLE;
        protected System.Web.UI.WebControls.Label capSUFFIX;
        protected System.Web.UI.WebControls.Label capFIRST_NAME;
        protected System.Web.UI.WebControls.Label capMIDDLE_NAME;
        protected System.Web.UI.WebControls.Label capLAST_NAME;
        protected System.Web.UI.WebControls.Label capADDRESS1;
        protected System.Web.UI.WebControls.Label capADDRESS2;
        protected System.Web.UI.WebControls.Label capCITY;
        protected System.Web.UI.WebControls.Label capCOUNTRY;
        protected System.Web.UI.WebControls.Label capSTATE;
        protected System.Web.UI.WebControls.Label capZIP_CODE;
        protected System.Web.UI.WebControls.Label capPHONE;
        protected System.Web.UI.WebControls.Label capMOBILE;
        //protected System.Web.UI.WebControls.Label capBUSINESS_PHONE;
        protected System.Web.UI.WebControls.Label capEXT;
        protected System.Web.UI.WebControls.Label capEMAIL;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPPLICANT_ID;
        protected System.Web.UI.WebControls.Label capAPPLICANT_STATUS;
        protected System.Web.UI.WebControls.Label lblAPPLICANT_STATUS;
        protected System.Web.UI.WebControls.RegularExpressionValidator revZIP_CODE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPHONE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revMOBILE;
         protected System.Web.UI.WebControls.RegularExpressionValidator revEMAIL;
        protected System.Web.UI.WebControls.RegularExpressionValidator revEXT;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revACCOUNT_NUMBER;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revBANK_BRANCH;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revBUSINESS_PHONE;
        protected System.Web.UI.WebControls.Label lblPull;
        protected Cms.CmsWeb.Controls.CmsButton btnPullCustomerAddress;
        protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_NAME;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_CODE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidZipeCodeVerificationMsg; 
        protected System.Web.UI.WebControls.TextBox txtCO_APPL_DOB;
        protected System.Web.UI.WebControls.HyperLink hlkCO_APPL_DOB;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCO_APPL_DOB;
        protected System.Web.UI.WebControls.CustomValidator csvCO_APPL_DOB;
        protected System.Web.UI.WebControls.CustomValidator csvCREATION_DATE;
        protected System.Web.UI.WebControls.Label capCO_APPL_SSN_NO;
        protected System.Web.UI.WebControls.TextBox txtCO_APPL_SSN_NO;
        protected System.Web.UI.WebControls.CustomValidator csvCO_APPL_SSN_NO;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCO_APPL_SSN_NO;
        protected System.Web.UI.WebControls.Label capCO_APPLI_OCCUPATION;
        protected System.Web.UI.WebControls.DropDownList cmbCO_APPLI_OCCU;
        protected System.Web.UI.WebControls.Label capCO_APPLI_EMPL_NAME;
        protected System.Web.UI.WebControls.TextBox txtCO_APPLI_EMPL_NAME;
        protected System.Web.UI.WebControls.Label capCO_APPLI_EMPL_ADDRESS;
        protected System.Web.UI.WebControls.Label capCO_APPLI_YEARS_WITH_CURR_EMPL;
        protected System.Web.UI.WebControls.TextBox txtCO_APPLI_YEARS_WITH_CURR_EMPL;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCO_APPLI_YEARS_WITH_CURR_EMPL;
        protected System.Web.UI.WebControls.Label capCO_APPL_YEAR_CURR_OCCU;
        protected System.Web.UI.WebControls.TextBox txtCO_APPL_YEAR_CURR_OCCU;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCO_APPL_YEAR_CURR_OCCU;
        protected System.Web.UI.WebControls.Label capCO_APPL_DOB;
        protected System.Web.UI.WebControls.Label capCO_APPL_MARITAL_STATUS;
        protected System.Web.UI.WebControls.DropDownList cmbCO_APPL_MARITAL_STATUS;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvCO_APPL_MARITAL_STATUS;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvDATE_OF_BIRTH;
        protected Cms.CmsWeb.Controls.CmsButton btnMakePrimaryApplicant;
        protected System.Web.UI.WebControls.Label capPRIMARY_APPLICANT;
        protected System.Web.UI.WebControls.Label lblPRIMARY_APPLICANT;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_TYPE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPRIMARY_APP;
        protected System.Web.UI.WebControls.Label capDESC_CO_APPLI_OCCU;
        protected System.Web.UI.WebControls.TextBox txtDESC_CO_APPLI_OCCU;
        protected System.Web.UI.WebControls.Label lblDESC_CO_APPLI_OCCU;
        protected System.Web.UI.WebControls.Label capFAX;
        protected System.Web.UI.WebControls.CustomValidator cstREG_ID_ISSUE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_CO_APPLI_OCCU;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvACCOUNT_TYPE;
        protected string isActive;
        //int customerId;
        protected System.Web.UI.WebControls.TextBox txtCO_APPLI_EMPL_ZIP_CODE;
        protected System.Web.UI.WebControls.TextBox txtCO_APPLI_EMPL_CITY;
        protected System.Web.UI.WebControls.Label capCO_APPLI_EMPL_COUNTRY;
        protected System.Web.UI.WebControls.DropDownList cmbCO_APPLI_EMPL_COUNTRY;
        protected System.Web.UI.WebControls.Label capCO_APPLI_EMPL_CITY;
        protected System.Web.UI.WebControls.Label capCO_APPLI_EMPL_STATE;
        protected System.Web.UI.WebControls.DropDownList cmbCO_APPLI_EMPL_STATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCO_APPLI_EMPL_ZIP_CODE;
        protected System.Web.UI.WebControls.TextBox txtCO_APPLI_EMPL_PHONE;
        protected System.Web.UI.WebControls.Label capCO_APPLI_EMPL_ZIP_CODE;
        protected System.Web.UI.WebControls.Label capCO_APPLI_EMPL_PHONE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCO_APPLI_EMPL_PHONE;
        protected System.Web.UI.WebControls.Label capCO_APPLI_EMPL_EMAIL;
        protected System.Web.UI.WebControls.TextBox txtCO_APPLI_EMPL_EMAIL;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCO_APPLI_EMPL_EMAIL;
       // protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATE;
        protected System.Web.UI.WebControls.TextBox txtCO_APPLI_EMPL_ADDRESS;
        protected System.Web.UI.WebControls.Label capCO_APPLI_EMPL_ADDRESS1;
        protected System.Web.UI.WebControls.TextBox txtCO_APPLI_EMPL_ADDRESS1;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCO_APPLI_OCCU;
        //protected System.Web.UI.WebControls.TextBox txtCO_APPL_RELATIONSHIP;
        protected System.Web.UI.WebControls.Label capCO_APPL_RELATIONSHIP;
        protected System.Web.UI.WebControls.Label capCO_APPL_GENDER;
        protected System.Web.UI.WebControls.DropDownList cmbCO_APPL_GENDER;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvGENDER;
        //protected System.Web.UI.HtmlControls.HtmlImage imgTitle;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidRESET;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTITLE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOSITION;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCO_APPL_RELATIONSHIP;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCO_APPLI_OCCU;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPPLICANT_TYPE;

        protected System.Web.UI.HtmlControls.HtmlImage imgRelation;
        protected System.Web.UI.HtmlControls.HtmlImage imgOccup;
        protected System.Web.UI.WebControls.DropDownList cmbCO_APPL_RELATIONSHIP;
        protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
        protected System.Web.UI.WebControls.Image imgZipLookup;
        protected System.Web.UI.WebControls.HyperLink hlkCoZipLookup;
        protected System.Web.UI.WebControls.Image imgCoZipLookup;
       // protected System.Web.UI.HtmlControls.HtmlGenericControl spnSTATE;
        //protected System.Web.UI.HtmlControls.HtmlGenericControl spnZIP_CODE;
        protected System.Web.UI.HtmlControls.HtmlGenericControl spnFIRST_NAME;
        protected System.Web.UI.HtmlControls.HtmlGenericControl spnLAST_NAME;
        protected System.Web.UI.HtmlControls.HtmlGenericControl spnCO_APPL_DOB;
        protected System.Web.UI.HtmlControls.HtmlGenericControl spnCO_APPL_MARITAL_STATUS;
       // protected System.Web.UI.HtmlControls.HtmlGenericControl spnADDRESS1;
        protected System.Web.UI.HtmlControls.HtmlGenericControl spnCO_APPLI_OCCUPATION;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_COUNTRY_LIST;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidEmpDetails_STATE_COUNTRY_LIST; //
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidEmpDetails_STATE_ID;
        protected System.Web.UI.WebControls.Label capAPPLICANT_TYPE;
        protected System.Web.UI.WebControls.DropDownList cmbAPPLICANT_TYPE;
        protected System.Web.UI.WebControls.CompareValidator cpvREG_ID_ISSUE;
        protected System.Web.UI.WebControls.CompareValidator cpvREG_ID_ISSUE2;
        
      

        protected System.Web.UI.WebControls.Label capCO_APPL_SSN_NO_HID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCO_APPL_SSN_NO;
        protected System.Web.UI.WebControls.RegularExpressionValidator rev_EXT;
        //Added by Lalit March 15,2010
        //Declare Labels
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPPL_ID;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCO_APPLI_BUSINESS_PHONE;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvREG_ID_ISSUE;
        protected System.Web.UI.WebControls.TextBox txtREG_ID_ISSUE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revREG_ID_ISSUE;
      //  protected System.Web.UI.WebControls.RequiredFieldValidator rfvcmbPOSITION;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvORIGINAL_ISSUE;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvREGIONAL_IDENTIFICATION;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvCITY;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvDISTRICT;
        
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvNUMBER;
        protected System.Web.UI.WebControls.HyperLink hlkREG_ID_ISSUE;
        protected System.Web.UI.HtmlControls.HtmlTableRow trRow1;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCUSTOMER_TYPE;
        protected System.Web.UI.HtmlControls.HtmlTableCell tdF_NAME;
        protected System.Web.UI.HtmlControls.HtmlTableCell tdM_NAME;
        protected System.Web.UI.HtmlControls.HtmlTableCell tdL_NAME;

        //For Customer Code
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCONTACT_CODE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCPF_CNPJ;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCPF_CNPJ;
        protected System.Web.UI.WebControls.CustomValidator csvCPF_CNPJ;
        protected System.Web.UI.WebControls.CustomValidator csvNOTE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revFAX;

        protected Label capPOSITION;
        protected Label capCONTACT_CODE;
        protected Label capID_TYPE;
        protected Label capID_TYPE_NUMBER;
        protected Label capNUMBER;
        protected Label capCOMPLIMENT;
        protected Label capDISTRICT;
        protected Label capNOTE;
        protected Label capREGIONAL_IDENTIFICATION;
        protected Label capREG_ID_ISSUE;
        protected Label capCO_APPLI_BUSINESS_PHONE;
        protected Label capCPF_CNPJ;
        protected Label capORIGINAL_ISSUE;
        protected Label capCREATION_DATE;
        //Declare Textbox

        protected TextBox txtCONTACT_CODE;
        
        protected TextBox txtID_TYPE_NUMBER;
        protected TextBox txtNUMBER;
        protected TextBox txtCOMPLIMENT;
        protected TextBox txtDISTRICT;
        protected TextBox txtNOTE;
        protected TextBox txtREGIONAL_IDENTIFICATION;
        protected TextBox txtORIGINAL_ISSUE;
        protected TextBox txtCUSTOMER_FIRST_NAME;
        protected TextBox txtCUSTOMER_CODE;
        protected TextBox txtCPF_CNPJ;
        protected TextBox txtCO_APPLI_BUSINESS_PHONE;
        protected TextBox txtFAX;
        //Declare Dropdown

        protected DropDownList cmbPOSITION;
        protected DropDownList cmbID_TYPE;

        //protected System.Web.UI.WebControls.DropDownList cmbAPPLICANT_TYPE;

        //protected Cms.CmsWeb.Controls.CmsButton btnBack;
        //Defining the business layer class object
        ClsApplicantInsued ObjAddApplicationInsured;
        //ADDED BY PRAVEEN KUMAR(09-02-2009)
        public string WebServiceURL;
        //END PRAVEEN
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
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            rfvCUSTOMER_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "9");
            rfvFIRST_NAME.Attributes.Add("ErrMsgFirstName", Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "10"));
            rfvFIRST_NAME.Attributes.Add("ErrMsgCustomerName", Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "11"));
            revCPF_CNPJ.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1081");//@"The format for CPF\CNPJ must be XXX.XXX.XXX-XX Or XX.XXX.XXX/XXXX-XX";
            
            revCPF_CNPJ.ValidationExpression = aRegExpCpf_Cnpj;
            revCPF_CNPJ.Attributes.Add("ErrMsgcpf", Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "15"));
            revCPF_CNPJ.Attributes.Add("ErrMsgcnpj", Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "16"));
            
            rfvCPF_CNPJ.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "12"); //"Please Enter CPF/CNPJ";//Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "13");             
            rfvCONTACT_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1983");
            rev_EXT.ValidationExpression = aRegExpExtn;
            //rfvREG_ID_ISSUE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "1");
          //  rfvcmbPOSITION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "2");
           // rfvORIGINAL_ISSUE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "3");
            //rfvREGIONAL_IDENTIFICATION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "4");
            //rfvCITY.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("56");
            //rfvDISTRICT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "5");
           // rfvNUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "7");
            rev_EXT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "25");
           // rfvACCOUNT_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1201");
            csvCREATION_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1409");
            revEMAIL.ValidationExpression = aRegExpEmail;
            revEXT.ValidationExpression = aRegExpExtn;
            revREG_ID_ISSUE.ValidationExpression = aRegExpDate;
            revREG_ID_ISSUE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "1");           
            
            revEMAIL.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "124");          
            revEXT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "25");
            rfvCO_APPLI_OCCU.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("873");
            //rfvLAST_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "58");
            //rfvADDRESS1.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("32");
            //rfvCOUNTRY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "33");
            //rfvSTATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "35");
            //rfvZIP_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "37");
            revCO_APPL_DOB.ValidationExpression = aRegExpDate;
            revCO_APPL_DOB.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            csvCO_APPL_DOB.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("481");
            //rfvDATE_OF_BIRTH.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("482");
            //rfvCO_APPL_MARITAL_STATUS.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("480");
            revCO_APPL_SSN_NO.ValidationExpression = aRegExpSSN;
            revCO_APPL_SSN_NO.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "130");
            revCO_APPLI_YEARS_WITH_CURR_EMPL.ValidationExpression = aRegExpInteger;
            revCO_APPLI_YEARS_WITH_CURR_EMPL.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
            revCO_APPL_YEAR_CURR_OCCU.ValidationExpression = aRegExpInteger;
            revCO_APPL_YEAR_CURR_OCCU.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
            rfvDESC_CO_APPLI_OCCU.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "457");
            revCO_APPLI_EMPL_PHONE.ValidationExpression = aRegExpPhoneBrazil;
            
            revCO_APPLI_EMPL_ZIP_CODE.ValidationExpression = aRegExpZip;
            revCO_APPLI_EMPL_EMAIL.ValidationExpression = aRegExpEmail;
            revCO_APPLI_EMPL_ZIP_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "24");
            revCO_APPLI_EMPL_EMAIL.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "124");
            revCO_APPLI_EMPL_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
            
            //rfvGENDER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("189");
            csvNOTE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "13");  //set remarks character length message;	
            //revACCOUNT_NUMBER.ValidationExpression = aRegExpAccountNumber;
            //revACCOUNT_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1198");
            
            //revBANK_BRANCH.ValidationExpression =aRegExpBankBranch;
            //revBANK_BRANCH.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1199");


            //revZIP_CODE.ValidationExpression = aRegExpZipBrazil;
            //revZIP_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1181");
            
            
           // revPHONE.ValidationExpression = aRegExpPhoneBrazil;
           // revPHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083");

           // revMOBILE.ValidationExpression = aRegExpPhoneBrazil;
           // revMOBILE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1086");

            //revFAX.ValidationExpression = aRegExpPhoneBrazil;
            //revFAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1085");

            //revCO_APPLI_BUSINESS_PHONE.ValidationExpression = aRegExpPhoneBrazil;
           // revCO_APPLI_BUSINESS_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083");

            revPHONE.ValidationExpression = aRegExpAgencyPhone;
            revFAX.ValidationExpression = aRegExpAgencyPhone;
            revMOBILE.ValidationExpression = aRegExpAgencyPhone;
            revCO_APPLI_BUSINESS_PHONE.ValidationExpression = aRegExpAgencyPhone;
            if (GetLanguageID() == "2")
            {

                revPHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1999");
                revFAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1921");
                revMOBILE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2000");
                revCO_APPLI_BUSINESS_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2002");

            }

            else
            {
                revPHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1999");
                revFAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1085");
                revMOBILE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2000");
                revCO_APPLI_BUSINESS_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2002");

            }

            //revBANK_NUMBER.ValidationExpression = aRegExpDoublePositiveNonZero;
            //revBANK_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");
            //rfv2txtCO_APPL_DOB.ErrorMessage = ClsMessages.FetchGeneralMessage("1626");
            
        }
        #endregion

        #region PageLoad event
        private void Page_Load(object sender, System.EventArgs e)
        {
            base.ScreenId = "134_2";
            Page.DataBind();
            lblMessage.Visible = false;
            
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");            
            btnActivateDeactivate.Enabled = false;
            javasciptmsg = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "8");
            javasciptCPFmsg = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "15");
            javasciptCNPJmsg = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "16");
            CPF_invalid = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "17");
            CNPJ_invalid  = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "18");
            CapMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            cpvREG_ID_ISSUE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1402");
            cpvREG_ID_ISSUE2.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1403");
            
            Ajax.Utility.RegisterTypeForAjax(typeof(AddApplicantInsued));
            // Put user code to initialize the page here
            //btnReset.Attributes.Add("onclick","javascript:ResetForm('" + Page.Controls[0].ID + "' );return Check();");
            btnActivateDeactivate.Text = ClsMessages.FetchGeneralMessage("1333");
            hlkREG_ID_ISSUE.Attributes.Add("OnClick", "fPopCalendar(document.CLT_APPLICANT_LIST.txtREG_ID_ISSUE,document.CLT_APPLICANT_LIST.txtREG_ID_ISSUE)");
            cmbAPPLICANT_TYPE.Attributes.Add("onChange", "javascript:FillTitles();OnCustomerTypeChange();");
            txtFIRST_NAME.Attributes.Add("onBlur", "javascript:GenerateCustomerCode();");
            txtLAST_NAME.Attributes.Add("onBlur", "javascript:GenerateCustomerCode();");
            btnReset.Attributes.Add("onclick", "javascript: return ResetTheForm();");
            btnSave.Attributes.Add("onclick", "javascript:Page_ClientValidate();DisableZipForCanada();EmpDetails_DisableZipForCanada();");
            hlkCO_APPL_DOB.Attributes.Add("OnClick", "fPopCalendar(document.CLT_APPLICANT_LIST.txtCO_APPL_DOB,document.CLT_APPLICANT_LIST.txtCO_APPL_DOB)");
            
            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************

            //Declare By Lalit ,March 15,2010
            btnbacktocustomerassistent.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Execute;
            btnbacktocustomerassistent.PermissionString = gstrSecurityXML;
            btnbacktocustomerassistent.Attributes.Add("onclick", "javascript:return DoBackToAssistant();");

            btnReset.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Execute;
            btnReset.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Execute;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnMakePrimaryApplicant.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Execute;
            btnMakePrimaryApplicant.PermissionString = gstrSecurityXML;

            btnPullCustomerAddress.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnPullCustomerAddress.PermissionString = gstrSecurityXML;

            btnAddNewQuickQuote.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnAddNewQuickQuote.PermissionString = gstrSecurityXML;

            btnAddNewApplication.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnAddNewApplication.PermissionString = gstrSecurityXML;

            btnAddNewApplication.Attributes.Add("onclick", "javascript:return GoToNewApplication();");
            btnAddNewQuickQuote.Attributes.Add("onclick", "javascript:return GoToNewQuote();");

            cmbCO_APPLI_OCCU.Attributes.Add("onchange", "javascript: return Check();");

            if (Request.QueryString["CUSTOMER_ID"] != null || Request.QueryString["CUSTOMER_ID"] != "")
            {
                hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();
            }
            if (Request.QueryString["BackToApplication"] != null && Request.QueryString["BackToApplication"] != "")
            {
                hidBackToApplication.Value = Request.QueryString["BackToApplication"].ToString();
            }
            else
                hidBackToApplication.Value = "0";
            

            GetCustomerType();

            //check for primary applicant
            // Modified By Swastika on 21st Mar'06 for Gen Iss #2367 : Added Field Phone
            //base.RequiredPullCustAdd(txtADDRESS1, txtADDRESS2
            //    , txtCITY, cmbCOUNTRY, cmbSTATE
            //    , txtZIP_CODE, txtPHONE, txtEMAIL, txtMOBILE, null, null, btnPullCustomerAddress);


            ClsApplicantInsued.CheckCustomerIsActive(int.Parse(hidCUSTOMER_ID.Value), out isActive);


            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            SetCultureThread(GetLanguageCode());
            objResourceMgr = new System.Resources.ResourceManager("Cms.Client.Aspx.AddApplicantInsued", System.Reflection.Assembly.GetExecutingAssembly());


            customerName = objResourceMgr.GetString("txtFIRST_NAME");
            FirstName = objResourceMgr.GetString("FIRST_NAME");
            
            if (Request.Form["__EVENTTARGET"] == "CountryChanged")
            {
                cmbCOUNTRY_Changed();
                hidFormSaved.Value = "2";
                return;
            }
            if (hidRESET.Value == "1")
            {
                cmbCOUNTRY_Changed();
                hidRESET.Value = "0";
                hidFormSaved.Value = "1";
            }
          
            if (!Page.IsPostBack)
            {
                SetErrorMessages();
               
                PopulateComboBox();
                GetOldDataXML();
                SetCaptions();

                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "client/support/PageXML/" + GetSystemId(), "AddApplicantInsued.xml"))
                    setPageControls(Page, Request.PhysicalApplicationPath + "client/support/PageXml/" + GetSystemId() + "/AddApplicantInsued.xml");

                
                GetOldData();

              

                //imgZipLookup.Attributes.Add("style", "cursor:hand");


                base.VerifyAddressDetailsBR(hlkZipLookup, txtADDRESS1, txtDISTRICT
                    , txtCITY, cmbSTATE, txtZIP_CODE);
                //imgCoZipLookup.Attributes.Add("style", "cursor:hand");

                //base.VerifyAddress(hlkCoZipLookup, txtCO_APPLI_EMPL_ADDRESS, txtCO_APPLI_EMPL_ADDRESS1
                //    , txtCO_APPLI_EMPL_CITY, cmbCO_APPLI_EMPL_STATE, txtCO_APPLI_EMPL_ZIP_CODE);
                // Added by Mohit Agarwal 10-Nov-2006
                string url = ClsCommon.GetLookupURL();
                imgRelation.Attributes.Add("onclick", "javascript:OpenLookupWithFunction('" + url + "','LOOKUP_UNIQUE_ID','LOOKUP_VALUE_DESC','hidCO_APPL_RELATIONSHIP','cmbCO_APPL_RELATIONSHIP','LookupTable','Title'," + "\"@LOOKUP_NAME=\'DRACD\'\"" + ",'FetchDataRelation()');");
                imgOccup.Attributes.Add("onclick", "javascript:OpenLookupWithFunction('" + url + "','LOOKUP_UNIQUE_ID','LOOKUP_VALUE_DESC','hidCO_APPLI_OCCU','cmbCO_APPLI_OCCU','LookupTable','Title'," + "\"@LOOKUP_NAME=\'%OCC\'\"" + ",'FetchDataOccup()');");
              
                
                if (Request.QueryString["APPLICANT_ID"] == null || Request.QueryString["APPLICANT_ID"].ToString().Length == 0)
                {
                    //cmbCOUNTRY.SelectedIndex = cmbCOUNTRY.Items.IndexOf(cmbCOUNTRY.Items.FindByValue("5"));
                    cmbCOUNTRY.SelectedIndex = cmbCOUNTRY.Items.IndexOf(cmbCOUNTRY.Items.FindByValue(cmbCOUNTRY.SelectedValue.ToString()));
                    DataSet cntrydt = new DataSet();
                    //cntrydt = AjaxFillState("5");
                    cntrydt = AjaxFillState(cmbCOUNTRY.SelectedValue.ToString());                    
                    cmbSTATE.DataSource = cntrydt.Tables[0];
                    cmbSTATE.DataValueField = STATE_ID;
                    cmbSTATE.DataTextField = STATE_NAME;
                    cmbSTATE.DataBind();
                }
                                    
            }

            if (Request.QueryString["CALLED_FROM"] != null)
                if (Request.QueryString["CALLED_FROM"].ToString() == "POLICIES")
                {
                    trRow1.Style.Add("Display", "none");
                    btnSave.Enabled = false;
                }
                else
                    trRow1.Style.Add("Display", "inline");

            else
                trRow1.Style.Add("Display", "inline");

            PopulateComboBox();
        }
        #endregion


        private void cmbCOUNTRY_Changed()
        {
            try
            {
                if (cmbCOUNTRY.SelectedItem != null && cmbCOUNTRY.SelectedItem.Value != "")
                {
                    PopulateStateDropDown(int.Parse(cmbCOUNTRY.SelectedItem.Value));
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
            }
            finally
            {
            }
        }
        private void FetchCountryState(string strXML)
        {
            string strSelectedState = ClsCommon.FetchValueFromXML("STATE", strXML);
            string strSelectedCountry = ClsCommon.FetchValueFromXML("COUNTRY", strXML);
            if (strSelectedCountry != "" && strSelectedCountry != "0")
            {
                PopulateStateDropDown(int.Parse(strSelectedCountry));
            }
            else
                PopulateStateDropDown(1);
        }

        private void PopulateStateDropDown(int COUNTRY_ID)
        {
            ClsStates objStates = new ClsStates();
            DataSet dsStates;
            if (COUNTRY_ID == 0)
                return;
            else         
                dsStates = objStates.GetStatesCountry(COUNTRY_ID);      
         
            cmbSTATE.Items.Clear();
            DataTable dtStates = objStates.GetStatesForCountry(COUNTRY_ID);
            if (dtStates != null && dtStates.Rows.Count > 0)
            {
                cmbSTATE.DataSource = dtStates;
                cmbSTATE.DataTextField = STATE_NAME;
                cmbSTATE.DataValueField = STATE_ID;
                cmbSTATE.DataBind();
                cmbSTATE.Items.Insert(0, "");
            }
            if (COUNTRY_ID != 1)
            {
                //rfvZIP_CODE.Enabled = rfvSTATE.Enabled = revZIP_CODE.Enabled = false;
                //imgZipLookup.Attributes.Add("style", "display:none");
                //spnSTATE.Attributes.Add("style", "display:none");
                //spnZIP_CODE.Attributes.Add("style", "display:none");
            }
            else
            {
                //rfvZIP_CODE.Enabled = rfvSTATE.Enabled = revZIP_CODE.Enabled = true;
                //imgZipLookup.Attributes.Add("style", "display:inline");
                //spnSTATE.Attributes.Add("style", "display:inline");
                //spnZIP_CODE.Attributes.Add("style", "display:inline");
            }
        }

        #region EMPLOYEE STATE AND COUNTRY
        //Added for Emp 
        private void FetchEmpCountryState(string strXML)
        {
            string strSelectedState = ClsCommon.FetchValueFromXML("CO_APPLI_EMPL_STATE", strXML);
            string strSelectedCountry = ClsCommon.FetchValueFromXML("CO_APPLI_EMPL_COUNTRY", strXML);
            if (strSelectedCountry != "" && strSelectedCountry != "0")
            {
                PopulateEmpStateDropDown(int.Parse(strSelectedCountry));
            }
            else
                PopulateEmpStateDropDown(1);
        }
        private void PopulateEmpStateDropDown(int COUNTRY_ID)
        {
            ClsStates objStates = new ClsStates();
            DataSet dsStates;
            if (COUNTRY_ID == 0)
                return;
            else
                dsStates = objStates.GetStatesCountry(COUNTRY_ID);

            cmbCO_APPLI_EMPL_STATE.Items.Clear();
            DataTable dtStates = objStates.GetStatesForCountry(COUNTRY_ID);
            if (dtStates != null && dtStates.Rows.Count > 0)
            {
                cmbCO_APPLI_EMPL_STATE.DataSource = dtStates;
                cmbCO_APPLI_EMPL_STATE.DataTextField = STATE_NAME;
                cmbCO_APPLI_EMPL_STATE.DataValueField = STATE_ID;
                cmbCO_APPLI_EMPL_STATE.DataBind();
                cmbCO_APPLI_EMPL_STATE.Items.Insert(0, "");
            }
            if (COUNTRY_ID != 1)
                revCO_APPLI_EMPL_ZIP_CODE.Enabled = false;
            else
                revCO_APPLI_EMPL_ZIP_CODE.Enabled = true;
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
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
            this.btnMakePrimaryApplicant.Click += new System.EventHandler(this.btnMakePrimaryApplicant_Click);         
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.hidTITLE.ServerChange += new System.EventHandler(this.hidTITLE_ServerChange);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        private void GetCustomerType()
        {
            DataSet dsCustomer = Cms.BusinessLayer.BlClient.ClsCustomer.GetCustomerDetails(Convert.ToInt32(hidCUSTOMER_ID.Value));

            if (dsCustomer.Tables.Count > 0)
            {
                if (dsCustomer.Tables[0].Rows.Count > 0)
                {
                    if (dsCustomer.Tables[0].Rows[0]["CUSTOMER_TYPE"] != null)
                        hidCUSTOMER_TYPE.Value = dsCustomer.Tables[0].Rows[0]["CUSTOMER_TYPE"].ToString();
                    if (dsCustomer.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"] != null)
                        hidCUSTOMER_NAME.Value = (dsCustomer.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"].ToString() + " " + dsCustomer.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString() + " " + dsCustomer.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"].ToString());
                    if (dsCustomer.Tables[0].Rows[0]["CUSTOMER_CODE"] != null)
                        hidCUSTOMER_CODE.Value = dsCustomer.Tables[0].Rows[0]["CUSTOMER_CODE"].ToString();
                }
            }
        }

        private void PopulateComboBox()
        {


            DataTable dt = Cms.CmsWeb.ClsFetcher.AllCountry;
            //Bind Co-Aplicant Country
            cmbCOUNTRY.DataSource = dt;
            cmbCOUNTRY.DataTextField = "Country_Name";
            cmbCOUNTRY.DataValueField = "Country_Id";
            cmbCOUNTRY.DataBind();

            cmbCO_APPLI_EMPL_COUNTRY.DataSource = dt;
            cmbCO_APPLI_EMPL_COUNTRY.DataTextField = "Country_Name";
            cmbCO_APPLI_EMPL_COUNTRY.DataValueField = "Country_Id";
            cmbCO_APPLI_EMPL_COUNTRY.DataBind();

            //Bind Customer Position

            //DataTable dt2 = Cms.BusinessLayer.BlApplication.ClsLocation.SELECTACTIVITYTYPE();
            //cmbPOSITION.DataSource = dt2;
            //cmbPOSITION.DataTextField = "ACTIVITY_DESC";
            //cmbPOSITION.DataValueField = "ACTIVITY_ID";
            //cmbPOSITION.DataBind();
            //cmbPOSITION.Items.Insert(0, "");

            //Bind Customer Type
            // for itrack no:780
            if (Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CUSTYP").Select("", "LookupDesc").Length > 0)
            {
                cmbAPPLICANT_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CUSTYP").Select("", "LookupDesc").CopyToDataTable<DataRow>(); 
            }
            else
            {
                cmbAPPLICANT_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CUSTYP");
            }
           
                cmbAPPLICANT_TYPE.DataTextField = "LookupDesc";
                cmbAPPLICANT_TYPE.DataValueField = "LookupID";
                cmbAPPLICANT_TYPE.DataBind();
                cmbAPPLICANT_TYPE.Items.Insert(0, "");
          
                //cmbAPPLICANT_TYPE.SelectedIndex = -1;//cmbAPPLICANT_TYPE.Items.IndexOf(cmbAPPLICANT_TYPE.Items.FindByValue("11110"));

            ClsCommon.BindLookupDDL(cmbTITLE, "%SAL", "");
            cmbCO_APPLI_OCCU.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("%OCC");
            cmbCO_APPLI_OCCU.DataTextField = "LookupDesc";
            cmbCO_APPLI_OCCU.DataValueField = "LookupID";
            cmbCO_APPLI_OCCU.DataBind();
            cmbCO_APPLI_OCCU.Items.Insert(0, "");

            //Bind Marital Status

            cmbCO_APPL_MARITAL_STATUS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("MARST");
            cmbCO_APPL_MARITAL_STATUS.DataTextField = "LookupDesc";
            cmbCO_APPL_MARITAL_STATUS.DataValueField = "LookupCode";
            cmbCO_APPL_MARITAL_STATUS.DataBind();
            cmbCO_APPL_MARITAL_STATUS.Items.Insert(0, "");

            cmbCO_APPL_RELATIONSHIP.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("DRACD");
            cmbCO_APPL_RELATIONSHIP.DataTextField = "LookupDesc";
            cmbCO_APPL_RELATIONSHIP.DataValueField = "LookupID";
            cmbCO_APPL_RELATIONSHIP.DataBind();
            cmbCO_APPL_RELATIONSHIP.Items.Insert(0, "");

            //Bind Gender

            cmbCO_APPL_GENDER.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("Sex");
            cmbCO_APPL_GENDER.DataTextField = "LookupDesc";
            cmbCO_APPL_GENDER.DataValueField = "LookupCode";
            cmbCO_APPL_GENDER.DataBind();
            cmbCO_APPL_GENDER.Items.Insert(0, "");

            cmbACCOUNT_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("ACTYP");
            cmbACCOUNT_TYPE.DataTextField = "LookupDesc";
            cmbACCOUNT_TYPE.DataValueField = "LookupID";
            cmbACCOUNT_TYPE.DataBind();
            cmbACCOUNT_TYPE.Items.Insert(0, "");

        }




        #region GetFormValue
        /// <summary>
        /// Fetch form's value and stores into model class object and return that object.
        /// </summary>
        private ClsApplicantInsuedInfo GetFormValue()
        {
            //Creating the Model object for holding the New data
            ClsApplicantInsuedInfo ObjApplicantInsuedInfo;
            ObjApplicantInsuedInfo = new ClsApplicantInsuedInfo();
            if (hidCUSTOMER_ID.Value != "")
            {
                ObjApplicantInsuedInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
            }           
            ObjApplicantInsuedInfo.SUFFIX = txtSUFFIX.Text;   
           
            if (cmbSTATE.SelectedItem != null && cmbSTATE.SelectedItem.Value != "")
                ObjApplicantInsuedInfo.STATE = cmbSTATE.SelectedValue;
            if (hidSTATE_ID.Value != "")
                ObjApplicantInsuedInfo.STATE = hidSTATE_ID.Value;
            else if (cmbSTATE.SelectedItem != null && cmbSTATE.SelectedItem.Value != "")
                ObjApplicantInsuedInfo.STATE = cmbSTATE.SelectedValue;
            
            //Set State ID
            if (hidSTATE_ID.Value != "")
                hidSTATE_ID_OLD.Value = hidSTATE_ID.Value;
            else if (cmbSTATE.SelectedItem != null && cmbSTATE.SelectedItem.Value != "")
                hidSTATE_ID_OLD.Value = cmbSTATE.SelectedValue;            
            
            ObjApplicantInsuedInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

            //-----------------------Added by Mohit-----------------
            if (cmbCO_APPLI_OCCU.SelectedValue != "")
            {
                ObjApplicantInsuedInfo.CO_APPLI_OCCU = cmbCO_APPLI_OCCU.SelectedValue;
            }
            ObjApplicantInsuedInfo.CO_APPLI_EMPL_NAME = txtCO_APPLI_EMPL_NAME.Text;
            ObjApplicantInsuedInfo.CO_APPLI_EMPL_ADDRESS = txtCO_APPLI_EMPL_ADDRESS.Text;
            ObjApplicantInsuedInfo.CO_APPLI_EMPL_ADDRESS1 = txtCO_APPLI_EMPL_ADDRESS1.Text;
            ObjApplicantInsuedInfo.CO_APPLI_EMPL_CITY = txtCO_APPLI_EMPL_CITY.Text;
            if (cmbCO_APPLI_EMPL_COUNTRY.SelectedValue != "")
            {
                ObjApplicantInsuedInfo.CO_APPLI_EMPL_COUNTRY = cmbCO_APPLI_EMPL_COUNTRY.SelectedValue;
            }
            ObjApplicantInsuedInfo.ACCOUNT_NUMBER = txtACCOUNT_NUMBER.Text.ToString();
            ObjApplicantInsuedInfo.BANK_BRANCH = txtBANK_BRANCH.Text.ToString();
            ObjApplicantInsuedInfo.BANK_NAME = txtBANK_NAME.Text.ToString();
            ObjApplicantInsuedInfo.BANK_NUMBER = txtBANK_NUMBER.Text.ToString();
            if (cmbACCOUNT_TYPE.SelectedValue != "")
            {
                ObjApplicantInsuedInfo.ACCOUNT_TYPE = int.Parse(cmbACCOUNT_TYPE.SelectedValue);
            }
            if (hidEmpDetails_STATE_ID.Value != "")
                ObjApplicantInsuedInfo.CO_APPLI_EMPL_STATE = hidEmpDetails_STATE_ID.Value;
            else if (cmbCO_APPLI_EMPL_STATE.SelectedItem != null && cmbCO_APPLI_EMPL_STATE.SelectedItem.Value != "")
                ObjApplicantInsuedInfo.CO_APPLI_EMPL_STATE = cmbCO_APPLI_EMPL_STATE.SelectedValue;

            //Set Emp State ID
            if (hidEmpDetails_STATE_ID.Value != "")
                hidEMPL_STATE_ID_OLD.Value = hidEmpDetails_STATE_ID.Value;
            else if (cmbCO_APPLI_EMPL_STATE.SelectedItem != null && cmbCO_APPLI_EMPL_STATE.SelectedItem.Value != "")
                hidEMPL_STATE_ID_OLD.Value = cmbCO_APPLI_EMPL_STATE.SelectedValue;

            ObjApplicantInsuedInfo.CO_APPLI_EMPL_ZIP_CODE = txtCO_APPLI_EMPL_ZIP_CODE.Text;
            ObjApplicantInsuedInfo.CO_APPLI_EMPL_PHONE = txtCO_APPLI_EMPL_PHONE.Text;
            ObjApplicantInsuedInfo.CO_APPLI_EMPL_EMAIL = txtCO_APPLI_EMPL_EMAIL.Text;

            if (cmbCO_APPL_RELATIONSHIP.SelectedItem != null && cmbCO_APPL_RELATIONSHIP.SelectedItem.Value != "")
                ObjApplicantInsuedInfo.CO_APPL_RELATIONSHIP = cmbCO_APPL_RELATIONSHIP.SelectedValue;
           
            if (txtCO_APPLI_YEARS_WITH_CURR_EMPL.Text.Trim() != "")
                ObjApplicantInsuedInfo.CO_APPLI_YEARS_WITH_CURR_EMPL = Double.Parse(txtCO_APPLI_YEARS_WITH_CURR_EMPL.Text);
            
            
            if (txtCO_APPL_YEAR_CURR_OCCU.Text.Trim() != "")
                ObjApplicantInsuedInfo.CO_APPL_YEAR_CURR_OCCU = double.Parse(txtCO_APPL_YEAR_CURR_OCCU.Text);
            if (txtCO_APPL_SSN_NO.Text != "")
            {
                ObjApplicantInsuedInfo.CO_APPL_SSN_NO = BusinessLayer.BlCommon.ClsCommon.EncryptString(txtCO_APPL_SSN_NO.Text);
                txtCO_APPL_SSN_NO.Text = "";
            }
            else
                ObjApplicantInsuedInfo.CO_APPL_SSN_NO = hidCO_APPL_SSN_NO.Value;

            //-----------------------End----------------------------


            // Added by mohit.
            // Check whether co applicant occupation is other.

            if (cmbCO_APPLI_OCCU.SelectedValue == "11427")
            {
                ObjApplicantInsuedInfo.DESC_CO_APPLI_OCCU = txtDESC_CO_APPLI_OCCU.Text;
            }
            else
            {
                ObjApplicantInsuedInfo.DESC_CO_APPLI_OCCU = "";
            }
            //These  assignments are common to all pages.
            strFormSaved = hidFormSaved.Value;
            oldXML = hidOldData.Value;
            //Returning the model object
            if (hidOldData.Value != "")
            {
                strRowId = hidAPPLICANT_ID.Value;
            }
            else
            {
                strRowId = "New";
            }
            //end comment 

            // Added By Lalit march 15,2010

            //if (cmbAPPLICANT_TYPE.SelectedValue != "")
            //{
            if (hidAPPLICANT_TYPE.Value != "")
            {
                ObjApplicantInsuedInfo.TYPE = int.Parse(hidAPPLICANT_TYPE.Value);
            }
            //}
            //if(cmbTITLE.SelectedValue != "")
            //    ObjApplicantInsuedInfo.TITLE = cmbTITLE.SelectedValue;
            if (hidTITLE.Value != "")
                ObjApplicantInsuedInfo.TITLE = int.Parse(hidTITLE.Value);
            
            ObjApplicantInsuedInfo.CONTACT_CODE = txtCONTACT_CODE.Text;
            ObjApplicantInsuedInfo.FIRST_NAME = txtFIRST_NAME.Text;
            ObjApplicantInsuedInfo.MIDDLE_NAME = txtMIDDLE_NAME.Text;
            ObjApplicantInsuedInfo.LAST_NAME = txtLAST_NAME.Text;
            ObjApplicantInsuedInfo.ACCOUNT_NUMBER = txtACCOUNT_NUMBER.Text.ToString();
            ObjApplicantInsuedInfo.BANK_BRANCH = txtBANK_BRANCH.Text.ToString();
            ObjApplicantInsuedInfo.BANK_NAME = txtBANK_NAME.Text.ToString();
            ObjApplicantInsuedInfo.BANK_NUMBER = txtBANK_NUMBER.Text.ToString();
            if (cmbACCOUNT_TYPE.SelectedValue != "")
            {
                ObjApplicantInsuedInfo.ACCOUNT_TYPE = int.Parse(cmbACCOUNT_TYPE.SelectedValue);
            }
            ObjApplicantInsuedInfo.ZIP_CODE = txtZIP_CODE.Text;
            ObjApplicantInsuedInfo.ADDRESS1 = txtADDRESS1.Text;
            ObjApplicantInsuedInfo.NUMBER = txtNUMBER.Text;
            ObjApplicantInsuedInfo.ADDRESS2 = txtADDRESS2.Text;
            ObjApplicantInsuedInfo.DISTRICT = txtDISTRICT.Text;
            ObjApplicantInsuedInfo.CITY = txtCITY.Text;
           
            


            //ObjApplicantInsuedInfo.STATE = cmbSTATE.SelectedValue;
            if (cmbCOUNTRY.SelectedValue != "")
            {
                ObjApplicantInsuedInfo.COUNTRY = cmbCOUNTRY.SelectedValue;
            }
            ObjApplicantInsuedInfo.CPF_CNPJ = txtCPF_CNPJ.Text;
            ObjApplicantInsuedInfo.PHONE = txtPHONE.Text;
            ObjApplicantInsuedInfo.BUSINESS_PHONE=txtCO_APPLI_BUSINESS_PHONE.Text;
            ObjApplicantInsuedInfo.EXT = txtEXT.Text;
            ObjApplicantInsuedInfo.MOBILE = txtMOBILE.Text;
            ObjApplicantInsuedInfo.FAX=txtFAX.Text;
            ObjApplicantInsuedInfo.EMAIL = txtEMAIL.Text;
            ObjApplicantInsuedInfo.REGIONAL_IDENTIFICATION = txtREGIONAL_IDENTIFICATION.Text;

            //if (txtREG_ID_ISSUE.Text.Trim() != "" )
            //   // ObjApplicantInsuedInfo.REG_ID_ISSUE = ConvertToDate(txtREG_ID_ISSUE.Text);
            //ObjApplicantInsuedInfo.REG_ID_ISSUE = Convert.ToDateTime(txtREG_ID_ISSUE.Text);
            if (txtREG_ID_ISSUE.Text != "")
            {
                ObjApplicantInsuedInfo.REG_ID_ISSUE = Convert.ToDateTime(txtREG_ID_ISSUE.Text);
            }
            ObjApplicantInsuedInfo.ORIGINAL_ISSUE = txtORIGINAL_ISSUE.Text;
            
            //if (cmbPOSITION.SelectedValue != null)
            //    ObjApplicantInsuedInfo.POSITION = Convert.ToInt32(cmbPOSITION.SelectedValue);
            if (hidPOSITION.Value != "")
                ObjApplicantInsuedInfo.POSITION = int.Parse(hidPOSITION.Value);

            if (txtCO_APPL_DOB.Text.Trim() != "")
                ObjApplicantInsuedInfo.CO_APPL_DOB = ConvertToDate(txtCO_APPL_DOB.Text);
            if (cmbCO_APPL_MARITAL_STATUS.SelectedItem != null)
                ObjApplicantInsuedInfo.CO_APPL_MARITAL_STATUS = cmbCO_APPL_MARITAL_STATUS.SelectedValue;
            if (cmbCO_APPL_GENDER.SelectedItem!=null)
                ObjApplicantInsuedInfo.CO_APPL_GENDER = cmbCO_APPL_GENDER.SelectedValue; ;

            ObjApplicantInsuedInfo.NOTE = txtNOTE.Text;

            return ObjApplicantInsuedInfo;
        }
        #endregion

        #region "Web Event Handlers"


        //Added by Sibin 0n 14-10-08 for Itrack Issue 4843 for filling drop down for states
        private void cmbCO_APPLI_EMPL_COUNTRY_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int iCountry = int.Parse(this.cmbCO_APPLI_EMPL_COUNTRY.SelectedValue);

            ClsStates objStates = new ClsStates();

            DataTable dtStates = objStates.GetStatesForCountry(iCountry);
            if (dtStates != null && dtStates.Rows.Count > 0)
            {
                cmbCO_APPLI_EMPL_STATE.Items.Clear();
                cmbCO_APPLI_EMPL_STATE.DataSource = dtStates;
                cmbCO_APPLI_EMPL_STATE.DataTextField = STATE_NAME;
                cmbCO_APPLI_EMPL_STATE.DataValueField = STATE_ID;
                cmbCO_APPLI_EMPL_STATE.DataBind();
                cmbCO_APPLI_EMPL_STATE.Items.Insert(0, "");
            }

            if (hidOldData.Value != "")
            {
                string strSelectedState = ClsCommon.FetchValueFromXML("CO_APPLI_EMPL_STATE", hidOldData.Value);
                ListItem list = cmbCO_APPLI_EMPL_STATE.Items.FindByValue(strSelectedState);
                if (list != null)
                    cmbCO_APPLI_EMPL_STATE.SelectedIndex = cmbCO_APPLI_EMPL_STATE.Items.IndexOf(list);
            }

            iCountry = int.Parse(this.cmbCOUNTRY.SelectedValue);
            DataTable dtStates1 = objStates.GetStatesForCountry(iCountry);
            if (dtStates != null && dtStates.Rows.Count > 0)
            {
                cmbSTATE.Items.Clear();
                cmbSTATE.DataSource = dtStates;
                cmbSTATE.DataTextField = STATE_NAME;
                cmbSTATE.DataValueField = STATE_ID;
                cmbSTATE.DataBind();
                cmbSTATE.Items.Insert(0, "");
            }

            if (hidOldData.Value != "")
            {
                string strSelectedState = ClsCommon.FetchValueFromXML("STATE", hidOldData.Value);
                ListItem list = cmbSTATE.Items.FindByValue(strSelectedState);
                if (list != null)
                    cmbSTATE.SelectedIndex = cmbSTATE.Items.IndexOf(list);
            }

        } //Added till here by Sibin 0n 14-10-08 for Itrack Issue 4843

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
                //string isActive;

                //Retreiving the form values into model class object
                ClsApplicantInsuedInfo ObjApplicantInsuedInfo = GetFormValue();


                if (hidCUSTOMER_TYPE.Value == "11110" && hidPRIMARY_APP.Value == "Yes") //Customer is of Persoal type
                {
                    // if applicant is primary applicant & customer type is personal
                    ObjAddApplicationInsured = new ClsApplicantInsued(1);
                }
                else
                {
                    ObjAddApplicationInsured = new ClsApplicantInsued();
                }



                if (strRowId.ToUpper().Equals("NEW")) //save case
                {

                    ObjApplicantInsuedInfo.CREATED_BY = int.Parse(GetUserId());
                    ObjApplicantInsuedInfo.CREATED_DATETIME = DateTime.Now;
                    ObjApplicantInsuedInfo.IS_ACTIVE = "Y";

                    Cms.BusinessLayer.BlCommon.ClsCommon objCommon = new ClsCommon();

                    //----Commneted by mohit on 18/10/2005-----. 
                    // Check if customer is active.
                    // This functinality is shifted to index page.
                    //ClsApplicantInsued.CheckCustomerIsActive(int.Parse(hidCUSTOMER_ID.Value),out isActive);
                    //if (isActive == "Y")
                    //{
                    //Calling the add method of business layer class
                    //intRetVal = ObjAddApplicationInsured.Add(ObjApplicantInsuedInfo);	
                    //}
                    //else
                    //{ 
                    // 	intRetVal= -3;
                    //}
                    //----------------End----------------------.		
                    string strCustomInfo = "";
                    strCustomInfo = hidCUSTOMER_NAME.Value.ToString() + "~" + hidCUSTOMER_CODE.Value.ToString();

                    //Add New
                    intRetVal = ObjAddApplicationInsured.Add(ObjApplicantInsuedInfo, strCustomInfo, FirstName);


                    if (intRetVal > 0)
                    {
                        hidAPPLICANT_ID.Value = ObjApplicantInsuedInfo.APPLICANT_ID.ToString();
                        //Insert into the application level applicant
                        if (hidBackToApplication.Value == "1")
                            SaveApplicantAtApplication(Convert.ToInt32(hidAPPLICANT_ID.Value));
                        // Insert into the policy level applicant
                        else if (hidBackToApplication.Value == "2")
                            SaveApplicantAtPolicy(Convert.ToInt32(hidAPPLICANT_ID.Value));
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                        hidFormSaved.Value = "1";
                        hidOldData.Value = ClsApplicantInsued.GetXmlForPageControls(int.Parse(hidAPPLICANT_ID.Value));
                        GetDecryptData();
                        hidIS_ACTIVE.Value = "Y";
                     //   rfvcmbPOSITION.Enabled = false;
                        //trBody.Attributes.Add("style", "display:inline");
                        FetchCountryState(hidOldData.Value);
                        FetchEmpCountryState(hidOldData.Value);
                        //---------
                        hidAPPL_ID.Value = hidAPPLICANT_ID.Value;
                        revZIP_CODE.Enabled = false;
                        revREG_ID_ISSUE.Enabled = false;

                        GetOldData();
                        //btnAddNewApplication.Attributes.Add("style", "display:inline");
                    }
                    else if (intRetVal == -1) //Code already exist
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                        hidFormSaved.Value = "2";
                    }
                    
                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                        hidFormSaved.Value = "2";
                    }
                    lblMessage.Visible = true;
                } // end save case
                else //UPDATE CASE
                {

                    //Creating the Model object for holding the Old data
                    ClsApplicantInsuedInfo objOldApplicantInsuedInfo;
                    objOldApplicantInsuedInfo = new ClsApplicantInsuedInfo();

                    //Setting  the Old Page details(XML File containing old details) into the Model Object
                    base.PopulateModelObject(objOldApplicantInsuedInfo, hidOldData.Value);


                    //Setting those values into the Model object which are not in the page
                    ObjApplicantInsuedInfo.APPLICANT_ID = int.Parse(strRowId);
                    ObjApplicantInsuedInfo.MODIFIED_BY = int.Parse(GetUserId());
                    ObjApplicantInsuedInfo.LAST_UPDATED_DATETIME = DateTime.Now;

                    ObjApplicantInsuedInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

                    //Updating the record using business layer class object
                    intRetVal = ObjAddApplicationInsured.Update(objOldApplicantInsuedInfo, ObjApplicantInsuedInfo,FirstName);
                    if (intRetVal > 0)			// update successfully performed
                    {                    
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "31");
                        hidFormSaved.Value = "1";
                        hidOldData.Value = ClsApplicantInsued.GetXmlForPageControls(int.Parse(hidAPPLICANT_ID.Value));
                        GetDecryptData();
                        FetchCountryState(hidOldData.Value);
                        FetchEmpCountryState(hidOldData.Value);
                        hidAPPL_ID.Value = hidAPPLICANT_ID.Value;
                        GetOldData();
                    //    rfvcmbPOSITION.Enabled = false;
                        revZIP_CODE.Enabled = false;
                        revREG_ID_ISSUE.Enabled = false;
                    }
                    else if (intRetVal == -1)	// Duplicate code exist, update failed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("18");
                        hidFormSaved.Value = "1";
                    }
                    else
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "1";
                    }
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (ObjAddApplicationInsured != null)
                    ObjAddApplicationInsured.Dispose();
            }
        }

        private void SaveApplicantAtApplication(int ApplicantID)
        {
            ArrayList a1 = new ArrayList();
            Cms.Model.Application.ClsApplicantInsuredInfo objApp = new Cms.Model.Application.ClsApplicantInsuredInfo();
            objApp = GetApplicationLevelValue();
            objApp.APPLICANT_ID = ApplicantID;
            a1.Add(objApp);
            Cms.BusinessLayer.BlApplication.ClsApplicantInsured objApplicantInsured = new Cms.BusinessLayer.BlApplication.ClsApplicantInsured();
            objApplicantInsured.SavePrimaryApplicantInsured(a1);
        }

        private void UpdateApplicantAtApplication(int ApplicantID)
        {
            ArrayList a1 = new ArrayList();
            Cms.Model.Application.ClsApplicantInsuredInfo objApp = new Cms.Model.Application.ClsApplicantInsuredInfo();
            objApp = GetApplicationLevelValue();
            objApp.APPLICANT_ID = ApplicantID;
            objApp.MODIFIED_BY = int.Parse(GetUserId());
            a1.Add(objApp);
            Cms.BusinessLayer.BlApplication.ClsApplicantInsured objApplicantInsured = new Cms.BusinessLayer.BlApplication.ClsApplicantInsured();
        }

        private void SaveApplicantAtPolicy(int ApplicantID)
        {
            ArrayList a1 = new ArrayList();
            Cms.Model.Policy.ClsPolicyInsuredInfo objPol = new Cms.Model.Policy.ClsPolicyInsuredInfo();
            objPol = GetPolicyLevelValue();
            objPol.APPLICANT_ID = ApplicantID;
            a1.Add(objPol);
            Cms.BusinessLayer.BlApplication.ClsApplicantInsured objApplicantInsured = new Cms.BusinessLayer.BlApplication.ClsApplicantInsured();
            Cms.BusinessLayer.BlApplication.ClsGeneralInformation.SavePrimaryNamedInsured(a1);         
        }

        private Cms.Model.Policy.ClsPolicyInsuredInfo GetPolicyLevelValue()
        {
            //Creating the Model object for holding the New data
            Cms.Model.Policy.ClsPolicyInsuredInfo objPol = new Cms.Model.Policy.ClsPolicyInsuredInfo();
            objPol.CUSTOMER_ID = Convert.ToInt32(hidCUSTOMER_ID.Value);
            objPol.POLICY_ID = Convert.ToInt32(GetPolicyID());
            objPol.POLICY_VERSION_ID = Convert.ToInt32(GetPolicyVersionID());
            objPol.CREATED_BY = int.Parse(GetUserId());
            objPol.IS_PRIMARY_APPLICANT = 0;
            return objPol;
        }

        private Cms.Model.Application.ClsApplicantInsuredInfo GetApplicationLevelValue()
        {
            //Creating the Model object for holding the New data
            Cms.Model.Application.ClsApplicantInsuredInfo objApp = new Cms.Model.Application.ClsApplicantInsuredInfo();


            objApp.CUSTOMER_ID = Convert.ToInt32(hidCUSTOMER_ID.Value);
            objApp.APP_ID = Convert.ToInt32(GetAppID());
            objApp.APP_VERSION_ID = Convert.ToInt32(GetAppVersionID());
            objApp.CREATED_BY = int.Parse(GetUserId());
            objApp.IS_PRIMARY_APPLICANT = 0;
            return objApp;
        }


        /// <summary>
        /// Activates and deactivates  .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
        {
            string strCustomInfo;
            int result = 0;
            ClsApplicantInsued ObjApplicantInsued = new ClsApplicantInsued();
            ClsApplicantInsuedInfo ObjApplicantInsuedInfo = GetFormValue();
            try
            {
                Cms.BusinessLayer.BlCommon.stuTransactionInfo objStuTransactionInfo = new Cms.BusinessLayer.BlCommon.stuTransactionInfo();
                objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
                objStuTransactionInfo.loggedInUserName = GetUserName();
                objStuTransactionInfo.clientId = int.Parse(GetCustomerID());


                strCustomInfo = ";Co-Applicant Name = " + ObjApplicantInsuedInfo.FIRST_NAME + " " + ObjApplicantInsuedInfo.MIDDLE_NAME + " " + ObjApplicantInsuedInfo.LAST_NAME;
                //Added by Sibin on 16 Feb 07 for Itrack Issue 4964
                result = ObjApplicantInsued.GetPolicyNameInsured(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidAPPLICANT_ID.Value));

                if (result == 1)
                {
                    lblAllPolicy.Text = ObjApplicantInsued.GetNameInsuredPolicyNumber(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidAPPLICANT_ID.Value));
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1436")+" " + lblAllPolicy.Text;//"Unable to Deactivate as Applicant is a Named insured on the following"
                }//Added till here
                else
                {
                    if (hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
                    {
                        objStuTransactionInfo.transactionDescription = ClsMessages.FetchGeneralMessage("1437");//"Co-Applicant deactivated successfully.";
                        ObjApplicantInsued.TransactionInfoParams = objStuTransactionInfo;
                        ObjApplicantInsued.ActivateDeactivateApplicant(hidAPPLICANT_ID.Value, "N", strCustomInfo, int.Parse(GetUserId()), int.Parse(hidCUSTOMER_ID.Value.Trim()));
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "41");
                        hidIS_ACTIVE.Value = "N";
                    }
                    else
                    {
                        objStuTransactionInfo.transactionDescription = ClsMessages.FetchGeneralMessage("1438");//"Co-Applicant activated successfully.";
                        ObjApplicantInsued.TransactionInfoParams = objStuTransactionInfo;
                        ObjApplicantInsued.ActivateDeactivateApplicant(hidAPPLICANT_ID.Value, "Y", strCustomInfo, int.Parse(GetUserId()), int.Parse(hidCUSTOMER_ID.Value.Trim()));
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "40");
                        hidIS_ACTIVE.Value = "Y";

                    }
                }
                hidFormSaved.Value = "1";
                hidOldData.Value = ClsApplicantInsued.GetXmlForPageControls(int.Parse(hidAPPLICANT_ID.Value));
                GetDecryptData();
                string IsActiveStatus = ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value.Trim());
                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(IsActiveStatus);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                ExceptionManager.Publish(ex);
            }
            finally
            {
                lblMessage.Visible = true;
                if (ObjAddApplicationInsured != null)
                    ObjAddApplicationInsured.Dispose();
            }
        }
        #endregion

        private void SetCaptions()
        {
            capTITLE.Text = objResourceMgr.GetString("cmbTITLE");
            capCONTACT_CODE.Text = objResourceMgr.GetString("txtCONTACT_CODE");
            capFIRST_NAME.Text = objResourceMgr.GetString("txtFIRST_NAME");            
            capMIDDLE_NAME.Text = objResourceMgr.GetString("txtMIDDLE_NAME");
            capLAST_NAME.Text = objResourceMgr.GetString("txtLAST_NAME");
            capZIP_CODE.Text = objResourceMgr.GetString("txtZIP_CODE");
            capADDRESS1.Text = objResourceMgr.GetString("txtADDRESS1");
            //capADDRESS2.Text = objResourceMgr.GetString("txtNUMBER");
            //capCOMPLIMENT.Text = objResourceMgr.GetString("txtADDRESS2"); Changed By Amit Kr Mishra for insuror
            capADDRESS2.Text = objResourceMgr.GetString("txtADDRESS2"); //   Added By Amit Kr Mishra for insuror
            capNUMBER.Text = objResourceMgr.GetString("txtNUMBER");
            capDISTRICT.Text = objResourceMgr.GetString("txtDISTRICT");
            capCITY.Text = objResourceMgr.GetString("txtCITY");
            capCOUNTRY.Text = objResourceMgr.GetString("cmbCOUNTRY");
            capSTATE.Text = objResourceMgr.GetString("cmbSTATE");
            capCPF_CNPJ.Text = objResourceMgr.GetString("txtCPF_CNPJ");
            capPHONE.Text = objResourceMgr.GetString("txtPHONE");
            capCO_APPLI_BUSINESS_PHONE.Text = objResourceMgr.GetString("txtCO_APPLI_BUSINESS_PHONE");
            capEXT.Text = objResourceMgr.GetString("txtEXT");
            capMOBILE.Text = objResourceMgr.GetString("txtMOBILE");
            capFAX.Text = objResourceMgr.GetString("txtFAX");
            capEMAIL.Text = objResourceMgr.GetString("txtEMAIL");
            capREGIONAL_IDENTIFICATION.Text = objResourceMgr.GetString("txtREGIONAL_IDENTIFICATION");
            capREG_ID_ISSUE.Text = objResourceMgr.GetString("txtREG_ID_ISSUE");
            capORIGINAL_ISSUE.Text = objResourceMgr.GetString("txtORIGINAL_ISSUE");
            capPOSITION.Text = objResourceMgr.GetString("cmbPOSITION");
            capCO_APPL_DOB.Text = objResourceMgr.GetString("txtCO_APPL_DOB");
            capCO_APPL_MARITAL_STATUS.Text = objResourceMgr.GetString("cmbCO_APPL_MARITAL_STATUS");
            capCO_APPL_GENDER.Text = objResourceMgr.GetString("cmbCO_APPL_GENDER");
            capNOTE.Text = objResourceMgr.GetString("txtNOTE");
            hidZipeCodeVerificationMsg.Value = objResourceMgr.GetString("hidZipeCodeVerificationMsg");
            capAPPLICANT_TYPE.Text = objResourceMgr.GetString("cmbAPPLICANT_TYPE");
            capACCOUNT_NUMBER.Text = objResourceMgr.GetString("txtACCOUNT_NUMBER");
            capBANK_NAME.Text = objResourceMgr.GetString("txtBANK_NAME");
            capACCOUNT_TYPE.Text = objResourceMgr.GetString("cmbACCOUNT_TYPE");
            capBANK_BRANCH.Text = objResourceMgr.GetString("txtBANK_BRANCH");
            capBANK_NUMBER.Text = objResourceMgr.GetString("txtBANK_NUMBER");
            capCREATION_DATE.Text = objResourceMgr.GetString("txtCO_APPL_creation");
            btnAddNewApplication.Text = ClsMessages.GetButtonsText(ScreenId, "btnAddNewApplication");
            btnbacktocustomerassistent.Text = ClsMessages.GetButtonsText(ScreenId, "btnbacktocustomerassistent");
            Caplook.Text = objResourceMgr.GetString("hidLookup");
            //bt.Text = ClsMessages.GetButtonsText(ScreenId, "btnCopyClient");
            	 
            //Unused Resouces captions

            //capSUFFIX.Text = objResourceMgr.GetString("capSUFFIX");           
            //capCOUNTRY.Text = objResourceMgr.GetString("capCOUNTRY");
            //capSTATE.Text = objResourceMgr.GetString("capSTATE");     
            //capCO_APPLI_OCCUPATION.Text = objResourceMgr.GetString("cmbCO_APPLI_OCCU");
            //capCO_APPLI_EMPL_NAME.Text = objResourceMgr.GetString("capCO_APPLI_EMPL_NAME");
            //capCO_APPLI_EMPL_ADDRESS.Text = objResourceMgr.GetString("capCO_APPLI_EMPL_ADDRESS");
            //capCO_APPLI_EMPL_ADDRESS1.Text = objResourceMgr.GetString("capCO_APPLI_EMPL_ADDRESS1");
            //capCO_APPLI_EMPL_CITY.Text = objResourceMgr.GetString("capCO_APPLI_EMPL_CITY");
            //capCO_APPLI_EMPL_COUNTRY.Text = objResourceMgr.GetString("capCO_APPLI_EMPL_COUNTRY");
            //capCO_APPLI_EMPL_STATE.Text = objResourceMgr.GetString("capCO_APPLI_EMPL_ZIP_CODE");
            //capCO_APPLI_EMPL_ZIP_CODE.Text = objResourceMgr.GetString("capCO_APPLI_EMPL_ZIP_CODE");
            //capCO_APPLI_EMPL_PHONE.Text = objResourceMgr.GetString("capCO_APPLI_EMPL_PHONE");
            //capCO_APPLI_EMPL_EMAIL.Text = objResourceMgr.GetString("capCO_APPLI_EMPL_EMAIL");
            //capCO_APPLI_YEARS_WITH_CURR_EMPL.Text = objResourceMgr.GetString("capCO_APPLI_YEARS_WITH_CURR_EMPL");
            //capCO_APPL_YEAR_CURR_OCCU.Text = objResourceMgr.GetString("capCO_APPL_YEAR_CURR_OCCU");
            //capCO_APPL_SSN_NO.Text = objResourceMgr.GetString("capCO_APPL_SSN_NO");
            //capDESC_CO_APPLI_OCCU.Text = objResourceMgr.GetString("capDESC_CO_APPLI_OCCU");
            //capCO_APPL_RELATIONSHIP.Text = objResourceMgr.GetString("capCO_APPL_RELATIONSHIP");
            //capZIP_CODE.Text = objResourceMgr.GetString("capZIP_CODE");
            //capCITY.Text = objResourceMgr.GetString("capCITY");
            //capCO_APPLI_EMPL_PHONE.Text = objResourceMgr.GetString("capCO_APPLI_EMPL_PHONE");
        }

        private void GetDecryptData()
        {
            //Added by Mohit Agarwal 3-Sep-08
            if (hidOldData.Value.IndexOf("NewDataSet") >= 0)
            {
                XmlDocument objxml = new XmlDocument();

                objxml.LoadXml(hidOldData.Value);

                XmlNode node = objxml.SelectSingleNode("NewDataSet");
                foreach (XmlNode nodes in node.SelectNodes("Table"))
                {
                    XmlNode noder1 = nodes.SelectSingleNode("CO_APPL_SSN_NO");
                    //noder1.InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder1.InnerText);

                    hidCO_APPL_SSN_NO.Value = noder1.InnerText;
                    string strCO_APPL_SSN_NO = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder1.InnerText);
                    if (strCO_APPL_SSN_NO.Trim() != "")//Done for Itrack Issue 6063 on 7 July 09
                    {
                        string strvaln = "xxx-xx-";
                        //for(var len=0; len < document.getElementById('txtCO_APPL_SSN_NO').value.length-4; len++)
                        //	txtvaln += 'x';
                        strvaln += strCO_APPL_SSN_NO.Substring(strvaln.Length, strCO_APPL_SSN_NO.Length - strvaln.Length);
                        capCO_APPL_SSN_NO_HID.Text = strvaln;
                    }
                    else
                        capCO_APPL_SSN_NO_HID.Text = "";
                }
                objxml = null;
            }
        }

        private void GetOldDataXML()
        {
            if (Request.QueryString["APPLICANT_ID"] != null && Request.QueryString["APPLICANT_ID"].ToString().Length > 0)
            {
                hidOldData.Value = ClsApplicantInsued.GetXmlForPageControls(int.Parse(Request.QueryString["APPLICANT_ID"].ToString()));
                hidSTATE_ID_OLD.Value = ClsCommon.FetchValueFromXML("STATE", hidOldData.Value);
                hidEMPL_STATE_ID_OLD.Value = ClsCommon.FetchValueFromXML("CO_APPLI_EMPL_STATE", hidOldData.Value);
                GetDecryptData();
               
            }
            if (hidOldData.Value != "")
            {
                
            }            
            FetchCountryState(hidOldData.Value);
            FetchEmpCountryState(hidOldData.Value);
        }

        private void btnReset_Click(object sender, System.EventArgs e)
        {

        }

        private void btnMakePrimaryApplicant_Click(object sender, System.EventArgs e)
        {
            int result = 0;
            lblMessage.Visible = true;
            if (hidIS_ACTIVE.Value == "Y")
            {
                result = ClsApplicantInsued.SetPrimaryApplicantCustomer(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidAPPLICANT_ID.Value));
            }
            else
            {
                lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "708");
                return;
            }
            if (result > 1)
            {
                lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "707");
                hidOldData.Value = ClsApplicantInsued.GetXmlForPageControls(int.Parse(hidAPPLICANT_ID.Value));
                GetDecryptData();
                ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID", "<script>RefreshWebGrid(1," + hidAPPLICANT_ID.Value + ");</script>");

            }
            else
                lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "20");
        }

        private void hidTITLE_ServerChange(object sender, System.EventArgs e)
        {

        }
        private void GetOldData() 
        {
            if (Request.QueryString["APPLICANT_ID"] != null && Request.QueryString["APPLICANT_ID"].ToString().Length > 0)
            {
                DataSet dsAplicant = ClsApplicantInsued.GetAplicantDetails(int.Parse(Request.QueryString["APPLICANT_ID"].ToString()));
                DataTable dt = dsAplicant.Tables[0];
                if (dt.Rows.Count > 0)
                {

                    cmbAPPLICANT_TYPE.SelectedIndex = cmbAPPLICANT_TYPE.Items.IndexOf(cmbAPPLICANT_TYPE.Items.FindByValue(dt.Rows[0]["APPLICANT_TYPE"].ToString()));
                    //11110  for APLICANT_TYPE=Personal
                    if (cmbAPPLICANT_TYPE.SelectedValue == "11110")
                    {
                        //Personal
                        txtFIRST_NAME.Text = dt.Rows[0]["FIRST_NAME"].ToString();
                        capFIRST_NAME.Text = FirstName;
                        tdF_NAME.ColSpan = int.Parse("1");
                        tdF_NAME.Style.Add("width", "33%");
                        tdM_NAME.Style.Add("width", "34%");
                        tdL_NAME.Style.Add("width", "33%");
                        tdM_NAME.Style.Add("display", "inline");
                        tdL_NAME.Style.Add("display", "inline");
                        //itrack 1385,modified by naveen
                        txtFIRST_NAME.Attributes.Add("size", "65");
                        csvCREATION_DATE.Enabled = false ;
                        
                        //rfv2txtCO_APPL_DOB.Enabled = false;
                      //  rfvLAST_NAME.Attributes.Add("Enabled", "true");
                       // rfvLAST_NAME.Attributes.Add("IsValid", "false");
                       // rfvLAST_NAME.Attributes.Add("display", "inline");
                       // rfvLAST_NAME.Attributes.Add("display", "none");
                        rfvFIRST_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "10");
                        if (dt.Rows[0]["MIDDLE_NAME"] != System.DBNull.Value)
                        {
                            txtMIDDLE_NAME.Text = dt.Rows[0]["MIDDLE_NAME"].ToString();
                        }

                        if (dt.Rows[0]["LAST_NAME"] != System.DBNull.Value)
                        {
                            txtLAST_NAME.Text = dt.Rows[0]["LAST_NAME"].ToString();

                        }
                        //if (dt.Rows[0]["CO_APPL_DOB"] != System.DBNull.Value)
                        //{
                        //    txtCO_APPL_DOB.Text = dt.Rows[0]["CO_APPL_DOB"].ToString();
                        //}

                        if (dt.Rows[0]["CO_APPL_DOB"] != System.DBNull.Value)
                        {
                            DateTime DOB = Convert.ToDateTime(dt.Rows[0]["CO_APPL_DOB"]);
                            this.txtCO_APPL_DOB.Text = DOB.ToShortDateString();
                        }
                       // capCO_APPL_DOB.Text = objResourceMgr.GetString("txtCO_APPL_DOB");
                        txtCPF_CNPJ.MaxLength = 14;
                        PopulateTitle(cmbTITLE, "11110");
                        PopulatePosition(cmbPOSITION, "11110");
                    }
                    //11109  for APLICANT_TYPE=Commercial
                    else if (cmbAPPLICANT_TYPE.SelectedValue == "11109" || cmbAPPLICANT_TYPE.SelectedValue == "14725")
                    {
                        //Commercial

                        if (dt.Rows[0]["CUSTOMER_FIRST_NAME"] != System.DBNull.Value)
                        {
                            txtFIRST_NAME.Text = dt.Rows[0]["FIRST_NAME"].ToString();
                            capFIRST_NAME.Text = customerName;
                            //tdF_NAME.ColSpan = int.Parse("3");
                            //tdF_NAME.Style.Add("width", "100%");
                            //tdM_NAME.Style.Add("display", "none");
                            //tdL_NAME.Style.Add("display", "none");
                            txtFIRST_NAME.Attributes.Add("size", "65");
                            
                          //  rfvLAST_NAME.Enabled = false;
                            rfvFIRST_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "11");
                            txtCPF_CNPJ.MaxLength = 18;

                        }
                        //rfvREG_ID_ISSUE.Enabled = false;
                        //rfvREGIONAL_IDENTIFICATION.Enabled = false;
                        //rfvDATE_OF_BIRTH.Enabled = false;
                        //rfvGENDER.Enabled = false;
                       // rfvORIGINAL_ISSUE.Enabled = false;
                        //rfvCO_APPL_MARITAL_STATUS.Enabled = false;

                        cpvREG_ID_ISSUE.Enabled = true;
                        cpvREG_ID_ISSUE2.Enabled = false;
                    
                        csvCO_APPL_DOB.Enabled = false;
                      //  capCO_APPL_DOB.Text = objResourceMgr.GetString("txtCO_APPL_creation");
                        PopulateTitle(cmbTITLE, "11109");
                        PopulatePosition(cmbPOSITION, "11109");
                        if (dt.Rows[0]["CO_APPL_DOB"] != System.DBNull.Value)
                        {
                            DateTime DOB = Convert.ToDateTime(dt.Rows[0]["CO_APPL_DOB"]);
                            this.txtCO_APPL_DOB.Text = DOB.ToShortDateString();
                        }
                    }

                   // cmbAPPLICANT_TYPE.SelectedValue = dt.Rows[0]["APPLICANT_TYPE"].ToString(); Commented by kuldeep
                    cmbAPPLICANT_TYPE.SelectedIndex = cmbAPPLICANT_TYPE.Items.IndexOf(cmbAPPLICANT_TYPE.Items.FindByValue(dt.Rows[0]["APPLICANT_TYPE"].ToString()));
                    cmbTITLE.SelectedIndex = cmbTITLE.Items.IndexOf(cmbTITLE.Items.FindByValue(dt.Rows[0]["TITLE"].ToString()));
                   // cmbTITLE.SelectedValue = dt.Rows[0]["TITLE"].ToString();
                    txtCONTACT_CODE.Text = dt.Rows[0]["CONTACT_CODE"].ToString();
                    txtCONTACT_CODE.Enabled = false;
                    txtZIP_CODE.Text = dt.Rows[0]["ZIP_CODE"].ToString();
                    txtADDRESS1.Text = dt.Rows[0]["ADDRESS1"].ToString();
                    txtNUMBER.Text = dt.Rows[0]["NUMBER"].ToString();
                    //txtCOMPLIMENT.Text = dt.Rows[0]["ADDRESS2"].ToString();
                    txtADDRESS2.Text = dt.Rows[0]["ADDRESS2"].ToString();
                    txtDISTRICT.Text = dt.Rows[0]["DISTRICT"].ToString();
                    txtCITY.Text = dt.Rows[0]["CITY"].ToString();
                    txtCPF_CNPJ.Text = dt.Rows[0]["CPF_CNPJ"].ToString();
                    txtPHONE.Text = dt.Rows[0]["PHONE"].ToString();
                    txtCO_APPLI_BUSINESS_PHONE.Text = dt.Rows[0]["BUSINESS_PHONE"].ToString();
                    txtEXT.Text = dt.Rows[0]["APPL_EXT"].ToString();
                    txtMOBILE.Text = dt.Rows[0]["CMOBILE"].ToString();
                    txtFAX.Text = dt.Rows[0]["FAX"].ToString();
                    txtEMAIL.Text = dt.Rows[0]["EMAIL"].ToString();
                    txtREGIONAL_IDENTIFICATION.Text = dt.Rows[0]["REGIONAL_IDENTIFICATION"].ToString();
                    if (dt.Rows[0]["REG_ID_ISSUE"].ToString() != null && dt.Rows[0]["REG_ID_ISSUE"].ToString() != string.Empty)
                    {
                        DateTime REG_ID = DateTime.Parse(dt.Rows[0]["REG_ID_ISSUE"].ToString());
                        txtREG_ID_ISSUE.Text = REG_ID.ToShortDateString();
                        
                    }
                    txtORIGINAL_ISSUE.Text = dt.Rows[0]["ORIGINAL_ISSUE"].ToString();
                    
                    if (dt.Rows[0]["CO_APPL_MARITAL_STATUS"] != System.DBNull.Value)
                    {
                        cmbCO_APPL_MARITAL_STATUS.SelectedIndex = cmbCO_APPL_MARITAL_STATUS.Items.IndexOf(cmbCO_APPL_MARITAL_STATUS.Items.FindByValue(dt.Rows[0]["CO_APPL_MARITAL_STATUS"].ToString()));
                    }
                    if (dt.Rows[0]["CO_APPL_GENDER"] != System.DBNull.Value)
                    {
                        cmbCO_APPL_GENDER.SelectedIndex = cmbCO_APPL_GENDER.Items.IndexOf(cmbCO_APPL_GENDER.Items.FindByValue(dt.Rows[0]["CO_APPL_GENDER"].ToString()));
                    }


                    cmbPOSITION.SelectedIndex = cmbPOSITION.Items.IndexOf(cmbPOSITION.Items.FindByValue(dt.Rows[0]["POSITION"].ToString()));
                    txtNOTE.Text = dt.Rows[0]["NOTE"].ToString();
                    cmbCOUNTRY.SelectedIndex = cmbCOUNTRY.Items.IndexOf(cmbCOUNTRY.Items.FindByValue(dt.Rows[0]["COUNTRY"].ToString()));
                    PopulateOldStateDropDown(int.Parse(cmbCOUNTRY.SelectedValue));

                    hidPOSITION.Value = dt.Rows[0]["POSITION"].ToString();
                    hidTITLE.Value = dt.Rows[0]["TITLE"].ToString();
                    cmbSTATE.SelectedIndex = cmbSTATE.Items.IndexOf(cmbSTATE.Items.FindByValue(dt.Rows[0]["STATE"].ToString()));
                    hidAPPLICANT_ID.Value = Request.QueryString["APPLICANT_ID"].ToString();
                    hidIS_ACTIVE.Value = dt.Rows[0]["IS_ACTIVE"].ToString();
                    btnActivateDeactivate.Enabled = true;
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(hidIS_ACTIVE.Value.Trim());
                    txtBANK_NUMBER.Text = dt.Rows[0]["BANK_NUMBER"].ToString();
                    txtBANK_BRANCH.Text = dt.Rows[0]["BANK_BRANCH"].ToString();
                    txtBANK_NAME.Text = dt.Rows[0]["BANK_NAME"].ToString();
                    txtACCOUNT_NUMBER.Text = dt.Rows[0]["ACCOUNT_NUMBER"].ToString();
                    cmbACCOUNT_TYPE.SelectedIndex = cmbACCOUNT_TYPE.Items.IndexOf(cmbACCOUNT_TYPE.Items.FindByValue(dt.Rows[0]["ACCOUNT_TYPE"].ToString()));
                }

            }
            else
            {
                if (hidAPPL_ID.Value != "")
                {
                    DataSet dsAplicant = ClsApplicantInsued.GetAplicantDetails(int.Parse(hidAPPL_ID.Value));
                    DataTable dt = dsAplicant.Tables[0];
                    if (dt.Rows.Count > 0)
                    {

                        cmbAPPLICANT_TYPE.SelectedIndex = cmbAPPLICANT_TYPE.Items.IndexOf(cmbAPPLICANT_TYPE.Items.FindByValue(dt.Rows[0]["APPLICANT_TYPE"].ToString()));
                        //11110  for APLICANT_TYPE=Personal
                        if (cmbAPPLICANT_TYPE.SelectedValue == "11110")
                        {
                            //Personal
                            txtFIRST_NAME.Text = dt.Rows[0]["FIRST_NAME"].ToString();
                            capFIRST_NAME.Text = FirstName;                 
                            tdF_NAME.ColSpan = int.Parse("1");
                            tdF_NAME.Style.Add("width", "33%");
                            tdM_NAME.Style.Add("width", "33%");
                            tdL_NAME.Style.Add("width", "33%");
                            tdM_NAME.Style.Add("display", "inline");
                            tdL_NAME.Style.Add("display", "inline");
                            txtFIRST_NAME.Attributes.Add("size", "35");
                            //rfv2txtCO_APPL_DOB.Enabled = false;
                           // rfvLAST_NAME.Attributes.Add("Enabled", "true");
                          //  rfvLAST_NAME.Attributes.Add("IsValid", "false");
                           // rfvLAST_NAME.Attributes.Add("display", "inline");
                          //  rfvLAST_NAME.Attributes.Add("display", "none");                            
                            rfvFIRST_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "10");
                            if (dt.Rows[0]["MIDDLE_NAME"] != System.DBNull.Value)
                            {
                                txtMIDDLE_NAME.Text = dt.Rows[0]["MIDDLE_NAME"].ToString();
                            }

                            if (dt.Rows[0]["LAST_NAME"] != System.DBNull.Value)
                            {
                                txtLAST_NAME.Text = dt.Rows[0]["LAST_NAME"].ToString();

                            }
                            //if (dt.Rows[0]["CO_APPL_DOB"] != System.DBNull.Value)
                            //{
                            //    txtCO_APPL_DOB.Text = dt.Rows[0]["CO_APPL_DOB"].ToString();
                            //}
                            if (dt.Rows[0]["CO_APPL_DOB"] != System.DBNull.Value)
                            {
                                DateTime DOB = Convert.ToDateTime(dt.Rows[0]["CO_APPL_DOB"]);
                                this.txtCO_APPL_DOB.Text = DOB.ToShortDateString();
                            }
                            PopulateTitle(cmbTITLE, "11110");
                            PopulatePosition(cmbPOSITION, "11110");
                            txtCPF_CNPJ.MaxLength = 14;
                        }
                        //11109  for APLICANT_TYPE=Commercial
                        else if (cmbAPPLICANT_TYPE.SelectedValue == "11109" || cmbAPPLICANT_TYPE.SelectedValue == "14725")
                        {
                            //Commercial

                            if (dt.Rows[0]["CUSTOMER_FIRST_NAME"] != System.DBNull.Value)
                            {
                                txtFIRST_NAME.Text = dt.Rows[0]["FIRST_NAME"].ToString();
                                capFIRST_NAME.Text = customerName;
                                //tdF_NAME.ColSpan = int.Parse("3");
                                //tdF_NAME.Style.Add("width", "100%");
                                //tdM_NAME.Style.Add("display", "none");
                                //tdL_NAME.Style.Add("display", "none");
                                txtFIRST_NAME.Attributes.Add("size", "65");
                              //  rfvLAST_NAME.Attributes.Add("enabled", "false");
                              //  rfvLAST_NAME.Attributes.Add("isvalid", "true");
                              //  rfvLAST_NAME.Attributes.Add("display", "none");
                                rfvFIRST_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "11");
                                txtCPF_CNPJ.MaxLength = 18;

                            }
                            //rfvREG_ID_ISSUE.Enabled = false;
                            //rfvREGIONAL_IDENTIFICATION.Enabled = false;
                            //rfvDATE_OF_BIRTH.Enabled = false;
                            //rfvGENDER.Enabled = false;
                            //rfvORIGINAL_ISSUE.Enabled = false;
                            //rfvCO_APPL_MARITAL_STATUS.Enabled = false;
                            csvCREATION_DATE.Enabled = true;
                            PopulateTitle(cmbTITLE, "11109");
                            PopulatePosition(cmbPOSITION, "11109");

                            if (dt.Rows[0]["CO_APPL_DOB"] != System.DBNull.Value)
                            {
                                DateTime DOB = Convert.ToDateTime(dt.Rows[0]["CO_APPL_DOB"]);
                                this.txtCO_APPL_DOB.Text = DOB.ToShortDateString();
                            }
                        }

                        cmbAPPLICANT_TYPE.SelectedValue = dt.Rows[0]["APPLICANT_TYPE"].ToString();

                        cmbTITLE.SelectedIndex = cmbTITLE.Items.IndexOf(cmbTITLE.Items.FindByValue(dt.Rows[0]["TITLE"].ToString()));
                        
                        txtFIRST_NAME.Text = dt.Rows[0]["FIRST_NAME"].ToString();
                        txtCONTACT_CODE.Text = dt.Rows[0]["CONTACT_CODE"].ToString();
                        txtCONTACT_CODE.Enabled = false;
                        txtZIP_CODE.Text = dt.Rows[0]["ZIP_CODE"].ToString();
                        txtADDRESS1.Text = dt.Rows[0]["ADDRESS1"].ToString();
                        txtNUMBER.Text = dt.Rows[0]["NUMBER"].ToString();
                        //txtCOMPLIMENT.Text = dt.Rows[0]["ADDRESS2"].ToString();
                        txtADDRESS2.Text = dt.Rows[0]["ADDRESS2"].ToString();
                        txtDISTRICT.Text = dt.Rows[0]["DISTRICT"].ToString();
                        txtCITY.Text = dt.Rows[0]["CITY"].ToString();
                        txtCPF_CNPJ.Text = dt.Rows[0]["CPF_CNPJ"].ToString();
                        txtPHONE.Text = dt.Rows[0]["PHONE"].ToString();
                        txtCO_APPLI_BUSINESS_PHONE.Text = dt.Rows[0]["BUSINESS_PHONE"].ToString();
                        txtEXT.Text = dt.Rows[0]["APPL_EXT"].ToString();
                        txtMOBILE.Text = dt.Rows[0]["CMOBILE"].ToString();
                        txtFAX.Text = dt.Rows[0]["FAX"].ToString();
                        txtEMAIL.Text = dt.Rows[0]["EMAIL"].ToString();
                        txtREGIONAL_IDENTIFICATION.Text = dt.Rows[0]["REGIONAL_IDENTIFICATION"].ToString();
                        txtORIGINAL_ISSUE.Text = dt.Rows[0]["ORIGINAL_ISSUE"].ToString();
                        txtNOTE.Text = dt.Rows[0]["NOTE"].ToString();
                        cmbPOSITION.SelectedIndex = cmbPOSITION.Items.IndexOf(cmbPOSITION.Items.FindByValue(dt.Rows[0]["POSITION"].ToString()));

                        if (dt.Rows[0]["REG_ID_ISSUE"] != System.DBNull.Value)
                        {
                            DateTime DataTbl = DateTime.Parse(dt.Rows[0]["REG_ID_ISSUE"].ToString());
                            txtREG_ID_ISSUE.Text = DataTbl.ToShortDateString();
                           
                        }
                        cmbCOUNTRY.SelectedIndex = cmbCOUNTRY.Items.IndexOf(cmbCOUNTRY.Items.FindByValue(dt.Rows[0]["COUNTRY"].ToString()));
                        PopulateOldStateDropDown(int.Parse(cmbCOUNTRY.SelectedValue));
                        cmbSTATE.SelectedIndex = cmbSTATE.Items.IndexOf(cmbSTATE.Items.FindByValue(dt.Rows[0]["STATE"].ToString()));
                        hidIS_ACTIVE.Value = dt.Rows[0]["IS_ACTIVE"].ToString();
                        hidAPPL_ID.Value = "";
                        btnActivateDeactivate.Enabled = true;
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(hidIS_ACTIVE.Value.Trim());
                        txtBANK_NUMBER.Text = dt.Rows[0]["BANK_NUMBER"].ToString();
                        txtBANK_BRANCH.Text = dt.Rows[0]["BANK_BRANCH"].ToString();
                        txtBANK_NAME.Text = dt.Rows[0]["BANK_NAME"].ToString();
                        txtACCOUNT_NUMBER.Text = dt.Rows[0]["ACCOUNT_NUMBER"].ToString();
                        cmbACCOUNT_TYPE.SelectedIndex = cmbACCOUNT_TYPE.Items.IndexOf(cmbACCOUNT_TYPE.Items.FindByValue(dt.Rows[0]["ACCOUNT_TYPE"].ToString()));
                    }

                }
            }

        }
        private void PopulateOldStateDropDown(int COUNTRY_ID)
        {
            ClsStates objStates = new ClsStates();
            DataSet dsStates;
            if (COUNTRY_ID == 0)
                return;
            else
            {
                dsStates = objStates.GetStatesCountry(COUNTRY_ID);            

            }
            cmbCO_APPLI_EMPL_STATE.Items.Clear();
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
        private void PopulatePosition(DropDownList DDL, string CustomerType)
        {
            try
            {
                CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();
                string result = "";
                result = obj.CustomerTypeTitlesNew(CustomerType);
                DataSet ds = new DataSet();
                ds.ReadXml(new System.IO.StringReader(result));
                DDL.DataSource = ds;
                DDL.DataTextField = "ACTIVITY_DESC";
                DDL.DataValueField = "ACTIVITY_ID";
                DDL.DataBind();
                DDL.Items.Insert(0, "");
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }
        private void PopulateTitle(DropDownList DDL,string CustomerType) 
        {
            try
            {
                CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();
                string result = "";
                result = obj.CustomerTypeTitlesNew(CustomerType);
                DataSet ds = new DataSet();                
                ds.ReadXml(new System.IO.StringReader(result));
                DDL.DataSource = ds;
                DDL.DataTextField = "ACTIVITY_DESC";
                DDL.DataValueField = "ACTIVITY_ID";
                DDL.DataBind();
                DDL.Items.Insert(0, "");
              }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }
        [Ajax.AjaxMethod()]
        public string AjaxFetchZipForState(int stateID, string ZipID)
        {
            CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();
            string result = "";
            result = obj.FetchZipForState(stateID, ZipID);
            return result;

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
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public DataSet AjaxFillTitles(string CustomerType)
        {
            try
            {
                CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();
                string result = "";
                result = obj.CustomerTypeTitlesNew(CustomerType);
                DataSet ds = new DataSet();
                ds.ReadXml(new System.IO.StringReader(result));
                return ds;
            }
            catch  (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
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