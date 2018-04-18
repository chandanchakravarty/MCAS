/******************************************************************************************
<Author				: - Vijay
<Start Date			: -	3/24/2005 10:37:29 AM
<End Date			: -	March 31, 2005
<Description		: - This page is used to add information into the Customer table	
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - April 7, 2005
<Modified By		: - Anshuman, Shrikant
<Purpose			: - Applying model approach for update and add.

<Modified Date		: - April 12, 2005
<Modified By		: - Anshuman, Shrikant
<Purpose			: - Applying model approach for update and add.

<Modified Date		: - April 28, 2005
<Modified By		: - Anurag Verma
<Purpose			: - Setting CustomerID Session variable in insert and update function

<Modified Date		: - May 17, 2005
<Modified By		: - Gaurav
<Purpose			: - Added FillCombo() Method to fill Business Type dropdwon from Lookup 

<Modified Date		: - May 27, 2005
<Modified By		: - Gaurav
<Purpose			: - Added Extra Reason Code

<Modified Date		: - July 7, 2005
<Modified By		: - Anurag Verma
<Purpose			: - preventing commerical data to be saved in case personal is selected as customer type

<Modified Date		: - Nov 04, 2005
<Modified By		: - Pradeep
<Purpose			: - Retrieved Customer reason codes along with Insurance score and 
						placed check on retrieval within the time period.
						

<Modified Date		: - 4/11/2005
<Modified By		: - Mohit
<Purpose			: - Adding description field in case Applicant Occupation selected option is "Other" 

<Modified Date		: - Nov 08,2005
<Modified By		: - Sumit Chhabra
<Purpose			: - Added two new buttons for adding new quote and new application

<Modified Date		: - Nov 21,2005
<Modified By		: - Shafi
<Purpose			: - Changed ThePermission of Back to Customer Assistant and Back To Search To Read 

<Modified Date		: - July 20, 2006
<Modified By		: - Anurag Verma
<Purpose			: - changing validation expression of revCUSTOMER_WEBSITE validator

<Modified Date		: - Nov 08, 2006
<Modified By		: - Mohit Agarwal
<Purpose			: - changing insurance score behaviour
*******************************************************************************************/

#region using Declare Namespaces
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
#endregion

namespace Cms.Client.Aspx
{
    /// <summary>
    /// This class is used for building the AddCustomer page
    /// Contains logic for saving and updating the customer information
    /// </summary>
    public class AddCustomer : Cms.Client.clientbase
    {
        #region//START:*********** Local variables to store valid control values  *************

        int intCUSTOMER_ID = 0;
        string strRowId;
        public string customerName, javasciptmsg, javasciptCPFmsg, javasciptCNPJmsg, CPF_invalid, CNPJ_invalid;
        public string FirstName;
        int intLoggedInUserID;
        //string strXML ="";
        //int iCustomerID;
        public string tabcaption = ""; //ClsMessages.GetTabTitles("134", "1");

        //creating resource manager object (used for reading field and label mapping)


        private Cms.BusinessLayer.BlClient.ClsCustomer objCustomer;
        #endregion

        #region webForm controls declaration

        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_LAST_NAME;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_ADDRESS2;

        //protected System.Web.UI.WebControls.TextBox txtAltCUSTOMER_ADDRESS1;
        //protected System.Web.UI.WebControls.TextBox txtAltCUSTOMER_ADDRESS2;
        //protected System.Web.UI.WebControls.TextBox txtAltCUSTOMER_CITY;
        //protected System.Web.UI.WebControls.DropDownList cmbAltCUSTOMER_COUNTRY;
        //protected System.Web.UI.WebControls.DropDownList cmbAltCUSTOMER_STATE;
        //protected System.Web.UI.WebControls.TextBox txtAltCUSTOMER_ZIP;



        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_CITY;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_CONTACT_NAME;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_PAGER_NO;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_WEBSITE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCREATION_DATE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDATE_OF_BIRTH;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidBackToApplication;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPREFIX;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomer_AGENCY_ID;
        //protected System.Web.UI.HtmlControls.HtmlInputHidden hidLookup;
        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected Cms.CmsWeb.Controls.CmsButton btnCopyCustomerAddress;
        protected System.Web.UI.WebControls.DropDownList cmbCUSTOMER_TYPE;
        protected System.Web.UI.WebControls.DropDownList cmbAMOUNT_TYPE;
        protected System.Web.UI.WebControls.DropDownList cmbCUSTOMER_STATE;
        protected System.Web.UI.WebControls.DropDownList cmbCUSTOMER_BUSINESS_TYPE;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvCUSTOMER_ADDRESS1;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvCUSTOMER_COUNTRY;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvMONTHLY_INCOME;
       // protected System.Web.UI.WebControls.RequiredFieldValidator rfvNET_ASSETS_AMOUNT;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCUSTOMER_TYPE;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_ADDRESS1;
        protected System.Web.UI.WebControls.DropDownList cmbCUSTOMER_COUNTRY;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvCUSTOMER_STATE;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_ZIP;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvCUSTOMER_ZIP;

        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCUSTOMER_ZIP;
        protected System.Web.UI.WebControls.RegularExpressionValidator revEMPLOYER_ZIPCODE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCUSTOMER_BUSINESS_PHONE;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_BUSINESS_PHONE;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_EXT;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCUSTOMER_EXT;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_HOME_PHONE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCUSTOMER_HOME_PHONE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revEMPLOYER_HOMEPHONE;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_MOBILE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCUSTOMER_MOBILE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCUSTOMER_FAX;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_FAX;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCUSTOMER_Email;
        protected System.Web.UI.WebControls.RegularExpressionValidator revEMPLOYER_EMAIL;
        protected System.Web.UI.WebControls.RegularExpressionValidator revNET_ASSETS_AMOUNT;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_Email;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
        protected System.Web.UI.WebControls.Label lblIS_ACTIVE;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Label lblMessage1;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_CODE;
        protected System.Web.UI.WebControls.TextBox txtID_TYPE;
        protected System.Web.UI.WebControls.TextBox txtCADEMP;
        protected System.Web.UI.WebControls.TextBox txtEMAIL_ADDRESS;
        protected System.Web.UI.WebControls.TextBox txtNATIONALITY;
        protected System.Web.UI.WebControls.TextBox txtREGIONAL_IDENTIFICATION_TYPE;
        protected System.Web.UI.WebControls.TextBox txtNET_ASSETS_AMOUNT;
        protected System.Web.UI.WebControls.TextBox txtMONTHLY_INCOME;
        protected System.Web.UI.WebControls.TextBox txtACCOUNT_NUMBER;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCUSTOMER_CODE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCUSTOMER_INSURANCE_RECEIVED_DATE;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_INSURANCE_RECEIVED_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCUSTOMER_INSURANCE_SCORE;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_INSURANCE_SCORE;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvCUSTOMER_CITY;
        protected System.Web.UI.WebControls.Label capIS_ACTIVE;
        protected System.Web.UI.WebControls.Label capCUSTOMER_TYPE;
        protected System.Web.UI.WebControls.Label capCUSTOMER_PARENT;
        protected System.Web.UI.WebControls.Label capCUSTOMER_FIRST_NAME;
        protected System.Web.UI.WebControls.Label capCUSTOMER_LAST_NAME;
        protected System.Web.UI.WebControls.Label capCUSTOMER_CODE;
        protected System.Web.UI.WebControls.Label capCUSTOMER_ADDRESS1;
        protected System.Web.UI.WebControls.Label capCUSTOMER_ADDRESS2;
        protected System.Web.UI.WebControls.Label capCUSTOMER_CITY;
        protected System.Web.UI.WebControls.Label capCUSTOMER_BUSINESS_TYPE;
        protected System.Web.UI.WebControls.Label capCUSTOMER_BUSINESS_DESC;
        protected System.Web.UI.WebControls.Label capCUSTOMER_CONTACT_NAME;
        protected System.Web.UI.WebControls.Label capCUSTOMER_BUSINESS_PHONE;
        protected System.Web.UI.WebControls.Label capCUSTOMER_EXT;
        protected System.Web.UI.WebControls.Label capCUSTOMER_HOME_PHONE;
        protected System.Web.UI.WebControls.Label capCUSTOMER_MOBILE;
        protected System.Web.UI.WebControls.Label capCUSTOMER_FAX;
        protected System.Web.UI.WebControls.Label capCUSTOMER_PAGER_NO;
        protected System.Web.UI.WebControls.Label capCUSTOMER_Email;
        protected System.Web.UI.WebControls.Label capCUSTOMER_WEBSITE;
        protected System.Web.UI.WebControls.Label capCUSTOMER_INSURANCE_SCORE;
        protected System.Web.UI.WebControls.Label capCUSTOMER_INSURANCE_RECEIVED_DATE;
        protected System.Web.UI.WebControls.Label capCUSTOMER_STATE;
        protected System.Web.UI.WebControls.Label capCUSTOMER_ZIP;
        protected System.Web.UI.WebControls.Label capCUSTOMER_COUNTRY;
        protected System.Web.UI.WebControls.Label capCREATION_DATE;
        protected System.Web.UI.WebControls.Label capACCOUNT_TYPE;

        protected System.Web.UI.WebControls.Label capAltCUSTOMER_ADDRESS1;
        protected System.Web.UI.WebControls.Label capAltCUSTOMER_ADDRESS2;
        protected System.Web.UI.WebControls.Label capAltCUSTOMER_CITY;
        protected System.Web.UI.WebControls.Label capAltCUSTOMER_COUNTRY;
        protected System.Web.UI.WebControls.Label capAltCUSTOMER_STATE;
        protected System.Web.UI.WebControls.Label capAltCUSTOMER_ZIP;
        protected System.Web.UI.WebControls.Label CapID_TYPE;
        protected System.Web.UI.WebControls.Label CapMONTHLY_INCOME;
        protected System.Web.UI.WebControls.Label CapAMOUNT_TYPE;
        protected System.Web.UI.WebControls.Label capCADEMP;
        protected System.Web.UI.WebControls.Label capNET_ASSETS_AMOUNT;
        protected System.Web.UI.WebControls.Label capREGIONAL_IDENTIFICATION_TYPE;
        protected System.Web.UI.WebControls.Label capNATIONALITY;
        protected System.Web.UI.WebControls.Label capEMAIL_ADDRESS;
        //protected System.Web.UI.WebControls.Label capMessage1;
        protected System.Web.UI.WebControls.Label capACCOUNT_NUMBER;
        protected System.Web.UI.WebControls.Label capBANK_NAME;
        protected System.Web.UI.WebControls.Label capBANK_BRANCH;
        protected System.Web.UI.WebControls.Label capBANK_NUMBER;
        protected System.Web.UI.WebControls.Label capIS_POLITICALLY_EXPOSED;
        protected System.Web.UI.WebControls.Label lblYES;
        protected System.Web.UI.WebControls.Label lblNO;
        protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden1;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldXML;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidRefreshTabIndex;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSaveMsg;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCarrierId;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revCUSTOMER_CITY;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCUSTOMER_WEBSITE;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_MIDDLE_NAME;
        protected System.Web.UI.WebControls.Label capCUSTOMER_MIDDLE_NAME;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_FIRST_NAME;
        protected System.Web.UI.WebControls.TextBox txtBANK_NAME;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCUSTOMER_FIRST_NAME;
        protected Cms.CmsWeb.Controls.CmsButton btnGetInsuranceScore;
        protected System.Web.UI.WebControls.HyperLink hlkCUSTOMER_INSURANCE_RECEIVED_DATE;
        protected System.Web.UI.WebControls.HyperLink hlkZipLookup;
        protected System.Web.UI.WebControls.Label capPREFIX;
        protected System.Web.UI.WebControls.Label capCUSTOMER_SUFFIX;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_SUFFIX;
        protected System.Web.UI.WebControls.DropDownList cmbPREFIX;
        protected System.Web.UI.WebControls.Label spnCUSTOMER_CONTACT_NAME;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvCUSTOMER_LAST_NAME;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCUSTOMER_FIRST_NAME;
        protected System.Web.UI.WebControls.DropDownList cmbCUSTOMER_REASON_CODE;
        protected System.Web.UI.WebControls.Label capCUSTOMER_REASON_CODE2;
        protected System.Web.UI.WebControls.DropDownList cmbCUSTOMER_REASON_CODE2;
        protected System.Web.UI.WebControls.Label capCUSTOMER_REASON_CODE3;
        protected System.Web.UI.WebControls.DropDownList cmbCUSTOMER_REASON_CODE3;
        protected System.Web.UI.WebControls.DropDownList cmbACCOUNT_TYPE;
        protected System.Web.UI.WebControls.Label capCUSTOMER_REASON_CODE4;
        protected System.Web.UI.WebControls.DropDownList cmbCUSTOMER_REASON_CODE4;
        protected System.Web.UI.WebControls.DropDownList cmbCUSTOMER_REASON_CODE1;
        protected System.Web.UI.WebControls.Label capCUSTOMER_REASON_CODE;
        protected Cms.CmsWeb.Controls.CmsButton btnBack;
        protected System.Web.UI.HtmlControls.HtmlImage imgSelect;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_PARENT;
        protected System.Web.UI.WebControls.Label Label1;
        protected System.Web.UI.WebControls.Label Label2;
        protected System.Web.UI.WebControls.Label Label3;
        //protected System.Web.UI.WebControls.Label capCUSTOMER_AGENCY_ID;
        protected System.Web.UI.WebControls.DropDownList cmbCUSTOMER_AGENCY_ID;
        protected System.Web.UI.WebControls.Label lblCUSTOMER_AGENCY_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCUSTOMER_AGENCY_ID;
        //protected System.Web.UI.WebControls.Label capcmbCUSTOMER_ACCOUNT_EXECUTIVE_ID;
        //protected System.Web.UI.WebControls.DropDownList cmbCUSTOMER_ACCOUNT_EXECUTIVE_ID;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvCUSTOMER_ACCOUNT_EXECUTIVE_ID;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvCUSTOMER_CSR;
        protected System.Web.UI.WebControls.Label capCUSTOMER_LATE_CHARGES;
        //Sumit Chhabra:21-12-2005:Control not in use
        //protected System.Web.UI.WebControls.CheckBox chkCUSTOMER_LATE_CHARGES;
        protected System.Web.UI.WebControls.Label capCUSTOMER_LATE_NOTICES;
        //Control not in use
        //protected System.Web.UI.WebControls.CheckBox chkCUSTOMER_LATE_NOTICES;
        protected System.Web.UI.WebControls.Label capCUSTOMER_SEND_STATEMENT;
        //Control not in use
        //protected System.Web.UI.WebControls.CheckBox chkCUSTOMER_SEND_STATEMENT;
        protected System.Web.UI.WebControls.Label capCUSTOMER_RECEIVABLE_DUE_DAYS;
        //Control not in use
        //protected System.Web.UI.WebControls.TextBox txtCUSTOMER_RECEIVABLE_DUE_DAYS;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCUSTOMER_RECEIVABLE_DUE_DAYS;
        protected System.Web.UI.WebControls.CompareValidator cmpCUSTOMER_RECEIVABLE_DUE_DAYS;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_PARENT_TEXT;
        protected Cms.CmsWeb.Controls.CmsButton btnCustomerAssistant;
        //protected Cms.CmsWeb.Controls.CmsButton btnCustomerPullAddress;
        //Nov 08,2005:Sumit Chhabra: Two new buttons for adding new quick quote and new application have been added
        protected Cms.CmsWeb.Controls.CmsButton btnAddNewQuickQuote;
        protected Cms.CmsWeb.Controls.CmsButton btnAddNewApplication;
        protected Cms.CmsWeb.Controls.CmsButton btnCopyClient;
        protected System.Web.UI.WebControls.CustomValidator csvCUSTOMER_INSURANCE_RECEIVED_DATE;
        protected System.Web.UI.HtmlControls.HtmlImage imgBusinessType;
       // protected System.Web.UI.WebControls.CheckBox chkType;
        protected System.Web.UI.WebControls.RadioButton rdYES;
        protected System.Web.UI.WebControls.RadioButton rdNO;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLAST_INSURANCE_SCORE_FETCHED;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDT_LAST_INSURANCE_SCORE_FETCHED;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCUSTOMER_PAGER_NO;
        protected System.Web.UI.WebControls.RegularExpressionValidator revEMAIL_ADDRESS;
        protected System.Web.UI.WebControls.RegularExpressionValidator revMONTHLY_INCOME;
        protected System.Web.UI.WebControls.CustomValidator csvCUSTOMER_BUSINESS_DESC;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_BUSINESS_DESC;
        protected Cms.CmsWeb.Controls.CmsButton btnViewMap;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidMsg;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidState_Code;
        public string strCalledFrom = "";
        public string insurancePeriod;
        public string insuranceInterval;
        public string ServiceURL;
        public int TotPolicy = 0;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_BUSINESS_TYPE_NAME;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_BUSINESS_TYPE;
        protected System.Web.UI.WebControls.Label capDATE_OF_BIRTH;
       
        protected System.Web.UI.WebControls.TextBox txtDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.TextBox txtCREATION_DATE;
        protected System.Web.UI.WebControls.HyperLink hlkDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.HyperLink hlkCREATION_DATE;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.RegularExpressionValidator revDATE_OF_BIRTH;
       // protected System.Web.UI.WebControls.RegularExpressionValidator revACCOUNT_NUMBER;
        protected System.Web.UI.WebControls.CustomValidator csvDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.CustomValidator csvCREATION_DATE;
        protected System.Web.UI.WebControls.Label capSSN_NO;
        protected System.Web.UI.WebControls.TextBox txtSSN_NO;
        protected System.Web.UI.WebControls.RegularExpressionValidator revSSN_NO;
        protected System.Web.UI.WebControls.Label capMARITAL_STATUS;
        protected System.Web.UI.WebControls.DropDownList cmbMARITAL_STATUS;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvMARITAL_STATUS;
        protected System.Web.UI.WebControls.Label capAPPLICANT_OCCU;
        protected System.Web.UI.WebControls.DropDownList cmbAPPLICANT_OCCU;
        protected System.Web.UI.WebControls.Label capEMPLOYER_NAME;
        protected System.Web.UI.WebControls.TextBox txtEMPLOYER_NAME;
        protected System.Web.UI.WebControls.TextBox txtBANK_NUMBER;
        //protected System.Web.UI.WebControls.Label capEMPLOYER_ADDRESS;
        //protected System.Web.UI.WebControls.TextBox txtEMPLOYER_ADDRESS;
        protected System.Web.UI.WebControls.Label capEMPLOYER_ADD1;
        protected System.Web.UI.WebControls.TextBox txtEMPLOYER_ADD1;
        protected System.Web.UI.WebControls.Label capEMPLOYER_ADD2;
        protected System.Web.UI.WebControls.TextBox txtEMPLOYER_ADD2;
        ///
        protected System.Web.UI.WebControls.Label capEMPLOYER_CITY;
        protected System.Web.UI.WebControls.TextBox txtEMPLOYER_CITY;
        protected System.Web.UI.WebControls.Label capEMPLOYER_COUNTRY;
        protected System.Web.UI.WebControls.DropDownList cmbEMPLOYER_COUNTRY;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvEMPLOYER_COUNTRY;
        protected System.Web.UI.WebControls.Label capEMPLOYER_STATE;
        protected System.Web.UI.WebControls.DropDownList cmbEMPLOYER_STATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvEMPLOYER_STATE;
       // protected System.Web.UI.WebControls.RegularExpressionValidator revBANK_BRANCH;
        protected System.Web.UI.WebControls.Label capEMPLOYER_EMAIL;
        protected System.Web.UI.WebControls.TextBox txtEMPLOYER_EMAIL;
        protected System.Web.UI.WebControls.Label capEMPLOYER_ZIPCODE;
        protected System.Web.UI.WebControls.TextBox txtEMPLOYER_ZIPCODE;
        protected System.Web.UI.WebControls.Label capEMPLOYER_HOMEPHONE;
        protected System.Web.UI.WebControls.TextBox txtEMPLOYER_HOMEPHONE;


        ///
        protected System.Web.UI.WebControls.Label capYEARS_WITH_CURR_EMPL;
        protected System.Web.UI.WebControls.TextBox txtYEARS_WITH_CURR_EMPL;
        protected System.Web.UI.WebControls.Label capYEARS_WITH_CURR_OCCU;
        protected System.Web.UI.WebControls.TextBox txtYEARS_WITH_CURR_OCCU;
        protected System.Web.UI.WebControls.RegularExpressionValidator revYEARS_WITH_CURR_EMPL;
        protected System.Web.UI.WebControls.RegularExpressionValidator revYEARS_WITH_CURR_OCCU;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCust_Type;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTabInsScore;
        protected System.Web.UI.WebControls.Label capDESC_APPLICANT_OCCU;
        protected System.Web.UI.WebControls.TextBox txtDESC_APPLICANT_OCCU;
        protected System.Web.UI.WebControls.Label lblDESC_APPLICANT_OCCU;
        protected System.Web.UI.WebControls.Label lblCUSTOMER_INSURANCE_SCORE;
        protected System.Web.UI.WebControls.Label lblSCORE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDESC_APPLICANT_OCCU;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvyesno;
        protected System.Web.UI.WebControls.Label capMOBILE;
        protected System.Web.UI.WebControls.TextBox txtMOBILE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revMOBILE;
        protected System.Web.UI.WebControls.Label capBUSINESS_PHONE;
        protected System.Web.UI.WebControls.TextBox txtBUSINESS_PHONE;
        protected System.Web.UI.WebControls.TextBox txtBANK_BRANCH;
        protected System.Web.UI.WebControls.RegularExpressionValidator revBUSINESS_PHONE;
        protected System.Web.UI.WebControls.Label capEXT;
        protected System.Web.UI.WebControls.TextBox txtEXT;
        protected System.Web.UI.WebControls.RegularExpressionValidator revEXT;
        System.Resources.ResourceManager objResourceMgr;
        protected System.Web.UI.WebControls.Label capGENDER;
        protected System.Web.UI.WebControls.DropDownList cmbGENDER;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvGENDER;
        // protected System.Web.UI.WebControls.RequiredFieldValidator rfvAPPLICANT_OCCU;
        protected System.Web.UI.WebControls.Label capPER_CUST_MOBILE;
        protected System.Web.UI.WebControls.TextBox txtPER_CUST_MOBILE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPER_CUST_MOBILE;
        protected System.Web.UI.WebControls.Label capEMP_EXT;
        protected System.Web.UI.WebControls.TextBox txtEMP_EXT;
        protected System.Web.UI.WebControls.RegularExpressionValidator revEMP_EXT;
        protected System.Web.UI.WebControls.TextBox txtAltCustomer_State;
        protected System.Web.UI.WebControls.Image imglookup;
        //protected System.Web.UI.WebControls.Image ImgZip;
        protected System.Web.UI.WebControls.Image imgZipLookup;
        protected System.Web.UI.WebControls.Image imgEmpZipLookup;
        protected System.Web.UI.WebControls.CheckBox chkCOMPLETE_APP;
        protected System.Web.UI.WebControls.Label capSCORE;
        protected System.Web.UI.WebControls.CheckBox chkSCORE;
        protected System.Web.UI.WebControls.HyperLink hlkEmpZipLookup;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCUSTOMER_CODE;
        protected System.Web.UI.WebControls.CustomValidator csvCUSTOMER_ZIP;
        //protected System.Web.UI.HtmlControls.HtmlGenericControl spnCUSTOMER_STATE;
        //protected System.Web.UI.HtmlControls.HtmlGenericControl spnCUSTOMER_ZIP;
        protected System.Web.UI.WebControls.CustomValidator csvEMPLOYER_ZIPCODE;
        protected System.Web.UI.WebControls.CustomValidator csvIS_POLITICALLY_EXPOSED;
        protected System.Web.UI.WebControls.Label capSSN_NO_HID;
        //START CONTROLS DECLARE ACCORDING TO NEW DOC
        //**** DECLARE LABELS 
        protected System.Web.UI.HtmlControls.HtmlTableCell tdF_NAME;
        protected System.Web.UI.HtmlControls.HtmlTableCell tdM_NAME;
        protected System.Web.UI.HtmlControls.HtmlTableCell tdL_NAME;
        protected System.Web.UI.HtmlControls.HtmlTableCell tdID_TYPE;
        protected System.Web.UI.HtmlControls.HtmlTableCell tdCADEMP;
        protected System.Web.UI.HtmlControls.HtmlTableRow trRNE;
        protected System.Web.UI.HtmlControls.HtmlTableCell tdNET_ASSETS_AMOUNT;
        protected System.Web.UI.HtmlControls.HtmlTableCell tdMONTHLY_INCOME;
        protected System.Web.UI.HtmlControls.HtmlTableCell tdAMOUNT_TYPE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEFAULT_COUNTRY;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidMAIN_TITLE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidMAIN_POSITION;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIN_FIRST_NAME;
        // protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIN_LAST_NAME;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvORIGINAL_ISSUE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIN_DISTRICT;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvDISTRICT;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIN_NUMBER;

        protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIN_CITY;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvREG_ID_ISSUE;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvREGIONAL_IDENTIFICATION;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIN_CONTACT_CODE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIN_ADDRESS;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIN_ZIPCODE;
        protected System.Web.UI.WebControls.HyperLink hlkREG_ID_ISSUE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revREG_ID_ISSUE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revMAIN_CONTACT_CODE;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvNUMBER;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfv2DATE_OF_BIRTH;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvACCOUNT_TYPE;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revBANK_NUMBER;
        protected System.Web.UI.WebControls.Label Caplook;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIN_STATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCPF_CNPJ;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCPF_CNPJ;
        protected System.Web.UI.WebControls.CustomValidator csvCPF_CNPJ;
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.WebControls.Label capMessages;

        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAIN_CPF_CNPJ;
        protected System.Web.UI.WebControls.RegularExpressionValidator revMAIN_CPF_CNPJ;
        protected System.Web.UI.WebControls.CustomValidator csvMAIN_CPF_CNPJ;
        protected System.Web.UI.WebControls.CustomValidator csvMAIN_NOTE;
        protected System.Web.UI.WebControls.CompareValidator cpvREG_ID_ISSUE;//ashish
        //protected System.Web.UI.WebControls.CompareValidator cpv3REG_ID_ISSUE;
        protected System.Web.UI.WebControls.CompareValidator cpvREG_ID_ISSUE2;
        //protected System.Web.UI.WebControls.CustomValidator csvMONTHLY_INCOME;
        protected Label capCPF_CNPJ;
        protected Label capNUMBER;
        protected Label capCUSTOMER_ADDRESS;
        protected Label capDISTRICT;
        protected Label capCUSTOMER_AGENCY_ID;
        protected Label capMAIN_TITLE;
        protected Label capMAIN_POSITION;
        protected Label capMAIN_CPF_CNPJ;
        protected Label capMAIN_ADDRESS;
        protected Label capMAIN_NUMBER;
        protected Label capMAIN_COMPLIMENT;
        protected Label capMAIN_DISTRICT;
        protected Label capMAIN_NOTE;
        protected Label capMAIN_CONTACT_CODE;
        protected Label capREGIONAL_IDENTIFICATION;
        protected Label capREG_ID_ISSUE;
        protected Label capORIGINAL_ISSUE;
        protected Label capMAIN_ZIPCODE;
        protected Label capMAIN_CITY;
        protected Label capMAIN_COUNTRY;
        protected Label capMAIN_STATE;
        protected Label capMAIN_FIRST_NAME;
        protected Label capMAIN_MIDDLE_NAME;
        protected Label capMAIN_LAST_NAME;
        //********DECLARE TEXTBOX
        //protected System.Web.UI.WebControls.TextBox txtMONTHLY_INCOME;
        protected TextBox txtMAIN_NOTE;
        protected TextBox txtMAIN_DISTRICT;
        protected TextBox txtMAIN_COMPLIMENT;
        protected TextBox txtMAIN_NUMBER;
        protected TextBox txtMAIN_ADDRESS;
        protected TextBox txtMAIN_CPF_CNPJ;
        protected TextBox txtDISTRICT;
        protected TextBox txtCOMPLIMENT;
        protected TextBox txtNUMBER;
        protected TextBox txtCPF_CNPJ;
        protected TextBox txtMAIN_CONTACT_CODE;
        protected TextBox txtREGIONAL_IDENTIFICATION;
        protected TextBox txtORIGINAL_ISSUE;
        protected TextBox txtMAIN_ZIPCODE;
        protected TextBox txtMAIN_CITY;
        protected TextBox txtREG_ID_ISSUE;
        protected TextBox txtMAIN_FIRST_NAME;
        protected TextBox txtMAIN_MIDDLE_NAME;
        protected TextBox txtMAIN_LAST_NAME;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidZipCodeCalledfor;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidZipeCodeVerificationMsg;
        protected HyperLink hypMAIN_ZIPCODE;
        //********DECLARE COMBOBOX


        protected DropDownList cmbMAIN_TITLE;
        protected DropDownList cmbMAIN_POSITION;
        protected DropDownList cmbREG_ID_ISSUE;
        protected DropDownList cmbMAIN_COUNTRY;
        protected DropDownList cmbMAIN_STATE;

        protected System.Web.UI.HtmlControls.HtmlGenericControl spnCPF_CNPJ;
        protected System.Web.UI.HtmlControls.HtmlGenericControl spnBROKER;
        //protected System.Web.UI.HtmlControls.HtmlGenericControl spnREG_ID_ISSUE;
        //protected System.Web.UI.HtmlControls.HtmlGenericControl spnORIGINAL_ISSUE;
        //protected System.Web.UI.HtmlControls.HtmlGenericControl spnREGIONAL_IDENTIFICATION;
        //protected System.Web.UI.HtmlControls.HtmlGenericControl spnMARITAL_STATUS;
        //protected System.Web.UI.HtmlControls.HtmlGenericControl spnGENDER;
        //protected System.Web.UI.HtmlControls.HtmlGenericControl spnDATE_OF_BIRTH;
        protected System.Web.UI.HtmlControls.HtmlGenericControl spnCREATION_DATE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidMessage;

        protected System.Web.UI.WebControls.RegularExpressionValidator revMAIN_ZIPCODE;
        protected System.Web.UI.WebControls.CustomValidator csvMAIN_ZIPCODE;
        //END DECLARE

        protected string REL;
        protected string BT;



        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSSN_NO;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_COUNTRY_LIST;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidEmpDetails_STATE_COUNTRY_LIST; //
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidEmpDetails_STATE_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID_OLD; //Added by Sibin on 16 Oct 08 for setting customer state 
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidEmpDetails_STATE_ID_OLD; //Added by Sibin on 16 Oct 08 for setting employer state
        protected System.Web.UI.HtmlControls.HtmlInputHidden HidBussiness;
        protected System.Web.UI.HtmlControls.HtmlInputHidden HidPersonal;
       
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidstatus2;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidstatus1;


      //  protected System.Web.UI.WebControls.CustomValidator cstREG_ID_ISSUE;

        #endregion

        private void Page_Load(object sender, System.EventArgs e)
        {
                     
            Ajax.Utility.RegisterTypeForAjax(typeof(AddCustomer));
            //Retreiving the user id
            //			if(cmbCUSTOMER_COUNTRY.SelectedIndex == 0)
            //			{
            //imgZipLookup.Attributes.Add("style", "cursor:hand");

            //string strJavascript = @"VerifyAddress(document.getElementById('"
            //    + txtCUSTOMER_ADDRESS1.ID + "'),document.getElementById('"
            //    + txtCUSTOMER_ADDRESS2.ID + "'),document.getElementById('" + txtCUSTOMER_CITY.ID
            //    + "'),document.getElementById('" + cmbCUSTOMER_STATE.ID
            //    + "'),document.getElementById('" + txtCUSTOMER_ZIP.ID + "'))";

            //imgZipLookup.Attributes.Add("onClick", strJavascript);
            //base.VerifyAddress(imgZipLookup, txtCUSTOMER_ADDRESS1, txtCUSTOMER_ADDRESS2
            //    , txtCUSTOMER_CITY, cmbCUSTOMER_STATE, txtCUSTOMER_ZIP);

            //			}
            //			else 
            //			{
            //				//imgZipLookup.Visible=false;
            //				//cmbCUSTOMER_COUNTRY.Attributes.Add("onBlur","javascript:DisableValidators();");
            //				cmbCUSTOMER_COUNTRY.Attributes.Add("onBlur","javascript:validatorControlsfail()");
            //			}

            //			imgZip.Attributes.Add("style","cursor:hand");
            //			base.VerifyAddress(imgZip, txtAltCUSTOMER_ADDRESS1,txtAltCUSTOMER_ADDRESS2
            //				, txtAltCUSTOMER_CITY, cmbAltCUSTOMER_STATE, txtAltCUSTOMER_ZIP);
            tabcaption = ClsMessages.GetTabTitles("134", "1");
            revCUSTOMER_CODE.ValidationExpression = aRegExpClientName;
            revCUSTOMER_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "64");
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            capMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1194");
            HidBussiness.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1321");
            REL = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1985");
            BT = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1986");
            HidPersonal.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1320");
            hidstatus1.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1322");//Active
            hidstatus2.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1323");//Inactive
           
            revMAIN_CONTACT_CODE.ValidationExpression = aRegExpClientName;
            revMAIN_CONTACT_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "64");
            
            //			this.cmbCUSTOMER_COUNTRY.SelectedIndex = int.Parse(aCountry);
            //this.cmbAltCUSTOMER_COUNTRY.SelectedIndex = int.Parse(aCountry);

            btnActivateDeactivate.Text = ClsMessages.FetchGeneralMessage("1333");
            intLoggedInUserID = int.Parse(base.GetUserId());
            if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"] != "")
            {
                base.ScreenId = "134_0";//can be Alpha Numeric 
               
                strCalledFrom = Request.QueryString["CalledFrom"].ToString();
                hidDEFAULT_COUNTRY.Value = "";
            }
            else
            {
                //ITrack 4765 Mohit Agarwal 18-Sep-08
                base.ScreenId = "134_0";//can be Alpha Numeric 
                //base.ScreenId			= "192_0";//can be Alpha Numeric 
                hidDEFAULT_COUNTRY.Value = "0";


            }
            Page.DataBind();
            if (Request.QueryString["BACK_TO_APPLICATION"] != null && Request.QueryString["BACK_TO_APPLICATION"].ToString() != "")
                hidBackToApplication.Value = Request.QueryString["BACK_TO_APPLICATION"].ToString();

            //strCalledFrom			= Request.QueryString["CalledFrom"].ToString();

            //	btnReset.Attributes.Add("onclick","javascript:ResetForm('" + Page.Controls[0].ID + "');OnCustomerTypeChange();return false;");
            hlkREG_ID_ISSUE.Attributes.Add("OnClick", "fPopCalendar(document.CLT_CUSTOMER_LIST.txtREG_ID_ISSUE,document.CLT_CUSTOMER_LIST.txtREG_ID_ISSUE)"); //Javascript Implementation for Calender				
            hlkDATE_OF_BIRTH.Attributes.Add("OnClick", "fPopCalendar(document.CLT_CUSTOMER_LIST.txtDATE_OF_BIRTH,document.CLT_CUSTOMER_LIST.txtDATE_OF_BIRTH)");
            hidMessage.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1277");
            cpvREG_ID_ISSUE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1402");
            cpvREG_ID_ISSUE2.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1403");
            //  chkSCORE.Attributes.Add("onclick","javascript:chkScore_clicked();");
            btnReset.Attributes.Add("onclick", "javascript:return LoadSelf();");
            btnBack.Attributes.Add("onclick", "javascript:return DoBack();");
            btnCustomerAssistant.Attributes.Add("onclick", "javascript:return DoBackToAssistant();");
            btnGetInsuranceScore.Attributes.Add("onclick", "javascript:return CheckInsuranceScore();");
            btnViewMap.Attributes.Add("onclick", "javascript:return ViewMap();");
            //hlkDATE_OF_BIRTH.Attributes.Add("onkeypress","fPopCalendar(document.CLT_CUSTOMER_LIST.txtDATE_OF_BIRTH,document.CLT_CUSTOMER_LIST.txtDATE_OF_BIRTH)");
            //cmbAltCUSTOMER_COUNTRY.Attributes.Add("OnChange","javascript:return selectState();");
            btnActivateDeactivate.Attributes.Add("onclick", "javascript:document.CLT_CUSTOMER_LIST.reset();");
            //-- Added by Mohit on 4/11/2005
            //cmbAPPLICANT_OCCU.Attributes.Add("OnChange","javascript: return Check();");  
            // ---End
            javasciptmsg = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "17");
            javasciptCPFmsg = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "21");
            javasciptCNPJmsg = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "22");
            CPF_invalid = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "23");
            CNPJ_invalid = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "24");

           // txtMONTHLY_INCOME.Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value);ValidatorOnChange();");

            btnSave.Attributes.Add("onclick", "javascript:DisableZipForCanada();EmpDetails_DisableZipForCanada();Page_ClientValidate();");//return callValidar();"); //return Validar(document.getElementById('txtCPF_CNPJ'));return Validar(document.getElementById('txtMAIN_CPF_CNPJ'));"); //Added on 15 Oct 2008 : To validate without Blur Text BOX-Added by Sibin
            //return Validar(document.getElementById('txtCPF_CNPJ'));return Validar(document.getElementById('txtMAIN_CPF_CNPJ'));
            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
            btnBack.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Read;
            btnBack.PermissionString = gstrSecurityXML;

            //btnCustomerAssistant.CmsButtonClass					=	Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnCustomerAssistant.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Read;
            btnCustomerAssistant.PermissionString = gstrSecurityXML;

            btnAddNewQuickQuote.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnAddNewQuickQuote.PermissionString = gstrSecurityXML;

            btnAddNewApplication.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnAddNewApplication.PermissionString = gstrSecurityXML;

            btnAddNewApplication.Attributes.Add("onclick", "javascript:return GoToNewApplication();");
            btnAddNewQuickQuote.Attributes.Add("onclick", "javascript:return GoToNewQuote();");

            //btnCustomerPullAddress.CmsButtonClass				=	Cms.CmsWeb.Controls.CmsButtonType.Write;
            //btnCustomerPullAddress.PermissionString				=	gstrSecurityXML;

            btnCopyClient.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnCopyClient.PermissionString = gstrSecurityXML;
            btnCopyClient.Enabled = false;

            //btnCustomerPullAddress.Attributes.Add("onClick","javascript:pullCustomerAddress();return false;");

            btnReset.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;
            btnActivateDeactivate.Enabled = false;
            btnGetInsuranceScore.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnGetInsuranceScore.PermissionString = gstrSecurityXML;

            btnViewMap.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Read;
            btnViewMap.PermissionString = gstrSecurityXML;

            btnCopyCustomerAddress.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnCopyCustomerAddress.PermissionString = gstrSecurityXML;

            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            SetCultureThread(GetLanguageCode());
            objResourceMgr = new System.Resources.ResourceManager("Cms.Client.Aspx.AddCustomer", System.Reflection.Assembly.GetExecutingAssembly());

            customerName = objResourceMgr.GetString("txtCUSTOMER_FIRST_NAME");
            FirstName = objResourceMgr.GetString("txtMAIN_FIRST_NAME");
            //imgSelect.Attributes.Add("onclick","javascript:OpenLookupWindow('" + url + "','CUSTOMER_ID','Name','hidCUSTOMER_PARENT','txtCUSTOMER_PARENT_TEXT','CustLookupForm','Customer Parent')");

            //iCustomerID = Convert.ToInt32(GetCustomerID());
            /*if (chkSCORE.Checked)
            {
                txtCUSTOMER_INSURANCE_SCORE.Text="-1";
                txtCUSTOMER_INSURANCE_SCORE.Visible=false;
            }
            else
            {
                txtCUSTOMER_INSURANCE_SCORE.Text="";
                txtCUSTOMER_INSURANCE_SCORE.Visible=true;
            }*/

            if (Request.Form["__EVENTTARGET"] == "CountryChanged")
            {
                //Commented by Sibin on 16 Oct 08

                //txtLOSS_DATE_TextChanged(null,null);						
                //cmbCUSTOMER_COUNTRY_Changed();
                //return;
            }

            if (Request.Form["__EVENTTARGET"] == "EmpCountryChanged")
            {
                //Commented by Sibin on 16 Oct 08

                //cmbEMPLOYER_COUNTRY_Changed(); 
                //return;
            }

           



            #region If form is not posted back then setting the default values
            if (!Page.IsPostBack)
            {
                try
                {
                    imgZipLookup.Attributes.Add("style", "cursor:hand");
                    //Added  by pradeep Kushwaha on 01-06-2010

                    //if (hidZipCodeCalledfor.Value !="" && hidZipCodeCalledfor.Value == "CUSTOMER_ZIP")
                    //{
                    //base.VerifyAddressDetailsBR(hlkZipLookup, txtCUSTOMER_ADDRESS1, txtCUSTOMER_ADDRESS2
                    //    , txtDISTRICT, txtCUSTOMER_CITY, cmbCUSTOMER_STATE, txtCUSTOMER_ZIP);
                    //}
                    //else if (hidZipCodeCalledfor.Value != "" && hidZipCodeCalledfor.Value == "MAIN_ZIPCODE")
                    //{
                    //    base.VerifyAddressDetailsBR(hypMAIN_ZIPCODE, txtMAIN_ADDRESS, txtMAIN_COMPLIMENT
                    //        , txtMAIN_DISTRICT, txtMAIN_CITY, cmbMAIN_STATE, txtMAIN_ZIPCODE);
                    //}

                    //cmbCUSTOMER_COUNTRY.Attributes.Add("onChange","javascript:validatorControlsfail();");  Commented by Sibin on 16 Oct 08



                    //imgEmpZipLookup.Attributes.Add("style","cursor:hand");
                    //base.VerifyAddress(hlkEmpZipLookup, txtEMPLOYER_ADD1,txtEMPLOYER_ADD2
                    //    , txtEMPLOYER_CITY, cmbEMPLOYER_STATE, txtEMPLOYER_ZIPCODE);


                    //Added by Sibin on 16 Oct 08 for fetching state for new customer

                    FetchCountryState(hidOldData.Value);
                    FetchEmpCountryState(hidOldData.Value);
                    //Setting the Page Captions
                    SetCaptions();
                    //FillAgencyDropdown();
                    //GetInsuranceScorePeriod();
                    BindamountType();
                    BindaccountType();
                    FillDropdowns();

                    

                    hidTabInsScore.Value = "0";

                    //txtMONTHLY_INCOME.Attributes.Add("onBlur", "this.value=formatAmount(this.value);SetChargeableField();"); 
                    txtCUSTOMER_INSURANCE_SCORE.Attributes.Add("onBlur", "javascript:InsuranceScoreChange();return false;");
                    txtMONTHLY_INCOME.Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value);ValidatorOnChange();");
                    cmbCUSTOMER_TYPE.Attributes.Add("onChange", "javascript:FillTitles();OnCustomerTypeChange();");
                    txtNET_ASSETS_AMOUNT.Attributes.Add("onblur", "javascript:this.value=formatAmount(this.value);ValidatorOnChange();");
                    txtCUSTOMER_FIRST_NAME.Attributes.Add("onBlur", "javascript:GenerateCustomerCode('txtCUSTOMER_FIRST_NAME','txtCUSTOMER_LAST_NAME','txtCUSTOMER_CODE','rfvCUSTOMER_CODE');");
                    //GenerateCustomerCode("txtCUSTOMER_FIRST_NAME", "txtCUSTOMER_LAST_NAME", "txtCUSTOMER_CODE", "rfvCUSTOMER_CODE");

                    txtCUSTOMER_LAST_NAME.Attributes.Add("onBlur", "javascript:GenerateCustomerCode('txtCUSTOMER_FIRST_NAME','txtCUSTOMER_LAST_NAME','txtCUSTOMER_CODE','rfvCUSTOMER_CODE');");
                    //txtMAIN_FIRST_NAME.Attributes.Add("onBlur", "javascript:GenerateCustomerCode('txtMAIN_FIRST_NAME','txtMAIN_LAST_NAME','txtMAIN_CONTACT_CODE','rfvMAIN_CONTACT_CODE');");
                    //txtMAIN_LAST_NAME.Attributes.Add("onBlur", "javascript:Main_Contact_code('txtMAIN_FIRST_NAME','txtMAIN_LAST_NAME','txtMAIN_CONTACT_CODE','rfvMAIN_CONTACT_CODE');");
                    txtMAIN_LAST_NAME.Attributes.Add("onBlur", "javascript:Main_Contact_code('txtMAIN_FIRST_NAME','txtMAIN_LAST_NAME','txtMAIN_CONTACT_CODE');");
                    hlkCUSTOMER_INSURANCE_RECEIVED_DATE.Attributes.Add("OnClick", "fPopCalendar(document.CLT_CUSTOMER_LIST.txtCUSTOMER_INSURANCE_RECEIVED_DATE, document.CLT_CUSTOMER_LIST.txtCUSTOMER_INSURANCE_RECEIVED_DATE)");

                    //Initializing the validators and other controls
                    SetPageLabels();


                    //Filling all the customer id related to any Agency id 

                    string strCarrierSystemID = CarrierSystemID;//System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString();
                    //if ( strSystemID.Trim().ToUpper()	!=	strCarrierSystemID.Trim().ToUpper())
                    //{


                    string strSystemID = GetSystemId();
                    int strAgencyID = ClsAgency.GetAgencyIDFromCode(strSystemID);

                    ClsCustomer objCustomer = new ClsCustomer();

                    string url = ClsCommon.GetLookupURL();
                    string sWHERECLAUSE = "@CUSTOMER_AGENCY_ID=" + strAgencyID.ToString();
                    imgSelect.Attributes.Add("onkeypress", "javascript:OpenLookup('" + url + "','CUSTOMER_ID','Name','hidCUSTOMER_PARENT','txtCUSTOMER_PARENT_TEXT','Customer','" + REL + "'," + "'" + sWHERECLAUSE + "');return false;");




                    //imgSelect.Attributes.Add("onsubmit", "javascript:OpenLookup('" + url + "','CUSTOMER_ID','Name','hidCUSTOMER_PARENT','txtCUSTOMER_PARENT_TEXT','Customer','Customer'," + "'" + sWHERECLAUSE + "');");
                    //imgSelect.Attributes.Add("onblur", "javascript:OpenLookup('" + url + "','CUSTOMER_ID','Name','hidCUSTOMER_PARENT','txtCUSTOMER_PARENT_TEXT','Customer','Customer'," + "'" + sWHERECLAUSE + "');");

                    //imgSelect.Attributes.Add("ongetfocus", "javascript:OpenLookup('" + url + "','CUSTOMER_ID','Name','hidCUSTOMER_PARENT','txtCUSTOMER_PARENT_TEXT','Customer','Customer'," + "'" + sWHERECLAUSE + "');");
                    //imgSelect.Attributes.Add("onkeydown", "javascript:postredraw();");
                    imgSelect.Attributes.Add("onclick", "javascript:OpenLookup('" + url + "','CUSTOMER_ID','Name','hidCUSTOMER_PARENT','txtCUSTOMER_PARENT_TEXT','Customer','"+ REL +"'," + "'" + sWHERECLAUSE + "');return false;");
                    if (GetSystemId().ToUpper() == "S001" || GetSystemId().ToUpper() == "SUAT")
                    {
                        imgBusinessType.Attributes.Add("onclick", "javascript:OpenLookup('" + url + "','BUSINESS_TYPE_ID','TYPE_DESCRIPTION','hidCUSTOMER_BUSINESS_TYPE','txtCUSTOMER_BUSINESS_TYPE_NAME','BusinessTypeSingapore','" + BT + "','');return false;");
                    }
                    else
                    {
                        imgBusinessType.Attributes.Add("onclick", "javascript:OpenLookup('" + url + "','ACTIVITY_ID','ACTIVITY_DESC','hidCUSTOMER_BUSINESS_TYPE','txtCUSTOMER_BUSINESS_TYPE_NAME','BusinessType','" + BT + "','');return false;");
                    }
                    hidCUSTOMER_ID.Value = Request.Params["CUSTOMER_ID"];
                    if (hidCUSTOMER_ID.Value == "")
                    {
                        //ClsCommon.SelectValueinDDL(cmbMAIN_COUNTRY, "5");
                        //ClsCommon.SelectValueinDDL(cmbCUSTOMER_COUNTRY, "5");

                        DataSet cntrydt = new DataSet();
                        cntrydt = AjaxFillState(cmbCUSTOMER_COUNTRY.SelectedValue.ToString());
                        cmbMAIN_STATE.DataSource = cntrydt.Tables[0];
                        cmbMAIN_STATE.DataValueField = STATE_ID;
                        cmbMAIN_STATE.DataTextField = STATE_NAME;
                        cmbMAIN_STATE.DataBind();

                        cmbCUSTOMER_STATE.DataSource = cntrydt.Tables[0];
                        cmbCUSTOMER_STATE.DataValueField = STATE_ID;
                        cmbCUSTOMER_STATE.DataTextField = STATE_NAME;
                        cmbCUSTOMER_STATE.DataBind();
                        // btnAddNewApplication.Attributes.Add("style", "display:none");

                    }

                    if (hidCUSTOMER_ID.Value != "")
                    {
                        btnAddNewApplication.Attributes.Add("style", "display:inline");
                        btnCustomerAssistant.Attributes.Add("style", "display:inline");
                    }
                    else
                    {
                        btnAddNewApplication.Attributes.Add("style", "display:none");
                        btnCustomerAssistant.Attributes.Add("style", "display:none");
                    }
                    //rfvCUSTOMER_ZIP.Attributes.Add("onkeypress","javascript:CountryDropDownRequiredField();");
                    //FillCombo();
                    string strSystemId = GetSystemId();
                    if (strSystemId.Trim().ToUpper() == "ALBAUAT")
                    {
                        strSystemId = "ALBA";
                    }

                    if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "/client/support/PageXML/" + strSystemId, "AddCustomer.xml"))
                        setPageControls(Page, Request.PhysicalApplicationPath + "/client/support/PageXml/" + strSystemId + "/AddCustomer.xml");

                    if (hidCUSTOMER_ID.Value == "")
                    {
                        btnGetInsuranceScore.Enabled = false;
                        //Added by Mohit Agarwal 8-Nov-2006
                        //txtCUSTOMER_INSURANCE_SCORE.ReadOnly=true;  
                        txtCUSTOMER_INSURANCE_RECEIVED_DATE.ReadOnly = true;
                        FillAgencyDropdown(); //Added by Charles on 17-Aug-09 for Cuatomer Page Optimization
                       // rfvDATE_OF_BIRTH.Visible = false;
                    }

                    else
                    {
                        btnGetInsuranceScore.Enabled = true;
                        //txtCUSTOMER_INSURANCE_SCORE.ReadOnly=false;  
                        txtCUSTOMER_INSURANCE_RECEIVED_DATE.ReadOnly = false;
                        LoadData();
                    }
                   
                   
                }
                catch (Exception objExcep)
                {
                    Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
                    this.lblMessage.Visible = true;
                    lblMessage.Text = objExcep.Message;

                    if (objExcep.InnerException != null)
                    {
                        lblMessage.Text = objExcep.InnerException.Message;
                        return;
                    }
                }
                strRowId = this.hidCUSTOMER_ID.Value;
                if
                    (cmbCUSTOMER_TYPE.SelectedValue == "11110")
                {
                    capCUSTOMER_FIRST_NAME.Text = objResourceMgr.GetString("txtMAIN_FIRST_NAME");

                }//Set Customer Name caption if Customer  Type is Personal
                else if (cmbCUSTOMER_TYPE.SelectedValue == "11109")
                {
                    //rfvREG_ID_ISSUE.Enabled = false;
                    capCUSTOMER_FIRST_NAME.Text = objResourceMgr.GetString("txtCUSTOMER_FIRST_NAME");
                }//Set Customer Name caption if Customer  Type is Commercial
            }

            //Created a temporary Session to indicate that a new customer has been created. 
            //This session will be set to null when the user visits other tabs like co-applicant and attention notes
            if (Request.QueryString["SaveMsg"] != null && Request.QueryString["SaveMsg"].ToString().Trim().ToUpper().Equals("INSERT") && Session["Insert"] != null && Session["Insert"].ToString() == "1") //Moved outside !Page.IsPostBack by Charles on 27-Oct-09 for Itrack 6525
            {
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("29");
                lblMessage.Visible = true;
                //if (rdYES.Checked == true)
                //{
                //    lblMessage1.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1884");
                //    lblMessage1.Visible = true;
                //}
                
            }

            //Commented by Charles on 13-Aug-09 for Customer Page Optimization, moved to LoadData()
            //ClsCustomer objCustomer1 = new  Cms.BusinessLayer.BlClient.ClsCustomer();
            //try
            //{
            //DataSet dsCust = objCustomer1.GetCustomerStatus(int.Parse(hidCUSTOMER_ID.Value), "Agency");
            //if(dsCust != null && dsCust.Tables[0].Rows.Count > 0)
            //		cmbCUSTOMER_AGENCY_ID.Enabled = false;
            //}
            //catch(Exception)
            //{
            //}
            //Commented till here
            #endregion

            //Creating and Setting the Customer XML

            //CreateTransXML();
            ServiceURL = System.Configuration.ConfigurationSettings.AppSettings["CmsWebUrl"].ToString();
            ServiceURL += "webservices/wscmsweb.asmx?WSDL";
            hidRefreshTabIndex.Value = "N";
           

        }

        private void FillDropdowns()
        {
            //Commented by Charles on 17-Aug-09 for Customer Page Optimization
            //FillAgencyDropdown(); 

            //Populating the country and state using the clsFetcher class
            //DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
            DataTable dt = Cms.CmsWeb.ClsFetcher.AllCountry;
            cmbCUSTOMER_COUNTRY.DataSource = dt;
            cmbCUSTOMER_COUNTRY.DataTextField = COUNTRY_NAME;
            cmbCUSTOMER_COUNTRY.DataValueField = COUNTRY_ID;
            cmbCUSTOMER_COUNTRY.DataBind();
            // cmbCUSTOMER_COUNTRY.SelectedItem.Text = "Brazil";
            cmbCUSTOMER_COUNTRY.SelectedIndex = cmbMAIN_COUNTRY.Items.IndexOf(cmbMAIN_COUNTRY.Items.FindByText("Brazil"));
            /*cmbCUSTOMER_COUNTRY.Items.Add("CANADA");
            cmbCUSTOMER_COUNTRY.Items.Add("ENGLAND");
            cmbCUSTOMER_COUNTRY.Items.Add("MEXICO");*/


            cmbMAIN_COUNTRY.DataSource = dt;
            cmbMAIN_COUNTRY.DataTextField = COUNTRY_NAME;
            cmbMAIN_COUNTRY.DataValueField = COUNTRY_ID;
            cmbMAIN_COUNTRY.DataBind();
            //cmbMAIN_COUNTRY.SelectedIndex = cmbMAIN_COUNTRY.Items.IndexOf(cmbMAIN_COUNTRY.Items.FindByText("Brazil"));


            //Populating the country field for Employer Details section
            cmbEMPLOYER_COUNTRY.DataSource = dt;
            cmbEMPLOYER_COUNTRY.DataTextField = COUNTRY_NAME;
            cmbEMPLOYER_COUNTRY.DataValueField = COUNTRY_ID;
            cmbEMPLOYER_COUNTRY.DataBind();



            //cmbAltCUSTOMER_COUNTRY.Items.Add("");
            //cmbAltCUSTOMER_COUNTRY.Items.Add("CANADA");
            //cmbAltCUSTOMER_COUNTRY.Items.Add("ENGLAND");
            //cmbAltCUSTOMER_COUNTRY.Items.Add("MEXICO");
            //cmbAltCUSTOMER_COUNTRY.Items.Add("USA");



            dt = Cms.CmsWeb.ClsFetcher.State;
            cmbCUSTOMER_STATE.DataSource = dt;
            cmbCUSTOMER_STATE.DataTextField = STATE_NAME;
            cmbCUSTOMER_STATE.DataValueField = STATE_ID;
            cmbCUSTOMER_STATE.DataBind();
            cmbCUSTOMER_STATE.Items.Insert(0, "");



            cmbMAIN_STATE.DataSource = dt;
            cmbMAIN_STATE.DataTextField = STATE_NAME;
            cmbMAIN_STATE.DataValueField = STATE_ID;
            cmbMAIN_STATE.DataBind();
            cmbMAIN_STATE.Items.Insert(0, "");

            //Populating the state field for Employer Details section
            cmbEMPLOYER_STATE.DataSource = dt;
            cmbEMPLOYER_STATE.DataTextField = STATE_NAME;
            cmbEMPLOYER_STATE.DataValueField = STATE_ID;
            cmbEMPLOYER_STATE.DataBind();
            cmbEMPLOYER_STATE.Items.Insert(0, "");

            //			cmbAltCUSTOMER_STATE.DataSource			= dt;
            //			cmbAltCUSTOMER_STATE.DataTextField			= STATE_NAME;
            //			cmbAltCUSTOMER_STATE.DataValueField		= STATE_ID;
            //			cmbAltCUSTOMER_STATE.DataBind();
            //			cmbAltCUSTOMER_STATE.Items.Insert(0,"");

            DataSet dsLookup = null;

            dsLookup = ClsCustomer.GetLookups();

            //Lookup for Reason
            //dt = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("RCFC");
            dt = dsLookup.Tables[0];
            DataView dv = dt.DefaultView;
            dv.Sort = "LOOKUP_VALUE_CODE";

            cmbCUSTOMER_REASON_CODE.DataSource = dv;
            cmbCUSTOMER_REASON_CODE.DataTextField = "LOOKUP_VALUE_CODE";
            cmbCUSTOMER_REASON_CODE.DataValueField = "LOOKUP_UNIQUE_ID";
            cmbCUSTOMER_REASON_CODE.DataBind();
            cmbCUSTOMER_REASON_CODE.Items.Insert(0, "");

            cmbCUSTOMER_REASON_CODE2.DataSource = dv;
            cmbCUSTOMER_REASON_CODE2.DataTextField = "LOOKUP_VALUE_CODE";
            cmbCUSTOMER_REASON_CODE2.DataValueField = "LOOKUP_UNIQUE_ID";
            cmbCUSTOMER_REASON_CODE2.DataBind();
            cmbCUSTOMER_REASON_CODE2.Items.Insert(0, "");

            cmbCUSTOMER_REASON_CODE3.DataSource = dv;
            cmbCUSTOMER_REASON_CODE3.DataTextField = "LOOKUP_VALUE_CODE";
            cmbCUSTOMER_REASON_CODE3.DataValueField = "LOOKUP_UNIQUE_ID";
            cmbCUSTOMER_REASON_CODE3.DataBind();
            cmbCUSTOMER_REASON_CODE3.Items.Insert(0, "");

            cmbCUSTOMER_REASON_CODE4.DataSource = dv;
            cmbCUSTOMER_REASON_CODE4.DataTextField = "LOOKUP_VALUE_CODE";
            cmbCUSTOMER_REASON_CODE4.DataValueField = "LOOKUP_UNIQUE_ID";
            cmbCUSTOMER_REASON_CODE4.DataBind();
            cmbCUSTOMER_REASON_CODE4.Items.Insert(0, "");

            //Lookup for Salutation
            dt = dsLookup.Tables[1];
            DataView dv_pre = dt.DefaultView;


            //Commented by lalit

            //cmbPREFIX.DataSource = dv_pre;
            //cmbPREFIX.DataTextField = "LOOKUP_VALUE_DESC"; 
            //cmbPREFIX.DataValueField = "LOOKUP_UNIQUE_ID";
            //cmbPREFIX.DataBind();
            //cmbPREFIX.Items.Insert(0,"");
            //DataSet ds = AjaxFillTitles("P");



            //Added By Lalit
            //cmbMAIN_TITLE.DataSource = dv_pre;
            //cmbMAIN_TITLE.DataTextField = "LOOKUP_VALUE_DESC";
            //cmbMAIN_TITLE.DataValueField = "LOOKUP_UNIQUE_ID";
            //cmbMAIN_TITLE.DataBind();
            //cmbMAIN_TITLE.Items.Insert(0, "");


            //Bind Merital Status

            cmbMARITAL_STATUS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("MARST");
            cmbMARITAL_STATUS.DataTextField = "LookupDesc";
            cmbMARITAL_STATUS.DataValueField = "LookupCode";
            cmbMARITAL_STATUS.DataBind();
            cmbMARITAL_STATUS.Items.Insert(0, "");

            //Bind Gender

            cmbGENDER.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("Sex");
            cmbGENDER.DataTextField = "LookupDesc";
            cmbGENDER.DataValueField = "LookupCode";
            cmbGENDER.DataBind();
            cmbGENDER.Items.Insert(0, "");


            cmbAPPLICANT_OCCU.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("%OCC");
            cmbAPPLICANT_OCCU.DataTextField = "LookupDesc";
            cmbAPPLICANT_OCCU.DataValueField = "LookupID";
            cmbAPPLICANT_OCCU.DataBind();
            cmbAPPLICANT_OCCU.Items.Insert(0, "");

            //  Bind Position

            //cmbMAIN_POSITION.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("%OCC");
            //cmbMAIN_POSITION.DataTextField = "LookupDesc";
            //cmbMAIN_POSITION.DataValueField = "LookupID";
            //cmbMAIN_POSITION.DataBind();
            //cmbMAIN_POSITION.Items.Insert(0, "");

            //ClsCommon.BindLookupDDL(cmbPREFIX,"%SAL","");

            /*
            //Lookup for Business type
            dt = dsLookup.Tables[3];
            cmbCUSTOMER_BUSINESS_TYPE.DataSource = dt;
            cmbCUSTOMER_BUSINESS_TYPE.DataTextField="LOOKUP_VALUE_DESC"; 
            cmbCUSTOMER_BUSINESS_TYPE.DataValueField="LOOKUP_UNIQUE_ID";
            cmbCUSTOMER_BUSINESS_TYPE.DataBind();
            */

            //Adding the Blank Entery into the combo 	
            ListItem TempLst = new ListItem();
            TempLst.Text = "";
            TempLst.Value = "";
            cmbCUSTOMER_BUSINESS_TYPE.Items.Insert(0, TempLst);

            //Binding the customer type combo box
            dt = dsLookup.Tables[2].Select("", "LOOKUP_VALUE_DESC").CopyToDataTable<DataRow>();
            cmbCUSTOMER_TYPE.DataSource = dt;
            cmbCUSTOMER_TYPE.DataTextField = "LOOKUP_VALUE_DESC";
            cmbCUSTOMER_TYPE.DataValueField = "LOOKUP_UNIQUE_ID";
            cmbCUSTOMER_TYPE.DataBind();
            cmbCUSTOMER_TYPE.Items.Insert(0, "");
            cmbCUSTOMER_TYPE.SelectedIndex = -1;


        }

        private void GetOldData()
        {
            ClsCustomer objCustomer = new ClsCustomer();
            //int iCustomerID								=		Convert.ToInt32(Request.Params["CUSTOMER_ID"]);

            int iCustomerID = Convert.ToInt32(hidCUSTOMER_ID.Value);
           
            string strXML = "";

            DataSet dsCustomer = ClsCustomer.GetCustomerDetails(iCustomerID);

            DataTable dt = dsCustomer.Tables[0];

            strXML = ClsCommon.GetXMLEncoded(dt);
            //FetchCountryState(strXML);
            this.hidOldData.Value = strXML;

        }

        private void FetchCountryState(string strXML)
        {
            string strSelectedState = ClsCommon.FetchValueFromXML("CUSTOMER_STATE", strXML);
            string strSelectedCountry = ClsCommon.FetchValueFromXML("CUSTOMER_COUNTRY", strXML);
            if (strSelectedCountry != "" && strSelectedCountry != "0")
            {
                PopulateStateDropDown(int.Parse(strSelectedCountry));
            }
            else //Added by Sibin on 16 Oct 08
                PopulateStateDropDown(1);

        }

        private void FetchEmpCountryState(string strXML)
        {
            string strSelectedState = ClsCommon.FetchValueFromXML("EMPLOYER_STATE", strXML);
            string strSelectedCountry = ClsCommon.FetchValueFromXML("EMPLOYER_COUNTRY", strXML);
            if (strSelectedCountry != "" && strSelectedCountry != "0")
            {
                PopulateEmpStateDropDown(int.Parse(strSelectedCountry));
            }
            else
                PopulateEmpStateDropDown(1);


        }

        private void selectReasonCode(DropDownList cmbReason, string code)
        {
            int index;
            for (index = 1; index < cmbReason.Items.Count; index++)
            {
                if (Convert.ToInt32(cmbReason.Items[index].Text) == Convert.ToInt32(code))
                {
                    cmbReason.SelectedIndex = index;
                    return;
                }
            }
            for (index = 1; index < cmbReason.Items.Count; index++)
            {
                if (cmbReason.Items[index].Value.ToString() == code)
                {
                    cmbReason.SelectedIndex = index;
                    return;
                }
            }
        } // Added by Mohit Agarwal for reason code selection general issue: bug # 3548		

        private void LoadData()
        {
            ClsCustomer objCustomer = new ClsCustomer();
            //int iCustomerID								=		Convert.ToInt32(Request.Params["CUSTOMER_ID"]);

            int iCustomerID = Convert.ToInt32(hidCUSTOMER_ID.Value);
            string strXML = "";

            DataSet dsCustomer = ClsCustomer.GetCustomerDetails(iCustomerID);

            DataTable dt = dsCustomer.Tables[0];

            if (dt.Rows.Count > 0)
            {
                btnCopyClient.Enabled = true;
                btnActivateDeactivate.Enabled = true;
                if (dt.Rows[0]["IS_ACTIVE"].ToString() == "Y")
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText("Y");
                else
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText("N");
            }
           
            DataTable dtr = dsCustomer.Tables[2];
            if (dtr.Rows[0]["INDIVIDUAL_CONTACT_ID"].ToString() == "0" && dt.Rows[0]["CUSTOMER_TYPE"].ToString() != "11110" && dt.Rows[0]["IS_POLITICALLY_EXPOSED"].ToString().Trim() == "Y")
            {

                lblMessage1.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1884");
                lblMessage1.Visible = true;
            }
            else if (dt.Rows[0]["IS_POLITICALLY_EXPOSED"].ToString().Trim() == "N" || dt.Rows[0]["CUSTOMER_TYPE"].ToString() == "11110")
            {
                lblMessage1.Visible = false;
            }
                     
            //Allowing to copy irrespective of any check
            // ITrack 3450, 22-Jan-08
            //			objCustomer = new  Cms.BusinessLayer.BlClient.ClsCustomer();
            //			DataSet dsCust = objCustomer.GetCustomerStatus(int.Parse(hidCUSTOMER_ID.Value), "Customer");
            //			if(dsCust != null && dsCust.Tables[0].Rows.Count <= 0)
            //				btnCopyClient.Enabled = false;

            strXML = ClsCommon.GetXMLEncoded(dt);

            //FetchCountryState(strXML);
            ClsCommon.SelectValueinDDL(cmbCUSTOMER_COUNTRY, dt.Rows[0]["CUSTOMER_COUNTRY"]);
            if (cmbCUSTOMER_COUNTRY.SelectedValue != null && cmbCUSTOMER_COUNTRY.SelectedValue != "")
                FillStates(cmbCUSTOMER_STATE, int.Parse(cmbCUSTOMER_COUNTRY.SelectedValue));
            ClsCommon.SelectValueinDDL(cmbCUSTOMER_STATE, dt.Rows[0]["CUSTOMER_STATE"]);
            hidSTATE_ID.Value = dt.Rows[0]["CUSTOMER_STATE"].ToString();
            if (cmbCUSTOMER_STATE.SelectedValue != "")
            {
                //rfvCUSTOMER_STATE.Attributes.Add("style", "display:none");
                //rfvCUSTOMER_STATE.Attributes.Add("isvalid", "true");
                //rfvCUSTOMER_STATE.Attributes.Add("enabled", "false");

            }

            hidOldXML.Value = strXML;

            //Load data///////////////
            ClsCommon.SelectValueinDDL(cmbAPPLICANT_OCCU, dt.Rows[0]["APPLICANT_OCCU"]);
            ClsCommon.SelectValueinDDL(cmbMARITAL_STATUS, dt.Rows[0]["MARITAL_STATUS"]);


            ClsCommon.SelectValueinDDL(cmbCUSTOMER_TYPE, dt.Rows[0]["CUSTOMER_TYPE"]);
            //ClsCommon.SelectValueinDDL(cmbPREFIX, dt.Rows[0]["CUSTOMER_SUFFIX"]);

            if (dt.Rows[0]["CUSTOMER_TYPE"].ToString() == "11109" || dt.Rows[0]["CUSTOMER_TYPE"].ToString() == "14725")
            {
                //Commercial or government             

                if (dt.Rows[0]["CUSTOMER_FIRST_NAME"] != System.DBNull.Value)
                {
                    txtCUSTOMER_FIRST_NAME.Text = dt.Rows[0]["CUSTOMER_FIRST_NAME"].ToString();
                    capCUSTOMER_FIRST_NAME.Text = customerName;
                    tdF_NAME.ColSpan = int.Parse("3");
                    tdF_NAME.Style.Add("width", "100%");
                    tdM_NAME.Style.Add("display", "none");
                    tdL_NAME.Style.Add("display", "none");
                    txtCUSTOMER_FIRST_NAME.Attributes.Add("size", "65");
                    //rfvCUSTOMER_LAST_NAME.Attributes.Add("enabled", "false");
                    //rfvCUSTOMER_LAST_NAME.Attributes.Add("isValid", "true");
                    //rfvCUSTOMER_LAST_NAME.Attributes.Add("display", "none");

                    //rfvREG_ID_ISSUE.Enabled = false;
                    revREG_ID_ISSUE.Enabled = true;
                    //rfvREGIONAL_IDENTIFICATION.Enabled = false;
                    //rfvORIGINAL_ISSUE.Enabled = false;
                    //rfvMARITAL_STATUS.Enabled = false;
                    //rfvGENDER.Enabled = false;
                   // rfvDATE_OF_BIRTH.Enabled = false;
                    revCUSTOMER_ZIP.Enabled = false;
                    revMAIN_ZIPCODE.Enabled = false;
                    //rfvDATE_OF_BIRTH.Enabled = false;
                    cpvREG_ID_ISSUE.Enabled = false;                    
                  
                    //cpvREG_ID_ISSUE2.Enabled = false;
                    csvDATE_OF_BIRTH.Enabled = false;
                    //rfvREG_ID_ISSUE.Attributes.Add("enabled", "false");
                    //rfvORIGINAL_ISSUE.Attributes.Add("enabled", "false");
                    //rfvREGIONAL_IDENTIFICATION.Attributes.Add("enabled", "false");
                    //rfvMARITAL_STATUS.Attributes.Add("enabled", "false");
                    //rfvGENDER.Attributes.Add("enabled", "false");
                    //rfvDATE_OF_BIRTH.Attributes.Add("enabled", "false");
                    //spnCREATION_DATE.Style.Add("display", "none");
                    //spnREG_ID_ISSUE.Style.Add("display", "none");
                    //spnORIGINAL_ISSUE.Style.Add("display", "none");
                    //spnREGIONAL_IDENTIFICATION.Style.Add("display", "none");
                    //spnMARITAL_STATUS.Style.Add("display", "none");
                    //spnGENDER.Style.Add("display", "none");
                    //spnDATE_OF_BIRTH.Style.Add("display", "inline");
                    //capDATE_OF_BIRTH.Style.Add("display", "none");
                    revMONTHLY_INCOME.Style.Add("display", "none");
                    //txtDATE_OF_BIRTH.ReadOnly = true;
                    //revNET_ASSETS_AMOUNT.Enabled = true;
                    //cpv3REG_ID_ISSUE.Enabled = false;
                    txtCPF_CNPJ.MaxLength = 18;
                    txtMAIN_CPF_CNPJ.MaxLength = 14;
                    

                }

                if (rdYES.Checked == true)
                {
                    rfvCPF_CNPJ.Style.Add("display", "none");
                    spnCPF_CNPJ.Style.Add("display", "none");
                    txtCADEMP.Enabled = false;
                    

                }

                //if (rdYES.Checked == true && txtCPF_CNPJ.Text == "")
                //{
                //    txtCPF_CNPJ.Enabled = false;
                //}


                if (rdNO.Checked == true)
                {
                    txtCADEMP.Enabled = false;
                    txtCPF_CNPJ.Enabled = true;
                    rfvCPF_CNPJ.Style.Add("display", "inline");
                    spnCPF_CNPJ.Style.Add("display", "inline");
                    txtCADEMP.Text = "";
                }

                if (dt.Rows[0]["CUSTOMER_TYPE"].ToString() == "11109")
                {
                    revDATE_OF_BIRTH.ValidationExpression = aRegExpDate.Replace("19|20", "18|19|20");
                }
                
                FillTitles(cmbPREFIX, "11109");
                FillTitles(cmbMAIN_TITLE, "11110");
                FillTitles(cmbMAIN_POSITION, "11109");
                if (dt.Rows[0]["DATE_OF_BIRTH"] != System.DBNull.Value)
                {
                    DateTime DOB = Convert.ToDateTime(dt.Rows[0]["DATE_OF_BIRTH"]);
                    this.txtDATE_OF_BIRTH.Text = DOB.ToShortDateString();
                }


            }
            else if (dt.Rows[0]["CUSTOMER_TYPE"].ToString() == "11110")
            {
                //For Personal

                txtCUSTOMER_FIRST_NAME.Text = dt.Rows[0]["CUSTOMER_FIRST_NAME"].ToString();
                capCUSTOMER_FIRST_NAME.Text = FirstName;
                txtCUSTOMER_MIDDLE_NAME.Text = dt.Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString();
                tdF_NAME.ColSpan = int.Parse("1");
                tdF_NAME.Style.Add("width", "33%");
                tdM_NAME.Style.Add("width", "33%");
                tdL_NAME.Style.Add("width", "33%");
                tdM_NAME.Style.Add("display", "inline");
                tdL_NAME.Style.Add("display", "inline");
                capDATE_OF_BIRTH.Style.Add("display", "inline");
                txtCUSTOMER_FIRST_NAME.Attributes.Add("size", "65");
                //rfvCUSTOMER_LAST_NAME.Attributes.Add("enabled", "true");
                //rfvCUSTOMER_LAST_NAME.Attributes.Add("isValid", "false");
                //rfvCUSTOMER_LAST_NAME.Attributes.Add("display", "inline");
                txtCPF_CNPJ.MaxLength = 14;
                txtMAIN_CPF_CNPJ.MaxLength = 14;
                //spnREG_ID_ISSUE.Visible = true;
                //spnORIGINAL_ISSUE.Visible = true;
                //spnREGIONAL_IDENTIFICATION.Visible = true;
                //capCREATION_DATE.Style.Add("display", "none");
                revCUSTOMER_ZIP.Enabled = false;
                revMAIN_ZIPCODE.Enabled = false;
                //spnREG_ID_ISSUE.Style.Add("display", "inline");
                //spnORIGINAL_ISSUE.Style.Add("display", "inline");
                //spnREGIONAL_IDENTIFICATION.Style.Add("display", "inline");
                //spnMARITAL_STATUS.Style.Add("display", "inline");
                //spnGENDER.Style.Add("display", "inline");
                //spnDATE_OF_BIRTH.Style.Add("display", "inline");
               // capCREATION_DATE.Style.Add("display", "inline");
                //spnCREATION_DATE.Style.Add("display", "none");
                //rfvREG_ID_ISSUE.Enabled = true;
                revREG_ID_ISSUE.Enabled = true;
                spnCPF_CNPJ.Style.Add("display", "inline");
                //rfvREGIONAL_IDENTIFICATION.Enabled = true;
                //rfvORIGINAL_ISSUE.Enabled = true;
                //rfvMARITAL_STATUS.Enabled = true;
                //rfvGENDER.Enabled = true;

                //cpvREG_ID_ISSUE2.Enabled = true;
                //revMONTHLY_INCOME.Enabled = true;
                revCUSTOMER_ZIP.Enabled = true;
                revMAIN_ZIPCODE.Enabled = true;
                //rfv2DATE_OF_BIRTH.Enabled = false;
                csvCREATION_DATE.Enabled = false;
                revNET_ASSETS_AMOUNT.Style.Add("display", "none");
               // txtDATE_OF_BIRTH.ReadOnly = false;
                if (dt.Rows[0]["CUSTOMER_MIDDLE_NAME"] != System.DBNull.Value)
                {
                    txtCUSTOMER_MIDDLE_NAME.Text = dt.Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString();
                }

                if (dt.Rows[0]["CUSTOMER_LAST_NAME"] != System.DBNull.Value)
                {
                    txtCUSTOMER_LAST_NAME.Text = dt.Rows[0]["CUSTOMER_LAST_NAME"].ToString();

                }
                FillTitles(cmbPREFIX, "11110");
                FillTitles(cmbMAIN_TITLE, "11110");
                FillTitles(cmbMAIN_POSITION, "11110");
                if (dt.Rows[0]["DATE_OF_BIRTH"] != System.DBNull.Value)
                {
                    DateTime DOB = Convert.ToDateTime(dt.Rows[0]["DATE_OF_BIRTH"]);
                    this.txtDATE_OF_BIRTH.Text = DOB.ToShortDateString();
                }
            }


            if (dt.Rows[0]["PER_CUST_MOBILE"] != System.DBNull.Value)
            {
                txtPER_CUST_MOBILE.Text = dt.Rows[0]["PER_CUST_MOBILE"].ToString();
            }

            if (dt.Rows[0]["CUSTOMER_ADDRESS2"] != System.DBNull.Value)
            {
                txtCUSTOMER_ADDRESS2.Text = dt.Rows[0]["CUSTOMER_ADDRESS2"].ToString();
            }

            if (dt.Rows[0]["CUSTOMER_CITY"] != System.DBNull.Value)
            {
                txtCUSTOMER_CITY.Text = dt.Rows[0]["CUSTOMER_CITY"].ToString();
            }

            if (dt.Rows[0]["CUSTOMER_CONTACT_NAME"] != System.DBNull.Value)
            {
                txtCUSTOMER_CONTACT_NAME.Text = dt.Rows[0]["CUSTOMER_CONTACT_NAME"].ToString();
            }

            if (dt.Rows[0]["CUSTOMER_PAGER_NO"] != System.DBNull.Value)
            {
                txtCUSTOMER_PAGER_NO.Text = dt.Rows[0]["CUSTOMER_PAGER_NO"].ToString();
            }

            if (dt.Rows[0]["CUSTOMER_WEBSITE"] != System.DBNull.Value)
            {
                txtCUSTOMER_WEBSITE.Text = dt.Rows[0]["CUSTOMER_WEBSITE"].ToString();
            }

            if (dt.Rows[0]["CUSTOMER_ADDRESS1"] != System.DBNull.Value)
            {
                txtCUSTOMER_ADDRESS1.Text = dt.Rows[0]["CUSTOMER_ADDRESS1"].ToString();
            }

            if (dt.Rows[0]["CUSTOMER_ZIP"] != System.DBNull.Value)
            {
                txtCUSTOMER_ZIP.Text = dt.Rows[0]["CUSTOMER_ZIP"].ToString();
            }


            if (dt.Rows[0]["CUSTOMER_BUSINESS_PHONE"] != System.DBNull.Value)
            {
                txtCUSTOMER_BUSINESS_PHONE.Text = dt.Rows[0]["CUSTOMER_BUSINESS_PHONE"].ToString();
            }

            if (dt.Rows[0]["CUSTOMER_EXT"] != System.DBNull.Value)
            {
                txtCUSTOMER_EXT.Text = dt.Rows[0]["CUSTOMER_EXT"].ToString();
            }

            if (dt.Rows[0]["EMP_EXT"] != System.DBNull.Value)
            {
                txtEMP_EXT.Text = dt.Rows[0]["EMP_EXT"].ToString();
            }

            if (dt.Rows[0]["CUSTOMER_HOME_PHONE"] != System.DBNull.Value)
            {
                txtCUSTOMER_HOME_PHONE.Text = dt.Rows[0]["CUSTOMER_HOME_PHONE"].ToString();
            }

            if (dt.Rows[0]["CUSTOMER_MOBILE"] != System.DBNull.Value)
            {
                txtCUSTOMER_MOBILE.Text = dt.Rows[0]["CUSTOMER_MOBILE"].ToString();
            }

            if (dt.Rows[0]["PER_CUST_MOBILE"] != System.DBNull.Value)
            {
                txtPER_CUST_MOBILE.Text = dt.Rows[0]["PER_CUST_MOBILE"].ToString();
            }

            if (dt.Rows[0]["CUSTOMER_FAX"] != System.DBNull.Value)
            {
                txtCUSTOMER_FAX.Text = dt.Rows[0]["CUSTOMER_FAX"].ToString();
            }

            if (dt.Rows[0]["CUSTOMER_Email"] != System.DBNull.Value)
            {
                txtCUSTOMER_Email.Text = dt.Rows[0]["CUSTOMER_Email"].ToString();
            }

            if (dt.Rows[0]["CUSTOMER_CODE"] != System.DBNull.Value)
            {
                txtCUSTOMER_CODE.Text = dt.Rows[0]["CUSTOMER_CODE"].ToString();
                txtCUSTOMER_CODE.Enabled = false;
            }

            if (dt.Rows[0]["CUSTOMER_INSURANCE_RECEIVED_DATE"] != System.DBNull.Value)
            {
                txtCUSTOMER_INSURANCE_RECEIVED_DATE.Text = Convert.ToDateTime(dt.Rows[0]["CUSTOMER_INSURANCE_RECEIVED_DATE"]).ToShortDateString();
            }

            if (dt.Rows[0]["CUSTOMER_INSURANCE_SCORE"] != System.DBNull.Value)
            {
                txtCUSTOMER_INSURANCE_SCORE.Text = dt.Rows[0]["CUSTOMER_INSURANCE_SCORE"].ToString();
                if (txtCUSTOMER_INSURANCE_SCORE.Text == "-1" || txtCUSTOMER_INSURANCE_SCORE.Text == "-2")
                {
                    if (txtCUSTOMER_INSURANCE_SCORE.Text == "-2")
                        chkSCORE.Checked = true;
                    txtCUSTOMER_INSURANCE_SCORE.Text = "";
                }
            }
            if (txtCUSTOMER_INSURANCE_SCORE.Text == "-2")
                chkSCORE.Checked = true;

            if (dt.Rows[0]["CUSTOMER_MIDDLE_NAME"] != System.DBNull.Value)
            {
                txtCUSTOMER_MIDDLE_NAME.Text = dt.Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString();
            }



            if (dt.Rows[0]["CUSTOMER_SUFFIX"] != System.DBNull.Value)
            {
                txtCUSTOMER_SUFFIX.Text = dt.Rows[0]["CUSTOMER_SUFFIX"].ToString();
            }

            if (dt.Rows[0]["CUSTOMER_BUSINESS_TYPE"] != System.DBNull.Value)
            {
                this.hidCUSTOMER_BUSINESS_TYPE.Value = dt.Rows[0]["CUSTOMER_BUSINESS_TYPE"].ToString();
            }

            if (dt.Rows[0]["CUSTOMER_BUSINESS_TYPE_NAME"] != System.DBNull.Value)
            {
                this.txtCUSTOMER_BUSINESS_TYPE_NAME.Text = dt.Rows[0]["CUSTOMER_BUSINESS_TYPE_NAME"].ToString();
            }

            if (dt.Rows[0]["CUSTOMER_PARENT_TEXT"] != System.DBNull.Value)
            {
                this.txtCUSTOMER_PARENT_TEXT.Text = dt.Rows[0]["CUSTOMER_PARENT_TEXT"].ToString();
            }

            if (dt.Rows[0]["CUSTOMER_PARENT"] != System.DBNull.Value)
            {
                hidCUSTOMER_PARENT.Value = dt.Rows[0]["CUSTOMER_PARENT"].ToString();
            }
            if (dt.Rows[0]["CUSTOMER_STATE_CODE"] != System.DBNull.Value)
            {
                hidState_Code.Value = dt.Rows[0]["CUSTOMER_STATE_CODE"].ToString();
            }



            if (dt.Rows[0]["SSN_NO"] != System.DBNull.Value)
            {
                hidSSN_NO.Value = dt.Rows[0]["SSN_NO"].ToString();
                //noder1.InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder1.InnerText);
                string strSSN_NO = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(hidSSN_NO.Value);
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

                this.txtSSN_NO.Text = "";
            }
            /*if ( dt.Rows[0]["EMPLOYER_ADDRESS"] != System.DBNull.Value )
            {
                this.txtEMPLOYER_ADDRESS.Text =  dt.Rows[0]["EMPLOYER_ADDRESS"].ToString();
            }*/

            if (dt.Rows[0]["EMPLOYER_ADD1"] != System.DBNull.Value)
            {
                this.txtEMPLOYER_ADD1.Text = dt.Rows[0]["EMPLOYER_ADD1"].ToString();
            }
            if (dt.Rows[0]["EMPLOYER_ADD2"] != System.DBNull.Value)
            {
                this.txtEMPLOYER_ADD2.Text = dt.Rows[0]["EMPLOYER_ADD2"].ToString();
            }
            if (dt.Rows[0]["EMPLOYER_CITY"] != System.DBNull.Value)
            {
                this.txtEMPLOYER_CITY.Text = dt.Rows[0]["EMPLOYER_CITY"].ToString();
            }
            if (dt.Rows[0]["EMPLOYER_ZIPCODE"] != System.DBNull.Value)
            {
                this.txtEMPLOYER_ZIPCODE.Text = dt.Rows[0]["EMPLOYER_ZIPCODE"].ToString();
            }
            if (dt.Rows[0]["EMPLOYER_HOMEPHONE"] != System.DBNull.Value)
            {
                this.txtEMPLOYER_HOMEPHONE.Text = dt.Rows[0]["EMPLOYER_HOMEPHONE"].ToString();
            }
            if (dt.Rows[0]["EMPLOYER_EMAIL"] != System.DBNull.Value)
            {
                this.txtEMPLOYER_EMAIL.Text = dt.Rows[0]["EMPLOYER_EMAIL"].ToString();
            }
            if (dt.Rows[0]["YEARS_WITH_CURR_OCCU"] != System.DBNull.Value)
            {
                this.txtYEARS_WITH_CURR_OCCU.Text = dt.Rows[0]["YEARS_WITH_CURR_OCCU"].ToString();
            }
            if (dt.Rows[0]["EMPLOYER_NAME"] != System.DBNull.Value)
            {
                this.txtEMPLOYER_NAME.Text = dt.Rows[0]["EMPLOYER_NAME"].ToString();
            }
            if (dt.Rows[0]["YEARS_WITH_CURR_EMPL"] != System.DBNull.Value)
            {
                this.txtYEARS_WITH_CURR_EMPL.Text = dt.Rows[0]["YEARS_WITH_CURR_EMPL"].ToString();
            }
            if (dt.Rows[0]["IS_ACTIVE"] != System.DBNull.Value)
            {
                this.hidIS_ACTIVE.Value = dt.Rows[0]["IS_ACTIVE"].ToString();
            }
            if (dt.Rows[0]["ID_TYPE"] != System.DBNull.Value)
            {
                this.txtID_TYPE.Text = dt.Rows[0]["ID_TYPE"].ToString();
            }

            //if (dt.Rows[0]["MONTHLY_INCOME"] != System.DBNull.Value)
            //{
            //    this.txtMONTHLY_INCOME.Text = dt.Rows[0]["MONTHLY_INCOME"].ToString();
            //}

            ClsCommon.SelectValueinDDL(cmbAMOUNT_TYPE, dt.Rows[0]["AMOUNT_TYPE"]);

            if (dt.Rows[0]["CADEMP"] != System.DBNull.Value)
            {
                this.txtCADEMP.Text = dt.Rows[0]["CADEMP"].ToString();
            }
           

            //if (dt.Rows[0]["NET_ASSETS_AMOUNT"] != System.DBNull.Value)
            //{
            //    this.txtNET_ASSETS_AMOUNT.Text = dt.Rows[0]["NET_ASSETS_AMOUNT"].ToString();
            //}

            if (dt.Rows[0]["NATIONALITY"] != System.DBNull.Value)
            {
                this.txtNATIONALITY.Text = dt.Rows[0]["NATIONALITY"].ToString();
            }

            if (dt.Rows[0]["EMAIL_ADDRESS"] != System.DBNull.Value)
            {
                this.txtEMAIL_ADDRESS.Text = dt.Rows[0]["EMAIL_ADDRESS"].ToString();
            }

            if (dt.Rows[0]["REGIONAL_IDENTIFICATION_TYPE"] != System.DBNull.Value)
            {
                this.txtREGIONAL_IDENTIFICATION_TYPE.Text = dt.Rows[0]["REGIONAL_IDENTIFICATION_TYPE"].ToString();
            }

            if (hidIS_ACTIVE.Value.ToUpper() == "Y")
            {
                lblIS_ACTIVE.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1277");//"Active";
                lblIS_ACTIVE.ForeColor = System.Drawing.Color.Black;

            }
            else
            {
                lblIS_ACTIVE.Text = "Inactive";
                lblIS_ACTIVE.ForeColor = System.Drawing.Color.Red;
            }

            if (dt.Rows[0]["CUSTOMER_BUSINESS_DESC"] != System.DBNull.Value)
            {
                this.txtCUSTOMER_BUSINESS_DESC.Text = dt.Rows[0]["CUSTOMER_BUSINESS_DESC"].ToString();
            }

            //----- Added by mohit on 4/11/2005
            if (dt.Rows[0]["DESC_APPLICANT_OCCU"] != System.DBNull.Value)
            {
                this.txtDESC_APPLICANT_OCCU.Text = dt.Rows[0]["DESC_APPLICANT_OCCU"].ToString();
            }
            if (dt.Rows[0]["PREFIX"] != System.DBNull.Value)
            {
                this.hidPREFIX.Value = dt.Rows[0]["PREFIX"].ToString();
            }

            txtACCOUNT_NUMBER.Text = dt.Rows[0]["ACCOUNT_NUMBER"].ToString();
            txtBANK_NAME.Text = dt.Rows[0]["BANK_NAME"].ToString();
            txtBANK_BRANCH.Text = dt.Rows[0]["BANK_BRANCH"].ToString();
            txtBANK_NUMBER.Text = dt.Rows[0]["BANK_NUMBER"].ToString();
            if (dt.Rows[0]["PREFIX"] != System.DBNull.Value)


            /*if ( dt.Rows[0]["ALT_CUSTOMER_ADDRESS1"] != System.DBNull.Value )
            {
                txtAltCUSTOMER_ADDRESS1.Text = dt.Rows[0]["ALT_CUSTOMER_ADDRESS1"].ToString();
            }

            if ( dt.Rows[0]["ALT_CUSTOMER_ADDRESS2"] != System.DBNull.Value )
            {
                txtAltCUSTOMER_ADDRESS2.Text = dt.Rows[0]["ALT_CUSTOMER_ADDRESS2"].ToString();
            }
			
            if ( dt.Rows[0]["ALT_CUSTOMER_CITY"] != System.DBNull.Value )
            {
                txtAltCUSTOMER_CITY.Text = dt.Rows[0]["ALT_CUSTOMER_CITY"].ToString();
            }
			
            if ( dt.Rows[0]["ALT_CUSTOMER_ZIP"] != System.DBNull.Value )
            {
                txtCUSTOMER_ZIP.Text =  dt.Rows[0]["ALT_CUSTOMER_ZIP"].ToString();
            }
			
                txtAltCUSTOMER_ZIP.Text =  dt.Rows[0]["ALT_CUSTOMER_ZIP"].ToString();*/



            //-----End 





            //ClsCommon.SelectValueinDDL(cmbCUSTOMER_STATE,dt.Rows[0]["CUSTOMER_STATE"]); //Commented by Sibin on 16 Oct 08 

            //Added by Sibin on 16 Oct 08 for setting customer state

            hidSTATE_ID_OLD.Value = dt.Rows[0]["CUSTOMER_STATE"].ToString();

            ClsCommon.SelectValueinDDL(cmbCUSTOMER_BUSINESS_TYPE, dt.Rows[0]["CUSTOMER_BUSINESS_TYPE"]);
            ClsCommon.SelectValueinDDL(cmbCUSTOMER_COUNTRY, dt.Rows[0]["CUSTOMER_COUNTRY"]);
            /*ClsCommon.SelectValueinDDL(cmbCUSTOMER_REASON_CODE,dt.Rows[0]["CUSTOMER_REASON_CODE"]);		
            //ClsCommon.SelectValueinDDL(cmbPREFIX,dt.Rows[0]["PREFIX"]);		
            ClsCommon.SelectValueinDDL(cmbCUSTOMER_REASON_CODE2,dt.Rows[0]["CUSTOMER_REASON_CODE2"]);		
            ClsCommon.SelectValueinDDL(cmbCUSTOMER_REASON_CODE3,dt.Rows[0]["CUSTOMER_REASON_CODE3"]);		
            ClsCommon.SelectValueinDDL(cmbCUSTOMER_REASON_CODE4,dt.Rows[0]["CUSTOMER_REASON_CODE4"]); */
            if (dt.Rows[0]["CUSTOMER_REASON_CODE"] != null && dt.Rows[0]["CUSTOMER_REASON_CODE"].ToString() != "")
                selectReasonCode(cmbCUSTOMER_REASON_CODE, dt.Rows[0]["CUSTOMER_REASON_CODE"].ToString());
            if (dt.Rows[0]["CUSTOMER_REASON_CODE2"] != null && dt.Rows[0]["CUSTOMER_REASON_CODE2"].ToString() != "")
                selectReasonCode(cmbCUSTOMER_REASON_CODE2, dt.Rows[0]["CUSTOMER_REASON_CODE2"].ToString());
            if (dt.Rows[0]["CUSTOMER_REASON_CODE3"] != null && dt.Rows[0]["CUSTOMER_REASON_CODE3"].ToString() != "")
                selectReasonCode(cmbCUSTOMER_REASON_CODE3, dt.Rows[0]["CUSTOMER_REASON_CODE3"].ToString());
            if (dt.Rows[0]["CUSTOMER_REASON_CODE4"] != null && dt.Rows[0]["CUSTOMER_REASON_CODE4"].ToString() != "")
                selectReasonCode(cmbCUSTOMER_REASON_CODE4, dt.Rows[0]["CUSTOMER_REASON_CODE4"].ToString());

            //			//Added by Charles on 13-Aug-09 for Customer Page Optimization
            string HasApplications = "N";

            if (dt.Rows[0]["HAS_APPLICATIONS"] != null && dt.Rows[0]["HAS_APPLICATIONS"] != DBNull.Value)
            {
                HasApplications = dt.Rows[0]["HAS_APPLICATIONS"].ToString();
            }
            //To set Agency Color
            if (dt.Rows[0]["IS_TERMINATED"].ToString() == "Y")
            {
                hidCustomer_AGENCY_ID.Value = dt.Rows[0]["CUSTOMER_AGENCY_ID"].ToString();
                lblCUSTOMER_AGENCY_ID.BackColor = Color.Lavender;
                lblCUSTOMER_AGENCY_ID.ForeColor = Color.Red;
            }
            if (HasApplications == "Y")
            {
                cmbCUSTOMER_AGENCY_ID.Enabled = false;

                string strAgencyName = dt.Rows[0]["AGENCY_NAME_ACTIVE_STATUS"].ToString();
                cmbCUSTOMER_AGENCY_ID.Items.Clear();
                cmbCUSTOMER_AGENCY_ID.Items.Add(new ListItem(strAgencyName, dt.Rows[0]["CUSTOMER_AGENCY_ID"].ToString()));
                cmbCUSTOMER_AGENCY_ID.SelectedIndex = 0;
                //To show label or combo
                if (GetSystemId().ToUpper() == CarrierSystemID.ToUpper())//System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToUpper())
                    hidCarrierId.Value = "1";
                else
                    hidCarrierId.Value = "0";
            }
            else
            {
                FillAgencyDropdown();
                ClsCommon.SelectValueinDDL(cmbCUSTOMER_AGENCY_ID, dt.Rows[0]["CUSTOMER_AGENCY_ID"]);
            }
            //Added till here

            ClsCommon.SelectValueinDDL(cmbEMPLOYER_COUNTRY, dt.Rows[0]["EMPLOYER_COUNTRY"]);

            //ClsCommon.SelectValueinDDL(cmbEMPLOYER_STATE,dt.Rows[0]["EMPLOYER_STATE"]);//Commented by Sibin on 16 Oct 08 for setting customer state

            //Added by on 16 Oct 08 for setting employer state

            hidEmpDetails_STATE_ID_OLD.Value = dt.Rows[0]["EMPLOYER_STATE"].ToString();

            ClsCommon.SelectValueinDDL(cmbGENDER, dt.Rows[0]["GENDER"]);

            //if(cmbAltCUSTOMER_COUNTRY.SelectedValue=="USA")
            //ClsCommon.SelectValueinDDL(cmbAltCUSTOMER_STATE,dt.Rows[0]["ALT_CUSTOMER_STATE"]);	
            //else
            //txtAltCustomer_State.Text = dt.Rows[0]["ALT_CUSTOMER_STATE"].ToString();
            //ClsCommon.SelectValueinDDL(cmbAltCUSTOMER_COUNTRY,dt.Rows[0]["ALT_CUSTOMER_COUNTRY"]);	


            //////////////////////////

            //string strXML								=		objCustomer.FillCustomerDetails(iCustomerID);


            //Setting the Customer ID into the session
            SetCustomerID(iCustomerID.ToString());

            GetInsuranceScorePeriod();

            //Disable enable Get Insurance score button accordingly.
            if (dt.Rows[0]["LAST_INSURANCE_SCORE_FETCHED"] != System.DBNull.Value)
            {
                DateTime dtLastScoreFetched = Convert.ToDateTime(dt.Rows[0]["LAST_INSURANCE_SCORE_FETCHED"]);

                //If the required months has elapsed from the last date on which score 
                //was fetched, enable button, else disable
                DateTime dtNewDate = dtLastScoreFetched.AddMonths(Convert.ToInt32(this.insurancePeriod));

                if (dtNewDate >= DateTime.Now)
                {
                    this.btnGetInsuranceScore.Enabled = false;
                }
                else
                {
                    this.btnGetInsuranceScore.Enabled = true;
                }
                if (chkSCORE.Checked != true)
                {
                    //lblSCORE.Visible=false;
                    lblCUSTOMER_INSURANCE_SCORE.Visible = true;
                    //lblCUSTOMER_INSURANCE_SCORE.Text="Insurance score is valid till "+ dtLastScoreFetched.AddYears(3).ToShortDateString() +". However, you can fetch next insurance score after "+ dtLastScoreFetched.AddYears(1).ToShortDateString();  
                    //Modified by Mohit Agarwal 8-Nov-2006
                    lblCUSTOMER_INSURANCE_SCORE.Text = "Insurance Score is valid until (" + dtLastScoreFetched.AddMonths(Convert.ToInt32(insurancePeriod)).ToShortDateString() + "). However you can update the Insurance Score after (" + dtLastScoreFetched.AddMonths(Convert.ToInt32(insuranceInterval)).ToShortDateString() + ")";
                }
                else
                {
                    lblSCORE.Visible = true;
                }
            }
            else
            {
                lblCUSTOMER_INSURANCE_SCORE.Visible = false;
            }

            // Added By Lalit March 12,2010
            // Fill The New Added Controls

            if (dt.Rows[0]["CPF_CNPJ"] != System.DBNull.Value)
            {
                this.txtCPF_CNPJ.Text = dt.Rows[0]["CPF_CNPJ"].ToString();
            }
            else
                txtCPF_CNPJ.Enabled = false;

            if (dt.Rows[0]["NUMBER"] != System.DBNull.Value)
            {
                this.txtNUMBER.Text = dt.Rows[0]["NUMBER"].ToString();
            }

            if (dt.Rows[0]["DISTRICT"] != System.DBNull.Value)
            {
                this.txtDISTRICT.Text = dt.Rows[0]["DISTRICT"].ToString();
            }
            if (dt.Rows[0]["MAIN_TITLE"] != System.DBNull.Value)
            {
                string MAIN_TITLE = dt.Rows[0]["MAIN_TITLE"].ToString();
                cmbMAIN_TITLE.SelectedIndex = cmbMAIN_TITLE.Items.IndexOf(cmbMAIN_TITLE.Items.FindByValue(MAIN_TITLE));
                //dt.Rows[0]["BROKER"].ToString();
            }

            if (dt.Rows[0]["MAIN_CPF_CNPJ"] != System.DBNull.Value)
            {
                this.txtMAIN_CPF_CNPJ.Text = dt.Rows[0]["MAIN_CPF_CNPJ"].ToString();
            }
            if (dt.Rows[0]["MAIN_ADDRESS"] != System.DBNull.Value)
            {
                this.txtMAIN_ADDRESS.Text = dt.Rows[0]["MAIN_ADDRESS"].ToString();
            }
            if (dt.Rows[0]["MAIN_NUMBER"] != System.DBNull.Value)
            {
                this.txtMAIN_NUMBER.Text = dt.Rows[0]["MAIN_NUMBER"].ToString();
            }
            if (dt.Rows[0]["MAIN_COMPLIMENT"] != System.DBNull.Value)
            {
                this.txtMAIN_COMPLIMENT.Text = dt.Rows[0]["MAIN_COMPLIMENT"].ToString();
            }
            if (dt.Rows[0]["MAIN_DISTRICT"] != System.DBNull.Value)
            {
                this.txtMAIN_DISTRICT.Text = dt.Rows[0]["MAIN_DISTRICT"].ToString();
            }
            if (dt.Rows[0]["MAIN_NOTE"] != System.DBNull.Value)
            {
                this.txtMAIN_NOTE.Text = dt.Rows[0]["MAIN_NOTE"].ToString();
            }
            if (dt.Rows[0]["MAIN_CONTACT_CODE"] != System.DBNull.Value)
            {
                this.txtMAIN_CONTACT_CODE.Text = dt.Rows[0]["MAIN_CONTACT_CODE"].ToString();
            }
            if (dt.Rows[0]["REGIONAL_IDENTIFICATION"] != System.DBNull.Value)
            {
                this.txtREGIONAL_IDENTIFICATION.Text = dt.Rows[0]["REGIONAL_IDENTIFICATION"].ToString();
            }
            if (dt.Rows[0]["REG_ID_ISSUE"] != System.DBNull.Value)
            {
                DateTime REG_ID_ISSUE = DateTime.Parse(dt.Rows[0]["REG_ID_ISSUE"].ToString());
                txtREG_ID_ISSUE.Text = REG_ID_ISSUE.ToShortDateString();
            }
            if (dt.Rows[0]["ORIGINAL_ISSUE"] != System.DBNull.Value)
            {
                this.txtORIGINAL_ISSUE.Text = dt.Rows[0]["ORIGINAL_ISSUE"].ToString();
            }
            if (dt.Rows[0]["MAIN_CITY"] != System.DBNull.Value)
            {
                this.txtMAIN_CITY.Text = dt.Rows[0]["MAIN_CITY"].ToString();
            }
            if (dt.Rows[0]["MAIN_ZIPCODE"] != System.DBNull.Value)
            {
                this.txtMAIN_ZIPCODE.Text = dt.Rows[0]["MAIN_ZIPCODE"].ToString();
            }
            if (dt.Rows[0]["MAIN_FIRST_NAME"] != System.DBNull.Value)
            {
                this.txtMAIN_FIRST_NAME.Text = dt.Rows[0]["MAIN_FIRST_NAME"].ToString();

            }
            if (dt.Rows[0]["MAIN_MIDDLE_NAME"] != System.DBNull.Value)
            {
                this.txtMAIN_MIDDLE_NAME.Text = dt.Rows[0]["MAIN_MIDDLE_NAME"].ToString();

            }
            if (dt.Rows[0]["MAIN_LAST_NAME"] != System.DBNull.Value)
            {

                this.txtMAIN_LAST_NAME.Text = dt.Rows[0]["MAIN_LAST_NAME"].ToString();

            }
            cmbACCOUNT_TYPE.SelectedIndex = cmbACCOUNT_TYPE.Items.IndexOf(cmbACCOUNT_TYPE.Items.FindByValue(dt.Rows[0]["ACCOUNT_TYPE"].ToString()));

            ClsCommon.SelectValueinDDL(cmbMAIN_COUNTRY, dt.Rows[0]["MAIN_COUNTRY"]);
            if (cmbMAIN_COUNTRY.SelectedValue != "" && cmbMAIN_COUNTRY.SelectedValue != null)
                FillStates(cmbMAIN_STATE, int.Parse(cmbMAIN_COUNTRY.SelectedValue));

            ClsCommon.SelectValueinDDL(cmbMAIN_STATE, dt.Rows[0]["MAIN_STATE"]);

            //if (dt.Rows[0]["MAIN_TITLE"] != System.DBNull.Value)
            //{
            //    ClsCommon.SelectValueinDDL(cmbMAIN_TITLE, dt.Rows[0]["MAIN_TITLE"]);
            //} if (dt.Rows[0]["PREFIX"] != System.DBNull.Value)
            //{
            //    ClsCommon.SelectValueinDDL(cmbPREFIX, dt.Rows[0]["PREFIX"]);
            //}

            hidMAIN_TITLE.Value = dt.Rows[0]["MAIN_TITLE"].ToString();
            hidPREFIX.Value = dt.Rows[0]["PREFIX"].ToString();
            hidMAIN_POSITION.Value = dt.Rows[0]["MAIN_POSITION"].ToString();



            //Added By Pradeep Kushwaha on 09-07-2010

            if (hidMAIN_TITLE.Value != "" && hidMAIN_TITLE.Value != null)
            {
                cmbMAIN_TITLE.SelectedIndex = cmbMAIN_TITLE.Items.IndexOf(cmbMAIN_TITLE.Items.FindByValue(hidMAIN_TITLE.Value.ToString()));
            }
            if (hidPREFIX.Value != "" && hidPREFIX.Value != null)
            {
                cmbPREFIX.SelectedIndex = cmbPREFIX.Items.IndexOf(cmbPREFIX.Items.FindByValue(hidPREFIX.Value.ToString()));
            }
            if (hidMAIN_POSITION.Value != "" && hidMAIN_POSITION.Value != null)
            {
                cmbMAIN_POSITION.SelectedIndex = cmbMAIN_POSITION.Items.IndexOf(cmbMAIN_POSITION.Items.FindByValue(hidMAIN_POSITION.Value.ToString()));
            }
            NfiBaseCurrency.NumberDecimalDigits = 2;
            if (dt.Rows[0]["MONTHLY_INCOME"] != System.DBNull.Value)
            {
                txtMONTHLY_INCOME.Text =  Convert.ToDouble(dt.Rows[0]["MONTHLY_INCOME"]).ToString("N", NfiBaseCurrency); 
            }
            if (dt.Rows[0]["NET_ASSETS_AMOUNT"] != System.DBNull.Value)
            {
                txtNET_ASSETS_AMOUNT.Text = Convert.ToDouble(dt.Rows[0]["NET_ASSETS_AMOUNT"]).ToString("N", NfiBaseCurrency);
            }

            if(dt.Rows[0]["IS_POLITICALLY_EXPOSED"].ToString().Trim() == "Y")
            {
                rdYES.Checked = true;
                
            }

            if (dt.Rows[0]["IS_POLITICALLY_EXPOSED"].ToString().Trim() == "N")
            {
                rdNO.Checked = true;

            }
            cpvREG_ID_ISSUE.Enabled = false;

            if (cmbCUSTOMER_TYPE.SelectedValue != "11110" && rdYES.Checked == true)
            {
                rfvCPF_CNPJ.Enabled = false;
                rfvCPF_CNPJ.Style.Add("display", "none");
                spnCPF_CNPJ.Style.Add("display", "none");

            }

            if ((cmbCUSTOMER_TYPE.SelectedValue == "11109" || cmbCUSTOMER_TYPE.SelectedValue == "14725") && txtCPF_CNPJ.Text != "" && rdYES.Checked == true)
            {
                txtCADEMP.Text = "";
                rfvCPF_CNPJ.Style.Add("display", "none");
                spnCPF_CNPJ.Style.Add("display", "none");

            }

           
                        
        }  // Populates the form fields with values

        private void GetInsuranceScorePeriod()
        {
            ClsSystemParams objSystemParams = new ClsSystemParams();
            try
            {
                DataSet dsParams = objSystemParams.GetSystemParams();
                if (dsParams != null && dsParams.Tables.Count > 0 && dsParams.Tables[0].Rows.Count > 0)
                {
                    insurancePeriod = dsParams.Tables[0].Rows[0]["SYS_INSURANCE_SCORE_VALIDITY"].ToString();
                    insuranceInterval = dsParams.Tables[0].Rows[0]["SYS_INSURANCE_SCORE_FETCH_INTERVAL"].ToString();
                }
                //insurancePeriod=objSystemParams.GetSystemParamsIS(); 
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally { }

        }

        private void FillCombo()
        {
            cmbCUSTOMER_BUSINESS_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("%SIC");
            cmbCUSTOMER_BUSINESS_TYPE.DataTextField = "LookupDesc";
            cmbCUSTOMER_BUSINESS_TYPE.DataValueField = "LookupID";
            cmbCUSTOMER_BUSINESS_TYPE.DataBind();

            //Adding the Blank Entry into the combo 	
            ListItem TempLst = new ListItem();
            TempLst.Text = "";
            TempLst.Value = "";
            cmbCUSTOMER_BUSINESS_TYPE.Items.Insert(0, TempLst);

            //Binding the customer type combo box
            cmbCUSTOMER_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CUSTYPE");
            cmbCUSTOMER_TYPE.DataTextField = "LookupDesc";
            cmbCUSTOMER_TYPE.DataValueField = "LookupID";
            cmbCUSTOMER_TYPE.DataBind();
            cmbCUSTOMER_TYPE.Items.Insert(0, "");

        }

        private void cmbCUSTOMER_COUNTRY_Changed()
        {
            try
            {
                if (cmbCUSTOMER_COUNTRY.SelectedItem != null && cmbCUSTOMER_COUNTRY.SelectedItem.Value != "")
                {
                    hidFormSaved.Value = "5";
                    PopulateStateDropDown(int.Parse(cmbCUSTOMER_COUNTRY.SelectedItem.Value));
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

        private void cmbEMPLOYER_COUNTRY_Changed()
        {
            try
            {
                if (cmbEMPLOYER_COUNTRY.SelectedItem != null && cmbEMPLOYER_COUNTRY.SelectedItem.Value != "")
                {
                    hidFormSaved.Value = "5";
                    PopulateEmpStateDropDown(int.Parse(cmbEMPLOYER_COUNTRY.SelectedItem.Value));
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

        private void PopulateStateDropDown(int COUNTRY_ID)
        {
            ClsStates objStates = new ClsStates();

            //Added by Sibin on 16 Oct 08
            //DataSet dsStates;
            if (COUNTRY_ID == 0)
                return;
            //			else
            //			{
            //				dsStates = objStates.GetStatesCountry(COUNTRY_ID);
            //				//hidSTATE_COUNTRY_LIST.Value=ClsCommon.GetXMLEncoded(dsStates.Tables[0]);
            //				
            //			}
            cmbCUSTOMER_STATE.Items.Clear();

            DataTable dtStates = objStates.GetStatesForCountry(COUNTRY_ID);
            if (dtStates != null && dtStates.Rows.Count > 0)
            {
                cmbCUSTOMER_STATE.DataSource = dtStates;
                cmbCUSTOMER_STATE.DataTextField = STATE_NAME;
                cmbCUSTOMER_STATE.DataValueField = STATE_ID;
                cmbCUSTOMER_STATE.DataBind();
                cmbCUSTOMER_STATE.Items.Insert(0, "");
                if (hidFormSaved.Value == "5")
                    SetFocus("cmbCUSTOMER_COUNTRY");

            }
            if (COUNTRY_ID != 1)
            {
                //rfvCUSTOMER_ZIP.Enabled = rfvCUSTOMER_STATE.Enabled = revCUSTOMER_ZIP.Enabled = false;
                //imgZipLookup.Attributes.Add("style","display:none");
                //spnCUSTOMER_ZIP.Attributes.Add("style", "display:none");
                //spnCUSTOMER_STATE.Attributes.Add("style", "display:none");
            }
            else
            {
               // rfvCUSTOMER_ZIP.Enabled = rfvCUSTOMER_STATE.Enabled = revCUSTOMER_ZIP.Enabled = true;
                //imgZipLookup.Attributes.Add("style","display:inline");
                //spnCUSTOMER_ZIP.Attributes.Add("style", "display:inline");
                //spnCUSTOMER_STATE.Attributes.Add("style", "display:inline");
            }


        }
       


        private void PopulateEmpStateDropDown(int COUNTRY_ID)
        {

            ClsStates objStates = new ClsStates();

            //Added by Sibin on 16 Oct 08

            DataSet dsStates;
            if (COUNTRY_ID == 0)
                return;
            else
            {
                dsStates = objStates.GetStatesCountry(COUNTRY_ID);
                //hidEmpDetails_STATE_COUNTRY_LIST.Value=ClsCommon.GetXMLEncoded(dsStates.Tables[0]);


            }

            cmbEMPLOYER_STATE.Items.Clear();
            DataTable dtStates = objStates.GetStatesForCountry(COUNTRY_ID);
            if (dtStates != null && dtStates.Rows.Count > 0)
            {

                cmbEMPLOYER_STATE.DataSource = dtStates;
                cmbEMPLOYER_STATE.DataTextField = STATE_NAME;
                cmbEMPLOYER_STATE.DataValueField = STATE_ID;
                cmbEMPLOYER_STATE.DataBind();
                cmbEMPLOYER_STATE.Items.Insert(0, "");
                if (hidFormSaved.Value == "5")
                    SetFocus("cmbEMPLOYER_COUNTRY");

            }
            if (COUNTRY_ID != 1)
            {
                //rfvCUSTOMER_ZIP.Enabled = 
                //rfvEMPLOYER_STATE.Enabled = false;
                revEMPLOYER_ZIPCODE.Enabled = false;
                //imgZipLookup.Attributes.Add("style","display:none");
                //spnCUSTOMER_ZIP.Attributes.Add("style","display:none");
                //spnCUSTOMER_STATE.Attributes.Add("style","display:none");
            }
            else
            {
                //rfvCUSTOMER_ZIP.Enabled = 
                //rfvEMPLOYER_STATE.Enabled = true;
                revEMPLOYER_ZIPCODE.Enabled = true;
                //imgZipLookup.Attributes.Add("style","display:inline");
                //spnCUSTOMER_ZIP.Attributes.Add("style","display:inline");
                //spnCUSTOMER_STATE.Attributes.Add("style","display:inline");
            }
        }

        #region Save function
        private void SaveFormValue()
        {
            try
            {
                int intRetVal;	//For retreiving the return value of business class save function

                // Creating Business layer object to do processing
                objCustomer = new Cms.BusinessLayer.BlClient.ClsCustomer();
                objCustomer.TransactionLogRequired = true;

                //Creating the Model object for holding the New data
                Model.Client.ClsCustomerInfo objNewCustomer = new Cms.Model.Client.ClsCustomerInfo();

                //Creating the Model object for holding the Old data
                Model.Client.ClsCustomerInfo objOldCustomer = new Cms.Model.Client.ClsCustomerInfo();

                lblMessage.Visible = true;

                if (this.hidCUSTOMER_ID.Value.ToUpper() == "NEW")
                {
                    //Mapping feild and Lebel to maintain the transction log into the database.

                    objNewCustomer = this.getFormValue();

                    int CustID;
                    //Setting those values into the Model object which are not in the page
                    objNewCustomer.CustomerId = intCUSTOMER_ID;
                    objNewCustomer.CREATED_BY = int.Parse(GetUserId());
                    objNewCustomer.CREATED_DATETIME = DateTime.Now;
                    //if (txtCPF_CNPJ.Text == "" && txtCADEMP.Text == "" && rdYES.Checked == true)
                    //{
                    //    rfvCPF_CNPJ.Enabled = true;
                    //    rfvCPF_CNPJ.Visible = true;
                    //    return;
                    //}


                    //Commented By Lalit

                    //if(txtCUSTOMER_INSURANCE_RECEIVED_DATE.Text!="")
                    //{
                    //    objNewCustomer.CustomerInsuranceReceivedDate	=	Convert.ToDateTime(txtCUSTOMER_INSURANCE_RECEIVED_DATE.Text);
                    //}


                    //if(cmbCUSTOMER_TYPE.SelectedValue.ToString()=="11109")//Commercial Customer
                    //{
                    //    objNewCustomer.CustomerContactName			=		txtCUSTOMER_CONTACT_NAME.Text;
                    //    objNewCustomer.CustomerBusinessPhone		=		txtCUSTOMER_BUSINESS_PHONE.Text;
                    //    objNewCustomer.CustomerExt					=		txtCUSTOMER_EXT.Text;			
                    //    objNewCustomer.CustomerMobile				=		txtCUSTOMER_MOBILE.Text;
                    //    objNewCustomer.CustomerFax					=		txtCUSTOMER_FAX.Text;			    
                    //    objNewCustomer.CustomerWebsite				=		txtCUSTOMER_WEBSITE.Text;
                    //    objNewCustomer.CustomerPagerNo				=		txtCUSTOMER_PAGER_NO.Text;
                    //    //objNewCustomer.CustomerBusinessType		=		cmbCUSTOMER_BUSINESS_TYPE.SelectedValue.ToString();
                    //    objNewCustomer.CustomerBusinessType			=		this.hidCUSTOMER_BUSINESS_TYPE.Value;
                    //    if(txtCUSTOMER_BUSINESS_DESC.Text.Length>999)
                    //    {
                    //        objNewCustomer.CustomerBusinessDesc		=		txtCUSTOMER_BUSINESS_DESC.Text.Substring(0,999);
                    //    }
                    //    else
                    //        objNewCustomer.CustomerBusinessDesc		=		txtCUSTOMER_BUSINESS_DESC.Text;
                    //    objNewCustomer.EMPLOYER_ADD1				=		"";
                    //    objNewCustomer.EMPLOYER_ADD2				=		"";
                    //    objNewCustomer.EMPLOYER_CITY				=		"";
                    //    objNewCustomer.EMPLOYER_COUNTRY				=		"";
                    //    objNewCustomer.EMPLOYER_EMAIL				=		"";
                    //    objNewCustomer.EMPLOYER_HOMEPHONE			=		"";
                    //    objNewCustomer.EMPLOYER_ZIPCODE				=		"";
                    //    objNewCustomer.EMPLOYER_STATE				=		"";
                    //    objNewCustomer.EMPLOYER_NAME				=		"";
                    //    objNewCustomer.PER_CUST_MOBILE				=		"";
                    //    objNewCustomer.DATE_OF_BIRTH				=		Convert.ToDateTime(null); 
                    ////	objNewCustomer.YEARS_WITH_CURR_EMPL			=		Double.MinValue;
                    ////	objNewCustomer.YEARS_WITH_CURR_OCCU			=		Double.MinValue;
                    //}	
                    //else												//Personal Customer
                    //{

                    //    objNewCustomer.CustomerContactName			=		"";
                    //    objNewCustomer.CustomerBusinessPhone		=		"";
                    //    objNewCustomer.CustomerExt					=		"";			
                    //    objNewCustomer.CustomerMobile				=		"";
                    //    objNewCustomer.CustomerFax					=		"";			    
                    //    objNewCustomer.CustomerWebsite				=		"";
                    //    objNewCustomer.CustomerPagerNo				=		"";
                    //    //objNewCustomer.CustomerBusinessType		=		"-1";
                    //    objNewCustomer.CustomerBusinessType		=		"";
                    //    objNewCustomer.CustomerBusinessDesc		=		"";

                    //}
                    //if(chkSCORE.Checked==true)
                    //{
                    //    objNewCustomer.CustomerInsuranceScore = -2;
                    //}
                    //else
                    //{
                    //    if(txtCUSTOMER_INSURANCE_SCORE.Text !="")
                    //    {
                    //        objNewCustomer.CustomerInsuranceScore		=	Convert.ToDecimal(txtCUSTOMER_INSURANCE_SCORE.Text);
                    //    }
                    //}
                    //if (hidCUSTOMER_PARENT.Value !="")
                    //{
                    //    objNewCustomer.CustomerParent			= Convert.ToInt32(hidCUSTOMER_PARENT.Value);
                    //}
                    //else
                    //{
                    //    objNewCustomer.CustomerParent			= Convert.ToInt32(null);
                    //}


                    //Add New 
                    
                    intRetVal = objCustomer.Add(objNewCustomer, out CustID, FirstName);
                     if ((cmbCUSTOMER_TYPE.SelectedValue == "11109" || cmbCUSTOMER_TYPE.SelectedValue == "14725") && txtCPF_CNPJ.Text == "" && txtCADEMP.Text == "" && rdYES.Checked == true)
                       
                    {
                        txtCADEMP.Enabled = true;
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1912");// "Please enter CPF_CNPJ/CADEMP";
                        //lblMessage1.Visible = false;
                        rfvCPF_CNPJ.Style.Add("display", "none");
                        spnCPF_CNPJ.Style.Add("display", "none");
                        return;
                         
                    }

                    if (intRetVal > 0)			// Value inserted successfully.
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("29");
                        
                         hidFormSaved.Value = "1";
                        hidCUSTOMER_ID.Value = CustID.ToString();
                        SetCustomerID(hidCUSTOMER_ID.Value.ToString());
                        //lblIS_ACTIVE.Text = "Active";
                        lblIS_ACTIVE.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1277");
                        hidIS_ACTIVE.Value = "Y";
                        hidRefreshTabIndex.Value = "Y";
                        hidSaveMsg.Value = "Insert";
                        btnAddNewApplication.Attributes.Add("style", "display:inline");
                        btnCopyClient.Attributes.Add("style", "display:inline");
                        Session["Insert"] = "1";


                        if (rdYES.Checked == true)
                        {
                           // rfvREG_ID_ISSUE.Enabled = false;
                            //rfvREGIONAL_IDENTIFICATION.Enabled = false;
                           // rfvORIGINAL_ISSUE.Enabled = false;
                            revCUSTOMER_ZIP.Enabled = false;
                            revMAIN_ZIPCODE.Enabled = false;
                            
                            //lblMessage1.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1884");
                            
                           // revDATE_OF_BIRTH.ValidationExpression = aRegExpDate.Replace("19|20", "18|19|20");
                        }
                    }
                    else if (intRetVal == -1)	// Duplicate code exist, insert failed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("18");
                        hidFormSaved.Value = "2";
                        rfvCPF_CNPJ.Enabled = false;
                        rfvCPF_CNPJ.Visible = false;
                        spnCPF_CNPJ.Visible = false;
                        return;
                    }
                    else						// Error occured while processing, insert failed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }
                    lblMessage.Visible = true;
                    //lblMessage1.Enabled = true;
                    //lblMessage1.Visible = true;
                }
                //Update Customer
                else
                {
                    // Creating Business layer object to do processing
                    ClsCustomer objCustomer1 = new ClsCustomer();

                    //customer id for the record which is being updated
                    intCUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
                    SetCustomerID(hidCUSTOMER_ID.Value.ToString());

                    //Creating the Model object for holding the New data
                    Model.Client.ClsCustomerInfo objNewCustomer1 = getFormValue();
                    if ((cmbCUSTOMER_TYPE.SelectedValue == "11109" || cmbCUSTOMER_TYPE.SelectedValue == "14725") && txtCPF_CNPJ.Text == "" && txtCADEMP.Text == "" && rdYES.Checked == true)
                       
                    {
                        txtCADEMP.Enabled = true;
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1912");// "Please enter CPF_CNPJ/CADEMP";
                        //lblMessage1.Visible = false;
                        return;
                    }

                    //Creating the Model object for holding the Old data
                    Model.Client.ClsCustomerInfo objOldCustomer1 = new Cms.Model.Client.ClsCustomerInfo();

                    //Setting  the Old Page details(XML File containing old details) into the Model Object
                    base.PopulateModelObject(objOldCustomer1, hidOldXML.Value);

                    objNewCustomer1.MODIFIED_BY = int.Parse(GetUserId());
                    objNewCustomer1.LAST_UPDATED_DATETIME = DateTime.Now;
                    objNewCustomer1.IS_ACTIVE = hidIS_ACTIVE.Value;
                    SetCustomerID(intCUSTOMER_ID.ToString());
                    //Setting those values into the Model object which are not in the page
                    objOldCustomer1.CustomerId = intCUSTOMER_ID;
                    intRetVal = objCustomer1.Update(objOldCustomer1, objNewCustomer1, FirstName);

                    if (intRetVal > 0)			// update successfully performed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("31");
                        hidFormSaved.Value = "1";
                        hidRefreshTabIndex.Value = "Y";
                        hidSaveMsg.Value = "Update";
                        revCUSTOMER_ZIP.Enabled = false;
                        revMAIN_ZIPCODE.Enabled = false;
                        //lblMessage1.Visible = false;

                        
                        
                    }
                    else if (intRetVal == -1)	// Duplicate code exist, update failed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("18");
                        hidFormSaved.Value = "2";
                        return;
                    }

                    //else if (intRetVal == -2)	// Duplicate code exist, update failed
                    //{
                    //    if (rdYES.Checked == true)
                    //    {
                    //        lblMessage1.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1884");
                    //        lblMessage1.Visible = true;
                    //        lblMessage.Visible = false;
                    //        hidFormSaved.Value = "2";
                            
                    //        return;
                    //    }
                    //}
                    else						// Error occured while processing, update failed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }
                }
            }
            catch (Exception ex)
            {
                // Show exception message
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("21") + " " + ex.Message + " Try again!";
                lblMessage.Visible = true;

                //Publishing the exception using the static method of Exception manager class
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";
                return;
            }
            finally
            {
                //if (cmbCUSTOMER_TYPE.SelectedValue == "11109")
                //{
                //    rfvREG_ID_ISSUE.Enabled = false;
                //    rfvREGIONAL_IDENTIFICATION.Enabled = false;
                //    rfvORIGINAL_ISSUE.Enabled = false;
                //}
                if (objCustomer != null)
                {
                    objCustomer.Dispose();
                }
                
            }

            //CreateTransXML();
            LoadData();
        }  // saves the posted data into table using the business layer class (clsCustomer)

        /// <summary>
        /// Setting the page values in to the Customer object
        /// </summary>
        /// <returns></returns>
        private ClsCustomerInfo getFormValue()
        {
            Model.Client.ClsCustomerInfo objCustomerInfo = new Model.Client.ClsCustomerInfo();


            NfiBaseCurrency.NumberDecimalDigits = 2;
            objCustomerInfo.CustomerId = Convert.ToInt32(Request.Params["CUSTOMER_ID"]);
            objCustomerInfo.CustomerType = cmbCUSTOMER_TYPE.SelectedValue.ToString();

            //Added By Lalit


            if (hidCUSTOMER_PARENT.Value != "")
                objCustomerInfo.CustomerParent = Convert.ToInt32(hidCUSTOMER_PARENT.Value);
            else
                objCustomerInfo.CustomerParent = 0;

            //if (cmbPREFIX.SelectedIndex >= 0 && cmbPREFIX.SelectedValue != "")
            //    objCustomerInfo.PREFIX = int.Parse(cmbPREFIX.SelectedValue);
            //else
            //    objCustomerInfo.PREFIX = 0;

            if (hidPREFIX.Value != "")
                objCustomerInfo.PREFIX = int.Parse(hidPREFIX.Value);
            else
                objCustomerInfo.PREFIX = 0;
            //objCustomerInfo.MONTHLY_INCOME = txtMONTHLY_INCOME.Text == "" ? 0 : double.Parse(txtMONTHLY_INCOME.Text, numberFormatInfo);
            objCustomerInfo.CustomerFirstName = txtCUSTOMER_FIRST_NAME.Text;
            objCustomerInfo.CustomerMiddleName = txtCUSTOMER_MIDDLE_NAME.Text;
            objCustomerInfo.CustomerLastName = txtCUSTOMER_LAST_NAME.Text;
            objCustomerInfo.CustomerCode = txtCUSTOMER_CODE.Text.Trim();
            objCustomerInfo.CustomerZip = txtCUSTOMER_ZIP.Text;
            objCustomerInfo.CustomerAddress1 = txtCUSTOMER_ADDRESS1.Text;
            objCustomerInfo.NUMBER = txtNUMBER.Text;
            objCustomerInfo.CustomerAddress2 = txtCUSTOMER_ADDRESS2.Text;
            objCustomerInfo.DISTRICT = txtDISTRICT.Text;
            if (hidSTATE_ID.Value != "")
                objCustomerInfo.CustomerState = hidSTATE_ID.Value.Trim();

            objCustomerInfo.CustomerCity = txtCUSTOMER_CITY.Text;

            objCustomerInfo.CustomerCountry = cmbCUSTOMER_COUNTRY.SelectedValue.ToString();
            objCustomerInfo.CPF_CNPJ = txtCPF_CNPJ.Text;
            if(cmbCUSTOMER_AGENCY_ID.SelectedValue!="")
            objCustomerInfo.CustomerAgencyId = int.Parse(cmbCUSTOMER_AGENCY_ID.SelectedValue);
            objCustomerInfo.CustomerWebsite = txtCUSTOMER_WEBSITE.Text;
            if (this.cmbMARITAL_STATUS.Items.Count > 0)
            {
                objCustomerInfo.MARITAL_STATUS = this.cmbMARITAL_STATUS.SelectedItem.Value;
            }
            
            objCustomerInfo.CustomerBusinessType = hidCUSTOMER_BUSINESS_TYPE.Value; //txtCUSTOMER_BUSINESS_TYPE_NAME.Text.Trim();		//cmbCUSTOMER_BUSINESS_TYPE.SelectedValue.ToString(); 
            if (txtCUSTOMER_BUSINESS_DESC.Text.Length > 999)
                objCustomerInfo.CustomerBusinessDesc = txtCUSTOMER_BUSINESS_DESC.Text.Substring(0, 999);
            else
                objCustomerInfo.CustomerBusinessDesc = txtCUSTOMER_BUSINESS_DESC.Text;

            //MAIN Information

            //if (cmbMAIN_TITLE.SelectedIndex >= 0 && cmbMAIN_TITLE.SelectedValue != "")
            //    objCustomerInfo.MAIN_TITLE = Convert.ToInt32(cmbMAIN_TITLE.SelectedValue);
            //else
            //    objCustomerInfo.MAIN_TITLE = 0;

            if (hidMAIN_TITLE.Value != "")
                objCustomerInfo.MAIN_TITLE = int.Parse(hidMAIN_TITLE.Value);
            else
                objCustomerInfo.MAIN_TITLE = 0;

            objCustomerInfo.MAIN_FIRST_NAME = txtMAIN_FIRST_NAME.Text;
            objCustomerInfo.MAIN_MIDDLE_NAME = txtMAIN_MIDDLE_NAME.Text;
            objCustomerInfo.MAIN_LAST_NAME = txtMAIN_LAST_NAME.Text;

            objCustomerInfo.CustomerContactName = txtCUSTOMER_CONTACT_NAME.Text;
            objCustomerInfo.MAIN_CONTACT_CODE = txtMAIN_CONTACT_CODE.Text;
            objCustomerInfo.MAIN_ZIPCODE = txtMAIN_ZIPCODE.Text;
            objCustomerInfo.MAIN_ADDRESS = txtMAIN_ADDRESS.Text;
            objCustomerInfo.MAIN_NUMBER = txtMAIN_NUMBER.Text;
            objCustomerInfo.MAIN_COMPLIMENT = txtMAIN_COMPLIMENT.Text;
            objCustomerInfo.MAIN_DISTRICT = txtMAIN_DISTRICT.Text;
            objCustomerInfo.MAIN_CITY = txtMAIN_CITY.Text;
            if (cmbMAIN_STATE.SelectedValue != "")
            {
                objCustomerInfo.MAIN_STATE = Convert.ToInt32(cmbMAIN_STATE.SelectedValue);
            }
            objCustomerInfo.MAIN_COUNTRY = Convert.ToInt32(cmbMAIN_COUNTRY.SelectedValue);
            objCustomerInfo.MAIN_CPF_CNPJ = txtMAIN_CPF_CNPJ.Text;
            objCustomerInfo.CustomerHomePhone = txtCUSTOMER_HOME_PHONE.Text;
            objCustomerInfo.CustomerBusinessPhone = txtCUSTOMER_BUSINESS_PHONE.Text;
            objCustomerInfo.CustomerExt = txtCUSTOMER_EXT.Text;
            objCustomerInfo.CustomerMobile = txtCUSTOMER_MOBILE.Text;
            objCustomerInfo.CustomerFax = txtCUSTOMER_FAX.Text;
            objCustomerInfo.CustomerEmail = txtCUSTOMER_Email.Text;
            objCustomerInfo.REGIONAL_IDENTIFICATION = txtREGIONAL_IDENTIFICATION.Text;
            if (txtREG_ID_ISSUE.Text != "")
            {
                objCustomerInfo.REG_ID_ISSUE = Convert.ToDateTime(txtREG_ID_ISSUE.Text);
            }

            objCustomerInfo.ORIGINAL_ISSUE = txtORIGINAL_ISSUE.Text;

            if (this.txtDATE_OF_BIRTH.Text.Trim() != "")
            {
                objCustomerInfo.DATE_OF_BIRTH = Convert.ToDateTime(txtDATE_OF_BIRTH.Text.Trim());
            }
            if (this.cmbMARITAL_STATUS.Items.Count > 0)
            {
                objCustomerInfo.MARITAL_STATUS = this.cmbMARITAL_STATUS.SelectedItem.Value;
            }
            objCustomerInfo.GENDER = this.cmbGENDER.SelectedItem.Value;

            if (hidMAIN_POSITION.Value != "")
                objCustomerInfo.MAIN_POSITION = int.Parse(hidMAIN_POSITION.Value);
            else
                objCustomerInfo.MAIN_POSITION = 0;

            objCustomerInfo.MAIN_NOTE = txtMAIN_NOTE.Text;


           
                if (txtCADEMP.Text.Trim() != "")
                {
                    objCustomerInfo.CADEMP = txtCADEMP.Text.Trim();

                }
            
           

                if (txtID_TYPE.Text != "")
                {
                    objCustomerInfo.ID_TYPE = txtID_TYPE.Text;

                }
            
            if (cmbCUSTOMER_TYPE.SelectedValue == "11109")
            {
                objCustomerInfo.MONTHLY_INCOME = 0;
            }
            else
            {
                if (this.txtMONTHLY_INCOME.Text.Trim() != "")
                {

                    objCustomerInfo.MONTHLY_INCOME = Convert.ToDouble(txtMONTHLY_INCOME.Text, NfiBaseCurrency);
                }
            }
            if (cmbCUSTOMER_TYPE.SelectedValue == "11110")
            {
                objCustomerInfo.NET_ASSETS_AMOUNT = 0;
            }
            else
            {
                if (this.txtNET_ASSETS_AMOUNT.Text.Trim() != "")
                {
                    objCustomerInfo.NET_ASSETS_AMOUNT = Convert.ToDouble(txtNET_ASSETS_AMOUNT.Text, NfiBaseCurrency);
                }
            }

            if (this.txtNATIONALITY.Text.Trim() != "")
            {
                objCustomerInfo.NATIONALITY = txtNATIONALITY.Text.Trim();

            }

            if (this.txtEMAIL_ADDRESS.Text.Trim() != "")
            {
                objCustomerInfo.EMAIL_ADDRESS = txtEMAIL_ADDRESS.Text.Trim();
            }


            if (this.txtREGIONAL_IDENTIFICATION_TYPE.Text.Trim() != "")
            {
                objCustomerInfo.REGIONAL_IDENTIFICATION_TYPE = txtREGIONAL_IDENTIFICATION_TYPE.Text.Trim();
            }

           
                if (cmbAMOUNT_TYPE.SelectedValue != "")
                    objCustomerInfo.AMOUNT_TYPE = cmbAMOUNT_TYPE.SelectedItem.Value;


                if (rdYES.Checked == true)
            {
                objCustomerInfo.IS_POLITICALLY_EXPOSED = "Y";
            }
            else
            {
                objCustomerInfo.IS_POLITICALLY_EXPOSED = "N";
            }

            if (txtACCOUNT_NUMBER.Text.Trim() != "")
            {
                objCustomerInfo.ACCOUNT_NUMBER = txtACCOUNT_NUMBER.Text.Trim();

            }

            if (cmbACCOUNT_TYPE.SelectedValue != "")
            {
                objCustomerInfo.ACCOUNT_TYPE = int.Parse(cmbACCOUNT_TYPE.SelectedValue);
            }
            if (txtBANK_NAME.Text.Trim() != "")
            {
                objCustomerInfo.BANK_NAME = txtBANK_NAME.Text.Trim();

            }
            if (txtBANK_BRANCH.Text.Trim() != "")
            {
                objCustomerInfo.BANK_BRANCH = txtBANK_BRANCH.Text.Trim();

            }
            if (txtBANK_NUMBER.Text.Trim() != "")
            {
                objCustomerInfo.BANK_NUMBER = txtBANK_NUMBER.Text.Trim();

            }

           

            //End 





            //Comment By Lalit March 16,2010

            //if (hidCUSTOMER_PARENT.Value !="")
            //{
            //    objCustomerInfo.CustomerParent			= Convert.ToInt32(hidCUSTOMER_PARENT.Value);
            //}
            //else
            //{
            //    objCustomerInfo.CustomerParent			= Convert.ToInt32(null);
            //}

            ////modify by lalit according to new
            //objCustomerInfo.CustomerFirstName = txtCUSTOMER_FIRST_NAME.Text;
            //objCustomerInfo.CustomerCode = txtCUSTOMER_CODE.Text;
            //objCustomerInfo.CustomerAddress1 = txtCUSTOMER_ADDRESS1.Text;
            //objCustomerInfo.CustomerCity = txtCUSTOMER_CITY.Text;
            //objCustomerInfo.CustomerCountry = cmbCUSTOMER_COUNTRY.SelectedValue.ToString();


            //objCustomerInfo.CustomerSuffix				=		txtCUSTOMER_SUFFIX.Text;			
            //objCustomerInfo.CustomerMiddleName			=		txtCUSTOMER_MIDDLE_NAME.Text;
            //objCustomerInfo.CustomerLastName			=		txtCUSTOMER_LAST_NAME.Text;			
            //objCustomerInfo.CustomerAddress2			=		txtCUSTOMER_ADDRESS2.Text;



            ////16 OCT 2008-Commented by Sibin
            ////if(cmbCUSTOMER_STATE.SelectedItem!=null && cmbCUSTOMER_STATE.SelectedItem.Value!="")
            ////	objCustomerInfo.CustomerState				=		cmbCUSTOMER_STATE.SelectedItem.Value.ToString();

            ////16 OCT 2008-Added by Sibin
            //if(cmbCUSTOMER_STATE.SelectedItem!=null && cmbCUSTOMER_STATE.SelectedItem.Value!="")
            //    objCustomerInfo.CustomerState =	cmbCUSTOMER_STATE.SelectedValue;

            //if (hidSTATE_ID.Value !="")
            //    objCustomerInfo.CustomerState=hidSTATE_ID.Value;	
            //else if (cmbCUSTOMER_STATE.SelectedItem!=null && cmbCUSTOMER_STATE.SelectedItem.Value!="")
            //    objCustomerInfo.CustomerState=	cmbCUSTOMER_STATE.SelectedValue;

            ////Added till here by Sibin

            //objCustomerInfo.CustomerZip					=		txtCUSTOMER_ZIP.Text;



            //if ( this.cmbMARITAL_STATUS.Items.Count > 0 )
            //{
            //    objCustomerInfo.MARITAL_STATUS = this.cmbMARITAL_STATUS.SelectedItem.Value;
            //}

            //if(txtSSN_NO.Text != "")
            //{
            //    objCustomerInfo.SSN_NO			=	Cms.BusinessLayer.BlCommon.ClsCommon.EncryptString(txtSSN_NO.Text.Trim());
            //    txtSSN_NO.Text = "";
            //}
            //else
            //    objCustomerInfo.SSN_NO			= hidSSN_NO.Value;

            //// Added by mohit on 30/09/2005 as ssn number not saving.
            ////objCustomerInfo.SSN_NO=BusinessLayer.BlCommon.ClsCommon.EncryptString(txtSSN_NO.Text);

            //if(cmbCUSTOMER_TYPE.SelectedValue.ToString()=="11109")
            //{
            //    //Commercial
            //    objCustomerInfo.CustomerContactName			=		txtCUSTOMER_CONTACT_NAME.Text;
            //    objCustomerInfo.CustomerBusinessPhone		=		txtCUSTOMER_BUSINESS_PHONE.Text;
            //    objCustomerInfo.CustomerExt					=		txtCUSTOMER_EXT.Text;			
            //    objCustomerInfo.CustomerMobile				=		txtCUSTOMER_MOBILE.Text;
            //    objCustomerInfo.CustomerFax					=		txtCUSTOMER_FAX.Text;			    
            //    
            //    objCustomerInfo.CustomerPagerNo				=		txtCUSTOMER_PAGER_NO.Text;

            //    // Added by mohit on 18/10/2005 as foolowing fields retains values in case of commercial customer.
            //    objCustomerInfo.DATE_OF_BIRTH				=       Convert.ToDateTime(null); 
            //    objCustomerInfo.SSN_NO						=       "";
            //    objCustomerInfo.MARITAL_STATUS				=       "";
            //    // End-------------------------------------------------------------------------------------------.


            //    objCustomerInfo.EMPLOYER_ADD1				=		"";
            //    objCustomerInfo.EMPLOYER_ADD2				=		"";
            //    objCustomerInfo.EMPLOYER_CITY				=		"";
            //    objCustomerInfo.EMPLOYER_COUNTRY			=		"";
            //    objCustomerInfo.EMPLOYER_EMAIL				=		"";
            //    objCustomerInfo.EMPLOYER_HOMEPHONE			=		"";
            //    objCustomerInfo.EMPLOYER_ZIPCODE			=		"";
            //    objCustomerInfo.EMPLOYER_STATE				=		"";
            //    objCustomerInfo.EMPLOYER_NAME				=		"";
            //    objCustomerInfo.PER_CUST_MOBILE				=		"";

            //    //objCustomerInfo.CustomerBusinessType		=		cmbCUSTOMER_BUSINESS_TYPE.SelectedValue.ToString();
            //    objCustomerInfo.CustomerBusinessType = this.hidCUSTOMER_BUSINESS_TYPE.Value;
            //    if(txtCUSTOMER_BUSINESS_DESC.Text.Length>999)
            //    {
            //        objCustomerInfo.CustomerBusinessDesc		=		txtCUSTOMER_BUSINESS_DESC.Text.Substring(0,999);
            //    }
            //    else
            //        objCustomerInfo.CustomerBusinessDesc		=		txtCUSTOMER_BUSINESS_DESC.Text;
            //}
            //else
            //{
            //    //Personal

            //    objCustomerInfo.CustomerContactName			=		"";
            //    objCustomerInfo.CustomerBusinessPhone		=		"";
            //    objCustomerInfo.CustomerExt					=		"";			
            //    objCustomerInfo.CustomerMobile				=		"";
            //    objCustomerInfo.CustomerFax					=		"";			    
            //    objCustomerInfo.CustomerWebsite				=		"";
            //    objCustomerInfo.CustomerPagerNo				=		"";
            //    //objCustomerInfo.CustomerBusinessType		=		"-1";
            //    objCustomerInfo.CustomerBusinessType		=		"";
            //    objCustomerInfo.CustomerBusinessDesc		=		"";



            //    if ( this.cmbAPPLICANT_OCCU.SelectedItem.Value != "" )
            //    {
            //        objCustomerInfo.APPLICANT_OCCU = this.cmbAPPLICANT_OCCU.SelectedItem.Value;
            //    }
            //    else
            //        objCustomerInfo.APPLICANT_OCCU="0";

            //    if ( this.txtEMPLOYER_NAME.Text.Trim() != "" )
            //    {
            //        objCustomerInfo.EMPLOYER_NAME = txtEMPLOYER_NAME.Text.Trim();
            //    }
            //    else
            //        objCustomerInfo.EMPLOYER_NAME="";

            //    /*if ( this.txtEMPLOYER_ADDRESS.Text.Trim() != "" )
            //    {
            //        objCustomerInfo.EMPLOYER_ADDRESS = txtEMPLOYER_ADDRESS.Text.Trim();
            //    }
            //    else
            //        objCustomerInfo.EMPLOYER_ADDRESS="";*/

            //    if ( this.txtEMPLOYER_ADD1.Text.Trim() != "" )
            //    {
            //        objCustomerInfo.EMPLOYER_ADD1 = txtEMPLOYER_ADD1.Text.Trim();
            //    }
            //    if ( this.txtEMPLOYER_ADD2.Text.Trim() != "" )
            //    {
            //        objCustomerInfo.EMPLOYER_ADD2 = txtEMPLOYER_ADD2.Text.Trim();
            //    }
            //    if ( this.txtEMPLOYER_CITY.Text.Trim() != "" )
            //    {
            //        objCustomerInfo.EMPLOYER_CITY =txtEMPLOYER_CITY.Text.Trim();
            //    }
            //    if ( this.txtEMPLOYER_ZIPCODE.Text.Trim() != "" )
            //    {
            //        objCustomerInfo.EMPLOYER_ZIPCODE = txtEMPLOYER_ZIPCODE.Text.Trim();
            //    }
            //    if ( this.txtEMPLOYER_HOMEPHONE.Text.Trim() != "" )
            //    {
            //        objCustomerInfo.EMPLOYER_HOMEPHONE = txtEMPLOYER_HOMEPHONE.Text.Trim();
            //    }
            //    if ( this.txtEMPLOYER_EMAIL.Text.Trim() != "" )
            //    {
            //        objCustomerInfo.EMPLOYER_EMAIL = txtEMPLOYER_EMAIL.Text.Trim();
            //    }
            //    if ( this.txtYEARS_WITH_CURR_OCCU.Text.Trim() != "" )
            //    {
            //        objCustomerInfo.YEARS_WITH_CURR_OCCU = Convert.ToInt32(txtYEARS_WITH_CURR_OCCU.Text.Trim());
            //    }
            //    if ( this.cmbEMPLOYER_COUNTRY.SelectedItem!=null && this.cmbEMPLOYER_COUNTRY.SelectedItem.Text != "" )
            //    {
            //        objCustomerInfo.EMPLOYER_COUNTRY = cmbEMPLOYER_COUNTRY.SelectedValue.ToString();
            //    }


            //    //16 OCT 2008-Commented by Sibin

            //    /*if ( this.cmbEMPLOYER_STATE!=null && this.cmbEMPLOYER_STATE.SelectedItem.Text!="")
            //    {
            //        objCustomerInfo.EMPLOYER_STATE = cmbEMPLOYER_STATE.SelectedValue.ToString();
            //    }*/

            //    //16 OCT 2008-Added by Sibin

            //    if (hidEmpDetails_STATE_ID.Value !="")
            //        objCustomerInfo.EMPLOYER_STATE = hidEmpDetails_STATE_ID.Value;	
            //    else if (cmbEMPLOYER_STATE.SelectedItem!=null && cmbEMPLOYER_STATE.SelectedItem.Value!="")
            //        objCustomerInfo.EMPLOYER_STATE = cmbEMPLOYER_STATE.SelectedValue;

            //    //Added till here by Sibin

            //    if ( this.txtYEARS_WITH_CURR_EMPL.Text.Trim() != "" )
            //    {
            //        objCustomerInfo.YEARS_WITH_CURR_EMPL = Convert.ToInt32(txtYEARS_WITH_CURR_EMPL.Text.Trim());
            //    }

            //    // Added by mohit on 4/11/2005.
            //    // Check whether co applicant occupation is other.

            //    if (cmbAPPLICANT_OCCU.SelectedValue =="11427")
            //    {
            //        objCustomerInfo.DESC_APPLICANT_OCCU=txtDESC_APPLICANT_OCCU.Text;
            //    }
            //    else
            //    {
            //        objCustomerInfo.DESC_APPLICANT_OCCU="";	
            //    }
            //}

            //objCustomerInfo.PER_CUST_MOBILE				=		txtPER_CUST_MOBILE.Text;
            //objCustomerInfo.CustomerHomePhone			=		txtCUSTOMER_HOME_PHONE.Text;
            //objCustomerInfo.EMP_EXT						=		txtEMP_EXT.Text;	
            //objCustomerInfo.CustomerEmail				=		txtCUSTOMER_Email.Text;
            //if(txtCUSTOMER_INSURANCE_SCORE.Text.Trim() !="")
            //{
            //    objCustomerInfo.CustomerInsuranceScore		=	Convert.ToDecimal(txtCUSTOMER_INSURANCE_SCORE.Text);
            //}

            //if(txtCUSTOMER_INSURANCE_RECEIVED_DATE.Text!="")
            //{
            //    objCustomerInfo.CustomerInsuranceReceivedDate	=	Convert.ToDateTime(txtCUSTOMER_INSURANCE_RECEIVED_DATE.Text);
            //}

            //objCustomerInfo.CustomerReasonCode			=	cmbCUSTOMER_REASON_CODE.Items[cmbCUSTOMER_REASON_CODE.SelectedIndex].Text;   //SelectedValue.ToString();

            //if (cmbCUSTOMER_REASON_CODE2.SelectedValue.ToString() != "")
            //    objCustomerInfo.CustomerReasonCode2			=	cmbCUSTOMER_REASON_CODE2.Items[cmbCUSTOMER_REASON_CODE2.SelectedIndex].Text;

            //if (cmbCUSTOMER_REASON_CODE3.SelectedValue.ToString() != "")
            //    objCustomerInfo.CustomerReasonCode3			=	cmbCUSTOMER_REASON_CODE3.Items[cmbCUSTOMER_REASON_CODE3.SelectedIndex].Text;

            //if (cmbCUSTOMER_REASON_CODE4.SelectedValue.ToString() != "")
            //    objCustomerInfo.CustomerReasonCode4			=	cmbCUSTOMER_REASON_CODE4.Items[cmbCUSTOMER_REASON_CODE4.SelectedIndex].Text;

            //objCustomerInfo.IS_ACTIVE					=	"Y";
            //objCustomerInfo.CREATED_BY					=	int.Parse(GetUserId());
            //objCustomerInfo.PREFIX=0;
            //if(cmbPREFIX.SelectedValue.ToString()!="")
            //{
            //    objCustomerInfo.PREFIX						=	int.Parse(cmbPREFIX.SelectedValue);
            //}

            //    // Account Info Fields
            //    //			if(cmbCUSTOMER_REFERRED_BY.SelectedValue == "2")
            //    //			{
            //    //				objCustomerInfo.CustomerReferredBy = "2";
            //    //			}
            //else
            //{
            //    objCustomerInfo.CustomerReferredBy = "1";
            //}

            //if (cmbCUSTOMER_AGENCY_ID.SelectedValue != null && cmbCUSTOMER_AGENCY_ID.SelectedValue.Trim() != "")
            //{
            //    objCustomerInfo.CustomerAgencyId = Convert.ToInt32(cmbCUSTOMER_AGENCY_ID.SelectedValue);
            //}

            ///*objCustomerInfo.Alt_CustomerAddress1			=		txtAltCUSTOMER_ADDRESS1.Text;
            //objCustomerInfo.Alt_CustomerAddress2			=		txtAltCUSTOMER_ADDRESS2.Text;
            //objCustomerInfo.Alt_CustomerCity				=		txtAltCUSTOMER_CITY.Text;
            //objCustomerInfo.Alt_CustomerCountry				=		cmbAltCUSTOMER_COUNTRY.SelectedValue.ToString();
            //if(cmbAltCUSTOMER_COUNTRY.SelectedValue=="USA")
            //objCustomerInfo.Alt_CustomerState				=		cmbAltCUSTOMER_STATE.SelectedValue.ToString();
            //else
            //objCustomerInfo.Alt_CustomerState				=		txtAltCustomer_State.Text;
            //objCustomerInfo.Alt_CustomerZip					=		txtAltCUSTOMER_ZIP.Text;*/

            return objCustomerInfo;
        }
        #endregion

        #region function assigning the validators controls
        /// <summary>
        /// This function will set the Error Message,validation expresion all validators controls
        /// </summary>
        private void SetPageLabels()
        {

            //setting error message by calling singleton functions
            //Added By Lalit

            //rfvORIGINAL_ISSUE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "9");
            // rfvMAIN_DISTRICT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "10");
            //rfvDISTRICT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "10");
            //rfvNUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "11");
            // rfvMAIN_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "11");
            //rfvREG_ID_ISSUE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "12");
            //rfvMAIN_CITY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "56");
            //rfvMAIN_FIRST_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "57");
            // rfvMAIN_LAST_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "58");
            //rfvMAIN_CONTACT_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "15");
            //rfvMAIN_ADDRESS.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("32");

            // rfvMAIN_ZIPCODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "37");
            //reg_id_issue
            revREG_ID_ISSUE.ValidationExpression = aRegExpDate;
            revREG_ID_ISSUE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            //rfvREGIONAL_IDENTIFICATION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "13");

            //Date 202/04/2010

            revCPF_CNPJ.Attributes.Add("ErrMsgcpf", Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "21"));
            revCPF_CNPJ.Attributes.Add("ErrMsgcnpj", Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "22"));

            revMAIN_CPF_CNPJ.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "21");
            //revMAIN_CPF_CNPJ.Attributes.Add("ErrMsgcpf", Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "21"));
            //revMAIN_CPF_CNPJ.Attributes.Add("ErrMsgcnpj", Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "22"));

            //revCPF_CNPJ.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1081");//@"The format for CPF\CNPJ must be XXX.XXX.XXX-XX Or XX.XXX.XXX/XXXX-XX";
            revCPF_CNPJ.ValidationExpression = aRegExpCpf_Cnpj;
            revMAIN_CPF_CNPJ.ValidationExpression = aRegExpCpf_Cnpj;
            rfvCPF_CNPJ.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "18"); //"Please Enter CPF/CNPJ";//Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "13"); 
            //revACCOUNT_NUMBER.ValidationExpression = aRegExpAccountNumber;
            //revACCOUNT_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1198");
            //rfvMAIN_CPF_CNPJ.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "18"); //Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "13"); 
            //rfvCUSTOMER_ADDRESS1.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("32");
            //rfvCUSTOMER_COUNTRY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "33");
            rfvCUSTOMER_FIRST_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "57");
            // rfvCUSTOMER_LAST_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "58");
            //rfvCUSTOMER_STATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "35");
            // rfvMAIN_STATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "35");

            rfvCUSTOMER_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "36");
            //rfvCUSTOMER_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "37");
            rfvCUSTOMER_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "38");
            //rfvCUSTOMER_CITY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "56");
            //rfvAPPLICANT_OCCU.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "14");

            //Adding two different attributes to validation controls
            //and we will change the ErrorMessage attribute on javascript as per selection of customer type
            rfvCUSTOMER_FIRST_NAME.Attributes.Add("ErrMsgFirstName", Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "57"));
            rfvCUSTOMER_FIRST_NAME.Attributes.Add("ErrMsgCustomerName", Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "872"));
            //rfvACCOUNT_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1201");
            revCUSTOMER_WEBSITE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("737");
            csvCREATION_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1409"); 
            revCUSTOMER_Email.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("23");
            revEMPLOYER_EMAIL.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("23");
            revCUSTOMER_EXT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("25");
            revEMP_EXT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "25");
            //revBANK_BRANCH.ValidationExpression =aRegExpBankBranch;
            //revBANK_BRANCH.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1199");
            revCUSTOMER_PAGER_NO.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "200");
            //revBANK_NUMBER.ValidationExpression = aRegExpDoublePositiveNonZero;
            //revBANK_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");
            revEMPLOYER_HOMEPHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "14");
            
            revPER_CUST_MOBILE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "16");
            revEMPLOYER_ZIPCODE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("24");
            revCUSTOMER_INSURANCE_RECEIVED_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            revCUSTOMER_FIRST_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "13");
            revCUSTOMER_INSURANCE_SCORE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "13");
            revCUSTOMER_WEBSITE.ValidationExpression = aRegExpWebSiteUrl;

            revCUSTOMER_Email.ValidationExpression = aRegExpEmail;
            revEMPLOYER_EMAIL.ValidationExpression = aRegExpEmail;
            revCUSTOMER_EXT.ValidationExpression = aRegExpExtn;
            revEMP_EXT.ValidationExpression = aRegExpExtn;
            revEMAIL_ADDRESS.ValidationExpression = aRegExpEmail;
            revMONTHLY_INCOME.ValidationExpression = aRegExpBaseCurrencyformat;
            revNET_ASSETS_AMOUNT.ValidationExpression = aRegExpBaseCurrencyformat;
            revCUSTOMER_PAGER_NO.ValidationExpression = aRegExpFax;
            revEMPLOYER_HOMEPHONE.ValidationExpression = aRegExpPhone;
            revPER_CUST_MOBILE.ValidationExpression = aRegExpMobile;
            revEMPLOYER_ZIPCODE.ValidationExpression = aRegExpZip;
            revCUSTOMER_INSURANCE_RECEIVED_DATE.ValidationExpression = aRegExpDate;
            revCUSTOMER_FIRST_NAME.ValidationExpression = aRegExpAlphaNum;
            revCUSTOMER_INSURANCE_SCORE.ValidationExpression = aRegExpInteger;
            this.revEMAIL_ADDRESS.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("23");
            revMONTHLY_INCOME.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");

            revNET_ASSETS_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "32");
           // rfvMONTHLY_INCOME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "31");
           // rfvNET_ASSETS_AMOUNT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "33");
            csvCUSTOMER_INSURANCE_RECEIVED_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("629");
            csvCUSTOMER_BUSINESS_DESC.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("451");
            this.rfvCUSTOMER_AGENCY_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "5");
            revDATE_OF_BIRTH.ValidationExpression = aRegExpDate;
            revDATE_OF_BIRTH.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");//Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"22");
            //rfvMARITAL_STATUS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");
            revSSN_NO.ValidationExpression = aRegExpSSN;
            revSSN_NO.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "130");
            revYEARS_WITH_CURR_EMPL.ValidationExpression = aRegExpInteger;
            revYEARS_WITH_CURR_EMPL.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "163");
            revYEARS_WITH_CURR_OCCU.ValidationExpression = aRegExpInteger;
            revYEARS_WITH_CURR_OCCU.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "163");
            //rfvyesno.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "35");
            //hidDATE_OF_BIRTH.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "7");
            //hidCREATION_DATE.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "29");
            //revMONTHLY_INCOME.ValidationExpression = aRegExpBaseDouble;
            //revNET_ASSETS_AMOUNT.ValidationExpression = aRegExpBaseDouble;
            //rfvDATE_OF_BIRTH.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "7");
            //rfv2DATE_OF_BIRTH.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "29");
          
            csvDATE_OF_BIRTH.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");
            rfvDESC_APPLICANT_OCCU.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "457");
            rfvEMPLOYER_COUNTRY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "33");
            rfvEMPLOYER_STATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "35");
            //rfvGENDER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("189");

            //set Zip,Phone No,Fax,Mobile No as per Brazil Format
            revCUSTOMER_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1181"); //Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"24");
            revCUSTOMER_ZIP.ValidationExpression = aRegExpZipBrazil;//aRegExpZip;

            revMAIN_ZIPCODE.ValidationExpression = aRegExpZipBrazil;//aRegExpZip;
            revMAIN_ZIPCODE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1181");

            //revCUSTOMER_HOME_PHONE.ValidationExpression = aRegExpPhoneBrazil;
            //revCUSTOMER_HOME_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083");

            //revCUSTOMER_MOBILE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1086");
            //revCUSTOMER_MOBILE.ValidationExpression = aRegExpPhoneBrazil;

            //revCUSTOMER_FAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1085");
            //revCUSTOMER_FAX.ValidationExpression = aRegExpPhoneBrazil;

            //revCUSTOMER_BUSINESS_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083");
           // revCUSTOMER_BUSINESS_PHONE.ValidationExpression = aRegExpPhoneBrazil;
            revCUSTOMER_BUSINESS_PHONE.ValidationExpression = aRegExpAgencyPhone;
            revCUSTOMER_FAX.ValidationExpression = aRegExpAgencyPhone;
            revCUSTOMER_MOBILE.ValidationExpression = aRegExpAgencyPhone;
            revCUSTOMER_HOME_PHONE.ValidationExpression = aRegExpAgencyPhone;
            if (GetLanguageID() == "2")
            {

                revCUSTOMER_BUSINESS_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1913");
                revCUSTOMER_FAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1921");
                revCUSTOMER_MOBILE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2000");
                revCUSTOMER_HOME_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1913");

             }

            else
            {
                revCUSTOMER_BUSINESS_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1864");
                revCUSTOMER_FAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1085");
                revCUSTOMER_MOBILE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2000");
                revCUSTOMER_HOME_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1864");

            }




            csvIS_POLITICALLY_EXPOSED.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "35");
           
            csvMAIN_NOTE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "19");
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
            this.cmbCUSTOMER_COUNTRY.SelectedIndexChanged += new System.EventHandler(this.cmbCUSTOMER_COUNTRY_SelectedIndexChanged);
            //this.cmbMAIN_COUNTRY.SelectedIndexChanged += new System.EventHandler(this.cmbMAIN_COUNTRY_SelectedIndexChanged);
            //this.cmbEMPLOYER_COUNTRY.SelectedIndexChanged += new System.EventHandler(this.cmbEMPLOYER_COUNTRY_SelectedIndexChanged);
            this.txtCUSTOMER_ZIP.TextChanged += new System.EventHandler(this.txtCUSTOMER_ZIP_TextChanged);
            this.btnGetInsuranceScore.Click += new System.EventHandler(this.btnGetInsuranceScore_Click);
            this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnCopyClient.Click += new System.EventHandler(this.btnCopyClient_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        #region FillAgencyDropdown
        /// <summary>
        /// Fills the agency drop down combo
        /// </summary>
        private void FillAgencyDropdown()
        {
            try
            {
                if (GetSystemId().ToUpper() == CarrierSystemID.ToUpper())// System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToUpper())
                {
                    hidCarrierId.Value = "1";
                    //Filling all the agency in drop if agency is wolverine, [as in web.config]
                    ClsAgency objAgency = new ClsAgency();
                    int Broker = (int)enumAgencyType.BROKER_AGENCY;
                    DataSet objDataSet = objAgency.FillAgency();
                    cmbCUSTOMER_AGENCY_ID.Items.Clear();
                    if (objDataSet != null && objDataSet.Tables.Count > 0 && objDataSet.Tables[0].Rows.Count > 0)
                    {
                        cmbCUSTOMER_AGENCY_ID.DataSource = objDataSet.Tables[0].Select("AGENCY_TYPE =" + Broker).CopyToDataTable(); 
                        //cmbCUSTOMER_AGENCY_ID.DataTextField					=	"AGENCY_DISPLAY_NAME";
                        cmbCUSTOMER_AGENCY_ID.DataTextField = "AGENCY_NAME_ACTIVE_STATUS";
                        cmbCUSTOMER_AGENCY_ID.DataValueField = "AGENCY_ID";
                        cmbCUSTOMER_AGENCY_ID.DataBind();
                        cmbCUSTOMER_AGENCY_ID.Items.Insert(0, new ListItem("", ""));
                        cmbCUSTOMER_AGENCY_ID.SelectedIndex = -1;
                    }
                    //Fill agency Color

                    GetCustomerAgencyColor();

                    //cmbCUSTOMER_ACCOUNT_EXECUTIVE_ID.Items.Clear();
                    //cmbCUSTOMER_CSR.Items.Clear();
                    //cmbCUSTOMER_PRODUCER_ID.Items.Clear();
                }
                else
                {

                    hidCarrierId.Value = "0";

                    //Adding the selected agency only
                    cmbCUSTOMER_AGENCY_ID.Items.Clear();
                    int intAgencyID = ClsAgency.GetAgencyIDFromCode(GetSystemId());
                    string strAgencyName = ClsAgency.GetAgencyName(intAgencyID.ToString());
                    cmbCUSTOMER_AGENCY_ID.Items.Add(new ListItem(strAgencyName, intAgencyID.ToString()));
                    cmbCUSTOMER_AGENCY_ID.SelectedIndex = 0;
                    //Populating the account exec, csr and producer of selected agency
                    //FillAccountsDetails(intAgencyID);
                    //FillCSRDropDown(intAgencyID);
                    //FillProducerDropDown(intAgencyID);
                }
            }
            catch (Exception objEx)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
            }
        }
        #endregion

        #region Fill Agency Color
        private void GetCustomerAgencyColor()
        {
            ClsAgency objAgency = new ClsAgency();
            DataSet ods = objAgency.GetCustomerAgency();
            string strCusAgnID = "";
            string StrCusIds = "";
            foreach (DataRow row in ods.Tables[0].Rows)
            {
                strCusAgnID = row["AGENCY_ID"].ToString();
                StrCusIds = StrCusIds + strCusAgnID + "^";
            }
            hidCustomer_AGENCY_ID.Value = StrCusIds;
        }
        #endregion

        #region Displaying the values from the database
        /// <summary>
        /// Function for Displaying the values from the database
        /// </summary>
        /// 


        private void BindamountType()
        {
            cmbAMOUNT_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("AMTTYP");
            cmbAMOUNT_TYPE.DataTextField = "LookupDesc";
            cmbAMOUNT_TYPE.DataValueField = "LookupID";
            cmbAMOUNT_TYPE.DataBind();
            cmbAMOUNT_TYPE.Items.Insert(0, "");
        } //private void BindamountType()

         private void BindaccountType()
        {

          cmbACCOUNT_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("ACTYP");
            cmbACCOUNT_TYPE.DataTextField = "LookupDesc";
            cmbACCOUNT_TYPE.DataValueField = "LookupID";
            cmbACCOUNT_TYPE.DataBind();
            cmbACCOUNT_TYPE.Items.Insert(0, "");

        } //private void BindaccountType()

        private void FillAccountsDetails(int intAgencyId)
        {

            ClsUser objUser = new ClsUser();
            DataSet objDataSet = new DataSet();

            //Filling the Account Executive DorpDown
            //			objDataSet = objUser.FillUser("ACCE",intAgencyId);
            //			cmbCUSTOMER_ACCOUNT_EXECUTIVE_ID.DataSource			=	objDataSet.Tables[0];
            //			cmbCUSTOMER_ACCOUNT_EXECUTIVE_ID.DataTextField		=	"USER_NAME";
            //			cmbCUSTOMER_ACCOUNT_EXECUTIVE_ID.DataValueField		=	"USER_ID";
            //			cmbCUSTOMER_ACCOUNT_EXECUTIVE_ID.DataBind();
            //			cmbCUSTOMER_ACCOUNT_EXECUTIVE_ID.Items.Insert(0,new ListItem("",""));
            //			cmbCUSTOMER_ACCOUNT_EXECUTIVE_ID.SelectedIndex = 0;

        }

        #endregion

        #region Fill CSRdropdown method
        /// <summary>
        /// Fills the CSR of specefied agency in drop down
        /// </summary>
        /// <param name="AgencyID">Id of agency</param>
        private void FillCSRDropDown(int AgencyID)
        {
            try
            {
                //fill CSR dropdown
                DataTable dtAgencyUsers = ClsUser.GetAgencyUsers(AgencyID);
                //				cmbCUSTOMER_CSR.DataSource		= new DataView(dtAgencyUsers);
                //				cmbCUSTOMER_CSR.DataTextField	= "USERNAME";
                //				cmbCUSTOMER_CSR.DataValueField	= "USER_ID";
                //				cmbCUSTOMER_CSR.DataBind();
                //				cmbCUSTOMER_CSR.Items.Insert(0,new ListItem("",""));
                //				cmbCUSTOMER_CSR.SelectedIndex=0;
            }
            catch (Exception exc)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
            }

        }
        #endregion

        #region Fill Producer method
        /// <summary>
        /// Fills the Producer of specefied agency in drop down
        /// </summary>
        /// <param name="AgencyID">Id of agency</param>
        private void FillProducerDropDown(int AgencyID)
        {
            try
            {
                //Populating the producer
                Cms.BusinessLayer.BlCommon.ClsUser objUser = new Cms.BusinessLayer.BlCommon.ClsUser();
                DataTable dt = objUser.FillUser("PRO", AgencyID).Tables[0];
                //				cmbCUSTOMER_PRODUCER_ID.DataSource = dt;
                //				cmbCUSTOMER_PRODUCER_ID.DataTextField = "USER_NAME";
                //				cmbCUSTOMER_PRODUCER_ID.DataValueField = "USER_ID";
                //				cmbCUSTOMER_PRODUCER_ID.DataBind();
                //				cmbCUSTOMER_PRODUCER_ID.Items.Insert(0,new System.Web.UI.WebControls.ListItem("",""));
                //				cmbCUSTOMER_PRODUCER_ID.Items.Insert(1,new System.Web.UI.WebControls.ListItem("Unassigned Producer","00"));
                //				cmbCUSTOMER_PRODUCER_ID.SelectedIndex = 0;
            }
            catch (Exception exc)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
            }

        }

        //COMMENETD BY PAWAN, NOT BEING USED
        //		private void FillProducerDropDownWithCode(string AgencyCode)
        //		{	
        //			try
        //			{
        //				//Populating the producer
        //				Cms.BusinessLayer.BlCommon.ClsUser objUser = new Cms.BusinessLayer.BlCommon.ClsUser();
        //				DataTable dt = objUser.FillUser("PRO",AgencyCode).Tables[0];
        //				//				cmbCUSTOMER_PRODUCER_ID.DataSource = dt;
        //				//				cmbCUSTOMER_PRODUCER_ID.DataTextField = "USER_NAME";
        //				//				cmbCUSTOMER_PRODUCER_ID.DataValueField = "USER_ID";
        //				//				cmbCUSTOMER_PRODUCER_ID.DataBind();
        //				//				cmbCUSTOMER_PRODUCER_ID.Items.Insert(0,"");
        //				//cmbCUSTOMER_PRODUCER_ID.Items[0].Value="0";  
        //
        //				//cmbCUSTOMER_PRODUCER_ID.Items.Insert(1,new System.Web.UI.WebControls.ListItem("Gaurav","Gaurav"));
        //				//cmbCUSTOMER_PRODUCER_ID.Items.Insert(1,new System.Web.UI.WebControls.ListItem("Unassigned Producer","00"));
        //				//cmbCUSTOMER_PRODUCER_ID.SelectedIndex = 0;
        //			}
        //			catch(Exception exc)
        //			{
        //				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
        //			}
        //			
        //		}

        #endregion

        #region Web form control's events
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            // Saving the values into the database
          
                SaveFormValue();
           
        }

        private void btnCopyClient_Click(object sender, System.EventArgs e)
        {
            objCustomer = new Cms.BusinessLayer.BlClient.ClsCustomer();
            int retval;
            int New_Cust_Id = 0;
            try
            {
                //retval = objCustomer.CopyCustomerTabs(int.Parse(hidCUSTOMER_ID.Value));

                /*Commented by Asfa (01-May-2008) - iTrack issue #4103
                //retval = objCustomer.CopyCustomerTabs(int.Parse(hidCUSTOMER_ID.Value), ref New_Cust_Id);
                */
                retval = objCustomer.CopyCustomerTabs(int.Parse(hidCUSTOMER_ID.Value), intLoggedInUserID, ref New_Cust_Id);

            }
            catch (Exception)
            {
                retval = -1;
            }
            if (retval >= 0)
            {
                hidCUSTOMER_ID.Value = New_Cust_Id.ToString();
                //	lblMessage.Text			=	"All Customer details including Co-Applicant and Attention Notes copied successfully. To edit newly copied Customer please search again from Customer Manager."; 				
                //added by Uday on 13-March-2008
                LoadData();
                lblMessage.Text = "All Customer details including Co-Applicant and Attention Notes copied successfully.";
                string CstTab = "";
                CstTab = "<script> " +
                    "RefreshCustomerTabIndex();" +
                    "</script> ";
                ClientScript.RegisterStartupScript(this.GetType(),"Test", CstTab);
                //				
            }
            else
                lblMessage.Text = "Error encountered in copying Customer Details\n Try again!";
            lblMessage.Visible = true;

        }

        private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
        {
            //Here we will call the ActivateDeactivate method of ClsCustomer class
            //To activate or deactivate the record
            try
            {
                int CustomerId;

                Cms.Model.Maintenance.ClsTransactionInfo objTransaction;
                objTransaction = new Cms.Model.Maintenance.ClsTransactionInfo();

                objCustomer = new Cms.BusinessLayer.BlClient.ClsCustomer();
                CustomerId = int.Parse(hidCUSTOMER_ID.Value);
                string strStatus;
                ClsCustomerInfo objCustomerInfo = new ClsCustomerInfo();
                //objCustomerInfo	= getFormValue();
                base.PopulateModelObject(objCustomerInfo, hidOldXML.Value);
                objCustomerInfo.MODIFIED_BY = int.Parse(GetUserId());
                objCustomerInfo.CustomerId = int.Parse(hidCUSTOMER_ID.Value);

                if (hidIS_ACTIVE.Value.ToString().ToUpper().Equals("N"))
                {
                    strStatus = "Y";
                    objCustomer.ActivateDeactivate(objCustomerInfo, strStatus);
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "40");
                    hidIS_ACTIVE.Value = "Y";
                    lblIS_ACTIVE.Text = "Active";
                    lblIS_ACTIVE.ForeColor = System.Drawing.Color.Black;
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(strStatus.ToUpper());

                    //					if(objCustomer.TransactionLogRequired)
                    //					{
                    //						objTransaction.TRANS_DESC	=	"Customer " + CustomerId.ToString() + " Has been Activated";
                    //					}
                }
                else
                {
                    strStatus = "N";

                    int iCustomerID = Convert.ToInt32(hidCUSTOMER_ID.Value);

                    DataSet dsCustomer = ClsCustomer.GetCustomerDetails(iCustomerID);


                    DataTable dtpol = dsCustomer.Tables[1];
                    TotPolicy = int.Parse(dtpol.Rows[0]["TOTPOLICY"].ToString());

                    if (TotPolicy > 0)
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "25"); ;//Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "40");
                        hidIS_ACTIVE.Value = "Y";

                        lblIS_ACTIVE.ForeColor = System.Drawing.Color.Black;
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText("Y");
                    }
                    else
                    {
                        objCustomer.ActivateDeactivate(objCustomerInfo, strStatus);
                        if (objCustomer.TransactionLogRequired)
                        {
                            objTransaction.TRANS_DESC = "Customer " + CustomerId.ToString() + " Has been Deactivated";
                            objTransaction.CUSTOM_INFO = ";Customer Name = " + objCustomerInfo.CustomerFirstName + " " + objCustomerInfo.CustomerMiddleName + " " + objCustomerInfo.CustomerLastName + ";Customer Code = " + objCustomerInfo.CustomerCode;
                        }
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "41");
                        hidIS_ACTIVE.Value = "N";
                        lblIS_ACTIVE.Text = "Inactive";
                        lblIS_ACTIVE.ForeColor = System.Drawing.Color.Red;
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(strStatus.ToUpper());
                    }
                }

                //				if(objCustomer.TransactionLogRequired)
                //				{
                //					objTransaction.RECORDED_BY		= int.Parse(GetUserId());
                //					objTransaction.TRANS_TYPE_ID	= 2;
                //					objCustomer.ActivateDeactivate(CustomerId.ToString(),strStatus,objTransaction);
                //				}
                //				else
                //				{
                //objCustomer.ActivateDeactivate(CustomerId.ToString(),strStatus);
                //				}
                //objCustomer.ActivateDeactivate(objCustomerInfo, strStatus);

                //if (strStatus.ToUpper().Equals("Y"))
                //{
                //    lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "40");
                //    hidIS_ACTIVE.Value = "Y";
                //    lblIS_ACTIVE.Text = "Active";
                //    lblIS_ACTIVE.ForeColor = System.Drawing.Color.Black;
                //    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(strStatus.ToUpper());
                //}
                //else
                //{
                //    lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "41");
                //    hidIS_ACTIVE.Value = "N";
                //    lblIS_ACTIVE.Text = "Inactive";
                //    lblIS_ACTIVE.ForeColor = System.Drawing.Color.Red;
                //    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(strStatus.ToUpper());

                //}
                lblMessage1.Visible = false;
                lblMessage.Visible = true;
                hidFormSaved.Value = "1";
                CreateTransXML();
            }
            catch (Exception ex)
            {
                //Publishing the exception using the static method of Exception manager class
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "21") + "\n" + ex.Message + "\n Try again!";
                lblMessage.Visible = true;
            }
        }

        #endregion

        #region set label Captions by reading resource file
        /// <summary>
        /// Function for setting the label Captions by reading resource file
        /// </summary>
        private void SetCaptions()
        {

            capCUSTOMER_PARENT.Text = objResourceMgr.GetString("txtCUSTOMER_PARENT");
            capCUSTOMER_TYPE.Text = objResourceMgr.GetString("cmbCUSTOMER_TYPE");
            capCUSTOMER_SUFFIX.Text = objResourceMgr.GetString("txtCUSTOMER_SUFFIX");
            capCUSTOMER_FIRST_NAME.Text = objResourceMgr.GetString("txtCUSTOMER_FIRST_NAME");
            capCUSTOMER_MIDDLE_NAME.Text = objResourceMgr.GetString("txtCUSTOMER_MIDDLE_NAME");
            capCUSTOMER_LAST_NAME.Text = objResourceMgr.GetString("txtCUSTOMER_LAST_NAME");
            capCUSTOMER_CODE.Text = objResourceMgr.GetString("txtCUSTOMER_CODE");
            capCUSTOMER_ADDRESS1.Text = objResourceMgr.GetString("txtCUSTOMER_ADDRESS1");
            capCUSTOMER_ADDRESS2.Text = objResourceMgr.GetString("txtCUSTOMER_ADDRESS2");
            capCUSTOMER_CITY.Text = objResourceMgr.GetString("txtCUSTOMER_CITY");
            capCUSTOMER_COUNTRY.Text = objResourceMgr.GetString("cmbCUSTOMER_COUNTRY");
            capCUSTOMER_STATE.Text = objResourceMgr.GetString("cmbCUSTOMER_STATE");
            capCUSTOMER_ZIP.Text = objResourceMgr.GetString("txtCUSTOMER_ZIP");
            capCUSTOMER_BUSINESS_TYPE.Text = objResourceMgr.GetString("cmbCUSTOMER_BUSINESS_TYPE");
            capCUSTOMER_BUSINESS_DESC.Text = objResourceMgr.GetString("txtCUSTOMER_BUSINESS_DESC");
            capCUSTOMER_CONTACT_NAME.Text = objResourceMgr.GetString("txtCUSTOMER_CONTACT_NAME");
            capCUSTOMER_BUSINESS_PHONE.Text = objResourceMgr.GetString("txtCUSTOMER_BUSINESS_PHONE");
            capCUSTOMER_EXT.Text = objResourceMgr.GetString("txtCUSTOMER_EXT");
            capEMP_EXT.Text = objResourceMgr.GetString("txtEMP_EXT");
            capCUSTOMER_HOME_PHONE.Text = objResourceMgr.GetString("txtCUSTOMER_HOME_PHONE");
            capCUSTOMER_MOBILE.Text = objResourceMgr.GetString("txtCUSTOMER_MOBILE");
            capPER_CUST_MOBILE.Text = objResourceMgr.GetString("txtPER_CUST_MOBILE");
            capCUSTOMER_FAX.Text = objResourceMgr.GetString("txtCUSTOMER_FAX");
            capCUSTOMER_PAGER_NO.Text = objResourceMgr.GetString("txtCUSTOMER_PAGER_NO");
            capCUSTOMER_Email.Text = objResourceMgr.GetString("txtCUSTOMER_Email");
            capCUSTOMER_WEBSITE.Text = objResourceMgr.GetString("txtCUSTOMER_WEBSITE");
            capCUSTOMER_INSURANCE_SCORE.Text = objResourceMgr.GetString("txtCUSTOMER_INSURANCE_SCORE");
            capCUSTOMER_INSURANCE_RECEIVED_DATE.Text = objResourceMgr.GetString("txtCUSTOMER_INSURANCE_RECEIVED_DATE");
            capCUSTOMER_REASON_CODE.Text = objResourceMgr.GetString("cmbCUSTOMER_REASON_CODE");
            capCUSTOMER_REASON_CODE2.Text = objResourceMgr.GetString("cmbCUSTOMER_REASON_CODE2");
            capCUSTOMER_REASON_CODE3.Text = objResourceMgr.GetString("cmbCUSTOMER_REASON_CODE3");
            capCUSTOMER_REASON_CODE4.Text = objResourceMgr.GetString("cmbCUSTOMER_REASON_CODE4");
            capPREFIX.Text = objResourceMgr.GetString("cmbPREFIX");
            capSCORE.Text = objResourceMgr.GetString("capSCORE");			// Added by mohit
            capDESC_APPLICANT_OCCU.Text = objResourceMgr.GetString("txtDESC_APPLICANT_OCCU");
            // End
            capCREATION_DATE.Text = objResourceMgr.GetString("capCREATION_DATE");
            capDATE_OF_BIRTH.Text = objResourceMgr.GetString("txtDATE_OF_BIRTH");
            capMARITAL_STATUS.Text = objResourceMgr.GetString("cmbMARITAL_STATUS");
            capAPPLICANT_OCCU.Text = objResourceMgr.GetString("cmbAPPLICANT_OCCU");
            capSSN_NO.Text = objResourceMgr.GetString("txtSSN_NO");
            //capEMPLOYER_ADDRESS.Text =		objResourceMgr.GetString("txtEMPLOYER_ADDRESS");
            capEMPLOYER_ADD1.Text = objResourceMgr.GetString("txtEMPLOYER_ADD1");
            capEMPLOYER_ADD2.Text = objResourceMgr.GetString("txtEMPLOYER_ADD2");
            capEMPLOYER_CITY.Text = objResourceMgr.GetString("txtEMPLOYER_CITY");
            capEMPLOYER_COUNTRY.Text = objResourceMgr.GetString("cmbEMPLOYER_COUNTRY");
            capEMPLOYER_STATE.Text = objResourceMgr.GetString("cmbEMPLOYER_STATE");
            capEMPLOYER_EMAIL.Text = objResourceMgr.GetString("txtEMPLOYER_EMAIL");
            capEMPLOYER_ZIPCODE.Text = objResourceMgr.GetString("txtEMPLOYER_ZIPCODE");
            capEMPLOYER_HOMEPHONE.Text = objResourceMgr.GetString("txtEMPLOYER_HOMEPHONE");


            capEMPLOYER_NAME.Text = objResourceMgr.GetString("txtEMPLOYER_NAME");
           // capCUSTOMER_AGENCY_ID.Text = objResourceMgr.GetString("cmbCUSTOMER_AGENCY_ID");

            capYEARS_WITH_CURR_EMPL.Text = objResourceMgr.GetString("txtYEARS_WITH_CURR_EMPL");
            capYEARS_WITH_CURR_OCCU.Text = objResourceMgr.GetString("txtYEARS_WITH_CURR_OCCU");
            capGENDER.Text = objResourceMgr.GetString("cmbGENDER");
            capREGIONAL_IDENTIFICATION_TYPE.Text = objResourceMgr.GetString("txtREGIONAL_IDENTIFICATION_TYPE");
            capNATIONALITY.Text = objResourceMgr.GetString("txtNATIONALITY");
            capEMAIL_ADDRESS.Text = objResourceMgr.GetString("txtEMAIL_ADDRESS");
            CapID_TYPE.Text = objResourceMgr.GetString("txtID_TYPE");
            CapMONTHLY_INCOME.Text = objResourceMgr.GetString("txtMONTHLY_INCOME");
            CapAMOUNT_TYPE.Text = objResourceMgr.GetString("cmbAMOUNT_TYPE");
            capCADEMP.Text = objResourceMgr.GetString("txtCADEMP");
            capNET_ASSETS_AMOUNT.Text = objResourceMgr.GetString("capNET_ASSETS_AMOUNT");
            //chkType.Text = objResourceMgr.GetString("chkType");
            //capMessage1.Text = objResourceMgr.GetString("capMessage1");
            capACCOUNT_TYPE.Text = objResourceMgr.GetString("cmbACCOUNT_TYPE");
            capACCOUNT_NUMBER.Text = objResourceMgr.GetString("txtACCOUNT_NUMBER");
            capBANK_NAME.Text = objResourceMgr.GetString("txtBANK_NAME");
            capBANK_BRANCH.Text = objResourceMgr.GetString("txtBANK_BRANCH");
            capBANK_NUMBER.Text = objResourceMgr.GetString("txtBANK_NUMBER");
            capIS_POLITICALLY_EXPOSED.Text = objResourceMgr.GetString("capIS_POLITICALLY_EXPOSED");

            /*capAltCUSTOMER_ADDRESS1.Text					=		objResourceMgr.GetString("txtCUSTOMER_ADDRESS1");
            capAltCUSTOMER_ADDRESS2.Text					=		objResourceMgr.GetString("txtCUSTOMER_ADDRESS2");
            capAltCUSTOMER_CITY.Text						=		objResourceMgr.GetString("txtCUSTOMER_CITY");
            capAltCUSTOMER_COUNTRY.Text					=		objResourceMgr.GetString("cmbCUSTOMER_COUNTRY");
            capAltCUSTOMER_STATE.Text						=		objResourceMgr.GetString("cmbCUSTOMER_STATE");
            capAltCUSTOMER_ZIP.Text						=		objResourceMgr.GetString("txtCUSTOMER_ZIP");*/


            // Account Info Fields

            //capCUSTOMER_LATE_CHARGES.Text			=		objResourceMgr.GetString("chkCUSTOMER_LATE_CHARGES");
            //capCUSTOMER_LATE_NOTICES.Text 			=		objResourceMgr.GetString("chkCUSTOMER_LATE_NOTICES");
            //capCUSTOMER_RECEIVABLE_DUE_DAYS.Text	=		objResourceMgr.GetString("txtCUSTOMER_RECEIVABLE_DUE_DAYS");
            //capCUSTOMER_REFERRED_BY.Text			=		objResourceMgr.GetString("txtCUSTOMER_REFERRED_BY");
            //capCUSTOMER_SEND_STATEMENT.Text			=		objResourceMgr.GetString("chkCUSTOMER_SEND_STATEMENT");
            lblSCORE.Text = objResourceMgr.GetString("lblSCORE");

            capCPF_CNPJ.Text = objResourceMgr.GetString("txtCPF_CNPJ");
            capNUMBER.Text = objResourceMgr.GetString("txtNUMBER");
            capCUSTOMER_ADDRESS.Text = objResourceMgr.GetString("txtCUSTOMER_ADDRESS2");
            capDISTRICT.Text = objResourceMgr.GetString("txtDISTRICT");
            capMAIN_TITLE.Text = objResourceMgr.GetString("cmbMAIN_TITLE");
            //capMAIN_POSITION.Text = objResourceMgr.GetString("cmbMAIN_POSITION");
            capMAIN_CPF_CNPJ.Text = objResourceMgr.GetString("txtMAIN_CPF_CNPJ");
            capMAIN_ADDRESS.Text = objResourceMgr.GetString("txtMAIN_ADDRESS");
            capMAIN_NUMBER.Text = objResourceMgr.GetString("txtMAIN_NUMBER");
            capMAIN_COMPLIMENT.Text = objResourceMgr.GetString("txtMAIN_COMPLIMENT");
            capMAIN_DISTRICT.Text = objResourceMgr.GetString("txtMAIN_DISTRICT");
            capMAIN_NOTE.Text = objResourceMgr.GetString("txtMAIN_NOTE");
            capCUSTOMER_AGENCY_ID.Text = objResourceMgr.GetString("capCUSTOMER_AGENCY_ID");
            capMAIN_CONTACT_CODE.Text = objResourceMgr.GetString("txtMAIN_CONTACT_CODE");
            btnSave.Text = objResourceMgr.GetString("btnSave");
            capREG_ID_ISSUE.Text = objResourceMgr.GetString("txtREG_ID_ISSUE");
            capREGIONAL_IDENTIFICATION.Text = objResourceMgr.GetString("txtREGIONAL_IDENTIFICATION");
            capORIGINAL_ISSUE.Text = objResourceMgr.GetString("txtORIGINAL_ISSUE");
            capMAIN_CITY.Text = objResourceMgr.GetString("txtMAIN_CITY");
            capMAIN_STATE.Text = objResourceMgr.GetString("cmbMAIN_STATE");
            capMAIN_COUNTRY.Text = objResourceMgr.GetString("cmbMAIN_COUNTRY");
            capMAIN_ZIPCODE.Text = objResourceMgr.GetString("txtMAIN_ZIPCODE");
            capMAIN_FIRST_NAME.Text = objResourceMgr.GetString("txtMAIN_FIRST_NAME");
            capMAIN_MIDDLE_NAME.Text = objResourceMgr.GetString("txtMAIN_MIDDLE_NAME");
            capMAIN_LAST_NAME.Text = objResourceMgr.GetString("txtMAIN_LAST_NAME");
            hidZipeCodeVerificationMsg.Value = objResourceMgr.GetString("hidZipeCodeVerificationMsg");
            btnCopyCustomerAddress.Text = ClsMessages.GetButtonsText(ScreenId, "btnCopyCustomerAddress");
            btnAddNewApplication.Text = ClsMessages.GetButtonsText(ScreenId, "btnAddNewApplication");
            btnCopyClient.Text = ClsMessages.GetButtonsText(ScreenId,"btnCopyClient");
            lblYES.Text = objResourceMgr.GetString("lblYES");
            lblNO.Text = objResourceMgr.GetString("lblNO");
            Caplook.Text = objResourceMgr.GetString("hidLookup");
        }
        #endregion





        #region create transaction Log XML
        /// <summary>
        /// Creating XML for related to Customer's Information    
        /// </summary>
        private void CreateTransXML()
        {
            ClsCustomer objCustomer = new ClsCustomer();
            int iCustomerID = Convert.ToInt32(Request.Params["CUSTOMER_ID"]);
            string strXML = objCustomer.FillCustomerDetails(iCustomerID);
            hidOldXML.Value = strXML;
            //Setting the Customer ID into the session
            SetCustomerID(iCustomerID.ToString());

            if (hidOldXML.Value != "" && !Page.IsPostBack)
            {
                //Filling the csr, producer and acct exe combo
                //as per agency id in xml
                //so that it can be populated in populatedXML javascript
                //function
                strXML = hidOldXML.Value;
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.LoadXml(strXML);

                //Selecting the agency id node
                System.Xml.XmlNode nod = doc.SelectSingleNode("/NewDataSet/Table/CUSTOMER_AGENCY_ID");

                if (nod != null)
                {
                    //Popualting the drop downs as per agency id
                    //FillCSRDropDown(int.Parse(nod.InnerText));
                    //FillProducerDropDown(int.Parse(nod.InnerText));
                    //FillAccountsDetails(int.Parse(nod.InnerText));

                }
            }

        }
        #endregion

        private void btnGetInsuranceScore_Click(object sender, System.EventArgs e)
        {
            objCustomer = new Cms.BusinessLayer.BlClient.ClsCustomer();

            ClsCustomerInfo objCustInfo = new ClsCustomerInfo();
            ClsCustomerInfo objOldCustInfo = new ClsCustomerInfo();
            base.PopulateModelObject(objOldCustInfo, hidOldXML.Value);
            base.PopulateModelObject(objCustInfo, hidOldXML.Value);
            objCustInfo.MODIFIED_BY = int.Parse(GetUserId());
            int intScore = -1;

            lblMessage.Visible = true;

            CreditScoreDetails objScore;

            System.Collections.Specialized.NameValueCollection dic = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationManager.GetSection("IIXSettings");
            string strUserName = dic["UserName"].ToString();
            string strPassword = dic["Password"].ToString();
            string strAccountNumber = dic["AccountNumber"].ToString();
            string strUrl = dic["URL"].ToString();
            Cms.CmsWeb.Utils.Utility objUtility = new Utility(strUserName, strPassword, strAccountNumber, strUrl);
            //SetFocus("txtCUSTOMER_INSURANCE_SCORE");
            hidTabInsScore.Value = "1"; // set tab focus on txtInsuranceScore
            try
            {
                objScore = objUtility.GetCustomerCreditScore(objCustInfo);
                intScore = objScore.Score;


            }
            catch (Exception ex)
            {
                //throw(new Exception("Error occured while parsing iix response." + objExp.Message));
                if (ex.Message.IndexOf("ADDRESS_NOT_VALIDATED") != -1)
                    this.lblMessage.Text = "Customer Insurance score can not be retrieved. Address could not be validated while fetching Insurance Score.";
                else
                    this.lblMessage.Text = "Customer Insurance score can not be retrieved";
                return;
            }

            if (intScore == -1)
            {
                this.lblMessage.Text = "Insurance score was not found for this customer.";

                //Added by Mohit Agarwal 8-Nov-2006
                if (this.txtCUSTOMER_INSURANCE_SCORE.Text.Trim() == "")
                    this.txtCUSTOMER_INSURANCE_SCORE.Text = "000";
                this.chkSCORE.Checked = true;
                this.txtCUSTOMER_INSURANCE_RECEIVED_DATE.Text = DateTime.Now.ToShortDateString();

                objCustInfo.CustomerInsuranceScore = intScore;
                objCustInfo.FACTOR1 = objScore.FirstFactor;
                objCustInfo.FACTOR2 = objScore.SecondFactor;
                objCustInfo.FACTOR3 = objScore.ThirdFactor;
                objCustInfo.FACTOR4 = objScore.FourthFactor;
                //Update the recieved Date details in the database
                objCustInfo.CustomerInsuranceReceivedDate = Convert.ToDateTime(txtCUSTOMER_INSURANCE_RECEIVED_DATE.Text);
                int resultVal = objCustomer.SetInsuranceScore(objCustInfo, objOldCustInfo);

                return;
            }
            if (intScore == -2)
                chkSCORE.Checked = true;
            this.txtCUSTOMER_INSURANCE_SCORE.Text = intScore.ToString();
            this.txtCUSTOMER_INSURANCE_RECEIVED_DATE.Text = DateTime.Now.ToShortDateString();
            objCustInfo.CustomerInsuranceScore = intScore;
            objCustInfo.FACTOR1 = objScore.FirstFactor;
            objCustInfo.FACTOR2 = objScore.SecondFactor;
            objCustInfo.FACTOR3 = objScore.ThirdFactor;
            objCustInfo.FACTOR4 = objScore.FourthFactor;

            //Update the insurance score details in the database
            objCustInfo.CustomerInsuranceReceivedDate = Convert.ToDateTime(txtCUSTOMER_INSURANCE_RECEIVED_DATE.Text);
            int result = objCustomer.SetInsuranceScore(objCustInfo, objOldCustInfo);

            if (result == -1)
            {
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("417");

            }
            else if (result == 1)
            {
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("418");

                this.btnGetInsuranceScore.Enabled = false;

                //Set the Customer reson codes
                ListItem li;


                //Reason code 1
                li = this.cmbCUSTOMER_REASON_CODE.Items.FindByText(objScore.FirstFactor);

                if (li != null)
                {
                    this.cmbCUSTOMER_REASON_CODE.SelectedIndex = this.cmbCUSTOMER_REASON_CODE.Items.IndexOf(li);
                }

                //Reason code 2
                li = this.cmbCUSTOMER_REASON_CODE2.Items.FindByText(objScore.SecondFactor);

                if (li != null)
                {
                    this.cmbCUSTOMER_REASON_CODE2.SelectedIndex = this.cmbCUSTOMER_REASON_CODE2.Items.IndexOf(li);
                }

                //Reason code 3
                li = this.cmbCUSTOMER_REASON_CODE3.Items.FindByText(objScore.ThirdFactor);

                if (li != null)
                {
                    this.cmbCUSTOMER_REASON_CODE3.SelectedIndex = this.cmbCUSTOMER_REASON_CODE3.Items.IndexOf(li);
                }

                //Reason code 4
                li = this.cmbCUSTOMER_REASON_CODE4.Items.FindByText(objScore.FourthFactor);

                if (li != null)
                {
                    this.cmbCUSTOMER_REASON_CODE4.SelectedIndex = this.cmbCUSTOMER_REASON_CODE4.Items.IndexOf(li);
                }

                this.txtCUSTOMER_INSURANCE_RECEIVED_DATE.Text = DateTime.Now.ToShortDateString();

            }

            hidFormSaved.Value = "0";
            GetOldData();


        }

        private void cmbCUSTOMER_COUNTRY_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            /*if(cmbCUSTOMER_COUNTRY.Items[cmbCUSTOMER_COUNTRY.SelectedIndex].Text== "USA")
            {
                rfvCUSTOMER_ZIP.EnableClientScript = true;
                revCUSTOMER_ZIP.EnableClientScript = true;
                //cmbCUSTOMER_COUNTRY.Attributes.Add("onBlur","javascript:postcheck();");
            }
            else
            {
                cmbCUSTOMER_COUNTRY.Attributes.Add("onBlur","javascript:DisableValidators();");
                imgZipLookup.Visible=false;
            }*/

        }

        private void txtCUSTOMER_ZIP_TextChanged(object sender, System.EventArgs e)
        {

            /*if(cmbCUSTOMER_COUNTRY.Items[cmbCUSTOMER_COUNTRY.SelectedIndex].Text== "USA")
            {
                rfvCUSTOMER_ZIP.EnableClientScript = true;
                revCUSTOMER_ZIP.EnableClientScript = true;
            }
            else
            {
                txtCUSTOMER_ZIP.Attributes.Add("onBlur","javascript:DisableValidators();");
                imgZipLookup.Visible=false;
            }*/
        }
        private void FillTitles(DropDownList ddlType, string CustType)
        {
            DataSet dsTitles = this.AjaxFillTitles(CustType);
            if (dsTitles != null && dsTitles.Tables.Count > 0 && dsTitles.Tables[0].Rows.Count > 0)
            {
                ddlType.DataSource = dsTitles;
                ddlType.DataTextField = "ACTIVITY_DESC";
                ddlType.DataValueField = "ACTIVITY_ID";
                ddlType.DataBind();
                ddlType.Items.Insert(0, "");//for itrack 705
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

        #region Ajax methods

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
        public string AjaxFetchZipForState(int stateID, string ZipID)
        {
            CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();
            string result = "";
            result = obj.FetchZipForState(stateID, ZipID);
            return result;

        }  //Fill Zip codes from database using Ajax

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
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return null;
            }
        }   //fill State from database onchange country

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
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return null;
            }
        }  //Fill customer titles onchange of customer Type

        [System.Web.Services.WebMethod]
        public static String GetCustomerAddressDetailsUsingZipeCode(String ZIPCODE, String COUNTRYID)
        {

            CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();  //Declare the object ob common webservice

            String ReturnValue = String.Empty;
            DataSet ds = new DataSet();

            ReturnValue = obj.GetAddressDetailsBasedOnZipeCode(ZIPCODE, Convert.ToInt32(COUNTRYID));


            return ReturnValue;
        }

        #endregion
    }
}
