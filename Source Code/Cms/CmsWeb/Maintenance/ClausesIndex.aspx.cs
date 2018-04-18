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
using System.Xml;
using System.IO;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb;
using System.Reflection;
using System.Resources;

namespace Cms.CmsWeb.Maintenance
{
    public partial class ClausesIndex : Cms.CmsWeb.cmsbase
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
            
            base.ScreenId = "493";
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.ClausesIndex", Assembly.GetExecutingAssembly());

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

           
            if (Request["AddNew"] != null && Request["AddNew"].ToString() != "")
                hidAddNew.Value = Request["AddNew"].ToString();
            #region loading web grid control
            BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
            try
            {
                objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL"; ;

                objWebGrid.SelectClause = "CLAUSE_ID,isnull(mntlm.LOB_DESC,MNT_LOB_MASTER.LOB_DESC) As LOB ,(case when " + GetLanguageID() + @" =2 then isnull(MLVN.SUB_LOB_DESC,'Geral') else isnull(MNT_SUB_LOB_MASTER.SUB_LOB_DESC,'General') end) AS SUBLOB_DESC ,CLAUSE_TITLE,CAST(CLAUSE_DESCRIPTION AS NVARCHAR) AS CLAUSE_DESCRIPTION, ISNULL(mnt4.LOOKUP_VALUE_DESC,MNT1.LOOKUP_VALUE_DESC) as CLAUSE_TYPE,ISNULL(mnt3.LOOKUP_VALUE_DESC,MNT2.LOOKUP_VALUE_DESC) as PROCESS_TYPE,CLAUSE_CODE";
                objWebGrid.FromClause = "MNT_CLAUSES "
                                        + " left outer join MNT_LOB_MASTER on MNT_CLAUSES.LOB_ID = MNT_LOB_MASTER.LOB_ID"
                                        + " left outer join MNT_SUB_LOB_MASTER on MNT_CLAUSES.LOB_ID=MNT_SUB_LOB_MASTER.LOB_ID and MNT_CLAUSES.SUBLOB_ID=MNT_SUB_LOB_MASTER.SUB_LOB_ID"
                                        + " left outer join MNT_LOOKUP_VALUES MNT1 on MNT_CLAUSES.CLAUSE_TYPE=MNT1.LOOKUP_UNIQUE_ID"
                                        + " left outer join MNT_LOOKUP_VALUES MNT2 on MNT_CLAUSES.PROCESS_TYPE=MNT2.LOOKUP_UNIQUE_ID"
                                        + " left outer join MNT_LOB_MASTER_MULTILINGUAL mntlm on mntlm.LOB_ID=MNT_LOB_MASTER.LOB_ID and mntlm.LANG_ID="+GetLanguageID()
                                        + " left outer join MNT_LOOKUP_VALUES_MULTILINGUAL mnt3 on mnt3.LOOKUP_UNIQUE_ID  =MNT2.LOOKUP_UNIQUE_ID  and mnt3.LANG_ID="+GetLanguageID()
                                        + " left outer join MNT_LOOKUP_VALUES_MULTILINGUAL mnt4 on mnt4.LOOKUP_UNIQUE_ID  =MNT1.LOOKUP_UNIQUE_ID  and mnt4.LANG_ID="+GetLanguageID()
                                        + "left outer join MNT_SUB_LOB_MASTER_MULTILINGUAL MLVN on MNT_CLAUSES.LOB_ID=MLVN.LOB_ID and MNT_CLAUSES.SUBLOB_ID=MLVN.SUB_LOB_ID and MLVN.LANG_ID =" + GetLanguageID();

               
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");  //"Product/Lob^SubLob^Title^Clause Description";

                objWebGrid.SearchColumnNames = "CLAUSE_CODE^MNT_LOB_MASTER.LOB_ID^(case when 2 =2 then isnull(MLVN.SUB_LOB_DESC,'Geral') else isnull(MNT_SUB_LOB_MASTER.SUB_LOB_DESC,'General') end)^CLAUSE_TITLE^CAST(CLAUSE_DESCRIPTION AS NVARCHAR)^ISNULL(mnt4.LOOKUP_VALUE_DESC,MNT1.LOOKUP_VALUE_DESC)^ISNULL(mnt3.LOOKUP_VALUE_DESC,MNT2.LOOKUP_VALUE_DESC)";
                objWebGrid.SearchColumnType = "T^L^T^T^T^T^T";
                objWebGrid.DropDownColumns = "^LOB^^^^^";
                objWebGrid.OrderByClause = "CLAUSE_ID asc";


                objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6^7";
                objWebGrid.DisplayColumnNames = "CLAUSE_CODE^LOB^SUBLOB_DESC^CLAUSE_TITLE^CLAUSE_DESCRIPTION^CLAUSE_TYPE^PROCESS_TYPE";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");  // "Product/Lob^SubLob^Title^Clause Description";/
                objWebGrid.DisplayTextLength = "20^20^20^20^100^100^100";
                objWebGrid.DisplayColumnPercent = "10^20^20^20^20^15^20";


                objWebGrid.PrimaryColumns = "1";
                objWebGrid.PrimaryColumnsName = "CLAUSE_ID";
                objWebGrid.ColumnTypes = "B^B^B^B^B^B^B";
                objWebGrid.AllowDBLClick = "true";
                objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
                objWebGrid.PageSize = int.Parse(GetPageSize());
                objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Clauses";
                objWebGrid.SelectClass = colors;
                objWebGrid.RequireQuery = "Y";
                objWebGrid.DefaultSearch = "Y";
                objWebGrid.QueryStringColumns = "CLAUSE_ID";
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
                objWebGrid.FilterColumnName = "MNT_CLAUSES.IS_ACTIVE";
                objWebGrid.FilterValue = "Y";
                GridHolder.Controls.Add(objWebGrid);
            }
            //Adding to controls to gridholder
            catch (Exception objExcep)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
            }

            TabCtl.TabURLs = "AddClausesDetails.aspx?";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
            #endregion
        }


    }
}

