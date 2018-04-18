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
using System.Resources;
using System.Reflection;

/******************************************************************************************
	<Author					: Vijay Arora- >
	<Start Date				: Oct. 03, 2005-	>
	<End Date				: - >
	<Description			: To be used to display the budget categories - >
	<Review Date			: - >
	<Reviewed By			: - >
*******************************************************************************************/

namespace Cms.CmsWeb.Maintenance.Accounting
{
	/// <summary>
	/// Summary description for BudgetCatgoryIndex.
	/// </summary>
	public class BudgetCatgoryIndex : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;

		//private string strCustomerID, strAppId, strAppVersionId;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        ResourceManager objResourceMgr = null;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="221"; //Added by Sibin on 27 Oct 08
			if (!CanShow())
			{
				capMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("401");
				capMessage.Visible=true;
				return;
			}
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.Accounting.BudgetCatgoryIndex", Assembly.GetExecutingAssembly());

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
				objWebGrid.SelectClause = "CATEGEORY_ID,CATEGEORY_CODE,CATEGORY_DEPARTEMENT_NAME,RESPONSIBLE_EMPLOYEE_NAME,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME";
				objWebGrid.FromClause = "ACT_BUDGET_CATEGORY";
				objWebGrid.WhereClause = " ";
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Code^Budget Category^Employee Name";
				objWebGrid.SearchColumnNames = "CATEGEORY_CODE^CATEGORY_DEPARTEMENT_NAME^RESPONSIBLE_EMPLOYEE_NAME";
				objWebGrid.SearchColumnType = "T^T^T^T^T";
				objWebGrid.OrderByClause = "CATEGEORY_CODE ASC";
				
				objWebGrid.DisplayColumnNumbers = "2^3^4";
				objWebGrid.DisplayColumnNames = "CATEGEORY_CODE^CATEGORY_DEPARTEMENT_NAME^RESPONSIBLE_EMPLOYEE_NAME";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Code^Budget Category^Employee Name";

				objWebGrid.DisplayTextLength = "5^150^150";
				objWebGrid.DisplayColumnPercent = "5^35^35";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "CATEGEORY_ID";

				objWebGrid.ColumnTypes = "B^B^B";

				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3";

                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";

                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
				objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim()+ "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Budget Category";
				objWebGrid.SelectClass = colors;	
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.QueryStringColumns = "CATEGEORY_ID";
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Show Inactive";
				objWebGrid.FilterColumnName="IS_ACTIVE";
				objWebGrid.FilterValue="Y";

				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(c1);
			}
			catch
			{
			}
			#endregion
            TabCtl.TabURLs = "AddBudgetCatgory.aspx??&";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
            TabCtl.TabLength = 150;
		}

		private bool CanShow()
		{
			//Checking whether customer id exits in database or not
			//if (System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToUpper() != GetSystemId().ToUpper() )
            if (CarrierSystemID.ToUpper() != GetSystemId().ToUpper())
			{
				return false;
			}

			return true;
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
