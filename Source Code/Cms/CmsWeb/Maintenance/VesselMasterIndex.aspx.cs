﻿/******************************************************************************************
	<Author					:   Abhishek Goel
	<Start Date				:   14/03/2012
	<End Date				: - >
	<Description			: - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - >
	<Modified By			: - >
	<Purpose				: - >
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
    public partial class VesselMasterIndex : Cms.CmsWeb.cmsbase
    {
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected System.Web.UI.WebControls.Label lblTemplate;
        protected System.Web.UI.HtmlControls.HtmlForm indexForm;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden1;
        protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden2;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidlocQueryStr;
        protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden3;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        ResourceManager objResourceMgr = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "569";
            SetCultureThread(GetLanguageCode());
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.VesselMasterIndex", Assembly.GetExecutingAssembly());
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
            BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("../../Cmsweb/webcontrols/BaseDataGrid.ascx");
            try
            {
                //Setting web grid control properties
                objWebGrid.WebServiceURL = "../../Cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

                objWebGrid.SelectClause = "MNT_VESSEL_MASTER.VESSEL_ID VESSEL_ID,MNT_VESSEL_MASTER.VESSEL_NAME VESSEL_NAME,MNT_VESSEL_MASTER.GRT GRT,MNT_VESSEL_MASTER.YEAR_BUILT YEAR_BUILT,MNT_LOOKUP_VALUES.LOOKUP_VALUE_DESC TYPE_OF_VESSEL,MNT_VESSEL_MASTER.IS_ACTIVE AS IS_ACTIVE";
                objWebGrid.FromClause = "MNT_VESSEL_MASTER LEFT OUTER JOIN MNT_LOOKUP_VALUES ON MNT_VESSEL_MASTER.TYPE_OF_VESSEL = MNT_LOOKUP_VALUES.LOOKUP_VALUE_CODE";
                objWebGrid.WhereClause = "MNT_LOOKUP_VALUES.LOOKUP_ID = '1453'";
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Category Code^Category Desc^Value Code^Value Desc";
                objWebGrid.SearchColumnNames = "VESSEL_NAME^GRT^YEAR_BUILT^MNT_LOOKUP_VALUES.LOOKUP_VALUE_DESC";
                //objWebGrid.DropDownColumns="^LookUpDesc^^";
                objWebGrid.SearchColumnType = "T^T^T^T";
                objWebGrid.OrderByClause = "MNT_VESSEL_MASTER.VESSEL_NAME asc";
                objWebGrid.DisplayColumnNumbers = "2^3^4^5";
                objWebGrid.DisplayColumnNames = "VESSEL_NAME^GRT^YEAR_BUILT^TYPE_OF_VESSEL";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Category Code^Category Description^Value Code^Value Description";
                objWebGrid.DisplayTextLength = "20^30^20^30";
                objWebGrid.DisplayColumnPercent = "20^30^20^30";
                objWebGrid.PrimaryColumns = "1";
                objWebGrid.PrimaryColumnsName = "VESSEL_ID";
                objWebGrid.ColumnTypes = "B^B^B^B";
                objWebGrid.AllowDBLClick = "true";
                objWebGrid.FetchColumns = "1^2^3^4";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                //objWebGrid.ExtraButtons = "2^Add New~Single Combined List^0~1";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
                objWebGrid.PageSize = int.Parse(GetPageSize());
                objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"LookUp" ;
                //objWebGrid.QueryStringColumns="LOOKUP_UNIQUE_ID^LookUpID^LookUpDesc^LookUpValueDesc^LookUpName";//Commented for Itrack Issue 5637 on 16 April 09
                objWebGrid.QueryStringColumns = "VESSEL_ID";
                //specifying text to be shown for filter checkbox
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
                //specifying column to be used for filtering
                objWebGrid.FilterColumnName = "MNT_VESSEL_MASTER.IS_ACTIVE";
                //value of filtering record
                objWebGrid.FilterValue = "Y";
                objWebGrid.RequireQuery = "Y";
                objWebGrid.DefaultSearch = "Y";

                objWebGrid.SelectClass = colors;
                GridHolder.Controls.Add(objWebGrid);
            }
            catch (Exception objExcep)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
            }
            #endregion
            TabCtl.TabURLs = "AddVesselMaster.aspx?&";
            //TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
            //TabCtl.TabTitles ="MCCA Attachment";
            TabCtl.TabLength = 150;

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