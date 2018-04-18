/******************************************************************************************
	<Author					: - >Sumit Chhabra
	<Start Date				: -	> May 03, 2006
	<End Date				: - >
	<Description			: - >This file is being used for loading grid control to show driver records
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
	public class DriverDetailsIndex : Cms.Claims.ClaimBase
	{
		//protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		#region "web form designer controls declaration"

		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;

		#endregion
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTYPE_OF_DRIVER;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		protected System.Web.UI.HtmlControls.HtmlTableRow tabCtlRow;	
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTYPE_OF_OWNER;
		protected string strLOB_ID="";

		private void Page_Load(object sender, System.EventArgs e)
		{
			SetActivityStatus("");
			base.ScreenId="306_5";
			// Put user code to initialize the page here

			
			#region GETTING BASE COLOR FOR ROW SELECTION
			string colorScheme=GetColorScheme();
			string colors="";

			GetQueryStringValues();

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

				/*************************************************************************/
				///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
				/************************************************************************/
				//specifying webservice URL
				sWHERECLAUSE=" CDI.CLAIM_ID=" + hidCLAIM_ID.Value;
				((BaseDataGrid)c1).WebServiceURL=httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				//specifying columns for select query
				((BaseDataGrid)c1).SelectClause =" * ";
				//((BaseDataGrid)c1).SelectClause ="ISNULL(VEHICLE_YEAR,'') + '-' + ISNULL(MAKE,'') + '-' + ISNULL(MODEL,'') + '-' + ISNULL(VIN,'') AS VIN, NAME, CDI.DRIVER_ID,CDI.CLAIM_ID,TYPE_OF_DRIVER,(ADDRESS1 + ' ' + ADDRESS2) AS ADDRESS,CITY,MCST.STATE_NAME,ZIP,HOME_PHONE,CDI.IS_ACTIVE";
				//specifying tables for from clause
				((BaseDataGrid)c1).SelectClause = "  ISNULL(VEHICLE_YEAR,'') + '-' + ISNULL(MAKE,'') + '-' + ISNULL(MODEL,'') + '-' + ISNULL(VIN,'') AS VIN, NAME, CDI.DRIVER_ID,CDI.CLAIM_ID,TYPE_OF_DRIVER,(ADDRESS1 + ' ' + ADDRESS2) AS ADDRESS,CITY,MCST.STATE_NAME,ZIP,HOME_PHONE,SSN,CDI.IS_ACTIVE, MLV.LOOKUP_VALUE_DESC AS VEHICLE_OWNER_DESC " ;
				((BaseDataGrid)c1).FromClause = " CLM_DRIVER_INFORMATION CDI LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCST ON CDI.STATE=MCST.STATE_ID LEFT OUTER JOIN CLM_INSURED_VEHICLE CIV ON CDI.VEHICLE_ID=CIV.INSURED_VEHICLE_ID AND CDI.CLAIM_ID=CIV.CLAIM_ID ";
				((BaseDataGrid)c1).FromClause+=" LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON MLV.LOOKUP_UNIQUE_ID = CDI.VEHICLE_OWNER ";
				((BaseDataGrid)c1).WhereClause = " CDI.CLAIM_ID= " + hidCLAIM_ID.Value  ;
				/*((BaseDataGrid)c1).FromClause= " ( SELECT ISNULL(VEHICLE_YEAR,'') + '-' + ISNULL(MAKE,'') + '-' + ISNULL(MODEL,'') + '-' + ISNULL(VIN,'') AS VIN, NAME, CDI.DRIVER_ID,CDI.CLAIM_ID,TYPE_OF_DRIVER,(ADDRESS1 + ' ' + ADDRESS2) AS ADDRESS,CITY,MCST.STATE_NAME,ZIP,HOME_PHONE,SSN,CDI.IS_ACTIVE, MLV.LOOKUP_VALUE_DESC AS VEHICLE_OWNER_DESC " ;
				((BaseDataGrid)c1).FromClause+="  FROM CLM_DRIVER_INFORMATION CDI LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCST ON CDI.STATE=MCST.STATE_ID LEFT OUTER JOIN CLM_INSURED_VEHICLE CIV ON CDI.VEHICLE_ID=CIV.INSURED_VEHICLE_ID AND CDI.CLAIM_ID=CIV.CLAIM_ID ";
				((BaseDataGrid)c1).FromClause+=" LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON MLV.LOOKUP_UNIQUE_ID = CDI.VEHICLE_OWNER ";
				((BaseDataGrid)c1).FromClause+=" WHERE CDI.CLAIM_ID= " + hidCLAIM_ID.Value + " ) Test ";*/
				//specifying conditions for where clause
				//((BaseDataGrid)c1).WhereClause=sWHERECLAUSE;//" agency_code not in ('"+ System.Configuration.ConfigurationSettings.AppSettings["CarrierSystemID"].ToString()   +  "')";
				if(strLOB_ID==((int)enumLOB.HOME).ToString())
				{
					//specifying Text to be shown in combo box				
					((BaseDataGrid)c1).SearchColumnHeadings="Driver of Vehicle^Operator Name^Operator Address";
					//specifying column to be used for combo box				
					((BaseDataGrid)c1).SearchColumnNames="MLV.LOOKUP_VALUE_DESC^NAME^ADDRESS";
					//search column data type specifying data type of the column to be used for combo box				
					((BaseDataGrid)c1).SearchColumnType="T^T^T";
					//specifying column for order by clause
					((BaseDataGrid)c1).OrderByClause="NAME asc";
					//specifying column numbers of the query to be displyed in grid
					((BaseDataGrid)c1).DisplayColumnNumbers="1^2^3";
					//specifying column names from the query				
					((BaseDataGrid)c1).DisplayColumnNames="VEHICLE_OWNER_DESC^NAME^ADDRESS";
					//specifying text to be shown as column headings				
					((BaseDataGrid)c1).DisplayColumnHeadings="Driver of Boat/Vehicle^Name^Address";
					//specifying column heading display text length
					((BaseDataGrid)c1).DisplayTextLength="50^50^50";
					//specifying width percentage for columns
					((BaseDataGrid)c1).DisplayColumnPercent="20^20^40";
					//specifying primary column number
					((BaseDataGrid)c1).PrimaryColumns="1";
					//specifying primary column name
					((BaseDataGrid)c1).PrimaryColumnsName="CDI.DRIVER_ID";
					//specifying column type of the data grid
					((BaseDataGrid)c1).ColumnTypes="B^B^B";
					//specifying links pages 
					//((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
					//specifying if double click is allowed or not
					((BaseDataGrid)c1).AllowDBLClick="true"; 
					//specifying which columns are to be displayed on first tab
					((BaseDataGrid)c1).FetchColumns="1^2^3";
					//specifying heading
					((BaseDataGrid)c1).HeaderString = "Operator/Driver Information";
					TabCtl.TabURLs   =  "AddWatercraftDriverDetails.aspx?TYPE_OF_DRIVER=" + hidTYPE_OF_DRIVER.Value + "&CLAIM_ID=" + hidCLAIM_ID.Value + "&CUSTOMER_ID=" + hidCUSTOMER_ID.Value + "&POLICY_ID=" + hidPOLICY_ID.Value + "&POLICY_VERSION_ID=" + hidPOLICY_VERSION_ID.Value + "&LOB_ID=" + strLOB_ID + "&TYPE_OF_OWNER=1&";
					TabCtl.TabTitles = "Operator/Driver Information";
					TabCtl.TabLength = 200;
				}
				else if(strLOB_ID==((int)enumLOB.BOAT).ToString())
				{
					//specifying Text to be shown in combo box				
					((BaseDataGrid)c1).SearchColumnHeadings="Driver of Boat^Operator Name^Operator Address^SSN";
					//specifying column to be used for combo box				
					((BaseDataGrid)c1).SearchColumnNames="MLV.LOOKUP_VALUE_DESC^NAME^ADDRESS";
					//search column data type specifying data type of the column to be used for combo box				
					((BaseDataGrid)c1).SearchColumnType="T^T^T";
					//specifying column for order by clause
					((BaseDataGrid)c1).OrderByClause="NAME asc";
					//specifying column numbers of the query to be displyed in grid
					((BaseDataGrid)c1).DisplayColumnNumbers="1^2^3";
					//specifying column names from the query				
					((BaseDataGrid)c1).DisplayColumnNames="VEHICLE_OWNER_DESC^NAME^ADDRESS";
					//specifying text to be shown as column headings				
					((BaseDataGrid)c1).DisplayColumnHeadings="Driver of Boat/Vehicle^Name^Address";
					//specifying column heading display text length
					((BaseDataGrid)c1).DisplayTextLength="50^50^50";
					//specifying width percentage for columns
					((BaseDataGrid)c1).DisplayColumnPercent="20^20^40";
					//specifying primary column number
					((BaseDataGrid)c1).PrimaryColumns="1";
					//specifying primary column name
					((BaseDataGrid)c1).PrimaryColumnsName="CDI.DRIVER_ID";
					//specifying column type of the data grid
					((BaseDataGrid)c1).ColumnTypes="B^B^B";
					//specifying links pages 
					//((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
					//specifying if double click is allowed or not
					((BaseDataGrid)c1).AllowDBLClick="true"; 
					//specifying which columns are to be displayed on first tab
					((BaseDataGrid)c1).FetchColumns="1^2^3";
					//specifying heading
					((BaseDataGrid)c1).HeaderString = "Operator/Driver Information";
					TabCtl.TabURLs   =  "AddWatercraftDriverDetails.aspx?TYPE_OF_DRIVER=" + hidTYPE_OF_DRIVER.Value + "&CLAIM_ID=" + hidCLAIM_ID.Value + "&CUSTOMER_ID=" + hidCUSTOMER_ID.Value + "&POLICY_ID=" + hidPOLICY_ID.Value + "&POLICY_VERSION_ID=" + hidPOLICY_VERSION_ID.Value + "&LOB_ID=" + strLOB_ID + "&TYPE_OF_OWNER=1&";
					TabCtl.TabTitles = "Operator/Driver Information";
					TabCtl.TabLength = 200;
				}
				else
				{
					if(hidTYPE_OF_OWNER.Value == ((int)enumTYPE_OF_OWNER.INSURED_VEHICLE).ToString())
					{
						//specifying Text to be shown in combo box				
						((BaseDataGrid)c1).SearchColumnHeadings="VIN^Driver of Vehicle^Name";
						//specifying column to be used for combo box				
						((BaseDataGrid)c1).SearchColumnNames="VIN^MLV.LOOKUP_VALUE_DESC^NAME";
						//search column data type specifying data type of the column to be used for combo box				
						((BaseDataGrid)c1).SearchColumnType="T^T^T";
						//specifying column for order by clause
						((BaseDataGrid)c1).OrderByClause="NAME asc";
						//specifying column numbers of the query to be displyed in grid
						((BaseDataGrid)c1).DisplayColumnNumbers="1^2^3";
						//specifying column names from the query				
						((BaseDataGrid)c1).DisplayColumnNames="VIN^VEHICLE_OWNER_DESC^NAME";
						//specifying text to be shown as column headings				
						((BaseDataGrid)c1).DisplayColumnHeadings="VIN^Driver of Vehicle^Name";
						//specifying column heading display text length
						((BaseDataGrid)c1).DisplayTextLength="40^30^30";
						//specifying width percentage for columns
						((BaseDataGrid)c1).DisplayColumnPercent="40^30^30";
						//specifying primary column number
						((BaseDataGrid)c1).PrimaryColumns="1";
						//specifying primary column name
						((BaseDataGrid)c1).PrimaryColumnsName="CDI.DRIVER_ID";
						//specifying column type of the data grid
						((BaseDataGrid)c1).ColumnTypes="B^B^B";
						//specifying links pages 
						//((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
						//specifying if double click is allowed or not
						((BaseDataGrid)c1).AllowDBLClick="true"; 
						//specifying which columns are to be displayed on first tab
						((BaseDataGrid)c1).FetchColumns="1^2^3";
						//specifying heading
						((BaseDataGrid)c1).HeaderString = "Driver Information";
						//Added for Itrack Issue 6053 on 8 Sept 09
						((BaseDataGrid)c1).FilterLabel = "Include Inactive";
						((BaseDataGrid)c1).FilterColumnName = "CDI.IS_ACTIVE";
						((BaseDataGrid)c1).FilterValue = "Y";
						((BaseDataGrid)c1).RequireQuery = "Y";
						((BaseDataGrid)c1).SystemColumnName = "IS_ACTIVE";
						//Added till here
						TabCtl.TabURLs   =  "AddDriverDetails.aspx?TYPE_OF_DRIVER=" + hidTYPE_OF_DRIVER.Value + "&CLAIM_ID=" + hidCLAIM_ID.Value + "&CUSTOMER_ID=" + hidCUSTOMER_ID.Value + "&POLICY_ID=" + hidPOLICY_ID.Value + "&POLICY_VERSION_ID=" + hidPOLICY_VERSION_ID.Value + "&LOB_ID=" + strLOB_ID + "&TYPE_OF_OWNER=1&";
						TabCtl.TabTitles = "Driver Details";
					}
					else
					{
						//specifying Text to be shown in combo box				
						((BaseDataGrid)c1).SearchColumnHeadings="VIN^Name";
						//specifying column to be used for combo box				
						((BaseDataGrid)c1).SearchColumnNames="VIN^NAME";
						//search column data type specifying data type of the column to be used for combo box				
						((BaseDataGrid)c1).SearchColumnType="T^T";
						//specifying column for order by clause
						((BaseDataGrid)c1).OrderByClause="NAME asc";
						//specifying column numbers of the query to be displyed in grid
						((BaseDataGrid)c1).DisplayColumnNumbers="1^2";
						//specifying column names from the query				
						((BaseDataGrid)c1).DisplayColumnNames="VIN^NAME";
						//specifying text to be shown as column headings				
						((BaseDataGrid)c1).DisplayColumnHeadings="VIN^Name";
						//specifying column heading display text length
						((BaseDataGrid)c1).DisplayTextLength="50^50";
						//specifying width percentage for columns
						((BaseDataGrid)c1).DisplayColumnPercent="50^50";
						//specifying primary column number
						((BaseDataGrid)c1).PrimaryColumns="1";
						//specifying primary column name
						((BaseDataGrid)c1).PrimaryColumnsName="CDI.DRIVER_ID";
						//specifying column type of the data grid
						((BaseDataGrid)c1).ColumnTypes="B^B";
						//specifying links pages 
						//((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
						//specifying if double click is allowed or not
						((BaseDataGrid)c1).AllowDBLClick="true"; 
						//specifying which columns are to be displayed on first tab
						((BaseDataGrid)c1).FetchColumns="1^2^3";
						//specifying heading
						((BaseDataGrid)c1).HeaderString = "Driver Information";
						TabCtl.TabURLs   =  "AddDriverDetails.aspx?TYPE_OF_DRIVER=" + hidTYPE_OF_DRIVER.Value + "&CLAIM_ID=" + hidCLAIM_ID.Value + "&CUSTOMER_ID=" + hidCUSTOMER_ID.Value + "&POLICY_ID=" + hidPOLICY_ID.Value + "&POLICY_VERSION_ID=" + hidPOLICY_VERSION_ID.Value + "&LOB_ID=" + strLOB_ID + "&TYPE_OF_OWNER=1&";
						TabCtl.TabTitles = "Driver Details";
					}
				}
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
				((BaseDataGrid)c1).QueryStringColumns ="DRIVER_ID";
				((BaseDataGrid)c1).DefaultSearch="Y";     
                
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
			if(Request.QueryString["TYPE_OF_DRIVER"]!=null && Request.QueryString["TYPE_OF_DRIVER"].ToString()!="" )
				hidTYPE_OF_DRIVER.Value = Request.QueryString["TYPE_OF_DRIVER"].ToString();

			if(Request.QueryString["CLAIM_ID"]!=null && Request.QueryString["CLAIM_ID"].ToString()!="" )
				hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();

			if(Request.QueryString["CUSTOMER_ID"]!=null && Request.QueryString["CUSTOMER_ID"].ToString()!="" )
				hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();

			if(Request.QueryString["POLICY_ID"]!=null && Request.QueryString["POLICY_ID"].ToString()!="" )
				hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].ToString();
			if(Request.QueryString["TYPE_OF_OWNER"]!=null && Request.QueryString["TYPE_OF_OWNER"].ToString()!="" )
				hidTYPE_OF_OWNER.Value = Request.QueryString["TYPE_OF_OWNER"].ToString();
			else
				hidTYPE_OF_OWNER.Value = "0";

			if(Request.QueryString["POLICY_VERSION_ID"]!=null && Request.QueryString["POLICY_VERSION_ID"].ToString()!="" )
				hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();
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
