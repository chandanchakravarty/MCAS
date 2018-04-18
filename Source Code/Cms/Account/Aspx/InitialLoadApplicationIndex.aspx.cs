/******************************************************************************************
<Author					: -   Sneha
<Start Date				: -	17/08/2011 
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: -
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
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using System.Reflection;
using System.Resources;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlAccount;

namespace Cms.Account.Aspx
{
    public partial class InitialLoadApplicationIndex : Cms.Account.AccountBase
    {
    //private int CacheSize = 1400;
        //protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        public string pageMode;
        public string lobId;
        public string IMPORT_FILE_TYPE_NAME;
        //System.Resources.ResourceManager objResourceMgr;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "551_3";

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
            System.Resources.ResourceManager objResourceMgr = new ResourceManager("Cms.Account.Aspx.InitialLoadApplicationIndex", Assembly.GetExecutingAssembly());
     
            BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
            //hidErrormsg.Value = objResourceMgr.GetString("hidErrormsg");
            string rootPath = System.Configuration.ConfigurationManager.AppSettings.Get("CmsPath").Trim();

            if (Request.QueryString["pageMode"] != null && Request.QueryString["pageMode"].ToString() != "")
                 pageMode = Request.QueryString["pageMode"];
            if (Request.QueryString["Import_File_Type_Name"] != null && Request.QueryString["Import_File_Type_Name"].ToString() != "")
                IMPORT_FILE_TYPE_NAME = Request.QueryString["IMPORT_FILE_TYPE_NAME"];
           if (Request.QueryString["lobId"] != null && Request.QueryString["lobId"].ToString() != "")
                 lobId = Request.QueryString["lobId"];

            hidPageMode.Value = pageMode;
            try
            {
               
                objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                string sWHERECLAUSE = " IMPORT_REQUEST_ID="+ Request.QueryString["IMPORT_REQUEST_ID"] ;
                // TO DISPLAY CUSTOMER PROCESS SUCCESSFULL RECORDS
                if (pageMode == "14936_P")
                {
                    sWHERECLAUSE += " AND HAS_ERRORS='0'";
                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CUSTOMER_SEQUENTIAL,CUSTOMER_CODE,CUSTOMER_NAME,CPF_CNPJ";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_CUSTOMER_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");
                    objWebGrid.SearchColumnNames = "CUSTOMER_SEQUENTIAL^CUSTOMER_CODE^CUSTOMER_NAME^CPF_CNPJ";
                    objWebGrid.SearchColumnType = "T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4";
                    objWebGrid.DisplayColumnNames = "CUSTOMER_SEQUENTIAL^CUSTOMER_CODE^CUSTOMER_NAME^CPF_CNPJ";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings"); 
                    objWebGrid.DisplayTextLength = "50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME+' '+objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";
                   
                }
                // TO DISPLAY CUSTOMER EXCEPTION RECORDS
                if (pageMode == "14936_E")
                {

                    sWHERECLAUSE += " AND HAS_ERRORS='1'";
                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_SEQUENTIAL,CUSTOMER_CODE,CUSTOMER_NAME,CPF_CNPJ";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_CUSTOMER_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");
                    objWebGrid.SearchColumnNames = "CUSTOMER_SEQUENTIAL^CUSTOMER_CODE^CUSTOMER_NAME^CPF_CNPJ";
                    objWebGrid.SearchColumnType = "T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4";
                    objWebGrid.DisplayColumnNames = "CUSTOMER_SEQUENTIAL^CUSTOMER_CODE^CUSTOMER_NAME^CPF_CNPJ";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_App_Exc"); 
                    objWebGrid.DisplayTextLength = "50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString =IMPORT_FILE_TYPE_NAME+' '+objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";

                }
                //For Co-Applicant Page 14937_E
                if (pageMode == "14937_E")
                {
                    sWHERECLAUSE += " AND HAS_ERRORS='1'";
                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CUSTOMER_SEQUENTIAL,COAPPLICANT_SEQUENTIAL,COAPPLICANT_CODE,CUSTOMER_NAME,CPF_CNPJ";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_CO_APPLICANT_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_14937_E");
                    objWebGrid.SearchColumnNames = "CUSTOMER_SEQUENTIAL^COAPPLICANT_SEQUENTIAL^COAPPLICANT_CODE^CUSTOMER_NAME^CPF_CNPJ";
                    objWebGrid.SearchColumnType = "T^T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";
                    objWebGrid.DisplayColumnNames = "CUSTOMER_SEQUENTIAL^COAPPLICANT_SEQUENTIAL^COAPPLICANT_CODE^CUSTOMER_NAME^CPF_CNPJ";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_14937_E");
                    objWebGrid.DisplayTextLength = "50^50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";

                }
                //For Co-Applicant Page 14937_P
                if (pageMode == "14937_P")
                {
                    sWHERECLAUSE += " AND HAS_ERRORS='0'";
                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CUSTOMER_SEQUENTIAL,COAPPLICANT_SEQUENTIAL,COAPPLICANT_CODE,CUSTOMER_NAME,CPF_CNPJ";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_CO_APPLICANT_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_14937_P");
                    objWebGrid.SearchColumnNames = "CUSTOMER_SEQUENTIAL^COAPPLICANT_SEQUENTIAL^COAPPLICANT_CODE^CUSTOMER_NAME^CPF_CNPJ";
                    objWebGrid.SearchColumnType = "T^T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";
                    objWebGrid.DisplayColumnNames = "CUSTOMER_SEQUENTIAL^COAPPLICANT_SEQUENTIAL^COAPPLICANT_CODE^CUSTOMER_NAME^CPF_CNPJ";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_14937_P");
                    objWebGrid.DisplayTextLength = "50^50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";

                }
                //For Contact Page 14938_E
                if (pageMode == "14938_E")
                {
                    sWHERECLAUSE += " AND HAS_ERRORS='1'";
                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CONTACT_CODE,CUSTOMER_NAME,CONTARCT_POSITION,CPF_CNPJ";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_CONTACT_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_14938_E");
                    objWebGrid.SearchColumnNames = "CONTACT_CODE^CUSTOMER_NAME^CONTARCT_POSITION^CPF_CNPJ";
                    objWebGrid.SearchColumnType = "T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4";
                    objWebGrid.DisplayColumnNames = "CONTACT_CODE^CUSTOMER_NAME^CONTARCT_POSITION^CPF_CNPJ";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_14938_E");
                    objWebGrid.DisplayTextLength = "50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";

                }
                //For Contact Page 14938_P
                if (pageMode == "14938_P")
                {
                    sWHERECLAUSE += " AND HAS_ERRORS='0'";
                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CONTACT_CODE,CUSTOMER_NAME,CONTARCT_POSITION,CPF_CNPJ";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_CONTACT_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_14938_P");
                    objWebGrid.SearchColumnNames = "CONTACT_CODE^CUSTOMER_NAME^CONTARCT_POSITION^CPF_CNPJ";
                    objWebGrid.SearchColumnType = "T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4";
                    objWebGrid.DisplayColumnNames = "CONTACT_CODE^CUSTOMER_NAME^CONTARCT_POSITION^CPF_CNPJ";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_14938_P");
                    objWebGrid.DisplayTextLength = "50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";
                }

                //string WhereClause = "";
                // MODIFIED BY SANTOSH KUMAR GAUATM ON 21 SEPT 2011
                if (pageMode == "14939_P" ||  pageMode == "14939_E") // FOR POLICY DETAIL
                {


                    if(pageMode == "14939_P")
                      sWHERECLAUSE += " AND HAS_ERRORS='0'";
                    else
                      sWHERECLAUSE += " AND HAS_ERRORS='1'";

                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_SEQUENCE_NO,ENDORSEMENT_SEQUENTIAL_NO,POLICY_NUMBER,ENDORSEMENT_NUMBER,DIVDEPTPC";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_POLICY_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_14939");
                    objWebGrid.SearchColumnNames = "POLICY_NUMBER^ENDORSEMENT_NUMBER";
                    objWebGrid.SearchColumnType = "T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";
                    objWebGrid.DisplayColumnNames = "POLICY_SEQUENCE_NO^ENDORSEMENT_SEQUENTIAL_NO^POLICY_NUMBER^ENDORSEMENT_NUMBER^DIVDEPTPC";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_14939");
                    objWebGrid.DisplayTextLength = "50^50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";
                }
                //FOR LOCATION DETAIL
                if (pageMode == "14960_P" || pageMode == "14960_E") 
                {


                    if (pageMode == "14960_P")
                        sWHERECLAUSE += " AND HAS_ERRORS='0'";
                    else
                        sWHERECLAUSE += " AND HAS_ERRORS='1'";

                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_SEQUENCE_NO,END_SEQUENCE_NO,LOCATION_SEQUENCE_NO,LOCATION_CODE,ADDRESS";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_POLICY_LOCATION_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_14960");
                    objWebGrid.SearchColumnNames = "POLICY_SEQUENCE_NO^END_SEQUENCE_NO^LOCATION_SEQUENCE_NO^LOCATION_CODE^ADDRESS";
                    objWebGrid.SearchColumnType = "T^T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";
                    objWebGrid.DisplayColumnNames = "POLICY_SEQUENCE_NO^END_SEQUENCE_NO^LOCATION_SEQUENCE_NO^LOCATION_CODE^ADDRESS";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_14960");
                    objWebGrid.DisplayTextLength = "50^50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";
                }

                //FOR REINSURANCE DETAIL
                if (pageMode == "14942_P" || pageMode == "14942_E")
                {


                    if (pageMode == "14942_P")
                        sWHERECLAUSE += " AND HAS_ERRORS='0'";
                    else
                        sWHERECLAUSE += " AND HAS_ERRORS='1'";

                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_SEQUENTIAL,REINSURANCE_SEQUENTIAL,ENDORSEMENT_SEQUENTIAL,COMMISSION_PERCENT";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_POLICY_REINSURANCE_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_14942");
                    objWebGrid.SearchColumnNames = "POLICY_SEQUENTIAL^ENDORSEMENT_SEQUENTIAL^REINSURANCE_SEQUENTIAL^COMMISSION_PERCENT";
                    objWebGrid.SearchColumnType = "T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4";
                    objWebGrid.DisplayColumnNames = "POLICY_SEQUENTIAL^ENDORSEMENT_SEQUENTIAL^REINSURANCE_SEQUENTIAL^COMMISSION_PERCENT";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_14942");
                    objWebGrid.DisplayTextLength = "50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";
                }

                //For Coinsurence
                if (pageMode == "14941_P" || pageMode == "14941_E")
                {


                    if (pageMode == "14941_P")
                        sWHERECLAUSE += " AND HAS_ERRORS='0'";
                    else
                        sWHERECLAUSE += " AND HAS_ERRORS='1'";

                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,MRCL.REIN_COMAPANY_NAME AS REIN_COMAPANY_NAME,MIPCD.POLICY_SEQUENTIAL AS POLICY_SEQUENTIAL,MIPCD.ENDORSEMENT_SEQUENTIAL AS ENDORSEMENT_SEQUENTIAL,MIPCD.COINSURANCE_SEQUENTIAL AS COINSURANCE_SEQUENTIAL,MIPCD.COI_SHARE AS COI_SHARE";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_POLICY_COINSURER_DETAILS MIPCD WITH(NOLOCK) LEFT OUTER JOIN MNT_REIN_COMAPANY_LIST MRCL ON MIPCD.COINSURANCE_NAME=MRCL.REIN_COMAPANY_ID ";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_14941");
                    objWebGrid.SearchColumnNames = "POLICY_SEQUENTIAL^ENDORSEMENT_SEQUENTIAL^COINSURANCE_SEQUENTIAL^REIN_COMAPANY_NAME^COI_SHARE";
                    objWebGrid.SearchColumnType = "T^T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";
                    objWebGrid.DisplayColumnNames = "POLICY_SEQUENTIAL^ENDORSEMENT_SEQUENTIAL^COINSURANCE_SEQUENTIAL^REIN_COMAPANY_NAME^COI_SHARE";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_14941");
                    objWebGrid.DisplayTextLength = "50^50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";
                }

                //FOR REMUNERATION DETAILS

                if (pageMode == "14940_P" || pageMode == "14940_E")
                {


                    if (pageMode == "14940_P")
                        sWHERECLAUSE += " AND HAS_ERRORS='0'";
                    else
                        sWHERECLAUSE += " AND HAS_ERRORS='1'";

                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_SEQUENCE_NO,END_SEQUENCE_NO,COAPPLICANT_CODE,REMUNERATION_SEQUENCE_NO,COMMISSION_PERCENTAGE";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_POLICY_REMUNERATION_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_14940");
                    objWebGrid.SearchColumnNames = "POLICY_SEQUENCE_NO^END_SEQUENCE_NO^REMUNERATION_SEQUENCE_NO^COAPPLICANT_CODE^COMMISSION_PERCENTAGE";
                    objWebGrid.SearchColumnType = "T^T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";
                    objWebGrid.DisplayColumnNames = "POLICY_SEQUENCE_NO^END_SEQUENCE_NO^REMUNERATION_SEQUENCE_NO^COAPPLICANT_CODE^COMMISSION_PERCENTAGE";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_14940");
                    objWebGrid.DisplayTextLength = "50^50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";
                }
                // FOR CLAUSES DETAILS 14962
                if (pageMode == "14962_P" || pageMode == "14962_E")
                {


                    if (pageMode == "14962_P")
                        sWHERECLAUSE += " AND HAS_ERRORS='0'";
                    else
                        sWHERECLAUSE += " AND HAS_ERRORS='1'";

                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_SEQUENTIAL,ENDORSEMENT_SEQUENTIAL,CLAUSE_SEQUENTIAL,CLAUSE_CODE,FILE_NAME";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_POLICY_CLAUSES_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_14962");
                    objWebGrid.SearchColumnNames = "POLICY_SEQUENTIAL^ENDORSEMENT_SEQUENTIAL^CLAUSE_SEQUENTIAL^CLAUSE_CODE^FILE_NAME";
                    objWebGrid.SearchColumnType = "T^T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";
                    objWebGrid.DisplayColumnNames = "POLICY_SEQUENTIAL^ENDORSEMENT_SEQUENTIAL^CLAUSE_SEQUENTIAL^CLAUSE_CODE^FILE_NAME";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_14962");
                    objWebGrid.DisplayTextLength = "50^50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";
                }
                //POLICY DISCOUNT

                if (pageMode == "14963_P" || pageMode == "14963_E")
                {


                    if (pageMode == "14963_P")
                        sWHERECLAUSE += " AND HAS_ERRORS='0'";
                    else
                        sWHERECLAUSE += " AND HAS_ERRORS='1'";

                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_SEQUENTIAL,ENDORSEMENT_SEQUENTIAL,POLICY_DISCOUNT_SURCHARGE_SEQUENTIAL,[PERCENT]";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_POLICY_DISCOUNTS_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_14963");
                    objWebGrid.SearchColumnNames = "POLICY_SEQUENTIAL^ENDORSEMENT_SEQUENTIAL^POLICY_DISCOUNT_SURCHARGE_SEQUENTIAL^PERCENT";
                    objWebGrid.SearchColumnType = "T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4";
                    objWebGrid.DisplayColumnNames = "POLICY_SEQUENTIAL^ENDORSEMENT_SEQUENTIAL^POLICY_DISCOUNT_SURCHARGE_SEQUENTIAL^PERCENT";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_14963");
                    objWebGrid.DisplayTextLength = "50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";
                }
                //FOR CO APPLICANT
                if (pageMode == "14961_P" || pageMode == "14961_E")
                {


                    if (pageMode == "14961_P")
                        sWHERECLAUSE += " AND HAS_ERRORS='0'";
                    else
                        sWHERECLAUSE += " AND HAS_ERRORS='1'";

                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_SEQUENCE_NUMBER,ENDORSEMNET_SEQUENCE_NUMBER,COAPPLICANT_SEQUENCE,COMMISION,FEES";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_POLICY_COAPPLICANT_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_14961");
                    objWebGrid.SearchColumnNames = "POLICY_SEQUENCE_NUMBER^ENDORSEMNET_SEQUENCE_NUMBER^COAPPLICANT_SEQUENCE^COMMISION^FEES";
                    objWebGrid.SearchColumnType = "T^T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";
                    objWebGrid.DisplayColumnNames = "POLICY_SEQUENCE_NUMBER^ENDORSEMNET_SEQUENCE_NUMBER^COAPPLICANT_SEQUENCE^COMMISION^FEES";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_14961");
                    objWebGrid.DisplayTextLength = "50^50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";
                }
                // FOR RISK DETAILS
                if (pageMode == "15008_P" || pageMode == "15008_E")
                {
                    if ((pageMode == "15008_P"))
                        sWHERECLAUSE += " AND HAS_ERRORS='0'";
                    else
                        sWHERECLAUSE += " AND HAS_ERRORS='1'";

                    objWebGrid.FromClause = "MIG_IL_POLICY_RISK_DETAILS";

                    if (lobId == "0116" || lobId =="0750" || lobId =="0111" ||lobId =="0118" || lobId =="0171" || lobId =="0173" ||lobId =="0196" ||lobId =="0115" ||lobId =="0351" ||lobId =="0114" ||lobId =="0167")
                    {
                        string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_SEQUENCE_NO,END_SEQUENCE_NO,RISK_SEQUENCE_NO,LOCATION_NUMBER,ITEM_NUMBER";
                        objWebGrid.SelectClause = sSELECTCLAUSE;
                        objWebGrid.WhereClause = sWHERECLAUSE;
                        objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_RISK_1");
                        objWebGrid.SearchColumnNames = "POLICY_SEQUENCE_NO^END_SEQUENCE_NO^RISK_SEQUENCE_NO^LOCATION_NUMBER^ITEM_NUMBER";
                        objWebGrid.SearchColumnType = "T^T^T^T^T";
                        objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";
                        objWebGrid.DisplayColumnNames = "POLICY_SEQUENCE_NO^END_SEQUENCE_NO^RISK_SEQUENCE_NO^LOCATION_NUMBER^ITEM_NUMBER";
                        objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_RISK_1");
                        objWebGrid.DisplayTextLength = "50^50^50^50^50";
                        objWebGrid.DisplayColumnPercent = "15^15^15^15^15";
                        objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10";
                        objWebGrid.ColumnTypes = "B^B^B^B^B";
                    }

                    if (lobId == "0746" || lobId == "0621" || lobId == "1163")//746,621,1163
                    {
                        string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_SEQUENCE_NO,END_SEQUENCE_NO,RISK_SEQUENCE_NO,ITEM";
                        objWebGrid.SelectClause = sSELECTCLAUSE;
                        objWebGrid.WhereClause = sWHERECLAUSE;
                        objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_RISK_2");
                        objWebGrid.SearchColumnNames = "POLICY_SEQUENCE_NO^END_SEQUENCE_NO^RISK_SEQUENCE_NO^ITEM";
                        objWebGrid.SearchColumnType = "T^T^T^T";
                        objWebGrid.DisplayColumnNumbers = "1^2^3^4";
                        objWebGrid.DisplayColumnNames = "POLICY_SEQUENCE_NO^END_SEQUENCE_NO^RISK_SEQUENCE_NO^ITEM";
                        objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_RISK_2");
                        objWebGrid.DisplayTextLength = "50^50^50^50";
                        objWebGrid.DisplayColumnPercent = "15^15^15^15";
                        objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9";
                        objWebGrid.ColumnTypes = "B^B^B^B";
                    }

                    if (lobId == "0435" || lobId == "0553" || lobId == "0523" || lobId == "0531" || lobId == "0654" || lobId == "0622")//435,553,523,531,654,622
                    {
                        string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_SEQUENCE_NO,END_SEQUENCE_NO,RISK_SEQUENCE_NO,APPLICANT_CO_APPLICANTS";
                        objWebGrid.SelectClause = sSELECTCLAUSE;
                        objWebGrid.WhereClause = sWHERECLAUSE;
                        objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_RISK_3");
                        objWebGrid.SearchColumnNames = "POLICY_SEQUENCE_NO^END_SEQUENCE_NO^RISK_SEQUENCE_NO^APPLICANT_CO_APPLICANTS";
                        objWebGrid.SearchColumnType = "T^T^T^T";
                        objWebGrid.DisplayColumnNumbers = "1^2^3^4";
                        objWebGrid.DisplayColumnNames = "POLICY_SEQUENCE_NO^END_SEQUENCE_NO^RISK_SEQUENCE_NO^APPLICANT_CO_APPLICANTS";
                        objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_RISK_3");
                        objWebGrid.DisplayTextLength = "50^50^50^50";
                        objWebGrid.DisplayColumnPercent = "15^15^15^15";
                        objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9";
                        objWebGrid.ColumnTypes = "B^B^B^B";
                    }

                    if (lobId == "0981" || lobId == "0993" || lobId == "0982" || lobId == "0977") //981,993,982,977
                    {
                        string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_SEQUENCE_NO,END_SEQUENCE_NO,RISK_SEQUENCE_NO,INDIVIDUAL_NAME,CODE,REGIONAL_ID,REGIONAL_IDENTIFICATION";
                        objWebGrid.SelectClause = sSELECTCLAUSE;
                        objWebGrid.WhereClause = sWHERECLAUSE;
                        objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_RISK_4");
                        objWebGrid.SearchColumnNames = "POLICY_SEQUENCE_NO^END_SEQUENCE_NO^RISK_SEQUENCE_NO^INDIVIDUAL_NAME^CODE^REGIONAL_ID^REGIONAL_IDENTIFICATION";
                        objWebGrid.SearchColumnType = "T^T^T^T^T^T^T";
                        objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6^7";
                        objWebGrid.DisplayColumnNames = "POLICY_SEQUENCE_NO^END_SEQUENCE_NO^RISK_SEQUENCE_NO^INDIVIDUAL_NAME^CODE^REGIONAL_ID^REGIONAL_IDENTIFICATION";
                        objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_RISK_4");
                        objWebGrid.DisplayTextLength = "50^50^50^50^50^50^50";
                        objWebGrid.DisplayColumnPercent = "15^15^15^15^15^15^15";
                        objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12";
                        objWebGrid.ColumnTypes = "B^B^B^B^B^B^B";
                    }

                    if (lobId == "0588" || lobId == "0589")//588,589
                    {
                        string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_SEQUENCE_NO,END_SEQUENCE_NO,RISK_SEQUENCE_NO,TICKET_NO,CATEGORY,STATE";
                        objWebGrid.SelectClause = sSELECTCLAUSE;
                        objWebGrid.WhereClause = sWHERECLAUSE;
                        objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_RISK_5");
                        objWebGrid.SearchColumnNames = "POLICY_SEQUENCE_NO^END_SEQUENCE_NO^RISK_SEQUENCE_NO^TICKET_NO^CATEGORY^STATE";
                        objWebGrid.SearchColumnType = "T^T^T^T^T^T";
                        objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6";
                        objWebGrid.DisplayColumnNames = "POLICY_SEQUENCE_NO^END_SEQUENCE_NO^RISK_SEQUENCE_NO^TICKET_NO^CATEGORY^STATE";
                        objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_RISK_5");
                        objWebGrid.DisplayTextLength = "50^50^50^50^50^50";
                        objWebGrid.DisplayColumnPercent = "15^15^15^15^15^15";
                        objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11";
                        objWebGrid.ColumnTypes = "B^B^B^B^B^B";
                    }
                    if (lobId == "9821" || lobId == "0433") //9821,433
                    {
                        string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_SEQUENCE_NO,END_SEQUENCE_NO,RISK_SEQUENCE_NO,NO_OF_PASSENGERS";
                        objWebGrid.SelectClause = sSELECTCLAUSE;
                        objWebGrid.WhereClause = sWHERECLAUSE;
                        objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_RISK_6");
                        objWebGrid.SearchColumnNames = "POLICY_SEQUENCE_NO^END_SEQUENCE_NO^RISK_SEQUENCE_NO^NO_OF_PASSENGERS";
                        objWebGrid.SearchColumnType = "T^T^T^T";
                        objWebGrid.DisplayColumnNumbers = "1^2^3^4";
                        objWebGrid.DisplayColumnNames = "POLICY_SEQUENCE_NO^END_SEQUENCE_NO^RISK_SEQUENCE_NO^NO_OF_PASSENGERS";
                        objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_RISK_6");
                        objWebGrid.DisplayTextLength = "50^50^50^50";
                        objWebGrid.DisplayColumnPercent = "15^15^15^15";
                        objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9";
                        objWebGrid.ColumnTypes = "B^B^B^B";
                    }                  
                    
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";
                }

                // FOR BILLING INFO

                if (pageMode == "14969_P" || pageMode == "14969_E")
                {


                    if (pageMode == "14969_P")
                        sWHERECLAUSE += " AND HAS_ERRORS='0'";
                    else
                        sWHERECLAUSE += " AND HAS_ERRORS='1'";

                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_SEQUENTIAL,ENDORSEMENT_SEQUENTIAL,INSTALLMENT_SEQUENTIAL,PREMIUM,ALBA_BANK_NUMBER";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_POLICY_BILLING_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_14969");
                    objWebGrid.SearchColumnNames = "POLICY_SEQUENTIAL^ENDORSEMENT_SEQUENTIAL^INSTALLMENT_SEQUENTIAL^PREMIUM^ALBA_BANK_NUMBER";
                    objWebGrid.SearchColumnType = "T^T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";
                    objWebGrid.DisplayColumnNames = "POLICY_SEQUENTIAL^ENDORSEMENT_SEQUENTIAL^INSTALLMENT_SEQUENTIAL^PREMIUM^ALBA_BANK_NUMBER";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_14969");
                    objWebGrid.DisplayTextLength = "50^50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";
                }

                //FOR RISK DISCOUNT
                if (pageMode == "14966_P" || pageMode == "14966_E")
                {


                    if (pageMode == "14966_P")
                        sWHERECLAUSE += " AND HAS_ERRORS='0'";
                    else
                        sWHERECLAUSE += " AND HAS_ERRORS='1'";

                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_SEQUENTIAL,ENDORSEMENT_SEQUENTIAL,RISK_LOCATION_SEQUENTIAL,DISCOUNT_SURCHARGE_RISK_SEQUENTIAL,[TYPE]";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_POLICY_RISK_DISCOUNTS_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_14966");
                    objWebGrid.SearchColumnNames = "POLICY_SEQUENTIAL^ENDORSEMENT_SEQUENTIAL^RISK_LOCATION_SEQUENTIAL^DISCOUNT_SURCHARGE_RISK_SEQUENTIAL^TYPE";
                    objWebGrid.SearchColumnType = "T^T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";
                    objWebGrid.DisplayColumnNames = "POLICY_SEQUENTIAL^ENDORSEMENT_SEQUENTIAL^RISK_LOCATION_SEQUENTIAL^DISCOUNT_SURCHARGE_RISK_SEQUENTIAL^TYPE";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_14966");
                    objWebGrid.DisplayTextLength = "50^50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";
                }
                //FOR BENIFICARY
                if (pageMode == "14967_P" || pageMode == "14967_E")
                {


                    if (pageMode == "14967_P")
                        sWHERECLAUSE += " AND HAS_ERRORS='0'";
                    else
                        sWHERECLAUSE += " AND HAS_ERRORS='1'";

                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_SEQUENTIAL,ENDORSEMENT_SEQUENTIAL,RISK_LOCATION_SEQUENTIAL,BENEFICIARY_SEQUENTIAL,BENEFICIARY_NAME";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_POLICY_RISK_BENIFICIARY_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_14967");
                    objWebGrid.SearchColumnNames = "POLICY_SEQUENTIAL^ENDORSEMENT_SEQUENTIAL^RISK_LOCATION_SEQUENTIAL^BENEFICIARY_SEQUENTIAL^BENEFICIARY_NAME";
                    objWebGrid.SearchColumnType = "T^T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";
                    objWebGrid.DisplayColumnNames = "POLICY_SEQUENTIAL^ENDORSEMENT_SEQUENTIAL^RISK_LOCATION_SEQUENTIAL^BENEFICIARY_SEQUENTIAL^BENEFICIARY_NAME";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_14967");
                    objWebGrid.DisplayTextLength = "50^50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";
                }

                //FOR CLAIM NOTIFICATION
                if (pageMode == "14943_P" || pageMode == "14943_E")
                {


                    if (pageMode == "14943_P")
                        sWHERECLAUSE += " AND HAS_ERRORS='0'";
                    else
                        sWHERECLAUSE += " AND HAS_ERRORS='1'";

                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CLAIM_ID,CLAIM_NUMBER,CLAIM_SEQUENTIAL,OFFICIAL_CLAIM_NUMBER";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_CLAIM_NOTIFICATION_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_14943");
                    objWebGrid.SearchColumnNames = "CLAIM_NUMBER^CLAIM_SEQUENTIAL^OFFICIAL_CLAIM_NUMBER";
                    objWebGrid.SearchColumnType = "T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3";
                    objWebGrid.DisplayColumnNames = "CLAIM_NUMBER^CLAIM_SEQUENTIAL^OFFICIAL_CLAIM_NUMBER";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_14943");
                    objWebGrid.DisplayTextLength = "50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";
                }
                  //FOR PARTIES
                if (pageMode == "14944_P" || pageMode == "14944_E")
                {


                    if (pageMode == "14944_P")
                        sWHERECLAUSE += " AND HAS_ERRORS='0'";
                    else
                        sWHERECLAUSE += " AND HAS_ERRORS='1'";

                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CLAIM_ID,CLAIM_SEQUENTIAL,PARTIES_SEQUENTIAL,PARTY_TYPE,PARTY_DETAIL";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_CLAIM_PARTIES_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_14944");
                    objWebGrid.SearchColumnNames = "CLAIM_SEQUENTIAL^PARTIES_SEQUENTIAL^PARTY_TYPE^PARTY_DETAIL";
                    objWebGrid.SearchColumnType = "T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4";
                    objWebGrid.DisplayColumnNames = "CLAIM_SEQUENTIAL^PARTIES_SEQUENTIAL^PARTY_TYPE^PARTY_DETAIL";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_14944");
                    objWebGrid.DisplayTextLength = "50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";
                }
                //FOR ACTIVITY
                if (pageMode == "14997_P" || pageMode == "14997_E")
                {


                    if (pageMode == "14997_P")
                        sWHERECLAUSE += " AND HAS_ERRORS='0'";
                    else
                        sWHERECLAUSE += " AND HAS_ERRORS='1'";

                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CLAIM_ID,CLAIM_SEQUENTIAL,CLAIM_ACTIVITY_SEQUENTIAL,ACTIVITY_DESCRIPTION";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_CLAIM_ACTIVITY_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_14997");
                    objWebGrid.SearchColumnNames = "CLAIM_SEQUENTIAL^CLAIM_ACTIVITY_SEQUENTIAL^ACTIVITY_DESCRIPTION";
                    objWebGrid.SearchColumnType = "T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3";
                    objWebGrid.DisplayColumnNames = "CLAIM_SEQUENTIAL^CLAIM_ACTIVITY_SEQUENTIAL^ACTIVITY_DESCRIPTION";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_14997");
                    objWebGrid.DisplayTextLength = "50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";
                }
                //FOR CLAIM PAYMENT
                if (pageMode == "14998_P" || pageMode == "14998_E")
                {


                    if (pageMode == "14998_P")
                        sWHERECLAUSE += " AND HAS_ERRORS='0'";
                    else
                        sWHERECLAUSE += " AND HAS_ERRORS='1'";

                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CLAIM_ID,CLAIM_SEQUENTIAL,CLAIM_ACTIVITY_SEQUENTIAL";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_CLAIM_PAYMENT_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_14998");
                    objWebGrid.SearchColumnNames = "CLAIM_SEQUENTIAL^CLAIM_ACTIVITY_SEQUENTIAL";
                    objWebGrid.SearchColumnType = "T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2";
                    objWebGrid.DisplayColumnNames = "CLAIM_SEQUENTIAL^CLAIM_ACTIVITY_SEQUENTIAL";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_14998");
                    objWebGrid.DisplayTextLength = "50^50";
                    objWebGrid.DisplayColumnPercent = "15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";
                }
                //FOR VICTIM
                if (pageMode == "14999_P" || pageMode == "14999_E")
                {


                    if (pageMode == "14999_P")
                        sWHERECLAUSE += " AND HAS_ERRORS='0'";
                    else
                        sWHERECLAUSE += " AND HAS_ERRORS='1'";

                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CLAIM_ID,CLAIM_SEQUENTIAL,VICTIM_SEQUENTIAL";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_CLAIM_VICTIM_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_14999");
                    objWebGrid.SearchColumnNames = "CLAIM_SEQUENTIAL^VICTIM_SEQUENTIAL";
                    objWebGrid.SearchColumnType = "T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2";
                    objWebGrid.DisplayColumnNames = "CLAIM_SEQUENTIAL^VICTIM_SEQUENTIAL";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_14999");
                    objWebGrid.DisplayTextLength = "50^50";
                    objWebGrid.DisplayColumnPercent = "15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";
                }

                //FOR THIRD PARTY DAMAGE
                if (pageMode == "15000_P" || pageMode == "15000_E")
                {


                    if (pageMode == "15000_P")
                        sWHERECLAUSE += " AND HAS_ERRORS='0'";
                    else
                        sWHERECLAUSE += " AND HAS_ERRORS='1'";

                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CLAIM_ID,CLAIM_SEQUENTIAL,THIRD_PARTY_SEQUENTIAL,ANOTHER_PROPERTY_DAMAGED,PROPERTY_DAMAGED_TYPE";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_CLAIM_THIRD_PARTY_DAMAGE_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_15000");
                    objWebGrid.SearchColumnNames = "CLAIM_SEQUENTIAL^THIRD_PARTY_SEQUENTIAL^ANOTHER_PROPERTY_DAMAGED^PROPERTY_DAMAGED_TYPE";
                    objWebGrid.SearchColumnType = "T^T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3^4";
                    objWebGrid.DisplayColumnNames = "CLAIM_SEQUENTIAL^THIRD_PARTY_SEQUENTIAL^ANOTHER_PROPERTY_DAMAGED^PROPERTY_DAMAGED_TYPE";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_15000");
                    objWebGrid.DisplayTextLength = "50^50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";
                }

                //FOR CLAIM RISK INFO 
                if (pageMode == "15001_P" || pageMode == "15001_E")
                {


                    if (pageMode == "15001_P")
                        sWHERECLAUSE += " AND HAS_ERRORS='0'";
                    else
                        sWHERECLAUSE += " AND HAS_ERRORS='1'";

                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CLAIM_ID,CLAIM_SEQUENTIAL,CLAIM_RISK_INFO_SEQUENTIAL,RISKID_LINK_WITH_CLAIM";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_CLAIM_RISK_INFO_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_15001");
                    objWebGrid.SearchColumnNames = "CLAIM_SEQUENTIAL^CLAIM_RISK_INFO_SEQUENTIAL^RISKID_LINK_WITH_CLAIM";
                    objWebGrid.SearchColumnType = "T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3";
                    objWebGrid.DisplayColumnNames = "CLAIM_SEQUENTIAL^CLAIM_RISK_INFO_SEQUENTIAL^RISKID_LINK_WITH_CLAIM";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_15001");
                    objWebGrid.DisplayTextLength = "50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";
                }

                //FOR PAYEE DETAILS 
                if (pageMode == "15002_P" || pageMode == "15002_E")
                {


                    if (pageMode == "15002_P")
                        sWHERECLAUSE += " AND HAS_ERRORS='0'";
                    else
                        sWHERECLAUSE += " AND HAS_ERRORS='1'";

                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CLAIM_ID,CLAIM_SEQUENTIAL,CLAIM_ACTIVITY_SEQUENTIAL,INVOICE_SERIAL_NUMBER";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_CLAIM_PAYEE_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_15002");
                    objWebGrid.SearchColumnNames = "CLAIM_SEQUENTIAL^CLAIM_ACTIVITY_SEQUENTIAL^INVOICE_SERIAL_NUMBER";
                    objWebGrid.SearchColumnType = "T^T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2^3";
                    objWebGrid.DisplayColumnNames = "CLAIM_SEQUENTIAL^CLAIM_ACTIVITY_SEQUENTIAL^INVOICE_SERIAL_NUMBER";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_15002");
                    objWebGrid.DisplayTextLength = "50^50^50";
                    objWebGrid.DisplayColumnPercent = "15^15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";
                }

                //FOR OCCURRENCE DETAIL
                 if (pageMode == "15003_P" || pageMode == "15003_E")
                {


                    if (pageMode == "15003_P")
                        sWHERECLAUSE += " AND HAS_ERRORS='0'";
                    else
                        sWHERECLAUSE += " AND HAS_ERRORS='1'";

                    string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CLAIM_ID,CLAIM_SEQUENTIAL,OCCURENCE_SEQUENTIAL";
                    objWebGrid.SelectClause = sSELECTCLAUSE;
                    objWebGrid.FromClause = "MIG_IL_CLAIM_OCCURRENCE_DETAILS";
                    objWebGrid.WhereClause = sWHERECLAUSE;
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_15003");
                    objWebGrid.SearchColumnNames = "CLAIM_SEQUENTIAL^OCCURENCE_SEQUENTIAL";
                    objWebGrid.SearchColumnType = "T^T";
                    objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                    objWebGrid.DisplayColumnNumbers = "1^2";
                    objWebGrid.DisplayColumnNames = "CLAIM_SEQUENTIAL^OCCURENCE_SEQUENTIAL";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_15003");
                    objWebGrid.DisplayTextLength = "50^50";
                    objWebGrid.DisplayColumnPercent = "15^15";
                    objWebGrid.PrimaryColumns = "2^3";
                    objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.AllowDBLClick = "true";
                    objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                    objWebGrid.PageSize = int.Parse(GetPageSize());
                    objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                    objWebGrid.SelectClass = colors;
                    objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                    objWebGrid.RequireQuery = "Y";
                    objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                    objWebGrid.ColumnTypes = "B^B";
                    objWebGrid.DefaultSearch = "Y";
                    objWebGrid.FilterValue = "Y";
                    objWebGrid.DefaultSearch = "N";
                    objWebGrid.RequireFocus = "Y";
                }

                 //FOR LITIFGATION DETAIL
                 if (pageMode == "15004_P" || pageMode == "15004_E")
                 {


                     if (pageMode == "15004_P")
                         sWHERECLAUSE += " AND HAS_ERRORS='0'";
                     else
                         sWHERECLAUSE += " AND HAS_ERRORS='1'";

                     string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CLAIM_ID,CLAIM_SEQUENTIAL,LITIGATION_INFO_SEQUENTIAL,JUDICIAL_PROCESS_NO";
                     objWebGrid.SelectClause = sSELECTCLAUSE;
                     objWebGrid.FromClause = "MIG_IL_CLAIM_LITIGATION_DETAILS";
                     objWebGrid.WhereClause = sWHERECLAUSE;
                     objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_15004");
                     objWebGrid.SearchColumnNames = "CLAIM_SEQUENTIAL^LITIGATION_INFO_SEQUENTIAL^JUDICIAL_PROCESS_NO";
                     objWebGrid.SearchColumnType = "T^T^T";
                     objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                     objWebGrid.DisplayColumnNumbers = "1^2^3";
                     objWebGrid.DisplayColumnNames = "CLAIM_SEQUENTIAL^LITIGATION_INFO_SEQUENTIAL^JUDICIAL_PROCESS_NO";
                     objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_15004");
                     objWebGrid.DisplayTextLength = "50^50^50";
                     objWebGrid.DisplayColumnPercent = "15^15^15";
                     objWebGrid.PrimaryColumns = "2^3";
                     objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                     objWebGrid.AllowDBLClick = "true";
                     objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9";
                     objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                     objWebGrid.PageSize = int.Parse(GetPageSize());
                     objWebGrid.CacheSize = int.Parse(GetCacheSize());
                     objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                     objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                     objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                     objWebGrid.SelectClass = colors;
                     objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                     objWebGrid.RequireQuery = "Y";
                     objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                     objWebGrid.ColumnTypes = "B^B^B";
                     objWebGrid.DefaultSearch = "Y";
                     objWebGrid.FilterValue = "Y";
                     objWebGrid.DefaultSearch = "N";
                     objWebGrid.RequireFocus = "Y";
                 }
                 //FOR CLAIM COVERAGES
                 if (pageMode == "15005_P" || pageMode == "15005_E")
                 {


                     if (pageMode == "15005_P")
                         sWHERECLAUSE += " AND HAS_ERRORS='0'";
                     else
                         sWHERECLAUSE += " AND HAS_ERRORS='1'";

                     string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CLAIM_ID,CLAIM_SEQUENTIAL,COVERAGE_SEQUENTIAL,COVERAGE_CODE";
                     objWebGrid.SelectClause = sSELECTCLAUSE;
                     objWebGrid.FromClause = "MIG_IL_CLAIM_COVERAGES_DETAILS";
                     objWebGrid.WhereClause = sWHERECLAUSE;
                     objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_15005");
                     objWebGrid.SearchColumnNames = "CLAIM_SEQUENTIAL^COVERAGE_SEQUENTIAL^COVERAGE_CODE";
                     objWebGrid.SearchColumnType = "T^T^T";
                     objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                     objWebGrid.DisplayColumnNumbers = "1^2^3";
                     objWebGrid.DisplayColumnNames = "CLAIM_SEQUENTIAL^COVERAGE_SEQUENTIAL^COVERAGE_CODE";
                     objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_15005");
                     objWebGrid.DisplayTextLength = "50^50^50";
                     objWebGrid.DisplayColumnPercent = "15^15^15";
                     objWebGrid.PrimaryColumns = "2^3";
                     objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                     objWebGrid.AllowDBLClick = "true";
                     objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9";
                     objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                     objWebGrid.PageSize = int.Parse(GetPageSize());
                     objWebGrid.CacheSize = int.Parse(GetCacheSize());
                     objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                     objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                     objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                     objWebGrid.SelectClass = colors;
                     objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                     objWebGrid.RequireQuery = "Y";
                     objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                     objWebGrid.ColumnTypes = "B^B^B";
                     objWebGrid.DefaultSearch = "Y";
                     objWebGrid.FilterValue = "Y";
                     objWebGrid.DefaultSearch = "N";
                     objWebGrid.RequireFocus = "Y";
                 }
                 //FOR CLAIM COINSURANCE
                 if (pageMode == "15006_P" || pageMode == "15006_E")
                 {

                     if (pageMode == "15006_P")
                         sWHERECLAUSE += " AND HAS_ERRORS='0'";
                     else
                         sWHERECLAUSE += " AND HAS_ERRORS='1'";

                     string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CLAIM_ID,CLAIM_SEQUENTIAL,ACCEPTED_COI_SEQUENTIAL,LEADER_POLICY_NO,LEADER_ENDORSEMENT_NO,LEADER_CLAIM_NO,LEADER_SUSEP_CODE";
                     objWebGrid.SelectClause = sSELECTCLAUSE;
                     objWebGrid.FromClause = "MIG_IL_CLAIM_COINSURANCE_DETAILS";
                     objWebGrid.WhereClause = sWHERECLAUSE;
                     objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_15006");
                     objWebGrid.SearchColumnNames = "CLAIM_SEQUENTIAL^ACCEPTED_COI_SEQUENTIAL^LEADER_POLICY_NO^LEADER_ENDORSEMENT_NO^LEADER_CLAIM_NO^LEADER_SUSEP_CODE";
                     objWebGrid.SearchColumnType = "T^T^T^T^T^T";
                     objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                     objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6";
                     objWebGrid.DisplayColumnNames = "CLAIM_SEQUENTIAL^ACCEPTED_COI_SEQUENTIAL^LEADER_POLICY_NO^LEADER_ENDORSEMENT_NO^LEADER_CLAIM_NO^LEADER_SUSEP_CODE";
                     objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_15006");
                     objWebGrid.DisplayTextLength = "50^50^50^50^50^50";
                     objWebGrid.DisplayColumnPercent = "15^15^15^15^15^15";
                     objWebGrid.PrimaryColumns = "2^3";
                     objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                     objWebGrid.AllowDBLClick = "true";
                     objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12";
                     objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                     objWebGrid.PageSize = int.Parse(GetPageSize());
                     objWebGrid.CacheSize = int.Parse(GetCacheSize());
                     objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                     objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                     objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                     objWebGrid.SelectClass = colors;
                     objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                     objWebGrid.RequireQuery = "Y";
                     objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                     objWebGrid.ColumnTypes = "B^B^B^B^B^B";
                     objWebGrid.DefaultSearch = "Y";
                     objWebGrid.FilterValue = "Y";
                     objWebGrid.DefaultSearch = "N";
                     objWebGrid.RequireFocus = "Y";
                 }

                //FOR CLAIM RESERVE
                 if (pageMode == "15007_P" || pageMode == "15007_E")
                 {

                     if (pageMode == "15007_P")
                         sWHERECLAUSE += " AND HAS_ERRORS='0'";
                     else
                         sWHERECLAUSE += " AND HAS_ERRORS='1'";

                     string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,CLAIM_ID,CLAIM_SEQUENTIAL,CLAIM_ACTIVITY_SEQUENTIAL,RESERVE_TYPE,COVERAGE_CODE,LOCATION_CODE";
                     objWebGrid.SelectClause = sSELECTCLAUSE;
                     objWebGrid.FromClause = "MIG_IL_CLAIM_RESERVE_DETAILS";
                     objWebGrid.WhereClause = sWHERECLAUSE;
                     objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_15007");
                     objWebGrid.SearchColumnNames = "CLAIM_SEQUENTIAL^CLAIM_ACTIVITY_SEQUENTIAL^RESERVE_TYPE^COVERAGE_CODE^LOCATION_CODE";
                     objWebGrid.SearchColumnType = "T^T^T^T^T";
                     objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                     objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";
                     objWebGrid.DisplayColumnNames = "CLAIM_SEQUENTIAL^CLAIM_ACTIVITY_SEQUENTIAL^RESERVE_TYPE^COVERAGE_CODE^LOCATION_CODE";
                     objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_15007");
                     objWebGrid.DisplayTextLength = "50^50^50^50^50";
                     objWebGrid.DisplayColumnPercent = "15^15^15^15^15";
                     objWebGrid.PrimaryColumns = "2^3";
                     objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                     objWebGrid.AllowDBLClick = "true";
                     objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11";
                     objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                     objWebGrid.PageSize = int.Parse(GetPageSize());
                     objWebGrid.CacheSize = int.Parse(GetCacheSize());
                     objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                     objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                     objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                     objWebGrid.SelectClass = colors;
                     objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                     objWebGrid.RequireQuery = "Y";
                     objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                     objWebGrid.ColumnTypes = "B^B^B^B^B";
                     objWebGrid.DefaultSearch = "Y";
                     objWebGrid.FilterValue = "Y";
                     objWebGrid.DefaultSearch = "N";
                     objWebGrid.RequireFocus = "Y";
                 }
                //POLICY RISK COVERAGES
                 if (pageMode == "14968_P" || pageMode == "14968_E")
                 {

                     if (pageMode == "14968_P")
                         sWHERECLAUSE += " AND HAS_ERRORS='0'";
                     else
                         sWHERECLAUSE += " AND HAS_ERRORS='1'";

                     string sSELECTCLAUSE = "IMPORT_REQUEST_ID,IMPORT_SERIAL_NO,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_SEQUENTIAL,ENDORSEMENT_SEQUENTIAL,COVERAGE_SEQUENTIAL,RISK_LOCATION_SEQUENTIAL";
                     objWebGrid.SelectClause = sSELECTCLAUSE;
                     objWebGrid.FromClause = "MIG_IL_POLICY_RISK_COVERAGES_DETAILS";
                     objWebGrid.WhereClause = sWHERECLAUSE;
                     objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_14968");
                     objWebGrid.SearchColumnNames = "POLICY_SEQUENTIAL^ENDORSEMENT_SEQUENTIAL^COVERAGE_SEQUENTIAL^RISK_LOCATION_SEQUENTIAL";
                     objWebGrid.SearchColumnType = "T^T^T^T";
                     objWebGrid.OrderByClause = "IMPORT_SERIAL_NO ASC";
                     objWebGrid.DisplayColumnNumbers = "1^2^3^4";
                     objWebGrid.DisplayColumnNames = "POLICY_SEQUENTIAL^ENDORSEMENT_SEQUENTIAL^COVERAGE_SEQUENTIAL^RISK_LOCATION_SEQUENTIAL";
                     objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_14968");
                     objWebGrid.DisplayTextLength = "50^50^50^50";
                     objWebGrid.DisplayColumnPercent = "15^15^15^15";
                     objWebGrid.PrimaryColumns = "2^3";
                     objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                     objWebGrid.AllowDBLClick = "true";
                     objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9";
                     objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                     objWebGrid.PageSize = int.Parse(GetPageSize());
                     objWebGrid.CacheSize = int.Parse(GetCacheSize());
                     objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                     objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                     objWebGrid.HeaderString = IMPORT_FILE_TYPE_NAME + ' ' + objResourceMgr.GetString("HeaderString");
                     objWebGrid.SelectClass = colors;
                     objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");
                     objWebGrid.RequireQuery = "Y";
                     objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^IMPORT_SERIAL_NO";
                     objWebGrid.ColumnTypes = "B^B^B^B";
                     objWebGrid.DefaultSearch = "Y";
                     objWebGrid.FilterValue = "Y";
                     objWebGrid.DefaultSearch = "N";
                     objWebGrid.RequireFocus = "Y";
                 }

                GridHolder.Controls.Add(objWebGrid);
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }

            //LoadData(int CUSTOMER_ID);
            TabCtl.TabURLs = "InitialLoadApplicationDetails.aspx?pageMode=" + hidPageMode.Value + "&";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
            TabCtl.TabLength = 200;
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

