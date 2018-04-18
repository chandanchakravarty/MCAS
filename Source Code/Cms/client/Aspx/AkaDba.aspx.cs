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
using Cms.BusinessLayer;

/******************************************************************************************
	<Author					: - > Pradeep Iyer
	<Start Date				: -	> Apr 26, 2005
	<End Date				: - >
	<Description			: - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - >
	<Modified By			: - >
	<Purpose				: - >
*******************************************************************************************/

namespace Cms.Client.Aspx
{
	/// <summary>
	/// Summary description for AkaDba.
	/// </summary>
	public class AkaDba : Cms.Client.clientbase
	{
		protected System.Web.UI.WebControls.Literal objectWebGrid;
		protected System.Web.UI.WebControls.Panel pnlGrid;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected System.Web.UI.WebControls.Label lblTemplate;
        protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		string colors = "";
		public string strCalledFrom="";

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Request.QueryString["CalledFrom"] != null && Request.QueryString["CalledFrom"] != "")
			{
				base.ScreenId			= "134_1";//can be Alpha Numeric 
				strCalledFrom = Request.QueryString["CalledFrom"].ToString();  
				TabCtl.TabURLs = "AddAkaDba.aspx?CALLEDFROM=" + strCalledFrom  +"&";  
			}
			else
			{
				base.ScreenId			= "192_1";//can be Alpha Numeric 				
				TabCtl.TabURLs = "AddAkaDba.aspx?CALLEDFROM=&";
			}

			//base.ScreenId ="134_1";
			// Put user code to initialize the page here
			if ( !Page.IsPostBack )
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

				if ( Request.QueryString["Customer_ID"] != null )
				{
					Session["AKACustomerID"] = Request.QueryString["Customer_ID"];
					ViewState["CustomerID"] = Request.QueryString["Customer_ID"];
					BindGrid();
				}

			}

		}
		
		/// <summary>
		/// Sets the parameters for the Web grid control
		/// </summary>
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
				((BaseDataGrid)c1).SelectClause = "CCA.AKADBA_ID, CCA.AKADBA_NAME, MLV1.LOOKUP_VALUE_DESC as AKADBA_TYPE, " + 
												 "MLV2.LOOKUP_VALUE_DESC as LEGAL_ENTITY, " + 
												 "CASE(CCA.AKADBA_NAME_ON_FORM) " + 
													"WHEN 'Y'THEN 'Yes' " +
													"WHEN 'N' THEN 'No'" + 
												 "END as AKADBA_NAME_ON_FORM" + 
												  ", " + 
												 "AKADBA_DISP_ORDER ";
				
				//specifying tables for from clause
				((BaseDataGrid)c1).FromClause = " CLT_CUSTOMER_AKADBA CCA " + 
											  " INNER JOIN MNT_LOOKUP_VALUES MLV1 ON " + 
													" MLV1.LOOKUP_UNIQUE_ID = CCA.AKADBA_TYPE " + 
											  " INNER JOIN MNT_LOOKUP_VALUES MLV2 ON " + 
													" MLV2.LOOKUP_UNIQUE_ID = CCA.AKADBA_LEGAL_ENTITY_CODE " ;
											  
				//specifying conditions for where clause
				((BaseDataGrid)c1).WhereClause = " CCA.CUSTOMER_ID = " +  ViewState["CustomerID"].ToString() + " ";
				
				//specifying Text to be shown in combo box
				((BaseDataGrid)c1).SearchColumnHeadings="Name^Type";
				
				//specifying column to be used for combo box
				((BaseDataGrid)c1).SearchColumnNames="CCA.AKADBA_NAME^MLV1.LOOKUP_VALUE_DESC";
				
				//search column data type specifying data type of the column to be used for combo box
				((BaseDataGrid)c1).SearchColumnType="T^T";
				
				//specifying column for order by clause
				((BaseDataGrid)c1).OrderByClause = "AKADBA_NAME ASC";
				
				//specifying column numbers of the query to be displyed in grid
				((BaseDataGrid)c1).DisplayColumnNumbers="2^3^4^5^6";
				
				//specifying column names from the query
				((BaseDataGrid)c1).DisplayColumnNames="AKADBA_NAME^AKADBA_TYPE^LEGAL_ENTITY^AKADBA_NAME_ON_FORM^AKADBA_DISP_ORDER";
				
				//specifying text to be shown as column headings
				((BaseDataGrid)c1).DisplayColumnHeadings="Name^Type^Legal Entity^Name on Form^Disp Order";
				
				//specifying column heading display text length
				((BaseDataGrid)c1).DisplayTextLength="50^50^50^50^10";
				
				//specifying width percentage for columns
				((BaseDataGrid)c1).DisplayColumnPercent="25^25^20^20^10";
				
				//specifying primary column number
				((BaseDataGrid)c1).PrimaryColumns="1";
				
				//specifying primary column name
				((BaseDataGrid)c1).PrimaryColumnsName="CCA.AKADBA_ID";
				
				//specifying column type of the data grid
				((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B";
				
				//specifying links pages 
				//((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
				//specifying if double click is allowed or not
				
				((BaseDataGrid)c1).AllowDBLClick="true"; 
				
				//specifying which columns are to be displayed on first tab
				((BaseDataGrid)c1).FetchColumns="1^2^3^4^5^6";
				
				//specifying message to be shown
				((BaseDataGrid)c1).SearchMessage="Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				
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
				((BaseDataGrid)c1).HeaderString = "AKA/DBA Information";
				
				((BaseDataGrid)c1).SelectClass = colors;
				
				//specifying text to be shown for filter checkbox
				//((BaseDataGrid)c1).FilterLabel="Show Complete";
				
				//specifying column to be used for filtering
				//((BaseDataGrid)c1).FilterColumnName="LISTOPEN";
				
				//value of filtering record
				//((BaseDataGrid)c1).FilterValue="Y";
				
				// property indiacating whether query string is required or not
				((BaseDataGrid)c1).RequireQuery ="Y";
				((BaseDataGrid)c1).DefaultSearch="Y";
				
				// column numbers to create query string
				((BaseDataGrid)c1).QueryStringColumns ="AKADBA_ID";

				// to show completed task we have to give check box
				GridHolder.Controls.Add(c1);
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
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
