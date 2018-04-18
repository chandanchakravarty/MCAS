/******************************************************************************************
	<Author					: - >Sumit Chhabra
	<Start Date				: -	> May 16, 2006
	<End Date				: - >
	<Description			: - >This file is being used for loading grid control to show property damaged records
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
using System.Resources;
using System.Reflection;  



namespace Cms.Claims.Aspx
{
	/// <summary>
	/// This class is used for showing grid that search and display agency records
	/// </summary>
	public class PropertyDamagedIndex : Cms.Claims.ClaimBase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		#region "web form designer controls declaration"

		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;

		#endregion
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;
		//protected System.Web.UI.HtmlControls.HtmlInputHidden hidTYPE_OF_HOME;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlTableRow tabCtlRow;
        ResourceManager objResourceMgr = null;
		private void Page_Load(object sender, System.EventArgs e)
		{
			SetActivityStatus("");
			base.ScreenId="306_6";
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
            objResourceMgr = new ResourceManager("Cms.Claims.Aspx.PropertyDamagedIndex", Assembly.GetExecutingAssembly());

			#region loading web grid control
			Control c1= LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			try
			{
                int LangID = int.Parse(GetLanguageID());
                string SelectClause = "convert(varchar(30),convert(money,isnull( ESTIMATE_AMOUNT,0)),1) DISPLAY_ESTIMATE_AMOUNT,convert(varchar(30),ESTIMATE_AMOUNT,1) SEARCH_ESTIMATE_AMOUNT, ";

                SelectClause += " CASE WHEN PROP_DAMAGED_TYPE='11962' THEN dbo.fun_GetLookupDesc(11962," + LangID + ") WHEN PROP_DAMAGED_TYPE='11963' THEN dbo.fun_GetLookupDesc(11963," + LangID + ") ELSE dbo.fun_GetLookupDesc(11964," + LangID + ") END AS PropertyDamagedType, ";
                SelectClause +="  CASE WHEN OTHER_INSURANCE='1' THEN 'Yes' ELSE 'No' END AS OTHERINSURANCE , PROPERTY_DAMAGED_ID, PARTY_TYPE, PARTY_TYPE_DESC, MLV.LOOKUP_VALUE_DESC AS PARTY_TYPE_VALUE_DESC,DESCRIPTION AS DESCRIBE_PROPERTY_DAMAGED";
				string sWHERECLAUSE;

				/*************************************************************************/
				///////////////  SETTING WEB GRID USER CONTROL PROPERTIES /////////////////
				/************************************************************************/
				//specifying webservice URL
				sWHERECLAUSE=" CLAIM_ID = " + hidCLAIM_ID.Value;// + " AND TYPE_OF_HOME=" + hidTYPE_OF_HOME.Value;
				((BaseDataGrid)c1).WebServiceURL=httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				//specifying columns for select query
				//((BaseDataGrid)c1).SelectClause =" substring(convert(varchar(30),convert(money,ISNULL(ESTIMATE_AMOUNT,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(ESTIMATE_AMOUNT,0)),1),0)) AS ESTIMATE_AMOUNT, CASE WHEN DAMAGED_ANOTHER_VEHICLE='1' THEN 'Yes' ELSE 'No' END AS ANOTHERVECHDAMAGED, CASE WHEN OTHER_INSURANCE='1' THEN 'Yes' ELSE 'No' END AS OTHERINSURANCE , PROPERTY_DAMAGED_ID";
				//((BaseDataGrid)c1).SelectClause =" convert(varchar(30),convert(money,isnull( ESTIMATE_AMOUNT,0)),1) DISPLAY_ESTIMATE_AMOUNT,convert(varchar(30),ESTIMATE_AMOUNT,1) SEARCH_ESTIMATE_AMOUNT, CASE WHEN DAMAGED_ANOTHER_VEHICLE='1' THEN 'Yes' ELSE 'No' END  AS ANOTHERVECHDAMAGED, CASE WHEN OTHER_INSURANCE='1' THEN 'Yes' ELSE 'No' END AS OTHERINSURANCE , PROPERTY_DAMAGED_ID, PARTY_TYPE, PARTY_TYPE_DESC, MLV.LOOKUP_VALUE_DESC AS PARTY_TYPE_VALUE_DESC,DESCRIPTION AS DESCRIBE_PROPERTY_DAMAGED"; - Commented by Sibin for Itrack Issue 5145 on 3 Dec 2008
				//((BaseDataGrid)c1).SelectClause =" convert(varchar(30),convert(money,isnull( ESTIMATE_AMOUNT,0)),1) DISPLAY_ESTIMATE_AMOUNT,convert(varchar(30),ESTIMATE_AMOUNT,1) SEARCH_ESTIMATE_AMOUNT, CASE WHEN PROP_DAMAGED_TYPE='11962' THEN 'Vehicle' WHEN PROP_DAMAGED_TYPE='11963' THEN 'Home' ELSE 'Other' END AS PropertyDamagedType, CASE WHEN OTHER_INSURANCE='1' THEN 'Yes' ELSE 'No' END AS OTHERINSURANCE , PROPERTY_DAMAGED_ID, PARTY_TYPE, PARTY_TYPE_DESC, MLV.LOOKUP_VALUE_DESC AS PARTY_TYPE_VALUE_DESC,DESCRIPTION AS DESCRIBE_PROPERTY_DAMAGED"; //Added by Sibin for Itrack Issue 5145 on 3 Dec 2008
                ((BaseDataGrid)c1).SelectClause = SelectClause;//" convert(varchar(30),convert(money,isnull( ESTIMATE_AMOUNT,0)),1) DISPLAY_ESTIMATE_AMOUNT,convert(varchar(30),ESTIMATE_AMOUNT,1) SEARCH_ESTIMATE_AMOUNT, CASE WHEN PROP_DAMAGED_TYPE='11962' THEN 'Vehicle' WHEN PROP_DAMAGED_TYPE='11963' THEN 'Home' ELSE 'Other' END AS PropertyDamagedType, CASE WHEN OTHER_INSURANCE='1' THEN 'Yes' ELSE 'No' END AS OTHERINSURANCE , PROPERTY_DAMAGED_ID, PARTY_TYPE, PARTY_TYPE_DESC, MLV.LOOKUP_VALUE_DESC AS PARTY_TYPE_VALUE_DESC,DESCRIPTION AS DESCRIBE_PROPERTY_DAMAGED"; //Changed by santosh kumar gautam on 21 Dec 2008
				//specifying tables for from clause
				((BaseDataGrid)c1).FromClause=" CLM_PROPERTY_DAMAGED CPD LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON CPD.PARTY_TYPE = MLV.LOOKUP_UNIQUE_ID ";
				//specifying conditions for where clause
				((BaseDataGrid)c1).WhereClause=sWHERECLAUSE;//" agency_code not in ('"+ System.Configuration.ConfigurationSettings.AppSettings["CarrierSystemID"].ToString()   +  "')";
				//specifying Text to be shown in combo box				
				//((BaseDataGrid)c1).SearchColumnHeadings="Another Property Damaged^Property Damaged Description"; - Commented by Sibin for Itrack Issue 5145 on 3 Dec 2008
                ((BaseDataGrid)c1).SearchColumnHeadings = objResourceMgr.GetString("SearchColumnHeadings");//"Property Damaged Type^Property Damaged Description"; //Added by Sibin for Itrack Issue 5145 on 3 Dec 2008
				//specifying column to be used for combo box
				//((BaseDataGrid)c1).SearchColumnNames="CASE WHEN DAMAGED_ANOTHER_VEHICLE='1' THEN 'Yes' ELSE 'No' END^DESCRIPTION"; - Commented by Sibin for Itrack Issue 5145 on 3 Dec 2008
				((BaseDataGrid)c1).SearchColumnNames="CASE WHEN PROP_DAMAGED_TYPE='11962' THEN 'Vehicle' WHEN PROP_DAMAGED_TYPE='11963' THEN 'Home' ELSE 'Other' END^DESCRIPTION"; //Added by Sibin for Itrack Issue 5145 on 3 Dec 2008
				//search column data type specifying data type of the column to be used for combo box				
				((BaseDataGrid)c1).SearchColumnType="T^T";
				//specifying column for order by clause
				((BaseDataGrid)c1).OrderByClause="PROPERTY_DAMAGED_ID asc";
				//specifying column numbers of the query to be displyed in grid
				((BaseDataGrid)c1).DisplayColumnNumbers="2";
				//specifying column names from the query				
				//((BaseDataGrid)c1).DisplayColumnNames="ANOTHERVECHDAMAGED^DESCRIBE_PROPERTY_DAMAGED"; - Commented by Sibin for Itrack Issue 5145 on 3 Dec 2008
				((BaseDataGrid)c1).DisplayColumnNames="PropertyDamagedType^DESCRIBE_PROPERTY_DAMAGED"; //Added by Sibin for Itrack Issue 5145 on 3 Dec 2008
				//specifying text to be shown as column headings				
				//((BaseDataGrid)c1).DisplayColumnHeadings="Another Property Damaged^Property Damaged Description"; - Commented by Sibin for Itrack Issue 5145 on 3 Dec 2008
                ((BaseDataGrid)c1).DisplayColumnHeadings = objResourceMgr.GetString("DisplayColumnHeadings");//"Property Damaged Type^Property Damaged Description"; //Added by Sibin for Itrack Issue 5145 on 3 Dec 2008
				//specifying column heading display text length
				((BaseDataGrid)c1).DisplayTextLength="20^20";
				//specifying width percentage for columns
				((BaseDataGrid)c1).DisplayColumnPercent="20^20";
				//specifying primary column number
				((BaseDataGrid)c1).PrimaryColumns="1";
				//specifying primary column name
				((BaseDataGrid)c1).PrimaryColumnsName="PROPERTY_DAMAGED_ID";
				//specifying column type of the data grid
				((BaseDataGrid)c1).ColumnTypes="B^B";
				//specifying links pages 
				//((BaseDataGrid)c1).ColumnsLink="client.aspx^webindex.aspx^webindex.aspx^webindex.aspx^webindex.aspx";
				//specifying if double click is allowed or not
				((BaseDataGrid)c1).AllowDBLClick="true"; 
				//specifying which columns are to be displayed on first tab
				((BaseDataGrid)c1).FetchColumns="1^2^3^4";
				//specifying message to be shown
                ((BaseDataGrid)c1).SearchMessage = objResourceMgr.GetString("SearchMessage");//"Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				//specifying buttons to be displayed on grid
                ((BaseDataGrid)c1).ExtraButtons = objResourceMgr.GetString("ExtraButtons");//"1^Add New^0^addRecord";
				//specifying number of the rows to be shown
				((BaseDataGrid)c1).PageSize= int.Parse(GetPageSize());//int.Parse(System.Configuration.ConfigurationSettings.AppSettings.Get("GRID_PAGE_SIZE"));
				//specifying cache size (use for top clause)
				((BaseDataGrid)c1).CacheSize=int.Parse(GetCacheSize());//int.Parse(System.Configuration.ConfigurationSettings.AppSettings.Get("GRID_CACHE_SIZE"));
				//specifying image path
                ((BaseDataGrid)c1).ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                ((BaseDataGrid)c1).HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				//specifying heading
                ((BaseDataGrid)c1).HeaderString = objResourceMgr.GetString("HeaderString");//"Third Party Damage";
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
				((BaseDataGrid)c1).QueryStringColumns ="PROPERTY_DAMAGED_ID";
				((BaseDataGrid)c1).DefaultSearch="Y";     
                
				// to show completed task we have to give check box
				GridHolder.Controls.Add(c1);	
			
				
				TabCtl.TabURLs   =  "AddPropertyDamaged.aspx?&CLAIM_ID=" + hidCLAIM_ID.Value + "&CUSTOMER_ID=" + hidCUSTOMER_ID.Value + "&POLICY_ID=" + hidPOLICY_ID.Value + "&POLICY_VERSION_ID=" + hidPOLICY_VERSION_ID.Value + "&LOB_ID=" + hidLOB_ID.Value + "&";// TYPE_OF_HOME=" + hidTYPE_OF_HOME.Value + "&";
                TabCtl.TabTitles = Cms.CmsWeb.ClsMessages.GetTabTitles(base.ScreenId, "TabCtl");
                TabCtl.TabLength = 150;
				//TabCtl.TabTitles = "Property Damaged";
				//TabCtl.TabLength = 200;
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
       

			#endregion

		}

		
		private void GetQueryStringValues()
		{
			if(Request.QueryString["CLAIM_ID"]!=null && Request.QueryString["CLAIM_ID"].ToString()!="" )
				hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();

			if(Request.QueryString["CUSTOMER_ID"]!=null && Request.QueryString["CUSTOMER_ID"].ToString()!="" )
				hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();

			if(Request.QueryString["POLICY_ID"]!=null && Request.QueryString["POLICY_ID"].ToString()!="" )
				hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].ToString();

			if(Request.QueryString["POLICY_VERSION_ID"]!=null && Request.QueryString["POLICY_VERSION_ID"].ToString()!="" )
				hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();

			if(Request.QueryString["LOB_ID"]!=null && Request.QueryString["LOB_ID"].ToString()!="" )
				hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();			
			/*if(Request.QueryString["TYPE_OF_HOME"]!=null && Request.QueryString["TYPE_OF_HOME"].ToString()!="" )
				hidTYPE_OF_HOME.Value = Request.QueryString["TYPE_OF_HOME"].ToString();
			else
				hidTYPE_OF_HOME.Value = "0";*/
			
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
