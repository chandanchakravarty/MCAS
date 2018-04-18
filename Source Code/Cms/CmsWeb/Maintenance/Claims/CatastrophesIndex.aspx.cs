/******************************************************************************************
<Author				: -		Vijay Arora
<Start Date			: -		21-04-2006
<End Date			: -	
<Description		: - 	Index Page for Catasphores Events.
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
//using Cms.BusinessLayer.BlCommon;

namespace Cms.CmsWeb.Maintenance.Claims
{

	/// <summary>
	/// 
	/// </summary>
	public class CatastrophesIndex : Cms.CmsWeb.cmsbase
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
        ResourceManager objResourceMgr = null;
		private void Page_Load(object sender, System.EventArgs e)
		{
			#region Setting screen id
				base.ScreenId	=	"299";
			#endregion
                objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.Claims.CatastrophesIndex", Assembly.GetExecutingAssembly());
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
				
				string sSELECTCLAUSE="", sFROMCLAUSE="";
				sSELECTCLAUSE= " * ";
                sFROMCLAUSE = " ( SELECT CATASTROPHE_EVENT_ID, DETAIL_TYPE_DESCRIPTION, CONVERT(VARCHAR(10),DATE_FROM,case when " + ClsCommon.BL_LANG_ID + "=1 then 101 else 103 end) AS DATE_FROM, " +
                " CONVERT(VARCHAR(10),DATE_TO,case when " + ClsCommon.BL_LANG_ID + "=1 then 101 else 103 end) AS DATE_TO, "+
                " DESCRIPTION, CAT_CODE, CLM_CATASTROPHE_EVENT.IS_ACTIVE AS IS_ACTIVE, "+
                " DATE_FROM AS FROM_DATE, DATE_TO AS TO_DATE "+
                " FROM CLM_CATASTROPHE_EVENT LEFT JOIN CLM_TYPE_DETAIL ON CLM_CATASTROPHE_EVENT.CATASTROPHE_EVENT_TYPE = CLM_TYPE_DETAIL.DETAIL_TYPE_ID ) Test ";

				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = sSELECTCLAUSE;
				objWebGrid.FromClause = sFROMCLAUSE ;
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Event Type^Date From^Date To^Description^Category Code";	
                objWebGrid.SearchColumnNames = "DETAIL_TYPE_DESCRIPTION^FROM_DATE^TO_DATE^DESCRIPTION^CAT_CODE";
				objWebGrid.SearchColumnType = "T^D^D^T^T";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"ID #^Event Type^Date From^Date To^Description^Category Code";
				objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6";
				objWebGrid.DisplayColumnNames = "CATASTROPHE_EVENT_ID^DETAIL_TYPE_DESCRIPTION^DATE_FROM^DATE_TO^DESCRIPTION^CAT_CODE";
				objWebGrid.DisplayTextLength = "6^16^11^11^35^16";
				objWebGrid.DisplayColumnPercent = "6^16^11^11^35^16";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "CATASTROPHE_EVENT_ID";
				objWebGrid.ColumnTypes = "B^B^B^B^B^B";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Catastrophe Events" ;
				objWebGrid.OrderByClause = "CATASTROPHE_EVENT_ID asc";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";											
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse (GetCacheSize());
				objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim()+ "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";				 
				objWebGrid.RequireQuery = "Y";
				objWebGrid.QueryStringColumns = "CATASTROPHE_EVENT_ID";
				objWebGrid.DefaultSearch = "Y";
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
				objWebGrid.FilterColumnName = "IS_ACTIVE";
				objWebGrid.FilterValue = "Y";
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);

				TabCtl.TabURLs = "AddCatastropheEvent.aspx?&"; 
				//TabCtl.TabTitles ="Catastrophe Events";
                TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
				TabCtl.TabLength =150;

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