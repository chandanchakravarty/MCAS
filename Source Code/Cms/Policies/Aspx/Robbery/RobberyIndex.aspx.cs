//------------------------------------------------------------------------------
// Created by Pradeep Kushwaha on 21-05-2010
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


namespace Cms.Policies.Aspx.Robbery
{
    public partial class RobberyIndex : Cms.CmsWeb.cmsbase
    {
       
        private string strCustomerID, strPolId, strPolVersionId;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
        protected System.Web.UI.WebControls.Label capMessage;
        protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
      
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        ResourceManager objResourceMgr = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string Url = "";
            objResourceMgr = new ResourceManager("Cms.Policies.Aspx.Robbery.RobberyIndex", Assembly.GetExecutingAssembly());
            GetSessionValues();

            if (!CanShow())
            {
                capMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("109");
                capMessage.Visible = true;
                return;
            }
            base.ScreenId = ROBBERYscreenId.INDEX_PAGE;
            
            
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
                objBaseDataGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                objBaseDataGrid.SelectClause = "POL_PRODUCT_LOCATION_INFO.PRODUCT_RISK_ID,POL_LOCATIONS.LOC_NUM as LOC_NUM ,LOCATION_NUMBER,ITEM_NUMBER"
                    + ",Rtrim(ltrim((ISNULL(NAME,'')+'-'+ISNULL(POL_LOCATIONS.LOC_ADD1,'')+'-'+ISNULL(CONVERT(VARCHAR(10), POL_LOCATIONS.LOC_NUM),'')+'-' +ISNULL(POL_LOCATIONS.LOC_ADD2,'')+'-'+ISNULL(POL_LOCATIONS.DISTRICT,'')+'-'+ ISNULL(POL_LOCATIONS.LOC_CITY,'') +'-'+ ISNULL(POL_LOCATIONS.LOC_ZIP,''))))  AS Loc_Address ,dbo.fun_FormatCurrency(POL_PRODUCT_LOCATION_INFO.VALUE_AT_RISK,2) as VALUE_AT_RISK,dbo.fun_FormatCurrency(POL_PRODUCT_LOCATION_INFO.MAXIMUM_LIMIT,2) as MAXIMUM_LIMIT "
                    + ",POL_PRODUCT_LOCATION_INFO.IS_ACTIVE";
                objBaseDataGrid.FromClause = " POL_PRODUCT_LOCATION_INFO left outer join POL_LOCATIONS on POL_PRODUCT_LOCATION_INFO.LOCATION=POL_LOCATIONS.LOCATION_ID"
                    + " AND POL_PRODUCT_LOCATION_INFO.CUSTOMER_ID=POL_LOCATIONS.CUSTOMER_ID AND POL_PRODUCT_LOCATION_INFO.POLICY_ID=POL_LOCATIONS.POLICY_ID AND POL_PRODUCT_LOCATION_INFO.POLICY_VERSION_ID= POL_LOCATIONS.POLICY_VERSION_ID ";
                objBaseDataGrid.WhereClause = " POL_PRODUCT_LOCATION_INFO.CUSTOMER_ID = '" + strCustomerID
                    + "' AND POL_PRODUCT_LOCATION_INFO.POLICY_ID = '" + strPolId
                    + "' AND POL_PRODUCT_LOCATION_INFO.POLICY_VERSION_ID = '" + strPolVersionId
                    + "'";

                objBaseDataGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings"); //"Location #,Address;
                objBaseDataGrid.SearchColumnNames = "IsNull(POL_LOCATIONS.LOC_NUM,'')^ISNULL(NAME,'') ! ' ' ! IsNull(POL_LOCATIONS.LOC_ADD1,'')! ' ' !ISNULL(CONVERT(VARCHAR(10), POL_LOCATIONS.LOC_NUM),'')! ' ' !IsNull(POL_LOCATIONS.LOC_ADD2,'') ! ' ' ! ISNULL(POL_LOCATIONS.DISTRICT,'') ! ' ' ! IsNull(POL_LOCATIONS.LOC_CITY,'') ! ' ' ! ISNULL(POL_LOCATIONS.LOC_ZIP,'')^POL_PRODUCT_LOCATION_INFO.VALUE_AT_RISK^POL_PRODUCT_LOCATION_INFO.MAXIMUM_LIMIT^LOCATION_NUMBER^ITEM_NUMBER";
                objBaseDataGrid.SearchColumnType = "T^T^T^T^T^T";//^T";
                objBaseDataGrid.OrderByClause = "POL_PRODUCT_LOCATION_INFO.PRODUCT_RISK_ID";
                objBaseDataGrid.DisplayColumnNumbers = "2^3^4^5^6^7";//^4";
                objBaseDataGrid.DisplayColumnNames = "LOC_NUM^Loc_Address^VALUE_AT_RISK^MAXIMUM_LIMIT^LOCATION_NUMBER^ITEM_NUMBER";//^OCCU";
                objBaseDataGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings"); //"Location #^Address;
                objBaseDataGrid.DisplayTextLength = "10^30^10^10^10^10";//^30";
                objBaseDataGrid.DisplayColumnPercent = "10^30^10^10^10^10";//^30";
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

                //Set Tan For Page 
                Url = "../AddPolicyDwellingInfo.aspx?CUSTOMER_ID=" + strCustomerID + "&POL_ID=" + strPolId + "&POL_VERSION_ID=" + strPolVersionId + "&CalledFrom=ROBBERY&";
                TabCtl.TabURLs = Url;

                TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl"); //Get Tab Titles From XML File
                //Set the BaseDataGrid Control Properties
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
