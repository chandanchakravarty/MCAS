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
    public partial class VictimIndex : Cms.Claims.ClaimBase
    {
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected void Page_Load(object sender, EventArgs e)
        {

            SetActivityStatus("");
            #region Setting screen id
            base.ScreenId = "306_16_0";
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




            ResourceManager objResourceMgr = new ResourceManager("Cms.Claims.Aspx.VictimIndex", Assembly.GetExecutingAssembly());

         #region loading web grid control
         BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
         string rootPath = System.Configuration.ConfigurationManager.AppSettings.Get("CmsPath").Trim();
         try
         {
             int LangID = int.Parse(GetLanguageID());
             string sSELECTCLAUSE = "", sFROMCLAUSE = "";
             sSELECTCLAUSE = " * ";
            sFROMCLAUSE = "  (SELECT VICTIM_ID ,NAME, ISNULL(N2.LOOKUP_VALUE_DESC,M2.LOOKUP_VALUE_DESC)AS [STATUS], ISNULL(N1.LOOKUP_VALUE_DESC,M1.LOOKUP_VALUE_DESC)AS INJURY_TYPE " +
                              " FROM CLM_VICTIM_INFO C LEFT OUTER JOIN   " +
                              " MNT_LOOKUP_VALUES M1 ON C.INJURY_TYPE=M1.LOOKUP_UNIQUE_ID LEFT OUTER JOIN  " +
                              " MNT_LOOKUP_VALUES M2 ON C.[STATUS]=M2.LOOKUP_UNIQUE_ID    LEFT OUTER JOIN  " +
                              " MNT_LOOKUP_VALUES_MULTILINGUAL N1 ON M1.LOOKUP_UNIQUE_ID =N1.LOOKUP_UNIQUE_ID   AND N1.LANG_ID=" + LangID +" LEFT OUTER JOIN "+
                              " MNT_LOOKUP_VALUES_MULTILINGUAL N2 ON M2.LOOKUP_UNIQUE_ID =N2.LOOKUP_UNIQUE_ID   AND N2.LANG_ID=" + LangID +
                              " WHERE (C.CLAIM_ID=" + hidCLAIM_ID.Value + " AND C.IS_ACTIVE='Y' ))TEST  ";
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
            objWebGrid.SearchColumnNames = "NAME";
            objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings"); 
            // search column data type specifying data type of the column to be used for combo box				
            objWebGrid.SearchColumnType = "T";
            // specifying column for order by clause
            //objWebGrid.OrderByClause = "CLAIM_COV_ID desc";
            // specifying column numbers of the query to be displyed in grid
            objWebGrid.DisplayColumnNumbers = "2^3^4";
             //specifying column names from the query
            
                objWebGrid.DisplayColumnNames = "NAME^STATUS^INJURY_TYPE";
              //  objWebGrid.CellHorizontalAlign = "4^5^6";
                objWebGrid.DisplayTextLength = "40^30^30";
                //specifying width percentage for columns
                objWebGrid.DisplayColumnPercent = "40^30^30";
                objWebGrid.ColumnTypes = "B^B^B";

               

         
             
             //specifying primary column number
            objWebGrid.PrimaryColumns = "1";
             //specifying primary column name
            objWebGrid.PrimaryColumnsName = "VICTIM_ID";
             //specifying column type of the data grid
            
             //specifying links pages 
            
             //specifying if double click is allowed or not
            objWebGrid.AllowDBLClick = "true";
             //specifying which columns are to be displayed on first tab
            objWebGrid.FetchColumns = "1^2^3^4";
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
            if (GetLOBID() == ((int)enumLOB.PAPEACC).ToString().ToString())
            {
                objWebGrid.HeaderString = objResourceMgr.GetString("PassengerHeaderString");
                TabCtl.TabTitles = objResourceMgr.GetString("PassengerTabTitles");
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("PassengerDisplayColumnHeadings"); 
            }
            else
            {
                objWebGrid.HeaderString = objResourceMgr.GetString("VictimHeaderString");
                TabCtl.TabTitles = objResourceMgr.GetString("VictimTabTitles");
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("VictimDisplayColumnHeadings"); 
            }

            objWebGrid.SelectClass = colors;
            

             // property indiacating whether query string is required or not
            objWebGrid.RequireQuery = "Y";

             // column numbers to create query string
            objWebGrid.QueryStringColumns = "VICTIM_ID";
             
            objWebGrid.DefaultSearch = "Y";
           

             // to show completed task we have to give check box
            GridHolder.Controls.Add(objWebGrid);


            TabCtl.TabURLs = "AddVictims.aspx?CLAIM_ID=" + hidCLAIM_ID.Value + "&";
            
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
