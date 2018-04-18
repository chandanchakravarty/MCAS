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
using System.Resources;
using System.Reflection;

/******************************************************************************************
	<Author					: - >Sumit Chhabra
	<Start Date				: -	> April 25, 2005
	<End Date				: - >
	<Description			: - >This file is being used for loading grid control to show Claims Adjuster Authority records
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
	public class ClaimsAdjusterAuthorityIndex : Cms.CmsWeb.cmsbase  
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected int ADJUSTER_ID;
		#region "web form designer controls declaration"

		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;

		#endregion
		protected System.Web.UI.HtmlControls.HtmlTableRow tabCtlRow;
        ResourceManager objResourceMgr = null;
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="298_1";
			// Put user code to initialize the page here
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.Claims.ClaimsAdjusterAuthorityIndex", Assembly.GetExecutingAssembly());
            //Get the Adjuster ID
			ADJUSTER_ID=0; 
			if(Request.QueryString["ADJUSTER_ID"]!=null && Request.QueryString["ADJUSTER_ID"].ToString()!="")
				ADJUSTER_ID = int.Parse(Request.QueryString["ADJUSTER_ID"].ToString());			

			
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

			Control c1= LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");

			try

			{

                

				string sWHERECLAUSE;

 

				/*************************************************************************/

				///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////

				/************************************************************************/

				//specifying webservice URL
                int BaseCurrency = 1;
                if (GetSYSBaseCurrency() == enumCurrencyId.BR)
                    BaseCurrency = 2;
                else
                    BaseCurrency = 1;


				sWHERECLAUSE=" ADJUSTER_ID = " + ADJUSTER_ID.ToString();
                string strEffectiveDate = " CONVERT(VARCHAR(10),EFFECTIVE_DATE,case when " + ClsCommon.BL_LANG_ID + "=2 then 103 else 101 end  ) AS EFFECTIVE_DATE ";

                string SelectClause = " ADJUSTER_AUTHORITY_ID,CAA.LOB_ID LOB_ID,CAA.LIMIT_ID LIMIT_ID," + strEffectiveDate +
                                      " ,CAA.IS_ACTIVE AS IS_ACTIVE,ADJUSTER_ID, "+
                                      " ISNULL(MLB_M.LOB_DESC, MLB.LOB_DESC)AS LOB_DESC, "+
                                      " dbo.fun_FormatCurrency(ISNULL(CAL.PAYMENT_LIMIT,0)," + BaseCurrency + ") AS PAYMENT_LIMIT , " +
                                      " dbo.fun_FormatCurrency(ISNULL(CAL.RESERVE_LIMIT,0)," + BaseCurrency + ") AS RESERVE_LIMIT , " +
                                      " CAL.AUTHORITY_LEVEL AUTHORITY_LEVEL";
                string FromClause =   " CLM_ADJUSTER_AUTHORITY CAA  "+
                                      " LEFT OUTER JOIN MNT_LOB_MASTER MLB ON CAA.LOB_ID = MLB.LOB_ID "+
                                      " LEFT OUTER JOIN MNT_LOB_MASTER_MULTILINGUAL MLB_M ON MLB_M.LOB_ID = MLB.LOB_ID AND MLB_M.LANG_ID=" +ClsCommon.BL_LANG_ID+
                                      " INNER JOIN CLM_AUTHORITY_LIMIT CAL ON CAL.LIMIT_ID = CAA.LIMIT_ID";

				((BaseDataGrid)c1).WebServiceURL=httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

				//specifying columns for select query

                ((BaseDataGrid)c1).SelectClause = SelectClause; //"ADJUSTER_AUTHORITY_ID,CAA.LOB_ID LOB_ID,CAA.LIMIT_ID LIMIT_ID," + strEffectiveDate + " ,CAA.IS_ACTIVE AS IS_ACTIVE,ADJUSTER_ID,MLB.LOB_DESC,substring(convert(varchar(30),convert(money,CAL.PAYMENT_LIMIT),1),0,charindex('.',convert(varchar(30),convert(money,CAL.PAYMENT_LIMIT),1),0)) PAYMENT_LIMIT ,substring(convert(varchar(30),convert(money,CAL.RESERVE_LIMIT),1),0,charindex('.',convert(varchar(30),convert(money,CAL.RESERVE_LIMIT),1),0)) RESERVE_LIMIT ,CAL.AUTHORITY_LEVEL AUTHORITY_LEVEL";

				//specifying tables for from clause

                ((BaseDataGrid)c1).FromClause = FromClause;//" CLM_ADJUSTER_AUTHORITY CAA INNER JOIN MNT_LOB_MASTER MLB ON CAA.LOB_ID = MLB.LOB_ID INNER JOIN CLM_AUTHORITY_LIMIT CAL ON CAL.LIMIT_ID = CAA.LIMIT_ID";

				//specifying conditions for where clause

				((BaseDataGrid)c1).WhereClause=sWHERECLAUSE;//" agency_code not in ('"+ System.Configuration.ConfigurationSettings.AppSettings["CarrierSystemID"].ToString()   +  "')";

				//specifying Text to be shown in combo box

                ((BaseDataGrid)c1).SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings"); //"LOB^Authority Level^Payment Limit^Reserve Limit^Effective Date";

				//specifying column to be used for combo box

				((BaseDataGrid)c1).SearchColumnNames="CAA.LOB_ID^CAL.AUTHORITY_LEVEL^PAYMENT_LIMIT^RESERVE_LIMIT^CAA.EFFECTIVE_DATE";

				//search column data type specifying data type of the column to be used for combo box

				((BaseDataGrid)c1).SearchColumnType="L^T^T^T^D";

				((BaseDataGrid)c1).DropDownColumns  =   "LOB^^^^";

				//specifying column for order by clause

				((BaseDataGrid)c1).OrderByClause="ADJUSTER_AUTHORITY_ID asc";

				//specifying column numbers of the query to be displyed in grid

				((BaseDataGrid)c1).DisplayColumnNumbers="1^2^3^4^5";

				//specifying column names from the query

				((BaseDataGrid)c1).DisplayColumnNames="LOB_DESC^AUTHORITY_LEVEL^PAYMENT_LIMIT^RESERVE_LIMIT^EFFECTIVE_DATE";

				//specifying text to be shown as column headings

                ((BaseDataGrid)c1).DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"LOB^Authority Level^Payment Limit^Reserve Limit^Effective Date";

				//specifying column heading display text length

				((BaseDataGrid)c1).DisplayTextLength="100^100^100^100^100";

				//specifying width percentage for columns

				((BaseDataGrid)c1).DisplayColumnPercent="20^20^20^20^20";

				//specifying primary column number

				((BaseDataGrid)c1).PrimaryColumns="1";

				//specifying primary column name

				((BaseDataGrid)c1).PrimaryColumnsName="ADJUSTER_AUTHORITY_ID";

				//specifying column type of the data grid

				((BaseDataGrid)c1).ColumnTypes="LBL^B^B^B^B";

				//specifying links pages 

				//((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";

				//specifying if double click is allowed or not

				((BaseDataGrid)c1).AllowDBLClick="true"; 

				//specifying which columns are to be displayed on first tab

				((BaseDataGrid)c1).FetchColumns="1^2^3^4^5^6^7^8^9^10^11^12^13^14^15";

				//specifying message to be shown

                ((BaseDataGrid)c1).SearchMessage = objResourceMgr.GetString("SearchMessage"); //"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";

				//specifying buttons to be displayed on grid

                ((BaseDataGrid)c1).ExtraButtons = objResourceMgr.GetString("ExtraButtons");

				//specifying number of the rows to be shown

				((BaseDataGrid)c1).PageSize= int.Parse(GetPageSize());//int.Parse(System.Configuration.ConfigurationSettings.AppSettings.Get("GRID_PAGE_SIZE"));

				//specifying cache size (use for top clause)

				((BaseDataGrid)c1).CacheSize=int.Parse(GetCacheSize());//int.Parse(System.Configuration.ConfigurationSettings.AppSettings.Get("GRID_CACHE_SIZE"));

				//specifying image path

                ((BaseDataGrid)c1).ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";

                ((BaseDataGrid)c1).HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";

				//specifying heading

                ((BaseDataGrid)c1).HeaderString = objResourceMgr.GetString("HeaderString");

                ((BaseDataGrid)c1).CellHorizontalAlign = "2^3"; 

				((BaseDataGrid)c1).SelectClass = colors;

				//specifying text to be shown for filter checkbox

				//((BaseDataGrid)c1).FilterLabel="Show Complete";

				//specifying column to be used for filtering

				//((BaseDataGrid)c1).FilterColumnName="LISTOPEN";

				//value of filtering record

				//((BaseDataGrid)c1).FilterValue="Y";

                        

				//specifying text to be shown for filter checkbox

                ((BaseDataGrid)c1).FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";

				//specifying column to be used for filtering

				((BaseDataGrid)c1).FilterColumnName="CAA.IS_ACTIVE";

				//value of filtering record

				((BaseDataGrid)c1).FilterValue="Y";

 
				//To enable grouping of application number field
				((BaseDataGrid)c1).Grouping                 =	"Y";

				((BaseDataGrid)c1).GroupQueryColumns        =	"CAA.LOB_ID";

 

				// property indiacating whether query string is required or not

				((BaseDataGrid)c1).RequireQuery ="Y";

				// column numbers to create query string

				((BaseDataGrid)c1).QueryStringColumns ="ADJUSTER_AUTHORITY_ID";

				((BaseDataGrid)c1).DefaultSearch="Y";     

                

				// to show completed task we have to give check box

				GridHolder.Controls.Add(c1);                    
				
				TabCtl.TabURLs="AddClaimsAdjusterAuthority.aspx?ADJUSTER_ID=" + ADJUSTER_ID.ToString() 
					+ "&ADJUSTER_NAME=" + Request.QueryString["ADJUSTER_NAME"].ToString()  + "&ADJUSTER_TYPE=" + Request.QueryString["ADJUSTER_TYPE"].ToString()+ "&";
                TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2094");
																									 

			}

			catch(Exception ex)

			{

				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

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
