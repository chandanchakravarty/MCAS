/******************************************************************************************
	<Author					: - > Anurag Verma	
	<Start Date				: -	> 22/04/2005
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


namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for DefaultValueIndex.
	/// </summary>
	public class DefaultValueIndex : Cms.CmsWeb.cmsbase  
	{
		/*
        protected System.Web.UI.WebControls.PlaceHolder plhTabHolder;
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
*/

		protected System.Web.UI.WebControls.Panel pnlGrid;
		protected System.Web.UI.WebControls.Table tblReport;
		protected System.Web.UI.WebControls.Panel pnlReport;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder  plhTabHolder;


		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId	=	"56";
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
				
				
				//Setting web grid control properties
				objWebGrid.WebServiceURL = "../webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = "DEFV_ID,DEFV_ENTITY_NAME,DEFV_VALUE,IS_ACTIVE";
				objWebGrid.FromClause = "MNT_DEFAULT_VALUE_LIST" ;
				objWebGrid.WhereClause = "";//"(IS_ACTIVE <> 'N' OR  IS_ACTIVE IS NULL)" ;
				objWebGrid.SearchColumnHeadings = "ENTITY NAME^VALUE";
				objWebGrid.SearchColumnNames = "DEFV_ENTITY_NAME^DEFV_VALUE";
				objWebGrid.SearchColumnType = "T^T";
				objWebGrid.OrderByClause = "DEFV_ENTITY_NAME asc";
				objWebGrid.DisplayColumnNumbers = "2^3";
				objWebGrid.DisplayColumnNames = "DEFV_ENTITY_NAME^DEFV_VALUE";
				objWebGrid.DisplayColumnHeadings = "ENTITY NAME^VALUE";
				objWebGrid.DisplayTextLength = "435^435";
				objWebGrid.DisplayColumnPercent = "50^50";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "DEFV_ID";
				objWebGrid.ColumnTypes = "B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3";
				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.HeaderString ="Default Values";
				objWebGrid.FilterColumnName = "";
				objWebGrid.FilterValue = "";
				objWebGrid.SelectClass = colors;
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch="Y";
				objWebGrid.QueryStringColumns = "DEFV_ID";

				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}
			#endregion


			#region Window Grid
           /*
			
			// Assinging the variable to be used for making the grid
            // Defining the contains the objectTextGrid literal control
            // These contains will generate the HTML required to generated the 
            // grid object  

            objectWindowsGrid.Text = "<OBJECT id=\"gridObject\" classid=\"" + GetWindowsGridUrl() + "\" VIEWASTEXT>"
                + "<PARAM NAME=\"SelectClause\" VALUE=\"DEFV_ID^DEFV_ENTITY_NAME^DEFV_VALUE^IS_ACTIVE\">"
                + "<PARAM NAME=\"FromClause\" VALUE=\"MNT_DEFAULT_VALUE_LIST\">"
                + "<PARAM NAME=\"WhereClause\" VALUE=\"\">"
                + "<PARAM NAME=\"GroupClause\" VALUE=\"\">"
                + "<PARAM NAME=\"SearchColumnNames\" VALUE=\"DEFV_ENTITY_NAME^DEFV_VALUE\">"
                + "<PARAM NAME=\"SearchColumnHeadings\" VALUE=\"ENTITY NAME^VALUE\">"
                + "<PARAM NAME=\"SearchColumnTypes\" VALUE=\"S^S\">"
                + "<PARAM NAME=\"DisplayColumnNumbers\" VALUE=\"2^3\">"
                + "<PARAM NAME=\"DisplayColumnHeadings\" VALUE=\"ENTITY NAME^VALUE\">"
                + "<PARAM NAME=\"DisplayTextLength\" VALUE=\"400^400\">"
                + "<PARAM NAME=\"PageSize\" VALUE=\"" + GetPageSize() + "\">"
                + "<PARAM NAME=\"ColumnTypes\" VALUE=\"S^S\">"
                + "<PARAM NAME=\"FetchColumns\" VALUE=\"1^2^3\">"
                + "<PARAM NAME=\"PrimaryKeyColumns\" VALUE=\"1\">"
                + "<PARAM NAME=\"GridHeaderText\" VALUE=\"Default Values\">"
                + "<PARAM NAME=\"CacheSize\" VALUE=\"" + GetCacheSize() + "\">"
                + "<PARAM NAME=\"ColorScheme\" VALUE=\"" + GetWindowsGridColor() + "\">"
                + "<PARAM NAME=\"ExtraButtons\" VALUE=\"1^Add New^0\">"
                + "</OBJECT>";
*/
			#endregion
            /*************************************************************************/
            ///////////////  LOADING TAB USER CONTROL ///////////////////////////////	
            /************************************************************************/
            #region loading tab control
            Control lCtrlIFrame = LoadControl("../webcontrols/BaseTabControl.ascx");

            //specifying title heading for different tab    
            ((BaseTabControl)lCtrlIFrame).TabTitles = "Default Value";			                  
            //specifying page to be load on different tab
            ((BaseTabControl)lCtrlIFrame).TabURLs = "AddDefaultValue.aspx?";
            //specifying tab length
            ((BaseTabControl)lCtrlIFrame).TabLength=200;
            //specifying tabcontrol ID
            ((BaseTabControl)lCtrlIFrame).ID="TabCtl";
            plhTabHolder.Controls.Add(lCtrlIFrame);
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
