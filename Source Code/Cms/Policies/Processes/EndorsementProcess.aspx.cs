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
using Cms.CmsWeb.Controls;
using Cms.CmsWeb;
using Cms.BusinessLayer.BlProcess;
using Cms.Model.Policy.Process;
using Cms.CmsWeb.WebControls;
using System.Xml;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlApplication;
using System.Globalization;// changed by praveer for TFS# 996

namespace Cms.Policies.Processes
{
    /// <summary>
    /// Summary description for EndorsementProcess.
    /// </summary>
    public class EndorsementProcess : Cms.Policies.Processes.Processbase
    {
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Label capEFFECTIVE_DATETIME;
        protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_DATETIME;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_DATETIME;
        protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_DATETIME;
        protected System.Web.UI.WebControls.Label capEXPIRY_DATE;
        protected System.Web.UI.WebControls.TextBox txtEXPIRY_DATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXPIRY_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revEXPIRY_DATE;
        protected System.Web.UI.WebControls.Label capPROPERTY_INSPECTION_CREDIT;
        protected System.Web.UI.WebControls.CheckBox chkPROPERTY_INSPECTION_CREDIT;
        protected System.Web.UI.WebControls.Label capCOMMENTS;
        protected System.Web.UI.WebControls.TextBox txtCOMMENTS;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidROW_ID;
        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPROCESS_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPROCESS_TYPE;
        protected Cms.CmsWeb.WebControls.PolicyTop cltPolicyTop;

        protected System.Web.UI.WebControls.HyperLink hlkEFFECTIVE_DATETIME;
        protected System.Web.UI.WebControls.HyperLink hlkEXPIRY_DATE;
        protected System.Web.UI.WebControls.CustomValidator csvCOMMENTS;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidENDORSEMENT_NO;
        protected Cms.CmsWeb.Controls.CmsButton btnRollback;
        protected Cms.CmsWeb.Controls.CmsButton btnCommit;
        protected System.Web.UI.WebControls.CompareValidator cmpEXPIRY_DATE;
        protected Cms.CmsWeb.Controls.CmsButton btnBack_To_Search;
        protected Cms.CmsWeb.Controls.CmsButton btnBack_To_Customer_Assistant;
        protected System.Web.UI.WebControls.CompareValidator cmpEXPIRY_DATE_POLICY_EXP_DATE;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        protected Cms.CmsWeb.Controls.CmsButton btnPolicyDetails;
        protected System.Web.UI.HtmlControls.HtmlTableRow trPROPERTY_INSPECTION_CREDIT;
        protected bool Saved = true;
        protected System.Web.UI.HtmlControls.HtmlTable tblMain;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidNEW_POLICY_VERSION_ID;
        protected Cms.CmsWeb.Controls.CmsButton btnGet_Premium;
        protected System.Web.UI.WebControls.PlaceHolder Placeholder1;
        protected System.Web.UI.WebControls.CompareValidator cmpEFFECTIVE_DATETIME;
        protected Cms.CmsWeb.Controls.CmsButton btnEffectivePremium;
        protected Cms.CmsWeb.Controls.CmsButton btnComitAynway;
        protected System.Web.UI.HtmlControls.HtmlGenericControl spnURStatus;
        protected System.Web.UI.HtmlControls.HtmlGenericControl myDIV;
        protected System.Web.UI.WebControls.Label capPRINTING_OPTIONS;
        protected System.Web.UI.WebControls.CheckBox chkPRINTING_OPTIONS;
        protected System.Web.UI.WebControls.Label capINSURED;
        protected System.Web.UI.WebControls.DropDownList cmbINSURED;

        protected System.Web.UI.WebControls.Label capAUTO_ID_CARD;
        protected System.Web.UI.WebControls.DropDownList cmbAUTO_ID_CARD;
        protected System.Web.UI.WebControls.Label capNO_COPIES;
        protected System.Web.UI.WebControls.TextBox txtNO_COPIES;
        protected System.Web.UI.WebControls.RangeValidator rngNO_COPIES;

        protected System.Web.UI.WebControls.Label capADD_INT;
        protected System.Web.UI.WebControls.DropDownList cmbADD_INT;
        protected System.Web.UI.WebControls.Label capSEND_ALL;
        protected System.Web.UI.WebControls.CheckBox chkSEND_ALL;
        protected System.Web.UI.WebControls.Label capUnassignLossCodes;
        protected System.Web.UI.WebControls.Label capAssignedLossCodes;
        protected System.Web.UI.WebControls.ListBox cmbUnAssignAddInt;
        protected System.Web.UI.WebControls.ListBox cmbAssignAddInt;
        protected System.Web.UI.WebControls.Label lblheader;
        protected System.Web.UI.WebControls.Label lblAdditional;
        protected System.Web.UI.WebControls.Label lblLetter;
        protected System.Web.UI.WebControls.Label lblPrinting;

        protected System.Web.UI.HtmlControls.HtmlInputButton btnAssignLossCodes;
        protected System.Web.UI.HtmlControls.HtmlInputButton btnUnAssignLossCodes;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCALLED_FROM;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidVEHICLE_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidADD_INT_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSTATE_CODE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOUNT;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidpopup;
        //bl object for interactio wil business layer

        protected System.Web.UI.WebControls.Label capINTERNAL_CHANGE;
        protected System.Web.UI.WebControls.CheckBox chkINTERNAL_CHANGE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPrinting_Options;
        protected System.Web.UI.WebControls.Label capADVERSE_LETTER_REQD;
        protected System.Web.UI.WebControls.DropDownList cmbADVERSE_LETTER_REQD;
        protected System.Web.UI.WebControls.Label capAGENCY_PRINT;
        protected System.Web.UI.WebControls.DropDownList cmbAGENCY_PRINT;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidUNDERWRITER;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDisplayBody;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidEFFECTIVE_DATETIME;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidEXPIRY_DATE;
        protected Cms.CmsWeb.Controls.CmsButton btnCommitInProgress;
        protected Cms.CmsWeb.Controls.CmsButton btnCommitAnywayInProgress;
        protected System.Web.UI.WebControls.Label capCOINSURANCE_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtCOINSURANCE_NUMBER;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_TYPE;
        protected System.Web.UI.WebControls.DropDownList cmbENDORSEMENT_TYPE;
        protected System.Web.UI.WebControls.Label capENDORSEMENT_TYPE;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvENDORSEMENT_TYPE;
        // protected System.Web.UI.WebControls.RequiredFieldValidator rfvENDORSEMENT_OPTION;
        //Added by Lalit Chauhan    Nov 16,2010 for date 

        protected System.Web.UI.WebControls.Label capHeader;
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidEND_EFFECTIVE_DATE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidEND_EXPIRY_DATE;
        // protected System.Web.UI.WebControls.DropDownList cmbENDORSEMENT_OPTION;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCARRIER_CODE;
        //protected System.Web.UI.HtmlControls.HtmlInputHidden hidEND_OPTION;
        //protected System.Web.UI.WebControls.Label capENDORSEMENT_OPTION;

        //Added By Lalit
        #region Endorsement Option
        public const string EFFECTIVE_PERIOD_END = "14833";
        public const string CARRY_FORWARD_END = "14834";
        public const string OVERRIDE_END = "14835";
        #endregion

        //Co-Applicant Field for master policy
        //i-track  #
        protected System.Web.UI.WebControls.Label capCO_APPLICANT_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCO_APPLICANT_ID;
        protected System.Web.UI.WebControls.DropDownList cmbCO_APPLICANT_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvCO_APPLICANT_ID;


        //Added By Lalit
        //for re-issue endorsement
        protected System.Web.UI.WebControls.DropDownList cmbSOURCE_VERSION_ID;
        protected System.Web.UI.WebControls.Label capSOURCE_VERSION_ID;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvSOURCE_VERSION_ID;
        protected System.Web.UI.WebControls.Label capENDORSEMENT_RE_ISSUE;
        protected System.Web.UI.WebControls.CheckBox chkENDORSEMENT_RE_ISSUE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSOURCE_VERSION_ID;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCONFM_MSG;
        System.Resources.ResourceManager objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Processes.EndorsementProcess", System.Reflection.Assembly.GetExecutingAssembly());
        ClsEndorsmentProcess objProcess = new ClsEndorsmentProcess();
        string strLob_Id;
        protected bool EligibleForProcess = true;
        protected string jmessage1;
        protected string jmessage2;
        private void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                //Setting the screen id

                Ajax.Utility.RegisterTypeForAjax(typeof(EndorsementProcess));
                ((cmsbase)this).ScreenId = "5000_26";

                //Setting the security xml of cmb button
                SetButtonsSecurityXML();
                //get Carrier information
                //end here

                cltPolicyTop.UseRequestVariables = false;

                btnComitAynway.Visible = false;
                spnURStatus.Visible = false;
                myDIV.Visible = false;

                if (!Page.IsPostBack)
                {
                    hidCARRIER_CODE.Value = GetSystemId().ToUpper();
                    btnPolicyDetails.Attributes.Add("onclick", "javascript:return ShowDetailsPolicy();");
                    cmbADD_INT.Attributes.Add("onChange", "javascript:return cmbADD_INT_Change();");
                    btnSave.Attributes.Add("onClick", "javascript:savePage();");
                    chkINTERNAL_CHANGE.Attributes.Add("onClick", "javascript: InternalChange();");
                    chkPRINTING_OPTIONS.Attributes.Add("onClick", "javascript: PrintOptions();");
                    chkSEND_ALL.Attributes.Add("onClick", "javascript: chkSEND_ALL_Change();");
                    btnCommit.Attributes.Add("onClick", "javascript:return HideShowCommitInProgress();");
                    btnComitAynway.Attributes.Add("onClick", "javascript:HideShowCommitAnywayInProgress();");
                    btnRollback.Attributes.Add("onClick", "javascript:HideShowCommit();");

                    //Fetching the query string values
                    GetQueryString();
                  
                    LoadDropDowns();

                   

                    strLob_Id = GetLOBID();
                    if (strLob_Id != "")
                    {
                        hidPOLICY_TYPE.Value = GetTransaction_Type(); //GetProduct_Type(int.Parse(strLob_Id));
                    }


                    //Setting the properties of validation controls
                    SetValidators();

                    //Setting the captions of label
                    SetCaptions();
                    PopulateCombobox();
                    //ClsMessages.SetCustomizedXml(GetLanguageCode());
                    capHeader.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1246");
                    capMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
                    //Setting the controls attribute
                    SetControlsAttribute();

                 

                    //bool isSuspenseEndorsement = false;
                    if (hidPROCESS_ID.Value == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS.ToString())
                    {
                        objProcess.BeginTransaction();
                        if (objProcess.CheckProcessEligibility(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidPROCESS_ID.Value)) == 1)
                        {
                            //Starting the process
                            SetDefaultValues();
                            if (StartProcess())
                            {
                                //Showing the default values from policy
                                verifyRule();
                                ShowPolicyDetails();
                                capPROPERTY_INSPECTION_CREDIT.Visible = false;
                                chkPROPERTY_INSPECTION_CREDIT.Visible = false;
                                GridHolder.Visible = true;
                            }
                            else
                            {
                                //Policy top will be show for policy passes in request
                                cltPolicyTop.UseRequestVariables = true;
                                cmpEXPIRY_DATE_POLICY_EXP_DATE.Visible = false;
                                cmpEFFECTIVE_DATETIME.Visible = false;
                            }
                        }
                        else
                        {
                            PopulateProcessInfo();
                            //Showing default values from policy
                            ShowPolicyDetails();
                        }
                        objProcess.CommitTransaction();
                    }
                    else
                    {

                        PopulateProcessInfo();
                        //Showing default values from policy
                        ShowPolicyDetails();
                        if (Convert.ToInt32(hidPROCESS_ID.Value) != ClsPolicyProcess.POLICY_ROLLBACK_ENDORSEMENT_PROCESS)
                            this.verifyRule();

                        //Check for suspense endorsement
                        if (Convert.ToInt32(hidPROCESS_ID.Value) == ClsPolicyProcess.POLICY_SUSPENSE_ENDORSEMENT_PROCESS)
                        {
                            //suspense endorsement jobs.
                            ClsProcessInfo objProcessinfo = new ClsProcessInfo();
                            objProcessinfo = GetFormValues();
                            bool retVal = objProcess.SuspenseEndorsementJobs(objProcessinfo);
                            if (retVal)
                            {
                                lblMessage.Text = ClsMessages.FetchGeneralMessage("904"); ;
                                lblMessage.Visible = true;
                                SetPolicyInSession(objProcessinfo.POLICY_ID, objProcessinfo.NEW_POLICY_VERSION_ID, objProcessinfo.CUSTOMER_ID);

                            }
                        }

                        //Populating the currently executing process information


                        //Refresh the Policy Top.
                        //cltPolicyTop.CallPageLoad();

                        //Only display if the LOB is HomeOwners
                        if (GetLOBString().ToString().ToUpper().Trim() == "RENT")
                        {
                            capPROPERTY_INSPECTION_CREDIT.Visible = false;
                            chkPROPERTY_INSPECTION_CREDIT.Visible = false;
                        }
                        else
                        {
                            capPROPERTY_INSPECTION_CREDIT.Visible = true;
                            chkPROPERTY_INSPECTION_CREDIT.Visible = true;
                        }
                    }


                    //Ravindra(08-31-2006) Moved Generate Policy Quote related code to the page load of 
                    // Quote.aspx
                    string JavascriptText = "window.open('/cms/application/Aspx/Quote/Quote.aspx?CALLEDFROM=QUOTE_POL&CUSTOMER_ID=" + hidCUSTOMER_ID.Value + "&POLICY_ID=" + hidPOLICY_ID.Value + "&POLICY_VERSION_ID=" + hidNEW_POLICY_VERSION_ID.Value + "&LOBID=" + GetLOBID() + "&QUOTE_ID=" + "0" + "&ONLYEFFECTIVE=" + "false" + "&SHOW=" + "0" + "','Quote','resizable=yes,scrollbars=yes,left=150,top=50,width=700,height=600');";
                    btnGet_Premium.Attributes.Add("onClick", JavascriptText + "return false;");

                    if (hidNEW_POLICY_VERSION_ID.Value == "" || hidNEW_POLICY_VERSION_ID.Value == "0") { hidNEW_POLICY_VERSION_ID.Value = hidPOLICY_VERSION_ID.Value; }
                    string EffectiveClickJS = "window.open('/cms/application/Aspx/Quote/Quote.aspx?CALLEDFROM=QUOTE_POL&CUSTOMER_ID=" + hidCUSTOMER_ID.Value + "&POLICY_ID=" + hidPOLICY_ID.Value + "&POLICY_VERSION_ID=" + hidPOLICY_VERSION_ID.Value + "&NEW_POLICY_VERSION_ID=" + hidNEW_POLICY_VERSION_ID.Value + "&LOBID=" + GetLOBID() + "&QUOTE_ID=" + "0" + "&ONLYEFFECTIVE=" + "true" + "&SHOW=" + "0" + "&EFFECTIVE_DATETIME=" + hidEFFECTIVE_DATETIME.Value + "&EXPIRY_DATE=" + hidEXPIRY_DATE.Value + "','Quote','resizable=yes,scrollbars=yes,left=150,top=50,width=700,height=600');";
                    //btnEffectivePremium.Attributes.Add("onClick",EffectiveClickJS + "return false;");
                    //btnEffectivePremium.Attributes.Add("onClick","EffectiveClickJS(); return false;");
                    //Added By Ravindra Ends Here


                  
               
                }


                //Added By Shikha Chourasiya
                //-------------Start--------
               
                hidCOUNT.Value = objProcess.rein_Install(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value));
              

                // ------------End-----------

                //Setting the policy top controls setting 
                cltPolicyTop.CustomerID = int.Parse(hidCUSTOMER_ID.Value);
                cltPolicyTop.PolicyID = int.Parse(hidPOLICY_ID.Value);
                if (hidNEW_POLICY_VERSION_ID.Value.Trim() != "")
                    cltPolicyTop.PolicyVersionID = int.Parse(hidNEW_POLICY_VERSION_ID.Value);
                else
                    cltPolicyTop.PolicyVersionID = int.Parse(hidPOLICY_VERSION_ID.Value);
                //Refresh the Policy Top.
                cltPolicyTop.CallPageLoad();
                // printing options
                //				if (chkINTERNAL_CHANGE.Checked == true)
                //				{
                //					chkPRINTING_OPTIONS.Checked =true;
                //					chkPRINTING_OPTIONS.Enabled =false;
                //				}
                //Binding the endorsement log grid
                BindGrid();
                //messages

                jmessage1 = ClsMessages.FetchGeneralMessage("1944");
                jmessage2 = ClsMessages.FetchGeneralMessage("1945");

                // if (hidEND_OPTION.Value == CARRY_FORWARD_END || hidEND_OPTION.Value == OVERRIDE_END)
                //    btnSave.Attributes.Add("style", "display:none");
            }

            catch (Exception objExp)
            {
                lblMessage.Text = objExp.Message.ToString();
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
                lblMessage.Visible = true;
            }
        }
        private void LoadDropDowns()
        {

            IList ListSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PRNTOP");

            ListItem iListItem = null;
            if (hidLOB_ID.Value == ((int)enumLOB.AUTOP).ToString() || hidLOB_ID.Value == ((int)enumLOB.CYCL).ToString())
            {
                cmbAUTO_ID_CARD.DataSource = ListSource;
                cmbAUTO_ID_CARD.DataTextField = "LookupDesc";
                cmbAUTO_ID_CARD.DataValueField = "LookupID";
                cmbAUTO_ID_CARD.DataBind();

                iListItem = cmbAUTO_ID_CARD.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS.DOWNLOAD).ToString());
                if (iListItem != null)
                    cmbAUTO_ID_CARD.Items.Remove(iListItem);
            }
            cmbINSURED.DataSource = ListSource;
            cmbINSURED.DataTextField = "LookupDesc";
            cmbINSURED.DataValueField = "LookupID";
            cmbINSURED.DataBind();

            //MICHIGAN_MAILERS #ITRACK 4068	(Allow MICHIGAN_MAILERS in cmbINSURED)		
            //iListItem = cmbINSURED.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS.MICHIGAN_MAILERS).ToString());
            //if(iListItem!=null)
            //cmbINSURED.Items.Remove(iListItem);

            cmbAGENCY_PRINT.DataSource = ListSource;
            cmbAGENCY_PRINT.DataTextField = "LookupDesc";
            cmbAGENCY_PRINT.DataValueField = "LookupID";
            cmbAGENCY_PRINT.DataBind();

            //MICHIGAN_MAILERS #ITRACK 4068	(Allow MICHIGAN_MAILERS in cmbAGENCY_PRINT)		
            //iListItem = null;
            //iListItem = cmbAGENCY_PRINT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS.MICHIGAN_MAILERS).ToString());
            //if(iListItem!=null)
            //cmbAGENCY_PRINT.Items.Remove(iListItem);

            cmbADVERSE_LETTER_REQD.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
            cmbADVERSE_LETTER_REQD.DataTextField = "LookupDesc";
            cmbADVERSE_LETTER_REQD.DataValueField = "LookupID";
            cmbADVERSE_LETTER_REQD.DataBind();

            cmbADD_INT.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PRNTOP");
            cmbADD_INT.DataTextField = "LookupDesc";
            cmbADD_INT.DataValueField = "LookupID";
            cmbADD_INT.DataBind();
            iListItem = null;

            //MICHIGAN_MAILERS #Itrack 4068 (Allow MICHIGAN_MAILERS in cmbADD_INT)
            //iListItem = cmbADD_INT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS.MICHIGAN_MAILERS).ToString());
            //if(iListItem!=null)
            //cmbADD_INT.Items.Remove(iListItem);

            iListItem = cmbADD_INT.Items.FindByValue(((int)clsprocess.enumPROC_PRINT_OPTIONS.DOWNLOAD).ToString());
            if (iListItem != null)
                cmbADD_INT.Items.Remove(iListItem);

            /*cmbCUSTOM_LETTER_REQD.DataSource=Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
            cmbCUSTOM_LETTER_REQD.DataTextField="LookupDesc";
            cmbCUSTOM_LETTER_REQD.DataValueField="LookupID";
            cmbCUSTOM_LETTER_REQD.DataBind();*/

            DataTable dtAddIntList = null;
            string PolicyVersionId = "0";
            if (hidNEW_POLICY_VERSION_ID.Value != "" && hidNEW_POLICY_VERSION_ID.Value != "0")
                PolicyVersionId = hidNEW_POLICY_VERSION_ID.Value;
            else
                PolicyVersionId = hidPOLICY_VERSION_ID.Value;
            Cms.BusinessLayer.BlApplication.ClsAdditionalInterest objAdditionalInterest = new Cms.BusinessLayer.BlApplication.ClsAdditionalInterest();
            dtAddIntList = objAdditionalInterest.GetAdditionalInterestList(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(PolicyVersionId), int.Parse(hidLOB_ID.Value));
            if (dtAddIntList != null && dtAddIntList.Rows.Count > 0)
            {
                cmbUnAssignAddInt.DataSource = dtAddIntList;
                cmbUnAssignAddInt.DataTextField = "ADD_INT_DETAILS";
                cmbUnAssignAddInt.DataValueField = "ADD_INT_ID";
                cmbUnAssignAddInt.DataBind();
            }

            //Adde By Lalit for Out Of secuence Endorsement
            //cmbENDORSEMENT_OPTION.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("ENOPTS");
            //cmbENDORSEMENT_OPTION.DataTextField = "LookupDesc";
            //cmbENDORSEMENT_OPTION.DataValueField = "LookupID";
            //cmbENDORSEMENT_OPTION.DataBind();
            //cmbENDORSEMENT_OPTION.Items.Insert(0, "");


            //Fill Cao-Applicant Name for master policy
            string PolicyVersionID;
            if (hidNEW_POLICY_VERSION_ID.Value != "" && hidNEW_POLICY_VERSION_ID.Value != "0")
                PolicyVersionID = hidNEW_POLICY_VERSION_ID.Value;
            else
                PolicyVersionID = hidPOLICY_VERSION_ID.Value;
            DataTable dt = ClsGeneralInformation.CheckApplicantForPolicy(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(PolicyVersionID));
            cmbCO_APPLICANT_ID.DataSource = dt;
            cmbCO_APPLICANT_ID.DataTextField = "APPLICANTNAME";
            cmbCO_APPLICANT_ID.DataValueField = "APPLICANT_ID";
            cmbCO_APPLICANT_ID.DataBind();
            cmbCO_APPLICANT_ID.Items.Insert(0, "");



            //Added By Lalit for Out Of secuence Endorsement
            ClsGeneralInformation objGeneralInformation = new ClsGeneralInformation();
            DataSet ds = objGeneralInformation.GetPolicyEndorsementVersions(int.Parse(hidCUSTOMER_ID.Value),
                int.Parse(hidPOLICY_ID.Value),
                int.Parse(hidPOLICY_VERSION_ID.Value),
                txtEFFECTIVE_DATETIME.Text == "" ? DateTime.Now : DateTime.Parse(txtEFFECTIVE_DATETIME.Text), "REISSUE");

            cmbSOURCE_VERSION_ID.DataSource = ds; //Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("ENOPTS");
            cmbSOURCE_VERSION_ID.DataTextField = "ENDORSEMENT_DESC";
            cmbSOURCE_VERSION_ID.DataValueField = "END_INFO";
            cmbSOURCE_VERSION_ID.DataBind();
            cmbSOURCE_VERSION_ID.Items.Insert(0, "");


        }

        private void SetDefaultValues()
        {
            chkSEND_ALL.Checked = true;
            hidADD_INT_ID.Value = "";
            for (int i = 0; i < cmbUnAssignAddInt.Items.Count; i++)
            {
                hidADD_INT_ID.Value += cmbUnAssignAddInt.Items[i].Value.ToString() + "~";
            }
            if (hidADD_INT_ID.Value != "")
                hidADD_INT_ID.Value = hidADD_INT_ID.Value.TrimEnd('~');
        }

        private void BindGrid()
        {
            #region GETTING BASE COLOR FOR ROW SELECTION
            string colorScheme = GetColorScheme();
            string colors = "";

            switch (int.Parse(colorScheme))
            {
                case 1:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();
                    break;
                case 2:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR2").ToString();
                    break;
                case 3:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR3").ToString();
                    break;
                case 4:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR4").ToString();
                    break;
            }

            if (colors != "")
            {
                string[] baseColor = colors.Split(new char[] { ',' });
                if (baseColor.Length > 0)
                    colors = "#" + baseColor[0];
            }
            #endregion


            #region Binding the grid

            Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
            BaseDataGrid objWebGrid;
            objWebGrid = (BaseDataGrid)c1;

            try
            {
                //Setting web grid control properties
                objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

                objWebGrid.SelectClause = "PLED.ENDORSEMENT_NO ENDRSE_NO, Convert(varchar(10),PLED.ENDORSEMENT_DATE,case " + GetLanguageID() + @" when 2 then 103 else 101 end) as  ENDORSEMENT_DATE, ISNULL(MLVM.LOOKUP_VALUE_DESC,LOOKUP.LOOKUP_VALUE_DESC) ENDORSEMENT_TYPE, PLED.ENDORSEMENT_DESC, CASE WHEN " + ClsCommon.BL_LANG_ID + "=2 THEN CASE ENDORSEMENT_STATUS WHEN 'COM' THEN 'Completo' WHEN 'CAN' THEN 'Cancelar' ELSE 'Pendentes' END  ELSE CASE ENDORSEMENT_STATUS WHEN 'COM' THEN 'Complete' WHEN 'CAN' THEN 'Rolled Back' ELSE 'Pending' END END  ENDORSEMENT_STATUS,ISNULL(UL.USER_FNAME,'') + ' ' + ISNULL(UL.USER_LNAME,'') AS USER_NAME,TRANS_ID";

                objWebGrid.FromClause = "POL_POLICY_ENDORSEMENTS_DETAILS PLED "
                    + " LEFT JOIN POL_POLICY_ENDORSEMENTS PLE ON PLE.ENDORSEMENT_NO = PLED.ENDORSEMENT_NO AND PLE.POLICY_ID = PLED.POLICY_ID AND PLE.POLICY_VERSION_ID = PLED.POLICY_VERSION_ID AND  PLE.CUSTOMER_ID = PLED.CUSTOMER_ID"
                    //+ " INNER JOIN POL_POLICY_PROCESS PPP ON PLED.CUSTOMER_ID = PLED.CUSTOMER_ID AND PLED.POLICY_ID = PPP.POLICY_ID  AND PPP.CREATED_DATETIME= (SELECT TOP 1 POL1.CREATED_DATETIME FROM POL_POLICY_PROCESS POL1 WHERE PLED.CUSTOMER_ID = POL1.CUSTOMER_ID  AND PLED.POLICY_ID = POL1.POLICY_ID  AND POL1.CREATED_DATETIME < PLED.CREATED_DATETIME ORDER BY POL1.CREATED_DATETIME DESC) "
                    //					+ " LEFT JOIN POL_POLICY_PROCESS PPP ON PPP.ENDORSEMENT_NO = PLED.ENDORSEMENT_NO AND PPP.POLICY_ID = PLED.POLICY_ID AND PPP.POLICY_VERSION_ID = PLED.POLICY_VERSION_ID AND  PPP.CUSTOMER_ID = PLED.CUSTOMER_ID "
                    + " LEFT JOIN MNT_LOOKUP_VALUES LOOKUP ON LOOKUP.LOOKUP_UNIQUE_ID = PLED.ENDORSEMENT_TYPE "
                    + " LEFT JOIN MNT_USER_LIST UL ON UL.USER_ID = PLED.CREATED_BY"
                    + " LEFT JOIN MNT_LOOKUP_VALUES_MULTILINGUAL MLVM ON MLVM.LOOKUP_UNIQUE_ID=PLED.ENDORSEMENT_TYPE AND MLVM.LANG_ID=" + GetLanguageID();
                objWebGrid.WhereClause = "PLED.POLICY_ID=" + hidPOLICY_ID.Value
                        + " AND PLED.CUSTOMER_ID =" + hidCUSTOMER_ID.Value;


                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Endorsement No^Endorsement Date^Endorsement Type^Description^Status";
                objWebGrid.SearchColumnNames = "PLED.ENDORSEMENT_NO^CONVERT(VARCHAR(50),PLED.ENDORSEMENT_DATE,109)^ENDORSEMENT_TYPE^ENDORSEMENT_DESC^CASE ENDORSEMENT_STATUS WHEN 'COM' THEN 'Complete' WHEN 'CAN' THEN 'Rolled Back' ELSE 'Pending' END";
                objWebGrid.DisplayColumnNames = "ENDRSE_NO^ENDORSEMENT_DATE^ENDORSEMENT_TYPE^ENDORSEMENT_DESC^ENDORSEMENT_STATUS^USER_NAME";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"End. #^Date^Endors. Type^Description^Status^User Name";
                objWebGrid.SearchColumnType = "T^T^T^T^T";
                objWebGrid.OrderByClause = "ENDORSEMENT_DATE DESC";
                objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6";


                objWebGrid.DisplayTextLength = "5^20^75^75^^25^50";
                objWebGrid.DisplayColumnPercent = "5^20^20^40^10^30";
                objWebGrid.PrimaryColumns = "1";
                objWebGrid.PrimaryColumnsName = "ENDORSEMENT_DETAIL_ID";

                objWebGrid.ColumnTypes = "B^B^B^B^B^B";
                //objWebGrid.HeaderString = "Driver Information";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Endorsement Log";


                objWebGrid.AllowDBLClick = "true";
                objWebGrid.FetchColumns = "1^2^3^4^5";

                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";

                //objWebGrid.ExtraButtons = "1^Add New^0~1^addRecord~copyApplicant~CoypApplicationDrivers";

                objWebGrid.PageSize = int.Parse(GetPageSize());
                objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";

                objWebGrid.SelectClass = colors;
                objWebGrid.RequireQuery = "Y";
                objWebGrid.DefaultSearch = "Y";
                objWebGrid.QueryStringColumns = "TRANS_ID";

                //Adding to controls to gridholder
                GridHolder.Controls.Add(c1);
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
            #endregion

        }

        /// <summary>
        /// Makes the property inspection credit field visible or invisible 
        /// </summary>
        private void VisibleInvisiblePropertyInspectionCredit(int PolicyID, int CustomerID, int PolicyVerID, int PrevVersionID)
        {
            try
            {
                if (GetLOBID() == LOB_HOME || GetLOBID() == LOB_RENTAL_DWELLING)
                {
                    objProcess = new ClsEndorsmentProcess();

                    //Calling the bl function for chking whether property inspection credit required or not
                    bool RetValue = objProcess.VisbilePropertyInspectionCredit(CustomerID, PolicyID, PolicyVerID, PrevVersionID);

                    capPROPERTY_INSPECTION_CREDIT.Visible = RetValue;
                    chkPROPERTY_INSPECTION_CREDIT.Visible = RetValue;
                    trPROPERTY_INSPECTION_CREDIT.Visible = RetValue;


                }
                else
                {

                    //Property inspection credit not required for lob other then HO and RENTAL
                    chkPROPERTY_INSPECTION_CREDIT.Visible = false;
                    capPROPERTY_INSPECTION_CREDIT.Visible = false;
                    trPROPERTY_INSPECTION_CREDIT.Visible = false;
                }


            }
            catch// (Exception objExp)
            {
            }
        }

        /// <summary>
        /// Shows the values from policy
        /// </summary>
        private void ShowPolicyDetails()
        {
            Cms.BusinessLayer.BlApplication.ClsGeneralInformation objPolicy;
            objPolicy = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();

            //Fetching the details of policy
            DataSet ds = objPolicy.GetPolicyDataSet(int.Parse(hidCUSTOMER_ID.Value)
                , int.Parse(hidPOLICY_ID.Value), int.Parse(hidNEW_POLICY_VERSION_ID.Value));

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (hidPOLICY_TYPE.Value == "")
                {
                    strLob_Id = ds.Tables[0].Rows[0]["POLICY_LOB"].ToString();
                    if (strLob_Id != "")
                    {
                        hidPOLICY_TYPE.Value = GetTransaction_Type();//GetProduct_Type(int.Parse(strLob_Id));
                    }
                }

                if (ds.Tables[0].Rows[0]["APP_EXPIRATION_DATE"] != DBNull.Value)
                {


                    txtEXPIRY_DATE.Text = ConvertDBDateToCulture(ds.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString()); //Convert.ToDateTime(ds.Tables[0].Rows[0]["APP_EXPIRATION_DATE"]).ToString("MM/dd/yyyy");

                    if (hidPOLICY_TYPE.Value == MASTER_POLICY)
                    { txtEXPIRY_DATE.ReadOnly = false; }
                    else
                    { txtEXPIRY_DATE.ReadOnly = true; }
                    //Setting the maximum values
                    cmpEXPIRY_DATE_POLICY_EXP_DATE.ValueToCompare = ConvertDBDateToCulture(ds.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString());
                }

                if (ds.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"] != DBNull.Value)
                {
                    //Setting the maximum values
                    cmpEFFECTIVE_DATETIME.ValueToCompare = ConvertDBDateToCulture(ds.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString());//Convert.ToDateTime(ds.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"]).ToString("MM/dd/yyyy");
                    ///hidEND_EFFECTIVE_DATE.Value = ConvertDBDateToCulture(ds.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString());
                }
                if (hidEND_EFFECTIVE_DATE.Value == "" && hidEND_EXPIRY_DATE.Value == "")
                {
                    if (ds.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString() != "")
                        hidEND_EFFECTIVE_DATE.Value = ConvertDBDateToCulture(ds.Tables[0].Rows[0]["APP_EFFECTIVE_DATE"].ToString());
                    if (ds.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString() != "")
                        hidEND_EXPIRY_DATE.Value = ConvertDBDateToCulture(ds.Tables[0].Rows[0]["APP_EXPIRATION_DATE"].ToString());
                }
            }
            //ds.Dispose();

        }

        /// <summary>
        /// Sets the security xml and type of button
        /// </summary>
        private void SetButtonsSecurityXML()
        {
            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnEffectivePremium.CmsButtonClass = CmsButtonType.Read;
            btnEffectivePremium.PermissionString = gstrSecurityXML;

            btnReset.CmsButtonClass = CmsButtonType.Read;
            btnReset.PermissionString = gstrSecurityXML;

            btnCommit.CmsButtonClass = CmsButtonType.Write;
            btnCommit.PermissionString = gstrSecurityXML;

            btnComitAynway.CmsButtonClass = CmsButtonType.Write;
            btnComitAynway.PermissionString = gstrSecurityXML;

            btnRollback.CmsButtonClass = CmsButtonType.Write;
            btnRollback.PermissionString = gstrSecurityXML;

            btnBack_To_Search.CmsButtonClass = CmsButtonType.Read;
            btnBack_To_Search.PermissionString = gstrSecurityXML;

            btnBack_To_Customer_Assistant.CmsButtonClass = CmsButtonType.Read;
            btnBack_To_Customer_Assistant.PermissionString = gstrSecurityXML;

            btnPolicyDetails.CmsButtonClass = CmsButtonType.Read;
            btnPolicyDetails.PermissionString = gstrSecurityXML;

            btnGet_Premium.CmsButtonClass = CmsButtonType.Read;
            btnGet_Premium.PermissionString = gstrSecurityXML;

            btnCommitInProgress.CmsButtonClass = CmsButtonType.Read;
            btnCommitInProgress.PermissionString = gstrSecurityXML;

            btnCommitAnywayInProgress.CmsButtonClass = CmsButtonType.Read;
            btnCommitAnywayInProgress.PermissionString = gstrSecurityXML;

        }

        /// <summary>
        /// Retreives the query string values into hidden controls
        /// </summary>
        private void GetQueryString()
        {

            hidCUSTOMER_ID.Value = Request.Params["CUSTOMER_ID"].ToString();
            hidPOLICY_ID.Value = Request.Params["POLICY_ID"].ToString();
            hidPOLICY_VERSION_ID.Value = Request.Params["policyVersionID"].ToString();

            if (Request.Params["process"].ToString().ToUpper() == "ENDORSE")
                hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS.ToString();
            else if (Request.Params["process"].ToString().ToUpper() == "CENDORSE")
            {
                hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS.ToString();
                btnRollback.Attributes.Add("STYLE", "DISPLAY:NONE");
            }
            else if (Request.Params["process"].ToString().ToUpper() == "RENDORSE")
            {
                hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ROLLBACK_ENDORSEMENT_PROCESS.ToString();
                btnComitAynway.Attributes.Add("STYLE", "DISPLAY:NONE");
                btnCommit.Attributes.Add("STYLE", "DISPLAY:NONE");
            }
            else if (Request.Params["process"].ToString().ToUpper() == "SENDORSE")
            {
                hidPROCESS_ID.Value = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_SUSPENSE_ENDORSEMENT_PROCESS.ToString();
                btnRollback.Attributes.Add("STYLE", "DISPLAY:NONE");
            }

            //hidLOB_ID.Value = GetLOBID();
            Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGeneralInformation = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
            DataSet dsPolicy = objGeneralInformation.GetPolicyDataSet(int.Parse(hidCUSTOMER_ID.Value == "" ? "0" : hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value == "" ? "0" : hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value == "" ? "0" : hidPOLICY_VERSION_ID.Value));

            if (dsPolicy.Tables[0].Rows[0]["UNDERWRITER"] != null && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString() != "" && dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString() != "0")
                hidUNDERWRITER.Value = dsPolicy.Tables[0].Rows[0]["UNDERWRITER"].ToString();

            hidLOB_ID.Value = dsPolicy.Tables[0].Rows[0]["POLICY_LOB"].ToString();

            if (dsPolicy.Tables[0].Rows[0]["STATE_CODE"] != null && dsPolicy.Tables[0].Rows[0]["STATE_CODE"].ToString() != "")
                hidSTATE_CODE.Value = dsPolicy.Tables[0].Rows[0]["STATE_CODE"].ToString().Trim();
            if (dsPolicy.Tables[0].Rows[0]["STATE_ID"] != null && dsPolicy.Tables[0].Rows[0]["STATE_ID"].ToString() != "")
                hidSTATE_ID.Value = dsPolicy.Tables[0].Rows[0]["STATE_ID"].ToString().Trim();

            //hidPROCESS_ID.Value		= Request.Params["PROCESS_ID"];
        }

        /// <summary>
        /// Starts the process by calling the StartProcess method of ClsEndorsement
        /// </summary>
        private bool StartProcess()
        {
            try
            {
                bool RetVal = false;
                Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();

                objProcessInfo = GetFormValues();
                objProcessInfo.CREATED_BY = int.Parse(GetUserId());
                objProcessInfo.CREATED_DATETIME = DateTime.Now;

                ClsEndorsmentProcess objProcess = new ClsEndorsmentProcess();

                if (objProcess.StartProcess(objProcessInfo) == true)
                {
                    hidFormSaved.Value = "1";
                    //saved successfully
                    hidROW_ID.Value = objProcessInfo.ROW_ID.ToString();
                    hidENDORSEMENT_NO.Value = objProcessInfo.ENDORSEMENT_NO.ToString();
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("682");
                    hidNEW_POLICY_VERSION_ID.Value = objProcessInfo.NEW_POLICY_VERSION_ID.ToString();
                    hidDisplayBody.Value = "True";
                    //Generating the xml of old data
                    GetOldDataXml();

                    //Setting the new policy in session
                    SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);

                    RetVal = true;
                }
                else
                {
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("594");

                    //Hiding the commit and rollback buttons
                    HideButtons();
                    hidDisplayBody.Value = "False";
                    RetVal = false;
                }


                lblMessage.Visible = true;

                //Refresh the Policy Top.
                cltPolicyTop.CallPageLoad();

                return RetVal;
            }
            catch (Exception objExp)
            {
                //Publishing the exception
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1245");//"Unable to start Process.Please Try later.";
                lblMessage.Visible = true;
                //Hiding the commit and rollback buttons
                HideButtons();

                return false;
            }

        }

        /// <summary>
        /// Hides the commit and rollback button
        /// </summary>
        private void HideButtons()
        {
            btnSave.Attributes.Add("style", "display:none");
            btnCommit.Attributes.Add("style", "display:none");
            btnRollback.Attributes.Add("style", "display:none");
            btnCommitInProgress.Attributes.Add("style", "display:none");

        }


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
        /// Retreives the values from form and populates the model object
        /// </summary>
        /// <returns>Model object of ClsProcessInfo type</returns>
        /// 



        private void PopulateCombobox()
        {
            cmbENDORSEMENT_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("PPAEND");
            cmbENDORSEMENT_TYPE.DataValueField = "LookupID";
            cmbENDORSEMENT_TYPE.DataTextField = "LookupDesc";
            cmbENDORSEMENT_TYPE.DataBind();
            cmbENDORSEMENT_TYPE.Items.Insert(0, "");

            //Remove Cancellation Endorsement type from dropdown
            //Lalit Jan 19,2010
            for (int i = cmbENDORSEMENT_TYPE.Items.Count - 1; i >= 0; i--)
            {
                if (cmbENDORSEMENT_TYPE.Items[i].Value == "14685")
                    cmbENDORSEMENT_TYPE.Items.Remove(cmbENDORSEMENT_TYPE.Items[i]);
                else if (cmbENDORSEMENT_TYPE.Items[i].Value == "14686")
                    cmbENDORSEMENT_TYPE.Items.Remove(cmbENDORSEMENT_TYPE.Items[i]);
            }
            // cmbENDORSEMENT_TYPE.SelectedIndex = cmbENDORSEMENT_TYPE.Items.IndexOf(cmbENDORSEMENT_TYPE.Items.FindByValue("11619")); //default to Other


        }
        private ClsProcessInfo GetFormValues()
        {
            ClsProcessInfo objProcessInfo = new ClsProcessInfo();

            objProcessInfo.POLICY_ID = int.Parse(hidPOLICY_ID.Value);
            objProcessInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
            objProcessInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);

            //objProcess.PROCESS_ID = POLICY_ENDORSEMENT_PROCESS;
            if (txtEFFECTIVE_DATETIME.Text != "")
                objProcessInfo.EFFECTIVE_DATETIME = ConvertToDate(txtEFFECTIVE_DATETIME.Text);
            hidEFFECTIVE_DATETIME.Value = txtEFFECTIVE_DATETIME.Text;
            if (txtEXPIRY_DATE.Text != "")
                objProcessInfo.EXPIRY_DATE = ConvertToDate(txtEXPIRY_DATE.Text);
            hidEXPIRY_DATE.Value = txtEXPIRY_DATE.Text;
            objProcessInfo.PROPERTY_INSPECTION_CREDIT = chkPROPERTY_INSPECTION_CREDIT.Checked ? "Y" : "N";

            if (hidENDORSEMENT_NO.Value.Trim() != "")
                objProcessInfo.ENDORSEMENT_NO = int.Parse(hidENDORSEMENT_NO.Value);

            objProcessInfo.COMMENTS = txtCOMMENTS.Text;

            objProcessInfo.NEW_CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
            objProcessInfo.NEW_POLICY_ID = int.Parse(hidPOLICY_ID.Value);

            if (hidNEW_POLICY_VERSION_ID != null && hidNEW_POLICY_VERSION_ID.Value != "")
                objProcessInfo.NEW_POLICY_VERSION_ID = int.Parse(hidNEW_POLICY_VERSION_ID.Value);
            else
                objProcessInfo.NEW_POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);

            if (hidUNDERWRITER.Value != "" && hidUNDERWRITER.Value != "0")
                objProcessInfo.UNDERWRITER = Convert.ToInt16(hidUNDERWRITER.Value);

            objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
            objProcessInfo.CREATED_BY = int.Parse(GetUserId());

            objProcessInfo.POLICY_PREVIOUS_STATUS = objProcess.GetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID);
            objProcessInfo.POLICY_CURRENT_STATUS = objProcess.GetPolicyStatus(objProcessInfo.CUSTOMER_ID, objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID);

            if (chkINTERNAL_CHANGE.Checked)
                objProcessInfo.INTERNAL_CHANGE = Convert.ToString((int)(enumYESNO_LOOKUP_CODE.YES));
            else
                objProcessInfo.INTERNAL_CHANGE = Convert.ToString((int)(enumYESNO_LOOKUP_CODE.NO));

            objProcessInfo.PRINTING_OPTIONS = int.Parse(Convert.ToString(hidPrinting_Options.Value) == "" ? "0" : hidPrinting_Options.Value);

            if (cmbINSURED.SelectedItem != null && cmbINSURED.SelectedItem.Value != "")
                objProcessInfo.INSURED = int.Parse(cmbINSURED.SelectedItem.Value);

            if (cmbAGENCY_PRINT.SelectedItem != null && cmbAGENCY_PRINT.SelectedItem.Value != "")
                objProcessInfo.AGENCY_PRINT = int.Parse(cmbAGENCY_PRINT.SelectedItem.Value);

            if (cmbADVERSE_LETTER_REQD.SelectedItem != null && cmbADVERSE_LETTER_REQD.SelectedItem.Value != "")
                objProcessInfo.ADVERSE_LETTER_REQD = int.Parse(cmbADVERSE_LETTER_REQD.SelectedItem.Value);


            if (cmbADD_INT.SelectedItem != null && cmbADD_INT.SelectedItem.Value != "")
            {
                objProcessInfo.ADD_INT = int.Parse(cmbADD_INT.SelectedItem.Value);
                //MICHIGAN_MAILERS Itrack #4068 
                if (objProcessInfo.ADD_INT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString())
                    || objProcessInfo.ADD_INT == int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.MICHIGAN_MAILERS).ToString()))
                {
                    objProcessInfo.ADD_INT_ID = hidADD_INT_ID.Value;
                    if (chkSEND_ALL.Checked == true)
                        objProcessInfo.SEND_ALL = (int)(enumYESNO_LOOKUP_CODE.YES);
                    else
                        objProcessInfo.SEND_ALL = (int)(enumYESNO_LOOKUP_CODE.NO);
                }
            }
            if (hidOldData.Value == "" || hidOldData.Value == "0")
            {
                if (hidLOB_ID.Value == ((int)enumLOB.AUTOP).ToString() || hidLOB_ID.Value == ((int)enumLOB.CYCL).ToString())
                {
                    objProcessInfo.AUTO_ID_CARD = int.Parse(((int)clsprocess.enumPROC_PRINT_OPTIONS.COMMIT_PRINT_SPOOL).ToString());
                }
            }
            else
            {
                if (hidLOB_ID.Value == ((int)enumLOB.AUTOP).ToString() || hidLOB_ID.Value == ((int)enumLOB.CYCL).ToString())
                {
                    if (cmbAUTO_ID_CARD.SelectedItem != null && cmbAUTO_ID_CARD.SelectedItem.Value != "")
                        objProcessInfo.AUTO_ID_CARD = int.Parse(cmbAUTO_ID_CARD.SelectedItem.Value);
                }
            }
            if (hidLOB_ID.Value == ((int)enumLOB.AUTOP).ToString() || hidLOB_ID.Value == ((int)enumLOB.CYCL).ToString())
            {
                if (txtNO_COPIES.Text.Trim() != "")
                    objProcessInfo.NO_COPIES = int.Parse(txtNO_COPIES.Text.Trim());
                else
                    objProcessInfo.NO_COPIES = Cms.BusinessLayer.BlCommon.ClsCommon.PRINT_OPTIONS_AUTO_CYCL_NO_OF_COPIES;
            }

            if (hidLOB_ID.Value != "" && hidLOB_ID.Value != "0")
                objProcessInfo.LOB_ID = int.Parse(hidLOB_ID.Value);
            if (hidSTATE_CODE.Value != "")
                objProcessInfo.STATE_CODE = hidSTATE_CODE.Value;
            if (hidSTATE_ID.Value != "" && hidSTATE_ID.Value != "0")
                objProcessInfo.STATE_ID = int.Parse(hidSTATE_ID.Value);

            objProcessInfo.PROCESS_ID = Convert.ToInt16(hidPROCESS_ID.Value);

            if (hidROW_ID.Value != null && hidROW_ID.Value != "")
                objProcessInfo.ROW_ID = Convert.ToInt16(hidROW_ID.Value);

            if (txtCOINSURANCE_NUMBER.Text.Trim() != "")
                objProcessInfo.COINSURANCE_NUMBER = txtCOINSURANCE_NUMBER.Text.Trim();

            if (cmbENDORSEMENT_TYPE.SelectedItem != null && cmbENDORSEMENT_TYPE.SelectedValue != "")
                objProcessInfo.ENDORSEMENT_TYPE = int.Parse(cmbENDORSEMENT_TYPE.SelectedValue);

            //Added By Lalit New Property
            //if (cmbENDORSEMENT_OPTION.SelectedValue != "")
            //  objProcessInfo.ENDORSEMENT_OPTION = int.Parse(cmbENDORSEMENT_OPTION.SelectedValue);


            if (GetTransaction_Type() == MASTER_POLICY)
            {
                if (hidCO_APPLICANT_ID.Value != "")
                    objProcessInfo.CO_APPLICANT_ID = int.Parse(hidCO_APPLICANT_ID.Value);
            }
            else
            {
                string PolicyVersionId;
                if (hidNEW_POLICY_VERSION_ID.Value != "" && hidNEW_POLICY_VERSION_ID.Value != "0")
                    PolicyVersionId = hidNEW_POLICY_VERSION_ID.Value;
                else
                    PolicyVersionId = hidPOLICY_VERSION_ID.Value;
                objProcessInfo.CO_APPLICANT_ID = ClsGeneralInformation.GetPolicyPrimary_Applicant(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(PolicyVersionId));
            }

            if (chkENDORSEMENT_RE_ISSUE.Checked)
            {
                objProcessInfo.ENDORSEMENT_RE_ISSUE = (int)enumYESNO_LOOKUP_UNIQUE_ID.YES;
                string Info = hidSOURCE_VERSION_ID.Value;//cmbSOURCE_VERSION_ID.SelectedValue;
                if (Info != "")
                {
                    //objProcessInfo.POLICY_VERSION_ID = int.Parse(Info);
                    objProcessInfo.SOURCE_VERSION_ID = int.Parse(Info);
                    if (hidEND_EFFECTIVE_DATE.Value != "")
                        objProcessInfo.EFFECTIVE_DATETIME = ConvertToDate(hidEND_EFFECTIVE_DATE.Value);
                    if (hidEND_EXPIRY_DATE.Value != "")
                        objProcessInfo.EXPIRY_DATE = ConvertToDate(hidEND_EXPIRY_DATE.Value);

                }
            }
            objProcessInfo.PROCESS_TYPE = hidPROCESS_TYPE.Value;
            return objProcessInfo;
        }

        /// <summary>
        /// Sets the property of various validator controls
        /// </summary>
        private void SetValidators()
        {
            //ClsMessages.SetCustomizedXml(GetLanguageCode());
            revEFFECTIVE_DATETIME.ValidationExpression = aRegExpDate;
            revEXPIRY_DATE.ValidationExpression = aRegExpDate;

            revEFFECTIVE_DATETIME.ErrorMessage = ClsMessages.FetchGeneralMessage("22");
            revEXPIRY_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("22");

            rfvEFFECTIVE_DATETIME.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1243");//"Please enter endorsement effective date.";
            rfvEXPIRY_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1244");//"Please enter endorsement expiry date.";
            csvCOMMENTS.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2017");//"Please enter only 250 characters.";
            rngNO_COPIES.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");
            rfvENDORSEMENT_TYPE.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1180");
            cmpEFFECTIVE_DATETIME.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1310");
            //rfvENDORSEMENT_OPTION.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1311");
            cmpEXPIRY_DATE_POLICY_EXP_DATE.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1312");
            cmpEXPIRY_DATE.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1313");
            rfvSOURCE_VERSION_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1942");

            rfvCO_APPLICANT_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1179");
           
                hidpopup.Value = btnCommitInProgress.Text = ClsMessages.GetMessage(this.ScreenId, "1");

        }

        /// <summary>
        /// Sets the caption of Labels from resource file
        /// </summary>
        private void SetCaptions()
        {
            objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Processes.EndorsementProcess", System.Reflection.Assembly.GetExecutingAssembly());
            capCOMMENTS.Text = objResourceMgr.GetString("txtCOMMENTS");
            capEFFECTIVE_DATETIME.Text = objResourceMgr.GetString("txtEFFECTIVE_DATETIME");
            capEXPIRY_DATE.Text = objResourceMgr.GetString("txtEXPIRY_DATE");
            capPROPERTY_INSPECTION_CREDIT.Text = objResourceMgr.GetString("chkPROPERTY_INSPECTION_CREDIT");

            capNO_COPIES.Text = objResourceMgr.GetString("txtNO_COPIES");
            capAUTO_ID_CARD.Text = objResourceMgr.GetString("cmbAUTO_ID_CARD");

            capINSURED.Text = objResourceMgr.GetString("cmbINSURED");
            capAGENCY_PRINT.Text = objResourceMgr.GetString("cmbAGENCY_PRINT");
            capINTERNAL_CHANGE.Text = objResourceMgr.GetString("chkINTERNAL_CHANGE");
            capPRINTING_OPTIONS.Text = objResourceMgr.GetString("chkPRINTING_OPTIONS");

            capADD_INT.Text = objResourceMgr.GetString("cmbADD_INT");
            capSEND_ALL.Text = objResourceMgr.GetString("chkSEND_ALL");
            capCOINSURANCE_NUMBER.Text = objResourceMgr.GetString("capCOINSURANCE_NUMBER");
            capENDORSEMENT_TYPE.Text = objResourceMgr.GetString("capENDORSEMENT_TYPE");
            lblheader.Text = objResourceMgr.GetString("lblheader");
            lblAdditional.Text = objResourceMgr.GetString("lblAdditional");
            lblLetter.Text = objResourceMgr.GetString("lblLetter");
            lblPrinting.Text = objResourceMgr.GetString("lblPrinting");
            capAssignedLossCodes.Text = objResourceMgr.GetString("capAssignedLossCodes");
            capUnassignLossCodes.Text = objResourceMgr.GetString("capUnassignLossCodes");
            //ClsMessages.SetCustomizedXml(GetLanguageCode());
            btnEffectivePremium.Text = ClsMessages.GetButtonsText(ScreenId, "btnEffectivePremium");
            btnGet_Premium.Text = ClsMessages.GetButtonsText(ScreenId, "btnGet_Premium");
            btnCommit.Text = ClsMessages.GetButtonsText(ScreenId, "btnCommit");
            btnBack_To_Customer_Assistant.Text = ClsMessages.GetButtonsText(ScreenId, "btnBack_To_Customer_Assistant");
            btnBack_To_Search.Text = ClsMessages.GetButtonsText(ScreenId, "btnBack_To_Search");
            btnPolicyDetails.Text = ClsMessages.GetButtonsText(ScreenId, "btnPolicyDetails");
            btnRollback.Text = objResourceMgr.GetString("btnRollback");
            //capENDORSEMENT_OPTION.Text = objResourceMgr.GetString("cmbENDORSEMENT_OPTION");
            //Commented by Lalit feb 27,2011 to remove from transaction log
            //i-track # 661
            //capADVERSE_LETTER_REQD.Text = objResourceMgr.GetString("cmbADVERSE_LETTER_REQD");
            //ClsMessages.SetCustomizedXml(GetLanguageCode());
            capADVERSE_LETTER_REQD.Text = ClsMessages.FetchGeneralMessage("1444");

            hidCONFM_MSG.Value = ClsMessages.FetchGeneralMessage("1356");
            btnComitAynway.Text = ClsMessages.GetButtonsText(this.ScreenId, "btnComitAynway");

            capCO_APPLICANT_ID.Text = objResourceMgr.GetString("cmbCO_APPLICANT_ID");

            capENDORSEMENT_RE_ISSUE.Text = objResourceMgr.GetString("chkENDORSEMENT_RE_ISSUE");
            capSOURCE_VERSION_ID.Text = objResourceMgr.GetString("cmbSOURCE_VERSION_ID");
        }

        /// <summary>
        /// Retreives the old data in the form of xml and will keep in hidOldData hidden field
        /// </summary>
        private void GetOldDataXml()
        {
            if (hidROW_ID.Value.Trim() != "")
            {
                hidOldData.Value = objProcess.GetOldDataXml(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value),
                    int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidROW_ID.Value));
                string Eff_date = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("EFFECTIVE_DATETIME", hidOldData.Value);
                string Exp_date = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("EXPIRY_DATE", hidOldData.Value);
                if (Eff_date != "" && Exp_date != "")
                {
                    hidEND_EFFECTIVE_DATE.Value = ConvertDBDateToCulture(Eff_date);
                    hidEND_EXPIRY_DATE.Value = ConvertDBDateToCulture(Exp_date);
                    txtEFFECTIVE_DATETIME.Text = ConvertDBDateToCulture(Eff_date);
                    txtEXPIRY_DATE.Text = ConvertDBDateToCulture(Exp_date);
                }

                string END_REISSUE = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("ENDORSEMENT_RE_ISSUE", hidOldData.Value);
                if (END_REISSUE != "" && int.Parse(END_REISSUE) == (int)enumYESNO_LOOKUP_UNIQUE_ID.YES)
                {
                    chkENDORSEMENT_RE_ISSUE.Checked = true;
                    btnSave.Attributes.Add("style", "display:none");

                    string CO_APPLICANT_ID = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("CO_APPLICANT_ID", hidOldData.Value).ToString();
                    string value = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("SOURCE_VERSION_ID", hidOldData.Value).ToString() + "^" + CO_APPLICANT_ID;
                    cmbSOURCE_VERSION_ID.SelectedIndex = cmbSOURCE_VERSION_ID.Items.IndexOf(cmbSOURCE_VERSION_ID.Items.FindByValue(value));

                }


                #region Remove ENDORSEMENT_RE_ISSUE  node from Xml
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(hidOldData.Value);
                XmlNode xNodeMAIN = xDoc.SelectSingleNode("NewDataSet/Table");
                if (xNodeMAIN != null)
                {
                    XmlNode xNode = xNodeMAIN.SelectSingleNode("ENDORSEMENT_RE_ISSUE"); //xDoc.NextSibling.SelectSingleNode("RETURN_PREMIUM");
                    if (xNode != null)
                    {
                        xNodeMAIN.RemoveChild(xNode);
                        hidOldData.Value = xDoc.InnerXml.ToString();
                    }
                }

                #endregion
            }
        }

        /// <summary>
        /// Sets the controls attribute
        /// </summary>
        private void SetControlsAttribute()
        {
            hlkEFFECTIVE_DATETIME.Attributes.Add("OnClick", "fPopCalendar(document.EndorsementProcess.txtEFFECTIVE_DATETIME, document.EndorsementProcess.txtEFFECTIVE_DATETIME)");
            hlkEXPIRY_DATE.Attributes.Add("OnClick", "fPopCalendar(document.EndorsementProcess.txtEXPIRY_DATE, document.EndorsementProcess.txtEXPIRY_DATE)");
            btnReset.Attributes.Add("onclick", "javascript:return formReset();");
        }

        /// <summary>
        /// Sets the process information 
        /// </summary>
        private void PopulateProcessInfo()
        {
            ClsPolicyProcess objPro = new ClsPolicyProcess();
            ClsProcessInfo objProcessInfo = objPro.GetRunningProcess(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value));
            if (objProcessInfo != null)
            {
                hidPOLICY_VERSION_ID.Value = objProcessInfo.POLICY_VERSION_ID.ToString();
                hidROW_ID.Value = objProcessInfo.ROW_ID.ToString();
                hidNEW_POLICY_VERSION_ID.Value = objProcessInfo.NEW_POLICY_VERSION_ID.ToString();
                hidPrinting_Options.Value = objProcessInfo.PRINTING_OPTIONS.ToString();
                hidDisplayBody.Value = "True";
                GetOldDataXml();
                hidEFFECTIVE_DATETIME.Value = objProcessInfo.EFFECTIVE_DATETIME.ToString("MM/dd/yyyy");
                hidEXPIRY_DATE.Value = objProcessInfo.EXPIRY_DATE.ToString("MM/dd/yyyy");
                //hidEND_OPTION.Value = objProcessInfo.ENDORSEMENT_OPTION.ToString();//.EXPIRY_DATE.ToString("MM/dd/yyyy");
                //Saving the policy details in session
                SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);

                if (objProcessInfo.ENDORSEMENT_RE_ISSUE == (int)enumYESNO_LOOKUP_UNIQUE_ID.YES)
                {
                    chkENDORSEMENT_RE_ISSUE.Checked = true;
                    int CO_APPLICANT_ID = objProcessInfo.CO_APPLICANT_ID;
                    var Value = objProcessInfo.SOURCE_VERSION_ID.ToString() + "^" + CO_APPLICANT_ID;
                    cmbSOURCE_VERSION_ID.SelectedIndex = cmbSOURCE_VERSION_ID.Items.IndexOf(cmbSOURCE_VERSION_ID.Items.FindByValue(Value));


                }
                //Visibling the propertyinspection credit field
                VisibleInvisiblePropertyInspectionCredit(int.Parse(hidPOLICY_ID.Value)
                    , int.Parse(hidCUSTOMER_ID.Value), objProcessInfo.NEW_POLICY_VERSION_ID
                    , int.Parse(hidPOLICY_VERSION_ID.Value));
            }
            else
            {
                SetPolicyInSession(int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidCUSTOMER_ID.Value));
                hidDisplayBody.Value = "False";
                //ClsMessages.SetCustomizedXml(GetLanguageCode());
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1217");//"No Process in Progress on this Policy.";
                lblMessage.Visible = true;
            }

        }

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
            this.btnRollback.Click += new System.EventHandler(this.btnRollback_Click);
            this.btnBack_To_Search.Click += new System.EventHandler(this.btnBack_To_Search_Click);
            this.btnBack_To_Customer_Assistant.Click += new System.EventHandler(this.btnBack_To_Customer_Assistant_Click);
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
            this.btnGet_Premium.Click += new System.EventHandler(this.btnGet_Premium_Click);
            this.btnEffectivePremium.Click += new System.EventHandler(this.btnEffectivePremium_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnComitAynway.Click += new System.EventHandler(this.btnComitAynway_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        private void btnSave_Click(object sender, System.EventArgs e)
        {
             DateTimeFormatInfo DateFormatinfoBR = new CultureInfo(enumCulture.BR, true).DateTimeFormat;// changed by praveer for TFS#996
             DateTimeFormatInfo DateFormatinfoUS = new CultureInfo(enumCulture.US, true).DateTimeFormat;// changed by praveer for TFS#996
            //ClsMessages.SetCustomizedXml(GetLanguageCode());

            //DateTime EffectiveDate;
            //DateTime ExpryDate;
            //if (hidEFFECTIVE_DATETIME.Value != "")
            //    EffectiveDate = ConvertToDate(txtEFFECTIVE_DATETIME.Text);
            //if (hidEXPIRY_DATE.Value != "")
            //    ExpryDate = ConvertToDate(hidEXPIRY_DATE.Value);

            ClsProcessInfo objOldProcessInfo = new ClsProcessInfo();
            //int New_Policy_VersionId = 0;
            //int RowId = 0;
            //int PolicyVrId = 0; ;
            try
            {
                Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo = new Cms.Model.Policy.Process.ClsProcessInfo();


                objProcessInfo = GetFormValues();
                //if (!chkEndorsmentDiff(objProcessInfo))
                //{
                //    return;
                //}

                objProcessInfo.PROCESS_STATUS = ClsPolicyProcess.PROCESS_STATUS_PENDING;
                objProcessInfo.ROW_ID = int.Parse(hidROW_ID.Value);
                objProcessInfo.NEW_POLICY_VERSION_ID = int.Parse(hidNEW_POLICY_VERSION_ID.Value);


                //Setting the new policy in session
                SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);


                //if (cmbENDORSEMENT_OPTION.SelectedValue == EFFECTIVE_PERIOD_END) //if Endorsement option is effective period

                if (!chkENDORSEMENT_RE_ISSUE.Checked)
                    if (objProcess.OutOfSequenceEndorsement(objProcessInfo))
                    {   //Out of sequence endorsement
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("610");
                        lblMessage.Visible = true;
                        Saved = false;
                        return;
                    }

                //Making model object which will contains old data
                base.PopulateModelObject(objOldProcessInfo, hidOldData.Value);
                if (GetLanguageID() == "2")// changed by praveer for TFS#996
                {
                    string Eff_date = ClsCommon.FetchValueFromXML("EFFECTIVE_DATETIME", hidOldData.Value);
                    string Exp_date = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("EXPIRY_DATE", hidOldData.Value);
                    if (Eff_date != "" && Exp_date != "")
                    {
                        DateTime Effective_date = Convert.ToDateTime(Convert.ToDateTime(Eff_date, DateFormatinfoUS), DateFormatinfoBR);
                        DateTime Expiry_date = Convert.ToDateTime(Convert.ToDateTime(Exp_date, DateFormatinfoUS), DateFormatinfoBR);
                        objOldProcessInfo.EFFECTIVE_DATETIME = Effective_date;
                        objOldProcessInfo.EXPIRY_DATE = Expiry_date;
                    }
                }
                if (objOldProcessInfo.ENDORSEMENT_TYPE == 0)// changed by praveer for TFS# 750/996
                {
                    objOldProcessInfo.ENDORSEMENT_TYPE = GetEbixIntDefaultValue(); 
                }
                if (objOldProcessInfo.CO_APPLICANT_ID == 0)// changed by praveer for TFS# 750/996
                {
                    objOldProcessInfo.CO_APPLICANT_ID = GetEbixIntDefaultValue();
                }
              

                //Updating the previous endorsement record
                objProcess.BeginTransaction();
                //if (!chkENDORSEMENT_RE_ISSUE.Checked || sender == null)
                //{
                objProcess.UpdateProcessInformation(objOldProcessInfo, objProcessInfo);
                //}
                #region Commented code for re-issue endorsement
                //else
                //{
                //    string Info = hidSOURCE_VERSION_ID.Value; //cmbSOURCE_VERSION_ID.SelectedValue;
                //    //&& arInfo[1] != null && arInfo[1].ToString() != ""
                //    //objProcessInfo.ENDORSEMENT_NO = int.Parse(arInfo[1].ToString());
                //    //string[] arInfo = Info.Split('^');
                //    if (Info != "")
                //    {
                //        objProcessInfo.POLICY_VERSION_ID = int.Parse(Info);
                //        if (hidEND_EFFECTIVE_DATE.Value != "")
                //            objProcessInfo.EFFECTIVE_DATETIME = ConvertToDate(hidEND_EFFECTIVE_DATE.Value);
                //        if (hidEND_EXPIRY_DATE.Value != "")
                //            objProcessInfo.EXPIRY_DATE = ConvertToDate(hidEND_EXPIRY_DATE.Value);

                //    }
                //    if (objProcess.StartOutOfSequenceEndorsement(objProcessInfo, objOldProcessInfo, out New_Policy_VersionId, out RowId, out PolicyVrId, "REISSUE"))
                //    {
                //        hidPOLICY_VERSION_ID.Value = PolicyVrId.ToString();
                //        hidROW_ID.Value = RowId.ToString();
                //        hidNEW_POLICY_VERSION_ID.Value = New_Policy_VersionId.ToString();
                //        // hidPOLICY_ID.Value = PolicyVrId.ToString();
                //        //btnSave.Attributes.Add("style", "display:none");
                //    }
                //    else
                //    {
                //        throw new Exception("Unable to start this process");

                //    }
                //}

                //Commented by Lalit remove endorsement option functionality
                //#region Endorsment option impliemntation
                //if (cmbENDORSEMENT_OPTION.SelectedValue == CARRY_FORWARD_END || cmbENDORSEMENT_OPTION.SelectedValue == OVERRIDE_END)
                //{
                //    //if endorsement is carry forword                    
                //    //if (cmbENDORSEMENT_OPTION.SelectedValue == CARRY_FORWARD_END || cmbENDORSEMENT_OPTION.SelectedValue == OVERRIDE_END)
                //    //{
                //    // bool start = false;
                //    if (objProcess.NoSequenceEndorsement(objProcessInfo, objOldProcessInfo, out New_Policy_VersionId, out RowId, out PolicyVrId))
                //    {
                //        //set values from page
                //        hidPOLICY_VERSION_ID.Value = PolicyVrId.ToString();
                //        hidROW_ID.Value = RowId.ToString();
                //        hidNEW_POLICY_VERSION_ID.Value = New_Policy_VersionId.ToString();
                //        // hidPOLICY_ID.Value = PolicyVrId.ToString();
                //        btnSave.Attributes.Add("style", "display:none");
                //    }
                //    else
                //    {
                //        throw new Exception("Unable to start this process");

                //    }
                //    //}

                //}

                //#endregion

                //Printing Jobs -if internal change then not required at all
                //objProcess.PrintJobsForEndorsement(objProcessInfo);	printing at commit only /by Pravesh			
                #endregion
                objProcess.CommitTransaction();

                Saved = true;	//Saved succesfully
                //ClsMessages.SetCustomizedXml(GetLanguageCode());

                lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                lblMessage.Visible = true;
                hidFormSaved.Value = "1";
                GetOldDataXml();
                if (chkENDORSEMENT_RE_ISSUE.Checked)
                    chkENDORSEMENT_RE_ISSUE.Enabled = false;
                //Refresh the Policy Top.
                cltPolicyTop.CallPageLoad();
                verifyRule();

            }
            catch (Exception objExp)
            {
                objProcess.RollbackTransaction();
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
                lblMessage.Text = objExp.Message.ToString();
                lblMessage.Visible = true;
            }
        }

        private bool verifyRule()
        {
            return verifyRule("");
        }

        private bool verifyRule(string CalledFrom)
        {
            try
            {
                string strRulesStatus = "0";
                bool valid = false;

                //string strRulesHTML = Cms.BusinessLayer.BlProcess.clsprocess.strHTML(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value),out valid,out strRulesStatus);
                if (hidNEW_POLICY_VERSION_ID.Value != "" && hidNEW_POLICY_VERSION_ID.Value != "0")
                {

                    Cms.BusinessLayer.BlProcess.clsprocess objProcess = new clsprocess();

                    objProcess.SystemID = CarrierSystemID;

                    string strRulesHTML = objProcess.strHTML(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidNEW_POLICY_VERSION_ID.Value), out valid, out strRulesStatus, "END");

                    if (valid && strRulesStatus == "0") // then commit
                    {
                        valid = true;
                    }
                    else
                    {
                        // show rules msg		


                        // chk here for referred/rejected cases
                        ChkReferedRejCaese(strRulesHTML, CalledFrom);
                        //this.mySPAN.InnerHtml=strRulesHTML;
                        //Added By Lalit March 28,2011.
                        //if user not equivalent to supervisor btncommit should not be available
                        if (getIsUserSuperVisor() != "Y")
                        {
                            btnComitAynway.Visible = false;
                        }
                        myDIV.InnerHtml = strRulesHTML;
                        myDIV.Visible = true;
                        spnURStatus.Visible = true;
                        valid = false;
                    }
                }
                return valid;
            }
            catch (Exception objExp)
            {
                throw (objExp);
                //return false;
            }
        }

        private void btnCommit_Click(object sender, System.EventArgs e)
        {


            // complete the follow up that was generated by the system at the beginning of the process
            //string strRulesStatus="0";
            //Saving the information first - record any changes that changed by the user in the process log 
            btnSave_Click(null, null);

            bool valid = false;
            valid = this.verifyRule("COMMIT");



            //if (Saved == false)
            //	return;	//Unable to saved hence can not be committed
            //Saved successfully, hence committing	
            if (valid)
            {
                try
                {

                    //ClsEndorsmentProcess objProcess = new ClsEndorsmentProcess();
                    ///ClsProcessInfo objProcessInfo = objProcess.GetRunningProcess (int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value));
                    ClsProcessInfo objProcessInfo = GetFormValues();
                    //if (!chkEndorsmentDiff(objProcessInfo))
                    //{
                    //    return;
                    //}
                    objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS;
                    objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
                    objProcessInfo.COMPLETED_DATETIME = DateTime.Now;
                    objProcessInfo.LOB_ID = int.Parse(hidLOB_ID.Value);
                    #region Re-Issue Endorsement



                    #endregion


                    //Insert  Endorsment Log
                    if (objProcess.IsEndorsementLogAvailable(objProcessInfo) != true)
                    {
                        this.InsertEndorsementTranLog();
                    }

                    //Check EndorsementLog Details before Commiting Process.
                    if (objProcess.IsEndorsementLogAvailable(objProcessInfo) == true)
                    {
                        //Commiting the process 
                        // if (objProcessInfo.ENDORSEMENT_OPTION.ToString() != OVERRIDE_END || objProcessInfo.ENDORSEMENT_OPTION.ToString() != CARRY_FORWARD_END)
                        if (objProcessInfo.ENDORSEMENT_RE_ISSUE != (int)enumYESNO_LOOKUP_UNIQUE_ID.YES)
                            if (objProcess.OutOfSequenceEndorsement(objProcessInfo))
                            {
                               // ClsMessages.SetCustomizedXml(GetLanguageCode());
                                //Out of sequence endorsement
                                lblMessage.Text = ClsMessages.FetchGeneralMessage("610");
                                lblMessage.Visible = true;
                                return;
                            }
                        if (objProcess.CommitProcess(objProcessInfo) == true)
                        {
                            //ClsMessages.SetCustomizedXml(GetLanguageCode());

                            hidPROCESS_ID.Value = objProcessInfo.PROCESS_ID.ToString();
                            //Committed successfully
                            lblMessage.Text = ClsMessages.FetchGeneralMessage("689");
                            hidDisplayBody.Value = "True";
                            //Hiding the buttons
                            HideButtons();
                            GetOldDataXml();
                            //Updating the policy top,session and menus
                            //SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
                        }
                        else
                        {
                            //Error occured
                            if (ClsPolicyErrMsg.strMessage != "")
                                lblMessage.Text = ClsPolicyErrMsg.strMessage;
                            //else
                            //    ClsMessages.SetCustomizedXml(GetLanguageCode());

                                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1239");//"Unable to commit the process, please try later";
                            btnCommit.Attributes.Add("style", "display:inline");
                            btnCommitInProgress.Attributes.Add("style", "display:none");
                            btnSave.Attributes.Add("style", "disabled:false");

                            //SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
                        }
                        //Updating the policy top,session and menus
                        cltPolicyTop.CallPageLoad();
                        SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);

                        lblMessage.Visible = true;
                    }
                    else
                    {
                        //ClsMessages.SetCustomizedXml(GetLanguageCode());

                        //Message No Endorsement Available Can Not Commit Process
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("1023");
                        lblMessage.Visible = true;

                    }
                }
                catch (Exception objExp)
                {
                    //ClsMessages.SetCustomizedXml(GetLanguageCode());
                    cltPolicyTop.UseRequestVariables = true;
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1225")//"Following error occurred while committing process. \n" 
                        + objExp.Message + "\n Please try later.";
                    lblMessage.Visible = true;
                    Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
                    btnCommit.Attributes.Add("style", "display:inline");
                    btnCommitInProgress.Attributes.Add("style", "display:none");
                    btnSave.Attributes.Add("style", "disabled:false");
                }
            }
            try
            {
                ClsCommon objClsCommon = new ClsCommon();
                DataSet DsEligibleProcess = objClsCommon.GetEligibleProcess(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidNEW_POLICY_VERSION_ID.Value == "" ? hidPOLICY_ID.Value : hidNEW_POLICY_VERSION_ID.Value));
                if (DsEligibleProcess != null && DsEligibleProcess.Tables.Count > 0 && DsEligibleProcess.Tables[0].Rows.Count > 0)
                {

                }
                else
                {
                    EligibleForProcess = false;
                }
            }
            catch (Exception objExp) { Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp); }

        }

        private void btnRollback_Click(object sender, System.EventArgs e)
        {
            try
            {
                ClsEndorsmentProcess objProcess = new ClsEndorsmentProcess();

                ClsProcessInfo objProcessInfo = objProcess.GetRunningProcess(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value));

                objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ROLLBACK_ENDORSEMENT_PROCESS;
                objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
                objProcessInfo.COMPLETED_DATETIME = DateTime.Now;
                hidPROCESS_ID.Value = objProcessInfo.PROCESS_ID.ToString();
                //Rollbacking the process 
                if (objProcess.RollbackProcess(objProcessInfo) == true)
                {
                    //ClsMessages.SetCustomizedXml(GetLanguageCode());
                    //Rolled back successfully
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("696");
                    hidDisplayBody.Value = "False";
                    //Hiding the buttons
                    HideButtons();

                    //Updating the policy top,session and menus
                    SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
                }
                else
                {
                    //Error occured
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1242");//"Unable to rollback the process, please try later";
                    hidDisplayBody.Value = "True";
                }
                lblMessage.Visible = true;
            }
            catch (Exception objExp)
            {
                cltPolicyTop.UseRequestVariables = true;
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1231")//"Following error occured while rollbacking process. \n" 
                    + objExp.Message + "\n Please try later.";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
            }

        }

        private void btnBack_To_Search_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("/cms/client/aspx/CustomerManagerSearch.aspx");
        }

        private void btnBack_To_Customer_Assistant_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("/cms/client/aspx/CustomerManagerIndex.aspx?Customer_ID=" + hidCUSTOMER_ID.Value);
        }
        /// <summary>
        /// Get premium
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGet_Premium_Click(object sender, System.EventArgs e)
        {
            try
            {
                cltPolicyTop.UseRequestVariables = false;
                //Changing the client top also
                cltPolicyTop.PolicyID = int.Parse(hidPOLICY_ID.Value);

                if (hidNEW_POLICY_VERSION_ID.Value.Trim() == "")
                    cltPolicyTop.PolicyVersionID = int.Parse(hidPOLICY_VERSION_ID.Value);
                else
                    cltPolicyTop.PolicyVersionID = int.Parse(hidNEW_POLICY_VERSION_ID.Value);

                cltPolicyTop.CustomerID = int.Parse(hidCUSTOMER_ID.Value);
                cltPolicyTop.RefreshPolicy();

                base.GeneratePolicyQuote(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidNEW_POLICY_VERSION_ID.Value));
            }
            catch (Exception objExp)
            {
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1224")//"Following error occured. \n" 
                    + objExp.Message + "\n Please try later.";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
            }
        }

        private void btnEffectivePremium_Click(object sender, System.EventArgs e)
        {
            try
            {
                cltPolicyTop.UseRequestVariables = false;
                //Changing the client top also
                cltPolicyTop.PolicyID = int.Parse(hidPOLICY_ID.Value);

                if (hidNEW_POLICY_VERSION_ID.Value.Trim() == "")
                    cltPolicyTop.PolicyVersionID = int.Parse(hidPOLICY_VERSION_ID.Value);
                else
                    cltPolicyTop.PolicyVersionID = int.Parse(hidNEW_POLICY_VERSION_ID.Value);

                cltPolicyTop.CustomerID = int.Parse(hidCUSTOMER_ID.Value);
                cltPolicyTop.RefreshPolicy();

                base.GeneratePolicyQuote(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidNEW_POLICY_VERSION_ID.Value), true);
            }
            catch (Exception objExp)
            {
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1224")//"Following error occured. \n" 
                    + objExp.Message + "\n Please try later.";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
            }
        }

        /// chk for application referred vs rejected cases
        private void ChkReferedRejCaese(string strRulesHTML)
        {
            ChkReferedRejCaese(strRulesHTML, "");
        }
        private void ChkReferedRejCaese(string strRulesHTML, string strCalledFrom)
        {
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
                if ((objXmlNodeList != null && objXmlNodeList.Count > 0) || (objXmlNodeListRej != null && objXmlNodeListRej.Count > 0))
                {

                    if (objXmlNodeListRej.Item(0).InnerText == "0")
                    {
                       // ClsMessages.SetCustomizedXml(GetLanguageCode());

                        btnComitAynway.Visible = false;
                        if (strCalledFrom == "COMMIT")
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1218");//"Unable to commit process. Because Policy has been rejected as shown below.";
                        else
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1219");//"Policy has been rejected as shown below." ;
                        lblMessage.Visible = true;
                    }
                    else if (objXmlNodeList.Item(0).InnerText == "0")
                    {

                        btnComitAynway.Visible = true;
                        //btnComitAynway.Visible=false;
                        if (strCalledFrom == "COMMIT")
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1220");//"Unable to commit process. Because Policy has been referred as shown below." ;
                        else
                            lblMessage.Text = lblMessage.Text + "<br>" + Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1223"); // Policy has been referred as shown below.";
                        lblMessage.Visible = true;
                    }
                }
                else
                {
                    if (strCalledFrom == "COMMIT")
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1221");//"Unable to commit process. Please fill the mandatory information as shown below.";
                    else
                        lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1222");//"Please fill the mandatory information as shown below." ;
                    lblMessage.Visible = true;
                    btnComitAynway.Visible = false;
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1224")//"Following error occured. \n" 
                    + ex.Message + "\n Please try later.";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }

        private void btnComitAynway_Click(object sender, System.EventArgs e)
        {
            try
            {
                //ClsMessages.SetCustomizedXml(GetLanguageCode());
                ClsEndorsmentProcess objProcess = new ClsEndorsmentProcess();
                //ClsProcessInfo objProcessInfo = objProcess.GetRunningProcess (int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value));
                ClsProcessInfo objProcessInfo = GetFormValues();
                objProcessInfo.LOB_ID = int.Parse(hidLOB_ID.Value);
                objProcessInfo.PROCESS_ID = Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_COMMIT_ENDORSEMENT_PROCESS;
                objProcessInfo.COMPLETED_BY = int.Parse(GetUserId());
                objProcessInfo.COMPLETED_DATETIME = DateTime.Now;

                if (objProcess.IsEndorsementLogAvailable(objProcessInfo) != true)
                {
                    this.InsertEndorsementTranLog();
                }

                if (objProcess.IsEndorsementLogAvailable(objProcessInfo) == true)
                {
                    //Commiting the process 
                    if (objProcess.OutOfSequenceEndorsement(objProcessInfo))
                    {
                        //Out of sequence endorsement
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("610");
                        lblMessage.Visible = true;
                        return;
                    }
                    //Commiting the process 
                    if (objProcess.CommitProcess(objProcessInfo, Cms.CmsWeb.cmsbase.COMMIT_ANYWAYS_RULES) == true)
                    {
                        //Committed successfully
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("689");
                        hidPROCESS_ID.Value = objProcessInfo.PROCESS_ID.ToString();
                        //Hiding the buttons
                        HideButtons();
                        GetOldDataXml();//Done for Itrack Issue 5537 on 16 April 2009ss
                        //Updating the policy top,session and menus
                        //SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
                    }
                    else
                    {
                        //Error occured
                        if (ClsPolicyErrMsg.strMessage != "")
                            lblMessage.Text = ClsPolicyErrMsg.strMessage;
                        else
                            lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1239");//"Unable to commit the process, please try later";

                        btnComitAynway.Attributes.Add("style", "display:inline");
                        btnCommitAnywayInProgress.Attributes.Add("style", "display:none");
                        btnSave.Attributes.Add("style", "disabled:false");
                    }
                    lblMessage.Visible = true;
                    SetPolicyInSession(objProcessInfo.POLICY_ID, objProcessInfo.NEW_POLICY_VERSION_ID, objProcessInfo.CUSTOMER_ID);
                }
                else
                {
                    //Message No Endorsement Available Can Not Commit Process
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("1023");
                    lblMessage.Visible = true;

                }

            }
            catch (Exception objExp)
            {
                cltPolicyTop.UseRequestVariables = true;
                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1225")//"Following error occurred while committing process. \n" 
                    + objExp.Message + "\n Please try later.";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
                btnComitAynway.Attributes.Add("style", "display:inline");
                btnCommitAnywayInProgress.Attributes.Add("style", "display:none");
                btnSave.Attributes.Add("style", "disabled:false");

            }
        }
        private int InsertEndorsementTranLog()
        {
            int retval = 0;
            ClsEndorsementDetailInfo objEndorsementDetailInfo = new ClsEndorsementDetailInfo();

            objEndorsementDetailInfo.POLICY_ID = int.Parse(hidPOLICY_ID.Value);
            objEndorsementDetailInfo.POLICY_VERSION_ID = int.Parse(hidNEW_POLICY_VERSION_ID.Value);
            objEndorsementDetailInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
            objEndorsementDetailInfo.ENDORSEMENT_NO = int.Parse(hidENDORSEMENT_NO.Value);
            if (hidEFFECTIVE_DATETIME.Value != "")
                objEndorsementDetailInfo.ENDORSEMENT_DATE = Convert.ToDateTime(hidEFFECTIVE_DATETIME.Value);

            if (cmbENDORSEMENT_TYPE.SelectedValue != "")
                objEndorsementDetailInfo.ENDORSEMENT_TYPE = int.Parse(cmbENDORSEMENT_TYPE.SelectedValue);

            objEndorsementDetailInfo.ENDORSEMENT_DESC = "";
            objEndorsementDetailInfo.REMARKS = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1241");// "Endorsement  Complete";
            objEndorsementDetailInfo.CREATED_BY = int.Parse(GetUserId());
            objEndorsementDetailInfo.CREATED_DATETIME = DateTime.Now;
            if (Session["EndorsementTranIds"] != null && Session["EndorsementTranIds"].ToString() != "")
                objEndorsementDetailInfo.TRANS_ID = Session["EndorsementTranIds"].ToString();
            retval = objProcess.AddEndorsementLogDetails(objEndorsementDetailInfo);
            return retval;
        }
        private int UpdateEndorsementTranLog()
        {
            int retval = 0;
            ClsEndorsementDetailInfo objEndorsementDetailInfo = new ClsEndorsementDetailInfo();
            objEndorsementDetailInfo.POLICY_ID = int.Parse(hidPOLICY_ID.Value);
            objEndorsementDetailInfo.POLICY_VERSION_ID = int.Parse(hidNEW_POLICY_VERSION_ID.Value);
            objEndorsementDetailInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
            objEndorsementDetailInfo.ENDORSEMENT_NO = int.Parse(hidENDORSEMENT_NO.Value);
            objEndorsementDetailInfo.ENDORSEMENT_DATE = Convert.ToDateTime(hidEFFECTIVE_DATETIME.Value);

            objEndorsementDetailInfo.ENDORSEMENT_TYPE = int.Parse(cmbENDORSEMENT_TYPE.SelectedValue);
            objEndorsementDetailInfo.ENDORSEMENT_DESC = "";
            objEndorsementDetailInfo.ENDORSEMENT_STATUS = "Completed";
            objEndorsementDetailInfo.MODIFIED_BY = int.Parse(GetUserId());
            objEndorsementDetailInfo.LAST_UPDATED_DATETIME = DateTime.Now;
            objEndorsementDetailInfo.TRANS_ID = "0";
            retval = objProcess.UpdateEndorsementLog(objEndorsementDetailInfo);

            return retval;
        }
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public string chkEndorsmentDiff(string Customer_id, string policy_id, string policy_version_id, string Co_applicant_id, string Effective_date, string ExpiryDate)
        {

            //ClsProcessInfo objProcessInfo
            DateTime dEffective_date = Convert.ToDateTime(Effective_date); //objProcessInfo.EFFECTIVE_DATETIME;
            DateTime dExpiryDate = Convert.ToDateTime(ExpiryDate); //objProcessInfo.EXPIRY_DATE;

            //bool validendorsement = true;

            if (GetTransaction_Type() != MASTER_POLICY) //if not master policy not required that
            {
                return "";//validendorsement;
            }

            DataSet ds = new DataSet();
            ds = ClsEndorsmentProcess.ChkEndorsementDate(int.Parse(Customer_id), int.Parse(policy_id), int.Parse(policy_version_id), int.Parse(Co_applicant_id), dEffective_date, dExpiryDate);


            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["DATE_DIFF_CURRENT"].ToString() == "" ? "0" : ds.Tables[0].Rows[0]["DATE_DIFF_CURRENT"].ToString()) > 1)
                {
                    //lblMessage.Text = ClsMessages.FetchGeneralMessage("1944");
                    // ClientScript.RegisterClientScriptBlock(GetType(), "alert", "<script lang='javascript'>confirm('" + lblMessage.Text + "')</script>");

                    return "1";
                }
            if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                if (Convert.ToInt32(ds.Tables[1].Rows[0]["DATE_DIFF_FROM_PREVIOUS_END"].ToString() == "" ? "0" : ds.Tables[1].Rows[0]["DATE_DIFF_FROM_PREVIOUS_END"].ToString()) > 1)
                {
                    //lblMessage.Text = ClsMessages.FetchGeneralMessage("1945");

                    //ClientScript.RegisterClientScriptBlock(GetType(), "alert", "<script lang='javascript'>confirm('" + lblMessage.Text + "')</script>");
                    return "2";
                }

            return "";
        }
    }
}
