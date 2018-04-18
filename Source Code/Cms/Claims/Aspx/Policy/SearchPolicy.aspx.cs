/******************************************************************************************
<Author				: -		Vijay Arora
<Start Date			: -		01-05-2006
<End Date			: -	
<Description		: - 	Index Page for searching the policy in Claims.
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		:		
<Modified By		:
<Purpose			: 
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
using System.Resources;
using System.Reflection;
using Cms.Model.Maintenance; 
using Cms.BusinessLayer.BlCommon;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.WebControls;


namespace Cms.Claims.Aspx.Policy
{

	/// <summary>
	/// 
	/// </summary>
	public class SearchPolicy : Cms.Claims.ClaimBase
	{
		protected System.Web.UI.WebControls.Panel pnlGrid;
		protected System.Web.UI.WebControls.Table tblReport;
		protected System.Web.UI.WebControls.Panel pnlReport;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Literal litTextGrid;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidBackToMatchPolicy;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDUMMY_CLAIM_NUMBER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDUMMY_POLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
        ResourceManager objResourceMgr = null;
		protected string strCustomerID;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			Ajax.Utility.RegisterTypeForAjax(typeof(SearchPolicy));
			SetActivityStatus("");
			#region Setting screen id
			//Done for Itrack Issue 6553 on 13 Oct 09
			base.ScreenId	=	"304";
			#endregion			
            objResourceMgr = new ResourceManager("Cms.Claims.Aspx.Policy.SearchPolicy", Assembly.GetExecutingAssembly());	
			#region GETTING BASE COLOR FOR ROW SELECTION
			string colorScheme=GetColorScheme();
			string colors="";

			switch (int.Parse(colorScheme))
			{
				case 1:
					colors=System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();     
					break;
				case 2:
					colors=System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR2").ToString();     
					break;
				case 3:
					colors=System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR3").ToString();     
					break;
				case 4:
					colors=System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR4").ToString();     
					break;
			}

			if(colors!="")
			{
				string [] baseColor=colors.Split(new char []{','});  
				if(baseColor.Length>0)
					colors= "#" + baseColor[0];
			}
			#endregion         

			GetQueryStringValues();
			//Clear claims session values			
			base.ClearClaimsSessionValues();
            
			#region loading web grid control
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{
                //Addded by santosh kumar gautam on 17 dec 2010
                string LangID = GetLanguageID();
                int BaseCurrency = 1;
                if (GetSYSBaseCurrency() == enumCurrencyId.BR)
                    BaseCurrency = 2;
                else
                    BaseCurrency = 1;
				
				string rootPath=System.Configuration.ConfigurationManager.AppSettings.Get("CmsPath").Trim();
				string sSELECTCLAUSE="", sFROMCLAUSE="";
				sSELECTCLAUSE= " * ";
				
                //Addded by santosh kumar gautam on 21 dec 2010
                sFROMCLAUSE = " (SELECT ISNULL(CCL.CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CCL.CUSTOMER_MIDDLE_NAME,'') + ' ' + ISNULL(CCL.CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME, " +
                            " PCPL.POLICY_NUMBER  + ' ' + PCPL.POLICY_DISP_VERSION + ' ' + '( ' + ISNULL(PM.POLICY_DESCRIPTION, PPSM.POLICY_DESCRIPTION) + ' )' AS NUMBER_POLICY, " +
                            " ISNULL(Convert(varchar,PCPL.POL_VER_EFFECTIVE_DATE,case when " + LangID + "=2 then 103 else 101 end),'') + ' - ' + " +
                            " ISNULL(Convert(varchar,PCPL.POL_VER_EXPIRATION_DATE,case when " + LangID + "=2 then 103 else 101 end),'') " +
                            " AS EFFECTIVE_PERIOD, ISNULL(MLM.LOB_DESC,M.LOB_DESC) AS LOB, PCPL.CUSTOMER_ID, PCPL.POLICY_ID, PCPL.POLICY_VERSION_ID, " +
                            " PCPL.POLICY_LOB AS LOB_ID,PCPL.POL_VER_EFFECTIVE_DATE AS EFFECTIVE_DATE, PCPL.POL_VER_EXPIRATION_DATE AS EXPIRATION_DATE,  " +
                            " ISNULL(CAL.FIRST_NAME ,'') + ' ' + ISNULL(CAL.MIDDLE_NAME,'')+' '+ISNULL(CAL.LAST_NAME,'') AS CO_APPLICANT_NAME " +
                            " FROM POL_CUSTOMER_POLICY_LIST PCPL   " +
                            " LEFT JOIN POL_POLICY_STATUS_MASTER PPSM ON PPSM.POLICY_STATUS_CODE = PCPL.POLICY_STATUS  " +
                            " LEFT JOIN POL_POLICY_STATUS_MASTER_MULTILINGUAL PM ON PM.POLICY_STATUS_CODE = PCPL.POLICY_STATUS  AND PM.LANG_ID="+LangID+" " +
                            " LEFT JOIN MNT_LOB_MASTER M ON M.LOB_ID = PCPL.POLICY_LOB   " +
                            " LEFT JOIN MNT_LOB_MASTER_MULTILINGUAL MLM ON MLM.LOB_ID= M.LOB_ID AND MLM.LOB_ID = PCPL.POLICY_LOB  AND MLM.LANG_ID="+LangID+" "  +
                            " LEFT JOIN CLT_CUSTOMER_LIST CCL ON CCL.CUSTOMER_ID = PCPL.CUSTOMER_ID  "+
                            " LEFT OUTER JOIN POL_APPLICANT_LIST PAL WITH(NOLOCK) ON PAL.CUSTOMER_ID=PCPL.CUSTOMER_ID  AND PAL.POLICY_ID = PCPL.POLICY_ID AND PAL.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID " +
		                    " LEFT OUTER JOIN CLT_APPLICANT_LIST CAL WITH(NOLOCK) ON CAL.CUSTOMER_ID= PAL.CUSTOMER_ID AND CAL.APPLICANT_ID= PAL.APPLICANT_ID " +
                            " WHERE PCPL.POLICY_STATUS IS NOT NULL AND PCPL.POLICY_STATUS <>'' AND PCPL.POLICY_STATUS NOT IN('UISSUE','REJECT','Suspended','APPLICATION') AND PCPL.POLICY_NUMBER IS NOT NULL AND PCPL.POLICY_NUMBER<>'' ";

                if (strCustomerID != "" && strCustomerID != "0")
                    sFROMCLAUSE += " AND PCPL.CUSTOMER_ID=" + strCustomerID;
				sFROMCLAUSE += " ) Test";
				

				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = sSELECTCLAUSE;
				objWebGrid.FromClause = sFROMCLAUSE ;
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Customer Name^Policy #^Effective Period^LOB";	
                objWebGrid.SearchColumnNames = "CUSTOMER_NAME^NUMBER_POLICY^EFFECTIVE_DATE^EXPIRATION_DATE^LOB_ID^CO_APPLICANT_NAME";
				objWebGrid.DropDownColumns          =   "^^^^LOB^";
				objWebGrid.SearchColumnType			=	"T^T^D^D^L^T";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Customer Name^Policy #^Effective Period^LOB";	
				objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";
                objWebGrid.DisplayColumnNames = "CUSTOMER_NAME^NUMBER_POLICY^EFFECTIVE_PERIOD^LOB^CO_APPLICANT_NAME";
				objWebGrid.DisplayTextLength = "30^30^25^15^30";
				objWebGrid.DisplayColumnPercent = "30^30^25^15^30";
				objWebGrid.PrimaryColumns = "2";
				objWebGrid.PrimaryColumnsName = "NUMBER_POLICY";
				objWebGrid.ColumnTypes = "LBL^B^B^B^B";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Policy Search" ;
                objWebGrid.OrderByClause = "CUSTOMER_NAME asc,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID";//Done for Itrack Issue 7670 on 21 July 2010
				objWebGrid.ColumnsLink= rootPath + "claims/aspx/ClaimsNotificationIndex.aspx?"; 
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				//objWebGrid.ExtraButtons = "1^Add Claim Against Unmatched Policy^0^addNewClaim";//Done for Itrack Issue 5978 on 19 June 2008											
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse (GetCacheSize());
				objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim()+ "/images/collapse_icon.gif";
				objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim()+ "/images/expand_icon.gif";
				objWebGrid.SelectClass = colors;				
				objWebGrid.RequireQuery = "Y";
				objWebGrid.QueryStringColumns = "CUSTOMER_ID^POLICY_ID^POLICY_VERSION_ID^LOB_ID^POLICY_NUMBER";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.Grouping = "Y";
				objWebGrid.GroupQueryColumns = "CUSTOMER_NAME";			

				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);
			}
			catch (Exception ex)
			{throw (ex);}
			#endregion

		}

		#region Get Query String Values
		private void GetQueryStringValues()
		{
			if(Request.QueryString["BackToMatchPolicy"]!=null && Request.QueryString["BackToMatchPolicy"].ToString()!="")
				hidBackToMatchPolicy.Value = Request.QueryString["BackToMatchPolicy"].ToString();
			else
				hidBackToMatchPolicy.Value = "0";

			if(Request.QueryString["DUMMY_POLICY_ID"]!=null && Request.QueryString["DUMMY_POLICY_ID"].ToString()!="")
				hidDUMMY_POLICY_ID.Value = Request.QueryString["DUMMY_POLICY_ID"].ToString();
			else
				hidDUMMY_POLICY_ID.Value = "0";

			if(Request.QueryString["DUMMY_CLAIM_NUMBER"]!=null && Request.QueryString["DUMMY_CLAIM_NUMBER"].ToString()!="")
				hidDUMMY_CLAIM_NUMBER.Value = Request.QueryString["DUMMY_CLAIM_NUMBER"].ToString();
			else
				hidDUMMY_CLAIM_NUMBER.Value = "0";

			if(Request.QueryString["CLAIM_ID"]!=null && Request.QueryString["CLAIM_ID"].ToString()!="")
				hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();
			else
				hidCLAIM_ID.Value = "0";
			if(Request.QueryString["CUSTOMER_ID"]!=null && Request.QueryString["CUSTOMER_ID"].ToString()!="")
				strCustomerID 	= Request.QueryString["CUSTOMER_ID"].ToString();
			else
				strCustomerID="0";
		}
		#endregion

		[Ajax.AjaxMethod()]
		public int ProcessInProgress(string rowXML)
		{
			CmsWeb.webservices.ClsWebServiceCommon obj =  new CmsWeb.webservices.ClsWebServiceCommon();
			int result = 0;
			result = obj.GetClaimPolicyStatus(rowXML);
			return result;
			
		}


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}