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
using Cms.Blcommon;

namespace Cms.CmsWeb.Maintenance
{
    public partial class DiscountSurchargeIndex : Cms.CmsWeb.cmsbase
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
            base.ScreenId = "456";
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.DiscountSurchargeIndex", Assembly.GetExecutingAssembly());
            
            #region GETTING BASE COLOR FOR ROW SELECTION
            string colorScheme = GetColorScheme();
            string colors = "";

            switch (int.Parse(colorScheme))
            {
                case 1:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();// System.Configuration.ConfigurationSettings.AppSettings.Get("GRID_COLOR1").ToString();
                    break;
                case 2:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR2").ToString();// System.Configuration.ConfigurationSettings.AppSettings.Get("GRID_COLOR2").ToString();
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
                objWebGrid.SelectClause = @"DISCOUNT_ID,isnull(mlmm.LOB_DESC ,MNT_LOB_MASTER.LOB_DESC) As PRODUCT,(case when " + GetLanguageID() + @" =2 then isnull(MLVN.SUB_LOB_DESC,'Geral') else isnull(MNT_SUB_LOB_MASTER.SUB_LOB_DESC,'General') end) AS SubLOB,isnull(mlm.LOOKUP_VALUE_DESC,mlv.LOOKUP_VALUE_DESC) as TYPE_ID,MNT_DISCOUNT_SURCHARGE.IS_ACTIVE,DISCOUNT_DESCRIPTION";

                objWebGrid.FromClause = @"MNT_DISCOUNT_SURCHARGE left outer join MNT_LOB_MASTER on MNT_DISCOUNT_SURCHARGE.LOB_ID=MNT_LOB_MASTER.LOB_ID 
                left outer join MNT_LOOKUP_VALUES mlv on mlv.LOOKUP_UNIQUE_ID = MNT_DISCOUNT_SURCHARGE.TYPE_ID
                left outer join MNT_LOOKUP_VALUES_MULTILINGUAL mlm on mlm.LOOKUP_UNIQUE_ID=mlv.LOOKUP_UNIQUE_ID and mlm.LANG_ID = " + GetLanguageID() +
                " left outer join MNT_SUB_LOB_MASTER on MNT_DISCOUNT_SURCHARGE.LOB_ID=MNT_SUB_LOB_MASTER.LOB_ID and MNT_DISCOUNT_SURCHARGE.SUBLOB_ID=MNT_SUB_LOB_MASTER.SUB_LOB_ID 	left join MNT_LOB_MASTER_MULTILINGUAL mlmm on mlmm.LOB_ID=MNT_DISCOUNT_SURCHARGE.LOB_ID and mlmm.LANG_ID=" + GetLanguageID()+
                " left outer join MNT_SUB_LOB_MASTER_MULTILINGUAL MLVN on MNT_DISCOUNT_SURCHARGE.LOB_ID=MLVN.LOB_ID and MNT_DISCOUNT_SURCHARGE.SUBLOB_ID=MLVN.SUB_LOB_ID and MLVN.LANG_ID =" + GetLanguageID();
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Product/Lob^SubLob^Discount Type^Discount Description";

                objWebGrid.SearchColumnNames = "(case when " + GetLanguageID() + @" =2 then isnull(MLVN.SUB_LOB_DESC,'Geral') else isnull(MNT_SUB_LOB_MASTER.SUB_LOB_DESC,'General') end)^MNT_LOB_MASTER.LOB_ID^isnull(mlm.LOOKUP_VALUE_DESC,mlv.LOOKUP_VALUE_DESC)^DISCOUNT_DESCRIPTION";                
                objWebGrid.SearchColumnType = "T^L^T^T";
                objWebGrid.DropDownColumns = "^PRODUCT^^";
                objWebGrid.OrderByClause = "DISCOUNT_ID asc";


                objWebGrid.DisplayColumnNumbers = "1^2^3^4";
                objWebGrid.DisplayColumnNames = "SubLOB^PRODUCT^TYPE_ID^DISCOUNT_DESCRIPTION";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");// "Product/Lob^SubLob^Discount Type^Discount Description";/
                objWebGrid.DisplayTextLength = "20^20^20^100";
                objWebGrid.DisplayColumnPercent = "20^20^20^30";


                objWebGrid.PrimaryColumns = "1";
                objWebGrid.PrimaryColumnsName = "DISCOUNT_ID";
                objWebGrid.ColumnTypes = "B^B^B^B";
                objWebGrid.AllowDBLClick = "true";
                objWebGrid.FetchColumns = "1^2^3^4^5";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
                objWebGrid.PageSize = int.Parse(GetPageSize());
                objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Discount/Surcharge";
                objWebGrid.SelectClass = colors;
                objWebGrid.RequireQuery = "Y";
                objWebGrid.DefaultSearch = "Y";
                objWebGrid.QueryStringColumns = "DISCOUNT_ID";
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
                objWebGrid.FilterColumnName = "MNT_DISCOUNT_SURCHARGE.IS_ACTIVE";
                objWebGrid.FilterValue = "Y";
                GridHolder.Controls.Add(objWebGrid);
            }
            //Adding to controls to gridholder
            catch (Exception objExcep)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
            }

            TabCtl.TabURLs = "AddDiscountSurcharge.aspx?";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
            #endregion
        }
      

    }
    }

