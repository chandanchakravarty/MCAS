//------------------------------------------------------------------------------
// Modified by Pradeep Kushwaha on 10-05-2010
// 
//
//
//------------------------------------------------------------------------------
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
using Cms.Blapplication;
using System.Resources;
using Cms.Model.Policy;
 

namespace Cms.Policies.Aspx
{
    public partial class AddProductLocationInfo : Cms.Policies.policiesbase // cmsbase
    {
        ResourceManager objresource;
        //Declare Dropdown For Multiple Deductible dropdown

        private static String strRowId = String.Empty;
        ClsNamedPerils objNamedPerils = new ClsNamedPerils();
        ClsProducts objProduct = new ClsProducts();
        int  PRO_RISK_ID ;
        string CalledFrom;
        public string PAGEFROM;
        
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {

            Ajax.Utility.RegisterTypeForAjax(typeof(AddProductLocationInfo));
            if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"].ToString() != "")
                CalledFrom = Request.QueryString["CalledFrom"].ToString();

            if (Request.QueryString["PAGEFROM"] != null)
                PAGEFROM = Request.QueryString["PAGEFROM"].ToString();
            switch (CalledFrom) 
            {
                case "CompCondo"://Comprehensive Condominium 

                    hidCALLED_FROM.Value = CalledFrom;
                    base.ScreenId = COMPREHENSIVE_CONDOMINIUMscreenId.LOCATION_INFORMATION;
                    hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl_CompCondo");
                    tdACTUAL_INSURED_OBJECT.ColSpan = 3;
                    break;
               
                case "RISK": //Diverified Risk

                    base.ScreenId = DIVERSIFIED_RISKSscreenId.LOCATION_INFORMATION;
                    hidCALLED_FROM.Value = CalledFrom;
                    hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl_RISK");
                    tdClassField.Visible = false;
                    tdPortableEquipment.Visible = true;
                    
                    break;

                case "CompComp"://Comprehensive Company 

                    base.ScreenId = COMPREHENSIVE_COMPANYscreenId.LOCATION_INFORMATION;
                    hidCALLED_FROM.Value = CalledFrom;
                    hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl_CompComp");
                    tdACTUAL_INSURED_OBJECT.ColSpan = 3;
                    break;

                case "TFIRE"://TRADITIONAL FIRE

                    base.ScreenId = TRADITIONAL_FIREscreenId.LOCATION_INFORMATION;
                    hidCALLED_FROM.Value = CalledFrom;
                    hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl_TFIRE");
                    tdClassField.Visible = false;
                    tdACTUAL_INSURED_OBJECT.ColSpan = 3;
                    break;
                case "GLBANK"://Global of Bank

                    base.ScreenId = GLOBAL_OF_BANKscreenId.LOCATION_INFORMATION;
                    hidCALLED_FROM.Value = CalledFrom;
                    hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl_RISK");
                    tdClassField.Visible = false;
                    tdACTUAL_INSURED_OBJECT.ColSpan = 3;
                    break;
                case "JDLGR"://Judicial Guarantee (Garantia Judicial)
                    base.ScreenId = JUDICIAL_GUARANTEEscreenId.LOCATION_INFORMATION;
                    hidCALLED_FROM.Value = CalledFrom;
                    hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl_RISK");
                    tdClassField.Visible = false;
                    tdACTUAL_INSURED_OBJECT.ColSpan = 3;
                    break;
                default:  
                    base.ScreenId = "494_1";
                    break;

            }
            btnSave.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;


            btnDelete.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Delete;
            btnDelete.PermissionString = gstrSecurityXML;

            btnReset.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Write;
            btnActivateDeactivate.PermissionString = gstrSecurityXML;
            
            btnSelect.CmsButtonClass = CmsButtonType.Write;
            btnSelect.PermissionString = gstrSecurityXML;
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            btnSelect.Text = Cms.CmsWeb.ClsMessages.FetchGeneralButtonsText("btnSelect");

            objresource = new System.Resources.ResourceManager("Cms.Policies.Aspx.AddProductLocationInfo", System.Reflection.Assembly.GetExecutingAssembly());
            

            btnReset.Attributes.Add("onclick", "javascript: return ResetTheForm();");

            #region ISPOSTBACK

            if (!IsPostBack)
            {
                btnDelete.Attributes.Add("onclick", "javascript:return ShowAlertMessageWhileDelete(true);");
                hidCUSTOMER_ID.Value = GetCustomerID();
                hidPOLICY_ID.Value = GetPolicyID();
                hidPOLICY_VERSION_ID.Value = GetPolicyVersionID();
                if (CalledFrom == "CompCondo")
                {
                    tdClassField.Visible = true;
                    this.BindClassFieldForCompreensiveCondominium();
                }
                else if (CalledFrom == "CompComp")
                {
                    tdClassField.Visible = true;
                    this.BindClassFieldForCompreensiveCompany();
                }
                this.SetCaptions();
                this.SetErrorMessages();
                this.BindConstruction();
                this.BindLocation();
                this.BindMultiDeductible();
                this.BindPORTABLE_EQUIPMENT();
                //this.BindOccupiedAs(); commented for itrack # 1152/TFS#2598
                this.BindActivityType();
                this.PopulateCoApplicant();
                this.BindExceededPremium();
                if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
                    hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();
                else
                    hidCUSTOMER_ID.Value = GetCustomerID();

                if (Request.QueryString["POL_ID"] != null && Request.QueryString["POL_ID"].ToString() != "")
                    hidPOLICY_ID.Value = Request.QueryString["POL_ID"].ToString();
                else
                    hidPOLICY_ID.Value = GetPolicyID();

                if (Request.QueryString["POL_VERSION_ID"] != null && Request.QueryString["POL_VERSION_ID"].ToString() != "")
                    hidPOLICY_VERSION_ID.Value = Request.QueryString["POL_VERSION_ID"].ToString();
                else
                    hidPOLICY_VERSION_ID.Value = GetPolicyVersionID();

                if (Request.QueryString["PRODUCT_RISK_ID"] != null && Request.QueryString["PRODUCT_RISK_ID"].ToString() != "" && Request.QueryString["PRODUCT_RISK_ID"].ToString() != "NEW")
                {
                    hidPRODUCT_RISK_ID.Value = Request.QueryString["PRODUCT_RISK_ID"].ToString();
                    this.GetOldDataObject(Convert.ToInt32(hidPRODUCT_RISK_ID.Value));
                    btnDelete.Enabled= true;
                    btnActivateDeactivate.Enabled = true;
                    btnDelete.Visible = true;
                    strRowId = hidPRODUCT_RISK_ID.Value;
                }
                else
                {
                    hidPRODUCT_RISK_ID.Value = "NEW";
                    strRowId = "NEW";
                    btnActivateDeactivate.Enabled = false;
                    btnDelete.Enabled = false;
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");   
                }

               

            }
            #endregion

            lblDelete.Text = "";
        }  //Page_load
        #endregion

        #region Set Captions from resource file for multilingual
        private void SetCaptions()
        {
            capACTIVITY_TYPE.Text = objresource.GetString("cmbACTIVITY_TYPE");
            capASSIST24.Text = objresource.GetString("chkASSIST24");
            capBUILDING_VALUE.Text = objresource.GetString("txtBUILDING_VALUE");
            capCONSTRUCTION.Text = objresource.GetString("cmbCONSTRUCTION");
            capCONTENTS_RAW_VALUES.Text = objresource.GetString("txtCONTENTS_RAW_VALUES");
            capLOCATION.Text = objresource.GetString("cmbLOCATION");
            capMAXIMUM_LIMIT.Text = objresource.GetString("txtMAXIMUM_LIMIT");
            capMULTIPLE_DEDUCTIBLE.Text = objresource.GetString("cmbMULTIPLE_DEDUCTIBLE");
            capOCCUPIED_AS.Text = objresource.GetString("cmbOCCUPIED_AS");
            capPARKING_SPACES.Text = objresource.GetString("txtPARKING_SPACES");
            capPOSSIBLE_MAX_LOSS.Text = objresource.GetString("txtPOSSIBLE_MAX_LOSS");
            capREMARKS.Text = objresource.GetString("txtREMARKS");
            capRUBRICA.Text = objresource.GetString("txtRUBRICA");
            capVALUE_AT_RISK.Text = objresource.GetString("txtVALUE_AT_RISK");
            capCONTENTS_VALUE.Text = objresource.GetString("txtCONTENTS_VALUE");
            capRAW_MATERIAL_VALUE.Text = objresource.GetString("txtRAW_MATERIAL_VALUE");
            lblManHeader.Text = objresource.GetString("lblManHeader");
            lblDeletemsg.Value = objresource.GetString("lblDeletemsg");
            capBONUS.Text = objresource.GetString("txtBONUS");
            capCLAIM_RATIO.Text = objresource.GetString("txtCLAIM_RATIO");
            capCoApplicant.Text = objresource.GetString("capCoApplicant");
            capCLASS_FIELD.Text = objresource.GetString("cmbCLASS_FIELD");
            capLOCATION_NUMBER.Text = objresource.GetString("capLOCATION_NUMBER");
            capITEM_NUMBER.Text = objresource.GetString("capITEM_NUMBER");
            capACTUAL_INSURED_OBJECT.Text = objresource.GetString("txtACTUAL_INSURED_OBJECT");
            capMRI_VALUE.Text = objresource.GetString("capMRI_VALUE");
            //Shikha
            capPortableEquipment.Text = objresource.GetString("cmbPORTABLE_EQUIPMENT");
            capExceededPremium.Text = objresource.GetString("cmbExceeded_Premium");
        } //Set caption from resource file
        #endregion

        private void SetErrorMessages()
        {
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            rfvLOCATION.ErrorMessage = ClsMessages.FetchGeneralMessage("544");

            revBONUS.ValidationExpression = aRegExpDoublePositiveWithZero;// aRegExpPositiveCurrency;
            
            revBONUS.ErrorMessage = ClsMessages.FetchGeneralMessage("492");

            revCLAIM_RATIO.ValidationExpression = aRegExpDoublePositiveWithZero;// aRegExpPositiveCurrency;
            
            revCLAIM_RATIO.ErrorMessage = ClsMessages.FetchGeneralMessage("492");

            revVALUE_AT_RISK.ValidationExpression = aRegExpPositiveCurrency;
            revVALUE_AT_RISK.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");

            revMAXIMUM_LIMIT.ValidationExpression = aRegExpPositiveCurrency;
            revMAXIMUM_LIMIT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");

            revPOSSIBLE_MAX_LOSS.ValidationExpression = aRegExpPositiveCurrency;//aRegExpCurrency;
            revPOSSIBLE_MAX_LOSS.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");
            
            //revPARKING_SPACES
           // revPARKING_SPACES.ValidationExpression = aRegExpInteger;
          //  revPARKING_SPACES.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");


            revCONTENTS_VALUE.ValidationExpression = aRegExpPositiveCurrency;
           revCONTENTS_VALUE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");

           revRAW_MATERIAL_VALUE.ValidationExpression = aRegExpPositiveCurrency;
           revRAW_MATERIAL_VALUE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");

           revBUILDING_VALUE.ValidationExpression = aRegExpPositiveCurrency;
           revBUILDING_VALUE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");

           revMRI_VALUE.ValidationExpression = aRegExpPositiveCurrency;
           revMRI_VALUE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");
           rfvCO_APPLICANT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1179");
          // cmpvITEM_NUMBER.ErrorMessage = ClsMessages.FetchGeneralMessage("1326");

            // for itrack 691
           revLOCATION_NUMBER.ValidationExpression = aRegExpDoublePositiveNonZero;
           revLOCATION_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");
           revITEM_NUMBER.ValidationExpression = aRegExpDoublePositiveNonZero;
           revITEM_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");
           rfvLOCATION_NUMBER.ErrorMessage = ClsMessages.FetchGeneralMessage("1510");
           rfvITEM_NUMBER.ErrorMessage = ClsMessages.FetchGeneralMessage("1511");
           //Added by Pradeep itrack 837 on 03/03/2011
           csvBONUS.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1455");
           csvCLAIM_RATIO.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1456");
            //Added till here 
          // rfvACTIVITY_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");
         //  rfvOCCUPIED_AS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");
           rfvPORTABLE_EQUIPMENT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1883");

           //Added by Pradeep itrack 1512/TFS#240
           csvVALUE_AT_RISK.ErrorMessage = ClsMessages.FetchGeneralMessage("1776");
           csvMAXIMUM_LIMIT.ErrorMessage = ClsMessages.FetchGeneralMessage("1776");
            //Added till here 
        }

        private void PopulateCoApplicant()
        {
            ClsProductLocationInfo ProductLocationInfo = new ClsProductLocationInfo();
            DataTable dt;
            dt= ProductLocationInfo.FetchApplicants(Convert.ToInt32(hidCUSTOMER_ID.Value), Convert.ToInt32(hidPOLICY_VERSION_ID.Value), Convert.ToInt32(hidPOLICY_ID.Value)).Tables[0];
            
            DataView dv = dt.DefaultView;
            dv.Sort = "APPLICANT_NAME";
            cmbCO_APPLICANT_ID.DataSource = dv;
            cmbCO_APPLICANT_ID.DataTextField = "APPLICANT_NAME";
            cmbCO_APPLICANT_ID.DataValueField = "APPLICANT_ID";
            cmbCO_APPLICANT_ID.DataBind();
            cmbCO_APPLICANT_ID.Items.Insert(0, "");
            // changes by praveer for itrack no 900
            string ApplicantID = "";
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["IS_PRIMARY_APPLICANT"].ToString() == "1")
                {
                    ApplicantID = dr["APPLICANT_ID"].ToString();
                    break;
                }
            }
            if (ApplicantID != "" && ApplicantID != null)
            {
                cmbCO_APPLICANT_ID.SelectedValue = ApplicantID;
            }
        }
        private void BindMultiDeductible()
        {           
            cmbMULTIPLE_DEDUCTIBLE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
            cmbMULTIPLE_DEDUCTIBLE.DataTextField = "LookupDesc";
            cmbMULTIPLE_DEDUCTIBLE.DataValueField = "LookupID";
            cmbMULTIPLE_DEDUCTIBLE.DataBind();
            cmbMULTIPLE_DEDUCTIBLE.Items.Insert(0, "");
        }//private void BindMultiDeductible()

        private void BindPORTABLE_EQUIPMENT()
        {
            cmbPORTABLE_EQUIPMENT.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
            cmbPORTABLE_EQUIPMENT.DataTextField = "LookupDesc";
            cmbPORTABLE_EQUIPMENT.DataValueField = "LookupID";
            cmbPORTABLE_EQUIPMENT.DataBind();
            cmbPORTABLE_EQUIPMENT.Items.Insert(0, "");
        }

        private void BindExceededPremium()
        {
            cmbExceeded_Premium.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
            cmbExceeded_Premium.DataTextField = "LookupDesc";
            cmbExceeded_Premium.DataValueField = "LookupID";
            cmbExceeded_Premium.DataBind();
        }

        private void BindClassFieldForCompreensiveCondominium()
        {
            cmbCLASS_FIELD.Items.Clear();
            DataTable dt;
            if (Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CLDFCM").Select("", "LookupDesc").Length > 0)
                dt = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CLDFCM").Select("", "LookupDesc").CopyToDataTable<DataRow>();
            else
                dt = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CLDFCM");//GetLookup("CLDFCM");
    
            DataView dv = dt.DefaultView;
            dv.Sort = "LookupDesc";
            cmbCLASS_FIELD.DataSource = dv;
            cmbCLASS_FIELD.DataTextField = "LookupDesc";
            cmbCLASS_FIELD.DataValueField = "LookupID";
            cmbCLASS_FIELD.DataBind();
            cmbCLASS_FIELD.Items.Insert(0, "");
        }
        private void BindClassFieldForCompreensiveCompany()
        {
            cmbCLASS_FIELD.Items.Clear();
            DataTable dt;
            if (Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CLDFCC").Select("", "LookupDesc").Length > 0)
                dt = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CLDFCC").Select("", "LookupDesc").CopyToDataTable<DataRow>();
            else
                dt = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CLDFCC");//GetLookup("CLDFCC");

            DataView dv = dt.DefaultView;
            dv.Sort = "LookupDesc";
            cmbCLASS_FIELD.DataSource = dv;
            cmbCLASS_FIELD.DataTextField = "LookupDesc";
            cmbCLASS_FIELD.DataValueField = "LookupID";
            cmbCLASS_FIELD.DataBind();
            cmbCLASS_FIELD.Items.Insert(0, "");
        }
        private void BindConstruction()
        {
            DataTable dt;
            if (Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CONTYP").Select("", "LookupDesc").Length > 0)
                dt= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CONTYP").Select("", "LookupDesc").CopyToDataTable<DataRow>(); //Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CONTYP").Select("", "LookupDesc");
            else
                dt = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CONTYP");
                DataView dv = dt.DefaultView;
                dv.Sort = "LookupDesc";
                cmbCONSTRUCTION.DataSource = dv;
                cmbCONSTRUCTION.DataTextField = "LookupDesc";
                cmbCONSTRUCTION.DataValueField = "LookupID";
                cmbCONSTRUCTION.DataBind();
                cmbCONSTRUCTION.Items.Insert(0, "");
        }//private void BindConstruction()
        //private void BindOccupiedAs()
        //{
        //    cmbOCCUPIED_AS.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("OCDAS");
        //    cmbOCCUPIED_AS.DataTextField = "LookupDesc";
        //    cmbOCCUPIED_AS.DataValueField = "LookupID";
        //    cmbOCCUPIED_AS.DataBind();
        //    cmbOCCUPIED_AS.Items.Insert(0, "");
        //}//private void BindOccupiedAs()
        //private void BindActivityType()
        //{
        //    Bind Activity Type 
        //    cmbACTIVITY_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("ACTTE");
        //    cmbACTIVITY_TYPE.DataTextField = "LookupDesc";
        //    cmbACTIVITY_TYPE.DataValueField = "LookupID";
        //    cmbACTIVITY_TYPE.DataBind();
        //    cmbACTIVITY_TYPE.Items.Insert(0, "");
        //}//private void BindActivityType()
        private void BindOccupiedAs(string _rubrica, int OCCUPIED_ID)
        {
            //Modified for itrack#1152 / TFS# 2598
            DataSet ds = Cms.BusinessLayer.BlApplication.ClsLocation.SELECTOCCUPIEDAS(_rubrica, OCCUPIED_ID, "RUBRICA");
            DataView dv = ds.Tables[0].DefaultView;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
                hidRUBRICA_ID.Value = ds.Tables[0].Rows[0]["RUBRICA"].ToString();

            dv.Sort = "OCCUPIED_AS";
            cmbOCCUPIED_AS.Items.Clear();
            cmbOCCUPIED_AS.DataSource = dv;
            cmbOCCUPIED_AS.DataTextField = "OCCUPIED_AS";
            cmbOCCUPIED_AS.DataValueField = "OCCUPIED_ID";
            cmbOCCUPIED_AS.DataBind();
            cmbOCCUPIED_AS.Items.Insert(0, "");
            cmbOCCUPIED_AS.SelectedIndex = 0;
        }
        private void BindActivityType()
        {
            DataTable dt = Cms.BusinessLayer.BlApplication.ClsLocation.SELECTACTIVITYTYPE();
            //DataView dv = dt.DefaultView;            
            DataView dv = new DataView(dt, "TYPE=11109", "ACTIVITY_DESC", DataViewRowState.CurrentRows);
            dv.Sort = "ACTIVITY_DESC";
            cmbACTIVITY_TYPE.Items.Clear();
            cmbACTIVITY_TYPE.DataSource = dv;
            cmbACTIVITY_TYPE.DataTextField = "ACTIVITY_DESC";
            cmbACTIVITY_TYPE.DataValueField = "ACTIVITY_ID_RUBRICA"; //Modified for itrack#1152 / TFS# 2598
            cmbACTIVITY_TYPE.DataBind();
            cmbACTIVITY_TYPE.Items.Insert(0, "");
            cmbACTIVITY_TYPE.SelectedIndex = 0;
        }

        private void BindLocationForUpdate()
        {
            objNamedPerils.GeLocationNumNAddress(cmbLOCATION, int.Parse(GetCustomerID()));
            //cmbLOCATION.Enabled = false;
        }//private void BindLocation()
        private void BindLocation()
        {
            objProduct.GetLocationDetailsForPolProductsinfo(cmbLOCATION, int.Parse(GetCustomerID()), int.Parse(GetPolicyID()));
            //objNamedPerils.GeLocationNumNAddress(cmbLOCATION, int.Parse(GetCustomerID()));
        }//private void BindLocation()

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int returnvalue = 0;
            ClsProductLocationInfo objLocationInfo;
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            try
            {

                if (strRowId.ToUpper().Trim().Equals("NEW"))
                { //Add New Record

                    objLocationInfo = new ClsProductLocationInfo();
                    this.GetFormValue(objLocationInfo);
                    objLocationInfo.CUSTOMER_ID.CurrentValue = Convert.ToInt32(hidCUSTOMER_ID.Value);
                    objLocationInfo.POLICY_ID.CurrentValue = Convert.ToInt32(hidPOLICY_ID.Value);
                    objLocationInfo.POLICY_VERSION_ID.CurrentValue = Convert.ToInt32(hidPOLICY_VERSION_ID.Value);

                    objLocationInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objLocationInfo.CREATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);
                    objLocationInfo.IS_ACTIVE.CurrentValue = "Y";

                    returnvalue = objProduct.AddProcductLocationInfo(objLocationInfo, Convert.ToString(hidCALLED_FROM.Value));
                    if (returnvalue > 0)
                    {
                        this.GetOldDataObject(objLocationInfo.PRODUCT_RISK_ID.CurrentValue);
                        hidPRODUCT_RISK_ID.Value = Convert.ToString(objLocationInfo.PRODUCT_RISK_ID.CurrentValue);
                        hidFormSaved.Value = "1";
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        btnActivateDeactivate.Enabled = true;
                        btnDelete.Enabled = true;
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objLocationInfo.IS_ACTIVE.CurrentValue.Trim());
                        strRowId = hidPRODUCT_RISK_ID.Value;
                    }
                    else if (returnvalue == -1)
                    {
                        hidFormSaved.Value = "2";
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                    }
                    else if (returnvalue == -5)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("1326");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";

                    }


                }
                else
                {  //Update

                    objLocationInfo = (ClsProductLocationInfo)base.GetPageModelObject();
                    this.GetFormValue(objLocationInfo);
                    objLocationInfo.IS_ACTIVE.CurrentValue = "Y";
                    objLocationInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objLocationInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);


                    returnvalue = objProduct.UpdateProcductLocationInfo(objLocationInfo, Convert.ToString(hidCALLED_FROM.Value));  //Call Business Layer Function for Update

                    if (returnvalue > 0)
                    {
                        this.GetOldDataObject(objLocationInfo.PRODUCT_RISK_ID.CurrentValue);
                        hidFormSaved.Value = "1";
                        hidPRODUCT_RISK_ID.Value = objLocationInfo.PRODUCT_RISK_ID.CurrentValue.ToString();
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                    }
                    else if (returnvalue == -1)
                    {
                        hidFormSaved.Value = "2";
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");

                    }
                    else if (returnvalue == -5)
                    {
                        this.GetOldDataObject(objLocationInfo.PRODUCT_RISK_ID.CurrentValue);
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("1326");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";

                    }
                }
                base.OpenEndorsementDetails();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.FetchGeneralMessage("21") + ":-" + ex.Message;
                hidFormSaved.Value = "2";
            }
            lblDelete.Text = "";
        }

        private void GetFormValue(ClsProductLocationInfo objLocationInfo)
        {
            objLocationInfo.ORIGINAL_VERSION_ID.CurrentValue = 0;// Convert.ToInt32(hidPOLICY_VERSION_ID.Value);
            if (hidPRODUCT_RISK_ID.Value != "0")
            {
                strRowId = hidPRODUCT_RISK_ID.Value;
            }
            else { strRowId = "NEW"; }

            if (cmbLOCATION.SelectedValue != "")
                objLocationInfo.LOCATION.CurrentValue = int.Parse(cmbLOCATION.SelectedValue);
            else
                objLocationInfo.LOCATION.CurrentValue = base.GetEbixIntDefaultValue();

            if (PAGEFROM == "QAPP")
            {
                objLocationInfo.LOCATION.CurrentValue = 0;
            }

            if (txtVALUE_AT_RISK.Text != "")
                objLocationInfo.VALUE_AT_RISK.CurrentValue = double.Parse(txtVALUE_AT_RISK.Text, numberFormatInfo);
            else
                objLocationInfo.VALUE_AT_RISK.CurrentValue = GetEbixDoubleDefaultValue();


            if (txtBUILDING_VALUE.Text != "")
                objLocationInfo.BUILDING_VALUE.CurrentValue = double.Parse(txtBUILDING_VALUE.Text, numberFormatInfo);
            else
                objLocationInfo.BUILDING_VALUE.CurrentValue = GetEbixDoubleDefaultValue();

            if (txtCONTENTS_VALUE.Text != "")
                objLocationInfo.CONTENTS_VALUE.CurrentValue = double.Parse(txtCONTENTS_VALUE.Text, numberFormatInfo);
            else
                objLocationInfo.CONTENTS_VALUE.CurrentValue = GetEbixDoubleDefaultValue();

            if (txtRAW_MATERIAL_VALUE.Text != "")
                objLocationInfo.RAW_MATERIAL_VALUE.CurrentValue = double.Parse(txtRAW_MATERIAL_VALUE.Text, numberFormatInfo);
            else
                objLocationInfo.RAW_MATERIAL_VALUE.CurrentValue = GetEbixDoubleDefaultValue();

            if (chkCONTENTS_RAW_VALUES.Checked)
                objLocationInfo.CONTENTS_RAW_VALUES.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.YES);
            else
                objLocationInfo.CONTENTS_RAW_VALUES.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.NO);
            
            if (txtMRI_VALUE.Text != "")
                objLocationInfo.MRI_VALUE.CurrentValue = double.Parse(txtMRI_VALUE.Text, numberFormatInfo);
            else
                objLocationInfo.MRI_VALUE.CurrentValue = GetEbixDoubleDefaultValue();

            if (txtMAXIMUM_LIMIT.Text != "")
                objLocationInfo.MAXIMUM_LIMIT.CurrentValue = double.Parse(txtMAXIMUM_LIMIT.Text, numberFormatInfo);
            else
                objLocationInfo.MAXIMUM_LIMIT.CurrentValue = GetEbixDoubleDefaultValue();

            if (txtPOSSIBLE_MAX_LOSS.Text != "")
                objLocationInfo.POSSIBLE_MAX_LOSS.CurrentValue = double.Parse(txtPOSSIBLE_MAX_LOSS.Text, numberFormatInfo);
            else
                objLocationInfo.POSSIBLE_MAX_LOSS.CurrentValue = GetEbixDoubleDefaultValue();

            if (cmbMULTIPLE_DEDUCTIBLE.SelectedValue != "")
                objLocationInfo.MULTIPLE_DEDUCTIBLE.CurrentValue = int.Parse(cmbMULTIPLE_DEDUCTIBLE.SelectedValue);
            else
                objLocationInfo.MULTIPLE_DEDUCTIBLE.CurrentValue = GetEbixIntDefaultValue();


            //shikha
            if (cmbPORTABLE_EQUIPMENT.SelectedValue != "")
                objLocationInfo.PORTABLE_EQUIPMENT.CurrentValue = int.Parse(cmbPORTABLE_EQUIPMENT.SelectedValue);
            else
                objLocationInfo.PORTABLE_EQUIPMENT.CurrentValue = GetEbixIntDefaultValue();


            if (cmbExceeded_Premium.SelectedValue != "")
                objLocationInfo.EXCEEDED_PREMIUM.CurrentValue = int.Parse(cmbExceeded_Premium.SelectedValue);
            else
                objLocationInfo.EXCEEDED_PREMIUM.CurrentValue = GetEbixIntDefaultValue();


            if (txtPARKING_SPACES.Text != "")
                objLocationInfo.PARKING_SPACES.CurrentValue = txtPARKING_SPACES.Text;
            else
                objLocationInfo.PARKING_SPACES.CurrentValue = String.Empty;
            
            //Modified for itrack#1152/TFS#2598
            if (cmbACTIVITY_TYPE.SelectedValue != "")
                objLocationInfo.ACTIVITY_TYPE.CurrentValue = int.Parse(hidACTIVITY_TYPE.Value);
            else
                objLocationInfo.ACTIVITY_TYPE.CurrentValue = GetEbixIntDefaultValue();


            //Modified for itrack#1152/TFS#2598
            if (hidOCCUPIED.Value != "")
                objLocationInfo.OCCUPIED_AS.CurrentValue = int.Parse(hidOCCUPIED.Value);
            else
                objLocationInfo.OCCUPIED_AS.CurrentValue = GetEbixIntDefaultValue();

            if (cmbCONSTRUCTION.SelectedValue != "")
                objLocationInfo.CONSTRUCTION.CurrentValue = int.Parse(cmbCONSTRUCTION.SelectedValue);
            else
                objLocationInfo.CONSTRUCTION.CurrentValue = GetEbixIntDefaultValue();


            if (txtRUBRICA.Text != "")
                objLocationInfo.RUBRICA.CurrentValue = txtRUBRICA.Text.ToString();
            else
                objLocationInfo.RUBRICA.CurrentValue = String.Empty;

            if (chkASSIST24.Checked)
               objLocationInfo.ASSIST24.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.YES);
            else 
               objLocationInfo.ASSIST24.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.NO);

            if (txtREMARKS.Text != "")
                objLocationInfo.REMARKS.CurrentValue = txtREMARKS.Text;
            else
                objLocationInfo.REMARKS.CurrentValue = String.Empty;
            
            if (txtCLAIM_RATIO.Text != "")
                objLocationInfo.CLAIM_RATIO.CurrentValue = double.Parse(txtCLAIM_RATIO.Text, numberFormatInfo);
            else
                objLocationInfo.CLAIM_RATIO.CurrentValue = GetEbixDoubleDefaultValue();

            if (txtBONUS.Text != "")
                objLocationInfo.BONUS.CurrentValue = double.Parse(txtBONUS.Text, numberFormatInfo);
            else
                objLocationInfo.BONUS.CurrentValue = GetEbixDoubleDefaultValue();

            if (cmbCO_APPLICANT_ID.SelectedValue != "")
                objLocationInfo.CO_APPLICANT_ID.CurrentValue = Convert.ToInt32(cmbCO_APPLICANT_ID.SelectedValue);
            else
                objLocationInfo.CO_APPLICANT_ID.CurrentValue = base.GetEbixIntDefaultValue();
            
            if (cmbCLASS_FIELD.SelectedValue != "")
                objLocationInfo.CLASS_FIELD.CurrentValue = Convert.ToInt32(cmbCLASS_FIELD.SelectedValue);
            else
                objLocationInfo.CLASS_FIELD.CurrentValue = base.GetEbixIntDefaultValue();

            if (txtLOCATION_NUMBER.Text != "") {
                objLocationInfo.LOCATION_NUMBER.CurrentValue = int.Parse(txtLOCATION_NUMBER.Text);
            }
            if (txtITEM_NUMBER.Text != "")
            {
                objLocationInfo.ITEM_NUMBER.CurrentValue = int.Parse(txtITEM_NUMBER.Text);
            }
            if (txtACTUAL_INSURED_OBJECT.Text != "")
                objLocationInfo.ACTUAL_INSURED_OBJECT.CurrentValue = txtACTUAL_INSURED_OBJECT.Text;
            else
                objLocationInfo.ACTUAL_INSURED_OBJECT.CurrentValue = String.Empty;
        }

        private void GetOldDataObject(int PRODUCT_RISK_ID)
        {
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            ClsProductLocationInfo objProductLocationInfo = new ClsProductLocationInfo();
            
            objProductLocationInfo.PRODUCT_RISK_ID.CurrentValue = PRODUCT_RISK_ID;
            objProductLocationInfo.CUSTOMER_ID.CurrentValue = int.Parse(GetCustomerID());
            objProductLocationInfo.POLICY_ID.CurrentValue = int.Parse(GetPolicyID());
            objProductLocationInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(GetPolicyVersionID());
            string policystatus = GetPolicyStatus();
           // this.BindLocationForUpdate();
            //btnSelect.Enabled = false;
            if (objProduct.FetchProductLocationInfo(ref objProductLocationInfo))
            {
               

                btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objProductLocationInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                // itrack no 867
                string originalversion = objProductLocationInfo.ORIGINAL_VERSION_ID.CurrentValue.ToString();
                if (originalversion != GetPolicyVersionID() && policystatus == "UENDRS" && originalversion != "0")
                {
                    btnActivateDeactivate.Visible = false;
                }

                if (objProductLocationInfo.LOCATION.CurrentValue != -1 && objProductLocationInfo.LOCATION.CurrentValue != 0)
                {
                    hidLOCATION.Value = objProductLocationInfo.LOCATION.CurrentValue.ToString();
                    this.BindLocationForUpdate();
                    cmbLOCATION.Enabled = false;
                    btnSelect.Enabled = false;
                    PopulatePageFromEbixModelObject(this.Page, objProductLocationInfo);
                    this.ConvertPageControlUsingPolicyCulure();
                }
                else
                {
                    cmbLOCATION.Enabled = true;
                    btnSelect.Enabled = true;
                    PopulatePageFromEbixModelObject(this.Page, objProductLocationInfo);
                    this.ConvertPageControlUsingPolicyCulure();
                }
                
                    if(objProductLocationInfo.BONUS.CurrentValue.ToString()!="-1,0" && objProductLocationInfo.BONUS.CurrentValue!=-1.0 )
                        txtBONUS.Text = objProductLocationInfo.BONUS.CurrentValue.ToString();
                    if (objProductLocationInfo.CLAIM_RATIO.CurrentValue.ToString() != "-1,0" && objProductLocationInfo.CLAIM_RATIO.CurrentValue != -1.0)
                        txtCLAIM_RATIO.Text = objProductLocationInfo.CLAIM_RATIO.CurrentValue.ToString();

                    //Added for itrack#1152/TFS# 2598
                    hidOCCUPIED.Value = objProductLocationInfo.OCCUPIED_AS.CurrentValue.ToString();
                    hidACTIVITY_TYPE.Value = objProductLocationInfo.ACTIVITY_TYPE.CurrentValue.ToString();
                    this.BindOccupiedAs("", objProductLocationInfo.OCCUPIED_AS.CurrentValue);
                    
                    string _activity_type = objProductLocationInfo.ACTIVITY_TYPE.CurrentValue.ToString();
                    if (_activity_type != "")
                        cmbACTIVITY_TYPE.SelectedIndex = cmbACTIVITY_TYPE.Items.IndexOf(cmbACTIVITY_TYPE.Items.FindByValue(_activity_type + "^" + hidRUBRICA_ID.Value));
                    //Added till here 

                base.SetPageModelObject(objProductLocationInfo);
            }// if (objProduct.FetchProductLocationInfo(ref objProductLocationInfo))

        }
        //Added for itrack#1152 / TFS# 2598
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet AjaxGetOccupied(string _rubrica)
        {
            try
            {
                DataSet ds = Cms.BusinessLayer.BlApplication.ClsLocation.SELECTOCCUPIEDAS(_rubrica, 0, "");
                return ds;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// This fuction is overloaded page control data, format the data based on the policy culture
        /// </summary>
        private void ConvertPageControlUsingPolicyCulure()
        {
            numberFormatInfo.NumberDecimalDigits = 2;
            if(txtVALUE_AT_RISK.Text.Trim()!=String.Empty)
                txtVALUE_AT_RISK.Text = Convert.ToDouble(txtVALUE_AT_RISK.Text,numberFormatInfo).ToString("N", numberFormatInfo);
            if(txtMAXIMUM_LIMIT.Text.Trim()!=String.Empty)
                txtMAXIMUM_LIMIT.Text = Convert.ToDouble(txtMAXIMUM_LIMIT.Text, numberFormatInfo).ToString("N", numberFormatInfo);
            if(txtPOSSIBLE_MAX_LOSS.Text.Trim()!=String.Empty)
                txtPOSSIBLE_MAX_LOSS.Text = Convert.ToDouble(txtPOSSIBLE_MAX_LOSS.Text, numberFormatInfo).ToString("N", numberFormatInfo);
          
            if(txtCONTENTS_VALUE.Text.Trim()!=String.Empty)
                txtCONTENTS_VALUE.Text = Convert.ToDouble(txtCONTENTS_VALUE.Text, numberFormatInfo).ToString("N", numberFormatInfo);
            if(txtRAW_MATERIAL_VALUE.Text.Trim()!=String.Empty)
                txtRAW_MATERIAL_VALUE.Text = Convert.ToDouble(txtRAW_MATERIAL_VALUE.Text, numberFormatInfo).ToString("N", numberFormatInfo);
            if(txtBUILDING_VALUE.Text.Trim()!=String.Empty)
                txtBUILDING_VALUE.Text = Convert.ToDouble(txtBUILDING_VALUE.Text, numberFormatInfo).ToString("N", numberFormatInfo);
            if(txtMRI_VALUE.Text.Trim()!=String.Empty)
                txtMRI_VALUE.Text = Convert.ToDouble(txtMRI_VALUE.Text, numberFormatInfo).ToString("N", numberFormatInfo);
            if (txtCLAIM_RATIO.Text.Trim() != String.Empty)
                txtCLAIM_RATIO.Text = Convert.ToDouble(txtCLAIM_RATIO.Text, numberFormatInfo).ToString("N", numberFormatInfo);
            if (txtBONUS.Text.Trim() != String.Empty)
                txtBONUS.Text = Convert.ToDouble(txtBONUS.Text, numberFormatInfo).ToString("N", numberFormatInfo);
 
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            ClsProductLocationInfo objProductLocationInfo;
            int retunvalue = 0;
            try
            {
                String ConfirmValue = String.Empty;

                if (hidPRODUCT_RISK_ID.Value != "" && hidConfirmValue.Value != "" && hidConfirmValue.Value != "undefined")
                {
                    ConfirmValue = hidConfirmValue.Value.ToString();

                    objProductLocationInfo = (ClsProductLocationInfo)base.GetPageModelObject();
                    objProductLocationInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());

                    objProductLocationInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);
                    objProductLocationInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);
                    objProductLocationInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);
                    objProductLocationInfo.PRODUCT_RISK_ID.CurrentValue = int.Parse(hidPRODUCT_RISK_ID.Value);
                    retunvalue = objProduct.DeleteProcductLocationInfo(objProductLocationInfo, Convert.ToString(hidCALLED_FROM.Value), Convert.ToInt32(ConfirmValue));
                    if (retunvalue > 0)
                    {
                        lblDelete.Text = ClsMessages.FetchGeneralMessage("127");
                        tbody.Attributes.Add("style", "display:none");
                        hidFormSaved.Value = "1";
                        hidPRODUCT_RISK_ID.Value = "";

                    }
                    else if (retunvalue == -1)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("128");
                        hidFormSaved.Value = "2";
                    }
                }
            }
            catch (Exception ex) 
            {
                lblMessage.Text = ClsMessages.FetchGeneralMessage("128");
                hidFormSaved.Value = "2";
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            

        } //Delete Product Location Information

        protected void btnActivateDeactivate_Click(object sender, EventArgs e)
        {
            int retunvalue = 0;
            ClsProductLocationInfo objProductLocationInfo;
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            try {
                if (int.TryParse(hidPRODUCT_RISK_ID.Value, out PRO_RISK_ID)) 
                {
                    objProductLocationInfo = (ClsProductLocationInfo)base.GetPageModelObject();
                    if (objProductLocationInfo.IS_ACTIVE.CurrentValue == "Y")
                        objProductLocationInfo.IS_ACTIVE.CurrentValue = "N";
                    else
                        objProductLocationInfo.IS_ACTIVE.CurrentValue = "Y";

                    objProductLocationInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objProductLocationInfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now);
                    retunvalue = objProduct.ActivateDeactivateProcductLocationInfo(objProductLocationInfo, Convert.ToString(hidCALLED_FROM.Value));
                    if (retunvalue > 0) 
                    {
                        if (objProductLocationInfo.IS_ACTIVE.CurrentValue == "N")
                        {
                            lblMessage.Text = ClsMessages.FetchGeneralMessage("9");
                           // btnActivateDeactivate.Visible = false;
                        }
                        else if (objProductLocationInfo.IS_ACTIVE.CurrentValue == "Y")
                        {
                            lblMessage.Text = ClsMessages.FetchGeneralMessage("7");
                        }
                        hidFormSaved.Value = "1";
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objProductLocationInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                        SetPageModelObject(objProductLocationInfo);
                    }
                    else if (retunvalue == -5)
                    {
                        this.GetOldDataObject(objProductLocationInfo.PRODUCT_RISK_ID.CurrentValue);
                        hidFormSaved.Value = "2";
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("1326");
                    }
                }
            }
            catch { }

        }    //Activate Deactivate the Information

        private void PopulatePageControlWithDefaultValue(DataSet ds)
        {
            if (ds.Tables[0].Rows.Count != 0)
            {
                if (ds.Tables[0].Rows[0]["CATEGORY"].ToString() != "")
                    txtRUBRICA.Text = ds.Tables[0].Rows[0]["CATEGORY"].ToString();
                else if (ds.Tables[0].Rows[0]["CATEGORY"].ToString() == "")
                    txtRUBRICA.Text = "";

                //Added for itrack#1152/TFS# 2598
                int OCCUPIED = 0;
                if (int.TryParse(ds.Tables[0].Rows[0]["OCCUPIED"].ToString(), out OCCUPIED))
                    this.BindOccupiedAs("", OCCUPIED);

                string _activity_type = ds.Tables[0].Rows[0]["ACTIVITY_TYPE"].ToString();
                if (_activity_type != "")
                    cmbACTIVITY_TYPE.SelectedIndex = cmbACTIVITY_TYPE.Items.IndexOf(cmbACTIVITY_TYPE.Items.FindByValue(_activity_type + "^" + hidRUBRICA_ID.Value));
                //Added till here 

                /*Commented for itrack#1152/TFS# 2598
                if (ds.Tables[0].Rows[0]["ACTIVITY_TYPE"].ToString() != "" && ds.Tables[0].Rows[0]["ACTIVITY_TYPE"].ToString() != "0")
                    cmbACTIVITY_TYPE.SelectedIndex = cmbACTIVITY_TYPE.Items.IndexOf(cmbACTIVITY_TYPE.Items.FindByValue(ds.Tables[0].Rows[0]["ACTIVITY_TYPE"].ToString()));

                else if (ds.Tables[0].Rows[0]["ACTIVITY_TYPE"].ToString() == "0")
                    cmbACTIVITY_TYPE.SelectedIndex = cmbACTIVITY_TYPE.Items.IndexOf(cmbACTIVITY_TYPE.Items.FindByValue(ds.Tables[0].Rows[0]["ACTIVITY_TYPE"].ToString()));
                 */

                if (ds.Tables[0].Rows[0]["OCCUPIED"].ToString() != "" && ds.Tables[0].Rows[0]["OCCUPIED"].ToString() != "0")
                    cmbOCCUPIED_AS.SelectedIndex = cmbOCCUPIED_AS.Items.IndexOf(cmbOCCUPIED_AS.Items.FindByValue(ds.Tables[0].Rows[0]["OCCUPIED"].ToString()));

                else if (ds.Tables[0].Rows[0]["OCCUPIED"].ToString() == "0")
                    cmbOCCUPIED_AS.SelectedIndex = cmbOCCUPIED_AS.Items.IndexOf(cmbOCCUPIED_AS.Items.FindByValue(ds.Tables[0].Rows[0]["OCCUPIED"].ToString()));
               

                if (ds.Tables[0].Rows[0]["CONSTRUCTION"].ToString() != "" && ds.Tables[0].Rows[0]["CONSTRUCTION"].ToString() != "0")
                    cmbCONSTRUCTION.SelectedIndex = cmbCONSTRUCTION.Items.IndexOf(cmbCONSTRUCTION.Items.FindByValue(ds.Tables[0].Rows[0]["CONSTRUCTION"].ToString()));
                else if (ds.Tables[0].Rows[0]["CONSTRUCTION"].ToString() == "0")
                    cmbCONSTRUCTION.SelectedIndex = cmbCONSTRUCTION.Items.IndexOf(cmbCONSTRUCTION.Items.FindByValue(ds.Tables[0].Rows[0]["CONSTRUCTION"].ToString()));


                if (ds.Tables[0].Rows[0]["DESCRIPTION"].ToString() != "")
                    txtREMARKS.Text = ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();

                else if (ds.Tables[0].Rows[0]["DESCRIPTION"].ToString() == "")
                    txtREMARKS.Text = "";

                 
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            //Proc_FetchLocationDetails
            if (cmbLOCATION.SelectedValue != "")
            {
                DataSet ds = objProduct.FetchLocationDataUsingLocationID(int.Parse(hidCUSTOMER_ID.Value), int.Parse(cmbLOCATION.SelectedValue));

                PopulatePageControlWithDefaultValue(ds);


            }
        }
    }
}
