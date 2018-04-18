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
using System.Text;
using System.Reflection;
using System.Resources;
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb.WebControls;



namespace Cms.CmsWeb.aspx
{
	/// <summary>
	/// Summary description for SearchLocation.
	/// </summary>
	public class SearchLocation : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
        ResourceManager objResourceMgr = null;
		
	
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

		private void Page_Load(object sender, System.EventArgs e)
           {
			
			base.ScreenId ="222";
            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.aspx.SearchLocation", System.Reflection.Assembly.GetExecutingAssembly());
			#region loading web grid control
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

			Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			
			BaseDataGrid objWebGrid;
			objWebGrid = (BaseDataGrid)c1;

			/* Check the SystemID of the logged in user.
				 * If the user is not a Wolverine user then display records of that agency ONLY
				 * else the normal flow follows */
			
			string sWHERECLAUSE	=	"";

			string  strSystemID			 = GetSystemId();
            string strCarrierSystemID = CarrierSystemID;//System.Configuration.ConfigurationSettings.AppSettings.Get("CarrierSystemID").ToString();
			if ( strSystemID.Trim().ToUpper() != strCarrierSystemID.Trim().ToUpper())
			{
			    	string strAgencyID = ClsAgency.GetAgencyIDFromCode(strSystemID).ToString();
					sWHERECLAUSE = " POL_CUSTOMER_POLICY_LIST.AGENCY_ID = " + strAgencyID;	
				    objWebGrid.WhereClause              =    sWHERECLAUSE;
			}



            string rootPath = System.Configuration.ConfigurationManager.AppSettings.Get("CmsPath").Trim();
			try
			{
				
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";

				objWebGrid.SelectClause = "LOC_NUM"
					+",POL_CUSTOMER_POLICY_LIST.POLICY_NUMBER AS POLICY_NUMBER,ISNULL(CCL.CUSTOMER_FIRST_NAME,'') + ' ' + ISNULL(CCL.CUSTOMER_MIDDLE_NAME,'') + ' ' +  ISNULL(CCL.CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME "
					+ ",Rtrim(Ltrim(IsNull(LOC_ADD1,'') + ' ' + IsNull(LOC_ADD2,'')  )) LOC_ADD1 "
					+ ",POL_LOCATIONS.LOCATION_ID,LOC_ZIP,MNT_COUNTRY_STATE_LIST.STATE_NAME AS STATE_NAME,IsNull(LOC_CITY,'') as LOC_CITY ";
				objWebGrid.FromClause = "POL_LOCATIONS "
					+ "INNER JOIN POL_CUSTOMER_POLICY_LIST ON POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID= POL_LOCATIONS.CUSTOMER_ID "
					+ "  AND      POL_CUSTOMER_POLICY_LIST.POLICY_ID=POL_LOCATIONS.POLICY_ID " 
					+ " AND POL_CUSTOMER_POLICY_LIST.POLICY_VERSION_ID=POL_LOCATIONS.POLICY_VERSION_ID " 
					+ " LEFT JOIN MNT_COUNTRY_STATE_LIST ON POL_LOCATIONS.LOC_STATE = MNT_COUNTRY_STATE_LIST.STATE_ID "
					+ " AND POL_LOCATIONS.LOC_COUNTRY = MNT_COUNTRY_STATE_LIST.COUNTRY_ID"
					+" left outer join CLT_CUSTOMER_LIST CCL on POL_CUSTOMER_POLICY_LIST.CUSTOMER_ID = CCL.CUSTOMER_ID";
                

				
				
				//objWebGrid.SearchColumnHeadings		=	"Address1^Address2^LOC_ZIP^STATE_NAME^LOC_CITY";
				
				objWebGrid.SearchColumnNames		=	"ISNULL(CCL.CUSTOMER_FIRST_NAME,'') ! ISNULL(CCL.CUSTOMER_MIDDLE_NAME,'') ! ISNULL(CCL.CUSTOMER_LAST_NAME,'')^POLICY_NUMBER^LOC_ADD1^LOC_CITY^LOC_ADD2^LOC_ZIP^STATE_NAME";
				
				
				
				objWebGrid.SearchColumnType			=	"T^T^T^T^T^T^T";
				
				
				
				
				objWebGrid.DisplayColumnNumbers		=	"1^2^3^6^4^5";

				objWebGrid.DisplayColumnNames		=   "CUSTOMER_NAME^POLICY_NUMBER^LOC_ADD1^LOC_CITY^LOC_ZIP^STATE_NAME";

                objWebGrid.SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings"); //"Customer Name^Policy Number^Address1^City^Address2^Zip^State";

                objWebGrid.DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings"); //"Customer Name^Policy Number^Address^City^Zip^State";

				
				objWebGrid.DisplayTextLength		=	"100^30^100^50^50^50";
				
				objWebGrid.DisplayColumnPercent		=	"10^8^10^12^12^12";
				
				

				
				objWebGrid.ColumnTypes				=	"B^B^B^B^B^B";
				
				objWebGrid.AllowDBLClick			=	"true";
                objWebGrid.SearchMessage = objResourceMgr.GetString("SearchMessage");// "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";				
				objWebGrid.PageSize					=	int.Parse(GetPageSize());
				objWebGrid.CacheSize				=	int.Parse(GetCacheSize());
                objWebGrid.HeaderString = objResourceMgr.GetString("HeaderString");//"Location Search";
				objWebGrid.SelectClass              =   colors;	
				
				objWebGrid.FetchColumns             =   "1^2^3^4^5^6^7^8";
                objWebGrid.ExtraButtons = objResourceMgr.GetString("ExtraButtons"); //"1^Close^0^CloseWin";
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
                
				
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.Grouping                 = "Y";
                
				
			
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(c1);
			}
			catch(Exception ex)
			{
				
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			#endregion

		}
		
		/// <summary>
		/// Populates the Search dropdown list with values from the Config file
		/// </summary>
		
	
		
		/// <summary>
		/// Returns a custom object with the total no of records for the query and other info
		/// </summary>
		/// <returns></returns>
	
		
	
		/// <summary>
		/// Gets the Datagrid column info from the XML file
		/// </summary>
		/// <returns></returns>
	
		/// <summary>
		/// Adds the relevant columns specified in teh XML file to the data grid
		/// </summary>
		
		/// <summary>
		/// Parses the relevant lookup information from the LookupForms.xml file
		/// </summary>
	
				
		/// <summary>
		/// Adds relevant javascript code to each row of the data grid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
	
	
		
		/// <summary>
		/// Clears the search field an re-binds the grid
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
	
		
		

		

		
		

	
		
	}
	
	
	
	

	
}


