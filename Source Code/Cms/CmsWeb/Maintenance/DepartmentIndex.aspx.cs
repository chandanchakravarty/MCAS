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
	<Author					: Ashwini Kumar- >
	<Start Date				: March 07, 2005-	>
	<End Date				: - >
	<Description			: - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - >
	<Modified By			: - >
	<Purpose				: - >
*******************************************************************************************/

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for DepartmentIndex.
	/// </summary>
	public class DepartmentIndex : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
        ResourceManager objResourceMgr = null;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		#region "Web form designer controls declaration"

		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;

		#endregion

		#region "Page_Load"
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "29";
			// Put user code to initialize the page here
			// Assinging the variable to be used for making the grid
			// Defining the contains the objectTextGrid literal control

			// These contains will generate the HTML required to generated the 
			// grid object

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
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.DepartmentIndex", Assembly.GetExecutingAssembly());

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
                objWebGrid.SelectClause = "Dept.DEPT_ID ,Dept.DEPT_CODE as DeptCode,Dept.DEPT_NAME as DeptName,IsNull(DEPT.DEPT_ADD1,'')+' '+IsNull(Dept.DEPT_ADD2,'') As Address,ISNULL(Convert(varchar,Dept.LAST_UPDATED_DATETIME,case when " + ClsCommon.BL_LANG_ID + "=1 then 101 else 103 end),'')AS MODIFIEDDATE,Dept.DEPT_CITY as DeptCity,Dept.DEPT_STATE as DeptState,Dept.DEPT_ZIP as DeptZip,Dept.DEPT_COUNTRY as DeptCountry,Dept.DEPT_PHONE as DeptPhone,Dept.DEPT_EXT as DeptExt,Dept.DEPT_FAX as DeptFax,Dept.DEPT_EMAIL as DeptEmail,Dept.IS_ACTIVE ,Dept.DEPT_ADD1 as DeptAdd1,Dept.DEPT_ADD2 as DeptAdd2,Dept.MODIFIED_BY ";
				objWebGrid.FromClause = "mnt_Dept_list Dept" ;						
				//objWebGrid.WhereClause = "";
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Department Code^Department Name^Address^Amended On";
				objWebGrid.SearchColumnNames = "Dept.DEPT_CODE^Dept.DEPT_NAME^IsNull(DEPT.DEPT_ADD1,'')! ' ' ! IsNull(Dept.DEPT_ADD2,'')^Dept.LAST_UPDATED_DATETIME";
				objWebGrid.SearchColumnType = "T^T^T^D";
				objWebGrid.OrderByClause = "DeptCode asc";
				objWebGrid.DisplayColumnNumbers = "2^3^4^5";
				objWebGrid.DisplayColumnNames = "DeptCode^DeptName^Address^MODIFIEDDATE";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Department Code^Department Name^Address^Amended On";
				objWebGrid.DisplayTextLength = "150^150^350^170";
				objWebGrid.DisplayColumnPercent = "20^20^20^20";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "Dept.DEPT_ID";
				objWebGrid.ColumnTypes = "B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^3^2^4^5^6^7^8^9^10^11^12^13^14^15^16^17";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Department Information" ;
				objWebGrid.SelectClass = colors;
				//objWebGrid.FilterLabel = "Show Complete";
				//objWebGrid.FilterColumnName = "";
				//objWebGrid.FilterValue = "";
				//objWebGrid.DefaultSearch="Y";
				//specifying text to be shown for filter checkbox
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
				//specifying column to be used for filtering
				objWebGrid.FilterColumnName="Dept.IS_ACTIVE";
				//value of filtering record
				objWebGrid.FilterValue="Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch ="Y";
				objWebGrid.QueryStringColumns = "DEPT_ID";
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}
            TabCtl.TabURLs = "AddDepartment.aspx?";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
            TabCtl.TabLength = 150;
		}
		
		#endregion

		#region Web form designer generated code
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
