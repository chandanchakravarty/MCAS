/******************************************************************************************
	<Author					:  Aditya Goel
	<Start Date				:   21/12/2010
	<End Date				: - >
	<Description			: - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - >
	<Modified By			: - >
	<Purpose				: - >
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
using Cms.CmsWeb.WebControls;
using System.Reflection;
using System.Resources;


namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for CoverageIndex.
	/// </summary>
    public class MonetaryIndex : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidlocQueryStr;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        ResourceManager objResourceMgr = null;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
		   base.ScreenId = "524_0";
           objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.MonetaryIndex", Assembly.GetExecutingAssembly());
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
				BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("../../Cmsweb/webcontrols/BaseDataGrid.ascx");
               
				try
				{
                    int LangID = int.Parse(GetLanguageID());
                    int BaseCurrency = 1;
                    if (GetSYSBaseCurrency() == enumCurrencyId.BR)
                        BaseCurrency = 2;
                    else
                        BaseCurrency = 1;
                    //int BaseCurrency = 1;
                    //if (GetSYSBaseCurrency() == enumCurrencyId.BR)
                    //    BaseCurrency = 2;
                    //else
                    //    BaseCurrency = 1;


					//Setting web grid control properties
					objWebGrid.WebServiceURL = "../../Cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

                    objWebGrid.SelectClause = " ROW_ID,ISNULL(Convert(varchar,[DATE],case when " + LangID + "=1 then 101 else 103 end),'') AS [DATE], " +
                        " dbo.fun_FormatCurrency (ISNULL(INFLATION_RATE,0)," + BaseCurrency + ") AS  INFLATION_RATE,  " +
                        " dbo.fun_FormatCurrency (ISNULL(INTEREST_RATE,0)," + BaseCurrency + ") AS  INTEREST_RATE,  " +
                        " IS_ACTIVE ";
                    objWebGrid.FromClause = "MNT_MONETORY_INDEX";
                    objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings"); // "DATE^INFLATION_RATE^INTEREST_RATE";

                    objWebGrid.SearchColumnNames = "DATE^INFLATION_RATE^INTEREST_RATE";

					objWebGrid.SearchColumnType = "D^T^T";
                    objWebGrid.OrderByClause = "ROW_ID asc";
					objWebGrid.DisplayColumnNumbers = "1^2^3";
                    objWebGrid.DisplayColumnNames = "DATE^INFLATION_RATE^INTEREST_RATE";
                    objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings"); // "DATE^INFLATION_RATE^INTEREST_RATE";
					objWebGrid.DisplayTextLength = "40^40^20";
                    objWebGrid.DisplayColumnPercent = "40^40^20";
					objWebGrid.PrimaryColumns = "1";
                    objWebGrid.PrimaryColumnsName = "ROW_ID";
					objWebGrid.ColumnTypes = "B^B^B";
					objWebGrid.AllowDBLClick = "true";
					objWebGrid.FetchColumns = "1^2^3^4^5";
                    objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage"); //"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button"; // RESOURCE 
					
					objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
					objWebGrid.PageSize = int.Parse (GetPageSize());
					objWebGrid.CacheSize = int.Parse(GetCacheSize());
                    objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");  // MONETARY
                   objWebGrid.QueryStringColumns = "ROW_ID";
					objWebGrid.RequireQuery="Y";
					objWebGrid.DefaultSearch="Y";
					
					objWebGrid.SelectClass = colors;
					GridHolder.Controls.Add(objWebGrid);
					TabCtl.TabURLs = "AddMonetaryDetails.aspx?";
                    TabCtl.TabTitles = objResourceMgr.GetString("TabTitles"); // Add Monetary Details 
				}
				catch(Exception objExcep)
				{
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
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
