using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.IO;
using Cms.CmsWeb.WebControls;
using System.Reflection;
using System.Resources;
using Cms.BusinessLayer.BlCommon;

namespace Cms.Policies.Aspx.Transportation
{
    public partial class TransportationIndex : Cms.Policies.policiesbase
    {
        private int CacheSize = 1400;
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        ResourceManager objResourceMgr = null;
        protected WorkFlow myWorkFlow;
        String CalledFrom = string.Empty;
        private string strCustomerID, strPolId, strPolVersionId;
        /// <summary>
        /// This method is use to load the all required details on the page load 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"].ToString() != "")
            {
                CalledFrom = Request.QueryString["CalledFrom"].ToString();
                switch (CalledFrom.ToUpper().Trim())
                {
                    case "NCTRANS":
                        base.ScreenId = NATIONAL_CARGO_TRANSPORTATIONscreenId.INDEX_PAGE;
                        break;
                    case "INTERNTRANS":
                        base.ScreenId = INTERNATIONAL_CARGO_TRANSPORTATIONscreenId.INDEX_PAGE;
                        break;
                    
                }
            }

           
            GetSessionValues();

            cltClientTop.PolicyID = int.Parse(strPolId);
            cltClientTop.CustomerID = int.Parse(strCustomerID);
            cltClientTop.PolicyVersionID = int.Parse(strPolVersionId);
            cltClientTop.ShowHeaderBand = "Policy";
            cltClientTop.Visible = true;

            #region GETTING BASE COLOR FOR ROW SELECTION
            
            string colorScheme = GetColorScheme();
            string colors = "";
            objResourceMgr = new ResourceManager("Cms.Policies.Aspx.Transportation.TransportationIndex", Assembly.GetExecutingAssembly());

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
            BaseDataGrid objBaseDataGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
            try
            {
                //Set the BaseDataGrid Control Properties

                objBaseDataGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL"; ;
                objBaseDataGrid.SelectClause = "COMMODITY_ID,COMMODITY_NUMBER,ISNULL(convert(varchar,DEPARTING_DATE,case when " + ClsCommon.BL_LANG_ID+ "=2 then 103 else 101 end),'') AS DEPARTING_DATE  ,ORIGIN_CITY,DESTINATION_CITY,POL_COMMODITY_INFO.IS_ACTIVE,(FIRST_NAME + ' ' + IsNull(MIDDLE_NAME,'') + ' ' + IsNull(LAST_NAME,'')) as APPLICANT";
                objBaseDataGrid.FromClause = "POL_COMMODITY_INFO INNER JOIN CLT_APPLICANT_LIST CLT WITH(NOLOCK) ON CLT.APPLICANT_ID=POL_COMMODITY_INFO.CO_APPLICANT_ID";

                objBaseDataGrid.WhereClause = " POL_COMMODITY_INFO.CUSTOMER_ID = '" + strCustomerID
                  + "' AND POL_COMMODITY_INFO.POLICY_ID = '" + strPolId
                  + "' AND POL_COMMODITY_INFO.POLICY_VERSION_ID = '" + strPolVersionId
                  + "'";

                objBaseDataGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Commodity #^Commodity^Departing Date^Sum Insured";
                objBaseDataGrid.SearchColumnNames = "COMMODITY_NUMBER^DEPARTING_DATE^ORIGIN_CITY^DESTINATION_CITY^FIRST_NAME ! ' ' ! IsNull(MIDDLE_NAME,'') ! ' ' ! IsNull(LAST_NAME,'')";
                objBaseDataGrid.SearchColumnType = "T^D^T^T^T";
                objBaseDataGrid.OrderByClause = " DEPARTING_DATE desc";
                objBaseDataGrid.DisplayColumnNumbers = "1^2^3^4^5";
                objBaseDataGrid.DisplayColumnNames = "COMMODITY_NUMBER^DEPARTING_DATE^ORIGIN_CITY^DESTINATION_CITY^APPLICANT";
                objBaseDataGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Commodity #^Commodity^Departing Date^Sum Insured";
                objBaseDataGrid.DisplayTextLength = "20^20^20^20^20";
                objBaseDataGrid.DisplayColumnPercent = "15^20^20^20^20";
                objBaseDataGrid.PrimaryColumns = "1";
                objBaseDataGrid.PrimaryColumnsName = "COMMODITY_ID";
                objBaseDataGrid.ColumnTypes = "B^B^B^B^B";
                objBaseDataGrid.AllowDBLClick = "true";
                objBaseDataGrid.FetchColumns = "5^6^8^9^10^12^13";
                objBaseDataGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objBaseDataGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
                objBaseDataGrid.PageSize = int.Parse(GetPageSize());
                objBaseDataGrid.CacheSize = CacheSize;
                objBaseDataGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objBaseDataGrid.HImagePath= System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif"; ;
                objBaseDataGrid.HeaderString = objResourceMgr.GetString("HeaderString");//Voyage Information";
                objBaseDataGrid.SelectClass = colors;
                objBaseDataGrid.RequireQuery = "Y";
                objBaseDataGrid.QueryStringColumns = "COMMODITY_ID";
                objBaseDataGrid.DefaultSearch = "Y";
                objBaseDataGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
                objBaseDataGrid.FilterColumnName = "POL_COMMODITY_INFO.IS_ACTIVE";
                objBaseDataGrid.FilterValue = "Y";
                objBaseDataGrid.ShowExcluded = true;
                GridHolder.Controls.Add(objBaseDataGrid);


                TabCtl.TabURLs = "AddCommodityInfo.aspx?CUSTOMER_ID=" + strCustomerID + "&POL_ID=" + strPolId + "&POL_VERSION_ID=" + strPolVersionId + "&CalledFrom=" + CalledFrom + "&";

                TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");

                SetWorkFlow();
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
 

        }
        private void GetSessionValues()
        {
            strPolId = base.GetPolicyID();
            strPolVersionId = base.GetPolicyVersionID();
            strCustomerID = base.GetCustomerID();
        }
        private void SetWorkFlow()
        {
            myWorkFlow.ScreenID = ScreenId;
            myWorkFlow.AddKeyValue("CUSTOMER_ID", GetCustomerID());
            myWorkFlow.AddKeyValue("POLICY_ID", GetPolicyID());
            myWorkFlow.AddKeyValue("POLICY_VERSION_ID", GetPolicyVersionID());
            myWorkFlow.WorkflowModule = "POL";

        }
    }
}
