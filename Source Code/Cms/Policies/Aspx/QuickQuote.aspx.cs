/***************************************************
<Author					: -  Agniswar Das
<Creation Date			: -	 22-Jun-2011
<Description			: -  Add Quick Quote Application
**************************************************** */

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Resources;
using System.Reflection;
using System.Data;
using Cms.DataLayer;
using Cms.CmsWeb.Controls;
using Cms.Model.Policy;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlApplication;
using Cms.CmsWeb;
using Cms.Model.Client;
using Cms.BusinessLayer.BlClient;
using Cms.Model;
namespace Cms.Policies.Aspx
{
    public partial class QuickQuote : Cms.Policies.policiesbase
    {

        public ResourceManager objResourceManager = null;
        public const string ALL_SECURITY = @"<Security><Read>Y</Read><Write>Y</Write><Delete>Y</Delete><Execute>Y</Execute></Security>";
        public const string READ_SECURITY = @"<Security><Read>Y</Read><Write>N</Write><Delete>N</Delete><Execute>N</Execute></Security>";
        public DataSet dsCustomer;
        private ClsCustomer objCustomer=null;
        private static string[] strDateNodes = null;
        public string FirstName;
        private int CustID;
        private int intRetVal; 
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
                    //Remove Hard Code
                    dt3 = ClsGeneralInformation.GetLOBTerms(3).Tables[0];
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
//                return ConvertToDate(sAPP_EFFECTIVE_DATE).AddMonths(int.Parse(sAPP_TERMS)).ToShortDateString();
                DateTime  dt = ConvertToDate(sAPP_EFFECTIVE_DATE).AddMonths(int.Parse(sAPP_TERMS));
                dt = dt.AddDays(-1);
                return dt.ToShortDateString();
            }
            catch
            {
                return "";
            }
        }



        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(QuickQuote));

            base.ScreenId = "";

            lblMessage.Visible = false;

            if (objResourceManager == null)
            {
                objResourceManager = new ResourceManager("Cms.Policies.Aspx.QuickApp", Assembly.GetExecutingAssembly());
                SetCaptions();
            }

            if(GetCustomerID().Trim() != "" )
            {
                hidCUSTOMER_ID.Value = GetCustomerID();
            }

            hidLOB_MSG.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1172");

            if (!IsPostBack)
            {
                #region Set Attributes
                //txtAPP_EFFECTIVE_DATE.Attributes.Add("onblur", "javascript:setExpDate();");
                hlkCalandarDate.Attributes.Add("onclick", "javascript:fPopCalendar(document.getElementById('txtAPP_EFFECTIVE_DATE'),document.getElementById('txtAPP_EFFECTIVE_DATE'))");
                hlkAPP_EXPIRATION_DATE.Attributes.Add("onclick", "javascript:fPopCalendar(document.getElementById('txtAPP_EXPIRATION_DATE'),document.getElementById('txtAPP_EXPIRATION_DATE'))");
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
                    txtAPP_EXPIRATION_DATE.Text = ConvertToDateCulture(DateTime.Now);
                }
            }
            //test
            
            SetButtonsSecurityXml();
            if (GetAttachedCustomerID() != "" && Convert.ToInt32(GetAttachedCustomerID()) != -100 && Convert.ToInt32(GetAttachedCustomerID()) != 0 && GetAttachedCustomerID() != null)
            {
               btnMakeApp.Enabled = true;
            }
            else
            {
                btnMakeApp.Enabled = false;
            }
        }

        private void SetCaptions()
        {
            try
            {
                lblHeader.Text = "Quick Quote Information";// objResourceManager.GetString("lblHeader");
                lblManHeader.Text = objResourceManager.GetString("lblManHeader");
                lblCustomerIndex.Text = objResourceManager.GetString("lblCustomerIndex");
                lblNewCustomerIndex.Text = objResourceManager.GetString("lblNewCustomerIndex");
                lblRate.Text = objResourceManager.GetString("lblRate");
                spnCustomerIndex.Attributes.Add("title", objResourceManager.GetString("lblCustomerIndex"));
                spnNewCustomerIndex.Attributes.Add("title", objResourceManager.GetString("lblNewCustomerIndex"));
                spnRate.Attributes.Add("title", objResourceManager.GetString("lblRate"));

                capPOLICY_LOB.Text = objResourceManager.GetString("cmbPOLICY_LOB");
                //capPOLICY_SUBLOB.Text = objResourceManager.GetString("cmbPOLICY_SUBLOB");
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

        private void BindMenu()
        {
            QuickAppMenu.DataSourceID = "XmlDataSourceMenu";

            string sLangID = ClsCommon.BL_LANG_ID.ToString();

            MenuItemBinding root = new MenuItemBinding();
            root.DataMember = "QuickQuote";
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
                    //if (hidLangCulture.Value.Trim() != cmsbase.DEFAULT_LANG_CULTURE)
                    //{
                    //    hidOldData.Value = FormatXMLDateNode(hidOldData.Value, strDateNodes);
                    //}                   


                    hidappnumber.Value = ClsCommon.FetchValueFromXML("QQ_NUMBER", hidOldData.Value);
                    hidPOLICY_LOB.Value = ClsCommon.FetchValueFromXML("POLICY_LOB", hidOldData.Value);

                    ClsQuickQuote objQQuote = new ClsQuickQuote();
                    //int qq_id = objQQuote.GetQuickQuoteDetails("-100", hidappnumber.Value);
                    int qq_id = objQQuote.GetQuickQuoteDetails(hidCUSTOMER_ID.Value, hidappnumber.Value);

                    SetQQ_ID(qq_id.ToString());
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

        private void SetErrorMessages()
        {
            try
            {
                revAPP_EFFECTIVE_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("22");
                revAPP_EFFECTIVE_DATE.ValidationExpression = aRegExpDate;
                revAPP_EXPIRATION_DATE.ValidationExpression = aRegExpDate;
                rfvPOLICY_LOB.ErrorMessage = ClsMessages.FetchGeneralMessage("97");                
                rfvAPP_TERMS.ErrorMessage = ClsMessages.FetchGeneralMessage("93");
                rfvBILL_TYPE.ErrorMessage = ClsMessages.FetchGeneralMessage("218");
                rfvINSTALL_PLAN_ID.ErrorMessage = ClsMessages.FetchGeneralMessage("731");                
                rfvPOLICY_CURRENCY.ErrorMessage = ClsMessages.FetchGeneralMessage("1070");
            }
            catch (Exception ex)
            {
                throw (ex);
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

        private void fillDropDowns()
        {
            try
            {
                cmbPOLICY_LOB.DataSource = Cms.CmsWeb.ClsFetcher.LOBs;
                cmbPOLICY_LOB.DataTextField = "LOB_DESC";
                cmbPOLICY_LOB.DataValueField = "LOB_ID";
                cmbPOLICY_LOB.DataBind();
                cmbPOLICY_LOB.Items.Insert(0, new ListItem("", ""));
                                
                //cmbPOLICY_CURRENCY.DataSource = Cms.CmsWeb.ClsFetcher.Currency;
                //cmbPOLICY_CURRENCY.DataTextField = "CURR_DESC";
                //cmbPOLICY_CURRENCY.DataValueField = "CURRENCY_ID";
                //cmbPOLICY_CURRENCY.DataBind();
                //cmbPOLICY_CURRENCY.Items.Insert(0, "");

                //cmbPOLICY_LOB.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("PRODSG");
                //cmbPOLICY_LOB.DataTextField = "LookupDesc";
                //cmbPOLICY_LOB.DataValueField = "LookupID";
                //cmbPOLICY_LOB.DataBind();
                //cmbPOLICY_LOB.Items.Insert(0, "-Select-");

                
                /**cmbPOLICY_CURRENCY.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("CURR"); Commented by Amit Kr. Mishra
                ////cmbPOLICY_CURRENCY.DataTextField = "LookupDesc";
                ////cmbPOLICY_CURRENCY.DataValueField = "LookupID";**/
               
                cmbPOLICY_CURRENCY.DataSource = Cms.CmsWeb.ClsFetcher.Currency;//Added by Amit Kr. Mishra
                cmbPOLICY_CURRENCY.DataTextField = "CURR_DESC";
                cmbPOLICY_CURRENCY.DataValueField = "CURRENCY_ID";
                cmbPOLICY_CURRENCY.DataBind();

                //cmbPOLICY_CURRENCY.Items.Insert(0, "-Select-");
                cmbPOLICY_CURRENCY.SelectedIndex = 2;

                //int iLOB_ID = GetLOBPRODUCT(cmbPOLICY_LOB.SelectedValue); // Remove Hard code. Make it LOB specific

                //DataTable dt = ClsGeneralInformation.GetLOBTerms(iLOB_ID).Tables[0];
                //cmbAPP_TERMS.DataSource = dt;
                //cmbAPP_TERMS.DataTextField = "LOOKUP_VALUE_DESC";
                //cmbAPP_TERMS.DataValueField = "LOOKUP_UNIQUE_ID";
                //cmbAPP_TERMS.DataBind();


                //dt = ClsGeneralInformation.GetBillType(iLOB_ID, "POL");
                //cmbBILL_TYPE_ID.DataSource = dt;
                //cmbBILL_TYPE_ID.DataTextField = "LOOKUP_VALUE_DESC";
                //cmbBILL_TYPE_ID.DataValueField = "LOOKUP_UNIQUE_ID";
                //cmbBILL_TYPE_ID.DataBind();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private int GetLOBPRODUCT(string LOB_DESC)
        {
            int LOBID = 0;
            switch (LOB_DESC.ToUpper())
            {
                case "HOME":
                    LOBID = 1;
                    break;
                case "MOTOR":
                    LOBID = 1;
                    break;
                case "FIRE":
                    LOBID = 2;
                    break;
                case "MARINE CARGO":
                    LOBID = 3;
                    break;
                default:
                    LOBID = 1;
                    break;
            }

            return LOBID;
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


        private void SetLastVisited(string CUSTOMER_ID, string POLICY_ID, string POLICY_VERSION_ID)
        {
            try
            {

                string quote_Details = CUSTOMER_ID + "~" + POLICY_ID + "~" + POLICY_VERSION_ID + "~" + DateTime.Now.ToString() + "~" + GetQQ_ID();
                ClsGeneralInformation objLastVisitedQuote = new ClsGeneralInformation();
                objLastVisitedQuote.UpdateLastVisitedPageEntry("Quote", quote_Details, int.Parse(GetUserId()), GetSystemId());
            }
            catch (Exception ex)
            {
                throw (ex);
            }
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

        private ClsQuickAppInfo GetFormValue()
        {
            ClsQuickAppInfo objQuickAppInfo = new ClsQuickAppInfo();

            try
            {
                objQuickAppInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value);
                objQuickAppInfo.POLICY_LOB.CurrentValue = hidPOLICY_LOB.Value;
                objQuickAppInfo.POLICY_SUBLOB.CurrentValue = hidPOLICY_SUBLOB.Value.Trim();
                objQuickAppInfo.APP_TERMS.CurrentValue = hidAPP_TERMS.Value.Trim();
                // Effective Date and Expiration Date Edited by Agniswar for Singapore Implementation on 13/09/2011
                //DateTime dtEffDate = DateTime.Parse(txtAPP_EFFECTIVE_DATE.Text.Trim().ToString());
                //string strEffDate = String.Format("{0:MM/dd/yyyy}", dtEffDate);
                //objQuickAppInfo.APP_EFFECTIVE_DATE.CurrentValue = ConvertToDate(strEffDate);

                //DateTime dtExpDate = objQuickAppInfo.APP_EFFECTIVE_DATE.CurrentValue.AddMonths(int.Parse(objQuickAppInfo.APP_TERMS.CurrentValue));
                //string strExpDate = String.Format("{0:MM/dd/yyyy}", dtExpDate);
                //objQuickAppInfo.APP_EXPIRATION_DATE.CurrentValue = ConvertToDate(strExpDate);

                objQuickAppInfo.APP_EFFECTIVE_DATE.CurrentValue = ConvertToDate(txtAPP_EFFECTIVE_DATE.Text.Trim());
                if (objQuickAppInfo.APP_TERMS.CurrentValue != "0")
                {
                    objQuickAppInfo.APP_EXPIRATION_DATE.CurrentValue =
                        objQuickAppInfo.APP_EFFECTIVE_DATE.CurrentValue.AddMonths(int.Parse(objQuickAppInfo.APP_TERMS.CurrentValue));
                    objQuickAppInfo.APP_EXPIRATION_DATE.CurrentValue = objQuickAppInfo.APP_EXPIRATION_DATE.CurrentValue.AddDays(-1);
                }
                else
                {
                    objQuickAppInfo.APP_EXPIRATION_DATE.CurrentValue = ConvertToDate(hidAPP_EXPIRATION_DATE.Value);
                }

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


        #region Buttons event
        protected void btnNext_Click(object sender, EventArgs e)
        {
            //Cache.Insert("Level", "1", null, DateTime.Now.AddHours(1), TimeSpan.FromMinutes(30), System.Web.Caching.CacheItemPriority.Normal, null);
            Cache["Level"] = "1";
            Response.Redirect(@"/Cms/Policies/Aspx/QuickQuoteFrame.aspx", true);
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

                        //hidCUSTOMER_ID.Value = "-100";

                        SetCustomerID(hidCUSTOMER_ID.Value);
                        SetPolicyID(hidPOLICY_ID.Value);
                        SetPolicyVersionID(hidPOLICY_VERSION_ID.Value);

                        //ClsQuickQuote objQQuote = new ClsQuickQuote();
                        //int qq_id = objQQuote.GetQuickQuoteDetails("-100","","");

                        ////SetQQ_ID(returnValue.ToString());

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

        protected void btnMakeApp_Click(object sender, EventArgs e)
        {
            ClsQuickAppInfo objQuickAppInfo = new ClsQuickAppInfo();
            int retVal = -2;
            string sAPP_NUMBER = "";

            try
            {
                string[] arrDIV_ID_DEPT_ID_PC_ID = hidDIV_ID_DEPT_ID_PC_ID.Value.Trim().Split('^');
                //if (GetAttachedCustomerID() != "" && Convert.ToInt32(GetAttachedCustomerID()) != -100 && Convert.ToInt32(GetAttachedCustomerID()) != 0 && GetAttachedCustomerID() != null)
                //{
                //    hidCUSTOMER_ID.Value = GetAttachedCustomerID();
                //}

                int CUST_AGENCY_ID = 0;
                DataTable objDataTable = Cms.BusinessLayer.BlCommon.ClsAgency.GetAgencyWithStatus(txtAPP_EFFECTIVE_DATE.Text, int.Parse(hidCUSTOMER_ID.Value)).Tables[0];
                if (objDataTable.Rows.Count > 0)
                {

                    CUST_AGENCY_ID = Convert.ToInt32(objDataTable.Rows[0]["AGENCY_ID"].ToString());

                }
                objDataTable.Dispose();




                //sAPP_NUMBER = ClsGeneralInformation.GenerateAppPolNumber(int.Parse(hidPOLICY_LOB.Value), int.Parse(arrDIV_ID_DEPT_ID_PC_ID[0]),
                // ConvertToDate(txtAPP_EFFECTIVE_DATE.Text), "APP");


                Cms.BusinessLayer.BlQuote.ClsQuoteDetails objQuoteDetails = new Cms.BusinessLayer.BlQuote.ClsQuoteDetails();
              
                ClsQuickQuote objQQuote = new ClsQuickQuote();
                if (objQQuote.GetQuickQuoteDetails(hidCUSTOMER_ID.Value, GetQQ_ID()).ToString() != "")
                {
                    hidQQ_ID.Value = objQQuote.GetQuickQuoteDetails(hidCUSTOMER_ID.Value, GetQQ_ID()).ToString();
                }
                else
                    hidQQ_ID.Value = "0";
                getFormValue();
               
                DataSet dsRate = objQuoteDetails.FetchVehicleRatingDetail(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), int.Parse(hidQQ_ID.Value));
                DataTable dtRate = dsRate.Tables[0];
                if (dtRate.Rows.Count != 0)
                {


                } 

                Model.Client.ClsCustomerInfo objNewCustomer = new Model.Client.ClsCustomerInfo();
                objCustomer = new ClsCustomer();
                objNewCustomer = getFormValue();
                //Comment by kuldeep as there was no code inside on 9_jan_2012
                //if (objNewCustomer.CustomerId.ToString() != "-100")
                //{
                //    //objCustomer.Update();
                //}
                if (GetLOBID() != "13")
                {
                    if (int.Parse(hidCUSTOMER_ID.Value) == -100)
                    {
                        intRetVal = objCustomer.Add(objNewCustomer, out CustID, FirstName);
                        hidCUSTOMER_ID.Value = CustID.ToString();
                        //SetCustomerID(hidCUSTOMER_ID.Value);

                    }
                }
                else
                {
                    Cms.Model.Quote.ClsInvoiceDetailQQInfo objNewMarineCustomer = new Model.Quote.ClsInvoiceDetailQQInfo();
                    Cms.BusinessLayer.BlQuote.ClsInvoiceDetailQQ objNewMarineCustomerBL = new BusinessLayer.BlQuote.ClsInvoiceDetailQQ();
                    objNewMarineCustomer = getFormValueMarine();
                    if (int.Parse(hidCUSTOMER_ID.Value) == -100)
                    {
                        intRetVal = objNewMarineCustomerBL.AddQQCustomer(objNewMarineCustomer, out CustID);
                        hidCUSTOMER_ID.Value = CustID.ToString();
                        //SetCustomerID(hidCUSTOMER_ID.Value);

                    }
                }
                
                sAPP_NUMBER = ClsGeneralInformation.GenerateApplicationNumber(int.Parse(hidPOLICY_LOB.Value), CUST_AGENCY_ID);//Again implemeted by sonal to differentiate application and policy number.n

                if (int.Parse(hidPOLICY_LOB.Value) == 38)
                {
                    retVal = objQuickAppInfo.MakeApp(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), sAPP_NUMBER, ClsCommon.ConnStr);
                }
                else
                {
                    retVal = objQuickAppInfo.MakeApp_MarineCargo(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value), sAPP_NUMBER, ClsCommon.ConnStr);
                }

            }
            catch(Exception ex)
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
                    SetAttachedCustomerID("");
                    break;
                default:
                    lblMessage.Text = ClsMessages.FetchGeneralMessage("1109");
                    break;
            }
           
            lblMessage.Visible = true;
        }

        //Added By Kuldeep for Marine QQ
        private  Cms.Model.Quote.ClsInvoiceDetailQQInfo getFormValueMarine()
        {
            Model.Quote.ClsInvoiceDetailQQInfo objInvoiceInfo = new Model.Quote.ClsInvoiceDetailQQInfo();
            Cms.BusinessLayer.BlQuote.ClsInvoiceDetailQQ objCustDetails = new Cms.BusinessLayer.BlQuote.ClsInvoiceDetailQQ();
            DataSet dsNewMarineCust = objCustDetails.FetchInvoiceParticularDetail(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidQQ_ID.Value));
            objInvoiceInfo.COMPANY_NAME = dsNewMarineCust.Tables[0].Rows[0]["COMPANY_NAME"].ToString() != "" ? dsNewMarineCust.Tables[0].Rows[0]["COMPANY_NAME"].ToString() : null;
            objInvoiceInfo.CUSTOMER_TYPE = dsNewMarineCust.Tables[0].Rows[0]["CUSTOMER_TYPE"].ToString() != "" ? int.Parse(dsNewMarineCust.Tables[0].Rows[0]["CUSTOMER_TYPE"].ToString()) : 0;

            objInvoiceInfo.BUSINESS_TYPE = dsNewMarineCust.Tables[0].Rows[0]["BUSINESS_TYPE"].ToString() != "" ? int.Parse(dsNewMarineCust.Tables[0].Rows[0]["BUSINESS_TYPE"].ToString()) : 0; 
            objInvoiceInfo.IS_ACTIVE = "Y";
           


            string strSystemID = GetSystemId();


            return objInvoiceInfo;

        }
        private ClsCustomerInfo getFormValue()
        {
            
           
            Cms.BusinessLayer.BlQuote.ClsCustomerParticularDetails objCustDetails = new Cms.BusinessLayer.BlQuote.ClsCustomerParticularDetails();
            dsCustomer = objCustDetails.FetchCustomerParticularDetail(int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidQQ_ID.Value));
            Model.Client.ClsCustomerInfo objCustomerInfo = new Model.Client.ClsCustomerInfo();
            if (dsCustomer != null && dsCustomer.Tables[0].Rows.Count > 0)
            {
                
                NfiBaseCurrency.NumberDecimalDigits = 2;
                objCustomerInfo.CustomerId = int.Parse(hidCUSTOMER_ID.Value);// Convert.ToInt32(Request.Params["CUSTOMER_ID"]);
                objCustomerInfo.CustomerType = dsCustomer.Tables[0].Rows[0]["CUSTOMER_TYPE"].ToString()!=""?dsCustomer.Tables[0].Rows[0]["CUSTOMER_TYPE"].ToString():null;
                objCustomerInfo.CustomerParent = 0;
                objCustomerInfo.PREFIX = 0;
                
                objCustomerInfo.CustomerFirstName = dsCustomer.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"].ToString() != "" ? dsCustomer.Tables[0].Rows[0]["CUSTOMER_FIRST_NAME"].ToString() : null;
                objCustomerInfo.CustomerMiddleName = dsCustomer.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString() != "" ? dsCustomer.Tables[0].Rows[0]["CUSTOMER_MIDDLE_NAME"].ToString() : null;
                objCustomerInfo.CustomerLastName = dsCustomer.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"].ToString() != "" ? dsCustomer.Tables[0].Rows[0]["CUSTOMER_LAST_NAME"].ToString() : null;
                objCustomerInfo.CustomerCode = dsCustomer.Tables[0].Rows[0]["CUSTOMER_CODE"].ToString() != "" ? dsCustomer.Tables[0].Rows[0]["CUSTOMER_CODE"].ToString() : null;
                objCustomerInfo.CustomerZip = dsCustomer.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString() != "" ? dsCustomer.Tables[0].Rows[0]["CUSTOMER_ZIP"].ToString() : null;
                objCustomerInfo.CustomerAddress1 = dsCustomer.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString() != "" ? dsCustomer.Tables[0].Rows[0]["CUSTOMER_ADDRESS1"].ToString() : null;
                objCustomerInfo.NUMBER = string.Empty;
                objCustomerInfo.CustomerAddress2 = dsCustomer.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString() != "" ? dsCustomer.Tables[0].Rows[0]["CUSTOMER_ADDRESS2"].ToString() : null;
                objCustomerInfo.CustomerCity =dsCustomer.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString() != "" ? dsCustomer.Tables[0].Rows[0]["CUSTOMER_CITY"].ToString() : null;
                objCustomerInfo.DATE_OF_BIRTH = DateTime.Parse(dsCustomer.Tables[0].Rows[0]["DATE_OF_BIRTH"].ToString()) != DateTime.MinValue ? DateTime.Parse(dsCustomer.Tables[0].Rows[0]["DATE_OF_BIRTH"].ToString()) : DateTime.MinValue;
                objCustomerInfo.CustomerCountry = dsCustomer.Tables[0].Rows[0]["NATIONALITY"].ToString() != "" ? dsCustomer.Tables[0].Rows[0]["NATIONALITY"].ToString() : null;
                objCustomerInfo.GENDER = dsCustomer.Tables[0].Rows[0]["GENDER"].ToString() != "" ? dsCustomer.Tables[0].Rows[0]["GENDER"].ToString() : null; 
                objCustomerInfo.NATIONALITY = dsCustomer.Tables[0].Rows[0]["NATIONALITY"].ToString() != "" ? dsCustomer.Tables[0].Rows[0]["NATIONALITY"].ToString() : null;
                objCustomerInfo.CREATED_BY = dsCustomer.Tables[0].Rows[0]["CREATED_BY"].ToString() != "" ? int.Parse(dsCustomer.Tables[0].Rows[0]["CREATED_BY"].ToString()) : 0;
                objCustomerInfo.CREATED_DATETIME = DateTime.Parse(dsCustomer.Tables[0].Rows[0]["CREATED_DATETIME"].ToString()) != DateTime.MinValue ? DateTime.Parse(dsCustomer.Tables[0].Rows[0]["CREATED_DATETIME"].ToString()) : DateTime.Now;

            }

            return objCustomerInfo;             
            //End 
            }

           

        protected void QuickAppMenu_MenuItemDataBound(object sender, MenuEventArgs e)
        {

            if (((MenuItem)e.Item).Text.ToUpper().Trim() == "QUICKQUOTE INFO")
            {
                ((MenuItem)e.Item).Selected = true;
            }

            else
            {
                ((MenuItem)e.Item).Selected = false;
            }

            if (((MenuItem)e.Item).Text.ToUpper().Trim() == "CUSTOMER DETAILS")
            {
                ((MenuItem)e.Item).Selected = true;
            }

            else
            {
                ((MenuItem)e.Item).Selected = false;
            }

            if (((MenuItem)e.Item).Text.ToUpper().Trim() == "RISK INFORMATION")
            {
                ((MenuItem)e.Item).Selected = true;
            }

            else
            {
                ((MenuItem)e.Item).Selected = false;
            }

            if (((MenuItem)e.Item).Text.ToUpper().Trim() == "QUOTE")
            {

                ((MenuItem)e.Item).NavigateUrl = ((MenuItem)e.Item).NavigateUrl + "&CUSTOMER_ID=" + GetCustomerID() + "&APP_ID=" + GetPolicyID() +
                 "&APP_VERSION_ID=" + GetPolicyVersionID() + "&LOB_ID=" + GetLOBID() + "&POLICY_ID="
                + GetPolicyID() + "&POLICY_VERSION_ID=" + GetPolicyVersionID();
            }


            if (((MenuItem)e.Item).Text.ToUpper().Trim() != "QUOTE INFORMATION" && ((MenuItem)e.Item).Text.Trim() != "Informações de faturamento")
            {
                ((MenuItem)e.Item).SeparatorImageUrl = "../../cmsweb/images/next.gif";
                ((MenuItem)e.Item).Selected = false;
            }
            else
            {

                ((MenuItem)e.Item).SeparatorImageUrl = "";
            }

            


        }



        #endregion
    }
}
