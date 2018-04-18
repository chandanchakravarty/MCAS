/******************************************************************************************
<Author					: -   Sneha
<Start Date				: -	  1/12/2011
<End Date				: -	
<Description			: -  Coverages Information screen
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			:  
<Modified By			:   
<Purpose				:  
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
using Cms.Model.Application.HomeOwners;
using Cms.Model.Application;
using Cms.Model.Policy;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using System.Xml;
using System.Text;

namespace Cms.Policies.Aspx.BOP
{
    public partial class AddLiabilityCoverage : Cms.Policies.policiesbase
    {
        public string calledFrom = "";
        DataSet dsCoverages = null;
        DataSet dsCoverages2 = null;
        XmlDocument xmldoc = new XmlDocument();
        StringBuilder sbScript = new StringBuilder();
        StringBuilder sbDisableScript = new StringBuilder();
        decimal coverageA = 0;
        int product = 0;
        int product2 = 0;
        private StringBuilder sbCtrlXML = new StringBuilder();
        public decimal replValue = 0;
        public decimal marketValue = 0;
        private int stateID;
        RuleData homeRuleData;
        protected Cms.CmsWeb.Controls.CmsButton btnDelete;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidREP_COST;
        private System.DateTime AppEffectiveDate;
        private System.DateTime AppEffectiveDate2;
       public int i = 0;
        private void Page_Load(object sender, System.EventArgs e)
        {
            trError.Visible = false;

           

            base.ScreenId = "61_6";
            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;
            //btnSave.Attributes.Add("Onclick", "OnCoverageChange();SetHO11();");
            
          
            if (!Page.IsPostBack)
            {

                hidCustomerID.Value = GetCustomerID();
                hidAppID.Value = GetPolicyID();
                hidAppVersionID.Value = GetPolicyVersionID();
                hidcbWBSPO.Value = "false";
                this.hidREC_VEH_ID.Value = "1";
                btnSave.Attributes.Add("onclick", "javascript:return setvalue();");
                BindGrid();
                BindGrid2();
            }

        }
// *********************************Grid 1****************************************

        private void BindGrid()
        {
            ClsHomeCoverages objHome = new ClsHomeCoverages();

           
            dsCoverages = objHome.GetBopGeneralCoverage(Convert.ToInt32(hidCustomerID.Value),
                Convert.ToInt32(hidAppID.Value),
                Convert.ToInt32(hidAppVersionID.Value),
                 "LL");
  
            DataTable dataTable = dsCoverages.Tables[0];
            DataTable dtProduct = this.dsCoverages.Tables[2];
            if (dtProduct != null)
            {
                if (dtProduct.Rows.Count > 0)
                {
                    if (dtProduct.Rows[0]["POLICY_TYPE"] != System.DBNull.Value)
                    {
                        product = Convert.ToInt32(dtProduct.Rows[0]["POLICY_TYPE"]);
                        this.hidProduct.Value = product.ToString();

                    }
                }
            }

   
            hidOldData.Value = ClsCommon.GetXMLEncoded(dataTable);
            
            this.sbCtrlXML.Append("<Root>");

             dgCoverages.DataSource = dsCoverages.Tables[0];
            dgCoverages.DataBind();

            this.hidROW_COUNT.Value = dgCoverages.Items.Count.ToString();
           
            this.sbCtrlXML.Append("</Root>");
            sbCtrlXML.Replace("\\", "");
            this.hidControlXML.Value = sbCtrlXML.ToString();
            
            RegisterScript();
          
        }


        #region OnItemDataBound Function for Binding Grid
        private void OnItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            
            e.Item.Attributes.Add("Class", "midcolora");
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                CheckBox cbDelete = (CheckBox)e.Item.FindControl("cbDelete");
                HtmlSelect ddlLimit = (HtmlSelect)e.Item.FindControl("ddlLIMIT");
                HtmlSelect ddlDed = (HtmlSelect)e.Item.FindControl("ddlDEDUCTIBLE");
                Label lblDeductible = (Label)e.Item.FindControl("lblDeductible");
               
                System.Web.UI.HtmlControls.HtmlGenericControl lblLimit = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("lblLimit");
                TextBox txtbox = (TextBox)e.Item.FindControl("txtDEDUCTIBLE_1_TYPE");
                Label lblNoDeductible = (Label)e.Item.FindControl("lblNoCoverage");
                Label lblNoLimit = (Label)e.Item.FindControl("lblNoCoverageLimit");
                Label lblDEDUCTIBLE_TYPE = (Label)e.Item.FindControl("lblDEDUCTIBLE_TYPE");
                TextBox txtLIMIT = (TextBox)e.Item.FindControl("txtLIMIT");
                HtmlInputHidden hidCbDelete = (System.Web.UI.HtmlControls.HtmlInputHidden)e.Item.FindControl("hidcbDelete");

                RequiredFieldValidator rfvLIMIT = (RequiredFieldValidator)e.Item.FindControl("rfvLIMIT");
                RangeValidator rngDWELLING_LIMIT = (RangeValidator)e.Item.FindControl("rngDWELLING_LIMIT");
                RangeValidator rngDEDUCTIBLE = (RangeValidator)e.Item.FindControl("rngDEDUCTIBLE");

                RegularExpressionValidator revValidator1 = (RegularExpressionValidator)e.Item.FindControl("revLIMIT_DEDUC_AMOUNT");
                revValidator1.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");
                revValidator1.ValidationExpression = aRegExpDoublePositiveWithZero;

                RegularExpressionValidator revLIMIT = (RegularExpressionValidator)e.Item.FindControl("revLIMIT");
                revLIMIT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");
                revLIMIT.ValidationExpression = aRegExpDoublePositiveWithZero;
                HtmlInputHidden hidLIMIT = ((HtmlInputHidden)e.Item.FindControl("hidLIMIT"));
                HtmlSelect ddlAddDed = (HtmlSelect)e.Item.FindControl("ddladdDEDUCTIBLE");
                TextBox txtaddDEDUCTIBLE = (TextBox)e.Item.FindControl("txtaddDEDUCTIBLE");
                Label lblDEDUCTIBLE_AMOUNT = (Label)e.Item.FindControl("lblDEDUCTIBLE_AMOUNT");
                Label lblNoaddDEDUCTIBLE = (Label)e.Item.FindControl("lblNoaddDEDUCTIBLE");
                Label lblCOV_ID = (Label)e.Item.FindControl("lblCOV_ID");
                CustomValidator csv = (CustomValidator)e.Item.FindControl("csvLIMIT_DEDUC_AMOUNT");
                CustomValidator csvLimit = (CustomValidator)e.Item.FindControl("csvLIMIT");

                int intCOV_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "COV_ID"));
                string strCov_code = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "COV_CODE"));

                DataGrid dg = (DataGrid)sender;
                string prefix = dg.ID + "__ctl" + (e.Item.ItemIndex + 2).ToString();
                this.sbCtrlXML.Append("<COV_CODE ID=\"" + strCov_code + "\">" + prefix + "</COV_CODE>");

                DataRowView drvItem = (DataRowView)e.Item.DataItem;

                //if (lblLimit.InnerText == "\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\tIncluded\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t");
                //{
                //    lblLimit.InnerText = "";
                //}
               
                if (drvItem["EFFECTIVE_FROM_DATE"] != DBNull.Value)
                {
                    if (AppEffectiveDate < Convert.ToDateTime(drvItem["EFFECTIVE_FROM_DATE"]))
                    {
                       
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
                        
                        e.Item.Attributes.Add("Class", "GrandFatheredCoverage");
                    }
                    else
                    {
                        e.Item.Attributes.Add("Class", "midcolora");
                    }

                }
                
                DataTable dtRanges = this.dsCoverages.Tables[1];

                string strLimitType = drvItem["LIMIT_TYPE"].ToString();
                string strDedType = drvItem["DEDUCTIBLE_TYPE"].ToString();
                string strAddDedType = drvItem["ADDDEDUCTIBLE_TYPE"].ToString();
                AppEffectiveDate = (DateTime)dsCoverages.Tables[3].Rows[0]["APP_EFFECTIVE_DATE"];
                if (strLimitType != "")
                {
                   
                }

                if (strDedType != "")
                {
                    ddlDed.Visible = true;
                }


                if (drvItem["COVERAGE_ID"] != System.DBNull.Value)
                {
                    cbDelete.Checked = true;
                    hidCbDelete.Value = "true";
                    if (strCov_code == "WBSPO" && hidWBSPOEXIST.Value == "true")
                    {
                        hidWBSPOEXIST.Value = "EXIST";
                    }
                    
                }
                else
                {
                    hidCbDelete.Value = "false";
                    cbDelete.Checked = false;
                }

                if (drvItem["IS_MANDATORY"] != System.DBNull.Value)
                {
                    if (drvItem["IS_MANDATORY"].ToString() == "Y" || drvItem["IS_MANDATORY"].ToString() == "1")
                    {
                        cbDelete.Enabled = false;
                    }

                }

             

             
                string conditionalScript = "";

               

                conditionalScript = "onCheck('" + cbDelete.ClientID + "')";


               
                e.Item.Attributes.Add("id", "Row_" + intCOV_ID.ToString());
              



                DataView dvLimitsRanges = new DataView(dtRanges);
                DataView dvDedRanges = new DataView(dtRanges);
                DataView dvAddDedRanges = new DataView(dtRanges);


                
                dvLimitsRanges.RowFilter = "COV_ID = " + intCOV_ID.ToString() + " AND LIMIT_DEDUC_TYPE = 'Limit'";
                dvDedRanges.RowFilter = "COV_ID = " + intCOV_ID.ToString() + " AND LIMIT_DEDUC_TYPE = 'Deduct'";
                dvAddDedRanges.RowFilter = "COV_ID = " + intCOV_ID.ToString() + " AND LIMIT_DEDUC_TYPE = 'Addded'";

                DataRow[] drRanges = dtRanges.Select("COV_ID = " + intCOV_ID.ToString());


               
              
               
                if (strCov_code == "BIPD")
                {
                    lblNoLimit.Visible = false;
                    //cbDelete.Enabled = false;
                    //cbDelete.Checked=true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                    hidCov_Code.Value = "BIPD";
                   
                }
         

                if (strCov_code == "PAIPO")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                    cbDelete.Checked = true;
                }

                if (strCov_code == "MPP")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }

                if (strCov_code == "DPRY")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }

                if (strCov_code == "OTPOA")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    
                    cbDelete.Checked = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                    ddlLimit.Visible = false;
                }

                if (strCov_code == "PCOA")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }

                if (strCov_code == "PDLD")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }

                if (strCov_code == "HA")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }

                if (strCov_code == "NOA")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                }

                if (strCov_code == "EBL")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }

                if (strCov_code == "GC")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }

                if (strCov_code == "GCo")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }

                if (strCov_code == "LME")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }

                if (strCov_code == "PCO")
                {
                    lblNoLimit.Visible = false;
                   // cbDelete.Checked = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                    ddlLimit.Visible = false;

                }



                if (drvItem["Limit_1"] != DBNull.Value && drvItem["COV_ID"].ToString() == "10119")
                {
                    cbDelete.Checked = true;
                }
                else if (drvItem["Limit_1"] != DBNull.Value && drvItem["COV_ID"].ToString() == "5001")
                {
                    cbDelete.Enabled = false;
                    cbDelete.Checked = true;
                }
                else if (drvItem["Limit_1"] != DBNull.Value && drvItem["COV_ID"].ToString() == "5005")
                {
                    cbDelete.Enabled = false;
                    cbDelete.Checked = true;
                }
                else if (drvItem["Limit_1"] != DBNull.Value && drvItem["COV_ID"].ToString() == "5002")
                {
                    cbDelete.Checked = true;
                }
                else if (drvItem["Limit_1"] != DBNull.Value && drvItem["COV_ID"].ToString() == "5003")
                {
                    cbDelete.Checked = true;
                }
                else if (drvItem["Limit_1"] != DBNull.Value && drvItem["COV_ID"].ToString() == "5004")
                {
                    cbDelete.Checked = true;
                }
                else if (drvItem["DEDUCTIBLE_1"] != DBNull.Value && drvItem["COV_ID"].ToString() == "5007")
                {
                    cbDelete.Checked = true;
                }
                else if (drvItem["Limit_1"] != DBNull.Value && drvItem["COV_ID"].ToString() == "5008")
                {
                    cbDelete.Checked = true;
                }
                else if (drvItem["Limit_1"] != DBNull.Value && drvItem["COV_ID"].ToString() == "5009")
                {
                    cbDelete.Checked = true;
                }
                else if (drvItem["Limit_1"] != DBNull.Value && drvItem["COV_ID"].ToString() == "5010")
                {
                    cbDelete.Checked = true;
                }
                else if (drvItem["Limit_1"] != DBNull.Value && drvItem["COV_ID"].ToString() == "5006")
                {
                    cbDelete.Checked = true;
                }
                else if (drvItem["Limit_1"] != DBNull.Value && drvItem["COV_ID"].ToString() == "5008" && cbDelete.Checked != true)
                {
                    lblLimit.InnerText = "";
                }
                else if (drvItem["Limit_1"] != DBNull.Value && drvItem["COV_ID"].ToString() == "5009" && cbDelete.Checked != true)
                {
                    lblLimit.InnerText = "";
                }
                else if (drvItem["Limit_1"] != DBNull.Value && drvItem["COV_ID"].ToString() == "5010" && cbDelete.Checked != true)
                {
                    lblLimit.InnerText = "";
                }
                else
                {
                    if (dsCoverages.Tables[4].Rows.Count == 0)
                    {
                        if (strCov_code == "BIPD")
                        {
                            cbDelete.Enabled = false;
                            cbDelete.Checked = true;
                            lblLimit.InnerText = "";
                        }
                        if (strCov_code == "PAIPO")
                        {
                            cbDelete.Enabled = true;
                            lblLimit.InnerText = "";
                        }
                        if (strCov_code == "OTPOA")
                        {
                            cbDelete.Enabled = false;
                            cbDelete.Checked = true;
                            lblLimit.InnerText = "";
                        }
                        if (strCov_code == "PCO")
                        {
                            cbDelete.Checked = true;
                            lblLimit.InnerText = "";
                        }
                        if (strCov_code == "MPP")
                        {
                            cbDelete.Checked = true;
                            lblLimit.InnerText = "";
                        }
                        if (strCov_code == "DPRY")
                        {
                            cbDelete.Checked = true;
                            lblLimit.InnerText = "";
                        }
                        
                        if (strCov_code == "HA")
                        {
                            cbDelete.Checked = false;
                            lblLimit.InnerText = "";
                        }
                        if (strCov_code == "NOA")
                        {
                            cbDelete.Checked = false;
                            lblLimit.InnerText = "";
                        }
                        if (strCov_code == "EBL")
                        {
                            cbDelete.Checked = false;
                            lblLimit.InnerText = "";
                        }
                    }
                    if (dsCoverages.Tables[5].Rows.Count == 0)
                    {
                        if (strCov_code == "PDLD")
                        {
                            cbDelete.Checked = true;
                            lblLimit.InnerText = "";
                        }
                    }
                }




                switch (strLimitType)
                {
                    case "1":
                        //Flat
                        ddlLimit.Visible = true;
                        ClsHomeCoverages.BindDropDown(ddlLimit, dvLimitsRanges, "LIMIT_1_DISPLAY", "LIMIT_DEDUC_ID", AppEffectiveDate);
                        ClsCoverages.SelectValueInDropDown(ddlLimit, DataBinder.Eval(e.Item.DataItem, "LIMIT_ID"));
                        lblNoLimit.Visible = false;
                        lblLimit.Visible = false;
                        txtLIMIT.Visible = false;
                        rfvLIMIT.Visible = false;
                        rngDWELLING_LIMIT.Visible = false;
                        revLIMIT.Visible = false;
                        if (strCov_code == "BIPD")
                        {
                          
                            ddlLimit.Attributes.Add("onchange", "javascript:getddllimit();");
                        }
                       

                       
                        break;
                    case "2":

                        ddlLimit.Visible = false;
                       
                     
                        break;
                    default:
                            txtLIMIT.Visible = false;
                            lblLimit.Visible = false;
                            ddlLimit.Visible = false;
                            
                            break;
                }
                i++;
                switch (strDedType)
                {
                    case "1":
                        //Flat
                        ClsHomeCoverages.BindDropDown(ddlDed, dvDedRanges, "LIMIT_1_DISPLAY", "LIMIT_DEDUC_ID", AppEffectiveDate);
                        ClsCoverages.SelectValueInDropDown(ddlDed, DataBinder.Eval(e.Item.DataItem, "DEDUC_ID"));
                        lblDEDUCTIBLE_AMOUNT.Visible = false;
                        lblDeductible.Visible = false;
                        txtbox.Visible = false;
                        revValidator1.Visible = false;
                        rngDEDUCTIBLE.Visible = false;
                        csv.Visible = false;
                        ddlDed.Visible = true;
                        break;
                    case "2":

                        break;
                    default:
                        lblDEDUCTIBLE_AMOUNT.Visible = false;
                        lblDeductible.Visible = false;
                        txtbox.Visible = false;
                        revValidator1.Visible = false;
                        rngDEDUCTIBLE.Visible = false;
                        csv.Visible = false;
                        ddlDed.Visible = false;
                        break;
                   

                }
                switch (strAddDedType)
                {
                    case "1":
                        //Flat
                        ClsHomeCoverages.BindDropDown(ddlAddDed, dvAddDedRanges, "LIMIT_1_DISPLAY", "LIMIT_DEDUC_ID", DateTime.Now);
                        ClsCoverages.SelectValueInDropDown(ddlAddDed, DataBinder.Eval(e.Item.DataItem, "ADDDEDUCTIBLE_ID"));
                        
                        ddlAddDed.Visible = true;
                        lblDEDUCTIBLE_AMOUNT.Visible = false;
                        txtaddDEDUCTIBLE.Visible = false;
                        txtbox.Visible = false;
                        revValidator1.Visible = false;
                        rngDEDUCTIBLE.Visible = false;
                        lblNoaddDEDUCTIBLE.Visible = false;
                        csv.Visible = false;
                        break;
                    default:
                        ddlAddDed.Visible = false;
                        lblDEDUCTIBLE_AMOUNT.Visible = false;
                        txtaddDEDUCTIBLE.Visible = false;
                        txtbox.Visible = false;
                        revValidator1.Visible = false;
                        rngDEDUCTIBLE.Visible = false;
                        lblNoaddDEDUCTIBLE.Visible = false;
                        csv.Visible = false;
                        break;
                }
                
            }
        }
        #endregion


       

        private void dgCoverages_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            OnItemDataBound(sender, e);

        }

        private void RegisterScript()
        {
            //if ( this.sbScript.ToString() == "" ) return;

            if (!Page.IsStartupScriptRegistered("Test"))
            {
                //string strCode = @"<script>firstTime = true;" + this.sbScript.ToString() + "</script>";

                //string strDisable = @"<script>function DisableDDL(){" + this.sbDisableScript.ToString() + "} DisableDDL();firstTime = false;</script>";

                //Page.RegisterStartupScript("Test", strCode + strDisable);

            }



        }



        private void btnSave_Click(object sender, System.EventArgs e)
        {
            lblMessage.Visible = true;

            int retVal = Save();
            int retVal2 = Save2();
            if (retVal > 0)
            {
                if (Convert.ToInt32(ViewState["TotalRecords"]) == 0)
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "29");
                }
                else
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "31");
                }

                BindGrid();

                
                return;
            }

            if (retVal2 > 0)
            {
                if (Convert.ToInt32(ViewState["TotalRecords"]) == 0)
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "29");
                }
                else
                {
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "31");
                }

              

                BindGrid2();
                return;
            }
            if (retVal == -2 || retVal2 == -2)
            {
                lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "332");
                //lblMessage.Text = "One of the Coverage code already exists. Please enter another code.";
                return;
            }
        }

        private void PopulateList(ArrayList alRecr, DataGrid dgCoverages)
        {

            foreach (DataGridItem dgi in dgCoverages.Items)
            {
                if (dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem)
                {
                    CheckBox cbDelete = (CheckBox)dgi.FindControl("cbDelete");
                    HtmlSelect ddlLimit = ((HtmlSelect)dgi.FindControl("ddlLimit"));
                    HtmlSelect ddlDeductible = ((HtmlSelect)dgi.FindControl("ddlDeductible"));
                    TextBox txtbox = ((TextBox)dgi.FindControl("txtDEDUCTIBLE_1_TYPE"));
                    TextBox txtLIMIT = ((TextBox)dgi.FindControl("txtLIMIT"));
                    //Label lblLIMIT = ((Label)dgi.FindControl("lblLIMIT"));
                    HtmlInputHidden hidLIMIT = ((HtmlInputHidden)dgi.FindControl("hidLIMIT"));
                    System.Web.UI.HtmlControls.HtmlGenericControl lblLIMIT = ((System.Web.UI.HtmlControls.HtmlGenericControl)dgi.FindControl("lblLIMIT"));
                    Label lblCOV_DESC = ((Label)dgi.FindControl("lblCOV_DESC"));
                    Label lblCOV_ID = ((Label)dgi.FindControl("lblCOV_ID"));
                    
                    ClsLiabilityCoverage objInfo = new ClsLiabilityCoverage();

                    
                    objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
                    objInfo.POLICY_VERSION_ID = Convert.ToInt32(this.hidAppVersionID.Value);
                    objInfo.POLICY_ID = Convert.ToInt32(this.hidAppID.Value);
                    
                    objInfo.MODIFIED_BY = int.Parse(GetUserId());
                    HtmlInputHidden hidDEDUCTIBLE = ((HtmlInputHidden)dgi.FindControl("hidDEDUCTIBLE"));
                    HtmlInputHidden hidCbDelete = (System.Web.UI.HtmlControls.HtmlInputHidden)dgi.FindControl("hidcbDelete");

                    HtmlSelect ddlAddDed = (HtmlSelect)dgi.FindControl("ddladdDEDUCTIBLE");
                    TextBox txtaddDEDUCTIBLE = (TextBox)dgi.FindControl("txtaddDEDUCTIBLE");
                    Label lblNoaddDEDUCTIBLE = (Label)dgi.FindControl("lblNoaddDEDUCTIBLE");
                    Label lblDEDUCTIBLE_AMOUNT = ((Label)dgi.FindControl("lblDEDUCTIBLE_AMOUNT"));
                    System.Web.UI.HtmlControls.HtmlInputHidden hidlbl_DEDUCTIBLE_AMOUNT = (HtmlInputHidden)dgi.FindControl("hidlbl_DEDUCTIBLE_AMOUNT");
                    System.Web.UI.HtmlControls.HtmlGenericControl spnDEDUCTIBLE_AMOUNT_TEXT = (HtmlGenericControl)dgi.FindControl("spnDEDUCTIBLE_AMOUNT_TEXT");
                    hidCbDelete.Value = cbDelete.Checked.ToString();
                   


                   
                    objInfo.COVERAGE_ID = int.Parse(lblCOV_ID.Text);
                    if (objInfo.COVERAGE_ID == -1)
                    {
                        objInfo.ACTION = "I";
                    }
                    else
                    {
                        
                        objInfo.ACTION = "U";
                    }
                   
                    int intWBSPO = Convert.ToInt32(((Label)dgi.FindControl("lblCOV_ID")).Text);

                    if (hidCbDelete.Value == "True")
                    {
                        objInfo.COVERAGE_CODE_ID = Convert.ToInt32(((Label)dgi.FindControl("lblCOV_ID")).Text);
                        objInfo.COVERAGE_TYPE = "LL";

                        string strLimitType = ((Label)dgi.FindControl("lblLIMIT_TYPE")).Text;
                        string strDedType = ((Label)dgi.FindControl("lblDEDUCTIBLE_TYPE")).Text;
                        string strAddDedType = ((Label)dgi.FindControl("lblAddDEDUCTIBLE_TYPE")).Text;

                        if (lblLIMIT.Visible == true)
                        {
                            
                            if (hidLIMIT.Value.Trim() != "")
                            {
                                		
                                objInfo.LIMIT_1 = Convert.ToDouble(hidLIMIT.Value.Replace("$", "").Replace(",", "").Trim());
                                
                            }

                        }
                        
                            

                        switch (strLimitType)
                        {
                            case "1":
                                //Flat
                                if (ddlLimit.Items.Count > 0 && ddlLimit.Items[ddlLimit.SelectedIndex].Text.Trim() != "")
                                {
                                    
                                    string amount = ddlLimit.Items[ddlLimit.SelectedIndex].Text.Substring(0, ddlLimit.Items[ddlLimit.SelectedIndex].Text.IndexOf(" "));
                                    string text = ddlLimit.Items[ddlLimit.SelectedIndex].Text.Substring(ddlLimit.Items[ddlLimit.SelectedIndex].Text.IndexOf(" "));

                                    objInfo.LIMIT_ID = Convert.ToInt32(ddlLimit.Items[ddlLimit.SelectedIndex].Value);

                                   
                                    if (amount.Trim() != "")
                                    {
                                        objInfo.LIMIT_1 = Convert.ToDouble(amount);
                                    }

                                    objInfo.LIMIT1_AMOUNT_TEXT = text.Trim();
                                  
                                }

                                break;
                            case "2":
                                if (lblCOV_ID.Text == "5005" || lblCOV_ID.Text == "10119")
                                {
                                    objInfo.LIMIT_1 =Convert.ToDouble(hidBPID.Value);
                                }

                                if (lblCOV_ID.Text == "5003")
                                {
                                    objInfo.LIMIT_1 = 5000.00;
                                }

                                if (lblCOV_ID.Text == "5002")
                                {
                                    objInfo.LIMIT_1 = Convert.ToDouble(hidPAIPO.Value);
                                }
                                if (lblCOV_ID.Text == "5008" || lblCOV_ID.Text == "5009" || lblCOV_ID.Text == "5010")
                                {
                                    objInfo.LIMIT_1 = Convert.ToDouble(hidPAIPO.Value);
                                }
                            break;
                            case "0":
                                if (((System.Web.UI.HtmlControls.HtmlGenericControl)dgi.FindControl("lblLIMIT")).InnerText.Trim() != "")
                                {
                                   
                                    if (hidLIMIT.Value.Trim() != "")
                                    {
                                       
                                        objInfo.LIMIT_1 = Convert.ToDouble(hidLIMIT.Value.Trim().Replace("$", "").Replace(",", ""));
                                    }
                                }
                                break;
                            case "3":
                               
                                if (txtLIMIT.Visible == true)
                                {
                                    if (txtLIMIT.Text.Trim() != "")
                                    {	
                                        objInfo.LIMIT_1 = Convert.ToDouble(txtLIMIT.Text.Trim().Replace("$", ""));
                                    }
                                }
                                break;
                        }

                        switch (strDedType)
                        {
                            case "1":
                              
                                if (ddlDeductible.Items.Count > 0 && ddlDeductible.Items[ddlDeductible.SelectedIndex].Value != "")
                                {
                                    if (ddlDeductible.Visible == true)
                                    {
                                        objInfo.DEDUC_ID = Convert.ToInt32(ddlDeductible.Items[ddlDeductible.SelectedIndex].Value);
                                        objInfo.DEDUCTIBLE_1 = Convert.ToDouble(ddlDeductible.Items[ddlDeductible.SelectedIndex].Text);
                                    }
                                }
                                break;
                            case "2":

                                if (lblCOV_ID.Text == "5007")
                                {
                                    objInfo.LIMIT_1 = Convert.ToDouble(hidBPID.Value);
                                }

                                break;
                            case "0":

                                if (hidDEDUCTIBLE.Value.ToString().Trim() != "")
                                    objInfo.DEDUCTIBLE1_AMOUNT_TEXT = hidDEDUCTIBLE.Value.ToString().Trim();
                                break;
                            case "3":
                             



                                if (txtbox.Text.Trim() != "")
                                    
                                    objInfo.DEDUCTIBLE_1 = Convert.ToDouble(txtbox.Text.Trim().Replace("$", ""));

                                break;
                        }
                        switch (strAddDedType)
                        {
                            case "1":
                               

                                if (ddlAddDed.Items.Count > 0 && ddlAddDed.Items[ddlAddDed.SelectedIndex].Text.Trim() != "")
                                {
                                    string amount = ddlAddDed.Items[ddlAddDed.SelectedIndex].Text.Substring(0, ddlAddDed.Items[ddlAddDed.SelectedIndex].Text.IndexOf(" "));
                                    string text = ddlAddDed.Items[ddlAddDed.SelectedIndex].Text.Substring(ddlAddDed.Items[ddlAddDed.SelectedIndex].Text.IndexOf(" "));
                                    


                                  
                                    if (amount.Trim() != "")
                                    {
                                        objInfo.MINIMUM_DEDUCTIBLE = Convert.ToDouble(amount);
                                    }

                                    
                                }
                                break;
                            case "2":

                                break;
                            case "3":
                              
                               // objInfo.MINIMUM_DEDUCTIBLE = Convert.ToDouble(txtaddDEDUCTIBLE.Text.Trim());
                             
                                break;
                            case "4":
                                if (hidlbl_DEDUCTIBLE_AMOUNT.Value.ToString() != "")
                                {
                                    string[] amountl = new string[2];
                                    amountl = hidlbl_DEDUCTIBLE_AMOUNT.Value.ToString().Split(' ');
                                    if (objInfo.COVERAGE_CODE_ID == 112 || objInfo.COVERAGE_CODE_ID == 169)
                                    {
                                        objInfo.MINIMUM_DEDUCTIBLE = Convert.ToDouble("2");
                                    }
                                    else
                                    {
                                        if (amountl[0].Trim() != "")
                                        {
                                            objInfo.MINIMUM_DEDUCTIBLE = Convert.ToDouble(amountl[0].ToString());
                                        }
                                    }
                                    if (amountl.Length > 1)
                                    {
                                        //Case added according to ashish mail.
                                        if (objInfo.COVERAGE_CODE_ID == 112 || objInfo.COVERAGE_CODE_ID == 169)
                                        {
                                           // objInfo.DEDUCTIBLE_TEXT = "%-" + amountl[0].ToString();
                                        }
                                        else
                                        {
                                           // objInfo.DEDUCTIBLE_TEXT = amountl[1].ToString();
                                        }
                                    }
                                }
                                break;
                            case "0":
                                //txtbox.Visible = false;
                                break;


                        }

                        alRecr.Add(objInfo);

                    }
                    else
                    {
                        //ClsCoveragesInfo objInfo = new ClsCoveragesInfo();	

                        if (dgCoverages.DataKeys[dgi.ItemIndex] != System.DBNull.Value)
                        {
                            objInfo.ACTION = "D";
                            alRecr.Add(objInfo);
                        }
                    }
                }
            }

        }


        private int Save()
        {
            ArrayList alRecr = new ArrayList();
            PopulateList(alRecr, this.dgCoverages);


            ClsHomeCoverages objCoverages = new ClsHomeCoverages();

            int retVal = 1;

            int customerID = Convert.ToInt32(hidCustomerID.Value);
            int POLICY_ID = Convert.ToInt32(hidAppID.Value);
            int POLICY_VERSION_ID = Convert.ToInt32(hidAppVersionID.Value);
           

            try
            {

                retVal = objCoverages.SaveBOPGeneralNew(alRecr, hidOldData.Value, "LL", customerID, POLICY_ID, POLICY_VERSION_ID);

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;

                if (ex.InnerException != null)
                {
                    lblMessage.Text = ex.InnerException.Message;

                }
                return -4;
            }

            return retVal;
        }

///////////////**************************Grid 1 end******************************
        
//************************************** Grid 2**********************************
        private void BindGrid2()
        {
            ClsHomeCoverages objHome = new ClsHomeCoverages();


            dsCoverages2 = objHome.GetBopGeneralCoverage(Convert.ToInt32(hidCustomerID.Value),
                Convert.ToInt32(hidAppID.Value),
                Convert.ToInt32(hidAppVersionID.Value),
                 "PL");

            DataTable dataTable2 = dsCoverages2.Tables[0];
            DataTable dtProduct2 = this.dsCoverages2.Tables[2];
            if (dtProduct2 != null)
            {
                if (dtProduct2.Rows.Count > 0)
                {
                    if (dtProduct2.Rows[0]["POLICY_TYPE"] != System.DBNull.Value)
                    {
                        product2 = Convert.ToInt32(dtProduct2.Rows[0]["POLICY_TYPE"]);
                        this.hidProduct.Value = product.ToString();

                    }
                }
            }


            hidOldData2.Value = ClsCommon.GetXMLEncoded(dataTable2);

            this.sbCtrlXML.Append("<Root>");

            dgCoverages2.DataSource = dsCoverages2.Tables[0];
            dgCoverages2.DataBind();

            this.hidROW_COUNT.Value = dgCoverages2.Items.Count.ToString();

            this.sbCtrlXML.Append("</Root>");
            sbCtrlXML.Replace("\\", "");
            this.hidControlXML.Value = sbCtrlXML.ToString();

            RegisterScript1();

        }


        #region OnItemDataBound2 Function for Binding Grid
        private void OnItemDataBound2(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {

            e.Item.Attributes.Add("Class", "midcolora");
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                CheckBox cbDelete2 = (CheckBox)e.Item.FindControl("cbDelete2");
                HtmlSelect ddlLimit2 = (HtmlSelect)e.Item.FindControl("ddlLIMIT2");
                HtmlSelect ddlDed2 = (HtmlSelect)e.Item.FindControl("ddlDEDUCTIBLE2");
                Label lblDeductible2 = (Label)e.Item.FindControl("lblDeductible2");

                System.Web.UI.HtmlControls.HtmlGenericControl lblLimit2 = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("lblLimit2");
                TextBox txtbox2 = (TextBox)e.Item.FindControl("txtadditional");
                Label lblNoDeductible2 = (Label)e.Item.FindControl("lblNoCoverage2");
                Label lblNoLimit2 = (Label)e.Item.FindControl("lblNoCoverageLimit2");
                Label lblDEDUCTIBLE_TYPE2 = (Label)e.Item.FindControl("lblDEDUCTIBLE_TYPE2");
                TextBox txtLIMIT2 = (TextBox)e.Item.FindControl("txtLIMIT2");
                HtmlInputHidden hidCbDelete2 = (System.Web.UI.HtmlControls.HtmlInputHidden)e.Item.FindControl("hidcbDelete2");

                RequiredFieldValidator rfvLIMIT2 = (RequiredFieldValidator)e.Item.FindControl("rfvLIMIT2");
                RangeValidator rngDWELLING_LIMIT2 = (RangeValidator)e.Item.FindControl("rngDWELLING_LIMIT2");
                RangeValidator rngDEDUCTIBLE2 = (RangeValidator)e.Item.FindControl("rngDEDUCTIBLE2");

                RegularExpressionValidator revValidator2 = (RegularExpressionValidator)e.Item.FindControl("revLIMIT_DEDUC_AMOUNT2");
                revValidator2.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");
                revValidator2.ValidationExpression = aRegExpDoublePositiveWithZero;

                RegularExpressionValidator revLIMIT2 = (RegularExpressionValidator)e.Item.FindControl("revLIMIT2");
                revLIMIT2.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("217");
                revLIMIT2.ValidationExpression = aRegExpDoublePositiveWithZero;
                HtmlInputHidden hidLIMIT2 = ((HtmlInputHidden)e.Item.FindControl("hidLIMIT2"));
                HtmlSelect ddlAddDed2 = (HtmlSelect)e.Item.FindControl("ddladdDEDUCTIBLE2");
                TextBox txtaddDEDUCTIBLE2 = (TextBox)e.Item.FindControl("txtaddDEDUCTIBLE2");
               // TextBox txtadditional = (TextBox)e.Item.FindControl("txtadditional");
                Label lblDEDUCTIBLE_AMOUNT2 = (Label)e.Item.FindControl("lblDEDUCTIBLE_AMOUNT2");
                Label lblNoaddDEDUCTIBLE2 = (Label)e.Item.FindControl("lblNoaddDEDUCTIBLE2");
                Label lblCOV_ID2 = (Label)e.Item.FindControl("lblCOV_ID2");
                CustomValidator csv2 = (CustomValidator)e.Item.FindControl("csvLIMIT_DEDUC_AMOUNT2");
                CustomValidator csvLimit2 = (CustomValidator)e.Item.FindControl("csvLIMIT2");

                int intCOV_ID2 = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "COV_ID"));
                string strCov_code2 = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "COV_CODE"));

                DataGrid dg2 = (DataGrid)sender;
                string prefix = dg2.ID + "__ctl" + (e.Item.ItemIndex + 2).ToString();
                this.sbCtrlXML.Append("<COV_CODE ID=\"" + strCov_code2 + "\">" + prefix + "</COV_CODE>");

                DataRowView drvItem2 = (DataRowView)e.Item.DataItem;

               

                DataTable dtRanges2 = this.dsCoverages2.Tables[1];

                string strLimitType2 = drvItem2["LIMIT_TYPE"].ToString();
                string strDedType2 = drvItem2["DEDUCTIBLE_TYPE"].ToString();
                string strAddDedType2 = drvItem2["ADDDEDUCTIBLE_TYPE"].ToString();
                AppEffectiveDate2 = (DateTime)dsCoverages2.Tables[3].Rows[0]["APP_EFFECTIVE_DATE"];
                if (strLimitType2 != "")
                {

                }

                if (strDedType2 != "")
                {
                    ddlDed2.Visible = true;
                }


                if (drvItem2["COVERAGE_ID"] != System.DBNull.Value)
                {
                    cbDelete2.Checked = true;
                    hidCbDelete2.Value = "true";
                   
                }
                else
                {
                    hidCbDelete2.Value = "false";
                    cbDelete2.Checked = false;
                }

                if (drvItem2["IS_MANDATORY"] != System.DBNull.Value)
                {
                    if (drvItem2["IS_MANDATORY"].ToString() == "Y" || drvItem2["IS_MANDATORY"].ToString() == "1")
                    {
                        cbDelete2.Enabled = false;
                    }

                }

                DataView dvLimitsRanges2 = new DataView(dtRanges2);
                DataView dvDedRanges2 = new DataView(dtRanges2);
                DataView dvAddDedRanges2 = new DataView(dtRanges2);



                dvLimitsRanges2.RowFilter = "COV_ID = " + intCOV_ID2.ToString() + " AND LIMIT_DEDUC_TYPE = 'Limit'";
                dvDedRanges2.RowFilter = "COV_ID = " + intCOV_ID2.ToString() + " AND LIMIT_DEDUC_TYPE = 'Deduct'";
                dvAddDedRanges2.RowFilter = "COV_ID = " + intCOV_ID2.ToString() + " AND LIMIT_DEDUC_TYPE = 'Addded'";

                DataRow[] drRanges2 = dtRanges2.Select("COV_ID = " + intCOV_ID2.ToString());





                if (strCov_code2 == "FA")
                {
                    lblNoLimit2.Visible = false;
                    lblNoDeductible2.Visible = false;
                    lblNoaddDEDUCTIBLE2.Visible = false;
                    txtbox2.Visible = false;
                  }

                if (strCov_code2 == "ED")
                {
                    lblNoLimit2.Visible = false;
                    lblNoDeductible2.Visible = false;
                    lblNoaddDEDUCTIBLE2.Visible = false;
                    txtbox2.Visible = false;
                }

                if (strCov_code2 == "EIDP")
                {
                    lblNoLimit2.Visible = false;
                    lblNoDeductible2.Visible = false;
                    lblNoaddDEDUCTIBLE2.Visible = false;
                    txtbox2.Visible = false;
                }

                if (strCov_code2 == "FA")
                {
                    lblNoLimit2.Visible = false;
                    lblNoDeductible2.Visible = false;
                    lblNoaddDEDUCTIBLE2.Visible = false;
                    txtbox2.Visible = false;
                }

                if (strCov_code2 == "EDAL")
                {
                    lblNoLimit2.Visible = false;
                    lblNoDeductible2.Visible = false;
                    lblNoaddDEDUCTIBLE2.Visible = false;
                    txtbox2.Visible = false;
                }

                if (strCov_code2 == "ICO")
                {
                    lblNoLimit2.Visible = false;
                    lblNoDeductible2.Visible = false;
                    lblNoaddDEDUCTIBLE2.Visible = false;
                    txtbox2.Visible = false;
                }

                if (strCov_code2 == "FDS")
                {
                    lblNoLimit2.Visible = false;
                    lblNoDeductible2.Visible = false;
                    lblNoaddDEDUCTIBLE2.Visible = false;
                    txtbox2.Visible = false;
                }

                if (strCov_code2 == "TER")
                {
                    lblNoLimit2.Visible = false;
                    lblNoDeductible2.Visible = false;
                    lblNoaddDEDUCTIBLE2.Visible = false;
                    txtbox2.Visible = false;
                }

                if (drvItem2["Limit_1"] != DBNull.Value && drvItem2["COV_ID"].ToString() == "10111")
                {
                    cbDelete2.Checked = true;
                    cbDelete2.Enabled = false;
                }
                else if (drvItem2["Limit_1"] != DBNull.Value && drvItem2["COV_ID"].ToString() == "10113")
                {
                    cbDelete2.Checked = true;
                    cbDelete2.Enabled = false;
                }
                else if (drvItem2["Limit_1"] != DBNull.Value && drvItem2["COV_ID"].ToString() == "10114")
                {
                    cbDelete2.Checked = true;
                    cbDelete2.Enabled = false;
                }
                else if (drvItem2["Limit_1"] != DBNull.Value && drvItem2["COV_ID"].ToString() == "10116")
                {
                    cbDelete2.Checked = true;
                    cbDelete2.Enabled = false;
                }
                else if (drvItem2["Limit_1"] != DBNull.Value && drvItem2["COV_ID"].ToString() == "10115")
                {
                    cbDelete2.Checked = true;
                    cbDelete2.Enabled = false;
                }
                else if (drvItem2["Limit_1"] != DBNull.Value && drvItem2["COV_ID"].ToString() == "10112")
                {
                    cbDelete2.Checked = true;
                }
                else if (drvItem2["Limit_1"] != DBNull.Value && drvItem2["COV_ID"].ToString() == "10112" && cbDelete2.Checked != true)
                {
                    lblLimit2.InnerText = "";
                }
               
                else
                {
                    if (dsCoverages2.Tables[6].Rows.Count == 0)
                    {
                        if (strCov_code2 == "FA")
                        {
                            cbDelete2.Checked = true;
                            cbDelete2.Enabled = false;
                        }

                        if (strCov_code2 == "ED")
                        {
                            cbDelete2.Checked = false;
                            cbDelete2.Enabled = false;
                        }

                        if (strCov_code2 == "EIDP")
                        {
                            cbDelete2.Checked = true;
                            cbDelete2.Enabled = false;
                        }

                        if (strCov_code2 == "EDAL")
                        {
                            cbDelete2.Checked = true;
                            cbDelete2.Enabled = false;
                        }

                        if (strCov_code2 == "ICO")
                        {
                            cbDelete2.Checked = true;
                            cbDelete2.Enabled = false;
                        }

                        if (strCov_code2 == "FDS")
                        {
                            cbDelete2.Checked = true;
                            cbDelete2.Enabled = false;
                        }

                        if (strCov_code2 == "TER")
                        {
                            cbDelete2.Checked = false;
                            cbDelete2.Enabled = false;
                        }
                       
                    }
                   
                }




                switch (strLimitType2)
                {
                    case "1":
                       
                        ddlLimit2.Visible = false;
                        ClsHomeCoverages.BindDropDown(ddlLimit2, dvLimitsRanges2, "LIMIT_1_DISPLAY", "LIMIT_DEDUC_ID", AppEffectiveDate2);
                        ClsCoverages.SelectValueInDropDown(ddlLimit2, DataBinder.Eval(e.Item.DataItem, "LIMIT_ID"));
                        lblNoLimit2.Visible = false;
                        lblLimit2.Visible = false;
                        txtLIMIT2.Visible = false;
                        rfvLIMIT2.Visible = false;
                        rngDWELLING_LIMIT2.Visible = false;
                        revLIMIT2.Visible = false;
                        break;
                    case "3":

                        ddlLimit2.Visible = false;
                        txtLIMIT2.Visible = false;
                       
                        break;
                    default:
                        txtLIMIT2.Visible = false;
                        lblLimit2.Visible = false;
                        ddlLimit2.Visible = false;

                        break;
                }
                i++;
                switch (strDedType2)
                {
                    case "1":
                        //Flat
                        ClsHomeCoverages.BindDropDown(ddlDed2, dvDedRanges2, "LIMIT_1_DISPLAY", "LIMIT_DEDUC_ID", AppEffectiveDate2);
                        ClsCoverages.SelectValueInDropDown(ddlDed2, DataBinder.Eval(e.Item.DataItem, "DEDUC_ID"));
                        lblDEDUCTIBLE_AMOUNT2.Visible = false;
                        lblDeductible2.Visible = false;
                        txtbox2.Visible = false;
                        revValidator2.Visible = false;
                        rngDEDUCTIBLE2.Visible = false;
                        csv2.Visible = false;
                        ddlDed2.Visible = true;
                        break;
                    case "2":
                        ddlLimit2.Visible = false;
                        break;
                    case "3":

                        ddlLimit2.Visible = false;
                        txtaddDEDUCTIBLE2.Visible = true;
                        ddlAddDed2.Visible = false;
                        break;
                    
                    default:
                        lblDEDUCTIBLE_AMOUNT2.Visible = false;
                        lblDeductible2.Visible = false;
                        txtbox2.Visible = false;
                        revValidator2.Visible = false;
                        rngDEDUCTIBLE2.Visible = false;
                        csv2.Visible = false;
                        ddlDed2.Visible = false;
                        break;


                }
                switch (strAddDedType2)
                {
                    case "1":
                        //Flat
                        ClsHomeCoverages.BindDropDown(ddlAddDed2, dvAddDedRanges2, "LIMIT_1_DISPLAY", "LIMIT_DEDUC_ID", DateTime.Now);
                        ClsCoverages.SelectValueInDropDown(ddlAddDed2, DataBinder.Eval(e.Item.DataItem, "ADDDEDUCTIBLE_ID"));
                       
                        ddlAddDed2.Visible = true;
                        lblDEDUCTIBLE_AMOUNT2.Visible = false;
                        txtaddDEDUCTIBLE2.Visible = false;
                        txtbox2.Visible = false;
                        revValidator2.Visible = false;
                        rngDEDUCTIBLE2.Visible = false;
                        lblNoaddDEDUCTIBLE2.Visible = false;
                        csv2.Visible = false;

                        if (strCov_code2 == "FA")
                        {

                            ddlLimit2.Attributes.Add("onchange", "javascript:getddllimitFA();");
                        }

                        break;
                    case "2":
                        //ddlLimit2.Visible = false;
                        break;
                    case "3":

                        //ddlLimit2.Visible = false;
                        ddlAddDed2.Visible = false;
                        txtbox2.Visible = true;

                        break;
                    default:
                        ddlAddDed2.Visible = false;
                        lblDEDUCTIBLE_AMOUNT2.Visible = false;
                        txtaddDEDUCTIBLE2.Visible = false;
                        txtbox2.Visible = false;
                        revValidator2.Visible = false;
                        rngDEDUCTIBLE2.Visible = false;
                        lblNoaddDEDUCTIBLE2.Visible = false;
                        csv2.Visible = false;
                        break;
                }

            }
        }
        #endregion

        private void dgCoverages2_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            OnItemDataBound2(sender, e);

        }

        private void RegisterScript1()
        {
            //if ( this.sbScript.ToString() == "" ) return;

            if (!Page.IsStartupScriptRegistered("Test"))
            {
                //string strCode = @"<script>firstTime = true;" + this.sbScript.ToString() + "</script>";

                //string strDisable = @"<script>function DisableDDL(){" + this.sbDisableScript.ToString() + "} DisableDDL();firstTime = false;</script>";

                //Page.RegisterStartupScript("Test", strCode + strDisable);

            }



        }

        private void PopulateList2(ArrayList alRecr, DataGrid dgCoverages)
        {

            foreach (DataGridItem dgi2 in dgCoverages2.Items)
            {
                if (dgi2.ItemType == ListItemType.Item || dgi2.ItemType == ListItemType.AlternatingItem)
                {
                    CheckBox cbDelete2 = (CheckBox)dgi2.FindControl("cbDelete2");
                    HtmlSelect ddlLimit2 = ((HtmlSelect)dgi2.FindControl("ddlLimit2"));
                    HtmlSelect ddlDeductible2 = ((HtmlSelect)dgi2.FindControl("ddlDeductible2"));
                    TextBox txtbox2 = ((TextBox)dgi2.FindControl("txtadditional"));
                    TextBox txtLIMIT2= ((TextBox)dgi2.FindControl("txtLIMIT2"));
                    //Label lblLIMIT = ((Label)dgi.FindControl("lblLIMIT"));
                    HtmlInputHidden hidLIMIT2 = ((HtmlInputHidden)dgi2.FindControl("hidLIMIT2"));
                    System.Web.UI.HtmlControls.HtmlGenericControl lblLIMIT2 = ((System.Web.UI.HtmlControls.HtmlGenericControl)dgi2.FindControl("lblLIMIT2"));
                    Label lblCOV_DESC2 = ((Label)dgi2.FindControl("lblCOV_DESC2"));
                    Label lblCOV_ID2 = ((Label)dgi2.FindControl("lblCOV_ID2"));

                    ClsLiabilityCoverage objInfo = new ClsLiabilityCoverage();


                    objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
                    objInfo.POLICY_VERSION_ID = Convert.ToInt32(this.hidAppVersionID.Value);
                    objInfo.POLICY_ID = Convert.ToInt32(this.hidAppID.Value);

                    objInfo.MODIFIED_BY = int.Parse(GetUserId());
                    HtmlInputHidden hidDEDUCTIBLE2 = ((HtmlInputHidden)dgi2.FindControl("hidDEDUCTIBLE2"));
                    HtmlInputHidden hidCbDelete2 = (System.Web.UI.HtmlControls.HtmlInputHidden)dgi2.FindControl("hidcbDelete2");

                    HtmlSelect ddlAddDed2 = (HtmlSelect)dgi2.FindControl("ddladdDEDUCTIBLE2");
                    TextBox txtaddDEDUCTIBLE = (TextBox)dgi2.FindControl("txtaddDEDUCTIBLE2");
                    Label lblNoaddDEDUCTIBLE2 = (Label)dgi2.FindControl("lblNoaddDEDUCTIBLE2");
                    Label lblDEDUCTIBLE_AMOUNT2 = ((Label)dgi2.FindControl("lblDEDUCTIBLE_AMOUNT2"));
                    System.Web.UI.HtmlControls.HtmlInputHidden hidlbl_DEDUCTIBLE_AMOUNT2 = (HtmlInputHidden)dgi2.FindControl("hidlbl_DEDUCTIBLE_AMOUNT2");
                    System.Web.UI.HtmlControls.HtmlGenericControl spnDEDUCTIBLE_AMOUNT_TEXT2 = (HtmlGenericControl)dgi2.FindControl("spnDEDUCTIBLE_AMOUNT_TEXT2");
                    hidCbDelete2.Value = cbDelete2.Checked.ToString();




                    objInfo.COVERAGE_ID = int.Parse(lblCOV_ID2.Text);
                    if (objInfo.COVERAGE_ID == -1)
                    {
                        objInfo.ACTION = "I";
                    }
                    else
                    {

                        objInfo.ACTION = "U";
                    }

                    int intWBSPO = Convert.ToInt32(((Label)dgi2.FindControl("lblCOV_ID2")).Text);

                    if (hidCbDelete2.Value == "True")
                    {
                        objInfo.COVERAGE_CODE_ID = Convert.ToInt32(((Label)dgi2.FindControl("lblCOV_ID2")).Text);
                        objInfo.COVERAGE_TYPE = "PL";

                        string strLimitType2 = ((Label)dgi2.FindControl("lblLIMIT_TYPE2")).Text;
                        string strDedType2 = ((Label)dgi2.FindControl("lblDEDUCTIBLE_TYPE2")).Text;
                        string strAddDedType2 = ((Label)dgi2.FindControl("lblAddDEDUCTIBLE_TYPE2")).Text;

                        if (lblLIMIT2.Visible == true)
                        {

                            if (hidLIMIT2.Value.Trim() != "")
                            {

                                objInfo.LIMIT_1 = Convert.ToDouble(hidLIMIT2.Value.Replace("$", "").Replace(",", "").Trim());

                            }

                        }



                        switch (strLimitType2)
                        {
                            case "1":
                                //Flat
                                if (ddlLimit2.Items.Count > 0 && ddlLimit2.Items[ddlLimit2.SelectedIndex].Text.Trim() != "")
                                {

                                    string amount = ddlLimit2.Items[ddlLimit2.SelectedIndex].Text.Substring(0, ddlLimit2.Items[ddlLimit2.SelectedIndex].Text.IndexOf(" "));
                                    string text = ddlLimit2.Items[ddlLimit2.SelectedIndex].Text.Substring(ddlLimit2.Items[ddlLimit2.SelectedIndex].Text.IndexOf(" "));

                                    objInfo.LIMIT_ID = Convert.ToInt32(ddlLimit2.Items[ddlLimit2.SelectedIndex].Value);


                                    if (amount.Trim() != "")
                                    {
                                        objInfo.LIMIT_1 = Convert.ToDouble(amount);
                                    }

                                    objInfo.LIMIT1_AMOUNT_TEXT = text.Trim();

                                }

                                break;
                            case "2":
                                
                               
                                break;
                            case "0":
                                if (((System.Web.UI.HtmlControls.HtmlGenericControl)dgi2.FindControl("lblLIMIT2")).InnerText.Trim() != "")
                                {

                                    if (hidLIMIT2.Value.Trim() != "")
                                    {

                                        objInfo.LIMIT_1 = Convert.ToDouble(hidLIMIT2.Value.Trim().Replace("$", "").Replace(",", ""));
                                    }
                                }
                                break;
                            case "3":

                                if (txtLIMIT2.Visible == true)
                                {
                                    if (txtLIMIT2.Text.Trim() != "")
                                    {
                                        objInfo.LIMIT_1 = Convert.ToDouble(txtLIMIT2.Text.Trim().Replace("$", ""));
                                    }
                                }
                                break;
                        }

                        switch (strDedType2)
                        {
                            case "1":

                                if (ddlDeductible2.Items.Count > 0 && ddlDeductible2.Items[ddlDeductible2.SelectedIndex].Value != "")
                                {
                                    if (ddlDeductible2.Visible == true)
                                    {
                                        objInfo.DEDUC_ID = Convert.ToInt32(ddlDeductible2.Items[ddlDeductible2.SelectedIndex].Value);
                                        objInfo.DEDUCTIBLE_1 = Convert.ToDouble(ddlDeductible2.Items[ddlDeductible2.SelectedIndex].Text);
                                    }
                                }
                                break;
                            case "2":

                                if (lblCOV_ID2.Text == "5007")
                                {
                                    objInfo.LIMIT_1 = Convert.ToDouble(hidBPID.Value);
                                }

                                break;
                            case "0":

                                if (hidDEDUCTIBLE2.Value.ToString().Trim() != "")
                                    objInfo.DEDUCTIBLE1_AMOUNT_TEXT = hidDEDUCTIBLE2.Value.ToString().Trim();
                                break;
                            case "3":




                                if (txtbox2.Text.Trim() != "")

                                    objInfo.DEDUCTIBLE_1 = Convert.ToDouble(txtbox2.Text.Trim().Replace("$", ""));

                                break;
                        }
                        switch (strAddDedType2)
                        {
                            case "1":


                                if (ddlAddDed2.Items.Count > 0 && ddlAddDed2.Items[ddlAddDed2.SelectedIndex].Text.Trim() != "")
                                {
                                    string amount = ddlAddDed2.Items[ddlAddDed2.SelectedIndex].Text.Substring(0, ddlAddDed2.Items[ddlAddDed2.SelectedIndex].Text.IndexOf(" "));
                                    string text = ddlAddDed2.Items[ddlAddDed2.SelectedIndex].Text.Substring(ddlAddDed2.Items[ddlAddDed2.SelectedIndex].Text.IndexOf(" "));




                                    if (amount.Trim() != "")
                                    {
                                        objInfo.MINIMUM_DEDUCTIBLE = Convert.ToDouble(amount);
                                    }


                                }
                                break;
                            case "2":

                                break;
                            case "3":

                                objInfo.MINIMUM_DEDUCTIBLE = Convert.ToDouble(txtbox2.Text.Trim());

                                break;
                            case "4":
                                if (hidlbl_DEDUCTIBLE_AMOUNT2.Value.ToString() != "")
                                {
                                    string[] amountl = new string[2];
                                    amountl = hidlbl_DEDUCTIBLE_AMOUNT2.Value.ToString().Split(' ');
                                    if (objInfo.COVERAGE_CODE_ID == 112 || objInfo.COVERAGE_CODE_ID == 169)
                                    {
                                        objInfo.MINIMUM_DEDUCTIBLE = Convert.ToDouble("2");
                                    }
                                    else
                                    {
                                        if (amountl[0].Trim() != "")
                                        {
                                            objInfo.MINIMUM_DEDUCTIBLE = Convert.ToDouble(amountl[0].ToString());
                                        }
                                    }
                                    if (amountl.Length > 1)
                                    {
                                        //Case added according to ashish mail.
                                        if (objInfo.COVERAGE_CODE_ID == 112 || objInfo.COVERAGE_CODE_ID == 169)
                                        {
                                            
                                        }
                                        else
                                        {
                                           
                                        }
                                    }
                                }
                                break;
                            case "0":
                               
                                break;


                        }

                        alRecr.Add(objInfo);

                    }
                    else
                    {
                       

                        if (dgCoverages.DataKeys[dgi2.ItemIndex] != System.DBNull.Value)
                        {
                            objInfo.ACTION = "D";
                            alRecr.Add(objInfo);
                        }
                    }
                }
            }

        }

        private int Save2()
        {
            ArrayList alRecr = new ArrayList();
            PopulateList2(alRecr, this.dgCoverages2);


            ClsHomeCoverages objCoverages = new ClsHomeCoverages();

            int retVal = 1;

            int customerID = Convert.ToInt32(hidCustomerID.Value);
            int POLICY_ID = Convert.ToInt32(hidAppID.Value);
            int POLICY_VERSION_ID = Convert.ToInt32(hidAppVersionID.Value);


            try
            {

                retVal = objCoverages.SaveBOPGeneralNew(alRecr, hidOldData.Value, "PL", customerID, POLICY_ID, POLICY_VERSION_ID);

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;

                if (ex.InnerException != null)
                {
                    lblMessage.Text = ex.InnerException.Message;

                }
                return -4;
            }

            return retVal;
        }
//**************************************Grid 2 end*******************************

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
            this.dgCoverages.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCoverages_ItemDataBound);
            this.dgCoverages2.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCoverages2_ItemDataBound);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

      

    }
}
