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
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Claims;
using Cms.BusinessLayer.BLClaims;
using Cms.CmsWeb.WebControls;
using System.Resources;
using System.Reflection;



namespace Cms.CmsWeb.Maintenance
{
    public partial class XOLIndex : Cms.CmsWeb.cmsbase
    {
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected void Page_Load(object sender, EventArgs e)
        {

          
            #region Setting screen id
            base.ScreenId = "262_10_0";
            #endregion
            GetQueryStringValues();
         

            ClsClaimCoverages ObjClaimCoverages = new ClsClaimCoverages();
            string RI_APPLIES= ObjClaimCoverages.GetRIAppliesFlag();

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




            ResourceManager objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.XOLIndex", Assembly.GetExecutingAssembly());

         #region loading web grid control
         BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
         string rootPath = System.Configuration.ConfigurationManager.AppSettings.Get("CmsPath").Trim();
         try
         {

           

             int BaseCurrency = 1;
             if (GetSYSBaseCurrency() == enumCurrencyId.BR)
                 BaseCurrency = 2;
             else
                 BaseCurrency = 1;

             string sSELECTCLAUSE = "", sFROMCLAUSE = "";
             sSELECTCLAUSE = " * ";
              
             sFROMCLAUSE = "    (              "+
                           "    SELECT         "+
                           "    XOL_ID,        "+
                           "    X.LOB_ID,      "+
                           "    X.IS_ACTIVE,   " +
                           "    ISNULL(N.LOOKUP_VALUE_DESC ,M.LOOKUP_VALUE_DESC) AS RECOVERY_BASE, " +
                           "    ISNULL(LOB_M.LOB_DESC, L.LOB_DESC) AS PRODUCT ," +	                         
                           "    dbo.fun_FormatCurrency (LOSS_DEDUCTION,        " + BaseCurrency + ") AS LOSS_DEDUCTION,        " + 
                           "    dbo.fun_FormatCurrency (AGGREGATE_LIMIT,       " + BaseCurrency + ") AS AGGREGATE_LIMIT,       " +
                           "    dbo.fun_FormatCurrency (MIN_DEPOSIT_PREMIUM,   " + BaseCurrency + ") AS MIN_DEPOSIT_PREMIUM,   " +
                           "    CASE WHEN " + BaseCurrency + "=2 THEN REPLACE(CAST( FLAT_ADJ_RATE AS VARCHAR(50)),'.',',') ELSE CAST( FLAT_ADJ_RATE AS VARCHAR(50)) END AS FLAT_ADJ_RATE , " +
                           "    CASE WHEN " + BaseCurrency + "=2 THEN REPLACE(CAST( REINSTATE_PREMIUM_RATE AS VARCHAR(50)),'.',',') ELSE CAST( REINSTATE_PREMIUM_RATE AS VARCHAR(50)) END AS REINSTATE_PREMIUM_RATE , " +

                 
              
                           //"    dbo.fun_FormatCurrency (REINSTATE_PREMIUM_RATE," + BaseCurrency + ") AS REINSTATE_PREMIUM_RATE," +
                           "    dbo.fun_FormatCurrency (PREMIUM_DISCOUNT,      " + BaseCurrency + ") AS PREMIUM_DISCOUNT,      " +	
                           "    REINSTATE_NUMBER                               " +
                           "    FROM MNT_XOL_INFORMATION X LEFT OUTER JOIN     " +
                           "    MNT_LOB_MASTER L ON X.LOB_ID=L.LOB_ID          " +
                           "    LEFT OUTER JOIN   MNT_LOB_MASTER_MULTILINGUAL LOB_M ON LOB_M.LOB_ID=L.LOB_ID  AND LOB_M.LANG_ID="+ ClsCommon.BL_LANG_ID+              
                           "    LEFT OUTER JOIN MNT_LOOKUP_VALUES M ON M.LOOKUP_UNIQUE_ID=X.RECOVERY_BASE "+
                           "    LEFT OUTER JOIN MNT_LOOKUP_VALUES_MULTILINGUAL N ON N.LOOKUP_UNIQUE_ID=X.RECOVERY_BASE AND N.LANG_ID= " + ClsCommon.BL_LANG_ID +
                           "    WHERE X.CONTRACT_ID = " +hidCONTRACT_ID.Value+ ""+	
                           "    )TEST  ";
             /*************************************************************************/
             ///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
             /************************************************************************/
             //specifying webservice URL
            
            objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
             //specifying columns for select query          
            objWebGrid.SelectClause = sSELECTCLAUSE;// "LITIGATION_ID,CLAIM_ID,JUDICIAL_PROCESS_NO,PLAINTIFF_NAME,PLAINTIFF_CPF,convert(varchar(30),convert(money,isnull( PLAINTIFF_REQUESTED_AMOUNT,0)),1) AS PLAINTIFF_REQUESTED_AMOUNT,convert(varchar(30),convert(money,isnull( DEFEDANT_OFFERED_AMOUNT,0)),1) AS DEFEDANT_OFFERED_AMOUNT";
            // specifying tables for from clause
            objWebGrid.FromClause = sFROMCLAUSE;
            // specifying conditions for where clause
            //objWebGrid.WhereClause = sWHERECLAUSE;
           //  specifying Text to be shown in combo box				            
           // objWebGrid. = "COV_DES"; 
           //  specifying column to be used for combo box           
            objWebGrid.SearchColumnNames = "ISNULL(RECOVERY_BASE,'')^LOSS_DEDUCTION^AGGREGATE_LIMIT^MIN_DEPOSIT_PREMIUM^FLAT_ADJ_RATE";//^LOSS_DEDUCTION^AGGREGATE_LIMIT^MIN_DEPOSIT_PREMIUM^FLAT_ADJ_RATE";
            objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");            
            // search column data type specifying data type of the column to be used for combo box				
            objWebGrid.SearchColumnType = "T^T^T^T^T";
            // specifying column for order by clause
            objWebGrid.OrderByClause = "XOL_ID desc";
            // specifying column numbers of the query to be displyed in grid
            objWebGrid.DisplayColumnNumbers = "6";
             //specifying column names from the query

                objWebGrid.DisplayColumnNames = "PRODUCT^RECOVERY_BASE^LOSS_DEDUCTION^AGGREGATE_LIMIT^MIN_DEPOSIT_PREMIUM^FLAT_ADJ_RATE";
                objWebGrid.CellHorizontalAlign = "2^3^4^5";
                objWebGrid.DisplayTextLength = "25^15^15^15^15^15";
                //specifying width percentage for columns
                objWebGrid.DisplayColumnPercent = "25^15^15^15^15^15";
                objWebGrid.ColumnTypes = "B^B^B^B^B^B";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings"); 

            
             //specifying primary column number
            objWebGrid.PrimaryColumns = "1";         
            objWebGrid.PrimaryColumnsName = "XOL_ID";
             //specifying column type of the data grid
            
             //specifying links pages 
            
             //specifying if double click is allowed or not
            objWebGrid.AllowDBLClick = "true";
             //specifying which columns are to be displayed on first tab
            objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12";
             //specifying message to be shown
            objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage"); 
             //specifying buttons to be displayed on grid
          //  objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");            

             //specifying number of the rows to be shown
            objWebGrid.PageSize = int.Parse(GetPageSize());
             //specifying cache size (use for top clause)
            objWebGrid.CacheSize = int.Parse(GetCacheSize());
             //specifying image path 
            objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
            objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
             //specifying heading
            objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");
            objWebGrid.SelectClass = colors;

            objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
            objWebGrid.FilterColumnName = "IS_ACTIVE";
            objWebGrid.FilterValue = "Y";
            objWebGrid.RequireQuery = "Y";
            objWebGrid.DefaultSearch = "Y";
            

             // column numbers to create query string
            objWebGrid.QueryStringColumns = "XOL_ID^LOB_ID";
             


             // to show completed task we have to give check box
            GridHolder.Controls.Add(objWebGrid);


            TabCtl.TabURLs = "AddXOLInformation.aspx?CONTRACT_ID="+hidCONTRACT_ID.Value+" &";
             TabCtl.TabTitles = objResourceMgr.GetString("TabTitles");
             TabCtl.TabLength = 200;
         }
         catch (Exception ex)
         {
             Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
         }


         #endregion
        }


        private void GetQueryStringValues()
        {
            if (Request.QueryString["CONTRACT"] != null && Request.QueryString["CONTRACT"].ToString() != "")
                hidCONTRACT_ID.Value = Request.QueryString["CONTRACT"].ToString(); 						
            else
                hidCONTRACT_ID.Value = ""; 

           
        }
		
    }
}
