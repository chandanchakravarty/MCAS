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
    public partial class AddShop : Cms.Policies.policiesbase
    {

        #region Page controls declaration
        public const string FRYERS = "cmbNUM_FRYERS";
        public const string GRILLS = "cmbNUM_GRILLS";
        private string XmlSchemaFileName = "";
        private string XmlFullFilePath = "";
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidSHOP_ID;
        #endregion
       
        protected void Page_Load(object sender, EventArgs e)
        {

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

            XmlSchemaFileName = "AddSuppFormShop.xml";
            XmlFullFilePath = Request.PhysicalApplicationPath + "Policies/support/PageXML/" + strSysID + "/" + XmlSchemaFileName;
            btnReset.Attributes.Add("onclick", "javascript:return ResetForm();");
          
            if (!Page.IsPostBack)
            {

                PopulateDropDown(cmbNUM_FRYERS);
                PopulateDropDown(cmbNUM_GRILLS);
                
                //BI common object
                if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "Policies/support/PageXML/" + GetSystemId(), "AddSuppFormShop.xml"))
                {
                    setPageControls(Page, @Request.PhysicalApplicationPath + "/Policies/support/PageXML/" + GetSystemId() + "/AddSuppFormShop.xml");
                }

                ClsSuppFormsInformation objPremises = new ClsSuppFormsInformation();
                DataSet dsPremises = objPremises.GetShopInformation(GetCustomerID(), GetPolicyID(), GetPolicyVersionID(), "1","1");
                //DataSet dsPremises = objPremises.GetPremisesInformation(GetCustomerID(), "22", GetPolicyVersionID(), "22");
                //DataSet dsBusiness = objPremises.g(GetCustomerID(), GetPolicyID(), GetPolicyVersionID());
                //objPremises.G
                if (dsPremises.Tables[0].Rows.Count > 0)
                {
                    hidSHOP_ID.Value = dsPremises.Tables[0].Rows[0]["SHOP_ID"].ToString();
                    LoadData(dsPremises.Tables[0]);
                }
                else
                {
                    hidSHOP_ID.Value = "NEW";
                }

            }


        }
        private void PopulateDropDown(DropDownList cmbType)
        {

            try
            {
                if (cmbType.Items.Count > 0)
                {
                    cmbType.Items.Clear();
                }
                for (int count = 0; count < 11; count++)
                {

                    ListItem ls = new ListItem();
                    ls.Text = count.ToString();
                    ls.Value = count.ToString();
                    cmbType.Items.Add(ls);
                }
            }
            catch(Exception ex)
            {

            }

        }

        private void LoadData(DataTable dtShop)
        {          

         
            txtPERCENT_SALES.Text = dtShop.Rows[0]["PERCENT_SALES"] == DBNull.Value ? "" : dtShop.Rows[0]["PERCENT_SALES"].ToString();

            cmbIS_ENTERTNMT.SelectedValue = dtShop.Rows[0]["IS_ENTERTNMT"] == DBNull.Value ? "" : dtShop.Rows[0]["IS_ENTERTNMT"].ToString();
            cmbBLDG_TYPE_COOKNG.SelectedValue = dtShop.Rows[0]["BLDG_TYPE_COOKNG"] == DBNull.Value ? "" : dtShop.Rows[0]["BLDG_TYPE_COOKNG"].ToString();
            cmbBBQ_PIT.SelectedValue = dtShop.Rows[0]["BBQ_PIT"] == DBNull.Value ? "" : dtShop.Rows[0]["BBQ_PIT"].ToString();

            cmbSEPARATE_BAR.SelectedValue = dtShop.Rows[0]["SEPARATE_BAR"] == DBNull.Value ? "" : dtShop.Rows[0]["SEPARATE_BAR"].ToString();
                                 

            cmbIS_INSURED.SelectedValue = dtShop.Rows[0]["IS_INSURED"] == DBNull.Value ? "" : dtShop.Rows[0]["IS_INSURED"].ToString();
            cmbDUCT_CLND_PST_SIX_MONTHS.SelectedValue = dtShop.Rows[0]["DUCT_CLND_PST_SIX_MONTHS"] == DBNull.Value ? "" : dtShop.Rows[0]["DUCT_CLND_PST_SIX_MONTHS"].ToString();

            cmbSUPPR_SYS.SelectedValue = dtShop.Rows[0]["SUPPR_SYS"] == DBNull.Value ? "" : dtShop.Rows[0]["SUPPR_SYS"].ToString();
            cmbDUCT_SYS.SelectedValue = dtShop.Rows[0]["DUCT_SYS"] == DBNull.Value ? "" : dtShop.Rows[0]["DUCT_SYS"].ToString();

            cmbNUM_GRILLS.SelectedValue = dtShop.Rows[0]["NUM_GRILLS"] == DBNull.Value ? "" : dtShop.Rows[0]["NUM_GRILLS"].ToString();
            cmbNUM_FRYERS.SelectedValue = dtShop.Rows[0]["NUM_FRYERS"] == DBNull.Value ? "" : dtShop.Rows[0]["NUM_FRYERS"].ToString();
            
            cmbFLAME_COOKING.SelectedValue = dtShop.Rows[0]["FLAME_COOKING"] == DBNull.Value ? "" : dtShop.Rows[0]["FLAME_COOKING"].ToString();
            cmbRESTURANT_OCUP.SelectedValue = dtShop.Rows[0]["RESTURANT_OCUP"] == DBNull.Value ? "" : dtShop.Rows[0]["RESTURANT_OCUP"].ToString();
            cmbTENANT_LIABILITY.SelectedValue = dtShop.Rows[0]["TENANT_LIABILITY"] == DBNull.Value ? "" : dtShop.Rows[0]["TENANT_LIABILITY"].ToString();

            txtPERCENT_OCUP.Text = dtShop.Rows[0]["PERCENT_OCUP"] == DBNull.Value ? "" : dtShop.Rows[0]["PERCENT_OCUP"].ToString();
            txtUNITS.Text = dtShop.Rows[0]["UNITS"] == DBNull.Value ? "" : dtShop.Rows[0]["UNITS"].ToString();
            txtBBQ_PIT_DIST.Text = dtShop.Rows[0]["BBQ_PIT_DIST"] == DBNull.Value ? "" : dtShop.Rows[0]["BBQ_PIT_DIST"].ToString();


        }
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            int intReturn = 0;
            intReturn = SaveFormValue();
            if (intReturn > 0)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                //hidFormSaved.Value = "1";
                hidOldData.Value = ClsDefaultValues.GetXmlForPageControls(hidDETAIL_TYPE_ID.Value);
                //lblMessage.Text = "Information saved successfully";
                if (hidSHOP_ID.Value == "NEW")
                    lblMessage.Text = "Information saved successfully";
                else
                    lblMessage.Text = "Information updated successfully";
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

        //To save the data on form value
        private int SaveFormValue()
        {
            int intRetVal = 0;
            try
            {
                //ClsNatureBusiness objNature = new ClsNatureBusiness();
                ClsSuppFormsInformation objPremises = new ClsSuppFormsInformation();
                //ClsNatureBusinessInfo objNatureInfo = getFormValue();

                ClsSuppFormShopInfo objMPremise = getFormValue();

                if (hidSHOP_ID.Value == "NEW")
                {
                    // intRetVal = objNature.AddNatureBusiness(objNatureInfo, XmlFullFilePath);
                    intRetVal = objPremises.AddShopInformation(objMPremise, XmlFullFilePath);
                }
                else
                {
                    //objNatureInfo.BUSINESS_ID = int.Parse(hidBusiness_ID.Value);
                    objMPremise.SHOP_ID = int.Parse(hidSHOP_ID.Value);
                    // hidPremLoc_ID = int.Parse(hidPremLoc_ID.Value);
                    intRetVal = objPremises.UpdateShopInformation(objMPremise, XmlFullFilePath);
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

            int intReturn=0;
            int intPremLocId = int.Parse(hidSHOP_ID.Value);
            //ClsAgencyInfo objAgencyInfo = GetFormValue();
            ClsPremisesInformation objPremises = new ClsPremisesInformation();
            //ClsNatureBusinessInfo objNatureInfo = getFormValue();

            ClsSuppFormShopInfo objMPremise = getFormValue();
            objMPremise.SHOP_ID = intPremLocId;
            //intReturn = objPremises.DeletePremisesLocation(objMPremise, XmlFullFilePath);
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
        // To get the controls values on Model objects
        private ClsSuppFormShopInfo getFormValue()
        {
            // ClsNatureBusinessInfo obj = new ClsNatureBusinessInfo();
            ClsSuppFormShopInfo obj = new ClsSuppFormShopInfo();

            obj.CUSTOMER_ID = int.Parse(GetCustomerID());
            obj.POLICY_ID = int.Parse(GetPolicyID());
            obj.POLICY_VERSION_ID = int.Parse(GetPolicyVersionID());
            obj.LOCATION_ID = 1;
            obj.PREMISES_ID = 1;
            
            
            obj.FLAME_COOKING = cmbFLAME_COOKING.SelectedValue;
            obj.NUM_FRYERS = cmbNUM_FRYERS.SelectedValue;
            obj.RESTURANT_OCUP = cmbRESTURANT_OCUP.SelectedValue;         
       
            obj.UNITS = String.IsNullOrEmpty(txtUNITS.Text) ? 0 : int.Parse(txtUNITS.Text);
            obj.PERCENT_OCUP = String.IsNullOrEmpty(txtPERCENT_OCUP.Text) ? 0 : decimal.Parse(txtPERCENT_OCUP.Text);
            obj.FLAME_COOKING = cmbFLAME_COOKING.SelectedValue;
            obj.RESTURANT_OCUP = cmbRESTURANT_OCUP.SelectedValue;
            obj.NUM_FRYERS = cmbNUM_FRYERS.SelectedValue;
            obj.NUM_GRILLS = cmbNUM_GRILLS.SelectedValue;
            obj.DUCT_SYS = cmbDUCT_SYS.SelectedValue;
            obj.SUPPR_SYS = cmbSUPPR_SYS.SelectedValue;
            obj.DUCT_CLND_PST_SIX_MONTHS = cmbDUCT_CLND_PST_SIX_MONTHS.SelectedValue;
            obj.IS_INSURED = cmbIS_INSURED.SelectedValue;
            obj.TENANT_LIABILITY = cmbTENANT_LIABILITY.SelectedValue;
            obj.PERCENT_SALES = String.IsNullOrEmpty(txtPERCENT_SALES.Text) ? 0 : decimal.Parse(txtPERCENT_SALES.Text);
            obj.SEPARATE_BAR = cmbSEPARATE_BAR.SelectedValue;
            obj.BBQ_PIT = cmbBBQ_PIT.SelectedValue;
            obj.BBQ_PIT_DIST = String.IsNullOrEmpty(txtBBQ_PIT_DIST.Text) ? 0 : decimal.Parse(txtBBQ_PIT_DIST.Text);
            obj.BLDG_TYPE_COOKNG = cmbBLDG_TYPE_COOKNG.SelectedValue;

            obj.IS_ENTERTNMT = cmbIS_ENTERTNMT.SelectedValue;


            

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