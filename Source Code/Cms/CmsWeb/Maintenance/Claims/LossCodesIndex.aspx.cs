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
	public class LossCodesIndex : Cms.CmsWeb.cmsbase  
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
			base.ScreenId="301";
			// Put user code to initialize the page here
            objResourceMgr = new ResourceManager("Cms.CmsWeb.Maintenance.Claims.LossCodesIndex", Assembly.GetExecutingAssembly());
			
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

				sWHERECLAUSE=" CTD.TYPE_ID=5 ";

				((BaseDataGrid)c1).WebServiceURL=httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

				//specifying columns for select query

                ((BaseDataGrid)c1).SelectClause = "LOSS_CODE_ID,CLC.LOB_ID AS LOB_ID,CLC.IS_ACTIVE AS IS_ACTIVE,ISNULL(MLMM.LOB_DESC,MLM.LOB_DESC) AS LOB_DESC, ISNULL(CTDM.TYPE_DESC,CTD.DETAIL_TYPE_DESCRIPTION) AS DESCRIPTION";

				//specifying tables for from clause

                ((BaseDataGrid)c1).FromClause = " CLM_LOSS_CODES CLC JOIN MNT_LOB_MASTER MLM ON CLC.LOB_ID = MLM.LOB_ID JOIN CLM_TYPE_DETAIL CTD ON CLC.LOSS_CODE_TYPE = CTD.DETAIL_TYPE_ID LEFT OUTER JOIN MNT_LOB_MASTER_MULTILINGUAL MLMM ON CLC.LOB_ID=MLMM.LOB_ID AND MLMM.LANG_ID ="+GetLanguageID()+" LEFT OUTER JOIN CLM_TYPE_DETAIL_MULTILINGUAL CTDM ON CLC.LOSS_CODE_TYPE =CTDM.DETAIL_TYPE_ID AND CTDM.LANG_ID="+GetLanguageID();

				//specifying conditions for where clause

				((BaseDataGrid)c1).WhereClause=sWHERECLAUSE;

				//specifying Text to be shown in combo box

                ((BaseDataGrid)c1).SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Product^Loss Codes";

				//specifying column to be used for combo box

				((BaseDataGrid)c1).SearchColumnNames="CLC.LOB_ID^CTD.DETAIL_TYPE_DESCRIPTION";

				//search column data type specifying data type of the column to be used for combo box

				((BaseDataGrid)c1).SearchColumnType="L^T";

				((BaseDataGrid)c1).DropDownColumns  =  "LOB^";

				//specifying column for order by clause

				((BaseDataGrid)c1).OrderByClause="CLC.LOB_ID asc";

				//specifying column numbers of the query to be displyed in grid

				((BaseDataGrid)c1).DisplayColumnNumbers="2^3";

				//specifying column names from the query

				((BaseDataGrid)c1).DisplayColumnNames="LOB_DESC^DESCRIPTION";

				//specifying text to be shown as column headings

                ((BaseDataGrid)c1).DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Product^Loss Codes";

				//specifying column heading display text length

				((BaseDataGrid)c1).DisplayTextLength="100^100";

				//specifying width percentage for columns

				((BaseDataGrid)c1).DisplayColumnPercent="30^50";

				//specifying primary column number

				((BaseDataGrid)c1).PrimaryColumns="1";

				//specifying primary column name

				((BaseDataGrid)c1).PrimaryColumnsName="LOSS_CODE_ID";

				//specifying column type of the data grid

				((BaseDataGrid)c1).ColumnTypes="LBL^B";

				//specifying links pages 

				//((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";

				//specifying if double click is allowed or not

				((BaseDataGrid)c1).AllowDBLClick="true"; 

				//specifying which columns are to be displayed on first tab

				((BaseDataGrid)c1).FetchColumns="1^2^3^4^5^6^7^8^9^10^11^12^13^14^15";

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

                ((BaseDataGrid)c1).HeaderString = objResourceMgr.GetString("HeaderString");//"Loss Codes";

				((BaseDataGrid)c1).SelectClass = colors;

				//specifying text to be shown for filter checkbox

				//((BaseDataGrid)c1).FilterLabel="Show Complete";

				//specifying column to be used for filtering

				//((BaseDataGrid)c1).FilterColumnName="LISTOPEN";

				//value of filtering record

				//((BaseDataGrid)c1).FilterValue="Y";

                        

				//                            //specifying text to be shown for filter checkbox

				//                            ((BaseDataGrid)c1).FilterLabel="Include Inactive";

				//                            //specifying column to be used for filtering

				//                            ((BaseDataGrid)c1).FilterColumnName="AGENCY.IS_ACTIVE";

				//                            //value of filtering record

				//                            ((BaseDataGrid)c1).FilterValue="Y";

 

 

				// property indiacating whether query string is required or not

				((BaseDataGrid)c1).RequireQuery ="Y";

				// column numbers to create query string

				((BaseDataGrid)c1).QueryStringColumns ="LOB_ID";

				((BaseDataGrid)c1).DefaultSearch="Y";     

				//To enable grouping of application number field
				((BaseDataGrid)c1).Grouping                 =	"Y";

				((BaseDataGrid)c1).GroupQueryColumns        =	"CLC.LOB_ID";

                

				// to show completed task we have to give check box

				GridHolder.Controls.Add(c1);                    

			}

			catch(Exception ex)

			{

				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

			}

            TabCtl.TabURLs = "AddLossCodes.aspx??";
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
