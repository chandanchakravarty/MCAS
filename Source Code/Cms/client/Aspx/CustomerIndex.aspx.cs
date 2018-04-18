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
/******************************************************************************************
	<Author					: - Vijay Joshi >
	<Start Date				: -	3/24/2005 10:37:29 AM>
	<End Date				: - 31-Mar-2005 >
	<Description			: - Used to show the index page of Customer>
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - >
	<Modified By			: - >
	<Purpose				: - >
*******************************************************************************************/

namespace Cms.Client.Aspx
{
	/// <summary>
	/// Summary description for CustomerIndex.
	/// </summary>
	public class CustomerIndex :  Cms.Client.clientbase
	{
		protected System.Web.UI.WebControls.Panel pnlGrid;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		private void Page_Load(object sender, System.EventArgs e)
		{
			//string strCustomerID = "1";
			#region loading web grid control
			
			Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			
			BaseDataGrid objWebGrid;
			objWebGrid = (BaseDataGrid)c1;

			/* Check the SystemID of the logged in user.
				 * If the user is not a Wolverine user then display records of that agency ONLY
				 * else the normal flow follows */
			string sWHERECLAUSE	=	"";
			string  strSystemID			 = GetSystemId();
            string strCarrierSystemID = CarrierSystemID;//System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString();
			if ( strSystemID.Trim().ToUpper() != strCarrierSystemID.Trim().ToUpper())
			{
				string strAgencyID = ClsAgency.GetAgencyIDFromCode(strSystemID).ToString();
				if (sWHERECLAUSE.Trim().Equals(""))
				{						
					sWHERECLAUSE = " CLT_CUSTOMER_LIST.CUSTOMER_AGENCY_ID = "+ strAgencyID;	
				}
				else
				{
					sWHERECLAUSE = " AND CLT_CUSTOMER_LIST.CUSTOMER_AGENCY_ID = "+ strAgencyID;	
				}
			}

			try
			{
				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

				objWebGrid.SelectClause				=	"CUSTOMER_ID,CUSTOMER_FIRST_NAME + ' ' + IsNull(CUSTOMER_MIDDLE_NAME,'') + ' ' + IsNull(CUSTOMER_LAST_NAME,'') Name,CUSTOMER_CODE,CUSTOMER_ADDRESS1,CUSTOMER_HOME_PHONE,AGENCY_DISPLAY_NAME,CUSTOMER_LAST_NAME";
                objWebGrid.FromClause				=	"CLT_CUSTOMER_LIST LEFT JOIN MNT_AGENCY_LIST ON AGENCY_ID = CUSTOMER_AGENCY_ID";
				objWebGrid.WhereClause				=	sWHERECLAUSE;
				
				objWebGrid.SearchColumnHeadings		=	"Last Name^Name^Code^Home Phone^Agency Name";
				objWebGrid.SearchColumnNames		=	"CUSTOMER_LAST_NAME^CUSTOMER_FIRST_NAME ! ' ' ! IsNull(CUSTOMER_MIDDLE_NAME,'') ! ' ' ! IsNull(CUSTOMER_LAST_NAME,'')^CUSTOMER_CODE^CUSTOMER_HOME_PHONE^AGENCY_DISPLAY_NAME";
				objWebGrid.SearchColumnType			=	"T^T^T^T^T";
				
				objWebGrid.OrderByClause			=	"Name ASC";
				
				objWebGrid.DisplayColumnNumbers		=	"2^3^4^5^6";
				objWebGrid.DisplayColumnNames		=	"Name^CUSTOMER_CODE^CUSTOMER_ADDRESS1^CUSTOMER_HOME_PHONE^AGENCY_DISPLAY_NAME";
				objWebGrid.DisplayColumnHeadings	=	"Name^Code^Address^Home Phone^Agency Name";

				objWebGrid.DisplayTextLength		=	"100^50^80^80^50";
				objWebGrid.DisplayColumnPercent		=	"20^10^20^20^10";
				objWebGrid.PrimaryColumns			=	"1";
				objWebGrid.PrimaryColumnsName		=	"CUSTOMER_ID";

				objWebGrid.ColumnTypes				=	"B^B^B^B^B";
				objWebGrid.AllowDBLClick			=	"true";
				objWebGrid.FetchColumns				=	"1^2^3^4^5^6^7";

				objWebGrid.SearchMessage			=	"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				
				objWebGrid.ExtraButtons				=	"1^Add New^0^addRecord";
				objWebGrid.PageSize					=	int.Parse(GetPageSize());
				objWebGrid.CacheSize				=	int.Parse(GetCacheSize()); 
				objWebGrid.ImagePath				=	System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim()+ "/images/collapse_icon.gif";
                objWebGrid.HImagePath               = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.HeaderString				=	"Customer Details";
				//objWebGrid.SelectClass = colors;	
				
				//objWebGrid.FilterLabel = "Show Complete";
				//objWebGrid.FilterColumnName = "";
				//objWebGrid.FilterValue = "";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.QueryStringColumns		=	"CUSTOMER_ID";
				objWebGrid.DefaultSearch = "Y";
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(c1);
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
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
