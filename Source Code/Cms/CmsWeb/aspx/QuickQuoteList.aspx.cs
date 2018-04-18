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
using Cms.CmsWeb.WebControls;
using System.Reflection;
using Cms.BusinessLayer.BlCommon;
//using Cms.CmsWeb.WebServices;
using System.Resources;
namespace Cms.CmsWeb.aspx
{
	public class QuickQuoteList : cmsbase
	{
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected System.Web.UI.WebControls.Panel pnlGrid;
        protected System.Web.UI.WebControls.Label lblTemplate;
        protected System.Web.UI.HtmlControls.HtmlForm indexForm;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        protected System.Web.UI.WebControls.Label lblError;
        private static string CUSTOMER_SECTION = "CLT";
        protected System.Web.UI.WebControls.Label capMessage;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidDeleteApp;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAppVersionID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
        protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
        protected Cms.CmsWeb.WebControls.Menu bottomMenu;
        public string strCALLEDFROM = "";
        public string strCssClass = "";
        ResourceManager objResourceMgr1 = null;

        private void Page_Load(object sender, System.EventArgs e)
        {
            //Sets the cookie value
            SetCookieValue();
            string rootPath = System.Configuration.ConfigurationManager.AppSettings.Get("CmsPath").Trim();

            #region GETTING BASE COLOR FOR ROW SELECTION
            string colorScheme = GetColorScheme();
            string colors = "";
            objResourceMgr1 = new ResourceManager("Cms.CmsWeb.Aspx.QuickQuoteList", Assembly.GetExecutingAssembly());
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
            BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
            try
            {
                bool ShowGrid = true;

                string strCUSTOMER_ID = "";
                cltClientTop.Visible = false;

                /* If the control comes from the customer section then 
                 * 1.  check the session variable 'CUSTOMER_ID'
                 * 2. if there is some value then where clause contains only those records belonging to this customer
                 *	  else prompt the user.
                 * If the control is not coming from the customer section then normal flow follows */
                string sWHERECLAUSE = "";


                if (Request.QueryString["CALLEDFROM"] != null)
                {
                    strCALLEDFROM = Request.QueryString["CALLEDFROM"].ToString();
                    if (strCALLEDFROM.ToUpper() != "INCLT")
                    {

                        strCUSTOMER_ID = GetCustomerID();
                        bottomMenu.Visible = true;
                        strCssClass = "tableWidth";
                    }
                    else
                    {
                        ((Cms.CmsWeb.cmsbase)this).ScreenId = "120_0";
                        strCUSTOMER_ID = Request.Params["CUSTOMER_ID"].ToString();
                        bottomMenu.Visible = false;
                        strCssClass = "tableWidthHeader";

                    }
                    if (strCALLEDFROM.ToUpper().Trim() == CUSTOMER_SECTION || strCALLEDFROM.ToUpper().Trim() == "INCLT")
                    {

                        if (strCUSTOMER_ID != null && strCUSTOMER_ID != "")
                        {
                            sWHERECLAUSE = " pcl.CUSTOMER_ID = " + strCUSTOMER_ID;

                            //Show the Client top
                            if (Request.Params["CalledFrom"].ToString().ToUpper() != "INCLT")
                            {
                                cltClientTop.CustomerID = int.Parse(strCUSTOMER_ID);
                                cltClientTop.ShowHeaderBand = "Client";
                                cltClientTop.Visible = true;
                            }

                        }
                        else
                        {
                            sWHERECLAUSE = " 1<>1 ";
                            lblError.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("115");
                            ShowGrid = false;
                        }
                    }
                }
                else
                {
                    bottomMenu.Visible = true;
                    strCssClass = "tableWidth";
                }
                hidCalledFrom.Value = strCALLEDFROM;

                /* Check the SystemID of the logged in user.
                 * If the user is not a Wolverine user then display records of that agency ONLY
                 * else the normal flow follows */
                string strSystemID = GetSystemId();
                //Changed by Charles on 19-May-10 for Itrack 51
                string strCarrierSystemID = CarrierSystemID;//System.Configuration.ConfigurationManager.AppSettings.Get("CarrierSystemID").ToString();
                if (strSystemID.Trim().ToUpper() != strCarrierSystemID.Trim().ToUpper())
                {
                    string strAgencyID = ClsAgency.GetAgencyIDFromCode(strSystemID).ToString();
                    if (sWHERECLAUSE.Trim().Equals(""))
                    {
                        sWHERECLAUSE = " pcl.AGENCY_ID = " + strAgencyID;
                    }
                    else
                    {
                        sWHERECLAUSE = sWHERECLAUSE + " AND pcl.AGENCY_ID = " + strAgencyID;
                    }
                }


                //Setting web grid control properties
                objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath + "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                //objWebGrid.SelectClause = "DISTINCT APP_LIST.APP_ID,isnull(CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME,'') +' '+isnull(CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME,'') +' '+ isnull(CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME,'') as CustomerName,MNT_LOB_MASTER.LOB_DESC,isnull(APP_LIST.APP_NUMBER,'') + ' ' + isnull(case when APP_LIST.app_number is not null then isnull(APP_LIST.APP_VERSION,'') + ISNull(' ' +MLV.LOOKUP_VALUE_DESC+' ' ,'') + '('+ isnull(UPPER(PCL.POLICY_STATUS),'')+')' end,'')APP_NUMBER, PCL.POLICY_NUMBER + ' ' + POLICY_DISP_VERSION + ' (' + ISNULL(STATUS_MASTER.POLICY_DESCRIPTION,'') + ') '  POLICY_NUMBER, QQ_NUMBER, MNT_AGENCY_LIST.AGENCY_DISPLAY_NAME as Agency, MNT_USER_LIST.USER_FNAME +' ' + MNT_USER_LIST.USER_LNAME as CSR,' ' as Quote, Convert(varchar(10),APP_LIST.APP_EFFECTIVE_DATE,101) as EffectiveDate ,APP_LIST.APP_STATUS, CLT_CUSTOMER_LIST.CUSTOMER_ZIP, APP_LIST.CUSTOMER_ID,  	APP_LIST.APP_VERSION_ID,MNT_LOB_MASTER.LOB_id,STATE_CODE, Convert(varchar(10),APP_LIST.APP_EXPIRATION_DATE,101) as ExpirationDate,case when quote_xml is not null then '<img src=" + (rootPath) + "/cmsweb/images/quote.gif style=''Border-Width:0'' ImageAlign=absMiddle>'  else '' end  as quote_xml,APP_LIST.IS_ACTIVE,APP_LIST.BILL_TYPE AS BILL_TYPE,PCL.POLICY_ID,PCL.POLICY_VERSION_ID";

                objWebGrid.SelectClause = @" pcl.APP_NUMBER as APP_NUMBER,
Convert(varchar(10),PCL.APP_EFFECTIVE_DATE,case " + GetLanguageID() + @" when 2 then 103 else 101 end) as EffectiveDate ,
Convert(varchar(10),PCL.APP_EXPIRATION_DATE,case " + GetLanguageID() + @" when 2 then 103 else 101 end) as ExpirationDate,
pcl.POLICY_LOB as lob_id,lob.LOB_DESC,pcl.POLICY_STATUS,
isnull(CLT.CUSTOMER_FIRST_NAME,'') +' '+isnull(CLT.CUSTOMER_MIDDLE_NAME,'') +' '+ isnull(CLT.CUSTOMER_LAST_NAME,'') as CustomerName,
isnull(MSCL.STATE_CODE,case " + GetLanguageID() + @" when 2 then 'Todos' else 'All' end) as STATE_CODE,pcl.BILL_TYPE,pcl.CUSTOMER_ID,pcl.APP_VERSION_ID,pcl.APP_ID,pcl.POLICY_ID,pcl.POLICY_VERSION_ID,
isnull(pcl.IS_ACTIVE,'N') as IS_ACTIVE,case when qcql.quote_xml is not null then '<img src=/cms//cmsweb/images/quote.gif style=''Border-Width:0'' ImageAlign=absMiddle>'  else '' end  as quote_xml,
'POLICY_ID='+isnull(cast(PCL.POLICY_ID as varchar(8000)),0)+'&CUSTOMER_ID='+isnull(cast(PCL.CUSTOMER_ID as varchar(8000)),0)+'&POLICY_VERSION_ID='+isnull(cast(PCL.POLICY_VERSION_ID as varchar(8000)),0)+'&LOB_id='+isnull(cast(LOB.LOB_id as varchar(8000)),0) as UniqueGrdId ";

                //objWebGrid.FromClause = "APP_LIST WITH(NOLOCK) LEFT JOIN CLT_QUICKQUOTE_LIST CQL  WITH(NOLOCK) ON APP_LIST.CUSTOMER_ID = CQL.CUSTOMER_ID AND CQL.APP_ID = APP_LIST.APP_ID AND CQL.APP_VERSION_ID = APP_LIST.APP_VERSION_ID  INNER JOIN MNT_LOB_MASTER WITH(NOLOCK) ON APP_LIST.APP_LOB = MNT_LOB_MASTER.LOB_ID "
                //    + " INNER JOIN CLT_CUSTOMER_LIST  WITH(NOLOCK) ON APP_LIST.CUSTOMER_ID = CLT_CUSTOMER_LIST.CUSTOMER_ID "
                //    + " LEFT OUTER JOIN MNT_USER_LIST  WITH(NOLOCK) ON APP_LIST.CSR = MNT_USER_LIST.USER_ID INNER JOIN MNT_AGENCY_LIST  WITH(NOLOCK) ON APP_LIST.APP_AGENCY_ID = MNT_AGENCY_LIST.AGENCY_ID "
                //    + " LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MSCL  WITH(NOLOCK) ON MSCL.STATE_ID=APP_LIST.STATE_ID "
                //    + " LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV WITH(NOLOCK) ON  MLV.LOOKUP_UNIQUE_ID=APP_LIST.POLICY_TYPE LEFT JOIN POL_CUSTOMER_POLICY_LIST PCL  WITH(NOLOCK) ON PCL.APP_ID = APP_LIST.APP_ID AND PCL.APP_VERSION_ID = APP_LIST.APP_VERSION_ID AND PCL.CUSTOMER_ID = APP_LIST.CUSTOMER_ID"
                //    + " LEFT JOIN POL_POLICY_STATUS_MASTER STATUS_MASTER  WITH(NOLOCK) ON STATUS_MASTER.POLICY_STATUS_CODE = PCL.POLICY_STATUS LEFT OUTER JOIN QOT_CUSTOMER_QUOTE_LIST qcql  WITH(NOLOCK) ON APP_LIST.customer_id=qcql.customer_id"
                //    + " and app_list.app_id=qcql.app_id and  app_list.app_version_id=qcql.app_version_id";

                objWebGrid.FromClause = @"  POL_CUSTOMER_POLICY_LIST PCL
LEFT OUTER JOIN QOT_CUSTOMER_QUOTE_LIST_POL qcql WITH(NOLOCK) ON pcl.customer_id=qcql.customer_id and pcl.POLICY_ID=qcql.POLICY_ID and pcl.POLICY_VERSION_ID=qcql.POLICY_VERSION_ID
LEFT OUTER JOIN CLT_CUSTOMER_LIST CLT  WITH(NOLOCK) ON PCL.CUSTOMER_ID = CLT.CUSTOMER_ID 
LEFT OUTER JOIN MNT_LOB_MASTER LOB WITH(NOLOCK) ON PCL.POLICY_LOB = LOB.LOB_ID
LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MSCL  WITH(NOLOCK) ON MSCL.STATE_ID=PCL.STATE_ID ";

                objWebGrid.WhereClause = sWHERECLAUSE + " AND PCL.APP_STATUS ='QAPP'";

                objWebGrid.SearchColumnHeadings = objResourceMgr1.GetString("SearchColumnHeadings");//"App #^Eff. Date^Exp. Date^Agency^CSR^LOB^State^Bill Type"; //Policy #^Quick Quote #^

                //objWebGrid.SearchColumnNames = "isnull(APP_LIST.APP_NUMBER,'') ! ' ' ! isnull(case when APP_LIST.app_number is not null then isnull(APP_LIST.APP_VERSION,'') ! ISNull(' ' ! MLV.LOOKUP_VALUE_DESC ! ' ' ,'') ! '(' ! isnull(APP_LIST.APP_STATUS,'') ! ')' else '' end,'')^APP_LIST.APP_EFFECTIVE_DATE^APP_LIST.APP_EXPIRATION_DATE^MNT_AGENCY_LIST.AGENCY_DISPLAY_NAME^ISNULL(MNT_USER_LIST.USER_FNAME,'') ! ISNULL(MNT_USER_LIST.USER_LNAME,'')^MNT_LOB_MASTER.LOB_ID^State_Code^APP_LIST.BILL_TYPE";//^PCL.POLICY_NUMBER ! ' ' ! POLICY_DISP_VERSION ! ' (' ! ISNULL(STATUS_MASTER.POLICY_DESCRIPTION,'') ! ') '^QQ_NUMBER				
                objWebGrid.SearchColumnNames = "APP_Number^APP_EFFECTIVE_DATE^APP_EXPIRATION_DATE^lob_id^MSCL.STATE_CODE^pcl.BILL_TYPE";

                objWebGrid.SearchColumnType = "T^D^D^L^T^T";//T^T^

                objWebGrid.DropDownColumns = "^^^LOB^^";//^^

                objWebGrid.OrderByClause = "APP_NUMBER ASC";

                objWebGrid.DisplayColumnNumbers = "4^10^7^3^17^16^20";//5^6^

                objWebGrid.DisplayColumnNames = "APP_NUMBER^EffectiveDate^ExpirationDate^quote_xml^LOB_DESC^STATE_CODE^BILL_TYPE";//^POLICY_NUMBER^QQ_NUMBER

                objWebGrid.DisplayColumnHeadings = objResourceMgr1.GetString("DisplayColumnHeadings");//"App #^Eff. Date^Exp. Date^Agency^CSR^Rate^LOB^State^Bill Type";//^Policy #^Quick Quote #

                objWebGrid.DisplayTextLength = "40^20^20^20^20^20^20";//20^20^

                objWebGrid.DisplayColumnPercent = "10^7^7^8^12^5^5";//15^10^
                objWebGrid.PrimaryColumns = "1";
                objWebGrid.PrimaryColumnsName = "pcl.APP_ID^pcl.CUSTOMER_ID^pcl.APP_VERSION_ID^LOB_id";

                objWebGrid.ColumnTypes = "B^B^B^IMGLNK^B^B^B";//B^B^
                objWebGrid.ColumnsLink = "^^^Quotegenerator.aspx^^^"; //^^
                objWebGrid.AllowDBLClick = "true";
                objWebGrid.FetchColumns = "2";
                objWebGrid.SearchMessage = objResourceMgr1.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objWebGrid.ExtraButtons = objResourceMgr1.GetString("ExtraButtons");//"1^Add New^0^addRecord";//"2^Add New~Delete App^0~1^addRecord~deleteApp"; //
                objWebGrid.PageSize = int.Parse(GetPageSize());
                objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr1.GetString("HeaderString");//"Application Information" ;
                objWebGrid.SelectClass = colors;

                objWebGrid.QueryStringColumns = "CUSTOMER_ID^POLICY_ID^POLICY_VERSION_ID^APP_ID^APP_VERSION_ID";// CUSTOMER_ID^APP_ID^APP_VERSION_ID
                objWebGrid.DefaultSearch = "Y";

                objWebGrid.FilterLabel = objResourceMgr1.GetString("FilterLabel");//"Include Inactive";
                objWebGrid.FilterColumnName = "pcl.IS_ACTIVE";
                objWebGrid.FilterValue = "Y";
                objWebGrid.RequireQuery = "Y";

                //To enable grouping of application number field
                objWebGrid.Grouping = "Y";
                objWebGrid.GroupQueryColumns = "APP_NUMBER";
                //objWebGrid.ScreenIDProp = base.ScreenId;  

                //Adding to gridholder
                if (ShowGrid)
                {
                    GridHolder.Controls.Add(objWebGrid);
                    TabCtl.Visible = false;
                    TabCtl.TabURLs = "GeneralInformation.aspx?CALLEDFROM=" + strCALLEDFROM + "&";
                }

                #region COMMENTED CODE FOR DELETING SELECTED APPLICATION - temporarily removed. = Nidhi : June 30th 2005
                //				if (Request.QueryString["DELETEAPP"]!=null)
                //				{
                //					if (Request.QueryString["DELETEAPP"].ToString()=="1")
                //					{
                //						if (Request.QueryString["CustomerID"]!=null)
                //							hidCustomerID.Value = Request.QueryString["CustomerID"].ToString();
                //
                //						if (Request.QueryString["AppID"]!=null)
                //							hidAppID.Value = Request.QueryString["AppID"].ToString();
                //
                //						if (Request.QueryString["AppVersionID"]!=null)
                //							hidAppVersionID.Value = Request.QueryString["AppVersionID"].ToString();
                //
                //					}
                //
                //					string DeletedBy = GetUserId();
                //					//Get the selected keys from 'hidlocQueryStr'
                //					 
                //					int RetVal = ClsGeneralInformation.DeleteApplication(int.Parse(hidCustomerID.Value ), int.Parse(hidAppID.Value ), int.Parse(hidAppVersionID.Value), int.Parse (DeletedBy==""?"0":DeletedBy));
                //				
                //					hidDeleteApp.Value="";
                //				}
                # endregion

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            #endregion
        }

        private void SetCookieValue()
        {
            Response.Cookies["LastVisitedTab"].Value = "0";
            Response.Cookies["LastVisitedTab"].Expires = DateTime.Now.Add(new TimeSpan(30, 0, 0, 0, 0));
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
