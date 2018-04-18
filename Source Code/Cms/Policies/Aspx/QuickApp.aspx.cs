/***************************************************
<Author					: -  Charles Gomes
<Creation Date			: -	 11-Jun-2010
<Description			: -  Add Quick Application
**************************************************** */

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Resources;
using System.Reflection;
using System.Data;

using Cms.CmsWeb.Controls;
using Cms.Model.Policy;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlApplication;
using Cms.CmsWeb;
using Cms.Model.Client;
using Cms.BusinessLayer.BlClient;

namespace Cms.Policies.Aspx
{
    public partial class QuickApp : Cms.Policies.policiesbase
    {
        public ResourceManager objResourceManager = null;
        public const string ALL_SECURITY = @"<Security><Read>Y</Read><Write>Y</Write><Delete>Y</Delete><Execute>Y</Execute></Security>";
        public const string READ_SECURITY = @"<Security><Read>Y</Read><Write>N</Write><Delete>N</Delete><Execute>N</Execute></Security>";
       
                   
        private static string[] strDateNodes = null;
        #region Ajax Methods

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public string AjaxAttachCustomer(int iCustomerID)
        {
            string retVal = "";
            try
            {
                ClsQuickAppInfo objQuickAppInfo = new ClsQuickAppInfo();
                //-100 is Quick App Customer                
                retVal = objQuickAppInfo.attachCustomer(-100, iCustomerID, int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()), ClsCommon.ConnStr);
                
                if (retVal != "")
                {
                    string[] arr = retVal.Split('~');
                    SetCustomerID(arr[0]);
                    SetPolicyID(arr[2]);
                    SetPolicyVersionID("1");
                    SetLastVisited(arr[0], arr[1], "1");
                    string CustomerName = "";
                    CustomerName = ClsCustomer.GetCustomerNameByID(int.Parse(arr[0]));
                    return retVal + '~' + CustomerName;
                }
                else
                {

                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet AjaxFetchInfo(int iLOB_ID)
        {
            DataSet ds = null;

            try
            {
                ds = new DataSet();

                DataTable dt1 = new DataTable();
                try
                {
                    dt1 = ClsEndorsmentDetails.GetSUBLOBs(iLOB_ID.ToString()).Tables[0];
                }
                catch
                { }
                ds.Tables.Add(dt1.Copy());
                ds.Tables[0].TableName = "SUBLOBS";

                DataTable dt2 = new DataTable();
                try
                {
                    dt2 = ClsGeneralInformation.GetBillType(iLOB_ID, "POL");
                }
                catch
                { }
                ds.Tables.Add(dt2.Copy());
                ds.Tables[1].TableName = "BILLTYPE";

                DataTable dt3 = new DataTable();
                try
                {
                    dt3 = ClsGeneralInformation.GetLOBTerms(iLOB_ID).Tables[0];
                }
                catch
                { }
                ds.Tables.Add(dt3.Copy());
                ds.Tables[2].TableName = "APPTERMS";

                return ds;
            }
            catch
            {
                return null;
            }
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public string GetDefaultInstallmentPlan(string sAPP_TERMS)
        {
            Cms.BusinessLayer.BlCommon.Accounting.ClsInstallmentPlan objPlan = new
                Cms.BusinessLayer.BlCommon.Accounting.ClsInstallmentPlan();

            if (sAPP_TERMS != null && sAPP_TERMS != "")
            {
                int PlanID = objPlan.GetDefaultPlanId(int.Parse(sAPP_TERMS));

                if (PlanID != 0)
                {
                    return PlanID.ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet GetInstallPlan(string sAPP_TERMS)
        {
            try
            {
                return ClsInstallmentInfo.GetApplicableInstallmentPlans(int.Parse(sAPP_TERMS));
            }
            catch
            {
                return null;
            }
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public string GetExpDate(string sAPP_TERMS, string sAPP_EFFECTIVE_DATE)
        {
            try
            {
                SetCultureThread(GetLanguageCode());
                return ConvertToDate(sAPP_EFFECTIVE_DATE).AddMonths(int.Parse(sAPP_TERMS)).ToShortDateString();
            }
            catch
            {
                return "";
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(QuickApp));
           
            base.ScreenId = "";

            lblMessage.Visible = false;

            if (objResourceManager == null)
            {
                objResourceManager = new ResourceManager("Cms.Policies.Aspx.QuickApp", Assembly.GetExecutingAssembly());
                SetCaptions();
            }
            hidLOB_MSG.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1172");
            if (!IsPostBack)
            {
                #region Set Attributes
                txtAPP_EFFECTIVE_DATE.Attributes.Add("onblur", "javascript:setExpDate();");
                hlkCalandarDate.Attributes.Add("onclick", "javascript:fPopCalendar(document.getElementById('txtAPP_EFFECTIVE_DATE'),document.getElementById('txtAPP_EFFECTIVE_DATE'))");
                btnSave.Attributes.Add("onclick", "javascript:return ValCheck();");
                #endregion
                hidLangCulture.Value = GetLanguageCode();

                strDateNodes = new string[2];

                strDateNodes[0] = "APP_EFFECTIVE_DATE";
                strDateNodes[1] = "APP_EXPIRATION_DATE";
                fetchQueryParams();

                fillDropDowns();
                SetErrorMessages();

                hidAttachUrl.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupURL();

                Cache["Level"] = "0";

                if (hidPOLICY_ID.Value != "0")
                {
                    fetchData();
                    btnNext.Visible = true;
                    QuickAppMenu.Enabled = true;
                    BindMenu();
                    ShowQappCustomer();
                }
                else
                {
                    txtAPP_EFFECTIVE_DATE.Text = ConvertToDateCulture(DateTime.Now);
                }
            }
                 


            SetButtonsSecurityXml();
        }

        private void ShowQappCustomer()
        {
            div_qapp.Attributes.Add("style", "display:block");
            capQApp_No.Text = ClsCommon.FetchValueFromXML("QQ_NUMBER", hidOldData.Value);
            string CustomerName = "";
            CustomerName = ClsCustomer.GetCustomerNameByID(int.Parse(hidCUSTOMER_ID.Value));

            if (CustomerName != "")
            {
                capCustomer_Name.Text = CustomerName;
                div_customer.Attributes.Add("style", "display:block");
            }
            if (hidAPP_STATUS.Value != "QAPP")
            {
                div_appno.Attributes.Add("style", "display:block");
                capApp_No.Text = ClsCommon.FetchValueFromXML("APP_NUMBER", hidOldData.Value); 
            }
        }

        private void fetchData()
        {
            ClsQuickAppInfo objQuickAppInfo = new ClsQuickAppInfo();
            DataSet dstemp = null;
            try
            {
                objQuickAppInfo.FLAG.CurrentValue = "F";
                objQuickAppInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);
                objQuickAppInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);
                objQuickAppInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);

                dstemp = objQuickAppInfo.FetchData();
                if (dstemp != null && dstemp.Tables.Count > 0)
                {
                    hidOldData.Value = ClsCommon.GetXMLEncoded(dstemp.Tables[0]);
                    if (hidLangCulture.Value.Trim() != cmsbase.DEFAULT_LANG_CULTURE)
                    {
                        hidOldData.Value = FormatXMLDateNode(hidOldData.Value, strDateNodes);
                    }
                    hidappnumber.Value = ClsCommon.FetchValueFromXML("QQ_NUMBER", hidOldData.Value);
                    hidPOLICY_LOB.Value = ClsCommon.FetchValueFromXML("POLICY_LOB", hidOldData.Value);
                    SetLOBID(hidPOLICY_LOB.Value);
                    SetProductString(hidPOLICY_LOB.Value);
                    SetQQ_ID(hidappnumber.Value);
                    hidAPP_TERMS.Value = ClsCommon.FetchValueFromXML("APP_TERMS", hidOldData.Value);
                    hidAPP_STATUS.Value = ClsCommon.FetchValueFromXML("APP_STATUS", hidOldData.Value);
                    hidDIV_ID_DEPT_ID_PC_ID.Value = ClsCommon.FetchValueFromXML("DIV_ID_DEPT_ID_PC_ID", hidOldData.Value);
                    ShowQappCustomer();
                  
                    ClsCommon.PopulateEbixPageModel(dstemp, objQuickAppInfo);
                    SetPageModelObject(objQuickAppInfo);
                    cmbPOLICY_LOB.Enabled = false;

                }
                else
                {
                    hidOldData.Value = "";
                }


            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (dstemp != null)
                {
                    dstemp = null;
                }
            }
        }

        private void SetProductString(string strLOB_ID)
        {
            switch (strLOB_ID)
            {
                case LOB_HOME:
                    SetLOBString("HOME");
                    break;
                case LOB_PRIVATE_PASSENGER:
                    SetLOBString("PPA");
                    break;
                case LOB_MOTORCYCLE:
                    SetLOBString("MOT");
                    break;
                case LOB_WATERCRAFT:
                    SetLOBString("WAT");
                    break;
                case LOB_RENTAL_DWELLING:
                    SetLOBString("RENT");
                    break;
                case LOB_UMBRELLA:
                    SetLOBString("UMB");
                    break;
                case LOB_GENERAL_LIABILITY:
                    SetLOBString("GEN");
                    break;
                case LOB_AVIATION:
                    SetLOBString("AVIATION");
                    break;
                default:
                    SetLOBString(ClsGeneralInformation.GetLOBCodeFromID(int.Parse(strLOB_ID)));
                    break;
            }
        }

        private void fetchQueryParams()
        {
            if (Request.QueryString["customer_id"] != null && Request.QueryString["customer_id"].ToString().Trim() != "")
            {
                hidCUSTOMER_ID.Value = Request.QueryString["customer_id"].ToString().Trim();
                SetCustomerID(hidCUSTOMER_ID.Value);
            }
            if (Request.QueryString["policy_id"] != null && Request.QueryString["policy_id"].ToString().Trim() != "")
            {
                hidPOLICY_ID.Value = Request.QueryString["policy_id"].ToString().Trim();
                //Cache["policy_id"] = hidPOLICY_ID.Value;
                SetPolicyID(hidPOLICY_ID.Value);
            }
            if (Request.QueryString["policy_version_id"] != null && Request.QueryString["policy_version_id"].ToString().Trim() != "")
            {
                hidPOLICY_VERSION_ID.Value = Request.QueryString["policy_version_id"].ToString().Trim();
                //Cache["policy_version_id"] = hidPOLICY_VERSION_ID.Value;
                SetPolicyVersionID(hidPOLICY_VERSION_ID.Value);
            }
            SetCalledFor("QAPP");
            if (Request.QueryString["CALLEDFROM"] != null && Request.QueryString["CALLEDFROM"].ToString().Trim() != ""
                && Request.QueryString["CALLEDFROM"].ToString().Trim() == "PREV")
            {
                hidCUSTOMER_ID.Value = GetCustomerID();
                hidPOLICY_ID.Value = GetPolicyID();//Cache["policy_id"].ToString().Trim();
                hidPOLICY_VERSION_ID.Value = GetPolicyVersionID();//Cache["policy_version_id"].ToString().Trim();
            }
        }

        private void SetCaptions()
        {
            try
            {
                lblHeader.Text = objResourceManager.GetString("lblHeader");
                lblManHeader.Text = objResourceManager.GetString("lblManHeader");
                lblCustomerIndex.Text = objResourceManager.GetString("lblCustomerIndex");
                lblRate.Text = objResourceManager.GetString("lblRate");
                spnCustomerIndex.Attributes.Add("title", objResourceManager.GetString("lblCustomerIndex"));
                spnRate.Attributes.Add("title", objResourceManager.GetString("lblRate"));

                capPOLICY_LOB.Text = objResourceManager.GetString("cmbPOLICY_LOB");
                capPOLICY_SUBLOB.Text = objResourceManager.GetString("cmbPOLICY_SUBLOB");
                capAPP_TERMS.Text = objResourceManager.GetString("cmbAPP_TERMS");
                capAPP_EFFECTIVE_DATE.Text = objResourceManager.GetString("txtAPP_EFFECTIVE_DATE");
                capAPP_EXPIRATION_DATE.Text = objResourceManager.GetString("txtAPP_EXPIRATION_DATE");
                capBILL_TYPE_ID.Text = objResourceManager.GetString("cmbBILL_TYPE_ID");
                capINSTALL_PLAN_ID.Text = objResourceManager.GetString("cmbINSTALL_PLAN_ID");
                capPOLICY_CURRENCY.Text = objResourceManager.GetString("cmbPOLICY_CURRENCY");
                capCustomer.Text = "<b>" + objResourceManager.GetString("lblCustomer") + "</b>";
                capQApp.Text = "<b>" + objResourceManager.GetString("lblQapp") + "</b>";
                capApp.Text = "<b>" + objResourceManager.GetString("lblappp") + "</b>";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private void SetErrorMessages()
        {
            try
            {
                revAPP_EFFECTIVE_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("22");
                revAPP_EFFECTIVE_DATE.ValidationExpression = aRegExpDate;
                rfvPOLICY_LOB.ErrorMessage = ClsMessages.FetchGeneralMessage("97");
                rfvPOLICY_SUBLOB.ErrorMessage = ClsMessages.FetchGeneralMessage("98");
                rfvAPP_TERMS.ErrorMessage = ClsMessages.FetchGeneralMessage("93");
                rfvBILL_TYPE.ErrorMessage = ClsMessages.FetchGeneralMessage("218");
                rfvINSTALL_PLAN_ID.ErrorMessage = ClsMessages.FetchGeneralMessage("731");
                rfvDIV_ID_DEPT_ID_PC_ID.ErrorMessage = ClsMessages.FetchGeneralMessage("1092");
                rfvPOLICY_CURRENCY.ErrorMessage = ClsMessages.FetchGeneralMessage("1070");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private void fillDropDowns()
        {
            try
            {
                cmbPOLICY_LOB.DataSource = Cms.CmsWeb.ClsFetcher.LOBs;
                cmbPOLICY_LOB.DataTextField = "LOB_DESC";
                cmbPOLICY_LOB.DataValueField = "LOB_ID";
                cmbPOLICY_LOB.DataBind();
                cmbPOLICY_LOB.Items.Insert(0, new ListItem("", ""));

                cmbDIV_ID_DEPT_ID_PC_ID.DataSource = ClsCommon.PopulateDivDeptPC();
                cmbDIV_ID_DEPT_ID_PC_ID.DataTextField = "TEXT";
                cmbDIV_ID_DEPT_ID_PC_ID.DataValueField = "VALUE";
                cmbDIV_ID_DEPT_ID_PC_ID.DataBind();
                cmbDIV_ID_DEPT_ID_PC_ID.Items.Insert(0, new ListItem("", ""));

                cmbPOLICY_CURRENCY.DataSource = Cms.CmsWeb.ClsFetcher.Currency;
                cmbPOLICY_CURRENCY.DataTextField = "CURR_DESC";
                cmbPOLICY_CURRENCY.DataValueField = "CURRENCY_ID";
                cmbPOLICY_CURRENCY.DataBind();
                cmbPOLICY_CURRENCY.Items.Insert(0, "");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private void SetButtonsSecurityXml()
        {
            try
            {
                if (hidAPP_STATUS.Value.ToUpper().Trim() == "QAPP" || hidAPP_STATUS.Value.ToUpper().Trim() == "")
                {
                    btnNext.CmsButtonClass = CmsButtonType.Read;
                    btnNext.PermissionString = ALL_SECURITY;

                    btnSave.CmsButtonClass = CmsButtonType.Write;
                    btnSave.PermissionString = ALL_SECURITY;

                    btnMakeApp.CmsButtonClass = CmsButtonType.Write;
                    btnMakeApp.PermissionString = ALL_SECURITY;
                    btnApplication.CmsButtonClass = CmsButtonType.Read;
                    btnApplication.PermissionString = ALL_SECURITY;
                }
                else
                {
                    btnNext.CmsButtonClass = CmsButtonType.Read;
                    btnNext.PermissionString = READ_SECURITY;

                    btnSave.CmsButtonClass = CmsButtonType.Write;
                    btnSave.PermissionString = READ_SECURITY;

                    btnMakeApp.CmsButtonClass = CmsButtonType.Write;
                    btnMakeApp.PermissionString = READ_SECURITY;
                    btnApplication.CmsButtonClass = CmsButtonType.Read;
                    btnApplication.PermissionString = READ_SECURITY;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            //Cache.Insert("Level", "1", null, DateTime.Now.AddHours(1), TimeSpan.FromMinutes(30), System.Web.Caching.CacheItemPriority.Normal, null);
            Cache["Level"] = "1";
            Response.Redirect(@"/Cms/Policies/Aspx/QuickAppFrame.aspx", true);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ClsQuickAppInfo objQuickAppInfo = new ClsQuickAppInfo();

            try
            {
                int returnValue = 0;

                if (hidPOLICY_ID.Value.Trim() != "0")
                {
                    objQuickAppInfo = (ClsQuickAppInfo)GetPageModelObject();
                }

                objQuickAppInfo = GetFormValue();

                if (hidPOLICY_ID.Value.Trim() != "0")
                {
                    objQuickAppInfo.FLAG.CurrentValue = "U";
                    objQuickAppInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);
                    objQuickAppInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);
                    objQuickAppInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);
                }
                else
                {
                    objQuickAppInfo.FLAG.CurrentValue = "I";
                }

                objQuickAppInfo.TransactLabel = ClsCommon.MapTransactionLabel("Policies/Aspx/QuickApp.aspx.resx");

                returnValue = objQuickAppInfo.QuickAppAddUpd();

                if (returnValue > 0)
                {
                    if (objQuickAppInfo.FLAG.CurrentValue == "I")
                    {
                        hidPOLICY_ID.Value = returnValue.ToString();
                        hidPOLICY_VERSION_ID.Value = "1";
                        hidCUSTOMER_ID.Value = "-100";

                        SetCustomerID(hidCUSTOMER_ID.Value);
                        SetPolicyID(hidPOLICY_ID.Value);
                        SetPolicyVersionID(hidPOLICY_VERSION_ID.Value);

                        lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
                        btnApplication.Attributes.Add("style", "display:none");
                    }
                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");
                    }
                    SetPolicyStatus("QAPP");
                    SetPolicyCurrency(objQuickAppInfo.POLICY_CURRENCY.CurrentValue.ToString());
                    btnNext.Visible = true;
                    QuickAppMenu.Enabled = true;

                    BindMenu();
                    fetchData();
                    SetLastVisited();
                    if (objQuickAppInfo.POLICY_LOB.CurrentValue.ToString() == "24")
                    {
                        Response.Redirect("/Cms/policies/aspx/windhail/QuickQuoteTab.aspx");
                    }
                }
                else
                {
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                }

                lblMessage.Visible = true;
            }
            catch (Exception ex)
            {
                btnNext.Visible = false;
                QuickAppMenu.Enabled = false;
                lblMessage.Text = ClsMessages.FetchGeneralMessage("21") + ": <br />" + ex.Message;
                lblMessage.Visible = true;
            }
        }

        protected void btnGo_Application(object sender, EventArgs e)
        {
            string qstr = "CUSTOMER_ID=" + GetCustomerID() + "&app_id=" + GetPolicyID() + "&app_version_id=" + GetPolicyVersionID() + "&POLICY_ID=" + GetPolicyID() + "&POLICY_VERSION_ID=" + GetPolicyVersionID() + "&CalledFrom=APP";
            Response.Redirect("/Cms/Policies/Aspx/PolicyTab.aspx" + QueryStringModule.EncriptQueryString(qstr));
        }

        private void SetLastVisited()
        {
            try
            {

                string quote_Details = hidCUSTOMER_ID.Value + "~" + hidPOLICY_ID.Value + "~" + hidPOLICY_VERSION_ID.Value + "~" + DateTime.Now.ToString() + "~" + GetQQ_ID();
                ClsGeneralInformation objLastVisitedQuote = new ClsGeneralInformation();
                objLastVisitedQuote.UpdateLastVisitedPageEntry("Quote", quote_Details, int.Parse(GetUserId()), GetSystemId());
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private void SetLastVisited(string CUSTOMER_ID, string POLICY_ID, string POLICY_VERSION_ID)
        {
            try
            {

                string quote_Details = CUSTOMER_ID + "~" + POLICY_ID + "~" + POLICY_VERSION_ID + "~" + DateTime.Now.ToString() +"~"+ GetQQ_ID();
                ClsGeneralInformation objLastVisitedQuote = new ClsGeneralInformation();
                objLastVisitedQuote.UpdateLastVisitedPageEntry("Quote", quote_Details, int.Parse(GetUserId()), GetSystemId());
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private ClsQuickAppInfo GetFormValue()
        {
            ClsQuickAppInfo objQuickAppInfo = new ClsQuickAppInfo();

            try
            {
                objQuickAppInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);
                objQuickAppInfo.POLICY_LOB.CurrentValue = hidPOLICY_LOB.Value;
                objQuickAppInfo.POLICY_SUBLOB.CurrentValue = hidPOLICY_SUBLOB.Value.Trim();
                objQuickAppInfo.APP_TERMS.CurrentValue = hidAPP_TERMS.Value.Trim();
                objQuickAppInfo.APP_EFFECTIVE_DATE.CurrentValue = ConvertToDate(txtAPP_EFFECTIVE_DATE.Text.Trim());
                objQuickAppInfo.APP_EXPIRATION_DATE.CurrentValue =
                    objQuickAppInfo.APP_EFFECTIVE_DATE.CurrentValue.AddMonths(int.Parse(objQuickAppInfo.APP_TERMS.CurrentValue));
                objQuickAppInfo.BILL_TYPE_ID.CurrentValue = int.Parse(hidBILL_TYPE_ID.Value.Trim());
                objQuickAppInfo.INSTALL_PLAN_ID.CurrentValue = int.Parse(hidINSTALL_PLAN_ID.Value.Trim());

                objQuickAppInfo.DIV_ID_DEPT_ID_PC_ID.CurrentValue = hidDIV_ID_DEPT_ID_PC_ID.Value;

                objQuickAppInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                if (cmbPOLICY_CURRENCY.SelectedItem != null && cmbPOLICY_CURRENCY.SelectedValue != "")
                {
                    objQuickAppInfo.POLICY_CURRENCY.CurrentValue = int.Parse(cmbPOLICY_CURRENCY.SelectedValue);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return objQuickAppInfo;
        }

        private void BindMenu()
        {
            QuickAppMenu.DataSourceID = "XmlDataSourceMenu";

            string sLangID = ClsCommon.BL_LANG_ID.ToString();

            MenuItemBinding root = new MenuItemBinding();
            root.DataMember = "QuickApp";
            root.NavigateUrlField = "url";
            root.TextField = "text" + sLangID;
            root.ToolTipField = "text" + sLangID;
            root.ValueField = "text" + sLangID;

            QuickAppMenu.DataBindings.Add(root);

            MenuItemBinding child = new MenuItemBinding();
            child.DataMember = "Menu";
            child.NavigateUrlField = "url";
            child.TextField = "text" + sLangID;
            child.ToolTipField = "text" + sLangID;
            child.ValueField = "text" + sLangID;
            QuickAppMenu.DataBindings.Add(child);


        }

        protected void btnMakeApp_Click(object sender, EventArgs e)
        {
            ClsQuickAppInfo objQuickAppInfo = new ClsQuickAppInfo();
            int retVal = -2;
            string sAPP_NUMBER = "";

            try
            {
                string[] arrDIV_ID_DEPT_ID_PC_ID = hidDIV_ID_DEPT_ID_PC_ID.Value.Trim().Split('^');


                int CUST_AGENCY_ID = 0;
                DataTable objDataTable = Cms.BusinessLayer.BlCommon.ClsAgency.GetAgencyWithStatus(txtAPP_EFFECTIVE_DATE.Text, int.Parse(hidCUSTOMER_ID.Value)).Tables[0];
                if (objDataTable.Rows.Count > 0)
                {
                   
                    CUST_AGENCY_ID = Convert.ToInt32(objDataTable.Rows[0]["AGENCY_ID"].ToString());
                  
                }
                objDataTable.Dispose();




                //sAPP_NUMBER = ClsGeneralInformation.GenerateAppPolNumber(int.Parse(hidPOLICY_LOB.Value), int.Parse(arrDIV_ID_DEPT_ID_PC_ID[0]),
                   // ConvertToDate(txtAPP_EFFECTIVE_DATE.Text), "APP");


                sAPP_NUMBER = ClsGeneralInformation.GenerateApplicationNumber(int.Parse(hidPOLICY_LOB.Value),CUST_AGENCY_ID);//Again implemeted by sonal to differentiate application and policy number.n
                retVal = objQuickAppInfo.MakeApp(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value),
                    sAPP_NUMBER, ClsCommon.ConnStr);
            }
            catch
            { retVal = -2; }

            switch (retVal)
            {
                case 1:
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("1108") + " " + sAPP_NUMBER;
                    fetchData();
                    btnMakeApp.Visible = false;
                    btnSave.Visible = false;
                    btnApplication.Visible = true;
                    SetLastVisited(hidCUSTOMER_ID.Value, hidPOLICY_ID.Value, hidPOLICY_VERSION_ID.Value);
                    SetCustomerID(hidCUSTOMER_ID.Value);
                    SetPolicyID(hidPOLICY_ID.Value);
                    SetPolicyVersionID(hidPOLICY_VERSION_ID.Value);

                    break;
                default:
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("1109");
                    break;
            }

            lblMessage.Visible = true;
        }

        protected void QuickAppMenu_MenuItemDataBound(object sender, MenuEventArgs e)
        {
         
            if (((MenuItem)e.Item).Text.ToUpper().Trim() == "QUOTE" || ((MenuItem)e.Item).Text.ToUpper().Trim() == "CITAÇÃO")
            {

                ((MenuItem)e.Item).NavigateUrl = ((MenuItem)e.Item).NavigateUrl + "&CUSTOMER_ID=" + GetCustomerID() + "&APP_ID=" + GetPolicyID() +
                 "&APP_VERSION_ID=" + GetPolicyVersionID() + "&LOB_ID=" + GetLOBID() + "&POLICY_ID="
                + GetPolicyID() + "&POLICY_VERSION_ID=" + GetPolicyVersionID();
            }
          

            if (((MenuItem)e.Item).Text.ToUpper().Trim() != "BILLING INFORMATION" && ((MenuItem)e.Item).Text.Trim() != "Informações de faturamento")
            {
                ((MenuItem)e.Item).SeparatorImageUrl = "../../cmsweb/images/next.gif";
                ((MenuItem)e.Item).Selected = false;
            }
            else
            {
               
                ((MenuItem)e.Item).SeparatorImageUrl = "";
            }

            if (((MenuItem)e.Item).Text.ToUpper().Trim() == "QUICK APPLICATION" || ((MenuItem)e.Item).Text.Trim() == "Aplicação rápida")
            {
                ((MenuItem)e.Item).Selected = true;
            }

            else
            {
                ((MenuItem)e.Item).Selected = false;
            }
          
              
            


        }
    }
}