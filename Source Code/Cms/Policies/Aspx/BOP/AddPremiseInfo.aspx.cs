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
using Cms.Policies.Aspx;

namespace Cms.Policies.Aspx.BOP
{
    public partial class AddPremiseInfo : Cms.Policies.policiesbase
    {

        #region Page controls declaration
        //protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;

        //protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        //protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        //protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;

        //protected System.Web.UI.HtmlControls.HtmlInputHidden hidExtraCover;

        //protected Cms.CmsWeb.Controls.CmsButton btnReset;
        //protected Cms.CmsWeb.Controls.CmsButton btnSave;
        //protected System.Web.UI.WebControls.Label lblMessage;

        //protected System.Web.UI.HtmlControls.HtmlInputHidden hidDETAIL_TYPE_ID;
        //protected System.Web.UI.HtmlControls.HtmlInputHidden hidTYPE_ID;
        //protected System.Web.UI.HtmlControls.HtmlInputHidden hidTRAN_CODE;
        //protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
        //protected System.Web.UI.HtmlControls.HtmlInputHidden hidCRITERIA_DESC;
        //protected System.Web.UI.HtmlControls.HtmlInputHidden hidBuilding_ID;
        //PREMISES_ID
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOC_DETAILS_ID;

        //protected System.Web.UI.HtmlControls.HtmlTableRow trbody;
        //protected System.Web.UI.HtmlControls.HtmlTableRow trTcode;

        //protected System.Web.UI.WebControls.Label capMessages;
        //protected System.Web.UI.WebControls.HyperLink hlkBusiness_Date;
        //protected System.Web.UI.WebControls.HyperLink hlkEXPIRATION_DATE;
        private string XmlSchemaFileName = "";
        private string XmlFullFilePath = "";
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            NfiBaseCurrency.NumberDecimalDigits = 2;
            txtANN_SALES.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
            txtTOT_PAYROLL.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
            txtMAX_CASH_MSG.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
            txtMAX_CASH_PREM.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
            txtMONEY_OVER_NIGHT.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
            
            
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

            XmlSchemaFileName = "AddPremises.xml";
            XmlFullFilePath = Request.PhysicalApplicationPath + "Policies/support/PageXML/" + strSysID + "/" + XmlSchemaFileName;
            btnReset.Attributes.Add("onclick", "javascript:return ResetForm();");

            GetLocationInforamtion();
            SetErrormessage();

            if (!Page.IsPostBack)
            {

                //PopulateDropDown(cmbBLANKET);
                //BI common object
                if (ClsCommon.IsXMLResourceExists(@Request.PhysicalApplicationPath + "Policies/support/PageXML/" + GetSystemId(), "AddPremises.xml"))
                {
                    setPageControls(Page, @Request.PhysicalApplicationPath + "/Policies/support/PageXML/" + GetSystemId() + "/AddPremises.xml");
                }

                ClsPremisesInformation objPremises = new ClsPremisesInformation();
                //DataSet dsPremiseLocDetails=objPremises.GetPremisesLocDetails(GetCustomerID(), GetPolicyID(), GetPolicyVersionID(),"1","1");
                DataSet dsPremiseLocDetails = objPremises.GetPremisesLocDetails(GetCustomerID(), GetPolicyID(), GetPolicyVersionID(), lblLOCATION_ID.Text.ToString());
                if (dsPremiseLocDetails.Tables[0].Rows.Count > 0)
                {
                    hidLOC_DETAILS_ID.Value = dsPremiseLocDetails.Tables[0].Rows[0]["PREMISES_ID"].ToString();
                    LoadData(dsPremiseLocDetails.Tables[0]);
                }
                else
                {
                    hidLOC_DETAILS_ID.Value = "NEW";

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
                for (int count = 1; count < 101; count++)
                {

                    ListItem ls = new ListItem();
                    ls.Text = count.ToString();
                    ls.Value = count.ToString();
                    cmbType.Items.Add(ls);
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void SetErrormessage()
        {
            //Below was commented as textbox changed to drop down list
            //revFIRE_HYDRANT_DIST.ValidationExpression = aRegExpDoublePositiveNonZero;
            //revFIRE_HYDRANT_DIST.ErrorMessage = "Please enter valid numeric value";

            //revFIRE_STATION_DIST.ValidationExpression = aRegExpDoublePositiveNonZero;
            //revFIRE_STATION_DIST.ErrorMessage = "Please enter valid numeric value";

            revTOT_PAYROLL.ValidationExpression = aRegExpDoublePositiveNonZero;
            revTOT_PAYROLL.ErrorMessage = "Please enter valid Amount";

            revPROT_CLS.ValidationExpression = aRegExpInteger;
            revPROT_CLS.ErrorMessage = "Please enter valid numeric value";


        }

        private void GetLocationInforamtion()
        {
            ClsPremisesInformation objPremises = new ClsPremisesInformation();
            DataSet dsPremises = objPremises.GetPremisesInformation(GetCustomerID(), GetPolicyID(), GetPolicyVersionID());
            if (dsPremises.Tables[0].Rows.Count > 0)
            {
                lblLOCATION_ID.Text = dsPremises.Tables[0].Rows[0]["LOCATION_ID"].ToString();
                lblBUILDING_ID.Text = dsPremises.Tables[0].Rows[0]["BUILDING"].ToString();
                //lblCLASS_CODE.Text =  dsPremises.Tables[0].Rows[0]["LOCATION_ID"].ToString();
            }
        }


        private void LoadData(DataTable dtPremiseLocDetails)
        {
            
            //txtTOT_AREA.Text = dtPremises.Rows[0]["TOT_AREA"] == DBNull.Value ? "" : dtPremises.Rows[0]["TOT_AREA"].ToString();
            //txtDESC_BLDNG.Text = dtPremiseLocDetails.Rows[0]["DESC_BLDNG"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["DESC_BLDNG"].ToString();
            //txtDESC_OPERTN.Text = dtPremiseLocDetails.Rows[0]["DESC_OPERTN"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["DESC_OPERTN"].ToString();

            txtLST_ALL_OCCUP.Text = dtPremiseLocDetails.Rows[0]["LST_ALL_OCCUP"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["LST_ALL_OCCUP"].ToString();
            txtANN_SALES.Text = dtPremiseLocDetails.Rows[0]["ANN_SALES"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["ANN_SALES"].ToString();
            txtTOT_PAYROLL.Text = dtPremiseLocDetails.Rows[0]["TOT_PAYROLL"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["TOT_PAYROLL"].ToString();

            txtPROT_CLS.Text = dtPremiseLocDetails.Rows[0]["PROT_CLS"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["PROT_CLS"].ToString();
            //txtFIRE_HYDRANT_DIST.Text = dtPremiseLocDetails.Rows[0]["FIRE_HYDRANT_DIST"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["FIRE_HYDRANT_DIST"].ToString();
            //txtFIRE_STATION_DIST.Text = dtPremiseLocDetails.Rows[0]["FIRE_STATION_DIST"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["FIRE_STATION_DIST"].ToString();

            txtFIRE_DIST_NAME.Text = dtPremiseLocDetails.Rows[0]["FIRE_DIST_NAME"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["FIRE_DIST_NAME"].ToString();
            txtFIRE_DIST_CODE.Text = dtPremiseLocDetails.Rows[0]["FIRE_DIST_CODE"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["FIRE_DIST_CODE"].ToString();
            //txtBCEGS.Text = dtPremiseLocDetails.Rows[0]["BCEGS"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["BCEGS"].ToString();


            txtSAFE_VAULT_CLASS.Text = dtPremiseLocDetails.Rows[0]["SAFE_VAULT_CLASS"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["SAFE_VAULT_CLASS"].ToString();
            txtSAFE_VAULT_MANUFAC.Text = dtPremiseLocDetails.Rows[0]["SAFE_VAULT_MANUFAC"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["SAFE_VAULT_MANUFAC"].ToString();
            txtMAX_CASH_PREM.Text = dtPremiseLocDetails.Rows[0]["MAX_CASH_PREM"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["MAX_CASH_PREM"].ToString();


            txtMAX_CASH_MSG.Text = dtPremiseLocDetails.Rows[0]["MAX_CASH_MSG"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["MAX_CASH_MSG"].ToString();
            txtMONEY_OVER_NIGHT.Text = dtPremiseLocDetails.Rows[0]["MONEY_OVER_NIGHT"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["MONEY_OVER_NIGHT"].ToString();
            txtFREQUENCY_DEPOSIT.Text = dtPremiseLocDetails.Rows[0]["FREQUENCY_DEPOSIT"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["FREQUENCY_DEPOSIT"].ToString();

            txtSAFE_DOOR_CONST.Text = dtPremiseLocDetails.Rows[0]["SAFE_DOOR_CONST"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["SAFE_DOOR_CONST"].ToString();
            txtGRADE.Text = dtPremiseLocDetails.Rows[0]["GRADE"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["GRADE"].ToString();
            txtOTH_PROTECTION.Text = dtPremiseLocDetails.Rows[0]["OTH_PROTECTION"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["OTH_PROTECTION"].ToString();
            // for safe dor below


            txtRIGHT_EXP_DESC.Text = dtPremiseLocDetails.Rows[0]["RIGHT_EXP_DESC"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["RIGHT_EXP_DESC"].ToString();
            txtFRONT_EXP_DESC.Text = dtPremiseLocDetails.Rows[0]["FRONT_EXP_DESC"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["FRONT_EXP_DESC"].ToString();
            txtRIGHT_EXP_DIST.Text = dtPremiseLocDetails.Rows[0]["RIGHT_EXP_DIST"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["RIGHT_EXP_DIST"].ToString();
            txtFRONT_EXP_DIST.Text = dtPremiseLocDetails.Rows[0]["FRONT_EXP_DIST"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["FRONT_EXP_DIST"].ToString();

            txtLEFT_EXP_DESC.Text = dtPremiseLocDetails.Rows[0]["LEFT_EXP_DESC"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["LEFT_EXP_DESC"].ToString();
            txtREAR_EXP_DESC.Text = dtPremiseLocDetails.Rows[0]["REAR_EXP_DESC"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["REAR_EXP_DESC"].ToString();
            txtLEFT_EXP_DIST.Text = dtPremiseLocDetails.Rows[0]["LEFT_EXP_DIST"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["LEFT_EXP_DIST"].ToString();
            txtREAR_EXP_DIST.Text = dtPremiseLocDetails.Rows[0]["REAR_EXP_DIST"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["REAR_EXP_DIST"].ToString();

            //cmbSTATE.SelectedValue = dtPremiseLocDetails.Rows[0]["STATE"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["STATE"].ToString();

            cmbBLANKET.SelectedValue = dtPremiseLocDetails.Rows[0]["BLANKETRATE"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["BLANKETRATE"].ToString();
            cmbFIRE_HYDRANT_DIST.SelectedValue = dtPremiseLocDetails.Rows[0]["FIRE_HYDRANT_DIST"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["FIRE_HYDRANT_DIST"].ToString();
            cmbFIRE_STATION_DIST.SelectedValue = dtPremiseLocDetails.Rows[0]["FIRE_STATION_DIST"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["FIRE_STATION_DIST"].ToString();
            cmbIS_ALM_USED.SelectedValue = dtPremiseLocDetails.Rows[0]["IS_ALM_USED"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["IS_ALM_USED"].ToString();
            cmbIS_RES_SPACE.SelectedValue = dtPremiseLocDetails.Rows[0]["IS_RES_SPACE"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["IS_RES_SPACE"].ToString();

            cmbRES_SPACE_SMK_DET.SelectedValue = dtPremiseLocDetails.Rows[0]["RES_SPACE_SMK_DET"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["RES_SPACE_SMK_DET"].ToString();
            cmbRES_OCC.SelectedValue = dtPremiseLocDetails.Rows[0]["RES_OCC"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["RES_OCC"].ToString();
            cmbCITY_LMT.SelectedValue = dtPremiseLocDetails.Rows[0]["CITY_LMT"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["CITY_LMT"].ToString();
            cmbSWIMMING_POOL.SelectedValue = dtPremiseLocDetails.Rows[0]["SWIMMING_POOL"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["SWIMMING_POOL"].ToString();
            cmbPLAY_GROUND.SelectedValue = dtPremiseLocDetails.Rows[0]["PLAY_GROUND"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["PLAY_GROUND"].ToString();


            cmbBUILD_UNDER_CON.SelectedValue = dtPremiseLocDetails.Rows[0]["BUILD_UNDER_CON"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["BUILD_UNDER_CON"].ToString();
            cmbBUILD_SHPNG_CENT.SelectedValue = dtPremiseLocDetails.Rows[0]["BUILD_SHPNG_CENT"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["BUILD_SHPNG_CENT"].ToString();
            cmbBOILER.SelectedValue = dtPremiseLocDetails.Rows[0]["BOILER"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["BOILER"].ToString();
            cmbMED_EQUIP.SelectedValue = dtPremiseLocDetails.Rows[0]["MED_EQUIP"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["MED_EQUIP"].ToString();
            cmbALARM_TYPE.SelectedValue = dtPremiseLocDetails.Rows[0]["ALARM_TYPE"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["ALARM_TYPE"].ToString();


            cmbALARM_DESC.SelectedValue = dtPremiseLocDetails.Rows[0]["ALARM_DESC"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["ALARM_DESC"].ToString();
            cmbSAFE_VAULT.SelectedValue = dtPremiseLocDetails.Rows[0]["SAFE_VAULT"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["SAFE_VAULT"].ToString();
            cmbPREMISE_ALARM.SelectedValue = dtPremiseLocDetails.Rows[0]["PREMISE_ALARM"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["PREMISE_ALARM"].ToString();
            cmbCYL_DOOR_LOCK.SelectedValue = dtPremiseLocDetails.Rows[0]["CYL_DOOR_LOCK"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["CYL_DOOR_LOCK"].ToString();
            cmbSAFE_VAULT_LBL.SelectedValue = dtPremiseLocDetails.Rows[0]["SAFE_VAULT_LBL"] == DBNull.Value ? "" : dtPremiseLocDetails.Rows[0]["SAFE_VAULT_LBL"].ToString();


        }

        #region "Web Event Handlers"
        /// <summary>
        /// If form is posted back then add entry in database using the BL object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            int intReturn = 0;
            intReturn = SaveFormValue();
            if (intReturn > 0)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "29");
                //hidFormSaved.Value = "1";
                hidOldData.Value = ClsDefaultValues.GetXmlForPageControls(hidDETAIL_TYPE_ID.Value);
                lblMessage.Text = "Information saved successfully";
                if (hidLOC_DETAILS_ID.Value == "NEW")
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

        private int SaveFormValue()
        {
            int intRetVal = 0;
            try
            {
                //ClsNatureBusiness objNature = new ClsNatureBusiness();
                ClsPremisesInformation objPremises = new ClsPremisesInformation();
                //ClsNatureBusinessInfo objNatureInfo = getFormValue();

                ClsPremisesInfo objMPremise = getFormValue();

                if (hidLOC_DETAILS_ID.Value == "NEW")
                {
                    // intRetVal = objNature.AddNatureBusiness(objNatureInfo, XmlFullFilePath);
                    intRetVal = objPremises.AddPremisesLocDetails(objMPremise, XmlFullFilePath);
                }
                else
                {
                    //objNatureInfo.BUSINESS_ID = int.Parse(hidBusiness_ID.Value);
                    objMPremise.LOC_DETAILS_ID = int.Parse(hidLOC_DETAILS_ID.Value);
                    // hidPremLoc_ID = int.Parse(hidPremLoc_ID.Value);
                    intRetVal = objPremises.UpdatePremisesLocDetails(objMPremise, XmlFullFilePath);
                }
                return intRetVal;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        protected void btndelete_Click(object sender, EventArgs e)
        {



            int intReturn;
            int intPremLocId = int.Parse(hidLOC_DETAILS_ID.Value);
            //ClsAgencyInfo objAgencyInfo = GetFormValue();
            ClsPremisesInformation objPremises = new ClsPremisesInformation();
            //ClsNatureBusinessInfo objNatureInfo = getFormValue();

            ClsPremisesInfo objMPremise = getFormValue();
            objMPremise.LOC_DETAILS_ID = intPremLocId;
            intReturn = objPremises.DeletePremisesLocDetails(objMPremise, XmlFullFilePath);
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
        private ClsPremisesInfo getFormValue()
        {
            // ClsNatureBusinessInfo obj = new ClsNatureBusinessInfo();
            ClsPremisesInfo obj = new ClsPremisesInfo();
            //below hard core value wil fetched from session object
            obj.PREMISES_ID = 1;
            obj.LOCATION_ID = 1;
            obj.CUSTOMER_ID = int.Parse(GetCustomerID());
            obj.POLICY_ID = int.Parse(GetPolicyID());
            obj.POLICY_VERSION_ID = int.Parse(GetPolicyVersionID());
           // obj.DESC_BLDNG = String.IsNullOrEmpty(txtDESC_BLDNG.Text) ? "" : txtDESC_BLDNG.Text;
           // obj.DESC_OPERTN = String.IsNullOrEmpty(txtDESC_OPERTN.Text) ? "" : txtDESC_OPERTN.Text;
            obj.LST_ALL_OCCUP = String.IsNullOrEmpty(txtLST_ALL_OCCUP.Text) ? "" : txtLST_ALL_OCCUP.Text;

            obj.ANN_SALES = String.IsNullOrEmpty(txtANN_SALES.Text) ? 0 : decimal.Parse(txtANN_SALES.Text, NfiBaseCurrency);
            obj.TOT_PAYROLL = String.IsNullOrEmpty(txtTOT_PAYROLL.Text) ? 0 : decimal.Parse(txtTOT_PAYROLL.Text, NfiBaseCurrency);
            obj.PROT_CLS = String.IsNullOrEmpty(txtPROT_CLS.Text) ? "" : txtPROT_CLS.Text;
          
           // obj.FIRE_HYDRANT_DIST = String.IsNullOrEmpty(txtFIRE_HYDRANT_DIST.Text) ? 0 : decimal.Parse(txtFIRE_HYDRANT_DIST.Text);

            //obj.FIRE_STATION_DIST = String.IsNullOrEmpty(txtFIRE_STATION_DIST.Text) ? 0 : decimal.Parse(txtFIRE_STATION_DIST.Text);
            obj.FIRE_DIST_NAME = String.IsNullOrEmpty(txtFIRE_DIST_NAME.Text) ? "" : (txtFIRE_DIST_NAME.Text);

            obj.FIRE_DIST_CODE = String.IsNullOrEmpty(txtFIRE_DIST_CODE.Text) ? "" : (txtFIRE_DIST_CODE.Text);

            
            //obj.BCEGS = String.IsNullOrEmpty(txtBCEGS.Text) ? "" : (txtBCEGS.Text);
            obj.BCEGS = "";
            // To get the combo values
            obj.FIRE_HYDRANT_DIST = String.IsNullOrEmpty(cmbFIRE_HYDRANT_DIST.SelectedValue) ? 0 : decimal.Parse(cmbFIRE_HYDRANT_DIST.SelectedValue);
            obj.FIRE_STATION_DIST = String.IsNullOrEmpty(cmbFIRE_STATION_DIST.SelectedValue) ? 0 : decimal.Parse(cmbFIRE_STATION_DIST.SelectedValue);
            obj.IS_ALM_USED = cmbIS_ALM_USED.SelectedValue;
            obj.IS_RES_SPACE = cmbIS_RES_SPACE.SelectedValue;

            obj.RES_SPACE_SMK_DET = cmbRES_SPACE_SMK_DET.SelectedValue;
            obj.RES_OCC = cmbRES_OCC.SelectedValue;
            obj.CITY_LMT = cmbCITY_LMT.SelectedValue;
            obj.SWIMMING_POOL = cmbSWIMMING_POOL.SelectedValue;

            obj.PLAY_GROUND = cmbPLAY_GROUND.SelectedValue;
            obj.BUILD_UNDER_CON = cmbBUILD_UNDER_CON.SelectedValue;
            obj.BUILD_SHPNG_CENT = cmbBUILD_SHPNG_CENT.SelectedValue;
            obj.BOILER = cmbBOILER.SelectedValue;
            obj.MED_EQUIP = cmbMED_EQUIP.SelectedValue;

            //Crime section
            obj.ALARM_TYPE = cmbALARM_TYPE.SelectedValue;
            obj.ALARM_DESC = cmbALARM_DESC .SelectedValue;
            obj.SAFE_VAULT = cmbSAFE_VAULT.SelectedValue;
            obj.PREMISE_ALARM = cmbPREMISE_ALARM.SelectedValue;

            obj.CYL_DOOR_LOCK = cmbCYL_DOOR_LOCK.SelectedValue;
            obj.SAFE_VAULT_LBL = cmbSAFE_VAULT_LBL.SelectedValue;            

            obj.SAFE_VAULT_CLASS =String.IsNullOrEmpty(txtSAFE_VAULT_CLASS.Text) ? "" : txtSAFE_VAULT_CLASS.Text;
            obj.SAFE_VAULT_MANUFAC = String.IsNullOrEmpty(txtSAFE_VAULT_MANUFAC.Text) ? "" : txtSAFE_VAULT_MANUFAC.Text;

            obj.IS_ALM_USED = cmbIS_ALM_USED.SelectedValue;
            obj.IS_RES_SPACE = cmbIS_RES_SPACE.SelectedValue;
            obj.BLANKETRATE = cmbBLANKET.SelectedValue;

            //MAX_CASH_PREM

            obj.MAX_CASH_PREM = String.IsNullOrEmpty(txtMAX_CASH_PREM.Text) ? 0 : decimal.Parse(txtMAX_CASH_PREM.Text, NfiBaseCurrency);

            obj.MAX_CASH_MSG = String.IsNullOrEmpty(txtMAX_CASH_MSG.Text) ? 0 : decimal.Parse(txtMAX_CASH_MSG.Text, NfiBaseCurrency);

            obj.MONEY_OVER_NIGHT = String.IsNullOrEmpty(txtMONEY_OVER_NIGHT.Text) ? 0 : decimal.Parse(txtMONEY_OVER_NIGHT.Text, NfiBaseCurrency);
            obj.SAFE_DOOR_CONST = String.IsNullOrEmpty(txtSAFE_DOOR_CONST.Text) ? "" : txtSAFE_DOOR_CONST.Text;
            obj.OTH_PROTECTION = String.IsNullOrEmpty(txtOTH_PROTECTION.Text) ? "" : txtOTH_PROTECTION.Text;
           
            obj.RIGHT_EXP_DESC = String.IsNullOrEmpty(txtRIGHT_EXP_DESC.Text) ? "" : txtRIGHT_EXP_DESC.Text;
            obj.RIGHT_EXP_DIST = String.IsNullOrEmpty(txtRIGHT_EXP_DIST.Text) ? "" : txtRIGHT_EXP_DIST.Text;

            obj.LEFT_EXP_DESC = String.IsNullOrEmpty(txtLEFT_EXP_DESC.Text) ? "" : txtLEFT_EXP_DESC.Text;
            obj.LEFT_EXP_DIST = String.IsNullOrEmpty(txtLEFT_EXP_DIST.Text) ? "" : txtLEFT_EXP_DIST.Text;

            obj.FRONT_EXP_DESC = String.IsNullOrEmpty(txtFRONT_EXP_DESC.Text) ? "" : txtFRONT_EXP_DESC.Text;
            obj.FRONT_EXP_DIST = String.IsNullOrEmpty(txtFRONT_EXP_DIST.Text) ? "" : txtFRONT_EXP_DIST.Text;

            obj.REAR_EXP_DESC = String.IsNullOrEmpty(txtREAR_EXP_DESC.Text) ? "" : txtREAR_EXP_DESC.Text;
            obj.REAR_EXP_DIST = String.IsNullOrEmpty(txtREAR_EXP_DIST.Text) ? "" : txtREAR_EXP_DIST.Text;

            obj.FREQUENCY_DEPOSIT = String.IsNullOrEmpty(txtFREQUENCY_DEPOSIT.Text) ? 0 : int.Parse(txtFREQUENCY_DEPOSIT.Text);
           
           

            return obj;
        }
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
