/******************************************************************************************
<Author					: -   Anurag Verma
<Start Date				: -	  17 Nov 05  
<End Date				: -	
<Description			: -  Solid Fuel Information screen
<Review Date			: - 
<Reviewed By			: - 	
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
using Cms.CmsWeb.WebControls;
using System.Web.UI.HtmlControls;
using Cms.BusinessLayer.BlApplication;

namespace Cms.Policies.aspx.HomeOwners
{
	/// <summary>
	/// Summary description for PolicySolidFuelIndex.
	/// </summary>
	public class PolicySolidFuelIndex : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Label capMessage;
		protected System.Web.UI.WebControls.PlaceHolder GridHolder;
		protected Cms.CmsWeb.WebControls.BaseTabControl TabCtl;

		private string strCustomerID, strPolId, strPolVersionId;//,strFuelId;
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
			switch (strCalledFrom) 
			{
				case "Home":
				case "HOME":
					base.ScreenId="244";
					break;
				case "Rental":
				case "RENTAL":
					base.ScreenId="163";
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
			cltClientTop.PolicyID  = int.Parse(strPolId);
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
				objWebGrid.WebServiceURL = httpProtocol + Request.Url.Host + Request.ApplicationPath+ "/cmsweb/webservices/BaseDataGridWS.asmx?WSDL";
				objWebGrid.SelectClause = "FUEL_ID,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,LOCATION_ID,SUB_LOC_ID,MANUFACTURER,BRAND_NAME,MODEL_NUMBER,FUEL,STOVE_TYPE,HAVE_LABORATORY_LABEL,IS_UNIT,UNIT_OTHER_DESC,CONSTRUCTION,LOCATION,LOC_OTHER_DESC,YEAR_DEVICE_INSTALLED,WAS_PROF_INSTALL_DONE,INSTALL_INSPECTED_BY,INSTALL_OTHER_DESC,HEATING_USE,HEATING_SOURCE,OTHER_DESC,LOOKUP_VALUE_DESC,POL_HOME_OWNER_SOLID_FUEL.IS_ACTIVE";

				//if (strFuelId.Trim().Equals(""))
				
				//Getting from APP_HOME_OWNER_SOLID_FUEL
				objWebGrid.FromClause = "POL_HOME_OWNER_SOLID_FUEL LEFT OUTER JOIN MNT_LOOKUP_VALUES ON LOCATION=LOOKUP_UNIQUE_ID";
					
				
					
				objWebGrid.WhereClause = " CUSTOMER_ID='" + strCustomerID 
					+ "' AND POLICY_ID='" + strPolId 
					+ "' AND POLICY_VERSION_ID = '" + strPolVersionId + "'";
			
				
				
				objWebGrid.SearchColumnHeadings = "Location^Manufacturer^Brand Name^Model Number";
				objWebGrid.SearchColumnNames = "LOCATION^MANUFACTURER^BRAND_NAME^MODEL_NUMBER";
				objWebGrid.SearchColumnType = "T^T^T^T";
				objWebGrid.OrderByClause = "LOCATION asc";
				objWebGrid.DisplayColumnNumbers = "16^25^8^9";
				objWebGrid.DisplayColumnNames = "LOOKUP_VALUE_DESC^MANUFACTURER^BRAND_NAME^MODEL_NUMBER";
				objWebGrid.DisplayColumnHeadings = "Location^Manufacturer^Brand Name^Model Number";
				objWebGrid.DisplayTextLength = "150^200^200^200";
				objWebGrid.DisplayColumnPercent = "15^20^20^20";
				objWebGrid.PrimaryColumns = "1";
				objWebGrid.PrimaryColumnsName = "FUEL_ID";
				objWebGrid.ColumnTypes = "B^B^B^B";
				objWebGrid.AllowDBLClick = "true";
				objWebGrid.FetchColumns = "1^2^3^4^5^6^7^8^9^10^11^12^13^14^15^16^17^18^19^20^21^22^23^24";
				objWebGrid.SearchMessage = "Please select the Search Option, enter Search Criteria, and click on the Search button^Please select the Search Option, enter Search Criteria, and click on the Search button";objWebGrid.ExtraButtons = "";
				objWebGrid.ExtraButtons = "1^Add New^0^addRecord";
				objWebGrid.PageSize =int.Parse(GetPageSize());
				objWebGrid.CacheSize = int.Parse(GetCacheSize());
                objWebGrid.ImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/collapse_icon.gif";
                objWebGrid.HImagePath = System.Configuration.ConfigurationManager.AppSettings.Get("RootURL").Trim() + "/images/expand_icon.gif";
				objWebGrid.HeaderString ="Solid Fuel" ;
				objWebGrid.SelectClass = colors ;
				objWebGrid.FilterLabel = "Include Inactive";
				objWebGrid.FilterColumnName = "POL_HOME_OWNER_SOLID_FUEL.IS_ACTIVE";
				objWebGrid.FilterValue = "y";
				objWebGrid.DefaultSearch = "Y";
				objWebGrid.RequireQuery = "Y";
				objWebGrid.QueryStringColumns = "FUEL_ID";
				//Adding to controls to gridholder
				GridHolder.Controls.Add(objWebGrid);
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			#endregion
		
			TabCtl.TabURLs = "PolicyAddSolidFuel.aspx?CUSTOMER_ID=" + strCustomerID 
				+ "&POL_ID=" + strPolId 
				+ "&POL_VERSION_ID=" + strPolVersionId 
				+ "&CalledFrom=" + strCalledFrom
				+ "&";
			#region set Workflow cntrol
			SetWorkFlow();
			#endregion
		}
		private void GetSessionValues()
		{
			strPolId = base.GetPolicyID();
			strPolVersionId = base.GetPolicyVersionID();
			strCustomerID = base.GetCustomerID();
		}

		private bool CanShow()
		{
			//Checking whether customer,application and version id exits in session or not
			if (strPolId == "" || strPolVersionId  == "" || strCustomerID == "")
			{
				return false;
			}

			return true;
		}

		private void SetWorkFlow()
		{
			if(base.ScreenId == "244" || base.ScreenId == "163")
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
