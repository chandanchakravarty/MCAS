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
    public partial class PolicyRemunerationIndex : Cms.Policies.policiesbase
    {
       //private int CacheSize = 1400;
        ResourceManager objResourceMgr = null;
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ScreenId = "224_26";

            string CustomerId = Request.QueryString["CUSTOMER_ID"].ToString();
            string PolicyId = Request.QueryString["POLICY_ID"].ToString();
            string PolicyVersionId = Request.QueryString["POLICY_VERSION_ID"].ToString();
            string PolicyLOB = Request.QueryString["POLICY_LOB"].ToString();
            string TransactionType = GetTransaction_Type();
            string langId = GetLanguageID();
            objResourceMgr = new ResourceManager("Cms.Policies.Aspx.PolicyRemunerationIndex", Assembly.GetExecutingAssembly());
            #region GETTING BASE COLOR FOR ROW SELECTION

            string colorScheme = GetColorScheme();
            string colors = "";

            switch (int.Parse(colorScheme))
            {
                case 1:
                    colors  = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();
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
                objBaseDataGrid.SelectClause = "REMUNERATION_ID,(FIRST_NAME + ' ' + IsNull(MIDDLE_NAME,'') + ' ' + IsNull(LAST_NAME,'')) as APPLICANT ,ISNULL(ATCM.DISPLAY_DESCRIPTION,ATC.DISPLAY_DESCRIPTION) AS DISPLAY_DESCRIPTION,BROKER_ID,case when BRANCH = 0 then null else BRANCH end as BRANCH,dbo.fun_FormatCurrency(COMMISSION_PERCENT," + langId + ") COMMISSION_PERCENT,ISNULL(MLVM.LOOKUP_VALUE_DESC,MLV.LOOKUP_VALUE_DESC) AS LEADER,PR.IS_ACTIVE,RTRIM(ISNULL (AGENCY_CODE,'')) +'-'+ ISNULL(AGENCY_DISPLAY_NAME,'') As BROKER_NAME";
                objBaseDataGrid.FromClause = "POL_REMUNERATION PR WITH(NOLOCK) LEFT OUTER JOIN CLT_APPLICANT_LIST CLT WITH(NOLOCK) ON CLT.APPLICANT_ID=PR.CO_APPLICANT_ID AND  CLT.CUSTOMER_ID=PR.CUSTOMER_ID" +
                     " LEFT OUTER JOIN MNT_AGENCY_LIST AGENCY WITH(NOLOCK) ON AGENCY.AGENCY_ID=PR.BROKER_ID"+
                     " LEFT OUTER JOIN POL_CUSTOMER_POLICY_LIST PCPL with(nolock) on PCPL.CUSTOMER_ID=PR.CUSTOMER_ID  and PCPL.POLICY_ID= PR.POLICY_ID and PCPL.POLICY_VERSION_ID = PR.POLICY_VERSION_ID " +
                     " LEFT OUTER JOIN ACT_TRANSACTION_CODES ATC WITH(NOLOCK)  ON PR.COMMISSION_TYPE=ATC.TRAN_ID"+
                     " LEFT OUTER JOIN ACT_TRANSACTION_CODES_MULTILINGUAL ATCM WITH(NOLOCK) ON ATCM.TRAN_ID=PR.COMMISSION_TYPE AND ATCM.LANG_ID=" + langId + " AND ATCM.LANG_ID=2 and ATC.TRAN_ID=ATCM.TRAN_ID"+
                     " LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV WITH(NOLOCK) ON MLV.LOOKUP_UNIQUE_ID=PR.LEADER "+
                     " LEFT OUTER JOIN MNT_LOOKUP_VALUES_MULTILINGUAL MLVM WITH(NOLOCK) ON MLVM.LOOKUP_UNIQUE_ID=PR.LEADER AND MLVM.LANG_ID=" + langId + " AND MLV.LOOKUP_UNIQUE_ID=MLVM.LOOKUP_UNIQUE_ID";
                objBaseDataGrid.WhereClause = "PR.CUSTOMER_ID=" + CustomerId + " and PR.POLICY_ID= " + PolicyId + " and PR.POLICY_VERSION_ID=" + PolicyVersionId + " and POLICY_LOB=" + PolicyLOB + "";
              
               
                if (TransactionType == MASTER_POLICY)
                {
                    objBaseDataGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");
                    objBaseDataGrid.SearchColumnNames = "isnull(FIRST_NAME,'')! ' ' ! IsNull(MIDDLE_NAME,'') ! ' ' ! IsNull(LAST_NAME,'')^ISNULL(ATCM.DISPLAY_DESCRIPTION,ATC.DISPLAY_DESCRIPTION)^isnull(AGENCY_CODE,'')! ' ' ! isnull(AGENCY_DISPLAY_NAME,'')^BRANCH^COMMISSION_PERCENT^ISNULL(MLVM.LOOKUP_VALUE_DESC,MLV.LOOKUP_VALUE_DESC)";
                    objBaseDataGrid.SearchColumnType = "T^T^T^T^T^T";
                    objBaseDataGrid.OrderByClause = "REMUNERATION_ID desc";
                    objBaseDataGrid.DisplayColumnNumbers = "1^2^3^4^5^6";
                    objBaseDataGrid.DisplayColumnNames = "APPLICANT^DISPLAY_DESCRIPTION^BROKER_NAME^BRANCH^COMMISSION_PERCENT^LEADER";
                    objBaseDataGrid.DisplayColumnHeadings =  objResourceMgr.GetString("DisplayColumnHeadings1");
                    objBaseDataGrid.DisplayTextLength = "100^100^100^50^100^50";
                    objBaseDataGrid.DisplayColumnPercent = "10^10^35^5^10^5";
                    objBaseDataGrid.FetchColumns = "1^2^3^4^5^6";
                    objBaseDataGrid.ColumnTypes = "B^B^B^B^B^B";
                    objBaseDataGrid.CellHorizontalAlign = "4";
                }
                else {

                    objBaseDataGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadingsnormal");
                    objBaseDataGrid.SearchColumnNames = "ISNULL(ATCM.DISPLAY_DESCRIPTION,ATC.DISPLAY_DESCRIPTION)^isnull(AGENCY_CODE,'')! ' ' ! isnull(AGENCY_DISPLAY_NAME,'')^BRANCH^COMMISSION_PERCENT^ISNULL(MLVM.LOOKUP_VALUE_DESC,MLV.LOOKUP_VALUE_DESC)";
                    objBaseDataGrid.SearchColumnType = "T^T^T^T^T";
                    objBaseDataGrid.OrderByClause = "REMUNERATION_ID desc";
                    objBaseDataGrid.DisplayColumnNumbers = "1^2^3^4^5";
                    objBaseDataGrid.DisplayColumnNames = "DISPLAY_DESCRIPTION^BROKER_NAME^BRANCH^COMMISSION_PERCENT^LEADER";
                    objBaseDataGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings2");
                    objBaseDataGrid.DisplayTextLength = "100^100^50^100^50";
                    objBaseDataGrid.DisplayColumnPercent = "10^35^5^10^5";
                    objBaseDataGrid.FetchColumns = "1^2^3^4^5";
                    objBaseDataGrid.ColumnTypes = "B^B^B^B^B";
                    objBaseDataGrid.CellHorizontalAlign = "3";
                }
                objBaseDataGrid.PrimaryColumns = "1";
                objBaseDataGrid.PrimaryColumnsName = "REMUNERATION_ID";                
                objBaseDataGrid.AllowDBLClick = "true";               
                objBaseDataGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objBaseDataGrid.ExtraButtons =  objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
                objBaseDataGrid.PageSize = int.Parse(GetPageSize());
                objBaseDataGrid.CacheSize = int.Parse(GetCacheSize());
                objBaseDataGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objBaseDataGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif"; ;
                objBaseDataGrid.HeaderString =  objResourceMgr.GetString("HeaderString");
                objBaseDataGrid.SelectClass = colors;
                objBaseDataGrid.RequireQuery =  "Y";
                objBaseDataGrid.QueryStringColumns = "REMUNERATION_ID";
                objBaseDataGrid.DefaultSearch ="Y";        
                objBaseDataGrid.FilterValue = "Y";

                GridHolder.Controls.Add(objBaseDataGrid);
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            TabCtl.TabURLs = "AddRemunerationDetails.aspx?REMUNERATION_ID &CUSTOMER_ID=" + CustomerId + "&POLICY_ID=" + PolicyId + "&POLICY_VERSION_ID=" + PolicyVersionId + "&";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
            TabCtl.TabLength = 150;
            
        }
    }
    }

