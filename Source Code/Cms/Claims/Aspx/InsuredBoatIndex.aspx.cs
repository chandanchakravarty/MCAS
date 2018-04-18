/******************************************************************************************
	<Author					: - >Sumit Chhabra
	<Start Date				: -	> May 23, 2006
	<End Date				: - >May 23, 2006
	<Description			: - >This file is being used for loading grid control to show insured boat records
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



namespace Cms.Claims.Aspx
{
	/// <summary>
	/// This class is used for showing grid that search and display agency records
	/// </summary>
	public class InsuredBoatIndex : Cms.Claims.ClaimBase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		#region "web form designer controls declaration"

		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;

		#endregion
		protected System.Web.UI.HtmlControls.HtmlTableRow tabCtlRow;	
		protected string strLOB_ID="";

		private void Page_Load(object sender, System.EventArgs e)
		{
			//Done for Itrack Issue 6553 on 13 Oct 09
			base.ScreenId="306_4";
			//base.ScreenId="300";
			// Put user code to initialize the page here

			
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
                
				GetQueryStringValues();
				string sWHERECLAUSE;

				/*************************************************************************/
				///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
				/************************************************************************/
				//specifying webservice URL
				sWHERECLAUSE=" CLAIM_ID=" + hidCLAIM_ID.Value;
				((BaseDataGrid)c1).WebServiceURL=httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				//specifying columns for select query
				((BaseDataGrid)c1).SelectClause ="CLAIM_ID,BOAT_ID,SERIAL_NUMBER,CASE WHEN [YEAR] = 0 THEN NULL ELSE [YEAR] END AS [YEAR],MAKE,MODEL,BODY_TYPE,LENGTH,WEIGHT,CASE WHEN HORSE_POWER = 0.0 THEN NULL ELSE HORSE_POWER END AS HORSE_POWER,OTHER_HULL_TYPE,PLATE_NUMBER,STATE,CIB.IS_ACTIVE,MLV.LOOKUP_VALUE_DESC AS BODYTYPE,MCSL.STATE_NAME AS STATE_NAME";
				//specifying tables for from clause
				((BaseDataGrid)c1).FromClause=" CLM_INSURED_BOAT CIB LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON CIB.BODY_TYPE=MLV.LOOKUP_UNIQUE_ID LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL ON CIB.STATE=MCSL.STATE_ID";
				//specifying conditions for where clause
				((BaseDataGrid)c1).WhereClause=sWHERECLAUSE;//" agency_code not in ('"+ System.Configuration.ConfigurationSettings.AppSettings["CarrierSystemID"].ToString()   +  "')";
				//specifying Text to be shown in combo box				
				((BaseDataGrid)c1).SearchColumnNames ="SERIAL_NUMBER^YEAR^MAKE^MODEL^MLV.LOOKUP_VALUE_DESC^LENGTH^MCSL.STATE_NAME";
				//specifying column to be used for combo box				
				((BaseDataGrid)c1).SearchColumnHeadings="Serial #^Year^Make^Model^Body Type^Length^State";
				//search column data type specifying data type of the column to be used for combo box				
				((BaseDataGrid)c1).SearchColumnType="T^T^T^T^T^T^T";
				//specifying column for order by clause
				((BaseDataGrid)c1).OrderByClause="BOAT_ID asc";
				//specifying column numbers of the query to be displyed in grid
				((BaseDataGrid)c1).DisplayColumnNumbers="1^2^3^4^5^6^7";
				//specifying column names from the query				
				((BaseDataGrid)c1).DisplayColumnNames="SERIAL_NUMBER^YEAR^MAKE^MODEL^BODYTYPE^LENGTH^STATE_NAME";
				//specifying text to be shown as column headings				
				((BaseDataGrid)c1).DisplayColumnHeadings="Serial #^Year^Make^Model^Body Type^Length^State";
				//specifying column heading display text length
				((BaseDataGrid)c1).DisplayTextLength="15^15^15^15^15^15^15";
				//specifying width percentage for columns
				((BaseDataGrid)c1).DisplayColumnPercent="15^15^15^15^15^15^15";
				//specifying primary column number
				((BaseDataGrid)c1).PrimaryColumns="1";
				//specifying primary column name
				((BaseDataGrid)c1).PrimaryColumnsName="BOAT_ID";
				//specifying column type of the data grid
				((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B^B^B";
				//specifying links pages 
				//((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
				//specifying if double click is allowed or not
				((BaseDataGrid)c1).AllowDBLClick="true"; 
				//specifying which columns are to be displayed on first tab
				((BaseDataGrid)c1).FetchColumns="1^2^3^4^5^6^7^8^9^10^11^12^13^14^15";
				//specifying message to be shown
				((BaseDataGrid)c1).SearchMessage="Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				//specifying buttons to be displayed on grid
				((BaseDataGrid)c1).ExtraButtons="1^Add New^0^addRecord";
				//specifying number of the rows to be shown
				((BaseDataGrid)c1).PageSize= int.Parse(GetPageSize());//int.Parse(System.Configuration.ConfigurationSettings.AppSettings.Get("GRID_PAGE_SIZE"));
				//specifying cache size (use for top clause)
				((BaseDataGrid)c1).CacheSize=int.Parse(GetCacheSize());//int.Parse(System.Configuration.ConfigurationSettings.AppSettings.Get("GRID_CACHE_SIZE"));
				//specifying image path
                ((BaseDataGrid)c1).ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                ((BaseDataGrid)c1).HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				//specifying heading
				((BaseDataGrid)c1).HeaderString = "Insured Boat";
				((BaseDataGrid)c1).SelectClass = colors;
				//specifying text to be shown for filter checkbox
				//((BaseDataGrid)c1).FilterLabel="Show Complete";
				//specifying column to be used for filtering
				//((BaseDataGrid)c1).FilterColumnName="LISTOPEN";
				//value of filtering record
				//((BaseDataGrid)c1).FilterValue="Y";
				
				//specifying text to be shown for filter checkbox
				((BaseDataGrid)c1).FilterLabel="Include Inactive";//Uncommented for Itrack Issue 5833 on 21 July 2009
				//specifying column to be used for filtering
				//((BaseDataGrid)c1).FilterColumnName="AGENCY.IS_ACTIVE";
				((BaseDataGrid)c1).FilterColumnName="CIB.IS_ACTIVE";//Added for Itrack Issue 5833 on 21 July 2009
				//value of filtering record
				((BaseDataGrid)c1).FilterValue="Y";//Uncommented for Itrack Issue 5833 on 21 July 2009


				// property indiacating whether query string is required or not
				((BaseDataGrid)c1).RequireQuery ="Y";
				// column numbers to create query string
				((BaseDataGrid)c1).QueryStringColumns ="BOAT_ID";
				((BaseDataGrid)c1).DefaultSearch="Y";     
                
				// to show completed task we have to give check box
				GridHolder.Controls.Add(c1);	
			
				TabCtl.TabURLs="AddInsuredBoat.aspx?CLAIM_ID=" + hidCLAIM_ID.Value + "&LOB_ID=" + strLOB_ID + "&";
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
       

			#endregion

		}

		private void GetQueryStringValues()
		{
			if(Request.QueryString["CLAIM_ID"]!=null && Request.QueryString["CLAIM_ID"].ToString()!="")
				hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();
			if(Request.QueryString["LOB_ID"]!=null && Request.QueryString["LOB_ID"].ToString()!="" )
				strLOB_ID = Request.QueryString["LOB_ID"].ToString();
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
