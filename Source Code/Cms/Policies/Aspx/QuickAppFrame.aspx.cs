/***************************************************
<Author					: -  Charles Gomes
<Creation Date			: -	 11-Jun-2010
<Description			: -  Quick Application Frame
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
    public partial class QuickAppFrame : Cms.Policies.policiesbase
    {
        public const string FRAME_URL = @"/Cms/Policies/Aspx/QuickAppFrame.aspx?";
        public const string PREV_HOME_URL = @"/Cms/Policies/Aspx/QuickApp.aspx?CALLEDFROM=PREV";
        public const string COVERAGE_URL = @"/Cms/Policies/Aspx/AddPolicyCoverages.aspx?CALLEDFROM=QAPP&amp;risk_id=";
        public const string BILLING_URL = @"/Cms/Policies/Aspx/BillingInfo.aspx?CALLEDFROM=QAPP";
        //public const string RATE_URL = @"/cms/application/Aspx/QuoteGenerator.aspx?Level=3&amp;CALLEDFROM=QAPP";
        public const string RATE_URL = @"/cms/application/Aspx/QuoteGenerator.aspx?Level=3&CALLEDFROM=QAPP";

        public const string ALL_SECURITY = @"<Security><Read>Y</Read><Write>Y</Write><Delete>Y</Delete><Execute>Y</Execute></Security>";
        public string LINKVAL = "";
        private ResourceManager objResourceManager = null;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "";

            lblMessage.Visible = false;
            Ajax.Utility.RegisterTypeForAjax(typeof(QuickAppFrame));
            if (objResourceManager == null)
            {
                objResourceManager = new ResourceManager("Cms.Policies.Aspx.QuickAppFrame", Assembly.GetExecutingAssembly());
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
                lblHeader.Text = GET_GEADER; //Show header for pages with no header
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
                    lblMessage.Text = CmsWeb.ClsMessages.FetchGeneralMessage("1102");
                    return;
                }

                trRisk.Visible = true;
                fillRiskDropDown();
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

        public string GET_GEADER
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
            switch (level)
            {
                case "0": url = PREV_HOME_URL;
                    break;
                case "1":
                    ClsQuickAppInfo.RISK_URL = GetLOBID();
                    url = ClsQuickAppInfo.RISK_URL;
                    break;
                //case "2":
                //    url = COVERAGE_URL;                  
                //    if (hidrisk_id.Value.Trim()!="0" && hidrisk_id.Value.Trim()!="")
                //    {
                //        url = url + hidrisk_id.Value.Trim();
                //    }
                //    break;
                case "2"://"3":
                    url = RATE_URL + "&CUSTOMER_ID=" + GetCustomerID() + "&APP_ID=" + GetPolicyID() +
                 "&APP_VERSION_ID=" + GetPolicyVersionID() + "&LOB_ID=" + GetLOBID() + "&POLICY_ID="
                + GetPolicyID() + "&POLICY_VERSION_ID=" + GetPolicyVersionID();
                    break;
                case "3":
                    url = BILLING_URL;
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
                    linkval = "Risk Information";
                    break;
                case "2":
                    linkval = "Quote";
                    break;
                case "3":
                    linkval = "Billing Information";
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
            root.DataMember = "QuickApp";
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
            if (((MenuItem)e.Item).Text.ToUpper().Trim() != "BILLING INFORMATION" && ((MenuItem)e.Item).Text.Trim() != "Informações de faturamento")
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
