/***********************************************************************
    <Modified Date			: - 07-02-06
    <Modified By			: - Shafi
	<Purpose				: - Populate Data for Underwriter
	
	<Modified Date			: - 09-02-06
    <Modified By			: - Shafi
	<Purpose				: - Add Filed For Property Inspection Credit applies
 
	<Modified Date			: - 27-02-2006
    <Modified By			: - Vijay Arora
	<Purpose				: - Add the Application and Policy Quote buttons.
 * 
 * 	<Modified Date			: - 
    <Modified By			: - Charles Gomes
	<Purpose				: - Policy Implementation
	
 *  <Modified Date			: - 17-06-2010
    <Modified By			: - Pradeep Kushwaha
    <Purpose				: - Add Reject button ,Implemetion of reject policy
 * *******************************************************************/
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
using Cms.Model.Application;
using Cms.Model.Quote;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlQuote;
using Cms.BusinessLayer.BlClient;
using Cms.BusinessLayer.BlAccount;
using Cms.Model.Policy;
using Cms.BusinessLayer.BlProcess;
using Model.Policy;


namespace Cms.Policies.Aspx
{
    /// <summary>
    /// Summary description for PolicyInformation.
    /// </summary>
    public class PolicyInformation : Cms.Policies.policiesbase
    {
        #region Page controls declaration

        protected System.Web.UI.WebControls.TextBox txtAPP_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtAPP_STATUS;
        protected System.Web.UI.WebControls.TextBox txtAPP_VERSION;
        protected System.Web.UI.WebControls.TextBox txtAPP_INCEPTION_DATE;
        protected System.Web.UI.WebControls.TextBox txtAPP_EFFECTIVE_DATE;
        protected System.Web.UI.WebControls.TextBox txtAPP_EXPIRATION_DATE;
        protected System.Web.UI.WebControls.TextBox txtBillTo;
        protected System.Web.UI.WebControls.TextBox txtPOLICY_LEVEL_COMISSION;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMERNAME;
        protected System.Web.UI.WebControls.TextBox txtPREFERENCE_DAY;
        protected System.Web.UI.WebControls.TextBox txtPOLICY_LOB;
        protected System.Web.UI.WebControls.TextBox txtPOLICY_DESCRIPTION;
        //protected System.Web.UI.WebControls.TextBox txtRECEIVED_PRMIUM;

        protected System.Web.UI.WebControls.DropDownList cmbAPP_SUBLOB;
        protected System.Web.UI.WebControls.DropDownList cmbPOLICY_SUBLOB;
        protected System.Web.UI.WebControls.DropDownList cmbTRANSACTION_TYPE;
        protected System.Web.UI.WebControls.DropDownList cmbDIV_ID_DEPT_ID_PC_ID;
        protected System.Web.UI.WebControls.DropDownList cmbBILL_TYPE;
        protected System.Web.UI.WebControls.DropDownList cmbINSTALL_PLAN_ID;
        //protected System.Web.UI.WebControls.DropDownList cmbAPP_TERMS;//Commented by Pradeep Kushwaha(20-oct-2010) for implementation of APP_TERMS on Textbox
        protected System.Web.UI.WebControls.DropDownList cmbPOLICY_CURRENCY;
        protected System.Web.UI.WebControls.DropDownList cmbPAYOR;
        protected System.Web.UI.WebControls.DropDownList cmbCO_INSURANCE;
        //protected System.Web.UI.WebControls.DropDownList cmbAPP_SUBLOB;
        protected System.Web.UI.WebControls.DropDownList cmbState_ID;
        //protected System.Web.UI.WebControls.DropDownList cmbContact_Person;
        //protected System.Web.UI.WebControls.DropDownList cmbCSR;
        //protected System.Web.UI.WebControls.DropDownList cmbPRODUCER;
        protected System.Web.UI.WebControls.DropDownList cmbPOLICY_TYPE;
        //protected System.Web.UI.WebControls.DropDownList cmbCHARGE_OFF_PRMIUM;

        protected System.Web.UI.WebControls.DropDownList cmbFUND_TYPE;

        protected System.Web.UI.HtmlControls.HtmlSelect cmbCSR;
        protected System.Web.UI.HtmlControls.HtmlSelect cmbPRODUCER;
        protected System.Web.UI.HtmlControls.HtmlSelect cmbUNDERWRITER;
        protected System.Web.UI.HtmlControls.HtmlGenericControl div_poltype;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidReset;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_INCEPTION_DATE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidUnderwriter;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAgencyCode;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIs_Agency;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppVersionID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOBID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_AGENCY_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCSR;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSUB_LOB;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidStateID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDepartmentXml;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidProfitCenterXml;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEPT_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPC_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidRefresh;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyStatus;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFULL_PAY_PLAN_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidBillingPlan;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidBILLTYPE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolTermEffDate;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPAYOR;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_STATUS_CODE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_STATUS;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOBXML;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidQuoteXML;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPRODUCER;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLDOWN_PAY_MODE;
        //Added by Pradeep Kushwaha on 06-07-2010
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidRejectMsg;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidREJECT_REASON_ID;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFUND_TYPE;

        //Added till here 
        protected Cms.CmsWeb.Controls.CmsButton btnCustomerAssistant;
        protected Cms.CmsWeb.Controls.CmsButton btnBack;
        protected Cms.CmsWeb.Controls.CmsButton btnChangeBillType;
        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnAppQuote;
        protected Cms.CmsWeb.Controls.CmsButton btnPolicyQuote;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected Cms.CmsWeb.Controls.CmsButton btnCopyPolicy;
        protected Cms.CmsWeb.Controls.CmsButton btnReject;
        protected Cms.CmsWeb.Controls.CmsButton btnCopy;
        protected Cms.CmsWeb.Controls.CmsButton btnSendCancelNotice;
        protected System.Web.UI.HtmlControls.HtmlTableRow trIsRewritePolicy;
        protected System.Web.UI.HtmlControls.HtmlTableRow trFORMMESSAGE;
        //protected System.Web.UI.HtmlControls.HtmlTableRow trPropInspCr;
        //protected System.Web.UI.HtmlControls.HtmlTableRow trCOMPLETE_APP;
        //protected System.Web.UI.HtmlControls.HtmlTableRow policyTR;  
        //protected System.Web.UI.HtmlControls.HtmlTableRow trBILL_MORTAGAGEE;  

        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAPP_TERMS;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvINSTALL_PLAN_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAPP_EFFECTIVE_DATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAPP_EXPIRATION_DATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAPP_LOB;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_LEVEL_COMISSION;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCUSTOMER_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvBILL_TYPE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_SUBLOB;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvUNDERWRITER;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_CURRENCY;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCO_INSURANCE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAYOR;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCSR;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvBillTo;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvState_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDIV_ID_DEPT_ID_PC_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_TYPE;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATE_ID;

        protected System.Web.UI.WebControls.Label lblManHeader;
        protected System.Web.UI.WebControls.Label lblAppHeader;
        protected System.Web.UI.WebControls.Label lblTermHeader;
        protected System.Web.UI.WebControls.Label lblAgencyHeader;
        protected System.Web.UI.WebControls.Label lblRemarksHeader;
        protected System.Web.UI.WebControls.Label lblBillingHeader;
        protected System.Web.UI.WebControls.Label lblRenewalHeader;
        protected System.Web.UI.WebControls.Label capIS_REWRITE_POLICY;
        protected System.Web.UI.WebControls.Label lblIS_REWRITE_POLICY;
        protected System.Web.UI.WebControls.Label lblFormLoadMessage;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Label capUNDERWRITER;
        protected System.Web.UI.WebControls.Label capBILL_TYPE;
        protected System.Web.UI.WebControls.Label capCUSTOMERNAME;
        protected System.Web.UI.WebControls.Label capPOLICY_STATUS;
        protected System.Web.UI.WebControls.Label capPOLICY_LOB;
        protected System.Web.UI.WebControls.Label capSUB_LOB_DESC;
        protected System.Web.UI.WebControls.Label capPOLICY_NUMBER;
        protected System.Web.UI.WebControls.Label capPOLICY_DISP_VERSION;
        protected System.Web.UI.WebControls.Label capAGENCY_DISPLAY_NAME;
        protected System.Web.UI.WebControls.Label capCSRNAME;
        protected System.Web.UI.WebControls.Label capPRODUCER;
        protected System.Web.UI.WebControls.Label capPOLICY_CURRENCY;
        protected System.Web.UI.WebControls.Label capPOLICY_LEVEL_COMISSION;
        protected System.Web.UI.WebControls.Label capBillTo;
        protected System.Web.UI.WebControls.Label capPAYOR;
        protected System.Web.UI.WebControls.Label capCO_INSURANCE;
        protected System.Web.UI.WebControls.Label lbINSTALL_PLAN_ID;
        protected System.Web.UI.WebControls.Label lblINSTALL_PLAN_ID;
        protected System.Web.UI.WebControls.Label lblDOWN_PAY_MODE;
        protected System.Web.UI.WebControls.Label lblPOLICY_STATUS;
        protected System.Web.UI.WebControls.Label capAPP_TERMS;
        protected System.Web.UI.WebControls.Label capAPP_INCEPTION_DATE;
        protected System.Web.UI.WebControls.Label capAPP_EFFECTIVE_DATE;
        protected System.Web.UI.WebControls.Label capAPP_EXPIRATION_DATE;
        protected System.Web.UI.WebControls.Label capPREFERENCE_DAY;
        protected System.Web.UI.WebControls.Label capPOLICY_LEVEL_COMM_APPLIES;
        protected System.Web.UI.WebControls.Label capBROKER_COMM_FIRST_INSTM;
        protected System.Web.UI.WebControls.Label capBROKER_REQUEST_NO;
        protected System.Web.UI.WebControls.Label capREMARKS;
        protected System.Web.UI.WebControls.Label capINSTALL_PLAN_ID;
        protected System.Web.UI.WebControls.Label lblHeader;
        protected System.Web.UI.WebControls.Label capState;
        protected System.Web.UI.WebControls.Label capHeader;
        protected System.Web.UI.WebControls.Label capTRANSACTION_TYPE;
        protected System.Web.UI.WebControls.Label capDIV_ID_DEPT_ID_PC_ID;
        //protected System.Web.UI.WebControls.Label capCOMPLETE_APP;
        //protected System.Web.UI.WebControls.TextBox txtPOLICY_STATUS;
        //protected System.Web.UI.WebControls.TextBox txtSTATE_NAME;
        //protected System.Web.UI.WebControls.TextBox txtSUB_LOB_DESC;
        //protected System.Web.UI.WebControls.Label capContact_Person;
        //protected System.Web.UI.WebControls.Label capSTATE_NAME;
        //protected System.Web.UI.WebControls.Label lblAllPoliciesHeader;
        //protected System.Web.UI.WebControls.Label capPOLICY_TYPE;     
        //protected System.Web.UI.WebControls.Label lblPolicies;		
        //protected System.Web.UI.WebControls.Label lblAllPolicy;		
        //protected System.Web.UI.WebControls.Label lblPoliciesDiscount;		
        //protected System.Web.UI.WebControls.Label lblEligbilePolicy;	
        //protected System.Web.UI.WebControls.Label capBILL_MORTAGAGEE;			
        //protected System.Web.UI.WebControls.Label lblBILL_MORTAGAGEE;		
        //protected System.Web.UI.WebControls.Label capCHARGE_OFF_PRMIUM;
        //protected System.Web.UI.WebControls.Label capRECEIVED_PRMIUM;        
        //protected System.Web.UI.WebControls.Label capPROXY_SIGN_OBTAINED;

        //protected System.Web.UI.WebControls.RangeValidator rngYEAR_AT_CURR_RESI;

        protected System.Web.UI.WebControls.RegularExpressionValidator revAPP_INCEPTION_DATE;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revCHARGE_OFF_PRMIUM;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revRECEIVED_PRMIUM;

        protected System.Web.UI.WebControls.CheckBox chkPOLICY_LEVEL_COMM_APPLIES;
        protected System.Web.UI.WebControls.CheckBox chkBROKER_COMM_FIRST_INSTM;
        //protected System.Web.UI.WebControls.CheckBox chkCOMPLETE_APP;

        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvPROXY_SIGN_OBTAINED;
        //protected System.Web.UI.WebControls.DropDownList cmbPROXY_SIGN_OBTAINED;
        //protected System.Web.UI.WebControls.Label capYEAR_AT_CURR_RESI;
        //protected System.Web.UI.WebControls.TextBox txtYEAR_AT_CURR_RESI;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revYEAR_AT_CURR_RESI;
        //protected System.Web.UI.WebControls.Label capYEARS_AT_PREV_ADD;
        //protected System.Web.UI.WebControls.TextBox txtYEARS_AT_PREV_ADD;
        //protected System.Web.UI.WebControls.CustomValidator csvYEARS_AT_PREV_ADD;

        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvYEARS_AT_PREV_ADD;

        //protected System.Web.UI.WebControls.DropDownList cmbPROPRTY_INSP_CREDIT;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvPROPRTY_INSP_CREDIT;
        //protected System.Web.UI.WebControls.Label capPIC_OF_LOC;
        //protected System.Web.UI.WebControls.DropDownList cmbPIC_OF_LOC;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvPIC_OF_LOC;

        //protected System.Web.UI.WebControls.Label capPROPRTY_INSP_CREDIT;
        protected System.Web.UI.WebControls.Label cap;
        //protected System.Web.UI.HtmlControls.HtmlGenericControl spnPROPRTY_INSP_CREDIT;
        //protected System.Web.UI.HtmlControls.HtmlGenericControl spnPIC_OF_LOC;
        protected System.Web.UI.WebControls.Label capDOWN_PAY_MODE;
        protected System.Web.UI.WebControls.DropDownList cmbDOWN_PAY_MODE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDOWN_PAY_MODE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDOWN_PAY_MODE;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidINSTALL_PLAN_ID;
        protected System.Web.UI.WebControls.Label capNOT_RENEW_REASON;
        protected System.Web.UI.HtmlControls.HtmlTableRow trRenewalInfo1;
        protected System.Web.UI.WebControls.Label capNOT_RENEW;
        protected System.Web.UI.WebControls.CheckBox chkNOT_RENEW;
        protected System.Web.UI.WebControls.Label capREFER_UNDERWRITER;
        protected System.Web.UI.WebControls.CheckBox chkREFER_UNDERWRITER;
        protected System.Web.UI.HtmlControls.HtmlTableRow trRenewalInfo2;
        protected System.Web.UI.WebControls.DropDownList cmbNOT_RENEW_REASON;
        //protected System.Web.UI.WebControls.DropDownList cmbREINS_SPECIAL_ACPT;
        protected System.Web.UI.HtmlControls.HtmlTableRow trRenewalInfo0;
        protected System.Web.UI.HtmlControls.HtmlTable tblstate;// added by sonal to implemet state for old products specially product ID < 8
        //protected System.Web.UI.WebControls.DropDownList cmbSTATE_ID;
        //DropDownList & RequiredFieldValidator commented by Charles on 21-Aug-09 for APP/POL OPTIMISATION
        //protected System.Web.UI.WebControls.DropDownList cmbAGENCY_ID;		
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_ID;
        protected System.Web.UI.WebControls.Label capMODALITY;
        protected System.Web.UI.WebControls.Label capACTIVITY;

        protected System.Web.UI.WebControls.Label lblCUATION;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEACTIVE_INSTALL_PLAN_ID;
        protected System.Web.UI.WebControls.Label capREFERAL_INSTRUCTIONS;
        //protected System.Web.UI.WebControls.Label capREINS_SPECIAL_ACPT;
        protected System.Web.UI.WebControls.TextBox txtREFERAL_INSTRUCTIONS;
        protected System.Web.UI.WebControls.CustomValidator csvREFERAL_INSTRUCTIONS;
        protected System.Web.UI.WebControls.CustomValidator csvINSTALL_PLAN_ID;

        protected System.Web.UI.WebControls.RegularExpressionValidator revPOLICY_LEVEL_COMISSION;
        protected System.Web.UI.WebControls.CustomValidator csvPOLICY_LEVEL_COMISSION;
        protected System.Web.UI.WebControls.RangeValidator rngPREFERENCE_DAY;

        protected System.Web.UI.WebControls.TextBox txtPOLICY_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtPOLICY_DISP_VERSION;
        protected System.Web.UI.WebControls.TextBox txtBROKER_REQUEST_NO;
        //protected System.Web.UI.WebControls.TextBox txtAGENCY_DISPLAY_NAME;//Commented by Charles on 24-Aug-09 for APP/POL OPTIMISATION
        protected System.Web.UI.WebControls.Label lblAGENCY_DISPLAY_NAME;//Added by Charles on 21-Aug-09 for APP/POL OPTIMISATION
        protected System.Web.UI.WebControls.TextBox txtCSRNAME;
        //protected System.Web.UI.WebControls.TextBox txtPRODUCER;

        protected System.Web.UI.WebControls.HyperLink hlkCalandarDate;
        protected System.Web.UI.WebControls.HyperLink hlkInceptionDate;
        //protected System.Web.UI.WebControls.HyperLink hlkAPP_EXPIRATION_DATE;

        protected System.Web.UI.WebControls.RegularExpressionValidator revAPP_EFFECTIVE_DATE;
        //Added By Pradeep Kushwaha on 20-0ct-2010
        protected System.Web.UI.WebControls.TextBox txtAPP_TERMS;
        protected System.Web.UI.WebControls.RegularExpressionValidator revAPP_TERMS;
        //Added till here 
        //Added By Lalit Noc 16,2010
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvTRANSACTION_TYPE;

        protected System.Web.UI.WebControls.Label capOLD_POLICY_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtOLD_POLICY_NUMBER;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOLD_POLICY_NUMBER;
        public string str;
        #endregion

        #region Local form variables
        //START:*********** Local form variables *************
        string oldXML;
        private System.Resources.ResourceManager objResourceMgr;
        private string strRowId = "", strFormSaved;

        public string primaryKeyValues = "";
        public string strNewVersion = "";
        public int strVersionID = -1;
        private int AGENCY_ID;
        protected int gIntPopulate = 0, gIntShowQuote = 0, gIntShowVerificationResult = 0, gIntShowPolicyQuote = 0;
        protected string gStrOldXML = "";

        protected int gIntQuoteID = 0, gIntCUSTOMER_ID = 0, gIntAPP_ID = 0, gIntAPP_VERSION_ID = 0, gIntPOLICY_ID = 0, gIntPOLICY_VERSION_ID = 0;
        protected string gstrLobID = "";

        //Defining the business layer class object
        ClsGeneralInformation objGeneralInformation;

        //public string appTerms;
        public string divID;
        public string delStr = "0";

        public int deptID;
        public string PCID;

        //public string exp_Date;
        /* Commented by Charles on 8-Mar-2010 for Policy Page implementation
		private const string LOB_HOME="1";
		private const string LOB_PRIVATE_PASSENGER="2";
		private const string LOB_MOTORCYCLE="3";
		private const string LOB_WATERCRAFT="4";
		private const string LOB_UMBRELLA="5";
		private const string LOB_RENTAL_DWELLING="6";
        private const string LOB_GENERAL_LIABILITY="7";
        */
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOL_TRAN_TYPE;
        private DataSet dsPolicy;

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

            revAPP_EFFECTIVE_DATE.ValidationExpression = aRegExpDate;
            revAPP_INCEPTION_DATE.ValidationExpression = aRegExpDate;
            revPOLICY_LEVEL_COMISSION.ValidationExpression = aRegExpDoublePositiveWithZero;
            //revYEAR_AT_CURR_RESI.ValidationExpression = aRegExpInteger;
            //revRECEIVED_PRMIUM.ValidationExpression = aRegExpCurrencyformat;

            revAPP_EFFECTIVE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");
            revAPP_INCEPTION_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22");

            //revRECEIVED_PRMIUM.ErrorMessage		=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("611");
            //revYEAR_AT_CURR_RESI.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");

            rfvAPP_TERMS.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("93");
            rfvAPP_EFFECTIVE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("95");
            rfvAPP_EXPIRATION_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("96");
            rfvBILL_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("218");
            rfvDOWN_PAY_MODE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1062");
            rfvPOLICY_SUBLOB.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("98");
            rfvPOLICY_CURRENCY.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1070");
            rfvCO_INSURANCE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1071");
            rfvPAYOR.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1072");
            rfvINSTALL_PLAN_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("731");
            rfvUNDERWRITER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("943");
            rfvPOLICY_LEVEL_COMISSION.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1080");
            rfvCSR.ErrorMessage = ClsMessages.FetchGeneralMessage("1093");
            rfvPOLICY_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("428");
            //rfvPROXY_SIGN_OBTAINED.ErrorMessage =	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("479");			
            //rfvYEARS_AT_PREV_ADD.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("520");			

            //rfvPROPRTY_INSP_CREDIT.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("797");
            //rfvPIC_OF_LOC.ErrorMessage			=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("798");

            csvREFERAL_INSTRUCTIONS.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("445");
            csvPOLICY_LEVEL_COMISSION.ErrorMessage = ClsMessages.FetchGeneralMessage("1064");
            //csvYEARS_AT_PREV_ADD.ErrorMessage	=	Cms.CmsWeb.ClsMessages.FetchGeneralMessage("445");
            rfvDIV_ID_DEPT_ID_PC_ID.ErrorMessage = ClsMessages.FetchGeneralMessage("1092");
            rngPREFERENCE_DAY.ErrorMessage = ClsMessages.FetchGeneralMessage("1091");
            rfvState_ID.ErrorMessage = ClsMessages.FetchGeneralMessage("788");
            //rngYEAR_AT_CURR_RESI.ErrorMessage	=   Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
            hidRejectMsg.Value = ClsMessages.FetchGeneralMessage("1114");//Added By Pradeep Kushwaha on 06-07-2010 ,While reject the confirmation message will display

            //Added by Pradeep Kushwaha(21-oct-2010) for implementation of APP_TERMS on Textbox
            revAPP_TERMS.ValidationExpression = aRegExpIntegerPositiveNonZero;
            revAPP_TERMS.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1148");
            //Added Till Here 

            //Added by Lalit Nov 18,2010
            rfvTRANSACTION_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1195");
        }
        #endregion

        #region PageLoad event
        private void Page_Load(object sender, System.EventArgs e)
        {
            if (Request["CALLEDFROM"] != null && Request["CALLEDFROM"].ToString().ToUpper() != "TREEVIEWNAVIGATION")
                base.ReloadPolicyMenu("");
            Ajax.Utility.RegisterTypeForAjax(typeof(PolicyInformation));

            // for wolverine user only, display chkCOMPLETE_APP
            //string userID = GetSystemId();
            //if (userID.ToUpper() != CarrierSystemID.ToUpper())
            //{trCOMPLETE_APP.Visible=false;}
            //else
            //{trCOMPLETE_APP.Visible=true;}

            #region reading input
            hidCustomerID.Value = Request["CUSTOMER_ID"].ToString();
            hidPolicyID.Value = Request["POLICY_ID"].ToString();
            hidPolicyVersionID.Value = Request["POLICY_VERSION_ID"].ToString();
            hidLOBID.Value = Request["POLICY_LOB"].ToString();
            if (Request["CALLEDFROM"] != null)
                hidCalledFrom.Value = Request["CALLEDFROM"].ToString();
            #endregion

            #region To get and set the App Number and App Version
            int AppId, AppVersionId;
            ClsPolicyProcess objProcess = new ClsPolicyProcess();
            objGeneralInformation = new ClsGeneralInformation();
            objGeneralInformation.GetAppDetailsFromPolicy(Convert.ToInt32(hidCustomerID.Value), Convert.ToInt32(hidPolicyID.Value), Convert.ToInt32(hidPolicyVersionID.Value), out AppId, out AppVersionId);
            hidPOLICY_STATUS_CODE.Value = objGeneralInformation.GetStatusOfPolicy(Convert.ToInt32(hidCustomerID.Value), Convert.ToInt32(hidPolicyID.Value), Convert.ToInt32(hidPolicyVersionID.Value));
            if (Request["APP_ID"] != null && Request["APP_ID"].ToString() != "")
                hidAppID.Value = Request["APP_ID"].ToString();
            else
                hidAppID.Value = AppId.ToString();

            if (Request["APP_VERSION_ID"] != null && Request["APP_VERSION_ID"].ToString() != "")
                hidAppVersionID.Value = Request["APP_VERSION_ID"].ToString();
            else
                hidAppVersionID.Value = AppVersionId.ToString();

            #endregion
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            gIntCUSTOMER_ID = int.Parse(hidCustomerID.Value);
            gIntAPP_ID = int.Parse(hidAppID.Value);
            gIntAPP_VERSION_ID = int.Parse(hidAppVersionID.Value);
            gstrLobID = hidLOBID.Value; gIntShowVerificationResult = 1;

            lblMessage.Visible = false;
            capPREFERENCE_DAY.Visible = false;
            txtPREFERENCE_DAY.Visible = false;
            btnSendCancelNotice.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1452");
            //For Lookup
            string rootPath = Cms.BusinessLayer.BlCommon.ClsCommon.GetApplicationPath();
            string url = rootPath + @"/cmsweb/aspx/LookupForm1.aspx";

            string strSystemIDD = GetSystemId();

            //Prepare to read captions from resource file
            SetCultureThread(GetLanguageCode());
            objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.PolicyInformation", System.Reflection.Assembly.GetExecutingAssembly());

            if (!Page.IsPostBack)
            {
                btnReject.Attributes.Add("onclick", "javascript:return ShowAlertMessageWhileReject();");//Added by Pradeep Kushwaha on 06-07-2010
                //for calendar picker
                hlkCalandarDate.Attributes.Add("OnClick", "fPopCalendar(document.APP_LIST.txtAPP_EFFECTIVE_DATE,document.APP_LIST.txtAPP_EFFECTIVE_DATE)");
                hlkInceptionDate.Attributes.Add("OnClick", "fPopCalendar(document.APP_LIST.txtAPP_INCEPTION_DATE,document.APP_LIST.txtAPP_INCEPTION_DATE)");
                //hlkAPP_EXPIRATION_DATE.Attributes.Add("OnClick", "fPopCalendar(document.APP_LIST.txtAPP_EXPIRATION_DATE,document.APP_LIST.txtAPP_EXPIRATION_DATE)");               
                btnAppQuote.Attributes.Add("onClick", "ShowQuote();");
                btnPolicyQuote.Attributes.Add("onClick", "ShowPolicyQuote();");
                //cmbAPP_TERMS.Attributes.Add("onBlur","javascript:fillBillingPlan();return false;");  //Commented by Charles on 17-Sep-09 for APP/POL Optimization
                //btnSave.Attributes.Add("onclick","javascript:ChangeDefaultDate();");
                //btnSave.Attributes.Add("onclick","javascript:Validate_ChangeDefaultDate();");
                //txtRECEIVED_PRMIUM.Attributes.Add("onBlur","this.value=formatCurrency(this.value);"); 
                btnCustomerAssistant.Attributes.Add("onclick", "javascript:return DoBackToAssistant();");
                btnBack.Attributes.Add("onclick", "javascript:return DoBack();");
                btnSave.Attributes.Add("onclick", "javascript:return Page_ClientValidate();");
                //For Reset
                btnReset.Attributes.Add("onclick", "javascript:document.getElementById('hidFormSaved').value = '0';return ResetForm1();"); //" + Page.Controls[0].ID + "' );");
                txtAPP_EFFECTIVE_DATE.Attributes.Add("onblur", "javascript:setTimeout('ChangeDefaultDate();',100);");//DisplaySubLobonEffDatChg();DisplaySubLobonPolVerEffDatChg();                
                txtAPP_EFFECTIVE_DATE.Attributes.Add("onChange", "javascript:ChangeDefaultDate();");
                //txtYEAR_AT_CURR_RESI.Attributes.Add("onblur","javascript:DisplayPreviousYearDesc()");
                #region Commented by Pradeep Kushwaha(21-oct-2010) for implementation of APP_TERMS on Textbox
                //cmbAPP_TERMS.Attributes.Add("onChange", "return cmbAPP_TERMS_Change();"); //Added by Charles on 17-Sep-09 for APP/POL Optimization
                // cmbAPP_TERMS.Attributes.Add("onkeypress", "javascript:if(event.keyCode==13){ChangeDefaultDate();fillBillingPlan();}");//Added by Charles on 6-Oct-09 for Itrack 6162
                #endregion
                txtAPP_TERMS.Attributes.Add("onkeypress", "javascript:if(event.keyCode==13){setExpDate();fillBillingPlan();}");//Added by Pradeep Kushwaha on 21-Oct-2010
                // set captions
                SetCaptions();
                SetCookieValue();
                // set error messages
                SetErrorMessages();
                SetCalledFor("POLICY");
                btnCopyPolicy.Attributes.Add("onclick", "javascript:TransaferPolicyToNewClient();");

                //Check billing plan
                //CheckBillingPlan();

                //Only For Home
                //if(	hidLOBID.Value=="1" || hidLOBID.Value=="6")
                //{
                //    trPropInspCr.Visible=true;					
                //}
                //else
                //{
                //    trPropInspCr.Visible=false;					
                //}
                //Only For Home And Rental
                //Load The ComboBox
                //if(	hidLOBID.Value=="1" || hidLOBID.Value =="6" || hidLOBID.Value =="2")
                //{
                //    LoadCombo();
                //    policyTR.Visible=hidLOBID.Value !="2"?true:false; 
                //}
                //else
                //{					
                //    policyTR.Visible=false; 
                //}

                LoadCombo();
                FillControls();// Fill controls				

                LoadData();// Fetchdata of the policy				

                //txtAPP_INCEPTION_DATE.Text = DateTime.Now.ToString("MM/dd/yyyy");				
                //txtAPP_EFFECTIVE_DATE.Text = DateTime.Now.ToString("MM/dd/yyyy");

                //hide the form message tablerow
                trFORMMESSAGE.Visible = false;

                //appTerms=cmbAPP_TERMS.SelectedItem==null?"":cmbAPP_TERMS.SelectedItem.Value ;  
                //exp_Date=txtAPP_EXPIRATION_DATE.Text.Trim();

                //SetMultiPolicy();
                CheckBillingPlan();
                ShowMessages();
                //Added by Rajiv on Nov-10 as SetPage controls was missing
                if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "Policies/support/PageXML/" + GetSystemId(), "PolicyInformation.xml"))
                {
                    setPageControls(Page, @Request.PhysicalApplicationPath + "Policies/support/PageXML/" + GetSystemId() + "/PolicyInformation.xml");
                }
                //
            }
            SetPolicyCookies(ClsCommon.FetchValueFromXML("POLICY_NUMBER", hidOldData.Value), hidCustomerID.Value, hidPolicyID.Value, hidPolicyVersionID.Value, hidAppID.Value, hidAppVersionID.Value, hidLOBID.Value);

            #region Setting screen id
            switch (gstrLobID)
            {
                case "1": // HOME
                    base.ScreenId = "224_1";
                    break;
                case "2": // Private passenger automobile                    
                    base.ScreenId = "224_2";
                    break;
                case "3": // Motorcycle
                    base.ScreenId = "224_3";
                    break;
                case "4": // Watercraft
                    base.ScreenId = "224_4";
                    break;
                case "5": // Umbrella
                    base.ScreenId = "224_6";
                    break;
                case "6": // Rental dwelling
                    base.ScreenId = "224_5";
                    break;
                case "7": // General liability
                    base.ScreenId = "224_7";
                    break;
                case "8": // Aviation
                    base.ScreenId = "224_13";
                    break;
                case "9":
                    base.ScreenId = "224_14";
                    break;
                case "10":
                    base.ScreenId = "224_15";
                    break;
                case "11":
                    base.ScreenId = "224_16";
                    break;
                case "12":
                    base.ScreenId = "224_17";
                    break;
                case "13":
                    base.ScreenId = "224_18";
                    break;
                case "14":
                    base.ScreenId = "224_19";
                    break;
                case "15":
                    base.ScreenId = "224_20";
                    break;
                case "16":
                    base.ScreenId = "224_21";
                    break;
                case "17":
                    base.ScreenId = "224_22";
                    break;
                case "18":
                    base.ScreenId = "224_23";
                    break;
                case "19":
                    base.ScreenId = "224_24";
                    break;
                case "20":
                    base.ScreenId = "224_27";
                    break;
                //Added By Pradeep Kushwaha on 28-April-2010
                case "21":
                    base.ScreenId = "224_35";
                    break;
                case "22":
                    base.ScreenId = "224_36";
                    break;
                case "23":
                    base.ScreenId = "224_37";
                    break;
                case "25":
                    base.ScreenId = "224_48";
                    break;
                case "35":
                    base.ScreenId = "224_49";
                    break;
                case "36"://Dpvat2(DPVAT(Cat. 1,2,9 e 10))
                    base.ScreenId = "224_50";
                    break;
                case "37"://Dpvat2(DPVAT(Cat. 1,2,9 e 10))
                    base.ScreenId = "224_51";
                    break;
                case "38"://Dpvat2(DPVAT(Cat. 1,2,9 e 10))
                    base.ScreenId = "224_45";
                    break;
                //End Added 
                default:
                    base.ScreenId = "224_2";
                    break;
            }

            #endregion
            //base.ScreenId	=	"201_2";

            SetButtonsSecurityXml();

            int intReturn = ClsGeneralInformation.GetPolicyStatus(int.Parse(hidCustomerID.Value == "" ? "0" : hidCustomerID.Value), int.Parse(hidPolicyID.Value == "" ? "0" : hidPolicyID.Value), int.Parse(hidPolicyVersionID.Value == "" ? "0" : hidPolicyVersionID.Value));

            SetQuoteValues();

            //Set the Policy Status Value.
            //hidPolicyStatus.Value = txtPOLICY_STATUS.Text; 
            hidPolicyStatus.Value = lblPOLICY_STATUS.Text;
            SetReadOnlyFields();
            //Added b y Lalit for set Currency symbol;
            ClsCommon.setPolicyCurrencySymbol(ClsCommon.FetchValueFromXML("CURRENCY_SYMBOL", hidOldData.Value));
            //setCarrierInsuredBill();
        }
        #endregion
        
        private void SetButtonsSecurityXml()
        {
            string userID = GetSystemId();

            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnChangeBillType.CmsButtonClass = CmsButtonType.Write;
            btnChangeBillType.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            //if (hidPOLICY_STATUS_CODE.Value.Trim() == "NORMAL" && userID.ToUpper() == CarrierSystemID.ToUpper())
            //btnSave.PermissionString = "<Security><Read>Y</Read><Write>Y</Write><Delete>Y</Delete><Execute>Y</Execute></Security>";
            //else
            btnSave.PermissionString = gstrSecurityXML;

            btnAppQuote.CmsButtonClass = CmsButtonType.Read;
            btnAppQuote.PermissionString = gstrSecurityXML;


            btnPolicyQuote.CmsButtonClass = CmsButtonType.Read;
            btnPolicyQuote.PermissionString = gstrSecurityXML;

            btnCustomerAssistant.CmsButtonClass = CmsButtonType.Read;
            btnCustomerAssistant.PermissionString = gstrSecurityXML;

            btnBack.CmsButtonClass = CmsButtonType.Read;
            btnBack.PermissionString = gstrSecurityXML;

            btnReject.CmsButtonClass = CmsButtonType.Read;
            btnReject.PermissionString = gstrSecurityXML;// "<Security><Read>Y</Read><Write>N</Write><Delete>N</Delete><Execute>N</Execute></Security>";

            btnCopy.CmsButtonClass = CmsButtonType.Read;
            btnCopy.PermissionString = gstrSecurityXML;

            btnSendCancelNotice.CmsButtonClass = CmsButtonType.Write;
            if (hidPOLICY_STATUS_CODE.Value.Trim().ToUpper() == ClsPolicyProcess.POLICY_STATUS_NORMAL)
                btnSendCancelNotice.PermissionString = "<Security><Read>Y</Read><Write>Y</Write><Delete>Y</Delete><Execute>Y</Execute></Security>";
            else
                btnSendCancelNotice.PermissionString = "<Security><Read>N</Read><Write>N</Write><Delete>Y</Delete><Execute>N</Execute></Security>";

            btnCopyPolicy.CmsButtonClass = CmsButtonType.Read;
            if (hidPOLICY_STATUS_CODE.Value.Trim() == "CANCEL" || hidPOLICY_STATUS_CODE.Value.Trim() == "SCANCEL")
                btnCopyPolicy.PermissionString = "<Security><Read>Y</Read><Write>Y</Write><Delete>Y</Delete><Execute>Y</Execute></Security>";
            else
                btnCopyPolicy.PermissionString = "<Security><Read>N</Read><Write>N</Write><Delete>Y</Delete><Execute>N</Execute></Security>";
        }

        /// <summary>
        /// Show Save Message
        /// </summary>
        /// Function added by Charles on 4-Mar-2010 for Policy Page Implementation
        private void ShowMessages()
        {
            if (Session["LoadedAfterSave"] != null)
            {
                if (Session["LoadedAfterSave"].ToString().ToUpper() == "TRUE")
                {
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("1082");
                    lblMessage.Visible = true;
                    Session["LoadedAfterSave"] = null;
                }
            }
        }

        private void LoadCombo()
        {
            /*
            int stateId=Cms.BusinessLayer.BlApplication.ClsVehicleInformation.GetStateIdForpolicy(int.Parse(hidCustomerID.Value.ToString()),hidPolicyID.Value.ToString(),hidPolicyVersionID.Value.ToString());
			
             */
            try
            {


                string SubLobXML = ClsCommon.GetXmlForLobWithoutState();
                System.IO.StringReader sr = new System.IO.StringReader(SubLobXML);
                DataSet ds = new DataSet();
                ds.ReadXml(sr);
                DataTable dt = ds.Tables[0];
                DataView dvSubLob = new DataView(dt);
                dvSubLob.RowFilter = "LOB_ID=" + hidLOBID.Value;// +" AND STATE_ID=" + stateId.ToString();

                cmbPOLICY_SUBLOB.DataSource = dvSubLob;
                cmbPOLICY_SUBLOB.DataTextField = "SUB_LOB_DESC";
                cmbPOLICY_SUBLOB.DataValueField = "SUB_LOB_ID";
                cmbPOLICY_SUBLOB.DataBind();
                cmbPOLICY_SUBLOB.Items.Insert(0, "");
                ds.Dispose();
                sr.Close();


                DataSet dsState = new DataSet();
                ClsStates objClsStates = new ClsStates();
                if (hidLOBID.Value != "" && hidLOBID.Value != null)
                {
                    dsState = objClsStates.PopLateAssignedState(int.Parse(hidLOBID.Value));
                    if (dsState != null && dsState.Tables.Count > 0)
                    {
                        cmbState_ID.DataSource = dsState.Tables[0];
                        cmbState_ID.DataTextField = "STATE_NAME";
                        cmbState_ID.DataValueField = "STATE_ID";
                        cmbState_ID.DataBind();
                        cmbState_ID.Items.Insert(0, new ListItem("", ""));
                        //setAgencyColor(dsTemp.Tables[0]);

                        dsState.Dispose();
                    }


                    if (int.Parse(hidLOBID.Value) <= 8)  // added by sonal to implemet state for old products specially product ID < 8
                    {
                        tblstate.Attributes.Add("style", "display:none");
                    }
                    else
                    {
                        tblstate.Attributes.Add("style", "display:none");
                    }
                    if (int.Parse(hidLOBID.Value) == 1 || int.Parse(hidLOBID.Value) == 6) //For Homeowners.
                    {
                        div_poltype.Attributes.Add("style", "display:none");

                    }

                    else
                    {
                        div_poltype.Attributes.Add("style", "display:none");

                    }

                }

            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }

        //Added to set Insured Bill for Carrier same as Agency {W001 in our case}		
        //private void setCarrierInsuredBill()
        //{
        //    string strCarrier = CarrierSystemID;//System.Configuration.ConfigurationManager.AppSettings.Get("CarrierSystemID").ToString();
        //    /*string agencyCode=hidAgencyCode.Value; //Commented by Charles on 17-Sep-09 for APP/POL Optimization		
        //    if(agencyCode.ToUpper().IndexOf(strCarrier.ToUpper()) == 0) */
        //    if(hidAPP_AGENCY_ID.Value.Trim().Equals("27"))//Added by Charles on 17-Sep-09 for APP/POL Optimization 
        //    {
        //        for(int index =0;index < cmbBILL_TYPE.Items.Count;index++)
        //        {
        //            if(cmbBILL_TYPE.Items[index].Value == "8460" || cmbBILL_TYPE.Items[index].Value == "11150")
        //            {
        //                cmbBILL_TYPE.SelectedIndex = index;
        //                cmbBILL_TYPE.Enabled = false;
        //                break;
        //            }
        //        }
        //    }
        //    /*else 
        //    {
        //        //cmbBILL_TYPE.Enabled = true;
        //        //cmbBILL_TYPE_ID.SelectedIndex = -1;
        //    }*/

        //}

        private void SetPolicyCookies(string polNo, string custID, string polId, string polVersionID, string aAppID, string aAppVersionID, string aLOBID)
        {
            //Added Mohit Agarwal 19-Feb 2007 to store last 3 cookies
            # region last 3 cookies
            string AgencyId = GetSystemId();
            if (AgencyId.ToUpper() != CarrierSystemID)
            {
                Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastPageVisited = new ClsGeneralInformation();
                DataSet ds = objLastPageVisited.GetLastVisitedPageEntry(int.Parse(GetUserId()), AgencyId);

                //if(System.Web.HttpContext.Current.Request.Cookies["polNo" + GetSystemId() + "_" + GetUserId() + "_1"] != null)
                if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows[0]["LAST_VISITED_POLICY"].ToString() != "")
                {
                    //string prevCook = System.Web.HttpContext.Current.Request.Cookies["polNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value;
                    string prevCook = ds.Tables[0].Rows[0]["LAST_VISITED_POLICY"].ToString();
                    string[] cookArr = prevCook.Split('@');
                    if (cookArr.Length > 0 && cookArr.Length < 4)
                    {
                        //System.Web.HttpContext.Current.Response.Cookies["polNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value = polNo + "~" + custID + "~" + polId + "~" + polVersionID + "~" +  aAppID + "~" + aAppVersionID + "~" + aLOBID + "~" + DateTime.Today.Date + "@" + System.Web.HttpContext.Current.Request.Cookies["polNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value;
                        string Pol_Details = polNo + "~" + custID + "~" + polId + "~" + polVersionID + "~" + aAppID + "~" + aAppVersionID + "~" + aLOBID + "~" + DateTime.Today.Date + "@" + ds.Tables[0].Rows[0]["LAST_VISITED_POLICY"].ToString();
                        Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedPolicy = new ClsGeneralInformation();
                        objLastVisitedPolicy.UpdateLastVisitedPageEntry("Policy", Pol_Details, int.Parse(GetUserId()), AgencyId);
                    }
                    else if (cookArr.Length >= 4)
                    {
                        int maxindex = cookArr.Length - 1;
                        if (maxindex >= 3)
                            maxindex = 2;

                        //System.Web.HttpContext.Current.Response.Cookies["polNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value = polNo + "~" + custID + "~" + polId + "~" + polVersionID + "~" +  aAppID + "~" + aAppVersionID + "~" + aLOBID + "~" + DateTime.Today.Date + "@";
                        string Pol_Details = polNo + "~" + custID + "~" + polId + "~" + polVersionID + "~" + aAppID + "~" + aAppVersionID + "~" + aLOBID + "~" + DateTime.Today.Date + "@";
                        Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedPolicy = new ClsGeneralInformation();
                        objLastVisitedPolicy.UpdateLastVisitedPageEntry("Policy", Pol_Details, int.Parse(GetUserId()), AgencyId);

                        for (int cookindex = 0; cookindex < maxindex; cookindex++)
                        {
                            //System.Web.HttpContext.Current.Response.Cookies["polNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value += cookArr[cookindex] + "@";
                            Pol_Details += cookArr[cookindex] + "@";
                        }
                        objLastVisitedPolicy.UpdateLastVisitedPageEntry("Policy", Pol_Details, int.Parse(GetUserId()), AgencyId);
                    }
                    else
                    {
                        //System.Web.HttpContext.Current.Response.Cookies["polNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value=polNo + "~" + custID + "~" + polId + "~" + polVersionID + "~" +  aAppID + "~" + aAppVersionID + "~" + aLOBID + "~" + DateTime.Today.Date + "@";
                        string Pol_Details = polNo + "~" + custID + "~" + polId + "~" + polVersionID + "~" + aAppID + "~" + aAppVersionID + "~" + aLOBID + "~" + DateTime.Today.Date + "@";
                        Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedPolicy = new ClsGeneralInformation();
                        objLastVisitedPolicy.UpdateLastVisitedPageEntry("Policy", Pol_Details, int.Parse(GetUserId()), AgencyId);
                    }
                    //System.Web.HttpContext.Current.Response.Cookies["polNo" + GetSystemId() + "_" + GetUserId() + "_1"].Expires=DateTime.MaxValue;
                }
                else
                {
                    //					System.Web.HttpContext.Current.Response.Cookies["polNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value=polNo + "~" + custID + "~" + polId + "~" + polVersionID + "~" +  aAppID + "~" + aAppVersionID + "~" + aLOBID + "~" + DateTime.Today.Date + "@";
                    //					System.Web.HttpContext.Current.Response.Cookies["polNo" + GetSystemId() + "_" + GetUserId() + "_1"].Expires=DateTime.MaxValue;
                    string Policy_Details = polNo + "~" + custID + "~" + polId + "~" + polVersionID + "~" + aAppID + "~" + aAppVersionID + "~" + aLOBID + "~" + DateTime.Today.Date + "@";
                    Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedPolicy = new ClsGeneralInformation();
                    objLastVisitedPolicy.UpdateLastVisitedPageEntry("Policy", Policy_Details, int.Parse(GetUserId()), AgencyId);
                }
            }
            #endregion
            else
            {
                string Policy_Details = polNo + "~" + custID + "~" + polId + "~" + polVersionID + "~" + aAppID + "~" + aAppVersionID + "~" + aLOBID + "~" + DateTime.Today.Date;
                Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedPolicy = new ClsGeneralInformation();
                objLastVisitedPolicy.UpdateLastVisitedPageEntry("Policy", Policy_Details, int.Parse(GetUserId()), AgencyId);
            }
        }

        #region LoadData()
        private void LoadData()
        {
            if (int.Parse(hidLOBID.Value) == 1) //For Homeowners.
            {
                //if (int.Parse(hidSTATE_ID.Value) == 22)
                //{
                    //cmbPOLICY_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("HOPTPM");
                    //cmbPOLICY_SUBLOB.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("HOPTPM");
                //}
                //else
                //{
                    cmbPOLICY_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("HOPTYP");
                    cmbPOLICY_SUBLOB.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("HOPTYP");
                //}
            }
          
            //--------------------------End--------------------.
            cmbPOLICY_TYPE.DataTextField = "LookupDesc";
            cmbPOLICY_TYPE.DataValueField = "LookupID";
            cmbPOLICY_TYPE.DataBind();
            cmbPOLICY_TYPE.Items.Insert(0, "");

            //Modified by Abhishek Goel on dated 17/02/2012
            if (int.Parse(hidLOBID.Value) == 1) //For Homeowners.
            {
                cmbPOLICY_SUBLOB.DataTextField = "LookupDesc";
                cmbPOLICY_SUBLOB.DataValueField = "LookupID";
                cmbPOLICY_SUBLOB.DataBind();
                cmbPOLICY_SUBLOB.Items.Insert(0, "");//comment by kuldeep on 16_feb_2012
            }

            objGeneralInformation = new ClsGeneralInformation();
            dsPolicy = new DataSet();
            dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCustomerID.Value == "" ? "0" : hidCustomerID.Value), int.Parse(hidPolicyID.Value == "" ? "0" : hidPolicyID.Value), int.Parse(hidPolicyVersionID.Value == "" ? "0" : hidPolicyVersionID.Value));
            //hidOldData.Value = dsPolicy.GetXml();
            hidOldData.Value = ClsCommon.GetXMLEncoded(dsPolicy.Tables[0]);

            txtCUSTOMERNAME.Text = dsPolicy.Tables[0].Rows[0]["CUSTOMERNAME"].ToString();
            //txtPOLICY_STATUS.Text = dsPolicy.Tables[0].Rows[0]["POLICY_STATUS"].ToString();
            lblPOLICY_STATUS.Text = dsPolicy.Tables[0].Rows[0]["POLICY_STATUS"].ToString();

            //For Itrack - 500 (Added by shikha)
            //if (hidPOLICY_STATUS_CODE.Value.ToUpper() == ClsPolicyProcess.POLICY_STATUS_NORMAL)//Added by Pradeep on 28-Sep-2010
            //    btnSendCancelNotice.Visible = true;
            string status = objResourceMgr.GetString("Policy_Reject");

            if (lblPOLICY_STATUS.Text == status)
            {

                //btnReject.PermissionString = gstrSecurityXML;// "<Security><Read>Y</Read><Write>N</Write><Delete>N</Delete><Execute>N</Execute></Security>";

                btnCopy.Visible = true;
                btnReject.Visible = false;
                btnSave.Visible = false;
                btnReset.Visible = false;

            }
            if (lblPOLICY_STATUS.Text.ToUpper() == "SUSPENDED")
            {
                btnReject.Visible = true;
            }
            //txtSTATE_NAME.Text = dsPolicy.Tables[0].Rows[0]["STATE_NAME"].ToString();
            txtPOLICY_LOB.Text = dsPolicy.Tables[0].Rows[0]["LOB"].ToString();

            SetSUB_LOB_ID(dsPolicy.Tables[0].Rows[0]["SUB_LOB_ID"].ToString()); //Added by Charles on 13-Apr-10 for Clause Page
            SetPolicyCurrency(dsPolicy.Tables[0].Rows[0]["POLICY_CURRENCY"].ToString());
            hidIS_ACTIVE.Value = dsPolicy.Tables[0].Rows[0]["IS_ACTIVE"].ToString();
            hidPolTermEffDate.Value = dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString();
            if (dsPolicy.Tables[0].Rows[0]["IS_REWRITE_POLICY"].ToString() == "Y" || dsPolicy.Tables[0].Rows[0]["IS_REWRITE_POLICY"].ToString() == "y")
            {
                lblIS_REWRITE_POLICY.Text = "Yes";
                trIsRewritePolicy.Visible = true;
            }
            else
            {
                trIsRewritePolicy.Visible = false;
            }
            string strDivDepId = dsPolicy.Tables[0].Rows[0]["DIV_ID"].ToString() + "^" +
                dsPolicy.Tables[0].Rows[0]["DEPT_ID"].ToString() + "^" +
                dsPolicy.Tables[0].Rows[0]["PC_ID"].ToString();

            cmbDIV_ID_DEPT_ID_PC_ID.SelectedIndex = cmbDIV_ID_DEPT_ID_PC_ID.Items.IndexOf(cmbDIV_ID_DEPT_ID_PC_ID.Items.FindByValue(strDivDepId));

            cmbDIV_ID_DEPT_ID_PC_ID.Enabled = false;

            //cmbPOLICY_SUBLOB.SelectedIndex = cmbPOLICY_SUBLOB.Items.IndexOf(cmbPOLICY_SUBLOB.Items.FindByValue(dsPolicy.Tables[0].Rows[0]["POLICY_SUBLOB"].ToString()));


            if ((dsPolicy.Tables[0].Rows[0]["POLICY_SUBLOB"].ToString() != null)||(dsPolicy.Tables[0].Rows[0]["POLICY_SUBLOB"].ToString() != ""))
            {
                cmbPOLICY_SUBLOB.SelectedValue = dsPolicy.Tables[0].Rows[0]["POLICY_SUBLOB"].ToString();
            }
            
            string sLObId = dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString();
            /*if select product not master policy product the transaction type 'open policy' should not available 
             for that product
             
            if (!(sLObId != "" && (int.Parse(sLObId) == 17 || int.Parse(sLObId) == 18
                                    || int.Parse(sLObId) == 21 || int.Parse(sLObId) == 34
                                    || int.Parse(sLObId) == 22)))
            {
                ListItem item = cmbTRANSACTION_TYPE.Items.FindByValue(MASTER_POLICY);
                if (item != null)
                    cmbTRANSACTION_TYPE.Items.Remove(item);

            }
             commented by lalit transaction type dropdown not selected (lobId= 22 and trantype=openPolicy) .i-track# -938
             */
            cmbTRANSACTION_TYPE.Enabled = false;

            cmbTRANSACTION_TYPE.SelectedIndex = cmbTRANSACTION_TYPE.Items.IndexOf(cmbTRANSACTION_TYPE.Items.FindByValue(dsPolicy.Tables[0].Rows[0]["TRANSACTION_TYPE"].ToString()));

            hidPOL_TRAN_TYPE.Value = dsPolicy.Tables[0].Rows[0]["TRANSACTION_TYPE"].ToString();

            // txtPREFERENCE_DAY.Text = dsPolicy.Tables[0].Rows[0]["PREFERENCE_DAY"].ToString() == "0" ? "" : dsPolicy.Tables[0].Rows[0]["PREFERENCE_DAY"].ToString();

            txtPOLICY_NUMBER.Text = dsPolicy.Tables[0].Rows[0]["POLICY_NUMBER"].ToString();

            txtPOLICY_DISP_VERSION.Text = dsPolicy.Tables[0].Rows[0]["POLICY_DISP_VERSION"].ToString();

            if (dsPolicy.Tables[0].Rows[0]["APP_TERMS"].ToString() != "")
            {
                #region Commented by Pradeep Kushwaha(21-oct-2010) for implementation of APP_TERMS on Textbox
                ////Added by Charles on 6-Jan-09 for Itrack 6905
                //ListItem li=new ListItem(); 
                //li=cmbAPP_TERMS.Items.FindByValue(dsPolicy.Tables[0].Rows[0]["APP_TERMS"].ToString());   
                //if(li!=null)//Added till here
                //    cmbAPP_TERMS.SelectedValue = dsPolicy.Tables[0].Rows[0]["APP_TERMS"].ToString();
                #endregion

                txtAPP_TERMS.Text = dsPolicy.Tables[0].Rows[0]["APP_TERMS"].ToString();
                txtAPP_TERMS.Enabled = false;

            }

            txtAPP_EFFECTIVE_DATE.Text = ConvertDBDateToCulture(dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString());
            txtAPP_EXPIRATION_DATE.Text = ConvertDBDateToCulture(dsPolicy.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString());
            txtAPP_INCEPTION_DATE.Text = ConvertDBDateToCulture(dsPolicy.Tables[0].Rows[0]["APP_INCEPTION_DATE"].ToString());
            hidAPP_INCEPTION_DATE.Value = ConvertDBDateToCulture(dsPolicy.Tables[0].Rows[0]["APP_INCEPTION_DATE"].ToString());

            if ((dsPolicy.Tables[0].Rows[0]["FUND_TYPE"].ToString() != null) || (dsPolicy.Tables[0].Rows[0]["FUND_TYPE"].ToString() != ""))
            {
                cmbFUND_TYPE.SelectedValue = dsPolicy.Tables[0].Rows[0]["FUND_TYPE"].ToString();
            }                     

            lblAGENCY_DISPLAY_NAME.Text = dsPolicy.Tables[0].Rows[0]["AGENCY_NAME_ACTIVE_STATUS"].ToString();

            if (dsPolicy.Tables[0].Rows[0]["REN_BROKER_COUNT"] != null && Convert.ToInt32(dsPolicy.Tables[0].Rows[0]["REN_BROKER_COUNT"].ToString().Trim()) > 1)
            {
                lblAGENCY_DISPLAY_NAME.Text = lblAGENCY_DISPLAY_NAME.Text + " \"" + objResourceMgr.GetString("lblBrokerSplit") + "\"";
            }

            hidAgencyCode.Value = dsPolicy.Tables[0].Rows[0]["AGENCY_CODE"].ToString().Trim();
            if (dsPolicy.Tables[0].Rows[0]["IS_TERMINATED"].ToString() == "Y")//Added by Charles on 24-Aug-09 for APP/POL Optimization
            {
                lblAGENCY_DISPLAY_NAME.BackColor = Color.Lavender;
                lblAGENCY_DISPLAY_NAME.ForeColor = Color.Red;
            }//Added till here	

            string CSR = dsPolicy.Tables[0].Rows[0]["CSRNAME"].ToString();
            string strCSR = "";
            if (CSR != "0" && CSR != null && CSR != "")
            {
                strCSR = CSR.Substring(0, CSR.IndexOf("-")).ToString();
            }
            cmbCSR.SelectedIndex = cmbCSR.Items.IndexOf(cmbCSR.Items.FindByValue(strCSR.Trim()));
            cmbPRODUCER.SelectedIndex = cmbPRODUCER.Items.IndexOf(cmbPRODUCER.Items.FindByValue(dsPolicy.Tables[0].Rows[0]["PRODUCER"].ToString()));
            cmbFUND_TYPE.SelectedIndex = cmbFUND_TYPE.Items.IndexOf(cmbFUND_TYPE.Items.FindByValue(dsPolicy.Tables[0].Rows[0]["FUND_TYPE"].ToString()));


            //cmbUNDERWRITER.SelectedIndex =cmbUNDERWRITER.Items.IndexOf(cmbUNDERWRITER.Items.FindByValue(dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()));
            hidCSR.Value = strCSR.Trim();
            hidPRODUCER.Value = dsPolicy.Tables[0].Rows[0]["PRODUCER"].ToString().Trim();

            hidUnderwriter.Value = dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString().Trim();


            if (dsPolicy.Tables[0].Rows[0]["BILL_TYPE"].ToString() != "")
            {
                ListItem li = new ListItem();
                li = cmbBILL_TYPE.Items.FindByValue(Convert.ToString(dsPolicy.Tables[0].Rows[0]["BILL_TYPE"].ToString()));
                if (li != null)
                    cmbBILL_TYPE.SelectedIndex = cmbBILL_TYPE.Items.IndexOf(li);
            }

            if (dsPolicy.Tables[0].Rows[0]["BILLTYPE"].ToString() != "")
                hidBILLTYPE.Value = dsPolicy.Tables[0].Rows[0]["BILLTYPE"].ToString();

            hidAPP_AGENCY_ID.Value = dsPolicy.Tables[0].Rows[0]["AGENCY_ID"].ToString() == "" ? "0" : dsPolicy.Tables[0].Rows[0]["AGENCY_ID"].ToString();
            hidSTATE_ID.Value = dsPolicy.Tables[0].Rows[0]["STATE_ID"].ToString();


           




            if (hidLOBID.Value != "" && hidLOBID.Value != null)
            {
                if (int.Parse(hidLOBID.Value) <= 8 && int.Parse(hidLOBID.Value) != 1)
                {
                    cmbState_ID.SelectedValue = dsPolicy.Tables[0].Rows[0]["STATE_ID"].ToString();
                }

                if (int.Parse(hidLOBID.Value) == 1 || int.Parse(hidLOBID.Value) == 6)
                {
                    cmbPOLICY_TYPE.SelectedValue = dsPolicy.Tables[0].Rows[0]["POLICY_TYPE"].ToString();
                    //cmbPOLICY_SUBLOB.SelectedValue = dsPolicy.Tables[0].Rows[0]["POLICY_TYPE"].ToString();
                }
            }


            //			if (dsPolicy.Tables[0].Rows[0]["SUB_LOB_DESC"].ToString() == "")
            //				txtSUB_LOB_DESC.Text = "N.A.";
            //			else
            //				txtSUB_LOB_DESC.Text = dsPolicy.Tables[0].Rows[0]["SUB_LOB_DESC"].ToString();   

            //if (dsPolicy.Tables[0].Rows[0]["PROXY_SIGN_OBTAINED"].ToString() != "")
            //    cmbPROXY_SIGN_OBTAINED.SelectedValue = dsPolicy.Tables[0].Rows[0]["PROXY_SIGN_OBTAINED"].ToString();
            //Changed string AGENCY_ID to hidAPP_AGENCY_ID, by Charles on 21-Aug-09 for APP/POL OPTIMISATION

            /*
        if (dsPolicy.Tables[0].Rows[0]["CSRNAME"].ToString() != "")
            //txtCSRNAME.Text = dsPolicy.Tables[0].Rows[0]["CSRNAME"].ToString();
            cmbCSR.SelectedValue= dsPolicy.Tables[0].Rows[0]["CSRNAME"].ToString();
        else
            //txtCSRNAME.Text = "N.A.";
            cmbCSR.SelectedIndex= -1;
            
        if (dsPolicy.Tables[0].Rows[0]["PRODUCER"]!=null && dsPolicy.Tables[0].Rows[0]["PRODUCER"].ToString() != "" && int.Parse(dsPolicy.Tables[0].Rows[0]["PRODUCER"].ToString()) != 0 )
            //txtPRODUCER.Text = dsPolicy.Tables[0].Rows[0]["PRODUCER"].ToString();
            cmbPRODUCER.SelectedValue = dsPolicy.Tables[0].Rows[0]["PRODUCER"].ToString();
        else
            //txtPRODUCER.Text = "N.A.";
            cmbPRODUCER.SelectedIndex = -1;
        */

            //if (dsPolicy.Tables[0].Rows[0]["BILL_TYPE"].ToString() != "")
            //	cmbBILL_TYPE.SelectedValue = dsPolicy.Tables[0].Rows[0]["BILL_TYPE"].ToString();

            //FillUnderWriter(int.Parse(hidAPP_AGENCY_ID.Value)); //Fill Policy Agency's Underwritters as discussed With Rajan Sir Itrack# 4901

            //			if (dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString() != "")
            //			{
            //				cmbUNDERWRITER.SelectedIndex =cmbUNDERWRITER.Items.IndexOf(cmbUNDERWRITER.Items.FindByValue(dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()));
            //			}

            /*		if (dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString() != "")
                    {
                        ListItem li=new ListItem(); 
                        li=cmbUNDERWRITER.Items.FindByValue(Convert.ToString(dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()));
                        if(li!=null)
                            cmbUNDERWRITER.SelectedIndex=cmbUNDERWRITER.Items.IndexOf(li);
                    }
        */

            //if (dsPolicy.Tables[0].Rows[0]["CHARGE_OFF_PRMIUM"].ToString() != "")
            //    cmbCHARGE_OFF_PRMIUM.SelectedValue = dsPolicy.Tables[0].Rows[0]["CHARGE_OFF_PRMIUM"].ToString();

            //if (dsPolicy.Tables[0].Rows[0]["RECEIVED_PRMIUM"].ToString().Trim() == "0")
            //	txtRECEIVED_PRMIUM.Text = "";
            //else
            //txtRECEIVED_PRMIUM.Text = dsPolicy.Tables[0].Rows[0]["RECEIVED_PRMIUM"].ToString();

            //if (dsPolicy.Tables[0].Rows[0]["YEAR_AT_CURR_RESI"].ToString() != "")
            //    txtYEAR_AT_CURR_RESI.Text = dsPolicy.Tables[0].Rows[0]["YEAR_AT_CURR_RESI"].ToString();
            //txtYEARS_AT_PREV_ADD.Text = dsPolicy.Tables[0].Rows[0]["YEARS_AT_PREV_ADD"].ToString();

            #region ToSet the Active Installment Plan Values in Billing Plan
            //if (Request["APP_ID"].ToString() != "")
            //{
            try
            {
                string TempVal = ClsCommon.FetchValueFromXML("INSTALL_PLAN_ID", hidOldData.Value);
                //cmbINSTALL_PLAN_ID.DataSource =ClsDepositDetails.FetchActiveInstallmentPlans(int.Parse(TempVal));
                //cmbINSTALL_PLAN_ID.DataTextField = "BILLING_PLAN";
                //cmbINSTALL_PLAN_ID.DataValueField = "INSTALL_PLAN_ID"; 
                //cmbINSTALL_PLAN_ID.DataBind();
                cmbINSTALL_PLAN_ID.Items.Insert(0, "");

                if (TempVal == "0")
                {
                    hidINSTALL_PLAN_ID.Value = "0";
                    //cmbINSTALL_PLAN_ID.SelectedIndex = 0;
                }
                else
                    hidINSTALL_PLAN_ID.Value = TempVal;
                //cmbINSTALL_PLAN_ID.SelectedValue = TempVal; */
            }
            catch { }
            //}
            #endregion
            /*try //Commented by Charles on 17-Sep-09 for APP/POL Optimization
			{
				if (dsPolicy.Tables[0].Rows[0]["INSTALL_PLAN_ID"] != null && dsPolicy.Tables[0].Rows[0]["INSTALL_PLAN_ID"].ToString() != "" && dsPolicy.Tables[0].Rows[0]["INSTALL_PLAN_ID"].ToString() != "0")
					cmbINSTALL_PLAN_ID.SelectedValue = dsPolicy.Tables[0].Rows[0]["INSTALL_PLAN_ID"].ToString();
				else
					cmbINSTALL_PLAN_ID.SelectedIndex = 0;
			}
			catch(Exception ex){}*/

            //if (dsPolicy.Tables[0].Rows[0]["COMPLETE_APP"].ToString().ToUpper() == "Y")
            //    chkCOMPLETE_APP.Checked = true;
            //else
            //    chkCOMPLETE_APP.Checked = false;

            if (dsPolicy.Tables[0].Rows[0]["BROKER_COMM_FIRST_INSTM"].ToString().ToUpper() == "Y")
                chkBROKER_COMM_FIRST_INSTM.Checked = true;
            else
                chkBROKER_COMM_FIRST_INSTM.Checked = false;

            if (dsPolicy.Tables[0].Rows[0]["BROKER_REQUEST_NO"].ToString().Trim() != "")
            {
                txtBROKER_REQUEST_NO.Text = dsPolicy.Tables[0].Rows[0]["BROKER_REQUEST_NO"].ToString().Trim();
            }


            //if (dsPolicy.Tables[0].Rows[0]["PIC_OF_LOC"].ToString() != "")
            //{
            //    ListItem listItem;
            //    listItem = cmbPIC_OF_LOC.Items.FindByValue(dsPolicy.Tables[0].Rows[0]["PIC_OF_LOC"].ToString());
            //    if(listItem != null)
            //    {
            //        cmbPIC_OF_LOC.SelectedIndex= cmbPIC_OF_LOC.Items.IndexOf(listItem);	
            //    }
            //}

            //if(dsPolicy.Tables[0].Rows[0]["COMPLETE_APP"]!=System.DBNull.Value)
            //{
            //if (dsPolicy.Tables[0].Rows[0]["PROPRTY_INSP_CREDIT"].ToString() != "")
            //{
            //    ListItem listItem;
            //    listItem = cmbPROPRTY_INSP_CREDIT.Items.FindByValue(dsPolicy.Tables[0].Rows[0]["PROPRTY_INSP_CREDIT"].ToString());
            //    if(listItem != null)
            //    {
            //        cmbPROPRTY_INSP_CREDIT.SelectedIndex= cmbPROPRTY_INSP_CREDIT.Items.IndexOf(listItem);	
            //    }


            //}
            //}

            //if(	hidLOBID.Value=="1" || hidLOBID.Value =="6")
            //{
            //    ListItem listItem;
            //    //Get The ListItem According To The Value
            //    listItem = cmbPOLICY_TYPE.Items.FindByValue(Convert.ToString(dsPolicy.Tables[0].Rows[0]["POLICY_TYPE"].ToString()));
            //    //Select it
            //    cmbPOLICY_TYPE.SelectedIndex= cmbPOLICY_TYPE.Items.IndexOf(listItem);	
            //}			
            //if((hidLOBID.Value==((int)enumLOB.HOME).ToString() || hidLOBID.Value==((int)enumLOB.REDW).ToString()) && dsPolicy.Tables[0].Rows[0]["BILL_MORTAGAGEE"]!=null && dsPolicy.Tables[0].Rows[0]["BILL_MORTAGAGEE"].ToString().Trim()!="")
            //{
            //    lblBILL_MORTAGAGEE.Text = dsPolicy.Tables[0].Rows[0]["BILL_MORTAGAGEE"].ToString();
            //    //trBILL_MORTAGAGEE.Attributes.Add("style","display:inline");
            //}	
            /*else
                trBILL_MORTAGAGEE.Attributes.Add("style","display:none");*/
            if (hidDOWN_PAY_MODE.Value != "0" && hidDOWN_PAY_MODE.Value != "")
            {
                ListItem listItem;
                listItem = cmbDOWN_PAY_MODE.Items.FindByValue(Convert.ToString(dsPolicy.Tables[0].Rows[0]["DOWN_PAY_MODE"].ToString()));
                cmbDOWN_PAY_MODE.SelectedIndex = cmbDOWN_PAY_MODE.Items.IndexOf(listItem);
            }
            else
            {
                hidDOWN_PAY_MODE.Value = dsPolicy.Tables[0].Rows[0]["DOWN_PAY_MODE"].ToString();
            }

            if (dsPolicy.Tables[0].Rows[0]["NOT_RENEW_REASON"] != System.DBNull.Value)
            {
                ListItem listItem;
                listItem = cmbNOT_RENEW_REASON.Items.FindByValue(Convert.ToString(dsPolicy.Tables[0].Rows[0]["NOT_RENEW_REASON"].ToString()));
                cmbNOT_RENEW_REASON.SelectedIndex = cmbNOT_RENEW_REASON.Items.IndexOf(cmbNOT_RENEW_REASON.Items.FindByValue(dsPolicy.Tables[0].Rows[0]["NOT_RENEW_REASON"].ToString()));
            }
            else
            {
                cmbNOT_RENEW_REASON.SelectedIndex = -1;
            }

            if (dsPolicy.Tables[0].Rows[0]["NOT_RENEW"].ToString() == "Y")
                chkNOT_RENEW.Checked = true;
            else
                chkNOT_RENEW.Checked = false;

            if (dsPolicy.Tables[0].Rows[0]["REFER_UNDERWRITER"].ToString() == "Y")
            {
                chkREFER_UNDERWRITER.Checked = true;
                txtREFERAL_INSTRUCTIONS.Text = dsPolicy.Tables[0].Rows[0]["REFERAL_INSTRUCTIONS"].ToString();
            }
            else
            {
                chkREFER_UNDERWRITER.Checked = false;
                txtREFERAL_INSTRUCTIONS.Text = "";
            }

            //if(	dsPolicy.Tables[0].Rows[0]["REINS_SPECIAL_ACPT"]!=System.DBNull.Value )
            //{
            //    cmbREINS_SPECIAL_ACPT.SelectedIndex =cmbREINS_SPECIAL_ACPT.Items.IndexOf(cmbREINS_SPECIAL_ACPT.Items.FindByValue(dsPolicy.Tables[0].Rows[0]["REINS_SPECIAL_ACPT"].ToString()));
            //}

            hidPOLICY_STATUS.Value = dsPolicy.Tables[0].Rows[0]["POLICY_STATUS"].ToString().ToUpper();
            string MyhidCalledFrom = hidCalledFrom.Value;
            string MyhidPOLICY_STATUS_CODE = hidPOLICY_STATUS_CODE.Value;

            //			if(hidCalledFrom.Value =="REWRITE" || hidPOLICY_STATUS_CODE.Value==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_UNDER_REWRITE ||
            //			hidPOLICY_STATUS_CODE.Value==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_SUSPENSE_REWRITE )
            //			{
            //if (dsPolicy.Tables[0].Rows[0]["STATE_ID"].ToString() !="0")
            //    cmbSTATE_ID.SelectedIndex =cmbSTATE_ID.Items.IndexOf(cmbSTATE_ID.Items.FindByValue(dsPolicy.Tables[0].Rows[0]["STATE_ID"].ToString()));    
            //string AGENCY_ID= dsPolicy.Tables[0].Rows[0]["AGENCY_ID"].ToString()==""?"0":dsPolicy.Tables[0].Rows[0]["AGENCY_ID"].ToString();

            //Commented by Charles on 21-Aug-09 for APP/POL OPTIMISATION
            //cmbAGENCY_ID.SelectedIndex =cmbAGENCY_ID.Items.IndexOf(cmbAGENCY_ID.Items.FindByValue(AGENCY_ID));
            FillCSRDropDown(int.Parse(hidAPP_AGENCY_ID.Value == "" ? "0" : hidAPP_AGENCY_ID.Value));//Changed to hidden variable hidAPP_AGENCY_ID, by Charles on 19-Aug-09 for APP/POL OPTIMISATION
            //FillProducerDropDown(int.Parse(hidAPP_AGENCY_ID.Value==""?"0":hidAPP_AGENCY_ID.Value));//Changed to hidden variable hidAPP_AGENCY_ID, by Charles on 19-Aug-09 for APP/POL OPTIMISATION

            FillUnderWriter(int.Parse(hidAPP_AGENCY_ID.Value)); //Fill Policy Agency's Underwritters as discussed With Rajan Sir Itrack# 4901
            CSR = dsPolicy.Tables[0].Rows[0]["CSRNAME"].ToString();
            strCSR = "";
            if (CSR != "0" && CSR != null && CSR != "")
            {
                strCSR = CSR.Substring(0, CSR.IndexOf("-")).ToString();
            }
            cmbCSR.SelectedIndex = cmbCSR.Items.IndexOf(cmbCSR.Items.FindByValue(strCSR.Trim()));
            cmbPRODUCER.SelectedIndex = cmbPRODUCER.Items.IndexOf(cmbPRODUCER.Items.FindByValue(dsPolicy.Tables[0].Rows[0]["PRODUCER"].ToString()));
            //Done for Itrack Issue 6658 on 2 Nov 09
            cmbUNDERWRITER.SelectedIndex = cmbUNDERWRITER.Items.IndexOf(cmbUNDERWRITER.Items.FindByValue(dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString()));
            //			}


            //add by Pravesh if policy is suspended or under rewrite uppper part of billing info can be Edit
            //hidPOLICY_STATUS_CODE.Value.Trim().ToUpper() ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_SUSPENDED ||
            if (hidPOLICY_STATUS_CODE.Value.Trim().ToUpper() == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_UNDER_REWRITE ||
                hidPOLICY_STATUS_CODE.Value.Trim().ToUpper() == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_UNDER_ISSUE ||// allow product type and policy term in NBS in Progress Itrack# 4927 
                hidPOLICY_STATUS_CODE.Value.Trim().ToUpper() == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_SUSPENSE_REWRITE //allow suspenses

                )
            {

                //cmbAPP_TERMS.Enabled=true;//Commented by Pradeep Kushwaha(21-oct-2010) for implementation of APP_TERMS on Textbox
                txtAPP_TERMS.Enabled = true;//Added by Pradeep Kushwaha on 21-oct-2010
                txtAPP_EFFECTIVE_DATE.ReadOnly = false;
                //cmbPOLICY_TYPE.Enabled =true;
            }
            else
            {
                //cmbAPP_TERMS.Enabled=false;//Commented by Pradeep Kushwaha(21-oct-2010) for implementation of APP_TERMS on Textbox
                //Added by Pradeep Kushwaha on 21-oct-2010
                if (hidPOLICY_STATUS_CODE.Value.Trim().ToUpper() != Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_UNDER_RENEW) txtAPP_TERMS.Enabled = false;
                txtAPP_EFFECTIVE_DATE.ReadOnly = true;
              
                //cmbPOLICY_TYPE.Enabled =false;
            }

            cmbPOLICY_CURRENCY.SelectedIndex = cmbPOLICY_CURRENCY.Items.IndexOf(cmbPOLICY_CURRENCY.Items.FindByValue(dsPolicy.Tables[0].Rows[0]["POLICY_CURRENCY"].ToString()));
            cmbCO_INSURANCE.SelectedIndex = cmbCO_INSURANCE.Items.IndexOf(cmbCO_INSURANCE.Items.FindByValue(dsPolicy.Tables[0].Rows[0]["CO_INSURANCE"].ToString()));
            cmbPAYOR.SelectedIndex = cmbPAYOR.Items.IndexOf(cmbPAYOR.Items.FindByValue(dsPolicy.Tables[0].Rows[0]["PAYOR"].ToString()));
            if (cmbPAYOR.SelectedIndex > 0)
            {
                hidPAYOR.Value = cmbPAYOR.SelectedValue;
            }

            //cmbContact_Person.SelectedIndex = cmbContact_Person.Items.IndexOf(cmbContact_Person.Items.FindByValue(dsPolicy.Tables[0].Rows[0]["CONTACT_PERSON"].ToString()));
            txtBillTo.Text = dsPolicy.Tables[0].Rows[0]["BILLTO"].ToString();
            txtPOLICY_DESCRIPTION.Text = dsPolicy.Tables[0].Rows[0]["POLICY_DESCRIPTION"].ToString();
            if (dsPolicy.Tables[0].Rows[0]["POLICY_LEVEL_COMM_APPLIES"].ToString().ToUpper() == "Y")
                chkPOLICY_LEVEL_COMM_APPLIES.Checked = true;
            else
                chkPOLICY_LEVEL_COMM_APPLIES.Checked = false;


            txtPOLICY_LEVEL_COMISSION.Text = dsPolicy.Tables[0].Rows[0]["POLICY_LEVEL_COMISSION"].ToString();
            hidOLD_POLICY_NUMBER.Value = dsPolicy.Tables[0].Rows[0]["OLD_POLICY_NUMBER"].ToString();
            txtOLD_POLICY_NUMBER.Text = dsPolicy.Tables[0].Rows[0]["OLD_POLICY_NUMBER"].ToString();
            SetReadOnlyFields();
        }

        #endregion

        private void SetReadOnlyFields()
        {
            //Following field will be read only in case of active policy: 
            //Effective Date,Bill Type,Billing Plan,Down Payment Mode 
            //These fields il be editable only in case of Suspended, Under Renewal , Under Rewrite and Under Reinstatement policies.
            //hidPOLICY_STATUS_CODE.Value.Trim().ToUpper() ==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_SUSPENDED ||
            if (hidPOLICY_STATUS_CODE.Value.Trim().ToUpper() == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_UNDER_REWRITE ||
                hidPOLICY_STATUS_CODE.Value.Trim().ToUpper() == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_UNDER_RENEW ||
                hidPOLICY_STATUS_CODE.Value.Trim().ToUpper() == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_UNDER_ISSUE ||
                hidPOLICY_STATUS_CODE.Value.Trim().ToUpper() == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_RENEWAL_SUSPENSE ||
                hidPOLICY_STATUS_CODE.Value.Trim().ToUpper() == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_SUSPENSE_REWRITE

                )
            {

                txtAPP_EFFECTIVE_DATE.ReadOnly = false;
                txtAPP_EFFECTIVE_DATE.Enabled = true;
                cmbBILL_TYPE.Enabled = true;
                cmbINSTALL_PLAN_ID.Enabled = true;
                cmbDOWN_PAY_MODE.Enabled = true;
            }
            else
            {
                txtAPP_EFFECTIVE_DATE.ReadOnly = true;
                txtAPP_EFFECTIVE_DATE.Enabled = false;
                cmbBILL_TYPE.Enabled = false;
                cmbINSTALL_PLAN_ID.Enabled = false;
                cmbDOWN_PAY_MODE.Enabled = false;
            }

            //modified by Pravesh on 29 sep08 itrack 4793
            if (hidPOLICY_STATUS_CODE.Value.Trim().ToUpper() == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_UNDER_ENDORSEMENT
               && hidINSTALL_PLAN_ID.Value == hidFULL_PAY_PLAN_ID.Value && (hidBILLTYPE.Value == "DB" || hidBILLTYPE.Value == "MB")
               && (hidLOBID.Value == LOB_RENTAL_DWELLING || hidLOBID.Value == LOB_HOME)
               )
            {
                btnChangeBillType.Visible = true;
                string strOldBillTypeID = cmbBILL_TYPE.SelectedValue;
                if (strOldBillTypeID == "11276")
                    btnChangeBillType.Text = "Change to Insured Bill";
                else
                    btnChangeBillType.Text = "Change to Mortgagee Bill";
            }
            else
                btnChangeBillType.Visible = false;

        }

        private void SetApplicationCookies(string appNo, string custID, string appID, string appVersionID)
        {
            //Added Mohit Agarwal 19-Feb 2007 to store last 3 cookies
            # region last 3 cookies
            string AgencyId = GetSystemId();
            if (AgencyId.ToUpper() != CarrierSystemID.ToUpper()) //System.Configuration.ConfigurationManager.AppSettings.Get("CarrierSystemID").ToUpper())
            {
                if (System.Web.HttpContext.Current.Request.Cookies["appNo" + GetSystemId() + "_" + GetUserId() + "_1"] != null)
                {
                    string prevCook = System.Web.HttpContext.Current.Request.Cookies["appNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value;
                    string[] cookArr = prevCook.Split('@');
                    if (cookArr.Length > 0 && cookArr.Length < 4)
                    {
                        System.Web.HttpContext.Current.Response.Cookies["appNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value = appNo + "~" + custID + "~" + appID + "~" + appVersionID + "~" + DateTime.Today.Date + "@" + System.Web.HttpContext.Current.Request.Cookies["appNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value;
                    }
                    else if (cookArr.Length >= 4)
                    {
                        int maxindex = cookArr.Length - 1;
                        if (maxindex >= 3)
                            maxindex = 2;

                        System.Web.HttpContext.Current.Response.Cookies["appNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value = appNo + "~" + custID + "~" + appID + "~" + appVersionID + "~" + DateTime.Today.Date + "@";
                        for (int cookindex = 0; cookindex < maxindex; cookindex++)
                        {
                            System.Web.HttpContext.Current.Response.Cookies["appNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value += cookArr[cookindex] + "@";
                        }
                    }
                    else
                    {
                        System.Web.HttpContext.Current.Response.Cookies["appNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value = appNo + "~" + custID + "~" + appID + "~" + appVersionID + "~" + DateTime.Today.Date + "@";
                    }
                    System.Web.HttpContext.Current.Response.Cookies["appNo" + GetSystemId() + "_" + GetUserId() + "_1"].Expires = DateTime.MaxValue;
                }
                else
                {
                    System.Web.HttpContext.Current.Response.Cookies["appNo" + GetSystemId() + "_" + GetUserId() + "_1"].Value = appNo + "~" + custID + "~" + appID + "~" + appVersionID + "~" + DateTime.Today.Date + "@";
                    System.Web.HttpContext.Current.Response.Cookies["appNo" + GetSystemId() + "_" + GetUserId() + "_1"].Expires = DateTime.MaxValue;
                }
            }
            #endregion
            else
            {
                System.Web.HttpContext.Current.Response.Cookies["appNo" + GetSystemId() + "_" + GetUserId()].Value = appNo + "~" + custID + "~" + appID + "~" + appVersionID + "~" + DateTime.Today.Date;
                System.Web.HttpContext.Current.Response.Cookies["appNo" + GetSystemId() + "_" + GetUserId()].Expires = DateTime.MaxValue;
            }

        }

        private void SetCookieValue()
        {
            Response.Cookies["LastVisitedTab"].Value = "1";
            Response.Cookies["LastVisitedTab"].Expires = DateTime.Now.Add(new TimeSpan(30, 0, 0, 0, 0));
            //Response.Write("<script>this.parent.document.location.href = '/Cms/Client/Aspx/CustomerManagerIndex.aspx';</script>");
        }

        #region GetFormValue

        /// Fetch form's value and stores into model class object and return that object.
        /// </summary>
        private ClsPolicyInfo GetFormValue()
        {
            //Creating the Model object for holding the New data
            ClsPolicyInfo objPolicyInfo = new ClsPolicyInfo();

            objPolicyInfo.CUSTOMER_ID = Convert.ToInt32(hidCustomerID.Value);
            objPolicyInfo.POLICY_ID = Convert.ToInt32(hidPolicyID.Value);
            objPolicyInfo.POLICY_VERSION_ID = Convert.ToInt32(hidPolicyVersionID.Value);

            //objPolicyInfo.APP_TERMS = cmbAPP_TERMS.SelectedItem.Value==null?"":cmbAPP_TERMS.SelectedItem.Value;//Commented by Pradeep Kushwaha(21-oct-2010) for implementation of APP_TERMS on Textbox

            objPolicyInfo.APP_TERMS = txtAPP_TERMS == null ? "" : txtAPP_TERMS.Text.Trim();//Added by Pradeep Kushwaha on 21-Oct-2010

            if (txtAPP_INCEPTION_DATE.Text.Trim() != "")
            {
                objPolicyInfo.APP_INCEPTION_DATE = ConvertToDate(txtAPP_INCEPTION_DATE.Text);
            }
            else
            {
                if (hidAPP_INCEPTION_DATE.Value != "")
                    objPolicyInfo.APP_INCEPTION_DATE = ConvertToDate(hidAPP_INCEPTION_DATE.Value);
            }

            objPolicyInfo.APP_EFFECTIVE_DATE = ConvertToDate(txtAPP_EFFECTIVE_DATE.Text);
            objPolicyInfo.APP_EXPIRATION_DATE = objPolicyInfo.APP_EFFECTIVE_DATE.AddDays(int.Parse(objPolicyInfo.APP_TERMS));

            if (cmbBILL_TYPE.SelectedItem != null)
            {
                objPolicyInfo.BILL_TYPE = int.Parse(cmbBILL_TYPE.SelectedValue);
                //if (objPolicyInfo.BILL_TYPE == 8460 || objPolicyInfo.BILL_TYPE == 11150 || objPolicyInfo.BILL_TYPE == 11278 || objPolicyInfo.BILL_TYPE == 11276) //Insured Bill/Insured Bill all terms/Insured Bill 1st term/Mortgagee @renewal 
                if (objPolicyInfo.BILL_TYPE == 114327 || objPolicyInfo.BILL_TYPE == 114328 || objPolicyInfo.BILL_TYPE == 114329 || objPolicyInfo.BILL_TYPE == 11276)//cHANGES BY kULDEEP ON JULY 9,2012 FOR TFS 5874
                {
                    if (hidDOWN_PAY_MODE.Value != "")
                        objPolicyInfo.DOWN_PAY_MODE = int.Parse(hidDOWN_PAY_MODE.Value);

                    if (Request["cmbINSTALL_PLAN_ID"] != null)
                        objPolicyInfo.INSTALL_PLAN_ID = Convert.ToInt32(Request["cmbINSTALL_PLAN_ID"]);
                    else
                    {
                        if (hidINSTALL_PLAN_ID.Value != "" && hidINSTALL_PLAN_ID.Value != "0")
                            objPolicyInfo.INSTALL_PLAN_ID = int.Parse(hidINSTALL_PLAN_ID.Value);
                        else
                            objPolicyInfo.INSTALL_PLAN_ID = int.Parse(hidFULL_PAY_PLAN_ID.Value);
                    }
                }
            }

            if (cmbPOLICY_SUBLOB.SelectedItem != null && cmbPOLICY_SUBLOB.SelectedValue != "")
                objPolicyInfo.POLICY_SUBLOB = cmbPOLICY_SUBLOB.SelectedValue;
            else
                objPolicyInfo.POLICY_SUBLOB = "0";

            if (txtPOLICY_DISP_VERSION.Text.Trim() != "")
                objPolicyInfo.POLICY_DISP_VERSION = txtPOLICY_DISP_VERSION.Text.Trim();
            if (txtPOLICY_NUMBER.Text.Trim() != "")
                objPolicyInfo.POLICY_NUMBER = txtPOLICY_NUMBER.Text.Trim();

            if (lblPOLICY_STATUS.Text.Trim() != "")
                objPolicyInfo.POLICY_STATUS = hidPOLICY_STATUS_CODE.Value.ToUpper(); //lblPOLICY_STATUS.Text.Trim();

            // chk for home employee
            if (hidPOLICY_STATUS_CODE.Value.ToUpper() == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_UNDER_REWRITE || hidPOLICY_STATUS_CODE.Value.ToUpper() == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_SUSPENSE_REWRITE)
            {
                //Changed to hidAgencyCode.Value by Charles on 21-Aug-09 for APP/POL OPTIMISATION
                string AgencyCode = hidAgencyCode.Value;//ClsAgency.GetAgencyCodeFromID(int.Parse(cmbAGENCY_ID.SelectedValue));
                string polAgencyCode = CarrierSystemID;//System.Configuration.ConfigurationManager.AppSettings.Get("CarrierSystemID");
                if (polAgencyCode.ToUpper().Trim() == AgencyCode.ToUpper().Trim())
                    objPolicyInfo.IS_HOME_EMP = true;
                else
                    objPolicyInfo.IS_HOME_EMP = false;
            }
            if (hidPOLICY_STATUS_CODE.Value.ToUpper() != Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_SUSPENDED && hidPOLICY_STATUS_CODE.Value != Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_UNDER_ISSUE)
            {
                if (chkNOT_RENEW.Checked == true)
                {
                    objPolicyInfo.NOT_RENEW = "Y";
                    if (cmbNOT_RENEW_REASON.SelectedValue != "" && cmbNOT_RENEW_REASON.SelectedIndex != -1)
                        objPolicyInfo.NOT_RENEW_REASON = int.Parse(cmbNOT_RENEW_REASON.SelectedValue);
                }
                else
                    objPolicyInfo.NOT_RENEW = "N";
                if (chkREFER_UNDERWRITER.Checked == true)
                {
                    objPolicyInfo.REFER_UNDERWRITER = "Y";
                    objPolicyInfo.REFERAL_INSTRUCTIONS = txtREFERAL_INSTRUCTIONS.Text.Trim();
                }
                else
                {
                    objPolicyInfo.REFER_UNDERWRITER = "N";
                    objPolicyInfo.REFERAL_INSTRUCTIONS = "";
                }
            }
            else
            {
                objPolicyInfo.NOT_RENEW = "N";
                objPolicyInfo.REFER_UNDERWRITER = "N";
            }

            if (int.Parse(hidAPP_AGENCY_ID.Value == "" ? "0" : hidAPP_AGENCY_ID.Value) != 0)//Added by Charles on 21-Aug-09 for APP/POL OPTIMISATION
                objPolicyInfo.AGENCY_ID = int.Parse(hidAPP_AGENCY_ID.Value);//int.Parse(cmbAGENCY_ID.SelectedValue); //Changed by Charles on 21-Aug-09 for APP/POL OPTIMISATION

            objPolicyInfo.CSR = int.Parse(hidCSR.Value == "" ? "-1" : hidCSR.Value);

            //Done for Itrack Issue 6658 on 2 Nov 09
            objPolicyInfo.UNDERWRITER = int.Parse(hidUnderwriter.Value == "" ? "-1" : hidUnderwriter.Value);

            if (cmbTRANSACTION_TYPE.SelectedItem != null && cmbTRANSACTION_TYPE.SelectedValue != "")
            {
                objPolicyInfo.TRANSACTION_TYPE = int.Parse(cmbTRANSACTION_TYPE.SelectedValue);
            }
            if (cmbFUND_TYPE.SelectedItem != null && cmbFUND_TYPE.SelectedValue != "")
            {
                objPolicyInfo.FUND_TYPE = int.Parse(cmbFUND_TYPE.SelectedValue);
            }
            if (cmbTRANSACTION_TYPE.SelectedValue == "14560") //for master policy Policy level commission not applicable ,i-track #-543
            {
                objPolicyInfo.POLICY_LEVEL_COMM_APPLIES = "N";
                objPolicyInfo.POLICY_LEVEL_COMISSION = 0;
            }
            else
            {
                objPolicyInfo.POLICY_LEVEL_COMM_APPLIES = chkPOLICY_LEVEL_COMM_APPLIES.Checked == true ? "Y" : "N";
                if (txtPOLICY_LEVEL_COMISSION.Text.Trim() != "" && chkPOLICY_LEVEL_COMM_APPLIES.Checked)
                {
                    objPolicyInfo.POLICY_LEVEL_COMISSION = double.Parse(txtPOLICY_LEVEL_COMISSION.Text);
                }
            }
            if (cmbPOLICY_CURRENCY.SelectedItem != null && cmbPOLICY_CURRENCY.SelectedValue != "")
            {
                objPolicyInfo.POLICY_CURRENCY = int.Parse(cmbPOLICY_CURRENCY.SelectedValue);
            }
            if (cmbCO_INSURANCE.SelectedItem != null && cmbCO_INSURANCE.SelectedValue != "")
            {
                objPolicyInfo.CO_INSURANCE = int.Parse(cmbCO_INSURANCE.SelectedValue);
            }

            if (hidPAYOR.Value != null && hidPAYOR.Value != "0" && hidPAYOR.Value != "")
            {
                objPolicyInfo.PAYOR = int.Parse(hidPAYOR.Value);

                if (txtCUSTOMERNAME.Text.Trim() != "" && objPolicyInfo.PAYOR != 14544)//Leader
                {
                    objPolicyInfo.BILLTO = txtCUSTOMERNAME.Text;
                }
            }



            //if (txtPREFERENCE_DAY.Text.Trim() != "")
            //{
            //    objPolicyInfo.PREFERENCE_DAY = int.Parse(txtPREFERENCE_DAY.Text.Trim());
            //}

            if (txtBROKER_REQUEST_NO.Text.Trim() != "")
            {
                objPolicyInfo.BROKER_REQUEST_NO = txtBROKER_REQUEST_NO.Text.Trim();
            }
            if (txtPOLICY_DESCRIPTION.Text.Trim() != "")
            {
                objPolicyInfo.POLICY_DESCRIPTION = txtPOLICY_DESCRIPTION.Text.Trim();
            }
            objPolicyInfo.BROKER_COMM_FIRST_INSTM = chkBROKER_COMM_FIRST_INSTM.Checked == true ? "Y" : "N";


            // state for all old products
            if (hidLOBID.Value != null && hidLOBID.Value != "")
            {
                if (int.Parse(hidLOBID.Value) <= 8)
                {
                    if (cmbState_ID.SelectedValue != "" && cmbState_ID.SelectedValue != null)
                    {
                        objPolicyInfo.STATE_ID = int.Parse(cmbState_ID.SelectedValue);
                    }

                }

                else
                {
                    objPolicyInfo.STATE_ID = 0;

                }
            }
            else
            { objPolicyInfo.STATE_ID = 0; }


            objPolicyInfo.PRODUCER = int.Parse(hidPRODUCER.Value == "" ? "-1" : hidPRODUCER.Value);

            #region Commented
            //if (cmbCSR.SelectedItem != null && cmbCSR.SelectedValue != "")
            //    objPolicyInfo.CSR = int.Parse(cmbCSR.SelectedValue); 
            //if (cmbPRODUCER.SelectedItem != null && cmbPRODUCER.SelectedValue != "")
            //    objPolicyInfo.PRODUCER = int.Parse(cmbPRODUCER.SelectedValue); 
            //objPolicyInfo.PRODUCER		= int.Parse(hidPRODUCER.Value=="" ? "-1": hidPRODUCER.Value);	

            //if (cmbContact_Person.SelectedItem != null && cmbContact_Person.SelectedValue != "")
            //{
            //    objPolicyInfo.CONTACT_PERSON = int.Parse(cmbContact_Person.SelectedValue);
            //}

            //if(cmbPIC_OF_LOC.SelectedItem!=null)
            //{
            //    objPolicyInfo.PIC_OF_LOC=cmbPIC_OF_LOC.SelectedItem.Value;
            //}
            //if(txtPOLICY_STATUS.Text.Trim()!="")
            //	objPolicyInfo.POLICY_STATUS = txtPOLICY_STATUS.Text.Trim();
            // objPolicyInfo.DOWN_PAY_MODE = int.Parse(hidDOWN_PAY_MODE.Value); 

            //			if (hidCalledFrom.Value =="REWRITE" || hidPOLICY_STATUS_CODE.Value==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_UNDER_REWRITE ||
            //				hidPOLICY_STATUS_CODE.Value==Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_SUSPENSE_REWRITE )
            //			{
            //if(cmbSTATE_ID.SelectedItem!=null && cmbSTATE_ID.SelectedValue!="")
            //    objPolicyInfo.STATE_ID = int.Parse(cmbSTATE_ID.SelectedValue);
            //else
            //    objPolicyInfo.STATE_ID = int.Parse(hidSTATE_ID.Value);
            //if(cmbAGENCY_ID.SelectedItem!=null && cmbAGENCY_ID.SelectedValue!="") //Commented by Charles on 21-Aug-09 for APP/POL OPTIMISATION

            //			if(cmbBILL_TYPE.SelectedItem!=null)
            //			{
            //				objPolicyInfo.BILL_TYPE=	int.Parse(cmbBILL_TYPE.SelectedValue);
            //			}

            /*if(cmbINSTALL_PLAN_ID.SelectedItem!=null && cmbINSTALL_PLAN_ID.SelectedItem.Text!="")
            {
                objPolicyInfo.INSTALL_PLAN_ID = int.Parse (cmbINSTALL_PLAN_ID.SelectedItem.Value);
            }
            else
            {
                objPolicyInfo.INSTALL_PLAN_ID=0;
            }*/
            //			if (hidINSTALL_PLAN_ID.Value !="")  
            //			{
            //			objPolicyInfo.INSTALL_PLAN_ID = int.Parse (hidINSTALL_PLAN_ID.Value);
            //			}
            //			else
            //			objPolicyInfo.INSTALL_PLAN_ID=0;

            //if(cmbCHARGE_OFF_PRMIUM.SelectedItem!=null)
            //{
            //    objPolicyInfo.CHARGE_OFF_PRMIUM=cmbCHARGE_OFF_PRMIUM.SelectedItem.Value;
            //}

            //if(txtRECEIVED_PRMIUM.Text.Trim()!="")// && txtRECEIVED_PRMIUM.Text.Trim()!= "0")
            //{
            //    objPolicyInfo.RECEIVED_PRMIUM=double.Parse(txtRECEIVED_PRMIUM.Text);
            //}
            //else
            //    objPolicyInfo.RECEIVED_PRMIUM = -1;

            //objPolicyInfo.COMPLETE_APP = chkCOMPLETE_APP.Checked == true ? "Y" : "N";
            //Check For Homeowner
            //if(hidLOBID.Value =="1" || hidLOBID.Value =="6")
            //{
            //    if(cmbPROPRTY_INSP_CREDIT.SelectedItem != null)
            //    {
            //        objPolicyInfo.PROPRTY_INSP_CREDIT=	cmbPROPRTY_INSP_CREDIT.SelectedValue;
            //    }
            //}

            //if(cmbPROXY_SIGN_OBTAINED.SelectedItem!=null && cmbPROXY_SIGN_OBTAINED.SelectedItem.Value!="") 
            //{
            //    objPolicyInfo.PROXY_SIGN_OBTAINED = int.Parse(cmbPROXY_SIGN_OBTAINED.SelectedItem.Value);
            //}
            //Done for Itrack Issue 6658 on 2 Nov 09
            //if (cmbUNDERWRITER.SelectedItem != null && cmbUNDERWRITER.SelectedItem.Value != "")
            //{
            //    objPolicyInfo.UNDERWRITER = int.Parse(cmbUNDERWRITER.SelectedValue);
            //}

            //if (txtYEAR_AT_CURR_RESI.Text.Trim() != "")
            //    objPolicyInfo.YEAR_AT_CURR_RESI=Convert.ToDouble(txtYEAR_AT_CURR_RESI.Text);

            //if (txtYEAR_AT_CURR_RESI.Text.Trim() != "" && int.Parse(txtYEAR_AT_CURR_RESI.Text.Trim()) < 3 && txtYEAR_AT_CURR_RESI.Text.Trim() != "0" )
            //    objPolicyInfo.YEARS_AT_PREV_ADD=txtYEARS_AT_PREV_ADD.Text;
            //else
            //    objPolicyInfo.YEARS_AT_PREV_ADD="";
            //if(hidLOBID.Value =="1" || hidLOBID.Value =="6")
            //    objPolicyInfo.POLICY_TYPE=cmbPOLICY_TYPE.SelectedItem.Value.ToString();
            //else
            //    objPolicyInfo.POLICY_TYPE = "-1";

            //			}
            //These  assignments are common to all pages.
            //objPolicyInfo.REINS_SPECIAL_ACPT =int.Parse(cmbREINS_SPECIAL_ACPT.SelectedValue);
            #endregion
            strFormSaved = hidFormSaved.Value;
            strRowId = hidAppID.Value;
            oldXML = hidOldData.Value;

            return objPolicyInfo;
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
            //this.cmbAPP_TERMS.SelectedIndexChanged += new System.EventHandler(this.cmbAPP_TERMS_SelectedIndexChanged);
            //this.cmbSTATE_ID.SelectedIndexChanged += new System.EventHandler(this.cmbSTATE_ID_SelectedIndexChanged);
            //this.cmbAGENCY_ID.SelectedIndexChanged += new System.EventHandler(this.cmbAGENCY_ID_SelectedIndexChanged);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnChangeBillType.Click += new System.EventHandler(this.btnChangeBillType_Click);
            this.Load += new System.EventHandler(this.Page_Load);
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            this.btnSendCancelNotice.Click += new System.EventHandler(this.btnSendCancelNotice_Click);
           
        }
        #endregion

        #region "Web Event Handlers"
        private void btnChangeBillType_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (hidLOBID.Value == LOB_RENTAL_DWELLING || hidLOBID.Value == LOB_HOME)
                {
                    int CUSTOMER_ID = Convert.ToInt32(hidCustomerID.Value);
                    int POLICY_ID = Convert.ToInt32(hidPolicyID.Value);
                    int POLICY_VERSION_ID = Convert.ToInt32(hidPolicyVersionID.Value);
                    string strOldBillTypeID = cmbBILL_TYPE.SelectedValue;
                    string strNewBillTypeId = "";
                    //11276		Mortgagee Bill from Inception
                    //11278		Insured Bill 1st term/Mortgagee @renewal 
                    //8460		Insured Bill,
                    //11150		Insured Bill all terms 
                    if (strOldBillTypeID == "11278" || strOldBillTypeID == "8460" || strOldBillTypeID == "11150")
                        strNewBillTypeId = "11276";
                    else if (strOldBillTypeID == "11276")
                        strNewBillTypeId = "11150";
                    else
                    {
                        lblMessage.Text = "Bill Type cannot be modified for this policy.";
                        lblMessage.Visible = true;
                        return;
                    }
                    int userID = int.Parse(GetUserId());
                    int intRetVal = objGeneralInformation.UpdatePolicyBillTypeId(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, strNewBillTypeId, strOldBillTypeID, userID);
                    if (intRetVal != -1)
                    {
                        lblMessage.Text = "Bill Type has been modified for this term.";
                        lblMessage.Visible = true;
                        base.OpenEndorsementDetails();
                        LoadData();
                    }
                }
                else
                {
                    lblMessage.Text = "Bill Type cannot be modified for this lob type.";
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error:Bill Type could not be modified." + ex.Message + ".Try later.";
                lblMessage.Visible = true;
            }
        }
        /// <summary>
        /// If form is posted back then add entry in database using the BL object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            ClsPolicyInfo objPolicyInfo = GetFormValue();

            try
            {
                //For retrieving the return value of business class save function
                int intRetVal;
                string policy_status;
                policy_status = hidPOLICY_STATUS_CODE.Value.ToUpper();
                objGeneralInformation = new ClsGeneralInformation();
                objGeneralInformation.TransactionRequired = true;

                //Creating the Model object for holding the Old data
                ClsPolicyInfo objOldPolicyInfo = new ClsPolicyInfo();

                //Setting  the Old Page details(XML File containing old details) into the Model Object
               
                base.PopulateModelObject(objOldPolicyInfo, hidOldData.Value);

                //Setting those values into the Model object which are not in the page
                objPolicyInfo.MODIFIED_BY = int.Parse(GetUserId());
                objPolicyInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                objPolicyInfo.POLICY_LOB = GetLOBID();

                //Updating the record using business layer class object
                objOldPolicyInfo.INSTALL_PLAN_ID = int.Parse(cmbINSTALL_PLAN_ID.SelectedValue);
                intRetVal = objGeneralInformation.UpdatePolicy(objOldPolicyInfo, objPolicyInfo, policy_status);

                if (intRetVal > 0)			// update successfully performed
                {
                    //Added by Charles on 17-May-10
                    //CO-INSURANCE UPDATE
                    if (objPolicyInfo.CO_INSURANCE != objOldPolicyInfo.CO_INSURANCE)
                    {
                        ClsCoInsuranceInfo objCoInsuranceInfo = new ClsCoInsuranceInfo();
                        objCoInsuranceInfo.CUSTOMER_ID = objPolicyInfo.CUSTOMER_ID;
                        objCoInsuranceInfo.POLICY_ID = objPolicyInfo.POLICY_ID;
                        objCoInsuranceInfo.POLICY_VERSION_ID = objPolicyInfo.POLICY_VERSION_ID;
                        objCoInsuranceInfo.LEADER_FOLLOWER = int.Parse(cmbCO_INSURANCE.SelectedValue);
                        ClsGeneralInformation.UpdateDefaultCoInsurer(objCoInsuranceInfo, GetSystemId());
                    }
                    //RENUMERATION TAB UPDATE
                    if (objPolicyInfo.AGENCY_ID != objOldPolicyInfo.AGENCY_ID)
                    {
                        ClsPolicyRemunerationInfo objnewPolicyRemunerationInfo = new ClsPolicyRemunerationInfo();
                        objnewPolicyRemunerationInfo.CUSTOMER_ID.CurrentValue = objPolicyInfo.CUSTOMER_ID;
                        objnewPolicyRemunerationInfo.POLICY_ID.CurrentValue = objPolicyInfo.POLICY_ID;
                        objnewPolicyRemunerationInfo.POLICY_VERSION_ID.CurrentValue = objPolicyInfo.POLICY_VERSION_ID;
                        ClsGeneralInformation.UpdateDefaultBroker(objnewPolicyRemunerationInfo, objPolicyInfo.AGENCY_ID, objOldPolicyInfo.AGENCY_ID);
                    }//Added till here

                    ClsGeneralInformation.GetBillType(cmbBILL_TYPE, Convert.ToInt32(hidLOBID.Value), int.Parse(hidCustomerID.Value), hidPolicyID.Value, int.Parse(hidPolicyVersionID.Value), "POL", GetLanguageID());
                    //lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
                    if (objPolicyInfo.POLICY_LOB == "1" || objPolicyInfo.POLICY_LOB == "6")
                    {
                        //If policy has not changed
                        if (objOldPolicyInfo.POLICY_TYPE == objPolicyInfo.POLICY_TYPE)
                        {
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("31");
                        }
                        else
                        {
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("723");
                        }
                    }
                    else
                    {
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("31");
                        //Information updated successfully.
                    }
                    //Show message depending on whether the product has changed or not

                    primaryKeyValues = hidAppID.Value + "^" + hidCustomerID.Value + "^" + hidAppVersionID.Value;
                    hidFormSaved.Value = "1";
                    SetAppID(hidAppID.Value.ToString());
                    SetAppVersionID(hidAppVersionID.Value.ToString());
                    SetCustomerID(hidCustomerID.Value.ToString());
                    SetCalledFor("POLICY");
                    hidRefresh.Value = "R";
                    //Added By Lalit Chauhan, Nov 08 2010
                    SetTransaction_Type(objPolicyInfo.TRANSACTION_TYPE.ToString());

                    //Showing the endorsement popup window
                    base.OpenEndorsementDetails();

                }
                else if (intRetVal == -1)	// Duplicate code exist, update failed
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("18");
                    hidFormSaved.Value = "2";
                }
                else
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("20");
                    hidFormSaved.Value = "2";
                }

                lblMessage.Visible = true;

            }

            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.FetchGeneralMessage("21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";
            }
            finally
            {
                if (objGeneralInformation != null)
                    objGeneralInformation.Dispose();
            }

            LoadData();
            //setCarrierInsuredBill(); //Added by Charles on 31-Jul-09 for Itrack 6205
        }

        /*
        private void btnIssuePolicy_Click(object sender, System.EventArgs e)
        {
			
            try
            {
                //For retrieving the return value of business class save function
                int intRetVal;	
                objGeneralInformation = new  ClsGeneralInformation();
                objGeneralInformation .TransactionRequired =true;
			
				 				
                intRetVal	= objGeneralInformation.IssuePolicy(Convert.ToInt32(hidCustomerID.Value),Convert.ToInt32(hidPolicyID.Value),Convert.ToInt32(hidPolicyVersionID.Value),int.Parse(GetUserId()),0, 0);
					
                if( intRetVal > 0 )			// update successfully performed
                {
                    lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"526");
                    primaryKeyValues=hidAppID.Value  + "^" + hidCustomerID.Value + "^" +  hidAppVersionID.Value; 
                    hidFormSaved.Value		=	"1";
                    SetAppID(hidAppID.Value.ToString());
                    SetAppVersionID(hidAppVersionID.Value.ToString());
                    SetCustomerID(hidCustomerID.Value.ToString());
                    btnIssuePolicy.Visible=false;
					 
                }
                else if(intRetVal == -1)	// Duplicate code exist, update failed
                {
                    lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
                    hidFormSaved.Value		=	"2";
                }
                else 
                {
                    lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
                    hidFormSaved.Value		=	"2";
                }
					
                lblMessage.Visible = true;

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
                if(objGeneralInformation!= null)
                    objGeneralInformation.Dispose();
            }

            LoadData();
            ReloadPolicyMenu("");
        }
		
        */
        #endregion

        #region SetCaptions
        private void SetCaptions()
        {
            lblHeader.Text = objResourceMgr.GetString("lblHeader");

            capCUSTOMERNAME.Text = objResourceMgr.GetString("txtCUSTOMERNAME");
            capPOLICY_STATUS.Text = objResourceMgr.GetString("txtPOLICY_STATUS");
            //capSTATE_NAME.Text =		objResourceMgr.GetString("cmbSTATE_ID");
            capPOLICY_LOB.Text = objResourceMgr.GetString("txtPOLICY_LOB");
            capSUB_LOB_DESC.Text = objResourceMgr.GetString("cmbAPP_SUBLOB"); //txtSUB_LOB_DESC
            capPOLICY_NUMBER.Text = objResourceMgr.GetString("txtPOLICY_NUMBER");
            capPOLICY_DISP_VERSION.Text = objResourceMgr.GetString("txtPOLICY_DISP_VERSION");
            capAGENCY_DISPLAY_NAME.Text = objResourceMgr.GetString("cmbAGENCY_ID");
            capCSRNAME.Text = objResourceMgr.GetString("cmbCSR");
            capPRODUCER.Text = objResourceMgr.GetString("cmbProducer");
            //capCSRNAME.Text =		objResourceMgr.GetString("txtCSRNAME");

            capINSTALL_PLAN_ID.Text = objResourceMgr.GetString("cmbINSTALL_PLAN_ID");
            capHeader.Text = objResourceMgr.GetString("capHeader");
            capAPP_TERMS.Text = objResourceMgr.GetString("cmbAPP_TERMS");
            capAPP_INCEPTION_DATE.Text = objResourceMgr.GetString("txtAPP_INCEPTION_DATE");
            capAPP_EFFECTIVE_DATE.Text = objResourceMgr.GetString("txtAPP_EFFECTIVE_DATE");
            capAPP_EXPIRATION_DATE.Text = objResourceMgr.GetString("txtAPP_EXPIRATION_DATE");
            capUNDERWRITER.Text = objResourceMgr.GetString("cmbUNDERWRITER");
            capBILL_TYPE.Text = objResourceMgr.GetString("cmbBILL_TYPE");
            //capCOMPLETE_APP.Text					=		objResourceMgr.GetString("chkCOMPLETE_APP");
            //capPROPRTY_INSP_CREDIT.Text				=		objResourceMgr.GetString("cmbPROPRTY_INSP_CREDIT");
            //capCHARGE_OFF_PRMIUM.Text				=		objResourceMgr.GetString("cmbCHARGE_OFF_PRMIUM");
            //capRECEIVED_PRMIUM.Text					=		objResourceMgr.GetString("txtRECEIVED_PRMIUM");
            //capPROXY_SIGN_OBTAINED.Text				=		objResourceMgr.GetString("cmbPROXY_SIGN_OBTAINED");
            //capYEAR_AT_CURR_RESI.Text			    =		objResourceMgr.GetString("txtYEAR_AT_CURR_RESI");
            //capYEARS_AT_PREV_ADD.Text			    =		objResourceMgr.GetString("txtYEARS_AT_PREV_ADD");
            //capPOLICY_TYPE.Text					    =	    objResourceMgr.GetString("cmbPOLICY_TYPE");
            //capPIC_OF_LOC.Text						=		objResourceMgr.GetString("cmbPIC_OF_LOC");
            capDOWN_PAY_MODE.Text = objResourceMgr.GetString("txtDOWN_PAY_MODE");
            capNOT_RENEW.Text = objResourceMgr.GetString("txtNOT_RENEW");
            capNOT_RENEW_REASON.Text = objResourceMgr.GetString("txtNOT_RENEW_REASON");
            capREFER_UNDERWRITER.Text = objResourceMgr.GetString("txtREFER_UNDERWRITER");
            capREFERAL_INSTRUCTIONS.Text = objResourceMgr.GetString("txtREFERAL_INSTRUCTIONS");
            //capREINS_SPECIAL_ACPT.Text				=		objResourceMgr.GetString("cmbREINS_SPECIAL_ACPT");
            capTRANSACTION_TYPE.Text = objResourceMgr.GetString("cmbTRANSACTION_TYPE");
            //Header Captions //Added by Charles on 8-Mar-10 for Multilingual Implementation
            lblManHeader.Text = objResourceMgr.GetString("lblManHeader");
            lblAppHeader.Text = objResourceMgr.GetString("lblAppHeader");
            lblTermHeader.Text = objResourceMgr.GetString("lblTermHeader");
            lblAgencyHeader.Text = objResourceMgr.GetString("lblAgencyHeader");
            lblRemarksHeader.Text = objResourceMgr.GetString("lblRemarksHeader");
            lblBillingHeader.Text = objResourceMgr.GetString("lblBillingHeader");
            //lblAllPoliciesHeader.Text = objResourceMgr.GetString("lblAllPoliciesHeader");
            //lblPolicies.Text = objResourceMgr.GetString("lblPolicies");
            //lblPoliciesDiscount.Text = objResourceMgr.GetString("lblPoliciesDiscount");
            btnReject.Text = objResourceMgr.GetString("btnReject");
            btnCopy.Text = objResourceMgr.GetString("btnCopy");
            capPOLICY_CURRENCY.Text = objResourceMgr.GetString("cmbPOLICY_CURRENCY");
            capPOLICY_LEVEL_COMISSION.Text = objResourceMgr.GetString("txtPOLICY_LEVEL_COMISSION");
            //capContact_Person.Text = objResourceMgr.GetString("cmbCONTACT_PERSON");
            capBillTo.Text = objResourceMgr.GetString("txtBILLTO");
            capPAYOR.Text = objResourceMgr.GetString("cmbPAYOR");
            capCO_INSURANCE.Text = objResourceMgr.GetString("cmbCO_INSURANCE");
            lblRenewalHeader.Text = objResourceMgr.GetString("lblRenewalHeader");
            //Added till here
            btnCopyPolicy.Text = objResourceMgr.GetString("btnCopyPolicy");
            //capPREFERENCE_DAY.Text = objResourceMgr.GetString("txtPREFERENCE_DAY");
            capPOLICY_LEVEL_COMM_APPLIES.Text = objResourceMgr.GetString("chkPOLICY_LEVEL_COMM_APPLIES");
            capBROKER_COMM_FIRST_INSTM.Text = objResourceMgr.GetString("chkBROKER_COMM_FIRST_INSTM");
            capBROKER_REQUEST_NO.Text = objResourceMgr.GetString("txtBROKER_REQUEST_NO");
            capREMARKS.Text = objResourceMgr.GetString("txtPOLICY_DESCRIPTION");
            capState.Text = objResourceMgr.GetString("cmbSTATE_ID");
            capMODALITY.Text = objResourceMgr.GetString("cmbMODALITY");
            capACTIVITY.Text = objResourceMgr.GetString("cmbACTIVITY");
            capPOLICY_NUMBER.Text = objResourceMgr.GetString("capPOLICY_NUMBER");
            capSUB_LOB_DESC.Text = objResourceMgr.GetString("capSUB_LOB_DESC");
            capDIV_ID_DEPT_ID_PC_ID.Text = objResourceMgr.GetString("cmbDIV_ID_DEPT_ID_PC_ID");
            capOLD_POLICY_NUMBER.Text = objResourceMgr.GetString("txtOLD_POLICY_NUMBER");
            str = objResourceMgr.GetString("str");
        }
        #endregion

        /// <summary>
        /// Retreives the default plan id, and sets it in installment plan field
        /// </summary>
        private void GetDefaultInstallmentPlan()
        {
            Cms.BusinessLayer.BlCommon.Accounting.ClsInstallmentPlan objPlan = new
                Cms.BusinessLayer.BlCommon.Accounting.ClsInstallmentPlan();

            hidFULL_PAY_PLAN_ID.Value = Cms.BusinessLayer.BlCommon.Accounting.ClsInstallmentPlan.FullPayPlanID().ToString();
            //Added by Pradeep on 0ct-2010 

            int PlanID = objPlan.GetDefaultPlanId(0);

            if (PlanID != 0)
            {
                if (cmbINSTALL_PLAN_ID.Items.FindByValue(PlanID.ToString()) != null)
                {
                    cmbINSTALL_PLAN_ID.SelectedValue = PlanID.ToString();
                }

            }
            //Added till here 
            #region Commented by Pradeep Kushwaha(21-oct-2010) for implementation of APP_TERMS on Textbox
            ////Retreiving the default installment plan
            //int PolTerm = int.Parse(cmbAPP_TERMS.SelectedItem.Value.ToString());
            //int PlanID = objPlan.GetDefaultPlanId(PolTerm);

            //if (PlanID != 0)
            //{
            //    if (cmbINSTALL_PLAN_ID.Items.FindByValue(PlanID.ToString()) != null)
            //    {
            //        cmbINSTALL_PLAN_ID.SelectedValue = PlanID.ToString();
            //    }

            //}
            #endregion
        }

        #region Fill Controls
        private void FillControls()
        {
            hidFULL_PAY_PLAN_ID.Value = Cms.BusinessLayer.BlCommon.Accounting.ClsInstallmentPlan.FullPayPlanID().ToString();

            DataTable dtPAYOR = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("PAYRC");
            DataView dvPAYOR = dtPAYOR.DefaultView;
            dvPAYOR.Sort = "LookupDesc";

            cmbPAYOR.DataSource = dvPAYOR;// Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PAYRC");
            cmbPAYOR.DataTextField = "LookupDesc";
            cmbPAYOR.DataValueField = "LookupID";
            cmbPAYOR.DataBind();
            cmbPAYOR.Items.Insert(0, "");

            cmbPOLICY_CURRENCY.DataSource = Cms.CmsWeb.ClsFetcher.Currency;
            cmbPOLICY_CURRENCY.DataTextField = "CURR_DESC";
            cmbPOLICY_CURRENCY.DataValueField = "CURRENCY_ID";
            cmbPOLICY_CURRENCY.DataBind();
            cmbPOLICY_CURRENCY.Items.Insert(0, "");


            DataTable dtCO_INSURANCE = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("COINC");
            DataView dvCO_INSURANCE = dtCO_INSURANCE.DefaultView;
            dvCO_INSURANCE.Sort = "LookupDesc";

            cmbCO_INSURANCE.DataSource = dvCO_INSURANCE;
            cmbCO_INSURANCE.DataTextField = "LookupDesc";
            cmbCO_INSURANCE.DataValueField = "LookupID";
            cmbCO_INSURANCE.DataBind();
            cmbCO_INSURANCE.Items.Insert(0, "");

            cmbTRANSACTION_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("TTYP");
            cmbTRANSACTION_TYPE.DataTextField = "LookupDesc";
            cmbTRANSACTION_TYPE.DataValueField = "LookupID";
            cmbTRANSACTION_TYPE.DataBind();
            cmbTRANSACTION_TYPE.Items.Insert(0, "");

            DataTable dtDIVDEP = ClsCommon.PopulateDivDeptPC();
            DataView dvDIVDEP = dtDIVDEP.DefaultView;
            dvDIVDEP.Sort = "TEXT";
            cmbDIV_ID_DEPT_ID_PC_ID.DataSource = dvDIVDEP;//ClsCommon.PopulateDivDeptPC();
            cmbDIV_ID_DEPT_ID_PC_ID.DataTextField = "TEXT";
            cmbDIV_ID_DEPT_ID_PC_ID.DataValueField = "VALUE";
            cmbDIV_ID_DEPT_ID_PC_ID.DataBind();
            cmbDIV_ID_DEPT_ID_PC_ID.Items.Insert(0, "");

            
            //RC testing
            cmbFUND_TYPE.DataSource = Cms.CmsWeb.ClsFetcher.Fund_Types;
            cmbFUND_TYPE.DataTextField = "FUND_TYPE_NAME";
            cmbFUND_TYPE.DataValueField = "FUND_TYPE_ID";
            cmbFUND_TYPE.DataBind();
            cmbFUND_TYPE.Items.Insert(0, "");


            #region Commented by Pradeep Kushwaha(21-oct-2010) for implementation of APP_TERMS on Textbox
            //Application Terms			
            //cmbAPP_TERMS.DataSource = ClsGeneralInformation.GetLOBTerms(Convert.ToInt32(hidLOBID.Value));
            //cmbAPP_TERMS.DataTextField = "LOOKUP_VALUE_DESC";
            //cmbAPP_TERMS.DataValueField = "LOOKUP_VALUE_CODE";
            //cmbAPP_TERMS.DataBind();
            //cmbAPP_TERMS.Items.Insert(0,"");
            #endregion

            //ClsGeneralInformation.GetBillType(cmbBILL_TYPE,Convert.ToInt32(hidLOBID.Value),"POL");
            //Modified on 7 May 2008
            ClsGeneralInformation.GetBillType(cmbBILL_TYPE, Convert.ToInt32(hidLOBID.Value), int.Parse(hidCustomerID.Value), hidPolicyID.Value, int.Parse(hidPolicyVersionID.Value), "POL");
            string strSystemIDD = GetSystemId();
            DataSet objDataSet = ClsAgency.GetAgencyIDAndNameFromCode(strSystemIDD);
            AGENCY_ID = int.Parse(objDataSet.Tables[0].Rows[0]["AGENCY_ID"].ToString());

            if (hidLOBID.Value == "1" || hidLOBID.Value == "6")
                cmbNOT_RENEW_REASON.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("HNRPS", "-1", "Y");
            else if (hidLOBID.Value == "4")
                cmbNOT_RENEW_REASON.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("WNRPS", "-1", "Y");
            else if (hidLOBID.Value == "2" || hidLOBID.Value == "3")
                cmbNOT_RENEW_REASON.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("MANRPS", "-1", "Y");
            else if (hidLOBID.Value == "5")
                cmbNOT_RENEW_REASON.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("UNRPS", "-1", "Y");
            else
                cmbNOT_RENEW_REASON.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("GWNRPS", "-1", "Y");
            //cmbNOT_RENEW_REASON.DataSource		=	Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CNCLRN");

            cmbNOT_RENEW_REASON.DataTextField = "LookupDesc";
            cmbNOT_RENEW_REASON.DataValueField = "LookupID";
            cmbNOT_RENEW_REASON.DataBind();
            cmbNOT_RENEW_REASON.Items.Insert(0, "");

            if (hidCalledFrom.Value == "REWRITE" || hidPOLICY_STATUS_CODE.Value == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_UNDER_REWRITE ||
                hidPOLICY_STATUS_CODE.Value == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_SUSPENSE_REWRITE)
            {
                if (lblCUATION != null)
                {
                    lblCUATION.Visible = true;
                    lblCUATION.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("944");
                }
                //DataSet dsState = Cms.BusinessLayer.BlCommon.ClsCommon.ExecuteDataSet("Proc_GetLobStateName '"+ hidLOBID.Value + "'");   
                //DataSet dsState = Cms.BusinessLayer.BlCommon.ClsAgency.GetStateNameId(hidLOBID.Value); 				
            }
            //else
            //cmbSTATE_ID.Attributes.Add("style","display:none");

            //Cms.BusinessLayer.BlCommon.ClsAgency objAgency = new ClsAgency();
            //DataSet dsRecieve =new DataSet();
            //dsRecieve=objAgency.PopulateassignedUnderWriter(AGENCY_ID,Convert.ToInt32(hidLOBID.Value));
            //if(dsRecieve.Tables.Count<1)
            //    return;
            //this.cmbUNDERWRITER.DataSource = dsRecieve.Tables[0];
            //this.cmbUNDERWRITER.DataValueField = dsRecieve.Tables[0].Columns[0].ToString();
            //this.cmbUNDERWRITER.DataTextField = dsRecieve.Tables[0].Columns[1].ToString();
            //this.cmbUNDERWRITER.DataBind();
            //this.cmbUNDERWRITER.Items.Insert(0,"");
            //this.cmbUNDERWRITER.SelectedIndex =0;             

            //cmbPIC_OF_LOC.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
            //cmbPIC_OF_LOC.DataTextField="LookupDesc";
            //cmbPIC_OF_LOC.DataValueField="LookupID";
            //cmbPIC_OF_LOC.DataBind();
            //cmbPIC_OF_LOC.Items.Insert(0,"");

            //cmbPROPRTY_INSP_CREDIT.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
            //cmbPROPRTY_INSP_CREDIT.DataTextField="LookupDesc";
            //cmbPROPRTY_INSP_CREDIT.DataValueField="LookupID";
            //cmbPROPRTY_INSP_CREDIT.DataBind();
            //cmbPROPRTY_INSP_CREDIT.Items.Insert(0,"");

            //cmbContact_Person.DataSource = ClsContactsManager.FetchContactList(GetCustomerID());
            //cmbContact_Person.DataTextField = "CONTACT_NAME";
            //cmbContact_Person.DataValueField = "CONTACT_ID";
            //cmbContact_Person.DataBind();
            //cmbContact_Person.Items.Insert(0, "");

            //cmbPROXY_SIGN_OBTAINED.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
            //cmbPROXY_SIGN_OBTAINED.DataTextField="LookupDesc";
            //cmbPROXY_SIGN_OBTAINED.DataValueField="LookupID";
            //cmbPROXY_SIGN_OBTAINED.DataBind();
            //cmbPROXY_SIGN_OBTAINED.Items.Insert(0,"");

            //cmbCHARGE_OFF_PRMIUM.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
            //cmbCHARGE_OFF_PRMIUM.DataTextField="LookupDesc";
            //cmbCHARGE_OFF_PRMIUM.DataValueField="LookupID";
            //cmbCHARGE_OFF_PRMIUM.DataBind();
            //cmbCHARGE_OFF_PRMIUM.Items.Insert(0,"");
            //cmbCHARGE_OFF_PRMIUM.SelectedIndex=1;

            //installment plans
            //cmbINSTALL_PLAN_ID.DataSource =ClsDepositDetails.FetchInstallmentPlans();
            //cmbINSTALL_PLAN_ID.DataTextField = "BILLING_PLAN";
            //cmbINSTALL_PLAN_ID.DataValueField = "INSTALL_PLAN_ID"; 
            //cmbINSTALL_PLAN_ID.DataBind();
            //cmbINSTALL_PLAN_ID.Items.Insert(0,"");
            //cmbINSTALL_PLAN_ID.Items[0].Value = "0";

            //cmbREINS_SPECIAL_ACPT.DataSource	=	Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
            //cmbREINS_SPECIAL_ACPT.DataTextField	=	"LookupDesc";
            //cmbREINS_SPECIAL_ACPT.DataValueField	=	"LookupID";
            //cmbREINS_SPECIAL_ACPT.DataBind(); 
            //cmbNOT_RENEW_REASON.Items.Insert(0,"");

            //DataSet dsState = ClsGeneralInformation.GetStateNameId(hidLOBID.Value);

            //cmbSTATE_ID.DataSource			= dsState;
            //cmbSTATE_ID.DataTextField		= "STATE_NAME";
            //cmbSTATE_ID.DataValueField		= "STATE_ID";
            //cmbSTATE_ID.DataBind();
            //cmbSTATE_ID.Items.Insert(0,new ListItem("",""));
            //cmbSTATE_ID.SelectedIndex=0;

            //Commented by Charles on 21-Aug-09 for APP/POL OPTIMISATION
            //string  strSystemID			 = GetSystemId();
            //string  strCarrierSystemID = System.Configuration.ConfigurationManager.AppSettings.Get("CarrierSystemID").ToString();
            //if ( strSystemID.Trim().ToUpper() != strCarrierSystemID.Trim().ToUpper())
            //{
            //    DataSet objAgencyDataSet = ClsAgency.GetAgencyIDAndNameFromCode(strSystemID);
            //    if (objAgencyDataSet.Tables[0].Rows.Count > 0 )
            //    {
            //        cmbAGENCY_ID.Items.Clear();
            //        cmbAGENCY_ID.Items.Add(new ListItem(objAgencyDataSet.Tables[0].Rows[0]["AGENCY_DISP_NAME"].ToString(),objAgencyDataSet.Tables[0].Rows[0]["AGENCY_ID"].ToString()));
            //    }
            //}
            //else
            //{				
            //    // Carrier user so show all agencies
            //    Cms.BusinessLayer.BlCommon.ClsAgency.GetAllAgencyNamesInDropDown(cmbAGENCY_ID);	
            //    FillCSRDropDown(int.Parse(cmbAGENCY_ID.SelectedItem.Value));
            //    FillProducerDropDown(int.Parse(cmbAGENCY_ID.SelectedItem.Value));				
            //}			  
        }




        private void FillUnderWriter(int AgencyID)
        {
            try
            {
                Cms.BusinessLayer.BlCommon.ClsAgency objAgency = new ClsAgency();
                DataSet dsRecieve = new DataSet();
                //dsRecieve=objAgency.PopulateassignedUnderWriter(AgencyID,Convert.ToInt32(hidLOBID.Value));
                dsRecieve = objAgency.PopulateAgency_MrkUW(AgencyID, Convert.ToInt32(hidLOBID.Value), "AUW");
                int UNDERWRITER = int.Parse(ClsCommon.FetchValueFromXML("UNDERWRITER", hidOldData.Value));
                if (dsRecieve.Tables.Count < 1)
                {
                    this.cmbUNDERWRITER.Items.Clear();
                    return;
                }
                this.cmbUNDERWRITER.DataSource = dsRecieve.Tables[0];
                this.cmbUNDERWRITER.DataValueField = dsRecieve.Tables[0].Columns["user_id"].ToString();
                this.cmbUNDERWRITER.DataTextField = dsRecieve.Tables[0].Columns["username"].ToString();
                this.cmbUNDERWRITER.DataBind();

                for (int i = 0; i < cmbUNDERWRITER.Items.Count; i++)
                {
                    string arrIsActiveStatus = dsRecieve.Tables[0].Rows[i]["IS_ACTIVE"].ToString();
                    if (arrIsActiveStatus.Equals("N"))
                        cmbUNDERWRITER.Items[i].Attributes.Add("style", "color:red");
                    if (UNDERWRITER != int.Parse(cmbUNDERWRITER.Items[i].Value.ToString()) && arrIsActiveStatus.Equals("N"))
                        cmbUNDERWRITER.Items.Remove(cmbUNDERWRITER.Items[i]);

                }
                this.cmbUNDERWRITER.Items.Insert(0, "");
                this.cmbUNDERWRITER.SelectedIndex = 0;
                dsRecieve.Dispose();
                objAgency.Dispose();
            }
            catch (Exception exc)
            {
                throw (exc);
            }
        }
        private void FillCSRDropDown(int AgencyID)
        {
            try
            {
                ClsUser objUser = new ClsUser();
                DataTable dtCSRProducers = objUser.GetCSRProducers(AgencyID, Convert.ToInt32(GetLOBID()), GetSystemId());
                int CSR = int.Parse(ClsCommon.FetchValueFromXML("CSR", hidOldData.Value));
                int PRODUCER = int.Parse(ClsCommon.FetchValueFromXML("PRODUCER", hidOldData.Value));
                cmbCSR.Items.Clear();
                if (dtCSRProducers != null && dtCSRProducers.Rows.Count > 0)
                {
                    DataView dvCSRProduces = dtCSRProducers.DefaultView;
                    dvCSRProduces.Sort = "USERNAME";

                    cmbCSR.DataSource = dvCSRProduces;
                    cmbCSR.DataTextField = "USERNAME";
                    cmbCSR.DataValueField = "USER_ID";
                    cmbCSR.DataBind();

                    cmbPRODUCER.DataSource = dvCSRProduces;//new DataView(dtCSRProducers);
                    cmbPRODUCER.DataTextField = "USERNAME";
                    cmbPRODUCER.DataValueField = "USER_ID";
                    cmbPRODUCER.DataBind();

                    for (int i = 0; i < cmbCSR.Items.Count; i++)
                    {
                        string arrIsActiveStatus = dtCSRProducers.Rows[i]["IS_ACTIVE"].ToString();
                        if (arrIsActiveStatus.Equals("N"))
                        {
                            cmbCSR.Items[i].Attributes.Add("style", "color:red");
                            cmbPRODUCER.Items[i].Attributes.Add("style", "color:red");

                            if (CSR != int.Parse(cmbCSR.Items[i].Value.ToString()) && arrIsActiveStatus.Equals("N"))
                                cmbCSR.Items.Remove(cmbCSR.Items[i]);
                            if (PRODUCER != int.Parse(cmbPRODUCER.Items[i].Value.ToString()) && arrIsActiveStatus.Equals("N"))
                                cmbPRODUCER.Items.Remove(cmbPRODUCER.Items[i]);
                        }
                    }
                }
                cmbCSR.Items.Insert(0, new ListItem("", ""));
                cmbPRODUCER.Items.Insert(0, new ListItem("", ""));

                //cmbCSR.Items[0].Value="-1"; 
                string agency_name = GetSystemId();
                string strCarrierSystemID = CarrierSystemID;//System.Configuration.ConfigurationManager.AppSettings.Get("CarrierSystemID").ToString();
                if (agency_name.ToUpper() != strCarrierSystemID.Trim().ToUpper())
                {
                    ListItem li = cmbCSR.Items.FindByValue(GetUserId());
                    if (li != null)
                    {
                        li.Selected = true;
                        hidCSR.Value = li.Value;
                    }
                }
            }
            catch (Exception exc)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
            }
        }

        //private void FillProducerDropDown(int AgencyID)
        //{		
        //    try
        //    {
        //        //fill Producer dropdown
        //        //DataTable dtAgencyUsers = ClsUser.GetAgencyUsers(AgencyID);
        //        ClsUser objUser = new ClsUser();
        //        DataTable dtProducers = objUser.GetProducers(AgencyID);
        //        cmbPRODUCER.Items.Clear();
        //        if(dtProducers!=null && dtProducers.Rows.Count>0)
        //        {
        //            cmbPRODUCER.DataSource			= new DataView(dtProducers);
        //            cmbPRODUCER.DataTextField		= "USERNAME";
        //            cmbPRODUCER.DataValueField		= "USER_ID";					
        //            cmbPRODUCER.DataBind();
        //            for (int i =0;i < cmbPRODUCER.Items.Count ;i++ )
        //            {
        //                string arrIsActiveStatus = dtProducers.Rows[i]["IS_ACTIVE"].ToString();
        //                if(arrIsActiveStatus.Equals("N"))
        //                    cmbPRODUCER.Items[i].Attributes.Add("style", "color:red");
        //            }
        //        }
        //        cmbPRODUCER.Items.Insert(0,new ListItem("",""));
        //        cmbPRODUCER.SelectedIndex=0;
        //    }
        //    catch(Exception exc)
        //    {
        //        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
        //    }
        //    finally
        //    {}
        //}		
        #endregion
        #region Commented by Pradeep Kushwaha(21-oct-2010) for implementation of APP_TERMS on Textbox
        //private void cmbAPP_TERMS_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    if (cmbAPP_TERMS.SelectedItem != null && cmbAPP_TERMS.SelectedItem.Value != "")//Added empty string check by Charles on 17-Sep-09 for APP/POL Optimization
        //    {
        //        int iMonths = 0;
        //        iMonths = int.Parse(cmbAPP_TERMS.SelectedItem.Value);
        //        txtAPP_EXPIRATION_DATE.Text = ConvertToDate(txtAPP_EFFECTIVE_DATE.Text).AddMonths(iMonths).ToShortDateString();
        //        //txtAPP_EXPIRATION_DATE.Text = Convert.ToDateTime(txtAPP_EFFECTIVE_DATE.Text).AddMonths(iMonths).ToString("MM/dd/yyyy"); ;
        //    }
        //}
        #endregion



        ////Added by Ruchika Chauhan on 12-Jan-2012 for Policy Screen
        //[Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        //public string GetTerm(string sInception, string sExpiry)
        //{
        //    try
        //    {
        //        SetCultureThread(GetLanguageCode());
        //        DateTime d1 = ConvertToDate(sInception);
        //        DateTime d2 = ConvertToDate(sExpiry);
        //        TimeSpan t = (d2).Subtract(d1);


        //        return t.TotalDays.ToString();
        //    }
        //    catch
        //    {
        //        return "";
        //    }
        //}


        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public string GetExpDateFromDays(string sAPP_TERMS, string sAPP_EFFECTIVE_DATE)
        {
            try
            {
                SetCultureThread(GetLanguageCode());
                return ConvertToDate(sAPP_EFFECTIVE_DATE).AddDays(int.Parse(sAPP_TERMS)).ToShortDateString();
            }
            catch
            {
                return "";
            }
        }

        private bool ValidInputXML(string inputXML)
        {
            try
            {
                bool retVal = true;
                XmlDocument tempDoc = new XmlDocument();
                if ((!inputXML.StartsWith("<ERROR")) && (inputXML.Trim() != "<INPUTXML></INPUTXML>"))
                {
                    tempDoc.LoadXml("<INPUTXML>" + inputXML + "</INPUTXML>");
                    XmlElement tempElement = tempDoc.DocumentElement;
                    XmlNodeList tempNodes = tempElement.ChildNodes;
                    foreach (XmlNode nodTempNode in tempNodes)
                    {
                        foreach (XmlNode nodTempChild in nodTempNode.ChildNodes)
                        {
                            if (nodTempChild.InnerText.Trim() == "" || nodTempChild.InnerText.Trim() == "NULL")
                            {
                                retVal = false;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    retVal = false;
                }
                return retVal;
            }
            catch (Exception exc)
            {
                throw (exc);
            }
        }

        //		private void txtPOLICY_LOB_TextChanged(object sender, System.EventArgs e)
        //		{
        //		
        //		}


        #region Quote Details

        private void SetQuoteValues()
        {
            objGeneralInformation = new ClsGeneralInformation();
            DataSet dsTemp;

            try
            {
                //Application Quote Details.
                dsTemp = objGeneralInformation.GetQuoteDetails(int.Parse(hidCustomerID.Value), int.Parse(hidAppID.Value), int.Parse(hidAppVersionID.Value));

                if (dsTemp.Tables[0].Rows.Count > 0)
                {
                    gIntQuoteID = Convert.ToInt32(dsTemp.Tables[0].Rows[0]["QUOTE_ID"].ToString());
                    gIntShowQuote = 1;
                    btnAppQuote.Visible = true;
                }
                else
                {
                    btnAppQuote.Visible = false;
                }


                //Policy Quote Details
                dsTemp = objGeneralInformation.GetPolicyQuoteDetails(int.Parse(hidCustomerID.Value), int.Parse(hidPolicyID.Value), int.Parse(hidPolicyVersionID.Value));

                if (dsTemp.Tables[0].Rows.Count > 0)
                {

                    if (dsTemp.Tables[0].Rows[0]["POLICY_PREMIUM_XML"] != DBNull.Value && dsTemp.Tables[0].Rows[0]["POLICY_PREMIUM_XML"].ToString().Trim() != "")
                    {
                        gIntPOLICY_ID = int.Parse(hidPolicyID.Value);
                        gIntPOLICY_VERSION_ID = int.Parse(hidPolicyVersionID.Value);
                        gIntShowPolicyQuote = 4;
                        btnPolicyQuote.Visible = true;
                    }
                    else
                    {
                        btnPolicyQuote.Visible = false;
                    }

                }
                else
                {
                    btnPolicyQuote.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        #endregion
        /*
		private void SetMultiPolicy()
		{
			objGeneralInformation = new ClsGeneralInformation();
			string PolicyNumber = "";
			
			int AgencyID = objGeneralInformation.GetAgencyId(GetSystemId());
			PolicyNumber = txtPOLICY_NUMBER.Text;

			lblAllPolicy.Text = objGeneralInformation.GetAllPolicyNumber(int.Parse(GetCustomerID()),AgencyID,PolicyNumber);
			lblEligbilePolicy.Text = objGeneralInformation.GetEligiblePolicyNumber(int.Parse(GetCustomerID()),AgencyID,int.Parse(GetLOBID()),PolicyNumber);

			if (lblAllPolicy.Text.Trim() == "")
				lblAllPolicy.Text = "N.A.";

			if (lblEligbilePolicy.Text.Trim() == "")
				lblEligbilePolicy.Text = "N.A.";
			
		}*/


        /// <summary>
        /// fetch the active billing plan XML 
        /// </summary>		
        /// 
        private void CheckBillingPlan()
        {
            try
            {
                DataSet dspol = new DataSet();
                if (Request.QueryString["POLICY_ID"].ToString() != "")
                {

                    dspol = ClsInstallmentInfo.GetapppolDownpayment(gIntCUSTOMER_ID, int.Parse(Request.QueryString["POLICY_ID"].ToString()), int.Parse(Request.QueryString["POLICY_VERSION_ID"].ToString()), "DOWNPAY");
                    //        DataTable dtCSRProducers = objUser.GetCSRProducers(AgencyID, Convert.ToInt16(GetLOBID()));

                    //        cmbCSR.Items.Clear();
                    if (dspol != null && dspol.Tables[0].Rows.Count > 0)
                    {
                        hidPOLDOWN_PAY_MODE.Value = dspol.Tables[0].Rows[0]["DOWN_PAY_MODE"].ToString();


                    }

                    int intPolicyTerm = int.Parse(ClsCommon.FetchValueFromXML("APP_TERMS", hidOldData.Value));

                    int iLobiid = int.Parse(ClsCommon.FetchValueFromXML("POLICY_LOB", hidOldData.Value));
                    DataSet dsPlan = new DataSet();
                    if (iLobiid != 0 && GetTransaction_Type() == MASTER_POLICY)//GetProduct_Type(iLobiid) == MASTER_POLICY)
                    {
                        DataTable dt = new DataTable();
                        dt = ClsInstallmentInfo.GetApplicableInstallmentPlans(0).Tables[0].Select("PLAN_TYPE in('MAUTO','MMANNUAL')").CopyToDataTable<DataRow>();
                        dsPlan.Tables.Add(dt);

                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        dt = ClsInstallmentInfo.GetApplicableInstallmentPlans(0).Tables[0].Select("PLAN_TYPE NOT IN ('MAUTO','MMANNUAL') OR PLAN_TYPE IS NULL").CopyToDataTable<DataRow>(); ;
                        dsPlan.Tables.Clear();
                        dsPlan.Tables.Add(dt.Copy());
                        //dsPlan = ClsInstallmentInfo.GetApplicableInstallmentPlans(0);
                    }
                    cmbINSTALL_PLAN_ID.DataSource = dsPlan;
                    cmbINSTALL_PLAN_ID.DataTextField = "BILLING_PLAN";
                    cmbINSTALL_PLAN_ID.DataValueField = "INSTALL_PLAN_ID";
                    cmbINSTALL_PLAN_ID.DataBind();
                    cmbINSTALL_PLAN_ID.Items.Insert(0, "");

                    cmbINSTALL_PLAN_ID.SelectedIndex = cmbINSTALL_PLAN_ID.Items.IndexOf(cmbINSTALL_PLAN_ID.Items.FindByValue(ClsCommon.FetchValueFromXML("INSTALL_PLAN_ID", hidOldData.Value)));



                    //DataSet ds = ClsInstallmentInfo.GetApplicableInstallmentPlans(gIntCUSTOMER_ID,int.Parse(Request.QueryString["POLICY_ID"].ToString()),int.Parse(Request.QueryString["POLICY_VERSION_ID"].ToString()),"POL");
                    hidBillingPlan.Value = dsPlan.GetXml();
                    //get Deactive Plan ID 
                    string PlanXML = hidBillingPlan.Value.ToString();
                    XmlDocument doc = new XmlDocument();
                    PlanXML = PlanXML.Replace("&AMP;", "&amp;");
                    PlanXML = PlanXML.Replace("\r\n", "");
                    doc.LoadXml(PlanXML);
                    if (doc != null)
                    {
                        foreach (XmlNode node in doc.SelectNodes("NewDataSet/Table"))
                        {
                            if (node != null)
                            {
                                if (node.SelectSingleNode("IS_ACTIVE").InnerText == "N")
                                    hidDEACTIVE_INSTALL_PLAN_ID.Value = node.FirstChild.InnerText.ToString();
                            }
                        }
                        int count = doc.SelectNodes("NewDataSet/Table/IS_ACTIVE[contains(.,'N')]").Count;
                        if (count == 0)
                            hidDEACTIVE_INSTALL_PLAN_ID.Value = "";


                    }

                }
                else
                {
                    DataTable dt = new DataTable();
                    dt = ClsInstallmentInfo.GetApplicableInstallmentPlans(0).Tables[0].Select("PLAN_TYPE NOT IN ('MAUTO','MMANNUAL') OR PLAN_TYPE IS NULL").CopyToDataTable<DataRow>(); ;
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);
                    hidBillingPlan.Value = ds.GetXml();

                    cmbINSTALL_PLAN_ID.Items.Clear();
                    cmbINSTALL_PLAN_ID.DataSource = ds;
                    cmbINSTALL_PLAN_ID.DataTextField = "BILLING_PLAN";
                    cmbINSTALL_PLAN_ID.DataValueField = "INSTALL_PLAN_ID";
                    cmbINSTALL_PLAN_ID.DataBind();
                    cmbINSTALL_PLAN_ID.Items.Insert(0, "");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {

                ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
                ClsPolicyInfo objPolicyInfo = new ClsPolicyInfo();

                base.PopulateModelObject(objPolicyInfo, hidOldData.Value);
                objPolicyInfo.CREATED_BY = int.Parse(GetUserId());

                String CalledFrom = "POL";

                int retvalue = objGeneralInformation.RejectAppPol(objPolicyInfo, CalledFrom);
                string JavascriptText = "";
                if (retvalue > 0)
                {

                    lblMessage.Text = ClsMessages.FetchGeneralMessage("1101");
                    lblMessage.Visible = true;
                    hidRefresh.Value = "R";
                    hidFormSaved.Value = "1";
                    hidPolicyStatus.Value = "Rejected";
                    btnReject.Visible = false;
                    btnSave.Visible = false;
                    btnCopy.Visible = true;
                    btnReset.Visible = false;

                    JavascriptText = "<script> window.open('/cms/Policies/Aspx/PolicyRejectReson.aspx?CUSTOMER_ID=" + objPolicyInfo.CUSTOMER_ID + "&POLICY_ID=" + objPolicyInfo.POLICY_ID + "&POLICY_VERSION_ID=" + objPolicyInfo.POLICY_VERSION_ID + "','PolicyReject','resizable=yes,scrollbars=yes,left=150,top=50,width=500,height=360'); </script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "ShowPolicyRejection", JavascriptText);
                }


            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1098") + "\n" + ex.Message.ToString();
                lblMessage.Visible = true;
            }
        }

        private void btnCopy_Click(object sender, System.EventArgs e)
        {
            try
            {

                string CreatedBy = GetUserId();
                ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
                // ClsPolicyInfo objGeneralInfo = new ClsPolicyInfo();
                ClsPolicyInfo objGeneralInfo = GetFormValue();

                //objGeneralInfo.APP_TERMS = cmbAPP_TERMS.SelectedValue;// Commented by Pradeep Kushwaha(21-oct-2010) for implementation of APP_TERMS on Textbox

                objGeneralInfo.APP_TERMS = txtAPP_TERMS.Text.Trim();// Added by Pradeep Kushwaha(21-oct-2010) for implementation of APP_TERMS on Textbox

                if (objGeneralInfo.POLICY_LOB == "0")
                    objGeneralInfo.POLICY_SUBLOB = null;

                objGenInfo.TransactionRequired = true;

                if (cmbDIV_ID_DEPT_ID_PC_ID.SelectedValue != "")
                {
                    string[] strDivDeptPC = cmbDIV_ID_DEPT_ID_PC_ID.SelectedValue.Split('^');

                    objGeneralInfo.DIV_ID = Convert.ToInt32(strDivDeptPC[0]);
                    objGeneralInfo.DEPT_ID = Convert.ToInt32(strDivDeptPC[1]);
                    objGeneralInfo.PC_ID = Convert.ToInt32(strDivDeptPC[2]);
                }
                else
                {
                    objGeneralInfo.DIV_ID = 0;
                    objGeneralInfo.DEPT_ID = 0;
                    objGeneralInfo.PC_ID = 0;
                }

                objGeneralInfo.APP_EFFECTIVE_DATE = ConvertToDate(txtAPP_EFFECTIVE_DATE.Text);

                // String NEW_POL_NUMBER = ClsGeneralInformation.GenerateAppPolNumber(int.Parse(hidLOBID.Value), objGeneralInfo.DIV_ID, objGeneralInfo.APP_EFFECTIVE_DATE, "APP");
                String NEW_POL_NUMBER = ClsGeneralInformation.GenerateApplicationNumber(int.Parse(hidLOBID.Value), int.Parse(hidAPP_AGENCY_ID.Value == "" ? "0" : hidAPP_AGENCY_ID.Value));
                objGeneralInfo.CUSTOMER_ID = int.Parse(hidCustomerID.Value);
                objGeneralInfo.POLICY_ID = int.Parse(hidPolicyID.Value);
                objGeneralInfo.POLICY_VERSION_ID = int.Parse(hidPolicyVersionID.Value);
                objGeneralInfo.CREATED_BY = int.Parse(CreatedBy == "" ? "0" : CreatedBy);

                int RetVal = objGenInfo.CopyAppPolFromReject(objGeneralInfo, NEW_POL_NUMBER, "POL");

                if (RetVal > 0)
                {
                    hidFormSaved.Value = "1";
                    SetAppID(objGeneralInfo.APP_ID.ToString());
                    SetAppVersionID(objGeneralInfo.APP_VERSION_ID.ToString());
                    SetPolicyID(objGeneralInfo.POLICY_ID.ToString());
                    SetPolicyVersionID(objGeneralInfo.POLICY_VERSION_ID.ToString());

                    string strCopyReject = @"<script>  alert('" + objResourceMgr.GetString("lblRejCopy") + NEW_POL_NUMBER + "');" +
                    " parent.location.href = 'PolicyTab.aspx?CUSTOMER_ID=" + objGeneralInfo.CUSTOMER_ID.ToString() + "&APP_ID=" +
                    objGeneralInfo.APP_ID.ToString() + "&APP_VERSION_ID=" + objGeneralInfo.APP_VERSION_ID.ToString() + "&POLICY_ID=" +
                    objGeneralInfo.POLICY_ID.ToString() + "&POLICY_VERSION_ID=" + objGeneralInfo.POLICY_VERSION_ID.ToString() + "'   </script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "COPYREJECT", strCopyReject);

                    //lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1105");
                    Session["LoadedAfterSave"] = "COPY";
                    SetPolicyStatus("");
                }
                else
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("20");
                    hidFormSaved.Value = "2";
                }
                lblMessage.Visible = true;
                lblMessage.Focus();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataTable AjaxFillPolicyType(string SelectType)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable(SelectType);
                return dt;
            }
            catch
            {
                return null;
            }

        }

        protected void btnSendCancelNotice_Click(object sender, EventArgs e)
        {

        }
        /*Commented by Charles on 21-Aug-09 for APP/POL OPTIMISATION
        private void cmbAGENCY_ID_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            cmbUNDERWRITER.Items.Clear();  
            int AGENCY_ID=int.Parse(cmbAGENCY_ID.SelectedValue) ; 	

            FillCSRDropDown(int.Parse(cmbAGENCY_ID.SelectedItem.Value));
            FillProducerDropDown(int.Parse(cmbAGENCY_ID.SelectedItem.Value));

            Cms.BusinessLayer.BlCommon.ClsAgency objAgency = new ClsAgency();
            DataSet dsRecieve =new DataSet();
            dsRecieve=objAgency.PopulateassignedUnderWriter(AGENCY_ID,Convert.ToInt32(hidLOBID.Value));
            if(dsRecieve.Tables.Count<1)
                return;
            this.cmbUNDERWRITER.DataSource = dsRecieve.Tables[0];
            this.cmbUNDERWRITER.DataValueField = dsRecieve.Tables[0].Columns[0].ToString();
            this.cmbUNDERWRITER.DataTextField = dsRecieve.Tables[0].Columns[1].ToString();
            this.cmbUNDERWRITER.DataBind();
            this.cmbUNDERWRITER.Items.Insert(0,"");
            this.cmbUNDERWRITER.SelectedIndex =0;
        }*/

        /*
		private void cmbSTATE_ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(cmbSTATE_ID.SelectedIndex !=0 && cmbSTATE_ID.SelectedValue != "")
				{
					int stateId=Convert.ToInt32(cmbSTATE_ID.SelectedValue);
					hidSTATE_ID.Value=stateId.ToString();
					int selVal=int.Parse(hidLOBID.Value);
					if (selVal == 1) //For Homeowners.
					{	
						if(stateId==22)
						{
							cmbPOLICY_TYPE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("HOPTPM");
						}
						else
						{
							cmbPOLICY_TYPE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("HOPTYP");
						}
					}
					else if(selVal == 6) 	// For Rental Dwelling.
					{
					
						if(stateId==22)
						{
							cmbPOLICY_TYPE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RTPTYP");
					
						}
						else
						{
							cmbPOLICY_TYPE.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RTPTYI");  
						}
					}					
					//--------------------------End--------------------.
					cmbPOLICY_TYPE.DataTextField="LookupDesc";
					cmbPOLICY_TYPE.DataValueField="LookupID";
					cmbPOLICY_TYPE.DataBind();
					cmbPOLICY_TYPE.Items.Insert(0,"");
				}
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}

		}
         */


    }
}

