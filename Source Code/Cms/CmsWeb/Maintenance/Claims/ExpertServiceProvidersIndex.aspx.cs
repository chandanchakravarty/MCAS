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
	<Author					: - >Anurag Verma
	<Start Date				: -	> March 11, 2005
	<End Date				: - >March 13, 2005
	<Description			: - >This file is being used for locading grid control to show agency records
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - >24/03/2005 
	<Modified By			: - >Anurag Verma
	<Purpose				: - >To change query alias for populateXML
    
	<Modified Date			: - >1/06/2005 
	<Modified By			: - >Anurag Verma
	<Purpose				: - >To change default search to y
	
	<Modified Date			: - >26/07/2005 
	<Modified By			: - >Anurag Verma
	<Purpose				: - >To change searchColumnName property with concatenated address field
*******************************************************************************************/

namespace Cms.CmsWeb.Maintenance.Claims
{
	/// <summary>
	/// This class is used for showing grid that search and display agency records
	/// </summary>
	public class ExpertServiceProvidersIndex : Cms.CmsWeb.cmsbase  
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
			base.ScreenId="300";
			// Put user code to initialize the page here
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.Claims.ExpertServiceProvidersIndex", Assembly.GetExecutingAssembly());
			
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

				/*************************************************************************/
				///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
				/************************************************************************/
				//specifying webservice URL
				sWHERECLAUSE="";
				((BaseDataGrid)c1).WebServiceURL=httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				
                //specifying columns for select query
				//((BaseDataGrid)c1).SelectClause ="EXPERT_SERVICE_ID,EXPERT_SERVICE_TYPE,EXPERT_SERVICE_NAME,(EXPERT_SERVICE_ADDRESS1 + ' ' + EXPERT_SERVICE_ADDRESS2) as Address,EXPERT_SERVICE_CITY,EXPERT_SERVICE_STATE,EXPERT_SERVICE_ZIP,EXPERT_SERVICE_VENDOR_CODE,EXPERT_SERVICE_CONTACT_NAME,EXPERT_SERVICE_CONTACT_PHONE,EXPERT_SERVICE_CONTACT_EMAIL,EXPERT_SERVICE_FEDRERAL_ID,EXPERT_SERVICE_1099_PROCESSING_OPTION,CESP.IS_ACTIVE AS IS_ACTIVE,CTD.DETAIL_TYPE_DESCRIPTION,MLV1.LOOKUP_VALUE_DESC AS PROCESSING_OPTION,MCST.STATE_NAME as State";
                ((BaseDataGrid)c1).SelectClause = "EXPERT_SERVICE_ID,EXPERT_SERVICE_TYPE,EXPERT_SERVICE_NAME,(ISNULL(EXPERT_SERVICE_ADDRESS1,'') + ' ' + ISNULL(EXPERT_SERVICE_ADDRESS2,'')) as Address,EXPERT_SERVICE_CITY,EXPERT_SERVICE_STATE,EXPERT_SERVICE_ZIP,EXPERT_SERVICE_VENDOR_CODE,EXPERT_SERVICE_CONTACT_NAME,EXPERT_SERVICE_CONTACT_PHONE,EXPERT_SERVICE_CONTACT_EMAIL,$DECRYPT_EXPERT_SERVICE_FEDRERAL_ID,EXPERT_SERVICE_1099_PROCESSING_OPTION,CESP.IS_ACTIVE AS IS_ACTIVE,MLV1.LOOKUP_VALUE_DESC AS PROCESSING_OPTION,MCST.STATE_NAME as State, CASE EXPERT_SERVICE_TYPE WHEN " + EXPERT_SERVICE_PROVIDER_TYPE_OTHER.ToString() + " THEN (ISNULL(CTD_M.TYPE_DESC, CTD.DETAIL_TYPE_DESCRIPTION) + ', ' + ISNULL(EXPERT_SERVICE_TYPE_DESC,'')) ELSE ISNULL( CTD_M.TYPE_DESC, CTD.DETAIL_TYPE_DESCRIPTION) END AS EXPERT_TYPE_DESC";
				
                //specifying tables for from clause
                ((BaseDataGrid)c1).FromClause = " CLM_EXPERT_SERVICE_PROVIDERS CESP LEFT OUTER JOIN CLM_TYPE_DETAIL CTD ON CESP.EXPERT_SERVICE_TYPE=CTD.DETAIL_TYPE_ID LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV1 ON CESP.EXPERT_SERVICE_1099_PROCESSING_OPTION=MLV1.LOOKUP_UNIQUE_ID LEFT OUTER JOIN CLM_TYPE_DETAIL_MULTILINGUAL CTD_M ON CTD_M.DETAIL_TYPE_ID = CTD.DETAIL_TYPE_ID and CTD_M.LANG_ID = " + ClsCommon.BL_LANG_ID +" LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCST ON CESP.EXPERT_SERVICE_STATE=MCST.STATE_ID";
				
                //specifying conditions for where clause
				((BaseDataGrid)c1).WhereClause=sWHERECLAUSE;//" agency_code not in ('"+ System.Configuration.ConfigurationSettings.AppSettings["CarrierSystemID"].ToString()   +  "')";
				
                //specifying Text to be shown in combo box
				//((BaseDataGrid)c1).SearchColumnHeadings="Expert Service #^Type^Name^EXPERT_SERVICE_ADDRESS1^EXPERT_SERVICE_ADDRESS2^City^State^Zip^Vendor Code^EXPERT_SERVICE_CONTACT_NAME^EXPERT_SERVICE_CONTACT_PHONE^EXPERT_SERVICE_CONTACT_EMAIL^EXPERT_SERVICE_FEDRERAL_ID^EXPERT_SERVICE_1099_PROCESSING_OPTION";
				/* Modified by Asfa (26-May-2008) - iTrack #4213
				((BaseDataGrid)c1).SearchColumnHeadings="Type^Name^Address^City^State^Zip^Vendor Code^1099 Processing Option";
				*/
                ((BaseDataGrid)c1).SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Type^Name^Address^City^State^Zip^Vendor Code^Federal Id ";
				
                //specifying column to be used for combo box				
				//((BaseDataGrid)c1).SearchColumnNames="EXPERT_SERVICE_ID^EXPERT_SERVICE_TYPE^EXPERT_SERVICE_NAME^EXPERT_SERVICE_ADDRESS1^EXPERT_SERVICE_ADDRESS2^EXPERT_SERVICE_CITY^EXPERT_SERVICE_STATE^EXPERT_SERVICE_ZIP^EXPERT_SERVICE_VENDOR_CODE^EXPERT_SERVICE_CONTACT_NAME^EXPERT_SERVICE_CONTACT_PHONE^EXPERT_SERVICE_CONTACT_EMAIL^EXPERT_SERVICE_FEDRERAL_ID^EXPERT_SERVICE_1099_PROCESSING_OPTION";
				//((BaseDataGrid)c1).SearchColumnNames="DETAIL_TYPE_DESCRIPTION^EXPERT_SERVICE_NAME^EXPERT_SERVICE_ADDRESS1 ! ' ' ! EXPERT_SERVICE_ADDRESS2^EXPERT_SERVICE_CITY^MCST.STATE_NAME^EXPERT_SERVICE_ZIP^EXPERT_SERVICE_VENDOR_CODE^MLV1.LOOKUP_VALUE_DESC";
                //((BaseDataGrid)c1).SearchColumnNames="CASE EXPERT_SERVICE_TYPE WHEN " + EXPERT_SERVICE_PROVIDER_TYPE_OTHER.ToString() + " THEN (ISNULL(CTD.DETAIL_TYPE_DESCRIPTION,'') ! ', ' ! ISNULL(EXPERT_SERVICE_TYPE_DESC,'')) ELSE CTD.DETAIL_TYPE_DESCRIPTION END^EXPERT_SERVICE_NAME^ISNULL(EXPERT_SERVICE_ADDRESS1,'')  ! ISNULL(EXPERT_SERVICE_ADDRESS2,'')^EXPERT_SERVICE_CITY^MCST.STATE_NAME^EXPERT_SERVICE_ZIP^EXPERT_SERVICE_VENDOR_CODE^$DECRYPT_11_EXPERT_SERVICE_FEDRERAL_ID"; // Line Commented by Agniswar for Singapore implementation

                ((BaseDataGrid)c1).SearchColumnNames = "CASE EXPERT_SERVICE_TYPE WHEN " + EXPERT_SERVICE_PROVIDER_TYPE_OTHER.ToString() + " THEN (ISNULL(CTD.DETAIL_TYPE_DESCRIPTION,'') ! ', ' ! ISNULL(EXPERT_SERVICE_TYPE_DESC,'')) ELSE CTD.DETAIL_TYPE_DESCRIPTION END^EXPERT_SERVICE_NAME^ISNULL(EXPERT_SERVICE_ADDRESS1,'')  ! ISNULL(EXPERT_SERVICE_ADDRESS2,'')^";
                ((BaseDataGrid)c1).SearchColumnNames += objResourceMgr.GetString("SearchColumnNames");
				
                //search column data type specifying data type of the column to be used for combo box
				//((BaseDataGrid)c1).SearchColumnType="T^T^T^T^T^T^T^T^T^T^T^T^T^T";
                //((BaseDataGrid)c1).SearchColumnType="T^T^T^T^T^T^T^T"; Line Commented by Agniswar for Singapore implementation
                ((BaseDataGrid)c1).SearchColumnType = objResourceMgr.GetString("SearchColumnType");
				
                //specifying column for order by clause
				((BaseDataGrid)c1).OrderByClause="EXPERT_SERVICE_ID asc";
				
                //specifying column numbers of the query to be displyed in grid
                //((BaseDataGrid)c1).DisplayColumnNumbers="1^2^3^4^6^7^8^11"; Line Commented by Agniswar for Singapore implementation
                ((BaseDataGrid)c1).DisplayColumnNumbers = objResourceMgr.GetString("DisplayColumnNumbers");
				
                //specifying column names from the query
				//((BaseDataGrid)c1).DisplayColumnNames="EXPERT_SERVICE_ID^EXPERT_SERVICE_TYPE^EXPERT_SERVICE_NAME^EXPERT_SERVICE_ADDRESS1^EXPERT_SERVICE_ADDRESS2^EXPERT_SERVICE_CITY^EXPERT_SERVICE_STATE^EXPERT_SERVICE_ZIP^EXPERT_SERVICE_VENDOR_CODE^EXPERT_SERVICE_CONTACT_NAME^EXPERT_SERVICE_CONTACT_PHONE^EXPERT_SERVICE_CONTACT_EMAIL^EXPERT_SERVICE_FEDRERAL_ID^EXPERT_SERVICE_1099_PROCESSING_OPTION";
                //((BaseDataGrid)c1).DisplayColumnNames="EXPERT_TYPE_DESC^EXPERT_SERVICE_NAME^Address^EXPERT_SERVICE_CITY^State^EXPERT_SERVICE_ZIP^EXPERT_SERVICE_CONTACT_PHONE^EXPERT_SERVICE_VENDOR_CODE^EXPERT_SERVICE_FEDRERAL_ID"; Line Commented by Agniswar for Singapore implementation
                ((BaseDataGrid)c1).DisplayColumnNames = objResourceMgr.GetString("DisplayColumnNames");
				
                //specifying text to be shown as column headings
				//((BaseDataGrid)c1).DisplayColumnHeadings="EXPERT_SERVICE_ID^EXPERT_SERVICE_TYPE^EXPERT_SERVICE_NAME^EXPERT_SERVICE_ADDRESS1^EXPERT_SERVICE_ADDRESS2^EXPERT_SERVICE_CITY^EXPERT_SERVICE_STATE^EXPERT_SERVICE_ZIP^EXPERT_SERVICE_VENDOR_CODE^EXPERT_SERVICE_CONTACT_NAME^EXPERT_SERVICE_CONTACT_PHONE^EXPERT_SERVICE_CONTACT_EMAIL^EXPERT_SERVICE_FEDRERAL_ID^EXPERT_SERVICE_1099_PROCESSING_OPTION";
                ((BaseDataGrid)c1).DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Type^Name^Address^City^State^Zip^Contact Phone^Vendor Code^Federal Id";
				
                //specifying column heading display text length
				//((BaseDataGrid)c1).DisplayTextLength="20^50^65^25^25^15^10^20^20"; Line Commented by Agniswar for Singapore implementation
                ((BaseDataGrid)c1).DisplayTextLength = objResourceMgr.GetString("DisplayTextLength");
				
                //specifying width percentage for columns
                //((BaseDataGrid)c1).DisplayColumnPercent="8^17^20^6^7^5^10^12^12"; Line Commented by Agniswar for Singapore implementation
                ((BaseDataGrid)c1).DisplayColumnPercent = objResourceMgr.GetString("DisplayColumnPercent");
				
                //specifying primary column number
				((BaseDataGrid)c1).PrimaryColumns="1";
				
                //specifying primary column name
				((BaseDataGrid)c1).PrimaryColumnsName="EXPERT_SERVICE_ID";
				
                //specifying column type of the data grid
				//((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B^B^B^B^B"; Line Commented by Agniswar for Singapore implementation
                ((BaseDataGrid)c1).ColumnTypes = objResourceMgr.GetString("ColumnTypes");
				
                //specifying links pages 
				//((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
				//specifying if double click is allowed or not
				((BaseDataGrid)c1).AllowDBLClick="true"; 
				
                //specifying which columns are to be displayed on first tab
				//((BaseDataGrid)c1).FetchColumns="1^2^3^4^5^6^7^8^9^10^11^12^13^14^15"; Line Commented by Agniswar for Singapore implementation
                ((BaseDataGrid)c1).FetchColumns = objResourceMgr.GetString("FetchColumns");

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
                ((BaseDataGrid)c1).HeaderString = objResourceMgr.GetString("HeaderString");//"Expert Service Providers";
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
				((BaseDataGrid)c1).FilterColumnName="CESP.IS_ACTIVE";
				
                //value of filtering record
				((BaseDataGrid)c1).FilterValue="Y";


				// property indiacating whether query string is required or not
				((BaseDataGrid)c1).RequireQuery ="Y";
				
                // column numbers to create query string
				((BaseDataGrid)c1).QueryStringColumns ="EXPERT_SERVICE_ID";
				((BaseDataGrid)c1).DefaultSearch="Y";     
                
				// to show completed task we have to give check box
				GridHolder.Controls.Add(c1);				
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
       

			#endregion
            TabCtl.TabURLs = "AddExpertServiceProviders.aspx?" + Request["EXPERT_SERVICE_ID"] + "&";
            TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
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
