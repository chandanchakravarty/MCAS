/* ***************************************************************************************
   Author		: Pravesh K Chandel
   Creation Date: 9 Aug 2007
   Last Updated : 
   Reviewed By	: 
   Purpose		: This is Grid file used for Excess Layer for a reinsurance Coverage Category
   Comments		: 
   ------------------------------------------------------------------------------------- 
   History	Date	     Modified By		Description
   
   ------------------------------------------------------------------------------------- 
   *****************************************************************************************/

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
using System.Resources;
using System.Reflection;
using Cms.BusinessLayer.BlCommon;
namespace Cms.CmsWeb.Maintenance.Reinsurance.MasterSetup
{
	/// <summary>
	/// Summary description for CoverageCategoriesIndex.
	/// </summary>
	public class CoverageCategoriesIndex :  Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        ResourceManager objResourceMgr = null;
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				base.ScreenId = "400";
                objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.Reinsurance.MasterSetup.CoverageCategoriesIndex", Assembly.GetExecutingAssembly());
		
				#region G E T T I N G   B A S E   C O L O R   F O R   R O W   S E L E C T I O N

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

				# region C O D E   F O R   G R I D   C O N T R O L

				Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");

				BaseDataGrid objWebGrid;
				objWebGrid = (BaseDataGrid)c1;

				//Setting web grid control properties
				objWebGrid.WebServiceURL = "http://" + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				
				//objWebGrid.SelectClause			= "COVERAGE_CATEGORY_ID,EFFECTIVE_DATE,LOB_ID,CATEGORY,ISNULL(IS_ACTIVE,'Y') AS IS_ACTIVE";
                objWebGrid.SelectClause = " COVERAGE_CATEGORY_ID,convert(varchar,EFFECTIVE_DATE,case when " + ClsCommon.BL_LANG_ID + "=1 then 101 else 103 end) as EFFECTIVE_DATE,isnull(mlmm.LOB_DESC,MNT_LOB_MASTER.LOB_DESC ) as LOB_DESC,MRCC.LOB_ID as LOB_ID,dbo.func_ReinsuranceCategory(COVERAGE_CATEGORY_ID) as CATEGORY,ISNULL(MRCC.IS_ACTIVE,'Y') AS IS_ACTIVE";
				objWebGrid.FromClause			= "MNT_REINSURANCE_COVERAGE_CATEGORY MRCC INNER JOIN MNT_LOB_MASTER ON MRCC.LOB_ID = MNT_LOB_MASTER.LOB_ID left outer join MNT_LOB_MASTER_MULTILINGUAL mlmm on mlmm.LOB_ID=MNT_LOB_MASTER.LOB_ID and mlmm.LANG_ID="+GetLanguageID();
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Effective Date^Product^Coverage Category";
				objWebGrid.SearchColumnNames	= "EFFECTIVE_DATE^MRCC.LOB_ID^dbo.func_ReinsuranceCategory(COVERAGE_CATEGORY_ID)";
				objWebGrid.SearchColumnType		= "D^L^T";
				objWebGrid.DropDownColumns          =   "^LOB^";
				objWebGrid.OrderByClause		= "COVERAGE_CATEGORY_ID asc";
				objWebGrid.DisplayColumnNumbers = "2^3^4";
				objWebGrid.DisplayColumnNames	= "EFFECTIVE_DATE^LOB_DESC^CATEGORY";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Effective Date^Product^Coverage Category";
				objWebGrid.DisplayTextLength	= "100^100^100";
				objWebGrid.DisplayColumnPercent = "30^30^40";
				objWebGrid.PrimaryColumns		= "1";
				objWebGrid.PrimaryColumnsName	= "COVERAGE_CATEGORY_ID";

				objWebGrid.ColumnTypes = "B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
				objWebGrid.PageSize =int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Coverage Categories";
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
                objWebGrid.FilterColumnName = "MRCC.IS_ACTIVE";
				objWebGrid.QueryStringColumns ="COVERAGE_CATEGORY_ID";
				objWebGrid.FilterValue="Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch="Y";
			
				
			
				//Adding control to gridholder
				GridHolder.Controls.Add(objWebGrid);

				
				# endregion
			}
			catch(Exception oEx)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(oEx);
			}
            TabCtl.TabURLs = "CoverageCategories.aspx?";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId,"TabCtl");
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
