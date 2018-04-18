/******************************************************************************************
	<Author					: Gaurav Tyagi- >
	<Start Date				: March 7, 2005-	>
	<End Date				: March 9, 2005- >
	<Description			: - >This file is being used for loading grid control to show UserType records
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
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using System.Resources;
using System.Reflection;
using Cms.BusinessLayer.BlCommon;



namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// This class is used for showing grid that search and display UserType records
	/// </summary>
	public class UserTypeIndex : Cms.CmsWeb.cmsbase
	{
		/*
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
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        ResourceManager objResourceMgr = null;

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId	=	"21";
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.UserTypeIndex", Assembly.GetExecutingAssembly());

			#region GETTING BASE COLOR FOR ROW SELECTION
			string colorScheme=GetColorScheme();
			string colors="";

			switch (int.Parse(colorScheme))
			{
				case 1:
                    colors = System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();     
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
                objWebGrid.SelectClause = "usrType.USER_TYPE_ID,usrType.USER_TYPE_CODE,isnull(MTZ.USER_TYPE_DESC,usrType.USER_TYPE_DESC)USER_TYPE_DESC,usrType.IS_ACTIVE,usrType.USER_TYPE_SYSTEM";
                objWebGrid.FromClause = "MNT_USER_TYPES usrType left outer join MNT_USER_TYPES_MULTILINGUAL MTZ on MTZ.USER_TYPE_ID=usrType.USER_TYPE_ID and LANG_ID="+ ClsCommon.BL_LANG_ID +"";
				objWebGrid.WhereClause = "";//"(IS_ACTIVE <> 'N' OR  IS_ACTIVE IS NULL)" ;
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");// "User Code^User Description";
				objWebGrid.SearchColumnNames = "usrType.USER_TYPE_CODE^usrType.USER_TYPE_DESC";
				objWebGrid.SearchColumnType = "T^T";
				objWebGrid.OrderByClause = "USER_TYPE_CODE asc";
				objWebGrid.DisplayColumnNumbers = "2^6";
				objWebGrid.DisplayColumnNames = "USER_TYPE_CODE^USER_TYPE_DESC";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"User Code^User Description";
				objWebGrid.DisplayTextLength = "435^435";
				objWebGrid.DisplayColumnPercent = "50^50";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "usrType.USER_TYPE_ID";
				objWebGrid.ColumnTypes = "B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";//objResourceMgr.GetString("SearchMessage");
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"User Type"; 
				objWebGrid.FilterColumnName = "";
				objWebGrid.FilterValue = "";
				objWebGrid.SelectClass = colors;
				//specifying text to be shown for filter checkbox
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
				//specifying column to be used for filtering
				objWebGrid.FilterColumnName="IS_ACTIVE";
				//value of filtering record
				objWebGrid.FilterValue="Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch="Y";
				objWebGrid.QueryStringColumns = "USER_TYPE_ID";

				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);
                TabCtl.TabURLs = "AddUserType.aspx??&";
                TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
                //TabCtl.TabTitles ="MCCA Attachment";
                TabCtl.TabLength = 150;
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}
			#endregion



			#region Window control
			/*
			#region "Code for making the grid object and passing the properties to it
			
			//Assinging the variable to be used for making the grid

			//Defining the contains the objectTextGrid literal control
			//These contains will generate the HTML required to generated the 
			//grid object
			litWindowsGrid.Text = "<OBJECT id=\"gridObject\"  classid=\"" + GetWindowsGridUrl() + "\" VIEWASTEXT>"
				+ "<PARAM NAME=\"SelectClause\" VALUE=\"usrType.USER_TYPE_ID as RowId^usrType.USER_TYPE_CODE as UserTypeCode^usrType.USER_TYPE_DESC as UserTypeDescription^usrType.IS_ACTIVE as IsActive^usrType.USER_TYPE_SYSTEM as SystemUser \">"
				+ "<PARAM NAME=\"FromClause\" VALUE=\"MNT_USER_TYPES usrType\">"
				+ "<PARAM NAME=\"WhereClause\" VALUE=\"\">"// (IS_ACTIVE <> 'N' OR IS_ACTIVE IS NULL) \">"
				+ "<PARAM NAME=\"GroupClause\" VALUE=\"\">"
				+ "<PARAM NAME=\"SearchColumnNames\" VALUE=\"usrType.USER_TYPE_CODE^usrType.USER_TYPE_DESC\">"
				+ "<PARAM NAME=\"SearchColumnHeadings\" VALUE=\"User Code^User Description\">"
				+ "<PARAM NAME=\"SearchColumnTypes\" VALUE=\"S^S\">"
				+ "<PARAM NAME=\"DisplayColumnNumbers\" VALUE=\"2^3\">"
				+ "<PARAM NAME=\"DisplayColumnHeadings\" VALUE=\"User Code^User Description\">"
				+ "<PARAM NAME=\"DisplayTextLength\" VALUE=\"150^670\">"
				+ "<PARAM NAME=\"PageSize\" VALUE=\"" + GetPageSize() + "\">"
				+ "<PARAM NAME=\"ColumnTypes\" VALUE=\"S^S^S^S\">"
				+ "<PARAM NAME=\"FetchColumns\" VALUE=\"1^2^3^4^5\">"
				+ "<PARAM NAME=\"PrimaryKeyColumns\" VALUE=\"1\">"
				+ "<PARAM NAME=\"GridHeaderText\" VALUE=\"User Type\">"
				+ "<PARAM NAME=\"CacheSize\" VALUE=\"" + GetCacheSize() + "\">"
				+ "<PARAM NAME=\"ImageColumn\" VALUE=\"\">"
				+ "<PARAM NAME=\"ColorScheme\" VALUE=\"" + GetWindowsGridColor() + "\">"
				+ "<PARAM NAME=\"ExtraButtons\" VALUE=\"1^Add New^0\">"
				+ "</OBJECT>";

			#endregion
			*/
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
