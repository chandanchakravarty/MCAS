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
    public partial class InterestRateIndex : Cms.CmsWeb.cmsbase
    {
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected System.Web.UI.WebControls.Label lblTemplate;
        protected System.Web.UI.HtmlControls.HtmlForm indexForm;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidlocQueryStr;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "560";
            SetCultureThread(GetLanguageCode());
            System.Resources.ResourceManager objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.InterestRateIndex", System.Reflection.Assembly.GetExecutingAssembly());

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
                int BaseCurrency = 1;
                if (GetSYSBaseCurrency() == enumCurrencyId.BR)
                    BaseCurrency = 2;
                else
                    BaseCurrency = 1;

                //Setting web grid control properties
                objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

                objWebGrid.SelectClause = " INTEREST_RATE_ID,ISNULL(Convert(varchar,[RATE_EFFECTIVE_DATE],case when " + LangID + "=1 then 101 else 103 end),'') AS [RATE_EFFECTIVE_DATE],NO_OF_INSTALLMENTS,isnull(MLVM.LOOKUP_VALUE_DESC,MLV.LOOKUP_VALUE_DESC ) as LOOKUP_VALUE_DESC,CASE WHEN " + GetLanguageID() + " = 2 THEN REPLACE(CAST((CAST(INTEREST_RATE as decimal(12,4))) AS VARCHAR(10)),'.',',') ELSE CAST( (CAST(INTEREST_RATE as decimal(12,4))) AS VARCHAR(50)) END AS INTEREST_RATE";
                objWebGrid.FromClause = "MNT_INTEREST_RATES MIR WITH(NOLOCK) left join MNT_LOOKUP_VALUES MLV on MIR.INTEREST_TYPE = MLV.LOOKUP_UNIQUE_ID LEFT JOIN MNT_LOOKUP_VALUES_MULTILINGUAL MLVM ON MLVM.LOOKUP_UNIQUE_ID=MIR.INTEREST_TYPE and MLVM.LANG_ID ="+GetLanguageID();

                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"RATE_EFFECTIVE_DATE^NO_OF_INSTALLMENTS^INTEREST_TYPE^INTEREST_RATE"; //

                objWebGrid.SearchColumnNames = "RATE_EFFECTIVE_DATE^NO_OF_INSTALLMENTS^isnull(MLVM.LOOKUP_VALUE_DESC,MLV.LOOKUP_VALUE_DESC )";

                objWebGrid.SearchColumnType = "D^T^T^T";
                objWebGrid.OrderByClause = "INTEREST_RATE_ID asc";
                objWebGrid.DisplayColumnNumbers = "1^2^3^4";
                objWebGrid.DisplayColumnNames = "RATE_EFFECTIVE_DATE^NO_OF_INSTALLMENTS^LOOKUP_VALUE_DESC^INTEREST_RATE";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"RATE_EFFECTIVE_DATE^NO_OF_INSTALLMENTS^INTEREST_TYPE^INTEREST_RATE"; //
                objWebGrid.DisplayTextLength = "25^25^25^25";
                objWebGrid.DisplayColumnPercent = "25^25^25^25";
                objWebGrid.PrimaryColumns = "1";
                objWebGrid.PrimaryColumnsName = "INTEREST_RATE_ID";
                objWebGrid.ColumnTypes = "B^B^B^B";
                objWebGrid.AllowDBLClick = "true";
                objWebGrid.FetchColumns = "1^2^3^4";
                objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button"; // objResourceMgr.GetString("SearchMessage"); //

                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons"); //"1^Add New^0^addRecord";//
                objWebGrid.PageSize = int.Parse(GetPageSize());
                objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString"); //"Interest Rate";//
                objWebGrid.QueryStringColumns = "INTEREST_RATE_ID";
                objWebGrid.RequireQuery = "Y";
                objWebGrid.DefaultSearch = "Y";
                objWebGrid.SelectClass = colors;
                GridHolder.Controls.Add(objWebGrid);

            }
            catch (Exception objExcep)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
            }
            TabCtl.TabURLs = "InterestRateDetail.aspx?INTEREST_RATE_ID&";
            TabCtl.TabTitles = objResourceMgr.GetString("TabTitles"); //"Interest Rate"; //
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