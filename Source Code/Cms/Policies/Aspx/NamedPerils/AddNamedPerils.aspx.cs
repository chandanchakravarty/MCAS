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
using Cms.Model.Policy.NamedPerils;
using System.Reflection;
using System.Resources;
namespace Cms.Policies.Aspx.NamedPerils
{
    public partial class AddNamedPerils : Cms.Policies.policiesbase
    {
        #region Adding webcontrols on page
        protected System.Web.UI.WebControls.Label lblDelete;
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Label capLOCATION;
        protected System.Web.UI.WebControls.DropDownList cmbLOCATION;
        protected System.Web.UI.WebControls.Label capVR;
        protected System.Web.UI.WebControls.TextBox txtVR;
        protected System.Web.UI.WebControls.Label capBUILDING;
        protected System.Web.UI.WebControls.TextBox txtBUILDING;
        protected System.Web.UI.WebControls.Label capCONTENTS_VALUE;
        protected System.Web.UI.WebControls.TextBox txtCONTENT_VALUE;
        protected System.Web.UI.WebControls.Label capRAW_MATERIAL_VALUE;
        protected System.Web.UI.WebControls.TextBox txtRAW_MATERIAL_VALUE;
        protected System.Web.UI.WebControls.Label capRAWVALUES;
        protected System.Web.UI.WebControls.CheckBox chkRAWVALUES;
        protected System.Web.UI.WebControls.Label capLMI;
        protected System.Web.UI.WebControls.TextBox txtLMI;
        protected System.Web.UI.WebControls.Label capMRI;
        protected System.Web.UI.WebControls.TextBox txtMRI;

        protected System.Web.UI.WebControls.Label capLOSS;
        protected System.Web.UI.WebControls.TextBox txtLOSS;
        protected System.Web.UI.WebControls.Label capTYPE;
        protected System.Web.UI.WebControls.TextBox txtTYPE;

        protected System.Web.UI.WebControls.Label capMULTIPLE_DEDUCTIBLE;
        protected System.Web.UI.WebControls.DropDownList cmbMULTIPLE_DEDUCTIBLE;
        protected System.Web.UI.WebControls.Label capPARKING_SPACES;
        protected System.Web.UI.WebControls.TextBox txtPARKING_SPACES;
        protected System.Web.UI.WebControls.Label capACTIVITY_TYPE;
        protected System.Web.UI.WebControls.DropDownList cmbACTIVITY_TYPE;
        protected System.Web.UI.WebControls.Label capOCCUPANCY;
        protected System.Web.UI.WebControls.DropDownList cmbOCCUPANCY;
        protected System.Web.UI.WebControls.Label capCONSTRUCTION;
        protected System.Web.UI.WebControls.DropDownList cmbCONSTRUCTION;
        protected System.Web.UI.WebControls.Label capASSIST24;
        protected System.Web.UI.WebControls.Label capCATEGORY;
        protected System.Web.UI.WebControls.TextBox txtCATEGORY;
        protected System.Web.UI.WebControls.Label capREMARKS;
        protected System.Web.UI.WebControls.TextBox txtREMARKS;
        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
        protected Cms.CmsWeb.Controls.CmsButton btnDelete;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected System.Web.UI.WebControls.Label capBONUS;
        protected System.Web.UI.WebControls.Label capCLAIM_RATIO;
        protected System.Web.UI.WebControls.TextBox txtBONUS;
        protected System.Web.UI.WebControls.TextBox txtCLAIM_RATIO;
        protected System.Web.UI.WebControls.RegularExpressionValidator revBONUS;
        protected System.Web.UI.WebControls.RegularExpressionValidator revCLAIM_RATIO;
        

        #endregion

        System.Resources.ResourceManager objResourceMgr;
        private String strRowId = String.Empty;
        int Peril_Id;
        ClsNamedPerils objNamedPerils = new ClsNamedPerils();
        ClsProducts objProduct = new ClsProducts();
        public string CALLEDFROM = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Ajax.Utility.RegisterTypeForAjax(typeof(AddNamedPerils));
                ClsMessages.SetCustomizedXml(GetLanguageCode());
                btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");

                CapMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

                #region setting screen id
                base.ScreenId = ALL_RISK_NAMED_PERILSscreenId.LOCATION_INFORMATION;
                hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl");
                #endregion



                this.SetErrorMessages();

                #region setting security Xml
                btnDelete.CmsButtonClass = Cms.CmsWeb.Controls.CmsButtonType.Delete;
                btnDelete.PermissionString = gstrSecurityXML;

                btnSave.CmsButtonClass = CmsButtonType.Write;
                btnSave.PermissionString = gstrSecurityXML;

                btnActivateDeactivate.CmsButtonClass = CmsButtonType.Write;
                btnActivateDeactivate.PermissionString = gstrSecurityXML;

                btnReset.CmsButtonClass = CmsButtonType.Write;
                btnReset.PermissionString = gstrSecurityXML;

                btnSelect.CmsButtonClass = CmsButtonType.Write;
                btnSelect.PermissionString = gstrSecurityXML;
                btnSelect.Text = Cms.CmsWeb.ClsMessages.FetchGeneralButtonsText("btnSelect");

                #endregion

                if (Request.QueryString["CALLEDFROM"] != null)
                {
                    CALLEDFROM = Request.QueryString["CALLEDFROM"].ToString();
                    ClsMessages.SetCustomizedXml(GetLanguageCode());
                    switch (CALLEDFROM.ToUpper().Trim())
                    {
                        case "ERISK":
                            base.ScreenId = ENGENEERING_RISKSscreenId.LOCATION_INFORMATION;
                            hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl");
                            hidCalledFrom.Value = CALLEDFROM.ToUpper();
                            break;
                        case "NAMEDPERILS":
                            base.ScreenId = ALL_RISK_NAMED_PERILSscreenId.LOCATION_INFORMATION;
                            hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl");
                            hidCalledFrom.Value = CALLEDFROM.ToUpper();
                            break;
                        default:
                            base.ScreenId = ALL_RISK_NAMED_PERILSscreenId.LOCATION_INFORMATION;
                            hidTAB_TITLES.Value = ClsMessages.GetTabTitles(ScreenId, "TabCtl");
                            break;

                    }
                }

                // strFormSaved = hidFormSaved.Value;

                btnReset.Attributes.Add("onclick", "javascript: return ResetTheForm();");

                objResourceMgr = new System.Resources.ResourceManager("Cms.policies.Aspx.NamedPerils.AddNamedPerils", System.Reflection.Assembly.GetExecutingAssembly());
                if (!Page.IsPostBack)
                {
                    btnDelete.Attributes.Add("onclick", "javascript:return ShowAlertMessageWhileDelete(true);");
                    this.SetCaptions();
                    this.BindMultiDeductible();
                    //this.BindOccupiedAs();commented for itrack # 1152/TFS#2598
                    this.BindActivityType();
                    this.BindConstruction();
                    this.BindLocation();
                    this.BindExceededPremium();
                    if (Request.QueryString["PERIL_ID"] != null && Request.QueryString["PERIL_ID"].ToString() != "" && Request.QueryString["PERIL_ID"].ToString() != "NEW")
                    {

                        hidPeril_Id.Value = Request.QueryString["PERIL_ID"].ToString();
                        this.GetOldDataObject(Convert.ToInt32(Request.QueryString["PERIL_ID"].ToString()));
                        btnActivateDeactivate.Enabled = true;
                        btnDelete.Visible = true;
                    }
                    else//if (Request.QueryString["PERIL_ID"] == null) 
                    {
                        btnActivateDeactivate.Enabled = false;
                        btnDelete.Enabled = false;
                        hidPeril_Id.Value = "NEW";

                    }
                    strRowId = hidPeril_Id.Value;


                }
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }
        private void GetOldDataObject(Int32 Peril_ID)
        {
            try
            {
                ClsNamedPerilsInfo objNamedPerilsInfo = new ClsNamedPerilsInfo();

                objNamedPerilsInfo.PERIL_ID.CurrentValue = Peril_ID;
                objNamedPerilsInfo.CUSTOMER_ID.CurrentValue = int.Parse(GetCustomerID());
                objNamedPerilsInfo.POLICY_ID.CurrentValue = int.Parse(GetPolicyID());
                objNamedPerilsInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(GetPolicyVersionID());
                string policystatus = GetPolicyStatus();
                //btnSelect.Enabled = false;

                if (objNamedPerils.FetchData(ref objNamedPerilsInfo))
                {


                    btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objNamedPerilsInfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                    // itrack no 867
                    string originalversion = objNamedPerilsInfo.ORIGINAL_VERSION_ID.CurrentValue.ToString();
                    if (originalversion != GetPolicyVersionID() && policystatus == "UENDRS" && originalversion != "0")
                    {
                        btnActivateDeactivate.Visible = false;
                    }
                    if (objNamedPerilsInfo.LOCATION.CurrentValue != -1 && objNamedPerilsInfo.LOCATION.CurrentValue != 0)
                    {
                        hidLOCATION.Value = objNamedPerilsInfo.LOCATION.CurrentValue.ToString();
                        this.BindLocationForUpdate();
                        cmbLOCATION.Enabled = false;
                        btnSelect.Enabled = false;
                        PopulatePageFromEbixModelObject(this.Page, objNamedPerilsInfo);

                        numberFormatInfo.NumberDecimalDigits = 2;

                        if (!String.IsNullOrEmpty(objNamedPerilsInfo.VR.CurrentValue.ToString()) && objNamedPerilsInfo.VR.CurrentValue != -1.0 && objNamedPerilsInfo.VR.CurrentValue.ToString().Trim() != "-1,0")
                            txtVR.Text = objNamedPerilsInfo.VR.CurrentValue.ToString("N", numberFormatInfo);

                        if (!String.IsNullOrEmpty(objNamedPerilsInfo.BUILDING.CurrentValue.ToString()) && objNamedPerilsInfo.BUILDING.CurrentValue != int.MinValue && objNamedPerilsInfo.BUILDING.CurrentValue != -1)
                            txtBUILDING.Text = objNamedPerilsInfo.BUILDING.CurrentValue.ToString("N", numberFormatInfo);

                        if (!String.IsNullOrEmpty(objNamedPerilsInfo.CONTENT_VALUE.CurrentValue.ToString()))
                            txtCONTENT_VALUE.Text = Convert.ToDouble(objNamedPerilsInfo.CONTENT_VALUE.CurrentValue).ToString("N", numberFormatInfo);

                        if (!String.IsNullOrEmpty(objNamedPerilsInfo.RAW_MATERIAL_VALUE.CurrentValue.ToString()))
                            txtRAW_MATERIAL_VALUE.Text = Convert.ToDouble(objNamedPerilsInfo.RAW_MATERIAL_VALUE.CurrentValue).ToString("N", numberFormatInfo);

                        if (!String.IsNullOrEmpty(objNamedPerilsInfo.LMI.CurrentValue.ToString()) && objNamedPerilsInfo.LMI.CurrentValue != -1.0 && objNamedPerilsInfo.LMI.CurrentValue.ToString().Trim() != "-1,0")
                            txtLMI.Text = objNamedPerilsInfo.LMI.CurrentValue.ToString("N", numberFormatInfo);

                        if (!String.IsNullOrEmpty(objNamedPerilsInfo.MRI.CurrentValue.ToString()) && objNamedPerilsInfo.MRI.CurrentValue != -1.0 && objNamedPerilsInfo.MRI.CurrentValue.ToString().Trim() != "-1,0")
                            txtMRI.Text = objNamedPerilsInfo.MRI.CurrentValue.ToString("N", numberFormatInfo);

                        if (!String.IsNullOrEmpty(objNamedPerilsInfo.LOSS.CurrentValue.ToString()) && objNamedPerilsInfo.LOSS.CurrentValue != int.MinValue && objNamedPerilsInfo.LOSS.CurrentValue != -1)
                            txtLOSS.Text = objNamedPerilsInfo.LOSS.CurrentValue.ToString("N", numberFormatInfo);

                        if (!String.IsNullOrEmpty(objNamedPerilsInfo.CLAIM_RATIO.CurrentValue.ToString()) && objNamedPerilsInfo.CLAIM_RATIO.CurrentValue != -1.0 && objNamedPerilsInfo.CLAIM_RATIO.CurrentValue.ToString().Trim() != "-1,0")
                            txtCLAIM_RATIO.Text = objNamedPerilsInfo.CLAIM_RATIO.CurrentValue.ToString("N", numberFormatInfo);

                        if (!String.IsNullOrEmpty(objNamedPerilsInfo.BONUS.CurrentValue.ToString()) && objNamedPerilsInfo.BONUS.CurrentValue != -1.0 && objNamedPerilsInfo.BONUS.CurrentValue.ToString().Trim() != "-1,0")
                            txtBONUS.Text = objNamedPerilsInfo.BONUS.CurrentValue.ToString("N", numberFormatInfo);


                    }
                    else
                    {
                        cmbLOCATION.Enabled = true;
                        btnSelect.Enabled = true;
                        PopulatePageFromEbixModelObject(this.Page, objNamedPerilsInfo);
                    }
                    if (objNamedPerilsInfo.BONUS.CurrentValue.ToString() != "-1,0" && objNamedPerilsInfo.BONUS.CurrentValue != -1.0)
                        txtBONUS.Text = objNamedPerilsInfo.BONUS.CurrentValue.ToString();
                    if (objNamedPerilsInfo.CLAIM_RATIO.CurrentValue.ToString() != "-1,0" && objNamedPerilsInfo.CLAIM_RATIO.CurrentValue != -1.0)
                        txtCLAIM_RATIO.Text = objNamedPerilsInfo.CLAIM_RATIO.CurrentValue.ToString();


                    base.SetPageModelObject(objNamedPerilsInfo);
                }
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
        }

        private void SetCaptions()
        {
            #region setcaption in resource file


            capLOCATION.Text = objResourceMgr.GetString("cmbLOCATION");
            capOCCUPANCY.Text = objResourceMgr.GetString("cmbOCCUPANCY");
            capCONSTRUCTION.Text = objResourceMgr.GetString("cmbCONSTRUCTION");
            capACTIVITY_TYPE.Text = objResourceMgr.GetString("cmbACTIVITY_TYPE");
            capVR.Text = objResourceMgr.GetString("txtVR");
            capLMI.Text = objResourceMgr.GetString("txtLMI");
            capBUILDING.Text = objResourceMgr.GetString("txtBUILDING");
            capMRI.Text = objResourceMgr.GetString("txtMRI");
            capTYPE.Text = objResourceMgr.GetString("txtTYPE");
            capLOSS.Text = objResourceMgr.GetString("txtLOSS");
            capMULTIPLE_DEDUCTIBLE.Text = objResourceMgr.GetString("cmbMULTIPLE_DEDUCTIBLE");
            capCATEGORY.Text = objResourceMgr.GetString("txtCATEGORY");
            capCONTENTS_VALUE.Text = objResourceMgr.GetString("txtCONTENTS_VALUE");
            capRAW_MATERIAL_VALUE.Text = objResourceMgr.GetString("txtRAW_MATERIAL_VALUE");
            capRAWVALUES.Text = objResourceMgr.GetString("chkpRAWVALUES");
            capPARKING_SPACES.Text = objResourceMgr.GetString("txtPARKING_SPACES");
            capRAWVALUES.Text = objResourceMgr.GetString("chkpRAWVALUES");
            capASSIST24.Text = objResourceMgr.GetString("chkASSIST24");
            capREMARKS.Text = objResourceMgr.GetString("txtREMARKS");
            hidDeletemsg.Value = objResourceMgr.GetString("hidDeletemsg");
            capBONUS.Text = objResourceMgr.GetString("txtBONUS");
            capCLAIM_RATIO.Text = objResourceMgr.GetString("txtCLAIM_RATIO");
            capLOCATION_NUMBER.Text = objResourceMgr.GetString("capLOCATION_NUMBER");
            capITEM_NUMBER.Text = objResourceMgr.GetString("capITEM_NUMBER");
            capACTUAL_INSURED_OBJECT.Text = objResourceMgr.GetString("txtACTUAL_INSURED_OBJECT");
            capExceededPremium.Text = objResourceMgr.GetString("cmbExceeded_Premium");

            #endregion


        }

        private void SetErrorMessages()
        {
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            revBONUS.ValidationExpression = aRegExpDoublePositiveWithZero;// aRegExpPositiveCurrency;
            revBONUS.ErrorMessage = ClsMessages.FetchGeneralMessage("492");
            revCLAIM_RATIO.ValidationExpression = aRegExpDoublePositiveWithZero;//aRegExpPositiveCurrency;
            revCLAIM_RATIO.ErrorMessage = ClsMessages.FetchGeneralMessage("492");

            //rev_VR.ValidationExpression = aRegExpCurrency;
            //rev_VR.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");

            //rev_VR.ValidationExpression = aRegExpInteger;
            //rev_VR.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "1");

            rev_VR.ValidationExpression = aRegExpPositiveCurrency;
            rev_VR.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");

            //rev_BUILDING.ValidationExpression = aRegExpInteger;
            //rev_BUILDING.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "2");

            rev_BUILDING.ValidationExpression = aRegExpPositiveCurrency;//aRegExpCurrencyformat;
            rev_BUILDING.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");

            //rev_CONTENTS_VALUE.ValidationExpression =  aRegExpDoublePositiveWithZero;//str
            //rev_CONTENTS_VALUE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");
            rev_CONTENTS_VALUE.ValidationExpression = aRegExpPositiveCurrency;
            rev_CONTENTS_VALUE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");

            //rev_RAW_MATERIAL_VALUE.ValidationExpression = aRegExpDoublePositiveWithZero;//str
            //rev_RAW_MATERIAL_VALUE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");
            rev_RAW_MATERIAL_VALUE.ValidationExpression = aRegExpPositiveCurrency;
            rev_RAW_MATERIAL_VALUE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");

            //rev_LMI.ValidationExpression = aRegExpCurrency;
            //rev_LMI.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");

            //rev_MRI.ValidationExpression = aRegExpCurrency;
            //rev_MRI.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");

            //rev_LMI.ValidationExpression = aRegExpInteger;
            //rev_LMI.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");

            rev_LMI.ValidationExpression = aRegExpPositiveCurrency;
            rev_LMI.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");
            //for itrack 691
            revLOCATION_NUMBER.ValidationExpression = aRegExpDoublePositiveNonZero;
            revLOCATION_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");
            revITEM_NUMBER.ValidationExpression = aRegExpDoublePositiveNonZero;
            revITEM_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");
            rfvLOCATION_NUMBER.ErrorMessage = ClsMessages.FetchGeneralMessage("1510");
            rfvITEM_NUMBER.ErrorMessage = ClsMessages.FetchGeneralMessage("1511");
            //rev_MRI.ValidationExpression = aRegExpInteger;
            //rev_MRI.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");

            rev_MRI.ValidationExpression = aRegExpPositiveCurrency;
            rev_MRI.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");

            //rev_LOSS.ValidationExpression = aRegExpInteger;
            //rev_LOSS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "8");

            rev_LOSS.ValidationExpression = aRegExpPositiveCurrency;
            rev_LOSS.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");


            rev_TYPE.ValidationExpression = aRegExpInteger;//str
            rev_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9");

            //rev_PARKING_SPACES.ValidationExpression = aRegExpInteger;//str
            //rev_PARKING_SPACES.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "12");

           // rev_PARKING_SPACES.ValidationExpression = aRegExpInteger;
           // rev_PARKING_SPACES.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");


            rev_CATEGORY.ValidationExpression = aRegExpTextArea100;//string
            rev_CATEGORY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "13");


            //rev_REMARKS.ValidationExpression = aRegExpTextArea500;//strin
            //csvREMARKS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "17");


            rfvLOCATION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "18");
            //rfvACTIVITY_TYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "19");
            //rfvOCCUPANCY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "20");
           // cmpvITEM_NUMBER.ErrorMessage = ClsMessages.FetchGeneralMessage("1326");
            //Added by Pradeep itrack 837 on 03/03/2011
            csvBONUS.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1455");
            csvCLAIM_RATIO.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1456");
            //Added till here 

            //Added by Pradeep itrack 1512/TFS#240
            csvVR.ErrorMessage = ClsMessages.FetchGeneralMessage("1776"); 
            csvLMI.ErrorMessage = ClsMessages.FetchGeneralMessage("1776");
            //Added till here 

        }
        private void BindLocationForUpdate()
        {
            objNamedPerils.GeLocationNumNAddress(cmbLOCATION, int.Parse(GetCustomerID()));
            //cmbLOCATION.Enabled = false;
        }//private void BindLocationForUpdate()

        private void BindLocation()
        {
            objNamedPerils.GetLocationDetailsForNamedPerilsinfo(cmbLOCATION, int.Parse(GetCustomerID()), int.Parse(GetPolicyID()));
            //objNamedPerils.GeLocationNumNAddress(cmbLOCATION, int.Parse(GetCustomerID()));
        }//private void BindLocation()

        private void BindExceededPremium()
        {
            cmbExceeded_Premium.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
            cmbExceeded_Premium.DataTextField = "LookupDesc";
            cmbExceeded_Premium.DataValueField = "LookupID";
            cmbExceeded_Premium.DataBind();
        }

        private void BindMultiDeductible()
        {
            cmbMULTIPLE_DEDUCTIBLE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("yesno");
            cmbMULTIPLE_DEDUCTIBLE.DataTextField = "LookupDesc";
            cmbMULTIPLE_DEDUCTIBLE.DataValueField = "LookupID";
            cmbMULTIPLE_DEDUCTIBLE.DataBind();
            cmbMULTIPLE_DEDUCTIBLE.Items.Insert(0, "");
        }// private void BindMultiDeductible()

        //private void BindOccupiedAs()
        //{
        //    cmbOCCUPANCY.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("OCDAS");
        //    cmbOCCUPANCY.DataTextField = "LookupDesc";
        //    cmbOCCUPANCY.DataValueField = "LookupID";
        //    cmbOCCUPANCY.DataBind();
        //    cmbOCCUPANCY.Items.Insert(0, "");
        //}//private void BindOccupiedAs(

        //private void BindActivityType()
        //{
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
            cmbOCCUPANCY.DataSource = dv;
            cmbOCCUPANCY.DataTextField = "OCCUPIED_AS";
            cmbOCCUPANCY.DataValueField = "OCCUPIED_ID";
            cmbOCCUPANCY.DataBind();
            cmbOCCUPANCY.Items.Insert(0, "");

        }
        private void BindActivityType()
        {
            DataTable dt = Cms.BusinessLayer.BlApplication.ClsLocation.SELECTACTIVITYTYPE();
           // DataView dv = dt.DefaultView;
            DataView dv = new DataView(dt, "TYPE=11109", "ACTIVITY_DESC", DataViewRowState.CurrentRows);
            dv.Sort = "ACTIVITY_DESC";
            cmbACTIVITY_TYPE.DataSource = dv;
            cmbACTIVITY_TYPE.DataTextField = "ACTIVITY_DESC";
            cmbACTIVITY_TYPE.DataValueField = "ACTIVITY_ID_RUBRICA"; //Modified for itrack#1152 / TFS# 2598
            cmbACTIVITY_TYPE.DataBind();
            cmbACTIVITY_TYPE.Items.Insert(0, "");

        }
        private void BindConstruction()
        {
            DataTable dt;
            if (Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CONTYP").Select("", "LookupDesc").Length > 0)
                dt = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CONTYP").Select("", "LookupDesc").CopyToDataTable<DataRow>(); //Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("CONTYP").Select("", "LookupDesc");
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

        //private void BindLoyalty()
        //{
        //    cmbLOYALTY.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("LOYTY");
        //    cmbLOYALTY.DataTextField = "LookupDesc";
        //    cmbLOYALTY.DataValueField = "LookupID";
        //    cmbLOYALTY.DataBind();
        //    cmbLOYALTY.Items.Insert(0, "");
        //}//private void BindLoyalty()

        private void getFormValues(ClsNamedPerilsInfo objNamedPerilsinfo)
        {
            try
            {
                if (CALLEDFROM == "QAPP")
                {
                    objNamedPerilsinfo.LOCATION.CurrentValue = 0;


                }
                else
                {
                    if ((cmbLOCATION.SelectedItem != null) && (cmbLOCATION.SelectedItem.Text.ToString().Trim() != ""))
                    {
                        objNamedPerilsinfo.LOCATION.CurrentValue = Convert.ToInt32(cmbLOCATION.SelectedItem.Value);

                    }
                    else
                        objNamedPerilsinfo.LOCATION.CurrentValue = base.GetEbixIntDefaultValue();

                }

                objNamedPerilsinfo.ORIGINAL_VERSION_ID.CurrentValue = 0;// int.Parse(GetPolicyVersionID());
                if (txtVR.Text.Trim() != "")
                    objNamedPerilsinfo.VR.CurrentValue = ConvertEbixDoubleValue(txtVR.Text.ToString());
                else
                    objNamedPerilsinfo.VR.CurrentValue = base.GetEbixDoubleDefaultValue();

                if (txtBUILDING.Text.Trim() != "")
                    objNamedPerilsinfo.BUILDING.CurrentValue = ConvertEbixDoubleValue(txtBUILDING.Text);
                else
                    objNamedPerilsinfo.BUILDING.CurrentValue = base.GetEbixDoubleDefaultValue();

                if (txtCONTENT_VALUE.Text.Trim() != "")
                    objNamedPerilsinfo.CONTENT_VALUE.CurrentValue = ConvertEbixDoubleValue(txtCONTENT_VALUE.Text.ToString()).ToString();
                else
                    objNamedPerilsinfo.CONTENT_VALUE.CurrentValue = String.Empty;

                if (txtRAW_MATERIAL_VALUE.Text.Trim() != "")
                    objNamedPerilsinfo.RAW_MATERIAL_VALUE.CurrentValue = ConvertEbixDoubleValue(txtRAW_MATERIAL_VALUE.Text.ToString()).ToString();
                else
                    objNamedPerilsinfo.RAW_MATERIAL_VALUE.CurrentValue = String.Empty;


                if (chkRAWVALUES.Checked) { objNamedPerilsinfo.RAWVALUES.CurrentValue = "Y"; }
                else { objNamedPerilsinfo.RAWVALUES.CurrentValue = "N"; }

                if (txtLMI.Text.Trim() != "")
                    objNamedPerilsinfo.LMI.CurrentValue = ConvertEbixDoubleValue(txtLMI.Text);
                else
                    objNamedPerilsinfo.LMI.CurrentValue = base.GetEbixDoubleDefaultValue();


                if (txtMRI.Text.Trim() != "")
                    objNamedPerilsinfo.MRI.CurrentValue = ConvertEbixDoubleValue(txtMRI.Text);
                else
                    objNamedPerilsinfo.MRI.CurrentValue = base.GetEbixDoubleDefaultValue();


                if (txtLOSS.Text.Trim() != "")
                    objNamedPerilsinfo.LOSS.CurrentValue = ConvertEbixDoubleValue(txtLOSS.Text);
                else
                    objNamedPerilsinfo.LOSS.CurrentValue = base.GetEbixDoubleDefaultValue();

                if (txtTYPE.Text.Trim() != "")
                    objNamedPerilsinfo.TYPE.CurrentValue = Convert.ToInt32(txtTYPE.Text);
                else
                    objNamedPerilsinfo.TYPE.CurrentValue = base.GetEbixIntDefaultValue();


                if ((cmbMULTIPLE_DEDUCTIBLE.SelectedItem != null) && (cmbMULTIPLE_DEDUCTIBLE.SelectedItem.Text.ToString().Trim() != ""))
                    objNamedPerilsinfo.MULTIPLE_DEDUCTIBLE.CurrentValue = Convert.ToString(cmbMULTIPLE_DEDUCTIBLE.SelectedItem.Value);
                else
                    objNamedPerilsinfo.MULTIPLE_DEDUCTIBLE.CurrentValue = String.Empty;

                if (txtPARKING_SPACES.Text.Trim() != "")
                    objNamedPerilsinfo.PARKING_SPACES.CurrentValue = txtPARKING_SPACES.Text;
                else
                    objNamedPerilsinfo.PARKING_SPACES.CurrentValue = String.Empty;
                //Modified for itrack#1152/TFS# 2598
                if ((cmbACTIVITY_TYPE.SelectedItem != null) && (cmbACTIVITY_TYPE.SelectedItem.Text.ToString().Trim() != ""))
                    objNamedPerilsinfo.ACTIVITY_TYPE.CurrentValue = hidACTIVITY_TYPE.Value;
                else
                    objNamedPerilsinfo.ACTIVITY_TYPE.CurrentValue = String.Empty;
                //Modified for itrack#1152/TFS# 2598
                if (hidOCCUPIED.Value!="")
                    objNamedPerilsinfo.OCCUPANCY.CurrentValue = hidOCCUPIED.Value;
                else
                    objNamedPerilsinfo.OCCUPANCY.CurrentValue = String.Empty;

                if ((cmbCONSTRUCTION.SelectedItem != null) && ((cmbCONSTRUCTION.SelectedItem.Text.ToString().Trim() != "")))
                    objNamedPerilsinfo.CONSTRUCTION.CurrentValue = cmbCONSTRUCTION.SelectedItem.Value;
                else
                    objNamedPerilsinfo.CONSTRUCTION.CurrentValue = String.Empty;

                if (txtCATEGORY.Text.Trim() != "")
                    objNamedPerilsinfo.CATEGORY.CurrentValue = txtCATEGORY.Text;
                else
                    objNamedPerilsinfo.CATEGORY.CurrentValue = String.Empty;

                if (chkASSIST24.Checked) { objNamedPerilsinfo.ASSIST24.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.YES); }
                else { objNamedPerilsinfo.ASSIST24.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.NO); }

                if (txtREMARKS.Text.Trim() != "")
                    objNamedPerilsinfo.REMARKS.CurrentValue = txtREMARKS.Text;
                else
                    objNamedPerilsinfo.REMARKS.CurrentValue = String.Empty;

                if (txtCLAIM_RATIO.Text != "")
                    objNamedPerilsinfo.CLAIM_RATIO.CurrentValue = ConvertEbixDoubleValue(txtCLAIM_RATIO.Text);
                else
                    objNamedPerilsinfo.CLAIM_RATIO.CurrentValue = GetEbixDoubleDefaultValue();


                if (txtBONUS.Text != "")
                    objNamedPerilsinfo.BONUS.CurrentValue = ConvertEbixDoubleValue(txtBONUS.Text);
                else
                    objNamedPerilsinfo.BONUS.CurrentValue = GetEbixDoubleDefaultValue();

                if (txtLOCATION_NUMBER.Text != "")
                {
                    objNamedPerilsinfo.LOCATION_NUMBER.CurrentValue = int.Parse(txtLOCATION_NUMBER.Text);
                }
                else
                    objNamedPerilsinfo.LOCATION_NUMBER.CurrentValue = base.GetEbixIntDefaultValue();

                if (txtITEM_NUMBER.Text != "")
                {
                    objNamedPerilsinfo.ITEM_NUMBER.CurrentValue = int.Parse(txtITEM_NUMBER.Text);
                }
                else
                    objNamedPerilsinfo.ITEM_NUMBER.CurrentValue = base.GetEbixIntDefaultValue();

                if (txtACTUAL_INSURED_OBJECT.Text != "")
                   objNamedPerilsinfo.ACTUAL_INSURED_OBJECT.CurrentValue = txtACTUAL_INSURED_OBJECT.Text;
                else
                    objNamedPerilsinfo.ACTUAL_INSURED_OBJECT.CurrentValue = String.Empty;

                if (cmbExceeded_Premium.SelectedValue != "")
                    objNamedPerilsinfo.EXCEEDED_PREMIUM.CurrentValue = int.Parse(cmbExceeded_Premium.SelectedValue);
                else
                    objNamedPerilsinfo.EXCEEDED_PREMIUM.CurrentValue = GetEbixIntDefaultValue();

            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            ClsMessages.SetCustomizedXml(GetLanguageCode());
            int intRetval;
            strRowId = hidPeril_Id.Value;

            ClsNamedPerilsInfo objNamedPerilsinfo;
            try
            {
                //For The Save Case                
                if (strRowId.ToUpper().Equals("NEW"))
                {

                    objNamedPerilsinfo = new ClsNamedPerilsInfo();
                    this.getFormValues(objNamedPerilsinfo);

                    objNamedPerilsinfo.CUSTOMER_ID.CurrentValue = int.Parse(GetCustomerID());
                    objNamedPerilsinfo.POLICY_ID.CurrentValue = int.Parse(GetPolicyID());
                    objNamedPerilsinfo.POLICY_VERSION_ID.CurrentValue = int.Parse(GetPolicyVersionID());

                    objNamedPerilsinfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objNamedPerilsinfo.CREATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    objNamedPerilsinfo.IS_ACTIVE.CurrentValue = "Y"; //hidIS_ACTIVE.Value;

                    objNamedPerilsinfo.CO_APPLICANT_ID.CurrentValue = ClsGeneralInformation.GetPolicyPrimary_Applicant(objNamedPerilsinfo.CUSTOMER_ID.CurrentValue, objNamedPerilsinfo.POLICY_ID.CurrentValue, objNamedPerilsinfo.POLICY_VERSION_ID.CurrentValue);
                    intRetval = objNamedPerils.AddNamedPeril(objNamedPerilsinfo,hidCalledFrom.Value);

                    if (intRetval > 0)
                    {
                        this.GetOldDataObject(objNamedPerilsinfo.PERIL_ID.CurrentValue);
                        hidPeril_Id.Value = objNamedPerilsinfo.PERIL_ID.CurrentValue.ToString();
                        btnActivateDeactivate.Enabled = true;
                        btnDelete.Enabled = true;
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        hidFormSaved.Value = "1";
                        base.OpenEndorsementDetails();

                    }
                    else if (intRetval == -1)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -5)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("1326");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }
                    lblDelete.Visible = false;
                    lblMessage.Visible = true;
                } // if (strRowId.ToUpper().Equals("NEW"))
                else //For The Update cse
                {


                    objNamedPerilsinfo = (ClsNamedPerilsInfo)base.GetPageModelObject();
                    this.getFormValues(objNamedPerilsinfo);
                    objNamedPerilsinfo.IS_ACTIVE.CurrentValue = "Y"; //hidIS_ACTIVE.Value;
                    objNamedPerilsinfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objNamedPerilsinfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objNamedPerilsinfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                    intRetval = objNamedPerils.UpdateNamedParils(objNamedPerilsinfo,hidCalledFrom.Value);

                    if (intRetval > 0)
                    {
                        this.GetOldDataObject(objNamedPerilsinfo.PERIL_ID.CurrentValue);
                        hidPeril_Id.Value = objNamedPerilsinfo.PERIL_ID.CurrentValue.ToString();
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");

                        hidFormSaved.Value = "1";
                        base.OpenEndorsementDetails();

                    }
                    else if (intRetval == -1)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                        hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -5)
                    {
                        this.GetOldDataObject(objNamedPerilsinfo.PERIL_ID.CurrentValue);
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("1326");
                        hidFormSaved.Value = "2";
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        hidFormSaved.Value = "2";
                    }
                    lblDelete.Visible = false;
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
            finally
            {
                //if (objNamedPerilsinfo != null)
                //    objNamedPerilsinfo.Dispose();
            }

        }

        ///// <summary>
        ///// Required method for Designer support - do not modify
        ///// the contents of this method with the code editor.
        ///// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ClsNamedPerilsInfo objNamedPerilsinfo;
            try
            {
                ClsMessages.SetCustomizedXml(GetLanguageCode());
                String ConfirmValue = String.Empty;

                if (hidPeril_Id.Value != "" && hidConfirmValue.Value != "" && hidConfirmValue.Value != "undefined")
                {

                    ConfirmValue = hidConfirmValue.Value.ToString();
                    objNamedPerilsinfo = (ClsNamedPerilsInfo)base.GetPageModelObject();

                    objNamedPerilsinfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());

                    int intRetval = objNamedPerils.DeleteNamedPeril(objNamedPerilsinfo, Convert.ToInt32(ConfirmValue),hidCalledFrom.Value);
                    if (intRetval > 0)
                    {
                        lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "127");
                        hidFormSaved.Value = "1";
                        hidPeril_Id.Value = "";
                        trBody.Attributes.Add("style", "display:none");
                    }
                    else if (intRetval == -1)
                    {
                        lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G", "128");
                        hidFormSaved.Value = "2";
                    }
                    lblDelete.Visible = true;
                    lblMessage.Visible = false;
                    // this.BindAllFormControlsUsingPerilID(objNamedPerilsinfo.PERIL_ID.CurrentValue);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }

        protected void btnActivateDeactivate_Click(object sender, EventArgs e)
        {
            ClsMessages.SetCustomizedXml(GetLanguageCode());
            ClsNamedPerilsInfo objNamedPerilsinfo;//= new ClsNamedPerilsInfo();

            try
            {
                if (int.TryParse(hidPeril_Id.Value, out Peril_Id))
                {
                    objNamedPerilsinfo = (ClsNamedPerilsInfo)base.GetPageModelObject();

                    if (objNamedPerilsinfo.IS_ACTIVE.CurrentValue == "Y")
                    { objNamedPerilsinfo.IS_ACTIVE.CurrentValue = "N"; }
                    else
                    { objNamedPerilsinfo.IS_ACTIVE.CurrentValue = "Y"; }

                    objNamedPerilsinfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objNamedPerilsinfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objNamedPerilsinfo.LAST_UPDATED_DATETIME.CurrentValue = Convert.ToDateTime(DateTime.Now.ToShortDateString());


                    int intRetval = objNamedPerils.ActivateDeactivateNamedPeril(objNamedPerilsinfo,hidCalledFrom.Value);
                    if (intRetval > 0)
                    {
                        if (objNamedPerilsinfo.IS_ACTIVE.CurrentValue == "N")
                        {
                            lblMessage.Text = ClsMessages.FetchGeneralMessage("9");
                            //irack no 867
                           // btnActivateDeactivate.Visible = false;
                        }
                        else
                        {
                            lblMessage.Text = ClsMessages.FetchGeneralMessage("7");
                        }

                        hidFormSaved.Value = "1";
                        btnActivateDeactivate.Text = ClsMessages.FetchActivateDeactivateButtonsText(objNamedPerilsinfo.IS_ACTIVE.CurrentValue.ToString().Trim());
                        SetPageModelObject(objNamedPerilsinfo);
                    }

                    else if (intRetval == -5) {
                        this.GetOldDataObject(objNamedPerilsinfo.PERIL_ID.CurrentValue);
                        hidFormSaved.Value = "2";
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("1326");
                    }

                    lblDelete.Visible = false;
                    lblMessage.Visible = true;

                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                hidFormSaved.Value = "2";

            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            //Proc_FetchLocationDetails
            if (cmbLOCATION.SelectedValue != "")
            {
                DataSet ds = objProduct.FetchLocationDataUsingLocationID(int.Parse(GetCustomerID()), int.Parse(cmbLOCATION.SelectedValue));

                PopulatePageControlWithDefaultValue(ds);


            }
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
        private void PopulatePageControlWithDefaultValue(DataSet ds)
        {
            if (ds.Tables[0].Rows.Count != 0)
            {

                //Added for itrack#1152/TFS# 2598
                int OCCUPIED = 0;
                if (int.TryParse(ds.Tables[0].Rows[0]["OCCUPIED"].ToString(), out OCCUPIED))
                    this.BindOccupiedAs("", OCCUPIED);

                string _activity_type = ds.Tables[0].Rows[0]["ACTIVITY_TYPE"].ToString();
                if (_activity_type != "")
                    cmbACTIVITY_TYPE.SelectedIndex = cmbACTIVITY_TYPE.Items.IndexOf(cmbACTIVITY_TYPE.Items.FindByValue(_activity_type + "^" + hidRUBRICA_ID.Value));
                //Added till here 
                /* commented itrack#1152/TFS# 2598
                if (ds.Tables[0].Rows[0]["ACTIVITY_TYPE"].ToString() != "" && ds.Tables[0].Rows[0]["ACTIVITY_TYPE"].ToString() != "0")
                    cmbACTIVITY_TYPE.SelectedIndex = cmbACTIVITY_TYPE.Items.IndexOf(cmbACTIVITY_TYPE.Items.FindByValue(ds.Tables[0].Rows[0]["ACTIVITY_TYPE"].ToString()));
                */
                if (ds.Tables[0].Rows[0]["OCCUPIED"].ToString() != "" && ds.Tables[0].Rows[0]["OCCUPIED"].ToString() != "0")
                    cmbOCCUPANCY.SelectedIndex = cmbOCCUPANCY.Items.IndexOf(cmbOCCUPANCY.Items.FindByValue(ds.Tables[0].Rows[0]["OCCUPIED"].ToString()));

                if (ds.Tables[0].Rows[0]["CONSTRUCTION"].ToString() != "" && ds.Tables[0].Rows[0]["CONSTRUCTION"].ToString() != "0")
                    cmbCONSTRUCTION.SelectedIndex = cmbCONSTRUCTION.Items.IndexOf(cmbCONSTRUCTION.Items.FindByValue(ds.Tables[0].Rows[0]["CONSTRUCTION"].ToString()));

                if (ds.Tables[0].Rows[0]["CATEGORY"].ToString() != "")
                    txtCATEGORY.Text = ds.Tables[0].Rows[0]["CATEGORY"].ToString();

                if (ds.Tables[0].Rows[0]["DESCRIPTION"].ToString() != "")
                    txtREMARKS.Text = ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();


            }
        }
    }
}