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
using Cms.CmsWeb.WebControls;
using System.Reflection;
using System.Resources;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlAccount;

namespace Cms.Account.Aspx
{
    public partial class AcceptedCOILoadApplicationIndex : Cms.Account.AccountBase
    {
        //private int CacheSize = 1400;
        //protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        public string pageMode;
        //System.Resources.ResourceManager objResourceMgr;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "538_1";

            #region GETTING BASE COLOR FOR ROW SELECTION
            string colorScheme = GetColorScheme();
            string colors = "";
           // string customerid;
           
            switch (int.Parse(colorScheme))
            {
                case 1:
                    colors =System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();
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
            SetCultureThread(GetLanguageCode());
            System.Resources.ResourceManager objResourceMgr = new ResourceManager("Cms.Account.Aspx.AcceptedCOILoadApplicationIndex", Assembly.GetExecutingAssembly());
     
            BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
            hidErrormsg.Value = objResourceMgr.GetString("hidErrormsg");
            string rootPath = System.Configuration.ConfigurationManager.AppSettings.Get("CmsPath").Trim();
            pageMode = Request.QueryString["pageMode"];
            hidPageMode.Value = pageMode;
            try
            {
               
                objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                string sWHERECLAUSE = " IMPORT_REQUEST_ID=" + Request.QueryString["IMPORT_REQUEST_ID"] ;
                // TO DISPLAY APPLICATION/POLICY SUCCESSFULL RECORDS
                if (pageMode == "APP_P")
                {
                    sWHERECLAUSE += " AND HAS_ERRORS='N'";
                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID ,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID , POLICY_LOB ,POLICY_SUBLOB ,APPLICANT_NAME ,LEADER_POLICY_NUMBER ,LEADER_ENDORSEMENT_NUMBER,ALBA_POLICY_NUMBER";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_CUSTOMER_POLICY_LIST";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");
                    objWebGrid.SearchColumnNames = "POLICY_LOB^POLICY_SUBLOB^APPLICANT_NAME^LEADER_POLICY_NUMBER^LEADER_ENDORSEMENT_NUMBER^ALBA_POLICY_NUMBER";
                    objWebGrid.SearchColumnType = "T^T^T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6";
                    objWebGrid.DisplayColumnNames = "POLICY_LOB^POLICY_SUBLOB^APPLICANT_NAME^LEADER_POLICY_NUMBER^LEADER_ENDORSEMENT_NUMBER^ALBA_POLICY_NUMBER";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings"); 
                    objWebGrid.DisplayTextLength = "50^50^50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B^B^B";
                    //objWebGrid.ColumnsLink = "AcceptedCOILoadApplicationIndex.aspx?^^^^^" + rootPath + "policies/aspx/policytab.aspx?";
                    //objWebGrid.GroupQueryColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO"; 
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    //objWebGrid.Grouping = "Y";
                    objWebGrid.RequireFocus = "Y";
                   
                }
                // TO DISPLAY APPLICATION/POLICY EXCEPTION RECORDS
                if (pageMode == "APP_E")
                {

                    sWHERECLAUSE += " AND HAS_ERRORS='Y'";
                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID ,IMPORT_SERIAL_NO, POLICY_LOB ,POLICY_SUBLOB ,APPLICANT_NAME ,LEADER_POLICY_NUMBER ,LEADER_ENDORSEMENT_NUMBER";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_CUSTOMER_POLICY_LIST";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_App_Exc");
                    objWebGrid.SearchColumnNames = "POLICY_LOB^POLICY_SUBLOB^APPLICANT_NAME^LEADER_POLICY_NUMBER^LEADER_ENDORSEMENT_NUMBER";
                    objWebGrid.SearchColumnType = "T^T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";
                    objWebGrid.DisplayColumnNames = "POLICY_LOB^POLICY_SUBLOB^APPLICANT_NAME^LEADER_POLICY_NUMBER^LEADER_ENDORSEMENT_NUMBER";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_App_Exc"); 
                    objWebGrid.DisplayTextLength = "50^50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B^B";
                   // objWebGrid.GroupQueryColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                   // objWebGrid.Grouping = "Y";
                    objWebGrid.RequireFocus = "Y";

                }
                // TO DISPLAY COVERAGE EXCEPTION RECORDS
                if (pageMode=="COV_E")
                {
                    sWHERECLAUSE += " AND HAS_ERRORS='Y' ";
                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,SERIAL_NO  AS IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,SEQUENCE_NO ,PRODUCT_LOB,LEADER_POLICY_NUMBER,LEADER_ENDORSEMENT_NUMBER,LEADER_COVERAGE_CODE,SERIAL_NO";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_POLICY_COVERAGES";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings1_App_Exc");
                    objWebGrid.SearchColumnNames = "SEQUENCE_NO^PRODUCT_LOB^LEADER_POLICY_NUMBER^LEADER_ENDORSEMENT_NUMBER^LEADER_COVERAGE_CODE";
                    objWebGrid.SearchColumnType = "T^T^T^T^T";
                    objWebGrid.OrderByClause = "SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";
                    objWebGrid.DisplayColumnNames = "SEQUENCE_NO^PRODUCT_LOB^LEADER_POLICY_NUMBER^LEADER_ENDORSEMENT_NUMBER^LEADER_COVERAGE_CODE";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings1_App_Exc");
                    objWebGrid.DisplayTextLength = "50^50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15^15";
                    objWebGrid.PrimaryColumns = "2";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B^B";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString1");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                }
                // TO DISPLAY COVERAGE SUCCESSFULL RECORDS
                if (pageMode == "COV_P") 
                {
                    sWHERECLAUSE += " AND HAS_ERRORS='N'";
                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,SERIAL_NO  AS IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,SEQUENCE_NO ,PRODUCT_LOB,LEADER_POLICY_NUMBER,LEADER_ENDORSEMENT_NUMBER,LEADER_COVERAGE_CODE,SERIAL_NO,ALBA_POLICY_NUMBER";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_POLICY_COVERAGES";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings1");
                    objWebGrid.SearchColumnNames = "SEQUENCE_NO^PRODUCT_LOB^LEADER_POLICY_NUMBER^LEADER_ENDORSEMENT_NUMBER^LEADER_COVERAGE_CODE^ALBA_POLICY_NUMBER";
                    objWebGrid.SearchColumnType = "T^T^T^T^T^T";
                    objWebGrid.OrderByClause = "SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6";
                    objWebGrid.DisplayColumnNames = "SEQUENCE_NO^PRODUCT_LOB^LEADER_POLICY_NUMBER^LEADER_ENDORSEMENT_NUMBER^LEADER_COVERAGE_CODE^ALBA_POLICY_NUMBER";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings1"); 
                    objWebGrid.DisplayTextLength = "50^50^50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15^15^15";
                    objWebGrid.PrimaryColumns = "2";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B^B^B";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12^13^14^15";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString1");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                }

                //======================================================================
                // TO DISPLAY INSTALLMENT PROCESS RECORDS
                if (pageMode == "IST_P")
                {
                    sWHERECLAUSE += " AND HAS_ERRORS='N'";
                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,PRODUCT_LOB,LEADER_POLICY_NUMBER,LEADER_ENDORSEMENT_NUMBER,INSTALLMENT_NUMBER,ALBA_POLICY_NUMBER";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                   
                    objWebGrid.FromClause = "MIG_POLICY_INSTALLMENT_CANCEL";
                  
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings3");
                    objWebGrid.SearchColumnNames = "PRODUCT_LOB^LEADER_POLICY_NUMBER^LEADER_ENDORSEMENT_NUMBER^INSTALLMENT_NUMBER^ALBA_POLICY_NUMBER";
                    objWebGrid.SearchColumnType = "T^T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";
                    objWebGrid.DisplayColumnNames = "PRODUCT_LOB^LEADER_POLICY_NUMBER^LEADER_ENDORSEMENT_NUMBER^INSTALLMENT_NUMBER^ALBA_POLICY_NUMBER";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings3"); 
                    objWebGrid.DisplayTextLength = "50^50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B^B";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString3");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                   objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                //    objWebGrid.Grouping = "Y";
                   


                   
                }
                // TO DISPLAY INSTALLMENT EXCEPTION RECORDS
                if (pageMode == "IST_E")
                {
                    sWHERECLAUSE += " AND HAS_ERRORS='Y' ";
                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,PRODUCT_LOB,LEADER_POLICY_NUMBER,LEADER_ENDORSEMENT_NUMBER,INSTALLMENT_NUMBER,ALBA_POLICY_NUMBER";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_POLICY_INSTALLMENT_CANCEL";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings3_Exc");
                    objWebGrid.SearchColumnNames = "PRODUCT_LOB^LEADER_POLICY_NUMBER^LEADER_ENDORSEMENT_NUMBER^INSTALLMENT_NUMBER";
                    objWebGrid.SearchColumnType = "T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4";
                    objWebGrid.DisplayColumnNames = "PRODUCT_LOB^LEADER_POLICY_NUMBER^LEADER_ENDORSEMENT_NUMBER^INSTALLMENT_NUMBER";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings3_Exc"); 
                    objWebGrid.DisplayTextLength = "50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString3");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                   // objWebGrid.Grouping = "Y";
                    objWebGrid.RequireFocus = "Y";

                }
                // TO DISPLAY CLAIM DETAIL SUCCESSFULL RECORDS
                if (pageMode == "CLM_D_P")
                {
                    sWHERECLAUSE += " AND HAS_ERRORS='N'";
                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,ALBA_CLAIM_ID,LOB_ID,PRODUCT_LOB,LEADER_POLICY_NUMBER,LEADER_ENDORSEMENT_NUMBER,LEADER_CLAIM_NUMBER,DATE_OF_LOSS,MOVEMENT_TYPE,ALBA_CLAIM_NUMBER";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_CLAIM_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadingsCLM");
                    objWebGrid.SearchColumnNames = "PRODUCT_LOB^LEADER_POLICY_NUMBER^LEADER_ENDORSEMENT_NUMBER^LEADER_CLAIM_NUMBER^DATE_OF_LOSS^MOVEMENT_TYPE^ALBA_CLAIM_NUMBER";
                    objWebGrid.SearchColumnType = "T^T^T^T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6^7";
                    objWebGrid.DisplayColumnNames = "PRODUCT_LOB^LEADER_POLICY_NUMBER^LEADER_ENDORSEMENT_NUMBER^LEADER_CLAIM_NUMBER^DATE_OF_LOSS^MOVEMENT_TYPE^ALBA_CLAIM_NUMBER";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadingsCLM");
                    objWebGrid.DisplayTextLength = "50^50^50^50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B^B^B^B";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12^13^14";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = objResourceMgr.GetString("HeaderStringCLM_Exc");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
               //     objWebGrid.Grouping = "Y";


                }
                // TO DISPLAY CLAIM DETAIL EXCEPTION RECORDS

                if (pageMode == "CLM_D_E")
                {
                    sWHERECLAUSE += " AND HAS_ERRORS='Y' ";
                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,PRODUCT_LOB,LEADER_POLICY_NUMBER,LEADER_ENDORSEMENT_NUMBER,LEADER_CLAIM_NUMBER,DATE_OF_LOSS,MOVEMENT_TYPE";
                    objWebGrid.SelectClause = sSELECTCLAUSE;

                    objWebGrid.FromClause = "MIG_CLAIM_DETAILS";

                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadingsCLM_Exc");
                    objWebGrid.SearchColumnNames = "PRODUCT_LOB^LEADER_POLICY_NUMBER^LEADER_ENDORSEMENT_NUMBER^LEADER_CLAIM_NUMBER^DATE_OF_LOSS^MOVEMENT_TYPE";
                    objWebGrid.SearchColumnType = "T^T^T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6";
                    objWebGrid.DisplayColumnNames = "PRODUCT_LOB^LEADER_POLICY_NUMBER^LEADER_ENDORSEMENT_NUMBER^LEADER_CLAIM_NUMBER^DATE_OF_LOSS^MOVEMENT_TYPE";

                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadingsCLM_Exc"); 
                    objWebGrid.DisplayTextLength = "50^50^50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = objResourceMgr.GetString("HeaderStringCLM_Exc");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                  
                }
                // TO DISPLAY PAID CLAIM DETAIL SUCCESSFULL RECORDS
                if (pageMode == "CLM_P")
                {
                    sWHERECLAUSE += " AND HAS_ERRORS='N'";
                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,ALBA_CLAIM_ID,LOB_ID,PRODUCT_LOB,LEADER_POLICY_NUMBER,LEADER_ENDORSEMENT_NUMBER,LEADER_CLAIM_NUMBER,PAYMENT_AMOUNT,PAYMENT_DATE,ALBA_CLAIM_NUMBER";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_PAID_CLAIM_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadingsCLMPD");
                    objWebGrid.SearchColumnNames = "PRODUCT_LOB^LEADER_POLICY_NUMBER^LEADER_ENDORSEMENT_NUMBER^LEADER_CLAIM_NUMBER^PAYMENT_AMOUNT^PAYMENT_DATE^ALBA_CLAIM_NUMBER";
                    objWebGrid.SearchColumnType = "T^T^T^T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6^7";
                    objWebGrid.DisplayColumnNames = "PRODUCT_LOB^LEADER_POLICY_NUMBER^LEADER_ENDORSEMENT_NUMBER^LEADER_CLAIM_NUMBER^PAYMENT_AMOUNT^PAYMENT_DATE^ALBA_CLAIM_NUMBER";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadingsCLMPD");
                    objWebGrid.DisplayTextLength = "50^50^50^50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B^B^B^B";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12^13^14";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = objResourceMgr.GetString("HeaderStringCLMPAD_Exc");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    //objWebGrid.Grouping = "Y";


                }

                // TO DISPLAY  PAID CLAIM DETAIL EXCEPTION RECORDS

                if (pageMode == "CLM_E")
                {
                    sWHERECLAUSE += " AND HAS_ERRORS='Y' ";
                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,PRODUCT_LOB,LEADER_POLICY_NUMBER,LEADER_ENDORSEMENT_NUMBER,LEADER_CLAIM_NUMBER,PAYMENT_AMOUNT,PAYMENT_DATE";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_PAID_CLAIM_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadingsCLMPAD_Exc");
                    objWebGrid.SearchColumnNames = "PRODUCT_LOB^LEADER_POLICY_NUMBER^LEADER_ENDORSEMENT_NUMBER^LEADER_CLAIM_NUMBER^PAYMENT_AMOUNT^PAYMENT_DATE";
                    objWebGrid.SearchColumnType = "T^T^T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6";
                    objWebGrid.DisplayColumnNames = "PRODUCT_LOB^LEADER_POLICY_NUMBER^LEADER_ENDORSEMENT_NUMBER^LEADER_CLAIM_NUMBER^PAYMENT_AMOUNT^PAYMENT_DATE";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadingsCLMPAD_Exc"); 
                    objWebGrid.DisplayTextLength = "50^50^50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = objResourceMgr.GetString("HeaderStringCLMPAD_Exc");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                  
                }

                GridHolder.Controls.Add(objWebGrid);
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }

            //LoadData(int CUSTOMER_ID);
            TabCtl.TabURLs = "AcceptedCOILoadApplicationDetails.aspx?pageMode="+hidPageMode.Value+"&";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");//"Exception Details";
            TabCtl.TabLength = 200;
            #endregion
           
        }

        //private void LoadData(int Customer_ID)
        //{
        //    hidCustomerId.Value = Customer_ID.ToString();
        //}

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

