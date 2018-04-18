/******************************************************************************************
<Author				: -   Ajit Singh Chahal
<Start Date				: -	4/7/2005 12:18:00 PM
<End Date				: -	
<Description				: - 	Code Behind for Vendor Index.
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
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
using System.Xml;
using System.IO;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb;
using System.Reflection;
using System.Resources;

namespace Cms.CmsWeb.Maintenance
{
    /// <summary>
    /// Class to server as code behind logic for Vendor.
    /// </summary>
    public partial class PortMasterIndex : Cms.CmsWeb.cmsbase
    {
       
        protected System.Web.UI.WebControls.Panel pnlGrid;
        protected System.Web.UI.WebControls.Table tblReport;
        protected System.Web.UI.WebControls.Panel pnlReport;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected System.Web.UI.WebControls.Label lblTemplate;
        protected System.Web.UI.HtmlControls.HtmlForm indexForm;
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAddNew;
        ResourceManager objResourceMgr = null;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;

        private void Page_Load(object sender, System.EventArgs e)
        {
            base.ScreenId = "570";
            SetCultureThread(GetLanguageCode());
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.PortMasterIndex", Assembly.GetExecutingAssembly());
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

            //if (Request["AddNew"] != null && Request["AddNew"].ToString() != "")
            //    hidAddNew.Value = Request["AddNew"].ToString();

            #region loading web grid control
            BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
            try
            {
                //Setting web grid control properties
                objWebGrid.WebServiceURL = "../webservices/BaseDataGridWS.asmx?WSDL";
                
                objWebGrid.SelectClause = "PORT_CODE,ISO_CODE,PORT_TYPE,COUNTRY,ADDITIONAL_WAR_RATE,FROM_DATE,TO_DATE,SETTLEMENT_AGENT_CODE,SETTLING_AGENT_NAME,IS_ACTIVE";
                objWebGrid.FromClause = "MNT_PORT_MASTER";

                objWebGrid.SearchColumnHeadings = "Port Code^Country/Port^Additional War Rate";
                objWebGrid.SearchColumnNames = "PORT_CODE^COUNTRY^ADDITIONAL_WAR_RATE";
                objWebGrid.SearchColumnType = "T^T^T";
                objWebGrid.OrderByClause = "PORT_CODE asc";

                //				objWebGrid.DisplayColumnNumbers = "2^3^22^23^7^24^10^11^13";
                //				objWebGrid.DisplayColumnNames = "VENDOR_CODE^COMPANY_NAME^Name^Address^CHK_MAIL_CITY^CHK_MAIL_STATE^CHK_MAIL_ZIP^VENDOR_PHONE^VENDOR_FAX";
                //				objWebGrid.DisplayColumnHeadings = "Vendor Code^Company Name^Name^Address^City^State^Zip^Phone^Fax";
                //				objWebGrid.DisplayTextLength = "120^70^100^200^100^100^50^100^90";
                //				objWebGrid.DisplayColumnPercent = "11^8^11^20^10^10^10^11^9";

                objWebGrid.DisplayColumnNumbers = "1^4^5";
                objWebGrid.DisplayColumnNames = "PORT_CODE^COUNTRY^ADDITIONAL_WAR_RATE";
                //objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Vendor Code^Company Name^Address^City^State^Zip^Phone^Fax^SUSEP Number";
                objWebGrid.DisplayColumnHeadings = "Port Code^Country/Port^Additional War Rate";
                objWebGrid.DisplayTextLength = "25^50^25";
                objWebGrid.DisplayColumnPercent = "25^50^25";

                objWebGrid.PrimaryColumns = "1";
                objWebGrid.PrimaryColumnsName = "PORT_CODE";
                objWebGrid.ColumnTypes = "B^B^B";
                objWebGrid.AllowDBLClick = "true";
                objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
                objWebGrid.PageSize = int.Parse(GetPageSize());
                objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = "Port Master";
                //objWebGrid.FilterColumnName = "";
                //objWebGrid.FilterValue = "";
                objWebGrid.SelectClass = colors;
                objWebGrid.RequireQuery = "Y";
                objWebGrid.DefaultSearch = "Y";
                objWebGrid.QueryStringColumns = "PORT_CODE";

                //specifying text to be shown for filter checkbox
                objWebGrid.FilterLabel = "Include Inactive";
                //specifying column to be used for filtering
                objWebGrid.FilterColumnName = "IS_ACTIVE";
                //value of filtering record
                objWebGrid.FilterValue = "Y";
                objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
                //Adding to controls to gridholder
                GridHolder.Controls.Add(objWebGrid);
            }
            catch (Exception objExcep)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
            }
            #endregion
            TabCtl.TabURLs = "AddPortMaster.aspx??&";
            TabCtl.TabTitles = "Port Master";
            TabCtl.TabLength = 150;

            #region Window Grid
            /*
			litTextGrid.Text = "<OBJECT id=\"gridObject\" classid=\"" + GetWindowsGridUrl() + "\" VIEWASTEXT>"
				+ "<PARAM NAME=\"SelectClause\" VALUE=\"t1.VENDOR_ID^t1.VENDOR_CODE^t1.VENDOR_FNAME^t1.VENDOR_LNAME^t1.VENDOR_ADD1^t1.VENDOR_ADD2^t1.VENDOR_CITY^t1.VENDOR_COUNTRY^t1.VENDOR_STATE^t1.VENDOR_ZIP^t1.VENDOR_PHONE^t1.VENDOR_EXT^t1.VENDOR_FAX^t1.VENDOR_MOBILE^t1.VENDOR_EMAIL^t1.VENDOR_SALUTATION^t1.VENDOR_FEDERAL_NUM^t1.VENDOR_NOTE^t1.VENDOR_ACC_NUMBER^t1.IS_ACTIVE^t1.MODIFIED_BY^t1.VENDOR_FNAME+ ' '+t1.VENDOR_LNAME as Name^t1.VENDOR_ADD1+' '+t1.VENDOR_ADD2 as Address^t2.STATE_NAME\">"
				//Name22,Address23,statename24,
				+ "<PARAM NAME=\"FromClause\" VALUE=\"MNT_VENDOR_LIST t1,MNT_COUNTRY_STATE_LIST t2\">"
				+ "<PARAM NAME=\"WhereClause\" VALUE=\"t1.VENDOR_STATE=t2.STATE_ID\">"
				+ "<PARAM NAME=\"GroupClause\" VALUE=\"\">"
				+ "<PARAM NAME=\"SearchColumnNames\" VALUE=\"t1.VENDOR_CODE^t1.VENDOR_FNAME^t1.VENDOR_LNAME\">"
				+ "<PARAM NAME=\"SearchColumnHeadings\" VALUE=\"Vendor Code^First Name^Last Name\">"
				+ "<PARAM NAME=\"SearchColumnTypes\" VALUE=\"S^S^S\">"
				+ "<PARAM NAME=\"DisplayColumnNumbers\" VALUE=\"2^22^23^7^24^10^11^13\">"
				+ "<PARAM NAME=\"DisplayColumnHeadings\" VALUE=\"Vendor Code^Name^Address^City^State^Zip^Phone^Fax\">"
				+ "<PARAM NAME=\"DisplayTextLength\" VALUE=\"120^150^200^100^100^50^120^90\">"
				+ "<PARAM NAME=\"PageSize\" VALUE=\"" + GetPageSize() + "\">"
				+ "<PARAM NAME=\"ColumnTypes\" VALUE=\"S^S^S\">"
				+ "<PARAM NAME=\"FetchColumns\" VALUE=\"1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21\">"
				+ "<PARAM NAME=\"PrimaryKeyColumns\" VALUE=\"1\">"
				+ "<PARAM NAME=\"GridHeaderText\" VALUE=\"Vendor Information\">"
				+ "<PARAM NAME=\"CacheSize\" VALUE=\"" + GetCacheSize() + "\">"
				+ "<PARAM NAME=\"ColorScheme\" VALUE=\"" + GetWindowsGridColor() + "\">"
				+ "<PARAM NAME=\"ExtraButtons\" VALUE=\"1^Add New^0\">"
				+ "</OBJECT>";
				*/
            #endregion
        }
        #region Web Form Designer generated code
        //override protected void OnInit(EventArgs e)
        //{
        //    InitializeComponent();
        //    base.OnInit(e);
        //}
        //private void InitializeComponent()
        //{
        //    this.Load += new System.EventHandler(this.Page_Load);

        //}
        #endregion
    }
}