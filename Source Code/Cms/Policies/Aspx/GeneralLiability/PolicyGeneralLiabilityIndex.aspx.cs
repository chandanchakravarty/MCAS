/******************************************************************************************
	<Author					: -> Ravindra Kumar Gupta
	<Start Date				: -> 04-03-2006
	<End Date				: ->
	<Description			: -> Index Page for General Liability Details
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

namespace Cms.Policies.Aspx.GeneralLiability
{
	/// <summary>
	/// Summary description for PolicyGeneralLiabilityIndex.
	/// </summary>
	public class PolicyGeneralLiabilityIndex :Cms.Policies.policiesbase 
	{
		#region Page Controls Declaration
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
//		protected System.Web.UI.WebControls.Label capMessage;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow; 
		#endregion
		
		#region Local Variables Declaration
		string strPolicyId, strPolicyVersionId, strCustomerID;
		#endregion
		protected System.Web.UI.WebControls.Label capMessage;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId ="283_0";
			strCustomerID=GetCustomerID();
			strPolicyId =GetPolicyID ();
			strPolicyVersionId=GetPolicyVersionID();

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

				objWebGrid.SelectClause = " ISNULL((CAST(AL.LOCATION_ID AS VARCHAR(5)) + ' ' + ISNULL(AL.LOC_ADD1,'') + ' ' + ISNULL(LOC_CITY,'')  + ' ' + ISNULL(AL.LOC_STATE,'')),'') AS LOCATIONS,AL.LOCATION_ID , ";
				objWebGrid.SelectClause += " GL.POLICY_GEN_ID,GL.LOCATION_ID,GL.CLASS_CODE,GL.BUSINESS_DESCRIPTION,GL.EXPOSURE,GL.RATE,GL.IS_ACTIVE, "; 
				objWebGrid.SelectClause += " L1.LOOKUP_VALUE_DESC AS COVERAGETYPE,L2.LOOKUP_VALUE_DESC AS COVERAGEFORM,L3.LOOKUP_VALUE_DESC AS EXPOSUREBASE ";
				
				objWebGrid.FromClause = "  POL_GENERAL_LIABILITY_DETAILS GL LEFT JOIN MNT_LOOKUP_VALUES L1 ON GL.COVERAGE_TYPE=L1.LOOKUP_UNIQUE_ID LEFT JOIN MNT_LOOKUP_VALUES L2 ON GL.COVERAGE_FORM=L2.LOOKUP_UNIQUE_ID LEFT JOIN MNT_LOOKUP_VALUES L3 ON GL.EXPOSURE_BASE=L3.LOOKUP_UNIQUE_ID ";
				objWebGrid.FromClause +=" LEFT JOIN POL_LOCATIONS AL ON GL.CUSTOMER_ID=AL.CUSTOMER_ID AND GL.POLICY_ID=AL.POLICY_ID AND GL.POLICY_VERSION_ID=AL.POLICY_VERSION_ID AND GL.LOCATION_ID=AL.LOCATION_ID ";
				
				objWebGrid.WhereClause = " GL.CUSTOMER_ID = '" + strCustomerID
					+ "' AND GL.POLICY_ID = '" + strPolicyId
					+ "' AND GL.POLICY_VERSION_ID = '" + strPolicyVersionId
					+ "'";
				
				objWebGrid.SearchColumnHeadings = "Location^Class Code^Business Description^Coverage Type^Coverage Form^Exposure Base^Exposure^Rate";
				objWebGrid.SearchColumnNames=	"ISNULL((CAST(AL.LOCATION_ID AS VARCHAR(5)) ! ' ' ! ISNULL(AL.LOC_ADD1,'') ! ' ' ! ISNULL(LOC_CITY,'')  ! ' ' ! ISNULL(AL.LOC_STATE,'')),'')^CLASS_CODE^BUSINESS_DESCRIPTION^L1.LOOKUP_VALUE_DESC^L2.LOOKUP_VALUE_DESC^L3.LOOKUP_VALUE_DESC^EXPOSURE^RATE";
				
				objWebGrid.SearchColumnType = "T^T^T^T^T^T^T^T";
				
				objWebGrid.OrderByClause = "AL.LOCATION_ID ASC";
				
				objWebGrid.DisplayColumnNumbers = "1^2^3^4^5^6^7^8";
				objWebGrid.DisplayColumnNames = "LOCATIONS^CLASS_CODE^BUSINESS_DESCRIPTION^COVERAGETYPE^COVERAGEFORM^EXPOSUREBASE^EXPOSURE^RATE";
				objWebGrid.DisplayColumnHeadings = "Location^Class Code^Business Description^Coverage Type^Coverage Form^Exposure Base^Exposure^Rate";

				//objWebGrid.DisplayTextLength = "10^20^20^20^10^20^20^20";
				objWebGrid.DisplayColumnPercent =  "5^5^20^20^20^20^5^5";
				objWebGrid.PrimaryColumns = "3";
				objWebGrid.PrimaryColumnsName = "POLICY_GEN_ID";

				objWebGrid.ColumnTypes = "B^B^B^B^B^B^B^B";

				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7";

				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				
				objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.HeaderString = "General Liability Details";
				objWebGrid.SelectClass = colors;	
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.QueryStringColumns = "POLICY_GEN_ID";
				/*objWebGrid.FilterLabel = "Include Inactive";
				objWebGrid.FilterValue = "Y";
				objWebGrid.FilterColumnName ="GL.IS_ACTIVE";*/
				//Adding to controls to gridholder
				GridHolder.Controls.Add(c1);
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			#endregion
			SetWorkFlow(); 

			TabCtl.TabURLs = "PolicyGeneralLiabilityDetails.aspx?CUSTOMER_ID=" + strCustomerID 
				+ "&POLICY_ID=" + strPolicyId  
				+ "&POLICY_VERSION_ID=" + strPolicyVersionId + "&";
		}
		#region SetWorkFlow Function 
		private void SetWorkFlow()
		{
			if(base.ScreenId == "283_0")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule="POL";
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}		
		#endregion

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
