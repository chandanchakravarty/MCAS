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

/******************************************************************************************
	<Author					: Gaurav- >
	<Start Date				: Aug 26, 2005-	>
	<End Date				: - >
	<Description			: To be used for displaying the Endorsments - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History
	<Modified Date			: - >
	<Modified By			:  - >
	<Purpose				:  - >			
*******************************************************************************************/

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for DivisionIndex.
	/// </summary>
	public class EndorsementIndex : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        ResourceManager objResourceMgr = null;
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "217";
			// Put user code to initialize the page here
			// Assinging the variable to be used for making the grid
			// Defining the contains the objectTextGrid literal control

			// These contains will generate the HTML required to generated the 
			// grid object
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.EndorsementIndex", Assembly.GetExecutingAssembly());
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
				
				
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("../../Cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{
				//Setting web grid control properties
				objWebGrid.WebServiceURL = "../../Cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = "ed.ENDORSMENT_ID ,ed.PURPOSE,ed.DESCRIPTION,ed.TEXT,ed.ENDORS_ASSOC_COVERAGE,ed.SELECT_COVERAGE,ed.IS_ACTIVE,case ed.type when 'M' then 'Mandatory' else 'Optional' end as TYPE,sl.state_name as state,lm.lob_DESC,lm.lob_id ";
				objWebGrid.FromClause = "MNT_ENDORSMENT_DETAILS ed inner join mnt_country_state_list sl on sl.state_id=ed.state_id inner join mnt_lob_master lm on lm.lob_id=ed.lob_id ";
                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"State^LOB^Endorsement Text^Endorsement Description^Type";
				objWebGrid.SearchColumnNames = "sl.STATE_ID^lm.LOB_ID^ed.TEXT^ed.DESCRIPTION^ed.TYPE";
				objWebGrid.DropDownColumns="^LOB^^^";
				objWebGrid.SearchColumnType = "T^T^T^T^T";
				objWebGrid.OrderByClause = "state asc";
				objWebGrid.DisplayColumnNumbers = "9^10^4^3^8";
				objWebGrid.DisplayColumnNames = "state^lob_DESC^TEXT^DESCRIPTION^TYPE";
                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"State^LOB^Endorsement Text^Endorsement Description^Type";
				objWebGrid.DisplayTextLength = "150^150^170^170^170";
				objWebGrid.DisplayColumnPercent = "20^20^20^20^20";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "ed.ENDORSMENT_ID";
				objWebGrid.ColumnTypes = "B^B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
				objWebGrid.PageSize = int.Parse (GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Endorsement Information" ;
				objWebGrid.SelectClass = colors;
                objWebGrid.FilterLabel = objResourceMgr.GetString("FilterLabel");//"Show Inactive";
				objWebGrid.FilterColumnName = "ed.IS_ACTIVE";
				objWebGrid.FilterValue = "Y";
				objWebGrid.DefaultSearch="Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.QueryStringColumns = "ENDORSMENT_ID";
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}
            TabCtl.TabURLs = "AddEndorsment.aspx?&";
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