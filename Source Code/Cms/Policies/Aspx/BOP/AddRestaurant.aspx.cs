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
using Cms.Model.Maintenance.Claims;
using Cms.Model.Maintenance.Accumulation;
using Cms.Model.Policy;
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BLClaims;
using Cms.ExceptionPublisher.ExceptionManagement;
using System.Xml;
using Cms.Blcommon;
using Cms.BusinessLayer.BlCommon.Accumulation;
using Cms.BusinessLayer.BlCommon;
namespace Cms.Policies.Aspx.BOP
{
    public partial class AddRestaurant : Cms.Policies.policiesbase
    {

        #region Page controls declaration
       
        private string XmlSchemaFileName = "";
        private string XmlFullFilePath = "";
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidRESTAURANT_ID;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            NfiBaseCurrency.NumberDecimalDigits = 2;
            txtTOT_EXPNS_FOOD_LIQUOR.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
            txtTOT_EXPNS_OTHERS.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
            txtNET_PROFIT.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
            txtACCNT_PAYABLE.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
            txtNOTES_PAYABLE.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
            txtBNK_LOANS_PAYABLE.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");                    
                        
            base.ScreenId = "134_0";
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            btndelete.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Delete;
            btndelete.PermissionString = gstrSecurityXML;
            string _queryValue = (string)Request.QueryString["encQR"];

            string strSysID = GetSystemId();
            if (strSysID == "ALBAUAT")
                strSysID = "ALBA";

            XmlSchemaFileName = "AddSuppFormRestaurant.xml";
            XmlFullFilePath = Request.PhysicalApplicationPath + "Policies/support/PageXML/" + strSysID + "/" + XmlSchemaFileName;
            btnReset.Attributes.Add("onclick", "javascript:return ResetForm();");
            if (!Page.IsPostBack)
            {                

                //BI common object
                if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "Policies/support/PageXML/" + GetSystemId(), "AddSuppFormRestaurant.xml"))
                {
                    setPageControls(Page, @Request.PhysicalApplicationPath + "/Policies/support/PageXML/" + GetSystemId() + "/AddSuppFormRestaurant.xml");
                }

                ClsSuppFormsInformation objPremises = new ClsSuppFormsInformation();
                DataSet dsPremises = objPremises.GetRestaurantInformation(GetCustomerID(), GetPolicyID(), GetPolicyVersionID(), "1", "1");
                //DataSet dsPremises = objPremises.GetPremisesInformation(GetCustomerID(), "22", GetPolicyVersionID(), "22");
                //DataSet dsBusiness = objPremises.g(GetCustomerID(), GetPolicyID(), GetPolicyVersionID());
                //objPremises.G
                if (dsPremises.Tables[0].Rows.Count > 0)
                {
                    hidRESTAURANT_ID.Value = dsPremises.Tables[0].Rows[0]["RESTAURANT_ID"].ToString();
                    LoadData(dsPremises.Tables[0]);
                }
                else
                {
                    hidRESTAURANT_ID.Value = "NEW";
                }

            }
        }
        private void LoadData(DataTable dtShop)
        {
            //txtPERCENT_SALES.Text = dtShop.Rows[0]["PERCENT_SALES"] == DBNull.Value ? "" : dtShop.Rows[0]["PERCENT_SALES"].ToString();

            //cmbIS_ENTERTNMT.SelectedValue = dtShop.Rows[0]["IS_ENTERTNMT"] == DBNull.Value ? "" : dtShop.Rows[0]["IS_ENTERTNMT"].ToString();
            //cmbBLDG_TYPE_COOKNG.SelectedValue = dtShop.Rows[0]["BLDG_TYPE_COOKNG"] == DBNull.Value ? "" : dtShop.Rows[0]["BLDG_TYPE_COOKNG"].ToString();
            //cmbBBQ_PIT.SelectedValue = dtShop.Rows[0]["BBQ_PIT"] == DBNull.Value ? "" : dtShop.Rows[0]["BBQ_PIT"].ToString();

            //cmbSEPARATE_BAR.SelectedValue = dtShop.Rows[0]["SEPARATE_BAR"] == DBNull.Value ? "" : dtShop.Rows[0]["SEPARATE_BAR"].ToString();


            //cmbIS_INSURED.SelectedValue = dtShop.Rows[0]["IS_INSURED"] == DBNull.Value ? "" : dtShop.Rows[0]["IS_INSURED"].ToString();
            //cmbDUCT_CLND_PST_SIX_MONTHS.SelectedValue = dtShop.Rows[0]["DUCT_CLND_PST_SIX_MONTHS"] == DBNull.Value ? "" : dtShop.Rows[0]["DUCT_CLND_PST_SIX_MONTHS"].ToString();

            //cmbSUPPR_SYS.SelectedValue = dtShop.Rows[0]["SUPPR_SYS"] == DBNull.Value ? "" : dtShop.Rows[0]["SUPPR_SYS"].ToString();
            //cmbDUCT_SYS.SelectedValue = dtShop.Rows[0]["DUCT_SYS"] == DBNull.Value ? "" : dtShop.Rows[0]["DUCT_SYS"].ToString();

            //cmbNUM_GRILLS.SelectedValue = dtShop.Rows[0]["NUM_GRILLS"] == DBNull.Value ? "" : dtShop.Rows[0]["NUM_GRILLS"].ToString();
            //cmbNUM_FRYERS.SelectedValue = dtShop.Rows[0]["NUM_FRYERS"] == DBNull.Value ? "" : dtShop.Rows[0]["NUM_FRYERS"].ToString();

            //cmbFLAME_COOKING.SelectedValue = dtShop.Rows[0]["FLAME_COOKING"] == DBNull.Value ? "" : dtShop.Rows[0]["FLAME_COOKING"].ToString();
           

            //txtPERCENT_OCUP.Text = dtShop.Rows[0]["PERCENT_OCUP"] == DBNull.Value ? "" : dtShop.Rows[0]["PERCENT_OCUP"].ToString();
            //txtUNITS.Text = dtShop.Rows[0]["UNITS"] == DBNull.Value ? "" : dtShop.Rows[0]["UNITS"].ToString();
            chkBUS_TYP_RESTURANT.Checked = dtShop.Rows[0]["BUS_TYP_RESTURANT"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["BUS_TYP_RESTURANT"].ToString());
            chkBUS_TYP_FM_STYLE.Checked = dtShop.Rows[0]["BUS_TYP_FM_STYLE"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["BUS_TYP_FM_STYLE"].ToString());
            chkBUS_TYP_NGHT_CLUB.Checked = dtShop.Rows[0]["BUS_TYP_NGHT_CLUB"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["BUS_TYP_NGHT_CLUB"].ToString());
            chkBUS_TYP_FRNCHSED.Checked = dtShop.Rows[0]["BUS_TYP_FRNCHSED"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["BUS_TYP_FRNCHSED"].ToString());
            chkBUS_TYP_NT_FRNCHSED.Checked = dtShop.Rows[0]["BUS_TYP_NT_FRNCHSED"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["BUS_TYP_NT_FRNCHSED"].ToString());
            chkBUS_TYP_SEASONAL.Checked = dtShop.Rows[0]["BUS_TYP_SEASONAL"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["BUS_TYP_SEASONAL"].ToString());
            chkBUS_TYP_YR_ROUND.Checked = dtShop.Rows[0]["BUS_TYP_YR_ROUND"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["BUS_TYP_YR_ROUND"].ToString());
            chkBUS_TYP_DINNER.Checked = dtShop.Rows[0]["BUS_TYP_DINNER"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["BUS_TYP_DINNER"].ToString());
            chkBUS_TYP_BNQT_HALL.Checked = dtShop.Rows[0]["BUS_TYP_BNQT_HALL"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["BUS_TYP_BNQT_HALL"].ToString());
            chkBUS_TYP_BREKFAST.Checked = dtShop.Rows[0]["BUS_TYP_BREKFAST"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["BUS_TYP_BREKFAST"].ToString());
            chkBUS_TYP_FST_FOOD.Checked = dtShop.Rows[0]["BUS_TYP_FST_FOOD"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["BUS_TYP_FST_FOOD"].ToString());
            chkBUS_TYP_TAVERN.Checked = dtShop.Rows[0]["BUS_TYP_TAVERN"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["BUS_TYP_TAVERN"].ToString());
            chkBUS_TYP_OTHER.Checked = dtShop.Rows[0]["BUS_TYP_OTHER"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["BUS_TYP_OTHER"].ToString());


            chkSTAIRWAYS.Checked = dtShop.Rows[0]["STAIRWAYS"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["STAIRWAYS"].ToString());
            chkELEVATORS.Checked = dtShop.Rows[0]["ELEVATORS"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["ELEVATORS"].ToString());
            chkESCALATORS.Checked = dtShop.Rows[0]["ESCALATORS"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["ESCALATORS"].ToString());
            chkGRILLING.Checked = dtShop.Rows[0]["GRILLING"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["GRILLING"].ToString());
            chkFRYING.Checked = dtShop.Rows[0]["FRYING"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["FRYING"].ToString());
            chkBROILING.Checked = dtShop.Rows[0]["BROILING"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["BROILING"].ToString());
            chkROASTING.Checked = dtShop.Rows[0]["ROASTING"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["ROASTING"].ToString());
            chkCOOKING.Checked = dtShop.Rows[0]["COOKING"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["COOKING"].ToString());
            chkPRK_TYP_VALET.Checked = dtShop.Rows[0]["PRK_TYP_VALET"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["PRK_TYP_VALET"].ToString());
            chkPRK_TYP_PREMISES.Checked = dtShop.Rows[0]["PRK_TYP_PREMISES"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["PRK_TYP_PREMISES"].ToString());
            chkOPR_ON_PREMISES.Checked = dtShop.Rows[0]["OPR_ON_PREMISES"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["OPR_ON_PREMISES"].ToString());
            chkOPR_OFF_PREMISES.Checked = dtShop.Rows[0]["OPR_OFF_PREMISES"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["OPR_OFF_PREMISES"].ToString());
            chkEMRG_LIGHTS.Checked = dtShop.Rows[0]["EMRG_LIGHTS"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["EMRG_LIGHTS"].ToString());


            chkWOOD_STOVE.Checked = dtShop.Rows[0]["WOOD_STOVE"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["WOOD_STOVE"].ToString());
            chkHIST_MARKER.Checked = dtShop.Rows[0]["HIST_MARKER"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["HIST_MARKER"].ToString());
            chkEXTNG_SYS_COV_COOKNG.Checked = dtShop.Rows[0]["EXTNG_SYS_COV_COOKNG"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["EXTNG_SYS_COV_COOKNG"].ToString());
            chkEXTNG_SYS_MNT_CNTRCT.Checked = dtShop.Rows[0]["EXTNG_SYS_MNT_CNTRCT"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["EXTNG_SYS_MNT_CNTRCT"].ToString());
            chkGAS_OFF_COOKNG.Checked = dtShop.Rows[0]["GAS_OFF_COOKNG"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["GAS_OFF_COOKNG"].ToString());
            chkHOOD_FILTER_CLND.Checked = dtShop.Rows[0]["HOOD_FILTER_CLND"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["HOOD_FILTER_CLND"].ToString());
            chkHOOD_DUCTS_EQUIP.Checked = dtShop.Rows[0]["HOOD_DUCTS_EQUIP"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["HOOD_DUCTS_EQUIP"].ToString());
            chkHOOD_DUCTS_MNT_SCH.Checked = dtShop.Rows[0]["HOOD_DUCTS_MNT_SCH"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["HOOD_DUCTS_MNT_SCH"].ToString());
            chkBC_EXTNG_AVL.Checked = dtShop.Rows[0]["BC_EXTNG_AVL"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["BC_EXTNG_AVL"].ToString());
            chkADQT_CLEARANCE.Checked = dtShop.Rows[0]["ADQT_CLEARANCE"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["ADQT_CLEARANCE"].ToString());
            chkBEER_SALES.Checked = dtShop.Rows[0]["BEER_SALES"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["BEER_SALES"].ToString());
            chkWINE_SALES.Checked = dtShop.Rows[0]["WINE_SALES"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["WINE_SALES"].ToString());
            chkFULL_BAR.Checked = dtShop.Rows[0]["FULL_BAR"] == DBNull.Value ? false : bool.Parse(dtShop.Rows[0]["FULL_BAR"].ToString());

            txtSEATINGCAPACITY.Text = dtShop.Rows[0]["SEATINGCAPACITY"] == DBNull.Value ? "" : dtShop.Rows[0]["SEATINGCAPACITY"].ToString();
            txtTOT_EXPNS_FOOD_LIQUOR.Text = dtShop.Rows[0]["TOT_EXPNS_FOOD_LIQUOR"] == DBNull.Value ? "" : dtShop.Rows[0]["TOT_EXPNS_FOOD_LIQUOR"].ToString();
            txtTOT_EXPNS_OTHERS.Text = dtShop.Rows[0]["TOT_EXPNS_OTHERS"] == DBNull.Value ? "" : dtShop.Rows[0]["TOT_EXPNS_OTHERS"].ToString();
            txtNET_PROFIT.Text = dtShop.Rows[0]["NET_PROFIT"] == DBNull.Value ? "" : dtShop.Rows[0]["NET_PROFIT"].ToString();
            txtACCNT_PAYABLE.Text = dtShop.Rows[0]["ACCNT_PAYABLE"] == DBNull.Value ? "" : dtShop.Rows[0]["ACCNT_PAYABLE"].ToString();
            txtNOTES_PAYABLE.Text = dtShop.Rows[0]["NOTES_PAYABLE"] == DBNull.Value ? "" : dtShop.Rows[0]["NOTES_PAYABLE"].ToString();

            txtBNK_LOANS_PAYABLE.Text = dtShop.Rows[0]["BNK_LOANS_PAYABLE"] == DBNull.Value ? "" : dtShop.Rows[0]["BNK_LOANS_PAYABLE"].ToString();
           



        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            int intReturn = 0;
            intReturn = SaveFormValue();
           


        }

        private int SaveFormValue()
        {
            int intRetVal = 0;
            int intReturn = 0;
            try
            {
                //ClsNatureBusiness objNature = new ClsNatureBusiness();
                ClsSuppFormsInformation objPremises = new ClsSuppFormsInformation();
                //ClsNatureBusinessInfo objNatureInfo = getFormValue();

                ClsSuppFormRestaurantInfo objMPremise = getFormValue();

                if (hidRESTAURANT_ID.Value == "NEW")
                {
                    objMPremise.CREATED_BY = int.Parse(GetUserId());
                    objMPremise.CREATED_DATETIME = ConvertToDate(DateTime.Now.ToString());
                    // intRetVal = objNature.AddNatureBusiness(objNatureInfo, XmlFullFilePath);
                    intRetVal = objPremises.AddRestaurantInformation(objMPremise, XmlFullFilePath);

                    if (intRetVal > 0)
                    {
                        
                        //hidFormSaved.Value = "1";
                        hidOldData.Value = ClsDefaultValues.GetXmlForPageControls(hidDETAIL_TYPE_ID.Value);
                        lblMessage.Text = "Information saved successfully";
                    }
                    else if (intRetVal == -1)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "18");
                        //hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                        //hidFormSaved.Value = "2";

                    }
                    lblMessage.Visible = true;
                }
                else
                {
                    //objNatureInfo.BUSINESS_ID = int.Parse(hidBusiness_ID.Value);
                    objMPremise.RESTAURANT_ID = int.Parse(hidRESTAURANT_ID.Value);
                    // hidPremLoc_ID = int.Parse(hidPremLoc_ID.Value);
                    objMPremise.CREATED_BY = int.Parse(GetUserId());
                    objMPremise.CREATED_DATETIME = ConvertToDate(DateTime.Now.ToString());
                    intRetVal = objPremises.UpdateRestaurantInformation(objMPremise, XmlFullFilePath);

                    if (intRetVal > 0)
                    {
                        lblMessage.Text = "Information updated successfully";
                        //hidFormSaved.Value = "1";
                        hidOldData.Value = ClsDefaultValues.GetXmlForPageControls(hidDETAIL_TYPE_ID.Value);
                        //lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                    }
                    else if (intReturn == -1)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "18");
                        //hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                        //hidFormSaved.Value = "2";

                    }
                    lblMessage.Visible = true;

                }
                return intRetVal;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        // To delete the data on form value
        protected void btndelete_Click(object sender, EventArgs e)
        {

            int intReturn = 0;
            int intPremLocId = int.Parse(hidRESTAURANT_ID.Value);
            //ClsAgencyInfo objAgencyInfo = GetFormValue();
            ClsSuppFormsInformation objPremises = new ClsSuppFormsInformation();
            //ClsNatureBusinessInfo objNatureInfo = getFormValue();

            ClsSuppFormRestaurantInfo objMPremise = getFormValue();
            objMPremise.RESTAURANT_ID = int.Parse(hidRESTAURANT_ID.Value);
            objMPremise.CREATED_BY = int.Parse(GetUserId());
            objMPremise.CREATED_DATETIME = ConvertToDate(DateTime.Now.ToString());
            intReturn = objPremises.DeleteRestaurantInformation(objMPremise, XmlFullFilePath);
            if (intReturn > 0)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                //hidFormSaved.Value = "1";
                hidOldData.Value = ClsDefaultValues.GetXmlForPageControls(hidDETAIL_TYPE_ID.Value);
                lblMessage.Text = "Record Deleted successfully";

            }
            else if (intReturn == -1)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "18");
                // hidFormSaved.Value = "2";
            }
            else
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "20");
                // hidFormSaved.Value = "2";
                lblMessage.Text = "Record Deleted successfully";
            }
            lblMessage.Visible = true;

        }

        private ClsSuppFormRestaurantInfo getFormValue()
        {
            // ClsNatureBusinessInfo obj = new ClsNatureBusinessInfo();
            ClsSuppFormRestaurantInfo obj = new ClsSuppFormRestaurantInfo();

            obj.CUSTOMER_ID = int.Parse(GetCustomerID());
            obj.POLICY_ID = int.Parse(GetPolicyID());
            obj.POLICY_VERSION_ID = int.Parse(GetPolicyVersionID());
            obj.LOCATION_ID = 1;
            obj.PREMISES_ID = 1;

            obj.BUS_TYP_RESTURANT = chkBUS_TYP_RESTURANT.Checked;
            obj.BUS_TYP_FM_STYLE = chkBUS_TYP_FM_STYLE.Checked;
            obj.BUS_TYP_NGHT_CLUB = chkBUS_TYP_NGHT_CLUB.Checked;

            obj.BUS_TYP_FRNCHSED = chkBUS_TYP_FRNCHSED.Checked;
            obj.BUS_TYP_NT_FRNCHSED = chkBUS_TYP_NT_FRNCHSED.Checked;
            obj.BUS_TYP_SEASONAL = chkBUS_TYP_SEASONAL.Checked;

            obj.BUS_TYP_YR_ROUND = chkBUS_TYP_YR_ROUND.Checked;
            obj.BUS_TYP_DINNER = chkBUS_TYP_DINNER.Checked;

            obj.BUS_TYP_BNQT_HALL = chkBUS_TYP_BNQT_HALL.Checked;
            obj.BUS_TYP_BREKFAST = chkBUS_TYP_BREKFAST.Checked;
            obj.BUS_TYP_FST_FOOD = chkBUS_TYP_FST_FOOD.Checked;
            obj.BUS_TYP_TAVERN = chkBUS_TYP_TAVERN.Checked;

            obj.BUS_TYP_OTHER = chkBUS_TYP_OTHER.Checked;

            obj.STAIRWAYS = chkSTAIRWAYS.Checked;
            obj.ELEVATORS = chkELEVATORS.Checked;
            obj.ESCALATORS = chkESCALATORS.Checked;
            obj.GRILLING = chkGRILLING.Checked;
            obj.FRYING = chkFRYING.Checked;

            obj.BROILING = chkBROILING.Checked;
            obj.ROASTING = chkROASTING.Checked;
            obj.COOKING = chkCOOKING.Checked;
            obj.PRK_TYP_VALET = chkPRK_TYP_VALET.Checked;
            obj.PRK_TYP_PREMISES = chkPRK_TYP_PREMISES.Checked;
            obj.OPR_ON_PREMISES = chkOPR_ON_PREMISES.Checked;

            obj.OPR_OFF_PREMISES = chkOPR_OFF_PREMISES.Checked;
            obj.EMRG_LIGHTS = chkEMRG_LIGHTS.Checked;
            obj.WOOD_STOVE = chkWOOD_STOVE.Checked;
            obj.HIST_MARKER = chkHIST_MARKER.Checked;
            obj.EXTNG_SYS_COV_COOKNG = chkEXTNG_SYS_COV_COOKNG.Checked;
            obj.PRK_TYP_PREMISES = chkPRK_TYP_PREMISES.Checked;

            obj.EXTNG_SYS_MNT_CNTRCT = chkEXTNG_SYS_MNT_CNTRCT.Checked;
            obj.GAS_OFF_COOKNG = chkGAS_OFF_COOKNG.Checked;
            obj.HOOD_FILTER_CLND = chkHOOD_FILTER_CLND.Checked;
            obj.HOOD_DUCTS_EQUIP = chkHOOD_DUCTS_EQUIP.Checked;
            obj.HOOD_DUCTS_MNT_SCH = chkHOOD_DUCTS_MNT_SCH.Checked;
            obj.BC_EXTNG_AVL = chkBC_EXTNG_AVL.Checked;


            obj.ADQT_CLEARANCE = chkADQT_CLEARANCE.Checked;
            obj.BEER_SALES = chkBEER_SALES.Checked;
            obj.WINE_SALES = chkWINE_SALES.Checked;
            obj.FULL_BAR = chkFULL_BAR.Checked;
            
            obj.TOT_EXPNS_FOOD_LIQUOR = String.IsNullOrEmpty(txtTOT_EXPNS_FOOD_LIQUOR.Text) ? 0 : decimal.Parse(txtTOT_EXPNS_FOOD_LIQUOR.Text, NfiBaseCurrency);

            obj.TOT_EXPNS_OTHERS = String.IsNullOrEmpty(txtTOT_EXPNS_OTHERS.Text) ? 0 : decimal.Parse(txtTOT_EXPNS_OTHERS.Text, NfiBaseCurrency);

            obj.NET_PROFIT = String.IsNullOrEmpty(txtNET_PROFIT.Text) ? 0 : decimal.Parse(txtNET_PROFIT.Text, NfiBaseCurrency);
            obj.ACCNT_PAYABLE = String.IsNullOrEmpty(txtACCNT_PAYABLE.Text) ? 0 : decimal.Parse(txtACCNT_PAYABLE.Text, NfiBaseCurrency);
            obj.NOTES_PAYABLE = String.IsNullOrEmpty(txtNOTES_PAYABLE.Text) ? 0 : decimal.Parse(txtNOTES_PAYABLE.Text, NfiBaseCurrency);
            obj.BNK_LOANS_PAYABLE = String.IsNullOrEmpty(txtBNK_LOANS_PAYABLE.Text) ? 0 : decimal.Parse(txtBNK_LOANS_PAYABLE.Text, NfiBaseCurrency);
            obj.SEATINGCAPACITY = String.IsNullOrEmpty(txtSEATINGCAPACITY.Text) ? 0 : int.Parse(txtSEATINGCAPACITY.Text, NfiBaseCurrency);


            
            return obj;
        }
        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btndelete.Click += new EventHandler(this.btndelete_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion
    }
}