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



namespace Cms.Claims.Aspx
{
    public partial class ClaimCoveragesIndex : Cms.Claims.ClaimBase
    {
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected void Page_Load(object sender, EventArgs e)
        {

            SetActivityStatus("");
            #region Setting screen id
            base.ScreenId = "306_14_0";
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
       

         

         ResourceManager objResourceMgr = new ResourceManager("Cms.Claims.Aspx.ClaimCoveragesIndex", Assembly.GetExecutingAssembly());

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

             int LangID = int.Parse(GetLanguageID());
             string sSELECTCLAUSE = "", sFROMCLAUSE = "";
             sSELECTCLAUSE = " * ";

             sFROMCLAUSE = " (SELECT C.CLAIM_COV_ID, M.LOB_ID AS PRODUCT_ID, C.COVERAGE_CODE_ID,C.IS_RISK_COVERAGE,ISNULL(M.COV_DES,N.COV_DES) AS COV_DES," +
                              " (CASE WHEN C.IS_ACTIVE='Y' THEN dbo.fun_GetMessage(282," + LangID + ") ELSE dbo.fun_GetMessage(283," + LangID + ")END) AS IS_ACTIVE, " +
                              " (CASE WHEN C.RI_APPLIES='Y' THEN dbo.fun_GetMessage(284," + LangID + ") ELSE dbo.fun_GetMessage(285," + LangID + ")END) AS RI_APPLIES ," +
                              " (CASE WHEN C.LIMIT_OVERRIDE='Y' THEN dbo.fun_GetMessage(284," + LangID + ") ELSE dbo.fun_GetMessage(285," + LangID + ")END) AS LIMIT_OVERRIDE ," +
                              " dbo.fun_FormatCurrency (ISNULL(C.LIMIT_1,0)," + PolicyCurrency + ") AS  LIMIT_1, " +
                              " dbo.fun_FormatCurrency (ISNULL(C.MINIMUM_DEDUCTIBLE,0)," + PolicyCurrency + ") AS  DEDUCTIBLE_1, " +
                              " dbo.fun_FormatCurrency (ISNULL(C.POLICY_LIMIT,0)," + PolicyCurrency + ") AS  POLICY_LIMIT ," +
                              " V.NAME AS VICTIM"+                             
                              " FROM CLM_PRODUCT_COVERAGES C LEFT OUTER JOIN " +
                              " MNT_COVERAGE M ON COVERAGE_CODE_ID=M.COV_ID    LEFT OUTER JOIN " +
                              " MNT_COVERAGE_MULTILINGUAL N ON COVERAGE_CODE_ID=N.COV_ID  AND N.LANG_ID=" + LangID +
                              " LEFT OUTER JOIN CLM_VICTIM_INFO V ON C.CLAIM_ID=V.CLAIM_ID AND C.VICTIM_ID=V.VICTIM_ID "+
                              " WHERE (C.CLAIM_ID=" + hidCLAIM_ID.Value + " AND IS_RISK_COVERAGE='Y' ))TEST  ";
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
            objWebGrid.SearchColumnNames = "COV_DES";
            objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings"); 
            // search column data type specifying data type of the column to be used for combo box				
            objWebGrid.SearchColumnType = "T";
            // specifying column for order by clause
            objWebGrid.OrderByClause = "CLAIM_COV_ID desc";
            // specifying column numbers of the query to be displyed in grid
            objWebGrid.DisplayColumnNumbers = "8";
             //specifying column names from the query
            if (RI_APPLIES == "1" && hidIS_VICTIM_CLAIM.Value == "10963")
            {
                objWebGrid.DisplayColumnNames = "COV_DES^VICTIM^RI_APPLIES^IS_ACTIVE^LIMIT_OVERRIDE^LIMIT_1^DEDUCTIBLE_1^POLICY_LIMIT";
                objWebGrid.CellHorizontalAlign = "4^5^6";
                objWebGrid.DisplayTextLength = "30^10^10^5^10^10^10^15";
                //specifying width percentage for columns
                objWebGrid.DisplayColumnPercent = "30^10^10^5^10^10^10^15";
                objWebGrid.ColumnTypes = "B^B^B^B^B^B^B^B";

                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings4"); 

            }
            else if (RI_APPLIES == "1")
            {
                objWebGrid.DisplayColumnNames = "COV_DES^RI_APPLIES^IS_ACTIVE^LIMIT_OVERRIDE^LIMIT_1^DEDUCTIBLE_1^POLICY_LIMIT";
                objWebGrid.CellHorizontalAlign = "4^5^6";
                objWebGrid.DisplayTextLength = "40^10^5^10^10^10^15";
                //specifying width percentage for columns
                objWebGrid.DisplayColumnPercent = "40^10^5^10^10^10^15";
                objWebGrid.ColumnTypes = "B^B^B^B^B^B^B";

                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings1"); 
            }
            else if (hidIS_VICTIM_CLAIM.Value == "10963")
            {

                objWebGrid.DisplayColumnNames = "COV_DES^VICTIM^IS_ACTIVE^LIMIT_OVERRIDE^LIMIT_1^DEDUCTIBLE_1^POLICY_LIMIT";
                objWebGrid.CellHorizontalAlign = "4^5^6";
                objWebGrid.DisplayTextLength = "35^15^5^10^10^10^15";
                //specifying width percentage for columns
                objWebGrid.DisplayColumnPercent = "35^15^10^10^13^10^15";
                objWebGrid.ColumnTypes = "B^B^B^B^B^B^B";

                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings3"); 

            }
            else 
            {

                objWebGrid.DisplayColumnNames = "COV_DES^IS_ACTIVE^LIMIT_OVERRIDE^LIMIT_1^DEDUCTIBLE_1^POLICY_LIMIT";
                objWebGrid.CellHorizontalAlign = "3^4^5";
                objWebGrid.DisplayTextLength = "30^10^10^15^15^20";
                //specifying width percentage for columns
                objWebGrid.DisplayColumnPercent = "30^10^10^15^15^20";
                objWebGrid.ColumnTypes = "B^B^B^B^B^B";


                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings2");

            }
             
             //specifying primary column number
            objWebGrid.PrimaryColumns = "1";
             //specifying primary column name
            objWebGrid.PrimaryColumnsName = "CLAIM_COV_ID";
             //specifying column type of the data grid
            
             //specifying links pages 
            
             //specifying if double click is allowed or not
            objWebGrid.AllowDBLClick = "true";
             //specifying which columns are to be displayed on first tab
            objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11";
             //specifying message to be shown
            objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage"); 
             //specifying buttons to be displayed on grid
            if (hidLITIGATION_FILE.Value == "10963" || hidCO_INSURANCE_TYPE.Value == "1")
            {
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");
                hidALLOW_ADD_COVERAGE.Value = "Y";

            }

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
            

             // property indiacating whether query string is required or not
            objWebGrid.RequireQuery = "Y";

             // column numbers to create query string
            objWebGrid.QueryStringColumns = "CLAIM_COV_ID^PRODUCT_ID";
             
            objWebGrid.DefaultSearch = "Y";
           

             // to show completed task we have to give check box
            GridHolder.Controls.Add(objWebGrid);


            TabCtl.TabURLs = "AddClaimCoverages.aspx?CUSTOMER_ID=" + hidCUSTOMER_ID.Value + "&POLICY_ID=" + hidPOLICY_ID.Value + "&POLICY_VERSION_ID=" + hidPOLICY_VERSION_ID.Value + "&CLAIM_ID=" + hidCLAIM_ID.Value + "&ALLOW_ADD_COVERAGE=" + hidALLOW_ADD_COVERAGE.Value + "&ACC_COI_FLG=" + hidACC_COI_FLG.Value + "&";
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

            if (Request.QueryString["LITIGATION_FILE"] != null && Request.QueryString["LITIGATION_FILE"].ToString() != "")
                hidLITIGATION_FILE.Value = Request.QueryString["LITIGATION_FILE"].ToString();

            if (Request.QueryString["CO_INSURANCE_TYPE"] != null && Request.QueryString["CO_INSURANCE_TYPE"].ToString() != "")
                hidCO_INSURANCE_TYPE.Value = Request.QueryString["CO_INSURANCE_TYPE"].ToString();

            if (Request.QueryString["IS_VICTIM_CLAIM"] != null && Request.QueryString["IS_VICTIM_CLAIM"].ToString() != "")
                hidIS_VICTIM_CLAIM.Value = Request.QueryString["IS_VICTIM_CLAIM"].ToString();

            if (Request.QueryString["ACC_COI_FLG"] != null && Request.QueryString["ACC_COI_FLG"].ToString() != "")
                hidACC_COI_FLG.Value = Request.QueryString["ACC_COI_FLG"].ToString();
            
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
