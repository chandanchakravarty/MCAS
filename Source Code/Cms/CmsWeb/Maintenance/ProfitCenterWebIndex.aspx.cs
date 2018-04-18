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

/******************************************************************************************
	<Author					: Priya Arora- >
	<Start Date				: Apr 04,2005->
	<End Date				: - >
	<Description			: - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - >
	<Modified By			: - >
	<Purpose				: - >
*******************************************************************************************/

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for ProfitCenterWebIndex.
	/// </summary>
	public class ProfitCenterWebIndex : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Panel pnlGrid;
		protected System.Web.UI.HtmlControls.HtmlForm pcIndex;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
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


			#region loading web grid control
            Control c1= LoadControl("../webcontrols/BaseDataGrid.ascx");
            try
            {
                /*************************************************************************/
                ///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
                /************************************************************************/
                //specifying webservice URL
                ((BaseDataGrid)c1).WebServiceURL="../webservices/BaseDataGridWS.asmx?WSDL";
                //specifying columns for select query
                ((BaseDataGrid)c1).SelectClause ="T.LISTID, T.TOUSERID, T.FROMUSERID, convert(varchar,T.RECDATE,101) as RECDATE, convert(varchar,T.FOLLOWUPDATE,101) as FOLLOWUPDATE, T.LISTTYPEID,T.SUBJECTLINE, T.NOTE, T.SYSTEMFOLLOWUPID, T.PRIORITY, datepart(hh,T.STARTTIME) as starthour, datepart(mi,T.STARTTIME) as startminute, datepart(hh,T.ENDTIME) as endhour, datepart(mi,T.ENDTIME) as endminute,  UL.USER_FNAME+' '+ UL.USER_LNAME  as Name,T.LISTOPEN LISTOPEN,TT.TYPEDESC";
                //specifying tables for from clause
                ((BaseDataGrid)c1).FromClause=" TODOLIST T, MNT_USER_LIST UL, TODOLISTTYPES TT";
                //specifying conditions for where clause
                ((BaseDataGrid)c1).WhereClause=" T.FROMUSERID = UL.USER_ID and T.LISTTYPEID=TT.TYPEID and T.TOUSERID = " + GetUserId();
                //specifying Text to be shown in combo box
                ((BaseDataGrid)c1).SearchColumnHeadings="Subject Line^Record Date^Note^FollowUp Date^Type";
                //specifying column to be used for combo box
                ((BaseDataGrid)c1).SearchColumnNames="T.SUBJECTLINE^T.RECDATE^ T.NOTE^T.FOLLOWUPDATE^TT.TYPEDESC";
                //search column data type specifying data type of the column to be used for combo box
                ((BaseDataGrid)c1).SearchColumnType="T^D^T^D^T";
                //specifying column for order by clause
                ((BaseDataGrid)c1).OrderByClause="T.FROMUSERID asc ";
                //specifying column numbers of the query to be displyed in grid
                ((BaseDataGrid)c1).DisplayColumnNumbers="3^4^7^8^17";
                //specifying column names from the query
                ((BaseDataGrid)c1).DisplayColumnNames="RECDATE^FOLLOWUPDATE^SUBJECTLINE^NOTE^TYPEDESC";
                //specifying text to be shown as column headings
                ((BaseDataGrid)c1).DisplayColumnHeadings="Record Date^Followup Date^Subject Line^Note^Type";
                //specifying column heading display text length
                ((BaseDataGrid)c1).DisplayTextLength="50^50^50^50^75";
                //specifying width percentage for columns
                ((BaseDataGrid)c1).DisplayColumnPercent="15^15^23^24^23";
                //specifying primary column number
                ((BaseDataGrid)c1).PrimaryColumns="1";
                //specifying primary column name
                ((BaseDataGrid)c1).PrimaryColumnsName="T.LISTID";
                //specifying column type of the data grid
                ((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B";
                //specifying links pages 
                //((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
                //specifying if double click is allowed or not
                ((BaseDataGrid)c1).AllowDBLClick="true"; 
                //specifying which columns are to be displayed on first tab
                ((BaseDataGrid)c1).FetchColumns="1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16";
                //specifying message to be shown
                ((BaseDataGrid)c1).SearchMessage="Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                //specifying buttons to be displayed on grid
                ((BaseDataGrid)c1).ExtraButtons="1^Add^0";
                //specifying number of the rows to be shown
                ((BaseDataGrid)c1).PageSize=int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("GRID_PAGE_SIZE"));
                //specifying cache size (use for top clause)
                ((BaseDataGrid)c1).CacheSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("GRID_CACHE_SIZE"));
                //specifying image path
                ((BaseDataGrid)c1).ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                ((BaseDataGrid)c1).HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                //specifying heading
                ((BaseDataGrid)c1).HeaderString = "Diary Information";
                ((BaseDataGrid)c1).SelectClass = colors;
                //specifying text to be shown for filter checkbox
                ((BaseDataGrid)c1).FilterLabel="Show Complete";
                //specifying column to be used for filtering
                ((BaseDataGrid)c1).FilterColumnName="LISTOPEN";
                //value of filtering record
                ((BaseDataGrid)c1).FilterValue="Y";
                // property indiacating whether query string is required or not
                ((BaseDataGrid)c1).RequireQuery ="N";
                // column numbers to create query string
                ((BaseDataGrid)c1).QueryStringColumns ="LISTID^TOUSERID^FROMUSERID";              
                // to show completed task we have to give check box
                GridHolder.Controls.Add(c1);
            }
            catch
            {
				
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
