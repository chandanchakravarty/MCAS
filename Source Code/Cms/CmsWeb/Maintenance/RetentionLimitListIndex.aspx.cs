using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Resources;
using System.Reflection;
using Cms.CmsWeb.WebControls;

namespace Cms.CmsWeb.Maintenance
{
    public partial class RetentionLimitListIndex : Cms.CmsWeb.cmsbase
    {

        protected System.Web.UI.WebControls.Panel pnlGrid;
        protected System.Web.UI.WebControls.Table tblReport;
        protected System.Web.UI.WebControls.Panel pnlReport;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected System.Web.UI.WebControls.Label lblTemplate;
        protected System.Web.UI.HtmlControls.HtmlForm indexForm;
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAddNew;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        ResourceManager objResourceMgr = null;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "558";
            string langId = GetLanguageID();

            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.RetentionLimitListIndex", Assembly.GetExecutingAssembly());

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
            try
            {
                objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL"; ;

                objWebGrid.SelectClause = "RL.RETENTION_LIMIT_ID,ISNULL(M1.SUSEP_LOB_DESC,M.SUSEP_LOB_DESC) as SUSEP_LOB_DESC ,dbo.fun_FormatCurrency(RL.RETENTION_LIMIT," + langId + ") RETENTION_LIMIT";
                objWebGrid.FromClause = "MNT_RETENTION_LIMIT RL join MNT_SUSEP_LOB_MASTER M on M.SUSEP_LOB_ID =RL.REF_SUSEP_LOB_ID left outer join  MNT_SUSEP_LOB_MASTER_MULTILINGUAL M1 on M1.SUSEP_LOB_ID=M.SUSEP_LOB_ID AND M1.LANG_ID="+langId;

                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");

                objWebGrid.SearchColumnNames = "ISNULL(M1.SUSEP_LOB_DESC,M.SUSEP_LOB_DESC)";
                objWebGrid.SearchColumnType = "T^T";
                objWebGrid.OrderByClause = " RL.RETENTION_LIMIT_ID";


                objWebGrid.DisplayColumnNumbers = "1^2";
                objWebGrid.DisplayColumnNames = "SUSEP_LOB_DESC^RETENTION_LIMIT";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings"); 
                objWebGrid.DisplayTextLength = "50^50";
                objWebGrid.DisplayColumnPercent = "50^50";


                objWebGrid.PrimaryColumns = "1";
                objWebGrid.PrimaryColumnsName = "RETENTION_LIMIT_ID";
                objWebGrid.ColumnTypes = "B^B";
                objWebGrid.AllowDBLClick = "true";
                objWebGrid.FetchColumns = "1^2";
                objWebGrid.SearchMessage =objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button"; //objResourceMgr.GetString("SearchMessage");
                objWebGrid.ExtraButtons =objResourceMgr.GetString("ExtraButtons");
                objWebGrid.PageSize = int.Parse(GetPageSize());
                objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString =objResourceMgr.GetString("HeaderString");
                objWebGrid.SelectClass = colors;
                objWebGrid.RequireQuery = "Y";
                objWebGrid.DefaultSearch = "Y";
                objWebGrid.QueryStringColumns = "RETENTION_LIMIT_ID";               
               // objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                //objWebGrid.FilterColumnName = "MNT_RETENTION_LIMIT.IS_ACTIVE";
               // objWebGrid.FilterValue = "Y";
                GridHolder.Controls.Add(objWebGrid);
            }
            //Adding to controls to gridholder
            catch (Exception objExcep)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
            }

            TabCtl.TabURLs = "AddRetentionLimitDetail.aspx?";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
            #endregion

            


        }
    }
}
