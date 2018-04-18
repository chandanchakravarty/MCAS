//------------------------------------------------------------------------------
// Created by Pradeep Kushwaha on 23-03-2010
// 
//
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.CmsWeb.WebControls;
using System.Resources;
using System.Reflection;


namespace Cms.Policies.Aspx.NamedPerils
{
    /// <summary>
    /// This is the partial index Class of Named Perils,Compehensive Condominium ,Diversified Risks,Coprihensive Company Index page 
    /// Use to display the Named Perils ,Compehensive Condominium ,Diversified Risks  and provide the search criteria of the searchable fields
    /// </summary>
    public partial class NamedPerilsIndex : Cms.Policies.policiesbase//Cms.CmsWeb.cmsbase
    {
        #region Variables
        //private int CacheSize = 1400;
        private string strCustomerID, strPolId, strPolVersionId;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
        protected System.Web.UI.WebControls.Label capMessage;
        protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
        ResourceManager objResourceMgr = null;
        String CalledFrom = string.Empty;
        #endregion

        /// <summary>
        /// This method is use to load the all required details on the page load 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string Url = "";
            if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"].ToString() != "")
            {
                CalledFrom = Request.QueryString["CalledFrom"].ToString();
                switch (CalledFrom.ToUpper().Trim())
                {

                    case "ERISK":
                        base.ScreenId = ENGENEERING_RISKSscreenId.INDEX_PAGE;
                        break;
                    case "NAMEDPERILS":
                        base.ScreenId = ALL_RISK_NAMED_PERILSscreenId.INDEX_PAGE;
                        break;
                    case "COMPCONDO":
                        base.ScreenId = COMPREHENSIVE_CONDOMINIUMscreenId.INDEX_PAGE;
                        break;
                    case "JDLGR":
                        base.ScreenId = JUDICIAL_GUARANTEEscreenId.RISK_INDEX_PAGE;
                        break;
                    case "GLBANK":
                        base.ScreenId = GLOBAL_OF_BANKscreenId.RISK_INDEX_PAGE;
                        break;
                    case "RISK":
                        base.ScreenId = DIVERSIFIED_RISKSscreenId.RISK_INDEX_PAGE;
                        break;
                    case "TFIRE":
                        base.ScreenId = COMPREHENSIVE_COMPANYscreenId.INDEX_PAGE;
                        break;
                    case "COMPCOMP":
                        base.ScreenId = COMPREHENSIVE_COMPANYscreenId.INDEX_PAGE;
                        break;
                    case "DWELLING":
                        base.ScreenId = DWELLINGscreenId.INDEX_PAGE;
                        break;
                    case "GENCVLLIB":
                        base.ScreenId = GENERAL_CIVIL_LIABILITYscreenId.INDEX_PAGE;
                        break;
                }
            }

            objResourceMgr = new ResourceManager("Cms.Policies.Aspx.NamedPerils.NamedPerilsIndex", Assembly.GetExecutingAssembly());
            GetSessionValues();
            Cms.CmsWeb.ClsMessages.SetCustomizedXml(GetLanguageCode());
            if (!CanShow())
            {
                capMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("109");
                capMessage.Visible = true;
                return;
            }

            cltClientTop.PolicyID = int.Parse(strPolId);
            cltClientTop.CustomerID = int.Parse(strCustomerID);
            cltClientTop.PolicyVersionID = int.Parse(strPolVersionId);
            cltClientTop.ShowHeaderBand = "Policy";
            cltClientTop.Visible = true;

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
            Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
            BaseDataGrid objBaseDataGrid = (BaseDataGrid)c1;
            try
            {
                //Set the BaseDataGrid Control Properties



                switch (CalledFrom.ToUpper().Trim())
                {
                    case "ERISK": //Enginering Risk ----Added By Lalit Nov 19,2010
                    case "NAMEDPERILS":  //For Named Perils
                        #region For Named Perils

                        objBaseDataGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                        objBaseDataGrid.SelectClause = "PERIL_ID,POL_LOCATIONS.LOC_NUM as LOC_NUM,LOCATION_NUMBER,ITEM_NUMBER "
                            + ",Rtrim(ltrim((ISNULL(NAME,'')+' - '+ISNULL(POL_LOCATIONS.LOC_ADD1,'')+', '+ISNULL(CONVERT(VARCHAR(10), POL_LOCATIONS.NUMBER),'')+' ' +ISNULL(POL_LOCATIONS.LOC_ADD2,'')+' - '+ISNULL(POL_LOCATIONS.DISTRICT,'')+' - '+ ISNULL(POL_LOCATIONS.LOC_CITY,'')+'/'+MCSL.STATE_CODE+' - '+ ISNULL(POL_LOCATIONS.LOC_ZIP,''))))  AS Loc_Address "
                            + ",ISNULL(MOP.OCCUPIED_AS,' ') as OCCU,POL_PERILS.IS_ACTIVE";
                        objBaseDataGrid.FromClause = " POL_PERILS left outer join POL_LOCATIONS on POL_PERILS.LOCATION=POL_LOCATIONS.LOCATION_ID"
                                                    + " AND POL_PERILS.CUSTOMER_ID=POL_LOCATIONS.CUSTOMER_ID AND POL_PERILS.POLICY_ID=POL_LOCATIONS.POLICY_ID AND POL_PERILS.POLICY_VERSION_ID= POL_LOCATIONS.POLICY_VERSION_ID "
                                                     + " left outer join MNT_OCCUPIED_MASTER MOP ON MOP.OCCUPIED_ID=POL_PERILS.OCCUPANCY"
                                                     + "  LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL on MCSL.STATE_ID=POL_LOCATIONS.LOC_STATE "
                                                     + " AND POL_LOCATIONS.LOC_COUNTRY=MCSL.COUNTRY_ID"; 
                        objBaseDataGrid.WhereClause = " POL_PERILS.CUSTOMER_ID = '" + strCustomerID
                            + "' AND POL_PERILS.POLICY_ID = '" + strPolId
                            + "' AND POL_PERILS.POLICY_VERSION_ID = '" + strPolVersionId
                            + "'";

                        objBaseDataGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_NamedPerils"); //"Location #^Address^Occupied As";
                        objBaseDataGrid.SearchColumnNames = "IsNull(POL_LOCATIONS.LOC_NUM,'')^ISNULL(NAME,'') ! ' ' ! IsNull(POL_LOCATIONS.LOC_ADD1,'') ! ' ' ! ISNULL(CONVERT(VARCHAR(10), POL_LOCATIONS.NUMBER),'') ! ' ' ! IsNull(POL_LOCATIONS.LOC_ADD2,'') ! ' ' ! ISNULL(POL_LOCATIONS.DISTRICT,'') ! ' ' ! IsNull(POL_LOCATIONS.LOC_CITY,'') ! ' ' ! ISNULL(POL_LOCATIONS.LOC_ZIP,'')^OCCUPANCY^LOCATION_NUMBER^ITEM_NUMBER";
                        objBaseDataGrid.SearchColumnType = "T^T^T^T^T";
                        objBaseDataGrid.OrderByClause = "POL_PERILS.PERIL_ID";
                        objBaseDataGrid.DisplayColumnNumbers = "2^3^4^5^6";
                        objBaseDataGrid.DisplayColumnNames = "LOC_NUM^Loc_Address^OCCU^LOCATION_NUMBER^ITEM_NUMBER";
                        objBaseDataGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_NamedPerils"); //"Location #^Address^Occupied As";
                        objBaseDataGrid.DisplayTextLength = "10^35^25^10^10";
                        objBaseDataGrid.DisplayColumnPercent = "10^35^25^10^10";
                        objBaseDataGrid.ColumnTypes = "B^B^B^B^B";
                        objBaseDataGrid.PrimaryColumns = "1";
                        objBaseDataGrid.PrimaryColumnsName = "PERIL_ID";
                        objBaseDataGrid.AllowDBLClick = "true";
                        objBaseDataGrid.FetchColumns = "1^2^3^4^5^6^7";
                        objBaseDataGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                        objBaseDataGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons"); //"1^Add New^0^addRecord";
                        objBaseDataGrid.PageSize = int.Parse(GetPageSize());
                        objBaseDataGrid.CacheSize = int.Parse(GetCacheSize()); ;
                        objBaseDataGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                        objBaseDataGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif"; ;
                        objBaseDataGrid.HeaderString = objResourceMgr.GetString("HeaderString"); //"Location Details";
                        objBaseDataGrid.SelectClass = colors;
                        objBaseDataGrid.RequireQuery = "Y";
                        objBaseDataGrid.DefaultSearch = "Y";
                        objBaseDataGrid.QueryStringColumns = "PERIL_ID";
                        objBaseDataGrid.FilterLabel = objResourceMgr.GetString("FilterLabel"); //"Include Inactive";
                        objBaseDataGrid.FilterValue = "Y";
                        objBaseDataGrid.FilterColumnName = "POL_PERILS.IS_ACTIVE";

                        GridHolder.Controls.Add(c1);

                        TabCtl.TabURLs = "AddNamedPerils.aspx?CUSTOMER_ID=" + strCustomerID + "&POL_ID=" + strPolId + "&POL_VERSION_ID=" + strPolVersionId + "&CalledFrom=" + CalledFrom + "&";

                        TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
                        #endregion

                        break;
                    //case "TFIRE":   //Tradition Fire     ----Added By Lalit Nov 19,2010
                    case "COMPCONDO":  //For Compehensive Condominium

                        #region For Compehensive Condominium
                        objBaseDataGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                        objBaseDataGrid.SelectClause = "POL_PRODUCT_LOCATION_INFO.PRODUCT_RISK_ID,POL_LOCATIONS.LOC_NUM as LOC_NUM ,LOCATION_NUMBER,ITEM_NUMBER "
                            + ",Rtrim(ltrim((ISNULL(NAME,'')+' - '+ISNULL(POL_LOCATIONS.LOC_ADD1,'')+', '+ISNULL(CONVERT(VARCHAR(10), POL_LOCATIONS.NUMBER),'')+' ' +ISNULL(POL_LOCATIONS.LOC_ADD2,'')+' - '+ISNULL(POL_LOCATIONS.DISTRICT,'')+' - '+ ISNULL(POL_LOCATIONS.LOC_CITY,'')+'/'+MCSL.STATE_CODE+' - '+ ISNULL(POL_LOCATIONS.LOC_ZIP,''))))  AS Loc_Address"
                            + ",POL_PRODUCT_LOCATION_INFO.IS_ACTIVE ,(FIRST_NAME + ' ' + IsNull(MIDDLE_NAME,'') + ' ' + IsNull(LAST_NAME,'')) as APPLICANT";
                        objBaseDataGrid.FromClause = " POL_PRODUCT_LOCATION_INFO left outer join POL_LOCATIONS on POL_PRODUCT_LOCATION_INFO.LOCATION=POL_LOCATIONS.LOCATION_ID"
                                                    +" AND POL_PRODUCT_LOCATION_INFO.CUSTOMER_ID=POL_LOCATIONS.CUSTOMER_ID AND POL_PRODUCT_LOCATION_INFO.POLICY_ID=POL_LOCATIONS.POLICY_ID AND POL_PRODUCT_LOCATION_INFO.POLICY_VERSION_ID= POL_LOCATIONS.POLICY_VERSION_ID "
                                                    + " LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL on MCSL.STATE_ID=POL_LOCATIONS.LOC_STATE "
                                                    + " INNER JOIN CLT_APPLICANT_LIST CLT WITH(NOLOCK) ON CLT.APPLICANT_ID=POL_PRODUCT_LOCATION_INFO.CO_APPLICANT_ID"
                                                    + " AND POL_LOCATIONS.LOC_COUNTRY=MCSL.COUNTRY_ID"; 
                        objBaseDataGrid.WhereClause = " POL_PRODUCT_LOCATION_INFO.CUSTOMER_ID = '" + strCustomerID
                            + "' AND POL_PRODUCT_LOCATION_INFO.POLICY_ID = '" + strPolId
                            + "' AND POL_PRODUCT_LOCATION_INFO.POLICY_VERSION_ID = '" + strPolVersionId
                            + "'";

                        objBaseDataGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_CompCondo"); //"Location #,Address;
                        objBaseDataGrid.SearchColumnNames = "ISNULL(POL_LOCATIONS.LOC_NUM,'')^(ISNULL(NAME,'')! ' ' !IsNull(POL_LOCATIONS.LOC_ADD1,'')! ' ' !ISNULL(CONVERT(VARCHAR(10), POL_LOCATIONS.NUMBER),'')! ' ' !IsNull(POL_LOCATIONS.LOC_ADD2,'') ! ' ' !ISNULL(POL_LOCATIONS.DISTRICT,'')! ' ' ! IsNull(POL_LOCATIONS.LOC_CITY,'')! ' ' !ISNULL(POL_LOCATIONS.LOC_ZIP,'')^FIRST_NAME ! ' ' ! IsNull(MIDDLE_NAME,'') ! ' ' ! IsNull(LAST_NAME,'')^LOCATION_NUMBER^ITEM_NUMBER";
                        objBaseDataGrid.SearchColumnType = "T^T^T^T^T";//^T";
                        objBaseDataGrid.OrderByClause = "POL_PRODUCT_LOCATION_INFO.PRODUCT_RISK_ID";
                        objBaseDataGrid.DisplayColumnNumbers = "2^3^4^5^6";//^4";
                        objBaseDataGrid.DisplayColumnNames = "LOC_NUM^Loc_Address^APPLICANT^LOCATION_NUMBER^ITEM_NUMBER";//^OCCU";
                        objBaseDataGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_CompCondo"); //"Location #^Address;
                        objBaseDataGrid.DisplayTextLength = "10^40^20^15^15";//^30";
                        objBaseDataGrid.DisplayColumnPercent = "10^40^20^15^15";//^30";
                        objBaseDataGrid.ColumnTypes = "B^B^B^B^B";//^B";
                        objBaseDataGrid.PrimaryColumns = "1";
                        objBaseDataGrid.PrimaryColumnsName = "PRODUCT_RISK_ID";
                        objBaseDataGrid.AllowDBLClick = "true";
                        objBaseDataGrid.FetchColumns = "1^2^3^4^5^6^7";//^5";
                        objBaseDataGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                        objBaseDataGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons"); //"1^Add New^0^addRecord";
                        objBaseDataGrid.PageSize = int.Parse(GetPageSize());
                        objBaseDataGrid.CacheSize = int.Parse(GetCacheSize()); ;
                        objBaseDataGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                        objBaseDataGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif"; ;
                        objBaseDataGrid.HeaderString = objResourceMgr.GetString("HeaderString"); //"Location Details";
                        objBaseDataGrid.SelectClass = colors;
                        objBaseDataGrid.RequireQuery = "Y";
                        objBaseDataGrid.DefaultSearch = "Y";
                        objBaseDataGrid.QueryStringColumns = "PRODUCT_RISK_ID";
                        objBaseDataGrid.FilterLabel = objResourceMgr.GetString("FilterLabel"); //"Include Inactive";
                        objBaseDataGrid.FilterValue = "Y";
                        objBaseDataGrid.FilterColumnName = "POL_PRODUCT_LOCATION_INFO.IS_ACTIVE";
                        objBaseDataGrid.ShowExcluded = true;
                        GridHolder.Controls.Add(c1);

                        //Set Tan For Page 
                        Url = "../AddProductLocationInfo.aspx?CUSTOMER_ID=" + strCustomerID + "&POL_ID=" + strPolId + "&POL_VERSION_ID=" + strPolVersionId + "&CalledFrom=CompCondo&";
                        TabCtl.TabURLs = Url;

                        TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl_CompCondo"); //Get Tab Titles From XML File
                        #endregion

                        break;
                    case "JDLGR"://Judicial Gurantee---added by Abhinav
                    case "GLBANK": //Global Of Bank ----Added By Lalit Nov 19,2010
                    case "RISK":  //For Diversified Risks

                        #region Diversified Risks product

                        objBaseDataGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                        objBaseDataGrid.SelectClause = "POL_PRODUCT_LOCATION_INFO.PRODUCT_RISK_ID,POL_LOCATIONS.LOC_NUM as LOC_NUM ,LOCATION_NUMBER,ITEM_NUMBER "
                            + ",Rtrim(ltrim((ISNULL(NAME,'')+' - '+ISNULL(POL_LOCATIONS.LOC_ADD1,'')+', '+ISNULL(CONVERT(VARCHAR(10), POL_LOCATIONS.NUMBER),'')+' ' +ISNULL(POL_LOCATIONS.LOC_ADD2,'')+' - '+ISNULL(POL_LOCATIONS.DISTRICT,'')+' - '+ ISNULL(POL_LOCATIONS.LOC_CITY,'') +'/'+MCSL.STATE_CODE +' - '+ ISNULL(POL_LOCATIONS.LOC_ZIP,''))))  AS Loc_Address ,dbo.fun_FormatCurrency(POL_PRODUCT_LOCATION_INFO.VALUE_AT_RISK,2) as VALUE_AT_RISK,dbo.fun_FormatCurrency(POL_PRODUCT_LOCATION_INFO.MAXIMUM_LIMIT,2) as MAXIMUM_LIMIT "
                            + ",POL_PRODUCT_LOCATION_INFO.IS_ACTIVE ,(FIRST_NAME + ' ' + IsNull(MIDDLE_NAME,'') + ' ' + IsNull(LAST_NAME,'')) as APPLICANT";
                        objBaseDataGrid.FromClause = " POL_PRODUCT_LOCATION_INFO left outer join POL_LOCATIONS on POL_PRODUCT_LOCATION_INFO.LOCATION=POL_LOCATIONS.LOCATION_ID"
                                                    +" AND POL_PRODUCT_LOCATION_INFO.CUSTOMER_ID=POL_LOCATIONS.CUSTOMER_ID AND POL_PRODUCT_LOCATION_INFO.POLICY_ID=POL_LOCATIONS.POLICY_ID AND POL_PRODUCT_LOCATION_INFO.POLICY_VERSION_ID= POL_LOCATIONS.POLICY_VERSION_ID "
                                                    + "    LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL on MCSL.STATE_ID=POL_LOCATIONS.LOC_STATE "
                                                    + " INNER JOIN CLT_APPLICANT_LIST CLT WITH(NOLOCK) ON CLT.APPLICANT_ID=POL_PRODUCT_LOCATION_INFO.CO_APPLICANT_ID"
                                                    + " AND POL_LOCATIONS.LOC_COUNTRY=MCSL.COUNTRY_ID"; 
                        objBaseDataGrid.WhereClause = " POL_PRODUCT_LOCATION_INFO.CUSTOMER_ID = '" + strCustomerID
                            + "' AND POL_PRODUCT_LOCATION_INFO.POLICY_ID = '" + strPolId
                            + "' AND POL_PRODUCT_LOCATION_INFO.POLICY_VERSION_ID = '" + strPolVersionId
                            + "'";

                        objBaseDataGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_RISK"); //"Location #,Address;
                        objBaseDataGrid.SearchColumnNames = "IsNull(POL_LOCATIONS.LOC_NUM,'')^ISNULL(NAME,'')! ' ' !IsNull(POL_LOCATIONS.LOC_ADD1,'')! ' ' !ISNULL(CONVERT(VARCHAR(10), POL_LOCATIONS.NUMBER),'')! ' ' !IsNull(POL_LOCATIONS.LOC_ADD2,'')! ' ' ! ISNULL(POL_LOCATIONS.DISTRICT,'')! ' ' ! IsNull(POL_LOCATIONS.LOC_CITY,'')! ' ' ! ISNULL(POL_LOCATIONS.LOC_ZIP,'')^POL_PRODUCT_LOCATION_INFO.VALUE_AT_RISK^POL_PRODUCT_LOCATION_INFO.MAXIMUM_LIMIT^FIRST_NAME ! ' ' ! IsNull(MIDDLE_NAME,'') ! ' ' ! IsNull(LAST_NAME,'')^LOCATION_NUMBER^ITEM_NUMBER";
                        objBaseDataGrid.SearchColumnType = "T^T^T^T^T^T^T";//^T";
                        objBaseDataGrid.OrderByClause = "POL_PRODUCT_LOCATION_INFO.PRODUCT_RISK_ID";
                        objBaseDataGrid.DisplayColumnNumbers = "2^3^4^5^6^7^8";//^4";
                        objBaseDataGrid.DisplayColumnNames = "LOC_NUM^Loc_Address^VALUE_AT_RISK^MAXIMUM_LIMIT^APPLICANT^LOCATION_NUMBER^ITEM_NUMBER";//^OCCU";
                        objBaseDataGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_RISK"); //"Location #^Address;
                        objBaseDataGrid.DisplayTextLength = "10^30^10^10^15^10^10";//^30";
                        objBaseDataGrid.DisplayColumnPercent = "10^30^10^10^15^10^10";//^30";
                        objBaseDataGrid.ColumnTypes = "B^B^B^B^B^B^B";//^B";
                        objBaseDataGrid.PrimaryColumns = "1";
                        objBaseDataGrid.PrimaryColumnsName = "PRODUCT_RISK_ID";
                        objBaseDataGrid.AllowDBLClick = "true";
                        objBaseDataGrid.FetchColumns = "1^2^3^4^5^6^7^8^9";//^5";
                        objBaseDataGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                        objBaseDataGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons"); //"1^Add New^0^addRecord";
                        objBaseDataGrid.PageSize = int.Parse(GetPageSize());
                        objBaseDataGrid.CacheSize = int.Parse(GetCacheSize()); ;
                        objBaseDataGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                        objBaseDataGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif"; ;
                        objBaseDataGrid.HeaderString = objResourceMgr.GetString("HeaderString"); //"Location Details";
                        objBaseDataGrid.SelectClass = colors;
                        objBaseDataGrid.RequireQuery = "Y";
                        objBaseDataGrid.DefaultSearch = "Y";
                        objBaseDataGrid.QueryStringColumns = "PRODUCT_RISK_ID";
                        objBaseDataGrid.FilterLabel = objResourceMgr.GetString("FilterLabel"); //"Include Inactive";
                        objBaseDataGrid.FilterValue = "Y";
                        objBaseDataGrid.FilterColumnName = "POL_PRODUCT_LOCATION_INFO.IS_ACTIVE";
                        objBaseDataGrid.CellHorizontalAlign = "2^3";
                        objBaseDataGrid.ShowExcluded = true;
                        GridHolder.Controls.Add(c1);
                       
                        //Set Tan For Page 
                        Url = "../AddProductLocationInfo.aspx?CUSTOMER_ID=" + strCustomerID + "&POL_ID=" + strPolId + "&POL_VERSION_ID=" + strPolVersionId + "&CalledFrom=" + CalledFrom + "&";
                        TabCtl.TabURLs = Url;
                        
                        TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl_Risk"); //Get Tab Titles From XML File

                        #endregion

                        break;
                    case "TFIRE":
                    case "COMPCOMP":  //Comprehensive Company 

                        #region for Comprehensive Company

                        
                        objBaseDataGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                        objBaseDataGrid.SelectClause = "POL_PRODUCT_LOCATION_INFO.PRODUCT_RISK_ID,POL_LOCATIONS.LOC_NUM as LOC_NUM ,LOCATION_NUMBER,ITEM_NUMBER "
                            + ",Rtrim(ltrim((ISNULL(NAME,'')+' - '+ISNULL(POL_LOCATIONS.LOC_ADD1,'')+' - '+ISNULL(CONVERT(VARCHAR(10), POL_LOCATIONS.NUMBER),'')+' ' +ISNULL(POL_LOCATIONS.LOC_ADD2,'')+' - '+ISNULL(POL_LOCATIONS.DISTRICT,'')+' - '+ ISNULL(POL_LOCATIONS.LOC_CITY,'')+'/'+MCSL.STATE_CODE +' - '+ ISNULL(POL_LOCATIONS.LOC_ZIP,''))))  AS Loc_Address ,ISNULL(MOP.OCCUPIED_AS,'') AS OCCUPIED_AS  "
                            + ",POL_PRODUCT_LOCATION_INFO.IS_ACTIVE,(FIRST_NAME + ' ' + IsNull(MIDDLE_NAME,'') + ' ' + IsNull(LAST_NAME,'')) as APPLICANT";
                        objBaseDataGrid.FromClause = " POL_PRODUCT_LOCATION_INFO left outer join POL_LOCATIONS  on POL_PRODUCT_LOCATION_INFO.LOCATION=POL_LOCATIONS.LOCATION_ID "
                             + " AND POL_PRODUCT_LOCATION_INFO.CUSTOMER_ID=POL_LOCATIONS.CUSTOMER_ID "
                                                     + " AND POL_PRODUCT_LOCATION_INFO.POLICY_ID=POL_LOCATIONS.POLICY_ID "
                                                     + " AND POL_PRODUCT_LOCATION_INFO.POLICY_VERSION_ID= POL_LOCATIONS.POLICY_VERSION_ID "
                                                     + "  LEFT JOIN MNT_OCCUPIED_MASTER MOP ON MOP.OCCUPIED_ID=POL_PRODUCT_LOCATION_INFO.OCCUPIED_AS "
                                                     + " LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL on MCSL.STATE_ID=POL_LOCATIONS.LOC_STATE "
                                                     + " INNER JOIN CLT_APPLICANT_LIST CLT WITH(NOLOCK) ON CLT.APPLICANT_ID=POL_PRODUCT_LOCATION_INFO.CO_APPLICANT_ID"
                                                     + " AND POL_LOCATIONS.LOC_COUNTRY=MCSL.COUNTRY_ID"; 
                         objBaseDataGrid.WhereClause = " POL_PRODUCT_LOCATION_INFO.CUSTOMER_ID = '" + strCustomerID
                            + "' AND POL_PRODUCT_LOCATION_INFO.POLICY_ID = '" + strPolId
                            + "' AND POL_PRODUCT_LOCATION_INFO.POLICY_VERSION_ID = '" + strPolVersionId
                            + "'";
                      
                        objBaseDataGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings"); //"Location #,Address;
                        objBaseDataGrid.SearchColumnNames = "ISNULL(POL_LOCATIONS.LOC_NUM,'')^ISNULL(NAME,'')! ' ' !IsNull(POL_LOCATIONS.LOC_ADD1,'')! ' ' !ISNULL(CONVERT(VARCHAR(10), POL_LOCATIONS.NUMBER),'')! ' ' !IsNull(POL_LOCATIONS.LOC_ADD2,'')! ' ' ! ISNULL(POL_LOCATIONS.DISTRICT,'') ! ' ' ! IsNull(POL_LOCATIONS.LOC_CITY,'')! ' ' !ISNULL(POL_LOCATIONS.LOC_ZIP,'')^ISNULL(LKP.LOOKUP_VALUE_DESC,' ')^FIRST_NAME ! ' ' ! IsNull(MIDDLE_NAME,'') ! ' ' ! IsNull(LAST_NAME,'')^LOCATION_NUMBER^ITEM_NUMBER";
                        objBaseDataGrid.SearchColumnType = "T^T^T^T^T^T";
                        objBaseDataGrid.OrderByClause = "POL_PRODUCT_LOCATION_INFO.PRODUCT_RISK_ID";
                        objBaseDataGrid.DisplayColumnNumbers = "2^3^4^5^6^7";//^4";
                        objBaseDataGrid.DisplayColumnNames = "LOC_NUM^Loc_Address^OCCUPIED_AS^APPLICANT^LOCATION_NUMBER^ITEM_NUMBER";//^OCCU";
                        objBaseDataGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings"); //"Location #^Address;
                        objBaseDataGrid.DisplayTextLength = "10^30^20^15^15^15";
                        objBaseDataGrid.DisplayColumnPercent = "10^30^20^15^15^15";
                        objBaseDataGrid.ColumnTypes = "B^B^B^B^B^B";
                        objBaseDataGrid.PrimaryColumns = "1";
                        objBaseDataGrid.PrimaryColumnsName = "PRODUCT_RISK_ID";
                        objBaseDataGrid.AllowDBLClick = "true";
                        objBaseDataGrid.FetchColumns = "1^2^3^4^5^6^7^8";//^5";
                        objBaseDataGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                        objBaseDataGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons"); //"1^Add New^0^addRecord";
                        objBaseDataGrid.PageSize = int.Parse(GetPageSize());
                        objBaseDataGrid.CacheSize = int.Parse(GetCacheSize()); ;
                        objBaseDataGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                        objBaseDataGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif"; ;
                        objBaseDataGrid.HeaderString = objResourceMgr.GetString("HeaderString"); //"Location Details";
                        objBaseDataGrid.SelectClass = colors;
                        objBaseDataGrid.RequireQuery = "Y";
                        objBaseDataGrid.DefaultSearch = "Y";
                        objBaseDataGrid.QueryStringColumns = "PRODUCT_RISK_ID";
                        objBaseDataGrid.FilterLabel = objResourceMgr.GetString("FilterLabel"); //"Include Inactive";
                        objBaseDataGrid.FilterValue = "Y";
                        objBaseDataGrid.FilterColumnName = "POL_PRODUCT_LOCATION_INFO.IS_ACTIVE";
                        objBaseDataGrid.ShowExcluded = true;
                        GridHolder.Controls.Add(c1);




                        //Set Tan For Page 
                        Url = "../AddProductLocationInfo.aspx?CUSTOMER_ID=" + strCustomerID + "&POL_ID=" + strPolId + "&POL_VERSION_ID=" + strPolVersionId + "&CalledFrom=" + CalledFrom + "&";
                        TabCtl.TabURLs = Url;

                        TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl_CompComp"); //Get Tab Titles From XML File

                        #endregion

                        break;
                    case "DWELLING":  //For Dwelling 

                        #region For Dwelling  
                        objBaseDataGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                        objBaseDataGrid.SelectClause = "POL_PRODUCT_LOCATION_INFO.PRODUCT_RISK_ID,POL_LOCATIONS.LOC_NUM as LOC_NUM ,LOCATION_NUMBER,ITEM_NUMBER "
                            + ",Rtrim(ltrim((ISNULL(NAME,'')+' - '+ISNULL(POL_LOCATIONS.LOC_ADD1,'')+', '+ISNULL(CONVERT(VARCHAR(10), POL_LOCATIONS.NUMBER),'')+' ' +ISNULL(POL_LOCATIONS.LOC_ADD2,'')+' - '+ISNULL(POL_LOCATIONS.DISTRICT,'')+' - '+ ISNULL(POL_LOCATIONS.LOC_CITY,'')+'/'+MCSL.STATE_CODE +' - '+ ISNULL(POL_LOCATIONS.LOC_ZIP,'') )))  AS Loc_Address"
                            + ",POL_PRODUCT_LOCATION_INFO.IS_ACTIVE";
                        objBaseDataGrid.FromClause = " POL_PRODUCT_LOCATION_INFO left outer join POL_LOCATIONS on POL_PRODUCT_LOCATION_INFO.LOCATION=POL_LOCATIONS.LOCATION_ID"
                                                    + " AND POL_PRODUCT_LOCATION_INFO.CUSTOMER_ID=POL_LOCATIONS.CUSTOMER_ID AND POL_PRODUCT_LOCATION_INFO.POLICY_ID=POL_LOCATIONS.POLICY_ID AND POL_PRODUCT_LOCATION_INFO.POLICY_VERSION_ID= POL_LOCATIONS.POLICY_VERSION_ID "
                                                    + " left outer join MNT_COUNTRY_STATE_LIST MCSL on MCSL.STATE_ID=POL_LOCATIONS.LOC_STATE"
                                                    + " AND POL_LOCATIONS.LOC_COUNTRY=MCSL.COUNTRY_ID"; 
                        objBaseDataGrid.WhereClause = " POL_PRODUCT_LOCATION_INFO.CUSTOMER_ID = '" + strCustomerID
                            + "' AND POL_PRODUCT_LOCATION_INFO.POLICY_ID = '" + strPolId
                            + "' AND POL_PRODUCT_LOCATION_INFO.POLICY_VERSION_ID = '" + strPolVersionId
                            + "'";

                        objBaseDataGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_Dwelling"); //"Location #,Address;
                        objBaseDataGrid.SearchColumnNames = "IsNull(POL_LOCATIONS.LOC_NUM,'')^ISNULL(NAME,'')! ' ' !IsNull(POL_LOCATIONS.LOC_ADD1,'')! ' ' !ISNULL(CONVERT(VARCHAR(10), POL_LOCATIONS.NUMBER),'')! ' ' !IsNull(POL_LOCATIONS.LOC_ADD2,'')! ' ' ! ISNULL(POL_LOCATIONS.DISTRICT,'') ! ' ' ! IsNull(POL_LOCATIONS.LOC_CITY,'')! ' ' !ISNULL(POL_LOCATIONS.LOC_ZIP,'')^LOCATION_NUMBER^ITEM_NUMBER";
                        objBaseDataGrid.SearchColumnType = "T^T^T^T";//^T";
                        objBaseDataGrid.OrderByClause = "POL_PRODUCT_LOCATION_INFO.PRODUCT_RISK_ID";
                        objBaseDataGrid.DisplayColumnNumbers = "2^3^4^5";//^4";
                        objBaseDataGrid.DisplayColumnNames = "LOC_NUM^Loc_Address^LOCATION_NUMBER^ITEM_NUMBER";//^OCCU";
                        objBaseDataGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_Dwelling"); //"Location #^Address;
                        objBaseDataGrid.DisplayTextLength = "10^40^15^15";//^30";
                        objBaseDataGrid.DisplayColumnPercent = "10^40^15^15";//^30";
                        objBaseDataGrid.ColumnTypes = "B^B^B^B";//^B";
                        objBaseDataGrid.PrimaryColumns = "1";
                        objBaseDataGrid.PrimaryColumnsName = "PRODUCT_RISK_ID";
                        objBaseDataGrid.AllowDBLClick = "true";
                        objBaseDataGrid.FetchColumns = "1^2^3^4^5^6";//^5";
                        objBaseDataGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                        objBaseDataGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons"); //"1^Add New^0^addRecord";
                        objBaseDataGrid.PageSize = int.Parse(GetPageSize());
                        objBaseDataGrid.CacheSize = int.Parse(GetCacheSize()); ;
                        objBaseDataGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                        objBaseDataGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif"; ;
                        objBaseDataGrid.HeaderString = objResourceMgr.GetString("HeaderString"); //"Location Details";
                        objBaseDataGrid.SelectClass = colors;
                        objBaseDataGrid.RequireQuery = "Y";
                        objBaseDataGrid.DefaultSearch = "Y";
                        objBaseDataGrid.QueryStringColumns = "PRODUCT_RISK_ID";
                        objBaseDataGrid.FilterLabel = objResourceMgr.GetString("FilterLabel"); //"Include Inactive";
                        objBaseDataGrid.FilterValue = "Y";
                        objBaseDataGrid.FilterColumnName = "POL_PRODUCT_LOCATION_INFO.IS_ACTIVE";
                        GridHolder.Controls.Add(c1);

                        //Set Tan For Page 
                        Url = "../AddPolicyDwellingInfo.aspx?CUSTOMER_ID=" + strCustomerID + "&POL_ID=" + strPolId + "&POL_VERSION_ID=" + strPolVersionId + "&CalledFrom=DWELLING&";
                        TabCtl.TabURLs = Url;

                        TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl"); //Get Tab Titles From XML File
                        #endregion

                        break;

                    case "GENCVLLIB": //for General Civil Liability

                        #region for General Civil Liability
                        objBaseDataGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                        objBaseDataGrid.SelectClause = "POL_PRODUCT_LOCATION_INFO.PRODUCT_RISK_ID,POL_LOCATIONS.LOC_NUM as LOC_NUM ,LOCATION_NUMBER,ITEM_NUMBER "
                            + ",Rtrim(ltrim((ISNULL(NAME,'')+' - '+ISNULL(POL_LOCATIONS.LOC_ADD1,'')+', '+ISNULL(CONVERT(VARCHAR(10), POL_LOCATIONS.NUMBER),'')+' ' +ISNULL(POL_LOCATIONS.LOC_ADD2,'')+' - '+ISNULL(POL_LOCATIONS.DISTRICT,'')+' - '+ ISNULL(POL_LOCATIONS.LOC_CITY,'')+'/'+MCSL.STATE_CODE +' - '+ ISNULL(POL_LOCATIONS.LOC_ZIP,''))))  AS Loc_Address ,dbo.fun_FormatCurrency(POL_PRODUCT_LOCATION_INFO.VALUE_AT_RISK,2) as VALUE_AT_RISK,dbo.fun_FormatCurrency(POL_PRODUCT_LOCATION_INFO.MAXIMUM_LIMIT,2) as MAXIMUM_LIMIT"
                            + ",POL_PRODUCT_LOCATION_INFO.IS_ACTIVE";
                        objBaseDataGrid.FromClause = " POL_PRODUCT_LOCATION_INFO left outer join POL_LOCATIONS on POL_PRODUCT_LOCATION_INFO.LOCATION=POL_LOCATIONS.LOCATION_ID"
                                                    + " AND POL_PRODUCT_LOCATION_INFO.CUSTOMER_ID=POL_LOCATIONS.CUSTOMER_ID AND POL_PRODUCT_LOCATION_INFO.POLICY_ID=POL_LOCATIONS.POLICY_ID AND POL_PRODUCT_LOCATION_INFO.POLICY_VERSION_ID= POL_LOCATIONS.POLICY_VERSION_ID "
                                                    + " left outer join MNT_COUNTRY_STATE_LIST MCSL on MCSL.STATE_ID=POL_LOCATIONS.LOC_STATE"
                                                    + " AND POL_LOCATIONS.LOC_COUNTRY=MCSL.COUNTRY_ID"; 

                        objBaseDataGrid.WhereClause = " POL_PRODUCT_LOCATION_INFO.CUSTOMER_ID = '" + strCustomerID
                            + "' AND POL_PRODUCT_LOCATION_INFO.POLICY_ID = '" + strPolId
                            + "' AND POL_PRODUCT_LOCATION_INFO.POLICY_VERSION_ID = '" + strPolVersionId
                            + "'";

                        objBaseDataGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_GenCvlLib"); //"Location #,Address;
                        objBaseDataGrid.SearchColumnNames = "IsNull(POL_LOCATIONS.LOC_NUM,'')^ISNULL(NAME,'')! ' ' !IsNull(POL_LOCATIONS.LOC_ADD1,'')! ' ' !ISNULL(CONVERT(VARCHAR(10), POL_LOCATIONS.NUMBER),'')! ' ' !IsNull(POL_LOCATIONS.LOC_ADD2,'') ! ' ' ! ISNULL(POL_LOCATIONS.DISTRICT,'') ! ' ' ! IsNull(POL_LOCATIONS.LOC_CITY,'') ! ' ' ! ISNULL(POL_LOCATIONS.LOC_ZIP,'')^POL_PRODUCT_LOCATION_INFO.VALUE_AT_RISK^POL_PRODUCT_LOCATION_INFO.MAXIMUM_LIMIT^LOCATION_NUMBER^ITEM_NUMBER";
                        objBaseDataGrid.SearchColumnType = "T^T^T^T^T^T";//^T";
                        objBaseDataGrid.OrderByClause = "POL_PRODUCT_LOCATION_INFO.PRODUCT_RISK_ID";
                        objBaseDataGrid.DisplayColumnNumbers = "2^3^4^5^6^7";//^4";
                        objBaseDataGrid.DisplayColumnNames = "LOC_NUM^Loc_Address^VALUE_AT_RISK^MAXIMUM_LIMIT^LOCATION_NUMBER^ITEM_NUMBER";//^OCCU";
                        objBaseDataGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_GenCvlLib"); //"Location #^Address;
                        objBaseDataGrid.DisplayTextLength = "10^30^20^20^10^10";//^30";
                        objBaseDataGrid.DisplayColumnPercent = "10^30^20^20^10^10";//^30";
                        objBaseDataGrid.ColumnTypes = "B^B^B^B^B^B";//^B";
                        objBaseDataGrid.PrimaryColumns = "1";
                        objBaseDataGrid.PrimaryColumnsName = "PRODUCT_RISK_ID";
                        objBaseDataGrid.AllowDBLClick = "true";
                        objBaseDataGrid.FetchColumns = "1^2^3^4^5^6^7^8";//^5";
                        objBaseDataGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                        objBaseDataGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons"); //"1^Add New^0^addRecord";
                        objBaseDataGrid.PageSize = int.Parse(GetPageSize());
                        objBaseDataGrid.CacheSize = int.Parse(GetCacheSize()); ;
                        objBaseDataGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                        objBaseDataGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif"; ;
                        objBaseDataGrid.HeaderString = objResourceMgr.GetString("HeaderString"); //"Location Details";
                        objBaseDataGrid.SelectClass = colors;
                        objBaseDataGrid.RequireQuery = "Y";
                        objBaseDataGrid.DefaultSearch = "Y";
                        objBaseDataGrid.QueryStringColumns = "PRODUCT_RISK_ID";
                        objBaseDataGrid.FilterLabel = objResourceMgr.GetString("FilterLabel"); //"Include Inactive";
                        objBaseDataGrid.FilterValue = "Y";
                        objBaseDataGrid.FilterColumnName = "POL_PRODUCT_LOCATION_INFO.IS_ACTIVE";
                        objBaseDataGrid.CellHorizontalAlign = "2^3";
                        GridHolder.Controls.Add(c1);

                        Url = "../AddPolicyDwellingInfo.aspx?CUSTOMER_ID=" + strCustomerID + "&POL_ID=" + strPolId + "&POL_VERSION_ID=" + strPolVersionId + "&CalledFrom=GenCvlLib&";
                        TabCtl.TabURLs = Url;
                     
                        TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl"); //Get Tab Titles From XML File
                        //Set the BaseDataGrid Control Properties
                        #endregion
                        break;
                    default:

                        break;

                }

            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }

            SetWorkFlow();
        }

        #region Methods
        private void GetSessionValues()
        {
            strPolId = base.GetPolicyID();
            strPolVersionId = base.GetPolicyVersionID();
            strCustomerID = base.GetCustomerID();
        }

        private bool CanShow()
        {
            //Checking whether customer id exits in database or not
            if (strPolId == "")
            {
                return false;
            }

            return true;
        }

        private void SetWorkFlow()
        {
            myWorkFlow.ScreenID = ScreenId;
            myWorkFlow.AddKeyValue("CUSTOMER_ID", GetCustomerID());
            myWorkFlow.AddKeyValue("POLICY_ID", GetPolicyID());
            myWorkFlow.AddKeyValue("POLICY_VERSION_ID", GetPolicyVersionID());
            myWorkFlow.WorkflowModule = "POL";

        }
        #endregion

    }
}
