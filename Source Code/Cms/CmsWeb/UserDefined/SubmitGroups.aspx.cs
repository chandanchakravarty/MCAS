/******************************************************************************************
	<Author					: - > Anurag Verma
	<Start Date				: -	> 26/05/2005
	<End Date				: - > 
	<Description			: - > This file is used to implement webgrid control for the user defined group screen module
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
using System.Xml; 
using System.IO;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb;

namespace Cms.CmsWeb.User_Defined
{
	/// <summary>
	/// Summary description for SubmitGroups.
	/// </summary>
	public class SubmitGroups : Cms.CmsWeb.cmsbase  
	{
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected System.Web.UI.WebControls.Label lblTemplate;
        protected System.Web.UI.HtmlControls.HtmlForm SubmitTab;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTemplateID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTabId;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="128_1_1";
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


            string lStrTemplateID = Request["ScreenID"];					
            hidTemplateID.Value = lStrTemplateID;

            string lStrTABID=Request["TabID"];
            hidTabId.Value = lStrTABID;


            #region loading web grid control
            Control c1= LoadControl("../webcontrols/BaseDataGrid.ascx");
            try
            {
                
                /*************************************************************************/
                ///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
                /************************************************************************/
                //specifying webservice URL
                ((BaseDataGrid)c1).WebServiceURL=httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                //specifying columns for select query
                ((BaseDataGrid)c1).SelectClause ="TM.GROUPID,TM.TABID,TM.GROUPNAME,TM.SEQNO,TM.GROUPTYPE,TM.ISACTIVE,TM.CARRIERID,CONVERT(VARCHAR(15),TM.LASTMODIFIEDDATE,101) LASTMODIFIEDDATE,UL.USER_FNAME + '' + UL.USER_LNAME NAME,TM.CLASSID,TM.SUBCLASSID";
                //specifying tables for from clause
                ((BaseDataGrid)c1).FromClause="QUESTIONGROUPMASTER TM LEFT OUTER JOIN MNT_USER_LIST UL ON UL.USER_ID = TM.LASTMODIFIEDBY ";
                //specifying conditions for where clause
				
				//modified by manab adding screenid in where clause
                //((BaseDataGrid)c1).WhereClause=" grouptype='G' and TM.TABID =" +  lStrTABID; 
				((BaseDataGrid)c1).WhereClause=" grouptype='G' and TM.screenid=" + lStrTemplateID + " and TM.TABID =" +  lStrTABID; 

                //specifying Text to be shown in combo box
                ((BaseDataGrid)c1).SearchColumnHeadings="Group Name^Amended On^Amended By";
                //specifying column to be used for combo box
                ((BaseDataGrid)c1).SearchColumnNames="TM.GROUPNAME^TM.LASTMODIFIEDDATE^NAME";
                //search column data type specifying data type of the column to be used for combo box
                ((BaseDataGrid)c1).SearchColumnType="T^D^T";
                //specifying column for order by clause
                ((BaseDataGrid)c1).OrderByClause="GROUPNAME asc";
                //specifying column numbers of the query to be displyed in grid
                ((BaseDataGrid)c1).DisplayColumnNumbers="3^8^9";
                //specifying column names from the query
                ((BaseDataGrid)c1).DisplayColumnNames="GROUPNAME^LASTMODIFIEDDATE^NAME";
                //specifying text to be shown as column headings
                ((BaseDataGrid)c1).DisplayColumnHeadings="Group Name^Amended On^Amended By";
                //specifying column heading display text length
                ((BaseDataGrid)c1).DisplayTextLength="150^50^50";
                //specifying width percentage for columns
                ((BaseDataGrid)c1).DisplayColumnPercent="50^25^25";
                //specifying primary column number
                ((BaseDataGrid)c1).PrimaryColumns="1";
                //specifying primary column name
                ((BaseDataGrid)c1).PrimaryColumnsName="TM.GROUPID";
                //specifying column type of the data grid
                ((BaseDataGrid)c1).ColumnTypes="B^B^B";
                //specifying links pages 
                //((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
                //specifying if double click is allowed or not
                ((BaseDataGrid)c1).AllowDBLClick="true"; 
                //specifying which columns are to be displayed on first tab
                ((BaseDataGrid)c1).FetchColumns="1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22";
                //specifying message to be shown
                ((BaseDataGrid)c1).SearchMessage="Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                //specifying buttons to be displayed on grid
                ((BaseDataGrid)c1).ExtraButtons="4^Add New~Questions~Change Question Order~Back To Tab^0~1~2~3^addRecord~fnSubmitQuestion~fnChangeQuestionOrder~fnMovePrev";
                //specifying number of the rows to be shown
                ((BaseDataGrid)c1).PageSize= int.Parse(GetPageSize());
                //specifying cache size (use for top clause)
                ((BaseDataGrid)c1).CacheSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("GRID_CACHE_SIZE"));
                //specifying image path
                ((BaseDataGrid)c1).ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                ((BaseDataGrid)c1).HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                //specifying heading
                ((BaseDataGrid)c1).HeaderString = "Search Groups";
                ((BaseDataGrid)c1).SelectClass = colors;
                //specifying text to be shown for filter checkbox
                //((BaseDataGrid)c1).FilterLabel="Show Complete";
                //specifying column to be used for filtering
                //((BaseDataGrid)c1).FilterColumnName="LISTOPEN";
                //value of filtering record
                //((BaseDataGrid)c1).FilterValue="Y";
                // property indiacating whether query string is required or not
                ((BaseDataGrid)c1).RequireQuery ="Y";
                // column numbers to create query string
                ((BaseDataGrid)c1).QueryStringColumns ="GROUPID";
                ((BaseDataGrid)c1).DefaultSearch="Y";   
                // to show completed task we have to give check box
				//specifying text to be shown for filter checkbox
				((BaseDataGrid)c1).FilterLabel="Include Inactive";
				//specifying column to be used for filtering
				((BaseDataGrid)c1).FilterColumnName="TM.ISACTIVE";
				//value of filtering record
				((BaseDataGrid)c1).FilterValue="Y";

                GridHolder.Controls.Add(c1);
            }
            catch
            {}
        
            #endregion

            TabCtl.TabURLs = @"GroupDetails.aspx?ScreenID=" +  lStrTemplateID + "&TabID=" + lStrTABID + "&";
                
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
