/******************************************************************************************
	<Author					: -> Anurag Verma
	<Start Date				: -> 23 June 2005
	<Description			: -> To present mixed search of customer, policies 

	*******************************************************************************************/

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
using Cms.CmsWeb.WebControls;
using Cms.BusinessLayer.BlCommon;
using System.Resources;
using System.Reflection;

namespace Cms.Client.Aspx
{
    /// <summary>
    /// Summary description for CustomerManagerSearch.
    /// </summary>
    public class CustomerManagerSearch : Cms.CmsWeb.cmsbase
    {
        protected System.Web.UI.WebControls.Panel pnlGrid;
        protected System.Web.UI.HtmlControls.HtmlForm indexForm;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected System.Web.UI.WebControls.Label lblTemplate;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected Cms.CmsWeb.WebControls.Menu bottomMenu;

        protected System.Web.UI.HtmlControls.HtmlContainerControl htmlBody;
        public string strCalledFor = "";
        ResourceManager objResourceMgr = null;

        private void Page_Load(object sender, System.EventArgs e)
        {
            base.ScreenId = "132";
            if (Request.QueryString["CalledFor"] != null && Request.QueryString["CalledFor"] != "")
            {
                strCalledFor = Request.QueryString["CalledFor"].ToString();
            }
            objResourceMgr = new ResourceManager("Cms.client.Aspx.CustomerManagerSearch", Assembly.GetExecutingAssembly());

            if (GetSystemId() == "I001" || GetSystemId() == "IUAT")
            {
                htmlBody.Attributes.Add("onload", "javascript:CreateMenu();setMenu();setfirstTime();");
                bottomMenu.Visible = false;
            }
            else
            {
                htmlBody.Attributes.Add("onload", "javascript:CreateMenu();setMenu();setfirstTime();top.topframe.main1.mousein = false;findMouseIn();");
                bottomMenu.Visible = true;
            }

            #region loading web grid control
            #region GETTING BASE COLOR FOR ROW SELECTION
            string colorScheme = GetColorScheme();
            string colors = "";

            switch (int.Parse(colorScheme))
            {
                case 1:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();
                    break;
                case 2:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR2").ToString();
                    break;
                case 3:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR3").ToString();
                    break;
                case 4:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR4").ToString();
                    break;
            }

            if (colors != "")
            {
                string[] baseColor = colors.Split(new char[] { ',' });
                if (baseColor.Length > 0)
                    colors = "#" + baseColor[0];
            }
            #endregion

            //Setting Policy Sessions to blank
            //			SetPolicyID("");
            //			SetPolicyVersionID("");

            //Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");

            //BaseDataGrid objWebGrid;
            //objWebGrid = (BaseDataGrid)c1;
            string Lang_Id = GetLanguageID();
            /* Check the SystemID of the logged in user.
                 * If the user is not a Wolverine user then display records of that agency ONLY
                 * else the normal flow follows */
            string sWHERECLAUSE = "";
            string strSystemID = GetSystemId();
            string strCarrierSystemID = CarrierSystemID;
            if (strSystemID.Trim().ToUpper() != strCarrierSystemID.Trim().ToUpper())
            {
                string strAgencyID = ClsAgency.GetAgencyIDFromCode(strSystemID).ToString();
                if (sWHERECLAUSE.Trim().Equals(""))
                {
                    //sWHERECLAUSE = " CCL.CUSTOMER_AGENCY_ID = " + strAgencyID;
                    //sWHERECLAUSE = " (CCL.APP_AGENCY_ID = " + strAgencyID + " OR CCL.CUSTOMER_AGENCY_ID = " + strAgencyID + ")";
                    sWHERECLAUSE = " (CCL.APP_AGENCY_ID = " + strAgencyID + " OR CCL.AGENCY_ID = " + strAgencyID + " OR CCL.CUSTOMER_AGENCY_ID = " + strAgencyID + " and CCL.LANG_ID=" + Lang_Id + ")";

                }
                else
                {
                    //sWHERECLAUSE = sWHERECLAUSE+ " AND CCL.CUSTOMER_AGENCY_ID = " + strAgencyID;	
                    sWHERECLAUSE = sWHERECLAUSE + " AND (CCL.APP_AGENCY_ID = " + strAgencyID + " OR CCL.CUSTOMER_AGENCY_ID = " + strAgencyID + " and CCL.LANG_ID=" + Lang_Id + ")";
                }
            }

            else
            {//for itrack 702
                sWHERECLAUSE = sWHERECLAUSE + " (isnull(CCL.LANG_ID," + Lang_Id + ")=" + Lang_Id + ")";
            }
            //string rootPath = System.Configuration.ConfigurationManager.AppSettings.Get("CmsPath").Trim();
            try
            {
                #region "Commented By Rahul Dwivedi on Date 30/11/2011"

                ////Setting web grid control properties
                //objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

                //objWebGrid.SelectClause = " CUSTOMER_NAME,APP_NUMBER,POLICY_NUMBER,DISPLAY_POLICY_NUMBER,CUSTOMER_FIRST_NAME,CUSTOMER_MIDDLE_NAME,CUSTOMER_LAST_NAME,AL_APP_NUMBER,APP_VERSION_ID,CUSTOMER_ID,app_id,POLICY_ID,POLICY_version_id,LOB_ID,LOB_DESC,STATE_NAME,IS_ACTIVE,STATE_CODE,STATE_NAME1,app_version_id,QQ_NUMBER,ISNULL(CONVERT (NVARCHAR(10),APP_EFFECTIVE_DATE, CASE WHEN " + ClsCommon.BL_LANG_ID + "=2 THEN 103 ELSE 101 END),'') APP_EFFECTIVE_DATE,ISNULL(CONVERT (NVARCHAR(10),APP_EXPIRATION_DATE, CASE WHEN " + ClsCommon.BL_LANG_ID + "=2 THEN 103 ELSE 101 END),'') APP_EXPIRATION_DATE,TRANSACTION_ID,LEADER_POLICY_NUMBER,CO_APPLICANT_NAME ,APP_STATUS ,POLICY_STATUS,POLICY_DISP_VERSION";
                //objWebGrid.FromClause = " VIW_CUSTOMER_APPLICATIONS_LIST CCL WITH(NOLOCK) ";


                //objWebGrid.WhereClause = sWHERECLAUSE;
                ////objWebGrid.GroupByClause            =   "CUSTOMER_ID^CUSTOMER_FIRST_NAME + ' ' + IsNull(CUSTOMER_MIDDLE_NAME,'') + ' ' + IsNull(CUSTOMER_LAST_NAME,'') Name^CUSTOMER_CODE^CUSTOMER_ADDRESS1^CUSTOMER_HOME_PHONE^AGENCY_DISPLAY_NAME^CUSTOMER_LAST_NAME";^Policy_version_id APP_VERSION

                //objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//Policy #^Name^First Name^Middle Name^Last Name^Policy Version^LOB^State Code//Application #^Application Version, Quote # Removed by Charles on 2-Mar-2010 for Policy Implementation
                //objWebGrid.SearchColumnNames = "Policy_Number^CUSTOMER_NAME^APP_NUMBER^lob_id^CO_APPLICANT_NAME^APP_EFFECTIVE_DATE^APP_EXPIRATION_DATE^TRANSACTION_ID^LEADER_POLICY_NUMBER^APP_STATUS^POLICY_STATUS^POLICY_DISP_VERSION";// changes by praveer for itrack no 1500
                ////Policy #^Name^First Name^Middle Name^Last Name^Application #^Product^State Code
                //objWebGrid.DropDownColumns = "^^^LOB^^^^^^APPSTATUS^POLSTATUS^";// changes by praveer for itrack no 1500
                //objWebGrid.SearchColumnType = "T^T^T^L^T^D^D^T^T^L^L^T";// changes by praveer for itrack no 1500

                //objWebGrid.OrderByClause = "DISPLAY_POLICY_NUMBER desc";

                //objWebGrid.DisplayColumnNumbers = "1^3^21^18^20^23^24^25";//Changed from  "1^2^3^5^18^17", by Charles on 2-Mar-2010 for Policy Implementation
                //objWebGrid.DisplayColumnNames = "CUSTOMER_NAME^APP_NUMBER^DISPLAY_POLICY_NUMBER^LOB_DESC^CO_APPLICANT_NAME^APP_EFFECTIVE_DATE^APP_EXPIRATION_DATE^TRANSACTION_ID^LEADER_POLICY_NUMBER";//Changed from  "CUSTOMER_NAME^QQ_NUMBER^APP_NUMBER^POLICY_NUMBER^STATE_CODE^LOB_DESC", by Charles on 2-Mar-2010 for Policy Implementation
                //objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//Customer Name^Policy #^State^LOB//Changed from  "Customer Name^Quick Quote #^Application #^Policy #^State^LOB", by Charles on 2-Mar-2010 for Policy Implementation

                //objWebGrid.DisplayTextLength = "160^60^100^60^230^100^100^100^100";//Changed from  "160^90^230^160^100^60", by Charles on 2-Mar-2010 for Policy Implementation
                //objWebGrid.DisplayColumnPercent = "15^10^15^10^15^15^15^15^15";//Changed from  "15^12^28^28^3^14", by Charles on 2-Mar-2010 for Policy Implementation
                //objWebGrid.PrimaryColumns = "8^9^7^3^14^15";
                //objWebGrid.PrimaryColumnsName = "CUSTOMER_ID^app_id^APP_VERSION^POLICY_ID";//^QUOTE_ID^QQ_ID

                //objWebGrid.ColumnTypes = "T^T^T^B^B^B^B^B^B";//Changed from  "T^T^T^T^B^B", by Charles on 2-Mar-2010 for Policy Implementation

                //if (strCalledFor == "Claim")
                //    objWebGrid.ColumnsLink = "CustomerManagerIndex.aspx?Calledfor=Claim&^" + rootPath + "policies/aspx/policytab.aspx?^" + rootPath + "policies/aspx/policytab.aspx?^" + rootPath + "cmsweb/Construction.html?";//rootPath + "cmsweb/aspx/QuickQuoteLoad.aspx?^" + rootPath + "Application/aspx/ApplicationTab.aspx?^" //Removed by Charles on 2-Mar-2010 for Policy Implementation
                //else if (strCalledFor == "AGENQUOTE")
                //    objWebGrid.ColumnsLink = "CustomerManagerIndex.aspx?Calledfor=AGENQUOTE&^" + rootPath + "policies/aspx/policytab.aspx?^" + rootPath + "policies/aspx/policytab.aspx?^" + rootPath + "cmsweb/Construction.html?";//rootPath + "cmsweb/aspx/QuickQuoteLoad.aspx?^" + rootPath + "Application/aspx/ApplicationTab.aspx?^" //Removed by Charles on 2-Mar-2010 for Policy Implementation
                //else if (strCalledFor == "AGENAPP")
                //    objWebGrid.ColumnsLink = "CustomerManagerIndex.aspx?Calledfor=AGENAPP&^" + rootPath + "policies/aspx/policytab.aspx?^" + rootPath + "policies/aspx/policytab.aspx?^" + rootPath + "cmsweb/Construction.html?";//rootPath + "cmsweb/aspx/QuickQuoteLoad.aspx?^" + rootPath + "Application/aspx/ApplicationTab.aspx?^" //Removed by Charles on 2-Mar-2010 for Policy Implementation
                //else
                //    objWebGrid.ColumnsLink = "CustomerManagerIndex.aspx?^" + rootPath + "policies/aspx/policytab.aspx?^" + rootPath + "policies/aspx/policytab.aspx?^" + rootPath + "cmsweb/Construction.html?";//rootPath + "cmsweb/aspx/QuickQuoteLoad.aspx?^" + rootPath + "Application/aspx/ApplicationTab.aspx?^" //Removed by Charles on 2-Mar-2010 for Policy Implementation

                //objWebGrid.AllowDBLClick = "true";
                //objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button
                //objWebGrid.PageSize = int.Parse(GetPageSize());
                //objWebGrid.CacheSize = int.Parse(GetCacheSize());
                //objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//Customer Manager Search
                //objWebGrid.SelectClass = colors;

                //objWebGrid.FetchColumns = "1^2^3^5^6^7";
                //objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add Customer^0^addCustomer";
                //objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                //objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";

                ////objWebGrid.RequireQuery = "Y";
                ////objWebGrid.QueryStringColumns		=	"CUSTOMER_ID";
                //objWebGrid.DefaultSearch = "N";
                //objWebGrid.Grouping = "Y";

                //objWebGrid.GroupQueryColumns = "customer_id~LOB_ID^customer_id~policy_id~policy_version_id~app_version_id~app_id~LOB_ID^customer_id~policy_id~policy_version_id~app_version_id~app_id~LOB_ID^customer_id~app_id~app_version_id~policy_id~policy_version_id~LOB_ID"; //Changed from  "customer_id^CUSTOMER_ID~STATE_NAME~QQ_ID~QQ_TYPE~QQ_NUMBER~LOB_DESC^customer_id~app_id~app_version_id^customer_id~policy_id~policy_version_id~app_version_id~app_id^customer_id~app_id~app_version_id~policy_id", by Charles on 2-Mar-2010 for Policy Implementation

                ////---------------- Added by Mohit on 29/09/2005----------.
                //objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel"); //Include Inactive
                ////specifying column to be used for filtering
                //objWebGrid.FilterColumnName = "CCL.IS_ACTIVE";
                ////value of filtering record
                //objWebGrid.FilterValue = "Y";
                ////---------------------------End-------------------------.

                //objWebGrid.RequireFocus = "Y";
                ////objWebGrid.ScreenIDProp = base.ScreenId; 				

                ////Adding to controls to gridholder
                //GridHolder.Controls.Add(c1);



                #endregion

                ///load datagrid by xML 
                BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");

                string strSysID = GetSystemId();
                if (strSysID == "ALBAUAT")
                    strSysID = "ALBA";


                string XmlFullFilePath = ClsCommon.WebAppUNCPath + "/client/support/PageXml/" + strSysID + "/CustomerManagerSearch.xml";

                SetDBGrid(objWebGrid, XmlFullFilePath, "");

                objWebGrid.WhereClause = sWHERECLAUSE;
                objWebGrid.Grouping = "Y";
                objWebGrid.GroupQueryColumns = "customer_id~LOB_ID^customer_id~policy_id~policy_version_id~app_version_id~app_id~LOB_ID^customer_id~policy_id~policy_version_id~app_version_id~app_id~LOB_ID^customer_id~app_id~app_version_id~policy_id~policy_version_id~LOB_ID"; //Changed from  "customer_id^CUSTOMER_ID~STATE_NAME~QQ_ID~QQ_TYPE~QQ_NUMBER~LOB_DESC^customer_id~app_id~app_version_id^customer_id~policy_id~policy_version_id~app_version_id~app_id^customer_id~app_id~app_version_id~policy_id", by Charles on 2-Mar-2010 for Policy Implementation

                objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                objWebGrid.AllowDBLClick = "true";
                objWebGrid.PageSize = int.Parse(GetPageSize());
                objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                string rootPath = System.Configuration.ConfigurationManager.AppSettings.Get("CmsPath").Trim();
                if (strCalledFor == "Claim")
                    objWebGrid.ColumnsLink = "CustomerManagerIndex.aspx?Calledfor=Claim&^" + rootPath + "policies/aspx/policytab.aspx?^" + rootPath + "policies/aspx/policytab.aspx?^" + rootPath + "cmsweb/Construction.html?";//rootPath + "cmsweb/aspx/QuickQuoteLoad.aspx?^" + rootPath + "Application/aspx/ApplicationTab.aspx?^" //Removed by Charles on 2-Mar-2010 for Policy Implementation
                else if (strCalledFor == "AGENQUOTE")
                    objWebGrid.ColumnsLink = "CustomerManagerIndex.aspx?Calledfor=AGENQUOTE&^" + rootPath + "policies/aspx/policytab.aspx?^" + rootPath + "policies/aspx/policytab.aspx?^" + rootPath + "cmsweb/Construction.html?";//rootPath + "cmsweb/aspx/QuickQuoteLoad.aspx?^" + rootPath + "Application/aspx/ApplicationTab.aspx?^" //Removed by Charles on 2-Mar-2010 for Policy Implementation
                else if (strCalledFor == "AGENAPP")
                    objWebGrid.ColumnsLink = "CustomerManagerIndex.aspx?Calledfor=AGENAPP&^" + rootPath + "policies/aspx/policytab.aspx?^" + rootPath + "policies/aspx/policytab.aspx?^" + rootPath + "cmsweb/Construction.html?";//rootPath + "cmsweb/aspx/QuickQuoteLoad.aspx?^" + rootPath + "Application/aspx/ApplicationTab.aspx?^" //Removed by Charles on 2-Mar-2010 for Policy Implementation
                else
                    objWebGrid.ColumnsLink = "CustomerManagerIndex.aspx?^" + rootPath + "policies/aspx/policytab.aspx?^" + rootPath + "policies/aspx/policytab.aspx?^" + rootPath + "cmsweb/Construction.html?";//rootPath + "cmsweb/aspx/QuickQuoteLoad.aspx?^" + rootPath + "Application/aspx/ApplicationTab.aspx?^" //Removed by Charles on 2-Mar-2010 for Policy Implementation

                objWebGrid.SelectClass = colors;
                //Adding to controls to gridholder
                GridHolder.Controls.Add(objWebGrid);
            }
            catch (Exception ex)
            {

                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            #endregion








        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion
    }
}

