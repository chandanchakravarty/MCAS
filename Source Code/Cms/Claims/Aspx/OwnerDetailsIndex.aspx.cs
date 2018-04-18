/******************************************************************************************
	<Author					: - >Sumit Chhabra
	<Start Date				: -	> May 02,2006
	<End Date				: - >
	<Description			: - >This file is being used for locading grid control to show claim owner records
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
	public class OwnerDetailsIndex : Cms.Claims.ClaimBase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTYPE_OF_OWNER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTYPE_OF_HOME;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected string strLOB_ID="";
		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;		
		#region "web form designer controls declaration"

		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;

		#endregion
		protected System.Web.UI.HtmlControls.HtmlTableRow tabCtlRow;		

		private void Page_Load(object sender, System.EventArgs e)
		{
			SetActivityStatus("");
			base.ScreenId="306_8";
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

			#region loading web grid control
			Control c1= LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{
                
				string sWHERECLAUSE;
				GetQueryStringValues();

				/*************************************************************************/
				///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
				/************************************************************************/
				//specifying webservice URL
				//sWHERECLAUSE=" CLAIM_ID=" + hidCLAIM_ID.Value ;
				sWHERECLAUSE=" COI.CLAIM_ID=" + hidCLAIM_ID.Value + " and TYPE_OF_OWNER = " + hidTYPE_OF_OWNER.Value + " and TYPE_OF_HOME = " + hidTYPE_OF_HOME.Value;;
				((BaseDataGrid)c1).WebServiceURL=httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				
				//specifying conditions for where clause
				((BaseDataGrid)c1).WhereClause=sWHERECLAUSE;//" agency_code not in ('"+ System.Configuration.ConfigurationSettings.AppSettings["CarrierSystemID"].ToString()   +  "')";								
				if(hidTYPE_OF_OWNER.Value == ((int)enumTYPE_OF_OWNER.INSURED_VEHICLE).ToString())
				{
					//specifying columns for select query
					((BaseDataGrid)c1).SelectClause ="COI.OWNER_ID,COI.CLAIM_ID,TYPE_OF_OWNER,NAME,(ADDRESS1 + ' ' + ADDRESS2) AS ADDRESS,CITY,MCST.STATE_NAME,ZIP,DEFAULT_PHONE_TO_NOTICE,PRODUCTS_INSURED_IS,OTHER_DESCRIPTION,TYPE_OF_PRODUCT,WHERE_PRODUCT_SEEN,OTHER_LIABILITY,COI.IS_ACTIVE IS_ACTIVE,WORK_PHONE,MLV1.LOOKUP_VALUE_DESC AS VEHICLE_OWNER,ISNULL(VEHICLE_YEAR,'') + '-' + ISNULL(MAKE,'') + '-' + ISNULL(MODEL,'') + '-' + ISNULL(VIN,'') AS VIN";
					//specifying tables for from clause
					((BaseDataGrid)c1).FromClause=" CLM_OWNER_INFORMATION COI LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCST ON COI.STATE=MCST.STATE_ID JOIN MNT_LOOKUP_VALUES MLV1 ON COI.VEHICLE_OWNER=MLV1.LOOKUP_UNIQUE_ID LEFT OUTER JOIN CLM_INSURED_VEHICLE CIV ON COI.VEHICLE_ID=CIV.INSURED_VEHICLE_ID AND COI.CLAIM_ID=CIV.CLAIM_ID";
					//specifying column numbers of the query to be displyed in grid
					((BaseDataGrid)c1).DisplayColumnNumbers="1^2^3";
					//specifying column names from the query				
					((BaseDataGrid)c1).DisplayColumnNames="VIN^VEHICLE_OWNER^NAME";
					//specifying text to be shown as column headings				
					((BaseDataGrid)c1).DisplayColumnHeadings="VIN^Owner of the Vehicle^Name";
					//specifying column heading display text length
					((BaseDataGrid)c1).DisplayTextLength="40^30^30";
					//specifying width percentage for columns
					((BaseDataGrid)c1).DisplayColumnPercent="40^30^30";
					//specifying column type of the data grid
					((BaseDataGrid)c1).ColumnTypes="B^B^B";
					//specifying Text to be shown in combo box				
					((BaseDataGrid)c1).SearchColumnHeadings="VIN^Owner of the Vehicle^Name";
					//specifying column to be used for combo box				
					((BaseDataGrid)c1).SearchColumnNames="ISNULL(VEHICLE_YEAR,'') ! '-' ! ISNULL(MAKE,'') ! '-' ! ISNULL(MODEL,'') ! '-' ! ISNULL(VIN,'')^MLV1.LOOKUP_VALUE_DESC^NAME";
					//search column data type specifying data type of the column to be used for combo box
					((BaseDataGrid)c1).SearchColumnType="T^T^T";				
					//specifying column for order by clause
					((BaseDataGrid)c1).OrderByClause="MLV1.LOOKUP_VALUE_DESC asc";		
				}
				else
				{
					//specifying columns for select query
					((BaseDataGrid)c1).SelectClause ="COI.OWNER_ID,COI.CLAIM_ID,TYPE_OF_OWNER,NAME,(ADDRESS1 + ' ' + ADDRESS2) AS ADDRESS,CITY,MCST.STATE_NAME,ZIP,DEFAULT_PHONE_TO_NOTICE,PRODUCTS_INSURED_IS,OTHER_DESCRIPTION,TYPE_OF_PRODUCT,WHERE_PRODUCT_SEEN,OTHER_LIABILITY,COI.IS_ACTIVE IS_ACTIVE,WORK_PHONE,VEHICLE_OWNER,ISNULL(VEHICLE_YEAR,'') + '-' + ISNULL(MAKE,'') + '-' + ISNULL(MODEL,'') + '-' + ISNULL(VIN,'') AS VIN";
					//specifying tables for from clause
					((BaseDataGrid)c1).FromClause=" CLM_OWNER_INFORMATION COI LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCST ON COI.STATE=MCST.STATE_ID LEFT OUTER JOIN CLM_INSURED_VEHICLE CIV ON COI.VEHICLE_ID=CIV.INSURED_VEHICLE_ID AND COI.CLAIM_ID=CIV.CLAIM_ID ";
					//specifying column numbers of the query to be displyed in grid
					((BaseDataGrid)c1).DisplayColumnNumbers="1^2^3^4^5^6";
					//specifying column names from the query				
					((BaseDataGrid)c1).DisplayColumnNames="VIN^NAME^ADDRESS^CITY^STATE_NAME^ZIP";
					//specifying text to be shown as column headings				
					((BaseDataGrid)c1).DisplayColumnHeadings="VIN^Name^Address^City^State^Zip";
					//specifying column heading display text length
					((BaseDataGrid)c1).DisplayTextLength="50^50^50^50^50^50";
					//specifying width percentage for columns
					((BaseDataGrid)c1).DisplayColumnPercent="20^25^30^10^10^5";
					//specifying column type of the data grid
					((BaseDataGrid)c1).ColumnTypes="B^B^B^B^B^B";
					//specifying Text to be shown in combo box				
					((BaseDataGrid)c1).SearchColumnHeadings="VIN^Name^Address^City^State^Zip";
					//specifying column to be used for combo box				
					((BaseDataGrid)c1).SearchColumnNames="ISNULL(VEHICLE_YEAR,'') ! '-' ! ISNULL(MAKE,'') ! '-' ! ISNULL(MODEL,'') ! '-' ! ISNULL(VIN,'')^NAME^(ADDRESS1 ! ' ' ! ADDRESS2)^CITY^STATE_NAME^ZIP";
					//search column data type specifying data type of the column to be used for combo box
					((BaseDataGrid)c1).SearchColumnType="T^T^T^T^T^T";	
					//specifying column for order by clause
					((BaseDataGrid)c1).OrderByClause="NAME asc";	
				}
				//specifying primary column number
				((BaseDataGrid)c1).PrimaryColumns="1";
				//specifying primary column name
				((BaseDataGrid)c1).PrimaryColumnsName="COI.OWNER_ID";
				
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
				if(hidTYPE_OF_OWNER.Value == ((int)enumTYPE_OF_OWNER.LIABILITY_TYPE_MANUFACTURER).ToString())
				{
					((BaseDataGrid)c1).HeaderString = "Manufacturer Details";
					TabCtl.TabTitles = "Manufacturer Details";
				}
				else
				{
					((BaseDataGrid)c1).HeaderString = "Owner Details";
					TabCtl.TabTitles = "Owner Details";
				}
				((BaseDataGrid)c1).SelectClass = colors;
				//specifying text to be shown for filter checkbox
				//((BaseDataGrid)c1).FilterLabel="Show Complete";
				//specifying column to be used for filtering
				//((BaseDataGrid)c1).FilterColumnName="LISTOPEN";
				//value of filtering record
				//((BaseDataGrid)c1).FilterValue="Y";
				
				//					//specifying text to be shown for filter checkbox
				//					((BaseDataGrid)c1).FilterLabel="Include Inactive";
				//					//specifying column to be used for filtering
				//					((BaseDataGrid)c1).FilterColumnName="AGENCY.IS_ACTIVE";
				//					//value of filtering record
				//					((BaseDataGrid)c1).FilterValue="Y";


				// property indiacating whether query string is required or not
				((BaseDataGrid)c1).RequireQuery ="Y";
				// column numbers to create query string
				((BaseDataGrid)c1).QueryStringColumns ="OWNER_ID";
				((BaseDataGrid)c1).DefaultSearch="Y";     

				//Setting tab control properties
				
				TabCtl.TabURLs   =  "AddOwnerDetails.aspx?TYPE_OF_OWNER=" + hidTYPE_OF_OWNER.Value + "&CLAIM_ID=" + hidCLAIM_ID.Value + "&CUSTOMER_ID=" + hidCUSTOMER_ID.Value + "&POLICY_ID=" + hidPOLICY_ID.Value + "&POLICY_VERSION_ID=" + hidPOLICY_VERSION_ID.Value + "&TYPE_OF_HOME=" + hidTYPE_OF_HOME.Value + "&LOB_ID=" + strLOB_ID + "&";
				
				
                
				// to show completed task we have to give check box
				GridHolder.Controls.Add(c1);				
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
       

			#endregion

		}

		private void GetQueryStringValues()
		{
			if(Request.QueryString["TYPE_OF_OWNER"]!=null && Request.QueryString["TYPE_OF_OWNER"].ToString()!="" )
				hidTYPE_OF_OWNER.Value = Request.QueryString["TYPE_OF_OWNER"].ToString();
			else
				hidTYPE_OF_OWNER.Value = "0";

			if(Request.QueryString["CLAIM_ID"]!=null && Request.QueryString["CLAIM_ID"].ToString()!="" )
				hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();

			if(Request.QueryString["CUSTOMER_ID"]!=null && Request.QueryString["CUSTOMER_ID"].ToString()!="" )
				hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();

			if(Request.QueryString["POLICY_ID"]!=null && Request.QueryString["POLICY_ID"].ToString()!="" )
				hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].ToString();

			if(Request.QueryString["POLICY_VERSION_ID"]!=null && Request.QueryString["POLICY_VERSION_ID"].ToString()!="" )
				hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();
			if(Request.QueryString["TYPE_OF_HOME"]!=null && Request.QueryString["TYPE_OF_HOME"].ToString()!="" )
				hidTYPE_OF_HOME.Value = Request.QueryString["TYPE_OF_HOME"].ToString();
			else
				hidTYPE_OF_HOME.Value = "0";

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
