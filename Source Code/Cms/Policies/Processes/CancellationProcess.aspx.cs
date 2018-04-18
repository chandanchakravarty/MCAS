/******************************************************************************************
<Author				: -  Ashwini
<Start Date			: -	 20 Dec.,2005
<End Date			: -	 
<Description		: -  Class for Policy Cancellation Process.
<Review Date		: - 
<Reviewed By		: - 	

Modification History
<Modified By		: - Vijay Arora
<Modified Date		: - 23-12-2005
<Purpose			: - Added the Process Launch, Rollback and Complted functionality. 
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
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.CmsWeb.Controls;
using System.Xml;
using Cms.Model.Policy;
using Cms.BusinessLayer.BlProcess;
using Cms.Model.Policy.Process;


namespace Cms.Policies.Processes
{
    /// <summary>
    /// Summary description for CancellationProcess.
    /// </summary>
    public class CancellationProcess : Cms.Policies.policiesbase
    {
        #region "Controls"
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Label capCOMPLETED_DATETIME;
        protected System.Web.UI.WebControls.Label capREQUESTED_BY;
        protected System.Web.UI.WebControls.DropDownList cmbREQUESTED_BY;
        protected System.Web.UI.WebControls.Label capCANCELLATION_OPTION;
        protected System.Web.UI.WebControls.DropDownList cmbCANCELLATION_OPTION;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCANCELLATION_OPTION;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCANCELLATION_TYPE;
        protected System.Web.UI.WebControls.Label capCANCELLATION_TYPE;
        protected System.Web.UI.WebControls.DropDownList cmbCANCELLATION_TYPE;
        protected System.Web.UI.WebControls.Label capREASON;
        protected System.Web.UI.WebControls.DropDownList cmbREASON;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvREASON;
        protected System.Web.UI.WebControls.Label capOTHER_REASON;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvOTHER_REASON;
        protected System.Web.UI.WebControls.Label capRETURN_PREMIUM;
        protected System.Web.UI.WebControls.TextBox txtRETURN_PREMIUM;
        //protected System.Web.UI.WebControls.TextBox txtPAST_DUE_PREMIUM;
        protected Cms.CmsWeb.Controls.CmsButton btnBack_To_Customer_Assistant;
        protected Cms.CmsWeb.Controls.CmsButton btnBack_To_Search;
        //		protected Cms.CmsWeb.Controls.CmsButton btnGenerate_cancellation_notice_or_Memo;
        protected Cms.CmsWeb.Controls.CmsButton btnGenerate_premium_Notice;
        protected Cms.CmsWeb.Controls.CmsButton btnPrint_Preview;
        //		protected Cms.CmsWeb.Controls.CmsButton btnCommit_To_Spool;
        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected Cms.CmsWeb.Controls.CmsButton btnSave_Send_Notice;
        protected Cms.CmsWeb.Controls.CmsButton btnRollBack;
        protected Cms.CmsWeb.Controls.CmsButton btnComplete;
        protected Cms.CmsWeb.Controls.CmsButton btnCalculate_Return_premium;
        protected Cms.CmsWeb.Controls.CmsButton btnPolicyDetails;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidEffDateForCompanyRequest;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCancelEffDateXML;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCANCELLATION_OPTION;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_PAYMENT_CODE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCANCEL_OPTIONS_XML;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCANCEL_RULE_XML;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidUNDERWRITER;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_CODE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAGENCY_CODE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidADD_INT_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidEFFECTIVE_DATE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidEFFECTIVE_DATETIME;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidROW_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_TIME;
        protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_TIME;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPROCESS_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidBILL_TYPE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDisplayBody;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCANCELLATION_DATE_EQUITY;
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected System.Web.UI.WebControls.Label capEFFECTIVE_DATETIME;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_DATE;
        protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_DATETIME;
        protected System.Web.UI.WebControls.Label capEFFECTIVE_TIME;
        protected System.Web.UI.WebControls.Label capAGENTPHONENO;
        protected System.Web.UI.WebControls.HyperLink hlkEFFECTIVE_DATE;
        protected System.Web.UI.WebControls.TextBox txtAGENT_PHONE_NUMBER;
        //protected System.Web.UI.WebControls.RequiredFieldValidator rfvAGENT_PHONE_NUMBER;
        protected Cms.CmsWeb.WebControls.PolicyTop cltPolicyTop;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_MINUTE;
        protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_HOUR;
        protected System.Web.UI.WebControls.Label lblEFFECTIVE_HOUR;
        protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_MINUTE;
        protected System.Web.UI.WebControls.Label Label1;
        protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_SEC;
        protected System.Web.UI.WebControls.Label Label2;
        protected System.Web.UI.WebControls.DropDownList cmbMERIDIEM;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_HOUR;
        protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_HOUR;
        protected System.Web.UI.WebControls.RegularExpressionValidator regEFFECTIVE_MINUTE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_SEC;
        protected System.Web.UI.WebControls.RegularExpressionValidator regEFFECTIVE_SEC;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvMERIDIEM;
        protected System.Web.UI.WebControls.RangeValidator rnvEFFECTIVE_HOUR;
        protected System.Web.UI.WebControls.RangeValidator rnvtEFFECTIVE_MINUTE;
        protected System.Web.UI.WebControls.RangeValidator rnvEFFECTIVE_SEC;
        protected System.Web.UI.WebControls.Label lblAGENT_PHONE_NUMBER;
        System.Resources.ResourceManager objResourceMgr;
        //		protected System.Web.UI.WebControls.Label lblOTHER_REASON;
        protected System.Web.UI.WebControls.TextBox txtOTHER_REASON;
        protected System.Web.UI.WebControls.CustomValidator csvOTHER_REASON;
        protected System.Web.UI.WebControls.Label capPRINTING_OPTIONS;
        protected System.Web.UI.WebControls.CheckBox chkPRINTING_OPTIONS;
        protected System.Web.UI.WebControls.Label capINSURED;
        protected System.Web.UI.WebControls.DropDownList cmbINSURED;

        protected System.Web.UI.WebControls.Label capAGENCY_PRINT;
        protected System.Web.UI.WebControls.DropDownList cmbAGENCY_PRINT;
        protected System.Web.UI.WebControls.Label capADD_INT;
        protected System.Web.UI.WebControls.DropDownList cmbADD_INT;
        protected System.Web.UI.WebControls.Label capSEND_ALL;
        protected System.Web.UI.WebControls.CheckBox chkSEND_ALL;
        protected System.Web.UI.WebControls.Label capUnassignLossCodes;
        protected System.Web.UI.WebControls.Label capAssignedLossCodes;
        protected System.Web.UI.WebControls.ListBox cmbUnAssignAddInt;
        protected System.Web.UI.WebControls.ListBox cmbAssignAddInt;
        protected System.Web.UI.HtmlControls.HtmlInputButton btnAssignLossCodes;
        protected System.Web.UI.HtmlControls.HtmlInputButton btnUnAssignLossCodes;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDateDifference;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOL_INCEPTION_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revRETURN_PREMIUM;
        protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden1;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCANCELLATION_OPTION_DB;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_CANCELLATION_OPTION_EXISTS;
        protected System.Web.UI.WebControls.RangeValidator rngEFFECTIVE_DATE;
        protected System.Web.UI.WebControls.CheckBox chkINCLUDE_REASON_DESC;
        protected System.Web.UI.WebControls.Label capINCLUDE_REASON_DESC;
        protected System.Web.UI.WebControls.Label capLabel;
        protected System.Web.UI.WebControls.Label capHeader;
        protected System.Web.UI.WebControls.Label capCancel;
        protected System.Web.UI.WebControls.Label lblPrinting;
        protected System.Web.UI.WebControls.Label lblAdditional;

        bool btnSaveFlag;
        bool btnCommitFlag;
        ClsCancellationProcess objClsCancellationProcess = new ClsCancellationProcess();
        #endregion

        public DateTime APP_EFFECTIVE_DATE, APP_EXPIRATION_DATE;
        //protected System.Web.UI.WebControls.Label lblMessage;  //ashish
        protected System.Web.UI.WebControls.Label capENDORSEMENT_DATE;//ashish
        protected System.Web.UI.WebControls.Label capENDORSEMENT_TYPE; //ashish
        protected System.Web.UI.WebControls.DropDownList cmbENDORSEMENT_TYPE; //ashish
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvENDORSEMENT_TYPE; //ashish
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEW_POLICY_VERSION_ID;
        protected Cms.CmsWeb.Controls.CmsButton btnGenerate_cancellation_notice_or_Memo;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_CURRENT_STATUS;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_PREVIOUS_STATUS;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCANCELLATION_NOTICE_SENT;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidREQUESTED_BY;
        protected System.Web.UI.WebControls.CustomValidator csvCANCELLATION_TYPE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidINSURED;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidGRACE_PERIOD;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidNOTICE_CLICK;
        protected Cms.CmsWeb.Controls.CmsButton btnCommitInProgress;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_EXPIRATION_DATE; //#4068
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_EFFECTIVE_DATE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidRETURN_PREMIUM;
        string strPOL_EFF_DATE = "", strPOL_EXP_DATE = "";
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidEFF_DAT;


        #region "Page_Load"
        private void Page_Load(object sender, System.EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(CancellationProcess));
            numberFormatInfo.NumberDecimalDigits = 0;
            hidEFFECTIVE_DATE.Value = DateTime.Now.AddDays(15).ToString("MM/dd/yyyy");
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            //Setting the screen id
            ((cmsbase)this).ScreenId = "5000_23";
            hlkEFFECTIVE_DATE.Attributes.Add("OnClick", "fPopCalendar(document.PROCESS_CANCELLATION.txtEFFECTIVE_DATETIME,document.PROCESS_CANCELLATION.txtEFFECTIVE_DATETIME)");
            //this.cmbREASON.Attributes.Add("onchange","javascript:Check();");			
            cmbCANCELLATION_TYPE.Attributes.Add("onchange", "javascript:cmbCANCELLATION_TYPE_Change(false);");
            //cmbPRINT_COMMENTS.Attributes.Add("onchange","javascript:CommentEnable();");
            cmbREQUESTED_BY.Attributes.Add("onchange", "javascript:DisplayAgentPhoneNo();");
            btnPolicyDetails.Attributes.Add("onclick", "javascript:return ShowDetailsPolicy();");
            btnReset.Attributes.Add("onclick", "javascript:return formReset();");
            btnGenerate_cancellation_notice_or_Memo.Attributes.Add("onclick", "javascript:return SetEffectiveDateOnSendCancellation();");
            btnRollBack.Attributes.Add("onclick", "javascript:HideShowCommit();");
            cmbMERIDIEM.Attributes.Add("onchange", "javascript:disableHourValidator();");//Added by Charles on 7-Sep-09 for Itrack 6323
            //Setting the security xml of cmb button
            SetButtonsSecurityXML();
            cltPolicyTop.UseRequestVariables = false;
            capCancel.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1408");
            if (!Page.IsPostBack)
            {
                //Fetching the query string values
                GetQueryString();

                //Setting the properties of validation controls
                SetValidators();
                SetCaptions();
                PopulateCombobox();
                //Setting the policy top controls setting
                SetPolicyTopControl();
                capLabel.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
                capHeader.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1304");
                ClsMessages.SetCustomizedXml(GetLanguageCode());
                //cmbCANCELLATION_OPTION.Attributes.Add("onChange","javascript: if(this.selectedIndex>0) document.getElementById('hidCANCELLATION_OPTION').value=this.options[this.selectedIndex].value;");
                cmbCANCELLATION_OPTION.Attributes.Add("onChange", "onOptionChange();");
                LoadCancellationRuleXml();
                // fill dropdowns
                GetDropdownData();
                //GetCancelEffDateXml();
                txtRETURN_PREMIUM.Attributes.Add("onBlur", "javascript:this.value=formatCurrency(this.value);");
                txtEFFECTIVE_DATETIME.Attributes.Add("onBlur", "javascript:return CallService();");
                cmbADD_INT.Attributes.Add("onChange", "javascript:return cmbADD_INT_Change();");
                chkSEND_ALL.Attributes.Add("onClick", "javascript: chkSEND_ALL_Change();");
                btnSave.Attributes.Add("onClick", "javascript: GetAssignAddInt();Page_ClientValidate();return Page_IsValid;");
                btnComplete.Attributes.Add("onClick", "javascript: GetAssignAddInt();HideShowCommitInProgress();Page_ClientValidate();return Page_IsValid;");

                if (hidPROCESS_ID.Value == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_CANCELLATION_PROCESS.ToString())
                {
                    objClsCancellationProcess.BeginTransaction();
                    if (objClsCancellationProcess.CheckProcessEligibility(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidPROCESS_ID.Value)) == 1)
                    {
                        //Starting the process
                        SetDefaultValues();
                        StartProcess();
                    }
                    else
                        //Populating the currently executing process information
                        PopulateProcessInfo();
                    objClsCancellationProcess.CommitTransaction();
                }
                else
                {
                    //Populating the currently executing process information
                    PopulateProcessInfo();
                }

                //Populating other data
                //				PopulateOtherInfo();

            }
        }
        #endregion

        private void SetDefaultValues()
        {
            chkSEND_ALL.Checked = true;
            hidADD_INT_ID.Value = "";
            for (int i = 0; i < cmbUnAssignAddInt.Items.Count; i++)
            {
                hidADD_INT_ID.Value += cmbUnAssignAddInt.Items[i].Value.ToString() + "~";
            }
            hidADD_INT_ID.Value = hidADD_INT_ID.Value.TrimEnd('~');
        }


        /// <summary>
        /// Fill the dropdowns 
        /// </summary>
        /// <returns></returns>
        private void GetDropdownData()
        {


            cmbMERIDIEM.Items.Insert(0, "AM");
            cmbMERIDIEM.Items[0].Value = "0";
            cmbMERIDIEM.Items.Insert(1, "PM");
            cmbMERIDIEM.Items[1].Value = "1";

            //For Standard Letter 
            /*cmbSTD_LETTER_REQD.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
            cmbSTD_LETTER_REQD.DataTextField="LookupDesc";
            cmbSTD_LETTER_REQD.DataValueField="LookupID";
            cmbSTD_LETTER_REQD.DataBind();*/

            //For Customized Letter 
            /*cmbCUSTOM_LETTER_REQD.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
            cmbCUSTOM_LETTER_REQD.DataTextField="LookupDesc";
            cmbCUSTOM_LETTER_REQD.DataValueField="LookupID";
            cmbCUSTOM_LETTER_REQD.DataBind();*/
            //SelectComboValue(cmbCUSTOM_LETTER_REQD,((int) enumCustomized_Letter_Required .Yes).ToString());

            DataTable dtOptions = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CANTYP", null, "S");

            cmbCANCELLATION_TYPE.DataSource = dtOptions;//Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CANTYP");
            cmbCANCELLATION_TYPE.DataTextField = "LookupDesc";
            cmbCANCELLATION_TYPE.DataValueField = "LookupID";
            cmbCANCELLATION_TYPE.DataBind();
            cmbCANCELLATION_TYPE.Items.Insert(0, "");

            //			ListItem iListItem = null;
            //			iListItem = cmbCANCELLATION_TYPE.Items.FindByValue(((int)ClsPolicyProcess.enumPROC_CANCEL_TYPE.AGENCY_TERMINATE_NOTIFICATION).ToString());
            //			if(iListItem!=null)
            //				cmbCANCELLATION_TYPE.Items.Remove(iListItem);
            //
            //			iListItem = null;
            //			iListItem = cmbCANCELLATION_TYPE.Items.FindByValue(((int)ClsPolicyProcess.enumPROC_CANCEL_TYPE.AGENCY_TERMINATE_NO_NOTIFICATION).ToString());
            //			if(iListItem!=null)
            //				cmbCANCELLATION_TYPE.Items.Remove(iListItem);

            ClsCommon.RemoveOptionFromDropdownByValue(cmbCANCELLATION_TYPE, ((int)clsprocess.enumPROC_CANCEL_TYPE.AGENCY_TERMINATE_NOTIFICATION).ToString());
            ClsCommon.RemoveOptionFromDropdownByValue(cmbCANCELLATION_TYPE, ((int)clsprocess.enumPROC_CANCEL_TYPE.AGENCY_TERMINATE_NO_NOTIFICATION).ToString());




            DataTable dtCancelOptions = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CANOPT", null, "S");
            if (dtCancelOptions != null)
            {
                cmbCANCELLATION_OPTION.DataSource = dtCancelOptions.DataSet.Tables[0];
                cmbCANCELLATION_OPTION.DataTextField = "LookupDesc";
                cmbCANCELLATION_OPTION.DataValueField = "LookupID";
                cmbCANCELLATION_OPTION.DataBind();
                ClsCommon.RemoveOptionFromDropdownByValue(cmbCANCELLATION_OPTION, ((int)clsprocess.enumCANCELLATION_OPTIONS.NOT_APPLICABLE).ToString());
                hidCANCEL_OPTIONS_XML.Value = dtCancelOptions.DataSet.GetXml();
            }
            ClsCancellationProcess objCancelProcess = new ClsCancellationProcess();
            DataTable dtCancelKeys = objCancelProcess.GetKeysForCancellation(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value));
            if (dtCancelKeys != null && dtCancelKeys.Rows.Count > 0 && dtCancelKeys.Rows[0]["PROCESS_PAYMENT_CODE"] != null && dtCancelKeys.Rows[0]["PROCESS_PAYMENT_CODE"].ToString() != "")
                hidPOLICY_PAYMENT_CODE.Value = dtCancelKeys.Rows[0]["PROCESS_PAYMENT_CODE"].ToString();

            cmbREASON.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CNCLRN");
            cmbREASON.DataTextField = "LookupDesc";
            cmbREASON.DataValueField = "LookupID";
            cmbREASON.DataBind();
            cmbREASON.Items.Insert(0, "");
            //			cmbREASON.Items[0].Value = "0";

            /*
            cmbREQUESTED_BY.Items.Insert(0,"");
            cmbREQUESTED_BY.Items[0].Value = "0";
            cmbREQUESTED_BY.Items.Insert(1,"Agent");
            cmbREQUESTED_BY.Items[1].Value = "1";
            cmbREQUESTED_BY.Items.Insert(2,"Company");
            cmbREQUESTED_BY.Items[2].Value = "2";
            cmbREQUESTED_BY.Items.Insert(3,"Customer");
            cmbREQUESTED_BY.Items[3].Value = "3";
            */
            cmbREQUESTED_BY.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("REQCD");
            cmbREQUESTED_BY.DataTextField = "LookupDesc";
            cmbREQUESTED_BY.DataValueField = "LookupID";
            cmbREQUESTED_BY.DataBind();
            cmbREQUESTED_BY.Items.Insert(0, "");
            cmbREQUESTED_BY.Items[0].Value = "0";
            ListItem iListItemReq = null;
            iListItemReq = cmbREQUESTED_BY.Items.FindByValue("6497"); //remove Other option
            if (iListItemReq != null)
                cmbREQUESTED_BY.Items.Remove(iListItemReq);
            iListItemReq = cmbREQUESTED_BY.Items.FindByValue("14152"); //remove Agency option
            if (iListItemReq != null)
                cmbREQUESTED_BY.Items.Remove(iListItemReq);
            iListItemReq = cmbREQUESTED_BY.Items.FindByValue("14153"); //remove Wolverine option
            if (iListItemReq != null)
                cmbREQUESTED_BY.Items.Remove(iListItemReq);


            /*cmbPRINT_COMMENTS.Items.Insert(0,"No");
            cmbPRINT_COMMENTS.Items[0].Value = "0";
            cmbPRINT_COMMENTS.Items.Insert(1,"Yes");
            cmbPRINT_COMMENTS.Items[1].Value = "1";*/

            IList ListSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PRNCAN");


            //iListItem = null;			

            cmbINSURED.DataSource = ListSource;
            cmbINSURED.DataTextField = "LookupDesc";
            cmbINSURED.DataValueField = "LookupID";
            cmbINSURED.DataBind();

            /*iListItem = null;
            iListItem = cmbINSURED.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.DOWNLOAD).ToString());
            if(iListItem!=null)
                cmbINSURED.Items.Remove(iListItem);

            iListItem = null;
            iListItem = cmbINSURED.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.MICHIGAN_MAILERS).ToString());
            if(iListItem!=null)
                cmbINSURED.Items.Remove(iListItem);

            iListItem = null;
            iListItem = cmbINSURED.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString());
            if(iListItem!=null)
                cmbINSURED.Items.Remove(iListItem);*/

            ClsCommon.RemoveOptionFromDropdownByValue(cmbINSURED, ((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.DOWNLOAD).ToString());
            //			if (hidSTATE_ID.Value!="22") comented on 3 april 2008 as asked by Rajan
            //			   ClsCommon.RemoveOptionFromDropdownByValue(cmbINSURED,((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.MICHIGAN_MAILERS).ToString());

            //ClsCommon.RemoveOptionFromDropdownByValue(cmbINSURED,((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString());

            cmbADD_INT.DataSource = ListSource;
            cmbADD_INT.DataTextField = "LookupDesc";
            cmbADD_INT.DataValueField = "LookupID";
            cmbADD_INT.DataBind();

            /*iListItem = null;
            iListItem = cmbADD_INT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.DOWNLOAD).ToString());
            if(iListItem!=null)
                cmbADD_INT.Items.Remove(iListItem);

            iListItem = null;
            iListItem = cmbADD_INT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.MICHIGAN_MAILERS).ToString());
            if(iListItem!=null)
                cmbADD_INT.Items.Remove(iListItem);*/

            ClsCommon.RemoveOptionFromDropdownByValue(cmbADD_INT, ((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.DOWNLOAD).ToString());
            //			if (hidSTATE_ID.Value!="22")  comented on 3 april 2008 as asked by Rajan
            //				ClsCommon.RemoveOptionFromDropdownByValue(cmbADD_INT,((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.MICHIGAN_MAILERS).ToString());
            //ClsCommon.RemoveOptionFromDropdownByValue(cmbADD_INT,((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString());

            cmbAGENCY_PRINT.DataSource = ListSource;
            cmbAGENCY_PRINT.DataTextField = "LookupDesc";
            cmbAGENCY_PRINT.DataValueField = "LookupID";
            cmbAGENCY_PRINT.DataBind();

            /*iListItem = null;
            iListItem = cmbAGENCY_PRINT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.DOWNLOAD).ToString());
            if(iListItem!=null)
                cmbAGENCY_PRINT.Items.Remove(iListItem);*/



            //ClsCommon.RemoveOptionFromDropdownByValue(cmbAGENCY_PRINT,((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.DOWNLOAD).ToString());
            //			if (hidSTATE_ID.Value!="22") comented on 3 april 2008 as asked by Rajan
            //			   ClsCommon.RemoveOptionFromDropdownByValue(cmbAGENCY_PRINT,((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.MICHIGAN_MAILERS).ToString());
            ClsCommon.RemoveOptionFromDropdownByValue(cmbAGENCY_PRINT, ((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString());


            DataTable dtAddIntList = null;
            Cms.BusinessLayer.BlApplication.ClsAdditionalInterest objAdditionalInterest = new Cms.BusinessLayer.BlApplication.ClsAdditionalInterest();
            dtAddIntList = objAdditionalInterest.GetAdditionalInterestList(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidLOB_ID.Value));
            if (dtAddIntList != null && dtAddIntList.Rows.Count > 0)
            {
                cmbUnAssignAddInt.DataSource = dtAddIntList;
                cmbUnAssignAddInt.DataTextField = "ADD_INT_DETAILS";
                cmbUnAssignAddInt.DataValueField = "ADD_INT_ID";
                cmbUnAssignAddInt.DataBind();
            }
        }

        //Load the cancellation effective date XML 
        private void GetCancelEffDateXml()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                TimeSpan ts = System.DateTime.Now.Subtract(Convert.ToDateTime(hidPOL_INCEPTION_DATE.Value));
                hidDateDifference.Value = ts.Days.ToString();
                int CancellationOption = (int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.COMPANY_REQUEST;
                string filePath = System.Web.HttpContext.Current.Server.MapPath(Request.ApplicationPath + "/cmsweb/support/CancellationEffDateRule.xml");
                xmlDoc.Load(filePath);
                if (xmlDoc != null)
                {
                    XmlNode xNode = xmlDoc.SelectSingleNode("/CancellationEffectiveDateMatrix/CancellationType[@Id='" + CancellationOption + "']/LOB[@Id='" + hidLOB_ID.Value + "']/STATE[@Id='" + hidSTATE_ID.Value + "']");
                    int DateDiff = int.Parse(hidDateDifference.Value);
                    int DayDiffValue = 0;
                    if (xNode != null)
                        DayDiffValue = int.Parse(xNode.SelectSingleNode("DayDiff").Attributes.Item(0).Value);

                    int DaysToAdd = 0;
                    string OperatorValue = "";
                    if (DayDiffValue == 0)
                        OperatorValue = "NotApplicable";
                    else
                    {
                        if (DateDiff > DayDiffValue)
                            OperatorValue = "GreaterThan";
                        else
                            OperatorValue = "LessThanEqual";
                    }
                    if (xNode != null)
                        DaysToAdd = int.Parse(xNode.SelectSingleNode("DayDiff[@Diff='" + DayDiffValue + "']/Operator[@Value='" + OperatorValue + "']").Attributes.Item(2).Value);
                    hidEffDateForCompanyRequest.Value = System.DateTime.Now.AddDays(DaysToAdd).Date.ToShortDateString(); ;


                }
                hidCANCELLATION_DATE_EQUITY.Value = ClsPolicyProcess.GetCancellationDateForEquity(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value)).ToString();
                hidGRACE_PERIOD.Value = ClsPolicyProcess.GetAgencyGracePeriod(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value)).ToString();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
            }
        }

        /// <summary>
        /// Retreives the values from form and populates the model object
        /// </summary>
        /// <returns>Model object of ClsProcessInfo type</returns>
        private Cms.Model.Policy.Process.ClsProcessInfo GetFormValues()
        {

            Cms.Model.Policy.Process.ClsProcessInfo objClsProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();

            objClsProcessInfo.POLICY_ID = int.Parse(hidPOLICY_ID.Value);
            objClsProcessInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
            objClsProcessInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
            objClsProcessInfo.PROCESS_ID = POLICY_CANCELLATION_PROCESS;
            objClsProcessInfo.NEW_CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
            objClsProcessInfo.NEW_POLICY_ID = int.Parse(hidPOLICY_ID.Value);
            if (hidNEW_POLICY_VERSION_ID.Value != "" && hidNEW_POLICY_VERSION_ID.Value != "0")
                objClsProcessInfo.NEW_POLICY_VERSION_ID = int.Parse(hidNEW_POLICY_VERSION_ID.Value);
            else
                objClsProcessInfo.NEW_POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
            objClsProcessInfo.POLICY_CURRENT_STATUS = objClsCancellationProcess.GetPolicyStatus(objClsProcessInfo.CUSTOMER_ID, objClsProcessInfo.POLICY_ID, objClsProcessInfo.NEW_POLICY_VERSION_ID);//GetPolicyStatus();
            objClsProcessInfo.POLICY_PREVIOUS_STATUS = objClsCancellationProcess.GetPolicyStatus(objClsProcessInfo.CUSTOMER_ID, objClsProcessInfo.POLICY_ID, objClsProcessInfo.POLICY_VERSION_ID);

            //DateTime EffDate = new DateTime(
            if (txtEFFECTIVE_DATETIME.Text == "")
                txtEFFECTIVE_DATETIME.Text = hidEFFECTIVE_DATETIME.Value;
            if (txtEFFECTIVE_DATETIME.Text != "")
            {
                DateTime EffDate = ConvertToDate(txtEFFECTIVE_DATETIME.Text);

                int Hr = int.Parse(txtEFFECTIVE_HOUR.Text);

                if (Hr == 12 && cmbMERIDIEM.SelectedIndex == 0) //If added by Charles on 7-Sep-09 for Itrack 6323
                {
                    Hr = 00;
                }
                if (cmbMERIDIEM.SelectedIndex == 1 && Hr != 12) //Added condition Hr!=12 by Charles on 7-Sep-09 for Itrack 6323
                {
                    Hr += 12;
                }
                /*if(Hr==24) //Commented by Charles on 7-Sep-09 for Itrack 6323
                {
                    Hr=00;
                }*/

                objClsProcessInfo.EFFECTIVE_DATETIME = new DateTime(EffDate.Year, EffDate.Month, EffDate.Day
                    , Hr, int.Parse(txtEFFECTIVE_MINUTE.Text)
                    , 0);
                objClsProcessInfo.EFFECTIVE_TIME = objClsProcessInfo.EFFECTIVE_DATETIME.ToLongTimeString();

            }

            if (hidCANCELLATION_OPTION.Value != "" && Convert.ToInt32(hidCANCELLATION_OPTION.Value) > 0)
                objClsProcessInfo.CANCELLATION_OPTION = Convert.ToInt32(hidCANCELLATION_OPTION.Value);


            DateTime DueDate;
            if (objClsProcessInfo.CANCELLATION_OPTION == 11996) //equity cancellation
            {
                DateTime EffDate = ConvertToDate(txtEFFECTIVE_DATETIME.Text);
                //Ravindra(08-27-2008): If cancellation effective from 12-2-2008 it means coverage till 12-1-2008
                DueDate = EffDate.AddDays(-1);
            }
            else
            {
                int GraceDay = int.Parse(hidGRACE_PERIOD.Value); //geting grace period
                DueDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                DueDate = DueDate.AddDays(GraceDay);
            }
            DayOfWeek day = DueDate.DayOfWeek;
            if (day.Equals(DayOfWeek.Saturday)) //if saturday
                DueDate = DueDate.AddDays(2);
            else if (day.Equals(DayOfWeek.Sunday)) //if sunday
                DueDate = DueDate.AddDays(1);

            objClsProcessInfo.DUE_DATE = DueDate;

            if (hidRETURN_PREMIUM.Value != "0")
                objClsProcessInfo.RETURN_PREMIUM = Convert.ToDouble(hidRETURN_PREMIUM.Value, numberFormatInfo);


            if (cmbCANCELLATION_TYPE.SelectedItem != null && cmbCANCELLATION_TYPE.SelectedValue != "")
                objClsProcessInfo.CANCELLATION_TYPE = int.Parse(cmbCANCELLATION_TYPE.SelectedValue);

            if (cmbREASON.SelectedItem != null && cmbREASON.SelectedValue != "")
                objClsProcessInfo.REASON = int.Parse(cmbREASON.SelectedValue);

            objClsProcessInfo.OTHER_REASON = txtOTHER_REASON.Text;


            if (objClsProcessInfo.CANCELLATION_TYPE == (int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.INSURED_REQUEST || objClsProcessInfo.CANCELLATION_TYPE == (int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.COMPANY_REQUEST
                || objClsProcessInfo.CANCELLATION_TYPE == (int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.AGENTS_REQUEST
                || objClsProcessInfo.CANCELLATION_TYPE == (int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.NSF_NO_REPLACEMENT
                )
            {
                objClsProcessInfo.REQUESTED_BY = int.Parse(hidREQUESTED_BY.Value);
            }
            else
            {
                if (cmbREQUESTED_BY.SelectedItem != null && cmbREQUESTED_BY.SelectedValue != "")
                    objClsProcessInfo.REQUESTED_BY = int.Parse(cmbREQUESTED_BY.SelectedValue);
            }


            /*if (cmbPRINT_COMMENTS.SelectedItem != null)
                objClsProcessInfo.PRINT_COMMENTS = cmbPRINT_COMMENTS.SelectedValue;*/


            /*if (cmbPRINT_COMMENTS.SelectedValue != "0")
                objClsProcessInfo.COMMENTS = txtCOMMENTS.Text;
            else
                objClsProcessInfo.COMMENTS = "";*/

            if (cmbAGENCY_PRINT.SelectedItem != null && cmbAGENCY_PRINT.SelectedItem.Value != "")
                objClsProcessInfo.AGENCY_PRINT = int.Parse(cmbAGENCY_PRINT.SelectedItem.Value);

            if (chkPRINTING_OPTIONS.Checked)
                objClsProcessInfo.PRINTING_OPTIONS = (int)(enumYESNO_LOOKUP_CODE.YES);
            else
                objClsProcessInfo.PRINTING_OPTIONS = (int)(enumYESNO_LOOKUP_CODE.NO);

            if (objClsProcessInfo.CANCELLATION_TYPE == (int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.INSURED_REQUEST || objClsProcessInfo.CANCELLATION_TYPE == (int)Cms.BusinessLayer.BlProcess.ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.NSF_REPLACE)
            {
                objClsProcessInfo.INSURED = int.Parse(hidINSURED.Value);
            }
            else
            {
                if (cmbINSURED.SelectedItem != null && cmbINSURED.SelectedItem.Value != "")
                    objClsProcessInfo.INSURED = int.Parse(cmbINSURED.SelectedItem.Value);
            }

            objClsProcessInfo.CREATED_BY = objClsProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
            objClsProcessInfo.CREATED_DATETIME = objClsProcessInfo.COMPLETED_DATETIME = System.DateTime.Now;

            if (hidUNDERWRITER.Value != "" && hidUNDERWRITER.Value != "0")
                objClsProcessInfo.UNDERWRITER = int.Parse(hidUNDERWRITER.Value);

            if (hidSTATE_ID.Value != "" && hidSTATE_ID.Value != "0")
                objClsProcessInfo.STATE_ID = int.Parse(hidSTATE_ID.Value);
            if (hidBILL_TYPE.Value != "" && hidBILL_TYPE.Value != "0")
                objClsProcessInfo.BILL_TYPE = hidBILL_TYPE.Value;

            if (hidAGENCY_CODE.Value != "" && hidAGENCY_CODE.Value != "0")
                objClsProcessInfo.AGENCY_CODE = hidAGENCY_CODE.Value;


            if (hidSTATE_CODE.Value != "" && hidSTATE_CODE.Value != "0")
                objClsProcessInfo.STATE_CODE = hidSTATE_CODE.Value;

            if (hidLOB_ID.Value != "" && hidLOB_ID.Value != "0")
                objClsProcessInfo.LOB_ID = int.Parse(hidLOB_ID.Value);



            if (cmbADD_INT.SelectedItem != null && cmbADD_INT.SelectedItem.Value != "")
            {
                objClsProcessInfo.ADD_INT = int.Parse(cmbADD_INT.SelectedItem.Value);
                //MICHIGAN_MAILERS ITRACK #4068
                if (objClsProcessInfo.ADD_INT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.COMMIT_PRINT_SPOOL).ToString())
                    || objClsProcessInfo.ADD_INT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.ON_DEMAND).ToString())
                    || objClsProcessInfo.ADD_INT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS_CANCEL.MICHIGAN_MAILERS).ToString()))
                {
                    objClsProcessInfo.ADD_INT_ID = hidADD_INT_ID.Value;
                    if (chkSEND_ALL.Checked == true)
                        objClsProcessInfo.SEND_ALL = (int)(enumYESNO_LOOKUP_CODE.YES);
                    //objClsProcessInfo.SEND_ALL = (int)(enumYESNO_LOOKUP_UNIQUE_ID.YES);

                    else
                        objClsProcessInfo.SEND_ALL = (int)(enumYESNO_LOOKUP_CODE.NO);
                    //objClsProcessInfo.SEND_ALL = (int)(enumYESNO_LOOKUP_UNIQUE_ID.NO);
                }
            }
            if (chkINCLUDE_REASON_DESC.Checked == true)
                objClsProcessInfo.INCLUDE_REASON_DESC = "Y";//(string)enumYESNO_LOOKUP_CODE.YES.ToString();
            else
                objClsProcessInfo.INCLUDE_REASON_DESC = "N";//(string)enumYESNO_LOOKUP_CODE.NO.ToString();

            /*if(cmbCUSTOM_LETTER_REQD.SelectedItem!=null && cmbCUSTOM_LETTER_REQD.SelectedItem.Value!="")
                objClsProcessInfo.CUSTOM_LETTER_REQD = int.Parse(cmbCUSTOM_LETTER_REQD.SelectedItem.Value);

            if(cmbSTD_LETTER_REQD.SelectedItem!=null && cmbSTD_LETTER_REQD.SelectedItem.Value!="")
                objClsProcessInfo.STD_LETTER_REQD = int.Parse(cmbSTD_LETTER_REQD.SelectedItem.Value);*/

            if (cmbENDORSEMENT_TYPE.SelectedValue != null && cmbENDORSEMENT_TYPE.SelectedValue != "")
                objClsProcessInfo.ENDORSEMENT_TYPE = int.Parse(cmbENDORSEMENT_TYPE.SelectedValue);        //ashish


            return objClsProcessInfo;
        }

        /// <summary>
        /// Loads the Cancellation Rule Xml into hidden variable..it will be used to 
        /// populate Cancellation Option Dropdown at run-time
        /// </summary>
        private void LoadCancellationRuleXml()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                string filePath = System.Web.HttpContext.Current.Server.MapPath(Request.ApplicationPath + "/cmsweb/support/CancellationActionRule.xml");
                xmlDoc.Load(filePath);
                if (xmlDoc != null)
                {
                    hidCANCEL_RULE_XML.Value = xmlDoc.InnerXml;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
            }
        }

        /// <summary>
        /// Starts the process by calling the StartProcess method of ClsEndorsement
        /// </summary>
        private void StartProcess()
        {
            try
            {
                Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
                objProcessInfo = GetFormValues();

                objProcessInfo.CREATED_BY = objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
                objProcessInfo.CREATED_DATETIME = objProcessInfo.COMPLETED_DATETIME = System.DateTime.Now;
                //objProcessInfo.DIARY_NOTE = objProcessInfo.DIARY_SUBJECT = "Cancellation Process started";				
                //Starting the process
                if (objClsCancellationProcess.StartProcess(objProcessInfo) == true)
                {
                    hidFormSaved.Value = "1";
                    //saved successfully
                    hidROW_ID.Value = objProcessInfo.ROW_ID.ToString();
                    hidDisplayBody.Value = "True";
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("683");
                    hidNEW_POLICY_VERSION_ID.Value = objProcessInfo.NEW_POLICY_VERSION_ID.ToString();
                    hidPOLICY_CURRENT_STATUS.Value = objProcessInfo.POLICY_CURRENT_STATUS;
                    cltPolicyTop.CallPageLoad();
                    cltPolicyTop.RefreshPolicy();
                    //Generating the xml of old data
                    btnSaveFlag = true;
                    btnCommitFlag = false;
                    FillhidOldData();
                }
                else
                {
                    //Hiding the extra buttons					
                    HideButtons();
                    hidDisplayBody.Value = "False";
                    lblMessage.Text = ClsPolicyErrMsg.strMessage;//ClsMessages.FetchGeneralMessage("594");				
                }

                lblMessage.Visible = true;
                HideButtonsForNotices();
                //Saving the session and refreshing the menu
                SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
            }
            catch (Exception objExp)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
                lblMessage.Text = objExp.Message.ToString();
                lblMessage.Visible = true;

                //Hiding the extra buttons
                HideButtons();
            }

        }

        /// <summary>
        /// Retreives the old data in the form of xml and will keep in hidOldData hidden field
        /// </summary>
        /*private void GetOldDataXml()
        {
            //if (hidROW_ID.Value.Trim() != "")
            {
                hidOldData.Value = objClsCancellationProcess.GetProcessInfoDataSet(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value),
                    int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidROW_ID.Value));
            }
        }*/

        /// <summary>
        /// Sets the specified policy in session
        /// </summary>
        private void SetPolicyInSession(int PolicyID, int PolicyVersionID, int CustomerID)
        {
            base.SetPolicyInSession(PolicyID, PolicyVersionID, CustomerID);

            //Changing the client top also
            cltPolicyTop.PolicyID = PolicyID;
            cltPolicyTop.PolicyVersionID = PolicyVersionID;
            cltPolicyTop.CustomerID = CustomerID;
            cltPolicyTop.RefreshPolicy();

        }
        /// <summary>
        /// Sets the property of various validator controls
        /// </summary>
        private void SetValidators()
        {
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            revEFFECTIVE_DATE.ValidationExpression = aRegExpDate;
            revEFFECTIVE_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("22");
            rfvCANCELLATION_OPTION.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1227");//"Plese select Cancellation option.";
            rfvEFFECTIVE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1933");//"Please select effective date.";
            rfvEFFECTIVE_HOUR.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1213");//"Please insert Hours.";
            rfvEFFECTIVE_MINUTE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1214");//"Please insert Minutes.";
            rfvCANCELLATION_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("889");

            rfvMERIDIEM.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1215");//"Please select AM/PM.";
            rfvOTHER_REASON.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1229");//"Please insert Reason description.";
            rfvREASON.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1210");// "Please select Reason.";
            //this.csvCOMMENTS.ErrorMessage			= "Please enter only 250 characters.";
            this.csvOTHER_REASON.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1211");//"Please enter only 250 characters.";
            //rfvCOMMENTS.ErrorMessage				= "Please enter comments.";
            //Commented By Lalit jan 20,2011
            //revRETURN_PREMIUM.ValidationExpression = aRegExpDoublePositiveWithZero;

            revRETURN_PREMIUM.ValidationExpression = aRegExpCurrencyformat;
            //revRETURN_PREMIUM.ErrorMessage= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"116");
            revRETURN_PREMIUM.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("116");

            rngEFFECTIVE_DATE.MinimumValue = strPOL_EFF_DATE; //APP_EFFECTIVE_DATE.ToString("d");
            rngEFFECTIVE_DATE.MaximumValue = strPOL_EXP_DATE;//APP_EXPIRATION_DATE.ToString("d");
            rngEFFECTIVE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1235");//"Date should be between policy effective date & expiry date";
            rfvENDORSEMENT_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1932");
        }

        private void SetPolicyTopControl()
        {
            cltPolicyTop.CustomerID = Convert.ToInt32(Request.Params["CUSTOMER_ID"]);
            cltPolicyTop.PolicyID = Convert.ToInt32(Request.Params["POLICY_ID"]);
            cltPolicyTop.PolicyVersionID = Convert.ToInt32(Request.Params["policyVersionID"]);
        }

        /// <summary>
        /// Retreives the query string values into hidden controls
        /// </summary>
        private void GetQueryString()
        {
            hidCUSTOMER_ID.Value = Request.Params["CUSTOMER_ID"].ToString();
            hidPOLICY_ID.Value = Request.Params["POLICY_ID"].ToString().Trim();
            hidPOLICY_VERSION_ID.Value = Request.Params["policyVersionID"].ToString().Trim();
            hidNEW_POLICY_VERSION_ID.Value = Request.Params["policyVersionID"].ToString().Trim();
            //			hidLOB_ID.Value = GetLOBID();
            if (Request.Params["process"].ToString().ToUpper() == "CANCEL")
                hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_CANCELLATION_PROCESS.ToString();
            else if (Request.Params["process"].ToString().ToUpper() == "CCANCEL")
            {
                hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_CANCELLATION_PROCESS.ToString();
                btnRollBack.Attributes.Add("style", "display:none");
            }
            else if (Request.Params["process"].ToString().ToUpper() == "RCANCEL")
            {
                hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ROLLBACK_CANCELLATION_PROCESS.ToString().Trim();
                //btnComplete.Attributes.Add("style", "display:none");
            }
            ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
            DataSet dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCUSTOMER_ID.Value == "" ? "0" : hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value == "" ? "0" : hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value == "" ? "0" : hidPOLICY_VERSION_ID.Value));

            if (dsPolicy.Tables[0].Rows[0]["UNDERWRITER"] != null && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString() != "")
                hidUNDERWRITER.Value = dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString().Trim();

            if (dsPolicy.Tables[0].Rows[0]["POLICY_LOB"] != null && dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString() != "")
                hidLOB_ID.Value = dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString().Trim();

            if (dsPolicy.Tables[0].Rows[0]["STATE_ID"] != null && dsPolicy.Tables[0].Rows[0]["STATE_ID"].ToString() != "")
                hidSTATE_ID.Value = dsPolicy.Tables[0].Rows[0]["STATE_ID"].ToString().Trim();

            if (dsPolicy.Tables[0].Rows[0]["AGENCY_PHONE"] != null && dsPolicy.Tables[0].Rows[0]["AGENCY_PHONE"].ToString() != "")
                lblAGENT_PHONE_NUMBER.Text = dsPolicy.Tables[0].Rows[0]["AGENCY_PHONE"].ToString().Trim();
            if (dsPolicy.Tables[0].Rows[0]["BILLTYPE"] != null && dsPolicy.Tables[0].Rows[0]["BILLTYPE"].ToString() != "")
                hidBILL_TYPE.Value = dsPolicy.Tables[0].Rows[0]["BILLTYPE"].ToString().Trim();


            if (dsPolicy.Tables[0].Rows[0]["STATE_CODE"] != null && dsPolicy.Tables[0].Rows[0]["STATE_CODE"].ToString() != "")
                hidSTATE_CODE.Value = dsPolicy.Tables[0].Rows[0]["STATE_CODE"].ToString().Trim();

            if (dsPolicy.Tables[0].Rows[0]["AGENCY_CODE"] != null && dsPolicy.Tables[0].Rows[0]["AGENCY_CODE"].ToString() != "")
                hidAGENCY_CODE.Value = dsPolicy.Tables[0].Rows[0]["AGENCY_CODE"].ToString().Trim();

            if (dsPolicy.Tables[0].Rows[0]["APP_INCEPTION_DATE"] != null && dsPolicy.Tables[0].Rows[0]["APP_INCEPTION_DATE"].ToString() != "")
                hidPOL_INCEPTION_DATE.Value = dsPolicy.Tables[0].Rows[0]["APP_INCEPTION_DATE"].ToString().Trim();

            //			if(dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"]!=null && dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString()!="")
            //				APP_EFFECTIVE_DATE = Convert.ToDateTime(dsPolicy.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"]);

            if (dsPolicy.Tables[0].Rows[0]["POL_REINS_EFFECTIVE_DATE"] != null && dsPolicy.Tables[0].Rows[0]["POL_REINS_EFFECTIVE_DATE"].ToString() != "")
            {
                strPOL_EFF_DATE = ConvertDBDateToCulture(dsPolicy.Tables[0].Rows[0]["POL_REINS_EFFECTIVE_DATE"].ToString());
                //APP_EFFECTIVE_DATE = ConvertDBDateToCulture(dsPolicy.Tables[0].Rows[0]["POL_REINS_EFFECTIVE_DATE"].ToString());

                hidAPP_EFFECTIVE_DATE.Value = strPOL_EFF_DATE;//dsPolicy.Tables[0].Rows[0]["POL_REINS_EFFECTIVE_DATE"].ToString();
            }
            if (dsPolicy.Tables[0].Rows[0]["APP_EXPIRATION_DATE"] != null && dsPolicy.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString() != "")
            {
                strPOL_EXP_DATE = ConvertDBDateToCulture(dsPolicy.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString());
                //After save APP_EXPIRATION_DATE varibale become out of scope at ASPX.
                //APP_EXPIRATION_DATE = Convert.ToDateTime(dsPolicy.Tables[0].Rows[0]["APP_EXPIRATION_DATE"]);
                hidAPP_EXPIRATION_DATE.Value = strPOL_EXP_DATE;//dsPolicy.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString();
            }

        }
        private void SetCaptions()
        {
            objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Processes.CancellationProcess", System.Reflection.Assembly.GetExecutingAssembly());
            //capCOMMENTS.Text						=		objResourceMgr.GetString("txtCOMMENTS");
            this.capAGENTPHONENO.Text		 = objResourceMgr.GetString("txtAGENT_PHONE_NUMBER");
            this.capCANCELLATION_OPTION.Text = objResourceMgr.GetString("cmbCANCELLATION_OPTION");
            this.capCANCELLATION_TYPE.Text = objResourceMgr.GetString("cmbCANCELLATION_TYPE");
            //this.capCOMMENTS.Text			= objResourceMgr.GetString("txtCOMMENTS");
            this.capEFFECTIVE_DATETIME.Text = objResourceMgr.GetString("txtEFFECTIVE_DATETIME");
            this.capEFFECTIVE_TIME.Text = objResourceMgr.GetString("txtEFFECTIVE_TIME");
            this.capOTHER_REASON.Text = objResourceMgr.GetString("txtOTHER_REASON");
            //this.capPAST_DUE_PREMIUM.Text	= objResourceMgr.GetString("txtPAST_DUE_PREMIUM");
            //this.capPRINT_COMMENTS.Text		= objResourceMgr.GetString("cmbPRINT_COMMENTS");
            this.capREASON.Text = objResourceMgr.GetString("cmbREASON");
            this.capREQUESTED_BY.Text = objResourceMgr.GetString("cmbREQUESTED_BY");
            this.capRETURN_PREMIUM.Text = objResourceMgr.GetString("txtRETURN_PREMIUM");
            //this.capCUSTOM_LETTER_REQD.Text =	objResourceMgr.GetString("cmbCUSTOM_LETTER_REQD");
            //this.capSTD_LETTER_REQD.Text 	=	objResourceMgr.GetString("cmbSTD_LETTER_REQD");				
            capINSURED.Text = objResourceMgr.GetString("cmbINSURED");
            capPRINTING_OPTIONS.Text = objResourceMgr.GetString("chkPRINTING_OPTIONS");
            capADD_INT.Text = objResourceMgr.GetString("cmbADD_INT");
            capSEND_ALL.Text = objResourceMgr.GetString("chkSEND_ALL");
            capAGENCY_PRINT.Text = objResourceMgr.GetString("cmbAGENCY_PRINT");
            capINCLUDE_REASON_DESC.Text = objResourceMgr.GetString("txtINCLUDE_REASON_DESC");
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            btnReset.Text = Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId,"btnReset");
            btnRollBack.Text = Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId,"btnRollBack");
            btnPolicyDetails.Text = Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId,"btnPolicyDetails");
            btnBack_To_Customer_Assistant.Text = Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId,"btnBack_To_Customer_Assistant");
            btnBack_To_Search.Text = Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId,"btnBack_To_Search");
            btnGenerate_cancellation_notice_or_Memo.Text = Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId,"btnGenerate_cancellation_notice_or_Memo");
            btnSave_Send_Notice.Text = Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId,"btnSave_Send_Notice");
            btnComplete.Text = Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId,"btnComplete");
            btnCommitInProgress.Text = Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId,"btnCommitInProgress");
            btnCalculate_Return_premium.Text = Cms.CmsWeb.ClsMessages.GetButtonsText(base.ScreenId,"btnCalculate_Return_premium");

            capENDORSEMENT_TYPE.Text = objResourceMgr.GetString("capENDORSEMENT_TYPE");
            lblAdditional.Text = objResourceMgr.GetString("lblAdditional");
            lblPrinting.Text = objResourceMgr.GetString("lblPrinting");
            capUnassignLossCodes.Text = objResourceMgr.GetString("capUnassignLossCodes");
            capAssignedLossCodes.Text = objResourceMgr.GetString("capAssignedLossCodes");
        }


        /// <summary>
        /// Sets the security xml and type of button
        /// </summary>
        private void SetButtonsSecurityXML()
        {
            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnSave_Send_Notice.CmsButtonClass = CmsButtonType.Write;
            btnSave_Send_Notice.PermissionString = gstrSecurityXML;

            btnReset.CmsButtonClass = CmsButtonType.Read;
            btnReset.PermissionString = gstrSecurityXML;

            btnRollBack.CmsButtonClass = CmsButtonType.Read;
            btnRollBack.PermissionString = gstrSecurityXML;

            btnComplete.CmsButtonClass = CmsButtonType.Read;
            btnComplete.PermissionString = gstrSecurityXML;

            btnPolicyDetails.CmsButtonClass = CmsButtonType.Read;
            btnPolicyDetails.PermissionString = gstrSecurityXML;

            this.btnBack_To_Customer_Assistant.CmsButtonClass = CmsButtonType.Read;
            this.btnBack_To_Customer_Assistant.PermissionString = gstrSecurityXML;

            this.btnBack_To_Search.CmsButtonClass = CmsButtonType.Read;
            this.btnBack_To_Search.PermissionString = gstrSecurityXML;

            this.btnCalculate_Return_premium.CmsButtonClass = CmsButtonType.Execute;
            this.btnCalculate_Return_premium.PermissionString = gstrSecurityXML;

            //			this.btnCommit_To_Spool.CmsButtonClass = CmsButtonType.Write;
            //			this.btnCommit_To_Spool.PermissionString = gstrSecurityXML;

            this.btnGenerate_cancellation_notice_or_Memo.CmsButtonClass = CmsButtonType.Write;
            this.btnGenerate_cancellation_notice_or_Memo.PermissionString = gstrSecurityXML;

            btnCommitInProgress.CmsButtonClass = CmsButtonType.Read;
            btnCommitInProgress.PermissionString = gstrSecurityXML;


            //			this.btnGenerate_premium_Notice.CmsButtonClass = CmsButtonType.Write;
            //			this.btnGenerate_premium_Notice.PermissionString = gstrSecurityXML;

            //			this.btnPrint_Preview.CmsButtonClass = CmsButtonType.Read;
            //			this.btnPrint_Preview.PermissionString = gstrSecurityXML;
        }

        /// <summary>
        /// Populates other information on this page like agency phone no
        /// </summary>
        //		private void PopulateOtherInfo()
        //		{
        //			ClsCancellationProcess objClsCancellationProcess =new ClsCancellationProcess();
        //			DataSet ds = objClsCancellationProcess.GetAgencyPhoneNo(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value));
        //			lblAGENT_PHONE_NUMBER.Text=ds.Tables[0].Rows[0]["AGENCY_PHONE"].ToString();
        //			hidBILL_TYPE.Value = ds.Tables[0].Rows[0]["BILL_TYPE"].ToString();
        //		}

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
            this.btnRollBack.Click += new System.EventHandler(this.btnRollBack_Click);
            this.btnPolicyDetails.Click += new System.EventHandler(this.btnPolicyDetails_Click);
            this.btnBack_To_Search.Click += new System.EventHandler(this.btnBack_To_Search_Click);
            this.btnBack_To_Customer_Assistant.Click += new System.EventHandler(this.btnBack_To_Customer_Assistant_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnGenerate_cancellation_notice_or_Memo.Click += new System.EventHandler(this.btnGenerate_cancellation_notice_or_Memo_Click);
            this.btnSave_Send_Notice.Click += new System.EventHandler(this.btnSave_Send_Notice_Click);
            this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
            this.btnCalculate_Return_premium.Click += new System.EventHandler(this.btnCalculate_Return_premium_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion


        private void PopulateCombobox()
        {
            cmbENDORSEMENT_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("PPAEND");
            cmbENDORSEMENT_TYPE.DataValueField = "LookupID";
            cmbENDORSEMENT_TYPE.DataTextField = "LookupDesc";
            cmbENDORSEMENT_TYPE.DataBind();
            cmbENDORSEMENT_TYPE.SelectedIndex = cmbENDORSEMENT_TYPE.Items.IndexOf(cmbENDORSEMENT_TYPE.Items.FindByValue("11619")); //default to Other
            //ListItem iListItem = null;

            //iListItem = cmbENDORSEMENT_TYPE.Items.FindByValue("14683"); //Remove
            for (int i = cmbENDORSEMENT_TYPE.Items.Count - 1; i >= 0; i--)
            {
                if (cmbENDORSEMENT_TYPE.Items[i].Value != "14685" && cmbENDORSEMENT_TYPE.Items[i].Value != "14686")
                    cmbENDORSEMENT_TYPE.Items.Remove(cmbENDORSEMENT_TYPE.Items[i]);

            }
            cmbENDORSEMENT_TYPE.Items.Insert(0, "");
            //if (iListItem != null)
            //ashish

        }  //ashish

        private void btnCalculate_Return_premium_Click(object sender, System.EventArgs e)
        {
            try
            {
                string strScript = "<script>setTimeout('CallService()', 1000);</script>";
                hidFormSaved.Value = "3";
                ClientScript.RegisterStartupScript(this.GetType(),"CallService", strScript);
                LoadData();
                cltPolicyTop.CallPageLoad();
                //SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
                if (hidNEW_POLICY_VERSION_ID.Value != "" && hidNEW_POLICY_VERSION_ID.Value != "0")
                    SetPolicyInSession(int.Parse(hidPOLICY_ID.Value), int.Parse(hidNEW_POLICY_VERSION_ID.Value), int.Parse(hidCUSTOMER_ID.Value));
                else
                    SetPolicyInSession(int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidCUSTOMER_ID.Value));
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally
            {
            }

        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                //Itrack #4282//Itrack 4104
                SetReturnPremiumAmount();

                Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();

                objProcessInfo = GetFormValues();
                if (hidPOLICY_PREVIOUS_STATUS.Value != "")
                    objProcessInfo.POLICY_PREVIOUS_STATUS = hidPOLICY_PREVIOUS_STATUS.Value;
                if (hidPOLICY_CURRENT_STATUS.Value != "")
                    objProcessInfo.POLICY_CURRENT_STATUS = hidPOLICY_CURRENT_STATUS.Value;
                if (hidCANCELLATION_NOTICE_SENT.Value != "")
                    objProcessInfo.CANCELLATION_NOTICE_SENT = hidCANCELLATION_NOTICE_SENT.Value;
                objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value);


                //Making model object which will contain old data
                Cms.Model.Policy.Process.ClsProcessInfo objOldProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
                base.PopulateModelObject(objOldProcessInfo, hidOldData.Value);

                //string strEffTime = ClsCommon.FetchValueFromXML("EFFECTIVETIME", hidOldData.Value);
                if (hidEFFECTIVE_DATETIME.Value != "")
                {
                    objOldProcessInfo.EFFECTIVE_DATETIME = ClsCommon.ConvertToDate(hidEFFECTIVE_DATETIME.Value);
                }
                //Updating the previous process record
                try
                {
                    objClsCancellationProcess.BeginTransaction();
                    objClsCancellationProcess.UpdateProcessInformation(objOldProcessInfo, objProcessInfo);
                    objClsCancellationProcess.CommitTransaction();
                }
                catch (Exception ex)
                {
                    objClsCancellationProcess.RollbackTransaction();
                    throw ex;
                }
                ClsMessages.SetCustomizedXml(GetLanguageCode());
                lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                lblMessage.Visible = true;
                hidFormSaved.Value = "1";
                LoadData();
                //Refresh the Policy Top.
                cltPolicyTop.PolicyVersionID = objProcessInfo.NEW_POLICY_VERSION_ID;
                cltPolicyTop.CallPageLoad();
                //SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
                SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);


            }
            catch (Exception objExp)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
                lblMessage.Text = objExp.Message.ToString();
                lblMessage.Visible = true;

            }
        }
        #region Process Complete
        private string getCancellationOptionDesc(string CanOption)
        {
            string Desc = "";
            switch (CanOption)
            {
                case "11994":
                    Desc = "Pro Rata";
                    break;
                case "11995":
                    Desc = "Flat";
                    break;
                case "11996":
                    Desc = "Equity";
                    break;
            }
            return Desc;
        }
        /// <summary>
        /// Completes the Process.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnComplete_Click(object sender, System.EventArgs e)
        {

            //Local Variable Declartions
            ClsProcessInfo objProcessInfo = new ClsProcessInfo();
            ClsPolicyProcessNoticInfo objProcessNoticeInfo = new ClsPolicyProcessNoticInfo();
            ClsPolicyProcess objProcess = new ClsPolicyProcess();
            //int ReturnPremium = 0;
            if (hidIS_CANCELLATION_OPTION_EXISTS.Value == "false" && hidCANCELLATION_NOTICE_SENT.Value == "Y") // as per Ravinder Mail regarding Itrack 4827
            {
                lblMessage.Text = "Cancellation option \"" + getCancellationOptionDesc(hidCANCELLATION_OPTION_DB.Value) + "\" was selected when cancellation was launched but current payment conditions on this policy does not allow \"" + getCancellationOptionDesc(hidCANCELLATION_OPTION_DB.Value) + "\" method. Please rollback cancellation.";
                lblMessage.Visible = true;
                //Added for Itrack Issue 5628 on 8 April 2009- To get default customer_id,policy_id,policy_version_id for customer assitant icon to navigate
                cltPolicyTop.UseRequestVariables = true;
                cltPolicyTop.CallPageLoad();
                return;
            }
            try
            {
                //Sets the Values
                btnSave_Click(null, null);
                objProcessInfo = GetFormValues();
                objProcessInfo = objClsCancellationProcess.GetRunningProcess(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value));
                objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value);
                objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
                objProcessInfo.COMPLETED_DATETIME = DateTime.Now;
                objProcessInfo.LOB_ID = int.Parse(hidLOB_ID.Value);

                if (hidRETURN_PREMIUM.Value != "0")
                    objProcessInfo.RETURN_PREMIUM = Convert.ToDouble(hidRETURN_PREMIUM.Value, numberFormatInfo);
                if (cmbCANCELLATION_TYPE.SelectedItem != null && cmbCANCELLATION_TYPE.SelectedValue != "")
                    objProcessInfo.CANCELLATION_TYPE = int.Parse(cmbCANCELLATION_TYPE.SelectedValue);
                if (hidSTATE_ID.Value != "" && hidSTATE_ID.Value != "0")
                    objProcessInfo.STATE_ID = int.Parse(hidSTATE_ID.Value);
                if (hidSTATE_ID.Value != "" && hidSTATE_ID.Value != "0")
                    objProcessInfo.STATE_ID = int.Parse(hidSTATE_ID.Value);
                if (hidBILL_TYPE.Value != "" && hidBILL_TYPE.Value != "0")
                    objProcessInfo.BILL_TYPE = hidBILL_TYPE.Value;
                if (hidAGENCY_CODE.Value != "" && hidAGENCY_CODE.Value != "0")
                    objProcessInfo.AGENCY_CODE = hidAGENCY_CODE.Value;
                if (hidSTATE_CODE.Value != "" && hidSTATE_CODE.Value != "0")
                    objProcessInfo.STATE_CODE = hidSTATE_CODE.Value;
                objClsCancellationProcess.SystemID = getCarrierSystemID();
                if (objClsCancellationProcess.CommitProcess(objProcessInfo) == true)
                {

                    hidFormSaved.Value = "1";
                    hidDisplayBody.Value = "True";
                    hidCANCELLATION_NOTICE_SENT.Value = objProcessInfo.CANCELLATION_NOTICE_SENT;//Set cancellation notice - Added by Pradeep on 22-July-2011 
                    if (ClsPolicyProcess.PrintingErrorFlag)
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("925");
                    else
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("690");
                    //objProcess.GetTotalReturnPremium(objProcessInfo.CUSTOMER_ID,objProcessInfo.POLICY_ID,objProcessInfo.POLICY_VERSION_ID,objProcessInfo.ROW_ID,out ReturnPremium); 
                    //lblRETURN_PREMIUM.Text = ReturnPremium.ToString();
                    //txtRETURN_PREMIUM.Text = ReturnPremium.ToString();
                    cltPolicyTop.PolicyVersionID = objProcessInfo.NEW_POLICY_VERSION_ID;
                    LoadData();
                    cltPolicyTop.CallPageLoad();
                    //Hiding the extra buttons
                    btnSave.Visible = false;
                    HideButtons();

                }
                else
                {
                    hidDisplayBody.Value = "False";
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("601");
                    btnComplete.Attributes.Add("style", "display:inline");
                    btnCommitInProgress.Attributes.Add("style", "display:none");

                }

                //Refresh the Policy Top.
                cltPolicyTop.CallPageLoad();

                lblMessage.Visible = true;

                //Updating the policy top,session and menus
                //SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
                SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);

            }
            catch (Exception objExp)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
                lblMessage.Text = objExp.Message.ToString();
                lblMessage.Visible = true;
                btnComplete.Attributes.Add("style", "display:inline");
                btnCommitInProgress.Attributes.Add("style", "display:none");
                cltPolicyTop.UseRequestVariables = true;
                cltPolicyTop.CallPageLoad();
            }
            finally
            {
                if (objProcess != null)
                {
                    objProcess.Dispose();
                }
                if (objProcessNoticeInfo != null)
                {
                    objProcessNoticeInfo = null;
                }
            }
        }

        #endregion

        private void HideButtonsForNotices()
        {
            if (btnCommitFlag)
            {
                // if (Request.Params["process"].ToString().ToUpper() != "RCANCEL")
                // btnComplete.Attributes.Add("style", "display:inline");
            }
            else
            {
                // btnComplete.Attributes.Add("style", "display:none");
            }
            //if (btnSaveFlag)
            //{
            //    btnSave.Attributes.Add("style", "display:inline");
            //    btnGenerate_cancellation_notice_or_Memo.Attributes.Add("style", "display:inline");
            //}
            //else
            //{
            //    btnSave.Attributes.Add("style", "display:none");
            //    btnGenerate_cancellation_notice_or_Memo.Attributes.Add("style", "display:none");
            //}
            //btnGenerate_cancellation_notice_or_Memo.Attributes.Add("style", "display:inline");
        }
        /// <summary>
        /// Hides the commit and rollback button
        /// </summary>
        private void HideButtons()
        {

            btnSave.Attributes.Add("style", "display:none");
            btnComplete.Attributes.Add("style", "display:none");
            btnRollBack.Attributes.Add("style", "display:none");
            btnSave_Send_Notice.Attributes.Add("style", "display:none");
            btnCommitInProgress.Attributes.Add("style", "display:none");

        }


        #region Process RollBack
        private void btnRollBack_Click(object sender, System.EventArgs e)
        {

            //Local Variable Declartions
            ClsProcessInfo objProcessInfo = new ClsProcessInfo();

            try
            {

                //Sets the Values
                //objProcessInfo = GetFormValues();
                objProcessInfo = objClsCancellationProcess.GetRunningProcess(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value));

                if (hidSTATE_ID.Value != "" && hidSTATE_ID.Value != "0")
                    objProcessInfo.STATE_ID = int.Parse(hidSTATE_ID.Value);
                if (hidBILL_TYPE.Value != "" && hidBILL_TYPE.Value != "0")
                    objProcessInfo.BILL_TYPE = hidBILL_TYPE.Value;
                if (hidAGENCY_CODE.Value != "" && hidAGENCY_CODE.Value != "0")
                    objProcessInfo.AGENCY_CODE = hidAGENCY_CODE.Value;
                if (hidSTATE_CODE.Value != "" && hidSTATE_CODE.Value != "0")
                    objProcessInfo.STATE_CODE = hidSTATE_CODE.Value;
                objProcessInfo.LOB_ID = int.Parse(hidLOB_ID.Value);
                objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value);
                objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ROLLBACK_CANCELLATION_PROCESS;
                objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
                objProcessInfo.COMPLETED_DATETIME = DateTime.Now;
                if (objClsCancellationProcess.RollbackProcess(objProcessInfo, true) == true)
                {

                    hidFormSaved.Value = "1";
                    hidDisplayBody.Value = "False";
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("697");
                    hidNEW_POLICY_VERSION_ID.Value = "";
                    cltPolicyTop.PolicyVersionID = objProcessInfo.POLICY_VERSION_ID;
                    LoadData();

                    //Hiding the extra buttons
                    HideButtons();
                }
                else
                {
                    hidDisplayBody.Value = "True";
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("602");
                }

                //Refresh the Policy Top.
                cltPolicyTop.CallPageLoad();

                lblMessage.Visible = true;

                //Updating the policy top,session and menus
                SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);

            }
            catch (Exception objExp)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
                lblMessage.Text = objExp.Message.ToString();
                lblMessage.Visible = true;
            }
        }
        #endregion

        private void btnBack_To_Search_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("/cms/client/aspx/CustomerManagerSearch.aspx");
        }

        private void btnBack_To_Customer_Assistant_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("/cms/client/aspx/CustomerManagerIndex.aspx?Customer_ID=" + hidCUSTOMER_ID.Value);
        }

        private void btnPrint_Preview_Click(object sender, System.EventArgs e)
        {
            /*int i_customer_ID,i_pol_ID,i_pol_Version_ID;
            string str_Lob_id;

            ClsGeneralInformation objGeneralInfo = new ClsGeneralInformation();
			
            i_customer_ID=int.Parse(GetCustomerID());
            i_pol_ID=int.Parse(GetPolicyID());
            i_pol_Version_ID=int.Parse(GetPolicyVersionID());
            str_Lob_id=GetLOBID();

            string strXML	=	objGeneralInfo.GetPolicyInputXML(i_customer_ID,i_pol_ID,i_pol_Version_ID,str_Lob_id);

            //	string strXML	=	objGeneralInfo.GetPolicyInputXML(1,1,1,"1");
			
//			string strXML			=	"<DWELLINGDETAILS ID='1' ADDRESS='48323' POLICYTYPE='HO-2'>	<LOB_ID>1</LOB_ID>	<POLICY_ID></POLICY_ID> <!--Basic Policy Page--><STATENAME>MICHIGAN</STATENAME>	<NEWBUSINESSFACTOR>Y</NEWBUSINESSFACTOR>	<QUOTEEFFDATE>12/26/2005</QUOTEEFFDATE>	<QUOTEEXPDATE>12/26/2010 12:00:00 AM</QUOTEEXPDATE>	<TERMFACTOR>12</TERMFACTOR>	<SEASONALSECONDARY>N</SEASONALSECONDARY>	<WOLVERINEINSURESPRIMARY>N</WOLVERINEINSURESPRIMARY>	<PRODUCTNAME>HO-2</PRODUCTNAME>	<PRODUCT_PREMIER></PRODUCT_PREMIER>	<REPLACEMENTCOSTFACTOR>175000</REPLACEMENTCOSTFACTOR>	<DWELLING_LIMITS>175000.00</DWELLING_LIMITS>	<PROTECTIONCLASS>01</PROTECTIONCLASS>	<DISTANCET_FIRESTATION>5</DISTANCET_FIRESTATION>	<FEET2HYDRANT>1000 or less</FEET2HYDRANT>	<DEDUCTIBLE>500</DEDUCTIBLE>	<EXTERIOR_CONSTRUCTION>8935</EXTERIOR_CONSTRUCTION>	<EXTERIOR_CONSTRUCTION_F_M>F</EXTERIOR_CONSTRUCTION_F_M>	<DOC>1974</DOC>	<AGEOFHOME>31</AGEOFHOME>	<NUMBEROFFAMILIES>1</NUMBEROFFAMILIES>	<NUMBEROFUNITS>1</NUMBEROFUNITS>	<PERSONALLIABILITY_LIMIT>100000.00</PERSONALLIABILITY_LIMIT>	<PERSONALLIABILITY_PREMIUM>0</PERSONALLIABILITY_PREMIUM>	<PERSONALLIABILITY_DEDUCTIBLE>0</PERSONALLIABILITY_DEDUCTIBLE>	<MEDICALPAYMENTSTOOTHERS_LIMIT>1000.00</MEDICALPAYMENTSTOOTHERS_LIMIT>	<MEDICALPAYMENTSTOOTHERS_PREMIUM>0</MEDICALPAYMENTSTOOTHERS_PREMIUM>	<MEDICALPAYMENTSTOOTHERS_DEDUCTIBLE>0</MEDICALPAYMENTSTOOTHERS_DEDUCTIBLE>	<FORM_CODE>1F</FORM_CODE> <!--Property Page--><HO20>N</HO20>	<HO21>N</HO21>	<HO25>N</HO25>	<HO23>N</HO23>	<HO34>Y</HO34>	<HO11>N</HO11>	<HO32>N</HO32>	<HO277>Y</HO277>	<HO455>Y</HO455>	<HO327>3000</HO327>	<HO33>Not Rented</HO33>	<HO315>N</HO315>	<HO9>N</HO9>	<HO287>N</HO287>	<HO96FINALVALUE>500.00</HO96FINALVALUE>	<HO96INCLUDE>500</HO96INCLUDE>	<HO96ADDITIONAL>0</HO96ADDITIONAL>	<HO48INCLUDE>17500</HO48INCLUDE>	<HO48ADDITIONAL>0</HO48ADDITIONAL>	<HO40INCLUDE></HO40INCLUDE>	<HO40ADDITIONAL>0</HO40ADDITIONAL>	<HO42ADDITIONAL>0.00</HO42ADDITIONAL> <!--Insert By Shrikant as on 19-09-2005 --><REPAIRCOSTINCLUDE></REPAIRCOSTINCLUDE>" +	
//										"<REPAIRCOSTADDITIONAL>0</REPAIRCOSTADDITIONAL>	<PERSONALPROPERTYINCREASEDLIMITINCLUDE>122500</PERSONALPROPERTYINCREASEDLIMITINCLUDE>	<PERSONALPROPERTYINCREASEDLIMITADDITIONAL>0.00</PERSONALPROPERTYINCREASEDLIMITADDITIONAL>	<PERSONALPROPERTYAWAYINCLUDE></PERSONALPROPERTYAWAYINCLUDE>	<PERSONALPROPERTYAWAYADDITIONAL>0</PERSONALPROPERTYAWAYADDITIONAL>	<UNSCHEDULEDJEWELRYINCLUDE>1000</UNSCHEDULEDJEWELRYINCLUDE>	<UNSCHEDULEDJEWELRYADDITIONAL>0</UNSCHEDULEDJEWELRYADDITIONAL>	<MONEYINCLUDE>200</MONEYINCLUDE>	<MONEYADDITIONAL>0</MONEYADDITIONAL>	<SECURITIESINCLUDE>1000</SECURITIESINCLUDE>	<SECURITIESADDITIONAL>0</SECURITIESADDITIONAL>	<SILVERWAREINCLUDE>2500</SILVERWAREINCLUDE>	<SILVERWAREADDITIONAL>0</SILVERWAREADDITIONAL>	<FIREARMSINCLUDE>2000</FIREARMSINCLUDE>	<FIREARMSADDITIONAL>0</FIREARMSADDITIONAL>	<HO312INCLUDE>2500</HO312INCLUDE>	<HO312ADDITIONAL>0</HO312ADDITIONAL>	<ADDITIONALLIVINGEXPENSEINCLUDE>52500</ADDITIONALLIVINGEXPENSEINCLUDE>	<ADDITIONALLIVINGEXPENSEADDITIONAL>0.00</ADDITIONALLIVINGEXPENSEADDITIONAL>	<HO53INCLUDE>500</HO53INCLUDE>	<HO53ADDITIONAL>0</HO53ADDITIONAL>	<HO35INCLUDE>N/A</HO35INCLUDE>	<HO35ADDITIONAL>0</HO35ADDITIONAL>	<SPECIFICSTRUCTURESINCLUDE></SPECIFICSTRUCTURESINCLUDE>	<SPECIFICSTRUCTURESADDITIONAL>0</SPECIFICSTRUCTURESADDITIONAL> <!--Liability Options--><OCCUPIED_INSURED>0.00</OCCUPIED_INSURED>	<RESIDENCE_PREMISES>0.00</RESIDENCE_PREMISES>	<OTHER_LOC_1FAMILY>1.00</OTHER_LOC_1FAMILY>	<OTHER_LOC_2FAMILY>1.00</OTHER_LOC_2FAMILY>	<ONPREMISES_HO42>N</ONPREMISES_HO42>	<LOCATED_OTH_STRUCTURE>N</LOCATED_OTH_STRUCTURE>	<INSTRUCTIONONLY_HO42>N</INSTRUCTIONONLY_HO42>	<OFF_PREMISES_HO43>N</OFF_PREMISES_HO43>	<PIP_HO82>N</PIP_HO82>	<HO200>N</HO200>	<RESIDENCE_EMP_NUMBER>0</RESIDENCE_EMP_NUMBER>	<CLERICAL_OFFICE_HO71>N</CLERICAL_OFFICE_HO71>	<SALESMEN_INC_INSTALLATION>N</SALESMEN_INC_INSTALLATION>	<TEACHER_ATHELETIC>N</TEACHER_ATHELETIC>	<TEACHER_NOC>N</TEACHER_NOC>	<INCIDENTAL_FARMING_HO72>N</INCIDENTAL_FARMING_HO72>	<OTH_LOC_OPR_EMPL_HO73>0.00</OTH_LOC_OPR_EMPL_HO73>" +	
//										"<OTH_LOC_OPR_OTHERS_HO73>0</OTH_LOC_OPR_OTHERS_HO73> <!--Credit and Charges--><LOSSFREE>N</LOSSFREE>	<NOTLOSSFREE>N</NOTLOSSFREE>	<VALUEDCUSTOMER>N</VALUEDCUSTOMER>	<MULTIPLEPOLICYFACTOR>N</MULTIPLEPOLICYFACTOR>	<NONSMOKER>N</NONSMOKER>	<EXPERIENCE>105</EXPERIENCE>	<CONSTRUCTIONCREDIT>N</CONSTRUCTIONCREDIT>	<REDUCTION_IN_COVERAGE_C>0</REDUCTION_IN_COVERAGE_C>	<N0_LOCAL_ALARM>0</N0_LOCAL_ALARM>	<BURGLER_ALERT_POLICE>N</BURGLER_ALERT_POLICE>	<FIRE_ALARM_FIREDEPT>N</FIRE_ALARM_FIREDEPT>	<BURGLAR>N</BURGLAR>	<BURGLAR_ACORD>51</BURGLAR_ACORD>	<CENTRAL_FIRE>N</CENTRAL_FIRE>	<INSURANCESCORE>550</INSURANCESCORE>	<WOODSTOVE_SURCHARGE>Y</WOODSTOVE_SURCHARGE>	<PRIOR_LOSS_SURCHARGE>N</PRIOR_LOSS_SURCHARGE>	<DOGSURCHARGE>0</DOGSURCHARGE>	<DOGFACTOR>N</DOGFACTOR> <!--Inland Marine--><SCH_BICYCLE_DED>250</SCH_BICYCLE_DED>	<SCH_BICYCLE_AMOUNT>200</SCH_BICYCLE_AMOUNT>	<SCH_CAMERA_DED>250</SCH_CAMERA_DED>	<SCH_CAMERA_AMOUNT>200</SCH_CAMERA_AMOUNT>	<SCH_CELL_DED>0</SCH_CELL_DED>	<SCH_CELL_AMOUNT>200</SCH_CELL_AMOUNT>	<SCH_FURS_DED>250</SCH_FURS_DED>	<SCH_FURS_AMOUNT>200</SCH_FURS_AMOUNT>	<SCH_GUNS_DED>2500</SCH_GUNS_DED>	<SCH_GUNS_AMOUNT>200</SCH_GUNS_AMOUNT>	<SCH_GOLF_DED>1000</SCH_GOLF_DED>	<SCH_GOLF_AMOUNT>200</SCH_GOLF_AMOUNT>	<SCH_JWELERY_DED>750</SCH_JWELERY_DED>	<SCH_JWELERY_AMOUNT>200</SCH_JWELERY_AMOUNT>	<SCH_MUSICAL_DED>0</SCH_MUSICAL_DED>	<SCH_MUSICAL_AMOUNT>200</SCH_MUSICAL_AMOUNT>	<SCH_PERSCOMP_DED>0</SCH_PERSCOMP_DED>	<SCH_PERSCOMP_AMOUNT>200</SCH_PERSCOMP_AMOUNT>	<SCH_SILVER_DED>100</SCH_SILVER_DED>	<SCH_SILVER_AMOUNT>200</SCH_SILVER_AMOUNT>	<SCH_STAMPS_DED>0</SCH_STAMPS_DED>	<SCH_STAMPS_AMOUNT>200</SCH_STAMPS_AMOUNT>	<SCH_RARECOINS_DED>0</SCH_RARECOINS_DED>	<SCH_RARECOINS_AMOUNT>500</SCH_RARECOINS_AMOUNT>	<SCH_FINEARTS_WO_BREAK_DED>0</SCH_FINEARTS_WO_BREAK_DED>	<SCH_FINEARTS_WO_BREAK_AMOUNT>300</SCH_FINEARTS_WO_BREAK_AMOUNT>	<SCH_FINEARTS_BREAK_DED>0</SCH_FINEARTS_BREAK_DED>	<SCH_FINEARTS_BREAK_AMOUNT>500</SCH_FINEARTS_BREAK_AMOUNT>" + 
//										"<!--Rest--><TERRITORYCODES>3</TERRITORYCODES>	<TERRITORYZONE>0</TERRITORYZONE>	<TERRITORYNAME>ORCHARD LAKE</TERRITORYNAME>	<TERRITORYCOUNTY>OAKLAND</TERRITORYCOUNTY>	<COVERAGEVALUE>175</COVERAGEVALUE>	<TEMPERATURE>0</TEMPERATURE>	<SMOKE>0</SMOKE><DWELLING>N</DWELLING>	<YEARS>0</YEARS>	<CHIMNEYSTOVE>N</CHIMNEYSTOVE>	<TERRITORYCODES1></TERRITORYCODES1>	<PREMIUMGROUP>0</PREMIUMGROUP>	<OTHERSTRUCTURES_LIMIT>0</OTHERSTRUCTURES_LIMIT>	<PERSONALPROPERTY_LIMIT>0</PERSONALPROPERTY_LIMIT>	<LOSSOFUSE_LIMIT>0</LOSSOFUSE_LIMIT>	<COVERAGEFACTOR>0</COVERAGEFACTOR>	<BASEPREMIUM>0</BASEPREMIUM>	<CLAIMS>N</CLAIMS>	<AMOUNT>0</AMOUNT></DWELLINGDETAILS>";
            string strFinalPremiun	=	"";
            ClsPolicyProcess objprocess = new ClsPolicyProcess();
            strFinalPremiun			=	objprocess.GetPremium(strXML,"1");

            Response.Write(strFinalPremiun);*/
        }


        /// <summary>
        /// Sets the process information 
        /// </summary>
        private void PopulateProcessInfo()
        {
            ClsPolicyProcess objPro = new ClsPolicyProcess();
            ClsProcessInfo objProcess = objPro.GetRunningProcess(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value));
            if (objProcess != null)
            {
                hidPOLICY_VERSION_ID.Value = objProcess.POLICY_VERSION_ID.ToString();
                hidNEW_POLICY_VERSION_ID.Value = objProcess.NEW_POLICY_VERSION_ID.ToString(); ;
                hidROW_ID.Value = objProcess.ROW_ID.ToString();
                hidPOLICY_CURRENT_STATUS.Value = objProcess.POLICY_CURRENT_STATUS;
                hidPOLICY_PREVIOUS_STATUS.Value = objProcess.POLICY_PREVIOUS_STATUS;
                hidCANCELLATION_NOTICE_SENT.Value = objProcess.CANCELLATION_NOTICE_SENT;
                hidDisplayBody.Value = "True";
                if (objProcess.CANCELLATION_NOTICE_SENT == "Y")
                {
                    btnSaveFlag = false;
                    btnCommitFlag = true;
                    hidCANCELLATION_NOTICE_SENT.Value = "Y";
                }
                else
                {
                    btnSaveFlag = true;
                    btnCommitFlag = false;
                }
                LoadData();
                HideButtonsForNotices();

                //Saving the session and refreshing the menu
                //SetPolicyInSession(objProcess.POLICY_ID, objProcess.POLICY_VERSION_ID, objProcess.CUSTOMER_ID);
                SetPolicyInSession(objProcess.POLICY_ID, objProcess.NEW_POLICY_VERSION_ID, objProcess.CUSTOMER_ID);
            }
            else
            {
                ClsMessages.SetCustomizedXml(GetLanguageCode());
                SetPolicyInSession(int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidCUSTOMER_ID.Value));
                hidDisplayBody.Value = "False";
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1205");//"No Process in Progress on this Policy.";
                lblMessage.Visible = true;
            }
        }


        private void LoadData()
        {
            numberFormatInfo.NumberDecimalDigits = 2;
            DataSet dsTemp = new DataSet();

            dsTemp = ClsPolicyProcess.GetProcessInfoDataSet(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value),
                int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidROW_ID.Value));

            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                if (dsTemp.Tables[0].Rows[0]["EFFECTIVETIME"] != DBNull.Value)
                {
                    int hour = 0;
                    hour = Convert.ToInt32(Convert.ToDateTime(dsTemp.Tables[0].Rows[0]["EFFECTIVETIME"]).Hour.ToString());

                    if (hour == 00)//Added by Charles on 7-Sep-09 for Itrack 6323
                    {
                        hour = 12;
                        cmbMERIDIEM.SelectedIndex = 0;
                    }
                    else if (hour == 12)
                    {
                        cmbMERIDIEM.SelectedIndex = 1;
                    }//Added till here
                    else if (hour > 12)
                    {
                        hour = hour - 12;
                        cmbMERIDIEM.SelectedIndex = 1;
                    }
                    else
                        cmbMERIDIEM.SelectedIndex = 0;

                    txtEFFECTIVE_HOUR.Text = hour.ToString();

                    txtEFFECTIVE_MINUTE.Text = Convert.ToDateTime(dsTemp.Tables[0].Rows[0]["EFFECTIVETIME"]).Minute.ToString();
                }

                if (dsTemp.Tables[0].Rows[0]["EFFECTIVE_DATETIME"] != DBNull.Value)
                {
                    string UI_DATE = ConvertDBDateToCulture(dsTemp.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString());
                    txtEFFECTIVE_DATETIME.Text = UI_DATE; //ConvertDBDateToCulture(dsTemp.Tables[0].Rows[0]["EFFECTIVE_DATETIME"].ToString();
                    hidEFF_DAT.Value = UI_DATE;
                }

                /*if (dsTemp.Tables[0].Rows[0]["COMMENTS"] != DBNull.Value)
                {
                    txtCOMMENTS.Text = dsTemp.Tables[0].Rows[0]["COMMENTS"].ToString();
                }*/

                /*if (dsTemp.Tables[0].Rows[0]["PRINT_COMMENTS"] != DBNull.Value && dsTemp.Tables[0].Rows[0]["PRINT_COMMENTS"].ToString().Trim()!="")
                {
                    cmbPRINT_COMMENTS.SelectedValue = dsTemp.Tables[0].Rows[0]["PRINT_COMMENTS"].ToString();
                }*/

                if (dsTemp.Tables[0].Rows[0]["REQUESTED_BY"] != DBNull.Value && dsTemp.Tables[0].Rows[0]["REQUESTED_BY"].ToString().Trim() != "")
                {
                    //cmbREQUESTED_BY.SelectedValue = dsTemp.Tables[0].Rows[0]["REQUESTED_BY"].ToString();
                    cmbREQUESTED_BY.SelectedIndex = cmbREQUESTED_BY.Items.IndexOf(cmbREQUESTED_BY.Items.FindByValue(dsTemp.Tables[0].Rows[0]["REQUESTED_BY"].ToString()));
                }

                if (dsTemp.Tables[0].Rows[0]["CANCELLATION_OPTION"] != DBNull.Value && dsTemp.Tables[0].Rows[0]["CANCELLATION_OPTION"].ToString().Trim() != "" && dsTemp.Tables[0].Rows[0]["CANCELLATION_OPTION"].ToString().Trim() != "0")
                {
                    //cmbCANCELLATION_OPTION.SelectedValue = dsTemp.Tables[0].Rows[0]["CANCELLATION_OPTION"].ToString();
                    hidCANCELLATION_OPTION.Value = dsTemp.Tables[0].Rows[0]["CANCELLATION_OPTION"].ToString();
                    hidCANCELLATION_OPTION_DB.Value = dsTemp.Tables[0].Rows[0]["CANCELLATION_OPTION"].ToString();
                }


                if (dsTemp.Tables[0].Rows[0]["CANCELLATION_TYPE"] != DBNull.Value && dsTemp.Tables[0].Rows[0]["CANCELLATION_TYPE"].ToString().Trim() != "" && dsTemp.Tables[0].Rows[0]["CANCELLATION_TYPE"].ToString().Trim() != "0")
                {
                    //cmbCANCELLATION_TYPE.SelectedValue = dsTemp.Tables[0].Rows[0]["CANCELLATION_TYPE"].ToString();
                    cmbCANCELLATION_TYPE.SelectedIndex = cmbCANCELLATION_TYPE.Items.IndexOf(cmbCANCELLATION_TYPE.Items.FindByValue(dsTemp.Tables[0].Rows[0]["CANCELLATION_TYPE"].ToString()));
                }

                if (dsTemp.Tables[0].Rows[0]["REASON"] != DBNull.Value && dsTemp.Tables[0].Rows[0]["REASON"].ToString().Trim() != "" && dsTemp.Tables[0].Rows[0]["REASON"].ToString().Trim() != "0")
                {
                    //cmbREASON.SelectedValue = dsTemp.Tables[0].Rows[0]["REASON"].ToString();
                    cmbREASON.SelectedIndex = cmbREASON.Items.IndexOf(cmbREASON.Items.FindByValue(dsTemp.Tables[0].Rows[0]["REASON"].ToString()));
                }


                if (dsTemp.Tables[0].Rows[0]["OTHER_REASON"] != DBNull.Value)
                {
                    txtOTHER_REASON.Text = dsTemp.Tables[0].Rows[0]["OTHER_REASON"].ToString();
                }

                if (dsTemp.Tables[0].Rows[0]["RETURN_PREMIUM"] != DBNull.Value && dsTemp.Tables[0].Rows[0]["RETURN_PREMIUM"].ToString() != "0")
                {

                    txtRETURN_PREMIUM.Text = Convert.ToDouble(dsTemp.Tables[0].Rows[0]["RETURN_PREMIUM"]).ToString("N", numberFormatInfo);
                    hidRETURN_PREMIUM.Value = txtRETURN_PREMIUM.Text;
                }

                /*if (dsTemp.Tables[0].Rows[0]["PAST_DUE_PREMIUM"] != DBNull.Value)
                {
                    lblPAST_DUE_PREMIUM.Text = dsTemp.Tables[0].Rows[0]["PAST_DUE_PREMIUM"].ToString();
                }*/
                if (dsTemp.Tables[0].Rows[0]["INSURED"] != DBNull.Value && dsTemp.Tables[0].Rows[0]["INSURED"].ToString() != "0")
                {
                    //cmbINSURED.SelectedValue =dsTemp.Tables[0].Rows[0]["INSURED"].ToString(); 
                    cmbINSURED.SelectedIndex = cmbINSURED.Items.IndexOf(cmbINSURED.Items.FindByValue(dsTemp.Tables[0].Rows[0]["INSURED"].ToString()));
                }
                hidOldData.Value = dsTemp.GetXml();
                ///remove Return premium node from xml for multilingual implmentation
                #region Remove Return premium node from Xml
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(dsTemp.GetXml());
                XmlNode xNodeMAIN = xDoc.SelectSingleNode("NewDataSet/Table");
                if (xNodeMAIN != null)
                {
                    XmlNode xNode = xNodeMAIN.SelectSingleNode("RETURN_PREMIUM"); //xDoc.NextSibling.SelectSingleNode("RETURN_PREMIUM");
                    if (xNode != null)
                    {
                        xNodeMAIN.RemoveChild(xNode);
                        hidOldData.Value = xDoc.InnerXml.ToString();
                    }
                }

                #endregion
                //Added by Lalit .Dec- 8 -2011.tfs # 463.
                //disable cancellation date field  after save .if cancellation Type is Non Pay
                //Start

                if (dsTemp.Tables[0].Rows[0]["CANCELLATION_TYPE"]!=null  && dsTemp.Tables[0].Rows[0]["CANCELLATION_TYPE"].ToString() != "") 
                {
                    if (dsTemp.Tables[0].Rows[0]["CANCELLATION_TYPE"].ToString() == "11969")//if cancellation Type is non Pay
                        txtEFFECTIVE_DATETIME.Attributes.Add("onkeydown", "return ReadOnly(this)");
                }
                //End here
            }
        }


        private void FillhidOldData()
        {

            DataSet dsTemp = ClsPolicyProcess.GetProcessInfoDataSet(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value),
                int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidROW_ID.Value));

            hidOldData.Value = dsTemp.GetXml();


        }

        private void btnPolicyDetails_Click(object sender, System.EventArgs e)
        {

        }

        private void btnSave_Send_Notice_Click(object sender, System.EventArgs e)
        {
            //for save data 
            try
            {
                Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();

                objProcessInfo = GetFormValues();
                objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value);

                //Making model object which will contains old data
                Cms.Model.Policy.Process.ClsProcessInfo objOldProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
                base.PopulateModelObject(objOldProcessInfo, hidOldData.Value);

                //Updating the previous process record
                objClsCancellationProcess.BeginTransaction();
                objClsCancellationProcess.UpdateProcessInformation(objOldProcessInfo, objProcessInfo);
                objClsCancellationProcess.CommitTransaction();

                //for sending notice(pdf file)
                objClsCancellationProcess.AddTransactionAndNotice(objProcessInfo);

                lblMessage.Text = ClsMessages.FetchGeneralMessage("897");
                lblMessage.Visible = true;
                hidFormSaved.Value = "1";
                LoadData();

                //Refresh the Policy Top.
                cltPolicyTop.CallPageLoad();
            }
            catch (Exception objExp)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
                lblMessage.Text = objExp.Message.ToString();
                lblMessage.Visible = true;
                objClsCancellationProcess.RollbackTransaction();
            }

        }

        private void btnGenerate_cancellation_notice_or_Memo_Click(object sender, System.EventArgs e)
        {
            SetReturnPremiumAmount();

            if (hidNOTICE_CLICK.Value == "1")
            {
                hidNOTICE_CLICK.Value = "0";
                return;
            }
            try
            {
                Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
                //objProcessInfo = objClsCancellationProcess.GetRunningProcess(int.Parse(hidCUSTOMER_ID.Value) ,int.Parse(hidPOLICY_ID.Value))   ;
                objProcessInfo = GetFormValues();
                if (hidSTATE_ID.Value != "" && hidSTATE_ID.Value != "0")
                    objProcessInfo.STATE_ID = int.Parse(hidSTATE_ID.Value);
                if (hidBILL_TYPE.Value != "" && hidBILL_TYPE.Value != "0")
                    objProcessInfo.BILL_TYPE = hidBILL_TYPE.Value;
                if (hidAGENCY_CODE.Value != "" && hidAGENCY_CODE.Value != "0")
                    objProcessInfo.AGENCY_CODE = hidAGENCY_CODE.Value;
                if (hidSTATE_CODE.Value != "" && hidSTATE_CODE.Value != "0")
                    objProcessInfo.STATE_CODE = hidSTATE_CODE.Value;
                objProcessInfo.LOB_ID = int.Parse(hidLOB_ID.Value);
                objProcessInfo.CREATED_BY = objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
                //saving Process screen Data Also
                if (hidPOLICY_PREVIOUS_STATUS.Value != "")
                    objProcessInfo.POLICY_PREVIOUS_STATUS = hidPOLICY_PREVIOUS_STATUS.Value;
                if (hidPOLICY_CURRENT_STATUS.Value != "")
                    objProcessInfo.POLICY_CURRENT_STATUS = hidPOLICY_CURRENT_STATUS.Value;
                if (hidCANCELLATION_NOTICE_SENT.Value != "")
                    objProcessInfo.CANCELLATION_NOTICE_SENT = hidCANCELLATION_NOTICE_SENT.Value;
                objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value);

                //Making model object which will contain old data
                Cms.Model.Policy.Process.ClsProcessInfo objOldProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();
                base.PopulateModelObject(objOldProcessInfo, hidOldData.Value);

                string strEffTime = ClsCommon.FetchValueFromXML("EFFECTIVETIME", hidOldData.Value);
                if (strEffTime != "")
                {
                    objOldProcessInfo.EFFECTIVE_DATETIME = Convert.ToDateTime(strEffTime);
                }
                //Updating the previous process record
                objClsCancellationProcess.BeginTransaction();
                objClsCancellationProcess.UpdateProcessInformation(objOldProcessInfo, objProcessInfo);
                //objClsCancellationProcess.CommitTransaction();
                //

                //objProcessInfo.COMPLETED_DATETIME = System.DateTime.Now;			
                if (objClsCancellationProcess.GenerateCancellationNotices(objProcessInfo) == true)
                {
                    //hidFormSaved.Value = "1";
                    hidDisplayBody.Value = "True";
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1265");//"Cancellation Notices have been generated successfully.";//ClsMessages.FetchGeneralMessage("683");
                    hidNEW_POLICY_VERSION_ID.Value = objProcessInfo.NEW_POLICY_VERSION_ID.ToString();
                    cltPolicyTop.CallPageLoad();
                    cltPolicyTop.RefreshPolicy();
                    if (objProcessInfo.CANCELLATION_NOTICE_SENT == "Y")
                    {
                        btnSaveFlag = false;
                        btnCommitFlag = true;
                        hidCANCELLATION_NOTICE_SENT.Value = "Y";
                    }
                    else
                    {
                        btnSaveFlag = true;
                        btnCommitFlag = false;

                    }
                    HideButtonsForNotices();
                    //Generating the xml of old data
                    FillhidOldData();
                }
                else
                {
                    //Hiding the extra buttons
                    HideButtons();
                    //hidDisplayBody.Value = "False";
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1270");//"Notices could not be generated.Please Try later.";//ClsMessages.FetchGeneralMessage("594"); //to be changed
                }

                lblMessage.Visible = true;

                //Saving the session and refreshing the menu
                LoadData();
                SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
            }
            catch (Exception objExp)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
                lblMessage.Text = objExp.Message.ToString();
                lblMessage.Visible = true;

                //Hiding the extra buttons
                HideButtons();
            }
        }


        /// <summary>
        /// Added on 5 June 2008
        /// Added by : Praveen
        /// Calculates the Return Premium.
        /// </summary>
        private void SetReturnPremiumAmount()
        {
            double retAmt = 0.00;
            try
            {

                int CustomerId = int.Parse(hidCUSTOMER_ID.Value.ToString().Trim());
                int PolicyId = int.Parse(hidPOLICY_ID.Value.ToString().Trim());
                int PolicyVersionId = int.Parse(hidPOLICY_VERSION_ID.Value.ToString().Trim());
                DateTime ChangeEffDate = DateTime.Parse(txtEFFECTIVE_DATETIME.Text.ToString().Trim());
                int CancellationType = int.Parse(cmbCANCELLATION_TYPE.SelectedValue.ToString().Trim());
                int CancellatoinOption = int.Parse(cmbCANCELLATION_OPTION.SelectedValue.ToString().Trim());
                //Calculate Return Premium For all Conacelation types 
                //if(CancellationType == 11971 || CancellationType == 11988 || CancellationType == 11989)

                if (CancellationType == (int)ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.INSURED_REQUEST)
                    retAmt = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.CalculateReturnPremium(CustomerId, PolicyId, PolicyVersionId, ChangeEffDate, CancellationType, CancellatoinOption);
                else if (CancellationType == (int)ClsPolicyProcess.enumPROCESS_CANCELLATION_TYPES.COMPANY_REQUEST)
                    retAmt = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.CalculateProRataAmount(CustomerId, PolicyId, PolicyVersionId, ChangeEffDate);



                if (retAmt.ToString() != "")
                    txtRETURN_PREMIUM.Text = retAmt.ToString().Trim();


            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }


        }


        [Ajax.AjaxMethod()]
        public string AjaxCancelProcReturnPremiumAmount(string CustomerId, string PolicyId, string PolicyVersionId, string ChangeEffDate, string CancellationType, string CancellatoinOption)
        {
            Cms.CmsWeb.webservices.ClsWebServiceCommon obj = new Cms.CmsWeb.webservices.ClsWebServiceCommon();
            string result = "";
            result = obj.CancelProcReturnPremiumAmount(CustomerId, PolicyId, PolicyVersionId, ChangeEffDate, CancellationType, CancellatoinOption);
            return result;

        }
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public string PolicyCancellationReturnPremium(string CustomerId, string PolicyId, string PolicyVersionId, string ChangeEffDate, string CancellationType, string CancellatoinOption)
        {
            Cms.CmsWeb.webservices.ClsWebServiceCommon obj = new Cms.CmsWeb.webservices.ClsWebServiceCommon();
            SetCultureThread(GetLanguageCode());
            string result = "";
            try
            {
                if (GetTransaction_Type() != MASTER_POLICY)
                {
                    result = obj.Get_PolicyCancellationReturnPremium(int.Parse(CustomerId), int.Parse(PolicyId), int.Parse(PolicyVersionId), ConvertToDate(ChangeEffDate));
                    //result = Math.Round(result, 0);
                }
                return result;//.ToString();
            }
            catch { return null; }
            //
        }

        [System.Web.Services.WebMethod(true)]
        public static string GetNonPayCancellationDate(string CUSTOMER_ID, string POLICY_ID, string POLICY_VERSION_ID)
        {
            //SetCultureThread(GetLanguageCode());
            try
            {
                DataTable dt = null;
                string UIdate = "";
                dt = ClsCancellationProcess.GetPolicyCancellationDate(int.Parse(CUSTOMER_ID), int.Parse(POLICY_ID), int.Parse(POLICY_VERSION_ID));

                if (dt != null && dt.Rows.Count > 0)
                    if (dt.Rows[0]["CANCELLATION_DATE"].ToString() != "")
                        UIdate = DateTime.Parse(dt.Rows[0]["CANCELLATION_DATE"].ToString()).ToShortDateString(); //ClsCommon.ConvertDBDateToCulture(dt.Rows[0]["CANCELLATION_DATE"].ToString());
                    else
                    {
                        UIdate = DateTime.Now.ToShortDateString();
                    }
                return UIdate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return UIdate.ToShortDateString();

        }
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public string CarrierRequestCancellation(string CustomerId, string PolicyId, string PolicyVersionId, string CancelDate)
        {
            Cms.CmsWeb.webservices.ClsWebServiceCommon obj = new Cms.CmsWeb.webservices.ClsWebServiceCommon();
            SetCultureThread(GetLanguageCode());
            string result="" ;
            try
            {
                if (GetTransaction_Type() != MASTER_POLICY)
                {
                    result = obj.Get_PolProrataAmount(int.Parse(CustomerId), int.Parse(PolicyId), int.Parse(PolicyVersionId), ConvertToDate(CancelDate));
                    //result = //Math.Round(result, 0);
                }
                return result.ToString();

            }
            catch { return null; }
            //
        }


    }
}
