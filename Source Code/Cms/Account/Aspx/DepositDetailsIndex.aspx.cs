/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	26-Oct-2010
<End Date			: -	
<Description		: - This index page is use for Deposit details line items 
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: -  
<Modified By		: -   
<Purpose			: -   
*******************************************************************************************/
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
using System.Reflection;
using System.Resources;
using Cms.BusinessLayer.BlCommon;

namespace Cms.Account.Aspx
{
    public partial class DepositDetailsIndex : Cms.Account.AccountBase
    {
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDEPOSIT_ID;
        ResourceManager objResourceMgr = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            String Deposit_id = String.Empty;
            String Deposit_Type = String.Empty;
            String RECEIPT_MODE = String.Empty;//Added for itrack - 1495

            capMessage.Text = "";
            base.ScreenId = "187_0";	//Setting the screen id of screen
           if (Request.QueryString["DEPOSIT_ID"] != null && Request.QueryString["DEPOSIT_ID"].ToString() != "")
           {
               Deposit_id = Request.QueryString["DEPOSIT_ID"].ToString();
               hidDEPOSIT_ID.Value = Request.QueryString["DEPOSIT_ID"].ToString();
               lblDEPOSIT_NUM.Text = Request.QueryString["DEPOSIT_NUM"].ToString();
               Deposit_Type = Request.QueryString["DEPOSIT_TYPE"].ToString();
               RECEIPT_MODE = Request.QueryString["RECEIPT_MODE"].ToString();
           }
           objResourceMgr = new ResourceManager("Cms.Account.Aspx.DepositDetailsIndex", Assembly.GetExecutingAssembly());
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
            int LangId = ClsCommon.BL_LANG_ID;


            Cms.CmsWeb.ClsMessages.SetCustomizedXml(GetLanguageCode());
            String[] tabtitles = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9").Split(',');
            String[] HeaderStringList = objResourceMgr.GetString("HeaderString").Split(',');
            String TabTitleName = String.Empty;
            String HeaderString = String.Empty;
            #region Header tilte
            switch (Deposit_Type)
            {
                case "14831"://Normal
                    TabTitleName = tabtitles[0];
                    HeaderString = HeaderStringList[0];
                    TabCtl.TabLength = 150;
                    break;
                case "14832"://Co-Insurance
                    TabTitleName = tabtitles[1];
                    HeaderString = HeaderStringList[1];
                    TabCtl.TabLength = 200;
                    break;
                case "14916"://Broker Refund
                    TabTitleName = tabtitles[2];
                    HeaderString = HeaderStringList[2];
                    TabCtl.TabLength = 200;
                    break;
                case "14917"://Reinsurance Refund
                    TabTitleName = tabtitles[3];
                    HeaderString = HeaderStringList[3];
                    TabCtl.TabLength = 200;
                    break;
                case "14918"://Ceded CO Refund
                    TabTitleName = tabtitles[4];
                    HeaderString = HeaderStringList[4];
                    TabCtl.TabLength = 200;
                    break;
                default:
                    TabTitleName = tabtitles[0];
                    HeaderString = HeaderStringList[0];
                    TabCtl.TabLength = 150;
                    break;

            }
            #endregion

            try
            {
                switch (Deposit_Type)
                {
                    case "14831"://Normal
                        #region Code for Normal and Co-Insurance
                        objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                        objWebGrid.SelectClause = " ISNULL(BOLETO.BOLETO_ID,0) as BOLETO_ID,ITEMS.CD_LINE_ITEM_ID,"
                                                    + " ISNULL(POL_POLICY.POLICY_NUMBER,'') as POLICY_NUMBER ,"
                                                    + " ENDO.ENDORSEMENT_NO ,"
                                                    + " INSTALL.INSTALLMENT_NO, "
                                                    + " ISNULL(T1.FIRST_NAME,'') + ' ' + ISNULL(T1.MIDDLE_NAME,'') + ' ' + ISNULL(T1.LAST_NAME,'') AS NAME,"
                                                    + " CASE WHEN ITEMS.EXCEPTION_REASON=395 AND ITEMS.IS_EXCEPTION='Y' THEN ITEMS.INCORRECT_OUR_NUMBER ELSE BOLETO.OUR_NUMBER END as BOLETO_NO, "
                                                    + " dbo.fun_FormatCurrency(ITEMS.RISK_PREMIUM," + LangId + ") RISK_PREMIUM, "
                                                    + " dbo.fun_FormatCurrency(ITEMS.FEE," + LangId + ") FEE , "
                                                    + " dbo.fun_FormatCurrency(ITEMS.TAX," + LangId + ") TAX ,"
                                                    + " dbo.fun_FormatCurrency(ITEMS.INTEREST," + LangId + ") INTEREST ,"
                                                    + " dbo.fun_FormatCurrency(ITEMS.LATE_FEE," + LangId + ") LATE_FEE , "
                                                    + " dbo.fun_FormatCurrency(ITEMS.RECEIPT_AMOUNT," + LangId + ") RECEIPT_AMOUNT , "
                                                    + " IS_EXCEPTION  = CASE  WHEN ITEMS.IS_EXCEPTION = 'Y' "
                                                    + " THEN '" + objResourceMgr.GetString("Yes") + "' ELSE '" + objResourceMgr.GetString("No") + "' END, "
                                                    + " IS_APPROVE= CASE  WHEN ISNULL(ITEMS.IS_EXCEPTION,'N') = 'N'  "
                                                    + " THEN '--' WHEN ITEMS.IS_EXCEPTION = 'Y'  "
                                                    + " THEN CASE WHEN  ITEMS.IS_APPROVE='A'	 "
                                                    + " THEN '" + objResourceMgr.GetString("Approve") + "' ELSE CASE WHEN  ITEMS.IS_APPROVE='R' then '" + objResourceMgr.GetString("Refund") + "' END END END ";

                        objWebGrid.FromClause = "ACT_CURRENT_DEPOSIT_LINE_ITEMS ITEMS with(nolock) "
                                                + "left join POL_INSTALLMENT_BOLETO BOLETO with(nolock) ON "
                                                 + "ITEMS.CUSTOMER_ID=BOLETO.CUSTOMER_ID and "
                                                 + "ITEMS.POLICY_ID = BOLETO.POLICY_ID and "
                                                 + "ITEMS.POLICY_VERSION_ID = BOLETO.POLICY_VERSION_ID and "
                                                 + "ITEMS.BOLETO_NO = BOLETO.BOLETO_ID "
                                                 + "left join ACT_POLICY_INSTALLMENT_DETAILS INSTALL with(nolock) on "
                                                 + "BOLETO.INSTALLEMT_ID=INSTALL.ROW_ID"
                                                 + " LEFT JOIN POL_CUSTOMER_POLICY_LIST POL_POLICY WITH(NOLOCK) ON "
                                                 + " POL_POLICY.CUSTOMER_ID=ITEMS.CUSTOMER_ID AND  "
                                                 + " POL_POLICY.POLICY_ID = ITEMS.POLICY_ID AND "
                                                 + " POL_POLICY.POLICY_VERSION_ID = ITEMS.POLICY_VERSION_ID   "
                                                 + " LEFT JOIN POL_POLICY_ENDORSEMENTS ENDO WITH(NOLOCK) ON "
                                                 + " ITEMS.CUSTOMER_ID=ENDO.CUSTOMER_ID AND"
                                                 + " ITEMS.POLICY_ID   =ENDO.POLICY_ID AND"
                                                 + " ITEMS.POLICY_VERSION_ID =ENDO.POLICY_VERSION_ID "
                                                 + " LEFT JOIN CLT_APPLICANT_LIST T1   WITH(NOLOCK) "
                                                 + " ON INSTALL.CO_APPLICANT_ID = T1.APPLICANT_ID ";


                        objWebGrid.WhereClause = "ITEMS.DEPOSIT_ID=" + int.Parse(Deposit_id);


                        objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");
                        objWebGrid.SearchColumnNames = "dbo.fun_FormatCurrency(ITEMS.RECEIPT_AMOUNT," + LangId + ")^T1.FIRST_NAME!''!T1.MIDDLE_NAME!''!LAST_NAME^POL_POLICY.POLICY_NUMBER^ENDO.ENDORSEMENT_NO^INSTALL.INSTALLMENT_NO^CASE WHEN ITEMS.EXCEPTION_REASON=395 AND ITEMS.IS_EXCEPTION='Y' THEN ITEMS.INCORRECT_OUR_NUMBER ELSE INSTALL.BOLETO_NO END";
                        objWebGrid.SearchColumnType = "T^T^T^T^T^T";//^T";
                        objWebGrid.OrderByClause = "CD_LINE_ITEM_ID desc";
                        objWebGrid.DisplayColumnNumbers = "2^3^4^5^6^7^8^9^10^11^12";
                        objWebGrid.DisplayColumnNames = "NAME^POLICY_NUMBER^ENDORSEMENT_NO^INSTALLMENT_NO^BOLETO_NO^RISK_PREMIUM^FEE^TAX^INTEREST^LATE_FEE^RECEIPT_AMOUNT^IS_EXCEPTION^IS_APPROVE";
                        objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");
                        objWebGrid.DisplayTextLength = "17^11^13^10^10^10^10^10^15^5^5^5^5";
                        objWebGrid.DisplayColumnPercent = "15^11^7^7^19^8^8^8^8^8^10^5^5";
                        objWebGrid.ColumnTypes = "B^B^B^B^B^B^B^B^B^B^B^B^B";
                        objWebGrid.PrimaryColumns = "1";
                        objWebGrid.PrimaryColumnsName = "CD_LINE_ITEM_ID";
                        objWebGrid.AllowDBLClick = "true";
                        objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12^13^14";
                        objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                        objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");
                        objWebGrid.PageSize = int.Parse(GetPageSize());
                        objWebGrid.CacheSize = int.Parse(GetCacheSize()); ;
                        objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                        objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif"; ;
                        objWebGrid.HeaderString = HeaderString;
                        objWebGrid.SelectClass = colors;
                        objWebGrid.RequireQuery = "Y";
                        objWebGrid.DefaultSearch = "Y";
                        objWebGrid.QueryStringColumns = "CD_LINE_ITEM_ID^BOLETO_ID";
                        objWebGrid.CellHorizontalAlign = "5^6^7^8^9^10";
                        GridHolder.Controls.Add(c1);
                        #endregion
                        break;
                    case "14832"://Co-Insurance
                        #region Code for Co-Insurance
                        objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                        objWebGrid.SelectClause = " ITEMS.CD_LINE_ITEM_ID,"
                                                +" ISNULL(T1.FIRST_NAME,'') + ' ' + ISNULL(T1.MIDDLE_NAME,'') + ' ' + ISNULL(T1.LAST_NAME,'') AS NAME,"
                                                +" POL_COINS.LEADER_POLICY_NUMBER,"
                                                + "  ISNULL(ENDO.CO_ENDORSEMENT_NO,CASE WHEN " + LangId + " = 2 THEN 'EMI' ELSE 'NBS' END) CO_ENDORSEMENT_NO,"
                                                +" ITEMS.INSTALLMENT_NO,"
                                                +" dbo.fun_FormatCurrency(ITEMS.RISK_PREMIUM,1) RISK_PREMIUM  ";

                        objWebGrid.FromClause = "ACT_CURRENT_DEPOSIT_LINE_ITEMS ITEMS WITH(NOLOCK) "
                                                +" INNER JOIN ACT_POLICY_INSTALLMENT_DETAILS INSTALL WITH(NOLOCK) ON "
                                                +"ITEMS.CUSTOMER_ID=INSTALL.CUSTOMER_ID AND "
                                                +"ITEMS.POLICY_ID=INSTALL.POLICY_ID AND "
                                                +"ITEMS.POLICY_VERSION_ID = INSTALL.POLICY_VERSION_ID AND "
                                                +"ITEMS.INSTALLMENT_NO=INSTALL.INSTALLMENT_NO "
                                                +"LEFT JOIN POL_POLICY_ENDORSEMENTS ENDO WITH(NOLOCK) ON  "
                                                +"ITEMS.CUSTOMER_ID=ENDO.CUSTOMER_ID AND "
                                                +"ITEMS.POLICY_ID   =ENDO.POLICY_ID AND "
                                                +"ITEMS.POLICY_VERSION_ID =ENDO.POLICY_VERSION_ID "
                                                +"LEFT JOIN CLT_APPLICANT_LIST T1   WITH(NOLOCK)  ON  "
                                                +"INSTALL.CO_APPLICANT_ID = T1.APPLICANT_ID "
                                                +"INNER JOIN POL_CO_INSURANCE POL_COINS WITH(NOLOCK) ON "
                                                +"POL_COINS.CUSTOMER_ID = ITEMS.CUSTOMER_ID  AND "
                                                +"POL_COINS.POLICY_ID = ITEMS.POLICY_ID  AND "
                                                +"POL_COINS.POLICY_VERSION_ID = ITEMS.POLICY_VERSION_ID  "
                                                +" and POL_COINS.COINSURANCE_ID=ITEMS.RECEIPT_FROM_ID ";

                        objWebGrid.WhereClause = "ITEMS.DEPOSIT_ID=" + int.Parse(Deposit_id);


                        objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadingsCoInsurance");
                        objWebGrid.SearchColumnNames = "dbo.fun_FormatCurrency(ITEMS.RISK_PREMIUM," + LangId + ")^T1.FIRST_NAME!''!T1.MIDDLE_NAME!''!LAST_NAME^POL_COINS.LEADER_POLICY_NUMBER^ENDO.CO_ENDORSEMENT_NO^ITEMS.INSTALLMENT_NO";
                        objWebGrid.SearchColumnType = "T^T^T^T^T";//^T";
                        objWebGrid.OrderByClause = "CD_LINE_ITEM_ID desc";
                        objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";
                        objWebGrid.DisplayColumnNames = "NAME^LEADER_POLICY_NUMBER^CO_ENDORSEMENT_NO^INSTALLMENT_NO^RISK_PREMIUM";
                        objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadingsCoInsurance");
                        objWebGrid.DisplayTextLength = "25^15^10^10^10";
                        objWebGrid.DisplayColumnPercent = "25^15^10^10^10";
                        objWebGrid.ColumnTypes = "B^B^B^B^B";
                        objWebGrid.PrimaryColumns = "1";
                        objWebGrid.PrimaryColumnsName = "CD_LINE_ITEM_ID";
                        objWebGrid.AllowDBLClick = "true";
                        objWebGrid.FetchColumns = "1^2^3^4^5^6";
                        objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                        objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");
                        objWebGrid.PageSize = int.Parse(GetPageSize());
                        objWebGrid.CacheSize = int.Parse(GetCacheSize()); ;
                        objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                        objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif"; ;
                        objWebGrid.HeaderString = HeaderString;
                        objWebGrid.SelectClass = colors;
                        objWebGrid.RequireQuery = "Y";
                        objWebGrid.DefaultSearch = "Y";
                        objWebGrid.QueryStringColumns = "CD_LINE_ITEM_ID";
                        objWebGrid.CellHorizontalAlign = "4";
                        GridHolder.Controls.Add(c1);
                        #endregion
                        break;
                    case "14916"://Broker Refund
                        objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                        objWebGrid.SelectClause = " ITEMS.CD_LINE_ITEM_ID, "
                                                  +"ISNULL(T1.FIRST_NAME,'') + ' ' + ISNULL(T1.MIDDLE_NAME,'') + ' ' + ISNULL(T1.LAST_NAME,'') AS NAME, "
                                                  +"ITEMS.POLICY_NUMBER , "
                                                  +"ENDO.ENDORSEMENT_NO, "
                                                  +"INSTALL.INSTALLMENT_NO, "
                                                  + "DBO.FUN_FORMATCURRENCY(ITEMS.RECEIPT_AMOUNT," + ClsCommon.BL_LANG_ID + ") RECEIPT_AMOUNT, "
                                                  +"CASE WHEN ITEMS.DEPOSIT_TYPE='14916' THEN AGN_LIST.AGENCY_DISPLAY_NAME ELSE "
                                                  +"CASE WHEN ITEMS.DEPOSIT_TYPE='14917' THEN REIN_COM.REIN_COMAPANY_NAME ELSE "
                                                  + "CASE WHEN ITEMS.DEPOSIT_TYPE='14918' THEN MNT_REIN.REIN_COMAPANY_NAME END  END END RECEIPT_FROM_NAME ";

                        objWebGrid.FromClause = "ACT_CURRENT_DEPOSIT_LINE_ITEMS ITEMS WITH(NOLOCK) "
                                                 + "INNER JOIN ACT_POLICY_INSTALLMENT_DETAILS INSTALL WITH(NOLOCK) ON "
                                                 + "ITEMS.CUSTOMER_ID=INSTALL.CUSTOMER_ID AND "
                                                 + "ITEMS.POLICY_ID=INSTALL.POLICY_ID AND "
                                                 + "ITEMS.POLICY_VERSION_ID=INSTALL.POLICY_VERSION_ID AND "
                                                 + "ITEMS.INSTALLMENT_NO=INSTALL.INSTALLMENT_NO "
                                                 + "INNER JOIN CLT_APPLICANT_LIST T1   WITH(NOLOCK)  ON  "
                                                 + "T1.APPLICANT_ID =INSTALL.CO_APPLICANT_ID "
                                                 + "LEFT JOIN POL_REMUNERATION  REMUN WITH(NOLOCK) ON "
                                                 + "REMUN.CUSTOMER_ID= ITEMS.CUSTOMER_ID AND "
                                                 + "REMUN.POLICY_ID= ITEMS.POLICY_ID AND "
                                                 + "REMUN.POLICY_VERSION_ID= ITEMS.POLICY_VERSION_ID "
                                                 + "AND REMUN.REMUNERATION_ID =ITEMS.RECEIPT_FROM_ID "
                                                 + "LEFT JOIN MNT_AGENCY_LIST AGN_LIST WITH(NOLOCK) ON "
                                                 + "REMUN.BROKER_ID=AGN_LIST.AGENCY_ID "
                                                
                                                 + "LEFT JOIN POL_REINSURANCE_INFO POL_REN WITH(NOLOCK)  ON   "
                                                 + "POL_REN.CUSTOMER_ID= ITEMS.CUSTOMER_ID AND "
                                                 + "POL_REN.POLICY_ID= ITEMS.POLICY_ID AND "
                                                 + "POL_REN.POLICY_VERSION_ID= ITEMS.POLICY_VERSION_ID "
                                                 + "AND POL_REN.REINSURANCE_ID=ITEMS.RECEIPT_FROM_ID "
                                                 + "LEFT JOIN MNT_REIN_COMAPANY_LIST REIN_COM WITH(NOLOCK) ON "
                                                 + "POL_REN.COMPANY_ID=REIN_COM.REIN_COMAPANY_ID "
                                                 + "LEFT JOIN POL_CO_INSURANCE POL_CO WITH(NOLOCK) ON   "
                                                 + "POL_CO.CUSTOMER_ID= ITEMS.CUSTOMER_ID AND "
                                                 + "POL_CO.POLICY_ID= ITEMS.POLICY_ID AND "
                                                 + "POL_CO.POLICY_VERSION_ID= ITEMS.POLICY_VERSION_ID "
                                                 + "AND POL_CO.COINSURANCE_ID=ITEMS.RECEIPT_FROM_ID "
                                                 + "LEFT JOIN MNT_REIN_COMAPANY_LIST MNT_REIN WITH(NOLOCK)   "
                                                 + "on POL_CO.COMPANY_ID=MNT_REIN.REIN_COMAPANY_ID   "
                                                 + " LEFT JOIN POL_POLICY_ENDORSEMENTS ENDO WITH(NOLOCK) ON "
                                                 + " ITEMS.CUSTOMER_ID=ENDO.CUSTOMER_ID AND"
                                                 + " ITEMS.POLICY_ID   =ENDO.POLICY_ID AND"
                                                 + " ITEMS.POLICY_VERSION_ID =ENDO.POLICY_VERSION_ID ";
                        objWebGrid.WhereClause = "ITEMS.DEPOSIT_ID=" + int.Parse(Deposit_id);


                        objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings1");
                        objWebGrid.SearchColumnNames = "ITEMS.POLICY_NUMBER^ENDO.ENDORSEMENT_NO^INSTALL.INSTALLMENT_NO^dbo.fun_FormatCurrency(ITEMS.RECEIPT_AMOUNT," + LangId + ")";
                        objWebGrid.SearchColumnType = "T^T^T^T";//^T";
                        objWebGrid.OrderByClause = "CD_LINE_ITEM_ID desc";
                        objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6";

                        objWebGrid.DisplayColumnNames = "POLICY_NUMBER^NAME^ENDORSEMENT_NO^INSTALLMENT_NO^RECEIPT_AMOUNT^RECEIPT_FROM_NAME";
                        objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings2");
                        objWebGrid.DisplayTextLength = "20^25^3^3^15^40";
                        objWebGrid.DisplayColumnPercent = "20^25^5^10^15^30";
                        objWebGrid.ColumnTypes = "B^B^B^B^B^B";
                        objWebGrid.PrimaryColumns = "1";
                        objWebGrid.PrimaryColumnsName = "CD_LINE_ITEM_ID";
                        objWebGrid.AllowDBLClick = "true";
                        objWebGrid.FetchColumns = "1^2^3^4^5^6^7";
                        objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                        objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");
                        objWebGrid.PageSize = int.Parse(GetPageSize());
                        objWebGrid.CacheSize = int.Parse(GetCacheSize()); ;
                        objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                        objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif"; ;
                        objWebGrid.HeaderString = HeaderString;
                        objWebGrid.SelectClass = colors;
                        objWebGrid.RequireQuery = "Y";
                        objWebGrid.DefaultSearch = "Y";
                        objWebGrid.QueryStringColumns = "CD_LINE_ITEM_ID";
                        objWebGrid.CellHorizontalAlign = "4";
                        GridHolder.Controls.Add(c1);
                       break;
                      case "14917"://Reinsurance Refund
                        objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                        objWebGrid.SelectClause = " ITEMS.CD_LINE_ITEM_ID, "
                                                  +"ISNULL(T1.FIRST_NAME,'') + ' ' + ISNULL(T1.MIDDLE_NAME,'') + ' ' + ISNULL(T1.LAST_NAME,'') AS NAME, "
                                                  +"ITEMS.POLICY_NUMBER , "
                                                  +"ENDO.ENDORSEMENT_NO, "
                                                  +"INSTALL.INSTALLMENT_NO, "
                                                  + "DBO.FUN_FORMATCURRENCY(ITEMS.RECEIPT_AMOUNT," + ClsCommon.BL_LANG_ID + ") RECEIPT_AMOUNT, "
                                                  +"CASE WHEN ITEMS.DEPOSIT_TYPE='14916' THEN AGN_LIST.AGENCY_DISPLAY_NAME ELSE "
                                                  +"CASE WHEN ITEMS.DEPOSIT_TYPE='14917' THEN REIN_COM.REIN_COMAPANY_NAME ELSE "
                                                  + "CASE WHEN ITEMS.DEPOSIT_TYPE='14918' THEN MNT_REIN.REIN_COMAPANY_NAME END  END END RECEIPT_FROM_NAME ";

                        objWebGrid.FromClause = "ACT_CURRENT_DEPOSIT_LINE_ITEMS ITEMS WITH(NOLOCK) "
                                                 + "INNER JOIN ACT_POLICY_INSTALLMENT_DETAILS INSTALL WITH(NOLOCK) ON "
                                                 + "ITEMS.CUSTOMER_ID=INSTALL.CUSTOMER_ID AND "
                                                 + "ITEMS.POLICY_ID=INSTALL.POLICY_ID AND "
                                                 + "ITEMS.POLICY_VERSION_ID=INSTALL.POLICY_VERSION_ID AND "
                                                 + "ITEMS.INSTALLMENT_NO=INSTALL.INSTALLMENT_NO "
                                                 + "INNER JOIN CLT_APPLICANT_LIST T1   WITH(NOLOCK)  ON  "
                                                 + "T1.APPLICANT_ID =INSTALL.CO_APPLICANT_ID "
                                                 + "LEFT JOIN POL_REMUNERATION  REMUN WITH(NOLOCK) ON "
                                                 + "REMUN.CUSTOMER_ID= ITEMS.CUSTOMER_ID AND "
                                                 + "REMUN.POLICY_ID= ITEMS.POLICY_ID AND "
                                                 + "REMUN.POLICY_VERSION_ID= ITEMS.POLICY_VERSION_ID "
                                                 + "AND REMUN.REMUNERATION_ID =ITEMS.RECEIPT_FROM_ID "
                                                 + "LEFT JOIN MNT_AGENCY_LIST AGN_LIST WITH(NOLOCK) ON "
                                                 + "REMUN.BROKER_ID=AGN_LIST.AGENCY_ID "
                                                
                                                 + "LEFT JOIN POL_REINSURANCE_INFO POL_REN WITH(NOLOCK)  ON   "
                                                 + "POL_REN.CUSTOMER_ID= ITEMS.CUSTOMER_ID AND "
                                                 + "POL_REN.POLICY_ID= ITEMS.POLICY_ID AND "
                                                 + "POL_REN.POLICY_VERSION_ID= ITEMS.POLICY_VERSION_ID "
                                                 + "AND POL_REN.REINSURANCE_ID=ITEMS.RECEIPT_FROM_ID "
                                                 + "LEFT JOIN MNT_REIN_COMAPANY_LIST REIN_COM WITH(NOLOCK) ON "
                                                 + "POL_REN.COMPANY_ID=REIN_COM.REIN_COMAPANY_ID "
                                                 + "LEFT JOIN POL_CO_INSURANCE POL_CO WITH(NOLOCK) ON   "
                                                 + "POL_CO.CUSTOMER_ID= ITEMS.CUSTOMER_ID AND "
                                                 + "POL_CO.POLICY_ID= ITEMS.POLICY_ID AND "
                                                 + "POL_CO.POLICY_VERSION_ID= ITEMS.POLICY_VERSION_ID "
                                                 + "AND POL_CO.COINSURANCE_ID=ITEMS.RECEIPT_FROM_ID "
                                                 + "LEFT JOIN MNT_REIN_COMAPANY_LIST MNT_REIN WITH(NOLOCK)   "
                                                 + "on POL_CO.COMPANY_ID=MNT_REIN.REIN_COMAPANY_ID   "
                                                 + " LEFT JOIN POL_POLICY_ENDORSEMENTS ENDO WITH(NOLOCK) ON "
                                                 + " ITEMS.CUSTOMER_ID=ENDO.CUSTOMER_ID AND"
                                                 + " ITEMS.POLICY_ID   =ENDO.POLICY_ID AND"
                                                 + " ITEMS.POLICY_VERSION_ID =ENDO.POLICY_VERSION_ID ";
                        objWebGrid.WhereClause = "ITEMS.DEPOSIT_ID=" + int.Parse(Deposit_id);


                        objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings1");
                        objWebGrid.SearchColumnNames = "ITEMS.POLICY_NUMBER^ENDO.ENDORSEMENT_NO^INSTALL.INSTALLMENT_NO^dbo.fun_FormatCurrency(ITEMS.RECEIPT_AMOUNT," + LangId + ")";
                        objWebGrid.SearchColumnType = "T^T^T^T";//^T";
                        objWebGrid.OrderByClause = "CD_LINE_ITEM_ID desc";
                        objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6";

                        objWebGrid.DisplayColumnNames = "POLICY_NUMBER^NAME^ENDORSEMENT_NO^INSTALLMENT_NO^RECEIPT_AMOUNT^RECEIPT_FROM_NAME";
                        objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings3");
                        objWebGrid.DisplayTextLength = "20^25^3^3^15^40";
                        objWebGrid.DisplayColumnPercent = "20^25^5^10^15^30";
                        objWebGrid.ColumnTypes = "B^B^B^B^B^B";
                        objWebGrid.PrimaryColumns = "1";
                        objWebGrid.PrimaryColumnsName = "CD_LINE_ITEM_ID";
                        objWebGrid.AllowDBLClick = "true";
                        objWebGrid.FetchColumns = "1^2^3^4^5^6^7";
                        objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                        objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");
                        objWebGrid.PageSize = int.Parse(GetPageSize());
                        objWebGrid.CacheSize = int.Parse(GetCacheSize()); ;
                        objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                        objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif"; ;
                        objWebGrid.HeaderString = HeaderString;
                        objWebGrid.SelectClass = colors;
                        objWebGrid.RequireQuery = "Y";
                        objWebGrid.DefaultSearch = "Y";
                        objWebGrid.QueryStringColumns = "CD_LINE_ITEM_ID";
                        objWebGrid.CellHorizontalAlign = "4";
                        GridHolder.Controls.Add(c1);
                       break;
                     case "14918"://Ceded CO Refund
                        objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                        objWebGrid.SelectClause = " ITEMS.CD_LINE_ITEM_ID, "
                                                  +"ISNULL(T1.FIRST_NAME,'') + ' ' + ISNULL(T1.MIDDLE_NAME,'') + ' ' + ISNULL(T1.LAST_NAME,'') AS NAME, "
                                                  +"ITEMS.POLICY_NUMBER , "
                                                  +"ENDO.ENDORSEMENT_NO, "
                                                  +"INSTALL.INSTALLMENT_NO, "
                                                  + "DBO.FUN_FORMATCURRENCY(ITEMS.RECEIPT_AMOUNT," + ClsCommon.BL_LANG_ID + ") RECEIPT_AMOUNT, "
                                                  +"CASE WHEN ITEMS.DEPOSIT_TYPE='14916' THEN AGN_LIST.AGENCY_DISPLAY_NAME ELSE "
                                                  +"CASE WHEN ITEMS.DEPOSIT_TYPE='14917' THEN REIN_COM.REIN_COMAPANY_NAME ELSE "
                                                  + "CASE WHEN ITEMS.DEPOSIT_TYPE='14918' THEN MNT_REIN.REIN_COMAPANY_NAME END  END END RECEIPT_FROM_NAME ";

                        objWebGrid.FromClause = "ACT_CURRENT_DEPOSIT_LINE_ITEMS ITEMS WITH(NOLOCK) "
                                                 + "INNER JOIN ACT_POLICY_INSTALLMENT_DETAILS INSTALL WITH(NOLOCK) ON "
                                                 + "ITEMS.CUSTOMER_ID=INSTALL.CUSTOMER_ID AND "
                                                 + "ITEMS.POLICY_ID=INSTALL.POLICY_ID AND "
                                                 + "ITEMS.POLICY_VERSION_ID=INSTALL.POLICY_VERSION_ID AND "
                                                 + "ITEMS.INSTALLMENT_NO=INSTALL.INSTALLMENT_NO "
                                                 + "INNER JOIN CLT_APPLICANT_LIST T1   WITH(NOLOCK)  ON  "
                                                 + "T1.APPLICANT_ID =INSTALL.CO_APPLICANT_ID "
                                                 + "LEFT JOIN POL_REMUNERATION  REMUN WITH(NOLOCK) ON "
                                                 + "REMUN.CUSTOMER_ID= ITEMS.CUSTOMER_ID AND "
                                                 + "REMUN.POLICY_ID= ITEMS.POLICY_ID AND "
                                                 + "REMUN.POLICY_VERSION_ID= ITEMS.POLICY_VERSION_ID "
                                                 + "AND REMUN.REMUNERATION_ID =ITEMS.RECEIPT_FROM_ID "
                                                 + "LEFT JOIN MNT_AGENCY_LIST AGN_LIST WITH(NOLOCK) ON "
                                                 + "REMUN.BROKER_ID=AGN_LIST.AGENCY_ID "
                                                
                                                 + "LEFT JOIN POL_REINSURANCE_INFO POL_REN WITH(NOLOCK)  ON   "
                                                 + "POL_REN.CUSTOMER_ID= ITEMS.CUSTOMER_ID AND "
                                                 + "POL_REN.POLICY_ID= ITEMS.POLICY_ID AND "
                                                 + "POL_REN.POLICY_VERSION_ID= ITEMS.POLICY_VERSION_ID "
                                                 + "AND POL_REN.REINSURANCE_ID=ITEMS.RECEIPT_FROM_ID "
                                                 + "LEFT JOIN MNT_REIN_COMAPANY_LIST REIN_COM WITH(NOLOCK) ON "
                                                 + "POL_REN.COMPANY_ID=REIN_COM.REIN_COMAPANY_ID "
                                                 + "LEFT JOIN POL_CO_INSURANCE POL_CO WITH(NOLOCK) ON   "
                                                 + "POL_CO.CUSTOMER_ID= ITEMS.CUSTOMER_ID AND "
                                                 + "POL_CO.POLICY_ID= ITEMS.POLICY_ID AND "
                                                 + "POL_CO.POLICY_VERSION_ID= ITEMS.POLICY_VERSION_ID "
                                                 + "AND POL_CO.COINSURANCE_ID=ITEMS.RECEIPT_FROM_ID "
                                                 + "LEFT JOIN MNT_REIN_COMAPANY_LIST MNT_REIN WITH(NOLOCK)   "
                                                 + "on POL_CO.COMPANY_ID=MNT_REIN.REIN_COMAPANY_ID   "
                                                 + " LEFT JOIN POL_POLICY_ENDORSEMENTS ENDO WITH(NOLOCK) ON "
                                                 + " ITEMS.CUSTOMER_ID=ENDO.CUSTOMER_ID AND"
                                                 + " ITEMS.POLICY_ID   =ENDO.POLICY_ID AND"
                                                 + " ITEMS.POLICY_VERSION_ID =ENDO.POLICY_VERSION_ID ";
                        objWebGrid.WhereClause = "ITEMS.DEPOSIT_ID=" + int.Parse(Deposit_id);


                        objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings1");
                        objWebGrid.SearchColumnNames = "ITEMS.POLICY_NUMBER^ENDO.ENDORSEMENT_NO^INSTALL.INSTALLMENT_NO^dbo.fun_FormatCurrency(ITEMS.RECEIPT_AMOUNT," + LangId + ")";
                        objWebGrid.SearchColumnType = "T^T^T^T";//^T";
                        objWebGrid.OrderByClause = "CD_LINE_ITEM_ID desc";
                        objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6";

                        objWebGrid.DisplayColumnNames = "POLICY_NUMBER^NAME^ENDORSEMENT_NO^INSTALLMENT_NO^RECEIPT_AMOUNT^RECEIPT_FROM_NAME";
                        objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings1");
                        objWebGrid.DisplayTextLength = "20^25^3^3^15^40";
                        objWebGrid.DisplayColumnPercent = "20^25^5^10^15^30";
                        objWebGrid.ColumnTypes = "B^B^B^B^B^B";
                        objWebGrid.PrimaryColumns = "1";
                        objWebGrid.PrimaryColumnsName = "CD_LINE_ITEM_ID";
                        objWebGrid.AllowDBLClick = "true";
                        objWebGrid.FetchColumns = "1^2^3^4^5^6^7";
                        objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                        objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");
                        objWebGrid.PageSize = int.Parse(GetPageSize());
                        objWebGrid.CacheSize = int.Parse(GetCacheSize()); ;
                        objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                        objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif"; ;
                        objWebGrid.HeaderString = HeaderString;
                        objWebGrid.SelectClass = colors;
                        objWebGrid.RequireQuery = "Y";
                        objWebGrid.DefaultSearch = "Y";
                        objWebGrid.QueryStringColumns = "CD_LINE_ITEM_ID";
                        objWebGrid.CellHorizontalAlign = "4";
                        GridHolder.Controls.Add(c1);
                       break;
                        
                    default:
                       
                        objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                        objWebGrid.SelectClause = " ITEMS.CD_LINE_ITEM_ID, "
                                                  +"ISNULL(T1.FIRST_NAME,'') + ' ' + ISNULL(T1.MIDDLE_NAME,'') + ' ' + ISNULL(T1.LAST_NAME,'') AS NAME, "
                                                  +"ITEMS.POLICY_NUMBER , "
                                                  +"ENDO.ENDORSEMENT_NO, "
                                                  +"INSTALL.INSTALLMENT_NO, "
                                                  + "DBO.FUN_FORMATCURRENCY(ITEMS.RECEIPT_AMOUNT," + ClsCommon.BL_LANG_ID + ") RECEIPT_AMOUNT, "
                                                  +"CASE WHEN ITEMS.DEPOSIT_TYPE='14916' THEN AGN_LIST.AGENCY_DISPLAY_NAME ELSE "
                                                  +"CASE WHEN ITEMS.DEPOSIT_TYPE='14917' THEN REIN_COM.REIN_COMAPANY_NAME ELSE "
                                                  + "CASE WHEN ITEMS.DEPOSIT_TYPE='14918' THEN MNT_REIN.REIN_COMAPANY_NAME END  END END RECEIPT_FROM_NAME ";

                        objWebGrid.FromClause = "ACT_CURRENT_DEPOSIT_LINE_ITEMS ITEMS WITH(NOLOCK) "
                                                 + "INNER JOIN ACT_POLICY_INSTALLMENT_DETAILS INSTALL WITH(NOLOCK) ON "
                                                 + "ITEMS.CUSTOMER_ID=INSTALL.CUSTOMER_ID AND "
                                                 + "ITEMS.POLICY_ID=INSTALL.POLICY_ID AND "
                                                 + "ITEMS.POLICY_VERSION_ID=INSTALL.POLICY_VERSION_ID AND "
                                                 + "ITEMS.INSTALLMENT_NO=INSTALL.INSTALLMENT_NO "
                                                 + "INNER JOIN CLT_APPLICANT_LIST T1   WITH(NOLOCK)  ON  "
                                                 + "T1.APPLICANT_ID =INSTALL.CO_APPLICANT_ID "
                                                 + "LEFT JOIN POL_REMUNERATION  REMUN WITH(NOLOCK) ON "
                                                 + "REMUN.CUSTOMER_ID= ITEMS.CUSTOMER_ID AND "
                                                 + "REMUN.POLICY_ID= ITEMS.POLICY_ID AND "
                                                 + "REMUN.POLICY_VERSION_ID= ITEMS.POLICY_VERSION_ID "
                                                 + "AND REMUN.REMUNERATION_ID =ITEMS.RECEIPT_FROM_ID "
                                                 + "LEFT JOIN MNT_AGENCY_LIST AGN_LIST WITH(NOLOCK) ON "
                                                 + "REMUN.BROKER_ID=AGN_LIST.AGENCY_ID "
                                                
                                                 + "LEFT JOIN POL_REINSURANCE_INFO POL_REN WITH(NOLOCK)  ON   "
                                                 + "POL_REN.CUSTOMER_ID= ITEMS.CUSTOMER_ID AND "
                                                 + "POL_REN.POLICY_ID= ITEMS.POLICY_ID AND "
                                                 + "POL_REN.POLICY_VERSION_ID= ITEMS.POLICY_VERSION_ID "
                                                 + "AND POL_REN.REINSURANCE_ID=ITEMS.RECEIPT_FROM_ID "
                                                 + "LEFT JOIN MNT_REIN_COMAPANY_LIST REIN_COM WITH(NOLOCK) ON "
                                                 + "POL_REN.COMPANY_ID=REIN_COM.REIN_COMAPANY_ID "
                                                 + "LEFT JOIN POL_CO_INSURANCE POL_CO WITH(NOLOCK) ON   "
                                                 + "POL_CO.CUSTOMER_ID= ITEMS.CUSTOMER_ID AND "
                                                 + "POL_CO.POLICY_ID= ITEMS.POLICY_ID AND "
                                                 + "POL_CO.POLICY_VERSION_ID= ITEMS.POLICY_VERSION_ID "
                                                 + "AND POL_CO.COINSURANCE_ID=ITEMS.RECEIPT_FROM_ID "
                                                 + "LEFT JOIN MNT_REIN_COMAPANY_LIST MNT_REIN WITH(NOLOCK)   "
                                                 + "on POL_CO.COMPANY_ID=MNT_REIN.REIN_COMAPANY_ID   "
                                                 + " LEFT JOIN POL_POLICY_ENDORSEMENTS ENDO WITH(NOLOCK) ON "
                                                 + " ITEMS.CUSTOMER_ID=ENDO.CUSTOMER_ID AND"
                                                 + " ITEMS.POLICY_ID   =ENDO.POLICY_ID AND"
                                                 + " ITEMS.POLICY_VERSION_ID =ENDO.POLICY_VERSION_ID ";
                        objWebGrid.WhereClause = "ITEMS.DEPOSIT_ID=" + int.Parse(Deposit_id);


                        objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings1");
                        objWebGrid.SearchColumnNames = "ITEMS.POLICY_NUMBER^ENDO.ENDORSEMENT_NO^INSTALL.INSTALLMENT_NO^dbo.fun_FormatCurrency(ITEMS.RECEIPT_AMOUNT," + LangId + ")";
                        objWebGrid.SearchColumnType = "T^T^T^T";//^T";
                        objWebGrid.OrderByClause = "CD_LINE_ITEM_ID desc";
                        objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6";

                        objWebGrid.DisplayColumnNames = "POLICY_NUMBER^NAME^ENDORSEMENT_NO^INSTALLMENT_NO^RECEIPT_AMOUNT^RECEIPT_FROM_NAME";
                        objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings4");
                        objWebGrid.DisplayTextLength = "20^25^3^3^15^40";
                        objWebGrid.DisplayColumnPercent = "20^25^5^10^15^30";
                        objWebGrid.ColumnTypes = "B^B^B^B^B^B";
                        objWebGrid.PrimaryColumns = "1";
                        objWebGrid.PrimaryColumnsName = "CD_LINE_ITEM_ID";
                        objWebGrid.AllowDBLClick = "true";
                        objWebGrid.FetchColumns = "1^2^3^4^5^6^7";
                        objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");
                        objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");
                        objWebGrid.PageSize = int.Parse(GetPageSize());
                        objWebGrid.CacheSize = int.Parse(GetCacheSize()); ;
                        objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                        objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif"; ;
                        objWebGrid.HeaderString = HeaderString;
                        objWebGrid.SelectClass = colors;
                        objWebGrid.RequireQuery = "Y";
                        objWebGrid.DefaultSearch = "Y";
                        objWebGrid.QueryStringColumns = "CD_LINE_ITEM_ID";
                        objWebGrid.CellHorizontalAlign = "4";
                        GridHolder.Controls.Add(c1);
                        #endregion
                       break;

                }
              

            }
            catch
            {
            }
         
            //Modified by Pradeep to Add on param RECEIPT_MODE - itrack - 1495
            TabCtl.TabURLs = "AddDepositDetails.aspx?DEPOSIT_ID=" + int.Parse(Deposit_id) + "&DEPOSIT_NUMBER=" + lblDEPOSIT_NUM.Text + "&DEPOSIT_TYPE=" + Deposit_Type + "&RECEIPT_MODE=" + RECEIPT_MODE + "&";
            String[] strTabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl").Split(',');
            if (Deposit_Type.ToString() == "14832")//"14831")//Normal//"14832")//Co-Insurance
                TabCtl.TabTitles = strTabTitles[0].ToString();
            else if (Deposit_Type.ToString() == "14831")
                TabCtl.TabTitles = strTabTitles[1].ToString();
            else if (Deposit_Type.ToString() == "14916")
                TabCtl.TabTitles = strTabTitles[2].ToString();
            else if (Deposit_Type.ToString() == "14917")
                TabCtl.TabTitles = strTabTitles[3].ToString();
            else if (Deposit_Type.ToString() == "14918")
                TabCtl.TabTitles = strTabTitles[4].ToString();

            lblDepositNum.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1435");
            //TabCtl.TabTitles = TabTitleName;
            
        }
    }
}
