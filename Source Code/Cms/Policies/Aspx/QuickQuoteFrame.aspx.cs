/***************************************************
<Author					: -  Agniswar Das
<Creation Date			: -	 22-Jun-2011
<Description			: -  Quick Quote Frame
**************************************************** */
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Resources;
using System.Reflection;
using System.Data;
using Cms.CmsWeb.Controls;
using Cms.Model.Policy;
using Cms.BusinessLayer.BlCommon;

namespace Cms.Policies.Aspx
{
    public partial class QuickQuoteFrame : Cms.Policies.policiesbase
    {
        public const string FRAME_URL = @"/Cms/Policies/Aspx/QuickQuoteFrame.aspx?";
        public const string PREV_HOME_URL = @"/Cms/Policies/Aspx/QuickQuote.aspx?CALLEDFROM=PREV";
        public const string COVERAGE_URL = @"/Cms/Policies/Aspx/AddPolicyCoverages.aspx?CALLEDFROM=QAPP&amp;risk_id=";
        
        //public const string RATE_URL = @"/cms/application/Aspx/QuoteGenerator.aspx?Level=3&amp;CALLEDFROM=QAPP";
        public const string CUST_DETAIL_URL = @"/cms/cmsWeb/aspx/PersonalDetailQQ.aspx?";
        public const string RISK_URL = @"/cms/cmsWeb/aspx/QuoteDetails.aspx?";

        //Added by Ruchika Chauhan for TFS Bug # 4171
        public const string RISK_MARINE_URL = @"/cms/cmsWeb/aspx/QQMarineRiskDetails.aspx?";

        public const string RATE_URL = @"/cms/application/Aspx/QuoteGenerator.aspx?Level=3&CALLEDFROM=QAPP";
        public const string BILLING_URL = @"/cms/cmsWeb/aspx/QuoteInformation.aspx?CALLEDFROM=QAPP";
        //public const string CUST_DETAIL_URL = @"/cms/client/Aspx/AddCustomerQQ.aspx?";
       
        //Added by Kuldeep for marine Cargo
        public const string INVOICE_DETAIL_URL = @"/cms/cmsWeb/aspx/InvoiceDetailQQ.aspx?";
        public const string BILLING_URL_MARINE = @"/cms/cmsweb/Construction.html";
        //TILL HERE

        public const string ALL_SECURITY = @"<Security><Read>Y</Read><Write>Y</Write><Delete>Y</Delete><Execute>Y</Execute></Security>";
        public string LINKVAL = "";
        private ResourceManager objResourceManager = null;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "";

            lblMessage.Visible = false;
            Ajax.Utility.RegisterTypeForAjax(typeof(QuickQuoteFrame));
            if (objResourceManager == null)
            {
                objResourceManager = new ResourceManager("Cms.Policies.Aspx.QuickQuoteFrame", Assembly.GetExecutingAssembly());
            }

            SetButtonsSecurityXml();

            if (!IsPostBack)
            {
                BindMenu();
            }

            if (Request.QueryString["Level"] != null && Request.QueryString["Level"].ToString().Trim() != "")
            {
                hidLevel.Value = Request.QueryString["Level"].ToString().Trim();
            }
            else
            {
                if (Cache["Level"] != null && Cache["Level"].ToString().Trim() != "")
                {
                    hidLevel.Value = Cache["Level"].ToString();
                }
                else
                {
                    Response.Redirect(PREV_HOME_URL, true);
                }
            }

            if (hidLevel.Value == "1")
            {
                lblHeader.Text = "Particular of Proposer"; //Show header for pages with no header
            }
            else if (hidLevel.Value == "2")
            {
                lblHeader.Text = "Risk Information";//GET_HEADER; //Show header for pages with no header
            }
            else if (hidLevel.Value == "3")
            {
                lblHeader.Text = "Quote";//GET_HEADER; //Show header for pages with no header
            }
            else if (hidLevel.Value == "4")
            {
                lblHeader.Text = "Quote Information";//GET_HEADER; //Show header for pages with no header
            }
            else
            {
                trHeader.Visible = false;
            }

            if (hidLevel.Value.Trim() == "0")
            {
                Response.Redirect(PREV_HOME_URL, true);
            }
            //else if (hidLevel.Value.Trim() == "2")
            //{
            //    if (Cache["risk_id"] == null || Cache["risk_id"].ToString().Trim() == "")
            //    {
            //        lblMessage.Visible = true;
            //        lblMessage.Text = CmsWeb.ClsMessages.FetchGeneralMessage("1103");
            //        return;
            //    }
            //    else
            //    {
            //        hidrisk_id.Value = Cache["risk_id"].ToString().Trim();
            //    }
            //}            

            SetUrl(hidLevel.Value);
            if (hidLevel.Value.Trim() == "1")
            {
                if (hidFrameUrl.Value.Trim() == "")
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Particular of Proposer";// CmsWeb.ClsMessages.FetchGeneralMessage("1102");
                    return;
                }

                tabLayer.Attributes["src"] = hidFrameUrl.Value;
                trRisk.Visible = false;
            }
            else if (hidLevel.Value.Trim() == "2")
            {
                if (hidFrameUrl.Value.Trim() == "")
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Risk Information";// CmsWeb.ClsMessages.FetchGeneralMessage("1102");
                    return;
                }

                tabLayer.Attributes["src"] = hidFrameUrl.Value;
                trRisk.Visible = false;
                
            }
            else if (hidLevel.Value.Trim() == "3")
            {
                if (hidFrameUrl.Value.Trim() == "")
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Quote";// CmsWeb.ClsMessages.FetchGeneralMessage("1102");
                    return;
                }

                tabLayer.Attributes["src"] = hidFrameUrl.Value;
                trRisk.Visible = false;

            }
            else if (hidLevel.Value.Trim() == "4")
            {
                if (hidFrameUrl.Value.Trim() == "")
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Quote Information";// CmsWeb.ClsMessages.FetchGeneralMessage("1102");
                    return;
                }

                tabLayer.Attributes["src"] = hidFrameUrl.Value;
                trRisk.Visible = false;

            }
            else
            {
                tabLayer.Attributes["src"] = hidFrameUrl.Value;
                trRisk.Visible = false;
            }
            if (hidrisk_id.Value == "0" && cmbRisk.SelectedValue != "")
                hidrisk_id.Value = cmbRisk.SelectedValue;


            TabCtl.TabURLs = hidFrameUrl.Value + hidrisk_id.Value + "&CUSTOMER_ID=" + GetCustomerID()
                + "&POL_ID=" + GetPolicyID()
                + "&POL_VERSION_ID=" + GetPolicyVersionID() + "&";
            ClsQuickAppInfo.SCREEN_ID = GetLOBID();

            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(ClsQuickAppInfo.SCREEN_ID, "TabCtl");

        }

        private void fillRiskDropDown()
        {
            ClsQuickAppInfo obj = new ClsQuickAppInfo();

            cmbRisk.DataSource = obj.fillRisk(int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()), ClsCommon.ConnStr);
            cmbRisk.DataTextField = "value";
            cmbRisk.DataValueField = "id";
            cmbRisk.DataBind();
            cmbRisk.Items.Insert(0, new ListItem("", ""));
        }

        public string GET_HEADER
        {
            get
            {
                return objResourceManager.GetString("lblHeader" + hidLevel.Value);
            }
        }

        private void SetUrl(string strLevel)
        {
            int iLevel = int.Parse(strLevel);
            hidFrameUrl.Value = getUrl(strLevel);
            hidNextUrl.Value = getUrl((iLevel + 1).ToString());

            if (hidNextUrl.Value.Trim() == "")
            {
                btnNext.Visible = false;
            }

            hidPrevUrl.Value = getUrl((iLevel - 1).ToString());
        }

        private string getUrl(string level)
        {
            string url = "";
            //Added by Kuldeep for marine cargo
            
           
            switch (level)
            {
                case "0": url = PREV_HOME_URL;
                    break;

                case "1":
                    //added by kuldeep for marine cargo
                    if (GetLOBID() == "13")
                    {
                        url = INVOICE_DETAIL_URL + "&CUSTOMER_ID=" + GetCustomerID() + "&APP_ID=" + GetPolicyID() +
                      "&APP_VERSION_ID=" + GetPolicyVersionID() + "&LOB_ID=" + GetLOBID() + "&POLICY_ID="
                     + GetPolicyID() + "&POLICY_VERSION_ID=" + GetPolicyVersionID() + "&QUOTE_NUM=" + GetQQ_ID();
                    }
                    else
                    {
                        url = CUST_DETAIL_URL + "&CUSTOMER_ID=" + GetCustomerID() + "&APP_ID=" + GetPolicyID() +
                     "&APP_VERSION_ID=" + GetPolicyVersionID() + "&LOB_ID=" + GetLOBID() + "&POLICY_ID="
                    + GetPolicyID() + "&POLICY_VERSION_ID=" + GetPolicyVersionID() + "&QUOTE_NUM=" + GetQQ_ID();
                    }
                    break;

                case "2":
                    //Make LOB Specific
                    //ClsQuickAppInfo.RISK_URL = GetLOBID();
                    //url = ClsQuickAppInfo.RISK_URL;

                    //Added by Ruchika for QuickQuote Risk Information for Marine Cargo
                    if (GetLOBID() == "13")
                    {
                         url = RISK_MARINE_URL + "&CUSTOMER_ID=" + GetCustomerID() + "&APP_ID=" + GetPolicyID() +
                     "&APP_VERSION_ID=" + GetPolicyVersionID() + "&LOB_ID=" + GetLOBID() + "&POLICY_ID="
                     + GetPolicyID() + "&POLICY_VERSION_ID=" + GetPolicyVersionID() + "&QUOTE_NUM=" + GetQQ_ID();
                    }
                    else
                    {
                         url = RISK_URL + "&CUSTOMER_ID=" + GetCustomerID() + "&APP_ID=" + GetPolicyID() +
                    "&APP_VERSION_ID=" + GetPolicyVersionID() + "&LOB_ID=" + GetLOBID() + "&POLICY_ID="
                    + GetPolicyID() + "&POLICY_VERSION_ID=" + GetPolicyVersionID() + "&QUOTE_NUM=" + GetQQ_ID();
                    }
                   
                    break;

                //case "2":
                //    url = COVERAGE_URL;                  
                //    if (hidrisk_id.Value.Trim()!="0" && hidrisk_id.Value.Trim()!="")
                //    {
                //        url = url + hidrisk_id.Value.Trim();
                //    }
                //    break;

                case "3"://"3":
                    string showDetails = "";
                    if (Request.QueryString["SHOWRATE"] != null)
                    {
                        showDetails = Request.QueryString["SHOWRATE"].ToString();
                    }
                    url = RATE_URL + "&CUSTOMER_ID=" + GetCustomerID() + "&POLICY_ID=" + GetPolicyID() +
                 "&POLICY_VERSION_ID=" + GetPolicyVersionID() + "&LOB_ID=" + GetLOBID() 
                 + "&SHOWRATE=" + showDetails;
                //+ "&CALLEDFROM=QQUOTE";
                    break;

                case "4":
                    if (GetLOBID() == "13")
                    {
                        url = BILLING_URL_MARINE;
                    }
                    else
                    {
                        url = BILLING_URL + "&CUSTOMER_ID=" + GetCustomerID() + "&APP_ID=" + GetPolicyID() +
                     "&APP_VERSION_ID=" + GetPolicyVersionID() + "&LOB_ID=" + GetLOBID() + "&POLICY_ID="
                    + GetPolicyID() + "&POLICY_VERSION_ID=" + GetPolicyVersionID() + "&QUOTE_NUM=" + GetQQ_ID();
                    }
                    break;               
            }
            return url;
        }


        private string getlinkval(string level)
        {
            string linkval = "";
            switch (level)
            {
                case "1":
                    linkval = "Customer Details";
                    break;
                case "2":
                    linkval = "Risk Information";
                    break;
                case "3":
                    linkval = "Quote";
                    break;
                case "4":
                    linkval = "Quote Information";
                    break;

            }
            return linkval;
        }

        private void SetButtonsSecurityXml()
        {
            try
            {
                btnNext.CmsButtonClass = CmsButtonType.Read;
                btnNext.PermissionString = ALL_SECURITY;

                btnPrevious.CmsButtonClass = CmsButtonType.Read;
                btnPrevious.PermissionString = ALL_SECURITY;

                btnAdd.CmsButtonClass = CmsButtonType.Execute;
                btnAdd.PermissionString = gstrSecurityXML;
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
            //root.NavigateUrlField = "url";
            root.NavigateUrl = PREV_HOME_URL;
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

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Cache["Level"] = int.Parse(hidLevel.Value) + 1;
            Response.Redirect(FRAME_URL, true);
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Cache["Level"] = int.Parse(hidLevel.Value) - 1;

            if (Cache["Level"].ToString() == "0")
            {
                Response.Redirect(PREV_HOME_URL, true);
            }
            else
            {
                Response.Redirect(FRAME_URL, true);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            tabLayer.Attributes["src"] = hidFrameUrl.Value + "NEW";
        }

        protected void cmbRisk_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRisk.SelectedValue != "")
            {
                tabLayer.Attributes["src"] = hidFrameUrl.Value + cmbRisk.SelectedValue;
            }
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet AjaxFetchRisk()
        {

            DataSet ds = null;
            try
            {
                ds = new DataSet();
                DataTable dt1 = new DataTable();
                ClsQuickAppInfo obj = new ClsQuickAppInfo();
                dt1 = obj.fillRisk(int.Parse(GetCustomerID()), int.Parse(GetPolicyID()), int.Parse(GetPolicyVersionID()), ClsCommon.ConnStr);
                ds.Tables.Add(dt1.Copy());
                ds.Tables[0].TableName = "RISKS";
                return ds;
            }
            catch
            {
                return null;

            }
        }

        protected void QuickAppMenu_MenuItemDataBound(object sender, MenuEventArgs e)
        {

            LINKVAL = getlinkval(hidLevel.Value);
            if (((MenuItem)e.Item).Text.ToUpper().Trim() != "QUOTE INFORMATION")
            {

                ((MenuItem)e.Item).SeparatorImageUrl = "../../cmsweb/images/next.gif";
            }

            if (((MenuItem)e.Item).Text.ToUpper().Trim() == LINKVAL.ToUpper().Trim())
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
