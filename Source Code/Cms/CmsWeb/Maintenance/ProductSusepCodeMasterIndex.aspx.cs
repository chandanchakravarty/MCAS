
/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	19-aug-2011
<End Date			: -	19-aug-2011
<Description		: - To display the all products SUSEP Code
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
using Cms.BusinessLayer.BlCommon;


namespace Cms.CmsWeb.Maintenance
{
    public partial class ProductSusepCodeMasterIndex : Cms.CmsWeb.cmsbase
    {
        
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        ResourceManager objResourceMgr ;
        protected void Page_Load(object sender, EventArgs e)
        {
            String LOB_ID ="0";
            base.ScreenId = "502_1";
            if (Request.QueryString["LOB_ID"] != null && Request.QueryString["LOB_ID"].ToString() != "")
                LOB_ID = Request.QueryString["LOB_ID"].ToString();
            SetCultureThread(GetLanguageCode());
            objResourceMgr = new System.Resources.ResourceManager("Cms.Cmsweb.Maintenance.ProductSusepCodeMasterIndex", System.Reflection.Assembly.GetExecutingAssembly());
            
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

                objWebGrid.SelectClause = "LOB_SUSEPCODE_ID ,LOB_ID,Convert(Varchar,EFFECTIVE_FROM,CASE WHEN " + ClsCommon.BL_LANG_ID + "=2 THEN 103 ELSE 101 END) EFFECTIVE_FROM ,Convert(Varchar,EFFECTIVE_TO,CASE WHEN " + ClsCommon.BL_LANG_ID + "=2 THEN 103 ELSE 101 END) EFFECTIVE_TO ,SUSEP_LOB_CODE  ";

                objWebGrid.FromClause = "MNT_LOB_SUSEPCODE_MASTER WITH(NOLOCK)";
                objWebGrid.WhereClause = "LOB_ID=" + LOB_ID;
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");
                objWebGrid.SearchColumnNames = "SUSEP_LOB_CODE^EFFECTIVE_FROM^EFFECTIVE_TO";
                objWebGrid.SearchColumnType = "T^D^D";

                objWebGrid.OrderByClause = "LOB_SUSEPCODE_ID ASC";

                objWebGrid.DisplayColumnNumbers = "1^2^3";
                objWebGrid.DisplayColumnNames = "SUSEP_LOB_CODE^EFFECTIVE_FROM^EFFECTIVE_TO";
                objWebGrid.DisplayColumnHeadings =  objResourceMgr.GetString("DisplayColumnHeadings");

                objWebGrid.DisplayTextLength = "10^20^20";
                objWebGrid.DisplayColumnPercent = "10^20^20^30";
                objWebGrid.ColumnTypes = "B^B^B";

                objWebGrid.PrimaryColumns = "1";
                objWebGrid.PrimaryColumnsName = "LOB_SUSEPCODE_ID";

                objWebGrid.AllowDBLClick = "true";
                objWebGrid.FetchColumns = "1^2^3";

                objWebGrid.SearchMessage =  objResourceMgr.GetString("SearchMessage");
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");
                objWebGrid.PageSize = int.Parse(GetPageSize());
                objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");
                objWebGrid.SelectClass = colors;
                objWebGrid.RequireQuery = "Y";
                objWebGrid.DefaultSearch = "Y";
                objWebGrid.QueryStringColumns = "LOB_SUSEPCODE_ID";
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel"); 
                objWebGrid.FilterValue = "Y";
               
                GridHolder.Controls.Add(c1);
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            #endregion

            TabCtl.TabURLs = "ProductSusepCodeMasterDetails.aspx?LOB_ID=" + LOB_ID + "&";

            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
        }
    }
}