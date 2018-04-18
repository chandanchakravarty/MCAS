/******************************************************************************************
<Author					: -   Pravesh K Chandel
<Start Date				: -	  29 March 2010  
<End Date				: -	
<Description			: -  Policy Coverages Information screen
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			:  
<Modified By			:   
<Purpose				:  
*******************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;

using System.Data;
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.Model.Policy;
using System.Text;

using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.Xml;
using System.Resources;
using System.Reflection;
using System.Globalization;




namespace Cms.Policies.Aspx
{
    public partial class AddPolicyCoverages : Cms.Policies.policiesbase
    {
        #region Page Controls Declaration

        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidRISK_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolVersionID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
        protected System.Web.UI.WebControls.Label lblTitle;
        protected System.Web.UI.WebControls.Label lblMsg;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCoverageXML;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidROW_COUNT;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIsunder25;
        protected System.Web.UI.WebControls.Label lblPolicyCaption;
        protected System.Web.UI.WebControls.DataGrid dgPolicyCoverages;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ROW_COUNT;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOBState;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomInfo;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidControlXML;
        protected System.Web.UI.HtmlControls.HtmlTableRow trPOLICY_LEVEL_GRID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCO_APPLICANT_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAlertMsg;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPROCESS_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidExcludeMsg;
        protected System.Web.UI.WebControls.Label lblTOTAL_COVERAGE_PREMIUM;//Added for itrack-943
        protected System.Web.UI.HtmlControls.HtmlTableRow trTOTAL_COVERAGE_PREMIUM;//Added for itrack-943
        
        #endregion

        #region Local Variables Declaration
        public string calledFrom = "";
        DataSet dsCoverages = null;
        private System.DateTime AppEffectiveDate;
        StringBuilder sbCtrlXML = new StringBuilder();
        StringBuilder sbScript = new StringBuilder();
        StringBuilder sbDisableScript = new StringBuilder();
        XmlDocument doc = new XmlDocument();
        int rowCount = 0;
        ResourceManager objResourceMgr = null;
        public NumberFormatInfo nfi;
        ClsCommon objCommon;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            #region setting screen id
            if (Request.QueryString["CALLEDFROM"] != null && Request.QueryString["CALLEDFROM"].ToString().Trim() != "")
            {
                calledFrom = Request.QueryString["CALLEDFROM"].ToString().Trim();

            }
            switch (calledFrom.ToUpper())
            {
                case "PPA":
                    base.ScreenId = "227_1";
                    break;
                case "UMB":
                    base.ScreenId = "81_1";
                    break;
                case "MOT":
                    base.ScreenId = "231_1";
                    break;
                case "WAT":
                    if (GetLOBString() == "UMB")
                    {
                        base.ScreenId = "83_2";
                    }
                    else
                    {
                        base.ScreenId = "246_2";
                    }
                    break;
                case "COMPCONDO":
                    base.ScreenId = COMPREHENSIVE_CONDOMINIUMscreenId.COVERAGES;
                    break;

                case "RISK":
                    base.ScreenId = DIVERSIFIED_RISKSscreenId.COVERAGES;
                    break;
                case "COMPCOMP":
                    base.ScreenId = COMPREHENSIVE_COMPANYscreenId.COVERAGES;
                    break;
                case "NCTRANS":
                    base.ScreenId = NATIONAL_CARGO_TRANSPORTATIONscreenId.COVERAGES;
                    break;
                case "INTERNTRANS":
                    base.ScreenId = INTERNATIONAL_CARGO_TRANSPORTATIONscreenId.COVERAGES;
                    break;
                case "INDPA":
                    base.ScreenId = INDIVIDUAL_PERSONAL_ACCIDENTscreenId.COVERAGES;//"116_1_3";
                    break;
                case "CPCACC":
                    base.ScreenId = GROUP_PERSONAL_ACCIDENTscreenId.COVERAGES;
                    break;
                case "DWELLING":
                    base.ScreenId = DWELLINGscreenId.COVERAGES;
                    break;
                case "ROBBERY":
                    base.ScreenId = ROBBERYscreenId.COVERAGES;
                    break;
                case "GENCVLLIB":
                    base.ScreenId = GENERAL_CIVIL_LIABILITYscreenId.COVERAGES;
                    break;
                default:
                    base.ScreenId = "44_2";
                    break;
            }
            #endregion
            objResourceMgr = new ResourceManager("Cms.Policies.Aspx.AddPolicyCoverages", Assembly.GetExecutingAssembly());
            //ClsMessages.SetCustomizedXml(GetLanguageCode());
            if (GetPolicyCurrency() != String.Empty && GetPolicyCurrency() == enumCurrencyId.BR)
                nfi = new CultureInfo(enumCulture.BR, true).NumberFormat;
            else
                nfi = new CultureInfo(enumCulture.US, true).NumberFormat;

            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;    
        
            #region ROBBERY Coverage XML Rule
            if (calledFrom.ToUpper() == "ROBBERY")
            {
                if (Cache["ProductRuleXml"] == null)
                {
                    string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/cmsweb/support/Coverages/ProductCoverageRule.xml");
                    doc.Load(filePath);

                    System.Web.Caching.CacheDependency dep = new System.Web.Caching.CacheDependency(filePath);
                    Cache.Insert("RuleXmlAuto", doc, dep);
                }
                else
                {
                    doc = (XmlDocument)Cache["ProductRuleXml"];
                }
            }
            #endregion

            if (!Page.IsPostBack)
            {
                
                hidCustomerID.Value = GetCustomerID();
                hidPolID.Value = GetPolicyID();
                hidPolVersionID.Value = GetPolicyVersionID();
                if (Request.QueryString["risk_id"] != null && Request.QueryString["risk_id"].ToString() != "")
                    this.hidRISK_ID.Value = Request.QueryString["risk_id"].ToString();
                if (Request.QueryString["CO_APPLICANT_ID"] != null && Request.QueryString["CO_APPLICANT_ID"].ToString() != "")
                    hidCO_APPLICANT_ID.Value = Request.QueryString["CO_APPLICANT_ID"];
                SetCaptions();
                BindGrid(calledFrom);
                this.EnableGridCoumnOnEndorement();
            } 
            SetWorkFlowControl();
            SetCustomSecurityxml(hidCO_APPLICANT_ID.Value, calledFrom);
           
        }
        private void EnableGridCoumnOnEndorement()
        {
            objCommon = new ClsCommon();
            DataSet ds_process = objCommon.GetPolicy_ProcessDetails(Convert.ToInt32(hidCustomerID.Value),
                    Convert.ToInt32(hidPolID.Value),
                    Convert.ToInt32(hidPolVersionID.Value));
            if (ds_process != null && ds_process.Tables.Count > 0 && ds_process.Tables[0].Rows.Count > 0)
            {
                if (ds_process.Tables[0].Rows[0]["PROCESS_ID"].ToString() == "14" || ds_process.Tables[0].Rows[0]["PROCESS_ID"].ToString() == "3")
                {
                    
                    hidPROCESS_ID.Value = ds_process.Tables[0].Rows[0]["PROCESS_ID"].ToString();
                    dgPolicyCoverages.Columns[1].Visible = true; ;
                }
            }
        }
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            lblMessage.Visible = true;

            this.lblMessage.Attributes.Add("style", "display:inline");

            int retVal = Save();

            if (retVal == 1)
            {
                if (Convert.ToInt32(ViewState["TotalRecords"]) == 0)
                {
                    //ClsMessages.SetCustomizedXml(GetLanguageCode());
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "29");
                }
                else
                {
                    //ClsMessages.SetCustomizedXml(GetLanguageCode());
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "31");
                }



                BindGrid(calledFrom);
                base.OpenEndorsementDetails();
                SetWorkFlowControl();
                UpdateRisk();
                return;
            }

            if (retVal == -2)
            {
                //ClsMessages.SetCustomizedXml(GetLanguageCode());
                lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "332");
                //lblMessage.Text = "One of the Coverage code already exists. Please enter another code.";
                return;
            }
        }


        private void UpdateRisk()
        {
            ClsProductCoverages objproduct = new ClsProductCoverages();
            int lob = int.Parse(GetLOBID());
            int customer = int.Parse(GetCustomerID());
            int policy = int.Parse(GetPolicyID());
            int policyversion = int.Parse(GetPolicyVersionID());
            int ret = 0;
            this.hidRISK_ID.Value = Request.QueryString["risk_id"].ToString();
            try
            {
                ret = objproduct.UpdateRisk(lob, customer, policy, policyversion, hidRISK_ID.Value);
            }
            catch (Exception ex)
            {
                //ClsMessages.SetCustomizedXml(GetLanguageCode());
                lblMessage.Text = ClsMessages.FetchGeneralMessage("21") + ":-" + ex.Message;
            }

        }
        /// <summary>
        /// Saves the Coverages
        /// </summary>
        /// <returns></returns>
        private int Save()
        {
            ArrayList alRecr = new ArrayList();
            ArrayList alDelete = new ArrayList();
            string Policy_status = GetPolicyStatus();
            string Tran_Type = GetTransaction_Type();
            PopulateList(alRecr, this.dgPolicyCoverages);

            ClsProductCoverages objCoverages;

            objCoverages = new ClsProductCoverages();
            int retVal = 1;

            try
            {
                retVal = objCoverages.SavePolicyProductCoverages(alRecr, hidOldData.Value, hidCustomInfo.Value, Policy_status, Tran_Type);
            }
            catch (Exception ex)
            {
                lblMessage.Visible = true;

                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

                if (ex.InnerException != null)
                {
                    lblMessage.Text = ex.InnerException.Message;
                }
                return -4;
            }
            return 1;
        }
        private void PopulateList(ArrayList alRecr, DataGrid dgCoverages)
        {
            //Vehicle Level Coverages
            foreach (DataGridItem dgi in dgCoverages.Items)
            {
                if (dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem)
                {
                    //Get the checkbox
                    CheckBox cbDelete = (CheckBox)dgi.FindControl("cbDelete");
                    CheckBox cbRIDelete = (CheckBox)dgi.FindControl("cbRIDelete");
                    CheckBox chkIS_ACTIVE = (CheckBox)dgi.FindControl("chkIS_ACTIVE");
                 
                    HtmlSelect ddlLimit = ((HtmlSelect)dgi.FindControl("ddlLimit"));
                    HtmlSelect ddlDeductible = ((HtmlSelect)dgi.FindControl("ddlDeductible"));
                    HtmlSelect ddlDeductType = ((HtmlSelect)dgi.FindControl("ddlDEDUCTIBLETYPE"));

                    TextBox txtLimit = ((TextBox)dgi.FindControl("txtLimit"));
                    TextBox txtDeductible = ((TextBox)dgi.FindControl("txtDEDUCTIBLE"));
                    TextBox txtMiniDeductible = ((TextBox)dgi.FindControl("txtMIN_DEDUCTIBLE"));


                    CheckBox cbDedReduce = (CheckBox)dgi.FindControl("cbDedReduce");
                    TextBox txtInitialRate = ((TextBox)dgi.FindControl("txtINITIAL_RATE"));
                    TextBox txtFinalRate = ((TextBox)dgi.FindControl("txtFINAL_RATE"));
                    CheckBox cbAvarageRate = (CheckBox)dgi.FindControl("cbAvarageRate");
                    TextBox txtPremium = ((TextBox)dgi.FindControl("txtPREMIUM"));
                    //ankit #424
                    TextBox txtIndemnity_Period = ((TextBox)dgi.FindControl("txtIndemnity_Period"));

                    Label lblCOV_DESC = ((Label)dgi.FindControl("lblCOV_DESC"));
                    Label lblCOV_CODE = ((Label)dgi.FindControl("lblCOV_CODE"));
                    Label lblCOV_ID = ((Label)dgi.FindControl("lblCOV_ID"));
                    Label lblCOV_TYPE = ((Label)dgi.FindControl("lblCOV_TYPE"));

                    Label lblAdd_IS_DEDUCT_APPLICABLE = ((Label)dgi.FindControl("lblAdd_IS_DEDUCT_APPLICABLE"));
                    HtmlInputHidden hidDEDUCTIBLE_TEXT = ((HtmlInputHidden)dgi.FindControl("hidDEDUCTIBLE_TEXT"));


                    Cms.Model.Policy.ClsPolicyCoveragesInfo objInfo = new Cms.Model.Policy.ClsPolicyCoveragesInfo();

                    objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
                    objInfo.POLICY_VERSION_ID = Convert.ToInt32(this.hidPolVersionID.Value);
                    objInfo.POLICY_ID = Convert.ToInt32(this.hidPolID.Value);
                    objInfo.RISK_ID = Convert.ToInt32(this.hidRISK_ID.Value);
                    objInfo.COV_DESC = ClsCommon.EncodeXMLCharacters(lblCOV_DESC.Text.Trim());
                    objInfo.COVERAGE_CODE = lblCOV_CODE.Text.Trim();
                    objInfo.COVERAGE_CODE_ID = Convert.ToInt32(lblCOV_ID.Text.Trim());
                    //ankit
                    if (txtIndemnity_Period.Text.Trim() != "")
                        objInfo.IDEMNITY_PERIOD = Convert.ToInt32(txtIndemnity_Period.Text.Trim());

                    objInfo.COVERAGE_TYPE = lblCOV_TYPE.Text.Trim();

                    objInfo.CREATED_BY = int.Parse(GetUserId());
                    objInfo.MODIFIED_BY = int.Parse(GetUserId());

                    if (dgCoverages.DataKeys[dgi.ItemIndex] != System.DBNull.Value)
                    {
                        objInfo.COVERAGE_ID = Convert.ToInt32(dgCoverages.DataKeys[dgi.ItemIndex]);
                    }

                    int row = dgi.ItemIndex + 2;

                    HtmlInputHidden hidCbDelete = (System.Web.UI.HtmlControls.HtmlInputHidden)dgi.FindControl("hidcbDelete");

                    bool isChecked = false;

                    if (hidCbDelete.Value == "true")
                    {
                        isChecked = true;
                    }
                    else
                    {
                        isChecked = false;
                    }

                    if (isChecked)
                    {
                        if (chkIS_ACTIVE.Checked == true)
                        {
                            if (hidPROCESS_ID.Value.ToString() == "3" || hidPROCESS_ID.Value.ToString() == "14")
                              objInfo.IS_ACTIVE = "N"; 
                            else
                                objInfo.IS_ACTIVE = "Y";
                        }
                        else
                        {
                            objInfo.IS_ACTIVE = "Y";
                        }
                        objInfo.COVERAGE_CODE_ID = Convert.ToInt32(
                            ((Label)dgi.FindControl("lblCOV_ID")).Text
                            );

                        string strLimitType = ((Label)dgi.FindControl("lblLIMIT_TYPE")).Text;
                        string strDedType = ((Label)dgi.FindControl("lblDEDUCTIBLE_TYPE")).Text;

                        //objInfo.ADD_INFORMATION = txtNoPersons.Text.Trim();
                        if (cbRIDelete.Checked)
                            objInfo.RI_APPLIES = "Y";
                        else
                            objInfo.RI_APPLIES = "N";
                        #region getting Limit /Sum Insured
                        switch (strLimitType)
                        {
                            case "1":
                                //Flat
                                if (ddlLimit.Items.Count > 0 && ddlLimit.Items[ddlLimit.SelectedIndex].Text.Trim() != "")
                                {
                                    //"100 Excess Medical"
                                    string amount = ddlLimit.Items[ddlLimit.SelectedIndex].Text.Substring(0, ddlLimit.Items[ddlLimit.SelectedIndex].Text.IndexOf(" "));
                                    string text = ddlLimit.Items[ddlLimit.SelectedIndex].Text.Substring(ddlLimit.Items[ddlLimit.SelectedIndex].Text.IndexOf(" "));
                                    objInfo.LIMIT_ID = Convert.ToInt32(ddlLimit.Items[ddlLimit.SelectedIndex].Value);
                                    if (amount.Trim() == "Reject")
                                    {
                                        objInfo.LIMIT1_AMOUNT_TEXT = text.Trim();
                                        break;
                                    }

                                    //string[] strArr = ddlLimit.SelectedItem.Text.Split(' ',
                                    if (amount.Trim() != "")
                                    {
                                        objInfo.LIMIT_1 = Convert.ToDouble(amount);
                                    }

                                    objInfo.LIMIT1_AMOUNT_TEXT = text.Trim();
                                    //objInfo.LIMIT_1 = Convert.ToDouble(ddlLimit.SelectedItem.Text);
                                }

                                break;
                            case "2":
                                //Split
                                if (ddlLimit.Items.Count > 0 && ddlLimit.Items[ddlLimit.SelectedIndex].Value != "")
                                {
                                    string[] strValues = ddlLimit.Items[ddlLimit.SelectedIndex].Text.Split('/');

                                    objInfo.LIMIT_ID = Convert.ToInt32(ddlLimit.Items[ddlLimit.SelectedIndex].Value);

                                    if (strValues[0].Trim() == "Reject" && strValues[1].Trim() == "Reject")
                                    {
                                        objInfo.LIMIT1_AMOUNT_TEXT = strValues[0].Trim();
                                        objInfo.LIMIT2_AMOUNT_TEXT = strValues[0].Trim();
                                        break;
                                    }


                                    string[] strLimits1 = strValues[0].Split(' ');
                                    string[] strLimits2 = strValues[1].Split(' ');

                                    objInfo.LIMIT_1 = Convert.ToDouble(strLimits1[0]);
                                    objInfo.LIMIT1_AMOUNT_TEXT = strLimits1[1];

                                    objInfo.LIMIT_2 = Convert.ToDouble(strLimits2[0]);
                                    objInfo.LIMIT2_AMOUNT_TEXT = strLimits2[1];
                                }

                                break;
                            case "0":
                            case "3":
                                //Open
                                if (txtLimit.Text.Trim() != "")
                                {
                                    string amount = txtLimit.Text.Trim();

                                    if (amount.IndexOf("/") == -1)
                                    {
                                        if (txtLimit.Text.Trim() == "Reject")
                                        {
                                            objInfo.LIMIT1_AMOUNT_TEXT = Convert.ToDouble(txtLimit.Text.Trim(), nfi).ToString();
                                        }
                                        else
                                        {
                                            objInfo.LIMIT_1 = Convert.ToDouble(txtLimit.Text.Trim(), nfi);
                                        }
                                    }

                                    if (amount.IndexOf("/") != -1)
                                    {
                                        string[] strValues = amount.Split('/');

                                        if (strValues.Length == 2)
                                        {
                                            if (strValues[0].Trim() == "Reject" && strValues[1].Trim() == "Reject")
                                            {
                                                objInfo.LIMIT1_AMOUNT_TEXT = strValues[0].Trim();
                                                objInfo.LIMIT2_AMOUNT_TEXT = strValues[1].Trim();
                                                break;
                                            }

                                        }

                                        string[] strLimits1 = strValues[0].Split(' ');
                                        string[] strLimits2 = strValues[1].Split(' ');

                                        if (strLimits1[0] != null && strLimits1[0] != "")
                                        {
                                            objInfo.LIMIT_1 = Convert.ToDouble(strLimits1[0]);
                                        }

                                        if (strLimits1.Length > 1)
                                        {
                                            objInfo.LIMIT1_AMOUNT_TEXT = strLimits1[1];
                                        }

                                        if (strLimits2[0] != null && strLimits2[0] != "")
                                        {
                                            objInfo.LIMIT_2 = Convert.ToDouble(strLimits2[0]);
                                        }

                                        if (strLimits2.Length > 1)
                                        {
                                            objInfo.LIMIT2_AMOUNT_TEXT = strLimits2[1];
                                        }
                                    }

                                }
                                break;
                        }
                        #endregion
                        #region getting Deductible
                        switch (strDedType)
                        {
                            case "1":
                                //Flat
                                if (ddlDeductible.Items.Count > 0 && ddlDeductible.Items[ddlDeductible.SelectedIndex].Value != "")
                                {

                                    string[] strArr = ddlDeductible.Items[ddlDeductible.SelectedIndex].Text.Split(' ');

                                    objInfo.DEDUC_ID = Convert.ToInt32(ddlDeductible.Items[ddlDeductible.SelectedIndex].Value);

                                    if (ddlDeductible.Items[ddlDeductible.SelectedIndex].Text.Trim() == "Limited")
                                    {
                                        objInfo.DEDUCTIBLE1_AMOUNT_TEXT = ddlDeductible.Items[ddlDeductible.SelectedIndex].Text.Trim();
                                        break;
                                    }

                                    if (strArr.Length == 2)
                                    {
                                        objInfo.DEDUCTIBLE_1 = Convert.ToDouble(strArr[0]);
                                        objInfo.DEDUCTIBLE1_AMOUNT_TEXT = strArr[1];
                                    }

                                    //objInfo.DEDUCTIBLE_1 = Convert.ToDouble(ddlDeductible.SelectedItem.Text);
                                }
                                break;
                            case "2":
                                //Split
                                if (ddlDeductible.Items.Count > 0 && ddlDeductible.Items[ddlDeductible.SelectedIndex].Value != "")
                                {
                                    string[] strValues = ddlDeductible.Items[ddlDeductible.SelectedIndex].Text.Split('/');

                                    string[] strDed1 = strValues[0].Split(' ');
                                    string[] strDed2 = strValues[1].Split(' ');

                                    objInfo.DEDUCTIBLE_1 = Convert.ToDouble(strDed1[0]);
                                    objInfo.DEDUCTIBLE1_AMOUNT_TEXT = strDed1[0];

                                    objInfo.DEDUCTIBLE_2 = Convert.ToDouble(strDed2[0]);
                                    objInfo.DEDUCTIBLE2_AMOUNT_TEXT = strDed2[1];

                                    objInfo.DEDUC_ID = Convert.ToInt32(ddlDeductible.Items[ddlDeductible.SelectedIndex].Value);

                                }

                                break;
                            case "0":
                            case "3":
                                //Open
                                if (txtDeductible.Text.Trim() != "")
                                {
                                    objInfo.DEDUCTIBLE_1 = Convert.ToDouble(txtDeductible.Text.Trim(), nfi);
                                }
                                objInfo.DEDUCTIBLE2_AMOUNT_TEXT = hidDEDUCTIBLE_TEXT.Value;
                                break;
                        }
                        #endregion

                        objInfo.DEDUCTIBLE_1_TYPE = ddlDeductType.Items[ddlDeductType.SelectedIndex].Value;
                        if (txtMiniDeductible.Text.Trim() != "")
                            objInfo.MINIMUM_DEDUCTIBLE = Convert.ToDouble(txtMiniDeductible.Text.Trim(), nfi);

                        if (cbDedReduce.Checked)
                            objInfo.DEDUCTIBLE_REDUCES = "Y";
                        else
                            objInfo.DEDUCTIBLE_REDUCES = "N";
                        if (txtInitialRate.Text.Trim() != "")
                            objInfo.INITIAL_RATE = Convert.ToDouble(txtInitialRate.Text.Trim(), nfi);
                        if (txtFinalRate.Text.Trim() != "")
                            objInfo.FINAL_RATE = Convert.ToDouble(txtFinalRate.Text.Trim(), nfi);
                        if (cbAvarageRate.Checked)
                            objInfo.AVERAGE_RATE = "Y";
                        else
                            objInfo.AVERAGE_RATE = "N";
                        if (txtPremium.Text.Trim() != "")
                            objInfo.WRITTEN_PREMIUM = Convert.ToDouble(txtPremium.Text.Trim(), nfi);

                        //INSERT 
                        if (objInfo.COVERAGE_ID == -1)
                        {
                            objInfo.ACTION = "I";
                            objInfo.CREATED_BY = int.Parse(GetUserId());
                            objInfo.CREATED_DATETIME = ConvertToDate(DateTime.Now.ToShortDateString());
                        }
                        else
                        {
                            //UPDATE
                            objInfo.ACTION = "U";
                            objInfo.MODIFIED_BY = int.Parse(GetUserId());
                            objInfo.LAST_UPDATED_DATETIME = ConvertToDate(DateTime.Now.ToShortDateString());

                        }
                        alRecr.Add(objInfo);

                    }
                    else
                    {
                        //ClsCoveragesInfo objInfo = new ClsCoveragesInfo();	

                        if (dgCoverages.DataKeys[dgi.ItemIndex] != System.DBNull.Value)
                        {
                            if (objInfo.COVERAGE_ID != -1)
                            {
                                objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
                                objInfo.POLICY_VERSION_ID = Convert.ToInt32(this.hidPolVersionID.Value);
                                objInfo.POLICY_ID = Convert.ToInt32(this.hidPolID.Value);
                                objInfo.RISK_ID = Convert.ToInt32(this.hidRISK_ID.Value);
                                objInfo.COVERAGE_ID = Convert.ToInt32(dgCoverages.DataKeys[dgi.ItemIndex]);
                                Label lblCOV_DES = ((Label)dgi.FindControl("lblCOV_DESC"));
                                objInfo.COV_DESC = lblCOV_DES.Text.Trim();

                                //DELETE
                                objInfo.ACTION = "D";
                                alRecr.Add(objInfo);

                            }


                        }
                    }

                }

            }
        }
        /// <summary>
        /// Fired for each row of Policy level coverages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgPolicyCoverages_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            //StringBuilder sbScript = new StringBuilder();

            OnItemDataBound(sender, e);


        }
        #region BindGrid Function
        /// <summary>
        /// Binds the datagrid to the dataset
        /// </summary>
        /// <param name="calledFrom"></param>
        private void BindGrid(string calledFrom)
        {
            ClsProductCoverages objCoverages = new ClsProductCoverages();

            try
            {
                //Get the relevant coverages
                switch (calledFrom.ToUpper())
                {
                    case "PERIL":
                        dsCoverages = objCoverages.GetPolicyProductCoverages(Convert.ToInt32(hidCustomerID.Value),
                        Convert.ToInt32(hidPolID.Value),
                        Convert.ToInt32(hidPolVersionID.Value),
                        Convert.ToInt32(hidRISK_ID.Value),
                        "N",
                        int.Parse(GetLanguageID())
                        );
                        break;
                    case "RREC":
                    case "HREC":
                        Response.Redirect("../../cmsweb/Construction.html");
                        break;

                    default:
                        dsCoverages = objCoverages.GetPolicyProductCoverages(Convert.ToInt32(hidCustomerID.Value),
                        Convert.ToInt32(hidPolID.Value),
                        Convert.ToInt32(hidPolVersionID.Value),
                        Convert.ToInt32(hidRISK_ID.Value),
                        "N",
                        int.Parse(GetLanguageID())
                        );
                        break;

                }
                //Get the state details
                string lob = base.GetLOBString();
                //Custom info for tran log
                //LoadCustomInfo();

                AppEffectiveDate = (DateTime)dsCoverages.Tables[2].Rows[0]["APP_EFFECTIVE_DATE"];

                //Get Old data XML
                DataTable dataTable = dsCoverages.Tables[0];
                hidOldData.Value = ClsCommon.GetXMLEncoded(dataTable);
                DataView dvPolicyCoverages = new DataView(dsCoverages.Tables[0]);
                
                // for itrack 1184
                //string filter = "COVERAGE_TYPE = 'PL'";
                //dvPolicyCoverages.RowFilter = filter; ;
                if (GetLOBID().ToString() == "16")
                    dvPolicyCoverages.Sort = "RANK ASC";
                //Added by Pradeep Kushwaha on-25-Oct-2011 for itrack # 906 / TFS ID # 1698
                //else
                //    if (GetPolicyStatus() != "")
                //        dvPolicyCoverages.Sort = "COVERAGE_ID";//DESC ,ASC
                //Added till here 
   
                rowCount = dvPolicyCoverages.Count;

                //Root tag of control xml 
                this.sbCtrlXML.Length = 0;
                this.sbCtrlXML.Append("<Root>");
                this.dgPolicyCoverages.DataSource = dvPolicyCoverages;
             
                if (rowCount == 0)
                {
                    dgPolicyCoverages.Attributes.Add("style", "display:none");
                    trPOLICY_LEVEL_GRID.Attributes.Add("style", "display:none");
                    lblPolicyCaption.Attributes.Add("style", "display:none");
                    trTOTAL_COVERAGE_PREMIUM.Attributes.Add("style", "display:none");//Added for itrack 943
                }
                //////////End of Policy level

                //Bind Risk level coverages
                DataView dvRiskCoverages = new DataView(dsCoverages.Tables[0]);

                //coded by kuldeep saxena to meet the tree navigation requirement only for insuror
                string riskFilter = null;
                if (GetSystemId().ToUpper() == "I001" || GetSystemId().ToUpper() == "IUAT")
                {
                    riskFilter = "COVERAGE_TYPE = 'PL'";
                    dvRiskCoverages.RowFilter = riskFilter; 
                    this.dgPolicyCoverages.DataSource = dvRiskCoverages;
                }
                else
                {
                    riskFilter = "COVERAGE_TYPE = 'RL'";
                }



                dvRiskCoverages.RowFilter = riskFilter; 

                rowCount = dvRiskCoverages.Count;
                //this.dgPolicyCoverages.DataSource = dvRiskCoverages;
                dgPolicyCoverages.DataBind();
                //End tag of control XML
                this.sbCtrlXML.Append("</Root>");
                this.hidControlXML.Value = sbCtrlXML.ToString();
                hidPOLICY_ROW_COUNT.Value = dgPolicyCoverages.Items.Count.ToString();
                RegisterScript();
            }
            catch (Exception ex)
            {
                lblMessage.Visible = true;

                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

                if (ex.InnerException != null)
                {
                    lblMessage.Text = ex.InnerException.Message;
                }
            }
        }

        #endregion
        #region OnItemDataBound Function for Binding Grid
        private void OnItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            //ClsMessages.SetCustomizedXml(GetLanguageCode());
            // e.Item.ItemType
            //Adding Style to Altern ating Item
            e.Item.Attributes.Add("Class", "midcolora");

            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label lblCOV_ID = (Label)e.Item.FindControl("lblCOV_ID");

                Label lblLIMIT_AMOUNT = (Label)e.Item.FindControl("lblLIMIT_AMOUNT");
                CheckBox cbDelete = (CheckBox)e.Item.FindControl("cbDelete");
                CheckBox cbRIDelete = (CheckBox)e.Item.FindControl("cbRIDelete");
                CheckBox chkIS_ACTIVE = (CheckBox)e.Item.FindControl("chkIS_ACTIVE");
                HtmlInputHidden hidIS_ACTIVE = ((HtmlInputHidden)e.Item.FindControl("hidIS_ACTIVE"));
                
                HtmlSelect ddlLimit = (HtmlSelect)e.Item.FindControl("ddlLIMIT");
                CustomValidator csvddlDEDUCTIBLE = (CustomValidator)e.Item.FindControl("csvddlDEDUCTIBLE");
                HtmlSelect ddlDed = (HtmlSelect)e.Item.FindControl("ddlDEDUCTIBLE");
                HtmlSelect ddlDedType = (HtmlSelect)e.Item.FindControl("ddlDEDUCTIBLETYPE");
                Label lblCOV_DESC = (Label)e.Item.FindControl("lblCOV_DESC");

                Label lblDeductible = (Label)e.Item.FindControl("lblDeductible");
                Label lblLimit = (Label)e.Item.FindControl("lblLimit");
                Label lnkDEDUCTIBLE = (Label)e.Item.FindControl("lnkDEDUCTIBLE");
                lnkDEDUCTIBLE.Text = objResourceMgr.GetString("lnkDEDUCTIBLE");
                HtmlInputHidden hidDEDUCTIBLE_TEXT = ((HtmlInputHidden)e.Item.FindControl("hidDEDUCTIBLE_TEXT"));
                if (hidDEDUCTIBLE_TEXT.Value != "")
                {
                    lnkDEDUCTIBLE.ForeColor = Color.Purple;
                }

                CheckBox cbDedReduce = (CheckBox)e.Item.FindControl("cbDedReduce");
                //ankit
                TextBox txtIndemnity_Period = ((TextBox)e.Item.FindControl("txtIndemnity_Period"));
                TextBox txtInitialRate = ((TextBox)e.Item.FindControl("txtINITIAL_RATE"));
                TextBox txtFinalRate = ((TextBox)e.Item.FindControl("txtFINAL_RATE"));
                CheckBox cbAvarageRate = (CheckBox)e.Item.FindControl("cbAvarageRate");
                TextBox txtPremium = ((TextBox)e.Item.FindControl("txtPREMIUM"));
                TextBox txtDeductible = (TextBox)e.Item.FindControl("txtDeductible");
                TextBox txtLimit = (TextBox)e.Item.FindControl("txtLimit");
                HtmlInputHidden hidLIMIT = (HtmlInputHidden)e.Item.FindControl("hidLIMIT");
                
                TextBox txtMiniDeductible = ((TextBox)e.Item.FindControl("txtMIN_DEDUCTIBLE"));
                Label lblLIMIT_TYPE = (Label)e.Item.FindControl("lblLIMIT_TYPE");
                Label lblDED_TYPE = (Label)e.Item.FindControl("lblDEDUCTIBLE_TYPE");
                Label lblLIMIT_APPL = (Label)e.Item.FindControl("lblIS_LIMIT_APPLICABLE");
                Label lblDED_APPL = (Label)e.Item.FindControl("lblIS_DEDUCT_APPLICABLE");
                Label lblIS_MAIN = (Label)e.Item.FindControl("lblIS_MAIN");
                RegularExpressionValidator revLIMIT = (RegularExpressionValidator)e.Item.FindControl("revLIMIT");
                CustomValidator csvddlDEDUCTIBLE1 = (CustomValidator)e.Item.FindControl("csvddlDEDUCTIBLE1");

                RegularExpressionValidator revMIN_DEDUCTIBLE = (RegularExpressionValidator)e.Item.FindControl("revMIN_DEDUCTIBLE");
                RegularExpressionValidator revINITIAL_RATE = (RegularExpressionValidator)e.Item.FindControl("revINITIAL_RATE");
                RegularExpressionValidator revFINAL_RATE = (RegularExpressionValidator)e.Item.FindControl("revFINAL_RATE");
                RegularExpressionValidator revPREMIUM = (RegularExpressionValidator)e.Item.FindControl("revPREMIUM");
                RegularExpressionValidator revIndemnity = (RegularExpressionValidator)e.Item.FindControl("revIndemnity");



                //modified by lalit
                RequiredFieldValidator rfvPREMIUM = (RequiredFieldValidator)e.Item.FindControl("rfvPREMIUM");
              //  CustomValidator csvPREMIUM = (CustomValidator)e.Item.FindControl("csvPREMIUM");
                RequiredFieldValidator rfvLIMIT = (RequiredFieldValidator)e.Item.FindControl("rfvLIMIT");
                CustomValidator csvLIMIT = (CustomValidator)e.Item.FindControl("csvLIMIT");
                CustomValidator csvDEDUCTIBLE = (CustomValidator)e.Item.FindControl("csvDEDUCTIBLE");
                CustomValidator csvMIN_DEDUCTIBLE = (CustomValidator)e.Item.FindControl("csvMIN_DEDUCTIBLE");
                CustomValidator csvINITIAL_RATE = (CustomValidator)e.Item.FindControl("csvINITIAL_RATE");
                CustomValidator csvFINAL_RATE = (CustomValidator)e.Item.FindControl("csvFINAL_RATE");
                CustomValidator csvGREATER_RATE = (CustomValidator)e.Item.FindControl("csvGREATER_RATE");
                CustomValidator csvGREATER_FINAL_RATE = (CustomValidator)e.Item.FindControl("csvGREATER_FINAL_RATE");
                CustomValidator csvLIMIT1 = (CustomValidator)e.Item.FindControl("csvLIMIT1");
                CustomValidator csvPREMIUM1 = (CustomValidator)e.Item.FindControl("csvPREMIUM1");
                //rfvPREMIUM
                rfvPREMIUM.ErrorMessage = ClsMessages.FetchGeneralMessage("1173");
               // csvPREMIUM.ErrorMessage = ClsMessages.FetchGeneralMessage("1174");
                rfvLIMIT.ErrorMessage = ClsMessages.FetchGeneralMessage("1175");
                csvLIMIT.ErrorMessage = ClsMessages.FetchGeneralMessage("1176");
                csvDEDUCTIBLE.ErrorMessage = ClsMessages.FetchGeneralMessage("1182");
                csvMIN_DEDUCTIBLE.ErrorMessage = ClsMessages.FetchGeneralMessage("1183");
                csvINITIAL_RATE.ErrorMessage = ClsMessages.FetchGeneralMessage("1184");
                csvFINAL_RATE.ErrorMessage = ClsMessages.FetchGeneralMessage("1185");
                csvGREATER_RATE.ErrorMessage = ClsMessages.FetchGeneralMessage("1432");
                csvGREATER_FINAL_RATE.ErrorMessage = ClsMessages.FetchGeneralMessage("1433");
                csvLIMIT1.ErrorMessage = ClsMessages.FetchGeneralMessage("1776");//Value is exceeding maximum limit Added by Pradeep Kushwaha itrack 440
                csvPREMIUM1.ErrorMessage = ClsMessages.FetchGeneralMessage("1776");//Value is exceeding maximum limit Added by Pradeep Kushwaha itrack 440
                //by ankit Indemnity validate
                revIndemnity.Enabled = true;
                revIndemnity.Visible = true;
                //ClsMessages.SetCustomizedXml(GetLanguageCode());
                revIndemnity.ErrorMessage = ClsMessages.FetchGeneralMessage("1177");
                revIndemnity.ValidationExpression = aRegExpIntegerPositiveNonZero;

                int intCOV_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "COV_ID"));
                string strCov_code = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "COV_CODE"));

                //Appending Node with previx as value for each Coverage in control XML
                DataGrid dg = (DataGrid)sender;
                string prefix = "";
                if ((e.Item.ItemIndex + 2) < 10)
                    prefix = dg.ID + "_ctl0" + (e.Item.ItemIndex + 2).ToString();
                else
                    prefix = dg.ID + "_ctl" + (e.Item.ItemIndex + 2).ToString();
                //this.sbCtrlXML.Append("<" + strCov_code + ">" + prefix + "</" + strCov_code + ">");
                this.sbCtrlXML.Append("<COV_CODE ID=\"" + strCov_code + "\">" + prefix + "</COV_CODE>");
                
                #region Getting Rules From XML
                //Getting Coverage Rules From XML already loaded in cache	
                //////////////////////////////////////
                ///
                //// For Checked = TRUE case
                HtmlInputHidden hidCHECKD_DISABLE = (HtmlInputHidden)e.Item.FindControl("hidCHECKDDISABLE");
                HtmlInputHidden hidCHECKD_ENABLE = (HtmlInputHidden)e.Item.FindControl("hidCHECKDENABLE");
                HtmlInputHidden hidCHECKD_SELECT = (HtmlInputHidden)e.Item.FindControl("hidCHECKDSELECT");
                HtmlInputHidden hidCHECKD_DSELECT = (HtmlInputHidden)e.Item.FindControl("hidCHECKDDSELECT");
                //hidCHECKD_DISABLE = null;
                if (hidCHECKD_DISABLE != null)
                {

                    StringBuilder sbXML = new StringBuilder();

                    XmlNode node = doc.SelectSingleNode("Root/Coverages[@Code='" + strCov_code + "' and @Checked='true' ]");

                    if (node != null)
                    {
                        //Fetch Coverages to be disabled
                        XmlNode disableNode = node.SelectSingleNode("ToDisable");
                        if (disableNode != null)
                        {
                            XmlNodeList childNodes = disableNode.SelectNodes("Coverage");
                            foreach (XmlNode disabledNode in childNodes)
                            {
                                string covCode = disabledNode.Attributes["Code"].Value;

                                if (sbXML.ToString() == "")
                                {
                                    sbXML.Append(covCode);
                                }
                                else
                                {
                                    sbXML.Append("," + covCode);
                                }
                            }
                            hidCHECKD_DISABLE.Value = sbXML.ToString();
                        }

                        //Fetch coverages to be enabled
                        sbXML.Remove(0, sbXML.Length);
                        disableNode = node.SelectSingleNode("ToEnable");
                        if (disableNode != null)
                        {
                            XmlNodeList childNodes = disableNode.SelectNodes("Coverage");
                            foreach (XmlNode disabledNode in childNodes)
                            {
                                string covCode = disabledNode.Attributes["Code"].Value;

                                if (sbXML.ToString() == "")
                                {
                                    sbXML.Append(covCode);
                                }
                                else
                                {
                                    sbXML.Append("," + covCode);
                                }
                            }
                            hidCHECKD_ENABLE.Value = sbXML.ToString();
                        }

                        //Fetch coverages to be unchecked
                        sbXML.Remove(0, sbXML.Length);
                        disableNode = node.SelectSingleNode("ToUncheck");
                        if (disableNode != null)
                        {
                            XmlNodeList childNodes = disableNode.SelectNodes("Coverage");
                            foreach (XmlNode disabledNode in childNodes)
                            {
                                string covCode = disabledNode.Attributes["Code"].Value;

                                if (sbXML.ToString() == "")
                                {
                                    sbXML.Append(covCode);
                                }
                                else
                                {
                                    sbXML.Append("," + covCode);
                                }
                            }
                            hidCHECKD_DSELECT.Value = sbXML.ToString();
                        }


                        //fetch coverages to be checked
                        sbXML.Remove(0, sbXML.Length);
                        disableNode = node.SelectSingleNode("ToCheck");
                        if (disableNode != null)
                        {
                            XmlNodeList childNodes = disableNode.SelectNodes("Coverage");
                            foreach (XmlNode disabledNode in childNodes)
                            {
                                string covCode = disabledNode.Attributes["Code"].Value;

                                if (sbXML.ToString() == "")
                                {
                                    sbXML.Append(covCode);
                                }
                                else
                                {
                                    sbXML.Append("," + covCode);
                                }
                            }
                            hidCHECKD_SELECT.Value = sbXML.ToString();
                        }


                    }
                    else
                    {
                        hidCHECKD_DISABLE.Value = "";
                        hidCHECKD_ENABLE.Value = "";
                        hidCHECKD_DSELECT.Value = "";
                        hidCHECKD_SELECT.Value = "";
                    }
                }

                //// For Checked = FALSE case
                HtmlInputHidden hidUNCHECKD_DISABLE = (HtmlInputHidden)e.Item.FindControl("hidUNCHECKDDISABLE");
                HtmlInputHidden hidUNCHECKD_ENABLE = (HtmlInputHidden)e.Item.FindControl("hidUNCHECKDENABLE");
                HtmlInputHidden hidUNCHECKD_SELECT = (HtmlInputHidden)e.Item.FindControl("hidUNCHECKDSELECT");
                HtmlInputHidden hidUNCHECKD_DSELECT = (HtmlInputHidden)e.Item.FindControl("hidUNCHECKDDSELECT");
                if (hidCHECKD_DISABLE != null)
                {

                    StringBuilder sbXML = new StringBuilder();

                    XmlNode node = doc.SelectSingleNode("Root/Coverages[@Code='" + strCov_code + "' and @Checked='false' ]");

                    if (node != null)
                    {
                        XmlNode disableNode = node.SelectSingleNode("ToDisable");
                        if (disableNode != null)
                        {
                            XmlNodeList childNodes = disableNode.SelectNodes("Coverage");
                            foreach (XmlNode disabledNode in childNodes)
                            {
                                string covCode = disabledNode.Attributes["Code"].Value;

                                if (sbXML.ToString() == "")
                                {
                                    sbXML.Append(covCode);
                                }
                                else
                                {
                                    sbXML.Append("," + covCode);
                                }
                            }
                            hidUNCHECKD_DISABLE.Value = sbXML.ToString();
                        }

                        sbXML.Remove(0, sbXML.Length);
                        disableNode = node.SelectSingleNode("ToEnable");
                        if (disableNode != null)
                        {
                            XmlNodeList childNodes = disableNode.SelectNodes("Coverage");
                            foreach (XmlNode disabledNode in childNodes)
                            {
                                string covCode = disabledNode.Attributes["Code"].Value;

                                if (sbXML.ToString() == "")
                                {
                                    sbXML.Append(covCode);
                                }
                                else
                                {
                                    sbXML.Append("," + covCode);
                                }
                            }
                            hidUNCHECKD_ENABLE.Value = sbXML.ToString();
                        }

                        sbXML.Remove(0, sbXML.Length);
                        disableNode = node.SelectSingleNode("ToUncheck");
                        if (disableNode != null)
                        {
                            XmlNodeList childNodes = disableNode.SelectNodes("Coverage");
                            foreach (XmlNode disabledNode in childNodes)
                            {
                                string covCode = disabledNode.Attributes["Code"].Value;

                                if (sbXML.ToString() == "")
                                {
                                    sbXML.Append(covCode);
                                }
                                else
                                {
                                    sbXML.Append("," + covCode);
                                }
                            }
                            hidUNCHECKD_DSELECT.Value = sbXML.ToString();
                        }


                        sbXML.Remove(0, sbXML.Length);
                        disableNode = node.SelectSingleNode("ToCheck");
                        if (disableNode != null)
                        {
                            XmlNodeList childNodes = disableNode.SelectNodes("Coverage");
                            foreach (XmlNode disabledNode in childNodes)
                            {
                                string covCode = disabledNode.Attributes["Code"].Value;

                                if (sbXML.ToString() == "")
                                {
                                    sbXML.Append(covCode);
                                }
                                else
                                {
                                    sbXML.Append("," + covCode);
                                }
                            }
                            hidUNCHECKD_SELECT.Value = sbXML.ToString();
                        }


                    }
                    else
                    {
                        hidUNCHECKD_DISABLE.Value = "";
                        hidUNCHECKD_ENABLE.Value = "";
                        hidUNCHECKD_DSELECT.Value = "";
                        hidUNCHECKD_SELECT.Value = "";
                    }
                }
                //End of Getting Coverage Rules From XML
                ///////////////////////////////////////
                #endregion
                

                lblCOV_ID.Attributes.Add("style", "display:none");

                //Populate the coverage ranges for each coverage
                DataTable dtRanges = this.dsCoverages.Tables[1];
                DataRowView drvItem = (DataRowView)e.Item.DataItem;
                DataTable dtDeductType = this.dsCoverages.Tables[3];
                DataView dvDeductType = dtDeductType.DefaultView;
                ClsVehicleCoverages.BindDropDown(ddlDedType, dvDeductType, "DEDUCT_TYPE_DESC", "DEDUCT_TYPE_ID", AppEffectiveDate);
                //Checks the checkbox if this coverage is selected
                if (drvItem["COVERAGE_ID"] != System.DBNull.Value)
                {
                    cbDelete.Checked = true;
                    if (hidIS_ACTIVE.Value.ToString() == "Y")
                        chkIS_ACTIVE.Checked = false;
                    if (hidIS_ACTIVE.Value.ToString() == "N")
                        chkIS_ACTIVE.Checked = true;

                    //Check if any of these coverages  SLL,RLCSL,BISPL,PD is saved
                    //if not than Message has to be diaplayed to prompt user for default values
                }

                if (drvItem["IS_MANDATORY"] != System.DBNull.Value)
                {
                    if (drvItem["IS_MANDATORY"].ToString() == "Y" || drvItem["IS_MANDATORY"].ToString() == "1")
                    {
                        cbDelete.Enabled = false;
                    }
                }
                //To Change the color of row if Coverage is availavle due to GrandFathered
                if (drvItem["EFFECTIVE_FROM_DATE"] != DBNull.Value)
                {
                    if (AppEffectiveDate < Convert.ToDateTime(drvItem["EFFECTIVE_FROM_DATE"]))
                    {
                        //e.Item.Attributes.CssStyle.Add("COLOR","Red");
                        e.Item.Attributes.Add("Class", "GrandFatheredCoverage");
                    }
                    else
                    {
                        e.Item.Attributes.Add("Class", "midcolora");
                    }
                }
                if (drvItem["EFFECTIVE_TO_DATE"] != DBNull.Value)
                {
                    if (AppEffectiveDate > Convert.ToDateTime(drvItem["EFFECTIVE_TO_DATE"]))
                    {
                        //e.Item.Attributes.CssStyle.Add("COLOR","Red");
                        e.Item.Attributes.Add("Class", "GrandFatheredCoverage");
                    }
                    else
                    {
                        e.Item.Attributes.Add("Class", "midcolora");
                    }

                }
                //OnBlur function for Limit text box: Extra Equipment-Comprehensive (A-15) 
                if (strCov_code == "EECOMP")
                {
                    txtLimit.Attributes.Add("onBlur", "this.value=formatAmount(this.value);ValidatorOnChange();onLimitChange(this,'" + strCov_code + "');");
                }
                else
                {
                    txtLimit.Attributes.Add("onBlur", "this.value=formatAmount(this.value);ValidatorOnChange();");
                }
                txtDeductible.Attributes.Add("onBlur", "this.value=formatAmount(this.value);ValidatorOnChange();");
                txtMiniDeductible.Attributes.Add("onBlur", "this.value=formatAmount(this.value);");
                txtInitialRate.Attributes.Add("onBlur", "this.value=formatRate(this.value,4);ValidatorOnChange();");
                txtFinalRate.Attributes.Add("onBlur", "this.value=formatRate(this.value,4);ValidatorOnChange();");
                txtPremium.Attributes.Add("onBlur", "this.value=formatAmount(this.value);");
                //DataView for Limist and deductibles
                DataView dvLimitsRanges = new DataView(dtRanges);
                DataView dvDedRanges = new DataView(dtRanges);
                //Set Filter for limits and deductibles
                dvLimitsRanges.RowFilter = "COV_ID = " + intCOV_ID.ToString() + " AND LIMIT_DEDUC_TYPE = 'Limit'";
                dvDedRanges.RowFilter = "COV_ID = " + intCOV_ID.ToString() + " AND LIMIT_DEDUC_TYPE = 'Deduct'";
                //Select the ranges applicable to this Coverage
                DataRow[] drRanges = dtRanges.Select("COV_ID = " + intCOV_ID.ToString());

                lblLIMIT_TYPE.Attributes.Add("style", "display:none");
                lblDED_TYPE.Attributes.Add("style", "display:none");
                lblLIMIT_APPL.Attributes.Add("style", "display:none");
                lblDED_APPL.Attributes.Add("style", "display:none");

                //Set up validators, messages, regex etc
                RegularExpressionValidator revLimit = (RegularExpressionValidator)e.Item.FindControl("revLIMIT");
                revLimit.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
                /*
                revLimit.ValidationExpression = aRegExpDoublePositiveWithZero;
                */

                //ClsMessages.SetCustomizedXml(GetLanguageCode());
                revLimit.ValidationExpression = aRegExpCurrencyformat;

                revMIN_DEDUCTIBLE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
                revMIN_DEDUCTIBLE.ValidationExpression = aRegExpCurrencyformat;
                revINITIAL_RATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
                revINITIAL_RATE.ValidationExpression = aRegExpDoublePositiveWithZeroFourDeci;
                revFINAL_RATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
                revFINAL_RATE.ValidationExpression = aRegExpDoublePositiveWithZeroFourDeci;
                revPREMIUM.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
                revPREMIUM.ValidationExpression = aRegExpCurrencyformat;

                revMIN_DEDUCTIBLE.Enabled = true;
                revMIN_DEDUCTIBLE.Visible = true;
                revINITIAL_RATE.Enabled = true;
                revINITIAL_RATE.Visible = true;
                revFINAL_RATE.Enabled = true;
                revFINAL_RATE.Visible = true;
                revPREMIUM.Enabled = true;
                revPREMIUM.Visible = true;

                RegularExpressionValidator revDeductible = (RegularExpressionValidator)e.Item.FindControl("revDEDUCTIBLE");
                revDeductible.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
                //revDeductible.ValidationExpression = aRegExpDoublePositiveWithZero;
                revDeductible.ValidationExpression = aRegExpCurrencyformat;

                ddlLimit.Attributes.Add("COVERAGE_ID", intCOV_ID.ToString());
                //ddlLimit.Attributes.Add("COVERAGE_CODE",strCov_code);

                string strLimitApply = drvItem["IS_LIMIT_APPLICABLE"].ToString();
                string strDedApply = drvItem["IS_DEDUCT_APPLICABLE"].ToString();

                string strLimitType = drvItem["LIMIT_TYPE"].ToString();
                string strDedType = drvItem["DEDUCTIBLE_TYPE"].ToString();
                cbRIDelete.Checked = drvItem["RI_APPLIES"].ToString() == "Y" ? true : false;

                #region setting Limits
                switch (strLimitType)
                {
                    case "1":
                        //Flat

                        ClsVehicleCoverages.BindDropDown(ddlLimit, dvLimitsRanges, "Limit_1_Display", "LIMIT_DEDUC_ID", AppEffectiveDate);
                        //Hide the Controls which are not relevant
                        txtLimit.Visible = false;
                        revLIMIT.Enabled = false;
                        revLIMIT.Visible = false;
                        if (ddlLimit.Items.Count > 0)
                        {
                            lblLimit.Attributes.CssStyle.Add("display", "none");
                        }
                        if (drvItem["COVERAGE_ID"] == System.DBNull.Value)
                        {
                            ddlLimit.Attributes.CssStyle.Add("display", "none");
                            lblLimit.Attributes.CssStyle.Add("display", "inline");
                            lblLimit.Text = "";
                        }
                        DataRow[] drDef = dvLimitsRanges.Table.Select("IS_DEFAULT='Y'");

                        if (drDef != null && drDef.Length > 0)
                        {
                            if (drDef[0]["LIMIT_1_DISPLAY"] != System.DBNull.Value)
                            {
                                ListItem li = ddlLimit.Items.FindByText(drDef[0]["LIMIT_1_DISPLAY"].ToString());

                                if (li != null)
                                {
                                    li.Selected = true;
                                }
                            }
                        }

                        ClsCoverages.SelectValueInDropDown(ddlLimit, DataBinder.Eval(e.Item.DataItem, "LIMIT_ID"));
                        break;
                    case "2":
                        ClsCoverages.BindDropDown(ddlLimit, dvLimitsRanges, "SplitAmount", "LIMIT_DEDUC_ID", AppEffectiveDate);
                        //Hide the Controls which are not relevant
                        txtLimit.Visible = false;
                        revLIMIT.Enabled = false;
                        revLIMIT.Visible = false;
                        if (ddlLimit.Items.Count > 0)
                        {
                            lblLimit.Attributes.CssStyle.Add("display", "none");
                        }
                        if (drvItem["COVERAGE_ID"] == System.DBNull.Value)
                        {
                            ddlLimit.Attributes.CssStyle.Add("display", "none");
                            lblLimit.Attributes.CssStyle.Add("display", "inline");
                            lblLimit.Text = "";
                        }

                        ClsCoverages.SelectValueInDropDown(ddlLimit, DataBinder.Eval(e.Item.DataItem, "LIMIT_ID"));
                        break;
                    case "0":
                        txtLimit.Visible = false;
                        revLIMIT.Enabled = false;
                        revLIMIT.Visible = false;
                        if (drvItem["COVERAGE_ID"] == System.DBNull.Value)
                        {
                            lblLimit.Text = "";
                        }
                        ddlLimit.Visible = false;
                        break;

                    case "3":

                        //Hide the Controls which are not relevant
                        ddlLimit.Visible = false;
                        lblLimit.Attributes.CssStyle.Add("display", "none");
                        if (drvItem["COVERAGE_ID"] == System.DBNull.Value)
                        {
                            //txtLimit.Attributes.CssStyle.Add("display", "none");
                            lblLimit.Attributes.CssStyle.Add("display", "none");
                            lblLimit.Text = "";
                        }

                        //Open
                        revLIMIT.Enabled = true;
                        revLIMIT.Visible = true;
                        /*if (intCOV_ID == 117 )
                        {
                            txtLimit.BorderStyle = BorderStyle.None;
                            txtLimit.ReadOnly = true;
                            txtLimit.CssClass = "midcoloraReadOnlyTextBox";
                            txtLimit.Font.Bold = true;
                            revLIMIT.Enabled = false;
                            //revLIMIT.Visible = false;
                        }*/

                        if (DataBinder.Eval(e.Item.DataItem, "LIMIT_1") != System.DBNull.Value)
                        {   //Commented by Pradeep Kushwaha on 17 March 2011 -itrack 440
                            //txtLimit.Text = String.Format("{0:,#,###}", DataBinder.Eval(e.Item.DataItem, "LIMIT_1"));
                            txtLimit.Text = DataBinder.Eval(e.Item.DataItem, "LIMIT_1").ToString();
                            if (txtLimit.Text.ToString() != "")
                                txtLimit.Text = Convert.ToDouble(txtLimit.Text).ToString("N", nfi);
                            //txtLimit.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"LIMIT_1"));
                        }

                        break;
                }
                #endregion
                #region setting Deductibles
                switch (strDedType)
                {
                    case "1":
                        //Flat
                        /* ClsCommon.SelectValueinDDL(ddlDed,DataBinder.Eval(e.Item.DataItem,"DEDUC_ID"));*/
                        ClsCoverages.BindDropDown(ddlDed, dvDedRanges, "Limit_1_Display", "LIMIT_DEDUC_ID", AppEffectiveDate);

                        //Hide the Controls which are not relevant
                        txtDeductible.Visible = false;
                        revDeductible.Enabled = false;
                        revDeductible.Visible = false;
                        if (ddlDed.Items.Count > 0)
                        {
                            lblDeductible.Attributes.CssStyle.Add("display", "none");
                        }
                        if (drvItem["COVERAGE_ID"] == System.DBNull.Value)
                        {
                            ddlDed.Attributes.CssStyle.Add("display", "none");
                            lblDeductible.Attributes.CssStyle.Add("display", "inline");
                            lblDeductible.Text = "";
                        }

                        ClsCoverages.SelectValueInDropDown(ddlDed, DataBinder.Eval(e.Item.DataItem, "DEDUC_ID"));

                        break;
                    case "2":
                        //Split
                        /*ClsCommon.SelectValueinDDL(ddlDed,DataBinder.Eval(e.Item.DataItem,"DEDUC_ID"));*/
                        ClsCoverages.BindDropDown(ddlDed, dvDedRanges, "SplitAmount", "LIMIT_DEDUC_ID", AppEffectiveDate);

                        //Hide Controls which are not applicable
                        txtDeductible.Visible = false;
                        revDeductible.Enabled = false;
                        revDeductible.Visible = false;
                        if (ddlDed.Items.Count > 0)
                        {
                            lblDeductible.Attributes.CssStyle.Add("display", "none");
                        }
                        if (drvItem["COVERAGE_ID"] == System.DBNull.Value)
                        {
                            ddlDed.Attributes.CssStyle.Add("display", "none");
                            lblDeductible.Attributes.CssStyle.Add("display", "inline");
                            lblDeductible.Text = "";
                        }

                        ClsCoverages.SelectValueInDropDown(ddlDed, DataBinder.Eval(e.Item.DataItem, "DEDUC_ID"));
                        break;
                    case "0":
                        txtDeductible.Visible = false;
                        revDeductible.Enabled = false;
                        revDeductible.Visible = false;
                        lblDeductible.Attributes.CssStyle.Add("display", "none");
                        ddlDed.Visible = false;
                        break;

                    case "3":
                        //Open

                        ddlDed.Visible = false;
                        lblDeductible.Attributes.CssStyle.Add("display", "none");
                        if (drvItem["COVERAGE_ID"] == System.DBNull.Value)
                        {
                            //txtDeductible.Attributes.CssStyle.Add("display", "none");
                            lblDeductible.Attributes.CssStyle.Add("display", "none");
                            lblDeductible.Text = "";
                        }

                        if (DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE_1") != System.DBNull.Value)
                        {
                            //Commented by Pradeep Kushwaha on 17-March-2011 itrack 440
                            //txtDeductible.Text = String.Format("{0:,#,###}", DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE_1"));
                            txtDeductible.Text = DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE_1").ToString();
                            if (txtDeductible.Text != "")
                                txtDeductible.Text = Convert.ToDouble(txtDeductible.Text).ToString("N", nfi);
                        }
                        revDeductible.Enabled = true;
                        revDeductible.Visible = true;
                        //TxtDeductible has to shown as lable
                        if (intCOV_ID == 116 || intCOV_ID == 843 || intCOV_ID == 997 || intCOV_ID == 118 || strCov_code == "SPA8" || strCov_code == "EBM49" || strCov_code == "CEBM49")
                        {
                            txtDeductible.BorderStyle = BorderStyle.None;
                            txtDeductible.ReadOnly = true;
                            txtDeductible.CssClass = "midcoloraReadOnlyTextBox";
                            txtDeductible.Font.Bold = true;
                        }

                        if (intCOV_ID == 843)
                        {
                            //If no record saved
                            if (drvItem["COVERAGE_ID"] == System.DBNull.Value)
                            {
                                txtDeductible.Text = "50";
                                txtDeductible.Text = Convert.ToDouble(txtDeductible.Text).ToString("N", nfi);
                            }
                            else
                            {
                                //txtDeductible.Text = String.Format("{0:,#,###}", DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE_1"));
                                txtDeductible.Text = DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE_1").ToString();
                                if (txtDeductible.Text.ToString() != "")
                                    txtDeductible.Text = Convert.ToDouble(txtDeductible.Text).ToString("N", nfi);
                            }
                        }
                        break;
                    ///////////////////////////
                }
                #endregion
                ClsCoverages.SelectValueInDropDown(ddlDedType, DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE_1_TYPE"));
                txtIndemnity_Period.Text = drvItem["INDEMNITY_PERIOD"].ToString();
                txtMiniDeductible.Text = drvItem["MINIMUM_DEDUCTIBLE"].ToString();
                cbDedReduce.Checked = drvItem["DEDUCTIBLE_REDUCES"].ToString() == "Y" ? true : false;
                txtInitialRate.Text = drvItem["INITIAL_RATE"].ToString();
                txtFinalRate.Text = drvItem["FINAL_RATE"].ToString();
                cbAvarageRate.Checked = drvItem["AVERAGE_RATE"].ToString() == "Y" ? true : false;
                txtPremium.Text = drvItem["WRITTEN_PREMIUM"].ToString();

                if (txtMiniDeductible.Text.Trim() != "")
                    txtMiniDeductible.Text = Convert.ToDouble(txtMiniDeductible.Text).ToString("N", nfi);

                if (txtPremium.Text.Trim() != "")
                    txtPremium.Text = Convert.ToDouble(txtPremium.Text).ToString("N", nfi);


                nfi.NumberDecimalDigits = 4;
                if (txtInitialRate.Text.Trim() != "")
                    txtInitialRate.Text = Convert.ToDouble(txtInitialRate.Text).ToString("N", nfi);
                if (txtFinalRate.Text.Trim() != "")
                    txtFinalRate.Text = Convert.ToDouble(txtFinalRate.Text).ToString("N", nfi);

                nfi.NumberDecimalDigits = 2;
                //Enable / disable controls////////////////////////////////////
                string disable = "";
                string disable1 = "";
                string script = "";
                //Function to check business rules: enable /disable coverages
                script = "onButtonClick(document.getElementById('" + cbDelete.ClientID + "'),'" + rowCount.ToString() + "')";

                if (sbScript.ToString() == "")
                {
                    sbScript.Append(script);
                }
                else
                {
                    sbScript.Append(";" + script);
                }

                cbDelete.Attributes.Add("IDs", disable);
                cbDelete.Attributes.Add("Codes", disable1);
                //Function to enable /disable controls based on Coverage type
                string disableScript = "DisableControls('" + cbDelete.ClientID + "')";
                if (sbDisableScript.ToString() == "")
                {
                    sbDisableScript.Append(disableScript);
                }
                else
                {
                    sbDisableScript.Append(";" + disableScript);
                }

                //Add on click attributes
                if (script != "")
                {
                    cbDelete.Attributes.Add("onClick", "javascript:" + script + ";" + disableScript + ";onselectEnablePremiumRfv(this)");
                }
                else
                {
                    cbDelete.Attributes.Add("onClick", "javascript:" + disableScript + ";onselectEnablePremiumRfv(this)");
                }

                e.Item.Attributes.Add("id", "Row_" + strCov_code);
                e.Item.Attributes.Add("code", strCov_code);

                if (lblIS_MAIN.Text.Trim().Equals("1"))
                {
                    lblCOV_DESC.Style.Add("font-weight", "Bold");
                }

                hidLIMIT.Value = txtLimit.Text;//Added by Lalit April 26,2011
            }
        }

        #endregion

        /// <summary>
        /// Registers javascript code with the page 
        /// </summary>
        private void RegisterScript()
        {
            if (!ClientScript.IsStartupScriptRegistered("Test"))
            {
                string strCode = @"<script>firstTime = true;" + this.sbScript.ToString() + "</script>";
                string strDisable = @"<script>firstTime = false;</script>";
                ClientScript.RegisterStartupScript(this.GetType(),"Test", strCode + strDisable);
            }
        }
        /// <summary>
        /// Sets the workflow properties
        /// </summary>
        private void SetWorkFlowControl()
        {
            if (base.ScreenId == "227_1" || base.ScreenId == "231_1" || base.ScreenId == "246_2")
            {
                myWorkFlow.IsTop = false;
                myWorkFlow.ScreenID = base.ScreenId;
                myWorkFlow.AddKeyValue("CUSTOMER_ID", GetCustomerID());
                myWorkFlow.AddKeyValue("POLICY_ID", GetPolicyID());
                myWorkFlow.AddKeyValue("POLICY_VERSION_ID", GetPolicyVersionID());
                myWorkFlow.GetScreenStatus();
                myWorkFlow.SetScreenStatus();
            }
        }
        private void SetCaptions()
        {
            lblPolicyCaption.Text = objResourceMgr.GetString("lblPolicyCaption");
            lblTitle.Text = objResourceMgr.GetString("lblTitle");
            lblMsg.Text = objResourceMgr.GetString("lblMsg");
            hidAlertMsg.Value = objResourceMgr.GetString("hidAlertMsg");
            hidExcludeMsg.Value = objResourceMgr.GetString("hidExcludeMsg");
            dgPolicyCoverages.Columns[0].HeaderText = objResourceMgr.GetString("hdrSelect");
            dgPolicyCoverages.Columns[1].HeaderText = objResourceMgr.GetString("chkIS_ACTIVE");
            dgPolicyCoverages.Columns[2].HeaderText = objResourceMgr.GetString("hdrRiApplies");
            dgPolicyCoverages.Columns[3].HeaderText = objResourceMgr.GetString("hdrCoverage");
            //ankit new column INDEMNITY PERIOD added so order increased by on
            dgPolicyCoverages.Columns[4].HeaderText = objResourceMgr.GetString("hdrIndemnityPeriod");
            dgPolicyCoverages.Columns[5].HeaderText = objResourceMgr.GetString("hdrSI");
            dgPolicyCoverages.Columns[6].HeaderText = objResourceMgr.GetString("hdrDeductType");
            dgPolicyCoverages.Columns[7].HeaderText = objResourceMgr.GetString("hdrDeductAmount");
            dgPolicyCoverages.Columns[8].HeaderText = objResourceMgr.GetString("hdrMinDeduct");
            dgPolicyCoverages.Columns[9].HeaderText = objResourceMgr.GetString("hdrDeductReduces");
            dgPolicyCoverages.Columns[10].HeaderText = objResourceMgr.GetString("hdrInitialRate");
            dgPolicyCoverages.Columns[11].HeaderText = objResourceMgr.GetString("hdrFinalRate");
            dgPolicyCoverages.Columns[12].HeaderText = objResourceMgr.GetString("hdrAverageRate");
            dgPolicyCoverages.Columns[13].HeaderText = objResourceMgr.GetString("hdrPremium");
            lblTOTAL_COVERAGE_PREMIUM.Text = objResourceMgr.GetString("txtTOTAL_COVERAGE_PREMIUM");//Added for itrack 943
        }
        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgPolicyCoverages.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgPolicyCoverages_ItemDataBound);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

        //master policy imlimentation
        private bool SetCustomSecurityxml(string CO_APP_ID, string CalledFrom)
        {
            bool Valid = true;
            if (CalledFrom.ToUpper() != "PAPEACC" //for personal accident for passenges
                && CalledFrom.ToUpper() != "CLTVEHICLEINFO" //civil Liability Transportation
                && CalledFrom.ToUpper() != "FLVEHICLEINFO" //Facultative Liability
                && CalledFrom.ToUpper() != "GRPLF" //Group life
                && CalledFrom.ToUpper() != "CPCACC" //for group personal accident for passenger

                ) 
                return Valid;

            if (GetTransaction_Type().Trim() == MASTER_POLICY)
            {
                string SecurityXml = base.CustomSecurityXml(CO_APP_ID);
                btnSave.PermissionString = SecurityXml;
            }
            return Valid;
        }
    }
}
