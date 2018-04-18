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

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for ReconGroupIndex.
	/// </summary>
	public class ReconGroupIndex : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMode;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidlocQueryStr;
		private string strSelect = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="191";
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
			
			Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			BaseDataGrid objWebGrid;
			objWebGrid = (BaseDataGrid)c1;

			try
			{
				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
/*
				objWebGrid.SelectClause = "Convert(Varchar,ACT_RECONCILIATION_GROUPS.CREATED_DATETIME,101) CD," 
					+ "CASE RECON_ENTITY_TYPE "
					+ " WHEN '1' THEN IsNull(CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME,'') + ' ' + IsNull(CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME,'') + ' ' + IsNull(CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME,'') "
					+ " WHEN '2' THEN IsNull(MNT_AGENCY_LIST.AGENCY_DISPLAY_NAME, '') "
					+ " END RECON_ENTITY_ID" 
					+",CASE RECON_ENTITY_TYPE WHEN '1' THEN 'Customer' WHEN '2' THEN 'Agency' END RECON_ENTITY_TYPE,GROUP_ID";

				objWebGrid.FromClause = "ACT_RECONCILIATION_GROUPS "
					+ " LEFT JOIN CLT_CUSTOMER_LIST with(nolock) ON "
					+ " CLT_CUSTOMER_LIST.CUSTOMER_ID = ACT_RECONCILIATION_GROUPS.RECON_ENTITY_ID AND "
					+ " ACT_RECONCILIATION_GROUPS.RECON_ENTITY_TYPE = '1' "
					+ " LEFT JOIN MNT_AGENCY_LIST with(nolock) ON "
					+ " MNT_AGENCY_LIST.AGENCY_ID = ACT_RECONCILIATION_GROUPS.RECON_ENTITY_ID AND "
					+ " ACT_RECONCILIATION_GROUPS.RECON_ENTITY_TYPE = '2'";
*/

				
				strSelect =  " ( Select Convert(Varchar,ACT_RECONCILIATION_GROUPS.CREATED_DATETIME,101) CD,ACT_RECONCILIATION_GROUPS.CREATED_DATETIME as CREATEDDATETIME," 
					+ "CASE RECON_ENTITY_TYPE "
					+ " WHEN 'CUST' THEN IsNull(CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME,'') + ' ' + IsNull(CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME,'') + ' ' + IsNull(CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME,'') "
					+ " WHEN 'AGN' THEN IsNull(MNT_AGENCY_LIST.AGENCY_DISPLAY_NAME, '') "
					+ " WHEN 'VEN' THEN IsNull(MNT_VENDOR_LIST.COMPANY_NAME, '') "
					+ " END RECON_ENTITY_ID" 
					+",CASE RECON_ENTITY_TYPE WHEN 'CUST' THEN 'Customer' WHEN 'AGN' THEN 'Agency'  WHEN 'VEN' THEN 'Vendor' END RECON_ENTITY_TYPE,GROUP_ID,";

				strSelect += " IS_COMMITTED,ACT_RECONCILIATION_GROUPS.CREATED_DATETIME FROM ACT_RECONCILIATION_GROUPS with(nolock) "
					+ " LEFT JOIN CLT_CUSTOMER_LIST with(nolock) ON "
					+ " CLT_CUSTOMER_LIST.CUSTOMER_ID = ACT_RECONCILIATION_GROUPS.RECON_ENTITY_ID AND "
					+ " ACT_RECONCILIATION_GROUPS.RECON_ENTITY_TYPE = 'CUST' "
					+ " LEFT JOIN MNT_AGENCY_LIST with(nolock) ON "
					+ " MNT_AGENCY_LIST.AGENCY_ID = ACT_RECONCILIATION_GROUPS.RECON_ENTITY_ID AND "
					+ " ACT_RECONCILIATION_GROUPS.RECON_ENTITY_TYPE = 'AGN' "
					+ " LEFT JOIN MNT_VENDOR_LIST with(nolock) ON "
					+ " MNT_VENDOR_LIST.VENDOR_ID = ACT_RECONCILIATION_GROUPS.RECON_ENTITY_ID AND "
					+ " ACT_RECONCILIATION_GROUPS.RECON_ENTITY_TYPE = 'VEN' ) Test";


				objWebGrid.SelectClause = " * ";
				objWebGrid.FromClause = strSelect;

				objWebGrid.WhereClause = "IsNull(IS_COMMITTED,'')<>'Y'";
				
				
				objWebGrid.SearchColumnHeadings = "Created Date^Entity Name^Entity Type";
				//Column cd Added For Itrack Issue #4946 Created Date Sorting .
				objWebGrid.SearchColumnNames = "cd^RECON_ENTITY_ID^RECON_ENTITY_TYPE";
				objWebGrid.SearchColumnType = "D^T^T";
				
				//Column cd Added For Itrack Issue #4946 For Created Date Sorting .
				objWebGrid.OrderByClause = "cd,RECON_ENTITY_ID desc";
				
				objWebGrid.DisplayColumnNumbers = "1^2^3";
				objWebGrid.DisplayColumnNames = "cd^RECON_ENTITY_ID^RECON_ENTITY_TYPE";
				objWebGrid.DisplayColumnHeadings = "Created Date^Entity Name^Entity Type";

				objWebGrid.DisplayTextLength = "20^40^40";
				objWebGrid.DisplayColumnPercent = "20^40^40";
				objWebGrid.PrimaryColumns = "4";
				objWebGrid.PrimaryColumnsName = "GROUP_ID";

				objWebGrid.ColumnTypes = "B^B^B";

				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4";

				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				
				objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.HeaderString = "Reconciliation Group Details";
				objWebGrid.SelectClass = colors;
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.QueryStringColumns = "GROUP_ID";
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(c1);
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
