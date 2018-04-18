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
using System.Resources;
using System.Reflection;
using Cms.BusinessLayer.BlCommon;


namespace Cms.Policies.Aspx
{
    public partial class BeneficiaryIndex : Cms.Policies.policiesbase
    {


        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected string CalledFrom = "";
        ResourceManager objResourceMgr = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"].ToString() != "")
                CalledFrom = Request.QueryString["CalledFrom"].ToString();
            switch (CalledFrom)
            {
                case "CPCACC":
                    base.ScreenId = GROUP_PERSONAL_ACCIDENTscreenId.BENEFICIARY_INFO;
                    break;
                case "GRPLF":
                    base.ScreenId = GROUP_LIFEscreenId.BENEFICIARY_INFO;
                    break;
                    // for itrack 1161
                case "INDPA":
                    base.ScreenId = INDIVIDUAL_PERSONAL_ACCIDENTscreenId.BENEFICIARY_INFO;//"116_1_5";// add beneficiary for Individual Personal Accident
                    break;
                default:
                    base.ScreenId = INDIVIDUAL_PERSONAL_ACCIDENTscreenId.BENEFICIARY_INFO; //"116_1_5";
                    break;
            }


            string langId = GetLanguageID();
            string CustomerId = GetCustomerID();
            string PolicyId = GetPolicyID();
            string PolicyVersionId = GetPolicyVersionID();
            int risk_id = 0;
            if (Request.QueryString["RISK_ID"] != "" && Request.QueryString["RISK_ID"] != null)
                risk_id = Convert.ToInt32(Request.QueryString["RISK_ID"].ToString());


            objResourceMgr = new ResourceManager("Cms.Policies.Aspx.BeneficiaryIndex", Assembly.GetExecutingAssembly());
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
            BaseDataGrid objBaseDataGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
            try
            {
                int PolicyCurrency = 1;
                if (GetPolicyCurrency() == enumCurrencyId.BR)
                    PolicyCurrency = 2;
                else
                    PolicyCurrency = 1;
                //Set the BaseDataGrid Control Properties
                //objWebGrid.SelectClause = " ";
                //objWebGrid.FromClause = "    with(nolock) ";
                string strCALLEDFROM = "";
                strCALLEDFROM = Request["CalledFrom"].ToString();

                objBaseDataGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL"; ;
                objBaseDataGrid.SelectClause = " BENEFICIARY_ID,BENEFICIARY_NAME,dbo.fun_FormatCurrency (ISNULL(BENEFICIARY_SHARE,0)," + langId + ") AS  BENEFICIARY_SHARE,  BENEFICIARY_RELATION ";
                objBaseDataGrid.FromClause = "POL_BENEFICIARY with(nolock)";


                objBaseDataGrid.WhereClause = "CUSTOMER_ID=" + CustomerId + " and POLICY_ID= " + PolicyId + " and POLICY_VERSION_ID=" + PolicyVersionId + " and RISK_ID=" + risk_id + "";

                objBaseDataGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings"); //"Beneficiary Name^Beneficiary Share^Beneficiary Relation"; //
                objBaseDataGrid.SearchColumnNames = "BENEFICIARY_NAME^BENEFICIARY_SHARE^BENEFICIARY_RELATION"; //"isnull(FIRST_NAME,'')! ' ' ! IsNull(MIDDLE_NAME,'') ! ' ' ! IsNull(LAST_NAME,'')^ISNULL(ATCM.DISPLAY_DESCRIPTION,ATC.DISPLAY_DESCRIPTION)^AGENCY_DISPLAY_NAME^BRANCH^COMMISSION_PERCENT^ISNULL(MLVM.LOOKUP_VALUE_DESC,MLV.LOOKUP_VALUE_DESC)";
                objBaseDataGrid.SearchColumnType = "T^T^T";
                //objBaseDataGrid.OrderByClause = "BENEFICIARY_ID desc";

                objBaseDataGrid.DisplayColumnNumbers = "1";
                objBaseDataGrid.DisplayColumnNames = "BENEFICIARY_NAME^BENEFICIARY_SHARE^BENEFICIARY_RELATION"; //"BENEFICIARY_NAME^BENEFICIARY_SHARE^BENEFICIARY_RELATION";
                objBaseDataGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings"); //"Beneficiary Name^Beneficiary Share^Beneficiary Relation"; //
                objBaseDataGrid.DisplayTextLength = "40^20^40";
                objBaseDataGrid.DisplayColumnPercent = "40^20^40";
                objBaseDataGrid.FetchColumns = "1^2^3^4^5";
                objBaseDataGrid.ColumnTypes = "B^B^B";
                objBaseDataGrid.CellHorizontalAlign = "1";

                objBaseDataGrid.PrimaryColumns = "1";
                objBaseDataGrid.PrimaryColumnsName = "BENEFICIARY_ID";
                objBaseDataGrid.AllowDBLClick = "true";
                objBaseDataGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button"; //
                objBaseDataGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");  //"1^Add New^0^addRecord"; //
                objBaseDataGrid.PageSize = int.Parse(GetPageSize());
                objBaseDataGrid.CacheSize = int.Parse(GetCacheSize());
                objBaseDataGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objBaseDataGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif"; ;
                objBaseDataGrid.HeaderString = objResourceMgr.GetString("HeaderString"); //"BENEFICIARY"; //
                objBaseDataGrid.SelectClass = colors;
                objBaseDataGrid.RequireQuery = "Y";
                objBaseDataGrid.QueryStringColumns = "BENEFICIARY_ID";
                objBaseDataGrid.DefaultSearch = "Y";
                objBaseDataGrid.FilterValue = "Y";

                GridHolder.Controls.Add(objBaseDataGrid);
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            string CO_APPLICANT_ID = "";
            if (Request.QueryString["CO_APPLICANT_ID"] != null && Request.QueryString["CO_APPLICANT_ID"] != "")
                CO_APPLICANT_ID = Request.QueryString["CO_APPLICANT_ID"].ToString();

            TabCtl.TabURLs = "AddBeneficiaryDetails.aspx?RISK_ID=" + risk_id + "&CO_APPLICANT_ID=" + CO_APPLICANT_ID + "&CalledFrom=" + CalledFrom + " & BENEFICIARY_ID &";

            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
            TabCtl.TabLength = 150;

        }
    }
}

