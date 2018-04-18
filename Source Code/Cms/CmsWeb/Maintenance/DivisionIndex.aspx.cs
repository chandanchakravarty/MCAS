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
using System.Resources;
using Cms.BusinessLayer.BlCommon;
/******************************************************************************************
	<Author					: Ashwani Kumar- >
	<Start Date				: May 10, 2005-	>
	<End Date				: - >
	<Description			: To be used for displaying the Divions - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History
	<Modified Date			: May 27, 2005- >
	<Modified By			: Anshuman - >
	<Purpose				: updating screenid according to menuid - >			
*******************************************************************************************/

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for DivisionIndex.
	/// </summary>
	public class DivisionIndex : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEntityDetail;
        ResourceManager objResourceMgr = null;
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "28";
			// Put user code to initialize the page here
			// Assinging the variable to be used for making the grid
			// Defining the contains the objectTextGrid literal control

			// These contains will generate the HTML required to generated the 
			// grid object
            //objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.DivisionIndex", Assembly.GetExecutingAssembly());
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
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.DivisionIndex", Assembly.GetExecutingAssembly());

			if(colors!="")
			{
				string [] baseColor=colors.Split(new char []{','});  
				if(baseColor.Length>0)
					colors= "#" + baseColor[0];
			}
			#endregion  		
				
				
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("../../Cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{
				//Setting web grid control properties
				objWebGrid.WebServiceURL = "../../Cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                objWebGrid.SelectClause = "Div.DIV_ID ,Div.DIV_CODE as DivisionCode,Div.DIV_NAME as DivisionName,IsNull(Div.DIV_ADD1,'')+' '+IsNull(Div.DIV_ADD2,'') As Address,ISNULL(Convert(varchar,Div.LAST_UPDATED_DATETIME,case when " + ClsCommon.BL_LANG_ID + "=1 then 101 else 103 end),'')AS MODIFIEDDATE,Div.DIV_COUNTRY as Country,Div.DIV_CITY as City,Div.DIV_ZIP as Zip,Div.DIV_PHONE as Phone,Div.DIV_EXT as Extension,Div.DIV_FAX as Fax,Div.DIV_EMAIL as Email,DIV.DIV_ADD1 as Address1,Div.DIV_ADD2 as Address2,Div.IS_ACTIVE as Is_Active,Div.DIV_STATE as State";
				objWebGrid.FromClause = "MNT_DIV_LIST Div";
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Division Code^Division Name^Address^Amended On";
				objWebGrid.SearchColumnNames = "Div.DIV_CODE^Div.DIV_NAME^Div.DIV_ADD1^Div.LAST_UPDATED_DATETIME";
				objWebGrid.SearchColumnType = "T^T^T^D";
				objWebGrid.OrderByClause = "DivisionCode asc";
				objWebGrid.DisplayColumnNumbers = "2^3^4^5";
				objWebGrid.DisplayColumnNames = "DivisionCode^DivisionName^Address^MODIFIEDDATE";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Division Code^Division Name^Address^Amended On";
				objWebGrid.DisplayTextLength = "150^150^350^170";
				objWebGrid.DisplayColumnPercent = "20^20^20^20";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "Div.DIV_ID";
				objWebGrid.ColumnTypes = "B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^3^2^4^5^6^7^8^9^10^11^12^13^14^15^16";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Division Information" ;
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Show Inactive";
				objWebGrid.FilterColumnName = "Is_Active";
				objWebGrid.FilterValue = "Y";
				objWebGrid.DefaultSearch="Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.QueryStringColumns = "DIV_ID";
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}
            TabCtl.TabURLs = "AddDivision.aspx?";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
            TabCtl.TabLength = 150;
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
