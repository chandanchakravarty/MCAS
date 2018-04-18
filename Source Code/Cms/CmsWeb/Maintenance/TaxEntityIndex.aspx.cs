/******************************************************************************************
<Author				    : -   Priya Arora
<Start Date				: -	  Apr 13,2005
<End Date				: -	
<Description			: - 	
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - 
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
using System.Xml; 
using System.IO;
using Cms.CmsWeb.WebControls;
using System.Resources;
using System.Reflection;
namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// 
	/// </summary>
	public class TaxEntityIndex : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Table tblReport;
		protected System.Web.UI.WebControls.Panel pnlReport;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.Literal litTextGrid;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEntityType;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidEntityId;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		string colors = "";
        ResourceManager objResourceMgr = null;
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId	=	"36";
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.TaxEntityIndex", Assembly.GetExecutingAssembly());	
			if ( !Page.IsPostBack)
			{

				#region GETTING BASE COLOR FOR ROW SELECTION
				
				string colorScheme=GetColorScheme();
				colors="";

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


				BindGrid();
			}

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

		private void BindGrid()
		{
			Control c1= LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{
				/*************************************************************************/
				///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
				/************************************************************************/
				//specifying webservice URL
				((BaseDataGrid)c1).WebServiceURL= httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				
				//specifying columns for select query
				((BaseDataGrid)c1).SelectClause = "t1.TAX_ID,t1.TAX_NAME,t1.TAX_CODE," +
                                "ISNULL(t1.TAX_ADDRESS1,'') + case when t1.TAX_ADDRESS2 !='' then  IsNull(', '+t1.TAX_ADDRESS2,'') else '' end as Address,t1.TAX_CITY,t1.TAX_COUNTRY," + 
								"t1.TAX_STATE,t1.TAX_ZIP,t1.TAX_PHONE,t1.TAX_EXT,t1.TAX_FAX," + 
								"t1.TAX_EMAIL,t1.TAX_WEBSITE,t1.IS_ACTIVE,t1.CREATED_BY," + 
								"t1.CREATED_DATETIME,t1.MODIFIED_BY,t1.LAST_UPDATED_DATETIME ";
				
				//specifying tables for from clause
				((BaseDataGrid)c1).FromClause = " MNT_TAX_ENTITY_LIST t1 ";
											  
				//specifying conditions for where clause
				//((BaseDataGrid)c1).WhereClause = " ATTACH_ENTITY_TYPE = '" + hidEntityType.Value + "' and ATTACH_ENT_ID='" + hidEntityId.Value + "' ";
				
				//specifying Text to be shown in combo box
                ((BaseDataGrid)c1).SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Tax Entity Name^Tax Entity Code^Tax Entity Address^Tax Entity Phone";
				
				//specifying column to be used for combo box
				((BaseDataGrid)c1).SearchColumnNames = "t1.TAX_NAME^t1.TAX_CODE^"+
														"ISNULL(t1.TAX_ADDRESS1,'') ! ' ' !  ISNULL(t1.TAX_ADDRESS2,'')^" + 
														"t1.TAX_PHONE";
				
				//search column data type specifying data type of the column to be used for combo box
				((BaseDataGrid)c1).SearchColumnType="T^T^T^P";
				
				//specifying column for order by clause
				((BaseDataGrid)c1).OrderByClause = " t1.TAX_NAME ASC";
				
				//specifying column numbers of the query to be displyed in grid
				((BaseDataGrid)c1).DisplayColumnNumbers = "2^3^4^10";
				
				//specifying column names from the query
				((BaseDataGrid)c1).DisplayColumnNames = "TAX_NAME^TAX_CODE^ADDRESS^" + 
					"TAX_PHONE" ;	
				
				//specifying text to be shown as column headings
                ((BaseDataGrid)c1).DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Name^Code^Address^Phone";
				
				//specifying column heading display text length
				((BaseDataGrid)c1).DisplayTextLength="50^50^50^50";
				
				//specifying width percentage for columns 
				((BaseDataGrid)c1).DisplayColumnPercent="15^15^23^23";
				
				//specifying primary column number
				((BaseDataGrid)c1).PrimaryColumns="1";
				
				//specifying primary column name
				((BaseDataGrid)c1).PrimaryColumnsName="t1.TAX_ID";
				
				//specifying column type of the data grid
				((BaseDataGrid)c1).ColumnTypes="B^B^B^B";
				
				//specifying links pages 
				//((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
				//specifying if double click is allowed or not
				
				((BaseDataGrid)c1).AllowDBLClick="true"; 
				
				//specifying which columns are to be displayed on first tab
				((BaseDataGrid)c1).FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19";
				
				//specifying message to be shown
                ((BaseDataGrid)c1).SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				
				//specifying buttons to be displayed on grid
                ((BaseDataGrid)c1).ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add^0^addRecord";
				
				//specifying number of the rows to be shown
				((BaseDataGrid)c1).PageSize=int.Parse(GetPageSize());//int.Parse(System.Configuration.ConfigurationSettings.AppSettings.Get("GRID_PAGE_SIZE"));
				
				//specifying cache size (use for top clause)
				((BaseDataGrid)c1).CacheSize=int.Parse(GetCacheSize());//int.Parse(System.Configuration.ConfigurationSettings.AppSettings.Get("GRID_CACHE_SIZE"));
				
				//specifying image path
                ((BaseDataGrid)c1).ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                ((BaseDataGrid)c1).HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				
				//specifying heading
                ((BaseDataGrid)c1).HeaderString = objResourceMgr.GetString("HeaderString");//"Tax Entity Information";
				
				((BaseDataGrid)c1).SelectClass = colors;
				
				//specifying text to be shown for filter checkbox
                ((BaseDataGrid)c1).FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
				//specifying column to be used for filtering
				((BaseDataGrid)c1).FilterColumnName="t1.IS_ACTIVE";
				//value of filtering record
				((BaseDataGrid)c1).FilterValue="Y";
				
				// property indiacating whether query string is required or not
				((BaseDataGrid)c1).RequireQuery ="Y";
				
				// column numbers to create query string
				((BaseDataGrid)c1).QueryStringColumns ="TAX_ID";

				//Default Search
				((BaseDataGrid)c1).DefaultSearch = "Y";

				// to show completed task we have to give check box
				GridHolder.Controls.Add(c1);

				//TabCtl.TabURLs = "AddAttachment.aspx?EntityId=" + hidEntityId.Value + "&EntityType=" + hidEntityType.Value + "&Grid=web&";

			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
            TabCtl.TabURLs = "AddTaxEntity.aspx??&";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
            TabCtl.TabLength = 150;
		}

	}
}