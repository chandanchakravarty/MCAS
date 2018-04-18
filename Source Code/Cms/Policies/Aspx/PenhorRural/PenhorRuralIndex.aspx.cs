/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	16-Dec-2010
<End Date			: -	
<Purpose			: - Use for Penhor Rural product Index page
<Description		: - 
<Review Date		: - 
<Reviewed By		: - 	
Modification History : Add new product index (Fiança Locatícia (Rental Surety) ) 
<Modified Date		: - 28-Jan-2011
<Modified By		: - Pradeep Kushwaha 

*******************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.CmsWeb.WebControls;
using System.Resources;
using System.Reflection;

namespace Cms.Policies.Aspx.PenhorRural
{
    public partial class PenhorRuralIndex : Cms.Policies.policiesbase
    {
        ResourceManager objResourceMgr = null;
        private string strCustomerID, strPolId, strPolVersionId;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
   
        protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
        string CalledFrom;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"].ToString() != "")
                CalledFrom = Request.QueryString["CalledFrom"].ToString();
            switch (CalledFrom)
            {
                case "RLLE": //Penhor Rural Info
                    base.ScreenId = PENHOR_RURALscreenId.INDEX_PAGE;
                    break;
                case "RETSURTY": //Fiança Locatícia (Rental Surety) 
                    base.ScreenId = RENTAL_SURETYscreenId.INDEX_PAGE;
                    break;
                default:
                    base.ScreenId = "526_0";
                    break;

            }

           
            objResourceMgr = new ResourceManager("Cms.Policies.Aspx.PenhorRural.PenhorRuralIndex", Assembly.GetExecutingAssembly());
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

            //BaseDataGrid object created 
            Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
            BaseDataGrid objBaseDataGrid = (BaseDataGrid)c1;
            try
            {
                switch (CalledFrom.ToUpper().Trim())
                {
                    case "RLLE": //Penhor Rural Info

                        //Set the BaseDataGrid Control Properties
                        int LangId = 1;
                        if (GetPolicyCurrency().ToString() == enumCurrencyId.BR)
                            LangId = 2;
                        else
                            LangId = 1;

                        objBaseDataGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                        objBaseDataGrid.SelectClause = "PENHOR_RURAL_ID,ITEM_NUMBER,LOOK_UP.LOOKUP_VALUE_DESC AS CULTIVATION,INSURED_AREA,dbo.fun_FormatCurrency(SUBSIDY_PREMIUM," + LangId + ") SUBSIDY_PREMIUM,PEN_RURAL.IS_ACTIVE";
                        objBaseDataGrid.FromClause = "POL_PENHOR_RURAL_INFO PEN_RURAL WITH(NOLOCK) LEFT JOIN "
                                                    + "MNT_LOOKUP_VALUES LOOK_UP WITH(NOLOCK) ON"
                                                    + " LOOK_UP.LOOKUP_UNIQUE_ID=PEN_RURAL.CULTIVATION";
                        objBaseDataGrid.WhereClause = " CUSTOMER_ID = '" + strCustomerID
                            + "' AND POLICY_ID = '" + strPolId
                            + "' AND POLICY_VERSION_ID = '" + strPolVersionId
                            + "'";

                        objBaseDataGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");
                        objBaseDataGrid.SearchColumnNames = "ITEM_NUMBER^INSURED_AREA";
                        objBaseDataGrid.SearchColumnType = "T^T";
                        objBaseDataGrid.OrderByClause = "PENHOR_RURAL_ID";
                        objBaseDataGrid.DisplayColumnNumbers = "2^3^4^5";
                        objBaseDataGrid.DisplayColumnNames = "ITEM_NUMBER^CULTIVATION^INSURED_AREA^SUBSIDY_PREMIUM";
                        objBaseDataGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");
                        objBaseDataGrid.DisplayTextLength = "10^40^15^15";
                        objBaseDataGrid.DisplayColumnPercent = "10^40^15^15";
                        objBaseDataGrid.ColumnTypes = "B^B^B^B";
                        objBaseDataGrid.PrimaryColumns = "1";
                        objBaseDataGrid.PrimaryColumnsName = "PENHOR_RURAL_ID";
                        objBaseDataGrid.AllowDBLClick = "true";
                        objBaseDataGrid.FetchColumns = "1^2^3^4^5^6";
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
                        objBaseDataGrid.QueryStringColumns = "PENHOR_RURAL_ID";
                        objBaseDataGrid.FilterLabel = objResourceMgr.GetString("FilterLabel"); //"Include Inactive";
                        objBaseDataGrid.FilterValue = "Y";
                        objBaseDataGrid.FilterColumnName = "PEN_RURAL.IS_ACTIVE";

                        GridHolder.Controls.Add(c1);

                        TabCtl.TabURLs = "PenhorRuralInfo.aspx?CUSTOMER_ID=" + strCustomerID + "&POL_ID=" + strPolId + "&POL_VERSION_ID=" + strPolVersionId + "&CalledFrom=RLLE&";



                        TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl"); 
                        break;
                    case "RETSURTY": //Fiança Locatícia (Rental Surety) 
                        //Set the BaseDataGrid Control Properties
                       

                        objBaseDataGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                        objBaseDataGrid.SelectClause = "PEN_RURAL.PENHOR_RURAL_ID,PEN_RURAL.ITEM_NUMBER,PEN_RURAL.IS_ACTIVE";

                        objBaseDataGrid.FromClause = "POL_PENHOR_RURAL_INFO PEN_RURAL WITH(NOLOCK) ";
                        objBaseDataGrid.WhereClause = " CUSTOMER_ID = '" + strCustomerID
                            + "' AND POLICY_ID = '" + strPolId
                            + "' AND POLICY_VERSION_ID = '" + strPolVersionId
                            + "'";

                        objBaseDataGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings1");
                        objBaseDataGrid.SearchColumnNames = "ITEM_NUMBER";
                        objBaseDataGrid.SearchColumnType = "T";
                        objBaseDataGrid.OrderByClause = "PENHOR_RURAL_ID";
                        objBaseDataGrid.DisplayColumnNumbers = "2";
                        objBaseDataGrid.DisplayColumnNames = "ITEM_NUMBER";
                        objBaseDataGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings1");
                        objBaseDataGrid.DisplayTextLength = "10";
                        objBaseDataGrid.DisplayColumnPercent = "100";
                        objBaseDataGrid.ColumnTypes = "B";
                        objBaseDataGrid.PrimaryColumns = "1";
                        objBaseDataGrid.PrimaryColumnsName = "PENHOR_RURAL_ID";
                        objBaseDataGrid.AllowDBLClick = "true";
                        objBaseDataGrid.FetchColumns = "1^2^3";
                        objBaseDataGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                        objBaseDataGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons"); //"1^Add New^0^addRecord";
                        objBaseDataGrid.PageSize = int.Parse(GetPageSize());
                        objBaseDataGrid.CacheSize = int.Parse(GetCacheSize()); ;
                        objBaseDataGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                        objBaseDataGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif"; ;
                        objBaseDataGrid.HeaderString = objResourceMgr.GetString("HeaderString1"); //"Location Details";
                        objBaseDataGrid.SelectClass = colors;
                        objBaseDataGrid.RequireQuery = "Y";
                        objBaseDataGrid.DefaultSearch = "Y";
                        objBaseDataGrid.QueryStringColumns = "PENHOR_RURAL_ID";
                        objBaseDataGrid.FilterLabel = objResourceMgr.GetString("FilterLabel"); //"Include Inactive";
                        objBaseDataGrid.FilterValue = "Y";
                        objBaseDataGrid.FilterColumnName = "PEN_RURAL.IS_ACTIVE";

                        GridHolder.Controls.Add(c1);

                        TabCtl.TabURLs = "PenhorRuralInfo.aspx?CUSTOMER_ID=" + strCustomerID + "&POL_ID=" + strPolId + "&POL_VERSION_ID=" + strPolVersionId + "&CalledFrom=RETSURTY&";

                        TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
                        TabCtl.TabLength = 200;
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
