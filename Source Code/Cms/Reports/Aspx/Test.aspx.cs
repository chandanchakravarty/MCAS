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
using Cms.CmsWeb.WebControls;
using Cms.BusinessLayer.BlCommon;

namespace Reports.Aspx
{	
	public class Test : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		#region "web form designer controls declaration"

		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;

		#endregion
		protected System.Web.UI.HtmlControls.HtmlTableRow tabCtlRow;
		public string  strSystemID="";

		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId="10000";
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
					colors=System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR3").ToString();     
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
			Control c1= LoadControl("/cms/cmsweb/webcontrols/BaseDataGrid.ascx");
			
			try
			{
                
				/* Check the SystemID of the logged in user.
				 * If the user is not a Wolverine user then display records of that agency ONLY
				 * else the normal flow follows */
				string sWHERECLAUSE="";
				strSystemID			 = GetSystemId();
				string strAgencyID = ClsAgency.GetAgencyIDFromCode(strSystemID).ToString();
                string strCarrierSystemID = CarrierSystemID;//System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString();
				if ( strSystemID.Trim().ToUpper() == strCarrierSystemID.Trim().ToUpper())
				{
					
					if (sWHERECLAUSE.Trim().Equals(""))
					{						
						sWHERECLAUSE = " AGENCY.AGENCY_ID  <> "+ strAgencyID;	
					}
					else
					{
						sWHERECLAUSE = " AND AGENCY.AGENCY_ID <> "+ strAgencyID;	
					}
				
				
					//									if (sWHERECLAUSE.Trim().Equals(""))
					//									{						
					//										sWHERECLAUSE = " AGENCY.AGENCY_CODE<>'" + strCarrierSystemID + "'";
					//									}
					//									else
					//									{
					//										sWHERECLAUSE += " AND AGENCY.AGENCY_CODE<>'" + strCarrierSystemID + "'";
					//									}

					/*************************************************************************/
					///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
					/************************************************************************/
					//specifying webservice URL
					//((BaseDataGrid)c1).WebServiceURL="../webservices/BaseDataGridWS.asmx?WSDL";
					((BaseDataGrid)c1).WebServiceURL=httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
					//objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
					//specifying columns for select query
					((BaseDataGrid)c1).SelectClause ="ISNULL(AGENCY.AGENCY_ID,0) AGENCY_ID,ISNULL(AGENCY.AGENCY_CODE,'') AGENCY_CODE,ISNULL(NUM_AGENCY_CODE,'') NUM_AGENCY_CODE,ISNULL(AGENCY_COMBINED_CODE,'') AGENCY_COMBINED_CODE,ISNULL(AGENCY.AGENCY_DISPLAY_NAME,'') AGENCY_DISPLAY_NAME,ISNULL(AGENCY_DBA,'') AGENCY_DBA,ISNULL(AGENCY.AGENCY_ADD1,'')+' '+ISNULL(AGENCY.AGENCY_ADD2,'') AS ADDRESS,ISNULL(AGENCY.AGENCY_CITY,'') AGENCY_CITY,ISNULL(AGENCY.AGENCY_STATE,0) AGENCY_STATE,ISNULL(AGENCY.AGENCY_ZIP,0) AGENCY_ZIP,ISNULL(AGENCY.AGENCY_COUNTRY,0) AGENCY_COUNTRY,ISNULL(AGENCY.AGENCY_PHONE,'') AGENCY_PHONE,ISNULL(AGENCY.AGENCY_EXT,0) AGENCY_EXT,ISNULL(AGENCY.AGENCY_FAX,0) AGENCY_FAX,ISNULL(AGENCY.AGENCY_EMAIL,'') AGENCY_EMAIL,ISNULL(AGENCY.AGENCY_WEBSITE,'') AGENCY_WEBSITE,ISNULL(AGENCY.AGENCY_PAYMENT_METHOD,0) AGENCY_PAYMENT_METHOD,ISNULL(AGENCY.AGENCY_COMMISSION,0.0) AGENCY_COMMISSION,ISNULL(AGENCY.AGENCY_BILL_TYPE,0) AGENCY_BILL_TYPE,ISNULL(AGENCY.AGENCY_SIGNATURES,0) AGENCY_SIGNATURES,AGENCY.IS_ACTIVE,AGENCY.LAST_UPDATED_DATETIME,ISNULL(AGENCY.AGENCY_ADD1,'') AGENCY_ADD1,ISNULL(AGENCY.AGENCY_ADD2,'') AGENCY_ADD2,AGENCY.AGENCY_LIC_NUM AGENCY_LIC_NUM";
					//specifying tables for from clause
					((BaseDataGrid)c1).FromClause=" mnt_agency_list agency ";
					//specifying conditions for where clause
					((BaseDataGrid)c1).WhereClause=sWHERECLAUSE;//" agency_code not in ('"+ System.Configuration.ConfigurationSettings.AppSettings["CarrierSystemID"].ToString()   +  "')";
					//specifying Text to be shown in combo box
					((BaseDataGrid)c1).SearchColumnHeadings="Agency Code^Numeric Agency Code^Combined Agency Code^Agency Name^DBA Name^Address^City^Phone";
					//specifying column to be used for combo box
					((BaseDataGrid)c1).SearchColumnNames="AGENCY_CODE^NUM_AGENCY_CODE^AGENCY_COMBINED_CODE^AGENCY_DISPLAY_NAME^AGENCY_DBA^ISNULL(AGENCY.AGENCY_ADD1,'') ! ISNULL(AGENCY.AGENCY_ADD2,'')^AGENCY_CITY^AGENCY_PHONE";
					//search column data type specifying data type of the column to be used for combo box
					((BaseDataGrid)c1).SearchColumnType="T^T^T^T^P";
					//specifying column for order by clause
					((BaseDataGrid)c1).OrderByClause="AGENCY_CODE asc";
					//specifying column numbers of the query to be displyed in grid
					((BaseDataGrid)c1).DisplayColumnNumbers="3^2^4^9^5";
					//specifying column names from the query
					((BaseDataGrid)c1).DisplayColumnNames="AGENCY_CODE^AGENCY_DISPLAY_NAME^ADDRESS^AGENCY_PHONE^AGENCY_CITY";
					//specifying text to be shown as column headings
					((BaseDataGrid)c1).DisplayColumnHeadings="Agency Code^Agency Name^Address^Phone^City";
					//specifying column heading display text length
					((BaseDataGrid)c1).DisplayTextLength="150^150^350^75^100";
					//specifying width percentage for columns
					((BaseDataGrid)c1).DisplayColumnPercent="20^20^20^20^20";
					//specifying primary column number
					((BaseDataGrid)c1).PrimaryColumns="1";
					//specifying primary column name
					((BaseDataGrid)c1).PrimaryColumnsName="AGENCY_ID";
					//specifying column type of the data grid
					((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B";
					//specifying links pages 
					//((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
					//specifying if double click is allowed or not
					((BaseDataGrid)c1).AllowDBLClick="true"; 
					//specifying which columns are to be displayed on first tab
					((BaseDataGrid)c1).FetchColumns="1^2^3^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22";
					//specifying message to be shown
					((BaseDataGrid)c1).SearchMessage="Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
					//specifying buttons to be displayed on grid
					((BaseDataGrid)c1).ExtraButtons="1^Add^0^addRecord";
					//specifying number of the rows to be shown
					((BaseDataGrid)c1).PageSize= int.Parse(GetPageSize());//int.Parse(System.Configuration.ConfigurationSettings.AppSettings.Get("GRID_PAGE_SIZE"));
					//specifying cache size (use for top clause)
					((BaseDataGrid)c1).CacheSize=int.Parse(GetCacheSize());//int.Parse(System.Configuration.ConfigurationSettings.AppSettings.Get("GRID_CACHE_SIZE"));
					//specifying image path
                    ((BaseDataGrid)c1).ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                    ((BaseDataGrid)c1).HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
					//specifying heading
					((BaseDataGrid)c1).HeaderString = "Agency Information";
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
					((BaseDataGrid)c1).FilterColumnName="AGENCY.IS_ACTIVE";
					//value of filtering record
					((BaseDataGrid)c1).FilterValue="Y";
	

					// property indiacating whether query string is required or not
					((BaseDataGrid)c1).RequireQuery ="Y";
					// column numbers to create query string
					((BaseDataGrid)c1).QueryStringColumns ="AGENCY_ID";
					((BaseDataGrid)c1).DefaultSearch="Y";     
                  
					// to show completed task we have to give check box
					GridHolder.Controls.Add(c1);
				}
				else
				{
					GridHolder.Visible=false;
					TabCtl.TabURLs = "TestDetail.aspx?AGENCY_ID="+strAgencyID+"&";
				}
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
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
