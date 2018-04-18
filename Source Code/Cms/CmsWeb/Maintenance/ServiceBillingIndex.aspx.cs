/******************************************************************************************
<Author				: -		Vijay Arora
<Start Date			: -		14-04-2006
<End Date			: -	
<Description		: - 	IIX Web Service Billing Index Page
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
using System.Xml; 
using System.IO;
using Cms.CmsWeb.WebControls;
using Cms.BusinessLayer.BlApplication;
using System.Reflection;
using System.Resources;
namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// 
	/// </summary>
	public class ServiceBillingIndex : Cms.CmsWeb.cmsbase
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
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
        ResourceManager objResourceMgr = null;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			#region Setting screen id
			base.ScreenId	=	"285_0";
            #endregion
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.ServiceBillingIndex", Assembly.GetExecutingAssembly());

			#region GETTING BASE COLOR FOR ROW SELECTION
			string colorScheme=GetColorScheme();
			string colors="";

			switch (int.Parse(colorScheme))
			{
				case 1:
					colors=System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();     
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

			if(colors!="")
			{
				string [] baseColor=colors.Split(new char []{','});  
				if(baseColor.Length>0)
					colors= "#" + baseColor[0];
			}
			#endregion         

			#region loading web grid control
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{
 
				string sSELECTCLAUSE="";
				
				sSELECTCLAUSE=" ( SELECT ISNULL(CCL.CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CCL.CUSTOMER_MIDDLE_NAME,'') + ' ' +  ISNULL(CCL.CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME,POLICY_NUMBER,POLICY_DISP_VERSION, MLV.LOOKUP_VALUE_DESC, SERVICE_VENDOR, CONVERT(VARCHAR(10),REQUEST_DATETIME,101) AS REQUEST_DATE, CONVERT(VARCHAR(10),RESPONSE_DATETIME,101) AS RESPONSE_DATE ";
				sSELECTCLAUSE  += " FROM MNT_REQUEST_RESPONSE_LOG MRRL LEFT JOIN CLT_CUSTOMER_LIST CCL ON MRRL.CUSTOMER_ID = CCL.CUSTOMER_ID LEFT JOIN POL_CUSTOMER_POLICY_LIST PCPL ON MRRL.CUSTOMER_ID = PCPL.CUSTOMER_ID AND MRRL.POLICY_ID = PCPL.POLICY_ID AND MRRL.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID LEFT JOIN MNT_LOOKUP_VALUES MLV ON MRRL.CATEGORY_ID = MLV.LOOKUP_UNIQUE_ID ) Test ";
				
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = " * ";
				objWebGrid.FromClause = sSELECTCLAUSE ;
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Customer Name^Policy #^Description^Vendor^Request Date^Response Date";
				objWebGrid.SearchColumnNames = "CUSTOMER_NAME^POLICY_NUMBER^LOOKUP_VALUE_DESC^SERVICE_VENDOR^REQUEST_DATE^RESPONSE_DATE";
				objWebGrid.SearchColumnType = "T^T^T^T^D^D";
				objWebGrid.OrderByClause	="REQUEST_DATE desc";
				objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6^7";
				objWebGrid.DisplayColumnNames = "CUSTOMER_NAME^POLICY_NUMBER^POLICY_DISP_VERSION^LOOKUP_VALUE_DESC^SERVICE_VENDOR^REQUEST_DATE^RESPONSE_DATE";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Customer Name^Policy #^Version^Description^Vendor^Request Date^Response Date";
				objWebGrid.DisplayTextLength = "25^10^5^25^5^15^15";
				objWebGrid.DisplayColumnPercent = "25^10^5^25^5^15^15";
				objWebGrid.PrimaryColumns = "6";
				objWebGrid.PrimaryColumnsName = "REQUEST_DATE";
				objWebGrid.ColumnTypes = "B^B^B^B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse (GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Service Billing" ;
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Show Complete";				 
				objWebGrid.DefaultSearch = "Y";
						
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);
			}
			catch (Exception ex)
			{throw (ex);}
			#endregion
		
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