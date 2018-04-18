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
using Cms.CmsWeb;
using System.Reflection;
using System.Resources;
using Cms.BusinessLayer.BlCommon;
/******************************************************************************************
	<Author					:Priya Arora - >
	<Start Date				:March 08,2005 - >
	<End Date				: - >
	<Description			: To be used for displaying the Profit Centers- >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: May 27, 2005- >
	<Modified By			: Anshuman - >
	<Purpose				: setting screenid according to menuid - >
*******************************************************************************************/

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for ProfitCenterIndex.
	/// </summary>
	public class ProfitCenterIndex : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Table tblReport;
		protected System.Web.UI.WebControls.Panel pnlReport;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        ResourceManager objResourceMgr = null;
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "27";

			#region GETTING BASE COLOR FOR ROW SELECTION
			string colorScheme=GetColorScheme();
			string colors="";
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.ProfitCenterIndex", Assembly.GetExecutingAssembly());

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
			try
			{
				BaseDataGrid objWebGrid;
				objWebGrid = (BaseDataGrid)c1;

				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

                objWebGrid.SelectClause = "PC_ID,PC_CODE,PC_NAME,IsNull(PC_ADD1,'')+' '+IsNull(PC_ADD2,'') As Address,ISNULL(Convert(varchar,LAST_UPDATED_DATETIME,case when " + ClsCommon.BL_LANG_ID + "=1 then 101 else 103 end),'')AS MODIFIEDDATE,PC_CITY,PC_COUNTRY,PC_STATE,PC_ZIP,PC_PHONE,PC_EXT,PC_FAX,PC_EMAIL,MODIFIED_BY,IS_ACTIVE";
                                      
				objWebGrid.FromClause = "MNT_PROFIT_CENTER_LIST";
				//objWebGrid.WhereClause = "";

                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Profit Center Code^Profit Center Name^Profit Center Address^Amended On";
				objWebGrid.SearchColumnNames = "PC_CODE^PC_NAME^IsNull(PC_ADD1,'') ! ' ' ! IsNull(PC_ADD2,'')^LAST_UPDATED_DATETIME";
				objWebGrid.SearchColumnType = "T^T^T^D";

				objWebGrid.OrderByClause = "PC_CODE asc";

				objWebGrid.DisplayColumnNumbers = "2^3^4^5";
				objWebGrid.DisplayColumnNames = "PC_CODE^PC_NAME^Address^MODIFIEDDATE";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Code^Name^Address^Amended On";
				objWebGrid.DisplayTextLength = "150^150^150^250";
				objWebGrid.DisplayColumnPercent = "20^20^20^20";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "PC_ID";

				objWebGrid.ColumnTypes = "B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";objWebGrid.ExtraButtons = "";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
				objWebGrid.PageSize =int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
				objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim()+ "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Profit Center" ;
				objWebGrid.SelectClass = colors ;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
				objWebGrid.FilterColumnName="IS_ACTIVE";
				objWebGrid.FilterValue="Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch="Y";
				objWebGrid.QueryStringColumns = "PC_ID";
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
            TabCtl.TabURLs = "AddProfitCenter.aspx?";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
            TabCtl.TabLength = 150;
			
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

