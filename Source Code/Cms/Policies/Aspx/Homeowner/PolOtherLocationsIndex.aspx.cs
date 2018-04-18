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

namespace Policies.Aspx.Homeowner
{
	/// <summary>
	/// Summary description for PolOtherLocationsIndex.
	/// </summary>
	public class PolOtherLocationsIndex : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;
		private string strCustomerID, strPolicyId, strPolicyVersionId;
		protected System.Web.UI.WebControls.Label lblTemplate;
		protected System.Web.UI.HtmlControls.HtmlForm indexForm;
		protected System.Web.UI.HtmlControls.HtmlGenericControl tabLayer;
		protected Cms.CmsWeb.WebControls.ClientTop cltClientTop;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		public string tblHeaderClass;
		string strCalledFrom="";
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			tblHeaderClass="tableWidthHeader";
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
					base.ScreenId="239_8";
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
			
			cltClientTop.ApplicationID	= int.Parse(strPolicyId);
			cltClientTop.CustomerID		= int.Parse(strCustomerID);
			cltClientTop.AppVersionID	= int.Parse(strPolicyVersionId);
			cltClientTop.ShowHeaderBand	= "Policy";
			cltClientTop.Visible= false;
	
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
				objWebGrid.SelectClause = "LOC_NUM"
					+ ",CASE WHEN LEN(Rtrim(Ltrim(IsNull(LOC_ADD1,'') + ' ' + IsNull(LOC_CITY,'') + ' ' + IsNull(STATE_NAME,''))))>150 THEN SUBSTRING(Rtrim(Ltrim(IsNull(LOC_ADD1,'') + ' ' + IsNull(LOC_CITY,'') + ' ' + IsNull(STATE_NAME,''))),0,148)+ '...' ELSE Rtrim(Ltrim(IsNull(LOC_ADD1,'') + ' ' + IsNull(LOC_CITY,'') + ' ' + IsNull(STATE_NAME,''))) END AS LOC_ADD1" //Case added by Charles on 21-Sep-09 for Itrack 6406
					+ ",MNT1.LOOKUP_VALUE_DESC as PHOTO_ATTACHED,MNT2.LOOKUP_VALUE_DESC as OCCUPIED_BY_INSURED"
					+ ",POL_OTHER_LOCATIONS.LOCATION_ID,POL_OTHER_LOCATIONS.CUSTOMER_ID,POL_OTHER_LOCATIONS.POLICY_ID,POL_OTHER_LOCATIONS.POLICY_VERSION_ID,POL_OTHER_LOCATIONS.IS_ACTIVE,"
					+ "CASE WHEN LEN(POL_OTHER_LOCATIONS.DESCRIPTION)>25 THEN SUBSTRING(POL_OTHER_LOCATIONS.DESCRIPTION,0,23)+ '...' ELSE POL_OTHER_LOCATIONS.DESCRIPTION END AS DESCRIPTION";//Description added by Charles on 21-Sep-09 for Itrack 6406
				
				objWebGrid.FromClause = "POL_OTHER_LOCATIONS "
					+ " LEFT JOIN MNT_COUNTRY_STATE_LIST ON POL_OTHER_LOCATIONS.LOC_STATE = MNT_COUNTRY_STATE_LIST.STATE_ID"
					+ " LEFT JOIN  MNT_LOOKUP_VALUES MNT1 ON POL_OTHER_LOCATIONS.PHOTO_ATTACHED = MNT1.LOOKUP_UNIQUE_ID  "
					+ " LEFT JOIN MNT_LOOKUP_VALUES MNT2 ON POL_OTHER_LOCATIONS.OCCUPIED_BY_INSURED= MNT2.LOOKUP_UNIQUE_ID";
										

				
				objWebGrid.WhereClause = " POL_OTHER_LOCATIONS.CUSTOMER_ID = '" +strCustomerID
					+ "' AND POL_OTHER_LOCATIONS.POLICY_ID = '" + strPolicyId 
					+ "' AND POL_OTHER_LOCATIONS.POLICY_VERSION_ID = '" + strPolicyVersionId
					+ "' AND POL_OTHER_LOCATIONS.DWELLING_ID = '" + Request.QueryString["DWELLINGID"].ToString()
					+ "'";
				
				
				objWebGrid.SearchColumnHeadings = "Location#^Address^Photo Attached^Occupied by Insured^Description";//Description added by Charles on 21-Sep-09 for Itrack 6406
				objWebGrid.SearchColumnNames = "LOC_NUM^IsNull(LOC_ADD1,'') ! '' ! IsNull(LOC_CITY,'') ! ' ' ! IsNull(STATE_NAME,'')^IsNull(MNT1.LOOKUP_VALUE_DESC,'')^IsNull(MNT2.LOOKUP_VALUE_DESC,'')^ISNULL(DESCRIPTION,'')";//Description added by Charles on 21-Sep-09 for Itrack 6406
				objWebGrid.SearchColumnType = "T^T^T^T^T";//Extra T added by Charles on 21-Sep-09 for Itrack 6406
				
				objWebGrid.OrderByClause = "LOC_NUM ASC";
				
				objWebGrid.DisplayColumnNumbers = "1^2^3^4^5";//5 added by Charles on 21-Sep-09 for Itrack 6406
				objWebGrid.DisplayColumnNames = "LOC_NUM^LOC_ADD1^PHOTO_ATTACHED^OCCUPIED_BY_INSURED^DESCRIPTION";//Description added by Charles on 21-Sep-09 for Itrack 6406
				objWebGrid.DisplayColumnHeadings = "Location#^Address^Photo Attached^Occupied by Insured^Description";//Description added by Charles on 21-Sep-09 for Itrack 6406

				objWebGrid.DisplayTextLength = "10^50^10^10^20";//Changed from "10^50^20^20" by Charles on 21-Sep-09 for Itrack 6406
				objWebGrid.DisplayColumnPercent = "10^50^10^10^20";//Changed from "10^50^20^20" by Charles on 21-Sep-09 for Itrack 6406
				objWebGrid.PrimaryColumns = "3";
				objWebGrid.PrimaryColumnsName = "LOCATION_ID";

				objWebGrid.ColumnTypes = "B^B^B^B^B";//Extra B added by Charles on 21-Sep-09 for Itrack 6406

				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5";//5 added by Charles on 21-Sep-09 for Itrack 6406

				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				
				objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.HeaderString = "Other Locations Details";
				objWebGrid.SelectClass = colors;	
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.QueryStringColumns = "LOCATION_ID";
				objWebGrid.FilterLabel = "Include Inactive";
				objWebGrid.FilterValue = "Y";
				objWebGrid.FilterColumnName ="POL_OTHER_LOCATIONS.IS_ACTIVE";
				//Adding to controls to gridholder
				GridHolder.Controls.Add(c1);
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			#endregion

			TabCtl.TabURLs = "AddPolOtherLocations.aspx?CUSTOMER_ID=" + strCustomerID 
				+ "&POLICY_ID=" + strPolicyId 
				+ "&POLICY_VERSION_ID=" + strPolicyVersionId
				+ "&DWELLING_ID=" + Request.QueryString["DWELLINGID"].ToString()
				+ "&CalledFrom=" + strCalledFrom + "&";

			#region set Workflow cntrol
			SetWorkFlow();
			#endregion
		}


		private bool CanShow()
		{
			//Checking whether customer id exits in database or not
			if (strPolicyId == "")
			{
				return false;
			}

			return true;
		}


		#region Session Values
		private void GetSessionValues()
		{
			strPolicyId = base.GetPolicyID();
			strPolicyVersionId = base.GetPolicyVersionID();
			strCustomerID = base.GetCustomerID();
		}
		#endregion

		#region Set Workflow
		private void SetWorkFlow()
		{
			if(base.ScreenId == "239_8")
			{ 
				myWorkFlow.IsTop = false;
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
