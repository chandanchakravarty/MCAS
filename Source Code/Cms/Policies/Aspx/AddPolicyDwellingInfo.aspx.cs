//------------------------------------------------------------------------------
// Created by Pradeep Kushwaha on 18-05-2010
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
    public partial class AddPolicyDwellingInfo : Cms.Policies.policiesbase
    {
        ResourceManager objresource;
        //Declare Dropdown For Multiple Deductible dropdown

        private static String strRowId = String.Empty;
        ClsNamedPerils objNamedPerils = new ClsNamedPerils();
        ClsProducts objProduct = new ClsProducts();
        int PRO_RISK_ID;
        string CalledFrom;
        public string PAGEFROM;
        
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"].ToString() != "")
                CalledFrom = Request.QueryString["CalledFrom"].ToString();
            if (Request.QueryString["PAGEFROM"] != null)
                PAGEFROM = Request.QueryString["PAGEFROM"].ToString();
            switch (CalledFrom) 
            {
                case "DWELLING"://Dwelling product 

                    hidCALLED_FROM.Value = CalledFrom;
                    base.ScreenId = DWELLINGscreenId.LOCATION_INFORMATION;
                    hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl");
                    
                    break;
                case "ROBBERY"://Robbery product 

                    hidCALLED_FROM.Value = CalledFrom;
                    base.ScreenId = ROBBERYscreenId.LOCATION_INFORMATION;
                    hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl");

                    break;
                case "GenCvlLib"://General Civil Liability

                    base.ScreenId = GENERAL_CIVIL_LIABILITYscreenId.LOCATION_INFORMATION;
                    hidCALLED_FROM.Value = CalledFrom;
                    hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl");
                    break;
                default:  
                    base.ScreenId = "491_0";
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
            btnSelect.Text = Cms.CmsWeb.ClsMessages.FetchGeneralButtonsText("btnSelect");
            
            objresource = new System.Resources.ResourceManager("Cms.Policies.Aspx.AddPolicyDwellingInfo", System.Reflection.Assembly.GetExecutingAssembly());


            btnReset.Attributes.Add("onclick", "javascript: return ResetTheForm();");
            if (!IsPostBack)
            {
                btnDelete.Attributes.Add("onclick", "javascript:return ShowAlertMessageWhileDelete(true);");

                if (CalledFrom.ToUpper() == "DWELLING")
                {
                    trClassField.Visible = true;
                    this.BindClassFieldForCompreensiveDwelling();
                }
                else
                    trClassField.Visible = false;

                this.SetCaptions();

                this.BindMultiDeductible();
                this.BindConstruction();
                this.SetErrorMessages();
                this.BindLocation();
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
                    btnDelete.Enabled = true;
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
                    this.BindLocation();
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");            
                }

            }
            lblDelete.Text = "";
        }
        #endregion
        private void BindClassFieldForCompreensiveDwelling()
        {
            cmbCLASS_FIELD.Items.Clear();
            DataTable dt;
            if (Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CLFDDL").Select("","LookupDesc").Length > 0)
                dt = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CLFDDL").Select("", "LookupDesc").CopyToDataTable<DataRow>();
            else
                dt = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CLFDDL");//GetLookup("CLDFCC");
            //cmbCLASS_FIELD.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CLFDDL");
            DataView dv = dt.DefaultView;
            dv.Sort = "LookupDesc";
            cmbCLASS_FIELD.DataSource = dv;
            cmbCLASS_FIELD.DataTextField = "LookupDesc";
            cmbCLASS_FIELD.DataValueField = "LookupID";
            cmbCLASS_FIELD.DataBind();
            cmbCLASS_FIELD.Items.Insert(0, "");
        }
        private void BindMultiDeductible()
        {
            cmbMULTIPLE_DEDUCTIBLE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
            cmbMULTIPLE_DEDUCTIBLE.DataTextField = "LookupDesc";
            cmbMULTIPLE_DEDUCTIBLE.DataValueField = "LookupID";
            cmbMULTIPLE_DEDUCTIBLE.DataBind();
            cmbMULTIPLE_DEDUCTIBLE.Items.Insert(0, "");
        }//private void BindMultiDeductible()
        private void BindLocationForUpdate()
        {
             objNamedPerils.GeLocationNumNAddress(cmbLOCATION, int.Parse(GetCustomerID()));
             //cmbLOCATION.Enabled = false;
        }//private void BindLocation()
        private void SetErrorMessages()
        {
            rfvLOCATION.ErrorMessage = ClsMessages.FetchGeneralMessage("544");
            revBONUS.ValidationExpression = aRegExpDoublePositiveWithZero;// aRegExpPositiveCurrency;
            revBONUS.ErrorMessage = ClsMessages.FetchGeneralMessage("492");
            revCLAIM_RATIO.ValidationExpression = aRegExpDoublePositiveWithZero;// aRegExpPositiveCurrency;
            revCLAIM_RATIO.ErrorMessage = ClsMessages.FetchGeneralMessage("492");

            revVALUE_AT_RISK.ValidationExpression = aRegExpPositiveCurrency;
            revVALUE_AT_RISK.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");

            revMAXIMUM_LIMIT.ValidationExpression = aRegExpPositiveCurrency;
            revMAXIMUM_LIMIT.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");
          //  cmpvITEM_NUMBER.ErrorMessage = ClsMessages.FetchGeneralMessage("1326");
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

            //Added by Pradeep itrack 1512/TFS#240
            csvVALUE_AT_RISK.ErrorMessage = ClsMessages.FetchGeneralMessage("1776");
            csvMAXIMUM_LIMIT.ErrorMessage = ClsMessages.FetchGeneralMessage("1776");
            //Added till here 
        }
        private void GetOldDataObject(int PRODUCT_RISK_ID)
        {
            ClsProductLocationInfo objProductLocationInfo = new ClsProductLocationInfo();

            objProductLocationInfo.PRODUCT_RISK_ID.CurrentValue = PRODUCT_RISK_ID;
            objProductLocationInfo.CUSTOMER_ID.CurrentValue = int.Parse(GetCustomerID());
            objProductLocationInfo.POLICY_ID.CurrentValue = int.Parse(GetPolicyID());
            objProductLocationInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(GetPolicyVersionID());
            string policystatus = GetPolicyStatus();
            //this.BindLocationForUpdate();
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
                if (objProductLocationInfo.BONUS.CurrentValue.ToString() != "-1,0" && objProductLocationInfo.BONUS.CurrentValue != -1.0)
                    txtBONUS.Text = objProductLocationInfo.BONUS.CurrentValue.ToString();
                if (objProductLocationInfo.CLAIM_RATIO.CurrentValue.ToString() != "-1,0" && objProductLocationInfo.CLAIM_RATIO.CurrentValue != -1.0)
                    txtCLAIM_RATIO.Text = objProductLocationInfo.CLAIM_RATIO.CurrentValue.ToString();

                base.SetPageModelObject(objProductLocationInfo);
                
            }// if (objProduct.FetchProductLocationInfo(ref objProductLocationInfo))

        }
        #region Set Captions from resource file for multilingual
        private void SetCaptions()
        {
            
            capASSIST24.Text = objresource.GetString("chkASSIST24");
            capCONSTRUCTION.Text = objresource.GetString("cmbCONSTRUCTION");
            capLOCATION.Text = objresource.GetString("cmbLOCATION");
            capMAXIMUM_LIMIT.Text = objresource.GetString("txtMAXIMUM_LIMIT");
            capREMARKS.Text = objresource.GetString("txtREMARKS");
            capVALUE_AT_RISK.Text = objresource.GetString("txtVALUE_AT_RISK");
            lblManHeader.Text = objresource.GetString("lblManHeader");
            lblDeletemsg.Value = objresource.GetString("lblDeletemsg");
            capMULTIPLE_DEDUCTIBLE.Text = objresource.GetString("cmbMULTIPLE_DEDUCTIBLE");
            capBONUS.Text = objresource.GetString("txtBONUS");
            capCLAIM_RATIO.Text = objresource.GetString("txtCLAIM_RATIO");
            capCLASS_FIELD.Text = objresource.GetString("cmbCLASS_FIELD");
            capLOCATION_NUMBER.Text = objresource.GetString("capLOCATION_NUMBER");
            capITEM_NUMBER.Text = objresource.GetString("capITEM_NUMBER");
            capACTUAL_INSURED_OBJECT.Text = objresource.GetString("txtACTUAL_INSURED_OBJECT");
            capExceededPremium.Text = objresource.GetString("cmbExceeded_Premium");
        } //Set caption from resource file
        #endregion
        private void BindConstruction()
        {
            DataTable dt;
            if (Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CONTYP").Select("", "LookupDesc").Length > 0)
               dt = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CONTYP").Select("", "LookupDesc").CopyToDataTable<DataRow>(); //Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CONTYP").Select("", "LookupDesc");
            else
                dt= Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CONTYP");
            DataView dv = dt.DefaultView;
            dv.Sort = "LookupDesc";
            cmbCONSTRUCTION.DataSource = dv;
            cmbCONSTRUCTION.DataTextField = "LookupDesc";
            cmbCONSTRUCTION.DataValueField = "LookupID";
            cmbCONSTRUCTION.DataBind();
            cmbCONSTRUCTION.Items.Insert(0, "");
        }//private void BindConstruction()
        private void BindLocation()
        {
            objProduct.GetLocationDetailsForPolProductsinfo(cmbLOCATION, int.Parse(GetCustomerID()), int.Parse(GetPolicyID()));
        }//private void BindLocation()

        private void BindExceededPremium()
        {
            cmbExceeded_Premium.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
            cmbExceeded_Premium.DataTextField = "LookupDesc";
            cmbExceeded_Premium.DataValueField = "LookupID";
            cmbExceeded_Premium.DataBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ClsProductLocationInfo objProductLocationInfo;
            int retunvalue = 0;
            try
            {
                    String ConfirmValue = String.Empty;
               
                   if (hidPRODUCT_RISK_ID.Value != "" && hidConfirmValue.Value != "" && hidConfirmValue.Value!="undefined")
                   {
                    ConfirmValue = hidConfirmValue.Value.ToString();

                    objProductLocationInfo = (ClsProductLocationInfo)base.GetPageModelObject();
                    objProductLocationInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());

                    objProductLocationInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);
                    objProductLocationInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);
                    objProductLocationInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);
                    objProductLocationInfo.PRODUCT_RISK_ID.CurrentValue = int.Parse(hidPRODUCT_RISK_ID.Value);
                    if (cmbLOCATION.SelectedValue.ToString() != "")
                        objProductLocationInfo.LOCATION.CurrentValue = int.Parse(cmbLOCATION.SelectedValue);
                 
                    retunvalue = objProduct.DeleteProcductLocationInfo(objProductLocationInfo, Convert.ToString(hidCALLED_FROM.Value),Convert.ToInt32(ConfirmValue));
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
            
        }
        private void GetFormValue(ClsProductLocationInfo objLocationInfo)
        {
            numberFormatInfo.NumberDecimalDigits = 2;
            objLocationInfo.ORIGINAL_VERSION_ID.CurrentValue = 0;// Convert.ToInt32(hidPOLICY_VERSION_ID.Value);
            if (hidPRODUCT_RISK_ID.Value != "0")
            {
                strRowId = hidPRODUCT_RISK_ID.Value;
            }
            else { strRowId = "NEW"; }

            if (cmbLOCATION.SelectedValue != "")
                objLocationInfo.LOCATION.CurrentValue = int.Parse(cmbLOCATION.SelectedValue);
            else
                objLocationInfo.LOCATION.CurrentValue = GetEbixIntDefaultValue();

            if (PAGEFROM == "QAPP")
            {
                objLocationInfo.LOCATION.CurrentValue = 0;
            }

            if (txtVALUE_AT_RISK.Text != "")
                objLocationInfo.VALUE_AT_RISK.CurrentValue = double.Parse(txtVALUE_AT_RISK.Text, numberFormatInfo);
            else
                objLocationInfo.VALUE_AT_RISK.CurrentValue = GetEbixDoubleDefaultValue();
            if (txtMAXIMUM_LIMIT.Text != "")
                objLocationInfo.MAXIMUM_LIMIT.CurrentValue = double.Parse(txtMAXIMUM_LIMIT.Text, numberFormatInfo);
            else
                objLocationInfo.MAXIMUM_LIMIT.CurrentValue = GetEbixDoubleDefaultValue();

            if (cmbCONSTRUCTION.SelectedValue != "")
                objLocationInfo.CONSTRUCTION.CurrentValue = int.Parse(cmbCONSTRUCTION.SelectedValue);
            else
                objLocationInfo.CONSTRUCTION.CurrentValue = GetEbixIntDefaultValue();

            if (cmbMULTIPLE_DEDUCTIBLE.SelectedValue != "")
                objLocationInfo.MULTIPLE_DEDUCTIBLE.CurrentValue = int.Parse(cmbMULTIPLE_DEDUCTIBLE.SelectedValue);
            else
                objLocationInfo.MULTIPLE_DEDUCTIBLE.CurrentValue = GetEbixIntDefaultValue();

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
            if (cmbCLASS_FIELD.SelectedValue != "")
                objLocationInfo.CLASS_FIELD.CurrentValue = Convert.ToInt32(cmbCLASS_FIELD.SelectedValue);
            else
                objLocationInfo.CLASS_FIELD.CurrentValue = base.GetEbixIntDefaultValue();

            if (txtLOCATION_NUMBER.Text != "")
            {
                objLocationInfo.LOCATION_NUMBER.CurrentValue = int.Parse(txtLOCATION_NUMBER.Text);
            }
            else

                objLocationInfo.LOCATION_NUMBER.CurrentValue = base.GetEbixIntDefaultValue();
            if (txtITEM_NUMBER.Text != "")
            {
                objLocationInfo.ITEM_NUMBER.CurrentValue = int.Parse(txtITEM_NUMBER.Text);
            }
            else
                objLocationInfo.ITEM_NUMBER.CurrentValue = base.GetEbixIntDefaultValue();
           
            if (txtACTUAL_INSURED_OBJECT.Text != "")
                objLocationInfo.ACTUAL_INSURED_OBJECT.CurrentValue = txtACTUAL_INSURED_OBJECT.Text;
            else
                objLocationInfo.ACTUAL_INSURED_OBJECT.CurrentValue = String.Empty;

            if (cmbExceeded_Premium.SelectedValue != "")
                objLocationInfo.EXCEEDED_PREMIUM.CurrentValue = int.Parse(cmbExceeded_Premium.SelectedValue);
            else
                objLocationInfo.EXCEEDED_PREMIUM.CurrentValue = GetEbixIntDefaultValue();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int returnvalue = 0;
            ClsProductLocationInfo objLocationInfo;
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
                    objLocationInfo.CO_APPLICANT_ID.CurrentValue = ClsGeneralInformation.GetPolicyPrimary_Applicant(objLocationInfo.CUSTOMER_ID.CurrentValue, objLocationInfo.POLICY_ID.CurrentValue, objLocationInfo.POLICY_VERSION_ID.CurrentValue);
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
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.FetchGeneralMessage("21") + ":-" + ex.Message;
                hidFormSaved.Value = "2";
            }
            lblDelete.Text = "";
        }

        protected void btnActivateDeactivate_Click(object sender, EventArgs e)
        {
            int retunvalue = 0;
            ClsProductLocationInfo objProductLocationInfo;
            try
            {
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
                            // itrack no 867
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
        }
        /// <summary>
        /// This fuction is overloaded page control data, format the data based on the policy culture
        /// </summary>
        private void ConvertPageControlUsingPolicyCulure()
        {
            numberFormatInfo.NumberDecimalDigits = 2;
            if (txtVALUE_AT_RISK.Text.Trim() != String.Empty)
                txtVALUE_AT_RISK.Text = Convert.ToDouble(txtVALUE_AT_RISK.Text, numberFormatInfo).ToString("N", numberFormatInfo);
            if (txtMAXIMUM_LIMIT.Text.Trim() != String.Empty)
                txtMAXIMUM_LIMIT.Text = Convert.ToDouble(txtMAXIMUM_LIMIT.Text, numberFormatInfo).ToString("N", numberFormatInfo);
            if (txtCLAIM_RATIO.Text.Trim() != String.Empty)
                txtCLAIM_RATIO.Text = Convert.ToDouble(txtCLAIM_RATIO.Text, numberFormatInfo).ToString("N", numberFormatInfo);
            if (txtBONUS.Text.Trim() != String.Empty)
                txtBONUS.Text = Convert.ToDouble(txtBONUS.Text, numberFormatInfo).ToString("N", numberFormatInfo);
            

        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            //Proc_FetchLocationDetails
            if (cmbLOCATION.SelectedValue != "")
            {
                DataSet ds = objProduct.FetchLocationDataUsingLocationID(int.Parse(hidCUSTOMER_ID.Value),int.Parse(cmbLOCATION.SelectedValue));
                
                PopulatePageControlWithDefaultValue(ds);
                     
                
            }
        }
        private void PopulatePageControlWithDefaultValue(DataSet ds)
        {
            if (ds.Tables[0].Rows.Count != 0)
            {
                if (ds.Tables[0].Rows[0]["CONSTRUCTION"].ToString() != "" && ds.Tables[0].Rows[0]["CONSTRUCTION"].ToString() != "0")
                {
                    cmbCONSTRUCTION.SelectedIndex = cmbCONSTRUCTION.Items.IndexOf(cmbCONSTRUCTION.Items.FindByValue(ds.Tables[0].Rows[0]["CONSTRUCTION"].ToString()));
                }          
                if (ds.Tables[0].Rows[0]["DESCRIPTION"].ToString() != "")
                 txtREMARKS.Text = ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();
            }
        }

    }
}
