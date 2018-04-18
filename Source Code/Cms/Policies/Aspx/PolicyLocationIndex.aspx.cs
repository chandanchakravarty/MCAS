/******************************************************************************************
<Author					: -   Anurag verma
<Start Date				: -	  10 Nov 05  
<End Date				: -	
<Description			: -  Policy Location Index screen
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			:  
<Modified By			:   
<Purpose				:  
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
using Cms.CmsWeb.WebControls;

namespace Cms.Policies.Aspx
{
	/// <summary>
	/// Summary description for PolicyLocationIndex.
	/// </summary>
	public class PolicyLocationIndex : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;

		private string strCustomerID, strPolId, strPolVersionId;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		protected string strCalledFrom="";
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			#region Setting ScreenId
			// Check from where the screen is called.
			if (Request.QueryString["CalledFrom"] != null || Request.QueryString["CalledFrom"] !="")
			{
				strCalledFrom=Request.QueryString["CalledFrom"].ToString();
			}			
			//Setting screen Id.	
			switch (strCalledFrom.ToUpper()) 
			{
				case "HOME":
					//base.ScreenId="233";
					base.ScreenId="238";
					break;
				case "RENTAL":
					base.ScreenId="258";
					break;			
			}
			#endregion
			
			GetSessionValues();
			
			if (!CanShow())
			{
				capMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("109");
				capMessage.Visible=true;
				return;
			}
			cltClientTop.PolicyID= int.Parse(strPolId);
			cltClientTop.CustomerID = int.Parse(strCustomerID);
			cltClientTop.PolicyVersionID = int.Parse(strPolVersionId);
			cltClientTop.ShowHeaderBand = "Policy";
			cltClientTop.Visible= true;

			#region GETTING BASE COLOR FOR ROW SELECTION
			string colorScheme=GetColorScheme();
			string colors="";

			switch (int.Parse(colorScheme))
			{
				case 1:
					colors=System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR1").ToString();     
					break;
				case 2:
					colors=System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR2").ToString();     
					break;
				case 3:
					colors=System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR3").ToString();     
					break;
				case 4:
					colors=System.Configuration.ConfigurationManager.AppSettings.Get("GRID_COLOR4").ToString();     
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

				//				objWebGrid.SelectClause = "LOC_NUM"
				//					+ ",Rtrim(Ltrim(IsNull(LOC_ADD1,'') + ' ' + IsNull(LOC_ADD2,'') + ' ' + IsNull(LOC_CITY,'') + ' ' + IsNull(STATE_NAME,''))) LOC_ADD1 "
				//					+ ",APP_LOCATIONS.LOCATION_ID,Case IS_PRIMARY When  'Y' then 'Yes' else 'No' end as IS_PRIMARY,APP_LOCATIONS.CUSTOMER_ID,APP_LOCATIONS.APP_ID,APP_LOCATIONS.APP_VERSION_ID,APP_LOCATIONS.IS_ACTIVE";

				objWebGrid.SelectClause = "LOC_NUM"
					+ ",Rtrim(Ltrim(IsNull(LOC_ADD1,'') + ' ' + IsNull(LOC_ADD2,'') + ' ' + IsNull(LOC_CITY,'') + ' ' + IsNull(STATE_NAME,''))) LOC_ADD1 "
					+ ",POL_LOCATIONS.LOCATION_ID,ISNULL(LKP.LOOKUP_VALUE_DESC,' ') as LOC_TYPE,POL_LOCATIONS.CUSTOMER_ID,POL_LOCATIONS.POLICY_ID,POL_LOCATIONS.POLICY_VERSION_ID,POL_LOCATIONS.IS_ACTIVE";
				
				objWebGrid.FromClause = "POL_LOCATIONS "
					+ " LEFT JOIN MNT_LOOKUP_VALUES LKP ON LKP.LOOKUP_UNIQUE_ID=POL_LOCATIONS.LOCATION_TYPE" 
					+ " LEFT JOIN MNT_COUNTRY_STATE_LIST ON POL_LOCATIONS.LOC_STATE = MNT_COUNTRY_STATE_LIST.STATE_ID "
					+ " AND POL_LOCATIONS.LOC_COUNTRY = MNT_COUNTRY_STATE_LIST.COUNTRY_ID";

				
				objWebGrid.WhereClause = " POL_LOCATIONS.CUSTOMER_ID = '" +strCustomerID
					+ "' AND POL_LOCATIONS.POLICY_ID = '" + strPolId 
					+ "' AND POL_LOCATIONS.POLICY_VERSION_ID = '" + strPolVersionId
					+ "'";
				
				
				objWebGrid.SearchColumnHeadings = "Location#^Address^Location Is";
				objWebGrid.SearchColumnNames = "LOC_NUM^IsNull(LOC_ADD1,'') ! ' ' ! IsNull(LOC_ADD2,'') ! '' ! IsNull(LOC_CITY,'') ! ' ' ! IsNull(STATE_NAME,'')^LKP.LOOKUP_VALUE_DESC";
				objWebGrid.SearchColumnType = "T^T^T";
				
				objWebGrid.OrderByClause = "LOC_NUM ASC";
				
				objWebGrid.DisplayColumnNumbers = "1^2^4";
				objWebGrid.DisplayColumnNames = "LOC_NUM^LOC_ADD1^LOC_TYPE";
				objWebGrid.DisplayColumnHeadings = "Location#^Address^Location Is";

				objWebGrid.DisplayTextLength = "10^70^20";
				objWebGrid.DisplayColumnPercent = "10^70^20";
				objWebGrid.PrimaryColumns = "3";
				objWebGrid.PrimaryColumnsName = "POL_LOCATIONS.LOCATION_ID^POL_LOCATIONS.CUSTOMER_ID^POL_LOCATIONS.POLICY_ID^POL_LOCATIONS.POLICY_VERSION_ID";

				objWebGrid.ColumnTypes = "B^B^B";

				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4";

				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				
				string AgencyId = GetSystemId();
				if(AgencyId.ToUpper() != System.Configuration.ConfigurationManager.AppSettings.Get("CarrierSystemID").ToUpper())
					//objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
                    objWebGrid.ExtraButtons = "3^Add New~Request Loss Report~Prior Loss^0~1~2^addRecord~FetchLossReport~PriorLossTab";//added by avijit goswami on 10/02/2012 for singapore project.                    
				else                            
					objWebGrid.ExtraButtons = "3^Add New~Request Loss Report~Prior Loss Tab^0~1^addRecord~FetchLossReport~PriorLossTab";

				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
				objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim()+ "/images/collapse_icon.gif";
				objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim()+ "/images/expand_icon.gif";
				objWebGrid.HeaderString = "Location Details";
				objWebGrid.SelectClass = colors;	
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.QueryStringColumns = "LOCATION_ID";
				objWebGrid.FilterLabel = "Include Inactive";
				objWebGrid.FilterValue = "Y";
				objWebGrid.FilterColumnName ="POL_LOCATIONS.IS_ACTIVE";
				//Adding to controls to gridholder
				GridHolder.Controls.Add(c1);
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			#endregion

			TabCtl.TabURLs = "PolicyAddLocation.aspx?CUSTOMER_ID=" + strCustomerID 
				+ "&POL_ID=" + strPolId 
				+ "&POL_VERSION_ID=" + strPolVersionId
				+ "&CalledFrom=" + strCalledFrom + "&";

			#region set Workflow cntrol
			SetWorkFlow();
			#endregion

		}
		private bool CanShow()
		{
			//Checking whether customer id exits in database or not
			if (strPolId == "")
			{
				return false;
			}

			return true;
		}

		private void GetSessionValues()
		{
			strPolId = base.GetPolicyID();
			strPolVersionId = base.GetPolicyVersionID();
			strCustomerID = base.GetCustomerID();
		}

	
		private void SetWorkFlow()
		{
			if(base.ScreenId == "238" || base.ScreenId == "258")
			{
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.WorkflowModule="POL";
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
