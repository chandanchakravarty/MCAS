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
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb.WebControls;
using System.Reflection;
using System.Resources;  

/******************************************************************************************
	<Author					: - >Sumit Chhabra
	<Start Date				: -	> April 20, 2005
	<End Date				: - >
	<Description			: - >This file is being used for locading grid control to show limits of authority records
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - >
	<Modified By			: - >
	<Purpose				: - >  
	
*******************************************************************************************/

namespace Cms.CmsWeb.Maintenance.Claims
{
	/// <summary>
	/// This class is used for showing grid that search and display agency records
	/// </summary>
	public class LimitsOfAuthorityIndex : Cms.CmsWeb.cmsbase  
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
        ResourceManager objResourceMgr = null;
		#region "web form designer controls declaration"

		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;

		#endregion
		protected System.Web.UI.HtmlControls.HtmlTableRow tabCtlRow;		

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="297";
			// Put user code to initialize the page here
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.Claims.LimitsOfAuthorityIndex", Assembly.GetExecutingAssembly());
			
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
			Control c1= LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{
                
				string sWHERECLAUSE;
                int BaseCurrency = 1;
                if (GetSYSBaseCurrency() == enumCurrencyId.BR)
                    BaseCurrency = 2;
                else
                    BaseCurrency = 1;

				/*************************************************************************/
				///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
				/************************************************************************/
				//specifying webservice URL
				sWHERECLAUSE="";
				((BaseDataGrid)c1).WebServiceURL=httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				//specifying columns for select query
                //((BaseDataGrid)c1).SelectClause = "LIMIT_ID,AUTHORITY_LEVEL,TITLE, substring(convert(varchar(30),convert(money,PAYMENT_LIMIT),1),0,charindex('.',convert(varchar(30),convert(money,PAYMENT_LIMIT),1),0)) PAYMENT_LIMIT , substring(convert(varchar(30),convert(money,RESERVE_LIMIT),1),0,charindex('.',convert(varchar(30),convert(money,RESERVE_LIMIT),1),0)) RESERVE_LIMIT,case when "+ClsCommon.BL_LANG_ID+"=2 then CASE CLAIM_ON_DUMMY_POLICY WHEN 1 THEN 'Sim' WHEN 0 THEN 'N�o' END else  CASE CLAIM_ON_DUMMY_POLICY WHEN 1 THEN 'Yes' WHEN 0 THEN 'No' END end AS CLAIM_ON_DUMMY_POLICY,IS_ACTIVE ";
                //((BaseDataGrid)c1).SelectClause = "LIMIT_ID,AUTHORITY_LEVEL,TITLE,dbo.fun_FormatCurrency(ISNULL(PAYMENT_LIMIT,0)," + BaseCurrency + ") as PAYMENT_LIMIT,dbo.fun_FormatCurrency(ISNULL(RESERVE_LIMIT,0)," + BaseCurrency + ") as RESERVE_LIMIT,case when " + ClsCommon.BL_LANG_ID + "=2 then CASE CLAIM_ON_DUMMY_POLICY WHEN 1 THEN 'Sim' WHEN 0 THEN 'N�o' END else  CASE CLAIM_ON_DUMMY_POLICY WHEN 1 THEN 'Yes' WHEN 0 THEN 'No' END end AS CLAIM_ON_DUMMY_POLICY,IS_ACTIVE ";
                ((BaseDataGrid)c1).SelectClause = "LIMIT_ID,AUTHORITY_LEVEL,TITLE,dbo.fun_FormatCurrency(ISNULL(PAYMENT_LIMIT,0)," + BaseCurrency + ") as PAYMENT_LIMIT,dbo.fun_FormatCurrency(ISNULL(RESERVE_LIMIT,0)," + BaseCurrency + ") as RESERVE_LIMIT,dbo.fun_FormatCurrency(ISNULL(REOPEN_CLAIM_LIMIT,0)," + BaseCurrency + ") as REOPEN_CLAIM_LIMIT,dbo.fun_FormatCurrency(ISNULL(GRATIA_CLAIM_AMOUNT,0)," + BaseCurrency + ") as GRATIA_CLAIM_AMOUNT,case when " + ClsCommon.BL_LANG_ID + "=2 then CASE CLAIM_ON_DUMMY_POLICY WHEN 1 THEN 'Sim' WHEN 0 THEN 'N�o' END else  CASE CLAIM_ON_DUMMY_POLICY WHEN 1 THEN 'Yes' WHEN 0 THEN 'No' END end AS CLAIM_ON_DUMMY_POLICY,IS_ACTIVE ";

				//specifying tables for from clause
				((BaseDataGrid)c1).FromClause=" CLM_AUTHORITY_LIMIT";
				//specifying conditions for where clause
				((BaseDataGrid)c1).WhereClause=sWHERECLAUSE;//" agency_code not in ('"+ System.Configuration.ConfigurationSettings.AppSettings["CarrierSystemID"].ToString()   +  "')";
				//specifying Text to be shown in combo box
                ((BaseDataGrid)c1).SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Authority Level^Title^Payment Limit^Reserve Limit^Claim on Dummy Policy";
				//specifying column to be used for combo box
				//((BaseDataGrid)c1).SearchColumnNames="AUTHORITY_LEVEL^TITLE^PAYMENT_LIMIT^RESERVE_LIMIT^case claim_on_dummy_policy when 1 then 'True' else 'False' end";
                ((BaseDataGrid)c1).SearchColumnNames = objResourceMgr.GetString("SearchColumnNames");
                
                //search column data type specifying data type of the column to be used for combo box
				//((BaseDataGrid)c1).SearchColumnType="T^T^T^T^T";
                ((BaseDataGrid)c1).SearchColumnType = objResourceMgr.GetString("SearchColumnType");

				//specifying column for order by clause
				((BaseDataGrid)c1).OrderByClause="LIMIT_ID asc";

				//specifying column numbers of the query to be displyed in grid
				//((BaseDataGrid)c1).DisplayColumnNumbers="2^3^4^5^6";
                ((BaseDataGrid)c1).DisplayColumnNumbers = objResourceMgr.GetString("DisplayColumnNumbers");

				//specifying column names from the query
				//((BaseDataGrid)c1).DisplayColumnNames="AUTHORITY_LEVEL^TITLE^PAYMENT_LIMIT^RESERVE_LIMIT^CLAIM_ON_DUMMY_POLICY";
                ((BaseDataGrid)c1).DisplayColumnNames = objResourceMgr.GetString("DisplayColumnNames");

				//specifying text to be shown as column headings
                ((BaseDataGrid)c1).DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Authority Level^Title^Payment Limit^Reserve Limit^Claim on Dummy Policy";
				
                //specifying column heading display text length
				//((BaseDataGrid)c1).DisplayTextLength="10^20^20^20^20";
                ((BaseDataGrid)c1).DisplayTextLength = objResourceMgr.GetString("DisplayTextLength");
				
                //specifying width percentage for columns
				//((BaseDataGrid)c1).DisplayColumnPercent="10^20^20^20^20";
                ((BaseDataGrid)c1).DisplayColumnPercent = objResourceMgr.GetString("DisplayColumnPercent");
				
                //specifying primary column number
				((BaseDataGrid)c1).PrimaryColumns="1";
				
                //specifying primary column name
				((BaseDataGrid)c1).PrimaryColumnsName="LIMIT_ID";               
				
                //specifying column type of the data grid
				//((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B";
                ((BaseDataGrid)c1).ColumnTypes = objResourceMgr.GetString("ColumnTypes");
				
                
                //specifying links pages 
                //((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
				//specifying if double click is allowed or not
				((BaseDataGrid)c1).AllowDBLClick="true"; 
				//specifying which columns are to be displayed on first tab
				((BaseDataGrid)c1).FetchColumns="1";
				//specifying message to be shown
                ((BaseDataGrid)c1).SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				//specifying buttons to be displayed on grid
                ((BaseDataGrid)c1).ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
				//specifying number of the rows to be shown
				((BaseDataGrid)c1).PageSize= int.Parse(GetPageSize());//int.Parse(System.Configuration.ConfigurationSettings.AppSettings.Get("GRID_PAGE_SIZE"));
				//specifying cache size (use for top clause)
				((BaseDataGrid)c1).CacheSize=int.Parse(GetCacheSize());//int.Parse(System.Configuration.ConfigurationSettings.AppSettings.Get("GRID_CACHE_SIZE"));
				//specifying image path
                ((BaseDataGrid)c1).ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                ((BaseDataGrid)c1).HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				//specifying heading
                ((BaseDataGrid)c1).HeaderString = objResourceMgr.GetString("HeaderString");//"Limits of Authority";
				((BaseDataGrid)c1).SelectClass = colors;
				//specifying text to be shown for filter checkbox
                ((BaseDataGrid)c1).FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
				//specifying column to be used for filtering
				((BaseDataGrid)c1).FilterColumnName="IS_ACTIVE";
				//value of filtering record
				((BaseDataGrid)c1).FilterValue="Y";
				
//					//specifying text to be shown for filter checkbox
//					((BaseDataGrid)c1).FilterLabel="Include Inactive";
//					//specifying column to be used for filtering
//					((BaseDataGrid)c1).FilterColumnName="AGENCY.IS_ACTIVE";
//					//value of filtering record
//					((BaseDataGrid)c1).FilterValue="Y";

                // Modified by santosh kumar gautam o 17 Feb 2011 
                ((BaseDataGrid)c1).CellHorizontalAlign = "2^3";

				// property indiacating whether query string is required or not
				((BaseDataGrid)c1).RequireQuery ="Y";
				// column numbers to create query string
				((BaseDataGrid)c1).QueryStringColumns ="LIMIT_ID";
				((BaseDataGrid)c1).DefaultSearch="Y";     
                
				// to show completed task we have to give check box
				GridHolder.Controls.Add(c1);				
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
            TabCtl.TabURLs = "LimitsOfAuthorityDetails.aspx?";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
       

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
