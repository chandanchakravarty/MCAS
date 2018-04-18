/******************************************************************************************
<Author					: - Shafi
<Start Date				: -	14-02-2006
<End Date				: -	
<Description			: -  Provides a list of vehicles/WaterCraft from which coverages can be copied.
<Review Date			: - 
<Reviewed By			: - 	
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
using Cms.Model.Policy;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using System.Xml;
using System.Text;

namespace Cms.Policies.Aspx.Watercrafts
{
    /// <summary>
    /// Summary description for WatercraftCoverages.
    /// </summary>
    public class PolicyWatercraftCoverages : Cms.Policies.policiesbase
    {
        protected System.Web.UI.WebControls.Label lblTitle;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.DataGrid dgCoverages;
        protected Cms.CmsWeb.Controls.CmsButton btnCopy;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidREC_VEH_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppVersionID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidROW_COUNT;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCoverageXML;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOBState;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidMotorcycleType;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTYPE_OF_WATERCRAFT;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomInfo;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidControlXML;
        protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
        //DataTable dtCoverages = null;
        private StringBuilder sbCtrlXML = new StringBuilder();

        string calledFrom = "";
        string pageFrom = "";
        DataSet dsCoverages = null;
        private int length = 0;

        StringBuilder sbScript = new StringBuilder();
        StringBuilder sbDisableScript = new StringBuilder();
        protected System.Web.UI.HtmlControls.HtmlGenericControl s;
        string strLOBState = "";
        XmlDocument xmldoc = new XmlDocument();
        decimal insuringValue = 0;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTrailer;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolVersionID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTrailerJet;
        protected System.Web.UI.WebControls.Label lblPolicyCaption;
        //protected System.Web.UI.WebControls.DataGrid dgPolicyCoverages;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ROW_COUNT;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidType;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidInsureValue;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIsGrandFather;
        //string agreedValue = "";
        private System.DateTime AppEffectiveDate;
        private int All_Data_Valid;

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

        private void Page_Load(object sender, System.EventArgs e)
        {
            // if called from private passenger automobile, otherwise use if else
            string strCalledFrom = "";
            #region setting screen id
            if (Request.QueryString["CALLEDFROM"] != null && Request.QueryString["CALLEDFROM"].ToString().Trim() != "")
            {
                strCalledFrom = Request.QueryString["CALLEDFROM"].ToString().Trim();

            }

            switch (strCalledFrom.ToUpper())
            {
                //case "PPA" :
                //	base.ScreenId	=	"44_2";
                //	break;
                //case "UMB" :
                //	base.ScreenId	=	"83_3";
                //	break;
                //case "MOT" :
                //	base.ScreenId	=	"48_1";
                //	break;
                case "WAT":
                    switch (GetLOBString())
                    {
                        default:
                            base.ScreenId = "246_2";
                            break;
                        //	case "UMB":
                        //		base.ScreenId	=	"83_3";
                        //		break;
                        case "HOME":
                            base.ScreenId = "251_2";
                            break;
                    }
                    break;
                default:
                    base.ScreenId = "44_2";
                    break;
            }
            #endregion





            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
            //btnReset.CmsButtonClass		=	CmsButtonType.Write;
            //btnReset.PermissionString	=	gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btnCopy.CmsButtonClass = CmsButtonType.Write;
            btnCopy.PermissionString = gstrSecurityXML;


            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************

            if (Request.QueryString["CalledFrom"] != null)
            {
                calledFrom = Request.QueryString["CalledFrom"].ToString();
                //strLobId= Request.QueryString["LOB_ID"].ToString();
            }


            if (Request.QueryString["PageFrom"] != null)
            {
                pageFrom = Request.QueryString["PageFrom"].ToString();

            }



            // Put user code to initialize the page here
            if (!Page.IsPostBack)
            {

                hidCustomerID.Value = GetCustomerID();
                hidPolID.Value = GetPolicyID();
                hidPolVersionID.Value = GetPolicyVersionID();


                this.hidREC_VEH_ID.Value = Request.QueryString["VehicleID"].ToString();

                this.lblMessage.Attributes.Add("style", "display:none");

                //Add Attributes for Copy Coverage Button

                switch (strCalledFrom.ToUpper())
                {
                    case "PPA":
                    case "MOT":
                        btnCopy.Attributes.Add("onclick",
                            "javascript:return OpenPopupWindow('PolicyCopyWatercraftCoverages.aspx?CalledFrom=" + calledFrom + "&VEHICLE_ID=" + this.hidREC_VEH_ID.Value + "')");
                        break;
                    case "UMB":
                        btnCopy.Attributes.Add("onclick",
                            "javascript:return OpenPopupWindow('PolicyCopyWatercraftCoverages.aspx?CalledFrom=" + calledFrom + "&VEHICLE_ID=" + this.hidREC_VEH_ID.Value + "')");
                        break;
                    case "WAT":
                        btnCopy.Attributes.Add("onclick",
                            "javascript:return OpenPopupWindow('PolicyCopyWatercraftCoverages.aspx?CalledFrom=" + calledFrom + "&PageFrom=" + this.pageFrom + "&VEHICLE_ID=" + this.hidREC_VEH_ID.Value + "')");
                        break;
                    default:
                        btnCopy.Attributes.Add("onclick",
                            "javascript:return false");
                        break;
                }



                if (Request.QueryString["PageTitle"] != null)
                {
                    lblTitle.Text = Request.QueryString["PageTitle"].ToString();
                }

                ViewState["CurrentPageIndex"] = 1;

                //LoadXml();

                BindGrid(calledFrom);

                //LoadVehicleDetails();

            }
            SetWorkFlowControl();
        }



        /// <summary>
        /// Loads the vehicle details
        /// </summary>



        /// <summary>
        /// Binds the datagrid to the dataset
        /// </summary>
        /// <param name="calledFrom"></param>
        private void BindGrid(string calledFrom)
        {
            ClsCoverages objHome = new ClsCoverages();

            Cms.BusinessLayer.BlApplication.ClsWatercraftCoverages objWater = new Cms.BusinessLayer.BlApplication.ClsWatercraftCoverages();

            //Get the relevant coverages
            switch (calledFrom.ToUpper())
            {

                case "WAT":
                    switch (pageFrom.ToUpper())
                    {
                        case "WWAT":
                        case "HWAT":
                        case "RWAT":



                            dsCoverages = objWater.GetWatCoveragesForPolicy(Convert.ToInt32(hidCustomerID.Value),
                                Convert.ToInt32(hidPolID.Value),
                                Convert.ToInt32(hidPolVersionID.Value),
                                Convert.ToInt32(hidREC_VEH_ID.Value), "N", pageFrom.ToUpper()
                                );
                            break;
                        case "WWTR":
                        case "HWTR":
                        case "RWTR":
                            /*Cms.BusinessLayer.BlApplication.clsapplication 	objCovInformation3 = new Cms.BusinessLayer.BlApplication.clsapplication();
					
                                dsCoverages=objCovInformation3.GetWatercraftCoverages(Convert.ToInt32(hidCustomerID.Value),
                                    Convert.ToInt32(hidAppID.Value),
                                    Convert.ToInt32(hidAppVersionID.Value),
                                    Convert.ToInt32(hidREC_VEH_ID.Value),"N","WWTR"
                                    );*/
                            Response.Redirect("../../cmsweb/Construction.html");
                            break;
                        case "WWTE":
                        case "HWTE":
                        case "RWTE":
                            /*Cms.BusinessLayer.BlApplication.clsapplication 	objCovInformation3 = new Cms.BusinessLayer.BlApplication.clsapplication();
					
                                dsCoverages=objCovInformation3.GetWatercraftCoverages(Convert.ToInt32(hidCustomerID.Value),
                                    Convert.ToInt32(hidAppID.Value),
                                    Convert.ToInt32(hidAppVersionID.Value),
                                    Convert.ToInt32(hidREC_VEH_ID.Value),"N","WWTR"
                                    );*/
                            Response.Redirect("../../cmsweb/Construction.html");
                            break;
                    }
                    break;
                /*
            case "PPA":
					
                ds = ClsCoverages.GetAppCoverages(Convert.ToInt32(hidCustomerID.Value),
                    Convert.ToInt32(hidAppID.Value),
                    Convert.ToInt32(hidAppVersionID.Value),
                    currentPageIndex,
                    pageSize);	
                break;*/
                default:
                    /*
                    dtCoverages = ClsCoverages.GetCoverages(Convert.ToInt32(hidCustomerID.Value), 
                        Convert.ToInt32(hidPolID.Value ), 
                        Convert.ToInt32(hidPolVersionID.Value));
                    */
                    break;

            }

            //Get the state details

            string lob = base.GetLOBString();
            DataTable dtState = dsCoverages.Tables[2];

            string state = dtState.Rows[0]["STATE_NAME"].ToString();
            strLOBState = lob + state;

            hidLOBState.Value = strLOBState;
            // Get App Effective Date & All_Data_valid
            AppEffectiveDate = (DateTime)dsCoverages.Tables[2].Rows[0]["APP_EFF_DATE"];


            //Commented by Ravindra
            /*
            //Set the Variable If Grandfather is applicable for Actual Cash Value/Agreed value
            DateTime dtGrand=ConvertToDate("01-03-2006");
            if(AppEffectiveDate > dtGrand)
                hidIsGrandFather.Value="1";
            else
                hidIsGrandFather.Value ="0";
            */

            //Ravindra(03-12-2008): If Inception date of policy is before this date then rule for minimum detuctible is not applicable
            //as there can be grandfathered deductible
            //hidIsGrandFather values "1" means rule Applicable and "0" means Rule Not applicable
            DateTime dtGrand = ConvertToDate("01-03-2006");
            DateTime dtInceptionDate = Convert.ToDateTime(dsCoverages.Tables[2].Rows[0]["INCEPTION_DATE"]);
            if (dtInceptionDate >= dtGrand)
                hidIsGrandFather.Value = "1";
            else
                hidIsGrandFather.Value = "0";


            if (dsCoverages.Tables[2].Rows[0]["ALL_DATA_VALID"] != DBNull.Value)
            {
                All_Data_Valid = Convert.ToInt32(dsCoverages.Tables[2].Rows[0]["ALL_DATA_VALID"].ToString());

                if (All_Data_Valid == 2)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("761");
                    this.lblMessage.Attributes.Add("style", "display:inline");
                }
            }
            /////
            ///
            //Display/Hide Signature column based on LOB****
            if (hidLOBState.Value == "PPAIndiana" || hidLOBState.Value == "MOTIndiana" || hidLOBState.Value == "MOTMichigan")
            {
                this.dgCoverages.Columns[4].Visible = true;
            }
            else
            {
                this.dgCoverages.Columns[4].Visible = false;
            }
            //********************************************

            //Get Watercraft info///////////////////////////

            DataTable dtWater = dsCoverages.Tables[3];

            if (dtWater != null)
            {
                if (dtWater.Rows.Count > 0)
                {
                    if (dtWater.Rows[0]["INSURING_VALUE"] != DBNull.Value)
                    {
                        this.insuringValue = Convert.ToDecimal(dtWater.Rows[0]["INSURING_VALUE"]);
                        hidInsureValue.Value = this.insuringValue.ToString();
                    }

                    if (dtWater.Rows[0]["TYPE_OF_WATERCRAFT"] != DBNull.Value)
                    {
                        hidTYPE_OF_WATERCRAFT.Value = Convert.ToString(dtWater.Rows[0]["TYPE_OF_WATERCRAFT"]);
                    }

                    if (dtWater.Rows[0]["TYPE"] != DBNull.Value)
                    {
                        hidType.Value = Convert.ToString(dtWater.Rows[0]["TYPE"]);
                    }
                    if (dtWater.Rows[0]["LENGTH"] != DBNull.Value)
                    {
                        this.length = Convert.ToInt32(dtWater.Rows[0]["LENGTH"]);
                    }
                    hidCustomInfo.Value = ";Boat # = " + dtWater.Rows[0]["BOAT_ID"] + ";Boat Make = " + dtWater.Rows[0]["MAKE"] + ";Boat Model = " + dtWater.Rows[0]["MODEL"];

                }
            }
            /////////////////////////////////////////////////
            ///
            //dgCoverages.DataSource = dsCoverages.Tables[0];
            //dgCoverages.DataBind();

            //this.hidROW_COUNT.Value = 	dgCoverages.Items.Count.ToString();

            DataTable dataTable = dsCoverages.Tables[0];

            //Get The Old XML

            hidOldData.Value = ClsCommon.GetXMLEncoded(dataTable);



            /*
            this.dgPolicyCoverages.Columns[4].Visible = false;  
			


			
            //Get The dataview 
            DataView dvPolicyLevel = new DataView(dsCoverages.Tables[0]);
			
            //Filter the Policy Level Coverages
            string filter = "COVERAGE_TYPE = 'PL'";
            dvPolicyLevel.RowFilter			=   filter.ToString();
			
            //Bind With Policy Level Grid
            this.dgPolicyCoverages.DataSource    =   dvPolicyLevel;
            dgPolicyCoverages.DataBind();

            //Get The Count Of Rows For Policy Level
            hidPOLICY_ROW_COUNT.Value = dgPolicyCoverages.Items.Count.ToString();

            //Get The Datarows Of policy Level Coverages
            DataRow [] drRemove= dsCoverages.Tables[0].Select("COVERAGE_TYPE = 'PL'");

          

            //Remove The rows From Dataset 
            foreach (DataRow dr in drRemove)
            {
                dsCoverages.Tables[0].Rows.Remove(dr);
            }

        */

            //Get The Control Xml
            this.sbCtrlXML.Append("<Root>");
            //Bind The Boat Level Coverages
            dgCoverages.DataSource = dsCoverages.Tables[0];
            dgCoverages.DataBind();
            this.sbCtrlXML.Append("</Root>");
            sbCtrlXML.Replace("\\", "");
            this.hidControlXML.Value = sbCtrlXML.ToString();

            //Get The No. of rows for Boat Level coverage
            this.hidROW_COUNT.Value = dgCoverages.Items.Count.ToString();




            RegisterScript();


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {

            //Adding Style to Alternating Item
            e.Item.Attributes.Add("Class", "midcolora");
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Label lblCOV_ID = (Label)e.Item.FindControl("lblCOV_ID");
                DropDownList ddlSigObt = (DropDownList)e.Item.FindControl("ddlSignatureObtained");

                lblCOV_ID.Attributes.Add("style", "display:none");

                CheckBox cbDelete = (CheckBox)e.Item.FindControl("cbDelete");

                //DropDownList ddlLimit = (DropDownList)e.Item.FindControl("ddlLIMIT");
                //DropDownList ddlDed = (DropDownList)e.Item.FindControl("ddlDEDUCTIBLE");

                HtmlSelect ddlLimit = (HtmlSelect)e.Item.FindControl("ddlLIMIT");
                HtmlSelect ddlDed = (HtmlSelect)e.Item.FindControl("ddlDEDUCTIBLE");

                Label lblDeductible = (Label)e.Item.FindControl("lblDeductible");
                Label lblLimit = (Label)e.Item.FindControl("lblLimit");

                TextBox txtDeductible = (TextBox)e.Item.FindControl("txtDeductible");

                TextBox txtLimit = (TextBox)e.Item.FindControl("txtLimit");

                Label lblLIMIT_TYPE = (Label)e.Item.FindControl("lblLIMIT_TYPE");
                Label lblDED_TYPE = (Label)e.Item.FindControl("lblDEDUCTIBLE_TYPE");
                Label lblLIMIT_APPL = (Label)e.Item.FindControl("lblIS_LIMIT_APPLICABLE");
                Label lblDED_APPL = (Label)e.Item.FindControl("lblIS_DEDUCT_APPLICABLE");
                Label lblLIMIT_AMOUNT = (Label)e.Item.FindControl("lblLIMIT_AMOUNT");
                Label lblDEDUCTIBLE_AMOUNT = (Label)e.Item.FindControl("lblDEDUCTIBLE_AMOUNT");

                lblLIMIT_TYPE.Attributes.Add("style", "display:none");
                lblDED_TYPE.Attributes.Add("style", "display:none");
                lblLIMIT_APPL.Attributes.Add("style", "display:none");
                lblDED_APPL.Attributes.Add("style", "display:none");

                RegularExpressionValidator revLimit = (RegularExpressionValidator)e.Item.FindControl("revLIMIT");
                revLimit.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
                revLimit.ValidationExpression = aRegExpDoublePositiveZero;

                RegularExpressionValidator revDeductible = (RegularExpressionValidator)e.Item.FindControl("revDEDUCTIBLE");
                revDeductible.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
                revDeductible.ValidationExpression = aRegExpDoublePositiveZero;

                //Populate the coverage ranges for each coverage
                DataTable dtRanges = this.dsCoverages.Tables[1];

                DataRowView drvItem = (DataRowView)e.Item.DataItem;

                int intCOV_ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "COV_ID"));
                //Appending Node with previx as value for each Coverage in control XML
                DataGrid dg = (DataGrid)sender;
                string strCov_code = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "COV_CODE"));
                string prefix = "";
                if ((e.Item.ItemIndex + 2) < 10)
                    prefix = dg.ID + "_ctl0" + (e.Item.ItemIndex + 2).ToString();
                else
                    prefix = dg.ID + "_ctl" + (e.Item.ItemIndex + 2).ToString();

                this.sbCtrlXML.Append("<COV_CODE ID=\"" + strCov_code + "\">" + prefix + "</COV_CODE>");



                if (drvItem["IS_MANDATORY"] != System.DBNull.Value)
                {
                    if (drvItem["IS_MANDATORY"].ToString() == "Y" || drvItem["IS_MANDATORY"].ToString() == "1")
                    {
                        cbDelete.Enabled = false;
                    }
                }

                if (drvItem["COVERAGE_ID"] == System.DBNull.Value)
                {



                }
                else
                {
                    cbDelete.Checked = true;



                }

                /*Disable Medical Payment Limit Dropdown In case of Watercraft is attached to home
                  Medical Payment,it will be same as of homeowners always for all type of watercrfats
                   except personal watercraft.
                 */

                if (strCov_code == "MCPAY")
                {
                    if (this.pageFrom == "HWAT")
                    {
                        if (
                            !
                            (this.hidType.Value.Trim() == "JS" ||
                            this.hidTYPE_OF_WATERCRAFT.Value.ToString().Trim() == "11373" ||
                            this.hidTYPE_OF_WATERCRAFT.Value.ToString().Trim() == "11386")
                            )
                            ddlLimit.Attributes.Add("disabled", "true");

                    }
                }
                if (strCov_code == "LCCSL")
                {
                    if (this.pageFrom == "HWAT")
                    {
                        ddlLimit.Attributes.Add("disabled", "true");
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



                //cbDelete.ID = "cbDelete_" + intCOV_ID.ToString();

                DataView dvLimitsRanges = new DataView(dtRanges);
                DataView dvDedRanges = new DataView(dtRanges);

                //dvLimitsRanges = dtRanges.DefaultView;
                //dvDedRanges = dtRanges.DefaultView;

                dvLimitsRanges.RowFilter = "COV_ID = " + intCOV_ID.ToString() + " AND LIMIT_DEDUC_TYPE = 'Limit'";
                dvDedRanges.RowFilter = "COV_ID = " + intCOV_ID.ToString() + " AND LIMIT_DEDUC_TYPE = 'Deduct'";

                //Select the ranges applicable to this Coverage
                DataRow[] drRanges = dtRanges.Select("COV_ID = " + intCOV_ID.ToString());



                ddlLimit.Attributes.Add("COVERAGE_ID", intCOV_ID.ToString());
                //ddlLimit.Attributes.Add("COVERAGE_CODE",strCov_code);

                string strLimitApply = drvItem["IS_LIMIT_APPLICABLE"].ToString();
                string strDedApply = drvItem["IS_DEDUCT_APPLICABLE"].ToString();

                string strLimitType = drvItem["LIMIT_TYPE"].ToString();
                string strDedType = drvItem["DEDUCTIBLE_TYPE"].ToString();

                switch (strLimitType)
                {
                    case "1":
                        //Flat
                        /*ddlLimit.DataTextField = "Limit_1_Display";
                        //ddlLimit.DataValueField = "LIMIT_DEDUC_AMOUNT";
                        ddlLimit.DataSource = dvLimitsRanges;
                        ddlLimit.DataBind();*/

                        ClsWatercraftCoverages.BindDropDown(ddlLimit, dvLimitsRanges, "Limit_1_Display", "LIMIT_DEDUC_ID", AppEffectiveDate);

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

                        //lblLimit.Visible = false;
                        //lblLimit.Attributes.Add("style","display:none");
                        //ddlLimit.Visible = true;

                        if (DataBinder.Eval(e.Item.DataItem, "LIMIT_1_DISPLAY") != System.DBNull.Value
                            )
                        {
                            //string strSplitAmt = DataBinder.Eval(e.Item.DataItem,"LIMIT_1") + "/" + DataBinder.Eval(e.Item.DataItem,"LIMIT_2");
                            //ClsCommon.SelectTextinDDL(ddlLimit,DataBinder.Eval(e.Item.DataItem,"LIMIT_1_DISPLAY"));
                            ClsWatercraftCoverages.SelectValueInDropDown(ddlLimit, DataBinder.Eval(e.Item.DataItem, "LIMIT_ID"));

                        }

                        //ClsCommon.SelectTextinDDL(DataBinder.Eval(e.Item.DataItem,"LIMIT_1"));

                        break;
                    case "2":
                        //Split
                        /*ddlLimit.DataTextField = "SplitAmount";
                        //ddlLimit.DataValueField = "SplitAmount";
                        ddlLimit.DataSource = dvLimitsRanges;
                        ddlLimit.DataBind();
                        //lblLimit.Visible = false;
                        //ddlLimit.Visible = true;*/

                        ClsWatercraftCoverages.BindDropDown(ddlLimit, dvLimitsRanges, "SplitAmount", "LIMIT_DEDUC_ID", AppEffectiveDate);

                        if ((DataBinder.Eval(e.Item.DataItem, "LIMIT_1") != System.DBNull.Value &&
                            DataBinder.Eval(e.Item.DataItem, "LIMIT_2") != System.DBNull.Value) ||
                            DataBinder.Eval(e.Item.DataItem, "LIMIT1_AMOUNT_TEXT") != System.DBNull.Value
                            )
                        {
                            string limit1Amt = "";
                            string limit2Amt = "";

                            if (DataBinder.Eval(e.Item.DataItem, "LIMIT1_AMOUNT_TEXT") != System.DBNull.Value)
                            {
                                if (Convert.ToString(DataBinder.Eval(e.Item.DataItem, "LIMIT1_AMOUNT_TEXT")) != "")
                                {
                                    limit1Amt = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "LIMIT1_AMOUNT_TEXT"));
                                }
                            }

                            if (DataBinder.Eval(e.Item.DataItem, "LIMIT2_AMOUNT_TEXT") != System.DBNull.Value)
                            {
                                if (Convert.ToString(DataBinder.Eval(e.Item.DataItem, "LIMIT2_AMOUNT_TEXT")) != "")
                                {
                                    limit2Amt = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "LIMIT2_AMOUNT_TEXT"));
                                }
                            }

                            string strSplitAmt = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "LIMIT_1")) +
                                " " + limit1Amt +
                                "/" +
                                Convert.ToString(DataBinder.Eval(e.Item.DataItem, "LIMIT_2")) +
                                " " + limit2Amt

                                ;

                            //ClsCommon.SelectTextinDDL(ddlLimit,strSplitAmt);
                            ClsWatercraftCoverages.SelectValueInDropDown(ddlLimit, DataBinder.Eval(e.Item.DataItem, "LIMIT_ID"));
                        }


                        break;
                    case "0":
                    case "3":
                        //Open

                        //Section 1 - Covered Property, Physical Damage - Actual Cash Value
                        //Section 1 - Covered Property, Physical Damage - Agreed Value
                        //Section 1 - Covered Property Damage Jet Ski 17,63,819
                        //Boat Towing and Emergency Service Coverage 764,765,766 BTESC
                        if (intCOV_ID == 817 || intCOV_ID == 11 || intCOV_ID == 59 ||
                            intCOV_ID == 15 || intCOV_ID == 818 || intCOV_ID == 61 ||
                            intCOV_ID == 17 || intCOV_ID == 63 || intCOV_ID == 819 ||
                            intCOV_ID == 764 || intCOV_ID == 765 || intCOV_ID == 766
                            || intCOV_ID == 761 || intCOV_ID == 762 || intCOV_ID == 763
                            )
                        {

                            lblLIMIT_AMOUNT.Visible = true;
                            ddlLimit.Visible = false;
                            txtLimit.Visible = false;

                            if (drvItem["COVERAGE_ID"] == System.DBNull.Value)
                            {
                                lblLIMIT_AMOUNT.Text = this.insuringValue.ToString("#,#");
                            }
                            else
                            {
                                lblLIMIT_AMOUNT.Text = String.Format("{0:,#,###}", DataBinder.Eval(e.Item.DataItem, "LIMIT_1"));
                            }

                            break;

                        }/////////

                        //Watercraft Liability Pollution Coverage (OP 900)
                        if (intCOV_ID == 41 || intCOV_ID == 83 || intCOV_ID == 829)
                        {
                            lblLIMIT_AMOUNT.Visible = true;
                            ddlLimit.Visible = false;
                            txtLimit.Visible = false;

                            if (drvItem["COVERAGE_ID"] == System.DBNull.Value)
                            {
                                lblLIMIT_AMOUNT.Text = "10,000";
                            }
                            else
                            {
                                lblLIMIT_AMOUNT.Text = String.Format("{0:,#,###}", DataBinder.Eval(e.Item.DataItem, "LIMIT_1"));
                            }

                            break;
                        }////////




                        if (DataBinder.Eval(e.Item.DataItem, "LIMIT_1") != System.DBNull.Value)
                        {
                            txtLimit.Text = String.Format("{0:,#,###}", DataBinder.Eval(e.Item.DataItem, "LIMIT_1"));
                            //txtLimit.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem,"LIMIT_1"));
                        }

                        break;
                }

                switch (strDedType)
                {
                    case "1":
                        //Flat

                        //Trailers  35,78,826////////////////////////////////////
                        //Trailer - Jet Ski 81, 39, 827
                        //Show label with appropriate values , either with text or without text
                        if (intCOV_ID == 35 || intCOV_ID == 78 || intCOV_ID == 826 ||
                            intCOV_ID == 81 || intCOV_ID == 39 || intCOV_ID == 827
                            )
                        {
                            lblDEDUCTIBLE_AMOUNT.Visible = true;
                            txtDeductible.Visible = false;
                            ddlDed.Visible = false;

                            //Show only of record exists

                            if (DataBinder.Eval(e.Item.DataItem, "COVERAGE_ID") != DBNull.Value)
                            {
                                if (DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE_1") != System.DBNull.Value
                                    )
                                {

                                    string ded1Amt = "";
                                    string ded1AmtText = "";

                                    ded1Amt = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE_1"));

                                    if (DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE1_AMOUNT_TEXT") != System.DBNull.Value)
                                    {
                                        if (Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE1_AMOUNT_TEXT")) != "")
                                        {
                                            ded1AmtText = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE1_AMOUNT_TEXT"));
                                        }
                                    }

                                    lblDEDUCTIBLE_AMOUNT.Text = ded1Amt + " " + ded1AmtText;
                                    hidTrailer.Value = lblDEDUCTIBLE_AMOUNT.Text;


                                }

                            }
                            break;
                        }
                        //End of Trailers/////////////////////////////	

                        /*ddlDed.DataTextField = "Limit_1_Display";
                        ddlDed.DataSource = dvDedRanges;
                        ddlDed.DataBind();*/

                        ClsWatercraftCoverages.BindDropDown(ddlDed, dvDedRanges, "Limit_1_Display", "LIMIT_DEDUC_ID", AppEffectiveDate);

                        if (DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE_1_DISPLAY") != System.DBNull.Value
                            )
                        {

                            string ded1Amt = "";
                            string ded2Amt = "";

                            if (DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE1_AMOUNT_TEXT") != System.DBNull.Value)
                            {
                                if (Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE1_AMOUNT_TEXT")) != "")
                                {
                                    ded1Amt = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE1_AMOUNT_TEXT"));
                                }
                            }

                            if (DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE2_AMOUNT_TEXT") != System.DBNull.Value)
                            {
                                if (Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE2_AMOUNT_TEXT")) != "")
                                {
                                    ded2Amt = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE2_AMOUNT_TEXT"));
                                }
                            }

                            //string strSplitAmt = DataBinder.Eval(e.Item.DataItem,"LIMIT_1") + "/" + DataBinder.Eval(e.Item.DataItem,"LIMIT_2");
                            //ClsCommon.SelectTextinDDL(ddlDed,DataBinder.Eval(e.Item.DataItem,"DEDUCTIBLE_1_DISPLAY"));
                            ClsWatercraftCoverages.SelectValueInDropDown(ddlDed, DataBinder.Eval(e.Item.DataItem, "DEDUC_ID"));

                        }

                        break;
                    case "2":
                        //Split
                        /*ddlDed.DataTextField = "SplitAmount";
                        //lblDeductible.Visible = false;
                        //ddlDed.Visible = true;
                        //ddlDed.DataValueFiel = "LIMIT_DEDUC_AMOUNT";
                        ddlDed.DataSource = dvDedRanges;
                        ddlDed.DataBind();*/
                        ClsWatercraftCoverages.BindDropDown(ddlDed, dvDedRanges, "SplitAmount", "LIMIT_DEDUC_ID", AppEffectiveDate);

                        if (DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE_1") != System.DBNull.Value &&
                            DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE_2") != System.DBNull.Value
                            )
                        {

                            string ded1Amt = "";
                            string ded2Amt = "";

                            if (DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE1_AMOUNT_TEXT") != System.DBNull.Value)
                            {
                                if (Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE1_AMOUNT_TEXT")) != "")
                                {
                                    ded1Amt = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE1_AMOUNT_TEXT"));
                                }
                            }

                            if (DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE2_AMOUNT_TEXT") != System.DBNull.Value)
                            {
                                if (Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE2_AMOUNT_TEXT")) != "")
                                {
                                    ded2Amt = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE2_AMOUNT_TEXT"));
                                }
                            }

                            string strSplitAmt = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE_1")) + "/" +
                                " " + ded1Amt +
                                Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE_2")) +
                                " " + ded2Amt
                                ;

                            //ClsCommon.SelectTextinDDL(ddlDed,strSplitAmt);
                            ClsWatercraftCoverages.SelectValueInDropDown(ddlDed, DataBinder.Eval(e.Item.DataItem, "DEDUC_ID"));

                        }

                        break;
                    case "0":
                    case "3":
                        //Open

                        //Increase in "Unattached Equipment" And Personal Effects Coverage 26,71,823
                        //Show label with 100
                        if (intCOV_ID == 26 || intCOV_ID == 71 || intCOV_ID == 823)
                        {
                            lblDEDUCTIBLE_AMOUNT.Visible = true;
                            txtDeductible.Visible = false;
                            ddlDed.Visible = false;

                            lblDEDUCTIBLE_AMOUNT.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE_1"));
                            break;
                        }

                        if (intCOV_ID == 761 || intCOV_ID == 762 || intCOV_ID == 763)
                        {
                            lblDEDUCTIBLE_AMOUNT.Visible = true;
                            txtDeductible.Visible = false;
                            ddlDed.Visible = false;
                            string ded1Amt = "";
                            string ded1AmtText = "";

                            ded1Amt = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE_1"));

                            if (DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE1_AMOUNT_TEXT") != System.DBNull.Value)
                            {
                                if (Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE1_AMOUNT_TEXT")) != "")
                                {
                                    ded1AmtText = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE1_AMOUNT_TEXT"));
                                }
                            }

                            lblDEDUCTIBLE_AMOUNT.Text = ded1Amt + " " + ded1AmtText;
                            hidTrailer.Value = lblDEDUCTIBLE_AMOUNT.Text;
                            break;

                        }

                        if (DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE_1") != System.DBNull.Value)
                        {
                            txtDeductible.Text = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DEDUCTIBLE_1"));
                        }
                        break;
                }



                //Enable / disable controls////////////////////////////////////


                string disable = "";
                string disable1 = "";
                string script = "";

                script = "onButtonClick(document.getElementById('" + cbDelete.ClientID + "'))";

                string disableScript = "DisableControls('" + cbDelete.ClientID + "')";

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



                if (sbDisableScript.ToString() == "")
                {
                    sbDisableScript.Append(disableScript);
                }
                else
                {
                    sbDisableScript.Append(";" + disableScript);
                }

                //Add on click attributes

                cbDelete.Attributes.Add("onClick", "javascript:" + script + ";" + disableScript);


                e.Item.Attributes.Add("id", "Row_" + strCov_code);
                e.Item.Attributes.Add("code", strCov_code);

                //////////////////////////////////////////////////////////////

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*private void dgPolicyCoverages_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            //StringBuilder sbScript = new StringBuilder();
			
            OnItemDataBound(sender,e);

			
        }*/
        /// <summary>
        /// Executed for each item of the datgrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        private void dgCoverages_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            //StringBuilder sbScript = new StringBuilder();

            OnItemDataBound(sender, e);


        }


        /// <summary>
        /// Registers javascript code with the page 
        /// </summary>
        private void RegisterScript()
        {
            //if ( this.sbScript.ToString() == "" ) return;

            if (!ClientScript.IsStartupScriptRegistered("Test"))
            {
                string strCode = @"<script>firstTime = true;" + this.sbScript.ToString() + ";firstTime = false;</script>";

                string strDisable = @"<script>function DisableDDL(){" + this.sbDisableScript.ToString() + "} DisableDDL();</script>";

                ClientScript.RegisterStartupScript(this.GetType(),"Test", strDisable + strCode);
                //Page.RegisterStartupScript("Test",strCode);

            }



        }



        /// <summary>
        /// Deletes the Coverages from database
        /// </summary>
        /// <returns></returns>
        private int Delete()
        {

            ArrayList alDelete = new ArrayList();

            foreach (DataGridItem dgi in dgCoverages.Items)
            {
                if (dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem)
                {
                    CheckBox cbDelete = (CheckBox)dgi.FindControl("cbDelete");

                    if (cbDelete.Checked)
                    {
                        ClsPolicyCoveragesInfo objInfo = new ClsPolicyCoveragesInfo();

                        objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
                        objInfo.POLICY_VERSION_ID = Convert.ToInt32(this.hidPolVersionID.Value);
                        objInfo.POLICY_ID = Convert.ToInt32(this.hidPolID.Value);
                        objInfo.COVERAGE_ID = Convert.ToInt32(dgCoverages.DataKeys[dgi.ItemIndex]);

                        alDelete.Add(objInfo);
                    }

                }
            }


            if (alDelete.Count > 0)
            {
                ClsCoverages objCoverages = new ClsCoverages();

                try
                {
                    switch (calledFrom)
                    {
                        case "UMB":
                            ///objCoverages.DeleteUmbrellaCoverages(alDelete);
                            break;
                        default:
                            ///objCoverages.DeleteCoverages(alDelete);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                    return -2;
                }

                return 1;

            }

            return 1;

        }


        /// <summary>
        /// Handles the Delete button event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            lblMessage.Visible = true;

            this.lblMessage.Attributes.Add("style", "display:inline");

            int retVal = Save();

            if (retVal == 1)
            {

                lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "31");
                //LoadXml();
                BindGrid(calledFrom);
                base.OpenEndorsementDetails();
                SetWorkFlowControl();
                return;
            }

            if (retVal == -2)
            {
                lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "332");
                //lblMessage.Text = "One of the Coverage code already exists. Please enter another code.";
                return;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="alRecr"></param>
        /// <param name="alDelete"></param>
        /// <param name="dgCoverages"></param>

        private void PopulateList(ArrayList alRecr, ArrayList alDelete, DataGrid dgCoverages)
        {

            foreach (DataGridItem dgi in dgCoverages.Items)
            {
                if (dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem)
                {
                    //Get the checkbox
                    CheckBox cbDelete = (CheckBox)dgi.FindControl("cbDelete");


                    //DropDownList ddlLimit = ((DropDownList)dgi.FindControl("ddlLimit"));
                    //DropDownList ddlDeductible = ((DropDownList)dgi.FindControl("ddlDeductible"));
                    HtmlSelect ddlLimit = ((HtmlSelect)dgi.FindControl("ddlLimit"));
                    HtmlSelect ddlDeductible = ((HtmlSelect)dgi.FindControl("ddlDeductible"));

                    DropDownList ddlSignatureObtained = ((DropDownList)dgi.FindControl("ddlSignatureObtained"));
                    Label lblLIMIT_AMOUNT = (Label)dgi.FindControl("lblLIMIT_AMOUNT");
                    Label lblDEDUCTIBLE_AMOUNT = (Label)dgi.FindControl("lblDEDUCTIBLE_AMOUNT");

                    TextBox txtLimit = ((TextBox)dgi.FindControl("txtLimit"));
                    TextBox txtDeductible = ((TextBox)dgi.FindControl("txtDEDUCTIBLE"));
                    HtmlInputHidden hidLIMIT_SEL_INDEX = (HtmlInputHidden)dgi.FindControl("hidLIMIT_SEL_INDEX");

                    Label lblCOV_DESC = ((Label)dgi.FindControl("lblCOV_DESC"));
                    Label lblCOV_CODE = ((Label)dgi.FindControl("lblCOV_CODE"));
                    string strCov_code = lblCOV_CODE.Text;
                    Label lblCOV_ID = ((Label)dgi.FindControl("lblCOV_ID"));
                    ///					ClsCoveragesInfo objInfo = new ClsCoveragesInfo();	

                    ClsPolicyCoveragesInfo objInfo = new ClsPolicyCoveragesInfo();
                    objInfo.CUSTOMER_ID = Convert.ToInt32(this.hidCustomerID.Value);
                    objInfo.POLICY_VERSION_ID = Convert.ToInt32(this.hidPolVersionID.Value);
                    objInfo.POLICY_ID = Convert.ToInt32(this.hidPolID.Value);
                    objInfo.RISK_ID = Convert.ToInt32(this.hidREC_VEH_ID.Value);
                    objInfo.COV_DESC = lblCOV_DESC.Text.Trim();

                    objInfo.SIGNATURE_OBTAINED = ddlSignatureObtained.SelectedItem.Value;

                    //int.Parse(GetUserId());
                    objInfo.CREATED_BY = int.Parse(GetUserId());
                    objInfo.MODIFIED_BY = int.Parse(GetUserId());

                    if (dgCoverages.DataKeys[dgi.ItemIndex] != System.DBNull.Value)
                    {
                        objInfo.COVERAGE_ID = Convert.ToInt32(dgCoverages.DataKeys[dgi.ItemIndex]);
                    }

                    int row = dgi.ItemIndex + 2;

                    //HtmlGenericControl hidCbDelete= (HtmlGenericControl)dgi.FindControl("hidcbDelete");

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

                        /*
                        if ( lblCOV_CODE.Text == "EBPPDAV" || lblCOV_CODE.Text == "EBPPDACV" 
                            || lblCOV_CODE.Text == "EBPPDJ"
                            )
                        {
                            ////agreedValue
                        }*/


                        objInfo.COVERAGE_CODE_ID = Convert.ToInt32(
                            ((Label)dgi.FindControl("lblCOV_ID")).Text
                            );

                        string strLimitType = ((Label)dgi.FindControl("lblLIMIT_TYPE")).Text;
                        string strDedType = ((Label)dgi.FindControl("lblDEDUCTIBLE_TYPE")).Text;

                        switch (strLimitType)
                        {
                            case "1":
                                //Flat
                                if (ddlLimit.Items.Count > 0 && ddlLimit.Items[ddlLimit.SelectedIndex].Text.Trim() != "")
                                {
                                    //"100 Excess Medical"
                                    int SelectedIndex = 0;
                                    if (strCov_code == "LCCSL")
                                    {
                                        SelectedIndex = Convert.ToInt32(hidLIMIT_SEL_INDEX.Value);
                                    }
                                    else
                                        SelectedIndex = ddlLimit.SelectedIndex;

                                    //string amount = ddlLimit.Items[ddlLimit.SelectedIndex].Text.Substring(0,ddlLimit.Items[ddlLimit.SelectedIndex].Text.IndexOf(" "));
                                    //string text = ddlLimit.Items[ddlLimit.SelectedIndex].Text.Substring(ddlLimit.Items[ddlLimit.SelectedIndex].Text.IndexOf(" "));
                                    string amount = ddlLimit.Items[SelectedIndex].Text.Substring(0, ddlLimit.Items[SelectedIndex].Text.IndexOf(" "));
                                    string text = ddlLimit.Items[SelectedIndex].Text.Substring(ddlLimit.Items[SelectedIndex].Text.IndexOf(" "));

                                    objInfo.LIMIT_ID = Convert.ToInt32(ddlLimit.Items[SelectedIndex].Value);
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
                                    //
                                    int SelectedIndex = 0;
                                    if (this.pageFrom == "HWAT")
                                    {
                                        if (strCov_code == "MCPAY")
                                        {
                                            if (
                                                !
                                                (this.hidType.Value.Trim() == "JS" ||
                                                this.hidTYPE_OF_WATERCRAFT.Value.ToString().Trim() == "11373" ||
                                                this.hidTYPE_OF_WATERCRAFT.Value.ToString().Trim() == "11386")
                                                )
                                                SelectedIndex = Convert.ToInt32(hidLIMIT_SEL_INDEX.Value);
                                            else
                                                SelectedIndex = ddlLimit.SelectedIndex;
                                        }
                                    }
                                    else
                                    {
                                        if (strCov_code == "MCPAY")
                                        {
                                            SelectedIndex = Convert.ToInt32(hidLIMIT_SEL_INDEX.Value);
                                        }
                                        else
                                            SelectedIndex = ddlLimit.SelectedIndex;
                                    }
                                    //
                                    //string[] strValues = ddlLimit.Items[ddlLimit.SelectedIndex].Text.Split('/');
                                    //objInfo.LIMIT_ID = Convert.ToInt32(ddlLimit.Items[ddlLimit.SelectedIndex].Value);
                                    string[] strValues = ddlLimit.Items[SelectedIndex].Text.Split('/');
                                    objInfo.LIMIT_ID = Convert.ToInt32(ddlLimit.Items[SelectedIndex].Value);
                                    if (strValues.Length > 1)
                                    {
                                        string[] strLimits1 = strValues[0].Split(' ');
                                        string[] strLimits2 = strValues[1].Split(' ');

                                        if (strLimits1.Length > 0)
                                        {
                                            if (strLimits1[0].Trim() != "")
                                            {
                                                objInfo.LIMIT_1 = Convert.ToDouble(strLimits1[0]);
                                            }
                                        }

                                        if (strLimits1.Length > 1)
                                        {
                                            objInfo.LIMIT1_AMOUNT_TEXT = strLimits1[1];
                                        }

                                        if (strLimits2[0].Length > 0)
                                        {
                                            if (strLimits2[0].Trim() != "")
                                            {
                                                objInfo.LIMIT_2 = Convert.ToDouble(strLimits2[0]);
                                            }
                                        }

                                        if (strLimits2[0].Length > 1)
                                        {
                                            objInfo.LIMIT2_AMOUNT_TEXT = strLimits2[1];
                                        }
                                    }
                                    else
                                    {
                                        if (strValues.Length >= 1)
                                            objInfo.LIMIT1_AMOUNT_TEXT = strValues[0].Trim();
                                    }
                                }

                                break;
                            case "0":
                            case "3":
                                //Open
                                if (txtLimit.Visible == true)
                                {
                                    if (txtLimit.Text.Trim() != "")
                                    {
                                        objInfo.LIMIT_1 = Convert.ToDouble(txtLimit.Text.Trim());
                                    }
                                }

                                if (lblLIMIT_AMOUNT.Visible == true)
                                {
                                    if (lblLIMIT_AMOUNT.Text.Trim() != "")
                                    {
                                        objInfo.LIMIT_1 = Convert.ToDouble(lblLIMIT_AMOUNT.Text.Trim());
                                    }
                                }

                                break;
                        }

                        switch (strDedType)
                        {
                            case "1":
                                //Flat

                                if (ddlDeductible.Visible == true)
                                {
                                    if (ddlDeductible.Items.Count > 0 && ddlDeductible.Items[ddlDeductible.SelectedIndex].Value != "")
                                    {
                                        string[] strArr = ddlDeductible.Items[ddlDeductible.SelectedIndex].Text.Split(' ');
                                        objInfo.DEDUC_ID = Convert.ToInt32(ddlDeductible.Items[ddlDeductible.SelectedIndex].Value);
                                        objInfo.DEDUCTIBLE_1 = Convert.ToDouble(strArr[0]);
                                        objInfo.DEDUCTIBLE1_AMOUNT_TEXT = strArr[1];

                                        //objInfo.DEDUCTIBLE_1 = Convert.ToDouble(ddlDeductible.SelectedItem.Text);
                                    }
                                }

                                if (lblDEDUCTIBLE_AMOUNT.Visible == true)
                                {
                                    /*
                                     * 81		EBSMETJ	Trailer - Jet Ski
                                        39		EBSMETJ	Trailer - Jet Ski
                                        827		EBSMETJ	Trailer - Jet Ski
                                        826		EBSMT	Trailers
                                        35		EBSMT	Trailers
                                        78		EBSMT	Trailers

                                     */
                                    if (hidTrailer.Value != "")
                                    {
                                        //string[] strArr = lblDEDUCTIBLE_AMOUNT.Text.Split(' ');

                                        string[] strArr = hidTrailer.Value.Split(' ');

                                        if (strArr.Length > 0)
                                        {
                                            if (strArr[0].Trim() != "")
                                            {
                                                objInfo.DEDUCTIBLE_1 = Convert.ToDouble(strArr[0]);
                                            }
                                        }

                                        if (strArr.Length > 1)
                                            objInfo.DEDUCTIBLE1_AMOUNT_TEXT = strArr[1];

                                        //objInfo.DEDUCTIBLE_1 = Convert.ToDouble(ddlDeductible.SelectedItem.Text);
                                    }
                                    if (this.hidTrailerJet.Value != "")
                                    {
                                        //string[] strArr = lblDEDUCTIBLE_AMOUNT.Text.Split(' ')

                                        objInfo.DEDUCTIBLE_1 = Convert.ToDouble(hidTrailerJet.Value);

                                        //objInfo.DEDUCTIBLE_1 = Convert.ToDouble(ddlDeductible.SelectedItem.Text);
                                    }
                                }

                                break;
                            case "2":
                                //Split
                                if (ddlDeductible.Items.Count > 0 && ddlDeductible.Items[ddlDeductible.SelectedIndex].Value != "")
                                {
                                    string[] strValues = ddlDeductible.Items[ddlDeductible.SelectedIndex].Text.Split('/');

                                    objInfo.DEDUC_ID = Convert.ToInt32(ddlDeductible.Items[ddlDeductible.SelectedIndex].Value);
                                    string[] strDed1 = strValues[0].Split(' ');
                                    string[] strDed2 = strValues[1].Split(' ');

                                    if (strDed1.Length > 0)
                                    {
                                        if (strDed1[0].Trim() != "")
                                        {
                                            objInfo.DEDUCTIBLE_1 = Convert.ToDouble(strDed1[0]);
                                        }
                                    }

                                    if (strDed1.Length > 1)
                                    {
                                        objInfo.DEDUCTIBLE1_AMOUNT_TEXT = strDed1[1];
                                    }

                                    if (strDed2.Length > 0)
                                    {
                                        if (strDed2[0].Trim() != "")
                                        {
                                            objInfo.DEDUCTIBLE_2 = Convert.ToDouble(strDed2[0]);
                                        }
                                    }

                                    if (strDed2.Length > 1)
                                    {
                                        objInfo.DEDUCTIBLE2_AMOUNT_TEXT = strDed2[1];
                                    }
                                }

                                break;
                            case "0":
                            case "3":
                                //Open

                                if (txtDeductible.Visible == true)
                                {
                                    if (txtDeductible.Text.Trim() != "")
                                    {
                                        objInfo.DEDUCTIBLE_1 = Convert.ToDouble(txtDeductible.Text.Trim());
                                    }
                                }

                                if (objInfo.COVERAGE_CODE_ID == 761 || objInfo.COVERAGE_CODE_ID == 762 || objInfo.COVERAGE_CODE_ID == 763)
                                {

                                    if (lblDEDUCTIBLE_AMOUNT.Visible == true)
                                    {
                                        if (hidTrailer.Value != "")
                                        {
                                            //string[] strArr = lblDEDUCTIBLE_AMOUNT.Text.Split(' ');

                                            string[] strArr = hidTrailer.Value.Split(' ');

                                            if (strArr.Length > 0)
                                            {
                                                if (strArr[0] != "")
                                                {
                                                    objInfo.DEDUCTIBLE_1 = Convert.ToDouble(strArr[0]);
                                                }
                                            }

                                            //Check For JetSki 
                                            if (strArr.Length > 1)
                                                objInfo.DEDUCTIBLE1_AMOUNT_TEXT = strArr[1];
                                            //objInfo.DEDUCTIBLE_1 = Convert.ToDouble(ddlDeductible.SelectedItem.Text);
                                        }
                                    }

                                }

                                else
                                {

                                    if (lblDEDUCTIBLE_AMOUNT.Visible == true)
                                    {
                                        if (lblDEDUCTIBLE_AMOUNT.Text.Trim() != "")
                                        {
                                            objInfo.DEDUCTIBLE_1 = Convert.ToDouble(lblDEDUCTIBLE_AMOUNT.Text.Trim());
                                        }
                                    }

                                }


                                break;
                        }

                        //INSERT 
                        if (objInfo.COVERAGE_ID == -1)
                        {
                            objInfo.ACTION = "I";
                        }
                        else
                        {
                            //UPDATE
                            objInfo.ACTION = "U";
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
                                objInfo.RISK_ID = Convert.ToInt32(this.hidREC_VEH_ID.Value);
                                objInfo.COVERAGE_ID = Convert.ToInt32(dgCoverages.DataKeys[dgi.ItemIndex]);
                                Label lblCOV_DES = ((Label)dgi.FindControl("lblCOV_DESC"));
                                objInfo.COV_DESC = lblCOV_DES.Text;

                                //DELETE
                                objInfo.ACTION = "D";

                                alRecr.Add(objInfo);

                                alDelete.Add(objInfo);
                            }


                        }
                    }

                }

            }
        }

        /// <summary>
        /// Saves the Coverages
        /// </summary>
        /// <returns></returns>
        private int Save()
        {
            try
            {
                ArrayList alRecr = new ArrayList();
                ArrayList alDelete = new ArrayList();
                //PopulateList(alRecr,alDelete, this.dgPolicyCoverages);
                PopulateList(alRecr, alDelete, this.dgCoverages);


                ClsWatercraftCoverages objCoverages = new ClsWatercraftCoverages();


                int retVal = 1;


                //Get the relevant coverages
                switch (calledFrom.ToUpper())
                {
                    case "WAT":
                        switch (pageFrom.ToUpper())
                        {
                            case "RWAT":
                            case "HWAT":
                            case "WWAT":
                                retVal = objCoverages.SaveWatercraftPolicyCoverages(alRecr, hidOldData.Value, hidCustomInfo.Value);
                                break;
                        }
                        break;
                }

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




        /// <summary>
        /// Sets the workflow properties
        /// </summary>
        private void SetWorkFlowControl()
        {
            if (base.ScreenId == "246_2" || base.ScreenId == "48_1" || base.ScreenId == "72_2"
                || base.ScreenId == "83_3" || base.ScreenId == "83_2" || base.ScreenId == "148_3" || base.ScreenId == "251_2")// ||  base.ScreenId == "83_1")
            {
                myWorkFlow.IsTop = false;
                myWorkFlow.ScreenID = base.ScreenId;
                myWorkFlow.AddKeyValue("CUSTOMER_ID", GetCustomerID());
                myWorkFlow.AddKeyValue("POLICY_ID", GetPolicyID());
                myWorkFlow.AddKeyValue("POLICY_VERSION_ID", GetPolicyVersionID());


                //Setting other optional keys
                SetOtherWorkflowKEys();
                myWorkFlow.GetScreenStatus();
                myWorkFlow.SetScreenStatus();
            }
        }


        private void btnReset_Click(object sender, System.EventArgs e)
        {
            BindGrid(calledFrom);
        }

        /// <summary>
        /// Sets different keys of workflow other then customer, app id and veriosn id
        /// </summary>
        private void SetOtherWorkflowKEys()
        {
            switch (base.ScreenId)
            {
                case "48_1":	//For motor cycle
                case "44_2":	//For automobile				
                    if (hidREC_VEH_ID.Value != "" && hidREC_VEH_ID.Value != "0")
                    {
                        myWorkFlow.AddKeyValue("VEHICLE_ID", hidREC_VEH_ID.Value);
                    }
                    break;
                case "148_3":	//For Homeowner-watercraft				
                case "246_2":	//For Watercraft
                case "83_3":	//For umbrella
                    if (hidREC_VEH_ID.Value != "" && hidREC_VEH_ID.Value != "0")
                    {
                        myWorkFlow.AddKeyValue("BOAT_ID", hidREC_VEH_ID.Value);
                    }
                    break;


            }
        }

    }

}

