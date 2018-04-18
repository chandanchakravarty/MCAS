
/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	01-Sep-2010
<End Date			: -	01-Sep-2010 
<Description		: - To display the all products ( Line of businesses) 
<Review Date		: - 
<Reviewed By		: - 	

Modification History
<Modified Date		: - 
<Modified By		: -   
<Purpose			: -  
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
using System.Resources;
using System.Reflection;

namespace Cms.CmsWeb.Maintenance
{
    public partial class ProductMasterIndex : Cms.CmsWeb.cmsbase
    {
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        ResourceManager objResourceMgr = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "502";

            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.ProductMasterIndex", Assembly.GetExecutingAssembly());
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

            Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
            BaseDataGrid objWebGrid;
            objWebGrid = (BaseDataGrid)c1;

            try
            {
                //Setting web grid control properties
                objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

                objWebGrid.SelectClause = "LM.LOB_ID,LOB_CODE,isnull(ml.LOB_DESC,LM.LOB_DESC) as LOB_DESC  ,isnull(sm.SUSEP_LOB_DESC ,SLM.SUSEP_LOB_DESC ) as SUSEP_LOB_DESC ";

                objWebGrid.FromClause = "MNT_LOB_MASTER LM WITH( NOLOCK) "
                                        + "  left join MNT_SUSEP_LOB_MASTER SLM with (nolock) on LM.SUSEP_LOB_ID = SLM.SUSEP_LOB_ID"
                                        + "  left join MNT_LOB_MASTER_MULTILINGUAL ml on ml.LOB_ID=LM.LOB_ID and ml.LANG_ID=" + GetLanguageID() 
                                        + @" left join MNT_SUSEP_LOB_MASTER_MULTILINGUAL  sm on sm.SUSEP_LOB_ID=SLM.SUSEP_LOB_ID and sm.LANG_ID=" + GetLanguageID() + @"";


                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings"); //"Vessel#^Name Of Vessel^Type Of Vessel^Manufacturer^Manufacturer Year";
                objWebGrid.SearchColumnNames = "LOB_CODE^isnull(ml.LOB_DESC,LM.LOB_DESC)^isnull(sm.SUSEP_LOB_DESC ,SLM.SUSEP_LOB_DESC )";
                objWebGrid.SearchColumnType = "T^T^T";
                objWebGrid.WhereClause = "IS_ACTIVE = 'Y'"; //Added By Lalit New Where Condition. itrack # 1528.
                objWebGrid.OrderByClause = "LOB_ID ASC";

                objWebGrid.DisplayColumnNumbers = "1^2^3";
                objWebGrid.DisplayColumnNames = "LOB_CODE^LOB_DESC^SUSEP_LOB_DESC";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");// "Vessel#^Name Of Vessel^Type Of Vessel^Manufacturer^Manufacturer Year";

                objWebGrid.DisplayTextLength = "10^20^20";
                objWebGrid.DisplayColumnPercent = "10^20^20^30";
                objWebGrid.ColumnTypes = "B^B^B";

                objWebGrid.PrimaryColumns = "1";
                objWebGrid.PrimaryColumnsName = "LM.LOB_ID";

                objWebGrid.AllowDBLClick = "true";
                objWebGrid.FetchColumns = "1^2^3";

                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");

              
                //    objWebGrid.ExtraButtons = "3^Hull Informations~Coverages~Prior Loss Tab^0~1^addRecord~FetchLossReport~PriorLossTab";

                objWebGrid.PageSize = int.Parse(GetPageSize());
                objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");
                objWebGrid.SelectClass = colors;
                objWebGrid.RequireQuery = "Y";
                objWebGrid.DefaultSearch = "Y";
                objWebGrid.QueryStringColumns = "LOB_ID";
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel"); //"Include Inactive";
                objWebGrid.FilterValue = "Y";
           
                GridHolder.Controls.Add(c1);
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            #endregion

            TabCtl.TabURLs = "ProductMasterDetails.aspx?&";

            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");

             
        }
    }
}
