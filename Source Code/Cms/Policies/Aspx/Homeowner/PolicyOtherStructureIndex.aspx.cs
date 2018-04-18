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
	/// Summary description for OtherLocationsIndex.
	/// </summary>
	public class OtherLocationsIndex : Cms.Policies.policiesbase
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
		string strCalledFrom="";
		int intDwelling;

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
					base.ScreenId="239_7";
					break;
				case "RENTAL":
					base.ScreenId="259_3";
					break;
						
			}
			#endregion
			GetQueryString();
			GetSessionValues();
			
			if (!CanShow())
			{
				capMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("109");
				capMessage.Visible=true;
				return;
			}
			
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
				
				objWebGrid.SelectClause = "OTHER_STRUCTURE_ID"
					+ ",Rtrim(Ltrim(IsNull(LOCATION_ADDRESS,'') + ' ' + IsNull(LOCATION_CITY,'') + ' ' + IsNull(STATE_NAME,''))) LOC_ADD1 "
					+ ",ISNULL(LKP.LOOKUP_VALUE_DESC,' ') as PREMISES_LOCATION,POL_OTHER_STRUCTURE_DWELLING.CUSTOMER_ID,POL_OTHER_STRUCTURE_DWELLING.POLICY_ID,POL_OTHER_STRUCTURE_DWELLING.POLICY_VERSION_ID,POL_OTHER_STRUCTURE_DWELLING.IS_ACTIVE"
					+ ",CASE WHEN LEN(PREMISES_DESCRIPTION)>150 THEN SUBSTRING(PREMISES_DESCRIPTION,0,148)+ '...' ELSE PREMISES_DESCRIPTION END AS PREMISES_DESCRIPTION, PREMISES_USE ";//Added Case for PREMISES_DESCRIPTION by Charles on 15-Sep-09 for Itrack 6405
				
				objWebGrid.FromClause = "POL_OTHER_STRUCTURE_DWELLING "
					+ " LEFT JOIN MNT_LOOKUP_VALUES LKP ON LKP.LOOKUP_UNIQUE_ID=POL_OTHER_STRUCTURE_DWELLING.PREMISES_LOCATION" 
					+ " LEFT JOIN MNT_COUNTRY_STATE_LIST ON POL_OTHER_STRUCTURE_DWELLING.LOCATION_STATE = MNT_COUNTRY_STATE_LIST.STATE_ID "	;

				
				objWebGrid.WhereClause = " POL_OTHER_STRUCTURE_DWELLING.CUSTOMER_ID = '" +strCustomerID
					+ "' AND POL_OTHER_STRUCTURE_DWELLING.POLICY_ID = '" + strPolId 
					+ "' AND POL_OTHER_STRUCTURE_DWELLING.POLICY_VERSION_ID = '" + strPolVersionId
					+ "' AND POL_OTHER_STRUCTURE_DWELLING.DWELLING_ID = '" + intDwelling + "'";
				
				
				objWebGrid.SearchColumnHeadings = "Other Structure# ^Location^Description^Use";
				objWebGrid.SearchColumnNames	= "OTHER_STRUCTURE_ID^LKP.LOOKUP_VALUE_DESC^PREMISES_DESCRIPTION^PREMISES_USE";
				objWebGrid.SearchColumnType		= "T^T^T^T";
								
				objWebGrid.OrderByClause = "OTHER_STRUCTURE_ID ASC";
				
				objWebGrid.DisplayColumnNumbers = "1^3^8^9";
				objWebGrid.DisplayColumnNames = "OTHER_STRUCTURE_ID^PREMISES_LOCATION^PREMISES_DESCRIPTION^PREMISES_USE";
				objWebGrid.DisplayColumnHeadings = "Other Structure#^Location^Description^Use";

				objWebGrid.DisplayTextLength = "10^30^30^30";
				objWebGrid.DisplayColumnPercent = "10^30^30^30";				
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "OTHER_STRUCTURE_ID";

				objWebGrid.ColumnTypes = "B^B^B^B";

				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^8^9";

				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";
				
				objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
				objWebGrid.PageSize = int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.HeaderString = "Other Structures Detail";
				objWebGrid.SelectClass = colors;	
				objWebGrid.RequireQuery = "Y";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.QueryStringColumns = "OTHER_STRUCTURE_ID";
				objWebGrid.FilterLabel = "Include Inactive";
				objWebGrid.FilterValue = "Y";
				objWebGrid.FilterColumnName ="POL_OTHER_STRUCTURE_DWELLING.IS_ACTIVE";
				//Adding to controls to gridholder
				GridHolder.Controls.Add(c1);
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			#endregion

			TabCtl.TabURLs = "PolicyAddOtherStructures.aspx?CUSTOMER_ID=" + strCustomerID 
				+ "&POLICY_ID=" + strPolId 
				+ "&POLICY_VERSION_ID=" + strPolVersionId
				+ "&CalledFrom=" + strCalledFrom + "&DWELLINGID=" + intDwelling + "&";

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
			
			intDwelling   = int.Parse(Request.QueryString["DWELLINGID"].ToString());
            
		}

		private void GetQueryString()
		{
		}

		private void SetWorkFlow()
		{
			if(base.ScreenId == "239_7" || base.ScreenId == "259_3")
			{
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				myWorkFlow.AddKeyValue("DWELLING_ID",intDwelling.ToString());
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