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
using Cms.CmsWeb.WebControls;
using Cms.BusinessLayer.BlCommon;
using System.Resources;
using System.Reflection;

namespace Cms.Reports.Aspx
{
    public partial class Deposits : Cms.CmsWeb.cmsbase
    {
        //protected System.Web.UI.WebControls.Label capMessage;
        //protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        //protected System.Web.UI.WebControls.Label lblTemplate;
        //protected System.Web.UI.HtmlControls.HtmlForm indexForm;
        //protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        //protected System.Web.UI.HtmlControls.HtmlInputHidden hidMode;
        //protected System.Web.UI.HtmlControls.HtmlInputHidden hidlocQueryStr;
        //protected System.Web.UI.HtmlControls.HtmlInputHidden hidGridRowClickMsg;
        //protected System.Web.UI.HtmlControls.HtmlInputHidden hidGridRowClickNumber;
        //private string strCalledFrom;
        ResourceManager objResourceMgr = null;
        private void Page_Load(object sender, System.EventArgs e)
        {
            base.ScreenId = "535";
           

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
            objResourceMgr = new ResourceManager("Cms.Reports.Aspx.Deposits", Assembly.GetExecutingAssembly());

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

                objWebGrid.SelectClause = "	ACT_CURRENT_DEPOSITS.DEPOSIT_ID,DEPOSIT_NUMBER,"
                    + " Convert(Varchar,ACT_CURRENT_DEPOSITS.CREATED_DATETIME,CASE WHEN " + ClsCommon.BL_LANG_ID + "=2 THEN 103 ELSE 101 END) +' '+ "
                    + " SUBSTRING(CONVERT(varchar, ACT_CURRENT_DEPOSITS.CREATED_DATETIME, 100), 13, 2) + ':'+ "
                     + " SUBSTRING(CONVERT(varchar, ACT_CURRENT_DEPOSITS.CREATED_DATETIME, 100), 16, 2) + ' '+ "
                    + " SUBSTRING(CONVERT(varchar,ACT_CURRENT_DEPOSITS.CREATED_DATETIME, 100), 18, 2) as CREATEDDATE, "
                    + " Convert(Varchar,ACT_CURRENT_DEPOSITS.DATE_COMMITED,CASE WHEN " + ClsCommon.BL_LANG_ID + "=2 THEN 103 ELSE 101 END) as DATE_COMMITED ,"
                    //					+ "ACT_CURRENT_DEPOSITS.TOTAL_DEPOSIT_AMOUNT,DEPOSIT_TYPE,"
                    + " dbo.fun_FormatCurrency ( IsNull(ACT_CURRENT_DEPOSITS.TOTAL_DEPOSIT_AMOUNT,0)," + ClsCommon.BL_LANG_ID + ") TOTAL_DEPOSIT_AMOUNT ,dbo.fun_GetLookupDesc (DEPOSIT_TYPE, " + ClsCommon.BL_LANG_ID + ") DEPOSIT_TYPE,"
                    //+ " Convert(Varchar,DEPOSIT_TRAN_DATE,CASE WHEN " + ClsCommon.BL_LANG_ID + "=2 THEN 103 ELSE 101 END) As DEPOSIT_TRAN_DATE,"//- iTrack 1323 Notes By Paula Dated 18-July-2011
                    //+ " case when AGL.acc_parent_id is null then AGL.ACC_DESCRIPTION + ' : ' +  isnull(AGL.ACC_DISP_NUMBER,'') else  isnull(AGL2.acc_description,'') + ' - ' + AGL.ACC_DESCRIPTION  + ' : ' + isnull(AGL.ACC_DISP_NUMBER,'')end as ACC_DESCRIPTION"
                    + " BANK_INFO.BANK_NAME +' : ' +BANK_INFO.BANK_NUMBER  AS ACC_DESCRIPTION, "
                    + "  convert(money,IsNull(ACT_CURRENT_DEPOSITS.TOTAL_DEPOSIT_AMOUNT,0)) TOTAL_DEPOSIT_AMOUNT_1"
                    + ",RTL_FILE";

                objWebGrid.FromClause = "ACT_CURRENT_DEPOSITS with(nolock)"
                                        + " LEFT JOIN ACT_GENERAL_LEDGER GL with(nolock) ON ACT_CURRENT_DEPOSITS.FISCAL_ID = GL.FISCAL_ID"
                                        + " INNER JOIN ACT_GL_ACCOUNTS AGL with(nolock) ON AGL.ACCOUNT_ID = ACT_CURRENT_DEPOSITS.ACCOUNT_ID"
                                        + " LEFT OUTER JOIN  ACT_GL_ACCOUNTS AGL2 with(nolock) ON AGL2.ACCOUNT_ID = AGL.ACC_PARENT_ID"
                                        + " LEFT JOIN ACT_BANK_INFORMATION BANK_INFO WITH(NOLOCK) ON BANK_INFO.BANK_ID =ACT_CURRENT_DEPOSITS.ACCOUNT_ID";

                //objWebGrid.WhereClause = "ISNULL(IS_COMMITED,'')<>'Y'";
                //objWebGrid.WhereClause = "ISNULL(IS_COMMITED_TO_SPOOL,'')<>'Y'AND ISNULL(IS_COMMITED,'')<>'Y'";
                objWebGrid.WhereClause = "(ISNULL(IS_COMMITED,'') = 'Y')";

                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");// "Deposit #^Deposit Created Date^Deposit Amount^Deposit Date^Bank Account^Deposit Type";
                objWebGrid.SearchColumnNames = "DEPOSIT_NUMBER^ACT_CURRENT_DEPOSITS.CREATED_DATETIME^ACT_CURRENT_DEPOSITS.DATE_COMMITED^"
                                                + "dbo.fun_FormatCurrency ( IsNull(ACT_CURRENT_DEPOSITS.TOTAL_DEPOSIT_AMOUNT,0)," + ClsCommon.BL_LANG_ID + ")^"
                                                //+ "DEPOSIT_TRAN_DATE^AGL.ACC_DISP_NUMBER" + "DEPOSIT_TYPE";//- iTrack 1323 Notes By Paula Dated 18-July-2011
                                                + " BANK_INFO.BANK_NAME!''!BANK_INFO.BANK_NUMBER";

                objWebGrid.SearchColumnType = "T^D^D^T^T";
                objWebGrid.OrderByClause = "DEPOSIT_NUMBER ASC";
                objWebGrid.DisplayColumnNumbers = "2^3^4^6^7^8^9";
                objWebGrid.DisplayColumnNames = "DEPOSIT_NUMBER^CREATEDDATE^DATE_COMMITED^TOTAL_DEPOSIT_AMOUNT^ACC_DESCRIPTION^DEPOSIT_TYPE^RTL_FILE";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings"); // "Deposit #^Deposit Created Date^Total Deposit Amount^Deposit Date^Bank Account^Deposit Type";
                objWebGrid.DisplayTextLength = "8^15^12^10^15^15^20";
                objWebGrid.DisplayColumnPercent = "8^15^10^13^15^15^20";
                objWebGrid.PrimaryColumns = "1";
                objWebGrid.PrimaryColumnsName = "ACT_CURRENT_DEPOSITS.DEPOSIT_ID";
                //Modified by Asfa(18-June-2008) - iTrack #3906(Note: 1)
                //objWebGrid.ColumnTypes = "B^B^B^B^B";
                objWebGrid.ColumnTypes = "B^B^B^N^B^B^B";
                objWebGrid.AllowDBLClick = "true";
                objWebGrid.FetchColumns = "1^2^3^4^6^7^8^9";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage"); //"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
               // objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons"); //"1^Add New^0^addRecord";
                objWebGrid.PageSize = 20;
                objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");
                objWebGrid.SelectClass = colors;
                objWebGrid.RequireQuery = "Y";
                objWebGrid.DefaultSearch = "Y";
                objWebGrid.QueryStringColumns = "DEPOSIT_ID";
                objWebGrid.CellHorizontalAlign = "3";
                //Adding to controls to gridholder
                GridHolder.Controls.Add(c1);
            }
            catch
            {
            }
            #endregion

            //TabCtl.TabURLs = "DepositReceipt.aspx?&";
            //TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1434");
          


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
