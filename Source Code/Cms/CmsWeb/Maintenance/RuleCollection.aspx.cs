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
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb.WebControls;
using System.Resources;
using System.Reflection;

namespace Cms.CmsWeb.Maintenance
{
    public partial class RuleCollection : Cms.CmsWeb.cmsbase
    {
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected System.Web.UI.WebControls.Label lblTemplate;
        protected System.Web.UI.HtmlControls.HtmlForm indexForm;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidlocQueryStr;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        //System.Resources.ResourceManager objResourceMgr;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            base.ScreenId = "556";
            SetCultureThread(GetLanguageCode());
            System.Resources.ResourceManager objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.RuleCollection", System.Reflection.Assembly.GetExecutingAssembly());

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



            #region loading web grid control           

            BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
            string rootPath = System.Configuration.ConfigurationManager.AppSettings.Get("CmsPath").Trim();
            try
            {
                int LangID = int.Parse(GetLanguageID());

                //Setting web grid control properties
                objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

                objWebGrid.SelectClause = " MRCD.RULE_COLLECTION_ID AS RULE_COLLECTION_ID,ISNULL(Convert(varchar,[EFFECTIVE_FROM],case when " + LangID + "=2 then 103 else 101 end),'') AS [EFFECTIVE_FROM], " +
                    "ISNULL(Convert(varchar,[EFFECTIVE_TO],case when " + LangID + "=2 then 103 else 101 end),'') AS [EFFECTIVE_TO], " +
                    "MRCD.IS_ACTIVE AS IS_ACTIVE,isnull(ml.LOB_DESC,MLM.LOB_DESC) as LOB_DESC,ISNULL(MCL.COUNTRY_NAME,'todos') AS COUNTRY_NAME,isnull(MCSL.STATE_NAME,'todos') AS STATE_NAME ,isnull(sm.SUB_LOB_DESC,MSLM.SUB_LOB_DESC) AS SUB_LOB_DESC";
                objWebGrid.FromClause = "MNT_RULE_COLLECTION_DETAILS MRCD WITH(NOLOCK) LEFT OUTER JOIN MNT_LOB_MASTER MLM ON MRCD.LOB_ID=MLM.LOB_ID  LEFT OUTER JOIN MNT_COUNTRY_LIST MCL ON MRCD.COUNTRY_ID=MCL.COUNTRY_ID  LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL ON MRCD.STATE_ID=MCSL.STATE_ID LEFT OUTER JOIN MNT_SUB_LOB_MASTER MSLM ON MRCD.SUB_LOB_ID=MSLM.SUB_LOB_ID AND MRCD.LOB_ID=MSLM.LOB_ID  left join MNT_LOB_MASTER_MULTILINGUAL ml on ml.LOB_ID=MLM.LOB_ID and ml.LANG_ID= " + LangID + "left join MNT_SUB_LOB_MASTER_MULTILINGUAL  sm on sm.SUB_LOB_ID=MSLM.SUB_LOB_ID and MLM.LOB_ID = sm.LOB_ID and sm.LANG_ID=" + LangID;

                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");// "EFFECTIVE_FROM^EFFECTIVE_TO"; //

                objWebGrid.SearchColumnNames = "EFFECTIVE_FROM^EFFECTIVE_TO^COUNTRY_NAME^STATE_NAME^LOB_DESC^SUB_LOB_DESC";

                objWebGrid.SearchColumnType = "D^D^T^T^T^T";
                objWebGrid.OrderByClause = "RULE_COLLECTION_ID asc";
                objWebGrid.DisplayColumnNumbers = "1^2";
                objWebGrid.DisplayColumnNames = "EFFECTIVE_FROM^EFFECTIVE_TO^COUNTRY_NAME^STATE_NAME^LOB_DESC^SUB_LOB_DESC";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings"); // "EFFECTIVE_FROM^EFFECTIVE_TO^LOB_DESC^COUNTRY_NAME^STATE_NAME"; //
                objWebGrid.DisplayTextLength = "15^15^15^15^20^20";
                objWebGrid.DisplayColumnPercent = "15^15^15^15^20^20";
                objWebGrid.PrimaryColumns = "1";
                objWebGrid.PrimaryColumnsName = "RULE_COLLECTION_ID";
                objWebGrid.ColumnTypes = "B^B^B^B^B^B";
                objWebGrid.AllowDBLClick = "true";
                objWebGrid.FetchColumns = "1^2^3^4^5^6";
                objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button"; // objResourceMgr.GetString("SearchMessage"); //

                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");// "1^Add New^0^addRecord"; //
                objWebGrid.PageSize = int.Parse(GetPageSize());
                objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");  // "Rule Collections"; //
                objWebGrid.QueryStringColumns = "RULE_COLLECTION_ID";               
                objWebGrid.RequireQuery = "Y";
                objWebGrid.DefaultSearch = "Y";
                objWebGrid.SelectClass = colors;
                GridHolder.Controls.Add(objWebGrid);

            }
            catch (Exception objExcep)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
            }
            TabCtl.TabURLs = "AddRuleCollectionsDetail.aspx?";
            TabCtl.TabTitles = objResourceMgr.GetString("TabTitles"); //"Add Rule Collections";  //
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
