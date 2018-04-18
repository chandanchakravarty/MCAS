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
using Cms.Application;
using Cms.BusinessLayer.BlCommon;

namespace Cms.Application.Aspx.Quote
{
	/// <summary>
	/// Summary description for QuoteIndex.
	/// </summary>
	public class QuoteIndex : Cms.Application.appbase
	{
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Panel pnlGrid;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected System.Web.UI.WebControls.Label lblError;
		//private static string  CUSTOMER_SECTION ="CLT";
		protected System.Web.UI.WebControls.Label capMessage;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId ="120_1";
			
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

			#region loading web grid control
			BaseDataGrid objWebGrid = (BaseDataGrid)LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{
				//bool ShowGrid =true;
				//string strCALLEDFROM="";
				cltClientTop.Visible = false;   

                int customer_ID=GetCustomerID()==""?0:int.Parse(GetCustomerID());

				/* If the control comes from the customer section then 
				 * 1.  check the session variable 'CUSTOMER_ID'
				 * 2. if there is some value then where clause contains only those records belonging to this customer
				 *	  else prompt the user.
				 * If the control is not coming from the customer section then normal flow follows */
				/*string sWHERECLAUSE="";
				string strCUSTOMER_ID = GetCustomerID(); 
				if (Request.QueryString["CALLEDFROM"]!= null )
				{
					strCALLEDFROM = Request.QueryString["CALLEDFROM"].ToString();
					if (strCALLEDFROM.ToUpper().Trim()==CUSTOMER_SECTION)
					{
						
						if (strCUSTOMER_ID!=null && strCUSTOMER_ID!="")
						{
							sWHERECLAUSE = " APP_LIST.CUSTOMER_ID = "+ strCUSTOMER_ID;	
							//Show the Client top
							cltClientTop.CustomerID = int.Parse(strCUSTOMER_ID);						
							cltClientTop.ShowHeaderBand ="Client";
							cltClientTop.Visible = true;        

						}
						else
						{
							sWHERECLAUSE =" 1<>1 ";
							lblError.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("115");
							ShowGrid =false;
						}
					}
				}*/
				 

				/* Check the SystemID of the logged in user.
				 * If the user is not a Wolverine user then display records of that agency ONLY
				 * else the normal flow follows */
				/*string  strSystemID					=	GetSystemId();
				string  strCarrierSystemID			=	System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString();
				if ( strSystemID.Trim().ToUpper()	!=	strCarrierSystemID.Trim().ToUpper())
				{
					string strAgencyID = ClsAgency.GetAgencyIDFromCode(strSystemID).ToString();
					if (sWHERECLAUSE.Trim().Equals(""))
					{						
						sWHERECLAUSE = " APP_LIST.APP_AGENCY_ID = "+ strAgencyID + "";	
					}
					else
					{
						sWHERECLAUSE = sWHERECLAUSE+ " AND APP_LIST.APP_AGENCY_ID = "+ strAgencyID ;	
					}
				}*/
				 

				//Setting web grid control properties
				/*objWebGrid.WebServiceURL			=	"http://" + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause				=	" APP_LIST.APP_ID,isnull(CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME,'') +' '+isnull(CLT_CUSTOMER_LIST.CUSTOMER_MIDDLE_NAME,'') +' '+ isnull(CLT_CUSTOMER_LIST.CUSTOMER_LAST_NAME,'') as CustomerName,MNT_LOB_MASTER.LOB_DESC,APP_LIST.APP_NUMBER, MNT_AGENCY_LIST.AGENCY_DISPLAY_NAME as Agency, MNT_USER_LIST.USER_FNAME +' ' + MNT_USER_LIST.USER_LNAME as CSR,' ' as Quote,APP_LIST.APP_VERSION, Convert(varchar(10),APP_LIST.APP_EFFECTIVE_DATE,101) as EffectiveDate ,APP_LIST.APP_STATUS, CLT_CUSTOMER_LIST.CUSTOMER_ZIP, APP_LIST.CUSTOMER_ID,  	APP_LIST.APP_VERSION_ID";
				objWebGrid.FromClause				=	" APP_LIST INNER JOIN MNT_LOB_MASTER ON APP_LIST.APP_LOB = MNT_LOB_MASTER.LOB_ID INNER JOIN CLT_CUSTOMER_LIST ON APP_LIST.CUSTOMER_ID = CLT_CUSTOMER_LIST.CUSTOMER_ID LEFT OUTER JOIN MNT_USER_LIST ON APP_LIST.CSR = MNT_USER_LIST.USER_ID INNER JOIN MNT_AGENCY_LIST ON APP_LIST.APP_AGENCY_ID = MNT_AGENCY_LIST.AGENCY_ID";
				objWebGrid.WhereClause				=	sWHERECLAUSE;
				objWebGrid.SearchColumnHeadings		=	"Customer Name^LOB^Application Number^Agency^CSR";
				objWebGrid.SearchColumnNames		=	"CLT_CUSTOMER_LIST.CUSTOMER_FIRST_NAME^MNT_LOB_MASTER.LOB_DESC^APP_LIST.APP_NUMBER^MNT_AGENCY_LIST.AGENCY_DISPLAY_NAME^MNT_USER_LIST.USER_FNAME";
				objWebGrid.SearchColumnType			=	"T^T^T^T^T";
				objWebGrid.OrderByClause			=	"CustomerName ASC";
				objWebGrid.DisplayColumnNumbers		=	"2^3^4^8^9^5^6^7";
				objWebGrid.DisplayColumnNames		=	"CustomerName^LOB_DESC^APP_NUMBER^APP_VERSION^EffectiveDate^Agency^CSR^Quote";
				objWebGrid.DisplayColumnHeadings	=	"Customer^Line Of Business^App #^Ver.^Eff. Date^Agency^CSR^Quote";
				objWebGrid.DisplayTextLength		=	"50^50^70^10^50^20^20^20";
				objWebGrid.DisplayColumnPercent		=	"15^15^20^5^12^15^15^5";
				objWebGrid.PrimaryColumns			=	"1";
				objWebGrid.PrimaryColumnsName		=	"APP_LIST.APP_ID";
				objWebGrid.ColumnTypes				=	"B^B^B^B^B^B^B^B";
				objWebGrid.AllowDBLClick			=	"true";
				objWebGrid.FetchColumns				=	"2";
				objWebGrid.SearchMessage			=	"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				//objWebGrid.ExtraButtons				=	"";
				objWebGrid.PageSize					=	int.Parse (GetPageSize());
				objWebGrid.CacheSize				=	int.Parse (GetCacheSize());
				objWebGrid.ImagePath				=	System.Configuration.ConfigurationSettings.AppSettings.Get("RootURL").Trim()+ "/images/collapse_icon.gif";
				objWebGrid.HImagePath				=	System.Configuration.ConfigurationSettings.AppSettings.Get("RootURL").Trim()+ "/images/expand_icon.gif";
				objWebGrid.HeaderString				=	"Application Information" ;
				objWebGrid.SelectClass				=	colors;
				objWebGrid.FilterLabel				=	"Include";				 
				objWebGrid.RequireQuery				=	"Y";
				objWebGrid.QueryStringColumns		=	"CUSTOMER_ID^APP_ID^APP_VERSION_ID";
				objWebGrid.DefaultSearch			=	"Y"; 
				
				//Adding to gridholder
				if (ShowGrid)
				{
					GridHolder.Controls.Add(objWebGrid);

					TabCtl.TabURLs = "Quote.aspx?CALLEDFROM=" + strCALLEDFROM +"&";	
				}
                */


                if(customer_ID!=0)
                {
         
                    #region loading web grid control
                    Control c1= LoadControl("../../../cmsweb/webcontrols/BaseDataGrid.ascx");
                    try
                    {
                
                        /*************************************************************************/
                        ///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
                        /************************************************************************/
                        //specifying webservice URL
                        ((BaseDataGrid)c1).WebServiceURL=httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                        //specifying columns for select query
                        ((BaseDataGrid)c1).SelectClause =" isnull(quote_number,'') quote_number,isnull(quote_type,'') quote_type,isnull(quote_description,'') quote_description,customer_id,app_id,app_version_id,quote_id,quote_version_id ";
                        //specifying tables for from clause
                        ((BaseDataGrid)c1).FromClause="  QOT_CUSTOMER_QUOTE_LIST  ";
                        //specifying conditions for where clause
                        ((BaseDataGrid)c1).WhereClause=" CUSTOMER_ID=" + customer_ID ;
                        //specifying Text to be shown in combo box
                        ((BaseDataGrid)c1).SearchColumnHeadings="Quote Number^Quote Type^Description";
                        //specifying column to be used for combo box
                        ((BaseDataGrid)c1).SearchColumnNames="quote_number^quote_type^quote_description";
                        //search column data type specifying data type of the column to be used for combo box
                        ((BaseDataGrid)c1).SearchColumnType="T^T^T";
                        //specifying column for order by clause
                        ((BaseDataGrid)c1).OrderByClause="quote_number ASC";
                        //specifying column numbers of the query to be displyed in grid
                        ((BaseDataGrid)c1).DisplayColumnNumbers="1^2^3";
                        //specifying column names from the query
                        ((BaseDataGrid)c1).DisplayColumnNames="quote_number^quote_type^quote_description";
                        //specifying text to be shown as column headings
                        ((BaseDataGrid)c1).DisplayColumnHeadings="Quote Number^Quote Type^Description";
                        //specifying column heading display text length
                        ((BaseDataGrid)c1).DisplayTextLength="50^50^50";
                        //specifying width percentage for columns
                        ((BaseDataGrid)c1).DisplayColumnPercent="33^33^33";
                        //specifying primary column number
                        ((BaseDataGrid)c1).PrimaryColumns="4^5^6^7";
                        //specifying primary column name
                        ((BaseDataGrid)c1).PrimaryColumnsName="customer_id^app_id^app_version_id^quote_id";
                        //specifying column type of the data grid
                        ((BaseDataGrid)c1).ColumnTypes="B^B^B";
                        //specifying links pages 
                        //((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
                        //specifying if double click is allowed or not
                        ((BaseDataGrid)c1).AllowDBLClick="true"; 
                        //specifying which columns are to be displayed on first tab
                        //((BaseDataGrid)c1).FetchColumns="1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22";
                        //specifying message to be shown
                        ((BaseDataGrid)c1).SearchMessage="Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                        //specifying buttons to be displayed on grid
                        //((BaseDataGrid)c1).ExtraButtons="1^Add^0^addRecord";
                        //specifying number of the rows to be shown
                        ((BaseDataGrid)c1).PageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("GRID_PAGE_SIZE"));
                        //specifying cache size (use for top clause)
                        ((BaseDataGrid)c1).CacheSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("GRID_CACHE_SIZE"));
                        //specifying image path
                        ((BaseDataGrid)c1).ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                        ((BaseDataGrid)c1).HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                        //specifying heading
                        ((BaseDataGrid)c1).HeaderString = "Quote Search";
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
                        ((BaseDataGrid)c1).QueryStringColumns ="customer_id^app_id^app_version_id^quote_id";
                  
                        // to show completed task we have to give check box
                        GridHolder.Controls.Add(c1);
                    }
                    catch(Exception ex)
                    {
                        Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                    }
        
                    #endregion
                }


                /*objWebGrid.WebServiceURL			=	"http://" + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
                objWebGrid.SelectClause				=	" isnull(quote_number,'') quote_number,isnull(quote_type,'') quote_type,isnull(quote_description,'') quote_description,customer_id,app_id,app_version_id,quote_id,quote_version_id ";
                objWebGrid.FromClause				=	" QOT_CUSTOMER_QUOTE_LIST ";
                objWebGrid.WhereClause				=	" customer_id=" + customer_ID;
                objWebGrid.SearchColumnNames		=	" quote_number^quote_type^quote_description ";
                objWebGrid.SearchColumnHeadings		=	" Quote Number^Quote Type^Description";
                objWebGrid.SearchColumnType			=	"T^T^T";
                objWebGrid.OrderByClause			=	"quote_number ASC";
                objWebGrid.DisplayColumnNumbers		=	"1^2^3";
                objWebGrid.DisplayColumnNames		=	" quote_number^quote_type^quote_description ";
                objWebGrid.DisplayColumnHeadings	=	" Quote Number^Quote Type^Description";
                objWebGrid.DisplayTextLength		=	"50^50^70";
                objWebGrid.DisplayColumnPercent		=	"33^33^22";
                objWebGrid.PrimaryColumns			=	"1";
                objWebGrid.PrimaryColumnsName		=	"customer_id^app_id^app_version_id^quote_id";
                objWebGrid.ColumnTypes				=	"B^B^B";
                objWebGrid.AllowDBLClick			=	"true";
                objWebGrid.FetchColumns				=	"2";
                objWebGrid.SearchMessage			=	"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
                //objWebGrid.ExtraButtons				=	"";
                objWebGrid.PageSize					=	int.Parse (GetPageSize());
                objWebGrid.CacheSize				=	int.Parse (GetCacheSize());
                objWebGrid.ImagePath				=	System.Configuration.ConfigurationSettings.AppSettings.Get("RootURL").Trim()+ "/images/collapse_icon.gif";
                objWebGrid.HImagePath				=	System.Configuration.ConfigurationSettings.AppSettings.Get("RootURL").Trim()+ "/images/expand_icon.gif";
                objWebGrid.HeaderString				=	"Quote Search" ;
                objWebGrid.SelectClass				=	colors;
                objWebGrid.FilterLabel				=	"Include";				 
                objWebGrid.RequireQuery				=	"Y";
                objWebGrid.QueryStringColumns		=	"customer_id^app_id^app_version_id^quote_id";
                objWebGrid.DefaultSearch			=	"Y"; */
				
			}
			catch(Exception ex)
			{
				throw(ex);
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
