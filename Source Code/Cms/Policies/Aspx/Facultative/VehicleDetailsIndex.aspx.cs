/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	26-04-2010
<End Date			: -	
<Description		: - The index page is use for Facultative Liability  (Vehicle information) 
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: -  
<Modified By		: -   
<Purpose			: -   
*******************************************************************************************/
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

namespace Cms.Policies.Aspx.Facultative
{
    public partial class VehicleDetailsIndex : Cms.Policies.policiesbase
    {
        private int CacheSize = 1400;

       
        private string strCustomerID, strPolId, strPolVersionId;
        ResourceManager objResourceMgr = null;

        protected BaseTabControl TabCtl;
        protected ClientTop cltClientTop;
         
        protected WorkFlow myWorkFlow;
        protected PlaceHolder GridHolder;


        protected System.Web.UI.UserControl bottomMenu;
        protected System.Web.UI.HtmlControls.HtmlForm indexForm;
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.UserControl grdSpacer;
         
        protected System.Web.UI.UserControl Gridspacer;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        protected System.Web.UI.WebControls.Label lblTemplate;
        protected System.Web.UI.UserControl pageFooter;
        string CalledFrom;
        /// <summary>
        /// This method is use to load the all required details on the page load 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"].ToString() != "")
                    CalledFrom = Request.QueryString["CalledFrom"].ToString();
            if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"].ToString() == "FLVEHICLEINFO")
            {
                base.ScreenId = FACULTATIVE_LIABILITYscreenId.INDEX_PAGE;
            }
            else if (CalledFrom == "MTOR") {
                base.ScreenId = MOTORscreenId.INDEX_PAGE;
            }
            else
            {
                base.ScreenId = AERONAUTICscreenId.INDEX_PAGE;
            }

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
            objResourceMgr = new ResourceManager("Cms.Policies.Aspx.Facultative.VehicleDetailsIndex", Assembly.GetExecutingAssembly());

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

                objBaseDataGrid.SelectClause = "VEHICLE_ID,CLIENT_ORDER,LICENSE_PLATE,MANUFACTURED_YEAR,Convert(varchar(10), VEHICLE_NUMBER) AS VEHICLE  "
                                               + " ,LOOK_UP.LOOKUP_VALUE_CODE+' - '+ ISNULL(MLMM.LOOKUP_VALUE_DESC,LOOK_UP.LOOKUP_VALUE_DESC) as CATEGORY,CHASSIS,ISNULL(CONVERT(VARCHAR(10),RISK_EFFECTIVE_DATE,case when " + ClsCommon.BL_LANG_ID + "=2 then 103 else 101 end),'') AS RISK_EFFECTIVE_DATE ,ISNULL(CONVERT(VARCHAR(20),RISK_EXPIRE_DATE,case when " + ClsCommon.BL_LANG_ID + "=2 then 103 else 101 end),'') AS RISK_EXPIRE_DATE ,POL_CIVIL_TRANSPORT_VEHICLES.IS_ACTIVE,(FIRST_NAME + ' ' + IsNull(MIDDLE_NAME,'') + ' ' + IsNull(LAST_NAME,'')) as APPLICANT";
                objBaseDataGrid.FromClause = "POL_CIVIL_TRANSPORT_VEHICLES INNER JOIN CLT_APPLICANT_LIST CLT WITH(NOLOCK) ON CLT.APPLICANT_ID=POL_CIVIL_TRANSPORT_VEHICLES.CO_APPLICANT_ID"
                     + " LEFT JOIN MNT_LOOKUP_VALUES LOOK_UP ON LOOK_UP.LOOKUP_UNIQUE_ID =POL_CIVIL_TRANSPORT_VEHICLES.CATEGORY "
                     + "LEFT JOIN MNT_LOOKUP_VALUES_MULTILINGUAL MLMM WITH(NOLOCK)ON MLMM.LOOKUP_UNIQUE_ID=POL_CIVIL_TRANSPORT_VEHICLES.CATEGORY AND MLMM.LANG_ID= "+ ClsCommon.BL_LANG_ID + ""; 

                objBaseDataGrid.WhereClause = " POL_CIVIL_TRANSPORT_VEHICLES.CUSTOMER_ID = '" + strCustomerID
                   + "' AND POL_CIVIL_TRANSPORT_VEHICLES.POLICY_ID = '" + strPolId
                   + "' AND POL_CIVIL_TRANSPORT_VEHICLES.POLICY_VERSION_ID = '" + strPolVersionId
                   + "'";

                objBaseDataGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Client Order #^Licence Plate^Year^Vehicle^Category^Chassis";

                objBaseDataGrid.SearchColumnNames = "CLIENT_ORDER^LICENSE_PLATE^MANUFACTURED_YEAR^LOOK_UP.LOOKUP_VALUE_CODE ! ' ' !ISNULL(MLMM.LOOKUP_VALUE_DESC,LOOK_UP.LOOKUP_VALUE_DESC)^CHASSIS^RISK_EFFECTIVE_DATE^RISK_EXPIRE_DATE^FIRST_NAME ! ' ' ! IsNull(MIDDLE_NAME,'') ! ' ' ! IsNull(LAST_NAME,'')^VEHICLE_NUMBER";

                objBaseDataGrid.SearchColumnType = "T^T^T^T^T^D^D^T^T";
                objBaseDataGrid.OrderByClause = " VEHICLE_ID desc";
                objBaseDataGrid.DisplayColumnNumbers = "1^2^3^4^5^6^7^8^9";

                objBaseDataGrid.DisplayColumnNames = "CLIENT_ORDER^LICENSE_PLATE^MANUFACTURED_YEAR^VEHICLE^CATEGORY^CHASSIS^RISK_EFFECTIVE_DATE^RISK_EXPIRE_DATE^APPLICANT";

                objBaseDataGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Client Order #^Licence Plate^Year^Vehicle^Category^Chassis"
                objBaseDataGrid.DisplayTextLength = "20^20^20^20^20^20^20^20^20";
                objBaseDataGrid.DisplayColumnPercent = "10^10^10^10^10^10^10^10^10";
                objBaseDataGrid.PrimaryColumns = "1";
                objBaseDataGrid.PrimaryColumnsName = "VEHICLE_ID";
                objBaseDataGrid.ColumnTypes = "B^B^B^B^B^B^B^B^B";
                objBaseDataGrid.AllowDBLClick = "true";
                objBaseDataGrid.FetchColumns = "5^6^8^9^10^11^12^13^14";

                objBaseDataGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objBaseDataGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
                objBaseDataGrid.PageSize = int.Parse(GetPageSize());
                objBaseDataGrid.CacheSize = CacheSize;
                objBaseDataGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objBaseDataGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif"; ;
                objBaseDataGrid.HeaderString = objResourceMgr.GetString("HeaderString");// ";
                objBaseDataGrid.SelectClass = colors;
                objBaseDataGrid.RequireQuery = "Y";
                objBaseDataGrid.QueryStringColumns = "VEHICLE_ID";
                objBaseDataGrid.DefaultSearch = "Y";
                objBaseDataGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
                objBaseDataGrid.FilterColumnName = "POL_CIVIL_TRANSPORT_VEHICLES.IS_ACTIVE";
                objBaseDataGrid.FilterValue = "Y";
                objBaseDataGrid.ShowExcluded = true;
                GridHolder.Controls.Add(objBaseDataGrid);


                SetWorkFlow();
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }

            TabCtl.TabURLs = "../Transportation/AddCivilTransportationVehicleInfo.aspx?CalledFrom=" + CalledFrom + "&CUSTOMER_ID=" + strCustomerID
               + "&POL_ID=" + strPolId
               + "&POL_VERSION_ID=" + strPolVersionId + "&";

            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");

            SetWorkFlow();
        }//protected void Page_Load(object sender, EventArgs e)
        private bool CanShow()
        {
            //Checking whether customer id exits in database or not
            if (strPolId == "")
            {
                return false;
            }

            return true;
        }//private bool CanShow()
        private void GetSessionValues()
        {
            strPolId = base.GetPolicyID();
            strPolVersionId = base.GetPolicyVersionID();
            strCustomerID = base.GetCustomerID();
        }//private void GetSessionValues()
        private void SetWorkFlow()
        {
            myWorkFlow.ScreenID = ScreenId;
            myWorkFlow.AddKeyValue("CUSTOMER_ID", GetCustomerID());
            myWorkFlow.AddKeyValue("POLICY_ID", GetPolicyID());
            myWorkFlow.AddKeyValue("POLICY_VERSION_ID", GetPolicyVersionID());
            myWorkFlow.WorkflowModule = "POL";

        }//private void SetWorkFlow()
    }
}
