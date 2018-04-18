/******************************************************************************************
	<Author					: - > Anurag Verma	
	<Start Date				: -	> March 11,2005
	<End Date				: - >
	<Description			: - > This file is used to add agency details,show agency details, update agency details 
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - > 31/03/2005 
	<Modified By			: - > Anurag Verma
	<Purpose				: - > Removed labels and clssingleton messages,removing setting text of activate and deactivate button
    

	<Modified Date			: - > 1/04/2005 
	<Modified By			: - > Anurag Verma
	<Purpose				: - > Changes done according to review sheet issues

    <Modified Date			: - > 3/04/2005 
	<Modified By			: - > Anurag Verma
	<Purpose				: - > Changes done according to review sheet issues
    
    <Modified Date			: - > 05/04/2005    
	<Modified By			: - > Anurag Verma
	<Purpose				: - > making changes for activate/deactivate functionality and removing SCREENID and SetSecurityXML function from page
    
    <Modified Date			: - > 12/04/2005    
	<Modified By			: - > Anurag Verma
	<Purpose				: - > making changes for activate/deactivate button and changing error message for extension
	

	<Modified Date			: - > 12/05/2005    
	<Modified By			: - > Gaurav
	<Purpose				: - > Making changes to CmsButton permissions , from Execute to Write
	<Modified Date			: - > 15/07/2005    
	<Modified By			: - > Gaurav
	<Purpose				: - > New Field Added
	

	<Modified Date			: - 26/08/2005
	<Modified By			: - Anurag Verma
	<Purpose				: - Applying Null Check for buttons on aspx page
	<Modified Date			: - > 17/11/2006    
	<Modified By			: - > Pravesh
	<Purpose				: - > Add Termination date renewal and termination Notice
	<Modified Date			: - > 26/05/2008    
	<Modified By			: - > Praveen Kasana
	<Purpose				: - > Reverify EFT Fields
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
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher;
using System.IO;
using System.Xml;


namespace Cms.CmsWeb.Maintenance
{
    /// <summary>
    /// 
    /// </summary>
    public class AddAgency : Cms.CmsWeb.cmsbase
    {
        #region Page controls declaration
        protected System.Web.UI.WebControls.TextBox txtAGENCY_CODE;
        protected System.Web.UI.WebControls.TextBox txtAGENCY_DISPLAY_NAME;
        protected System.Web.UI.WebControls.TextBox txtAGENCY_LIC_NUM;
        protected System.Web.UI.WebControls.TextBox txtAGENCY_ADD1;
        protected System.Web.UI.WebControls.TextBox txtAGENCY_ADD2;
        protected System.Web.UI.WebControls.TextBox txtAGENCY_CITY;
        protected System.Web.UI.WebControls.DropDownList cmbAGENCY_STATE;
        protected System.Web.UI.WebControls.TextBox txtAGENCY_ZIP;
        protected System.Web.UI.WebControls.DropDownList cmbAGENCY_COUNTRY;
        protected System.Web.UI.WebControls.TextBox txtAGENCY_PHONE;
        protected System.Web.UI.WebControls.TextBox txtAGENCY_EXT;
        protected System.Web.UI.WebControls.TextBox txtAGENCY_FAX;
        protected System.Web.UI.WebControls.TextBox txtAGENCY_EMAIL;
        protected System.Web.UI.WebControls.TextBox txtAGENCY_WEBSITE;
        protected System.Web.UI.WebControls.TextBox txtBROKER_CPF_CNPJ;
        protected System.Web.UI.WebControls.TextBox txtDISTRICT;
        protected System.Web.UI.WebControls.TextBox txtNUMBER;
        protected System.Web.UI.WebControls.TextBox txtDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.TextBox txtBROKER_REGIONAL_ID;
        protected System.Web.UI.WebControls.TextBox txtREGIONAL_ID_ISSUANCE;
        protected System.Web.UI.WebControls.TextBox txtREGIONAL_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.TextBox txtBROKER_BANK_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtBROKER_DATE_OF_BIRTH;

        protected System.Web.UI.WebControls.DropDownList cmbAGENCY_PAYMENT_METHOD;
        protected System.Web.UI.WebControls.DropDownList cmbAGENCY_BILL_TYPE;
        protected System.Web.UI.WebControls.DropDownList cmbPROCESS_1099;
        protected System.Web.UI.WebControls.DropDownList cmbAGENCY_TYPE_ID;
        protected System.Web.UI.WebControls.DropDownList cmbBROKER_TYPE;
        protected System.Web.UI.WebControls.DropDownList cmbMARITAL_STATUS;
        protected System.Web.UI.WebControls.DropDownList cmbGENDER;
        protected System.Web.UI.WebControls.Label capAGENCY_TYPE_ID;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_CODE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_NEW;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidZIP_CodeMsg;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidtab;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCancel;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidEdit;

        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;

        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_TYPE_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_DISPLAY_NAME;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_CODE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_ADD1;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_PAYMENT_METHOD;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_BILL_TYPE;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvFEDERAL_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_LIC_NUM;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvBANK_NAME;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPROCESS_1099;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvBROKER_CURRENCY;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvBROKER_TYPE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvBROKER_CPF_CNPJ;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvBROKER_REGIONAL_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvREGIONAL_ID_ISSUANCE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvREGIONAL_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvMARITAL_STATUS;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvGENDER;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvBROKER_BANK_NUMBER;

        protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_DISPLAY_NAME;
        protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_EMAIL;
        protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_FAX;
        protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_WEBSITE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_LIC_NUM;
        protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_CITY;
        protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_ZIP;
        protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_PHONE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_EXT;
        protected System.Web.UI.WebControls.RegularExpressionValidator revBROKER_CPF_CNPJ;
        protected System.Web.UI.WebControls.CustomValidator csvDFI_ACC_NO;
        protected System.Web.UI.WebControls.CustomValidator csvROUTING_NUMBER;
        protected System.Web.UI.WebControls.CustomValidator csvVERIFY_ROUTING_NUMBER;
        protected System.Web.UI.WebControls.CustomValidator csvVERIFY_ROUTING_NUMBER_LENGHT;
        protected System.Web.UI.WebControls.RegularExpressionValidator revROUTING_NUMBER;
        protected System.Web.UI.WebControls.CustomValidator csvDFI_ACC_NO1;
        protected System.Web.UI.WebControls.CustomValidator csvROUTING_NUMBER1;
        protected System.Web.UI.WebControls.CustomValidator csvVERIFY_ROUTING_NUMBER1;
        protected System.Web.UI.WebControls.CustomValidator csvVERIFY_ROUTING_NUMBER_LENGHT1;
        protected System.Web.UI.WebControls.CustomValidator csvBROKER_CPF_CNPJ;
        protected System.Web.UI.WebControls.CustomValidator csvDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.RegularExpressionValidator revROUTING_NUMBER1;
        protected System.Web.UI.WebControls.CustomValidator csvREGIONAL_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revBANK_ACCOUNT_NUMBER;
        protected System.Web.UI.WebControls.RegularExpressionValidator revBANK_ACCOUNT_NUMBER1;
        protected System.Web.UI.WebControls.RegularExpressionValidator revFEDERAL_ID;
        protected System.Web.UI.WebControls.RegularExpressionValidator revDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.RegularExpressionValidator revREGIONAL_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.Image imgAgencyZipLookup;
        protected System.Web.UI.WebControls.Image imgAgencyMailZipLookup;
        protected System.Web.UI.WebControls.HyperLink hlkAgencyZipLookup;
        protected System.Web.UI.WebControls.HyperLink hlkAgencyMailZipLookup;

        protected System.Web.UI.WebControls.Label lblMessage;


        protected System.Web.UI.WebControls.Label capAGENCY_CODE;
        protected System.Web.UI.WebControls.Label capAGENCY_DISPLAY_NAME;
        protected System.Web.UI.WebControls.Label capAGENCY_LIC_NUM;
        protected System.Web.UI.WebControls.Label capAGENCY_ADD1;
        protected System.Web.UI.WebControls.Label capAGENCY_ADD2;
        protected System.Web.UI.WebControls.Label capAGENCY_CITY;
        protected System.Web.UI.WebControls.Label capAGENCY_STATE;
        protected System.Web.UI.WebControls.Label capAGENCY_ZIP;
        protected System.Web.UI.WebControls.Label capAGENCY_COUNTRY;
        protected System.Web.UI.WebControls.Label capAGENCY_PHONE;
        protected System.Web.UI.WebControls.Label capAGENCY_EXT;
        protected System.Web.UI.WebControls.Label capAGENCY_FAX;
        protected System.Web.UI.WebControls.Label capAGENCY_EMAIL;
        protected System.Web.UI.WebControls.Label capAGENCY_WEBSITE;
        protected System.Web.UI.WebControls.Label capAGENCY_PAYMENT_METHOD;
        protected System.Web.UI.WebControls.Label capAGENCY_BILL_TYPE;
        protected System.Web.UI.WebControls.Label capPROCESS_1099;
        protected System.Web.UI.WebControls.Label capBROKER_TYPE;
        protected System.Web.UI.WebControls.Label capBROKER_CPF_CNPJ;
        protected System.Web.UI.WebControls.Label capDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.Label capBROKER_REGIONAL_ID;
        protected System.Web.UI.WebControls.Label capREGIONAL_ID_ISSUANCE;
        protected System.Web.UI.WebControls.Label capREGIONAL_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.Label capMARITAL_STATUS;
        protected System.Web.UI.WebControls.Label capGENDER;
        protected System.Web.UI.WebControls.Label capBROKER_BANK_NUMBER;
        protected System.Web.UI.WebControls.Label capDISTRICT;
        protected System.Web.UI.WebControls.Label capNUMBER;
        protected System.Web.UI.WebControls.Label capRAD_CHK;
        protected System.Web.UI.WebControls.Label capRAD_SAV;
        protected System.Web.UI.WebControls.Label capACCOUNT;
        //Added by Swarup on 12/01/2007
        //Start
        protected System.Web.UI.WebControls.Label capALLOWS_EFT;
        protected System.Web.UI.WebControls.Label capALLOWS_CUSTOMER_SWEEP;
        protected System.Web.UI.WebControls.DropDownList cmbALLOWS_EFT;
        protected System.Web.UI.WebControls.DropDownList cmbALLOWS_CUSTOMER_SWEEP;
        protected System.Web.UI.WebControls.Label capVARIFIED1;
        protected System.Web.UI.WebControls.Label capVARIFIED2;
        protected System.Web.UI.WebControls.Label capVARIFIED_DATE1;
        protected System.Web.UI.WebControls.Label capVARIFIED_DATE2;
        protected System.Web.UI.WebControls.Label capREASON1;
        protected System.Web.UI.WebControls.Label capREASON2;
        protected System.Web.UI.WebControls.RadioButton rdbACC_CASH_ACC_TYPEO;
        protected System.Web.UI.WebControls.RadioButton rdbACC_CASH_ACC_TYPEO2;

        protected System.Web.UI.WebControls.Label capACCOUNT_TYPE;
        protected System.Web.UI.WebControls.Label capACCOUNT_TYPE_2;
        //protected System.Web.UI.WebControls.Label cap1099_PROCESS;
        protected System.Web.UI.WebControls.RadioButton rdbACC_CASH_ACC_TYPET;
        protected System.Web.UI.WebControls.RadioButton rdbACC_CASH_ACC_TYPET2;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidACCOUNT_TYPE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidACCOUNT_TYPE_2;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidREVERIFIED_AC1;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidREVERIFIED_AC2;

        //Added by pradeep kushwaha 
        protected System.Web.UI.WebControls.Label capMANDATORY_FIELD;
        protected System.Web.UI.WebControls.Label capPHYSICAL_ADDRESS;
        protected System.Web.UI.WebControls.Label capMAILING_ADDRESS;
        protected System.Web.UI.WebControls.Label capEFT_BANK_INFORMATION;
        protected System.Web.UI.WebControls.Label capACCOUNT_FOR_AGENCY_MSG;
        protected System.Web.UI.WebControls.Label capOTHER_INFORMATION;
        protected System.Web.UI.WebControls.HyperLink hlkDATE_OF_BIRTH;
        protected System.Web.UI.WebControls.HyperLink hlkREGIONAL_ID_ISSUE_DATE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidM_AGENCY_STATE; // Added by santosk kumar gautam on 27 jan 2011 itrack:458
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_STATE;

        //END
        /*protected System.Web.UI.WebControls.TextBox txtVARIFIED1;
        protected System.Web.UI.WebControls.TextBox txtVARIFIED2;
        protected System.Web.UI.WebControls.TextBox txtVARIFIED_DATE1;
        protected System.Web.UI.WebControls.TextBox txtVARIFIED_DATE2;
        protected System.Web.UI.WebControls.TextBox txtREASON1;
        protected System.Web.UI.WebControls.TextBox txtREASON2;*/
        //End
        public string strSystemID = "";
        protected string strCarrierSystemID = "";
        protected string strCommonCarrierCode = "";
        public string javasciptmsg, javasciptCPFmsg, javasciptCNPJmsg, CPF_invalid, CNPJ_invalid;
        #endregion
        #region Local form variables
        //START:*********** Local form variables *************
        string oldXML;
        //creating resource manager object (used for reading field and label mapping)
        System.Resources.ResourceManager objResourceMgr;
        private string strRowId, strFormSaved;
        //private int	intLoggedInUserID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden1;
        protected Cms.CmsWeb.Controls.CmsButton btnDelete;
        protected System.Web.UI.WebControls.Label lblDelete;
        protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
        protected System.Web.UI.WebControls.Label capPRINCIPAL_CONTACT;
        protected System.Web.UI.WebControls.TextBox txtPRINCIPAL_CONTACT;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPRINCIPAL_CONTACT;
        protected System.Web.UI.WebControls.Label capOTHER_CONTACT;
        protected System.Web.UI.WebControls.TextBox txtOTHER_CONTACT;
        protected System.Web.UI.WebControls.RegularExpressionValidator revOTHER_CONTACT;
        protected System.Web.UI.WebControls.Label capFEDERAL_ID;
        protected System.Web.UI.WebControls.TextBox txtFEDERAL_ID;
        protected System.Web.UI.WebControls.Label capBANK_ACCOUNT_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtBANK_ACCOUNT_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtROUTING_NUMBER;
        protected System.Web.UI.WebControls.Label lbl_BANK_NAME;
        protected System.Web.UI.WebControls.Label lbl_BANK_BRANCH;
        protected System.Web.UI.WebControls.Label lbl_BANK_ACCOUNT_NUMBER;
        protected System.Web.UI.WebControls.Label lbl_ROUTING_NUMBER;
        protected System.Web.UI.WebControls.Label lbl_BANK_ACCOUNT_NUMBER1;
        protected System.Web.UI.WebControls.Label lbl_ROUTING_NUMBER1;

        protected System.Web.UI.WebControls.Label capORIGINAL_CONTRACT_DATE;
        protected System.Web.UI.WebControls.Label capCURRENT_CONTRACT_DATE;
        protected System.Web.UI.WebControls.TextBox txtORIGINAL_CONTRACT_DATE;
        protected System.Web.UI.WebControls.TextBox txtCURRENT_CONTRACT_DATE;

        protected System.Web.UI.WebControls.HyperLink hlkORIGINAL_CONTRACT_DATE;
        protected System.Web.UI.WebControls.HyperLink hlkCURRENT_CONTRACT_DATE;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidUnderWriter;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revROUTING_NUMBER;
        protected System.Web.UI.WebControls.CustomValidator csvORIGINAL_CONTRACT_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revORIGINAL_CONTRACT_DATE;

        protected System.Web.UI.WebControls.CustomValidator csvCURRENT_CONTRACT_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCURRENT_CONTRACT_DATE;


        protected System.Web.UI.WebControls.Label capM_AGENCY_ADD_1;
        protected System.Web.UI.WebControls.TextBox txtM_AGENCY_ADD_1;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvM_AGENCY_ADD_1;
        protected System.Web.UI.WebControls.Label capM_AGENCY_ADD_2;
        protected System.Web.UI.WebControls.TextBox txtM_AGENCY_ADD_2;
        protected System.Web.UI.WebControls.Label capM_AGENCY_CITY;
        protected System.Web.UI.WebControls.TextBox txtM_AGENCY_CITY;
        protected System.Web.UI.WebControls.RegularExpressionValidator revM_AGENCY_CITY;
        protected System.Web.UI.WebControls.Label capM_AGENCY_COUNTRY;
        protected System.Web.UI.WebControls.DropDownList cmbM_AGENCY_COUNTRY;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvM_AGENCY_COUNTRY;
        protected System.Web.UI.WebControls.Label capM_AGENCY_STATE;
        protected System.Web.UI.WebControls.DropDownList cmbM_AGENCY_STATE;
        protected System.Web.UI.WebControls.Label capM_AGENCY_ZIP;
        protected System.Web.UI.WebControls.TextBox txtM_AGENCY_ZIP;
        protected System.Web.UI.WebControls.RegularExpressionValidator revM_AGENCY_ZIP;
        //protected System.Web.UI.WebControls.Label capM_AGENCY_PHONE;
        //protected System.Web.UI.WebControls.TextBox txtM_AGENCY_PHONE;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revM_AGENCY_PHONE;
        //protected System.Web.UI.WebControls.Label capM_AGENCY_EXT;
        //protected System.Web.UI.WebControls.TextBox txtM_AGENCY_EXT;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revM_AGENCY_EXT;
        //protected System.Web.UI.WebControls.Label capM_AGENCY_FAX;
        //protected System.Web.UI.WebControls.TextBox txtM_AGENCY_FAX;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revM_AGENCY_FAX;
        protected Cms.CmsWeb.Controls.CmsButton btnCopyPhysicalAddress;
        protected System.Web.UI.WebControls.Label lblCopy_Address;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidReset;
        protected System.Web.UI.WebControls.TextBox txtTERMINATION_DATE;
        protected System.Web.UI.WebControls.HyperLink hlkTERMINATION_DATE;
        //protected System.Web.UI.WebControls.HyperLink hlkTERMINATION_REASON;

        protected System.Web.UI.WebControls.RegularExpressionValidator revTERMINATION_DATE;
        protected System.Web.UI.HtmlControls.HtmlTableRow rowTermination;
        protected System.Web.UI.WebControls.Label capAGENCY_SPEED_DIAL;
        protected System.Web.UI.WebControls.TextBox txtAGENCY_SPEED_DIAL;
        protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_SPEED_DIAL;
        protected System.Web.UI.WebControls.Label capAGENCY_DBA;
        protected System.Web.UI.WebControls.Label capAgencyDecName;
        protected System.Web.UI.WebControls.TextBox txtAGENCY_DBA;
        protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_DBA;
        protected System.Web.UI.WebControls.Label capAGENCY_COMBINED_CODE;
        protected System.Web.UI.WebControls.TextBox txtAGENCY_COMBINED_CODE;
        protected System.Web.UI.WebControls.Label capBANK_NAME;

        protected System.Web.UI.WebControls.Label capBANK_NAME_2;
        protected System.Web.UI.WebControls.Label capBANK_BRANCH_2;


        protected System.Web.UI.WebControls.RegularExpressionValidator revBANK_NAME;
        protected System.Web.UI.WebControls.RegularExpressionValidator revBANK_NAME_2;

        protected System.Web.UI.WebControls.Label capBANK_BRANCH;


        protected System.Web.UI.WebControls.TextBox txtBANK_NAME;
        protected System.Web.UI.WebControls.TextBox txtBANK_BRANCH;

        protected System.Web.UI.WebControls.TextBox txtBANK_NAME_2;
        protected System.Web.UI.WebControls.TextBox txtBANK_BRANCH_2;

        protected System.Web.UI.WebControls.RegularExpressionValidator revBANK_BRANCH;
        protected System.Web.UI.WebControls.RegularExpressionValidator revBANK_BRANCH_2;

        protected System.Web.UI.WebControls.Label capBANK_ACCOUNT_NUMBER1;
        protected System.Web.UI.WebControls.TextBox txtBANK_ACCOUNT_NUMBER1;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revBANK_ACCOUNT_NUMBER1;
        protected System.Web.UI.WebControls.Label capROUTING_NUMBER1;
        protected System.Web.UI.WebControls.Label capROUTING_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtROUTING_NUMBER1;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revROUTING_NUMBER1;
        protected System.Web.UI.WebControls.Label capNOTES;
        protected System.Web.UI.WebControls.TextBox txtNOTES;
        protected System.Web.UI.WebControls.Label capNUM_AGENCY_CODE;
        protected System.Web.UI.WebControls.TextBox txtNUM_AGENCY_CODE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revNUM_AGENCY_CODE;
        protected System.Web.UI.WebControls.DropDownList cmbAGENCYNAME;
        protected System.Web.UI.WebControls.DropDownList cmbBROKER_CURRENCY;
        protected System.Web.UI.WebControls.Label capTERMINATION_DATE_RENEW;
        protected System.Web.UI.WebControls.Label capTERMINATION_DATE;
        protected System.Web.UI.WebControls.TextBox txtTERMINATION_DATE_RENEW;
        protected System.Web.UI.WebControls.HyperLink hlkTERMINATION_DATE_RENEW;
        protected System.Web.UI.WebControls.RegularExpressionValidator revTERMINATION_DATE_RENEW;
        protected System.Web.UI.WebControls.Label capTERMINATION_NOTICE;
        protected System.Web.UI.WebControls.Label capREVERIFIED_AC1;
        protected System.Web.UI.WebControls.Label capREVERIFIED_AC2;
        protected System.Web.UI.WebControls.DropDownList cmbTERMINATION_NOTICE;
        protected System.Web.UI.WebControls.Label capINCORPORATED_LICENSE;
        protected System.Web.UI.WebControls.DropDownList cmbINCORPORATED_LICENSE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvALLOW_EFT;

        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvBANK_BRANCH;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvBANK_ACCOUNT_NUMBER;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvROUTING_NUMBER;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvBANK_ACCOUNT_NUMBER1;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvROUTING_NUMBER1;

        protected System.Web.UI.WebControls.Label lblVARIFIED1;
        protected System.Web.UI.WebControls.Label lblVARIFIED2;
        protected System.Web.UI.WebControls.Label lblVARIFIED_DATE1;
        protected System.Web.UI.WebControls.Label lblVARIFIED_DATE2;
        protected System.Web.UI.WebControls.Label lblREASON1;
        protected System.Web.UI.WebControls.Label lblREASON2;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvALLOWS_EFT;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvBANK_ACCOUNT_NUMBER;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvROUTING_NUMBER;
        protected System.Web.UI.WebControls.Label capACCOUNT_TYPE1;
        protected System.Web.UI.WebControls.RadioButton rdbACC_CASH_ACC_TYPEO_2;
        protected System.Web.UI.WebControls.RadioButton rdbACC_CASH_ACC_TYPET_2;
        protected System.Web.UI.WebControls.Label lblACC_CASH_ACC_TYPE_2;
        protected System.Web.UI.WebControls.Label lbl_BANK_NAME_2;
        protected System.Web.UI.WebControls.Label lbl_BANK_BRANCH_2;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvBANK_ACCOUNT_NUMBER1;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvROUTING_NUMBER1;
        protected System.Web.UI.WebControls.Label lblACC_CASH_ACC_TYPE;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_EMAIL;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvTERMINATION_NOTICE;
        protected System.Web.UI.WebControls.CheckBox chkREVERIFIED_AC1;
        protected System.Web.UI.WebControls.CheckBox chkREVERIFIED_AC2;

        protected System.Web.UI.WebControls.Label capBANK_ACCOUNT_NUMBER_HID;
        protected System.Web.UI.WebControls.Label capBANK_ACCOUNT_NUMBER1_HID;
        protected System.Web.UI.WebControls.Label capFEDERAL_ID_HID;
        protected System.Web.UI.WebControls.Label capROUTING_NUMBER_HID;
        protected System.Web.UI.WebControls.Label capROUTING_NUMBER1_HID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidBANK_ACCOUNT_NUMBER;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidBANK_ACCOUNT_NUMBER1;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFEDERAL_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidROUTING_NUMBER;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidROUTING_NUMBER1;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTAB_TITLES;

        //Added by Sibin Itrack #4768

        protected System.Web.UI.WebControls.RequiredFieldValidator rfvNUM_AGENCY_CODE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_STATE;//Added for Itrack Issue 5811 on 12 May 09
        protected System.Web.UI.WebControls.CompareValidator cmpvAGENCY_CODE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_CODE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revM_AGENCY_ZIP_NUMERIC;
        protected System.Web.UI.WebControls.RegularExpressionValidator revAGENCY_ZIP_NUMERIC;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_COMBINED_CODE;
        //Added By Raghav For Special Handling.
        protected System.Web.UI.WebControls.Label capREQ_SPECIAL_HANDLING;
        protected System.Web.UI.WebControls.CheckBox chkREQ_SPECIAL_HANDLING;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidREQ_SPECIAL_HANDLING;

        //Added by Pradeep Kushwaha on 09-03-2010
        protected System.Web.UI.WebControls.Label capSUSEP_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtSUSEP_NUMBER;
        protected System.Web.UI.WebControls.Label capBROKER_CURRENCY;


        protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEW_BROKER_DATE_OF_BIRTH;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEWREGIONAL_ID_ISSUE_DATE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEW_TERMINATION_DATE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEW_TERMINATION_DATE_RENEW;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEW_ORIGINAL_CONTRACT_DATE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEW_CURRENT_CONTRACT_DATE;
        protected System.Web.UI.WebControls.CompareValidator cpvREGIONAL_ID_ISSUE_DATE;
        protected System.Web.UI.WebControls.CompareValidator cpv2REGIONAL_ID_ISSUE_DATE;
        //end

        //Defining the business layer class object
        ClsAgency objAgency;
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
            //Added by Sibin on 22-09-2008
            rfvAGENCY_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1031");
            rfvNUM_AGENCY_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1032");
            rfvAGENCY_COMBINED_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1033");
            cmpvAGENCY_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1034");
            rfvAGENCY_DISPLAY_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "65");
            rfvAGENCY_STATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "35");//Added for Itrack Issue 5811 on 12 May 09

            //rfvAGENCY_CODE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"38");
            //rfvAGENCY_ADD1.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"32");
            //rfvAGENCY_COUNTRY.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"33");
            rfvAGENCY_PAYMENT_METHOD.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "78");
            rfvAGENCY_BILL_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "79");
            rfvAGENCY_LIC_NUM.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "80");
            //rfvAGENCY_EMAIL.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"975");
            //rfvFEDERAL_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"976");
            rfvPROCESS_1099.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "977");
            rfvBROKER_CURRENCY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1170");
            revAGENCY_DISPLAY_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
            revAGENCY_DBA.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
            revAGENCY_EMAIL.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("124");
            //revAGENCY_FAX.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("15");
            revAGENCY_WEBSITE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("737");
            revAGENCY_LIC_NUM.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "80");
            revAGENCY_CITY.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
            rfvAGENCY_TYPE_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1189");
            cpvREGIONAL_ID_ISSUE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1402");
            cpv2REGIONAL_ID_ISSUE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1403");
            /*==========================================================
            * SANTOSH GAUTAM : ValidationExpression, ErrorMessage PROPERTIES OF revAGENCY_ZIP, revAGENCY_PHONE, revAGENCY_EXT MODIFIED ON 28 OCT 2010
            * 1. OLD VALUE => BELOW COMMENTED
            *==========================================================*/
            //revAGENCY_ZIP.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("24");
            //revAGENCY_ZIP.ValidationExpression = aRegExpZip;
            //set Zip,Phone No,Fax,Mobile No as per Brazil Format
            if (GetLanguageID() == "2") //Changes done by Aditya for TFS BUG # 1832
            {
                revAGENCY_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1084");
            }
            else
                revAGENCY_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("24");


            if (GetLanguageID() == "2")   //Added by Aditya for TFS BUG # 1832
            {
                revAGENCY_ZIP.ValidationExpression = aRegExpZipBrazil;//aRegExpZip;
            }
            else
                revAGENCY_ZIP.ValidationExpression = aRegExpZipUS;

            //revAGENCY_PHONE.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");  //commented by Aditya for TFS BUG # 1832
            //revAGENCY_PHONE.ValidationExpression = aRegExpPhone;  //commented by Aditya for TFS BUG # 1832
            revAGENCY_PHONE.ValidationExpression = aRegExpAgencyPhone;
            if (GetLanguageID() == "1")
            {
                revAGENCY_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083");
                revAGENCY_EXT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2000");
                revAGENCY_FAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1085");
            }

            else
            {
                revAGENCY_PHONE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1083");
                revAGENCY_EXT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2000");
                revAGENCY_FAX.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1921");
            }

            //revAGENCY_EXT.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
            //revAGENCY_EXT.ValidationExpression = aRegExpPhone;
            revAGENCY_EXT.ValidationExpression = aRegExpAgencyPhone;



            revAGENCY_SPEED_DIAL.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");

            revAGENCY_DISPLAY_NAME.ValidationExpression = aRegExpClientName;
            revAGENCY_DBA.ValidationExpression = aRegExpClientName;
            revAGENCY_EMAIL.ValidationExpression = aRegExpEmail;


            // revAGENCY_FAX.ValidationExpression = aRegExpPhoneBrazil;

            revAGENCY_FAX.ValidationExpression = aRegExpAgencyPhone;
            //revAGENCY_WEBSITE.ValidationExpression=aRegExpSiteUrlWithoutHttp;
            revAGENCY_WEBSITE.ValidationExpression = aRegExpWebSiteUrl;

            revAGENCY_LIC_NUM.ValidationExpression = aRegExpInteger;
            revAGENCY_CITY.ValidationExpression = aRegExpClientName;



            revAGENCY_SPEED_DIAL.ValidationExpression = aRegExpInteger;

            revBANK_ACCOUNT_NUMBER.ValidationExpression = aRegExpBankAccountNumber; //Changed by Aditya or TFS BUG # 2246
            revBANK_ACCOUNT_NUMBER1.ValidationExpression = aRegExpBankAccountNumber; //Changed by Aditya or TFS BUG # 2688

            //Comment
            revFEDERAL_ID.ValidationExpression = aRegExpFederalID;//aRegExpClientName;
            //End Comment 

            revPRINCIPAL_CONTACT.ValidationExpression = aRegExpClientName;
            revOTHER_CONTACT.ValidationExpression = aRegExpClientName;
            //revROUTING_NUMBER.ValidationExpression	=	aRegExpClientName;
            //revROUTING_NUMBER1.ValidationExpression	=	aRegExpClientName;
            revBANK_BRANCH.ValidationExpression = aRegExpBankAccountNumber;  //Changed by Aditya or TFS BUG # 2246
            revBANK_NAME.ValidationExpression = aRegExpClientName;

            revBANK_BRANCH_2.ValidationExpression = aRegExpBankAccountNumber; //Changed by Aditya or TFS BUG # 2246
            revBANK_NAME_2.ValidationExpression = aRegExpClientName;

            revNUM_AGENCY_CODE.ValidationExpression = aRegExpInteger;

            revFEDERAL_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("434");
            revBANK_ACCOUNT_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2112");
            revBANK_ACCOUNT_NUMBER1.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2112");

            revBANK_BRANCH.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("903");
            revBANK_NAME.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("63");

            revBANK_BRANCH_2.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("903");
            revBANK_NAME_2.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("63");

            revPRINCIPAL_CONTACT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("435");
            revOTHER_CONTACT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("436");
            //revROUTING_NUMBER.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("437");
            //revROUTING_NUMBER1.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("437");

            revORIGINAL_CONTRACT_DATE.ValidationExpression = aRegExpDate;
            revORIGINAL_CONTRACT_DATE.ErrorMessage = "<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            csvORIGINAL_CONTRACT_DATE.ErrorMessage = "<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("510");

            revCURRENT_CONTRACT_DATE.ValidationExpression = aRegExpDate;
            revCURRENT_CONTRACT_DATE.ErrorMessage = "<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            csvCURRENT_CONTRACT_DATE.ErrorMessage = "<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("510");

            //rfvM_AGENCY_ADD_1.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"32");
            rfvM_AGENCY_COUNTRY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "33");
            //revM_AGENCY_FAX.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("15");
            revM_AGENCY_CITY.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("13");
            //revM_AGENCY_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1181");
            revNUM_AGENCY_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("467");
            /*
            revM_AGENCY_PHONE.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("14");
            revM_AGENCY_EXT.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("25");
            revM_AGENCY_FAX.ValidationExpression=aRegExpFax;
			
            revM_AGENCY_PHONE.ValidationExpression=aRegExpPhone;  
            revM_AGENCY_EXT.ValidationExpression=aRegExpExtn; */
            revM_AGENCY_CITY.ValidationExpression = aRegExpClientName;
            if (GetLanguageID() == "2")  //Added by Aditya for TFS BUG # 1832
            {
                revM_AGENCY_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1084");
            }
            else
                revM_AGENCY_ZIP.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("24");


            if (GetLanguageID() == "2")
            {
                revM_AGENCY_ZIP.ValidationExpression = aRegExpZipBrazil;
            }
            else
                revM_AGENCY_ZIP.ValidationExpression = aRegExpZipUS;

            //revM_AGENCY_ZIP.ValidationExpression=aRegExpZip;  //commented by Aditya for TFS BUG # 1832
            // Added by Mohit on 2/11/2005
            revTERMINATION_DATE.ValidationExpression = aRegExpDate;
            revTERMINATION_DATE.ValidationExpression = aRegExpDate;
            revTERMINATION_DATE.ErrorMessage = "<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            revTERMINATION_DATE_RENEW.ValidationExpression = aRegExpDate;
            revTERMINATION_DATE_RENEW.ErrorMessage = "<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            //csvTERMINATION_DATE.ErrorMessage				= "<br>" +  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("525");

            //			revTERMINATION_REASON.ValidationExpression=aRegExpDate;
            //			revTERMINATION_REASON.ValidationExpression		= aRegExpDate;
            //			revTERMINATION_REASON.ErrorMessage              = "<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            //			
            // -------------End
            //Changes By kuldeep to Select Messages on the basis  of Screen ID
            rfvBANK_ACCOUNT_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2112");
            rfvROUTING_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("971");
            rfvBANK_ACCOUNT_NUMBER1.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2112");
            rfvROUTING_NUMBER1.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("973");
            rfvBROKER_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1202");
            rfvBROKER_CPF_CNPJ.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2089");
            rfvDATE_OF_BIRTH.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1255");
            rfvBROKER_REGIONAL_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1256");
            rfvREGIONAL_ID_ISSUANCE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1257");
            rfvREGIONAL_ID_ISSUE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1258");
            rfvMARITAL_STATUS.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1259");
            rfvGENDER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1260");
            rfvBROKER_BANK_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1261");
            revBROKER_CPF_CNPJ.Attributes.Add("ErrMsgcpf", Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "2090"));
            revBROKER_CPF_CNPJ.Attributes.Add("ErrMsgcnpj", Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "2090"));
            revBROKER_CPF_CNPJ.ValidationExpression = aRegExpCpf_Cnpj;
            revDATE_OF_BIRTH.ValidationExpression = aRegExpDate;
            revDATE_OF_BIRTH.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            //csvDATE_OF_BIRTH.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "162");
            csvDATE_OF_BIRTH.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("198");
            revREGIONAL_ID_ISSUE_DATE.ValidationExpression = aRegExpDate;
            revREGIONAL_ID_ISSUE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            rfvM_AGENCY_ADD_1.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1"); //sneha
            rfvAGENCY_ADD1.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1"); //sneha
            rfvALLOWS_EFT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
            rfvTERMINATION_NOTICE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1360");
            hidZIP_CodeMsg.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1373");
            csvDFI_ACC_NO1.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1849");
            csvROUTING_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1850");
            csvVERIFY_ROUTING_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1851");
            csvVERIFY_ROUTING_NUMBER_LENGHT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1852");
            revROUTING_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1853");
            csvDFI_ACC_NO1.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1849");
            csvROUTING_NUMBER1.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1850");
            csvVERIFY_ROUTING_NUMBER1.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1851");
            csvVERIFY_ROUTING_NUMBER_LENGHT1.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1852");
            revROUTING_NUMBER1.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1853");
            csvDFI_ACC_NO.ErrorMessage = ClsMessages.FetchGeneralMessage("1914");
            revAGENCY_ZIP_NUMERIC.ValidationExpression = aRegExpAlphaNumWithDash;
            revAGENCY_ZIP_NUMERIC.ErrorMessage = ClsMessages.FetchGeneralMessage("467");
            revM_AGENCY_ZIP_NUMERIC.ValidationExpression = aRegExpAlphaNumWithDash;
            revM_AGENCY_ZIP_NUMERIC.ErrorMessage = ClsMessages.FetchGeneralMessage("467");
            csvREGIONAL_ID_ISSUE_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("2088");
        }

        #endregion

        #region PageLoad event
        private void Page_Load(object sender, System.EventArgs e)
        {
            Page.DataBind();
            Ajax.Utility.RegisterTypeForAjax(typeof(AddAgency));
            strCommonCarrierCode = GetSystemId().ToString().ToUpper();

            //cmbAGENCY_COUNTRY.SelectedIndex=int.Parse(aCountry);
            //txtAGENCY_DISPLAY_NAME.Attributes.Add("onblur","javascript:generateCode();");  
            // itrack 458 praveer panghal       
            //txtAGENCY_PHONE.Attributes.Add("onBlur","javascript:DisableExt('txtAGENCY_PHONE','txtAGENCY_EXT');");           
            revROUTING_NUMBER.ValidationExpression = aRegExpDoublePositiveNonZeroStartWithZero;
            revROUTING_NUMBER1.ValidationExpression = aRegExpDoublePositiveNonZeroStartWithZero;
            //txtROUTING_NUMBER.Attributes.Add("Onblur","javascript:CalculateCheckDigit(this);");
            //txtROUTING_NUMBER1.Attributes.Add("Onblur","javascript:CalculateCheckDigit(this);");

            btnReset.Attributes.Add("onclick", "javascript:return ResetAgency();");
            //btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");


            //Added by Ruchika on 12-Jan-2012
            if ((GetSystemId().ToString().ToUpper() != "S001") && (GetSystemId().ToString().ToUpper() != "SUAT"))
            {
                btnCopyPhysicalAddress.Attributes.Add("onclick", "javascript:return CopyPhysicalAddress();");
            }

            if ((GetSystemId().ToString().ToUpper() != "S001") || (GetSystemId().ToString().ToUpper() != "SUAT"))
            {
                btnCopyPhysicalAddress.Visible = false;
                lblCopy_Address.Visible = false;
            }
            //Added till here


            hlkORIGINAL_CONTRACT_DATE.Attributes.Add("OnClick", "fPopCalendar(document.MNT_AGENCY_LIST.txtORIGINAL_CONTRACT_DATE,document.MNT_AGENCY_LIST.txtORIGINAL_CONTRACT_DATE)"); //Javascript Implementation for Calender				
            hlkCURRENT_CONTRACT_DATE.Attributes.Add("OnClick", "fPopCalendar(document.MNT_AGENCY_LIST.txtCURRENT_CONTRACT_DATE,document.MNT_AGENCY_LIST.txtCURRENT_CONTRACT_DATE)"); //Javascript Implementation for Calender				
            // Added by mohit on 2/11/2005

            hlkTERMINATION_DATE.Attributes.Add("OnClick", "fPopCalendar(document.MNT_AGENCY_LIST.txtTERMINATION_DATE,document.MNT_AGENCY_LIST.txtTERMINATION_DATE)");
            hlkTERMINATION_DATE_RENEW.Attributes.Add("OnClick", "fPopCalendar(document.MNT_AGENCY_LIST.txtTERMINATION_DATE_RENEW,document.MNT_AGENCY_LIST.txtTERMINATION_DATE_RENEW)");
            hlkREGIONAL_ID_ISSUE_DATE.Attributes.Add("OnClick", "fPopCalendar(document.MNT_AGENCY_LIST.txtREGIONAL_ID_ISSUE_DATE,document.MNT_AGENCY_LIST.txtREGIONAL_ID_ISSUE_DATE)"); //Javascript Implementation for Calender				
            hlkDATE_OF_BIRTH.Attributes.Add("OnClick", "fPopCalendar(document.MNT_AGENCY_LIST.txtBROKER_DATE_OF_BIRTH,document.MNT_AGENCY_LIST.txtBROKER_DATE_OF_BIRTH)");
            txtM_AGENCY_ADD_1.Attributes.Add("onblur", "javascript:EnableDisableRfv()");

            //   end
            //	hlkTERMINATION_REASON.Attributes.Add("OnClick","fPopCalendar(document.MNT_AGENCY_LIST.txtTERMINATION_REASON,document.MNT_AGENCY_LIST.txtTERMINATION_REASON)");
            //btnActivateDeactivate.Attributes.Add("onclick","javascript:document.MNT_AGENCY_LIST.reset();");
            //  this.btnSave.Attributes.Add("onClick","javascript:CountUnderWriter();");
            // phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
            base.ScreenId = "10_0";
            Page.DataBind();
            lblMessage.Visible = false;
            capMANDATORY_FIELD.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
            hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCon");
            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;


            btnDelete.CmsButtonClass = CmsButtonType.Delete;
            btnDelete.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;


            btnCopyPhysicalAddress.CmsButtonClass = CmsButtonType.Write;
            btnCopyPhysicalAddress.PermissionString = gstrSecurityXML;
            javasciptmsg = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "1269");
            javasciptCPFmsg = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "1263");
            javasciptCNPJmsg = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "1262");
            CPF_invalid = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "1264");
            CNPJ_invalid = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "1206");
            hidtab.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1959");
            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************

            //Added by praeep kushwaha on 12/03/2010 for Langualge Culture
            //base.SetCultureThread(base.GetLanguageCode());
            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddAgency", System.Reflection.Assembly.GetExecutingAssembly());
            ReadOnlyFields();
            if (cmbAGENCY_COUNTRY.SelectedValue != "" && hidAGENCY_STATE.Value != "")
            {
                FillStates(cmbAGENCY_STATE, int.Parse(cmbAGENCY_COUNTRY.SelectedValue.ToString()));
                if ((!string.IsNullOrEmpty(hidAGENCY_STATE.Value)) && hidAGENCY_STATE.Value != "0")
                {
                    cmbAGENCY_STATE.SelectedValue = hidAGENCY_STATE.Value;
                }
            }
            if (cmbM_AGENCY_COUNTRY.SelectedValue != "" && hidM_AGENCY_STATE.Value != "")
            {
                FillStates(cmbM_AGENCY_STATE, int.Parse(cmbM_AGENCY_COUNTRY.SelectedValue.ToString()));
                cmbM_AGENCY_STATE.SelectedValue = hidM_AGENCY_STATE.Value;
            }
            if (hidAGENCY_ID.Value == "New")
            {
                btnDelete.Style.Add("display", "none");
                btnActivateDeactivate.Enabled = false;                                
            }

           

            if (!Page.IsPostBack)
            {
                SetErrorMessages();
                imgAgencyZipLookup.Attributes.Add("style", "cursor:hand");
                cmbBROKER_TYPE.Attributes.Add("onChange", "javascript:OnBrokerTypeChange();");
                base.VerifyAddress(hlkAgencyZipLookup, txtAGENCY_ADD1, txtAGENCY_ADD2
                    , txtAGENCY_CITY, cmbAGENCY_STATE, txtAGENCY_ZIP);

                imgAgencyMailZipLookup.Attributes.Add("style", "cursor:hand");

                base.VerifyAddress(hlkAgencyMailZipLookup, txtM_AGENCY_ADD_1, txtM_AGENCY_ADD_2
                    , txtM_AGENCY_CITY, cmbM_AGENCY_STATE, txtM_AGENCY_ZIP);
                int intAgencyID = ClsAgency.GetAgencyIDFromCode(GetSystemId());
                FillUnderWriter(intAgencyID);
                SetDropdownList();
                SetCaptions();

                if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "CmsWeb/support/PageXML/" + GetSystemId(), "AddAgency.xml"))
                {
                    setPageControls(Page, @Request.PhysicalApplicationPath + "/CmsWeb/support/PageXML/" + GetSystemId() + "/AddAgency.xml");
                }

                if (Request.QueryString["AGENCY_ID"] != null)
                {
                    hidAGENCY_CODE.Value = ClsAgency.GetAgencyCodeFromID(int.Parse(Request.QueryString["AGENCY_ID"]));
                    // Added by mohit
                    // Commented by mohit on 28/10/2005
                    //hidAGENCY_ID.Value=Request.QueryString["AGENCY_ID"].ToString();

                    GenerateXML(Request.QueryString["AGENCY_ID"].ToString());
                }
                else if (Request.QueryString["NEW_AGENCY_ID"] != null && Request.QueryString["NEW_AGENCY_ID"].ToString().ToUpper() != "NEW")
                {
                    hidAGENCY_CODE.Value = ClsAgency.GetAgencyCodeFromID(int.Parse(Request.QueryString["NEW_AGENCY_ID"]));
                    // Added by mohit
                    // Commented by mohit on 28/10/2005
                    //hidAGENCY_ID.Value=Request.QueryString["AGENCY_ID"].ToString();
                    GenerateXML(Request.QueryString["NEW_AGENCY_ID"].ToString());
                }

                // ReadOnlyFields();

                /*if(Request.QueryString["AGENCY_ID"]!=null && Request.QueryString["AGENCY_ID"].ToString().Length>0)
                {
                    SetXml(Request.QueryString["AGENCY_ID"]);
                    if(hidOldData.Value.Length > 0)
                        hidACCOUNT_TYPE.Value = GetEFTInfo(hidOldData.Value);
//					strPageMode = "Edit";
                }
                else if(hidAGENCY_ID.Value != null && hidAGENCY_ID.Value.ToString().Length > 0)
                {
                    SetXml(hidVENDOR_ID.Value.ToString());
                    if(hidOldData.Value.Length > 0)
                        hidACCOUNT_TYPE.Value = GetEFTInfo(hidOldData.Value);
//					strPageMode = "Edit";
                }*/
                //Populating DropdownList


                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(ClsCommon.FetchValueFromXML("IS_ACTIVE", hidOldData.Value.ToString()));
                SetAgencyId(Request.QueryString["AGENCY_ID"]);

            }

            //Added for TFS Bug # 3108
            if ((hidAGENCY_ID.Value == "0") && (hidFormSaved.Value == "0"))
            {
                string s = ClsAgency.GetAgencyCode();
                txtAGENCY_CODE.Text = s;
            }

        }//end pageload
        #endregion

        /*	private void SetXml(string strVendorId)
			{
				hidOldData.Value = ClsAgency.GetXmlForPageControls(strVendorId);
			}*/

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

        private void FillUnderWriter(int AgencyID)
        {
            try
            {
                //Populating the producer
                //				Cms.BusinessLayer.BlCommon.ClsUser objUser = new Cms.BusinessLayer.BlCommon.ClsUser();
                //				DataTable dt = objUser.FillUser("UWT",AgencyID).Tables[0];
                //				lstUNDERWRITER_ASSIGNED_AGENCY.DataSource = dt;
                //				lstUNDERWRITER_ASSIGNED_AGENCY.DataTextField = "USER_NAME";
                //				lstUNDERWRITER_ASSIGNED_AGENCY.DataValueField = "USER_ID";
                //				lstUNDERWRITER_ASSIGNED_AGENCY.DataBind();
                //				lstUNDERWRITER_ASSIGNED_AGENCY.Items.Insert(0,new System.Web.UI.WebControls.ListItem("",""));

            }
            catch (Exception exc)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
            }

        }
        /// <summary>
        /// fetching data based on query string values
        /// </summary>
        private void GenerateXML(string agencyID)
        {
            string strAgencyID = agencyID;

            objAgency = new ClsAgency();


            if (strAgencyID != "" && strAgencyID != null)
            {
                try
                {
                    DataSet ds = new DataSet();
                    ds = objAgency.FetchData(int.Parse(strAgencyID));
                    DataTable dt = ds.Tables[0];
                    //hidOldData.Value=ds.GetXml(); 
                    hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(ds.Tables[0]);

                    if (hidOldData.Value.IndexOf("NewDataSet") >= 0)
                    {
                        XmlDocument objxml = new XmlDocument();

                        objxml.LoadXml(hidOldData.Value);

                        XmlNode node = objxml.SelectSingleNode("NewDataSet");
                        foreach (XmlNode nodes in node.SelectNodes("Table"))
                        {
                            XmlNode noder1 = nodes.SelectSingleNode("FEDERAL_ID");
                            //noder1.InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder1.InnerText);

                            hidFEDERAL_ID.Value = noder1.InnerText;
                            string strFEDERAL_ID_HID = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder1.InnerText);

                            string strvaln = "";
                            if (strFEDERAL_ID_HID.Trim() != "")//Done for Itrack Issue 6063 on 7 July 09
                            {
                                for (int len = 0; len < strFEDERAL_ID_HID.Length - 4; len++)
                                    strvaln += "x";
                                strvaln += strFEDERAL_ID_HID.Substring(strvaln.Length, strFEDERAL_ID_HID.Length - strvaln.Length);
                                capFEDERAL_ID_HID.Text = strvaln;

                            }
                            else
                                capFEDERAL_ID_HID.Text = "";

                            XmlNode noder2 = nodes.SelectSingleNode("BANK_ACCOUNT_NUMBER");
                            //noder2.InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder2.InnerText);
                            hidBANK_ACCOUNT_NUMBER.Value = noder2.InnerText;
                            string strBANK_ACCOUNT_NUMBER_HID = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder2.InnerText);
                            // To show encripted data on screen
                            strvaln = "";
                            if (strBANK_ACCOUNT_NUMBER_HID.Trim() != "")//Done for Itrack Issue 6063 on 7 July 09
                            {
                                for (int len = 0; len < strBANK_ACCOUNT_NUMBER_HID.Length - 4; len++)
                                    strvaln += "x";
                                strvaln += strBANK_ACCOUNT_NUMBER_HID.Substring(strvaln.Length, strBANK_ACCOUNT_NUMBER_HID.Length - strvaln.Length);
                                capBANK_ACCOUNT_NUMBER_HID.Text = strvaln;

                            }
                            else
                                capBANK_ACCOUNT_NUMBER_HID.Text = "";

                            strvaln = "";
                            XmlNode noder3 = nodes.SelectSingleNode("BANK_ACCOUNT_NUMBER1");
                            //noder3.InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder3.InnerText);
                            hidBANK_ACCOUNT_NUMBER1.Value = noder3.InnerText;
                            string strBANK_ACCOUNT_NUMBER1_HID = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder3.InnerText);

                            if (strBANK_ACCOUNT_NUMBER1_HID.Trim() != "")//Done for Itrack Issue 6063 on 7 July 09
                            {
                                for (int len = 0; len < strBANK_ACCOUNT_NUMBER1_HID.Length - 4; len++)
                                    strvaln += "x";
                                strvaln += strBANK_ACCOUNT_NUMBER1_HID.Substring(strvaln.Length, strBANK_ACCOUNT_NUMBER1_HID.Length - strvaln.Length);
                                capBANK_ACCOUNT_NUMBER1_HID.Text = strvaln;

                            }
                            else
                                capBANK_ACCOUNT_NUMBER1_HID.Text = "";

                            strvaln = "";
                            XmlNode noder4 = nodes.SelectSingleNode("ROUTING_NUMBER");
                            //noder4.InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder4.InnerText);
                            // To show encripted data on screen
                            hidROUTING_NUMBER.Value = noder4.InnerText;
                            string strROUTING_NUMBER_HID = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder4.InnerText);
                            if (strROUTING_NUMBER_HID.Trim() != "")//Done for Itrack Issue 6063 on 7 July 09
                            {
                                for (int len = 0; len < strROUTING_NUMBER_HID.Length - 4; len++)
                                    strvaln += "x";
                                strvaln += strROUTING_NUMBER_HID.Substring(strvaln.Length, strROUTING_NUMBER_HID.Length - strvaln.Length);
                                capROUTING_NUMBER_HID.Text = strvaln;

                            }
                            else
                                capROUTING_NUMBER_HID.Text = "";

                            strvaln = "";
                            XmlNode noder5 = nodes.SelectSingleNode("ROUTING_NUMBER1");
                            //noder5.InnerText = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder5.InnerText);
                            // To show encripted data on screen
                            hidROUTING_NUMBER1.Value = noder5.InnerText;
                            string strROUTING_NUMBER1_HID = Cms.BusinessLayer.BlCommon.ClsCommon.DecryptString(noder5.InnerText);
                            if (strROUTING_NUMBER1_HID.Trim() != "")//Done for Itrack Issue 6063 on 7 July 09
                            {
                                for (int len = 0; len < strROUTING_NUMBER1_HID.Length - 4; len++)
                                    strvaln += "x";
                                strvaln += strROUTING_NUMBER1_HID.Substring(strvaln.Length, strROUTING_NUMBER1_HID.Length - strvaln.Length);
                                capROUTING_NUMBER1_HID.Text = strvaln;

                            }
                            else
                                capROUTING_NUMBER1_HID.Text = "";
                        }

                        //hidOldData.Value = objxml.OuterXml;

                        objxml = null;
                    }

                    //hidACCOUNT_TYPE.Value = GetEFTInfo(hidOldData.Value);
                    hidACCOUNT_TYPE.Value = ClsCommon.FetchValueFromXML("ACCOUNT_TYPE", hidOldData.Value.ToString());
                    hidACCOUNT_TYPE_2.Value = ClsCommon.FetchValueFromXML("ACCOUNT_TYPE_2", hidOldData.Value.ToString());


                    hidREVERIFIED_AC1.Value = ClsCommon.FetchValueFromXML("REVERIFIED_AC1", hidOldData.Value);
                    hidREVERIFIED_AC2.Value = ClsCommon.FetchValueFromXML("REVERIFIED_AC2", hidOldData.Value);

                    lblVARIFIED1.Text = ClsCommon.FetchValueFromXML("ACCOUNT_ISVERIFIED1", hidOldData.Value);
                    lblVARIFIED2.Text = ClsCommon.FetchValueFromXML("ACCOUNT_ISVERIFIED2", hidOldData.Value);

                    lblVARIFIED_DATE1.Text = ClsCommon.FetchValueFromXML("ACCOUNT_VERIFIED_DATE1", hidOldData.Value);
                    lblVARIFIED_DATE2.Text = ClsCommon.FetchValueFromXML("ACCOUNT_VERIFIED_DATE2", hidOldData.Value);
                    if (dt.Rows[0]["BROKER_DATE_OF_BIRTH"] != System.DBNull.Value)
                    {
                        DateTime DOB = ConvertToDate(dt.Rows[0]["BROKER_DATE_OF_BIRTH"].ToString());
                        //  this.txtBROKER_DATE_OF_BIRTH.Text = ConvertToDateCulture(DOB);
                        this.hidNEW_BROKER_DATE_OF_BIRTH.Value = ConvertToDateCulture(DOB);

                        //hidOldData.Value = hidOldData.Value.Replace("<BROKER_DATE_OF_BIRTH","")

                    }
                    if (dt.Rows[0]["REGIONAL_ID_ISSUE_DATE"] != System.DBNull.Value)
                    {
                        DateTime RID = Convert.ToDateTime(dt.Rows[0]["REGIONAL_ID_ISSUE_DATE"]);
                        this.hidNEWREGIONAL_ID_ISSUE_DATE.Value = ConvertToDateCulture(RID);
                        // this.txtREGIONAL_ID_ISSUE_DATE.Text = RID.ToShortDateString();
                    }
                    if (dt.Rows[0]["TERMINATION_DATE"] != System.DBNull.Value)
                    {
                        DateTime TD = Convert.ToDateTime(dt.Rows[0]["TERMINATION_DATE"]);
                        this.hidNEW_TERMINATION_DATE.Value = ConvertToDateCulture(TD);
                        // this.txtTERMINATION_DATE.Text = TD.ToShortDateString();
                    }
                    if (dt.Rows[0]["TERMINATION_DATE_RENEW"] != System.DBNull.Value)
                    {
                        DateTime TDR = Convert.ToDateTime(dt.Rows[0]["TERMINATION_DATE_RENEW"]);
                        this.hidNEW_TERMINATION_DATE_RENEW.Value = ConvertToDateCulture(TDR);
                        // this.txtTERMINATION_DATE_RENEW.Text = TDR.ToShortDateString();
                    }

                    if (dt.Rows[0]["ORIGINAL_CONTRACT_DATE"] != System.DBNull.Value)
                    {
                        DateTime OCD = Convert.ToDateTime(dt.Rows[0]["ORIGINAL_CONTRACT_DATE"]);
                        this.hidNEW_ORIGINAL_CONTRACT_DATE.Value = ConvertToDateCulture(OCD);

                    }
                    if (dt.Rows[0]["CURRENT_CONTRACT_DATE"] != System.DBNull.Value)
                    {
                        DateTime CCD = Convert.ToDateTime(dt.Rows[0]["CURRENT_CONTRACT_DATE"]);
                        this.hidNEW_CURRENT_CONTRACT_DATE.Value = ConvertToDateCulture(CCD);

                    }

                    if (dt.Rows[0]["AGENCY_COUNTRY"] != System.DBNull.Value)
                        FillStates(cmbAGENCY_STATE, int.Parse(dt.Rows[0]["AGENCY_COUNTRY"].ToString()));
                    if (dt.Rows[0]["M_AGENCY_COUNTRY"] != System.DBNull.Value)
                        FillStates(cmbM_AGENCY_STATE, int.Parse(dt.Rows[0]["M_AGENCY_COUNTRY"].ToString()));

                    //hidFormSaved.Value="1"; 
                }
                catch (Exception ex)
                {
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                    lblMessage.Visible = true;
                    Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                    hidFormSaved.Value = "2";

                }
                finally
                {
                    if (objAgency != null)
                        objAgency.Dispose();
                }

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
        private string GetEFTInfo(string oldxml)
        {
            DataSet dsEFTInfo = new DataSet();
            try
            {
                //Create a StringReader to hold the XML
                StringReader xmlStrReader = new StringReader(oldxml);

                //Stuff XML into a DataSet
                dsEFTInfo.ReadXml(xmlStrReader);

                if (dsEFTInfo.Tables.Count > 0 && dsEFTInfo.Tables[0].Rows.Count > 0)
                    return dsEFTInfo.Tables[0].Rows[0]["ACCOUNT_TYPE"].ToString();
                else
                    return "";
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return "";
            }

        }

        /// <summary>
        /// This function will populate dropdown list
        /// </summary>
        private void SetDropdownList()
        {
            //ListItem agencyLI	= new ListItem("Agency Name","0");
            //ListItem agencyLI1	= new ListItem("DBA Name","1");

            //cmbAGENCYNAME.Items.Insert(0,agencyLI);
            //cmbAGENCYNAME.Items.Insert(1,agencyLI1);
            //cmbAGENCYNAME.Items[0].Value="0";
            //cmbAGENCYNAME.Items[1].Value="1";
            //cmbAGENCYNAME.SelectedIndex =0;


            cmbAGENCYNAME.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("AGname");
            cmbAGENCYNAME.DataTextField = "LookupDesc";
            cmbAGENCYNAME.DataValueField = "LookupCode";
            cmbAGENCYNAME.DataBind();
            cmbAGENCYNAME.Items.Insert(0, "");

            // AGENCY_PAYMENT_METHOD. will be NET(0) in all cases. Gross option has been removed
            //list item for payment method dropdown
            //            ListItem li         = new ListItem("Net","0");
            //            ListItem li1        = new ListItem("Gross","1");
            // 
            //            cmbAGENCY_PAYMENT_METHOD.Items.Insert(0,li);
            //            cmbAGENCY_PAYMENT_METHOD.Items.Insert(1,li1);  			
            //            cmbAGENCY_PAYMENT_METHOD.Items[0].Value="0";  
            //            cmbAGENCY_PAYMENT_METHOD.Items[1].Value="1"; 
            //            cmbAGENCY_PAYMENT_METHOD.SelectedIndex =0;
            //			cmbAGENCY_PAYMENT_METHOD.SelectedValue="0";
            cmbALLOWS_EFT.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
            cmbALLOWS_EFT.DataTextField = "LookupDesc";
            cmbALLOWS_EFT.DataValueField = "LookupID";
            cmbALLOWS_EFT.DataBind();
            //commented by Swarup as Itrack issue #1915
            //cmbALLOWS_EFT.Items.Insert(0,"");

            //Itrack #4098
            cmbALLOWS_CUSTOMER_SWEEP.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
            cmbALLOWS_CUSTOMER_SWEEP.DataTextField = "LookupDesc";
            cmbALLOWS_CUSTOMER_SWEEP.DataValueField = "LookupID";
            cmbALLOWS_CUSTOMER_SWEEP.DataBind();


            cmbINCORPORATED_LICENSE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
            cmbINCORPORATED_LICENSE.DataTextField = "LookupDesc";
            cmbINCORPORATED_LICENSE.DataValueField = "LookupID";
            cmbINCORPORATED_LICENSE.DataBind();


            cmbTERMINATION_NOTICE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
            cmbTERMINATION_NOTICE.DataTextField = "LookupDesc";
            cmbTERMINATION_NOTICE.DataValueField = "LookupID";
            cmbTERMINATION_NOTICE.DataBind();







            /*int VarInsuredBill = BillType.InsuredBill;


            int VarAgencyBill  = BillType.AgencyBill;
            int VarAgencyBill1stTerm = BillType.AgencyBill1stTerm;

            cmbAGENCY_BILL_TYPE.Items.Insert(0,"Insured Bill All Terms");
            cmbAGENCY_BILL_TYPE.Items.Insert(1,"Agency Bill All Terms");
            cmbAGENCY_BILL_TYPE.Items.Insert(2,"Agency Bill 1st term/Insured Bill @renewal");
            // cmbAGENCY_BILL_TYPE.SelectedIndex=0; 
            cmbAGENCY_BILL_TYPE.Items[0].Value = VarInsuredBill.ToString().Trim();
            cmbAGENCY_BILL_TYPE.Items[1].Value = VarAgencyBill.ToString().Trim();
            cmbAGENCY_BILL_TYPE.Items[2].Value = VarAgencyBill1stTerm.ToString().Trim();*/
            if (Request.QueryString["AGENCY_ID"] != null)
            {
                int agencyId = int.Parse(Request.QueryString["AGENCY_ID"].ToString().Trim());
                ClsAgency.GetBillTypeAgency(cmbAGENCY_BILL_TYPE, agencyId, "");
            }
            else
            {
                ClsAgency.GetBillTypeAgency(cmbAGENCY_BILL_TYPE, 0, "NEW");
            }



            //1099 Process

            cmbPROCESS_1099.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("ESPPO");
            cmbPROCESS_1099.DataTextField = "LookupDesc";
            cmbPROCESS_1099.DataValueField = "LookupID";
            cmbPROCESS_1099.DataBind();

            cmbAGENCY_TYPE_ID.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("AGTYP");
            cmbAGENCY_TYPE_ID.DataTextField = "LookupDesc";
            cmbAGENCY_TYPE_ID.DataValueField = "LookupID";
            cmbAGENCY_TYPE_ID.DataBind();
            cmbAGENCY_TYPE_ID.Items.Insert(0, "");

            // Added by Mohit Agarwal 13-Oct 2008 ITrack 4795 Reopen
            Cms.BusinessLayer.BlCommon.ClsCommon.RemoveOptionFromDropdownByValue(cmbPROCESS_1099, "14123");
            //cmbPROCESS_1099.Items.Remove(cmbPROCESS_1099.Items.FindByValue("14123"));

            //using singleton object for country and state dropdown
            DataTable dt = Cms.CmsWeb.ClsFetcher.Country;
            cmbAGENCY_COUNTRY.DataSource = dt;
            cmbAGENCY_COUNTRY.DataTextField = "Country_Name";
            cmbAGENCY_COUNTRY.DataValueField = "Country_Id";
            cmbAGENCY_COUNTRY.DataBind();
            //cmbAGENCY_COUNTRY.Items.Insert(0, "");
            cmbM_AGENCY_COUNTRY.DataSource = dt;
            cmbM_AGENCY_COUNTRY.DataTextField = "Country_Name";
            cmbM_AGENCY_COUNTRY.DataValueField = "Country_Id";
            cmbM_AGENCY_COUNTRY.DataBind();
            //cmbM_AGENCY_COUNTRY.Items.Insert(0, "");

            ClsStates objStates = new ClsStates();
            DataTable dtStates = objStates.GetStatesForCountry(Convert.ToInt32(dt.Rows[0]["Country_Id"].ToString()));
            if (dtStates != null && dtStates.Rows.Count > 0)
            {
                cmbAGENCY_STATE.DataSource = dtStates;
                cmbAGENCY_STATE.DataTextField = STATE_NAME;
                cmbAGENCY_STATE.DataValueField = STATE_ID;
                cmbAGENCY_STATE.DataBind();
                cmbAGENCY_STATE.Items.Insert(0, "");
            }

            if (dtStates != null && dtStates.Rows.Count > 0)
            {
                cmbM_AGENCY_STATE.DataSource = dtStates;
                cmbM_AGENCY_STATE.DataTextField = STATE_NAME;
                cmbM_AGENCY_STATE.DataValueField = STATE_ID;
                cmbM_AGENCY_STATE.DataBind();
                cmbM_AGENCY_STATE.Items.Insert(0, "");
            }




            //dt = Cms.CmsWeb.ClsFetcher.State;
            //cmbAGENCY_STATE.DataSource		= dt;
            //cmbAGENCY_STATE.DataTextField	= "STATE_NAME";
            //cmbAGENCY_STATE.DataValueField	= "STATE_ID";
            //cmbAGENCY_STATE.DataBind();
            //cmbAGENCY_STATE.Items.Insert(0,"");



            //dt = Cms.CmsWeb.ClsFetcher.State;
            //cmbM_AGENCY_STATE.DataSource		= dt;
            //cmbM_AGENCY_STATE.DataTextField	= "STATE_NAME";
            //cmbM_AGENCY_STATE.DataValueField	= "STATE_ID";
            //cmbM_AGENCY_STATE.DataBind();
            //cmbM_AGENCY_STATE.Items.Insert(0,"");


            dt = Cms.CmsWeb.ClsFetcher.Currency;
            cmbBROKER_CURRENCY.DataSource = dt;
            cmbBROKER_CURRENCY.DataTextField = "CURR_DESC";
            cmbBROKER_CURRENCY.DataValueField = "CURRENCY_ID";
            cmbBROKER_CURRENCY.DataBind();
            cmbBROKER_CURRENCY.Items.Insert(0, "");

            //Binding the Broker type combo box
            cmbBROKER_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CUSTYPE");
            cmbBROKER_TYPE.DataTextField = "LookupDesc";
            cmbBROKER_TYPE.DataValueField = "LookupID";
            cmbBROKER_TYPE.DataBind();
            cmbBROKER_TYPE.Items.Insert(0, "");

            cmbMARITAL_STATUS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("MARST");
            cmbMARITAL_STATUS.DataTextField = "LookupDesc";
            cmbMARITAL_STATUS.DataValueField = "LookupID";
            cmbMARITAL_STATUS.DataBind();
            cmbMARITAL_STATUS.Items.Insert(0, "");


            //Bind Gender

            cmbGENDER.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("Sex");
            cmbGENDER.DataTextField = "LookupDesc";
            cmbGENDER.DataValueField = "LookupID";
            cmbGENDER.DataBind();
            cmbGENDER.Items.Insert(0, "");

        }

        #region GetFormValue
        /// <summary>
        /// Fetch form's value and stores into model class object and return that object.
        /// </summary>
        private ClsAgencyInfo GetFormValue()
        {
            //BusinessLayer.BlCommon.ClsCommon objcommon = new ClsCommon();
            //Creating the Model object for holding the New data
            ClsAgencyInfo objAgencyInfo;
            objAgencyInfo = new ClsAgencyInfo();

            objAgencyInfo.AGENCY_CODE = txtAGENCY_CODE.Text;
            objAgencyInfo.AGENCY_COMBINED_CODE = txtAGENCY_COMBINED_CODE.Text;
            objAgencyInfo.AGENCY_DISPLAY_NAME = txtAGENCY_DISPLAY_NAME.Text;
            objAgencyInfo.AGENCY_DBA = txtAGENCY_DBA.Text;
            if (txtAGENCY_LIC_NUM.Text.Trim() != string.Empty)
            {
                objAgencyInfo.AGENCY_LIC_NUM = int.Parse(txtAGENCY_LIC_NUM.Text);
            }
            objAgencyInfo.AGENCY_ADD1 = txtAGENCY_ADD1.Text;
            objAgencyInfo.AGENCY_ADD2 = txtAGENCY_ADD2.Text;
            objAgencyInfo.AGENCY_CITY = txtAGENCY_CITY.Text;
            //objAgencyInfo.AGENCY_STATE=	cmbAGENCY_STATE.SelectedValue;
            objAgencyInfo.AGENCY_ZIP = txtAGENCY_ZIP.Text;
            objAgencyInfo.AGENCY_COUNTRY = cmbAGENCY_COUNTRY.SelectedValue;

            // Added by santosk kumar gautam on 27 jan 2011 itrack:458
            //added by praveer
            if (cmbAGENCY_STATE.SelectedValue != "")
                objAgencyInfo.AGENCY_STATE = cmbAGENCY_STATE.SelectedValue.Trim();
            else
            {
                if (!string.IsNullOrEmpty(hidAGENCY_STATE.Value) && hidAGENCY_STATE.Value != "0")
                    objAgencyInfo.AGENCY_STATE = hidAGENCY_STATE.Value.Trim();
            }
            objAgencyInfo.AGENCY_PHONE = txtAGENCY_PHONE.Text;
            objAgencyInfo.AGENCY_EXT = txtAGENCY_EXT.Text;
            objAgencyInfo.AGENCY_FAX = txtAGENCY_FAX.Text;
            objAgencyInfo.AGENCY_SPEED_DIAL = txtAGENCY_SPEED_DIAL.Text;
            objAgencyInfo.AGENCY_EMAIL = txtAGENCY_EMAIL.Text;
            objAgencyInfo.AGENCY_WEBSITE = txtAGENCY_WEBSITE.Text;
            objAgencyInfo.M_AGENCY_ADD_1 = txtM_AGENCY_ADD_1.Text;
            objAgencyInfo.M_AGENCY_ADD_2 = txtM_AGENCY_ADD_2.Text;
            objAgencyInfo.M_AGENCY_CITY = txtM_AGENCY_CITY.Text;
            objAgencyInfo.M_AGENCY_COUNTRY = cmbM_AGENCY_COUNTRY.SelectedValue;

            // Added by santosk kumar gautam on 27 jan 2011 itrack:458
            //added by praveer
            if (cmbM_AGENCY_STATE.SelectedValue != "")
                objAgencyInfo.M_AGENCY_STATE = cmbM_AGENCY_STATE.SelectedValue;
            else
            {
                if (!string.IsNullOrEmpty(hidM_AGENCY_STATE.Value) && hidM_AGENCY_STATE.Value != "0")
                    objAgencyInfo.M_AGENCY_STATE = hidM_AGENCY_STATE.Value;

            }
            objAgencyInfo.M_AGENCY_ZIP = txtM_AGENCY_ZIP.Text;
            objAgencyInfo.PROCESS_1099 = Convert.ToInt32(cmbPROCESS_1099.SelectedValue);
            objAgencyInfo.ALLOWS_EFT = Convert.ToInt32(cmbALLOWS_EFT.SelectedValue);
            objAgencyInfo.BROKER_TYPE = Convert.ToInt32(cmbBROKER_TYPE.SelectedValue);
            //objAgencyInfo.AGENCY_TYPE_ID = Convert.ToInt32(cmbAGENCY_TYPE_ID.SelectedValue);
            //Itrack 4098
            objAgencyInfo.ALLOWS_CUSTOMER_SWEEP = Convert.ToInt32(cmbALLOWS_CUSTOMER_SWEEP.SelectedValue);

            //Added by pradeep kushwaha
            objAgencyInfo.SUSEP_NUMBER = txtSUSEP_NUMBER.Text;

            //Added by praveer panghal
            if (cmbBROKER_CURRENCY.SelectedItem != null && cmbBROKER_CURRENCY.SelectedValue != "")
            {
                objAgencyInfo.BROKER_CURRENCY = int.Parse(cmbBROKER_CURRENCY.SelectedValue);
            }
            if (cmbAGENCY_TYPE_ID.SelectedItem != null && cmbAGENCY_TYPE_ID.SelectedValue != "")
            {
                objAgencyInfo.AGENCY_TYPE_ID = int.Parse(cmbAGENCY_TYPE_ID.SelectedValue);
            }

            //objAgencyInfo.NUM_AGENCY_CODE= int.Parse(txtNUM_AGENCY_CODE.Text.Trim()==""?"0":txtNUM_AGENCY_CODE.Text.Trim());
            if (txtNUM_AGENCY_CODE.Text.Trim() != "")
            { objAgencyInfo.NUM_AGENCY_CODE = Convert.ToInt32(txtNUM_AGENCY_CODE.Text); }

            //objAgencyInfo.M_AGENCY_PHONE= txtM_AGENCY_PHONE.Text;
            //objAgencyInfo.M_AGENCY_EXT= txtM_AGENCY_EXT.Text;
            //objAgencyInfo.M_AGENCY_FAX= txtM_AGENCY_FAX.Text;


            // Added by mohit on 2/11/2005
            if (txtTERMINATION_DATE.Text.Trim() != "")
                objAgencyInfo.TERMINATION_DATE = ConvertToDate(txtTERMINATION_DATE.Text);

            //objAgencyInfo.TERMINATION_REASON= txtTERMINATION_REASON.Text;
            // End

            //nbisht
            //if(txtTERMINATION_REASON.Text.Trim()!="")
            //	objAgencyInfo.TERMINATION_REASON= Convert.ToDateTime(txtTERMINATION_REASON.Text);

            //Start --  New Field Added -- Gaurav
            if (txtPRINCIPAL_CONTACT.Text.Trim() != "")
                objAgencyInfo.PRINCIPAL_CONTACT = txtPRINCIPAL_CONTACT.Text;
            if (txtOTHER_CONTACT.Text.Trim() != "")
                objAgencyInfo.OTHER_CONTACT = txtOTHER_CONTACT.Text;

            if (txtORIGINAL_CONTRACT_DATE.Text.Trim() != "")
                objAgencyInfo.ORIGINAL_CONTRACT_DATE = ConvertToDate(txtORIGINAL_CONTRACT_DATE.Text);

            if (txtCURRENT_CONTRACT_DATE.Text.Trim() != "")
                objAgencyInfo.CURRENT_CONTRACT_DATE = ConvertToDate(txtCURRENT_CONTRACT_DATE.Text);


            if (txtROUTING_NUMBER.Text.Trim() != "")
            {
                objAgencyInfo.ROUTING_NUMBER = BusinessLayer.BlCommon.ClsCommon.EncryptString(txtROUTING_NUMBER.Text);
                txtROUTING_NUMBER.Text = "";
            }
            else
                objAgencyInfo.ROUTING_NUMBER = hidROUTING_NUMBER.Value;

            if (txtBANK_ACCOUNT_NUMBER.Text.Trim() != "")
            {
                objAgencyInfo.BANK_ACCOUNT_NUMBER = BusinessLayer.BlCommon.ClsCommon.EncryptString(txtBANK_ACCOUNT_NUMBER.Text);
                txtBANK_ACCOUNT_NUMBER.Text = "";
            }
            else
                objAgencyInfo.BANK_ACCOUNT_NUMBER = hidBANK_ACCOUNT_NUMBER.Value;

            if (txtROUTING_NUMBER1.Text.Trim() != "")
            {
                objAgencyInfo.ROUTING_NUMBER1 = BusinessLayer.BlCommon.ClsCommon.EncryptString(txtROUTING_NUMBER1.Text);
                txtROUTING_NUMBER1.Text = "";
            }
            else
                objAgencyInfo.ROUTING_NUMBER1 = hidROUTING_NUMBER1.Value;

            if (txtBANK_ACCOUNT_NUMBER1.Text.Trim() != "")
            {
                objAgencyInfo.BANK_ACCOUNT_NUMBER1 = BusinessLayer.BlCommon.ClsCommon.EncryptString(txtBANK_ACCOUNT_NUMBER1.Text);
                txtBANK_ACCOUNT_NUMBER1.Text = "";
            }
            else
                objAgencyInfo.BANK_ACCOUNT_NUMBER1 = hidBANK_ACCOUNT_NUMBER1.Value;

            if (txtBANK_NAME.Text.Trim() != "")
                objAgencyInfo.BANK_NAME = txtBANK_NAME.Text;
            if (txtBANK_BRANCH.Text.Trim() != "")
                objAgencyInfo.BANK_BRANCH = txtBANK_BRANCH.Text;

            if (txtBANK_NAME_2.Text.Trim() != "")
                objAgencyInfo.BANK_NAME_2 = txtBANK_NAME_2.Text;
            if (txtBANK_BRANCH_2.Text.Trim() != "")
                objAgencyInfo.BANK_BRANCH_2 = txtBANK_BRANCH_2.Text;



            if (txtNOTES.Text.Trim() != "")
                objAgencyInfo.NOTES = txtNOTES.Text;
            if (txtFEDERAL_ID.Text.Trim() != "")
            {
                objAgencyInfo.FEDERAL_ID = BusinessLayer.BlCommon.ClsCommon.EncryptString(txtFEDERAL_ID.Text);
                txtFEDERAL_ID.Text = "";
            }
            else
                objAgencyInfo.FEDERAL_ID = hidFEDERAL_ID.Value;

            //Added By Raghav For Special Handling
            if (chkREQ_SPECIAL_HANDLING.Checked == true)
                objAgencyInfo.REQ_SPECIAL_HANDLING = int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.YES).ToString()); //YES

            else
                objAgencyInfo.REQ_SPECIAL_HANDLING = int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.NO).ToString()); //NO

            //Reverify Model Objects
            if (chkREVERIFIED_AC1.Checked == true)
                objAgencyInfo.REVERIFIED_AC1 = int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.YES).ToString()); //YES
            else
                objAgencyInfo.REVERIFIED_AC1 = int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.NO).ToString()); //NO

            if (chkREVERIFIED_AC2.Checked == true)
                objAgencyInfo.REVERIFIED_AC2 = int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.YES).ToString()); //YES
            else
                objAgencyInfo.REVERIFIED_AC2 = int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.NO).ToString()); //NO

            //			if(lstUNDERWRITER_ASSIGNED_AGENCY.SelectedItem!=null)
            //			objAgencyInfo.UNDERWRITER_ASSIGNED_AGENCY = hidUnderWriter.Value;

            //End --  New Field Added -- Gaurav

            objAgencyInfo.IS_ACTIVE = hidIS_ACTIVE.Value;
            objAgencyInfo.AGENCY_PAYMENT_METHOD = cmbAGENCY_PAYMENT_METHOD.SelectedValue;
            objAgencyInfo.AGENCY_BILL_TYPE = cmbAGENCY_BILL_TYPE.SelectedValue;
            objAgencyInfo.AgencyName = cmbAGENCYNAME.SelectedValue;
            objAgencyInfo.PROCESS_1099 = Convert.ToInt32(cmbPROCESS_1099.SelectedValue);

            //BY PRAVESH
            //objAgencyInfo.TERMINATION_REASON =	txtTERMINATION_REASON.Text;
            if (txtTERMINATION_DATE_RENEW.Text.Trim() != "")
                objAgencyInfo.TERMINATION_DATE_RENEW = ConvertToDate(txtTERMINATION_DATE_RENEW.Text);
            objAgencyInfo.TERMINATION_NOTICE = cmbTERMINATION_NOTICE.SelectedValue;

            if (objAgencyInfo.ALLOWS_EFT == int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.NO).ToString()))
            {

                objAgencyInfo.ACCOUNT_TYPE = System.DBNull.Value.ToString();
                //objAgencyInfo.ACCOUNT_TYPE_2="";

            }
            else
            {
                if (rdbACC_CASH_ACC_TYPEO.Checked)//Checking
                {
                    objAgencyInfo.ACCOUNT_TYPE = Convert.ToString((int)EFTCodes.CheckingAccount);
                    //objAgencyInfo.ACCOUNT_TYPE_2 = Convert.ToString((int)EFTCodes.CheckingAccount);
                }

                else if (rdbACC_CASH_ACC_TYPET.Checked)//Saving
                {
                    objAgencyInfo.ACCOUNT_TYPE = Convert.ToString((int)EFTCodes.SavingAccount);
                    //objAgencyInfo.ACCOUNT_TYPE_2 = Convert.ToString((int)EFTCodes.SavingAccount);
                }

            }

            if (objAgencyInfo.ALLOWS_CUSTOMER_SWEEP == int.Parse(((int)Cms.BusinessLayer.BlCommon.ClsCommon.enumYESNO_LOOKUP_UNIQUE_ID.NO).ToString()))
            {
                objAgencyInfo.ACCOUNT_TYPE_2 = System.DBNull.Value.ToString();
            }
            else
            {
                if (rdbACC_CASH_ACC_TYPEO_2.Checked)//Checking
                {
                    //objAgencyInfo.ACCOUNT_TYPE = Convert.ToString((int)EFTCodes.CheckingAccount);
                    objAgencyInfo.ACCOUNT_TYPE_2 = Convert.ToString((int)EFTCodes.CheckingAccount);
                }

                else if (rdbACC_CASH_ACC_TYPET_2.Checked)//Saving
                {
                    //objAgencyInfo.ACCOUNT_TYPE = Convert.ToString((int)EFTCodes.SavingAccount);
                    objAgencyInfo.ACCOUNT_TYPE_2 = Convert.ToString((int)EFTCodes.SavingAccount);
                }
            }


            objAgencyInfo.INCORPORATED_LICENSE = cmbINCORPORATED_LICENSE.SelectedValue;


            objAgencyInfo.DISTRICT = txtDISTRICT.Text;
            objAgencyInfo.NUMBER = txtNUMBER.Text;
            objAgencyInfo.BROKER_CPF_CNPJ = txtBROKER_CPF_CNPJ.Text;
            if (txtBROKER_DATE_OF_BIRTH.Text != "")
            {
                objAgencyInfo.BROKER_DATE_OF_BIRTH = ConvertToDate(txtBROKER_DATE_OF_BIRTH.Text);
            }
            objAgencyInfo.BROKER_REGIONAL_ID = txtBROKER_REGIONAL_ID.Text;
            objAgencyInfo.REGIONAL_ID_ISSUANCE = txtREGIONAL_ID_ISSUANCE.Text;
            if (txtREGIONAL_ID_ISSUE_DATE.Text != "")
            {
                objAgencyInfo.REGIONAL_ID_ISSUE_DATE = ConvertToDate(txtREGIONAL_ID_ISSUE_DATE.Text);
            }
            objAgencyInfo.BROKER_BANK_NUMBER = txtBROKER_BANK_NUMBER.Text;
            if (cmbBROKER_TYPE.SelectedValue == "11110")
            {
                objAgencyInfo.MARITAL_STATUS = int.Parse(cmbMARITAL_STATUS.SelectedValue);
                objAgencyInfo.GENDER = int.Parse(cmbGENDER.SelectedValue);
            }



            //END HERE
            //These  assignments are common to all pages.
            strFormSaved = hidFormSaved.Value;
            strRowId = hidAGENCY_ID.Value;
            oldXML = hidOldData.Value;
            //Returning the model object


            return objAgencyInfo;
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
            this.txtM_AGENCY_ADD_2.TextChanged += new System.EventHandler(this.txtM_AGENCY_ADD_2_TextChanged);
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
                objAgency = new ClsAgency();

                //Retreiving the form values into model class object
                ClsAgencyInfo objAgencyInfo = GetFormValue();

                if (strRowId.ToUpper().Equals("NEW")) //save case
                {
                    objAgencyInfo.CREATED_BY = int.Parse(GetUserId());
                    objAgencyInfo.CREATED_DATETIME = DateTime.Now;
                    objAgencyInfo.IS_ACTIVE = "Y";

                    //Calling the add method of business layer class
                    intRetVal = objAgency.Add(objAgencyInfo);

                    if (intRetVal > 0)
                    {
                        hidAGENCY_ID.Value = objAgencyInfo.AGENCY_ID.ToString();
                        //Added by kuldeep on 30-11-2011 to get data accroding to the agency id (agency code is hidden in some implementation TFS 840)
                        SetAgencyId(hidAGENCY_ID.Value);
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                        hidFormSaved.Value = "1";
                        hidIS_ACTIVE.Value = "Y";
                        btnDelete.Style.Add("display", "inline");  //added by aditya on 10-08-2011 for TFS Bug # 121
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objAgencyInfo.IS_ACTIVE.ToString().Trim());
                    }
                    else if (intRetVal == -1)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "18");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetVal == -2)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "16");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetVal == -3)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "854");
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
                    ClsAgencyInfo objOldAgencyInfo;
                    objOldAgencyInfo = new ClsAgencyInfo();

                    //Setting  the Old Page details(XML File containing old details) into the Model Object
                    base.PopulateModelObject(objOldAgencyInfo, hidOldData.Value);

                    //Setting those values into the Model object which are not in the page
                    if (strRowId != "")
                        objAgencyInfo.AGENCY_ID = int.Parse(strRowId);
                    objAgencyInfo.MODIFIED_BY = int.Parse(GetUserId());
                    objAgencyInfo.LAST_UPDATED_DATETIME = DateTime.Now;


                    //Updating the record using business layer class object
                    intRetVal = objAgency.Update(objOldAgencyInfo, objAgencyInfo);
                    if (intRetVal > 0)			// update successfully performed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "31");
                        hidFormSaved.Value = "1";
                        ClsAgency.GetBillTypeAgency(cmbAGENCY_BILL_TYPE, objAgencyInfo.AGENCY_ID, "");
                    }
                    else if (intRetVal == -1)	// Duplicate code exist, update failed
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "18");
                        hidFormSaved.Value = "1";
                    }
                    else if (intRetVal == -2)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "16");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetVal == -3)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "854");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId, "20");
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

            }
            finally
            {
                if (objAgency != null)
                    objAgency.Dispose();
            }

            if (hidAGENCY_ID.Value.ToUpper() != "NEW")
                GenerateXML(hidAGENCY_ID.Value);
        }

        /// <summary>
        /// Activates and deactivates  .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
        {
            Cms.BusinessLayer.BlCommon.ClsAgency objAgency = new Cms.BusinessLayer.BlCommon.ClsAgency();
            try
            {
                Cms.BusinessLayer.BlCommon.stuTransactionInfo objStuTransactionInfo = new Cms.BusinessLayer.BlCommon.stuTransactionInfo();
                objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
                objStuTransactionInfo.loggedInUserName = GetUserName();
                // ClsAgencyInfo objAgencyInfo = GetFormValue();
                ClsAgencyInfo objAgencyInfo = new ClsAgencyInfo();
                if (hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
                {
                    objStuTransactionInfo.transactionDescription = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1576");// "Agency is Deactivated Succesfully.";
                    string AgencyCode = ClsCommon.FetchValueFromXML("AGENCY_CODE", hidOldData.Value);
                    string AgencyName = ClsCommon.FetchValueFromXML("AGENCY_DISPLAY_NAME", hidOldData.Value);
                    string strCustInfo = "<br>" + "Agency Name :" + AgencyName + "<br>" + "Agency Code :" + AgencyCode;

                    objAgency.TransactionInfoParams = objStuTransactionInfo;
                    string strRetVal = objAgency.ActivateDeactivate(hidAGENCY_ID.Value, "N", strCustInfo);
                    if (strRetVal == "1")
                    {
                        objAgencyInfo.IS_ACTIVE = "N";
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objAgencyInfo.IS_ACTIVE.ToString().Trim());
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "41");
                        hidIS_ACTIVE.Value = "N";
                    }
                    else if (strRetVal == "-2")
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "948");
                        hidIS_ACTIVE.Value = "Y";
                    }

                    //string strScript="<script>RefreshWebGrid('" + hidAGENCY_ID.Value + "','');</script>";
                    //RegisterStartupScript("REFRESHGRID",strScript);
                }
                else
                {


                    objStuTransactionInfo.transactionDescription = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1577");// "Agency is Activated Succesfully.";
                    string AgencyCode = ClsCommon.FetchValueFromXML("AGENCY_CODE", hidOldData.Value);
                    string AgencyName = ClsCommon.FetchValueFromXML("AGENCY_DISPLAY_NAME", hidOldData.Value);
                    string strCustInfo = "<br>" + "Agency Name :" + AgencyName + "<br>" + "Agency Code :" + AgencyCode;
                    objAgency.TransactionInfoParams = objStuTransactionInfo;
                    objAgency.ActivateDeactivate(hidAGENCY_ID.Value, "Y", strCustInfo);
                    objAgencyInfo.IS_ACTIVE = "Y";
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objAgencyInfo.IS_ACTIVE.ToString().Trim());
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "40");
                    hidIS_ACTIVE.Value = "Y";
                    //string strScript="<script>RefreshWebGrid('','" + hidAGENCY_ID.Value + "');</script>";
                    //RegisterStartupScript("REFRESHGRID",strScript);
                }
                hidFormSaved.Value = "1";
                GenerateXML(hidAGENCY_ID.Value);
                hidReset.Value = "0";
                //GetOldDataXML();
                //				Commented by Praveen Kumar(8-01-09):ITRACK 5269
                //				string strScript="<script>RefreshWebGrid('1','" + hidAGENCY_ID.Value + "');</script>";
                //				RegisterStartupScript("REFRESHGRID",strScript);

                //				END COMMENT PRAVEEN KUMAR

            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;

            }
            finally
            {
                lblMessage.Visible = true;
                if (objAgency != null)
                    objAgency.Dispose();
            }
            //if(hidAGENCY_ID.Value.ToUpper()!="NEW")
            //GenerateXML(Request.QueryString["AGENCY_ID"].ToString());
            //	GenerateXML(hidAGENCY_ID.Value);

        }
        #endregion

        private void SetCaptions()
        {
            capAGENCY_CODE.Text = objResourceMgr.GetString("txtAGENCY_CODE");
            capAGENCY_COMBINED_CODE.Text = objResourceMgr.GetString("txtAGENCY_COMBINED_CODE");
            capAGENCY_DISPLAY_NAME.Text = objResourceMgr.GetString("txtAGENCY_DISPLAY_NAME");
            capAGENCY_DBA.Text = objResourceMgr.GetString("txtAGENCY_DBA");
            capAgencyDecName.Text = objResourceMgr.GetString("txtAGENCYNAME");
            capAGENCY_LIC_NUM.Text = objResourceMgr.GetString("txtAGENCY_LIC_NUM");
            capAGENCY_ADD1.Text = objResourceMgr.GetString("txtAGENCY_ADD1");
            capAGENCY_ADD2.Text = objResourceMgr.GetString("txtAGENCY_ADD2");
            capAGENCY_CITY.Text = objResourceMgr.GetString("txtAGENCY_CITY");
            capAGENCY_STATE.Text = objResourceMgr.GetString("cmbAGENCY_STATE");
            capAGENCY_ZIP.Text = objResourceMgr.GetString("txtAGENCY_ZIP");
            capAGENCY_COUNTRY.Text = objResourceMgr.GetString("cmbAGENCY_COUNTRY");
            capAGENCY_PHONE.Text = objResourceMgr.GetString("txtAGENCY_PHONE");
            capAGENCY_EXT.Text = objResourceMgr.GetString("txtAGENCY_EXT");
            capAGENCY_FAX.Text = objResourceMgr.GetString("txtAGENCY_FAX");
            capAGENCY_SPEED_DIAL.Text = objResourceMgr.GetString("txtAGENCY_SPEED_DIAL");
            capAGENCY_EMAIL.Text = objResourceMgr.GetString("txtAGENCY_EMAIL");
            capAGENCY_WEBSITE.Text = objResourceMgr.GetString("txtAGENCY_WEBSITE");
            capAGENCY_PAYMENT_METHOD.Text = objResourceMgr.GetString("cmbAGENCY_PAYMENT_METHOD");
            capAGENCY_BILL_TYPE.Text = objResourceMgr.GetString("cmbAGENCY_BILL_TYPE");
            capPRINCIPAL_CONTACT.Text = objResourceMgr.GetString("txtPRINCIPAL_CONTACT");
            capOTHER_CONTACT.Text = objResourceMgr.GetString("txtOTHER_CONTACT");
            capFEDERAL_ID.Text = objResourceMgr.GetString("txtFEDERAL_ID");
            capORIGINAL_CONTRACT_DATE.Text = objResourceMgr.GetString("txtORIGINAL_CONTRACT_DATE");
            capCURRENT_CONTRACT_DATE.Text = objResourceMgr.GetString("txtCURRENT_CONTRACT_DATE");

            //capUNDERWRITER_ASSIGNED_AGENCY.Text		=		objResourceMgr.GetString("lstUNDERWRITER_ASSIGNED_AGENCY");
            capBANK_ACCOUNT_NUMBER.Text = objResourceMgr.GetString("txtBANK_ACCOUNT_NUMBER");
            capROUTING_NUMBER.Text = objResourceMgr.GetString("txtROUTING_NUMBER");
            capBANK_ACCOUNT_NUMBER1.Text = objResourceMgr.GetString("txtBANK_ACCOUNT_NUMBER1");
            capROUTING_NUMBER1.Text = objResourceMgr.GetString("txtROUTING_NUMBER1");

            capBANK_NAME.Text = objResourceMgr.GetString("txtBANK_NAME");
            capBANK_BRANCH.Text = objResourceMgr.GetString("txtBANK_BRANCH");

            //Add By kranti
            capBANK_NAME_2.Text = objResourceMgr.GetString("txtBANK_NAME_2");
            capBANK_BRANCH_2.Text = objResourceMgr.GetString("txtBANK_BRANCH_2");

            capNOTES.Text = objResourceMgr.GetString("txtNOTES");
            capNUM_AGENCY_CODE.Text = objResourceMgr.GetString("txtNUM_AGENCY_CODE");

            //new fiels
            capM_AGENCY_ADD_1.Text = objResourceMgr.GetString("txtM_AGENCY_ADD_1");
            capM_AGENCY_ADD_2.Text = objResourceMgr.GetString("txtM_AGENCY_ADD_2");
            capM_AGENCY_CITY.Text = objResourceMgr.GetString("txtM_AGENCY_CITY");
            capM_AGENCY_COUNTRY.Text = objResourceMgr.GetString("cmbM_AGENCY_COUNTRY");
            capM_AGENCY_STATE.Text = objResourceMgr.GetString("cmbM_AGENCY_STATE");
            capM_AGENCY_ZIP.Text = objResourceMgr.GetString("txtM_AGENCY_ZIP");
            //capM_AGENCY_PHONE.Text					=      objResourceMgr.GetString("txtM_AGENCY_PHONE");
            //capM_AGENCY_EXT.Text						=	   objResourceMgr.GetString("txtM_AGENCY_EXT");
            //capM_AGENCY_FAX.Text						=	   objResourceMgr.GetString("txtM_AGENCY_FAX");
            lblCopy_Address.Text = objResourceMgr.GetString("lblCopy_Address");



            capALLOWS_EFT.Text = objResourceMgr.GetString("cmbALLOWS_EFT");
            capVARIFIED1.Text = objResourceMgr.GetString("capVARIFIED1");
            capVARIFIED2.Text = objResourceMgr.GetString("capVARIFIED2");
            capVARIFIED_DATE1.Text = objResourceMgr.GetString("capVARIFIED_DATE1");
            capVARIFIED_DATE2.Text = objResourceMgr.GetString("capVARIFIED_DATE2");
            capREASON1.Text = objResourceMgr.GetString("capREASON1");
            capREASON2.Text = objResourceMgr.GetString("capREASON2");
            capALLOWS_CUSTOMER_SWEEP.Text = objResourceMgr.GetString("cmbALLOWS_CUSTOMER_SWEEP");

            //Added By Raghav For Special Handling

            capREQ_SPECIAL_HANDLING.Text = objResourceMgr.GetString("chkREQ_SPECIAL_HANDLING");
            capTERMINATION_DATE.Text = objResourceMgr.GetString("txtTERMINATION_DATE");
            capTERMINATION_DATE_RENEW.Text = objResourceMgr.GetString("txtTERMINATION_DATE_RENEW");
            capTERMINATION_NOTICE.Text = objResourceMgr.GetString("txtTERMINATION_NOTICE");
            capREVERIFIED_AC1.Text = objResourceMgr.GetString("chkREVERIFIED_AC1");
            capREVERIFIED_AC2.Text = objResourceMgr.GetString("chkREVERIFIED_AC2");

            //Added by pradeep kushwaha

            capSUSEP_NUMBER.Text = objResourceMgr.GetString("txtSUSEP_NUMBER");
            // capMANDATORY_FIELD.Text                     =       objResourceMgr.GetString("capMANDATORY_FIELD");
            capPHYSICAL_ADDRESS.Text = objResourceMgr.GetString("capPHYSICAL_ADDRESS");
            capMAILING_ADDRESS.Text = objResourceMgr.GetString("capMAILING_ADDRESS");
            capEFT_BANK_INFORMATION.Text = objResourceMgr.GetString("capEFT_BANK_INFORMATION");
            capACCOUNT_FOR_AGENCY_MSG.Text = objResourceMgr.GetString("capACCOUNT_FOR_AGENCY_MSG");
            capOTHER_INFORMATION.Text = objResourceMgr.GetString("capOTHER_INFORMATION");
            capINCORPORATED_LICENSE.Text = objResourceMgr.GetString("capINCORPORATED_LICENSE");
            capPROCESS_1099.Text = objResourceMgr.GetString("capPROCESS_1099");
            btnCopyPhysicalAddress.Text = objResourceMgr.GetString("btnCopyPhysicalAddress");
            capBROKER_CURRENCY.Text = objResourceMgr.GetString("capBROKER_CURRENCY");
            capAGENCY_TYPE_ID.Text = objResourceMgr.GetString("cmbAGENCY_TYPE_ID");
            capBROKER_TYPE.Text = objResourceMgr.GetString("cmbBROKER_TYPE");
            capBROKER_CPF_CNPJ.Text = objResourceMgr.GetString("capBROKER_CPF_CNPJ");
            capDATE_OF_BIRTH.Text = objResourceMgr.GetString("capDATE_OF_BIRTH");
            capBROKER_REGIONAL_ID.Text = objResourceMgr.GetString("capBROKER_REGIONAL_ID");
            capREGIONAL_ID_ISSUANCE.Text = objResourceMgr.GetString("capREGIONAL_ID_ISSUANCE");
            capREGIONAL_ID_ISSUE_DATE.Text = objResourceMgr.GetString("capREGIONAL_ID_ISSUE_DATE");
            capMARITAL_STATUS.Text = objResourceMgr.GetString("capMARITAL_STATUS");
            capGENDER.Text = objResourceMgr.GetString("capGENDER");
            capBROKER_BANK_NUMBER.Text = objResourceMgr.GetString("capBROKER_BANK_NUMBER");
            capDISTRICT.Text = objResourceMgr.GetString("capDISTRICT");
            capNUMBER.Text = objResourceMgr.GetString("capNUMBER");
            capACCOUNT_TYPE.Text = objResourceMgr.GetString("capACCOUNT_TYPE"); //sneha
            capACCOUNT_TYPE_2.Text = objResourceMgr.GetString("capACCOUNT_TYPE_2"); //sneha
            capRAD_CHK.Text = objResourceMgr.GetString("capRAD_CHK");
            capRAD_SAV.Text = objResourceMgr.GetString("capRAD_SAV");
            capACCOUNT.Text = objResourceMgr.GetString("capACCOUNT");
            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");
            rdbACC_CASH_ACC_TYPEO_2.Text = objResourceMgr.GetString("rdbACC_CASH_ACC_TYPEO_2");
            rdbACC_CASH_ACC_TYPET_2.Text = objResourceMgr.GetString("rdbACC_CASH_ACC_TYPET_2");
            hidEdit.Value = objResourceMgr.GetString("hidEdit");
            hidCancel.Value = objResourceMgr.GetString("hidCancel");

        }


        private void ReadOnlyFields()
        {
            strSystemID = GetSystemId();

            string strAgencyID = ClsAgency.GetAgencyIDFromCode(strSystemID).ToString();
            strCarrierSystemID = CarrierSystemID;
            if (strSystemID.Trim().ToUpper() == strCarrierSystemID.Trim().ToUpper())
            {
                txtAGENCY_CODE.ReadOnly = false;
                txtAGENCY_LIC_NUM.ReadOnly = false;
                txtORIGINAL_CONTRACT_DATE.ReadOnly = false;
                txtCURRENT_CONTRACT_DATE.ReadOnly = false;
                //imgORIGINAL_CONTRACT_DATE.Enabled = false;
                //hlkORIGINAL_CONTRACT_DATE.Attributes.Add("OnClick","fPopCalendar(document.MNT_AGENCY_LIST.txtORIGINAL_CONTRACT_DATE,document.MNT_AGENCY_LIST.txtORIGINAL_CONTRACT_DATE)"); //Javascript Implementation for Calender

                // Added by Mohit on 2/11/2005
                // txtTERMINATION_DATE.ReadOnly=false;
                // txtTERMINATION_REASON.ReadOnly=false;
                rowTermination.Visible = true;

                // End 
            }
            else
            {
                txtAGENCY_CODE.ReadOnly = true;
                txtAGENCY_LIC_NUM.ReadOnly = true;
                txtORIGINAL_CONTRACT_DATE.ReadOnly = true;
                txtCURRENT_CONTRACT_DATE.ReadOnly = true;
                //imgORIGINAL_CONTRACT_DATE.Enabled = true;
                //hlkORIGINAL_CONTRACT_DATE.Attributes.Add("OnClick","javascript:return false;"); //Javascript Implementation for Calender

                // Added by Mohit on 2/11/2005
                //txtTERMINATION_DATE.ReadOnly=true;
                //txtTERMINATION_REASON.ReadOnly=true;	
                rowTermination.Visible = false;
                // End

            }

        }
        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            int intRetVal;
            int intAgencyID = int.Parse(hidAGENCY_ID.Value);
            ClsAgencyInfo objAgencyInfo = GetFormValue();
            objAgencyInfo.CREATED_BY = int.Parse(GetUserId());
            objAgencyInfo.CREATED_DATETIME = DateTime.Now;
            objAgency = new Cms.BusinessLayer.BlCommon.ClsAgency();
            intRetVal = objAgency.Delete(objAgencyInfo, intAgencyID);
            //			intRetVal = objAgency.Delete(intAgencyID);
            if (intRetVal > 0)
            {
                lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "127");
                hidFormSaved.Value = "5";
                hidOldData.Value = "";
                trBody.Attributes.Add("style", "display:none");

                string strScript = "<script>parent.RefreshWebgrid(1);</script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "refresh", strScript);
                lblDelete.Visible = true;
            }
            else if (intRetVal == -2)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "449"); // chk it
                hidFormSaved.Value = "2";
            }
            else if (intRetVal == -1)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "128");
                hidFormSaved.Value = "2";
            }
            lblMessage.Visible = true;
        }

        private void txtM_AGENCY_ADD_2_TextChanged(object sender, System.EventArgs e)
        {

        }
        [System.Web.Services.WebMethod]
        public static String GetValidateZipeCode(String ZIPCODE, String COUNTRYID)
        {

            CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();  //Declare the object ob common webservice

            ZIPCODE = ZIPCODE.Replace("-", "");
            String ReturnValue = String.Empty;
            DataSet ds = new DataSet();

            ReturnValue = obj.GetAddressDetailsBasedOnZipeCode(ZIPCODE, Convert.ToInt32(COUNTRYID));


            return ReturnValue;
        }

    }
}

