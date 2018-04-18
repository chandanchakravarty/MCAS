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




namespace CmsWeb.Maintenance
{
    public partial class CurrencyRateIndex : Cms.CmsWeb.cmsbase 
    {
        //private int CacheSize = 1400;
        ResourceManager objResourceMgr = null;
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "523_0";
            objResourceMgr = new ResourceManager("CmsWeb.Maintenance.CurrencyRateIndex", Assembly.GetExecutingAssembly());
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
                //SelectClause edited by Agniswar on 12 Sep 2011 for Itrack 1619 [bug# 899]

                int BaseCurrency = 1;
                if (GetSYSBaseCurrency() == enumCurrencyId.BR)
                    BaseCurrency = 2;
                else
                    BaseCurrency = 1;

                objBaseDataGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL"; ;
                objBaseDataGrid.SelectClause = "CRR_RATE_ID,CASE WHEN " + GetLanguageID() + " = 2 THEN REPLACE(CAST((CAST(RATE as decimal(7,2))) AS VARCHAR(10)),'.',',') ELSE CAST( (CAST(RATE as decimal(7,2))) AS VARCHAR(50)) END AS RATE,ISNULL(Convert(varchar,RATE_EFFETIVE_FROM,case when " + ClsCommon.BL_LANG_ID + "=1 then 101 else 103 end),'')AS RATE_EFFETIVE_FROM, ISNULL(Convert(varchar,RATE_EFFETIVE_TO,case when " + ClsCommon.BL_LANG_ID + "=1 then 101 else 103 end),'') AS RATE_EFFETIVE_TO,MCRM.IS_ACTIVE,CURR_DESC";
                objBaseDataGrid.FromClause = "MNT_CURRENCY_RATE_MASTER MCRM LEFT OUTER JOIN MNT_CURRENCY_MASTER MCM ON MCRM.CURRENCY_ID=MCM.CURRENCY_ID";
                objBaseDataGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");
                objBaseDataGrid.SearchColumnNames = "CURR_DESC^RATE^RATE_EFFETIVE_FROM^RATE_EFFETIVE_TO";
                objBaseDataGrid.SearchColumnType = "T^T^D^D";
                objBaseDataGrid.OrderByClause = "CRR_RATE_ID desc";
                objBaseDataGrid.DisplayColumnNumbers = "1^2^3^4";
                objBaseDataGrid.DisplayColumnNames = "CURR_DESC^RATE^RATE_EFFETIVE_FROM^RATE_EFFETIVE_TO";
                objBaseDataGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");
                objBaseDataGrid.DisplayTextLength = "100^100^100^100";
                objBaseDataGrid.DisplayColumnPercent = "15^15^15^15";
                objBaseDataGrid.PrimaryColumns = "1";
                objBaseDataGrid.PrimaryColumnsName = "CRR_RATE_ID";
                objBaseDataGrid.ColumnTypes = "B^B^B^B";
                objBaseDataGrid.AllowDBLClick = "true";
                objBaseDataGrid.FetchColumns = "1^2^3^4";
                objBaseDataGrid.SearchMessage =  objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objBaseDataGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
                objBaseDataGrid.PageSize = int.Parse(GetPageSize());
                objBaseDataGrid.CacheSize = int.Parse(GetCacheSize());
                objBaseDataGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objBaseDataGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif"; ;
                objBaseDataGrid.HeaderString = objResourceMgr.GetString("HeaderString");
                objBaseDataGrid.SelectClass = colors;
                objBaseDataGrid.RequireQuery = "Y";
                objBaseDataGrid.QueryStringColumns = "CRR_RATE_ID";
                objBaseDataGrid.DefaultSearch = "Y";
                objBaseDataGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
                objBaseDataGrid.FilterColumnName = "MCRM.IS_ACTIVE";
                objBaseDataGrid.FilterValue = "Y";

                GridHolder.Controls.Add(objBaseDataGrid);
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            TabCtl.TabURLs = "AddCurrencyRate.aspx?CRR_RATE_ID&";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
            TabCtl.TabLength = 150;
            
        }
    }
}
