/******************************************************************************************
<Author				: -		Avijit Goswami
<Start Date			: -		16/03/2012
<End Date			: -	
<Description		: - 	Idex page for Marine Cargo Settling Agents
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		:		
<Modified By		:
<Purpose			: 
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
using System.Resources;
using System.Reflection;
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlCommon;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.WebControls;


namespace Cms.CmsWeb.Maintenance
{
    public partial class MarineCargoSetAgentsIndex : Cms.CmsWeb.cmsbase
    {
        protected System.Web.UI.WebControls.Panel pnlGrid;
        protected System.Web.UI.WebControls.Table tblReport;
        protected System.Web.UI.WebControls.Panel pnlReport;        
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected System.Web.UI.WebControls.Literal litTextGrid;        
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        ResourceManager objResourceMgr = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Setting screen id and Type ID
            base.ScreenId = "571";
            #endregion
            #region GETTING BASE COLOR FOR ROW SELECTION
            string colorScheme = GetColorScheme();
            string colors = "";
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.MarineCargoSetAgentsIndex", Assembly.GetExecutingAssembly());

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
            try
            {
                BaseDataGrid objWebGrid;
                objWebGrid = (BaseDataGrid)c1;
                                
                objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";                
                objWebGrid.SelectClause = "MCSA.AGENT_ID AGENT_ID,MCSA.AGENT_CODE AGENT_CODE,MCSA.AGENT_NAME AGENT_NAME,ISNULL(MCSA.AGENT_ADDRESS1,'') +' '+ISNULL(MCSA.AGENT_ADDRESS2,'') AS Address,ISNULL(MCSA.AGENT_CITY,'') AS AGENT_CITY,MCL.COUNTRY_NAME AGENT_COUNTRY,ISNULL(MCSA.AGENT_SURVEY_CODE,'') AS AGENT_SURVEY_CODE,ISNULL(MCSA.IS_ACTIVE,'Y') AS IS_ACTIVE";                
                objWebGrid.FromClause = "MARINE_CARGO_SETTLING_AGENTS MCSA LEFT OUTER JOIN MNT_COUNTRY_LIST MCL ON MCL.COUNTRY_ID = MCSA.AGENT_COUNTRY";
                //objWebGrid.WhereClause = "";  
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//Agent Code^Agent Name^Country^Survey Code
                objWebGrid.SearchColumnNames = objResourceMgr.GetString("SearchColumnNames");//AGENT_CODE^AGENT_NAME^AGENT_COUNTRY^AGENT_SURVEY_CODE                
                objWebGrid.SearchColumnType = objResourceMgr.GetString("SearchColumnType");
                objWebGrid.OrderByClause = "MCSA.AGENT_CODE ASC";
                objWebGrid.DisplayColumnNumbers = objResourceMgr.GetString("DisplayColumnNumbers"); //2^3^7^8;                
                objWebGrid.DisplayColumnNames = objResourceMgr.GetString("DisplayColumnNames");//AGENT_CODE^AGENT_NAME^AGENT_COUNTRY^AGENT_SURVEY_CODE                
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//Agent Code^Agent Name^Country^Survey Code
                objWebGrid.DisplayTextLength = objResourceMgr.GetString("DisplayTextLength");//50^100^100^100
                objWebGrid.DisplayColumnPercent = objResourceMgr.GetString("DisplayColumnPercent"); //25^25^25^25
                objWebGrid.PrimaryColumns = "1";
                objWebGrid.PrimaryColumnsName = "AGENT_ID";
                objWebGrid.ColumnTypes = objResourceMgr.GetString("ColumnTypes"); //B^B^B^B
                objWebGrid.AllowDBLClick = "true";
                objWebGrid.FetchColumns = objResourceMgr.GetString("FetchColumns");//1^2^3^4
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//1^Add New^0^addRecord
                objWebGrid.PageSize = int.Parse(GetPageSize());
                objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//Marine Cargo Settling Agents
                objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
                objWebGrid.FilterColumnName = "MCSA.IS_ACTIVE";
                objWebGrid.FilterValue = "Y";
                objWebGrid.RequireQuery = "Y";
                objWebGrid.DefaultSearch = "Y";
                objWebGrid.QueryStringColumns = "AGENT_ID";
                //Adding to controls to gridholder
                GridHolder.Controls.Add(objWebGrid);
            }
            catch (Exception ex)
            { throw (ex); }
            TabCtl.TabURLs = "AddMarineCargoSetAgents.aspx?&";
            TabCtl.TabLength = 150;
            TabCtl.TabTitles = "Marine Cargo Settling Agents";
            #endregion
        }  
    }
}