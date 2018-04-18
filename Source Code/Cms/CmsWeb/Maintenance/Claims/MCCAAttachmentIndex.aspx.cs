/******************************************************************************************
<Author				: -		Vijay Arora
<Start Date			: -		08-08-2006
<End Date			: -	
<Description		: - 	Index Page for MCCA Attachment Index.
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
	public class MCCAAttachmentIndex : Cms.CmsWeb.cmsbase
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
			base.ScreenId	=	"346";
			#endregion
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.Claims.MCCAAttachmentIndex", Assembly.GetExecutingAssembly());
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
                sFROMCLAUSE = " ( SELECT CONVERT(VARCHAR(10),POLICY_PERIOD_DATE_FROM,case when " + ClsCommon.BL_LANG_ID + "=1 then 101 else 103 end  ) AS POLICY_PERIOD_DATE_FROM, "+
                              "  CONVERT(VARCHAR(10),POLICY_PERIOD_DATE_TO,case when " + ClsCommon.BL_LANG_ID + "=1 then 101 else 103 end  ) AS POLICY_PERIOD_DATE_TO,CONVERT(VARCHAR(10),LOSS_PERIOD_DATE_FROM,case when " + ClsCommon.BL_LANG_ID + "=1 then 101 else 103 end  )AS LOSS_PERIOD_DATE_FROM,CONVERT(VARCHAR(10),LOSS_PERIOD_DATE_TO,case when " + ClsCommon.BL_LANG_ID + "=1 then 101 else 103 end  )AS LOSS_PERIOD_DATE_TO,substring(convert(varchar(30),convert(money,MCCA_ATTACHMENT_POINT),1),0,charindex('.',convert(varchar(30),convert(money,MCCA_ATTACHMENT_POINT),1),0)) MCCA_ATTACHMENT_POINT,MCCA_ATTACHMENT_POINT AS MCCA_ATTACHMENT_POINT_SEARCH,IS_ACTIVE, MCCA_ATTACHMENT_ID, " +
                              "  POLICY_PERIOD_DATE_FROM AS POLICY_PERIOD_FROM_DATE ,POLICY_PERIOD_DATE_TO AS  POLICY_PERIOD_TO_DATE, LOSS_PERIOD_DATE_FROM AS LOSS_PERIOD_FROM_DATE , LOSS_PERIOD_DATE_TO AS LOSS_PERIOD_TO_DATE " +
                              "  FROM CLM_MCCA_ATTACHMENT   ) Test ";

				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = sSELECTCLAUSE;
				objWebGrid.FromClause = sFROMCLAUSE ;
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Policy Period Date From^Policy Period Date To^Loss Period Date From^Loss Period Date To^Attachment Point";	
                objWebGrid.SearchColumnNames = "POLICY_PERIOD_FROM_DATE^POLICY_PERIOD_TO_DATE^LOSS_PERIOD_FROM_DATE^LOSS_PERIOD_TO_DATE^MCCA_ATTACHMENT_POINT_SEARCH";
				objWebGrid.SearchColumnType = "D^D^D^D^T";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Policy Period Date From^Policy Period Date To^Loss Period Date From^Loss Period Date To^Attachment Point";	
				objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";
				objWebGrid.DisplayColumnNames = "POLICY_PERIOD_DATE_FROM^POLICY_PERIOD_DATE_TO^LOSS_PERIOD_DATE_FROM^LOSS_PERIOD_DATE_TO^MCCA_ATTACHMENT_POINT";
				objWebGrid.DisplayTextLength = "20^20^20^20^20";
				objWebGrid.DisplayColumnPercent = "20^20^20^20^20";
				objWebGrid.PrimaryColumns = "7";
				objWebGrid.PrimaryColumnsName = "MCCA_ATTACHMENT_ID";
				objWebGrid.ColumnTypes = "B^B^B^B^B";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"MCCA Attachment" ;
				objWebGrid.OrderByClause = "MCCA_ATTACHMENT_ID asc";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";											
				objWebGrid.PageSize = int.Parse (GetPageSize());				
				objWebGrid.CellHorizontalAlign="4^4^4^4";
				objWebGrid.CacheSize = int.Parse (GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";				 
				objWebGrid.RequireQuery = "Y";
				objWebGrid.QueryStringColumns = "MCCA_ATTACHMENT_ID";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.FilterColumnName = "IS_ACTIVE";
				objWebGrid.FilterValue = "Y";
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);

				TabCtl.TabURLs = "AddMCCAAttachment.aspx?&";
                TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
				//TabCtl.TabTitles ="MCCA Attachment";
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