/******************************************************************************************
	<Author					: Swarup Pal - >
	<Start Date				: 22-Feb-2007 -	>
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
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb.WebControls;
using System.Resources;
using System.Reflection;

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for WordingsIndex.
	/// </summary>
	public class WordingsIndex : Cms.CmsWeb.cmsbase 
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlTableRow tabCtlRow;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        ResourceManager objResourceMgr = null;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="394";
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.WordingsIndex", Assembly.GetExecutingAssembly());
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
			// Put user code to initialize the page here
			#region loading web grid control
			Control c1= LoadControl("../webcontrols/BaseDataGrid.ascx");
			try
			{
                
				/* Check the SystemID of the logged in user.
				 * If the user is not a Wolverine user then display records of that agency ONLY
				 * else the normal flow follows */
				string sWHERECLAUSE="";
//				strSystemID			 = GetSystemId();
//				string strWordingsID = ClsAgency.GetAgencyIDFromCode(strSystemID).ToString();
//				string  strCarrierSystemID = System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString();
//				if ( strSystemID.Trim().ToUpper() == strCarrierSystemID.Trim().ToUpper())
//				{
//					
//					if (sWHERECLAUSE.Trim().Equals(""))
//					{						
//						sWHERECLAUSE = " AGENCY.AGENCY_ID  <> "+ strAgencyID;	
//					}
//					else
//					{
//						sWHERECLAUSE = " AND AGENCY.AGENCY_ID <> "+ strAgencyID;	
//					}
//				
				
					/*************************************************************************/
					///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
					/************************************************************************/
					//specifying webservice URL
					((BaseDataGrid)c1).WebServiceURL="../webservices/BaseDataGridWS.asmx?WSDL";
					//specifying columns for select query
                    ((BaseDataGrid)c1).SelectClause = "WORDINGS_ID,mcsl.STATE_NAME as STATE_DESC,isnull(mntppm.PROCESS_DESC,ppm.PROCESS_DESC) AS PROCESS_DESC,isnull(mlmm.LOB_DESC , mlm.LOB_DESC) AS LOB_DESC,PDF_WORDINGS,mlm.LOB_ID";
					//specifying tables for from clause
					((BaseDataGrid)c1).FromClause=" mnt_process_wordings mpw left outer join mnt_country_state_list mcsl on mcsl.state_id=mpw.state_id left outer join mnt_lob_master mlm on mlm.lob_id=mpw.lob_id left outer join pol_process_master ppm on ppm.process_id=mpw.process_id left join POL_PROCESS_MASTER_MULTILINGUAL mntppm on mntppm.PROCESS_ID =mpw.PROCESS_ID and mntppm.LANG_ID ="+GetLanguageID() +"left join MNT_LOB_MASTER_MULTILINGUAL mlmm on mlmm.LOB_ID =mpw.LOB_ID and mlmm.LANG_ID ="+GetLanguageID();
					//specifying conditions for where clause
					((BaseDataGrid)c1).WhereClause=sWHERECLAUSE;//" agency_code not in ('"+ System.Configuration.ConfigurationSettings.AppSettings["CarrierSystemID"].ToString()   +  "')";
					//specifying Text to be shown in combo box
                    ((BaseDataGrid)c1).SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Wording ID^State Name^LOB^Policy Process^PDF Wordings";
					//specifying column to be used for combo box
                    ((BaseDataGrid)c1).SearchColumnNames = "WORDINGS_ID^mcsl.STATE_NAME^mlm.LOB_ID^isnull(mntppm.PROCESS_DESC,ppm.PROCESS_DESC)^PDF_WORDINGS";
					((BaseDataGrid)c1).DropDownColumns          =   "^^LOB^^";
					//search column data type specifying data type of the column to be used for combo box
					((BaseDataGrid)c1).SearchColumnType="T^T^L^T^T";
					//specifying column for order by clause
					((BaseDataGrid)c1).OrderByClause="WORDINGS_ID asc";
					//specifying column numbers of the query to be displyed in grid
					((BaseDataGrid)c1).DisplayColumnNumbers="1^2^4^3^5";
					//specifying column names from the query
					((BaseDataGrid)c1).DisplayColumnNames="WORDINGS_ID^STATE_DESC^LOB_DESC^PROCESS_DESC^PDF_WORDINGS";
					//specifying text to be shown as column headings
                    ((BaseDataGrid)c1).DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Wording ID^State Name^LOB^Policy Process^PDF Wordings";
					//specifying column heading display text length
					((BaseDataGrid)c1).DisplayTextLength="50^80^80^150^350";
					//specifying width percentage for columns
					((BaseDataGrid)c1).DisplayColumnPercent="7^11^11^21^51";
					//specifying primary column number
					((BaseDataGrid)c1).PrimaryColumns="1";
					//specifying primary column name
					((BaseDataGrid)c1).PrimaryColumnsName="WORDINGS_ID";
					//specifying column type of the data grid
					((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B";
					((BaseDataGrid)c1).QueryStringColumns="WORDINGS_ID";
					//specifying links pages 
					//((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
					((BaseDataGrid)c1).ColumnsLink              =   "AdditionalWordings.aspx";	
					//specifying if double click is allowed or not
					((BaseDataGrid)c1).AllowDBLClick="true"; 
					//specifying which columns are to be displayed on first tab
					((BaseDataGrid)c1).FetchColumns="1^2^3^4^5";
					//specifying message to be shown
                    ((BaseDataGrid)c1).SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
					//specifying buttons to be displayed on grid
                    ((BaseDataGrid)c1).ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add^0^addRecord";
					//specifying number of the rows to be shown
					((BaseDataGrid)c1).PageSize= int.Parse(GetPageSize());//int.Parse(System.Configuration.ConfigurationSettings.AppSettings.Get("GRID_PAGE_SIZE"));
					//specifying cache size (use for top clause)
					((BaseDataGrid)c1).CacheSize=int.Parse(GetCacheSize());//int.Parse(System.Configuration.ConfigurationSettings.AppSettings.Get("GRID_CACHE_SIZE"));
					//specifying image path
                    ((BaseDataGrid)c1).ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    ((BaseDataGrid)c1).HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
					//specifying heading
                    ((BaseDataGrid)c1).HeaderString = objResourceMgr.GetString("HeaderString");//"Additional Wordings";
					((BaseDataGrid)c1).SelectClass = colors;
					//specifying text to be shown for filter checkbox
					//((BaseDataGrid)c1).FilterLabel="Show Complete";
					//specifying column to be used for filtering
					//((BaseDataGrid)c1).FilterColumnName="LISTOPEN";
					//value of filtering record
					//((BaseDataGrid)c1).FilterValue="Y";
					
					//specifying text to be shown for filter checkbox
					//((BaseDataGrid)c1).FilterLabel="Include Inactive";
					//specifying column to be used for filtering
					//((BaseDataGrid)c1).FilterColumnName="mpw.IS_ACTIVE";
					//value of filtering record
					//((BaseDataGrid)c1).FilterValue="Y";
	

					// property indiacating whether query string is required or not
					((BaseDataGrid)c1).RequireQuery ="Y";
					// column numbers to create query string
					((BaseDataGrid)c1).QueryStringColumns ="WORDINGS_ID";
					((BaseDataGrid)c1).DefaultSearch="Y";     
                  
					// to show completed task we have to give check box
					GridHolder.Controls.Add(c1);
//				}
//				else
//				{
//					GridHolder.Visible=false;
//					TabCtl.TabURLs = "AdditionalWordings.aspx?";
//				}
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
       

			#endregion
            TabCtl.TabURLs = "AdditionalWordings.aspx?&";
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
