/******************************************************************************************
<Author					: -  Charles Gomes
<Creation Date			: -	 25-Feb-2010
<Description			: -  New Add Policy Page	

<Modified Date			: - 16-06-2010
<Modified By			: - Pradeep Kushwaha
<Purpose				: - Add Reject button and Copy button ,Implemetion of rejected application
****************************************************************************************** */

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
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Configuration;

using Cms.BusinessLayer.BlApplication;
using Cms.Model.Application;
using Cms.Model.Quote;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BlProcess;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlQuote;
using Cms.BusinessLayer.BlClient;
using Cms.BusinessLayer.BlAccount;
using Cms.Model.Policy;
using System.Resources;
using System.Reflection;
using Ajax;
using System.Collections.Generic;
using Cms.ExceptionPublisher.ExceptionManagement;
//using Cms.ExceptionPublisher;
//using System.IO;
//using System.Xml;
namespace Cms.Policies.Aspx
{
    public class PolicyGeneralInfo : Cms.Policies.policiesbase
    {
        #region Page controls declaration
        protected System.Web.UI.WebControls.TextBox txtAPP_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtPOLICY_STATUS;
        protected System.Web.UI.WebControls.TextBox txtPOLICY_DISP_VERSION;
        protected System.Web.UI.WebControls.TextBox txtAPP_INCEPTION_DATE;
        protected System.Web.UI.WebControls.TextBox txtAPP_EFFECTIVE_DATE;
        protected System.Web.UI.WebControls.TextBox txtAPP_EXPIRATION_DATE;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMERNAME;
        protected System.Web.UI.WebControls.TextBox txtCUSTOMER_NAME;
        protected System.Web.UI.WebControls.TextBox txtRECEIVED_PRMIUM;
        protected System.Web.UI.WebControls.TextBox txtBILLTO;

        // protected System.Web.UI.WebControls.TextBox txtREMARKS_INFO;
        protected System.Web.UI.WebControls.TextBox txtPOLICY_LEVEL_COMISSION;
        protected System.Web.UI.WebControls.TextBox txtPREFERENCE_DAY;
        protected System.Web.UI.WebControls.TextBox txtBROKER_REQUEST_NO;
        protected System.Web.UI.WebControls.TextBox txtPOLICY_DESCRIPTION;

        //protected System.Web.UI.WebControls.TextBox txtYEAR_AT_CURR_RESI;
        //protected System.Web.UI.WebControls.TextBox txtYEARS_AT_PREV_ADD;
        //protected System.Web.UI.WebControls.Label capPROXY_SIGN_OBTAINED;
        //protected System.Web.UI.WebControls.Label lblAllPoliciesHeader;
        //protected System.Web.UI.WebControls.Label capPOLICY_TYPE;
        //protected System.Web.UI.WebControls.Label lblPolicies;
        //protected System.Web.UI.WebControls.Label lblAllPolicy;
        //protected System.Web.UI.WebControls.Label lblPoliciesDiscount;
        //protected System.Web.UI.WebControls.Label lblEligbilePolicy;
        //protected System.Web.UI.WebControls.Label capBILL_MORTAGAGEE;
        //protected System.Web.UI.WebControls.Label lblBILL_MORTAGAGEE;        
        //protected System.Web.UI.WebControls.Label capSTATE_ID;
        //protected System.Web.UI.WebControls.Label capCHARGE_OFF_PRMIUM;
        //protected System.Web.UI.WebControls.Label capAPP_POLICY_TYPE;
        //protected System.Web.UI.WebControls.Label capYEAR_AT_CURR_RESI;
        //protected System.Web.UI.WebControls.Label capPIC_OF_LOC;
        //protected System.Web.UI.WebControls.Label capYEARS_AT_PREV_ADD;
        protected System.Web.UI.WebControls.DropDownList cmbPOLICY_TYPE;
        //protected System.Web.UI.WebControls.DropDownList cmbPROPRTY_INSP_CREDIT;
        //protected System.Web.UI.WebControls.DropDownList cmbCHARGE_OFF_PRMIUM;
        //protected System.Web.UI.WebControls.DropDownList cmbSTATE_ID;
        //protected System.Web.UI.WebControls.DropDownList cmbPROXY_SIGN_OBTAINED;
        //protected System.Web.UI.WebControls.DropDownList cmbPIC_OF_LOC;

        protected System.Web.UI.WebControls.Label capPRODUCER;
        protected System.Web.UI.WebControls.Label lblAGENCY_DISPLAY_NAME;
        protected System.Web.UI.WebControls.Label lblManHeader;
        protected System.Web.UI.WebControls.Label lblAppHeader;
        protected System.Web.UI.WebControls.Label lblTermHeader;
        protected System.Web.UI.WebControls.Label lblAgencyHeader;
        protected System.Web.UI.WebControls.Label lblRemarksHeader;
        protected System.Web.UI.WebControls.Label lblBillingHeader;
        protected System.Web.UI.WebControls.Label capUNDERWRITER;
        protected System.Web.UI.WebControls.Label capDOWN_PAY_MODE;
        protected System.Web.UI.WebControls.Label capCUSTOMER_ID;
        protected System.Web.UI.WebControls.Label capAPP_STATUS;
        protected System.Web.UI.WebControls.Label capAPP_NUMBER;
        protected System.Web.UI.WebControls.Label capAPP_VERSION;
        protected System.Web.UI.WebControls.Label capAPP_TERMS;
        protected System.Web.UI.WebControls.Label capAPP_INCEPTION_DATE;
        protected System.Web.UI.WebControls.Label capAPP_EFFECTIVE_DATE;
        protected System.Web.UI.WebControls.Label capAPP_EXPIRATION_DATE;
        protected System.Web.UI.WebControls.Label capAPP_LOB;
        protected System.Web.UI.WebControls.Label capPOLICY_SUBLOB;
        protected System.Web.UI.WebControls.Label capAGENCY_ID;
        protected System.Web.UI.WebControls.Label capCSR;
        protected System.Web.UI.WebControls.Label lblFormLoadMessage;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Label capBILL_TYPE;
        protected System.Web.UI.WebControls.Label capINSTALL_PLAN_ID;//, capCOMPLETE_APP;//, capPROPRTY_INSP_CREDIT;        
        protected System.Web.UI.WebControls.Label capRECEIVED_PRMIUM;
        protected System.Web.UI.WebControls.Label lblDOWN_PAY_MODE;
        protected System.Web.UI.WebControls.Label lblINSTALL_PLAN_ID;
        protected System.Web.UI.WebControls.Label capBILL_TYPE_ID;
        protected System.Web.UI.WebControls.Label lblHeader;
        //protected System.Web.UI.WebControls.Label lblUNDERWRITER_UN_ASG;
        protected System.Web.UI.WebControls.Label lblProduct;
        protected System.Web.UI.WebControls.Label capPOLICY_CURRENCY;
        protected System.Web.UI.WebControls.Label capPOLICY_LEVEL_COMISSION;
        protected System.Web.UI.WebControls.Label capPOLICY_LEVEL_COMM_APPLIES;
        protected System.Web.UI.WebControls.Label capCONTACT_PERSON;
        protected System.Web.UI.WebControls.Label capBILLTO;
        protected System.Web.UI.WebControls.Label capPAYOR;
        protected System.Web.UI.WebControls.Label capCO_INSURANCE;
        protected System.Web.UI.WebControls.Label capTRANSACTION_TYPE;
        protected System.Web.UI.WebControls.Label capDIV_ID_DEPT_ID_PC_ID;
        protected System.Web.UI.WebControls.Label capPREFERENCE_DAY;
        protected System.Web.UI.WebControls.Label capBROKER_REQUEST_NO;
        protected System.Web.UI.WebControls.Label capREMARKS;
        protected System.Web.UI.WebControls.Label capBROKER_COMM_FIRST_INSTM;
        protected System.Web.UI.WebControls.Label capACTIVITY;
        protected System.Web.UI.WebControls.Label capMODALITY;
        protected System.Web.UI.WebControls.Label capState;
        //protected System.Web.UI.WebControls.DropDownList cmbAPP_TERMS; //Commented by Pradeep Kushwaha(20-oct-2010) for implementation of APP_TERMS on Textbox
        protected System.Web.UI.WebControls.DropDownList cmbAPP_LOB;
        protected System.Web.UI.WebControls.DropDownList cmbPOLICY_SUBLOB;
        protected System.Web.UI.WebControls.DropDownList cmbDOWN_PAY_MODE;
        protected System.Web.UI.WebControls.DropDownList cmbINSTALL_PLAN_ID;
        protected System.Web.UI.WebControls.DropDownList cmbBILL_TYPE;
        protected System.Web.UI.WebControls.DropDownList cmbPOLICY_CURRENCY;
        protected System.Web.UI.WebControls.DropDownList cmbCONTACT_PERSON;
        protected System.Web.UI.WebControls.DropDownList cmbPAYOR;
        protected System.Web.UI.WebControls.DropDownList cmbCO_INSURANCE;
        protected System.Web.UI.WebControls.DropDownList cmbAGENCY_ID;
        protected System.Web.UI.WebControls.DropDownList cmbTRANSACTION_TYPE;
        protected System.Web.UI.WebControls.DropDownList cmbDIV_ID_DEPT_ID_PC_ID;
        protected System.Web.UI.WebControls.DropDownList cmbState_ID;

        //Added by Agniswar for Singapore Implementation
        protected System.Web.UI.WebControls.DropDownList cmbFUND_TYPE;
        protected System.Web.UI.WebControls.DropDownList cmbBILLING_CURRENCY;

        protected System.Web.UI.WebControls.Label capFUND_TYPE;
        protected System.Web.UI.WebControls.Label capBILLING_CURRENCY;

        // Till Here

        protected System.Web.UI.HtmlControls.HtmlSelect cmbCSR;
        protected System.Web.UI.HtmlControls.HtmlSelect cmbPRODUCER;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppVersionID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidReset;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyNumber;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDOWN_PAY_MODE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidBILL_TYPE_FLAG;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIsTerminated;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLangCulture;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSubLOBXml;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_AGENCY_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCSR;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidProducer;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCallefroms;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOBID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSUB_LOB;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOBXML, hidDEPT_ID, hidPC_ID, hidDepartmentXml, hidProfitCenterXml, hidPOLICY_TYPE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDeleteApp;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidStateID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidUnderwriter;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEACTIVE_INSTALL_PLAN_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidQuoteXML;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidINSTALL_PLAN_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidBillingPlan;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFocusFlag;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidRuleVerify;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAlert;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidBILL_TYPE_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIs_Agency;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFULL_PAY_PLAN_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyVersionID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyStatus;
        public System.Web.UI.HtmlControls.HtmlInputHidden lblDelete;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDIV_ID_DEPT_ID_PC_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPAYOR;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidRefresh;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidRulesMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidApplicationStatus;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_TERMS;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidInstall;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLDOWN_PAY_MODE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_CURRENCY;

        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected Cms.CmsWeb.Controls.CmsButton btnBack;
        protected Cms.CmsWeb.Controls.CmsButton btnCustomerAssistant;
        protected Cms.CmsWeb.Controls.CmsButton btnConvertAppToPolicy;
        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
        protected Cms.CmsWeb.Controls.CmsButton btnCreateNewVersion;
        protected Cms.CmsWeb.Controls.CmsButton btnDelete;
        protected Cms.CmsWeb.Controls.CmsButton btnVerifyApplication;
        protected Cms.CmsWeb.Controls.CmsButton btnSubmitAnyway;

        protected Cms.CmsWeb.Controls.CmsButton btnReject;
        protected Cms.CmsWeb.Controls.CmsButton btnCopy;

        protected System.Web.UI.HtmlControls.HtmlTableRow policyTR;
        protected System.Web.UI.HtmlControls.HtmlTableRow trFORMMESSAGE;
        protected System.Web.UI.HtmlControls.HtmlTableRow trDETAILS;
        protected System.Web.UI.HtmlControls.HtmlTable tblstate;// added by sonal to implemet state for old products specially product ID < 8
        protected System.Web.UI.HtmlControls.HtmlGenericControl div_poltype;
        protected System.Web.UI.WebControls.TextBox txtAPP_TERMS;//Added By Pradeep Kushwaha on 20-0ct-2010

        protected System.Web.UI.WebControls.RegularExpressionValidator revAPP_TERMS;
        //protected System.Web.UI.HtmlControls.HtmlTableRow trCOMPLETE_APP;
        //protected System.Web.UI.HtmlControls.HtmlTableRow trPropInspCr;
        //protected System.Web.UI.HtmlControls.HtmlTableRow trbutton;

        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAPP_TERMS;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAPP_EFFECTIVE_DATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAPP_EXPIRATION_DATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAPP_LOB;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvBILL_TYPE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDOWN_PAY_MODE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvINSTALL_PLAN_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENCY_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_CURRENCY;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCO_INSURANCE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPAYOR;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_SUBLOB;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_LEVEL_COMISSION;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvDIV_ID_DEPT_ID_PC_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCSR;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvState_ID;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATE_ID;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvCUSTOMER_ID;        
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvPROXY_SIGN_OBTAINED;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_TYPE;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvPIC_OF_LOC;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvPROPRTY_INSP_CREDIT;        
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvYEARS_AT_PREV_ADD;   
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvPRODUCER;
     
        //protected System.Web.UI.WebControls.RangeValidator rngYEAR_AT_CURR_RESI;
        protected System.Web.UI.WebControls.RangeValidator rngPREFERENCE_DAY;

        protected System.Web.UI.WebControls.RegularExpressionValidator revAPP_EFFECTIVE_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revAPP_INCEPTION_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revRECEIVED_PRMIUM;
        protected System.Web.UI.WebControls.RegularExpressionValidator revPOLICY_LEVEL_COMISSION;
        protected System.Web.UI.WebControls.RegularExpressionValidator revAPP_NUMBER;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revCHARGE_OFF_PRMIUM;
        //protected System.Web.UI.WebControls.RegularExpressionValidator revYEAR_AT_CURR_RESI;

        protected System.Web.UI.HtmlControls.HtmlImage imgSelect;

        protected System.Web.UI.WebControls.HyperLink hlkCalandarDate;
        protected System.Web.UI.WebControls.HyperLink hlkInceptionDate;
        protected System.Web.UI.WebControls.HyperLink hlkAPP_EXPIRATION_DATE;

        //protected System.Web.UI.WebControls.CheckBox chkCOMPLETE_APP;
        protected System.Web.UI.WebControls.CheckBox chkPOLICY_LEVEL_COMM_APPLIES;
        protected System.Web.UI.WebControls.CheckBox chkBROKER_COMM_FIRST_INSTM;

        protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;

        //protected System.Web.UI.WebControls.CustomValidator csvYEARS_AT_PREV_ADD;
        protected System.Web.UI.WebControls.CustomValidator csvAPP_EFFECTIVE_DATE;
        protected System.Web.UI.WebControls.CustomValidator csvINSTALL_PLAN_ID;
        protected System.Web.UI.WebControls.CustomValidator csvPOLICY_LEVEL_COMISSION;

        //Added by Lalit Nov 18, 2010
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvTRANSACTION_TYPE;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOL_BILL_PLANMSG;
        protected System.Web.UI.WebControls.RangeValidator rngAPP_INCEPTION_DATE;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOL_POLICY_STATUS;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidUSER_SUPERVISOR;

        protected System.Web.UI.WebControls.Label capOLD_POLICY_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtOLD_POLICY_NUMBER;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOLD_POLICY_NUMBER;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_EFFECTIVE_DATE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerType;
        protected System.Web.UI.HtmlControls.HtmlTable tblTerm;
        protected string strBillType = "";

        protected string strCustomerType = "";
       

        #endregion

        protected string InceptionMsg;

        #region Local form variables
        string oldXML;
        string strLOB_ID = "";
        private System.Resources.ResourceManager objResourceMgr;
        private string strRowId = "", strFormSaved;
        private static string[] strDateNodes = null;

        public string primaryKeyValues = "";
        public string strNewVersion = "";
        public int strVersionID = -1;
        public string strPolicy;
        protected int gIntPopulate = 0, gIntShowQuote = 0, gIntShowVerificationResult = 0, intSubmitAnyway = 0;
        protected string gStrOldXML = "";

        private const string CALLED_FROM_CLIENT = "CLT";
        private const string CALLED_FROM_INNER_CLIENT = "InCLT";
        private const string CALLED_FROM_APPLICATION = "APP";
        private string DefaultAppStatus;//= "Incomplete";
        public string TEMP_APP_NUMBER= "To be generated";
        public const string CALLED_FROM_GEN_INFO = "GEN_INFO";

        protected int gIntQuoteID = 0, gIntCUSTOMER_ID = 0, gIntAPP_ID = 0, gIntAPP_VERSION_ID = 0;
        protected string gstrLobID = "";

        private DataSet dsPolicy = null;

        //Defining the business layer class object
        ClsGeneralInformation objGeneralInformation;
        ClsStates objClsStates;
        public string appTerms;
        public string exp_Date;
        public string delStr = "0";
        public string gIntShowQuote4 = "";

        public string divID;
        public int deptID;
        public string PCID;

        public string strCarrierSystemID;//= CarrierSystemID;//System.Configuration.ConfigurationManager.AppSettings.Get("CarrierSystemID").ToString();
        public string strAgency_ID = "";
        public string userID;
        public string App_Eff_Date;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOL_TRAN_TYPE;

        protected System.Web.UI.WebControls.RequiredFieldValidator rfvAPP_INCEPTION_DATE;
        #endregion

        #region PageLoad event
        private void Page_Load(object sender, System.EventArgs e)
        {
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            Ajax.Utility.RegisterTypeForAjax(typeof(PolicyGeneralInfo));
           

            if (!Page.IsPostBack)
            {
                hidLangCulture.Value = GetLanguageCode();

                strDateNodes = new string[3];

                strDateNodes[0] = "APP_INCEPTION_DATE";
                strDateNodes[1] = "APP_EFFECTIVE_DATE";
                strDateNodes[2] = "APP_EXPIRATION_DATE";
            }

            SetCultureThread(hidLangCulture.Value);
            Page.DataBind();
            //lblMessage.Visible = true;
            strPolicy = "0";

            #region Set CustID , AppID, AppVerID
            gIntCUSTOMER_ID = int.Parse(Request["CUSTOMER_ID"] == null ? "0" : Request["CUSTOMER_ID"].ToString());
            if (hidOldData.Value.Trim() != null && hidOldData.Value.Trim() != "" && hidOldData.Value.Trim() != "0")
            {
                gIntAPP_ID = int.Parse(Request["APP_ID"] == null || Request["APP_ID"] == "" ? ClsCommon.FetchValueFromXML("APP_ID", hidOldData.Value) : Request["APP_ID"].ToString());
                gIntAPP_VERSION_ID = int.Parse(Request["APP_VERSION_ID"] == null || Request["APP_VERSION_ID"] == "" ? ClsCommon.FetchValueFromXML("APP_VERSION_ID", hidOldData.Value) : Request["APP_VERSION_ID"].ToString());
            }
            #endregion

            objResourceMgr = new ResourceManager("Cms.Policies.Aspx.PolicyGeneralInfo", Assembly.GetExecutingAssembly());
            //SetCaptions();

            InceptionMsg = ClsMessages.FetchGeneralMessage("1343");
            if (GetLanguageID() == "2")// changed by praveer for TFS# 1959
            {
                TEMP_APP_NUMBER = "Gerada Pelo Sistema";
            }           
           
            if (!Page.IsPostBack)
            {
                #region !PostBack

                #region Add Attributes
                btnVerifyApplication.Attributes.Add("onClick", "VerifyPolicy();return false;");
                btnDelete.Attributes.Add("onclick", "javascript:return ShowAlertMessageForDelete();");
                hlkCalandarDate.Attributes.Add("OnClick", "fPopCalendar(document.APP_LIST.txtAPP_EFFECTIVE_DATE,document.APP_LIST.txtAPP_EFFECTIVE_DATE)"); //Javascript Implementation for Calender				
                hlkInceptionDate.Attributes.Add("OnClick", "fPopCalendar(document.APP_LIST.txtAPP_INCEPTION_DATE,document.APP_LIST.txtAPP_INCEPTION_DATE)");
                hlkAPP_EXPIRATION_DATE.Attributes.Add("OnClick", "fPopCalendar(document.APP_LIST.txtAPP_EXPIRATION_DATE,document.APP_LIST.txtAPP_EXPIRATION_DATE)");    
           
                btnBack.Attributes.Add("onclick", "javascript:return DoBack();");
                btnCustomerAssistant.Attributes.Add("onclick", "javascript:return DoBackToAssistant();");
                txtAPP_EFFECTIVE_DATE.Attributes.Add("onblur", "javascript:setTimeout('ChangeDefaultDate();',100);");

                if (GetSystemId().ToUpper() == "S001" || GetSystemId().ToUpper() == "SUAT")
                {
                    cmbINSTALL_PLAN_ID.Attributes.Add("onchange","javascript:SetINSTALL_PLAN_ID();");
                }
                else
                {
                    cmbINSTALL_PLAN_ID.Attributes.Add("onchange", "javascript:SetINSTALL_PLAN_ID();fillDownPayMode();");
                }

                //Added by RC
                if (GetSystemId().ToUpper() == "S001" || GetSystemId().ToUpper() == "SUAT")
                {
                    txtAPP_TERMS.Attributes.Add("onchange", "javascript:SetAPP_TERMS();fillBillPlan();setDefaultPlan();");
                }
                else
                {
                    txtAPP_TERMS.Attributes.Add("onchange", "javascript:SetAPP_TERMS();fillBillPlan();setDefaultPlan();setExpDate();");
                }
                

                //txtAPP_EFFECTIVE_DATE.Attributes.Add("onChange", "javascript:ChangeDefaultDate();");

                //Added by RC
                if (GetSystemId().ToUpper() == "S001" || GetSystemId().ToUpper() == "SUAT")
                {
                    txtAPP_EFFECTIVE_DATE.Attributes.Add("onChange", "javascript:FillAgency();");
                }
                else
                {
                    txtAPP_EFFECTIVE_DATE.Attributes.Add("onChange", "javascript:setExpDate();FillAgency();");
                }
               
                //txtYEAR_AT_CURR_RESI.Attributes.Add("onblur", "javascript:DisplayPreviousYearDesc()");                

                //btnReject.Attributes.Add("onClick", "javascript:RefreshClient();");//Added by Pradeep Kushwaha on 17 -june -2010 to refresh the client while reject application

                string rootPath = Cms.BusinessLayer.BlCommon.ClsCommon.GetApplicationPath();
                string url = rootPath + @"/cmsweb/aspx/LookupForm1.aspx";
                imgSelect.Attributes.Add("onclick", "javascript:OpenLookupWithFunction('" + url + "','CUSTOMER_ID','Name','hidCustomerID','txtCUSTOMER_NAME','CustAndStateLookupForm','Customer','','SetLookupValues()')");
                btnReset.Attributes.Add("onclick", "javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
                btnSave.Attributes.Add("onclick", "javascript:SetBillTypeFlag();");
                //cmbAPP_TERMS.Attributes.Add("onChange", "return cmbAPP_TERMS_Change();");
                //cmbAPP_TERMS.Attributes.Add("onkeypress", "javascript:if(event.keyCode==13){fillBillingPlan();}");
                #endregion

                txtAPP_INCEPTION_DATE.Text = ConvertToDateCulture(DateTime.Now);//.ToString("MM/dd/yyyy");
                txtAPP_EFFECTIVE_DATE.Text = ConvertToDateCulture(DateTime.Now);//.ToString("MM/dd/yyyy");

              
                string strSystemID = GetSystemId();
                string strCarrierSystemID = CarrierSystemID;//System.Configuration.ConfigurationManager.AppSettings.Get("CarrierSystemID").ToString();
                if (strSystemID.Trim().ToUpper() != strCarrierSystemID.Trim().ToUpper())
                {
                    hidIs_Agency.Value = "1";
                }//END OF SYSTEM ID CHECK 

                FillControls();
                FillAgency("");

                SetCaptions();
                SetErrorMessages();

                // Added by Agniswar for Screen Customization on 5 Oct 2011

                string strSysID = GetSystemId();
                if (strSysID == "ALBAUAT")
                    strSysID = "ALBA";

                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "/Policies/support/PageXML/" + strSysID, "PolicyGeneralInfo.xml"))
                    setPageControls(Page, Request.PhysicalApplicationPath + "/Policies/support/PageXml/" + strSysID + "/PolicyGeneralInfo.xml");

                
                //userID = GetSystemId(); //Commented by Lalit March 28 ,2011. userID can not be system id
                hidUSER_SUPERVISOR.Value = getIsUserSuperVisor();

                hidLOBXML.Value = ClsCommon.GetXmlForLobWithoutState();
                trFORMMESSAGE.Visible = false;
                trDETAILS.Visible = false;
                // Fill page for Add and Edit mode			
                bool showDetails = true;
                if (Request.QueryString["APP_ID"] != null && Request.QueryString["APP_ID"].ToString() != "" && Request.QueryString["POLICY_ID"].ToString().Trim() != "") //Edit Mode
                {
                    #region Fill hidden variables : Cust, App, AppVer, FormSaved,Old Data, Called From


                    hidCustomerID.Value = Request.QueryString["CUSTOMER_ID"].Trim().ToString();
                    hidAppID.Value = Request.QueryString["APP_ID"].Trim().ToString();
                    hidAppVersionID.Value = Request.QueryString["APP_VERSION_ID"].Trim().ToString();
                    hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].Trim().ToString();
                    hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].Trim().ToString();

                    hidFormSaved.Value = "0";
                    dsPolicy = new DataSet();
                    objGeneralInformation = new ClsGeneralInformation();
                    dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCustomerID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value));
                    if (dsPolicy != null && dsPolicy.Tables.Count > 0 && dsPolicy.Tables[0].Rows.Count > 0)
                        hidPOL_TRAN_TYPE.Value = dsPolicy.Tables[0].Rows[0]["TRANSACTION_TYPE"].ToString();
                    hidOldData.Value = ClsCommon.GetXMLEncoded(dsPolicy.Tables[0]);
                    if (hidLangCulture.Value.Trim() != cmsbase.DEFAULT_LANG_CULTURE)
                    {
                        hidOldData.Value = FormatXMLDateNode(hidOldData.Value, strDateNodes);
                    }

                    hidCalledFrom.Value = Request.QueryString["CALLEDFROM"].ToString();
                    try
                    {
                        if (hidOldData.Value.Trim() != null && hidOldData.Value.Trim() != "" && hidOldData.Value.Trim() != "0")
                        {
                            txtAPP_EXPIRATION_DATE.Text = ClsCommon.FetchValueFromXML("APP_EXPIRATION_DATE", hidOldData.Value).ToString();
                            txtAPP_EFFECTIVE_DATE.Text = ClsCommon.FetchValueFromXML("APP_EFFECTIVE_DATE", hidOldData.Value).ToString();
                            txtAPP_TERMS.Text = Convert.ToString(Convert.ToDateTime(txtAPP_EXPIRATION_DATE.Text) - Convert.ToDateTime(txtAPP_EFFECTIVE_DATE.Text));                            
                              
                        }
                        else
                        {
                            txtAPP_INCEPTION_DATE.Text = ConvertToDateCulture(DateTime.Now);//.ToString("MM/dd/yyyy");
                            txtAPP_EFFECTIVE_DATE.Text = ConvertToDateCulture(DateTime.Now);//.ToString("MM/dd/yyyy");
                        }
                    }
                    catch (Exception ex)
                    { }
                    btnSave.Visible = true;
                    btnConvertAppToPolicy.Visible = true;
                    btnSubmitAnyway.Visible = true;
                    btnReset.Visible = true;
                    // btnCreateNewVersion.Visible = true;
                    btnCreateNewVersion.Visible = false;
                    btnDelete.Visible = true;
                    btnActivateDeactivate.Visible = true;
                    btnVerifyApplication.Visible = true;
                    //btnReject.Visible = true;
                    #endregion

                    string strOldLobString = "";
                    try
                    {
                        strOldLobString = GetLOBString();
                    }
                    catch
                    { }

                    if (hidOldData.Value != null && hidOldData.Value != "")
                    {
                        //Get the agency code and populate the CSR dropdown
                        string strAGENCYID = ClsCommon.FetchValueFromXML("AGENCY_ID", hidOldData.Value);
                        hidAPP_AGENCY_ID.Value = strAGENCYID;
                        string strBillTypeID = ClsCommon.FetchValueFromXML("BILL_TYPE_ID", hidOldData.Value);
                        hidPolicyStatus.Value = ClsCommon.FetchValueFromXML("POL_POLICY_STATUS", hidOldData.Value);
                        hidApplicationStatus.Value = ClsCommon.FetchValueFromXML("APP_STATUS", hidOldData.Value);
                        hidIS_ACTIVE.Value = ClsCommon.FetchValueFromXML("POLICY_IS_ACTIVE", hidOldData.Value);
                        hidPolicyNumber.Value = ClsCommon.FetchValueFromXML("APP_NUMBER", hidOldData.Value);
                        hidPAYOR.Value = ClsCommon.FetchValueFromXML("PAYOR", hidOldData.Value);
                        SetSUB_LOB_ID(ClsCommon.FetchValueFromXML("SUB_LOB_ID", hidOldData.Value));

                        
                        SetPolicyCurrency(ClsCommon.FetchValueFromXML("POLICY_CURRENCY", hidOldData.Value));
                        txtAPP_NUMBER.ReadOnly = true;
                        hidINSTALL_PLAN_ID.Value = ClsCommon.FetchValueFromXML("INSTALL_PLAN_ID", hidOldData.Value);
                        hidIsTerminated.Value = ClsCommon.FetchValueFromXML("IS_TERMINATED", hidOldData.Value);
                        hidBILL_TYPE_ID.Value = ClsCommon.FetchValueFromXML("BILL_TYPE", hidOldData.Value);
                        strBillType = hidBILL_TYPE_ID.Value;

                        hidDIV_ID_DEPT_ID_PC_ID.Value = ClsCommon.FetchValueFromXML("DIV_ID", hidOldData.Value) + "^"
                            + ClsCommon.FetchValueFromXML("DEPT_ID", hidOldData.Value) + "^" + ClsCommon.FetchValueFromXML("PC_ID", hidOldData.Value);
                        //Added by aditya, for itrack - 1284
                        DateTime dt    = ConvertToDate(ClsCommon.FetchValueFromXML("APP_EFFECTIVE_DATE", hidOldData.Value));
                        App_Eff_Date = dt.ToShortDateString();
                        //Added By Lalit, March for renewal process
                        hidPOL_POLICY_STATUS.Value = ClsCommon.FetchValueFromXML("POL_POLICY_STATUS", hidOldData.Value);

                        cmbDIV_ID_DEPT_ID_PC_ID.SelectedValue = hidDIV_ID_DEPT_ID_PC_ID.Value;
                        cmbBILL_TYPE.SelectedValue = hidBILL_TYPE_ID.Value;
                        
                        //cmbDIV_ID_DEPT_ID_PC_ID.Enabled = false;
                        //rfvDIV_ID_DEPT_ID_PC_ID.Enabled = false;
                        //rfvDIV_ID_DEPT_ID_PC_ID.Visible = false;

                        //Added By Lalit Chauhan , Nov 08,2010
                        SetTransaction_Type(ClsCommon.FetchValueFromXML("TRANSACTION_TYPE", hidOldData.Value));
                        //End here Lalit change

                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(hidIS_ACTIVE.Value);

                        if (hidApplicationStatus != null && hidApplicationStatus.Value != "" && hidApplicationStatus.Value != "0")
                        {
                            if (hidApplicationStatus.Value.ToUpper() != "APPLICATION")
                            {
                                btnConvertAppToPolicy.Visible = false;
                                btnSubmitAnyway.Visible = false;
                                btnSave.Visible = false;
                                btnReset.Visible = false;
                                btnDelete.Visible = false;
                                btnCreateNewVersion.Visible = false;
                                btnActivateDeactivate.Visible = false;
                                btnVerifyApplication.Visible = false;

                                btnReject.Visible = false;
                            }
                            if (hidPOL_POLICY_STATUS.Value.Trim().ToUpper() == ClsCommon.POLICY_STATUS_UNDER_RENEW)
                            {
                                btnSave.Visible = true;
                                cmbTRANSACTION_TYPE.Attributes.Add("disabled", "true");
                                //cmbTRANSACTION_TYPE.Enabled = false;
                            }
                            if (hidApplicationStatus.Value.ToUpper() == "APPLICATION")
                            {
                                //btnReject.Visible = true;
                                btnCopy.Visible = false;
                            }
                            else if (hidApplicationStatus.Value.ToUpper() == "REJECT")
                            {
                                btnCopy.Visible = true;
                                btnReset.Visible = false;
                            }
                            //Modified by Lalit Feb 25,2011 
                            //i-track # 862
                            if (hidPolicyStatus.Value.ToUpper() == ClsCommon.POLICY_STATUS_REJECT)
                            {
                                btnCopy.Visible = true;
                            }
                            //else
                            //{
                            //    btnCopy.Visible = false;
                            //}

                        }
                        //if (hidPolicyStatus != null && hidPolicyStatus.Value != "" && hidPolicyStatus.Value != "0")
                        //{
                        //    if (hidPolicyStatus.Value.ToUpper() == "REJECT")
                        //    {
                        //        btnCopy.Visible = true;

                        //    }
                        //}
                        //FillCSRDropDown(int.Parse(strAGENCYID == "" ? "0" : strAGENCYID));

                        // Get the LOBID
                        strLOB_ID = ClsCommon.FetchValueFromXML("POLICY_LOB", hidOldData.Value);
                        hidLOBID.Value = strLOB_ID;
                        cmbAPP_LOB.SelectedValue = hidLOBID.Value;
                        //hidSTATE_ID.Value = FetchValueFromXML("STATE_ID", hidOldData.Value);

                        cmbAPP_LOB_SelectedIndexChanged(null, null);
                        cmbAPP_LOB.Enabled = false;
                        cmbPOLICY_CURRENCY.Enabled = false;
                        //cmbPOLICY_SUBLOB.Enabled = false;//Added by Pradeep Kushwaha on 27-Oct-2010
                        hidPOLICY_TYPE.Value = ClsCommon.FetchValueFromXML("POLICY_TYPE", hidOldData.Value);
                        hidPOLICY_CURRENCY.Value = ClsCommon.FetchValueFromXML("POLICY_CURRENCY", hidOldData.Value);

                        //cmbBILL_TYPE.SelectedIndex = cmbBILL_TYPE.Items.IndexOf(cmbBILL_TYPE.Items.FindByValue(strBillTypeID));





                        //hidFormSaved value can be changed inside aboove function calls, 
                        //Hence we are reseting its value to 0
                        //Because page is not postback hence new record mode
                        hidFormSaved.Value = "0";

                        # region Set LOB String
                        switch (strLOB_ID)
                        {
                            case LOB_HOME:
                                SetLOBString("HOME");
                                break;
                            case LOB_PRIVATE_PASSENGER:
                                SetLOBString("PPA");
                                break;
                            case LOB_MOTORCYCLE:
                                SetLOBString("MOT");
                                break;
                            case LOB_WATERCRAFT:
                                SetLOBString("WAT");
                                break;
                            case LOB_RENTAL_DWELLING:
                                SetLOBString("RENT");
                                break;
                            case LOB_UMBRELLA:
                                SetLOBString("UMB");
                                break;
                            case LOB_GENERAL_LIABILITY:
                                SetLOBString("GEN");
                                break;
                            case LOB_AVIATION:
                                SetLOBString("AVIATION");
                                break;
                            case MOTOR_VEHICLE:
                                SetLOBString("MOT");
                                break;
                            case FIRE:
                                SetLOBString("FIR");
                                break;
                            case MARINE_CARGO:
                                SetLOBString("MCAR");
                                break;
                            #region Commented Lob Codes
                            /*
                            case LOB_ALL_RISKS_AND_NAMED_PERILS:
                                SetLOBString("ARPERIL");                               
                                break;                            
                            case LOB_CIVIL_LIABILITY_TRANSPORTATION:
                                SetLOBString("CVLIABTR");                               
                                break;
                            case LOB_COMPREHENSIVE_COMPANY:
                                SetLOBString("COMPCOMPY");                                
                                break;
                            case LOB_COMPREHENSIVE_CONDOMINIUM:
                                SetLOBString("COMPCONDO");                                
                                break;
                            case LOB_DIVERSIFIED_RISKS:
                                SetLOBString("DRISK");                                
                                break;
                            case LOB_DWELLING:
                                SetLOBString("DWELLING");                               
                                break;
                            case LOB_FACULTATIVE_LIABILITY:
                                SetLOBString("FACLIAB");                                
                                break;
                            case LOB_GENERAL_CIVIL_LIABILITY:
                                SetLOBString("GENCVLLIB");                                
                                break;
                            case LOB_INDIVIDUAL_PERSONAL_ACCIDENT:
                                SetLOBString("INDPA");                                
                                break;
                            case LOB_MARITIME:
                                SetLOBString("MTIME");                                
                                break;
                            case LOB_ROBBERY:
                                SetLOBString("ROBBERY");                                
                                break;   
                             */
                            #endregion
                            default:
                                SetLOBString(ClsGeneralInformation.GetLOBCodeFromID(int.Parse(strLOB_ID)));
                                break;

                        }
                        hidCallefroms.Value = GetLOBString();
                        #endregion

                        if (hidOldData.Value != "0" && hidOldData.Value != "")
                            FillAgency(hidAPP_AGENCY_ID.Value);

                        hidCustomerType.Value = ClsCommon.FetchValueFromXML("CUSTOMER_TYPE", hidOldData.Value);

                    }//END OF HIDOLDDATA CHECK

                    SetAppID(hidAppID.Value.ToString());
                    SetCalledFor("APPLICATION");
                    SetAppVersionID(hidAppVersionID.Value.ToString());
                    SetPolicyID(hidPOLICY_ID.Value);
                    SetPolicyVersionID(hidPOLICY_VERSION_ID.Value);
                    SetCustomerID(hidCustomerID.Value.ToString());
                    //Added By Lalit setCurrencySymbol();
                    ClsCommon.setPolicyCurrencySymbol(ClsCommon.FetchValueFromXML("CURRENCY_SYMBOL", hidOldData.Value));

                    hidSTATE_ID.Value = ClsCommon.FetchValueFromXML("STATE_ID", hidOldData.Value);
                    hidPOLICY_TYPE.Value = ClsCommon.FetchValueFromXML("POLICY_TYPE", hidOldData.Value);
                    DataSet dsState = new DataSet();
                    objClsStates = new ClsStates();
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


                        if (int.Parse(hidLOBID.Value) == 1 || int.Parse(hidLOBID.Value) == 6) //For Homeowners.
                        {
                            div_poltype.Attributes.Add("style", "display:block");

                        }

                        else
                        {
                            div_poltype.Attributes.Add("style", "display:none");

                        }


                    }

                    if (int.Parse(hidLOBID.Value) <= 8)  // added by sonal to implemet state for old products specially product ID < 8
                    {
                        tblstate.Attributes.Add("style", "display:block");
                    }
                    else
                    {
                        tblstate.Attributes.Add("style", "display:none");
                    }

                    base.ReloadPolicyMenu("");
                    CheckBillingPlan();
                    if (hidOldData.Value != "" && hidOldData.Value != null && hidOldData.Value != "0")
                    {
                        SetPolicyCookies(ClsCommon.FetchValueFromXML("APP_NUMBER", hidOldData.Value), hidCustomerID.Value, hidPolicyID.Value == "" ? GetPolicyID() : hidPOLICY_ID.Value, hidPolicyVersionID.Value == "" ? GetPolicyVersionID() : hidPOLICY_VERSION_ID.Value, hidAppID.Value, hidAppVersionID.Value, hidLOBID.Value == "" ? GetLOBID() : hidLOBID.Value);
                        string PLAN_ID = ClsCommon.FetchValueFromXML("INSTALL_PLAN_ID", hidOldData.Value);
                        string DOWN_PAY_MODE = ClsCommon.FetchValueFromXML("DOWN_PAY_MODE", hidOldData.Value);
                        hidINSTALL_PLAN_ID.Value = PLAN_ID;
                        hidDOWN_PAY_MODE.Value = DOWN_PAY_MODE;
                        DataSet dsInstall = new DataSet();
                        dsInstall = objGeneralInformation.GetPlanStatus(int.Parse(hidCustomerID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value));
                        if (dsInstall != null && dsInstall.Tables[0].Rows.Count > 0)
                        {
                            if (cmbINSTALL_PLAN_ID.SelectedIndex != -1)
                            {
                                cmbINSTALL_PLAN_ID.Enabled = false;
                                cmbDOWN_PAY_MODE.Enabled = false;
                            }
                            hidInstall.Value = "Y";
                        }

                    }
                }//END OF IF PART OF REQUEST CHECK
                else
                {
                    if(hidOldData.Value != "" && hidOldData.Value != "0")
                    strBillType = ClsCommon.FetchValueFromXML("BILL_TYPE", hidOldData.Value);
                    //Add Mode

                    /* If the control is coming from the customer section then the customer name cannot be chaged.
                     * We will show the label in this case */

                    //policyTR.Visible = false;
                    //if (userID.ToUpper() != CarrierSystemID.ToUpper())
                    //{
                    //    chkCOMPLETE_APP.Checked = true;
                    //}

                    tblstate.Attributes.Add("style", "display:none"); // added by sonal to implemet state for old products
                    if (Request.QueryString["CALLEDFROM"] != null && (Request.QueryString["CALLEDFROM"].ToString().Equals(CALLED_FROM_CLIENT) || Request.QueryString["CALLEDFROM"].ToString().Equals(CALLED_FROM_INNER_CLIENT)))
                    {
                        #region Fill Hidden Values (Cust/App/App Ver/Called From)
                        hidFormSaved.Value = "4";
                        hidCustomerID.Value = GetCustomerID();
                        hidAppID.Value = GetAppID();
                        hidAppVersionID.Value = GetAppVersionID();
                        hidCalledFrom.Value = Request.QueryString["CALLEDFROM"].ToString();

                        
                        #endregion

                        #region Check Customer Status / Fill CSR, Producer
                        /*	CHECK THE STATUS OF THE CUSTOMER. IF THE CUSTOMER IS INACTIVE THEN SHOW MESSAGE AND EXIT ELSE
						 * 	GET THE CUSTOMER NAME AND STATE ID */
                        string stateID = "";

                        DataSet dsCustomer = ClsCustomer.GetCustomerDetails(int.Parse(hidCustomerID.Value.ToString()));
                        if (dsCustomer != null && dsCustomer.Tables[0].Rows.Count > 0)
                        {
                            if (dsCustomer.Tables[0].Rows[0]["IS_CUSTOMER_ACTIVE"].ToString().Trim() == "" || dsCustomer.Tables[0].Rows[0]["IS_CUSTOMER_ACTIVE"].ToString().Trim() == "Y")
                            {
                                txtCUSTOMERNAME.Text = (dsCustomer.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"] == null ? "" : dsCustomer.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"]) +
                                    " " +
                                    (dsCustomer.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"] == null ? "" : dsCustomer.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"]) +
                                    " " +
                                    (dsCustomer.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"] == null ? "" : dsCustomer.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"]);

                                txtBILLTO.Text = txtCUSTOMERNAME.Text;

                                stateID = dsCustomer.Tables[0].Rows[0]["CUSTOMER_STATE"] == null ? "" : dsCustomer.Tables[0].Rows[0]["CUSTOMER_STATE"].ToString();
                                strAgency_ID = dsCustomer.Tables[0].Rows[0]["AGENCY_ID"] == null ? "" : dsCustomer.Tables[0].Rows[0]["AGENCY_ID"].ToString();
                                hidCustomerType.Value = dsCustomer.Tables[0].Rows[0]["CUSTOMER_TYPE"].ToString().Trim();

                                ListItem li = cmbCO_INSURANCE.Items.FindByValue("14547"); //Direct
                                if (li != null)
                                {
                                    li.Selected = true;
                                }

                                //    FillCSRDropDown(int.Parse(strAgency_ID));
                                //    FillProducerDropDown(int.Parse(strAgency_ID));
                              
                                
                            }
                            else
                            {
                                lblFormLoadMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("339");
                                showDetails = false;
                            }
                        }
                        #endregion

                        //cmbSTATE_ID.SelectedIndex = cmbSTATE_ID.Items.IndexOf(cmbSTATE_ID.Items.FindByValue(stateID));
                        //cmbSTATE_ID_SelectedIndexChanged(null, null);
                        //cmbPOLICY_TYPE.SelectedIndex = cmbPOLICY_TYPE.Items.IndexOf(cmbPOLICY_TYPE.Items.FindByValue(hidPOLICY_TYPE.Value));                                          

                    }//END OF IF PART OF CALLED FROM CHECK
                    else
                    {
                        hidCustomerID.Value = "NEW";
                        txtCUSTOMERNAME.Text = "";
                    }//END OF ELSE PART OF CALLED FROM CHECK
                    //Added By Lalit May 19,2011.clear session value add new application case
                    #region Clear session Value when new application added
                    hidAppVersionID.Value = "1";
                    hidAppID.Value = "NEW";
                    hidPOLICY_VERSION_ID.Value = "1";
                    hidPOLICY_ID.Value = "0";
                    SetPolicyID(hidPOLICY_ID.Value);
                    SetPolicyVersionID(hidPOLICY_VERSION_ID.Value);
                    SetAppID(hidAppID.Value.ToString());
                    SetAppVersionID(hidAppVersionID.Value.ToString());
                    if (Request.QueryString["APP_ID"].ToString().Trim() != "")
                    {
                        base.ReloadPolicyMenu("");
                    }
                    SetPolicyCurrency("0");
                    #endregion

                    if (txtAPP_NUMBER.Text.Trim() == "")
                    {
                        txtAPP_NUMBER.Text = TEMP_APP_NUMBER;
                    }

                    // Added by RC on 20-Dec-2011 for TFS# 1214
                    if (GetSystemId().ToUpper() != "S001" && GetSystemId().ToUpper() != "SUAT")
                    {                        
                        txtAPP_EXPIRATION_DATE.ReadOnly = true;
                    }

                    txtAPP_EXPIRATION_DATE.Text = ConvertToDateCulture(DateTime.Now);//.ToString("MM/dd/yyyy");
                    txtPOLICY_STATUS.Text = DefaultAppStatus;
                    txtPOLICY_DISP_VERSION.Text = "1.0";

                }//END OF ELSE PART OF REQUEST CHECK

                if (showDetails)
                {
                    trDETAILS.Visible = true;
                    trFORMMESSAGE.Visible = false;
                }//END OF IF PART OF SHOWDETAILS CHECK
                else
                {
                    trDETAILS.Visible = false;
                    trFORMMESSAGE.Visible = true;
                    lblFormLoadMessage.Visible = true;
                }//END OF ELSE PART OF SHOWDETAILS CHECK


                //DataSet dsInstall = new DataSet();
                //dsInstall = objGeneralInformation.GetPlanStatus(int.Parse(hidCustomerID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value));


                //if (dsInstall != null && dsInstall.Tables[0].Rows.Count > 0)
                //{
                //    cmbINSTALL_PLAN_ID.SelectedValue = dsInstall.Tables[0].Rows[0]["PLAN_ID"].ToString();
                //    if (cmbINSTALL_PLAN_ID.SelectedIndex != -1)
                //    {

                //        cmbINSTALL_PLAN_ID.Enabled = false;
                //        cmbBILL_TYPE.Enabled = false;
                //    }
                //    hidInstall.Value = "Y";
                //}

                //SetMultiPolicy();
                //SetCarrierInsureBillAtPageLoad();

                ShowMessages();
                #endregion

                //Added By Lalit Dec 15,2010

                hidPOL_BILL_PLANMSG.Value = ClsMessages.FetchGeneralMessage("1301");
             
            }//END OF IF PART OF POSTBACK CHECK				
            else
            {

                if (hidOldData.Value != "" && hidOldData.Value != "0")
                    strBillType = ClsCommon.FetchValueFromXML("BILL_TYPE", hidOldData.Value);
                //appTerms = cmbAPP_TERMS.SelectedItem == null ? "" : cmbAPP_TERMS.SelectedItem.Value;//Commented by Pradeep Kushwaha(20-oct-2010) for implementation of APP_TERMS on Textbox

                appTerms = txtAPP_TERMS == null ? "" : txtAPP_TERMS.Text.Trim();//Added by Pradeep Kushwaha on 20-Oct-2010
                exp_Date = txtAPP_EXPIRATION_DATE.Text.Trim();


                //FillCSRDropDown(int.Parse(hidAPP_AGENCY_ID.Value == "" ? "0" : hidAPP_AGENCY_ID.Value));
                //FillProducerDropDown(int.Parse(hidAPP_AGENCY_ID.Value == "" ? "0" : hidAPP_AGENCY_ID.Value));

            }//END OF ELSE PART OF POSTBACK CHECK				

            #region Show/Hide buttons on value of 'ShowQuote'
            /* this will be visible if the application is  verified at least once.
						* SHOW_QUOTE column in APP_LIST */
            string showQuote = "";
            if (hidOldData.Value.Trim() != "" && hidOldData.Value.Trim() != "0")
            {
                showQuote = ClsCommon.FetchValueFromXML("SHOW_QUOTE", hidOldData.Value);
            }
            //string strSysID = GetSystemId();
            #endregion

            #region Setting screen id
            switch (hidLOBID.Value)
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
                case "9"://All Risks and Named Perils
                    base.ScreenId = "224_14";
                    break;
                case "10"://Comprehensive Condominium
                    base.ScreenId = "224_15";
                    break;
                case "11"://Comprehensive Company
                    base.ScreenId = "224_16";
                    break;
                case "12"://General Civil Liability
                    base.ScreenId = "224_17";
                    break;
                case "13"://Maritime
                    base.ScreenId = "224_18";
                    break;
                case "14"://Diversified Risks
                    base.ScreenId = "224_19";
                    break;
                case "15"://Individual Personal Accident
                    base.ScreenId = "224_20";
                    break;
                case "16"://Robbery
                    base.ScreenId = "224_21";
                    break;
                case "17"://Facultative Liability
                    base.ScreenId = "224_22";
                    break;
                case "18"://Civil Liability Transportation
                    base.ScreenId = "224_23";
                    break;
                case "19"://Dwelling
                    base.ScreenId = "224_24";
                    break;
                case "20"://National Cargo Transport
                    base.ScreenId = "224_27";
                    break;
                //Added By Pradeep Kushwaha on 28-April-2010
                case "21"://Group Passenger Personal Accident 
                    base.ScreenId = "224_35";
                    break;
                case "22"://Passenger Personal Accident 
                    base.ScreenId = "224_36";
                    break;
                case "23"://International Cargo Transport 
                    base.ScreenId = "224_37";
                    break;
                //End Added 
                case "35"://Pehor Rural Product
                    base.ScreenId = "224_49";
                    break;
                //End Added 
                case "36"://Dpvat2(DPVAT(Cat. 1,2,9 e 10))
                    base.ScreenId = "224_50";
                    break;
                //End Added 
                case "37"://Dpvat2(DPVAT(Cat. 1,2,9 e 10))
                    base.ScreenId = "224_51";
                    break;
                case "38": // Motor Singapore
                    base.ScreenId = "224_2";
                    break;
                case "39": // Fire Singapore
                    base.ScreenId = "224_2";
                    break;
                case "40": // Marine Cargo Singapore
                    base.ScreenId = "224_2";
                    break;
                //End Added 
                case "":
                case "0":
                    // Application is added
                    cmsbase cb;
                    policiesbase ab;
                    ab = (policiesbase)this;
                    cb = (cmsbase)ab;

                    cb.ScreenId = "201_0";
                    break;
                default: //
                    ((cmsbase)this).ScreenId = "";
                    break;
            }
            myWorkFlow.IsTop = false;
            myWorkFlow.Display = false;
            #endregion
            capPREFERENCE_DAY.Visible = false;
            txtPREFERENCE_DAY.Visible = false;
            SetButtonsSecurityXml();
            // itrack no 1161 by praveer panghal
            if (hidIS_ACTIVE.Value.ToUpper() != "Y" && hidIS_ACTIVE.Value.ToUpper() != "0")
            {
                btnDelete.Visible = false;
                btnCreateNewVersion.Visible = false;
                btnConvertAppToPolicy.Visible = false;
                btnSubmitAnyway.Visible = false;
                btnVerifyApplication.Visible = false;
                btnReject.Visible = false;
                btnSave.PermissionString = "<Security><Read>Y</Read><Write>Y</Write><Delete>N</Delete><Execute>N</Execute></Security>";
                btnActivateDeactivate.PermissionString = "<Security><Read>Y</Read><Write>Y</Write><Delete>N</Delete><Execute>N</Execute></Security>";
            }

            //if (Request.Form["__EVENTTARGET"] == "cmbAPP_TERMS_Change")
            //{
            //    cmbAPP_TERMS_SelectedIndexChanged(null, null);
            //}
           // string JavascriptText = "window.open('/cms/application/Aspx/Quote/Quote.aspx?CALLEDFROM=QUOTE_POL&CUSTOMER_ID=" + hidCustomerID.Value + "&POLICY_ID=" + hidPOLICY_ID.Value + "&POLICY_VERSION_ID=" + hidPOLICY_VERSION_ID.Value + "&LOBID=" + hidLOBID.Value + "&QUOTE_ID=" + "0" + "&ONLYEFFECTIVE=" + "false" + "&SHOW=" + "0" + "','Quote','resizable=yes,scrollbars=yes,left=150,top=50,width=700,height=600');";
           // btnConvertAppToPolicy.Attributes.Add("onClick", JavascriptText + "return false;");


         
        }
        private void FillAgency(string AgencyID)
        {
            cmbAGENCY_ID.Items.Clear();
            ClsAgency objAgency = new ClsAgency();
            DataSet dsTemp = new DataSet();
            int Broker = (int)enumAgencyType.BROKER_AGENCY;
            dsTemp = objAgency.FillAgency();
            DateTime dtAPP_EFFECTIVE_DATE = new DateTime();
            if(App_Eff_Date != "" && App_Eff_Date !=null)
                dtAPP_EFFECTIVE_DATE = Convert.ToDateTime(App_Eff_Date);
            else
                dtAPP_EFFECTIVE_DATE = Convert.ToDateTime(txtAPP_EFFECTIVE_DATE.Text);

            string IS_TERMINATED = "N";
            if (AgencyID == "")
                AgencyID = "0";
            if (dsTemp != null && dsTemp.Tables.Count > 0)
            {

                if (dsTemp.Tables[0].Select("AGENCY_ID = " + AgencyID + " and AGENCY_TYPE =" + Broker).Length > 0)
                {
                    DateTime AgencyDateTime = Convert.ToDateTime(dsTemp.Tables[0].Select("AGENCY_ID = " + AgencyID + " and AGENCY_TYPE =" + Broker).CopyToDataTable().Rows[0]["TERMINATION_DATE"]);
                    if (AgencyDateTime < dtAPP_EFFECTIVE_DATE)
                        IS_TERMINATED = "Y";
                }

                if ((GetSystemId().ToString().ToUpper() != "S001") && (GetSystemId().ToString().ToUpper() != "SUAT"))
                {
                    cmbAGENCY_ID.DataSource = dsTemp.Tables[0].Select("(TERMINATION_DATE >= '" + dtAPP_EFFECTIVE_DATE + "' or AGENCY_ID = " + AgencyID + "  ) and AGENCY_TYPE =" + Broker, "AGENCY_NAME_ACTIVE_STATUS ASC").CopyToDataTable();
                }
                else if ((GetSystemId().ToString().ToUpper() == "S001") || (GetSystemId().ToString().ToUpper() == "SUAT"))
                {
                    cmbAGENCY_ID.DataSource = dsTemp.Tables[0];
                }
                
                
                cmbAGENCY_ID.DataTextField = "AGENCY_NAME_ACTIVE_STATUS";
                cmbAGENCY_ID.DataValueField = "AGENCY_ID";
                cmbAGENCY_ID.DataBind();
                cmbAGENCY_ID.Items.Insert(0, new ListItem("", ""));
                // setAgencyColor(dsTemp.Tables[0]);
                if (IS_TERMINATED == "Y")
                {
                    cmbAGENCY_ID.Items.FindByValue(AgencyID).Attributes.Add("style", "color:red");
                }

                dsTemp.Dispose();
            }

        }

        #endregion

        //Added by aditya, for itrack - 1284

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet AjaxFillAgency(string date)
        {
            try
            {
                CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();
                
                int Broker = (int)enumAgencyType.BROKER_AGENCY;
                int LangId = int.Parse(GetLanguageID());
                DataSet result = obj.FillAgency(Broker,date, LangId);
                
                return result;
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return null;
            }
        }



        #region last 3 cookies
        private void SetPolicyCookies(string polNo, string custID, string polId, string polVersionID, string aAppID, string aAppVersionID, string aLOBID)
        {

            string AgencyId = GetSystemId();
            if (AgencyId.ToUpper() != CarrierSystemID)
            {
                Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastPageVisited = new ClsGeneralInformation();
                DataSet ds = objLastPageVisited.GetLastVisitedPageEntry(int.Parse(GetUserId()), AgencyId);

                if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows[0]["LAST_VISITED_POLICY"].ToString() != "")
                {
                    string prevCook = ds.Tables[0].Rows[0]["LAST_VISITED_POLICY"].ToString();
                    string[] cookArr = prevCook.Split('@');
                    if (cookArr.Length > 0 && cookArr.Length < 4)
                    {
                        string Pol_Details = polNo + "~" + custID + "~" + polId + "~" + polVersionID + "~" + aAppID + "~" + aAppVersionID + "~" + aLOBID + "~" + DateTime.Today.Date + "@" + ds.Tables[0].Rows[0]["LAST_VISITED_POLICY"].ToString();
                        Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedPolicy = new ClsGeneralInformation();
                        objLastVisitedPolicy.UpdateLastVisitedPageEntry("APPLICATION", Pol_Details, int.Parse(GetUserId()), AgencyId);
                    }
                    else if (cookArr.Length >= 4)
                    {
                        int maxindex = cookArr.Length - 1;
                        if (maxindex >= 3)
                            maxindex = 2;

                        string Pol_Details = polNo + "~" + custID + "~" + polId + "~" + polVersionID + "~" + aAppID + "~" + aAppVersionID + "~" + aLOBID + "~" + DateTime.Today.Date + "@";
                        Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedPolicy = new ClsGeneralInformation();
                        objLastVisitedPolicy.UpdateLastVisitedPageEntry("APPLICATION", Pol_Details, int.Parse(GetUserId()), AgencyId);

                        for (int cookindex = 0; cookindex < maxindex; cookindex++)
                        {
                            Pol_Details += cookArr[cookindex] + "@";
                        }
                        objLastVisitedPolicy.UpdateLastVisitedPageEntry("APPLICATION", Pol_Details, int.Parse(GetUserId()), AgencyId);
                    }
                    else
                    {
                        string Pol_Details = polNo + "~" + custID + "~" + polId + "~" + polVersionID + "~" + aAppID + "~" + aAppVersionID + "~" + aLOBID + "~" + DateTime.Today.Date + "@";
                        Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedPolicy = new ClsGeneralInformation();
                        objLastVisitedPolicy.UpdateLastVisitedPageEntry("APPLICATION", Pol_Details, int.Parse(GetUserId()), AgencyId);
                    }
                }
                else
                {
                    string Policy_Details = polNo + "~" + custID + "~" + polId + "~" + polVersionID + "~" + aAppID + "~" + aAppVersionID + "~" + aLOBID + "~" + DateTime.Today.Date + "@";
                    Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedPolicy = new ClsGeneralInformation();
                    objLastVisitedPolicy.UpdateLastVisitedPageEntry("APPLICATION", Policy_Details, int.Parse(GetUserId()), AgencyId);
                }
            }

            else
            {
                string Policy_Details = polNo + "~" + custID + "~" + polId + "~" + polVersionID + "~" + aAppID + "~" + aAppVersionID + "~" + aLOBID + "~" + DateTime.Today.Date;
                Cms.BusinessLayer.BlApplication.ClsGeneralInformation objLastVisitedPolicy = new ClsGeneralInformation();
                objLastVisitedPolicy.UpdateLastVisitedPageEntry("APPLICATION", Policy_Details, int.Parse(GetUserId()), AgencyId);
            }

        }
        #endregion

        /// <summary>
        /// Show Save Message
        /// </summary>
        /// Function added by Charles on 4-Mar-2010 for Policy Page Implementation
        private void ShowMessages()
        {
            if (Session["LoadedAfterSave"] != null)
            {
                if (Session["LoadedAfterSave"].ToString().ToUpper().Trim() == "TRUE")
                {
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                    lblMessage.Visible = true;
                    Session["LoadedAfterSave"] = null;
                }
                else if (Session["LoadedAfterSave"].ToString().ToUpper().Trim() == "COPY")
                {


                    //string message = @"Policy has been copied successfully "+ "</br>" + "New Application Number :- " + txtAPP_NUMBER.Text;  
                    //ClientScript.RegisterStartupScript(typeof(Page), "PolicyCopy", "<script language=JavaScript>alert('" + (message) + "');</script>"); 

                    lblMessage.Text = ClsMessages.FetchGeneralMessage("1105");

                    lblMessage.Visible = true;
                    Session["LoadedAfterSave"] = null;
                }
            }
        }

        #region Utility Functions ( Button Security / Show Messages / Application Cookies / GetFormValue) etc..

        private void SetButtonsSecurityXml()
        {
            try
            {
                btnReset.CmsButtonClass = CmsButtonType.Write;
                btnReset.PermissionString = gstrSecurityXML;

                btnBack.CmsButtonClass = CmsButtonType.Read;
                btnBack.PermissionString = gstrSecurityXML;

                btnCustomerAssistant.CmsButtonClass = CmsButtonType.Read;
                btnCustomerAssistant.PermissionString = gstrSecurityXML;

                btnSave.CmsButtonClass = CmsButtonType.Write;
                btnSave.PermissionString = gstrSecurityXML;

                btnConvertAppToPolicy.CmsButtonClass = CmsButtonType.Write;
                btnConvertAppToPolicy.PermissionString = gstrSecurityXML;

                btnCreateNewVersion.CmsButtonClass = CmsButtonType.Write;
                btnCreateNewVersion.PermissionString = gstrSecurityXML;

                btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
                btnActivateDeactivate.PermissionString = gstrSecurityXML;

                btnDelete.CmsButtonClass = CmsButtonType.Delete;
                btnDelete.PermissionString = gstrSecurityXML;

                btnVerifyApplication.CmsButtonClass = CmsButtonType.Write;
                btnVerifyApplication.PermissionString = gstrSecurityXML;

                btnSubmitAnyway.CmsButtonClass = CmsButtonType.Write;
                btnSubmitAnyway.PermissionString = gstrSecurityXML;


                btnReject.CmsButtonClass = CmsButtonType.Write;
                btnReject.PermissionString = gstrSecurityXML;

                btnCopy.CmsButtonClass = CmsButtonType.Read;
                btnCopy.PermissionString = gstrSecurityXML;

                //btnCopy.CmsButtonClass = CmsButtonType.Write;
                //btnCopy.PermissionString = gstrSecurityXML;


            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
        {
            try
            {
                ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
                ClsPolicyInfo objGeneralInfo = new ClsPolicyInfo();
                objGeneralInfo.CUSTOMER_ID = int.Parse(hidCustomerID.Value);
                objGeneralInfo.APP_ID = int.Parse(hidAppID.Value);
                objGeneralInfo.APP_VERSION_ID = int.Parse(hidAppVersionID.Value);
                objGeneralInfo.POLICY_ID = int.Parse(hidPOLICY_ID.Value);
                objGeneralInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
                objGeneralInfo.CREATED_BY = objGeneralInfo.MODIFIED_BY = int.Parse(GetUserId());
                hidIS_ACTIVE.Value = ClsCommon.FetchValueFromXML("POLICY_IS_ACTIVE", hidOldData.Value);
                if (hidIS_ACTIVE.Value.ToUpper().Equals("Y"))
                {
                    objGeneralInformation.ActivateDeactivatePolicy(objGeneralInfo, "N");
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("41");
                    hidIS_ACTIVE.Value = "N";
                    btnReject.Visible = false;
                    btnCopy.Visible = false;
                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(hidIS_ACTIVE.Value);

                    btnDelete.Visible = false;
                    btnCreateNewVersion.Visible = false;
                    btnConvertAppToPolicy.Visible = false;
                    btnSubmitAnyway.Visible = false;
                    btnSave.PermissionString = "<Security><Read>Y</Read><Write>Y</Write><Delete>N</Delete><Execute>N</Execute></Security>";
                    btnActivateDeactivate.PermissionString = "<Security><Read>Y</Read><Write>Y</Write><Delete>N</Delete><Execute>N</Execute></Security>";
                }
                else
                {
                    objGeneralInformation.ActivateDeactivatePolicy(objGeneralInfo, "Y");
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("40");
                    hidIS_ACTIVE.Value = "Y";
                    //btnReject.Visible = true;//Commented for itrack- 1496 

                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(hidIS_ACTIVE.Value);

                    btnDelete.Visible = true;
                    // btnCreateNewVersion.Visible = true;
                    btnCreateNewVersion.Visible = false;
                    btnConvertAppToPolicy.Visible = true;
                    //btnSubmitAnyway.Visible = true;//Commented for itrack- 1496 
                }

                lblMessage.Visible = true;

                dsPolicy = new DataSet();
                dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCustomerID.Value == "" ? GetCustomerID() : hidCustomerID.Value), int.Parse(hidPolicyID.Value == "" ? GetPolicyID() : hidPolicyID.Value), int.Parse(hidPolicyVersionID.Value == "" ? GetPolicyVersionID() : hidPolicyVersionID.Value));
                hidOldData.Value = ClsCommon.GetXMLEncoded(dsPolicy.Tables[0]);
                if (hidLangCulture.Value.Trim() != cmsbase.DEFAULT_LANG_CULTURE)
                {
                    hidOldData.Value = FormatXMLDateNode(hidOldData.Value, strDateNodes);
                }

                hidFormSaved.Value = "1";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.FetchGeneralMessage("21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
                lblMessage.Visible = true;
            }
        }
        private void btnCopy_Click(object sender, System.EventArgs e)
        {

            //moified by Lalit feb 25,2011
            //i-track # -862 
            //Copy rejected Policy as per new implimentation of genration new Application No logic
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
                objGeneralInfo.POLICY_ID = int.Parse(hidPOLICY_ID.Value);
                objGeneralInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
                objGeneralInfo.CREATED_BY = int.Parse(CreatedBy == "" ? "0" : CreatedBy);

                int RetVal = objGenInfo.CopyAppPolFromReject(objGeneralInfo, NEW_POL_NUMBER, "APP");

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

            /*
            try
            {
                string CreatedBy = GetUserId();
                string strLOB = "";
                ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
                ClsPolicyInfo objGeneralInfo = new ClsPolicyInfo();

                objGeneralInfo.APP_TERMS = appTerms;
                if (objGeneralInfo.POLICY_LOB == "0")
                    objGeneralInfo.POLICY_SUBLOB = null;
                objGenInfo.TransactionRequired = true;
                if (cmbAPP_LOB.SelectedIndex > 0)
                    strLOB = cmbAPP_LOB.SelectedItem.Text;

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

              //  String NEW_APP_NUMBER = ClsGeneralInformation.GenerateAppPolNumber(int.Parse(cmbAPP_LOB.SelectedItem.Value), objGeneralInfo.DIV_ID, objGeneralInfo.APP_EFFECTIVE_DATE, "APP");
                String NEW_APP_NUMBER = ClsGeneralInformation.GenerateApplicationNumber(int.Parse(cmbAPP_LOB.SelectedItem.Value), int.Parse(hidAPP_AGENCY_ID.Value == "" ? "0" : hidAPP_AGENCY_ID.Value));
                objGeneralInfo.CUSTOMER_ID = int.Parse(hidCustomerID.Value);
                objGeneralInfo.POLICY_ID = int.Parse(hidPOLICY_ID.Value);
                objGeneralInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
                objGeneralInfo.CREATED_BY = int.Parse(CreatedBy == "" ? "0" : CreatedBy);

                int RetVal = objGenInfo.CopyAppPolFromReject(objGeneralInfo, NEW_APP_NUMBER, "APP");

                if (RetVal > 0)
                {
                    hidFormSaved.Value = "1";
                    SetAppID(objGeneralInfo.APP_ID.ToString());
                    SetAppVersionID(objGeneralInfo.APP_VERSION_ID.ToString());
                    SetPolicyID(objGeneralInfo.POLICY_ID.ToString());
                    SetPolicyVersionID(objGeneralInfo.POLICY_VERSION_ID.ToString());

                    string strCopyReject = @"<script>var con = confirm('" + objResourceMgr.GetString("lblRejCopy") + NEW_APP_NUMBER +"');" +
                    " if (con) { parent.location.href = 'PolicyTab.aspx?CUSTOMER_ID=" + objGeneralInfo.CUSTOMER_ID.ToString() + "&APP_ID=" +
                    objGeneralInfo.APP_ID.ToString() + "&APP_VERSION_ID=" + objGeneralInfo.APP_VERSION_ID.ToString() + "&POLICY_ID=" +
                    objGeneralInfo.POLICY_ID.ToString() + "&POLICY_VERSION_ID=" + objGeneralInfo.POLICY_VERSION_ID.ToString() + "' }</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "COPYREJECT", strCopyReject);

                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1104");
                }
                else
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("20");
                    hidFormSaved.Value = "2";
                }
                lblMessage.Visible = true;
                lblMessage.Focus();

                dsPolicy = new DataSet();
                int CustomerID = Convert.ToInt32(objGeneralInfo.CUSTOMER_ID.ToString());
                int PolicyID = Convert.ToInt32(objGeneralInfo.POLICY_ID.ToString());
                int PolVerID = Convert.ToInt32(objGeneralInfo.POLICY_VERSION_ID.ToString());

                dsPolicy = objGenInfo.GetPolicyDataSet(CustomerID, PolicyID, PolVerID);
                hidOldData.Value = ClsCommon.GetXMLEncoded(dsPolicy.Tables[0]);
                if (hidLangCulture.Value.Trim() != cmsbase.DEFAULT_LANG_CULTURE)
                {
                    hidOldData.Value = FormatXMLDateNode(hidOldData.Value, strDateNodes);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }*/
        }
        /// <summary>
        /// Creates New Version of Application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateNewVersion_Click(object sender, System.EventArgs e)
        {


            try
            {
                string CreatedBy = GetUserId();
                string strLOB = "";
                ClsPolicyInfo objGeneralInfo = GetFormValue();

                objGeneralInfo.APP_TERMS = appTerms;
                if (objGeneralInfo.POLICY_LOB == "0")
                    objGeneralInfo.POLICY_SUBLOB = null;

                objGeneralInformation = new ClsGeneralInformation();
                objGeneralInformation.TransactionRequired = true;
                if (cmbAPP_LOB.SelectedIndex > 0)
                    strLOB = cmbAPP_LOB.SelectedItem.Text;

                int new_Version = -1;
                ClsGeneralInformation objGenInfo = new ClsGeneralInformation();

                int RetVal = objGenInfo.CopyApplication(objGeneralInfo, int.Parse(hidCustomerID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(CreatedBy == "" ? "0" : CreatedBy), strLOB, out new_Version);
                if (RetVal > 0)
                {
                    lblMessage.Text = objResourceMgr.GetString("lblCopy1") + ClsCommon.FetchValueFromXML("POLICY_NUMBER", hidOldData.Value) + objResourceMgr.GetString("lblCopy2") + ClsCommon.FetchValueFromXML("POLICY_VERSION_ID", hidOldData.Value) + objResourceMgr.GetString("lblCopy3");
                    hidFormSaved.Value = "1";

                    string strCheckNewVersion = @"<script>var con = confirm('" + objResourceMgr.GetString("lblNewVersion") + "');" +
                    " if (con) { parent.location.href = 'PolicyTab.aspx?CUSTOMER_ID=" + GetCustomerID() + "&APP_ID=" + GetAppID() + "&APP_VERSION_ID=" + GetAppVersionID() + "&POLICY_ID=" + GetPolicyID() + "&POLICY_VERSION_ID=" + (int.Parse(GetPolicyVersionID()) + 1).ToString() + "' }</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "NEWVERSION", strCheckNewVersion);
                }
                else
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("20");
                    hidFormSaved.Value = "2";
                }
                lblMessage.Visible = true;
                lblMessage.Focus();

                dsPolicy = new DataSet();
                dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCustomerID.Value == "" ? GetCustomerID() : hidCustomerID.Value), int.Parse(hidPolicyID.Value == "" ? GetPolicyID() : hidPolicyID.Value), int.Parse(hidPolicyVersionID.Value == "" ? GetPolicyVersionID() : hidPolicyVersionID.Value));
                hidOldData.Value = ClsCommon.GetXMLEncoded(dsPolicy.Tables[0]);
                if (hidLangCulture.Value.Trim() != cmsbase.DEFAULT_LANG_CULTURE)
                {
                    hidOldData.Value = FormatXMLDateNode(hidOldData.Value, strDateNodes);
                }

                strNewVersion = "1";
                strVersionID = new_Version;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        private void SetCaptions()
        {
            DefaultAppStatus = objResourceMgr.GetString("DefaultAppStatus");
            TEMP_APP_NUMBER = objResourceMgr.GetString("TEMP_APP_NUMBER");
            hidUnderwriter.Value = objResourceMgr.GetString("lblUNDERWRITER");
            gIntShowQuote4 = objResourceMgr.GetString("gIntShowQuote4");
            hidRulesMessage.Value = objResourceMgr.GetString("lblRulesMessage");
            lblHeader.Text = objResourceMgr.GetString("lblHeader");

            capCUSTOMER_ID.Text = objResourceMgr.GetString("txtCUSTOMER_ID");
            capAPP_STATUS.Text = objResourceMgr.GetString("txtPOLICY_STATUS");
            capAPP_NUMBER.Text = objResourceMgr.GetString("txtAPP_NUMBER");
            capAPP_VERSION.Text = objResourceMgr.GetString("txtPOLICY_DISP_VERSION");
            capAPP_TERMS.Text = objResourceMgr.GetString("cmbAPP_TERMS");
            capAPP_INCEPTION_DATE.Text = objResourceMgr.GetString("txtAPP_INCEPTION_DATE");
            capAPP_EFFECTIVE_DATE.Text = objResourceMgr.GetString("txtAPP_EFFECTIVE_DATE");
            capAPP_EXPIRATION_DATE.Text = objResourceMgr.GetString("txtAPP_EXPIRATION_DATE");
            capAPP_LOB.Text = objResourceMgr.GetString("cmbAPP_LOB");
            capPOLICY_SUBLOB.Text = objResourceMgr.GetString("cmbPOLICY_SUBLOB");
            capAGENCY_ID.Text = objResourceMgr.GetString("cmbAPP_AGENCY_ID");
            capCSR.Text = objResourceMgr.GetString("cmbCSR");
            capUNDERWRITER.Text = objResourceMgr.GetString("cmbUNDERWRITER");
            capPOLICY_CURRENCY.Text = objResourceMgr.GetString("cmbPOLICY_CURRENCY");
            capACTIVITY.Text = objResourceMgr.GetString("cmbACTIVITY");
            capMODALITY.Text = objResourceMgr.GetString("cmbMODALITY");
            capPRODUCER.Text = objResourceMgr.GetString("cmbPRODUCER");
            capPOLICY_CURRENCY.Text = objResourceMgr.GetString("cmbPOLICY_CURRENCY");

            capTRANSACTION_TYPE.Text = objResourceMgr.GetString("cmbTRANSACTION_TYPE");
            capBROKER_REQUEST_NO.Text = objResourceMgr.GetString("txtBROKER_REQUEST_NO");
            capREMARKS.Text = objResourceMgr.GetString("txtPOLICY_DESCRIPTION");
            capBROKER_COMM_FIRST_INSTM.Text = objResourceMgr.GetString("chkBROKER_COMM_FIRST_INSTM");
            capPOLICY_LEVEL_COMM_APPLIES.Text = objResourceMgr.GetString("chkPOLICY_LEVEL_COMM_APPLIES");
            capPOLICY_LEVEL_COMISSION.Text = objResourceMgr.GetString("txtPOLICY_LEVEL_COMISSION");

            //billing information	
            capINSTALL_PLAN_ID.Text = objResourceMgr.GetString("cmbINSTALL_PLAN_ID");
            capBILL_TYPE_ID.Text = objResourceMgr.GetString("cmbBILL_TYPE");
            capDOWN_PAY_MODE.Text = objResourceMgr.GetString("txtDOWN_PAY_MODE");
            capBILLTO.Text = objResourceMgr.GetString("txtBILLTO");
            capCO_INSURANCE.Text = objResourceMgr.GetString("cmbCO_INSURANCE");
            capDIV_ID_DEPT_ID_PC_ID.Text = objResourceMgr.GetString("cmbDIV_ID_DEPT_ID_PC_ID");
            //capPREFERENCE_DAY.Text = objResourceMgr.GetString("txtPREFERENCE_DAY");
            capPAYOR.Text = objResourceMgr.GetString("cmbPAYOR");

            //Other Captions
            lblManHeader.Text = objResourceMgr.GetString("lblManHeader");
            lblAppHeader.Text = objResourceMgr.GetString("lblAppHeader");
            lblTermHeader.Text = objResourceMgr.GetString("lblTermHeader");
            lblAgencyHeader.Text = objResourceMgr.GetString("lblAgencyHeader");
            lblRemarksHeader.Text = objResourceMgr.GetString("lblRemarksHeader");
            lblBillingHeader.Text = objResourceMgr.GetString("lblBillingHeader");
            lblProduct.Text = objResourceMgr.GetString("lblProduct");
            lblDelete.Value = objResourceMgr.GetString("lblDelete");

            //Buttons
            btnCreateNewVersion.Text = ClsMessages.FetchGeneralButtonsText("btnCreateNewVersion");
            btnVerifyApplication.Text = ClsMessages.FetchGeneralButtonsText("btnVerifyApplication");
            btnReject.Text = ClsMessages.FetchGeneralButtonsText("btnReject");
            btnCopy.Text = ClsMessages.FetchGeneralButtonsText("btnCopy");
            capState.Text = objResourceMgr.GetString("cmbState_ID");
            capOLD_POLICY_NUMBER.Text = objResourceMgr.GetString("txtOLD_POLICY_NUMBER");
            #region Commented Captions
            //capCONTACT_PERSON.Text = objResourceMgr.GetString("cmbCONTACT_PERSON"); 
            //capSTATE_ID.Text = objResourceMgr.GetString("cmbSTATE_ID");
            //capPROPRTY_INSP_CREDIT.Text = objResourceMgr.GetString("cmbPROPRTY_INSP_CREDIT");
            //capCHARGE_OFF_PRMIUM.Text = objResourceMgr.GetString("cmbCHARGE_OFF_PRMIUM");
            //capPROXY_SIGN_OBTAINED.Text = objResourceMgr.GetString("cmbPROXY_SIGN_OBTAINED");
            //capYEAR_AT_CURR_RESI.Text = objResourceMgr.GetString("txtYEAR_AT_CURR_RESI");
            //capYEARS_AT_PREV_ADD.Text = objResourceMgr.GetString("txtYEARS_AT_PREV_ADD");
            //capPIC_OF_LOC.Text = objResourceMgr.GetString("cmbPIC_OF_LOC");
            //lblAllPoliciesHeader.Text = objResourceMgr.GetString("lblAllPoliciesHeader");
            //lblPolicies.Text = objResourceMgr.GetString("lblPolicies");
            //lblPoliciesDiscount.Text = objResourceMgr.GetString("lblPoliciesDiscount");
            //capAPP_POLICY_TYPE.Text = objResourceMgr.GetString("capAPP_POLICY_TYPE");
            //capCOMPLETE_APP.Text = objResourceMgr.GetString("chkCOMPLETE_APP");            
            //capRECEIVED_PRMIUM.Text = objResourceMgr.GetString("txtRECEIVED_PRMIUM");


            rngAPP_INCEPTION_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("1454");
            #endregion
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            try
            {
                string DeletedBy = GetUserId();

                ClsPolicyInfo objGeneralInfo = GetFormValue();

                ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
                objGenInfo.TransactionRequired = true;

                objGeneralInfo.CUSTOM_INFO = objResourceMgr.GetString("lblCustomAppNumber") + objGeneralInfo.APP_NUMBER + objResourceMgr.GetString("lblCustomVersion") + objGeneralInfo.APP_VERSION;

                int RetVal = objGenInfo.DeleteApplication(objGeneralInfo, int.Parse(hidCustomerID.Value), int.Parse(hidAppID.Value), int.Parse(hidAppVersionID.Value), int.Parse(DeletedBy == "" ? "0" : DeletedBy));
                if (RetVal > 0)
                {
                    lblFormLoadMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("406");
                    hidFormSaved.Value = "5";		//If record deleted, fidFormSaved should be 5
                    lblFormLoadMessage.Visible = true;
                    trDETAILS.Visible = false;
                    btnDelete.Visible = false;
                    delStr = "1";
                    trFORMMESSAGE.Visible = true;
                    SetAppID("");
                    SetAppVersionID("");
                    SetPolicyID("");
                    SetPolicyVersionID("");
                    SetLOBID("");
                    ClientScript.RegisterStartupScript(this.GetType(), "LoadPage", "<script>alert('" + lblFormLoadMessage.Text + "');this.parent.document.location.href = '/Cms/Client/Aspx/CustomerManagerIndex.aspx';</script>");

                }
                else
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("407");
                    hidFormSaved.Value = "2";
                    lblMessage.Visible = true;
                    delStr = "0";
                    trDETAILS.Visible = true;
                    btnDelete.Visible = true;
                    trFORMMESSAGE.Visible = false;
                }
            }
            catch (Exception ex)
            { throw (ex); }
        }

        private void FillControls()
        {
            hidFULL_PAY_PLAN_ID.Value = Cms.BusinessLayer.BlCommon.Accounting.ClsInstallmentPlan.FullPayPlanID().ToString();
            //cmbAGENCY_ID.Items.Clear();
            //ClsAgency objAgency = new ClsAgency();
            //DataSet dsTemp = new DataSet();
            //int Broker = (int)enumAgencyType.BROKER_AGENCY;
            //dsTemp = objAgency.FillAgency();
            //if (AgencyID == "")
            //    AgencyID = "0";
            //if (dsTemp != null && dsTemp.Tables.Count > 0)
            //{


            //    DateTime dtAPP_EFFECTIVE_DATE = new DateTime();
            //    dtAPP_EFFECTIVE_DATE = Convert.ToDateTime(txtAPP_EFFECTIVE_DATE.Text);

            //    cmbAGENCY_ID.DataSource = dsTemp.Tables[0].Select("(TERMINATION_DATE > '" + dtAPP_EFFECTIVE_DATE + "' or AGENCY_ID = "+AgencyID+"  ) and AGENCY_TYPE =" + Broker, "AGENCY_NAME_ACTIVE_STATUS ASC").CopyToDataTable();
            //    cmbAGENCY_ID.DataTextField = "AGENCY_NAME_ACTIVE_STATUS";
            //    cmbAGENCY_ID.DataValueField = "AGENCY_ID";
            //    cmbAGENCY_ID.DataBind();
            //    cmbAGENCY_ID.Items.Insert(0, new ListItem("", ""));
            //   // setAgencyColor(dsTemp.Tables[0]);


            //    dsTemp.Dispose();
            //}

            DataTable dtPAYOR = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("PAYRC");
            DataView dvPAYOR = dtPAYOR.DefaultView;
            dvPAYOR.Sort = "LookupDesc";
            cmbPAYOR.DataSource = dvPAYOR;
            cmbPAYOR.DataTextField = "LookupDesc";
            cmbPAYOR.DataValueField = "LookupID";
            cmbPAYOR.DataBind();
            cmbPAYOR.Items.Insert(0, "");


            cmbPOLICY_CURRENCY.DataSource = Cms.CmsWeb.ClsFetcher.Currency;
            cmbPOLICY_CURRENCY.DataTextField = "CURR_DESC";
            cmbPOLICY_CURRENCY.DataValueField = "CURRENCY_ID";
            cmbPOLICY_CURRENCY.DataBind();
            if (cmbPOLICY_CURRENCY.Items.Count == 1)
            {
                cmbPOLICY_CURRENCY.SelectedIndex = 0;
                hidPOLICY_CURRENCY.Value = cmbPOLICY_CURRENCY.SelectedValue;

            }
            else
            {
                cmbPOLICY_CURRENCY.Items.Insert(0, "");
               // cmbPOLICY_CURRENCY.SelectedIndex = 3;
            }

            DataTable dt = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("COINC");
            DataView dv = dt.DefaultView;
            dv.Sort = "LookupDesc";

            cmbCO_INSURANCE.DataSource = dv;
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
            cmbDIV_ID_DEPT_ID_PC_ID.DataSource = dvDIVDEP;
            cmbDIV_ID_DEPT_ID_PC_ID.DataTextField = "TEXT";
            cmbDIV_ID_DEPT_ID_PC_ID.DataValueField = "VALUE";
            cmbDIV_ID_DEPT_ID_PC_ID.DataBind();
            cmbDIV_ID_DEPT_ID_PC_ID.Items.Insert(0, "");





            //cmbContact_Person.DataSource = ClsContactsManager.FetchContactList(GetCustomerID());
            //cmbContact_Person.DataTextField = "CONTACT_NAME";
            //cmbContact_Person.DataValueField = "CONTACT_ID";
            //cmbContact_Person.DataBind();
            //cmbContact_Person.Items.Insert(0, "");

            //cmbPROXY_SIGN_OBTAINED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
            //cmbPROXY_SIGN_OBTAINED.DataTextField = "LookupDesc";
            //cmbPROXY_SIGN_OBTAINED.DataValueField = "LookupID";
            //cmbPROXY_SIGN_OBTAINED.DataBind();
            //cmbPROXY_SIGN_OBTAINED.Items.Insert(0, "");

            /*
            cmbCHARGE_OFF_PRMIUM.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
            cmbCHARGE_OFF_PRMIUM.DataTextField = "LookupDesc";
            cmbCHARGE_OFF_PRMIUM.DataValueField = "LookupID";
            cmbCHARGE_OFF_PRMIUM.DataBind();
            cmbCHARGE_OFF_PRMIUM.Items.Insert(0, "");
            cmbCHARGE_OFF_PRMIUM.SelectedIndex = 1;
             */

            //cmbPIC_OF_LOC.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
            //cmbPIC_OF_LOC.DataTextField = "LookupDesc";
            //cmbPIC_OF_LOC.DataValueField = "LookupID";
            //cmbPIC_OF_LOC.DataBind();
            //cmbPIC_OF_LOC.Items.Insert(0, "");

            //state
            //DataTable dtState1 = Cms.CmsWeb.ClsFetcher.ActiveState;
            //cmbSTATE_ID.DataSource = dtState1;
            //cmbSTATE_ID.DataTextField = "STATE_NAME";
            //cmbSTATE_ID.DataValueField = "STATE_ID";
            //cmbSTATE_ID.DataBind();
            //cmbSTATE_ID.Items.Insert(0, new ListItem("", ""));
            //cmbSTATE_ID.SelectedIndex = 0;

            //LOBs
            DataTable dtLOBs = Cms.CmsWeb.ClsFetcher.LOBs;
            cmbAPP_LOB.DataSource = dtLOBs;
            cmbAPP_LOB.DataTextField = "LOB_DESC";
            cmbAPP_LOB.DataValueField = "LOB_ID";
            cmbAPP_LOB.DataBind();
            cmbAPP_LOB.Items.Insert(0, new ListItem("", ""));

            //cmbPROPRTY_INSP_CREDIT.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
            //cmbPROPRTY_INSP_CREDIT.DataTextField = "LookupDesc";
            //cmbPROPRTY_INSP_CREDIT.DataValueField = "LookupID";
            //cmbPROPRTY_INSP_CREDIT.DataBind();
            //cmbPROPRTY_INSP_CREDIT.Items.Insert(0, "");

            string EffectiveDate;
            if (txtAPP_EFFECTIVE_DATE.Text != "")
            {
                EffectiveDate = txtAPP_EFFECTIVE_DATE.Text;
            }
            else
            {
                EffectiveDate = "01/01/1950";
            }
            //Cms.BusinessLayer.BlCommon.ClsAgency.GetAgencyWithStatusInDropDown(cmbAPP_AGENCY_ID,EffectiveDate); //Commented by Charles on 21-Aug-09 for APP/POL Optimization 
            DataTable objDataTable = Cms.BusinessLayer.BlCommon.ClsAgency.GetAgencyWithStatus(EffectiveDate, gIntCUSTOMER_ID).Tables[0];
            if (objDataTable.Rows.Count > 0)
            {
                lblAGENCY_DISPLAY_NAME.Text = objDataTable.Rows[0]["AGENCY_NAME_ACTIVE_STATUS"].ToString();
                hidIsTerminated.Value = objDataTable.Rows[0]["IS_TERMINATED"].ToString();
                hidAPP_AGENCY_ID.Value = objDataTable.Rows[0]["AGENCY_ID"].ToString();
                ClsCommon.SelectValueinDDL(cmbAGENCY_ID, hidAPP_AGENCY_ID.Value);
                //Setting Agency Color
                if (hidIsTerminated.Value == "Y")
                {
                    lblAGENCY_DISPLAY_NAME.BackColor = Color.Lavender;
                    lblAGENCY_DISPLAY_NAME.ForeColor = Color.Red;
                }
            }
            objDataTable.Dispose();

            DataSet dsState = new DataSet();
            objClsStates = new ClsStates();
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
            }


            if (hidLOBID.Value != "" && hidLOBID.Value != null)
            {

                string SystemId = GetSystemId();

                ClsUser objUser = new ClsUser();
                DataTable dtCSRProducers = objUser.GetCSRProducers(int.Parse(hidAPP_AGENCY_ID.Value), int.Parse(hidLOBID.Value), SystemId);
                if (dtCSRProducers != null)
                {
                    DataView dvSCRPRODUCTs = dtCSRProducers.DefaultView;
                    dvSCRPRODUCTs.Sort = "USERNAME";

                    cmbCSR.DataSource = dvSCRPRODUCTs;
                    cmbCSR.DataTextField = "USER_NAME_ID";
                    cmbCSR.DataValueField = "USER_ID";
                    cmbCSR.DataBind();
                    cmbCSR.Items.Insert(0, new ListItem("", ""));
                }



            }
            AjaxFetchInfo(38);

        }
        private void FillDownPayMode()
        {

        }

        //private void setAgencyColor(DataTable dt)
        //{
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        if (dt.Rows[i]["AGENCY_ID"].ToString() == cmbAGENCY_ID.Items[i].Value && dt.Rows[i]["IS_ACTIVE"].ToString()=="N")
        //        {
        //            cmbAGENCY_ID.Items[i].Attributes.Add("style", "color: red;background-color: #ccccff;");
        //        }
        //    }
        //    cmbAGENCY_ID.Items.Insert(0, new ListItem("", ""));
        //}

        //private void FillCSRDropDown(int AgencyID)
        //{
        //    try
        //    {
        //        ClsUser objUser = new ClsUser();
        //        DataTable dtCSRProducers = objUser.GetCSRProducers(AgencyID, Convert.ToInt16(GetLOBID()));

        //        cmbCSR.Items.Clear();
        //        if (dtCSRProducers != null && dtCSRProducers.Rows.Count > 0)
        //        {
        //            cmbCSR.DataSource = new DataView(dtCSRProducers);
        //            cmbCSR.DataTextField = "USERNAME";
        //            cmbCSR.DataValueField = "USER_ID";
        //            cmbCSR.DataBind();
        //            for (int i = 0; i < cmbCSR.Items.Count; i++)
        //            {
        //                string arrIsActiveStatus = dtCSRProducers.Rows[i]["IS_ACTIVE"getElementById].ToString();
        //                if (arrIsActiveStatus.Equals("N"))
        //                    cmbCSR.Items[i].Attributes.Add("style", "color:red");
        //            }
        //        }
        //        cmbCSR.Items.Insert(0, new ListItem("", ""));
        //        if (hidOldData.Value == "" || hidOldData.Value == null || hidOldData.Value == "0")
        //        {
        //            cmbCSR.Items[0].Value = "-1";
        //        }
        //        string agency_name = GetSystemId();
        //        string strCarrierSystemID = CarrierSystemID;// ConfigurationManager.AppSettings.Get("CarrierSystemID").ToString();
        //        if (agency_name.ToUpper() != strCarrierSystemID.Trim().ToUpper())
        //        {
        //            ListItem li = cmbCSR.Items.FindByValue(GetUserId());
        //            if (li != null)
        //            {
        //                li.Selected = true;
        //                hidCSR.Value = li.Value;
        //            }
        //        }
        //    }
        //    catch (Exception exc)
        //    {
        //        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
        //    }
        //    finally
        //    { }
        //}

        
        //private void FillProducerDropDown(int AgencyID)
        //{
        //    try
        //    {
        //        //fill Producer dropdown
        //        //DataTable dtAgencyUsers = ClsUser.GetAgencyUsers(AgencyID);
        //        ClsUser objUser = new ClsUser();
        //        DataTable dtProducers = objUser.GetProducers(AgencyID);
        //        cmbPRODUCER.Items.Clear();
        //        if (dtProducers != null && dtProducers.Rows.Count > 0)
        //        {
        //            cmbPRODUCER.DataSource = new DataView(dtProducers);
        //            cmbPRODUCER.DataTextField = "USERNAME";
        //            cmbPRODUCER.DataValueField = "USER_ID";
        //            cmbPRODUCER.DataBind();
        //            for (int i = 0; i < cmbPRODUCER.Items.Count; i++)
        //            {
        //                string arrIsActiveStatus = dtProducers.Rows[i]["IS_ACTIVE"].ToString();
        //                if (arrIsActiveStatus.Equals("N"))
        //                    cmbPRODUCER.Items[i].Attributes.Add("style", "color:red");
        //            }
        //        }
        //        cmbPRODUCER.Items.Insert(0, new ListItem("", ""));
        //        cmbPRODUCER.SelectedIndex = 0;
        //    }
        //    catch (Exception exc)
        //    {
        //        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
        //    }
        //    finally
        //    { }
        //}

        [Ajax.AjaxMethod(HttpSessionStateRequirement.Read)]
        public string AjaxFetchCSRProducer(string sAgencyID, string sLobID)
        {
            try
            {
                string result = "";
                SetCultureThread(GetLanguageCode());
                int iAPP_AGENCY_ID = Convert.ToInt32(sAgencyID);
                int iLOBID = int.Parse(sLobID);
                string SystemId = GetSystemId();

                ClsUser objUser = new ClsUser();
                DataTable dtCSRProducers = objUser.GetCSRProducers(iAPP_AGENCY_ID, iLOBID, SystemId);

                if (dtCSRProducers != null)
                {
                    DataView dvCSR = dtCSRProducers.DefaultView;
                    dvCSR.Sort = "USERNAME";
                    result = dvCSR.Table.DataSet.GetXml();

                    //    result = dtCSRProducers.DataSet.GetXml();
                }
                return result == null ? "" : result;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// fetch the active billing plan XML 
        /// </summary>
        private void CheckBillingPlan()
        {
            DataSet ds = new DataSet();
            DataSet dsPlan = new DataSet();
            try
            {
                if (Request.QueryString["APP_ID"].ToString() != "")
                {


                    ds = ClsInstallmentInfo.GetapppolDownpayment(gIntCUSTOMER_ID, int.Parse(Request.QueryString["APP_ID"].ToString()), int.Parse(Request.QueryString["APP_VERSION_ID"].ToString()), "DOWNPAY");
                    //        DataTable dtCSRProducers = objUser.GetCSRProducers(AgencyID, Convert.ToInt16(GetLOBID()));

                    //        cmbCSR.Items.Clear();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        hidPOLDOWN_PAY_MODE.Value = ds.Tables[0].Rows[0]["DOWN_PAY_MODE"].ToString();


                    }

                    #region Commented by Pradeep Kushwaha(20-oct-2010) for implementation of APP_TERMS on Textbox
                    //if (Request.Form["__EVENTTARGET"] == "cmbAPP_TERMS_Change")
                    //{
                    //    ds = ClsInstallmentInfo.GetApplicableInstallmentPlans(int.Parse(cmbAPP_TERMS.SelectedValue));
                    //}
                    //else
                    //{
                    //    int intPolicyTerm = int.Parse(ClsCommon.FetchValueFromXML("APP_TERMS", hidOldData.Value));
                    //    ds = ClsInstallmentInfo.GetApplicableInstallmentPlans(intPolicyTerm, gIntCUSTOMER_ID, int.Parse(Request.QueryString["APP_ID"].ToString()), int.Parse(Request.QueryString["APP_VERSION_ID"].ToString()), "APP");//intPolicyTerm added by Charles on 14-Sep-09 for APP/POL Optimization
                    //}
                    #endregion

                    //Added By Pradeep Kushwaha on 20-Oct-2010
                    int intPolicyTerm = int.Parse(ClsCommon.FetchValueFromXML("APP_TERMS", hidOldData.Value));

                    int iLobiid = int.Parse(ClsCommon.FetchValueFromXML("POLICY_LOB", hidOldData.Value));

                    if (iLobiid != 0 && GetTransaction_Type() == MASTER_POLICY)//GetProduct_Type(iLobiid) == MASTER_POLICY)
                    {
                        DataTable dt = new DataTable();

                        // SANTOSH GAUTAM : BELOW LINE MODIFIED ON 29 OCT 2010
                        // 1. OLD VALUE =>dt = ClsInstallmentInfo.GetApplicableInstallmentPlans(0).Tables[0].Select("PLAN_TYPE in('MAUTO','MMANNUAL')").CopyToDataTable<DataRow>();
                        DataRow[] SelectedRows = ClsInstallmentInfo.GetApplicableInstallmentPlans(0).Tables[0].Select("PLAN_TYPE in('MAUTO','MMANNUAL')");
                        if (SelectedRows.Length > 0)
                            dt = SelectedRows.CopyToDataTable<DataRow>();

                        dsPlan.Tables.Add(dt);
                    }
                    else
                    {

                        DataTable dt = new DataTable();
                        dt = ClsInstallmentInfo.GetApplicableInstallmentPlans(0).Tables[0].Select("PLAN_TYPE NOT IN ('MAUTO','MMANNUAL') OR PLAN_TYPE IS NULL").CopyToDataTable<DataRow>(); ;
                        dsPlan.Tables.Clear();
                        dsPlan.Tables.Add(dt.Copy());
                    }

                    //ds = ClsInstallmentInfo.GetApplicableInstallmentPlans(intPolicyTerm, gIntCUSTOMER_ID, int.Parse(Request.QueryString["APP_ID"].ToString()), int.Parse(Request.QueryString["APP_VERSION_ID"].ToString()), "APP");//intPolicyTerm added by Charles on 14-Sep-09 for APP/POL Optimization
                    //TIll here

                    cmbINSTALL_PLAN_ID.DataSource = dsPlan;
                    cmbINSTALL_PLAN_ID.DataTextField = "BILLING_PLAN";
                    cmbINSTALL_PLAN_ID.DataValueField = "INSTALL_PLAN_ID";
                    cmbINSTALL_PLAN_ID.DataBind();
                    cmbINSTALL_PLAN_ID.Items.Insert(0, "");

                    hidBillingPlan.Value = dsPlan.GetXml();

                    //Get Deactive Plan ID PK.
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
                            if (node != null)
                            {
                                if (node.SelectSingleNode("DEFAULT_PLAN").InnerText == "")
                                    hidFULL_PAY_PLAN_ID.Value = node.FirstChild.InnerText.ToString();
                            }
                        }
                        int count = doc.SelectNodes("NewDataSet/Table/IS_ACTIVE[contains(.,'N')]").Count;
                        if (count == 0)
                            hidDEACTIVE_INSTALL_PLAN_ID.Value = "";
                    }



                }
                else
                {//new app
                    #region Commented by Pradeep Kushwaha(20-oct-2010) for implementation of APP_TERMS on Textbox
                    //if (cmbAPP_TERMS.SelectedValue == "")
                    //    ds = ClsInstallmentInfo.GetApplicableInstallmentPlans();
                    //else
                    //    ds = ClsInstallmentInfo.GetApplicableInstallmentPlans(int.Parse(cmbAPP_TERMS.SelectedValue));
                    #endregion

                    ds = ClsInstallmentInfo.GetApplicableInstallmentPlans(); //Added By pradeep Kushwaha on 20-oct-2010

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
            finally
            {
                ds.Dispose();
                dsPlan.Dispose();
            }
        }

        //Added to set Insured Bill for Carrier same as Agency {W001 in our case}       
        //private void setCarrierInsuredBill()
        //{
        //    cmbBILL_TYPE.Enabled = true;
        //    string strCarrierSystemID = CarrierSystemID;// System.Configuration.ConfigurationManager.AppSettings.Get("CarrierSystemID").ToString();
        //    string strAGENCY_ID = hidAPP_AGENCY_ID.Value.ToString(); 
        //    if (strAGENCY_ID.Equals("27"))//Added by Charles on 24-Aug-09 for APP/POL Optimization 
        //    {
        //        for (int index = 0; index < cmbBILL_TYPE.Items.Count; index++)
        //        {
        //            if (cmbBILL_TYPE.Items[index].Value == "8460" || cmbBILL_TYPE.Items[index].Value == "11150")
        //            {
        //                cmbBILL_TYPE.SelectedIndex = index;
        //                cmbBILL_TYPE.Enabled = false;
        //                break;
        //            }
        //        }
        //    }
        //}

        //private void SetCarrierInsureBillAtPageLoad()
        //{
        //    if (hidOldData.Value != null && hidOldData.Value != "" && hidOldData.Value != "0")
        //    {
        //        System.Xml.XmlDocument objXMLDoc = new XmlDocument();
        //        objXMLDoc.LoadXml(hidOldData.Value);
        //        string strAgency = ClsCommon.GetNodeValue(objXMLDoc, "//APP_AGENCY_ID");

        //        if (strAgency.Equals("27")) // W001-Wolverine = 27
        //        {
        //            cmbBILL_TYPE.Enabled = false;
        //            return;
        //        }
        //    }
        //    else
        //    {                
        //        setCarrierInsuredBill();
        //    }
        //}        

        /*
        private void SetMultiPolicy()
        {
            //Error comes at GetLOBID() function ..Session value being not set when adding new application
            try
            {
                objGeneralInformation = new ClsGeneralInformation();
                string PolicyNumber = "";

                int AgencyID = objGeneralInformation.GetAgencyId(GetSystemId());


                if (hidOldData.Value != null && hidOldData.Value != "0" && hidOldData.Value != "")
                {
                    SetLOBID(FetchValueFromXML("LOB_ID", hidOldData.Value));
                    PolicyNumber = FetchValueFromXML("APP_NUMBER", hidOldData.Value);
                    PolicyNumber = PolicyNumber.Substring(0, (PolicyNumber.Length - 3));
                }

                lblAllPolicy.Text = objGeneralInformation.GetAllPolicyNumber(int.Parse(GetCustomerID()), AgencyID, PolicyNumber);
                if (GetLOBID() != "")//Added by Charles on 18-Sep-09 for APP/POL Optimization
                    lblEligbilePolicy.Text = objGeneralInformation.GetEligiblePolicyNumber(int.Parse(GetCustomerID()), AgencyID, int.Parse(GetLOBID()), PolicyNumber);

                if (lblAllPolicy.Text.Trim() == "")
                    lblAllPolicy.Text = "N.A.";

                if (lblEligbilePolicy.Text.Trim() == "")
                    lblEligbilePolicy.Text = "N.A.";
            }
            catch
            {
                if (lblAllPolicy.Text.Trim() == "")
                    lblAllPolicy.Text = "N.A.";

                if (lblEligbilePolicy.Text.Trim() == "")
                    lblEligbilePolicy.Text = "N.A.";
            }
        }
         */

        private void GetDefaultInstallmentPlan()
        {
            Cms.BusinessLayer.BlCommon.Accounting.ClsInstallmentPlan objPlan = new
                Cms.BusinessLayer.BlCommon.Accounting.ClsInstallmentPlan();

            hidFULL_PAY_PLAN_ID.Value = Cms.BusinessLayer.BlCommon.Accounting.ClsInstallmentPlan.FullPayPlanID().ToString();

            //Added by Pradeep Kushwaha on 20-Oct-2010
            int PlanID = objPlan.GetDefaultPlanId(0);
            if (PlanID != 0)
            {
                if (cmbINSTALL_PLAN_ID.Items.FindByValue(PlanID.ToString()) != null)
                {
                    cmbINSTALL_PLAN_ID.SelectedValue = PlanID.ToString();
                }
                hidINSTALL_PLAN_ID.Value = cmbINSTALL_PLAN_ID.SelectedValue;
            }
            //till here 

            #region Commented by Pradeep Kushwaha(20-oct-2010) for implementation of APP_TERMS on Textbox
            //if (cmbAPP_TERMS.SelectedItem != null && cmbAPP_TERMS.SelectedItem.Value != "")
            //{
            //    int PolTerm = int.Parse(cmbAPP_TERMS.SelectedItem.Value.ToString());

            //    int PlanID = objPlan.GetDefaultPlanId(PolTerm);
            //    if (PlanID != 0)
            //    {
            //        if (cmbINSTALL_PLAN_ID.Items.FindByValue(PlanID.ToString()) != null)
            //        {
            //            cmbINSTALL_PLAN_ID.SelectedValue = PlanID.ToString();
            //        }
            //        hidINSTALL_PLAN_ID.Value = cmbINSTALL_PLAN_ID.SelectedValue;
            //    }
            //}
            #endregion
        }

        #region GetFormValue

        private ClsPolicyInfo GetFormValue()
        {
            //Creating the Model object for holding the New data
            ClsPolicyInfo objGeneralInfo = new ClsPolicyInfo();

            if (hidCustomerID.Value.ToString().Trim() != "" && hidCustomerID.Value.ToString().Trim() != "NEW")
            {
                objGeneralInfo.CUSTOMER_ID = int.Parse(hidCustomerID.Value.ToString() == "" ? "0" : hidCustomerID.Value.ToString());
            }
            else
            {
                string customerID = GetCustomerID();
                hidCustomerID.Value = customerID;
                objGeneralInfo.CUSTOMER_ID = int.Parse(customerID.Trim() == "" ? "0" : customerID);
            }

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
            if (hidAPP_EFFECTIVE_DATE.Value != "")
                objGeneralInfo.APP_EFFECTIVE_DATE = ConvertToDate(hidAPP_EFFECTIVE_DATE.Value);
            else
            objGeneralInfo.APP_EFFECTIVE_DATE = ConvertToDate(txtAPP_EFFECTIVE_DATE.Text);

            // Get the AppID from the database depending on the customer selected
            if (hidAppID.Value.ToString().Trim() != "" && hidAppID.Value.ToString().Equals("NEW"))
            {
                objGeneralInfo.APP_ID = 1;

                if (txtAPP_NUMBER.Text.Trim() == TEMP_APP_NUMBER)
                {
                    objGeneralInfo.APP_NUMBER = ClsGeneralInformation.GenerateApplicationNumber(int.Parse(cmbAPP_LOB.SelectedItem.Value), int.Parse(hidAPP_AGENCY_ID.Value == "" ? "0" : hidAPP_AGENCY_ID.Value));//Again implemeted by sonal to differentiate application and policy number.n
                    //objGeneralInfo.APP_NUMBER = ClsGeneralInformation.GenerateAppPolNumber(int.Parse(cmbAPP_LOB.SelectedItem.Value), objGeneralInfo.DIV_ID, objGeneralInfo.APP_EFFECTIVE_DATE, "APP");

                    if (objGeneralInfo.APP_NUMBER.Trim() == "")
                    {
                        return null;
                    }
                }
                else
                {
                    objGeneralInfo.APP_NUMBER = txtAPP_NUMBER.Text;
                    //hidAppID.Value = GetAppID();
                    //objGeneralInfo.APP_ID = int.Parse(hidAppID.Value);
                }
                hidPolicyNumber.Value = objGeneralInfo.APP_NUMBER;
            }
            else
            {
                objGeneralInfo.APP_NUMBER = hidPolicyNumber.Value == "" ? txtAPP_NUMBER.Text : hidPolicyNumber.Value;
                objGeneralInfo.APP_ID = int.Parse(hidAppID.Value);
            }

            objGeneralInfo.APP_VERSION_ID = int.Parse(hidAppVersionID.Value);
            objGeneralInfo.APP_STATUS = txtPOLICY_STATUS.Text;

            objGeneralInfo.APP_VERSION = objGeneralInfo.APP_VERSION_ID.ToString() + ".0";

            if (txtAPP_INCEPTION_DATE.Text.Trim() != "")
            {
                objGeneralInfo.APP_INCEPTION_DATE = ConvertToDate(txtAPP_INCEPTION_DATE.Text);
                //Convert.ToDateTime(Convert.ToDateTime().ToString("MM/dd/yyyy"));
            }
            else
            {
                //added by Lalit march 02,11. If inception date is blank then inception date = effective date
                //i-track # 644
                objGeneralInfo.APP_INCEPTION_DATE = ConvertToDate(txtAPP_EFFECTIVE_DATE.Text);//ConvertToDate(txtAPP_INCEPTION_DATE.Text);
            }

            //Convert.ToDateTime(Convert.ToDateTime(txtAPP_EFFECTIVE_DATE.Text).ToString("MM/dd/yyyy")) ;
            appTerms = hidAPP_TERMS.Value == "" ? "0" : hidAPP_TERMS.Value;
            exp_Date = txtAPP_EXPIRATION_DATE.Text.Trim();
            //objGeneralInfo.APP_EXPIRATION_DATE = objGeneralInfo.APP_EFFECTIVE_DATE.AddMonths(int.Parse(appTerms));//Commented by Pradeep Kushwaha(20-oct-2010) for implementation of APP_TERMS on Textbox
            objGeneralInfo.APP_EXPIRATION_DATE = objGeneralInfo.APP_EFFECTIVE_DATE.AddDays(int.Parse(appTerms));//Added by Pradeep Kushwaha on 20-Oct-2010
            objGeneralInfo.POLICY_LOB = cmbAPP_LOB.SelectedValue == null ? "" : cmbAPP_LOB.SelectedValue;
            objGeneralInfo.POLICY_SUBLOB = hidSUB_LOB.Value == "" ? "0" : hidSUB_LOB.Value;

            if (objGeneralInfo.POLICY_SUBLOB == "0" && (GetSystemId().ToUpper() == "S001" || GetSystemId().ToUpper() == "SUAT"))
                objGeneralInfo.POLICY_SUBLOB = "1";

            objGeneralInfo.CSR = int.Parse(hidCSR.Value == "" ? "-1" : hidCSR.Value);

            objGeneralInfo.PRODUCER = int.Parse(hidProducer.Value == "" ? "0" : hidProducer.Value);
            hidLOBID.Value = cmbAPP_LOB.SelectedItem.Value == null ? "0" : cmbAPP_LOB.SelectedItem.Value;

            objGeneralInfo.COUNTRY_ID = 1;

            //objGeneralInfo.AGENCY_ID = int.Parse(hidAPP_AGENCY_ID.Value == "" ? "0" : hidAPP_AGENCY_ID.Value); //Added by Charles on 21-Aug-09 for APP/POL Optimization
    
           if (cmbAGENCY_ID.SelectedValue != "" && cmbAGENCY_ID.SelectedValue != "0")
           {
               objGeneralInfo.AGENCY_ID = int.Parse(cmbAGENCY_ID.SelectedValue);
               hidAPP_AGENCY_ID.Value = cmbAGENCY_ID.SelectedValue;
           }
           else
           {
               if (!string.IsNullOrEmpty(hidAGENCY_ID.Value) && hidAGENCY_ID.Value != "0")
                   objGeneralInfo.AGENCY_ID = int.Parse(hidAGENCY_ID.Value.Trim());
           }
            //DataSet ds = new DataSet();
            //ds = ClsDefaultHierarchy.GetDefaultHierarchy(Convert.ToInt32(hidAPP_AGENCY_ID.Value)); //Changed to hidAPP_AGENCY_ID.Value by Charles on 21-Aug-09 for APP/POL Optimization
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    objGeneralInfo.DIV_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["Division"].ToString());
            //    objGeneralInfo.DEPT_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["DeptId"].ToString());
            //    objGeneralInfo.PC_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["ProfitCenterId"].ToString());
            //}
            //else
            //{
            //    objGeneralInfo.DIV_ID = 0;
            //    objGeneralInfo.DEPT_ID = 0;
            //    objGeneralInfo.PC_ID = 0;
            //}
            //ds.Dispose();   

            if (cmbBILL_TYPE.SelectedValue != null)
            {
                objGeneralInfo.BILL_TYPE = int.Parse(hidBILL_TYPE_ID.Value);
                strBillType = hidBILL_TYPE_ID.Value;
                //if (objGeneralInfo.BILL_TYPE == 8460 || objGeneralInfo.BILL_TYPE == 11150 || objGeneralInfo.BILL_TYPE == 11278 || objGeneralInfo.BILL_TYPE == 11276) //Insured Bill/Insured Bill all terms/Insured Bill 1st term/Mortgagee @renewal Comment By Ruchika
                if (objGeneralInfo.BILL_TYPE == 114327 || objGeneralInfo.BILL_TYPE == 114328 || objGeneralInfo.BILL_TYPE == 114329 || objGeneralInfo.BILL_TYPE == 11276) //Insured Bill/Insured Bill all terms/Insured Bill 1st term/Mortgagee @renewal 
                {
                    if (hidDOWN_PAY_MODE.Value != "")
                        objGeneralInfo.DOWN_PAY_MODE = int.Parse(hidDOWN_PAY_MODE.Value);

                    if (hidINSTALL_PLAN_ID.Value != "") //modified by Lalit Nov 26,2010
                        objGeneralInfo.INSTALL_PLAN_ID = Convert.ToInt32(hidINSTALL_PLAN_ID.Value);
                    else
                        objGeneralInfo.INSTALL_PLAN_ID = int.Parse(hidFULL_PAY_PLAN_ID.Value);
                }
            }

            //Added by Agniswar for Singapore Implementation

            if (cmbFUND_TYPE.SelectedValue != null && cmbFUND_TYPE.SelectedValue != "")
            {
                objGeneralInfo.FUND_TYPE = int.Parse(cmbFUND_TYPE.SelectedValue);
            }

            if (cmbBILLING_CURRENCY.SelectedValue != null && cmbBILLING_CURRENCY.SelectedValue != "")
            {
                objGeneralInfo.BILLING_CURRENCY = int.Parse(cmbBILLING_CURRENCY.SelectedValue);
            }
            if (GetSystemId().ToUpper() == "S001" || GetSystemId().ToUpper() == "SUAT")
            {
                try
                {
                    if (hidOldData.Value.Trim() != null && hidOldData.Value.Trim() != "" && hidOldData.Value.Trim() != "0")
                    {
                        objGeneralInfo.RECEIVED_PRMIUM = double.Parse(ClsCommon.FetchValueFromXML("RECEIVED_PRMIUM", hidOldData.Value));
                    }
                }
                catch (Exception ex)
                { }
            }
            //objGeneralInfo.COMPLETE_APP = chkCOMPLETE_APP.Checked == true ? "Y" : "N";

            if (hidPOL_TRAN_TYPE.Value.Trim() != "" && hidPOL_TRAN_TYPE.Value.Trim() != "0")
            {
                objGeneralInfo.TRANSACTION_TYPE = int.Parse(hidPOL_TRAN_TYPE.Value.Trim());
            }
            if (hidPOL_TRAN_TYPE.Value.Trim() == "14560") //for master policy Policy level commission not applicable ,i-track #-543
            {
                objGeneralInfo.POLICY_LEVEL_COMM_APPLIES = "N";//chkPOLICY_LEVEL_COMM_APPLIES.Checked == true ? "Y" : "N";
                objGeneralInfo.POLICY_LEVEL_COMISSION = 0;

            }
            else
            {

                objGeneralInfo.POLICY_LEVEL_COMM_APPLIES = chkPOLICY_LEVEL_COMM_APPLIES.Checked == true ? "Y" : "N";

                if (txtPOLICY_LEVEL_COMISSION.Text.Trim() != "" && chkPOLICY_LEVEL_COMM_APPLIES.Checked)
                {
                    //Modified by Pradeep on 22-July-2011-iTrack-1411,1370- number format info based on the policy currecny selected 
                    NumberFormatInfo NfiPolicyLevelCommission = new NumberFormatInfo();
                    if(hidPOLICY_CURRENCY.Value=="2")
                        NfiPolicyLevelCommission = new CultureInfo(enumCulture.BR, true).NumberFormat;
                    else
                        NfiPolicyLevelCommission = new CultureInfo(enumCulture.US, true).NumberFormat;
                    objGeneralInfo.POLICY_LEVEL_COMISSION = Convert.ToDouble(txtPOLICY_LEVEL_COMISSION.Text, NfiPolicyLevelCommission);  //double.Parse(txtPOLICY_LEVEL_COMISSION.Text);
                    //Till here 
                }
            }
            //if (cmbPOLICY_CURRENCY.SelectedItem != null && cmbPOLICY_CURRENCY.SelectedValue != "")
            //{
            objGeneralInfo.POLICY_CURRENCY = int.Parse(hidPOLICY_CURRENCY.Value);

            //    objGeneralInfo.POLICY_CURRENCY = int.Parse(cmbPOLICY_CURRENCY.SelectedValue);
            //}
            if (cmbCO_INSURANCE.SelectedItem != null && cmbCO_INSURANCE.SelectedValue != "")
            {
                objGeneralInfo.CO_INSURANCE = int.Parse(cmbCO_INSURANCE.SelectedValue);
            }
            //if (cmbContact_Person.SelectedItem != null && cmbContact_Person.SelectedValue!="")
            //{
            //    objGeneralInfo.CONTACT_PERSON = int.Parse(cmbContact_Person.SelectedValue);
            //}                  

            if (hidPAYOR.Value != null && hidPAYOR.Value != "0" && hidPAYOR.Value != "")
            {
                objGeneralInfo.PAYOR = int.Parse(hidPAYOR.Value);

                if (txtCUSTOMERNAME.Text.Trim() != "" && objGeneralInfo.PAYOR != 14544)//Leader
                {
                    objGeneralInfo.BILLTO = txtCUSTOMERNAME.Text;
                }
            }



            //if (txtPREFERENCE_DAY.Text.Trim() != "")
            //{
            //    objGeneralInfo.PREFERENCE_DAY = int.Parse(txtPREFERENCE_DAY.Text.Trim());
            //}

            if (txtBROKER_REQUEST_NO.Text.Trim() != "")
            {
                objGeneralInfo.BROKER_REQUEST_NO = txtBROKER_REQUEST_NO.Text.Trim();
            }
            if (txtPOLICY_DESCRIPTION.Text.Trim() != "")
            {
                objGeneralInfo.POLICY_DESCRIPTION = txtPOLICY_DESCRIPTION.Text.Trim();
            }

            objGeneralInfo.BROKER_COMM_FIRST_INSTM = chkBROKER_COMM_FIRST_INSTM.Checked == true ? "Y" : "N";

            #region Commented
            /*
           if (cmbPROXY_SIGN_OBTAINED.SelectedItem != null && cmbPROXY_SIGN_OBTAINED.SelectedItem.Value != "")
           {
               objGeneralInfo.PROXY_SIGN_OBTAINED = int.Parse(cmbPROXY_SIGN_OBTAINED.SelectedItem.Value);
           }            
           if (cmbSTATE_ID.SelectedItem != null)
           {
               if (cmbSTATE_ID.SelectedItem.Value != "")
                   objGeneralInfo.STATE_ID = int.Parse(cmbSTATE_ID.SelectedItem.Value);
               else
                   objGeneralInfo.STATE_ID = int.Parse(hidSTATE_ID.Value);
           }
            
            if (cmbPROPRTY_INSP_CREDIT.SelectedItem != null)
            {
                objGeneralInfo.PROPRTY_INSP_CREDIT = cmbPROPRTY_INSP_CREDIT.SelectedValue;
            }
            if (cmbPIC_OF_LOC.SelectedItem != null)
            {
                objGeneralInfo.PIC_OF_LOC = cmbPIC_OF_LOC.SelectedItem.Value;
            }
             
           if (hidOldData.Value.Trim() != "" && hidOldData.Value.Trim() != "0")
           {
               //Taking from hidden control in edit mode
               objGeneralInfo.POLICY_TYPE = hidPOLICY_TYPE.Value;
           }
           else
           {
               //Taking from combox in add mode
               //if(cmbPOLICY_TYPE.SelectedItem!=null)
               if ((hidLOBID.Value == ((int)enumLOB.HOME).ToString() || hidLOBID.Value == ((int)enumLOB.REDW).ToString()) && cmbPOLICY_TYPE.SelectedItem != null && cmbPOLICY_TYPE.SelectedItem.Value != "")
               {
                   if (cmbPOLICY_TYPE.SelectedItem.Value != "")
                   {
                       objGeneralInfo.POLICY_TYPE = cmbPOLICY_TYPE.SelectedItem.Value;
                       hidPOLICY_TYPE.Value = cmbPOLICY_TYPE.SelectedItem.Value;
                   }
               }
           }
            
            if (txtYEAR_AT_CURR_RESI.Text.Trim() != "")
                objGeneralInfo.YEAR_AT_CURR_RESI = Convert.ToInt32(txtYEAR_AT_CURR_RESI.Text);

            //--- Condition added by mohit on 13/10/2005-------------.
            if (txtYEAR_AT_CURR_RESI.Text.Trim() != "" && int.Parse(txtYEAR_AT_CURR_RESI.Text.Trim()) < 3 && txtYEAR_AT_CURR_RESI.Text.Trim() != "0")
            {
                //if (txtYEARS_AT_PREV_ADD.Text.Trim() != "")
                objGeneralInfo.YEARS_AT_PREV_ADD = txtYEARS_AT_PREV_ADD.Text;
            }
            else
            {
                objGeneralInfo.YEARS_AT_PREV_ADD = "";
            }
            
           if (cmbCHARGE_OFF_PRMIUM.SelectedItem != null)
           {
               objGeneralInfo.CHARGE_OFF_PRMIUM = cmbCHARGE_OFF_PRMIUM.SelectedItem.Value;
           }
            */
            #endregion

            // chk for home employee
            string AgencyCode = ClsAgency.GetAgencyCodeFromID(int.Parse(hidAPP_AGENCY_ID.Value == "" ? "0" : hidAPP_AGENCY_ID.Value));//Changed to hidAPP_AGENCY_ID.Value by Charles on 21-Aug-09 for APP/POL Optimization
            string appAgencyCode = CarrierSystemID;// System.Configuration.ConfigurationManager.AppSettings.Get("CarrierSystemID");
            if (appAgencyCode.ToUpper().Trim() == AgencyCode.ToUpper().Trim())
                objGeneralInfo.IS_HOME_EMP = true;
            else
                objGeneralInfo.IS_HOME_EMP = false;

            // state for all old products
            if (cmbAPP_LOB.SelectedItem != null)
            {
                if (int.Parse(cmbAPP_LOB.SelectedValue) <= 8)
                    objGeneralInfo.STATE_ID = int.Parse(hidSTATE_ID.Value);
                else if (int.Parse(cmbAPP_LOB.SelectedValue) == 38)
                    objGeneralInfo.STATE_ID = 92;//int.Parse(hidSTATE_ID.Value);
                else
                    objGeneralInfo.STATE_ID = 0;



            }
            else
                objGeneralInfo.STATE_ID = 0;


            if (cmbAPP_LOB.SelectedItem != null)
            {
                if (int.Parse(cmbAPP_LOB.SelectedValue) == 1 || int.Parse(cmbAPP_LOB.SelectedValue) == 6)
                {
                    objGeneralInfo.POLICY_TYPE = hidPOLICY_TYPE.Value;
                }
            }

            //These  assignments are common to all pages.
            strFormSaved = hidFormSaved.Value;
            strRowId = hidAppID.Value;
            oldXML = hidOldData.Value;

            return objGeneralInfo;
        }
        #endregion

        #region Set Validators ErrorMessages
        private void SetErrorMessages()
        {
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            try
            {
                rfvAPP_TERMS.ErrorMessage = ClsMessages.FetchGeneralMessage("93");
                rfvAPP_EFFECTIVE_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("95");
                rfvAPP_EXPIRATION_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("96");
                rfvAPP_LOB.ErrorMessage = ClsMessages.FetchGeneralMessage("97");
                rfvBILL_TYPE.ErrorMessage = ClsMessages.FetchGeneralMessage("218");
                rfvINSTALL_PLAN_ID.ErrorMessage = ClsMessages.FetchGeneralMessage("731");
                rfvDOWN_PAY_MODE.ErrorMessage = ClsMessages.FetchGeneralMessage("1062");
                rfvAGENCY_ID.ErrorMessage = ClsMessages.FetchGeneralMessage("99");
                rfvPOLICY_CURRENCY.ErrorMessage = ClsMessages.FetchGeneralMessage("1070");
                rfvCO_INSURANCE.ErrorMessage = ClsMessages.FetchGeneralMessage("1071");
                rfvPAYOR.ErrorMessage = ClsMessages.FetchGeneralMessage("1072");
                rfvPOLICY_SUBLOB.ErrorMessage = ClsMessages.FetchGeneralMessage("98");
                rfvPOLICY_LEVEL_COMISSION.ErrorMessage = ClsMessages.FetchGeneralMessage("1080");
                rfvDIV_ID_DEPT_ID_PC_ID.ErrorMessage = ClsMessages.FetchGeneralMessage("1092");
                rfvCSR.ErrorMessage = ClsMessages.FetchGeneralMessage("1093");

                revPOLICY_LEVEL_COMISSION.ErrorMessage = ClsMessages.FetchGeneralMessage("611");
                revAPP_EFFECTIVE_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("22");
                revAPP_INCEPTION_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("22");

                csvPOLICY_LEVEL_COMISSION.ErrorMessage = ClsMessages.FetchGeneralMessage("1064");
                csvAPP_EFFECTIVE_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("490");

                rngPREFERENCE_DAY.ErrorMessage = ClsMessages.FetchGeneralMessage("1091");
                revAPP_NUMBER.ErrorMessage = ClsMessages.FetchGeneralMessage("1167");
                revAPP_EFFECTIVE_DATE.ValidationExpression = aRegExpDate;
                revAPP_INCEPTION_DATE.ValidationExpression = aRegExpDate;
                revPOLICY_LEVEL_COMISSION.ValidationExpression = aRegExpDoublePositiveWithZeroFourDeci;
                revAPP_NUMBER.ValidationExpression = aRegExpAppPolicyNum;

                rfvState_ID.ErrorMessage = ClsMessages.FetchGeneralMessage("788");
                rfvPOLICY_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("428");

                revAPP_TERMS.ValidationExpression = aRegExpIntegerPositiveNonZero;
                revAPP_TERMS.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1178");
                //Added by lalit

                rfvTRANSACTION_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1195");


                rfvAPP_INCEPTION_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("94");

                //Added by Ruchika Chauhan on 18-Jan-2012 for TFS # 1211
                rfvPRODUCER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1097");
                
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            #region Commented
            //rngYEAR_AT_CURR_RESI.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
            //revRECEIVED_PRMIUM.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("611");            
            //rfvPOLICY_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("428");
            //rfvPROXY_SIGN_OBTAINED.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("479");
            //csvYEARS_AT_PREV_ADD.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("445");
            //revYEAR_AT_CURR_RESI.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
            //rfvPROPRTY_INSP_CREDIT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("797");
            //rfvPIC_OF_LOC.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("798");
            //rfvYEARS_AT_PREV_ADD.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("520");
            //rfvCUSTOMER_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("100");
            //rfvSTATE_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("35");

            //revYEAR_AT_CURR_RESI.ValidationExpression = aRegExpInteger;
            //revRECEIVED_PRMIUM.ValidationExpression = aRegExpCurrencyformat;
            #endregion
        }
        #endregion

        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            //this.cmbAPP_LOB.SelectedIndexChanged += new System.EventHandler(this.cmbAPP_LOB_SelectedIndexChanged);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.Load += new System.EventHandler(this.Page_Load);
            this.btnConvertAppToPolicy.Click += new System.EventHandler(this.btnConvertAppToPolicy_Click);
            this.btnCreateNewVersion.Click += new System.EventHandler(this.btnCreateNewVersion_Click);
            this.btnDelete.Click += new EventHandler(btnDelete_Click);
            this.btnActivateDeactivate.Click += new EventHandler(btnActivateDeactivate_Click);
            //this.cmbSTATE_ID.SelectedIndexChanged+=new EventHandler(this.cmbSTATE_ID_SelectedIndexChanged);
            this.btnSubmitAnyway.Click += new EventHandler(btnSubmitAnyway_Click);
            this.btnReject.Click += new EventHandler(btnReject_Click);
            this.btnCopy.Click += new EventHandler(btnCopy_Click);

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
            ClsPolicyInfo objGeneralInfo = GetFormValue();

            if (objGeneralInfo == null || objGeneralInfo.APP_NUMBER.Trim() == "")
            {
                ClsMessages.SetCustomizedXml(GetLanguageCode());
                lblMessage.Text = ClsMessages.FetchGeneralMessage("21") + "<br />" + objResourceMgr.GetString("strErrMsg");
                lblMessage.Visible = true;
                hidFormSaved.Value = "2";
                return;
            }

            objGeneralInfo.APP_TERMS = appTerms;
            //objGeneralInfo.APP_EXPIRATION_DATE = Convert.ToDateTime(exp_Date);
            try
            {
                //For retrieving the return value of business class save function
                int intRetVal;
                int checkVal;
                objGeneralInformation = new ClsGeneralInformation();
                objGeneralInformation.TransactionRequired = true;

                //Retreiving the form values into model class object
                string strLOB = "";
                if (cmbAPP_LOB.SelectedIndex > 0)
                    strLOB = cmbAPP_LOB.SelectedItem.Text;
                checkVal = objGeneralInformation.CheckDuplicateAppNumber(objGeneralInfo.APP_NUMBER, "APP", int.Parse(cmbAPP_LOB.SelectedItem.Value), objGeneralInfo.APP_EFFECTIVE_DATE);
                if (strRowId.ToUpper().Equals("NEW")) //save case
                {
                    objGeneralInfo.CREATED_BY = int.Parse(GetUserId());
                    objGeneralInfo.CREATED_DATETIME = DateTime.Now;
                    objGeneralInfo.IS_ACTIVE = "Y";
                    strRowId = objGeneralInfo.APP_ID.ToString();
                    //Calling the add method of business layer class
                    objGeneralInfo.POLICY_STATUS = "APPLICATION";
                    if (checkVal == 0)
                    {
                        intRetVal = objGeneralInformation.AddPolicy(objGeneralInfo, strLOB);

                        if (intRetVal > 0)
                        {
                            //if (CarrierSystemID.ToUpper() == GetSystemId().ToUpper())
                            //{
                            //CO-INSURANCE UPDATE
                            ClsCoInsuranceInfo objCoInsuranceInfo = new ClsCoInsuranceInfo();
                            objCoInsuranceInfo.CUSTOMER_ID = objGeneralInfo.CUSTOMER_ID;
                            objCoInsuranceInfo.POLICY_ID = objGeneralInfo.POLICY_ID;
                            objCoInsuranceInfo.POLICY_VERSION_ID = objGeneralInfo.APP_VERSION_ID;
                            objCoInsuranceInfo.LEADER_FOLLOWER = int.Parse(cmbCO_INSURANCE.SelectedValue);
                            ClsGeneralInformation.UpdateDefaultCoInsurer(objCoInsuranceInfo, GetSystemId()); //CarrierSystemID.ToUpper());                        
                            //}
                            //RENUMERATION TAB UPDATE
                            ClsPolicyRemunerationInfo objnewPolicyRemunerationInfo = new ClsPolicyRemunerationInfo();
                            objnewPolicyRemunerationInfo.CUSTOMER_ID.CurrentValue = objGeneralInfo.CUSTOMER_ID;
                            objnewPolicyRemunerationInfo.POLICY_ID.CurrentValue = objGeneralInfo.POLICY_ID;
                            objnewPolicyRemunerationInfo.POLICY_VERSION_ID.CurrentValue = objGeneralInfo.APP_VERSION_ID;
                            ClsGeneralInformation.UpdateDefaultBroker(objnewPolicyRemunerationInfo, objGeneralInfo.AGENCY_ID, -1);

                            //CLAUSES TAB UPDATE
                            ClsPolicyClauseInfo objPolicyClauseInfo = new ClsPolicyClauseInfo();
                            objPolicyClauseInfo.CUSTOMER_ID.CurrentValue = objGeneralInfo.CUSTOMER_ID;
                            objPolicyClauseInfo.POLICY_ID.CurrentValue = objGeneralInfo.POLICY_ID;
                            objPolicyClauseInfo.POLICY_VERSION_ID.CurrentValue = objGeneralInfo.APP_VERSION_ID;
                            objPolicyClauseInfo.SUSEP_LOB_ID.CurrentValue = int.Parse(objGeneralInfo.POLICY_LOB);
                            int lobID = int.Parse(objGeneralInfo.POLICY_LOB);
                            objPolicyClauseInfo.UpdateDefaultClauses(lobID);


                            Session["LoadedAfterSave"] = "True";
                            lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                            this.btnConvertAppToPolicy.Attributes.Add("Style", "display:inline");
                            btnSubmitAnyway.Attributes.Add("Style", "display:inline");

                            hidCustomerID.Value = objGeneralInfo.CUSTOMER_ID.ToString();
                            hidAppID.Value = objGeneralInfo.APP_ID.ToString();
                            hidAppVersionID.Value = objGeneralInfo.APP_VERSION_ID.ToString();
                            hidPolicyID.Value = objGeneralInfo.POLICY_ID.ToString();
                            hidPolicyVersionID.Value = objGeneralInfo.APP_VERSION_ID.ToString();
                            hidLOBID.Value = objGeneralInfo.POLICY_LOB;

                            SetCustomerID(hidCustomerID.Value);
                            SetAppID(hidAppID.Value);
                            SetAppVersionID(hidAppVersionID.Value);
                            SetPolicyID(hidPolicyID.Value);
                            SetPolicyVersionID(hidPolicyVersionID.Value);
                            SetLOBID(hidLOBID.Value);
                            SetPolicyStatus("");

                            
                            SetPolicyCurrency(objGeneralInfo.POLICY_CURRENCY.ToString());
                            SetApplicationStatus("APPLICATION");
                            hidFormSaved.Value = "1";
                            hidIS_ACTIVE.Value = "Y";

                            //Added By Lalit Chauhan, Nov 08 2010
                            SetTransaction_Type(objGeneralInfo.TRANSACTION_TYPE.ToString());
                            hidPOL_TRAN_TYPE.Value = objGeneralInfo.TRANSACTION_TYPE.ToString(); //dsPolicy.Tables[0].Rows[0]["TRANSACTION_TYPE"].ToString();
                            hidPOLICY_ID.Value = hidPolicyID.Value;
                            hidPOLICY_VERSION_ID.Value = hidPolicyVersionID.Value;

                            btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(hidIS_ACTIVE.Value);
                            ClientScript.RegisterStartupScript(this.GetType(),
                            "LoadPage", "<script>parent.document.location.href='/Cms/Policies/aspx/PolicyTab.aspx?CalledFrom=APP&APP_ID=" + hidAppID.Value
                            + "&APP_VERSION_ID=" + hidAppVersionID.Value + "&POLICY_ID=" + hidPOLICY_ID.Value + "&POLICY_VERSION_ID="
                            + hidPOLICY_VERSION_ID.Value + "&POLICY_LOB=" + hidLOBID.Value + "'</script>");

                            SetCookieValue();
                        }
                        else if (intRetVal == -1)
                        {
                            ClsMessages.SetCustomizedXml(GetLanguageCode());
                            lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                            hidFormSaved.Value = "2";
                        }
                        else
                        {
                            ClsMessages.SetCustomizedXml(GetLanguageCode());
                            lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                            hidFormSaved.Value = "2";
                        }
                    }
                    else
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = objResourceMgr.GetString("lbDuplicate");
                        hidFormSaved.Value = "2";
                    }

                    dsPolicy = new DataSet();
                    dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCustomerID.Value == "" ? GetCustomerID() : hidCustomerID.Value), int.Parse(hidPolicyID.Value == "" ? GetPolicyID() : hidPolicyID.Value), int.Parse(hidPolicyVersionID.Value == "" ? GetPolicyVersionID() : hidPolicyVersionID.Value));


                    hidOldData.Value = ClsCommon.GetXMLEncoded(dsPolicy.Tables[0]);
                    if (hidLangCulture.Value.Trim() != cmsbase.DEFAULT_LANG_CULTURE)
                    {
                        hidOldData.Value = FormatXMLDateNode(hidOldData.Value, strDateNodes);
                    }


                    //add by lalit
                    ClsCommon.setPolicyCurrencySymbol(objGeneralInfo.POLICY_CURRENCY.ToString());
                }
                else //UPDATE CASE
                {

                    objGeneralInfo.POLICY_ID = int.Parse(hidPOLICY_ID.Value == "" ? GetPolicyID() : hidPOLICY_ID.Value);
                    objGeneralInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value == "" ? GetPolicyVersionID() : hidPOLICY_VERSION_ID.Value);

                    objGeneralInformation = new ClsGeneralInformation();
                    objGeneralInformation.TransactionRequired = true;

                    //Creating the Model object for holding the Old data
                    ClsPolicyInfo objOldPolicyInfo = new ClsPolicyInfo();

                    if (hidOldData.Value == "")
                    {
                        dsPolicy = new DataSet();
                        dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCustomerID.Value == "" ? GetCustomerID() : hidCustomerID.Value), int.Parse(hidPolicyID.Value == "" ? GetPolicyID() : hidPolicyID.Value), int.Parse(hidPolicyVersionID.Value == "" ? GetPolicyVersionID() : hidPolicyVersionID.Value));
                        hidOldData.Value = ClsCommon.GetXMLEncoded(dsPolicy.Tables[0]);
                        if (hidLangCulture.Value.Trim() != cmsbase.DEFAULT_LANG_CULTURE)
                        {
                            hidOldData.Value = FormatXMLDateNode(hidOldData.Value, strDateNodes);
                        }
                    }

                    //Setting  the Old Page details(XML File containing old details) into the Model Object
                    base.PopulateModelObject(objOldPolicyInfo, hidOldData.Value);
                    hidIS_ACTIVE.Value = ClsCommon.FetchValueFromXML("POLICY_IS_ACTIVE", hidOldData.Value);

                    //Setting those values into the Model object which are not in the page
                    objGeneralInfo.MODIFIED_BY = int.Parse(GetUserId());
                    objGeneralInfo.LAST_UPDATED_DATETIME = DateTime.Now;
                    objGeneralInfo.POLICY_LOB = GetLOBID();
                    objGeneralInfo.IS_ACTIVE = hidIS_ACTIVE.Value;
                    objGeneralInfo.POLICY_NUMBER = ClsCommon.FetchValueFromXML("POLICY_NUMBER", hidOldData.Value);
                    objGeneralInfo.POLICY_DISP_VERSION = ClsCommon.FetchValueFromXML("POLICY_DISP_VERSION", hidOldData.Value);

                    objGeneralInfo.POLICY_STATUS = "APPLICATION";
                    //Added By kuldeep on 14/1/2012
                    if (cmbDIV_ID_DEPT_ID_PC_ID.SelectedValue != "")
                    {
                        string[] strDivDeptPC = cmbDIV_ID_DEPT_ID_PC_ID.SelectedValue.Split('^');

                        objGeneralInfo.DIV_ID = Convert.ToInt32(strDivDeptPC[0]);
                        objGeneralInfo.DEPT_ID = Convert.ToInt32(strDivDeptPC[1]);
                        objGeneralInfo.PC_ID = Convert.ToInt32(strDivDeptPC[2]);
                    }
                    //Updating the record using business layer class object
                    intRetVal = objGeneralInformation.UpdatePolicy(objOldPolicyInfo, objGeneralInfo, "APPLICATION");
                    hidPOL_TRAN_TYPE.Value = objGeneralInfo.TRANSACTION_TYPE.ToString();

                    if (intRetVal > 0)			// update successfully performed
                    {
                        if ((objGeneralInfo.APP_EFFECTIVE_DATE != objOldPolicyInfo.APP_EFFECTIVE_DATE)
                            || (objGeneralInfo.APP_EXPIRATION_DATE != objOldPolicyInfo.APP_EXPIRATION_DATE)
                            || (objGeneralInfo.APP_TERMS != objOldPolicyInfo.APP_TERMS))
                        {
                            hidRefresh.Value = "R";
                        }
                        //ClsGeneralInformation.GetBillType(cmbBILL_TYPE, Convert.ToInt32(hidLOBID.Value==""?GetLOBID():hidLOBID.Value), int.Parse(hidCustomerID.Value), hidPolicyID.Value==""?GetPolicyID():hidPolicyID.Value, int.Parse(hidPolicyVersionID.Value==""?GetPolicyVersionID():hidPOLICY_VERSION_ID.Value), "POL");                       

                        //if (CarrierSystemID.ToUpper() == GetSystemId().ToUpper())
                        //{
                        //CO-INSURANCE UPDATE
                        if (objGeneralInfo.CO_INSURANCE != objOldPolicyInfo.CO_INSURANCE)
                        {
                            ClsCoInsuranceInfo objCoInsuranceInfo = new ClsCoInsuranceInfo();
                            objCoInsuranceInfo.CUSTOMER_ID = objGeneralInfo.CUSTOMER_ID;
                            objCoInsuranceInfo.POLICY_ID = objGeneralInfo.POLICY_ID;
                            objCoInsuranceInfo.POLICY_VERSION_ID = objGeneralInfo.POLICY_VERSION_ID;
                            objCoInsuranceInfo.LEADER_FOLLOWER = int.Parse(cmbCO_INSURANCE.SelectedValue);
                            ClsGeneralInformation.UpdateDefaultCoInsurer(objCoInsuranceInfo, GetSystemId()); //CarrierSystemID.ToUpper());
                        }

                        //}
                        //RENUMERATION TAB UPDATE
                        if (objGeneralInfo.AGENCY_ID != objOldPolicyInfo.AGENCY_ID)
                        {
                            ClsPolicyRemunerationInfo objnewPolicyRemunerationInfo = new ClsPolicyRemunerationInfo();
                            objnewPolicyRemunerationInfo.CUSTOMER_ID.CurrentValue = objGeneralInfo.CUSTOMER_ID;
                            objnewPolicyRemunerationInfo.POLICY_ID.CurrentValue = objGeneralInfo.POLICY_ID;
                            objnewPolicyRemunerationInfo.POLICY_VERSION_ID.CurrentValue = objGeneralInfo.POLICY_VERSION_ID;
                            ClsGeneralInformation.UpdateDefaultBroker(objnewPolicyRemunerationInfo, objGeneralInfo.AGENCY_ID, objOldPolicyInfo.AGENCY_ID);
                        }

                        hidFormSaved.Value = "1";
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("31");

                        hidCustomerID.Value = objGeneralInfo.CUSTOMER_ID.ToString();
                        hidAppID.Value = objGeneralInfo.APP_ID.ToString();
                        hidAppVersionID.Value = objGeneralInfo.APP_VERSION_ID.ToString();
                        hidPolicyID.Value = objGeneralInfo.POLICY_ID.ToString();
                        hidPolicyVersionID.Value = objGeneralInfo.POLICY_VERSION_ID.ToString();
                        hidLOBID.Value = objGeneralInfo.POLICY_LOB;
                        SetCustomerID(hidCustomerID.Value);
                        SetAppID(hidAppID.Value);
                        SetAppVersionID(hidAppVersionID.Value);
                        SetPolicyID(hidPolicyID.Value);
                        SetPolicyVersionID(hidPolicyVersionID.Value);
                        SetLOBID(hidLOBID.Value);

                        SetPolicyCurrency(objGeneralInfo.POLICY_CURRENCY.ToString());
                        //Added by Lalit , Nov 08,2010
                        SetTransaction_Type(objGeneralInfo.TRANSACTION_TYPE.ToString());
                    }
                    else if (intRetVal == -1)
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("18");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }

                    dsPolicy = new DataSet();
                    dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCustomerID.Value == "" ? GetCustomerID() : hidCustomerID.Value), int.Parse(hidPolicyID.Value == "" ? GetPolicyID() : hidPolicyID.Value), int.Parse(hidPolicyVersionID.Value == "" ? GetPolicyVersionID() : hidPolicyVersionID.Value));
                    hidOldData.Value = ClsCommon.GetXMLEncoded(dsPolicy.Tables[0]);
                    if (hidLangCulture.Value.Trim() != cmsbase.DEFAULT_LANG_CULTURE)
                    {
                        hidOldData.Value = FormatXMLDateNode(hidOldData.Value, strDateNodes);
                    }
                    ClsCommon.setPolicyCurrencySymbol(ClsCommon.FetchValueFromXML("CURRENCY_SYMBOL", hidOldData.Value));
                    //Added by Lalit April 04,2011.i-track # 956.
                    //set agency termination on update
                    hidIsTerminated.Value = ClsCommon.FetchValueFromXML("IS_TERMINATED", hidOldData.Value);
                }

                lblMessage.Visible = true;

                strLOB_ID = hidLOBID.Value == "" ? GetLOBID() : hidLOBID.Value;
                switch (strLOB_ID)
                {
                    case LOB_HOME:
                        SetLOBString("HOME");
                        break;
                    case LOB_PRIVATE_PASSENGER:
                        SetLOBString("PPA");
                        break;
                    case LOB_MOTORCYCLE:
                        SetLOBString("MOT");
                        break;
                    case LOB_WATERCRAFT:
                        SetLOBString("WAT");
                        break;
                    case LOB_RENTAL_DWELLING:
                        SetLOBString("RENT");
                        break;
                    case LOB_UMBRELLA:
                        SetLOBString("UMB");
                        break;
                    case LOB_GENERAL_LIABILITY:
                        SetLOBString("GEN");
                        break;
                    case LOB_AVIATION:
                        SetLOBString("AVIATION");
                        break;
                    case MOTOR_VEHICLE:
                        SetLOBString("MOT");
                        break;
                    case FIRE:
                        SetLOBString("FIR");
                        break;
                    case MARINE_CARGO:
                        SetLOBString("MCAR");
                        break;
                    default:
                        SetLOBString(ClsGeneralInformation.GetLOBCodeFromID(int.Parse(strLOB_ID)));
                        break;
                }
                hidCallefroms.Value = GetLOBString();
                CheckBillingPlan();
                //Added By Lalit Nov  25,2010
                string PLAN_ID = ClsCommon.FetchValueFromXML("INSTALL_PLAN_ID", hidOldData.Value);
                string DOWN_PAY_MODE = ClsCommon.FetchValueFromXML("DOWN_PAY_MODE", hidOldData.Value);
                if (PLAN_ID != "" && PLAN_ID != "0")
                {
                    cmbINSTALL_PLAN_ID.SelectedValue = PLAN_ID;
                    hidINSTALL_PLAN_ID.Value = PLAN_ID;
                    hidDOWN_PAY_MODE.Value = DOWN_PAY_MODE;
                }
                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(hidIS_ACTIVE.Value);

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

                //Set Bill Type After saving page :Refer Onchange evnet of Agemcy"
                hidBILL_TYPE_FLAG.Value = "0";
            }
            FillAgency(hidAPP_AGENCY_ID.Value);
           
        }
        #region Commented by Pradeep Kushwaha(20-oct-2010) for implementation of APP_TERMS on Textbox
        //private void cmbAPP_TERMS_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    //change the expiry date
        //    if (hidFormSaved.Value != "4")
        //    {
        //        hidFormSaved.Value = "3";
        //    }
        //    if (cmbAPP_TERMS.SelectedItem != null && cmbAPP_TERMS.SelectedItem.Value != "")
        //    {
        //        int iMonths = 0;
        //        if (cmbAPP_TERMS.SelectedIndex != -1)
        //            iMonths = int.Parse(cmbAPP_TERMS.SelectedItem.Value);
        //        txtAPP_EXPIRATION_DATE.Text = ConvertToDate(txtAPP_EFFECTIVE_DATE.Text).AddMonths(iMonths).ToShortDateString();
        //        //Convert.ToDateTime(txtAPP_EFFECTIVE_DATE.Text).AddMonths(iMonths).ToString("MM/dd/yyyy"); ;
        //    }
        //    CheckBillingPlan(); //Added by Charles on 14-Sep-09 for APP/POL Optimization
        //    GetDefaultInstallmentPlan();
        //}
        #endregion
        private void cmbAPP_LOB_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //if (cmbSTATE_ID.SelectedValue != "" && cmbAPP_LOB.SelectedValue != "")//cmbAPP_LOB.SelectedValue check added by Charles on 25-Aug-09 for APP/POL Optimization
            //{
            //int stateId = Convert.ToInt32(cmbSTATE_ID.SelectedValue);
            hidLOBID.Value = cmbAPP_LOB.SelectedValue;
            try
            {
                int selVal = int.Parse(cmbAPP_LOB.SelectedValue);

                #region Commented by Pradeep Kushwaha(20-oct-2010) for implementation of APP_TERMS on Textbox
                //cmbAPP_TERMS.DataSource = ClsGeneralInformation.GetLOBTerms(int.Parse(cmbAPP_LOB.SelectedValue));
                //cmbAPP_TERMS.DataTextField = "LOOKUP_VALUE_DESC";
                //cmbAPP_TERMS.DataValueField = "LOOKUP_VALUE_CODE";
                //cmbAPP_TERMS.DataBind();
                //cmbAPP_TERMS.Items.Insert(0, "");
                #endregion
                /*
                if (cmbAPP_LOB.SelectedItem != null)
                    if (int.Parse(cmbAPP_LOB.SelectedValue) == 1 || int.Parse(cmbAPP_LOB.SelectedValue) == 6) //Condition added for rental dwelling(Lob Id 6) 
                        policyTR.Visible = true;
                    else
                        policyTR.Visible = false;
                 */

                ClsGeneralInformation.GetBillType(cmbBILL_TYPE, Convert.ToInt32(cmbAPP_LOB.SelectedValue), int.Parse(hidCustomerID.Value), hidAppID.Value, int.Parse(hidAppVersionID.Value), "APP", GetLanguageID());
                SetAgencyBillType();



                #region Commented by Pradeep Kushwaha(20-oct-2010) for implementation of APP_TERMS on Textbox
                //switch (Convert.ToInt32(cmbAPP_LOB.SelectedValue))
                //{
                //    case 1:
                //        cmbAPP_TERMS.SelectedIndex = 1;
                //        break;
                //    case 2:
                //        cmbAPP_TERMS.SelectedIndex = 1;
                //        break;
                //    case 3:
                //        cmbAPP_TERMS.SelectedIndex = 2;
                //        break;
                //    case 4:
                //        cmbAPP_TERMS.SelectedIndex = 1;
                //        break;
                //    case 5:
                //        cmbAPP_TERMS.SelectedIndex = 1;
                //        break;
                //    case 6:
                //        cmbAPP_TERMS.SelectedIndex = 1;
                //        break;
                //    case 7:
                //        cmbAPP_TERMS.SelectedIndex = 1;
                //        break;
                //    default:
                //        cmbAPP_TERMS.SelectedIndex = 1;
                //        break;
                //}
                #endregion

                //Execute the event corresponding to selection of policy term listbox 
                //cmbAPP_TERMS_SelectedIndexChanged(null, null);  Commented by Pradeep Kushwaha(20-oct-2010) for implementation of APP_TERMS on Textbox
                hidFocusFlag.Value = "1";
                //setCarrierInsuredBill();
                //GetDefaultInstallmentPlan(); //Commented by Charles on 25-Aug-09 for APP/POL optimization. Function already called inside cmbAPP_TERMS_SelectedIndexChanged
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            //}
        }

        /*
        private void cmbSTATE_ID_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (cmbSTATE_ID.SelectedIndex != 0 && cmbSTATE_ID.SelectedValue != "")//Added by Charles on 18-Sep-09 for APP/POL Optimization
                {
                    int stateID;
                    ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
                    DataSet dsLOB = new DataSet();
                    stateID = cmbSTATE_ID.SelectedItem == null ? -1 : int.Parse(cmbSTATE_ID.SelectedItem.Value);
                    if (stateID != -1 && stateID != 0)
                    {
                        dsLOB = objGenInfo.GetLOBBYSTATEID(stateID);
                        cmbAPP_LOB.DataSource = dsLOB;
                        cmbAPP_LOB.DataTextField = "LOB_DESC";
                        cmbAPP_LOB.DataValueField = "LOB_ID";
                        cmbAPP_LOB.DataBind();
                        cmbAPP_LOB.Items.Insert(0, new ListItem("", ""));
                        cmbAPP_LOB_SelectedIndexChanged(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }
         */

        #endregion

        private void btnSubmitAnyway_Click(object sender, EventArgs e)
        {
            try
            {
                if (hidIsTerminated.Value == "Y")
                {
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("1014");
                    lblMessage.Visible = true;
                    return;
                }

                string strHTML = "";
                bool valid = false;
                int returnResult = 0;

                gIntCUSTOMER_ID = int.Parse(hidCustomerID.Value);
                gIntAPP_ID = int.Parse(hidAppID.Value);
                gIntAPP_VERSION_ID = int.Parse(hidAppVersionID.Value);
                gstrLobID = hidLOBID.Value;


                string isActive = "";
                isActive = ClsGeneralInformation.CheckIsActivePolicy(gIntCUSTOMER_ID, int.Parse(hidPolicyID.Value == "" ? GetPolicyID() : hidPOLICY_ID.Value), int.Parse(hidPolicyVersionID.Value == "" ? GetPolicyVersionID() : hidPOLICY_VERSION_ID.Value));

                if (isActive != "N")
                {
                    ClsRatingAndUnderwritingRules objVerifyRules = new ClsRatingAndUnderwritingRules(CarrierSystemID);
                    strHTML = objVerifyRules.SubmitAnywayPolVerify(gIntCUSTOMER_ID, int.Parse(hidPolicyID.Value == "" ? GetPolicyID() : hidPOLICY_ID.Value), int.Parse(hidPolicyVersionID.Value == "" ? GetPolicyVersionID() : hidPOLICY_VERSION_ID.Value), hidLOBID.Value == "" ? GetLOBID() : hidLOBID.Value, GetColorScheme(), out  valid);

                    if (valid)
                        ChkReferredRejCase(strHTML, "SUBMITANYWAY", out valid);

                    returnResult = CommonAppToPolicy(valid, strHTML);

                    if (returnResult == -2)
                    {
                        ClsMessages.SetCustomizedXml(GetLanguageCode());
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("974");
                        lblMessage.Visible = true;
                        hidFormSaved.Value = "2";
                        return;
                    }
                    
                    hidIS_ACTIVE.Value = "Y";
                    Generate_Premium();
                }
                else
                {
                    hidIS_ACTIVE.Value = "N";
                }
                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(hidIS_ACTIVE.Value);

                if (returnResult > 0)
                {
                 
                    SetCookieValue();
                }
            }
            catch (Exception ex)
            {
                ClsMessages.SetCustomizedXml(GetLanguageCode());
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("553") + "\n" + ex.Message.ToString();
                lblMessage.Visible = true;
            }
        }

        //Added by kuldeep to calculate erate premium at the time of submit
        protected void Generate_Premium()
        {
            //Cms.Application.Aspx.Quote.Quote objQuote = new Application.Aspx.Quote.Quote("QUOTE_POL", int.Parse(hidCustomerID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), hidLOBID.Value, 0, "false", "0",false);
            //objQuote.Generate_Quote_Details();

        }
    
        private void btnConvertAppToPolicy_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (hidIsTerminated.Value == "Y")
                {
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("1014");
                    lblMessage.Visible = true;
                    return;
                }

                gIntCUSTOMER_ID = int.Parse(hidCustomerID.Value);
                gIntAPP_ID = int.Parse(hidAppID.Value);
                gIntAPP_VERSION_ID = int.Parse(hidAppVersionID.Value);
                gstrLobID = hidLOBID.Value;

                string strSystemID = GetSystemId();
                string strCSSNo = GetColorScheme();
                string strRulesStatus = "0";
                int returnResult = 0;
                bool valid = false;

                string strHTML = "";
                string isActive = "";

                isActive = ClsGeneralInformation.CheckIsActivePolicy(gIntCUSTOMER_ID, int.Parse(hidPolicyID.Value == "" ? GetPolicyID() : hidPOLICY_ID.Value), int.Parse(hidPolicyVersionID.Value == "" ? GetPolicyVersionID() : hidPOLICY_VERSION_ID.Value));

                if (isActive != "N")
                {
                    ClsRatingAndUnderwritingRules objVerifyRules = new ClsRatingAndUnderwritingRules(CarrierSystemID);
                    strHTML = objVerifyRules.VerifyPolicy(gIntCUSTOMER_ID, int.Parse(hidPolicyID.Value == "" ? GetPolicyID() : hidPOLICY_ID.Value), int.Parse(hidPolicyVersionID.Value == "" ? GetPolicyVersionID() : hidPOLICY_VERSION_ID.Value), hidLOBID.Value == "" ? GetLOBID() : hidLOBID.Value, out  valid, strCSSNo, out strRulesStatus);
                    if (valid)
                        ChkReferredRejCase(strHTML, "SUBMIT", out valid);

                    returnResult = CommonAppToPolicy(valid, strHTML);
                    Generate_Premium();
                    objVerifyRules.Insert_ReInsurance_Premium(int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()), 0, 0);
                    
                    //Added by Ruchika on 13-March-2012 for TFS Bug # 3635
                    updatePolAccumulationDetails();

                    if (returnResult == -2)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("974");
                        lblMessage.Visible = true;
                        hidFormSaved.Value = "2";
                        return;
                    }

                    hidIS_ACTIVE.Value = "Y";
                    //Generate_Premium();
                }
                else
                {
                    hidIS_ACTIVE.Value = "N";
                }
                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(hidIS_ACTIVE.Value);

                //txtPOLICY_STATUS.Text = AppStatus;
                if (returnResult > 0)
                {
                    SetCookieValue();
                   
                }
                //Added by kuldeep on 6-feb-2012
                //string JavascriptText = "window.open('/cms/application/Aspx/Quote/Quote.aspx?CALLEDFROM=QUOTE_POL&CUSTOMER_ID=" + hidCustomerID.Value + "&POLICY_ID=" + hidPOLICY_ID.Value + "&POLICY_VERSION_ID=" + hidPOLICY_VERSION_ID.Value + "&LOBID=" + hidLOBID.Value + "&QUOTE_ID=" + "0" + "&ONLYEFFECTIVE=" + "false" + "&SHOW=" + "0" + "','Quote','resizable=yes,scrollbars=yes,left=150,top=50,width=700,height=600');";
                //btnConvertAppToPolicy.Attributes.Add("onClick", JavascriptText + "return false;");
                ////till here 
                //int intPolQuote_ID;
                //Cms.BusinessLayer.BlQuote.ClsGenerateQuote objGenerateQuote = new Cms.BusinessLayer.BlQuote.ClsGenerateQuote(CarrierSystemID);
                //int showDetails = objGenerateQuote.GeneratePolicyQuote(int.Parse(hidCustomerID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), hidLOBID.Value, strCSSNo, out intPolQuote_ID, GetUserId());
                //Cms.Application.Aspx.Quote.Quote objquote=new Cms.Application.Aspx.Quote.Quote();
                //objquote.GenerateQuickQuote();
            }
            catch (Exception ex)
            {
                ClsMessages.SetCustomizedXml(GetLanguageCode());
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("553") + "\n" + ex.Message.ToString();
                lblMessage.Visible = true;
            }
        }


        //Added by Ruchika Chauhan on 13-March-2012 for TFS Bug # 3635
        private void updatePolAccumulationDetails()
        {
            //public DataSet FillAccumulationDetails(int Acc_id, int Policy_id, int Policy_version_id, int Cust_id, , out string FacultativeRI, out string OwnRetention, out string QuotaShare, out string FirstSurplus)
            DataTable dtHolder;
            DataRow rdHolderDetails;
            //string TotalPolicies = "", AccCode = "", TotalSumInsured = "", FacultativeRI = "", OwnRetention = "", QuotaShare = "", FirstSurplus = "";

            ClsGeneralInformation objHolder = new ClsGeneralInformation();
            dtHolder = objHolder.FillAccumulationDetails(int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()), int.Parse(GetCustomerID())).Tables[0];
            if (dtHolder.Rows.Count > 0)
            {
                rdHolderDetails = dtHolder.Rows[0];
                //txtTreaty_cap_limit.Text = rdHolderDetails["TREATY_CAPACITY_LIMIT"].ToString();
                //txtOwn_ret_limit.Text = rdHolderDetails["CRITERIA_VALUE"].ToString();
                //txtAcc_limit_avl.Text = rdHolderDetails["AVAILABLE_LIMIT"].ToString();
                //txtTot_policies.Text = TotalPolicies;
                //txtAcc_code.Text = AccCode;
                //txtTot_sum_insured.Text = TotalSumInsured;
                //txtFacultative_RI.Text = FacultativeRI.ToString();
                //txtGross_ret_SI.Text = (int.Parse(TotalSumInsured) - int.Parse(FacultativeRI)).ToString();
                //txtOwn_ret.Text = OwnRetention.ToString();
                //txtQuota_share.Text = QuotaShare.ToString(); ;
                //txtIst_Surplus.Text = FirstSurplus.ToString();
                //txtOwn_abs_net_ret.Text = (int.Parse(txtGross_ret_SI.Text) - int.Parse(QuotaShare) - int.Parse(FirstSurplus)).ToString();
            }
            //hidFormSaved.Value = "2";

        }


        //private void SetAccumulationReferenceValues()
        //{
        //    hidLOOKUP.Value = ""; //Clearing the lookup field
        //    this.hidHOLDER_NAME.Value = this.txtAcc_ref.Text.Trim();

        //    DataTable dtHolder;
        //    DataRow rdHolderDetails;
        //    string TotalPolicies = "", AccCode = "", TotalSumInsured = "", FacultativeRI = "", OwnRetention = "", QuotaShare = "", FirstSurplus = "";

        //    ClsPolicyAccumulationDetails objHolder = new ClsPolicyAccumulationDetails();
        //    dtHolder = objHolder.FillAccumulationReference(int.Parse(hidDETAIL_TYPE_ID.Value), txtAcc_ref.Text, int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()), out TotalPolicies, out AccCode, out TotalSumInsured).Tables[0];
        //    if (dtHolder.Rows.Count > 0)
        //    {
        //        rdHolderDetails = dtHolder.Rows[0];
        //        txtTreaty_cap_limit.Text = rdHolderDetails["TREATY_CAPACITY_LIMIT"].ToString();
        //        txtOwn_ret_limit.Text = rdHolderDetails["CRITERIA_VALUE"].ToString();
        //        txtAcc_limit_avl.Text = rdHolderDetails["AVAILABLE_LIMIT"].ToString();
        //        txtTot_policies.Text = TotalPolicies;
        //        txtAcc_code.Text = AccCode;
        //        txtTot_sum_insured.Text = TotalSumInsured;
        //        //txtFacultative_RI.Text = FacultativeRI.ToString();
        //        //txtGross_ret_SI.Text = (int.Parse(TotalSumInsured) - int.Parse(FacultativeRI)).ToString();
        //        //txtOwn_ret.Text = OwnRetention.ToString();
        //        //txtQuota_share.Text = QuotaShare.ToString(); ;
        //        //txtIst_Surplus.Text = FirstSurplus.ToString();
        //        //txtOwn_abs_net_ret.Text = (int.Parse(txtGross_ret_SI.Text) - int.Parse(QuotaShare) - int.Parse(FirstSurplus)).ToString();
        //    }
        //    hidFormSaved.Value = "2";
        //}
		

        //Created By Kuldeep on 06-feb-2012
        private void Get_Premium()
        {
          
        }
        private void ChkReferredRejCase(string strRulesHTML, string CalledFrom, out bool ValidXML)
        {
            ValidXML = true;
            try
            {

                System.Xml.XmlDocument objXmlDocument = new XmlDocument();
                strRulesHTML = strRulesHTML.Replace("\t", "");
                strRulesHTML = strRulesHTML.Replace("\r\n", "");
                strRulesHTML = strRulesHTML.Replace("<LINK", "<!-- <LINK");
                strRulesHTML = strRulesHTML.Replace(" rel=\"stylesheet\"> ", "rel=\"stylesheet/\"> -->");
                strRulesHTML = strRulesHTML.Replace("<META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-16\">", "");
                objXmlDocument.LoadXml("<RULEHTML>" + strRulesHTML + "</RULEHTML>");

                //chk for referred

                XmlNodeList objXmlNodeList = objXmlDocument.GetElementsByTagName("ReferedStatus");
                XmlNodeList objXmlNodeListRej = objXmlDocument.GetElementsByTagName("returnValue");
                //XmlNodeList objXmlNodeListRej = objXmlDocument.GetElementsByTagName("returnValue");
                if ((objXmlNodeList != null && objXmlNodeList.Count > 0) || (objXmlNodeListRej != null && objXmlNodeListRej.Count > 0))
                {
                    if (CalledFrom.ToUpper() == "SUBMIT")
                    {
                        if (objXmlNodeList.Item(0).InnerText == "0")
                        {
                            ValidXML = false;
                        }
                    }
                    else if (CalledFrom.ToUpper() == "SUBMITANYWAY")
                    {
                        if (objXmlNodeListRej.Item(0).InnerText == "0")
                        {
                            ValidXML = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Following error occured. \n"
                    + ex.Message + "\n Please try later.";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }


        public int CommonAppToPolicy(bool valid, string strHTML)
        {
            int returnResult = 0;
            bool NbsLaunch = false;
            if (valid)
            {
                int returnUWResult = (new ClsGeneralInformation()).CheckToAssignedUnderWriter(gIntCUSTOMER_ID, int.Parse(hidAppID.Value == "" ? GetAppID() : hidAppID.Value), int.Parse(hidAppVersionID.Value == "" ? GetAppVersionID() : hidAppVersionID.Value), hidLOBID.Value == "" ? GetLOBID() : hidLOBID.Value, int.Parse(GetUserId()), "SUBMIT");
                if (returnUWResult == -2)
                {
                    return returnUWResult;
                }
                //else
                //{

                //}

                //{
                //int iDIV_ID = 0;
                //if (cmbDIV_ID_DEPT_ID_PC_ID.SelectedValue != "")
                //{
                //    string[] strDivDeptPC = cmbDIV_ID_DEPT_ID_PC_ID.SelectedValue.Split('^');
                //    iDIV_ID = Convert.ToInt32(strDivDeptPC[0]);
                //}
                //DateTime dAPP_EFFECTIVE_DATE = ConvertToDate(txtAPP_EFFECTIVE_DATE.Text);
                //string sNewAppPolNumber = ClsGeneralInformation.GenerateAppPolNumber(int.Parse(cmbAPP_LOB.SelectedItem.Value), iDIV_ID, dAPP_EFFECTIVE_DATE, "POL");
                string appNumber = hidPolicyNumber.Value;

                //returnResult = ClsGeneralInformation.SetPolicyStatus(gIntCUSTOMER_ID, int.Parse(hidPolicyID.Value == "" ? GetPolicyID() : hidPOLICY_ID.Value), int.Parse(hidPolicyVersionID.Value == "" ? GetPolicyVersionID() : hidPOLICY_VERSION_ID.Value), "Suspended", int.Parse(GetUserId()), GetLOBID(), sNewAppPolNumber);

                try
                {
                    //returnResult = ClsGeneralInformation.SetPolicyStatus(gIntCUSTOMER_ID, int.Parse(hidPolicyID.Value == "" ? GetPolicyID() : hidPOLICY_ID.Value), int.Parse(hidPolicyVersionID.Value == "" ? GetPolicyVersionID() : hidPOLICY_VERSION_ID.Value), "Suspended", int.Parse(GetUserId()), GetLOBID(), appNumber, objWrapper);
                    int retVal = 0;
                    NbsLaunch = StartNBSProcess(gIntCUSTOMER_ID, int.Parse(hidPolicyID.Value == "" ? GetPolicyID() : hidPOLICY_ID.Value), int.Parse(hidPolicyVersionID.Value == "" ? GetPolicyVersionID() : hidPOLICY_VERSION_ID.Value), GetLOBID(), appNumber, out retVal);



                }
                catch (Exception ex)
                {
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
                    Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                    ExceptionManager.Publish(ex);
                    lblMessage.Text = "Not Converted";
                }
                if (NbsLaunch)//returnResult == 1)
                {
                    Session["LoadedAfterSave"] = "true";
                    ClientScript.RegisterStartupScript(this.GetType(), "LoadPage", "<script>parent.document.location.href='/Cms/Policies/aspx/PolicyTab.aspx?customer_id=" + GetCustomerID() + "&POLICY_ID=" + GetPolicyID() + "&APP_ID=" + GetAppID() + "&APP_VERSION_ID=" + GetAppVersionID() + "&POLICY_VERSION_ID=" + GetPolicyVersionID() + "&POLICY_LOB=" + GetLOBID() + "'</script>");
                }
            }

            //}
            else
            {
                ClientScript.RegisterHiddenField("hidPOLHTML", strHTML);
                ClientScript.RegisterStartupScript(this.GetType(), "ShowVerifiyDialog", "<script>ShowPolicyMsg();</script>");
            }
            if (!NbsLaunch)
                returnResult = 0;
            return returnResult;
        }

        private void SetCookieValue()
        {
            Response.Cookies["LastVisitedTab"].Value = "0";
            Response.Cookies["LastVisitedTab"].Expires = DateTime.Now.Add(new TimeSpan(30, 0, 0, 0, 0));
            //Response.Write("<script>this.parent.document.location.href = '/Cms/Client/Aspx/CustomerManagerIndex.aspx';</script>");
        }

        private void SetAgencyBillType()
        {
            ClsAgency objAgency = new ClsAgency();
            DataSet dsAgency = objAgency.FetchData(int.Parse(hidAPP_AGENCY_ID.Value == "" ? "0" : hidAPP_AGENCY_ID.Value));//Changed to hidAPP_AGENCY_ID.Value by Charles on 21-Aug-09 for APP/POL Optimization
            if (dsAgency != null && dsAgency.Tables[0].Rows.Count > 0)
            {
                string bill_type = dsAgency.Tables[0].Rows[0]["AGENCY_BILL_TYPE"].ToString().Trim();

                for (int index = 0; index < cmbBILL_TYPE.Items.Count; index++)
                {
                    if (cmbBILL_TYPE.Items[index].Value == bill_type)
                    {
                        cmbBILL_TYPE.SelectedIndex = index;
                        break;
                    }
                    else
                    {
                        if (cmbBILL_TYPE.Items[index].Value == "8460" && bill_type == "11150")
                        {
                            cmbBILL_TYPE.SelectedIndex = index;
                            break;
                        }
                    }
                }
            }
        }

        private void HideButtonsOnReject()
        {
            btnSave.Visible = false;
            btnCreateNewVersion.Visible = false;
            btnActivateDeactivate.Visible = false;
            btnVerifyApplication.Visible = false;
            btnConvertAppToPolicy.Visible = false;
            btnSubmitAnyway.Visible = false;
            btnCopy.Visible = true;
        }
        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
                ClsPolicyInfo objPolicyInfo = new ClsPolicyInfo();


                objPolicyInfo.CUSTOMER_ID = int.Parse(hidCustomerID.Value);
                objPolicyInfo.POLICY_VERSION_ID = int.Parse(hidPolicyVersionID.Value == "" ? GetPolicyVersionID() : hidPOLICY_VERSION_ID.Value);
                objPolicyInfo.POLICY_ID = int.Parse(hidPolicyID.Value == "" ? GetPolicyID() : hidPOLICY_ID.Value);
                objPolicyInfo.CREATED_BY = int.Parse(GetUserId());

                String CalledFrom = "APP";

                int retvalue = objGeneralInformation.RejectAppPol(objPolicyInfo, CalledFrom);

                if (retvalue > 0)
                {
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("1100");
                    lblMessage.Visible = true;
                    hidFormSaved.Value = "1";
                    hidRefresh.Value = "R";
                    hidApplicationStatus.Value = "Rejected";
                    btnReject.Visible = false;
                    HideButtonsOnReject();

                }

            }
            catch (Exception ex)
            {
                ClsMessages.SetCustomizedXml(GetLanguageCode());
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1097") + "\n" + ex.Message.ToString();
                lblMessage.Visible = true;
            }
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet AjaxFetchInfo(int iLOB_ID)
        {
            DataSet ds = null;
            objClsStates = new ClsStates();
            string LANG_ID = GetLanguageID();
            try
            {
                ds = new DataSet();

                DataTable dt1 = new DataTable();
                try
                {
                    dt1 = ClsEndorsmentDetails.GetSUBLOBs(iLOB_ID.ToString(), LANG_ID).Tables[0];
                }
                catch
                { }
                ds.Tables.Add(dt1.Copy());
                ds.Tables[0].TableName = "SUBLOBS";

                DataTable dt2 = new DataTable();
                try
                {
                    dt2 = ClsGeneralInformation.GetBillType(iLOB_ID, "POL");
                }
                catch
                { }
                ds.Tables.Add(dt2.Copy());
                ds.Tables[1].TableName = "BILLTYPE";

                DataTable dt3 = new DataTable();
                try
                {
                    dt3 = ClsGeneralInformation.GetLOBTerms(iLOB_ID).Tables[0];
                }

                catch
                { }
                ds.Tables.Add(dt3.Copy());
                ds.Tables[2].TableName = "APPTERMS";
                DataTable dt4 = new DataTable();
                try
                {
                    dt4 = objClsStates.PopLateAssignedState(iLOB_ID).Tables[0];
                }

                catch
                {

                }
                ds.Tables.Add(dt4.Copy());
                ds.Tables[3].TableName = "STATE";

                return ds;
            }
            catch
            {
                return null;
            }
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet GetInstallPlan(string sAPP_TERMS, String sLobid, string Tran_Type)
        {

            try
            {
                //if(sLobid!="" && GetProduct_Type(int.Parse(sLobid))== MASTER_POLICY)
                if (sLobid != "" && GetTransaction_Type(Tran_Type) == MASTER_POLICY)// && (sLobid == "17" || sLobid == "18" || sLobid == "21" || sLobid == "34"))
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    dt = ClsInstallmentInfo.GetApplicableInstallmentPlans(0).Tables[0].Select("PLAN_TYPE in('MAUTO','MMANNUAL')").CopyToDataTable<DataRow>();
                    ds.Tables.Add(dt);
                    return ds;
                }
                else
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    dt = ClsInstallmentInfo.GetApplicableInstallmentPlans(0).Tables[0].Select("PLAN_TYPE NOT IN ('MAUTO','MMANNUAL') OR PLAN_TYPE IS NULL").CopyToDataTable<DataRow>(); ;
                    ds.Tables.Add(dt);
                    return ds;
                }
            }
            catch
            {
                return null;
            }
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public string GetExpDate(string sAPP_TERMS, string sAPP_EFFECTIVE_DATE)
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

        //Added by Ruchika Chauhan on 7-Dec-2011 for TFS # 1211
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public string GetTerm(string sInception, string sExpiry)
        {
            try
            {
                SetCultureThread(GetLanguageCode());
                DateTime d1 = ConvertToDate(sInception);
                DateTime d2 = ConvertToDate(sExpiry);
                TimeSpan  t = (d2).Subtract(d1);


                return t.TotalDays.ToString();
            }
            catch
            {
                return "";
            }
        }

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
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public string GetDefaultInstallmentPlanAjax(string sAPP_TERMS, string sLOB_ID)
        {
            Cms.BusinessLayer.BlApplication.ClsGeneralInformation objPlan = new
                 Cms.BusinessLayer.BlApplication.ClsGeneralInformation();

            if (sAPP_TERMS != null && sAPP_TERMS != "")
            {
                int PlanID = objPlan.GetDefaultPlanId(int.Parse(sAPP_TERMS), int.Parse(sLOB_ID));

                if (PlanID != 0)
                {
                    return PlanID.ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
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

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet AjaxTranType()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                IList Ilist = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("TTYP");
                dt = ConvertiListToTable(Ilist);
                ds.Tables.Add(dt);
                return ds;
            }
            catch
            {
                return null;
            }

        }


        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public static string GetProductType(string LobId)
        {
            string productType = "";
            DataSet ds = new DataSet();
            try
            {
                ds = ClsGeneralInformation.GetLOBInfo(int.Parse(LobId));  //Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("TTYP");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    productType = ds.Tables[0].Rows[0]["PRODUCT_TYPE"].ToString();

                return productType;
            }
            catch
            {
                return null;
            }

        }

        private static DataTable ConvertiListToTable(IList ilist)
        {
            DataTable dt = new DataTable();
            System.Reflection.PropertyInfo[] propInfo = ilist[0].GetType().GetProperties();
            dt.TableName = ilist[0].GetType().Name;
            for (int row = 0; row < ilist.Count - 1; row++)
            {
                dt.Columns.Add(propInfo[row].Name, typeof(string));
                //dt.Columns.Add("LOOKUP_VALUE_DESC", typeof(string));
            }
            DataRow dr;

            for (int row = 0; row < ilist.Count; row++)
            {
                dr = dt.NewRow();
                object tempObject = ilist[row];
                object LookupID = propInfo[0].GetValue(tempObject, null);
                object LookupDesc = propInfo[1].GetValue(tempObject, null);
                object LookupCode = propInfo[2].GetValue(tempObject, null);
                if (LookupID != null)
                    dr[0] = LookupID.ToString();
                if (LookupDesc != null)
                    dr[1] = LookupDesc.ToString();
                if (LookupDesc != null)
                    dr[2] = LookupCode.ToString();
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public bool StartNBSProcess(int iCustomerId, int iPolId, int iPolVersionId, string strLOB_ID, String AppNumber, out int iretval)
        {
            bool retval = false;
            iretval = 0;
            //string StartNewBusiness="";
            Cms.BusinessLayer.BlProcess.ClsNewBusinessProcess objProcess = new Cms.BusinessLayer.BlProcess.ClsNewBusinessProcess();
            ClsGeneralInformation objGenInfo = new ClsGeneralInformation();
            try
            {

                //StartNewBusiness = objGenInfo.CheckForStartNBSProcess(iCustomerId ,iPolId, iPolVersionId);
                //if (StartNewBusiness.ToUpper() == "Y")
                //{

                objProcess.BeginTransaction();
                retval = objProcess.SubmitAppStartNBSProcess(iCustomerId, iPolId, iPolVersionId, strLOB_ID, int.Parse(GetUserId()), AppNumber, out iretval);
                objProcess.CommitTransaction();
                //}
                return retval;
            }
            catch (Exception exp)
            {
                ClsMessages.SetCustomizedXml(GetLanguageCode());
                lblMessage.Visible = true;
                lblMessage.Text = ClsMessages.FetchGeneralMessage("1342") + " : " + exp.Message.ToString(); ;
                objProcess.RollbackTransaction();
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exp);
                //ExceptionManager.Publish(exp);
                return false;
                //throw (exp);
            }

        }



    }
}
