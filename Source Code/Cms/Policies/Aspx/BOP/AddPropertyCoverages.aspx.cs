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
    public partial class AddPropertyCoverages : Cms.Policies.policiesbase
    {
        public string calledFrom = "";
        DataSet dsCoverages = null;
        DataSet deGetData = null;
        XmlDocument xmldoc = new XmlDocument();
        StringBuilder sbScript = new StringBuilder();
        StringBuilder sbDisableScript = new StringBuilder();
        decimal coverageA = 0;
        int product = 0;
        private StringBuilder sbCtrlXML = new StringBuilder();
        public decimal replValue = 0;
        public decimal marketValue = 0;
        private int stateID;
        RuleData homeRuleData;
        protected Cms.CmsWeb.Controls.CmsButton btnDelete;
        //protected System.Web.UI.HtmlControls.HtmlInputHidden hidREP_COST;
        private System.DateTime AppEffectiveDate;

        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidROW_COUNT;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidControlXML;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomInfo;
        private void Page_Load(object sender, System.EventArgs e)
        {
            trError.Visible = false;



            base.ScreenId = "61_6";
            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;
           

            if (!Page.IsPostBack)
            {

                hidCustomerID.Value = GetCustomerID();
                hidAppID.Value = GetPolicyID();
                hidAppVersionID.Value = GetPolicyVersionID();

                getvalues();
               
                hidcbWBSPO.Value = "false";
              

                BindGrid();
            }

        }
        private void getvalues()
        {
            ClsHomeCoverages objHome = new ClsHomeCoverages();

            deGetData = objHome.GetLocatioDetailCoverage(Convert.ToInt32(hidCustomerID.Value),
                Convert.ToInt32(hidAppID.Value),
                Convert.ToInt32(hidAppVersionID.Value));

            if (deGetData.Tables[0].Rows.Count >0)
            {
                hidLOCATION_ID.Value =deGetData.Tables[0].Rows[0]["LOCATION_ID"].ToString();

                hidPREMISES_ID.Value = deGetData.Tables[0].Rows[0]["PREMISES_ID"].ToString();
            }
        }

        private void BindGrid()
        {
            ClsHomeCoverages objHome = new ClsHomeCoverages();


            dsCoverages = objHome.GetBopliabilityCoverage(Convert.ToInt32(hidCustomerID.Value),
                Convert.ToInt32(hidAppID.Value),
                Convert.ToInt32(hidAppVersionID.Value),
                 "RL");

            DataRow[] drCoverageA = dsCoverages.Tables[0].Select("COV_CODE = 'DWELL'");

            if (drCoverageA != null && drCoverageA.Length > 0)
            {
                if (drCoverageA[0]["LIMIT_1"] != DBNull.Value)
                {
                    this.coverageA = Convert.ToInt32(drCoverageA[0]["LIMIT_1"]);
                    decimal perc = 0.10M;

                    decimal tenPerc = coverageA * perc;
                    
                    this.hidCoverageA.Value = Convert.ToInt32(tenPerc).ToString(); ;
                }
            }

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




                DataView dvLimitsRanges = new DataView(dtRanges);
                DataView dvDedRanges = new DataView(dtRanges);
                DataView dvAddDedRanges = new DataView(dtRanges);



                dvLimitsRanges.RowFilter = "COV_ID = " + intCOV_ID.ToString() + " AND LIMIT_DEDUC_TYPE = 'Limit'";
                dvDedRanges.RowFilter = "COV_ID = " + intCOV_ID.ToString() + " AND LIMIT_DEDUC_TYPE = 'Deduct'";
                dvAddDedRanges.RowFilter = "COV_ID = " + intCOV_ID.ToString() + " AND LIMIT_DEDUC_TYPE = 'Addded'";

                DataRow[] drRanges = dtRanges.Select("COV_ID = " + intCOV_ID.ToString());


                if (strCov_code == "AR")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    //ddlLimit.Visible = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }

                if (strCov_code == "VP")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    //ddlLimit.Visible = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }

                if (strCov_code == "BIWEELI")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    //ddlLimit.Visible = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }

                if (strCov_code == "BIWEE")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    //ddlLimit.Visible = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }

                if (strCov_code == "ALM")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    //ddlLimit.Visible = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }

                if (strCov_code == "BICTP")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    //ddlLimit.Visible = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }

                if (strCov_code == "BIDP")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    //ddlLimit.Visible = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }

                if (strCov_code == "DR")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    //ddlLimit.Visible = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }

                if (strCov_code == "OLA")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    //ddlLimit.Visible = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                }

                if (strCov_code == "ORP")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    //ddlLimit.Visible = true;
                    txtbox.Visible = false;
                }

                if (strCov_code == "ED")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    //ddlLimit.Visible = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }

                if (strCov_code == "MAD")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    //ddlLimit.Visible = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }

                if (strCov_code == "MSP")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    //ddlLimit.Visible = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }



                if (strCov_code == "MSOP")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    //ddlLimit.Visible = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }

                if (strCov_code == "ICO")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    //ddlLimit.Visible = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }

                if (strCov_code == "EDF")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    //ddlLimit.Visible = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }

                 if (strCov_code == "EB")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    //ddlLimit.Visible = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }

                 if (strCov_code == "CME")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    //ddlLimit.Visible = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }
                    
                 if (strCov_code == "CMI")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    //ddlLimit.Visible = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }
                  if (strCov_code == "CM")
                {
                    lblNoLimit.Visible = false;
                    cbDelete.Enabled = true;
                    //ddlLimit.Visible = true;
                    lblNoDeductible.Visible = false;
                    lblNoaddDEDUCTIBLE.Visible = false;
                    txtbox.Visible = false;
                }
                  if (strCov_code == "OBL")
                  {
                      lblNoLimit.Visible = false;
                      cbDelete.Enabled = true;
                      //ddlLimit.Visible = true;
                      lblNoDeductible.Visible = false;
                      lblNoaddDEDUCTIBLE.Visible = false;
                      txtbox.Visible = false;
                  }

                  if (strCov_code == "BDC")
                  {
                      lblNoLimit.Visible = false;
                      cbDelete.Enabled = true;
                      //ddlLimit.Visible = true;
                      lblNoDeductible.Visible = false;
                      lblNoaddDEDUCTIBLE.Visible = false;
                      txtbox.Visible = false;
                  }

                  if (strCov_code == "BIC")
                  {
                      lblNoLimit.Visible = false;
                      cbDelete.Enabled = true;
                      //ddlLimit.Visible = true;
                      lblNoDeductible.Visible = false;
                      lblNoaddDEDUCTIBLE.Visible = false;
                      txtbox.Visible = false;
                  }

                  if (strCov_code == "OP")
                  {
                      lblNoLimit.Visible = false;
                      cbDelete.Enabled = true;
                      //ddlLimit.Visible = true;
                      lblNoDeductible.Visible = false;
                      lblNoaddDEDUCTIBLE.Visible = false;
                      txtbox.Visible = false;
                  }

                  if (strCov_code == "IB")
                  {
                      lblNoLimit.Visible = false;
                      cbDelete.Enabled = true;
                      //ddlLimit.Visible = true;
                      lblNoDeductible.Visible = false;
                      lblNoaddDEDUCTIBLE.Visible = false;
                      txtbox.Visible = false;
                  }

                  if (strCov_code == "OS")
                  {
                      lblNoLimit.Visible = false;
                      cbDelete.Enabled = true;
                      //ddlLimit.Visible = true;
                      lblNoDeductible.Visible = false;
                      lblNoaddDEDUCTIBLE.Visible = false;
                      txtbox.Visible = false;
                  }

                  if (strCov_code == "TD")
                  {
                      lblNoLimit.Visible = false;
                      cbDelete.Enabled = true;
                      //ddlLimit.Visible = true;
                      lblNoDeductible.Visible = false;
                      lblNoaddDEDUCTIBLE.Visible = false;
                      txtbox.Visible = false;
                  }

                  if (strCov_code == "AA")
                  {
                      lblNoLimit.Visible = false;
                      cbDelete.Enabled = true;
                      //ddlLimit.Visible = true;
                      lblNoDeductible.Visible = false;
                      lblNoaddDEDUCTIBLE.Visible = false;
                      txtbox.Visible = false;
                  }

                  if (strCov_code == "WH")
                  {
                      lblNoLimit.Visible = false;
                      cbDelete.Enabled = true;
                      //ddlLimit.Visible = true;
                      lblNoDeductible.Visible = false;
                      lblNoaddDEDUCTIBLE.Visible = false;
                      txtbox.Visible = false;
                  }

                  if (strCov_code == "PD")
                  {
                      lblNoLimit.Visible = false;
                      cbDelete.Enabled = true;
                      //ddlLimit.Visible = true;
                      lblNoDeductible.Visible = false;
                      lblNoaddDEDUCTIBLE.Visible = false;
                      txtbox.Visible = false;
                  }

                  if (strCov_code == "OD")
                  {
                      lblNoLimit.Visible = false;
                      cbDelete.Enabled = true;
                      //ddlLimit.Visible = true;
                      lblNoDeductible.Visible = false;
                      lblNoaddDEDUCTIBLE.Visible = false;
                      txtbox.Visible = false;
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
                        break;
                    case "3":
                         txtLIMIT.Visible = true;
                         lblLimit.Visible = false;
                         ddlLimit.Visible = false;
                       



                        break;
                    default:
                        txtLIMIT.Visible = false;
                        lblLimit.Visible = false;
                        ddlLimit.Visible = false;
                       //lblLIMIT .Visible = false;
                        break;
                }

                switch (strDedType)
                {
                    case "1":
                        //Flat
                        ClsHomeCoverages.BindDropDown(ddlDed, dvDedRanges, "LIMIT_1_DISPLAY", "LIMIT_DEDUC_ID", AppEffectiveDate);
                        ClsCoverages.SelectValueInDropDown(ddlDed, DataBinder.Eval(e.Item.DataItem, "DEDUC_ID"));
                        lblDEDUCTIBLE_AMOUNT.Visible = false;
                        //Hide irrelevant Controls 
                        lblDeductible.Visible = false;
                        txtbox.Visible = false;
                        revValidator1.Visible = false;
                        rngDEDUCTIBLE.Visible = false;
                        csv.Visible = false;
                        ddlDed.Visible = true;
                        break;

                    default:
                        lblDEDUCTIBLE_AMOUNT.Visible = false;
                        //Hide irrelevant Controls 
                        lblDeductible.Visible = false;
                        txtbox.Visible = false;
                        revValidator1.Visible = false;
                        rngDEDUCTIBLE.Visible = false;
                        csv.Visible = false;
                        //ddlAddDed.Visible = false;
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
                        // ddlDed.Visible = false;
                        break;
                }
            }
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
            this.dgCoverages.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCoverages_ItemDataBound);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.Load += new System.EventHandler(this.Page_Load);

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



        //		private void btnDelete_Click(object sender, System.EventArgs e)
        //		{
        //			int retVal = Delete();
        //			
        //			lblMessage.Visible = true;
        //
        //			if ( retVal == 2 )
        //			{
        //				BindGrid();
        //				lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","127");
        //				SetWorkFlowControl();
        //			}
        //		}

        //		private int Delete()
        //		{
        //			
        //			ArrayList alDelete = new ArrayList();
        //
        //			foreach(DataGridItem dgi in dgCoverages.Items)
        //			{
        //				if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
        //				{
        //					CheckBox cbDelete = (CheckBox)dgi.FindControl("cbDelete");
        //
        //					if ( cbDelete.Checked )
        //					{
        //						ClsCoveragesInfo objInfo = new ClsCoveragesInfo();
        //
        //						objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
        //						objInfo.APP_VERSION_ID = Convert.ToInt32(this.hidAppVersionID.Value);
        //						objInfo.APP_ID = Convert.ToInt32(this.hidAppID.Value);
        //						objInfo.COVERAGE_ID = Convert.ToInt32(dgCoverages.DataKeys[dgi.ItemIndex]);
        //
        //						alDelete.Add(objInfo);
        //					}
        //
        //				}
        //			}
        //			
        //
        //			
        //			
        //			return 1;
        //			
        //		}

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            lblMessage.Visible = true;

            int retVal = Save();

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

            if (retVal == -2)
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

                    ClsPropertyCoveragesInfo objInfo = new ClsPropertyCoveragesInfo();  

                    objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
                    objInfo.POLICY_VERSION_ID = Convert.ToInt32(this.hidAppVersionID.Value);
                    objInfo.POLICY_ID = Convert.ToInt32(this.hidAppID.Value);
                    objInfo.LOCATION_ID = Convert.ToInt32(this.hidLOCATION_ID.Value);
                    objInfo.PREMISES_ID = Convert.ToInt32(this.hidPREMISES_ID.Value);
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
                        objInfo.COVERAGE_TYPE = "RL";

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
                                    //"100 Excess Medical"
                                    string amount = ddlLimit.Items[ddlLimit.SelectedIndex].Text.Substring(0, ddlLimit.Items[ddlLimit.SelectedIndex].Text.IndexOf(" "));
                                    string text = ddlLimit.Items[ddlLimit.SelectedIndex].Text.Substring(ddlLimit.Items[ddlLimit.SelectedIndex].Text.IndexOf(" "));

                                    objInfo.LIMIT_ID = Convert.ToInt32(ddlLimit.Items[ddlLimit.SelectedIndex].Value);

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
                                //Open

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
                                //Flat
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
                                //Split
                                if (ddlDeductible.Items.Count > 0 && ddlDeductible.Items[ddlDeductible.SelectedIndex].Value != "")
                                {
                                    objInfo.DEDUC_ID = Convert.ToInt32(ddlDeductible.Items[ddlDeductible.SelectedIndex].Value);
                                    string[] strValues = ddlDeductible.Items[ddlDeductible.SelectedIndex].Text.Split('/');
                                    objInfo.DEDUCTIBLE_1 = Convert.ToDouble(strValues[0]);
                                    objInfo.DEDUCTIBLE_2 = Convert.ToDecimal(strValues[1]);
                                }

                                break;
                            case "0":

                                if (hidDEDUCTIBLE.Value.ToString().Trim() != "")
                                    objInfo.DEDUCTIBLE1_AMOUNT_TEXT = hidDEDUCTIBLE.Value.ToString().Trim();
                                break;
                            case "3":
                                //Open



                                if (txtbox.Text.Trim() != "")
                                    //Changed:Itrack # 6093 -13 July 2009 -Manoj Rathore 
                                    objInfo.DEDUCTIBLE_1 = Convert.ToDouble(txtbox.Text.Trim().Replace("$", ""));

                                break;
                        }
                        switch (strAddDedType)
                        {
                            case "1":
                                //Flat

                                if (ddlAddDed.Items.Count > 0 && ddlAddDed.Items[ddlAddDed.SelectedIndex].Text.Trim() != "")
                                {
                                    string amount = ddlAddDed.Items[ddlAddDed.SelectedIndex].Text.Substring(0, ddlAddDed.Items[ddlAddDed.SelectedIndex].Text.IndexOf(" "));
                                    string text = ddlAddDed.Items[ddlAddDed.SelectedIndex].Text.Substring(ddlAddDed.Items[ddlAddDed.SelectedIndex].Text.IndexOf(" "));
                                    //objInfo.ADDDEDUCTIBLE_ID = Convert.ToInt32(ddlAddDed.Items[ddlAddDed.SelectedIndex].Value);



                                    if (amount.Trim() != "")
                                    {
                                      //  objInfo.DEDUCTIBLE = Convert.ToDouble(amount);
                                    }

                                   // objInfo.DEDUCTIBLE_TEXT = text.Trim();
                                }
                                break;
                            case "2":

                                break;
                            case "3":
                                //Open

                                //lblDeductible.Visible = false;
                                objInfo.MINIMUM_DEDUCTIBLE = Convert.ToDouble(txtaddDEDUCTIBLE.Text.Trim());
                                //ddlDed.Visible = false;

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
            int LOCATION_ID = Convert.ToInt32(hidLOCATION_ID.Value);
            int PREMISES_ID = Convert.ToInt32(hidPREMISES_ID.Value);
            try
            {

                retVal = objCoverages.SaveBOPLiabilitylNew(alRecr, hidOldData.Value, "RL", customerID, POLICY_ID, POLICY_VERSION_ID, LOCATION_ID, PREMISES_ID);

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




    }
}
