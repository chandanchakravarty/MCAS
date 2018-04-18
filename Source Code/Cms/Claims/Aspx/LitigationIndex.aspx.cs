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
using Cms.CmsWeb.WebControls;
using System.Resources;
using System.Reflection;



namespace Cms.Claims.Aspx
{
    public partial class LitigationIndex : Cms.Claims.ClaimBase
    {
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected void Page_Load(object sender, EventArgs e)
        {

            SetActivityStatus("");
            #region Setting screen id
            base.ScreenId = "306_13_0";
            #endregion
            
            

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
       

         GetQueryStringValues();

         ResourceManager objResourceMgr = new ResourceManager("Cms.Claims.Aspx.LitigationIndex", Assembly.GetExecutingAssembly());

         #region loading web grid control
         BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
         string rootPath = System.Configuration.ConfigurationManager.AppSettings.Get("CmsPath").Trim();
         try
         {

             int PolicyCurrency = 1;
             if (GetPolicyCurrency() == enumCurrencyId.BR)
                 PolicyCurrency = 2;
             else
                 PolicyCurrency = 1;

             string sWHERECLAUSE;
             string sSELECTCLAUSE = "LITIGATION_ID,CLAIM_ID,JUDICIAL_PROCESS_NO,PLAINTIFF_NAME,PLAINTIFF_CPF, "+
             " dbo.fun_FormatCurrency (ISNULL(PLAINTIFF_REQUESTED_AMOUNT,0)," + PolicyCurrency + ") AS  PLAINTIFF_REQUESTED_AMOUNT, " +
             " dbo.fun_FormatCurrency (ISNULL(DEFEDANT_OFFERED_AMOUNT,0)," + PolicyCurrency + ") AS  DEFEDANT_OFFERED_AMOUNT ,IS_ACTIVE";
             //LITIGATION_ID,CLAIM_ID,JUDICIAL_PROCESS_NO,PLAINTIFF_NAME,PLAINTIFF_CPF,
             //convert(varchar(30),convert(money,isnull( PLAINTIFF_REQUESTED_AMOUNT,0)),1) AS PLAINTIFF_REQUESTED_AMOUNT,
             //convert(varchar(30),convert(money,isnull( DEFEDANT_OFFERED_AMOUNT,0)),1) AS DEFEDANT_OFFERED_AMOUNT
             /*************************************************************************/
             ///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
             /************************************************************************/
             //specifying webservice URL
             sWHERECLAUSE = " CLAIM_ID = " + hidCLAIM_ID.Value;
            objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
             //specifying columns for select query          
            objWebGrid.SelectClause = sSELECTCLAUSE;// "LITIGATION_ID,CLAIM_ID,JUDICIAL_PROCESS_NO,PLAINTIFF_NAME,PLAINTIFF_CPF,convert(varchar(30),convert(money,isnull( PLAINTIFF_REQUESTED_AMOUNT,0)),1) AS PLAINTIFF_REQUESTED_AMOUNT,convert(varchar(30),convert(money,isnull( DEFEDANT_OFFERED_AMOUNT,0)),1) AS DEFEDANT_OFFERED_AMOUNT";
             //specifying tables for from clause
            objWebGrid.FromClause = " CLM_LITIGATION_INFORMATION";
             //specifying conditions for where clause
            objWebGrid.WhereClause = sWHERECLAUSE;
             //specifying Text to be shown in combo box				            
            objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings"); 
             //specifying column to be used for combo box           
            objWebGrid.SearchColumnNames = "JUDICIAL_PROCESS_NO^PLAINTIFF_NAME";
             //search column data type specifying data type of the column to be used for combo box				
            objWebGrid.SearchColumnType = "T^T";
             //specifying column for order by clause
            objWebGrid.OrderByClause = "LITIGATION_ID desc";
             //specifying column numbers of the query to be displyed in grid
            objWebGrid.DisplayColumnNumbers = "5";
             //specifying column names from the query				           
            objWebGrid.DisplayColumnNames = "JUDICIAL_PROCESS_NO^PLAINTIFF_NAME^PLAINTIFF_CPF^PLAINTIFF_REQUESTED_AMOUNT^DEFEDANT_OFFERED_AMOUNT";
             //specifying text to be shown as column headings			

            objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings"); 
             //specifying column heading display text length
            objWebGrid.DisplayTextLength = "50^50^50^50^50";
             //specifying width percentage for columns
            objWebGrid.DisplayColumnPercent = "15^15^15^15^15";
             //specifying primary column number
            objWebGrid.PrimaryColumns = "1";
             //specifying primary column name
            objWebGrid.PrimaryColumnsName = "LITIGATION_ID";
             //specifying column type of the data grid
            objWebGrid.ColumnTypes = "B^B^B^B^B";
             //specifying links pages 
            
             //specifying if double click is allowed or not
            objWebGrid.AllowDBLClick = "true";
             //specifying which columns are to be displayed on first tab
            objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8";
             //specifying message to be shown
            objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage"); 
             //specifying buttons to be displayed on grid
            objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons"); 
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
          
             // property indiacating whether query string is required or not
            objWebGrid.RequireQuery = "Y";

             // column numbers to create query string
            objWebGrid.QueryStringColumns = "LITIGATION_ID";
             
            objWebGrid.DefaultSearch = "Y";
            objWebGrid.CellHorizontalAlign = "3^4";

             // to show completed task we have to give check box
            GridHolder.Controls.Add(objWebGrid);


             TabCtl.TabURLs = "AddLitigationInformation.aspx?CLAIM_ID=" + hidCLAIM_ID.Value + "&CUSTOMER_ID=" + hidCUSTOMER_ID.Value + "&POLICY_ID=" + hidPOLICY_ID.Value + "&POLICY_VERSION_ID=" + hidPOLICY_VERSION_ID.Value + "&LOB_ID=" + hidLOB_ID.Value + "&";// TYPE_OF_HOME=" + hidTYPE_OF_HOME.Value + "&";
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
            
      
            if (Request.QueryString["CLAIM_ID"] != null && Request.QueryString["CLAIM_ID"].ToString() != "")
                hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();

            if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
                hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();

            if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString() != "")
                hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].ToString();

            if (Request.QueryString["POLICY_VERSION_ID"] != null && Request.QueryString["POLICY_VERSION_ID"].ToString() != "")
                hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();

            if (Request.QueryString["LOB_ID"] != null && Request.QueryString["LOB_ID"].ToString() != "")
                hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();

            /*if(Request.QueryString["TYPE_OF_HOME"]!=null && Request.QueryString["TYPE_OF_HOME"].ToString()!="" )
                hidTYPE_OF_HOME.Value = Request.QueryString["TYPE_OF_HOME"].ToString();
            else
                hidTYPE_OF_HOME.Value = "0";*/


        }
        private void LoadGrid()
        {
         
        }
    }
}
