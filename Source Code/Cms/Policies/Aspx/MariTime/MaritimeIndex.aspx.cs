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

namespace Cms.Policies.Aspx.MariTime
{
    public partial class MaritimeIndex : Cms.Policies.policiesbase
    {
        #region Variables
        //private int CacheSize = 1400;
        private string strCustomerID, strPolId, strPolVersionId;
        protected string strCalledFrom = "";
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
        protected System.Web.UI.WebControls.Label capMessage;
        protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
        //protected System.Web.UI.HtmlControls.HtmlForm indexForm;
        //protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        ResourceManager objResourceMgr = null;
        #endregion
        

        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "455";
            objResourceMgr = new ResourceManager("Cms.Policies.Aspx.MariTime.MaritimeIndex", Assembly.GetExecutingAssembly());
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

            try
            {
                //Setting web grid control properties
                objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

                objWebGrid.SelectClause = "POL_MARITIME.MARITIME_ID,VESSEL_NUMBER,MNT_VESSEL_MASTER.VESSEL_NAME NAME_OF_VESSEL,POL_MARITIME.TYPE_OF_VESSEL TYPE_OF_VESSEL,MANUFACTURER,MANUFACTURE_YEAR,POL_MARITIME.IS_ACTIVE IS_ACTIVE";

                objWebGrid.FromClause = "POL_MARITIME INNER JOIN MNT_VESSEL_MASTER ON POL_MARITIME.NAME_OF_VESSEL = MNT_VESSEL_MASTER.VESSEL_ID";

                objWebGrid.WhereClause = " POL_MARITIME.CUSTOMER_ID = '" + strCustomerID
                    + "' AND POL_MARITIME.POLICY_ID = '" + strPolId
                    + "' AND POL_MARITIME.POLICY_VERSION_ID = '" + strPolVersionId
                    + "'";


                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings"); //"Vessel#^Name Of Vessel^Type Of Vessel^Manufacturer^Manufacturer Year";
                objWebGrid.SearchColumnNames = "VESSEL_NUMBER^NAME_OF_VESSEL^MANUFACTURE_YEAR^TYPE_OF_VESSEL";
                objWebGrid.SearchColumnType = "T^T^T^T";

                objWebGrid.OrderByClause = "MNT_VESSEL_MASTER.VESSEL_NAME ASC";

                objWebGrid.DisplayColumnNumbers = "1^2^3^4";
                objWebGrid.DisplayColumnNames = "VESSEL_NUMBER^NAME_OF_VESSEL^MANUFACTURE_YEAR^TYPE_OF_VESSEL";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");// "Vessel#^Name Of Vessel^Type Of Vessel^Manufacturer^Manufacturer Year";

                objWebGrid.DisplayTextLength = "10^40^30^20";
                objWebGrid.DisplayColumnPercent = "10^40^30^20";
                objWebGrid.ColumnTypes = "B^B^B^B";

                objWebGrid.PrimaryColumns = "1";
                objWebGrid.PrimaryColumnsName = "POL_MARITIME.MARITIME_ID";

                objWebGrid.AllowDBLClick = "true";
                objWebGrid.FetchColumns = "1^2^3^4";

                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");

                string AgencyId = GetSystemId();
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
                objWebGrid.QueryStringColumns = "MARITIME_ID";
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel"); //"Include Inactive";
                objWebGrid.FilterValue = "Y";
                objWebGrid.FilterColumnName = "POL_MARITIME.IS_ACTIVE";
                //Adding to controls to gridholder
                GridHolder.Controls.Add(c1);
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            #endregion

            TabCtl.TabURLs = "AddMeriTimeInfo.aspx?CUSTOMER_ID=" + strCustomerID
                + "&POL_ID=" + strPolId
                + "&POL_VERSION_ID=" + strPolVersionId + "&"; 

            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");

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
