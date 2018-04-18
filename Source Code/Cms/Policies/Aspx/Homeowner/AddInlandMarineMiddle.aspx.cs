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
using Cms.BusinessLayer.BlApplication;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Application.HomeOwners;
using Cms.Model.Application;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using System.Xml;
using System.Text;

namespace Policies.Aspx.Homeowner
{
	/// <summary>
	/// Summary description for AddInlandMarineMiddle.
	/// </summary>-
	public class AddInlandMarineMiddle : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAddNew;
		public string strItemID = "0";
		protected System.Web.UI.HtmlControls.HtmlForm Form1;			
		string colors="";
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		private void Page_Load(object sender, System.EventArgs e)
		{	
			#region Setting ScreenId
				base.ScreenId="236_0";			
			#endregion
			
			
			HttpCookie cookie = Request.Cookies["CovgID"];
			if (cookie != null)
			{
				strItemID = cookie.Value.ToString();	
				cookie.Value = "0";
			}

			HttpCookie cookiePK_ITEM = new HttpCookie("PK_ITEM","0");

			#region GETTING BASE COLOR FOR ROW SELECTION
			string colorScheme=GetColorScheme();
	
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

			BindGrid(strItemID);
			TabCtl.TabURLs = "AddInlandMarineBOTTOM.aspx?ITEM_ID=" + strItemID + "&CalledFrom=Home&";		
			
		}

		private void BindGrid(string strItemID)
		{			
			//SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,ITEM_INSURING_VALUE),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,ITEM_INSURING_VALUE),1),0)) ITEM_INSURING_VALUE_FORMAT 
			Control c1= LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{
				//specifying webservice URL
				((BaseDataGrid)c1).WebServiceURL= httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				
				//specifying columns for select query
				((BaseDataGrid)c1).SelectClause = "CUSTOMER_ID,POL_ID,POL_VERSION_ID,ITEM_ID,ITEM_DETAIL_ID,ITEM_NUMBER,ITEM_DESCRIPTION,ITEM_SERIAL_NUMBER,ITEM_INSURING_VALUE,ITEM_APPRAISAL_BILL,ITEM_PICTURE_ATTACHED,SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(ITEM_INSURING_VALUE,0)),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(ITEM_INSURING_VALUE,0)),1),0)) ITEM_INSURING_VALUE_FORMAT,IS_ACTIVE";
				
				//specifying tables for from clause
				((BaseDataGrid)c1).FromClause = " POL_HOME_OWNER_SCH_ITEMS_CVGS_DETAILS";
											  
				//specifying conditions for where clause
				((BaseDataGrid)c1).WhereClause = " CUSTOMER_ID = " + GetCustomerID() + " AND POL_ID = " + GetPolicyID() + " AND POL_VERSION_ID = " + GetPolicyVersionID() + " AND ITEM_ID = " + strItemID; 
				
				//specifying Text to be shown in combo box
				((BaseDataGrid)c1).SearchColumnHeadings = "Item #^Description^Insuring Value";
				
				//specifying column to be used for combo box
				((BaseDataGrid)c1).SearchColumnNames = "ITEM_NUMBER^ITEM_DESCRIPTION^ITEM_INSURING_VALUE";
				
				//search column data type specifying data type of the column to be used for combo box
				((BaseDataGrid)c1).SearchColumnType="T^T^T";
				
				//specifying column for order by clause
				((BaseDataGrid)c1).OrderByClause = "ITEM_ID, ITEM_DETAIL_ID ASC ";
				
				//specifying column numbers of the query to be displyed in grid
				//((BaseDataGrid)c1).DisplayColumnNumbers = "2^3^4^7^5";
				((BaseDataGrid)c1).DisplayColumnNumbers = "6^7^11";
				
				//specifying column names from the query
				//((BaseDataGrid)c1).DisplayColumnNames = "DWELLING_NUMBER^LOC_NUM^SUB_LOC_NUMBER^Address^YEAR_BUILT^PURCHASE_YEAR";
				((BaseDataGrid)c1).DisplayColumnNames = "ITEM_NUMBER^ITEM_DESCRIPTION^ITEM_INSURING_VALUE_FORMAT";
				
				//specifying text to be shown as column headings
				//((BaseDataGrid)c1).DisplayColumnHeadings = "Dwelling #^Location #^Sublocation #^Address^Year Built^Purchase Year";
				((BaseDataGrid)c1).DisplayColumnHeadings = "Item #^Description^Insuring Value";
				
				//specifying column heading display text length
				//((BaseDataGrid)c1).DisplayTextLength="10^10^15^40^10^20";
				((BaseDataGrid)c1).DisplayTextLength="20^50^30";
				
				//specifying width percentage for columns
				//((BaseDataGrid)c1).DisplayColumnPercent="10^10^15^40^10^20";
				((BaseDataGrid)c1).DisplayColumnPercent="20^50^30";
				
				//specifying primary column number
				((BaseDataGrid)c1).PrimaryColumns="5";
				
				//specifying primary column name
				((BaseDataGrid)c1).PrimaryColumnsName="ITEM_DETAIL_ID";
				
					
				((BaseDataGrid)c1).ColumnTypes="B^B^B";
				
					
				((BaseDataGrid)c1).AllowDBLClick="true"; 
				
				//specifying which columns are to be displayed on first tab
				((BaseDataGrid)c1).FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12";
				
				//specifying message to be shown
				((BaseDataGrid)c1).SearchMessage="Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				
				//((BaseDataGrid)c1).ExtraButtons = "2^Add~Delete^0~1";

				//specifying buttons to be displayed on grid
				((BaseDataGrid)c1).ExtraButtons="1^Add^0^addRecord";
				
				
				//specifying number of the rows to be shown
				((BaseDataGrid)c1).PageSize=int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("GRID_PAGE_SIZE"));
				
				//specifying cache size (use for top clause)
                ((BaseDataGrid)c1).CacheSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("GRID_CACHE_SIZE"));
				
				//specifying image path
                ((BaseDataGrid)c1).ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                ((BaseDataGrid)c1).HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				
				//specifying heading
				((BaseDataGrid)c1).HeaderString = "Scheduled Items / Coverages Details";
				
				((BaseDataGrid)c1).SelectClass = colors;
				
				//specifying text to be shown for filter checkbox
				((BaseDataGrid)c1).FilterLabel="Include Inactive";
				
				//specifying column to be used for filtering
				((BaseDataGrid)c1).FilterColumnName="IS_ACTIVE";
				
				//value of filtering record
				((BaseDataGrid)c1).FilterValue="Y";
				
				// property indiacating whether query string is required or not
				((BaseDataGrid)c1).RequireQuery ="Y";
				
				// column numbers to create query string
				((BaseDataGrid)c1).QueryStringColumns ="ITEM_DETAIL_ID";
				((BaseDataGrid)c1).DefaultSearch = "Y";
				// to show completed task we have to give check box
				GridHolder.Controls.Add(c1);

				//TabCtl.TabURLs = "AddAttachment.aspx?EntityId=" + hidEntityId.Value + "&EntityType=" + hidEntityType.Value + "&Grid=web&";

			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		}
				
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
	}
		
}

