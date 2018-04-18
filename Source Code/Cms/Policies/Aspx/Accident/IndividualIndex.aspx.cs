using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cms.CmsWeb.WebControls;
using System.Resources;
using System.Reflection;
using Cms.CmsWeb.webcontrols;
using Cms.BusinessLayer.BlCommon;

namespace Cms.Policies.Aspx.Accident
{
    public partial class IndividualIndex : Cms.Policies.policiesbase
    {
        #region Variables
        private string strCustomerID, strPolId, strPolVersionId;
        ResourceManager objResourceMgr = null;
        protected BaseTabControl TabCtl;
        protected ClientTop cltClientTop;
        protected Label capMessage;
        protected WorkFlow myWorkFlow;
        protected PlaceHolder GridHolder;
        String CalledFrom = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"].ToString() != "")
            {
                CalledFrom = Request.QueryString["CalledFrom"].ToString();
                switch (CalledFrom.ToUpper().Trim())
                {
                    case "INDPA":
                        base.ScreenId =INDIVIDUAL_PERSONAL_ACCIDENTscreenId.INDEX_PAGE;// "116_1_1";
                        break;
                    case "DPVA":
                        base.ScreenId = DPVATscreenId.INDEX_PAGE;
                        break;
                    case "DPVAT2":
                        base.ScreenId = DPVAT2screenId.INDEX_PAGE;
                        break;
                    case "MRTG":
                        base.ScreenId = MORTGAGEscreenId.INDEX_PAGE;
                            break;
                    case "GRPLF":
                        base.ScreenId = GROUP_LIFEscreenId.INDEX_PAGE;
                        break;
                    case "CPCACC":
                        base.ScreenId = GROUP_PERSONAL_ACCIDENTscreenId.INDEX_PAGE;
                        break;
                    case "PAPEACC":
                        base.ScreenId = PASSENGERS_PERSONAL_ACCIDENTscreenId.INDEX_PAGE;
                        break;
                    
                }
            }

            objResourceMgr = new ResourceManager("Cms.Policies.Aspx.Accident.IndividualIndex", Assembly.GetExecutingAssembly());
            GetSessionValues();
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

            #region loading web grid control

            Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
            BaseDataGrid objWebGrid;
            objWebGrid = (BaseDataGrid)c1;
            String AgencyId = String.Empty;
            try
            {

                switch (CalledFrom.ToUpper().Trim())
                {
                    case "INDPA":  // INDIVILUAL PERSONAL ACCIDENT
                        #region INDIVILUAL PERSONAL ACCIDENT
                        //Setting web grid control properties
                        objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

                        objWebGrid.SelectClause = "PERSONAL_INFO_ID,INDIVIDUAL_NAME,MAM.ACTIVITY_DESC as Position,isnull(CONVERT(VARCHAR(10),DATE_OF_BIRTH,case when " + ClsCommon.BL_LANG_ID + "= 2 then 103 else 101 end),'' ) AS DATE_OF_BIRTH,POL_PERSONAL_ACCIDENT_INFO.IS_ACTIVE,(FIRST_NAME + ' ' + IsNull(MIDDLE_NAME,'') + ' ' + IsNull(LAST_NAME,'')) as APPLICANT";

                        objWebGrid.FromClause = "POL_PERSONAL_ACCIDENT_INFO left outer join MNT_ACTIVITY_MASTER MAM on POL_PERSONAL_ACCIDENT_INFO.POSITION_ID=MAM.ACTIVITY_ID"
                                                +" INNER JOIN CLT_APPLICANT_LIST CLT WITH(NOLOCK) ON CLT.APPLICANT_ID=POL_PERSONAL_ACCIDENT_INFO.APPLICANT_ID";
                        objWebGrid.WhereClause = " POL_PERSONAL_ACCIDENT_INFO.CUSTOMER_ID = '" + strCustomerID
                            + "' AND POL_PERSONAL_ACCIDENT_INFO.POLICY_ID = '" + strPolId
                            + "' AND POL_PERSONAL_ACCIDENT_INFO.POLICY_VERSION_ID = '" + strPolVersionId
                            + "'";


                        objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");
                        objWebGrid.SearchColumnNames = "INDIVIDUAL_NAME^MNT_LOOKUP_VALUES.LOOKUP_VALUE_DESC^DATE_OF_BIRTH^FIRST_NAME ! ' ' ! IsNull(MIDDLE_NAME,'') ! ' ' ! IsNull(LAST_NAME,'')";
                        objWebGrid.SearchColumnType = "T^T^T^T";

                        objWebGrid.OrderByClause = "PERSONAL_INFO_ID ASC";

                        objWebGrid.DisplayColumnNumbers = "2^3^4^5";
                        objWebGrid.DisplayColumnNames = "INDIVIDUAL_NAME^Position^DATE_OF_BIRTH^APPLICANT";
                        objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");

                        objWebGrid.DisplayTextLength = "20^40^20^20";
                        objWebGrid.DisplayColumnPercent = "20^40^20^20";
                        objWebGrid.ColumnTypes = "B^B^B^B";

                        objWebGrid.PrimaryColumns = "1";
                        objWebGrid.PrimaryColumnsName = "PERSONAL_INFO_ID";

                        objWebGrid.AllowDBLClick = "true";
                        objWebGrid.FetchColumns = "1^2^3^4^5^6";

                        objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");

                        AgencyId = GetSystemId();
                        //if (AgencyId.ToUpper() != System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToUpper())
                        objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons"); //"1^Add New^0^addRecord";
                        //else
                        //    objWebGrid.ExtraButtons = "3^Hull Informations~Coverages~Prior Loss Tab^0~1^addRecord~FetchLossReport~PriorLossTab";

                        objWebGrid.PageSize = int.Parse(GetPageSize());
                        objWebGrid.CacheSize = int.Parse(GetCacheSize());
                        objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                        objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                        objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");
                        objWebGrid.SelectClass = colors;
                        objWebGrid.RequireQuery = "Y";
                        objWebGrid.DefaultSearch = "Y";
                        objWebGrid.QueryStringColumns = "PERSONAL_INFO_ID";
                        objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel"); //"Include Inactive";
                        objWebGrid.FilterValue = "Y";
                        objWebGrid.FilterColumnName = "POL_PERSONAL_ACCIDENT_INFO.IS_ACTIVE";
                        objWebGrid.ShowExcluded = true;
                        //Adding to controls to gridholder
                        GridHolder.Controls.Add(c1);
                        TabCtl.TabURLs = "AddInvidualInfo.aspx?CUSTOMER_ID=" + strCustomerID
                                          + "&POL_ID=" + strPolId
                                          + "&POL_VERSION_ID=" + strPolVersionId + "&CalledFrom=INDPA&";

                        TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
                        #endregion
                        break;
                    case "DPVA"://Dpvat(Cat. 3 e 4)
                    case "DPVAT2"://DPVAT(Cat. 1,2,9 e 10)
                        #region Dpvat(Cat. 3 e 4) and DPVAT(Cat. 1,2,9 e 10)
                        //Setting web grid control properties
                        objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

                        objWebGrid.SelectClause = " CIV_TRA.VEHICLE_ID,CIV_TRA.TICKET_NUMBER,"
                                                + " MNT_LOOKUP_VALUES.LOOKUP_VALUE_DESC AS CATEGORY,CIV_TRA.IS_ACTIVE ";

                        objWebGrid.FromClause = "POL_CIVIL_TRANSPORT_VEHICLES CIV_TRA WITH(NOLOCK) "
                                                +" LEFT OUTER JOIN MNT_LOOKUP_VALUES WITH(NOLOCK)"
                                                +" ON CIV_TRA.CATEGORY=MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID ";
                        objWebGrid.WhereClause = " CIV_TRA.CUSTOMER_ID = '" + strCustomerID
                            + "' AND  CIV_TRA.POLICY_ID= '" + strPolId
                            + "' AND CIV_TRA.POLICY_VERSION_ID = '" + strPolVersionId
                            + "'";


                        objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadingsForDpvat");
                        objWebGrid.SearchColumnNames = "CIV_TRA.TICKET_NUMBER^MNT_LOOKUP_VALUES.LOOKUP_VALUE_DESC ";
                        objWebGrid.SearchColumnType = "T^T";

                        objWebGrid.OrderByClause = " VEHICLE_ID ASC ";

                        objWebGrid.DisplayColumnNumbers = "2^3";
                        objWebGrid.DisplayColumnNames = "TICKET_NUMBER^CATEGORY";
                        objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadingsForDpvat");

                        objWebGrid.DisplayTextLength = "20^20";
                        objWebGrid.DisplayColumnPercent = "20^20";
                        objWebGrid.ColumnTypes = "B^B";

                        objWebGrid.PrimaryColumns = "1";
                        objWebGrid.PrimaryColumnsName = "VEHICLE_ID";

                        objWebGrid.AllowDBLClick = "true";
                        objWebGrid.FetchColumns = "1^2^3^4";

                        objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");

                        AgencyId = GetSystemId();
                        //if (AgencyId.ToUpper() != System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToUpper())
                        objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons"); //"1^Add New^0^addRecord";
                        //else
                        //    objWebGrid.ExtraButtons = "3^Hull Informations~Coverages~Prior Loss Tab^0~1^addRecord~FetchLossReport~PriorLossTab";

                        objWebGrid.PageSize = int.Parse(GetPageSize());
                        objWebGrid.CacheSize = int.Parse(GetCacheSize());
                        objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                        objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                        objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");
                        objWebGrid.SelectClass = colors;
                        objWebGrid.RequireQuery = "Y";
                        objWebGrid.DefaultSearch = "Y";
                        objWebGrid.QueryStringColumns = "VEHICLE_ID";
                        objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel"); //"Include Inactive";
                        objWebGrid.FilterValue = "Y";
                        objWebGrid.FilterColumnName = "CIV_TRA.IS_ACTIVE";
                        objWebGrid.ShowExcluded = true;
                        //Adding to controls to gridholder
                        GridHolder.Controls.Add(c1);
                        TabCtl.TabURLs = "../AddDpvatInfo.aspx?CUSTOMER_ID=" + strCustomerID
                                          + "&POL_ID=" + strPolId
                                          + "&POL_VERSION_ID=" + strPolVersionId + "&CalledFrom=" + CalledFrom + "&";

                        TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
                        #endregion
                        break;
                    case "MRTG":
                    case "GRPLF": //FOR GROUP LIFE 
                    case "CPCACC": //FOR THE GROUP PERSONAL ACCEIDENT
                        #region FOR THE GROUP PERSONAL ACCEIDENT
                        objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

                        objWebGrid.SelectClause = "PERSONAL_INFO_ID,INDIVIDUAL_NAME,MAM.ACTIVITY_DESC as Position,isnull(CONVERT(VARCHAR(10),DATE_OF_BIRTH,case when " + ClsCommon.BL_LANG_ID+ "=2 then 103 else 101 end),'') AS DATE_OF_BIRTH,POL_PERSONAL_ACCIDENT_INFO.IS_ACTIVE ,(FIRST_NAME + ' ' + IsNull(MIDDLE_NAME,'') + ' ' + IsNull(LAST_NAME,'')) as APPLICANT";

                        objWebGrid.FromClause = "POL_PERSONAL_ACCIDENT_INFO left outer join MNT_ACTIVITY_MASTER MAM on POL_PERSONAL_ACCIDENT_INFO.POSITION_ID=MAM.ACTIVITY_ID"
                                                + " INNER JOIN CLT_APPLICANT_LIST CLT WITH(NOLOCK) ON CLT.APPLICANT_ID=POL_PERSONAL_ACCIDENT_INFO.APPLICANT_ID";
                        objWebGrid.WhereClause = " POL_PERSONAL_ACCIDENT_INFO.CUSTOMER_ID = '" + strCustomerID
                            + "' AND POL_PERSONAL_ACCIDENT_INFO.POLICY_ID = '" + strPolId
                            + "' AND POL_PERSONAL_ACCIDENT_INFO.POLICY_VERSION_ID = '" + strPolVersionId
                            + "'";


                        objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_FOR_GROUP");
                        objWebGrid.SearchColumnNames = "INDIVIDUAL_NAME^MAM.ACTIVITY_DESC^DATE_OF_BIRTH^FIRST_NAME ! ' ' ! IsNull(MIDDLE_NAME,'') ! ' ' ! IsNull(LAST_NAME,'')";
                        objWebGrid.SearchColumnType = "T^T^D^T";

                        objWebGrid.OrderByClause = "PERSONAL_INFO_ID ASC";

                        objWebGrid.DisplayColumnNumbers = "2^3^4^5";
                        objWebGrid.DisplayColumnNames = "INDIVIDUAL_NAME^Position^DATE_OF_BIRTH^APPLICANT";
                        objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_FOR_GROUP");

                        objWebGrid.DisplayTextLength = "20^20^20^20";
                        objWebGrid.DisplayColumnPercent = "20^20^20^20";
                        objWebGrid.ColumnTypes = "B^B^B^B";

                        objWebGrid.PrimaryColumns = "1";
                        objWebGrid.PrimaryColumnsName = "PERSONAL_INFO_ID";

                        objWebGrid.AllowDBLClick = "true";
                        objWebGrid.FetchColumns = "1^2^3^4^5^6";

                        objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");

                        AgencyId = GetSystemId();
                        //if (AgencyId.ToUpper() != System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToUpper())
                        objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons"); //"1^Add New^0^addRecord";
                        //else
                        //    objWebGrid.ExtraButtons = "3^Hull Informations~Coverages~Prior Loss Tab^0~1^addRecord~FetchLossReport~PriorLossTab";

                        objWebGrid.PageSize = int.Parse(GetPageSize());
                        objWebGrid.CacheSize = int.Parse(GetCacheSize());
                        objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                        objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                        objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");
                        objWebGrid.SelectClass = colors;
                        objWebGrid.RequireQuery = "Y";
                        objWebGrid.DefaultSearch = "Y";
                        objWebGrid.QueryStringColumns = "PERSONAL_INFO_ID";
                        objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel"); //"Include Inactive";
                        objWebGrid.FilterValue = "Y";
                        objWebGrid.FilterColumnName = "POL_PERSONAL_ACCIDENT_INFO.IS_ACTIVE";
                        objWebGrid.ShowExcluded = true;
                        //Adding to controls to gridholder
                        GridHolder.Controls.Add(c1);
                        TabCtl.TabURLs = "AddInvidualInfo.aspx?CUSTOMER_ID=" + strCustomerID
                                         + "&POL_ID=" + strPolId
                                         + "&POL_VERSION_ID=" + strPolVersionId + "&CalledFrom=" + CalledFrom + "&";

                        TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
                        #endregion
                        break;
                    case "PAPEACC": //Personal Accident for Passengers 
                        
                        #region //Personal Accident for Passengers
                        objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

                        objWebGrid.SelectClause = "PERSONAL_ACCIDENT_ID,isnull(CONVERT(VARCHAR(10),START_DATE,case when " + ClsCommon.BL_LANG_ID + "= 2 then 103 else 101 end),'') AS START_DATE ,isnull(CONVERT(VARCHAR(10),END_DATE,case when " + ClsCommon.BL_LANG_ID + "= 2 then 103 else 101 end),'') AS END_DATE  ,NUMBER_OF_PASSENGERS,POL_PASSENGERS_PERSONAL_ACCIDENT_INFO.IS_ACTIVE,(FIRST_NAME + ' ' + IsNull(MIDDLE_NAME,'') + ' ' + IsNull(LAST_NAME,'')) as APPLICANT,ISNULL(CAST(RISK_ORIGINAL_ENDORSEMENT_NO AS NVARCHAR),'NBS') AS RISK_ORIGINAL_ENDORSEMENT_NO";

                        objWebGrid.FromClause = "POL_PASSENGERS_PERSONAL_ACCIDENT_INFO INNER JOIN CLT_APPLICANT_LIST CLT WITH(NOLOCK) ON CLT.APPLICANT_ID=POL_PASSENGERS_PERSONAL_ACCIDENT_INFO.CO_APPLICANT_ID";

                        objWebGrid.WhereClause = " POL_PASSENGERS_PERSONAL_ACCIDENT_INFO.CUSTOMER_ID = '" + strCustomerID
                            + "' AND POL_PASSENGERS_PERSONAL_ACCIDENT_INFO.POLICY_ID = '" + strPolId
                            + "' AND POL_PASSENGERS_PERSONAL_ACCIDENT_INFO.POLICY_VERSION_ID = '" + strPolVersionId
                            + "'";


                        objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings_FOR_PARSONAL");
                        objWebGrid.SearchColumnNames = "START_DATE^END_DATE^NUMBER_OF_PASSENGERS^FIRST_NAME ! ' ' ! IsNull(MIDDLE_NAME,'') ! ' ' ! IsNull(LAST_NAME,'')^ISNULL(CAST(RISK_ORIGINAL_ENDORSEMENT_NO AS NVARCHAR),'NBS')";
                        objWebGrid.SearchColumnType = "D^D^T^T^T";//itrack-1537 tfs#466
                        // changed by praveer for itrack no 1390, only for product 0520 
                        objWebGrid.OrderByClause = "PERSONAL_ACCIDENT_ID DESC";

                        objWebGrid.DisplayColumnNumbers = "2^3^4^5^6";
                        objWebGrid.DisplayColumnNames = "RISK_ORIGINAL_ENDORSEMENT_NO^START_DATE^END_DATE^NUMBER_OF_PASSENGERS^APPLICANT";
                        objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings_FOR_PARSONAL");

                        objWebGrid.DisplayTextLength = "20^20^20^20^20";
                        objWebGrid.DisplayColumnPercent = "20^20^20^20^20";
                        objWebGrid.ColumnTypes = "B^B^B^B^B";

                        objWebGrid.PrimaryColumns = "1";
                        objWebGrid.PrimaryColumnsName = "PERSONAL_ACCIDENT_ID";

                        objWebGrid.AllowDBLClick = "true";
                        objWebGrid.FetchColumns = "1^2^3^4^5^6^7";

                        objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");

                        AgencyId = GetSystemId();
                        //if (AgencyId.ToUpper() != System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToUpper())
                        objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons"); //"1^Add New^0^addRecord";
                        //else
                        //    objWebGrid.ExtraButtons = "3^Hull Informations~Coverages~Prior Loss Tab^0~1^addRecord~FetchLossReport~PriorLossTab";

                        objWebGrid.PageSize = int.Parse(GetPageSize());
                        objWebGrid.CacheSize = int.Parse(GetCacheSize());
                        objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                        objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                        objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");
                        objWebGrid.SelectClass = colors;
                        objWebGrid.RequireQuery = "Y";
                        objWebGrid.DefaultSearch = "Y";
                        objWebGrid.QueryStringColumns = "PERSONAL_ACCIDENT_ID";
                        objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel"); //"Include Inactive";
                        objWebGrid.FilterValue = "Y";
                        objWebGrid.FilterColumnName = "POL_PASSENGERS_PERSONAL_ACCIDENT_INFO.IS_ACTIVE";
                        objWebGrid.ShowExcluded = true;
                        //Adding to controls to gridholder
                        GridHolder.Controls.Add(c1);
                        TabCtl.TabURLs = "AddPassengerAccidentInfo.aspx?CUSTOMER_ID=" + strCustomerID
                                         + "&POL_ID=" + strPolId
                                         + "&POL_VERSION_ID=" + strPolVersionId + "&CalledFrom=PAPEACC&";

                        TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
                        #endregion
                        
                        break;
                }
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            #endregion

          

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
