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


namespace Reports.Aspx
{
	/// <summary>
	/// Summary description for TrailBalance.
	/// </summary>
	public class TrailBalance : Cms.CmsWeb.cmsbase  
	{

		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlTableRow tabCtlRow;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;

		public string  strSystemID="";

		private void Page_Load(object sender, System.EventArgs e)
		{
			#region loading web grid control
			Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{
				BaseDataGrid objWebGrid;
				objWebGrid = (BaseDataGrid)c1;

				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

				objWebGrid.SelectClause = "REIN_COMAPANY_ID,REIN_COMAPANY_CODE,REIN_COMAPANY_NAME,IsNull(REIN_COMAPANY_ADD1,'')+' '+IsNull(REIN_COMAPANY_ADD2,'') As Address,REIN_COMAPANY_PHONE,REIN_COMAPANY_EXT,REIN_COMAPANY_CITY,B.STATE_NAME REIN_COMAPANY_STATE,REIN_COMAPANY_FAX,convert(varchar,TERMINATION_DATE,101) TERMINATION_DATE,C.LOOKUP_VALUE_DESC REIN_COMPANY_TYPE,ISNULL(A.IS_ACTIVE,'Y') AS IS_ACTIVE";
                                      
				objWebGrid.FromClause = "MNT_REIN_COMAPANY_LIST A inner join MNT_COUNTRY_STATE_LIST B ON A.REIN_COMAPANY_STATE=B.STATE_ID inner join MNT_LOOKUP_VALUES C ON A.REIN_COMPANY_TYPE=C.lookup_value_code and C.lookup_id=1315";
				//objWebGrid.WhereClause = "";

				objWebGrid.SearchColumnHeadings = "Reinsurer Code^Reinsurer/Broker Name^Address^City^State^Phone^Fax^Reinsurance Type";
				objWebGrid.SearchColumnNames = "REIN_COMAPANY_CODE^REIN_COMAPANY_NAME^REIN_COMAPANY_ADD1^REIN_COMAPANY_CITY^REIN_COMAPANY_STATE^REIN_COMAPANY_PHONE^REIN_COMAPANY_FAX^REIN_COMPANY_TYPE";
				objWebGrid.SearchColumnType = "T^T^T^T^T^T^T^T";

				objWebGrid.OrderByClause = "REIN_COMAPANY_NAME asc";

				objWebGrid.DisplayColumnNumbers = "3^4^7^8^5^6^9^10";
				objWebGrid.DisplayColumnNames = "REIN_COMAPANY_NAME^Address^REIN_COMAPANY_CITY^REIN_COMAPANY_STATE^REIN_COMAPANY_PHONE^REIN_COMAPANY_EXT^REIN_COMAPANY_FAX^TERMINATION_DATE";
				objWebGrid.DisplayColumnHeadings = "Reinsurer/Broker Name^Address^City^State^Phone 1^Phone 2^Fax^Termination Date";
				objWebGrid.DisplayTextLength = "150^100^100^100^100^100^100^150";
				objWebGrid.DisplayColumnPercent = "20^20^10^10^10^10^10^10";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "REIN_COMAPANY_ID";

				objWebGrid.ColumnTypes = "B^B^B^B^B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12";
				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";objWebGrid.ExtraButtons = "";
				objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
				objWebGrid.PageSize =int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.HeaderString ="Reinsurer/Brokers Information" ;
				/////objWebGrid.SelectClass = colors ;
				objWebGrid.FilterLabel="Include Inactive";
				objWebGrid.FilterColumnName="A.IS_ACTIVE";
				objWebGrid.FilterValue="Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch="Y";
				objWebGrid.QueryStringColumns = "REIN_COMAPANY_ID";
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);


				
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			
		}
	
		
		#endregion
		
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
