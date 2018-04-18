/******************************************************************************************
	<Author					: -> Ravindra Kumar Gupta
	<Start Date				: -> 03-21-2006
	<End Date				: ->
	<Description			: -> Index Page for umbrella real estate locations(Policies)
	<Review Date			: ->
	<Reviewed By			: ->
	
	Modification History
******************************************************************************************/
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
using Cms.BusinessLayer.BlCommon;


namespace Cms.Policies.Aspx.Umbrella
{
	/// <summary>
	/// Summary description for PolicyRealExtateLocationIndex.
	/// </summary>
	public class PolicyRealExtateLocationIndex :Cms.Policies.policiesbase 
	{
		
		#region Page Controls Declaration
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm POLICY_UM_LOCATION_INDEX;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		#endregion
		
		protected string strCustomerID, strPolicyId, strPolicyVersionId,strLocationID;
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId ="274";

			strPolicyId = base.GetPolicyID();
			strPolicyVersionId = base.GetPolicyVersionID();
			strCustomerID = base.GetCustomerID();

			cltClientTop.PolicyID  = int.Parse(strPolicyId );
			cltClientTop.CustomerID = int.Parse(strCustomerID);
			cltClientTop.PolicyVersionID  = int.Parse(strPolicyVersionId );
			cltClientTop.ShowHeaderBand = "Policy";
			cltClientTop.Visible= true;

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

			#region loading web grid control
			
			Control c1 = LoadControl("~/cmsweb/webcontrols/BaseDataGrid.ascx");
			BaseDataGrid objWebGrid;
			objWebGrid = (BaseDataGrid)c1;

			try
			{
				//Setting web grid control properties
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host +  Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
							
				objWebGrid.SelectClause = "CLIENT_LOCATION_NUMBER,(IsNull(ADDRESS_1,'') + ' ' + IsNull(ADDRESS_2,'') + ' ' + IsNull(CITY,'') + ' ' + IsNull(COUNTY,'') + ' ' + IsNull(MNT.STATE_NAME,'') + ' ' + IsNull(ZIPCODE,'')) As Address,LOCATION_NUMBER,LOCATION_ID,POL.IS_ACTIVE,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,OTHER_POLICY";
				
				objWebGrid.FromClause = "POL_UMBRELLA_REAL_ESTATE_LOCATION POL LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MNT ON POL.STATE = MNT.STATE_ID";
				objWebGrid.WhereClause = " POL.CUSTOMER_ID = '" +strCustomerID
					+ "' AND POL.POLICY_ID = '" + strPolicyId 
					+ "' AND POL.POLICY_VERSION_ID = '" + strPolicyVersionId
					+ "'";
				
				objWebGrid.SearchColumnHeadings = "Location #^Address^Policy Number";
				objWebGrid.SearchColumnNames = "LOCATION_NUMBER^(IsNull(ADDRESS_1,'') + ' ' + IsNull(ADDRESS_2,'') + ' ' + IsNull(CITY,'') + ' ' + IsNull(COUNTY,'') + ' ' + IsNull(MNT.STATE_NAME,'') + ' ' + IsNull(ZIPCODE,''))^OTHER_POLICY";
				objWebGrid.SearchColumnType = "T^T^T";
				
				objWebGrid.OrderByClause = "LOCATION_NUMBER ASC";
				
				objWebGrid.DisplayColumnNumbers = "1^2^3";
				objWebGrid.DisplayColumnNames = "LOCATION_NUMBER^Address^OTHER_POLICY";
				objWebGrid.DisplayColumnHeadings = "Location #^Address^Policy number";

				objWebGrid.DisplayTextLength = "25^100^25";
				objWebGrid.DisplayColumnPercent = "15^60^15";

				/*objWebGrid.SearchColumnHeadings = "Location #^Customer Location #^Address";
				objWebGrid.SearchColumnNames = "LOCATION_NUMBER^CLIENT_LOCATION_NUMBER^IsNull(ADDRESS_1,'')!' ' !IsNull(ADDRESS_2,'')";
				objWebGrid.SearchColumnType = "T^T^T";
				
				objWebGrid.OrderByClause = "LOCATION_NUMBER ASC";
				
				objWebGrid.DisplayColumnNumbers = "1^2^3";
				objWebGrid.DisplayColumnNames = "LOCATION_NUMBER^CLIENT_LOCATION_NUMBER^Address";
				objWebGrid.DisplayColumnHeadings = "Location #^Customer Location#^Address";

				objWebGrid.DisplayTextLength = "25^25^100";
				objWebGrid.DisplayColumnPercent = "15^15^60";*/
				objWebGrid.PrimaryColumns = "4";
				objWebGrid.PrimaryColumnsName = "POL.LOCATION_ID";

				objWebGrid.ColumnTypes = "B^B^B";

				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4";

				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				objWebGrid.FilterLabel = "Include Inactive";
				objWebGrid.FilterColumnName = "POL.IS_ACTIVE";
				objWebGrid.FilterValue = "Y";

				//objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
				objWebGrid.ExtraButtons = "2^Add New~View Records^0~1^addRecord~CopySchRecords";
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.HeaderString = "Locations - Other Carriers";
				objWebGrid.SelectClass = colors;	
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.QueryStringColumns = "LOCATION_ID";
				
				//Adding to controls to gridholder
				GridHolder.Controls.Add(c1);   
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			#endregion

			#region set Workflow cntrol
			SetWorkFlow();
			#endregion

			TabCtl.TabURLs = "PolicyAddRealEstateLocation.aspx?CUSTOMER_ID=" + strCustomerID 
				+ "&POLICY_ID=" + strPolicyId 
				+ "&POLICY_VERSION_ID=" + strPolicyVersionId + "&";


		}

		private void SetWorkFlow()
		{
			if(base.ScreenId == "274")
			{
				myWorkFlow.ScreenID = base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID ());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule ="POL";
			}
			else
			{
				myWorkFlow.Display	=	false;
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
