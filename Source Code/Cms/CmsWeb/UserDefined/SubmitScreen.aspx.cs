/******************************************************************************************
	<Author					: - > Anurag Verma
	<Start Date				: -	> 25/05/2005
	<End Date				: - > 
	<Description			: - > This file is used to implement webgrid control for the user defined screen module
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
using System.Resources;
using System.Reflection;
using Cms.BusinessLayer.BlCommon;
namespace Cms.CmsWeb.User_Defined
{
	/// <summary>
	/// Summary description for SubmitScreen.
	/// </summary>
	public class SubmitScreen : Cms.CmsWeb.cmsbase  
	{
        protected System.Web.UI.WebControls.Panel pnlGrid;
        protected System.Web.UI.WebControls.Table tblReport;
        protected System.Web.UI.WebControls.Panel pnlReport;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
        protected System.Web.UI.WebControls.PlaceHolder GridHolder;
        protected System.Web.UI.WebControls.Label lblTemplate;
        protected System.Web.UI.WebControls.Label capMessage;
        protected System.Web.UI.HtmlControls.HtmlForm indexForm;		// Stores the RGB value for grid Base
        protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
        protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
        ResourceManager objResourceMgr = null;
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="128";
            objResourceMgr = new ResourceManager("Cms.CmsWeb.User_Defined.SubmitScreen", Assembly.GetExecutingAssembly());
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


            
           // int customer_ID=GetCustomerID()==""?0:int.Parse(GetCustomerID());
            
           // if(customer_ID!=0)
           // {
         
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
                    ((BaseDataGrid)c1).SelectClause ="SM.SCREENID,SM.SCREENNAME,SM.ISACTIVE,CONVERT(VARCHAR(15),SM.LASTMODIFIEDDATE,case when "+ClsCommon.BL_LANG_ID+"=2 then 103 else 101 end) LASTMODIFIEDON,(UL.USER_FNAME + ' ' + UL.USER_LNAME) As NAME";
                    //specifying tables for from clause
                    ((BaseDataGrid)c1).FromClause="ONLINESCREENMASTER SM LEFT OUTER JOIN MNT_USER_LIST UL ON UL.USER_ID=SM.LASTMODIFIEDBY ";
                    //specifying conditions for where clause
                    ((BaseDataGrid)c1).WhereClause=" SM.SCREENID > 0";
                    //specifying Text to be shown in combo box
                    ((BaseDataGrid)c1).SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Amended On^Screen Name^Amended By";
                    //specifying column to be used for combo box
                    ((BaseDataGrid)c1).SearchColumnNames = "SM.LASTMODIFIEDDATE^SM.SCREENNAME^(UL.USER_FNAME  ! ' ' !  UL.USER_LNAME)";
                    //search column data type specifying data type of the column to be used for combo box
                    ((BaseDataGrid)c1).SearchColumnType="D^T^T";
                    //specifying column for order by clause
                    ((BaseDataGrid)c1).OrderByClause="SCREENID asc";
                    //specifying column numbers of the query to be displyed in grid
                    ((BaseDataGrid)c1).DisplayColumnNumbers="1^2^4^5";
                    //specifying column names from the query
                    ((BaseDataGrid)c1).DisplayColumnNames="SCREENID^SCREENNAME^LASTMODIFIEDON^NAME";
                    //specifying text to be shown as column headings
                    ((BaseDataGrid)c1).DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Screen #^Screen Name^Amended On^Amended By";
                    //specifying column heading display text length
                    ((BaseDataGrid)c1).DisplayTextLength="50^50^50^50";
                    //specifying width percentage for columns
                    ((BaseDataGrid)c1).DisplayColumnPercent="25^25^25^25";
                    //specifying primary column number
                    ((BaseDataGrid)c1).PrimaryColumns="1";
                    //specifying primary column name
                    ((BaseDataGrid)c1).PrimaryColumnsName="SM.SCREENID";
                    //specifying column type of the data grid
                    ((BaseDataGrid)c1).ColumnTypes="B^B^B^B";
                    //specifying links pages 
                    //((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
                    //specifying if double click is allowed or not
                    ((BaseDataGrid)c1).AllowDBLClick="true"; 
                    //specifying which columns are to be displayed on first tab
                    ((BaseDataGrid)c1).FetchColumns="1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22";
                    //specifying message to be shown
                    ((BaseDataGrid)c1).SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                    //specifying buttons to be displayed on grid
                   //manab
					// ((BaseDataGrid)c1).ExtraButtons="5^Add New~Tabs~Change Tab Order~Preview Screen~Copy Screen^0~1~2~3~4^addRecord~fnSubmitTab~fnSubmitTabOrder~fnPreviewScreen~fnCopyScreen";
                    ((BaseDataGrid)c1).ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"5^Add New~Tabs~Change Tab Order~Preview Screen~Field Mapping^0~1~2~3~4^addRecord~fnSubmitTab~fnSubmitTabOrder~fnPreviewScreen~fnMappingScreen";
                    
					//specifying number of the rows to be shown
                    ((BaseDataGrid)c1).PageSize= int.Parse(GetPageSize());
                    //specifying cache size (use for top clause)
                    ((BaseDataGrid)c1).CacheSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("GRID_CACHE_SIZE"));
                    //specifying image path
                    ((BaseDataGrid)c1).ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    ((BaseDataGrid)c1).HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                    //specifying heading
                    ((BaseDataGrid)c1).HeaderString = objResourceMgr.GetString("HeaderString");//"Search Screen";
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
                    ((BaseDataGrid)c1).QueryStringColumns ="SCREENID";
                    ((BaseDataGrid)c1).DefaultSearch="Y";
 
					//specifying text to be shown for filter checkbox
                    ((BaseDataGrid)c1).FilterLabel = objResourceMgr.GetString("FilterLabel");//"Include Inactive";
					//specifying column to be used for filtering
					((BaseDataGrid)c1).FilterColumnName="SM.ISACTIVE";
					//value of filtering record
					((BaseDataGrid)c1).FilterValue="Y";
                  
                    // to show completed task we have to give check box
                    GridHolder.Controls.Add(c1);
                }
                catch
                {}

                TabCtl.TabURLs = "ScreenDetails.aspx??&";
                TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
                TabCtl.TabLength = 150;

                #endregion
                
             /*   int flag=0;
                cltClientTop.CustomerID = int.Parse(GetCustomerID());

                if(GetAppID()!="" && GetAppID()!=null && GetAppID()!="0")
                {
                    cltClientTop.ApplicationID = int.Parse(GetAppID());
                    flag=1;
                }

                if(GetAppVersionID()!="" && GetAppVersionID()!=null && GetAppVersionID()!="0")
                {
                    cltClientTop.AppVersionID = int.Parse(GetAppVersionID());
                    flag=2;
                }
            
                if(flag>0)
                    cltClientTop.ShowHeaderBand ="Application";
                else
                    cltClientTop.ShowHeaderBand ="Client";

                cltClientTop.Visible = true;        
            }
            else
            {
                capMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("115");       
                capMessage.Visible=true; 
            }*/

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
