using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.IO;
using Cms.CmsWeb.WebControls;
using System.Resources;
using System.Reflection;
using Cms.BusinessLayer.BlCommon;

namespace Cms.Account.Aspx
{
    public partial class PolicyMelEvents  : Cms.CmsWeb.cmsbase
    {
        //private int CacheSize = 1400;
        ResourceManager objResourceMgr = null;
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
       protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "540";
            //D:\Projects\EBIX-ADVANTAGE-BRAZIL\Source Code\Cms\Account\Aspx\PolicyMelEvents.aspx
            objResourceMgr = new ResourceManager("Cms.Account.Aspx.PolicyMelEvents", Assembly.GetExecutingAssembly());
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

            //BaseDataGrid object created 
            BaseDataGrid objBaseDataGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
            try
            {
                //Set the BaseDataGrid Control Properties

                objBaseDataGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL"; ;
                //objBaseDataGrid.SelectClause = "CRR_RATE_ID,RATE,ISNULL(Convert(varchar,RATE_EFFETIVE_FROM,case when " + ClsCommon.BL_LANG_ID + "=2 then 103 else 101 end),'')AS RATE_EFFETIVE_FROM, ISNULL(Convert(varchar,RATE_EFFETIVE_TO,case when " + ClsCommon.BL_LANG_ID + "=2 then 103 else 101 end),'') AS RATE_EFFETIVE_TO,MCRM.IS_ACTIVE,CURR_DESC";
                objBaseDataGrid.SelectClause = "ROW_ID,Convert(Varchar,DATE,CASE WHEN " + ClsCommon.BL_LANG_ID + "=2 THEN 103 ELSE 101 END) As DATE ,POLICY_NO";
                objBaseDataGrid.FromClause = "MEL_EVENTOS_POLICY_EXCLUSION";
                objBaseDataGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");
                objBaseDataGrid.SearchColumnNames = "DATE^POLICY_NO";
                objBaseDataGrid.SearchColumnType = "D^T";
                objBaseDataGrid.OrderByClause = "ROW_ID asc";
                objBaseDataGrid.DisplayColumnNumbers = "2^3";
                objBaseDataGrid.DisplayColumnNames = "DATE^POLICY_NO";
                objBaseDataGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");
                objBaseDataGrid.DisplayTextLength = "15^25";
                objBaseDataGrid.DisplayColumnPercent = "15^25";
                objBaseDataGrid.PrimaryColumns = "1";
                objBaseDataGrid.PrimaryColumnsName = "ROW_ID";
                objBaseDataGrid.ColumnTypes = "B^B";
                objBaseDataGrid.AllowDBLClick = "true";
                objBaseDataGrid.FetchColumns = "1^2^3";
                //objBaseDataGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objBaseDataGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
                objBaseDataGrid.PageSize = int.Parse(GetPageSize());
                objBaseDataGrid.CacheSize = int.Parse(GetCacheSize());
                objBaseDataGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objBaseDataGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif"; ;
                objBaseDataGrid.HeaderString = objResourceMgr.GetString("HeaderString");
                objBaseDataGrid.SelectClass = colors;
                objBaseDataGrid.RequireQuery = "Y";
                objBaseDataGrid.QueryStringColumns = "ROW_ID";
                objBaseDataGrid.DefaultSearch = "Y";
                objBaseDataGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
                //objBaseDataGrid.FilterColumnName = "";
                //objBaseDataGrid.FilterValue = "Y";

                GridHolder.Controls.Add(objBaseDataGrid);
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            TabCtl.TabURLs = "PolicyMelEventsDetail.aspx?ROW_ID&";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
            TabCtl.TabLength = 150;

        }
    }
}
