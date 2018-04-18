/******************************************************************************************
<Author					: -   Sneha
<Start Date				: -	17/08/2011 
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: -
*******************************************************************************************/
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

namespace Cms.Account.Aspx
{
    public partial class InitialLoadIndex : Cms.Account.AccountBase
    {
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected System.Web.UI.WebControls.Label lblTemplate;
        protected System.Web.UI.HtmlControls.HtmlForm indexForm;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        protected System.Web.UI.WebControls.Label lblEntryNo;
        protected System.Web.UI.WebControls.Label lblDate;
        protected System.Web.UI.WebControls.Label Label1;
        protected System.Web.UI.WebControls.Label lblProof;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidJournalInfoXML;
         System.Resources.ResourceManager objResourceMgr;
        private void Page_Load(object sender, System.EventArgs e)
        {

             base.ScreenId = "551_0";
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

            System.Resources.ResourceManager objResourceMgr = new ResourceManager("Cms.Account.Aspx.InitialLoadIndex", Assembly.GetExecutingAssembly());
            BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");

            try
            {
                Control c1 = LoadControl("../../cmsweb/webcontrols/BaseDataGrid.ascx");
                int LANG_ID = ClsCommon.BL_LANG_ID;

                //string sWHERECLAUSE;
                string sSELECTCLAUSE = "[IMPORT_REQUEST_ID]  ,[IMPORT_RECORD_COUNT] ,[IS_PROCESSED]  ," +
                        " ISNULL(Convert(varchar,SUBMITTED_DATE,case when " + LANG_ID + "=2 then 103 else 101 end),'') AS 'SUBMITTED_DATE' , " +
                        " ROW_NUMBER() OVER(ORDER BY [IMPORT_REQUEST_ID] ASC) AS 'SNO', " +
                        " ISNULL(U.USER_FNAME,'')+' '+ISNULL(U.USER_LNAME,'') AS 'SUBMITTED_BY', " +
                        " FAILED_RECORD_COUNT as 'FAILED_RECORD'," +
                          " SUCCEDDED_RECORD_COUNT  as 'SUCCEEDED_RECORD'," +
                          " CASE WHEN ([REQUEST_STATUS]='INPRG' OR [REQUEST_STATUS]='IMPRG' ) AND " + LANG_ID + "=1  THEN 'In Progress' " +
                          "   WHEN ([REQUEST_STATUS]='INPRG'  OR [REQUEST_STATUS]='IMPRG' ) AND " + LANG_ID + "=2  THEN 'Em Andamento' " +
                          "   WHEN [REQUEST_STATUS]='NSTART'   AND " + LANG_ID + "=1  THEN 'Not Started' " +
                          "   WHEN [REQUEST_STATUS]='NSTART'   AND " + LANG_ID + "=2  THEN 'Não Iniciado' " +
                          "   WHEN [REQUEST_STATUS]='FAIL'     AND " + LANG_ID + "=1  THEN 'Failed' " +
                          "   WHEN [REQUEST_STATUS]='FAIL'     AND " + LANG_ID + "=2  THEN 'Falha' " +
                          "   WHEN [REQUEST_STATUS]='COMP'     AND " + LANG_ID + "=1  THEN 'Completed' " +
                          "   WHEN [REQUEST_STATUS]='COMP'     AND " + LANG_ID + "=2  THEN 'Concluída'  " +
                          "   WHEN [REQUEST_STATUS]='INQUEUE'  AND " + LANG_ID + "=2  THEN 'Em fila para processamento' " +
                          "   WHEN [REQUEST_STATUS]='INQUEUE'  AND " + LANG_ID + "=1  THEN 'Queued for processing' " +
                          "   WHEN [REQUEST_STATUS]='WTCHS'    AND " + LANG_ID + "=1  THEN 'Waiting for checksum validation' " +
                          "   WHEN [REQUEST_STATUS]='WTCHS'    AND " + LANG_ID + "=2  THEN 'À espera de validação de checksum' " +

                          "   WHEN [REQUEST_STATUS]='IMQUEUE'  AND " + LANG_ID + "=1  THEN 'Queued for importing' " +
                          "   WHEN [REQUEST_STATUS]='IMQUEUE'  AND " + LANG_ID + "=2  THEN 'Na fila para a importação' " +
                          "   ELSE 'Not Started' " +
                          " END AS 'REQUEST_STATUS'  ,   " +
                      " REQUEST_DESC,REQUEST_STATUS AS REQUEST_STATUS_CODE ";

                objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                //specifying columns for select query          
                objWebGrid.SelectClause = sSELECTCLAUSE;// "LITIGATION_ID,CLAIM_ID,JUDICIAL_PROCESS_NO,PLAINTIFF_NAME,PLAINTIFF_CPF,convert(varchar(30),convert(money,isnull( PLAINTIFF_REQUESTED_AMOUNT,0)),1) AS PLAINTIFF_REQUESTED_AMOUNT,convert(varchar(30),convert(money,isnull( DEFEDANT_OFFERED_AMOUNT,0)),1) AS DEFEDANT_OFFERED_AMOUNT";
                //specifying tables for from clause
                objWebGrid.FromClause = " [MIG_IL_IMPORT_REQUEST]AS M WITH(NOLOCK) LEFT OUTER JOIN MNT_USER_LIST  AS U WITH(NOLOCK) ON M.SUBMITTED_BY=U.USER_ID ";
                //specifying conditions for where clause
                objWebGrid.WhereClause = " [IS_DELETED]='N' AND 	M.[IS_ACTIVE]='Y'";
                //specifying Text to be shown in combo box				            
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings"); //"REQUEST_DESC";
                //specifying column to be used for combo box           
                objWebGrid.SearchColumnNames = "REQUEST_DESC";
                //search column data type specifying data type of the column to be used for combo box				
                objWebGrid.SearchColumnType = "T";
                //specifying column for order by clause
                objWebGrid.OrderByClause = "IMPORT_REQUEST_ID desc";
                //specifying column numbers of the query to be displyed in grid
                objWebGrid.DisplayColumnNumbers = "5";
                //specifying column names from the query				           
                objWebGrid.DisplayColumnNames = "SNO^REQUEST_DESC^IMPORT_RECORD_COUNT^FAILED_RECORD^SUCCEEDED_RECORD^REQUEST_STATUS^SUBMITTED_DATE^SUBMITTED_BY";
                //specifying text to be shown as column headings			

                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings"); //"SNO^REQUEST_DESC^IMPORT_RECORD_COUNT^REQUEST_STATUS^HAS_ERRORS^SUBMITTED_DATE^SUBMITTED_BY";//objResourceMgr.GetString("DisplayColumnHeadings"); 
                //specifying column heading display text length
                objWebGrid.DisplayTextLength = "50^50^50^50^50^50^50^50";
                //specifying width percentage for columns
                objWebGrid.DisplayColumnPercent = "15^15^15^15^15^15^15^15";
                //specifying primary column number
                objWebGrid.PrimaryColumns = "1";
                //specifying primary column name
                objWebGrid.PrimaryColumnsName = "IMPORT_REQUEST_ID";
                //specifying column type of the data grid
                objWebGrid.ColumnTypes = "B^B^B^B^B^B^B^B";
                //specifying links pages 

                //specifying if double click is allowed or not
                objWebGrid.AllowDBLClick = "true";
                //specifying which columns are to be displayed on first tab
                objWebGrid.FetchColumns = objResourceMgr.GetString("FetchColumns"); //"1^2^3^4^5^6^7^8^9";
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
                //objWebGrid.FilterColumnName = "IS_ACTIVE";
                //objWebGrid.FilterValue = "Y";

                // property indiacating whether query string is required or not
                objWebGrid.RequireQuery = "Y";

                // column numbers to create query string
                objWebGrid.QueryStringColumns = "IMPORT_REQUEST_ID^REQUEST_STATUS_CODE";

                objWebGrid.DefaultSearch = "Y";
                // objWebGrid.CellHorizontalAlign = "3^4";

                // to show completed task we have to give check box
                GridHolder.Controls.Add(objWebGrid);


                TabCtl.TabURLs = "AddInitialLoad.aspx?";
                TabCtl.TabTitles = objResourceMgr.GetString("TabTitles");
                TabCtl.TabLength = 200;


            }
            catch
            {
            }
            #endregion
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion
    }
}