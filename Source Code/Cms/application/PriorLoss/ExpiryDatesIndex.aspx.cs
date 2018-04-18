/******************************************************************************************
	<Author					: - > Anurag Verma
	<Start Date				: -	> 27/04/2005
	<End Date				: - > 
	<Description			: - > This file is used to implement webgrid control for the expirydates module
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

namespace Cms.Application.PriorLoss
{
	/// <summary>
	/// Summary description for ExpiryDatesIndex.
	/// </summary>
	public class ExpiryDatesIndex : Cms.Application.appbase  
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
    
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId = "119";
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


  
            int customer_ID=GetCustomerID()==""?0:int.Parse(GetCustomerID());

            if(customer_ID!=0)
            {
            #region loading web grid control
            Control c1= LoadControl("../../cmsweb/webcontrols/BaseDataGrid.ascx");
            try
            {
                /*************************************************************************/
                ///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
                /************************************************************************/
                //specifying webservice URL
                ((BaseDataGrid)c1).WebServiceURL=httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                //specifying columns for select query
                ((BaseDataGrid)c1).SelectClause ="ED.CUSTOMER_ID,ED.EXPDT_ID,ED.EXPDT_LOB,ED.EXPDT_CARR," +
                                                  "CONVERT(VARCHAR(15),ED.EXPDT_DATE,101)EXPDT_DATE,convert(varchar(15),ED.EXPDT_PREM,1) EXPDT_PREM,CONVERT(VARCHAR(15)," +
                                                  "ED.EXPDT_CONT_DATE,101) EXPDT_CONT_DATE,ED.EXPDT_CSR,ED.EXPDT_PROD,ED.EXPDT_NOTES,ED.POLICY_NUMBER," +
                                                  "CONVERT(VARCHAR(15),ED.EFF_DATE,101) EFF_DATE,ED.IS_ACTIVE,ED.CREATED_BY,ED.CREATED_DATETIME," +
                                                  "ED.MODIFIED_BY,ED.LAST_UPDATED_DATETIME,LV.LOB_DESC LOB,LV.LOB_ID";
                //specifying tables for from clause
                ((BaseDataGrid)c1).FromClause=" APP_EXPIRY_DATES ED,MNT_LOB_MASTER LV ";
                //specifying conditions for where clause
                ((BaseDataGrid)c1).WhereClause=" ED.CUSTOMER_ID=" + customer_ID + " AND ED.EXPDT_LOB=convert(varchar,LV.LOB_ID)";
                //specifying Text to be shown in combo box
                ((BaseDataGrid)c1).SearchColumnHeadings="Line of Business^Policy Number^Company^Expiration Date^Contact Date";
                //specifying column to be used for combo box
                ((BaseDataGrid)c1).SearchColumnNames="LV.LOB_ID^ED.POLICY_NUMBER^ED.EXPDT_CARR^ED.EXPDT_DATE^ED.EXPDT_CONT_DATE";
                //search column data type specifying data type of the column to be used for combo box
				((BaseDataGrid)c1).DropDownColumns="LOB^^^^";	
                ((BaseDataGrid)c1).SearchColumnType="T^T^T^D^D";
                //specifying column for order by clause
                ((BaseDataGrid)c1).OrderByClause="LOB asc";
                //specifying column numbers of the query to be displyed in grid
                ((BaseDataGrid)c1).DisplayColumnNumbers="18^11^4^5^7";
                //specifying column names from the query
                ((BaseDataGrid)c1).DisplayColumnNames="LOB^POLICY_NUMBER^EXPDT_CARR^EXPDT_DATE^EXPDT_CONT_DATE";
                //specifying text to be shown as column headings
                ((BaseDataGrid)c1).DisplayColumnHeadings="Line of Business^Policy Number^Company^Expiration Date^Contact Date";
                //specifying column heading display text length
                ((BaseDataGrid)c1).DisplayTextLength="50^50^50^50^50";
                //specifying width percentage for columns
                ((BaseDataGrid)c1).DisplayColumnPercent="20^20^20^20^20";
                //specifying primary column number
                ((BaseDataGrid)c1).PrimaryColumns="1";
                //specifying primary column name
                ((BaseDataGrid)c1).PrimaryColumnsName="ED.EXPDT_ID^ED.CUSTOMER_ID";
                //specifying column type of the data grid
                ((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B";
                //specifying links pages 
                //((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
                //specifying if double click is allowed or not
                ((BaseDataGrid)c1).AllowDBLClick="true"; 
                //specifying which columns are to be displayed on first tab
                ((BaseDataGrid)c1).FetchColumns="1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18";
                //specifying message to be shown
                ((BaseDataGrid)c1).SearchMessage="Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                //specifying buttons to be displayed on grid
                ((BaseDataGrid)c1).ExtraButtons="1^Add^0^addRecord";
                //specifying number of the rows to be shown
                ((BaseDataGrid)c1).PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("GRID_PAGE_SIZE"));
                //specifying cache size (use for top clause)
                ((BaseDataGrid)c1).CacheSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("GRID_CACHE_SIZE"));
                //specifying image path
                ((BaseDataGrid)c1).ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                ((BaseDataGrid)c1).HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                //specifying heading
                ((BaseDataGrid)c1).HeaderString = "Expiration Dates";
                ((BaseDataGrid)c1).SelectClass = colors;
                //specifying text to be shown for filter checkbox
                ((BaseDataGrid)c1).FilterLabel="Include Inactive";
                //specifying column to be used for filtering
                ((BaseDataGrid)c1).FilterColumnName="ED.IS_ACTIVE";
                //value of filtering record
                ((BaseDataGrid)c1).FilterValue="Y";
                // property indiacating whether query string is required or not
                ((BaseDataGrid)c1).RequireQuery ="Y";
                // column numbers to create query string
                ((BaseDataGrid)c1).QueryStringColumns ="EXPDT_ID^CUSTOMER_ID";
				((BaseDataGrid)c1).DefaultSearch="Y";
              
                // to show completed task we have to give check box
                GridHolder.Controls.Add(c1);
            }
            catch(Exception ex)
            {
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}

            #endregion

                int flag=0;
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
            }
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
