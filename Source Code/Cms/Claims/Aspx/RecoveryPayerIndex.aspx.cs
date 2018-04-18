/******************************************************************************************
	<Author					: - >Sumit Chhabra
	<Start Date				: -	> July 17, 2006
	<End Date				: - >
	<Description			: - >This file is being used for loading grid control to show payer records
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History
	
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



namespace Cms.CmsWeb.Maintenance.Claims
{
	/// <summary>
	/// This class is used for showing grid that search and display agency records
	/// </summary>
	public class RecoveryPayerIndex : Cms.Claims.ClaimBase  
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidACTIVITY_ID;
		#region "web form designer controls declaration"

		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;

		#endregion
		protected System.Web.UI.HtmlControls.HtmlTableRow tabCtlRow;		

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="309_1_0_3";//Done for Itrack Issue 6553 on 16 Oct 09
			// Put user code to initialize the page here

			
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

			GetQueryStringValues();
			#region loading web grid control
			Control c1= LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{
                
				string sWHERECLAUSE;

				/*************************************************************************/
				///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
				/************************************************************************/
				//specifying webservice URL
				sWHERECLAUSE=" CLAIM_ID = " + hidCLAIM_ID.Value + " AND ACTIVITY_ID = " + hidACTIVITY_ID.Value;
				((BaseDataGrid)c1).WebServiceURL=httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				//specifying columns for select query
				((BaseDataGrid)c1).SelectClause ="CLAIM_ID,ACTIVITY_ID,PAYER_ID,RECOVERY_TYPE,CONVERT(CHAR,RECEIVED_DATE,101) RECEIVED_DATE,RECEIVED_FROM,CHECK_NUMBER,DESCRIPTION,CARP.IS_ACTIVE AS IS_ACTIVE ";
				//specifying tables for from clause
				((BaseDataGrid)c1).FromClause=" CLM_ACTIVITY_RECOVERY_PAYER CARP ";
				//specifying conditions for where clause
				((BaseDataGrid)c1).WhereClause=sWHERECLAUSE;
				//specifying Text to be shown in combo box				
				((BaseDataGrid)c1).SearchColumnHeadings="Received Date^Received From^Cheque #^Description";
				//specifying column to be used for combo box				
				((BaseDataGrid)c1).SearchColumnNames="RECEIVED_DATE^RECEIVED_FROM^CHECK_NUMBER^DESCRIPTION";
				//search column data type specifying data type of the column to be used for combo box				
				((BaseDataGrid)c1).SearchColumnType="T^T^T^T";
				//specifying column for order by clause
				((BaseDataGrid)c1).OrderByClause="PAYER_ID asc";
				//specifying column numbers of the query to be displyed in grid
				((BaseDataGrid)c1).DisplayColumnNumbers="1^2^3^4";
				//specifying column names from the query				
				((BaseDataGrid)c1).DisplayColumnNames="RECEIVED_DATE^RECEIVED_FROM^CHECK_NUMBER^DESCRIPTION";
				//specifying text to be shown as column headings				
				((BaseDataGrid)c1).DisplayColumnHeadings="Received Date^Received From^Cheque #^Description";
				//specifying column heading display text length
				((BaseDataGrid)c1).DisplayTextLength="25^25^25^25";
				//specifying width percentage for columns
				((BaseDataGrid)c1).DisplayColumnPercent="25^25^25^25";
				//specifying primary column number
				((BaseDataGrid)c1).PrimaryColumns="1";
				//specifying primary column name
				((BaseDataGrid)c1).PrimaryColumnsName="PAYER_ID";
				//specifying column type of the data grid
				((BaseDataGrid)c1).ColumnTypes="B^B^B^B";
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
				((BaseDataGrid)c1).HeaderString = "Payer Details";
				((BaseDataGrid)c1).SelectClass = colors;
				//specifying text to be shown for filter checkbox
				//((BaseDataGrid)c1).FilterLabel="Show Complete";
				//specifying column to be used for filtering
				//((BaseDataGrid)c1).FilterColumnName="LISTOPEN";
				//value of filtering record
				//((BaseDataGrid)c1).FilterValue="Y";
				
				//specifying text to be shown for filter checkbox
				((BaseDataGrid)c1).FilterLabel="Include Inactive";
				//specifying column to be used for filtering
				((BaseDataGrid)c1).FilterColumnName="CARP.IS_ACTIVE";
				//value of filtering record
				((BaseDataGrid)c1).FilterValue="Y";


				// property indiacating whether query string is required or not
				((BaseDataGrid)c1).RequireQuery ="Y";
				// column numbers to create query string
				((BaseDataGrid)c1).QueryStringColumns ="PAYER_ID";
				((BaseDataGrid)c1).DefaultSearch="Y";     
                
				// to show completed task we have to give check box
				GridHolder.Controls.Add(c1);	
			

				TabCtl.TabURLs = "AddRecoveryPayer.aspx?CLAIM_ID=" + hidCLAIM_ID.Value + "&ACTIVITY_ID=" + hidACTIVITY_ID.Value + "&";
				TabCtl.TabTitles  = "Payer Details";
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
       

			#endregion

		}

		private void GetQueryStringValues()
		{
			if(Request["CLAIM_ID"]!=null && Request["CLAIM_ID"].ToString()!="")			
				hidCLAIM_ID.Value = Request["CLAIM_ID"].ToString();						
			else	
				hidCLAIM_ID.Value = "";

			if(Request["ACTIVITY_ID"]!=null && Request["ACTIVITY_ID"].ToString()!="")			
				hidACTIVITY_ID.Value = Request["ACTIVITY_ID"].ToString();												
			else	
				hidACTIVITY_ID.Value = "";
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
